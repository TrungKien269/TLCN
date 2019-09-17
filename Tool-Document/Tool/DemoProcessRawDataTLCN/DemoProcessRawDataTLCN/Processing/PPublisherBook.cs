using System;
using System.Collections.Generic;
using System.Text;
using DemoProcessRawDataTLCN.BUS;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PPublisherBook
    {
        private PublisherBookBAL publisherBookBal;

        public PPublisherBook()
        {
            publisherBookBal = new PublisherBookBAL();
        }

        public void Execute()
        {
            var ds = publisherBookBal.GetListPublisherBook();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var book_id = ds.Tables[0].Rows[i][0].ToString();
                if (publisherBookBal.GetIDPublisher(ds.Tables[0].Rows[i][1].ToString()).Obj != null)
                {
                    var publisher_id = int.Parse(publisherBookBal.GetIDPublisher(ds.Tables[0].Rows[i][1].ToString()).Obj
                        .ToString());
                    publisherBookBal.InsertPublisherBook(book_id, publisher_id);
                    Console.WriteLine(book_id);
                }
            }
        }
    }
}
