using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Configuration;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;

/*
************************************************************************************************************************************
COMMENT ID:01
COMMENTOR NAME : Akshada N Wagh
METHODE/EVENT: ConsentLetter
REMARK:Email/SMS to be triggered if Consent Follow up codes are raised.
DateTime :18/05/2020
*********************************************************************************************************************************
*/
public partial class Appcode_Bpmuwmodule : System.Web.UI.MasterPage
{
    //This is set to UW default . letter this value come from query string.
    DataSet _ds = new DataSet();
    UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
    Commfun objComm = new Commfun();
    BussLayer objBussiness = new BussLayer();
    CommonObject objCommonObj = new CommonObject();
    ChangeValue objChangeObj = new ChangeValue();
    string struserid = string.Empty;
    string strUWmode = string.Empty;
    string strUserGroup = string.Empty;
    string strApplicationno = string.Empty;
    string strOptinselected = string.Empty;
    string strAppstatusKey = string.Empty;
    string strChannelType = string.Empty;
    string strPolicyNo = string.Empty;
    DataSet _dsPrevPol = new DataSet();
    int response = 1;
    //########################## Added by Shyam Starts ###########################################
    string NEWFollowUp = string.Empty;
    WCFGenerateInputOutput.WCFGenerateInputOutputClient clientService = new WCFGenerateInputOutput.WCFGenerateInputOutputClient();
    WCFHitMQ.WCFHitMQClient mqHit = new WCFHitMQ.WCFHitMQClient();
    string returnmsg = string.Empty;
    string msg = string.Empty;
    string Flag = string.Empty;
    string Status = string.Empty;
    //########################## Added by Shyam End ###########################################
    //########################## 1.1 BEGIN OF CHANGES CR 28153 ###########################################
    DataSet dsreqdetails = new DataSet();
    #region Consent Declarations

    ConsentEntity.ConsentParams objconsentparams = new ConsentEntity.ConsentParams();
    ConsentEntity.EmailParams objemailparams = new ConsentEntity.EmailParams();
    UWSaralDecision.CommFun objcommfun = new UWSaralDecision.CommFun();
    UWSaralServices.PremiumCalculationDetails objPremcal = new UWSaralServices.PremiumCalculationDetails();
    bool ConsentRaised = false;
    DataTable dtinsertclientdetials = new DataTable();
    DataTable dtemail = new DataTable();
    DataSet _dsClientInfo = new DataSet();
    DataTable dtclientinfo = new DataTable();
    DataSet _dsProdDtls = new DataSet();
    DataTable dtproductdetails = new DataTable();
    DataTable dt = new DataTable();
    DataTable dtclientdetails;
    DataSet _dsriderdetails = new DataSet();
    DataSet _dsPremcal = new DataSet();
    string BIResponse = string.Empty;
    DataRow dsemailrows;
    DataTable dtrequirements = new DataTable();
    string RequestId_number = string.Empty;
    string strLApushStatus = string.Empty;
    string strConsentRespons = string.Empty;
    int RequestID = new int();
    string Consent_Raised = string.Empty;
    #endregion

