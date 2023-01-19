using API.Repositories;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private static readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=lichvablob;AccountKey=GViAL6dnsvV4lqPjCtyKmZS+cftwG9101FzxPoeTdCu43wczTumxr9CE5IZZSIsGf5uomIEmJID7+AStik1orQ==;EndpointSuffix=core.windows.net";

        private IBankRepository Repository { get; set; }
        public DocumentController(IBankRepository repo)
        {
            Repository = repo;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadFile(
            [FromHeader] string authToken,
            IList<IFormFile> files
            )
        {
            try
            {
                await Repository.AuthenticateUserAsync(authToken);

                BlobContainerClient client = new BlobContainerClient(_connectionString, "lichvapdf");
                foreach (var file in files)
                {
                    using MemoryStream stream = new();
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                    await client.UploadBlobAsync(file.FileName, stream);
                }
                return Ok("Successfully uploaded files");
            }
            catch ( Exception ex )
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
