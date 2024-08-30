using Azure.Storage.Queues;

namespace ST10339549_CLDV6212_POE.Services
{
    public class QueueStorageService
    {
        private readonly QueueClient _queueClient;

        public QueueStorageService(string connectionString)
        {
            _queueClient = new QueueClient(connectionString, "orders");
            _queueClient.CreateIfNotExists();
        }

        public async Task AddMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }

        public async Task<List<string>> GetMessagesAsync()
        {
            var messages = new List<string>();
            var response = await _queueClient.ReceiveMessagesAsync(maxMessages: 5);
            foreach (var msg in response.Value)
            {
                messages.Add(msg.MessageText);
                await _queueClient.DeleteMessageAsync(msg.MessageId, msg.PopReceipt);
            }
            return messages;
        }
    }
}
