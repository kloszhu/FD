using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FD.Vue.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SharpBaseController : ControllerBase
    {
        // GET: api/<SharpBaseController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SharpBaseController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SharpBaseController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SharpBaseController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SharpBaseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
