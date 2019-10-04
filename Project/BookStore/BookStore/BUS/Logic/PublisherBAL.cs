using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class PublisherBAL
    {
        private BookStoreContext context;

        public PublisherBAL()
        {
            this.context = new BookStoreContext();
        }

        public async Task<Response> GetFamousList()
        {
            var task = await context.FamousPublisher.ToListAsync();
            return new Response("Success", true, 1, task);
        }
    }
}
