using Microsoft.EntityFrameworkCore;

namespace WebApp.Models;

public class ECommerceDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
    {
    }
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public float Price { get; set; }
    public decimal Discount { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Manufacturer { get; set; }

    public Product(string name, int quantity, float price, decimal discount, string description, string category, string manufacturer)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Discount = discount;
        Description = description;
        Category = category;
        Manufacturer = manufacturer;
    }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Customer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public Customer() { }

}



