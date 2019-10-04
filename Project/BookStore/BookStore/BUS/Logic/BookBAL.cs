using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BUS.Logic
{
    public class BookBAL:IReporsitory<Book, string, Response>
    {
        private BookStoreContext context;

        public BookBAL()
        {
            this.context = new BookStoreContext();
        }

        public async Task<Response> GetList()
        {
            var list = await context.Book.Where(x => x.OriginalPrice > 50000 && x.OriginalPrice < 130000)
                .OrderBy(x => x.Id)
                .Take(30).Skip(10).ToListAsync();
            return new Response("Success", true, 0, list);
        }

        public async Task<Response> GetBookFromFamousPublisher(string id)
        {
            var publisher = await context.FamousPublisher.Where(x => x.Id.Equals(int.Parse(id))).FirstOrDefaultAsync();
            var task = await context.PublisherBook.Where(x => x.Publisher.Name.Contains(publisher.Name))
                .Select(x => x.Book).Take(4).ToListAsync();
            return new Response("Success", true, 1, task);
        }

        public Task<Response> Insert(Book obj)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Delete(Book obj)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Book obj)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetObject(string id)
        {
            throw new NotImplementedException();
        }
    }
}
