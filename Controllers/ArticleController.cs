using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

using KoroneLibrary.Models;

namespace KoroneLibrary.Controllers
{
    public class ArticleController : Controller
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly Search search;

        public ArticleController(Search search)
        {
            this.search = search;
        }

        // GET: ArticleController
        public ActionResult Index()
        {
            Logger.Info("index visited.");
            Article article = new Article
            {
                Author = "a",
                Body = "b",
                Title = "c"
            };
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
            //Search result = search.SearchDetailsMock();
            IList<Article> result = search.AdvancedSearch(searchword);
            if (!string.IsNullOrEmpty(searchword))
            { ViewData["Search"] = true; }
            else { ViewData["Search"] = false; }
            return View(result);
        }
    }
}
