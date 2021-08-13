using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class LoginStatusResponse
    {
        public Boolean isSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public LoginStatus LoginStatus { get; set; }
        public List<Error> ErrorList { get; set; }
    }
}