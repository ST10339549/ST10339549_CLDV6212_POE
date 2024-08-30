using Microsoft.AspNetCore.Mvc;
using ST10339549_CLDV6212_POE.Services;

namespace ST10339549_CLDV6212_POE.Controllers
{
    public class OrderProcessingController : Controller
    {
        private readonly QueueStorageService _queueService;

        public OrderProcessingController()
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=st10339549;AccountKey=2r3eN6egjj4zNt9nF8Bw2zMs7XwNBGnPcCiTgJG1jtDfATA+SeE8xYjqgCEdyFy9XMNHTiV1NPJw+AStGagjiw==;EndpointSuffix=core.windows.net";
            _queueService = new QueueStorageService(connectionString);

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
