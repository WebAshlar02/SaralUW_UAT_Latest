
//**********************************************************************
// Sr. No.              : 1
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

public partial class Appcode_UserAccessFlatMortality : System.Web.UI.Page
{
    //1.1 Begin of Changes; Bhaumik Patel - [CR-5855]
    string strUserId = string.Empty;
    string strUserGroup = string.Empty;
    public string strPolicyEnquiery = string.Empty;
    int Result = 0;
    string struserName = string.Empty;

    CommonObject objCommonObj = new CommonObject();

    CommanAssignmentDetails commobj = new CommanAssignmentDetails();
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
            commobj.OnlineMasterUser_FlatMortalityDetails_GET("DropDownListValue", ref _dsFilldata);

            if (_dsFilldata.Tables.Count > 0)
            {

                ddluserid.Items.Clear();
                ddluserid.Items.Insert(0, new ListItem("Please select ", ""));
                ddluserid.SelectedValue = "";

                foreach (DataRow row in _dsFilldata.Tables[0].Rows)
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

                    ddluserid.Items.Add(item);

                }
                ddlMortality.Items.Clear();
                ddlMortality.Items.Insert(0, new ListItem("Please select ", ""));
                ddlMortality.SelectedValue = "";
                foreach (DataRow row in _dsFilldata.Tables[1].Rows)
                {

                    if (row["value"] != null)
                        ddlMortality.DataValueField = row["value"].ToString();
                    else
                        ddlMortality.DataValueField = "";

                    if (row["name"] != null)
                        ddlMortality.DataTextField = row["name"].ToString();
                    else
                        ddlMortality.DataTextField = "";

                    ddlMortality.Items.Add(new ListItem(ddlMortality.DataTextField,ddlMortality.DataValueField));

                }

