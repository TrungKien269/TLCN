using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS;
using BookStore.BUS.Control;
using BookStore.BUS.Logic;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Controllers
{
    public class DashboardController : Controller
    {
        private DashboardBAL dashboardBal;

        public DashboardController()
        {
            dashboardBal = new DashboardBAL();
        }
        
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            var response = await dashboardBal.GetListCategory();
            string session = HttpContext.Session.GetString("BookStore");
            if (session is null)
            {
                string cookie =
                    await Task.FromResult<string>(Request.Cookies.Where(x => x.Key.Equals("BookStore")).FirstOrDefault()
                        .Value);
                if (cookie != null)
                {
                    var account = await dashboardBal.GetAccountByCookie(cookie);
                    if (account.Status is true)
                    {
                        HttpContext.Session.SetString("BookStore", cookie);
                        ViewBag.Session = HttpContext.Session.GetString("BookStore");
                        ViewBag.FullName = (account.Obj as Account).IdNavigation.FullName;
                    }
                }
            }
            else
            {
                ViewBag.Session = session;
                var account = await dashboardBal.GetAccountByCookie(session);
                ViewBag.FullName = (account.Obj as Account).IdNavigation.FullName;
            }
            ViewBag.ListSalesBook = (await dashboardBal.GetListSalesBook()).Obj as List<Book>;
            ViewBag.ListFamousPublisher = (await dashboardBal.GetListFamousPublisher()).Obj as List<FamousPublisher>;

            return View(response.Obj as List<Category>);
        }

        [HttpGet("SubLists/{name}")]
        public async Task<List<SubCategory>> GetSubLists([FromRoute] string name)
        {
            var response = await dashboardBal.GetListSubCategory(name);
            return response.Obj as List<SubCategory>;
        }

        [HttpPost("FamousPublisherBookList")]
        public async Task<Response> GetBookFromFamousPublisher(string id)
        {
            var response = await dashboardBal.GetListBookFromFamousPublisher(id);
            return response;
        }

        [HttpGet("Logout")]
        public async Task<Response> Logout()
        {
            string cookie =
                await Task.FromResult<string>(Request.Cookies.Where(x => x.Key.Equals("BookStore")).FirstOrDefault()
                    .Value);
            if (cookie is null)
            {
                var session = HttpContext.Session.GetString("BookStore");
                HttpContext.Session.Remove("BookStore");
                return await dashboardBal.Logout(session);
            }
            else
            {
                Response.Cookies.Delete("BookStore");
                HttpContext.Session.Remove("BookStore");
                return await dashboardBal.Logout(cookie);
            }
        }
    }
}
