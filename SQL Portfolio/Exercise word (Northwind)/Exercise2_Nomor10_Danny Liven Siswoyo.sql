select [Job Tittle], tblPivot.Northern, tblPivot.Eastern, tblPivot.Southern, tblPivot.Western
from(
select r.RegionDescription, e.Title [Job Tittle], count(1) [totalEmployee]
from Employees e
join EmployeeTerritories et on et.EmployeeID = e.EmployeeID
join Territories t on et.TerritoryID = t.TerritoryID
join region r on t.RegionID = r.RegionID
group by e.Title, r.RegionDescription
) as tblTotal
pivot(sum(totalEmployee) for RegionDescription in([Northern], [Eastern], [Southern], [Western])) as tblPivot