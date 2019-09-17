using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DemoProcessRawDataTLCN.BUS;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PPublisher
    {
        private PublisherBAL publisherBal;

        public PPublisher()
        {
            publisherBal = new PublisherBAL();
        }

        public void Execute()
        {
            var ds = publisherBal.GetPublishers();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var name = ds.Tables[0].Rows[i][0].ToString();
                publisherBal.InsertPublisher(i + 1, name);
            }
        }
    }
}
