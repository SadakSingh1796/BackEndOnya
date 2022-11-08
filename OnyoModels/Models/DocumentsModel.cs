﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnyaModels
{
    public class DocumentsModel
    {
        public class AddDocumentRequestModel
        {
            public int userid { get; set; }
            public string profile { get; set; }
            public string passport { get; set; }
            public string license { get; set; }
            public string governmentid { get; set; }
        }

        public class DocumentModel
        {
            public int documentid { get; set; }
            public int userid { get; set; }
            public string profile { get; set; }
            public string passport { get; set; }
            public string license { get; set; }
            public string governmentid { get; set; }
            public bool isverified { get; set; }
        }

        public class UserDocument
        {
            public string documentType { get; set; }
            public string documentValue { get; set; }
        }
    }
}