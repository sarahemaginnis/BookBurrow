using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
    
        // GET: api/<RatingController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ratingRepository.GetAll());
        }

        // GET api/<RatingController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var rating = _ratingRepository.GetById(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(rating);
        }

        // POST api/<RatingController>
        [HttpPost]
        public IActionResult Post(Rating rating)
        {
            _ratingRepository.Add(rating);
            return CreatedAtAction("Get", new { id = rating.Id }, rating);
        }

        // PUT api/<RatingController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Rating rating)
        {
            if (id != rating.Id)
            {
                return BadRequest();
            }
            _ratingRepository.Update(rating);
            return NoContent();
        }

        // DELETE api/<RatingController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ratingRepository.Delete(id);
            return NoContent();
        }
    }
}
