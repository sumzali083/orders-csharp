using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
string[] text = File.ReadAllLines("orders.csv");

//Console.WriteLine(text);

foreach (string x in text)
{
    string[] columns = x.Split(",");
    Console.WriteLine(columns[0]);
    
}

int max = text.Length - 1;
Console.WriteLine("number of orders = "+max);
