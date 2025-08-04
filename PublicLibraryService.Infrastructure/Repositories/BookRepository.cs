using Microsoft.EntityFrameworkCore;
using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(PublicLibraryDbContext context) : base(context) { }

        public async Task<Book?> GetBookWithInventoryAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.Inventory)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }
        //public new async Task<IEnumerable<Book>> GetbyId(Expression<Func<Book, bool>> predicate)
        //{
        //    return await _context.Books.ToListAsync();
        //}
    }
}
