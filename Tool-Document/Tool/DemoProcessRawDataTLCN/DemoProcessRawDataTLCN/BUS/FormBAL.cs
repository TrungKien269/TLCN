using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class FormBAL
    {
        private SQLManager sql;

        public FormBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetForms()
        {
            string strSQL = "Select Distinct Form From RawBook Order by Form ASC;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertForm(int id, string name)
        {
            string strSQL = "Insert into Form Values(@ID, @Name)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID", id),
                new SqlParameter("@Name", name));
        }
    }
}
