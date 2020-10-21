using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace KoroneLibrary.Data
{
    public class Article:ICloneable
    {
        //private static readonly string FILENAME_FILTER = "[\\/:*?\" <>|]+";
        //private static readonly Regex regex = new Regex(FILENAME_FILTER);

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

        public string FileName { get { return $"{Uuid}.json"; ; } }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Article DeepClone()
        {
            Article res = new Article
            {
                Uuid = Uuid,
                Grade = Grade,
                Unit = Unit,
                Index = Index,
                SubIndex = SubIndex,
                Title = Title,
                Author = Author,
                Tag = Tag,
                Body = Body,
                Node = new List<Pair<string, string>>(),
                Filepath = Filepath
            };
            foreach(Pair<string,string>item in Node)
            {
                res.Node.Add(item.DeepClone());
            }
            return res;
        }
    }
}