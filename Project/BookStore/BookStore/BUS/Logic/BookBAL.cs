using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class BookBAL:IReporsitory<Book, string>
    {
        private BookStoreContext context;

        public BookBAL(BookStoreContext context)
        {
            this.context = context;
        }

        public Task<List<Book>> GetList()
        {
            return context.Book.Where(x => x.OriginalPrice > 50000 && x.OriginalPrice < 130000).OrderBy(x => x.Id)
                .Take(10).ToListAsync();
        }

        public Task<int> Insert(Book obj)
        {
            context.Book.Add(obj);
            return context.SaveChangesAsync();
        }

        public Task<int> Delete(Book obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Book obj)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetObject(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
