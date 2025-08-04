using Moq;
using PublicLibraryService.Application.Handlers;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using System.Linq.Expressions;
using PublicLibraryService.Application.Models.Request;

namespace PublicLibraryService.UnitTests.Application.Handlers
{

    public class GetBorrowedBooksByBorrowerIdHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBookLendingRepository> _mockBookLendingRepo;
        private readonly Mock<IGenericRepository<Book>> _mockBookRepo;
        private readonly GetBorrowedBooksByBorrowerIdHandler _handler;

        public GetBorrowedBooksByBorrowerIdHandlerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBookLendingRepo = new Mock<IBookLendingRepository>();
            _mockBookRepo = new Mock<IGenericRepository<Book>>();

            _mockUnitOfWork.Setup(u => u.BookLendings).Returns(_mockBookLendingRepo.Object);
            _mockUnitOfWork.Setup(u => u.Books).Returns(_mockBookRepo.Object);

            _handler = new GetBorrowedBooksByBorrowerIdHandler(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_ReturnsBorrowedBooksList_WhenDataExists()
        {
            // Arrange
            int borrowerId = 1;
            var bookLendings = new List<BookLending>
        {
            new BookLending { BookId = 101, BorrowerId = borrowerId, BorrowedOn = new DateTime(2023, 1, 1), ReturnedOn = null },
            new BookLending { BookId = 102, BorrowerId = borrowerId, BorrowedOn = new DateTime(2023, 2, 1), ReturnedOn = null },
            new BookLending { BookId = 102, BorrowerId = 2, BorrowedOn = new DateTime(2024, 2, 1), ReturnedOn = null }
        };

            var books = new List<Book>
        {
            new Book { Id = 101, Title = "Book A", Author = "Author A" ,PublishedDate = new DateTime(2015, 12, 31),TotalPages = 250},
            new Book { Id = 102, Title = "Book B", Author = "Author B" ,PublishedDate = new DateTime(2015, 12, 31), TotalPages = 252}
        };
            var BorrowedOn1 = new DateTime(2023, 1, 1);


            _mockBookRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

            _mockBookLendingRepo
                .Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<BookLending, bool>>>()))
                .ReturnsAsync(bookLendings);

            var query = new GetBorrowedBooksByBorrowerIdQuery(new BorrowedBooksRequest() { BorrowerId = borrowerId, FromDate = BorrowedOn1, ToDate = null });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(result, b => b.Title == "Book A");
            Assert.Contains(result, b => b.Title == "Book B");
        }


        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoBorrowedBooksExist()
        {
            // Arrange
            int borrowerId = 2;
            var BorrowedOn2 = new DateTime(2025, 1, 1);
            _mockBookLendingRepo
                .Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<BookLending, bool>>>()))
                .ReturnsAsync(new List<BookLending>());

            var query = new GetBorrowedBooksByBorrowerIdQuery(new BorrowedBooksRequest() { BorrowerId = borrowerId, FromDate = BorrowedOn2, ToDate = null });

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}


