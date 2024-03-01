using DBExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private AppDbContext _context;
        // Dependency injection of the app context to be able to interface DB
        public ProductsController (AppDbContext context)
        {
            _context = context;
        }

        // Get endpoint with attribute routing to get a list of produts
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        // Attribute routing with a route parameter that needs to be passed into the url
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No Id requested");
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "No product found");
            }
            return StatusCode(StatusCodes.Status200OK, product);
        }

        // Here we are making Put request with route param ID and we are being passed a product through the body in app/json format
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int? id, Product product)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "No Id requested");
            }

            try
            {
                // Check not changing product ID
                // NOTE: the user is passing the ID field in the body in this case
                if (id != product.ProductID)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }

                _context.Products.Update(product);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, product);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Async function as we are saving to the database
        // Returning appropriate status code or 500 on error when trying to add
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created, product);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
                throw;
            }
        }

    }
}
