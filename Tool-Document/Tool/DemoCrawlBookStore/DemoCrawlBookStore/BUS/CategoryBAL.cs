using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoCrawlBookStore.DAL;
using DemoCrawlBookStore.Models;

namespace DemoCrawlBookStore.BUS
{
    public class CategoryBAL
    {
        private SQLManager sql;

        public CategoryBAL()
        {
            sql = new SQLManager();
        }

        public Notification InsertCategory(string name, int region)
        {
            string strSQL = "INSERT INTO Category VALUES(@Name, @Region)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text,
                new SqlParameter("@Name", name) {DbType = DbType.String},
                new SqlParameter("@Region", region) {DbType = DbType.Int32});
        }

        public Notification InsertSubCategory(string name, int cateID)
        {
            string strSQL = "INSERT INTO SubCategory VALUES(@Name, @CateID)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text,
                new SqlParameter("@Name", name) {DbType = DbType.String},
                new SqlParameter("@CateID", cateID) {DbType = DbType.Int32});
        }

        public Notification GetCategoryID(string name)
        {
            string strSQL = "Select ID From SubCategory Where Name = @Name";

            return sql.ExecuteScalar(strSQL, CommandType.Text,
                new SqlParameter("@Name", name) {DbType = DbType.String});
        }

        public DataSet GetSubCategoryNameList()
        {
            string strSQL = "Select Name From SubCategory";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }
    }
}
