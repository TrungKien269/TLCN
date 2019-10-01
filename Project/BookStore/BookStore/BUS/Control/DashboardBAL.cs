using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Control
{
    public class DashboardBAL
    {
        private BookStoreContext context;
        private BookBAL bookBal;
        private CategoryBAL categoryBal;
        private AccountBAL accountBal;

        public DashboardBAL()
        {
            this.context = new BookStoreContext();
            bookBal = new BookBAL(this.context);
            this.categoryBal = new CategoryBAL(this.context);
            this.accountBal = new AccountBAL(this.context);
        }

        public async Task<Response> GetListCategory()
        {
            return await categoryBal.GetList();
        }

        public async Task<Response> GetListSubCategory(string category)
        {
            return await categoryBal.GetSubList(category);
        }

        public async Task<Response> GetAccountByCookie(string cookie)
        {
            return await accountBal.GetAccountByCookie(cookie);
        }

        public async Task<Response> Logout(string cookie)
        {
            var account = await accountBal.GetAccountByCookie(cookie);
            if (account is null)
            {
                return await Task.FromResult(new Response("Success", true, 0, null));
            }
            return await accountBal.SetCookie(null, account.Obj as Account);
        }
    }
}
