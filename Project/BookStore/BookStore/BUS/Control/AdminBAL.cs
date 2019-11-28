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

        public AdminBAL()
        {
            accountBal = new AccountBAL();
            orderBal = new OrderBAL();
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
    }
}
