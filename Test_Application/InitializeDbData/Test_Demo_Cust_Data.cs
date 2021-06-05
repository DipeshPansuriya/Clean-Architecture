using Application_Database;
using Application_Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test_Application.InitializeDbData
{
    public class Test_Demo_Cust_Data
    {
        public static void CustomerInitializeData(APP_DbContext context)
        {
            context.Demo_Customers.Add(new Demo_Customer
            {
                Code = "MM",
                Name = "This is Test"
            });

            context.SaveChanges();
        }
    }
}