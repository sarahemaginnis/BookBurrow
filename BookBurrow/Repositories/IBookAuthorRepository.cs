using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IBookAuthorRepository
    {
        public List<BookAuthor> GetAllOrderedByTitle();
        public List<BookAuthor> Search(string criterion);
        public BookAuthor GetById(int id);
        public BookAuthor GetByBookId(int id);
        public void Add(BookAuthor bookAuthor);
        public void Update(BookAuthor bookAuthor);
        public void Delete(int id);
    }
}
