using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;

public partial class grpUW_GroupMasterPage : System.Web.UI.MasterPage
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //strUserGroup = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsUserGroup"]);
        //strApplicationno = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
        //strChannelType = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsChannelName"]);
        //strPolicyNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsPolicyNo"]);

        //if (!IsPostBack)
        //{
        //    // objCommonObj = (CommonObject)Session["objCommonObj"];
        //    objChangeObj = (ChangeValue)Session["objLoginObj"];
        //    Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :Page_Load" + System.Environment.NewLine);
        //    lblCaptionAppNo.Text = "Application Number: " + strApplicationno;
        //    lblCaptionPolNo.Text = "Policy Number: " + strPolicyNo;
        //    BindMenu(objChangeObj.userLoginDetails._UserGroup);
        //}
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

    //public static object DateFormat(object objInput)
    //{
    //    //if (object.ReferenceEquals(objInput, DBNull.Value))
    //    //{
    //    //    return null;
    //    //}
    //    //else
    //    //{
    //    //    if (string.IsNullOrEmpty(Convert.ToString(objInput)))
    //    //    {
    //    //        return null;
    //    //    }
    //    //    else
    //    //    {
    //    //        //DateTime dt = DateTime.ParseExact(Convert.ToString(objInput), "mm-dd-yyyy", CultureInfo.InvariantCulture);
    //    //        System.DateTime dt = Convert.ToDateTime(objInput);
    //    //        objInput = dt.ToString("dd/MM/yyyy");
    //    //        return objInput;
    //    //    }
    //    //}
    //}
    public event EventHandler contentCallEvent;
    public event EventHandler masterCallEvent;
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            //Logger.Info(strApplicationno + "master post Event Start" + System.Environment.NewLine);
            //lblPreIssueVal.Text = string.Empty;
            //MasterPageComparision objNewMasterComparision1 = new MasterPageComparision();
            //MasterPageComparision objNewMasterComparision2 = new MasterPageComparision();
            //ContentClientDetails(objNewMasterComparision1);
            //ContentRiderDetails(objNewMasterComparision1);
            //ContentProductDetails(objNewMasterComparision1);

            //System.Web.Script.Serialization.JavaScriptSerializer objJavaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //HiddenField hdnOldValue = (HiddenField)ContentPlaceHolder1.FindControl("hdnOldValue");
            //objNewMasterComparision2 = objJavaScriptSerializer.Deserialize<MasterPageComparision>(hdnOldValue.Value);
            //int intRef = -1;
            //string strUserId = string.Empty;
            //if (Session["objCommonObj"] != null)
            //{
            //    objCommonObj = (CommonObject)Session["objCommonObj"];
            //    strUserId = objCommonObj._Bpmuserdetails._UserID;
            //    objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_POST", false, ref intRef);
            //}
            ///*added by shri on 28 dec 17 to add tracking*/
            //int intTrackingRet = -1;
            //objCommonObj = (CommonObject)Session["objCommonObj"];
            //strUserId = objCommonObj._Bpmuserdetails._UserID;
            //InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "POST", ref intTrackingRet);
            ///*end here*/
            ///*END HERE*/
            //if (masterCallEvent != null)
            //{
            //    masterCallEvent(this, EventArgs.Empty);
            //}
            //int LAPushErrorCode = 0;
            //int UWDecisionResult = 0;
            //string strLAPushErrorMsg = string.Empty;
            //int Result = 0;
            //objCommonObj = (CommonObject)Session["objCommonObj"];
            //// objCommonObj = new CommonObject();
            //objChangeObj = (ChangeValue)Session["objLoginObj"];
            //struserid = objChangeObj.userLoginDetails._UserID;
            //// strUserGroup = objChangeObj.userLoginDetails._UserGroup;
            ////strApplicationno = objCommonObj._ApplicationNo;
            //strUWmode = strChannelType;
            //UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
            //Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnPost_Click" + System.Environment.NewLine);
            //DropDownList ddlUWDecesion = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWDecesion");
            //DropDownList ddlUWreason = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWreason");
            //DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPeriod");
            //Label lblErrorDecisiondtls = (Label)ContentPlaceHolder1.FindControl("lblErrorDecisiondtls");
            //string _strUWDecesion = ddlUWDecesion.SelectedValue;
            //string _strUWPeriod = ddlPostpone.SelectedValue == "0" ? "" : ddlPostpone.SelectedValue;
            //string _strDataValue = string.Empty;
            ////string _strPolicyStatus = string.Empty;
            //int intRetVal = -1;

            ///*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            //UpdateDecisionInLa(ref response);
            //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_POST", true, ref intRef);
            ///*END HERE*/
            ///*added by shri on 28 dec 17 to update tracking*/
            //UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            ///*END HERE*/
            //Logger.Info(strApplicationno + "master post Event End" + System.Environment.NewLine);
        }
        catch (Exception Error)
        {
            //Logger.Info(strApplicationno + "Exception In Post" + Error.Message + System.Environment.NewLine);
            //if (Error.Message.Contains("UDE-"))
            //{
            //    ShowPopupMessage(Error.Message.Replace("UDE-", string.Empty), 0);
            //}
            //else
            //{
            //    ShowPopupMessage("Details Not Post", 0);
            //}
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //Logger.Info(strApplicationno + "PAGE_NAME:UWSaralDecision/btnSave_Click //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution Begin" + System.Environment.NewLine);
            ///*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            //int intRef = -1;
            //string strUserId = string.Empty;
            //if (Session["objCommonObj"] != null)
            //{
            //    objCommonObj = (CommonObject)Session["objCommonObj"];
            //    strUserId = objCommonObj._Bpmuserdetails._UserID;
            //}
            //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_SAVE", false, ref intRef);
            ///*END HERE*/
            //Logger.Info("STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnSave_Click" + System.Environment.NewLine);
            //if (contentCallEvent != null)
            //    contentCallEvent(this, EventArgs.Empty);
            //objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_SAVE", true, ref intRef);
            //Logger.Info(strApplicationno + "PAGE_NAME:UWSaralDecision/btnSave_Click //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution End" + System.Environment.NewLine);
        }
        catch (Exception Error)
        {
            //Logger.Info(strApplicationno + "Exception In Save" + Error.Message + System.Environment.NewLine);
            //ShowPopupMessage("Erroe While Saving Application Details,Please Contact System Admin", 0);
        }
    }

    public void UpdateUWControlTable(string strProcessStatus, ref int strResult)
    {
        ////Change the status of DOCQC to Closed.
        //objChangeObj = (ChangeValue)Session["objLoginObj"];
        //// objCommonObj = (CommonObject)Session["objCommonObj"];
        //Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :UpdateUWControlTable" + System.Environment.NewLine);
        //int _SaveResult = 0;
        //objComm.ChangeUWSaralStatus(strApplicationno, strPolicyNo, strProcessStatus, "CLOSE", true, true, DateTime.Now, DateTime.Now, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, Convert.ToDateTime(objCommonObj._bpm_creationDate), Convert.ToDateTime(objCommonObj._bpm_systemDate), objCommonObj._bpm_systemDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, objCommonObj._bpm_applicationName, ref _SaveResult);
    }

    public void InsertUWControlTable(string strProcessStatus, ref int strResult)
    {
        ////Change the status of DOCQC to Closed.
        ////objCommonObj = (CommonObject)Session["objCommonObj"];
        //objChangeObj = (ChangeValue)Session["objLoginObj"];
        //Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :InsertUWControlTable" + System.Environment.NewLine);
        //int _SaveResult = 0;
        //objComm.ChangeUWSaralStatus(strApplicationno, strPolicyNo, strProcessStatus, "WIP", true, false, DateTime.Now, DateTime.Now, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, Convert.ToDateTime(objCommonObj._bpm_creationDate), Convert.ToDateTime(objCommonObj._bpm_systemDate), objCommonObj._bpm_systemDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, objCommonObj._bpm_applicationName, ref _SaveResult);
    }

    protected void btnsendToUW_Click(object sender, EventArgs e)
    {
        //int _SaveResult = 0;
        //// objCommonObj = (CommonObject)Session["objCommonObj"];
        //objChangeObj = (ChangeValue)Session["objLoginObj"];
        //strUserGroup = objChangeObj.userLoginDetails._UserGroup;
        //struserid = objChangeObj.userLoginDetails._UserID;
        //strUWmode = strChannelType;
        ////UpdateUWControlTable("DOCQC", ref _SaveResult);
        ////InsertUWControlTable("UW", ref _SaveResult);
        ////objComm.UpdateControlMechanism(objCommonObj._ApplicationNo, struserid, "1119763", "UWAssign", ref _SaveResult);
        //strAppstatusKey = (strUserGroup == "UW") ? "UR" : "DR";
        //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref _SaveResult);
        //Logger.Info("STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnSave_Click" + System.Environment.NewLine);
        //if (_SaveResult != -1)
        //{
        //    Response.Redirect("~/LoginPage.aspx");
        //}
        //else
        //{
        //    Logger.Error("STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnSave_Click : Error : UW Saral Status noot change" + System.Environment.NewLine);
        //}
    }

    public delegate void DoEvent();
    protected void btnManual_Click(object sender, EventArgs e)
    {
        //UpdateDecisionInLa(ref response);
    }

    private void UpdateDecisionInLa(ref int response)
    {
      //  //declare variable
      //  int LAPushErrorCode = 0;
      //  int UWDecisionResult = 0;
      //  string strLAPushErrorMsg = string.Empty;
      //  int Result = 0;
      //  Boolean isConsentReq = false;
      //  objCommonObj = (CommonObject)Session["objCommonObj"];
      //  objChangeObj = (ChangeValue)Session["objLoginObj"];
      //  struserid = objChangeObj.userLoginDetails._UserID;
      //  strUWmode = strChannelType;
      //  UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
      //  Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnPost_Click" + System.Environment.NewLine);
      //  DropDownList ddlUWDecesion = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWDecesion");
      //  DropDownList ddlUWreason = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWreason");
      //  DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPeriod");
      //  Label lblErrorDecisiondtls = (Label)ContentPlaceHolder1.FindControl("lblErrorDecisiondtls");

      //  if (objChangeObj.Load_Loadingdetails != null)
      //  {
      //      isConsentReq = objChangeObj.Load_Loadingdetails.isConsentRequired;
      //  }
      //  else
      //  {
      //      isConsentReq = false;
      //  }
      //  string _strUWDecesion = ddlUWDecesion.SelectedValue;
      //  string _strUWPeriod = ddlPostpone.SelectedValue == "0" ? "" : ddlPostpone.SelectedValue;
      //  string _strDataValue = string.Empty;
      //  int intRetVal = -1;

      //  //call servics
      //  objComm.OnlineUWDecisionDetails_Save(objCommonObj._AppType, strApplicationno, ddlUWDecesion.SelectedItem.Text, ddlUWreason.SelectedItem.Text, ddlUWDecesion.SelectedValue, ddlUWreason.SelectedItem.Value, _strUWPeriod, struserid, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, objCommonObj._bpm_creationDate,
      //objCommonObj._bpm_systemDate, objCommonObj._bpm_businessDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref UWDecisionResult);
      //  if (UWDecisionResult != -1)
      //  {
      //      string strConsentResponse = string.Empty;
      //      //lblErrorDecisiondtls.Text = "Decision details save successfully";
      //      objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
      //      if (!string.IsNullOrEmpty(strConsentResponse) && !strConsentResponse.Equals("Failed"))
      //      {
      //          Logger.Info("Consent Letter for application No : {0} : {1}" + strApplicationno + strConsentResponse);
      //          //string filePath = Request.QueryString["FILEPATH"].ToString();
      //          //Response.ContentType = "application/pdf";
      //          //Response.WriteFile(strConsentResponse);
      //          Response.Write("<script>");
      //          Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(strConsentResponse)));
      //          Response.Write("</script>");
      //          //WebClient myWebClient = new WebClient();
      //          // myWebClient.DownloadFile(strConsentResponse, "D:\abc.pdf");
      //          //Response.Redirect(strConsentResponse);
      //      }
      //      if (LAPushErrorCode == 0)
      //      {
      //          //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Details Post Successfully')", true);
      //          ShowPopupMessage("Details Post Successfully", 1);
      //          //lblErrorDecisiondtls.Text = "Decision Details Updated in Life Asia successfully";
      //          if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
      //          {
      //              //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
      //              //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);
      //              strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
      //              objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
      //              //_strPolicyStatus = "IF";
      //              objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);

      //              //To Insert Record in LF_applicationstatus table with '091' status 
      //              objBussiness.InsertAppStatus(strApplicationno, struserid, ref intRetVal);
      //          }
      //          else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
      //          {

      //              //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
      //              //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
      //              strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
      //              objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);

      //              //_strPolicyStatus = "DC";
      //              objBussiness.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);


      //          }
      //          else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
      //          {
      //              strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
      //              objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
      //              //_strPolicyStatus = "PO";
      //              objBussiness.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
      //          }
      //          else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
      //          {
      //              strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
      //              objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
      //              //_strPolicyStatus = "WD";
      //              objBussiness.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);
      //          }
      //          else if (ddlUWDecesion.SelectedValue == "proposal" && LAPushErrorCode == 0)
      //          {
      //              strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
      //              //objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
      //              //_strPolicyStatus = "WD";
      //              objBussiness.UpdatePolicyStatus(strApplicationno, "PS", struserid, ref intRetVal);
      //          }
      //          /*added by shri on 06-07-17 to close page on success*/
      //          //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show success message", "alert('Decision details save successfully');window.close();", true);
      //          /*end here*/
      //      }
      //      else
      //      {
      //          ShowPopupMessage(strLAPushErrorMsg, 0);
      //          /*commented and added by shri on 06-07-17 to close page on success*/
      //          //lblErrorDecisiondtls.Text = "Decision Details Not Updated in Life Asia,Please Contact system admin";
      //          //lblErrorDecisiondtls.Focus();
      //          //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert("+strLAPushErrorMsg+");", true);
      //          //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision Details Not Updated in Life Asia due to " + strLAPushErrorMsg+ ");", true);
      //          /*end here*/
      //      }
      //  }
      //  else
      //  {
      //      /*commented and added by shri on 06-07-17 to close page on success*/
      //      //lblErrorDecisiondtls.Text = "Decision details Not Save ,Please Contact system admin";
      //      //lblErrorDecisiondtls.Focus();
      //      Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision details Not Save ,Please Contact system admin');", true);
      //      /*end here*/
      //  }
    }

    protected void btnSystem_Click(object sender, EventArgs e)
    {
        ////int intRetVal = -1;
        ////string struserid = string.Empty;
        ////if (Session["objCommonObj"] != null)
        ////{
        ////    objCommonObj = (CommonObject)Session["objCommonObj"];
        ////    struserid = objCommonObj._Bpmuserdetails._UserID;
        ////    objComm.ManageApplicationLifeCycle(strApplicationno, struserid, "UW_DECISION_SYSTEM", false, ref intRetVal);
        ////}
        ////objBussiness.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);
        ////objComm.ManageApplicationLifeCycle(strApplicationno, struserid, "UW_DECISION_SYSTEM", true, ref intRetVal);
        //////Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Added to queue');window.close();", true);
        //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision details Not Save ,Please Contact system admin');", true);
    }

    private void ContentClientDetails(MasterPageComparision objMasterPageComparision)
    {
        //GridView gvClientDetails = (GridView)ContentPlaceHolder1.FindControl("gvClientDetails");
        //List<ClientSection> lstClientDetails = new List<ClientSection>();
        //foreach (GridViewRow rowfollowup in gvClientDetails.Rows)
        //{
        //    ClientSection objClientDetails = new ClientSection();
        //    //define variable                            
        //    //Label lblClientId = (Label)rowfollowup.FindControl("ClientId");
        //    //Label lblRole = (Label)rowfollowup.FindControl("Role");
        //    string lblClientId = rowfollowup.Cells[0].Text;
        //    string lblRole = rowfollowup.Cells[5].Text;

        //    objClientDetails.ClientId = lblClientId;
        //    objClientDetails.ClientRole = lblRole;
        //    lstClientDetails.Add(objClientDetails);
        //}
        //objMasterPageComparision.LstClientDetails = lstClientDetails;
    }

    private void ContentRiderDetails(MasterPageComparision objMasterPageComparision)
    {
        //GridView gvRiderDtls = (GridView)ContentPlaceHolder1.FindControl("gvRiderDtls");
        //List<RiderSection> lstRiderSection = new List<RiderSection>();
        //foreach (GridViewRow rowfollowup in gvRiderDtls.Rows)
        //{
        //    //define variable
        //    CheckBox cbIsRider = (CheckBox)rowfollowup.FindControl("chkremoveRider");

        //    if (cbIsRider.Checked)
        //    {
        //        Label lblRiderName = (Label)rowfollowup.FindControl("lblRiderName");
        //        RiderSection objRiderSection = new RiderSection();
        //        TextBox txtRiderSumAssured = (TextBox)rowfollowup.FindControl("txtRiderSumAssure");
        //        Label lblRiderCode = (Label)rowfollowup.FindControl("lblRiderCode");
        //        string[] strRiderSumAssured = txtRiderSumAssured.Text.Split(',');
        //        if (strRiderSumAssured.Length > 0)
        //        {
        //            txtRiderSumAssured.Text = strRiderSumAssured[strRiderSumAssured.Length - 1];
        //        }
        //        objRiderSection.RiderCode = lblRiderCode.Text;
        //        objRiderSection.RiderSumAssured = txtRiderSumAssured.Text;
        //        lstRiderSection.Add(objRiderSection);
        //    }
        //}
        //objMasterPageComparision.LstRiderSection = lstRiderSection;
    }

    private void ContentProductDetails(MasterPageComparision objMasterPageComparision)
    {
        //List<ProductSection> lstProductSection = new List<ProductSection>();
        //string strComboMonthlyPayout = string.Empty;
        //string strProdMonthlyPayout = string.Empty;
        //ProductSection objProductSectionBase = new ProductSection();
        //ProductSection objProductSectionCombo = new ProductSection();
        //TextBox txtPolterm = (TextBox)ContentPlaceHolder1.FindControl("txtPolterm");
        //TextBox txtSumassure = (TextBox)ContentPlaceHolder1.FindControl("txtSumassure");
        //TextBox txtProdcode = (TextBox)ContentPlaceHolder1.FindControl("txtProdcode");
        //TextBox txtPrepayterm = (TextBox)ContentPlaceHolder1.FindControl("txtPrepayterm");
        //DropDownList ddlFrequency = (DropDownList)ContentPlaceHolder1.FindControl("ddlFrequency");


        //TextBox txtCombProdCode = (TextBox)ContentPlaceHolder1.FindControl("txtCombProdCode");
        //TextBox txtCombPolTerm = (TextBox)ContentPlaceHolder1.FindControl("txtCombPolTerm");
        //TextBox txtCombSumAssured = (TextBox)ContentPlaceHolder1.FindControl("txtCombSumAssured");
        //TextBox txtCombPayTerm = (TextBox)ContentPlaceHolder1.FindControl("txtCombPayTerm");

        //objProductSectionBase.PolicyTerm = Request.Form[txtPolterm.UniqueID];
        //objProductSectionBase.PremiumFreq = ddlFrequency.SelectedValue;
        //objProductSectionBase.SumAssured = Request.Form[txtSumassure.UniqueID];
        //objProductSectionBase.ProductCode = txtProdcode.Text;
        //objProductSectionBase.PremiumTerm = Request.Form[txtPrepayterm.UniqueID];
        //lstProductSection.Add(objProductSectionBase);

        //if (!string.IsNullOrEmpty(txtCombProdCode.Text))
        //{
        //    objProductSectionCombo.PolicyTerm = Request.Form[txtCombPolTerm.UniqueID];
        //    objProductSectionCombo.PremiumFreq = ddlFrequency.SelectedValue;
        //    objProductSectionCombo.SumAssured = Request.Form[txtCombSumAssured.UniqueID];
        //    objProductSectionCombo.ProductCode = Request.Form[txtCombProdCode.UniqueID];
        //    objProductSectionCombo.PremiumTerm = Request.Form[txtCombPayTerm.UniqueID];
        //    lstProductSection.Add(objProductSectionCombo);
        //}
        //objMasterPageComparision.LstProductSection = lstProductSection;
    }


    private void ShowPopupMessage(string alertMessage, int strErrorCode)
    {
        //if (strErrorCode == 0)
        //{

        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('" + alertMessage + "');window.close();", true);
        //    //Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "ShowModalPopupClose('" + alertMessage + "');", true);
        //}
    }

    /*added by shri on 28 dec 17 to add tracking*/
    private void InsertUwDecisionTracking(string strApplicationNo, string strUserId, DateTime dtCurrentDateTime, string strEventName, ref int intRet)
    {
        //objComm = new Commfun();
        //objComm.InsertUwDecisionTracking(strApplicationNo, strUserId, dtCurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fff"), strEventName, ref intRet);
    }

    /*added by shri on 28 dec 17 to update tracking*/
    private void UpdateUwDecisionTracking(int intSrNo, DateTime dtEndDate, ref int intRet)
    {
        //objComm = new Commfun();
        //objComm.UpdateUwDecisionTracking(intSrNo, dtEndDate.ToString("yyyy-MM-dd HH:mm:ss:fff"), ref intRet);
    }
    /*end here*/
}
