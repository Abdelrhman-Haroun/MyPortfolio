using MyPortfolio.Models;

namespace MyPortfolio.ViewModel
{
    public class HomeViewModel
    {
        public Admin admin { get; set; }
        public List<PortfolioItem> items { get; set; }

    }
}
