using BasiliskBusiness.Interfaces;
using BasiliskWeb.ViewModels;
using BasiliskWeb.ViewModels.Order;
using TradeOfBasiliskDataAccess.Models;
namespace BasiliskWeb.Services;

public class OrderService
{
    private readonly IOrderRepository repository;

    public OrderService(IOrderRepository orderRepository)
    {
        repository = orderRepository;
    }

    public string GetSalesmanFullName(string SalesEmployeeNumber)
    {
        var sales = repository.GetSalesman(SalesEmployeeNumber);
        return $"{sales.FirstName} {sales.LastName}";
    }

    public List<OrderCustomerVM> GetOrderCustomerVMs()
    {
        return repository.GetCustomers().Select(cus => new OrderCustomerVM()
        {
            Id = cus.Id,
            CompanyName = cus.CompanyName
        }).ToList();
    }

    public List<OrderSalesmanVM> GetOrderSalesmanVMs()
    {
        return repository.GetSalesmens().Select(sal => new OrderSalesmanVM()
        {
            SalesEmployeeNumber = sal.EmployeeNumber,
            SalesEmployeeFullName = $"{sal.FirstName} {sal.LastName}"
        }).ToList();
    }

    public List<OrderDeliveryVM> GetOrderDeliveryVMs()
    {
        return repository.GetDeliveries().Select(del => new OrderDeliveryVM()
        {
            Id = del.Id,
            CompanyName = del.CompanyName
        }).ToList();
    }

    public List<OrderProductVM> GetOrderProductVMs()
    {
        return repository.GetProducts().Select(pro => new OrderProductVM()
        {
            Id = pro.Id,
            ProductName = pro.Name
        }).ToList();
    }

    public string GetNewInvoiceNumber()
    {
        List<string> lastInvoiceNumber = repository.GetInvoiceNumbers();
        int incrementInoviceNumber = Int32.Parse(lastInvoiceNumber[0].Substring(6));
        incrementInoviceNumber += 1;
        string newInvoiceNumber = lastInvoiceNumber[0].Substring(0,6) + incrementInoviceNumber.ToString();
        return newInvoiceNumber;
    }

    public decimal GetDeliveryCost(long id)
    {
        var delivery = repository.GetDeliveryCost(id);
        return delivery.Cost;
    }

