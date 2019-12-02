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
            ViewBag.LoadNumber = 1;
            ViewBag.ListCategory = (await listBookBal.GetListCategory()).Obj as List<Category>;
            ViewBag.ListPubliser = (await listBookBal.GetListPublisher()).Obj as List<FamousPublisher>;
            return View((await listBookBal.GetListBookByCategory("Fiction", 0)).Obj as List<Book>);
        }

        [HttpGet("ListBook/{category}")]
        public async Task<IActionResult> Page(string category)
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            ViewBag.LoadNumber = 1;
            ViewBag.ListCategory = (await listBookBal.GetListCategory()).Obj as List<Category>;
            ViewBag.ListPubliser = (await listBookBal.GetListPublisher()).Obj as List<FamousPublisher>;
            return View("Index", (await listBookBal.GetListBookByCategory(category, 0)).Obj as List<Book>);
        }

        [HttpPost("GetMoreBook")]
        public async Task<Response> GetMoreBook(string category, int skipNumber)
        {
            return await listBookBal.GetListBookByCategory(category, skipNumber);
        }

        [HttpGet("ListBook/Subcategory/{subcategory}")]
        public async Task<IActionResult> Subcategory(string subcategory)
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            ViewBag.LoadNumber = 1;
            ViewBag.ListCategory = (await listBookBal.GetListCategory()).Obj as List<Category>;
            ViewBag.ListPubliser = (await listBookBal.GetListPublisher()).Obj as List<FamousPublisher>;
            return View((await listBookBal.GetListBookBySubCategory(subcategory, 0)).Obj as List<Book>);
        }

        [HttpPost("GetMoreBook/Subcategory")]
        public async Task<Response> GetMoreBookSubcategory(string subcategory, int skipNumber)
        {
            return await listBookBal.GetListBookBySubCategory(subcategory, skipNumber);
        }

        [HttpGet("Search/value={value}")]
        public async Task<IActionResult> Search(string value)
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            ViewBag.LoadNumber = 1;
            ViewBag.ListCategory = (await listBookBal.GetListCategory()).Obj as List<Category>;
            ViewBag.ListPubliser = (await listBookBal.GetListPublisher()).Obj as List<FamousPublisher>;
            ViewBag.searchStr = value;
            var books = await listBookBal.SearchBook(value, 0);
            return View(books.Obj as List<Book>);
        }

        [HttpPost("GetMoreBook/Search")]
        public async Task<Response> GetMoreBookSearch(string value, int skipNumber)
        {
            return await listBookBal.SearchBook(value, skipNumber);
        }

        [HttpPost("Filter")]
        public async Task<Response> FilterBook(List<int> cateIDs, int skipNumber)
        {
            return await listBookBal.FilterBook(cateIDs, skipNumber);
        }

        [HttpPost("GetMoreBook/Filter")]
        public async Task<Response> GetMoreBookFilter(List<int> cateIDs, int skipNumber)
        {
            return await listBookBal.FilterBook(cateIDs, skipNumber);
        }
    }
}
