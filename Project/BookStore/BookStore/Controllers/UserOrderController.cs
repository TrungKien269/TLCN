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
            this.userOrderBal = new UserOrderBAL();
        }

        [HttpGet("Order")]
        public async Task<IActionResult> Index()
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            HttpContext.Session.SetString("PreviousState", "Order");
            var session = HttpContext.Session.GetString("BookStore");
            var response = await userOrderBal.GetCart(session);
            if (response.Status is false)
            {
                ViewBag.ListCartBook = SessionHelper.GetCartSession(HttpContext.Session);
                return View(new Order());
            }
            else
            {
                ViewBag.ListCartBook = (response.Obj as Cart).CartBook;
                return View(new Order
                {
                    FullName = (response.Obj as Cart).IdNavigation.FullName,
                    PhoneNumber = (response.Obj as Cart).IdNavigation.PhoneNumber,
                    Address = (response.Obj as Cart).IdNavigation.Address
                });
            }
        }

        [HttpPost("ProceedOrder")]
        public async Task<Response> ProceedOrder([FromForm] Order order)
        {
            var session = HttpContext.Session.GetString("BookStore");
            var response = await userOrderBal.GetCart(session);
            if (response.Status is false)
            {
                List<CartBook> listCart = SessionHelper.GetCartSession(HttpContext.Session);
                var task_Count = await userOrderBal.CountOrder();
                var orderID = "Order" + ((task_Count.Obj as int?) + 1);
                order.Id = orderID;
                order.UserId = 0;
                order.Total = listCart.Sum(x => x.SubTotal);
                order.CreatedDate = DateTime.Now;
                order.Status = "Processing";

                foreach (var book in listCart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = orderID,
                        BookId = book.BookId,
                        Quantity = book.Quantity
                    };
                    order.OrderDetail.Add(orderDetail);
                }
                SessionHelper.ResetCartSession(this.HttpContext.Session, listCart);

                return await userOrderBal.CreateOrder(order);
            }
            else
            {
                List<CartBook> listCart = (response.Obj as Cart).CartBook;
                var task_Count = await userOrderBal.CountOrder();
                var orderID = "Order" + ((task_Count.Obj as int?) + 1);
                order.Id = orderID;
                order.UserId = HttpContext.Session.GetInt32("UserID").Value;
                order.Total = listCart.Sum(x => x.SubTotal);
                order.CreatedDate = DateTime.Now;
                order.Status = "Processing";

                foreach (var book in listCart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = orderID,
                        BookId = book.BookId,
                        Quantity = book.Quantity
                    };
                    order.OrderDetail.Add(orderDetail);
                }
                await userOrderBal.ResetCart(HttpContext.Session.GetInt32("UserID").Value);

                return await userOrderBal.CreateOrder(order);
            }
        }
    }
}
