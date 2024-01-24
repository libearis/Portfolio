namespace Asuransi;
using static Menu.MainMenu;
class Program
{
    static void Main(string[] args)
    {
        
        Dictionary<string, Customer> customers = new Dictionary<string, Customer>();
        List<Prospect> prospects = new List<Prospect>();
        PrintMainMenu(prospects, customers);
    }
}
