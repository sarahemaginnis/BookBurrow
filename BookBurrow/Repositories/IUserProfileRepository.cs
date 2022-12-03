using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserProfileRepository
    {
        public List<UserProfile> GetAll();
        public UserProfile GetById(int id);
        public void Add(UserProfile userProfile);
        public void Update(UserProfile userProfile);
        public void Delete(int id);
    }
}
