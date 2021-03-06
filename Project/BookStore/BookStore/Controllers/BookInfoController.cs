﻿using System;
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
    public class BookInfoController : Controller
    {
        private BookInfoBAL bookInfoBal;

        public BookInfoController()
        {
            this.bookInfoBal = new BookInfoBAL();
        }

        [HttpGet("Book/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.FullName = HttpContext.Session.GetString("UserFullName");
            ViewBag.ListCategory = (await bookInfoBal.GetListCategory()).Obj as List<Category>;
            var originalID = SecureHelper.GetOriginalInput(id);
            var response = await bookInfoBal.GetBook(originalID);
            ViewBag.RelatedList = (await bookInfoBal.GetListRelatedBooks(originalID)).Obj as List<Book>;
            HttpContext.Session.SetString("PreviousState", "Book/" + id);
            return View(response.Obj as Book);
        }

        [HttpPost("Book")]
        public async Task<Response> GetBook(string id)
        {
            var originalID = SecureHelper.GetOriginalInput(id);
            var response = await bookInfoBal.GetBookWithSecureID(originalID);
            return response;
        }

        [HttpPost("AddToCart")]
        public async Task<Response> AddBookToCart(string id, string quantity)
        {
            var userID = HttpContext.Session.GetInt32("UserID");
            if (userID is null)
            {
                try
                {
                    var originalID = SecureHelper.GetOriginalInput(id);
                    var book = (await bookInfoBal.GetBookWithoutDetail(originalID)).Obj as Book;
                    var listCartBook = SessionHelper.GetCartSession(this.HttpContext.Session);
                    var index = listCartBook.FindIndex(x => x.BookId.Equals(originalID));
                    if (index >= 0)
                    {
                        listCartBook[index].Quantity += int.Parse(quantity);
                        listCartBook[index].SubTotal += book.CurrentPrice * int.Parse(quantity);
                    }
                    else
                    {
                        listCartBook.Add(new CartBook
                        {
                            BookId = originalID,
                            Quantity = int.Parse(quantity),
                            SubTotal = int.Parse(quantity) * book.CurrentPrice,
                            PickedDate = DateTime.Now,
                            Book = book
                        });
                    }
                    SessionHelper.SetCartSession(this.HttpContext.Session, listCartBook);
                    return await Task.FromResult<Response>(new Response("Success", true, 1, listCartBook));
                }
                catch (Exception e)
                {
                    return Models.Response.CatchError(e.Message);
                }

            }
            else
            {
                var cart = (await bookInfoBal.GetCart(int.Parse(userID.ToString()))).Obj as Cart;
                if (cart is null)
                {
                    var newCart_Response = await bookInfoBal.CreateCart(int.Parse(userID.ToString()));
                    Cart newCart = newCart_Response.Obj as Cart;
                    var originalID = SecureHelper.GetOriginalInput(id);
                    var book = (await bookInfoBal.GetBook(originalID)).Obj as Book;
                    await bookInfoBal.AddToCart(newCart, book, int.Parse(quantity));
                    return new Response("Success", true, 1, newCart.CartBook);
                }
                else
                {
                    var originalID = SecureHelper.GetOriginalInput(id);
                    var book = (await bookInfoBal.GetBook(originalID)).Obj as Book;
                    await bookInfoBal.AddToCart(cart, book, int.Parse(quantity));
                    return new Response("Success", true, 1, cart.CartBook);
                }
            }
        }
    }
}
