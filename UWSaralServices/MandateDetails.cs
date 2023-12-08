
using Platform.Utilities.LoggerFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;

namespace UWSaralServices
{
    public class MandateDetails
    {
        CommFun objcomm = new CommFun();
        string CHDRSEL, ZDOCTOR, BPMBRANCH, BPMUSERROLE, BPMUSERID;
        //DDMAPR variables
        string CLNTNUM = string.Empty;
        string MANDREFF = string.Empty;
        string DTEAPROV = string.Empty;

        //DDMCRT variables/DDMUPD
        string BANKACCKEY = string.Empty;
        string BANKKEY = string.Empty;
        string DETLSUMM = string.Empty;
        string EFFDATE = string.Empty;
        string FACTHOUS = string.Empty;
        string MANDAMT = string.Empty;
        string MANDSTAT = string.Empty;
        string TIMESUSE = string.Empty;
        string ZDDDAY = string.Empty;
        string BKCARDNUM = string.Empty;
        string strIFSCCode, strMICRCode, strURNUM = string.Empty;

        //DDMRJC variable

        string DTEFREJ = string.Empty;
        string DTEFSBM = string.Empty;
        string DTESREJ = string.Empty;
        string DTESSBM = string.Empty;
        string FREASON = string.Empty;
        string SREASON = string.Empty;



