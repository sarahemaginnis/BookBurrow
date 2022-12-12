using BookBurrow.Models;
using BookBurrow.ViewModels;

namespace BookBurrow.Repositories
{
    public interface ILoginViewModelRepository
    {
        public List<LoginViewModel> GetAll();
        public void AddUser(LoginViewModel loginViewModel);
        public void AddUserProfile(LoginViewModel loginViewModel);
    }
}
