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
            return Ok(result);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _productsRepository.DeleteProduct(id);
            return Ok(deletedProduct);
        }

        [HttpGet("SearchProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> SearchProducts([FromQuery] string query)
        {
            var productsResult = await _productsRepository.SearchProducts(query);
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(productsResult);
            return Ok(productsDTO);
        }
    }
}
