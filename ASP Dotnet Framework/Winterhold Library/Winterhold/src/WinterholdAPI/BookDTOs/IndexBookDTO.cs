namespace WinterholdAPI.BookDTOs;

public class IndexBookDTO
{
    public string CategoryName { get; set; }
    public List<BookDTO> Books { get; set; }
    public PaginationVM Pagination { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }
}
