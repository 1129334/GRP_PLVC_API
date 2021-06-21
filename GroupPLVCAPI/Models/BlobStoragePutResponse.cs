using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class BlobStoragePutResponse
    {
        public string erroCode { set; get; }
        public string errorDesc { set; get; }
        public string filePath { set; get; }
    }
}