//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 18/11/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_UWAssignment_AddNewGP : System.Web.UI.Page
{
    #region 1.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    //1.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    CommanAssignmentDetails commobj = new CommanAssignmentDetails();
    string strUserId = string.Empty;
    string strUserGroup = string.Empty;
    string struserName = string.Empty;
    CommonObject objCommonObj = new CommonObject();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Binddropdown();
            TextClear();
            divfromrange.Visible = false;
            divtorange.Visible = false;

            if (Session["objLoginObj"] != null)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                struserName = objCommonObj._Bpmuserdetails._UserName;
            }
        }
        else
        {

            if (Session["objLoginObj"] != null)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                struserName = objCommonObj._Bpmuserdetails._UserName;
            }
        }
            
    }

    public void Binddropdown()
    {
        DataSet _dsFilldata = new DataSet();
        commobj.OnlineMasterUnderWriting_AssignmentDetails_GET("DropDownListValue", ref _dsFilldata);
        ddlgp.Items.Clear();
        ddlgp.Items.Insert(0, new ListItem("Please select ", ""));
        ddlgp.SelectedValue = "";
        foreach (DataRow row in _dsFilldata.Tables[6].Rows)
        {

            if (row["value"] != null)
                ddlgp.DataValueField = row["value"].ToString();
            else
                ddlgp.DataValueField = "";

            if (row["name"] != null)
                ddlgp.DataTextField = row["name"].ToString();
            else
                ddlgp.DataTextField = "";

            ddlgp.Items.Add(new ListItem(ddlgp.DataTextField, ddlgp.DataValueField));

        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int result = 0;
        int torange = 0;
        int fromrange = 0;
        if (txttorange.Text!=""||txtfrmrange.Text!="")
        {
            
            fromrange = Convert.ToInt32(txtfrmrange.Text);
            torange = Convert.ToInt32(txttorange.Text);
        }
        else
        {
            fromrange = 0;
            torange = 0;

        }

        commobj.Underwriting_AssignmentDetails_AddnewGroup_Save("Save", ddlgp.SelectedItem.Text.Trim().ToString(), txtdis.Text.Trim().ToString(), fromrange, torange, strUserId, ref result);
        if (result > 0)
        {
            DataSet ds1 = new DataSet();
            commobj.UW_AddnewGP_GETDATA("GetData", ref ds1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds1.Tables[0].Rows)
                {
                    commobj.UW_AddNewGP_Log("logs",strUserId, struserName, "Insert", Convert.ToInt32(row["ID"]), row["Type"].ToString(), row["value"].ToString(), row["FromRange"].ToString(),
                       row["ToRange"].ToString(), row["SystemDate"].ToString(), ref result);
                    if (result > 0)
                    {
                        string script = "<script type=\"text/javascript\">alert('Save Data Successfuly.....!');</script>";
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
                        TextClear();
                    }

                }
            }

                
        }
        else
        {
            string script = "<script type=\"text/javascript\">alert('"+ txtdis.Text + " Group already exists in Buckest.......!');</script>";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script);
           
        }
    }

    protected void TextClear()
    {
        try
        {
            ddlgp.ClearSelection();
            txtdis.Text = "";
            txtfrmrange.Text = "";
            txttorange.Text = "";

        }
        catch (Exception ex)
        {
            throw ex;
        }



    }

    protected void ddlgp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlgp.SelectedItem.Text.Trim().ToString() == "Limit")
        {
            divfromrange.Visible = true;
            divtorange.Visible = true;
        }
        else
        {
            divfromrange.Visible = false;
            divtorange.Visible = false;
        }
    }

    protected void btncleare_Click(object sender, EventArgs e)
    {

        try
        {
            TextClear();
            divfromrange.Visible = false;
            divtorange.Visible = false;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //1.1 End of Changes; Bhaumik Patel - [CR-5307]
    #endregion 1.1 End of Changes; Bhaumik Patel - [CR-5307]
}