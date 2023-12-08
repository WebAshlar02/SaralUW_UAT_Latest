using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Platform.Utilities.LoggerFramework;
using System.Configuration;
using System.IO;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :AMIT CHAUDHARY
METHODE/EVENT:  lnkCommondashbord_Click
REMARK:         COMMENTED DASBORD COUNT METHODE AS THIS WAS NOT NEEDED AT THE TIME OF FETCHING APPLICATION DETAILS .                       
DateTime :03NOV2017
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :shrijeet shanil
METHODE/EVENT:  lnkCommondashbord_Click
REMARK:         added by shri to show total cases in future generali
DateTime :22Jan17
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :shrijeet shanil
METHODE/EVENT:  
REMARK:         add my wip bucket
DateTime :25feb19
*********************************************************************************************************************************
* **********************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:  gvAppdasbord_ItemCommand
REMARK: create 1 method to get combi flag ,according to this flag we will call uwdescsion_Combi page for combi cases and normal page for existing cases.
DateTime :20 SEP 2020
* **********************************************************************************************************************************
COMMENT ID: 5
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT:  
REMARK: Show and Hide Policy Risk Flag Label .
DateTime :8 Feb 2022

***********************************************************************************************************************************
COMMENT ID: 6
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT:  
REMARK: Mer Videos CR- 30499.
DateTime :8 April 2022
***********************************************************************************************************************************
COMMENT ID: 7
COMMENTOR NAME :Sushant Devkate
METHODE/EVENT:  
REMARK: IIB UI Cr-30363
DateTime :1 Jun 2022
*/
//**********************************************************************
// Sr. No.              : 8
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 03/01/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************
//**********************************************************************
// Sr. No.              : 9
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5855 
// Date Of Creation     : 19/06/2023
// Description          : Grid based Loading access for Counter offer in Saral.
//**********************************************************************
//***********************************************************************************************************
// Sr. No.            : 10
// Program Author     : Bhaumik Patel [WebAshlar02]
// Module             : Image QC
// BRD/CR/WINO        : CR-8222
// Date Of Change     : 03/11/2023
// Change Description : 1.MDRT Agent flagging in NB workflow
//************************************************************************************************************
public partial class Default3 : System.Web.UI.Page
{
    #region Variable Declaration Begins.
    DataSet _ds = new DataSet();
    BussLayer ObjBuss = new BussLayer();
    string strUserId = string.Empty;
    string strUWmode = string.Empty;
    string strPolicyRiskFlag = string.Empty;
    string strUserGroup = string.Empty;
    string strApplicationno = string.Empty;
    string strOptinselected = string.Empty;
    string strAppstatusKey = string.Empty;
    public string strPolicyEnquiery = string.Empty;
    int Result = 0;
    DataSet _dsDashcount = null;
    Commfun objComm = new Commfun();
    CommonObject objCommonObj = new CommonObject();
   //8.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    string struserName = string.Empty;
    string strBranchCode = string.Empty;
    string strBranchName = string.Empty;
    CommanAssignmentDetails CommAssD = new CommanAssignmentDetails();
    //8.1 End of Changes; Bhaumik Patel - [CR-5307]
    Boolean IsPageRefresh;
    #endregion Variable declaration End.

    #region Event Declaration Begins.

    protected void Page_PreInit(object sender, EventArgs e)
    {
        objCommonObj = (CommonObject)Session["objCommonObj"];
        if (Session["objLoginObj"] == null || !objCommonObj._Bpmuserdetails._UserID.Equals(Request.QueryString["Userid"].ToUpper()))// || Request.QueryString["Userid"] == null)
        {
            //Response.Redirect("~/9011042143.aspx");
        }
     
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        /*
        string name = "Questions_Demo.xlsx"; // getting file name from session and put it in local variable name.

        Response.ContentType = "application/excel";  // set the application type here we use msword mime type you can change the MIME to other application.

        Response.AppendHeader("Content-Disposition", "attachment; filename=" + name ); // set the file name

        Response.TransmitFile(Server.MapPath(name)); // Map the path from virtual directory.

        Response.End(); // end of response
        */
        Logger.Info("New Journey Begins At ****************" + DateTime.Now + "***************" + System.Environment.NewLine);
        Logger.Info(":PageName :Default.aspx // MethodeName :Page_Load" + System.Environment.NewLine);
        //Added by Suraj on 01 - Nov - 2018 - wanted to restrict copy and paste of URL to open the page
        string strPreviousPage = "";
        //if (Request.UrlReferrer != null)
        //{
        //    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
        //}
        //if (strPreviousPage == "")
        //{
        //    Response.Redirect("~/9011042143.aspx");
        //}
        if (Session["PreviousPage"] != null)
        {
            strPreviousPage = Session["PreviousPage"].ToString();
        }

        if (Session["objLoginObj"] != null)
        {
            //8.1 Begin of Changes; Bhaumik Patel - [CR-5307]
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            struserName = objCommonObj._Bpmuserdetails._UserName;
            strBranchCode = objCommonObj._Bpmuserdetails._userBranch;
            //8.1 End of Changes; Bhaumik Patel - [CR-5307]

        }
        else
        {
            Response.Redirect("~/9011042143.aspx");
        }
        //if (strPreviousPage == "")
        //{
        //    Response.Redirect("~/9011042143.aspx");
        //}
        //8.1 Begin of Changes; Bhaumik Patel - [CR-5307]
        DataSet ds1 = new DataSet();
        CommAssD.UnderWriting_AssignmentUserAccess_GET_byUSERCOUNT("CheckUserAccessCount", ref ds1);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            DataSet ds2 = new DataSet();
            CommAssD.UnderWriting_AssignmentUserAccess_GET_byUserID("CheckUserAccess", strUserId, ref ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                divrights.Visible = true;
                lblrights.Text = (ds1.Tables[0].Rows.Count).ToString();
            }
            else
            {
                divrights.Visible = false;
            }
        }
        else
        {
            divrights.Visible = false;
        }
        //8.1 End of Changes; Bhaumik Patel - [CR-5307]

