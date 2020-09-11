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
        
        [Obsolete]
        public Search SearchDetailsMock(Dictionary<string, string> searchDict = null)
        {
            Search result = new Search();
            var articleList = new List<Article>();
            int count = random.Next(1,100);
            for(var i = 0;i<count;++i)
            {
                Article article = new Article
                {
                    Author = $"示例作者{random.Next()}",
                    Body = $"示例正文{random.Next()}",
                    Title = $"示例标题{random.Next()}",
                    Grade = $"示例年级{random.Next()}",
                    Tag = $"示例标签{random.Next()}",
                    Node = new Dictionary<string, string>
                    {
                        { "N1", $"示例注释{random.Next()}" }
                    }
                };
                articleList.Add(article);
            }
            return result;
        }

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
    //    public IDictionary<string, SearchInfo> preciseSearch(string src = "")
    //    {
    //        var res = new Dictionary<string, SearchInfo>();
    //        Dictionary<string, List<string>> type;
    //        try
    //        {
    //            type = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(src);
    //        }
    //        catch
    //        {
    //            return search();
    //        }
    //        foreach (var articlePair in CacheList)
    //        {
    //            var searchInfo = new SearchInfo(articlePair.Value.title);
    //            var propTypes = articlePair.Value.GetType().GetProperties();
    //            foreach (var prop in propTypes)
    //            {
    //                if (prop.PropertyType.ToString().Contains("System.Collections"))
    //                {
    //                    foreach (var (node, rawText) in prop.GetValue(articlePair.Value) as Dictionary<string, string>)
    //                    {
    //                        var text = rawText ?? "";
    //                        if (type.ContainsKey("none"))
    //                        {
    //                            foreach (var i in type["none"])
    //                            {
    //                                if (text.Contains(i) && (!searchInfo.node.Contains($"[{prop.Name}] {text}")))
    //                                {
    //                                    searchInfo.node.Add($"[{prop.Name}] {text}");
    //                                }
    //                            }
    //                        }
    //                        if (type.ContainsKey(prop.Name))
    //                        {
    //                            foreach (var i in type[prop.Name])
    //                            {
    //                                if (text.Contains(i) && (!searchInfo.node.Contains($"[{prop.Name}] {text}")))
    //                                {
    //                                    searchInfo.node.Add($"[{prop.Name}] {text}");
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //                else
    //                {

    //                    var text = (prop.GetValue(articlePair.Value) ?? "").ToString();
    //                    if (type.ContainsKey("none"))
    //                    {
    //                        foreach (var i in type["none"])
    //                        {
    //                            if (text.Contains(i) && (!searchInfo.node.Contains($"[{prop.Name}] {text}")))
    //                            {
    //                                searchInfo.node.Add($"[{prop.Name}] {text}");
    //                            }
    //                        }
    //                    }
    //                    if (type.ContainsKey(prop.Name))
    //                    {
    //                        foreach (var i in type[prop.Name])
    //                        {
    //                            if (text.Contains(i) && (!searchInfo.node.Contains($"[{prop.Name}] {text}")))
    //                            {
    //                                searchInfo.node.Add($"[{prop.Name}] {text}");
    //                            }
    //                        }
    //                    }
    //                }

    //            }
    //            if (searchInfo.node.Count == type.Count)
    //            {
    //                res.Add(articlePair.Key, searchInfo);
    //            }
    //        }
    //        return res;
    //    }
    }
}
