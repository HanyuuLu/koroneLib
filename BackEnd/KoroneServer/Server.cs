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
        private Dictionary<string, JsonDocument> CacheList;
        private string LibraryFolder;
        public static KoroneServer Instance { get; } = new KoroneServer();
        public KoroneServer()
        {
            TitleList = new Dictionary<string, string>();
            CacheList = new Dictionary<string, JsonDocument>();
            LibraryFolder = "./Library";
            if (!Directory.Exists(LibraryFolder))
            {
                Directory.CreateDirectory(LibraryFolder);
            }
            flushTitleList();
            Console.WriteLine("KoroneServer初始化完成");
        }
        public string getArticle(string filename)
        {
            if(CacheList.ContainsKey(filename))
            {
                return CacheList[filename].RootElement.ToString();
            }
            return "";
        }
        public IDictionary<string,string> getArticleList(string? searchKey=null)
        {
            return TitleList;
        }
        public void flushTitleList()
        {
            Console.WriteLine("触发全局刷新");
            foreach (var filename in Directory.GetFiles(LibraryFolder, "*.json"))
            {
                try
                {
                    var bytes = File.OpenRead(filename);
                    var reader = JsonDocument.Parse(bytes);

                    var title = reader.RootElement.GetProperty("title").GetString();
                    if (!TitleList.ContainsKey(filename))
                    {
                        TitleList.Add(filename, title);
                    }
                    else if (TitleList[filename] != title)
                    {
                        TitleList[filename] = title;
                    }
                    if (!CacheList.ContainsKey(filename))
                    {
                        CacheList.Add(filename, reader);
                    }
                    else if (CacheList[filename].GetHashCode() != reader.GetHashCode())
                    {
                        CacheList[filename] = reader;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("{0}\n{1}", filename, e.Message));
                }
            }
            Console.WriteLine(getArticle("./Library\\七上《论语》十二章.json"));
        }
    }
}
