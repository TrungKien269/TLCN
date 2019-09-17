using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoCrawlBookStore.DAL;
using DemoCrawlBookStore.Models;

namespace DemoCrawlBookStore.BUS
{
    public class PublisherBAL
    {
        private SQLManager sql;

        public PublisherBAL()
        {
            sql = new SQLManager();
        }

        public Notification InsertPublisher(Publisher publisher)
        {
            string strSQL = "Insert into RawPublisher Values(@Name, @Image, @Description)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@Name", publisher.Name),
                new SqlParameter("@Image", publisher.Image), new SqlParameter("@Description", publisher.Description));
        }
    }
}
