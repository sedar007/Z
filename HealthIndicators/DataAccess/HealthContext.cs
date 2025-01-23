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
            modelBuilder.Entity<UserDAO>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserDAO>()
                .HasMany(u => u.WellnessMetrics)
                .WithOne(w => w.User)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration pour WellnessMetricsDAO
            modelBuilder.Entity<WellnessMetricsDAO>()
                .HasKey(w => w.Id);

            modelBuilder.Entity<WellnessMetricsDAO>()
                .HasOne(w => w.User)
                .WithMany(u => u.WellnessMetrics)
                .HasForeignKey(w => w.UserId);
        }
    }
    
}