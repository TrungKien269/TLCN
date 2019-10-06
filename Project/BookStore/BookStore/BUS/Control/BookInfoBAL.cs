using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;

namespace BookStore.BUS.Control
{
    public class BookInfoBAL
    {
        private BookBAL bookBal;

        public BookInfoBAL()
        {
            this.bookBal = new BookBAL();
        }

        public async Task<Response> GetBook(string id)
        {
            return await bookBal.GetObject(id);
        }
    }
}
