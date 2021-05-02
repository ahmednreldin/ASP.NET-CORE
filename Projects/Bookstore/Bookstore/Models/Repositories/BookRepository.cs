using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepositories<Book>
    {

        // create List of type book to store data 
        IList<Book> books;

        // Initialize List 
        public BookRepository()
        {
            books = new List<Book>() {
                new Book
                {
                    Id = 1 , 
                    Title = "c# programming",
                    Description = "No Description",
                    ImageUrl ="c#.png" ,
                    Author = new Author { Id = 1 } ,
                },
                new Book
                {
                    Id = 2 , 
                    Title = "Java programming",
                    Description = "Nothing",
                    ImageUrl ="java.png",
                    Author = new Author { },
                },
                new Book
                {
                    Id = 3 , 
                    Title = "Python programming",
                    Description = "Nothing",
                    ImageUrl ="python.png",
                    Author = new Author { }
                },
            };
        }

        public void Add(Book entity)
        {
            // ID identity increment 
            entity.Id = books.Max(b => b.Id) + 1;
                
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        // return List of books
        public IList<Book> List()
        {
           return books;
        }

        public List<Book> Search(string term)
        {
            throw new NotImplementedException();
        }

        public void Update(int id,Book newbook)
        {
            var book = Find(id);
            book.Title = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImageUrl = newbook.ImageUrl;
        }
    }
}
