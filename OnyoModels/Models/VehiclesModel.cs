using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnyaModels
{
    public class VehiclesModel
    {
        public class AddVehicleRequestModel
        {
            public int userid { get; set; }
            public string registrationnumber { get; set; }
            public string vehicletype { get; set; }
            public string photofront { get; set; }
            public string photoback { get; set; }
            public bool isregisteredentity { get; set; }
            public string registeredownername { get; set; }
            public bool owntrailer { get; set; }
            public string regonumber { get; set; }
        }

        public class VehicleModel
        {
            public int vehicleid { get; set; }
            public int userid { get; set; }
            public string registrationnumber { get; set; }
            public string vehicletype { get; set; }
            public string photofront { get; set; }
            public string photoback { get; set; }
            public bool isregisteredentity { get; set; }
            public string registeredownername { get; set; }
            public bool owntrailer { get; set; }
            public string regonumber { get; set; }
        }
    }
}
