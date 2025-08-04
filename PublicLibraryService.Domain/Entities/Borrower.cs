using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Domain.Entities
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;

        // Navigation
        public ICollection<BookLending> BookLendings { get; set; } = new List<BookLending>();
    }
}
