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
                var listBook = await context.PublisherBook.Where(x =>
                        x.Publisher.Name.Contains(publisher.Name, StringComparison.CurrentCultureIgnoreCase))
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

        public async Task<Response> FilterBooksFromListFamousPublisher(List<int> cateIDs, int skipNumber)
        {
            try
            {
                var publishers = await context.FamousPublisher.Where(x => cateIDs.Any(y => y.Equals(x.Id))).ToListAsync();
                var listBook = await context.PublisherBook.Where(x =>
                        publishers.Any(
                            y => x.Publisher.Name.Contains(y.Name, StringComparison.CurrentCultureIgnoreCase)))
                    .Select(x => x.Book).OrderBy(x => x.CurrentPrice).Skip(skipNumber).Take(12).ToListAsync();
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

        public async Task<Response> Insert(Book obj)
        {
            try
            {
                var res = await context.Book.Where(x => x.Id.Equals(obj.Id, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefaultAsync();
                if (res is null)
                {
                    obj.Status = "Available";
                    await context.Book.AddAsync(obj);
                    await context.SaveChangesAsync();
                    return new Response("Success", true, 1, obj);
                }
                else
                {
                    return new Response("This book has been added to system!", false, 1, obj);
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> InsertAuthorsBook(string bookID, int authorID)
        {
            try
            {
                var authorBook = new AuthorBook
                {
                    BookId = bookID,
                    AuthorId = authorID
                };
                await context.AuthorBook.AddAsync(authorBook);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, authorBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> InsertImagesBook(string bookID, int imageID, string path)
        {
            try
            {
                var imageBook = new ImageBook
                {
                    BookId = bookID,
                    ImageId = imageID,
                    Path = path
                };
                await context.ImageBook.AddAsync(imageBook);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, imageBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> InsertCategoryBook(string bookID, int cateID)
        {
            try
            {
                var bookCategory = new BookCategory
                {
                    BookId = bookID,
                    CateId = cateID
                };
                await context.BookCategory.AddAsync(bookCategory);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, bookCategory);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> InsertFormBook(string bookID, int formID)
        {
            try
            {
                var formBook = new FormBook
                {
                    BookId = bookID,
                    FormId = formID
                };
                await context.FormBook.AddAsync(formBook);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, formBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> InsertPublisherBook(string bookID, int publisherID)
        {
            try
            {
                var publisherBook = new PublisherBook
                {
                    BookId = bookID,
                    PublisherId = publisherID
                };
                await context.PublisherBook.AddAsync(publisherBook);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, publisherBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> InsertSupplierrBook(string bookID, int supplierID)
        {
            try
            {
                var supplierBook = new SupplierBook
                {
                    BookId = bookID,
                    SupplierId = supplierID
                };
                await context.SupplierBook.AddAsync(supplierBook);
                await context.SaveChangesAsync();
                return new Response("Success", true, 1, supplierBook);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
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
                return new Response("Success", true, books.Count, books);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }

        public async Task<Response> SearchBook(string value, int skipNumber)
        {
            try
            {
                var books = await context.Book.Include(x => x.BookCategory).ThenInclude(x => x.Cate)
                    .ThenInclude(x => x.Cate).Where(x =>
                        x.Name.Contains(value, StringComparison.CurrentCultureIgnoreCase) ||
                        x.Id.Contains(value, StringComparison.CurrentCultureIgnoreCase)).Skip(skipNumber).Take(12)
                    .ToListAsync();
                foreach (var book in books)
                {
                    book.Id = SecureHelper.GetSecureOutput(book.Id);
                }
                return new Response("Success", true, books.Count, books);
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
