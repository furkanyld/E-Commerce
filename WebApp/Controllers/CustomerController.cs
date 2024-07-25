using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        public CustomerController(ECommerceDbContext context)
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
        //[HttpGet("GetCustomers")]
       
    }
}
