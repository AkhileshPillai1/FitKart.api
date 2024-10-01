using Newtonsoft.Json.Linq;

namespace FitStore.Models
{
    public class GenericResponse
    {
        public bool isSuccess { get; set; }
        public string errorMessage { get; set; } = string.Empty;
        public object? data { get; set; }
    }
}
