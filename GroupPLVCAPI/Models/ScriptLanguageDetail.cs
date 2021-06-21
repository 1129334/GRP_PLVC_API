using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class ScriptLanguageDetail
    {
        public string LanguageCode { get; set; }
        public List<T_LanguageScriptMaster> QuestionList { get; set; }
        public string MasterZipFilePath { get; set; }
    }
}