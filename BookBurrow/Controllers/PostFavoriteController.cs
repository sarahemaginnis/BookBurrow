using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostFavoriteController : ControllerBase
    {
        private readonly IPostFavoriteRepository _postFavoriteRepository;
        public PostFavoriteController(IPostFavoriteRepository postFavoriteRepository)
        {
            _postFavoriteRepository = postFavoriteRepository;
        }

        // GET: api/<PostFavoriteController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postFavoriteRepository.GetAllOrderedByFavoritedDate());
        }

        // GET api/<PostFavoriteController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var postLike = _postFavoriteRepository.GetById(id);
            if (postLike == null)
            {
                return NotFound();
            }
            return Ok(postLike);
        }

        // POST api/<PostFavoriteController>
        [HttpPost]
        public IActionResult Post(PostFavorite postFavorite)
        {
            _postFavoriteRepository.Add(postFavorite);
            return CreatedAtAction("Get", new { id = postFavorite.Id }, postFavorite);
        }

        // PUT api/<PostFavoriteController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, PostFavorite postFavorite)
        {
            if (id != postFavorite.Id)
            {
                return BadRequest();
            }
            _postFavoriteRepository.Update(postFavorite);
            return NoContent();
        }

        // DELETE api/<PostFavoriteController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postFavoriteRepository.Delete(id);
            return NoContent();
        }
    }
}
