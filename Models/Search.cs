using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoroneLibrary.Models
{
    public class Search
    {
        static Random random = new Random();
        readonly DataServer dataServer;
        
        public Search(DataServer dataServer) { this.dataServer = dataServer; }
        public List<Article> articleList;
        public Search() { }
        public Search SearchDetailsMock(Dictionary<string, string> searchDict = null)
        {
            Search result = new Search();
            result.articleList = new List<Article>();
            int count = random.Next(1,100);
            for(var i = 0;i<count;++i)
            {
                Article article = new Article();
                article.author = $"示例作者{random.Next().ToString()}";
                article.body = $"示例正文{random.Next().ToString()}";
                article.title = $"示例标题{random.Next().ToString()}";
                article.grade = $"示例年级{random.Next().ToString()}";
                var dict = new Dictionary<string, string>();
                dict.Add("N1", $"示例注释{random.Next().ToString()}");
                article.node = dict;
                article.tag = $"示例标签{random.Next().ToString()}";
                result.articleList.Add(article);
            }
            return result;
        }

        public IDictionary<string,Article> search(string searchword)
        {
            if (searchword==null)
            {
                return dataServer.ArticleDictionary;
            }
            else
            {
                var res = new Dictionary<string, Article>();
                string[] searchList = searchword.Split(" ");
                foreach (var i in dataServer.ArticleDictionary)
                {
                    Article searchRes = new Article();
                    var propList = i.Value.GetType().GetProperties();
                    foreach (var prop in propList)
                    {
                        if (prop.GetValue(i.Value) != null 
                            && prop.PropertyType.ToString().Contains("System.Collections"))
                        {
                            foreach (var j in prop.GetValue(i.Value) as Dictionary<string, string>)
                            {
                                foreach (var key in searchList)
                                {
                                    if ((j.Value ?? "").Contains(key))
                                    {
                                        if (searchRes.node == null) { searchRes.node = new Dictionary<string, string>(); }
                                        searchRes.node.Add(j.Key, j.Value);
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var key in searchList)
                            {
                                if ((prop.GetValue(i.Value) ?? "").ToString().Contains(key))
                                { prop.SetValue(searchRes, prop.GetValue(i.Value)); }
                            }
                        }
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
