using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class UserOrderController : Controller
    {
        private UserOrderBAL userOrderBal;

        public UserOrderController()
        {
            userOrderBal = new UserOrderBAL();
        }

        [HttpGet("Orders")]
        public async Task<IActionResult> Index()
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            if (ViewBag.FullName is null)
            {
                return RedirectToAction("Anonymous");
            }
            HttpContext.Session.SetString("PreviousState", "Orders");
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewBag.ListDelivery = (await userOrderBal.GetListDelivery(userID)).Obj as List<Order>;
            ViewBag.ListDelivered = (await userOrderBal.GetListDelivered(userID)).Obj as List<Order>;
            ViewBag.ListCanceled = (await userOrderBal.GetListCanceled(userID)).Obj as List<Order>;
            return View((await userOrderBal.GetListProcessing(userID)).Obj as List<Order>);
        }
        
        [HttpGet("AnonymousOrder")]
        public async Task<IActionResult> Anonymous()
        {
            HttpContext.Session.SetString("PreviousState", "AnonymousOrder");
            var ListOrders = await Task.FromResult<List<Order>>(SessionHelper.GetOrdersSession(HttpContext.Session));
            return View(ListOrders);
        }

        [HttpPost("CancelOrder")]
        public async Task<Response> CancelOrder(string id)
        {
            var order = (await userOrderBal.GetOrder(id)).Obj as Order;
            var newOrder = await userOrderBal.UpdateStatus(order, "Canceled");
            return new Response("Sucess", true, 1, newOrder.Obj as Order);
        }
    }
}
