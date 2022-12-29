using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserBookRepository
    {
        public List<UserBook> GetAllOrderedByReviewCreatedAt();
        public UserBook GetById(int user, int book);
        public void Add(UserBook userBook);
        public void Update(UserBook userBook);
        public void Delete(int id);
    }
}
