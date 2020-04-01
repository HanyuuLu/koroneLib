using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.RegularExpressions;
//using Microsoft.Extensions.Configuration;

namespace KoroneServer
{
    public class KoroneServer
    {
        public Dictionary<string, string> TitleList { get; }
        private Dictionary<string, Article> CacheList;
        private string LibraryFolder;
        public static KoroneServer Instance { get; } = new KoroneServer();
        public KoroneServer()
        {
            TitleList = new Dictionary<string, string>();
            CacheList = new Dictionary<string, Article>();
            LibraryFolder = "./Library";
            if (!Directory.Exists(LibraryFolder))
            {
                Directory.CreateDirectory(LibraryFolder);
            }
            flushTitleList();
            Console.WriteLine("KoroneServer初始化完成");
        }
        public Article getArticle(string filename)
        {
            if (CacheList.ContainsKey(filename))
            {
                return CacheList[filename];
            }
            return new Article();
        }
        public IDictionary<string, string> getArticleList(string? searchKey = null)
        {
            return TitleList;
        }
        public void flushTitleList()
        {
            Console.WriteLine("触发全局刷新");
            foreach (var filename
                in Directory.GetFiles(LibraryFolder, "*.json"))
            {
                try
                {
                    var jsonString = File.ReadAllText(filename);
                    var filenameBase = base64x(filename);
                    var article = JsonSerializer.Deserialize<Article>(jsonString);

                    var title = article.title;
                    if (!TitleList.ContainsKey(filenameBase))
                    {
                        TitleList.Add(filenameBase, title);
                    }
                    else
                    {
                        TitleList[filenameBase] = title;
                    }
                    if (!CacheList.ContainsKey(filenameBase))
                    {
                        CacheList.Add(filenameBase, article);
                    }
                    else
                    {
                        CacheList[filenameBase] = article;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("{0}\n{1}", filename, e.Message));
                }
            }
        }
        public string update(string id,Article content)
        {
            if (CacheList.ContainsKey(id))
            {
                CacheList[id] = content;
            }
            else
            {
                var filename = $"{LibraryFolder}/{content.title}-{content.author}.json";
                TitleList.Add(base64x(filename), content.title);
                CacheList.Add(base64x(filename), content);
                File.WriteAllText(filename,JsonSerializer.Serialize(content));
            }
            return id;
        }
        public void delete(string id)
        {
            if(File.Exists(rebase64x(id)))
            {
                File.Delete(rebase64x(id));
                if(TitleList.ContainsKey(id))
                {
                    TitleList.Remove(id);
                }
                if(CacheList.ContainsKey(id))
                {
                    CacheList.Remove(id);
                }
            }
        }
        public IDictionary<string,string> search(string src)
        {
            if (src==null || src=="")
            { return KoroneServer.Instance.getArticleList(src); }
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach(var i in CacheList)
            {
                if(
                    i.Value.title.Contains(src) ||
                    i.Value.author.Contains(src) ||
                    i.Value.tag.Contains(src)||
                    i.Value.body.Contains(src)
                    )
                {
                    res.Add(i.Key, i.Value.title);
                    continue;
                }
                foreach(var j in i.Value.node)
                {
                    if(j.Value.Contains(src))
                    { res.Add(i.Key, i.Value.title); break; }
                }
            }
            return res;
        }
        public string base64x(string src)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(src)).Replace("+", "-").Replace("/", "_");
        }
        public string rebase64x(string src)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(src.Replace("-", "+").Replace("_", "/")));
        }
    }
}
