using MediatR;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Interfaces;

namespace PublicLibraryService.Application.Handlers
{
    /// <summary>
    ///  Handler to get the availability of a book (How many copies of a specific book are currently borrowed vs. available?)
    /// </summary>
    
    public class GetBookAvailabilityHandler : IRequestHandler<GetBookAvailabilityQuery, AvailableBook>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBookAvailabilityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<AvailableBook> Handle(GetBookAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.Filter.BookId);
            if (book == null)
                return null!;

            var inventory = await _unitOfWork.BookInventories.FindAsync(bi => bi.BookId == request.Filter.BookId);
            var bookInventory = inventory.FirstOrDefault();

            if (bookInventory == null)
                return null!;

            // Count how many copies currently borrowed (not returned)
            var borrowedCount = await _unitOfWork.BookLendings
                .FindAsync(bl => bl.BookId == request.Filter.BookId && bl.ReturnedOn == null);

            int borrowed = borrowedCount.Count();

            return new AvailableBook
            {
                BookId = request.Filter.BookId,
                Title = book.Title,
                TotalCopies = bookInventory.TotalCopies,
                BorrowedCopies = borrowed,
                AvailableCopies = bookInventory.TotalCopies - borrowed
            };
        }
    }
}
