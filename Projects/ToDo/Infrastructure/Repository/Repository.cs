using Core.Entites;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _db;
        private DbSet<TodoItem> TodoTable = null;
        public Repository(AppDbContext dbContext)
        {
            _db = dbContext;
            TodoTable = _db.Set<TodoItem>();
        }
        public void AddItem(TodoItem todoItem)
        {
            TodoTable.Add(todoItem);
        }

        public void DeleteItem(int id)
        {
            TodoTable.Remove(GetItemById(id));
        }

        public List<TodoItem> GetAllItems()
        {
            return TodoTable.ToList();
        }

        public TodoItem GetItemById(int id)
        {
            return TodoTable.Find(id);
        }

        public void UpdateItem(TodoItem todoItem)
        {

            TodoTable.Update(todoItem);
        }
    }
}
