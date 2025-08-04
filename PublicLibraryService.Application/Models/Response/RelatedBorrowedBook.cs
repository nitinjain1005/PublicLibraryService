namespace PublicLibraryService.Application.Models.Response
{
    public class RelatedBorrowedBook
    {
        public required string BorrowerName { get; set; }
        public required string BookTitle { get; set; }
        public DateTime BorrowedOn { get; set; }
    }
}
