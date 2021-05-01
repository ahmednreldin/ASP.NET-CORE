using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookstoreRepositories<Author> authorRepository;

        public AuthorController(IBookstoreRepositories<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            var authors = authorRepository.List();
            return View(authors);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            var authors = authorRepository.Find(id);

            return View(authors);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorRepository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {

                Author oldAuthor = authorRepository.Find(id);
                oldAuthor.FullName = author.FullName;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
