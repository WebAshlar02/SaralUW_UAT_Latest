/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET (013)
METHODE/EVENT:
REMARK: ADD AFI TYPE, RCD AND ASSIGN UW 
DateTime :24Feb18
**********************************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using UWSaralObjects;
using System.Configuration;

public partial class UserControl_PopupAfiCfiUW : System.Web.UI.UserControl
{
    #region variable declaration
    string strIdentitiFlag, strUserId, strUserRole, strBranch;
    DataSet _dsPrevPol = new DataSet();
    ChangeValue objChangeObj = new ChangeValue();
    Commfun objcomm = new Commfun();
    string strConsentRespons = string.Empty;
    #endregion

    #region event
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet _ds;
        /*ID:1 START*/
        //if (Page.IsPostBack)
        //{
        //fetch users 
        _ds = new DataSet();
        FetchActiveUsers(ref _ds);
        //bind users to drop down
        BindActiveUsers(_ds);
        //}
        /*ID:1 END*/
        /*added by shri to use ajax on 08 june 17*/
        Ajax.Utility.RegisterTypeForAjax(typeof(UserControl_PopupAfiCfiUW), Page);
        /*end here*/
    }
    #endregion

    #region ajax method
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string FetchAFI(string strAppno, string strPolNo, string strComment)
    {
        string strRet = string.Empty;
        try
        {
            //fetch session
            CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
            ChangeValue objChangeValue1 = (ChangeValue)System.Web.HttpContext.Current.Session["objLoginObj"];
            LoginUserDetails objLoginUserDetails = objChangeValue1.userLoginDetails;
            strAppno = strAppno.Trim();
            strPolNo = strPolNo.Trim();
            //fetch details from database
            DataSet _ds = new DataSet();
            FetchAfiCfiDetais(ref _ds, strAppno, strPolNo);
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                //prepoare request to call service
                UWSaralObjects.ChangeValue objChangeValue = new UWSaralObjects.ChangeValue();
                UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
                string strPQuoteNo = Convert.ToString(_ds.Tables[0].Rows[0]["APPLICATION_NUMBER"])
                    , strAppType = objCommonObj._AppType
                    , strLAPushErrorMsg = string.Empty
                    , strParentId = string.Empty
                    , strProcessId = string.Empty;
                strUserId = string.IsNullOrEmpty(objLoginUserDetails._UserID) ? string.Empty : objLoginUserDetails._UserID;
                strIdentitiFlag = "AFI";
                strBranch = string.IsNullOrEmpty(objLoginUserDetails._userBranch) ? string.Empty : objLoginUserDetails._userBranch;
                strUserRole = string.IsNullOrEmpty(objLoginUserDetails._UserRole) ? string.Empty : objLoginUserDetails._UserRole;
                UWSaralObjects.AfiCfiUW objAfiCfiUw = new UWSaralObjects.AfiCfiUW();
                int intLApushErrorCod = -1;
                DataSet _dsResponce = new DataSet();
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    objAfiCfiUw._PolicyNo = Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_NUMBER"]);
                    objAfiCfiUw._Branch = strBranch;
                    objAfiCfiUw._UserRole = strUserRole;
                    objAfiCfiUw._PartnerId = strParentId;
                    objAfiCfiUw._strProcessId = strProcessId;
                    objAfiCfiUw._ApplicationNo = strAppno;
                    objAfiCfiUw._UserId = strUserId;
                    objAfiCfiUw._ReasonDesc = strComment;
                    objAfiCfiUw._ReasonId = "";
                    objChangeValue.AfiCfiUw = objAfiCfiUw;

                    //call service
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strPQuoteNo, strAppType, objChangeValue, ref _dsResponce, ref _dsPrevPol, strIdentitiFlag
                                                                            , ref intLApushErrorCod, ref strLAPushErrorMsg, ref strConsentRespons);
                    //validate the response
                    if (intLApushErrorCod == 0)
                    {
                        string strPolicyStatus = "PS";
                        string strPolicyNumber = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_NUMBER"]);
                        int intRetVal = -1;
                        //objChangeObj = (ChangeValue)Session["objLoginObj"];
                        //This is use to change the status of the Application to AFI.
                        //objcomm.MaintainApplicationLog(strPQuoteNo, strIdentitiFlag, string.Empty, objChangeObj.userLoginDetails._UserID, "", ref intRetVal);
                        int intCommitResult = -1;
                        objcomm.OnlineUWCommentsDetails_Save(objCommonObj._bpm_userName, objCommonObj._ProcessName, objCommonObj._bpmgrp, strComment, objCommonObj._ApplicationNo, objCommonObj._bpm_userID, objCommonObj._bpm_userName, objCommonObj._bpmgrp, objCommonObj._bpm_branchCode, objCommonObj._bpm_userBranch, objCommonObj._ProcessName, strPQuoteNo, "General", ref intCommitResult);
                        //update policy status
                        UpdatePolicyStatus(strPQuoteNo, strPolicyStatus, ref intRetVal);
                        //insert afi type and backdate


                        if (intRetVal > 0)
                        {
                            //maintain log
                            (new UWSaralDecision.CommFun()).LogClientProfile_Push(" applicaiton no " + strAppno + " " + Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_STATUS"]), "Application No " + strAppno + " " + strPolicyStatus, (int)ClientAction.Afi, objLoginUserDetails._UserID
                                        , objLoginUserDetails._UserName, objLoginUserDetails._UserGroup, ref intRetVal);
                        }
                        //serialize and show data
                        UWSaralObjects.PolicyDetails objPolicyDetails = new UWSaralObjects.PolicyDetails();
                        objPolicyDetails.ApplicationNumber = strPQuoteNo;
                        objPolicyDetails.PolicyNumber = strPolicyNumber;
                        objPolicyDetails.FirstNameLgivName = Convert.ToString(_ds.Tables[0].Rows[0]["FIRST_NAME_LGIVNAME"]);
                        if (intRetVal > 0)
                        {
                            objPolicyDetails.PolicyStatus = strPolicyStatus;
                        }
                        else
                        {
                            objPolicyDetails.PolicyStatus = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_STATUS"]);
                        }
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + (new JavaScriptSerializer()).Serialize(objPolicyDetails);

                        //saving comment 
                        int intRet = -1;
                        objcomm.ManageApplicationStatus(strAppno, "AFI", string.Empty, strUserId, string.Empty, "SARALUW", ref intRet);
                    }
                    else
                    {
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + strLAPushErrorMsg;
                    }
                }
            }
            else
            {
                strRet = "1#No Records Found";
            }
        }
        catch (Exception ex)
        {
            strRet = "1#Try Again Later";
        }
        return strRet;
    }

    /*ID:1 START*/
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string FetchAFIWithUnderWriterSelection(string strAppno, string strPolNo, string strComment, string strAFIType, string strRCD, string strAssignTo)
    {
        string strRet = string.Empty;
        try
        {            
            //fetch session
            CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
            ChangeValue objChangeValue1 = (ChangeValue)System.Web.HttpContext.Current.Session["objLoginObj"];
            LoginUserDetails objLoginUserDetails = objChangeValue1.userLoginDetails;
            strAppno = strAppno.Trim();
            strPolNo = strPolNo.Trim();
            //fetch details from database
            DataSet _ds = new DataSet();
            FetchAfiCfiDetais(ref _ds, strAppno, strPolNo);
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                //prepoare request to call service
                UWSaralObjects.ChangeValue objChangeValue = new UWSaralObjects.ChangeValue();
                UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
                string strPQuoteNo = Convert.ToString(_ds.Tables[0].Rows[0]["APPLICATION_NUMBER"])
                    , strAppType = objCommonObj._AppType
                    , strLAPushErrorMsg = string.Empty
                    , strParentId = string.Empty
                    , strProcessId = string.Empty;
                strUserId = string.IsNullOrEmpty(objLoginUserDetails._UserID) ? string.Empty : objLoginUserDetails._UserID;
                strIdentitiFlag = "AFI";
                strBranch = string.IsNullOrEmpty(objLoginUserDetails._userBranch) ? string.Empty : objLoginUserDetails._userBranch;
                strUserRole = string.IsNullOrEmpty(objLoginUserDetails._UserRole) ? string.Empty : objLoginUserDetails._UserRole;
                UWSaralObjects.AfiCfiUW objAfiCfiUw = new UWSaralObjects.AfiCfiUW();
                int intLApushErrorCod = -1;
                DataSet _dsResponce = new DataSet();
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    objAfiCfiUw._PolicyNo = Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_NUMBER"]);
                    objAfiCfiUw._Branch = strBranch;
                    objAfiCfiUw._UserRole = strUserRole;
                    objAfiCfiUw._PartnerId = strParentId;
                    objAfiCfiUw._strProcessId = strProcessId;
                   // objAfiCfiUw._ApplicationNo = strAppno;
                    objAfiCfiUw._ApplicationNo = strPQuoteNo;
                    objAfiCfiUw._UserId = strUserId;
                    objAfiCfiUw._ReasonDesc = strComment;
                    objAfiCfiUw._ReasonId = "";
                    objChangeValue.AfiCfiUw = objAfiCfiUw;

                    //call service
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strPQuoteNo, strAppType, objChangeValue, ref _dsResponce, ref _dsPrevPol, strIdentitiFlag
                                                                            , ref intLApushErrorCod, ref strLAPushErrorMsg, ref strConsentRespons);
                    //validate the response
                    if (intLApushErrorCod == 0)
                    {
                        string strPolicyStatus = "PS";
                        string strPolicyNumber = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_NUMBER"]);
                        int intRetVal = -1;
                        //objChangeObj = (ChangeValue)Session["objLoginObj"];
                        //This is use to change the status of the Application to AFI.
                        //objcomm.MaintainApplicationLog(strPQuoteNo, strIdentitiFlag, string.Empty, objChangeObj.userLoginDetails._UserID, "", ref intRetVal);
                        int intCommitResult = -1;
                        objcomm.OnlineUWCommentsDetails_Save(objCommonObj._bpm_userName, objCommonObj._ProcessName, objCommonObj._bpmgrp, strComment, strPQuoteNo, objCommonObj._bpm_userID, objCommonObj._bpm_userName, objCommonObj._bpmgrp, objCommonObj._bpm_branchCode, objCommonObj._bpm_userBranch, objCommonObj._ProcessName, strPQuoteNo, "General", ref intCommitResult);
                        //update policy status
                        UpdatePolicyStatus(strPQuoteNo, strPolicyStatus, ref intRetVal);
                        //insert afi type and backdate and assign user
                        objcomm.MangeAFITypeRCDUnderWriter(strPolNo,strAFIType, strRCD, strAssignTo, strUserId,ref intRetVal);

                        if (intRetVal > 0)
                        {
                            //maintain log
                            (new UWSaralDecision.CommFun()).LogClientProfile_Push(" applicaiton no " + strAppno + " " + Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_STATUS"]), "Application No " + strAppno + " " + strPolicyStatus, (int)ClientAction.Afi, objLoginUserDetails._UserID
                                        , objLoginUserDetails._UserName, objLoginUserDetails._UserGroup, ref intRetVal);
                        }
                        //serialize and show data
                        UWSaralObjects.PolicyDetails objPolicyDetails = new UWSaralObjects.PolicyDetails();
                        objPolicyDetails.ApplicationNumber = strPQuoteNo;
                        objPolicyDetails.PolicyNumber = strPolicyNumber;
                        objPolicyDetails.FirstNameLgivName = Convert.ToString(_ds.Tables[0].Rows[0]["FIRST_NAME_LGIVNAME"]);
                        if (intRetVal > 0)
                        {
                            objPolicyDetails.PolicyStatus = strPolicyStatus;
                        }
                        else
                        {
                            objPolicyDetails.PolicyStatus = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_STATUS"]);
                        }
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + (new JavaScriptSerializer()).Serialize(objPolicyDetails);

                        //saving comment 
                        int intRet = -1;
                        objcomm.ManageApplicationStatus(strAppno, "AFI", string.Empty, strUserId, string.Empty, "SARALUW", ref intRet);

                        //Added by kavita CR-30542
                        UWSaralServices.LAUwdecisionService.ServiceClient svccntrctdecision = new UWSaralServices.LAUwdecisionService.ServiceClient();
                        UWSaralServices.LAUwdecisionService.Master dsmaster = new UWSaralServices.LAUwdecisionService.Master();
                        if (strAFIType == "3")
                        {
                            string ReasonCD = Convert.ToString(ConfigurationManager.AppSettings["ReasonCD"]);
                            string ReasonDescription = Convert.ToString(ConfigurationManager.AppSettings["ReasonDescription"]);
                            dsmaster = svccntrctdecision.PRPWDR(strPolNo, ReasonCD, ReasonDescription, DateFormatfirstdate1(System.DateTime.Now.ToShortDateString()).ToString(), strBranch, strUserRole, strUserId,
                                     objLoginUserDetails._UserName, objCommonObj._ProcessName, strAppno);

                        }

                        if (dsmaster.ERRORCODE == "0")
                        {
                            strRet = "1#Policy Withdrawal";
                            strPolicyStatus = "WD";
                            intRetVal = -1;
                            //update policy status
                            UpdatePolicyStatus(strPQuoteNo, strPolicyStatus, ref intRetVal);
                            if (intRetVal > 0)
                            {
                                (new UWSaralDecision.CommFun()).LogClientProfile_Push(" application no " + strAppno + " " + "PS", "Application No " + strAppno + " " + strPolicyStatus, (int)ClientAction.Afi, objLoginUserDetails._UserID
                                         , objLoginUserDetails._UserName, objLoginUserDetails._UserGroup, ref intRetVal);
                            }
                        }
                        else
                        {
                            strRet = "1#Policy Not Withdrawal";
                        }
                        //Ended by kavita CR-30542
                    }
                    else
                    {
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + strLAPushErrorMsg;
                    }
                }
                //UWDecisionService. svccntrctdecision = new UWDecisionService.ServiceClient();
                //UWDecisionService.Master dsmaster = new UWDecisionService.Master();
                //dsmaster = svccntrctdecision.PRPWDR(clscrt.PolicyNumber, ReasonCD, ReasonDescription,
                //                                     clsDAL.DateFormatfirstdate1(System.DateTime.Now.ToShortDateString()).ToString(), UserBranch, UserRole, UserID, UserName, ProcessName, mdtbo.ApplicationNumber);
               
            }
            else
            {
                strRet = "1#No Records Found";
            }
        }
        catch (Exception ex)
        {
            strRet = "1#Try Again Later";
        }
        return strRet;
    }
    /*ID:1 END*/

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string FetchCFI(string strAppno, string strPolNo, string strComment)
    {
        string strRet = string.Empty;
        try
        {
            //fetch session
            CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
            ChangeValue objChangeValue1 = (ChangeValue)System.Web.HttpContext.Current.Session["objLoginObj"];
            LoginUserDetails objLoginUserDetails = objChangeValue1.userLoginDetails;
            strAppno = strAppno.Trim();
            strPolNo = strPolNo.Trim();

            //fetch details from database
            DataSet _ds = new DataSet();
            FetchAfiCfiDetais(ref _ds, strAppno, strPolNo);
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                //prepoare request to call service
                UWSaralObjects.ChangeValue objChangeValue = new UWSaralObjects.ChangeValue();
                UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
                string strPQuoteNo = Convert.ToString(_ds.Tables[0].Rows[0]["APPLICATION_NUMBER"])
                    , strAppType = string.Empty
                    , strLAPushErrorMsg = string.Empty
                    , strParentId = string.Empty
                    , strProcessId = string.Empty;
                strUserId = string.IsNullOrEmpty(objLoginUserDetails._UserID) ? string.Empty : objLoginUserDetails._UserID;
                strIdentitiFlag = "CFI";
                strBranch = string.IsNullOrEmpty(objLoginUserDetails._userBranch) ? string.Empty : objLoginUserDetails._userBranch;
                strUserRole = string.IsNullOrEmpty(objLoginUserDetails._UserRole) ? string.Empty : objLoginUserDetails._UserRole;
                UWSaralObjects.AfiCfiUW objAfiCfiUw = new UWSaralObjects.AfiCfiUW();
                int intLApushErrorCod = -1;
                DataSet _dsResponce = new DataSet();
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    objAfiCfiUw._PolicyNo = Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_NUMBER"]);
                    objAfiCfiUw._Branch = strBranch;
                    objAfiCfiUw._UserRole = strUserRole;
                    objAfiCfiUw._PartnerId = strParentId;
                    objAfiCfiUw._strProcessId = strProcessId;
                    objAfiCfiUw._ApplicationNo = strAppno;
                    objAfiCfiUw._UserId = strUserId;
                    objAfiCfiUw._ReasonDesc = strComment;
                    objChangeValue.AfiCfiUw = objAfiCfiUw;

                    //call service
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strPQuoteNo, strAppType, objChangeValue, ref _dsResponce, ref _dsPrevPol, strIdentitiFlag
                                                                            , ref intLApushErrorCod, ref strLAPushErrorMsg, ref strConsentRespons);
                    //validate the response
                    if (intLApushErrorCod == 0)
                    {
                        string strPolicyStatus = "PS";
                        string strPolicyNumber = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_NUMBER"]);
                        int intRetVal = -1;

                        //update policy status
                        UpdatePolicyStatus(strPQuoteNo, strPolicyStatus, ref intRetVal);

                        //maintain log
                        if (intRetVal > 0)
                        {
                            (new UWSaralDecision.CommFun()).LogClientProfile_Push(" applicaiton no " + strAppno + " " + Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_STATUS"]), "Application No " + strAppno + " " + strPolicyStatus, (int)ClientAction.Cfi, objLoginUserDetails._UserID
                                        , objLoginUserDetails._UserName, objLoginUserDetails._UserGroup, ref intRetVal);
                        }
                        //serialize and show data
                        UWSaralObjects.PolicyDetails objPolicyDetails = new UWSaralObjects.PolicyDetails();
                        objPolicyDetails.ApplicationNumber = strPQuoteNo;
                        objPolicyDetails.PolicyNumber = strPolicyNumber;
                        objPolicyDetails.FirstNameLgivName = Convert.ToString(_ds.Tables[0].Rows[0]["FIRST_NAME_LGIVNAME"]);
                        if (intRetVal > 0)
                        {
                            objPolicyDetails.PolicyStatus = strPolicyStatus;
                        }
                        else
                        {
                            objPolicyDetails.PolicyStatus = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_STATUS"]);
                        }
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + (new JavaScriptSerializer()).Serialize(objPolicyDetails);

                        //saving comment                         
                        Commfun objComm = new Commfun();
                        int intCommitResult = -1;
                        objComm.OnlineUWCommentsDetails_Save(objCommonObj._bpm_userName, objCommonObj._ProcessName, objCommonObj._bpmgrp, strComment, objCommonObj._ApplicationNo, objCommonObj._bpm_userID, objCommonObj._bpm_userName, objCommonObj._bpmgrp, objCommonObj._bpm_branchCode, objCommonObj._bpm_userBranch, objCommonObj._ProcessName, strPQuoteNo, "General", ref intCommitResult);
                    }
                    else
                    {
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + strLAPushErrorMsg;
                    }
                }
            }
            else
            {
                strRet = "1#No Records Found";
            }
        }
        catch (Exception ex)
        {
            strRet = "1#Try Again Later";
        }
        return strRet;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string FetchUW(string strAppno, string strPolNo, string strComment)
    {
        string strRet = string.Empty;
        try
        {
            //fetch session
            CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
            ChangeValue objChangeValue1 = (ChangeValue)System.Web.HttpContext.Current.Session["objLoginObj"];
            LoginUserDetails objLoginUserDetails = objChangeValue1.userLoginDetails;
            strAppno = strAppno.Trim();
            strPolNo = strPolNo.Trim();

            //fetch details from database
            DataSet _ds = new DataSet();
            FetchAfiCfiDetais(ref _ds, strAppno, strPolNo);
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                //prepoare request to call service
                UWSaralObjects.ChangeValue objChangeValue = new UWSaralObjects.ChangeValue();
                UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
                string strPQuoteNo = Convert.ToString(_ds.Tables[0].Rows[0]["APPLICATION_NUMBER"])
                    , strAppType = string.Empty
                    , strLAPushErrorMsg = string.Empty
                    , strParentId = string.Empty
                    , strProcessId = string.Empty;
                strIdentitiFlag = "UW";
                strUserId = string.IsNullOrEmpty(objLoginUserDetails._UserID) ? string.Empty : objLoginUserDetails._UserID;
                strBranch = string.IsNullOrEmpty(objLoginUserDetails._userBranch) ? string.Empty : objLoginUserDetails._userBranch;
                strUserRole = string.IsNullOrEmpty(objLoginUserDetails._UserRole) ? string.Empty : objLoginUserDetails._UserRole;
                UWSaralObjects.AfiCfiUW objAfiCfiUw = new UWSaralObjects.AfiCfiUW();
                int intLApushErrorCod = -1;
                DataSet _dsResponce = new DataSet();
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)

                {
                    objAfiCfiUw._PolicyNo = Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_NUMBER"]);
                    objAfiCfiUw._Branch = strBranch;
                    objAfiCfiUw._UserRole = strUserRole;
                    objAfiCfiUw._PartnerId = strParentId;
                    objAfiCfiUw._strProcessId = strProcessId;
                    objAfiCfiUw._ApplicationNo = strAppno;
                    objAfiCfiUw._UserId = strUserId;
                    objChangeValue.AfiCfiUw = objAfiCfiUw;
                    objAfiCfiUw._ReasonDesc = strComment;

                    //call service
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strPQuoteNo, strAppType, objChangeValue, ref _dsResponce, ref _dsPrevPol, strIdentitiFlag
                                                                            , ref intLApushErrorCod, ref strLAPushErrorMsg, ref strConsentRespons);
                    //validate the response
                    if (intLApushErrorCod == 0)
                    {
                        string strPolicyStatus = "PS";
                        string strPolicyNumber = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_NUMBER"]);
                        int intRetVal = -1;

                        //update policy status
                        UpdatePolicyStatus(strPQuoteNo, strPolicyStatus, ref intRetVal);

                        //maintain log
                        if (intRetVal > 0)
                        {
                            (new UWSaralDecision.CommFun()).LogClientProfile_Push(" applicaiton no " + strAppno + " " + Convert.ToString(_ds.Tables[0].Rows[i]["POLICY_STATUS"]), "Application No " + strAppno + " " + strPolicyStatus, (int)ClientAction.Uw, objLoginUserDetails._UserID
                                        , objLoginUserDetails._UserName, objLoginUserDetails._UserGroup, ref intRetVal);
                        }
                        //serialize and show data
                        UWSaralObjects.PolicyDetails objPolicyDetails = new UWSaralObjects.PolicyDetails();
                        objPolicyDetails.ApplicationNumber = strPQuoteNo;
                        objPolicyDetails.PolicyNumber = strPolicyNumber;
                        objPolicyDetails.FirstNameLgivName = Convert.ToString(_ds.Tables[0].Rows[0]["FIRST_NAME_LGIVNAME"]);
                        if (intRetVal > 0)
                        {
                            objPolicyDetails.PolicyStatus = strPolicyStatus;
                        }
                        else
                        {
                            objPolicyDetails.PolicyStatus = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_STATUS"]);
                        }
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + (new JavaScriptSerializer()).Serialize(objPolicyDetails);

                        //saving comment 
                        Commfun objComm = new Commfun();
                        int intCommitResult = -1;
                        objComm.OnlineUWCommentsDetails_Save(objCommonObj._bpm_userName, objCommonObj._ProcessName, objCommonObj._bpmgrp, strComment, objCommonObj._ApplicationNo, objCommonObj._bpm_userID, objCommonObj._bpm_userName, objCommonObj._bpmgrp, objCommonObj._bpm_branchCode, objCommonObj._bpm_userBranch, objCommonObj._ProcessName, strPQuoteNo, "General", ref intCommitResult);
                    }
                    else
                    {
                        strRet = Convert.ToString(intLApushErrorCod) + "#" + strLAPushErrorMsg;
                    }
                }
            }
            else
            {
                strRet = "1#No Records Found";
            }
        }
        catch (Exception ex)
        {
            strRet = "1#Try Again Later";
        }
        return strRet;
    }
    #endregion

    #region private method
    private void FetchAfiCfiDetais(ref DataSet _ds, string strApplicationNo, string strPolicyNo)
    {
        BussLayer objBussLayer = new BussLayer();
        objBussLayer.Afi_Cfi_GET(ref _ds, strApplicationNo, strPolicyNo);
    }

    private void UpdatePolicyStatus(string strApplicationNumber, string strPolicyStatus, ref int intRetVal)
    {
        BussLayer objBussLayer = new BussLayer();
        objBussLayer.UpdatePolicyStatus(strApplicationNumber, strPolicyStatus, ref intRetVal);
    }
    /*ID:1 START*/
    private void FetchActiveUsers(ref DataSet _ds)
    {
        objcomm.FetchUser(string.Empty, ref _ds);
    }

    public static string DateFormatfirstdate1(string objInput)
    {
        if (string.IsNullOrEmpty(objInput))
        {
            return "";
        }
        else
        {

            System.DateTime dt = Convert.ToDateTime(objInput);
            objInput = dt.ToString("dd/MM/yyyy");
            return dt.ToString("dd/MM/yyyy");

        }
    }

    private void BindActiveUsers(DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            ListItem objListItem = new ListItem("SELECT USER", "-1");
            ddlAssignTo.DataSource = _ds; 
            ddlAssignTo.DataTextField = "TEXT";
            ddlAssignTo.DataValueField = "VALUE";
            ddlAssignTo.DataBind();
            ddlAssignTo.Items.Insert(0, objListItem);
        }
        else
        {
            ddlAssignTo.DataSource = null;
            ddlAssignTo.DataBind();
        }

    }
    /*ID:1 END*/
    #endregion
}