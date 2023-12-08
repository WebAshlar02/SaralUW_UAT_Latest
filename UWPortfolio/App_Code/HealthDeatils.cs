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
/// <summary>
/// Summary description for HealthDeatils
/// </summary>
public class HealthDeatils
{
    #region Feild declaration begins.
    DataLayer objData = new DataLayer();
    DataSet _dsSMSEmail;
    DataSet _dsAppdtls;
    string strInputBo = string.Empty;
    string strOutpuBo = string.Empty;
    string strSender = string.Empty;
    string ErrorCode = string.Empty;
    string strSmsBody = string.Empty;
    string stremailBody = string.Empty;
    string stremailSub = string.Empty;
    string strReceiverName = string.Empty;
    string strMobileNo = string.Empty;
    string stremail = string.Empty;
    string BoString;
    #endregion Feild declaration begins End.

    #region Object declaration begins.

    //esbSendSMSSvc.sendSMS objReq = new esbSendSMSSvc.sendSMS();
    //esbSendSMSSvc.sendSMSResponse objRes = new esbSendSMSSvc.sendSMSResponse();
    //esbSendSMSSvc.FGSMSServiceBPMIntfClient objInvoke = new esbSendSMSSvc.FGSMSServiceBPMIntfClient();
    #endregion Object declaration End.
	/*added by shri on 04 july 17 to fetch questions from database*/
    public void FillHealthUserInput(ref DataSet _ds,string strApplnNo)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplnNo);
            _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_LA_HEALTH_DETAILS", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineUWCommentsDetails_Save", "E-Error", "", "", Error.ToString());
        }
    }

    public void FillBmiUserInput(ref DataSet _ds, string strApplnNo)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplnNo);
            _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_LA_BMI_DETAILS", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineUWCommentsDetails_Save", "E-Error", "", "", Error.ToString());
        }
    }

    public void FillHealthQuestionAnswerUserInput(ref DataSet _ds, string strApplnNo)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplnNo);
            _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_QUESTION_DETAILS", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineUWCommentsDetails_Save", "E-Error", "", "", Error.ToString());
        }
    }

    public void FillCriminalPoliticalDetails(ref DataSet _ds, string strApplnNo, string strAssuredType)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[2];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplnNo);
            _sqlparam[1] = new SqlParameter("@ASSURED_TYPE", strAssuredType);
            _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_CRIMINAL_POLITICAL_BACKGROUND_DETAILS", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineUWCommentsDetails_Save", "E-Error", "", "", Error.ToString());
        }
    }

    public void FillFamilyInfo(ref DataSet _ds, string strApplnNo)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplnNo);            
            _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_FAMILY_INFO", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineUWCommentsDetails_Save", "E-Error", "", "", Error.ToString());
        }
    }

    public void FillAssetInfo(ref DataSet _ds, string strApplnNo)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplnNo);            
            _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_ASSETS", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineUWCommentsDetails_Save", "E-Error", "", "", Error.ToString());
        }
    }

    public void FillSimulatniousApplication(ref DataSet _ds, string strApplnNo)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplnNo);
            _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_SIMULTANIOUS_APPLICATION", _sqlparam);
        }
        catch (Exception Error)
        {
            Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :OnlineUWCommentsDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
            //SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlineUWCommentsDetails_Save", "E-Error", "", "", Error.ToString());
        }
    }    

    /*end here*/
    //public void SaveErrorLogs(string strApplicationno, string strErrordiscp, string strApplsource, string strClassname, string strMethodename, string strLayercode, string strRequest, string strResponce, string strComment)
    //{
    //    try
    //    {
    //        Logger.Info(strApplicationno + "STAG 2 :PageName :Commfun.CS // MethodeName :SaveErrorLogs");
    //        SqlParameter[] _sqlparam = new SqlParameter[9];
    //        _sqlparam[0] = new SqlParameter("@ApplicationNo", strApplicationno);
    //        _sqlparam[1] = new SqlParameter("@ErrorLogDesc", strErrordiscp);
    //        _sqlparam[2] = new SqlParameter("@ApplicationSource", strApplsource);
    //        _sqlparam[3] = new SqlParameter("@ClassName", strClassname);
    //        _sqlparam[4] = new SqlParameter("@MethodName", strMethodename);
    //        _sqlparam[5] = new SqlParameter("@LayerCode", strLayercode);
    //        _sqlparam[6] = new SqlParameter("@XmlRequestData", strRequest);
    //        _sqlparam[7] = new SqlParameter("@XmlResponseData", strResponce);
    //        _sqlparam[8] = new SqlParameter("@Comments", strComment);
    //        objData.Insertrecord("USP_BPM_ErrorLogDetailsave_V2", _sqlparam);
    //    }
    //    catch (Exception Error)
    //    {
    //        Logger.Error(strApplicationno + "STAG 2 :PageName :Commfun.CS // MethodeName :SaveErrorLogs Error :" + System.Environment.NewLine + Error.ToString());
    //        //SendEmailSMS(Error.ToString(), "OnlineBMPScncIntegration Error : / Level/OnlineApplicationdetails");
    //    }
    //}
    //public void SendEmailSMS(string Error, string strSms)
    //{
    //    Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :SendEmailSMS");
    //    #region  Send sms-email Begin.
    //    ErrorCode = "";
    //    _dsSMSEmail = objData.RetrieveDataset_woParam("usp_Part_SMS_EMAILDetails__GET");
    //    strSmsBody = strSms;
    //    stremailBody = "Error Discription :" + Error.ToString();
    //    stremailSub = "OnlineBMPScyncIntegration Error/Level/CoreIntegrationFuncation";
    //    //strSmsBody = ConfigurationSettings.AppSettings["CCsmsText"];
    //    //stremailBody = ConfigurationSettings.AppSettings["CCemailText"];
    //    //stremailSub = ConfigurationSettings.AppSettings["CCemailSubject"];
    //    int x;
    //    for (x = 0; x < _dsSMSEmail.Tables[0].Rows.Count; x++)
    //    {
    //        strReceiverName = _dsSMSEmail.Tables[0].Rows[x]["ReceiverName"].ToString();
    //        strMobileNo = _dsSMSEmail.Tables[0].Rows[x]["MobileNo"].ToString();
    //        stremail = _dsSMSEmail.Tables[0].Rows[x]["EmailId"].ToString();
    //        SendSms("", strReceiverName, strMobileNo, stremail, strSmsBody);
    //        SendMail("", strReceiverName, strMobileNo, stremail, stremailBody, stremailSub);
    //    }
    //    #endregion #region  Send sms-email End
    //}
    //public void SendSms(string Pid, string Receiver, string MobNo, string Emailid, string smsbody)
    //{
    //    Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :SendSms");
    //    objReq.textMsg = smsbody.Replace("PID", Pid);
    //    objReq.toNumber = MobNo;
    //    try
    //    {
    //        objRes = objInvoke.sendSMS(objReq);
    //    }
    //    catch (Exception Error)
    //    {
    //        Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :SendSms Error :" + System.Environment.NewLine + Error.ToString());
    //        SaveErrorLogs(Pid, "Failed", "UWPortfolio", "Commfun.cs", "SendSms", "E-Error", "", "", Error.ToString());
    //    }
    //}
    //public void SendMail(string Pid, string Receiver, string MobNo, string Emailid, string emailbody, string emailSubject)
    //{

    //    try
    //    {
    //        Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :SendMail");
    //        SmtpClient objsmtpClient = new SmtpClient();
    //        strSender = ConfigurationSettings.AppSettings["Sender"];
    //        objsmtpClient.Host = ConfigurationSettings.AppSettings["Host"]; ;
    //        objsmtpClient.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["Port"]); ;
    //        MailMessage objMailMessage = new MailMessage();
    //        objMailMessage.From = new MailAddress(strSender);
    //        objMailMessage.To.Add(Emailid);
    //        objMailMessage.Subject = emailSubject;
    //        objMailMessage.Body = emailbody;
    //        objsmtpClient.Send(objMailMessage);
    //    }
    //    catch (Exception Error)
    //    {
    //        Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :SendMail Error :" + System.Environment.NewLine + Error.ToString());
    //        SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "SendMail", "E-Error", "", "", Error.ToString());
    //    }
    //}
}