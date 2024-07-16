using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.IRepository;
using MyPortfolio.Models;
using MyPortfolio.ViewModel;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IPorfolioItemRepository _portfolio;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting;

        public PortfolioItemsController(IPorfolioItemRepository portfolio, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            _portfolio = portfolio;
            _hosting = hosting;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_portfolio.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.GetById(Id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?Link((Id))=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortfolioViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }

                PortfolioItem portfolioItem = new PortfolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };

                _portfolio.Insert(portfolioItem);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public IActionResult Edit(int Id)
        {
            if (((Id)) == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.GetById(Id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                Id = portfolioItem.Id,
                Description = portfolioItem.Description,
                ProjectName = portfolioItem.ProjectName,
                ImageUrl = portfolioItem.ImageUrl
            };

            return View(portfolioViewModel);
        }

        // POST: PortfolioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?Link((Id))=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, PortfolioViewModel model)
        {
            if (Id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    PortfolioItem portfolioItem = new PortfolioItem
                    {
                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };

                    _portfolio.Update(portfolioItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
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
            return View(model);
        }

        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.GetById(Id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Id)
        {
            _portfolio.Delete(Id);
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(int Id)
        {
            return _portfolio.GetAll().Any(e => e.Id == Id);
        }
    }
}