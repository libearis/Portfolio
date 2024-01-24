namespace Asuransi;

public class Prospect : Person
{
    public int ProspectNumber { get; set; }
    public string Job { get; set; }
    public IsCustomer IsCustomer { get; set; }
    public Prospect()
    {
        
    }
    public Prospect(int prospectNumber, string job, string firstName, string lastName, DateTime birthDate, string birthCity, char gender, IsCustomer isCustomer) 
    :base(firstName, lastName, birthDate, birthCity, gender)
    {
        ProspectNumber = prospectNumber;
        Job = job;
        IsCustomer = isCustomer;
    }
    public void PrintProspectInformation()
    {
        if(Gender == 'P')
        {
            System.Console.WriteLine($"No: {ProspectNumber}, {FirstName} {LastName}, Perempuan, {BirthDate.ToString("dd MMMM yyyy")}({BirthCity}), {Job}, {IsCustomer}");
        }
        else
        {
            System.Console.WriteLine($"No: {ProspectNumber}, {FirstName} {LastName}, Laki-laki, {BirthDate.ToString("dd MMMM yyyy")}({BirthCity}), {Job}, {IsCustomer}");
        }
    }
    public virtual void PrintInformation()
    {
        System.Console.WriteLine($"Nama: {FirstName} {LastName}");
        System.Console.WriteLine($"Gender: {Gender}");
    }
}
