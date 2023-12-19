using System;

namespace AdvanceUI.Models.DTO.Advance
{
    public class AccountantApproveDTO
    {
        public int ID { get; set; }

        public string ReceiptNo { get; set; }

        public bool? isRefundReceipt { get; set; }

        public int TitleID { get; set; }

        public decimal ApprovedAmount { get; set; }

        public int? AdvanceID { get; set; }

        public DateTime? Date { get; set; }

        public int? AccountantID { get; set; }
    }
}
