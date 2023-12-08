//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 18/11/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************

//**********************************************************************
// Sr. No.              : 2
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel [WebAshlar02]
// BRD/CR/Codesk No/Win : CR-5855  
// Date Of Creation     : 04-05-2023
// Description          : Grid based Loading access for Counter offer in Saral.
//**********************************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using Platform.Utilities.LoggerFramework;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UWSaralObjects;
using RestSharp;


public class CommanAssignmentDetails
{
    #region 1.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    //1.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    Commfun comfun = new Commfun();
    DataLayer objSql = new DataLayer();
    public CommanAssignmentDetails()
    {
        
    }
    public void OnlineMasterUnderWriting_AssignmentDetails_GET(string mode,ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterUnderWriting_AssignmentDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);

            if (_ds.Tables.Count > 0)
            {
                _ds.Tables[0].TableName = "UWBucket";
                _ds.Tables[1].TableName = "Limit";
                _ds.Tables[2].TableName = "Allocation Parameters"; 
                _ds.Tables[3].TableName = "Decision Rights";
                _ds.Tables[4].TableName = "Risk Category Allowed";
            }   
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterUnderWriting_AssignmentDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterUnderWriting_AssignmentDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void OnlineMasterUnderWriting_Priority_GET(string mode, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterUnderWriting_AssignmentDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details_AddNewGroup", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterUnderWriting_AssignmentDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterUnderWriting_AssignmentDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void OnlineDecisionRightsDisplayDetails_GET(string mode,string userid, string torange, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineDecisionRightsDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[3];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@userIDsaral", userid);
            _sqlparam[2] = new SqlParameter("@TorangeLimit", torange);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details_V1", _sqlparam);

