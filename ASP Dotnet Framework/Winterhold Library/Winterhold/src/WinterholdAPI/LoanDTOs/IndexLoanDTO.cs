namespace WinterholdAPI.LoanDTOs;

public class IndexLoanDTO
{
    public List<LoanDTO> LoanDTOs { get; set; }
    public PaginationVM Pagination { get; set; }
    public string BookTitle { get; set; }
    public string CustomerName { get; set; }
    public bool PassedDueDate { get; set; }
}