        //Comman
        string Branch = string.Empty;
        string UserRole = string.Empty;
        string UserID = string.Empty;
        string strpartnerId = string.Empty;
        string strprocessID = string.Empty;
        string strApplicationNo = string.Empty;
        int i = 0;
        //DDMAPR start
        public void MandatePushService(string strPQuoteNo, DataSet _dsMandate, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******Mandate creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandatePushService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LAMandate.ServiceClient objMandate = new LAMandate.ServiceClient();
                LAMandate.MasterMandate objRes = new LAMandate.MasterMandate();



                if (_dsMandate.Tables.Count > 0 && _dsMandate.Tables[0].Rows.Count > 0)
                {

                    //List<string> strAppNo = _dsMandate.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CLNTNUM = _dsMandate.Tables[0].Rows[0]["CBANKSEL"].ToString();
                    MANDREFF = strLAPushStatus;//_dsMandate.Tables[0].Rows[0]["MANDREFF"].ToString();
                    DTEAPROV = _dsMandate.Tables[0].Rows[0]["EFFDATE"].ToString();
                    Branch = _dsMandate.Tables[0].Rows[0]["Branch"].ToString();
                    UserRole = _dsMandate.Tables[0].Rows[0]["UserRole"].ToString();
                    UserID = _dsMandate.Tables[0].Rows[0]["UserID"].ToString();
                    //strpartnerId =_dsMandate.Tables[0].Rows[0]["strpartnerId"].ToString();
                    //strprocessID =_dsMandate.Tables[0].Rows[0]["strprocessID"].ToString();
                    //strApplicationNo =_dsMandate.Tables[0].Rows[0]["strApplicationNo"].ToString();

                    //ZDOCTOR = _dsMandate.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                    //BPMBRANCH = _dsMandate.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                    //BPMUSERROLE = _dsMandate.Tables[0].Rows[0]["BPMUSERROLE"].ToString();

                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    objRes = objMandate.DDMAPR(CLNTNUM, MANDREFF, DTEAPROV, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);



                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :DDMAPR" + System.Environment.NewLine + "MandateDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******MandateDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :MandatePushService" + System.Environment.NewLine + "MandateDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandatePushService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
        //DDMCRT start
        public void MandateCreateService(string strPQuoteNo, DataSet _dsMandate, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******Mandate creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateCreateService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LAMandate.ServiceClient objMandateCRT = new LAMandate.ServiceClient();
                LAMandate.MasterMandateCreate objRes = new LAMandate.MasterMandateCreate();



                if (_dsMandate.Tables.Count > 0 && _dsMandate.Tables[0].Rows.Count > 0)
                {

                    //List<string> strAppNo = _dsMandate.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();
                    CLNTNUM = Convert.ToString(_dsMandate.Tables[0].Rows[0]["CBANKSEL"]);
                    BANKACCKEY = Convert.ToString(_dsMandate.Tables[0].Rows[0]["BANKACOUNT"]);
                    BANKKEY = Convert.ToString(_dsMandate.Tables[0].Rows[0]["BANKKEY"]);
                    DETLSUMM = Convert.ToString(_dsMandate.Tables[0].Rows[0]["DETLSUMM"]);
                    EFFDATE = Convert.ToString(_dsMandate.Tables[0].Rows[0]["EFFDATE"]);
                    FACTHOUS = Convert.ToString(_dsMandate.Tables[0].Rows[0]["FACTHOUS"]);
                    MANDAMT = Convert.ToString(_dsMandate.Tables[0].Rows[0]["MANDAMT"]);
                    MANDSTAT = Convert.ToString(_dsMandate.Tables[0].Rows[0]["MANDSTAT"]);
                    TIMESUSE = Convert.ToString(_dsMandate.Tables[0].Rows[0]["TIMESUSE"]);
                    ZDDDAY = Convert.ToString(_dsMandate.Tables[0].Rows[0]["ZDDDAY"]);
                    BKCARDNUM = Convert.ToString(_dsMandate.Tables[0].Rows[0]["BKCARDNUM"]);
                    Branch = Convert.ToString(_dsMandate.Tables[0].Rows[0]["Branch"]);
                    UserRole = Convert.ToString(_dsMandate.Tables[0].Rows[0]["UserRole"]);
                    UserID = Convert.ToString(_dsMandate.Tables[0].Rows[0]["UserID"]);

                    strIFSCCode = Convert.ToString(_dsMandate.Tables[0].Rows[0]["IFSCCODE"]);
                    strMICRCode = Convert.ToString(_dsMandate.Tables[0].Rows[0]["MICRCODE"]);
                    strURNUM = Convert.ToString(_dsMandate.Tables[0].Rows[0]["URNNO"]);


                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    //objRes = objMandateCRT.DDMCRT(CLNTNUM, BANKACCKEY, BANKKEY, DETLSUMM, EFFDATE, FACTHOUS, MANDAMT, MANDSTAT, TIMESUSE, ZDDDAY, BKCARDNUM, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);
                    objRes = objMandateCRT.DDMCRT(CLNTNUM, BANKACCKEY, BANKKEY, DETLSUMM, EFFDATE, FACTHOUS, MANDAMT, MANDSTAT, TIMESUSE, ZDDDAY, BKCARDNUM, Branch, UserRole, UserID
                                        , strpartnerId, strprocessID, strApplicationNo
                                        , strIFSCCode, strMICRCode, strURNUM);

                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :DDMCRT" + System.Environment.NewLine + "MandateDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******MandateDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        strLAPushStatus = objRes.MANDREF;
                        int intRef = -1;
                        objcomm.UpdateMandateReferenceNo(strApplicationNo, strLAPushStatus, ref intRef);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateCreateService" + System.Environment.NewLine + "MandateDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                        strLAPushStatus = objRes.VALUES;
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    //strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateCreateService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }

        //DDMENQ start
        public void MandateEnquiryService(string strPQuoteNo, DataSet _dsMandate, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******Mandate creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateEnquiryService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LAMandate.ServiceClient objMandateENQ = new LAMandate.ServiceClient();
                LAMandate.MasterMandateEnquiry objRes = new LAMandate.MasterMandateEnquiry();



                if (_dsMandate.Tables.Count > 0 && _dsMandate.Tables[0].Rows.Count > 0)
                {

                    List<string> strAppNo = _dsMandate.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CLNTNUM = _dsMandate.Tables[0].Rows[0]["CLNTNUM"].ToString();
                    MANDREFF = _dsMandate.Tables[0].Rows[0]["MANDREFF"].ToString();

                    Branch = _dsMandate.Tables[0].Rows[0]["Branch"].ToString();
                    UserRole = _dsMandate.Tables[0].Rows[0]["UserRole"].ToString();
                    UserID = _dsMandate.Tables[0].Rows[0]["UserID"].ToString();


                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    objRes = objMandateENQ.DDMENQ(CLNTNUM, MANDREFF, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);


                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :DDMENQ" + System.Environment.NewLine + "MandateDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******MandateDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateEnquiryService" + System.Environment.NewLine + "MandateDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateCreateService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }

        //DDMRJC start
        public void MandateRejectService(string strPQuoteNo, DataSet _dsMandate, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******Mandate creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateRejectService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LAMandate.ServiceClient objMandateUpdt = new LAMandate.ServiceClient();
                LAMandate.MasterMandate objRes = new LAMandate.MasterMandate();



                if (_dsMandate.Tables.Count > 0 && _dsMandate.Tables[0].Rows.Count > 0)
                {

                    List<string> strAppNo = _dsMandate.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CLNTNUM = _dsMandate.Tables[0].Rows[0]["CLNTNUM"].ToString();
                    MANDREFF = _dsMandate.Tables[0].Rows[0]["MANDREFF"].ToString();
                    DTEFREJ = _dsMandate.Tables[0].Rows[0]["DTEFREJ"].ToString();
                    DTEFSBM = _dsMandate.Tables[0].Rows[0]["DTEFSBM"].ToString();
                    DTESREJ = _dsMandate.Tables[0].Rows[0]["DTESREJ"].ToString();
                    DTESSBM = _dsMandate.Tables[0].Rows[0]["DTESSBM"].ToString();
                    FREASON = _dsMandate.Tables[0].Rows[0]["FREASON"].ToString();
                    SREASON = _dsMandate.Tables[0].Rows[0]["SREASON"].ToString();

                    Branch = _dsMandate.Tables[0].Rows[0]["Branch"].ToString();
                    UserRole = _dsMandate.Tables[0].Rows[0]["UserRole"].ToString();
                    UserID = _dsMandate.Tables[0].Rows[0]["UserID"].ToString();
                    //strpartnerId =_dsMandate.Tables[0].Rows[0]["strpartnerId"].ToString();
                    //strprocessID =_dsMandate.Tables[0].Rows[0]["strprocessID"].ToString();
                    //strApplicationNo =_dsMandate.Tables[0].Rows[0]["strApplicationNo"].ToString();

                    //ZDOCTOR = _dsMandate.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                    //BPMBRANCH = _dsMandate.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                    //BPMUSERROLE = _dsMandate.Tables[0].Rows[0]["BPMUSERROLE"].ToString();

                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    objRes = objMandateUpdt.DDMRJC(CLNTNUM, MANDREFF, DTEFREJ, DTEFSBM, DTESREJ, DTESSBM, FREASON, SREASON, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);

                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :DDMRJC" + System.Environment.NewLine + "MandateDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******MandateDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateRejectService" + System.Environment.NewLine + "MandateDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateRejectService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
        //DDMUPD start
        public void MandateUpdateService(string strPQuoteNo, DataSet _dsMandate, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******Mandate creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateUpdateService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LAMandate.ServiceClient objMandateUPD = new LAMandate.ServiceClient();
                LAMandate.MasterMandateUpdate objRes = new LAMandate.MasterMandateUpdate();



                if (_dsMandate.Tables.Count > 0 && _dsMandate.Tables[0].Rows.Count > 0)
                {

                    List<string> strAppNo = _dsMandate.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CLNTNUM = _dsMandate.Tables[0].Rows[0]["CLNTNUM"].ToString();
                    MANDREFF = _dsMandate.Tables[0].Rows[0]["MANDREFF"].ToString();
                    BANKACCKEY = _dsMandate.Tables[0].Rows[0]["BANKACCKEY"].ToString();
                    BANKKEY = _dsMandate.Tables[0].Rows[0]["BANKKEY"].ToString();
                    DETLSUMM = _dsMandate.Tables[0].Rows[0]["DETLSUMM"].ToString();
                    EFFDATE = _dsMandate.Tables[0].Rows[0]["EFFDATE"].ToString();
                    FACTHOUS = _dsMandate.Tables[0].Rows[0]["FACTHOUS"].ToString();
                    MANDAMT = _dsMandate.Tables[0].Rows[0]["MANDAMT"].ToString();
                    MANDSTAT = _dsMandate.Tables[0].Rows[0]["MANDSTAT"].ToString();
                    TIMESUSE = _dsMandate.Tables[0].Rows[0]["TIMESUSE"].ToString();

                    BKCARDNUM = _dsMandate.Tables[0].Rows[0]["BKCARDNUM"].ToString();
                    Branch = _dsMandate.Tables[0].Rows[0]["Branch"].ToString();
                    UserRole = _dsMandate.Tables[0].Rows[0]["UserRole"].ToString();
                    UserID = _dsMandate.Tables[0].Rows[0]["UserID"].ToString();
                    //strpartnerId =_dsMandate.Tables[0].Rows[0]["strpartnerId"].ToString();
                    //strprocessID =_dsMandate.Tables[0].Rows[0]["strprocessID"].ToString();
                    //strApplicationNo =_dsMandate.Tables[0].Rows[0]["strApplicationNo"].ToString();

                    //ZDOCTOR = _dsMandate.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                    //BPMBRANCH = _dsMandate.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                    //BPMUSERROLE = _dsMandate.Tables[0].Rows[0]["BPMUSERROLE"].ToString();

                    BPMUSERID = objChangeObj.userLoginDetails._UserID;


                    strIFSCCode = Convert.ToString(_dsMandate.Tables[0].Rows[0]["IFSCCODE"]);
                    strMICRCode = Convert.ToString(_dsMandate.Tables[0].Rows[0]["MICRCODE"]);
                    strURNUM = Convert.ToString(_dsMandate.Tables[0].Rows[0]["URNNO"]);
                    ZDDDAY = Convert.ToString(_dsMandate.Tables[0].Rows[0]["ZDDDAY"]);


                    objRes = objMandateUPD.DDMUPD(CLNTNUM, MANDREFF, BANKACCKEY, BANKKEY, DETLSUMM, EFFDATE, FACTHOUS, MANDAMT, MANDSTAT
                                , TIMESUSE, BKCARDNUM, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo
                                , strIFSCCode, strMICRCode, strURNUM, ZDDDAY);



                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :DDMUpd" + System.Environment.NewLine + "MandateDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******MandateDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateUPDATEService" + System.Environment.NewLine + "MandateDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :MandateDetails.cs // MethodeName :MandateUpdateService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MandateDetails.cs", "MandateDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }

    }
}
