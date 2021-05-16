using Core.Entites;
using Core.Interfaces;
using Core.Interfaces.FileManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _todo;
        private readonly IFileManager _fileManager;

        public HomeController(IUnitOfWork todo,IFileManager fileManager)
        {
            _todo = todo;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var items = _todo.Entity.GetAllItems();
            return View(items);
        }

        public IActionResult View(int id)
        {
            var result = _todo.Entity.GetItemById(id);
            return View(result);
        }

//        [Authorize(Policy = "IsAdmin")]
        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(TodoBindingModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You Have to fill All required fields");
                return View();
            }
            string fileName = "";
            if(model.Image != null)
            {
                fileName = _fileManager.SaveImage(model.Image);
            }

            var todoitems = new TodoItem()
            {
                Title = model.Title,
                Body = model.Body,
                ImageUrl = fileName,
            };

            _todo.Entity.AddItem(todoitems);
            _todo.SaveItem();

            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            _todo.Entity.DeleteItem(id);
            _todo.SaveItem();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = _todo.Entity.GetItemById(id);

            var model = new TodoBindingModel
            {
                Title = result.Title,
                Body = result.Body,
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(TodoBindingModel model)
        {
            var itm = new TodoItem
            {
                Title = model.Title,
                Body = model.Body,
                ImageUrl = _fileManager.SaveImage(model.Image),
                Id = model.Id
            };
            _todo.Entity.UpdateItem(itm);
            _todo.SaveItem();
            return RedirectToAction("Index");
        }


    }
}
