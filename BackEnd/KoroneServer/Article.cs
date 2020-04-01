using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoroneServer
{
    public class Article
    {
        public string grade { get; set; }
        public string unit { get; set; }
        public string index { get; set; }
        public string subIndex { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string tag { get; set; }
        public string body { get; set; }
        public Dictionary<string, string> node { get; set; }
    }
}
