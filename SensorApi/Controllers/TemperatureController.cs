using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SensorApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorApi.Controllers
{
    [Route("api/[controller]")]
    public class TemperatureController : Controller
    {
        private readonly TemperatureContext _context;

        public TemperatureController(TemperatureContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public List<TemperatureItem> GetAll(DateTime startDate, long deviceId)
        {
            var timeFilteredData = _context.TemperatureItems.Where(x => x.Timestamp >= startDate && x.DeviceId == deviceId);
            return timeFilteredData.ToList();
        }

        // GET: api/<controller>/latest
        [EnableCors("AllowCors"), HttpGet("latest", Name = "LatestTemperature")]
        public List<TemperatureItem> GetLatest(int[] deviceIds)
        {
            List<TemperatureItem> retval = new List<TemperatureItem>();

            foreach (int deviceId in deviceIds)
            {
                var item = _context.TemperatureItems.Where(x => x.DeviceId == deviceId).Last();
                retval.Add(item);
            }

            return retval;
        }

        // GET: api/<controller>/5
        [HttpGet("{id}", Name = "GetTemperature")]
        public IActionResult GetById(long id)
        {
            var item = _context.TemperatureItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Create([FromBody] TemperatureItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TemperatureItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTemperature", new { id = item.Id }, item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
