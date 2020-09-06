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

        static readonly string FILENAME_FILTER_STRING = "[\\/:*?\" <>|]+";
        static readonly Regex FILE_NAME_FILTER_REGEX = new Regex(FILENAME_FILTER_STRING);
        static readonly string DATA_FOLDER = "./Lib";
        public Dictionary<string, Article> ArticleDictionary { get; }

        public DataServer()
        {
            ArticleDictionary = new Dictionary<string, Article>();
            if (!Directory.Exists(DATA_FOLDER)) { Directory.CreateDirectory(DATA_FOLDER); }
            flushTitleList();
            Logger.Info("服务初始化完成");
        }
        public Article getArticle(string fileCode)
        {
            if (ArticleDictionary.ContainsKey(fileCode)) { return ArticleDictionary[fileCode]; }
            throw new Exception("文件不存在");
        }
        public void flushTitleList()
        {
            Logger.Info("触发全局刷新");
            lock (ArticleDictionary)
            {

                foreach (var filename in Directory.GetFiles(DATA_FOLDER, "*.json"))
                {
                    try
                    {
                        var jsonString = File.ReadAllText(filename);
                        var article = JsonSerializer.Deserialize<Article>(jsonString);

                        if (!ArticleDictionary.ContainsKey(filename))
                        { ArticleDictionary.Add(filename, article); }
                        else
                        { ArticleDictionary[filename] = article; }
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"解析文件出错: {filename}\n{e.Message}\n{e.StackTrace}");
                    }
                }
            }
            Logger.Info($"已加载 {ArticleDictionary.Count} 篇文档");
        }
        public string save(Article content)
        {
            string filename = getFullFileNameWithPath(content.title);
            try
            {
                if (!ArticleDictionary.ContainsKey(filename)) { ArticleDictionary.Add(filename, content); }
                else { ArticleDictionary[filename] = content; }
                File.WriteAllText(filename, JsonSerializer.Serialize(content));
            }
            catch (Exception e)
            { 
                Logger.Error($"写入文件出错: {filename}\n{e.Message}\n{e.StackTrace}");
                throw e;
            }
            return filename;
        }
        public void delete(string filename)
        {
            try
            {
                if (ArticleDictionary.ContainsKey(filename)) { ArticleDictionary.Remove(filename); }
                if (File.Exists(filename)) { File.Delete(filename); }
            }
            catch (Exception e)
            { 
                Logger.Error($"删除文件出错: {filename}\n{e.Message}\n{e.StackTrace}");
                throw e;
            }
        }

        public string update(string filename, Article content)
        {
            string newfilename = save(content);
            if (filename != getFullFileNameWithPath(content.title)) { delete(filename); }
            return newfilename;
        }

        public string base64URL(string src)
        {
            //return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(src)).Replace("+", "-").Replace("/", "_");
            return Base64UrlTextEncoder.Encode(Encoding.UTF8.GetBytes(src));
        }
        public string rebase64URL(string src)
        {
            //return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(src.Replace("-", "+").Replace("_", "/")));
            return Encoding.UTF8.GetString(Convert.FromBase64String(src));
        }
        public string getFullFileNameWithPath(string title)
        {
            return Path.Join(DATA_FOLDER,$"{FILE_NAME_FILTER_REGEX.Replace(title,"")}.json");
        }
        public string getFileCode(string filename)
        {
            return base64URL(filename);
        }
        public string getTitle(string fileCode)
        {
            return rebase64URL(fileCode);
        }
    }
}