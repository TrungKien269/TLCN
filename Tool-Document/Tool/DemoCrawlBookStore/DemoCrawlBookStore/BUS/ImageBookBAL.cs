using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoCrawlBookStore.DAL;
using DemoCrawlBookStore.Models;

namespace DemoCrawlBookStore.BUS
{
    public class ImageBookBAL
    {
        private SQLManager sql;

        public ImageBookBAL()
        {
            sql = new SQLManager();
        }

        public Notification InsertImageBook(ImageBook imageBook)
        {
            string strSQL = "INSERT INTO RawImageBook VALUES(@ID_Book, @ID_Image, @Path)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@ID_Book", imageBook.id_book),
                new SqlParameter("@ID_Image", imageBook.id_image), new SqlParameter("@Path", imageBook.path));
        }

        public Notification CheckExistedBook(string isbn)
        {
            string strSQL = "Select Count(*) From RawBook Where Id = @Id";

            return sql.ExecuteScalar(strSQL, CommandType.Text, new SqlParameter("@Id", isbn));
        }

        public Notification CheckDuplicateBook(string isbn)
        {
            string strSQL = "Select Count(*) From RawImageBook Where ID_Book = @Id";

            return sql.ExecuteScalar(strSQL, CommandType.Text, new SqlParameter("@Id", isbn));
        }
    }
}
