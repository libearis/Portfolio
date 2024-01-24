namespace Asuransi;

public class Policy
{

    public Customer PolicyOwner { get; set; }
    public Customer InsuredCustomer { get; set; }
    public Vehicle InsuredVehicle { get; set; }
    public DateTime StartDate { get; set; }
    public Product Product { get; set; }
    public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public Policy(Customer policyOwner, Customer insured, DateTime startDate, Product productBenefit)
    {
        PolicyOwner = policyOwner;
        InsuredCustomer = insured;
        StartDate = startDate;
        Product = productBenefit;
    }
    public Policy(Customer policyOwner, Vehicle vehicle, DateTime startDate, Product productBenefit)
    {
        PolicyOwner = policyOwner;
        InsuredVehicle = vehicle;
        StartDate = startDate;
        Product = productBenefit;
    }
    public void PrintPolicyInfo()
    {
        if(InsuredCustomer != null)
        {
            System.Console.WriteLine("=============================POLICY INFO=================================");
            System.Console.WriteLine($"Tertanggung: {InsuredCustomer.FirstName} {InsuredCustomer.LastName}");
            System.Console.WriteLine($"Product: {Product.ProductName}");
            System.Console.WriteLine($"Tanggal Mulai Dijalankan: {StartDate.ToString("dd MMMM yyyy")}");
            System.Console.WriteLine($"Manfaat: {Product.Benefit}");
            System.Console.WriteLine("========================================================================");
        }
        else if(InsuredVehicle != null)
        {
            System.Console.WriteLine("=============================POLICY INFO================================");
            System.Console.WriteLine($"Product: {Product.ProductName}");
            System.Console.WriteLine($"Tanggal Mulai Dijalankan: {StartDate.ToString("dd MMMM yyyy")}");
            System.Console.WriteLine($"Jenis kendaraan: {InsuredVehicle.VehicleType}");
            System.Console.WriteLine($"Nomor Polisi: {InsuredVehicle.PoliceNumber}");
            System.Console.WriteLine($"Nomor STNK: {InsuredVehicle.VehicleRegistration}");
            System.Console.WriteLine($"Merek Kendaraan: {InsuredVehicle.VehicleBrand}");
            System.Console.WriteLine($"Model Kendaraan: {InsuredVehicle.VehicleModel}");
            System.Console.WriteLine($"Warna Kendaraan: {InsuredVehicle.VehicleColor}");
            System.Console.WriteLine("=======================================================================");
        }
        else
        {
            System.Console.WriteLine("=============================POLICY INFO===============================");
            System.Console.WriteLine("Belum ada informasi polis apapun, tambahkan polis dengan membeli produk kami");
            System.Console.WriteLine("=======================================================================");
        }
    }
    public void PrintAllPayment()
    {
        decimal totalPrice = 0;
        foreach(Subscription subscription in Subscriptions)
        {
            System.Console.WriteLine($"Harga: {subscription.SubscriptionPrice}");
            totalPrice += subscription.SubscriptionPrice;
        }
        System.Console.WriteLine(totalPrice);
    }
}
