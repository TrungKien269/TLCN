using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Control
{
    public class AdminBAL
    {
        private AccountBAL accountBal;
        private OrderBAL orderBal;
        private BookBAL bookBal;
        private AuthorBAL authorBal;
        private CRUDBookBAL crudBookBal;

        public AdminBAL()
        {
            accountBal = new AccountBAL();
            orderBal = new OrderBAL();
            bookBal = new BookBAL();
            authorBal = new AuthorBAL();
            crudBookBal = new CRUDBookBAL();
        }

        public async Task<Response> GetListProcessing()
        {
            return await orderBal.GetListProcessingOrders();
        }

        public async Task<Response> GetListDelivery()
        {
            return await orderBal.GetListDeliveryOrders();
        }

        public async Task<Response> GetListDelivered()
        {
            return await orderBal.GetListDeliveredOrders();
        }

        public async Task<Response> GetListCanceled()
        {
            return await orderBal.GetListCanceledOrders();
        }

        public async Task<Response> GetOrder(string secureID)
        {
            return await orderBal.GetOrder(SecureHelper.GetOriginalInput(secureID));
        }

        public async Task<Response> UpdateStatus(Order order, string status)
        {
            return await orderBal.UpdateStatusOrder(order, status);
        }

        public async Task<Response> InsertBook(Book book)
        {
            return await bookBal.Insert(book);
        }

        public async Task<Response> InsertAuthor(Author author)
        {
            return await authorBal.InsertAuthor(author);
        }

        public async Task<Response> TestInsertBook(Book book, List<Author> authors, List<string> images, int cateID,
            int formID, int supplierID, int publisherID)
        {
            return await crudBookBal.CreateBookProcess(book, authors, images, cateID, formID, supplierID, publisherID);
        }
    }
}
