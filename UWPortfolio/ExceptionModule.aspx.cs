

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

public partial class ExceptionModule : System.Web.UI.Page
{
    Commfun objComm = new Commfun();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            getLAData();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void setbypassflagddl()
    {
        try
        {
            DataSet ds = objComm.ByPassDDL(txtAppno.Text);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //string text = "Yes";
                    //var item = ddlLABypassFlag.Items.FindByText(text);
                    //if (item != null)
                    //    item.Selected = true;
                    ddlLABypassFlag.ClearSelection(); //making sure the previous selection has been cleared
                    ddlLABypassFlag.Items.FindByText(ds.Tables[0].Rows[0][0].ToString() == "Y" ? "Yes" : "No").Selected = true;
                    //ddlLABypassFlag.SelectedValue = (ds.Tables[0].Rows[0][0].ToString() == "Y" ? "1":"0");
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    ddlProposer.ClearSelection(); //making sure the previous selection has been cleared
                    ddlProposer.Items.FindByText(ds.Tables[1].Rows[0][0].ToString() == "Y" ? "Yes" : "No").Selected = true;
                    //ddlProposer.SelectedIndex = (ds.Tables[1].Rows[0][0].ToString() == "Y" ? 1 : 0);
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    ddlPayer.ClearSelection(); //making sure the previous selection has been cleared
                    ddlPayer.Items.FindByText(ds.Tables[2].Rows[0][0].ToString() == "Y" ? "Yes" : "No").Selected = true;
                    //ddlPayer.SelectedIndex = (ds.Tables[2].Rows[0][0].ToString() == "Y" ? 1 : 0);
                }
            }
           
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlLABypassFlag_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    public void getLAData()
    {
        try
        {
            DataSet ds = objComm.GetLABypassFlagData(txtAppno.Text);
            if (ds != null)
            {
                //Current LA
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LACurrentGrid.DataSource = ds.Tables[0];
                    LACurrentGrid.DataBind();
                    LACurrentGrid.Visible = true;
                    //other
                }
                else
                {
                    LACurrentGrid.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    LACurrentGrid.DataBind();
                }
                //Matching LA
                if (ds.Tables[1].Rows.Count > 0)
                {
                    LAMatchingGrid.DataSource = ds.Tables[1];
                    LAMatchingGrid.DataBind();
                    LAMatchingGrid.Visible = true;
                    //other
                }
                else
                {
                    LAMatchingGrid.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    LAMatchingGrid.DataBind();
                }
                //Current Proposer
                if (ds.Tables[2].Rows.Count > 0)
                {
                    ProposerCGridView.DataSource = ds.Tables[2];
                    ProposerCGridView.DataBind();
                    ProposerCGridView.Visible = true;
                    //other
                }
                else
                {
                    ProposerCGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    LACurrentGrid.DataBind();
                }

                //Matching Proposer
                if (ds.Tables[3].Rows.Count > 0)
                {
                    ProposerMGridView.DataSource = ds.Tables[3];
                    ProposerMGridView.DataBind();
                    ProposerMGridView.Visible = true;
                    //other
                }
                else
                {
                    ProposerMGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    ProposerMGridView.DataBind();
                }


                //Current Payer
                if (ds.Tables[4].Rows.Count > 0)
                {
                    PayerCGridView.DataSource = ds.Tables[4];
                    PayerCGridView.DataBind();
                    PayerCGridView.Visible = true;
                    //other
                }
                else
                {
                    PayerCGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    PayerCGridView.DataBind();
                }
                //MAtching Payer
                if (ds.Tables[5].Rows.Count > 0)
                {
                    PayerMGridView.DataSource = ds.Tables[5];
                    PayerMGridView.DataBind();
                    PayerMGridView.Visible = true;
                    //other
                }
                else
                {
                    PayerMGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    PayerMGridView.DataBind();
                }
            }
            

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void getLAData(string applicationno)
    {
        try
        {
            DataSet ds = objComm.GetLABypassFlagData(txtAppno.Text);
            if (ds != null)
            {
                //Current LA
                if (ds.Tables[0].Rows.Count > 0)
                {
                    LACurrentGrid.DataSource = ds.Tables[0];
                    LACurrentGrid.DataBind();
                    LACurrentGrid.Visible = true;
                    //other
                }
                else
                {
                    LACurrentGrid.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    LACurrentGrid.DataBind();
                }
                //Matching LA
                if (ds.Tables[1].Rows.Count > 0)
                {
                    LAMatchingGrid.DataSource = ds.Tables[1];
                    LAMatchingGrid.DataBind();
                    LAMatchingGrid.Visible = true;
                    //other
                }
                else
                {
                    LAMatchingGrid.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    LAMatchingGrid.DataBind();
                }
                //Current Proposer
                if (ds.Tables[2].Rows.Count > 0)
                {
                    ProposerCGridView.DataSource = ds.Tables[2];
                    ProposerCGridView.DataBind();
                    ProposerCGridView.Visible = true;
                    //other
                }
                else
                {
                    ProposerCGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    ProposerCGridView.DataBind();
                }

                //Matching Proposer
                if (ds.Tables[3].Rows.Count > 0)
                {
                    ProposerMGridView.DataSource = ds.Tables[3];
                    ProposerMGridView.DataBind();
                    ProposerMGridView.Visible = true;
                    //other
                }
                else
                {
                    ProposerMGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    ProposerMGridView.DataBind();
                }


                //Current Payer
                if (ds.Tables[4].Rows.Count > 0)
                {
                    PayerCGridView.DataSource = ds.Tables[4];
                    PayerCGridView.DataBind();
                    PayerCGridView.Visible = true;
                    //other
                }
                else
                {
                    PayerCGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    PayerCGridView.DataBind();
                }
                //MAtching Payer
                if (ds.Tables[5].Rows.Count > 0)
                {
                    PayerMGridView.DataSource = ds.Tables[5];
                    PayerMGridView.DataBind();
                    PayerMGridView.Visible = true;
                    //other
                }
                else
                {
                    PayerMGridView.DataSource = null;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    //lblMsg.Text = "No record found!";
                    PayerMGridView.DataBind();
                }
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
            lblMsg.Text = string.Empty;
            if (txtAppno.Text != string.Empty)
            {
                getLAData(txtAppno.Text.Trim());
                setbypassflagddl();
            }
            else
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please Enter Application Number";
            }
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please try again";

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string ddlLA = ddlLABypassFlag.SelectedItem.Text;
            string ddlProp = ddlProposer.SelectedItem.Text;
            string ddlPay = ddlPayer.SelectedItem.Text;

            objComm.UpdateBypassFlag(txtAppno.Text, (ddlLA == "Yes" ? "Y" : "N"), "LA");

            objComm.UpdateBypassFlag(txtAppno.Text, (ddlProp == "Yes" ? "Y" : "N"), "Proposer");

            objComm.UpdateBypassFlag(txtAppno.Text, (ddlPay == "Yes" ? "Y" : "N"), "Payer");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"alert('Data updateted successfully')");
            sb.Append(@"</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlProposer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlPayer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}