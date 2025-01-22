using Common;
using Common.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess
{
    public class HealthContext : DbContext
    {
        public DbSet<GameDao> Games { get; set; }
        public DbSet<ProjectCategoryDao> Categories { get; set; }

        public DbSet<ExperienceDao> Experiences { get; set; }

        public DbSet<CompetenceDao> Competences { get; set; }

        public DbSet<FormationDao> Formations { get; set; }

        public DbSet<UserDao> Users { get; set; }

        public DbSet<MenuDao> Menus { get; set; }


        private string SQLConnectionString;

        public HealthContext(IOptions<AppSettings> options)
        {
            SQLConnectionString = options.Value.SQLConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(SQLConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var CategoriesDaoBuilder = modelBuilder.Entity<ProjectCategoryDao>();
            var ExperiencesDaoBuilder = modelBuilder.Entity<ExperienceDao>();
            var CompetenceDaoBuilder = modelBuilder.Entity<CompetenceDao>();
            var UserDaoBuilder = modelBuilder.Entity<UserDao>();
            var FormationsDaoBuilder = modelBuilder.Entity<FormationDao>();
            var MenusDaoBuilder = modelBuilder.Entity<MenuDao>();

            CategoriesDaoBuilder.HasKey(x => x.Id);
            ExperiencesDaoBuilder.HasKey(x => x.Id);
            CompetenceDaoBuilder.HasKey(x => x.Id);
            FormationsDaoBuilder.HasKey(x => x.Id);
            UserDaoBuilder.HasKey(x => x.Id);
            MenusDaoBuilder.HasKey(x => x.Id);
        }
    }
}