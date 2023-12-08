using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    #region Declaration
    BussLayer BAL = new BussLayer();
    string AppNo = string.Empty;
    int b = 0;
    #endregion Declaration
    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(_Default), Page);
        lblMsg.Visible = false;
        lblDedupeMsg.Visible = false;
        lblSelDedupeMsg.Visible = false;
        btnSave.Visible = false;
        btnNewClient.Attributes.Add("style", "margin-left:532px;margin-top:10px;width:200px");
        //btnNewClient.Visible = false;
        if (!IsPostBack)
        {
            GetDedupe(AppNo);
        }


    }
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void GetDedupe(string AppNo)
    {
        DataSet ds = new DataSet();
        try
        {
            BAL.GetPendingClient(AppNo, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdPendingDedupe.DataSource = ds;
                grdPendingDedupe.DataBind();
                btnNewClient.Visible = true;
            }
            else
            {
                grdPendingDedupe.DataSource = null;
                grdPendingDedupe.DataBind();
                lblDedupeMsg.Visible = true;
                btnNewClient.Visible = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void GetPageIndexSelected()
    {
        DataSet ds = new DataSet();
        try
        {
            foreach (GridViewRow row in grdPendingDedupe.Rows)
            {
                int rowindex = Convert.ToInt32(Session["rowindex"]);
                AppNo = grdPendingDedupe.Rows[rowindex].Cells[1].Text;
                string FirstName = grdPendingDedupe.Rows[rowindex].Cells[2].Text;
                string LastName = grdPendingDedupe.Rows[rowindex].Cells[3].Text;
                string Gender = grdPendingDedupe.Rows[rowindex].Cells[4].Text;
                string DOB = Convert.ToString(DateFormatMonth(grdPendingDedupe.Rows[rowindex].Cells[5].Text));
                string PinCode = grdPendingDedupe.DataKeys[rowindex].Values[0].ToString();
                long RefNo = Convert.ToInt32(grdPendingDedupe.DataKeys[rowindex].Values[1]);
                string assuredType = grdPendingDedupe.Rows[rowindex].Cells[6].Text;
                Session["AppNo"] = AppNo;
                Session["RefNo"] = RefNo;
                Session["assuredType"] = assuredType;
                if (AppNo != null)
                {
                    BAL.GetSelectedResult(FirstName, LastName, Gender, DOB, PinCode, ref b,ref ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        grdSelectedDedupe.DataSource = ds;
                        grdSelectedDedupe.DataBind();
                        btnSave.Visible = true;
                        //btnNewClient.Visible = true;
                        btnNewClient.Attributes.Add("style", "margin-left:40px;margin-top:10px;width:200px");
                    }
                    else
                    {
                        grdSelectedDedupe.DataSource = null;
                        grdSelectedDedupe.DataBind();
                        lblSelDedupeMsg.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    public void GetSelectedDedupe()
    {
        DataSet ds = new DataSet();
        try
        {
            foreach (GridViewRow row in grdPendingDedupe.Rows)
            {
                RadioButton radiobtn = (RadioButton)row.FindControl("rdoSelect");
                if (radiobtn.Checked == true)
                {
                    int rowindex = grdPendingDedupe.Rows[row.RowIndex].RowIndex;
                    AppNo = grdPendingDedupe.Rows[row.RowIndex].Cells[1].Text;
                    string FirstName = grdPendingDedupe.Rows[row.RowIndex].Cells[2].Text;
                    string LastName = grdPendingDedupe.Rows[row.RowIndex].Cells[3].Text;
                    string Gender = grdPendingDedupe.Rows[row.RowIndex].Cells[4].Text;
                    string DOB = Convert.ToString(DateFormatMonth(grdPendingDedupe.Rows[row.RowIndex].Cells[5].Text));
                    string PinCode = grdPendingDedupe.DataKeys[rowindex].Values[0].ToString();
                    long RefNo = Convert.ToInt32(grdPendingDedupe.DataKeys[rowindex].Values[1]);
                    string assuredType = grdPendingDedupe.Rows[row.RowIndex].Cells[6].Text;
                    Session["AppNo"] = AppNo;
                    Session["RefNo"] = RefNo;
                    Session["assuredType"] = assuredType;
                    Session["rowindex"] = rowindex;
                    if (AppNo != null)
                    {
                        BAL.GetSelectedResult(FirstName, LastName, Gender, DOB, PinCode, ref b,ref ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grdSelectedDedupe.DataSource = ds;
                            grdSelectedDedupe.DataBind();
                            btnSave.Visible = true;
                            btnNewClient.Attributes.Add("style", "margin-left:40px;margin-top:10px;width:200px");
                            //btnNewClient.Visible = true;
                        }
                        else
                        {
                            grdSelectedDedupe.DataSource = null;
                            grdSelectedDedupe.DataBind();
                            lblSelDedupeMsg.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rdoSelect_CheckedChanged(object sender, EventArgs e)
    {
        GetSelectedDedupe();
    }
    protected void grdPendingDedupe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AppNo = e.Row.Cells[1].Text;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdPendingDedupe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdPendingDedupe.PageIndex = e.NewPageIndex;
        grdSelectedDedupe.DataSource = null;
        grdSelectedDedupe.DataBind();
        GetDedupe(AppNo);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            foreach (GridViewRow row in grdSelectedDedupe.Rows)
            {
                int result = 0;

                RadioButton radioSelect = (RadioButton)row.FindControl("rdoSearchSelect");
                if (radioSelect.Checked == true)
                {
                    count++;
                    DataSet ds = new DataSet();
                    int rowindex = grdSelectedDedupe.Rows[row.RowIndex].RowIndex;
                    AppNo = Convert.ToString(Session["AppNo"]);
                    int isNewClient = 1;
                    int ClientId = Convert.ToInt32(grdSelectedDedupe.Rows[row.RowIndex].Cells[1].Text);
                    long RefNo = Convert.ToInt32(Session["RefNo"]);
                    string assuredType = Convert.ToString(Session["assuredType"]);
                    BAL.UpdateClient(RefNo, assuredType, isNewClient, AppNo, ClientId, ref result);
                    if (result == 1)
                    {
                        GetDedupe(string.Empty);
                        grdSelectedDedupe.DataSource = null;
                        grdSelectedDedupe.DataBind();
                        lblMsg.InnerText = "Dedupe Updated Successfully";
                        lblMsg.Visible = true;
                    }
                    else
                    {
                        lblMsg.InnerText = "Dedupe Updation Failed";
                        lblMsg.Visible = true;
                    }
                }
            }
            if (count == 0)
            {
                lblMsg.InnerText = "Please Select";
                lblMsg.Visible = true;
                btnSave.Visible = true;
                btnNewClient.Attributes.Add("style", "margin-left:40px;margin-top:10px;width:200px");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            grdSelectedDedupe.DataSource = null;
            grdSelectedDedupe.DataBind();
            GetDedupe(txtappno.Value);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static object DateFormatMonth(object objInput)
    {
        try
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
                    System.DateTime dt = Convert.ToDateTime(objInput);
                    objInput = dt.ToString("MM/dd/yyyy");
                    return objInput;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnNewClient_Click(object sender, EventArgs e)
    {
        try
        {
           int result = 0;
           DataSet ds = new DataSet();
           int rowindex = Convert.ToInt32(Session["rowindex"]);
           AppNo = Convert.ToString(Session["AppNo"]);
           int isNewClient = 0;
           int ClientId = 0;
           long RefNo = Convert.ToInt32(Session["RefNo"]);
           string assuredType = Convert.ToString(Session["assuredType"]);
           BAL.UpdateClient(RefNo, assuredType, isNewClient, AppNo, ClientId, ref result);
           if (result == 1)
           {
               GetDedupe(string.Empty);
               grdSelectedDedupe.DataSource = null;
               grdSelectedDedupe.DataBind();
               lblMsg.InnerText = "Dedupe Updated Successfully";
               lblMsg.Visible = true;
           }
           else
           {
               lblMsg.InnerText = "Dedupe Updation Failed";
               lblMsg.Visible = true;
           }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  
    protected void grdSelectedDedupe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSelectedDedupe.PageIndex = e.NewPageIndex;
        GetPageIndexSelected();
    }
    protected void rdoSearchSelect_CheckedChanged(object sender, EventArgs e)
    {
        btnSave.Visible = true;
        btnNewClient.Attributes.Add("style", "margin-left:40px;margin-top:10px;width:200px");
    }
}