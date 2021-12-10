using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrickingLibrary.Api.Models;
using TrickingLibrary.Data;

namespace TrickingLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class TricksController : ControllerBase
    {
        private readonly AppDbContext _ctx;

        public TricksController(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult All() => Ok(_ctx.Tricks.ToList());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(_ctx.Tricks.FirstOrDefault(t => t.Id.Equals(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Trick trick)
        {
            _ctx.Tricks.Add(trick);
            await _ctx.SaveChangesAsync();
            return Ok(trick);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Trick trick)
        {
            if (trick is null || trick.Id == 0)
            {
                return BadRequest();
            }

            _ctx.Tricks.Add(trick);
            await _ctx.SaveChangesAsync();
            return Ok(trick);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var trick = _ctx.Tricks.FirstOrDefault(t => !t.Deleted && t.Id.Equals(id));

            if (trick is null)
            {
                return BadRequest("Trick already deleted.");
            }

            trick.Deleted = true;
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{trickId}/submissions")]
        public IEnumerable<Submission> ListSubmissionsForTrick(int trickId) =>
            _ctx.Submissions.Where(s => s.TrickId.Equals(trickId)).ToList();
    }
}