using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Appcode_BulkManualAssignment : System.Web.UI.Page
{
    Commfun objCommfun = null;
    DataSet _ds = null;
    int intResponse = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                FetchUserList();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnBulkManualAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            #region validation
            if (string.IsNullOrEmpty(txtApplicationNo.Text))
            {
                lblBulkManualAllocationMsg.Text = "Enter Application Number";
                txtApplicationNo.Focus();
                return;
            }
            if (ddlUserName.SelectedValue.Equals("-1"))
            {
                lblBulkManualAllocationMsg.Text = "Select User";
                ddlUserName.Focus();
                return;
            }
            #endregion
            #region call service      
            string strAppno = txtApplicationNo.Text.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t",string.Empty);
            objCommfun = new Commfun();
            objCommfun.ManageBulkAllocation(strAppno, ddlUserName.SelectedValue, ref intResponse);

            string[] strArr = strAppno.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            lblBulkManualAllocationMsg.Text = Convert.ToString(intResponse)+" Records updated out of " + Convert.ToString(strArr.LongLength);
            #endregion
        }
        catch (Exception ex)
        {
            lblBulkManualAllocationMsg.Text = "Try Again Later";

        }
    }
    protected void rblChannel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FetchUserList();
        }
        catch (Exception ex)
        {
                        
        }
    }
    private void FetchUserList()
    {
        _ds = new DataSet();
        objCommfun = new Commfun();
        objCommfun.FetchUser(rblChannel.SelectedValue, ref _ds);
        ddlUserName.DataSource = _ds;
        ddlUserName.DataTextField = "TEXT";
        ddlUserName.DataValueField = "VALUE";
        ddlUserName.DataBind();
    }
}