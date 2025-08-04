using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Models.Response
{
    public class BookReadingRate
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalPages { get; set; }
        public List<ReadingRatePerUser> ReaderRates { get; set; } = new();
    }
}
