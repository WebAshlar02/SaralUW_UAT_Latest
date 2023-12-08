using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_DeclineThirdParty : System.Web.UI.Page
{
    protected DataSet ds { get; set; }
    BussLayer buss = new BussLayer();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region UnHold Cases
            SubmitHold.Visible = false;
            dynamic UnHoldValues = buss.GetUnHoldData();
            TableUnHoldCases.DataSource = UnHoldValues;
            TableUnHoldCases.DataBind();
            if (UnHoldValues.Tables[0].Rows.Count == 0)
            {
                SubmitUnHold.Visible = false;
            }
            else
            {
                SubmitUnHold.Visible = true;
            }
            #endregion
        }


    }



    protected void btn_Search(object sender, EventArgs e)
    {
        dynamic HoldValues = buss.GetHoldData(Request.Form["Application_number"]);
        TableHold.DataSource = HoldValues;
        TableHold.DataBind();

        if (HoldValues.Tables[0].Rows.Count == 0)
        {
            SubmitHold.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "NoData()", true);
        }
        else
        {
            SubmitHold.Visible = true;
        }

    }
    protected void Un_Hold_Case(object sender, EventArgs e)
    {
        var Application_Number = "";
        var Proposer_Name = "";
        var Policy_Status = "";
        var LA_Name = "";
        var Product_Name = "";
        var Created_By = "";
        var Created_Date = "";

        try
        {
            BussLayer buss = new BussLayer();
            foreach (GridViewRow gv in TableUnHoldCases.Rows)
            {
                if ((gv.FindControl("UnHoldCheckBox") as CheckBox).Checked == true)
                {
                    Application_Number = ((System.Web.UI.WebControls.Label)gv.FindControl("Application_Number")).Text;
                    Policy_Status = ((System.Web.UI.WebControls.Label)gv.FindControl("Policy_Status")).Text;
                    Proposer_Name = ((System.Web.UI.WebControls.Label)gv.FindControl("Proposer_Name")).Text;
                    LA_Name = ((System.Web.UI.WebControls.Label)gv.FindControl("LA_Name")).Text;
                    Product_Name = ((System.Web.UI.WebControls.Label)gv.FindControl("Product_Name")).Text;
                    Created_By = ((System.Web.UI.WebControls.Label)gv.FindControl("Created_By")).Text;
                    Created_Date = ((System.Web.UI.WebControls.Label)gv.FindControl("Created_Date")).Text;
                    buss.InsertUnHoldData(Application_Number, LA_Name, "HOLD", Policy_Status, Proposer_Name, Product_Name, Created_By, Created_Date);
                }
            }
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {
            string script = "toastr.info('" + ex.Message.Replace("'", "").ToString() + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
        }
    }
    protected void Hold_Cases(object sender, EventArgs e)
    {
        var PolicyNumber = "";
        var Application_Number = "";
        var Policy_Status = "";
        var Proposal_Name = "";
        var LA_Name = "";
        var Product_Name = "";
        var File_Name = "";
        var WithdrawReason = "";
        var objCommonObj = (CommonObject)Session["objCommonObj"];
        objCommonObj._ChannelType = "OFFLINE";
        Session["objCommonObj"] = objCommonObj;
        string strUserId = objCommonObj._Bpmuserdetails._UserID;
        try
        {
            BussLayer buss = new BussLayer();
            foreach (GridViewRow gv in TableHold.Rows)
            {
                if ((gv.FindControl("CheckboxHoldSelect") as CheckBox).Checked == true)
                {
                    Application_Number = ((System.Web.UI.WebControls.Label)gv.FindControl("Application_Number")).Text;
                    Policy_Status = ((System.Web.UI.WebControls.Label)gv.FindControl("Policy_Status")).Text;
                    Proposal_Name = ((System.Web.UI.WebControls.Label)gv.FindControl("Proposal_Name")).Text;
                    LA_Name = ((System.Web.UI.WebControls.Label)gv.FindControl("LA_Name")).Text;
                    Product_Name = ((System.Web.UI.WebControls.Label)gv.FindControl("Product_Name")).Text;
                    FileUpload UploadFile = (FileUpload)gv.FindControl("UploadFile");
                    var TPDropDown = "Third Party Payment";

                    if (!string.IsNullOrEmpty(UploadFile.FileName))
                    {
                            File_Name = "New_Business_" + Application_Number + "_Decline" + "." + UploadFile.FileName.Split('.')[1];
                            string targetFolder = ConfigurationSettings.AppSettings["HoldCasesFileUpload"];
                            UploadFile.SaveAs(targetFolder + "\\" + File_Name);
                            buss.InsertHoldData(Application_Number, LA_Name, "HOLD", Policy_Status, Proposal_Name, Product_Name, File_Name, strUserId);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "SuccessAlert()", true);
                            TableHold.Visible = false;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "Mand()", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string script = "toastr.info('" + ex.Message.Replace("'", "").ToString() + "');";
            ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", script, true);
            TableHold.Visible = false;
            SubmitHold.Visible = false;
        }
    }
}