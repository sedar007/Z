using Common;
using Common.DAO;
using Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAccess
{
    public class HealthContext : DbContext
    {
        public DbSet<UserDAO> Users { get; set; }
        public DbSet<WellnessMetricsDAO> WellnessMetrics { get; set; }
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
            var WellnessMetricsDAOBuilder = modelBuilder.Entity<WellnessMetricsDAO>();
            WellnessMetricsDAOBuilder.HasKey(x => x.Id);
            WellnessMetricsDAOBuilder.HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}