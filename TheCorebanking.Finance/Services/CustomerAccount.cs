using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCoreBanking.Finance.Data.Models;

namespace TheCorebanking.Finance.Services
{
    public class CustomerAccount
    {
        public string accountnumber { get; set; }
        public string accountname { get; set; }
        public string availablebalance { get; set; }
        public string operationid { get; set; }
        public string approvalstatusid { get; set; }

        public TblSingleFundTransfer transaction { get; set; }

    }
}
