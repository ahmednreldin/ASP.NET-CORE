using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepositories<Book> bookstoreRepository;
        private readonly IBookstoreRepositories<Author> authorRepository;

        public BookController(IBookstoreRepositories<Book> bookstoreRepository,IBookstoreRepositories<Author> authorRepository)
        {
            this.bookstoreRepository = bookstoreRepository;
            this.authorRepository = authorRepository;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookstoreRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookstoreRepository.Find(id);

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = authorRepository.List().ToList()
            };


            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            try
            {
                bookstoreRepository.Add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookstoreRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Book book)
        {
            try
            {
                bookstoreRepository.Update(id, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookstoreRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            try

            {
                bookstoreRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
