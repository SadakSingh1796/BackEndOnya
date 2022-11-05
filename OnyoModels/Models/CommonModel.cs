using System;
using System.Collections.Generic;
using System.Text;

namespace OnyaModels
{
    public class CommonModel
    {
        public class ApiResult
        {
            public bool isSuccess { get; set; }
            public string message { get; set; }
            public object data { get; set; }
        }

        public class CommonResponse
        {
            public int userid { get; set; }
            public string name { get; set; }
        }
    }
}
