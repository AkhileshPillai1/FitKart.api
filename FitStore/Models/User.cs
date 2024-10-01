using MongoDB.Bson;

namespace FitStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string EmailId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int Persona { get; set; }
    }
    public class Address
    {
        public string Title { get; set; } = string.Empty;
        public string Line1 { get; set; } = string.Empty;
        public string Line2 { get; set; } = string.Empty;
        public string Line3 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int ZipCode { get; set; }
        public bool IsDefault { get; set; }
    }
    public class Cart
    {
        public int ProductId { get; set; };
        public int Quantity { get; set; }
    }
}