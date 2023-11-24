using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCorebanking.Finance.Services
{
    public class Currency
    {
        public string from { get; set; }
        public string to { get; set; }
        public string amount { get; set; }
        public string rate { get; set; }

        public class RootObject
        {
            public List<Currency> currencyList { get; set; }
        }
    }
}
