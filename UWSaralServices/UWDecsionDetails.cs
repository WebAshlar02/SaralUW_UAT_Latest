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
    public class UWDecsionDetails
    {
        string strPolicyNo = string.Empty;
        string strReasonCode = string.Empty;
        string strReasondiscp = string.Empty;
        string strBpmUserId = string.Empty;
        string strBpmUserRole = string.Empty;
        string strdatetime = string.Empty;
        string strBpmBranch = string.Empty;
        string strPostPeriod;
        string strPostPeriodcode = string.Empty;
        string strpartnerId = string.Empty;
        string strprocessID = string.Empty;
        string strApplicationNo = string.Empty;
        int i = 0;
        CommFun objcomm = new CommFun();
        public void UWDecsionPushService(string strPQuoteNo, string strUwdecision, DataSet _dsUWDec, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: UW DECISION FOR " + strUwdecision+ System.Environment.NewLine);
                if (strUwdecision == "Declined")
                {
                    DeclineProposal(strPQuoteNo, _dsUWDec, objChangeObj, ref strLAPushErrorcode, ref  strLAPushStatus);
                }
                if (strUwdecision == "Postponed")
                {
                    PostponeProposal(strPQuoteNo, _dsUWDec, objChangeObj, ref strLAPushErrorcode, ref  strLAPushStatus);
                }
                if (strUwdecision == "Withdrawn")
                {
                    WithdrawProposal(strPQuoteNo, _dsUWDec, objChangeObj, ref strLAPushErrorcode, ref  strLAPushStatus);
                }
                if (strUwdecision == "Approved")
                {
                    ApproveProposal(strPQuoteNo, _dsUWDec, objChangeObj, ref strLAPushErrorcode, ref  strLAPushStatus);
                }
                if (strUwdecision == "DeclineWithdrawa")
                {
                    WithdrawProposal(strPQuoteNo, _dsUWDec, objChangeObj, ref strLAPushErrorcode, ref  strLAPushStatus);
                }
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "*******UW decision Service  end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :UWDecsionDetails.cs // MethodeName :UWDecsionPushService Error :" + System.Environment.NewLine + Error.StackTrace);
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "UWDecsionPushService", "E-Error", "", "", Error.ToString());
            }
        }

        public void DeclineProposal(string strPQuoteNo, DataSet _dsUWDec, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: UW DECISION-DECLAINE" + System.Environment.NewLine);
                LAUwdecisionService.ServiceClient objUWdecision = new LAUwdecisionService.ServiceClient();
                LAUwdecisionService.Master objResponce = new LAUwdecisionService.Master();
                if (_dsUWDec.Tables.Count > 0 && _dsUWDec.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < _dsUWDec.Tables[0].Rows.Count; i++)
                    {
                        strPolicyNo = _dsUWDec.Tables[0].Rows[i]["CHDRSEL"].ToString();
                        strReasonCode = _dsUWDec.Tables[0].Rows[i]["REASONCD"].ToString();
                        strReasondiscp = _dsUWDec.Tables[0].Rows[i]["RESNDESC"].ToString();
                        strdatetime = _dsUWDec.Tables[0].Rows[i]["DATIME"].ToString();
                        strBpmBranch = _dsUWDec.Tables[0].Rows[i]["BPMBRANCH"].ToString();
                        strBpmUserRole = _dsUWDec.Tables[0].Rows[i]["BPMUSERROLE"].ToString();
                        //strBpmUserId = _dsUWDec.Tables[0].Rows[0]["BPMUSERID"].ToString();
                        strBpmUserId = objChangeObj.userLoginDetails._UserID;
                        strpartnerId = "UWSaral";
                        strprocessID = "UWSaral";
                        strApplicationNo = strPQuoteNo;
                        objResponce = objUWdecision.PRPDEC(strPolicyNo, strReasonCode, strReasondiscp, strdatetime, strBpmBranch, strBpmUserRole, strBpmUserId, strpartnerId, strprocessID, strApplicationNo);
                    }
                    if (objResponce.ERRORCODE == "0")
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = "Success";
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: DECLAINE" + System.Environment.NewLine);
                    }
                    else
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = objResponce.VALUES.ToString();                        
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "DeclineProposal", "E-ServiceCallError", "", "", objResponce.VALUES.ToString());
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: DECLAINE" + System.Environment.NewLine);

                    }
                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Error While Declined policy,Please Contact System Admin";                
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "DeclineProposal", "E-ExceptionError", "", "", Error.ToString());
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : DECLAINE" + Error.Message + System.Environment.NewLine);
            }
        }

        public void PostponeProposal(string strPQuoteNo, DataSet _dsUWDec, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: UW DECISION-POSTPONE" + System.Environment.NewLine);
                LAUwdecisionService.ServiceClient objUWdecision = new LAUwdecisionService.ServiceClient();
                LAUwdecisionService.Master objResponce = new LAUwdecisionService.Master();

                if (_dsUWDec.Tables.Count > 0 && _dsUWDec.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < _dsUWDec.Tables[0].Rows.Count; i++)
                    {
                        strPolicyNo = _dsUWDec.Tables[0].Rows[i]["CHDRSEL"].ToString();
                        strReasonCode = _dsUWDec.Tables[0].Rows[i]["REASONCD"].ToString();
                        strReasondiscp = _dsUWDec.Tables[0].Rows[i]["RESNDESC"].ToString();
                        strdatetime = _dsUWDec.Tables[0].Rows[i]["DATIME"].ToString();
                        strPostPeriod = _dsUWDec.Tables[0].Rows[i]["ZPOSTPRD"].ToString();
                        strPostPeriodcode = _dsUWDec.Tables[0].Rows[i]["ZPSTPRCD"].ToString();
                        strBpmBranch = _dsUWDec.Tables[0].Rows[i]["BPMBRANCH"].ToString();
                        strBpmUserRole = _dsUWDec.Tables[0].Rows[i]["BPMUSERROLE"].ToString();
                        //strBpmUserId = _dsUWDec.Tables[0].Rows[0]["BPMUSERID"].ToString();
                        strBpmUserId = objChangeObj.userLoginDetails._UserID;
                        strpartnerId = "UWSaral";
                        strprocessID = "UWSaral";
                        strApplicationNo = strPQuoteNo;
                        objResponce = objUWdecision.PRPPST(strPolicyNo, strReasonCode, strReasondiscp, strPostPeriod, strPostPeriodcode, strdatetime, strBpmBranch, strBpmUserRole, strBpmUserId, strpartnerId, strprocessID, strApplicationNo);
                    }

                    if (objResponce.ERRORCODE == "0")
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = "Success";
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: POSTPONE" + System.Environment.NewLine);

                    }
                    else
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = objResponce.VALUES.ToString();                                             
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "PostponeProposal", "E-ServiceCallError", "", "", objResponce.VALUES.ToString());
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: POSTPONE" + System.Environment.NewLine);

                    }
                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Error While Postpon policy,Please Contact System Admin";                 
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "PostponeProposal", "E-ExceptionError", "", "", Error.ToString());
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : POSTPONE" + Error.Message + System.Environment.NewLine);

            }
        }

        public void ApproveProposal(string strPQuoteNo, DataSet _dsUWDec, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: UW DECISION-APPROVE" + System.Environment.NewLine);
                LAUwapprovalService.ServiceClient objUWdecision = new LAUwapprovalService.ServiceClient();
                LAUwapprovalService.Master objResponce = new LAUwapprovalService.Master();
                if (_dsUWDec.Tables.Count > 0 && _dsUWDec.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < _dsUWDec.Tables[0].Rows.Count; i++)
                    {
                        strPolicyNo = _dsUWDec.Tables[0].Rows[i]["CHDRSEL"].ToString();
                        strReasonCode = _dsUWDec.Tables[0].Rows[i]["REASONCD"].ToString();
                        strReasondiscp = _dsUWDec.Tables[0].Rows[i]["RESNDESC"].ToString();
                        strdatetime = _dsUWDec.Tables[0].Rows[i]["DATIME"].ToString();
                        
                        strBpmBranch = _dsUWDec.Tables[0].Rows[i]["BPMBRANCH"].ToString();
                        strBpmUserRole = _dsUWDec.Tables[0].Rows[i]["BPMUSERROLE"].ToString();
                        //strBpmUserId = _dsUWDec.Tables[0].Rows[0]["BPMUSERID"].ToString();
                        strBpmUserId = objChangeObj.userLoginDetails._UserID;
                        strpartnerId = "UWSaral";
                        strprocessID = "UWSaral";
                        strApplicationNo = strPQuoteNo;
                        objResponce = objUWdecision.UWACRT(strPolicyNo, strdatetime, strBpmBranch, strBpmUserRole, strBpmUserId, strpartnerId, strprocessID, strApplicationNo);
                    }
                    if (objResponce.ERRORCODE == "0")
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = "Success";
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: APPROVE" + System.Environment.NewLine);
                    }
                    else
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = objResponce.VALUES.ToString();                    
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "ApproveProposal", "E-ServiceCallError", "", "", objResponce.VALUES.ToString());
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: APPROVE" + System.Environment.NewLine);


                    }
                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Error While Approve policy,Please Contact System Admin";             
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "ApproveProposal", "E-ExceptionError", "", "", Error.ToString());
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : APPROVE" + Error.Message + System.Environment.NewLine);
            }
        }

        public void WithdrawProposal(string strPQuoteNo, DataSet _dsUWDec, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: UW DECISION-WITHDRAWN" + System.Environment.NewLine);
            try
            {
                Logger.Info(strPQuoteNo + "*******UW decision Withdrwan  Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
                LAUwdecisionService.ServiceClient objUWdecision = new LAUwdecisionService.ServiceClient();
                LAUwdecisionService.Master objResponce = new LAUwdecisionService.Master();
                if (_dsUWDec.Tables.Count > 0 && _dsUWDec.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < _dsUWDec.Tables[0].Rows.Count; i++)
                    {
                        strPolicyNo = _dsUWDec.Tables[0].Rows[i]["CHDRSEL"].ToString();
                        strReasonCode = _dsUWDec.Tables[0].Rows[i]["REASONCD"].ToString();
                        strReasondiscp = _dsUWDec.Tables[0].Rows[i]["RESNDESC"].ToString();
                        strdatetime = _dsUWDec.Tables[0].Rows[i]["DATIME"].ToString();
                        strBpmBranch = _dsUWDec.Tables[0].Rows[i]["BPMBRANCH"].ToString();
                        strBpmUserRole = _dsUWDec.Tables[0].Rows[i]["BPMUSERROLE"].ToString();
                        //strBpmUserId = _dsUWDec.Tables[0].Rows[0]["BPMUSERID"].ToString();
                        strBpmUserId = objChangeObj.userLoginDetails._UserID;
                        strpartnerId = "UWSaral";
                        strprocessID = "UWSaral";
                        strApplicationNo = strPQuoteNo;
                        objResponce = objUWdecision.PRPWDR(strPolicyNo, strReasonCode, strReasondiscp, strdatetime, strBpmBranch, strBpmUserRole, strBpmUserId, strpartnerId, strprocessID, strApplicationNo);
                    }
                    if (objResponce.ERRORCODE == "0")
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = objResponce.VALUES.ToString();   
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: WITHDRAWN" + System.Environment.NewLine);
                    }
                    else
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = "Failed";                        
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "WithdrawProposal", "E-ServiceCallError", "", "", objResponce.VALUES.ToString());
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: WITHDRAWN" + System.Environment.NewLine);

                    }

                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Error While Approve policy,Please Contact System Admin";            
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "WithdrawProposal", "E-ExceptionError", "", "", Error.ToString());
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : WITHDRAWN" + Error.Message + System.Environment.NewLine);
              
            }
        }

        public void ReverseDeclineWithdrawa(string strPQuoteNo, DataSet _dsUWDec, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: UW DECISION-ReverseDeclineWithdrawa" + System.Environment.NewLine);
            try
            {
                Logger.Info(strPQuoteNo + "*******UW decision ReverseDeclineWithdrawa  Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
                LAUwdecisionService.ServiceClient objUWdecision = new LAUwdecisionService.ServiceClient();
                LAUwdecisionService.Master objResponce = new LAUwdecisionService.Master();
                if (_dsUWDec.Tables.Count > 0 && _dsUWDec.Tables[0].Rows.Count > 0)
                {
                    for (i = 0; i < _dsUWDec.Tables[0].Rows.Count; i++)
                    {
                        strPolicyNo = _dsUWDec.Tables[0].Rows[i]["CHDRSEL"].ToString();
                        strReasonCode = _dsUWDec.Tables[0].Rows[i]["REASONCD"].ToString();
                        strReasondiscp = _dsUWDec.Tables[0].Rows[i]["RESNDESC"].ToString();
                        strBpmBranch = _dsUWDec.Tables[0].Rows[i]["BPMBRANCH"].ToString();
                        strBpmUserRole = _dsUWDec.Tables[0].Rows[i]["BPMUSERROLE"].ToString();
                        //strBpmUserId = _dsUWDec.Tables[0].Rows[0]["BPMUSERID"].ToString();
                        strBpmUserId = objChangeObj.userLoginDetails._UserID;
                        strpartnerId = "UWSaral";
                        strprocessID = "UWSaral";
                        strApplicationNo = strPQuoteNo;
                        objUWdecision.PRPRDW(strPolicyNo, strReasonCode, strReasondiscp, strBpmBranch, strBpmUserRole, strBpmUserId, strpartnerId, strprocessID, strApplicationNo);
                    }
                    if (objResponce.ERRORCODE == "0")
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = "Success";
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: ReverseDeclineWithdrawa" + System.Environment.NewLine);
                    }
                    else
                    {
                        strLAPushErrorcode = Convert.ToInt32(objResponce.ERRORCODE.ToString());
                        strLAPushStatus = objResponce.VALUES.ToString();  
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: ReverseDeclineWithdrawa" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "ReverseDeclineWithdrawa", "E-ServiceCallError", "", "", objResponce.VALUES.ToString());

                    }
                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Error While Approve UWReversal,Please Contact System Admin";             
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UWDecsionDetails.cs", "ReverseDeclineWithdrawa", "E-ExceptionError", "", "", Error.ToString());
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : ReverseDeclineWithdrawa" + Error.Message + System.Environment.NewLine);
               
            }
        }
    }
}
