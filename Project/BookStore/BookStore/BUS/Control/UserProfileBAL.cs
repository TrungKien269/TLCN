using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Control
{
    public class UserProfileBAL
    {
        private UserBAL userBal;
        private AccountBAL accountBal;

        public UserProfileBAL()
        {
            userBal = new UserBAL();
            accountBal = new AccountBAL();
        }

        public async Task<Response> GetUserInfo(int id)
        {
            return await userBal.GetUserOnly(id);
        }

        public async Task<Response> UpdateUser(User user)
        {
            return await userBal.Update(user);
        }

        public async Task<Response> GetAccountInfo(string cookie)
        {
            return await accountBal.GetAccountByCookie(cookie);
        }

        public async Task<Response> CheckCurrentPassword(Account account, string password)
        {
            return await accountBal.CheckCurrentPassword(account, password);
        }

        public async Task<Response> ChangePassword(Account account, string new_password)
        {
            return await accountBal.ChangePassword(account, new_password);
        }
    }
}
