using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoroneServer
{
    [Route("api/[controller]")]
    public class searchController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IDictionary<string,string> Get()
        {
            return KoroneServer.Instance.getArticleList();
        }

        // GET api/<controller>/5
        [HttpGet("{key}")]
        public IDictionary<string, string> Get(string key)
        {
            return KoroneServer.Instance.search(key);
        }
    }
}
