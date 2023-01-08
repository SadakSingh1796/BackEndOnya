using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public class PaymentIntentModel
        {
            [Required]
            public long? Amount { get; set; }
            [Required]
            public string Currency { get; set; }
            public string Name { get; set; }
            public long UserId { get; set; }
            public string Stripe_Customer_Id { get; set; }
        }
    }
}
