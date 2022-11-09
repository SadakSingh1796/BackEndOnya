using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using static OnyaModels.DocumentsModel;

namespace OnyaServices
{

    public class DocumentsService
    {
        private QueryHelper helper;
        public IConfiguration Configuration;

        public DocumentsService()
        {
            helper = new QueryHelper();
        }

        public List<DocumentModel> GetUserDocuments(int userid)
        {
            try
            {
                string query = string.Format(@" select documentid,userid,profile,passport,license,governmentid,isverified from dbo.tbl_documents where userid = @userid");
                return helper.GetList<DocumentModel>(query, new { userid  = userid });
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public DocumentModel GetUserDocument(int userid)
        {
            try
            {
                string query = string.Format(@" select documentid,userid,profile,passport,license,governmentid,isverified from dbo.tbl_documents where userid = @userid");
                return helper.Get<DocumentModel>(query, new { userid = userid });
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public int InsertDocument(int userid,string profile, string passport, string license, string governmentid)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_documents(userid,profile,passport,license,governmentid)
                values(@userid,@profile,@passport,@license,@governmentid) RETURNING documentid;");
                return helper.InsertAndGetId(query, new { userid = userid, profile = profile, passport = passport, license = license, governmentid = governmentid });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

    }

}
