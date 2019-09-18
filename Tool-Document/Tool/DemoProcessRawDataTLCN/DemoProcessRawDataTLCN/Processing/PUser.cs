using System;
using System.Collections.Generic;
using System.Text;
using DemoProcessRawDataTLCN.BUS;
using DemoProcessRawDataTLCN.Models;

namespace DemoProcessRawDataTLCN.Processing
{
    public class PUser
    {
        private UserBAL userBal;

        public PUser()
        {
            userBal = new UserBAL();
        }

        public void Execute()
        {
            var ds = userBal.GetListUser();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var user = new User()
                {
                    ID = int.Parse(ds.Tables[0].Rows[i][0].ToString()),
                    FullName = ds.Tables[0].Rows[i][1].ToString(),
                    Gender = ds.Tables[0].Rows[i][2].ToString(),
                    Birthday = Convert.ToDateTime(ds.Tables[0].Rows[i][3]),
                    PhoneNumber = ds.Tables[0].Rows[i][4].ToString(),
                    Address = ds.Tables[0].Rows[i][5].ToString() + " " + ds.Tables[0].Rows[i][6].ToString()
                };
                userBal.InsertUser(user);
                Console.WriteLine(user.ID);
            }
        }
    }
}
