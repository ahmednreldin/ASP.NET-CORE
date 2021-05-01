
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepositories<Author>
    {
        List<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author {Id = 1 ,FullName = "Martin Fowler" },
                new Author {Id = 2 ,FullName = "Robert C. Martin" },
                new Author {Id = 3 ,FullName = "ian Sommerville" },
            };
        }
        public void Add(Author entity)
        {
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id == id );
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author newAuthor)
        {
            var author = Find(id);
            author.FullName = newAuthor.FullName;
        }
    }
}
