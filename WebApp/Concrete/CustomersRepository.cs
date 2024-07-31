﻿using Bogus;
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

        //add değil addasync
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return new CreatedAtActionResult(nameof(CreateCustomer), "Customers", new { id = customer.Id }, customer);
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

        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return new NotFoundObjectResult(new { Message = $"Customer with Id = {id} not found." });
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return new OkObjectResult(new { Message = $"Customer with Id = {id} has been deleted." });
        }

        public IActionResult GenerateCustomers(int number)
        {
            List<Customer> _customers = new Faker<Customer>()
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .Generate(number);

            _customers.ForEach(c => _context.Customers.Add(c));
            _context.SaveChanges();

            return new OkObjectResult(_customers);
        }

        public async Task<List<API.Models.Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
    }
}
