using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IBookRepository
    {
        public List<Book> GetAllOrderedByDatePublished();
        public Book GetById(int id);
        public void Add(Book book);
        public void Update(Book book);
        public void Delete(int id);
    }
}
