using MediatR;
using PublicLibraryService.Application.Models.Request;
using PublicLibraryService.Application.Models.Response;

namespace PublicLibraryService.Application.Queries
{
    public class GetBorrowedBooksByBorrowerIdQuery : IRequest<List<BorrowedBook>>
    {
        public BorrowedBooksRequest Filter { get; set; }

        public GetBorrowedBooksByBorrowerIdQuery(BorrowedBooksRequest borrowedBooksRequest)
        {
            Filter = borrowedBooksRequest;
        }
    }
}
