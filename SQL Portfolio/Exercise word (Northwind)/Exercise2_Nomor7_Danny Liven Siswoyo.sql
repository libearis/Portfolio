select RegionDescription, COUNT(1) [Total Employee]
from Territories t
join Region r on t.RegionID = r.RegionID
join EmployeeTerritories et on t.TerritoryID = et.TerritoryID
group by RegionDescription
order by [Total Employee] desc, RegionDescription asc
