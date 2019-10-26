using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class ListBookController : Controller
    {
        private ListBookBAL listBookBal;

        public ListBookController()
        {
            this.listBookBal = new ListBookBAL();
        }

        [HttpGet("ListBook")]
        public async Task<IActionResult> Index()
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            ViewBag.ListCategory = (await listBookBal.GetListCategory()).Obj as List<Category>;
            return View();
        }
    }
}
