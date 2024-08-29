using Microsoft.Azure.Cosmos.Table;

namespace ST10339549_CLDV6212_POE.Models
{
  public class Product : TableEntity
  {
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public decimal ProductPrice { get; set; }
    public Product()
    {
      PartitionKey = "Product";
      RowKey = ProductId ?? Guid.NewGuid().ToString();
    }
  }
}
