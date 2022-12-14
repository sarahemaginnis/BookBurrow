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
    public class RegisterViewModelController : ControllerBase
    {
        private readonly IRegisterViewModelRepository _registerViewModelRepository;
        public RegisterViewModelController(IRegisterViewModelRepository registerViewModelRepository)
        {
            _registerViewModelRepository = registerViewModelRepository;
        }
        // GET: api/<RegisterViewModelController>
        [HttpGet]
        [ActionName("AllUserProfiles")]
        public IActionResult Get()
        {
            return Ok(_registerViewModelRepository.GetAllUserProfiles());
        }

        // GET api/<RegisterViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserById")]
        public IActionResult GetUser(int id)
        {
            var user = _registerViewModelRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET api/<RegisterViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserProfileById")]
        public IActionResult GetUserProfile(int id)
        {
            var userProfile = _registerViewModelRepository.GetUserProfileById(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(userProfile);
        }

        // GET api/<RegisterViewModelController>/5
        [HttpGet("{id}")]
        [ActionName("UserRoleById")]
        public IActionResult GetUserRole(int id)
        {
            var userRole = _registerViewModelRepository.GetUserRoleById(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(userRole);
        }

        // POST api/<RegisterViewModelController>
        [HttpPost]
        [ActionName("AddUserRole")]
        public IActionResult Post(RegisterViewModel registerViewModel)
        {
            _registerViewModelRepository.AddUserRole(registerViewModel);
            return CreatedAtAction("UserRoleById", new { id = registerViewModel.UserRole.Id }, registerViewModel.UserRole);
        }

        // PUT api/<RegisterViewModelController>/5
        [HttpPut("{id}")]
        [ActionName("UpdateUserProfile")]
        public IActionResult Put(int id, RegisterViewModel registerViewModel)
        {
            if (id != registerViewModel.UserProfile.Id)
            {
                return BadRequest();
            }
            _registerViewModelRepository.UpdateUserProfile(registerViewModel);
            return NoContent();
        }
    }
}
