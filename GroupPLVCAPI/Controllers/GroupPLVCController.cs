using GroupPLVCAPI.Helper;
using GroupPLVCAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GroupPLVCAPI.Controllers
{
    [RoutePrefix("api/GroupPLVC")]
    public class GroupPLVCController : ApiController
    {
        DAL objDal = new DAL();
        BAL objBAL = new BAL();
        RequestResponseLog requestResponseLog = new RequestResponseLog();
        HttpResponseMessage ObjHttpResponseMesg;

        [HttpGet]
        [CheckAuthorize]
        [Route("FetchLookupDetails/{lookupCode?}")]
        public HttpResponseMessage FetchLookupDetails(string lookupCode)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchLookupDetails(lookupCode));
            requestResponseLog.LeadId = 0;
            requestResponseLog.DataKey = "LookupCode";
            requestResponseLog.DataValue = lookupCode;
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = lookupCode;
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [CheckAuthorize]
        [Route("FetchAllRecordings/{userId?}/{fromDate?}/{lastDate?}/{status?}")]
        public HttpResponseMessage FetchAllRecordings(string userId = "", string fromDate = "", string lastDate = "", string status = "")
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchUserRecording(userId, fromDate, lastDate, status));
            requestResponseLog.LeadId = 0;
            requestResponseLog.DataKey = "userId";
            requestResponseLog.DataValue = userId;
            requestResponseLog.UserId = userId;
            requestResponseLog.Request = string.Concat(userId, fromDate, lastDate, status);
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        #region Record App

        [HttpGet]
        [CheckAuthorize]
        [Route("FetchLanguageScriptMaster/{languageCode?}")]
        public HttpResponseMessage FetchLanguageScriptMaster(string languageCode)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchLanguageScriptMaster(languageCode));
            requestResponseLog.LeadId = 0;
            requestResponseLog.DataKey = "languageCode";
            requestResponseLog.DataValue = languageCode;
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = languageCode;
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [CheckAuthorize]
        [Route("SaveLeadDetails")]
        public HttpResponseMessage SaveLeadDetails(LeadDetails leadDetails)
        {
            LeadDetailsReponse objLeadDetailsReponse = new LeadDetailsReponse();
            if (leadDetails != null)
            {
                requestResponseLog.LeadId = leadDetails.LeadID;
                requestResponseLog.DataKey = "LeadId";
                requestResponseLog.DataValue = Convert.ToString(leadDetails.LeadID);
                requestResponseLog.UserId = leadDetails.CreaedBy;
                requestResponseLog.Request = objBAL.GetJsonRequestString(leadDetails);
                objBAL.SaveRequestResponseLog(ref requestResponseLog);
            }
            if (ModelState.IsValid)
            {
                objLeadDetailsReponse = objBAL.SaveLeadDetails(leadDetails);
            }
            else
            {
                string[] separators = { "," };
                List<Error> errorList = new List<Error>();
                var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).ToList();
                errorList = modelStateErrors.Select(x => new Error { ValidationMessage = x.ErrorMessage, ExceptionMessage = (x.Exception != null ? x.Exception.Message.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0] : string.Empty) }).ToList();

                objLeadDetailsReponse.isSuccess = false;
                objLeadDetailsReponse.ErrorMessage = "";
                objLeadDetailsReponse.ErrorList = errorList;

                if (leadDetails != null)
                {
                    objDal.SaveExceptionLog("LeadID", Convert.ToString(leadDetails.LeadID), "Controller.SaveLeadDetails", 1, JsonConvert.SerializeObject(errorList), "API_GRP_PLVC", Convert.ToString(leadDetails.CreaedBy));
                }
            }

            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objLeadDetailsReponse);
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [CheckAuthorize]
        [Route("SaveBlobDetails")]
        public HttpResponseMessage SaveBlobDetails(BlobDetailsRequest blobDetailsRequest)
        {
            BlobDetailsResponse objBlobDetailsResponse = new BlobDetailsResponse();
            if (blobDetailsRequest != null)
            {
                requestResponseLog.LeadId = blobDetailsRequest.LeadID;
                requestResponseLog.DataKey = "LeadId";
                requestResponseLog.DataValue = Convert.ToString(blobDetailsRequest.LeadID);
                requestResponseLog.UserId = blobDetailsRequest.CreatedBy;
                requestResponseLog.Request = objBAL.GetJsonRequestString(blobDetailsRequest);
                objBAL.SaveRequestResponseLog(ref requestResponseLog);
            }
            if (ModelState.IsValid)
            {
                objBlobDetailsResponse = objBAL.BlobDetailsInsert(blobDetailsRequest);
            }
            else
            {
                string[] separators = { "," };
                List<Error> errorList = new List<Error>();
                var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).ToList();
                errorList = modelStateErrors.Select(x => new Error { ValidationMessage = x.ErrorMessage, ExceptionMessage = (x.Exception != null ? x.Exception.Message.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0] : string.Empty) }).ToList();

                objBlobDetailsResponse.errorcode = "1";
                objBlobDetailsResponse.errordescription = "";
                objBlobDetailsResponse.ErrorList = errorList;

                if (blobDetailsRequest != null)
                {
                    objDal.SaveExceptionLog("LeadID", Convert.ToString(blobDetailsRequest.LeadID), "Controller.SaveBlobDetails", 1, JsonConvert.SerializeObject(errorList), "API_GRP_PLVC", Convert.ToString(blobDetailsRequest.CreatedBy));
                }
            }

            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBlobDetailsResponse);
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [CheckAuthorize]
        [Route("FetchPLVCTimingDetails")]
        public HttpResponseMessage FetchPLVCTimingDetails(int leadId)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchPLVCTimingDetails(leadId));
            requestResponseLog.LeadId = leadId;
            requestResponseLog.DataKey = "LeadId";
            requestResponseLog.DataValue = Convert.ToString(leadId);
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [CheckAuthorize]
        [Route("SaveLoginStatusDetails")]
        public HttpResponseMessage SaveLoginStatusDetails(LoginStatus loginStatus)
        {
            LoginStatusResponse objLoginStatusResponse = new LoginStatusResponse();
            if(loginStatus != null)
            {
                requestResponseLog.LeadId = 0;
                requestResponseLog.DataKey = "AgentToken";
                requestResponseLog.DataValue = Convert.ToString(loginStatus.AgentToken);
                requestResponseLog.UserId = loginStatus.AgentCode;
                requestResponseLog.Request = objBAL.GetJsonRequestString(loginStatus);
                objBAL.SaveRequestResponseLog(ref requestResponseLog);
            }
            if (ModelState.IsValid)
            {
                objLoginStatusResponse = objBAL.SaveLoginStatusDetails(loginStatus);
            }
            else
            {
                string[] separators = { "," };
                List<Error> errorList = new List<Error>();
                var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).ToList();
                errorList = modelStateErrors.Select(x => new Error { ValidationMessage = x.ErrorMessage, ExceptionMessage = (x.Exception != null ? x.Exception.Message.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0] : string.Empty) }).ToList();

                objLoginStatusResponse.isSuccess = false;
                objLoginStatusResponse.ErrorMessage = "";
                objLoginStatusResponse.ErrorList = errorList;

                if (loginStatus != null)
                {
                    objDal.SaveExceptionLog("loginStatus", Convert.ToString(loginStatus.isLoggedIn), "Controller.SaveLoginStatusDetails", 1, JsonConvert.SerializeObject(errorList), "API_GRP_PLVC", Convert.ToString(loginStatus.AgentCode));
                }
            }

            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objLoginStatusResponse);

            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);

            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [CheckAuthorize]
        [Route("FetchLoginStatusDetails")]
        public HttpResponseMessage FetchLoginStatusDetails(string agentToken)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchLoginStatusDetails(agentToken));

            requestResponseLog.LeadId = 0;
            requestResponseLog.DataKey = "AgentToken";
            requestResponseLog.DataValue = Convert.ToString(agentToken);
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);

            return ObjHttpResponseMesg;
        }

        [HttpGet]        
        [Route("GetGeoLocation/{latlng?}/{key?}")]
        public HttpResponseMessage GetGeoLocation(string latlng, string key)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.GetGeoLocation(latlng));

            requestResponseLog.LeadId = 0;
            requestResponseLog.DataKey = "latlng";
            requestResponseLog.DataValue = Convert.ToString(latlng);
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);

            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [Route("RcrdApp_Login")]
        public HttpResponseMessage RcrdApp_Login(RcrdApp_Login_Request rcrdApp_Login_Request)
        {
            if(rcrdApp_Login_Request != null)
            {
                requestResponseLog.LeadId = 0;
                requestResponseLog.DataKey = "AgentCode";
                requestResponseLog.DataValue = Convert.ToString(rcrdApp_Login_Request.AgentCode);
                requestResponseLog.UserId = rcrdApp_Login_Request.AgentCode;
                requestResponseLog.Request = objBAL.GetJsonRequestString(rcrdApp_Login_Request);
                objBAL.SaveRequestResponseLog(ref requestResponseLog);
            }
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.RcrdLogin(rcrdApp_Login_Request));

            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);

            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [CheckAuthorize]
        [Route("LivenessDetection")]
        public HttpResponseMessage LivenessDetection()
        {
            var httpRequests = HttpContext.Current.Request;

            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.BlinkDetection(httpRequests.Files[0], Convert.ToInt32(httpRequests.Form["LeadID"])));

            requestResponseLog.LeadId = Convert.ToInt32(httpRequests.Form["LeadID"]);
            requestResponseLog.DataKey = "LeadId";
            requestResponseLog.DataValue = Convert.ToString(httpRequests.Form["LeadID"]);
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = objBAL.GetJsonRequestString(httpRequests.Files[0]);
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);

            return ObjHttpResponseMesg;
        }

        #endregion

        #region Console App

        [HttpGet]
        [CheckAuthorize]
        [Route("FetchRecordingVideo/{leadId?}")]
        public HttpResponseMessage FetchRecordingVideo(int leadId)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchRecordingVideo(leadId));
            requestResponseLog.LeadId = leadId;
            requestResponseLog.DataKey = "LeadId";
            requestResponseLog.DataValue = Convert.ToString(leadId);
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [CheckAuthorize]
        [Route("SaveRecordingStatusDetails")]
        public HttpResponseMessage SaveRecordingStatusDetails(RecordingDetails recordingDetails)
        {
            RecordingDetailsResponse objRecordingDetailsResponse = new RecordingDetailsResponse();
            if (recordingDetails != null)
            {
                requestResponseLog.LeadId = recordingDetails.LeadID;
                requestResponseLog.DataKey = "LeadId";
                requestResponseLog.DataValue = Convert.ToString(recordingDetails.LeadID);
                requestResponseLog.UserId = recordingDetails.CreatedBy;
                requestResponseLog.Request = objBAL.GetJsonRequestString(recordingDetails);
                objBAL.SaveRequestResponseLog(ref requestResponseLog);
            }
            if (ModelState.IsValid)
            {
                objRecordingDetailsResponse = objBAL.SaveRecordingDetails(recordingDetails);
            }
            else
            {
                string[] separators = { "," };
                List<Error> errorList = new List<Error>();
                var modelStateErrors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors).ToList();
                errorList = modelStateErrors.Select(x => new Error { ValidationMessage = x.ErrorMessage, ExceptionMessage = (x.Exception != null ? x.Exception.Message.Split(separators, StringSplitOptions.RemoveEmptyEntries)[0] : string.Empty) }).ToList();

                objRecordingDetailsResponse.isSuccess = false;
                objRecordingDetailsResponse.ErrorMessage = "";
                objRecordingDetailsResponse.ErrorList = errorList;

                if (recordingDetails != null)
                {
                    objDal.SaveExceptionLog("LeadID", Convert.ToString(recordingDetails.LeadID), "Controller.SaveRecordingStatusDetails", 1, JsonConvert.SerializeObject(errorList), "API_GRP_PLVC", Convert.ToString(recordingDetails.CreatedBy));
                }
            }

            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objRecordingDetailsResponse);
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [CheckAuthorize]
        [Route("SavePolicyIssuanceRecords")]
        public HttpResponseMessage SavePolicyIssuanceRecords()
        {
            PolicyIssuanceRecordsResponse objPolicyIssuanceRecordsResponse = new PolicyIssuanceRecordsResponse();
            string filePath = String.Empty;
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Excel_Upload/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            if (hfc.Count == 0)
            {
                objPolicyIssuanceRecordsResponse.isSuccess = false;
                objPolicyIssuanceRecordsResponse.ErrorMessage = "No File Uploaded";
            }
            else if (hfc.Count == 1)
            {
                System.Web.HttpPostedFile hpf1 = hfc[0];

                if (Path.GetExtension(hpf1.FileName) != ".xlsx")
                {
                    objPolicyIssuanceRecordsResponse.isSuccess = false;
                    objPolicyIssuanceRecordsResponse.ErrorMessage = "File format not allowed";
                }
                else if (Path.GetExtension(hpf1.FileName) == ".xlsx")
                {
                    // CHECK THE FILE COUNT.
                    for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
                    {
                        System.Web.HttpPostedFile hpf = hfc[iCnt];

                        if (hpf.ContentLength > 0)
                        {
                            filePath = sPath + Path.GetFileName(hpf.FileName);

                            // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                            if (!File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                            {
                                // SAVE THE FILES IN THE FOLDER.
                                hpf.SaveAs(filePath);
                                iUploadedCnt = iUploadedCnt + 1;
                            }
                            else
                            {
                                File.Delete(filePath);

                                // SAVE THE FILES IN THE FOLDER.
                                hpf.SaveAs(filePath);
                                iUploadedCnt = iUploadedCnt + 1;
                            }

                            objPolicyIssuanceRecordsResponse = objBAL.SavePolicyIssuanceRecords(filePath);
                        }
                        else
                        {
                            objPolicyIssuanceRecordsResponse.isSuccess = false;
                            objPolicyIssuanceRecordsResponse.ErrorMessage = "File with zero content Uploaded";
                        }
                    }
                }
            }
            
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objPolicyIssuanceRecordsResponse);

            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [CheckAuthorize]
        [Route("DownloadRecordingsExcel/{userId?}/{fromDate?}/{lastDate?}/{status?}")]
        public HttpResponseMessage DownloadRecordingsExcel(string userId = "", string fromDate = "", string lastDate = "", string status = "")
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.CreateExcelReport(userId, fromDate, lastDate, status));
            requestResponseLog.LeadId = 0;
            requestResponseLog.DataKey = "userId";
            requestResponseLog.DataValue = userId;
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [Route("ConsoleApp_Login")]
        public HttpResponseMessage ConsoleApp_Login(string username, string password, string sessionId, string captchaText, string channelType)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.ConsoleLogin(username, password, sessionId, captchaText, channelType));
            requestResponseLog.LeadId = 0;
            requestResponseLog.DataKey = "username";
            requestResponseLog.DataValue = username;
            requestResponseLog.UserId = string.Empty;
            requestResponseLog.Request = String.Concat(username, ";", password, ";", sessionId, ";", captchaText, ";");
            requestResponseLog.Response = objBAL.GetJsonString(ObjHttpResponseMesg);
            objBAL.SaveRequestResponseLog(ref requestResponseLog);
            return ObjHttpResponseMesg;
        }

        #endregion
    }
}