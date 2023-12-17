using AdvanceUI.Models.DTO.Employee;
using System;

namespace AdvanceUI.Models.DTO.AdvanceHistory
{
    public class AdvanceHistorySelectDTO
    {
        public int ID { get; set; }

        public int? StatusID { get; set; }

        public int? AdvanceID { get; set; }

        public int? TransactorID { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public DateTime? Date { get; set; }

        public virtual EmployeeSelectDTO Transactor { get; set; }
    }
}