    public IndexVM Get(int pageNumber, int pageSize, string invoiceNumber, long customerId, string salesEmployeeNumber, long deliveryId, DateTime orderDate)
    {
        var model = repository.Get(pageNumber, pageSize, invoiceNumber, customerId, salesEmployeeNumber, deliveryId, orderDate).Select(ord => new OrderVM(){
            InvoiceNumber = ord.InvoiceNumber,
            CustomerName = repository.GetCustomer(ord.CustomerId).CompanyName,
            SalesEmployeeNumber = GetSalesmanFullName(ord.SalesEmployeeNumber),
            DeliveryName = repository.GetDelivery(ord.DeliveryId).CompanyName,
            OrderDate = ord.OrderDate
        });

        return new IndexVM()
        {
            Orders = model.ToList(),
            Customers = GetOrderCustomerVMs(),
            Salesmans = GetOrderSalesmanVMs(),
            Deliveries = GetOrderDeliveryVMs(),
            InvoiceNumber = invoiceNumber,
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.Count(invoiceNumber, customerId, salesEmployeeNumber, deliveryId, orderDate)
            },
            CustomerId = customerId,
            SalesEmployeeNumber = salesEmployeeNumber,
            DeliveryId = deliveryId,
            OrderDate = orderDate
        };
    }

    public OrderDetailIndexVM GetDetailIndexVM(string invoiceNumber, string customerName, string salesEmployeeNumber, DateTime orderDate, int pageNumber, int pageSize)
    {
        var model = repository.GetDetail(invoiceNumber, pageNumber, pageSize).Select(det=> new OrderDetailVM()
        {
            Id = det.Id,
            InvoiceNumber = det.InvoiceNumber,
            ProductName = repository.GetProductById(det.ProductId).Name,
            UnitPrice = det.UnitPrice,
            Quantity = det.Quantity,
            Discount = det.Discount
        });

        return new OrderDetailIndexVM()
        {
            OrderDetails = model.ToList(),
            Pagination = new PaginationViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalSize = repository.CountDetail(invoiceNumber)
            },
            InvoiceNumber = invoiceNumber,
            CustomerName = customerName,
            SalesName = salesEmployeeNumber,
            OrderDate = orderDate
        };
    }

    public OrderInsertVM GetListForInsert()
    {
        return new OrderInsertVM()
        {
            Customers = GetOrderCustomerVMs(),
            Salesmans = GetOrderSalesmanVMs(),
            Deliveries = GetOrderDeliveryVMs()
        };
    }

    public OrderUpdateVM GetByInvoiceNumber(string invoiceNumber)
    {
        var model = repository.GetByInvoiceNumber(invoiceNumber);

        return new OrderUpdateVM()
        {
            Customers = GetOrderCustomerVMs(),
            Salesmans = GetOrderSalesmanVMs(),
            Deliveries = GetOrderDeliveryVMs(),
            
            InvoiceNumber = model.InvoiceNumber,
            CustomerId = model.CustomerId,
            SalesEmployeeNumber = model.SalesEmployeeNumber,
            OrderDate = model.OrderDate,
            ShippedDate = model.ShippedDate,
            DueDate = model.DueDate,
            DeliveryId = model.DeliveryId,
            DestinationAddress = model.DestinationAddress,
            DestinationCity = model.DestinationCity,
            DestinationPostalCode = model.DestinationPostalCode
        };
    }

    public OrderDetailInsertVM GetDetailInsert(string invoiceNumber)
    {
        return new OrderDetailInsertVM()
        {
            InvoiceNumber = invoiceNumber,
            Products = GetOrderProductVMs(),
        };
    }

    public OrderDetailUpdateVM GetDetailById(long id)
    {
        var model = repository.GetOrderDetailById(id);

        return new OrderDetailUpdateVM()
        {
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            Products = GetOrderProductVMs(),
            ProductId = model.ProductId,
            UnitPrice = model.UnitPrice,
            Quantity = model.Quantity,
            Discount = model.Discount
        };
    }

    public void Insert(OrderInsertVM viewModel)
    {
        var model = new Order()
        {
            InvoiceNumber = GetNewInvoiceNumber(),
            CustomerId = viewModel.CustomerId,
            SalesEmployeeNumber = viewModel.SalesEmployeeNumber,
            OrderDate = viewModel.OrderDate,
            ShippedDate = viewModel.ShippedDate,
            DueDate = viewModel.DueDate,
            DeliveryId = viewModel.DeliveryId,
            DeliveryCost = GetDeliveryCost(viewModel.DeliveryId),
            DestinationAddress = viewModel.DestinationAddress,
            DestinationCity = viewModel.DestinationCity,
            DestinationPostalCode = viewModel.DestinationPostalCode
        };

        repository.Insert(model);
    }

    public void InsertDetail(OrderDetailInsertVM viewModel)
    {
        var model = new OrderDetail()
        {
            InvoiceNumber = viewModel.InvoiceNumber,
            ProductId = viewModel.ProductId,
            UnitPrice = repository.GetProductById(viewModel.ProductId).Price,
            Quantity = viewModel.Quantity,
            Discount = viewModel.Discount
        };
        
        repository.InsertDetail(model);
    }

    public void Update(OrderUpdateVM viewModel)
    {
        var model = repository.GetByInvoiceNumber(viewModel.InvoiceNumber);
        model.CustomerId = viewModel.CustomerId;
        model.SalesEmployeeNumber = viewModel.SalesEmployeeNumber;
        model.OrderDate = viewModel.OrderDate;
        model.ShippedDate = viewModel.ShippedDate;
        model.DueDate = viewModel.DueDate;
        model.DeliveryId = viewModel.DeliveryId;
        model.DeliveryCost = GetDeliveryCost(viewModel.DeliveryId);
        model.DestinationAddress = viewModel.DestinationAddress;
        model.DestinationCity = viewModel.DestinationCity;
        model.DestinationPostalCode = viewModel.DestinationPostalCode;

        repository.Update(model);
    }

    public void UpdateDetail(OrderDetailUpdateVM viewModel)
    {
        var model = repository.GetOrderDetailById(viewModel.Id);
        model.ProductId = viewModel.ProductId;
        model.UnitPrice = repository.GetProductById(viewModel.ProductId).Price;
        model.Quantity = viewModel.Quantity;
        model.Discount = viewModel.Discount;

        repository.UpdateDetail(model);
    }

    public void HardDelete(string invoiceNumber)
    {
        var model = repository.GetByInvoiceNumber(invoiceNumber);
        repository.Delete(model);
    }

    public void HardDeleteDetail(long id)
    {
        var model = repository.GetOrderDetailById(id);
        repository.DeleteDetail(model);
    }
}
