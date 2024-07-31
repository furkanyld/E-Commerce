using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTOs;
using WebApp.API.Models;

namespace WebApp.API.Abstract
{
    public interface ICustomersRepository
    {
        Task<IActionResult> GenerateCustomers(int number);
        Task<Customer> CreateCustomer(Customer customer);
        Task<IActionResult> DeleteCustomer(int id);
        Task<IActionResult> DeleteAllCustomers();
        Task<List<API.Models.Customer>> GetCustomers();
        Task<IActionResult> ModifyCustomer(int id, CustomerDTO customerDTO);
    }
}
