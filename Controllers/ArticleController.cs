using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

using KoroneLibrary.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace KoroneLibrary.Controllers
{
    public class ArticleController : Controller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly DataServer dataServer;

        private readonly Search search;

        public ArticleController(DataServer dataServer, Search search)
        {
            this.dataServer = dataServer;
            this.search = search;
        }

        //[Authorize]
        // GET: ArticleController
        public ActionResult Index(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) { throw new Exception("未指定文档uuid"); }
                Article article = dataServer.GetArticle(id);
                Logger.Info($"Article {article.Title} visited, with uuid {article.Uuid}");
                return View(article);
            }
            catch (Exception e)
            {
                Article article = new Article
                {
                    Title = "Oops! 该文档不存在",
                    Body = $"该文档可能已被他人删除、移动，被不规范地导入，也可能是系统故障。请尝试重新搜索并访问这个文档，如果所有文档都不可访问，请在“关于”页面查看帮助或联系系统管理员和开发者。",
                    Node = new Dictionary<string, string>()
                };
                article.Node.Add("错误信息", e.Message);
                article.Node.Add("错误堆栈", e.StackTrace);
                Logger.Error($"{e.Message}\n{e.StackTrace}");
                return View(article);
            }
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View("Index");
        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) { throw new Exception("未指定文档uuid"); }
                Article article = dataServer.GetArticle(id);
                Logger.Info($"Article {article.Title} visited, with uuid {article.Uuid}");
                return View(article);
            }
            catch (Exception e)
            {
                Article article = new Article
                {
                    Title = "Oops! 该文档不存在",
                    Body = $"该文档可能已被他人删除、移动，被不规范地导入，也可能是系统故障。请尝试重新搜索并访问这个文档，如果所有文档都不可访问，请在“关于”页面查看帮助或联系系统管理员和开发者。",
                    Node = new Dictionary<string, string>()
                };
                article.Node.Add("错误信息", e.Message);
                article.Node.Add("错误堆栈", e.StackTrace);
                Logger.Error($"{e.Message}\n{e.StackTrace}");
                return View(article);
            }
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string searchword = null)
        {
            IList<Article> result = search.AdvancedSearch(searchword);
            Logger.Info($"search {searchword},{result.Count} results found");
            if (!string.IsNullOrEmpty(searchword))
            { ViewData["Search"] = true; }
            else { ViewData["Search"] = false; }
            return View(result);
        }
    }
}
