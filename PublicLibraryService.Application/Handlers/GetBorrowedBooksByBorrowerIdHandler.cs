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
    /// What books has a particular user borrowed during a specified period
    /// </summary>
    public class GetBorrowedBooksByBorrowerIdHandler : IRequestHandler<GetBorrowedBooksByBorrowerIdQuery, List<BorrowedBook>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBorrowedBooksByBorrowerIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BorrowedBook>> Handle(GetBorrowedBooksByBorrowerIdQuery request, CancellationToken cancellationToken)
        {
            var lendings = await _unitOfWork.BookLendings
                .FindAsync
                (
                    l =>l.BorrowerId == request.Filter.BorrowerId &&
                        l.BorrowedOn >= request.Filter.FromDate &&
                        (
                        request.Filter.ToDate == null || (l.ReturnedOn != null && l.ReturnedOn <= request.Filter.ToDate )
                        )  
                 );

            var books = await _unitOfWork.Books.GetAllAsync();

            var result = lendings.Join(
                books,
                lending => lending.BookId,
                book => book.Id,
                (lending, book) => new BorrowedBook
                {
                    BookId = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    BorrowedOn = lending.BorrowedOn,
                    ReturnedOn = lending.ReturnedOn
                }).ToList();

            return result;
        }
    }
}
