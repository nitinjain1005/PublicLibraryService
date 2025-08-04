using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicLibraryService.Domain.Interfaces;

namespace PublicLibraryService.TestData.Abstraction
{
    public interface ITestDataSeeder
    {
        Task SeedAsync(IUnitOfWork unitOfWork);
        
    }
}
