using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class UserProfileController : Controller
    {
        private UserProfileBAL profileBal;

        public UserProfileController()
        {
            profileBal = new UserProfileBAL();
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> Index()
        {
            var userID = HttpContext.Session.GetInt32("UserID").Value;
            var response = await profileBal.GetUserInfo(userID);
            return View(response.Obj as User);
        }

        [HttpPost("UpdateProfile")]
        public async Task<Response> UpdateUser([FromForm] User user)
        {
            user.Id = HttpContext.Session.GetInt32("UserID").Value;
            return await profileBal.UpdateUser(user);
        }

        [HttpPost("CheckPassword")]
        public async Task<Response> CheckPassword([FromForm] string current_password)
        {
            var session = HttpContext.Session.GetString("BookStore");
            var account = await profileBal.GetAccountInfo(session);
            return await profileBal.CheckCurrentPassword(account.Obj as Account, current_password);
        }

        [HttpPost("ChangePassword")]
        public async Task<Response> ChangePassword(string new_password)
        {
            var session = HttpContext.Session.GetString("BookStore");
            var account = await profileBal.GetAccountInfo(session);
            return await profileBal.ChangePassword(account.Obj as Account, new_password);
        }
    }
}
