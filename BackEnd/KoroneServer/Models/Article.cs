using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace KoroneLibrary.Models
{
    public class Article
    {

        private static String FILENAME_FILTER = "[\\/:*?\" <>|]+";
        private static Regex regex = new Regex(FILENAME_FILTER);

        public string grade { get; set; } = "";
        public string unit { get; set; } = "";
        public string index { get; set; } = "";
        public string subIndex { get; set; } = "";
        public string title { get; set; } = "";
        public string author { get; set; } = "";
        public string tag { get; set; } = "";
        public string body { get; set; } = "";
        public Dictionary<string, string> node { get; set; }

        public string FileName { get { return $"{regex.Replace(title, "")}.json"; ; } }
    }
}