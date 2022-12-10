using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCommentController : ControllerBase
    {
        private readonly IPostCommentRepository _postCommentRepository;
        public PostCommentController(IPostCommentRepository postCommentRepository)
        {
            _postCommentRepository = postCommentRepository;
        }

        // GET: api/<PostCommentController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postCommentRepository.GetAllOrderedByCommentDate());
        }

        // GET api/<PostCommentController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var postLike = _postCommentRepository.GetById(id);
            if (postLike == null)
            {
                return NotFound();
            }
            return Ok(postLike);
        }

        // POST api/<PostCommentController>
        [HttpPost]
        public IActionResult Post(PostComment postComment)
        {
            _postCommentRepository.Add(postComment);
            return CreatedAtAction("Get", new { id = postComment.Id }, postComment);
        }

        // PUT api/<PostCommentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PostComment postComment)
        {
            if (id != postComment.Id)
            {
                return BadRequest();
            }
            _postCommentRepository.Update(postComment);
            return NoContent();
        }

        // DELETE api/<PostCommentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postCommentRepository.Delete(id);
            return NoContent();
        }
    }
}
