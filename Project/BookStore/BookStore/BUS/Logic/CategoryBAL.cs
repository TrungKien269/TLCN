using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class CategoryBAL
    {
        private BookStoreContext context;

        public CategoryBAL(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<Response> GetList()
        {
            var list = await context.Category.Include(x => x.SubCategory).ToListAsync();
            return new Response("Success", true, 0, list);
        }

        public async Task<Response> GetSubList(string category)
        {
            var list = await context.SubCategory.Where(x => x.Cate.Name.Equals(category)).ToListAsync();
            return new Response("Success", true, 0, list);
        }
    }
}
