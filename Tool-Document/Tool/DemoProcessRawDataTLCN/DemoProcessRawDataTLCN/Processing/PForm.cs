using System;
using System.Collections.Generic;
using System.Text;
using DemoProcessRawDataTLCN.BUS;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PForm
    {
        private FormBAL formBal;

        public PForm()
        {
            formBal = new FormBAL();
        }

        public void Execute()
        {
            var ds = formBal.GetForms();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var name = ds.Tables[0].Rows[i][0].ToString();
                formBal.InsertForm(i + 1, name);
            }
        }
    }
}
