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
    public class LoginViewModelController : ControllerBase
    {
        private readonly ILoginViewModelRepository _loginViewModelRepository;
        public LoginViewModelController(ILoginViewModelRepository loginViewModelRepository)
        {
            _loginViewModelRepository = loginViewModelRepository;
        }

        // GET: api/<PostCommentController>
        [HttpGet]
        [ActionName("GetUsersAndUserProfiles")]
        public IActionResult Get()
        {
            return Ok(_loginViewModelRepository.GetAll());
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
