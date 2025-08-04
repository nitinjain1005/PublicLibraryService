using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Domain.Interfaces;
using PublicLibraryService.Infrastructure.Data;

namespace PublicLibraryService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PublicLibraryDbContext _context;

        public IGenericRepository<Book> Books { get; }
        public IBookInventoryRepository BookInventories { get; }
        public IBookLendingRepository BookLendings { get; }
        public IGenericRepository<Borrower> Borrowers { get; }

        public UnitOfWork(PublicLibraryDbContext context)
        {
            _context = context;
            Books = new GenericRepository<Book>(_context);
            BookInventories = new BookInventoryRepository(_context);
            BookLendings = new BookLendingRepository(_context);
            Borrowers = new GenericRepository<Borrower>(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
