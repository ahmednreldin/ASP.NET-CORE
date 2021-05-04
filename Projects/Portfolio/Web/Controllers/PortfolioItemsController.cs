using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entites;
using Infrastructure;
using Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Core.Interfaces;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IWebHostEnvironment _hosting;
        private readonly IUnitOfWork<PortfolioItem> _portfolio;

        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portfolio, IWebHostEnvironment hosting)
        {
            _hosting = hosting;
            _portfolio = portfolio;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_portfolio.Entity.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);

            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (model.File != null)
                {

                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    fileName = model.File.FileName;
                    string fullPath = Path.Combine(uploads, fileName);

                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));

                }


                PortfolioItem portfolioItem = new PortfolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };

                _portfolio.Entity.Insert(portfolioItem);
                _portfolio.Save();


                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);

            PortfolioViewModel model = new PortfolioViewModel
            {
                Id = portfolioItem.Id,
                ProjectName = portfolioItem.ProjectName,
                Description = portfolioItem.Description,
                ImageUrl = portfolioItem.ImageUrl,
            };

            if (portfolioItem == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioViewModel portfolio)
        {
            if (id != portfolio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = String.Empty;
                    if (portfolio.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        fileName = portfolio.File.FileName;
                        string fullPath = Path.Combine(uploads, fileName);
                        portfolio.File.CopyTo(new FileStream(fullPath, FileMode.Create));

                    }
                    PortfolioItem portfolioItem = _portfolio.Entity.GetById(id);

                    portfolioItem.ProjectName = portfolio.ProjectName;
                    portfolioItem.Description = portfolio.Description;
                    portfolioItem.ImageUrl = portfolio.ImageUrl;

                    _portfolio.Entity.Update(portfolioItem);
                    _portfolio.Save();
                    //_context.Update(portfolioItem);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(portfolio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(portfolio);
        }

        // GET: PortfolioItems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.Save();

            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
