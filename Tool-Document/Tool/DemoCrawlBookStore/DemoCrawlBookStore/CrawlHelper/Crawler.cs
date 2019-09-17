using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoCrawlBookStore.BUS;
using DemoCrawlBookStore.Models;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System.Web;
using System.Globalization;
using System.IO;

namespace DemoCrawlBookStore.CrawlHelper
{
    public class Crawler
    {
        private HtmlWeb htmlWeb;
        private BookBAL bookBal;
        private CategoryBAL categoryBal;
        private ImageBookBAL imageBookBal;
        private PublisherBAL publisherBal;

        public Crawler()
        {
            this.htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };

            bookBal = new BookBAL();
            categoryBal = new CategoryBAL();
            imageBookBal = new ImageBookBAL();
            publisherBal = new PublisherBAL();
        }

        public void CrawlListPublisher()
        {
            try
            {
                var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("Fahasa").Value;
                HtmlDocument document = htmlWeb.Load(url, "POST");
                var threadItems = document.DocumentNode.QuerySelectorAll("div.wrapperImage > div.imageNXB").ToList();

                for (int i = 0; i < threadItems.Count; i++)
                {
                    var link = threadItems[i].ChildNodes["a"].Attributes["href"].Value;
                    var image = threadItems[i].ChildNodes["a"].ChildNodes["img"].Attributes["src"].Value;

                    document = htmlWeb.Load(url + "/" + link, "POST");
                    var item_description = document.DocumentNode.QuerySelectorAll("div.description > p").ToList();

                    Publisher publisher = new Publisher()
                    {
                        Name = FamousPublisher.list[i],
                        Image = image,
                        Description = ContentProcessing(item_description)
                    };

                    publisherBal.InsertPublisher(publisher);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DownloadPublisherImage(string link)
        {
            
        }

        public void CrawlListImageBook()
        {
            try
            {
                var urls = ConfigHelper.GetConfig().GetSection("Link").GetSection("ListCategory").GetChildren()
                    .Select(x => x.Value).ToList();

                var url = urls[2];

                for (int i = 1; i <= 20; i++)
                {
                    if (i > 1)
                    {
                        url = urls[1].Replace(".html", "/page/" + i + ".html");
                    }
                    Console.WriteLine(url);

                    HtmlDocument document = htmlWeb.Load(url, "POST");
                    var threadItems = document.DocumentNode.QuerySelectorAll("ul.products-grid > li")
                        .ToList();

                    if (threadItems.Count != 0)
                    {
                        foreach (var threadItem in threadItems)
                        {
                            var item = threadItem
                                .QuerySelector(
                                    "div.item-inner > div.ma-box-content > div.products > div.images-container");
                            var link = item.ChildNodes["a"].Attributes["href"].Value;
                            var book_name = threadItem.QuerySelector("div.item-inner > div.ma-box-content > h2 > a")
                                .Attributes["title"].Value;
                            Console.Write(book_name + "\t\t\t ");
                            CrawlImageBook(link, book_name);
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine();
                }


                #region Draft

                //var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("ListBook").Value;

                //HtmlDocument document = htmlWeb.Load(url, "POST");
                //var threadItems = document.DocumentNode.QuerySelectorAll("ul.products-grid > li")
                //    .ToList();

                //if (threadItems.Count != 0)
                //{
                //    var item = threadItems[2]
                //        .QuerySelector(
                //            "div.item-inner > div.ma-box-content > div.products > div.images-container");
                //    var link = item.ChildNodes["a"].Attributes["href"].Value;
                //    var name = threadItems[0].QuerySelector("div.item-inner > div.ma-box-content > h2 > a")
                //        .Attributes["title"].Value;

                //    document = htmlWeb.Load(link, "POST");
                //    var item_image = document.DocumentNode.QuerySelector(
                //        "div.product-essential > div.col-md-5 > div.more-views > div.row > div.lightgallery");

                //    var images = item_image.QuerySelectorAll("a.include-in-gallery").ToList();
                //    foreach (var img in images)
                //    {
                //        Console.WriteLine(img.Attributes["href"].Value);
                //    }
                //}

                #endregion

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CrawlImageBook(string link, string book_name)
        {
            HtmlDocument document = htmlWeb.Load(link, "POST");
            var item_id = document.DocumentNode.QuerySelectorAll("table.table-striped > tbody > tr")
                            .ToList()[0];
            var id_book = RemoveBlankBeforeText(item_id.QuerySelector("td").InnerHtml.Replace("\n", "")
                .Replace("\t", ""));

            if (int.Parse(imageBookBal.CheckExistedBook(id_book).Obj.ToString()).Equals(1) && 
                int.Parse(imageBookBal.CheckDuplicateBook(id_book).Obj.ToString()).Equals(0))
            {
                var item_image = document.DocumentNode.QuerySelector(
                    "div.product-essential > div.col-md-5 > div.more-views > div.row > div.lightgallery");
                if (item_image != null)
                {
                    var images = item_image.QuerySelectorAll("a.include-in-gallery").ToList();
                    images = images.Where(x => Path.GetExtension(x.Attributes["href"].Value).Equals(".jpg") ||
                                               Path.GetExtension(x.Attributes["href"].Value).Equals(".png") ||
                                               Path.GetExtension(x.Attributes["href"].Value).Equals(".jpeg") ||
                                               Path.GetExtension(x.Attributes["href"].Value).Equals(".svg") ||
                                               Path.GetExtension(x.Attributes["href"].Value).Equals(".gif"))
                        .ToList();

                    ImageBook img = new ImageBook()
                    {
                        id_book = RemoveBlankBeforeText(id_book)
                    };
                    for (int i = 0; i < images.Count; i++)
                    {
                        img.id_image = i + 1;
                        img.path = images[i].Attributes["href"].Value;
                        imageBookBal.InsertImageBook(img);
                    }
                    Console.Write(images.Count);
                }
            }
        }

        public void CrawlListFailureImageBook()
        {
            var dsSubCategory = categoryBal.GetSubCategoryNameList();
            for (int i = 28; i < dsSubCategory.Tables[0].Rows.Count; i++)
            {
                var dsBook = bookBal.GetListFailureIDBookWhenInsertImage(dsSubCategory.Tables[0].Rows[i][0].ToString())
                    .Tables[0];
                Console.WriteLine("----------------" + dsSubCategory.Tables[0].Rows[i][0].ToString() +
                                  "----------------");
                for (int j = 0; j < dsBook.Rows.Count; j++)
                {
                    var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("Search").Value + dsBook.Rows[j][0];
                    HtmlDocument document = htmlWeb.Load(url, "POST");
                    var threadItems = document.DocumentNode.QuerySelectorAll("ul.products-grid > li")
                        .ToList();
                    if (threadItems.Count != 0)
                    {
                        var item = threadItems[0]
                            .QuerySelector(
                                "div.item-inner > div.ma-box-content > div.products > div.images-container");
                        var link = item.ChildNodes["a"].Attributes["href"].Value;
                        var book_name = threadItems[0].QuerySelector("div.item-inner > div.ma-box-content > h2 > a")
                            .Attributes["title"].Value;
                        Console.Write(book_name + "\t\t\t ");
                        CrawlImageBook(link, book_name);
                        Console.WriteLine();
                    }
                }
            }

            #region Draft

            //var dsBook = bookBal.GetListFailureIDBookWhenInsertImage("Crime").Tables[0];
            //for (int i = 0; i < dsBook.Rows.Count; i++)
            //{
            //    var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("Search").Value + dsBook.Rows[i][0];
            //    HtmlDocument document = htmlWeb.Load(url, "POST");
            //    var threadItems = document.DocumentNode.QuerySelectorAll("ul.products-grid > li")
            //        .ToList();
            //    if (threadItems.Count != 0)
            //    {
            //        var item = threadItems[0]
            //            .QuerySelector(
            //                "div.item-inner > div.ma-box-content > div.products > div.images-container");
            //        var link = item.ChildNodes["a"].Attributes["href"].Value;
            //        var book_name = threadItems[0].QuerySelector("div.item-inner > div.ma-box-content > h2 > a")
            //            .Attributes["title"].Value;
            //        Console.Write(book_name + "\t\t\t ");
            //        CrawlImageBook(link, book_name);
            //        Console.WriteLine();
            //    }
            //}

            #endregion
        }

        public void CrawlListBook()
        {
            try
            {
                for (int i = 1; i <= 20; i++)
                {
                    var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("ListBook").Value;
                    if (i > 1)
                    {
                        url = url.Replace(".html", "/page/" + i + ".html");
                    }
                    Console.WriteLine(url);
                    HtmlDocument document = htmlWeb.Load(url, "POST");
                    var threadItems = document.DocumentNode.QuerySelectorAll("ul.products-grid > li")
                        .ToList();

                    var subCategory = document.DocumentNode
                        .QuerySelectorAll("div.container-inner > ol.breadcrumb > li").ToList().LastOrDefault()
                        .ChildNodes["strong"].InnerHtml;

                    if (threadItems.Count != 0)
                    {
                        foreach (var threadItem in threadItems)
                        {
                            var item = threadItem
                                .QuerySelector(
                                    "div.item-inner > div.ma-box-content > div.products > div.images-container");
                            var link = item.ChildNodes["a"].Attributes["href"].Value;
                            var name = threadItem.QuerySelector("div.item-inner > div.ma-box-content > h2 > a")
                                .Attributes["title"].Value;

                            CrawlBook(link, name, subCategory);
                        }
                    }
                }

                //var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("ListBook").Value;
                //HtmlDocument document = htmlWeb.Load(url, "POST");
                //var threadItems = document.DocumentNode.QuerySelectorAll("ul.products-grid > li")
                //    .ToList();
                //var item = threadItems[3]
                //    .QuerySelector("div.item-inner > div.ma-box-content > div.products > div.images-container");
                //var link = item.ChildNodes["a"].Attributes["href"].Value;
                //var name = threadItems[3].QuerySelector("div.item-inner > div.ma-box-content > h2 > a")
                //    .Attributes["title"].Value;
                //var subCategory = document.DocumentNode
                //    .QuerySelectorAll("div.container-inner > ol.breadcrumb > li").ToList().LastOrDefault()
                //    .ChildNodes["strong"].InnerHtml;

                //CrawlBook(link, name, subCategory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CrawlBook(string url, string book_name, string subCategory)
        {
            List<string> Book_Info = new List<string>();
            try
            {
                HtmlDocument document = htmlWeb.Load(url, "POST");
                var item_image = document.DocumentNode.QuerySelector("div.product-essential > div.col-md-5");
                var item_book = document.DocumentNode.QuerySelector("div.product-essential > div.col-md-7");

                var img = item_image.ChildNodes["img"].Attributes["src"].Value;

                var item_price = item_book.QuerySelector(
                    "div.price-block > div.price-block-left > div.price-box");
                var old_price = item_price.QuerySelector("p.old-price > span.price").InnerHtml
                    .Replace("\n", "").Replace(" ", "");
                old_price = old_price.Remove(old_price.Length - 2);
                var price = GetPrice(old_price);

                var item_info =
                    document.DocumentNode.QuerySelectorAll(
                        "div#product_tabs_additional_contents > table.table-striped > tbody > tr").Take(9).ToList();

                var item_content = document.DocumentNode
                    .QuerySelectorAll("div#product_tabs_description_contents > div.std > p").ToList();

                for (int i = 0; i < item_info.Count; i++)
                {
                    if (item_info[i].ChildNodes["td"].ChildNodes["a"] != null)
                    {
                        var info = item_info[i].ChildNodes["td"].ChildNodes["a"].InnerHtml.Replace("\n", "")
                            .Replace("\t", "");
                        Book_Info.Add(RemoveBlankBeforeText(info));
                    }
                    else if (i != 6)
                    {
                        var info = item_info[i].ChildNodes["td"].InnerHtml.Replace("\n", "").Replace("\t", "");
                        Book_Info.Add(RemoveBlankBeforeText(info));
                    }
                }

                Book book = CreateBook(Book_Info);
                book.Name = HttpUtility.HtmlDecode(book_name.Replace("&amp;", "&"));
                book.Image = img;
                book.Price = price;
                book.Summary = HttpUtility.HtmlDecode(ContentProcessing(item_content));
                Console.WriteLine(HttpUtility.HtmlDecode(book_name));
                //Console.WriteLine(HttpUtility.HtmlDecode(ContentProcessing(item_content)));

                InsertBook(book);
                InsertBookCategory(book.Id, subCategory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CrawlCategory()
        {
            try
            {
                //var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("ListBook").Value;
                //HtmlDocument document = htmlWeb.Load(url, "POST");
                //var threadItems = document.DocumentNode.QuerySelectorAll("ul.nav > li")
                //    .ToList();

                //var item_cate = threadItems[0];
                //var cateList = item_cate.QuerySelectorAll("ul.nav-links > li").ToList();
                //cateList = cateList.Where(x => x.QuerySelector("a").ChildNodes["span"] == null).ToList();
                //cateList.ForEach(x => Console.WriteLine(HttpUtility.HtmlDecode(x.ChildNodes["a"].InnerHtml)));

                ////foreach (var cate in cateList)
                ////{
                ////    InsertCategory(cate.ChildNodes["a"].InnerHtml.Replace("&amp;", "&"), 1);
                ////}

                var url = ConfigHelper.GetConfig().GetSection("Link").GetSection("Category").Value;
                HtmlDocument document = htmlWeb.Load(url, "POST");

                var category = document.DocumentNode.QuerySelector("div.m-current-category > span").InnerHtml;
                var subCategory = document.DocumentNode.QuerySelectorAll("ol.m-child-category-list > li")
                    .ToList();

                InsertCategory(category.Replace("&amp;", "&"), 1);
                CrawlSubCategory(subCategory, category.Replace("&amp;", "&"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CrawlSubCategory(List<HtmlNode> Nodes, string category)
        {
            int id = int.Parse(categoryBal.GetCategoryID(category).Obj.ToString());
            Nodes.ForEach(x =>
            {
                InsertSubCategory(HttpUtility.HtmlDecode(x.ChildNodes["a"].InnerHtml), id);
                Console.WriteLine(HttpUtility.HtmlDecode(x.ChildNodes["a"].InnerHtml));
            });

            //for (int i = 7; i < Nodes.Count; i++)
            //{
            //    InsertSubCategory(HttpUtility.HtmlDecode(Nodes[i].ChildNodes["a"].InnerHtml), id);
            //    Console.WriteLine(HttpUtility.HtmlDecode(Nodes[i].ChildNodes["a"].InnerHtml));
            //}
        }

        public Book CreateBook(List<string> Book_Info)
        {
            Book book = new Book();
            book.Id = Book_Info[0];
            book.Supplier = Book_Info[1];
            book.Author = Book_Info[2];
            book.Publisher = Book_Info[3];
            book.ReleaseYear = int.Parse(GetReleaseYear(Book_Info[4]));
            book.Weight = double.Parse(Book_Info[5]);
            book.NumOfPage = int.Parse(Book_Info[6]);
            book.Form = Book_Info[7];
            return book;
        }

        public Notification InsertBook(Book book)
        {
            if (int.Parse(bookBal.CheckExistedBook(book).Obj.ToString()).Equals(0))
                return bookBal.InsertBook(book);
            else
            {
                return new Notification("This book has been stored", false, -1, null);
            }
        }

        public Notification InsertCategory(string name, int region)
        {
            return categoryBal.InsertCategory(name, region);
        }

        public Notification InsertSubCategory(string name, int cateID)
        {
            return categoryBal.InsertSubCategory(name, cateID);
        }

        public Notification InsertBookCategory(string bookID, string category)
        {
            int cateID = int.Parse(categoryBal.GetCategoryID(category.Replace("&amp;", "&")).Obj.ToString());

            return bookBal.InsertBookCategory(bookID, cateID);
        }

        public string ContentProcessing(List<HtmlNode> Nodes)
        {
            StringBuilder content = new StringBuilder();

            foreach (var node in Nodes)
            {
                content.Append(node.InnerHtml + Environment.NewLine);
            }
            return content.ToString().Remove(content.Length - 1, 1);
        }

        public Int32 GetPrice(string price)
        {
            return Int32.Parse(price.Replace(".", ""));
        }

        public string GetReleaseYear(string datetime)
        {
            if (datetime.Contains('/'))
            {
                return datetime.Split('/').ToList().Where(x => x.Length == 4).FirstOrDefault().ToString();
            }
            else
            {
                return datetime.Substring(datetime.Length - 4);
            }
        }

        public string RemoveBlankBeforeText(string text)
        {
            var above = text.IndexOf(text.Where(x => !x.Equals(' ')).FirstOrDefault());
            text = text.Substring(above);

            var below = text.LastIndexOf(text.Where(x => !x.Equals(' ')).LastOrDefault());
            return below == text.Length - 1 ?  text : text.Remove(below + 1);
        }
    }
}
