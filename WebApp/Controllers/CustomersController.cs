using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.API.Abstract;
using WebApp.API.Concrete;
using WebApp.API.DTOs;
using WebApp.API.Models;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;
        public CustomersController(ECommerceDbContext context, ICustomersRepository customerRepository, IMapper mapper)
        {
            _context = context;
            _customersRepository = customerRepository;
            _mapper = mapper;
        }
        
        [HttpPost("GenerateCustomers")]
        public async Task<IActionResult> GenerateCustomers(int number)
        {
            var customers = await _customersRepository.GenerateCustomers(number);
            return Ok(customers);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            await _customersRepository.CreateCustomer(customer);
            return Ok(customer);
        }

        [HttpDelete("DeleteCostumer")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deletedCustomer = await _customersRepository.DeleteCustomer(id);
            return Ok(deletedCustomer);
        }

        [HttpDelete("DeleteAllCustomers")]
        public async Task<IActionResult> DeleteAllCustomers()
        {
            await _customersRepository.DeleteAllCustomers();
            return Ok(new { Message = "All customers have been deleted." });
        }

        [HttpGet("GetCustomers")]
        public async Task<List<Models.Customer>> GetCustomers()
        {
            var customerList = _customersRepository.GetCustomers();
            return await customerList;
        }

        [HttpPut("ModifyCustomer/{id}")]
        public async Task<IActionResult> ModifyCustomer(int id, CustomerDTO customerDTO)
        {
            var result = await _customersRepository.ModifyCustomer(id, customerDTO);
            return Ok(result);
        }
    }
}
