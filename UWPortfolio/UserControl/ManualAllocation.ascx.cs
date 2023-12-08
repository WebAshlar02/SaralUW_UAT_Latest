using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Platform.Utilities.LoggerFramework;
public partial class UserControl_ManualAllocation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (Session["application"] != null)
                {
                    Session.Remove("application");
                }
                if (Session["eligible"] != null)
                {
                    Session.Remove("eligible");
                }
                if (Session["noneligible"] != null)
                {
                    Session.Remove("noneligible");
                }
                FillUserListDropdown();
                //CallScript();
            }
            CallScript();
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnFetch_Click(object sender, EventArgs e)
    {
        try
        {
            BindDataGrids(true);
            //Commfun objComFun = new Commfun();
            //DataSet ds = new DataSet();
            //string strInput1 = string.Empty, strInput2 = string.Empty;
            //int intFilterType = -1;
            //strInput1 = txtAssignedFrom.Text;
            //strInput2 = txtAssignedTo.Text;
            //intFilterType = string.IsNullOrEmpty(ddlAssignBy.SelectedValue) ? 0 : Convert.ToInt32(ddlAssignBy.SelectedValue);
            //objComFun.FetchManualAllocation((rdoOnline.Checked) ? "ONLINE" : "OFFLINE", intFilterType, strInput1, strInput2, ref ds);
            //if (ds != null)
            //{
            //    for (int i = 0; i < ds.Tables.Count; i++)
            //    {
            //        if (ds.Tables[i] != null && ds.Tables[i].Rows.Count > 0)
            //        {
            //            if (Convert.ToString(ds.Tables[i].Rows[0]["Type"]).Equals("Application"))
            //            {
            //                Session["application"]=dgApplicationNo.DataSource = ds.Tables[i];
            //                dgApplicationNo.DataBind();
            //            }
            //            else if (Convert.ToString(ds.Tables[i].Rows[0]["Type"]).Equals("Eligible"))
            //            {
            //                Session["eligible"]=dgUserWithinLimit.DataSource = ds.Tables[i];
            //                dgUserWithinLimit.DataBind();
            //            }
            //            else if (Convert.ToString(ds.Tables[i].Rows[0]["Type"]).Equals("NotElegible"))
            //            {
            //                Session["noneligible"]=dgUserNotInLimit.DataSource = ds.Tables[i];
            //                dgUserNotInLimit.DataBind();
            //            }
            //        }
            //    }
            //}         
            divSaveButtonBlock.Style.Clear();
        }
        catch (Exception ex)
        {
            lblManualAllocationMsg.Text = "Try again later";
        }
        //CallScript();
    }
    protected void btnUpdateData_Click(object sender, EventArgs e)
    {
        try
        {
            divApplicationNumber.Style.Clear();
            divRemainingUsers.Style.Clear();
            divWithinRangeUsers.Style.Clear();
            CommonObject objCommonObj = new CommonObject();
            string strUserId = string.Empty;
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            if (Session["objCommonObj"] != null)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
            }
            Commfun objComm = new Commfun();
            int intRef = -1;
            objComm.ManageApplicationLifeCycle(hdnManageApplicationNo.Value, strUserId, "UW_MANUAL_ALLOCATION", false, ref intRef);
            /*END HERE*/
            if (hdnManageApplicationNo.Value.Equals("|"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Select Application No'); ", true);
                return;
            }
            if (hdnManageElegibleUser.Value.Equals("|"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Select Users No')", true);
                return;
            }
            Commfun objCommFun = new Commfun();
            int intRetVal = -1;
            //save into data base		
            objCommFun.ManageApplication(hdnManageApplicationNo.Value, hdnManageElegibleUser.Value, Convert.ToInt32(ddlAssignBy.SelectedValue),strUserId, ref intRetVal);
            //clear text and refill data		
            lblManualAllocationMsg.Text = "Records Updated Successfully";
            hdnManageApplicationNo.Value = string.Empty;
            hdnManageElegibleUser.Value = string.Empty;
            BindDataGrids(true);
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            objComm.ManageApplicationLifeCycle(hdnManageApplicationNo.Value, strUserId, "UW_MANUAL_ALLOCATION", true, ref intRef);
            /*END HERE*/
        }
        catch (Exception ex)
        {
            lblManualAllocationMsg.Text = "Try again later";
            Logger.Error(string.Empty + "STAG 2 :PageName :Manual Allocation .CS // MethodeName :Update Manual ALLocation Error :" + System.Environment.NewLine + ex.ToString());

        }
        //CallScript();
    }
    protected void rdoOnline_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            FillUserListDropdown();
            //CallScript();
        }
        catch (Exception ex)
        {

        }
    }
    protected void dgApplicationNo_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgApplicationNo.CurrentPageIndex = e.NewPageIndex;
            if (Session["application"] != null)
            {
                dgApplicationNo.DataSource = (DataTable)Session["application"];
                dgApplicationNo.DataBind();
            }
            //CallScript();
        }
        catch (Exception ex)
        {

        }
    }
    private void BindDataGrids(bool blnTrue)
    {
        //change current page index to default		
        dgApplicationNo.CurrentPageIndex = 0;
        dgUserNotInLimit.CurrentPageIndex = 0;
        dgUserWithinLimit.CurrentPageIndex = 0;
        //null datagrid		
        dgApplicationNo.DataSource = null;
        dgUserNotInLimit.DataSource = null;
        dgUserWithinLimit.DataSource = null;
        Commfun objComFun = new Commfun();
        DataSet ds = new DataSet();
        string strInput1 = string.Empty
            , strInput2 = string.Empty;
        int intFilterType = -1;
        strInput1 = txtAssignedFrom.Text;
        strInput2 = txtAssignedTo.Text;
        intFilterType = string.IsNullOrEmpty(ddlAssignBy.SelectedValue) ? 0 : Convert.ToInt32(ddlAssignBy.SelectedValue);
		//########################## Added by Shyam Starts ###########################################
		if (ddlBucket.SelectedValue == "Revival")
		{
			objComFun.FetchManualAllocationRevival((ddlChannel.SelectedValue.Trim().Equals("Online")) ? "ONLINE" : "OFFLINE", intFilterType, (intFilterType == 4) ? string.Concat("F", ddlUsers.SelectedValue) : strInput1, strInput2, ref ds);
		}
		else
		{
			objComFun.FetchManualAllocation((ddlChannel.SelectedValue.Trim().Equals("Online")) ? "ONLINE" : "OFFLINE", intFilterType, (intFilterType == 4) ? string.Concat("F", ddlUsers.SelectedValue) : strInput1, strInput2, ref ds);
		}
		//########################## Added by Shyam End ###########################################
		if (ds != null && ds.Tables.Count > 0)
        {
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                if (ds.Tables[i].Rows.Count > 0 && Convert.ToString(ds.Tables[i].Rows[0]["type"]) == "Application")
                {
                    divApplicationNumber.Style.Clear();
                    dgApplicationNo.Style.Clear();
                    Session["application"] = dgApplicationNo.DataSource = ds.Tables[i];
                }
                if (ds.Tables[i].Rows.Count > 0 && Convert.ToString(ds.Tables[i].Rows[0]["type"]) == "Eligible")
                {
                    divWithinRangeUsers.Style.Clear();
                    dgUserWithinLimit.Style.Clear();
                    Session["eligible"] = dgUserWithinLimit.DataSource = ds.Tables[i];
                }
                if (ds.Tables[i].Rows.Count > 0 && Convert.ToString(ds.Tables[i].Rows[0]["type"]) == "NotElegible")
                {
                    divRemainingUsers.Style.Clear();
                    dgUserNotInLimit.Style.Clear();
                    Session["noneligible"] = dgUserNotInLimit.DataSource = ds.Tables[i];
                }
            }
        }
        else
        {
            lblManualAllocationMsg.Text = "No Record Found";
        }
        dgApplicationNo.DataBind();
        dgUserNotInLimit.DataBind();
        dgUserWithinLimit.DataBind();
    }
    protected void dgUserWithinLimit_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgUserWithinLimit.CurrentPageIndex = e.NewPageIndex;
            if (Session["eligible"] != null)
            {
                dgUserWithinLimit.DataSource = (DataTable)Session["eligible"];
                dgUserWithinLimit.DataBind();
            }
            //CallScript();
        }
        catch (Exception ex)
        {

        }
    }
    protected void dgUserNotInLimit_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        try
        {
            dgUserNotInLimit.CurrentPageIndex = e.NewPageIndex;
            if (Session["noneligible"] != null)
            {
                dgUserNotInLimit.DataSource = (DataTable)Session["noneligible"];
                dgUserNotInLimit.DataBind();

            }
            //CallScript();
        }
        catch (Exception ex)
        {

        }
    }
    protected void dgApplicationNo_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            //Application No 1		
            string strPipe = "|";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (hdnManageApplicationNo.Value.Contains(strPipe + Convert.ToString(e.Item.Cells[1].Text) + strPipe))
                {
                    CheckBox cb = (CheckBox)e.Item.FindControl("chApplication");
                    if (cb != null)
                    {
                        cb.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void dgUserWithinLimit_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            //user id 1
            string strPipe = "|";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (hdnManageElegibleUser.Value.Contains(strPipe + Convert.ToString(e.Item.Cells[1].Text) + strPipe))
                {
                    CheckBox cb = (CheckBox)e.Item.FindControl("chEligibleUser");
                    if (cb != null)
                    {
                        cb.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void dgUserNotInLimit_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            //user id 1
            string strPipe = "|";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (hdnManageElegibleUser.Value.Contains(strPipe + Convert.ToString(e.Item.Cells[1].Text) + strPipe))
                {
                    CheckBox cb = (CheckBox)e.Item.FindControl("cbNonEligibleUser");
                    if (cb != null)
                    {
                        cb.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void CallScript()
    {
        //ddlAssignBy.SelectedIndex = 0;        
        if (ddlAssignBy.SelectedValue.Equals("1"))
        {
            divUserList.Style.Add("display", "none");
            divAssignedFrom.Style.Add("display", "block;");
            divAssignedTo.Style.Add("display", "block;");
        }
        else if (ddlAssignBy.SelectedValue.Equals("2"))
        {
            divUserList.Style.Add("display", "none");
            divAssignedFrom.Style.Add("display", "block;");
            divAssignedTo.Style.Add("display", "block;");
        }
        else if (ddlAssignBy.SelectedValue.Equals("3"))
        {
            divUserList.Style.Add("display", "none");
            divAssignedFrom.Style.Add("display", "block;");
            divAssignedTo.Style.Add("display", "block;");
        }
        else if (ddlAssignBy.SelectedValue.Equals("4"))
        {
            divUserList.Style.Add("display", "block;");
            divAssignedFrom.Style.Add("display", "none");
            divAssignedTo.Style.Add("display", "none;");
        }
        else if (ddlAssignBy.SelectedValue.Equals("5"))
        {
            divUserList.Style.Add("display", "none;");
            divAssignedFrom.Style.Add("display", "block");
            divAssignedTo.Style.Add("display", "none;");
        }
    }

    private void FillUserListDropdown()
    {
        Commfun objCommFun = new Commfun();
        DataSet _ds = new DataSet();
        objCommFun.FetchUser((ddlChannel.SelectedValue.Trim().Equals("Online")) ? "ONLINE" : "OFFLINE", ref _ds);
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            ddlUsers.DataSource = _ds;
            ddlUsers.DataTextField = "Text";
            ddlUsers.DataValueField = "Value";
            ddlUsers.DataBind();
        }
    }


    protected void ddlChannel_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            divApplicationNumber.Style.Add("display", "none;");
            divUserList.Style.Add("display", "none;");
            divWithinRangeUsers.Style.Add("display", "none;");

            dgApplicationNo.Style.Add("display", "none;");
            dgUserWithinLimit.Style.Add("display", "none;");
            dgUserNotInLimit.Style.Add("display", "none;");
            FillUserListDropdown();
            //CallScript();
        }
        catch (Exception ex)
        {

        }
    }
    protected void ddlBucket_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            divApplicationNumber.Style.Clear();
            divRemainingUsers.Style.Clear();
            divWithinRangeUsers.Style.Clear();

            if (Session["application"] != null)
            {
                DataTable dtApplication = null;
                dtApplication = (DataTable)Session["application"];
                string strBucket = ddlBucket.SelectedValue.Trim();
                if (!strBucket.Trim().Equals("All"))
                {
                    DataView dvFilter = new DataView(dtApplication);
                    dvFilter.RowFilter = "Bucket ='" + strBucket.ToUpper() + "'";
                    dgApplicationNo.DataSource = dvFilter;
                    dgApplicationNo.DataBind();
                }
                else
                {
                    dgApplicationNo.DataSource = dtApplication;
                    dgApplicationNo.DataBind();
                }
            }
            else
            {
                divApplicationNumber.Style.Add("display", "none;");
                divWithinRangeUsers.Style.Add("display", "none;");
                divRemainingUsers.Style.Add("display", "none;");

                dgApplicationNo.Style.Add("display", "none;");
                dgUserWithinLimit.Style.Add("display", "none;");
                dgUserNotInLimit.Style.Add("display", "none;");
            }
        }
        catch
        {

        }
    }
}