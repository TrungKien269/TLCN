using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoProcessRawDataTLCN.DAL;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.BUS
{
    public class BookBAL
    {
        private SQLManager sql;

        public BookBAL()
        {
            sql = new SQLManager();
        }

        public DataSet GetListBook()
        {
            string strSQL = "Select Id, Name, Price, ReleaseYear, Weight, NumOfPage, Image, Summary From RawBook;";

            return sql.ExecuteReader(strSQL, CommandType.Text, null);
        }

        public Notification InsertBook(Book book)
        {
            string strSQL =
                "Insert into Book Values(@ID, @Name, @OriginalPrice, @CurrentPrice, @ReleaseYear, @Weight, " +
                "@NumOfPage, @Image, @Summary)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID", book.ID),
                new SqlParameter("@Name", book.Name), new SqlParameter("@OriginalPrice", book.OriginalPrice),
                new SqlParameter("@CurrentPrice", book.CurrentPrice), new SqlParameter("@ReleaseYear", book.ReleaseYear),
                new SqlParameter("@Weight", book.Weight), new SqlParameter("@NumOfPage", book.NumOfPage),
                new SqlParameter("@Image", book.Image), new SqlParameter("@Summary", book.Summary));
        }
    }
}
