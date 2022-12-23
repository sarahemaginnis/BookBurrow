using BookBurrow.Models;

namespace BookBurrow.ViewModels
{
    public class BookAuthorViewModel
    {
        public BookAuthor BookAuthor { get; set; }
        public List<BookStatus> BookStatusOptions { get; set; }
    }
}
