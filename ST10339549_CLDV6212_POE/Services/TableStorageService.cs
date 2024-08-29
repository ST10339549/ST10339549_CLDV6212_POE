using Microsoft.Azure.Cosmos.Table;
using ST10339549_CLDV6212_POE.Models;

namespace ST10339549_CLDV6212_POE.Services
{
  public class TableStorageService
  {
    private readonly CloudTableClient _tableClient;
    private readonly CloudTable _customerTable;
    private readonly CloudTable _productTable;

    public TableStorageService(string connectionString)
    {
      CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
      _tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

      _customerTable = _tableClient.GetTableReference("Customers");
      _productTable = _tableClient.GetTableReference("Products");

      _customerTable.CreateIfNotExists();
      _productTable.CreateIfNotExists();
    }

    public async Task AddCustomerAsync(Customer customer)
    {
      var insertOperation = TableOperation.Insert(customer);
      await _customerTable.ExecuteAsync(insertOperation);
    }

    public async Task AddProductAsync(Product product)
    {
      var insertOperation = TableOperation.Insert(product);
      await _productTable.ExecuteAsync(insertOperation);
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
      var query = new TableQuery<Customer>();
      var customerTable = await _customerTable.ExecuteQuerySegmentedAsync(query, null);
      return customerTable.Results;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
      var query = new TableQuery<Product>();
      var productTable = await _customerTable.ExecuteQuerySegmentedAsync(query, null);
      return productTable.Results;
    }
  }
}
