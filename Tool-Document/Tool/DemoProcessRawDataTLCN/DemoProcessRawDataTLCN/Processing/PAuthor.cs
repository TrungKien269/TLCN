using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DemoProcessRawDataTLCN.BUS;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PAuthor
    {
        private AuthorBAL authorBal;

        public PAuthor()
        {
            authorBal = new AuthorBAL();
        }

        public void Execute()
        {
            var AuthorList = GetListAuthorFromDataset();
            var newAuthorList = SplitMultipleAuthor(AuthorList);
            for (int i = 0; i < newAuthorList.Count; i++)
            {
                authorBal.InsertAuthor(i + 1, newAuthorList[i]);
                Console.WriteLine(newAuthorList[i]);
            }
        }

        public List<string> GetListAuthorFromDataset()
        {
            List<string> AuthorList = new List<string>();

            for (int i = 0; i < authorBal.GetAuthors().Tables[0].Rows.Count; i++)
            {
                AuthorList.Add(authorBal.GetAuthors().Tables[0].Rows[i][0].ToString());
            }

            return AuthorList;
        }

        public List<string> SplitMultipleAuthor(List<string> AuthorList)
        {
            var multipleAuthorList = AuthorList.Where(x => x.Contains(',')).ToList();
            AuthorList = AuthorList.Where(x => !x.Contains(",")).ToList();
            foreach (var str in multipleAuthorList)
            {
                var strArr = str.Split(",");
                for (int i = 0; i < strArr.Length; i++)
                {
                    if (strArr[i] != "" && strArr[i] != " ")
                    {
                        AuthorList.Add(RemoveBadBlankSpace(strArr[i]));
                    }
                }
            }
            return AuthorList.Distinct().OrderBy(x => x).ToList();
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
