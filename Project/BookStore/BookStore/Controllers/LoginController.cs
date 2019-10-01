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
    public class LoginController : Controller
    {
        private LoginBAL loginBal;

        public LoginController()
        {
            this.loginBal = new LoginBAL();
        }

        [HttpGet("Login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<Response> Login(string username, string password)
        {
            var respone = await loginBal.Login(username, password);
            if (respone.Status == true)
            {
                //SessionHelper.SetObjectAsJson(HttpContext.Session, "BookStore", username);
                //Response.Cookies.Append("BookStore", username, await CookieHelper.SetCookie());
                var hash = await Task.FromResult<string>(
                    CryptographyHelper.GenerateHash(username + DateTime.Now.ToString(), (respone.Obj as Account).Salt));
                //SessionHelper.SetObjectAsJson(HttpContext.Session, "BookStore", hash);
                HttpContext.Session.SetString("BookStore", hash);
                Response.Cookies.Append("BookStore", hash, await CookieHelper.SetCookie());
                await loginBal.SetCookieForAccount(hash, respone.Obj as Account);
                ViewBag.Session = HttpContext.Session.GetString("BookStore");
                ViewBag.FullName = (respone.Obj as Account).IdNavigation.FullName;
                return respone;
            }
            return respone;
        }
    }
}
