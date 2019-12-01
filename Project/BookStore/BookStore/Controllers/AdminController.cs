using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.Models;
using BookStore.Models.Objects;
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
                ViewBag.ListProcessing = (await adminBal.GetListProcessing()).Obj as List<Order>;
                ViewBag.ListDelivery = (await adminBal.GetListDelivery()).Obj as List<Order>;
                ViewBag.ListDelivered = (await adminBal.GetListDelivered()).Obj as List<Order>;
                ViewBag.ListCanceled = (await adminBal.GetListCanceled()).Obj as List<Order>;
                return View();
                //return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.ListProcessing = (await adminBal.GetListProcessing()).Obj as List<Order>;
                ViewBag.ListDelivery = (await adminBal.GetListDelivery()).Obj as List<Order>;
                ViewBag.ListDelivered = (await adminBal.GetListDelivered()).Obj as List<Order>;
                ViewBag.ListCanceled = (await adminBal.GetListCanceled()).Obj as List<Order>;
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
        public async Task<Response> InsertBook(Book book)
        {
            return await adminBal.InsertBook(book);
        }

        [HttpPost("InsertAuthor")]
        public async Task<Response> InsertAuthor(Author author)
        {
            return await adminBal.InsertAuthor(author);
        }

        [HttpPost("Test")]
        public async Task<Response> Test(Book book, List<Author> authors, List<string> images, int cateID, int formID,
            int supplierID, int publisherID)
        {
            return await adminBal.TestInsertBook(book, authors, images, cateID, formID, supplierID, publisherID);
        }
    }
}
