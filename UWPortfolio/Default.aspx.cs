using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Platform.Utilities.LoggerFramework;


public partial class _Default : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            objCommonObj = (CommonObject)Session["objCommonObj"];
            objCommonObj._ChannelType = lnkOnline.Text;
            Session["objCommonObj"] = objCommonObj;
            strUWmode = lnkOnline.Text;
            strOptinselected = "PENDING CASES";
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            spanusername.InnerText = objCommonObj._Bpmuserdetails._UserName;
            GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
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

    }

    protected void lnkOnline_Click(object sender, EventArgs e)
    {
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objCommonObj._ChannelType = lnkOnline.Text;
        Session["objCommonObj"] = objCommonObj;
        strOptinselected = lnkOnline.Text;
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
        strUWmode = objCommonObj._ChannelType;
        GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
        DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
    }

    protected void lnkoffline_Click(object sender, EventArgs e)
    {
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objCommonObj._ChannelType = lnkoffline.Text;
        Session["objCommonObj"] = objCommonObj;
        strOptinselected = lnkoffline.Text;
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
        strUWmode = objCommonObj._ChannelType;
        GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
        DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
    }

    protected void gvAppdasbord_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
            LinkButton lnkApplicationNo = (LinkButton)row.FindControl("lnkAppno");
            HiddenField hdnProductName = (HiddenField)row.FindControl("hdnProductName");
            Label lblChannelType = (Label)row.FindControl("lblUtmchannel");
            Label lblPolicyNo = (Label)row.FindControl("lblpolno");
            string strApplicationno = lnkApplicationNo.Text;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            objCommonObj._ApplicationNo = strApplicationno;
            objCommonObj._ChannelType = lblChannelType.Text;
            objCommonObj._PolicyNo = lblPolicyNo.Text;
            objCommonObj._ProductName = hdnProductName.Value;
            Session["objCommonObj"] = objCommonObj;
            #region Control Logic Begin.
            strAppstatusKey = (strUserGroup == "UW") ? "UL" : "DL";
            //objComm.OnlineApplicationLockChecker(strApplicationno, strUserId, strUserGroup, ref _ds);
            //if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            //{
            //    objComm.OnlineApplicationchangeStatus(strApplicationno, strUserId, strAppstatusKey, "", ref Result);
            //    if (Result != 0 || Result != -1)
            //    {
                    Response.Redirect(string.Format("Appcode/Uwdecision.aspx?qsUserGroup={0}&qsAppNo={1}&qsChannelName={2}&qsPolicyNo={3}", UWSaralDecision.CommFun.Base64EncodingMethod(objCommonObj._Bpmuserdetails._UserGroup), UWSaralDecision.CommFun.Base64EncodingMethod(strApplicationno), UWSaralDecision.CommFun.Base64EncodingMethod(lblChannelType.Text), UWSaralDecision.CommFun.Base64EncodingMethod(lblPolicyNo.Text)), true);
            //    }
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#myModal1').modal();", true);
            //}
            #endregion Control Logic End.

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
    protected void lnkCommondashbord_Click(object sender, EventArgs e)
    {
        LinkButton lnkOptinselected = (LinkButton)sender;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strOptinselected = lnkOptinselected.Text;
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
        strUWmode = objCommonObj._ChannelType;
        GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
        DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
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
        ObjBuss.OnlineApplicationdashbordBucketsCount_Get(ref _dsDashcount, strUWmode, strUserId, strLinkOption, strUserGroup);
        if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
        {

            if (_dsDashcount.Tables[0].Rows.Count > 0)
                lblFreshcount.Text = _dsDashcount.Tables[0].Rows[0]["FRESHCOUNT"].ToString();

            if (_dsDashcount.Tables[1].Rows.Count > 0)
                lblPendingcount.Text = _dsDashcount.Tables[1].Rows[0]["PENDINGCOUNT"].ToString();

            if (_dsDashcount.Tables[2].Rows.Count > 0)
                lblUWRefercount.Text = _dsDashcount.Tables[2].Rows[0]["UWREFERCOUNT"].ToString();

            if (_dsDashcount.Tables[3].Rows.Count > 0)
                lblPendingVerificationcount.Text = _dsDashcount.Tables[3].Rows[0]["UWNOTVERIFIED"].ToString();

            if (_dsDashcount.Tables[4].Rows.Count > 0)
                lblDocVerifiedcount.Text = _dsDashcount.Tables[4].Rows[0]["UWVERIFIED"].ToString();

            if (_dsDashcount.Tables[5].Rows.Count > 0)
                lblDocRealizedCount.Text = _dsDashcount.Tables[5].Rows[0]["REALIZED"].ToString();

            if (_dsDashcount.Tables[6].Rows.Count > 0)
                lblReadytoissuecount.Text = _dsDashcount.Tables[6].Rows[0]["READYTOISSUECOUNT"].ToString();


        }
    }

    public void GetDashbordDetails(string strUserid, string strUserGroup, string strlnkOption, string strUWmode)
    {

        ObjBuss.OnlineApplicationDashborddetails_GET(ref _ds, strUWmode, strUserid, strUserGroup, strlnkOption);
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
}