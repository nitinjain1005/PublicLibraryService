using PublicLibraryService.Domain.Entities;

namespace PublicLibraryService.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Book> Books { get; }
        IBookLendingRepository BookLendings { get; }
        IBookInventoryRepository BookInventories { get; }
        IGenericRepository<Borrower> Borrowers { get; }
        Task<int> CommitAsync();
    }
}