        //9.1 Begin of Changes; Bhaumik Patel - [CR-5855]

        string CheckMortalityUser = ConfigurationManager.AppSettings["MortalityAccessUser"].ToString();
        if (CheckMortalityUser.Split(',').ToArray().Contains(strUserId))
        {
            MortalityID.Visible = true;
        }
        else
        {
            MortalityID.Visible = false;
        }
        //9.1 End of Changes; Bhaumik Patel - [CR-5855]

        if (!IsPostBack)
        {
            try
            {
                //if (Request.UrlReferrer != null)
                //{
                //    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                //    Session["PreviousPage"] = strPreviousPage;
                //}
                //if (strPreviousPage == "")
                //{
                //    Response.Redirect("https://fglpg001.futuregenerali.in/FGUniconnect_UAT/FGUniconnectSSO.aspx?aspxerrorpath=%2fFGUniconnect_UAT%2fFGUniconnectSSO.aspx");
                //}
                /*added by shri on 07 nov 17 to add application log */
                //objComm = new Commfun();
                //objComm.ManageApplication()
                /*end here*/
                int intRef = -1;
                strPolicyEnquiery = ConfigurationManager.AppSettings["PolicyEnquiry"];
              
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                objCommonObj = (CommonObject)Session["objCommonObj"];
                objCommonObj._ChannelType = "OFFLINE";
                Session["objCommonObj"] = objCommonObj;
                strUWmode = "OFFLINE";
                strOptinselected = "FRESH";
                //8.1 Begin of Changes; Bhaumik Patel - [CR-5307]
                int result = 0;
                //SessionIDManager manager = new SessionIDManager();

                //string newID = manager.CreateSessionID(Context);
               

                var uniqueGuID = System.Guid.NewGuid().ToString("N").Substring(0, 8);
                Session["NewSessionId"] = uniqueGuID;
                //bool redirected = false;
                //bool isAdded = false;
                //manager.SaveSessionID(Context, newID, out redirected, out isAdded);

                CommAssD.Underwriting_AssignmentDetails_LOGINLOGOUT_Log(strUserId, struserName, strUserGroup, "Login", "Insert_Login_Logout_Logs","0", ref result);
                CommAssD.Underwriting_LOGIN_Log("INSERT_LOGIN_LOGS", uniqueGuID.ToString(), strUserGroup, strUserId, struserName, strBranchCode, strBranchName, "Login from UserId", "SaralUW", 1,1, ref result);
                //8.1 End of Changes; Bhaumik Patel - [CR-5307]
                if (!string.IsNullOrEmpty(objCommonObj._Bpmuserdetails._UserGroup) && objCommonObj._Bpmuserdetails._UserGroup == "CMO")
                {
                    divCMO.Visible = true;
                    //9.1 Added by Royson for Displaying only CMO CR-10
                    divWIP.Visible = false;
                    divTele.Visible = false;
                    divCounter.Visible = false;
                    divPivc.Visible = false;
                    divPivcRea.Visible = false;
                    divPivcPen.Visible = false;
                    divRev.Visible = false;
                    divClsRev.Visible = false;
                    divSignOff.Visible = false;
                    divResolve.Visible = false;
                    divReady.Visible = false;
                    divPending.Visible = false;
                    divFresh.Visible = false;
                    hrHidden.Visible = false;
                    //9.1End by Royson for Displaying only CMO CR-10
                }
                else
                {
                    divCMO.Visible = false;
                }
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                spanusername.InnerText = objCommonObj._Bpmuserdetails._UserName;
                objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "DAHS_LOAD", false, ref intRef);
                //GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
                DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
                //DashbordBucketsCountAll_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
                objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "DAHS_LOAD", true, ref intRef);
               
                //6-1 Start of changes: Sagar Thorave[MFL00886] CR- 30499
               string strMerVideos = ConfigurationManager.AppSettings["MerVideos"];
                if (strMerVideos.Split(',').ToArray().Contains(strUserId))
                {
                    MerVideos.Style.Add("display", "lock");
                }
                //6-1 End of changes: Sagar Thorave[MFL00886] CR- 30499

