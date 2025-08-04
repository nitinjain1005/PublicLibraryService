using PublicLibraryService.Domain.Entities;
using PublicLibraryService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.TestData.TestDataFactory
{
    public static class TestBorrowerFactory
    {
        public static Borrower WilliAlice => new()
        {
            Id = 45,
            Name = "Willi Alice",
            Age = 36,
            Email = "alice.johnson@example.com"
        };

        public static Borrower NitinIrani => new()
        {
            Id = 42,
            Name = "Nitin Irani",
            Age = 30,
            Email = "ntin.irani@example.com"
        };

        public static Borrower RjSmith => new()
        {
            Id = 43,
            Name = "Rj Smith",
            Age = 32,
            Email = "rj.smith@example.com"
        };

        public static List<Borrower> GetAll() => new() { WilliAlice, NitinIrani, RjSmith };

        public static Borrower Create(
            int id = 1,
            string name = "Test Borrower",
            int age=25,
            string email="t@test.com"
            )
        {
            return new Borrower
            {
                Id = id,
                Name = name,
                Age = age,
                Email = email
            };
        }

        public static async Task<Borrower> CreateAndSeedAsync(
            PublicLibraryDbContext dbContext,
            int id = 1,
            string name = "Test Borrower",
            int age=25,
            string email="t@test.com")
        {

            var borrower = Create(id, name,age,email);
            dbContext.Borrowers.Add(borrower);
            await dbContext.SaveChangesAsync();
            return borrower;
        }
    }
}
