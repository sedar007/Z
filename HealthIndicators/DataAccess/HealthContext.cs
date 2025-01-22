using Common;
using Common.DAO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess
{
    public class HealthContext : DbContext
    {
  

        public DbSet<UserDAO> Users { get; set; }

      


        private string SQLConnectionString;

        public HealthContext(IOptions<AppSettings> options)
        {
            SQLConnectionString = options.Value.SQLConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(SQLConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            var UserDAOBuilder = modelBuilder.Entity<UserDAO>();
           

          
            UserDAOBuilder.HasKey(x => x.Id);
        }
    }
}