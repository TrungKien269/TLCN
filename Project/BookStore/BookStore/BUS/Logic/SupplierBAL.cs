﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class SupplierBAL
    {
        private BookStoreContext context;

        public SupplierBAL()
        {
            this.context = new BookStoreContext();
        }

        public async Task<Response> GetListSupplier()
        {
            try
            {
                var list = await context.Supplier.ToListAsync();
                return new Response("Success", true, 0, list);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
