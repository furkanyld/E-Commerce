using Microsoft.EntityFrameworkCore;

namespace WebApp.API.Models;

public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
    { 
    }
}



