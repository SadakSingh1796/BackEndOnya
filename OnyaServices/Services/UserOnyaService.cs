using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using static OnyaModels.UserOnyaModel;

namespace OnyaServices
{
    public class UserOnyaService
    {
        private QueryHelper helper;
        public IConfiguration Configuration;

        public UserOnyaService()
        {
            helper = new QueryHelper();
        }

        public List<OnyaModel> GetAllOnyas()
        {
            try
            {
                string query = string.Format(@" select onyaid,userid,driverid,packagesize,packageweight,packagetype,comments,pickupdate,pickuplat,
                                        pickuplong,pickupaddress,droplat,droplong,dropaddress,pickuppoint,droppoint,pickupslot,dropslot,
                                        receiveremail,receiverphone,amount,cancounter,status,driverstatus from dbo.tbl_onyas");
                return helper.GetList<OnyaModel>(query, new { query = query });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<OnyaModel> GetUserOnyas(int userid)
        {
            try
            {
                string query = string.Format(@" select onyaid,userid,driverid,packagesize,packageweight,packagetype,comments,pickupdate,pickuplat,
                                        pickuplong,pickupaddress,droplat,droplong,dropaddress,pickuppoint,droppoint,pickupslot,dropslot,
                                        receiveremail,receiverphone,amount,cancounter,status,driverstatus from dbo.tbl_onyas where userid = @userid");
                return helper.GetList<OnyaModel>(query, new { query = query, userid = userid });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<OnyaModel> GetDriverOnyas(int userid)
        {
            try
            {
                string query = string.Format(@" select onyaid,userid,driverid,packagesize,packageweight,packagetype,comments,pickupdate,pickuplat,
                                        pickuplong,pickupaddress,droplat,droplong,dropaddress,pickuppoint,droppoint,pickupslot,dropslot,
                                        receiveremail,receiverphone,amount,cancounter,status,driverstatus from dbo.tbl_onyas where driverid = @userid");
                return helper.GetList<OnyaModel>(query, new { query = query, userid = userid });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int CreateOnya(int userid, string packagesize, double packageweight, string packagetype, string comments, DateTime pickupdate, double pickuplat,double pickuplong,
                                                string pickupaddress, double droplat,double droplong,string dropaddress,string pickuppoint,string droppoint,string pickupslot,
                                                string dropslot,string receiveremail,string receiverphone,double amount,bool cancounter)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_onyas(userid,packagesize,packageweight,packagetype,comments,pickupdate,pickuplat,pickuplong,
                                                pickupaddress,droplat,droplong,dropaddress,pickuppoint,droppoint,pickupslot,dropslot,receiveremail,receiverphone,
                                                amount,cancounter,status)
                                                values(@userid,@packagesize,@packageweight,@packagetype,@comments,@pickupdate,@pickuplat,@pickuplong,
                                                @pickupaddress,@droplat,@droplong,@dropaddress,@pickuppoint,@droppoint,@pickupslot,@dropslot,@receiveremail,@receiverphone,
                                                @amount,@cancounter,1) RETURNING onyaid;");
                return helper.InsertAndGetId(query, new {
                    userid = userid,
                    packagesize = packagesize,
                    packageweight = packageweight,
                    packagetype = packagetype,
                    comments = comments,
                    pickupdate = pickupdate,
                    pickuplat = pickuplat,
                    pickuplong = pickuplong,
                    pickupaddress = pickupaddress,
                    droplat = droplat,
                    droplong = droplong,
                    dropaddress = dropaddress,
                    pickuppoint = pickuppoint,
                    droppoint = droppoint,
                    pickupslot = pickupslot,
                    dropslot = dropslot,
                    receiveremail = receiveremail,
                    receiverphone = receiverphone,
                    amount = amount,
                    cancounter = cancounter
                });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }
    }

}
