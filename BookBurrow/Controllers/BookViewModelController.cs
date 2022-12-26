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
        private readonly IUserPostRepository _userPostRepository;
        public BookViewModelController(IBookAuthorRepository bookAuthorRepository, IUserPostRepository userPostRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _userPostRepository = userPostRepository;
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
            var options = BookStatus.ListBookStatuses();
            var vm = new BookAuthorViewModel { BookAuthor = book, BookStatusOptions = options };
            return Ok(vm);
        }

        //GET: api/<BookViewModelController>
        [HttpGet]
        [ActionName("GetBookPosts/{id}")]
        public IActionResult GetBookPosts([FromRoute]int id)
        {
            List<UserPost> userPosts = _userPostRepository.GetAllUserPostsForBookByBookIdOrderedByCreationDate(id);
            if (userPosts == null)
            {
                return NotFound();
            }
            return Ok(userPosts);
        }
    }
}
