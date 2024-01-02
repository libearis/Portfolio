select t.TerritoryDescription, concat(e.firstname, ' ', e.LastName)[Full Name]
from Territories t
join EmployeeTerritories et on t.TerritoryID = et.TerritoryID
join Employees e on et.EmployeeID = e.EmployeeID