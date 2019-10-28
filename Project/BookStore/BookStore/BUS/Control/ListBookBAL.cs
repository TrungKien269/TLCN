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
        private PublisherBAL publisherBal;

        public ListBookBAL()
        {
            categoryBal = new CategoryBAL();
            publisherBal = new PublisherBAL();
        }

        public async Task<Response> GetListCategory()
        {
            return await categoryBal.GetList();
        }

        public async Task<Response> GetListPublisher()
        {
            return await publisherBal.GetFamousList();
        }
    }
}
