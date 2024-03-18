using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TradeOfBasiliskDataAccess.Models;

namespace TradeOfBasilisk;
class Program
{
    //Link Zoom = https://us06web.zoom.us/j/9414779273?pwd=YTRsMkg1RWlqOGh4bFhaYTJnUTRpZz09
    static void Main(string[] args)
    {
        // FirstQuery();
        // SecondQuery();
        // ThirdQuery();
        // FourthQuery();
        DateTime testTime = new DateTime(2012,4,18);
        System.Console.WriteLine(testTime.ToString("MM-dd-0001"));
        List<string> model = new List<string>()
        {
            "04-18-0002",
            "04-18-0003",
            "04-18-0004",
            "04-18-0005",
            "04-18-0006",
            "04-18-0007"
        };
        foreach(var invoice in model)
        {
            System.Console.WriteLine(invoice);
            if(testTime.ToString("MM-dd").Contains(""))
            {
                System.Console.WriteLine("Udah ada");
            }
            else
            {
                System.Console.WriteLine("Baru gk ada");
            }
        }
    }

    private static void FirstQuery()
    {
        var context = new BasiliskTFContext();
        var categories = from cat in context.Categories join prod in context.Products on cat.Id equals prod.CategoryId
        select cat;

        Dictionary<string, int> categoriesDictionary = new Dictionary<string, int>(); 
        foreach(var category in categories)
        {
            if(categoriesDictionary.ContainsKey(category.Name))
            {
                categoriesDictionary[category.Name] += 1;
                continue;
            }
            categoriesDictionary.Add(category.Name, 1);
        }

        System.Console.WriteLine("Category Name | Total Product in Category");
        System.Console.WriteLine("============================================");
        foreach(KeyValuePair<string, int> keyValuePair in categoriesDictionary)
        {
            if(keyValuePair.Key == "Mainan" || keyValuePair.Key == "Buku" || keyValuePair.Key == "Dapur" || keyValuePair.Key == "Gaming")
            {
                System.Console.WriteLine($"{keyValuePair.Key} \t\t\t{keyValuePair.Value}");
            }
            else
            {
                System.Console.WriteLine($"{keyValuePair.Key} \t\t{keyValuePair.Value}");
            }
        }
    }

    private static void SecondQuery()
    {
        var context = new BasiliskTFContext();
        var regions = context.Regions.Include(sal => sal.SalesmanEmployeeNumbers).ToList();
        int count = 0;
        System.Console.WriteLine("City\t\t| Total Salesman");
        System.Console.WriteLine("===================================");
        foreach(Region region in regions)
        {
            foreach(Salesman sales in region.SalesmanEmployeeNumbers)
            {
                count++;
            }
            if(region.City == "Bandung" || region.City == "Bekasi" || 
            region.City == "Bogor" || region.City == "Malang" ||
            region.City == "Jakarta" || region.City == "Medan")
            {
                System.Console.WriteLine($"{region.City}\t\t\t {count}");
                count = 0;
            }
            else
            {
                System.Console.WriteLine($"{region.City}\t\t {count}");
                count = 0;
            }
        }
    }
    
    private static void ThirdQuery()
    {
        var context = new BasiliskTFContext();
        var salesName = from salsUp in context.Salesmen join sal in context.Salesmen on salsUp.EmployeeNumber equals sal.SuperiorEmployeeNumber
        select sal;

        var context2 = new BasiliskTFContext();
        var superiorName = from salsUp in context2.Salesmen join sal in context2.Salesmen on salsUp.EmployeeNumber equals sal.SuperiorEmployeeNumber
        select salsUp;

        System.Console.WriteLine("Superior      |  Staff");
        System.Console.WriteLine("==================================");

        //Cara 1
        // foreach(var superior in superiorName)
        // {
        //     foreach(var sales in salesName)
        //     {
        //         System.Console.WriteLine($"{superior.FirstName} {superior.LastName} \t{sales.FirstName} {sales.LastName}");
        //     }
        //     break;
        // }

        //Cara 2(Lebih Dinamis)
        Dictionary<string, string>totalSales = new Dictionary<string, string>();
        foreach(var superior in superiorName)
        {
            foreach(var sales in salesName)
            {
                if(!totalSales.ContainsKey($"{sales.FirstName} {sales.LastName}"))
                {
                    totalSales.Add($"{sales.FirstName} {sales.LastName}", $"{superior.FirstName} {superior.LastName}");
                }
            }
        }

        foreach(KeyValuePair<string, string> keyValuePair in totalSales)
        {
            System.Console.WriteLine($"{keyValuePair.Value}\t {keyValuePair.Key}");
        }
    }

    private static void FourthQuery()
    {
        var context = new BasiliskTFContext();
        var salesName = from sal in context.Salesmen
        select sal;

        var context2 = new BasiliskTFContext();
        var superiorName = from salsUp in context2.Salesmen join sal in context2.Salesmen on salsUp.EmployeeNumber equals sal.SuperiorEmployeeNumber
        select salsUp;

        Dictionary<string, int> staffList = new Dictionary<string, int>();
        foreach(var sales in salesName)
        {
            staffList.Add(sales.EmployeeNumber, 0);
        }

        foreach(var sales in salesName)
        {
            if(sales.SuperiorEmployeeNumber != null)
            {
                staffList[sales.SuperiorEmployeeNumber] += 1;
            }
        }
        
        foreach(var sales in salesName)
        {
            if(staffList.ContainsKey(sales.SuperiorEmployeeNumber))
            {
                
            }
        }
        
        // Dictionary<string, int> superiorList = new Dictionary<string, int>();
        // foreach(var superior in superiorName)
        // {
        //     if(!superiorList.ContainsKey($"{superior.FirstName} {superior.LastName}"))
        //     {
        //         superiorList.Add($"{superior.FirstName} {superior.LastName}", 1);
        //     }
        //     else
        //     {
        //         superiorList[$"{superior.FirstName} {superior.LastName}"] += 1;
        //     }
        // }

        // Dictionary<string, int> salesAndTheirStaff = new Dictionary<string, int>();
        // foreach(var sales in salesName)
        // {
        //     if(superiorList.ContainsKey($"{sales.FirstName} {sales.LastName}"))
        //     {
        //         salesAndTheirStaff.Add($"{sales.FirstName} {sales.LastName}", superiorList[$"{sales.FirstName} {sales.LastName}"]);
        //     }
        //     else
        //     {
        //         salesAndTheirStaff.Add($"{sales.FirstName} {sales.LastName}", 0);
        //     }
        // }

        // System.Console.WriteLine("Superior\t\t| Total Staff");
        // System.Console.WriteLine("=======================================");
        // foreach(KeyValuePair<string,int> keyValuePair in salesAndTheirStaff)
        // {
        //     if(keyValuePair.Key == "Olivia Puspasari" || keyValuePair.Key.Contains("Agusalim") ||
        //     keyValuePair.Key.Contains("Santoso") || keyValuePair.Key.Contains("Jayadi"))
        //     {
        //         System.Console.WriteLine($"{keyValuePair.Key}\t     {keyValuePair.Value}");    
        //     }
        //     else
        //     {
        //         System.Console.WriteLine($"{keyValuePair.Key}\t\t     {keyValuePair.Value}");
        //     }
        // }
    }
}
