using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.API.Abstract;
using WebApp.API.Models;
using WebApp.API.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.Concrete
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;

        public ProductsRepository(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<API.Models.Product> CreateProduct(API.Models.Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<API.Models.Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return null;
            }
            return product;
        }


        public async Task<List<API.Models.Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IActionResult> ModifyProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return new BadRequestObjectResult("Product ID mismatch.");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(productDTO, existingProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }
            return new NoContentResult();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return new NotFoundResult();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        public async Task<IEnumerable<API.Models.Product>> SearchProducts([FromQuery] string query)
        {
            var products = await _context.Products
                                          .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                                          .ToListAsync();
            return products;
        }
    }
}

