using Core.Entites;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase 
    {
        private readonly IUnitOfWork _todo;

        public ApiController(IUnitOfWork todo)
        {
            _todo = todo;
        }

        [HttpGet]
        public List<TodoItem> Index()
        {
            return _todo.Entity.GetAllItems().ToList();
        } 
        [HttpGet("{id}")]
        public TodoItem GetItemById(int id)
        {
            return _todo.Entity.GetItemById(id);
        }

    }
}
