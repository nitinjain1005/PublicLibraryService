using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Models.Response
{
    public class BorrowedBook
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime BorrowedOn { get; set; }
        public DateTime? ReturnedOn { get; set; }
    }
}
