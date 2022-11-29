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
using static OnyaModels.BusinessAccountModel;

namespace OnyaServices
{
    public class BusinessAccountService
    {
        private QueryHelper helper;
        public IConfiguration Configuration;

        public BusinessAccountService()
        {
            helper = new QueryHelper();
        }

        public List<BusinessAccount> GetBusinessAccounts()
        {
            try
            {
                string query = string.Format(@" select accountid,name,email,phonenumber,isactive from dbo.tbl_business_accounts");
                return helper.GetList<BusinessAccount>(query, new { query = query });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int AddBusinessAccount(string name, string email, string phonenumber, string password, DateTime accountcreated)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_business_accounts(name,email,phonenumber,password,accountcreated)
                values(@name,@email,@phonenumber,@password,@accountcreated) RETURNING accountid;");
                return helper.InsertAndGetId(query, new { name = name, email = email, phonenumber = phonenumber, password = password, accountcreated  = accountcreated });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public int ToggleBusinessAccount(bool isVerified, int accountid)
        {
            string query = string.Format(@"update dbo.tbl_business_accounts set isactive = @isVerified where accountid = @accountid;");
            return helper.ExecuteNonQuery(query.ToLower(), new { isVerified = isVerified, accountid = accountid });
        }
    }

}
