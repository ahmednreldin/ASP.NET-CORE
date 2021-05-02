using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{   
    // <TEntity> Generic datatype 
    public interface IBookstoreRepositories<TEntity>
    {
        // Fetch Element in list<Book> 
        IList<TEntity> List();

        // Search about element ,, return TEntity value 
        TEntity Find(int id);

        // Add element , pass object parameter
        void Add(TEntity entity);

        // Update Element 
        void Update(int id ,TEntity entity);
        // Delete Element
        void Delete(int id);

        // Search Method
        public List<TEntity> Search(string term);

    }
}
