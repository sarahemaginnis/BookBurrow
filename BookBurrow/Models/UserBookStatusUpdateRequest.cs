namespace BookBurrow.Models
{
    public class UserBookStatusUpdateRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public BookStatus BookStatus { get; set; }
    }
}
