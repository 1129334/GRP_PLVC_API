using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class BlobDetailsResponse
    {
        public string errorcode { get; set; }
        public string errordescription { get; set; }
        public string blobURL { get; set; }
        public string FileBase64String { get; set; }
        public List<Error> ErrorList { get; set; }
    }
}