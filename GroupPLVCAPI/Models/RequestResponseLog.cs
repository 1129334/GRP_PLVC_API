using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class RequestResponseLog
    {
        private int intSrNo;

        public int SrNo
        {
            get { return intSrNo; }
            set { intSrNo = value; }
        }

        private int intLeadId;

        public int LeadId
        {
            get { return intLeadId; }
            set { intLeadId = value; }
        }

        private string strDataKey;

        public string DataKey
        {
            get { return strDataKey; }
            set { strDataKey = value; }
        }

        private string strDataValue;

        public string DataValue
        {
            get { return strDataValue; }
            set { strDataValue = value; }
        }

        private string strRequest;

        public string Request
        {
            get { return strRequest; }
            set { strRequest = value; }
        }

        private string strResponse;

        public string Response
        {
            get { return strResponse; }
            set { strResponse = value; }
        }

        private string strUserId;

        public string UserId
        {
            get { return strUserId; }
            set { strUserId = value; }
        }

    }
}