using FitStore.DataAccess;
using FitStore.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;


namespace FitStore.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly ProductRepo _productRepo;
        public ProductController(ProductRepo productRepo) {
            _productRepo = productRepo;
        }
        [HttpGet]
        public IActionResult GetProducts([FromQuery]string category="", [FromQuery] double discount = 0, [FromQuery] double rating = 0, [FromQuery] string sort = "") {
            var filters = new List<FilterDefinition<Product>>();
            SortDefinition<Product>? sortFilters = null;
            if (!String.IsNullOrWhiteSpace(category)) { 
                filters.Add(Builders<Product>.Filter.Eq("category",category));
            }
            if (discount != 0)
            {
                filters.Add(Builders<Product>.Filter.Gte("discountPercentage", discount));
            }
            if (rating != 0)
            {
                filters.Add(Builders<Product>.Filter.Gte("rating", rating));
            }
            if (!String.IsNullOrWhiteSpace(sort))
            {
                var field = sort.Split('#')[0];
                var direction = sort.Split('#')[1];
                if (direction == "asc")
                {
                    sortFilters = Builders<Product>.Sort.Ascending(field);
                }
                else if (direction == "desc")
                {
                    sortFilters = Builders<Product>.Sort.Descending(field);
                }
            }
            var combinedFilter = filters.Count > 0 ? Builders<Product>.Filter.And(filters) : FilterDefinition<Product>.Empty;
            var products = _productRepo.GetProductsByFilter(combinedFilter, sortFilters);
            return Ok(new GenericResponse()
            {
                isSuccess = true,
                data = products
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetProduct(string id)
        {
            try
            {
                ObjectId _id = ObjectId.Parse(id);
                var product = _productRepo.GetProductById(_id);
                if (product == null) {
                    return NotFound(new GenericResponse()
                    {
                        isSuccess = true,
                        errorMessage = "Product not found!"
                    });
                }
                return Ok(new GenericResponse()
                {
                    isSuccess = true,
                    data = product
                });
            }
            catch (Exception ex) {
                return BadRequest(new GenericResponse()
                {
                    isSuccess = true,
                    errorMessage = ex.Message
                });
            }
        }
        [HttpPost("create")]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(product.ProductName) || String.IsNullOrWhiteSpace(product.Seller) || String.IsNullOrWhiteSpace(product.Category))
                {
                    return BadRequest(new GenericResponse()
                    {
                        isSuccess = false,
                        errorMessage = "Please enter all mandatory fields!"
                    });
                }
                if (!String.IsNullOrWhiteSpace(product.ProductCode))
                {
                    var filter = Builders<Product>.Filter.And(Builders<Product>.Filter.Eq("seller", product.Seller), Builders<Product>.Filter.Eq("productCode", product.ProductCode));
                    List<Product> duplicateProducts = _productRepo.GetProductsByFilter(filter);
                    if (duplicateProducts.Count > 0)
                    {
                        return BadRequest(new GenericResponse()
                        {
                            isSuccess = false,
                            errorMessage = "Please enter a unique product code!"
                        });
                    }
                }

                var createdProduct = _productRepo.CreateProduct(product);
                return Ok(new GenericResponse()
                {
                    isSuccess = true,
                    data = createdProduct
                });
            }
            catch (Exception ex) {
                return StatusCode(500,new GenericResponse()
                {
                    isSuccess = true,
                    errorMessage = ex.Message
                });
            }
        }
    }
}
