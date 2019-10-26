using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;

namespace BookStore.BUS.Control
{
    public class ListBookBAL
    {
        private CategoryBAL categoryBal;

        public ListBookBAL()
        {
            categoryBal = new CategoryBAL();
        }

        public async Task<Response> GetListCategory()
        {
            return await categoryBal.GetList();
        }
    }
}
