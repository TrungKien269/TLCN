using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;
using BookStore.Models.OnlinePayment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class ProceedOrder : Controller
    {
        private ProceedOrderBAL userOrderBal;

        public ProceedOrder()
        {
            this.userOrderBal = new ProceedOrderBAL();
        }

        [HttpGet("ProceedOrder")]
        public async Task<IActionResult> Index()
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            HttpContext.Session.SetString("PreviousState", "ProceedOrder");
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
        public async Task<Response> SaveOrder([FromForm] Order order)
        {
            var session = HttpContext.Session.GetString("BookStore");
            var response = await userOrderBal.GetCart(session);
            if (response.Status is false)
            {
                List<CartBook> listCart = SessionHelper.GetCartSession(HttpContext.Session);
                List<Order> orderList = SessionHelper.GetOrdersSession(HttpContext.Session);
                var task_Count = await userOrderBal.CountOrder();
                var orderID = "Order" + ((task_Count.Obj as int?) + 1);
                order.Id = orderID;
                order.UserId = 0;
                order.Total = listCart.Sum(x => x.SubTotal);
                order.CreatedDate = DateTime.Now;
                order.Status = "Processing";

                var order_session = new Order()
                {
                    Id = orderID,
                    FullName = order.FullName,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    CreatedDate = order.CreatedDate,
                    Status = "Processing",
                    Total = order.Total,
                    UserId = 0
                };

                foreach (var cartbook in listCart)
                {
                    cartbook.Book.Id = SecureHelper.GetOriginalInput(cartbook.Book.Id);
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = orderID,
                        BookId = cartbook.BookId,
                        Quantity = cartbook.Quantity,
                    };
                    order.OrderDetail.Add(orderDetail);

                    var orderDetail_session = new OrderDetail
                    {
                        OrderId = orderID,
                        BookId = cartbook.BookId,
                        Quantity = cartbook.Quantity,
                        Book = cartbook.Book
                    };
                    order_session.OrderDetail.Add(orderDetail_session);
                }
                orderList.Add(order_session);
                SessionHelper.SetOrdersSession(HttpContext.Session, orderList);
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

                foreach (var cartbook in listCart)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = orderID,
                        BookId = cartbook.BookId,
                        Quantity = cartbook.Quantity
                    };
                    order.OrderDetail.Add(orderDetail);
                }
                await userOrderBal.ResetCart(HttpContext.Session.GetInt32("UserID").Value);
                return await userOrderBal.CreateOrder(order);
            }
        }

        [HttpPost("OnlineProceed")]
        public string NganLuong(string fullname, string mobile, string email, string total)
        {
            string payment_method = "VISA";
            string str_bankcode = "1234567890";


            RequestInfo info = new RequestInfo();
            info.Merchant_id = "36680";
            info.Merchant_password = "matkhauketnoi";
            info.Receiver_email = "demo@nganluong.vn";



            info.cur_code = "vnd";
            info.bank_code = str_bankcode;

            info.Order_code = "ma_don_hang01";
            //info.Total_amount = "10000";
            info.Total_amount = total;
            info.fee_shipping = "0";
            info.Discount_amount = "0";
            info.order_description = "Thanh toan tes thu dong hang";
            info.return_url = "http://localhost";
            info.cancel_url = "http://localhost";

            //info.Buyer_fullname = buyer_fullname.Value;
            //info.Buyer_email = buyer_email.Value;
            //info.Buyer_mobile = buyer_mobile.Value;

            info.Buyer_fullname = fullname;
            info.Buyer_email = email;
            info.Buyer_mobile = mobile;

            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseInfo result = objNLChecout.GetUrlCheckout(info, payment_method);

            if (result.Error_code == "00")
            {
                //return Redirect(result.Checkout_url);
                return result.Checkout_url;
            }
            else
            {
                //return Redirect(info.cancel_url);
                return info.cancel_url;
            }
        }
    }
}
