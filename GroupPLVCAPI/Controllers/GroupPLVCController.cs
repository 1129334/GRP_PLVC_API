using GroupPLVCAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupPLVCAPI.Controllers
{
    [RoutePrefix("api/GroupPLVC")]
    public class GroupPLVCController : ApiController
    {
        DAL objDal = new DAL();
        BAL objBAL = new BAL();
        HttpResponseMessage ObjHttpResponseMesg;

        [HttpGet]
        [Route("FetchLookupDetails/{lookupCode?}")]
        public HttpResponseMessage FetchLookupDetails(string lookupCode)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchLookupDetails(lookupCode));
            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [Route("FetchAllRecordings/{userId?}/{fromDate?}/{lastDate?}/{status?}")]
        public HttpResponseMessage FetchAllRecordings(string userId = "", string fromDate = "", string lastDate = "", string status = "")
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchUserRecording(userId, fromDate, lastDate, status));
            return ObjHttpResponseMesg;
        }

        #region Record App

        [HttpGet]
        [Route("FetchLanguageScriptMaster/{languageCode?}")]
        public HttpResponseMessage FetchLanguageScriptMaster(string languageCode)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchLanguageScriptMaster(languageCode));
            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [Route("SaveLeadDetails")]
        public HttpResponseMessage SaveLeadDetails(LeadDetails leadDetails)
        {
            LeadDetailsReponse objLeadDetailsReponse = new LeadDetailsReponse();
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

            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [Route("SaveBlobDetails")]
        public HttpResponseMessage SaveBlobDetails(BlobDetailsRequest blobDetailsRequest)
        {
            BlobDetailsResponse objBlobDetailsResponse = new BlobDetailsResponse();
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

            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [Route("FetchPLVCTimingDetails")]
        public HttpResponseMessage FetchPLVCTimingDetails(int leadId)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchPLVCTimingDetails(leadId));
            return ObjHttpResponseMesg;
        }

        #endregion

        #region Console App

        [HttpGet]
        [Route("FetchRecordingVideo/{leadId?}")]
        public HttpResponseMessage FetchRecordingVideo(int leadId)
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.FetchRecordingVideo(leadId));
            return ObjHttpResponseMesg;
        }

        [HttpPost]
        [Route("SaveRecordingStatusDetails")]
        public HttpResponseMessage SaveRecordingStatusDetails(RecordingDetails recordingDetails)
        {
            RecordingDetailsResponse objRecordingDetailsResponse = new RecordingDetailsResponse();
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

            return ObjHttpResponseMesg;
        }

        [HttpPost]
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
            else
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

            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objPolicyIssuanceRecordsResponse);

            return ObjHttpResponseMesg;
        }

        [HttpGet]
        [Route("DownloadRecordingsExcel/{userId?}/{fromDate?}/{lastDate?}/{status?}")]
        public HttpResponseMessage DownloadRecordingsExcel(string userId = "", string fromDate = "", string lastDate = "", string status = "")
        {
            ObjHttpResponseMesg = Request.CreateResponse(HttpStatusCode.OK, objBAL.CreateExcelReport(userId, fromDate, lastDate, status));
            return ObjHttpResponseMesg;
        }

        #endregion
    }

}