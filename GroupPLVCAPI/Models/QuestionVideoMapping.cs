using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class QuestionVideoMapping
    {
        public int QuestionID { get; set; }
        public string VirtualPath { get; set; }

        public DateTime CreatedDt { get; set; }
        public DateTime? ModifiedDt { get; set; }
    }
}