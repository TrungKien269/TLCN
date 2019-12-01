using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Objects;

namespace BookStore.BUS.Logic
{
    public class CRUDBookBAL
    {
        private BookBAL bookBal;
        private AuthorBAL authorBal;

        public CRUDBookBAL()
        {
            bookBal = new BookBAL();
            authorBal = new AuthorBAL();
        }

        public async Task<Response> CreateBookProcess(Book book, List<Author> authors, List<string> images, int cateID,
            int formID, int supplierID, int publisherID)
        {
            try
            {
                var res = await bookBal.Insert(book);
                if (res.Status)
                {
                    foreach (var author in authors)
                    {
                        var response = await authorBal.InsertAuthor(author);
                        await bookBal.InsertAuthorsBook(book.Id, (response.Obj as Author).Id);
                    }

                    for (int i = 0; i < images.Count; i++)
                    {
                        await bookBal.InsertImagesBook(book.Id, i + 1, images[i]);
                    }

                    await bookBal.InsertCategoryBook(book.Id, cateID);
                    await bookBal.InsertFormBook(book.Id, formID);
                    await bookBal.InsertSupplierrBook(book.Id, supplierID);
                    await bookBal.InsertPublisherBook(book.Id, publisherID);

                    return new Response("Success", true, 1, book);
                }
                else
                {
                    return res;
                }
            }
            catch (Exception e)
            {
                return Response.CatchError(e.Message);
            }
        }
    }
}
