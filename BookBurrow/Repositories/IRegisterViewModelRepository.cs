using BookBurrow.Models;
using BookBurrow.ViewModels;

namespace BookBurrow.Repositories
{
    public interface IRegisterViewModelRepository
    {
        public RegisterViewModel GetUserById(int id);
        public List<RegisterViewModel> GetAllUserProfiles();
        public RegisterViewModel GetUserProfileById(int id);
        public RegisterViewModel GetUserRoleById(int id);
        public void UpdateUserProfile(RegisterViewModel registerViewModel);
        public void AddUserRole(RegisterViewModel registerViewModel);
    }
}
