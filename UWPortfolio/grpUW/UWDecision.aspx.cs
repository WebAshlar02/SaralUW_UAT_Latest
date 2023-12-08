using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using UWSaralObjects;
using UWSaralDecision;

public partial class grpUW_UWDecision : System.Web.UI.Page
{

    //<appSettings>
    //  <add key="DocUrlPath" value="http://Godavari:8060/GPPDocs/"/>
    //</appSettings>
    //<add name="groupFG" connectionString="Data source= UNIVERSE\SQL2012; Initial Catalog=FG_LF_GROUP_POLICY_UAT; user id=DEVELOPER_FG_LF_GROUP_POLICY; password=GroupP@123; Integrated Security=False" providerName="System.Data.SqlClient"/>
    public string strURL = System.Configuration.ConfigurationManager.AppSettings["DocUrlPath"];
    #region "Declaration"
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["groupFG"].ConnectionString);
    string strDBCS = ConfigurationManager.ConnectionStrings["groupFG"].ConnectionString;
    SqlConnection sqlCon = null;
    SqlCommand sqlCmd = null;
    SqlDataReader sqlDR = null;
    string MsgText = "";
    UWSaralDecision.BOUWDec objBOUWDec = new UWSaralDecision.BOUWDec();
    DataTable sqlDT_LETTER_TYPE = new DataTable();
    DataTable sqlDT_UW_DECISION = new DataTable();
    CommonObject objCommonObj = new CommonObject();
    string strUserId = string.Empty;
    string strUserGroup = string.Empty;
    int intUWUserKey = 0;
    UWSaralDecision.BOUWDashBoard objBOUWDB = new UWSaralDecision.BOUWDashBoard();
    #endregion

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["objLoginObj"] == null)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(grpUW_UWDecision), Page);
        //lblmsg.Visible = false;
        int memId = Convert.ToInt32(Request.QueryString["id"]);
        if (!IsPostBack)
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            intUWUserKey = objBOUWDB.GetUserKey(strUserId);
            //if (intUWUserKey > 0)
            hdnUWUserKey.Value = intUWUserKey.ToString();

            BindMemberData(memId, intUWUserKey);
            BindgvMedReqs(intUWUserKey);
            BindgvNonMedReqs(intUWUserKey);
            BindddlMemDocs(intUWUserKey);
            BindddlUnderwritersDecision(intUWUserKey);
        }
        if (IsPostBack)
        { }
    }

    #region "Binding Member Data To Controls(textboxes) For Members Policy Information"
    private void BindMemberData(int ID, int intUWUserKey)
    {
        MemberInfo memInfo = objBOUWDec.getMemInfo(ID, intUWUserKey);

        txtNameOfMember.Text = memInfo.CustomerName;
        txtNameOfSpouse.Text = memInfo.SpouseName;
        //txtDate.Text = Convert.ToString(memInfo.Date); 
        txtPolicyNo.Text = memInfo.PolicyCode;
        txtCompanyName.Text = memInfo.CompanyName;
        //txtReligareHousingFinance.Text = memInfo.ReligareHousingFinance; 
        txtEffectiveDate.Text = memInfo.EffectiveDate.ToString();
        txtApplicationNo.Text = memInfo.ApplicationNo;
        txtDateOfBirth.Text = memInfo.DateOfBirth.ToString();
        txtGender.Text = memInfo.Gender;
        txtTotalSumAssuredForLife.Text = memInfo.SumAssured;
        //txtMedicalCalledFor.Text = memInfo.MedicalCalledFor; 
        //txtFreeCoverLimitForLife.Text = memInfo.FreeCoverLimit;
        //txtMedicalSumAssuredForLife.Text = memInfo.MedicalSumAssuredForLife;    
        txtMemberNo.Text = memInfo.MemberNo;
        txtAging.Text = memInfo.Aging.ToString();
        //txtMemKey.Text = memInfo.MemberId.ToString();
        hdnMemKey.Value = memInfo.MemberId.ToString();
        hdnUwStatus.Value = memInfo.UwStatus.ToString();
        if (memInfo.UwStatus == 8)
        {
            hdnPostponeByMonth.Value = memInfo.PostponeByMonth.ToString();
            hdnUWDecisionDate.Value = memInfo.ReviewedOn.ToString();
        }
    }
    #endregion

    #region "Code For Gridview of Med Req Types"

    #region "Binding Gridview of Med Req Types"
    public void BindgvMedReqs(int intUWUserKey)
    {
        int MemKey = Convert.ToInt32(hdnMemKey.Value);
        gvMedReqs.DataSource = objBOUWDec.GetGridReqData("MED_REQ", MemKey, intUWUserKey);/* 789-> Med Req Type*/
        gvMedReqs.DataBind();
    }
    #endregion

    #region "Binding Gridview of Med Req Types"
    public void gvMedReqsRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row. 
            DropDownList ddlMedReqStatus = (e.Row.FindControl("ddlMedReqStatus") as DropDownList);
            ddlMedReqStatus.DataSource = objBOUWDec.GetDdlLkpData("REQ_STATUS", 0, intUWUserKey);
            ddlMedReqStatus.DataTextField = "LKP_CODE";
            ddlMedReqStatus.DataValueField = "LKP_VALUE";
            ddlMedReqStatus.DataBind();

            //Add Default Item in the DropDownList.
            ddlMedReqStatus.Items.Insert(0, new ListItem("Select Status", "-1"));

            //Select the status of member in DropDownList.
            string strReqStatus = (e.Row.FindControl("hdnMedReqStatus") as HiddenField).Value;
            ddlMedReqStatus.Items.FindByValue(strReqStatus).Selected = true;

            /*to assign requirement's previous comment as title to image*/
            Image imgMedReqRemarks = (e.Row.FindControl("imgMedReqRemarks") as Image);
            string strReqRemarks = (e.Row.FindControl("hdnMedReqRemarks") as HiddenField).Value;
            imgMedReqRemarks.Attributes.Add("title", strReqRemarks);
        }
    }
    #endregion

    #endregion

    #region "Code For Gridview of Non-Med Req Types"

    #region "Binding Non-Med Reqs gridview"
    public void BindgvNonMedReqs(int intUWUserKey)
    {
        int MemKey = Convert.ToInt32(hdnMemKey.Value);
        gvNonMedReqs.DataSource = objBOUWDec.GetGridReqData("NON_MED_REQ", MemKey, intUWUserKey);/* 790-> Non Med Req Type*/
        gvNonMedReqs.DataBind();
    }
    #endregion

    #region "Binding Gridview of Non-Med Req Types"
    public void gvNonMedReqsRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row.
            DropDownList ddlNonMedReqStatus = (e.Row.FindControl("ddlNonMedReqStatus") as DropDownList);
            ddlNonMedReqStatus.DataSource = objBOUWDec.GetDdlLkpData("REQ_STATUS", 0, intUWUserKey);
            ddlNonMedReqStatus.DataTextField = "LKP_CODE";
            ddlNonMedReqStatus.DataValueField = "LKP_VALUE";
            ddlNonMedReqStatus.DataBind();

            //Add Default Item in the DropDownList.
            ddlNonMedReqStatus.Items.Insert(0, new ListItem("Select Status", "-1"));

            //Select the status of member in DropDownList.
            string status = (e.Row.FindControl("hdnNonMedReqStatus") as HiddenField).Value;
            ddlNonMedReqStatus.Items.FindByValue(status).Selected = true;

            /*to assign requirement's previous comment as title to image*/
            Image imgNonMedReqRemarks = (e.Row.FindControl("imgNonMedReqRemarks") as Image);
            string strReqRemarks = (e.Row.FindControl("hdnNonMedReqRemarks") as HiddenField).Value;
            imgNonMedReqRemarks.Attributes.Add("title", strReqRemarks);
        }
    }
    #endregion

    #endregion

    #region "Binding Dropdown of UnderWriter Status"
    public void BindddlUnderwritersDecision(int intUWUserKey)
    {
        ddlUnderwritersDecision.DataSource = objBOUWDec.GetDdlLkpData("UW_STATUS", 0, intUWUserKey);
        ddlUnderwritersDecision.DataTextField = "LKP_DESC";
        ddlUnderwritersDecision.DataValueField = "LKP_VALUE";
        ddlUnderwritersDecision.DataBind();
        ddlUnderwritersDecision.Items.Insert(0, new ListItem("Select Status", "-1"));
    }
    #endregion

    #region "Binding Dropdown of Non-Med Requirement Status"
    [Ajax.AjaxMethod]
    public string getRequirements(int value, int intUWUserKey)
    {
        intUWUserKey = Convert.ToInt32(hdnUWUserKey.Value);
        List<DdlData> listDoc = new List<DdlData>();
        listDoc = objBOUWDec.GetDdlReqNameData(value, intUWUserKey);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(listDoc);
    }
    #endregion

    #region "Binding Dropdown for Displaying Docs"
    public void BindddlMemDocs(int intUWUserKey)
    {
        int MemKey = Convert.ToInt32(hdnMemKey.Value);
        ddlMemDocs.DataSource = objBOUWDec.GetDdlMemDocs(MemKey, intUWUserKey);
        ddlMemDocs.DataTextField = "NAME_OF_FILE";
        ddlMemDocs.DataValueField = "DOC_PATH";
        ddlMemDocs.DataBind();
        ddlMemDocs.Items.Insert(0, new ListItem("Select Document", "-1"));
        con.Close();
    }
    #endregion

    #region "method for adding data into sessions"
    [System.Web.Services.WebMethod(EnableSession = true)]//[Ajax.AjaxMethod]
    public static void btnSubmitData(List<Uw_Remarks> FurtherReq)
    {
        //btnSubmitData(List<Uw_Remarks> MedReq, List<Uw_Remarks> NonMedReq, List<Uw_Remarks> FurtherReq, Comments BottomComments)
        //bool var1 = true;
        //HttpContext.Current.Session["MedReq"] = MedReq;
        //HttpContext.Current.Session["NonMedReq"] = NonMedReq;
        HttpContext.Current.Session["FurtherReq"] = FurtherReq;
        //HttpContext.Current.Session["BottomComments"] = BottomComments;
        //JavaScriptSerializer js = new JavaScriptSerializer();
        //return js.Serialize(var1);
    }
    #endregion

    #region "get & set value to session in web method //verified on net//"
    //[System.Web.Script.Services.ScriptMethod()]
    //[System.Web.Services.WebMethod]
    //[System.Web.Services.WebMethod(EnableSession = true)]
    //public static void WebMethodForSessionDemo()
    //{
    //    //Setting a value to session variable
    //    HttpContext.Current.Session["VariableName"] = "Value";

    //    //Getting a value from session variable
    //    String value = (String)HttpContext.Current.Session["VariableName"];
    //}
    #endregion

    #region "get Rows Data of Further Req Grid using ajax method"
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
    public string getFurtherReqRowsData()
    {
        List<Uw_Remarks> FurtherReqs = new List<Uw_Remarks>();
        if (Session["FurtherReq"] != null)
        {
            FurtherReqs = (List<Uw_Remarks>)Session["FurtherReq"];
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(FurtherReqs);
    }
    #endregion

    #region "get Rows Data of Further Req Grid using ajax method"
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
    public string getFurtherReqErrorMsgData()
    {
        List<FurtherReqErrorMsg> lstFurtherReqErrorMsg = new List<FurtherReqErrorMsg>();
        if (Session["lstFurtherReqErrorMsg"] != null)
        {
            lstFurtherReqErrorMsg = (List<FurtherReqErrorMsg>)Session["lstFurtherReqErrorMsg"];
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(lstFurtherReqErrorMsg);
    }
    #endregion

    //#region "Server Side Validation By Ajax Method"
    //[Ajax.AjaxMethod]
    //public string validateFormConrols(string uw_Comments, int uw_Decision)
    //{
    //    string MsgText = "";
    //    Int32 rsltcount = 0;
    //    string patternUWC = @"^[a-zA-Z0-9 .,_-]*$"; //@"^[a-zA-Z ]{1}[a-zA-Z0-9 '.]*$";/^[a-zA-Z0-9 .,_-]+$/;
    //    //Underwriters Comments Validation  
    //    if (uw_Comments == "" || uw_Comments == null)
    //    {
    //        rsltcount += 1;
    //        MsgText += "Underwriter's Comment is Required.\n";
    //    }
    //    else if (uw_Comments != "" && uw_Comments != null)
    //    {
    //        Regex rgx = new Regex(patternUWC, RegexOptions.IgnoreCase);
    //        MatchCollection matches = rgx.Matches(uw_Comments);
    //        if (uw_Comments.Length > 500)
    //        {
    //            rsltcount += 1;
    //            MsgText += "Comments can have atmost 500 characters.\n";
    //        }
    //        else if (matches.Count == 0 && uw_Comments.Length < 500)
    //        {
    //            rsltcount += 1;
    //            MsgText += "Invalid UnderWriter Comments.\n";
    //        }
    //    }
    //    //Underwriters Decision Validation  
    //    if (uw_Decision == -1)
    //    {
    //        rsltcount += 1;
    //        MsgText += "Please Select Underwriters Decision Status.\n";
    //    }
    //    JavaScriptSerializer js = new JavaScriptSerializer();
    //    return js.Serialize(MsgText);
    //    //return rsltcount;
    //}
    //#endregion

    #region "getting Previous Comments"
    [Ajax.AjaxMethod]
    public string getPreviousComments(int MemKey, int intUWUserKey)
    {
        intUWUserKey = Convert.ToInt32(hdnUWUserKey.Value);
        List<Comments> listComments = new List<Comments>();
        listComments = objBOUWDec.GetPreviousComments(MemKey, intUWUserKey);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(listComments);
    }
    #endregion

    #region "Submit Buton Click Event"
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty(Request.Form["txtUnderwritersComments"])){uwComments = Request.Form["txtUnderwritersComments"];}
        //int MemKey = Convert.ToInt32(Request.Form["hdnMemKey"]);
        int MemKey = Convert.ToInt32(hdnMemKey.Value);
        intUWUserKey = Convert.ToInt32(hdnUWUserKey.Value);
        //int UserKey = Convert.ToInt32(hdnUWUserKey.Value); 
        int errorCnt = 0;

        /*Getting data from session*/
        //Comments comments = new Comments();
        Comments objComm = new Comments();
        List<Uw_Remarks> FurtherReq = new List<Uw_Remarks>();
        //List<Uw_Remarks> MedReq = new List<Uw_Remarks>();
        //List<Uw_Remarks> NonMedReq = new List<Uw_Remarks>();
        List<Uw_Remarks> MemRequirements = new List<Uw_Remarks>();
        if (Session["FurtherReq"] != null)
        {
            FurtherReq = (List<Uw_Remarks>)Session["FurtherReq"];
        }
        //if (Session["BottomComments"] != null)
        //{
        //    comments = (Comments)Session["BottomComments"];
        //}
        //if (Session["MedReq"] != null)
        //{
        //    MedReq = (List<Uw_Remarks>)Session["MedReq"];
        //}
        //if (Session["NonMedReq"] != null)
        //{
        //    NonMedReq = (List<Uw_Remarks>)Session["NonMedReq"];
        //}

        /*traversing through all checked rows of gvMedReqs for validation*/
        foreach (GridViewRow row in gvMedReqs.Rows)
        {
            //int abc = gvMedReqs.Rows.Count;
            if (((CheckBox)row.FindControl("CheckItem")).Checked)
            {
                TextBox txtDecision = (TextBox)row.FindControl("txtDecision");
                DropDownList ddlMedReqStatus = (DropDownList)row.FindControl("ddlMedReqStatus");
                int MrdKey = Convert.ToInt32(row.Cells[0].Text);
                string MrdDesc = row.Cells[2].Text;
                int RevStatus = Convert.ToInt32(ddlMedReqStatus.SelectedValue);
                string Remarks = txtDecision.Text;
                string ToolTipString = validateRemarks(Remarks);
                if (ToolTipString != "")
                {
                    txtDecision.Attributes.Add("title", ToolTipString);
                    txtDecision.Style.Add("Border", "1px Solid red");
                    errorCnt++;
                }
                else if (ToolTipString == "")
                {
                    txtDecision.Style.Add("Border", "1px Solid #ccc");
                    txtDecision.Attributes.Remove("title");
                }
                if (RevStatus == -1)
                {
                    ddlMedReqStatus.Attributes.Add("title", "Please Select Review Status.");
                    ddlMedReqStatus.Style.Add("Border", "1px Solid red");
                    errorCnt++;
                }
                else if (RevStatus <= 0 && RevStatus != -1)
                {
                    ddlMedReqStatus.Attributes.Add("title", "Invalid Review Status.");
                    ddlMedReqStatus.Style.Add("Border", "1px Solid red");
                    errorCnt++;
                }
                else if (RevStatus > 0)
                {
                    ddlMedReqStatus.Style.Add("Border", "1px Solid #ccc");
                    ddlMedReqStatus.Attributes.Remove("title");
                }
                if ((ToolTipString == "") && (RevStatus > 0))
                {
                    Uw_Remarks objRemarks = new Uw_Remarks();
                    objRemarks.MrdKey = MrdKey;
                    objRemarks.ReqTypeId = 0;
                    objRemarks.ReqType = "";
                    objRemarks.ReqNameId = 0;
                    objRemarks.ReqName = "";
                    objRemarks.Status = RevStatus;
                    objRemarks.Remarks = Remarks;
                    objRemarks.MemKey = MemKey;
                    //objRemarks.UserKey = UserKey;
                    MemRequirements.Add(objRemarks);
                }
            }
        }

        /*traversing through all checked rows of gvNonMedReqs  for validation*/
        foreach (GridViewRow row in gvNonMedReqs.Rows)
        {
            //int abc = gvMedReqs.Rows.Count;
            if (((CheckBox)row.FindControl("CheckItem")).Checked)
            {
                TextBox txtDecision = (TextBox)row.FindControl("txtDecision");
                DropDownList ddlNonMedReqStatus = (DropDownList)row.FindControl("ddlNonMedReqStatus");
                int MrdKey = Convert.ToInt32(row.Cells[0].Text);
                string MrdDesc = row.Cells[2].Text;
                int RevStatus = Convert.ToInt32(ddlNonMedReqStatus.SelectedValue);
                string Remarks = txtDecision.Text;
                string ToolTipString = validateRemarks(Remarks);
                if (ToolTipString != "")
                {
                    txtDecision.Attributes.Add("title", ToolTipString);
                    txtDecision.Style.Add("Border", "1px Solid red");
                    errorCnt++;
                }
                else if (ToolTipString == "")
                {
                    txtDecision.Style.Add("Border", "1px Solid #ccc");
                    txtDecision.Attributes.Remove("title");
                }
                if (RevStatus == -1)
                {
                    ddlNonMedReqStatus.Attributes.Add("title", "Please Select Review Status.");
                    ddlNonMedReqStatus.Style.Add("Border", "1px Solid red");
                    errorCnt++;
                }
                else if (RevStatus <= 0 && RevStatus != -1)
                {
                    ddlNonMedReqStatus.Attributes.Add("title", "Invalid Review Status.");
                    ddlNonMedReqStatus.Style.Add("Border", "1px Solid red");
                    errorCnt++;
                }
                else if (RevStatus > 0)
                {
                    ddlNonMedReqStatus.Style.Add("Border", "1px Solid #ccc");
                    ddlNonMedReqStatus.Attributes.Remove("title");
                }
                if ((ToolTipString == "") && (RevStatus > 0))
                {
                    Uw_Remarks objRemarks = new Uw_Remarks();
                    objRemarks.MrdKey = MrdKey;
                    objRemarks.ReqTypeId = 0;
                    objRemarks.ReqType = "";
                    objRemarks.ReqNameId = 0;
                    objRemarks.ReqName = "";
                    objRemarks.Status = RevStatus;
                    objRemarks.Remarks = Remarks;
                    objRemarks.MemKey = MemKey;
                    //objRemarks.UserKey = UserKey;
                    MemRequirements.Add(objRemarks);
                }
            }
        }

        /*traversing through all rows of FurtherReq for validation*/

        int RowIndex = 0;
        List<FurtherReqErrorMsg> lstFurtherReqErrorMsg = new List<FurtherReqErrorMsg>();
        //int lstFurtherReqErrorMsgCntr = 0;

        DataTable dtAllRequirements = objBOUWDec.FetchAllRequirements("ALL_REQ", intUWUserKey);

        foreach (var objRemarks in FurtherReq)
        {
            RowIndex++;

            //string errorMsg = "";
            string RequirementErrorMsg = "";
            string RequirementTypeErrorMsg = "";
            string RemarksErrorMsg = "";
            string LkpDescrption = "";
            string LkpCode = "";
            int Lkp_Key = 0;
            int Lkp_Type = 0;

            /*validating Underwriter's Remark*/
            if (objRemarks.Remarks == "" || objRemarks.Remarks == null)
            {
                RemarksErrorMsg = "Underwriter's Remark is Required <br> ";/* &#013;";*/
                errorCnt++;
            }
            else if (objRemarks.Remarks != "" && objRemarks.Remarks != null)
            {
                string patternUWRemarks = @"^[a-zA-Z0-9 .,_-]*$";//[a-zA-Z0-9 .,_-]
                //Regex rgx = new Regex(patternUWRemarks, RegexOptions.IgnoreCase);
                //MatchCollection matches = rgx.Matches(objRemarks.Remarks);
                var match = Regex.Match(objRemarks.Remarks, patternUWRemarks, RegexOptions.IgnoreCase);
                if (objRemarks.Remarks.Length > 500)
                {
                    RemarksErrorMsg = "Remarks can have atmost 500 characters <br> ";
                    errorCnt++;
                }
                else if (!match.Success && objRemarks.Remarks.Length < 500)
                {
                    RemarksErrorMsg = "Invalid Remarks  <br> ";
                    errorCnt++;
                }
            }

            /*validating Requirement Name*/
            if (objRemarks.ReqName == "" || objRemarks.ReqName == null)
            {
                RequirementErrorMsg += "Requirement Name is Required <br> ";
                errorCnt++;
            }
            else if (objRemarks.ReqName != "" && objRemarks.ReqName != null)
            {
                string patternReqName = @"^[a-zA-Z0-9 .,_-]*$";
                //Regex rgx = new Regex(patternUWRemarks, RegexOptions.IgnoreCase);
                //MatchCollection matches = rgx.Matches(objRemarks.Remarks);
                var match = Regex.Match(objRemarks.ReqName, patternReqName, RegexOptions.IgnoreCase);
                if (objRemarks.ReqName.Length > 500)
                {
                    RequirementErrorMsg = "Requirement Name can have atmost 500 characters <br> ";
                    errorCnt++;
                }
                else if (!match.Success)
                {
                    RequirementErrorMsg = "Invalid Requirement Name <br> ";
                    errorCnt++;
                }
                else if (match.Success && objRemarks.ReqName.Length < 500)
                {
                    DataRow[] drRequirements = dtAllRequirements.Select("LKP_DESC ='" + objRemarks.ReqName.ToString() + "'");
                    if (drRequirements.Length > 0)
                    {
                        foreach (DataRow drReq in drRequirements)
                        {
                            LkpDescrption = drReq["LKP_DESC"].ToString();
                            LkpCode = drReq["LKP_CODE"].ToString();
                            Lkp_Key = Convert.ToInt32(drReq["LKP_KEY"]);
                            Lkp_Type = Convert.ToInt32(drReq["LKP_TYPE"]);
                        }
                    }
                    else
                    {
                        RequirementErrorMsg = "Requirement Name is Invalid <br> ";
                        errorCnt++;
                    }
                }
            }

            /*validating Requirement Name ID*/
            if (objRemarks.ReqNameId == -1)
            {
                RequirementErrorMsg += "Requirement Name ID is Required <br> ";
                errorCnt++;
            }
            else if (objRemarks.ReqNameId <= 0 && objRemarks.ReqNameId != -1)
            {
                RequirementErrorMsg += "Requirement Name ID is Invalid <br> ";
                errorCnt++;
            }
            else if (objRemarks.ReqNameId > 0)
            {
                if (Lkp_Key != objRemarks.ReqNameId && Lkp_Key != 0)
                {
                    RequirementErrorMsg += "Requirement Name ID Does Not Match <br> ";
                    errorCnt++;
                }
            }


            /*validating Requirement Type*/
            if (objRemarks.ReqType == "" || objRemarks.ReqType == null)
            {
                RequirementTypeErrorMsg = "Requirement Type is Required <br> ";
                errorCnt++;
            }
            else if (objRemarks.ReqType != "" && objRemarks.ReqType != null)
            {
                if (objRemarks.ReqType != "Non-Medical")
                {
                    if (objRemarks.ReqType != "Medical")
                    {
                        RequirementTypeErrorMsg = "Requirement Type is Invalid <br> ";
                        errorCnt++;
                    }
                }
            }

            /*validating Requirement Type ID*/
            if (objRemarks.ReqTypeId == -1)
            {
                RequirementTypeErrorMsg += "Requirement Type is Required <br> ";
                errorCnt++;
            }
            else if (objRemarks.ReqTypeId <= 0 && objRemarks.ReqTypeId != -1)
            {
                RequirementTypeErrorMsg += "Requirement Type ID is Invalid <br> ";
                //RequirementTypeErrorMsg += "Requirement Type of Your Requirement is Invalid <br> ";
                errorCnt++;
            }
            else if (objRemarks.ReqTypeId > 0)
            {
                if (Lkp_Type != objRemarks.ReqTypeId && Lkp_Type != 0)
                {
                    RequirementTypeErrorMsg += "Requirement Type ID is Invalid <br> ";
                    //RequirementTypeErrorMsg += "Requirement Type of Your Requirement is Invalid <br> ";
                    errorCnt++;
                }
            }

            /*checking if any validation error*/
            if (RemarksErrorMsg != "" || RequirementErrorMsg != "" || RequirementTypeErrorMsg != "")
            {
                FurtherReqErrorMsg objFurtherReqErrorMsg = new FurtherReqErrorMsg();
                objFurtherReqErrorMsg.RowIndex = RowIndex;
                objFurtherReqErrorMsg.RequirementErrorMsg = RequirementErrorMsg;
                objFurtherReqErrorMsg.RequirementTypeErrorMsg = RequirementTypeErrorMsg;
                objFurtherReqErrorMsg.RemarksErrorMsg = RemarksErrorMsg;
                lstFurtherReqErrorMsg.Add(objFurtherReqErrorMsg);
            }
            else if (RemarksErrorMsg == "" && RequirementErrorMsg == "" && RequirementTypeErrorMsg == "")
            {
                Uw_Remarks objectRemarks = new Uw_Remarks();
                objectRemarks.MrdKey = objRemarks.MrdKey;
                objectRemarks.ReqTypeId = objRemarks.ReqTypeId;
                objectRemarks.ReqType = objRemarks.ReqType;
                objectRemarks.ReqNameId = objRemarks.ReqNameId;
                objectRemarks.ReqName = objRemarks.ReqName;
                objectRemarks.Status = objRemarks.Status;
                objectRemarks.Remarks = objRemarks.Remarks;
                objectRemarks.MemKey = MemKey;
                //objectRemarks.UserKey = UserKey;
                MemRequirements.Add(objectRemarks);
            }
        }

        if (lstFurtherReqErrorMsg.Count > 0)
        {
            Session["lstFurtherReqErrorMsg"] = lstFurtherReqErrorMsg;
        }

        /*getting values of comments section for validation*/

        sqlDT_LETTER_TYPE = objBOUWDec.GetLettersData("LETTER_TYPE", intUWUserKey);
        sqlDT_UW_DECISION = objBOUWDec.GetLettersData("UW_DECISION", intUWUserKey);

        string UnderwritersComments = txtUnderwritersComments.Text.Trim();
        string CMOsComments = txtCMOsComments.Text.Trim();
        string HODsComments = txtHODsComments.Text.Trim();
        int UWStatus = Convert.ToInt32(Request.Form[ddlUnderwritersDecision.UniqueID]);
        string RateUpPostponeText = txtRateUpPostpone.Text.Trim();
        string CommencementDate = dpCommencementDate.Text.Trim();
        string ToolTipUWStr = validateRemarks(UnderwritersComments);
        string ToolTipCMOStr = validateRemarks(CMOsComments == "" ? "test" : CMOsComments);
        string ToolTipHODStr = validateRemarks(HODsComments == "" ? "test" : HODsComments);
        DateTime Current_Financial_Year_Start_Date;
        DateTime dtCommencementDate;
        if (CommencementDate != "")
            dtCommencementDate = Convert.ToDateTime(CommencementDate);
        else
            dtCommencementDate = DateTime.Now;

        if (DateTime.Now.Month < 4)
        {
            Current_Financial_Year_Start_Date = new DateTime(DateTime.Now.Year - 1, 4, 1);
        }
        else
        {
            Current_Financial_Year_Start_Date = new DateTime(DateTime.Now.Year, 4, 1);
        }

        int RatedUpPostponeVal = 0;
        if ((UWStatus == 3 || UWStatus == 8) & RateUpPostponeText != "")
            RatedUpPostponeVal = Convert.ToInt32(RateUpPostponeText);

        if (ToolTipUWStr != "")
        {
            txtUnderwritersComments.Attributes.Add("title", ToolTipUWStr);
            txtUnderwritersComments.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (ToolTipUWStr == "")
        {
            txtUnderwritersComments.Style.Add("Border", "1px Solid #ccc");
            txtUnderwritersComments.Attributes.Remove("title");
        }
        if (ToolTipCMOStr != "")
        {
            txtCMOsComments.Attributes.Add("title", ToolTipCMOStr);
            txtCMOsComments.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (ToolTipCMOStr == "")
        {
            txtCMOsComments.Style.Add("Border", "1px Solid #ccc");
            txtCMOsComments.Attributes.Remove("title");
        }
        if (ToolTipHODStr != "")
        {
            txtHODsComments.Attributes.Add("title", ToolTipHODStr);
            txtHODsComments.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (ToolTipHODStr == "")
        {
            txtHODsComments.Style.Add("Border", "1px Solid #ccc");
            txtHODsComments.Attributes.Remove("title");
        }

        if (UWStatus == -1)
        {
            ddlUnderwritersDecision.Attributes.Add("title", "Please Select Review Status.");
            ddlUnderwritersDecision.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (UWStatus == 2 && CommencementDate == "")
        {
            dpCommencementDate.Attributes.Add("title", "Please Select Commencement Date.");
            dpCommencementDate.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (UWStatus == 2 && Current_Financial_Year_Start_Date > dtCommencementDate)
        {
            dpCommencementDate.Attributes.Add("title", "Select Commencement Date after " + string.Format("{0:MMMM d, yyyy}", Current_Financial_Year_Start_Date));
            dpCommencementDate.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (UWStatus == 3 && RateUpPostponeText == "")
        {
            txtRateUpPostpone.Attributes.Add("title", "Enter Rate Up Percentages.");
            txtRateUpPostpone.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (UWStatus == 8 && RateUpPostponeText == "")
        {
            txtRateUpPostpone.Attributes.Add("title", "Enter No of Months to Postpone.");
            txtRateUpPostpone.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (UWStatus == 3 && (RatedUpPostponeVal < 1 || RatedUpPostponeVal > 999))
        {
            txtRateUpPostpone.Attributes.Add("title", "Percentages Entered must be a Positive Number Less Than 999.");
            txtRateUpPostpone.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        else if (UWStatus == 8 && (RatedUpPostponeVal < 1 || RatedUpPostponeVal > 12))
        {
            txtRateUpPostpone.Attributes.Add("title", "Months Entered must be a Positive Number Less Than 12.");
            txtRateUpPostpone.Style.Add("Border", "1px Solid red");
            errorCnt++;
        }
        //else if (UWStatus == 3 && (RatedUpPostponeVal >= 1 && RatedUpPostponeVal <= 999))
        //{
        //    txtRateUpPostpone.Style.Add("Border", "1px Solid #ccc");
        //    txtRateUpPostpone.Attributes.Remove("title");
        //}
        else if (UWStatus != -1)
        {
            ddlUnderwritersDecision.Style.Add("Border", "1px Solid #ccc");
            ddlUnderwritersDecision.Attributes.Remove("title");
        }

        if ((UWStatus != -1) && (ToolTipHODStr == "") && (ToolTipCMOStr == "") && (ToolTipUWStr == "") && (errorCnt == 0))
        {
            //Comments objComm = new Comments();
            objComm.UWComment = UnderwritersComments;
            objComm.CMOComment = CMOsComments;
            objComm.HODComment = HODsComments;
            objComm.UWStatus = UWStatus;
            objComm.UWCommencementDate = dtCommencementDate;
            //objComm.UserKey = UserKey;
            objComm.MemKey = MemKey;
            if (UWStatus == 3)
                objComm.RateUPByPremium = RatedUpPostponeVal;
            else if (UWStatus == 8)
                objComm.PostponeByMonth = RatedUpPostponeVal;
        }

        string Lkp_Code = "";
        int LetType = 0;
        DataRow[] dr_UW_DECISION = sqlDT_UW_DECISION.Select("LKP_VALUE = " + UWStatus);
        if (dr_UW_DECISION.Length > 0)
        {
            Lkp_Code = dr_UW_DECISION[0]["LKP_CODE"].ToString();
        }
        switch (Lkp_Code)
        {
            //ADD_PREMIUM_CL, DECLINED_CL, POSTPONED_CL, MED_REQ_CL, UW_REQ_CL
            case "AS":
                LetType = getLetType("SCL");
                break;
            case "RU": //AR
                //LetType = getLetType("RUC");
                LetType = getLetType("ADD_PREMIUM_CL");
                break;
            case "P":
                //LetType = getLetType("PL");
                LetType = getLetType("POSTPONED_CL");
                break;
            case "D":
                //LetType = getLetType("DL");
                LetType = getLetType("DECLINED_CL");
                break;
            case "W":
                //LetType = getLetType("DL");
                LetType = getLetType("DECLINED_CL");
                break;
            case "FQ":
                //LetType = getLetType("AR");
                //LetType = getLetType("UW_REQ_CL");
                LetType = getLetType("MED_REQ_CL");
                break;
        }
        if (errorCnt == 0)
        {
            lblmsg.Text = "";
            lblmsg.Visible = false;

            string LetPath = "";
            DataTable dtMemRequirements = objBOUWDec.ToDataTable(MemRequirements);
            dtMemRequirements.Columns["UserKey"].SetOrdinal(0);
            dtMemRequirements.Columns["MemKey"].SetOrdinal(1);
            dtMemRequirements.Columns["MrdKey"].SetOrdinal(2);
            dtMemRequirements.Columns["ReqTypeId"].SetOrdinal(3);
            dtMemRequirements.Columns["ReqType"].SetOrdinal(4);
            dtMemRequirements.Columns["ReqNameId"].SetOrdinal(5);
            dtMemRequirements.Columns["ReqName"].SetOrdinal(6);
            dtMemRequirements.Columns["Status"].SetOrdinal(7);
            dtMemRequirements.Columns["Remarks"].SetOrdinal(8);

            string result = objBOUWDec.manageComments(objComm, dtMemRequirements, LetType, LetPath, intUWUserKey);
            if (result == "SUCCESS")
            {
                ClientScript.RegisterStartupScript(
                    Page.GetType()
                    , "alert"
                    , "alert('Data Submited Sucessfully');window.location='GrpDashBoard.aspx';"
                    , true);
            }
            else
            {
                lblFlag.Text = "Error";
                ClientScript.RegisterStartupScript(
                    Page.GetType()
                    , "alert"
                    , "alert('Something Went Wrong!!');"
                    , true);
            }
        }
        else
        {
            lblFlag.Text = "Error";
        }
    }
    #endregion

    #region "Server Side Validation"
    //public int validate(string uw_Comments, string uw_Decision)
    //{
    //    Int32 rsltcount = 0;
    //    string patternUWC = @"^[a-zA-Z0-9 .,_-]*$"; //@"^[a-zA-Z ]{1}[a-zA-Z0-9 '.]*$";
    //    //Underwriters Comments Validation  
    //    if (uw_Comments == "" || uw_Comments == null)
    //    {
    //        rsltcount += 1;
    //        MsgText += "Underwriter's Comment is Required.\n";
    //    }
    //    else if (uw_Comments != "" && uw_Comments != null)
    //    {
    //        Regex rgx = new Regex(patternUWC, RegexOptions.IgnoreCase);
    //        MatchCollection matches = rgx.Matches(uw_Comments);
    //        if (uw_Comments.Length > 500)
    //        {
    //            rsltcount += 1;
    //            MsgText += "Comments can have atmost 500 characters.\n";
    //        }
    //        else if (matches.Count == 0 && uw_Comments.Length < 500)
    //        {
    //            rsltcount += 1;
    //            MsgText += "Invalid UnderWriter Comments.\n";
    //        }
    //    }
    //    //Underwriters Decision Validation  
    //    if (uw_Decision == "-1")
    //    {
    //        rsltcount += 1;
    //        MsgText += "Please Select Underwriters Decision Status.\n";
    //    }
    //    return rsltcount;
    //}

    public string validateRemarks(string uwRemarks)
    {
        //Int32 rsltcount = 0;
        string errorText = "";
        string patternUWC = @"^[a-zA-Z0-9 .,_-]*$"; //@"^[a-zA-Z ]{1}[a-zA-Z0-9 '.]*$"; /^[a-zA-Z0-9 .,_-]+$/;
        //Underwriters Comments Validation  
        if (uwRemarks == "" || uwRemarks == null)
        {
            //rsltcount += 1;
            errorText += "Underwriter's Comment is Required.\n";
        }
        else if (uwRemarks != "" && uwRemarks != null)
        {
            Regex rgx = new Regex(patternUWC, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(uwRemarks);
            if (uwRemarks.Length > 500)
            {
                //rsltcount += 1;
                errorText += "Comments can have atmost 500 characters.\n";
            }
            else if (matches.Count == 0 && uwRemarks.Length < 500)
            {
                //rsltcount += 1;
                errorText += "Invalid Comment.\n";
            }
        }
        return errorText;
    }
    #endregion

    public int getLetType(string LkpCode)
    {
        int LetType = 0;
        DataRow[] dr_LETTER_TYPE = sqlDT_LETTER_TYPE.Select("LKP_CODE ='" + LkpCode.ToString() + "'");//Select(string.Format("Cinema_strID = '{0}'", "ABIC")
        if (dr_LETTER_TYPE.Length > 0)
        {
            LetType = Convert.ToInt32(dr_LETTER_TYPE[0]["LKP_KEY"]);
        }
        return LetType;
    }

}