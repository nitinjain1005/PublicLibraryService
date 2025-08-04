using Moq;
using PublicLibraryService.Application.Handlers;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;

namespace PublicLibraryService.UnitTests.Application.Handlers
{
    public class GetMostBorrowedBooksHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IGenericRepository<Book>> _mockBookRepo;
        private readonly Mock<IBookLendingRepository> _mockLendingRepo;
        private readonly GetMostBorrowedBooksHandler _handler;

        public GetMostBorrowedBooksHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBookRepo = new Mock<IGenericRepository<Book>>();
            _mockLendingRepo = new Mock<IBookLendingRepository>();

            _mockUnitOfWork.Setup(u => u.Books).Returns(_mockBookRepo.Object);
            _mockUnitOfWork.Setup(u => u.BookLendings).Returns(_mockLendingRepo.Object);

            _handler = new GetMostBorrowedBooksHandler(_mockUnitOfWork.Object);
        }
        [Fact]
        public async Task Handle_ReturnsTopBorrowedBooks()
        {
            // Arrange
            var books = new List<Book>
            {
                new() { Id = 1, Title = "Book A", Author = "Author A",TotalPages=254,PublishedDate= new DateTime(2015, 12, 31) },
                new () { Id = 2, Title = "Book B", Author = "Author B" ,TotalPages=354,PublishedDate= new DateTime(2014, 12, 31)}
            };

            var lendings = new List<BookLending>
            {
                new() { Id = 1,BookId = 1 ,BorrowerId=1,BorrowedOn=new DateTime(2018, 12, 31)},
                new() { Id = 2,BookId = 1 ,BorrowerId=2,BorrowedOn=new DateTime(2018, 12, 31)},
                new() { Id = 3,BookId = 2 ,BorrowerId=1,BorrowedOn=new DateTime(2016, 12, 31)}
            };

            _mockBookRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(books);
            _mockLendingRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(lendings);

            var query = new GetMostBorrowedBooksQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1, result[0].BookId); // Book with most lends first
            Assert.Equal(2, result[0].BorrowCount);
        }
    }
}
