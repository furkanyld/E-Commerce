using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.API.Models;
using WebApp.API.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApp.API.Concrete
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly ECommerceDbContext _context;

        public CustomersRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IActionResult> DeleteAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null || customers.Count == 0)
            {
                return new NotFoundObjectResult(new { Message = "No customers found to delete." });
            }

            _context.Customers.RemoveRange(customers);
            await _context.SaveChangesAsync();

            // Id reset
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Customers', RESEED, 0)");

            return new OkObjectResult(new { Message = "All customers have been deleted." });
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return new NotFoundObjectResult(new { Message = $"Customer with Id = {id} not found." });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new { Message = $"Customer with Id = {id} has been deleted." });
        }

        public async Task<IActionResult> GenerateCustomers(int number)
        {
            List<Customer> _customers = new Faker<Customer>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .Generate(number);

            _customers.ForEach(c => _context.Customers.AddAsync(c));
            await _context.SaveChangesAsync();

            return new OkObjectResult(_customers);
        }

        public async Task<List<API.Models.Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
