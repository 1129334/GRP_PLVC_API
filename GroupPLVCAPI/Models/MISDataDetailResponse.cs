using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class MISDataDetailResponse
    {
        public Boolean isSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public List<MISData> lstMISData { get; set; }
    }
}