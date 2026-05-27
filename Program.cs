using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

string path = @"C:\Users\summe\source\repos\sumzali083\orders-csharp\orders.csv";
string[] lines;
try
{
    lines = File.ReadAllLines(path);
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to read file '{path}': {ex.Message}");
    return;
}

double totalRevenue = 0;
var productRevenue = new Dictionary<string, double>();
var categoryOrders = new Dictionary<string, int>();

// region -> (sum, count) to compute average
var regionStats = new Dictionary<string, (double sum, int count)>();
// Which day of the week gets the most orders?
var dayOfWeekOrders = new Dictionary<string, int>();

foreach (var line in lines.Skip(1))
{
    if (string.IsNullOrWhiteSpace(line)) continue;
    var columns = line.Split(',');
    if (columns.Length < 12) continue; // require at least 12 columns (0..11)

    // delivered revenue (columns[10] == "Delivered") and order amount in columns[9]
    var status = columns[10].Trim();
    if (status.Equals("Delivered", StringComparison.OrdinalIgnoreCase))
    {
        if (double.TryParse(columns[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var revenue))
            totalRevenue += revenue;
    }

    var productName = columns[3].Trim();
    if (double.TryParse(columns[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var lineTotal))
    {
        if (productRevenue.ContainsKey(productName))
            productRevenue[productName] += lineTotal;
        else
            productRevenue[productName] = lineTotal;
    }

    var category = columns[4].Trim();
    if (categoryOrders.ContainsKey(category)) categoryOrders[category]++;
    else categoryOrders[category] = 1;

    // Region is column index 11 (0-based)
    var region = columns[11].Trim();
    if (double.TryParse(columns[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var orderAmount))
    {
        if (regionStats.ContainsKey(region))
            regionStats[region] = (regionStats[region].sum + orderAmount, regionStats[region].count + 1);
        else
            regionStats[region] = (orderAmount, 1);
    }
    var dayOfWeek = columns[2]
}

// Results
var uniqueOrders = lines.Skip(1).Select(x => x.Split(',')[0]).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().Count();

// Safe top product
if (productRevenue.Any())
{
    var topProduct = productRevenue.OrderByDescending(p => p.Value).First();
    Console.WriteLine($"Top product: {topProduct.Key} - £{topProduct.Value:F2}");
}
else
{
    Console.WriteLine("Top product: (no product revenue data)");
}

// Safe top category
if (categoryOrders.Any())
{
    var topCategory = categoryOrders.OrderByDescending(c => c.Value).First();
    Console.WriteLine($"Top category: {topCategory.Key} - {topCategory.Value} orders");
}
else
{
    Console.WriteLine("Top category: (no category data)");
}

// top region by average order value (safe)
if (regionStats.Any())
{
    var topRegionByAvg = regionStats
        .Where(kv => kv.Value.count > 0)
        .Select(kv => new { Region = kv.Key, Avg = kv.Value.sum / kv.Value.count, Sum = kv.Value.sum })
        .OrderByDescending(x => x.Avg)
        .FirstOrDefault();

    if (topRegionByAvg != null)
        Console.WriteLine($"Top region by avg order value: {topRegionByAvg.Region} - £{topRegionByAvg.Avg:F2} (sum £{topRegionByAvg.Sum:F2})");
    else
        Console.WriteLine("Top region by avg order value: (insufficient region data)");
}
else
{
    Console.WriteLine("Top region by avg order value: (no region data)");
}

Console.WriteLine($"Number of orders: {uniqueOrders}");
Console.WriteLine($"Total delivered revenue: £{totalRevenue:F2}");