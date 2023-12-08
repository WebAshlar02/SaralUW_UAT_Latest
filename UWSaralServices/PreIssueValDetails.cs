using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
//using System.Web.SessionState;

namespace UWSaralServices
{
    public class PreIssueValDetails
    {
        string strPolicyNo = string.Empty;
        string strReasonCode = string.Empty;
        string strReasondiscp = string.Empty;
        string strBpmUserId = string.Empty;
        string strBpmUserRole = string.Empty;
        string strdatetime = string.Empty;
        string strBpmBranch = string.Empty;
        string strpartnerId = string.Empty;
        string strprocessID = string.Empty;
        string strApplicationNo = string.Empty;
        string stramountdue = string.Empty;
        CommFun objcomm = new CommFun();
        DataTable dtPreVal = new DataTable();
        int j = 0;
        public void PreIssuevalidationPushService(string strPQuoteNo, ref DataSet _DsPreIssueRslt, DataSet _dsPreIssue, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: PREISSUE VALIDATION" + System.Environment.NewLine);
            if (_dsPreIssue != null && _dsPreIssue.Tables[0].Rows.Count > 0)
            {
                _DsPreIssueRslt = new DataSet();
                _DsPreIssueRslt.Locale = CultureInfo.InvariantCulture;

                DataTable sampleDataTable = _DsPreIssueRslt.Tables.Add("SampleData");
                sampleDataTable.Columns.Add("Applicationno", typeof(string));
                sampleDataTable.Columns.Add("PRE ISSUE DESCRIPTION", typeof(string));
                sampleDataTable.Columns.Add("BACKDATINGINTEREST", typeof(string));
                //23NOV2017 BEGIN
                sampleDataTable.Columns.Add("TOATALAMOUNT", typeof(string));
                sampleDataTable.Columns.Add("AMOUNTINSUSPENCE", typeof(string));
                sampleDataTable.Columns.Add("SERVICETAX", typeof(string));
                sampleDataTable.Columns.Add("AMOUNTDUE", typeof(string));

                string IsDetach = string.Empty;
                if (!string.IsNullOrEmpty(_dsPreIssue.Tables[0].Rows[0]["IsDetach"].ToString()))
                {
                    IsDetach= _dsPreIssue.Tables[0].Rows[j]["IsDetach"].ToString();
                }

                for (j = 0; j < _dsPreIssue.Tables[0].Rows.Count; j++)
                {
                    try
                    {
                        Logger.Info(strPQuoteNo + "*******Preissue validation Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        Logger.Info("STAG 2 :PageName :PreIssueValDetails.CS // MethodeName :PreIssuevalidationPushService" + System.Environment.NewLine);
                        LAPreIssueValService.ServiceClient ObjPriIssueval = new LAPreIssueValService.ServiceClient();
                        LAPreIssueValService.MasterPolicyPreIssue[] objResponce = null;
                        strpartnerId = "UWSaral";
                        strprocessID = "UWSaral";
                        strApplicationNo = strPQuoteNo;
                        if (_dsPreIssue.Tables.Count > 0 && _dsPreIssue.Tables[0].Rows.Count > 0)
                        {
                            strApplicationNo = _dsPreIssue.Tables[0].Rows[j]["APPLICATIONNO"].ToString();
                            strPolicyNo = _dsPreIssue.Tables[0].Rows[j]["CHDRSEL"].ToString();
                            strdatetime = _dsPreIssue.Tables[0].Rows[j]["DATIME"].ToString();
                            strBpmBranch = _dsPreIssue.Tables[0].Rows[j]["BPMBRANCH"].ToString();
                            strBpmUserRole = _dsPreIssue.Tables[0].Rows[j]["BPMUSERROLE"].ToString();
                            //strBpmUserId = _dsPreIssue.Tables[0].Rows[0]["BPMUSERID"].ToString();
                            strBpmUserId = objChangeObj.userLoginDetails._UserID;
                            objResponce = ObjPriIssueval.PRPPREISS(strPolicyNo, strdatetime, strBpmBranch, strBpmUserRole, strBpmUserId, strpartnerId, strprocessID, strApplicationNo);
                        }
                        if (objResponce.Length > 0)
                        {

                            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: PRE ISSUE VALIDATION" + System.Environment.NewLine);
                            objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "PreIssueValDetails.cs", "PreIssuevalidationPushService", "E-Error", "", "", "pre issue validation pending");

                            //}
                            strLAPushErrorcode = 1;
                            strLAPushStatus = "Failed";



                            //23NOV2017 END
                            DataRow sampleDataRow;
                            for (int i = 0; i <= objResponce.Length - 1; i++)
                            {
                                if (!string.IsNullOrEmpty(objResponce[i].ERORDSC))
                                {
                                    sampleDataRow = sampleDataTable.NewRow();
                                    sampleDataRow["Applicationno"] = strApplicationNo;//objResponce[i].ERORDSC.Trim();
                                    sampleDataRow["PRE ISSUE DESCRIPTION"] = objResponce[i].ERORDSC.Trim();
                                    sampleDataRow["BACKDATINGINTEREST"] = objResponce[i].ZBCKDINT01.Trim();
                                    //23NOV2017 BEGIN
                                    sampleDataRow["TOATALAMOUNT"] = objResponce[i].ZTAMTDUE.Trim();
                                    sampleDataRow["AMOUNTINSUSPENCE"] = objResponce[i].PREMSUSP.Trim();
                                    sampleDataRow["SERVICETAX"] = objResponce[i].STAXAMT01.Trim();
                                    stramountdue = Convert.ToString(Convert.ToDecimal(objResponce[i].PREMSUSP.Trim()) - Convert.ToDecimal(objResponce[i].ZTAMTDUE.Trim()));
                                    sampleDataRow["AMOUNTDUE"] = stramountdue;
                                    //23NOV2017 BEGIN
                                    sampleDataTable.Rows.Add(sampleDataRow);
                                    strLAPushStatus = strLAPushStatus + System.Environment.NewLine + sampleDataTable.Rows[0]["PRE ISSUE DESCRIPTION"].ToString();
                                }
                            }
                            strLAPushStatus = sampleDataTable.Rows[0]["PRE ISSUE DESCRIPTION"].ToString();
                        }
                        else
                        {

                            if (strLAPushErrorcode != 1 && (IsDetach == "No" || IsDetach ==""))
                            {
                                strLAPushErrorcode = 0;
                                strLAPushStatus = "Success";
                            }
                            else
                            {
                                strLAPushErrorcode = 0;
                                strLAPushStatus = "Success";
                            }
                            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: PRE ISSUE VALIDATION" + System.Environment.NewLine);
                        }

                    }
                    catch (Exception Error)
                    {
                        strLAPushErrorcode = 1;
                        strLAPushStatus = "Failed";
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR:Service Call Execution ERROR: PRE ISSUE VALIDATION" + "ERROR MESSAGE:" + Error.Message + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "PreIssueValDetails.cs", "PreIssuevalidationPushService", "E-ExceptionError", "", "", Error.ToString());
                    }
                }
            }
            else
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "No Data";

                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "PreIssueValDetails.cs", "PreIssuevalidationPushService", "E-Error", "", "", strLAPushStatus);
            }
        }
    }
}
