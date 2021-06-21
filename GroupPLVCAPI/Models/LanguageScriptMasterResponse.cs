using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class LanguageScriptMasterResponse
    {
        public Boolean isSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public List<ScriptLanguageDetail> lstScriptLanguageDetail { get; set; }
    }
}