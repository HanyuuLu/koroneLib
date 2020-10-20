using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KoroneLibrary.Models
{
    public class Search
    {
        static readonly Random random = new Random();
        readonly DataServer dataServer;
        
        public Search(DataServer dataServer) { this.dataServer = dataServer; }
        public Search() { }

        public IList<Article> AdvancedSearch(string searchword)
        {
            if (string.IsNullOrEmpty(searchword))
            {
                return dataServer.ArticleDictionary.Values.ToList();
            }
            else
            {
                var res = new List<Article>();
                string[] searchList = searchword.Split(" ");
                foreach (var i in dataServer.ArticleDictionary)
                {
                    Article searchRes = new Article();
                    var propList = i.Value.GetType().GetProperties();
                    bool selected = false;
                    foreach (var prop in propList)
                    {
                        if (prop.Name == "Filepath" || prop.Name== "FileName")
                        { continue; }
                        // Collection类型，注解
                        if (prop.GetValue(i.Value) != null 
                            && prop.PropertyType.ToString().Contains("System.Collections"))
                        {
                            foreach (var j in prop.GetValue(i.Value) as Dictionary<string, string>)
                            {
                                foreach (var key in searchList)
                                {
                                    if ((j.Value ?? "").Contains(key))
                                    {
                                        if (searchRes.Node == null) { searchRes.Node = new Dictionary<string, string>(); }
                                        searchRes.Node.Add(j.Key, j.Value);
                                        selected = true;
                                    }
                                }
                            }
                        }
                        // 一般类型，标题、作者等
                        else
                        {
                            foreach (var key in searchList)
                            {
                                if ((prop.GetValue(i.Value) ?? "").ToString().Contains(key))
                                {
                                    string resString = prop.GetValue(i.Value).ToString();
                                    if (resString.Length > 64)
                                    {
                                        Regex regex = new Regex($"[^。?……！\\s]*{key}[^。?……！\\s]*");
                                        var regRes = regex.Matches(prop.GetValue(i.Value).ToString());
                                        resString =  string.Join('\n', regRes);
                                    }
                                    prop.SetValue(searchRes, resString);
                                    selected = true;
                                }
                            }
                        }
                    }
                    if (selected)
                    {
                        searchRes.Title = i.Value.Title;
                        searchRes.Uuid = i.Value.Uuid;
                        res.Add(searchRes);
                    }
                }
                return res;
            }
        }
    }
}
