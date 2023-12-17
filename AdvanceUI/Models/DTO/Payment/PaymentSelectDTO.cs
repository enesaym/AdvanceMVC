using System;

namespace AdvanceUI.Models.DTO.Payment
{
    public class PaymentSelectDTO
    {
        public int ID { get; set; }

        public DateTime? DeterminedPaymentDate { get; set; }

        public int? FinanceManagerID { get; set; }

        public int? AdvanceId { get; set; }
    }
}
