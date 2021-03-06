﻿using System;
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
        [ValidateAntiForgeryToken]
        public async Task<Response> Login([FromForm] Account account)
        {
            var response = await loginBal.Login(account.Username, account.Password);
            response.previousState = HttpContext.Session.GetString("PreviousState");
            if (response.Status == true)
            {
                if ((response.Obj as Account).Username.Equals("admin"))
                {
                    var hash = await Task.FromResult<string>(
                        CryptographyHelper.GenerateHash(account.Username + DateTime.Now.ToString(),
                            (response.Obj as Account).Salt));
                    SessionHelper.SetAdminSession(this.HttpContext.Session, hash);
                    return response;
                }
                else
                {
                    var hash = await Task.FromResult<string>(
                        CryptographyHelper.GenerateHash(account.Username + DateTime.Now.ToString(),
                            (response.Obj as Account).Salt));
                    SessionHelper.SetWebsiteSession(this.HttpContext.Session, hash);
                    SessionHelper.SetUserSession(this.HttpContext.Session, (response.Obj as Account).Id,
                        (response.Obj as Account).IdNavigation.FullName);
                    CookieHelper.SetWebsiteCookie(this.Response, hash);

                    await loginBal.SetCartAfterLogin(this.HttpContext.Session, (response.Obj as Account).Id);
                    await loginBal.SetCookieForAccount(hash, response.Obj as Account);
                    ViewBag.Session = HttpContext.Session.GetString("BookStore");
                    ViewBag.FullName = response.Obj as Account is null
                        ? null
                        : (response.Obj as Account).IdNavigation.FullName;
                    return response;
                }
            }
            return response;
        }

        [HttpPost("Signup")]
        [ValidateAntiForgeryToken]
        public async Task<Response> Signup([FromForm] User user)
        {
            var response = await loginBal.Signup(user);
            if (response.Status is true)
            {
                response.previousState = HttpContext.Session.GetString("PreviousState");
                var hash = await Task.FromResult<string>(
                    CryptographyHelper.GenerateHash(user.Account.Username + DateTime.Now.ToString(),
                        (response.Obj as User).Account.Salt));
                SessionHelper.SetWebsiteSession(this.HttpContext.Session, hash);
                SessionHelper.SetUserSession(this.HttpContext.Session, (response.Obj as User).Id,
                    (response.Obj as User).FullName);
                CookieHelper.SetWebsiteCookie(this.Response, hash);
                await loginBal.SetCookieForAccount(hash, (response.Obj as User).Account);
                ViewBag.Session = HttpContext.Session.GetString("BookStore");
                ViewBag.FullName = response.Obj as User is null
                    ? null
                    : (response.Obj as User).FullName;
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
