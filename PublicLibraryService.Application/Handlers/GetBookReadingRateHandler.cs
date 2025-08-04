using MediatR;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Interfaces;

namespace PublicLibraryService.Application.Handlers
{
    /// <summary>
    /// Estimate the reading rate (pages/day) for a book based on borrow and return times, assuming continuous reading
    /// </summary>
    public class GetBookReadingRateHandler : IRequestHandler<GetBookReadingRateQuery, BookReadingRate>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBookReadingRateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BookReadingRate> Handle(GetBookReadingRateQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.Filter.BookId);
            if (book == null)
                return null!;

            var lendings = await _unitOfWork.BookLendings
                .FindAsync(bl => bl.BookId == request.Filter.BookId && bl.ReturnedOn != null);

            var borrowers = await _unitOfWork.Borrowers.GetAllAsync();

            var response = new BookReadingRate
            {
                BookId = book.Id,
                Title = book.Title,
                TotalPages = book.TotalPages, // assuming this property exists
                ReaderRates = lendings.Select(l =>
                {
                    var borrower = borrowers.FirstOrDefault(b => b.Id == l.BorrowerId);
                    int daysTaken = (l.ReturnedOn!.Value - l.BorrowedOn).Days;
                    return new ReadingRatePerUser
                    {
                        BorrowerName = borrower?.Name ?? "No More active subscription based User",
                        BorrowedOn = l.BorrowedOn,
                        ReturnedOn = l.ReturnedOn.Value,
                        DaysTaken = daysTaken,
                        ReadingRatePerDay = daysTaken > 0 ? Math.Round((double)book.TotalPages / daysTaken, 2) : 0
                    };
                }).ToList()
            };

            return response;

        }
    }
}
