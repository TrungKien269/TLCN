using System;
using System.Collections.Generic;
using System.Text;
using DemoProcessRawDataTLCN.BUS;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PSupplierBook
    {
        private SupplierBookBAL supplierBookBal;

        public PSupplierBook()
        {
            supplierBookBal = new SupplierBookBAL();
        }

        public void Execute()
        {
            var ds = supplierBookBal.GetListSupplierBook();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var book_id = ds.Tables[0].Rows[i][0].ToString();
                if (supplierBookBal.GetIDSupplier(ds.Tables[0].Rows[i][1].ToString()).Obj != null)
                {
                    var supplier_id = int.Parse(supplierBookBal.GetIDSupplier(ds.Tables[0].Rows[i][1].ToString()).Obj
                        .ToString());
                    supplierBookBal.InsertSupplierBook(book_id, supplier_id);
                    Console.WriteLine(book_id);
                }
            }
        }
    }
}
