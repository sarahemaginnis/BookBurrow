namespace BookBurrow.Models
{
    public class UserBookRatingUpdateRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public BookStatus BookStatus { get; set; }
        public Rating Rating { get; set; }
    }
}
