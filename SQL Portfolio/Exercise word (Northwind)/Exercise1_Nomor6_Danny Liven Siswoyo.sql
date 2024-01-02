select top 1 s.CompanyName, count(ShipVia) [Total Order]
from Orders o
join Shippers s on o.ShipVia = s.ShipperID
group by s.CompanyName
order by [Total Order] desc