


using Coffee.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Coffee.Data
{
    public class SolarDbContext : IdentityDbContext 
    {
        public SolarDbContext()
        {

        }

        public SolarDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Customer> Customers { get; set; }
    }
}
