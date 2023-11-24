using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TheCorebanking.Finance.Data
{
    [DataContract]
    public class PostingDTO
    {
        /// <summary>
        /// Account number to debit
        /// </summary>
        /// 
        [DataMember]
        public string DrAccount { get; set; }
        /// <summary>
        /// Account number to credit
        /// </summary>
        [DataMember]
        public string CrAccount { get; set; }
        /// <summary>
        /// Debit narration
        /// </summary>
        [DataMember]
        public string DrNarration { get; set; }
        /// <summary>
        /// Credit Narration
        /// </summary>
        [DataMember]
        public string CrNarration { get; set; }
        /// <summary>
        /// Amount to credit or debit
        /// </summary>
        [DataMember]
        public decimal Amount { get; set; }
        /// <summary>
        /// Value date
        /// </summary>
        [DataMember]
        public DateTime ValueDate { get; set; }
        /// <summary>
        /// transaction posted by?
        /// </summary>
        [DataMember]
        public string PostedBy { get; set; }
        /// <summary>
        /// Transaction approved  by?
        /// </summary>
        [DataMember]
        public string ApprovedBy { get; set; }
        /// <summary>
        /// Batch Reference
        /// </summary>
        [DataMember]
        public string BatchRef { get; set; }
        /// <summary>
        /// Transaction type
        /// </summary>
        [DataMember]
        public int TransactionType { get; set; }
        /// <summary>
        /// Calling application
        /// </summary>
        [DataMember]
        public string AppID { get; set; }

        [DataMember]
        public string Status { get; set; }
        /// Branch where transaction occurred
        /// </summary>
        [DataMember]
        public string BrCode { get; set; }
        /// <summary>
        /// Post type 1- Customer to customer Transation, 2- Customer to GL transaction, 3- General Ledger transaction
        /// </summary>
        [DataMember]
        public int PostType { get; set; }
        /// <summary>
        /// Mis Code
        /// </summary>
        [DataMember]
        public string MISCode { get; set; }
        [DataMember]
        public string transSequence { get; set; }

    }
}