                //5 - 1 Start of changes: Sagar Thorave[MFL00886] CR- 30573
                strPolicyRiskFlag = ConfigurationManager.AppSettings["BmpsPolicyFlagId"];
                if (strPolicyRiskFlag.Split(',').ToArray().Contains(strUserId))
                {
                    PolicyRiskId.Style.Add("display", "lock");
                }
                //5-1 End of changes: Sagar Thorave[MFL00886] CR- 30573 
                //7-1 Start of changes: Sushant Devkate[MFL00905] CR - 30363
                string strAddPincode = ConfigurationManager.AppSettings["OpenAddPincode"].ToString();
                if (strAddPincode.Split(',').ToArray().Contains(strUserId))
                {
                    liAddNegPinCode.Style.Add("display", "block");
                }
                //7-1End of changes: Sushant Devkate[MFL00905] CR- 30363
            }
            catch (Exception EX)
            {

            }
            //DashbordBucketsCountDynamic_Get(strUserId, strUserGroup, strUWmode);
        }
        else
        {

            //    if (ViewState["postids"].ToString() != Session["postid"].ToString())
            //    {
            //        IsPageRefresh = true;
            //    }
            //    Session["postid"] = System.Guid.NewGuid().ToString();
            //    ViewState["postids"] = Session["postid"].ToString();
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    protected void lnkOnline_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("DashboardOnline.aspx", false);
            //objCommonObj = (CommonObject)Session["objCommonObj"];
            //objCommonObj._ChannelType = lnkOnline.Text;
            //Session["objCommonObj"] = objCommonObj;
            //strOptinselected = "FRESH";
            //strUserId = objCommonObj._Bpmuserdetails._UserID;
            //strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            //strUWmode = "ONLINE";
            ////GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCountAll_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    protected void lnkoffline_Click(object sender, EventArgs e)
    {
        try
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];
            objCommonObj._ChannelType = "offline";
            Session["objCommonObj"] = objCommonObj;
            strOptinselected = "FRESH";
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            strUWmode = "OFFLINE";
            GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
            DashbordBucketsCountAll_Get(strUserId, strUserGroup, strOptinselected, strUWmode);

        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    protected void gvAppdasbord_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkApplicationNo = (LinkButton)row.FindControl("lnkAppno");
                HiddenField hdnProductName = (HiddenField)row.FindControl("hdnProductName");
                Label lblChannelType = (Label)row.FindControl("lblUtmchannel");
                Label lblPolicyNo = (Label)row.FindControl("lblpolno");
                string strApplicationno = lnkApplicationNo.Text;
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                objCommonObj._ApplicationNo = strApplicationno;
                objCommonObj._ChannelType = "OFFLINE";
                //Added by Suraj for Close file review
                //DataSet ds = new DataSet();
                //objComm.CheckCloseFileReview(strUserId, ref ds);
                string strclicklink = string.Empty;
                //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //{
                //    strclicklink = "Close_File_Review";
                //    //hdncolsefilerw.Value = "";
                //}
                //else
                //{
                //    strclicklink = "";
                //}
                //End
                objCommonObj._PolicyNo = lblPolicyNo.Text;
                objCommonObj._ProductName = hdnProductName.Value;
                Session["objCommonObj"] = objCommonObj;
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                int intRef = -1;
           

                objComm.ManageApplicationLifeCycle(lnkApplicationNo.Text, strUserId, "APP_SELECT", false, ref intRef);
                /*END HERE*/
                #region Control Logic Begin.
                strAppstatusKey = (strUserGroup == "UW") ? "UL" : "DL";
                objComm.OnlineApplicationLockChecker(strApplicationno, strUserId, strUserGroup, ref _ds);
                if (_ds != null && _ds.Tables[0].Rows.Count > 0)
                {
                    string strUserLimit =Convert.ToString(_ds.Tables[0].Rows[0]["LIMIT"]);
                    objComm.OnlineApplicationchangeStatus(strApplicationno, strUserId, strAppstatusKey, "", ref Result);
                    if (Result != 0 || Result != -1)
                    {
                        /*ID : 4 START*/
                        DataSet _dscombiflag = new DataSet();
                        bool Iscombi = false;//ConfigurationManager.AppSettings["IsCombi"];
                        objComm.GetCombiFlag(ref _dscombiflag, strApplicationno);
                        if (_dscombiflag != null && _dscombiflag.Tables.Count > 0 && _dscombiflag.Tables[0].Rows.Count > 0)
                        {
                            Iscombi = true;
                            Session["IsCombi"] = Iscombi;
                        }
                        if (Iscombi)
                        {
                            Response.Redirect(string.Format("Appcode/Uwdecision_Combi.aspx?qsUserGroup={0}&qsAppNo={1}&qsChannelName={2}&qsPolicyNo={3}&qsUserLimit={4}&qsClickBucketLink={5}",
                            UWSaralDecision.CommFun.Base64EncodingMethod(objCommonObj._Bpmuserdetails._UserGroup),
                             //UWSaralDecision.CommFun.Base64EncodingMethod(strApplicationno),
                             strApplicationno,
                            UWSaralDecision.CommFun.Base64EncodingMethod(lblChannelType.Text),
                            UWSaralDecision.CommFun.Base64EncodingMethod(lblPolicyNo.Text),
                            UWSaralDecision.CommFun.Base64EncodingMethod(strUserLimit),
                             UWSaralDecision.CommFun.Base64EncodingMethod(strclicklink) //Added by Suraj on 24 APRIL 2018 for Close file review
                            ), false);
                        }
                        else
                        {
                            /*ID : 4 END*/
                            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('www.google.com','_blank');", true);
                            Response.Redirect(string.Format("Appcode/Uwdecision.aspx?qsUserGroup={0}&qsAppNo={1}&qsChannelName={2}&qsPolicyNo={3}&qsUserLimit={4}&qsClickBucketLink={5}",
                            //Response.Redirect(string.Format("Appcode/Uwdecision_Combi.aspx?qsUserGroup={0}&qsAppNo={1}&qsChannelName={2}&qsPolicyNo={3}&qsUserLimit={4}&qsClickBucketLink={5}",
                            UWSaralDecision.CommFun.Base64EncodingMethod(objCommonObj._Bpmuserdetails._UserGroup),
                             //UWSaralDecision.CommFun.Base64EncodingMethod(strApplicationno),
                             strApplicationno,
                            UWSaralDecision.CommFun.Base64EncodingMethod(lblChannelType.Text),
                            UWSaralDecision.CommFun.Base64EncodingMethod(lblPolicyNo.Text),
                            UWSaralDecision.CommFun.Base64EncodingMethod(strUserLimit),
                             UWSaralDecision.CommFun.Base64EncodingMethod(strclicklink) //Added by Suraj on 24 APRIL 2018 for Close file review
                            ), false);
                        }
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#myModal1').modal();window.close();alert('test message');", true);
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "window.close();alert('The case is already locked by other user');", true);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
                }
                #endregion Control Logic End.
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                objComm.ManageApplicationLifeCycle(lnkApplicationNo.Text, strUserId, "APP_SELECT", true, ref intRef);
                /*END HERE*/
            }
            else if (e.CommandName == "PendingDocs")
            {
                DataSet dsPendingDocs = new DataSet();
                RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
                LinkButton hdnApplicationNo = (LinkButton)row.FindControl("lnkAppno");
                Literal lblapp = (Literal)row.FindControl("lblapp");
                UserControl_PopupDoc WebUserControl1 = (UserControl_PopupDoc)FindControl("PendingDocs");
                if (!string.IsNullOrEmpty(hdnApplicationNo.Text))
                {
                    int response = 0;
                    WebUserControl1.FillPendingDoc(hdnApplicationNo.Text, "", ref response);
                    if (response == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#divPendingDocPopup').modal('show');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#myModalNoDocPending').modal();", true);
                    }
                }

            }

        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }
    protected void lnkCommondashbord_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkOptinselected = (LinkButton)sender;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strOptinselected = lnkOptinselected.Text;
            string strUpperFilter = lnkOptinselected.Text.ToUpper();
            hdncolsefilerw.Value = string.Empty;
            switch (strUpperFilter)
            {
                case "FRESH CASES":
                    {
                        strOptinselected = "FRESH";
                        break;
                    }

                case "UWREFER CASES":
                    {
                        strOptinselected = "REFER";
                        break;
                    }

                case "PENDING":
                    {
                        strOptinselected = "PENDING";
                        break;
                    }

                case "DOCUMENTS VERIFIED":
                    {
                        strOptinselected = "DOCS";
                        break;
                    }

                case "PAYMENT REALIZED":
                    {
                        strOptinselected = "PAYMENT";
                        break;
                    }

                case "READY TO ISSUE":
                    {
                        strOptinselected = "RDYTOISU";
                        break;
                    }

                case "RESOLVE":
                    {
                        strOptinselected = "RESOLVE";
                        break;
                    }

                case "SIGN OFF":
                    {
                        strOptinselected = "SIGNOFF";
                        break;
                    }
                case "PIVC PENDING":
                    {
                        strOptinselected = "PIVCPNDG";
                        break;
                    }
                case "PIVC & REALIZATION COMPLETE":
                    {
                        strOptinselected = "PIVC_COMPLETE_REALIZATION_COMPLETE";
                        break;
                    }
                case "PIVC COMPLETE REALIZATION PENDING":
                    {
                        strOptinselected = "PIVC_COMPLETE_REALIZATION_PENDING";
                        break;
                    }
                case "DOCQC":
                    {
                        strOptinselected = "DOCQC";
                        break;
                    }
                /*ID:2 START*/
                case "FG CASES":
                    {
                        strOptinselected = "FG_CASES";
                        break;
                    }
                case "COUNTER OFFER":
                    {
                        strOptinselected = "COUNTER_OFFER";
                        break;
                    }

                case "TELE CONVERSATION":
                    {
                        strOptinselected = "TELE_CONVERSATION";
                        break;
                    }
                /*ID:2 END*/
                //Added by Suraj on 10Aprl2018 for close file review
                case "CLOSE FILE REVIEW":
                    {
                        strOptinselected = "Close_File_Review";
                        //Session["CloseFile"] = "Close_File_Review";
                        //hdncolsefilerw.Value = "Close_File_Review";
                        break;
                    }
                //End
                //9.1 Start By Royson Pinto on 16th Feb CMO Data Count for CR 4783 CR-10
                case "CMO":
                    {
                        dynamic temp = ObjBuss.CMOCount();
                        lblcmo.Text = temp.Tables[0].Rows[0][0].ToString();//Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                        break;
                    }
                //9.1 End By Royson Pinto on 16th Feb CMO Data Count for CR 4783 CR-10

                case "MY WIP":
                    {
                        strOptinselected = "MY_WIP";
                        break;
                    }
                //########################## Added by Shyam Starts ###########################################
                case "REVIVAL CASES":
                    {
                        strOptinselected = "Revival_Cases";
                        break;
                    }
                //########################## Added by Shyam End ###########################################
                default:
                    break;
            }

            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            strUWmode = objCommonObj._ChannelType;
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            int intRef = -1;
            objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "BKT_" + strUpperFilter, false, ref intRef);
            /*END HERE*/
            GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            upDashboardDetails.Update();
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            objComm.ManageApplicationLifeCycle(string.Empty, strUserId, "BKT_" + strUpperFilter, true, ref intRef);
            /*END HERE*/
            //ID:1 BEGINS
            //DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCountAll_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
            //ID:1 END            
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "$('#ldrModal').fadeOut();", true);

        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);

    }
    #endregion Event Declaration End.

    #region Methode Declaration begin.
    public void DashbordBucketsCountDynamic_Get(string strUserid, string strUserGroup, string strUWmode)
    {
        ObjBuss.OnlineApplicationdashbordBucketsCountDynamic_Get(ref _dsDashcount, strUWmode, strUserId, strUserGroup);
        if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
        {
            divDashbord.InnerHtml = _dsDashcount.Tables[0].Rows[0][0].ToString();

        }
    }
    public void DashbordBucketsCount_Get(string strUserid, string strUserGroup, string strLinkOption, string strUWmode)
    {
        ObjBuss.OnlineApplicationdashbordBucketsCount_Get(strUWmode, strUserId, strLinkOption, strUserGroup, ref _dsDashcount);
        if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
        {
            if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
            {
                for (int i = 0; i < _dsDashcount.Tables[0].Rows.Count; i++)
                {
                    switch (Convert.ToString(_dsDashcount.Tables[0].Rows[i]["MODE"]))
                    {
                        case "FRESH":
                            {
                                lblFreshcount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "PENDING":
                            {
                                lblPendingcount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "UWREFERAL":
                            {
                                //lblUWRefercount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "PAYMENT_REALIZED":
                            {
                                lblDocRealizedCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "READYTOISSUE":
                            {
                                lblReadytoissuecount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "RESOLVE":
                            {
                                lblResolveCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "SIGNOF":
                            {
                                lblSignoffCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "DOCQC":
                            {
                                //lblDocVerifiedcount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                lblDocQcCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_PENDING":
                            {
                                lblPivcPendingCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_COMPLETE_REALIZATION_COMPLETE":
                            {
                                lblPivcRealizationCompleteCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_COMPLETE_REALIZATION_PENDING":
                            {
                                lblPivcCompleteRelizationPendingCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        /*ID:2 START*/
                        //case "FG_CASES":
                        //    {
                        //        lblFGCasesCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                        //        break;
                        //    }
                        case "COUNTER_OFFER":
                            {
                                lblCounterOfferCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "TELE_CONVERSATION":
                            {
                                lblTeleConversationCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        /*ID:2 END*/
                        case "CLOSEFILE":
                            {
                                lblclsrew.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "CMO":
                            {
                                dynamic temp = ObjBuss.CMOCount();
                                lblcmo.Text = temp.Tables[0].Rows[0][0].ToString();//Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                                //lblcmo.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                //break;
                            }
                        //case "CMO":
                        //    {
                        //        lblcmo.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                        //        break;
                        //    }
                        /*ID:3 START*/
                        case "MYWIP":
                            {
                                lblMyWIPCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        /*ID:3 START*/
                        //########################## Added by Shyam Starts ###########################################
                        //Dis patch of code display the block with count of cases in Revival bucket
                        case "Revival_Cases":
                            {
                                lblRevival.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        //########################## Added by Shyam End ###########################################
                        default:
                            break;
                    }
                }
            }
        }
    }

    public void GetDashbordDetails(string strUserid, string strUserGroup, string strlnkOption, string strUWmode)
    {
        if (strlnkOption == "Revival_Cases")
        {
            //########################## Added by Shyam Starts ###########################################
            gvAppdasbord.Visible = false;
            gvAppdasbord_CMO.Visible = false;
            gvAppdasbord_Revival.Visible = true;
            ObjBuss.OnlineApplicationDashborddetails_GET(strUWmode, strUserid, strUserGroup, strlnkOption, ref _ds);
            //gvAppdasbord_Revival

            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                lbldashborderror.Text = "";
                gvAppdasbord_Revival.DataSource = _ds;
                gvAppdasbord_Revival.DataBind();
            }
            else
            {
                lbldashborderror.Text = "No Record found";
                gvAppdasbord_Revival.DataSource = null;
                gvAppdasbord_Revival.DataBind();
            }
            //########################## Added by Shyam End ###########################################
        }
        else
        {
            gvAppdasbord_Revival.Visible = false;
            ObjBuss.OnlineApplicationDashborddetails_GET(strUWmode, strUserid, strUserGroup, strlnkOption, ref _ds);
            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                lbldashborderror.Text = "";
                if (strOptinselected == "CMO")
                {
                    gvAppdasbord.Visible = false;
                    gvAppdasbord_CMO.Visible = true;
                    gvAppdasbord_CMO.DataSource = _ds;
                    gvAppdasbord_CMO.DataBind();
                }
                else
                {
                    gvAppdasbord_CMO.Visible = false;
                    gvAppdasbord.Visible = true;
                    gvAppdasbord.DataSource = _ds;
                    gvAppdasbord.DataBind();

                }

            }
            else
            {
                lbldashborderror.Text = "No Record found";
                if (strOptinselected == "CMO")
                {
                    gvAppdasbord.Visible = false;
                    gvAppdasbord_CMO.Visible = true;
                    gvAppdasbord_CMO.DataSource = null;
                    gvAppdasbord_CMO.DataBind();
                }
                else
                {
                    gvAppdasbord_CMO.Visible = false;
                    gvAppdasbord.Visible = true;
                    gvAppdasbord.DataSource = null;
                    gvAppdasbord.DataBind();
                }
            }
        }

    }

    #endregion Methode Declaration End.
    protected void lnkSearchAppilcation_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["objCommonObj"] != null)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                objCommonObj._ChannelType = "OFFLINE";
                Session["objCommonObj"] = objCommonObj;
                strUWmode = "OFFLINE";
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                int intRef = -1;
                objComm.ManageApplicationLifeCycle(txtSearch.Text, strUserId, "DASH_APP_SEARCH", false, ref intRef);
                /*END HERE*/
                //23NOV2017
                ObjBuss.GetApplicationDetails(txtSearch.Text, strUserId, "OFFLINE", ref _ds);

                //23NOV2017 end
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    //Revival Chnages start
                    if (_ds.Tables[0].Rows[0]["CurruntStage"].ToString().Trim() == "Revival_Cases")
                    {

                        gvAppdasbord_Revival.DataSource = _ds;
                        gvAppdasbord_Revival.DataBind();
                        gvAppdasbord_Revival.Focus();
                        // upDashboardDetails.Update();
                    }
                    else
                    {
                        gvAppdasbord.DataSource = _ds;
                        gvAppdasbord.DataBind();
                        //gvAppdasbord.Focus();
                        lblfocus.Focus();
                        // upDashboardDetails.Update();
                    }
                    upDashboardDetails.Update();
                    //Revival Chnages start
                }
                else
                {
                    lblErrorinfo.Text = "No Record Found";
                }
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                objComm.ManageApplicationLifeCycle(txtSearch.Text, strUserId, "DASH_APP_SEARCH", true, ref intRef);
                ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
                /*END HERE*/
            }
        }
        catch (Exception ex)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }
    protected void tmrDashboard_Tick(object sender, EventArgs e)
    {
        try
        {
            if (Session["objCommonObj"] != null)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                objCommonObj._ChannelType = "OFFLINE";
                Session["objCommonObj"] = objCommonObj;
                strUWmode = "OFFLINE";
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                spanusername.InnerText = objCommonObj._Bpmuserdetails._UserName;
                DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
                DashbordBucketsCountAll_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
                upDashboardCount.Update();
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void DashbordBucketsCountAll_Get(string strUserid, string strUserGroup, string strLinkOption, string strUWmode)
    {
        ObjBuss.OnlineApplicationdashbordBucketsCountAll_Get(strUWmode, strUserId, strLinkOption, strUserGroup, ref _dsDashcount);
        if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
        {
            if (_dsDashcount != null && _dsDashcount.Tables.Count > 0)
            {
                for (int i = 0; i < _dsDashcount.Tables[0].Rows.Count; i++)
                {
                    switch (Convert.ToString(_dsDashcount.Tables[0].Rows[i]["MODE"]))
                    {
                        case "FRESH":
                            {
                                lblFreshcount.Text = lblFreshcount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "PENDING":
                            {
                                lblPendingcount.Text = lblPendingcount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "UWREFERAL":
                            {
                                //lblUWRefercount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "PAYMENT_REALIZED":
                            {
                                lblDocRealizedCount.Text = lblDocRealizedCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "READYTOISSUE":
                            {
                                lblReadytoissuecount.Text = lblReadytoissuecount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "RESOLVE":
                            {
                                lblResolveCount.Text = lblResolveCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "SIGNOF":
                            {
                                lblSignoffCount.Text = lblSignoffCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }

                        case "DOCQC":
                            {
                                //lblDocVerifiedcount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                lblDocQcCount.Text = lblDocQcCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_PENDING":
                            {
                                lblPivcPendingCount.Text = lblPivcPendingCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_COMPLETE_REALIZATION_COMPLETE":
                            {
                                lblPivcRealizationCompleteCount.Text = lblPivcRealizationCompleteCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "PIVC_COMPLETE_REALIZATION_PENDING":
                            {
                                lblPivcCompleteRelizationPendingCount.Text = lblPivcCompleteRelizationPendingCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        /*ID:2 START*/
                        //case "FG_CASES":
                        //    {
                        //        lblFGCasesCount.Text = Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                        //        break;
                        //    }
                        case "COUNTER_OFFER":
                            {
                                lblCounterOfferCount.Text = lblCounterOfferCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        case "TELE_CONVERSATION":
                            {
                                lblTeleConversationCount.Text = lblTeleConversationCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        /*ID:2 END*/
                        case "CLOSEFILE":
                            {
                                lblclsrew.Text = lblclsrew.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        /*ID:3 START*/
                        case "MYWIP":
                            {
                                lblMyWIPCount.Text = lblMyWIPCount.Text + "/" + Convert.ToString(_dsDashcount.Tables[0].Rows[i]["COUNT"]);
                                break;
                            }
                        /*ID:3 END*/
                        default:
                            break;
                    }
                }
            }
        }
    }
    protected void gvAppdasbord_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsGreen = (Label)e.Item.FindControl("lblIsChannelGreen");
                if (lblIsGreen.Text.Equals("YES"))
                {
                    Label lblPolicyNo = (Label)e.Item.FindControl("lblpolno");
                    lblPolicyNo.BackColor = System.Drawing.Color.LimeGreen;
                }
                //Added by Suraj on 28-12-2018 for MDRT Agent color change
                Label lblmdrtagnt = (Label)e.Item.FindControl("lblmdragent");
                if (!string.IsNullOrEmpty(lblmdrtagnt.Text))
                {
                    Label lblPolicyNo = (Label)e.Item.FindControl("lblpolno");
                    lblPolicyNo.BackColor = System.Drawing.Color.DeepSkyBlue;
                    LinkButton lnkappno = (LinkButton)e.Item.FindControl("lnkAppno");
                    lnkappno.BackColor = System.Drawing.Color.DeepSkyBlue;
                    lnkappno.ForeColor = System.Drawing.Color.Black;
                }

                //10.1 Begin of Changes ; Bhaumik Patel [CR-8222]

                string MDRT = (!string.IsNullOrEmpty(DataBinder.Eval(e.Item.DataItem, "MDRT").ToString())) ? (DataBinder.Eval(e.Item.DataItem, "MDRT").ToString()) : "";

                if (MDRT == "Yes")
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("itemrow");
                    tr.Attributes.Add("style", "background-color:Pink;");

                }
                //10.1 End of Changes ; Bhaumik Patel [CR-8222]
            }

        }        
       catch (Exception ex)
        {

        }
    }

    protected void gvAppdasbord_CMO_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsGreen = (Label)e.Item.FindControl("lblIsChannelGreen");
                if (lblIsGreen.Text.Equals("YES"))
                {
                    Label lblPolicyNo = (Label)e.Item.FindControl("lblpolno");
                    lblPolicyNo.BackColor = System.Drawing.Color.LimeGreen;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvAppdasbord_CMO_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkApplicationNo = (LinkButton)row.FindControl("lnkAppno");
                HiddenField hdnProductName = (HiddenField)row.FindControl("hdnProductName");
                Label lblChannelType = (Label)row.FindControl("lblUtmchannel");
                Label lblPolicyNo = (Label)row.FindControl("lblpolno");
                string strApplicationno = lnkApplicationNo.Text;
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                objCommonObj._ApplicationNo = strApplicationno;
                objCommonObj._ChannelType = "OFFLINE";
                objCommonObj._PolicyNo = lblPolicyNo.Text;
                objCommonObj._ProductName = hdnProductName.Value;
                Session["objCommonObj"] = objCommonObj;

                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('www.google.com','_blank');", true);
                //string url = "DMSVeiwer.aspx?ApplnNo=" + strApplicationno + "&DocType=Medical";
                //string url = "Appcode/CMO.aspx?&qsAppNo=" + strApplicationno + "&qsChannelName=" + UWSaralDecision.CommFun.Base64EncodingMethod("Offline") + "&qsPolicyNo=" + UWSaralDecision.CommFun.Base64EncodingMethod(lblPolicyNo.Text);
                string url = "Appcode/CMOApprove.aspx?&qsAppNo=" + strApplicationno;
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
                //Response.Redirect(string.Format("Appcode/CMO.aspx?qsUserGroup={0}&qsAppNo={1}&qsChannelName={2}&qsPolicyNo={3}&qsUserLimit={4}&qsClickBucketLink={5}",
                //    UWSaralDecision.CommFun.Base64EncodingMethod(objCommonObj._Bpmuserdetails._UserGroup),
                //     //UWSaralDecision.CommFun.Base64EncodingMethod(strApplicationno),
                //     strApplicationno,
                //    UWSaralDecision.CommFun.Base64EncodingMethod("Offline"),
                //    UWSaralDecision.CommFun.Base64EncodingMethod(lblPolicyNo.Text),
                //    UWSaralDecision.CommFun.Base64EncodingMethod(strUserLimit),
                //     UWSaralDecision.CommFun.Base64EncodingMethod(strclicklink) //Added by Suraj on 24 APRIL 2018 for Close file review
                //    ), false);

            }


        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    protected void lnkGrp_Click(object sender, EventArgs e)
    {
        //DASHBOARD OF GROUP 
        Response.Redirect("grpUW/GrpDashBoard.aspx", false);
        //Response.Redirect("grpUW/mydashboard.aspx", false);
    }

    //########################## Added by Shyam Starts ###########################################
    protected void gvAppdasbord_Revival_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblIsGreen = (Label)e.Item.FindControl("lblIsChannelGreen");
                if (lblIsGreen.Text.Equals("YES"))
                {
                    Label lblPolicyNo = (Label)e.Item.FindControl("lblpolno");
                    lblPolicyNo.BackColor = System.Drawing.Color.LimeGreen;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gvAppdasbord_Revival_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
                LinkButton lnkApplicationNo = (LinkButton)row.FindControl("lnkAppno");
                HiddenField hdnProductName = (HiddenField)row.FindControl("hdnProductName");
                Label lblChannelType = (Label)row.FindControl("lblUtmchannel");
                Label lblPolicyNo = (Label)row.FindControl("lblpolno");
                string strApplicationno = lnkApplicationNo.Text;
                objCommonObj = (CommonObject)Session["objCommonObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID;
                strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                objCommonObj._ApplicationNo = strApplicationno;
                objCommonObj._ChannelType = "OFFLINE";
                string strclicklink = string.Empty;
                objCommonObj._PolicyNo = lblPolicyNo.Text;
                objCommonObj._ProductName = hdnProductName.Value;
                Session["objCommonObj"] = objCommonObj;
                int intRef = -1;
                objComm.ManageApplicationLifeCycle(lnkApplicationNo.Text, strUserId, "APP_SELECT", false, ref intRef);
                #region Control Logic Begin.
                strAppstatusKey = (strUserGroup == "UW") ? "UL" : "DL";


                //##################### FG CODE To check user and redirect to uwdecision ####################################
                //Commented for testing bypass flow 
                objComm.OnlineApplicationLockChecker(strApplicationno, strUserId, strUserGroup, ref _ds);
                //##################### END ##################################################################################

                if (_ds != null && _ds.Tables[0].Rows.Count > 0)
                {
                    string strUserLimit = Convert.ToString(_ds.Tables[0].Rows[0]["LIMIT"]);
                    objComm.OnlineApplicationchangeStatus(strApplicationno, strUserId, strAppstatusKey, "", ref Result);
                    if (Result != 0 || Result != -1)
                    {
                        //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('www.google.com','_blank');", true);
                        Response.Redirect(string.Format("Appcode/RevivalUwdecision.aspx?qsUserGroup={0}&qsAppNo={1}&qsChannelName={2}&qsPolicyNo={3}&qsUserLimit={4}&qsClickBucketLink={5}",
                            UWSaralDecision.CommFun.Base64EncodingMethod(objCommonObj._Bpmuserdetails._UserGroup),
                             //UWSaralDecision.CommFun.Base64EncodingMethod(strApplicationno),
                             strApplicationno,
                            UWSaralDecision.CommFun.Base64EncodingMethod("Offline"),
                            UWSaralDecision.CommFun.Base64EncodingMethod(lblPolicyNo.Text),
                            UWSaralDecision.CommFun.Base64EncodingMethod(strUserLimit),
                             UWSaralDecision.CommFun.Base64EncodingMethod(strclicklink) //Added by Suraj on 24 APRIL 2018 for Close file review
                            ), false);
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#myModal1').modal();window.close();alert('test message');", true);
                    ClientScript.RegisterStartupScript(GetType(), "hwa", "window.close();alert('The case is already locked by other user');", true);
                    ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
                }
                #endregion Control Logic End.
                /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                objComm.ManageApplicationLifeCycle(lnkApplicationNo.Text, strUserId, "APP_SELECT", true, ref intRef);
                /*END HERE*/
            }
            else if (e.CommandName == "PendingDocs")
            {
                DataSet dsPendingDocs = new DataSet();
                RepeaterItem row = (RepeaterItem)(((Control)e.CommandSource).NamingContainer);
                LinkButton hdnApplicationNo = (LinkButton)row.FindControl("lnkAppno");
                Literal lblapp = (Literal)row.FindControl("lblapp");
                UserControl_PopupDoc WebUserControl1 = (UserControl_PopupDoc)FindControl("PendingDocs");
                if (!string.IsNullOrEmpty(hdnApplicationNo.Text))
                {
                    int response = 0;
                    WebUserControl1.FillPendingDoc(hdnApplicationNo.Text, "", ref response);
                    if (response == 1)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#divPendingDocPopup').modal('show');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "hwa", "$('#myModalNoDocPending').modal();", true);
                    }
                }

            }

        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }
    //########################## Added by Shyam End ###########################################

    //8.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    protected void lnkrights_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        CommAssD.UnderWriting_AssignmentUserAccess_GET_byUserID("CheckUserAccess", strUserId, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Appcode/Underwriting_Assignment_Module.aspx','_blank');", true);
        }
        else
        {
            string alertMessage = struserName + "  Have No Access .....";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
        }


    }

    protected void lnkrights1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        CommAssD.UnderWriting_AssignmentUserAccess_GET_byUserID("CheckUserAccess", strUserId, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //lblrights.Text = (ds.Tables[0].Rows.Count).ToString();
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Appcode/Underwriting_Assignment_Module.aspx','_blank');", true);
            //Response.Redirect("~/Appcode/Underwriting_Assignment_Module.aspx", true);
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
        else
        {
            string alertMessage = struserName + "  Have No Access .....";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
        }
    }

    protected void lnklogout_Click(object sender, EventArgs e)
    {
        int result = 0;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        
       CommAssD.Underwriting_AssignmentDetails_LOGINLOGOUT_Log(strUserId, struserName, strUserGroup, "LogOut", "Insert_Logout_Logs", "0", ref result);
       CommAssD.Underwriting_LOGIN_Log("INSERT_LOGOUT_LOGS", Session["NewSessionId"].ToString(), "", strUserId, "", "", "", "LogOut From Default3.aspx", "SaralUW", 1, 1, ref result);
        if (result > 0)
        {

            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect(ConfigurationSettings.AppSettings["Logoutkey"], true);
        }

    }
    //8.1 End of Changes; Bhaumik Patel - [CR-5307]
}