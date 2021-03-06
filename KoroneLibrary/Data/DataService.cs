﻿//using AspNetCore;
using Microsoft.AspNetCore.Authentication;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace KoroneLibrary.Data
{
    public class DataService
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static readonly string DATA_FOLDER = "./Lib";
        public Dictionary<string, Article> ArticleDictionary { get; }

        public DataService()
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
                        article.Filepath = filename;
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
        public string Save(Article article)
        {
            if (string.IsNullOrEmpty(article.Uuid))
            { article.Uuid = Guid.NewGuid().ToString(); }
            if (string.IsNullOrEmpty(article.Filepath))
            { article.Filepath = Path.Join(DATA_FOLDER, article.FileName); }
            if (!ArticleDictionary.ContainsKey(article.Uuid)) { ArticleDictionary.Add(article.Uuid, article); }
            else { ArticleDictionary[article.Uuid] = article; }
            try {
                File.WriteAllText(article.Filepath, JsonSerializer.Serialize(article));
                return article.Uuid;
            }
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
        public void Delete(string uuid)
        {
            if (string.IsNullOrEmpty(uuid)) { return; }
            if (ArticleDictionary.ContainsKey(uuid))
            {
                if (File.Exists(ArticleDictionary[uuid].Filepath))
                {
                    try
                    {
                        File.Delete(ArticleDictionary[uuid].Filepath);
                    }
                    catch (Exception e)
                    {
                        Logger.Error($"删除文件出错: {uuid}\n{e.Message}\n{e.StackTrace}");
                        throw e;
                    }

                }
                ArticleDictionary.Remove(uuid);
            }

        }

        public string Update(Article article)
        {
            if (!string.IsNullOrEmpty(article.Uuid))
            {
                if (ArticleDictionary.ContainsKey(article.Uuid))
                { Delete(ArticleDictionary[article.Uuid]); }
            }
            else
            { article.Uuid = Guid.NewGuid().ToString(); }
            Save(article);
            return article.Uuid;
        }
    }
}