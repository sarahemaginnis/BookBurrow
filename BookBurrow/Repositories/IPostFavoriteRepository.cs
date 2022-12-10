using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IPostFavoriteRepository
    {
        public List<PostFavorite> GetAllOrderedByFavoritedDate();
        public PostFavorite GetById(int id);
        public void Add(PostFavorite postFavorite);
        public void Update(PostFavorite postFavorite);
        public void Delete(int id);
    }
}
