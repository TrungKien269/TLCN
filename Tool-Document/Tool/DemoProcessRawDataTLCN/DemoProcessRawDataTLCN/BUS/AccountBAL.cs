using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class AccountBAL
    {
        private SQLManager sql;

        public AccountBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetListAccount()
        {
            string strSQL = "Select ID, Username, Password, Email, CreatedDate, CreatedTime From RawUser;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertAccount(Account account)
        {
            string strSQL = "Insert into Account Values(@ID, @Username, @Password, @Salt, @Email, @CreatedDateTime)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID", account.ID),
                new SqlParameter("@Username", account.Username), new SqlParameter("@Password", account.Password),
                new SqlParameter("@Salt", account.Salt), new SqlParameter("@Email", account.Email),
                new SqlParameter("@CreatedDateTime", account.CrearedDateTime));
        }
    }
}
