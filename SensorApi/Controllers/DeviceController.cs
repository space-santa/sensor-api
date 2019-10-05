using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensorApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorApi.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : Controller
    {
        private readonly TemperatureContext _context;

        public DeviceController(TemperatureContext context)
        {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet]
        [AllowAnonymous]
        public List<Device> GetAll()
        {
            return _context.Devices.ToList();
        }

        // GET: api/<controller>/5
        [HttpGet("{id}", Name = "GetDevice")]
        [AllowAnonymous]
        public IActionResult GetById(long id)
        {
            var item = _context.Devices.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = Constants.DeviceRole)]
        public IActionResult Create([FromBody] Device device)
        {
            if (device == null)
            {
                return BadRequest();
            }

            _context.Devices.Add(device);
            _context.SaveChanges();

            return CreatedAtRoute("GetDevice", new { id = device.Id }, device);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Roles = Constants.AdministratorRole)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.AdministratorRole)]
        public void Delete(int id)
        {
        }
    }
}
