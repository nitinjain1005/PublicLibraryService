using FluentAssertions;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.SystemTests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.SystemTests.Controllers
{
    public class BookControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public BookControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllBooks_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/v1/Books/availability?bookId=1");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var books = await response.Content.ReadFromJsonAsync<AvailableBook>();
            books.Should().NotBeNull();
        }
    }
}
