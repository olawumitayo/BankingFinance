using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheCorebanking.Finance.Data.Models;

using TheCorebanking.Finance.Models;
using TheCorebanking.Finance.Services;
using TheCoreBanking.Finance.Data.Contracts;

namespace TheCorebanking.Finance.Controllers
{
   // [Authorize]
    public class ReversalController : Controller
    {
        private ILogger<ReversalController> logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        TheCoreBankingContext db = new TheCoreBankingContext();
        public ReversalController(ISetupUnitOfWork uowSetup, IHostingEnvironment hostingEnvironment, ILogger<ReversalController> financeLogger)

        {
            setupUnitOfWork = uowSetup;
            _hostingEnvironment = hostingEnvironment;
            logger = financeLogger;
        }

        public ISetupUnitOfWork setupUnitOfWork { get; set; }
        //[Authorize(Roles = "finance")]
        //[AllowAnonymous]
        public IActionResult Index()
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

        public string GetLoggedUser()
        {
            var logUser = User.Identity.Name??"tayo.olawumi";
            if (logUser == null)
            {
                logUser = "tayo.olawumi";
            }
            else
            {
                logUser = User.Identity.Name??"tayo.olawumi";
            }
            return logUser;
        }
        public IActionResult Customer()
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
        public IActionResult BulkUpload()
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
        public IActionResult GeneralLedger()
        {
            var teller = setupUnitOfWork.TillLimit.GetAll().ToList();

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
        public JsonResult listmultiple(int ID,string TranDate)
        {
            string idnow = Convert.ToString(ID);
            if (ID != 0)
            {
                var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Id == ID).FirstOrDefault();
                var replys = setupUnitOfWork.Transaction.spMultipleSingle(TranDate, teller.Tilluser).ToList();
                return Json(replys);
            }
            return Json("");
        }
        public JsonResult listSingle(int ID, string TranDate)
        {
            string idnow = Convert.ToString(ID);
            if (ID != 0)
            {
                var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Id == ID).FirstOrDefault();
                var reply = setupUnitOfWork.Transaction.spSingle(TranDate, teller.Tilluser).ToList();
                return Json(reply);
            }
            return Json("");
        }
        public JsonResult listmultipleGL(int ID, string TranDate)
        {
            string idnow = Convert.ToString(ID);
            if (ID != 0)
            {
                var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Id == ID).FirstOrDefault();
                var replys = setupUnitOfWork.Transaction.spMultipleSingleGL(TranDate, teller.Tilluser).ToList();
                return Json(replys);
            }
            return Json("");
        }
        public JsonResult listBulkCustomer(string ID, string TranDate)
        {
            string idnow = Convert.ToString(ID);
            var dateAll = Convert.ToDateTime(TranDate);
            if (string.IsNullOrEmpty(ID) ==false)
            {
                var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Tilluser == ID).FirstOrDefault();
                var reply = (from tbl_FinanceCounterpartyTransaction in db.TblFinanceCounterpartyTransaction
                             join TBL_CASA in db.TblCasa on new { Ref = (string)tbl_FinanceCounterpartyTransaction.Ref } equals new { Ref = TBL_CASA.Accountnumber }
                             where
                               tbl_FinanceCounterpartyTransaction.Approved == true &&
                               tbl_FinanceCounterpartyTransaction.IsReversed == false &&
                               tbl_FinanceCounterpartyTransaction.Show == true &&
                               tbl_FinanceCounterpartyTransaction.UserName == teller.Tilluser &&
                               tbl_FinanceCounterpartyTransaction.PostDate == dateAll
                             orderby
                               tbl_FinanceCounterpartyTransaction.TransactionId descending
                             select new
                             {
                                 tbl_FinanceCounterpartyTransaction.UserName,
                                 tbl_FinanceCounterpartyTransaction.TransactionId,
                                 tbl_FinanceCounterpartyTransaction.TransactionDate,
                                 tbl_FinanceCounterpartyTransaction.Ref,
                                 tbl_FinanceCounterpartyTransaction.Description,
                                 tbl_FinanceCounterpartyTransaction.DebitAmount,
                                 tbl_FinanceCounterpartyTransaction.CreditAmount,
                                 tbl_FinanceCounterpartyTransaction.FormNo,
                                 Amount =
                               tbl_FinanceCounterpartyTransaction.DebitAmount == 0 ? tbl_FinanceCounterpartyTransaction.CreditAmount : tbl_FinanceCounterpartyTransaction.DebitAmount,
                                 tbl_FinanceCounterpartyTransaction.BatchRef,
                                 tbl_FinanceCounterpartyTransaction.PostDate,
                                 tbl_FinanceCounterpartyTransaction.Approved,
                                 tbl_FinanceCounterpartyTransaction.IsReversed,
                                 tbl_FinanceCounterpartyTransaction.Show,
                                 TBL_CASA.Accountname
                             }).AsEnumerable().ToList();
                //var replys = setupUnitOfWork.Transaction.spBulkList(TranDate, teller.Tilluser).ToList();
                return Json(reply);
            }
            return Json("");
        }
        public JsonResult listBulkGL(string ID, string TranDate)
        {
            string idnow = Convert.ToString(ID);
            if (string.IsNullOrEmpty(ID) ==false)
            {
                var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Tilluser == ID).FirstOrDefault();
             
                var replys = (from tbl_FinanceTransaction in db.TblFinanceTransaction
                              join tbl_FinanceChartOfAccount in db.TblFinanceChartOfAccount on tbl_FinanceTransaction.AccountId equals tbl_FinanceChartOfAccount.AccountId
                              where
                                tbl_FinanceTransaction.Approved == true &&
                                tbl_FinanceTransaction.Deleted == false &&
                                tbl_FinanceTransaction.SCoyCode == teller.Companyid &&
                                tbl_FinanceTransaction.ValueDate == Convert.ToDateTime(TranDate) &&
                                (new int?[] { 1, 2 }).Contains(tbl_FinanceTransaction.TransactionType) 
                              orderby
                                tbl_FinanceTransaction.Ref
                              select new
                              {
                                  tbl_FinanceTransaction.Ref,
                                  tbl_FinanceTransaction.Id,
                                  tbl_FinanceTransaction.Description,
                                  tbl_FinanceTransaction.DebitAmt,
                                  tbl_FinanceTransaction.CreditAmt,
                                  tbl_FinanceTransaction.AccountId,
                                  tbl_FinanceTransaction.PostedBy,
                                  PostDate = tbl_FinanceTransaction.ValueDate,
                                  tbl_FinanceChartOfAccount.AccountName
                              }).Distinct();

                return Json(replys.ToList());
            }
            return Json("");
        }

        public JsonResult listSingleGL(int ID, string TranDate)
        {
            string idnow = Convert.ToString(ID);
            if (ID != 0)
            {
                var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Id == ID).FirstOrDefault();
                var reply = setupUnitOfWork.Transaction.spSingleGL(TranDate, teller.Tilluser).ToList();
                return Json(reply);
            }
           
            return Json("");
        }
        // [Authorize(Roles = "finance")]
        public JsonResult ReversalList()
        {
            var reply = setupUnitOfWork.Reversal.GetAll().Where(o => o.Approved == false).ToList();
         
            return Json(reply);
        }
       // [Authorize(Roles = "finance")]
       // [AllowAnonymous]
       [HttpPost]
       public IActionResult ReverseBulk(List<TblFinanceCounterpartyTransaction> transfers,string Comment,string TranDate)
        {
            var logUser = User.Identity.Name??"tayo.olawumi";
            if (transfers.Count==0)
            {
                return Json(new { message = "No record selected " });
            }
            int counter = 1;
  
            var listTransfer = transfers.FirstOrDefault().BatchRef;
            int counters = setupUnitOfWork.counterparty.GetAll().Where(o => o.BatchRef == listTransfer).Count();
           
                foreach (var transfer in transfers) // For Validation
                {

                    if (transfer == null)
                    {
                        continue;
                    }

               
                    var multipleTrans = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.BatchRef).Count();
                    var singleTrans = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.BatchRef).Count();
                    var batchTrans = db.TblBankingFundTransferUpload.Where(o => o.Reference == transfer.BatchRef).Count();
                    string coycode = string.Empty;
                    string brcode = string.Empty;
                    string CreatedBy = string.Empty;

                    if (multipleTrans > 0)
                    {
                        var multipleTran = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.BatchRef).FirstOrDefault();
                        coycode = multipleTran.CoyCode;
                        brcode = multipleTran.BrCode;
                        CreatedBy = multipleTran.CreatedBy;
                    }
                    if (singleTrans > 0)
                    {
                        var singleTran = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.BatchRef).FirstOrDefault();
                        coycode = singleTran.CoyCode;
                        brcode = singleTran.BrCode;
                        CreatedBy = singleTran.CreateBy;
                    }
                    if (batchTrans > 0)
                    {
                        var batchTran = db.TblBankingFundTransferUpload.Where(o => o.Reference == transfer.BatchRef).FirstOrDefault();
                        coycode = batchTran.CoyCode;
                        brcode = batchTran.BrCode;
                        CreatedBy = batchTran.CreatedBy;
                    }
                    decimal Amount = 0;
                    if (transfer.DebitAmount != 0)
                    {
                        Amount = Convert.ToDecimal(transfer.DebitAmount);
                    }
                    if (transfer.CreditAmount != 0)
                    {
                        Amount = Convert.ToDecimal(transfer.CreditAmount);
                    }

                    TblBankingReversal reversal = new TblBankingReversal
                    {
                        AccountNo = transfer.Ref,
                        AmountReversed = Amount,
                        BrCode = brcode,
                        CoyCode = coycode,
                        DateReversed = DateTime.Now,
                        OperationId = Convert.ToInt32(PostType.REVERSAL),
                        PostDate = Convert.ToDateTime(TranDate),
                        PostedBy = CreatedBy,
                        ReversedBy = logUser,
                        Reference = transfer.BatchRef,
                        TrasactionDate = Convert.ToDateTime(transfer.TransactionDate),
                        TransactionId = transfer.TransactionId,
                        Comment = Comment
                    };
                    setupUnitOfWork.Reversal.Add(reversal);
                    setupUnitOfWork.Commit();

                var transId = setupUnitOfWork.Reversal.GetAll().Where(o => o.TransactionId == transfer.TransactionId).FirstOrDefault();
                var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                var staffId = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                TblApprovalTrack track = new TblApprovalTrack();
                track.ALevel = 1;
                track.Brcode = brcode.ToString();
                track.Coycode = coycode.ToString();
                track.OperationDate = DateTime.Now;
                track.OperationId = Convert.ToInt32(PostType.REVERSAL);
                track.OperationName = "Reversal";
                track.Staffid = staffId.StaffNo;
                track.Username = logUser;
                track.TransactionId = Convert.ToInt32(transId.Id);
                track.MaxAmount = approval.MaxAmt;
                track.MinAmount = approval.MinAmt;
                track.ALevel = approval.ApprovingLevel;
                setupUnitOfWork.ApprovalTrack.Add(track);

                setupUnitOfWork.Commit();


            }

            return Json(new { message = " " });
        }

        [HttpPost]
        public IActionResult ReversalGL(List<sp_ListGL> transfers, string Comment, string TranDate)
        {
            var logUser = User.Identity.Name??"tayo.olawumi";
            if (transfers.Count == 0)
            {
                return Json(new { message = "No record selected " });
            }
            int counter = 1;
           
            var listTransfer = transfers.FirstOrDefault().Ref;
            int counters = setupUnitOfWork.Transaction.GetAll().Where(o => o.Ref == listTransfer).Count();
            var teller = setupUnitOfWork.TillLimit.GetAll().Where(o => o.Tilluser == logUser).FirstOrDefault();
            foreach (var transfer in transfers) // For Validation
            {

                if (transfer == null)
                {
                    continue;
                }


                var multipleTrans = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.Ref).Count();
                var singleTrans = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.Ref).Count();
                var batchTrans = db.TblBankingFundTransferUpload.Where(o => o.Reference == transfer.Ref).Count();
                string coycode = string.Empty;
                string brcode = string.Empty;
                string CreatedBy = string.Empty;

                if (multipleTrans > 0)
                {
                    var multipleTran = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.Ref).FirstOrDefault();
                    coycode = multipleTran.CoyCode;
                    brcode = multipleTran.BrCode;
                    CreatedBy = multipleTran.CreatedBy;
                }
                if (singleTrans > 0)
                {
                    var singleTran = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.Ref).FirstOrDefault();
                    coycode = singleTran.CoyCode;
                    brcode = singleTran.BrCode;
                    CreatedBy = singleTran.CreateBy;
                }
                if (batchTrans > 0)
                {
                    var batchTran = db.TblBankingFundTransferUpload.Where(o => o.Reference == transfer.Ref).FirstOrDefault();
                    coycode = batchTran.CoyCode;
                    brcode = batchTran.BrCode;
                    CreatedBy = batchTran.CreatedBy;
                }
                decimal Amount = 0;
                if (transfer.DebitAmt != 0)
                {
                    Amount = Convert.ToDecimal(transfer.DebitAmt);
                }
                if (transfer.CreditAmt != 0)
                {
                    Amount = Convert.ToDecimal(transfer.CreditAmt);
                }

                TblBankingReversal reversal = new TblBankingReversal
                {
                    AccountNo = transfer.AccountId,
                    AmountReversed = Amount,
                    BrCode = teller.Branchid,
                    CoyCode = teller.Companyid,
                    DateReversed = DateTime.Now,
                    OperationId = Convert.ToInt32(PostType.REVERSAL),
                    PostDate = Convert.ToDateTime(TranDate),
                    PostedBy = CreatedBy,
                    ReversedBy = logUser,
                    Reference = transfer.Ref,
                    TrasactionDate = Convert.ToDateTime(transfer.PostDate),
                    TransactionId = transfer.Id,
                    Comment = Comment
                };
                setupUnitOfWork.Reversal.Add(reversal);
                setupUnitOfWork.Commit();
                var transId = setupUnitOfWork.Reversal.GetAll().Where(o => o.TransactionId == transfer.Id).FirstOrDefault();
                var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                var staffId = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                TblApprovalTrack track = new TblApprovalTrack();
                track.ALevel = 1;
                track.Brcode = teller.Branchid.ToString();
                track.Coycode = teller.Companyid.ToString();
                track.OperationDate = DateTime.Now;
                track.OperationId = Convert.ToInt32(PostType.REVERSAL);
                track.OperationName = "Reversal";
                track.Staffid = staffId.StaffNo;
                track.Username = logUser;
                track.TransactionId = Convert.ToInt32(transId.Id);
                track.MaxAmount = approval.MaxAmt;
                track.MinAmount = approval.MinAmt;
                track.ALevel = approval.ApprovingLevel;
                setupUnitOfWork.ApprovalTrack.Add(track);

                setupUnitOfWork.Commit();


            }

            return Json(new { message = " " });
        }


        [HttpPost]
        public IActionResult ReverseCustomer(multipleTransfer transfer)
        {

            List<multipleTransfer> transfers = new List<multipleTransfer>();
            var logUser = User.Identity.Name??"tayo.olawumi";
            if(string.IsNullOrEmpty(logUser) == true)
            {
                logUser = "tayo.olawumi";
            }

           
            var multipleCount = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.Reference).Count();
           
                var multiple=setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.Reference).FirstOrDefault();
                var reverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.Reference && o.Approved ==false).Count();
            // var reversals = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.Reference && o.Approved == false || o.Reference == transfer.References && o.Approved == false).FirstOrDefault();
            ////  var  counterParty = setupUnitOfWork.counterparty.GetAll().Where(o => o.TransactionId == reversals.TransactionId).FirstOrDefault();
            //if (reverse > 0)
            //{
            //    return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
            //}
          
            if (multipleCount > 0 && reverse == 0)
            {
                var multipleTrans = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.Reference).FirstOrDefault();
                var multiplecounterParty = setupUnitOfWork.counterparty.GetAll().Where(o => o.FormNo == multipleTrans.TransCode).FirstOrDefault();
                var multiplecounterPartys = setupUnitOfWork.counterparty.GetAll().Where(o => o.FormNo == multipleTrans.TransCode).ToList();
                //var existReverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.AccountNo == multiple.AccountNoDr && o.Approved == false).ToList();
                //if (existReverse.Count() > 0)
                //{
                //    return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
                //}
                if (multiplecounterPartys.Count() == 0)
                {
                    return Json(new { Message = "Transaction doesn't exist in customer account" });
                }
                TblBankingReversal reversal = new TblBankingReversal
                {
                    AccountNo = multiple.AccountNoDr,
                    AmountReversed = transfer.Amount,
                    BrCode = multiple.BrCode,
                    CoyCode = multiple.CoyCode,
                    DateReversed = DateTime.Now,
                    OperationId = Convert.ToInt32(PostType.REVERSAL),
                    PostDate = Convert.ToDateTime(transfer.TranDate),
                    PostedBy = multiple.CreatedBy,
                    ReversedBy = logUser,
                    Reference = multiple.Reference,
                    TrasactionDate = Convert.ToDateTime(transfer.TranDate),
                    TransactionId = multiplecounterParty.TransactionId,
                    Comment = transfer.Comment
                };
                setupUnitOfWork.Reversal.Add(reversal);
                setupUnitOfWork.Commit();

                var transId = setupUnitOfWork.Reversal.GetAll().Where(o => o.TransactionId == multiplecounterParty.TransactionId).FirstOrDefault();
                var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                var staffId = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                TblApprovalTrack track = new TblApprovalTrack();
                track.ALevel = 1;
                track.Brcode = multiple.BrCode.ToString();
                track.Coycode = multiple.CoyCode.ToString();
                track.OperationDate = DateTime.Now;
                track.OperationId = Convert.ToInt32(PostType.REVERSAL);
                track.OperationName = "Reversal";
                track.Staffid = staffId.StaffNo;
                track.Username = logUser;
                track.TransactionId = Convert.ToInt32(transId.Id);
                track.MaxAmount = approval.MaxAmt;
                track.MinAmount = approval.MinAmt;
                track.ALevel = approval.ApprovingLevel;
                setupUnitOfWork.ApprovalTrack.Add(track);

                setupUnitOfWork.Commit();
                return Json(new { message = " " });
            }
        

            return Json(new { message = "Transaction has already been reversed. Waiting for approval" });
        }
        // [AllowAnonymous]
        [HttpPost]
        public IActionResult ReverseCustomerSingle(multipleTransfer transfer)
        {

            List<multipleTransfer> transfers = new List<multipleTransfer>();
            var logUser = User.Identity.Name??"tayo.olawumi";
            if (string.IsNullOrEmpty(logUser) == true)
            {
                logUser = "tayo.olawumi";
            }

            var singleCount = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.References).Count();
            
            var single = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.References).FirstOrDefault();
           
            var reverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.References && o.Approved == false ).Count();
            // var reversals = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.Reference && o.Approved == false || o.Reference == transfer.References && o.Approved == false).FirstOrDefault();
            ////  var  counterParty = setupUnitOfWork.counterparty.GetAll().Where(o => o.TransactionId == reversals.TransactionId).FirstOrDefault();
            //if (reverse > 0)
            //{
            //    return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
            //}

          
            if (singleCount > 0 && reverse == 0)
            {
                var singleTrans = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.References).FirstOrDefault();
                var singlecounter = setupUnitOfWork.counterparty.GetAll().Where(o => o.FormNo == singleTrans.Reference).ToList();
                var singlecounterParty = singlecounter.FirstOrDefault();
                //var existReverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.AccountNo == single.AccountDr && o.Approved == false).ToList();
                //if (existReverse.Count() > 0)
                //{
                //    return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
                //}
                if (singlecounter.Count() == 0)
                {
                    return Json(new { Message = "Transaction doesn't exist in customer account" });
                }
                TblBankingReversal reversal = new TblBankingReversal
                {
                    AccountNo = single.AccountDr,
                    AmountReversed = single.Amount,
                    BrCode = single.BrCode,
                    CoyCode = single.CoyCode,
                    DateReversed = DateTime.Now,
                    OperationId = Convert.ToInt32(PostType.REVERSAL),
                    PostDate = Convert.ToDateTime(transfer.TranDate),
                    PostedBy = single.CreateBy,
                    ReversedBy = logUser,
                    Reference = single.Reference,
                    TrasactionDate = Convert.ToDateTime(transfer.TranDate),
                    TransactionId = singlecounterParty.TransactionId,
                    Comment = transfer.Comment
                };
                setupUnitOfWork.Reversal.Add(reversal);
                setupUnitOfWork.Commit();

                var transId = setupUnitOfWork.Reversal.GetAll().Where(o => o.TransactionId == singlecounterParty.TransactionId).FirstOrDefault();
                var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                var staffId = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                TblApprovalTrack track = new TblApprovalTrack();
                track.ALevel = 1;
                track.Brcode = single.BrCode.ToString();
                track.Coycode = single.CoyCode.ToString();
                track.OperationDate = DateTime.Now;
                track.OperationId = Convert.ToInt32(PostType.REVERSAL);
                track.OperationName = "Reversal";
                track.Staffid = staffId.StaffNo;
                track.Username = logUser;
                track.TransactionId = Convert.ToInt32(transId.Id);
                track.MaxAmount = approval.MaxAmt;
                track.MinAmount = approval.MinAmt;
                track.ALevel = approval.ApprovingLevel;
                setupUnitOfWork.ApprovalTrack.Add(track);

                setupUnitOfWork.Commit();
                return Json(new { message = " " });
            }
            //}

            return Json(new { message = "Transaction has already been reversed. Waiting for approval" });
        }

        [HttpPost]
        public IActionResult ReverseGL(multipleTransfer transfer)
        {

            List<multipleTransfer> transfers = new List<multipleTransfer>();
            var logUser = User.Identity.Name??"tayo.olawumi";
            if (string.IsNullOrEmpty(logUser) == true)
            {
                logUser = "tayo.olawumi";
            }

            var singleCount = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.References).Count();
            var multipleCount = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.References).Count();
            var single = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.References).FirstOrDefault();
            var multiple = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.References).FirstOrDefault();
            var reverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.References && o.Approved == false).Count();
            var reversals = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.References && o.Approved == false || o.Reference == transfer.References && o.Approved == false).FirstOrDefault();
            //  var  counterParty = setupUnitOfWork.counterparty.GetAll().Where(o => o.TransactionId == reversals.TransactionId).FirstOrDefault();
            if (reverse > 0)
            {
                return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
            }

            if (multipleCount > 0 && reverse == 0)
            {
                var multipleTrans = setupUnitOfWork.MultipleFund.GetAll().Where(o => o.Reference == transfer.References).FirstOrDefault();
                var multiplecounterParty = setupUnitOfWork.counterparty.GetAll().Where(o => o.FormNo == multipleTrans.TransCode).FirstOrDefault();
                var multiplecounterPartys = setupUnitOfWork.counterparty.GetAll().Where(o => o.FormNo == multipleTrans.TransCode).ToList();
                var multipletranPartys = setupUnitOfWork.Transaction.GetAll().Where(o => o.Ref == multipleTrans.Reference).ToList();
                //var existReverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.AccountNo == multiple.AccountNoDr && o.Approved == false).ToList();
                //if (existReverse.Count() > 0)
                //{
                //    return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
                //}
                if (multiplecounterPartys.Count() > 0 )
                {

                    TblBankingReversal reversal = new TblBankingReversal
                    {
                        AccountNo = multiple.AccountNoDr,
                        AmountReversed = transfer.Amounts,
                        BrCode = multiple.BrCode,
                        CoyCode = multiple.CoyCode,
                        DateReversed = DateTime.Now,
                        OperationId = Convert.ToInt32(PostType.REVERSAL),
                        PostDate = Convert.ToDateTime(transfer.TranDate),
                        PostedBy = multiple.CreatedBy,
                        ReversedBy = logUser,
                        Reference = multiple.Reference,
                        TrasactionDate = Convert.ToDateTime(transfer.TranDate),
                        TransactionId = multiplecounterParty.TransactionId,
                        Comment = transfer.Comment
                    };

                    setupUnitOfWork.Reversal.Add(reversal);
                    setupUnitOfWork.Commit();
                    return Json(new { message = " " });
                }
                if ( multipletranPartys.Count() > 0)
                {

                    TblBankingReversal reversal = new TblBankingReversal
                    {
                        AccountNo = multiple.AccountNoDr,
                        AmountReversed = transfer.Amounts,
                        BrCode = multiple.BrCode,
                        CoyCode = multiple.CoyCode,
                        DateReversed = DateTime.Now,
                        OperationId = Convert.ToInt32(PostType.REVERSAL),
                        PostDate = Convert.ToDateTime(transfer.TranDate),
                        PostedBy = multiple.CreatedBy,
                        ReversedBy = logUser,
                        Reference = multiple.Reference,
                        TrasactionDate = Convert.ToDateTime(transfer.TranDate),
                        TransactionId = multipletranPartys.FirstOrDefault().Id,
                        Comment = transfer.Comment
                    };

                    setupUnitOfWork.Reversal.Add(reversal);
                    setupUnitOfWork.Commit();

                    var transId = setupUnitOfWork.Reversal.GetAll().Where(o => o.TransactionId == multipletranPartys.FirstOrDefault().Id).FirstOrDefault();
                    var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                    var staffId = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                    TblApprovalTrack track = new TblApprovalTrack();
                    track.ALevel = 1;
                    track.Brcode = multiple.BrCode;
                    track.Coycode = multiple.CoyCode;
                    track.OperationDate = DateTime.Now;
                    track.OperationId = Convert.ToInt32(PostType.REVERSAL);
                    track.OperationName = "Reversal";
                    track.Staffid = staffId.StaffNo;
                    track.Username = logUser;
                    track.TransactionId = Convert.ToInt32(transId.Id);
                    track.MaxAmount = approval.MaxAmt;
                    track.MinAmount = approval.MinAmt;
                    track.ALevel = approval.ApprovingLevel;
                    setupUnitOfWork.ApprovalTrack.Add(track);

                    setupUnitOfWork.Commit();

                    return Json(new { message = " " });
                }

            }

          

            return Json(new { message = "Transaction has already been reversed. Waiting for approval" });
        }
       
        [HttpPost]
        public IActionResult ReverseGLSingle(multipleTransfer transfer)
        {

            List<multipleTransfer> transfers = new List<multipleTransfer>();
            var logUser = User.Identity.Name??"tayo.olawumi";
            if (string.IsNullOrEmpty(logUser) == true)
            {
                logUser = "tayo.olawumi";
            }

            var singleCount = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.Reference).Count();
           
            var single = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.Reference).FirstOrDefault();
           
            var reverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.Reference && o.Approved == false).Count();
            var reversals = setupUnitOfWork.Reversal.GetAll().Where(o => o.Reference == transfer.Reference && o.Approved == false).FirstOrDefault();
            //  var  counterParty = setupUnitOfWork.counterparty.GetAll().Where(o => o.TransactionId == reversals.TransactionId).FirstOrDefault();
            if (reverse > 0)
            {
                return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
            }

         

            if (singleCount > 0 && reverse == 0)
            {
                var singleTrans = setupUnitOfWork.SingleFund.GetAll().Where(o => o.Reference == transfer.Reference).FirstOrDefault();
                var singlecounter = setupUnitOfWork.counterparty.GetAll().Where(o => o.FormNo == singleTrans.Reference).ToList();
                var singlecounterParty = singlecounter.FirstOrDefault();
                //var existReverse = setupUnitOfWork.Reversal.GetAll().Where(o => o.AccountNo == single.AccountDr && o.Approved == false).ToList();
                //if (existReverse.Count() > 0)
                //{
                //    return Json(new { Message = "Transaction has already been reversed. Waiting for approval" });
                //}
                if (singlecounter.Count() == 0)
                {
                    return Json(new { Message = "Transaction doesn't exist in customer account" });
                }
                TblBankingReversal reversal = new TblBankingReversal
                {
                    AccountNo = single.AccountDr,
                    AmountReversed = single.Amount,
                    BrCode = single.BrCode,
                    CoyCode = single.CoyCode,
                    DateReversed = DateTime.Now,
                    OperationId = Convert.ToInt32(PostType.REVERSAL),
                    PostDate = Convert.ToDateTime(transfer.TranDate),
                    PostedBy = single.CreateBy,
                    ReversedBy = logUser,
                    Reference = single.Reference,
                    TrasactionDate = Convert.ToDateTime(transfer.TranDate),
                    TransactionId = singlecounterParty.TransactionId,
                    Comment = transfer.Comment
                };
                setupUnitOfWork.Reversal.Add(reversal);
                setupUnitOfWork.Commit();

                var transId = setupUnitOfWork.Reversal.GetAll().Where(o => o.TransactionId == singlecounterParty.TransactionId).FirstOrDefault();
                var approval = db.TblOperationApproval.Where(o => o.OperationId == transId.OperationId && o.ApprovingAuthority == logUser).FirstOrDefault();
                var staffId = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                TblApprovalTrack track = new TblApprovalTrack();
                track.ALevel = 1;
                track.Brcode = single.BrCode.ToString();
                track.Coycode = single.CoyCode.ToString();
                track.OperationDate = DateTime.Now;
                track.OperationId = Convert.ToInt32(PostType.REVERSAL);
                track.OperationName = "Reversal";
                track.Staffid = staffId.StaffNo;
                track.Username = logUser;
                track.TransactionId = Convert.ToInt32(transId.Id);
                track.MaxAmount = approval.MaxAmt;
                track.MinAmount = approval.MinAmt;
                track.ALevel = approval.ApprovingLevel;
                setupUnitOfWork.ApprovalTrack.Add(track);

                setupUnitOfWork.Commit();
                return Json(new { message = " " });
            }
            //}

            return Json(new { message = "Transaction has already been reversed. Waiting for approval" });
        }
       
        
        public JsonResult loadTeller()
        {
         
            var logUser = User.Identity.Name??"tayo.olawumi";
            var Branch = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault().BranchId;
          
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.TillLimit.GetAll().ToList();
            List<SelectContent> list = new List<SelectContent>();
            var reply = (from tbl_FinanceCounterpartyTransaction in db.TblFinanceCounterpartyTransaction
                          where
                            (
                            tbl_FinanceCounterpartyTransaction.Branch == Branch)
                            orderby
                            tbl_FinanceCounterpartyTransaction.UserName
                        select new
                        {
                            tbl_FinanceCounterpartyTransaction.UserName
                            //tbl_FinanceCounterpartyTransaction.TransactionId
                        }).AsEnumerable().Distinct();
            foreach (var item in reply)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.UserName,
                    text = item.UserName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
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
        }
        #endregion
    }
}