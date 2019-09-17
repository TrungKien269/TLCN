using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class AuthorBookBAL
    {
        private SQLManager sql;

        public AuthorBookBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetListAuthorBook()
        {
            string strSQL = "Select Id, Author From RawBook Order by Id ASC;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertAuthorBook(string book_id, int author_id)
        {
            string strSQL = "Insert into AuthorBook Values(@Book_ID, @Author_ID)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@Book_ID", book_id),
                new SqlParameter("@Author_ID", author_id));
        }

        public Notification GetIDAuthor(string name)
        {
            string strSQL = "Select ID From Author Where Name Like N'%" + name + "%'";

            return sql.ExecuteScalar(strSQL, CommandType.Text, null);
        }
    }
}
