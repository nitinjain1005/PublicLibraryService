using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Models.Response
{
    public class HighValuedBorrower
    {
        public int BorrowerId { get; set; }
        public string Name { get; set; } = null!;
        public int TotalBorrowedBooks { get; set; }
    }
}
