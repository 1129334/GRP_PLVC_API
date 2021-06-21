using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class T_LanguageScriptMaster
    {        
        public string Sequence { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string LanguageCode { get; set; }

        public string AudioLink { get; set; }        
    }
}