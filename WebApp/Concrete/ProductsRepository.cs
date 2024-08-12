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

        public async Task<API.Models.Product> CreateProduct(Product product)
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

        public async Task<ModifyProductDTO> ModifyProduct(int id, ModifyProductDTO modifyProductDTO)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                throw new Exception();
            }

            _mapper.Map(modifyProductDTO, existingProduct);
            await _context.SaveChangesAsync();

            return modifyProductDTO;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<Product> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
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

