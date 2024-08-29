using Microsoft.AspNetCore.Mvc;
using ST10339549_CLDV6212_POE.Models;
using ST10339549_CLDV6212_POE.Services;

namespace ST10339549_CLDV6212_POE.Controllers
{
  public class CustomerController : Controller
  {
    private readonly TableStorageService _tableStorageService; 

    public CustomerController()
    {
      string connectionString = "DefaultEndpointsProtocol=https;AccountName=st10339549;AccountKey=HCmh5cT2Fn6cnXsTna69QdswQCunUPD06UBscauSGoKgAlGTm4Vbsa9zpuQUQoR7lRKEuM1WFdId+AStcM6ObQ==;EndpointSuffix=core.windows.net";
      _tableStorageService = new TableStorageService(connectionString);
    }

    public async Task<IActionResult> Index()
    {
      var customers = await _tableStorageService.GetCustomersAsync();
      return View(customers);
    }

    public IActionResult Create() => View();

    public async Task<IActionResult> Create(Customer customer)
    {
      if (ModelState.IsValid)
      {
        await _tableStorageService.AddCustomerAsync(customer);
        return RedirectToAction(nameof(Index));
      }
      return View(customer);
    }
  }
}
