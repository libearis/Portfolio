using BasiliskBusiness.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using TradeOfBasiliskDataAccess.Models;

namespace BasiliskBusiness.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly BasiliskTFContext dbContext;

    public OrderRepository(BasiliskTFContext basiliskTFContext)
    {
        dbContext = basiliskTFContext;
    }
    public int CountDetail(string invoiceNumber)
    {
        return dbContext.OrderDetails.Where(det => det.InvoiceNumber == invoiceNumber).Count();
    }
    public int Count(string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate)
    {
        if(orderDate == new DateTime())
        {
            if(customerId == 0)
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&& 
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"".ToLower())).Count();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&& 
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"") && ord.DeliveryId == deliveryId).Count();
                }
            }
            else
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")).Count();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")&&
                    ord.DeliveryId == deliveryId).Count();
                }
                
            }
        }
        else
        {
            if(customerId == 0)
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&& 
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"") && ord.OrderDate == orderDate).Count();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&& 
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"") && ord.DeliveryId == deliveryId&&
                    ord.OrderDate == orderDate).Count();
                }
            }
            else
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")&&
                    ord.OrderDate == orderDate).Count();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")&&
                    ord.DeliveryId == deliveryId && ord.OrderDate == orderDate).Count();
                }
                
            }
        }
    }

    public List<Order> Get()
    {
        return dbContext.Orders.ToList();  
    }
    public List<OrderDetail> GetDetail( string invoiceNumber, int pageNumber, int pageSize)
    {
        return dbContext.OrderDetails.Where(det => det.InvoiceNumber == invoiceNumber).
        Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }
    public List<Order> Get(int pageNumber, int pageSize, string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate)
    {
        if(orderDate == new DateTime())
        {
            if(customerId == 0)
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"") && ord.DeliveryId == deliveryId &&
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            else
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")&&
                    ord.DeliveryId == deliveryId).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                
            }
        }
        else
        {
            if(customerId == 0)
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&& 
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"") && ord.OrderDate == orderDate).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&& 
                    ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"") && ord.DeliveryId == deliveryId&&
                    ord.OrderDate == orderDate).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            else
            {
                if(deliveryId == 0)
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")&&
                    ord.OrderDate == orderDate).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    return dbContext.Orders.Where(ord => ord.InvoiceNumber.Contains(invoiceNumber??"")&&
                    ord.CustomerId == customerId && ord.SalesEmployeeNumber.Contains(salesEmployeeNumber??"")&&
                    ord.DeliveryId == deliveryId && ord.OrderDate == orderDate).
                    Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                
            }
        }
    }

    public List<string> GetInvoiceNumbers()
    {
        var totalCount = dbContext.Orders.Count();
        return dbContext.Orders.Select(ord=>ord.InvoiceNumber).Skip(totalCount - 1).ToList();
    }

    public Delivery GetDeliveryCost(long id)
    {
        return dbContext.Deliveries.Find(id);
    }

    public Customer GetCustomer(long id)
    {
        return dbContext.Customers.Find(id)?? throw new NullReferenceException("Customer tidak ditemukan");
    }
    public Delivery GetDelivery(long id)
    {
        return dbContext.Deliveries.Find(id)?? throw new NullReferenceException("Customer tidak ditemukan");
    }
    public Salesman GetSalesman(string SalesEmployeeNumber)
    {
        return dbContext.Salesmen.Find(SalesEmployeeNumber)?? throw new NullReferenceException("Sales tidak ditemukan");
    }

    public List<Customer> GetCustomers()
    {
        return dbContext.Customers.ToList();
    }

    public List<Salesman> GetSalesmens()
    {
        return dbContext.Salesmen.ToList();
    }

    public List<Delivery> GetDeliveries()
    {
        return dbContext.Deliveries.ToList();
    }

    public Order GetByInvoiceNumber(string invoiceNumber)
    {
        return dbContext.Orders.Find(invoiceNumber)??throw new NullReferenceException("Id tidak ada");
    }

    public OrderDetail GetOrderDetailById(long id)
    {
        return dbContext.OrderDetails.Find(id)?? throw new NullReferenceException("Order Detail tidak ada");
    }

    public Product GetProductById(long id)
    {
        return dbContext.Products.Find(id)?? throw new NullReferenceException("Produk tidak ada");
    }

    public List<Product> GetProducts()
    {
        return dbContext.Products.ToList();
    }

    public void Insert(Order order)
    {
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
    }

    public void InsertDetail(OrderDetail orderDetail)
    {
        dbContext.OrderDetails.Add(orderDetail);
        dbContext.SaveChanges();
    }

    public void Update(Order order)
    {
        dbContext.Orders.Update(order);
        dbContext.SaveChanges();
    }

    public void UpdateDetail(OrderDetail orderDetail)
    {
        dbContext.OrderDetails.Update(orderDetail);
        dbContext.SaveChanges();
    }

    public void Delete(Order order)
    {
        dbContext.Orders.Remove(order);
        dbContext.SaveChanges();
    }

    public void DeleteDetail(OrderDetail orderDetail)
    {
        dbContext.OrderDetails.Remove(orderDetail);
        dbContext.SaveChanges();
    }
}
