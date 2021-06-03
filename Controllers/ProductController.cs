using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ApiProduct.Models;
using ApiProductServices.Services;

namespace ApiProduct.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase {
        public ProductController(){
        }

        // GET all action
        [HttpGet]
        public ActionResult<List<Product>> GetAll() => ProductService.GetAll();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id){
            var product = ProductService.Get(id);

            if(product == null)
                return NotFound();

            return product;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Product product){            
            // This code will save the product and return a result
            ProductService.Add(product);
            return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
        }

        // PUT action
        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product){
            // This code will update the product and return a result
            if (id != product.Id)
                return BadRequest();

            var existingProduct= ProductService.Get(id);
            if(existingProduct is null)
                return NotFound();

            ProductService.Update(product);           

            return NoContent();
        }

        // DELETE action
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            // This code will delete the product and return a result
            var product = ProductService.Get(id);

            if (product is null)
                return NotFound();

            ProductService.Delete(id);

            return NoContent();
        }
    }
}