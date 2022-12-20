using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;
using BookBurrow.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookController : ControllerBase
    {
        private readonly IUserBookRepository _userBookRepository; 
        public UserBookController(IUserBookRepository userBookRepository)
        {
            _userBookRepository = userBookRepository;
        }

        // GET: api/<UserBookController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userBookRepository.GetAllOrderedByReviewCreatedAt());
        }

        // GET api/<UserBookController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userBook = _userBookRepository.GetById(id);
            if (userBook == null)
            {
                return NotFound();
            }
            var options = BookStatus.ListBookStatuses();
            var vm = new UserBookViewModel { UserBook = userBook, BookStatusOptions = options };
            return Ok(vm);
        }

        // POST api/<UserBookController>
        [HttpPost]
        public IActionResult Post(UserBook userBook)
        {
            _userBookRepository.Add(userBook);
            return CreatedAtAction("Get", new { id = userBook.Id }, userBook);
        }

        // PUT api/<UserBookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserBook userBook)
        {
            if (id!= userBook.Id)
            {
                return BadRequest();
            }
            _userBookRepository.Update(userBook);
            return NoContent();
        }

        // DELETE api/<UserBookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            _userBookRepository.Delete(id);
            return NoContent();
        }
    }
}
