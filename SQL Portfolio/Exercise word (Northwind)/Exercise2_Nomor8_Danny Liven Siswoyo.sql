select 
	c.ContactName [Full Name],
	'Customer' [Entitas],
	c.Phone [Nomor Telepon]
from 
	Customers c
union
select 
	CONCAT(FirstName, ' ', LastName)[Full Name],
	'Employee',
	e.HomePhone
from 
	Employees e
union
select 
	s.ContactName [Full Name],
	'Supplier',
	s.Phone
from 
	Suppliers s
order by
	[Full Name] desc