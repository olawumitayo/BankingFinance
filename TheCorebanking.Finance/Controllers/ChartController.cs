using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Stimulsoft.Report;
using TheCorebanking.Finance.Data;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using TheCoreBanking.Finance.Services;

namespace TheCoreBanking.Finance.Controllers
{
    [AllowAnonymous]
    public class ChartController : Controller
    {
        string[] Finance = { "Post No Debit", "Post No Credit" };
        string[] Teller = { "Post No Debit", "Post No Credit" };
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _emailSender;
        private TheCoreBankingContext _Context;
        public ISetupUnitOfWork setupUnitOfWork { get; set; }
        public string GetLoggedUser()
        {
            var logUser = User.Identity.Name??"tayo.olawumi";

            return logUser;
        }
        public ChartController(ISetupUnitOfWork uowSetup, IHostingEnvironment hostingEnvironment, IEmailSender emailSender)
        {
            setupUnitOfWork = uowSetup;
            _hostingEnvironment = hostingEnvironment;
            _emailSender = emailSender;
        }
        //[Authorize(Roles = "finance")]
        //[AllowAnonymous]
        public IActionResult Index()
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId || i.Id.ToString() == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            ViewBag.finance = Finance;
            ViewBag.teller = Teller;
            return View();

        }
        [HttpPost]
        //[Route("account/send-email")]
        public async Task<IActionResult> SendEmailAsync(string email, string subject, string message)
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            await _emailSender.SendEmailAsync(email, subject, message);
            return View();
        }
        public JsonResult AddChart(long Id, [Bind("Id", "AccountId", "AccountName", "AccountTypeId", "AccountCategoryId", "AccountGroupId", "CoyId", "BrId", "CurrCode", "Costcode", "AccountStatus")] TblFinanceChartOfAccount chartInformation)
        {

            DateTime time = DateTime.Now;
            string format = "dd/MMM/yyyy";
            string payDate = time.ToString(format);
            chartInformation.DateCreated = Convert.ToDateTime(payDate);
            Random random = new Random();
            var refer = random.Next(100001, 999999).ToString();
            setupUnitOfWork.Chart.Add(chartInformation);
            var refers = random.Next(100001, 999999);
            //chartInformation.AccountId = refer;
            //chartInformation.AccountTypeId = refers;
            chartInformation.CoyId = "1";
            chartInformation.BrSpecific = true;
            chartInformation.UserName = "sys";
            setupUnitOfWork.Commit();
            return Json(chartInformation.AccountId);
        }
        [HttpPost]
        public JsonResult UpdateChart(TblFinanceChartOfAccount chartInformation)
        {
            var reply = (from c in _Context.TblFinanceChartOfAccount.Where(c => c.Id == chartInformation.Id) select c).FirstOrDefault();
            if (reply != null)
            {
                reply.AccountName = chartInformation.AccountName;
                _Context.Update(reply);
                _Context.SaveChanges();
            }
            //setupUnitOfWork.Chart.Update(chartInformation);
            //setupUnitOfWork.Commit();
            return Json(chartInformation.Id);
        }
        public IActionResult RemoveChart(TblFinanceChartOfAccount chartInformation)
        {

            setupUnitOfWork.Chart.Delete(chartInformation);
            setupUnitOfWork.Commit();
            return Json(chartInformation.Id);
        }

        public bool TellerPnc { get; set; }
        public JsonResult AddDefault(long DfId, [Bind("DfId", "DfDescription", "AccountId", "AccountName", "FinancePnd", "FinancePnc", "TellerPnd", "TellerPnc")] TblFinanceDefaultAccounts tblFinanceDefault)
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;

            var companyid = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault().CompanyId;
            //ViewBag.AccountNumber = setupUnitOfWork.Chart;
            tblFinanceDefault.ApprovedBy = logUser;
            tblFinanceDefault.CreatedBy = logUser;
            tblFinanceDefault.CoyCode = companyid;
            tblFinanceDefault.DateApproved = DateTime.Now;
            tblFinanceDefault.DateCreated = DateTime.Now;
            tblFinanceDefault.LastUpdate = DateTime.Now;
            setupUnitOfWork.Default.Add(tblFinanceDefault);
            setupUnitOfWork.Commit();
            return Json(tblFinanceDefault.DfId);
        }
        public JsonResult UpdateDefault(long DfId, TblFinanceDefaultAccounts defaultInformation)
        {
            var username = User.Identity.Name??"tayo.olawumi";
            var logUser = username;

            var companyid = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser && o.Approved == true).FirstOrDefault().CompanyId;
            //ViewBag.AccountNumber = setupUnitOfWork.Chart;
            defaultInformation.ApprovedBy = logUser;
            defaultInformation.CreatedBy = logUser;
            defaultInformation.CoyCode = companyid;
            defaultInformation.DateApproved = DateTime.Now;
            defaultInformation.DateCreated = DateTime.Now;
            defaultInformation.LastUpdate = DateTime.Now;
            //ViewBag.finance = Finance.ToList();
            //ViewBag.teller = Teller.ToList();
            //defaultInformation.FinancePnc = ViewBag.finance;
            //defaultInformation.TellerPnc = ViewBag.teller;
            setupUnitOfWork.Default.Update(defaultInformation);
            setupUnitOfWork.Commit();
            return Json(defaultInformation.DfId);
        }
        public IActionResult RemoveDefault(long ID, TblFinanceDefaultAccounts defaultInformation)
        {

            setupUnitOfWork.Default.Delete(defaultInformation);
            setupUnitOfWork.Commit();
            return Json(defaultInformation.DfId);
        }

        public JsonResult AddGL(TblFinanceBank tblFinanceGlmapping)
        {

            setupUnitOfWork.GL.Add(tblFinanceGlmapping);
            setupUnitOfWork.Commit();
            return Json(tblFinanceGlmapping.Id);
        }
        public JsonResult UpdateGL(int Id, TblFinanceBank glInformation)
        {
            setupUnitOfWork.GL.Update(glInformation);
            setupUnitOfWork.Commit();
            return Json(glInformation.Id);
        }
        public IActionResult RemoveGL(TblFinanceBank glInformation)
        {

            setupUnitOfWork.GL.Delete(glInformation);
            setupUnitOfWork.Commit();
            return Json(glInformation.Id);
        }




        #region Chart of Account Upload

        //[Authorize(Roles = "CapitalMarket.PriceUpload")]
        public JsonResult ChartUpload()
        {
            ImportChart();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceChartOfAccount> ImportChart()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\chartOfAccount.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["AccountChart"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceChartOfAccount> chartList = new List<TblFinanceChartOfAccount>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {
                        string date = workSheet.Cells[i, 1].Value.ToString();
                        int first = date.IndexOf('-');
                        int last = date.LastIndexOf('-');
                        chartList.Add(new TblFinanceChartOfAccount
                        {
                            AccountName = workSheet.Cells[i, 2].Value.ToString(),
                            AccountId = workSheet.Cells[i, 3].Value.ToString(),
                            AccountTypeId = Convert.ToInt32(workSheet.Cells[i, 4].Value),
                            AccountCategoryId = Convert.ToInt32(workSheet.Cells[i, 5].Value),
                            AccountGroupId = Convert.ToInt32(workSheet.Cells[i, 6].Value),
                            BrId = workSheet.Cells[i, 7].Value.ToString(),
                            CoyId = workSheet.Cells[i, 8].Value.ToString(),
                            Costcode = Convert.ToInt32(workSheet.Cells[i, 9].Value),
                            CurrCode = Convert.ToInt32(workSheet.Cells[i, 10].Value),
                            StCode = Convert.ToInt32(workSheet.Cells[i, 12].Value),
                            UserName = Convert.ToString(workSheet.Cells[i, 11].Value),
                            IncomeSheetOrder = Convert.ToInt32(workSheet.Cells[i, 16].Value),
                            BalanceSheetOrder = Convert.ToInt32(workSheet.Cells[i, 13].Value),
                            SystemUse = Convert.ToBoolean(workSheet.Cells[i, 14].Value),
                            AccountStatus = Convert.ToInt32(workSheet.Cells[i, 15].Value),
                            DateCreated = Convert.ToDateTime(workSheet.Cells[i, 17].Value),


                        });
                    }
                }

                setupUnitOfWork.Chart.AddRange(chartList);
                setupUnitOfWork.Commit();
                return chartList;
            }
        }

        public JsonResult GLUpload()
        {
            ImportGL();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceBank> ImportGL()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\GLBank.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["gLBank"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceBank> GLList = new List<TblFinanceBank>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {

                        GLList.Add(new TblFinanceBank
                        {
                            BankId = workSheet.Cells[i, 1].Value.ToString(),
                            BankName = workSheet.Cells[i, 2].Value.ToString(),
                            AccNo = Convert.ToString(workSheet.Cells[i, 3].Value),
                            BranchName = Convert.ToString(workSheet.Cells[i, 4].Value),
                            ContactName = Convert.ToString(workSheet.Cells[i, 5].Value),
                            ContactPhoneNo = workSheet.Cells[i, 6].Value.ToString(),
                            ContactEmail = workSheet.Cells[i, 7].Value.ToString(),
                            AccountId = Convert.ToString(workSheet.Cells[i, 8].Value),
                            ContactAddress = Convert.ToString(workSheet.Cells[i, 9].Value)

                        });
                    }
                }

                setupUnitOfWork.GL.AddRange(GLList);
                setupUnitOfWork.Commit();
                return GLList;
            }
        }

        public JsonResult BankUpload()
        {
            ImportBank();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceDefaultAccounts> ImportBank()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\Bank.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["bank"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceDefaultAccounts> BankList = new List<TblFinanceDefaultAccounts>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {

                        BankList.Add(new TblFinanceDefaultAccounts
                        {
                            DfDescription = workSheet.Cells[i, 1].Value.ToString(),
                            AccountId = workSheet.Cells[i, 2].Value.ToString(),
                            AccountName = Convert.ToString(workSheet.Cells[i, 3].Value),
                            FinancePnd = Convert.ToString(workSheet.Cells[i, 4].Value),
                            FinancePnc = Convert.ToString(workSheet.Cells[i, 5].Value),
                            TellerPnd = Convert.ToString(workSheet.Cells[i, 6].Value),
                            CreatedBy = Convert.ToString(workSheet.Cells[i, 7].Value),
                            CoyCode = Convert.ToString(workSheet.Cells[i, 8].Value),
                            TellerPnc = Convert.ToString(workSheet.Cells[i, 9].Value),
                            DateCreated = Convert.ToDateTime(workSheet.Cells[i, 10].Value),
                            Approved = Convert.ToBoolean(workSheet.Cells[i, 11].Value),
                            ApprovedBy = Convert.ToString(workSheet.Cells[i, 12].Value),
                            DateApproved = Convert.ToDateTime(workSheet.Cells[i, 13].Value),
                            Disapproved = Convert.ToBoolean(workSheet.Cells[i, 14].Value),
                            Updated = Convert.ToBoolean(workSheet.Cells[i, 15].Value),
                            UpdatedBy = Convert.ToString(workSheet.Cells[i, 16].Value),
                            LastUpdate = Convert.ToDateTime(workSheet.Cells[i, 17].Value),

                        });
                    }
                }

                setupUnitOfWork.Default.AddRange(BankList);
                setupUnitOfWork.Commit();
                return BankList;
            }
        }
        #endregion



        #region

        public JsonResult listchart()
        {
            var result = setupUnitOfWork.Chart.GetAll().OrderByDescending(c => c.Id);
            return Json(result);
        }
        public JsonResult listdefault()
        {
            var result = setupUnitOfWork.Default.GetAll().OrderByDescending(c => c.DfId);
            return Json(result);
        }
        public JsonResult listGL()
        {
            var result = setupUnitOfWork.GL.GetAll().OrderByDescending(c => c.Id);
            return Json(result);
        }
        #endregion

        public JsonResult loadCompany()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Company.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.CoyName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadBranch()
        {
            var logUser = User.Identity.Name??"tayo.olawumi"; //"abayomi.adelola";
            var UserLog = logUser;

            var staff = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
            var company = setupUnitOfWork.Company.GetAll();
            Select2Format loadFormat = new Select2Format();
            var results = setupUnitOfWork.Branch.GetAll().Where(o => o.CoyId ==staff.CompanyId).ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in results)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.BrName
                };

                list.Add(load);
            }

            loadFormat.results = list;
            return Json(loadFormat);

        }

        public JsonResult loadAccountType()
        {


            using (var context = new TheCoreBankingContext())
            {

                var result = (
               from chartofAccount in context.TblFinanceChartOfAccount
               join accountType in context.TblFinanceAccountType on chartofAccount.AccountTypeId equals accountType.Id into tmpMapp
               join accountcategory in context.TblFinanceAccountCategory on chartofAccount.AccountCategoryId equals accountcategory.Id into tmpGroups
               join accountGroup in context.TblFinanceAccountGroup on chartofAccount.AccountGroupId equals accountGroup.Id into tmpAccount
               join accountCurrency in context.TblFinanceCurrency on chartofAccount.CurrCode equals accountCurrency.CurrCode into tmpCurrency
               from accountType in tmpMapp.DefaultIfEmpty()
               from accountcategory in tmpGroups.DefaultIfEmpty()
               from accountGroup in tmpAccount.DefaultIfEmpty()
               from accountCurrency in tmpCurrency.DefaultIfEmpty()
               select new
               {
                   accountid = chartofAccount.AccountId,
                   accountname = chartofAccount.AccountName,
                   categoryid = chartofAccount.AccountCategoryId,
                   typeid = chartofAccount.AccountTypeId,
                   Coyid = chartofAccount.CoyId,
                   currCode = chartofAccount.CurrCode,
                   groupid = chartofAccount.AccountGroupId,
                   //atypeid = accountType.Id,                   
                   coyid = chartofAccount.CoyId,
                   brid = chartofAccount.BrId,
                   currcode = chartofAccount.CurrCode,
                   costcode = chartofAccount.Costcode,
                   typedescription = accountType.Description,
                   //accountids = accountType.Id,
                   //status = chartofAccount.AccountStatus,
                   //subid = accountType.SubCaptionId,
                   categorydescription = accountcategory.Descriptions,
                   groupDescription = accountGroup.Description
                   //currency = accountCurrency.CurrName
               }

           ).ToList();
                Select2Format loadFormat = new Select2Format();
                //var result = setupUnitOfWork.AccountType.GetAll();
                List<SelectContent> list = new List<SelectContent>();
                foreach (var item in result)
                {
                    SelectContent load = new SelectContent()
                    {

                        id = item.accountid.ToString(),
                        text = item.accountname.ToString(),
                        catid = item.categoryid.ToString(),
                        Typ = item.groupid.ToString(),
                        cate = item.categorydescription.ToString(),
                        acctid = item.currcode.ToString(),
                        branch = item.groupDescription

                    };

                    list.Add(load);
                }
                loadFormat.results = list;
                return Json(loadFormat);
            }


        }

        //public JsonResult loadAccountTypeUpdate()
        //{

        //        using (var context = new TheCoreBankingContext())
        //        {

        //            var result = (
        //           from chartofAccount in context.TblFinanceChartOfAccount
        //           join accountType in context.TblFinanceAccountType on chartofAccount.AccountTypeId equals accountType.Id into tmpMapp
        //           join accountcategory in context.TblFinanceAccountCategory on chartofAccount.AccountCategoryId equals accountcategory.Id into tmpGroups
        //           from accountType in tmpMapp.DefaultIfEmpty()
        //           from accountcategory in tmpGroups.DefaultIfEmpty()

        //           select new
        //           {
        //               accountid = chartofAccount.AccountId,
        //               accountname = chartofAccount.AccountName,
        //               typedescription = accountType.Description,
        //               categorydescription = accountcategory.Descriptions,
        //               coyid = chartofAccount.CoyId


        //               //categoryid = chartofAccount.AccountCategoryId,
        //               //typeid = chartofAccount.AccountTypeId,
        //               //Coyid = chartofAccount.CoyId,
        //               //currCode = chartofAccount.CurrCode,
        //               //groupid = chartofAccount.AccountGroupId,
        //               //atypeid = accountType.Id,
        //               //brid = chartofAccount.BrId,
        //               //currcode = chartofAccount.CurrCode,
        //               //costcode = chartofAccount.Costcode,
        //               //status = chartofAccount.AccountStatus,
        //               //subid = accountType.SubCaptionId

        //           }

        //       ).ToList();
        //            Select2Format loadFormat = new Select2Format();
        //            List<SelectContent> list = new List<SelectContent>();
        //            foreach (var item in result)
        //            {
        //                SelectContent load = new SelectContent()
        //                {

        //                    id = item.accountid.ToString(),
        //                    text = item.accountname.ToString(),
        //                    catid = item.coyid.ToString(),
        //                    Typ = item.typedescription.ToString(),
        //                    cate = item.categorydescription.ToString()
        //                    // acctid = item.currcode.ToString(),
        //                    // branch = item.groupDescription

        //                };

        //                list.Add(load);
        //            }
        //            loadFormat.results = list;
        //            return Json(loadFormat);
        //        }





        //}
        public JsonResult loadAccountTypeNew()
        {


            var accountType = setupUnitOfWork.AccountType.GetAll();
            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in accountType.ToList())
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Description
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCategoryNew(string Id)
        {
            var accountType = setupUnitOfWork.AccountType.GetAll().Where(o => o.Id == Convert.ToInt32(Id)).FirstOrDefault();
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Category.GetAll().Where(o => o.Id == accountType.AccountCategoryId).ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Descriptions
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadAccountGroupNew(string Id)
        {
            var accountType = setupUnitOfWork.AccountType.GetAll().Where(o => o.Id == Convert.ToInt32(Id)).FirstOrDefault();
            var category = setupUnitOfWork.Category.GetAll().Where(o => o.Id == accountType.AccountCategoryId).FirstOrDefault();
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Group.GetAll().Where(o => o.Id == Convert.ToInt32(category.AccountGroupId)).ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Description
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        public JsonResult loadChartNumber(string Id)
        {
            var accountType = setupUnitOfWork.AccountType.GetAll().Where(o => o.Id == Convert.ToInt32(Id)).FirstOrDefault().Id;
            List<GetAccountType> accountList = new List<GetAccountType>();
            Select2Format loadFormat = new Select2Format();
            accountList = setupUnitOfWork.Transaction.ChartOfAccountNumber(accountType.ToString());
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in accountList.ToList())
            {
                SelectContent load = new SelectContent()
                {
                    id = item.id.ToString(),
                    text = item.account
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        public JsonResult loadAccountGroup()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Group.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Description
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCurrency()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Currency.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.CurrCode.ToString(),
                    text = item.CurrName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCategory()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Category.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Descriptions
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadCost()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Cost.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Costname
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadStatus()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Status.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.StCode.ToString(),
                    text = item.StStatus
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadChart()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Chart.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.AccountName,
                    acctid = item.AccountId,
                    branch = item.BrId
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }

        public JsonResult loadVwDefault()
        {

            Select2Format loadFormat = new Select2Format();

            TheCoreBankingContext db = new TheCoreBankingContext();
            var result = (from c in db.TblFinanceChartOfAccount
                          join t in db.TblFinanceAccountType on new { AccountTypeID = (long)c.AccountTypeId } equals new { AccountTypeID = (long)t.Id }
                          join ca in db.TblFinanceAccountCategory on new { AccountCategoryID = (long)c.AccountCategoryId } equals new { AccountCategoryID = (long)ca.Id }
                          join d in db.TblFinanceDefaultAccounts on c.AccountId equals d.AccountId into d_join
                          from d in d_join.DefaultIfEmpty()
                          where
                            d.AccountId == null
                          select new
                          {
                              c.Id,
                              c.AccountId,
                              c.AccountName,
                              Type = t.Description,
                              Category = ca.Descriptions
                          }

                ).ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.AccountId.ToString(),
                    acctid = item.AccountId,
                    text = item.AccountName,
                    Typ = item.Type,
                    cate = item.Category
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        #region  Newchart
        public JsonResult loadProduct()
        {
            Select2Format loadFormat = new Select2Format();

            List<SelectContent> list = new List<SelectContent>();


            var customerReply = db.TblProductGroup.ToList();
            foreach (var item in customerReply)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Productgroupname

                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);
        }
        public JsonResult loadCategoryNewChart()
        {

            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.AccountType.GetAll().ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.Description
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadNewGL()
        {

            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Chart.GetAll().Where(o => o.IsParentGl == true);
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.AccountName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadGLLevel()
        {
            TheCoreBankingContext db = new TheCoreBankingContext();
            Select2Format loadFormat = new Select2Format();
            var result = db.Gllevel.ToList();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.LevelName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        TheCoreBankingContext db = new TheCoreBankingContext();
        Random rand = new Random();
        #endregion
        #region New Chart of Account
        [HttpPost]
        public JsonResult CreateChartOfAccount(TblFinanceChartOfAccount account)
        {

            try
            {

                var username = User.Identity.Name??"tayo.olawumi";
                var logUser = username;
                var type = setupUnitOfWork.AccountType.GetAll().Where(o => o.Id == account.AccountTypeId).FirstOrDefault();
                account.AccountCategoryId = type.AccountCategoryId;
                var category = setupUnitOfWork.Category.GetAll().Where(o => o.Id == account.AccountCategoryId).FirstOrDefault();
                account.AccountGroupId = Convert.ToInt32(category.AccountGroupId);

                var GLPolicy = db.TblGlpolicy.FirstOrDefault();

                var length = Convert.ToInt32(GLPolicy.GlLength);

                var getbranch = setupUnitOfWork.Staff.GetAll().Where(o => o.StaffName == logUser).FirstOrDefault();
                var branch = getbranch.BranchId;
                string accounttype = string.Empty;
                string ProductId = string.Empty;
                string CurrCode = string.Empty;
                if (string.IsNullOrEmpty(account.ParentGlid) == true)
                {
                    if (length == 9)
                    {
                        var glAccount1 = account.AccountTypeId.ToString().Length;
                        var glAccount2 = account.ProductId.ToString().Length;
                        var glaccount3 = account.CurrCode.ToString().Length;
                        if (glAccount1 > 3)
                        {
                            accounttype = account.AccountTypeId.ToString().Substring(0, 3);
                        }
                        else
                        {
                            accounttype = account.AccountTypeId.ToString().PadRight(3, '0');
                        }
                        if (glAccount2 > 3)
                        {
                            ProductId = account.ProductId.ToString().Substring(0, 3);
                        }
                        else
                        {
                            ProductId = account.ProductId.ToString().PadRight(3, '0');
                        }
                        if (glaccount3 > 3)
                        {
                            CurrCode = account.CurrCode.ToString().Substring(0, 3);
                        }
                        else
                        {
                            CurrCode = account.CurrCode.ToString().PadRight(3, '0');
                        }
                        var glAccount = accounttype + ProductId + CurrCode;
                        var newGL = glAccount.Substring(0, length);
                        var existGL = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).Count();
                        var existGL2 = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).FirstOrDefault();
                        if (existGL != 0)
                        {
                            return Json(new { message = $"You have existing account {existGL2.AccountName} on the product selected. Choose another product" });
                        }
                        account.DateCreated = DateTime.Now;
                        account.BalanceSheetOrder = 1;
                        account.IncomeSheetOrder = 1;
                        account.AccountId = newGL;
                        account.CoyId = getbranch.CompanyId;
                        account.BrId = getbranch.BranchId;
                        account.CreatedBy = logUser;
                        account.UserName = logUser;
                        account.AccountStatus = 1;
                        setupUnitOfWork.Chart.Add(account);
                        setupUnitOfWork.Commit();
                    }
                    if (length == 8)
                    {
                        var glAccount1 = account.AccountTypeId.ToString().Length;
                        var glAccount2 = account.ProductId.ToString().Length;
                        var glaccount3 = account.CurrCode.ToString().Length;
                        if (glAccount1 > 3)
                        {
                            accounttype = account.AccountTypeId.ToString().Substring(0, 3);
                        }
                        else
                        {
                            accounttype = account.AccountTypeId.ToString().PadRight(3, '0');
                        }
                        if (glAccount2 > 3)
                        {
                            ProductId = account.ProductId.ToString().Substring(0, 3);
                        }
                        else
                        {
                            ProductId = account.ProductId.ToString().PadRight(3, '0');
                        }
                        if (glaccount3 > 2)
                        {
                            CurrCode = account.CurrCode.ToString().Substring(0, 2);
                        }
                        else
                        {
                            CurrCode = account.CurrCode.ToString().PadRight(2, '0');
                        }
                        var glAccount = accounttype + ProductId + CurrCode;
                        var newGL = glAccount.Substring(0, length);
                        var existGL = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).Count();
                        var existGL2 = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).FirstOrDefault();
                        if (existGL != 0)
                        {
                            return Json(new { message = $"You have existing account {existGL2.AccountName} on the product selected. Choose another product" });
                        }

                        account.DateCreated = DateTime.Now;
                        account.BalanceSheetOrder = 1;
                        account.IncomeSheetOrder = 1;
                        account.AccountId = newGL;
                        account.CoyId = getbranch.CompanyId;
                        account.BrId = getbranch.BranchId;
                        account.CreatedBy = logUser;
                        account.UserName = logUser;
                        setupUnitOfWork.Chart.Add(account);
                        setupUnitOfWork.Commit();
                    }
                    if (length == 7)
                    {
                        var glAccount1 = account.AccountTypeId.ToString().Length;
                        var glAccount2 = account.ProductId.ToString().Length;
                        var glaccount3 = account.CurrCode.ToString().Length;
                        if (glAccount1 > 3)
                        {
                            accounttype = account.AccountTypeId.ToString().Substring(0, 3);
                        }
                        else
                        {
                            accounttype = account.AccountTypeId.ToString().PadRight(3, '0');
                        }
                        if (glAccount2 > 2)
                        {
                            ProductId = account.ProductId.ToString().Substring(0, 2);
                        }
                        else
                        {
                            ProductId = account.ProductId.ToString().PadRight(2, '0');
                        }
                        if (glaccount3 > 2)
                        {
                            CurrCode = account.CurrCode.ToString().Substring(0, 2);
                        }
                        else
                        {
                            CurrCode = account.CurrCode.ToString().PadRight(2, '0');
                        }
                        var glAccount = accounttype + ProductId + CurrCode;
                        var newGL = glAccount.Substring(0, length);
                        var existGL = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).Count();
                        var existGL2 = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).FirstOrDefault();
                        if (existGL != 0)
                        {
                            return Json(new { message = $"You have existing account {existGL2.AccountName} on the product selected. Choose another product" });
                        }
                        account.DateCreated = DateTime.Now;
                        account.BalanceSheetOrder = 1;
                        account.IncomeSheetOrder = 1;
                        account.AccountId = newGL;
                        account.CoyId = getbranch.CompanyId;
                        account.BrId = getbranch.BranchId;
                        account.CreatedBy = logUser;
                        account.UserName = logUser;
                        setupUnitOfWork.Chart.Add(account);
                        setupUnitOfWork.Commit();
                    }
                    if (length == 6)
                    {
                        var glAccount1 = account.AccountTypeId.ToString().Length;
                        var glAccount2 = account.ProductId.ToString().Length;
                        var glaccount3 = account.CurrCode.ToString().Length;
                        if (glAccount1 > 2)
                        {
                            accounttype = account.AccountTypeId.ToString().Substring(0, 2);
                        }
                        else
                        {
                            accounttype = account.AccountTypeId.ToString().PadRight(2, '0');
                        }
                        if (glAccount2 > 2)
                        {
                            ProductId = account.ProductId.ToString().Substring(0, 2);
                        }
                        else
                        {
                            ProductId = account.ProductId.ToString().PadRight(2, '0');
                        }
                        if (glaccount3 > 2)
                        {
                            CurrCode = account.CurrCode.ToString().Substring(0, 2);
                        }
                        else
                        {
                            CurrCode = account.CurrCode.ToString().PadRight(2, '0');
                        }
                        var glAccount = accounttype + ProductId + CurrCode;
                        var newGL = glAccount.Substring(0, length);
                        var existGL = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).Count();
                        var existGL2 = setupUnitOfWork.Chart.GetAll().Where(o => o.AccountId == newGL).FirstOrDefault();
                        if (existGL != 0)
                        {
                            return Json(new { message = $"You have existing account {existGL2.AccountName} on the product selected. Choose another product" });
                        }
                        account.DateCreated = DateTime.Now;
                        account.BalanceSheetOrder = 1;
                        account.IncomeSheetOrder = 1;
                        account.AccountId = newGL;
                        account.CoyId = getbranch.CompanyId;
                        account.BrId = getbranch.BranchId;
                        account.CreatedBy = logUser;
                        account.UserName = logUser;
                        setupUnitOfWork.Chart.Add(account);
                        setupUnitOfWork.Commit();
                    }
                }
                else
                {
                    var getGL = setupUnitOfWork.Chart.GetAll().Where(o => o.Id.ToString() == account.ParentGlid && o.IsParentGl == true).FirstOrDefault();
                    var newGLs = getGL.AccountId.Substring(0, 2) + 1;
                    var oldGl = getGL.AccountId.Substring(3, 6);
                    var accountGL = newGLs + oldGl;
                    account.DateCreated = DateTime.Now;
                    account.BalanceSheetOrder = 1;
                    account.IncomeSheetOrder = 1;
                    account.AccountId = accountGL;
                    account.CoyId = getbranch.CompanyId;
                    account.BrId = getbranch.BranchId;
                    account.CreatedBy = logUser;
                    account.UserName = logUser;
                    setupUnitOfWork.Chart.Add(account);
                    setupUnitOfWork.Commit();
                }
                return Json(new { message = " " });
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public JsonResult GenerateGLNo(TblChartOfAccount account)
        {

            var glAccount = account.AccountTypeId + account.ProductId + account.CurrencyId + rand.Next();
            return Json(glAccount);
        }
        public JsonResult listnewchart()
        {
            var result = setupUnitOfWork.Chart.GetAll().OrderByDescending(o => o.Id);
            return Json(result);
        }
        #endregion



        public class Select2Format
        {
            public List<SelectContent> results { get; set; }
        }
        public class SelectContent
        {
            public string id { get; set; }
            public string text { get; set; }
            public string Typ { get; set; }
            public string cate { get; set; }
            public string acctid { get; set; }
            public string branch { get; set; }
            public string catid { get; set; }


        }



    }
}
