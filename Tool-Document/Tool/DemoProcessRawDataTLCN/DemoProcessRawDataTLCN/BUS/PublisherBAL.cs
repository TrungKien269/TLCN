using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class PublisherBAL
    {
        private SQLManager sql;

        public PublisherBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetPublishers()
        {
            string strSQL = "Select Distinct Publisher From RawBook Order by Publisher ASC;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertPublisher(int id, string name)
        {
            string strSQL = "Insert into Publisher Values(@ID, @Name)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID", id),
                new SqlParameter("@Name", name));
        }
    }
}
