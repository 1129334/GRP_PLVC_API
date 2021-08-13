using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class ResponseObject
    {
        public bool isGroupPolicyRights { get; set; }
        public string SessionID { get; set; }
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public string Channel { get; set; }
        public string AgentStatus { get; set; }
        public string AgentType { get; set; }
        public string PartnerName { get; set; }
        public string PartnerIdentifier { get; set; }
        public string emailId { get; set; }
        public string branch { get; set; }
        public int isPosEligible { get; set; }
        public string designation { get; set; }
        public string branchName { get; set; }
        public bool isEnableOTPConfirmation { get; set; }
        public int PosPartner { get; set; }
        public int PosProduct { get; set; }
        public int NonPosProduct { get; set; }
        public int Ulip { get; set; }
        public string DesignationCode { get; set; }
        public int isBIcustSearchMandatory { get; set; }
        public int isPOScustSearchMandatory { get; set; }
        public int isBIotpMandatory { get; set; }
        public int isPOSotpMandatory { get; set; }
        public int isBIthirdPartyKYC { get; set; }
        public int isPOSthirdPartyKYC { get; set; }
        public int isBIstp { get; set; }
        public int isPOSstp { get; set; }
    }

    public class RcrdApp_Login_Response
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public ResponseObject ResponseObject { get; set; }
        
        public LoginStatus LoginStatus { get; set; }
    }

}