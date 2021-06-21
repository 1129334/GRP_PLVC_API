using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class ExceptionalDetail
    {
        public string DataKey { get; set; }

        public string DataValue { get; set; }

        public string Stage { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMsg { get; set; }

        public string SourceSystem { get; set; }

        public string CreatedBy { get; set; }
    }
}