using System;
using System.Collections.Generic;
using System.Text;

namespace OnyaModels
{
    public class UserAuthModel
    {
        public class LoginRequestModel
        {
            public string name { get; set; }
            public string type { get; set; }
            public string value { get; set; }
            public string notificationToken { get; set; }
            public int deviceType { get; set; }
        }

        public class SignupRequestModel
        {
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string notificationToken { get; set; }
            public int deviceType { get; set; }
        }

        public class UserModel
        {
            public int userId { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public bool isemailverified { get; set; }
            public bool ismobileverified { get; set; }
            public bool isuserverified { get; set; }
            public bool isdriververified { get; set; }
        }

        public class AllUserModel
        {
            public int userid { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public bool isemailverified { get; set; }
            public bool ismobileverified { get; set; }
            public DateTime accountcreated { get; set; }
            public bool isactive { get; set; }
            public bool isdeleted { get; set; }
            public bool isuserverified { get; set; }
            public bool isdriververified { get; set; }
            public int devicetype { get; set; }
        }

        public class SignupModel
        {
            public string name { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public DateTime accountcreated { get; set; }
            public string notificationtoken { get; set; }
            public int devicetype { get; set; }
        }

        public class SignupResponseModel
        {
            public int userid { get; set; }
        }

        public class SocialSignupModel
        {
            public int name { get; set; }
            public string facebookid { get; set; }
            public string googleid { get; set; }
            public DateTime accountcreated { get; set; }
            public string notificationtoken { get; set; }
            public int devicetype { get; set; }
        }

        public class UserAdminVerifyModel
        {
            public int userid { get; set; }
            public bool isverified { get; set; }
        }

        public class DocumentVerifyModel
        {
            public int documentid { get; set; }
            public bool isverified { get; set; }
            public string comment { get; set; }
        }
    }
}
