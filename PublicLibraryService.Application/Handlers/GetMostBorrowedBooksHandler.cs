using AutoMapper;
using MediatR;
using PublicLibraryService.Application.Models.Response;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Domain.Interfaces;

namespace PublicLibraryService.Application.Handlers
{
    /// <summary>
    /// Handler for retrieving the most borrowed books in the library.(What are the most borrowed books? )
    /// </summary>
    public class GetMostBorrowedBooksHandler : IRequestHandler<GetMostBorrowedBooksQuery, List<MostBorrowedBook>>
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public GetMostBorrowedBooksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }


        public async Task<List<MostBorrowedBook>> Handle(GetMostBorrowedBooksQuery request, CancellationToken cancellationToken)
        {
            // 1. Get all book lendings grouped by BookId with count
            var lendings = await _unitOfWork.BookLendings.GetAllAsync();

            var grouped = lendings
                .GroupBy(bl => bl.BookId)
                .Select(g => new { BookId = g.Key, BorrowCount = g.Count() })
                .OrderByDescending(x => x.BorrowCount)
                .ToList();

            // 2. Get all books to join for Title and Author info
            var books = await _unitOfWork.Books.GetAllAsync();

            // 3. Join grouped counts with books
            var result = (from g in grouped
                          join b in books on g.BookId equals b.Id
                          select new MostBorrowedBook
                          {
                              BookId = b.Id,
                              Title = b.Title,
                              Author = b.Author,
                              BorrowCount = g.BorrowCount
                          }).ToList();

            return result;
        }
    }
    }
