using AdvanceUI.Models.DTO.Employee;
using AdvanceUI.Models.DTO.Project;
using System.Collections.Generic;
using System;
using AdvanceUI.Models.DTO.Status;
using AdvanceUI.Models.DTO.Payment;
using AdvanceUI.Models.DTO.AdvanceHistory;
using AdvanceUI.Models.Receipt;

namespace AdvanceUI.Models.DTO.Advance
{
    public class AdvanceSelectDTO
    {
        public int ID { get; set; }

        public decimal? AdvanceAmount { get; set; }

        public string AdvanceDescription { get; set; }

        public int? ProjectID { get; set; }

        public DateTime? DesiredDate { get; set; }

        public int? StatusID { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? EmployeeID { get; set; }

        public virtual StatusSelectDTO Status { get; set; }

        public virtual EmployeeSelectDTO Employee { get; set; }

        public virtual ICollection<PaymentSelectDTO> Payments { get; set; }

        public virtual ProjectSelectDTO Project { get; set; }

        public virtual ICollection<AdvanceHistorySelectDTO> AdvanceHistories { get; set; }

        public virtual ICollection<ReceiptSelectDTO> Receipts { get; set; }
    }
}
