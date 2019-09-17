using System;
using System.Data;
using System.Globalization;
using DemoCrawlBookStore;
using DemoProcessRawDataTLCN.BUS;
using DemoProcessRawDataTLCN.Processing;

namespace DemoProcessRawDataTLCN
{
    class Program
    {
        static void Main(string[] args)
        {
            PBook pBook = new PBook();
            pBook.Execute();

            //var name = "HarperCollins Children's Books";
            //var tmp = name.Split("'");
            //foreach (var str in tmp)
            //{
            //    Console.WriteLine(str);
            //}

            //string name = "DAW BOOKS";
            //Console.WriteLine(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower()));
        }
    }
}
