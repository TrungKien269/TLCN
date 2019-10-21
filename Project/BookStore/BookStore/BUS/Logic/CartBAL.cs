﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class CartBAL
    {
        private BookStoreContext context;

        public CartBAL()
        {
            context = new BookStoreContext();
        }

        public async Task<Response> GetCart(int userID)
        {
            try
            {
                var cart = await context.Cart.Include(x => x.IdNavigation).Include(x => x.CartBook)
                    .ThenInclude(x => x.Book)
                    .Where(x => x.IdNavigation.Id.Equals(userID)).FirstOrDefaultAsync();
                if (cart is null)
                {
                    return new Response("Can not find a cart for this account!", false, 0, null);
                }
                return new Response("Success", true, 1, cart);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> CreateCart(int userID)
        {
            try
            {
                var cart = new Cart
                {
                    Id = userID,
                    CreatedDate = DateTime.Now
                };
                await context.Cart.AddAsync(cart);
                var check = await context.SaveChangesAsync();
                if (check is 1)
                {
                    return new Response("Success", true, 1, cart);
                }
                else
                {
                    return new Response("Can not create a cart for this account!", false, 0, null);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> InsertToCart(Cart cart, Book book)
        {
            try
            {
                var cartBook = new CartBook
                {
                    BookId = book.Id,
                    CartId = cart.Id,
                    PickedDate = DateTime.Now,
                    Quantity = 1,
                    SubTotal = book.CurrentPrice
                };
                context.CartBook.Add(cartBook);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, cartBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> RemoveFromCart(int cartID, string bookID)
        {
            try
            {
                var cartBook = await context.CartBook.Where(x => x.BookId.Equals(bookID) && x.CartId.Equals(cartID))
                    .FirstOrDefaultAsync();
                context.CartBook.Remove(cartBook);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, cartBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
