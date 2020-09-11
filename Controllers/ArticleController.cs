using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

using KoroneLibrary.Models;
using Microsoft.AspNetCore.Authentication;

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

        // GET: ArticleController
        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id)) { return View(new Article()); }
            Article article = dataServer.GetArticle(id);
            Logger.Info($"Article {article.Title} visited, with uuid {article.Uuid}");
            return View(article);
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
        public ActionResult Edit(int id)
        {
            return View();
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
