using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using UWSaralObjects;

public partial class Appcode_UWStatusReport : System.Web.UI.Page
{
    Commfun objCommFun = new Commfun();
    BussLayer objBussLayer = new BussLayer();
    DataSet _ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
            }
        }
        catch (Exception ex)
        {

            lblUWStatus.Text = "Try Again Later";
        }
    }
    protected void btnFetchRecord_Click(object sender, EventArgs e)
    {
        try
        {
            _ds = new DataSet();
            string strFromDate = string.IsNullOrEmpty(txtFromDate.Text) ? System.DateTime.Now.ToString("dd/MM/yyyy") : txtFromDate.Text;
            string strToDate = string.IsNullOrEmpty(txtToDate.Text) ? System.DateTime.Now.ToString("dd/MM/yyyy") : txtToDate.Text;
            string strstatus = (ddlStatus.SelectedValue == "0") ? string.Empty : Convert.ToString(ddlStatus.SelectedValue);
            
            //string strUserId = (ddlProductivityUser.SelectedValue == "0") ? string.Empty : ddlProductivityUser.SelectedValue;
            if (Convert.ToDateTime(strToDate) < Convert.ToDateTime(strFromDate))
            {
                lblUWStatus.Text = "To date should be less than or equal to from date";
                dgUWStatus.DataSource = null;
                dgUWStatus.DataBind();
            }
            else
            {
                /*ID:1 START*/
                if (!ddlStatus.SelectedValue.Equals("0"))
                {
                    if ((Convert.ToDateTime(strToDate) - Convert.ToDateTime(strFromDate)).TotalDays <= 29)
                    {
                        objBussLayer.FetchStatusReportData(txtFromDate.Text, txtToDate.Text, strstatus, ref _ds);
                        BindProductivityReport(_ds);
                    }
                    /*ID:1 END*/
                }
                else
                {
                    lblUWStatus.Text = "Please select Status!";
                }


            }
        }
        catch (Exception ex)
        {
            lblUWStatus.Text = "Try Again Later";
        }
    }
    private void BindProductivityReport(DataSet _ds)
    {

        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            dgUWStatus.DataSource = null;
            dgUWStatus.DataBind();
            dgUWStatus.DataSource = _ds;
            lblUWStatus.Text = "Record Fetched Successfully";
        }
        else
        {
            dgUWStatus.DataSource = null;
            lblUWStatus.Text = "No Record Found";
        }
        dgUWStatus.DataBind();
    }
    //private void BindDropDown(DataSet _ds)
    //{
    //    ListItem objListItem = new ListItem();
    //    objListItem.Text = "SELECT USER";
    //    objListItem.Value = "0";
    //    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddlProductivityUser.DataSource = _ds;
    //        ddlProductivityUser.DataTextField = "TEXT";
    //        ddlProductivityUser.DataValueField = "VALUE";
    //        ddlProductivityUser.DataBind();
    //    }
    //    ddlProductivityUser.Items.Insert(0, objListItem);
    //}
    protected void btnExportToCsv_Click(object sender, EventArgs e)
    {
        try
        {
            _ds = new DataSet();
            string strFromDate = string.IsNullOrEmpty(txtFromDate.Text) ? System.DateTime.Now.ToString("dd/MM/yyyy") : txtFromDate.Text;
            string strToDate = string.IsNullOrEmpty(txtToDate.Text) ? System.DateTime.Now.ToString("dd/MM/yyyy") : txtToDate.Text;
            string strStatus = (ddlStatus.SelectedValue == "0") ? string.Empty : Convert.ToString(ddlStatus.SelectedValue);
            if (Convert.ToDateTime(strToDate) < Convert.ToDateTime(strFromDate))
            {
                lblUWStatus.Text = "To date should be less than or equal to from date";
                dgUWStatus.DataSource = null;
                dgUWStatus.DataBind();
            }
            else
            {

                if (!ddlStatus.SelectedValue.Equals("0"))
                {
                    if ((Convert.ToDateTime(strToDate) - Convert.ToDateTime(strFromDate)).TotalDays <= 29)
                    {
                        objBussLayer.FetchStatusReportData(txtFromDate.Text, txtToDate.Text, strStatus, ref _ds);
                        ExportData(_ds);
                    }
                    //if ((Convert.ToDateTime(strToDate) - Convert.ToDateTime(strFromDate)).TotalDays <= 29)
                    //{
                    //    objBussLayer.FetchUWProductivity(txtFromDate.Text, txtToDate.Text, strUserId, ref _ds);
                    //    ExportData(_ds);
                    //}

                    else
                    {
                        lblUWStatus.Text = "Date difference between from date and todate should not be greater than 30";
                        dgUWStatus.DataSource = null;
                        dgUWStatus.DataBind();
                    }
                }
                else
                {
                    Response.Write("Please select Status");
                }
            }
        }
        catch (Exception ex)
        {
            lblUWStatus.Text = "Try Again Later";
        }
    }

    private void ExportData(DataSet _ds)
    {
        try
        {
            string strFileName = "UWStatusReport" + System.DateTime.Now.ToString("yyyyMMddHHmmsssfff");
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = _ds;
            GridView1.DataBind();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //Apply text style to each Row
                GridView1.Rows[i].Attributes.Add("class", "textmode");
            }
            GridView1.RenderControl(hw);
            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception EX)
        {

        }
    }

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

}