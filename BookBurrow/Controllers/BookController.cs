using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookrepository)
        {
            _bookRepository = bookrepository;
        }

        // GET: api/<BookController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookRepository.GetAllOrderedByDatePublished());
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // POST api/<BookController>
        [HttpPost]
        public IActionResult Post(Book book)
        {
            _bookRepository.Add(book);
            return CreatedAtAction("Get", new { id = book.Id }, book);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            _bookRepository.Update(book);
            return NoContent();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookRepository.Delete(id);
            return NoContent();
        }
    }
}
