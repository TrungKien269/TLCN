using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.BUS.Logic;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class UserCartController : Controller
    {
        private UserCartBAL userCartBal;

        public UserCartController()
        {
            this.userCartBal = new UserCartBAL();
        }

        [HttpGet("Cart")]
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("PreviousState", "Cart");
            var session = HttpContext.Session.GetString("BookStore");
            var response = await userCartBal.GetCart(session);
            if (response.Status is false)
            {
                return View(SessionHelper.GetCartSession(HttpContext.Session));
            }
            else
            {
                return View((response.Obj as Cart).CartBook);
            }
        }

        [HttpPost("RemoveBookCart")]
        public async Task<Response> RemoveBookInCart(string id)
        {
            var session = HttpContext.Session.GetString("BookStore");
            var response = await userCartBal.GetCart(session);
            var originalID = SecureHelper.GetOriginalInput(id);
            if (response.Status is false)
            {
                try
                {
                    var listCartBook = SessionHelper.GetCartSession(this.HttpContext.Session);
                    var book = listCartBook.Where(x => x.BookId.Equals(originalID)).FirstOrDefault();
                    listCartBook.Remove(book);
                    SessionHelper.SetCartSession(this.HttpContext.Session, listCartBook);
                    return await Task.FromResult<Response>(new Response("Success", true, 1, listCartBook));
                }
                catch (Exception e)
                {
                    return Models.Response.CatchError(e.Message);
                }
            }
            else
            {
                var cart = response.Obj as Cart;
                await userCartBal.RemoveFromCart(cart.Id, originalID);
                return await Task.FromResult<Response>(new Response("Success", true, 1, cart.CartBook));
            }
        }

        [HttpPost("EditQuantityCart")]
        public async Task<Response> EditQuantityBookInCart(string id, string quantity)
        {
            var session = HttpContext.Session.GetString("BookStore");
            var response = await userCartBal.GetCart(session);
            var originalID = SecureHelper.GetOriginalInput(id);
            if (response.Status is false)
            {
                try
                {
                    var listCartBook = SessionHelper.GetCartSession(this.HttpContext.Session);
                    var index = listCartBook.FindIndex(x => x.BookId.Equals(originalID));
                    listCartBook[index].Quantity = int.Parse(quantity);
                    listCartBook[index].SubTotal = listCartBook[index].Book.CurrentPrice * int.Parse(quantity);
                    SessionHelper.SetCartSession(this.HttpContext.Session, listCartBook);
                    return await Task.FromResult<Response>(new Response("Success", true, 1, listCartBook[index]));
                }
                catch (Exception e)
                {
                    return Models.Response.CatchError(e.Message);
                }
            }
            else
            {
                var cart = response.Obj as Cart;
                var newCartBook = await userCartBal.EditQuantityInCart(cart.Id, originalID, int.Parse(quantity));
                return await Task.FromResult<Response>(new Response("Success", true, 1, newCartBook.Obj as CartBook));
            }
        }
    }
}
