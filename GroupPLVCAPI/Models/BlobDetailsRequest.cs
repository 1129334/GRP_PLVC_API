using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class BlobDetailsRequest
    {
        [DataMember]
        [Required]
        public int LeadID { get; set; }

        [DataMember]
        [Required]
        public int DataKey { get; set; }

        [DataMember]
        [Required]
        public string Container { get; set; }
        [DataMember]
        [Required]
        public string Subfolder { get; set; }

        [DataMember]
        [Required]
        public string FileName { get; set; }
        [DataMember]
        [Required]
        public string FileExtension { get; set; }

        [DataMember]
        [Required]      
        public string FileBase64 { get; set; }

        public string blobURL { get; set; }

        [DataMember]
        [Required]
        public bool isblobuploaded { get; set; }

        [DataMember]
        public string detectedaddress { get; set; }

        [DataMember]
        [Required]
        public string languagecode { get; set; }

        [DataMember]
        [Required]
        public string AppVersion { get; set; }

        [DataMember]
        [Required]
        public string Latitude { get; set; }

        [DataMember]
        [Required]
        public string Longitude { get; set; }

        [DataMember]
        [Required]
        public string source { get; set; }

        [DataMember]
        [Required]
        public string CreatedBy { get; set; }        
    }
}