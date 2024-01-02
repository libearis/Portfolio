select top 2 CategoryID, SUM(UnitsInStock)[Jumlah]
from Products
group by CategoryID
order by [Jumlah]
