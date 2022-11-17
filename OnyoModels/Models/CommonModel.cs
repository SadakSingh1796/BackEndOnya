using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public class CustomDateTimeConverter : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                //Don't implement this unless you're going to use the custom converter for serialization too
                throw new NotImplementedException();
            }
        }
    }
}
