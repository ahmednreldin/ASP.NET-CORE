using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Contollers
{
    [Authorize]

    public class PanelController : Controller
    {
        private readonly IRepository _db;
        private readonly IFileManager _fileManager;

            public PanelController(IRepository db , IFileManager fileManager)
            {
                _db = db;
                _fileManager = fileManager;
            }
        public IActionResult Index()
        {
            var posts = _db.GetAllPosts();
            return View(posts);
        }


        [HttpGet]
        public IActionResult Edit(int? id) // make id nullable 
        {
            if (id == null)
                return View(new PostViewModel());

            var post = _db.GetPost((int)id);
            return View(new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
            });
        }

        [HttpPost]
        public IActionResult Edit(PostViewModel model)
        {
            var post = new Post
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                Image = _fileManager.SaveImage(model.Image)
            };
            if (post.Id > 0)
                _db.UpdatePost(post);
            else
                _db.AddPost(post);

            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            _db.RemovePost(id);
            return RedirectToAction("index");
        }

    }

    
}
