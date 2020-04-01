using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoroneServer
{
    [Route("api/[controller]")]
    public class articleController : Controller
    {
        [HttpGet]
        public String Get()
        {
            return "hi";
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Article Get(string id)
        {
            return KoroneServer.Instance.getArticle(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
