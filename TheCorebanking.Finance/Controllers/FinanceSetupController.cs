using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheCorebanking.Finance.Data.Models;
using TheCoreBanking.Finance.Data.Contracts;
using TheCoreBanking.Finance.Data.Models;
using OfficeOpenXml;
using System.Web;
using OfficeOpenXml.Drawing;
using System.IO;
using System.Diagnostics;
using System.Data;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Stimulsoft.Report.Web;
using TheCorebanking.Finance.Services;
using Newtonsoft.Json.Linq;
using static TheCorebanking.Finance.Services.Currency;

namespace TheCoreBanking.Finance.Controllers
{
   
    //[Authorize]
 [AllowAnonymous]
    public class FinanceSetupController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private ILogger<FinanceSetupController> logger;


        TheCoreBankingContext db = new TheCoreBankingContext();
        string[] status = { "Active", "Inactive" };
        public FinanceSetupController(ISetupUnitOfWork uowSetup, IHostingEnvironment hostingEnvironment, ILogger<FinanceSetupController> financeLogger)

        {
            setupUnitOfWork = uowSetup;
            _hostingEnvironment = hostingEnvironment;
            logger = financeLogger;
        }

        public ISetupUnitOfWork setupUnitOfWork { get; set; }
        public string GetLoggedUser()
        {
            var logUser = User.Identity.Name??"tayo.olawumi";
   
            return logUser;
        }

       
       // [Authorize(Roles = "finance")]
        public IActionResult Index()
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branchs = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId || i.Id ==Convert.ToInt32(staffInfo.BranchId));
            var branch = branchs.FirstOrDefault().BrAddress;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            logger.LogInformation("Index method called");
            ViewBag.Status = status.ToList();
            return View();
        }


       public async Task<IActionResult> AddCurrency(string CurrName, string CurrSymbol, string amount,TblFinanceCurrency currency)
        {
            var staffInfo = db.TblStaffInformation.Where(i => i.StaffName == GetLoggedUser()).FirstOrDefault();
            var branch = setupUnitOfWork.Branch.GetAll().Where(i => i.BrId == staffInfo.BranchId).FirstOrDefault().BrAddress;
            //var currentDate = setupUnitOfWork.CurrentDate.GetAll().FirstOrDefault().CurrentDate;
            var currentDate = DateTime.Now;
            ViewData["StaffName"] = staffInfo.StaffName;
            ViewData["CompanyName"] = staffInfo.CoyName;
            ViewData["CurrentDate"] = string.Format("{0:dddd MMMM dd, yyyy}", currentDate);
            ViewData["Branch"] = branch;
            HttpClient client;
            string url = string.Format("https://www.amdoren.com/api/currency.php?api_key=Nk2yUQRv8nDghedC73rdePmBdERzRg&from={0}&to={1}&amount={2}", currency.CurrName.ToUpper(), currency.CurrSymbol.ToUpper(), amount);
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                JObject jsonResponse = JObject.Parse(responseData);
                Currency printObj = JsonConvert.DeserializeObject<Currency>(responseData);
                TblFinanceCurrency currencyList = new TblFinanceCurrency
                {
                    ExchangeRate = Convert.ToDecimal(printObj.amount),
                    CurrName = CurrName,
                    CurrSymbol = CurrSymbol
                };
                ViewBag.Output = printObj.amount;
                setupUnitOfWork.Currency.Add(currencyList);
                setupUnitOfWork.Commit();
                return Json(currencyList.CurrCode);

            }
            return View();

        }

    
        public JsonResult AddGroup(TblFinanceAccountGroup groupInformation)
        {

            ViewBag.Status = status.ToList();
            setupUnitOfWork.Group.Add(groupInformation);
            setupUnitOfWork.Commit();
            return Json(groupInformation.Id);
        }
        public JsonResult UpdateGroup(int Id, TblFinanceAccountGroup groupInformation)
        {
            string message = string.Empty;
            try
            {
                if (groupInformation.Active == null)
                {
                    groupInformation.Active = "Active";
                    setupUnitOfWork.Group.Update(groupInformation);
                    setupUnitOfWork.Commit();
                }
                else
                {
                    ViewBag.Status = groupInformation.Active.ToList();
                    setupUnitOfWork.Group.Update(groupInformation);
                    setupUnitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {

                message = APIUTIL.FormatException(ex) + " An unknown error occured";
            }
           
           
            return Json(groupInformation.Id);
        }
        public IActionResult RemoveGroup(TblFinanceAccountGroup groupInformation)
        {

            setupUnitOfWork.Group.Delete(groupInformation);
            setupUnitOfWork.Commit();
            return Json(groupInformation.Id);
        }

     
        #region Cost
        public JsonResult AddCost(long Id,[Bind("Id", "Costcode", "Costname")] TblFinanceCostCenter tblFinanceCostCenter)
        {
            
            System.Random random = new System.Random();
            tblFinanceCostCenter.Costcode = (random.Next(1, 1000));
            setupUnitOfWork.Cost.Add(tblFinanceCostCenter);
            setupUnitOfWork.Commit();
            return Json(tblFinanceCostCenter.Id);
        }
        public JsonResult UpdateCost(long Id, [Bind("Id", "Costcode", "Costname")] TblFinanceCostCenter costInformation)
        {

            setupUnitOfWork.Cost.Update(costInformation);
            setupUnitOfWork.Commit();
            return Json(costInformation.Id);
        }


        [HttpPost]
        public IActionResult RemoveCost(TblFinanceCostCenter costInformation)
        {

            setupUnitOfWork.Cost.Delete(costInformation);
            setupUnitOfWork.Commit();
            return Json(costInformation.Id);
        }
        public JsonResult AddCategory(long Id,[Bind("Id","Descriptions", "AccountGroupId", "Active")] TblFinanceAccountCategory tblFinanceAccountCategory)
        {

            setupUnitOfWork.Category.Add(tblFinanceAccountCategory);
            setupUnitOfWork.Commit();
            return Json(tblFinanceAccountCategory.Id);
        }
        public JsonResult UpdateCategory(int Id, TblFinanceAccountCategory categoryInformation)
        {
            string msg = string.Empty;
            try
            {
                setupUnitOfWork.Category.Update(categoryInformation);
                setupUnitOfWork.Commit();
                
            }
            catch (Exception ex)
            {

                msg = APIUTIL.FormatException(ex) + "Unknown error occurred";
            }
            return Json(categoryInformation.Id);

        }


        [HttpPost]
        public IActionResult RemoveCategory(TblFinanceAccountCategory categoryInformation)
        {

            setupUnitOfWork.Category.Delete(categoryInformation);
            setupUnitOfWork.Commit();
            return Json(categoryInformation.Id);
        }
        #endregion

        #region GL Level
        public JsonResult AddGLLevel(long Id, [Bind("Id", "LevelName")] Gllevel level)
        {

            db.Gllevel.Add(level);
            db.SaveChanges();
            return Json(level.Id);
        }
        public JsonResult UpdateGLLevel(long Id, [Bind("Id", "LevelName")] Gllevel level)
        {
            db.Gllevel.Update(level);
            db.SaveChanges();
            return Json(level.Id);
        }
        public JsonResult listlevel()
        {
           var result= db.Gllevel.ToList();
           
            return Json(result);
        }
        #endregion
        #region GL Policy
        [HttpPost]
        public JsonResult AddGLPolicy(TblGlpolicy tblGlpolicy)
        {
            if(Convert.ToInt32(tblGlpolicy.GlLength) < 6 || Convert.ToInt32(tblGlpolicy.GlLength) > 9)
            {
                return Json(new { message="The number must not exceed 9 and not less than 6" } );
            }
            else
            {
                db.TblGlpolicy.Add(tblGlpolicy);
                db.SaveChanges();
                return Json(tblGlpolicy.Id);
            }
           
        }
        [HttpGet]
        public JsonResult listPolicy()
        {

         var result=   db.TblGlpolicy.ToList().OrderByDescending(o=>o.Id);
            
            return Json(result);
        }
        #endregion

        #region Currency
        public JsonResult AddCurrencys(TblFinanceCurrency currency)
        {
            
            setupUnitOfWork.Currency.Add(currency);
            setupUnitOfWork.Commit();
            return Json(currency.CurrCode);
        }

        public JsonResult EditCurrency(int CurrCode, TblFinanceCurrency currency)
        {
            setupUnitOfWork.Currency.Update(currency);
            setupUnitOfWork.Commit();
            return Json(currency.CurrCode);
        }

        public IActionResult RemoveCurrency(TblFinanceCurrency currency)
        {
            setupUnitOfWork.Currency.Delete(currency);
            setupUnitOfWork.Commit();
            return Json(currency.CurrCode);
        }
        #endregion

        #region Finance Upload
        public JsonResult CategoryUpload()
        {
            ImportCategory();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceAccountCategory> ImportCategory()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\category.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["category"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceAccountCategory> CategoryList = new List<TblFinanceAccountCategory>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {
                        string date = workSheet.Cells[i, 1].Value.ToString();
                        int first = date.IndexOf('-');
                        int last = date.LastIndexOf('-');
                        CategoryList.Add(new TblFinanceAccountCategory
                        {
                            Descriptions = workSheet.Cells[i, 1].Value.ToString(),
                            AccountGroupId = Convert.ToInt32(workSheet.Cells[i, 2].Value),
                            Active = Convert.ToString(workSheet.Cells[i, 3].Value)
                           
                        });
                    }
                }

                setupUnitOfWork.Category.AddRange(CategoryList);
                setupUnitOfWork.Commit();
                return CategoryList;
            }
        }

        public JsonResult CostUpload()
        {
            ImportCost();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceCostCenter> ImportCost()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\costCenter.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["Cost"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceCostCenter> CostList = new List<TblFinanceCostCenter>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {
                        string date = workSheet.Cells[i, 1].Value.ToString();
                        int first = date.IndexOf('-');
                        int last = date.LastIndexOf('-');
                        CostList.Add(new TblFinanceCostCenter
                        {
                            Costcode = Convert.ToInt32(workSheet.Cells[i, 1].Value),
                            Costname = Convert.ToString(workSheet.Cells[i, 2].Value)
                           
                        });
                    }
                }

                setupUnitOfWork.Cost.AddRange(CostList);
                setupUnitOfWork.Commit();
                return CostList;
            }
        }

        public JsonResult CurrencyUpload()
        {
            ImportCurrency();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceCurrency> ImportCurrency()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\Currency.xlsx";
            FileInfo file = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["currency"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceCurrency> CurrencyList = new List<TblFinanceCurrency>();
                for (int i = 2; i <= totalRows; i++)
                {
                    if (workSheet.Cells[i, 1].Value != null)
                    {
                      
                        CurrencyList.Add(new TblFinanceCurrency
                        {
                            CurrName = Convert.ToString(workSheet.Cells[i, 1].Value),
                            CurrSymbol = Convert.ToString(workSheet.Cells[i, 2].Value),
                            ExchangeRate = Convert.ToDecimal(workSheet.Cells[i, 3].Value),
                            CountryCode = Convert.ToString(workSheet.Cells[i, 4].Value)

                        });
                    }
                }

                setupUnitOfWork.Currency.AddRange(CurrencyList);
                setupUnitOfWork.Commit();
                return CurrencyList;
            }
        }

        public JsonResult GroupUpload()
        {
            ImportGroup();
            return Json("Index");
        }

        //[HttpPost]
        public IList<TblFinanceAccountGroup> ImportGroup()
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            string filePath = @"C:\\Fintrak\Group.xlsx";
            FileInfo file = new FileInfo(filePath);
            
            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["group"];
                int totalRows = workSheet.Dimension.Rows;
                List<TblFinanceAccountGroup> GroupList = new List<TblFinanceAccountGroup>();
                for (int i = 2; i <= totalRows; i++)
                {
                   if (workSheet.Cells[i, 1].Value != null && (workSheet.Cells[i, 1].Value) != (setupUnitOfWork.Group.GetAll().Where(o=>o.Description != workSheet.Cells[i, 1].Value.ToString() ).FirstOrDefault()) )
                    {
                       
                        GroupList.Add(new TblFinanceAccountGroup
                        {
                            Description = Convert.ToString(workSheet.Cells[i, 1].Value),
                             Active= Convert.ToString(workSheet.Cells[i, 2].Value)
                           
                        });
                    }
                }

                setupUnitOfWork.Group.AddRange(GroupList);
                setupUnitOfWork.Commit();
                return GroupList;
            }
        }
        #endregion

        #region Fetch Ledger Data

        public JsonResult listgroup()
        {
            var result = setupUnitOfWork.Group.GetAll().ToList();
            return Json(result);
        }
        public JsonResult listcategory()
        {
            var result = setupUnitOfWork.Category.GetAll().ToList();
            return Json(result);
        }
        [AllowAnonymous]
        public JsonResult listcurrency()
        {
            var result = setupUnitOfWork.Currency.GetAll();
            return Json(result);
        }
        public JsonResult listcost()
        {
            var result = setupUnitOfWork.Cost.GetAll();
            return Json(result);
        }
        #endregion

        //For Dropdown list for company() box Select2 starts here
        public JsonResult loadGroup()
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
        //22192596439
        public JsonResult loadCurrency()
        {
            Select2Format loadFormat = new Select2Format();
            var result = setupUnitOfWork.Currency.GetAll().ToList();
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