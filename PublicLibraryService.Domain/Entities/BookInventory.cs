using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Domain.Entities
{
    public class BookInventory
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int TotalCopies { get; set; }
        public Book Book { get; set; } = null!;
    }
}
