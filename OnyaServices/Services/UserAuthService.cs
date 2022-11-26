using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using static OnyaModels.HomeModel;
using static OnyaModels.UserAuthModel;

namespace OnyaServices
{
    public class UserAuthService
    {
        private QueryHelper helper;
        public IConfiguration Configuration;

        public UserAuthService()
        {
            helper = new QueryHelper();
        }

        public List<AllUserModel> GetAllUsers()
        {
            try
            {
                string query = string.Format(@"select userid,name,email,phone,isemailverified,ismobileverified,accountcreated,isactive,isdeleted,isuserverified,isdriververified,devicetype from dbo.tbl_users order by userid desc");
                return helper.GetList<AllUserModel>(query , new { query = query });
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public UserModel GetUser(LoginRequestModel model)
        {
            try
            {
                string filter = "";

                if (model.type == "email")
                {
                    filter = "email = ";
                }
                else if (model.type == "phone")
                {
                    filter = "phone = ";
                }
                else if (model.type == "google")
                {
                    filter = "googleid = ";
                }
                else if (model.type == "facebook")
                {
                    filter = "facebookid = ";
                }

                filter += "'" + model.value + "'";

                string query = string.Format(@" select userid,name,email,phone,isemailverified,ismobileverified,isuserverified,isdriververified from dbo.tbl_users where " + filter + " and isactive = true and isdeleted = false");
                return helper.Get<UserModel>(query);
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public UserModel GetUserByEmailOrPhone(string email, string phone)
        {
            try
            {
                string query = string.Format(@"select userid,name,email,phone,isemailverified,ismobileverified,isuserverified,isdriververified from dbo.tbl_users where  email = @email or phone = @phone");
                return helper.Get<UserModel>(query, new { email = email, phone = phone });
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public HomeUserModel GetUserById(int userid)
        {
            try
            {
                string query = string.Format(@"select name,email,phone,isuserverified,isdriververified from dbo.tbl_users where  userid = @userid");
                return helper.Get<HomeUserModel>(query, new { userid = userid });
            }
            catch (Exception ex)
            {

            }

            return null;
        }

        public int InsertUser(string name, string email, string phone, DateTime accountcreated, string notificationtoken, int devicetype)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_users(name, email, phone, accountcreated,notificationtoken, devicetype)
                values(@name, @email, @phone, @accountcreated, @notificationtoken,@devicetype) RETURNING userid;");
                return helper.InsertAndGetId(query, new { name = name, email = email, phone = phone, accountcreated = accountcreated, notificationtoken = notificationtoken, devicetype = devicetype });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public int InsertSocialUser(string name, string facebookid, string googleid, DateTime accountcreated, string notificationtoken, int devicetype)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_users(name, facebookid, googleid, accountcreated,notificationtoken, devicetype)
                values(@name, @facebookid, @googleid, @accountcreated, @notificationtoken,@devicetype) RETURNING userid;");
                return helper.InsertAndGetId(query, new { name = name, facebookid = facebookid, googleid = googleid, accountcreated = accountcreated, notificationtoken = notificationtoken, devicetype = devicetype });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public int UpdateUserStatus(bool isVerified, int userid)
        {
            string query = string.Format(@"update dbo.tbl_users set isuserverified = @isverified where userid = @userid;");
            return helper.ExecuteNonQuery(query.ToLower(), new { isVerified = isVerified, userid = userid });
        }

        public int UpdateDriverStatus(bool isVerified, int userid)
        {
            string query = string.Format(@"update dbo.tbl_users set isdriververified = @isverified where userid = @userid;");
            return helper.ExecuteNonQuery(query.ToLower(), new { isVerified = isVerified, userid = userid });
        }

        public int VerifyUserDocuments(int documentid, bool isverified, string comment)
        {
            string query = string.Format(@"update dbo.tbl_documents set isverified = @isverified and comment = @comment where documentid = @documentid;");
            return helper.ExecuteNonQuery(query.ToLower(), new { isverified = isverified, documentid = documentid, comment = comment });
        }
    }
}
