using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{

    public class DeviceDetails
    {
        public string AppName { get; set; }
        public string Device { get; set; }
        public bool IsTablet { get; set; }
        public bool IsMobileDevice { get; set; }
        public string VersionCode { get; set; }
        public string VersionName { get; set; }
        public string IPAddress { get; set; }
        public string DeviceID { get; set; }
        public string DeviceManifacturer { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceType { get; set; }
        public string DeviceVersion { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PinCode { get; set; }
        public string AppID { get; set; }
    }

    public class RcrdApp_Login_Request
    {
        public string Source { get; set; }
        public string AgentPassword { get; set; }
        public string AgentCode { get; set; }
        public string LoginType { get; set; }
        public DeviceDetails DeviceDetails { get; set; }
    }
}