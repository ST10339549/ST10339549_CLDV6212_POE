using Microsoft.AspNetCore.Mvc;
using ST10339549_CLDV6212_POE.Models;
using ST10339549_CLDV6212_POE.Services;

namespace ST10339549_CLDV6212_POE.Controllers
{
  public class ProductController : Controller
  {
    private readonly TableStorageService _tableStorageService;

    public ProductController()
    {
      string connectionString = "DefaultEndpointsProtocol=https;AccountName=st10339549;AccountKey=HCmh5cT2Fn6cnXsTna69QdswQCunUPD06UBscauSGoKgAlGTm4Vbsa9zpuQUQoR7lRKEuM1WFdId+AStcM6ObQ==;EndpointSuffix=core.windows.net";
      _tableStorageService = new TableStorageService(connectionString);
    }

    public async Task<IActionResult> Index()
    {
      var products = await _tableStorageService.GetProductsAsync();
      return View(products);
    }

    public IActionResult Create() => View();

    public async Task<IActionResult> Create(Product product)
    {
      if (ModelState.IsValid)
      {
        await _tableStorageService.AddProductAsync(product);
        return RedirectToAction(nameof(Index));
      }
      return View(product);
    }
  }
}
