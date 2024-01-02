select concat(FirstName, ' ', LastName) [Full Name], datediff(YEAR, BirthDate, GETDATE())[Umur]
from Employees
order by Umur desc