using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class Lookup
    {
        private int intLookupId;

        public int LookupId
        {
            get { return intLookupId; }
            set { intLookupId = value; }
        }

        private string strLookupCode;

        public string LookupCode
        {
            get { return Utility.ValidateString(strLookupCode); }
            set { strLookupCode = value; }
        }

        private string strLookupDesc;

        public string LookupDesc
        {
            get { return Utility.ValidateString(strLookupDesc); }
            set { strLookupDesc = value; }
        }

        private int intOrderBy;

        public int OrderBy
        {
            get { return intOrderBy; }
            set { intOrderBy = value; }
        }
    }
}