using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Interfaces;

public interface IOrderRepository
{
    public Order GetByInvoiceNumber(string invoiceNumber);
    public OrderDetail GetOrderDetailById(long id);
    public List<Order> Get();
    public List<Order> Get(int pageNumber, int pageSize, string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate);
    public int Count(string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate);
    public List<string> GetInvoiceNumbers();
    public Delivery GetDeliveryCost(long id);
    public Customer GetCustomer(long id);
    public Delivery GetDelivery(long id);
    public Salesman GetSalesman(string SalesEmployeeNumber);
    public List<Customer> GetCustomers();
    public List<Salesman> GetSalesmens();
    public List<Delivery> GetDeliveries();
    public List<OrderDetail> GetDetail(string invoiceNumber, int pageNumber, int pageSize);
    public int CountDetail(string invoiceNumber);
    public Product GetProductById(long id);
    public List<Product> GetProducts();
    public void Insert(Order order);
    public void InsertDetail(OrderDetail orderDetail);
    public void Update(Order order);
    public void UpdateDetail(OrderDetail orderDetail);
    public void Delete(Order order);
    public void DeleteDetail(OrderDetail orderDetail);
}
