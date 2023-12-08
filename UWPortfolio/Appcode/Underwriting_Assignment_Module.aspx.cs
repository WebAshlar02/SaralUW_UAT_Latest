//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 18/11/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************
//**********************************************************************
// Sr. No.              : 2
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel [WebAshlar02]
// BRD/CR/Codesk No/Win : CR-5855  
// Date Of Creation     : 04-05-2023
// Description          : Grid based Loading access for Counter offer in Saral.
//**********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_Underwriting_Assignment_Module : System.Web.UI.Page
{

    #region 1.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    //1.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    CommanAssignmentDetails commobj = new CommanAssignmentDetails();
    string strUserId = string.Empty;
    string strUserGroup = string.Empty;
    public string strPolicyEnquiery = string.Empty;
    int Result = 0;
    string struserName = string.Empty;

    CommonObject objCommonObj = new CommonObject();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["objLoginObj"] != null)
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            struserName = objCommonObj._Bpmuserdetails._UserName;
        }
        else
        {
            Response.Redirect("~/9011042143.aspx");
        }
        lblusername.Text = struserName;
        if (!IsPostBack)
        {
            try
            {
               
                TextClear();
                FillMasterDetails();
                BindGrid();
                divid.Visible = false;
                DataSet ds = new DataSet();
                commobj.UnderWriting_AssignmentUserAccess_GET_byUserID("Check_SpecialUser_Access", strUserId, ref ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    divAddNewGroup.Visible = true;
                }
                else
                {
                    divAddNewGroup.Visible = false;
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        else
        {
            BindGrid();
        }


    }

    public void FillMasterDetails()
    {
        try
        {
            DataSet _dsFilldata = new DataSet();
            commobj.OnlineMasterUnderWriting_AssignmentDetails_GET("DropDownListValue", ref _dsFilldata);

            if (_dsFilldata.Tables.Count > 0)
            {
                ddlbucket.Items.Clear();
                ddlbucket.Items.Insert(0, new ListItem("Please select ", ""));
                ddlbucket.SelectedValue = "";
                foreach (DataRow row in _dsFilldata.Tables[0].Rows)
                {

                    if (row["value"] != null)
                        ddlbucket.DataValueField = row["value"].ToString();
                    else
                        ddlbucket.DataValueField = "";

                    if (row["name"] != null)
                        ddlbucket.DataTextField = row["name"].ToString();
                    else
                        ddlbucket.DataTextField = "";

                    ddlbucket.Items.Add(new ListItem(ddlbucket.DataTextField));

                }

                ddllimit.Items.Clear();
                ddllimit.SelectedValue = "";
                ddllimit.Items.Insert(0, new ListItem("Please select ", ""));
                foreach (DataRow row in _dsFilldata.Tables[1].Rows)
                {

                    if (row["value"] != null)
                        ddllimit.DataValueField = row["value"].ToString();
                    else
                        ddllimit.DataValueField = "";

                    if (row["name"] != null)
                        ddllimit.DataTextField = row["name"].ToString();
                    else
                        ddllimit.DataTextField = "";

                    ddllimit.Items.Add(new ListItem(ddllimit.DataTextField));

                }
                chkallocation.Items.Clear();
                chkallocation.SelectedValue = "";

                foreach (DataRow row in _dsFilldata.Tables[2].Rows)
                {
                    ListItem item = new ListItem();
                    if (row["value"] != null)
                        item.Value = row["value"].ToString();
                    else
                        item.Value = "";

                    if (row["name"] != null)
                        item.Text = row["name"].ToString();
                    else
                        item.Text = "";

                    chkallocation.Items.Add(item);

                }
                chkdecision.Items.Clear();
                chkdecision.SelectedValue = "";
                foreach (DataRow row in _dsFilldata.Tables[3].Rows)
                {
                    ListItem item = new ListItem();
                    if (row["value"] != null)
                        item.Value = row["value"].ToString();
                    else
                        item.Value = "";

                    if (row["name"] != null)
                        item.Text = row["name"].ToString();
                    else
                        item.Text = "";

                    chkdecision.Items.Add(item);

                }
                chkcategory.Items.Clear();
                chkcategory.SelectedValue = "";

                foreach (DataRow row in _dsFilldata.Tables[4].Rows)
                {
                    ListItem item = new ListItem();
                    if (row["value"] != null)
                        item.Value = row["value"].ToString();
                    else
                        item.Value = "";

                    if (row["name"] != null)
                        item.Text = row["name"].ToString();
                    else
                        item.Text = "";

                    chkcategory.Items.Add(item);

                }

                chkuserid.Items.Clear();
                chkuserid.SelectedValue = "";

                foreach (DataRow row in _dsFilldata.Tables[5].Rows)
                {
                    ListItem item = new ListItem();
                    if (row["value"] != null)
                        item.Value = row["value"].ToString();
                    else
                        item.Value = "";

                    if (row["name"] != null)
                        item.Text = row["name"].ToString();
                    else
                        item.Text = "";

                    chkuserid.Items.Add(item);

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
        try
        {
            int result = 0;

            if (chkuserid.Items.Count > 0)
            {
                lblcheckboxlistvalue4.Text = "";
                string selectedItemsuser = String.Join(",",
                 chkuserid.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue4.Text = selectedItemsuser;

                lblcheckboxlistvalue5.Text = "";
                string selectedItemsuser1 = String.Join(",",
                 chkuserid.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Value));
                lblcheckboxlistvalue5.Text = selectedItemsuser1;
            }
            if (chkallocation.Items.Count > 0)
            {
                lblcheckboxlistvalue1.Text = "";
                string selectedItemsAllocationParameter = String.Join(",",
                 chkallocation.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue1.Text = selectedItemsAllocationParameter;
            }
            if (chkdecision.Items.Count > 0)
            {
                lblcheckboxlistvalue2.Text = "";
                string selectedItemsDecision = String.Join(",",
                 chkdecision.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue2.Text = selectedItemsDecision;
            }
            if (chkcategory.Items.Count > 0)
            {
                lblcheckboxlistvalue3.Text = "";
                string selectedItemsCategory = String.Join(",",
                 chkcategory.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue3.Text = selectedItemsCategory;
            }
            DataSet ds = new DataSet();
            commobj.UnderWriting_AssignmentDetails_CheckUWbucket("Check_UWBucket", ddlbucket.SelectedItem.Text, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                    BindGrid();
                    TextClear();
                    string script = "<script type=\"text/javascript\">alert('UW Buckets Already Exists In DataBase .....!');</script>";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
            }
            else
            {
                commobj.Underwriting_AssignmentDetails_Save("Save", ddlbucket.SelectedItem.Text.Trim().ToString(), lblcheckboxlistvalue5.Text.Trim().ToString(), lblcheckboxlistvalue4.Text.Trim().ToString(), ddllimit.SelectedItem.Text.Trim().ToString(),
                lblcheckboxlistvalue1.Text.Trim().ToString(), lblcheckboxlistvalue2.Text.Trim().ToString(), lblcheckboxlistvalue3.Text.Trim().ToString(),
                txtallocationlimit.Text.Trim().ToString(), ref result);
                if (result > 0)
                {
                    DataSet ds1 = new DataSet();
                    commobj.UnderWriting_AssignmentDetails_GETDATA("GetData",0, ref ds1);
                    if(ds1.Tables[0].Rows.Count>0)
                    {
                        foreach(DataRow row in ds1.Tables[0].Rows)
                        {
                            commobj.Underwriting_AssignmentDetails_ProcessLog(strUserId, struserName, "INSERT", Convert.ToInt32(row["ID"]), row["UW_Bucket"].ToString(), row["UserID"].ToString(), row["Limit"].ToString(),
                            row["Allocation_Parameters"].ToString(), row["Decision_Rights"].ToString(), row["Risk_category"].ToString(), row["Allocation_Limit"].ToString(), ref result);
                            if (result > 0)
                            {
                                string script = "<script type=\"text/javascript\">alert('Save Data Successfuly.....!');</script>";
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                                BindGrid();
                                TextClear();
                            }
                                
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

    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
            if (chkuserid.Items.Count > 0)
            {
                lblcheckboxlistvalue4.Text = "";
                string selectedItemsuser = String.Join(",",
                 chkuserid.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue4.Text = selectedItemsuser;

                lblcheckboxlistvalue5.Text = "";
                string selectedItemsuser1 = String.Join(",",
                 chkuserid.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Value));
                lblcheckboxlistvalue5.Text = selectedItemsuser1;
            }
            if (chkallocation.Items.Count > 0)
            {
                lblcheckboxlistvalue1.Text = "";
                string selectedItemsAllocationParameter = String.Join(",",
                 chkallocation.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue1.Text = selectedItemsAllocationParameter;
            }
            if (chkdecision.Items.Count > 0)
            {
                lblcheckboxlistvalue2.Text = "";
                string selectedItemsDecision = String.Join(",",
                 chkdecision.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue2.Text = selectedItemsDecision;
            }
            if (chkcategory.Items.Count > 0)
            {
                lblcheckboxlistvalue3.Text = "";
                string selectedItemsCategory = String.Join(",",
                 chkcategory.Items.OfType<ListItem>().Where(r => r.Selected)
                .Select(r => r.Text));
                lblcheckboxlistvalue3.Text = selectedItemsCategory;
            }

            commobj.UnderWriting_AssignmentDetails_Update
                ("Update", ddlbucket.SelectedItem.Text.Trim().ToString(), lblcheckboxlistvalue5.Text.Trim().ToString(), lblcheckboxlistvalue4.Text.Trim().ToString(), ddllimit.SelectedItem.Text.Trim().ToString(),
                lblcheckboxlistvalue1.Text.Trim().ToString(), lblcheckboxlistvalue2.Text.Trim().ToString(), lblcheckboxlistvalue3.Text.Trim().ToString(),
                txtallocationlimit.Text.Trim().ToString(), Convert.ToInt32(txtid.Text), ref result);

            if (result > 0)
            {
                DataSet ds1 = new DataSet();
                commobj.UnderWriting_AssignmentDetails_GETDATA("GetAssignmentData_ByID", Convert.ToInt32(txtid.Text), ref ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        commobj.Underwriting_AssignmentDetails_ProcessLog(strUserId, struserName, "UPDATE", Convert.ToInt32(row["ID"]), row["UW_Bucket"].ToString(), row["UserID"].ToString(), row["Limit"].ToString(),
                        row["Allocation_Parameters"].ToString(), row["Decision_Rights"].ToString(), row["Risk_category"].ToString(), row["Allocation_Limit"].ToString(), ref result);
                        if (result > 0)
                        {
                            string script = "<script type=\"text/javascript\">alert('Updated Data Successfuly.....!');</script>";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                            BindGrid();
                            TextClear();
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

    protected void btncleare_Click(object sender, EventArgs e)
    {
        try
        {
            TextClear();
            divsave.Visible = true;
            divupdate.Visible = false;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void BindGrid()
    {
        try
        {
            DataSet _ds = new DataSet();
            commobj.UnderWriting_AssignmentDetails_GET("GetGriddata", ref _ds);

            if (_ds.Tables[0].Rows.Count > 0)
            {
                rp_Assignment.DataSource = _ds.Tables[0];
                rp_Assignment.DataBind();
            }
            else
            {
                rp_Assignment.DataSource = null;
                rp_Assignment.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void TextClear()
    {
        try
        {
            ddlbucket.ClearSelection();
            ddllimit.ClearSelection();
            chkallocation.ClearSelection();
            chkdecision.ClearSelection();
            chkuserid.ClearSelection();
            chkcategory.ClearSelection();
            txtallocationlimit.Text = string.Empty;
            lblcheckboxlistvalue1.Text = "";
            lblcheckboxlistvalue2.Text = "";
            lblcheckboxlistvalue3.Text = "";
            divid.Visible = false;
            rfvbucket.Text = "";
            rfvlimit.Text = "";
            rfvuserid.Text = "";
            rfvallocationlimit.Text = "";
            divsave.Visible = true;
            divupdate.Visible = false;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rp_Assignment_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int result = 0;
            DataSet _ds = new DataSet();
            switch (e.CommandName)
            {

                case ("Delete"):
                    int id = Convert.ToInt32(e.CommandArgument);
                    commobj.UnderWriting_AssignmentDetails_GET_byId("GetAssignmentData_ByID", id, ref _ds);
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        foreach(DataRow row in _ds.Tables[0].Rows)
                        {
                            commobj.Underwriting_AssignmentDetails_ProcessLog(strUserId, struserName, "DELETE", Convert.ToInt32(row["ID"]), row["UW_Bucket"].ToString(), row["UserID"].ToString(), row["Limit"].ToString(),
                            row["Allocation_Parameters"].ToString(), row["Decision_Rights"].ToString(), row["Risk_category"].ToString(), row["Allocation_Limit"].ToString(), ref result);
                        }
                        
                    }
                    commobj.UnderWriting_AssignmentDetails_Delete("Delete", id, ref result);
                    if (result > 0)
                    {
                        string script = "<script type=\"text/javascript\">alert('Deleted Data Successfuly.....!');</script>";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                        TextClear();
                        BindGrid();
                    }

                    break;
                case ("Edit"):
                    id = Convert.ToInt32(e.CommandArgument);
                    divid.Visible = true;
                    divupdate.Visible = true;
                    divsave.Visible = false;
                    commobj.UnderWriting_AssignmentDetails_GET_byId("GetAssignmentData_ByID", id, ref _ds);
                    editdatabind(_ds);
                    break;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void editdatabind(DataSet _ds)
    {
        try
        {
            if (_ds.Tables[0].Rows.Count > 0)
            {
                chkuserid.ClearSelection();
                chkdecision.ClearSelection();
                chkallocation.ClearSelection();
                chkcategory.ClearSelection();
                foreach (DataRow row in _ds.Tables[0].Rows)
                {

                    txtid.Text = row["ID"].ToString();
                    divid.Visible = false;
                    if (row["UW_Bucket"] != null)
                        ddlbucket.SelectedValue = row["UW_Bucket"].ToString();
                    else
                        ddlbucket.SelectedValue = "";

                    for (int i = 0; i < chkuserid.Items.Count; i++)
                    {
                        string parameters4 = row["UserName"].ToString();
                        for (int j = 0; j < parameters4.Split(',').Length; j++)
                        {
                            if (chkuserid.Items[i].Text.Trim() == parameters4.Split(',')[j].Trim())
                            {
                                chkuserid.Items[i].Selected = true;
                                break;
                            }
                        }

                    }


                    if (row["Limit"] != null)
                        ddllimit.SelectedValue = row["Limit"].ToString().Trim();
                    else
                        ddllimit.SelectedValue = "";

                    for (int i = 0; i < chkallocation.Items.Count; i++)
                    {
                        string parameters1 = row["Allocation_Parameters"].ToString();
                        for (int j = 0; j < parameters1.Split(',').Length; j++)
                        {
                            if (chkallocation.Items[i].Text.Trim() == parameters1.Split(',')[j].Trim())
                            {
                                chkallocation.Items[i].Selected = true;
                                break;
                            }
                        }

                    }

                    for (int i = 0; i < chkdecision.Items.Count; i++)
                    {
                        string parameters2 = row["Decision_Rights"].ToString();
                        for (int j = 0; j < parameters2.Split(',').Length; j++)
                        {
                            if (chkdecision.Items[i].Text.Trim() == parameters2.Split(',')[j].Trim())
                            {
                                chkdecision.Items[i].Selected = true;
                                break;
                            }
                        }

                    }

                    for (int i = 0; i < chkcategory.Items.Count; i++)
                    {
                        string parameters3 = row["Risk_category"].ToString();
                        for (int j = 0; j < parameters3.Split(',').Length; j++)
                        {
                            if (chkcategory.Items[i].Text.Trim() == parameters3.Split(',')[j].Trim())
                            {
                                chkcategory.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                    if (row["Allocation_Limit"] != null)
                        txtallocationlimit.Text = row["Allocation_Limit"].ToString();
                    else
                        txtallocationlimit.Text = "";

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('UWAssignment_AddNewGP.aspx','mywindow','menubar=1,resizable=1,width=600,height=600,left=450,top=100;');", true);
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }

    //1.1 End of Changes; Bhaumik Patel - [CR-5307]
    #endregion 1.1 End of Changes; Bhaumik Patel - [CR-5307]

    protected void btnPriority_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('UWAllocation_Priority.aspx','mywindow','menubar=1,resizable=1,width=600,height=600,left=450,top=100;');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    ////2.1 Begin of Changes; Bhaumik Patel - [CR-5855]
    //protected void btnMortality_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('UserAccessFlatMortality.aspx','mywindow','menubar=1,resizable=1,width=600,height=600,left=450,top=100;');", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    ////2.1 END of Changes; Bhaumik Patel - [CR-5855]
}