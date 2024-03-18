namespace BasiliskAPI.Salesmen;

public class SalesDTO
{
    public string EmployeeNumber { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string? SuperiorEmployeeName { get; set; }
}
