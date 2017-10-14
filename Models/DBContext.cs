using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using Domain.Mapping;


namespace Dao
{
    public class DwDbContext : DbContext
    {
        public DwDbContext(DbContextOptions<DwDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<SurveyEntity> Survey { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.MapUser();
            modelBuilder.MapSurvey();
            base.OnModelCreating(modelBuilder);
        }
    }
}