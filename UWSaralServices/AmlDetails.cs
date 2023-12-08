using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
/*
***********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT: 
REMARK: AML risk categorisation in Life Asia
DateTime :22 Jun 2022
* **********************************************************************************************************************************
 */
namespace UWSaralServices
{
    public class AmlDetails
    {
        CommFun objcomm = new CommFun();
        string strpartnerId = "UWSaral";
        string strprocessID = "UWSaral";
        string strApplicationNo = string.Empty;
        string _strUserRole = string.Empty;
        string _strUserId = string.Empty;
        string _strUserBranch = string.Empty;

        //1.1 Start of Changes; Sagar Thorave-[mfl00886]
        public void AMLPushService(string strPQuoteNo, DataRow _drAml, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus, string CLNTRSKIND1 )
        {
            //1.1 End of Changes; Sagar Thorave-[mfl00886]
            try
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: AML Modification" + System.Environment.NewLine);
                #region Object Declaration Begins.
                LAAmlService.ServiceClient ObjAml = new LAAmlService.ServiceClient();
                LAAmlService.MasterClient objRes = new LAAmlService.MasterClient();

                LAAmlEnquiryService.ServiceClient objAmlEnq = new LAAmlEnquiryService.ServiceClient();

                LAAmlEnquiryService.Master obiEnqRes = new LAAmlEnquiryService.Master();

                #endregion Object Declaration End.
                if (_drAml["CLTTWO"].ToString() != null)
                {
                    #region AMl Service Enquiry Begins.
                    string _strClientId = _drAml["CLTTWO"].ToString();
                    string _strUserRole = objChangeObj.userLoginDetails._UserRole;
                    //string _strUserId = _dsAml.Tables[0].Rows[0]["BPMUSERID"].ToString();
                    string _strUserId = objChangeObj.userLoginDetails._UserID;
                    string _strUserBranch = objChangeObj.userLoginDetails._userBranch;
                    strApplicationNo = strPQuoteNo;
                    obiEnqRes = objAmlEnq.AMLEnquiry(_strClientId, _strUserBranch, _strUserRole, _strUserId, strpartnerId, strprocessID, strApplicationNo);
                    //obiEnqRes = objAmlEnq.AMLEnquiry()
                    Logger.Info(strPQuoteNo + "*******Aml Enquiry End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    #endregion Aml Service Enquiry End.
                }

                #region Aml Begins.
                //1.1 Start of Changes; Sagar Thorave-[mfl00886]
                if (!string.IsNullOrEmpty(CLNTRSKIND1))
                {
                   //1.1 End of Changes; Sagar Thorave-[mfl00886]
                    
                        string CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, BPMBRANCH, BPMUSERROLE, BPMUSERID, ZFORM, ZPANCOPPY;
                    IDPRFDT = "";
                    IDPRFNUM = "";
                    ZFOTOPRF = _drAml["ZFOTOPRF"].ToString();
                    ZFOTOIND = _drAml["ZFOTOIND"].ToString();
                    if (!string.IsNullOrEmpty(_drAml["IDPRF"].ToString()))
                    {
                        DateTime IDPrfDate = DateTime.Parse(_drAml["IDPRFDT"].ToString());
                        IDPRFDT = IDPrfDate.ToString("dd-MM-yyyy");
                        IDPRFNUM = _drAml["IDPRFNUM"].ToString();
                        ZFOTOPRF = _drAml["IDPRF"].ToString();
                        ZFOTOIND = "Y";
                    }
                    CLTTWO = _drAml["CLTTWO"].ToString();
                    ADDRPRF = _drAml["ADDRPRF"].ToString();
                    //1.1 Begin of Changes; Sagar Thorave-[mfl00886]
                    CLNTRSKIND = CLNTRSKIND1;
                    //1.1 End of Changes; Sagar Thorave-[mfl00886]
                    IDPRF = _drAml["IDPRF"].ToString();
                    INCPRF = _drAml["INCPRF"].ToString();
                    ISSUEAUTH = _drAml["ISSUEAUTH"].ToString();
                    //1.1 Begin of Changes; Sagar Thorave-[mfl00886]
                    if (CLNTRSKIND == "4")
                    {
                        REASONCD = "AHRC";
                    }
                    else if (CLNTRSKIND == "1")
                    {
                        REASONCD = "ALRC";
                    }
                    else
                    {
                        REASONCD = _drAml["REASONCD"].ToString();
                    }
                    //1.1 End of Changes; Sagar Thorave-[mfl00886]
                    ZAGEPRF = _drAml["ZAGEPRF"].ToString(); 
                    ZPANIDNO = _drAml["ZPANIDNO"].ToString();
                    //Begin changes CR -29431 Pooja Shetye[1133038] on 29.09.2021
                    ZFORM = (string.IsNullOrEmpty(ZPANIDNO) ? "Y" : "N");
                    if (ZFORM == "Y") { ZPANIDNO = ""; }
                    //END changes CR -29431 Pooja Shetye[1133038] on 29.09.2021

                    ZPANCOPPY = (string.IsNullOrEmpty(ZPANIDNO) ? "N" : "Y");
                    BPMBRANCH = objChangeObj.userLoginDetails._userBranch;
                    BPMUSERROLE = objChangeObj.userLoginDetails._UserRole;
                    BPMUSERID = objChangeObj.userLoginDetails._UserID;
                    if (string.IsNullOrEmpty(obiEnqRes.CLTTWO))
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: AML CREATEION" + System.Environment.NewLine);
                        objRes = ObjAml.AMLCRT(CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, ZFORM, ZPANCOPPY, BPMBRANCH, BPMUSERROLE, BPMUSERID, strpartnerId, strprocessID, strPQuoteNo);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: AML Modification" + System.Environment.NewLine);
                        objRes = ObjAml.AMLUPD(CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, ZFORM, ZPANCOPPY, BPMBRANCH, BPMUSERROLE, BPMUSERID, strpartnerId, strprocessID, strPQuoteNo);
                    }
                }

                //if (_drAml["CLNTRSKIND"].ToString() != null)
                //{
                //    string CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, BPMBRANCH, BPMUSERROLE, BPMUSERID, ZFORM, ZPANCOPPY;
                //    IDPRFDT = "";
                //    IDPRFNUM = "";
                //    ZFOTOPRF = _drAml["ZFOTOPRF"].ToString();
                //    ZFOTOIND = _drAml["ZFOTOIND"].ToString();
                //    if (!string.IsNullOrEmpty(_drAml["IDPRF"].ToString()))
                //    {
                //        DateTime IDPrfDate = DateTime.Parse(_drAml["IDPRFDT"].ToString());
                //        IDPRFDT = IDPrfDate.ToString("dd-MM-yyyy");
                //        IDPRFNUM = _drAml["IDPRFNUM"].ToString();
                //        ZFOTOPRF = _drAml["IDPRF"].ToString();
                //        ZFOTOIND = "Y";
                //    }
                //    CLTTWO = _drAml["CLTTWO"].ToString();
                //    ADDRPRF = _drAml["ADDRPRF"].ToString();
                //    CLNTRSKIND = _drAml["CLNTRSKIND"].ToString();
                //    IDPRF = _drAml["IDPRF"].ToString();
                //    INCPRF = _drAml["INCPRF"].ToString();
                //    ISSUEAUTH = _drAml["ISSUEAUTH"].ToString();
                //    REASONCD = _drAml["REASONCD"].ToString();
                //    ZAGEPRF = _drAml["ZAGEPRF"].ToString();
                //    ZPANIDNO = _drAml["ZPANIDNO"].ToString();
                //    ZFORM = "";
                //    ZPANCOPPY = (string.IsNullOrEmpty(ZPANIDNO) ? "N" : "Y");
                //    BPMBRANCH = objChangeObj.userLoginDetails._userBranch;
                //    BPMUSERROLE = objChangeObj.userLoginDetails._UserRole;
                //    BPMUSERID = objChangeObj.userLoginDetails._UserID;

                //    objRes = ObjAml.AMLUPD(CLTTWO, ADDRPRF, CLNTRSKIND, IDPRF, IDPRFDT, IDPRFNUM, INCPRF, ISSUEAUTH, REASONCD, ZAGEPRF, ZFOTOIND, ZFOTOPRF, ZPANIDNO, ZFORM, ZPANCOPPY, BPMBRANCH, BPMUSERROLE, BPMUSERID, strpartnerId, strprocessID, strPQuoteNo);

                //}
                else
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: AML Modification" + System.Environment.NewLine);
                    objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "AmlDetails.cs", "AMLPushService", "E-ServiceCallError", "", "", obiEnqRes.VALUES.ToString() + System.Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: AML Modification" + System.Environment.NewLine);
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES.ToString();
                }
                else
                {

                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :AmlDetails.cs // MethodeName :AMLPushService" + System.Environment.NewLine + "Aml creation service called failed" + System.Environment.NewLine);
                    objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "AmlDetails.cs", "AMLUpdateService", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES.ToString();
                }
                #endregion Aml Upation End.

                #region Aml Updation From database Begins.

                #endregion Aml updation from database End.
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";

                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR:Service Call Execution ERROR: AML Modification" + "ERROR MESSAGE:" + Error.Message + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "AmlDetails.cs", "AMLPushService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
            }
        }
    }
}
