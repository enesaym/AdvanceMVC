using AdvanceUI.Models.DTO.Advance;
using AdvanceUI.Models.DTO.Employee;
using AdvanceUI.Models.DTO.Status;
using System;

namespace AdvanceUI.Models.DTO.AdvanceHistory
{
    public class AdvanceHistorySelectDTO
    {
        public int ID { get; set; }

        public int? StatusID { get; set; }

        public int? AdvanceID { get; set; }


        public EmployeeSelectDTO AfterEmployee { get; set; }

        public StatusSelectDTO AfterStatus { get; set; }

        public int? TransactorID { get; set; }

        public decimal? ApprovedAmount { get; set; }

        public DateTime? Date { get; set; }

        public StatusSelectDTO Status { get; set; }

        public virtual EmployeeSelectDTO Transactor { get; set; }

        public virtual AdvanceSelectDTO Advance { get; set; }
    }
}
