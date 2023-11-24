using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCorebanking.Finance.Culture
{
    public class NoCacheMiddleware
    {

        private readonly RequestDelegate m_next;
        public NoCacheMiddleware(RequestDelegate next)
        {
            m_next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.OnStarting((state) =>
            {

                httpContext.Response.Headers.Append("Cache-Control", "no-cache, no-store, must-revalidate");
                httpContext.Response.Headers.Append("Pragma", "no-cache");
                httpContext.Response.Headers.Append("Expires", "0");
                return Task.FromResult(0);
            }, null);
            await m_next.Invoke(httpContext);
        }
    }
}
