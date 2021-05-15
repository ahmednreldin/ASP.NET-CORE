using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository
    {
        // CURD

        List<TodoItem> GetAllItems();
        TodoItem GetItemById(int id);
        void AddItem(TodoItem todoItem);
        void UpdateItem(TodoItem todoItem);
        void DeleteItem(int id);
        
    }
}
