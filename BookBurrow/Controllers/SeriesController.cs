using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesRepository _seriesRepository;
        public SeriesController(ISeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        // GET: api/<SeriesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_seriesRepository.GetAllOrderedByName());
        }

        // GET api/<SeriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var series = _seriesRepository.GetById(id);
            if (series == null)
            {
                return NotFound();
            }
            return Ok(series);
        }

        // POST api/<SeriesController>
        [HttpPost]
        public IActionResult Post(Series series)
        {
            _seriesRepository.Add(series);
            return CreatedAtAction("Get", new { id = series.Id }, series);
        }

        // PUT api/<SeriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Series series)
        {
            if (id != series.Id)
            {
                return BadRequest();
            }
            _seriesRepository.Update(series);
            return NoContent();
        }

        // DELETE api/<SeriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _seriesRepository.Delete(id);
            return NoContent();
        }
    }
}
