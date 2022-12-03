using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserPronounRepository
    {
        public List<UserPronoun> GetAll();
        public UserPronoun GetById(int id);
        public void Add(UserPronoun userPronoun);
        public void Update(UserPronoun userPronoun);
        public void Delete(int id);
    }
}
