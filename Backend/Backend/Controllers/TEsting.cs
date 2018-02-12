using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelMate.Models;

namespace NovelMate.Controllers
{
    [Produces("application/json")]
    [Route("api/Testing")]
    public class Testing : Controller
    {
        private readonly NovelMateContext _context;

        public Testing(NovelMateContext context)
        {
            _context = context;
        }

        // GET: api/TEsting
        [HttpGet]
        public IEnumerable<StoryNode> GetStoryNodes()
        {
            return _context.StoryNodes;
        }

        // GET: api/TEsting/5
        [HttpGet("{id}")]
        public IActionResult GetStoryNode([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storyNode = _context.StoryNodes.SingleOrDefault(m => m.Id == id);

            if (storyNode == null)
            {
                return NotFound();
            }

            return Ok(storyNode);
        }

        // PUT: api/TEsting/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoryNode([FromRoute] Guid id, [FromBody] StoryNode storyNode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != storyNode.Id)
            {
                return BadRequest();
            }

            _context.Entry(storyNode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryNodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TEsting
        [HttpPost]
        public IActionResult PostStoryNode([FromBody] StoryNode storyNode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            _context.StoryNodes.Add(storyNode);
            _context.SaveChanges();

            return CreatedAtAction("GetStoryNode", new { id = storyNode.Id }, storyNode);
        }

        // DELETE: api/TEsting/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoryNode([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var storyNode = await _context.StoryNodes.SingleOrDefaultAsync(m => m.Id == id);
            if (storyNode == null)
            {
                return NotFound();
            }

            _context.StoryNodes.Remove(storyNode);
            await _context.SaveChangesAsync();

            return Ok(storyNode);
        }

        private bool StoryNodeExists(Guid id)
        {
            return _context.StoryNodes.Any(e => e.Id == id);
        }
    }
}