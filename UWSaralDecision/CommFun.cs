/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: 
DateTime :13JAN2017
**********************************************************************************************************************************
 **********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: add tpa service
DateTime :06Feb18
**********************************************************************************************************************************
  **********************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: add NEW CONTRACT MODIFICATION (ONLY IN PRE-PROD )
DateTime :23Feb18
**********************************************************************************************************************************
 ***********************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :AJAY SAHU
METHODE/EVENT:WarningMessage
REMARK: Fetch Warning Messages
DateTime :23May18
**********************************************************************************************************************************
*  ***********************************************************************************************************************************
COMMENT ID: 5
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT:GET_DC_PINCODE
REMARK: Fetch DC PINCODE AND CUSTOMER PINCODE FOR CALCULATION DISTANCE FOR GET DC CODE WITHIN 100 KM
DateTime :27JULY18
**********************************************************************************************************************************
*  ***********************************************************************************************************************************
COMMENT ID: 6
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT: GetRegisterMIS
REMARK: FETCH REGISTERED CASES
DateTime :06JAN19
************************************************************************************************************************************
COMMENT ID: 7
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT: UWSARAL_TPALast_Schedular_Log
REMARK: FETCH REGISTERED CASES
DateTime :27FEB2019
**********************************************************************************************************************************
*  ***********************************************************************************************************************************
COMMENT ID: 9
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT: 
REMARK: TPA REVAMP
DateTime :09MAR19
**********************************************************************************************************************************
* 
 ************************************************************************************************************************************
COMMENT ID: 10
COMMENTOR NAME :DINESH KONDABATTINI
METHODE/EVENT: 
REMARK: CHECK MWP APPLICATION NO
DateTime :25NOV19
**********************************************************************************************************************************
  ************************************************************************************************************************************
COMMENT ID: 11
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT: 
REMARK: AML risk categorisation in Life Asia
DateTime 22JUN2022
**********************************************************************************************************************************
 
  ************************************************************************************************************************************
COMMENT ID: 12
COMMENTOR NAME :Bhaumik Patel
METHODE/EVENT: 
REMARK: CR-5523 Death Benefit details required in Saral
DateTime 28 March 2023
**********************************************************************************************************************************
 
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using Platform.Utilities.LoggerFramework;

namespace UWSaralDecision
{
    public class CommFun
    {
        DataLayer objData = new DataLayer();
        DataSet _dsSMSEmail;
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
        public void SaveErrorLogs(string strApplicationno, string strErrordiscp, string strApplsource, string strClassname, string strMethodename, string strLayercode, string strRequest, string strResponce, string strComment)
        {
            Logger.Info(strApplicationno + ":PageName :UWSaralServices/CommFun.CS // MethodeName :SaveErrorLogs");
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
                Logger.Error(strApplicationno + ":PageName :UWSaralServices/CommFun.CS // MethodeName :SaveErrorLogs");
                SendEmailSMS(Error.ToString(), "OnlineBMPScncIntegration Error : / Level/OnlineApplicationdetails");
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
        #endregion Common funcation End.
        #region Life Asia Service methodes Begins.


        public void OnlineServiceUWDecisionDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : UW DECEISION" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _ds = objData.RetrieveDataset("USP_BPM_PolicyUWDecisionDetailsGet_V2", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : UW DECEISION" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceUWDecisionDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineServiceFollowUPDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : REQUIRMENTS" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _ds = objData.RetrieveDataset("USP_BPM_PolicyFollowpDetailsGet_V2", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : REQUIRMENTS" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceFollowUPDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }


        public void OnlineServiceSTPDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServiceSTPDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _ds = objData.RetrieveDataset("USP_BPM_PolicySTPDetailsGet_V2", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServiceSTPDetails_GET");
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceSTPDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }


