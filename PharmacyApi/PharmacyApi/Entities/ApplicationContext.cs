using Microsoft.EntityFrameworkCore;

namespace PharmacyApi.Entities
{
    public class ApplicationContext:DbContext
    {      
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //bağlanmasını sağlamak.
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Staff> Staffs { get; set; }
    }
}
