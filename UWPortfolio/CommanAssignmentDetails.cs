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
    DataLayer objSql = new DataLayer();
    public CommanAssignmentDetails()
    {
        
    }
    public void OnlineMasterUnderWriting_AssignmentDetails_GET(string mode,ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
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
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
           // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }

    public void OnlineDecisionRightsDisplayDetails_GET(string mode,string userid, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@userIDsaral", userid);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);

            if (_ds.Tables.Count > 0)
            {
                _ds.Tables[0].TableName = "ddlUWDecesion";
            }
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
        }
    }
    public void OnlineRisk_CategoryDisplayDetails_GET(string mode, string userid, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@userIDsaral", userid);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);

        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
        }
    }
    public void UnderWriting_AssignmentDetails_GET(string mode, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }

    public void UnderWriting_AssignmentDetails_GET_byId(string mode, int id ,ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@Id", id);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }

    public void UnderWriting_AssignmentDetails_GETDATA(string mode,  ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }
    public void Underwriting_AssignmentDetails_Save(string mode,string uw_bucket, string userid,string username ,string limit,
    string allocationparamete, string decisionrights, string riskcategory, string allocationlimit,ref int result)
    {
        try
        {
            //Logger.Info(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save");
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
            throw Error;
            //Logger.Error(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs(LD_applicationNoStr, "Failed", "UWPortfolio", "Commfun.cs", "OnlineApplicationdashbord_Get", "E-Error", "", "", Error.ToString());
        }
    }

    public void UnderWriting_AssignmentDetails_Delete(string mode,int id, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@Id", id);
            result = objSql.DeleteRecord("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }

    public void UnderWriting_AssignmentDetails_CheckUWbucket(string mode, string Uwbuket, ref DataSet ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@uw_bucket", Uwbuket);
            ds = objSql.RetrieveDataset("SP_Underwriting_Assignment_Details", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }

    public void UnderWriting_AssignmentDetails_Update(string mode, string uw_bucket, string userid,string UserName, string limit,
    string allocationparamete, string decisionrights, string riskcategory, string allocationlimit, int id, ref int result)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
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
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }

    public void Underwriting_AssignmentDetails_ProcessLog(string User_Id, string UserName, string ProccesKeyword, int ID, string uw_bucket, string userid, string limit,
   string allocationparamete, string decisionrights, string riskcategory, string allocationlimit, ref int result)
    {
        try
        {
            //Logger.Info(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save");
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
            throw Error;
            //Logger.Error(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs(LD_applicationNoStr, "Failed", "UWPortfolio", "Commfun.cs", "OnlineApplicationdashbord_Get", "E-Error", "", "", Error.ToString());
        }
    }

    public void Underwriting_AssignmentDetails_AddnewGroup_Save(string mode, string type, string discription,string fromrange,string torange, ref int result)
    {
        try
        {
            //Logger.Info(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[5];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@value", discription);
            _sqlparam[2] = new SqlParameter("@Type", type);
            _sqlparam[3] = new SqlParameter("@fromrange", fromrange);
            _sqlparam[4] = new SqlParameter("@Torange", torange);

            result = objSql.Insertrecord("SP_Underwriting_Assignment_Details_AddNewGroup", _sqlparam);
        }
        catch (Exception Error)
        {
            throw Error;
            //Logger.Error(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs(LD_applicationNoStr, "Failed", "UWPortfolio", "Commfun.cs", "OnlineApplicationdashbord_Get", "E-Error", "", "", Error.ToString());
        }
    }

    public void UnderWriting_AssignmentUserAccess_GET_byUserID(string mode, string userrid, ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _sqlparam[1] = new SqlParameter("@UserID", userrid);
            _ds = objSql.RetrieveDataset("SP_UW_Rights_AccessCheck", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }
    public void UnderWriting_AssignmentUserAccess_GET_byUSERCOUNT(string mode,  ref DataSet _ds)
    {
        try
        {
            Logger.Info("STAG 2 A1 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@MODE", mode);
            _ds = objSql.RetrieveDataset("SP_UW_Rights_AccessCheck", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineMasterDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
            // SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineMasterDisplayDetails_GET", "E-Error", "", "", Error.ToString());
        }
    }

    public void Underwriting_AssignmentDetails_LOGINLOGOUT_Log(string UserId, string UserName, string UserGroup,string ProccesKeyword , string Mode, ref int result)
    {
        try
        {
            //Logger.Info(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[5];
            _sqlparam[0] = new SqlParameter("@UserId", UserId);
            _sqlparam[1] = new SqlParameter("@UserName", UserName);
            _sqlparam[2] = new SqlParameter("@UserGroup ", UserGroup);
            _sqlparam[3] = new SqlParameter("@Process", ProccesKeyword);
            _sqlparam[4] = new SqlParameter("@MODE", Mode);
            
            result = objSql.Insertrecord("SP_UW_Rights_AccessCheck", _sqlparam);
        }
        catch (Exception Error)
        {
            throw Error;
            //Logger.Error(LD_applicationNoStr + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineLoadingDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs(LD_applicationNoStr, "Failed", "UWPortfolio", "Commfun.cs", "OnlineApplicationdashbord_Get", "E-Error", "", "", Error.ToString());
        }
    }
}

