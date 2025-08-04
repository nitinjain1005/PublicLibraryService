using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Models.Response
{
   public class MostBorrowedBook
    {
        public int BookId { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public int BorrowCount { get; set; }
    }
    
}
