namespace Asuransi.Menu;
using static Helper.InputValidation;

public static class CustomerMenu
{
    public static void PrintCustomerMenu(List<Prospect> prospects, Dictionary<string, Customer> customers)
    {
        if(customers.Count == 0)
        {
            System.Console.WriteLine("Belum ada customer yang terdaftar, harap masukan data customer dari prospek terlebih dahulu");
            MainMenu.PrintMainMenu(prospects, customers);
        }
        else
        {
            System.Console.WriteLine();
            foreach(KeyValuePair<string, Customer> customer in customers)
            {
                customer.Value.PrintCustomerInformation();
            }
            System.Console.WriteLine("\nPilih menu yang ingin dipilih");
            System.Console.WriteLine("  1. Buy Product");
            System.Console.WriteLine("  2. Detail Policy");
            System.Console.WriteLine("  3. Main Menu");
            System.Console.WriteLine("  4. Exit Application");

            switch(MenuInputValidation(1,4))
            {
                case "1":
                BuyProduct(prospects, customers);
                break;

                case "2":
                DetailPolicy(prospects, customers);
                break;

                case "3":
                MainMenu.PrintMainMenu(prospects, customers);
                break;

                case "4":
                break;
            }
        }
        
    }
    public static void BuyProduct(List<Prospect> prospects, Dictionary<string, Customer> customers)
    {
        Dictionary<string, Product> products = Data.InstantiateProduct();

        foreach(KeyValuePair<string, Customer> customer in customers)
        {
            customer.Value.PrintCustomerInformation();
        }

        System.Console.WriteLine("\nPilih Nomor Customer:");
        string customerNumberInput = CustomersKeyValidation(customers);

        System.Console.WriteLine("Pilih Nama Product:");
        string productNameInput = ProductsKeyValidation(products);

        if(products[productNameInput].ProductType == ProductType.Kendaraan)
        {
            System.Console.WriteLine("\nInput Data Kendaraan:");
            System.Console.WriteLine("Jenis kendaraan:");
            VehicleType vehicleTypeInput = VehicleTypeValidation();

            System.Console.WriteLine("\nNomor Polisi:");
            string policeNumber = Console.ReadLine();

            System.Console.WriteLine("\nNomor STNK");
            string vehicleRegistrationInput = NumberInputValidation();

            System.Console.WriteLine("\nMerek kendaraan:");
            string vehicleBrandInput = StringInputValidation();

            System.Console.WriteLine("\nModel kendaraan:");
            string vehicleModelInput = StringInputValidation();

            System.Console.WriteLine("\nWarna kendaraan:");
            string vehicleColorInput = StringInputValidation();

            Vehicle newVehicle = new Vehicle(vehicleTypeInput, policeNumber, vehicleRegistrationInput, vehicleBrandInput, vehicleModelInput, vehicleColorInput);
            Policy newPolicy = new Policy(customers[customerNumberInput], newVehicle, DateTime.Now, products[productNameInput]);
            Subscription firstBill = new Subscription(DateTime.Now, newPolicy.Product.SetPrice(customers[customerNumberInput].Age));
            Subscription secondBill = new Subscription(DateTime.Now.AddYears(1), newPolicy.Product.SetPrice(customers[customerNumberInput].Age));
            Subscription thirdBill = new Subscription(DateTime.Now.AddYears(2), newPolicy.Product.SetPrice(customers[customerNumberInput].Age));
        
            newPolicy.Subscriptions.Add(firstBill);
            newPolicy.Subscriptions.Add(secondBill);
            newPolicy.Subscriptions.Add(thirdBill);
            customers[customerNumberInput].Policies.Add(newPolicy);
        }
        else
        {
            string insuredNumberInput;
            if(productNameInput == "Sehat Extra" || productNameInput == "Life Special")
            {
                System.Console.WriteLine("Produk ini otomatis menanggung diri sendiri");
                insuredNumberInput = customerNumberInput;
            }
            else
            {
                System.Console.WriteLine("Pilih Nomor Customer Tertanggung:");
                insuredNumberInput = CustomersKeyValidation(customers);
            }
            if(products[productNameInput].PaymentFrequency == PaymentFrequency.Tahunan)
            {
                Policy newPolicy = new Policy(customers[customerNumberInput], customers[insuredNumberInput], DateTime.Now, products[productNameInput]);
                Subscription firstBill = new Subscription(DateTime.Now, newPolicy.Product.SetPrice(customers[insuredNumberInput].Age));
                Subscription secondBill = new Subscription(DateTime.Now.AddYears(1), newPolicy.Product.SetPrice(customers[insuredNumberInput].Age));
                Subscription thirdBill = new Subscription(DateTime.Now.AddYears(2), newPolicy.Product.SetPrice(customers[insuredNumberInput].Age));

                newPolicy.Subscriptions.Add(firstBill);
                newPolicy.Subscriptions.Add(secondBill);
                newPolicy.Subscriptions.Add(thirdBill);
                customers[customerNumberInput].Policies.Add(newPolicy);
            }
            else
            {
                Policy newPolicy = new Policy(customers[customerNumberInput], customers[insuredNumberInput], DateTime.Now, products[productNameInput]);
                Subscription firstBill = new Subscription(DateTime.Now, newPolicy.Product.SetPrice(customers[insuredNumberInput].Age));
                Subscription secondBill = new Subscription(DateTime.Now.AddMonths(1), newPolicy.Product.SetPrice(customers[insuredNumberInput].Age));
                Subscription thirdBill = new Subscription(DateTime.Now.AddMonths(2), newPolicy.Product.SetPrice(customers[insuredNumberInput].Age));

                newPolicy.Subscriptions.Add(firstBill);
                newPolicy.Subscriptions.Add(secondBill);
                newPolicy.Subscriptions.Add(thirdBill);
                customers[customerNumberInput].Policies.Add(newPolicy);
            }
            
        }
        
        PrintCustomerMenu(prospects, customers);
    }
    public static void DetailPolicy(List<Prospect> prospects, Dictionary<string, Customer> customers)
    {
        foreach(KeyValuePair<string, Customer> customer in customers)
        {
            customer.Value.PrintCustomerInformation();
        }

        System.Console.WriteLine("\nPilih Nomor Customer:");
        string customerNumberInput = CustomersKeyValidation(customers);

        customers[customerNumberInput].PrintCustomerInformationPolicy();
        foreach(Policy policy in customers[customerNumberInput].Policies)
        {
            policy.PrintPolicyInfo();
            System.Console.WriteLine("====================================PAYMENT=====================================");
            foreach(Subscription subscription in policy.Subscriptions)
            {
                subscription.PrintPayment();
            }
            System.Console.WriteLine("================================================================================");
        }
        System.Console.WriteLine();
        PrintCustomerMenu(prospects, customers);
    }
}
