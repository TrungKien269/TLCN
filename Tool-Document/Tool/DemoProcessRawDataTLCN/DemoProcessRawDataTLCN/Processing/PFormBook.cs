using System;
using System.Collections.Generic;
using System.Text;
using DemoProcessRawDataTLCN.BUS;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PFormBook
    {
        private FormBookBAL formBookBal;

        public PFormBook()
        {
            formBookBal = new FormBookBAL();
        }

        public void Execute()
        {
            var ds = formBookBal.GetListFormBook();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var book_id = ds.Tables[0].Rows[i][0].ToString();
                if (formBookBal.GetIDForm(ds.Tables[0].Rows[i][1].ToString()).Obj != null)
                {
                    var supplier_id = int.Parse(formBookBal.GetIDForm(ds.Tables[0].Rows[i][1].ToString()).Obj
                        .ToString());
                    formBookBal.InsertFormBook(book_id, supplier_id);
                    Console.WriteLine(book_id);
                }
            }
        }
    }
}
