using Microsoft.Extensions.Configuration;
using OnyaModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using static OnyaModels.BankDetailsModel;

namespace OnyaServices
{
    public class BankDetailsService
    {
        private QueryHelper helper;
        public IConfiguration Configuration;

        public BankDetailsService()
        {
            helper = new QueryHelper();
        }

        public List<BankDetailModel> GetUserBankDetails(int userid)
        {
            try
            {
                string query = string.Format(@" select id,userid,holdername,accounttype,bankname,bsbnumber,accountnumber,isprimary from dbo.tbl_bank_details where userid = @userid");
                return helper.GetList<BankDetailModel>(query, new { userid  = userid });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int InsertBankDetails(int userid, string holdername, string accounttype, string bankname, string bsbnumber, string accountnumber, bool isprimary)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_bank_details(userid,holdername,accounttype,bankname,bsbnumber,accountnumber,isprimary)
                values(@userid,@holdername,@accounttype,@bankname,@bsbnumber,@accountnumber,@isprimary) RETURNING id;");
                return helper.InsertAndGetId(query, new { userid = userid, holdername = holdername, accounttype = accounttype, bankname = bankname, bsbnumber = bsbnumber, accountnumber = accountnumber, isprimary = isprimary });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }
    }

}
