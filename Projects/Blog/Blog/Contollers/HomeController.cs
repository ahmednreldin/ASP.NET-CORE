using Blog.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Contollers
{
    public class HomeController : Controller
    {
        private readonly IRepository _db;

        public HomeController(IRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var posts = _db.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _db.GetPost(id);
            return View(post);
        }
        
 
    }
}
