using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class UserCartController : Controller
    {
        [HttpGet("Cart")]
        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("BookStore");
            if (session is null)
            {
                HttpContext.Session.SetString("PreviousState", "Cart");
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
