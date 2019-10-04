using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;

namespace BookStore.BUS.Control
{
    public class LoginBAL: IHttpContextAccessor
    {
        private BookStoreContext context;
        private AccountBAL accountBal;
        public HttpContext HttpContext { get; set; }

        public LoginBAL()
        {
            this.context = new BookStoreContext();
            this.accountBal = new AccountBAL();
        }

        public async Task<Response> Login(string username, string password)
        {
            return await accountBal.Login(username, password);
        }

        public async Task<Response> SetCookieForAccount(string cookie, Account account)
        {
            return await accountBal.SetCookie(cookie, account);
        }
    }
}
