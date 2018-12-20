using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Spice.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        // GET api/plants
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/plants/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/plants
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/plants/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/plants/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}