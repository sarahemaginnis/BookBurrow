using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
   
        // GET: api/<AuthorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_authorRepository.GetAllOrderedByLastName());
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var author = _authorRepository.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public IActionResult Post(Author author)
        {
            _authorRepository.Add(author);
            return CreatedAtAction("Get", new { id = author.Id }, author);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }
            _authorRepository.Update(author);
            return NoContent();
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _authorRepository.Delete(id);
            return NoContent();
        }
    }
}
