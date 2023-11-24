using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheCorebanking.Finance.Models
{
  
    public class transactionModel
    {
        public int Id { get; set; }
        public string AccountDr { get; set; }
        public string AccountCr { get; set; }
        public string Amount { get; set; }
        public string TransactionType { get; set; }
        public string NarrationDr { get; set; }
        public string NarrationCr { get; set; }
        public string OperationId { get; set; }
        public int OperationType { get; set; }
        public string PostDate { get; set; }
        public string ChequeNo { get; set; }
        public string availablebalance { get; set; }
        public string Accountname { get; set; }
        public string ValueDate { get; set; }
        public string AmountCr { get; set; }
        public string availablebalanceCr { get; set; }
        public decimal maintenanceFee { get; set; }

        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }

    }
    public class tempTransferModel
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public string Reference { get; set; }
    }
    public class TotalResult
    {
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }

        public bool ChargeStamp { get; set; }
    }
    public class multipleTransfer
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public string Comment { get; set; }
        public string TranDate { get; set; }
        public string References { get; set; }
        public decimal Amounts { get; set; }

    }
    public class Trans
    {
        public string Comment { get; set; }
        public string TranDate { get; set; }
    }
   
}
