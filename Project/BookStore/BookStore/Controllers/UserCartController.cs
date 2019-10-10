using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.BUS.Logic;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class UserCartController : Controller
    {
        private UserCartBAL userCartBal;
        private AccountBAL accountBal;

        public UserCartController()
        {
            this.userCartBal = new UserCartBAL();
            this.accountBal = new AccountBAL();
        }

        [HttpGet("Cart")]
        public async Task<IActionResult> Index()
        {
            var session = HttpContext.Session.GetString("BookStore");
            if (session is null)
            {
                HttpContext.Session.SetString("PreviousState", "Cart");
                return RedirectToAction("Index", "Login");
            }
            var userID = ((await accountBal.GetAccountByCookie(session)).Obj as Account).Id;
            return View((await userCartBal.GetCart(userID)).Obj as Cart);
        }
    }
}
