using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostLikeController : ControllerBase
    {
        private readonly IPostLikeRepository _postLikeRepository;
        public PostLikeController(IPostLikeRepository postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }

        // GET: api/<PostLikeController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postLikeRepository.GetAllOrderedByLikedDate());
        }

        // GET api/<PostLikeController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var postLike = _postLikeRepository.GetById(id);
            if (postLike == null)
            {
                return NotFound();
            }
            return Ok(postLike);
        }

        // POST api/<PostLikeController>
        [HttpPost]
        public IActionResult Post(PostLike postLike)
        {
            _postLikeRepository.Add(postLike);
            return CreatedAtAction("Get", new { id = postLike.Id }, postLike);
        }

        // PUT api/<PostLikeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PostLike postLike)
        {
            if (id != postLike.Id)
            {
                return BadRequest();
            }
            _postLikeRepository.Update(postLike);
            return NoContent();
        }

        // DELETE api/<PostLikeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postLikeRepository.Delete(id);
            return NoContent();
        }
    }
}
