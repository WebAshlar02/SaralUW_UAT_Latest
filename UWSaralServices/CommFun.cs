/*
**********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: added by shri to change mis status to resolve
DateTime :13Mar18
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT:BUSSINESS LAYER
REMARK: Add log to table
DateTime :22OCT2018
*********************************************************************************************************************************
* ************************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT: UWSARAL_TPALast_Schedular_Log
REMARK: FETCH REGISTERED CASES
DateTime :27FEB2019
**********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:DAO
REMARK: revamping of tpa
DateTime :11Mar19
*********************************************************************************************************************************

 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Configuration;
namespace UWSaralServices
{          
   public  class CommFun
    {
        DataSet _dsSMSEmail;
        DataLayer objData = new DataLayer();
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

        #region Object declaration begins.
        
        esbSendSMSSvc.sendSMS objReq = new esbSendSMSSvc.sendSMS();
        esbSendSMSSvc.sendSMSResponse objRes = new esbSendSMSSvc.sendSMSResponse();
        esbSendSMSSvc.FGSMSServiceBPMIntfClient objInvoke = new esbSendSMSSvc.FGSMSServiceBPMIntfClient();
        #endregion Object declaration End.

        #region common funcation Begins.
        public void OnlineTsmrMsarDisplayDetails_GET(ref DataSet _ds, string strPQuoteNo, string strChanneltype)
        {
            try
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET");
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strChanneltype);
                _ds = objData.RetrieveDataset("SPR_UWSARAL_MSARTSARFETCHDETAILS_GET", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(strPQuoteNo, "Failed", "UWPortfolio", "Commfun.cs", "OnlineTsmrMsarDisplayDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void BankEnqManageDetails_GET(string strApplicationNo, ref DataSet _ds)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :BankEnqManageDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NUMBER", strApplicationNo);
                _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_SERVICE_BANK_ENQ_MANAGE", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :BankEnqManageDetails_GET");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "BankEnqManageDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void PreviouspolicyDetails_GET(ref DataSet _ds, string strPQuoteNo, string strClientNo, string strAssuredType, string strAppStatus, ref int stroutput)
        {
            try
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET");
                SqlParameter[] _sqlparam = new SqlParameter[4];

                _sqlparam[0] = new SqlParameter("@Clientid", strClientNo);
                _sqlparam[1] = new SqlParameter("@assuredtype", strAssuredType);
                _sqlparam[2] = new SqlParameter("@appStatus", strAppStatus);
                _sqlparam[3] = new SqlParameter("@b", stroutput);
                _sqlparam[3].Direction = ParameterDirection.Output;
                _ds = objData.RetrieveDataset("Usp_esb_fetchprevPolDetails", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(strPQuoteNo, "Failed", "UWPortfolio", "Commfun.cs", "OnlineTsmrMsarDisplayDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void MSARTSARKeyvalue_GET(ref DataSet _ds, string strPQuoteNo, string strProdcode,string strKeyvalue)
        {
            try
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET");
                SqlParameter[] _sqlparam = new SqlParameter[2];

                _sqlparam[0] = new SqlParameter("@prodcode", strProdcode);
                _sqlparam[1] = new SqlParameter("@src", strKeyvalue);              
                _ds = objData.RetrieveDataset_Lombardimatersync("usp_gethealthproducts", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(strPQuoteNo, "Failed", "UWPortfolio", "Commfun.cs", "OnlineTsmrMsarDisplayDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }



        public void OnlineCRTUPDRiderDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineCRTUPDRiderDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _ds = objData.RetrieveDataset("USP_UWSralCrtUpdRiderDetails_Get", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineCRTUPDRiderDetails_GET");
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "Commfun.cs", "OnlineCRTUPDRiderDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineBussinessDate_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineBussinessDate_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@appno", strPQuoteNo);
                //_ds = objData.RetrieveDataset("spr_UWSaralBussinessDate_Get", _sqlparam);
                _ds = objData.RetrieveDataset("GetBusinessDateDEQC_V2_New", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineBussinessDate_GET");
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "Commfun.cs", "OnlineBussinessDate_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void SaveErrorLogs(string strApplicationno, string strErrordiscp, string strApplsource, string strClassname, string strMethodename, string strLayercode, string strRequest, string strResponce, string strComment)
        {
            Logger.Info(strApplicationno+":PageName :UWSaralDecision/CommFun.CS // MethodeName :SaveErrorLogs");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[9];
                _sqlparam[0] = new SqlParameter("@ApplicationNo", strApplicationno);
                _sqlparam[1] = new SqlParameter("@ErrorLogDesc", strErrordiscp);
                _sqlparam[2] = new SqlParameter("@ApplicationSource", strApplsource);
                _sqlparam[3] = new SqlParameter("@ClassName", strClassname);
                _sqlparam[4] = new SqlParameter("@MethodName", strMethodename);
                _sqlparam[5] = new SqlParameter("@LayerCode", strLayercode);
                _sqlparam[6] = new SqlParameter("@XmlRequestData", strRequest);
                _sqlparam[7] = new SqlParameter("@XmlResponseData", strResponce);
                _sqlparam[8] = new SqlParameter("@Comments", strComment);
                objData.Insertrecord("USP_BPM_ErrorLogDetailsave_V2", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strApplicationno + "STAG 2 :PageName :UWSaralDecision/CommFun.CS // MethodeName :SaveErrorLogs");
                SendEmailSMS(Error.ToString(), "OnlineBMPScncIntegration Error : / Level/OnlineApplicationdetails");
            }
        }
        public void SavePdfToTiffIssue(string strApplicationno, string strErrordiscp, string strClassname, string strMethodename)
        {
            Logger.Info(strApplicationno + ":PageName :UWSaralDecision/CommFun.CS // MethodeName :SavePdfToTiffIssue");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[4];
                _sqlparam[0] = new SqlParameter("@ApplicationNo", strApplicationno);
                _sqlparam[1] = new SqlParameter("@ErrorLogDesc", strErrordiscp);
                _sqlparam[2] = new SqlParameter("@ClassName", strClassname);
                _sqlparam[3] = new SqlParameter("@MethodName", strMethodename);
                objData.Insertrecord("Sp_UWSARAL_SchedulerTPA_PdfToTiff_Log", _sqlparam);
            }
            catch (Exception Error)
            {
            }
        }
        public void SendEmailSMS(string Error, string strSms)
        {
            #region  Send sms-email Begin.
            ErrorCode = "";
            _dsSMSEmail = objData.RetrieveDataset_woParam("usp_Part_SMS_EMAILDetails__GET");
            strSmsBody = strSms;
            stremailBody = "Error Discription :" + Error.ToString();
            stremailSub = "OnlineBMPScyncIntegration Error/Level/CoreIntegrationFuncation";
            //strSmsBody = ConfigurationSettings.AppSettings["CCsmsText"];
            //stremailBody = ConfigurationSettings.AppSettings["CCemailText"];
            //stremailSub = ConfigurationSettings.AppSettings["CCemailSubject"];
            int x;
            for (x = 0; x < _dsSMSEmail.Tables[0].Rows.Count; x++)
            {
                strReceiverName = _dsSMSEmail.Tables[0].Rows[x]["ReceiverName"].ToString();
                strMobileNo = _dsSMSEmail.Tables[0].Rows[x]["MobileNo"].ToString();
                stremail = _dsSMSEmail.Tables[0].Rows[x]["EmailId"].ToString();
                SendSms("", strReceiverName, strMobileNo, stremail, strSmsBody);
                SendMail("", strReceiverName, strMobileNo, stremail, stremailBody, stremailSub);
            }
            #endregion #region  Send sms-email End
        }
        public void SendSms(string Pid, string Receiver, string MobNo, string Emailid, string smsbody)
        {
            objReq.textMsg = smsbody.Replace("PID", Pid);
            objReq.toNumber = MobNo;
            try
            {
                objRes = objInvoke.sendSMS(objReq);
            }
            catch (Exception Error)
            {
                SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "SendSms", "E-Error", "", "", Error.ToString());
            }
        }
        public void SendMail(string Pid, string Receiver, string MobNo, string Emailid, string emailbody, string emailSubject)
        {

            try
            {
                SmtpClient objsmtpClient = new SmtpClient();
                strSender = ConfigurationSettings.AppSettings["Sender"];
                objsmtpClient.Host = ConfigurationSettings.AppSettings["Host"]; ;
                objsmtpClient.Port = Convert.ToInt32(ConfigurationSettings.AppSettings["Port"]); ;
                MailMessage objMailMessage = new MailMessage();
                objMailMessage.From = new MailAddress(strSender);
                objMailMessage.To.Add(Emailid);
                objMailMessage.Subject = emailSubject;
                objMailMessage.Body = emailbody;
                objsmtpClient.Send(objMailMessage);
            }
            catch (Exception Error)
            {
                SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "SendMail", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineProductDetails_Save(string strAppType, string strapplicationNumber, string strPolNumber, string strProductCode, string strPOL_sumAssured, string strPOL_policyTerm, string strPOL_premPayingTerm, string strPOL_premPayingFreq, string strPREM_basePremium, string strPREM_totalPremium, string strPREM_serviceTaxBasePremium, string strMonthlyPayout, ref int PolResult,
            string POL_riskCommencementStrDate,string UserID)
        {
            try
            {
                Logger.Info(strapplicationNumber + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineProductDetails_Save");
                SqlParameter[] _sqlparam = new SqlParameter[14];
                _sqlparam[0] = new SqlParameter("@AppType", strAppType);
                _sqlparam[1] = new SqlParameter("@AppNo", strapplicationNumber);
                _sqlparam[2] = new SqlParameter("@PolNo", strPolNumber);
                _sqlparam[3] = new SqlParameter("@POL_sumAssured", strPOL_sumAssured);
                _sqlparam[4] = new SqlParameter("@POL_policyTerm", strPOL_policyTerm);
                _sqlparam[5] = new SqlParameter("@POL_premPayingTerm", strPOL_premPayingTerm);
                _sqlparam[6] = new SqlParameter("@POL_premPayingFreq", strPOL_premPayingFreq);
                _sqlparam[7] = new SqlParameter("@PREM_basePremium", strPREM_basePremium);
                _sqlparam[8] = new SqlParameter("@PREM_totalPremium", strPREM_totalPremium);
                _sqlparam[9] = new SqlParameter("@PREM_serviceTaxBasePremium", strPREM_serviceTaxBasePremium);
                _sqlparam[10] = new SqlParameter("@ProductCode", strProductCode);
                _sqlparam[11] = new SqlParameter("@MonthlyPayout", strMonthlyPayout);
                _sqlparam[12] = new SqlParameter("@POL_riskCommencementStrDate",Convert.ToDateTime(POL_riskCommencementStrDate));
                _sqlparam[13] = new SqlParameter("@UserID", UserID);
                PolResult = objData.Insertrecord("USP_BPM_ProductDetailsSave_V2", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strapplicationNumber + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineProductDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(strapplicationNumber, "Failed", "UWPortfolio", "Commfun.cs", "OnlineProductDetails_Save", "E-Error", "", "", Error.ToString());
            }
        }

       /*added by shri on 27 july 17 to add log*/
        public void MaintainLog(string strFEMethodName, string strLaMethodName, string strPatnerRequest, string strPartnerResponse, string strLARequest, string strLAResponse, string strPartnerId
                , string strProcessID, string strError, string strApplicationNo)
        {
            SqlParameter[] _sqlparam = new SqlParameter[10];
            _sqlparam[0] = new SqlParameter("@FEMethodName", strFEMethodName);
            _sqlparam[1] = new SqlParameter("@LAMethodName", strLaMethodName);
            _sqlparam[2] = new SqlParameter("@partnerRequest", strPatnerRequest);
            _sqlparam[3] = new SqlParameter("@partnerResponse", strPartnerResponse);
            _sqlparam[4] = new SqlParameter("@LARequest", strLARequest);
            _sqlparam[5] = new SqlParameter("@LAResponse", strLAResponse);
            _sqlparam[6] = new SqlParameter("@partnerID", strPartnerId);
            _sqlparam[7] = new SqlParameter("@processID", strProcessID);
            _sqlparam[8] = new SqlParameter("@error", strError);
            _sqlparam[9] = new SqlParameter("@applicationNo", strApplicationNo);
            objData.Insertrecord("SP_AS400_Logs", _sqlparam);
        }

        /*added by shri on 03 july 17 to create specific serialize data into xml format */
        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        /*added by shri on 13 sept 17 to call ofac details service*/
        public void UpdateMandateReferenceNo(string strApplicationNo, string strMandateNo, ref int intRet)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :BankEnqManageDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplicationNo);
                _sqlparam[1] = new SqlParameter("@MANDATE_NO", strMandateNo);
                intRet = objData.Insertrecord("USP_UWSARAL_UPDATE_MANDATE_REF_NO", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :BankEnqManageDetails_GET");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "BankEnqManageDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }
        /*end here*/
       /*end here*/
        #endregion Common funcation End.

        public static string AadharEncrypt(string clearText)
        {
            string EncryptionKey = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Ekey"]);
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public void UWSaralTPA_Fetch_document_To_Be_Pushed(ref DataSet _ds)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :UWSaralTPA_Fetch_document_To_Be_Pushed");
                //_ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_FETCH_DMS_FILE");
                _ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_FETCH_DMS_FILE");
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UWSaralTPA_Fetch_document_To_Be_Pushed Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "UWSaralTPA_Fetch_document_To_Be_Pushed", "E-Error", "", "", Error.ToString());
            }
        }

        public void UWSaralTPAUpdateDocumentStatus(DataTable _dt, ref int intRef)
        {
            try
            {
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus Start" + System.Environment.NewLine);
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@TYP_UWSARAL_TPA_DMS_FILES", _dt);
                intRef = objData.Insertrecord("USP_UWSARAL_TPA_UPDATE_DOCUMENT_STATUS", _sqlparam);
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus End" + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "UWSaralTPAUpdateDocumentStatus", "E-Error", "", "", Error.ToString());
            }
        }
        //For MER
        public void UWSaralTPAUpdateDocumentStatus_MER(DataTable _dt, ref int intRef)
        {
            try
            {
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus Start" + System.Environment.NewLine);
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@TYP_UWSARAL_TPA_DMS_FILES_MER", _dt);
                intRef = objData.Insertrecord("USP_UWSARAL_TPA_UPDATE_DOCUMENT_STATUS_MER", _sqlparam);
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus End" + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "UWSaralTPAUpdateDocumentStatus", "E-Error", "", "", Error.ToString());
            }
        }

        /*ID:1 START */
        public void UpdateTPAResolveStatus(string strApplicationNumber,ref int intRetVal)
        {
            try
            {
                Logger.Info("STAG 6 :PageName :Commfun.CS // MethodeName :UpdateTPAResolveStatus Start" + System.Environment.NewLine);
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NUMBER", strApplicationNumber);                
                intRetVal = objData.Insertrecord("USP_UWSARAL_TPA_MAINTAIN_APPLICATION_BUKET_V1", _sqlparam);
                Logger.Info("STAG 6 :PageName :Commfun.CS // MethodeName :UpdateTPAResolveStatus End" + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 6 :PageName :Commfun.CS // MethodeName :UpdateTPAResolveStatus Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UpdateTPAResolveStatus", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:1 END */
        /*ID:2 START */
        public void InsertDMSLog(string strApplicationNumber,string strDocInpath, ref int intRetVal)
        {
            try
            {
                Logger.Info("STAG 6 :PageName :Commfun.CS // MethodeName :InsertDMSLog Start" + System.Environment.NewLine);
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NUMBER", strApplicationNumber);
                _sqlparam[1] = new SqlParameter("@DocInPath", strDocInpath);
                intRetVal = objData.Insertrecord("USP_UWSARAL_INSERT_LOG", _sqlparam);
                Logger.Info("STAG 6 :PageName :Commfun.CS // MethodeName :InsertDMSLog End" + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 6 :PageName :Commfun.CS // MethodeName :InsertDMSLog Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "InsertDMSLog", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:2 END */
        /*ID:3 START */
        public void UWSARAL_TPALast_Schedular_Log(string ftp, ref int intRetVal)
        {
            try
            {
                Logger.Info("STAG 6 :PageName :Commfun.CS // MethodeName :UWSARAL_TPALast_Schedular_Log Start" + System.Environment.NewLine);
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@Code", ftp);
                intRetVal = objData.Insertrecord("USP_UWSARAL_TPA_MANAGE_LAST_SCHEDULAR_LOGS", _sqlparam);
                Logger.Info("STAG 6 :PageName :Commfun.CS // MethodeName :UpdateTPAResolveStatus End" + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 6 :PageName :Commfun.CS // MethodeName :UWSARAL_TPALast_Schedular_Log Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "TPADMSSRVICE", "Commfun.cs", "UWSARAL_TPALast_Schedular_Log", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:3 END */
        /*ID:4 START*/
        public void FetchMedicalReceived(ref DataSet _ds)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :FetchMedicalReceived");
                //_ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_FETCH_DMS_FILE");
                _ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_FETCH_DMS_FILE");
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :FetchMedicalReceived Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "FetchMedicalReceived", "E-Error", "", "", Error.ToString());
            }
        }

        public void UWSaralTPAUpdateDocumentStatus(string strFgiRefno, int intFlag, string strMessage, ref int intRef)
        {
            try
            {
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus Start" + System.Environment.NewLine);
                SqlParameter[] _sqlparam = new SqlParameter[3];
                _sqlparam[0] = new SqlParameter("@FGIREFNO", strFgiRefno);
                _sqlparam[1] = new SqlParameter("@RESPONCE", intFlag);
                _sqlparam[2] = new SqlParameter("@MESSAGE", strMessage);
                intRef = objData.Insertrecord("USP_UWSARAL_TPA_REVAMP_UPDATE_DOCUMENT_STATUS", _sqlparam);
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus End" + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "UWSaralTPAUpdateDocumentStatus", "E-Error", "", "", Error.ToString());
            }
        }

        public void FetchRevampMedicalReceived(ref DataSet _ds)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :FetchRevampMedicalReceived");
                //_ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_FETCH_DMS_FILE");
                _ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_REVAMP_FETCH_DMS_FILE");
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :FetchRevampMedicalReceived Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "FetchRevampMedicalReceived", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:4 END*/
        //Begin of Changes CR-28153


        public void FetchFundDetails(ref DataSet dsFunddetails, string Flag, string Applicationno)
        {

            try
            {
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus Start" + System.Environment.NewLine);
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@Flag", Flag);
                _sqlparam[1] = new SqlParameter("@Applicationno", Applicationno);

                dsFunddetails = objData.FetchFundDetails("USP_Fetch_Client_Details_Consent_letter", _sqlparam);
                Logger.Info("STAG 7 :PageName :Commfun.CS // MethodeName :UWSaralTPAUpdateDocumentStatus End" + System.Environment.NewLine);

            }
            catch (Exception ex)
            {
                //Logger.Error(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineProductDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
                //SaveErrorLogs(strPQuoteNo, "Failed", "UWPortfolio", "Commfun.cs", "OnlineProductDisplayDetails_GET", "E-Error", "", "", Error.ToString());
            }

        }



        //END of Changes CR-28153
    }
}
