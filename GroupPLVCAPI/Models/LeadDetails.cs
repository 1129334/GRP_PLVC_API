using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GroupPLVCAPI.Models
{
    [DataContract]
    public class LeadDetails
    {
        [DataMember]
        public int LeadID { get; set; }

        [DataMember]
        [Required]
        [MaxLength(20)]
        public string LoanAccountNumber { get; set; }

        [DataMember]        
        [MaxLength(20)]
        public string CustomerNo { get; set; }

        [DataMember]
        [Required]
        [MaxLength(20)]
        public string Fname { get; set; }

        [DataMember]        
        [MaxLength(20)]
        public string Mname { get; set; }

        [DataMember]
        [Required]
        [MaxLength(20)]
        public string Lname { get; set; }

        [DataMember]        
        [RegularExpression(@"(^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$)", ErrorMessage = "Date should be in yyyy-MM-dd")]
        public string Dob { get; set; }

        [DataMember]        
        public string EmailId { get; set; }

        [DataMember]       
        public string MobileNumber { get; set; }

        [DataMember]
        [Required]
        public string PanNo { get; set; }

        [DataMember]
        [Required]
        public int? Occupation { get; set; }

        [DataMember]
        [Required]
        public int PlvcLanguage { get; set; }

        [DataMember]
        [Required]
        public bool RevaStatus { get; set; }

        [DataMember]
        [Required]
        public int PlvcStatus { get; set; }

        [DataMember]
        [Required]
        public int ApplicationStatus { get; set; }

        [DataMember]
        [Required]
        public string SourceSystem { get; set; }

        [DataMember]
        [Required]
        public string CreaedBy { get; set; }

        [DataMember]
        [Required]
        public string AgentName { get; set; }

        [DataMember]
        [Required]
        public string Channel { get; set; }

        [DataMember]
        [Required]
        public string PartnerName { get; set; }

    }
}