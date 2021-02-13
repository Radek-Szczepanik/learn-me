using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;


namespace LearnMe.Controllers.Files
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpGet("{fileString}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Download(string fileString)
        {
            var folderName = Path.Combine("wwwroot", "Homeworks");
            var pathToFileOnServer = Path.Combine(Directory.GetCurrentDirectory(), folderName, fileString);

            var file = System.IO.File.OpenRead(pathToFileOnServer);

            if (file == null)
                return NotFound();

            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileString,
                Inline = false
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());

            return File(file, "application/octet-stream"); // returns a FileStreamResult
        }
    }
}