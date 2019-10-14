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
            var originalID = SecureHelper.GetOriginalInput(id);
            var response = await bookInfoBal.GetBook(originalID);
            return View(response.Obj as Book);
        }

        [HttpPost("AddToCart")]
        public async Task<Response> AddBookToCart(string id)
        {
            var userID = HttpContext.Session.GetInt32("UserID");
            if (userID is null)
            {
                try
                {
                    var originalID = SecureHelper.GetOriginalInput(id);
                    var book = (await bookInfoBal.GetBook(originalID)).Obj as Book;
                    var listCartBook = SessionHelper.GetCartSession(this.HttpContext.Session);
                    listCartBook.Add(new CartBook
                    {
                        BookId = book.Id,
                        Quantity = 1,
                        SubTotal = 1 * book.CurrentPrice,
                        PickedDate = DateTime.Now,
                        Book = book
                    });
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
                return await bookInfoBal.GetCart(int.Parse(userID.ToString()));
            }
        }
    }
}
