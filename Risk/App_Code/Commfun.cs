/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:PAGE LOAD
REMARK: ADDED BY SHRI TO UPLOAD RISK E&Y
DateTime :10MAY18
**********************************************************************************************************************************
 ***********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :AJAY SAHU
METHODE/EVENT:UnderWritingRequirementUpload
REMARK: TO upload datatable into table 
DateTime :26MAY18
**********************************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Platform.Utilities.LoggerFramework;


/// <summary>
/// Summary description for Commfun
/// </summary>
public class Commfun
{
    
    DataLayer objData = new DataLayer();
   
    /*added by shri on 29 dec 17 for risk parameter*/
    public void FetchBulkRiskApplication(string strApplicationNo, ref DataSet ds)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :BAL.CS // MethodeName :UpdateApplicationStatus");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplicationNo);
            ds = objData.RetrieveDataset("USP_UWSARAL_MANAGE_RISK_APPLICATION", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :BAL.CS // MethodeName :UpdateAppStatus Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWAutoIssuence", "BAL.CS", "UpdateAppStatus", "E-Error", "", "", Error.ToString());
        }
    }

    public void FetchDesignChange(string UserId , ref DataSet ds)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :BAL.CS // MethodeName :UpdateApplicationStatus");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@UserId", UserId);
            ds = objData.RetrieveDataset("USP_UWSARAL_RISK_DASHBOARD_MANAGE", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :BAL.CS // MethodeName :UpdateAppStatus Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWAutoIssuence", "BAL.CS", "UpdateAppStatus", "E-Error", "", "", Error.ToString());
        }
    }

    /*added by shri on 24 dec 17 for risk parameter*/
    public void ManageBulkRiskParameter(DataTable _dtRiskParameter, ref int intRet)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :BAL.CS // MethodeName :UpdateApplicationStatus");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@TYP_UWSARAL_RISK_PARAMETER", _dtRiskParameter);
            objData.Insertrecord("USP_UWSARAL_MANAGE_RISK_PARAMETER", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :BAL.CS // MethodeName :UpdateAppStatus Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWAutoIssuence", "BAL.CS", "UpdateAppStatus", "E-Error", "", "", Error.ToString());
        }
    }
    public void TPADocsManualInsert(DataTable _dtRiskParameter, ref int intRet)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :BAL.CS // MethodeName :UpdateApplicationStatus");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@TYP_UWSARAL_TPA_MANUAL_PUSH", _dtRiskParameter);
            intRet = objData.Insertrecord("[TRANSACTIONDBLFTPA].DBO.[USP_UWSARAL_TPA_UTILITY_MANUAL_DMS_PUSH]", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :BAL.CS // MethodeName :UpdateAppStatus Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWAutoIssuence", "BAL.CS", "UpdateAppStatus", "E-Error", "", "", Error.ToString());
        }
    }
    public void ManageBulkRiskEANDYCASES(DataTable _dtRiskParameter, ref int intRet)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :BAL.CS // MethodeName :UpdateApplicationStatus");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@TYP_UWSARAL_RISK_EANDY", _dtRiskParameter);
            objData.Insertrecord("USP_UWSARAL_MANAGE_RISK_EANDY_CASES", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :BAL.CS // MethodeName :UpdateAppStatus Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWAutoIssuence", "BAL.CS", "UpdateAppStatus", "E-Error", "", "", Error.ToString());
        }
    }
    /*ID:2 START*/
    public void UnderWritingRequirementUpload(DataTable _dtRiskParameter, ref int intRet)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :BAL.CS // MethodeName :UnderWritingRequirementUpload");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@dt", _dtRiskParameter);
            intRet = objData.Insertrecord("[TRANSACTIONDBLFTPA].DBO.[USP_UWSARAL_UTILITY_UNDERWRITER_MEDICAL_REQ_RAISED_CASES]", _sqlparam);
            if (intRet == -1)
            {
                intRet = 1;
            }
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :BAL.CS // MethodeName :UpdateAppStatus Error :" + System.Environment.NewLine + Error.ToString());
            throw new Exception(Error.Message);
            //SaveErrorLogs("", "Failed", "UWAutoIssuence", "BAL.CS", "UpdateAppStatus", "E-Error", "", "", Error.ToString());
        }
    }
    /*ID:2 END*/
}