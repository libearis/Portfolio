namespace WinterholdAPI.LoanDTOs;

public class BookDTO
{
    public string Code { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string AuthorName { get; set; }
    public int Floor { get; set; }
    public string Isle { get; set; }
    public string Bay { get; set; }
}
