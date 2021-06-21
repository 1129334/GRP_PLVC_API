using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class LeadDetailsReponse
    {
        public Boolean isSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public List<LeadDetails> lstLeadDetails { get; set; }

        public List<Error> ErrorList { get; set; }
    }
}