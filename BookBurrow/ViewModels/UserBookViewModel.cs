using BookBurrow.Models;

namespace BookBurrow.ViewModels
{
    public class UserBookViewModel
    {
        public UserBook UserBook { get; set; }
        public List<BookStatus> BookStatusOptions { get; set; }
    }
}
