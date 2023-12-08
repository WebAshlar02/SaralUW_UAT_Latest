using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
namespace UWSaralServices
{
    public class UWIssuanceDetails
    {
        CommFun objcomm = new CommFun();
        string strCHDRSEL = string.Empty;
        string strDATIME = string.Empty;
        string strAGNUM = string.Empty;
        string strREPORTAG01 = string.Empty;
        string strREPORTAG02 = string.Empty;
        string strZZSRCE01 = string.Empty;
        string strZZSRCE02 = string.Empty;
        string strZARACDE = string.Empty;
        string strZFEREGN01 = string.Empty;
        string strZFEREGN02 = string.Empty;
        string strZFEZONE01 = string.Empty;
        string strZFEZONE02 = string.Empty;
        string strBpmUserId = string.Empty;
        string strBpmUserRole = string.Empty;
        string strBpmBranch = string.Empty;
        string strpartnerId = string.Empty;
        string strprocessID = string.Empty;
        string strApplicationNo = string.Empty;
        int I = 0;
        LAPreIssueValService.ServiceClient ObjPriIssueval = new LAPreIssueValService.ServiceClient();
        LAPreIssueValService.MasterPolicyIssuance[] objResponce;
        public void UWIssuencePushService(string strPQuoteNo, DataSet _dsUWIssue, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "*******UW issuance Service  Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :UWIssuanceDetails.cs // MethodeName :UWIssuencePushService" + System.Environment.NewLine);
                if (_dsUWIssue.Tables.Count > 0 && _dsUWIssue.Tables[0].Rows.Count > 0)
                {
                    for (I = 0; I < _dsUWIssue.Tables[0].Rows.Count; I++)
                    {
                        strCHDRSEL = _dsUWIssue.Tables[0].Rows[I]["CHDRSEL"].ToString();
                        strDATIME = _dsUWIssue.Tables[0].Rows[I]["DATIME"].ToString();
                        strAGNUM = "";
                        strREPORTAG01 = "";
                        strREPORTAG02 = "";
                        strZZSRCE01 = "";
                        strZZSRCE02 = "";
                        strZARACDE = "";
                        strZFEREGN01 = "";
                        strZFEREGN02 = "";

                        strZFEZONE01 = "";
                        strZFEZONE02 = "";
                        strpartnerId = "UWSaral";
                        strprocessID = "UWSaral";
                        strApplicationNo = strPQuoteNo;
                        strBpmBranch = _dsUWIssue.Tables[0].Rows[I]["BPMBRANCH"].ToString();
                        strBpmUserRole = _dsUWIssue.Tables[0].Rows[I]["BPMUSERROLE"].ToString();
                        //strBpmUserId = _dsUWIssue.Tables[0].Rows[0]["BPMUSERID"].ToString();
                        strBpmUserId = objChangeObj.userLoginDetails._UserID;
                        objResponce = ObjPriIssueval.NBPISSUE(strCHDRSEL, strDATIME, strAGNUM, strREPORTAG01, strREPORTAG02, strZZSRCE01, strZZSRCE02, strZARACDE, strZFEREGN01, strZFEREGN02, strZFEZONE01, strZFEZONE02, strBpmBranch, strBpmUserRole, strBpmUserId, strpartnerId, strprocessID, strApplicationNo);
                    }
                    if (objResponce.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(objResponce[0].ERRORCODE.ToString()))
                        {
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :UWIssuanceDetails.cs // MethodeName :UWIssuencePushService" + System.Environment.NewLine + "UW Issuance  service Succeed" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + "*******UW issuance Service  End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        }
                        else
                        {
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :UWIssuanceDetails.cs // MethodeName :UWIssuencePushService" + System.Environment.NewLine + "UW Issuance  service Failed" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + "*******UW issuance Service  End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWIssuanceDetails.cs", "UWIssuencePushService", "E-ServiceCallError", "", "", objResponce[0].VALUES.ToString());

                        }
                    }
                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :UWIssuanceDetails.cs // MethodeName :UWIssuencePushService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWIssuanceDetails.cs", "UWIssuencePushService", "E-ExceptionError", "", "", Error.ToString());
                Logger.Info(strPQuoteNo + "*******UW issuance Service  end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
    }
}
