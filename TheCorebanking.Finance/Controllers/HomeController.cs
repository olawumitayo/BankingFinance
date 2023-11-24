using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Events;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheCorebanking.Finance.Culture;
using TheCoreBanking.Finance.Models;

namespace TheCoreBanking.Finance.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
     
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult SetCulture(string culture)
        //{
        //    // Validate input
        //    culture = CultureHelper.GetValidCulture(culture);
        //    // Save culture in a cookie
        //    HttpCookie cookie = Request.Cookies["_culture"];
        //    if (cookie != null)
        //        cookie.Value = culture;   // update cookie value
        //    else
        //    {
        //        cookie = new HttpCookie("_culture");
        //        cookie.HttpOnly = false; // Not accessible by JS.
        //        cookie.Value = culture;
        //        cookie.Expires = DateTime.Now.AddYears(1);
        //    }
        //    Response.Cookies.Add(cookie);
        //    return RedirectToAction("Index", "Home", new { cultureName = culture });
        //}
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync("Main.Cookies");

        //    await HttpContext.SignOutAsync("oidc");
        //    return Redirect("/");
        //}
        public async Task Logout()
        {
           await HttpContext.SignOutAsync("Main.Cookies");
           
            await HttpContext.SignOutAsync("oidc");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

                [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            
            return View();
        }

    }
}
