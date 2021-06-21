using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GroupPLVCAPI.Models
{
    public class DAL
    {
        public string conn = ConfigurationManager.ConnectionStrings["conGRP_PLVC"].ToString();

        public DataTable SaveLeadDetails(LeadDetails leadDetails)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_INSERT_LEAD_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LoanAccountNumber", leadDetails.LoanAccountNumber));
                cmd.Parameters.Add(new SqlParameter("@CustomerNo", leadDetails.CustomerNo));

                cmd.Parameters.Add(new SqlParameter("@Fname", leadDetails.Fname));
                cmd.Parameters.Add(new SqlParameter("@Mname", leadDetails.Mname));
                cmd.Parameters.Add(new SqlParameter("@Lname", leadDetails.Lname));

                cmd.Parameters.Add(new SqlParameter("@Dob", leadDetails.Dob));
                cmd.Parameters.Add(new SqlParameter("@EmailId", leadDetails.EmailId));
                cmd.Parameters.Add(new SqlParameter("@MobileNumber", leadDetails.MobileNumber));

                cmd.Parameters.Add(new SqlParameter("@PanNo", leadDetails.PanNo));

                cmd.Parameters.Add(new SqlParameter("@Occupation", leadDetails.Occupation));
                cmd.Parameters.Add(new SqlParameter("@PlvcLanguage", leadDetails.PlvcLanguage));
                cmd.Parameters.Add(new SqlParameter("@RevaStatus", leadDetails.RevaStatus));
                cmd.Parameters.Add(new SqlParameter("@PlvcStatus", leadDetails.PlvcStatus));

                cmd.Parameters.Add(new SqlParameter("@ApplicationStatus", leadDetails.ApplicationStatus));
                cmd.Parameters.Add(new SqlParameter("@SourceSystem", leadDetails.SourceSystem));

                cmd.Parameters.Add(new SqlParameter("@CreaedBy", leadDetails.CreaedBy));

                cmd.Parameters.Add(new SqlParameter("@AgentName", leadDetails.AgentName));
                cmd.Parameters.Add(new SqlParameter("@Channel", leadDetails.Channel));
                cmd.Parameters.Add(new SqlParameter("@PartnerName", leadDetails.PartnerName));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                SaveExceptionLog("LeadID", Convert.ToString(leadDetails.LeadID), "DAL.SaveLeadDetails", 1, ex.ToString(), "API_GRP_PLVC", leadDetails.CreaedBy);
            }

            return dt;
        }

        public DataTable SaveBlobDetails(BlobDetailsRequest blobDetailsRequest)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_INSERT_BLOB_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LeadID", blobDetailsRequest.LeadID));
                cmd.Parameters.Add(new SqlParameter("@DataKey", blobDetailsRequest.DataKey));

                cmd.Parameters.Add(new SqlParameter("@Container", blobDetailsRequest.Container));
                cmd.Parameters.Add(new SqlParameter("@Subfolder", blobDetailsRequest.Subfolder));

                cmd.Parameters.Add(new SqlParameter("@FileName", blobDetailsRequest.FileName));
                cmd.Parameters.Add(new SqlParameter("@FileExtension", blobDetailsRequest.FileExtension));                
                cmd.Parameters.Add(new SqlParameter("@FilePath", blobDetailsRequest.blobURL));

                cmd.Parameters.Add(new SqlParameter("@isBlobUploaded", blobDetailsRequest.isblobuploaded));
                cmd.Parameters.Add(new SqlParameter("@DetectedAddress", blobDetailsRequest.detectedaddress));
                cmd.Parameters.Add(new SqlParameter("@LanguageCode", blobDetailsRequest.languagecode));

                cmd.Parameters.Add(new SqlParameter("@SourceSystem", blobDetailsRequest.source));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", blobDetailsRequest.CreatedBy));

                cmd.Parameters.Add(new SqlParameter("@AppVersion", blobDetailsRequest.AppVersion));
                cmd.Parameters.Add(new SqlParameter("@Longitude", blobDetailsRequest.Longitude));
                cmd.Parameters.Add(new SqlParameter("@Latitude", blobDetailsRequest.Latitude));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                SaveExceptionLog("LeadID", Convert.ToString(blobDetailsRequest.LeadID), "DAL.SaveBlobDetails", 1, ex.ToString(), "API_GRP_PLVC", blobDetailsRequest.CreatedBy);
            }
                    
            return dt;
        }
       
        public DataTable FetchLanguageScipt(string languageCode)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_LANGUAGE_SCRIPT]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LanguageCode", languageCode));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch(Exception ex)
            {
                SaveExceptionLog("languageCode", languageCode, "DAL.FetchLanguageScipt", 1, ex.ToString(), "API_GRP_PLVC", string.Empty);
            }
           
            return dt;
        }

        public DataTable FetchUserRecording(string userId, string fromDate, string lastDate, string status)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
           
            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_USER_RECORDING]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@FromDate", fromDate));
                cmd.Parameters.Add(new SqlParameter("@LastDate", lastDate));
                cmd.Parameters.Add(new SqlParameter("@Status", status));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("userId", userId, "DAL.FetchUserRecording", 1, ex.ToString(), "API_GRP_PLVC", userId);
            }

            return dt;
        }

        public DataTable FetchUserRecording_Excel(string userId, string fromDate, string lastDate, string status)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_DOWNLOAD_USER_RECORDING_EXCEL]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@UserID", userId));
                cmd.Parameters.Add(new SqlParameter("@FromDate", fromDate));
                cmd.Parameters.Add(new SqlParameter("@LastDate", lastDate));
                cmd.Parameters.Add(new SqlParameter("@Status", status));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("userId", userId, "DAL.FetchUserRecording_Excel", 1, ex.ToString(), "API_GRP_PLVC", userId);
            }

            return dt;
        }

        public void SaveExceptionLog(string dataKey, string dataValue, string stage, int errorCode, string errorMsg, string sourceSystem, string createdBy)
        {
            ExceptionalDetail exceptionalDetail = new ExceptionalDetail();
            exceptionalDetail.DataKey = dataKey;
            exceptionalDetail.DataValue = dataValue;

            exceptionalDetail.Stage = stage;
            exceptionalDetail.ErrorCode = errorCode;
            exceptionalDetail.ErrorMsg = errorMsg;

            exceptionalDetail.SourceSystem = sourceSystem;
            exceptionalDetail.CreatedBy = createdBy;

            SaveExceptionLogDB(exceptionalDetail);
        }

        public DataTable SaveExceptionLogDB(ExceptionalDetail exceptionalDetail)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
          
            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_INSERT_EXCEPTION_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@DataKey", exceptionalDetail.DataKey));
                cmd.Parameters.Add(new SqlParameter("@DataValue", exceptionalDetail.DataValue));

                cmd.Parameters.Add(new SqlParameter("@Stage", exceptionalDetail.Stage));
                cmd.Parameters.Add(new SqlParameter("@ErrorCode", exceptionalDetail.ErrorCode));
                cmd.Parameters.Add(new SqlParameter("@ErrorMsg", exceptionalDetail.ErrorMsg));


                cmd.Parameters.Add(new SqlParameter("@SourceSystem", exceptionalDetail.SourceSystem));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", exceptionalDetail.CreatedBy));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable FetchLookupDetails(string lookUpCode)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_LOOKUP_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LOOKUPCODE", lookUpCode));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("lookUpCode", lookUpCode, "DAL.FetchLookupDetails", 1, ex.ToString(), "API_GRP_PLVC", string.Empty);
            }

            return dt;
        }

        public DataTable FetchPLVCTimingDetails(int leadId)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_PLVCTIMING_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadId));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("leadId", Convert.ToString(leadId), "DAL.FetchPLVCTimingDetails", 1, ex.ToString(), "API_GRP_PLVC", string.Empty);
            }

            return dt;
        }

        public DataTable SaveRecordingDetails(RecordingDetails recordingDetails)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_INSERT_RECORDING_STATUS_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LeadID", recordingDetails.LeadID));
                cmd.Parameters.Add(new SqlParameter("@isAccept", recordingDetails.isAccept));
                cmd.Parameters.Add(new SqlParameter("@RejectStatus", recordingDetails.StatusId));
                cmd.Parameters.Add(new SqlParameter("@RejectComment", recordingDetails.Comment));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", recordingDetails.CreatedBy));
                
                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("LeadID", Convert.ToString(recordingDetails.LeadID), "DAL.SaveRecordingDetails", 1, ex.ToString(), "API_GRP_PLVC", recordingDetails.CreatedBy);
            }

            return dt;
        }

        public DataTable FetchBlobVideoDetails(int leadID, int dataKey)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_BLOB_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadID));
                cmd.Parameters.Add(new SqlParameter("@DataKey", dataKey));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("leadId", Convert.ToString(leadID), "DAL.FetchBlobVideoDetails", 1, ex.ToString(), "API_GRP_PLVC", string.Empty);
            }

            return dt;
        }

        public DataTable UpdateBlobFetched(int leadID, int dataKey, Boolean isBlobFetched)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_UPDATE_BLOB_DETAILS]", sqlconn);
                cmd.Parameters.Add(new SqlParameter("@LeadID", leadID));
                cmd.Parameters.Add(new SqlParameter("@DataKey", dataKey));
                cmd.Parameters.Add(new SqlParameter("@isBlobFetched", isBlobFetched));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("leadId", Convert.ToString(leadID), "DAL.UpdateBlobFetched", 1, ex.ToString(), "API_GRP_PLVC", string.Empty);
            }

            return dt;
        }

        public DataTable SavePolicyIssuanceRecords(DataTable dtPolIssuance)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_INSERT_POLICY_STATUS_DETAILS]", sqlconn);

                SqlParameter parameter;
                parameter = cmd.Parameters.AddWithValue("@TempTable", dtPolIssuance);
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.PolicyStatusUpload";//table which is created in SQL(User-Defined-Data-Type
               
                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("dtPolIssuance", String.Empty, "DAL.SavePolicyIssuanceRecords", 1, ex.ToString(), "API_GRP_PLVC", string.Empty);
            }

            return dt;
        }
        
    }
}