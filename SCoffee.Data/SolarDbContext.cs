

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SCoffee.Data.Models;

namespace SCoffee.Data
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
