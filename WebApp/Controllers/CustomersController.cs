using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        public CustomersController(ECommerceDbContext context)
        {
            _context = context;
        }
        //private List<Customer> _customers = FakeData.FakeData.GetCustomers(200);
        [HttpPost("GenerateCustomers")]
        public IActionResult GenerateCustomers(int number)
        {
            List<Customer> _customers = new List<Customer>();
            _customers = new Faker<Customer>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .Generate(number);

            _customers.ForEach(c => _context.Customers.Add(c));
            _context.SaveChanges();

            return Ok(_customers);
        }

        [HttpPost("AddCustomer")]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateCustomer), new { id = customer.Id }, customer);
        }

        [HttpDelete("DeleteCostumer")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with Id = {id} not found." });
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return Ok(new { Message = $"Customer with Id = {id} has been deleted." });
        }

        [HttpDelete("DeleteAllCustomers")]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            if (customers == null || customers.Count == 0)
            {
                return NotFound(new { Message = "No customers found to delete." });
            }

            _context.Customers.RemoveRange(customers);
            await _context.SaveChangesAsync();

            // Id reset
            await _context.Database.ExecuteSqlRawAsync("DBCC CHECKIDENT ('Customers', RESEED, 0)");

            return Ok(new { Message = "All customers have been deleted." });
        }

        [HttpGet("GetCustomers")]
        public async Task<List<Models.Customer>> GetCustomers()
        {
            var customerList = _context.Customers.ToListAsync();
            return await customerList;
        }

    }
}
