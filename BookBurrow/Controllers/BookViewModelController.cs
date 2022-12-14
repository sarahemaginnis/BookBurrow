using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;
using BookBurrow.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookViewModelController : ControllerBase
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        public BookViewModelController(IBookAuthorRepository bookAuthorRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
        }

        // GET: api/<PostCommentController>
        [HttpGet]
        [ActionName("GetBook/{id}")]
        public IActionResult GetBookByBookId([FromRoute]int id)
        {
            var book = _bookAuthorRepository.GetByBookId(id);
            if (book == null)
            {
                return NoContent();
            }
            return Ok(book);
        }
    }
}
