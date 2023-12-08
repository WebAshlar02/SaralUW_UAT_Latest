using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Platform.Utilities.LoggerFramework;
using System.Configuration;
public partial class DashboardOnline : System.Web.UI.Page
{
    #region Variable Declaration Begins.
    DataSet _ds = new DataSet();
    BussLayer ObjBuss = new BussLayer();
    string strUserId = string.Empty;
    string strUWmode = string.Empty;
    string strUserGroup = string.Empty;
    string strApplicationno = string.Empty;
    string strOptinselected = string.Empty;
    string strAppstatusKey = string.Empty;
    public string strPolicyEnquiery = string.Empty;
    int Result = 0;
    DataSet _dsDashcount = null;
    Commfun objComm = new Commfun();
    CommonObject objCommonObj = new CommonObject();
    Boolean IsPageRefresh;
    #endregion Variable declaration End.

    #region Event Declaration Begins.

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["objLoginObj"] == null)
        {
            Response.Redirect("~/9011042143.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Logger.Info("New Journey Begins At ****************" + DateTime.Now + "***************" + System.Environment.NewLine);
        Logger.Info(":PageName :Default.aspx // MethodeName :Page_Load" + System.Environment.NewLine);
        //Added by Suraj on 01-Nov-2018 -wanted to restrict copy and paste of URL to open the page
        string strPreviousPage = "";
        if (Request.UrlReferrer != null)
        {
            strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        }
        if (strPreviousPage == "")
        {
            Response.Redirect("~/9011042143.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                int intRef = -1;
                strPolicyEnquiery = ConfigurationSettings.AppSettings["PolicyEnquiry"];
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                objCommonObj = (CommonObject)Session["objCommonObj"];
                objCommonObj._ChannelType = "ONLINE";
                Session["objCommonObj"] = objCommonObj;
                strUWmode = "ONLINE";
                strOptinselected = "FRESH";
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                spanusername.InnerText = objCommonObj._Bpmuserdetails._UserName;
                //////objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "DAHS_LOAD", false, ref intRef);
                //GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
                DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
                //////objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "DAHS_LOAD", true, ref intRef);
            }
            catch (Exception EX)
            {

            }
            //DashbordBucketsCountDynamic_Get(strUserId, strUserGroup, strUWmode);
        }
        //else
        //{
        //    if (ViewState["postids"].ToString() != Session["postid"].ToString())
        //    {
        //        IsPageRefresh = true;
        //    }
        //    Session["postid"] = System.Guid.NewGuid().ToString();
        //    ViewState["postids"] = Session["postid"].ToString();
        //}
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);

    }

    protected void lnkOnline_Click(object sender, EventArgs e)
    {
        try
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "LoaderPop();", true);
            objCommonObj = (CommonObject)Session["objCommonObj"];
            objCommonObj._ChannelType = "ONLINE";
            Session["objCommonObj"] = objCommonObj;
            strOptinselected = "FRESH";
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            strUWmode = "ONLINE";
            //GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    protected void lnkoffline_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Default3.aspx", true);
            //objCommonObj = (CommonObject)Session["objCommonObj"];
            //objCommonObj._ChannelType = lnkoffline.Text;
            //Session["objCommonObj"] = objCommonObj;
            //strOptinselected = "FRESH";
            //strUserId = objCommonObj._Bpmuserdetails._UserID;
            //strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            //strUWmode = "OFFLINE";
            //GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
        }
        catch (Exception EX)
        {

        }
    }

    protected void gvAppdasbord_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkApplicationNo = (LinkButton)row.FindControl("lnkAppno");
                HiddenField hdnProductName = (HiddenField)row.FindControl("hdnProductName");                
                Label lblPolicyNo = (Label)row.FindControl("lblpolno");
                string strApplicationno = lnkApplicationNo.Text;
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                objCommonObj._ApplicationNo = strApplicationno;
                objCommonObj._ChannelType = "ONLINE";
                objCommonObj._PolicyNo = lblPolicyNo.Text;
                objCommonObj._ProductName = hdnProductName.Value;
                Session["objCommonObj"] = objCommonObj;
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                int intRef = -1;
                //////objComm.ManageApplicationLifeCycle(lnkApplicationNo.Text, strUserId, "APP_SELECT", false, ref intRef);
                /*END HERE*/
                #region Control Logic Begin.
                strAppstatusKey = (strUserGroup == "UW") ? "UL" : "DL";
                objComm.OnlineApplicationLockChecker(strApplicationno, strUserId, strUserGroup, ref _ds);
                if (_ds != null && _ds.Tables[0].Rows.Count > 0)
                {
                    objComm.OnlineApplicationchangeStatus(strApplicationno, strUserId, strAppstatusKey, "", ref Result);
                    if (Result != 0 || Result != -1)
                    {
                        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('www.google.com','_blank');", true);
                        Response.Redirect(string.Format("Appcode/Uwdecision.aspx?qsUserGroup={0}&qsAppNo={1}&qsChannelName={2}&qsPolicyNo={3}",
                            UWSaralDecision.CommFun.Base64EncodingMethod(objCommonObj._Bpmuserdetails._UserGroup), 
                            //UWSaralDecision.CommFun.Base64EncodingMethod(strApplicationno),
                            strApplicationno,
                            UWSaralDecision.CommFun.Base64EncodingMethod("ONLINE"), 
                            UWSaralDecision.CommFun.Base64EncodingMethod(lblPolicyNo.Text)),
                            false);
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#myModal1').modal();window.close();alert('test message');", true);
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "AlreadyOpenMsg();", true);
                }
                #endregion Control Logic End.
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                //////objComm.ManageApplicationLifeCycle(lnkApplicationNo.Text, strUserId, "APP_SELECT", true, ref intRef);
                /*END HERE*/

            }
            else if (e.CommandName == "PendingDocs")
            {
                DataSet dsPendingDocs = new DataSet();
                RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
                LinkButton hdnApplicationNo = (LinkButton)row.FindControl("lnkAppno");
                Literal lblapp = (Literal)row.FindControl("lblapp");
                UserControl_PopupDoc WebUserControl1 = (UserControl_PopupDoc)FindControl("PendingDocs");
                if (!string.IsNullOrEmpty(hdnApplicationNo.Text))
                {
                    int response = 0;
                    WebUserControl1.FillPendingDoc(hdnApplicationNo.Text, "", ref response);
                    if (response == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#divPendingDocPopup').modal('show');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#myModalNoDocPending').modal();", true);
                    }
                }

            }
        }
        catch (Exception EX)
        {

        }

    }
    protected void lnkCommondashbord_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOptinselected = (LinkButton)sender;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strOptinselected = lnkOptinselected.Text;
            switch (lnkOptinselected.Text.ToUpper())
            {
                case "FRESH CASES":
                    {
                        strOptinselected = "FRESH";
                        break;
                    }

                case "UWREFER CASES":
                    {
                        strOptinselected = "REFER";
                        break;
                    }

                case "PENDING":
                    {
                        strOptinselected = "PENDING";
                        break;
                    }

                case "DOCUMENTS VERIFIED":
                    {
                        strOptinselected = "DOCS";
                        break;
                    }

                case "PAYMENT REALIZED":
                    {
                        strOptinselected = "PAYMENT";
                        break;
                    }

                case "READY TO ISSUE":
                    {
                        strOptinselected = "RDYTOISU";
                        break;
                    }

                case "RESOLVE":
                    {
                        strOptinselected = "RESOLVE";
                        break;
                    }

                case "SIGN OFF":
                    {
                        strOptinselected = "SIGNOFF";
                        break;
                    }
                case "PIVC Pending":
                    {
                        strOptinselected = "PIVC_PENDING";
                        break;
                    }
                case "Pivc & Realization Complete":
                    {
                        strOptinselected = "PIVC_COMPLETE_REALIZATION_COMPLETE";
                        break;
                    }
                case "Pivc Complete Realization Pending":
                    {
                        strOptinselected = "PIVC_COMPLETE_REALIZATION_PENDING";
                        break;
                    }
                case "DOCQC":
                    {
                        strOptinselected = "DOCQC";
                        break;
                    }
                case "FORM FILLING PENDING":
                    {
                        strOptinselected = "FORM_FILLING_PENDING";
                        break;
                    }
                default:
                    break;
            }

            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            strUWmode = "ONLINE";
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            int intRef = -1;
            //////objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "BKT_" + strOptinselected.ToUpper(), false, ref intRef);
            /*END HERE*/
            GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);        
            upDashboardDetails.Update();
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            //////objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "BKT_" + strOptinselected.ToUpper(), true, ref intRef);
            /*END HERE*/
        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }
    #endregion Event Declaration End.

    #region Methode Declaration begin.
    public void DashbordBucketsCountDynamic_Get(string strUserid, string strUserGroup, string strUWmode)
    {
        ObjBuss.OnlineApplicationdashbordBucketsCountDynamic_Get(ref _dsDashcount, strUWmode, strUserId, strUserGroup);
        if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
        {
            divDashbord.InnerHtml = _dsDashcount.Tables[0].Rows[0][0].ToString();

        }
    }
    public void DashbordBucketsCount_Get(string strUserid, string strUserGroup, string strLinkOption, string strUWmode)
    {
        ObjBuss.OnlineApplicationdashbordBucketsCount_Get(strUWmode, strUserId, strLinkOption, strUserGroup, ref _dsDashcount);
        if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
        {
            if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
            {
                for (int i = 0; i < _dsDashcount.Tables[0].Rows.Count; i++)
                {
                    switch (Convert.ToString(_dsDashcount.Tables[0].Rows[i]["MODE"]))
                    {
                        case "FRESH":
                            {
                                lblFreshcount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "PENDING":
                            {
                                lblPendingcount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "UWREFERAL":
                            {
                                //lblUWRefercount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "PAYMENT_REALIZED":
                            {
                                lblDocRealizedCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "READYTOISSUE":
                            {
                                lblReadytoissuecount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "RESOLVE":
                            {
                                lblResolveCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "SIGNOF":
                            {
                                lblSignoffCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "DOCQC":
                            {
                                //lblDocVerifiedcount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                lblDocQcCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_PENDING":
                            {
                                lblPivcPendingCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_COMPLETE_REALIZATION_COMPLETE":
                            {
                                lblPivcRealizationCompleteCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_COMPLETE_REALIZATION_PENDING":
                            {
                                lblPivcCompleteRelizationPendingCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "FORM_FILLING_PENDING":
                            {
                                lblformpending.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        default:
                            break;
                    }
                }
            }
        }
    }

    public void GetDashbordDetails(string strUserid, string strUserGroup, string strlnkOption, string strUWmode)
    {

        ObjBuss.OnlineApplicationDashborddetails_GET(strUWmode, strUserid, strUserGroup, strlnkOption, ref _ds);
        if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            lbldashborderror.Text = "";
            gvAppdasbord.DataSource = _ds;
            gvAppdasbord.DataBind();
        }
        else
        {
            lbldashborderror.Text = "No Record found";
            gvAppdasbord.DataSource = null;
            gvAppdasbord.DataBind();
        }
    }

    #endregion Methode Declaration End.
    protected void lnkSearchAppilcation_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["objCommonObj"] != null)
            {
                lbldashborderror.Text = string.Empty;
                objCommonObj = (CommonObject)Session["objCommonObj"];
                objCommonObj._ChannelType = "ONLINE";
                Session["objCommonObj"] = objCommonObj;
                strUWmode = "ONLINE";
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                int intRef = -1;
                //////objComm.ManageApplicationLifeCycle(txtSearch.Text, strUserId, "DASH_APP_SEARCH", false, ref intRef);
                /*END HERE*/
                //23NOV2017 
                ObjBuss.GetApplicationDetails(txtSearch.Text, strUserId,"ONLINE", ref _ds);
                //23NOV2017 END
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    gvAppdasbord.DataSource = _ds;
                    gvAppdasbord.DataBind();
                    upDashboardDetails.Update();
                }
                else
                {
                    lbldashborderror.Text = "No Record Found";
                }
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                //////objComm.ManageApplicationLifeCycle(txtSearch.Text, strUserId, "DASH_APP_SEARCH", true, ref intRef);
                /*END HERE*/
            }
        }
        catch (Exception ex)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }
    protected void tmrDashboard_Tick(object sender, EventArgs e)
    {
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objCommonObj._ChannelType = "Online";
        Session["objCommonObj"] = objCommonObj;
        strUWmode = "ONLINE";
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
        spanusername.InnerText = objCommonObj._Bpmuserdetails._UserName;
        DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
        upDashboardCount.Update();
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

   
}