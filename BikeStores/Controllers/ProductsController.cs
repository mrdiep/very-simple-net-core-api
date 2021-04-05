using BikeStores.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BikeStores.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly BikeStoresContext context;

        public ProductsController(BikeStoresContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Product> Get(int pageIndex = 0, int pageSize = 10)
        {
            return context.Products.Skip(pageIndex * pageSize).Take(pageSize);
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return context.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }

        [HttpPut]
        [Route("{id}")]
        public bool Update(int id, [FromBody]Product product)
        {
            var existsProduct = context.Products.FirstOrDefault(x => x.ProductId == id);
            if (existsProduct == null) return false;
            existsProduct.ProductName = product.ProductName;

            context.SaveChanges();

            return true;
        }

        [HttpPost]
        public int Create([FromBody] Product product)
        {
            product.ProductId = 0;
            context.Products.Add(product);
            context.SaveChanges();

            return product.ProductId;
        }
    }
}
