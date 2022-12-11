using BookBurrow.ViewModels;

namespace BookBurrow.Repositories
{
    public interface IUserProfileViewModelRepository
    {
        public UserProfileViewModel GetUserBiographicalSectionById(int id);
        public List<UserProfileViewModel> GetAllUserPostsByUserIdForCount(int id);
        public List<UserProfileViewModel> GetAllUserFollowersByUserIdForCount(int id);
        public List<UserProfileViewModel> GetAllUserFollowingByUserIdForCount(int id);
        public List<UserProfileViewModel> GetAllUserBooksCurrentlyReadingByUserIdOrderedByStartReadingDateDescending(int id);
        public List<UserProfileViewModel> GetAllUserPostsByUserIdOrderedByCreationDateDescending(int id);
        public List<UserProfileViewModel> GetAllFavoritedUserPostsByUserIdOrderedByFavoritedDateDescending(int id);
        public List<UserProfileViewModel> GetAllLikedUserPostsByUserIdOrderedByLikedDateDescending(int id);
    }
}
