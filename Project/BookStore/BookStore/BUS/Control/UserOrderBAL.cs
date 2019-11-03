using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.BUS.Logic;
using BookStore.Models;

namespace BookStore.BUS.Control
{
    public class UserOrderBAL
    {
        private AccountBAL accountBal;
        private OrderBAL orderBal;

        public UserOrderBAL()
        {
            accountBal = new AccountBAL();
            orderBal = new OrderBAL();
        }

        public async Task<Response> GetListProcessing(int userID)
        {
            return await orderBal.GetListUserProcessingOrders(userID);
        }

        public async Task<Response> GetListDelivery(int userID)
        {
            return await orderBal.GetListUserDeliveryOrders(userID);
        }

        public async Task<Response> GetListDelivered(int userID)
        {
            return await orderBal.GetListUserDeliveredOrders(userID);
        }
    }
}
