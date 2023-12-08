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
/*
***********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:BUSSINESS LAYER
REMARK: Get records on button click
DateTime :12 JULY 2018
*********************************************************************************************************************************
 ************************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:BUSSINESS LAYER
REMARK: Bind data to gridview
DateTime :12 JULY 2018
*********************************************************************************************************************************
 **********************************************************************************************************************************
 ************************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:BUSSINESS LAYER
REMARK: Export data to csv file format
DateTime :12 JULY 2018
*********************************************************************************************************************************

 */
public partial class Appcode_MedicalStatus4Sales : System.Web.UI.Page
{
    Commfun objCommFun = new Commfun();
    BussLayer objBussLayer = new BussLayer();
    DataSet _ds = null;
    DataSet _dsReq = null;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                _ds = new DataSet();
                objCommFun = new Commfun();
                //ddlrpttype.SelectedValue = "1";
                //objCommFun.FetchUser("offline", ref _ds);
                //BindDropDown(_ds);
                //FillReportType();
            }
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
            _ds = new DataSet();
            _dsReq = new DataSet();
            

            if (!string.IsNullOrEmpty(txtAppno.Text))
            {
                objCommFun.OnlineCommentsDisplayDetails_GET(ref _ds, Convert.ToString(txtAppno.Text), "", "General");
                ViewState["ds"] = _ds;
                BindComments(_ds);
                objCommFun.Get_MedicalStatus(ref _dsReq, Convert.ToString(txtAppno.Text));
                ViewState["dsReq"] = _dsReq;
                BindMdicalStatus(_dsReq);
            }

            else
            {
                lblUWComments.Text = "Please Enter Application No";
            }



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

            if (!string.IsNullOrEmpty(txtAppno.Text))
            {
                //_ds = (DataSet)ViewState["ds"];
                ExportData();

            }
            else
            {
                lblUWComments.Text = "Please Enter Application No";
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
                    var sheet1 = wb.Worksheets.Add(dsUWComments.Tables[0]);
                    sheet1.Table("Table1").ShowAutoFilter = false;
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

    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtAppno.Text = string.Empty;
        dgUWComments.DataSource = null;
        dgUWComments.DataBind();
        lblUWComments.Text = "";
        divreqlbl.Visible = false;
        txtSaralStatus.Text = string.Empty;
        txtLAStatus.Text = string.Empty;
        divUwComm.Visible = false;
        ViewState["ds"] = null;
        ViewState["dsReq"] = null;
        ViewState["_dsStatus"] = null;
    }
    /*ID:4 START*/
    private void BindMdicalStatus(DataSet _ds)
    {

        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            dgMedicalStatus.DataSource = null;
            dgMedicalStatus.DataBind();
            divreqlbl.Visible = true;
            dgMedicalStatus.DataSource = _ds;
            lblUWComments.Text = "Record Fetched Successfully";
        }
        else
        {
            dgMedicalStatus.DataSource = null;
            lblUWComments.Text = "No Record Found";
        }
        dgMedicalStatus.DataBind();
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
        }
    }

  
}