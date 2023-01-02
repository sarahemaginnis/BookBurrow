using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IUserPostRepository
    {
        public List<UserPost> GetAllOrderedByPostCreationDate();
        public List<UserPost> Search(string criterion);
        public List<UserPost> GetAllUserPostsForBookByBookIdOrderedByCreationDate(int id);
        public UserPost GetById(int id);
        public void Add(UserPost userPost);
        public void Update(UserPost userPost);
        public void Delete(int id);
    }
}
