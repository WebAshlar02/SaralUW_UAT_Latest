using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public partial class Appcode_DeactiveUsers : System.Web.UI.Page
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
                _ds = new DataSet();
                objCommFun = new Commfun();
                objCommFun.GetUser(ref _ds);
                BindGrid(_ds);
            }
        }
        catch (Exception ex)
        {

            lblUWDeact.Text = "Try Again Later";
        }
    }

    private void BindGrid(DataSet _ds)
    {   
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            gduser.DataSource = _ds.Tables[0];
            gduser.DataBind();
        }
        
    }
    protected void btnubmit_Click(object sender, EventArgs e)
    {
        string selectedValues = string.Empty;
        if (Convert.ToInt32(ddlAction.SelectedValue) != -1 )
        {
            foreach (GridViewRow gvrow in gduser.Rows)
            {
                var checkbox = gvrow.FindControl("CheckBox1") as CheckBox;
                if (checkbox.Checked)
                {
                    var lblUserID = gvrow.FindControl("lblUserId") as Label;
                    selectedValues += lblUserID.Text + ",";
                }

            }

            if (!string.IsNullOrEmpty(selectedValues.TrimEnd(',')))
            {
                //selectedValues = selectedValues.TrimEnd(',');
                int count = 0;
                objCommFun.UserActiveDeactive(("," + selectedValues), Convert.ToInt32(ddlAction.SelectedValue), ref count);
                if (count > 0)
                {
                    objCommFun.GetUser(ref _ds);
                    BindGrid(_ds);
                    
                    if (Convert.ToInt32(ddlAction.SelectedValue) == 0)
                    {
                        lblUWDeact.Text = "Users Deactivated Successfully";
                        
                    }
                    if (Convert.ToInt32(ddlAction.SelectedValue) == 1)
                    {
                        lblUWDeact.Text = "Users Activated Successfully";
                    }
                    ddlAction.SelectedValue = "-1";
                }
            }
            else
            {
                lblUWDeact.Text = "Please select User!";
            }
            //selectedValues = string.Concat("'",selectedValues,"'");
        }
        else
        {
            lblUWDeact.Text = "Please select Action!";
        }
    }
}