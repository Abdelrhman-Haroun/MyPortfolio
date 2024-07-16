using MyPortfolio.IRepository;
using MyPortfolio.Models;

namespace MyPortfolio.Repository
{
    public class PortfolioItemRepository : IPorfolioItemRepository
    {
        AppDbContext db;
        public PortfolioItemRepository(AppDbContext db)
        {
            this.db = db;
        }
        public List<PortfolioItem> GetAll()
        {
            return db.portfolioItems.ToList();
        }
        public PortfolioItem GetById(int id)
        {
            return db.portfolioItems.FirstOrDefault(x => x.Id == id);
        }
        public Admin GetAdmin()
        {
            return db.admins.FirstOrDefault();
        }
        public void Insert(PortfolioItem portfolioItem)
        {
            db.portfolioItems.Add(portfolioItem);
            db.SaveChanges();
        }
        public void Update(PortfolioItem portfolioItem)
        {

            db.portfolioItems.Update(portfolioItem);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            db.portfolioItems.Remove(GetById(id));
            db.SaveChanges();
        }

    }
}
