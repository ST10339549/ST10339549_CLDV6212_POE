using Microsoft.AspNetCore.Mvc;
using ST10339549_CLDV6212_POE.Services;

namespace ST10339549_CLDV6212_POE.Controllers
{
    public class OrderProcessingController : Controller
    {
        private readonly QueueStorageService _queueService;

        public OrderProcessingController()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=st10339549;AccountKey=HCmh5cT2Fn6cnXsTna69QdswQCunUPD06UBscauSGoKgAlGTm4Vbsa9zpuQUQoR7lRKEuM1WFdId+AStcM6ObQ==;EndpointSuffix=core.windows.net";
        }
        public async Task<IActionResult> Index()
        {
            var messages = await _queueService.GetMessagesAsync();
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> OrderProcessing(string order)
        {
            if (!string.IsNullOrEmpty(order))
            {
                await _queueService.AddMessageAsync(order);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
