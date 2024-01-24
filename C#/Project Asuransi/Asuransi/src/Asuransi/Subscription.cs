namespace Asuransi;

public class Subscription
{

    public DateTime PaymentDate { get; set; }
    public decimal SubscriptionPrice { get; set; }
    public Subscription(DateTime paymentDate, decimal subscriptionPrice)
    {
        PaymentDate = paymentDate;
        SubscriptionPrice = subscriptionPrice;
    }
    public void PrintPayment()
    {
        System.Console.WriteLine($"Product: {PaymentDate.ToString("dd MMMM yyyy")}, {SubscriptionPrice.ToString("c2")}");
    }
}
