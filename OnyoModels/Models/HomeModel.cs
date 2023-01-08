using System;
using System.Collections.Generic;
using System.Text;
using static OnyaModels.BankDetailsModel;
using static OnyaModels.DocumentsModel;
using static OnyaModels.UserOnyaModel;
using static OnyaModels.VehiclesModel;

namespace OnyaModels
{
    public class HomeModel
    {
        public class HomeCollection
        {
            public HomeUserModel userModel { get; set; }
            public List<VehicleModel> vehicleModels { get; set; }
            public List<DocumentModel> documentModel { get; set; }
            public List<BankDetailModel> bankDetailModels { get; set; }
            public List<OnyaModel> sendingOnyas { get; set; }
            public List<OnyaModel> deliveringOnyas { get; set; }
        }

        public class HomeUserModel
        {
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string profilepic { get; set; }
            public bool isuserverified { get; set; }
            public bool isdriververified { get; set; }
            public string notificationtoken { get; set; }
        }
    }
}
