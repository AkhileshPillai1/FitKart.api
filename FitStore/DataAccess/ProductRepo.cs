using FitStore.DBConn;
using FitStore.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FitStore.DataAccess
{
    public class ProductRepo
    {
        //private readonly IMongoDatabase _database;
        //private readonly IMongoCollection<Product> _productsCollection;
        private readonly ApplicationDBContext _dbContext;
        public ProductRepo(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Product> GetAllProducts() {
            return _dbContext.Products.ToList();
        }
        public Product? GetProductById(int id)
        {
            return _dbContext.Products.Find(id);
        }
        public Product CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            return product;
        }
        public bool UpdateProduct(ObjectId id, Product product) {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result = _productsCollection.ReplaceOne(filter, product);
            return true;
        }
    }
}
