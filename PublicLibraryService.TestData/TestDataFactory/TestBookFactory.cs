using Microsoft.EntityFrameworkCore;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Infrastructure.Data;

namespace PublicLibraryService.TestData.TestDataFactory
{
    public static class TestBookFactory
    {
        public static Book TheGreatGatsby => new()
        {
            Id = 85,
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald",
            PublishedDate = new DateTime(1925, 4, 10),
            TotalPages = 5
        };

        public static Book HowtoStart => new()
        {
            Id = 86,
            Title = "How to Start",
            Author = "Harper Lee",
            PublishedDate = new DateTime(1960, 7, 11),
            TotalPages = 5
        };

        public static Book CSharpNet9 => new()
        {
            Id = 87,
            Title = "C# net9",
            Author = "Bret Lee",
            PublishedDate = new DateTime(1960, 7, 11),
            TotalPages = 5
        };

        public static List<Book> GetAll() => [TheGreatGatsby, HowtoStart, CSharpNet9];
        public static Book Create(int id = 1,string title = "Default Book", int totalPages = 340,string author="Nitin1", DateTime publishedDate= default)
        {
            return new Book
            {
                Id = id,
                Title = title,
                TotalPages = totalPages,
                PublishedDate = publishedDate == default ? new DateTime(2023, 1, 1) : publishedDate,
                Author = author

                // Add other defaults here
            };
        }

        public static async Task<Book> CreateAndSeedAsync(
            PublicLibraryDbContext dbContext,
            int id = 1, string title = "Default Book", int totalPages = 340, string author = "Nitin1", DateTime publishedDate = default)
        {
            var book = Create(id, title, totalPages, author, publishedDate);
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();
            return book;
        }


    }

}
