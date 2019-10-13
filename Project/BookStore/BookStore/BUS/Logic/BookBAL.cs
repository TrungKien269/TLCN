using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Helper;
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
            try
            {
                var list = await context.Book.Where(x => x.OriginalPrice > 50000 && x.OriginalPrice < 130000)
                    .OrderBy(x => x.Id)
                    .Take(30).Skip(10).ToListAsync();
                return new Response("Success", true, 0, list);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> GetBookFromFamousPublisher(string id)
        {
            try
            {
                var publisher = await context.FamousPublisher.Where(x => x.Id.Equals(int.Parse(id))).FirstOrDefaultAsync();
                var listBook = await context.PublisherBook.Where(x => x.Publisher.Name.Contains(publisher.Name))
                    .Select(x => x.Book).Take(4).ToListAsync();
                foreach (var book in listBook)
                {
                    book.Id = SecureHelper.GetSecureOutput(book.Id);
                }
                return new Response("Success", true, 1, listBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
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

        public async Task<Response> GetObject(string id)
        {
            try
            {
                var book = await context.Book.Include(x => x.AuthorBook).Include(x => x.BookCategory)
                    .Include(x => x.Comment).Include(x => x.FormBook).Include(x => x.ImageBook)
                    .Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (book is null)
                {
                    return new Response("Cannot find this book", false, 0, null);
                }
                else
                {
                    return new Response("Success", true, 1, book);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
