using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static GroupPLVCAPI.Models.PolicyIssuanceRecords;

namespace GroupPLVCAPI.Models
{
    public class MISData
    {
        public LeadDetails LeadDetails { get; set; }
        public BlobDetailsRequest BlobDetailsRequest { get; set; }
        public PLVCTimingDetails PLVCTimingDetails { get; set; }
        public PolicyIssuanceRecords PolicyIssuanceRecords { get; set; }
        public RecordingDetails RecordingDetails { get; set; }


        public string OccupationDecription { get; set; }
        public string PlvcLanguageDescription { get; set; }
        public string PlvcStatusDecription { get; set; }
        public string ApplicationStatusDecription { get; set; }

        public DateTime? BlobDetailsRequest_CreatedDate { get; set; }

        public string RecordingDetails_StatusDescription { get; set; }
        public DateTime? RecordingDetails_CreatedDate { get; set; }
    }
}