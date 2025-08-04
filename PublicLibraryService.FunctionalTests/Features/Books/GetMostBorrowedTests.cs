using FluentAssertions;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.FunctionalTests.TestSetup;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PublicLibraryService.FunctionalTests.Features.Books
{
    public class GetMostBorrowedTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public GetMostBorrowedTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetMostBorrowed_ShouldReturnOkWithData()
        {
            // Act
            var response = await _client.GetAsync("/v1/books/most-borrowed");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var data = await response.Content.ReadFromJsonAsync<List<MostBorrowedBook>>();
            data.Should().NotBeNull();
            data!.Count.Should().BeGreaterThan(0);
        }
    }
}
