using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;
using BookStore.Models.Statistics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        private AdminBAL adminBal;

        public AdminController()
        {
            adminBal = new AdminBAL();
        }

        [HttpGet("Admin")]
        public async Task<IActionResult> Index()
        {
            var session = HttpContext.Session.GetString("Admin");
            if (session is null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.ListProcessing = (await adminBal.GetListProcessing()).Obj as List<Order>;
                ViewBag.ListDelivery = (await adminBal.GetListDelivery()).Obj as List<Order>;
                ViewBag.ListDelivered = (await adminBal.GetListDelivered()).Obj as List<Order>;
                ViewBag.ListCanceled = (await adminBal.GetListCanceled()).Obj as List<Order>;

                ViewBag.ListSubcategory = (await adminBal.GetListSubCategory()).Obj as List<SubCategory>;
                ViewBag.ListFormBook = (await adminBal.GetListFormBook()).Obj as List<Form>;
                ViewBag.ListSupplier = (await adminBal.GetListSupplier()).Obj as List<Supplier>;
                ViewBag.ListPublisher = (await adminBal.GetListPublisher()).Obj as List<Publisher>;
                return View();
            }
        }

        [HttpPost("ConfirmOrder")]
        public async Task<Response> ConfirmOrder(string id, string status)
        {
            var order = (await adminBal.GetOrder(id)).Obj as Order;
            var newOrder = await adminBal.UpdateStatus(order, status);
            return new Response("Sucess", true, 1, newOrder.Obj as Order);
        }

        [HttpPost("InsertBook")]
        public async Task<Response> Test(Book book, List<Author> authors, List<string> images, int cateID, int formID,
            int supplierID, int publisherID)
        {
            return await adminBal.InsertBook(book, authors, images, cateID, formID, supplierID, publisherID);
        }

        [HttpPost("SearchAdmin")]
        public async Task<Response> SearchBook(string value)
        {
            return await adminBal.SearchBook(value);
        }

        [HttpPost("Remove")]
        public async Task<Response> RemoveBook(string id)
        {
            return await adminBal.RemoveBook(id);
        }

        [HttpGet("Statistics")]
        public async Task<IActionResult> Statistics()
        {
            var session = HttpContext.Session.GetString("Admin");
            if (session is null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.ListBookWithQuantity =
                    (await adminBal.StatisticsBookWithQuantityByMonth()).Obj as List<BookWithQuantity>;
                ViewBag.ListTop3User = (await adminBal.StatisticsTop3Users()).Obj as List<TopUser>;
                return View();
            }
        }
    }
}
