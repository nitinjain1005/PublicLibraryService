using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.TestData.Abstraction;

namespace PublicLibraryService.TestData.Seeders;
/// <summary>
/// This class is being used for Data seed.
/// </summary>
public class TestDataSeeder : ITestDataSeeder
{
    public async Task SeedAsync(IUnitOfWork unitOfWork)
    {
        var borrower1 = new Borrower { Id = 45, Name = "Willi Alice", Age = 36, Email = "alice.johnson@example.com" };
        var borrower2 = new Borrower { Id = 42, Name = "Nitin Irani", Age = 30, Email = "ntin.irani@example.com" };
        var borrower3 = new Borrower { Id = 42, Name = "Rj Smith", Age = 32, Email = "Rj.smith@example.com" };
        await unitOfWork.Borrowers.AddRangeAsync(new List<Borrower>() { borrower1, borrower2, borrower3 });
        await unitOfWork.CommitAsync();

        var book1 = new Book
        {
            Id = 85,
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
        var book3 = new Book
        {
            Id = 87,
            Title = "C# net9",
            Author = "Bret Lee",
            PublishedDate = new DateTime(1960, 7, 11),
            TotalPages = 5
        };

        await unitOfWork.Books.AddRangeAsync(new List<Book>() { book1, book2,book3 });
        await unitOfWork.CommitAsync();

        await unitOfWork.BookInventories.AddRangeAsync(new List<BookInventory>()
        {
            new BookInventory
        {
            BookId = book1.Id,
            TotalCopies = 10
        },

        new BookInventory
        {
            BookId = book2.Id,
            TotalCopies = 100
        }});

        await unitOfWork.CommitAsync();


        await unitOfWork.BookLendings.AddRangeAsync(new List<BookLending>()
        { 
            new BookLending
        {
            BookId = book1.Id,
            BorrowerId = borrower1.Id,
            BorrowedOn = DateTime.UtcNow.AddDays(-25)
        }, 
            new BookLending
        {
            BookId = book2.Id,
            BorrowerId = borrower2.Id,
            BorrowedOn = DateTime.UtcNow.AddDays(-25),
            ReturnedOn = DateTime.UtcNow.AddDays(-1)
        },
            new BookLending
        {
            BookId = book2.Id,
            BorrowerId = borrower1.Id,
            BorrowedOn = DateTime.UtcNow.AddDays(-25)
        },
             new BookLending
            {
            BookId = book2.Id,
            BorrowerId = borrower1.Id,
            BorrowedOn = DateTime.UtcNow.AddDays(-25)
        }});

        await unitOfWork.CommitAsync();

    }
}