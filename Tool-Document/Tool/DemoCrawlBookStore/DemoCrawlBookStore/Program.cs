using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DemoCrawlBookStore.BUS;
using DemoCrawlBookStore.CrawlHelper;
using DemoCrawlBookStore.Models;

namespace DemoCrawlBookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Crawler crawl = new Crawler();

            crawl.CrawlListPublisher();

            //crawl.CrawlListImageBook();
            //crawl.CrawlListFailureImageBook();
            //crawl.CrawlListBook();
            //crawl.CrawlCategory();

            //string name = "LOVE & STORY";
            //name = Regex.Replace(name, @"[^0-9a-zA-Z]+", "");
            //var tmp = name.Replace(" ", String.Empty).All(x => char.IsUpper(x) || char.IsLower(x));
            //Console.WriteLine(tmp);
            //Console.WriteLine(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower()));
        }
    }
}
