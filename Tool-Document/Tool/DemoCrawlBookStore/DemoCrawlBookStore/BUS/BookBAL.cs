using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DemoCrawlBookStore.DAL;
using DemoCrawlBookStore.Models;

namespace DemoCrawlBookStore.BUS
{
    public class BookBAL
    {
        private SQLManager sql;

        public BookBAL()
        {
            sql = new SQLManager();
        }

        public Notification InsertBook(Book book)
        {
            string strSQL = "INSERT INTO RawBook VALUES(@Id, @Name, @Price, @Supplier, @Author, @Publisher, " +
                            "@ReleaseYear, @Weight, @NumOfPage, @Form, @Image, @Summary)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@Id", book.Id),
                new SqlParameter("@Name", book.Name), new SqlParameter("@Price", book.Price),
                new SqlParameter("@Supplier", book.Supplier), new SqlParameter("@Author", book.Author),
                new SqlParameter("@Publisher", book.Publisher), new SqlParameter("@ReleaseYear", book.ReleaseYear),
                new SqlParameter("@Weight", book.Weight), new SqlParameter("@NumOfPage", book.NumOfPage),
                new SqlParameter("@Form", book.Form), new SqlParameter("@Image", book.Image), 
                new SqlParameter("@Summary", book.Summary));
        }

        public Notification CheckExistedBook(Book book)
        {
            string strSQL = "Select Count(*) From RawBook Where Id = @Id";

            return sql.ExecuteScalar(strSQL, CommandType.Text, new SqlParameter("@Id", book.Id));
        }

        public Notification InsertBookCategory(string bookID, int cateID)
        {
            string strSQL = "Insert into BookCategory Values(@BookID, @CateID)";

            return sql.ExecuteNonQuery(strSQL, CommandType.Text, new SqlParameter("@BookID", bookID),
                new SqlParameter("@CateID", cateID));
        }

        public DataSet GetListFailureIDBookWhenInsertImage(string subcategory)
        {
            string strSQL = "Select RawBook.Id " +
                            "From RawBook inner join BookCategory on RawBook.Id = BookCategory.BookID " +
                            "inner join SubCategory on BookCategory.CateID = SubCategory.ID " +
                            "Where SubCategory.Name = @Name and RawBook.Id Not in " +
                            "(Select RawImageBook.ID_Book From RawImageBook);";

            return sql.ExecuteReader(strSQL, CommandType.Text, new SqlParameter("@Name", subcategory));
        }
    }
}
