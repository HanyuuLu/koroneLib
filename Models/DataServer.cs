//using AspNetCore;
using Microsoft.AspNetCore.Authentication;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace KoroneLibrary.Models
{
    public class DataServer
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static readonly string DATA_FOLDER = "./Lib";
        public Dictionary<string, Article> ArticleDictionary { get; }

        public DataServer()
        {
            ArticleDictionary = new Dictionary<string, Article>();
            if (!Directory.Exists(DATA_FOLDER)) { Directory.CreateDirectory(DATA_FOLDER); }
            FlushTitleList();
            Logger.Info("服务初始化完成");
        }
        public Article GetArticle(string uuid)
        {
            if (ArticleDictionary.ContainsKey(uuid)) { return ArticleDictionary[uuid]; }
            throw new Exception($"访问了不存在的文档,uuid为 {uuid}");
        }
        public void FlushTitleList()
        {
            Logger.Info("触发全局刷新");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            lock (ArticleDictionary)
            {

                foreach (var filename in Directory.GetFiles(DATA_FOLDER, "*.json"))
                {
                    try
                    {
                        var jsonString = File.ReadAllText(filename);
                        var article = JsonSerializer.Deserialize<Article>(jsonString, options);

                        if (string.IsNullOrEmpty(article.Uuid))
                        {
                            article.Uuid = Guid.NewGuid().ToString();
                            Update(article);
                        }
                        if (!ArticleDictionary.ContainsKey(article.Uuid))
                        { ArticleDictionary.Add(article.Uuid, article); }
                        else
                        { ArticleDictionary[article.Uuid] = article; }
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"解析文件出错: {filename}\n{e.Message}\n{e.StackTrace}");
                    }
                }
            }
            Logger.Info($"已加载 {ArticleDictionary.Count} 篇文档");
        }
        public void Save(Article article)
        {
            //temp
            //
            if (string.IsNullOrEmpty(article.Uuid))
            { article.Uuid = Guid.NewGuid().ToString(); }
            if (string.IsNullOrEmpty(article.Filepath))
            { article.Filepath = Path.Join(DATA_FOLDER, article.FileName); }
            //{ article.Filepath = GetFullFileNameWithPath(article.Title); }
            if (!ArticleDictionary.ContainsKey(article.Uuid)) { ArticleDictionary.Add(article.Uuid, article); }
            else { ArticleDictionary[article.Uuid] = article; }
            try { File.WriteAllText(article.Filepath, JsonSerializer.Serialize(article)); }
            catch (Exception e)
            {
                Logger.Error($"写入文件出错:{article.Title}-{article.Filepath}\n{e.Message}\n{e.StackTrace}");
                throw e;
            }
        }
        public void Delete(Article article)
        {
            if (!string.IsNullOrEmpty(article.Uuid) && ArticleDictionary.ContainsKey(article.Uuid))
            { ArticleDictionary.Remove(article.Uuid); }
            if (string.IsNullOrEmpty(article.Filepath)) { return; }
            try { if (File.Exists(article.Filepath)) { File.Delete(article.Filepath); } }
            catch (Exception e)
            {
                Logger.Error($"删除文件出错: {article.Title}-{article.Filepath}\n{e.Message}\n{e.StackTrace}");
                throw e;
            }
        }

        public void Update(Article article)
        {
            if (!string.IsNullOrEmpty(article.Uuid))
            {
                if (ArticleDictionary.ContainsKey(article.Uuid) && article.Title != ArticleDictionary[article.Uuid].Title)
                { Delete(ArticleDictionary[article.Uuid]); }
            }
            else
            { article.Uuid = Guid.NewGuid().ToString(); }
            Save(article);
        }

        [Obsolete]
        static readonly string FILENAME_FILTER_STRING = "[\\/:*?\" <>|]+";
        [Obsolete]
        static readonly Regex FILE_NAME_FILTER_REGEX = new Regex(FILENAME_FILTER_STRING);
        [Obsolete]
        public string GetFullFileNameWithPath(string title)
        {
            return Path.Join(DATA_FOLDER, $"{FILE_NAME_FILTER_REGEX.Replace(title, "")}.json");
        }

        [Obsolete]
        public string GetFileCode(string filename)
        {
            return Base64URL(filename);
        }

        [Obsolete]
        public string GetTitle(string fileCode)
        {
            return Rebase64URL(fileCode);
        }

        [Obsolete]
        public string Base64URL(string src)
        {
            //return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(src)).Replace("+", "-").Replace("/", "_");
            return Base64UrlTextEncoder.Encode(Encoding.UTF8.GetBytes(src));
        }

        [Obsolete]
        public string Rebase64URL(string src)
        {
            //return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(src.Replace("-", "+").Replace("_", "/")));
            return Encoding.UTF8.GetString(Convert.FromBase64String(src));
        }
    }
}