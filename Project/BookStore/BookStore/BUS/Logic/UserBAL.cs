using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class UserBAL
    {
        private BookStoreContext context;

        public UserBAL()
        {
            this.context = new BookStoreContext();
        }

        public async Task<Response> GetUser(int id)
        {
            if (id.Equals(-1))
            {
                return await Task.FromResult<Response>(new Response("Cannot find an account!", false, 0, null));
            }
            else
            {
                var user = await context.User.Include(x => x.Account).Where(x => x.Id.Equals(id))
                    .FirstOrDefaultAsync();
                return new Response("Success", true, 1, user);
            }
        }
    }
}
