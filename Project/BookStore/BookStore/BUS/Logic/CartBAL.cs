using System;
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
                var cart = await context.Cart.Include(x => x.CartBook).Include(x => x.IdNavigation)
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
    }
}
