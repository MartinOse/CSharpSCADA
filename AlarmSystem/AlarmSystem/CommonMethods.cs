using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AlarmSystem
{
    public static class CommonMethods
    {
        public static string SerializereObject(object value)
        {
            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();

            StringWriter sw = new StringWriter();
            Newtonsoft.Json.JsonTextWriter writer = new Newtonsoft.Json.JsonTextWriter(sw);
            json.Serialize(writer, value);

            string output = sw.ToString();
            return output;
        }
    }
}