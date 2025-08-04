using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibraryService.Application.Models.Response
{
    public class ReadingRatePerUser
    {
        public string BorrowerName { get; set; } = string.Empty;
        public DateTime BorrowedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public int DaysTaken { get; set; }
        public double ReadingRatePerDay { get; set; }  // TotalPages / DaysTaken
    }
}
