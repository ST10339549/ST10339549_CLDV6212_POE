using Microsoft.Azure.Cosmos.Table;
using System.ComponentModel.DataAnnotations;

namespace ST10339549_CLDV6212_POE.Models
{
    public class Product : TableEntity
    {
        private string _productId;

        [Required]
        public string? ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                RowKey = _productId ?? Guid.NewGuid().ToString(); // Ensures RowKey is set when ProductId is set
            }
        }

        [Required]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        [Required]
        public double ProductPrice { get; set; }

        public Product()
        {
            PartitionKey = "Product";
            RowKey = Guid.NewGuid().ToString(); // Ensures RowKey is initialized
        }
    }
}
