using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using TheCorebanking.Finance.Data;
using TheCorebanking.Finance.Data.Models;
using TheCorebanking.Finance.Models;
using TheCorebanking.Finance.Services;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCorebanking.Finance.Controllers
{
   // [Authorize]
    public class FundTransferController : Controller
    {
        private ILogger<FundTransferController> logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        string[] currency = { "NGN", "USD", "GBP", "JPY" };
        static Random rand = new Random();
        string[] status = { "Active", "Inactive" };
        TheCoreBankingContext db = new TheCoreBankingContext();

        public FundTransferController(ISetupUnitOfWork uowSetup, IHostingEnvironment hostingEnvironment, ILogger<FundTransferController> financeLogger)

        {
            setupUnitOfWork = uowSetup;
            _hostingEnvironment = hostingEnvironment;
            logger = financeLogger;
        }

        public ISetupUnitOfWork setupUnitOfWork { get; set; }
        // [Authorize(Roles = "finance")]
        //[AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.Currency = currency;

            var pending = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Approved == false).Count();
            var approved = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Approved == true).Count();
            var totalTransfer = setupUnitOfWork.SingleFund.GetAll().Sum(o => o.Amount);
            ViewBag.pending = pending;
            ViewBag.approve = approved;
            ViewBag.balance = totalTransfer;
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId || i.Id.ToString() == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            return View();
        }
        public string GetLoggedUser()
        {
            var logUser = User.Identity.Name??"tayo.olawumi";

            return logUser;
        }
        [HttpPost]
        public JsonResult Index(decimal availablebalance, TblSingleFundTransfer transaction)
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;
            var tellerExist = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Tilluser == logUser).Count();
            if (tellerExist == 0)
            {
                return Json(new { message = "You do not have the previledge to perform these transaction" });
            }
            var getbranch = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
            var branch = getbranch.BranchId;
            var BatchRef = "TRU/" + branch + "/" + rand.Next().ToString().Substring(0, 7);
            var tblCasa = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountnumber == transaction.AccountDr).FirstOrDefault();
            var tblCasas = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountnumber == transaction.AccountCr).FirstOrDefault();
            var customerAccount = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountnumber == transaction.AccountCr).FirstOrDefault();
            var GetApprovalLimit = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Tilluser == logUser && o.Isdelete == false).FirstOrDefault();
            //var GeneralAcctDr = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountnumber == transaction.AccountDr).FirstOrDefault();
            //var GeneralAcctCr = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == transaction.AccountCr).FirstOrDefault();
            var GeneralAcctDr = CustomerAndGL().Where(o => o.Accountnumber == transaction.AccountDr).FirstOrDefault();
            var GeneralAcctCr = CustomerAndGL().Where(o => o.Accountnumber == transaction.AccountCr).FirstOrDefault();
            bool EntryState = false;
            if (transaction.Amount == 0 || transaction.Amount.ToString() == string.Empty || transaction.Amount.ToString() == "" || transaction.AccountDr == string.Empty || transaction.AccountDr.ToString() == "")
            {
                return Json(new { message = "You cannot enter empty value" });
            }
            var staff = setupUnitOfWork.Security.GetAll().Where(o => o.StaffName == logUser && o.Approval == true).FirstOrDefault();
            ViewBag.Title = "Fintrak Banking System";
            var countPendingTransaction = setupUnitOfWork.SingleFund.GetAll().Where(m => m.Approved == false && m.AccountDr == transaction.AccountDr).Count();
            if (countPendingTransaction > 0)
            {
                return Json(new { message = "The account number have transaction(s) awaiting approval. Please be patient" });
            }
            //var pendingTransferAmount = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Approved == false && o.AccountDr == transaction.AccountDr).Sum(o => o.Amount);

            var accountBalance = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountnumber == transaction.AccountDr).FirstOrDefault();
            decimal currentBalance = loadCustomerBalance(transaction.AccountDr);
            //pendingTransferAmount = pendingTransferAmount == null ? 0 : Convert.ToDecimal(pendingTransferAmount);
            if (transaction.OperationType == 1 || transaction.OperationType == 2)
            {
                if (Convert.ToDecimal(transaction.Amount) > currentBalance)
                {

                    return Json(new { message = "Insufficient Fund" });
                }
            }
            //Decimal amountPending = Convert.ToDecimal(transaction.Amount) + Convert.ToDecimal(pendingTransferAmount);
            Decimal CurrentBalanceDr = currentBalance - Convert.ToDecimal(transaction.Amount);
            //if (transaction.Amount > currentBalance || transaction.TransactionType == 1 || transaction.TransactionType == 4 || GeneralAcctDr.Accountnumber == null)
            //{
            //    return Json(new { message = "Insuficient Fund...Account cannot be thrown into debit." + CurrentBalanceDr });
            //}
            var ReferenceID = GenerateReference();
            int TransID = 0;
            var transSequence = "TRN/" + branch + DateTime.Now.Second + "/" + rand.Next().ToString().Substring(0, 7);
            var transCode = branch + DateTime.Now.Second + "/" + rand.Next().ToString().Substring(0, 7);
            int ddlOperationType = 0;
            var cust = CustomerAndGL();
            var gl = setupUnitOfWork.Chart.GetAll().FirstOrDefault();

            if (GeneralAcctDr.isGL == 0 && GeneralAcctCr.isGL == 0)
            {
                ddlOperationType = 1;
            }
            if (GeneralAcctDr.isGL == 0 && GeneralAcctCr.isGL == 1)
            {
                ddlOperationType = 2;
            }
            if (GeneralAcctDr.isGL == 1 && GeneralAcctCr.isGL == 1)
            {
                ddlOperationType = 3;
            }
            if (GeneralAcctDr.isGL == 1 && GeneralAcctCr.isGL == 0)
            {
                ddlOperationType = 4;
            }
            bool isCheque = false;
            bool ChargeStamp = false;

            //TransID = setupUnitOfWork.Transaction.InsertFund(transaction.AccountDr, transaction.AccountCr, transaction.Amount, ddlOperationType, Convert.ToInt32(transaction.TransactionType), Convert.ToInt32(PostType.SINGLETRANSFER), logUser, transaction.NarrationDr, 
            //    transaction.NarrationCr, DateTime.Now.ToShortTimeString(), transSequence, transCode, branch, getbranch.CompanyId, EntryState,
            //    Convert.ToDateTime(transaction.PostDate), DateTime.Now, isCheque, 0, transaction.ChequeNo, transaction.ChargeStamp, 0);
            var existTran = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transSequence && o.TransCode == transCode).Count();
            if (existTran == 0)
            {
                TblSingleFundTransfer tblSingle = new TblSingleFundTransfer
                {
                    AccountCr = transaction.AccountCr,
                    AccountDr = transaction.AccountDr,
                    Amount = transaction.Amount,
                    OperationId = Convert.ToInt32(PostType.SINGLETRANSFER),
                    OperationType = ddlOperationType,
                    TransactionType = Convert.ToInt32(transaction.TransactionType),
                    CreateBy = logUser,
                    NarrationDr = transaction.NarrationDr,
                    NarrationCr = transaction.NarrationCr,
                    PostingTime = DateTime.Now.ToShortTimeString(),
                    TransCode = transCode,
                    Reference = transSequence,
                    BrCode = branch,
                    CoyCode = getbranch.CompanyId,
                    Remark = "PENDING",
                    InEntryState = EntryState,
                    PostDate = Convert.ToDateTime(transaction.PostDate),
                    ValueDate = DateTime.Now,
                    IsCheque = isCheque,
                    AppSource = 0,
                    ChequeNo = transaction.ChequeNo,
                    ChargeStamp = transaction.ChargeStamp,
                    TransferChargeType = 0,
                    IsAmend = false,
                    IsReciept = false,
                    IsBatch = false,
                    IsCancel = false,
                    IsJournal = false,
                    IsRepayment = false,
                    Approved = false,
                    Disapproved = false,
                    ApprovedBy = string.Empty,
                    DateApproved = DateTime.Now,
                    ApprovalLevel = 1,
                    ReciepNo = "0"
                };


                db.TblSingleFundTransfer.Add(tblSingle);
                TransID = db.SaveChanges();
            }

            string comment = string.Empty;
            var request = HttpContext.Request;
            var IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
            // The URL that was accessed
            var AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
            // Creates Timestamp
            var TimeStamp = DateTime.Now;
            TblBankingAuditTrail tblBankingAudit = new TblBankingAuditTrail();
            tblBankingAudit.Ipaddress = IPAddress;
            tblBankingAudit.TransDate = TimeStamp;
            tblBankingAudit.UserName = logUser;
            tblBankingAudit.BrCode = getbranch.BranchId.ToString();
            tblBankingAudit.CoyCode = getbranch.CompanyId.ToString();
            tblBankingAudit.TransType = "Transferred " + transaction.Amount + " between: " + transaction.AccountDr + " And " + transaction.AccountCr;
            tblBankingAudit.TransTime = TimeStamp.ToShortTimeString();
            tblBankingAudit.Status = true;
            tblBankingAudit.CmpName = "FINTRAK";
            db.TblBankingAuditTrail.Add(tblBankingAudit);
            db.SaveChanges();

            if (TransID == 1)
            {
                var transId = setupUnitOfWork.SingleFund.GetAll().Where(o => o.TransCode == transCode).FirstOrDefault();
                var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                TblApprovalTrack track = new TblApprovalTrack();
                track.ALevel = 1;
                track.Brcode = getbranch.BranchId.ToString();
                track.Coycode = getbranch.CompanyId.ToString();
                track.OperationDate = DateTime.Now;
                track.OperationId = Convert.ToInt32(PostType.SINGLETRANSFER);
                track.OperationName = "SINGLETRANSFER";
                track.Staffid = getbranch.StaffNo;
                track.Username = logUser;
                track.TransactionId = Convert.ToInt32(transId.Id);
                track.MaxAmount = approval.MaxAmt;
                track.MinAmount = approval.MinAmt;
                track.ALevel = approval.ApprovingLevel;
                setupUnitOfWork.ApprovalTrack.Add(track);

                setupUnitOfWork.Commit();
                return Json(new { message = " " });
            }
            else
            {
                return Json(new { message = " Error Occurred" + ReferenceID });
            }



        }

        public JsonResult customergl()
        {

            var cust = CustomerAndGL();
            return Json(cust.ToList());
        }
        [HttpGet]
        public List<vw_CustomerAndGLAccount> CustomerAndGL()
        {


            List<vw_CustomerAndGLAccount> customer = new List<vw_CustomerAndGLAccount>();
            var db = new TheCoreBankingContext();



            var infoQuery = (from casa in db.TblCasa
                             select new vw_CustomerAndGLAccount()
                             {
                                 ID = casa.Casaaccountid,
                                 AccountName = casa.Accountname,
                                 Accountnumber = casa.Accountnumber,
                                 Iscurrentaccount = casa.Iscurrentaccount,
                                 isGL = 0,
                                 AVAILABLEBALANCE = casa.Availablebalance,
                                 OPERATIONID = (int?)casa.Operationid
                             }


                             ).AsEnumerable().Union(from chart in db.TblFinanceChartOfAccount
                                                    select new vw_CustomerAndGLAccount()
                                                    {
                                                        ID = chart.Id,
                                                        AccountName = chart.AccountName,
                                                        Accountnumber = chart.AccountId,
                                                        Iscurrentaccount = false,
                                                        isGL = 1,
                                                        AVAILABLEBALANCE = 0,
                                                        OPERATIONID = (Int32?)0
                                                    }).ToList();
            return infoQuery;

        }

        [Authorize(Roles = "finance")]
        [AllowAnonymous]
        public IActionResult MultipleTransfers()
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            return View();
        }

        [Authorize(Roles = "finance")]
        [AllowAnonymous]
        public IActionResult MultipleTransfer(string message)
        {

            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId || i.Id.ToString() == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            return View();
        }

        [HttpPost]
        public JsonResult MultipleTransferAll(List<multipleTransfer> transfers, TotalResult totalResult, string BatchName, bool IsJournal, bool IsReciept)
        {

            var logUser = User.Identity.Name??"tayo.olawumi";

            if (totalResult.TotalDebit.ToString() == "0")
            {
                return Json(new { message = " Total debit must not be empty" });
            }

            if (totalResult.TotalCredit.ToString() == "0")
            {
                return Json(new { message = " Total credit must not be empty" });
            }
            int resultSP = 0;
            TblMultipleAccountToCreditFundTransfer multipleFundCredit = new TblMultipleAccountToCreditFundTransfer();
            List<multipleTransfer> listTransfers = new List<multipleTransfer>();
            int counter = 0;
            string AccountNo = string.Empty;
            string Reference = string.Empty;
            bool sourceCA = true;
            bool DestinationCA = true;
            if (totalResult.TotalCredit == totalResult.TotalDebit)
            {

                var Ref = "Batch" + "/" + rand.Next().ToString().Substring(0, 4);
                foreach (var Multiple in transfers)
                {

                    var listMultiple = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.Reference == Multiple.Reference).FirstOrDefault();
                    var existMultiple = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.AccountNo == listMultiple.AccountNo && o.Reference == Multiple.Reference).Count();

                    if (existMultiple > 0)
                    {
                        return Json(new { message = "Transaction already exist" });
                    }


                    if (listMultiple == null)
                    {
                        return Json(new { message = " " });
                    }
                    var acctfrom = loadGLBalance(listMultiple.AccountNoDr);
                    var customer = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser && o.Approved == true).FirstOrDefault();
                    var RefCode = "TR/" + listMultiple.BrCode + "/" + rand.Next().ToString().Substring(0, 7);
                    BatchName = RefCode;
                    if (customer.BranchId == listMultiple.BrCode)
                    {
                        sourceCA = true;

                    }
                    else
                    {
                        sourceCA = false;
                    }
                    if (customer.CompanyId == listMultiple.CoyCode)
                    {
                        DestinationCA = true;
                    }
                    else
                    {
                        DestinationCA = false;
                    }


                   
                    var existTemp = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.AccountNo == listMultiple.AccountNo).Count();
                    var acctName = CustomerAndGL().Where(o => o.Accountnumber == listMultiple.AccountNo).FirstOrDefault().AccountName;
                    var acctNameDR = CustomerAndGL().Where(o => o.Accountnumber == listMultiple.AccountNoDr).FirstOrDefault().AccountName;
                    var existTep1 = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.AccountNo == listMultiple.AccountNo && o.Reference == listMultiple.Reference).Count();
                    if (existMultiple == 0)
                    {


                        TblMultipleAccountToCreditFundTransfer multipleAll = new TblMultipleAccountToCreditFundTransfer
                        {
                            Reference = Multiple.Reference,
                            AccountNo = listMultiple.AccountNo,
                            BrCode = listMultiple.BrCode,
                            CoyCode = listMultiple.CoyCode,
                            Amount = Convert.ToDecimal(listMultiple.Amount),
                            OperationId = Convert.ToInt32(listMultiple.OperationId),
                            Narration = listMultiple.Narration,
                            TransCode = listMultiple.TransCode,
                            ValueDate = Convert.ToDateTime(listMultiple.ValueDate),
                            DateCreated = Convert.ToDateTime(listMultiple.DateCreated),
                            CreatedBy = logUser,
                            InEntryState = listMultiple.InEntryState,
                            TransactionType = listMultiple.TransactionType,
                            OperationType = listMultiple.OperationType,
                            BatchName = Ref,
                            AppSource = listMultiple.AppSource,
                            IsJournal = IsJournal,
                            IsReciept = IsReciept,
                            Approved = false,
                            Disapproved = false,
                            ChargeStamp = totalResult.ChargeStamp,
                            IsAmend = false,
                            IsCancel = false,
                            Remark = "PENDING",
                            ReciepNo = "0",
                            ApprovedBy = string.Empty,
                            DateApproved = DateTime.Now,
                            AccountNameCr = acctName,
                            AccountNoDr = listMultiple.AccountNoDr
                        };
                        TblBankingAccountToDebit debit = new TblBankingAccountToDebit
                        {
                            AccountName = acctNameDR,
                            AccountNo = listMultiple.AccountNoDr,
                            Amount = Convert.ToDecimal(listMultiple.Amount),
                            Approved = false,
                            ApprovedBy = string.Empty,
                            DateApproved = DateTime.Now,
                            DisApprove = false,
                            BrCode = listMultiple.BrCode,
                            CoyCode = listMultiple.CoyCode,
                            CreatedBy = logUser,
                            DateCreated = Convert.ToDateTime(listMultiple.DateCreated),
                            IsCancel = false,
                            IsUsed = false,
                            ValueDate = Convert.ToDateTime(listMultiple.ValueDate),
                            Narration = listMultiple.Narration,
                            Reference = listMultiple.Reference,
                            Transactiontype = listMultiple.TransactionType,
                            SlipNo = listMultiple.TransCode,

                        };
                        setupUnitOfWork.MultipleFund.Add(multipleAll);
                        setupUnitOfWork.Commit();
                        // db.TblMultipleAccountToCreditFundTransfer.Add(multipleAll);
                        db.TblBankingAccountToDebit.Add(debit);
                        resultSP = db.SaveChanges();
                        AccountNo = listMultiple.AccountNo;
                        Reference = listMultiple.Reference;
                        var existTemp2 = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.AccountNo == listMultiple.AccountNo && o.Reference == listMultiple.Reference).FirstOrDefault();
                        setupUnitOfWork.multipleFundTemp.Delete(existTemp2.Id);
                        setupUnitOfWork.Commit();

                    }

                    //var resultSP = setupUnitOfWork.Transaction.InsertMultipleFund(Ref, listMultiple.AccountNo, listMultiple.BrCode, listMultiple.CoyCode,
                    //    Convert.ToDecimal(listMultiple.Amount), Convert.ToInt32(listMultiple.OperationId), listMultiple.Narration, listMultiple.TransCode, 
                    //    Convert.ToDateTime(listMultiple.ValueDate), Convert.ToDateTime(listMultiple.DateCreated), logUser,
                    //    listMultiple.InEntryState, listMultiple.TransactionType, listMultiple.OperationType, BatchName, listMultiple.AppSource, IsJournal, IsReciept, false, false, totalResult.ChargeStamp);

                    if (resultSP == 1)
                    {

                        var transId = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.TransCode == listMultiple.TransCode).FirstOrDefault();
                        var staffId = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                        var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                        TblApprovalTrack track = new TblApprovalTrack();
                        track.ALevel = 1;
                        track.Brcode = listMultiple.BrCode.ToString();
                        track.Coycode = listMultiple.CoyCode.ToString();
                        track.OperationDate = DateTime.Now;
                        track.OperationId = Convert.ToInt32(PostType.MULTIPLETRANSFER);
                        track.OperationName = "MULTIPLETRANSFER";
                        track.Staffid = staffId.StaffNo;
                        track.Username = logUser;
                        track.TransactionId = Convert.ToInt32(transId.Id);
                        track.MaxAmount = approval.MaxAmt;
                        track.MinAmount = approval.MinAmt;
                        track.ALevel = approval.ApprovingLevel;

                        setupUnitOfWork.ApprovalTrack.Add(track);
                        setupUnitOfWork.Commit();

                    }

                    var request = HttpContext.Request;
                    var IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
                    var AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
                    var TimeStamp = DateTime.Now;
                    TblBankingAuditTrail tblBankingAudit = new TblBankingAuditTrail();
                    tblBankingAudit.Ipaddress = IPAddress;
                    tblBankingAudit.TransDate = TimeStamp;
                    tblBankingAudit.UserName = logUser;
                    tblBankingAudit.BrCode = listMultiple.BrCode;
                    tblBankingAudit.CoyCode = listMultiple.CoyCode;
                    tblBankingAudit.TransType = " Save Transaction With Batchref - " + Ref;
                    tblBankingAudit.TransTime = TimeStamp.ToShortTimeString();
                    tblBankingAudit.Status = true;
                    tblBankingAudit.CmpName = "MULTIPLETRANSFER";
                    setupUnitOfWork.Audit.Add(tblBankingAudit);
                    setupUnitOfWork.Commit();
                }

                return Json(new { message = " " });


            }
            return Json(new { message = "Invalid transaction-check the total credit must the same with total debit " });
        }

        [HttpPost]
        public JsonResult BatchTransfers([FromBody] List<multipleTransfer> transfers)
        {
            int success = 0;
            foreach (var Multiple in transfers)
            {
                bool sourceCA = true;
                bool DestinationCA = true;
                var listMultiple = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.Reference == Multiple.Reference).FirstOrDefault();
                var acctName = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountnumber == listMultiple.AccountNo).FirstOrDefault();
                var GlName = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == listMultiple.AccountNoDr).FirstOrDefault();
                var acctfrom = loadGLBalance(listMultiple.AccountNoDr);

                var branchId = setupUnitOfWork.Branch.GetAll().FirstOrDefault(); //.Where(o => o.BrId == listMultiple.BrCode)
                var companyId = setupUnitOfWork.Company.GetAll().FirstOrDefault();
                var RefCode = "TR/" + listMultiple.BrCode + "/" + rand.Next().ToString().Substring(0, 7);
                if (branchId.BrId == listMultiple.BrCode)
                {
                    sourceCA = true;
                }
                else
                {
                    sourceCA = false;
                }
                if (companyId.CoyId == listMultiple.CoyCode)
                {
                    DestinationCA = true;
                }
                else
                {
                    DestinationCA = false;
                }
                //success = setupUnitOfWork.Transaction.InsertTransfer(acctName.Customerid.ToString(), listMultiple.AccountNoDr, GlName.AccountName, listMultiple.BrCode, listMultiple.CoyCode, acctName.Miscode, Convert.ToDateTime(listMultiple.DateCreated), acctName.Accountname, acctName.Accountnumber, listMultiple.CoyCode, listMultiple.BrCode, Convert.ToInt16(PostType.CASATRANSFER), Convert.ToDecimal(listMultiple.Amount), listMultiple.Reference, false, Convert.ToInt16(Operations.TRANSFER),
                //   false, Convert.ToDecimal(acctfrom), true, Convert.ToDecimal(acctName.Availablebalance), acctName.Customerid.ToString(), sourceCA, DestinationCA, DateTime.Now.ToShortTimeString(), RefCode, listMultiple.Narration, false, false, Convert.ToDecimal(charge), Convert.ToDateTime(listMultiple.ValueDate), listMultiple.Remark);
                TblBankingCatransfer catransfer = new TblBankingCatransfer()
                {
                    CustCode = acctName.Customerid.ToString(),
                    ProductAcctNo = listMultiple.AccountNoDr,
                    AccountName = GlName.AccountName,
                    BranchId = listMultiple.BrCode,
                    CoyCode = listMultiple.CoyCode,
                    Miscode = acctName.Miscode,
                    DateCreated = Convert.ToDateTime(listMultiple.DateCreated),
                    TransAcctNo = acctName.Accountname,
                    TransAcctName = acctName.Accountnumber,
                    TransAcctCustCode = acctName.Customerid.ToString(),
                    TransBank = "1",
                    TransBankAddr = listMultiple.BrCode,
                    TransAcctType = listMultiple.TransactionType.ToString(),
                    ActualDate = Convert.ToDateTime(listMultiple.ValueDate),
                    AmtTransfered = Convert.ToDecimal(listMultiple.Amount),
                    Remark = RefCode,
                    Approved = false,
                    OperationId = listMultiple.OperationId,
                    Disapproved = false,
                    Balance = Convert.ToDecimal(acctName.Availablebalance),
                    Internal = true,
                    TransAcctBal = acctName.Availablebalance,
                    SourceCa = sourceCA,
                    DestinationCa = DestinationCA,
                    TransTime = DateTime.Now.ToShortTimeString(),
                    OtherSource = false,
                    SlipNumber = listMultiple.Reference,
                    Narration = listMultiple.Narration,
                    IsReversed = false,
                    SameCustomer = false,
                    Charge = 0,
                    IsCancel = false,
                    InEntryState = true,
                    Narration2 = listMultiple.Remark

                };
                db.TblBankingCatransfer.Add(catransfer);
                success = db.SaveChanges();
                if (success > 0)
                {

                    int TransID = success;
                    ApprovalTrack2 approval = new ApprovalTrack2
                    {
                        Opid = listMultiple.OperationId,
                        Transid = TransID,
                        Coycode = listMultiple.CoyCode,
                        Brcode = listMultiple.BrCode,
                        ALevel = 1,
                        Staffid = "staffid",
                        Username = User.Identity.Name??"tayo.olawumi",
                        Mins = 0,
                        Maxs = 1,
                        OpName = "MULTIPLETRANSFER"
                    };
                    TheCoreBankingContext db = new TheCoreBankingContext();
                    db.ApprovalTrack2.Add(approval);
                    db.SaveChanges();

                }
            }



            return Json(new { message = " " });
        }


        [HttpPost]
        public JsonResult TempTransfer(transactionModel transaction)
        {
            var logUser = User.Identity.Name??"tayo.olawumi";

            var customer = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser && o.Approved == true).FirstOrDefault();
            var tellerExist = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Tilluser == logUser).Count();
            if (tellerExist == 0)
            {
                return Json(new { message = "You do not have the previledge to perform the transaction" });
            }
            decimal currentBalance = loadCustomerBalance(transaction.AccountDr);
            if (transaction.OperationId == "1" || transaction.OperationId == "0")
            {
                if (Convert.ToDecimal(transaction.TotalDebit) > currentBalance)
                {
                    return Json(new { message = "You have insufficient balance" });
                }
            }

            if (Convert.ToInt32(transaction.Amount) == 0 || String.IsNullOrEmpty(transaction.Amount) == true || String.IsNullOrEmpty(transaction.AccountDr) == true || String.IsNullOrEmpty(transaction.AccountCr) == true)
            {
                return Json(new { message = "You cannot enter empty value" });
            }
            var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Tilluser == logUser).FirstOrDefault();
            if (transaction.TotalDebit > teller.Tilllimitamount)
            {
                return Json(new { message = "You do not have the previledge to perform the transaction. Total amount is greater than your cummulative amount limit!" });
            }

            if (Convert.ToDecimal(transaction.TotalDebit) <= Convert.ToDecimal(teller.Tilllimitamount))
            {


                Decimal balance = 0;
                var ReferenceID = GenerateReference();
                var company = customer.CompanyId;
                var branch = customer.BranchId;
                var staff = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser && o.Approved == true).FirstOrDefault();
                balance = Convert.ToDecimal(transaction.Amount) - PendingTransaction(transaction.AccountDr);
                var BatchRef = "REF/" + branch + "/" + rand.Next().ToString().Substring(0, 6);
                bool refExist = false;
                bool isValid;
                string TableRef = (from a in db.TblBankingCamultipleTransfer where a.Reference == BatchRef select a.Reference).FirstOrDefault();
                if (String.IsNullOrEmpty(TableRef) == true)
                {
                    BatchRef = "REF/" + branch + "/" + rand.Next().ToString().Substring(0, 7);
                }
                else
                {
                    BatchRef = "REF/" + branch + "/" + rand.Next().ToString().Substring(0, 6);
                }
                var pendingTransferAmount = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.Approved == false && o.AccountNoDr == transaction.AccountDr).Sum(o => o.Amount);
                if (Convert.ToDecimal(transaction.Amount) > balance)
                {

                    return Json(new { message = "In-Sufficient balance. Savings account cannot be thrown into debit - " });
                }
                transaction.TotalDebit = transaction.TotalDebit;

                TblMultipleAccountToCreditFundTransferTemp tblMultipleFund = new TblMultipleAccountToCreditFundTransferTemp
                {

                    AccountNo = transaction.AccountCr,
                    AccountNoDr = transaction.AccountDr,
                    Amount = Convert.ToDecimal(transaction.AmountCr),
                    BatchName = transaction.ChequeNo,
                    DateCreated = DateTime.Now,
                    Narration = transaction.NarrationDr,
                    AppSource = 1,
                    BrCode = branch.ToString(),
                    CoyCode = company.ToString(),
                    CreatedBy = logUser,
                    Reference = BatchRef,
                    Remark = "PENDING",
                    InEntryState = false,
                    Id = transaction.Id,
                    //Date = transaction.PostDate,               
                    IsAmend = false,
                    IsCancel = false,
                    IsJournal = true,
                    IsReciept = false,
                    OperationId = Convert.ToInt32(PostType.MULTIPLETRANSFER),
                    OperationType = Convert.ToInt32(transaction.OperationType),
                    TransactionType = Convert.ToInt32(transaction.TransactionType),
                    TransCode = ReferenceID,
                    ValueDate = Convert.ToDateTime(transaction.ValueDate),
                    Approved = false,
                    Disapproved = false,
                    ReciepNo = ("TRM/" + rand.Next(1234, 3241).ToString()),
                    TotalDebit = transaction.TotalDebit

                };
                setupUnitOfWork.multipleFundTemp.Add(tblMultipleFund);
                setupUnitOfWork.Commit();
                return Json(new { message = " " });
            }

            return Json(new { message = "The total debit must not be empty " });
        }
        [HttpPost]
        public IActionResult RemoveTransfer(transactionModel transaction)
        {
            var tblTemp = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.Id == transaction.Id).FirstOrDefault();


            setupUnitOfWork.multipleFundTemp.Delete(tblTemp);
            setupUnitOfWork.Commit();
            return Json(transaction.Id);
        }
        [Authorize(Roles = "finance")]
        [AllowAnonymous]
        [HttpGet]
        public IActionResult BulkUpload()
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId || i.Id.ToString() == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            return View();
        }

        public JsonResult UpdateMultiple([FromBody] tempTransferModel transactions)
        {
            var logUser = User.Identity.Name??"tayo.olawumi"; //"abayomi.adelola"; 

            var tblTemp = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.Reference == transactions.Reference).FirstOrDefault();
            tblTemp.Amount = Convert.ToDecimal(transactions.Amount);

            setupUnitOfWork.multipleFundTemp.Update(tblTemp);
            setupUnitOfWork.Commit();



            return Json(new { message = " " });
        }
        [HttpPost]
        public JsonResult ImportTransaction()
        {
            GeneralUploads();
            return Json("GeneralUpload");
        }
        public IList<TblBankingTransferUploadTemp> GeneralUploads()
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;

            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\batchupload.xlsx";
            FileInfo file = new FileInfo(filePath);
            var user = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
            var branch = user.BranchId;
            var BatchRef = "TRU/" + branch + "/" + rand.Next().ToString().Substring(0, 7);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["batchupload"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblBankingTransferUploadTemp> chartList = new List<TblBankingTransferUploadTemp>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {

                        string date = workSheet.Cells[i, 1].Value.ToString();
                        int first = date.IndexOf('-');
                        int last = date.LastIndexOf('-');
                        chartList.Add(new TblBankingTransferUploadTemp
                        {
                            AccountNoDr = workSheet.Cells[i, 1].Value.ToString(),
                            AccountNoCr = workSheet.Cells[i, 2].Value.ToString(),
                            ValueDate = Convert.ToDateTime(workSheet.Cells[i, 3].Value),
                            IsCheque = Convert.ToBoolean(workSheet.Cells[i, 4].Value),
                            ChequeNo = Convert.ToString(workSheet.Cells[i, 5].Value),
                            Amount = Convert.ToDecimal(workSheet.Cells[i, 6].Value),
                            NarrationDr = workSheet.Cells[i, 7].Value.ToString(),
                            NarrationCr = Convert.ToString(workSheet.Cells[i, 8].Value),
                            OperationType = Convert.ToInt32(workSheet.Cells[i, 9].Value),
                            TransType = Convert.ToInt32(workSheet.Cells[i, 10].Value),
                            BatchRef = BatchRef,
                            ErrorCode = 0,
                            IsDelete = false,

                        });
                    }
                }

                setupUnitOfWork.GeneralUploadTemp.AddRange(chartList);
                setupUnitOfWork.Commit();
                var request = HttpContext.Request;
                var IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
                // The URL that was accessed
                var AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
                // Creates Timestamp
                var TimeStamp = DateTime.Now;
                TblBankingAuditTrail tblBankingAudit = new TblBankingAuditTrail();
                tblBankingAudit.Ipaddress = IPAddress;
                tblBankingAudit.TransDate = TimeStamp;
                tblBankingAudit.UserName = logUser;
                tblBankingAudit.BrCode = user.BranchId;
                tblBankingAudit.CoyCode = user.CompanyId;
                tblBankingAudit.TransType = "Save Transaction With Batchref - " + BatchRef;
                tblBankingAudit.TransTime = TimeStamp.ToShortTimeString();
                tblBankingAudit.Status = true;
                tblBankingAudit.CmpName = "FINTRAK";
                setupUnitOfWork.Audit.Add(tblBankingAudit);
                setupUnitOfWork.Commit();
                return chartList.ToList();
            }
        }

        [HttpPost]
        public JsonResult ImportBulkUpload()
        {

            TransferUpload();
            return Json(new { message = " " });
        }
        public IList<TblBankingBatchTransferUploadTemp> TransferUpload()
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;

            string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\MultipleUpload.xlsx";
            FileInfo file = new FileInfo(filePath);
            var user = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
            var branch = user.BranchId;
            var BatchRef = "TRU/" + branch + "/" + rand.Next().ToString().Substring(0, 7);


            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["MultipleUpload"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblBankingBatchTransferUploadTemp> chartList = new List<TblBankingBatchTransferUploadTemp>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {
                        var customer = workSheet.Cells[i, 1].Value.ToString();
                        var account = CustomerAndGL().ToList().Where(o => o.Accountnumber == customer).FirstOrDefault().AccountName;
                        var balance = CustomerAndGL().ToList().Where(o => o.Accountnumber == customer).FirstOrDefault().AVAILABLEBALANCE;
                        string date = workSheet.Cells[i, 1].Value.ToString();
                        int first = date.IndexOf('-');
                        int last = date.LastIndexOf('-');
                        chartList.Add(new TblBankingBatchTransferUploadTemp
                        {
                            AccountNo = workSheet.Cells[i, 1].Value.ToString(),
                            //AccountNoCr = workSheet.Cells[i, 2].Value.ToString(),
                            //ValueDate = Convert.ToDateTime(workSheet.Cells[i, 3].Value),
                            IsCheque = Convert.ToBoolean(workSheet.Cells[i, 2].Value),
                            ChequeNo = Convert.ToString(workSheet.Cells[i, 3].Value),
                            Amount = Convert.ToDecimal(workSheet.Cells[i, 4].Value),
                            Narration = workSheet.Cells[i, 5].Value.ToString(),
                            StampDuty = Convert.ToBoolean(workSheet.Cells[i, 6].Value),
                            AccountName = account,
                            AccountBalance = balance,
                            BatchRef = BatchRef,
                            ErrorCode = 0,
                            IsDelete = false,
                            Status = "PENDING",


                        });
                    }
                }

                setupUnitOfWork.BatchUpload.AddRange(chartList);
                setupUnitOfWork.Commit();
                var request = HttpContext.Request;
                var IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
                // The URL that was accessed
                var AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
                // Creates Timestamp
                var TimeStamp = DateTime.Now;
                TblBankingAuditTrail tblBankingAudit = new TblBankingAuditTrail();
                tblBankingAudit.Ipaddress = IPAddress;
                tblBankingAudit.TransDate = TimeStamp;
                tblBankingAudit.UserName = logUser;
                tblBankingAudit.BrCode = branch;
                tblBankingAudit.CoyCode = user.CompanyId;
                tblBankingAudit.TransType = "Save Transaction With Batchref - " + BatchRef;
                tblBankingAudit.TransTime = TimeStamp.ToShortTimeString();
                tblBankingAudit.Status = true;
                tblBankingAudit.CmpName = "FINTRAK";
                setupUnitOfWork.Audit.Add(tblBankingAudit);
                setupUnitOfWork.Commit();
                return chartList.ToList();
            }
        }

        public IActionResult RemoveBulk(TblBankingBatchTransferUploadTemp TransferUpload)
        {

            setupUnitOfWork.BatchUpload.Delete(TransferUpload);
            setupUnitOfWork.Commit();
            return Json(TransferUpload.Id);
        }
        public JsonResult uploadTransfer([FromBody] List<TblBankingTransferUploadTemp> tempList, bool stamp, string BatchRef, string Stamp)
        {
            //var uploadList = setupUnitOfWork.GeneralUploadTemp.GetAll().Where(o => o.Id == temp.Id).FirstOrDefault();
            //var casa=CustomerAndGL().Where(o=>o.Accountnumber ==uploadList.AccountNoDr)
            var username = User.Identity.Name??"tayo.olawumi";
            int result = 0;
            var logUser = username;

            foreach (var temp in tempList)
            {
                var user = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                var branch = user.BranchId;
                result = setupUnitOfWork.Transaction.InsertBulkUploadTransfer(temp.BatchRef, logUser, branch, user.CompanyId, DateTime.Now.ToShortTimeString(), "0", DateTime.Now, stamp, 1);
            }
            if (result == 0)
            {
                return Json(new { message = "" });
            }
            else
            {
                return Json(new { message = "" });
            }
        }
        [HttpPost]
        public JsonResult batchuploadTransfer(List<BatchTransferUpload> batchData, BatchTransferUpload BatchTransfer)
        {
            //var uploadList = setupUnitOfWork.GeneralUploadTemp.GetAll().Where(o => o.Id == temp.Id).FirstOrDefault();
            var casa = CustomerAndGL().Where(o => o.AccountName == BatchTransfer.acctNodr).FirstOrDefault().Accountnumber;
            var username = User.Identity.Name??"tayo.olawumi";
            int result = 0;
            if (string.IsNullOrEmpty(BatchTransfer.acctNodr) == true || string.IsNullOrEmpty(BatchTransfer.amountdr.ToString()) == true || string.IsNullOrEmpty(BatchTransfer.type.ToString()) == true)
            {
                return Json(new { message = "You need to enter your acct or amount to debit or process type " });
            }
            if (BatchTransfer.amountdr != BatchTransfer.TotalCredit)
            {
                return Json(new { message = "Sum of credit not equal to sum of debit " });
            }
            var logUser = username;

            var GeneralRef = "";
            var user = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
            var branch = user.BranchId;
            var acct = "";
            foreach (var temp in batchData)
            {
               
                //result = setupUnitOfWork.Transaction.InsertBulkTransferUpload(temp.BatchRef, logUser, branch, user.CompanyId, false, temp.StampDuty, 1, casa, BatchTransfer.amountdr, BatchTransfer.BatchName, BatchTransfer.Narration, Convert.ToDateTime(BatchTransfer.Transactiondate), BatchTransfer.type);
                // var UploadTemp = setupUnitOfWork.BatchUpload.GetAll().Where(o=>o.BatchRef==temp.BatchRef);
                TblBankingFundTransferUpload transferUpload = new TblBankingFundTransferUpload
                {
                    AccountNo = temp.AccountNo,
                    AccountNoDr = casa,
                    Amount = temp.Amount,
                    Approved = false,
                    ApprovedBy = string.Empty,
                    DateApproved = DateTime.Now,
                    IsAmend = false,
                    InEntryState = false,
                    IsCancel = false,
                    OperationId = Convert.ToInt32(PostType.BULKTRANSFER),
                    BatchName = BatchTransfer.BatchName,
                    BrCode = branch,
                    CreatedBy = logUser,
                    ChargeStamp = BatchTransfer.StampDuty,
                    ChequeNo = BatchTransfer.ChequeNo,
                    CoyCode = user.CompanyId,
                    DateCreated = DateTime.Now,
                    Disapproved = false,
                    ValueDate = Convert.ToDateTime(BatchTransfer.Transactiondate),
                    Narration = BatchTransfer.Narration,
                    TransCode = temp.BatchRef,
                    ProcessTypeId = BatchTransfer.type,
                    OperationType = 0,
                    TransactionType = 2,
                    Reference = temp.BatchRef,
                    Remark = "UPLOADED",
                    TransferChargeType = 1,
                    

                };
                GeneralRef = temp.BatchRef;
                acct = temp.AccountNo;
                db.TblBankingFundTransferUpload.Add(transferUpload);
                result = db.SaveChanges();
                var transId = db.TblBankingFundTransferUpload.ToList().Where(o => o.AccountNo == acct).FirstOrDefault();
                var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                TblApprovalTrack track = new TblApprovalTrack();
                track.ALevel = 1;
                track.Brcode = user.BranchId.ToString();
                track.Coycode = user.CompanyId.ToString();
                track.OperationDate = DateTime.Now;
                track.OperationId = Convert.ToInt32(PostType.BULKTRANSFER);
                track.OperationName = "BULKTRANSFER";
                track.Staffid = user.StaffNo;
                track.Username = logUser;
                track.TransactionId = Convert.ToInt32(transId.Id);
                track.MaxAmount = approval.MaxAmt;
                track.MinAmount = approval.MinAmt;
                track.ALevel = approval.ApprovingLevel;
                setupUnitOfWork.ApprovalTrack.Add(track);

                setupUnitOfWork.Commit();
                var UploadTemp = setupUnitOfWork.BatchUpload.GetAll().Where(o => o.AccountNo == temp.AccountNo).FirstOrDefault();
                setupUnitOfWork.BatchUpload.Delete(UploadTemp);
                setupUnitOfWork.Commit();
            }
            if (result == 1)
            {
                string comment = string.Empty;
                var request = HttpContext.Request;
                var IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
                // The URL that was accessed
                var AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
                // Creates Timestamp
                var TimeStamp = DateTime.Now;
                TblBankingAuditTrail tblBankingAudit = new TblBankingAuditTrail();
                tblBankingAudit.Ipaddress = IPAddress;
                tblBankingAudit.TransDate = TimeStamp;
                tblBankingAudit.UserName = logUser;
                tblBankingAudit.BrCode = user.BranchId.ToString();
                tblBankingAudit.CoyCode = user.CompanyId.ToString();
                tblBankingAudit.TransType = "BULKTRANSFER";
                tblBankingAudit.TransTime = TimeStamp.ToShortTimeString();
                tblBankingAudit.Status = true;
                tblBankingAudit.CmpName = "FINTRAK";
                db.TblBankingAuditTrail.Add(tblBankingAudit);
                db.SaveChanges();

                    
                   
                return Json(new { message = " " });

            }
            else
            {
                return Json(new { message = "The transaction failed to process." });
            }
        }
        public JsonResult listupload()
        {
            var result = setupUnitOfWork.GeneralUploadTemp.GetAll().ToList();
            return Json(result);
        }
        public JsonResult listbatchupload()
        {
            var result = setupUnitOfWork.BatchUpload.GetAll().ToList();
            return Json(result);
        }
        public IActionResult GeneralUpload()
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            return View();
        }
        public JsonResult GetCustomerDetailsFromCASA(string AccountNumber)
        {
            var customer = new TblCasa();
            var db = new TheCoreBankingContext();
            customer = (from a in db.TblCasa
                        join b in db.TblCustomer on a.Customerid equals b.Customerid
                        where (a.Accountnumber == AccountNumber)
                        select a).FirstOrDefault();
            return Json(customer);
        }
        public JsonResult AccountList()
        {
            var result = setupUnitOfWork.SingleFund.GetAll().OrderByDescending(o => o.PostDate).ToList();
            return Json(result);
        }
        public JsonResult listmultiple()
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;

            var result = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Approved == false && o.CreatedBy == logUser).OrderByDescending(o => o.Id).ToList();
            return Json(result);
        }
        public JsonResult listmultipleTemp()
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;

            var result = setupUnitOfWork.multipleFundTemp.GetAll().Where(o => o.CreatedBy == logUser).OrderByDescending(o => o.Id).ToList();
            return Json(result);
        }
        public JsonResult OperationTypeList()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.OperationType.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.OperationType
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadLedgerAccount()
        {


            var customerResult = CustomerAndGL().Where(o => o.isGL == 0).ToList();
            var glResult = CustomerAndGL().Where(o => o.isGL == 1).ToList();


            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();


            foreach (var item in glResult)
            {
                decimal balance = loadGLBalance(item.Accountnumber);
                SelectContent load = new SelectContent()
                {
                    id = item.AccountName.ToString(),
                    text = item.AccountName.ToString() + " : " + item.Accountnumber,
                    amount = item.Accountnumber,
                    availablebalance = balance.ToString(),
                    operationid = item.OPERATIONID.ToString(),


                };

                list.Add(load);
            }

            loadFormat.results = list;
            return Json(loadFormat);

        }

        public JsonResult PostTypeList()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.PostType.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Posting
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCustomerAccount(int ID)
        {

            var reply = setupUnitOfWork.Accounts.GetAll().Where(o => o.Operationid == 1).Where(o => o.Accountstatusid == 1).ToList();
            var customerResult = CustomerAndGL().Where(o => o.isGL == 0).ToList();
            var glResult = CustomerAndGL().Where(o => o.isGL == 1).ToList();
            var chart = setupUnitOfWork.Chart.GetAll().Select(o => new { o.AccountName });

            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();
            if (ID == 2 || ID == 1)
            {
                if (reply.Count() == 0)
                {
                    return Json(new { message = "Operation cannot be found,try another operation" });
                }
                foreach (var item in customerResult)
                {
                    decimal balance = loadCustomerBalance(item.Accountnumber);
                    SelectContent load = new SelectContent()
                    {
                        id = item.AccountName.ToString(),
                        text = item.AccountName.ToString() + " : " + item.Accountnumber,
                        amount = item.Accountnumber,
                        availablebalance = balance.ToString(),
                        operationid = item.OPERATIONID.ToString(),
                        approvalstatusid = reply.FirstOrDefault().Approvalstatusid.ToString(),

                    };

                    list.Add(load);

                }
            }
            else
            {
                foreach (var item in glResult)
                {
                    decimal balance = loadGLBalance(item.Accountnumber);
                    SelectContent load = new SelectContent()
                    {
                        id = item.AccountName.ToString(),
                        text = item.AccountName.ToString() + " : " + item.Accountnumber,
                        amount = item.Accountnumber,
                        availablebalance = balance.ToString(),
                        operationid = item.OPERATIONID.ToString(),


                    };

                    list.Add(load);
                }
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadProduct(string id)
        {

            var customerResult = CustomerAndGL().Where(o => o.AccountName == id && o.isGL == 0).ToList();
            var glResult = CustomerAndGL().Where(o => o.AccountName == id && o.isGL == 1).ToList();
            var chart = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountName == id).FirstOrDefault();


            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();
            if (customerResult.Count() != 0)
            {
                var account = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountname == id).FirstOrDefault();

                var customerReply = setupUnitOfWork.Product.GetAll().Where(o => o.Id == account.Productid).ToList();
                foreach (var item in customerReply)
                {
                    SelectContent load = new SelectContent()
                    {
                        id = item.Id.ToString(),
                        text = item.Productname,


                    };

                    list.Add(load);
                }
                loadFormat.results = list;

            }
            else
            {
                var customerReply = setupUnitOfWork.Product.GetAll().Where(o => o.Id.ToString() == chart.ProductId).ToList();
                foreach (var item in customerReply)
                {
                    SelectContent load = new SelectContent()
                    {
                        id = item.Id.ToString(),
                        text = item.Productname.ToString(),

                    };

                    list.Add(load);
                }
                loadFormat.results = list;

            }

            return Json(loadFormat);
        }
        public JsonResult loadTransactionType(string id)
        {

            var account = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountname == id).FirstOrDefault();

            var reply = setupUnitOfWork.Batch.GetAll();

            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in reply)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.OperationName


                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCustomerAccountCr(int ID, string acctName)
        {

            var reply = setupUnitOfWork.Accounts.GetAll().Where(o => o.Operationid == ID && o.Accountname != acctName).ToList();
            //var customer = CustomerAndGL().Where(o => o.AccountName != acctName).ToList();
            var chart = setupUnitOfWork.Chart.GetAll().Select(o => new { o.AccountName });

            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();
            if (ID == 1 || ID == 4)
            {
                var customer = CustomerAndGL().Where(o => o.AccountName != acctName && o.isGL == 0).ToList();
                foreach (var item in customer)
                {
                    decimal balance = loadCustomerBalance(item.Accountnumber);
                    SelectContent load = new SelectContent()
                    {
                        id = item.AccountName.ToString(),
                        text = item.AccountName.ToString() + " : " + item.Accountnumber,
                        amount = item.Accountnumber,
                        availablebalance = balance.ToString(),
                        operationid = item.OPERATIONID.ToString(),
                        //approvalstatusid = item.Approvalstatusid.ToString(),

                    };

                    list.Add(load);
                }
            }
            else
            {
                var customer = CustomerAndGL().Where(o => o.AccountName != acctName && o.isGL == 1).ToList();
                foreach (var item in customer)
                {
                    decimal balance = loadGLBalance(item.Accountnumber);
                    SelectContent load = new SelectContent()
                    {
                        id = item.AccountName.ToString(),
                        text = item.AccountName.ToString() + " : " + item.Accountnumber,
                        amount = item.Accountnumber,
                        availablebalance = balance.ToString(),
                        operationid = item.OPERATIONID.ToString(),
                        //approvalstatusid = item.Approvalstatusid.ToString(),

                    };

                    list.Add(load);
                }
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadProductCr(string id)
        {
            try
            {
                var account = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountname == id).FirstOrDefault();

                var reply = setupUnitOfWork.Product.GetAll().Where(o => o.Id == account.Productid);

                Select2Format loadFormat = new Select2Format();

                List<SelectContent> list = new List<SelectContent>();
                foreach (var item in reply)
                {
                    SelectContent load = new SelectContent()
                    {
                        id = item.Id.ToString(),
                        text = item.Productname


                    };

                    list.Add(load);
                }
                loadFormat.results = list;
                return Json(loadFormat);
            }
            catch (Exception ex)
            {
                var error = ex.Message == null ? ex.InnerException.Message : ex.Message;
                return Json(error);
            }


        }
        public JsonResult loadBank()
        {

            var account = setupUnitOfWork.Banks.GetAll();
            Select2Format loadFormat = new Select2Format();
            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in account)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.BankId.ToString(),
                    text = item.BankName

                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult ChargeTypeList()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.ChargeType.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.ChargeType
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCustAccount()
        {

            var replys = setupUnitOfWork.Transaction.GetAll();
            var reply = setupUnitOfWork.Chart.GetAll();


            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in reply)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.AccountName.ToString(),
                    text = item.AccountName.ToString() + " : " + item.AccountId,
                    amount = item.AccountId,

                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        //Batch Ref
        public JsonResult loadBatchRef()
        {
            TheCoreBankingContext context = new TheCoreBankingContext();
            Select2Format loadFormat = new Select2Format();
            var reply = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Disapproved == false && o.Approved == true && o.InEntryState == false);
            var result = (from multiple in setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Approved == true).AsEnumerable()
                          group multiple by multiple.Reference into ReferenceMultiple
                          select ReferenceMultiple.OrderByDescending(o => o.Id).AsEnumerable().First()
                          );


            //var result = approvalUnitOfWork.MultipleFund.GetAll().Where(o => o.Disapproved == false && o.Approved == false && o.InEntryState == false).ToList();

            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Reference

                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult listFundMultipFund(int Id)
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;

            var reply = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Id == Id).FirstOrDefault().Reference;
            var result = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == reply && o.CreatedBy == logUser).ToList();
            return Json(result);
        }
        public JsonResult loadTransactionsType()
        {

            var reply = setupUnitOfWork.Batch.GetAll();

            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in reply)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.OperationName


                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCustAccountCr()
        {

            var reply = setupUnitOfWork.Accounts.GetAll();

            Select2Format loadFormat = new Select2Format();
            List<SelectContent> list = new List<SelectContent>();
            foreach (var item in reply)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Accountname.ToString(),
                    text = item.Accountname.ToString() + " : " + item.Accountnumber,
                    amount = item.Accountnumber,
                    availablebalance = item.Availablebalance.ToString(),
                    operationid = item.Productid.ToString(),
                    approvalstatusid = item.Approvalstatusid.ToString(),

                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public decimal loadGLBalance(string AccountID)
        {

            try
            {
                var chart = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountName == AccountID).OrderByDescending(o => o.DateCreated).FirstOrDefault();
                //decimal balance = setupUnitOfWork.Transaction.SPGLBalance(chart.AccountId);
                decimal? Balance = 0;
                // var reply = setupUnitOfWork.Transaction.GetAll().Where(o => o.AccountId == chart.AccountId).OrderByDescending(o => o.Id).FirstOrDefault();
                var CategoryId = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == AccountID).FirstOrDefault().AccountCategoryId;
                if (CategoryId >= 1 && CategoryId <= 5)
                {
                    Balance = setupUnitOfWork.Transaction.GetAll().Where(o => o.AccountId == AccountID && o.Approved == true).Sum(o => o.DebitAmt - o.CreditAmt);
                }
                else
                {
                    Balance = setupUnitOfWork.Transaction.GetAll().Where(o => o.AccountId == AccountID && o.Approved == true).Sum(o => o.CreditAmt - o.DebitAmt);
                }
                return Convert.ToDecimal(Balance);
            }
            catch (Exception ex)
            {

                return 0;
            }

          

        }
        #region API
        public string GetNewTransferRef()
        {
            try
            {
                string refID = Guid.NewGuid().ToString().ToUpper().Substring(0, 7);
                string prefix = "TRANS/FTB/";
                // 01 - IntraBank Transfer ------ NIP - Interbank Transfer
                string suffix = "/" + System.DateTime.Now.Day.ToString() + "" + System.DateTime.Now.Hour.ToString() + "" +
                                System.DateTime.Now.Minute.ToString() + "" + System.DateTime.Now.Second.ToString();
                string _ref = string.Concat(prefix, refID);

                return string.Concat(_ref, suffix);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        //GL Balance
        public decimal GetGLBalance(string AccountID)
        {
            try
            {
                var chart = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountName == AccountID).OrderByDescending(o => o.DateCreated).FirstOrDefault();
                //decimal balance = setupUnitOfWork.Transaction.SPGLBalance(chart.AccountId);
                decimal? Balance = 0;
               // var reply = setupUnitOfWork.Transaction.GetAll().Where(o => o.AccountId == chart.AccountId).OrderByDescending(o => o.Id).FirstOrDefault();
                var CategoryId = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountName == AccountID).FirstOrDefault().AccountCategoryId;
                if(CategoryId >=1 && CategoryId <= 5)
                {
                    Balance = setupUnitOfWork.Transaction.GetAll().Where(o => o.AccountId == chart.AccountId && o.Approved == true).Sum(o => o.DebitAmt - o.CreditAmt);
                }
                else
                {
                    Balance = setupUnitOfWork.Transaction.GetAll().Where(o => o.AccountId == chart.AccountId && o.Approved == true).Sum(o => o.CreditAmt - o.DebitAmt);
                }
                return Convert.ToDecimal(Balance);
            }
            catch (Exception ex)
            {

                return 0;
            }


        }
        //Customer Balance
        public decimal loadCustomerBalance(string AccountID)
        {

            //AccountID = "00897878721";
            var casa = setupUnitOfWork.Accounts.GetAll().Where(o => o.Accountnumber == AccountID).FirstOrDefault();
            //decimal balance = setupUnitOfWork.Transaction.CustomerBalance(AccountID);

            var AppDate = db.TblFinanceCurrentDate.FirstOrDefault().CurrentDate;
            decimal? ODAmount = db.TblCasa.Where(o => o.Accountnumber == AccountID).FirstOrDefault().Overdraftamount ?? 0;            
            decimal? EndBalance = setupUnitOfWork.counterparty.GetAll().Where(o => o.Ref == AccountID && o.Approved == true && o.IsReversed == false).Sum(o => o.CreditAmount - o.DebitAmount) ?? 0 + ODAmount;
            var BatchPendingDebit = db.TblBankingFundTransferUpload;
            decimal MultiplePendingDebit = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Approved == false && o.Disapproved == false && o.IsCancel == false && o.IsAmend == false && o.AccountNo == AccountID && o.TransactionType != 2).Sum(o => o.Amount) ?? 0;
            decimal SinglePendingDebit= setupUnitOfWork.SingleFund.GetAll().Where(o => o.Approved == false && o.Disapproved == false && o.IsCancel == false && o.IsAmend == false && o.AccountDr == AccountID && o.TransactionType != 2).Sum(o => o.Amount);
            decimal? Deposit = db.TblBankingCawithdrawal.Where(o => o.Approved == false && o.Disapproved == false && o.IsReversed == false && o.ProductAcctNo == AccountID).Sum(o => o.AmtWithdraw) ?? 0;
            decimal? MMarketPendingDr = db.TblInwardbankcheque.Where(o => o.Approved == false && o.Casaaccountno == AccountID).Sum(o => o.Amount + o.Chargeamount);
            decimal? TotalPendingDebit = MultiplePendingDebit + SinglePendingDebit + Deposit + MMarketPendingDr;
            decimal? ActualBalance = EndBalance - TotalPendingDebit;
            return Convert.ToDecimal(ActualBalance);

        }
        //Transaction Exist
        private bool isTransactionExisting(string BatchRef)
        {
            try
            {
                TblFinanceTransaction transactions = new TblFinanceTransaction();

                transactions = db.TblFinanceTransaction.Where(p => p.BatchRef == BatchRef).FirstOrDefault();

                if (transactions != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DateTime GetCurrentSystemDate()
        {
            try
            {
                DateTime currentDate = new DateTime();
                using (TheCoreBankingContext dbContext = new TheCoreBankingContext())
                {
                    currentDate = (from a in dbContext.TblFinanceCurrentDate select a.CurrentDate).FirstOrDefault();
                }
                return currentDate;
            }
            catch (Exception)
            {

                return new DateTime();
            }
        }

        //Post Transaction
        public string PostTransactions(PostingDTO listPosting)
        {
            string systemDate = GetCurrentSystemDate().ToString();
            var lstPostingObject = new List<PostingDTO>();
            var oPostingDTo = new PostingDTO
            {
                Amount = listPosting.Amount,
                AppID = "FINTRAK BANKING",
                ApprovedBy = listPosting.ApprovedBy,
                BatchRef = listPosting.BatchRef,
                BrCode = "001",
                CrAccount = listPosting.CrAccount,
                CrNarration = listPosting.CrNarration,
                DrAccount = listPosting.DrAccount,
                DrNarration = listPosting.DrNarration,
                MISCode = "0",
                PostType = 1,
                PostedBy = "FINTRAK BANKING",
                TransactionType = 53,
                ValueDate = DateTime.Parse(systemDate)
            };
            lstPostingObject.Add(oPostingDTo);

            try
            {
                if (lstPostingObject.Count() == 0)
                {
                    return "Object is empty";
                }
                string currentBatchRef = "";
                int counter = 0;
                List<PostingDTO> lstNewTransaction = new List<PostingDTO>();
                TheCoreBankingContext db = new TheCoreBankingContext();
                #region Validation Region
                foreach (var rec in lstPostingObject) // For Validation
                {
                    if (rec == null)
                    {
                        continue;
                    }
                    counter = counter + 1;

                    if (counter == 1)
                    {
                        currentBatchRef = rec.BatchRef;
                        if (string.IsNullOrEmpty(rec.BatchRef) == true)
                        {
                            return "Batch reference required";
                        }
                        bool BatchExist;
                        BatchExist = isTransactionExisting(currentBatchRef);
                        if (BatchExist)
                        {
                            return "Transaction already exist";
                        }
                    }
                    if (rec.PostType == 0)
                    {
                        return "Post type required - " + counter;
                    }
                    if (string.IsNullOrEmpty(rec.DrAccount) == true)
                    {
                        return "Account Number to credit is required! Check record number - " + counter;
                    }
                    if (string.IsNullOrEmpty(rec.CrAccount) == true)
                    {
                        return "Account Number to credit is required! Check record number - " + counter;
                    }

                    if (string.IsNullOrEmpty(rec.DrNarration) == true && string.IsNullOrEmpty(rec.CrNarration) == true)
                    {
                        return "Enter at least one narration! Check record number - " + counter;
                    }
                    DateTime currentDate = GetCurrentSystemDate();

                    if (rec.ValueDate == null)
                    {
                        rec.ValueDate = currentDate;
                    }
                    else
                    {
                        if (rec.ValueDate > currentDate)
                        {
                            return "Value date cannot be greater than the application date. Check record number - " + counter;
                        }
                    }
                    if (string.IsNullOrEmpty(rec.PostedBy) == true)
                    {
                        return "Posted by is required. Check record number - " + counter;
                    }
                    if (string.IsNullOrEmpty(rec.ApprovedBy) == true)
                    {
                        return " Approved by is required. Check record number - " + counter;
                    }
                    if (rec.TransactionType == 0)
                    {
                        return "Transaction type required. Check record number - " + counter;
                    }

                    if (string.IsNullOrEmpty(rec.AppID) == true)
                    {
                        return "App ID required! Check record number - " + counter;
                    }
                    if (string.IsNullOrEmpty(rec.BrCode) == true)
                    {
                        return "Branch code is required - " + counter;
                    }
                    TblCasa Acct1 = new TblCasa();
                    TblCasa Acct2 = new TblCasa();
                    TblFinanceChartOfAccount chart1 = new TblFinanceChartOfAccount();
                    TblFinanceChartOfAccount chart2 = new TblFinanceChartOfAccount();
                    Acct1 = GetCustomerAcctDetails(rec.DrAccount);
                    if (Acct1 == null)
                    {
                        chart1 = GetChartOfAccountDetails(rec.DrAccount);
                        if (chart1 == null)
                        {
                            return "Credit account number does not exist. Check record number - " + counter;
                        }
                    }
                    Acct2 = GetCustomerAcctDetails(rec.CrAccount);
                    if (Acct2 == null)
                    {
                        chart2 = GetChartOfAccountDetails(rec.CrAccount);
                        if (chart2 == null)
                        {
                            return "Debit account number does not exist. Check record number - " + counter;
                        }
                    }
                    if (ValidatePostType(rec.DrAccount, rec.CrAccount, rec.PostType))
                    {
                        lstNewTransaction.Add(rec);
                    }
                    else
                    {
                        return "Wrong post type selected.";
                    }


                }
                #endregion

                string status = "";
                #region Postings Region
                if (lstNewTransaction.Count() != 0)
                {
                    foreach (var rec in lstNewTransaction)
                    {
                        string transSequence = "";
                        #region Customer to customer Posting
                        if (rec.PostType == 1)//Customer to customer Posting
                        {
                            transSequence = new Random().Next().ToString().Substring(0, 7);
                            //string _Status = string.Empty;
                            bool _Status = false;
                            string retResult;
                            bool result = setupUnitOfWork.Transaction.PostTransaction(rec.CrAccount, rec.DrAccount, rec.BrCode, GetCompanyCode(), rec.MISCode, rec.Amount, rec.TransactionType,
                                rec.CrNarration, rec.DrNarration, rec.PostedBy, rec.AppID, rec.ValueDate, rec.BatchRef, transSequence, _Status);
                            if (result == true)
                            {
                                status = "Successful";
                            }
                            if (result == false)
                            {
                                status = "Error Occured";
                            }

                        }
                        #endregion

                        #region GL to GL Posting
                        if (rec.PostType == 2)//GL to GL Posting
                        {
                            transSequence = new Random().Next().ToString().Substring(0, 7);
                            string retResult;
                            bool _Status = false;
                            bool result = setupUnitOfWork.Transaction.PostTransactions(rec.CrAccount, rec.DrAccount, rec.BrCode, GetCompanyCode(), rec.MISCode, rec.Amount, rec.TransactionType,
                                rec.CrNarration, rec.DrNarration, rec.PostedBy, rec.AppID, rec.ValueDate, rec.BatchRef, transSequence, _Status);
                            //return retResult;
                            if (result == true)
                            {
                                status = "Successful";
                            }
                            if (result == false)
                            {
                                status = "Error Occured";
                            }
                        }

                        #endregion

                    }
                }
                #endregion
                return status;
            }
            catch (Exception ex)
            {

                return "UNKNOWN ERROR OCCURRED - " + ex.Message.ToString();
            }
        }
        private string GetCompanyCode()
        {
            try
            {
                string result;
                TblCompanyInformation company = new TblCompanyInformation();
                using (TheCoreBankingContext db = new TheCoreBankingContext())
                {
                    company = (from a in db.TblCompanyInformation select a).FirstOrDefault();
                }
                if (company != null)
                {
                    result = company.CoyId;
                }
                else
                { result = "Record not found"; }
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        private bool ValidatePostType(string DrAccountNumber, string CrAccountNumber, int PostType)
        {
            try
            {
                bool reply = false;
                if (PostType == 1)
                {
                    TblCasa DrAcct = new TblCasa();
                    TblCasa CrAcct = new TblCasa();
                    DrAcct = GetCustomerAcctDetails(DrAccountNumber);
                    DrAcct = GetCustomerAcctDetails(CrAccountNumber);
                    if (DrAcct != null && CrAcct != null)
                    {
                        reply = true;
                    }
                    else
                    {
                        reply = false;
                    }
                }
                if (PostType == 2)
                {
                    TblCasa Acct = new TblCasa();
                    TblFinanceChartOfAccount chart = new TblFinanceChartOfAccount();
                    Acct = GetCustomerAcctDetails(DrAccountNumber);
                    chart = GetChartOfAccountDetails(CrAccountNumber);
                    if (Acct != null && chart != null)
                    {
                        reply = true;
                    }
                    else
                    {
                        Acct = GetCustomerAcctDetails(CrAccountNumber);
                        chart = GetChartOfAccountDetails(DrAccountNumber);
                        if (Acct != null && chart != null)
                        {
                            reply = true;
                        }
                        else
                        {
                            reply = false;
                        }
                    }
                }
                if (PostType == 3)
                {
                    TblFinanceChartOfAccount chart1 = new TblFinanceChartOfAccount();
                    TblFinanceChartOfAccount chart2 = new TblFinanceChartOfAccount();
                    chart1 = GetChartOfAccountDetails(CrAccountNumber);
                    chart2 = GetChartOfAccountDetails(DrAccountNumber);
                    if (chart1 != null && chart2 != null)
                    {
                        reply = true;
                    }
                    else
                    {
                        reply = false;
                    }
                }
                return reply;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private TblCasa GetCustomerAcctDetails(string AccountNumber)
        {
            try
            {
                //string result;
                TblCasa customer = new TblCasa();
                using (TheCoreBankingContext db = new TheCoreBankingContext())
                {
                    customer = db.TblCasa.Where(p => p.Accountnumber == AccountNumber).FirstOrDefault();
                }

                return customer;
            }
            catch (Exception ex)
            {
                return new TblCasa();
            }
        }
        private TblFinanceChartOfAccount GetChartOfAccountDetails(string AccountNumber)
        {
            try
            {
                //string result;
                TblFinanceChartOfAccount customer = new TblFinanceChartOfAccount();
                using (TheCoreBankingContext db = new TheCoreBankingContext())
                {
                    customer = db.TblFinanceChartOfAccount.Where(p => p.AccountId == AccountNumber).FirstOrDefault();
                }

                return customer;
            }
            catch (Exception ex)
            {
                return new TblFinanceChartOfAccount();
            }
        }
        #endregion
        #region Select2 Helper

        public class Select2Format
        {
            public List<SelectContent> results { get; set; }
        }
        public class SelectContent
        {
            public string id { get; set; }
            public string text { get; set; }
            public string amount { get; set; }
            public string availablebalance { get; set; }
            public string operationid { get; set; }
            public string approvalstatusid { get; set; }



        }
        #endregion

        //public decimal FetchBalance(string accountNumber)
        //{
        //    decimal balance = 0;
        //    var balanceTask = Task.Run(() =>
        //    {
        //        balance = setupUnitOfWork.Accounts.Get(o =>Convert.ToBoolean(o.Availablebalance)); //connectBank.GetCustomerBalance(accountNumber);
        //        return balance;
        //    });

        //    balance = balanceTask.Result;
        //    return balance;
        //}

        public string GenerateReference()
        {

            string bankcode = "070012";

            DateTime date = DateTime.Now;

            string year = date.Year.ToString().Substring(2, 2);

            string month = date.Month.ToString();

            int _month = int.Parse(month);
            if (_month < 10 && month.Length == 1)
            {
                month = "0" + _month.ToString();
            }

            string day = date.Day.ToString();

            int _day = int.Parse(day);
            if (_day < 10 && day.Length == 1)
            {
                day = "0" + _day.ToString();
            }

            string hour = date.Hour.ToString();

            int _hour = int.Parse(hour);
            if (_hour < 10 && hour.Length == 1)
            {
                hour = "0" + _hour.ToString();
            }

            string min = date.Minute.ToString();

            int _min = int.Parse(min);
            if (_min < 10 && min.Length == 1)
            {
                min = "0" + _min.ToString();
            }

            string sec = date.Second.ToString();

            int _sec = int.Parse(sec);
            if (_sec < 10 && sec.Length == 1)
            {
                sec = "0" + _sec.ToString();
            }

            string sessionID = bankcode + year + month + day + hour + min + sec;


            string part1 = rand.Next(1234, 3241).ToString();
            //string part2 = rand.Next(3242, 4232).ToString();
            //string part3 = rand.Next(4233, 9535).ToString();

            string figures = part1;// + part2 + part3;
            return sessionID + figures;
        }
        public decimal PendingTransaction(string AccountNo)
        {
            decimal Amount = 0;
            IEnumerable<TblBankingCottransaction> COT = setupUnitOfWork.maintenanceFee.GetAll().Where(o => o.ProductAcctNo == AccountNo && o.IsCharged == false).ToList();
            if (COT != null)
            {
                foreach (TblBankingCottransaction transaction in COT)
                {
                    Amount = Amount + transaction.Cotamount;
                }

            }
            string Result = String.Empty;
            return Amount;
        }

        #region Download template
        [HttpPost]
        public JsonResult downloadnow()
        {

            return Json("");
        }
        [HttpPost]
        public async Task<FileResult> Download()
        {
            string fileName;
            fileName = "MultipleUpload.xlsx";


            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "wwwroot\\", fileName);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, MediaTypeNames.Application.Octet, Path.GetFileName(path));
        }
        public async Task<IActionResult> Downloads(string filename)
        {
            filename = "MultipleUpload.xlsx";
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                 {".csv", "text/csv"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                //{".pdf", "application/pdf"},
                //{".xls", "application/vnd.ms-excel"},
                //{".xlsx", "application/vnd.openxmlformats
                //           officedocument.spreadsheetml.sheet"},
                //{".png", "image/png"},
                //{".jpg", "image/jpeg"},
                //{".jpeg", "image/jpeg"},
                //{".gif", "image/gif"},
               
            };
        }

        #endregion
    }
}