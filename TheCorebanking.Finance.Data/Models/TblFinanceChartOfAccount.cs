using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheCoreBanking.Finance.Data.Models
{
    public partial class TblFinanceChartOfAccount
    {
        public long Id { get; set; }
        public string AccountId { get; set; }
        public string AccountName { get; set; }
        public int AccountTypeId { get; set; }
        public int AccountCategoryId { get; set; }
        public int AccountGroupId { get; set; }
        public string CoyId { get; set; }
        public string BrId { get; set; }
        public int CurrCode { get; set; }
        public int Costcode { get; set; }
        public int StCode { get; set; }
        public string UserName { get; set; }
        public int IncomeSheetOrder { get; set; }
        public int BalanceSheetOrder { get; set; }
        public bool SystemUse { get; set; }
        public int AccountStatus { get; set; }
        public bool? BrSpecific { get; set; }
        public DateTime DateCreated { get; set; }
        public string OldAccountId { get; set; }
        public string OldAccountId1 { get; set; }
        public string OldAccountId2 { get; set; }
        public string FscaptionCode { get; set; }
        public string RelationShip { get; set; }
        public string LevelId { get; set; }
        public bool? IsParentGl { get; set; }
        public string ParentGlid { get; set; }
        public string CreatedBy { get; set; }
        public string ProductId { get; set; }

        //[ForeignKey("AccountTypeId")]
        // public TblFinanceAccountType accounttype { get; set; }
    }

  

}
