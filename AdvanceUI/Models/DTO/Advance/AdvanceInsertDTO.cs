using System;

namespace AdvanceUI.Models.DTO.Advance
{
    public class AdvanceInsertDTO
    {
        public decimal? AdvanceAmount { get; set; }

        public string AdvanceDescription { get; set; }

        public int? ProjectID { get; set; }

        public DateTime? DesiredDate { get; set; }

        public int? StatusID { get; set; }

        public DateTime? RequestDate { get; set; }

        public int? EmployeeID { get; set; }
    }
}
