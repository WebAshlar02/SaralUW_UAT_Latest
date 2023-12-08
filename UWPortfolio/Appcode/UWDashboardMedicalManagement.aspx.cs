using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UWSaralObjects;
using System.IO;
using System.Configuration;

public partial class Appcode_UWDashboardMedicalManagement : System.Web.UI.Page
{
    string strPdf = ".pdf";
    BussLayer objBussLayer;
    DataSet _ds;
    int intIndexValue;
    DashboardMedicalManagement objDashboardMedicalManagement;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorUWDashboardMedicalManagement.Text = string.Empty;
        if (!Page.IsPostBack)
        {
            try
            {
                BindCategory(FetchCategory());
                BindPartners(FetchPartners());
                BindLastRunSchedularStaus(FetchLastRunSchedularStaus());
            }
            catch (Exception ex)
            {
                lblErrorUWDashboardMedicalManagement.Text = ex.Message;
            }
        }
    }

    private void BindCategory(DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            ddlCategory.DataSource = _ds;
            ddlCategory.DataTextField = "LOOKUP_DESCRIPTION";
            ddlCategory.DataValueField = "LOOKUP_NAME";
        }
        else
        {
            ddlCategory.DataSource = null;
        }
        ddlCategory.DataBind();

    }

    private DataSet FetchPartners()
    {
        _ds = new DataSet();
        objBussLayer = new BussLayer();
        objBussLayer.FetchDashboardUWMedicalManagementPartners(ref _ds);
        return _ds;
    }
    private void BindPartners(DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            ddlPartnerType.DataSource = _ds;
            ddlPartnerType.DataTextField = "PartnerDesc";
            ddlPartnerType.DataValueField = "PartnerName";
        }
        else
        {
            ddlPartnerType.DataSource = null;
        }
        ddlPartnerType.DataBind();
    }
    private DataSet FetchCategory()
    {
        _ds = new DataSet();
        objBussLayer = new BussLayer();
        objBussLayer.GetLookupData(327, ref _ds);
        return _ds;
    }

    protected void btnSearchUWDashboardMedicalManagement_Click(object sender, EventArgs e)
    {
        try
        {
            objDashboardMedicalManagement = new DashboardMedicalManagement();
            BindDashboardMedicalManagement(objDashboardMedicalManagement);
            BindMedicalDetails(FetchMedicalDetais(objDashboardMedicalManagement));
        }
        catch (Exception ex)
        {
            lblErrorUWDashboardMedicalManagement.Text = ex.Message;
        }
    }
    private void BindDashboardMedicalManagement(DashboardMedicalManagement objDashboardMedicalManagement)
    {
        DateTime dt;
        objDashboardMedicalManagement.ApplicationNo = txtApplicationNumber.Text;
        objDashboardMedicalManagement.PolicyNo = txtPolicyNumber.Text;
        if (DateTime.TryParse(txtStartDate.Text, out dt))
        {
            objDashboardMedicalManagement.StartDate = dt;
        }
        else
        {
            objDashboardMedicalManagement.StartDate = Convert.ToDateTime("01-01-1975");
        }
        if (DateTime.TryParse(txtEndDate.Text, out dt))
        {
            objDashboardMedicalManagement.EndDate = dt;
        }
        else
        {
            objDashboardMedicalManagement.EndDate = Convert.ToDateTime("01-01-1975");
        }
        objDashboardMedicalManagement.Category = ddlCategory.SelectedValue;
        objDashboardMedicalManagement.IsOnline = cbIsOnline.Checked;
    }

    private DataSet FetchMedicalDetais(DashboardMedicalManagement objDashboardMedicalManagement)
    {
        objBussLayer = new BussLayer();
        _ds = new DataSet();
        objBussLayer.FetchDashboardUWMedicalManagementDetails(objDashboardMedicalManagement, ref _ds);
        return _ds;
    }
    private void BindMedicalDetails(DataSet _ds)
    {
        string strCss, strScrollBar = "ScrollBar";
        strCss = divMedicalGrid.Attributes["class"];
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            dgMedicalGrid.DataSource = _ds;
            if (!strCss.Contains(strScrollBar))
            {
                strCss += strScrollBar;
            }
        }
        else
        {
            dgMedicalGrid.DataSource = null;
            lblErrorUWDashboardMedicalManagement.Text = "Medical Not Raised Against Application For Selected Criteria";
            strCss = strCss.Replace(strScrollBar, string.Empty);
        }
        divMedicalGrid.Attributes["class"] = strCss;
        dgMedicalGrid.DataBind();
    }

    protected void dgMedicalGrid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            byte[] bytMedicalReport = new byte[1];
            //download medical documents
            if (e.CommandName == "VIEWFILE")
            {
                intIndexValue = 3;
                //FETCH FILE FROM DMS SERVER      
                FetchReports(e.Item.Cells[intIndexValue].Text, ref bytMedicalReport);
                //DOWNLOAD FILE FROM DMS SERVER
                DownloadFile(bytMedicalReport, e.Item.Cells[intIndexValue].Text, strPdf);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Success Message", "fnDownloadSuccess();", false);
            }
            //fetch tpa status grid
            else if (e.CommandName == "TPA")
            {
                intIndexValue = 4;
                //check null
                if (!string.IsNullOrEmpty(e.Item.Cells[intIndexValue].Text) && e.Item.Cells[intIndexValue].Text != "&nbsp;")
                {
                    //fetch data and bind it
                    objDashboardMedicalManagement = new DashboardMedicalManagement();
                    objDashboardMedicalManagement.ApplicationNo = e.Item.Cells[intIndexValue].Text;
                    BindTPADetails(FetchTPADetails(objDashboardMedicalManagement));
                }
                else
                {
                    BindTPADetails(new DataSet());
                    lblErrorUWDashboardMedicalManagement.Text = "Application Not Registerd To TPA";
                }
            }
        }
        catch (Exception ex)
        {
            lblErrorUWDashboardMedicalManagement.Text = ex.Message;

        }
    }

    protected void dgMedicalGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkappno = (LinkButton)e.Item.FindControl("lnkAppno");
            }
            e.Item.Cells[2].Visible = false;
            e.Item.Cells[3].Visible = false;
        }
        catch (Exception ex)
        {
            lblErrorUWDashboardMedicalManagement.Text = ex.Message;
        }
    }

    protected void lnkAppicationNo_Click(object sender, EventArgs e)
    {

    }

    protected void lnkViewFile_Click(object sender, EventArgs e)
    {

    }

    private void BindTPADetails(DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            dgTpaGrid.DataSource = _ds;

        }
        else
        {
            dgTpaGrid.DataSource = null;
            lblErrorUWDashboardMedicalManagement.Text = "No Record Found";
        }
        dgTpaGrid.DataBind();
    }

    private DataSet FetchTPADetails(DashboardMedicalManagement objDashboardMedicalManagement)
    {
        _ds = new DataSet();
        objBussLayer = new BussLayer();
        objBussLayer.FetchDashboardUWMedicalManagementTPADetails(objDashboardMedicalManagement, ref _ds);
        return _ds;
    }

    private void FetchReports(string strApplicationNo, ref byte[] bytMedicalReport)
    {
        bytMedicalReport = FetchMedicalReport(strApplicationNo);
    }

    private byte[] FetchMedicalReport(string strApplicationNo)
    {
        string strFilePath = ConfigurationManager.AppSettings["TPASourcePath"];
        return FetchPDF(strApplicationNo, strFilePath, strPdf);
    }

    private byte[] FetchMERReport(string strApplicationNo)
    {
        string strFilePath = ConfigurationManager.AppSettings["TPASourcePath"];
        return FetchPDF(strApplicationNo + "_MER", strFilePath, strPdf);
    }

    private byte[] FetchPDF(string strApplicationNo, string strFilePath, string strExtension)
    {
        byte[] bytRet = new byte[1];
        try
        {
            UWSaralServices.CommFun objCommFun = new UWSaralServices.CommFun();
            if (Directory.Exists(strFilePath))
            {
                string strFullFile = Path.Combine(strFilePath, strApplicationNo + string.Empty + strExtension);
                if (File.Exists(strFullFile))
                {

                    bytRet = System.IO.File.ReadAllBytes(strFullFile);
                }
                else
                {
                    throw new Exception("File Not Found");
                }

            }
            else
            {
                throw new Exception("Directory Not Exists");
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //lblErrorUWDashboardMedicalManagement.Text = ex.Message;
        }
        return bytRet;
    }

    private void DownloadFile(byte[] bytMedicalReport, string strFileName, string strExtension)
    {
        if (bytMedicalReport != null && bytMedicalReport.Length > 1)
        {
            Response.Clear();
            MemoryStream ms = new MemoryStream(bytMedicalReport);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + strExtension);
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        else
        {
            lblErrorUWDashboardMedicalManagement.Text = "File Not Found";

        }
    }

    private DataSet FetchLastRunSchedularStaus()
    {
        _ds = new DataSet();
        objBussLayer = new BussLayer();
        objBussLayer.FetchlastRunScheduarStatus(ref _ds);
        return _ds;
    }

    private void BindLastRunSchedularStaus(DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToString(_ds.Tables[0].Rows[i]["code"]) == "REG")
                {
                    lblRegistered.Text = Convert.ToString(Convert.ToString(_ds.Tables[0].Rows[i]["LASTRUNON"]));
                }
                else if (Convert.ToString(_ds.Tables[0].Rows[i]["code"]) == "FTP")
                {
                    lblTransfer.Text = Convert.ToString(Convert.ToString(_ds.Tables[0].Rows[i]["LASTRUNON"]));
                }
            }
        }
    }

    private void ExportDataToCSV(DataSet _ds)
    {
        try
        {
            string strFileName = "UWSaralMedicalData" + System.DateTime.Now.ToString("yyyyMMddHHmmsssfff");
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

    protected void btnExportToCSV_Click(object sender, EventArgs e)
    {
        try
        {
            objDashboardMedicalManagement = new DashboardMedicalManagement();
            BindDashboardMedicalManagement(objDashboardMedicalManagement);
            ExportDataToCSV(FetchMedicalDetais(objDashboardMedicalManagement));
        }
        catch (Exception ex)
        {
            lblErrorUWDashboardMedicalManagement.Text = ex.Message;
        }
    }
}