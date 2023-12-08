using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using UWSaralObjects;
using ClosedXML.Excel;
using System.Configuration;

/*
***********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:BUSSINESS LAYER
REMARK: Get records on button click
DateTime :18 APRIL 2018
*********************************************************************************************************************************
 ************************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:BUSSINESS LAYER
REMARK: Bind data to gridview
DateTime :18 APRIL 2018
*********************************************************************************************************************************
 **********************************************************************************************************************************
 ************************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:BUSSINESS LAYER
REMARK: Export data to csv file format
DateTime :18 APRIL 2018
*********************************************************************************************************************************
 *************************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :AJAY SAHU
METHODE/EVENT: BindStatusDescription
REMARK: get bmp audit trail
DateTime :25 MAY 2018
*********************************************************************************************************************************
**************************************************************************************************************************************
COMMENT ID: 5
COMMENTOR NAME :KAVITA MAHAJAN
METHODE/EVENT: ViewChecklist
REMARK: Show checklist on View button
DateTime :23 NOV 2021
*********************************************************************************************************************************
**************************************************************************************************************************************
COMMENT ID: 6
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT: Btn_SaveComment
REMARK: Cr-1844 Add textbox to add comment
DateTime :23 Sep 2022
* ********************************************************************************************************************************
 */
public partial class Appcode_UWProductivity : System.Web.UI.Page
{
    Commfun objCommFun = new Commfun();
    BussLayer objBussLayer = new BussLayer();
    DataSet _ds = null;
    DataSet _dsReq = null;
    DataSet _dsStatus = null;
    ChangeValue objChangeObj = new ChangeValue();
    string strUserId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //  6.1 start of changes by Sagar Thorave -CR-1844
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            strUserId= objChangeObj.userLoginDetails._UserID;
            //  6.1 End of changes by Sagar Thorave -CR-1844
            string strPreviousPage = "";
            if (Request.UrlReferrer != null)
            {
                strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
            }
            if (strPreviousPage == "")
            {
                Response.Redirect("~/9011042143.aspx");
            }
            if (!Page.IsPostBack)
            {
                //div_addcomments.Visible = false;
                _ds = new DataSet();
                objCommFun = new Commfun();
                ddlrpttype.SelectedValue = "1";
                //objCommFun.FetchUser("offline", ref _ds);
                //BindDropDown(_ds);
                //FillReportType();
            }
            // string commentAccess = ConfigurationManager.AppSettings["UWCommentAccess"];
            //if (commentAccess.Split(',').ToArray().Contains(strUserId))
            //{
            //    btnUpdateComment.Style.Add("display", "lock");
            //}
        }
        catch (Exception ex)
        {

            lblUWComments.Text = "Try Again Later";
        }
    }
    /*ID:1 START*/
    protected void btnFetchRecord_Click(object sender, EventArgs e)
    {
        try
        {
           // div_addcomments.Visible = true;
            _ds = new DataSet();
            _dsReq = new DataSet();
            _dsStatus = new DataSet();

            if (!ddlrpttype.SelectedValue.Equals("0") && !string.IsNullOrEmpty(txtAppno.Text))
            {
                objCommFun.OnlineCommentsDisplayDetails_GET(ref _ds, Convert.ToString(txtAppno.Text), "", "General");
                ViewState["ds"] = _ds;
                BindComments(_ds);
                objCommFun.Get_RequiremenSummary(ref _dsReq, Convert.ToString(txtAppno.Text));
                ViewState["dsReq"] = _dsReq;
                BindRequirementSummary(_dsReq);
                objCommFun.GetStatusDiscription(ref _dsStatus, Convert.ToString(txtAppno.Text));
                ViewState["_dsStatus"] = _dsStatus;
                BindStatusDescription(_dsStatus);
            }

            else
            {
                lblUWComments.Text = "Please select Report Type and Enter Application No";
            }
            //  6.1 start of changes by Sagar Thorave -CR-1844
            DataSet ds=new DataSet();
            objCommFun.Is_UWComment_User_Valid(ref ds, strUserId);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string IsActive = (ds.Tables[0].Rows[0]["IsActive"].ToString());
                if (IsActive == "1")
                {
                    btnUpdateComment.Style.Add("display", "lock");
                }
                else {

                }
            }
            ////  6.1 start of changes by Sagar Thorave -CR-1844
        }
        catch (Exception ex)
        {
            lblUWComments.Text = "Try Again Later";
        }
    }
    /*ID:1 END*/
    /*ID:2 START*/
    private void BindComments(DataSet _ds)
    {

        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            dgUWComments.DataSource = null;
            dgUWComments.DataBind();
            divUwComm.Visible = true;
            dgUWComments.DataSource = _ds;
            lblUWComments.Text = "Record Fetched Successfully";
        }
        else
        {
            dgUWComments.DataSource = null;
            lblUWComments.Text = "No Record Found";
        }
        dgUWComments.DataBind();
    }
    /*ID:2 END*/
    protected void btnExportToCsv_Click(object sender, EventArgs e)
    {
        try
        {
            //_ds = new DataSet();

            if (!ddlrpttype.SelectedValue.Equals("0") && !string.IsNullOrEmpty(txtAppno.Text))
            {
                //_ds = (DataSet)ViewState["ds"];
                ExportData();

            }
            else
            {
                lblUWComments.Text = "Please select Report Type and Enter Application No";
            }

        }
        catch (Exception ex)
        {
            lblUWComments.Text = "Try Again Later";
        }
    }
    /*ID:4 START*/
    private void ExportData()
    {
        try
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                if (ViewState["ds"] != null || ViewState["dsReq"] != null || ViewState["_dsStatus"] != null)
                {
                    DataSet dsUWComments = new DataSet();
                    dsUWComments = (DataSet)ViewState["ds"];
                    dsUWComments.Tables[0].TableName = "UW Comments";

                    DataSet dsRequirement = new DataSet();
                    dsRequirement = (DataSet)ViewState["dsReq"];
                    dsRequirement.Tables[0].TableName = "Requirement Summary";

                    DataSet dsStatusDesc = new DataSet();
                    dsStatusDesc = (DataSet)ViewState["_dsStatus"];
                    dsStatusDesc.Tables[0].TableName = "BPMS Audit Trail";

                    var sheet1 = wb.Worksheets.Add(dsUWComments.Tables[0]);
                    sheet1.Table("Table1").ShowAutoFilter = false;
                    //ssheet1.Table("Table1").Theme = XLTableTheme.None;

                    var sheet2 = wb.Worksheets.Add(dsRequirement.Tables[0]);
                    sheet2.Table("Table1").ShowAutoFilter = false;
                    //sheet1.Table("Table1").Theme = XLTableTheme.None;

                    var sheet3 = wb.Worksheets.Add(dsStatusDesc.Tables[0]);
                    sheet3.Table("Table1").ShowAutoFilter = false;
                    //sheet1.Table("Table1").Theme = XLTableTheme.None;

                    //wb.Worksheets.Add(dsUWComments);
                    //wb.Worksheets.Add(dsRequirement);
                    //wb.Worksheets.Add(dsStatusDesc);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=UWComments.xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
        catch (Exception EX)
        {

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    /*ID:4 END*/

    //private void FillReportType()
    //{
    //    ListReportList lstReportList = new ListReportList();
    //    ddlrpttype.DataSource = lstReportList.ReportList;
    //    ddlrpttype.DataTextField = "Text";
    //    ddlrpttype.DataValueField = "Value";
    //    ddlrpttype.DataBind();
    //    ddlrpttype.Items.Add(new ListItem("---Select---", "0"));
    //    if (ddlrpttype.Items.FindByValue("0") != null)
    //    {
    //        ddlrpttype.Items.FindByValue("0").Selected = true;
    //    }
    //}

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtAppno.Text = string.Empty;
        dgUWComments.DataSource = null;
        dgUWComments.DataBind();
        dgRequirement.DataSource = null;
        dgRequirement.DataBind();
        dgStatusDesc.DataSource = null;
        dgStatusDesc.DataBind();
        divreqlbl.Visible = false;
        txtSaralStatus.Text = string.Empty;
        txtLAStatus.Text = string.Empty;
        txtAppno.Text = string.Empty;
        txtpolicyno.Text = string.Empty;
        txtriskscore.Text = string.Empty;
        txtenyscore.Text = string.Empty;
        divUwComm.Visible = false;
        divStatusDesc.Visible = false;
        ViewState["ds"] = null;
        ViewState["dsReq"] = null;
        ViewState["_dsStatus"] = null;
    }
    /*ID:4 START*/
    private void BindRequirementSummary(DataSet _ds)
    {

        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            dgRequirement.DataSource = null;
            dgRequirement.DataBind();
            divreqlbl.Visible = true;
            dgRequirement.DataSource = _ds;
            lblUWComments.Text = "Record Fetched Successfully";
        }
        else
        {
            dgRequirement.DataSource = null;
            lblUWComments.Text = "No Record Found";
        }
        dgRequirement.DataBind();
    }
    /*ID:4 END*/
    protected void txtAppno_TextChanged(object sender, EventArgs e)
    {
        objCommFun.Get_Status(ref _ds, Convert.ToString(txtAppno.Text));
        if (_ds.Tables.Count > 0)
        {
            if (_ds.Tables[0].Rows.Count > 0)
            {
                txtSaralStatus.Text = Convert.ToString(_ds.Tables[0].Rows[0]["APP_STAGE"]);
            }
            if (_ds.Tables[1].Rows.Count > 0)
            {
                txtLAStatus.Text = Convert.ToString(_ds.Tables[1].Rows[0]["POL_POLICYSTATUS"]);
            }
            if (_ds.Tables[2].Rows.Count > 0)
            {
                txtriskscore.Text = Convert.ToString(_ds.Tables[2].Rows[0]["Risk_Score"]);
            }
            if (_ds.Tables[3].Rows.Count > 0)
            {
                txtenyscore.Text = Convert.ToString(_ds.Tables[3].Rows[0]["ENYValue"]);
            }
            if (_ds.Tables[1].Rows.Count > 0)
            {
                txtappnumber.Text = Convert.ToString(_ds.Tables[1].Rows[0]["POL_applicationNoStr"]);
            }
            if (_ds.Tables[1].Rows.Count > 0)
            {
                txtpolicyno.Text = Convert.ToString(_ds.Tables[1].Rows[0]["POL_policyNumber"]);
            }
        }
    }
    /*ID:4 START*/
    private void BindStatusDescription(DataSet _ds)
    {

        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            dgStatusDesc.DataSource = null;
            dgStatusDesc.DataBind();
            divStatusDesc.Visible = true;
            dgStatusDesc.DataSource = _ds;
            lblUWComments.Text = "Record Fetched Successfully";
        }
        else
        {
            dgStatusDesc.DataSource = null;
            lblUWComments.Text = "No Record Found";
        }
        dgStatusDesc.DataBind();
    }
    /*
    protected void dgStatusDesc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataSet _dsStatusD = new DataSet();
        _dsStatusD = (DataSet)ViewState["_dsStatus"];
        dgStatusDesc.PageIndex = e.NewPageIndex;
        this.BindStatusDescription(_dsStatusD);
    }
    */
    /*ID:4 End*/
    //4.1 Begin of changes by kavita -CR-30489 Phase 2
    protected void btnViewChecklist_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtAppno.Text != "")
            {
                //Response.Redirect("ViewChecklist.aspx");
                Response.Redirect("ViewChecklist.aspx?AppNo=" + txtAppno.Text.ToString(),false);
            }
            else
            {
                lblUWComments.Text = "Please Enter Application No..";
            }
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'http://localhost:59925/ImageQCCheckList.aspx?ApplicationNo=L00007196&CheckListType=UWSaral&Taskid=F8797811-5CB6-4430-B269-19465A2D5A0A', null, 'height=700,width=900,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
        catch (Exception ex)
        {
        }
    }
    //4.1 End of changes by kavita -CR-30489 Phase 2
  //  6.1 start of changes by Sagar Thorave -CR-1844
    protected void Btn_SaveComment_Click(object sender, EventArgs e)
    {
        try
        {
            string strApplicationno = txtAppno.Text.ToString();
            if (!string.IsNullOrEmpty(AddNewUWComment.Text))
            {
                string s = string.Empty;
                int UWCommentResult = 0;
                objCommFun.OnlineUWCommentsDetails_Save(objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._UserGroup, Convert.ToString(AddNewUWComment.Text), strApplicationno, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, "General", ref UWCommentResult);
                objCommFun.OnlineCommentsDisplayDetails_GET(ref _ds, Convert.ToString(txtAppno.Text), "", "General");
                BindComments(_ds);
                AddNewUWComment.Text = string.Empty;
            }
        }
        catch (Exception ex) {
        }
    }
   
    protected void btnUpdateComment_Click(object sender, EventArgs e)
    {
        try
        {
            UwCommAdd.Style.Add("display", "lock");
        }
        catch (Exception ex) {

        }
    }
    //6.1 End of changes by Sagar Thorave -CR-1844

}
