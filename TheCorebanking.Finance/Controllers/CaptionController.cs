using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;

namespace TheCoreBanking.Finance.Controllers
{


    [Authorize]
   [AllowAnonymous]
    public class CaptionController : Controller
    {
        TheCoreBankingContext db = new TheCoreBankingContext();
        public CaptionController(ISetupUnitOfWork uowSetup)
        {
            setupUnitOfWork = uowSetup;
        }

        public string GetLoggedUser()
        {
            var logUser = User.Identity.Name??"tayo.olawumi";
           
            return logUser;
        }
        public ISetupUnitOfWork setupUnitOfWork { get; set; }
        //[Authorize()]
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
            String[] Status = { "Active", "Inactive" };
            ViewBag.status = Status;
            return View();
        }
        public JsonResult AddMain(TblFinanceAccountType mainInformation)
        {
            //bool active = false;
            if (mainInformation.Active == "1")
            {
                mainInformation.Active = "Active";
            }
            else
            {
                mainInformation.Active = "Not Active";
            }
            // setupUnitOfWork.Transaction.InsertAccountType(mainInformation.Description, mainInformation.AccountCategoryId, active,Convert.ToInt16(mainInformation.SubCaptionId));
            // mainInformation.Active = mainInformation.Active;
            mainInformation.BalanceSheetOrder = 1;
            mainInformation.IncomeSheetOrder = 1;
            long ID = setupUnitOfWork.AccountType.GetAll().OrderByDescending(o => o.Id).FirstOrDefault().Id;
            mainInformation.Id = ID + 1;
            setupUnitOfWork.AccountType.Add(mainInformation);
            setupUnitOfWork.Commit();
            return Json(mainInformation.Id);
        }
        public JsonResult UpdateMain(int Id, TblFinanceAccountType mainInformation)
        {
            mainInformation.Active = "Active";
            mainInformation.BalanceSheetOrder = 1;
            mainInformation.IncomeSheetOrder = 1;
            setupUnitOfWork.AccountType.Update(mainInformation);
            setupUnitOfWork.Commit();
            return Json(mainInformation.Id);
        }
        public IActionResult RemoveMain(TblFinanceAccountType mainInformation)
        {

            setupUnitOfWork.AccountType.Delete(mainInformation);
            setupUnitOfWork.Commit();
            return Json(mainInformation.Id);
        }

        public JsonResult AddSub(long Id,[Bind("Id", "AccountSubName", "AccountSubId", "AccountId", "AccountStatus", "Currency", "UserName", "MsreplTranVersion")]TblFinanceAccountSub tblFinanceAccountSub)
        {
            //System.Random randoms = new System.Random();
            //tblFinanceCostCenter.Costcode = (randoms.Next(1, 10000000));
            Random random = new Random();
            var refer = random.Next(100001, 999999).ToString();            
            tblFinanceAccountSub.AccountId = refer;
            tblFinanceAccountSub.AccountSubId = refer;
            tblFinanceAccountSub.UserName = "sys";
            setupUnitOfWork.Sub.Add(tblFinanceAccountSub);
            setupUnitOfWork.Commit();
            return Json(tblFinanceAccountSub.Id);
        }
        public JsonResult UpdateSub(long Id, [Bind("Id", "AccountSubName", "AccountSubId", "AccountId", "AccountStatus", "Currency", "UserName", "MsreplTranVersion")] TblFinanceAccountSub subInformation)
        {
            setupUnitOfWork.Sub.Update(subInformation);
            setupUnitOfWork.Commit();
            return Json(subInformation.Id);
        }
        public IActionResult RemoveSub(TblFinanceAccountSub subInformation)
        {

            setupUnitOfWork.Sub.Delete(subInformation);
            setupUnitOfWork.Commit();
            return Json(subInformation.Id);
        }
        #region Caption Upload

        public JsonResult MainUpload()
        {
            ImportMain();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceAccountType> ImportMain()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\Main.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["main"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceAccountType> MainList = new List<TblFinanceAccountType>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {
                        string date = workSheet.Cells[i, 1].Value.ToString();
                        int first = date.IndexOf('-');
                        int last = date.LastIndexOf('-');
                        MainList.Add(new TblFinanceAccountType
                        {
                            Description = workSheet.Cells[i, 1].Value.ToString(),
                            AccountCategoryId = Convert.ToInt32(workSheet.Cells[i, 2].Value),
                            BalanceSheetOrder = Convert.ToInt32(workSheet.Cells[i, 3].Value),
                            IncomeSheetOrder = Convert.ToInt32(workSheet.Cells[i, 4].Value),
                            Active = Convert.ToString(workSheet.Cells[i, 5].Value),
                            SubCaptionId = Convert.ToInt32(workSheet.Cells[i, 6].Value),
                            MainCaptionCode = Convert.ToString(workSheet.Cells[i, 7].Value)

                        });
                    }
                }

                setupUnitOfWork.AccountType.AddRange(MainList);
                setupUnitOfWork.Commit();
                return MainList;
            }
        }

        public JsonResult SubUpload()
        {
            ImportSub();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceAccountSub> ImportSub()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\Sub.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["sub"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceAccountSub> SubList = new List<TblFinanceAccountSub>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {
                        string date = workSheet.Cells[i, 1].Value.ToString();
                        
                        SubList.Add(new TblFinanceAccountSub
                        {
                            AccountSubId = workSheet.Cells[i, 1].Value.ToString(),
                            AccountSubName = Convert.ToString(workSheet.Cells[i, 2].Value),
                            AccountId = Convert.ToString(workSheet.Cells[i, 3].Value),
                            AccountStatus = Convert.ToInt32(workSheet.Cells[i, 4].Value),
                            Currency = Convert.ToInt32(workSheet.Cells[i, 5].Value),
                            UserName = Convert.ToString(workSheet.Cells[i, 6].Value),
                            MsreplTranVersion = workSheet.Cells[i, 7].Value.ToString()
                           

                        });
                    }
                }

                setupUnitOfWork.Sub.AddRange(SubList);
                setupUnitOfWork.Commit();
                return SubList;
            }
        }

        #endregion
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
        public JsonResult listSubCaption()
        {
            var results = setupUnitOfWork.Sub.GetAll().OrderByDescending(p=>p.Id);
            return Json(results);
        }
        public JsonResult listMain()
        {
            var result = setupUnitOfWork.AccountType.GetAll().OrderByDescending(p=>p.Id);
            return Json(result);
        }
        public JsonResult loadSUb()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Sub.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.AccountSubName
                };

                list.Add(load);
            }
            loadFormat.results = list;
            return Json(loadFormat);

        }
        public JsonResult loadMain()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Sub.GetAll();
            List<SelectContent> list = new List<SelectContent>();

            foreach (var item in result)
            {
                SelectContent load = new SelectContent()
                {
                    id = item.Id.ToString(),
                    text = item.AccountSubName
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
        #region Select2 Helper

        public class Select2Format
        {
            public List<SelectContent> results { get; set; }
        }
        public class SelectContent
        {
            public string id { get; set; }
            public string text { get; set; }
        }
        #endregion
    }
}