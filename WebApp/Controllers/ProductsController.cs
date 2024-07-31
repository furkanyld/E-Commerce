using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApp.API.Models;
using WebApp.API.Abstract;
using WebApp.API.Concrete;
using WebApp.API.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            var createdProduct = await _productsRepository.CreateProduct(product);
            var createdProductDTO = _mapper.Map<ProductDTO>(createdProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProductDTO.Id }, createdProductDTO);
        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var productResult = await _productsRepository.GetProductById(id);

            if (productResult == null)
            {
                return NotFound();
            }

            var productDTO = _mapper.Map<ProductDTO>(productResult);
            return Ok(productDTO);
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productsRepository.GetProducts();
            return Ok(products);
        }

        [HttpPut("ModifyProduct/{id}")]
        public async Task<IActionResult> ModifyProduct(int id, ModifyProductDTO modifyProductDTO)
        {
            var result = await _productsRepository.ModifyProduct(id, modifyProductDTO);
            if (result is NotFoundResult)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productsRepository.DeleteProduct(id);

            if (result is NotFoundResult)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("SearchProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchProducts([FromQuery] string query)
        {
            var productsResult = await _productsRepository.SearchProducts(query);

            if (productsResult is null)
            {
                return NotFound();
            }

            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(productsResult);
            return Ok(productsDTO);
        }
    }
}


/*private readonly ECommerceDbContext _context;
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

[HttpPut("ModifyProduct")]
public async Task<IActionResult> ModifyProduct(int id, ProductDTO productDTO)
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
}*/


