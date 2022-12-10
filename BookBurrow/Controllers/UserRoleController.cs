using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        // GET: api/<UserRoleController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userRoleRepository.GetAllOrderedByCreatedAt());
        }

        // GET api/<UserRoleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userRole = _userRoleRepository.GetById(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return Ok(userRole);
        }

        // POST api/<UserRoleController>
        [HttpPost]
        public IActionResult Post(UserRole userRole)
        {
            _userRoleRepository.Add(userRole);
            return CreatedAtAction("Get", new { id = userRole.Id }, userRole);
        }

        // PUT api/<UserRoleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return BadRequest();
            }
            _userRoleRepository.Update(userRole);
            return NoContent();
        }

        // DELETE api/<UserRoleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userRoleRepository.Delete(id);
            return NoContent();
        }
    }
}
