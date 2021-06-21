using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class Error
    {
        public string ValidationMessage { get; set; }
        public string ExceptionMessage { get; set; }
    }
}