using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using static OnyaModels.UserOnyaModel;
using static System.Net.Mime.MediaTypeNames;

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
                                        receivername,receiveremail,receiverphone,amount,cancounter,status,driverstatus from dbo.tbl_onyas");
                return helper.GetList<OnyaModel>(query, new { query = query });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<OnyaModel> AllOtherOnyas(int userid)
        {
            try
            {
                string query = string.Format(@" select tbo.onyaid,tbo.userid,tbo.driverid,tbo.packagesize,tbo.packageweight,tbo.packagetype,tbo.comments,tbo.pickupdate,tbo.pickuplat,
                                        tbo.pickuplong,tbo.pickupaddress,tbo.droplat,tbo.droplong,tbo.dropaddress,tbo.pickuppoint,
										tbo.droppoint,tbo.pickupslot,tbo.dropslot,tbo.receivername,tbo.receiveremail,tbo.receiverphone,
										tbo.amount,tbo.cancounter,tbo.status,tbo.driverstatus,tu.userid as ownerid,tu.name as ownername,tu.profilepic as ownerimage
										from dbo.tbl_onyas tbo 
										inner join dbo.tbl_users tu on tbo.userid = tu.userid
										where tbo.userid != @userid");
                return helper.GetList<OnyaModel>(query, new { query = query, userid = userid });
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
                string query = string.Format(@" select tbo.onyaid,tbo.userid,tbo.driverid,tbo.packagesize,tbo.packageweight,tbo.packagetype,tbo.comments,tbo.pickupdate,tbo.pickuplat,
                                        tbo.pickuplong,tbo.pickupaddress,tbo.droplat,tbo.droplong,tbo.dropaddress,tbo.pickuppoint,
										tbo.droppoint,tbo.pickupslot,tbo.dropslot,tbo.receivername,tbo.receiveremail,tbo.receiverphone,
										tbo.amount,tbo.cancounter,tbo.status,tbo.driverstatus,tu.userid as ownerid,tu.name as ownername,tu.profilepic as ownerimage
										from dbo.tbl_onyas tbo 
										inner join dbo.tbl_users tu on tbo.userid = tu.userid
										where tbo.userid = @userid");
                return helper.GetList<OnyaModel>(query, new { query = query, userid = userid });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<String> GetOnyaImages(int onyaid)
        {
            try
            {
                string query = string.Format(@" select image from dbo.tbl_onya_images where onyaid = @onyaid");
                return helper.GetList<String>(query, new { query = query, onyaid = onyaid });
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
                string query = string.Format(@" select tbo.onyaid,tbo.userid,tbo.driverid,tbo.packagesize,tbo.packageweight,tbo.packagetype,tbo.comments,tbo.pickupdate,tbo.pickuplat,
                                        tbo.pickuplong,tbo.pickupaddress,tbo.droplat,tbo.droplong,tbo.dropaddress,tbo.pickuppoint,
										tbo.droppoint,tbo.pickupslot,tbo.dropslot,tbo.receivername,tbo.receiveremail,tbo.receiverphone,
										tbo.amount,tbo.cancounter,tbo.status,tbo.driverstatus,tu.userid as ownerid,tu.name as ownername,tu.profilepic as ownerimage
										from dbo.tbl_onyas tbo 
										inner join dbo.tbl_users tu on tbo.userid = tu.userid
										where tbo.driverid = @userid");
                return helper.GetList<OnyaModel>(query, new { query = query, userid = userid });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int CreateOnya(int userid, string packagesize, double packageweight, string packagetype, string comments, DateTime pickupdate, double pickuplat, double pickuplong,
                                                string pickupaddress, double droplat, double droplong, string dropaddress, string pickuppoint, string droppoint, string pickupslot,
                                                string dropslot, string receivername, string receiveremail, string receiverphone, double amount, bool cancounter)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_onyas(userid,packagesize,packageweight,packagetype,comments,pickupdate,pickuplat,pickuplong,
                                                pickupaddress,droplat,droplong,dropaddress,pickuppoint,droppoint,pickupslot,dropslot,receivername,receiveremail,receiverphone,
                                                amount,cancounter,status)
                                                values(@userid,@packagesize,@packageweight,@packagetype,@comments,@pickupdate,@pickuplat,@pickuplong,
                                                @pickupaddress,@droplat,@droplong,@dropaddress,@pickuppoint,@droppoint,@pickupslot,@dropslot,@receivername,@receiveremail,@receiverphone,
                                                @amount,@cancounter,1) RETURNING onyaid;");
                return helper.InsertAndGetId(query, new
                {
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
                    receivername = receivername,
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

        public int InsertOnyaImages(int onyaid, string image, int imagetype)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_onya_images(onyaid, image, imagetype)
                                                values(@onyaid,@image,@imagetype) RETURNING id;");

                return helper.InsertAndGetId(query, new
                {
                    onyaid = onyaid,
                    image = image,
                    imagetype = imagetype
                });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public int InsertOnyaRequests(int onyaid, int driverid)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_onya_request(onyaid, driverid)
                                                values(@onyaid,@driverid) RETURNING requestid;");

                return helper.InsertAndGetId(query, new
                {
                    onyaid = onyaid,
                    driverid = driverid
                });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }

        public int UpdateDriverId(int onyaid, int driverid)
        {
            string query = string.Format(@"update dbo.tbl_onyas set driverid = @driverid where onyaid = @onyaid;");
            return helper.ExecuteNonQuery(query.ToLower(), new { driverid = driverid, onyaid = onyaid });
        }

        public int DeleteDriverRequest(int onyaid, int driverid)
        {
            string query = string.Format(@"delete from dbo.tbl_onya_request where driverid = @driverid and onyaid = @onyaid;");
            return helper.ExecuteNonQuery(query.ToLower(), new { driverid = driverid, onyaid = onyaid });
        }
    }

}
