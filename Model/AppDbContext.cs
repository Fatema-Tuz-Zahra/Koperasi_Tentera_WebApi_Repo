using Microsoft.EntityFrameworkCore;

namespace Koperasi_Tentera_WebApi.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
