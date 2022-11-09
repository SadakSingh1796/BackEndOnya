using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;
using static OnyaModels.VehiclesModel;

namespace OnyaServices
{
    public class VehiclesService
    {
        private QueryHelper helper;
        public IConfiguration Configuration;

        public VehiclesService()
        {
            helper = new QueryHelper();
        }

        public List<VehicleModel> GetUserVehicles(int userid)
        {
            try
            {
                string query = string.Format(@" select vehicleid,userid,registrationnumber,vehicletype,photofront,photoback,isregisteredentity,registeredownername,owntrailer,regonumber from dbo.tbl_vehicles where userid = @userid");
                return helper.GetList<VehicleModel>(query, new { userid  = userid });
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public int InsertVehicle(int userid,string registrationnumber,string vehicletype,string photofront,string photoback,bool isregisteredentity,string registeredownername,bool owntrailer,string regonumber)
        {
            try
            {
                string query = string.Format(@" insert into dbo.tbl_vehicles(userid,registrationnumber,vehicletype,photofront,photoback,isregisteredentity,registeredownername,owntrailer,regonumber)
                values(@userid,@registrationnumber,@vehicletype,@photofront,@photoback,@isregisteredentity,@registeredownername,@owntrailer,@regonumber) RETURNING vehicleid;");
                return helper.InsertAndGetId(query, new { userid = userid, registrationnumber = registrationnumber, vehicletype = vehicletype, photofront = photofront, photoback = photoback, isregisteredentity = isregisteredentity, registeredownername = registeredownername, owntrailer = owntrailer, regonumber = regonumber });
            }
            catch (Exception ex)
            {

            }

            return 0;
        }
    }

}
