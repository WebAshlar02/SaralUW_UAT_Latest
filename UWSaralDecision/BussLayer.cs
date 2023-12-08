/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:BUSSINESS LAYER
REMARK: 
DateTime :13JAN2017
* *  ************************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :DINESH KONDABATTINI
METHODE/EVENT: 
REMARK: CHECK MWP APPLICATION NO AND PASS MWP FALG TO 
DateTime :25NOV19
**********************************************************************************************************************************
* * **********************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:  strIdentitiFlag == "Approved"
REMARK: added if else condition in preissu validation region 1)combi cases 2)combi and detach cases 3)normal cases(Else condition) also called uw descision region in detach condition and  add isUWapprove flag is set to true.so that after preissue region UWdescision region not getting called.
DateTime :06 DEC 2020

* **********************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT: strIdentitiFlag == "PREISSUEVAL_COMBI"
REMARK: added pre issue validation service method for combi cases
DateTime :06 DEC 2020
* **********************************************************************************************************************************
COMMENT ID: 5
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:  strIdentitiFlag == "COMBIDET"
REMARK: added combi detach service method for combi cases
DateTime :06 DEC 2020  

* **********************************************************************************************************************************
COMMENT ID: 6
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:  strIdentitiFlag == "COMBIENQ"
REMARK: added combi enquirey service method for combi cases(after this combi flag removed from LA and policy act as an individual case)
DateTime :06 DEC 2020
* **********************************************************************************************************************************
* **********************************************************************************************************************************
COMMENT ID: 7
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT: 
REMARK: AML risk categorisation in Life Asia
DateTime :22 Jun 2022
***********************************************************************************************************************************
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using TPASTATUSCODE;
namespace UWSaralDecision
{

    public class BussLayer
    {
        DataSet _ds = new DataSet();
        DataSet _dsFollowup = null;
        DataSet _dsPol = null;
        DataSet _dsAml = null;
        DataSet _dsPreIssueVal = null;
        DataSet _dsFollowUp = null;
        DataSet _dsLoading = null;
        DataSet _dsUWApproval = null;
        DataSet _dsUWIssuence = null;
        DataSet _dsSTP = null;
        DataSet _UWDeceison = null;
        DataTable _dsSTPdrugsDeta = null;
        DataSet _dsPreIssueRes = null;
        DataSet _dsPanval = null;
        DataSet _dsPremcal = null;
        DataSet ds12 = null;
        CommFun objComm = new CommFun();
        bool isLoadingUpdated = false;
        int LoadedPremiumA = 0;
        int TotalPremiumA = 0;
        int LoadedPremiumB = 0;
        int TotalPremiumB = 0;
        int TotalPremiumAB = 0;
        int LoadedPremiumAB = 0;
        int strLApushErrorCode = -1;
        string _strLoadDiscp = string.Empty;
        string _strLoadCode = string.Empty;
        string _strReason = string.Empty;
        UWSaralServices.LACombi_DetachService.MasterCADENQ objCombiResponse_ENQ;
        UWSaralServices.LACombi_DetachService.MasterCADDET objCombiResponse_DET;

        string strDatavalue = string.Empty;
        public void OnlineApplicationLAServiceDetails_PUSH(string strPQuoteNo, string strAppType, ChangeValue objChangeValue, ref DataSet _dsResponce, ref DataSet _dsPrevPol, string strIdentitiFlag, ref int LApushErrorCode, ref string LAPushErrorMsg, ref string strConsentRespons)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution Begin" + System.Environment.NewLine);
            if (strIdentitiFlag == "Approved")
            {
                bool isUwapprove = false;
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : Approved" + System.Environment.NewLine);
                #region Consume Contract Modification Begin.
                _dsPol = new DataSet();
                objComm.OnlineServiceContractDetails_GET(ref _dsPol, strPQuoteNo, strAppType);
                if (_dsPol.Tables.Count > 0 && _dsPol.Tables[0].Rows.Count > 0)
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : Contract Modification" + System.Environment.NewLine);
                    UWSaralServices.ContractModification objContract = new UWSaralServices.ContractModification();
                    /*ID:2 START by Dinesh Kondabattini*/
                    string MWP = objComm.CheckIsMwpApp(strPQuoteNo);
                    /*ID:2 END by Dinesh Kondabattini*/
                    objContract.ContractModPushService(strPQuoteNo, _dsPol, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg, MWP); /*ID:2 Added MWP Flag by Dinesh Kondabattini*/
                }
                else
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Data not found : Contract Modification" + System.Environment.NewLine);
                    LApushErrorCode = -1;
                }
                #endregion Consume Contract Modification End.

