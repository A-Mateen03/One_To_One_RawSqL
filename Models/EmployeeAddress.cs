using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace One_To_One_RawSqL.Models
{
    public class EmployeeAddress
    {
        [Key, ForeignKey("Employee")]
        public int EmployeeID { get; set; }
        public required string Address { get; set; }
        //
        //Navigation property Returns the Employee object
        public virtual Employee? Employee { get; set; }
    }
}
