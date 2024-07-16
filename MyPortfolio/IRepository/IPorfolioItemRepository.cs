using MyPortfolio.Models;

namespace MyPortfolio.IRepository
{
    public interface IPorfolioItemRepository
    {
        public List<PortfolioItem> GetAll();
        PortfolioItem GetById(int id);
        Admin GetAdmin();
        void Insert(PortfolioItem item);
        void Update(PortfolioItem item);
        void Delete(int id);
    }
}
