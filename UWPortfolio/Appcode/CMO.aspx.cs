using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using System.Web.Configuration;
using System.Web.Caching;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public partial class Appcode_CMO : System.Web.UI.Page
{
    string strApplicationno = string.Empty;
    string strChannelType = string.Empty;
    string strUserId = string.Empty;
    string strUWmode = string.Empty;
    string strUserGroup = string.Empty;
    string strOptinselected = string.Empty;
    string strAppstatusKey = string.Empty;
    string strPolicyNo = string.Empty;
    string strConsentRespons = string.Empty;
    Commfun objComm = new Commfun();
    BussLayer objBuss = new BussLayer();
    DataSet _ds = new DataSet();
    DataSet dsMSAR = new DataSet();
    DataTable _Dt = new DataTable();
    DataSet _dsAgentdetails = new DataSet();
    DataSet _dsBank = new DataSet();
    DataSet _dsPrevPol = new DataSet();
    DataSet _dsJournal = new DataSet();
    DataSet _dsFollowuo = new DataSet();
    DataSet _dsProdontrol = new DataSet();
    DataSet _dsUWDisplayOption = new DataSet();
    DataSet _dsAppdtls = new DataSet();
    DataSet _dsPandtls = new DataSet();
    DataSet _dsTsarMsarDtls = new DataSet();
    DataSet _dsProdDtls = new DataSet();
    DataTable _dsRiderDtls = new DataTable();
    DataSet _dsFundsDtls = new DataSet();
    DataSet _dscommentDtls = new DataSet();
    DataSet _dsPymentsDtls = new DataSet();
    DataSet _dsRequrmentDtls = new DataSet();
    DataSet _dsReceiptDtls = new DataSet();
    DataSet _dsLoadingDtls = new DataSet();
    DataSet _dsReportDtls = new DataSet();
    DataSet _dsUWMaster = new DataSet();
    DataSet _dsPageData = new DataSet();
    DataSet _dsDashcount = null;
    DataSet _dsQuestion = new DataSet();
    string response = "";
    UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
    CommonObject objCommonObj = new CommonObject();
    ChangeValue objChangeObj = new ChangeValue();
    Commfun objcomm = new Commfun();
    int strLApushErrorCode;
    string strLApushStatus;
    string clsName = "";
    decimal ServiceTax;
    bool HighRisk;
    bool IsCommentSave;
    int IsCommentStore;
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.FindControl("btnPost").Visible = false;
        Master.FindControl("btnReview").Visible = false;
        Master.FindControl("btnReferToCmo").Visible = false;
        Master.FindControl("btnDms").Visible = false; 
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        if (Request.QueryString["qsAppNo"] != null)
        {
            //strApplicationno = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
            strApplicationno = Request.QueryString["qsAppNo"];
        }
        if (Request.QueryString["qsChannelName"] != null)
        {
            strChannelType = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsChannelName"]);
            //strChannelType = Request.QueryString["qsChannelName"];
        }
        FillCommentsDetails(strApplicationno, strChannelType, ddlCommentsearch.SelectedValue);
    }
    protected void btnCommonEvent_Command(object sender, CommandEventArgs e)
    {
        //if (Session["objLoginObj"] != null)
        //{
        //    objChangeObj = (ChangeValue)Session["objLoginObj"];
        //    int intRetVal = -1;
        //    objcomm.MaintainApplicationLog(strApplicationno, e.CommandName, string.Empty, objChangeObj.userLoginDetails._UserID, ref intRetVal);
        //}
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }
    protected void btnUWCommSave_Click(object sender, EventArgs e)
    {
        SaveUwComment();
    }
    protected void ddlCommentsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCommentsDetails(strApplicationno, strChannelType, ddlCommentsearch.SelectedValue);
    }
    private void SaveUwComment()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnUWCommSave_Click");
        int UWCommentResult = 0;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        int intTrackingRet = -1;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        int intRef = -1;
        objComm.OnlineUWCommentsDetails_Save(objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._UserGroup, Convert.ToString("CMO Comments :"+txtUWComments.Value), strApplicationno, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ddlComment.SelectedValue, ref UWCommentResult);
        if (UWCommentResult != 0)
        {
            ddlComment.SelectedValue = "General";
            txtUWComments.Value = "";
            FillCommentsDetails(strApplicationno, strChannelType, ddlComment.SelectedValue);
            lblErrorcommentdtls.Text = "Comment details saved successfully";
            //lblErrorLoadingDetailBody.Text = "Comment details saved successfully";
        }
        else
        {
            lblErrorcommentdtls.Text = "Comment details Not Saved CLICK To Know More";
            //lblErrorLoadingDetailBody.Text = "Comment details Not Saved In DataBase";
        }
        chkCommentDtls.Checked = false;
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        /*END HERE*/
    }
    public void FillCommentsDetails(string strApplicationno, string ChannelType, string strCommenttype)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillCommentsDetails" + System.Environment.NewLine);
        objComm.OnlineCommentsDisplayDetails_GET(ref _dscommentDtls, strApplicationno, ChannelType, strCommenttype);
        BindCommentsDetails(_dscommentDtls.Tables[0]);
        clsName = divExistingComments.Attributes["class"].ToString();
        divExistingComments.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
    }
    public void BindCommentsDetails(DataTable _dscommentDtls)
    {
        _dscommentDtls.Columns.RemoveAt(0);
        if (_dscommentDtls.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindCommentsDetails" + System.Environment.NewLine);
            gvUWComments.DataSource = _dscommentDtls;
            gvUWComments.DataBind();
        }
    }
    protected void btnAddComment_Click(object sender, EventArgs e)
    {
        try
        {
            AutoComment objAutoComment = new AutoComment();
            objBuss = new BussLayer();
            objBuss.FetchAutoCommentDetails(strApplicationno, strChannelType, ref _ds);
            BindCommentSection(objAutoComment, _ds);
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref dsMSAR, ref _dsPrevPol, "MSAR", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            BindSarDetails(objAutoComment, dsMSAR);
            txtUWComments.Value = objAutoComment.SummaryComment;
        }
        catch (Exception ex)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    private void BindCommentSection(AutoComment objAutoComment, DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            objAutoComment.TotalPremium = Convert.ToString(_ds.Tables[0].Rows[0]["TOTAL_PREMIUM"]);
            objAutoComment.NameOfLa = Convert.ToString(_ds.Tables[0].Rows[0]["LA_NAME"]);
            objAutoComment.Gender = Convert.ToString(_ds.Tables[0].Rows[0]["GENDER"]);
            objAutoComment.Age = Convert.ToString(_ds.Tables[0].Rows[0]["AGE"]);
            objAutoComment.Education = Convert.ToString(_ds.Tables[0].Rows[0]["EDUCATION"]);
            objAutoComment.Occupation = Convert.ToString(_ds.Tables[0].Rows[0]["OCCUPATION"]);
            objAutoComment.AnnualIncome = Convert.ToString(_ds.Tables[0].Rows[0]["ANNUAL_INCOME"]);
            objAutoComment.Nominee = Convert.ToString(_ds.Tables[0].Rows[0]["NOMINEE_RELATION"]);
            objAutoComment.Proposer = Convert.ToString(_ds.Tables[0].Rows[0]["PROPOSAR_NAME"]);
            objAutoComment.Bmi = Convert.ToString(_ds.Tables[0].Rows[0]["BMI"]);
            objAutoComment.NatureOfDuty = Convert.ToString(_ds.Tables[0].Rows[0]["NATURE_OF_DUTY"]);
            objAutoComment.TypeOfIncomeProof = Convert.ToString(_ds.Tables[0].Rows[0]["INCOME_PROOF"]);
            objAutoComment.IdProof = Convert.ToString(_ds.Tables[0].Rows[0]["ID_PROOF"]);
            objAutoComment.AddressProof = Convert.ToString(_ds.Tables[0].Rows[0]["ADDR_PROOF"]);
            objAutoComment.AgeProof = Convert.ToString(_ds.Tables[0].Rows[0]["AGE_PROOF"]);
            objAutoComment.FamilyHistory = Convert.ToString(_ds.Tables[0].Rows[0]["FAMILY_HISTORY"]);
            objAutoComment.PersonalMedicalHistory = Convert.ToString(_ds.Tables[0].Rows[0]["PERSONAL_MEDICAL_HISTORY"]);
            //Added by Ajay sahu for auto comment format
            objAutoComment.PlanName = Convert.ToString(_ds.Tables[0].Rows[0]["PLAN_NAME"]);
            objAutoComment.SumAssured = Convert.ToString(_ds.Tables[0].Rows[0]["SUM_ASSURED"]);
            objAutoComment.PolicyTerm = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_TERM"]);
            objAutoComment.PolicyPayingTerm = Convert.ToString(_ds.Tables[0].Rows[0]["PLOLICY_PAYING_TERM"]);
            objAutoComment.MaritualStatus = Convert.ToString(_ds.Tables[0].Rows[0]["MARITAL_STATUS"]);
            objAutoComment.BranchCode = Convert.ToString(_ds.Tables[0].Rows[0]["BRANCH_CODE"]);
            objAutoComment.RiskScore = Convert.ToString(_ds.Tables[0].Rows[0]["RISK_SCORE"]);
            objAutoComment.EYRiskScore = Convert.ToString(_ds.Tables[0].Rows[0]["ENY_VALUE"]);
            objAutoComment.ChannelName = Convert.ToString(_ds.Tables[0].Rows[0]["CHANNEL_NAME"]);
            string existsins = string.Empty;
            if (_ds.Tables.Count > 0 && _ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < _ds.Tables[1].Rows.Count; i++)
                {
                    var stringArr = _ds.Tables[1].Rows[i].ItemArray.Select(x => x.ToString()).ToArray();
                    existsins += Environment.NewLine + string.Join("/", stringArr.ToArray());

                }
            }
            objAutoComment.ExistingIns = existsins;
            //Added by suraj on 04/09/2018 as discussed with shabbir sir -display total SA(existing insurance+current) for HHI and Non HHI in auto comment
            if (_ds.Tables.Count > 2 && _ds.Tables[2].Rows.Count > 0)
            {
                objAutoComment.TOTALSA_HHI = Convert.ToString(_ds.Tables[2].Rows[0]["HHI_TSA"]);
                objAutoComment.TOTALSA_NON_HHI = Convert.ToString(_ds.Tables[2].Rows[0]["NON_HHI_TSA"]);
            }
        }
    }

    private void BindSarDetails(AutoComment objAutoComment, DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            DataRow[] dr = _ds.Tables[0].Select("ClientRole='LA'");
            if (dr != null && dr.Length > 0)
            {
                //objAutoComment.TMSAR = Convert.ToString(dr[0]["TSAR"]);
                objAutoComment.TMSAR = Convert.ToString(dr[0]["MSAR"]);
                objAutoComment.FSAR = Convert.ToString(dr[0]["Fsar"]);
                objAutoComment.TSAR = Convert.ToString(dr[0]["Tsar"]);

            }

        }
        else
        {
            objAutoComment.TMSAR = "";
            objAutoComment.FSAR = "";
            objAutoComment.TSAR = "";
        }
    }


    protected void btnsbmit_Click(object sender, EventArgs e)
    {
        int UWResolveResult = 0;
        objComm.UWCMOFoloupcdRS(strApplicationno,ref UWResolveResult);
        //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "closePage", "window.close();", true);
    }

    protected void btnViewDoc_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("DMSVeiwer.aspx?ApplnNo={0}&DocType={1}", strApplicationno,"Medical"), false);
        //string url = "MedicalDataEntryNew.aspx?ApplnNo=" + strApplicationno + "&DocType=Medical";
        string url = "DMSVeiwer.aspx?ApplnNo=" + strApplicationno + "&DocType=Medical";
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
    }
}
