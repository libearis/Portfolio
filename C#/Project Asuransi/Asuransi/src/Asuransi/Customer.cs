namespace Asuransi;

public class Customer : Prospect
{
    public string CustomerNumber { get; set; }
    public string IdCardNumber { get; set; }
    public string FamilyStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public List<Policy> Policies { get; set; } = new List<Policy>();
    public Customer()
    {
        
    }
    public Customer
    (
        string idCardNumber, string familyStatus, string paymentMethod, int prospectNumber, string job, 
        string firstName, string lastName, DateTime birthDate, string birthCity, char gender, IsCustomer isCustomer
    )
    :base(prospectNumber, job, firstName, lastName, birthDate, birthCity, gender, isCustomer)
    {
        IdCardNumber = idCardNumber;
        FamilyStatus = familyStatus;

        PaymentMethod = GetPaymentMethod(paymentMethod);
        CustomerNumber = GetCustomerNumber();
    }
    public string GetCustomerNumber()
    {
        return $"{DateTime.Now.Month}/{DateTime.Now.Year}/{ProspectNumber}";
    }
    public PaymentMethod GetPaymentMethod(string paymentMethod)
    {
        if(paymentMethod == "CC")
        {
            return PaymentMethod.CC;
        }
        else if(paymentMethod == "AC")
        {
            return PaymentMethod.AC;
        }
        else
        {
            return PaymentMethod.VP;
        }
    }
    public void PrintCustomerInformation()
    {
        string gender = Gender == 'P'? "Perempuan" : "Laki-laki"; 
        
        System.Console.WriteLine($"No: {CustomerNumber}, {FirstName} {LastName}, {gender}, {BirthDate.ToString("dd MMMM yyyy")}({BirthCity}), {Job}");
        System.Console.WriteLine($"KTP: {IdCardNumber}, Status di KK: {FamilyStatus}, Payment Method: " + (Convert.ToBoolean(PaymentMethod.AC)? "Auto Collection" : Convert.ToBoolean(PaymentMethod.CC)? "Credit Card" : "Voucher Point").ToString());
        System.Console.WriteLine("==================================================================================");
    }
    public void PrintCustomerInformationPolicy()
    {
        if(Gender == 'P')
        {
            System.Console.WriteLine("=================================CUSTOMER INFO===================================");
            System.Console.WriteLine($"No: {CustomerNumber}, {FirstName} {LastName}, Perempuan, {BirthDate.ToString("dd MMMM yyyy")}({BirthCity}), {Job}");
            System.Console.WriteLine($"KTP: {IdCardNumber}, Status di KK: {FamilyStatus}, Payment Method: " + (Convert.ToBoolean(PaymentMethod.AC)? "Auto Collection" : Convert.ToBoolean(PaymentMethod.CC)? "Credit Card" : "Voucher Point").ToString());
            System.Console.WriteLine("=================================================================================");
        }
        else
        {
            System.Console.WriteLine("=================================CUSTOMER INFO===================================");
            System.Console.WriteLine($"No: {CustomerNumber}, {FirstName} {LastName}, Laki-laki, {BirthDate.ToString("dd MMMM yyyy")}({BirthCity}), {Job}");
            System.Console.WriteLine($"KTP: {IdCardNumber}, Status di KK: {FamilyStatus}, Payment Method: " + (Convert.ToBoolean(PaymentMethod.AC)? "Auto Collection" : Convert.ToBoolean(PaymentMethod.CC)? "Credit Card" : "Voucher Point").ToString());
            System.Console.WriteLine("=================================================================================");
        }
    }
    public override void PrintInformation()
    {
        System.Console.WriteLine($"Nama: {FirstName} {LastName}");
        System.Console.WriteLine($"Gender: {Gender}");
        System.Console.WriteLine($"Customer Number: {CustomerNumber}");
    }
    public Dictionary<string, Customer> MengembalikanDictionary()
    {
        Dictionary<string, Customer> customers = new Dictionary<string, Customer>
        {
            {CustomerNumber, this}
        };

        return customers;
    }
}
