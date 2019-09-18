using System;
using System.Data;
using System.Globalization;
using DemoCrawlBookStore;
using DemoProcessRawDataTLCN.BUS;
using DemoProcessRawDataTLCN.Models;
using DemoProcessRawDataTLCN.Processing;

namespace DemoProcessRawDataTLCN
{
    class Program
    {
        static void Main(string[] args)
        {
            PAccount pAccount = new PAccount();
            pAccount.Execute();
        }
    }
}
