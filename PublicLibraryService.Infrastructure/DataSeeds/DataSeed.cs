using Microsoft.EntityFrameworkCore;
using PublicLibraryService.Domain.Entities;

namespace PublicLibraryService.Infrastructure.DataSeeds
{
    internal static class DataSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    PublishedDate = new DateTime(2025, 7, 25),
                    TotalPages = 464
                },
                new Book
                {
                    Id = 2,
                    Title = "The Pragmatic Programmer",
                    Author = "Andy Hunt & Dave Thomas",
                    PublishedDate = new DateTime(2024, 7, 25),
                    TotalPages= 352
                }
            );
            // Book Inventories
            modelBuilder.Entity<BookInventory>().HasData
                (
                new BookInventory
                {
                    Id = 1,
                    BookId = 1,
                    TotalCopies = 5
                },
                new BookInventory
                {
                    Id = 2,
                    BookId = 2,
                    TotalCopies = 3
                }
            );

            modelBuilder.Entity<Borrower>().HasData(
                new Borrower
                {
                    Id = 1,
                    Name = "Nitin Jain",
                    Age = 30,
                    Email = "nitin@example.com"
                },
                new Borrower
                {
                    Id = 2,
                    Name = "Monika Jain",
                    Age = 28,
                    Email = "monika@example.com"
                },
                new Borrower
                {
                    Id = 3,
                    Name = "Sandeep Jain",
                    Age = 35,
                    Email = "sandeep@example.com"
                }
            );

            modelBuilder.Entity<BookLending>().HasData
                (
                new BookLending
                {
                    Id = 1,
                    BookId = 1,
                    BorrowerId = 1,
                    BorrowedOn = new DateTime(2024, 6, 1),
                    ReturnedOn = null
                },
                new BookLending
                {
                    Id = 2,
                    BookId = 2,
                    BorrowerId = 2,
                    BorrowedOn = new DateTime(2024, 7, 1),
                    ReturnedOn = null
                },
                new BookLending
                {
                    Id = 3,
                    BookId = 1,
                    BorrowerId = 3,
                    BorrowedOn = new DateTime(2024, 7, 1),
                    ReturnedOn = new DateTime(2024, 7, 10)
                }
);
        }
    }
}
