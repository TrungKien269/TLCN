using System;
using System.Collections.Generic;
using System.Text;
using DemoProcessRawDataTLCN.BUS;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PSupplier
    {
        private SupplierBAL supplierBal;

        public PSupplier()
        {
            supplierBal = new SupplierBAL();
        }

        public void Execute()
        {
            var ds = supplierBal.GetSuppliers();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var name = ds.Tables[0].Rows[i][0].ToString();
                supplierBal.InsertSupplier(i + 1, name);
            }
        }
    }
}
