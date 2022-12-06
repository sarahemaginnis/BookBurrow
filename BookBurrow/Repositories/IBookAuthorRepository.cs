using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IBookAuthorRepository
    {
        public List<BookAuthor> GetAllOrderedByTitle();
        public BookAuthor GetById(int id);
        public void Add(BookAuthor bookAuthor);
        public void Update(BookAuthor bookAuthor);
        public void Delete(int id);
    }
}
