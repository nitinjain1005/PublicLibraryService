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
    public class BookInventoryRepository(PublicLibraryDbContext context) : GenericRepository<BookInventory>(context), IBookInventoryRepository
    {
        private readonly PublicLibraryDbContext _context = context;

        public new async Task<IEnumerable<BookInventory>> FindAsync(Expression<Func<BookInventory, bool>> predicate)
        {
            return await _context.BookInventories.Where(predicate).ToListAsync();
        }
    }
}
