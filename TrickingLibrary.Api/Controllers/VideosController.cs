using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace TrickingLibrary.Api.Controllers
{
    [Route("api/{controller}")]
    public class VideosController: ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public VideosController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("{videoName}")]
        public IActionResult GetVideo(string videoName)
        {
            var path = Path.Combine(_env.WebRootPath, videoName);
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var result = new FileStreamResult(fileStream, "video/*");
            return result;
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadVideo(IFormFile video)
        {
            var mime = video.FileName.Split('.').Last();
            var fileName = string.Concat(Path.GetRandomFileName(),".", mime);
            var savePath = Path.Combine(_env.WebRootPath, fileName);

            await using (var fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                await video.CopyToAsync(fileStream);
            }
            
            return Ok(fileName);
        }
    }
}