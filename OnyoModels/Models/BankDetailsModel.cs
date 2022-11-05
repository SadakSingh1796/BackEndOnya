using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnyaModels
{
    public class BankDetailsModel
    {
        public class AddBankDetailsRequestModel
        {
            public int userid { get; set; }
            public string holdername { get; set; }
            public string accounttype { get; set; }
            public string bankname { get; set; }
            public string bsbnumber { get; set; }
            public string accountnumber { get; set; }
            public bool isprimary { get; set; }
        }

        public class BankDetailModel
        {
            public int id { get; set; }
            public int userid { get; set; }
            public string holdername { get; set; }
            public string accounttype { get; set; }
            public string bankname { get; set; }
            public string bsbnumber { get; set; }
            public string accountnumber { get; set; }
            public bool isprimary { get; set; }
        }
    }
}
