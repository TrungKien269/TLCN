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
        private BookBAL bookBal;

        public ListBookBAL()
        {
            categoryBal = new CategoryBAL();
            publisherBal = new PublisherBAL();
            bookBal = new BookBAL();
        }

        public async Task<Response> GetListCategory()
        {
            return await categoryBal.GetList();
        }

        public async Task<Response> GetListPublisher()
        {
            return await publisherBal.GetFamousList();
        }

        public async Task<Response> GetListBookByCategory(string category, int skipNumber)
        {
            return await bookBal.GetBookByCategory(category, skipNumber);
        }

        public async Task<Response> GetListBookBySubCategory(string subcategory, int skipNumber)
        {
            return await bookBal.GetBookBySubCategory(subcategory, skipNumber);
        }
    }
}
