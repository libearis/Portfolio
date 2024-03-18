using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WinterholdAPI.LoanDTOs;

public class InsertLoanDTO
{
    public List<SelectListItem> Customers { get; set; }
    public List<SelectListItem> Books { get; set; }
    public string CustomerNumber { get; set; }
    public string BookCode { get; set; }
    public DateTime LoanDate { get; set; }
    public string Note { get; set; }
}
