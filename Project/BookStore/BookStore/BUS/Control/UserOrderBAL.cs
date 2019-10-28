using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Control
{
    public class UserOrderBAL
    {
        private CartBAL cartBal;
        private AccountBAL accountBal;
        private OrderBAL orderBal;

        public UserOrderBAL()
        {
            this.cartBal = new CartBAL();
            this.accountBal = new AccountBAL();
            this.orderBal = new OrderBAL();
        }

        public async Task<Response> GetCart(string cookie)
        {
            var response_Account = await accountBal.GetAccountByCookie(cookie);
            if (response_Account.Status is true)
            {
                return await cartBal.GetCart((response_Account.Obj as Account).Id);
            }
            else
            {
                return new Response("Cannot recognize an account!", false, 0, null);
            }
        }

        public async Task<Response> ResetCart(int cartID)
        {
            return await cartBal.ResetCart(cartID);
        }

        public async Task<Response> CountOrder()
        {
            return await orderBal.CountOrder();
        }

        public async Task<Response> CreateOrder(Order order)
        {
            return await orderBal.CreateOrder(order);
        }
    }
}
