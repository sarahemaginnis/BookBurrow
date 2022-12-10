using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginViewModelController : ControllerBase
    {
        private readonly ILoginViewModelRepository _loginViewModelRepository;
        public LoginViewModelController(ILoginViewModelRepository loginViewModelRepository)
        {
            _loginViewModelRepository = loginViewModelRepository;
        }

        // GET: api/<LoginViewModelController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginViewModelController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginViewModelController>
        [HttpPost("AddUser")]
        public IActionResult Post(User user)
        {
            _loginViewModelRepository.Add(user);
            return CreatedAtAction("Get", new {id = user.Id}, user);
        }

        // POST api/<LoginViewModelController>
        [HttpPost("AddUserProfile")]
        public IActionResult Post(UserProfile userProfile)
        {
            _loginViewModelRepository.Add(userProfile);
            return CreatedAtAction("Get", new { id = userProfile.Id }, userProfile);
        }

        // PUT api/<LoginViewModelController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginViewModelController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
