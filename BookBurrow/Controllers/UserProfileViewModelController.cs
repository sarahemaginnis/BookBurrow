using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;
using BookBurrow.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileViewModelController : ControllerBase
    {
        private readonly IUserProfileViewModelRepository _userProfileViewModelRepository;
        public UserProfileViewModelController(IUserProfileViewModelRepository userProfileViewModelRepository)
        {
            _userProfileViewModelRepository = userProfileViewModelRepository;
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserBiography")]
        public IActionResult GetUserBiographicalSection(int id)
        {
            var userProfile = _userProfileViewModelRepository.GetUserBiographicalSectionById(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserPostCount")]
        public IActionResult GetAllUserPostsForCount(int id)
        {
            List<UserProfileViewModel> userPosts = _userProfileViewModelRepository.GetAllUserPostsByUserIdForCount(id);
            if (userPosts == null)
            {
                return NotFound();
            }
            return Ok(userPosts.Count);
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserFollowerCount")]
        public IActionResult GetAllUserFollowersForCount(int id)
        {
            List<UserProfileViewModel> userFollowers = _userProfileViewModelRepository.GetAllUserFollowersByUserIdForCount(id);
            if (userFollowers == null)
            {
                return NotFound();
            }
            return Ok(userFollowers.Count);
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UsersFollowingCount")]
        public IActionResult GetAllUsersFollowingForCount(int id)
        {
            List<UserProfileViewModel> usersFollowing = _userProfileViewModelRepository.GetAllUserFollowingByUserIdForCount(id);
            if (usersFollowing == null)
            {
                return NotFound();
            }
            return Ok(usersFollowing.Count);
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserBooksCurrentlyReading")]
        public IActionResult GetAllBooksCurrentlyReading(int id)
        {
            List<UserProfileViewModel> booksCurrentlyReading = _userProfileViewModelRepository.GetAllUserBooksCurrentlyReadingByUserIdOrderedByStartReadingDateDescending(id);
            if (booksCurrentlyReading == null)
            {
                return NotFound();
            }
            return Ok(booksCurrentlyReading);
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserPosts")]
        public IActionResult GetAllUserPosts(int id)
        {
            List<UserProfileViewModel> userPosts = _userProfileViewModelRepository.GetAllUserPostsByUserIdOrderedByCreationDateDescending(id);
            if (userPosts == null)
            {
                return NotFound();
            }
            return Ok(userPosts);
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("FavoritePosts")]
        public IActionResult GetAllFavoritePosts(int id)
        {
            List<UserProfileViewModel> favoritePosts = _userProfileViewModelRepository.GetAllFavoritedUserPostsByUserIdOrderedByFavoritedDateDescending(id);
            if (favoritePosts == null)
            {
                return NotFound();
            }
            return Ok(favoritePosts);
        }

        // GET api/<UserProfileViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("LikedPosts")]
        public IActionResult GetAllLikePosts(int id)
        {
            List<UserProfileViewModel> likedPosts = _userProfileViewModelRepository.GetAllLikedUserPostsByUserIdOrderedByLikedDateDescending(id);
            if (likedPosts == null)
            {
                return NotFound();
            }
            return Ok(likedPosts);
        }
    }
}
