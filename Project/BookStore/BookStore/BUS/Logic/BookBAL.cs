﻿using System;
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
                var book = await context.Book.Include(x => x.AuthorBook).ThenInclude(x => x.Author)
                    .Include(x => x.ImageBook).Include(x => x.PublisherBook).ThenInclude(x => x.Publisher)
                    .Include(x => x.FormBook).ThenInclude(x => x.Form)
                    .Include(x => x.SupplierBook).ThenInclude(x => x.Supplier)
                    .Include(x => x.BookCategory).ThenInclude(x => x.Cate)
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

        public async Task<Response> GetBookWithSecureID(string id)
        {
            try
            {
                var book = await context.Book.Include(x => x.AuthorBook).ThenInclude(x => x.Author)
                    .Include(x => x.ImageBook).Include(x => x.PublisherBook).ThenInclude(x => x.Publisher)
                    .Include(x => x.FormBook).ThenInclude(x => x.Form)
                    .Include(x => x.SupplierBook).ThenInclude(x => x.Supplier)
                    .Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (book is null)
                {
                    return new Response("Cannot find this book", false, 0, null);
                }
                else
                {
                    book.Id = SecureHelper.GetSecureOutput(book.Id);
                    return new Response("Success", true, 1, book);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> GetBookOnly(string id)
        {
            try
            {
                var book = await context.Book.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                if (book is null)
                {
                    return new Response("Cannot find this book", false, 0, null);
                }
                else
                {
                    book.Id = SecureHelper.GetSecureOutput(book.Id);
                    return new Response("Success", true, 1, book);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> GetBookByCategory(string category, int skipNumber)
        {
            try
            {
                var books = await context.Book.Include(x => x.BookCategory).ThenInclude(x => x.Cate)
                    .ThenInclude(x => x.Cate)
                    .Where(x => x.BookCategory.All(y => y.Cate.Cate.Name.Equals(category)))
                    .OrderByDescending(x => x.CurrentPrice).Skip(skipNumber).Take(12).ToListAsync();
                foreach (var book in books)
                {
                    book.Id = SecureHelper.GetSecureOutput(book.Id);
                }
                return new Response("Success", true, 1, books);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> GetBookBySubCategory(string subcategory, int skipNumber)
        {
            try
            {
                var books = await context.Book.Include(x => x.BookCategory).ThenInclude(x => x.Cate)
                    .ThenInclude(x => x.Cate)
                    .Where(x => x.BookCategory.All(y => y.Cate.Name.Equals(subcategory)))
                    .OrderByDescending(x => x.CurrentPrice).Skip(skipNumber).Take(12).ToListAsync();
                foreach (var book in books)
                {
                    book.Id = SecureHelper.GetSecureOutput(book.Id);
                }
                return new Response("Success", true, 1, books);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
