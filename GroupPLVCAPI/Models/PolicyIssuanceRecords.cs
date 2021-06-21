using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class PolicyIssuanceRecords
    {
        public string PAN_Number { get; set; }

        public string Unique_Reference_Number { get; set; }

        public string PolicyStatus { get; set; }

        public DateTime? DateOfDisbursement { get; set; }

        public string LoanAccount_Number { get; set; }
    }
}