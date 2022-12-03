using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPronounController : ControllerBase
    {
        private readonly IUserPronounRepository _userPronounRepository;
        public UserPronounController(IUserPronounRepository userPronounRepository)
        {
            _userPronounRepository = userPronounRepository;
        }

        // GET: api/<UserPronounController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userPronounRepository.GetAll());
        }

        // GET api/<UserPronounController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userPronoun = _userPronounRepository.GetById(id);
            if (userPronoun == null)
            {
                return NotFound();
            }
            return Ok(userPronoun);
        }

        // POST api/<UserPronounController>
        [HttpPost]
        public IActionResult Post(UserPronoun userPronoun)
        {
            _userPronounRepository.Add(userPronoun);
            return CreatedAtAction("Get", new { id = userPronoun.Id }, userPronoun);
        }

        // PUT api/<UserPronounController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserPronoun userPronoun)   
        {
            if (id != userPronoun.Id)
            {
                return BadRequest();
            }
            _userPronounRepository.Update(userPronoun);
            return NoContent();
        }

        // DELETE api/<UserPronounController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userPronounRepository.Delete(id);
            return NoContent();
        }
    }
}
