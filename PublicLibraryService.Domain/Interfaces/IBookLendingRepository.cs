using PublicLibraryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Domain.Interfaces
{
    public interface IBookLendingRepository : IGenericRepository<BookLending>
    {
        Task<List<BookLending>> GetBorrowedBooksByBorrowerIdAsync(int borrowerId, string? fromDate, string? toDate);
    }
}
