using System;
using Microsoft.AspNetCore.Mvc;
using BookBurrow.Repositories;
using BookBurrow.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookBurrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesBookController : ControllerBase
    {
        private readonly ISeriesBookRepository _seriesBookRepository;
        public SeriesBookController(ISeriesBookRepository seriesBookRepository)
        {
            _seriesBookRepository = seriesBookRepository;
        }
        // GET: api/<SeriesBookController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_seriesBookRepository.GetAllOrderedBySeriesPosition());
        }

        // GET api/<SeriesBookController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var seriesBook = _seriesBookRepository.GetById(id);
            if (seriesBook == null)
            {
                return NotFound();
            }
            return Ok(seriesBook);
        }

        // POST api/<SeriesBookController>
        [HttpPost]
        public IActionResult Post(SeriesBook seriesBook)
        {
            _seriesBookRepository.Add(seriesBook);
            return CreatedAtAction("Get", new {id = seriesBook.Id}, seriesBook);
        }

        // PUT api/<SeriesBookController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, SeriesBook seriesBook)
        {
            if (id != seriesBook.Id)
            {
                return BadRequest();
            }
            _seriesBookRepository.Update(seriesBook);
            return NoContent();
        }

        // DELETE api/<SeriesBookController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _seriesBookRepository.Delete(id);
            return NoContent();
        }
    }
}
