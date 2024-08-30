using Azure.Storage.Files.Shares;
using Microsoft.AspNetCore.WebUtilities;

namespace ST10339549_CLDV6212_POE.Services
{
    public class FileStorageService
    {
        private readonly ShareClient _share;

        public FileStorageService(string connectionString)
        {
            _share = new ShareClient(connectionString, "contracts");
            _share.CreateIfNotExists();
        }

        public async Task UploadAsync(IFormFile formFile)
        {
            var directory = _share.GetRootDirectoryClient();
            var fileClient = directory.GetFileClient(formFile.FileName);

            using var readStream = formFile.OpenReadStream();

            // Create the file on Azure Files with the length of the stream
            await fileClient.CreateAsync(readStream.Length);

            // Upload the file in chunks if necessary; otherwise, use a single call if the file is small.
            await fileClient.UploadAsync(readStream); // Upload the stream directly now that the file is created.
        }

        public async Task<List<string>> FilesAsync()
        {
            var files = new List<string>();
            var directory = _share.GetRootDirectoryClient();
            await foreach (var item in directory.GetFilesAndDirectoriesAsync())
            {
                // Generate the full URL to the file for display purposes
                var fileUrl = $"{_share.Uri}/{item.Name}";
                files.Add(fileUrl);
            }
            return files;
        }
    }
}
