using Microsoft.EntityFrameworkCore;

namespace MyPortfolio.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext() : base()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Admin> admins { get; set; }
        public DbSet<PortfolioItem> portfolioItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    Id = 1,
                    Name = "Abdelrahman Haroon",
                    Aavatr = "avatar.png",
                    Address = "Cairo/Egypt",
                    Portfolio = "Asp.Net Developer"
                }
            );
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("server=DESKTOP-6PD4T3T\\SQLEXPRESS;Database=Portfolio;Integrated Security=True;TrustServerCertificate=True;"));
        }


    }
}
