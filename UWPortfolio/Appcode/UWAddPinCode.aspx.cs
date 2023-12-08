//*//*********************************************************************     
//*                      FUTURE GENERALI INDIA                        *    
//**********************************************************************/      
//*                  I N F O R M A T I O N                                       
//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win :  CR-30363 
// Date Of Creation     : 21/02/2022
// Description          : Add and delete the Negative Pincode
//**********************************************************************

//1.1 Start: Sushant Devkate MFL00905
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_UWAddPinCode : System.Web.UI.Page
{
    string ConString = ConfigurationManager.AppSettings["transactiondbLF"];
    CommonObject objCommonObj = new CommonObject();
    string strUserId = string.Empty;

     protected void Page_Load(object sender, EventArgs e)
    {
        objCommonObj = (CommonObject)Session["objCommonObj"];
       
        if (objCommonObj != null)
        {
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            if (!IsPostBack)
            {
                lblSuccess.Visible = false;
                lblSuccess.Text = "";
                BindGridView();
            }
        }
        else {
            Response.Redirect("../9011042143.aspx");
        }
    }

    public void BindGridView()
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection connection = new SqlConnection(ConString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("Usp_GetAllNegativePincode", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            connection.Close();
            BindData(ds);
        }
        catch (Exception ex)
        {
                 
        }

    }

    public void BindData(DataSet ds) 
    {
        DataTable dt = ds.Tables[0];

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind(); ;
        }
        else
        {
            dt.Rows.Add(dt.NewRow());
            GridView1.DataSource = dt;
            GridView1.DataBind();
            int columncount = GridView1.Rows[0].Cells.Count;
            GridView1.Rows[0].Cells.Clear();
            GridView1.Rows[0].Cells.Add(new TableCell());
            GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
            GridView1.Rows[0].Cells[0].Text = "No Records Found";
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            PinCodeDO ObjPinCode = new PinCodeDO();
            ObjPinCode.CircleName = txtCircleName.Text.Trim();
            ObjPinCode.RegionName = txtRegionName.Text.Trim();
            ObjPinCode.DivisionName = txtDivisionName.Text.Trim();
            ObjPinCode.OfficeName = txtOfficeName.Text.Trim();
            ObjPinCode.PinCode = txtPinCode.Text.Trim();
            ObjPinCode.OfficeType = txtOfficeType.Text.Trim();
            ObjPinCode.District = txtDistrict.Text.Trim();
            ObjPinCode.Delivery = txtDelivery.Text.Trim();
            ObjPinCode.StateName = txtStateName.Text.Trim();
            ObjPinCode.Remarks = txtRemarks.Text.Trim();
            ObjPinCode.UserId = strUserId;
            int Result = new Commfun().InsertPinCode(ObjPinCode);
            if (Result > 0)
            {
                clearvalue();
                txtPincodeSearch.Text = "";
                 BindGridView();
                // ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('Records has inserted successfully.');</script>", false);
                lblSuccess.Visible = true;
                lblSuccess.Text = "Record has inserted successfully";
                lblSuccess.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }catch(Exception ex)
        {

        }
    }


    public void clearvalue()
    {
        txtCircleName.Text = "";
        txtRegionName.Text = "";
        txtDivisionName.Text = "";
        txtOfficeName.Text = "";
        txtPinCode.Text = "";
        txtOfficeType.Text = "";
        txtDistrict.Text = "";
        txtDelivery.Text = "";
        txtStateName.Text = "";
        txtRemarks.Text = "";
    


    }

     protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            int Result = new Commfun().DeletePinCode(ID, strUserId);
            if (Result > 0)
            {
                clearvalue();
                txtPincodeSearch.Text = "";
                //ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('Records has deleted successfully.');</script>", false);
                BindGridView();
                lblSuccess.Visible = true;
                lblSuccess.Text = "Record has deleted successfully.";
                lblSuccess.ForeColor = System.Drawing.Color.DarkGreen;
               
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        btnSearch_Click(null, null);
        clearvalue();
        lblSuccess.Visible = false;
        lblSuccess.Text = "";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblSuccess.Visible = false;
        lblSuccess.Text = "";
        DataSet ds = new Commfun().GetSearchByPincode(txtPincodeSearch.Text.Trim());
         BindData(ds);
        clearvalue();
        

    }

    protected void btnreset_Click(object sender, EventArgs e)
    {
        lblSuccess.Visible = false;
        lblSuccess.Text = "";
        clearvalue();
        txtPincodeSearch.Text = "";
        BindGridView();

    }
}
  //1.1 End: Sushant Devkate MFL00905