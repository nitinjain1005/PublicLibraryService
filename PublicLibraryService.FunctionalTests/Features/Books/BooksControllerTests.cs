using FluentAssertions;
using PublicLibraryService.FunctionalTests.TestSetup;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PublicLibraryService.FunctionalTests.Features.Books
{
    [Collection("FunctionalTestCollection")]
    public class BooksControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public BooksControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetMostBorrowedBooks_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/v1/Books/most-borrowed");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var books = await response.Content.ReadFromJsonAsync<List<object>>();
            books.Should().NotBeNull();
        }

        [Fact]
        public async Task GetBookAvailability_ValidBookId_ShouldReturnOk()
        {
            var validBookId = 85; // Ensure book with ID 1 is seeded

            var response = await _client.GetAsync($"/v1/Books/availability?BookId={validBookId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetBookAvailability_InvalidBookId_ShouldReturnNotFound()
        {
            var invalidBookId = 99990;

            var response = await _client.GetAsync($"/v1/Books/availability?BookId={invalidBookId}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetBorrowedBooksByBorrowerId_ShouldReturnOk()
        {
            var borrowerId = 45; // Seeded borrower
            var fromDate = DateTime.UtcNow.AddDays(-25).ToString("yyyy-MM-dd");
            string? toDate = null;

            var url = $"/v1/Books/borrowed-books-by-borrowerid?borrowerId={borrowerId}&fromDate={fromDate}&toDate={toDate}";

            var response = await _client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var books = await response.Content.ReadFromJsonAsync<List<object>>();
            books.Should().NotBeNull();
        }

        [Fact]
        public async Task GetRelatedBorrowedBooks_ShouldReturnOk()
        {
            var bookId = 85; // Seeded book with lendings

            var response = await _client.GetAsync($"/v1/Books/related-borrowed-books?BookId={bookId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
