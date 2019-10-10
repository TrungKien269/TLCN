using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;

namespace BookStore.BUS.Control
{
    public class UserCartBAL
    {
        private CartBAL cartBal;

        public UserCartBAL()
        {
            this.cartBal = new CartBAL();
        }

        public async Task<Response> GetCart(int userID)
        {
            return await cartBal.GetCart(userID);
        }
    }
}
