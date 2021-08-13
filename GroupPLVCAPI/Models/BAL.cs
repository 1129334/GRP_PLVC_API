using Newtonsoft.Json;
using OfficeOpenXml;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class BAL
    {
        DAL objDal = new DAL();

        public LookupDetailsReponse FetchLookupDetails(string lookUpCode)
        {
            LookupDetailsReponse objLookupDetailsReponse = new LookupDetailsReponse();
            List<Lookup> lstLookups = new List<Lookup>();

            try
            {
                DataTable dtRecording = objDal.FetchLookupDetails(lookUpCode);
                Utility.ConvertDataTable(dtRecording, ref lstLookups);

                objLookupDetailsReponse.isSuccess = true;
                objLookupDetailsReponse.ErrorMessage = String.Empty;
                objLookupDetailsReponse.lstLookUp = lstLookups;
            }
            catch (Exception ex)
            {
                objLookupDetailsReponse.isSuccess = false;
                objLookupDetailsReponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("lookUpCode", lookUpCode, "BAL.FetchLookupDetails", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return objLookupDetailsReponse;
        }

        public MISDataDetailResponse FetchUserRecording(string userId, string fromDate, string lastDate, string status)
        {
            MISDataDetailResponse objMISDataDetailResponse = new MISDataDetailResponse();
            List<MISData> lstMISData = new List<MISData>();

            try
            {
                DataTable dtRecording = objDal.FetchUserRecording(userId, fromDate, lastDate, status);

                foreach (DataRow dtRow in dtRecording.Rows)
                {
                    MISData objMISData = new MISData();

                    LeadDetails objLeadDetails = new LeadDetails();
                    BlobDetailsRequest objBlobDetailsRequest = new BlobDetailsRequest();
                    PLVCTimingDetails objPLVCTimingDetails = new PLVCTimingDetails();
                    PolicyIssuanceRecords objPolicyIssuanceRecords = new PolicyIssuanceRecords();
                    RecordingDetails objRecordingDetails = new RecordingDetails();

                    objLeadDetails.LeadID = Convert.ToInt32(dtRow["LeadID"]);
                    objLeadDetails.LoanAccountNumber = Convert.ToString(dtRow["LoanAccountNumber"]);
                    objLeadDetails.CustomerNo = Convert.ToString(dtRow["CustomerNo"]);
                    objLeadDetails.Fname = Convert.ToString(dtRow["Fname"]);
                    objLeadDetails.Mname = Convert.ToString(dtRow["Mname"]);
                    objLeadDetails.Lname = Convert.ToString(dtRow["Lname"]);
                    objLeadDetails.Dob = Convert.ToString(dtRow["Dob"]);
                    objLeadDetails.EmailId = Convert.ToString(dtRow["EmailId"]);
                    objLeadDetails.MobileNumber = Convert.ToString(dtRow["MobileNumber"]);
                    objLeadDetails.PanNo = Convert.ToString(dtRow["PanNo"]);
                    objLeadDetails.Occupation = Convert.ToInt32(dtRow["Occupation"]);
                    objLeadDetails.PlvcLanguage = Convert.ToInt32(dtRow["PlvcLanguage"]);
                    objLeadDetails.RevaStatus = Convert.ToBoolean(dtRow["RevaStatus"]);
                    objLeadDetails.PlvcStatus = Convert.ToInt32(dtRow["PlvcStatus"]);
                    objLeadDetails.ApplicationStatus = Convert.ToInt32(dtRow["ApplicationStatus"]);
                    objLeadDetails.SourceSystem = Convert.ToString(dtRow["SourceSystem"]);
                    objLeadDetails.CreaedBy = Convert.ToString(dtRow["CreaedBy"]);

                    objLeadDetails.PartnerName = Convert.ToString(dtRow["PartnerName"]);
                    objLeadDetails.AgentName = Convert.ToString(dtRow["AgentName"]);
                    objLeadDetails.Channel = Convert.ToString(dtRow["Channel"]);

                    objMISData.OccupationDecription = Convert.ToString(dtRow["OccupationDecription"]);
                    objMISData.PlvcLanguageDescription = Convert.ToString(dtRow["PlvcLanguageDescription"]);
                    objMISData.PlvcStatusDecription = Convert.ToString(dtRow["PlvcStatusDecription"]);
                    objMISData.ApplicationStatusDecription = Convert.ToString(dtRow["ApplicationStatusDecription"]);


                    objBlobDetailsRequest.LeadID = Convert.ToInt32(dtRow["LeadID"]);
                    objBlobDetailsRequest.DataKey = Convert.ToInt32(dtRow["DataKey"]);
                    objBlobDetailsRequest.Container = Convert.ToString(dtRow["Container"]);
                    objBlobDetailsRequest.Subfolder = Convert.ToString(dtRow["Subfolder"]);
                    objBlobDetailsRequest.FileName = Convert.ToString(dtRow["FileName"]);
                    objBlobDetailsRequest.FileExtension = Convert.ToString(dtRow["FileExtension"]);
                    objBlobDetailsRequest.blobURL = Convert.ToString(dtRow["FilePath"]);
                    objBlobDetailsRequest.isblobuploaded = Convert.ToBoolean(dtRow["isblobuploaded"]);
                    objBlobDetailsRequest.detectedaddress = Convert.ToString(dtRow["Detectedaddress"]);
                    objBlobDetailsRequest.languagecode = Convert.ToString(dtRow["LanguageCode"]);
                    objBlobDetailsRequest.AppVersion = Convert.ToString(dtRow["AppVersion"]);
                    objBlobDetailsRequest.Latitude = Convert.ToString(dtRow["Latitude"]);
                    objBlobDetailsRequest.Longitude = Convert.ToString(dtRow["Longitude"]);
                    objBlobDetailsRequest.source = Convert.ToString(dtRow["SourceSystem"]);
                    objBlobDetailsRequest.CreatedBy = Convert.ToString(dtRow["CreatedByBlob"]);
                    objMISData.BlobDetailsRequest_CreatedDate = (!String.IsNullOrEmpty(Convert.ToString(dtRow["CreatedDateBlob"])) ? Convert.ToDateTime(dtRow["CreatedDateBlob"]) : (DateTime?)null);


                    objRecordingDetails.LeadID = Convert.ToInt32(dtRow["LeadID"]);
                    objRecordingDetails.isAccept = Convert.ToBoolean(dtRow["isAccept"]);
                    objRecordingDetails.StatusId = Convert.ToInt32(dtRow["RejectStatus"]);
                    objRecordingDetails.Comment = Convert.ToString(dtRow["RejectComment"]);
                    objRecordingDetails.CreatedBy = Convert.ToString(dtRow["CreatedByRecord"]);
                    objMISData.RecordingDetails_CreatedDate = (!String.IsNullOrEmpty(Convert.ToString(dtRow["CreatedDateRecord"])) ? Convert.ToDateTime(dtRow["CreatedDateRecord"]) : (DateTime?)null);

                    objMISData.RecordingDetails_StatusDescription = Convert.ToString(dtRow["StatusDescription"]);


                    objPolicyIssuanceRecords.PAN_Number = Convert.ToString(dtRow["PAN_Number"]);
                    objPolicyIssuanceRecords.PolicyStatus = Convert.ToString(dtRow["PolicyStatus"]);
                    objPolicyIssuanceRecords.DateOfDisbursement = (!String.IsNullOrEmpty(Convert.ToString(dtRow["DateOfDisbursement"])) ? Convert.ToDateTime(dtRow["DateOfDisbursement"]) : (DateTime?)null);
                    objPolicyIssuanceRecords.LoanAccount_Number = Convert.ToString(dtRow["LoanAccount_Number"]);

                    objPLVCTimingDetails.RecordedOn = (!String.IsNullOrEmpty(Convert.ToString(dtRow["RecordedOn"])) ? Convert.ToDateTime(dtRow["RecordedOn"]) : (DateTime?)null);
                    objPLVCTimingDetails.ProcessedOn = (!String.IsNullOrEmpty(Convert.ToString(dtRow["ProcessedOn"])) ? Convert.ToDateTime(dtRow["ProcessedOn"]) : (DateTime?)null);
                    objPLVCTimingDetails.SyncedOn = (!String.IsNullOrEmpty(Convert.ToString(dtRow["SyncedOn"])) ? Convert.ToDateTime(dtRow["SyncedOn"]) : (DateTime?)null);
                    objPLVCTimingDetails.PLVCSatusDt = Convert.ToDateTime(dtRow["PLVCSatusDt"]);
                    objPLVCTimingDetails.PLVCStatus = Convert.ToString(dtRow["PlvcStatusDecription"]);


                    objMISData.LeadDetails = objLeadDetails;
                    objMISData.BlobDetailsRequest = objBlobDetailsRequest;
                    objMISData.PLVCTimingDetails = objPLVCTimingDetails;
                    objMISData.PolicyIssuanceRecords = objPolicyIssuanceRecords;
                    objMISData.RecordingDetails = objRecordingDetails;

                    lstMISData.Add(objMISData);
                }

                objMISDataDetailResponse.isSuccess = true;
                objMISDataDetailResponse.ErrorMessage = String.Empty;
                objMISDataDetailResponse.lstMISData = lstMISData;

            }
            catch (Exception ex)
            {
                objMISDataDetailResponse.isSuccess = false;
                objMISDataDetailResponse.ErrorMessage = ex.Message;

                if (!String.IsNullOrEmpty(userId)) { objDal.SaveExceptionLog("userId", userId, "BAL.FetchUserRecording", 1, ex.ToString(), "API_GRP_PLVC", userId); }
            }

            return objMISDataDetailResponse;
        }

        public bool ValidateAgentToken(string agentToken)
        {
            //3.Check UserToken is valid
            bool result = true;

            LoginStatusResponse objLoginStatusResponse = FetchLoginStatusDetails(agentToken);

            if (objLoginStatusResponse.LoginStatus.isLoggedIn == false || objLoginStatusResponse.LoginStatus.TokenExpiryDt < DateTime.Now)
            {
                result = false;
            }

            return result;
        }

        #region Record App

        public LanguageScriptMasterResponse FetchLanguageScriptMaster(string languageCode)
        {
            LanguageScriptMasterResponse objLanguageScriptMasterResponse = new LanguageScriptMasterResponse();
            List<ScriptLanguageDetail> lstScriptLanguageDetail = new List<ScriptLanguageDetail>();
            try
            {
                DataTable dtMaster = objDal.FetchLanguageScipt(languageCode);

                List<T_LanguageScriptMaster> lstT_LanguageScriptMaster = new List<T_LanguageScriptMaster>();
                Utility.ConvertDataTable(dtMaster, ref lstT_LanguageScriptMaster);

                List<String> distinctlanguageCode = lstT_LanguageScriptMaster.Select(x => x.LanguageCode).Distinct().ToList();

                foreach (string languagecodeStr in distinctlanguageCode)
                {
                    ScriptLanguageDetail objScriptLanguageDetail = new ScriptLanguageDetail();

                    objScriptLanguageDetail.LanguageCode = languagecodeStr;
                    objScriptLanguageDetail.QuestionList = lstT_LanguageScriptMaster.Where(x => x.LanguageCode == languagecodeStr).ToList();
                    objScriptLanguageDetail.MasterZipFilePath = ConfigurationManager.AppSettings["MasterZipFilePath"] + languagecodeStr + ".zip";

                    lstScriptLanguageDetail.Add(objScriptLanguageDetail);
                }

                objLanguageScriptMasterResponse.isSuccess = true;
                objLanguageScriptMasterResponse.ErrorMessage = String.Empty;
                objLanguageScriptMasterResponse.lstScriptLanguageDetail = lstScriptLanguageDetail;
            }
            catch (Exception ex)
            {
                objLanguageScriptMasterResponse.isSuccess = false;
                objLanguageScriptMasterResponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("languageCode", languageCode, "BAL.FetchLanguageScriptMaster", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return objLanguageScriptMasterResponse;
        }

        public LeadDetailsReponse SaveLeadDetails(LeadDetails leadDetails)
        {
            LeadDetailsReponse objLeadDetailsReponse = new LeadDetailsReponse();
            List<LeadDetails> lstLeadDetails = new List<LeadDetails>();

            try
            {
                DataTable dtInsert = objDal.SaveLeadDetails(leadDetails);

                if (dtInsert != null && dtInsert.Rows.Count > 0)
                {
                    //Set LeadId from DB
                    leadDetails.LeadID = Convert.ToInt32(dtInsert.Rows[0]["LeadID"]);

                    objLeadDetailsReponse.isSuccess = true;
                    objLeadDetailsReponse.ErrorMessage = String.Empty;
                    lstLeadDetails.Add(leadDetails);
                    objLeadDetailsReponse.lstLeadDetails = lstLeadDetails;
                }
                else
                {
                    objLeadDetailsReponse.isSuccess = false;
                    objLeadDetailsReponse.ErrorMessage = "T_LeadDetails Table Insert Failed";
                }
            }
            catch (Exception ex)
            {
                objLeadDetailsReponse.isSuccess = false;
                objLeadDetailsReponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("LeadID", Convert.ToString(leadDetails.LeadID), "BAL.SaveLeadDetails", 1, ex.ToString(), "API_GRP_PLVC", leadDetails.CreaedBy);
            }

            return objLeadDetailsReponse;
        }

        public BlobDetailsResponse BlobDetailsInsert(BlobDetailsRequest blobDetailsRequest)
        {
            BlobDetailsResponse objBlobDetailsResponse = new BlobDetailsResponse();
            string bloburl = "";

            try
            {
                //5.Check for 5 second video + Blink Detection is successfull
                if (blobDetailsRequest.DataKey == 3)
                {
                    DataTable dtBlinkDetection = objDal.FetchBlinkDetetcionStatusDetails(blobDetailsRequest.LeadID);

                    if (dtBlinkDetection != null && dtBlinkDetection.Rows.Count > 0)
                    {
                        string errorCode = Convert.ToString(dtBlinkDetection.Rows[0]["BlinkDetect_Status"]);

                        if (errorCode.ToUpper() == "TRUE")
                        {
                            objBlobDetailsResponse.errorcode = "1";
                            objBlobDetailsResponse.errordescription = "Liveness Detection is not complete for this case.";

                            return objBlobDetailsResponse;
                        }
                    }
                }

                //Save File in Shared Folder
                string vkycpdflocation = ConfigurationManager.AppSettings["SharedFolderlocation"].ToString();
                vkycpdflocation = vkycpdflocation + blobDetailsRequest.LeadID + "\\";

                string pdf = blobDetailsRequest.FileBase64.Split(',')[1].ToString();
                byte[] arr = Convert.FromBase64String(pdf);

                if (!Directory.Exists(vkycpdflocation))
                {
                    Directory.CreateDirectory(vkycpdflocation);
                }
                File.WriteAllBytes(vkycpdflocation + blobDetailsRequest.FileName, arr);

                //Upload File in Blob
                bloburl = BlobVideoStorageUpload(blobDetailsRequest);

                if (!String.IsNullOrEmpty(bloburl))
                {
                    blobDetailsRequest.isblobuploaded = true;
                    blobDetailsRequest.blobURL = bloburl;
                }
                else
                {
                    blobDetailsRequest.isblobuploaded = false;
                    blobDetailsRequest.blobURL = String.Empty;
                }

                try
                {
                    //Update DB Entries
                    objDal.SaveBlobDetails(blobDetailsRequest);

                    objBlobDetailsResponse.errorcode = "0";
                    objBlobDetailsResponse.errordescription = String.Empty;
                    objBlobDetailsResponse.blobURL = bloburl;
                }
                catch (Exception ex)
                {
                    objBlobDetailsResponse.errorcode = "1";
                    objBlobDetailsResponse.errordescription = ex.Message;
                    objBlobDetailsResponse.blobURL = bloburl;

                    objDal.SaveExceptionLog("LeadID", Convert.ToString(blobDetailsRequest.LeadID), "BAL.BlobDetailsInsert_Table_Insert", 1, ex.ToString(), "API_GRP_PLVC", blobDetailsRequest.CreatedBy);
                }
            }
            catch (Exception ex)
            {
                objBlobDetailsResponse.errorcode = "1";
                objBlobDetailsResponse.errordescription = ex.Message;

                objDal.SaveExceptionLog("LeadID", Convert.ToString(blobDetailsRequest.LeadID), "BAL.BlobDetailsInsert_File_Save", 1, ex.ToString(), "API_GRP_PLVC", blobDetailsRequest.CreatedBy);
            }

            return objBlobDetailsResponse;
        }

        public string BlobVideoStorageUpload(BlobDetailsRequest blobDetailsRequest)
        {
            string result = "";
            BlobStoragePutResponse blobStoragePutResponse = new BlobStoragePutResponse();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BlobStorageAPI"].ToString() + "PutBlobStorage");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    DateTime now = DateTime.Today;

                    string inputJson = JsonConvert.SerializeObject(new
                    {
                        applicationNumber = blobDetailsRequest.LeadID.ToString(),
                        containName = blobDetailsRequest.Container,
                        subFolder = blobDetailsRequest.Subfolder,
                        fileName = blobDetailsRequest.FileName,
                        fileBytes = blobDetailsRequest.FileBase64.Split(',')[1].ToString()
                    });

                    streamWriter.Write(inputJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                blobStoragePutResponse = JsonConvert.DeserializeObject<BlobStoragePutResponse>(result);
            }
            catch (Exception ex)
            {
                objDal.SaveExceptionLog("LeadID", Convert.ToString(blobDetailsRequest.LeadID), "BAL.BlobVideoStorageUpload", 1, ex.ToString(), "API_GRP_PLVC", blobDetailsRequest.CreatedBy);
            }

            return blobStoragePutResponse.filePath;
        }

        public PLVCTimingDetailResponse FetchPLVCTimingDetails(int leadId)
        {
            PLVCTimingDetailResponse objPLVCTimingDetailResponse = new PLVCTimingDetailResponse();
            List<PLVCTimingDetails> lstPLVCTimingDetails = new List<PLVCTimingDetails>();

            try
            {
                DataTable dtRecords = objDal.FetchPLVCTimingDetails(leadId);
                Utility.ConvertDataTable(dtRecords, ref lstPLVCTimingDetails);

                objPLVCTimingDetailResponse.isSuccess = true;
                objPLVCTimingDetailResponse.ErrorMessage = String.Empty;
                objPLVCTimingDetailResponse.PLVCTimingDetails = lstPLVCTimingDetails.FirstOrDefault();
            }
            catch (Exception ex)
            {
                objPLVCTimingDetailResponse.isSuccess = false;
                objPLVCTimingDetailResponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("leadId", Convert.ToString(leadId), "BAL.FetchPLVCTimingDetails", 1, ex.ToString(), "API_GRP_PLVC", string.Empty);
            }

            return objPLVCTimingDetailResponse;
        }

        public LoginStatusResponse SaveLoginStatusDetails(LoginStatus loginStatus)
        {
            LoginStatusResponse objLoginStatusResponse = new LoginStatusResponse();
            List<LoginStatus> lstLoginStatus = new List<LoginStatus>();

            try
            {
                DataTable dtInsert = objDal.SaveLoginStatusDetails(loginStatus);
                Utility.ConvertDataTable(dtInsert, ref lstLoginStatus);

                if (dtInsert != null && dtInsert.Rows.Count > 0)
                {
                    objLoginStatusResponse.isSuccess = true;
                    objLoginStatusResponse.ErrorMessage = String.Empty;

                    objLoginStatusResponse.LoginStatus = lstLoginStatus.FirstOrDefault();
                }
                else
                {
                    objLoginStatusResponse.isSuccess = false;
                    objLoginStatusResponse.ErrorMessage = "T_LoginStatus Table Insert Failed";
                }
            }
            catch (Exception ex)
            {
                objLoginStatusResponse.isSuccess = false;
                objLoginStatusResponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("loginStatus", Convert.ToString(loginStatus.isLoggedIn), "BAL.SaveLoginStatusDetails", 1, ex.ToString(), "API_GRP_PLVC", loginStatus.AgentToken);
            }

            return objLoginStatusResponse;
        }


        public LoginStatusResponse FetchLoginStatusDetails(String agentToken)
        {
            LoginStatusResponse objLoginStatusResponse = new LoginStatusResponse();
            List<LoginStatus> lstLoginStatus = new List<LoginStatus>();

            try
            {
                DataTable dtInsert = objDal.FetchLoginStatusDetails(agentToken);
                Utility.ConvertDataTable(dtInsert, ref lstLoginStatus);

                if (dtInsert != null && dtInsert.Rows.Count > 0)
                {
                    objLoginStatusResponse.isSuccess = true;
                    objLoginStatusResponse.ErrorMessage = String.Empty;

                    objLoginStatusResponse.LoginStatus = lstLoginStatus.FirstOrDefault();
                }
                else
                {
                    objLoginStatusResponse.isSuccess = false;
                    objLoginStatusResponse.ErrorMessage = "T_LoginStatus Table Fetch Failed";
                }
            }
            catch (Exception ex)
            {
                objLoginStatusResponse.isSuccess = false;
                objLoginStatusResponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("AgentToken", String.Empty, "BAL.FetchLoginStatusDetails", 1, ex.ToString(), "API_GRP_PLVC", agentToken);
            }

            return objLoginStatusResponse;
        }

        public GoogleGeoLocationResponse GetGeoLocation(string latlng)
        {
            GoogleGeoLocationResponse googleGeoLocationResponse = new GoogleGeoLocationResponse();

            string googleMapUrl = ConfigurationManager.AppSettings["googleMapUrl"].ToString();
            string googleMapUrlkey = ConfigurationManager.AppSettings["googleMapUrlKey"].ToString();

            string URL = googleMapUrl + "latlng=" + latlng + "&key=" + googleMapUrlkey;

            try
            {
                WebRequest request = HttpWebRequest.Create(URL);

                WebResponse response = request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());

                string responseText = reader.ReadToEnd();

                googleGeoLocationResponse = JsonConvert.DeserializeObject<GoogleGeoLocationResponse>(responseText);
            }
            catch (Exception ex)
            {
                objDal.SaveExceptionLog("latlng", Convert.ToString(latlng), "BAL.GetGeoLocation", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return googleGeoLocationResponse;
        }

        public RcrdApp_Login_Response RcrdLogin(RcrdApp_Login_Request rcrdApp_Login_Request)
        {
            RcrdApp_Login_Response objRcrdApp_Login_Response = new RcrdApp_Login_Response();
            LoginStatus objLoginStatus = new LoginStatus();

            var client = new RestClient(ConfigurationManager.AppSettings["Rcrd_Login_API_Url"].ToString());
            string jsonBody = JsonConvert.SerializeObject(rcrdApp_Login_Request);

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response != null)
                {
                    objRcrdApp_Login_Response = JsonConvert.DeserializeObject<RcrdApp_Login_Response>(response.Content);

                    if (objRcrdApp_Login_Response.IsSuccess)
                    {
                        //1.Save Login Status Details in Db                       
                        objLoginStatus.AgentCode = objRcrdApp_Login_Response.ResponseObject.AgentCode;
                        objLoginStatus.AgentToken = Guid.NewGuid().ToString().Replace("-", "");
                        objLoginStatus.TokenExpiryDt = DateTime.Now.AddDays(1);
                        objLoginStatus.isLoggedIn = true;

                        objRcrdApp_Login_Response.LoginStatus = objLoginStatus;

                        objDal.SaveLoginStatusDetails(objLoginStatus);
                    }
                }
            }
            catch (Exception ex)
            {
                objRcrdApp_Login_Response.IsSuccess = false;
                objRcrdApp_Login_Response.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("AgentCode", Convert.ToString(rcrdApp_Login_Request.AgentCode), "BAL.RcrdLogin", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return objRcrdApp_Login_Response;
        }

        public BlinkDetectionResponse BlinkDetection(HttpPostedFile postedFile, int leadID)
        {
            DataTable dataTable = new DataTable();
            BlinkDetectionResponse blinkDetectionResponse = new BlinkDetectionResponse();

            try
            {
                string binkDetectionAPI = ConfigurationManager.AppSettings["BlinkDetectionAPI"].ToString();
                string _classificationKey = ConfigurationManager.AppSettings["ClassificationKey"].ToString();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(postedFile.InputStream))
                {
                    fileData = binaryReader.ReadBytes(postedFile.ContentLength);
                }
                ServicePointManager.Expect100Continue = true;

                string contentType = "multipart/form-data";
                string filename = "1.jpeg";
                var webClient = new WebClient();
                string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
                webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
                var fileDatas = webClient.Encoding.GetString(fileData);
                var package = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", boundary, filename, contentType, fileDatas);

                var nfile = webClient.Encoding.GetBytes(package);

                byte[] respbyte = webClient.UploadData(binkDetectionAPI, "POST", nfile);

                string resp = Encoding.ASCII.GetString(respbyte);

                blinkDetectionResponse = JsonConvert.DeserializeObject<BlinkDetectionResponse>(resp);

                //4.Save Reponse in Db
                objDal.SaveBlinkDetectionStatus(leadID, blinkDetectionResponse.errorcode == "1" ? true : false, blinkDetectionResponse.errormessage);
            }
            catch (Exception ex)
            {
                blinkDetectionResponse.errorcode = "1";
                blinkDetectionResponse.errormessage = "Exception - " + ex.Message;

                objDal.SaveExceptionLog("FileName", Convert.ToString(postedFile.FileName), "BAL.BlinkDetection", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }
            return blinkDetectionResponse;
        }

        #endregion

        #region Console App

        public BlobDetailsResponse FetchRecordingVideo(int leadId)
        {
            BlobDetailsResponse objBlobDetailsResponse = new BlobDetailsResponse();
            List<BlobDetailsRequest> lstBlobDetailsRequest = new List<BlobDetailsRequest>();
            List<QuestionVideoMapping> questionVideoMappings = new List<QuestionVideoMapping>();

            try
            {
                DataTable dtVideoDetails = objDal.FetchBlobVideoDetails(leadId, 2);

                if (dtVideoDetails != null && dtVideoDetails.Rows.Count > 0)
                {
                    //Utility.ConvertDataTable(dtVideoDetails, ref lstBlobDetailsRequest);

                    //string blobURL = BlobVideoStorageDownload(lstBlobDetailsRequest.FirstOrDefault());

                    //if (!String.IsNullOrEmpty(blobURL))
                    //{
                    //    //Update isBlobFetched
                    //    objDal.UpdateBlobFetched(leadId, 2, true);

                    //    objBlobDetailsResponse.errorcode = "0";
                    //    objBlobDetailsResponse.errordescription = String.Empty;
                    //    objBlobDetailsResponse.blobURL = blobURL;
                    //}
                    //else
                    //{
                    //    //Update isBlobFetched
                    //    objDal.UpdateBlobFetched(leadId, 2, false);

                    //    string virtualPath = ConfigurationManager.AppSettings["VirtualFolderlocation"] + "//" + lstBlobDetailsRequest.FirstOrDefault().LeadID + "//" + lstBlobDetailsRequest.FirstOrDefault().FileName;

                    //    objBlobDetailsResponse.errorcode = "0";
                    //    objBlobDetailsResponse.errordescription = String.Empty;
                    //    objBlobDetailsResponse.blobURL = virtualPath;
                    //}

                    foreach (DataRow dtRow in dtVideoDetails.Rows)
                    {
                        QuestionVideoMapping objQuestionVideoMapping = new QuestionVideoMapping();
                        objQuestionVideoMapping.VirtualPath = ConfigurationManager.AppSettings["VirtualFolderlocation"] + "//" + Convert.ToString(dtRow["LeadID"]) + "//" + Convert.ToString(dtRow["FileName"]);
                        objQuestionVideoMapping.QuestionID = Convert.ToInt32(dtRow["QuestionID"]);

                        objQuestionVideoMapping.CreatedDt = Convert.ToDateTime(dtRow["CreatedDate"]);
                        objQuestionVideoMapping.ModifiedDt = (!String.IsNullOrEmpty(Convert.ToString(dtRow["ModifiedDate"])) ? Convert.ToDateTime(dtRow["ModifiedDate"]) : (DateTime?)null);

                        questionVideoMappings.Add(objQuestionVideoMapping);
                    }

                    objBlobDetailsResponse.errorcode = "0";
                    objBlobDetailsResponse.errordescription = String.Empty;
                    objBlobDetailsResponse.lstQuestionVideoMapping = questionVideoMappings;
                }
                else
                {
                    objBlobDetailsResponse.errorcode = "1";
                    objBlobDetailsResponse.errordescription = "No row found in T_PlvcBlobDetails";
                }
            }
            catch (Exception ex)
            {
                objBlobDetailsResponse.errorcode = "1";
                objBlobDetailsResponse.errordescription = ex.Message;

                objDal.SaveExceptionLog("LeadID", Convert.ToString(leadId), "BAL.SaveRecordingDetails", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return objBlobDetailsResponse;
        }

        public string BlobVideoStorageDownload(BlobDetailsRequest blobDetailsRequest)
        {
            string video64String = string.Empty;
            string result = "";
            BlobStoragePutResponse blobStoragePutResponse = new BlobStoragePutResponse();

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["BlobStorageAPI"].ToString() + "GetBlobStorage");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    DateTime now = DateTime.Today;

                    string inputJson = JsonConvert.SerializeObject(new
                    {
                        applicationNumber = blobDetailsRequest.LeadID.ToString(),
                        containName = blobDetailsRequest.Container,
                        subFolder = blobDetailsRequest.Subfolder,
                        fileName = blobDetailsRequest.FileName
                    });

                    streamWriter.Write(inputJson);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                blobStoragePutResponse = JsonConvert.DeserializeObject<BlobStoragePutResponse>(result);

                video64String = blobStoragePutResponse.filePath;

                //if (blobStoragePutResponse != null)
                //{                    
                //    using (var client = new WebClient())
                //    {
                //        client.DownloadFile(blobStoragePutResponse.filePath, ConfigurationManager.AppSettings["LocalFilePath"] + blobDetailsRequest.FileName);

                //        byte[] documentByteArray = File.ReadAllBytes(ConfigurationManager.AppSettings["LocalFilePath"] + blobDetailsRequest.FileName);

                //        video64String = Convert.ToBase64String(documentByteArray);
                //    }
                //}
            }
            catch (Exception ex)
            {
                objDal.SaveExceptionLog("LeadID", Convert.ToString(blobDetailsRequest.LeadID), "BAL.BlobVideoStorageDownload", 1, ex.ToString(), "API_GRP_PLVC", blobDetailsRequest.CreatedBy);
            }

            return video64String;
        }

        public RecordingDetailsResponse SaveRecordingDetails(RecordingDetails recordingDetails)
        {
            RecordingDetailsResponse objRecordingDetailsResponse = new RecordingDetailsResponse();

            try
            {
                DataTable dtInsert = objDal.SaveRecordingDetails(recordingDetails);

                if (dtInsert != null && dtInsert.Rows.Count > 0)
                {
                    objRecordingDetailsResponse.isSuccess = true;
                    objRecordingDetailsResponse.ErrorMessage = String.Empty;
                    objRecordingDetailsResponse.RecordingDetails = recordingDetails;
                }
                else
                {
                    objRecordingDetailsResponse.isSuccess = false;
                    objRecordingDetailsResponse.ErrorMessage = "T_RecordingStatusDetails Table Insert Failed";
                }
            }
            catch (Exception ex)
            {
                objRecordingDetailsResponse.isSuccess = true;
                objRecordingDetailsResponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("LeadID", Convert.ToString(recordingDetails.LeadID), "BAL.SaveRecordingDetails", 1, ex.ToString(), "API_GRP_PLVC", recordingDetails.CreatedBy);
            }

            return objRecordingDetailsResponse;
        }

        public PolicyIssuanceRecordsResponse SavePolicyIssuanceRecords(string fileName)
        {
            PolicyIssuanceRecordsResponse objPolicyIssuanceRecordsResponse = new PolicyIssuanceRecordsResponse();

            try
            {
                //Convert to DataTable
                DataTable dtRecords = GetDataTableFromExcel(fileName);

                //Insert DataTable in Db
                DataTable dtInsertRcrds = objDal.SavePolicyIssuanceRecords(dtRecords);

                if (dtInsertRcrds != null && dtInsertRcrds.Rows.Count > 0)
                {
                    objPolicyIssuanceRecordsResponse.isSuccess = true;
                    objPolicyIssuanceRecordsResponse.ErrorMessage = String.Empty;
                    objPolicyIssuanceRecordsResponse.NoOfCountInserted = dtRecords.Rows.Count;
                }
                else
                {
                    objPolicyIssuanceRecordsResponse.isSuccess = false;
                    objPolicyIssuanceRecordsResponse.ErrorMessage = "No Records Inserted";
                    objPolicyIssuanceRecordsResponse.NoOfCountInserted = dtInsertRcrds.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                objPolicyIssuanceRecordsResponse.isSuccess = false;
                objPolicyIssuanceRecordsResponse.ErrorMessage = ex.Message;
            }

            return objPolicyIssuanceRecordsResponse;
        }

        public DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }

        public DownloadReportResponse CreateExcelReport(string userId, string fromDate, string lastDate, string status)
        {
            DownloadReportResponse objDownloadReportResponse = new DownloadReportResponse();

            try
            {
                DataTable dtRecords = objDal.FetchUserRecording_Excel(userId, fromDate, lastDate, status);
                var fileName = DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                String strNewPath1 = System.Web.Hosting.HostingEnvironment.MapPath("~/Excel_Download/") + fileName;
                String virtualFolderPath = ConfigurationManager.AppSettings["DnVirtualPath"].ToString();

                FileInfo fileInfo = new FileInfo(strNewPath1);

                using (ExcelPackage pck = new ExcelPackage(fileInfo))
                {
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Group_PLVC_Details");
                    ws.Cells["A1"].LoadFromDataTable(dtRecords, true, OfficeOpenXml.Table.TableStyles.Custom);
                    pck.Save();
                }

                objDownloadReportResponse.isSuccess = true;
                objDownloadReportResponse.ErrorMessage = String.Empty;
                objDownloadReportResponse.FilePath = virtualFolderPath + fileName;
            }
            catch (Exception ex)
            {
                objDownloadReportResponse.isSuccess = false;
                objDownloadReportResponse.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("userId", userId, "BAL.CreateExcelReport", 1, ex.ToString(), "API_GRP_PLVC", userId);
            }

            return objDownloadReportResponse;
        }

        public RcrdApp_Login_Response ConsoleLogin(string username, string password, string sessionId, string captchaText, string channelType)
        {
            RcrdApp_Login_Response objRcrdApp_Login_Response = new RcrdApp_Login_Response();
            LoginStatus objLoginStatus = new LoginStatus();

            try
            {
                //CAPTCHA VALDIATION
                if (Base64Decode(sessionId) == captchaText)
                {
                    //2.Console Login Implementation
                    DataTable dtRecords = objDal.FetchConsoleLoginStatusDetails(username, Base64Decode(password), channelType);

                    if (dtRecords != null && dtRecords.Rows.Count > 0)
                    {
                        String result = Convert.ToString(dtRecords.Rows[0]["RESULT"]);

                        if (result == "PASSWORD_IS_CORRECT")
                        {
                            objRcrdApp_Login_Response.IsSuccess = true;
                            objRcrdApp_Login_Response.ErrorMessage = result;

                            //1.Save Login Status Details in Db
                            objLoginStatus.AgentCode = username;
                            objLoginStatus.AgentToken = Guid.NewGuid().ToString().Replace("-", "");
                            objLoginStatus.TokenExpiryDt = DateTime.Now.AddDays(1);
                            objLoginStatus.isLoggedIn = true;

                            objRcrdApp_Login_Response.LoginStatus = objLoginStatus;

                            objDal.SaveLoginStatusDetails(objLoginStatus);
                        }
                        else if (result == "INVALID_PASSWORD")
                        {
                            objRcrdApp_Login_Response.IsSuccess = false;
                            objRcrdApp_Login_Response.ErrorMessage = result;
                        }
                        else if (result == "INVALID_AGENT_CODE")
                        {
                            objRcrdApp_Login_Response.IsSuccess = false;
                            objRcrdApp_Login_Response.ErrorMessage = result;
                        }
                    }
                    else
                    {
                        objRcrdApp_Login_Response.IsSuccess = false;
                        objRcrdApp_Login_Response.ErrorMessage = "NO_DATA_ROW";
                    }
                }
                else
                {
                    objRcrdApp_Login_Response.IsSuccess = false;
                    objRcrdApp_Login_Response.ErrorMessage = "CAPTCHA_DID_NOT_MATCH";
                }
            }
            catch (Exception ex)
            {
                objRcrdApp_Login_Response.IsSuccess = false;
                objRcrdApp_Login_Response.ErrorMessage = ex.Message;

                objDal.SaveExceptionLog("Username", username, "BAL.ConsoleLogin", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return objRcrdApp_Login_Response;
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion

        #region Application RequestResponse Log

        internal string GetJsonString(HttpResponseMessage ObjHttpResponseMesg)
        {
            string jsonString = string.Empty;
            try
            {
                jsonString = ObjHttpResponseMesg.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
            }
            return jsonString;
        }

        internal string GetJsonRequestString(object objectValue)
        {
            string jsonString = string.Empty;
            try
            {
                jsonString = JsonConvert.SerializeObject(objectValue);
            }
            catch (Exception ex)
            {
            }
            return jsonString;
        }

        public void SaveRequestResponseLog(ref RequestResponseLog requestResponseLog)
        {
            try
            {
                objDal.SaveRequestResponseLog(ref requestResponseLog);
            }
            catch (Exception ex)
            {
                objDal.SaveExceptionLog("LeadID", Convert.ToString(requestResponseLog.LeadId), "BAL.SaveRequestResponseLog", 1, ex.ToString(), "API_GRP_PLVC", requestResponseLog.UserId);
            }
        }

        #endregion

    }
}