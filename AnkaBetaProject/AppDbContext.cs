using AnkaBetaProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AnkaBetaProject
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<City> Cities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tablo ve sütun isimlerini belirleme
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Writer>().ToTable("Writers");
            modelBuilder.Entity<Library>().ToTable("Libraries");
            modelBuilder.Entity<City>().ToTable("Cities");

            modelBuilder.Entity<City>()
                .HasMany(c => c.Libraries)
                .WithOne(l => l.City)
                .HasForeignKey(l => l.CityId)
                                .OnDelete(DeleteBehavior.NoAction);
            ;

            modelBuilder.Entity<Library>()
                .HasOne(l => l.City)
                .WithMany(c => c.Libraries)
                .HasForeignKey(l => l.CityId)
                .OnDelete(DeleteBehavior.NoAction);
            ;

            modelBuilder.Entity<Library>()
                .HasMany(l => l.Books)
                .WithOne(b => b.Library)
                .HasForeignKey(b => b.LibraryId)
                .OnDelete(DeleteBehavior.NoAction);
            ;

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Library)
                .WithMany(l => l.Books)
                .HasForeignKey(b => b.LibraryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Writer>()
                .HasMany(w => w.Books)
                .WithOne(b => b.Writer)
                .OnDelete(DeleteBehavior.NoAction);
            ;
        }
    }
}
