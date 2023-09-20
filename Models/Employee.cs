using System.ComponentModel.DataAnnotations;

namespace One_To_One_RawSqL.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }

        //Navigation property Returns the Employee Address
        public virtual EmployeeAddress? EmployeeAddress { get; set; }
    }
}
