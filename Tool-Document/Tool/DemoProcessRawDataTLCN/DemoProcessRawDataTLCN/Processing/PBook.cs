using System;
using System.Collections.Generic;
using System.Text;
using DemoProcessRawDataTLCN.BUS;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PBook
    {
        private BookBAL bookBal;

        public PBook()
        {
            bookBal = new BookBAL();
        }

        public void Execute()
        {
            var ds = bookBal.GetListBook();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var book = new Book()
                {
                    ID = ds.Tables[0].Rows[i][0].ToString(),
                    Name = ds.Tables[0].Rows[i][1].ToString(),
                    OriginalPrice = Int32.Parse(ds.Tables[0].Rows[i][2].ToString()),
                    CurrentPrice = Int32.Parse(ds.Tables[0].Rows[i][2].ToString()),
                    ReleaseYear = int.Parse(ds.Tables[0].Rows[i][3].ToString()),
                    Weight = Double.Parse(ds.Tables[0].Rows[i][4].ToString()),
                    NumOfPage = int.Parse(ds.Tables[0].Rows[i][5].ToString()),
                    Image = ds.Tables[0].Rows[i][6].ToString(),
                    Summary = ds.Tables[0].Rows[i][7].ToString()
                };
                bookBal.InsertBook(book);
                Console.WriteLine(book.ID);
            }
        }
    }
}
