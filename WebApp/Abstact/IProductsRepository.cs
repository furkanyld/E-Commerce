using Microsoft.AspNetCore.Mvc;
using WebApp.API.DTOs;
using WebApp.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.API.Abstract
{
    public interface IProductsRepository
    {
        Task<Product> CreateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts();
        Task<IActionResult> ModifyProduct(int id, ModifyProductDTO modifyProductDTO);
        Task<IActionResult> DeleteProduct(int id);
        Task<IEnumerable<Product>> SearchProducts([FromQuery] string query);
    }
}

