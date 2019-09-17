using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoProcessRawDataTLCN.BUS;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PAuthorBook
    {
        private AuthorBookBAL authorBookBal;

        public PAuthorBook()
        {
            authorBookBal = new AuthorBookBAL();
        }

        public void Execute()
        {
            var ds = authorBookBal.GetListAuthorBook();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var book_id = ds.Tables[0].Rows[i][0].ToString();
                var authors = ds.Tables[0].Rows[i][1].ToString();
                Console.WriteLine(book_id);
                foreach (var author in SplitMultipleAuthor(authors))
                {
                    if (authorBookBal.GetIDAuthor(author).Obj != null)
                    {
                        int author_id = int.Parse(authorBookBal.GetIDAuthor(author).Obj.ToString());
                        authorBookBal.InsertAuthorBook(book_id, author_id);
                    }
                }
            }
        }

        public List<string> SplitMultipleAuthor(string authors)
        {
            List<string> authorList = new List<string>();
            if (authors.Contains(","))
            {
                var authorArr = authors.Split(",");
                for (int i = 0; i < authorArr.Length; i++)
                {
                    if (authorArr[i] != "" && authorArr[i] != " ")
                    {
                        authorList.Add(RemoveBadBlankSpace(authorArr[i]));
                    }
                }
            }
            else
            {
                authorList.Add(authors);
            }
            return authorList;
        }

        public string RemoveBadBlankSpace(string substring)
        {
            if (char.IsWhiteSpace(substring[0]))
            {
                while (char.IsWhiteSpace(substring[0]))
                {
                    substring = substring.Remove(0, 1);
                }
            }
            if (char.IsWhiteSpace(substring[substring.Length - 1]))
            {
                while (char.IsWhiteSpace(substring[substring.Length - 1]))
                {
                    substring = substring.Remove(substring.Length - 1, 1);
                }
            }
            return substring;
        }
    }
}
