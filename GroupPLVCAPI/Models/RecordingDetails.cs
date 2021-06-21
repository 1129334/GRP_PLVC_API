using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class RecordingDetails
    {
        [DataMember]
        [Required]
        public int LeadID { get; set; }

        [DataMember]
        [Required]
        public bool isAccept { get; set; }

        [DataMember]
        [Required]
        public int StatusId { get; set; }

        [DataMember]
        [Required]
        public string Comment { get; set; }

        [DataMember]
        [Required]
        public string CreatedBy { get; set; }
    }
}