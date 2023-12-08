/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET (013)
METHODE/EVENT:PAGE LOAD
REMARK: CALL ALL THE SAVE BUTTON ON CLICK OF REFER OR REFER 
DateTime :24Feb18
**********************************************************************************************************************************
*/
//**********************************************************************
// Sr. No.              : 2
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 18/11/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UWSaralObjects;
public partial class UserControl_UserRefer : System.Web.UI.UserControl
{
    Commfun objComm = new Commfun();
    //2.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    CommanAssignmentDetails commanAssignment = new CommanAssignmentDetails();
    //2.1 End of Changes; Bhaumik Patel - [CR-5307]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                FillReferUsersList(false);
            }
            catch (Exception ex)
            {
                lblManualAllocationMsg.Text = "Try Again Later";
            }
        }
    }

    private void FillReferUsersList(bool blnFlag)
    {
        CommonObject objCommonObj = null;
        //2.1 Begin of Changes; Bhaumik Patel - [CR-5307]
        ProdDtls objprodchangevalue = new ProdDtls();
        string limit = string.Empty;
        string strUserId = string.Empty;
        string strApplicationNo = string.Empty, strChannel = string.Empty;
        if (Session["objprodchangevalue"] != null)
        {
            objprodchangevalue = (ProdDtls)Session["objprodchangevalue"];
            limit = objprodchangevalue._Sumassured;
        }
        //2.1 End of Changes; Bhaumik Patel - [CR-5307]
        if (Session["objCommonObj"] != null)
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
        }
        if (Request.QueryString["qsAppNo"] != null)
        {
            //lblApplicationNos.Text = strApplicationNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
            lblApplicationNos.Text = strApplicationNo = Request.QueryString["qsAppNo"];
        }
        if (Request.QueryString["qsChannelName"] != null)
        {
            lblChannel.Text = strChannel = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsChannelName"]);
        }

        BussLayer objBussLayer = new BussLayer();
        DataSet _ds = new DataSet();
        if (blnFlag)
        {
            strUserId = strUserId.Substring(1);
            objBussLayer.FetchReferUser(strApplicationNo, strChannel, ddlAssignBy.SelectedValue, strUserId, ref _ds);
        }
        else
        {
            objBussLayer.FetchReferUser(strApplicationNo, strChannel, ref _ds);
        }
        //2.1 Begin of Changes; Bhaumik Patel - [CR-5307]
        DataSet ds2 = new DataSet();
        string parameter = string.Empty;

        if (ddlAssignBy.SelectedValue.ToString() == "UWSIGNOFF")
        {
            parameter = "Refer/Sign off/Re-Assign cases";
        }
        else if (ddlAssignBy.SelectedValue.ToString() == "UWREFER")
        {
            parameter = "Refer for Senior Underwriter Opinion";
        }
        else
        {
            parameter = "PleaseSelect";
        }

        commanAssignment.UnderWriting_AssignmentDetails_CheckAllocationparameter("GetUserAccess_AllocationParameter", limit, parameter, ref ds2);
        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        {
            ddlUsers.Items.Clear();
            ddlUsers.Items.Insert(0, new ListItem("Please select ", ""));
            ddlUsers.SelectedValue = "";

            foreach (DataRow row in ds2.Tables[0].Rows)
            {
                    if (row["UserID"] != null)
                        ddlUsers.DataValueField = row["UserID"].ToString();
                    else
                        ddlUsers.DataValueField = "";

                    if (row["UserName"] != null)
                        ddlUsers.DataTextField = row["UserName"].ToString();
                    else
                        ddlUsers.DataTextField = "";

                    ddlUsers.Items.Add(new ListItem(ddlUsers.DataTextField, ddlUsers.DataValueField));
            }
        }
        else
        {
            ddlUsers.Items.Clear();
            ddlUsers.Items.Insert(0, new ListItem("Please select ", ""));
            ddlUsers.SelectedValue = "";
        }

        //if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        //{
        //    ddlUsers.DataSource = _ds;
        //    ddlUsers.DataTextField = "FULL_NAME";
        //    ddlUsers.DataValueField = "USER_ID";
        //}
        //else
        //{
        //    ddlUsers.DataSource = null;
        //}
        //ddlUsers.DataBind();

        //2.1 End of Changes; Bhaumik Patel - [CR-5307]
    }
    protected void btnRefer_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["objCommonObj"] != null)
            {

                if (ddlAssignBy.SelectedValue == "0")
                {
                    lblManualAllocationMsg.Text = "Select Allocate By";
                    return;
                }
                int intRef = -1;
                int intRefServ = -1;
                if (!string.IsNullOrEmpty(ddlAssignBy.SelectedValue))
                {
                    CommonObject objCommonObj = (CommonObject)Session["objCommonObj"];
                    UserInfo objUserInfo = objCommonObj._Bpmuserdetails;
                    /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                    //Commfun objComm = new Commfun();                    
                    objComm.ManageApplicationLifeCycle(lblApplicationNos.Text, objUserInfo._UserID, "UW_DECISION_FERFER", false, ref intRef);
                    /*END HERE*/
                    BussLayer objBussLayer = new BussLayer();
                    //ddlUsers.SelectedValue
                    objBussLayer.ManageReferUser(lblApplicationNos.Text, lblChannel.Text, ddlUsers.SelectedValue, objUserInfo._UserID, ddlAssignBy.SelectedValue, ref intRefServ);
                    /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                    objComm.ManageApplicationLifeCycle(lblApplicationNos.Text, objUserInfo._UserID, "UW_DECISION_FERFER", true, ref intRef);
                    /*added by shri on 05 dec 17 to add reference value*/
                    if (intRefServ > 0)
                    {
                        Commfun objCom = new Commfun();
                        objCom.ManageApplicationStatus(lblApplicationNos.Text, ddlAssignBy.SelectedValue, string.Empty, objUserInfo._UserID, ddlUsers.SelectedValue, "SARALUW", ref intRef);
                        //objComm.MaintainApplicationLog(lblApplicationNos.Text, ddlAssignBy.SelectedValue, string.Empty, objUserInfo._UserID, ddlUsers.SelectedValue, ref intRef);
                    }
                    /*END HERE*/
                    /*ID:1 START*/
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "give feedback and close window", "window.close();alert('application is assigned to user successfully');", true);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>__doPostBack('refer','refer');</script>", false);
                    /*ID:1 END*/
                }
                else
                {
                    lblManualAllocationMsg.Text = "Select Sending Type";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "give feedback and close window", "$('#divReferOption').modal('show');", true);
                }
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void btnReSend_Click(object sender, EventArgs e)
    {
        if (Session["objCommonObj"] != null)
        {
            int intRef = -1;
            int intRefServ = -1;
            if (!string.IsNullOrEmpty(ddlAssignBy.SelectedValue))
            {
                CommonObject objCommonObj = (CommonObject)Session["objCommonObj"];
                UserInfo objUserInfo = objCommonObj._Bpmuserdetails;
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                Commfun objComm = new Commfun();
                objComm.ManageApplicationLifeCycle(lblApplicationNos.Text, objUserInfo._UserID, "UW_DECISION_FERFER", false, ref intRef);
                /*END HERE*/
                BussLayer objBussLayer = new BussLayer();
                //ddlUsers.SelectedValue
                objBussLayer.ManageReferUser(lblApplicationNos.Text, lblChannel.Text, ddlUsers.SelectedValue, objUserInfo._UserID, "RESEND", ref intRefServ);
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                objComm.ManageApplicationLifeCycle(lblApplicationNos.Text, objUserInfo._UserID, "UW_DECISION_FERFER", true, ref intRef);
                /*END HERE*/
                if (intRefServ > 1)
                {
                    Commfun objCom = new Commfun();
                    objCom.ManageApplicationStatus(lblApplicationNos.Text, ddlAssignBy.SelectedValue, string.Empty, objUserInfo._UserID, ddlUsers.SelectedValue, "SARALUW", ref intRef);
                    /*ID:1 START*/
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "give feedback and close window", "window.close();alert('application is assigned to user successfully');", true);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>__doPostBack('refer','refer');</script>", false);
                    /*ID:1 END*/
                }
                else
                {
                    lblManualAllocationMsg.Text = "Cannot Assign Since Was Assigned System";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "give feedback and close window", "$('#divReferOption').modal('show');", true);
                }

            }
            else
            {
                lblManualAllocationMsg.Text = "Select Sending Type";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "give feedback and close window", "$('#divReferOption').modal('show');", true);
            }
        }
    }
    protected void ddlAssignBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillReferUsersList(true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "give feedback and close window", "$('#divReferOption').modal('show');", true);
    }


}