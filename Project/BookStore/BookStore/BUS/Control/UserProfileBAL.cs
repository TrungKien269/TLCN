using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Control
{
    public class UserProfileBAL
    {
        private UserBAL userBal;

        public UserProfileBAL()
        {
            userBal = new UserBAL();
        }

        public async Task<Response> GetUserInfo(int id)
        {
            return await userBal.GetUserOnly(id);
        }

        public async Task<Response> UpdateUser(User user)
        {
            return await userBal.Update(user);
        }
    }
}
