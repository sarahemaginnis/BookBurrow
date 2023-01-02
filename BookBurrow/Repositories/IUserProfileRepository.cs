using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserProfileRepository
    {
        public List<UserProfile> GetAll();
        public List<UserProfile> Search(string criterion);
        public UserProfile GetById(int id);
        public UserProfile GetByUserId(int id);
        public void Add(UserProfile userProfile);
        public void Update(UserProfile userProfile);
        public void Delete(int id);
    }
}
