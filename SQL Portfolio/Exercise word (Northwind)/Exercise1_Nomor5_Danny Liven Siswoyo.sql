select s.Country, AVG(p.unitprice) as [Harga rata-rata]
from Suppliers s
join Products p on s.SupplierID = p.SupplierID
--join [Order Details] od on p.ProductID = od.ProductID
where country like ('%Germany%') or country like ('%Spain%') or country like ('%Sweden%') or 
	  country like ('%Denmark%') or country like ('%Norway%') or country like ('%Italy%') or
	  country like ('%Netherland%') or country like ('%Finland%') or country like ('%France%')
Group by s.Country
having AVG(p.unitprice) <= 50
order by [Harga rata-rata] desc
