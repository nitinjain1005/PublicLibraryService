using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Domain.Entities
{
    public class BookLending
    {
        public int Id { get; set; }
        // Foreign Keys
        public int BookId { get; set; }
        public int BorrowerId { get; set; }

        // Fields
        public DateTime BorrowedOn { get; set; }
        public DateTime? ReturnedOn { get; set; } // nullable for "not returned yet"

        // Navigation Properties
        public Book Book { get; set; } = null!;
        public Borrower Borrower { get; set; } = null!;

    }
}
