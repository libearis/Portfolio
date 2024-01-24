namespace Asuransi;

public class Product
{

    public string ProductName { get; set; }
    public ProductType ProductType { get; set; }
    public PaymentFrequency PaymentFrequency { get; set; }
    public decimal SubscriptionPrice { get; set; }
    public string Benefit { get; set; }
    public Product(string productName, ProductType productType, PaymentFrequency paymentFrequency, string benefit)
    {
        ProductName = productName;
        ProductType = productType;
        PaymentFrequency = paymentFrequency;
        Benefit = benefit;
    }
    
    public decimal SetPrice(int age)
    {
        if(age <= 20 && ProductName == "Sehat Bersama")
        {
            return 200_000m;
        }
        else if(age > 20 && ProductName == "Sehat Bersama")
        {
            return 400_000m;
        }
        else if(age <= 20 && ProductName == "Sehat Extra")
        {
            return 300_000m;
        }
        else if(age > 20 && ProductName == "Sehat Extra")
        {
            return 500_000m;
        }
        else if(age <= 20 && ProductName == "Life Keluarga")
        {
            return 250_000m;
        }
        else if(age > 20 && ProductName == "Life Keluarga")
        {
            return 450_000m;
        }
        else if(age <= 20 && ProductName == "Life Special")
        {
            return 3_600_000m;
        }
        else if(age > 20 && ProductName == "Life Special")
        {
            return 4_800_000m;
        }
        else if(ProductName == "Protection")
        {
            return 2_000_000m;
        }
        else
        {
            return 2_500_000m;
        }
    }
}
