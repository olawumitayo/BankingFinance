using System;
using System.Collections.Generic;

namespace TheCorebanking.Finance.Data.Models
{
    public partial class TblFinanceCurrency
    {
        public int CurrCode { get; set; }
        public string CurrName { get; set; }
        public string CurrSymbol { get; set; }
        public decimal? ExchangeRate { get; set; }
        public string CountryCode { get; set; }
    }
}
