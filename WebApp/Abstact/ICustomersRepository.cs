using Microsoft.AspNetCore.Mvc;
using WebApp.API.Models;

namespace WebApp.API.Abstract
{
    public interface ICustomersRepository
    {
        IActionResult GenerateCustomers(int number);
        Task<ActionResult<Customer>> CreateCustomer(Customer customer);
        IActionResult DeleteCustomer(int id);
        Task<IActionResult> DeleteAllCustomers();
        Task<List<API.Models.Customer>> GetCustomers();
    }
}
