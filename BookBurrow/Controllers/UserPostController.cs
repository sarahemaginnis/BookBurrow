using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly IUserPostRepository _userPostRepository; 
        public UserPostController(IUserPostRepository userPostRepository)
        {
            _userPostRepository = userPostRepository;
        }

        // GET: api/<UserPostController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userPostRepository.GetAllOrderedByPostCreationDate());
        }

        //GET: api/<UserPostController>/search
        [HttpGet("search")]
        public IActionResult Search(string q)
        {
            return Ok(_userPostRepository.Search(q));
        }

        // GET api/<UserPostController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)    
        {
            var userPost = _userPostRepository.GetById(id);
            if (userPost == null)
            {
                return NotFound();
            }
            return Ok(userPost);
        }

        //Get api/<UserPostController>/5
        [HttpGet("dashboard")]
        public IActionResult GetAll(int id)
        {
            var userPost = _userPostRepository.GetAllPostsForFeedByUserId(id);
            if (userPost == null)
            {
                return NotFound();
            }
            return Ok(userPost);
        }

        // POST api/<UserPostController>
        [HttpPost]
        public IActionResult Post(UserPost userPost)
        {
            _userPostRepository.Add(userPost);
            return CreatedAtAction("Get", new { id = userPost.Id }, userPost);
        }

        // PUT api/<UserPostController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserPost userPost)
        {
            if (id != userPost.Id)
            {
                return BadRequest();
            }
            _userPostRepository.Update(userPost);
            return NoContent();
        }

        // DELETE api/<UserPostController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userPostRepository.Delete(id);
            return NoContent();
        }
    }
}
