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
        private CategoryBAL categoryBal;

        public BookInfoBAL()
        {
            this.bookBal = new BookBAL();
            this.cartBal = new CartBAL();
            this.categoryBal = new CategoryBAL();
        }

        public async Task<Response> GetListCategory()
        {
            return await categoryBal.GetList();
        }

        public async Task<Response> GetBook(string id)
        {
            return await bookBal.GetObject(id);
        }

        public async Task<Response> GetBookWithSecureID(string id)
        {
            return await bookBal.GetBookWithSecureID(id);
        }

        public async Task<Response> GetBookWithoutDetail(string id)
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
