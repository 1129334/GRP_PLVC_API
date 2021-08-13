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

                cmd.Parameters.Add(new SqlParameter("@QuestionID", blobDetailsRequest.QuestionID));

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

        public DataTable SaveLoginStatusDetails(LoginStatus loginStatus)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_INSERT_LOGIN_STATUS]", sqlconn);

                cmd.Parameters.Add(new SqlParameter("@AgentCode", loginStatus.AgentCode));
                cmd.Parameters.Add(new SqlParameter("@isLoggedIn", loginStatus.isLoggedIn));
                cmd.Parameters.Add(new SqlParameter("@AgentToken", loginStatus.AgentToken));

                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("loginStatus", Convert.ToString(loginStatus.isLoggedIn), "DAL.SaveLoginStatusDetails", 1, ex.ToString(), "API_GRP_PLVC", loginStatus.AgentCode);
            }

            return dt;
        }

        public DataTable FetchLoginStatusDetails(string agentToken)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_LOGIN_STATUS]", sqlconn);

                cmd.Parameters.Add(new SqlParameter("@AgentToken", agentToken));                

                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("AgentToken", String.Empty, "DAL.FetchLoginStatusDetails", 1, ex.ToString(), "API_GRP_PLVC", agentToken);
            }

            return dt;
        }

        internal void SaveRequestResponseLog(ref RequestResponseLog requestResponseLog)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[USP_GRP_PLVC_INSERTUPDATEREQUESTRESPONSE]", sqlconn);
                SqlParameter sqlParameter = new SqlParameter("@SRNO", requestResponseLog.SrNo);
                sqlParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(sqlParameter);
                cmd.Parameters.Add(new SqlParameter("@LEADID", requestResponseLog.LeadId));
                cmd.Parameters.Add(new SqlParameter("@DATAKEY", requestResponseLog.DataKey));
                cmd.Parameters.Add(new SqlParameter("@DATAVALUE", requestResponseLog.DataValue));
                cmd.Parameters.Add(new SqlParameter("@REQUEST", requestResponseLog.Request));
                cmd.Parameters.Add(new SqlParameter("@RESPONSE", requestResponseLog.Response));
                cmd.Parameters.Add(new SqlParameter("@USERID", requestResponseLog.UserId));
                cmd.CommandType = CommandType.StoredProcedure;
                sqlconn.Open();
                cmd.ExecuteNonQuery();
                requestResponseLog.SrNo = Convert.ToInt32(sqlParameter.Value);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("LeadId", Convert.ToString(requestResponseLog.LeadId), "DAL.SaveRequestResponseLog", 1, ex.ToString(), "API_GRP_PLVC", requestResponseLog.UserId);
            }
            finally
            {
                sqlconn.Close();
            }
        }


        public DataTable FetchConsoleLoginStatusDetails(string agentCode, string password, string channelType)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_CONSOLE_LOGIN_STATUS]", sqlconn);

                cmd.Parameters.Add(new SqlParameter("@AgentCode", agentCode));
                cmd.Parameters.Add(new SqlParameter("@Password", password));
                cmd.Parameters.Add(new SqlParameter("@ChannelType", channelType));

                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("AgentCode", String.Empty, "DAL.FetchConsoleLoginStatusDetails", 1, ex.ToString(), "API_GRP_PLVC", agentCode);
            }

            return dt;
        }

        public DataTable FetchBlinkDetetcionStatusDetails(int leadID)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_FETCH_BLINK_DETECTION_STATUS]", sqlconn);

                cmd.Parameters.Add(new SqlParameter("@LeadID", leadID));                

                cmd.CommandType = CommandType.StoredProcedure;

                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("leadID", Convert.ToString(leadID), "DAL.FetchBlinkDetetcionStatusDetails", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return dt;
        }

        public DataTable SaveBlinkDetectionStatus(int leadID, Boolean status, string message)
        {
            DataTable dtresult = new DataTable();

            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                cmd = new SqlCommand("[usp_GRP_PLVC_INSERT_BLINK_DETECTION_STATUS]", sqlconn);

                cmd.Parameters.Add(new SqlParameter("@LeadID", leadID));
                cmd.Parameters.Add(new SqlParameter("@BlinkDetect_Status", status));
                cmd.Parameters.Add(new SqlParameter("@BlinkDetect_Message", message));

                cmd.CommandType = CommandType.StoredProcedure;
                da.SelectCommand = cmd;
                da.Fill(dt);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                SaveExceptionLog("LeadID", Convert.ToString(leadID), "DAL.SaveLeadDetails", 1, ex.ToString(), "API_GRP_PLVC", String.Empty);
            }

            return dt;
        }
    }
}