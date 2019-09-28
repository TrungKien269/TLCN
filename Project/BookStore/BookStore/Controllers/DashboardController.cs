using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS;
using BookStore.Models.Objects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class DashboardController : Controller
    {
        private BookStoreContext context;
        private BookBAL bookBal;

        public DashboardController(BookStoreContext context)
        {
            this.context = context;
            bookBal = new BookBAL(this.context);
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await bookBal.GetList());
        }
    }
}
