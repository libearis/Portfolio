select ProductName, UnitPrice
from Products
where Discontinued = 0
order by UnitPrice desc