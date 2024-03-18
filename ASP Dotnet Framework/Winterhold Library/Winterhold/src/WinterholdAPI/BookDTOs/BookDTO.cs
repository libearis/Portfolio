namespace WinterholdAPI.BookDTOs;

public class BookDTO
{
    public string Code { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string AuthorName { get; set; }
    public bool IsBorrowed { get; set; }
    public string? Summary { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? TotalPage { get; set; }
}
