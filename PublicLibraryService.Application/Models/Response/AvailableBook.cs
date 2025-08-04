
namespace PublicLibraryService.Application.Models.Response
{
    public class AvailableBook
    {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int TotalCopies { get; set; }
        public int BorrowedCopies { get; set; }
        public int AvailableCopies { get; set; }
    }
}
