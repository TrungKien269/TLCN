using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class SupplierBAL
    {
        private SQLManager sql;

        public SupplierBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetSuppliers()
        {
            string strSQL = "Select Distinct Supplier From RawBook Order by Supplier ASC;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertSupplier(int id, string name)
        {
            string strSQL = "Insert into Supplier Values(@ID, @Name)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID", id),
                new SqlParameter("@Name", name));
        }
    }
}
