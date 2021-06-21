using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class PLVCTimingDetailResponse
    {
        public Boolean isSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public PLVCTimingDetails PLVCTimingDetails { get; set; }
    }
}