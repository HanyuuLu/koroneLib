using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
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
            if(CacheList.ContainsKey(filename))
            {
                return CacheList[filename];
            }
            return new Article();
        }
        public IDictionary<string,string> getArticleList(string? searchKey=null)
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
                    var filenameBase = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(filename));
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
    }
}
