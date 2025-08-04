using Microsoft.EntityFrameworkCore;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.Infrastructure.Data;

namespace PublicLibraryService.Infrastructure.Repositories
{
    public class BookLendingRepository(PublicLibraryDbContext context) : GenericRepository<BookLending>(context), IBookLendingRepository
    {
        public async Task<List<BookLending>> GetBorrowedBooksByBorrowerIdAsync(int borrowerId, string? fromDate, string? toDate)
        {
            var query = _context.BookLendings
                .AsNoTracking()
                .Where(bl => bl.BorrowerId == borrowerId);

            if (DateTime.TryParse(fromDate, out var from))
            {
                query = query.Where(bl => bl.BorrowedOn >= from);
            }

            if (DateTime.TryParse(toDate, out var to))
            {
                query = query.Where(bl => bl.BorrowedOn <= to);
            }

            return await query.ToListAsync();
        }
    }
}
