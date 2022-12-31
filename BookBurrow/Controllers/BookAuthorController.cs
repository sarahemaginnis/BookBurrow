using BookBurrow.Models;
using BookBurrow.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        public BookAuthorController(IBookAuthorRepository bookAuthorRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
        }

        // GET: api/<BookAuthorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookAuthorRepository.GetAllOrderedByTitle());
        }

        //GET: api/<BookAuthorController>/search
        [HttpGet("search")]
        public IActionResult Search(string q)
        {
            return Ok(_bookAuthorRepository.Search(q));
        }

        // GET api/<BookAuthorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var bookAuthor = _bookAuthorRepository.GetById(id);
            if (bookAuthor == null)
            {
                return NotFound();
            }
            return Ok(bookAuthor);
        }

        // POST api/<BookAuthorController>
        [HttpPost]
        public IActionResult Post(BookAuthor bookAuthor)
        {
            _bookAuthorRepository.Add(bookAuthor);
            return CreatedAtAction("Get", new { id = bookAuthor.Id }, bookAuthor);
        }

        // PUT api/<BookAuthorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, BookAuthor bookAuthor)
        {
            if (id != bookAuthor.Id)
            {
                return BadRequest();
            }
            _bookAuthorRepository.Update(bookAuthor);
            return NoContent();
        }

        // DELETE api/<BookAuthorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookAuthorRepository.Delete(id);
            return NoContent();
        }
    }
}
