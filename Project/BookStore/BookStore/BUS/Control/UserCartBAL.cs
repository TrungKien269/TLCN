﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Control
{
    public class UserCartBAL
    {
        private CartBAL cartBal;
        private AccountBAL accountBal;

        public UserCartBAL()
        {
            this.cartBal = new CartBAL();
            this.accountBal = new AccountBAL();
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

        public async Task<Response> RemoveFromCart(int cartID, string bookID)
        {
            return await cartBal.RemoveFromCart(cartID, bookID);
        }

        public async Task<Response> EditQuantityInCart(int cartID, string bookID, int quantity)
        {
            return await cartBal.UpdateQuantity(cartID, bookID, quantity);
        }
    }
}
