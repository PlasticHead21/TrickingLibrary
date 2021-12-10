using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Models;
using TrickingLibrary.Data;

namespace TrickingLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class SubmissionsController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public SubmissionsController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult All() => Ok(_ctx.Submissions.ToList());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_ctx.Submissions.FirstOrDefault(t => t.Id.Equals(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Submission submission)
        {
            _ctx.Submissions.Add(submission);
            await _ctx.SaveChangesAsync();
            return Ok(submission);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Submission submission)
        {
            if (submission is null || submission.Id == 0)
            {
                return BadRequest();
            }

            _ctx.Submissions.Add(submission);
            await _ctx.SaveChangesAsync();
            return Ok(submission);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var submission = _ctx.Submissions.FirstOrDefault(t => !t.Deleted && t.Id.Equals(id));

            if (submission is null)
            {
                return NotFound("Submission already deleted.");
            }

            submission.Deleted = true;
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}