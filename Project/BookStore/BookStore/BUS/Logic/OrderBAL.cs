using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class OrderBAL
    {
        private BookStoreContext context;

        public OrderBAL()
        {
            context = new BookStoreContext();
        }

        public async Task<Response> CountOrder()
        {
            try
            {
                int count = await context.Order.CountAsync();
                return new Response("Success", true, 1, count);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> CreateOrder(Order order)
        {
            try
            {
                await context.Order.AddAsync(order);
                var check = await context.SaveChangesAsync();
                return new Response("Success", true, 1, order);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
