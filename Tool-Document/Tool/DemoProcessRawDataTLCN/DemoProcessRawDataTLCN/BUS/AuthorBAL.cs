using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class AuthorBAL
    {
        private SQLManager sql;

        public AuthorBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetAuthors()
        {
            string strSQL = "Select Distinct Author From RawBook";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertAuthor(int id, string name)
        {
            string strSQL = "Insert into Author Values(@ID, @Name)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID", id),
                new SqlParameter("@Name", name));
        }
    }
}
