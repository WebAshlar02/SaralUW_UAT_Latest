using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_UWAllocation_Priority : System.Web.UI.Page
{
    CommanAssignmentDetails commbj = new CommanAssignmentDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           if (!IsPostBack)
            {
                BindPriority_DropDown();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void BindPriority_DropDown()
    {
        try
        {
            DataSet _dsFilldata = new DataSet();
            commbj.OnlineMasterUnderWriting_Priority_GET("Priority", ref _dsFilldata);

            if (_dsFilldata.Tables.Count > 0)
            {
                ddlPriority.Items.Clear();
                ddlPriority.Items.Insert(0, new ListItem("Please select ", ""));
                ddlPriority.SelectedValue = "";
                foreach (DataRow row in _dsFilldata.Tables[0].Rows)
                {

                    if (row["value"] != null)
                        ddlPriority.DataValueField = row["value"].ToString();
                    else
                        ddlPriority.DataValueField = "";

                    if (row["name"] != null)
                        ddlPriority.DataTextField = row["name"].ToString();
                    else
                        ddlPriority.DataTextField = "";

                    ddlPriority.Items.Add(new ListItem(ddlPriority.DataTextField, ddlPriority.DataValueField));

                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        commbj.UnderWriting_AssignmentDetails_GET_Priority("Update_Priority", Convert.ToInt32(ddlPriority.SelectedValue), ref ds);
        if (ds.Tables.Count > 0)
        {
            string script = "<script type=\"text/javascript\">alert('Save Priority Successfuly.....!');</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
            BindPriority_DropDown();
        }
    }
}