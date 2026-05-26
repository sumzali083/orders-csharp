using System;
using System.IO;
using System.Linq;

string[] text = File.ReadAllLines(@"C:\Users\summe\source\repos\sumzali083\orders-csharp\orders.csv");

double totalRevenue = 0;
var productRevenue = new Dictionary<string, double>();

foreach (string x in text.Skip(1))
{
    string[] columns = x.Split(",");

    // Total revenue from delivered orders
    if (columns[10] == "Delivered")
    {
        double revenue = double.Parse(columns[9]);
        totalRevenue += revenue;
    }

    // Build up revenue per product
    string productName = columns[3];
    double lineTotal = double.Parse(columns[9]);

    if (productRevenue.ContainsKey(productName))
    {
        productRevenue[productName] += lineTotal;
    }
    else
    {
        productRevenue[productName] = lineTotal;
    }
}

// Results
var uniqueOrders = text.Skip(1).Select(x => x.Split(",")[0]).Distinct().Count();
var topProduct = productRevenue.OrderByDescending(p => p.Value).First();

Console.WriteLine($"Number of orders: {uniqueOrders}");
Console.WriteLine($"Total delivered revenue: £{totalRevenue:F2}");
Console.WriteLine($"Top product: {topProduct.Key} - £{topProduct.Value:F2}");