            if (_ds.Tables.Count > 0)
            {
                _ds.Tables[0].TableName = "ddlUWDecesion";
            }
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineDecisionRightsDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineDecisionRightsDisplayDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void OnlineRisk_CategoryDisplayDetails_GET(string mode, string userid, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineRisk_CategoryDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@userIDsaral", userid);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);

        }
        catch (Exception Error)
        {
            Logger.Error(userid+"STAG 2 :PageName :Commfun.CS // MethodeName :OnlineRisk_CategoryDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs(userid+"", "Failed", "UWPortfolio", "Commfun.cs", "OnlineRisk_CategoryDisplayDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentDetails_GET(string mode, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentDetails_GET_byId(string mode, int id ,ref DataSet _ds)
    {
        try
        {
            Logger.Info(id+"STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET_byId");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@Id", id);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error(id+"STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET_byId Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs(id+"", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GET_byId", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentDetails_GET_Priority(string mode, int priority, ref DataSet _ds)
    {
        try
        {
            Logger.Info( "STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET_byId");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@Priority", priority);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details_AddNewGroup", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET_byId Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs( "", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GET_byId", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentDetails_GETDATA(string mode,int id , ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GETDATA");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@ID", id);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GETDATA Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GETDATA", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UW_AddnewGP_GETDATA(string mode, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GETDATA");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details_AddNewGroup", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GETDATA Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GETDATA", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void Underwriting_AssignmentDetails_Save(string mode,string uw_bucket, string userid,string username ,string limit,
    string allocationparamete, string decisionrights, string riskcategory, string allocationlimit,ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[9];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@uw_bucket", uw_bucket);
            _sqlparam[2] = new SqlParameter("@UserID", userid);
            _sqlparam[3] = new SqlParameter("@UserName", username);
            _sqlparam[4] = new SqlParameter("@limit", limit);//ME for offline
            _sqlparam[5] = new SqlParameter("@Allocationparameter", allocationparamete);
            _sqlparam[6] = new SqlParameter("@DecisionRights", decisionrights);
            _sqlparam[7] = new SqlParameter("@RiskCategory", riskcategory);
            _sqlparam[8] = new SqlParameter("@AllocationLimit", allocationlimit);
            result = objSql.Insertrecord("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_AssignmentDetails_Save", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentDetails_Delete(string mode,int id, ref int result)
    {
        try
        {
            Logger.Info(id+"STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_Delete");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@Id", id);
            result = objSql.DeleteRecord("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error(id+"STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_Delete Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs(id+"", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_Delete", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentDetails_CheckUWbucket(string mode, string Uwbuket, ref DataSet ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_CheckUWbucket");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@uw_bucket", Uwbuket);
            ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_CheckUWbucket Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_CheckUWbucket", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentDetails_Update(string mode, string uw_bucket, string userid,string UserName, string limit,
    string allocationparamete, string decisionrights, string riskcategory, string allocationlimit, int id, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_Update");
            SqlParameter[] _sqlparam = new SqlParameter[10];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@uw_bucket", uw_bucket);
            _sqlparam[2] = new SqlParameter("@UserID", userid);
            _sqlparam[3] = new SqlParameter("@UserName", UserName);
            _sqlparam[4] = new SqlParameter("@limit", limit);//ME for offline
            _sqlparam[5] = new SqlParameter("@Allocationparameter", allocationparamete);
            _sqlparam[6] = new SqlParameter("@DecisionRights", decisionrights);
            _sqlparam[7] = new SqlParameter("@RiskCategory", riskcategory);
            _sqlparam[8] = new SqlParameter("@AllocationLimit", allocationlimit);
            _sqlparam[9] = new SqlParameter("@ID", id);

            result = objSql.Insertrecord("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_Update Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_Update", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void Underwriting_AssignmentDetails_ProcessLog(string User_Id, string UserName, string ProccesKeyword, int ID, string uw_bucket, string userid, string limit,
   string allocationparamete, string decisionrights, string riskcategory, string allocationlimit, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_ProcessLog");
            SqlParameter[] _sqlparam = new SqlParameter[11];
            _sqlparam[0] = new SqlParameter("@User_Id", User_Id);
            _sqlparam[1] = new SqlParameter("@UserName", UserName);
            _sqlparam[2] = new SqlParameter("@ProccesKeyword", ProccesKeyword);
            _sqlparam[3] = new SqlParameter("@ID", ID);
            _sqlparam[4] = new SqlParameter("@uw_bucket", uw_bucket);
            _sqlparam[5] = new SqlParameter("@UserID", userid);
            _sqlparam[6] = new SqlParameter("@limit", limit);//ME for offline
            _sqlparam[7] = new SqlParameter("@Allocationparameter", allocationparamete);
            _sqlparam[8] = new SqlParameter("@DecisionRights", decisionrights);
            _sqlparam[9] = new SqlParameter("@RiskCategory", riskcategory);
            _sqlparam[10] = new SqlParameter("@AllocationLimit", allocationlimit);
            result = objSql.Insertrecord("SP_Underwriting_Assignment_ProcessLog", _sqlparam);
        }
        catch (Exception Error)
        {
            
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_ProcessLog Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_AssignmentDetails_ProcessLog", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void Underwriting_AssignmentDetails_AddnewGroup_Save(string mode, string type, string discription,int fromrange,int torange,string userid, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_AddnewGroup_Save");
            SqlParameter[] _sqlparam = new SqlParameter[6];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@value", discription);
            _sqlparam[2] = new SqlParameter("@Type", type);
            _sqlparam[3] = new SqlParameter("@fromrange", fromrange);
            _sqlparam[4] = new SqlParameter("@Torange", torange);
            _sqlparam[5] = new SqlParameter("@Created_By", userid);

            result = objSql.Insertrecord("SP_Underwriting_Assignment_Details_AddNewGroup", _sqlparam);
        }
        catch (Exception Error)
        {
          
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_AddnewGroup_Save Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_AssignmentDetails_AddnewGroup_Save", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void UW_AddNewGP_Log(string mode ,string User_Id, string UserName, string ProccesKeyword, int ID, string type, string Discription, string From_Limit,
  string To_Limit, string SystemDate, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :UW_AddNewGP_Log");
            SqlParameter[] _sqlparam = new SqlParameter[10];
            _sqlparam[0] = new SqlParameter("@Created_By", User_Id);
            _sqlparam[1] = new SqlParameter("@UserName", UserName);
            _sqlparam[2] = new SqlParameter("@Proccesskeyword", ProccesKeyword);
            _sqlparam[3] = new SqlParameter("@ID", ID);
            _sqlparam[4] = new SqlParameter("@Type", type);
            _sqlparam[5] = new SqlParameter("@Value", Discription);
            _sqlparam[6] = new SqlParameter("@fromrange", From_Limit);//ME for offline
            _sqlparam[7] = new SqlParameter("@Torange", To_Limit);
            _sqlparam[8] = new SqlParameter("@SystemDate", SystemDate);
            _sqlparam[9] = new SqlParameter("@MODE", mode);
            result = objSql.Insertrecord("SP_Underwriting_Assignment_Details_AddNewGroup", _sqlparam);
        }
        catch (Exception Error)
        {

            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UW_AddNewGP_Log Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UW_AddNewGP_Log", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentUserAccess_GET_byUserID(string mode, string userrid, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentUserAccess_GET_byUserID");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@UserID", userrid);
            _ds = objSql.RetrieveDataset("SP_UW_Rights_AccessCheck", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentUserAccess_GET_byUserID Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentUserAccess_GET_byUserID", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void UnderWriting_AssignmentUserAccess_GET_byUSERCOUNT(string mode,  ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentUserAccess_GET_byUSERCOUNT");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_UW_Rights_AccessCheck", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentUserAccess_GET_byUSERCOUNT Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentUserAccess_GET_byUSERCOUNT", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void Underwriting_AssignmentDetails_LOGINLOGOUT_Log(string UserId, string UserName, string UserGroup,string ProccesKeyword , string Mode,string flag, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_LOGINLOGOUT_Log");
            SqlParameter[] _sqlparam = new SqlParameter[6];
            _sqlparam[0] = new SqlParameter("@UserId", UserId);
            _sqlparam[1] = new SqlParameter("@UserName", UserName);
            _sqlparam[2] = new SqlParameter("@UserGroup ", UserGroup);
            _sqlparam[3] = new SqlParameter("@Process", ProccesKeyword);
            _sqlparam[4] = new SqlParameter("@MODE", Mode);
            _sqlparam[5] = new SqlParameter("@LoginFlag", flag);
            
            result = objSql.Insertrecord("SP_UW_Rights_AccessCheck", _sqlparam);
        }
        catch (Exception Error)
        {
            
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_LOGINLOGOUT_Log Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_AssignmentDetails_LOGINLOGOUT_Log", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void UnderWriting_AssignmentDetails_CheckAllocationparameter(string mode, string limit, string alparameter , ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_CheckAllocationparameter");
            SqlParameter[] _sqlparam = new SqlParameter[3];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@TorangeLimit", limit);
            _sqlparam[2] = new SqlParameter("@Allocationparameter", alparameter);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_CheckAllocationparameter Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_CheckAllocationparameter", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void Underwriting_LOGIN_Log(string Mode, string sessionid, string UserGroup, string UserId, string UserName,  string BranchCode,  string BranchName, string action , string source ,int version, int islogin, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_LOGINLOGOUT_Log");
            SqlParameter[] _sqlparam = new SqlParameter[11];
            _sqlparam[0] = new SqlParameter("@MODE", Mode);
            _sqlparam[1] = new SqlParameter("@SESSIONID", sessionid);
            _sqlparam[2] = new SqlParameter("@USERGROUP ", UserGroup);
            _sqlparam[3] = new SqlParameter("@USERID", UserId);
            _sqlparam[4] = new SqlParameter("@USERNAME", UserName);
            _sqlparam[5] = new SqlParameter("@BRANCHCODE", BranchCode);
            _sqlparam[6] = new SqlParameter("@BRANCHNAME", BranchName);
            _sqlparam[7] = new SqlParameter("@ACTION", action);
            _sqlparam[8] = new SqlParameter("@SOURCE", source);
            _sqlparam[9] = new SqlParameter("@VERSION", version);
            _sqlparam[10] = new SqlParameter("@ISLOGIN", islogin);

            result = objSql.Insertrecord("SP_UW_SARALUSER_LOGINLOGS", _sqlparam);
        }
        catch (Exception Error)
        {

            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_LOGINLOGOUT_Log Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_LOGINLOGOUT_Log", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    //1.1 End of Changes; Bhaumik Patel - [CR-5307]
    #endregion 1.1 End of Changes; Bhaumik Patel - [CR-5307]


    //2.1 Begin of Changes; Bhaumik Patel - [CR-5855]
    public void OnlineMasterUser_FlatMortalityDetails_GET(string mode, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterUser_FlatMortalityDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_User_FlatMortality_Details", _sqlparam);

            if (_ds.Tables.Count > 0)
            {
                _ds.Tables[0].TableName = "Users";
                _ds.Tables[1].TableName = "MortalityGrid";
                _ds.Tables[2].TableName = "FlatExstra";
               
            }
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterUser_FlatMortalityDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterUser_FlatMortalityDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void FlatMortalityDetails_Save(string mode, string userid, string username, string Mortality, string MortalityCode,
  string Created_By, string limit, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[7];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@UserID", userid);
            _sqlparam[2] = new SqlParameter("@UserName", username);
            _sqlparam[3] = new SqlParameter("@Mortality", Mortality);
            _sqlparam[4] = new SqlParameter("@MortalityCode", MortalityCode);//ME for offline
            _sqlparam[5] = new SqlParameter("@Created_By", Created_By);
            _sqlparam[6] = new SqlParameter("@limit", limit);
            result = objSql.Insertrecord("SP_User_FlatMortality_DetailsSave", _sqlparam);
        }
        catch (Exception Error)
        {

            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_AssignmentDetails_Save", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void FlatMortalityDetails_Update(string mode, string userid, string username, string Mortality, string MortalityCode,
 string Created_By, string limit,int id, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[8];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@UserID", userid);
            _sqlparam[2] = new SqlParameter("@UserName", username);
            _sqlparam[3] = new SqlParameter("@Mortality", Mortality);
            _sqlparam[4] = new SqlParameter("@MortalityCode", MortalityCode);//ME for offline
            _sqlparam[5] = new SqlParameter("@Modified_By", Created_By);
            _sqlparam[6] = new SqlParameter("@limit", limit);
            _sqlparam[7] = new SqlParameter("@ID", id);
            result = objSql.Insertrecord("SP_User_FlatMortality_Details", _sqlparam);
        }
        catch (Exception Error)
        {

            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_AssignmentDetails_Save", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void FlatMortalityDetails_GET(string mode, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_User_FlatMortality_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void FlatMortalityDetails_Delete(string mode, int id, ref int result)
    {
        try
        {
            Logger.Info(id + "STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_Delete");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@Id", id);
            result = objSql.DeleteRecord("SP_User_FlatMortality_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error(id + "STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_Delete Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs(id + "", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_Delete", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void FlatMortalityDetails_GET_byId(string mode, int id, ref DataSet _ds)
    {
        try
        {
            Logger.Info(id + "STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET_byId");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@Id", id);
            _ds = objSql.RetrieveDataset("SP_User_FlatMortality_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error(id + "STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GET_byId Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs(id + "", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GET_byId", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void OnlineFlatMortalityDetails_GET(string mode, string userid, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineFlatMortalityDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@UserID", userid);
            _ds = objSql.RetrieveDataset("SP_User_FlatMortality_Details", _sqlparam);

            if (_ds.Tables.Count > 0)
            {
                _ds.Tables[0].TableName = "ddlLoadFlatMortality";
            }
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineFlatMortalityDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineFlatMortalityDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void OnlineFlatMortalityDetails_GET_RateAdjust(string mode, string userid, ref DataSet _dsRate)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineFlatMortalityDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@UserID", userid);
            _dsRate = objSql.RetrieveDataset("SP_User_FlatMortality_Details", _sqlparam);

            if (_dsRate.Tables.Count > 0)
            {
                _dsRate.Tables[0].TableName = "RateAdjust";
            }
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineFlatMortalityDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineFlatMortalityDetails_GET", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
    public void FlatMortality_ProcessLog(string User_Id, string UserName, string ProccesKeyword, int ID,  string Mortality ,string Mortality_Code,
    string Flat_Extra, string userid, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_ProcessLog");
            SqlParameter[] _sqlparam = new SqlParameter[8];
            _sqlparam[0] = new SqlParameter("@User_Id", User_Id);
            _sqlparam[1] = new SqlParameter("@UserName", UserName);
            _sqlparam[2] = new SqlParameter("@ProccesKeyword", ProccesKeyword);
            _sqlparam[3] = new SqlParameter("@ID", ID);
            _sqlparam[4] = new SqlParameter("@Mortality_Code", Mortality_Code);
            _sqlparam[5] = new SqlParameter("@UserID", userid);
            _sqlparam[6] = new SqlParameter("@Mortality", Mortality);//ME for offline
            _sqlparam[7] = new SqlParameter("@Flat_Extra", Flat_Extra);
            result = objSql.Insertrecord("SP_FlatMortality_ProcessLog", _sqlparam);
        }
        catch (Exception Error)
        {

            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :Underwriting_AssignmentDetails_ProcessLog Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "Underwriting_AssignmentDetails_ProcessLog", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }

    public void FlatMortality_GETDATA(string mode, int id, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GETDATA");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@ID", id);
            _ds = objSql.RetrieveDataset("SP_User_FlatMortality_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UnderWriting_AssignmentDetails_GETDATA Error :" + System.Environment.NewLine + Error.ToString());
            comfun.SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UnderWriting_AssignmentDetails_GETDATA", "E-Error", "", "", Error.ToString());
            throw Error;
        }
    }
}


