using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.ObjectHelpers.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepositories<Book> bookRepository;
        private readonly IBookstoreRepositories<Author> authorRepository;
        private readonly IHostingEnvironment hosting;

        public BookController(IBookstoreRepositories<Book> bookRepository, IBookstoreRepositories<Author> authorRepository,IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository; 
            this.hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };


            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {

            if (ModelState.IsValid)
            {
                string fileName = UploadFile(model.File) == null ? string.Empty : UploadFile(model.File) ;
                
                try
                {

                    if (model.AuthorId == -1)
                    {
                        // viewbag => Dynamic Properties send data between controller and view
                        // .Message we put the property we need for example we can replace Message by any other name 
                        ViewBag.Message = "Please select an author from the list ! ";

                       return View(GetAllAuthors());
                    }

                    var author = authorRepository.Find(model.AuthorId);


                    Book book = new Book();

                    book.Id = model.BookId;
                    book.Title = model.Title;
                    book.Description = model.Description;
                    book.Author = author;
                    book.ImageUrl = fileName;


                    bookRepository.Add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            ModelState.AddModelError("", "You have to fill all required fields");
            return View(GetAllAuthors());

        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);

            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = FillSelectList(),
                ImageUrl = book.ImageUrl
            };
            
            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel model)
        {
            string fileName = UploadFile(model.File,model.ImageUrl);
        

            if (model.AuthorId == -1 )
            {

                ViewBag.Message = "Please select an author from the list ! ";
                return View(GetAllAuthors());
            }

            try
            {
                var author = authorRepository.Find(model.AuthorId);

                Book book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = author,
                    ImageUrl = fileName
                };

                bookRepository.Update(model.BookId,book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return vmodel;
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try

            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Method to add Default unSelect value for authors
        List<Author> FillSelectList()
        {
            var authors = authorRepository.List().ToList();

            authors.Insert(0, new Author { Id = -1, FullName = "---- Please Select an author ---" });

            return authors;
        }

        // File 
        string UploadFile(IFormFile file )
        {

            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
                return file.FileName;
            }
              return null;
        }
        string UploadFile(IFormFile file ,string ImageUrl)
        {

            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, ImageUrl);

                if (newPath != oldPath)
                {
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                    return file.FileName;
            }
                return ImageUrl;

        }
        
        public ActionResult Search(string term)
        {
            var reuslt = bookRepository.Search(term);

            return View("index", reuslt);
        }
    }
}
