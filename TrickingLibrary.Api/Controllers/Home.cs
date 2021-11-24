using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TrickingLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Home : ControllerBase
    {
        [HttpGet]
        public async Task<string> Index()
        {
            return "Hello Dude";
        }
    }
}