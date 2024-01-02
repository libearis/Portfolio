use Northwind
select concat(firstname, ' ', lastname) [Full Name], TitleOfCourtesy, HomePhone, Country
from Employees
where country like ('%USA')