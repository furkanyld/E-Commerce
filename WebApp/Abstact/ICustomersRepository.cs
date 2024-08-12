using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTOs;
using WebApp.API.Models;

namespace WebApp.API.Abstract
{
    public interface ICustomersRepository
    {
        Task<List<Customer>> GenerateCustomers(int number);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> DeleteCustomer(int id);
        Task<IActionResult> DeleteAllCustomers();
        Task<List<API.Models.Customer>> GetCustomers();
        Task<Customer> ModifyCustomer(int id, CustomerDTO customerDTO);
    }
}
