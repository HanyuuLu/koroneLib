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
            return "alive";
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Article Get(string id)
        {
            return KoroneServer.Instance.getArticle(id);
        }

        // POST api/<controller>
        [HttpPost]
        public string Post([FromBody]Article value)
        {
            if (value == null)
            {
                Response.StatusCode = 404;
                return "bad request";
            }
            else if(value.node.ContainsKey("type") && value.node.ContainsKey("id"))
            {
                if (value.node["type"] == "update")
                {
                    string id = value.node["type"];
                    value.node.Remove("type");
                    value.node.Remove("id");
                    KoroneServer.Instance.update(id, value);
                    return id;
                }
                else if (value.node["type"] == "delete")
                {
                    KoroneServer.Instance.delete(value.node["id"]);
                }
                return "none";
            }
            else
            {
                Response.StatusCode = 404;
                return "bad request";
            }
        }

        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //    Console.WriteLine(value);
        //}

        //// DELETE api/<controller>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
