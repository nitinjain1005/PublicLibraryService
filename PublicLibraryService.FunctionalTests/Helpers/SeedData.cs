using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace PublicLibraryService.FunctionalTests.Helpers
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IUnitOfWork unitOfWork)
        {
            // Create Borrowers
            var borrower1 = new Borrower
            {
                Id=45,
                Name = "Alice",
                Age = 36,
                Email = "alice.johnson@example.com"
            };

            var borrower2 = new Borrower
            {
                Id=42,
                Name = "bo",
                Age = 30,
                Email = "bob.smith@example.com"
            };

            await unitOfWork.Borrowers.AddAsync(borrower1);
            await unitOfWork.Borrowers.AddAsync(borrower2);
            await unitOfWork.CommitAsync();

            // Create Books
            var book1 = new Book
            {
                Id=85,
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",               
                PublishedDate = new DateTime(1925, 4, 10),
                TotalPages = 5
            };

            var book2 = new Book
            {
                Id = 86,
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                PublishedDate = new DateTime(1960, 7, 11),
                TotalPages = 5
            };

            await unitOfWork.Books.AddAsync(book1);
            await unitOfWork.Books.AddAsync(book2);
            await unitOfWork.CommitAsync();

            var inventory1 = new BookInventory
            {
                BookId = book1.Id,
                TotalCopies = 10
            };

            await unitOfWork.BookInventories.AddAsync(inventory1);
            var inventory2 = new BookInventory
            {
                BookId = book2.Id,
                TotalCopies = 100
            };

            await unitOfWork.BookInventories.AddAsync(inventory2);
            await unitOfWork.CommitAsync();

            // Create Lending
            var lending1 = new BookLending
            {
                BookId = book1.Id,
                BorrowerId = borrower1.Id,
                BorrowedOn = DateTime.UtcNow.AddDays(-25),
                ReturnedOn = null
            };

            await unitOfWork.BookLendings.AddAsync(lending1);
            var lending2 = new BookLending
            {
                BookId = book2.Id,
                BorrowerId = borrower2.Id,
                BorrowedOn = DateTime.UtcNow.AddDays(-25),
                ReturnedOn = DateTime.UtcNow.AddDays(-1)
            };
            var lending3 = new BookLending
            {
                BookId = book2.Id,
                BorrowerId = borrower1.Id,
                BorrowedOn = DateTime.UtcNow.AddDays(-25),
                ReturnedOn = null
            };

            await unitOfWork.BookLendings.AddAsync(lending3);
            await unitOfWork.CommitAsync();
        }
    }
}
