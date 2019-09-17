using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class PublisherBookBAL
    {
        private SQLManager sql;

        public PublisherBookBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetListPublisherBook()
        {
            string strSQL = "Select Id, Publisher From RawBook Order by Id ASC;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertPublisherBook(string book_id, int publisher_id)
        {
            string strSQL = "Insert into PublisherBook Values(@Book_ID, @Publisher_ID)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@Book_ID", book_id),
                new SqlParameter("@Publisher_ID", publisher_id));
        }

        public Notification GetIDPublisher(string name)
        {
            string strSQL = "";
            if (name.Contains("'"))
            {
                strSQL = "Select ID From Publisher Where Name Like N'%";
                var tmp = name.Split("'");
                for (int i = 0; i < tmp.Length - 1; i++)
                {
                    strSQL += tmp[i] + "''";
                }
                strSQL += tmp[tmp.Length - 1] + "%'";
            }
            else
            {
                strSQL = "Select ID From Publisher Where Name Like N'%" + name + "%'";
            }

            return sql.ExecuteScalar(strSQL, CommandType.Text, null);
        }
    }
}
