using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int id);
        public void Add(User user);
        public void Update(User user);
        public void Delete(int id);

    }
}
