using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class FormBookBAL
    {
        private SQLManager sql;

        public FormBookBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetListFormBook()
        {
            string strSQL = "Select Id, Form From RawBook Order by Id ASC;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification GetIDForm(string name)
        {
            string strSQL = "Select ID From Form Where Name Like N'%" + name + "%'";

            return sql.ExecuteScalar(strSQL, CommandType.Text, null);
        }

        public Notification InsertFormBook(string book_id, int form_id)
        {
            string strSQL = "Insert into FormBook Values(@Book_ID, @Form_ID)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@Book_ID", book_id),
                new SqlParameter("@Form_ID", form_id));
        }
    }
}
