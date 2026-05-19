using Microsoft.EntityFrameworkCore;
using Chinook.Web.Models;

namespace Chinook.Web.Data
{
    public class ChinookContext : DbContext
    {
        public ChinookContext(DbContextOptions<ChinookContext> options) : base(options) { }

        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Genre>().HasKey(g => g.GenreId);
            modelBuilder.Entity<Genre>().Property(g => g.GenreId).HasColumnName("GenreId");
            modelBuilder.Entity<Genre>().Property(g => g.Name).HasColumnName("Name");
        }
    }
}
