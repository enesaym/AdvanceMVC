using AdvanceUI.Models.DTO.BusinessUnit;
using AdvanceUI.Models.DTO.Title;

namespace AdvanceUI.Models.DTO.Employee
{
    public class EmployeeSelectDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int? BusinessUnitID { get; set; }

        public TitleSelectDTO Title { get; set; }

        public BusinessUnitSelectDTO BusinessUnit { get; set; }

        public int TitleID { get; set; }

        public string TitleName { get; set; }

    }
}
