using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
string[] text = File.ReadAllLines(@"C:\Users\summe\source\repos\sumzali083\orders-csharp\orders.csv");

//Console.WriteLine(text);
double totalRevenue = 0;

var products = new Dictionary<string, int>();

foreach (string x in text)
{
    string[] columns = x.Split(",");
    //number of orders
    //Console.WriteLine(columns[0]);
    if (columns[10] == "Delivered") {
        double revenue = double.Parse(columns[9]);
        //calculate total revenue
        totalRevenue += revenue;
    }
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
//max orders 
int max = text.Length - 1;
Console.WriteLine("number of orders = "+max);
Console.WriteLine(totalRevenue);
//what product has the most orders
Console.WriteLine("Product with the most revenue:" + products);
