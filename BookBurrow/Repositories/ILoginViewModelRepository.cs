using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface ILoginViewModelRepository
    {
        public void Add(User user);
        public void Add(UserProfile userProfile);
    }
}
