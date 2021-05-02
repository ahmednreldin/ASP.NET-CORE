using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web; // Response function 

namespace Bookstore.ObjectHelpers.Extensions
{
    static class ObjectExtension
    {
        public static void Dump(this object data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            // Response.Write(json);
            //System.Web.HttpContext.Current.Response.Write("");

            Debug.Write(json);
            //  Response.End();
            //Console.WriteLine(json);
            // System.Diagnostics.Debug.WriteLine("Hello, world");

        }
    }
}
