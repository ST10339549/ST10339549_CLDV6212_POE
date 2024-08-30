using Azure.Storage.Files.Shares;

namespace ST10339549_CLDV6212_POE.Services
{
    public class FileStorageService
    {
        private readonly ShareClient _shareClient;

        public FileStorageService(string connectionString)
        {
            _shareClient = new ShareClient(connectionString, "contracts");
            _shareClient.CreateIfNotExists();
        }

        public async Task UploadFileAsync(IFormFile file)
        {
            var directoryClient = _shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(file.FileName);
            using var stream = file.OpenReadStream();
            await fileClient.CreateAsync(stream.Length);
            await fileClient.UploadAsync(stream);
        }

        public async Task<List<string>> ListFilesAsync()
        {
            var files = new List<string>();
            var directoryClient = _shareClient.GetRootDirectoryClient();
            await foreach (var item in directoryClient.GetFilesAndDirectoriesAsync())
            {
                files.Add(item.Name);
            }
            return files;
        }
    }
}
