using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnyaModels
{
    public class BusinessAccountModel
    {
        public class BusinessAccount
        {
            public int accountid { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phonenumber { get; set; }
            public bool isactive { get; set; }
        }

        public class BusinessAccountRequestModel
        {
            public string name { get; set; }
            public string email { get; set; }
            public string phonenumber { get; set; }
            public string password { get; set; }
        }

        public class ToggleBusinessAccountRequestModel
        {
            public int accountid  { get; set; }
            public bool isactive { get; set; }
        }
    }
}
