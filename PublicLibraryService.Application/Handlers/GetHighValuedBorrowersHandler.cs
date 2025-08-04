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
    /// Which users have borrowed the most books within a given time frame? 
    /// </summary>
    public class GetHighValuedBorrowersHandler : IRequestHandler<GetHighValuedBorrowersQuery, List<HighValuedBorrower>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHighValuedBorrowersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<HighValuedBorrower>> Handle(GetHighValuedBorrowersQuery request, CancellationToken cancellationToken)
        {
            var lendings = await _unitOfWork.BookLendings
                .FindAsync(l => l.BorrowedOn >= request.Filter.FromDate && l.BorrowedOn <= request.Filter.ToDate);

            var borrowers = await _unitOfWork.Borrowers.GetAllAsync();

            var grouped = lendings
                .GroupBy(l => l.BorrowerId)
                .Select(group => new
                {
                    BorrowerId = group.Key,
                    Total = group.Count()
                })
                .OrderByDescending(x => x.Total)
                .ToList();

            var result = grouped.Select(g =>
            {
                var borrower = borrowers.FirstOrDefault(b => b.Id == g.BorrowerId);
                return new HighValuedBorrower
                {
                    BorrowerId = g.BorrowerId,
                    Name = borrower?.Name ?? "Unknown",
                    TotalBorrowedBooks = g.Total
                };
            }).ToList();

            return result;
        }
    }
}
