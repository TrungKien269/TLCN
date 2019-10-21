using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Control
{
    public class BookInfoBAL
    {
        private BookBAL bookBal;
        private CartBAL cartBal;

        public BookInfoBAL()
        {
            this.bookBal = new BookBAL();
            this.cartBal = new CartBAL();
        }

        public async Task<Response> GetBook(string id)
        {
            return await bookBal.GetBookOnly(id);
        }

        public async Task<Response> GetCart(int userID)
        {
            return await cartBal.GetCart(userID);
        }

        public async Task<Response> CreateCart(int userID)
        {
            return await cartBal.CreateCart(userID);
        }

        public async Task<Response> AddToCart(Cart cart, Book book)
        {
            return await cartBal.InsertToCart(cart, book);
        }
    }
}
