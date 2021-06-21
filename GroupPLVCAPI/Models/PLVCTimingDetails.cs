using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class PLVCTimingDetails
    {
        public DateTime? RecordedOn { get; set; }

        public DateTime? ProcessedOn { get; set; }

        public DateTime? SyncedOn { get; set; }

        public string PLVCStatus { get; set; }

        public DateTime PLVCSatusDt { get; set; }
    }
}