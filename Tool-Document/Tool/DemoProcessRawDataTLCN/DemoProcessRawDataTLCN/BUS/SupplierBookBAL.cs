using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class SupplierBookBAL
    {
        private SQLManager sql;

        public SupplierBookBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetListSupplierBook()
        {
            string strSQL = "Select Id, Supplier From RawBook Order by Id ASC;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification GetIDSupplier(string name)
        {
            string strSQL = "Select ID From Supplier Where Name Like N'%" + name + "%'";

            return sql.ExecuteScalar(strSQL, CommandType.Text, null);
        }

        public Notification InsertSupplierBook(string book_id, int supplier_id)
        {
            string strSQL = "Insert into SupplierBook Values(@Book_ID, @Supplier_ID)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@Book_ID", book_id),
                new SqlParameter("@Supplier_ID", supplier_id));
        }
    }
}