    //########################## 1.1 END OF CHANGES CR 28153 ###########################################

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["qsUserGroup"] != null)
        {
            strUserGroup = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsUserGroup"]);
        }
        if (Request.QueryString["qsAppNo"] != null)
        {
            //strApplicationno = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
            strApplicationno = Request.QueryString["qsAppNo"];
        }
        if (Request.QueryString["qsChannelName"] != null)
        {
            strChannelType = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsChannelName"]);
        }
        if (Request.QueryString["qsPolicyNo"] != null)
        {
            strPolicyNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsPolicyNo"]);
        }
        //########################## Added by Shyam Starts ###########################################
        try
        {
            Session["RequestType"] = "";
            Logger.Info(strApplicationno + Request.Url.Segments[3].ToString() + " " + "Check Page" + System.Environment.NewLine);
            if (Request.Url.Segments[3].ToString() == "RevivalUwdecision.aspx")
            {
                Session["RequestType"] = "Revival";
                Logger.Info(strApplicationno + Request.Url.Segments[3].ToString() + System.Environment.NewLine);
            }
        }
        catch (Exception)
        {
        }

        //########################## Added by Shyam End ###########################################

        if (!IsPostBack)
        {
            // objCommonObj = (CommonObject)Session["objCommonObj"];
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :Page_Load" + System.Environment.NewLine);
            lblCaptionAppNo.Text = "Application Number: " + strApplicationno;
            lblCaptionPolNo.Text = "Policy Number: " + strPolicyNo;
            //BindMenu(objChangeObj.userLoginDetails._UserGroup);

        }
    }


    public void BindMenu(string strGroupName)
    {
        //if (strGroupName == "UW")
        //{
        //    //btnsendToUW.Attributes.Add("class", "HideControl");
        //}
        //if (strGroupName == "DOCQC")
        //{
        //    btnReferToCmo.Attributes.Add("class", "HideControl");
        //}
    }

    public static object DateFormat(object objInput)
    {
        if (object.ReferenceEquals(objInput, DBNull.Value))
        {
            return null;
        }
        else
        {
            if (string.IsNullOrEmpty(Convert.ToString(objInput)))
            {
                return null;
            }
            else
            {
                //DateTime dt = DateTime.ParseExact(Convert.ToString(objInput), "mm-dd-yyyy", CultureInfo.InvariantCulture);
                System.DateTime dt = Convert.ToDateTime(objInput);
                objInput = dt.ToString("dd/MM/yyyy");
                return objInput;
            }
        }
    }
    public event EventHandler contentCallEvent;
    public event EventHandler masterCallEvent;
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet _dscombiflag = new DataSet();
            Repeater rptDecision = (Repeater)ContentPlaceHolder1.FindControl("rptDecision");
            bool IsCombi = false;
            IsCombi = (bool)Session["IsCombi"];
            //objComm.GetCombiFlag(ref _dscombiflag, strApplicationno);
            //if (_dscombiflag != null && _dscombiflag.Tables.Count > 0 && _dscombiflag.Tables[0].Rows.Count > 0)
            if (IsCombi)
            {
                foreach (RepeaterItem item in rptDecision.Items)
                {
                    Label lblAppno = (Label)item.FindControl("lblAppno");
                    Session["AppNo_Combi"] = lblAppno.Text;
                    Label lblPolNum = (Label)item.FindControl("lblPolNum");
                    Session["PolNo_Combi"] = lblPolNum.Text;
                    strApplicationno = lblAppno.Text;
                    Logger.Info(strApplicationno + "master post Event Start_Step1" + System.Environment.NewLine);
                    lblPreIssueVal.Text = string.Empty;
                    MasterPageComparision objNewMasterComparision1 = new MasterPageComparision();
                    MasterPageComparision objNewMasterComparision2 = new MasterPageComparision();
                    ContentClientDetails(objNewMasterComparision1);
                    ContentRiderDetails(objNewMasterComparision1);
                    ContentProductDetails_Combi(objNewMasterComparision1, lblPolNum.Text);

                    System.Web.Script.Serialization.JavaScriptSerializer objJavaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    HiddenField hdnOldValue = (HiddenField)ContentPlaceHolder1.FindControl("hdnOldValue");
                    objNewMasterComparision2 = objJavaScriptSerializer.Deserialize<MasterPageComparision>(hdnOldValue.Value);
                    int intRef = -1;
                    string strUserId = string.Empty;
                    Logger.Info(strApplicationno + "master post Event Start_Step2" + System.Environment.NewLine);
                    if (Session["objCommonObj"] != null)
                    {
                        objCommonObj = (CommonObject)Session["objCommonObj"];
                        strUserId = objCommonObj._Bpmuserdetails._UserID;
                        Logger.Info(strApplicationno + "master post Event Start_Step3" + System.Environment.NewLine);
                        //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_POST", false, ref intRef);
                    }
                    /*added by shri on 28 dec 17 to add tracking*/
                    int intTrackingRet = -1;
                    objCommonObj = (CommonObject)Session["objCommonObj"];
                    strUserId = objCommonObj._Bpmuserdetails._UserID;
                    InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "POST", ref intTrackingRet);
                    Logger.Info(strApplicationno + "master post Event Start_Step4" + System.Environment.NewLine);
                    /*end here*/
                    /*END HERE*/
                    if (masterCallEvent != null)
                    {
                        masterCallEvent(this, EventArgs.Empty);
                    }
                    int LAPushErrorCode = 0;
                    int UWDecisionResult = 0;
                    string strLAPushErrorMsg = string.Empty;
                    int Result = 0;
                    objCommonObj = (CommonObject)Session["objCommonObj"];
                    // objCommonObj = new CommonObject();
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                    struserid = objChangeObj.userLoginDetails._UserID;
                    // strUserGroup = objChangeObj.userLoginDetails._UserGroup;
                    //strApplicationno = objCommonObj._ApplicationNo;
                    strUWmode = strChannelType;
                    Logger.Info(strApplicationno + "master post Event Start_Step9" + System.Environment.NewLine);
                    UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
                    Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnPost_Click" + System.Environment.NewLine);
                    DropDownList ddlUWDecesion = (DropDownList)item.FindControl("ddlUWDecesion");
                    DropDownList ddlUWreason = (DropDownList)item.FindControl("ddlUWreason");
                    DropDownList ddlPostpone = (DropDownList)item.FindControl("ddlPeriod");
                    Label lblErrorDecisiondtls = (Label)item.FindControl("lblErrorDecisiondtls");
                    string _strUWDecesion = ddlUWDecesion.SelectedValue;
                    string _strUWPeriod = ddlPostpone.SelectedValue == "0" ? "" : ddlPostpone.SelectedValue;
                    string _strDataValue = string.Empty;
                    //string _strPolicyStatus = string.Empty;
                    int intRetVal = -1;

                    /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                    Logger.Info(strApplicationno + "master post Event Start_Step10" + System.Environment.NewLine);
                    UpdateDecisionInLa_Combi(ref response, ddlUWDecesion, ddlUWreason, ddlPostpone, lblErrorDecisiondtls);
                    Logger.Info(strApplicationno + "master post Event Start_Step16" + System.Environment.NewLine);
                    //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_POST", true, ref intRef);
                    /*END HERE*/
                    /*added by shri on 28 dec 17 to update tracking*/
                    UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
                    /*END HERE*/
                    Logger.Info(strApplicationno + "master post Event End" + System.Environment.NewLine);
                }
            }
            else
            {
                Logger.Info(strApplicationno + "master post Event Start_Step1" + System.Environment.NewLine);
                lblPreIssueVal.Text = string.Empty;
                MasterPageComparision objNewMasterComparision1 = new MasterPageComparision();
                MasterPageComparision objNewMasterComparision2 = new MasterPageComparision();
                ContentClientDetails(objNewMasterComparision1);
                ContentRiderDetails(objNewMasterComparision1);
                ContentProductDetails(objNewMasterComparision1);

                System.Web.Script.Serialization.JavaScriptSerializer objJavaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                HiddenField hdnOldValue = (HiddenField)ContentPlaceHolder1.FindControl("hdnOldValue");
                objNewMasterComparision2 = objJavaScriptSerializer.Deserialize<MasterPageComparision>(hdnOldValue.Value);
                int intRef = -1;
                string strUserId = string.Empty;
                Logger.Info(strApplicationno + "master post Event Start_Step2" + System.Environment.NewLine);
                if (Session["objCommonObj"] != null)
                {
                    objCommonObj = (CommonObject)Session["objCommonObj"];
                    strUserId = objCommonObj._Bpmuserdetails._UserID;
                    Logger.Info(strApplicationno + "master post Event Start_Step3" + System.Environment.NewLine);
                    //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_POST", false, ref intRef);
                }
                /*added by shri on 28 dec 17 to add tracking*/
                int intTrackingRet = -1;
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "POST", ref intTrackingRet);
                Logger.Info(strApplicationno + "master post Event Start_Step4" + System.Environment.NewLine);
                /*end here*/
                /*END HERE*/
                if (masterCallEvent != null)
                {
                    masterCallEvent(this, EventArgs.Empty);
                }
                int LAPushErrorCode = 0;
                int UWDecisionResult = 0;
                string strLAPushErrorMsg = string.Empty;
                int Result = 0;
                objCommonObj = (CommonObject)Session["objCommonObj"];
                // objCommonObj = new CommonObject();
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                struserid = objChangeObj.userLoginDetails._UserID;
                // strUserGroup = objChangeObj.userLoginDetails._UserGroup;
                //strApplicationno = objCommonObj._ApplicationNo;
                strUWmode = strChannelType;
                Logger.Info(strApplicationno + "master post Event Start_Step9" + System.Environment.NewLine);
                UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
                Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnPost_Click" + System.Environment.NewLine);
                DropDownList ddlUWDecesion = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWDecesion");
                DropDownList ddlUWreason = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWreason");
                DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPeriod");
                Label lblErrorDecisiondtls = (Label)ContentPlaceHolder1.FindControl("lblErrorDecisiondtls");
                string _strUWDecesion = ddlUWDecesion.SelectedValue;
                string _strUWPeriod = ddlPostpone.SelectedValue == "0" ? "" : ddlPostpone.SelectedValue;
                string _strDataValue = string.Empty;
                //string _strPolicyStatus = string.Empty;
                int intRetVal = -1;

                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                Logger.Info(strApplicationno + "master post Event Start_Step10" + System.Environment.NewLine);
                UpdateDecisionInLa(ref response);
                Logger.Info(strApplicationno + "master post Event Start_Step16" + System.Environment.NewLine);
                //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_POST", true, ref intRef);
                /*END HERE*/
                /*added by shri on 28 dec 17 to update tracking*/
                UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
                /*END HERE*/
                Logger.Info(strApplicationno + "master post Event End" + System.Environment.NewLine);
            }
        }
        catch (Exception Error)
        {
            Logger.Info(strApplicationno + "Exception In Post" + Error.Message + System.Environment.NewLine);
            if (Error.Message.Contains("UDE-"))
            {
                ShowPopupMessage(Error.Message.Replace("UDE-", string.Empty), 0);
            }
            else
            {
                ShowPopupMessage("Details Not Post", 0);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Logger.Info(strApplicationno + "PAGE_NAME:UWSaralDecision/btnSave_Click //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution Begin" + System.Environment.NewLine);
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            int intRef = -1;
            string strUserId = string.Empty;
            if (Session["objCommonObj"] != null)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
            }
            //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_SAVE", false, ref intRef);
            /*END HERE*/
            Logger.Info("STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnSave_Click" + System.Environment.NewLine);
            if (contentCallEvent != null)
                contentCallEvent(this, EventArgs.Empty);
            //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_SAVE", true, ref intRef);
            Logger.Info(strApplicationno + "PAGE_NAME:UWSaralDecision/btnSave_Click //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution End" + System.Environment.NewLine);
        }
        catch (Exception Error)
        {
            Logger.Info(strApplicationno + "Exception In Save" + Error.Message + System.Environment.NewLine);
            ShowPopupMessage("Erroe While Saving Application Details,Please Contact System Admin", 0);
        }
    }

    public void UpdateUWControlTable(string strProcessStatus, ref int strResult)
    {
        //Change the status of DOCQC to Closed.
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :UpdateUWControlTable" + System.Environment.NewLine);
        int _SaveResult = 0;
        objComm.ChangeUWSaralStatus(strApplicationno, strPolicyNo, strProcessStatus, "CLOSE", true, true, DateTime.Now, DateTime.Now, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, Convert.ToDateTime(objCommonObj._bpm_creationDate), Convert.ToDateTime(objCommonObj._bpm_systemDate), objCommonObj._bpm_systemDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, objCommonObj._bpm_applicationName, ref _SaveResult);
    }

    public void InsertUWControlTable(string strProcessStatus, ref int strResult)
    {
        //Change the status of DOCQC to Closed.
        //objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :InsertUWControlTable" + System.Environment.NewLine);
        int _SaveResult = 0;
        objComm.ChangeUWSaralStatus(strApplicationno, strPolicyNo, strProcessStatus, "WIP", true, false, DateTime.Now, DateTime.Now, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, Convert.ToDateTime(objCommonObj._bpm_creationDate), Convert.ToDateTime(objCommonObj._bpm_systemDate), objCommonObj._bpm_systemDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, objCommonObj._bpm_applicationName, ref _SaveResult);
    }

    protected void btnsendToUW_Click(object sender, EventArgs e)
    {
        int _SaveResult = 0;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        strUserGroup = objChangeObj.userLoginDetails._UserGroup;
        struserid = objChangeObj.userLoginDetails._UserID;
        strUWmode = strChannelType;
        //UpdateUWControlTable("DOCQC", ref _SaveResult);
        //InsertUWControlTable("UW", ref _SaveResult);
        //objComm.UpdateControlMechanism(objCommonObj._ApplicationNo, struserid, "1119763", "UWAssign", ref _SaveResult);
        strAppstatusKey = (strUserGroup == "UW") ? "UR" : "DR";
        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref _SaveResult);
        Logger.Info("STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnSave_Click" + System.Environment.NewLine);
        if (_SaveResult != -1)
        {
            Response.Redirect("~/9011042143.aspx");
        }
        else
        {
            Logger.Error("STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnSave_Click : Error : UW Saral Status noot change" + System.Environment.NewLine);
        }
    }

    public delegate void DoEvent();
    protected void btnManual_Click(object sender, EventArgs e)
    {
        UpdateDecisionInLa(ref response);
    }

    private void UpdateDecisionInLa_Combi(ref int response,DropDownList ddlUWDecesion,DropDownList ddlUWreason,DropDownList ddlPostpone,Label lblErrorDecisiondtls)
    {
        Logger.Info(strApplicationno + "master post Event Start_Step17" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Begins" + System.Environment.NewLine);
        //declare variable
        int LAPushErrorCode = 0;
        int UWDecisionResult = 0;
        string strLAPushErrorMsg = string.Empty;
        int Result = 0;
        Boolean isConsentReq = false;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        struserid = objChangeObj.userLoginDetails._UserID;
        strUWmode = strChannelType;
        UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
        Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnPost_Click" + System.Environment.NewLine);
        //DropDownList ddlUWDecesion = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWDecesion");
       // DropDownList ddlUWreason = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWreason");
        //DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPStart_Step18eriod");
        //DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPeriod");
        //Label lblErrorDecisiondtls = (Label)ContentPlaceHolder1.FindControl("lblErrorDecisiondtls");

        //########################## Added by Shyam Starts ###########################################
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_01chnages starts" + System.Environment.NewLine);
        DropDownList ddlStatusREQ = (DropDownList)ContentPlaceHolder1.FindControl("ddlStatus");
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_01chnages ends" + System.Environment.NewLine);
        //########################## Added by Shyam End ###########################################

        if (objChangeObj.Load_Loadingdetails != null)
        {
            isConsentReq = objChangeObj.Load_Loadingdetails.isConsentRequired;
        }
        else
        {
            isConsentReq = false;
        }
        string _strUWDecesion = ddlUWDecesion.SelectedValue;
        string _strUWPeriod = ddlPostpone.SelectedValue == "0" ? "" : ddlPostpone.SelectedValue;
        string _strDataValue = string.Empty;
        int intRetVal = -1;

        //########################## Added by Shyam Starts ###########################################
        //Changes start 04042020
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_02chnages starts" + System.Environment.NewLine);
        Session["RequestType"] = "";
        if (Request.Url.Segments.Length > 3 && Request.Url.Segments[3].ToString() == "RevivalUwdecision.aspx")
        {
            Session["RequestType"] = "Revival";
        }
        if (Session["RequestType"].ToString() == "Revival")
            Logger.Info(Session["RequestType"].ToString() + "Revival start" + System.Environment.NewLine);
        {
            objCommonObj._AppType = "Single";
        }
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_02chnages ends" + objCommonObj._AppType + System.Environment.NewLine);
        //Changes start 04042020
        //########################## Added by Shyam End ###########################################

        Logger.Info(strApplicationno + "master post Event Start_Step18" + System.Environment.NewLine);
        //call servics

        objComm.OnlineUWDecisionDetails_Save(objCommonObj._AppType, strApplicationno, ddlUWDecesion.SelectedItem.Text, ddlUWreason.SelectedItem.Text,
            ddlUWDecesion.SelectedValue, ddlUWreason.SelectedItem.Value, _strUWPeriod, struserid, objChangeObj.userLoginDetails._UserName,
        objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, objCommonObj._bpm_creationDate, objCommonObj._bpm_systemDate,
        objCommonObj._bpm_businessDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName,
      strApplicationno, ref UWDecisionResult);
        //Changes start 04042020

        Logger.Info(strApplicationno + "master post Event Start_Step19" + System.Environment.NewLine);
        StringBuilder sb = new StringBuilder();

        sb.Append(objCommonObj._AppType + "," + strApplicationno + "," + ddlUWDecesion.SelectedItem.Text + "," +
        ddlUWreason.SelectedItem.Text + "," + ddlUWDecesion.SelectedValue + "," + ddlUWreason.SelectedItem.Value + "," + _strUWPeriod
        + "," + struserid + "," + objChangeObj.userLoginDetails._UserName + "," + objChangeObj.userLoginDetails._UserGroup
        + "," + objCommonObj._bpm_branchCode + "," + objCommonObj._bpm_creationDate + "," + objCommonObj._bpm_systemDate + ","
        + objCommonObj._bpm_businessDate + "," + objChangeObj.userLoginDetails._userBranch + "," + objChangeObj.userLoginDetails._ProcessName
        + "," + strApplicationno + "," + UWDecisionResult);

        string result = sb.ToString();

        Logger.Info(result + "master post Event Start_input" + System.Environment.NewLine);

        //Changes start 04042020
        if (UWDecisionResult != -1)
        {
            Logger.Info(strApplicationno + "master post Event Start_Step20" + System.Environment.NewLine);
            string strConsentResponse = string.Empty;
            //lblErrorDecisiondtls.Text = "Decision details save successfully";



            //########################## Added by Shyam Starts ###########################################
            //Changes start 04042020
            Session["RequestType"] = "";
            Logger.Info(Session["RequestType"].ToString() + "check revival" + System.Environment.NewLine);
            if (Request.Url.Segments.Length > 3 && Request.Url.Segments[3].ToString() == "RevivalUwdecision.aspx")
            {
                Logger.Info(Session["RequestType"].ToString() + "check revival_1" + System.Environment.NewLine);
                Session["RequestType"] = "Revival";
                Logger.Info(Session["RequestType"].ToString() + "check revival_2" + System.Environment.NewLine);
            }
            //Changes end 04042020
            if (Session["RequestType"].ToString() == "Revival")
            {
                Logger.Info(Session["RequestType"].ToString() + "check revival_3" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "master post Event Start_Step21" + System.Environment.NewLine);
                HiddenField HdnPremPayingStatus1 = (HiddenField)ContentPlaceHolder1.FindControl("hdnPremPayingStatus");
                HiddenField HdnPolicyStatus1 = (HiddenField)ContentPlaceHolder1.FindControl("hdnPolicyStatus");
                if (Session["hdnNewFollowup"] != null)
                {
                    NEWFollowUp = Session["hdnNewFollowup"].ToString();
                    NEWFollowUp.ToString();

                }

                if (HdnPremPayingStatus1.Value.Trim() == "Lapsed" ||
                        HdnPremPayingStatus1.Value.Trim() == "HA" || HdnPremPayingStatus1.Value.Trim() == "AC"
                        || HdnPremPayingStatus1.Value.Trim() == "PH" || HdnPremPayingStatus1.Value.Trim() == "UD" || HdnPolicyStatus1.Value.Trim() == "LA" ||
                        HdnPolicyStatus1.Value.Trim() == "PU" || HdnPolicyStatus1.Value.Trim() == "DU" || HdnPolicyStatus1.Value.Trim() == "HP")
                {
                    Logger.Info(strApplicationno + "master post Event Start_04042020_v1" + System.Environment.NewLine);
                    LAPushErrorCode = 0;
                }
                else
                {
                    objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
                }
            }
            else
            {
                objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
            }
            Logger.Info(strApplicationno + "master post Event Start_Step22" + System.Environment.NewLine);
            //########################## Added by Shyam End ###########################################



            if (!string.IsNullOrEmpty(strConsentResponse) && !strConsentResponse.Equals("Failed"))
            {
                Logger.Info("Consent Letter for application No : {0} : {1}" + strApplicationno + strConsentResponse);
                //string filePath = Request.QueryString["FILEPATH"].ToString();
                //Response.ContentType = "application/pdf";
                //Response.WriteFile(strConsentResponse);
                Response.Write("<script>");
                Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(strConsentResponse)));
                Response.Write("</script>");
                //WebClient myWebClient = new WebClient();
                // myWebClient.DownloadFile(strConsentResponse, "D:\abc.pdf");
                //Response.Redirect(strConsentResponse);
            }
            if (LAPushErrorCode == 0)
            {

                //########################## Added by Shyam Starts ###########################################
                Session["RequestType"] = "";
                if (Request.Url.Segments.Length > 3 && Request.Url.Segments[3].ToString() == "RevivalUwdecision.aspx")
                {
                    Logger.Info(strApplicationno + "master post Event Start_postponerevival" + System.Environment.NewLine);
                    // Changes start 04042020
                    Session["RequestType"] = "Revival";
                }
                //Changes start 04042020
                if (Session["RequestType"].ToString() == "Revival")
                {
                    HiddenField HdnPolicyStatus = (HiddenField)ContentPlaceHolder1.FindControl("hdnPolicyStatus");
                    HiddenField HdnPremPayingStatus = (HiddenField)ContentPlaceHolder1.FindControl("hdnPremPayingStatus");
                    HiddenField HdnPolicyNumber = (HiddenField)ContentPlaceHolder1.FindControl("hdnPolNo");
                    TextBox txtProposerName = (TextBox)ContentPlaceHolder1.FindControl("LAName");
                    //txtTotalPrmPay
                    TextBox txtPremAmt = (TextBox)ContentPlaceHolder1.FindControl("txtTotalPrmPay");
                    TextBox txtPlanName = (TextBox)ContentPlaceHolder1.FindControl("txtProname1");
                    HiddenField hdnMobile = (HiddenField)ContentPlaceHolder1.FindControl("hdnModilenumber");
                    HiddenField hdnEmail = (HiddenField)ContentPlaceHolder1.FindControl("hdnEmail");
                    HiddenField HdnAppNumber = (HiddenField)ContentPlaceHolder1.FindControl("HdnApplicationNumber");
                    DropDownList ddlStatusREQ1 = (DropDownList)ContentPlaceHolder1.FindControl("ddlStatus");
                    //HiddenField hdnbankname = (HiddenField)ContentPlaceHolder1.FindControl("HfnBankName");
                    //HiddenField hdnbankaccount = (HiddenField)ContentPlaceHolder1.FindControl("HfnAccountNo");
                    //Changes start 04042020
                    HiddenField hdnbankname = (HiddenField)ContentPlaceHolder1.FindControl("hdfnBankName");
                    HiddenField hdnbankaccount = (HiddenField)ContentPlaceHolder1.FindControl("hdfnBankAcctNumber");
                    //Changes end  04042020
                    //1.1 Start of Changes for Revival 18 / 04 / 2020
                    HiddenField hdnLAFullName = (HiddenField)ContentPlaceHolder1.FindControl("hdnLAFullname");
                    //1.1 End of Changes for Revival 18 / 04 / 2020

                    if (HdnPremPayingStatus.Value.Trim() == "Lapsed" ||
                        HdnPremPayingStatus.Value.Trim() == "HA" || HdnPremPayingStatus.Value.Trim() == "AC"
                        || HdnPremPayingStatus.Value.Trim() == "PH" || HdnPremPayingStatus.Value.Trim() == "UD" || HdnPolicyStatus.Value.Trim() == "LA" ||
                        HdnPolicyStatus.Value.Trim() == "PU" || HdnPolicyStatus.Value.Trim() == "DU" || HdnPolicyStatus.Value.Trim() == "HP")
                    {
                        //Communication for new requirement raised.

                        if (ddlUWDecesion.SelectedValue == "Declined")
                        {
                            string ReqStatus = string.Empty;
                            GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");
                            int counter = 0;
                            foreach (GridViewRow rowfollowup in GvReq.Rows)
                            {
                                DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;
                                if (ddlStatus.SelectedValue == "O")
                                {
                                    counter = counter + 1;
                                }
                            }


                            if (counter > 0 && NEWFollowUp == "TRUE")
                            {
                                DataTable dtSMS = new DataTable();
                                string Flag1 = "02";
                                string CommunicationType = "SMS";
                                string CommunicationKey = "SMS";

                                string Process = "Under writing deision saral";
                                string TemplateId = "4";
                                //Template Id 2 is for requirement raised should ne used where decision is pending
                                string MailTo = hdnEmail.Value.Trim();
                                string MailCC = string.Empty;
                                string MobileNo = hdnMobile.Value.Trim();
                                string Mode = "UW";
                                string CreatedBy = "System";
                                string IsAttached = "0";
                                string AttachedFiles = null;
                                string ApplicationNo = HdnAppNumber.Value.Trim();
                                string PolicyNumber = HdnPolicyNumber.Value.Trim();
                                string ParameterList = "";
                                string FileName = "";
                                string IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];

                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string parameter = dtSMS.Rows[i][1].ToString();

                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<proposer name>":
                                            //1.2 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'),";
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                            //1.2 End of Changes for Revival 18 / 04 / 2020
                                            break;

                                        case "<Plan Name>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<Policy No.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'";
                                            break;



                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                Session["hdnNewFollowup"] = null;

                                //RejectedForImage//
                                try
                                {
                                    Logger.Info(strApplicationno + "RejectedForImage_email_start" + System.Environment.NewLine);
                                    DataTable dtemail = new DataTable();
                                    //1.3 Start of Changes for Revival 18 / 04 / 2020
                                    //string CustomerName = txtProposerName.Text.Trim();                    
                                    string CustomerName = hdnLAFullName.Value;
                                    //1.3 End of Changes for Revival 18 / 04 / 2020
                                    string PolicyName = txtPlanName.Text.Trim();
                                    /*PolicyNumber*/
                                    string Reason1 = ddlUWDecesion.SelectedValue.ToString();
                                    string Reason2 = ddlUWreason.SelectedValue.ToString();
                                    string BankName = string.Empty;
                                    string BankAccountNumber = string.Empty;



                                    Flag1 = "02";
                                    CommunicationType = "EMAIL";
                                    CommunicationKey = "EMAIL";
                                    //Changes start 04042020
                                    Process = "We Are Unable To Revive Your Policy";
                                    //Changes end 04042020
                                    TemplateId = "27";
                                    MailTo = hdnEmail.Value.Trim();
                                    MailCC = string.Empty;
                                    MobileNo = hdnMobile.Value.Trim();
                                    Mode = "UW";
                                    CreatedBy = "System";
                                    IsAttached = "0";
                                    AttachedFiles = null;
                                    ApplicationNo = HdnAppNumber.Value.Trim();
                                    PolicyNumber = HdnPolicyNumber.Value.Trim();
                                    ParameterList = "";
                                    FileName = "Rejected_For_Image.html";
                                    IsExternal = "YES";


                                    dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                    DataRow dsemailrows = dtSMS.Rows[0];

                                    string RequestId_no = dsemailrows["RequestId"].ToString();
                                    Flag1 = "01";
                                    dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                    for (int i = 0; i < dtemail.Rows.Count; i++)
                                    {
                                        string parameter = dtemail.Rows[i][1].ToString();

                                        string col = dtemail.Rows[i][1].ToString();
                                        switch (parameter)
                                        {

                                            case "<Customer_Name>":
                                                ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                                break;

                                            case "<Policy_Name>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                                break;

                                            case "<Policy_Number>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                                break;

                                            case "<Reason1>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason1 + "'),";
                                                break;

                                            case "<Reason2>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason2 + "'),";
                                                break;

                                            case "<Bank_Name>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + BankName + "'),";
                                                break;

                                            case "<Bank_Account_Number>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + BankAccountNumber + "'";
                                                break;

                                            default:
                                                break;
                                        }
                                    }
                                    Flag1 = "03";
                                    dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                    Logger.Info(strApplicationno + "RejectedForImage_email_success" + System.Environment.NewLine);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Info(strApplicationno + "RejectedForImage_email_fail" + System.Environment.NewLine);
                                    throw ex;
                                }

                            }
                        }

                        else if (ddlUWDecesion.SelectedValue == "Postponed")
                        {
                            Logger.Info(strApplicationno + "master post Event Start_Step22_postponedstarts" + System.Environment.NewLine);
                            HiddenField HdnMonthCounts = (HiddenField)ContentPlaceHolder1.FindControl("HdnFinalMonthCount");

                            if (Convert.ToInt64(HdnMonthCounts.Value) > 0)
                            {
                                Logger.Info("poststart" + "master post Event Start_postponedstarts_s" + System.Environment.NewLine);
                                int Months = Convert.ToInt32(HdnMonthCounts.Value) - 1;
                                int postponeMonths = Convert.ToInt32(ddlPostpone.SelectedValue);
                                int ActuallMonths = Convert.ToInt32(HdnMonthCounts.Value);
                                StringBuilder sb1 = new StringBuilder();
                                sb1.Append(Months + "," + postponeMonths + "," + ActuallMonths);
                                string inputmsg = sb1.ToString();
                                Logger.Info(inputmsg + "master post Event Start_postponedstarts_1" + System.Environment.NewLine);
                                //ShowPopupMessage("" + HdnMonthCounts.Value + " Months remaining for revival period", 0);
                                if (postponeMonths > Months)
                                {
                                    ShowPopupMessage("The revival can be allowed for this case up to " + ActuallMonths + " period only.Hence you can postpone this policy up to " + Months + " months maximum.Please change postpone period or revise the UW decision accordingly", 0);
                                    return;
                                }
                                //else if (postponeMonths > Months)
                                //{
                                //    ShowPopupMessage("The revival can be allowed for this case up to " + Months + " period only.Hence you can postpone this policy up to " + Months + " months maximum.Please change postpone period or revise the UW decision accordingly", 0);
                                //    return;
                                //}
                                //else
                                //{
                                //    ShowPopupMessage("The revival can be allowed for this case up to " + Months + " period only.Hence you can postpone this policy up to " + Months + " months maximum.Please change postpone period or revise the UW decision accordingly", 0);
                                //}


                            }
                            else
                            {
                                ShowPopupMessage("You cannot Postpone this case upto provided " + ddlPostpone.SelectedValue + ",since revival after " + ddlPostpone.SelectedValue + " not allowed. Please review UW decision accordingly", 0);
                                return;
                            }

                            Logger.Info(strApplicationno + "master post Event Start_Step23" + System.Environment.NewLine);

                            string ReqStatus = string.Empty;
                            GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");

                            int counter = 0;

                            foreach (GridViewRow rowfollowup in GvReq.Rows)
                            {

                                DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;

                                if (ddlStatus.SelectedValue == "O")
                                {
                                    counter = counter + 1;
                                }
                            }
                            Logger.Info(strApplicationno + "master post Event Start_Step24" + System.Environment.NewLine);

                            if (counter > 0 && NEWFollowUp == "TRUE")
                            {
                                Logger.Info(strApplicationno + "master post Event Start_Step25" + System.Environment.NewLine);
                                DataTable dtSMS = new DataTable();
                                string Flag1 = "02";
                                string CommunicationType = "SMS";
                                string CommunicationKey = "SMS";
                                string Process = "UW Requirement raised";
                                string TemplateId = "2";
                                string MailTo = hdnEmail.Value.Trim();
                                string MailCC = string.Empty;
                                string MobileNo = hdnMobile.Value.Trim();
                                string Mode = "UW";
                                string CreatedBy = "System";
                                string IsAttached = "0";
                                string AttachedFiles = null;
                                string ApplicationNo = HdnAppNumber.Value.Trim();
                                string PolicyNumber = HdnPolicyNumber.Value.Trim();
                                string ParameterList = "";
                                string FileName = "";
                                string IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];
                                Logger.Info(strApplicationno + "master post Event Start_Step26" + System.Environment.NewLine);
                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "master post Event Start_Step27" + System.Environment.NewLine);
                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string parameter = dtSMS.Rows[i][1].ToString();

                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<proposer name>":

                                            //1.5 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'),";
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                            //1.5 Start of Changes for Revival 18 / 04 / 2020
                                            break;

                                        case "<Plan Name>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<Policy No.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'";
                                            break;

                                        //case "<Premium Amount>":
                                        //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                        //	break;

                                        default:
                                            break;
                                    }
                                }
                                Logger.Info(strApplicationno + "master post Event Start_Step28" + System.Environment.NewLine);
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Session["hdnNewFollowup"] = null;
                                Logger.Info(strApplicationno + "master post Event Start_Step29" + System.Environment.NewLine);
                            }
                        }

                        if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
                        {
                            //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);



                            //DataSet dsREQ = new DataSet();
                            //objBussiness.CheckOpenRequirement(HdnAppNumber.Value.Trim(), "1",ref dsREQ);
                            //if(dsREQ.Tables[0].Rows.Count == 0 )
                            //{


                            //}
                            //else
                            //{

                            //	ShowPopupMessage("Please close all Requirements", 0);
                            //}

                            //Commented Start by kunal 26-03-2020  Reason - Details to be updated only if status changes in LA
                            //string ReqStatus = string.Empty;
                            //GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");


                            //foreach (GridViewRow rowfollowup in GvReq.Rows)
                            //{
                            //	DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;

                            //	if (ddlStatus.SelectedValue == "O")
                            //	{
                            //		ShowPopupMessage("Please close all Requirements", 0);
                            //		return;
                            //	}
                            //	else
                            //	{
                            //		//objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);

                            //		//Akshay
                            //		UWSaralServices.FollowupDetails objFollowup = new UWSaralServices.FollowupDetails();
                            //		//CommFun objComm = new CommFun();
                            //		DataSet _dsFollowUp = null;

                            //		objComm.OnlineServiceFollowUPDetails_GET(ref _dsFollowUp, HdnAppNumber.Value.Trim(), "Offline");

                            //		objFollowup.FollowuPushService(HdnAppNumber.Value.Trim(), _dsFollowUp, objChangeObj, ref LAPushErrorCode, ref strLAPushErrorMsg);
                            //	}
                            //}

                            //strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            ////_strPolicyStatus = "IF";
                            //objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);

                            ////To Insert Record in LF_applicationstatus table with '091' status 
                            //objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);
                            //Commented End by kunal 26-03-2020  Reason - Details to be updated only if status changes in LA

                            //UWSARAL STATUS UPDATE.
                            string STPResult = string.Empty;

                            REINSTSTPService.Service1Client objREINSTSTP = new REINSTSTPService.Service1Client();
                            string UserID = struserid.ToString().StartsWith("F") ? struserid.ToString() : "F" + struserid.ToString();
                            STPResult = objREINSTSTP.IsSTPRulesPassORFail(HdnPolicyNumber.Value.Trim(), "BOR", "BOR", "UW", "10", "2", UserID);
                            //objCommonObj._bpm_branchCode

                            if (STPResult == "0")
                            {
                                //Commented code pasted Start by kunal 26-03-2020  Reason - Details to be updated only if status changes in LA
                                string ReqStatus = string.Empty;
                                GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");
                                foreach (GridViewRow rowfollowup in GvReq.Rows)
                                {
                                    DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;

                                    if (ddlStatus.SelectedValue == "O")
                                    {
                                        ShowPopupMessage("Please close all Requirements", 0);
                                        return;
                                    }
                                    else
                                    {

                                        UWSaralServices.FollowupDetails objFollowup = new UWSaralServices.FollowupDetails();

                                        DataSet _dsFollowUp = null;

                                        objComm.OnlineServiceFollowUPDetails_GET(ref _dsFollowUp, HdnAppNumber.Value.Trim(), "Offline");

                                        objFollowup.FollowuPushService(HdnAppNumber.Value.Trim(), _dsFollowUp, objChangeObj, ref LAPushErrorCode, ref strLAPushErrorMsg);
                                    }
                                }
                                strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                                objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                                objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);


                                objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);




                                Flag = "1";
                                Status = "UW Approved";
                                objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);

                                objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");

                                ShowPopupMessage("Details Post Successfully", 0);
                                STPResult = "";

                                //UW ACCEPTS
                                DataTable dtSMS = new DataTable();
                                string Flag1 = "02";
                                string CommunicationType = "SMS";
                                string CommunicationKey = "SMS";
                                string Process = "Under writing deision saral";
                                string TemplateId = "3";
                                string MailTo = hdnEmail.Value.Trim();
                                string MailCC = string.Empty;
                                string MobileNo = hdnMobile.Value.Trim();
                                string Mode = "UW";
                                string CreatedBy = "System";
                                string IsAttached = "0";
                                string AttachedFiles = null;
                                string ApplicationNo = HdnAppNumber.Value.Trim();
                                string PolicyNumber = HdnPolicyNumber.Value.Trim();
                                string ParameterList = "";
                                string FileName = "";
                                string IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];

                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (col)
                                    {

                                        case "<plan name>":
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<policy no.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                            break;


                                        case "<Proposer Name>":
                                            //1.6 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'";
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'";
                                            //1.6 Start of Changes for Revival 18 / 04 / 2020
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            }
                            else
                            {

                                ShowPopupMessage("Policy can't reinstate.", 0);
                                STPResult = "";
                            }






                        }
                        else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
                        {

                            //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                            //_strPolicyStatus = "DC";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);

                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Rejected";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);


                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");



                            ShowPopupMessage("Details Post Successfully", 0);



                            //Changes By Akshada UW Reject
                            string Flag1;
                            string CommunicationType;
                            string CommunicationKey;
                            string Process;
                            string TemplateId;
                            string MailTo;
                            string MailCC; ;
                            string MobileNo;
                            string Mode;
                            string CreatedBy;
                            string IsAttached;
                            string AttachedFiles;
                            string ApplicationNo;
                            string PolicyNumber;
                            string ParameterList;
                            string FileName;
                            string IsExternal;

                            try
                            {
                                Logger.Info(strApplicationno + "RejectedForImage_sms_start" + System.Environment.NewLine);
                                DataTable dtSMS = new DataTable();
                                Flag1 = "02";
                                CommunicationType = "SMS";
                                CommunicationKey = "SMS";
                                Process = "Under writing deision saral";
                                TemplateId = "4";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "";
                                IsExternal = "";

                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag_02" + System.Environment.NewLine);
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];

                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag_01" + System.Environment.NewLine);

                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string parameter = dtSMS.Rows[i][1].ToString();

                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<plan name>":
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<policy no.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                            break;


                                        case "<Proposer Name>":
                                            //1.7 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'";
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'";
                                            //1.7 End of Changes for Revival 18 / 04 / 2020
                                            break;

                                        //case "<Premium Amount>":
                                        //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                        //	break;

                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag3" + System.Environment.NewLine);
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_sms_success" + System.Environment.NewLine);

                            }
                            catch (Exception)
                            {

                                Logger.Info(strApplicationno + "RejectedForImage_sms_success" + System.Environment.NewLine);
                            }

                            /**Code Added By Akshada**/
                            //RejectedForImage//
                            try
                            {
                                Logger.Info(strApplicationno + "RejectedForImage_email_start" + System.Environment.NewLine);
                                DataTable dtemail = new DataTable();
                                //1.8 Start of Changes for Revival 18 / 04 / 2020
                                //string CustomerName = txtProposerName.Text.Trim();                    
                                string CustomerName = hdnLAFullName.Value;
                                //1.8 End of Changes for Revival 18 / 04 / 2020

                                string PolicyName = txtPlanName.Text.Trim();
                                /*PolicyNumber*/
                                string Reason1 = ddlUWDecesion.SelectedValue.ToString();
                                string Reason2 = ddlUWreason.SelectedItem.ToString();
                                string BankName = hdnbankname.Value;
                                string BankAccountNumber = hdnbankaccount.Value;

                                // string FirstDigit = BankAccountNumber.ToString().Substring(0, 8);

                                var last4 = "****" + BankAccountNumber.Substring(BankAccountNumber.Trim().Length - 4, 4) + "";

                                Flag1 = "02";
                                CommunicationType = "EMAIL";
                                CommunicationKey = "EMAIL";
                                Process = "We Are Unable To Revive Your Policy";
                                TemplateId = "27";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "Rejected_For_Image.html";
                                IsExternal = "YES";


                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemailrows = dtemail.Rows[0];

                                string RequestId_no = dsemailrows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag1" + System.Environment.NewLine);

                                for (int i = 0; i < dtemail.Rows.Count; i++)
                                {
                                    string parameter = dtemail.Rows[i][1].ToString();

                                    string col = dtemail.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<Customer_Name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Policy_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                            break;

                                        case "<Reason1>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason1 + "'),";
                                            break;

                                        case "<Reason2>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason2 + "'),";
                                            break;

                                        case "<Bank_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + BankName + "'),";
                                            break;

                                        case "<Bank_Account_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + last4 + "'";
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_email_success_03" + System.Environment.NewLine);
                            }
                            catch (Exception ex)
                            {
                                Logger.Info(strApplicationno + "RejectedForImage_email_fail" + ex.Message.ToString() + System.Environment.NewLine);
                                //throw ex;

                            }


                        }
                        else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
                        {
                            Logger.Info(strApplicationno + "master post Event Start_Step30" + System.Environment.NewLine);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            Logger.Info(strApplicationno + "master post Event Start_Step31" + System.Environment.NewLine);
                            //_strPolicyStatus = "PO";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
                            Logger.Info(strApplicationno + "master post Event Start_Step32" + System.Environment.NewLine);

                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Hold";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);
                            Logger.Info(strApplicationno + "master post Event Start_Step33" + System.Environment.NewLine);

                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");
                            Logger.Info(strApplicationno + "master post Event Start_Step34" + System.Environment.NewLine);


                            ShowPopupMessage("Details Post Successfully", 0);
                            Logger.Info(strApplicationno + "master post Event Start_Step35" + System.Environment.NewLine);
                            string Flag1;
                            string CommunicationType;
                            string CommunicationKey;
                            string Process;
                            string TemplateId;
                            string MailTo;
                            string MailCC;
                            string MobileNo;
                            string Mode;
                            string CreatedBy;
                            string IsAttached;
                            string AttachedFiles;
                            string ApplicationNo;
                            string PolicyNumber;
                            string ParameterList = "";
                            string FileName;
                            string IsExternal;
                            string RevivalCompletiondate;
                            HiddenField HdnRCD_date = (HiddenField)ContentPlaceHolder1.FindControl("hdnRCDdate");
                            DataTable dt_Revival = objBussiness.revivaldate(HdnPolicyNumber.Value);
                            RevivalCompletiondate = dt_Revival.Rows[0]["REVIVAL_END_DT"].ToString();
                            DateTime date = Convert.ToDateTime(RevivalCompletiondate);
                            try
                            {
                                Logger.Info(strApplicationno + "Postponed_sms_start" + System.Environment.NewLine);
                                DataTable dtSMS = new DataTable();
                                Flag1 = "02";
                                CommunicationType = "SMS";
                                CommunicationKey = "SMS";
                                Process = "We Are Unable To Revive Your Policy";
                                TemplateId = "5";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "";
                                IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];
                                Logger.Info(strApplicationno + "master post Event Start_Step36" + System.Environment.NewLine);
                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "master post Event Start_Step37" + System.Environment.NewLine);
                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (col)
                                    {

                                        case "<plan name>":
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<policy no.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                            break;


                                        case "<Postpone>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + ddlPostpone.SelectedValue + "'),";
                                            break;


                                        case "<Proposer Name>":
                                            //1.9 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'),";
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                            //1.9 End of Changes for Revival 18 / 04 / 2020
                                            break;

                                        case "<date of completion of revival period>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + RevivalCompletiondate.ToString().Trim() + "'";
                                            break;


                                        default:
                                            break;
                                    }
                                }
                                Logger.Info(strApplicationno + "master post Event Start_Step38" + System.Environment.NewLine);
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "Postponed_sms_success" + System.Environment.NewLine);

                            }
                            catch (Exception ex)
                            {

                                Logger.Info(strApplicationno + "Postponed_sms_Fail" + ex.Message.ToString() + System.Environment.NewLine);

                            }


                            //Code Regarding Email_PostPone_Template
                            /***Code Added By Akshada****/
                            try
                            {
                                Logger.Info(strApplicationno + "master post Event Start_Step39" + System.Environment.NewLine);
                                string RevivalDate = string.Empty;
                                Logger.Info(strApplicationno + "Postponed_email_start" + System.Environment.NewLine);
                                DataTable dtemail_PostPone = new DataTable();
                                HiddenField HdnRCDdate = (HiddenField)ContentPlaceHolder1.FindControl("hdnRCDdate");

                                DataTable dtdate = objBussiness.revivaldate(HdnPolicyNumber.Value);
                                RevivalDate = dtdate.Rows[0]["REVIVAL_END_DT"].ToString();
                                Logger.Info(strApplicationno + "master post Event Start_Step40" + System.Environment.NewLine);
                                //1.10 Start of Changes for Revival 18 / 04 / 2020
                                //string CustomerName = txtProposerName.Text.Trim();                    
                                string CustomerName = hdnLAFullName.Value;
                                //1.10 End of Changes for Revival 18 / 04 / 2020
                                //string CustomerName = txtProposerName.Text;
                                string Reason1 = ddlUWDecesion.SelectedValue;
                                string Reason2 = ddlUWreason.SelectedItem.Text.ToString();
                                string BankName = hdnbankname.Value;
                                string BankAccountNumber = hdnbankaccount.Value;
                                string PostPonedMonth = ddlPostpone.SelectedValue.ToString(); //string.Empty;
                                string PlanName = txtPlanName.Text.Trim();
                                string Policy_Number = HdnPolicyNumber.Value;
                                Logger.Info(strApplicationno + "master post Event Start_Step41" + System.Environment.NewLine);
                                //string RevivalDate = Convert.ToString(HdnRCDdate.Value);
                                //string FirstDigit = BankAccountNumber.ToString().Substring(0, 8);

                                var last4 = "****" + BankAccountNumber.Substring(BankAccountNumber.Trim().Length - 4, 4) + "";
                                Logger.Info(strApplicationno + "Postponed_email_data_assigned" + System.Environment.NewLine);


                                Flag1 = "02";
                                CommunicationType = "EMAIL";
                                CommunicationKey = "EMAIL";
                                Process = "We Have Postponed Your Request";
                                TemplateId = "29";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "PostPoned_Template.html";
                                IsExternal = "Yes";

                                Logger.Info(strApplicationno + "master post Event Start_Step41" + System.Environment.NewLine);
                                dtemail_PostPone = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemail_Postpone_Rows = dtemail_PostPone.Rows[0];
                                Logger.Info(strApplicationno + "master post Event Start_Step42" + System.Environment.NewLine);
                                string RequestId_no = dsemail_Postpone_Rows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail_PostPone = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "master post Event Start_Step43" + System.Environment.NewLine);
                                for (int i = 0; i < dtemail_PostPone.Rows.Count; i++)
                                {
                                    string parameter = dtemail_PostPone.Rows[i][1].ToString();

                                    string col = dtemail_PostPone.Rows[i][1].ToString();
                                    switch (parameter)
                                    {
                                        case "<Plan_name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + PlanName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + Policy_Number + "'),";
                                            break;

                                        case "<Customer_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Reason_1>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + Reason1 + "'),";
                                            break;

                                        case "<Reason_2>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + Reason2 + "'),";
                                            break;

                                        case "<Bank_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + BankName + "'),";
                                            break;

                                        case "<Bank_Account_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + last4 + "'),";
                                            break;

                                        case "<Postponed_Month>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + PostPonedMonth + "'),";
                                            break;

                                        case "<Revival_Date>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + RevivalDate + "'";
                                            break;



                                        default:
                                            break;

                                    }
                                }
                                Logger.Info(strApplicationno + "master post Event Start_Step44" + System.Environment.NewLine);
                                Flag1 = "03";
                                dtemail_PostPone = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "Remark:" + "Email_PostPone_Template_success" + "UW PostPone Mail-Success" + System.Environment.NewLine);
                            }
                            catch (Exception ex)
                            {

                                Logger.Info(strApplicationno + "Remark:" + "Email_PostPone_Template_fail" + ex.Message.ToString() + "UW PostPone Mail-Failure" + System.Environment.NewLine);
                            }
                        }
                        /**Code Ended By Akshada**/
                        else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);


                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Withdrawn";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);



                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");


                            ShowPopupMessage("Details Post Successfully", 0);

                            DataTable dtSMS = new DataTable();
                            string Flag1 = "02";
                            string CommunicationType = "SMS";
                            string CommunicationKey = "SMS";
                            string Process = "Under writing deision saral";
                            string TemplateId = "4";
                            string MailTo = hdnEmail.Value.Trim();
                            string MailCC = string.Empty;
                            string MobileNo = hdnMobile.Value.Trim();
                            string Mode = "UW";
                            string CreatedBy = "System";
                            string IsAttached = "0";
                            string AttachedFiles = null;
                            string ApplicationNo = HdnAppNumber.Value.Trim();
                            string PolicyNumber = HdnPolicyNumber.Value.Trim();
                            string ParameterList = "";
                            string FileName = "";
                            string IsExternal = "";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                            DataRow drSMS = dtSMS.Rows[0];

                            string RequestId = drSMS["RequestId"].ToString();
                            Flag1 = "01";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            for (int i = 0; i < dtSMS.Rows.Count; i++)
                            {
                                string col = dtSMS.Rows[i][1].ToString();
                                switch (col)
                                {
                                    case "<plan name>":
                                        ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                        break;

                                    case "<policy no.>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                        break;


                                    case "<Proposer Name>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'";
                                        break;

                                    //case "<Premium Amount>":
                                    //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                    //	break;

                                    default:
                                        break;
                                }
                            }
                            Flag1 = "03";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                        }
                        else if (ddlUWDecesion.SelectedValue == "Pending" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PS", struserid, ref intRetVal);

                            objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Pending";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);


                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");

                            ShowPopupMessage("Details Post Successfully", 0);

                            DataTable dtSMS = new DataTable();
                            string Flag1 = "02";
                            string CommunicationType = "SMS";
                            string CommunicationKey = "SMS";
                            string Process = "UW Requirement raised";
                            string TemplateId = "2";
                            string MailTo = hdnEmail.Value.Trim();
                            string MailCC = string.Empty;
                            string MobileNo = hdnMobile.Value.Trim();
                            string Mode = "UW";
                            string CreatedBy = "System";
                            string IsAttached = "0";
                            string AttachedFiles = null;
                            string ApplicationNo = HdnAppNumber.Value.Trim();
                            string PolicyNumber = HdnPolicyNumber.Value.Trim();
                            string ParameterList = "";
                            string FileName = "";
                            string IsExternal = "";


                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                            DataRow drSMS = dtSMS.Rows[0];

                            string RequestId = drSMS["RequestId"].ToString();
                            Flag1 = "01";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            for (int i = 0; i < dtSMS.Rows.Count; i++)
                            {
                                string col = dtSMS.Rows[i][1].ToString();
                                switch (col)
                                {

                                    case "<Plan Name>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                        break;

                                    case "<Policy No.>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'";
                                        break;


                                    case "<proposer name>":
                                        //1.11 Start of Changes for Revival 18 / 04 / 2020
                                        ParameterList += "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                        //1.11 End of Changes for Revival 18 / 04 / 2020
                                        break;

                                    //case "<Premium Amount>":
                                    //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                    //	break;


                                    default:
                                        break;
                                }
                            }
                            Flag1 = "03";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            //1.4 Start of Changes for Revival 18 / 04 / 2020
                            //string CustomerName = txtProposerName.Text.Trim();                    
                            string CustomerName = hdnLAFullName.Value;
                            //1.4 End of Changes for Revival 18 / 04 / 2020
                            //string CustomerName = txtProposerName.Text.Trim();
                            string PolicyName = txtPlanName.Text.Trim();

                            //Pending Required 
                            DataTable dtemail = new DataTable();
                            string ReqStatus = string.Empty;
                            GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");

                            int counter = 0;
                            string strdesc = string.Empty;

                            bool isMedical = false;
                            StringBuilder strMedical = new StringBuilder();

                            List<string> obj = new List<string>();

                            List<string> obj1 = new List<string>();

                            int counter1 = 0;
                            string MedicalRequiremt = string.Empty;
                            StringBuilder medrequirementraised = new StringBuilder();
                            int medicalcounter = 1;

                            foreach (GridViewRow rowfollowup in GvReq.Rows)
                            {

                                DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;
                                TextBox ddlDesc = rowfollowup.FindControl("lblfollowupDiscp") as TextBox;
                                DropDownList ddlCat = rowfollowup.FindControl("ddlCategory") as DropDownList;

                                if (ddlStatus.SelectedValue == "L")
                                {
                                    //strdesc =  ddlDesc.Text.ToString();

                                    //obj[counter1] = ddlDesc.Text.ToString();
                                    //counter1++;

                                    if (ddlCat.SelectedValue == "Medical" || ddlCat.SelectedValue == "Non Medical") //Changes added 19022020
                                    {
                                        obj1.Add(ddlDesc.Text.ToString());

                                        if (ddlCat.SelectedValue == "Medical")
                                        {
                                            isMedical = true;
                                            MedicalRequiremt = ddlDesc.Text.ToString();

                                            medrequirementraised.Append("<br/>" + medicalcounter + ". " + MedicalRequiremt);




                                        }
                                        medicalcounter++;
                                    }
                                    else
                                    {
                                        obj.Add(ddlDesc.Text.ToString());
                                    }


                                }


                            }

                            int Count = obj.Count;
                            int MedicalCount = 0;
                            //foreach(var value in obj)
                            //{
                            //    strdesc = value[obj].ToString();
                            //}
                            for (int i = 0; i < obj.Count; i++)
                            {
                                strdesc += string.Join("<br/>", obj[i]);

                            }

                            for (int i = 0; i < obj1.Count; i++)
                            {
                                MedicalCount++;
                                strMedical.Append("<br/>" + MedicalCount + ". " + obj1[i]);
                                //strMedical += string.Join("<br/>", obj1[i]);
                                //Count++;
                                //strMedical = Count + " " + obj1[i];
                                ViewState["Records"] = strMedical.ToString();

                            }

                            //for (int i = 0; i < obj.Count; i++)
                            //{
                            //    //strdesc += string.Join("<br/>", obj[i]);
                            //    strdesc += string.Join("<br/>", i+ ""+ obj[i]);
                            //}

                            //for (int i = 0; i < obj1.Count; i++)
                            //{
                            //   // strMedical += string.Join("<br/>", obj1[i]);
                            //    strMedical += string.Join("<br/>",i+""+ obj1[i]);
                            //}

                            //if (strMedical == "")
                            //{
                            //    strMedical = "N/A";
                            //}
                            //1.12 Start of Changes for Revival 18 / 04 / 2020
                            if (MedicalRequiremt == "")
                            {
                                MedicalRequiremt = "N/A";
                            }
                            //1.12 End of Changes for Revival 18 / 04 / 2020
                            DataTable dtemail_ = new DataTable();
                            Flag1 = "02";
                            CommunicationType = "EMAIL";
                            CommunicationKey = "EMAIL";
                            Process = "We Need More Information";
                            TemplateId = "26";
                            MailTo = hdnEmail.Value.Trim();
                            MailCC = "";
                            MobileNo = hdnMobile.Value.Trim();
                            Mode = "";
                            CreatedBy = "";
                            IsAttached = "0";
                            AttachedFiles = null;
                            ApplicationNo = HdnAppNumber.Value.Trim();
                            PolicyNumber = HdnPolicyNumber.Value.Trim();
                            ParameterList = "";
                            FileName = "Requirement_Raised_template.html";
                            IsExternal = "YES";

                            if (isMedical == false)
                            {
                                //strdesc = "N/A";

                                TemplateId = "52";
                                FileName = "RequirementRaised_NonMedical.html";


                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemailrows = dtemail.Rows[0];

                                string RequestId_no = dsemailrows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtemail.Rows.Count; i++)
                                {

                                    string col = dtemail.Rows[i][1].ToString();
                                    switch (col)
                                    {
                                        case "<Customer_Name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Policy_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                            break;

                                        case "<REQTable>":
                                            // ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strdesc + "'),";
                                            //1.13 Start of Changes for Revival 18 / 04 / 2020
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strMedical.ToString() + "'";
                                            break;


                                        default:
                                            break;
                                    }
                                }
                            }
                            else
                            {

                                TemplateId = "26";
                                FileName = "Requirement_Raised_template.html";

                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemailrows = dtemail.Rows[0];

                                string RequestId_no = dsemailrows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtemail.Rows.Count; i++)
                                {

                                    string col = dtemail.Rows[i][1].ToString();
                                    switch (col)
                                    {
                                        case "<Customer_Name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Policy_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                            break;

                                        case "<REQTable>":
                                            // ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strdesc + "'),";
                                            //1.13 Start of Changes for Revival 18 / 04 / 2020
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strMedical.ToString() + "'),";
                                            break;

                                        case "<REQMedical>":
                                            // ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strMedical + "'";
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + medrequirementraised.ToString() + "'";
                                            //1.13 End of Changes for Revival 18 / 04 / 2020
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            Flag1 = "03";
                            dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                        }
                        //############################################# Added by Akshay END ######################################################################################    
                    }
                    else
                    {
                        if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
                        {
                            //objComm.OnlineUWMISDecision_Save(strAppliationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "IF";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);

                            //To Insert Record in LF_applicationstatus table with '091' status 
                            objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);


                            //Added by akshay
                            //Insert policy status in Reinstatment base table 

                        }
                        else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
                        {

                            //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                            //_strPolicyStatus = "DC";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);

                        }
                        else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "PO";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
                        }
                        else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);
                        }
                        else if (ddlUWDecesion.SelectedValue == "proposal" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";

                            /************************1.2 Begin of Changes  CR-28153****************************************/
                            /***Comments: Used to Check Consent FollowUpCode Raised or not ****/
                            if (isConsentReq)
                            {
                                try
                                {
                                    objComm.OnlineRequirmentDisplayDetails_GET(ref dsreqdetails, strApplicationno, strChannelType);
                                    if (dsreqdetails.Tables[0].Rows.Count > 0)
                                    {

                                        int ConsentReqtRaisedCount = dsreqdetails.Tables[0].AsEnumerable()
                                                     .Where(r => r.Field<string>("REQ_followUpCode") == "SIS" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "CNE" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "COS" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "CME" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "COP" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "COL" && r.Field<string>("REQ_status") != "RS"
                                                           ).Count();
                                        ViewState["dtrequirements"] = dsreqdetails.Tables[0];
                                        if (ConsentReqtRaisedCount > 0)
                                        {
                                            ConsentLetter(strApplicationno, strChannelType);
                                        }
                                        else
                                        {
                                            Logger.Info(strApplicationno + "Consent FollowUpCode Not Raised" + System.Environment.NewLine);
                                        }

                                    }

                                }
                                catch (Exception ex)
                                {

                                    Logger.Info(strApplicationno + "ConsentLetterFailure" + " " + ex.Message.ToString() + System.Environment.NewLine);
                                }


                            }
                            else
                            {
                                Logger.Info(strApplicationno + "ConsentFollowUpNotRaised" + " " + isConsentReq + System.Environment.NewLine);
                            }

                            /************************1.2 End of Changes  CR-28153****************************************/
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PS", struserid, ref intRetVal);
                        }
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Details Post Successfully')", true);
                    ShowPopupMessage("Details Post Successfully", 1);

                    //lblErrorDecisiondtls.Text = "Decision Details Updated in Life Asia successfully";
                    if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
                    {
                        //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                        //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "IF";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);

                        //To Insert Record in LF_applicationstatus table with '091' status 
                        objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);
                    }
                    else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
                    {

                        //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                        //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                        //_strPolicyStatus = "DC";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);


                    }
                    else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
                    {
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "PO";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
                    }
                    else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
                    {
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "WD";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);
                    }
                    else if (ddlUWDecesion.SelectedValue == "proposal" && LAPushErrorCode == 0)
                    {
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "WD";


                        /************************1.3 Begin of Changes  CR-28153****************************************/
                        /***Comments: Used to Check Consent FollowUpCode Raised or not ****/
                        try
                        {
                            objComm.OnlineRequirmentDisplayDetails_GET(ref dsreqdetails, strApplicationno, strChannelType);
                            if (dsreqdetails.Tables[0].Rows.Count > 0)
                            {
                                DataTable dtrequirements = dsreqdetails.Tables[0].AsEnumerable()
                                             .Where(r => r.Field<string>("REQ_followUpCode") == "SIS" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "CNE" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "COS" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "CME" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "COP" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "COL" && r.Field<string>("REQ_status") != "RS"
                                                   ).CopyToDataTable();

                                ViewState["dtrequirements"] = dtrequirements;
                                TextBox txtSumassure = (TextBox)ContentPlaceHolder1.FindControl("txtSumassure");
                                HiddenField hdfSumassure = (HiddenField)ContentPlaceHolder1.FindControl("hdfSumassure");
                                HiddenField hfdCalPremSA = (HiddenField)ContentPlaceHolder1.FindControl("hfdCalPremSA");
                                HiddenField hfdCalPremFlag = (HiddenField)ContentPlaceHolder1.FindControl("hfdCalPremFlag");


                                if (txtSumassure.Text.Trim() != hdfSumassure.Value.Trim() && txtSumassure.Text.Trim() == hfdCalPremSA.Value.Trim() && hfdCalPremFlag.Value == "0")
                                {
                                    ShowPopupMessage("Please Click on Calculate Premium", 0);
                                    Logger.Info(strApplicationno + "Not Clicked on Premium calculation" + System.Environment.NewLine);
                                    return;
                                }
                                else
                                {
                                    Logger.Info(strApplicationno + "ConsentLetter_Invoked" + System.Environment.NewLine);
                                    ConsentLetter(strApplicationno, strChannelType);

                                }
                            }
                            else
                            {
                                Logger.Info(strApplicationno + "Consent FollowUpCode Not Raised" + System.Environment.NewLine);
                            }
                        }
                        catch (Exception ex)
                        {

                            Logger.Info(strApplicationno + "ConsentLetter_failure" + " " + ex.Message.ToString() + System.Environment.NewLine);
                        }

                        /************************1.3 End of Changes  CR-28153****************************************/

                        objBussiness.UpdatePolicyStatus(strApplicationno, "PS", struserid, ref intRetVal);
                    }
                    //Session.Abandon();
                    /*added by shri on 06-07-17 to close page on success*/
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show success message", "alert('Decision details save successfully');window.close();", true);
                    /*end here*/
                }

            }
            else
            {
                ShowPopupMessage(strLAPushErrorMsg, 0);
                /*commented and added by shri on 06-07-17 to close page on success*/
                //lblErrorDecisiondtls.Text = "Decision Details Not Updated in Life Asia,Please Contact system admin";
                //lblErrorDecisiondtls.Focus();
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert("+strLAPushErrorMsg+");", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision Details Not Updated in Life Asia due to " + strLAPushErrorMsg+ ");", true);
                /*end here*/
            }
        }
        else
        {
            /*commented and added by shri on 06-07-17 to close page on success*/
            //lblErrorDecisiondtls.Text = "Decision details Not Save ,Please Contact system admin";
            //lblErrorDecisiondtls.Focus();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision details Not Save ,Please Contact system admin');", true);
            /*end here*/
        }
    }

    private void UpdateDecisionInLa(ref int response)
    {
        Logger.Info(strApplicationno + "master post Event Start_Step17" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Begins" + System.Environment.NewLine);
        //declare variable
        int LAPushErrorCode = 0;
        int UWDecisionResult = 0;
        string strLAPushErrorMsg = string.Empty;
        int Result = 0;
        Boolean isConsentReq = false;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        struserid = objChangeObj.userLoginDetails._UserID;
        strUWmode = strChannelType;
        UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
        Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnPost_Click" + System.Environment.NewLine);
        DropDownList ddlUWDecesion = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWDecesion");
        DropDownList ddlUWreason = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWreason");
        //DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPStart_Step18eriod");
        DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPeriod");
        Label lblErrorDecisiondtls = (Label)ContentPlaceHolder1.FindControl("lblErrorDecisiondtls");

        //########################## Added by Shyam Starts ###########################################
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_01chnages starts" + System.Environment.NewLine);
        DropDownList ddlStatusREQ = (DropDownList)ContentPlaceHolder1.FindControl("ddlStatus");
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_01chnages ends" + System.Environment.NewLine);
        //########################## Added by Shyam End ###########################################

        if (objChangeObj.Load_Loadingdetails != null)
        {
            isConsentReq = objChangeObj.Load_Loadingdetails.isConsentRequired;
        }
        else
        {
            isConsentReq = false;
        }
        string _strUWDecesion = ddlUWDecesion.SelectedValue;
        string _strUWPeriod = ddlPostpone.SelectedValue == "0" ? "" : ddlPostpone.SelectedValue;
        string _strDataValue = string.Empty;
        int intRetVal = -1;

        //########################## Added by Shyam Starts ###########################################
        //Changes start 04042020
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_02chnages starts" + System.Environment.NewLine);
        Session["RequestType"] = "";
        if (Request.Url.Segments.Length > 3 && Request.Url.Segments[3].ToString() == "RevivalUwdecision.aspx")
        {
            Session["RequestType"] = "Revival";
        }
        if (Session["RequestType"].ToString() == "Revival")
            Logger.Info(Session["RequestType"].ToString() + "Revival start" + System.Environment.NewLine);
        {
            objCommonObj._AppType = "Single";
        }
        Logger.Info(strApplicationno + "UpdateDecisionInLa_Revival_02chnages ends" + objCommonObj._AppType + System.Environment.NewLine);
        //Changes start 04042020
        //########################## Added by Shyam End ###########################################

        Logger.Info(strApplicationno + "master post Event Start_Step18" + System.Environment.NewLine);
        //call servics

        objComm.OnlineUWDecisionDetails_Save(objCommonObj._AppType, strApplicationno, ddlUWDecesion.SelectedItem.Text, ddlUWreason.SelectedItem.Text,
            ddlUWDecesion.SelectedValue, ddlUWreason.SelectedItem.Value, _strUWPeriod, struserid, objChangeObj.userLoginDetails._UserName,
        objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, objCommonObj._bpm_creationDate, objCommonObj._bpm_systemDate,
        objCommonObj._bpm_businessDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName,
      strApplicationno, ref UWDecisionResult);
        //Changes start 04042020

        Logger.Info(strApplicationno + "master post Event Start_Step19" + System.Environment.NewLine);
        StringBuilder sb = new StringBuilder();

        sb.Append(objCommonObj._AppType + "," + strApplicationno + "," + ddlUWDecesion.SelectedItem.Text + "," +
        ddlUWreason.SelectedItem.Text + "," + ddlUWDecesion.SelectedValue + "," + ddlUWreason.SelectedItem.Value + "," + _strUWPeriod
        + "," + struserid + "," + objChangeObj.userLoginDetails._UserName + "," + objChangeObj.userLoginDetails._UserGroup
        + "," + objCommonObj._bpm_branchCode + "," + objCommonObj._bpm_creationDate + "," + objCommonObj._bpm_systemDate + ","
        + objCommonObj._bpm_businessDate + "," + objChangeObj.userLoginDetails._userBranch + "," + objChangeObj.userLoginDetails._ProcessName
        + "," + strApplicationno + "," + UWDecisionResult);

        string result = sb.ToString();

        Logger.Info(result + "master post Event Start_input" + System.Environment.NewLine);

        //Changes start 04042020
        if (UWDecisionResult != -1)
        {
            Logger.Info(strApplicationno + "master post Event Start_Step20" + System.Environment.NewLine);
            string strConsentResponse = string.Empty;
            //lblErrorDecisiondtls.Text = "Decision details save successfully";



            //########################## Added by Shyam Starts ###########################################
            //Changes start 04042020
            Session["RequestType"] = "";
            Logger.Info(Session["RequestType"].ToString() + "check revival" + System.Environment.NewLine);
            if (Request.Url.Segments.Length > 3 && Request.Url.Segments[3].ToString() == "RevivalUwdecision.aspx")
            {
                Logger.Info(Session["RequestType"].ToString() + "check revival_1" + System.Environment.NewLine);
                Session["RequestType"] = "Revival";
                Logger.Info(Session["RequestType"].ToString() + "check revival_2" + System.Environment.NewLine);
            }
            //Changes end 04042020
            if (Session["RequestType"].ToString() == "Revival")
            {
                Logger.Info(Session["RequestType"].ToString() + "check revival_3" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "master post Event Start_Step21" + System.Environment.NewLine);
                HiddenField HdnPremPayingStatus1 = (HiddenField)ContentPlaceHolder1.FindControl("hdnPremPayingStatus");
                HiddenField HdnPolicyStatus1 = (HiddenField)ContentPlaceHolder1.FindControl("hdnPolicyStatus");
                if (Session["hdnNewFollowup"] != null)
                {
                    NEWFollowUp = Session["hdnNewFollowup"].ToString();
                    NEWFollowUp.ToString();

                }

                if (HdnPremPayingStatus1.Value.Trim() == "Lapsed" ||
                        HdnPremPayingStatus1.Value.Trim() == "HA" || HdnPremPayingStatus1.Value.Trim() == "AC"
                        || HdnPremPayingStatus1.Value.Trim() == "PH" || HdnPremPayingStatus1.Value.Trim() == "UD" || HdnPolicyStatus1.Value.Trim() == "LA" ||
                        HdnPolicyStatus1.Value.Trim() == "PU" || HdnPolicyStatus1.Value.Trim() == "DU" || HdnPolicyStatus1.Value.Trim() == "HP")
                {
                    Logger.Info(strApplicationno + "master post Event Start_04042020_v1" + System.Environment.NewLine);
                    LAPushErrorCode = 0;
                }
                else
                {
                    objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
                }
            }
            else
            {
                objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
            }
            Logger.Info(strApplicationno + "master post Event Start_Step22" + System.Environment.NewLine);
            //########################## Added by Shyam End ###########################################



            if (!string.IsNullOrEmpty(strConsentResponse) && !strConsentResponse.Equals("Failed"))
            {
                Logger.Info("Consent Letter for application No : {0} : {1}" + strApplicationno + strConsentResponse);
                //string filePath = Request.QueryString["FILEPATH"].ToString();
                //Response.ContentType = "application/pdf";
                //Response.WriteFile(strConsentResponse);
                Response.Write("<script>");
                Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(strConsentResponse)));
                Response.Write("</script>");
                //WebClient myWebClient = new WebClient();
                // myWebClient.DownloadFile(strConsentResponse, "D:\abc.pdf");
                //Response.Redirect(strConsentResponse);
            }
            if (LAPushErrorCode == 0)
            {

                //########################## Added by Shyam Starts ###########################################
                Session["RequestType"] = "";
                if (Request.Url.Segments.Length > 3 && Request.Url.Segments[3].ToString() == "RevivalUwdecision.aspx")
                {
                    Logger.Info(strApplicationno + "master post Event Start_postponerevival" + System.Environment.NewLine);
                    // Changes start 04042020
                    Session["RequestType"] = "Revival";
                }
                //Changes start 04042020
                if (Session["RequestType"].ToString() == "Revival")
                {
                    HiddenField HdnPolicyStatus = (HiddenField)ContentPlaceHolder1.FindControl("hdnPolicyStatus");
                    HiddenField HdnPremPayingStatus = (HiddenField)ContentPlaceHolder1.FindControl("hdnPremPayingStatus");
                    HiddenField HdnPolicyNumber = (HiddenField)ContentPlaceHolder1.FindControl("hdnPolNo");
                    TextBox txtProposerName = (TextBox)ContentPlaceHolder1.FindControl("LAName");
                    //txtTotalPrmPay
                    TextBox txtPremAmt = (TextBox)ContentPlaceHolder1.FindControl("txtTotalPrmPay");
                    TextBox txtPlanName = (TextBox)ContentPlaceHolder1.FindControl("txtProname1");
                    HiddenField hdnMobile = (HiddenField)ContentPlaceHolder1.FindControl("hdnModilenumber");
                    HiddenField hdnEmail = (HiddenField)ContentPlaceHolder1.FindControl("hdnEmail");
                    HiddenField HdnAppNumber = (HiddenField)ContentPlaceHolder1.FindControl("HdnApplicationNumber");
                    DropDownList ddlStatusREQ1 = (DropDownList)ContentPlaceHolder1.FindControl("ddlStatus");
                    //HiddenField hdnbankname = (HiddenField)ContentPlaceHolder1.FindControl("HfnBankName");
                    //HiddenField hdnbankaccount = (HiddenField)ContentPlaceHolder1.FindControl("HfnAccountNo");
                    //Changes start 04042020
                    HiddenField hdnbankname = (HiddenField)ContentPlaceHolder1.FindControl("hdfnBankName");
                    HiddenField hdnbankaccount = (HiddenField)ContentPlaceHolder1.FindControl("hdfnBankAcctNumber");
                    //Changes end  04042020
                    //1.1 Start of Changes for Revival 18 / 04 / 2020
                    HiddenField hdnLAFullName = (HiddenField)ContentPlaceHolder1.FindControl("hdnLAFullname");
                    //1.1 End of Changes for Revival 18 / 04 / 2020

                    if (HdnPremPayingStatus.Value.Trim() == "Lapsed" ||
                        HdnPremPayingStatus.Value.Trim() == "HA" || HdnPremPayingStatus.Value.Trim() == "AC"
                        || HdnPremPayingStatus.Value.Trim() == "PH" || HdnPremPayingStatus.Value.Trim() == "UD" || HdnPolicyStatus.Value.Trim() == "LA" ||
                        HdnPolicyStatus.Value.Trim() == "PU" || HdnPolicyStatus.Value.Trim() == "DU" || HdnPolicyStatus.Value.Trim() == "HP")
                    {
                        //Communication for new requirement raised.

                        if (ddlUWDecesion.SelectedValue == "Declined")
                        {
                            string ReqStatus = string.Empty;
                            GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");
                            int counter = 0;
                            foreach (GridViewRow rowfollowup in GvReq.Rows)
                            {
                                DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;
                                if (ddlStatus.SelectedValue == "O")
                                {
                                    counter = counter + 1;
                                }
                            }


                            if (counter > 0 && NEWFollowUp == "TRUE")
                            {
                                DataTable dtSMS = new DataTable();
                                string Flag1 = "02";
                                string CommunicationType = "SMS";
                                string CommunicationKey = "SMS";

                                string Process = "Under writing deision saral";
                                string TemplateId = "4";
                                //Template Id 2 is for requirement raised should ne used where decision is pending
                                string MailTo = hdnEmail.Value.Trim();
                                string MailCC = string.Empty;
                                string MobileNo = hdnMobile.Value.Trim();
                                string Mode = "UW";
                                string CreatedBy = "System";
                                string IsAttached = "0";
                                string AttachedFiles = null;
                                string ApplicationNo = HdnAppNumber.Value.Trim();
                                string PolicyNumber = HdnPolicyNumber.Value.Trim();
                                string ParameterList = "";
                                string FileName = "";
                                string IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];

                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string parameter = dtSMS.Rows[i][1].ToString();

                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<proposer name>":
                                            //1.2 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'),";
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                            //1.2 End of Changes for Revival 18 / 04 / 2020
                                            break;

                                        case "<Plan Name>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<Policy No.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'";
                                            break;



                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                Session["hdnNewFollowup"] = null;

                                //RejectedForImage//
                                try
                                {
                                    Logger.Info(strApplicationno + "RejectedForImage_email_start" + System.Environment.NewLine);
                                    DataTable dtemail = new DataTable();
                                    //1.3 Start of Changes for Revival 18 / 04 / 2020
                                    //string CustomerName = txtProposerName.Text.Trim();                    
                                    string CustomerName = hdnLAFullName.Value;
                                    //1.3 End of Changes for Revival 18 / 04 / 2020
                                    string PolicyName = txtPlanName.Text.Trim();
                                    /*PolicyNumber*/
                                    string Reason1 = ddlUWDecesion.SelectedValue.ToString();
                                    string Reason2 = ddlUWreason.SelectedValue.ToString();
                                    string BankName = string.Empty;
                                    string BankAccountNumber = string.Empty;



                                    Flag1 = "02";
                                    CommunicationType = "EMAIL";
                                    CommunicationKey = "EMAIL";
                                    //Changes start 04042020
                                    Process = "We Are Unable To Revive Your Policy";
                                    //Changes end 04042020
                                    TemplateId = "27";
                                    MailTo = hdnEmail.Value.Trim();
                                    MailCC = string.Empty;
                                    MobileNo = hdnMobile.Value.Trim();
                                    Mode = "UW";
                                    CreatedBy = "System";
                                    IsAttached = "0";
                                    AttachedFiles = null;
                                    ApplicationNo = HdnAppNumber.Value.Trim();
                                    PolicyNumber = HdnPolicyNumber.Value.Trim();
                                    ParameterList = "";
                                    FileName = "Rejected_For_Image.html";
                                    IsExternal = "YES";


                                    dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                    DataRow dsemailrows = dtSMS.Rows[0];

                                    string RequestId_no = dsemailrows["RequestId"].ToString();
                                    Flag1 = "01";
                                    dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                    for (int i = 0; i < dtemail.Rows.Count; i++)
                                    {
                                        string parameter = dtemail.Rows[i][1].ToString();

                                        string col = dtemail.Rows[i][1].ToString();
                                        switch (parameter)
                                        {

                                            case "<Customer_Name>":
                                                ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                                break;

                                            case "<Policy_Name>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                                break;

                                            case "<Policy_Number>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                                break;

                                            case "<Reason1>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason1 + "'),";
                                                break;

                                            case "<Reason2>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason2 + "'),";
                                                break;

                                            case "<Bank_Name>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + BankName + "'),";
                                                break;

                                            case "<Bank_Account_Number>":
                                                ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + BankAccountNumber + "'";
                                                break;

                                            default:
                                                break;
                                        }
                                    }
                                    Flag1 = "03";
                                    dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                    Logger.Info(strApplicationno + "RejectedForImage_email_success" + System.Environment.NewLine);
                                }
                                catch (Exception ex)
                                {
                                    Logger.Info(strApplicationno + "RejectedForImage_email_fail" + System.Environment.NewLine);
                                    throw ex;
                                }

                            }
                        }

                        else if (ddlUWDecesion.SelectedValue == "Postponed")
                        {
                            Logger.Info(strApplicationno + "master post Event Start_Step22_postponedstarts" + System.Environment.NewLine);
                            HiddenField HdnMonthCounts = (HiddenField)ContentPlaceHolder1.FindControl("HdnFinalMonthCount");

                            if (Convert.ToInt64(HdnMonthCounts.Value) > 0)
                            {
                                Logger.Info("poststart" + "master post Event Start_postponedstarts_s" + System.Environment.NewLine);
                                int Months = Convert.ToInt32(HdnMonthCounts.Value) - 1;
                                int postponeMonths = Convert.ToInt32(ddlPostpone.SelectedValue);
                                int ActuallMonths = Convert.ToInt32(HdnMonthCounts.Value);
                                StringBuilder sb1 = new StringBuilder();
                                sb1.Append(Months + "," + postponeMonths + "," + ActuallMonths);
                                string inputmsg = sb1.ToString();
                                Logger.Info(inputmsg + "master post Event Start_postponedstarts_1" + System.Environment.NewLine);
                                //ShowPopupMessage("" + HdnMonthCounts.Value + " Months remaining for revival period", 0);
                                if (postponeMonths > Months)
                                {
                                    ShowPopupMessage("The revival can be allowed for this case up to " + ActuallMonths + " period only.Hence you can postpone this policy up to " + Months + " months maximum.Please change postpone period or revise the UW decision accordingly", 0);
                                    return;
                                }
                                //else if (postponeMonths > Months)
                                //{
                                //    ShowPopupMessage("The revival can be allowed for this case up to " + Months + " period only.Hence you can postpone this policy up to " + Months + " months maximum.Please change postpone period or revise the UW decision accordingly", 0);
                                //    return;
                                //}
                                //else
                                //{
                                //    ShowPopupMessage("The revival can be allowed for this case up to " + Months + " period only.Hence you can postpone this policy up to " + Months + " months maximum.Please change postpone period or revise the UW decision accordingly", 0);
                                //}


                            }
                            else
                            {
                                ShowPopupMessage("You cannot Postpone this case upto provided " + ddlPostpone.SelectedValue + ",since revival after " + ddlPostpone.SelectedValue + " not allowed. Please review UW decision accordingly", 0);
                                return;
                            }

                            Logger.Info(strApplicationno + "master post Event Start_Step23" + System.Environment.NewLine);

                            string ReqStatus = string.Empty;
                            GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");

                            int counter = 0;

                            foreach (GridViewRow rowfollowup in GvReq.Rows)
                            {

                                DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;

                                if (ddlStatus.SelectedValue == "O")
                                {
                                    counter = counter + 1;
                                }
                            }
                            Logger.Info(strApplicationno + "master post Event Start_Step24" + System.Environment.NewLine);

                            if (counter > 0 && NEWFollowUp == "TRUE")
                            {
                                Logger.Info(strApplicationno + "master post Event Start_Step25" + System.Environment.NewLine);
                                DataTable dtSMS = new DataTable();
                                string Flag1 = "02";
                                string CommunicationType = "SMS";
                                string CommunicationKey = "SMS";
                                string Process = "UW Requirement raised";
                                string TemplateId = "2";
                                string MailTo = hdnEmail.Value.Trim();
                                string MailCC = string.Empty;
                                string MobileNo = hdnMobile.Value.Trim();
                                string Mode = "UW";
                                string CreatedBy = "System";
                                string IsAttached = "0";
                                string AttachedFiles = null;
                                string ApplicationNo = HdnAppNumber.Value.Trim();
                                string PolicyNumber = HdnPolicyNumber.Value.Trim();
                                string ParameterList = "";
                                string FileName = "";
                                string IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];
                                Logger.Info(strApplicationno + "master post Event Start_Step26" + System.Environment.NewLine);
                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "master post Event Start_Step27" + System.Environment.NewLine);
                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string parameter = dtSMS.Rows[i][1].ToString();

                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<proposer name>":

                                            //1.5 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'),";
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                            //1.5 Start of Changes for Revival 18 / 04 / 2020
                                            break;

                                        case "<Plan Name>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<Policy No.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'";
                                            break;

                                        //case "<Premium Amount>":
                                        //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                        //	break;

                                        default:
                                            break;
                                    }
                                }
                                Logger.Info(strApplicationno + "master post Event Start_Step28" + System.Environment.NewLine);
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Session["hdnNewFollowup"] = null;
                                Logger.Info(strApplicationno + "master post Event Start_Step29" + System.Environment.NewLine);
                            }
                        }

                        if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
                        {
                            //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);



                            //DataSet dsREQ = new DataSet();
                            //objBussiness.CheckOpenRequirement(HdnAppNumber.Value.Trim(), "1",ref dsREQ);
                            //if(dsREQ.Tables[0].Rows.Count == 0 )
                            //{


                            //}
                            //else
                            //{

                            //	ShowPopupMessage("Please close all Requirements", 0);
                            //}

                            //Commented Start by kunal 26-03-2020  Reason - Details to be updated only if status changes in LA
                            //string ReqStatus = string.Empty;
                            //GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");


                            //foreach (GridViewRow rowfollowup in GvReq.Rows)
                            //{
                            //	DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;

                            //	if (ddlStatus.SelectedValue == "O")
                            //	{
                            //		ShowPopupMessage("Please close all Requirements", 0);
                            //		return;
                            //	}
                            //	else
                            //	{
                            //		//objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);

                            //		//Akshay
                            //		UWSaralServices.FollowupDetails objFollowup = new UWSaralServices.FollowupDetails();
                            //		//CommFun objComm = new CommFun();
                            //		DataSet _dsFollowUp = null;

                            //		objComm.OnlineServiceFollowUPDetails_GET(ref _dsFollowUp, HdnAppNumber.Value.Trim(), "Offline");

                            //		objFollowup.FollowuPushService(HdnAppNumber.Value.Trim(), _dsFollowUp, objChangeObj, ref LAPushErrorCode, ref strLAPushErrorMsg);
                            //	}
                            //}

                            //strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            ////_strPolicyStatus = "IF";
                            //objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);

                            ////To Insert Record in LF_applicationstatus table with '091' status 
                            //objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);
                            //Commented End by kunal 26-03-2020  Reason - Details to be updated only if status changes in LA

                            //UWSARAL STATUS UPDATE.
                            string STPResult = string.Empty;

                            REINSTSTPService.Service1Client objREINSTSTP = new REINSTSTPService.Service1Client();
                            string UserID = struserid.ToString().StartsWith("F") ? struserid.ToString() : "F" + struserid.ToString();
                            STPResult = objREINSTSTP.IsSTPRulesPassORFail(HdnPolicyNumber.Value.Trim(), "BOR", "BOR", "UW", "10", "2", UserID);
                            //objCommonObj._bpm_branchCode

                            if (STPResult == "0")
                            {
                                //Commented code pasted Start by kunal 26-03-2020  Reason - Details to be updated only if status changes in LA
                                string ReqStatus = string.Empty;
                                GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");
                                foreach (GridViewRow rowfollowup in GvReq.Rows)
                                {
                                    DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;

                                    if (ddlStatus.SelectedValue == "O")
                                    {
                                        ShowPopupMessage("Please close all Requirements", 0);
                                        return;
                                    }
                                    else
                                    {

                                        UWSaralServices.FollowupDetails objFollowup = new UWSaralServices.FollowupDetails();

                                        DataSet _dsFollowUp = null;

                                        objComm.OnlineServiceFollowUPDetails_GET(ref _dsFollowUp, HdnAppNumber.Value.Trim(), "Offline");

                                        objFollowup.FollowuPushService(HdnAppNumber.Value.Trim(), _dsFollowUp, objChangeObj, ref LAPushErrorCode, ref strLAPushErrorMsg);
                                    }
                                }
                                strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                                objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                                objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);


                                objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);




                                Flag = "1";
                                Status = "UW Approved";
                                objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);

                                objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");

                                ShowPopupMessage("Details Post Successfully", 0);
                                STPResult = "";

                                //UW ACCEPTS
                                DataTable dtSMS = new DataTable();
                                string Flag1 = "02";
                                string CommunicationType = "SMS";
                                string CommunicationKey = "SMS";
                                string Process = "Under writing deision saral";
                                string TemplateId = "3";
                                string MailTo = hdnEmail.Value.Trim();
                                string MailCC = string.Empty;
                                string MobileNo = hdnMobile.Value.Trim();
                                string Mode = "UW";
                                string CreatedBy = "System";
                                string IsAttached = "0";
                                string AttachedFiles = null;
                                string ApplicationNo = HdnAppNumber.Value.Trim();
                                string PolicyNumber = HdnPolicyNumber.Value.Trim();
                                string ParameterList = "";
                                string FileName = "";
                                string IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];

                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (col)
                                    {

                                        case "<plan name>":
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<policy no.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                            break;


                                        case "<Proposer Name>":
                                            //1.6 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'";
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'";
                                            //1.6 Start of Changes for Revival 18 / 04 / 2020
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            }
                            else
                            {

                                ShowPopupMessage("Policy can't reinstate.", 0);
                                STPResult = "";
                            }






                        }
                        else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
                        {

                            //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                            //_strPolicyStatus = "DC";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);

                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Rejected";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);


                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");



                            ShowPopupMessage("Details Post Successfully", 0);



                            //Changes By Akshada UW Reject
                            string Flag1;
                            string CommunicationType;
                            string CommunicationKey;
                            string Process;
                            string TemplateId;
                            string MailTo;
                            string MailCC; ;
                            string MobileNo;
                            string Mode;
                            string CreatedBy;
                            string IsAttached;
                            string AttachedFiles;
                            string ApplicationNo;
                            string PolicyNumber;
                            string ParameterList;
                            string FileName;
                            string IsExternal;

                            try
                            {
                                Logger.Info(strApplicationno + "RejectedForImage_sms_start" + System.Environment.NewLine);
                                DataTable dtSMS = new DataTable();
                                Flag1 = "02";
                                CommunicationType = "SMS";
                                CommunicationKey = "SMS";
                                Process = "Under writing deision saral";
                                TemplateId = "4";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "";
                                IsExternal = "";

                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag_02" + System.Environment.NewLine);
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];

                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag_01" + System.Environment.NewLine);

                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string parameter = dtSMS.Rows[i][1].ToString();

                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<plan name>":
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<policy no.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                            break;


                                        case "<Proposer Name>":
                                            //1.7 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'";
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'";
                                            //1.7 End of Changes for Revival 18 / 04 / 2020
                                            break;

                                        //case "<Premium Amount>":
                                        //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                        //	break;

                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag3" + System.Environment.NewLine);
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_sms_success" + System.Environment.NewLine);

                            }
                            catch (Exception)
                            {

                                Logger.Info(strApplicationno + "RejectedForImage_sms_success" + System.Environment.NewLine);
                            }

                            /**Code Added By Akshada**/
                            //RejectedForImage//
                            try
                            {
                                Logger.Info(strApplicationno + "RejectedForImage_email_start" + System.Environment.NewLine);
                                DataTable dtemail = new DataTable();
                                //1.8 Start of Changes for Revival 18 / 04 / 2020
                                //string CustomerName = txtProposerName.Text.Trim();                    
                                string CustomerName = hdnLAFullName.Value;
                                //1.8 End of Changes for Revival 18 / 04 / 2020

                                string PolicyName = txtPlanName.Text.Trim();
                                /*PolicyNumber*/
                                string Reason1 = ddlUWDecesion.SelectedValue.ToString();
                                string Reason2 = ddlUWreason.SelectedItem.ToString();
                                string BankName = hdnbankname.Value;
                                string BankAccountNumber = hdnbankaccount.Value;

                                // string FirstDigit = BankAccountNumber.ToString().Substring(0, 8);

                                var last4 = "****" + BankAccountNumber.Substring(BankAccountNumber.Trim().Length - 4, 4) + "";

                                Flag1 = "02";
                                CommunicationType = "EMAIL";
                                CommunicationKey = "EMAIL";
                                Process = "We Are Unable To Revive Your Policy";
                                TemplateId = "27";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "Rejected_For_Image.html";
                                IsExternal = "YES";


                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemailrows = dtemail.Rows[0];

                                string RequestId_no = dsemailrows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_sms_flag1" + System.Environment.NewLine);

                                for (int i = 0; i < dtemail.Rows.Count; i++)
                                {
                                    string parameter = dtemail.Rows[i][1].ToString();

                                    string col = dtemail.Rows[i][1].ToString();
                                    switch (parameter)
                                    {

                                        case "<Customer_Name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Policy_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                            break;

                                        case "<Reason1>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason1 + "'),";
                                            break;

                                        case "<Reason2>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + Reason2 + "'),";
                                            break;

                                        case "<Bank_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + BankName + "'),";
                                            break;

                                        case "<Bank_Account_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + last4 + "'";
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                Flag1 = "03";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "RejectedForImage_email_success_03" + System.Environment.NewLine);
                            }
                            catch (Exception ex)
                            {
                                Logger.Info(strApplicationno + "RejectedForImage_email_fail" + ex.Message.ToString() + System.Environment.NewLine);
                                //throw ex;

                            }


                        }
                        else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
                        {
                            Logger.Info(strApplicationno + "master post Event Start_Step30" + System.Environment.NewLine);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            Logger.Info(strApplicationno + "master post Event Start_Step31" + System.Environment.NewLine);
                            //_strPolicyStatus = "PO";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
                            Logger.Info(strApplicationno + "master post Event Start_Step32" + System.Environment.NewLine);

                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Hold";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);
                            Logger.Info(strApplicationno + "master post Event Start_Step33" + System.Environment.NewLine);

                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");
                            Logger.Info(strApplicationno + "master post Event Start_Step34" + System.Environment.NewLine);


                            ShowPopupMessage("Details Post Successfully", 0);
                            Logger.Info(strApplicationno + "master post Event Start_Step35" + System.Environment.NewLine);
                            string Flag1;
                            string CommunicationType;
                            string CommunicationKey;
                            string Process;
                            string TemplateId;
                            string MailTo;
                            string MailCC;
                            string MobileNo;
                            string Mode;
                            string CreatedBy;
                            string IsAttached;
                            string AttachedFiles;
                            string ApplicationNo;
                            string PolicyNumber;
                            string ParameterList = "";
                            string FileName;
                            string IsExternal;
                            string RevivalCompletiondate;
                            HiddenField HdnRCD_date = (HiddenField)ContentPlaceHolder1.FindControl("hdnRCDdate");
                            DataTable dt_Revival = objBussiness.revivaldate(HdnPolicyNumber.Value);
                            RevivalCompletiondate = dt_Revival.Rows[0]["REVIVAL_END_DT"].ToString();
                            DateTime date = Convert.ToDateTime(RevivalCompletiondate);
                            try
                            {
                                Logger.Info(strApplicationno + "Postponed_sms_start" + System.Environment.NewLine);
                                DataTable dtSMS = new DataTable();
                                Flag1 = "02";
                                CommunicationType = "SMS";
                                CommunicationKey = "SMS";
                                Process = "We Are Unable To Revive Your Policy";
                                TemplateId = "5";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "";
                                IsExternal = "";

                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow drSMS = dtSMS.Rows[0];
                                Logger.Info(strApplicationno + "master post Event Start_Step36" + System.Environment.NewLine);
                                string RequestId = drSMS["RequestId"].ToString();
                                Flag1 = "01";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "master post Event Start_Step37" + System.Environment.NewLine);
                                for (int i = 0; i < dtSMS.Rows.Count; i++)
                                {
                                    string col = dtSMS.Rows[i][1].ToString();
                                    switch (col)
                                    {

                                        case "<plan name>":
                                            ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                            break;

                                        case "<policy no.>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                            break;


                                        case "<Postpone>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + ddlPostpone.SelectedValue + "'),";
                                            break;


                                        case "<Proposer Name>":
                                            //1.9 Start of Changes for Revival 18 / 04 / 2020
                                            //ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtProposerName.Text.Trim() + "'),";
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                            //1.9 End of Changes for Revival 18 / 04 / 2020
                                            break;

                                        case "<date of completion of revival period>":
                                            ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + RevivalCompletiondate.ToString().Trim() + "'";
                                            break;


                                        default:
                                            break;
                                    }
                                }
                                Logger.Info(strApplicationno + "master post Event Start_Step38" + System.Environment.NewLine);
                                Flag1 = "03";
                                dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "Postponed_sms_success" + System.Environment.NewLine);

                            }
                            catch (Exception ex)
                            {

                                Logger.Info(strApplicationno + "Postponed_sms_Fail" + ex.Message.ToString() + System.Environment.NewLine);

                            }


                            //Code Regarding Email_PostPone_Template
                            /***Code Added By Akshada****/
                            try
                            {
                                Logger.Info(strApplicationno + "master post Event Start_Step39" + System.Environment.NewLine);
                                string RevivalDate = string.Empty;
                                Logger.Info(strApplicationno + "Postponed_email_start" + System.Environment.NewLine);
                                DataTable dtemail_PostPone = new DataTable();
                                HiddenField HdnRCDdate = (HiddenField)ContentPlaceHolder1.FindControl("hdnRCDdate");

                                DataTable dtdate = objBussiness.revivaldate(HdnPolicyNumber.Value);
                                RevivalDate = dtdate.Rows[0]["REVIVAL_END_DT"].ToString();
                                Logger.Info(strApplicationno + "master post Event Start_Step40" + System.Environment.NewLine);
                                //1.10 Start of Changes for Revival 18 / 04 / 2020
                                //string CustomerName = txtProposerName.Text.Trim();                    
                                string CustomerName = hdnLAFullName.Value;
                                //1.10 End of Changes for Revival 18 / 04 / 2020
                                //string CustomerName = txtProposerName.Text;
                                string Reason1 = ddlUWDecesion.SelectedValue;
                                string Reason2 = ddlUWreason.SelectedItem.Text.ToString();
                                string BankName = hdnbankname.Value;
                                string BankAccountNumber = hdnbankaccount.Value;
                                string PostPonedMonth = ddlPostpone.SelectedValue.ToString(); //string.Empty;
                                string PlanName = txtPlanName.Text.Trim();
                                string Policy_Number = HdnPolicyNumber.Value;
                                Logger.Info(strApplicationno + "master post Event Start_Step41" + System.Environment.NewLine);
                                //string RevivalDate = Convert.ToString(HdnRCDdate.Value);
                                //string FirstDigit = BankAccountNumber.ToString().Substring(0, 8);

                                var last4 = "****" + BankAccountNumber.Substring(BankAccountNumber.Trim().Length - 4, 4) + "";
                                Logger.Info(strApplicationno + "Postponed_email_data_assigned" + System.Environment.NewLine);


                                Flag1 = "02";
                                CommunicationType = "EMAIL";
                                CommunicationKey = "EMAIL";
                                Process = "We Have Postponed Your Request";
                                TemplateId = "29";
                                MailTo = hdnEmail.Value.Trim();
                                MailCC = string.Empty;
                                MobileNo = hdnMobile.Value.Trim();
                                Mode = "UW";
                                CreatedBy = "System";
                                IsAttached = "0";
                                AttachedFiles = null;
                                ApplicationNo = HdnAppNumber.Value.Trim();
                                PolicyNumber = HdnPolicyNumber.Value.Trim();
                                ParameterList = "";
                                FileName = "PostPoned_Template.html";
                                IsExternal = "Yes";

                                Logger.Info(strApplicationno + "master post Event Start_Step41" + System.Environment.NewLine);
                                dtemail_PostPone = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemail_Postpone_Rows = dtemail_PostPone.Rows[0];
                                Logger.Info(strApplicationno + "master post Event Start_Step42" + System.Environment.NewLine);
                                string RequestId_no = dsemail_Postpone_Rows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail_PostPone = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "master post Event Start_Step43" + System.Environment.NewLine);
                                for (int i = 0; i < dtemail_PostPone.Rows.Count; i++)
                                {
                                    string parameter = dtemail_PostPone.Rows[i][1].ToString();

                                    string col = dtemail_PostPone.Rows[i][1].ToString();
                                    switch (parameter)
                                    {
                                        case "<Plan_name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + PlanName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + Policy_Number + "'),";
                                            break;

                                        case "<Customer_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Reason_1>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + Reason1 + "'),";
                                            break;

                                        case "<Reason_2>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + Reason2 + "'),";
                                            break;

                                        case "<Bank_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + BankName + "'),";
                                            break;

                                        case "<Bank_Account_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + last4 + "'),";
                                            break;

                                        case "<Postponed_Month>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + PostPonedMonth + "'),";
                                            break;

                                        case "<Revival_Date>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail_PostPone.Rows[i][1].ToString() + "'" + "," + "'" + RevivalDate + "'";
                                            break;



                                        default:
                                            break;

                                    }
                                }
                                Logger.Info(strApplicationno + "master post Event Start_Step44" + System.Environment.NewLine);
                                Flag1 = "03";
                                dtemail_PostPone = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                Logger.Info(strApplicationno + "Remark:" + "Email_PostPone_Template_success" + "UW PostPone Mail-Success" + System.Environment.NewLine);
                            }
                            catch (Exception ex)
                            {

                                Logger.Info(strApplicationno + "Remark:" + "Email_PostPone_Template_fail" + ex.Message.ToString() + "UW PostPone Mail-Failure" + System.Environment.NewLine);
                            }
                        }
                        /**Code Ended By Akshada**/
                        else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);


                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Withdrawn";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);



                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");


                            ShowPopupMessage("Details Post Successfully", 0);

                            DataTable dtSMS = new DataTable();
                            string Flag1 = "02";
                            string CommunicationType = "SMS";
                            string CommunicationKey = "SMS";
                            string Process = "Under writing deision saral";
                            string TemplateId = "4";
                            string MailTo = hdnEmail.Value.Trim();
                            string MailCC = string.Empty;
                            string MobileNo = hdnMobile.Value.Trim();
                            string Mode = "UW";
                            string CreatedBy = "System";
                            string IsAttached = "0";
                            string AttachedFiles = null;
                            string ApplicationNo = HdnAppNumber.Value.Trim();
                            string PolicyNumber = HdnPolicyNumber.Value.Trim();
                            string ParameterList = "";
                            string FileName = "";
                            string IsExternal = "";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                            DataRow drSMS = dtSMS.Rows[0];

                            string RequestId = drSMS["RequestId"].ToString();
                            Flag1 = "01";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            for (int i = 0; i < dtSMS.Rows.Count; i++)
                            {
                                string col = dtSMS.Rows[i][1].ToString();
                                switch (col)
                                {
                                    case "<plan name>":
                                        ParameterList = "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                        break;

                                    case "<policy no.>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'),";
                                        break;


                                    case "<Proposer Name>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'";
                                        break;

                                    //case "<Premium Amount>":
                                    //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                    //	break;

                                    default:
                                        break;
                                }
                            }
                            Flag1 = "03";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                        }
                        else if (ddlUWDecesion.SelectedValue == "Pending" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PS", struserid, ref intRetVal);

                            objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
                            //UWSARAL STATUS UPDATE.
                            Flag = "1";
                            Status = "UW Pending";
                            objBussiness.UWSaralUpdate(HdnPolicyNumber.Value.Trim(), struserid, Flag, Status);


                            //Update RefundAmount in Pos 
                            objBussiness.UpdateReciptAmt(HdnAppNumber.Value.Trim(), "1");

                            ShowPopupMessage("Details Post Successfully", 0);

                            DataTable dtSMS = new DataTable();
                            string Flag1 = "02";
                            string CommunicationType = "SMS";
                            string CommunicationKey = "SMS";
                            string Process = "UW Requirement raised";
                            string TemplateId = "2";
                            string MailTo = hdnEmail.Value.Trim();
                            string MailCC = string.Empty;
                            string MobileNo = hdnMobile.Value.Trim();
                            string Mode = "UW";
                            string CreatedBy = "System";
                            string IsAttached = "0";
                            string AttachedFiles = null;
                            string ApplicationNo = HdnAppNumber.Value.Trim();
                            string PolicyNumber = HdnPolicyNumber.Value.Trim();
                            string ParameterList = "";
                            string FileName = "";
                            string IsExternal = "";


                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                            DataRow drSMS = dtSMS.Rows[0];

                            string RequestId = drSMS["RequestId"].ToString();
                            Flag1 = "01";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            for (int i = 0; i < dtSMS.Rows.Count; i++)
                            {
                                string col = dtSMS.Rows[i][1].ToString();
                                switch (col)
                                {

                                    case "<Plan Name>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPlanName.Text.Trim() + "'),";
                                        break;

                                    case "<Policy No.>":
                                        ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + HdnPolicyNumber.Value.Trim() + "'";
                                        break;


                                    case "<proposer name>":
                                        //1.11 Start of Changes for Revival 18 / 04 / 2020
                                        ParameterList += "'" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + hdnLAFullName.Value.Trim() + "'),";
                                        //1.11 End of Changes for Revival 18 / 04 / 2020
                                        break;

                                    //case "<Premium Amount>":
                                    //	ParameterList += "('" + RequestId + "'" + "," + "'" + dtSMS.Rows[i][1].ToString() + "'" + "," + "'" + txtPremAmt.Text.Trim() + "'";
                                    //	break;


                                    default:
                                        break;
                                }
                            }
                            Flag1 = "03";
                            dtSMS = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                            //1.4 Start of Changes for Revival 18 / 04 / 2020
                            //string CustomerName = txtProposerName.Text.Trim();                    
                            string CustomerName = hdnLAFullName.Value;
                            //1.4 End of Changes for Revival 18 / 04 / 2020
                            //string CustomerName = txtProposerName.Text.Trim();
                            string PolicyName = txtPlanName.Text.Trim();

                            //Pending Required 
                            DataTable dtemail = new DataTable();
                            string ReqStatus = string.Empty;
                            GridView GvReq = (GridView)ContentPlaceHolder1.FindControl("gvRequirmentDetails");

                            int counter = 0;
                            string strdesc = string.Empty;

                            bool isMedical = false;
                            StringBuilder strMedical = new StringBuilder();

                            List<string> obj = new List<string>();

                            List<string> obj1 = new List<string>();

                            int counter1 = 0;
                            string MedicalRequiremt = string.Empty;
                            StringBuilder medrequirementraised = new StringBuilder();
                            int medicalcounter = 1;

                            foreach (GridViewRow rowfollowup in GvReq.Rows)
                            {

                                DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;
                                TextBox ddlDesc = rowfollowup.FindControl("lblfollowupDiscp") as TextBox;
                                DropDownList ddlCat = rowfollowup.FindControl("ddlCategory") as DropDownList;

                                if (ddlStatus.SelectedValue == "L")
                                {
                                    //strdesc =  ddlDesc.Text.ToString();

                                    //obj[counter1] = ddlDesc.Text.ToString();
                                    //counter1++;

                                    if (ddlCat.SelectedValue == "Medical" || ddlCat.SelectedValue == "Non Medical") //Changes added 19022020
                                    {
                                        obj1.Add(ddlDesc.Text.ToString());

                                        if (ddlCat.SelectedValue == "Medical")
                                        {
                                            isMedical = true;
                                            MedicalRequiremt = ddlDesc.Text.ToString();

                                            medrequirementraised.Append("<br/>" + medicalcounter + ". " + MedicalRequiremt);




                                        }
                                        medicalcounter++;
                                    }
                                    else
                                    {
                                        obj.Add(ddlDesc.Text.ToString());
                                    }


                                }


                            }

                            int Count = obj.Count;
                            int MedicalCount = 0;
                            //foreach(var value in obj)
                            //{
                            //    strdesc = value[obj].ToString();
                            //}
                            for (int i = 0; i < obj.Count; i++)
                            {
                                strdesc += string.Join("<br/>", obj[i]);

                            }

                            for (int i = 0; i < obj1.Count; i++)
                            {
                                MedicalCount++;
                                strMedical.Append("<br/>" + MedicalCount + ". " + obj1[i]);
                                //strMedical += string.Join("<br/>", obj1[i]);
                                //Count++;
                                //strMedical = Count + " " + obj1[i];
                                ViewState["Records"] = strMedical.ToString();

                            }

                            //for (int i = 0; i < obj.Count; i++)
                            //{
                            //    //strdesc += string.Join("<br/>", obj[i]);
                            //    strdesc += string.Join("<br/>", i+ ""+ obj[i]);
                            //}

                            //for (int i = 0; i < obj1.Count; i++)
                            //{
                            //   // strMedical += string.Join("<br/>", obj1[i]);
                            //    strMedical += string.Join("<br/>",i+""+ obj1[i]);
                            //}

                            //if (strMedical == "")
                            //{
                            //    strMedical = "N/A";
                            //}
                            //1.12 Start of Changes for Revival 18 / 04 / 2020
                            if (MedicalRequiremt == "")
                            {
                                MedicalRequiremt = "N/A";
                            }
                            //1.12 End of Changes for Revival 18 / 04 / 2020
                            DataTable dtemail_ = new DataTable();
                            Flag1 = "02";
                            CommunicationType = "EMAIL";
                            CommunicationKey = "EMAIL";
                            Process = "We Need More Information";
                            TemplateId = "26";
                            MailTo = hdnEmail.Value.Trim();
                            MailCC = "";
                            MobileNo = hdnMobile.Value.Trim();
                            Mode = "";
                            CreatedBy = "";
                            IsAttached = "0";
                            AttachedFiles = null;
                            ApplicationNo = HdnAppNumber.Value.Trim();
                            PolicyNumber = HdnPolicyNumber.Value.Trim();
                            ParameterList = "";
                            FileName = "Requirement_Raised_template.html";
                            IsExternal = "YES";

                            if (isMedical == false)
                            {
                                //strdesc = "N/A";

                                TemplateId = "52";
                                FileName = "RequirementRaised_NonMedical.html";


                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemailrows = dtemail.Rows[0];

                                string RequestId_no = dsemailrows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtemail.Rows.Count; i++)
                                {

                                    string col = dtemail.Rows[i][1].ToString();
                                    switch (col)
                                    {
                                        case "<Customer_Name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Policy_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                            break;

                                        case "<REQTable>":
                                            // ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strdesc + "'),";
                                            //1.13 Start of Changes for Revival 18 / 04 / 2020
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strMedical.ToString() + "'";
                                            break;


                                        default:
                                            break;
                                    }
                                }
                            }
                            else
                            {

                                TemplateId = "26";
                                FileName = "Requirement_Raised_template.html";

                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);
                                DataRow dsemailrows = dtemail.Rows[0];

                                string RequestId_no = dsemailrows["RequestId"].ToString();
                                Flag1 = "01";
                                dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                                for (int i = 0; i < dtemail.Rows.Count; i++)
                                {

                                    string col = dtemail.Rows[i][1].ToString();
                                    switch (col)
                                    {
                                        case "<Customer_Name>":
                                            ParameterList = "'" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + CustomerName + "'),";
                                            break;

                                        case "<Policy_Name>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyName + "'),";
                                            break;

                                        case "<Policy_Number>":
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + PolicyNumber + "'),";
                                            break;

                                        case "<REQTable>":
                                            // ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strdesc + "'),";
                                            //1.13 Start of Changes for Revival 18 / 04 / 2020
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strMedical.ToString() + "'),";
                                            break;

                                        case "<REQMedical>":
                                            // ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + strMedical + "'";
                                            ParameterList += "('" + RequestId_no + "'" + "," + "'" + dtemail.Rows[i][1].ToString() + "'" + "," + "'" + medrequirementraised.ToString() + "'";
                                            //1.13 End of Changes for Revival 18 / 04 / 2020
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            Flag1 = "03";
                            dtemail = objBussiness.EmailParameter(Flag1, CommunicationType, CommunicationKey, Process, TemplateId, MailTo, MailCC, MobileNo, Mode, CreatedBy, IsAttached, AttachedFiles, ApplicationNo, PolicyNumber, ParameterList, FileName, IsExternal);

                        }
                        //############################################# Added by Akshay END ######################################################################################    
                    }
                    else
                    {
                        if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
                        {
                            //objComm.OnlineUWMISDecision_Save(strAppliationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "IF";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);

                            //To Insert Record in LF_applicationstatus table with '091' status 
                            objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);


                            //Added by akshay
                            //Insert policy status in Reinstatment base table 

                        }
                        else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
                        {

                            //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                            //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                            //_strPolicyStatus = "DC";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);

                        }
                        else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "PO";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
                        }
                        else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";
                            objBussiness.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);
                        }
                        else if (ddlUWDecesion.SelectedValue == "proposal" && LAPushErrorCode == 0)
                        {
                            strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                            //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                            //_strPolicyStatus = "WD";

                            /************************1.2 Begin of Changes  CR-28153****************************************/
                            /***Comments: Used to Check Consent FollowUpCode Raised or not ****/
                            if (isConsentReq)
                            {
                                try
                                {
                                    objComm.OnlineRequirmentDisplayDetails_GET(ref dsreqdetails, strApplicationno, strChannelType);
                                    if (dsreqdetails.Tables[0].Rows.Count > 0)
                                    {

                                        int ConsentReqtRaisedCount = dsreqdetails.Tables[0].AsEnumerable()
                                                     .Where(r => r.Field<string>("REQ_followUpCode") == "SIS" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "CNE" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "COS" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "CME" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "COP" && r.Field<string>("REQ_status") != "RS"
                                                           || r.Field<string>("REQ_followUpCode") == "COL" && r.Field<string>("REQ_status") != "RS"
                                                           ).Count();
                                        ViewState["dtrequirements"] = dsreqdetails.Tables[0];
                                        if (ConsentReqtRaisedCount > 0)
                                        {
                                            ConsentLetter(strApplicationno, strChannelType);
                                        }
                                        else
                                        {
                                            Logger.Info(strApplicationno + "Consent FollowUpCode Not Raised" + System.Environment.NewLine);
                                        }

                                    }

                                }
                                catch (Exception ex)
                                {

                                    Logger.Info(strApplicationno + "ConsentLetterFailure" + " " + ex.Message.ToString() + System.Environment.NewLine);
                                }


                            }
                            else
                            {
                                Logger.Info(strApplicationno + "ConsentFollowUpNotRaised" + " " + isConsentReq + System.Environment.NewLine);
                            }

                            /************************1.2 End of Changes  CR-28153****************************************/
                            objBussiness.UpdatePolicyStatus(strApplicationno, "PS", struserid, ref intRetVal);
                        }
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Details Post Successfully')", true);
                    ShowPopupMessage("Details Post Successfully", 1);

                    //lblErrorDecisiondtls.Text = "Decision Details Updated in Life Asia successfully";
                    if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
                    {
                        //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                        //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "IF";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);

                        //To Insert Record in LF_applicationstatus table with '091' status 
                        objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);
                    }
                    else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
                    {

                        //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                        //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

                        //_strPolicyStatus = "DC";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);


                    }
                    else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
                    {
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "PO";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
                    }
                    else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
                    {
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "WD";
                        objBussiness.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);
                    }
                    else if (ddlUWDecesion.SelectedValue == "proposal" && LAPushErrorCode == 0)
                    {
                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                        //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                        //_strPolicyStatus = "WD";


                        /************************1.3 Begin of Changes  CR-28153****************************************/
                        /***Comments: Used to Check Consent FollowUpCode Raised or not ****/
                        try
                        {
                            objComm.OnlineRequirmentDisplayDetails_GET(ref dsreqdetails, strApplicationno, strChannelType);
                            if (dsreqdetails.Tables[0].Rows.Count > 0)
                            {
                                DataTable dtrequirements = dsreqdetails.Tables[0].AsEnumerable()
                                             .Where(r => r.Field<string>("REQ_followUpCode") == "SIS" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "CNE" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "COS" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "CME" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "COP" && r.Field<string>("REQ_status") != "RS"
                                                   || r.Field<string>("REQ_followUpCode") == "COL" && r.Field<string>("REQ_status") != "RS"
                                                   ).CopyToDataTable();

                                ViewState["dtrequirements"] = dtrequirements;
                                TextBox txtSumassure = (TextBox)ContentPlaceHolder1.FindControl("txtSumassure");
                                HiddenField hdfSumassure = (HiddenField)ContentPlaceHolder1.FindControl("hdfSumassure");
                                HiddenField hfdCalPremSA = (HiddenField)ContentPlaceHolder1.FindControl("hfdCalPremSA");
                                HiddenField hfdCalPremFlag = (HiddenField)ContentPlaceHolder1.FindControl("hfdCalPremFlag");


                                if (txtSumassure.Text.Trim() != hdfSumassure.Value.Trim() && txtSumassure.Text.Trim() == hfdCalPremSA.Value.Trim() && hfdCalPremFlag.Value == "0")
                                {
                                    ShowPopupMessage("Please Click on Calculate Premium", 0);
                                    Logger.Info(strApplicationno + "Not Clicked on Premium calculation" + System.Environment.NewLine);
                                    return;
                                }
                                else
                                {
                                    Logger.Info(strApplicationno + "ConsentLetter_Invoked" + System.Environment.NewLine);
                                    ConsentLetter(strApplicationno, strChannelType);

                                }
                            }
                            else
                            {
                                Logger.Info(strApplicationno + "Consent FollowUpCode Not Raised" + System.Environment.NewLine);
                            }
                        }
                        catch (Exception ex)
                        {

                            Logger.Info(strApplicationno + "ConsentLetter_failure" + " " + ex.Message.ToString() + System.Environment.NewLine);
                        }

                        /************************1.3 End of Changes  CR-28153****************************************/

                        objBussiness.UpdatePolicyStatus(strApplicationno, "PS", struserid, ref intRetVal);
                    }
                    //Session.Abandon();
                    /*added by shri on 06-07-17 to close page on success*/
                    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show success message", "alert('Decision details save successfully');window.close();", true);
                    /*end here*/
                }

            }
            else
            {
                ShowPopupMessage(strLAPushErrorMsg, 0);
                /*commented and added by shri on 06-07-17 to close page on success*/
                //lblErrorDecisiondtls.Text = "Decision Details Not Updated in Life Asia,Please Contact system admin";
                //lblErrorDecisiondtls.Focus();
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert("+strLAPushErrorMsg+");", true);
                //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision Details Not Updated in Life Asia due to " + strLAPushErrorMsg+ ");", true);
                /*end here*/
            }
        }
        else
        {
            /*commented and added by shri on 06-07-17 to close page on success*/
            //lblErrorDecisiondtls.Text = "Decision details Not Save ,Please Contact system admin";
            //lblErrorDecisiondtls.Focus();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision details Not Save ,Please Contact system admin');", true);
            /*end here*/
        }
    }

    protected void btnSystem_Click(object sender, EventArgs e)
    {
        //int intRetVal = -1;
        //string struserid = string.Empty;
        //if (Session["objCommonObj"] != null)
        //{
        //    objCommonObj = (CommonObject)Session["objCommonObj"];
        //    struserid = objCommonObj._Bpmuserdetails._UserID;
        //    objComm.ManageApplicationLifeCycle(strApplicationno, struserid, "UW_DECISION_SYSTEM", false, ref intRetVal);
        //}
        //objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);
        //objComm.ManageApplicationLifeCycle(strApplicationno, struserid, "UW_DECISION_SYSTEM", true, ref intRetVal);
        ////Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Added to queue');window.close();", true);
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision details Not Save ,Please Contact system admin');", true);
    }

    private void ContentClientDetails(MasterPageComparision objMasterPageComparision)
    {
        GridView gvClientDetails = (GridView)ContentPlaceHolder1.FindControl("gvClientDetails");
        List<ClientSection> lstClientDetails = new List<ClientSection>();
        foreach (GridViewRow rowfollowup in gvClientDetails.Rows)
        {
            ClientSection objClientDetails = new ClientSection();
            //define variable                            
            //Label lblClientId = (Label)rowfollowup.FindControl("ClientId");
            //Label lblRole = (Label)rowfollowup.FindControl("Role");
            string lblClientId = rowfollowup.Cells[0].Text;
            string lblRole = rowfollowup.Cells[5].Text;

            objClientDetails.ClientId = lblClientId;
            objClientDetails.ClientRole = lblRole;
            lstClientDetails.Add(objClientDetails);
        }
        objMasterPageComparision.LstClientDetails = lstClientDetails;
    }

    private void ContentRiderDetails(MasterPageComparision objMasterPageComparision)
    {
        GridView gvRiderDtls = (GridView)ContentPlaceHolder1.FindControl("gvRiderDtls");
        List<RiderSection> lstRiderSection = new List<RiderSection>();
        foreach (GridViewRow rowfollowup in gvRiderDtls.Rows)
        {
            //define variable
            CheckBox cbIsRider = (CheckBox)rowfollowup.FindControl("chkremoveRider");

            if (cbIsRider.Checked)
            {
                Label lblRiderName = (Label)rowfollowup.FindControl("lblRiderName");
                RiderSection objRiderSection = new RiderSection();
                TextBox txtRiderSumAssured = (TextBox)rowfollowup.FindControl("txtRiderSumAssure");
                Label lblRiderCode = (Label)rowfollowup.FindControl("lblRiderCode");
                string[] strRiderSumAssured = txtRiderSumAssured.Text.Split(',');
                if (strRiderSumAssured.Length > 0)
                {
                    txtRiderSumAssured.Text = strRiderSumAssured[strRiderSumAssured.Length - 1];
                }
                objRiderSection.RiderCode = lblRiderCode.Text;
                objRiderSection.RiderSumAssured = txtRiderSumAssured.Text;
                lstRiderSection.Add(objRiderSection);
            }
        }
        objMasterPageComparision.LstRiderSection = lstRiderSection;
    }

    private void ContentProductDetails(MasterPageComparision objMasterPageComparision,string PolNum)
    {
        List<ProductSection> lstProductSection = new List<ProductSection>();
        string strComboMonthlyPayout = string.Empty;
        string strProdMonthlyPayout = string.Empty;
        ProductSection objProductSectionBase = new ProductSection();
        ProductSection objProductSectionCombo = new ProductSection();
        Repeater rptproductlist = (Repeater)ContentPlaceHolder1.FindControl("rptproductlist");
        foreach (RepeaterItem item in rptproductlist.Items)
        {
            TextBox txtBasepolno = (TextBox)item.FindControl("txtBasepolno");
            if (txtBasepolno.Text == PolNum)
            {


                TextBox txtPolterm = (TextBox)item.FindControl("txtPolterm");
                TextBox txtSumassure = (TextBox)item.FindControl("txtSumassure");
                TextBox txtProdcode = (TextBox)item.FindControl("txtProdcode");
                TextBox txtPrepayterm = (TextBox)item.FindControl("txtPrepayterm");
                DropDownList ddlFrequency = (DropDownList)item.FindControl("ddlFrequency");

                objProductSectionBase.PolicyTerm = Request.Form[txtPolterm.UniqueID];
                objProductSectionBase.PremiumFreq = ddlFrequency.SelectedValue;
                objProductSectionBase.SumAssured = Request.Form[txtSumassure.UniqueID];
                objProductSectionBase.ProductCode = txtProdcode.Text;
                objProductSectionBase.PremiumTerm = Request.Form[txtPrepayterm.UniqueID];
                lstProductSection.Add(objProductSectionBase);
            }
        }
        
     
        objMasterPageComparision.LstProductSection = lstProductSection;
    }

    //added by suraj for combi product
    private void ContentProductDetails_Combi(MasterPageComparision objMasterPageComparision, string PolNum)
    {
        List<ProductSection> lstProductSection = new List<ProductSection>();
        string strComboMonthlyPayout = string.Empty;
        string strProdMonthlyPayout = string.Empty;
        ProductSection objProductSectionBase = new ProductSection();
        ProductSection objProductSectionCombo = new ProductSection();
        Repeater rptproductlist = (Repeater)ContentPlaceHolder1.FindControl("rptproductlist");
        foreach (RepeaterItem item in rptproductlist.Items)
        {
            TextBox txtBasepolno = (TextBox)item.FindControl("txtBasepolno");
            if (txtBasepolno.Text == PolNum)
            {


                TextBox txtPolterm = (TextBox)item.FindControl("txtPolterm");
                TextBox txtSumassure = (TextBox)item.FindControl("txtSumassure");
                TextBox txtProdcode = (TextBox)item.FindControl("txtProdcode");
                TextBox txtPrepayterm = (TextBox)item.FindControl("txtPrepayterm");
                DropDownList ddlFrequency = (DropDownList)item.FindControl("ddlFrequency");

                objProductSectionBase.PolicyTerm = txtPolterm.Text;
                objProductSectionBase.PremiumFreq = ddlFrequency.SelectedValue;
                objProductSectionBase.SumAssured = txtSumassure.Text;
                objProductSectionBase.ProductCode = txtProdcode.Text;
                objProductSectionBase.PremiumTerm =txtPrepayterm.Text;
                lstProductSection.Add(objProductSectionBase);
            }
        }


        objMasterPageComparision.LstProductSection = lstProductSection;
    }

    private void ShowPopupMessage(string alertMessage, int strErrorCode)
    {
        if (strErrorCode == 0)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('" + alertMessage + "');window.close();", true);
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "ShowModalPopupClose('" + alertMessage + "');", true);
        }
    }

    /*added by shri on 28 dec 17 to add tracking*/
    private void InsertUwDecisionTracking(string strApplicationNo, string strUserId, DateTime dtCurrentDateTime, string strEventName, ref int intRet)
    {
        Logger.Info(strApplicationno + "master post Event Start_Step5" + System.Environment.NewLine);
        objComm = new Commfun();
        objComm.InsertUwDecisionTracking(strApplicationNo, strUserId, dtCurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fff"), strEventName, ref intRet);
        Logger.Info(strApplicationno + "master post Event Start_Step8" + System.Environment.NewLine);
    }

    /*added by shri on 28 dec 17 to update tracking*/
    private void UpdateUwDecisionTracking(int intSrNo, DateTime dtEndDate, ref int intRet)
    {
        Logger.Info(strApplicationno + "master post Event Start_Step11" + System.Environment.NewLine);
        objComm = new Commfun();
        objComm.UpdateUwDecisionTracking(intSrNo, dtEndDate.ToString("yyyy-MM-dd HH:mm:ss:fff"), ref intRet);
        Logger.Info(strApplicationno + "master post Event Start_Step15" + System.Environment.NewLine);
    }
    /*end here*/

    protected void btnOpenExcel_Click(object sender, EventArgs e)
    {
        Response.Write("<script>");
        Response.Write("window.open('../AppCode/UW Guidelines for IT -March 2019.pdf', '_newtab');");
        Response.Write("</script>");
    }

    /***********1.4 Begin of Changes CR 28153************/

    string ConsentAPI = ConfigurationManager.AppSettings["ConsentAPI"].ToString().Trim();
    private bool ConsentLetter(string strApplicationno, string ChannelType)
    {

        try
        {

            int strLApushErrorCode = 0;

            //Existing Method Used To Fetch Requirement Details
            objComm.OnlineRequirmentDisplayDetails_GET(ref dsreqdetails, strApplicationno, ChannelType);

            try
            {

                objComm.ClientBasicInfo_GET(ref _dsClientInfo, strApplicationno);//Existing Method Used to fetch Client And Proposer name
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);//Existing Method
                dtclientdetails = objComm.GetClientData(strPolicyNo);//EzytekMethod Used to get Client Email Id and MobileNo
                objComm.OnlineProductDisplayDetails_GET(ref _dsProdDtls, strApplicationno, ChannelType);//Existing Method Used to Get Plan details

                dtclientinfo = _dsClientInfo.Tables[0].Select("Role = 'Proposer'").CopyToDataTable();
                dtproductdetails = _dsProdDtls.Tables[0];
                dt = _ds.Tables[0];

                if (dtclientinfo.Rows.Count > 0 && dtproductdetails.Rows.Count > 0 && dt.Rows.Count > 0)
                {
                    #region BasePlan Details
                    objconsentparams.CustomerName = dtclientinfo.Rows[0]["ClientFullName"].ToString().Trim();
                    objconsentparams.ProductName = dtproductdetails.Rows[0]["ProdcutName"].ToString().Trim();
                    objconsentparams.B_SumAssured = !string.IsNullOrEmpty(dtproductdetails.Rows[0]["SumAssured"].ToString().Trim()) ? Convert.ToDecimal(dtproductdetails.Rows[0]["SumAssured"]) : 0;
                    objconsentparams.B_PolicyTerm = !string.IsNullOrEmpty(dtproductdetails.Rows[0]["PolicyTerm"].ToString().Trim()) ? Convert.ToInt32(dtproductdetails.Rows[0]["PolicyTerm"]) : 0;
                    objconsentparams.B_PremiumPaymentTerm = !string.IsNullOrEmpty(dtproductdetails.Rows[0]["PremiumTerm"].ToString().Trim()) ? Convert.ToInt32(dtproductdetails.Rows[0]["PremiumTerm"]) : 0;
                    objconsentparams.B_PlanId = dtproductdetails.Rows[0]["ProductCode"].ToString().Trim();
                    objconsentparams.Proposer = objconsentparams.CustomerName;
                    objconsentparams.B_BasePremium = !string.IsNullOrEmpty(dtproductdetails.Rows[0]["BasePremium"].ToString().Trim()) ? Convert.ToDecimal(dtproductdetails.Rows[0]["BasePremium"]) : 0;
                    objconsentparams.b_LoadedPremium = !string.IsNullOrEmpty(dt.Rows[0]["ExtraPremiumAmt"].ToString().Trim()) ? Convert.ToDecimal(dt.Rows[0]["ExtraPremiumAmt"]) : 0;
                    objconsentparams.B_Tax = !string.IsNullOrEmpty(dtproductdetails.Rows[0]["ServiceTax"].ToString().Trim()) ? Convert.ToDecimal(dtproductdetails.Rows[0]["ServiceTax"]) : 0;
                    objconsentparams.B_TotalPayableAmount = objconsentparams.B_BasePremium + objconsentparams.b_LoadedPremium + objconsentparams.B_Tax;
                    objconsentparams.ConsentCreatedBy = objCommonObj._Bpmuserdetails._UserID;
                    objconsentparams.Consentupdatedby = objCommonObj._Bpmuserdetails._UserID;
                    objconsentparams.B_TotalAmountPaidTillNow = objconsentparams.B_BasePremium + objconsentparams.b_LoadedPremium;
                    objconsentparams.B_BalanceAmountPayable = objconsentparams.B_TotalPayableAmount - objconsentparams.B_TotalAmountPaidTillNow;
                    objconsentparams.EmaiILD = dtclientdetails.Rows[0]["Email"].ToString();
                    objconsentparams.MobileNo = dtclientdetails.Rows[0]["MobileNumber"].ToString();
                    string IsExternal = ConfigurationManager.AppSettings["IsExternal"].ToString().Trim();
                    objconsentparams.ConsentStatus = ConfigurationManager.AppSettings["ConsentRaised"].ToString().Trim();
                    #endregion BasePlan

                    #region BasePlan Insertion Start 


                    //Ezytek Method to insert in Consent Tables
                    try
                    {
                        dtinsertclientdetials = objComm.InsertConsentDetails(strPolicyNo, "02", strApplicationno, objconsentparams.ProductName, objconsentparams.Proposer, objconsentparams.CustomerName, objconsentparams.B_SumAssured, objconsentparams.B_PolicyTerm, objconsentparams.B_BasePremium, objconsentparams.b_LoadedPremium, objconsentparams.B_Tax, objconsentparams.B_TotalPayableAmount, objconsentparams.B_TotalAmountPaidTillNow, objconsentparams.B_BalanceAmountPayable, objconsentparams.B_PlanId, objconsentparams.ConsentStatus, objconsentparams.B_PremiumPaymentTerm, objconsentparams.ConsentCreatedBy, objconsentparams.Consentupdatedby, true, objconsentparams.Guid, "", "");
                        objconsentparams.Guid = dtinsertclientdetials.Rows[0]["UniqueID"].ToString().Trim();
                        dtinsertclientdetials = objComm.InsertConsentDetails(strPolicyNo, "05", strApplicationno, objconsentparams.ProductName, objconsentparams.Proposer, objconsentparams.CustomerName, objconsentparams.B_SumAssured, objconsentparams.B_PolicyTerm, objconsentparams.B_BasePremium, objconsentparams.b_LoadedPremium, objconsentparams.B_Tax, objconsentparams.B_TotalPayableAmount, objconsentparams.B_TotalAmountPaidTillNow, objconsentparams.B_BalanceAmountPayable, objconsentparams.B_PlanId, objconsentparams.ConsentStatus, objconsentparams.B_PremiumPaymentTerm, objconsentparams.ConsentCreatedBy, objconsentparams.Consentupdatedby, true, objconsentparams.Guid, objconsentparams.EmaiILD, objconsentparams.MobileNo);
                        Logger.Info(strApplicationno + "BasePlan Insertion Sucess For Consent Letter" + System.Environment.NewLine);
                    }
                    catch (Exception ex)
                    {

                        Logger.Info(strApplicationno + "BasePlan Insertion Failure For Consent Letter" + " " + ex.Message.ToString() + System.Environment.NewLine);
                    }

                    //Condition used to insert revised sum assured value in CommHistory table only if Reduction of Sum assured followup is raised else pass 0
                    dtrequirements = (DataTable)ViewState["dtrequirements"];
                    if (dtrequirements.Rows[0]["REQ_followUpCode"].ToString() == "COS" && dtrequirements.Rows[0]["REQ_status"].ToString() != "RS")
                    {

                        TextBox txtSumassure = (TextBox)ContentPlaceHolder1.FindControl("txtSumassure");
                        TextBox RevisedPT = (TextBox)ContentPlaceHolder1.FindControl("txtPolterm");
                        TextBox RevisedSumassured = (TextBox)ContentPlaceHolder1.FindControl("txtSumassure");
                        // (TextBox)ContentPlaceHolder1.FindControl(txtSumassure.UniqueID);----@LoadedPremium
                        //TextBox RevisedTotalTax=(TextBox)ContentPlaceHolder1.FindControl("txtCombServiceTax"); /*-----@TotalTax*/
                        //TextBox RevisedTotalPremium =(TextBox)ContentPlaceHolder1.FindControl("txtCombTotalPrem");





                        string RevisedSumAssured = txtSumassure.Text.Trim();

                        objconsentparams.B_SumAssured = Convert.ToDecimal(RevisedSumAssured);
                        objconsentparams.B_PolicyTerm = Convert.ToInt32(RevisedPT.Text.Trim());
                        objconsentparams.ConsentStatus = ConfigurationManager.AppSettings["COS_ConsentSatus"].ToString();
                        //objconsentparams.B_Tax = Convert.ToInt32(RevisedTotalTax.Text.Trim());
                        //objconsentparams.B_TotalPayableAmount = Convert.ToDecimal(RevisedTotalPremium.Text.Trim());
                        //objconsentparams.B_BalanceAmountPayable = objconsentparams.B_TotalPayableAmount - objconsentparams.B_TotalAmountPaidTillNow;
                        dtinsertclientdetials = objComm.InsertConsentDetails(strPolicyNo, "07", strApplicationno, objconsentparams.ProductName, objconsentparams.Proposer, objconsentparams.CustomerName, objconsentparams.B_SumAssured, objconsentparams.B_PolicyTerm, objconsentparams.B_BasePremium, objconsentparams.b_LoadedPremium, objconsentparams.B_Tax, objconsentparams.B_TotalPayableAmount, objconsentparams.B_TotalAmountPaidTillNow, objconsentparams.B_BalanceAmountPayable, objconsentparams.B_PlanId, objconsentparams.ConsentStatus, objconsentparams.B_PremiumPaymentTerm, objconsentparams.ConsentCreatedBy, objconsentparams.Consentupdatedby, true, objconsentparams.Guid, "", "");
                    }
                    else
                    {
                        objconsentparams.B_SumAssured = 0;
                    }
                    dtinsertclientdetials = objComm.InsertConsentDetails(strPolicyNo, "03", strApplicationno, objconsentparams.ProductName, objconsentparams.Proposer, objconsentparams.CustomerName, objconsentparams.B_SumAssured, objconsentparams.B_PolicyTerm, objconsentparams.B_BasePremium, objconsentparams.b_LoadedPremium, objconsentparams.B_Tax, objconsentparams.B_TotalPayableAmount, objconsentparams.B_TotalAmountPaidTillNow, objconsentparams.B_BalanceAmountPayable, objconsentparams.B_PlanId, objconsentparams.ConsentStatus, objconsentparams.B_PremiumPaymentTerm, objconsentparams.ConsentCreatedBy, objconsentparams.Consentupdatedby, true, objconsentparams.Guid, "", "");
                    Logger.Info(strApplicationno + "ConsentLetter_Success" + System.Environment.NewLine);
                    #endregion BasePlan Insertion End

                    #region Rider Section

                    //Ezytek method to insert rider details in consent table
                    _dsriderdetails = objComm.FetchRiderDetailsConsentLetter(strApplicationno, ChannelType);
                    try
                    {
                        foreach (DataTable dtttt in _dsriderdetails.Tables)
                        {
                            foreach (DataRow drrr in dtttt.Rows)
                            {
                                objconsentparams.Ridername = drrr["RIDERNAME"].ToString();
                                objconsentparams.RiderId = drrr["RIDERCODE"].ToString();

                                objconsentparams.R_SumAssured = !string.IsNullOrEmpty(drrr["RIDERSUMASSURED"].ToString()) ? Convert.ToDecimal(drrr["RIDERSUMASSURED"]) : 0;
                                objconsentparams.R_PolicyTerm = !string.IsNullOrEmpty(drrr["RiderPT"].ToString()) ? Convert.ToInt32(drrr["RiderPT"]) : 0;
                                objconsentparams.R_PremiumPaymentTerm = !string.IsNullOrEmpty(drrr["RiderPPT"].ToString()) ? Convert.ToInt32(drrr["RiderPPT"]) : 0;
                                objconsentparams.R_TotalTax = !string.IsNullOrEmpty(drrr["SERVICETAX"].ToString()) ? Convert.ToInt32(drrr["SERVICETAX"]) : 0;
                                dtinsertclientdetials = objComm.InsertConsentDetails(strPolicyNo, "05", strApplicationno, objconsentparams.Ridername, objconsentparams.Proposer, objconsentparams.CustomerName, objconsentparams.R_SumAssured, objconsentparams.R_PolicyTerm, objconsentparams.R_BasePremium, objconsentparams.R_LoadedPremium, objconsentparams.R_TotalTax, 0, 0, 0, objconsentparams.RiderId, objconsentparams.ConsentStatus, objconsentparams.R_PremiumPaymentTerm, objconsentparams.ConsentCreatedBy, objconsentparams.Consentupdatedby, false, objconsentparams.Guid, "", "");
                                dtinsertclientdetials = objComm.InsertConsentDetails(strPolicyNo, "03", strApplicationno, objconsentparams.Ridername, objconsentparams.Proposer, objconsentparams.CustomerName, 0, objconsentparams.R_PolicyTerm, objconsentparams.R_BasePremium, objconsentparams.R_LoadedPremium, objconsentparams.R_TotalTax, 0, 0, 0, objconsentparams.RiderId, objconsentparams.ConsentStatus, objconsentparams.R_PremiumPaymentTerm, objconsentparams.ConsentCreatedBy, objconsentparams.Consentupdatedby, false, objconsentparams.Guid, "", "");
                            }
                            Logger.Info(strApplicationno + "Rider Details Insertion Success" + System.Environment.NewLine);
                        }
                    }
                    catch (Exception ex)
                    {

                        Logger.Info(strApplicationno + "Rider Details Insertion Failure" + "" + ex.Message.ToString() + System.Environment.NewLine);
                    }


                    #endregion Rider Section

                    #region Reduction OF sum Assured
                    dtrequirements = (DataTable)ViewState["dtrequirements"];
                    if (dtrequirements.Rows[0]["REQ_followUpCode"].ToString() == "COS" && dtrequirements.Rows[0]["REQ_status"].ToString() != "RS")
                    {

                        try
                        {
                            objemailparams.TemplateId = ConfigurationManager.AppSettings["ReductionOfSumAssured"].ToString().Trim();
                            objemailparams.IsAttached = "1";

                            #region BI generation 


                            //Existing Method used to pass PremiumCalculation service details to SIS Service
                            objcommfun.OnlineServicePremiumCalDetails_GET(ref _dsPremcal, strApplicationno, strChannelType);

                            //EzytekMethod Used to Generate BI
                            objPremcal.SISCalculation(strApplicationno, ref _ds, objChangeObj, _dsPremcal, ref strLApushErrorCode, ref strLApushStatus, ref BIResponse, ref dtclientdetails);
                            objemailparams.AttachedFiles = BIResponse;
                            RequestID = API_FetchRequestID(ConfigurationManager.AppSettings["COS_Process"].ToString().Trim(), objconsentparams.Guid);
                            Api_GetSerializedAttachments(RequestID, ConfigurationManager.AppSettings["ConsentFileName"].ToString().Trim(), objemailparams.AttachedFiles);
                            Logger.Info(strApplicationno + "Reduction Of SumAssured Success" + "RequestID :" + RequestID + System.Environment.NewLine);
                            #endregion
                            //Reduction of Sum Assured Template ID
                        }
                        catch (Exception ex)
                        {

                            Logger.Info(strApplicationno + "Reduction Of SumAssured Failue-1" + ex.Message.ToString() + System.Environment.NewLine);
                        }

                    }
                    #endregion
                    else
                    {
                        RequestID = API_FetchRequestID(ConfigurationManager.AppSettings["CounterOfferGenerated"].ToString().Trim(), objconsentparams.Guid);
                        Logger.Info(strApplicationno + "CounterOffer Success" + "RequestID :" + RequestID + System.Environment.NewLine);
                    }

                    ConsentRaised = true;

                }
                else
                {
                    ConsentRaised = false;
                    Logger.Info(strApplicationno + "Data Not Found" + System.Environment.NewLine);

                }
            }
            catch (Exception ex)
            {
                ConsentRaised = false;
                Logger.Info(strApplicationno + "ConsentLetter_Failure-1" + ex.Message.ToString() + System.Environment.NewLine);

            }


            Logger.Info(strApplicationno + "ConsentFollowUpRaised" + System.Environment.NewLine);

        }
        catch (Exception ex)
        {
            Logger.Info(strApplicationno + "ConsentLetter_Failure-2" + ex.Message.ToString() + System.Environment.NewLine);
            ConsentRaised = false;

        }

        return ConsentRaised;
    }

    private void ContentProductDetails(MasterPageComparision objMasterPageComparision)
    {
        List<ProductSection> lstProductSection = new List<ProductSection>();
        string strComboMonthlyPayout = string.Empty;
        string strProdMonthlyPayout = string.Empty;
        ProductSection objProductSectionBase = new ProductSection();
        ProductSection objProductSectionCombo = new ProductSection();
        TextBox txtPolterm = (TextBox)ContentPlaceHolder1.FindControl("txtPolterm");
        TextBox txtSumassure = (TextBox)ContentPlaceHolder1.FindControl("txtSumassure");
        TextBox txtProdcode = (TextBox)ContentPlaceHolder1.FindControl("txtProdcode");
        TextBox txtPrepayterm = (TextBox)ContentPlaceHolder1.FindControl("txtPrepayterm");
        DropDownList ddlFrequency = (DropDownList)ContentPlaceHolder1.FindControl("ddlFrequency");


        TextBox txtCombProdCode = (TextBox)ContentPlaceHolder1.FindControl("txtCombProdCode");
        TextBox txtCombPolTerm = (TextBox)ContentPlaceHolder1.FindControl("txtCombPolTerm");
        TextBox txtCombSumAssured = (TextBox)ContentPlaceHolder1.FindControl("txtCombSumAssured");
        TextBox txtCombPayTerm = (TextBox)ContentPlaceHolder1.FindControl("txtCombPayTerm");

        objProductSectionBase.PolicyTerm = Request.Form[txtPolterm.UniqueID];
        objProductSectionBase.PremiumFreq = ddlFrequency.SelectedValue;
        objProductSectionBase.SumAssured = Request.Form[txtSumassure.UniqueID];
        objProductSectionBase.ProductCode = txtProdcode.Text;
        objProductSectionBase.PremiumTerm = Request.Form[txtPrepayterm.UniqueID];
        lstProductSection.Add(objProductSectionBase);

        if (!string.IsNullOrEmpty(txtCombProdCode.Text))
        {
            objProductSectionCombo.PolicyTerm = Request.Form[txtCombPolTerm.UniqueID];
            objProductSectionCombo.PremiumFreq = ddlFrequency.SelectedValue;
            objProductSectionCombo.SumAssured = Request.Form[txtCombSumAssured.UniqueID];
            objProductSectionCombo.ProductCode = Request.Form[txtCombProdCode.UniqueID];
            objProductSectionCombo.PremiumTerm = Request.Form[txtCombPayTerm.UniqueID];
            lstProductSection.Add(objProductSectionCombo);
        }
        objMasterPageComparision.LstProductSection = lstProductSection;
    }
    #region API Methods
    public static byte[] ReadFully(Stream input)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }

    public void Api_GetSerializedAttachments(int RequestId, string strFileName, string BIFile)
    {
        try
        {

            var input = new
            {
                REQUESTID = RequestId,
                MEDIATYPE = ConfigurationManager.AppSettings["ConsentAttachmentType"].ToString(),
                ATTACHMENTNAME = strFileName,
                SRC = ConfigurationManager.AppSettings["AttachmentSource"].ToString(),
                ATTACHMENT = BIFile

            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(ConsentAPI + "/GetSerializedAttachments", inputContent).Result;

            if (response.IsSuccessStatusCode)
            {
                JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);

            }

        }
        catch (Exception ex)
        {
            Logger.Info(strApplicationno + "Api_GetSerializedAttachments Failure" + ex.Message.ToString() + System.Environment.NewLine);
        }
    }

    public int API_FetchRequestID(string Process, string Guid)
    {

        int RequestID = new int();
        try
        {
            var input = new
            {
                ProcessName = Process,
                GUID = Guid


            };
            string inputJson = (new JavaScriptSerializer()).Serialize(input);
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(ConsentAPI + "/SendCommunication", inputContent).Result;
            if (response.IsSuccessStatusCode)
            {
                RequestID = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
            }
        }
        catch (Exception ex)
        {

            Logger.Info(strApplicationno + "API_FetchRequestID Failure" + ex.Message.ToString() + System.Environment.NewLine);
        }
        return RequestID;
    }

    #endregion

    /************1.4 Begin of Changes CR 28153***********/
}