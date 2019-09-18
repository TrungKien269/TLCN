using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class UserBAL
    {
        private SQLManager sql;

        public UserBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetListUser()
        {
            string strSQL = "Select ID, FullName, Gender, Birthday, PhoneNumber, Address, Country From RawUser;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertUser(User user)
        {
            string strSQL =
                "Insert into [User] Values(@ID, @FullName, @Gender, @Birthday, @PhoneNumber, @Address)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID", user.ID),
                new SqlParameter("@FullName", user.FullName), new SqlParameter("@Gender", user.Gender),
                new SqlParameter("@Birthday", user.Birthday), new SqlParameter("@PhoneNumber", user.PhoneNumber),
                new SqlParameter("@Address", user.Address));
        }
    }
}
