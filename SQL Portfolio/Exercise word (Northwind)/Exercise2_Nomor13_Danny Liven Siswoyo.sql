select ProductName[Name], c.CategoryName [Category], s.CompanyName [Supplier], UnitPrice [Price]
from Products p
join Suppliers s on p.SupplierID = s.SupplierID
join Categories c on p.CategoryID = c.CategoryID
for xml raw('Product'), root('Products') , elements