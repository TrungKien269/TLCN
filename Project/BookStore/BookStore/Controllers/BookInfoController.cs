using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Control;
using BookStore.Models.Objects;
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
            var response = await bookInfoBal.GetBook(id);
            return View(response.Obj as Book);
        }
    }
}
