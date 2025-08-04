using MediatR;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Handlers
{
    /// <summary>
    /// What other books were borrowed by individuals who borrowed a particular book? Done
    /// </summary>
    public class GetRelatedBorrowedBooksHandler : IRequestHandler<GetRelatedBorrowedBooksQuery, List<RelatedBorrowedBook>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRelatedBorrowedBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RelatedBorrowedBook>> Handle(GetRelatedBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            var lendings = await _unitOfWork.BookLendings.GetAllAsync();    

            var targetBorrowerIds = lendings
                .Where(l => l.BookId == request.Filter.BookId)
                .Select(l => l.BorrowerId)
                .Distinct()
                .ToList();

            var allBooks = await _unitOfWork.Books.GetAllAsync();
            var borrowers = await _unitOfWork.Borrowers.GetAllAsync();

            var relatedBorrowedBooks = lendings
                .Where(l => targetBorrowerIds.Contains(l.BorrowerId) && l.BookId != request.Filter.BookId)
                .Select(l => new RelatedBorrowedBook
                {
                    BorrowerName = borrowers.First(b => b.Id == l.BorrowerId).Name,
                    BookTitle = allBooks.First(b => b.Id == l.BookId).Title,
                    BorrowedOn = l.BorrowedOn
                })
                .ToList();

            return relatedBorrowedBooks;
        }
    }
}