                ddllimit.Items.Clear();
                ddllimit.SelectedValue = "";
                ddllimit.Items.Insert(0, new ListItem("Please select ", ""));
                foreach (DataRow row in _dsFilldata.Tables[2].Rows)
                {

                    if (row["value"] != null)
                        ddllimit.DataValueField = row["value"].ToString();
                    else
                        ddllimit.DataValueField = "";

                    if (row["name"] != null)
                        ddllimit.DataTextField = row["name"].ToString();
                    else
                        ddllimit.DataTextField = "";

                    ddllimit.Items.Add(new ListItem(ddllimit.DataTextField, ddllimit.DataValueField));

                }
               



            }
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
            commobj.FlatMortalityDetails_GET("GetGriddata", ref _ds);

            if (_ds.Tables[0].Rows.Count > 0)
            {
                rp_Mortality.DataSource = _ds.Tables[0];
                rp_Mortality.DataBind();
            }
            else
            {
                rp_Mortality.DataSource = null;
                rp_Mortality.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Btnsave_Click(object sender, EventArgs e)
    {
        int result = 0;
        DataSet _dsRate = new DataSet();
        commobj.OnlineFlatMortalityDetails_GET_RateAdjust("GetData_ByUSERID", ddluserid.SelectedValue, ref _dsRate);
        if (_dsRate.Tables[0].Rows.Count > 0)
        {
          
            string script = "<script type=\"text/javascript\">alert('" + ddluserid.SelectedItem.Text + " Already Exists In DataBase .....!');</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
            BindGrid();
            TextClear();
        }
        else
        {
            commobj.FlatMortalityDetails_Save("Save", ddluserid.SelectedValue.Trim().ToString(), ddluserid.SelectedItem.Text.Trim().ToString(), 
                ddlMortality.SelectedItem.Text.Trim().ToString(), ddlMortality.SelectedValue.Trim().ToString(),
            strUserId.Trim().ToString(), ddllimit.SelectedValue .Trim().ToString(),ref result);
            if (result > 0)
            {
                DataSet ds1 = new DataSet();
                commobj.FlatMortalityDetails_GET_byId("GetData", 0, ref ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        commobj.FlatMortality_ProcessLog(row["UserID"].ToString(), row["UserName"].ToString(),  "INSERT", Convert.ToInt32(row["ID"]), row["Mortality"].ToString(),  row["MortalityCode"].ToString(),
                        row["Flat_Extra"].ToString(),strUserId,  ref result);
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
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            int result = 0;
           

            commobj.FlatMortalityDetails_Update("UPDATE", ddluserid.SelectedValue.Trim().ToString(), ddluserid.SelectedItem.Text.Trim().ToString(),
                ddlMortality.SelectedItem.Text.Trim().ToString(), ddlMortality.SelectedValue.Trim().ToString(),
            strUserId.Trim().ToString(), ddllimit.SelectedValue.Trim().ToString(), Convert.ToInt32(txtid.Text), ref result);

            if (result > 0)
            {
                DataSet ds1 = new DataSet();
                commobj.FlatMortalityDetails_GET_byId("FlatMortalityDetails_GET_byId", Convert.ToInt32(txtid.Text), ref ds1);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds1.Tables[0].Rows)
                    {
                        commobj.FlatMortality_ProcessLog(row["UserID"].ToString(), row["UserName"].ToString(), "UPDATE", Convert.ToInt32(row["ID"]), row["Mortality"].ToString(), row["MortalityCode"].ToString(),
                           row["Flat_Extra"].ToString(), strUserId, ref result);
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
    protected void btnclear_Click(object sender, EventArgs e)
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
    protected void TextClear()
    {
        try
        {
            ddlMortality.ClearSelection();
            ddllimit.ClearSelection();
            ddluserid.ClearSelection();
            lblcheckboxlistvalue1.Text = "";
            lblcheckboxlistvalue2.Text = "";
            lblcheckboxlistvalue3.Text = "";
            divid.Visible = false;
            rfvlimit.Text = "";
            rfvuserid.Text = "";
            divsave.Visible = true;
            divupdate.Visible = false;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void rp_Mortality_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            int result = 0;
            DataSet _ds = new DataSet();
            switch (e.CommandName)
            {

                case ("Delete"):
                    int id = Convert.ToInt32(e.CommandArgument);
                    commobj.FlatMortalityDetails_GET_byId("GetFlatMortalityData_ByID", id, ref _ds);
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in _ds.Tables[0].Rows)
                        {
                            commobj.FlatMortality_ProcessLog(row["UserID"].ToString(), row["UserName"].ToString(), "DELETE", Convert.ToInt32(row["ID"]), row["Mortality"].ToString(), row["MortalityCode"].ToString(),
                        row["Flat_Extra"].ToString(), strUserId, ref result);
                        }

                    }
                    commobj.FlatMortalityDetails_Delete("DELETE", id, ref result);
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
                    commobj.FlatMortalityDetails_GET_byId("GetFlatMortalityData_ByID", id, ref _ds);
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
                ddluserid.ClearSelection();
                ddlMortality.ClearSelection();
                ddllimit.ClearSelection();
                foreach (DataRow row in _ds.Tables[0].Rows)
                {

                    txtid.Text = row["ID"].ToString();
                    divid.Visible = false;
                   

                    for (int i = 0; i < ddluserid.Items.Count; i++)
                    {
                        string parameters4 = row["UserName"].ToString();
                        for (int j = 0; j < parameters4.Split(',').Length; j++)
                        {
                            if (ddluserid.Items[i].Text.Trim() == parameters4.Split(',')[j].Trim())
                            {
                                ddluserid.Items[i].Selected = true;
                                break;
                            }
                        }

                    }

                    for (int i = 0; i < ddlMortality.Items.Count; i++)
                    {
                        string parameters3 = row["Mortality"].ToString();
                        for (int j = 0; j < parameters3.Split(',').Length; j++)
                        {
                            if (ddlMortality.Items[i].Text.Trim() == parameters3.Split(',')[j].Trim())
                            {
                                ddlMortality.Items[i].Selected = true;
                                break;
                            }
                        }
                    }


                    for (int i = 0; i < ddllimit.Items.Count; i++)
                    {
                        string parameters2 = row["Flat_Extra"].ToString();
                        for (int j = 0; j < parameters2.Split(',').Length; j++)
                        {
                            if (ddllimit.Items[i].Text.Trim() == parameters2.Split(',')[j].Trim())
                            {
                                ddllimit.Items[i].Selected = true;
                                break;
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

    //1.1 END of Changes; Bhaumik Patel - [CR-5855]
}