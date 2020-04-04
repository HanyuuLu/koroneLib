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
        private Dictionary<string, Article> CacheList;
        private string LibraryFolder;
        public static KoroneServer Instance { get; } = new KoroneServer();
        public KoroneServer()
        {
            CacheList = new Dictionary<string, Article>();
            LibraryFolder = "./Lib";
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
            return new Article("访问了不存在的文档，文档可能已被删除，请刷新重试");
        }
        public void flushTitleList()
        {
            Console.WriteLine("触发全局刷新");
            foreach (var filenameWtihPath
                in Directory.GetFiles(LibraryFolder, "*.json"))
            {
                try
                {
                    var jsonString = File.ReadAllText(filenameWtihPath);
                    var filenameBase = base64x(Path.GetFileName(filenameWtihPath));
                    var article = JsonSerializer.Deserialize<Article>(jsonString);

                    var title = article.title;
                    if (!CacheList.ContainsKey(filenameBase))
                    {
                        lock (CacheList)
                        {
                            CacheList.Add(filenameBase, article);
                        }
                    }
                    else
                    {
                        lock(CacheList)
                        { 
                            CacheList[filenameBase] = article;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("{0}\n{1}", filenameWtihPath, e.Message));
                }
            }
            Console.WriteLine($"已加载 {CacheList.Count} 篇文档");
        }
        public string update(string id, Article content)
        {
            var filename = genFilenmae(content);
            var newID = base64x(filename);
            try
            {

                if (!CacheList.ContainsKey(newID))
                {
                    lock (CacheList)
                    {
                        CacheList.Add(newID, content);
                    }
                }
                else
                {
                    lock (CacheList)
                    {
                        CacheList[newID] = content;
                    }
                }
                /*
                 * TODO:加上脏写功能
                 */
                File.WriteAllText(Path.Join(LibraryFolder, filename), JsonSerializer.Serialize(content));
                if (id != newID)
                {
                    delete(id);
                }
                return newID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return newID;
            }
            finally
            {
                flushTitleList();
            }
        }
        public void delete(string id)
        {
            try
            {
                var filenameWithPath = Path.Join(LibraryFolder, rebase64x(id));
                if (File.Exists(filenameWithPath))
                {
                    File.Delete(filenameWithPath);
                }
                if (CacheList.ContainsKey(id))
                {
                    lock (CacheList)
                    {
                        CacheList.Remove(id);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                flushTitleList();
            }
        }
        public IDictionary<string, SearchInfo> search(string src = "",string type="0")
        {
            Dictionary<string, SearchInfo> res = new Dictionary<string, SearchInfo>();
            if (src == "")
            {
                lock (CacheList)
                {
                    foreach (var i in CacheList)
                    {
                        res.Add(i.Key, new SearchInfo(genArticleItemListHeader(i.Value)));
                    }
                }
                return res;
            }
            else
            {
                int typeInt;
                try
                {
                    typeInt = int.Parse(type);
                }
                catch
                {
                    return search();
                }
                lock (CacheList)
                {
                    foreach (var i in CacheList)
                    {
                        SearchInfo item = new SearchInfo(genArticleItemListHeader(i.Value));
                        var propList = i.Value.GetType().GetProperties();
                        var typeCopy = typeInt;
                        foreach(var prop in propList)
                        {
                            if((typeCopy & 1)==0)
                            {
                                if (prop.PropertyType.ToString().Contains("System.Collections"))
                                {
                                    foreach(var j in prop.GetValue(i.Value) as Dictionary<string,string>)
                                    {
                                        if(j.Value.Contains(src))
                                        {
                                            item.node.Add($"[node] {j.Value}");
                                        }
                                    }
                                }
                                else
                                {
                                    if(prop.GetValue(i.Value).ToString().Contains(src))
                                    {
                                        item.node.Add($"[{prop.Name}] {prop.GetValue(i.Value).ToString()}");
                                    }
                                }
                            }
                            typeCopy >>= 1;
                        }
                        if (item.node.Count > 0)
                        {
                            res.Add(i.Key, item);
                        }
                    }
                }
                return res;
            }
        }
        public string base64x(string src)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(src)).Replace("+", "-").Replace("/", "_");
        }
        public string rebase64x(string src)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(src.Replace("-", "+").Replace("_", "/")));
        }
        public string genFilenmae(Article src)
        {
            return $"{src.grade}-{src.unit}-{src.title}-{src.author}.json";
        }
        public string genArticleItemListHeader(Article src)
        {
            return $"[{src.grade}] {src.title}-{src.author}";
        }
    }
}
