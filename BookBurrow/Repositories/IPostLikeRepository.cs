using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IPostLikeRepository
    {
        public List<PostLike> GetAllOrderedByLikedDate();
        public PostLike GetById(int id);
        public void Add(PostLike postLike);
        public void Update(PostLike postLike);
        public void Delete(int id);
    }
}
