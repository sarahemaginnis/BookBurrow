using BookBurrow.Models;
using BookBurrow.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowerController : ControllerBase
    {
        private readonly IUserFollowerRepository _userFollowerRepository;
        public UserFollowerController(IUserFollowerRepository userFollowerRepository)
        {
            _userFollowerRepository = userFollowerRepository;
        }

        // GET: api/<UserFollowerController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userFollowerRepository.GetAllFollowerProfilesOrderedByFollowDate());
        }

        // GET api/<UserFollowerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userFollower = _userFollowerRepository.GetFollowerProfileById(id);
            if (userFollower == null)
            {
                return NotFound();
            }
            return Ok(userFollower);
        }

        //GET api/<UserFollowerController>/5
        [HttpGet("VerifyFollower")]
        public IActionResult Get(int userId, int profileId)
        {
            var userFollower = _userFollowerRepository.VerifyFollowerStatus(userId, profileId);
            if (userFollower == null)
            {
                return NotFound();
            }
            return Ok(userFollower);
        }

        // POST api/<UserFollowerController>
        [HttpPost]
        public IActionResult Post(UserFollower userFollower)
        {
            _userFollowerRepository.Add(userFollower);
            return CreatedAtAction("Get", new { id = userFollower.Id }, userFollower);
        }

        // PUT api/<UserFollowerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, UserFollower userFollower)
        {
            if (id != userFollower.Id)
            {
                return BadRequest();
            }
            _userFollowerRepository.Update(userFollower);
            return NoContent();
        }

        // DELETE api/<UserFollowerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userFollowerRepository.Delete(id);
            return NoContent();
        }
    }
}
