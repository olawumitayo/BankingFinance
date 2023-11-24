using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCorebanking.Finance.Services
{
    public class APIUTIL
    {
        public static string FormatException(Exception ex)
        {
            var message = ex.Message;

            //Add the inner exception if present (showing only the first 50 characters of the first exception)
            if (ex.InnerException == null) return message;
            if (message.Length > 50)
                message = message.Substring(0, 50);

            message += "...->" + ex.InnerException.Message;

            return message;
        }
    }
}
