using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class AccountBAL
    {
        private BookStoreContext context;

        public AccountBAL()
        {
            this.context = new BookStoreContext();
        }

        public async Task<Response> Login(string username, string password)
        {
            try
            {
                var account = await context.Account.Include(x => x.IdNavigation).Where(x => x.Username.Equals(username))
                    .FirstOrDefaultAsync();
                if (account != null)
                {
                    var check = await Task.FromResult<bool>(CryptographyHelper.AreEqual(password, account.Password,
                        account.Salt));
                    if (check)
                    {
                        return new Response("Success", true, 1, account);
                    }
                    else
                    {
                        return new Response("Wrong password", false, 0, null);
                    }
                }
                else
                {
                    return new Response("Wrong username", false, 0, null);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> SetCookie(string cookie, Account account)
        {
            try
            {
                account.Cookie = cookie;
                context.Account.Update(account);
                var check = await context.SaveChangesAsync();
                if (check == 1)
                {
                    return new Response("Success", true, 1, 1);
                }
                else
                {
                    return new Response("Fail", false, 0, 0);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> GetAccountByCookie(string cookie)
        {
            try
            {
                var account = await context.Account.Include(x => x.IdNavigation).Where(x => x.Cookie.Equals(cookie))
                    .FirstOrDefaultAsync();
                if (account is null)
                {
                    return new Response("Not found", false, 0, null);
                }
                else
                {
                    return new Response("Success", true, 1, account);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
