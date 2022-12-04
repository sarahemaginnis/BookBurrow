using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IAuthorRepository
    {
        public List<Author> GetAllOrderedByLastName();
        public Author GetById(int id);
        public void Add(Author author);
        public void Update(Author author);
        public void Delete(int id);
    }
}
