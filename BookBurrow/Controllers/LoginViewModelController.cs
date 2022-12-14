using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;
using BookBurrow.ViewModels;
using FirebaseAdmin;
using FirebaseAdmin.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginViewModelController : ControllerBase
    {
        private readonly ILoginViewModelRepository _loginViewModelRepository;

        private readonly IUserRepository _userRepository;
        public LoginViewModelController(ILoginViewModelRepository loginViewModelRepository, IUserRepository userRepository)
        {
            _loginViewModelRepository = loginViewModelRepository;
            _userRepository = userRepository;
        }

        // GET: api/<PostCommentController>
        [HttpGet]
        [ActionName("GetUsersAndUserProfiles")]
        public IActionResult Get()
        {
            return Ok(_loginViewModelRepository.GetAll());
        }

        // GET: api/<PostCommentController>
        [HttpGet]
        [ActionName("VerifyUser/{id}")]
        public IActionResult GetUser([FromRoute]string id)
        {
            var user = _userRepository.GetByFirebaseId(id);
            if(user == null)
            {
                return NoContent();
            }
            return Ok(user);
        }

        // POST api/<LoginViewModelController>
        [HttpPost]
        [ActionName("AddUser")]
        public IActionResult PostNewUser(LoginViewModel loginViewModel)
        {
            _loginViewModelRepository.AddUser(loginViewModel);
            return CreatedAtAction("GetUsersAndUserProfiles", new {id = loginViewModel.User.Id}, loginViewModel.User);
        }

        // POST api/<LoginViewModelController>
        [HttpPost]
        [ActionName("AddUserProfile")]
        public IActionResult PostNewUserProfile(LoginViewModel loginViewModel)
        {
            _loginViewModelRepository.AddUserProfile(loginViewModel);
            return CreatedAtAction("GetUsersAndUserProfiles", new { id = loginViewModel.UserProfile.Id }, loginViewModel.UserProfile);
        }
    }
}
