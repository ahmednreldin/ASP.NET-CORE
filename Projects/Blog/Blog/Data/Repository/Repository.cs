using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _db;

        public Repository(AppDbContext db)
        {
            _db = db;
        }
        public void AddPost(Post post)
        {
            _db.Posts.Add(post);
            SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
            return _db.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            return _db.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _db.Posts.Remove(GetPost(id));
            SaveChanges();
        }

        public void UpdatePost(Post post)
        {
            _db.Posts.Update(post);
            SaveChanges();
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }

    }

}