using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GroupPLVCAPI.Models
{
    [DataContract]
    public class LoginStatus
    {
        [DataMember]        
        public string AgentCode { get; set; }

        [DataMember]
        [Required]
        public bool? isLoggedIn { get; set; }

        [DataMember]
        [Required]        
        public string AgentToken { get; set; }

        [DataMember]
        public DateTime TokenExpiryDt { get; set; }
    }
}