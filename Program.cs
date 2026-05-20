using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
string[] text = File.ReadAllLines(@"C:\Users\summe\source\repos\sumzali083\orders-csharp\orders.csv");

//Console.WriteLine(text);
double totalRevenue = 0;
double 
foreach (string x in text)
{
    string[] columns = x.Split(",");
    Console.WriteLine(columns[0]);
    if (columns[10] == "Delivered") {
        double revenue = double.Parse(columns[9]);
        totalRevenue += revenue;
    }
}

int max = text.Length - 1;
Console.WriteLine("number of orders = "+max);
Console.WriteLine(totalRevenue);