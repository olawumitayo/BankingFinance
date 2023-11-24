using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TheCorebanking.Finance
{
    public class ActivityLog : ActionFilterAttribute
    {
        private TheCoreBankingContext _service;

        //injecting specified service to insert the log in database.
        public ActivityLog(TheCoreBankingContext service)
        {
            _service = service;
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Stores the Request in an Accessible object
            var request = filterContext.HttpContext.Request;
            // Generate the log of user activity

            LogUserActivity log = new LogUserActivity()
            {
                // Username. shall be changed to UserId later afteter Auth management.
                UserName = filterContext.HttpContext.User.Identity.Name ?? "Anonymous",
                // The IP Address of the Request
            }
            IPAddress = request.HttpContext.Connection.RemoteIpAddress.ToString();
            // The URL that was accessed
            AreaAccessed = Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
            // Creates Timestamp
            TimeStamp = DateTime.Now
        }

        //saves the log to database
        _service.SaveLog(log);

        // Finishes executing the Action as normal 
        base.OnActionExecuting(filterContext);
    }
}
