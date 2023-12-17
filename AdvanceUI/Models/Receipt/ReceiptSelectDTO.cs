using System;

namespace AdvanceUI.Models.Receipt
{
    public class ReceiptSelectDTO
    {
        public int ID { get; set; }

        public string ReceiptNo { get; set; }

        public bool? isRefundReceipt { get; set; }

        public int? AdvanceID { get; set; }

        public DateTime? Date { get; set; }

        public int? AccountantID { get; set; }
    }
}
