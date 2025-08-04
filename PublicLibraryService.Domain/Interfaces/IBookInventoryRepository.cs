using PublicLibraryService.Domain.Entities;
using System.Linq.Expressions;

namespace PublicLibraryService.Domain.Interfaces
{
    public interface IBookInventoryRepository : IGenericRepository<BookInventory>
    {
        new Task<IEnumerable<BookInventory>> FindAsync(Expression<Func<BookInventory, bool>> predicate);
    }

}
