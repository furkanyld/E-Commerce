using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApp.Models;
using WebApp.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ECommerceDbContext _context;
        private readonly IMapper _mapper;


        public ProductsController(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<Models.Product>> CreateProduct(Models.Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpGet("GetProductById")]
        public async Task<ActionResult<Models.Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            //var product1= await _context.Products
            //.Where(x=>x.Id == id)
            //.FirstOrDefaultAsync();

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("GetProducts")]
        public async Task<List<Models.Product>> GetProducts()
        {
            var productList = _context.Products.ToListAsync();
            return await productList;
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Search Product")]
        public async Task<ActionResult<IEnumerable<Models.Product>>> SearchProducts([FromQuery] string query)
        {
            var products = await _context.Products
                                          .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                                          .ToListAsync();
            return Ok(products);
        }
        [HttpPut("ProductModify")]
        public async Task<IActionResult> ProductModify(int id, ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (productDTO.Name != null)
            {
                existingProduct.Name = productDTO.Name;
            }
            if (productDTO.Price != 0)
            {
                existingProduct.Price = productDTO.Price;
            }
            if (productDTO.Quantity != 0)
            {
                existingProduct.Quantity = productDTO.Quantity;
            }
            if (productDTO.Discount != 0)
            {
                existingProduct.Discount = productDTO.Discount;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

    }
}


