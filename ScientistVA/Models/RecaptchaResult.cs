using Azure.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace ScientistVA.Models
{
    [DataContract]
    public class RecaptchaResult
    {
        //attribute có tên trùng với field của json trả về
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        //attribute có tên trùng với field của json trả về
        [DataMember(Name = "error-codes")]
        public string[] ErrorCodes { get; set; }
       
    }
}