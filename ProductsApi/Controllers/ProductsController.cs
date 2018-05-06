using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET api/Products
        [HttpGet]
        public Task<IEnumerable<Product>> Get()
        {
            return _productRepository.GetAllProducts();
        }

        // GET api/Products/name
        [HttpGet("{id:int}")]
        public async Task<Product> Get(int id)
        {
            return await _productRepository.GetProductsById(id);
        }

        // GET api/Products/name
        [HttpGet("{name}")]
        public async Task<Product> Get(string name)
        {
            return await _productRepository.GetProducts(name);
        }

        [HttpGet("price/max")]
        public async Task<Product> GetByMaxPrice()
        {

            return await _productRepository.GetProductByMaxPrice();
        }

        [HttpGet("price/min")]
        public async Task<Product> GetByMinPrice()
        {
            return await _productRepository.GetProductByMinPrice();
        }

        [HttpGet("fantastic/{value}/{type}/{name}")]
        public async Task<IEnumerable<Product>> GetByFantastic(bool value, int type, string name)
        {
            return await _productRepository.GetProductByFantastic(value, type, name);
        }

        [HttpGet("rating/max")]
        public async Task<Product> GetByMaxRating()
        {
            return await _productRepository.GetProductByMaxRating();
        }

        [HttpGet("rating/min")]
        public async Task<Product> GetByMinRating()
        {
            return await _productRepository.GetProductByMinRating();
        }
        // POST api/Products
        [HttpPost]
        public async void Post([FromBody]Product product)
        {
            await _productRepository.Create(product);
        }

        // PUT api/Products/5
        [HttpPut("{id}")]
        public async void Put(int id, [FromBody]Product product)
        {
            await _productRepository.Update(product);
        }

        // DELETE api/Products/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            await _productRepository.Delete(id);
        }
    }
}
