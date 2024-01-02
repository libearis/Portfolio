select p.ProductName, s.CompanyName, c.CategoryName, p.QuantityPerUnit, p.UnitsInStock
from Products p
join Suppliers s on p.SupplierID = s.SupplierID 
join Categories c on p.CategoryID = c.CategoryID
where QuantityPerUnit like ('%jar%') or QuantityPerUnit like ('%bottle%') or QuantityPerUnit like ('%glass%')
