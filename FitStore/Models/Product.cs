using MongoDB.Bson;

namespace FitStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DetailedDescription { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int DiscountPercentage { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public int Seller { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Review
    {
        public string Comment { get; set; } = string.Empty;
        public int Stars { get; set; }
        public int UserId { get; set; }
        public DateTime CommentDate { get; set; }
        public bool IsAnonymous { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    public class Star
    {
        public int ProductId { get; set; }
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
    }
}