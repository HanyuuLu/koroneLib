using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KoroneLibrary.Data
{
    public class Article
    {
        private static readonly string FILENAME_FILTER = "[\\/:*?\" <>|]+";
        private static readonly Regex regex = new Regex(FILENAME_FILTER);

        public string Uuid { get; set; }
        public string Grade { get; set; } = "";
        public string Unit { get; set; } = "";
        public string Index { get; set; } = "";
        public string SubIndex { get; set; } = "";
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string Tag { get; set; } = "";
        public string Body { get; set; } = "";

        [NotMapped]
        public List<Pair<string, string>> Node { get; set; }

        [JsonIgnore]
        public string Filepath { get; set; }

        public string FileName { get { return $"{regex.Replace(Title, "")}.json"; ; } }
    }
}