namespace Asuransi.Menu;

using System.Runtime.ConstrainedExecution;
using static Helper.InputValidation;

public static class ProspectMenu
{
    public static void PrintProspectMenu(List<Prospect> prospects, Dictionary<string, Customer> customers)
    {
        System.Console.WriteLine("Pilih lah salah satu menu di bawah ini:");
        System.Console.WriteLine("  1. Add Prospect");
        System.Console.WriteLine("  2. Prospect To Customer");
        System.Console.WriteLine("  3. Main Menu");
        System.Console.WriteLine("  4. Exit Application");

        switch(MenuInputValidation(1, 4))
        {
            case "1":
            AddProspect(prospects, customers);
            break;

            case "2":
            ProspectToCustomer(prospects, customers);
            break;

            case "3":
            MainMenu.PrintMainMenu(prospects, customers);
            break;

            case "4":
            break;
        }
    }
    public static void AddProspect(List<Prospect> prospects, Dictionary<string, Customer> customers)
    {
        System.Console.WriteLine("Masukan data prospect satu-persatu:\n");
        System.Console.WriteLine("Nama Depan:");
        string firstName = StringInputValidation();

        System.Console.WriteLine("Nama Belakang:");
        string lastName = StringInputValidation();

        System.Console.WriteLine("Tanggal Lahir (dd/MM/yyyy)");
        DateTime birthDate = DateInputValidation();

        System.Console.WriteLine("Tempat Lahir:");
        string birthCity = StringInputValidation();

        System.Console.WriteLine("Jenis Kelamin (P/L)");
        char gender = GenderValidation();

        System.Console.WriteLine("Pekerjaan:");
        string job = StringInputValidation();

        int newProspectNumber = prospects.Count + 1;
        Prospect newProspect = new Prospect(newProspectNumber, job, firstName, lastName, birthDate, birthCity, gender, IsCustomer.Prospect);
        prospects.Add(newProspect);
        string coba = $"{job}/{birthCity}";

        System.Console.WriteLine("\nBerhasil Menambahkan Prospect Baru");
        MainMenu.PrintMainMenu(prospects, customers);
    }
    public static void ProspectToCustomer(List<Prospect> prospects, Dictionary<string, Customer> customers)
    {
        int index = 0;
        foreach(Prospect prospect in prospects)
        {
            if(prospect.IsCustomer == IsCustomer.Prospect) break;
            index++;
        }
        if(prospects.Count == 0)
        {
            System.Console.WriteLine("Belum ada yang menjadi Prospect, harap input data prospect terlebih dahulu");
            PrintProspectMenu(prospects, customers);
        }
        else if(index == prospects.Count)
        {
            System.Console.WriteLine("Semua telah menjadi Nasabah, harap input data prospect terlebih dahulu");
            PrintProspectMenu(prospects, customers);
        }
        else
        {
            foreach(Prospect prospect in prospects)
            {
                prospect.PrintProspectInformation();
            }
            System.Console.WriteLine("\nKetik nomor prospect yang ingin dijadikan nasabah.");
            string prospectNumber = MenuInputValidation(1, prospects.Count);

            if(prospects[Convert.ToInt32(prospectNumber) - 1].IsCustomer == IsCustomer.Nasabah)
            {
                System.Console.WriteLine($"{prospects[Convert.ToInt32(prospectNumber) - 1].FirstName} sudah menjadi nasabah");
                ProspectToCustomer(prospects, customers);
            }
            else
            {
                System.Console.WriteLine("\nNomor KTP:");
                string nIK = NumberInputValidation();

                System.Console.WriteLine("Status KK:");
                string familyStatus = FamilyStatusValidation();

                System.Console.WriteLine("Payment Method(CC/AC/VP)");
                string paymentMethod = PaymentMethodValidation();

                Customer newCustomer = 
                new Customer
                (
                    nIK, familyStatus, paymentMethod, prospects[Convert.ToInt32(prospectNumber) - 1].ProspectNumber,
                    prospects[Convert.ToInt32(prospectNumber) - 1].Job, prospects[Convert.ToInt32(prospectNumber) - 1].FirstName,
                    prospects[Convert.ToInt32(prospectNumber) - 1].LastName, prospects[Convert.ToInt32(prospectNumber) - 1].BirthDate,
                    prospects[Convert.ToInt32(prospectNumber) - 1].BirthCity, prospects[Convert.ToInt32(prospectNumber) - 1].Gender,
                    prospects[Convert.ToInt32(prospectNumber) - 1].IsCustomer
                );
                customers.Add(newCustomer.CustomerNumber, newCustomer);

                Prospect alex = new Customer("1234123412341234", "Anak", "CC", 1, "Pelajar", "Alex", "Ferdinand", new DateTime(2000, 1, 11), "Jakarta", 'L', IsCustomer.Nasabah);
                prospects[Convert.ToInt32(prospectNumber) - 1].IsCustomer = IsCustomer.Nasabah;
                Customer customer = (Customer) alex;
                alex.PrintInformation();
                ((Prospect)alex).PrintInformation();

                System.Console.WriteLine();
                PrintProspectMenu(prospects, customers);
            }
        }
    }
}
