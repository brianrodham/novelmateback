using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovelMate.Models;
using NovelMate.Objects;

namespace NovelMate.Controllers
{
    [Produces("application/json")]
    [Route("api/StoryNodes")]
    public class StoryNodesController : Controller
    {
        private readonly NovelMateContext _context;

        public StoryNodesController(NovelMateContext context)
        {
            _context = context;
        }

        // GET: api/StoryNodes
        [HttpGet]
        public IEnumerable<StoryNode> GetStoryNodes()
        {
            return _context.StoryNodes;
        }

        // GET: api/StoryNodes/[nodeId]
        [HttpGet("{userId}")]
        public IActionResult GetStoryNode([FromRoute] Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<SimpleNode> nodes = new List<SimpleNode>();

            foreach (StoryNode node in _context.StoryNodes)
            {
                if (node.UserId == userId)
                {
                    nodes.Add(new SimpleNode(node.Id, node.Name));
                }
            }

            return Ok(nodes);
        }

        // GET: api/StoryNodes/[userId]/[nodeId]
        [HttpGet("{userId}/{nodeId}")]
        public IActionResult GetStoryNode([FromRoute] Guid userId, Guid nodeId)
        {
            StoryNode node = _context.StoryNodes.SingleOrDefault(item => item.UserId == userId && item.Id == nodeId);
            return Ok(node);
        }

        // PUT: api/StoryNodes/5
        [HttpPut("{id}")]
        public IActionResult PutStoryNode([FromRoute] Guid id, [FromBody] StoryNode storyNode)
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
                _context.SaveChangesAsync();
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

        // POST: api/StoryNodes
        [HttpPost]
        public IActionResult PostStoryNode([FromBody] SimpleDataNode storyNode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Populate node data
            StoryNode node = new StoryNode();
            node.Id = Guid.NewGuid();
            node.UserId = storyNode.UserID;
            node.Name = storyNode.Name;
            node.Content = storyNode.Content;
            node.CreatedAt = DateTime.Now;
            node.ModifiedAt = DateTime.Now;

            // Add the new node to the array
            _context.StoryNodes.Add(node);
            _context.SaveChanges();

            return Ok(node);
            //return CreatedAtAction("GetStoryNode", new { id = storyNode.Id }, storyNode);
        }

        // DELETE: api/StoryNodes/[userId]/[nodeId]
        [HttpDelete("{userId}/{nodeId}")]
        public IActionResult DeleteStoryNode([FromRoute] Guid userId, Guid nodeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            StoryNode storyNode = _context.StoryNodes.SingleOrDefault(item => item.UserId == userId && item.Id == nodeId);
            if (storyNode == null)
            {
                return NotFound();
            }

            _context.StoryNodes.Remove(storyNode);
            _context.SaveChanges();

            return Ok(storyNode);
        }

        private bool StoryNodeExists(Guid id)
        {
            return _context.StoryNodes.Any(e => e.Id == id);
        }
    }
}