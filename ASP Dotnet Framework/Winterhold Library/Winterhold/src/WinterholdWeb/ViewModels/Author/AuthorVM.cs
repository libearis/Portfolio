namespace WinterholdWeb.ViewModels.Author;

public class AuthorVM
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime? DeceasedDate { get; set; }
    public int Age { get{
        return GetAge(DeceasedDate??DateTime.Now, BirthDate);
    } }
    public string AliveStatus { get{
        return DeceasedDate == null? "Alived" : "Deceased";  
    } }
    public string? Education { get; set; }
    public string? Summary { get; set; }
    public int TotalBookBorrowed { get; set; }

    public int GetAge(DateTime deceasedDate, DateTime birthDate)
    {
        TimeSpan getYear = deceasedDate - birthDate;
        int age = Convert.ToInt32(getYear.Days) / 365;
        return age;
    }
}
