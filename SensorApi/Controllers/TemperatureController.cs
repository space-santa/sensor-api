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
        public List<TemperatureItem> GetAll()
        {
            return _context.TemperatureItems.ToList();
        }

        // GET: api/<controller>/latest
        [EnableCors("AllowCors"), HttpGet("latest", Name = "LatestTemperature")]
        public List<TemperatureItem> GetLatest()
        {
            var list = _context.TemperatureItems.ToList();
            List<TemperatureItem> retval = new List<TemperatureItem>();

            System.Diagnostics.Debug.WriteLine($"Get the latest of {list.Count.ToString()}");
            for (int i = Math.Max(list.Count() - 4, 0); i < list.Count(); ++i)
            {
                retval.Add(list[i]);
                System.Diagnostics.Debug.WriteLine($"at {i.ToString()}");
            }
            System.Diagnostics.Debug.WriteLine($"result is of length {retval.Count.ToString()}");

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
