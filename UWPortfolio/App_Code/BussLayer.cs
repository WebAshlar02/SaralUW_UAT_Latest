/*
**********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: ADDED AUTO COMMENT FEATURE IN EXISTING SARAL AND PRODUCTIVITY
DateTime :09JAN17
*********************************************************************************************************************************
***********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: ADDED update is verified flag of pan and aadhar
DateTime :16JAN17
*********************************************************************************************************************************
 ************************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: ADDED TO MAINTAIN USER LOGS 
DateTime :16JAN17
*********************************************************************************************************************************
************************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :ROSHAN LOHAR
METHODE/EVENT:BUSSINESS LAYER
REMARK: ADDED ReportData function
DateTime :23JAN18
*********************************************************************************************************************************
************************************************************************************************************************************
COMMENT ID: 5
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: ADDED ReportData function
DateTime :23JAN18
*********************************************************************************************************************************
************************************************************************************************************************************
COMMENT ID: 6
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: fetch sum assured of user 
DateTime :23JAN18
*********************************************************************************************************************************
************************************************************************************************************************************
COMMENT ID: 7
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: insert policy printing 
DateTime :07 May 18
*********************************************************************************************************************************
 **************************************************************************************************************************************
COMMENT ID: 8
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT:BUSSINESS LAYER
REMARK: Fetch Question for close file review tab
DateTime :11APRL2018
*********************************************************************************************************************************
 **************************************************************************************************************************************
COMMENT ID: 9
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT:SaveAnswer_CloseFile LAYER
REMARK: Save answer to database
DateTime :11APRL2018
*********************************************************************************************************************************
 **************************************************************************************************************************************
COMMENT ID: 10
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT:Save_MedicalReport_Reason LAYER
REMARK: Save medical reason to database
DateTime :11APRL2018
*********************************************************************************************************************************
*  **************************************************************************************************************************************
COMMENT ID: 12
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: FETCH CATEGORY FOR MEDICAL DASHBOARD
DateTime :26FEB18
*********************************************************************************************************************************
 *************************************************************************************************************************************
COMMENT ID: 13
COMMENTOR NAME : SHRIJEET SHANIL
METHODE/EVENT: CREATE UTILITY TO SET PRIORITY OF CASES
REMARK: 
DateTime 01MAR19
*********************************************************************************************************************************
*********************************************************************************************************************************
 Sr. No.              : 14
 Company              : Life            
 Module               : UW Saral         
 Program Author       : Sushant Devkate - MFL00905
 BRD/CR/Codesk No/Win :  CR-2639
 Date Of Creation     : 04/04/2022
 Description          : To Fetch the FSAR limit
*********************************************************************************************************************************
//******************************************************************************************************************************* 
// Sr. No.              : 15
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Royson Pinto      
// BRD/CR/Codesk No/Win : CR-4783  
// Date Of Creation     : 22-11-2022            
// Description          : Create a Restrict Further Cover Option
//*******************************************************************************************************************************
//******************************************************************************************************************************* 
// Sr. No.              : 16
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Royson Pinto      
// BRD/CR/Codesk No/Win : CR-4783  
// Date Of Creation     : 17-02-2023            
// Description          : Create a Restrict Further Cover Option
//*******************************************************************************************************************************
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using UWSaralObjects;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for BussLayer
/// </summary>
public class BussLayer
{
    Commfun objComm = new Commfun();
    DataSet _ds = new DataSet();
    DataSet _dsFollowup = null;
    DataSet _dsPol = null;
    DataSet _dsAml = null;
    DataSet _dsPreIssueVal = null;
    DataSet _dsLoading = null;
    DataSet _dsUWApproval = null;
    DataSet _dsUWIssuence = null;
    DataSet _dsUWDasbordCount = null;

    public BussLayer()
    {

    }


    public void OnlineApplicationDashborddetails_GET(ref DataSet _ds, string strUwmode, string struseID, string strUserGroup, string strlnkOption)
    {
        objComm.OnlineApplicationdashbord_Get(ref _ds, strUwmode, struseID, strUserGroup, strlnkOption);
    }

    public void OnlineApplicationDashborddetails_GET(string strUwmode, string struseID, string strUserGroup, string strlnkOption, ref DataSet _ds)
    {
        objComm.OnlineApplicationdashbord_Get(strUwmode, struseID, strUserGroup, strlnkOption, ref _ds);
    }

    public void OnlineApplicationDisplayDetails_GET(ref DataSet _ds, string strAppno, string strApptype)
    {
        objComm.OnlineApplicationDisplayDetails_GET(ref _ds, strAppno, strApptype);
    }

    public void OnlinAppLockMechanisamDetails_GET(string strApplicationNumber, string struserid, string strAssignTo, string strOptionKey, ref int Result)
    {
        objComm.UpdateControlMechanism(strApplicationNumber, struserid, strAssignTo, strOptionKey, ref Result);
    }

    public void OnlineApplicationdashbordBucketsCountDynamic_Get(ref DataSet _dsUWDasbordCount, string strUwmode, string struserid, string strUserGroup)
    {
        objComm.OnlineDynamicdashbordCount_Get(ref _dsUWDasbordCount, strUwmode, struserid, strUserGroup);
    }

    public void OnlineApplicationUWResultDynamicAppChecker_Get(string strApplicationNumber, ref string response, string strUwmode, string struserid, string strUserGroup)
    {
        objComm.OnlineDynamicUWResult_Get(strApplicationNumber, ref response, strUwmode, struserid, strUserGroup);
    }

    public void OnlineApplicationdashbordBucketsCount_Get(string strUwmode, string struserid, string strLinkOption, string strUserGroup, ref DataSet _dsUWDasbordCount)
    {
        objComm.OnlineApplicationdashbordBucketsCount_Get(strUwmode, struserid, strLinkOption, strUserGroup, ref _dsUWDasbordCount);
    }

    public void OnlineApplicationdashbordBucketsCount_Get(ref DataSet _dsUWDasbordCount, string strUwmode, string struserid, string strLinkOption, string strUserGroup)
    {
        objComm.OnlineApplicationdashbordBucketsCount_Get(ref _dsUWDasbordCount, strUwmode, struserid, strLinkOption, strUserGroup);
    }
    public void FollowupDetails_Save(string strAppType, string strApplicationNoStr, string strFollowUpCode, string strDescription, string strCategory,
string strCriteria, string strIsLetterPrintRequired, string strRaisedBy, string strStatus, string strClassification, string strLifeType, string strBpm_userID,
string strBpm_userName, string strBpmgrp, string strBpm_branchCode, string strBpm_userBranch, string strBpm_processName, string strBpm_applicationName, ref int FollowupResult)
    {
        try
        {
            objComm.OnlinefollowupDetails_Save(strAppType, strApplicationNoStr, strFollowUpCode, strDescription, strCategory,
strCriteria, strIsLetterPrintRequired, strRaisedBy, strStatus, strClassification, strLifeType, strBpm_userID,
strBpm_userName, strBpmgrp, strBpm_branchCode, strBpm_userBranch, strBpm_processName, strBpm_applicationName, ref FollowupResult);
        }
        catch (Exception Err)
        {

        }
    }

    public void Afi_Cfi_GET(ref DataSet _dsUWDasbordCount, string strApplicationNo, string strPolicyNo)
    {
        objComm.AfiCfi_GET(ref _dsUWDasbordCount, strApplicationNo, strPolicyNo);
    }

    /*ADDED BY SHRI*/
    public void ClientBasicInfo_GET(ref DataSet _dsClientInfo, string strApplnNo)
    {
        objComm.ClientBasicInfo_GET(ref _dsClientInfo, strApplnNo);
    }

    public void ClientFullInfo_GET(ref DataSet _dsClientInfo, string strClientId, string strApplnNo, string strType)
    {
        objComm.ClientFullInfo(ref _dsClientInfo, strClientId, strApplnNo, strType);
    }

    public void UpdateClientBasicInfo(UWSaralObjects.ClientDetails objClientDetails, string strOldClientId, ref int intRetVal)
    {
        objComm.ClientFullInfo_Save(objClientDetails, strOldClientId, ref intRetVal);
    }

    public void ValidateRedundantClientOnApplication(UWSaralObjects.ClientDetails objClientDetails, string strOldClientId, ref int intRetVal)
    {
        objComm.ValidateRedundantClientOnApplication(objClientDetails, strOldClientId, ref intRetVal);
    }
    public void DedupeSearch_GET(ref DataSet _ds, string strFirstName, string strLastName, string strDob, char chGender, string strPincode)
    {
        objComm.OnlineServiceDeduptClientSearch_GET(ref _ds, strFirstName, strLastName, strDob, chGender, strPincode);
    }

    public void DedupeDetails_GET(ref DataSet _ds, string strClientId)
    {
        objComm.OnlineServiceDeduptClientDetails_GET(ref _ds, strClientId);
    }
    /*END HERE*/
    public void UpdatePolicyStatus(string strApplicationNumber, string strPolicyStatus, ref int intRetVal)
    {
        objComm.UpdatePolicyStatus(strApplicationNumber, strPolicyStatus, ref intRetVal);
    }

    public void FetchPendingDocs(ref DataSet _ds, string strApplicationNo)
    {
        objComm.FetchPendingDocs(ref _ds, strApplicationNo);
    }
    public void InsertUpdateLeadDocument(string LeadDocumentId, string FileName, string FilePath, string ModifiedBy, bool IsReceived, bool IsVerified, string LeadID, string DocProofId, string DocTypeId, string UWStatus, ref int Result)
    {
        objComm.InsertUpdateLeadDocument(LeadDocumentId, FileName, FilePath, ModifiedBy, IsReceived, IsVerified, LeadID, DocProofId, DocTypeId, UWStatus, ref Result);
    }

    public void AMLDetails_Get(ref DataSet _ds, string strApplicationNo, string strAppType)
    {
        objComm.AMLDetails_Get(ref _ds, strApplicationNo, strAppType);
    }
    public void FetchPendingDocsOffline(ref DataSet _ds, string strApplicationNo)
    {
        objComm.FetchPendingDocsOffline(ref _ds, strApplicationNo);
    }

    public void ManageOfflineDocument(string strApplicationNumber, string strDocProof, string strDocTypeId, string strAssuredType, string strUserID, string strUserGroup
                            , string strUserBranch, string strProcessName
                            , string strApplicationName)
    {
        objComm.ManageOfflineDocument(strApplicationNumber, strDocProof, strDocTypeId, strAssuredType, strUserID, strUserGroup
                            , strUserBranch, strProcessName
                            , strApplicationName);
    }

    /*added by shri on 04 sep to fetch aml docs for service call*/
    public void AMLDetailsOffline_Get(ref DataSet _ds, string strApplicationNo)
    {
        objComm.FetchAMLOfflineDocument(ref _ds, strApplicationNo);
    }

    /*ADDED BY SHRI TO ADD USER ID */
    public void UpdatePolicyStatus(string strApplicationNumber, string strPolicyStatus, string strUserId, ref int intRetVal)
    {
        objComm.UpdatePolicyStatus(strApplicationNumber, strPolicyStatus, strUserId, ref intRetVal);
    }

    /*added by shri on 27 oct 17 to fetch application details from search*/
    //23NOV2017
    public void GetApplicationDetails(string strApplicationNo, string strUserId, string strChanneltype, ref DataSet _ds)
    {
        objComm.GetApplicationDetails(strApplicationNo, strUserId, strChanneltype, ref _ds);
    }
    //23NOV2017 END

    /*added by shri on 27 10 to to fetch users list*/
    public void FetchReferUser(string strApplicationNo, string strChannel, ref DataSet _ds)
    {
        objComm.FetchReferUser(strApplicationNo, strChannel, ref _ds);
    }
    /*added by shri on 27 10 to to fetch users list based on condition*/
    public void FetchReferUser(string strApplicationNo, string strChannel, string strAssignType, string strUserId, ref DataSet _ds)
    {
        objComm.FetchReferUser(strApplicationNo, strChannel, strAssignType, strUserId, ref _ds);
    }
    /*added by shri on 21 12 to fetch user list */

    public void ManageReferUser(string strApplicationNo, string strChannel, string strReferTo, string strReferFrom, string strType, ref int intRef)
    {
        objComm.ManageReferUser(strApplicationNo, strChannel, strReferTo, strReferFrom, strType, ref intRef);
    }
    /*end here*/

    public void InsertAppStatus(string strApplicationNumber, string strUserId, ref int intRet)
    {
        objComm.InsertAppStatus(strApplicationNumber, strUserId, ref intRet);
    }
    /*ID:1 START*/
    public void FetchAutoCommentDetails(string strApplicationNo, string strChannel, ref DataSet _ds)
    {
        objComm.FetchAutoCommentDetails(strApplicationNo, strChannel, ref _ds);
    }
    public void FetchUWProductivity(string strFromDate, string strToDate, string strUserId, ref DataSet _ds)
    {
        objComm.FetchUWProductivity(strFromDate, strToDate, strUserId, ref _ds);
    }
    /*ID:1 END*/
    /*ID:2 START*/
    public void UpdateUWDecitionAdditionalInfoFlag(string strApplicationNo, bool blnIsVerified, int intFlag, ref int intRet)
    {
        objComm.UpdateUWDecitionAdditionalInfoFlag(strApplicationNo, blnIsVerified, intFlag, ref intRet);
    }

    public void UpdateUWDecitionAdditionalInfoDetails(string strApplicationNo, string strDetails, int intFlag, string strUserId, ref int intRet)
    {
        objComm.UpdateUWDecitionAdditionalInfoDetails(strApplicationNo, strDetails, intFlag, strUserId, ref intRet);
    }
    /*ID:2 END*/
    /*ID:3 START*/
    public void InsertUserChangesLog(string strApplicationNo, string strUserId, string strKeys, string strValue, ref int intRet)
    {
        objComm.InsertUserChangesLog(strApplicationNo, strUserId, strKeys, strValue, ref intRet);
    }
    public void OnlineApplicationdashbordBucketsCountAll_Get(string strUwmode, string struserid, string strLinkOption, string strUserGroup, ref DataSet _dsUWDasbordCount)
    {
        objComm.OnlineApplicationdashbordBucketsCountAll_Get(strUwmode, struserid, strLinkOption, strUserGroup, ref _dsUWDasbordCount);
    }
    /*ID:3 END*/

    /*ID:4 START*/
    public void FetchReportData(string frmDate, string ToDate, string uid, int flag, ref DataSet _ds)
    {


        if (flag == 1)
        {
            objComm.ReportReject(frmDate, ToDate, ref _ds);

        }
        else if (flag == 2)
        {
            objComm.ReportFail(frmDate, ToDate, ref _ds);
        }
        else if (flag == 3)
        {
            objComm.ReportFY(ref _ds);
        }
        else if (flag == 4)
        {
            objComm.FetchUWProductivity(frmDate, ToDate, uid, ref _ds);
        }
        else if (flag == 5)
        {
            objComm.ReportReqRaised(frmDate, ToDate, ref _ds);
        }
    }
    /*ID:4 END*/
    /*ID:5 START*/
    public void UWDecisionManageDecisionDetails(DataTable _ds, ref int intRet)
    {
        objComm.UWDecisionManageDecisionDetails(_ds, ref intRet);
    }
    /*ID:5 END*/
    public void Save_MedicalReport_Reason(int Mode, string strAppno, int AssignType, string strUserId, ref int resp)
    {
        objComm.Save_MedicalReport_Reason(Mode, strAppno, AssignType, strUserId, ref resp);
    }
    public void Save_Country(string StrMode, string strAppno, string Country, string strUserId, ref int resp)
    {
        objComm.Save_Country(StrMode,strAppno, Country, strUserId, ref resp);
    }
    public void GetCountry(string strAppno, ref DataSet ds)
    {
        objComm.GetCountry(strAppno, ref ds);
    }

    public void GetPendingClient(string AppNo, ref DataSet ds)
    {
        objComm.GetPendingClient(AppNo, ref ds);
    }

    public void GetSelectedResult(string FirstName, string Lastname, string Gender, string DOB, string PinCode, ref int b, ref DataSet ds)
    {
        objComm.GetSelectedResult(FirstName, Lastname, Gender, DOB, PinCode, ref b, ref ds);
    }

    public void UpdateClient(long RefNo, string assuredType, int isNewClient, string AppNo, int CLIENTID, ref int Result)
    {
        objComm.UpdateClient(RefNo, assuredType, isNewClient, AppNo, CLIENTID, ref Result);
    }
    /*ID:7 START*/
    public void ManagePolicyPrinting(string strPhApplicationNo, string strPhSource, bool blnPhStatus, string strPhUserName, string strPhBranchCode, string strPhCreatedBy, string strPhUserBranch, string strPhProcessName, string strPhApplicationName, string strMode, ref int intResp)
    {
        objComm.ManagePolicyPrinting(strPhApplicationNo, strPhSource, blnPhStatus, strPhUserName, strPhBranchCode, strPhCreatedBy, strPhUserBranch, strPhProcessName, strPhApplicationName, strMode, ref intResp);
    }
    /*ID:7 END*/
    /*ID:8 START*/
    public void BindQuestion_CloseFile(ref DataSet _ds)
    {
        objComm.BindQuestion_CloseFile(ref _ds);
    }
    /*ID:8 END*/
    /*ID:9 START*/
    public void SaveAnswer_CloseFile(DataTable _dt,string remark,int CaseCate, ref int resp)
    {
        objComm.SaveAnswer_CloseFile(_dt, remark, CaseCate, ref resp);
    }
    /*ID:9 END*/
    /*ID:10 START*/
    public void Save_MedicalReport_Reason(string strAppno, int ReqReason, string strUserId, ref int resp)
    {
        objComm.Save_MedicalReport_Reason(strAppno, ReqReason, strUserId, ref resp);
    }
    /*ID:10 END*/
    /*ID:11 START*/
    public void FetchStatusReportData(string frmDate, string ToDate, string status, ref DataSet _ds)
    {
        objComm.GetStatusReport(frmDate, ToDate, status, ref _ds);
    }
    /*ID:11 END*/
    /*ID:9 START*/
    public void Save_TsarMsar(DataTable _dt, ref int resp)
    {
        objComm.Save_TsarMsar(_dt, ref resp);
    }

    /*ID:9 END*/
    /*ID:12 START*/
    public void GetLookupData(int intType, ref DataSet _ds)
    {
        objComm.GetLookupData(intType, ref _ds);
    }

    public void FetchDashboardUWMedicalManagementDetails(DashboardMedicalManagement objDashboardMedicalManagement, ref DataSet _ds)
    {
        objComm.FetchDashboardUWMedicalManagementDetails(objDashboardMedicalManagement.ApplicationNo, objDashboardMedicalManagement.PolicyNo, objDashboardMedicalManagement.StartDate, objDashboardMedicalManagement.EndDate, objDashboardMedicalManagement.Category, objDashboardMedicalManagement.IsOnline, ref _ds);
    }

    public void FetchDashboardUWMedicalManagementTPADetails(DashboardMedicalManagement objDashboardMedicalManagement, ref DataSet _ds)
    {
        objComm.FetchDashboardUWMedicalManagementTPADetails(objDashboardMedicalManagement.ApplicationNo, ref _ds);
    }
    public void FetchDashboardUWMedicalManagementPartners(ref DataSet _ds)
    {
        objComm.FetchDashboardUWMedicalManagementPartners(ref _ds);
    }
    public void FetchlastRunScheduarStatus(ref DataSet _ds)
    {
        objComm.FetchlastRunScheduarStatus(ref _ds);
    }
    /*ID:12 END*/
    /*ID:13 END*/
    public void ManageApplicationPriority(DataTable _dt, ref int intRet)
    {
        objComm.ManageApplicationPriority(_dt, ref intRet);
    }
	/*ID:13 END*/
	//########################## Added by Shyam Starts ###########################################
	public DataTable EmailParameter(string Flag1, string CommunicationType, string CommunicationKey, string Process, string TemplateId, string MailTo, string MailCC, string MobileNo, string Mode, string CreatedBy, string IsAttached, string AttachedFiles, string ApplicationNo, string PolicyNumber, string ParameterList, string FileName, string IsExternal)
	{
		DataTable ResultTable = new DataTable();
		// objComm.InsertAppStatus(strApplicationNumber, strUserId, ref intRet);
		return ResultTable = objComm.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
	}
	public void UWSaralUpdate(string Policynumber, string strUserId, string Flag, string Status)
	{
		objComm.UWSaralUpdate(Policynumber, strUserId, Flag, Status);
		// objComm.InsertAppStatus(strApplicationNumber, strUserId, ref intRet);
	}
	public void CheckOpenRequirement(string ApplicationNumber, string Flag, ref DataSet dsREQ)
	{
		objComm.CheckOpenRequirement(ApplicationNumber, Flag, ref dsREQ);
		// objComm.InsertAppStatus(strApplicationNumber, strUserId, ref intRet);
	}
	public void UpdateReciptAmt(string ApplicationNumber, string Flag)
	{
		objComm.UpdateReciptAmt(ApplicationNumber, Flag);
		// objComm.InsertAppStatus(strApplicationNumber, strUserId, ref intRet);
	}
    public DataTable revivaldate(string PolicyNo)
    {
        DataTable dtrevivaldate = new DataTable();
        // objComm.InsertAppStatus(strApplicationNumber, strUserId, ref intRet);
       return dtrevivaldate=objComm.GetRevivalDate(PolicyNo);
    }
    //########################## Added by Shyam End ###########################################
    //Added by pragati CR 27524
    public void Save_RsnExcessPremium(string StrMode, string strAppno, string RsnExcessPremium, string strUserId, string Country, ref int resp)
    {
        objComm.Save_RsnExcessPremium(StrMode, strAppno, RsnExcessPremium, strUserId, Country, ref resp);
    }
    //end
    
       // Added by Brijesh
      public void Save_StaffDetails(string APP_applicationNoStr, string RelationwithStaff, bool APP_isStaff, string NAWPStatus, string APPType, string bpm_UserID, ref int resp)
    {
        objComm.Save_StaffDetails(APP_applicationNoStr, RelationwithStaff, APP_isStaff, NAWPStatus, APPType, bpm_UserID, ref resp);
    }
    // End
    public void Save_BusinessException(string StrMode,string strAppno, bool BussException, string strUserId, ref int resp)
    {
        objComm.Save_BusinessException(StrMode, strAppno, BussException, strUserId, ref resp);
    }

    //4.1 Begin of changes by kavita -CR-30489 Phase 2
    public void FetchCheckListReportData(string ApplicationNo, ref DataSet _ds)
    {
        objComm.GetChecklistReport(ApplicationNo, ref _ds);
    }
    //4.1 End of changes by kavita -CR-30489 Phase 2

    #region start 14.1: This CR-2639 changes done by Sushant Devkate MFL00905 
    public decimal GetFSARofLA(string ApplicationNo)
    {
       // string FSARofLA = string.Empty;
        decimal FSARofLA = 0;
        DataSet ds= objComm.GetFSARofLA(ApplicationNo);
        if (ds.Tables[0].Rows.Count > 0) 
        {
           // FSARofLA = Convert.ToString(ds.Tables[0].Rows[0]["FSARLA"].ToString());
             FSARofLA = Convert.ToDecimal(ds.Tables[0].Rows[0]["FSARLA"]);
        }

        return FSARofLA;

    }
    #endregion End 14.1: This CR-2639 changes done by Sushant Devkate MFL00905 


    //Mer Videos Mfl00886 Sagar Thorave
    public string BlobTokn()
    {
        string BlobTokenAPI = ConfigurationManager.AppSettings["GenerateToken"].ToString().Trim();
        string BlobPutAPI = ConfigurationManager.AppSettings["PutBlobStorage"].ToString().Trim();
        string BlobGetAPI = ConfigurationManager.AppSettings["GetBlobStorage"].ToString().Trim();

        BlobDO blobClass = new BlobDO();
        RequestGenerateToken generateToken = new RequestGenerateToken();
        generateToken.userName = ConfigurationManager.AppSettings["BlobUserName"].ToString().Trim();
        generateToken.password = ConfigurationManager.AppSettings["BlobPassword"].ToString().Trim();
        generateToken.source = ConfigurationManager.AppSettings["BlobSource"].ToString().Trim();

        var json2 = RestSharp.SimpleJson.SerializeObject(generateToken);
        var webResponse = objComm.GenerateWebRequest(BlobTokenAPI, json2);
        var vOthr = new JavaScriptSerializer().Deserialize<ResponseGenerateToken>(webResponse);
        if (vOthr.tokenErrorCode == "0")
        {
            //InsertCommunicationLog("Blob Token Generated", "helper", "GetBlobTokn", "TokenID", vOthr.token, "", "", "Blob Token Generated");
            return vOthr.token;
        }
        else
        {
            //InsertCommunicationLog("Exception", "helper", "GetBlobTokn", "TokenErrorCode", vOthr.tokenErrorCode, "", "", "Exception: " + vOthr.tokenErrorDesc);
            return "";
        }

    }
    //Mer Videos Mfl00886 Sagar Thorave

    //Added By Royson Pinto on 22nd NOV to pass Restrict Further Cover
    public string GetRestrict_Further_Cover(string strApplicationNumber)
    {
        return objComm.GetRestrict_Further_Cover(strApplicationNumber);
    }
    //End
    // 16.1 Added By Royson Pinto on 16th Feb CMO Data Count for CR 4783 CR-10
    public dynamic CMOData(string application, string comments, string user_name)
    {
        return objComm.CMOData(application, comments, user_name);
    }
    public dynamic CMOCount()
    {
        return objComm.CMOCount();
    }
    public dynamic GetCMOComments(string application)
    {
        return objComm.GetCMOComments(application);
    }
    public dynamic GetDeclineThirdParty(string application)
    {
        return objComm.GetDeclineThirdParty(application);
    }
    //Added By Royson Pinto on 16th Feb Decline Third Party for CR 4783 CR-7
    public dynamic GetHoldData(string application)
    {
        return objComm.GetHoldData(application);
    }
    public dynamic GetUnHoldData()
    {
        return objComm.GetUnHoldData();
    }
    public dynamic InsertUnHoldData(string Application_Number, string LA_Name, string Status, string Policy_Status, string Proposer_Name, string Product_Name, string Created_By, string Created_Date)
    {
        return objComm.InsertUnHoldData(Application_Number, LA_Name, Status, Policy_Status, Proposer_Name, Product_Name, Created_By, Created_Date);
    }
    public dynamic InsertHoldData(string Application_Number, string LA_Name, string Status, string Policy_Status, string Proposer_Name, string Product_Name, string File_Name, string Created_By)
    {
        return objComm.InsertHoldData(Application_Number, LA_Name, Status, Policy_Status, Proposer_Name, Product_Name, File_Name, Created_By);
    }
    //
    //16.1 End By Royson Pinto on 16th Feb CMO Data Count for CR 4783 CR-10
}