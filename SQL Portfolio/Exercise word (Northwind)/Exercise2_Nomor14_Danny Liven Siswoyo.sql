select o.OrderID, format(o.OrderDate, 'dd MMMM yyyy', 'id-ID'), od.Quantity
from Orders o
join [Order Details] od on o.OrderID = od.OrderID
order by OrderDate