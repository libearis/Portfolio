namespace WinterholdAPI.LoanDTOs;

public class LoanDTO
{
    public long Id { get; set; }
    public CustomerDTO Customer { get; set; } = null!;
    public BookDTO Book { get; set; } = null!;
    public DateTime LoanDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public string? Note { get; set; }
}
