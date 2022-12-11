using BookBurrow.Models;

namespace BookBurrow.Repositories
{
    public interface IRegisterViewModelRepository
    {
        public List<UserProfile> GetAllUserProfiles();
        public User GetUserById(int id);
        public UserProfile GetUserProfileById(int id);
        public UserRole GetUserRoleById(int id);
        public void UpdateUserProfile(UserProfile userProfile);
        public void AddUserRole(UserRole userRole);
    }
}