                #region Consume Loading Begin.
                if (LApushErrorCode == 0)
                {
                    _dsLoading = new DataSet();
                    string strLoadingCode = string.Empty;
                    UWSaralServices.LoadingDetails objLoading = new UWSaralServices.LoadingDetails();
                    UWSaralServices.ConsentLetter objConsentLetter = new UWSaralServices.ConsentLetter();
                    if (objChangeValue.Load_Loadingdetails != null)
                    {
                        strLoadingCode = objChangeValue.Load_Loadingdetails._strProdcode;
                    }
                    else
                    {
                        strLoadingCode = "";
                    }

                    objComm.OnlineServiceLoadingDetails_GET(ref _dsLoading, strPQuoteNo, strLoadingCode, strAppType);
                    if (_dsLoading.Tables.Count > 0 && _dsLoading.Tables[0].Rows.Count > 0)
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : LOADING" + System.Environment.NewLine);
                        objLoading.LoadingPushService(strPQuoteNo, _dsLoading, objChangeValue, ref _dsResponce, ref LApushErrorCode, ref LAPushErrorMsg);

                        if (LApushErrorCode == 0 && strAppType.Trim().Equals("ONLINE"))
                        {
                            if (_dsResponce != null)
                            {
                                if (_dsResponce.Tables[0].Rows.Count > 0)
                                {

                                    objComm.OnlineLoadingDetails_Get(strPQuoteNo, ref _ds);

                                    TotalPremiumAB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["TotalPremiumAB"].ToString().Replace(".00", ""));
                                    LoadedPremiumAB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["LoadedPremiumAB"].ToString().Replace(".00", ""));
                                    TotalPremiumA = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["TotalPremiumA"].ToString().Replace(".00", ""));
                                    LoadedPremiumA = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["LoadedPremiumA"].ToString().Replace(".00", ""));
                                    TotalPremiumB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["TotalPremiumB"].ToString().Replace(".00", ""));
                                    LoadedPremiumB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["LoadedPremiumB"].ToString().Replace(".00", ""));
                                    if (_ds != null)
                                    {
                                        if (_ds.Tables[0].Rows.Count > 0)
                                        {
                                            _strLoadDiscp = _ds.Tables[0].Rows[0]["LoadDecription"].ToString();
                                            _strLoadCode = _ds.Tables[0].Rows[0]["LoadingCode"].ToString();
                                            _strReason = _ds.Tables[0].Rows[0]["LoadingReason"].ToString();
                                        }
                                        objComm.OnlinepremiumLoadingDetails_Save(strPQuoteNo, LoadedPremiumAB, _strLoadDiscp, _strLoadCode, _strReason, "", _strLoadDiscp, LoadedPremiumA, LoadedPremiumB, TotalPremiumA, TotalPremiumB, "", ref strLApushErrorCode);
                                    }
                                }
                            }
                        }

                        if (LApushErrorCode == 0 && strAppType.Trim().ToUpper().Equals("OFFLINE"))
                        {
                            isLoadingUpdated = true;

                        }
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Data not found : LOADING " + System.Environment.NewLine);
                    }
                }
                else
                {

                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution FAILED: Contract Modification" + System.Environment.NewLine);
                }
                #endregion Consume Loading End.

                #region Consume Followup Service Begins.
                if (LApushErrorCode == 0)
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : REQUIRMENTS" + System.Environment.NewLine);
                    _dsPreIssueVal = new DataSet();
                    UWSaralServices.FollowupDetails objFollowup = new UWSaralServices.FollowupDetails();
                    objComm.OnlineServiceFollowUPDetails_GET(ref _dsFollowUp, strPQuoteNo, strAppType);
                    if (_dsFollowUp.Tables.Count > 0 && _dsFollowUp.Tables[0].Rows.Count > 0)
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : REQUIRMENTS" + System.Environment.NewLine);
                        objFollowup.FollowuPushService(strPQuoteNo, _dsFollowUp, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                        if (LApushErrorCode > 0)
                        {
                            objComm.FetchMedicalRequirement(strPQuoteNo, ref _ds);
                            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                            {
                                UWSaralServices.MedicalDataEntryInvoke objMedicalDataEntryInvoke = new UWSaralServices.MedicalDataEntryInvoke();
                                objMedicalDataEntryInvoke.MedicalPushService(strPQuoteNo, ref LApushErrorCode, ref LAPushErrorMsg);
                                if (LApushErrorCode == 0)
                                {
                                    int intRet = -1;
                                    objComm.UpdateMedicalRequirement(strPQuoteNo, ref intRet);
                                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : MEDICAL REQUIREMENT FOR " + strPQuoteNo + System.Environment.NewLine);
                                }
                            }
                        }
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Data not found : REQUIRMENTS " + System.Environment.NewLine);
                    }
                }
                else
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution FAILED: LOADING" + System.Environment.NewLine);
                }
                #endregion Consume Followup serviceEnd.

                #region Consume AML Begins.
                if (LApushErrorCode == 0)
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : AML" + System.Environment.NewLine);
                    _dsAml = new DataSet();
                    UWSaralServices.AmlDetails objAml = new UWSaralServices.AmlDetails();
                    objComm.OnlineServiceAmlDetails_GET(ref _dsAml, strPQuoteNo, strAppType);
                    if (_dsAml.Tables.Count > 0 && _dsAml.Tables[0].Rows.Count > 0)
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : AML" + System.Environment.NewLine);
                        //7.1 Start of Changes; Sagar Thorave-[mfl00886]
                        objComm.Featch_AMLFLAG_Details_LifeAsia(ref ds12, strPQuoteNo,"SaralUW");
                        string CLNTRSKIND = string.Empty;
                        for (int i = 0; i < _dsAml.Tables[0].Rows.Count; i++)
                        {
                            if(_dsAml.Tables[0].Rows[0]["CLTTWO"].ToString()== _dsAml.Tables[0].Rows[1]["CLTTWO"].ToString())
                            {
                                CLNTRSKIND = (ds12.Tables[0].Rows[0]["CLNTRSKIND"].ToString());
                                objAml.AMLPushService(strPQuoteNo, _dsAml.Tables[0].Rows[0], objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg, CLNTRSKIND);
                                break;
                            }
                            else
                            {
                                CLNTRSKIND = (ds12.Tables[i].Rows[0]["CLNTRSKIND"].ToString());
                                objAml.AMLPushService(strPQuoteNo, _dsAml.Tables[0].Rows[i], objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg, CLNTRSKIND);
                                
                            }
                        }
                        
                        //7.1 End of Changes; Sagar Thorave-[mfl00886]
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Data not found : AML " + System.Environment.NewLine);

                    }
                }
                else
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution FAILED: REQUIRMENT" + System.Environment.NewLine);
                }
                #endregion Consume AML End.

                #region Preissuevalidation Begins.
                if (LApushErrorCode == 0)
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : PREISSUE" + System.Environment.NewLine);
                    _dsPreIssueVal = new DataSet();
                    UWSaralServices.PreIssueValDetails objPreIssue = new UWSaralServices.PreIssueValDetails();
                    UWSaralServices.UWPreIssue_Combi objPreIssue_Combi = new UWSaralServices.UWPreIssue_Combi();
                    objComm.OnlineServicePreIssuevalDetails_GET(ref _dsPreIssueVal, strPQuoteNo, strAppType);
                    if (_dsPreIssueVal.Tables.Count > 0 && _dsPreIssueVal.Tables[0].Rows.Count > 0)
                    {
                        /*ID:3 START*/
                        if (_dsPreIssueVal.Tables[0].Rows[0]["IsCombyplan"].Equals("Yes") && _dsPreIssueVal.Tables[0].Rows[0]["IsDetach"].Equals("Yes"))
                        {

                            //objPreIssue_Combi.PreIssuevalidationPushService_Combi(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString(), ref _dsResponce, _dsPreIssueVal, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                            //if (LApushErrorCode != 0)
                            //{
                            for (int j = 0; j < _dsPreIssueVal.Tables[0].Rows.Count; j++)
                            {
                                DataRow dr = _dsPreIssueVal.Tables[0].Rows[j];
                                //_dsPreIssueVal.Clear();
                                DataTable dt = new DataTable();
                                dt = _dsPreIssueVal.Tables[0].Clone();
                                DataSet _ds = new DataSet();
                                //_ds.Tables[0].Rows.Add(dr.ItemArray);
                                //_dsPreIssueVal.Tables[0].ImportRow(_dsPreIssueVal.Tables[0].Rows[j]);
                                //dt.Rows.Add(dr);
                                dt.ImportRow(dr);

                                _ds.Tables.Add(dt);

                                Logger.Info(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString() + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : PRE ISSUE VALIDATION" + System.Environment.NewLine);
                                objPreIssue.PreIssuevalidationPushService(strPQuoteNo, ref _dsPreIssueRes, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                                if (LApushErrorCode != 0)
                                {
                                    Logger.Info(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString() + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: PRE ISSUE VALIDATION" + System.Environment.NewLine);

                                    LAPushErrorMsg = "Pre Issue Failed";
                                }
                                dt.Clear();
                                #region UW Decision Begins.

                                if (LApushErrorCode == 0)
                                {
                                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UWDECEISON" + System.Environment.NewLine);
                                    _dsUWApproval = new DataSet();
                                    UWSaralServices.UWDecsionDetails objUWDecision = new UWSaralServices.UWDecsionDetails();
                                    objComm.OnlineServiceUWDecisionDetails_GET(ref _dsUWApproval, strPQuoteNo, strAppType);
                                    if (_dsUWApproval.Tables.Count > 0 && _dsUWApproval.Tables[0].Rows.Count > 0)
                                    {
                                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UW DECEISON" + System.Environment.NewLine);
                                        objUWDecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _dsUWApproval, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                                    }
                                    if (LApushErrorCode == 0)
                                    {
                                        isUwapprove = true;
                                    }
                                }
                                else if (LApushErrorCode == 1 && _dsPreIssueRes != null && _dsPreIssueRes.Tables.Count > 0 && _dsPreIssueRes.Tables[0].Rows.Count > 0)
                                {
                                    int preissue = 0;
                                    for (int i = 0; i < _dsPreIssueRes.Tables[0].Rows.Count; i++)
                                    {
                                        if (Convert.ToString(_dsPreIssueRes.Tables[0].Rows[i]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Cheque Status is Pending") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[i]["PRE ISSUE DESCRIPTION"]).Trim().Equals("EFFDATE < UW Date"))
                                        {
                                        }
                                        else
                                        {
                                            preissue = 1;
                                            break;
                                        }

                                    }
                                    //if (Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Cheque Status is Pending") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Prem+cont amts > Susp") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("EFFDATE < UW Date"))
                                    //if (Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Cheque Status is Pending") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("EFFDATE < UW Date"))
                                    if (preissue == 0)
                                    {
                                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UWDECEISON+CHEQUE FAILED" + System.Environment.NewLine);

                                        _dsUWApproval = new DataSet();
                                        UWSaralServices.UWDecsionDetails objUWDecision = new UWSaralServices.UWDecsionDetails();
                                        objComm.OnlineServiceUWDecisionDetails_GET(ref _dsUWApproval, strPQuoteNo, strAppType);
                                        if (_dsUWApproval.Tables.Count > 0 && _dsUWApproval.Tables[0].Rows.Count > 0)
                                        {
                                            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UW DECEISON" + System.Environment.NewLine);
                                            objUWDecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _dsUWApproval, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                                        }
                                        if (LApushErrorCode == 0)
                                        {
                                            isUwapprove = true;
                                        }
                                    }
                                }

                                #endregion UW Decision End.
                            }

                            //}
                        }
                        //Combi pre issue service call
                        else if (_dsPreIssueVal.Tables[0].Rows[0]["IsCombyplan"].Equals("Yes"))
                        {

                            objPreIssue_Combi.PreIssuevalidationPushService_Combi(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString(), ref _dsResponce, _dsPreIssueVal, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                            if (LApushErrorCode != 0)
                            {
                                Logger.Info(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString() + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : PRE ISSUE VALIDATION" + System.Environment.NewLine);
                                objPreIssue.PreIssuevalidationPushService(strPQuoteNo, ref _dsPreIssueRes, _dsPreIssueVal, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                                if (LApushErrorCode != 0)
                                {
                                    Logger.Info(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString() + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: PRE ISSUE VALIDATION" + System.Environment.NewLine);

                                    LAPushErrorMsg = "Pre Issue Failed";
                                }
                            }
                        }

                        else
                        {
                            Logger.Info(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString() + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : PRE ISSUE VALIDATION" + System.Environment.NewLine);
                            objPreIssue.PreIssuevalidationPushService(strPQuoteNo, ref _dsPreIssueRes, _dsPreIssueVal, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                            if (LApushErrorCode != 0)
                            {
                                Logger.Info(_dsPreIssueVal.Tables[0].Rows[0]["PARENTAPP"].ToString() + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: PRE ISSUE VALIDATION" + System.Environment.NewLine);

                                LAPushErrorMsg = "Pre Issue Failed";
                            }
                        }
                        /*ID:3 END*/
                    }
                }
                else
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution FAILED: AML" + System.Environment.NewLine);
                }
                #endregion Preissuevalidation End.

                #region UW Decision Begins.
                if (!isUwapprove)
                {
                    if (LApushErrorCode == 0)
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UWDECEISON" + System.Environment.NewLine);
                        _dsUWApproval = new DataSet();
                        UWSaralServices.UWDecsionDetails objUWDecision = new UWSaralServices.UWDecsionDetails();
                        objComm.OnlineServiceUWDecisionDetails_GET(ref _dsUWApproval, strPQuoteNo, strAppType);
                        if (_dsUWApproval.Tables.Count > 0 && _dsUWApproval.Tables[0].Rows.Count > 0)
                        {
                            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UW DECEISON" + System.Environment.NewLine);
                            objUWDecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _dsUWApproval, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                        }
                    }
                    else if (LApushErrorCode == 1 && _dsPreIssueRes != null && _dsPreIssueRes.Tables.Count > 0 && _dsPreIssueRes.Tables[0].Rows.Count > 0)
                    {
                        int preissue = 0;
                        for (int i = 0; i < _dsPreIssueRes.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToString(_dsPreIssueRes.Tables[0].Rows[i]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Cheque Status is Pending") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[i]["PRE ISSUE DESCRIPTION"]).Trim().Equals("EFFDATE < UW Date"))
                            {
                            }
                            else
                            {
                                preissue = 1;
                                break;
                            }

                        }
                        //if (Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Cheque Status is Pending") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Prem+cont amts > Susp") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("EFFDATE < UW Date"))
                        //if (Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("Cheque Status is Pending") || Convert.ToString(_dsPreIssueRes.Tables[0].Rows[0]["PRE ISSUE DESCRIPTION"]).Trim().Equals("EFFDATE < UW Date"))
                        if (preissue == 0)
                        {
                            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UWDECEISON+CHEQUE FAILED" + System.Environment.NewLine);

                            _dsUWApproval = new DataSet();
                            UWSaralServices.UWDecsionDetails objUWDecision = new UWSaralServices.UWDecsionDetails();
                            objComm.OnlineServiceUWDecisionDetails_GET(ref _dsUWApproval, strPQuoteNo, strAppType);
                            if (_dsUWApproval.Tables.Count > 0 && _dsUWApproval.Tables[0].Rows.Count > 0)
                            {
                                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : UW DECEISON" + System.Environment.NewLine);
                                objUWDecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _dsUWApproval, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                            }
                        }
                    }
                }
                #endregion UW Decision End.

                #region  UW issuance Begins.
                // This Part is commented because Issance is done through batch job.
                //if (LApushErrorCode == 0)
                //{
                //    _dsUWIssuence = new DataSet();
                //    UWSaralServices.UWIssuanceDetails objUWissuance = new UWSaralServices.UWIssuanceDetails();
                //    objComm.OnlineServiceUWDecisionDetails_GET(ref _dsUWIssuence, strPQuoteNo, strAppType);
                //    if (_dsUWIssuence.Tables.Count > 0 && _dsUWIssuence.Tables[0].Rows.Count > 0)
                //    {
                //        objUWissuance.UWIssuencePushService(strPQuoteNo, _dsUWIssuence, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                //    }
                //}
                //else
                //{
                //    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in UW Issuance Details Service");
                //}
                #endregion UW issuance End.

                if (objChangeValue.Load_Loadingdetails != null)
                {
                    if (objChangeValue.Load_Loadingdetails.isConsentRequired && isLoadingUpdated)
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Initiated : CONSENT LETTER" + System.Environment.NewLine);
                        UWSaralServices.ConsentLetter objConsentLetter = new UWSaralServices.ConsentLetter();
                        objComm.OnlineConsentDetails_Get(ref _ds, strPQuoteNo, strAppType);
                        objConsentLetter.ConsentletterService(strPQuoteNo, "", _ds, ref strConsentRespons);
                    }
                }

                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution END for  : Approved" + System.Environment.NewLine);
            }
            else if (strIdentitiFlag == "Declined")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : Declined" + System.Environment.NewLine);
                _UWDeceison = new DataSet();
                objComm.OnlineServiceUWDecisionDetails_GET(ref _UWDeceison, strPQuoteNo, strAppType);
                UWSaralServices.UWDecsionDetails objUWecision = new UWSaralServices.UWDecsionDetails();
                objUWecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _UWDeceison, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in UW Decision Details Service for Declined");
                }
            }

            else if (strIdentitiFlag == "Postponed")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : Postponed" + System.Environment.NewLine);
                _UWDeceison = new DataSet();
                objComm.OnlineServiceUWDecisionDetails_GET(ref _UWDeceison, strPQuoteNo, strAppType);
                UWSaralServices.UWDecsionDetails objUWecision = new UWSaralServices.UWDecsionDetails();
                objUWecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _UWDeceison, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in UW Decision Details Service for Postponed");
                }
            }
            else if (strIdentitiFlag == "Withdrawn")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : Withdrawn" + System.Environment.NewLine);
                _UWDeceison = new DataSet();
                objComm.OnlineServiceUWDecisionDetails_GET(ref _UWDeceison, strPQuoteNo, strAppType);
                UWSaralServices.UWDecsionDetails objUWecision = new UWSaralServices.UWDecsionDetails();
                objUWecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _UWDeceison, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in UW Decision Details Service for Withdrawn");
                }
            }
            else if (strIdentitiFlag == "DeclineWithdrawa")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : DeclineWithdrawa" + System.Environment.NewLine);
                _UWDeceison = new DataSet();
                objComm.OnlineServiceUWDecisionDetails_GET(ref _UWDeceison, strPQuoteNo, strAppType);
                UWSaralServices.UWDecsionDetails objUWecision = new UWSaralServices.UWDecsionDetails();
                objUWecision.UWDecsionPushService(strPQuoteNo, strIdentitiFlag, _UWDeceison, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in UW Decision Details Service for DeclineWithdrawa");
                }
            }
            else if (strIdentitiFlag == "STP")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : STP" + System.Environment.NewLine);
                UWSaralServices.StpValidationDetails objSTP = new UWSaralServices.StpValidationDetails();
                _dsSTP = new DataSet();
                objComm.OnlineServiceSTPDetails_GET(ref _dsSTP, strPQuoteNo, strAppType);
                objSTP.StpPushService(strPQuoteNo, ref _dsResponce, _dsSTP, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in  STP Details Service for ");
                }
            }
            else if (strIdentitiFlag == "PREISSUEVAL")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : PREISSUEVAL" + System.Environment.NewLine);
                UWSaralServices.PreIssueValDetails objSTP = new UWSaralServices.PreIssueValDetails();
                _dsSTP = new DataSet();
                objComm.OnlineServicePreIssuevalDetails_GET(ref _dsSTP, strPQuoteNo, strAppType);
                objSTP.PreIssuevalidationPushService(strPQuoteNo, ref _dsResponce, _dsSTP, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in  Pre issue validation Details Service for ");
                }
            }
            /*ID : 4 START*/
            else if (strIdentitiFlag == "PREISSUEVAL_COMBI")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : PREISSUEVAL" + System.Environment.NewLine);
                UWSaralServices.UWPreIssue_Combi objSTP = new UWSaralServices.UWPreIssue_Combi();
                _dsSTP = new DataSet();
                objComm.OnlineServicePreIssuevalDetails_GET(ref _dsSTP, strPQuoteNo, strAppType);
                objSTP.PreIssuevalidationPushService_Combi(strPQuoteNo, ref _dsResponce, _dsSTP, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in  Pre issue combi validation Details Service for ");
                }
            }
            /*ID : 4 END*/
            else if (strIdentitiFlag == "FOLLOWUP")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : REQUIRMENT" + System.Environment.NewLine);
                _dsPreIssueVal = new DataSet();
                UWSaralServices.FollowupDetails objFollowup = new UWSaralServices.FollowupDetails();
                objComm.OnlineServiceFollowUPDetails_GET(ref _dsFollowUp, strPQuoteNo, strAppType);
                if (_dsFollowUp.Tables.Count > 0 && _dsFollowUp.Tables[0].Rows.Count > 0)
                {
                    objFollowup.FollowuPushService(strPQuoteNo, _dsFollowUp, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                    if (LApushErrorCode != 0)
                    {
                        Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in follow up Details Service for ");

                    }
                    else
                    {
                        objComm.FetchMedicalRequirement(strPQuoteNo, ref _ds);
                        //if record exists then call service 
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            UWSaralServices.MedicalDataEntryInvoke objMedicalDataEntryInvoke = new UWSaralServices.MedicalDataEntryInvoke();
                            objMedicalDataEntryInvoke.MedicalPushService(strPQuoteNo, ref LApushErrorCode, ref LAPushErrorMsg);
                            Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : MEDICAL REQUIREMENT " + strPQuoteNo + System.Environment.NewLine);
                            if (LApushErrorCode == 0)
                            {
                                int intRet = -1;
                                objComm.UpdateMedicalRequirement(strPQuoteNo, ref intRet);
                            }
                        }
                    }
                }
            }
            else if (strIdentitiFlag == "CONTRACTMODIFICATION" || strIdentitiFlag == "proposal")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execprenution Start for  : CONTRACTMODIFICATION/proposal" + System.Environment.NewLine);
                #region Consume Contract Modification Begin.
                _dsPol = new DataSet();
                objComm.OnlineServiceContractDetails_GET(ref _dsPol, strPQuoteNo, strAppType);
                if (_dsPol.Tables.Count > 0 && _dsPol.Tables[0].Rows.Count > 0)
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : CONTRACT MODIFICATION" + System.Environment.NewLine);
                    UWSaralServices.ContractModification objContract = new UWSaralServices.ContractModification();
                    /*ID:2 START Dinesh Kondabattini*/
                    string MWP = objComm.CheckIsMwpApp(strPQuoteNo);
                    /*ID:2 END Dinesh Kondabattini */
                    objContract.ContractModPushService(strPQuoteNo, _dsPol, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg, MWP); /*ID:2 Added MWP Flag by Dinesh Kondabattini*/
                    if (LApushErrorCode != 0)
                    {
                        Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in Contact Modification Details Service for ");
                    }
                }
                #endregion Consume Contract Modification End.
                //if (LApushErrorCode == 0)
                //{
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : REQUIRMENT MODIFICATION" + System.Environment.NewLine);
                _dsPreIssueVal = new DataSet();
                UWSaralServices.FollowupDetails objFollowup = new UWSaralServices.FollowupDetails();
                objComm.OnlineServiceFollowUPDetails_GET(ref _dsFollowUp, strPQuoteNo, strAppType);
                if (_dsFollowUp.Tables.Count > 0 && _dsFollowUp.Tables[0].Rows.Count > 0)
                {
                    string strResponseMsg = LAPushErrorMsg;
                    objFollowup.FollowuPushService(strPQuoteNo, _dsFollowUp, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                    LAPushErrorMsg = "Follow up Service Message : " + (string.IsNullOrEmpty(LAPushErrorMsg) ? "Success " : LAPushErrorMsg) + " Contract Modification message : " + strResponseMsg;
                    if (LApushErrorCode != 0)
                    {
                        Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in followUp Modification Details Service  ");
                    }
                    else
                    {
                        objComm.FetchMedicalRequirement(strPQuoteNo, ref _ds);
                        //if record exists then call service 
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            UWSaralServices.MedicalDataEntryInvoke objMedicalDataEntryInvoke = new UWSaralServices.MedicalDataEntryInvoke();
                            TPARegisteration objTPAREgistration = new TPARegisteration();
                            TPASTATUSCODE.Service1 objTpaStatusCode = new Service1();
                            TPASTATUSCODE.BussLayer.Integration objCoreInt = new TPASTATUSCODE.BussLayer.Integration();
                            //objMedicalDataEntryInvoke.MedicalPushService(strPQuoteNo, ref LApushErrorCode, ref LAPushErrorMsg);
                            objCoreInt.strAppno = strPQuoteNo;
                            objCoreInt.strChannelType = strAppType;
                            objCoreInt.strReqRaisedBy = objCoreInt.strCreatedBy = objChangeValue.userLoginDetails._UserID.Substring(1);
                            objCoreInt.strFgiSubRefNo = string.Empty;
                            objCoreInt.strRequestFrom = "UWSARAL";
                            objCoreInt.strTpaRefNo = string.Empty;
                            Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : MEDICAL REQUIREMENT " + strPQuoteNo + System.Environment.NewLine);
                            objComm.UpdateMedicalRequirement(strPQuoteNo, ref LApushErrorCode);
                            //objTpaStatusCode.TPAIntegrationDetails_Insert(objCoreInt);
                        }
                        Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in follow up Details Service for ");
                    }
                }
                //}

                #region Consume Loading Begin.
                if (LApushErrorCode == 0)
                {
                    Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : LOADING" + System.Environment.NewLine);
                    _dsLoading = new DataSet();
                    UWSaralServices.LoadingDetails objLoading = new UWSaralServices.LoadingDetails();
                    //Bhaumik Patel CR-3334
                    objComm.OnlineServiceLoadingDetails_GET(ref _dsLoading, strPQuoteNo, string.Empty, strAppType);
                    
                   // objComm.OnlineServiceLoadingDetails_GET(ref _dsLoading, strPQuoteNo, objLoading.L, strAppType);
                   //
                    if (_dsLoading.Tables.Count > 0 && _dsLoading.Tables[0].Rows.Count > 0)
                    {
                        objLoading.LoadingPushService(strPQuoteNo, _dsLoading, objChangeValue, ref _dsResponce, ref LApushErrorCode, ref LAPushErrorMsg);
                        if (LApushErrorCode == 0 && strAppType.Trim().Equals("ONLINE"))
                        {
                            if (_dsResponce != null)
                            {
                                if (_dsResponce.Tables[0].Rows.Count > 0)
                                {

                                    objComm.OnlineLoadingDetails_Get(strPQuoteNo, ref _ds);

                                    TotalPremiumAB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["TotalPremiumAB"].ToString().Replace(".00", ""));
                                    LoadedPremiumAB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["LoadedPremiumAB"].ToString().Replace(".00", ""));
                                    TotalPremiumA = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["TotalPremiumA"].ToString().Replace(".00", ""));
                                    LoadedPremiumA = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["LoadedPremiumA"].ToString().Replace(".00", ""));
                                    TotalPremiumB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["TotalPremiumB"].ToString().Replace(".00", ""));
                                    LoadedPremiumB = Convert.ToInt32(_dsResponce.Tables[0].Rows[0]["LoadedPremiumB"].ToString().Replace(".00", ""));
                                    if (_ds != null)
                                    {
                                        if (_ds.Tables[0].Rows.Count > 0)
                                        {
                                            _strLoadDiscp = _ds.Tables[0].Rows[0]["LoadDecription"].ToString();
                                            _strLoadCode = _ds.Tables[0].Rows[0]["LoadingCode"].ToString();
                                            _strReason = _ds.Tables[0].Rows[0]["LoadingReason"].ToString();
                                        }
                                        objComm.OnlinepremiumLoadingDetails_Save(strPQuoteNo, LoadedPremiumAB, _strLoadDiscp, _strLoadCode, _strReason, "", _strLoadDiscp, LoadedPremiumA, LoadedPremiumB, TotalPremiumA, TotalPremiumB, "", ref strLApushErrorCode);
                                    }
                                }
                            }
                        }

                        if (LApushErrorCode == 0 && strAppType.Trim().ToUpper().Equals("OFFLINE"))
                        {
                            isLoadingUpdated = true;

                        }
                        if (LApushErrorCode != 0)
                        {
                            Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in Loading Details Service  ");
                        }
                    }
                }
                #endregion Consume Loading End.

                if (objChangeValue.Load_Loadingdetails != null)
                {
                    {
                        if (objChangeValue.Load_Loadingdetails.isConsentRequired && isLoadingUpdated)
                            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : CONSENT LETTER" + System.Environment.NewLine);
                        UWSaralServices.ConsentLetter objConsentLetter = new UWSaralServices.ConsentLetter();
                        objComm.OnlineConsentDetails_Get(ref _ds, strPQuoteNo, strAppType);
                        objConsentLetter.ConsentletterService(strPQuoteNo, "", _ds, ref strConsentRespons);
                    }
                }

            }
            else if (strIdentitiFlag == "PANVALIDATION")
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : PANVALIDATION");
                _dsPol = new DataSet();
                objComm.OnlineServicePancardDetails_GET(ref _dsPanval, strPQuoteNo, strAppType);
                UWSaralServices.PancardValidationDetails objPanVal = new UWSaralServices.PancardValidationDetails();
                objPanVal.PanCardPushService(strPQuoteNo, ref _dsResponce, _dsPanval, strDatavalue, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in pan validation Details Service  ");
                }
            }
            else if (strIdentitiFlag == "PREMIUMCAL")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : PREMIUMCAL" + System.Environment.NewLine);
                _dsPol = new DataSet();

                objComm.OnlineServicePremiumCalDetails_GET(ref _dsPremcal, strPQuoteNo, strAppType);
                UWSaralServices.PremiumCalculationDetails objPremcal = new UWSaralServices.PremiumCalculationDetails();
                objPremcal.PremiumCalculationPushService(strPQuoteNo, ref _dsResponce, objChangeValue, _dsPremcal, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in Premium calculation Details Service  ");
                }
            }
            else if (strIdentitiFlag == "MSAR")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : MSAR" + System.Environment.NewLine);
                _dsPol = new DataSet();
                objComm.OnlineTsmrMsarDisplayDetails_GET(ref _dsPol, strPQuoteNo, strAppType);
                UWSaralServices.MsarTsarDetails objMsarcal = new UWSaralServices.MsarTsarDetails();
                objMsarcal.MsarTsarPushService(strPQuoteNo, ref _dsResponce, ref _dsPrevPol, _dsPol, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in Msar/tsar Calculation Details Service  ");
                }
            }
            else if (strIdentitiFlag == "RATEUP")
            {
                #region Consume Loading Begin.
                if (LApushErrorCode == 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : RATEUP");
                    _dsLoading = new DataSet();
                    UWSaralServices.LoadingDetails objLoading = new UWSaralServices.LoadingDetails();
                    if (objChangeValue.Load_Loadingdetails == null)
                    {
                        objChangeValue.Load_Loadingdetails._strProdcode = "";
                    }
                    objComm.OnlineServiceLoadingDetails_GET(ref _dsLoading, strPQuoteNo, objChangeValue.Load_Loadingdetails._strProdcode, strAppType);
                    if (_dsLoading.Tables.Count > 0 && _dsLoading.Tables[0].Rows.Count > 0)
                    {
                        objLoading.LoadingPushService(strPQuoteNo, _dsLoading, objChangeValue, ref _dsResponce, ref LApushErrorCode, ref LAPushErrorMsg);
                        if (LApushErrorCode != 0)
                        {
                            Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in Loading Details Service  ");
                        }
                    }
                }
                else
                {
                    //Error Message.
                }
                #endregion Consume Loading End.
            }
            else if (strIdentitiFlag == "AFI")
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : AFI");
                _dsPol = new DataSet();
                UWSaralServices.AFI objAfi = new UWSaralServices.AFI();
                objAfi.ConAFI(strPQuoteNo, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);

                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : AFI");
                }
            }
            else if (strIdentitiFlag == "CFI")
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : CFI");
                _dsPol = new DataSet();
                UWSaralServices.AFI objAfi = new UWSaralServices.AFI();
                objAfi.ConCFI(strPQuoteNo, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);

                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : CFI");
                }
            }
            else if (strIdentitiFlag == "UW")
            {
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : UW");
                _dsPol = new DataSet();
                UWSaralServices.UnderWriter objUw = new UWSaralServices.UnderWriter();
                objUw.UWAREV(strPQuoteNo, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode != 0)
                {
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : UW");
                }
            }
            /*added by shri */
            else if (strIdentitiFlag == "CLIENTUPDATION")
            {
                UWSaralServices.ClientUpdation objClientUpdation = new UWSaralServices.ClientUpdation();
                objComm.OnlineServiceClientDetails_GET(ref _ds, strPQuoteNo, objChangeValue.ClientDetails.ClientId);
                objClientUpdation.ClientUpdationModPushService(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
            }
            else if (strIdentitiFlag == "RECEIPTCANCELATION")
            {
                UWSaralServices.ReceiptCancelation objClientUpdation = new UWSaralServices.ReceiptCancelation();
                objComm.OnlineServiceRecepitDetails_GET(ref _ds, strPQuoteNo);
                objClientUpdation.ReceiptCancelation_PUSH(strPQuoteNo, objChangeValue, _ds, ref LApushErrorCode, ref LAPushErrorMsg);
                if (LApushErrorCode == 0)
                {
                    string strCancelationReason = "PYCH";
                    string strStatus = "canceled";
                    objComm.ChangeReceiptStatus_PUSH(strPQuoteNo, strStatus, strCancelationReason);
                }
            }
            else if (strIdentitiFlag == "CLIENTCREATION")
            {
                UWSaralServices.ClientCreation objClientCreateion = new UWSaralServices.ClientCreation();
                objClientCreateion.ClientCreationModPushService(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
            }
            else if (strIdentitiFlag == "BANKENQ")
            {
                string strResponse = string.Empty, strSpace = " ";
                UWSaralServices.BankEnquiryDetails objBankEnquiryDetails = new UWSaralServices.BankEnquiryDetails();
                UWSaralServices.MandateDetails objMandateDetails = new UWSaralServices.MandateDetails();
                DataSet _ds = new DataSet();
                string strResp = string.Empty;

                //fetch data from db 
                objComm.BankEnqManageDetails_GET(strPQuoteNo, ref _ds);
                //call bank enquiry service                
                objBankEnquiryDetails.BankEnquireService(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                //check response 
                if (LApushErrorCode == -3 && !string.IsNullOrEmpty(LAPushErrorMsg))
                {
                    //update bank details
                    objBankEnquiryDetails.BankUpdateService(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                    strResponse = "Bank Updated Successfully";
                }
                else if (LApushErrorCode == -2 || LApushErrorCode == 0)
                {
                    //create bank details
                    objBankEnquiryDetails.BankPushService(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                    LApushErrorCode = -1;
                    strResponse = "Bank Created Successfully";
                }
                else
                {
                    strResponse = "Bank Enquiry Failed";
                }
                if (LApushErrorCode == 0)
                {
                    //create mandate
                    objMandateDetails.MandateCreateService(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                    if (LApushErrorCode == 0)
                    {
                        strResponse = strResponse + strSpace + "Mandate Created Successfully";
                        //approve mandate
                        objMandateDetails.MandatePushService(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                    }

                }
                LAPushErrorMsg = strResponse;
            }
            /*added by shri on 08 sept to call ofac service*/
            else if (strIdentitiFlag == "OFAC")
            {
                UWSaralServices.Ofac objOfac = new UWSaralServices.Ofac();
                DataSet _ds = new DataSet();
                CommFun objCommon = new CommFun();
                objCommon.OfacRequest_GET(strPQuoteNo, ref _ds);
                objOfac.VerifyOfac(strPQuoteNo, _ds, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
            }
            else if (strIdentitiFlag == "JOURNAL")
            {
                UWSaralServices.LAJournalDetails objLAJournalDetails = new UWSaralServices.LAJournalDetails();
                objLAJournalDetails.JournalAuth(strPQuoteNo, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);

            }
            //ID:1 BEGINS
            else if (strIdentitiFlag == "AADHAR_VERIFICATION")
            {
                UWSaralServices.LAAdhar objAadhar = new UWSaralServices.LAAdhar();
                DataSet _ds = new DataSet();
                CommFun objComm = new CommFun();
                objComm.FetchAadharDetails(strPQuoteNo, strAppType, ref _ds);
                objAadhar.Demographic(strPQuoteNo, _ds, ref _dsResponce, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);

            }
            //ID:1 END
            /*end here*/
            /*ID : 6 START*/
            //added by suraj for combi product enq--start
            else if (strIdentitiFlag == "COMBIENQ")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : PREISSUEVAL" + System.Environment.NewLine);
                UWSaralServices.LACombi_DetachService.ServiceClient objCombiENQ = new UWSaralServices.LACombi_DetachService.ServiceClient();
                UWSaralServices.LACombi_DetachService.IService objCombiENQ1 = new UWSaralServices.LACombi_DetachService.ServiceClient();
                UWSaralServices.LACombi_DetachService.MasterCADDET objCombiDET = new UWSaralServices.LACombi_DetachService.MasterCADDET();

                DataSet _dsCombiENQ = new DataSet();
                objComm.CombiApplicationENQData_GET(ref _dsCombiENQ, strPQuoteNo, strAppType);
                objCombiResponse_ENQ = objCombiENQ.CADENQ("C", _dsCombiENQ.Tables[0].Rows[0]["ParentAppNo"].ToString(), "", "", objChangeValue.userLoginDetails._UserRole, objChangeValue.userLoginDetails._UserID,
                    "", "SARALUW", strPQuoteNo);//(strPQuoteNo, ref _dsResponce, _dsSTP, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);
                if (true)
                {

                }
                if (LApushErrorCode != 0)
                {

                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Approve" + System.Environment.NewLine + "Error in  Pre issue validation Details Service for ");
                }
            }
            //end
            /*ID : 6 END*/
            /*ID : 5 START*/
            //added by suraj for combi product detach--start
            else if (strIdentitiFlag == "COMBIDET")
            {
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Execution Start for  : PREISSUEVAL" + System.Environment.NewLine);
                UWSaralServices.LACombi_DetachService.ServiceClient objCombi = new UWSaralServices.LACombi_DetachService.ServiceClient();
                UWSaralServices.LACombi_DetachService.IService objCombiENQ1 = new UWSaralServices.LACombi_DetachService.ServiceClient();
                UWSaralServices.LACombi_DetachService.MasterCADDET objCombiDET = new UWSaralServices.LACombi_DetachService.MasterCADDET();
                string struserid = string.Empty, struserrole = string.Empty;
                if (objChangeValue != null && objChangeValue.userLoginDetails != null)
                {
                    struserid = objChangeValue.userLoginDetails._UserID;
                    struserrole = objChangeValue.userLoginDetails._UserRole;
                }
                else
                {
                    struserid = "WEBSITE";
                }
                DataSet _dsCombiENQ = new DataSet();
                DataSet _dsCombiDET = new DataSet();
                //get data for combi enq service----
                objComm.CombiApplicationENQData_GET(ref _dsCombiENQ, strPQuoteNo, strAppType);

                //call service of combi enq
                objCombiResponse_ENQ = objCombi.CADENQ("C", _dsCombiENQ.Tables[0].Rows[0]["ParentAppNo"].ToString(), "", "",struserrole, struserid, "", "SARALUW", strPQuoteNo);

                LApushErrorCode = Convert.ToInt32(objCombiResponse_ENQ.ERRORCODE);

                if (LApushErrorCode==0)
                {
                    string strCHDRNUM01 = string.Empty, strCHDRNUM02 = string.Empty, strCHDRNUM03 = string.Empty, strCHDRNUM04 = string.Empty,
                    strCHDRNUM05 = string.Empty, strCHDRNUM06 = string.Empty, strCHDRNUM07 = string.Empty, strCHDRNUM08 = string.Empty,
                    strCHDRNUM09 = string.Empty, strCHDRNUM10 = string.Empty, strCHDRNUM11 = string.Empty;

                    string strCNTTYPE01 = string.Empty, strCNTTYPE02 = string.Empty, strCNTTYPE03 = string.Empty, strCNTTYPE04 = string.Empty, strCNTTYPE05 = string.Empty, strCNTTYPE06 = string.Empty,
                        strCNTTYPE07 = string.Empty, strCNTTYPE08 = string.Empty, strCNTTYPE09 = string.Empty, strCNTTYPE10 = string.Empty, strCNTTYPE11 = string.Empty;

                    strCHDRNUM01 = objCombiResponse_ENQ.CHDRNUM01.Trim() == "" ? "" : "Y";
                    strCHDRNUM02 = objCombiResponse_ENQ.CHDRNUM02.Trim() == "" ? "" : "Y";
                    strCHDRNUM03 = objCombiResponse_ENQ.CHDRNUM03.Trim() == "" ? "" : "Y";
                    strCHDRNUM04 = objCombiResponse_ENQ.CHDRNUM04.Trim() == "" ? "" : "Y";
                    strCHDRNUM05 = objCombiResponse_ENQ.CHDRNUM05.Trim() == "" ? "" : "Y";
                    strCHDRNUM06 = objCombiResponse_ENQ.CHDRNUM06.Trim() == "" ? "" : "Y";
                    strCHDRNUM07 = objCombiResponse_ENQ.CHDRNUM07.Trim() == "" ? "" : "Y";
                    strCHDRNUM08 = objCombiResponse_ENQ.CHDRNUM08.Trim() == "" ? "" : "Y";
                    strCHDRNUM09 = objCombiResponse_ENQ.CHDRNUM09.Trim() == "" ? "" : "Y";
                    strCHDRNUM10 = objCombiResponse_ENQ.CHDRNUM10.Trim() == "" ? "" : "Y";
                    strCHDRNUM11 = objCombiResponse_ENQ.CHDRNUM11.Trim() == "" ? "" : "Y";

                    strCNTTYPE01 = objCombiResponse_ENQ.CNTTYPE01.Trim() == "" ? "" : "MISC";
                    strCNTTYPE02 = objCombiResponse_ENQ.CNTTYPE02.Trim() == "" ? "" : "MISC";
                    strCNTTYPE03 = objCombiResponse_ENQ.CNTTYPE03.Trim() == "" ? "" : "MISC";
                    strCNTTYPE04 = objCombiResponse_ENQ.CNTTYPE04.Trim() == "" ? "" : "MISC";
                    strCNTTYPE05 = objCombiResponse_ENQ.CNTTYPE05.Trim() == "" ? "" : "MISC";
                    strCNTTYPE06 = objCombiResponse_ENQ.CNTTYPE06.Trim() == "" ? "" : "MISC";
                    strCNTTYPE07 = objCombiResponse_ENQ.CNTTYPE07.Trim() == "" ? "" : "MISC";
                    strCNTTYPE08 = objCombiResponse_ENQ.CNTTYPE08.Trim() == "" ? "" : "MISC";
                    strCNTTYPE09 = objCombiResponse_ENQ.CNTTYPE09.Trim() == "" ? "" : "MISC";
                    strCNTTYPE10 = objCombiResponse_ENQ.CNTTYPE10.Trim() == "" ? "" : "MISC";
                    strCNTTYPE11 = objCombiResponse_ENQ.CNTTYPE11.Trim() == "" ? "" : "MISC";


                    if (LApushErrorCode == 0)
                    {


                        objComm.CombiApplicationENQData_GET(ref _dsCombiDET, strPQuoteNo, strAppType);
                        objCombiResponse_DET = objCombi.CADDET("B", _dsCombiDET.Tables[0].Rows[0]["ParentAppNo"].ToString(), strCHDRNUM01, strCHDRNUM02, strCHDRNUM03,
                                                    strCHDRNUM04, strCHDRNUM05, strCHDRNUM06, strCHDRNUM07, strCHDRNUM08, strCHDRNUM09, strCHDRNUM10, strCHDRNUM11,
                                                    strCNTTYPE01, strCNTTYPE02, strCNTTYPE03, strCNTTYPE04, strCNTTYPE05, strCNTTYPE06, strCNTTYPE07, strCNTTYPE08,
                                                    strCNTTYPE09, strCNTTYPE10, strCNTTYPE11, "", "", struserrole, struserid, "SARALUW", "SARALUW", strPQuoteNo);//(strPQuoteNo, ref _dsResponce, _dsSTP, objChangeValue, ref LApushErrorCode, ref LAPushErrorMsg);

                    }
                    LApushErrorCode = Convert.ToInt32(objCombiResponse_DET.ERRORCODE);
                    if (LApushErrorCode != 0)
                    {
                        Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Pol Detach" + System.Environment.NewLine + "Error in  Pplicy detach fro Combi product ");
                    }
                }
                else
                {
                    if (objCombiResponse_ENQ.VALUES.ToString()== "Combi Plan doesn't exist")
                    {
                        LApushErrorCode = 0;
                    }
                    
                    Logger.Info(strPQuoteNo + "STAG 2 :PageName :Busslayer.CS // MethodeName :OnlineApplicationLAServiceDetails_PUSH //UWDeceision : Pol Detach" + System.Environment.NewLine + "Error in  Pplicy detach fro Combi product or already detach");
                }
                
            }
            //end
            /*ID : 5 END*/
        }
    }
}
