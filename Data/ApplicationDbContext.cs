using Microsoft.EntityFrameworkCore;
using One_To_One_RawSqL.Models;

namespace One_To_One_RawSqL.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddress { get; set; }

    }
}
