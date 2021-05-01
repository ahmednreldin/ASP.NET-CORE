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
                    Id = 1 , Title = "c# programming",Description = "No Description"
                },
                new Book
                {
                    Id = 2 , Title = "Java programming",Description = "Nothing"
                },
                new Book
                {
                    Id = 3 , Title = "Python programming",Description = "Nothing"
                },
            };
        }

        public void Add(Book entity)
        {
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

        public void Update(int id,Book newbook)
        {
            var book = Find(id);
            newbook.Title = book.Title;
            newbook.Description = book.Description;
            newbook.Author = book.Author;
        }
    }
}
