namespace Asuransi.Menu;
using static Helper.InputValidation;

public static class MainMenu
{
    public static void PrintMainMenu(List<Prospect> prospects, Dictionary<string, Customer> customers)
    {
        System.Console.WriteLine("Pilih lah salah satu menu dibawah ini");
        System.Console.WriteLine("  1. Menu Prospect");
        System.Console.WriteLine("  2. Menu Customer");
        System.Console.WriteLine("  3. Exit Application");

        switch(MenuInputValidation(1,3))
        {
            case "1":
            ProspectMenu.PrintProspectMenu(prospects, customers);
            break;

            case "2":
            CustomerMenu.PrintCustomerMenu(prospects, customers);
            break;

            case "3":
            break;
        }
    }
}
