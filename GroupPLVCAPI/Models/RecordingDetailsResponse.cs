using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class RecordingDetailsResponse
    {
        public Boolean isSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public RecordingDetails RecordingDetails { get; set; }
        public List<Error> ErrorList { get; set; }
    }
}