        public void OnlineServiceContractDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : Contract Modification" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                if (strAppType.ToUpper() == "ONLINE")
                {
                    /*ID:3 START*/
                    //_ds = objData.RetrieveDataset("USP_BPM_PolicyCRTUPDDetailsGet_V2", _sqlparam);
                    //_ds = objData.RetrieveDataset("USP_BPM_PolicyCRTUPDDetailsGet_V3", _sqlparam);
                    _ds = objData.RetrieveDataset("USP_BPM_PolicyCRTUPDDetailsGet_V3_COMBI", _sqlparam);
                    /*ID:3 END*/
                }
                else
                {
                    _ds = objData.RetrieveDataset("USP_BPM_CreateContract_UW_SARAL_V2", _sqlparam);
                    //_ds = objData.RetrieveDataset("USP_BPM_CreateContract_UW_SARAL_V1_BKP_AMIT", _sqlparam);
                }

            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : CONTRACT MODIFICATION" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceContractDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineServicePancardDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServicePancardDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _ds = objData.RetrieveDataset("usp_UWOnlinePanDetails_Get", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServicePancardDetails_GET");
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceContractDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineServicePremiumCalDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : PREMIUMCAL" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                //_ds = objData.RetrieveDataset("USP_BPM_PremiumcalculationDetailsSave_V2", _sqlparam);
                _ds = objData.RetrieveDataset("USP_BPM_PremiumcalculationDetailsSave_V3", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : PREMIUMCAL" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServicePremiumCalDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineServiceLoadingDetails_GET(ref DataSet _ds, string strPQuoteNo, string strLoadingCode, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : Loading" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[3];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _sqlparam[2] = new SqlParameter("@LoadingCode", strLoadingCode);
                //Bhaumik Patel Cr 3334
                _ds = objData.RetrieveDataset("USP_BPM_PolicyLoadingDetailsGet_V2_CR_3334", _sqlparam);
                //
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : Loading" + Error.Message+System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceLoadingDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineServiceAmlDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : AML" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                //_ds = objData.RetrieveDataset("usp_IntegrateWithFGLIAMLUpdation", _sqlparam);
                _ds = objData.RetrieveDataset("usp_IntegrateWithFGLIAMLUpdation_Test", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : AML" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceAmlDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineServicePreIssuevalDetails_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : PREISSUEVAL" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _ds = objData.RetrieveDataset("USP_BPM_PolicyPreIssueValDetailsGet_V2", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : PREISSUEVAL" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServicePreIssuevalDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        #endregion Life Asia Service methodes Begins.

        /*added by shri on 14 june 17 to encode and decode string*/
        public static string Base64EncodingMethod(string sData)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(sData));
        }

        public static string Base64DecodingMethod(string sData)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(sData));
            //return "";
        }
        /*added by shri on 20 june 17 to fetch contract modification*/
        public void OnlineServiceClientDetails_GET(ref DataSet _ds, string strPQuoteNo, string strClientId)
        {
            Logger.Info(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServiceClientDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NUMBER", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@ClientId", strClientId);
                _ds = objData.RetrieveDataset("STP_FETCH_CLIENT_DETAILS", _sqlparam);


            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServiceClientDetails_GET");
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceClientDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public void OnlineServiceRecepitDetails_GET(ref DataSet _ds, string strPQuoteNo)
        {
            Logger.Info(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServiceClientDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NUMBER", strPQuoteNo);
                _ds = objData.RetrieveDataset("USP_FETCH_RECEIPT_DETAILS", _sqlparam);


            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + ":PageName :UWSaralServices/CommFun.CS // MethodeName :OnlineServiceClientDetails_GET");
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineServiceClientDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }

        public string CreateClient_PUSH(string clientCode, string clientType, string applicationNo, string strSalutation, string strClntName,
        string strClntSurName, string strClntDOB, string strGender, string strPerflang, string strCountryofBirth, string strOccupation,
        string strLAAdress1, string strLAAdress2, string strLAAdress3, string strLALandmark, string strClntPincode, string strLACity,
        string strLADistrict, string strLAState, string strLACountry,
        string strLAEmail, string strClntMobileNo, string strClntAlternateNo, string strIsMarried, string strPANNo, string strAddressType)
        {
            try
            {
                #region

                #endregion

                string strStatus = string.Empty;

                SqlParameter[] _sqlparam = new SqlParameter[41];
                _sqlparam[0] = new SqlParameter("@CLNTNUM", clientCode);

                //--LF_cltHeader
                _sqlparam[1] = new SqlParameter("@CLTM_clientType", clientType);
                _sqlparam[2] = new SqlParameter("@CLTM_salutation_SALUTAL", strSalutation);
                _sqlparam[3] = new SqlParameter("@CLTM_firstName_LGIVNAME", strClntName);
                _sqlparam[4] = new SqlParameter("@CLTM_lastName_LSURNAME", strClntSurName);
                _sqlparam[5] = new SqlParameter("@CLTM_dob_CLTDOBX", Convert.ToDateTime(strClntDOB));
                _sqlparam[6] = new SqlParameter("@CLTM_gender_CLTSEX", strGender);
                _sqlparam[7] = new SqlParameter("@CLTM_prefLanguage", strPerflang);

                _sqlparam[8] = new SqlParameter("@CLTM_clientStatus_CLTSTAT", string.Empty);
                _sqlparam[9] = new SqlParameter("@CLTM_panId_RTAXIDNUM", null);
                _sqlparam[10] = new SqlParameter("@CLTM_maritalStatus_MARRYD", strIsMarried);
                _sqlparam[11] = new SqlParameter("@CLTM_nationality_NATLTY", strCountryofBirth);
                _sqlparam[12] = new SqlParameter("@CLTM_servBranch_SERVBRH", "10");
                _sqlparam[13] = new SqlParameter("@CLTM_occupationCode_OCCPCODE", strOccupation);
                _sqlparam[14] = new SqlParameter("@CLTM_givenName_calc", strClntName);
                _sqlparam[15] = new SqlParameter("@CLTM_surname_calc", strClntSurName);

                //--LF_cltCncts
                _sqlparam[16] = new SqlParameter("@CNCT_addressType", strAddressType);
                _sqlparam[17] = new SqlParameter("@CNCT_addressLine1_CLTADDR01", strLAAdress1);
                _sqlparam[18] = new SqlParameter("@CNCT_addressLine2_CLTADDR02", strLAAdress2);
                _sqlparam[19] = new SqlParameter("@CNCT_addressLine3_CLTADDR03", strLAAdress3);
                _sqlparam[20] = new SqlParameter("@CNCT_city", strLACity);
                _sqlparam[21] = new SqlParameter("@CNCT_state", strLAState);
                _sqlparam[22] = new SqlParameter("@CNCT_pinCode_CLTPCODE", strClntPincode);
                _sqlparam[23] = new SqlParameter("@CNCT_countryCode_CTRYCODE", strLACountry);
                _sqlparam[24] = new SqlParameter("@CNCT_landmark", strLALandmark);
                _sqlparam[25] = new SqlParameter("@CNCT_district", strLADistrict);
                _sqlparam[26] = new SqlParameter("@CNCT_mobileNo_MBLPHONE", strClntMobileNo);
                _sqlparam[27] = new SqlParameter("@CNCT_phone1_CLTPHONE01", strClntAlternateNo);
                _sqlparam[28] = new SqlParameter("@emailId_RINTERNET", (!String.IsNullOrEmpty(strLAEmail) ? strLAEmail : null));

                //--LF_cltAdditionalInfo
                _sqlparam[29] = new SqlParameter("@ApplicationNo", applicationNo);
                _sqlparam[30] = new SqlParameter("@BirthPlace", strLACountry);
                //bpm_userID	bpm_userName	bpmgrp	bpm_branchCode	bpm_creationDate	bpm_systemDate	bpm_businessDate	bpm_userBranch	bpm_processName
                // MFL00248	FMFL00248	        NULL	KAN	            2016-09-26 00:00:00.000	2016-09-26 00:00:00.000	2016-09-26 00:00:00.000	Kanpur Branch Office	Manual Underwriting
                _sqlparam[31] = new SqlParameter("@bpm_userID", "MFL00248");//strUserID
                _sqlparam[32] = new SqlParameter("@bpm_userName", "FMFL00248");//strUserName
                _sqlparam[33] = new SqlParameter("@bpmgrp", "NULL");//strGrp
                _sqlparam[34] = new SqlParameter("@bpm_branchCode", "KAN");//strBranchCode
                _sqlparam[35] = new SqlParameter("@bpm_creationDate", System.DateTime.Now.ToString());//strCreationDate
                _sqlparam[36] = new SqlParameter("@bpm_systemDate", System.DateTime.Now.ToString());//strSystemDate
                _sqlparam[37] = new SqlParameter("@bpm_businessDate", System.DateTime.Now.ToString());//strBusinessDate
                _sqlparam[38] = new SqlParameter("@bpm_userBranch", "");//strUserBranch
                _sqlparam[39] = new SqlParameter("@bpm_processName", "Kanpur Branch Office");
                _sqlparam[40] = new SqlParameter("@bpm_applicationName", applicationNo);
                objData.Insertrecord("usp_Save_ClientData", _sqlparam);
                return strStatus;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string ChangeReceiptStatus_PUSH(string strAppno, string strReceiptStatus, string strRecepitCancelationReason)
        {
            string strRet = string.Empty;
            Logger.Info(strAppno + ":PageName :UWSaralServices/CommFun.CS // MethodeName :ChangeReceiptStatus_PUSH");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[3];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NUMBER", strAppno);
                _sqlparam[1] = new SqlParameter("@RECEIPT_STATUS", strReceiptStatus);
                _sqlparam[2] = new SqlParameter("@RECEIPT_CANCELATION_REASON", strRecepitCancelationReason);
                objData.Insertrecord("USP_CHANGE_RECEIPT_STATUS_BY_APPLICATION_NO", _sqlparam);


            }
            catch (Exception Error)
            {
                Logger.Error(strAppno + ":PageName :UWSaralServices/CommFun.CS // MethodeName :ChangeReceiptStatus_PUSH");
                SaveErrorLogs(strAppno, "Failed", "UWSaralDecision", "Commfun.cs", "ChangeReceiptStatus_PUSH", "E-Error", "", "", Error.ToString());
            }
            return strRet;
        }

        public void LogClientProfile_Push(string strOldClient, string strNewClient, int intAction, string strBmpUserId, string strBmpUserName, string strBmpGrp, ref int intRetVal)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :LogClientProfile_Push");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[6];
                _sqlparam[0] = new SqlParameter("@OldValue", strOldClient);
                _sqlparam[1] = new SqlParameter("@NewValue", strNewClient);
                _sqlparam[2] = new SqlParameter("@Action", intAction);
                _sqlparam[3] = new SqlParameter("@BmpUserId", strBmpUserId);
                _sqlparam[4] = new SqlParameter("@BmpUserName", strBmpUserName);
                _sqlparam[5] = new SqlParameter("@BmpGrp", strBmpGrp);
                intRetVal = objData.Insertrecord("USP_INSERT_LOG_CLIENT_PROFILE", _sqlparam);
            }
            catch (Exception Error)
            {
                intRetVal = -1;
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :LogClientProfile_Push");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "LogClientProfile_Push", "E-Error", "", "", Error.ToString());
            }
        }

        /*added by shri on 07 sept 17 to call bank details service*/
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

        public void OnlineTsmrMsarDisplayDetails_GET(ref DataSet _ds, string strPQuoteNo, string strChanneltype)
        {
            try
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET");
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strChanneltype);
                //_ds = objData.RetrieveDataset("SPR_UWSARAL_MSARTSARFETCHDETAILS_GET", _sqlparam);
                //_ds = objData.RetrieveDataset("SPR_UWSARAL_MSARTSARFETCHDETAILS_GET_V1", _sqlparam);
                //12.1 Begin of Changes; Bhaumik Patel-[CR-5523]
                //_ds = objData.RetrieveDataset("SPR_UWSARAL_MSARTSARFETCHDETAILS_GET_V1_Combi", _sqlparam);
                _ds = objData.RetrieveDataset("SPR_UWSARAL_MSARTSARFETCHDETAILS_GET_V1_Combi_V2", _sqlparam);
                //12.1 End of Changes; Bhaumik Patel-[CR-5523]
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlineTsmrMsarDisplayDetails_GET Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs(strPQuoteNo, "Failed", "UWPortfolio", "Commfun.cs", "OnlineTsmrMsarDisplayDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }



        /*added by shri on 08 sept 17 to call ofac details service*/
        public void OfacRequest_GET(string strApplicationNo, ref DataSet _ds)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :BankEnqManageDetails_GET");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NUMBER", strApplicationNo);
                _ds = objData.RetrieveDataset("USP_UWSARAL_FETCH_OFAC_REQUEST_SERVICE", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :BankEnqManageDetails_GET");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "BankEnqManageDetails_GET", "E-Error", "", "", Error.ToString());
            }
        }
        /*added by shri on 04 nov 17 to call medical application details if exists then */
        public void FetchMedicalRequirement(string strApplicationNo, ref DataSet _ds)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :FetchMedicalRequirement");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplicationNo);
                _ds = objData.RetrieveDataset("USP_UWSARAL_MANAGE_MEDICAL_FOLLOWUP", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :FetchMedicalRequirement");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "FetchMedicalRequirement", "E-Error", "", "", Error.ToString());
            }
        }
        /*set medical status*/
        public void UpdateMedicalRequirement(string strApplicationNo, ref int intRet)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :FetchMedicalRequirement");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplicationNo);
                intRet = objData.Insertrecord("USP_UWSARAL_MANAGE_MEDICAL_FOLLOWUP_STATUS", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :FetchMedicalRequirement");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "FetchMedicalRequirement", "E-Error", "", "", Error.ToString());
            }
        }
        /*end here*/

        public void OnlineConsentDetails_Get(ref DataSet _ds, string strPQuoteNo, string strChanneltype)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : CONSENT LETTER" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strChanneltype);
                _ds = objData.RetrieveDataset("USP_BPM_ConsentDetailsGet", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Data not found : CONSENT LETTER" + System.Environment.NewLine);
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineConsentDetails_Get", "E-Error", "", "", Error.ToString());
            }
        }

       public void OnlineLoadingDetails_Get(string strPQuoteNo, ref DataSet _ds)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Date Request : PREMIUMCAL" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@PQuoteNo", strPQuoteNo);
                _ds = objData.RetrieveDataset("USP_BPM_LoadingDetailsGet", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR Date Request : PREMIUMCAL" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "OnlineLoadingDetails_Get", "E-Error", "", "", Error.ToString());
            }
        }

       public void OnlinepremiumLoadingDetails_Save(string strApplicationNo, int strAmount, string strDescription, string strLD_code, string strReason, string strComments, string strDescriptions, int strPremiumBOPPlanA, int strPremiumBOPPlanB, int strTotalPremiumBOPPlanA, int strTotalPremiumBOPPlanB, string strLoadingPercentage, ref int LoadingPremresult)
       {
           try
           {
               Logger.Info(strApplicationNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlinepremiumLoadingDetails_Save");
               SqlParameter[] _sqlparam = new SqlParameter[12];
               _sqlparam[0] = new SqlParameter("@ApplicationNo", strApplicationNo);
               _sqlparam[1] = new SqlParameter("@Amount", strAmount);
               _sqlparam[2] = new SqlParameter("@Description", strDescription);
               _sqlparam[3] = new SqlParameter("@LD_code", strLD_code);
               _sqlparam[4] = new SqlParameter("@Reason", strReason);
               _sqlparam[5] = new SqlParameter("@Comments", strComments);
               _sqlparam[6] = new SqlParameter("@Descriptions", strDescriptions);
               _sqlparam[7] = new SqlParameter("@PremiumBOPPlanA", strPremiumBOPPlanA);
               _sqlparam[8] = new SqlParameter("@PremiumBOPPlanB", strPremiumBOPPlanB);
               _sqlparam[9] = new SqlParameter("@TotalPremiumBOPPlanA", strTotalPremiumBOPPlanA);
               _sqlparam[10] = new SqlParameter("@TotalPremiumBOPPlanB", strTotalPremiumBOPPlanB);
               _sqlparam[11] = new SqlParameter("@LoadingPercentage", strLoadingPercentage);
               LoadingPremresult = objData.Insertrecord("USP_BPM_LoadingpremiumDetailsSave_V2", _sqlparam);
           }
           catch (Exception Error)
           {
               Logger.Error(strApplicationNo + "STAG 2 :PageName :Commfun.CS // MethodeName :OnlinepremiumLoadingDetails_Save Error :" + System.Environment.NewLine + Error.ToString());
               SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "OnlinepremiumLoadingDetails_Save", "E-Error", "", "", Error.ToString());
           }
       }
        //ID:1 BEGINS
       public void FetchAadharDetails(string strApplicationNo, string strChannel, ref DataSet _ds)
       {
           try
           {
               Logger.Info(strApplicationNo + "STAG 2 :PageName :Commfun.CS // MethodeName :FetchAadharDetails");
               SqlParameter[] _sqlparam = new SqlParameter[2];
               _sqlparam[0] = new SqlParameter("@APPLICATION_NO", strApplicationNo);
               _sqlparam[1] = new SqlParameter("@CHANNEL", strChannel);
               _ds = objData.RetrieveDataset("USP_UWSARAL_DECISION_FETCH_AADHAR_DETAILS", _sqlparam);
           }
           catch (Exception Error)
           {
               Logger.Error(strApplicationNo + "STAG 2 :PageName :Commfun.CS // MethodeName :FetchAadharDetails Error :" + System.Environment.NewLine + Error.ToString());
               SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "FetchAadharDetails", "E-Error", "", "", Error.ToString());
           }       
       }
        //ID:1 END
       /*ID:2 START*/
       public void PostTpaMedicalTest(ref DataSet _ds)
       {
           try
           {
               Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :PostTpaMedicalTest");
               _ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_MEDICALTEST_SAVE");
           }
           catch (Exception Error)
           {
               Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :PostTpaMedicalTest Error :" + System.Environment.NewLine + Error.ToString());
               SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "PostTpaMedicalTest", "E-Error", "", "", Error.ToString());
           }
       }

       public void GetTpaMedicalTest(ref DataSet _ds)
       {
           try
           {
               Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :GetTpaMedicalTest");
               _ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_MIDEICALDETAILS_REGISTER");
           }
           catch (Exception Error)
           {
               Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :GetTpaMedicalTest Error :" + System.Environment.NewLine + Error.ToString());
               SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "GetTpaMedicalTest", "E-Error", "", "", Error.ToString());
           }
       }

       public void UpdateTPARegistertaionRerNo(DataTable _dt, ref int intResponse)
       {
           try
           {
               Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :UpdateTPARegistertaionRerNo");
               SqlParameter[] _sqlparam = new SqlParameter[1];
               _sqlparam[0] = new SqlParameter("@TYP_UWSARAL_TPA_REGISTERATION", _dt);
               intResponse = objData.Insertrecord("USP_UWSARAL_TPA_REGISTER_UPDATE_TPAREFNO", _sqlparam);
           }
           catch (Exception Error)
           {
               Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UpdateTPARegistertaionRerNo Error :" + System.Environment.NewLine + Error.ToString());
               SaveErrorLogs("", "Failed", "UWPortfolio", "Commfun.cs", "UpdateTPARegistertaionRerNo", "E-Error", "", "", Error.ToString());
           }
       }
        /*ID:2 END*/
       /*ID:4 START*/
       public void WarningMessage(ref DataSet _ds, int strParentID)
       {
           try
           {
               Logger.Info(strParentID + "STAG 3-L :PageName :Commfun.CS // MethodeName :WarningMessage");
               SqlParameter[] _sqlparam = new SqlParameter[1];
               _sqlparam[0] = new SqlParameter("@ParentID", strParentID);
               _ds = objData.RetrieveDataset("USP_UWSaral_UW_Warning", _sqlparam);
           }
           catch (Exception Error)
           {
               Logger.Error(strParentID + "STAG 2 :PageName :Commfun.CS // MethodeName :WarningMessage Error :" + System.Environment.NewLine + Error.ToString());
               SaveErrorLogs(Convert.ToString(strParentID), "Failed", "UWPortfolio", "Commfun.cs", "WarningMessage", "E-Error", "", "", Error.ToString());
           }
       }
        /*ID:4 END*/
        /*ID:5 START*/
        public void GET_DC_PINCODE(ref DataSet _ds)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :GET_DC_PINCODE");
                _ds = objData.RetrieveDataset_woParam("GET_DCCODE_PINCODE");
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :GET_DC_PINCODE Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "UW_DCCode_Calculation", "Commfun.cs", "GET_DC_PINCODE", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:5 END*/
        /*ID:5 START*/
        public void Save_DC_Code(string APPNO, string DCCODE, string CUSTPINCODE, string DCPINCODE, ref int intResponse)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :GET_DC_PINCODE");
                SqlParameter[] _sqlparam = new SqlParameter[4];
                _sqlparam[0] = new SqlParameter("@APPNO", APPNO);
                _sqlparam[1] = new SqlParameter("@DC_CODE", DCCODE);
                _sqlparam[2] = new SqlParameter("@CUSTPINCODE", CUSTPINCODE);
                _sqlparam[3] = new SqlParameter("@DCPINCODE", DCPINCODE);
                intResponse = objData.Insertrecord("USP_UWSARAL_INSERT_DC_CODE", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :GET_DC_PINCODE Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "UW_DCCode_Calculation", "Commfun.cs", "GET_DC_PINCODE", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:5 END*/
        public void FetchBranchState(ref DataSet _ds, string strApplnNo)
        {
            Logger.Info(strApplnNo + "STAG 3-A :PageName :Commfun.CS // MethodeName :DeleteExistingLoading");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@AppNo", strApplnNo);
            _ds = objData.RetrieveDataset("USP_UWSARAL_GETBRANCHSTATE", _sqlparam);
        }
        public void IsClientExist(ref DataSet _ds, string strApplnNo)
        {
            Logger.Info(strApplnNo + "STAG 3-A :PageName :Commfun.CS // MethodeName :DeleteExistingLoading");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@AppNo", strApplnNo);
            _ds = objData.RetrieveDataset("USP_UWSARAL_IsClientExist_V1", _sqlparam);
        }
        public void PreviousCaseStatus(ref DataSet _ds, string strApplnNo)
        {
            Logger.Info(strApplnNo + "STAG 3-A :PageName :Commfun.CS // MethodeName :DeleteExistingLoading");
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@AppNo", strApplnNo);
            _ds = objData.RetrieveDataset("USP_UWSARAL_PreviousPlolicyStatus", _sqlparam);
        }
        public void AddWarningMsgFollowup(string strApplicationNo, ref int intRet)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :AddWarningMsgFollowup");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@AppNo", strApplicationNo);
                intRet = objData.Insertrecord("USP_UWSARAL_AddFolloupForWarning", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :AddWarningMsgFollowup");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "AddWarningMsgFollowup", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:6 START*/
        public void GetRegisterMIS(ref DataSet _ds)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :GetRegisterMIS");
                _ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPAREGISTER_MIS");
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :GetRegisterMIS Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "TPAREGISTERMIS", "Commfun.cs", "GetRegisterMIS", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:6 END*/

        /*ID:7 START*/
        public void UWSARAL_TPALast_Schedular_Log(string Reg,ref int intResponse)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :UpdateTPARegistertaionRerNo");
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@Code", Reg);
                //_sqlparam[1] = new SqlParameter("@ftp", ftp);
                intResponse = objData.Insertrecord("USP_UWSARAL_TPA_MANAGE_LAST_SCHEDULAR_LOGS", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :UWSARAL_TPALast_Schedular_Log Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "TPAMEDICALREGISTRATIONSYNC", "Commfun.cs", "UWSARAL_TPALast_Schedular_Log", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:7 END*/
        /*ID:9 START*/
        public void FetchApplicationPartnerToRegister(ref DataSet _ds)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :FetchApplicationPartnerToRegister");
                _ds = objData.RetrieveDataset_woParam("USP_UWSARAL_TPA_FETCH_APPLICTION_TO_REGISTER");
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :FetchApplicationPartnerToRegister Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "TPAREGISTERMIS", "Commfun.cs", "FetchApplicationPartnerToRegister", "E-Error", "", "", Error.ToString());
            }
        }

        public void FetchApplicationDetailsForHealthIndia(string strFgiRefNo, string strApplicationSource, string strApplicationChannel, ref DataSet _ds)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :FetchApplicationDetailsForHealthIndia");
                SqlParameter[] _sqlparam = new SqlParameter[3];
                _sqlparam[0] = new SqlParameter("@FgiRefNo", strFgiRefNo);
                _sqlparam[1] = new SqlParameter("@ApplicationSource", strApplicationSource);
                _sqlparam[2] = new SqlParameter("@ApplicationChannel", strApplicationChannel);
                _ds = objData.RetrieveDataset("USP_UWSARAL_TPA_HEALTH_INDIA_MIDEICAL_REGISTER", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :FetchApplicationDetailsForHealthIndia Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "TPAREGISTERMIS", "Commfun.cs", "FetchApplicationDetailsForHealthIndia", "E-Error", "", "", Error.ToString());
            }
        }
        public void UpdateApplicationStatusForHealthIndia(string strFgiRefNo, string strControlNumber, bool blnIsSuccess, string strResponse, string strRequest, ref int intRet)
        {
            try
            {
                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :FetchApplicationDetailsForHealthIndia");
                SqlParameter[] _sqlparam = new SqlParameter[5];
                _sqlparam[0] = new SqlParameter("@FgiRefNo", strFgiRefNo);
                _sqlparam[1] = new SqlParameter("@ControlNumber", string.IsNullOrEmpty(strControlNumber) ? string.Empty : strControlNumber);
                _sqlparam[2] = new SqlParameter("@ISSUCCESS", blnIsSuccess);
                _sqlparam[3] = new SqlParameter("@Response", strResponse);
                _sqlparam[4] = new SqlParameter("@Request", strRequest);
                intRet = objData.Insertrecord("USP_UWSARAL_TPA_HEALTH_INDIA_REGISTER_UPDATE_STATUS", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :FetchApplicationDetailsForHealthIndia Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "TPAREGISTERMIS", "Commfun.cs", "FetchApplicationDetailsForHealthIndia", "E-Error", "", "", Error.ToString());
            }
        }
        /*ID:9 END*/
        /*ID:10 START by Dinesh Kondabattini*/
        public string CheckIsMwpApp(string strApplicationNo)
        {
            string Result = string.Empty;
            DataSet ds = new DataSet();
            try
            {

                Logger.Info("STAG 2 :PageName :Commfun.CS // MethodeName :CheckIsMwpApp");
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@ApplicationNo", strApplicationNo);
                ds = objData.RetrieveDataset("usp_isMWPApplicationOrNot", _sqlparam);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Result = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception Error)
            {
                Logger.Error("STAG 2 :PageName :Commfun.CS // MethodeName :CheckIsMwpApp Error :" + System.Environment.NewLine + Error.ToString());
                SaveErrorLogs("", "Failed", "TPAREGISTERMIS", "Commfun.cs", "CheckIsMwpApp", "E-Error", "", "", Error.ToString());
            }
            return Result;
        }
        /*ID:10 END by Dinesh Kondabattini*/
        //added by suraj for get data for combi application enq.
        public void CombiApplicationENQData_GET(ref DataSet _ds, string strPQuoteNo, string strAppType)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:CombiApplicationENQData_GET//I-INFO:Date Request : PREISSUEVAL" + System.Environment.NewLine);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[2];
                _sqlparam[0] = new SqlParameter("@AppNo", strPQuoteNo);
                _sqlparam[1] = new SqlParameter("@AppType", strAppType);
                _ds = objData.RetrieveDataset("USP_UWSARAL_COMBIENQDATA_GET", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:CombiApplicationENQData_GET//E-ERROR Date Request : PREISSUEVAL" + Error.Message + System.Environment.NewLine);
                SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralDecision", "Commfun.cs", "CombiApplicationENQData_GET", "E-Error", "", "", Error.ToString());
            }
        }
        //get child data from parent appno ---if user select combi appno(parent) for adding req then add requirement against child appno
        public void GetChildAppdata(string strApplicationNo, ref DataSet _ds)
        {
            Logger.Info(":PageName :UWSaralServices/CommFun.CS // MethodeName :GetChildAppdata");
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@Appno", strApplicationNo);
                _ds = objData.RetrieveDataset("USP_UWSARAL_GET_COMBIDETAILS", _sqlparam);
            }
            catch (Exception Error)
            {
                Logger.Error(":PageName :UWSaralServices/CommFun.CS // MethodeName :GetChildAppdata");
                SaveErrorLogs(string.Empty, "Failed", "UWSaralDecision", "Commfun.cs", "GetChildAppdata", "E-Error", "", "", Error.ToString());
            }
        }
        //11.1 Begin of Changes; Sagar Thorave-[mfl00886]
       
        public void Featch_AMLFLAG_Details_LifeAsia(ref DataSet _ds, string applicationNo,string source)
        {

            SqlParameter[] _sqlparam = new SqlParameter[2];
            try
            {
                _sqlparam[0] = new SqlParameter("@AppNo", applicationNo);
                _sqlparam[1] = new SqlParameter("@Source", source);
                _ds = objData.RetrieveDataset("USP_FetchAMLRisk_FinalDetails", _sqlparam);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //11.1 End of Changes; Sagar Thorave-[mfl00886]
    }
   
}


