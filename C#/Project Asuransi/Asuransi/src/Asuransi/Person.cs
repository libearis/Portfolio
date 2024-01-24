namespace Asuransi;

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string BirthCity { get; set; }
    public char Gender { get; set; }
    public int Age { get; set; }

    public Person()
    {

    }

    public Person(string firstName, string lastName, DateTime birthDate, string birthCity, char gender)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        BirthCity = birthCity;
        Gender = gender;
        
        Age = GetAge();
    }
    public int GetAge()
    {
        TimeSpan timeSpan = DateTime.Now - BirthDate;
        int age = Convert.ToInt32(timeSpan.TotalDays)/365;

        return age;
    }
}
