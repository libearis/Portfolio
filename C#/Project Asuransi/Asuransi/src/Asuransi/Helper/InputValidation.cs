using System.Globalization;

namespace Asuransi.Helper;

public static class InputValidation
{
    public static string MenuInputValidation(int minRange, int maxRange)
    {
        string userInput = Console.ReadLine();
        int number = 0;

        if(!Int32.TryParse(userInput, out number) || Convert.ToInt32(userInput) < minRange || Convert.ToInt32(userInput) > maxRange)
        {
            System.Console.WriteLine($"Pilihan Input tidak tersedia, Input sesuai pilihan: ");
            userInput = MenuInputValidation(minRange, maxRange);
        }
        return userInput;
    }
    public static string StringInputValidation()
    {
        string userInput = Console.ReadLine();
        if(userInput.Any(char.IsDigit))
        {
            System.Console.WriteLine("Input tidak boleh angka, hanya berupa string:");
            userInput = StringInputValidation();
        }
        return userInput;
    }
    public static string NumberInputValidation()
    {
        long number = 0;
        string userInput = Console.ReadLine();
        if(!Int64.TryParse(userInput, out number))
        {
            System.Console.WriteLine("Input tidak boleh huruf, hanya berupa angka:");
            userInput = NumberInputValidation();
        }
        return userInput;
    }
    public static string FamilyStatusValidation()
    {
        string userInput = Console.ReadLine();
        if(userInput != "Istri" && userInput != "Kepala Keluarga" && userInput != "Anak")
        {
            System.Console.WriteLine("Input hanya berisi Kepala Keluarga, Istri atau Anak");
            userInput = FamilyStatusValidation();
        }
        return userInput;
    }
    public static string PaymentMethodValidation()
    {
        string userInput = Console.ReadLine();
        if(userInput != "CC" && userInput != "AC" && userInput != "VP")
        {
            System.Console.WriteLine("Hanya input(CC/AC/VP)");
            userInput = PaymentMethodValidation();
        }
        return userInput;
    }
    public static string CustomersKeyValidation(Dictionary<string, Customer> customers)
    {
        string userInput = Console.ReadLine();
        if(!customers.ContainsKey(userInput))
        {
            System.Console.WriteLine("Input salah. Pilih nomor customer yang tersedia");
            userInput = CustomersKeyValidation(customers);
        }

        return userInput;
    }
    public static string ProductsKeyValidation(Dictionary<string, Product> products)
    {
        string userInput = Console.ReadLine();
        if(!products.ContainsKey(userInput))
        {
            System.Console.WriteLine("Input salah. Pilih nama produk yang tersedia");
            userInput = ProductsKeyValidation(products);
        }

        return userInput;
    }
    public static VehicleType VehicleTypeValidation()
    {
        string userInput = Console.ReadLine();
        if(userInput != VehicleType.Mobil.ToString() && userInput != VehicleType.Motor.ToString())
        {
            System.Console.WriteLine("Mohon Input Mobil/Motor saja");
            userInput = VehicleTypeValidation().ToString();
        }
        if(userInput == VehicleType.Mobil.ToString())
        {
            return VehicleType.Mobil;
        }
        else
        {
            return VehicleType.Motor;
        }

    }
    public static DateTime DateInputValidation()
    {
        string userInput = Console.ReadLine();
        DateTime date;
        if(!DateTime.TryParseExact(userInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            System.Console.WriteLine("Mohon inpuut sesuai format (dd/MM/yyyy):");
            userInput = DateInputValidation().ToString();
        }
        return Convert.ToDateTime(userInput);
    }
    public static char GenderValidation()
    {
        string userInput = Console.ReadLine();
        if(userInput != "P" && userInput != "L")
        {
            System.Console.WriteLine("Mohon input P/L:");
            userInput = GenderValidation().ToString();
        }
        return Convert.ToChar(userInput);
    }
}
