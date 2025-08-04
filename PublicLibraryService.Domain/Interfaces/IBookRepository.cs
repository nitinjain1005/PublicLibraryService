using PublicLibraryService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetBookWithInventoryAsync(int bookId);
    }

}
