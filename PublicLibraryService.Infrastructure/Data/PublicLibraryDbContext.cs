using Microsoft.EntityFrameworkCore;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Infrastructure.DataSeeds;

namespace PublicLibraryService.Infrastructure.Data
{
    public class PublicLibraryDbContext : DbContext
    {
        public PublicLibraryDbContext(DbContextOptions<PublicLibraryDbContext> options)
            : base(options) { }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<BookInventory> BookInventories => Set<BookInventory>();
        public DbSet<BookLending> BookLendings => Set<BookLending>();
        public DbSet<Borrower> Borrowers => Set<Borrower>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure keys and relationships
            // Configure relationships and constraints

            // Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Author).IsRequired();
                entity.Property(e => e.PublishedDate).IsRequired();
                entity.Property(e => e.TotalPages).IsRequired();

                entity.HasOne(e => e.Inventory)
                      .WithOne(i => i.Book)
                      .HasForeignKey<BookInventory>(i => i.BookId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.BookLendings)
                      .WithOne(bl => bl.Book)
                      .HasForeignKey(bl => bl.BookId);
            });

            // BookInventory
            modelBuilder.Entity<BookInventory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasKey(e => e.BookId);
                entity.Property(e => e.TotalCopies);
            });

            // Borrower
            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.Email).IsRequired();

                entity.HasMany(b => b.BookLendings)
                      .WithOne(bl => bl.Borrower)
                      .HasForeignKey(bl => bl.BorrowerId);
            });

            modelBuilder.Entity<BookLending>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BorrowedOn).IsRequired();
                entity.Property(e => e.ReturnedOn).IsRequired(false);
                entity.HasOne(bl => bl.Book)
                      .WithMany(b => b.BookLendings)
                      .HasForeignKey(bl => bl.BookId);

                entity.HasOne(bl => bl.Borrower)
                      .WithMany(br => br.BookLendings)
                      .HasForeignKey(bl => bl.BorrowerId);
            });

            // Seed data
            DataSeed.Seed(modelBuilder);
        }

    }
}

