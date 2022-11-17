using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnyaModels
{
    public class UserOnyaModel
    {
        public class CreateOnyaRequestModel
        {
            public int userid { get; set; }
            public string packagesize { get; set; }
            public double packageweight { get; set; }
            public string packagetype { get; set; }
            public string comments { get; set; }
            public String pickupdate { get; set; }
            public double pickuplat { get; set; }
            public double pickuplong { get; set; }
            public string pickupaddress { get; set; }
            public double droplat { get; set; }
            public double droplong { get; set; }
            public string dropaddress { get; set; }
            public string pickuppoint { get; set; }
            public string droppoint { get; set; }
            public string pickupslot { get; set; }
            public string dropslot { get; set; }
            public string receivername { get; set; }
            public string receiveremail { get; set; }
            public string receiverphone { get; set; }
            public double amount { get; set; }
            public bool cancounter { get; set; }
            public int status { get; set; }
            public List<String> images { get; set; }
        }

        public class OnyaModel
        {
            public int onyaid { get; set; }
            public int userid { get; set; }
            public int driverid { get; set; }
            public string packagesize { get; set; }
            public double packageweight { get; set; }
            public string packagetype { get; set; }
            public string comments { get; set; }
            public DateTime pickupdate { get; set; }
            public double pickuplat { get; set; }
            public double pickuplong { get; set; }
            public string pickupaddress { get; set; }
            public double droplat { get; set; }
            public double droplong { get; set; }
            public string dropaddress { get; set; }
            public string pickuppoint { get; set; }
            public string droppoint { get; set; }
            public string pickupslot { get; set; }
            public string dropslot { get; set; }
            public string receivername { get; set; }
            public string receiveremail { get; set; }
            public string receiverphone { get; set; }
            public double amount { get; set; }
            public bool cancounter { get; set; }
            public int status { get; set; }
            public int driverstatus { get; set; }
            public int ownerid { get; set; }
            public string ownername { get; set; }
            public string ownerimage { get; set; }
        }
    }
}
