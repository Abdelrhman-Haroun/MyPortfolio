using Microsoft.AspNetCore.Mvc;
using MyPortfolio.IRepository;
using MyPortfolio.ViewModel;

namespace MyPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPorfolioItemRepository portfolioItem;
        public HomeController(IPorfolioItemRepository _portfolioItem)
        {
            portfolioItem = _portfolioItem;
        }
        public IActionResult Index()
        {
            HomeViewModel homeView = new HomeViewModel
            {
                admin = portfolioItem.GetAdmin(),
                items = portfolioItem.GetAll()
            };
            return View(homeView);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
