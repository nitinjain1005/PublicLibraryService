namespace PublicLibraryService.Domain.Entities
{
    public class Book
{
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime PublishedDate { get; set; }
        public int TotalPages { get; set; }

        // Navigation
        public BookInventory? Inventory { get; set; }
        public ICollection<BookLending> BookLendings { get; set; } = new List<BookLending>();
    }
}
