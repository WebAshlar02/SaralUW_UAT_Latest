using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platform.Utilities.LoggerFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using UWSaralObjects;
using IIBData;

/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :AMIT CHAUDHARY
METHODE/EVENT:PAGE LOAD
REMARK: COMMENTED AML PART FROM PAGE LOAD AS THERE IS NO NEED TO POPULATE THE SAME AS WE ARE DISPALY ON PENDING COUNT AND SAME 
WAS FETCH AND POPULATED ON PENDING CLICKS.
DateTime :03NOV2017
**********************************************************************************************************************************
*********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :AMIT CHAUDHARY
METHODE/EVENT:PAGE LOAD
REMARK: COMMENTED NSAP LOADING ON PAGE LOAD . SAME ARE NEED TO BE INSERT IN LIFE ASIA AT THE TIME OF DOCQC.AND SAME WAS INSERTED IN 
LF_LOADINGDETAILS.
DateTime :03NOV2017       
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:PAGE LOAD
REMARK: ADDED AUTO COMMENT FEATURE IN EXISTING SARAL
DateTime :09JAN17
*********************************************************************************************************************************
***********************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: MANAGE CONTROLS FOR ONLINE / OFFLINE
DateTime :12JAN17
*********************************************************************************************************************************
***********************************************************************************************************************************
COMMENT ID: 5
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: IS PAN AND AADHAR VERIFIED
DateTime :12JAN17
*********************************************************************************************************************************
 ************************************************************************************************************************************
COMMENT ID: 6
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: TO CHECH AND SAVE COMMENT BEFORE POSTING SAVE IS SMOKER FLAG AND FORCEFUL AADHAR VERIFICATION
DateTime :22JAN17
*********************************************************************************************************************************
*************************************************************************************************************************************
COMMENT ID: 7
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: TO ADD ISNSAP EVENT CODE INTO A METHOD AND CALL IT IN CALL BACK and do same for user refernce
DateTime :22FEB18
*********************************************************************************************************************************
*************************************************************************************************************************************
COMMENT ID: 8
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: TO ADD BRANCH BASE PREMIUM IN PRODUCT CODE AND SHOW ALERT MESSAGE
DateTime :28FEB18
*********************************************************************************************************************************
**************************************************************************************************************************************
COMMENT ID: 9
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: TO ADD AND SHOW TPA STATUS IN SARAL 
DateTime :14MAR18
*********************************************************************************************************************************
**************************************************************************************************************************************
COMMENT ID: 10
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: TO ADD AND SHOW RISK DECISION TYPE PROPOSER TYPE 
DateTime :26MAR18
*********************************************************************************************************************************
**************************************************************************************************************************************
COMMENT ID: 11
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: TO ADD AND UW INSTRUCTIONS
DateTime :30MAR18
*********************************************************************************************************************************
 ***********************************************************************************************************************************
COMMENT ID: 12
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:btnSaveAnswer_Click
REMARK: Save answer for close file review
DateTime :24 APRIL 2018
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 13
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:BindQuestion_CloseFile
REMARK: Bind questions for close file review
DateTime :24 APRIL 2018
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 14
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:grdclosefilerw_RowDataBound
REMARK: bind values to answer dropdown in question gridview for close file review 
DateTime :24 APRIL 2018
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 15
COMMENTOR NAME :Suraj Bhamre
METHODE/EVENT:DisableControl_CloseFileReview()
REMARK: disabled all control except Closefile review section for close file review
DateTime :24 APRIL 2018
*********************************************************************************************************************************
*********************************************************************************************************************************
COMMENT ID: 16
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT:
REMARK: TO ADD HOLD POLICY PRINTING FLAG
DateTime :07May18
*********************************************************************************************************************************
*********************************************************************************************************************************
COMMENT ID: 17
COMMENTOR NAME :SURAJ BHAMRE
METHODE/EVENT:ddlApplicationDetailsProposalType_SelectedIndexChanged
REMARK: WHEN PROPOSAL DROPDOWN SELECTED TO EMPLOYER THEN ONLY ASSIGNMENT DROPDOWN WILL SHOW
        WHEN PROPOSAL DROPDOWN SELECTED TO NRI THEN ONLY COUNTRY DROPDOWN WILL SHOW 
DateTime :30MAR18
*********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 18
COMMENTOR NAME : AJAY SAHU
METHODE/EVENT:ddlApplicationDetailsProposalType_SelectedIndexChanged
REMARK: Unchecked edit 
DateTime :15MAY18
*********************************************************************************************************************************
***********************************************************************************************************************************
COMMENT ID: 19
COMMENTOR NAME : SURAJ BHAMRE
METHODE/EVENT:
REMARK: SHOW WARNING MESSAGE FOR RISPECTIVE CONDITIONS
DateTime :16MAY18
*********************************************************************************************************************************
************************************************************************************************************************************
COMMENT ID: 20
COMMENTOR NAME : SHREEJIT
METHODE/EVENT:
REMARK: 
DateTime :24MAY18
*********************************************************************************************************************************
************************************************************************************************************************************
COMMENT ID: 21
COMMENTOR NAME : SHREEJIT
METHODE/EVENT:
REMARK: DEVELOPMEMT OF E&Y SHOWING 
DateTime :28MAY18
*********************************************************************************************************************************
 **********************************************************************************************************************************
COMMENT ID:22
COMMENTOR NAME : AJAY SAHU
METHODE/EVENT:
REMARK: CHECK RIDER
DateTime :11JUNE18
*********************************************************************************************************************************
 **********************************************************************************************************************************
COMMENT ID:23
COMMENTOR NAME : SHRIJEET SHANIL
METHODE/EVENT:
REMARK: CALL CONTRACT MODIFICATION ON POST BACK REFER 
DateTime :5JULY18
*********************************************************************************************************************************
* ***********************************************************************************************************************************
COMMENT ID:24
COMMENTOR NAME : AJAY SAHU
METHODE/EVENT:
REMARK: PLVC Video
DateTime :14Feb19
*********************************************************************************************************************************
* ***********************************************************************************************************************************
COMMENT ID:25
COMMENTOR NAME : shrijeet shanil
METHODE/EVENT:
REMARK: change E&Y value 
DateTime :30Mar19
*********************************************************************************************************************************
*******************************************************************************************************************************
// Sr. No.              : 26
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bibekananda Nanda [1103055]               
// BRD/CR/Codesk No/Win : ///27626    
// Date Of Creation     : 12.05.2020            
// Description          : 1.All the PAN card details to be saved and displayed after NSDL validation)
//*********************************************************************************************************************************/
//*******************************************************************************************************************************
// Sr. No.              : 27
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Shyam Patil             
// BRD/CR/Codesk No/Win : ///ENYScore changes Api    
// Date Of Creation     : 21.05.20201           
// Description          : 1.API call for ENY score
//*********************************************************************************************************************************/
//*******************************************************************************************************************************
// Sr. No.              : 28
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Brijesh Pandey              
// BRD/CR/Codesk No/Win : ///29084    
// Date Of Creation     : 08-06-2021.03.2020            
// Description          : Add Relation with Staff field against Is Staff flag 
//*********************************************************************************************************************************/
//******************************************************************************************************************************* 
// Sr. No.              : 29
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Suraj Bhamre           
// BRD/CR/Codesk No/Win : ///30450    
// Date Of Creation     : 29-06-2021            
// Description          : Add business exception readio button and agent vintage logic
//*********************************************************************************************************************************/
//******************************************************************************************************************************* 
// Sr. No.              : 30
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Pooja Shetye          
// BRD/CR/Codesk No/Win : CR - 29431    
// Date Of Creation     : 27-08-2021            
// Description          : PAN and FORM 60 Flagging validation 
//*********************************************************************************************************************************/

//******************************************************************************************************************************* 
// Sr. No.              : 31
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant Devkate        
// BRD/CR/Codesk No/Win : CR -2591    
// Date Of Creation     : 21-03-2022            
// Description          : Checked the Sum assured limit of UW user 
//*********************************************************************************************************************************/
//******************************************************************************************************************************* 
// Sr. No.              : 32
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant Devkate        
// BRD/CR/Codesk No/Win : CR -2639    
// Date Of Creation     : 4-04-2022            
// Description          : Checked the FSAR limit with UW user limit
//*********************************************************************************************************************************/
//**********************************************************************
// Sr. No.              : 33
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win :  CR-2809
// Date Of Creation     : 11/04/2022
// Description          : Seperate IIB Query for LA ,Proposal and Payer
//**********************************************************************
//**********************************************************************
// Sr. No.              : 34
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave - MFL00886
// BRD/CR/Codesk No/Win :  CR-30554
// Date Of Creation     : 19/05/2022
// Description          :Integrate SmartApiEnyRiskScore and Add EnyScore value and Risk Score value
//**********************************************************************
//**********************************************************************
// Sr. No.              : 35
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave - MFL00886
// BRD/CR/Codesk No/Win :  CR-2593
// Date Of Creation     : 01/06/2022
// Description          :FC_Video Mer V1 3
//************************************************************************
//**********************************************************************
// Sr. No.              : 36
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave - MFL00886
// BRD/CR/Codesk No/Win : CR-3387 
// Date Of Creation     : 16/06/2022
// Description          : AML risk categorisation in Life Asia
//************************************************************************
//******************************************************************************************************************************* 
// Sr. No.              : 37
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant Devkate          
// BRD/CR/Codesk No/Win : CR-2304  
// Date Of Creation     : 29-07-2022            
// Description          : Audit Point  Customer contact details validation with Agent database
//*******************************************************************************************************************************
//******************************************************************************************************************************* 
// Sr. No.              : 38
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel         
// BRD/CR/Codesk No/Win : CR-4134  
// Date Of Creation     : 25-08-2022            
// Description          :Add Box For RedFlagging of Clients ID
//******************************************************************************************************************************* 
//********************************************************************
// Sr. No.              : 39
// Company              : Life            
// Module               : UW Saral   
//METHODE/EVENT:BUSSINESS LAYER
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win :  CR-30543 
// Date Of Creation     :  12/09/2022
// Description          : Added functinality on IsSmokar 
//***********************************************************
//******************************************************************************************************************************* 
// Sr. No.              : 40
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant      
// BRD/CR/Codesk No/Win : CR-4971  
// Date Of Creation     : 15-11-2022            
// Description          : Enabling NRI contact number in primary number field
//******************************************************************************************************************************* 
//******************************************************************************************************************************* 
// Sr. No.              : 41
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Royson Pinto      
// BRD/CR/Codesk No/Win : CR-4783  
// Date Of Creation     : 22-11-2022            
// Description          : Create a Restrict Further Cover Option
//*******************************************************************************************************************************
//******************************************************************************************************************************* 
// Sr. No.              : 42
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Jayendra Patankar [WebAshlar01]      
// BRD/CR/Codesk No/Win :  
// Date Of Creation     : 25-11-2022            
// Description          : Create a Restrict for UINNO
//*******************************************************************************************************************************
//**********************************************************************
// Sr. No.              : 43
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave - MFL00886
// BRD/CR/Codesk No/Win : CR-3381 
// Date Of Creation     : 18/11/2022
// Description          : Mapping of special Insurance
//************************************************************************
//**********************************************************************
// Sr. No.              : 44
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-3334 
// Date Of Creation     : 18/11/2022
// Description          :Retup Multiple Reason Option
//**********************************************************************

//**********************************************************************
// Sr. No.              : 45
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 3/01/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************
//**********************************************************************
// Sr. No.              : 46
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-4753 
// Date Of Creation     : 20/03/2023
// Description          : Exception approval Details to be Added in BOE Assessment Sheet
//**********************************************************************
//**********************************************************************
// Sr. No.              : 47
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Jayendra [WebAshlar01]
// BRD/CR/Codesk No/Win : STP Changes 
// Date Of Creation     : 05-03-2023
// Description          :  1. Schedular for the STP Integration
//**********************************************************************
//**********************************************************************
// Sr. No.              : 48
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel [WebAshlar02]
// BRD/CR/Codesk No/Win : CR-5855  
// Date Of Creation     : 04-05-2023
// Description          :  Grid based Loading access for Counter offer in Saral.
//**********************************************************************
//**********************************************************************
// Sr. No.              : 49
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave [MFL00886]
// BRD/CR/Codesk No/Win : CR-7049  
// Date Of Creation     : 10-07-2023
// Description          :  Add AcceptanceReason function
//**********************************************************************
//******************************************************************
// Sr. No.              : 50
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Jay Patnakar [Webashlar01]
// BRD/CR/Codesk No/Win : 
// Date Of Creation     : 18-08-2023
// Description          : Add Input filed for Acceptance Reason value Other selected
//********************************************************************** 

public partial class Appcode_Default : System.Web.UI.Page
{
    string strApplicationno = string.Empty;
    string strChannelType = string.Empty;
    string strUserId = string.Empty;
    string strUWmode = string.Empty;
    string strUserGroup = string.Empty;
    string strOptinselected = string.Empty;
    string strAppstatusKey = string.Empty;
    string strPolicyNo = string.Empty;
    string strConsentRespons = string.Empty;
    string UserLimit = string.Empty;
    string strClickBucketLink = string.Empty;//Added by Suraj on 24 APRIL 2018 for Close file review
    Commfun objComm = new Commfun();
    BussLayer objBuss = new BussLayer();
    DataSet _ds = new DataSet();
    DataSet dsMSAR = new DataSet();
    DataTable _Dt = new DataTable();
    DataSet _dsAppNo = new DataSet();
    DataSet _dsAgentdetails = new DataSet();
    DataSet _dsBank = new DataSet();
    DataSet _dsPrevPol = new DataSet();
    DataSet _dsJournal = new DataSet();
    DataSet _dsFollowuo = new DataSet();
    DataSet _dsProdontrol = new DataSet();
    DataSet _dsUWDisplayOption = new DataSet();
    DataSet _dsAppdtls = new DataSet();
    DataSet _dsPandtls = new DataSet();
    DataSet _dsTsarMsarDtls = new DataSet();
    DataSet _dsProdDtls = new DataSet();
    DataTable _dsRiderDtls = new DataTable();
    DataSet _dsFundsDtls = new DataSet();
    DataSet _dscommentDtls = new DataSet();
    DataSet _dsPymentsDtls = new DataSet();
    DataSet _dsRequrmentDtls = new DataSet();
    // 47.1 Start of Changes; Jayendra  - [Webashlar01]
    DataSet _dsRequrmentDtls_STP = new DataSet();
    // 47.1 End of Changes; Jayendra  - [Webashlar01]
    DataSet _dsReceiptDtls = new DataSet();
    DataSet _dsLoadingDtls = new DataSet();
    DataSet _dsReportDtls = new DataSet();
    DataSet _dsUWMaster = new DataSet();
    DataSet _dsPageData = new DataSet();
    DataSet _dsDashcount = null;
    DataSet _dsQuestion = new DataSet();
    string response = "";
    UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
    CommonObject objCommonObj = new CommonObject();
    UWSaralServices.CommFun objsercomm = new UWSaralServices.CommFun();
    ChangeValue objChangeObj = new ChangeValue();
    Commfun objcomm = new Commfun();
    int strLApushErrorCode;
    string strLApushStatus;
    string clsName = "";
    decimal ServiceTax;
    bool HighRisk;
    bool IsCommentSave;
    int IsCommentStore;
    /*added by shri on 23 june 17*/
    public string strAppno = string.Empty;
    /*end here*/
   // string strPolicyNo = string.Empty;
    //34.1 Begin of Changes; Sagar Thorave-[mfl00886]
    string IsENY = ConfigurationManager.AppSettings["IsENY"].ToString();
    string IsRiskScore = ConfigurationManager.AppSettings["IsRiskScore"].ToString();
    //34.1 End of Changes; Sagar Thorave-[mfl00886]
    bool IsCombi = false;//added by suraj for combi product

    string struserName = string.Empty;
    CommanAssignmentDetails commobj = new CommanAssignmentDetails();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["objLoginObj"] == null)
        {
            Response.Redirect("~/9011042143.aspx");
        }
        else
        {
            Bindeventhandler();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        Logger.Info(strApplicationno + " STAG2:-page load start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + " STAG2:-A" + System.Environment.NewLine);
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        //Added by Suraj on 01-Nov-2018 -wanted to restrict copy and paste of URL to open the page
        //pragati changes start cr-27524
        ddlRsnExcessPremium.Enabled = false;
        lblErrorDecisiondtls.Text = "";
        //pragati changes end

        //38.1 Begin of Changes; Bhaumik Patel - [CR-4134]
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        struserName = objCommonObj._Bpmuserdetails._UserName;
        //38.1 End of Changes; Bhaumik Patel - [CR-4134]
        try
        {

            Logger.Info(strApplicationno + " STAG2:-B" + System.Environment.NewLine);
            Session["IsCombi"] = IsCombi;
            //IsCommentStore = 0;
            /*ID:20 START*/

            if (Request.QueryString["qsUserGroup"] != null)
            {
                strUserGroup = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsUserGroup"]);
                Logger.Info(strApplicationno + " STAG2:-C" + System.Environment.NewLine);
            }
            if (Request.QueryString["qsAppNo"] != null)
            {
                //strApplicationno = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
                strApplicationno = Request.QueryString["qsAppNo"];
                strAppno = strApplicationno;
                Logger.Info(strApplicationno + "STAG1:-Retrive Querystring Value");
                Logger.Info(strApplicationno + " STAG2:-D" + System.Environment.NewLine);
            }
            if (Request.QueryString["qsChannelName"] != null)
            {
                strChannelType = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsChannelName"]);
                Logger.Info(strApplicationno + " STAG2:-E" + System.Environment.NewLine);
            }
            if (Request.QueryString["qsPolicyNo"] != null)
            {
                strPolicyNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsPolicyNo"]);
                Logger.Info(strApplicationno + " STAG2:-F" + System.Environment.NewLine);
            }
            /*ID:20 END*/
            MasterPageComparision objOldMasterComparision = new MasterPageComparision();
            //strUserGroup = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsUserGroup"]);
            //strApplicationno = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
            //strChannelType = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsChannelName"]);
            //strPolicyNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsPolicyNo"]);


            //if (Request.QueryString["qsClickBucketLink"] != null)
            //{
            //    strClickBucketLink = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsClickBucketLink"]);
            //}
            //end
            if (Request.QueryString["qsUserLimit"] != null)
            {
                hdnUserLimit.Value = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsUserLimit"]);
                 UserLimit = hdnUserLimit.Value.ToString();
                Logger.Info(strApplicationno + " STAG2:-F" + System.Environment.NewLine);
            }

            if (objCommonObj != null)
            {
                Logger.Info(strApplicationno + " STAG2:-H" + System.Environment.NewLine);
                string strOfflineClass = string.Empty;
                string strOnlineClass = string.Empty;
                /*added by shri on 29 aug 17 to hide client DEQC for online */
                if (strChannelType.Equals("ONLINE"))
                {
                    divOfflineAml.Attributes["class"] = divOfflineAml.Attributes["class"] + " HideControl";
                    divOnlineAml.Attributes["class"] = divOnlineAml.Attributes["class"].Replace(" HideControl", string.Empty);
                    Logger.Info(strApplicationno + " STAG2:-H" + System.Environment.NewLine);
                }
                else
                {

                    divOnlineAml.Attributes["class"] = divOnlineAml.Attributes["class"] + " HideControl";
                    divOfflineAml.Attributes["class"] = divOfflineAml.Attributes["class"].Replace(" HideControl", string.Empty);
                    Logger.Info(strApplicationno + " STAG2:-J" + System.Environment.NewLine);
                }
                /*end here*/
            }
            if (ddlUWDecesion.SelectedValue == "0" || ddlUWDecesion.SelectedValue == "proposal")
            {
                divUWReason1.Visible = false;
            }

            if (!IsPostBack)
            {
                //Start changes by rupaliezytek
                getFGData();
                //End changes by rupaliezytek
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/9011042143.aspx");
                }
                /*
                if (ViewState["CommonViewState"] == null)
                {
                    CommonViewState viewstst = new CommonViewState();
                    viewstst.IsCommentSave = false;
                    ViewState["CommonViewState"] = viewstst;
                }
                else
                {
                    CommonViewState viewstst = (CommonViewState)ViewState["CommonViewState"];
                    viewstst.IsCommentSave = false;
                    ViewState["CommonViewState"] = viewstst;
                }
                */
                Logger.Info(strApplicationno + " STAG2:-K" + System.Environment.NewLine);
                if (Session["objCommonObj"] != null)
                {
                    Logger.Info(strApplicationno + " STAG2:-L" + System.Environment.NewLine);
                    int intTrackingRet = -1;
                    objCommonObj = (CommonObject)Session["objCommonObj"];
                    strUserId = objCommonObj._Bpmuserdetails._UserID;
                    /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                    int intRef = -1;
                    objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_WINDOW", false, ref intRef);
                    objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_LOAD", false, ref intRef);
                    /*END HERE*/
                    Logger.Info("***********************UW Journey Start****************************" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + " STAG1:-Retrive Application Number " + strApplicationno + " and ChannelType as " + strChannelType + "and user group is" + strUserGroup + System.Environment.NewLine);
                    /*added by shri on 28 dec 17 to add tracking*/
                    InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "LOAD", ref intTrackingRet);
                    /*end here*/
                    //Added by Suraj on 24 APRIL 2018 for close file review set text for click bucket link
                    DataSet ds = new DataSet();
                    objComm.CheckCloseFileReview(strUserId, strApplicationno, ref ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        strClickBucketLink = "Close_File_Review";
                        if (strApplicationno.Contains("F"))
                        {
                            strChannelType = "ONLINE";
                        }
                        else
                        {
                            strChannelType = "OFFLINE";
                        }
                        //hdncolsefilerw.Value = "";
                    }
                    else
                    {
                        strClickBucketLink = "";
                    }
                    Logger.Info(strApplicationno + " STAG2:-M" + System.Environment.NewLine);
                    DisableControl_CloseFileReview();
                    /*end here*/

                    if (!string.IsNullOrEmpty(strApplicationno))
                    {
                        #region Fill master Details Begins.
                        Logger.Info(strApplicationno + " STAG2:-N" + System.Environment.NewLine);
                        Logger.Info(strApplicationno + " STAG2:-Fill master Details Region" + System.Environment.NewLine);
                        FillMasterDetails(ref _dsUWMaster);
                        //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                        FillloadingReason("");
                        lblErrorDecisiondtls.Text = "";
                        //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
                        Logger.Info(strApplicationno + " STAG2:-O" + System.Environment.NewLine);
                        FillProductControlDetails(strApplicationno, strChannelType);
                        Logger.Info(strApplicationno + " STAG2:-P" + System.Environment.NewLine);
                        objcomm.OnlineLoadingMasterDetails_GET(ref _dsFollowuo, strApplicationno, strChannelType);
                        Logger.Info(strApplicationno + " STAG2:-Q" + System.Environment.NewLine);
                        if (_dsFollowuo != null && _dsFollowuo.Tables[0].Rows.Count > 0)
                        {
                            Logger.Info(strApplicationno + " STAG2:-R" + System.Environment.NewLine);
                            ViewState["LoadMaster"] = _dsFollowuo.Tables[0];
                            ddlLoadRiderCode.DataSource = _dsFollowuo.Tables[0];
                            ddlLoadRiderCode.DataTextField = "NAME";
                            ddlLoadRiderCode.DataValueField = "VALUE";
                            //ddlLoadRiderCode.DataMember = "Code";
                            ddlLoadRiderCode.DataBind();
                            ddlLoadRiderCode.Items.Insert(0, new ListItem("--Select--", "0"));
                            Logger.Info(strApplicationno + " STAG2:-S" + System.Environment.NewLine);
                        }
                        ddlLoadRsn1.Items.Clear();
                        #endregion Fill master details end.

                        #region Fill Individual Section Details Begins.
                        /*ADDED BY SHRI ON 08 NOV TO FETCH DATA FROM OBJECT TABLE*/
                        //objcomm.FetchApplicationObject(strApplicationno, strChannelType, strUserId, ref _ds);
                        //if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        //{
                        //    string strCommonObjectXml = Convert.ToString(_ds.Tables[0].Rows[0]["OBJECTDATA"]);
                        //    hdnSrNo.Value = Convert.ToString(_ds.Tables[0].Rows[0]["SRNO"]);
                        //    DataSet _dsCommonObject = new DataSet();
                        //    Commfun.ConvertXmlToDataSet(strCommonObjectXml, ref _dscommentDtls);
                        //    if (_dscommentDtls != null && _dscommentDtls.Tables.Count > 0 && _dscommentDtls.Tables[0].Rows.Count > 0)
                        //    {
                        //        FillUwDecision(_dscommentDtls);
                        //    }
                        //    _ds.Tables.RemoveAt(0);
                        //    FillUwDecision(_ds);
                        //}
                        //else
                        //{
                        Logger.Info(strApplicationno + "STAG3:-Fill Individual section Details Region" + System.Environment.NewLine);
                        objcomm.LoadUWsaralPageDetails(ref _dsPageData, strApplicationno, strChannelType, strUserId);
                        Logger.Info(strApplicationno + " STAG2:-T" + System.Environment.NewLine);
                        if (_dsPageData != null && _dsPageData.Tables.Count > 0)
                        {
                            Logger.Info(strApplicationno + " STAG2:-U" + System.Environment.NewLine);
                            FillUwDecision(_dsPageData);
                            ENYScore(strApplicationno);
                            Logger.Info(strApplicationno + " STAG2:-W" + System.Environment.NewLine);

                        }

                        //}
                        /*added by shri on 08 for temp data pull*/
                        //objcomm.FetchApplicationObject(strApplicationno, ref _ds);
                        //string strCommonObjectXml = Convert.ToString(_ds.Tables[0].Rows[0]["OBJECTDATA"]);
                        //hdnSrNo.Value = Convert.ToString(_ds.Tables[0].Rows[0]["SRNO"]);
                        //DataSet _dsCommonObject = new DataSet();
                        //Commfun.ConvertXmlToDataSet(strCommonObjectXml, ref _dscommentDtls);
                        //if (_dscommentDtls != null && _dscommentDtls.Tables.Count > 0 && _dscommentDtls.Tables[0].Rows.Count > 0)
                        //{
                        //    FillUwDecision(_dscommentDtls);
                        //}
                        /*end here*/
                        //}
                        //ID:2 BEGINS
                        //
                        //ID:2 END.
                        /*added by shri on 21 june 17*/

                        #region NSAP Loading.
                        FillNsaploadingDetails(strApplicationno, strChannelType);
                        Logger.Info(strApplicationno + " STAG2:-X" + System.Environment.NewLine);
                        #endregion NSAP Loading.

                        #region fetch client basic details
                        FetchClientBasicDetails(ref _ds, strApplicationno);
                        Logger.Info(strApplicationno + " STAG2:-Y" + System.Environment.NewLine);
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            gvClientDetails.DataSource = _ds;
                            gvClientDetails.DataBind();
                            _ds.Clear();
                            Logger.Info(strApplicationno + " STAG2:-Z" + System.Environment.NewLine);
                        }
                        #endregion

                        #region AML
                        //ID:1 BEGINS
                        //UserControl_PopupDoc objUserControl = (UserControl_PopupDoc)PendingDocs;
                        //int response = 0;
                        //objUserControl.FillPendingDoc(strApplicationno, "", ref response);
                        //ID:1 END
                        #endregion
                        /*end here*/
                        #endregion Fill Individual Section Details Begins.
                        divUWReason1.Visible = false;
                        Logger.Info(strApplicationno + " STAG2:-A1" + System.Environment.NewLine);
                    }


                    objComm.FetchMasterRequiredData(strApplicationno, strChannelType, ref _ds);
                    Logger.Info(strApplicationno + " STAG2:-A2" + System.Environment.NewLine);
                    foreach (DataTable dt in _ds.Tables)
                    {
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                Logger.Info(strApplicationno + " STAG2:-A3" + System.Environment.NewLine);
                                switch (dt.Rows[0][0].ToString())
                                {
                                    case "PRODUCT":
                                        {
                                            Logger.Info(strApplicationno + " STAG2:-A4" + System.Environment.NewLine);
                                            FillProductDetails(dt, objOldMasterComparision);
                                            break;
                                        }

                                    case "CLIENT":
                                        {
                                            Logger.Info(strApplicationno + " STAG2:-A5" + System.Environment.NewLine);
                                            FillClientDetails(dt, objOldMasterComparision);
                                            break;
                                        }
                                    case "RIDERDETAILS":
                                        {
                                            Logger.Info(strApplicationno + " STAG2:-6" + System.Environment.NewLine);
                                            FillRiderDetails(dt, objOldMasterComparision);
                                            break;
                                        }
                                }
                            }
                        }
                    }
                    Logger.Info(strApplicationno + " STAG2:-A7" + System.Environment.NewLine);
                    BindQuestion_CloseFile();//Added by Suraj on 24 APRIL 2018 for close file review bind questions
                    Logger.Info(strApplicationno + " STAG2:-A8" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "STAG3:-Fill Event Handler For Master Page" + System.Environment.NewLine);
                    //Bindeventhandler();
                    /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
                    objComm.ManageApplicationLifeCycle(strApplicationno, strUserId, "UW_DECISION_LOAD", true, ref intRef);
                    Logger.Info(strApplicationno + " STAG2:-A9" + System.Environment.NewLine);
                    //objComm.MaintainApplicationLog(strApplicationno, "OPEN", string.Empty, strUserId, "", ref intRef);
                    /*added by shri on 28 dec 17 to update tracking*/
                    UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
                    /*END HERE*/
                    /*Added by Suraj on 22 March 2019 for Video MER link*/
                    //BindVideoMER();
                    /*End*/
                    OfacService();

                    /*
                    if (objcomm.CheckIsSignUploaded(strApplicationno))
                    {
                        RFVSignatr.Enabled = true;
                        divSignature.Style.Add("display", "inline");
                    }
                    else
                    {
                        RFVSignatr.Enabled = false;
                        divSignature.Style.Add("display", "none");
                    }
                    */
                    //intTrackingRet 
                }
                else
                {
                    Response.Redirect("../9011042143.aspx");
                }
                System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                objSerializer.Serialize(objOldMasterComparision);
                hdnOldValue.Value = objSerializer.Serialize(objOldMasterComparision); //Commfun.GetXMLFromObject(objOldMasterComparision);
                /*added by shri on 05 dec 17 to add application log*/
                int intRet = -1;

                //Added By Royson Pinto on 22nd NOV to store Restrict Further Cover
                objCommonObj._Restrict_Further_Cover = objBuss.GetRestrict_Further_Cover(objCommonObj._ApplicationNo);
                if (objCommonObj._Restrict_Further_Cover == "Yes")
                {
                    RadioButton1.Checked = true;
                    Session["objCommonObj"] = objCommonObj;
                }
                else if (objCommonObj._Restrict_Further_Cover == "No")
                {
                    RadioButton2.Checked = true;
                    Session["objCommonObj"] = objCommonObj;
                }
                //End

                //Added parameter "SARAL" to change status SP for enrty in mis report  table from which application
                objcomm.ManageApplicationStatus(strApplicationno, "OPEN", string.Empty, strUserId, string.Empty, "SARALUW", ref intRet);
                /*end here*/

                //34.1 Begin of Changes; Sagar Thorave-[mfl00886]
                if (IsENY == "true")
                {
                    Get_EnyScore_Data();
                }
                if (IsRiskScore == "false")
                {
                    Get_RiskScore_Data();
                }
                //34.1 End of Changes; Sagar Thorave-[mfl00886]

                //36.1 Begin of Changes; Sagar Thorave-[mfl00886]
                GetAmlRiskValue();
                //36.1 End of Changes; Sagar Thorave-[mfl00886]

                //43.1 Begin of Changes; Sagar Thorave-[mfl00886]
                SetSpecialInsurance();
                //43.1 End of Changes; Sagar Thorave-[mfl00886]

                //40.1  Begin of changes Sushant Devkate- mfl00905
                GetcheckISNRICustomer();
                //40.1 End of changes Sushant Devkate- mfl00905

                // 47.1 Begin of Changes; Jayendra  - [Webashlar01]
                FillRUW_STP_Message(strApplicationno);
                // 47.1 End of Changes; Jayendra  - [Webashlar01]
            }
            /*ID:7 START*/
            else
            {

                if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].Equals("aml"))
                {
                    objcomm.OnlineNsapProductDetails_GET(ref _dsProdontrol, strApplicationno, strChannelType);
                    if (_dsProdontrol != null && _dsProdontrol.Tables.Count > 0 && _dsProdontrol.Tables[0].Rows.Count > 0)
                    {
                        if (!cbIsNsap.Checked)
                        {
                            cbIsNsap.Checked = true;
                            ChangeNsapValue();
                        }
                    }
                    else
                    {
                        if (cbIsNsap.Checked)
                        {
                            cbIsNsap.Checked = false;
                            ChangeNsapValue();
                        }
                    }
                }
                else if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].Equals("refer"))
                {
                    btnSaveToDatabase(new object(), new EventArgs());
                    /*ID:23 START*/
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                    if (strChannelType.Equals("ONLINE"))
                    {
                        int intRet = -1;
                        objBuss = new BussLayer();
                        objBuss.UpdatePolicyStatus(strApplicationno, "PS", strUserId, ref intRet);

                    }
                    /*ID:23 END*/
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "SHOW SUCCESS MESSAGE AND CLOSE PAGE", "window.close();alert('application is assigned to user successfully');", true);
                }
            }
            /*ID:7 END*/
            //else
            //{
            //    if (Session["objCommonObj"] != null)
            //    {
            //        objCommonObj = (CommonObject)Session["objCommonObj"];
            //        /*added by shri on 29 aug 17 to hide client DEQC for online */
            //        if (objCommonObj._ChannelType.ToLower() == "online")
            //        {
            //            //HIDE HEALTH
            //            divOfflineAml.Visible = divHealth.Visible = false;
            //            divOnlineAml.Visible = true;
            //        }
            //        else
            //        {
            //            //SHOW HEALTH
            //            divOfflineAml.Visible = divHealth.Visible = true;
            //            divOnlineAml.Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        Response.Redirect("../9011042143.aspx");
            //    }

            //}            
            /*ID:4 START*/
            //ManageContent();
            /*ID:4 END*/


           


            Logger.Info(strApplicationno + " STAG2:-page load end" + System.Environment.NewLine);
        }
        catch (Exception ex)
        {
            throw ex;//Logger.Info(strApplicationno + "STAG3:-Function Call PageLoad"+ " : "+ex + System.Environment.NewLine);
        }
    }


    //34.1 Begin of Changes; Sagar Thorave-[mfl00886]
    public void Get_RiskScore_Data()
    {
        try
        {
            EnyScoreRequest enyRequest = new EnyScoreRequest();
            DataSet ds2 = new Commfun().Featch_SMARTAPIRISK_Score_Details(txtAppno.Text);
            if (ds2.Tables[0].Rows.Count != 0 && ds2.Tables[0].Rows.Count > 0)
            {
                enyRequest.DataKeyName = "ApplicationNo";
                enyRequest.Source = "UWSaral";
                enyRequest.DataKeyValue = (txtAppno.Text);
                enyRequest.Laname = (ds2.Tables[0].Rows[0]["LANAME"].ToString());
                enyRequest.La_Pcode = (ds2.Tables[0].Rows[0]["LA_PCODE"].ToString());
                enyRequest.La_Occupation = (ds2.Tables[0].Rows[0]["LA_OCCUPATION"].ToString());
                enyRequest.La_Education = (ds2.Tables[0].Rows[0]["LA_EDUCATION"].ToString());
                enyRequest.La_Education_Desc = (ds2.Tables[0].Rows[0]["LA_EDUCATION_DESC"].ToString());
                enyRequest.Product_Name = txtProname.Text;
                enyRequest.La_Dob = (ds2.Tables[0].Rows[0]["LA_DOB"].ToString());
                enyRequest.APIDetails = "RiskScoreDetails";
                enyRequest.Agntnum = txtAgentcode.Text;
                enyRequest.Agentname = txtAgentName.Text;
                enyRequest.La_Gender = (ds2.Tables[0].Rows[0]["LA_GENDER"].ToString());
                enyRequest.La_Annual_Income = (ds2.Tables[0].Rows[0]["LA_ANNUAL_INCOME"].ToString());
                enyRequest.Ppt = txtPrepayterm.Text;
                enyRequest.Pt = txtPolterm.Text;
                enyRequest.La_Age_Proof = (ds2.Tables[0].Rows[0]["LA_AGE_PROOF"].ToString());
                enyRequest.Register = (ds2.Tables[0].Rows[0]["REGISTER"].ToString());
                enyRequest.Sumassured = (ds2.Tables[0].Rows[0]["sumAssured"].ToString());
                enyRequest.La_Code = (ds2.Tables[0].Rows[0]["LA_CODE"].ToString());
                enyRequest.Product_Code = (ds2.Tables[0].Rows[0]["PRODUCT_CODE"].ToString());
                enyRequest.La_Age_At_Entry = (ds2.Tables[0].Rows[0]["LA_AGE_AT_ENTRY"].ToString());
                enyRequest.Channel = (ds2.Tables[0].Rows[0]["Channel"].ToString());
                enyRequest.Premium_Amt = (ds2.Tables[0].Rows[0]["PREMIUM_AMT"].ToString());
                enyRequest.Branch_Name = (ds2.Tables[0].Rows[0]["BRANCH_NAME"].ToString());
                enyRequest.Billfreq = (ds2.Tables[0].Rows[0]["BILLFREQ"].ToString());
                enyRequest.Statcode = (ds2.Tables[0].Rows[0]["STATCODE"].ToString());
                enyRequest.Ape = (ds2.Tables[0].Rows[0]["APE"].ToString());
                enyRequest.La_Incomeprf = (ds2.Tables[0].Rows[0]["LA_INCOMEPRF"].ToString());
                enyRequest.Nominee_Dob = (ds2.Tables[0].Rows[0]["NOMINEE_DOB"].ToString());
                enyRequest.Nomineerelation = (ds2.Tables[0].Rows[0]["NOMINEERELATION"].ToString());
                enyRequest.Owner_Pancard = (ds2.Tables[0].Rows[0]["OWNER_PANCARD"].ToString());
                enyRequest.La_State = (ds2.Tables[0].Rows[0]["LA_STATE"].ToString());
                string errorCode = CallRiskScore(enyRequest);
                if (!string.IsNullOrEmpty(errorCode))
                {
                    if (errorCode == "0")
                    {
                        new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, "Api called successfully", "UWSaral", "RiskScoreDetails", "Success");

                    }
                    else
                    {
                        new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, "Api call successfully-erro", "UWSaral", "RiskScoreDetails", "Success");

                    }
                }
            }
            else
            {
                new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, "Api Data not found in database ", "UWSaral", "RiskScoreDetails", "Error");
            }
        }
        catch (Exception ex)
        {
            new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, ex.Message, "UWSaral", "RiskScoreDetails", "Error");

        }

    }
    private string CallRiskScore(EnyScoreRequest riskeny)
    {
        try
        {
            string rs = string.Empty;
            string inputJson = string.Empty;
            string result = string.Empty;
            string Authorization = string.Empty;
            string apiUrl = ConfigurationManager.AppSettings["RiskScoreApiURL"].ToString();
            generateToken(ref Authorization);

            if (!String.IsNullOrEmpty(Authorization))
            {
                inputJson = (new JavaScriptSerializer()).Serialize(riskeny);
                result = SmartApi(apiUrl, "GetRiskScore", inputJson, Authorization, riskeny.DataKeyValue);
                EnyScoreResponse objResponseClass = JsonConvert.DeserializeObject<EnyScoreResponse>(result);

                if (objResponseClass.responseResult.responseCode == "0")
                {
                    string score = objResponseClass.responseBody.risk_Score;
                    if (!string.IsNullOrEmpty(score))
                    {
                        InsertRiskVal(objResponseClass.responseBody.policyNo, objResponseClass.responseBody.underwriting_Due_Diligence_Required, "",
                            objResponseClass.responseBody.risk_Score, objResponseClass.responseBody.suggestive_Requirment, objResponseClass.responseBody.remarks, objResponseClass.responseBody.dataKeyValue, objResponseClass.responseBody.sumAssured, objResponseClass.responseBody.agntNum, objResponseClass.responseBody.channel_Code, objResponseClass.responseBody.channel_Name);
                        //set risk score
                        txtSaralRiskScore.Text = score;
                        return objResponseClass.responseResult.responseCode;
                    }
                    else
                    {
                        new Commfun().Insert_Logs_RiskAndEY(riskeny.DataKeyValue, "RiskScore is null ", "UWSaral", "CallRiskScore", "Fail to update");
                        return objResponseClass.responseResult.responseCode;
                    }

                }
                else
                {
                    return objResponseClass.responseResult.responseCode;
                }


            }
            else
            {
                new Commfun().Insert_Logs_RiskAndEY(riskeny.DataKeyValue, "Token not found ", "UWSaral", "CallRiskScore", "Fail");

            }
        }

        catch (Exception ex)
        {
            new Commfun().Insert_Logs_RiskAndEY(riskeny.DataKeyValue, ex.Message, "UWSaral", "CallRiskScore", "Fail");

        }
        return "";
    }
    public void InsertRiskVal(string policyNo, string underwriting_Due_Diligence_Required, string Parameter_Combinations,
        string risk_Score, string suggestive_Requirment, string remarks, string AppNo, string sumAssured, string AgentNum, string channelCode, string ChannelName)
    {
        new Commfun().Insert_RiskScore_Details(policyNo, underwriting_Due_Diligence_Required, Parameter_Combinations,
        risk_Score, suggestive_Requirment, remarks, AppNo, sumAssured, AgentNum, channelCode, ChannelName);
    }

    private void Get_EnyScore_Data()
    {
        try
        {
            string relation_nominee_la = "";
            EYRiskScoreRequest eny_Score = new EYRiskScoreRequest();
            RequestSource resSoc = new RequestSource();
            Requestbody1 resBody = new Requestbody1();
            DataSet ds2 = new Commfun().Featch_SMARTAPIENYFLAG_Details(txtAppno.Text);
            if (ds2.Tables[0].Rows.Count != 0 && ds2.Tables[0].Rows.Count > 0)
            {
                resSoc.DataKeyName = "ApplicationNo";
                resSoc.DataKeyValue = txtAppno.Text;
                resSoc.Source = "UWSaral";
                resSoc.APIDetails = "EYScoreDetails";
                resBody.relation_nominee_la = relation_nominee_la;
                resBody.PolicyID = txtPolno.Text;
                resBody.AppNo2 = txtAppno.Text;
                resBody.PolicyTerm = txtPolterm.Text;
                resBody.Agntnum = txtAgentcode.Text;
                resBody.LA_name = (ds2.Tables[0].Rows[0]["LANAME"].ToString());
                string date_of_birth = (ds2.Tables[0].Rows[0]["LA_DOB"].ToString());
                string Dob = Convert.ToDateTime(date_of_birth).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                resBody.LA_DOB = Dob;
                resBody.LA_Annual_Income = (ds2.Tables[0].Rows[0]["LA_ANNUAL_INCOME"].ToString());
                resBody.LA_Education = (ds2.Tables[0].Rows[0]["LA_EDUCATION_DESC"].ToString());
                resBody.LA_Occupation = (ds2.Tables[0].Rows[0]["LA_OCCUPATION"].ToString());
                resBody.LA_Occupation_Desc = (ds2.Tables[0].Rows[0]["LA_OCCUPATION"].ToString());
                resBody.LA_State = (ds2.Tables[0].Rows[0]["LA_State"].ToString());
                resBody.LA_PCODE = (ds2.Tables[0].Rows[0]["LA_PCODE"].ToString());
                resBody.APE = (ds2.Tables[0].Rows[0]["APE"].ToString());
                resBody.flag_la_not_equal_proposer = ds2.Tables[0].Rows[0]["flag_la_not_equal_proposer"].ToString();
                resBody.SumAssured = (ds2.Tables[0].Rows[0]["sumAssured"].ToString());
                resBody.PolicyID = (ds2.Tables[0].Rows[0]["PolicyID"].ToString());
                string issue = ds2.Tables[0].Rows[0]["Issue_date"].ToString();
                string issue_Date = Convert.ToDateTime(issue).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                string Login_Date = ds2.Tables[0].Rows[0]["Login_Date"].ToString();
                string myDate = Convert.ToDateTime(Login_Date).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                resBody.Login_Date = myDate;
                resBody.issue_date = issue_Date;
                resBody.Address_proof = ds2.Tables[0].Rows[0]["Address_proof"].ToString();
                resBody.Product_Code = ds2.Tables[0].Rows[0]["ProductCode"].ToString();
                resBody.Channel = ds2.Tables[0].Rows[0]["Channel"].ToString();
                resBody.Sales_manager_id = ds2.Tables[0].Rows[0]["Sales_manager_id"].ToString();
                resBody.covid_check = ds2.Tables[0].Rows[0]["covid_check"].ToString();
                resBody.policy_decline_or_postponsed = ds2.Tables[0].Rows[0]["policy_decline_or_postponsed"].ToString();
                resBody.distance_between_insurer_and_branch = ds2.Tables[0].Rows[0]["distance_between_insurer_and_branch"].ToString();
                eny_Score.RequestSource = resSoc;
                eny_Score.Requestbody = resBody;
                string Eyscore = CallENYScore(eny_Score);

                if (!string.IsNullOrEmpty(Eyscore))
                {
                    if (Eyscore == "0")
                    {
                        new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, "Api called successfully", "UWSaral", "EnyScoreDetails", "Success");
                    }
                    else
                    {
                        new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, "Api call successfully-error", "UWSaral", "EnyScoreDetails", "Success");

                    }
                }
                else
                {
                    new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, "Api call fail", "UWSaral", "EnyScoreDetails", "Fail");
                }

            }
            else
            {
                new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, "Api data not found in database ", "UWSaral", "EnyScoreDetails", "Error");

            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
            new Commfun().Insert_Logs_RiskAndEY(txtAppno.Text, ex.Message, "UWSaral", "EnyScoreDetails", "Error");

        }
    }
    private string CallENYScore(EYRiskScoreRequest ey)
    {
        try
        {
            string rs = string.Empty;
            string inputJson = string.Empty;
            string result = string.Empty;
            string Authorization = string.Empty;
            string apiUrl = ConfigurationManager.AppSettings["ENYScoreApiURL"].ToString();
            generateToken(ref Authorization);

            if (!String.IsNullOrEmpty(Authorization))
            {
                inputJson = (new JavaScriptSerializer()).Serialize(ey);
                result = SmartApi(apiUrl, "GetEYRiskScore", inputJson, Authorization, ey.Requestbody.AppNo2);
                EnyScoreResponse1 objResponseClass = JsonConvert.DeserializeObject<EnyScoreResponse1>(result);
                if (objResponseClass.ResponseResult.responseCode == "0")
                {

                    String score = objResponseClass.ResponseBody[0].score;
                    if (!string.IsNullOrEmpty(score))
                    {
                        InsertEnyVal(objResponseClass.ResponseBody[0].appno2, objResponseClass.ResponseBody[0].score, objResponseClass.ResponseBody[0].Early_claim_risk_level);
                        txtENYScore.Text = score;
                        return objResponseClass.ResponseResult.responseCode;
                    }
                    else
                    {
                        new Commfun().Insert_Logs_RiskAndEY(ey.RequestSource.DataKeyValue, "EnyRiskScore is null", "UWSaral", "CallENYScore", "Fail");
                        return objResponseClass.ResponseResult.responseCode;
                    }
                }
                else
                {
                    return objResponseClass.ResponseResult.responseCode;
                }
            }
            else
            {
                new Commfun().Insert_Logs_RiskAndEY(ey.RequestSource.DataKeyValue, "Token Not found", "UWSaral", "CallENYScore", "Fail");
            }
        }
        catch (Exception ex)
        {
            new Commfun().Insert_Logs_RiskAndEY(ey.RequestSource.DataKeyValue, ex.Message, "UWSaral", "CallENYScore", "Fail");

        }
        return "";
    }
    public void InsertEnyVal(string appno, string score, string early_claim_risk_level)
    {
        new Commfun().Insert_EnyFlag_Details(appno, score, early_claim_risk_level);
    }

    private void generateToken(ref string token)
    {
        token = string.Empty;
        try
        {
            using (var client1 = new HttpClient())
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var postData = new List<KeyValuePair<string, string>>();

                postData.Add(new KeyValuePair<string, string>("ClientID", ConfigurationManager.AppSettings["ClientID"].ToString()));
                postData.Add(new KeyValuePair<string, string>("ClientSecret", ConfigurationManager.AppSettings["ClientSecret"].ToString()));
                postData.Add(new KeyValuePair<string, string>("Source", ConfigurationManager.AppSettings["Source"].ToString()));
                postData.Add(new KeyValuePair<string, string>("PartnerID", ConfigurationManager.AppSettings["PartnerID"].ToString()));

                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client1.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["TOKENSubscriptionKey"].ToString());

                HttpContent content = new FormUrlEncodedContent(postData);

                var responseResult = client1.PostAsync(ConfigurationManager.AppSettings["TOKENURL"].ToString(), content).Result;


                string Result = string.Empty;
                if (responseResult.IsSuccessStatusCode)
                {
                    Result = responseResult.Content.ReadAsStringAsync().Result;
                    JObject json = JObject.Parse(Result);

                    token = json["access_token"].ToString();
                    //token = json["token_type"].ToString() + " " + json["access_token"].ToString();
                }
            }
        }
        catch (Exception)
        {
        }

    }
    public string SmartApi(string apiUrl, string Method, string inputJson, string Authorization, string Appno)
    {

        try
        {
            System.Net.WebClient client = new System.Net.WebClient();
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            client.Headers["Content-type"] = "application/json";
            client.Headers["Ocp-Apim-Subscription-Key"] = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"].ToString();
            client.Headers["Authorization"] = Authorization;
            client.Encoding = Encoding.UTF8;
            string Response = client.UploadString(apiUrl + Method, inputJson);

            return Response;
        }
        catch (Exception ex)
        {
            new Commfun().Insert_Logs_RiskAndEY(Appno, ex.Message, "UWSaral", "CallENYScore", "Fail");
            return null;
        }
    }



    //34.1 End of Changes; Sagar Thorave-[mfl00886]
    public void DashbordUWResultDynamic_Get(string strApplicationNumber, string strUserid, string strUserGroup, string strUWmode)
    {
        Logger.Info(strApplicationno + "STAG3:-Function Call DashbordUWResultDynamic_Get" + System.Environment.NewLine);
        objBuss.OnlineApplicationUWResultDynamicAppChecker_Get(strApplicationNumber, ref response, strUWmode, strUserid, strUserGroup);
        if (response != null)
        {
            divAppChecklist.InnerHtml = response;
        }
    }

    protected void Bindeventhandler()
    {
        Logger.Info(strApplicationno + "STAG 4 :PageName :Uwdecision.aspx.CS // MethodeName :Bindeventhandler" + System.Environment.NewLine);

        //if (chkReqDtls.Checked)
        //Master.masterCallEvent += new EventHandler(btnRequirmentDtlsSave_Click);

        ////if (chkAppDtls.Checked)
        //Master.masterCallEvent += new EventHandler(btnAppDtlsSave_Click);

        ////if (chkPanDtls.Checked)
        //Master.masterCallEvent += new EventHandler(btnPandtlsSave_Click);

        ////if (chkProductDtls.Checked)
        //Master.masterCallEvent += new EventHandler(btnProddtlsSave_Click);

        ////if (chkFunddtlsSave.Checked)
        //Master.masterCallEvent += new EventHandler(btnFundDtlsSave_Click);

        ////if (chkLoadingDtls.Checked)
        //Master.masterCallEvent += new EventHandler(btnLoadingDtlsSave_Click);


        //Master.masterCallEvent += new EventHandler(btnRiderDtlsSave_Click);

        //Master.masterCallEvent += new EventHandler(btnMandateDtlsSave_Click);

        ////if (chkBankDtls.Checked)
        //Master.masterCallEvent += new EventHandler(btnBankDtlsSave_Click);

        //if (txtUWComments.Value != "")
        // += new EventHandler(btnUWCommSave_Click);

        //// on post
        Master.masterCallEvent += new EventHandler(btnServerValidation);
        Master.masterCallEvent += new EventHandler(btnSaveToDatabase);
        Master.masterCallEvent += new EventHandler(btnClosePopupClientProfile_Click);
        //ucPrompt.checkIfExist += new uc_prompt.customHandler(MyParentMethod);
        //on save
        Master.contentCallEvent += new EventHandler(btnSaveToXml);
    }

    public void UWPreIssueServiceCall(string strApplicationNo, string strAppType)
    {
        Logger.Info(strApplicationno + "STAG3:-Function Call UWPreIssueServiceCall" + System.Environment.NewLine);
        Logger.Info(strApplicationNo + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :UWPreIssueServiceCall");
        DataSet _dsPreissue = new DataSet();
        #region PreIssue Begin.
        strLApushErrorCode = 0;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, strAppType, objChangeObj, ref _dsPreissue, ref _dsPrevPol, "PREISSUEVAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);

        if (_dsPreissue != null && _dsPreissue.Tables.Count > 0 && _dsPreissue.Tables[0].Rows.Count > 0)
        {
            decimal _TotalPremium = 0;
            decimal _AmountInsuspense = 0;
            _TotalPremium = Convert.ToDecimal(_dsPreissue.Tables[0].Rows[0]["TOATALAMOUNT"].ToString());
            _AmountInsuspense = Convert.ToDecimal(_dsPreissue.Tables[0].Rows[0]["AMOUNTINSUSPENCE"].ToString());
            divpreissueval.Visible = true;
            txtTotalPremDue.Text = Convert.ToString(_TotalPremium);
            txtAmountinsuspense.Text = Convert.ToString(_AmountInsuspense);
            txtPremServiceTax.Text = _dsPreissue.Tables[0].Rows[0]["SERVICETAX"].ToString();
            txtAmountdue.Text = Convert.ToString(_AmountInsuspense - _TotalPremium);
            txtPrebackdateintrest.Text = _dsPreissue.Tables[0].Rows[0]["BACKDATINGINTEREST"].ToString();
            DataTable dt = _dsPreissue.Tables[0];
            DataView view = new DataView(dt);
            DataTable resultTable = view.ToTable(false, "PRE ISSUE DESCRIPTION");
            //_dsPreissue.Tables[0].Columns.RemoveAt(1);
            //_dsPreissue.Tables[0].Columns.RemoveAt(1);
            //_dsPreissue.Tables[0].Columns.RemoveAt(1);
            //_dsPreissue.Tables[0].Columns.RemoveAt(1);

            gvPreissueDtls.DataSource = resultTable;
            gvPreissueDtls.DataBind();
        }
        else
        {
            divpreissueval.Visible = false;
            gvPreissueDtls.DataSource = null;
            gvPreissueDtls.DataBind();
        }

        if (strLApushErrorCode == 0)
        {
            lblPreIssueRslt.Text = "PreIssue Validation Success for Application No " + strApplicationNo;
            lblPreIssueRslt.CssClass = "LableSuccess";
        }
        if (strLApushErrorCode == 1)
        {
            lblPreIssueRslt.Text = "PreIssue Validation Failed for Application No " + strApplicationNo;
            lblPreIssueRslt.CssClass = "Lablefailure";
        }
        else if (strLApushErrorCode == -1)
        {
            lblPreIssueRslt.Text = "No Details Found For Application No " + strApplicationNo;
            lblPreIssueRslt.CssClass = "LableInfo";
        }
        if (!string.IsNullOrEmpty(txtAmountdue.Text) && Convert.ToDouble(txtAmountdue.Text) > 0)
        {
            lblPreIssueRslt.Text += "<br/>" + "Suspense amount is greater than Total Premium Due!";
        }
        //ClientScript.RegisterStartupScript(this.GetType(), "Pop", "$('#divShowMessage').modal('show');", true);
        #endregion PreIssue End.
    }

    public void UWSTPServiceCall(string strApplicationNo, string strAppType)
    {
        Logger.Info(strApplicationno + "STAG3:-Function Call UWSTPServiceCall" + System.Environment.NewLine);
        Logger.Info(strApplicationNo + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :UWSTPServiceCall" + System.Environment.NewLine);
        strLApushErrorCode = 0;
        strLApushStatus = string.Empty;
        DataSet _dsSTPResult = new DataSet();
        #region STP Begins.
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, strAppType, objChangeObj, ref _dsSTPResult, ref _dsPrevPol, "STP", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
        if (_dsSTPResult.Tables.Count > 0 && _dsSTPResult.Tables[0].Rows.Count > 0)
        {
            gvStpDetails.DataSource = _dsSTPResult;
            gvStpDetails.DataBind();
        }

        if (strLApushErrorCode == 1)
        {
            lblStpResult.Text = "STP Validation Failed for Application No " + strApplicationNo;
            lblStpResult.CssClass = "Lablefailure";
        }
        else if (strLApushErrorCode == 0)
        {
            lblStpResult.Text = "STP Validation Success for Application No " + strApplicationNo;
            lblStpResult.CssClass = "LableSuccess";
        }
        else if (strLApushErrorCode == -1 || _dsSTPResult.Tables[0].Rows.Count == 0)
        {
            lblStpResult.Text = "No Details Found For Application No " + strApplicationNo;
            lblStpResult.CssClass = "LableInfo";
        }
        //else
        //{
        //    lblStpResult.Text = "STP Validation Success for Application No " + strApplicationNo;
        //    lblStpResult.CssClass = "LableSuccess";
        //}
        #endregion STP End.
    }


    public void FillcommonObjectDetails(string strApplicationNo, string strAppType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillcommonObjectDetails" + System.Environment.NewLine);
        objComm.OnlineCommonObjectDetails_GET(ref _ds, strApplicationno, strAppType);
        BindCommonObjectDetails(_ds.Tables[0], strAppType);

    }


    public void BindCommonObjectDetails(DataTable _ds, string strAppType)
    {
        if (_ds.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindCommonObjectDetails" + System.Environment.NewLine);
            objCommonObj._ApplicationNo = _ds.Rows[0]["APPLICATIONNO"].ToString();
            objCommonObj._AppType = _ds.Rows[0]["APPTYPE"].ToString();
            objCommonObj._PolicyNo = _ds.Rows[0]["POLICYNO"].ToString();
            objCommonObj._bpm_branchCode = _ds.Rows[0]["BPMBRANCHCODE"].ToString();
            objCommonObj._bpm_businessDate = _ds.Rows[0]["BUSSDATE"].ToString();
            objCommonObj._bpm_creationDate = _ds.Rows[0]["CREATIONDATE"].ToString();
            objCommonObj._bpm_systemDate = _ds.Rows[0]["SYSTEMDATE"].ToString();
            objCommonObj._bpm_userBranch = _ds.Rows[0]["BPMUSERBRANCH"].ToString();
            objCommonObj._bpm_userID = _ds.Rows[0]["BPMUSERID"].ToString();
            objCommonObj._bpm_userName = _ds.Rows[0]["BPMUSERNAME"].ToString();
            objCommonObj._bpmgrp = _ds.Rows[0]["BPMUSERGRP"].ToString();
            objCommonObj._ProductCode = _ds.Rows[0]["PRODCODE"].ToString();
            objCommonObj._ProductType = _ds.Rows[0]["PRODTYPE"].ToString();
            objCommonObj._ProcessName = _ds.Rows[0]["PROCESSNAME"].ToString();
            objCommonObj._ChannelType = strAppType;
            Session["objCommonObj"] = objCommonObj;
            //CR - 28958 Start Changes by Shyam Patil
            string strCommonobj = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(objCommonObj);
            Session["strCommonobj"] = strCommonobj;
            //CR - 28958 End Changes by Shyam Patil
            DataSet _dsdt = new DataSet();
            objsercomm.OnlineBussinessDate_GET(ref _dsdt, objCommonObj._ApplicationNo, strAppType);


            //Start Pragati backdating

            ChangeValue objChangeValue = new ChangeValue();
            objChangeValue = (ChangeValue)Session["objLoginObj"];

            //DateTime drrr = objChangeValue.userLoginDetails._BusinessDate;
            DateTime drrr = Convert.ToDateTime(_dsdt.Tables[0].Rows[0][0]);
            string StartDate = string.Empty;
            string EndDate = string.Empty;

            EndDate = Convert.ToDateTime(drrr).ToString("dd-MM-yyyy");
            if (drrr.Month == 4)
            {
                if (drrr.Day < 30)
                {
                    StartDate = Convert.ToDateTime(drrr).AddDays(-(drrr.Day - 1)).ToString("dd-MM-yyyy");
                }
                else
                {
                    StartDate = Convert.ToDateTime(drrr).AddDays(-30).ToString("dd-MM-yyyy");
                }
            }
            else
            {
                StartDate = Convert.ToDateTime(drrr).AddDays(-30).ToString("dd-MM-yyyy");
            }
            HdnBusinessDate.Value = StartDate + "#" + EndDate;
            //End Pragati backdating
        }
    }


    public void FillPageDetails()
    {
        BindApplicationDetails(_dsAppdtls.Tables[0]);
    }

    public void FillApplicationDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillApplicationDetails" + System.Environment.NewLine);
        objComm.OnlineApplicationDisplayDetails_GET(ref _dsAppdtls, strApplicationno, ChannelType);
        BindApplicationDetails(_dsAppdtls.Tables[0]);
    }

    public void BindApplicationDetails(DataTable _dsAppdtls)
    {
        Logger.Info(strApplicationno + "STAG3:-Function Call BindApplicationDetails" + System.Environment.NewLine);
        if (_dsAppdtls.Rows.Count > 0)
        {
            string strImageUrl = string.Empty;
            txtAppno.Text = _dsAppdtls.Rows[0]["AppNo"].ToString();
            txtPolno.Text = _dsAppdtls.Rows[0]["PolNo"].ToString();
            txtAppsigndate.Text = _dsAppdtls.Rows[0]["AppSignDate"].ToString();
            txtAppchannel.Text = _dsAppdtls.Rows[0]["Channel"].ToString();
            txtPivcStatus.Text = _dsAppdtls.Rows[0]["PIVCSTATUS"].ToString();
            bool strBackDateFlag = Convert.ToBoolean(_dsAppdtls.Rows[0]["BACKDATEFLAG"]);
            bool strIsstaff = Convert.ToBoolean(_dsAppdtls.Rows[0]["IsStaff"].ToString());
            ddlAutoPaytype.SelectedValue = _dsAppdtls.Rows[0]["AUTOPAYTYPE"].ToString();
            txtBackDateIntrest.Text = Convert.ToString(_dsAppdtls.Rows[0]["BACKDATEINTREST"]);
            txtAgentChannel.Text = _dsAppdtls.Rows[0]["AgentChannel"].ToString();
            if (!string.IsNullOrEmpty(_dsAppdtls.Rows[0]["BACKDATE"].ToString()))
            {
                txtRcdreq.Text = _dsAppdtls.Rows[0]["BACKDATE"].ToString();
                txtRcdreq.CssClass = "form-control lblLable";
            }
            if (!string.IsNullOrEmpty(_dsAppdtls.Rows[0]["BACHDATEREASON"].ToString()))
            {
                ddlBkdateReason.SelectedValue = _dsAppdtls.Rows[0]["BACHDATEREASON"].ToString();
                ddlBkdateReason.CssClass = "form-control lblLable";
            }
            if (strIsstaff == true)
            {
                hd_que_2.Checked = true;
            }
            else
            {
                hd_que_2.Checked = false;
            }

            if (strBackDateFlag == true)
            {
                switch_havInsurance.Checked = true;
                //lblBackdateCaption.CssClass = lblBackdateCaption.CssClass.Replace("form-control lblLable", "form-control");

            }
            else
            {
                switch_havInsurance.Checked = false;
            }
            /*added by shri on 08 sept 17 to bind txtPivcRejectReason*/
            txtPivcRejectReason.Text = Convert.ToString(_dsAppdtls.Rows[0]["PIVCRJCREASON"]);
            /*added by shri on 23 oct 17 to add pivc status*/
            if (Convert.ToInt16(_dsAppdtls.Rows[0]["SCRH_PIVC_STATUS"]) == 1)
            {
                strImageUrl = @"../dist/img/Success.png";
                imgPivcStatus.ImageUrl = strImageUrl;
                //clsName = divDecisionDetails.Attributes["class"].ToString();
                ////divDecisionDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));                
                //divDecisionDetails.Attributes.Add("class", clsName.Replace("lblLable", string.Empty).Replace("HideControl", string.Empty));
            }
            else
            {
                strImageUrl = @"~/dist/img/Failuer.png";
                imgPivcStatus.ImageUrl = strImageUrl;
                /*
                if (ddlUWDecesion.Items.FindByValue("proposal") != null)
                {
                    ddlUWDecesion.Items.FindByValue("proposal").Selected = true;
                    ddlUWDecesion.Enabled = false;
                }
                */
                //divDecisionDetails.Attributes.Add("class", "col-md-12 HideControl");
            }
            /*added by shri on 16 nov 17*/
            cbIsNsap.Checked = Convert.ToBoolean(_dsAppdtls.Rows[0]["APP_isNSAP"]);
            /*added by shri for mandate */
            /*ID:10 START*/
            cbIsSicl.Checked = Convert.ToBoolean(_dsAppdtls.Rows[0]["APP_isSICL"]);
            /*ID:10 END*/
            /*ID:12 START*/
            txtApplicationDetailsCibil.Text = Convert.ToString(_dsAppdtls.Rows[0]["APP_Cibil"]);
            /*ID:12 END*/
            /*ID:24 START*/
            bool IsPlvcVideoUploaded = Convert.ToBoolean(_dsAppdtls.Rows[0]["IsPlvcVideoUploaded"]);
            if (IsPlvcVideoUploaded == true)
            {
                btnPlvcVideo.Visible = true;
                ChangeThumbStatus(imgPlvcVideo, true);
            }
            else
            {
                btnPlvcVideo.Visible = false;
                lblMsgPlvcVideo.Text = "Plvc video not available.";
                ChangeThumbStatus(imgPlvcVideo, false);
            }
            /*ID:24 END*/
            //Added by suraj for NAWP product
            if (!string.IsNullOrEmpty(_dsAppdtls.Rows[0]["NAWPCatagory"].ToString()))
            {
                ddlNAWPCatagory.SelectedValue = _dsAppdtls.Rows[0]["NAWPCatagory"].ToString();
                //ddlNAWPCatagory.CssClass = "form-control lblLable";
            }
            HideShowMandate();
            /*end here*/
            //added by suraj for POS flag as suggested by prafulla on 22 JULY 2020
            if (!string.IsNullOrEmpty(_dsAppdtls.Rows[0]["POSFlag"].ToString()) && _dsAppdtls.Rows[0]["POSFlag"].ToString().ToUpper() == "Y")
            {
                chkPOS.Checked = true;
            }
            else
            {
                chkPOS.Checked = false;
            }
            //end

            //added by suraj for CR - 29847 - Backdating of ULIP to be disallowed in UW stag on 19 JAN 2021
            if (_dsAppdtls.Rows[0]["ProductType"].ToString() == "UL")
            {
                hdnprodtype.Value = _dsAppdtls.Rows[0]["ProductType"].ToString();
                txtRcdreq.Text = string.Empty;
                txtRcdreq.CssClass = "form-control lblLable";

                ddlBkdateReason.SelectedValue = "0";
                ddlBkdateReason.CssClass = "form-control lblLable";

                lblBackdateCaption.Attributes.Add("class", "form-control lblLable");
                switch_havInsurance.Checked = false;
            }
            //end

            // Added by Brijesh Pandey

            if (strIsstaff == true)
            {
                divRelationwithStaff.Visible = true;
            }
            else
            {
                divRelationwithStaff.Visible = false;
                ddlRelationStaff.SelectedValue = "0";
            }
            if (!string.IsNullOrEmpty(_dsAppdtls.Rows[0]["RelationwithStaff"].ToString()))
            {
                ddlRelationStaff.SelectedValue = _dsAppdtls.Rows[0]["RelationwithStaff"].ToString();
            }
            //End
        }
    }

    public void UWFollowupServiceCall(string strApplicationNo, string strAppType)
    {
        DataSet _dsFollowup = new DataSet();
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, strAppType, objChangeObj, ref _dsFollowup, ref _dsPrevPol, "FOLLOWUP", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
    }


    public void FillAgentDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillAgentDetails" + System.Environment.NewLine);
        objComm.OnlineAgentDisplayDetails_GET(ref _dsAgentdetails, strApplicationno, ChannelType);
        BindAgentDetails(_dsAgentdetails.Tables[0]);
    }

    public void BindAgentDetails(DataTable _dsAgentdetails)
    {
        if (_dsAgentdetails.Rows.Count > 0)
        {
            string strImageUrl = string.Empty;
            Logger.Info(strApplicationno + "STAG3:-Function Call BindAgentDetails" + System.Environment.NewLine);
            txtAgentcode.Text = _dsAgentdetails.Rows[0]["AgentCode"].ToString();
            txtFgempcode.Text = _dsAgentdetails.Rows[0]["EmpCode"].ToString();
            txtPartnerempcode.Text = _dsAgentdetails.Rows[0]["PartnerCode"].ToString();
            txtcampaigncode.Text = _dsAgentdetails.Rows[0]["CampaignCode"].ToString();
            txtLeadcode.Text = _dsAgentdetails.Rows[0]["LeadCode"].ToString();
            chkAgentverified.Checked = Convert.ToBoolean(_dsAgentdetails.Rows[0]["IsAgentVerified"]);
            txtAgentName.Text = Convert.ToString(_dsAgentdetails.Rows[0]["AgentName"]);
            /*ID 33 START*/
            txtAgentvintage.Text = Convert.ToString(_dsAgentdetails.Rows[0]["AgentVintage"]);
            /*ID 33 END*/
            if (!string.IsNullOrEmpty(txtLeadcode.Text))
            {
                chkAgentLeadcode.Checked = true;

            }
            else
            {
                chkAgentLeadcode.Checked = false;
            }

            if (!string.IsNullOrEmpty(_dsAgentdetails.Rows[0]["MDRTAgent"].ToString()))
            {
                strImageUrl = @"../dist/img/Success.png";
                imagemdrt.ImageUrl = strImageUrl;
            }
            else
            {
                strImageUrl = @"~/dist/img/Failuer.png";
                imagemdrt.ImageUrl = strImageUrl;
            }


        }
    }

    public void FillBankDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillBankDetails" + System.Environment.NewLine);
        objComm.OnlineBankDisplayDetails_GET(ref _dsBank, strApplicationno, ChannelType);
        BindBankDetails(_dsBank.Tables[0]);
    }

    public void BindBankDetails(DataTable _dsBank)
    {
        if (_dsBank.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindBankDetails" + System.Environment.NewLine);
            txtBnkClientname.Text = _dsBank.Rows[0]["ClientName"].ToString();
            txtBnkClienttype.Text = _dsBank.Rows[0]["ClientType"].ToString();
            txtBnkClientnumber.Text = _dsBank.Rows[0]["ClientNumber"].ToString();
            txtBnkIfsccode.Text = _dsBank.Rows[0]["IFSCCode"].ToString();
            txtBnkBankname.Text = _dsBank.Rows[0]["BankName"].ToString();
            txtBnkBranchname.Text = _dsBank.Rows[0]["BankBranch"].ToString();
            txtBnkBankaddress.Text = _dsBank.Rows[0]["BankAddress"].ToString();
            txtBnkBankaccno.Text = _dsBank.Rows[0]["BankAccountNumber"].ToString();
        }
    }

    public void FillPanDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillPanDetails" + System.Environment.NewLine);
        objComm.OnlinePanDisplayDetails_GET(ref _dsPandtls, strApplicationno, ChannelType);
        BindPanDetails(_dsPandtls.Tables[0]);
    }

    public void BindPanDetails(DataTable _dsPandtls)
    {
        if (_dsPandtls.Rows.Count > 0)
        {
            //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

            string ND_PanStatus = string.Empty;
            string ND_PanTitle = string.Empty;
            string PanYear = string.Empty;
            string IsNSAP = string.Empty;

            //26.1 End of Changes; Bibekananda Nanda - [1103055]

            /*added by shri on 22 july 17 to check pan conditionally*/
            //chkPanCopy.Checked = true;
            /*end here*/
            Logger.Info(strApplicationno + "STAG3:-Function Call BindPanDetails" + System.Environment.NewLine);
            txtPannumber.Text = _dsPandtls.Rows[0]["PANCardNo"].ToString();
            /*added by shri on 22 july 17 to check pan conditionally*/
            if (!Convert.ToBoolean(_dsPandtls.Rows[0]["ISPANAPPLIED"]) && string.IsNullOrEmpty(txtPannumber.Text))
            {
                chkForm16Copy.Checked = true;
                //chkForm16Copy.Visible = true;
                //chkPanCopy.Visible = false;
                chkPanCopy.Checked = false;
                //divPanValidation.Visible = false;
                //divPanDetails.Style.Add("display", "none"); 
                divPanValidation.Attributes["class"] = divPanValidation.Attributes["class"] + " HideControl";
                divPanManipulate.Attributes["class"] = divPanManipulate.Attributes["class"] + " HideControl";
                /* logic changed as suggested by ganesh rajput on 12/10/2018
                chkPanCopy.Checked = true;
                //chkPanCopy.Visible = true;
                //chkForm16Copy.Visible = false;
                chkForm16Copy.Checked = false;
                //divPanDetails.Style.Add("display", "none");
                divPanValidation.Attributes["class"] = divPanValidation.Attributes["class"].Replace("HideControl", string.Empty);
                divPanManipulate.Attributes["class"] = divPanManipulate.Attributes["class"].Replace("HideControl", string.Empty);
                */
            }
            else
            {
                chkPanCopy.Checked = true;
                //chkPanCopy.Visible = true;
                //chkForm16Copy.Visible = false;
                chkForm16Copy.Checked = false;
                //divPanDetails.Style.Add("display", "none");
                divPanValidation.Attributes["class"] = divPanValidation.Attributes["class"].Replace("HideControl", string.Empty);
                divPanManipulate.Attributes["class"] = divPanManipulate.Attributes["class"].Replace("HideControl", string.Empty);
                /*
                chkForm16Copy.Checked = true;
                //chkForm16Copy.Visible = true;
                //chkPanCopy.Visible = false;
                chkPanCopy.Checked = false;
                //divPanValidation.Visible = false;
                //divPanDetails.Style.Add("display", "none"); 
                divPanValidation.Attributes["class"] = divPanValidation.Attributes["class"] + " HideControl";
                divPanManipulate.Attributes["class"] = divPanManipulate.Attributes["class"] + " HideControl";
                */
            }
            txtPanComment.Text = Convert.ToString(_dsPandtls.Rows[0]["COMMENT"]); ;
            txtPanDescription.Text = Convert.ToString(_dsPandtls.Rows[0]["RESULTDETAILS"]);
            lblPanIsValidated.Text = Convert.ToString(_dsPandtls.Rows[0]["ISMATCHED"]);

            //26.1 Begin of Changes; Bibekananda Nanda - [1103055]
            lblnsdlPanType.Text = Convert.ToString(_dsPandtls.Rows[0]["ND_PanType"]);

            lblnsdlLastUpdDt.Text = Convert.ToString(_dsPandtls.Rows[0]["ND_LastUpdDate"]);

            ND_PanStatus = Convert.ToString(_dsPandtls.Rows[0]["ND_PanStatus"]);
            ND_PanTitle = Convert.ToString(_dsPandtls.Rows[0]["ND_PanTitle"]);

            PanYear = Convert.ToString(_dsPandtls.Rows[0]["PanYear"]);
            IsNSAP = Convert.ToString(_dsPandtls.Rows[0]["IsNSAP"]);

            if (PanYear == "0" && IsNSAP == "false")
            {
                lblPanMsgProp.Visible = true;
                lblPanMsg.Visible = true;
                lblPanMsg.Text = "PAN card issued within 1 year, please process with due diligence";
            }
            else
            {
                lblPanMsgProp.Visible = false;
                lblPanMsg.Visible = false;
                lblPanMsg.Text = "";
            }
            //26.1 End of Changes; Bibekananda Nanda - [1103055]

            if (lblPanIsValidated.Text.Contains("Manual") || lblPanIsValidated.Text.Contains("Auto Matched"))
            {
                chkIsPanValidate.Checked = true;
                ChangeThumbStatus(imgPanVerified, true);
            }
            else
            {
                chkIsPanValidate.Checked = false;
                ChangeThumbStatus(imgPanVerified, false);
            }
            /*end here*/
        }
    }

    public void FillTsmrMsarDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG3:-Function Call FillTsmrMsarDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillTsmrMsarDetails");
        DataSet _dsMsarTsar = new DataSet();
        strLApushErrorCode = 0;
        strLApushStatus = string.Empty;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        objComm.OnlineTsmrMsarDisplayDetails_GET(ref _dsTsarMsarDtls, strApplicationno, strChannelType);
        if (_dsTsarMsarDtls != null && _dsTsarMsarDtls.Tables.Count > 0 && _dsTsarMsarDtls.Tables[0].Rows.Count > 0)
        {
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, ChannelType, objChangeObj, ref _dsMsarTsar, ref _dsPrevPol, "MSAR", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (_dsMsarTsar != null && _dsMsarTsar.Tables.Count > 0)
            {
                SaveTsarMsarDetails(_dsMsarTsar.Tables[0]);
                BindTsarMsarDetails(_dsMsarTsar.Tables[0]);
            }
            if (_dsPrevPol != null && _dsPrevPol.Tables.Count > 0)
            {
                BindPreviousPolictDetails(_dsPrevPol.Tables[0]);
            }
            //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, ChannelType, objChangeObj, ref _dsMsarTsar, ref _dsPrevPol, "MSARSARAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            //BindTsarMsarDetailsSaral(_dsMsarTsar.Tables[0]);
            //BindPreviousPolictDetailsSaral(_dsPrevPol.Tables[0]);
        }
    }
    public void SaveTsarMsarDetails(DataTable _dtTsarMsar)
    {
        int resp = 0;
        objBuss.Save_TsarMsar(_dtTsarMsar, ref resp);
    }
    public void BindPreviousPolictDetails(DataTable _dsPrev)
    {
        if (_dsPrev.Rows.Count > 0)
        {
            _dsPrev.Columns.Remove("stat");
            _dsPrev.Columns.Remove("ageProofSubmitted");
            _dsPrev.Columns.Remove("idProofSubmitted");
            _dsPrev.Columns.Remove("addressProofSubmitted");
            _dsPrev.Columns.Remove("incomeProofSubmitted");
            _dsPrev.Columns.Remove("underwriterComments");

            divPrePolicy.Visible = true;
            gridPrevPol.DataSource = _dsPrev;
            gridPrevPol.DataBind();
        }
        else
        {
            divPrePolicy.Visible = false;
            lblErrorPrevpol.Text = "No Previous Policy Details found";
        }
    }
    public void BindTsarMsarDetails(DataTable _dsTsarMsarDtls)
    {
        if (_dsTsarMsarDtls.Rows.Count > 0)
        {
            GridMsarTsar.DataSource = _dsTsarMsarDtls;
            GridMsarTsar.DataBind();
        }

    }

    //38.1 Begin of Changes; Bhaumik Patel - [CR-4134]
    public void FillRedFlagDetails(String strApplicationno)
    {
        chkredflag.Checked = false;
        DataSet _dsRedFlagDtls = new DataSet();
        string Mode;
        Mode = "UWDecisionRedFlagGrid";
        objComm.OnlineRedFlagtDetails_GET(ref _dsRedFlagDtls, strApplicationno, Mode);
        BindRedFlagDetails(_dsRedFlagDtls.Tables[0]);

    }

    public void BindRedFlagDetails(DataTable _dsRedFlagDtls)
    {
        if (_dsRedFlagDtls.Rows.Count > 0)
        {

            GridRedFlag.DataSource = _dsRedFlagDtls;
            GridRedFlag.DataBind();
        }

    }
    //38.1 End of Changes; Bhaumik Patel - [CR-4134]

    public void FillProductDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillProductDetails" + System.Environment.NewLine);
        objComm.OnlineProductDisplayDetails_GET(ref _dsProdDtls, strApplicationno, ChannelType);
        BindProductDetails(_dsProdDtls.Tables[0]);
        //BindRiderDetails(_dsProdDtls.Tables[1]);
    }

    public void BindProductDetails(DataTable _dsProdDtls)
    {
        string ProductName = string.Empty;
        if (_dsProdDtls.Rows.Count > 0)
        {
            //Bhaumik Patel Cr-5703
            ProdDtls objprodchangevalue = new ProdDtls();
            //Bhaumik Patel Cr-5703
            double strServiceTax = Convert.ToDouble(ConfigurationManager.AppSettings["ServiceTax"]);
            double strBasePremium = Math.Round(Convert.ToInt32(_dsProdDtls.Rows[0]["BasePremium"].ToString()) * strServiceTax) + Convert.ToInt32(_dsProdDtls.Rows[0]["BasePremium"].ToString());
            Logger.Info(strApplicationno + "STAG3:-Function Call BindProductDetails" + System.Environment.NewLine);
            hdnProductType.Value = _dsProdDtls.Rows[0]["ProductType"].ToString();
            txtProdcode.Text = _dsProdDtls.Rows[0]["ProductCode"].ToString();
            txtProname.Text = _dsProdDtls.Rows[0]["ProdcutName"].ToString();
            txtBasepolno.Text = _dsProdDtls.Rows[0]["PolicyNo"].ToString();
            txtPolterm.Text = _dsProdDtls.Rows[0]["PolicyTerm"].ToString();
            txtPrepayterm.Text = _dsProdDtls.Rows[0]["PremiumTerm"].ToString();
            //Bhaumik Patel Cr-5703
            objprodchangevalue._Sumassured = txtSumassure.Text = _dsProdDtls.Rows[0]["SumAssured"].ToString();
            //Bhaumik Patel Cr-5703
            ddlFrequency.SelectedValue = _dsProdDtls.Rows[0]["PremiumFreq"].ToString();
            //1.1 Begin of Changes CR-28153
            hdfSumassure.Value = _dsProdDtls.Rows[0]["SumAssured"].ToString();
            hfdCalPremSA.Value = _dsProdDtls.Rows[0]["SumAssured"].ToString();
            //1.1 End of Changes CR-28153
            // txtSisamount.Text = _dsProdDtls.Rows[0]["AmountInSIS"].ToString();
            txtBasepremium.Text = _dsProdDtls.Rows[0]["BasePremium"].ToString();
            txtServicetax.Text = _dsProdDtls.Rows[0]["ServiceTax"].ToString();
            txtTotalpremium.Text = _dsProdDtls.Rows[0]["TotalPremium"].ToString();
            txtMonthlyPayoutBase.Text = (string.IsNullOrEmpty(Convert.ToString(_dsProdDtls.Rows[0]["MonthlyPayout"]))) ? "0" : _dsProdDtls.Rows[0]["MonthlyPayout"].ToString();
            /*ID:8 START*/
            txtProdBranchBasePremium.Text = Convert.ToString(_dsProdDtls.Rows[0]["BranchBasePremium"]);

            //added by suraj for new product code T36/37 & E91/92
            //Bhaumik Patel Cr-5703
            Session["objprodchangevalue"] = objprodchangevalue;
            //Bhaumik Patel Cr-5703
            if (txtProdcode.Text == "T36" || txtProdcode.Text == "T37" || txtProdcode.Text == "T38")
            {
                txtLumpSumPercent.Text = Convert.ToString(_dsProdDtls.Rows[0]["LumpSumPercent"]);
                ddlCategory.SelectedValue = _dsProdDtls.Rows[0]["Category"].ToString();
                ddlPayoutType.SelectedValue = _dsProdDtls.Rows[0]["PayoutType"].ToString();
                ddlPayoutTerm.SelectedValue = _dsProdDtls.Rows[0]["PayoutTerm"].ToString();
            }
            if (txtProdcode.Text == "E91" || txtProdcode.Text == "E92" || txtProdcode.Text == "E97" || txtProdcode.Text == "E98")
            {
                ddlCategory.SelectedValue = _dsProdDtls.Rows[0]["Category"].ToString();
                ddlpayoutfreq.SelectedValue = _dsProdDtls.Rows[0]["PayOutFrquency"].ToString();
                if (txtProdcode.Text == "E97" || txtProdcode.Text == "E98")
                {
                    ddlPayoutType.SelectedValue = _dsProdDtls.Rows[0]["PayoutType"].ToString();
                }
            }
            if (txtProdcode.Text == "E93" || txtProdcode.Text == "E94")
            {
                ddlCategory.SelectedValue = _dsProdDtls.Rows[0]["Category"].ToString();
                ddlprodcategory.SelectedValue = _dsProdDtls.Rows[0]["ProductCategory"].ToString();
            }

            //end
            ddlPayoutType_SelectedIndexChanged(null, null);

            IsBasePremiumDifferentChecker();
            /*ID:8 END*/
            //Added by suraj on 02JULY2020 for NAWP catagory
            if (txtProdcode.Text == "E83" || txtProdcode.Text == "E84")
            {
                divNAWPCAT.Visible = true;
            }
            else
            {
                divNAWPCAT.Visible = false;
            }
            /*ID:10 START*/
            if (_dsProdDtls.Columns.Contains("SpecialInsuranceType"))
            {
                if (ddlApplicationDetailsProposalType.Items.FindByValue(Convert.ToString(_dsProdDtls.Rows[0]["SpecialInsuranceType"])) != null)
                {
                    ddlApplicationDetailsProposalType.ClearSelection();
                    ddlApplicationDetailsProposalType.Items.FindByValue(Convert.ToString(_dsProdDtls.Rows[0]["SpecialInsuranceType"])).Selected = true;
                    //added by suraj on 10 MAY 2018 for disable assign type dropdown
                    if (ddlApplicationDetailsProposalType.SelectedValue.Equals("EMP"))
                    {
                        ddlAssigmentType.Attributes["class"] = ddlAssigmentType.Attributes["class"].ToString().Replace("lblLable", "");
                    }
                    if (ddlApplicationDetailsProposalType.SelectedValue.Equals("NRI"))
                    {
                        ddlcountry.Attributes["class"] = ddlcountry.Attributes["class"].ToString().Replace("lblLable", "");
                    }
                }
            }
            /*ID:10 END*/
            //Added by Suraj on 02 JAN 2020 for signature matched value to be bind
            if (_dsProdDtls.Columns.Contains("SignatureMatched"))
            {
                if (ddlSignature.Items.FindByText(Convert.ToString(_dsProdDtls.Rows[0]["SignatureMatched"])) != null)
                {
                    ddlSignature.ClearSelection();
                    ddlSignature.Items.FindByText(Convert.ToString(_dsProdDtls.Rows[0]["SignatureMatched"])).Selected = true;
                }
            }
            if (_dsProdDtls.Rows.Count > 1)
            {
                clsName = divcomboprod.Attributes["class"].ToString();
                divcomboprod.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                txtCombProdCode.Text = _dsProdDtls.Rows[1]["ProductCode"].ToString();
                txtCombopolno.Text = _dsProdDtls.Rows[1]["PolicyNo"].ToString();
                txtComboMonthlyPayout.Text = _dsProdDtls.Rows[1]["MonthlyPayout"].ToString();
                txtCombProdName.Text = _dsProdDtls.Rows[1]["ProdcutName"].ToString();
                ddlComboFrequency.SelectedValue = _dsProdDtls.Rows[1]["PremiumFreq"].ToString();
                txtCombProdCode.Text = _dsProdDtls.Rows[1]["ProductCode"].ToString();
                txtCombPolTerm.Text = _dsProdDtls.Rows[1]["PolicyTerm"].ToString();
                txtCombPayTerm.Text = _dsProdDtls.Rows[1]["PremiumTerm"].ToString();
                txtCombSumAssured.Text = _dsProdDtls.Rows[1]["SumAssured"].ToString();
                txtCombServiceTax.Text = _dsProdDtls.Rows[1]["ServiceTax"].ToString();
                txtCombTotalPrem.Text = _dsProdDtls.Rows[1]["TotalPremium"].ToString();
                txtCombPremAmount.Text = _dsProdDtls.Rows[1]["BasePremium"].ToString();
            }

            foreach (DataRow dr in _dsProdDtls.Rows)
            {
                ProductName += dr["ProdcutName"].ToString() + " + ";
            }
            ProductName = ProductName.Remove(ProductName.Length - 2);
            updProductDetails.Update();
        }
        ((Label)Master.FindControl("lblProductName")).Text = ProductName;
        /*ID:10 start*/
        if (IsUserLimitLessThanSumAssured())
        {
            RemoveWarningMessage("1");
            FillWarningMessage("1");
            DisplaySaralWarningMessage();
        }
        /*ID:10 end*/
    }

    public void BindRiderDetails(DataTable _dsProdDtls)
    {
        DataTable dt = _dsProdDtls.Clone();
        DataTable dtCloned = dt.Clone();
        dt.Columns["IsActive"].DataType = typeof(bool);
        DataSet _ds = new DataSet();
        objComm.GetMandatoryRider(ref _ds, txtProdcode.Text);
        ViewState["MandatoryRider"] = _ds.Tables[0];
        foreach (DataRow row in _dsProdDtls.Rows)
        {
            dt.ImportRow(row);
        }

        _dsProdDtls.Columns.RemoveAt(0);
        if (_dsProdDtls.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindRiderDetails" + System.Environment.NewLine);
            ViewState["Rider"] = _dsProdDtls;
            div6.Attributes.Add("class", "col-md-12");
            gvRiderDtls.DataSource = dt;
            gvRiderDtls.DataBind();
        }
        else
        {
            div6.Attributes.Add("class", "col-md-12 HideControl");
        }
        DataView dtactiverider = new DataView(dt);
        dtactiverider.RowFilter = "IsActive = True";
        //_dsProdDtls = dtactiverider.ToTable();
        /*
        foreach (GridViewRow gvrow in gvRiderDtls.Rows)
        {
            CheckBox cbIsActive = (CheckBox)gvrow.FindControl("chkremoveRider");
            if (dtactiverider != null && dtactiverider.Count > 0 && _ds !=null && _ds.Tables.Count>0 && _ds.Tables[0].Rows.Count>0 )
            {
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    //if (dt.Rows[i]["RTABLE"].ToString() == gvrow.Cells[2].Text)
                    //{
                    //for (int j = 0; j < dtactiverider.Count; j++)
                    //{
                        if (Convert.ToString(_ds.Tables[0].Rows[i]["RTABLE"]) == Convert.ToString(dtactiverider[i]["RIDERCODE"]))
                            cbIsActive.Enabled = false;
                    //}
                    
                    //}
                }
            }
        }   
        */
    }

    public void FillRiderDetails(string strApplicationno)
    {
        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillRiderDetails" + System.Environment.NewLine);
        objComm.OnlineRiderDisplayDetails_GET(ref _dsRiderDtls, strApplicationno);
    }

    public void FillfundsDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillfundsDetails" + System.Environment.NewLine);
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objComm.OnlineFundsDisplayDetails_GET(ref _dsFundsDtls, strApplicationno, objCommonObj._ProductCode, ChannelType);
        BindFundDetails(_dsFundsDtls.Tables[0]);
    }

    public void BindFundDetails(DataTable _dsFundsDtls)
    {
        _dsFundsDtls.Columns.RemoveAt(0);
        if (_dsFundsDtls.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindFundDetails" + System.Environment.NewLine);
            divFunddetails.Attributes.Add("Class", "col-md-12");
            gvFundDtls.DataSource = _dsFundsDtls;
            gvFundDtls.DataBind();
        }
        else
        {
            divFunddetails.Attributes.Add("Class", "col-md-12 HideControl");
        }
    }

    public void FillCommentsDetails(string strApplicationno, string ChannelType, string strCommenttype)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillCommentsDetails" + System.Environment.NewLine);
        objComm.OnlineCommentsDisplayDetails_GET(ref _dscommentDtls, strApplicationno, ChannelType, strCommenttype);
        BindCommentsDetails(_dscommentDtls.Tables[0]);
        clsName = divExistingComments.Attributes["class"].ToString();
        divExistingComments.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
    }

    public void BindCommentsDetails(DataTable _dscommentDtls)
    {
        _dscommentDtls.Columns.RemoveAt(0);
        if (_dscommentDtls.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindCommentsDetails" + System.Environment.NewLine);
            gvUWComments.DataSource = _dscommentDtls;
            gvUWComments.DataBind();
        }
    }

    public void FillPaymentsDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillPaymentsDetails" + System.Environment.NewLine);
        objComm.OnlinePaymentsDisplayDetails_GET(ref _dsPymentsDtls, strApplicationno, ChannelType);

    }


    public void FillRequirmentDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillRequirmentDetails" + System.Environment.NewLine);
        objComm.OnlineRequirmentDisplayDetails_GET(ref _dsRequrmentDtls, strApplicationno, ChannelType);
        BindRequirmentDetails(_dsRequrmentDtls.Tables[0]);
    }

    public void BindRequirmentDetails(DataTable _dtRequrmentDtls)
    {
        _dtRequrmentDtls.Columns.RemoveAt(0);
        if (_dtRequrmentDtls.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindRequirmentDetails" + System.Environment.NewLine);
            ViewState["CurrentTable1"] = _dtRequrmentDtls.Copy();
            //ViewState["CurrentTable1"] = _dtRequrmentDtls;
            Session["ReqCount"] = _dtRequrmentDtls.Rows.Count;
            gvRequirmentDetails.DataSource = _dtRequrmentDtls;
            gvRequirmentDetails.DataBind();

        }
    }


    public void FillReceiptDetails(string strApplicationno, string strReqSource)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillReceiptDetails" + System.Environment.NewLine);
        objComm.OnlineReceiptDisplayDetails_GET(ref _dsReceiptDtls, strApplicationno, strReqSource);
        BindReciptDetails(_dsReceiptDtls.Tables[0]);
    }

    public void BindReciptDetails(DataTable _dsReceiptDtls)
    {
        _dsReceiptDtls.Columns.RemoveAt(0);
        if (_dsReceiptDtls.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindReciptDetails" + System.Environment.NewLine);
            gvRcptDtls.DataSource = _dsReceiptDtls;
            gvRcptDtls.DataBind();
            //txtInstalamentAmt.Text = _dsReceiptDtls.Tables[1].Rows[0]["INSTALLMENTPREMIUM"].ToString();
            //txtTotalamtPaid.Text = _dsReceiptDtls.Tables[1].Rows[0]["TOTALAMOUNT"].ToString();
            //txtAmountRequired.Text = _dsReceiptDtls.Tables[1].Rows[0]["AMOUNTREQUIRED"].ToString();

        }
    }

    public void FillLoadingDetails(string strApplicationno, string strReqSource)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillLoadingDetails" + System.Environment.NewLine);
        objComm.OnlineLoadingDisplayDetails_GET(ref _dsLoadingDtls, strApplicationno, strReqSource);
        BindLoadingDetails(_dsLoadingDtls.Tables[0]);
    }

    public void BindLoadingDetails(DataTable _dsLoadingDtls)
    {
        //_dsLoadingDtls.Columns.RemoveAt(0);
        if (_dsLoadingDtls.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindLoadingDetails" + System.Environment.NewLine);
            //divExistingloading.Attributes.Remove("Class");
            //divExistingloading.Attributes.Add()
            lblErrorExistingLoading.Text = "";
            clsName = divExistingloading.Attributes["class"].ToString();
            divExistingloading.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
            ViewState["ExitingLoadingData"] = _dsLoadingDtls.Copy();
            gvExtLoadDetails.DataSource = _dsLoadingDtls;
            gvExtLoadDetails.DataBind();
        }
        else
        {
            //divExistingloading.Attributes.Add("Class", "HideControl");
            gvExtLoadDetails.DataSource = null;
            gvExtLoadDetails.DataBind();
        }
    }

    public void FillReportDetails(string strApplicationno)
    {
        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillReportDetails" + System.Environment.NewLine);
        objComm.OnlineReportDisplayDetails_GET(ref _dsReportDtls, strApplicationno);
    }

    public void FillSubMasterDetails(ref DataSet _dsFollowuo, string strDatavalue, string ddlType, string ddlsubtype)
    {
        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillSubMasterDetails" + System.Environment.NewLine);
        objComm.OnlineSubMasterDisplayDetails_GET(ref _dsFollowuo, strDatavalue, ddlType, ddlsubtype);
    }

    //44.1 Begin of Changes; Bhaumik Patel - [CR-3334]
    public void FillSubMasterDetailsCategory(ref DataSet _dsFollowuo, string strDatavalue, string category, string ddlType, string ddlsubtype)
    {
        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillSubMasterDetails" + System.Environment.NewLine);
        objComm.OnlineSubMasterDisplay_DecisionDetails_GET(ref _dsFollowuo, strDatavalue, category, ddlType, ddlsubtype);
    }
    //44.1 Begin of Changes; Bhaumik Patel - [CR-3334]

    public void FillNsaploadingDetails(string strApplicationno, string ChannelType)
    {
        objcomm.OnlineNsapProductDetails_GET(ref _dsProdontrol, strApplicationno, strChannelType);
        AddLoadingDetails(_dsProdontrol);
    }
    public void AddLoadingDetails(DataSet _dS)
    {
        if (_dS != null && _dS.Tables[0].Rows.Count > 0)
        {
            lblErrorloadingdtls.Text = string.Empty;
            int LoadingResult = 0;

            objChangeObj = (ChangeValue)Session["objLoginObj"];
            string _strLoadRiderName = string.Empty;
            string _strLoadType = string.Empty;
            string _strLoadDiscp = string.Empty;
            string _strLoadReason1 = string.Empty;
            string _strLoadReason2 = string.Empty;
            string _strLoadReason3 = string.Empty;
            string _strLoadReason4 = string.Empty;
            string _strLoadPercent = string.Empty;
            string _strRateAdjust = string.Empty;
            string _strFlatmortality = string.Empty;
            string _strLetterPrint = string.Empty;
            string _strRiderCode = string.Empty;
            string _strLoadCode = string.Empty;
            string _strReason1code = string.Empty;
            string _strReason2code = string.Empty;
            string _strSumAssured = string.Empty;
            string _strPremiumPayingterm = string.Empty;
            _strLoadCode = _dS.Tables[0].Rows[0]["LD_code"].ToString();
            _strLoadDiscp = _dS.Tables[0].Rows[0]["LD_loadingType"].ToString();

            //44.1 Begin of Changes; Bhaumik Patel - [CR-3334]
            string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];
            //_strLoadReason1 = _dS.Tables[0].Rows[0]["LD_reason1"].ToString();
            //_strLoadReason2 = _dS.Tables[0].Rows[0]["LD_reason2"].ToString();
            //_strLoadReason3 = _dS.Tables[0].Rows[0]["LD_reason2"].ToString();
            //_strLoadReason4 = _dS.Tables[0].Rows[0]["LD_reason4"].ToString();
            if (!string.IsNullOrEmpty(loadrsncode))
            {

                for (int k = 0; k < loadrsncode.Split(',').Length; k++)
                {
                    if (k == 0)
                    {
                        _strLoadReason1 = loadrsncode.Split(',')[k];
                    }


                    if (k == 1)
                    {
                        _strLoadReason2 = loadrsncode.Split(',')[k];
                    }

                    if (k == 2)
                    {
                        _strLoadReason3 = loadrsncode.Split(',')[k];
                    }


                    if (k == 3)
                    {
                        _strLoadReason4 = loadrsncode.Split(',')[k];
                    }
                }
            }
            else
            {
                loadrsncode = "";
            }
            //44.1 End of Changes; Bhaumik Patel - [CR-3334]
            _strSumAssured = _dS.Tables[0].Rows[0]["LD_sumAssured"].ToString();
            _strRateAdjust = _dS.Tables[0].Rows[0]["LD_rateAdjustment"].ToString();
            _strFlatmortality = _dS.Tables[0].Rows[0]["LD_flatMortality"].ToString();
            _strPremiumPayingterm = _dS.Tables[0].Rows[0]["LD_premiumPayingTerm"].ToString();
            _strLetterPrint = _dS.Tables[0].Rows[0]["LD_isLetterPrintRequired"].ToString();
            _strLoadRiderName = _dS.Tables[0].Rows[0]["LD_rider"].ToString();
            LoadDtls objLoad = new LoadDtls();
            objLoad._strProdcode = _dS.Tables[0].Rows[0]["LD_rider"].ToString();
            objChangeObj.Load_Loadingdetails = objLoad;
            //44.1 Begin of Changes; Bhaumik Patel - [CR-3334]
            objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, _strLoadCode, _strLoadDiscp, _strLoadReason1, _strLoadReason2, _strLoadReason3, _strSumAssured, txtLoadPer.Text, _strRateAdjust, _strFlatmortality, _strPremiumPayingterm, _strLetterPrint, _strLoadRiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, _strLoadReason4, ref LoadingResult);
            //44.1 End of Changes; Bhaumik Patel - [CR-3334]
            if (LoadingResult != -1)
            {
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, "RATEUP", ref strLApushErrorCode, ref strLApushStatus);
                if (strLApushErrorCode == 0)
                {
                }
            }
        }
    }

    public void FillProductControlDetails(string strApplicationno, string ChannelType)
    {
        objcomm.OnlineProdcutControlMasterDetails_GET(ref _dsProdontrol, strApplicationno, strChannelType);
        BindProductControlDetails(_dsProdontrol);
    }
    //Added by Suraj Bhamre on 20/11/2020 -- drop down for new product t36/t37/e91/e92
    public bool Get_DropDownData(DropDownList ddl, string Mode, string ProductID, string ProductCode, string dataValue, string dataText)
    {
        string ConStr;
        SqlConnection Con = null;
        SqlDataAdapter da = null;
        bool result = false;
        try
        {
            ConStr = ConfigurationManager.AppSettings["transactiondbLF"].ToString();
            Con = new SqlConnection(ConStr);
            DataSet ds = new DataSet();
            // Con.Open();
            SqlCommand comm = new SqlCommand("USP_GetDropdowndata", Con);
            da = new SqlDataAdapter(comm);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Mode", Mode);
            da.SelectCommand.Parameters.AddWithValue("@ProductID", ProductID);
            da.SelectCommand.Parameters.AddWithValue("@ProductCode", ProductCode);
            da.Fill(ds);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl.Items.Clear();
                        ddl.AppendDataBoundItems = true;
                        ddl.Items.Insert(0, new ListItem("--Select--"));
                        ddl.DataSource = ds.Tables[0];
                        ddl.DataTextField = dataText;
                        ddl.DataValueField = dataValue;
                        ddl.DataBind();
                        result = true;
                    }
                }
            }

            return result;
        }
        catch (Exception)
        {
            //SISExceptionLog("", "", "Get_DropDownData()", ex.ToString());
            return result;
        }
        finally
        {
            if (Con.State == ConnectionState.Open)
            {
                Con.Close();
            }

            if (da != null)
            {
                da.Dispose();
            }
        }
    }
    protected void ddlPayoutType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ViewState["ProductCode"].ToString() != "E97" && ViewState["ProductCode"].ToString() != "E98")
        {
            if (ddlPayoutType.SelectedValue != "--Select--" && ddlPayoutType.SelectedValue != "")
            {


                if (ddlPayoutType.SelectedValue == "lumpsum")
                {
                    divLumpSumPercent.Visible = true;
                    divPayoutTerm.Visible = false;
                    //Get_DropDownData(ddlPayoutTerm, "payoutterm", "", ViewState["ProductCode"].ToString(), "Value", "Text");
                }
                else
                {
                    if (ddlPayoutType.SelectedValue == "mixed")
                    {
                        divLumpSumPercent.Visible = true;
                        divPayoutTerm.Visible = true;
                    }
                    divPayoutTerm.Visible = true;
                    //divLumpSumPercent.Visible = false;
                    Get_DropDownData(ddlPayoutTerm, "payoutterm", "", ViewState["ProductCode"].ToString(), "Value", "Text");
                }
            }
        }
    }
    //end
    public void BindProductControlDetails(DataSet _ds)
    {

        if (_ds != null && _ds.Tables.Count > 0)
        {
            DataTable _dt = _ds.Tables[0];
            if (_dt.Rows.Count > 0)
            {
                txtPolterm.Enabled = (!string.IsNullOrEmpty(_dt.Rows[0]["PT"].ToString())) ? Convert.ToBoolean(_dt.Rows[0]["PT"]) : false;
                txtPrepayterm.Enabled = (!string.IsNullOrEmpty(_dt.Rows[0]["PPT"].ToString())) ? Convert.ToBoolean(_dt.Rows[0]["PPT"]) : false;
                txtBasepremium.Enabled = (!string.IsNullOrEmpty(_dt.Rows[0]["BasePremium"].ToString())) ? Convert.ToBoolean(_dt.Rows[0]["BasePremium"]) : false;
                txtSumassure.Enabled = (!string.IsNullOrEmpty(_dt.Rows[0]["SumAssure"].ToString())) ? Convert.ToBoolean(_dt.Rows[0]["SumAssure"]) : false;
                ddlFrequency.Enabled = (!string.IsNullOrEmpty(_dt.Rows[0]["Frequency"].ToString())) ? Convert.ToBoolean(_dt.Rows[0]["Frequency"]) : false;
                txtMonthlyPayoutBase.Visible = lblMonthlyPayout.Visible = (!string.IsNullOrEmpty(_dt.Rows[0]["MonthlyPayout"].ToString())) ? Convert.ToBoolean(_dt.Rows[0]["MonthlyPayout"]) : false;
                txtMonthlyPayoutBase.Enabled = (!string.IsNullOrEmpty(_dt.Rows[0]["MonthlyPayout"].ToString())) ? Convert.ToBoolean(_dt.Rows[0]["MonthlyPayout"]) : false;
            }
            else
            {
                txtPolterm.Enabled = true;
                txtPrepayterm.Enabled = true;
                txtBasepremium.Enabled = false;
                txtSumassure.Enabled = true;
                ddlFrequency.Enabled = true;
                txtMonthlyPayoutBase.Enabled = false;
            }
            if (_dt.Rows.Count > 1)
            {
                txtCombPolTerm.Enabled = (!string.IsNullOrEmpty(_dt.Rows[1]["PT"].ToString())) ? Convert.ToBoolean(_dt.Rows[1]["PT"]) : false;
                txtCombPayTerm.Enabled = (!string.IsNullOrEmpty(_dt.Rows[1]["PPT"].ToString())) ? Convert.ToBoolean(_dt.Rows[1]["PPT"]) : false;
                txtCombPremAmount.Enabled = (!string.IsNullOrEmpty(_dt.Rows[1]["BasePremium"].ToString())) ? Convert.ToBoolean(_dt.Rows[1]["BasePremium"]) : false;
                txtCombSumAssured.Enabled = (!string.IsNullOrEmpty(_dt.Rows[1]["SumAssure"].ToString())) ? Convert.ToBoolean(_dt.Rows[1]["SumAssure"]) : false;
                ddlComboFrequency.Enabled = (!string.IsNullOrEmpty(_dt.Rows[1]["Frequency"].ToString())) ? Convert.ToBoolean(_dt.Rows[1]["Frequency"]) : false;
                txtComboMonthlyPayout.Enabled = (!string.IsNullOrEmpty(_dt.Rows[1]["MonthlyPayout"].ToString())) ? Convert.ToBoolean(_dt.Rows[1]["MonthlyPayout"]) : false;
                txtComboMonthlyPayout.Visible = lblMonthlyPayoutCombo.Visible = (!string.IsNullOrEmpty(_dt.Rows[1]["MonthlyPayout"].ToString())) ? Convert.ToBoolean(_dt.Rows[1]["MonthlyPayout"]) : false;
            }

            //Added by Suraj Bhamre on 20/11/2020 -- drop down for new product t36/t37/e91/e92
            if (_ds != null && _ds.Tables.Count > 1 && _ds.Tables[1].Rows.Count > 0)
            {
                ViewState["ProductCode"] = _ds.Tables[1].Rows[0]["PROD_productCode_CHDRTYPE"].ToString();

                //for product code T36/T37/T38
                if (Get_DropDownData(ddlCategory, "category", "", ViewState["ProductCode"].ToString(), "Value", "Text"))
                {
                    divCategory.Visible = true;
                }
                else
                {
                    divCategory.Visible = false;
                }
                if (Get_DropDownData(ddlPayoutType, "payouttype", "", ViewState["ProductCode"].ToString(), "Value", "Text"))
                {
                    if (ViewState["ProductCode"].ToString() == "E97" || ViewState["ProductCode"].ToString() == "E98")
                    {
                        lblPayoutType.InnerText = "Income Option";
                    }
                    divPayoutType.Visible = true;
                }
                else
                {
                    divPayoutType.Visible = false;
                }
                if (Get_DropDownData(ddlPayoutTerm, "payoutterm", "", ViewState["ProductCode"].ToString(), "Value", "Text"))
                {
                    divPayoutTerm.Visible = true;
                }
                else
                {
                    divPayoutTerm.Visible = false;
                }

                //for product code E91/E92
                if (Get_DropDownData(ddlpayoutfreq, "payoutfreq", "", ViewState["ProductCode"].ToString(), "Value", "Text"))
                {
                    divpayoutfreq.Visible = true;
                }
                else
                {
                    divpayoutfreq.Visible = false;
                }
                //for product code E93/E94
                if (Get_DropDownData(ddlprodcategory, "productcategory", "", ViewState["ProductCode"].ToString(), "Value", "Text"))
                {
                    divprodcategory.Visible = true;
                }
                else
                {
                    divprodcategory.Visible = false;
                }
            }
            //end
        }


        //SetProductControlVisibility();
    }

    //public void SetProductControlVisibility()
    //{
    //    txtPolterm.Enabled = (!string.IsNullOrEmpty(hdnBasePP.Value)) ? Convert.ToBoolean(hdnBasePP.Value) : false;
    //    txtPrepayterm.Enabled = (!string.IsNullOrEmpty(hdnBasePPT.Value)) ? Convert.ToBoolean(hdnBasePPT.Value) : false;
    //    txtBasepremium.Enabled = (!string.IsNullOrEmpty(hdnBasePremium.Value)) ? Convert.ToBoolean(hdnBasePremium.Value) : false;
    //    txtSumassure.Enabled = (!string.IsNullOrEmpty(hdnBaseSumassured.Value)) ? Convert.ToBoolean(hdnBaseSumassured.Value) : false;
    //    ddlFrequency.Enabled = (!string.IsNullOrEmpty(hdnBaseFrequency.Value)) ? Convert.ToBoolean(hdnBaseFrequency.Value) : false;
    //    txtMonthlyPayoutBase.Enabled = (!string.IsNullOrEmpty(hdnBaseMonthlypayout.Value)) ? Convert.ToBoolean(hdnBaseMonthlypayout.Value) : false;

    //    txtCombPolTerm.Enabled = (!string.IsNullOrEmpty(hdnComboPP.Value)) ? Convert.ToBoolean(hdnComboPP.Value) : false;
    //    txtCombPayTerm.Enabled = (!string.IsNullOrEmpty(hdnComboPPT.Value)) ? Convert.ToBoolean(hdnComboPPT.Value) : false;
    //    txtCombPremAmount.Enabled = (!string.IsNullOrEmpty(hdnComboPremium.Value)) ? Convert.ToBoolean(hdnComboPremium.Value) : false;
    //    txtCombSumAssured.Enabled = (!string.IsNullOrEmpty(hdnComboSumassued.Value)) ? Convert.ToBoolean(hdnComboSumassued.Value) : false;
    //    ddlComboFrequency.Enabled = (!string.IsNullOrEmpty(hdnComboFrequency.Value)) ? Convert.ToBoolean(hdnComboFrequency.Value) : false;
    //    txtComboMonthlyPayout.Enabled = (!string.IsNullOrEmpty(hdnBaseMonthlypayout.Value)) ? Convert.ToBoolean(hdnBaseMonthlypayout.Value) : false;
    //}


    public void FillMasterDetailsOld(ref DataSet _dsFollowuo)
    {
        Logger.Info(strApplicationno + "STAG 2A:-Fill master Details Region" + System.Environment.NewLine);
        objComm.OnlineMasterDisplayDetails_GET(ref _dsFollowuo);
        if (_dsFollowuo.Tables.Count > 0)
        {
            ViewState["_dsFollowuo"] = _dsFollowuo;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //BindMasterDetails(ddlUWreason1);
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            BindMasterDetails(ddlUWDecesion);
            BindMasterDetails(ddlBkdateReason);
            BindMasterDetails(ddlLoadType);
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            // BindMasterDetailsCheckBox(ddlLoadRsn1);
            // BindMasterDetails(ddlLoadRsn2);
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            BindMasterDetails(ddlLoadFlatMortality);
            BindMasterDetails(ddlCommonStatus);
            //BindMasterDetails(ddlIfscCode);            
            BindMasterDetails(ddlAutoPaytype);
            //Added by Suraj on 18 APRIL 2018 for add reason dropdown for medical
            BindMasterDetails(ddlRequirementMedicalReason);
            BindMasterDetails(ddlRsnExcessPremium); // added by pragati Cr 27524
        }
    }
    public void FillMasterDetails(ref DataSet _dsFollowuo)
    {
        Logger.Info(strApplicationno + "STAG 2A:-Fill master Details Region" + System.Environment.NewLine);
        /*check if cache is null or not */

        //pragati backdating start
        string sss = HdnBusinessDate.Value;

        objComm.GetHealthProduct(ref _dsAppNo, strApplicationno, "01");

        if (_dsAppNo.Tables[0].Rows.Count > 0)
        {
            hdnHealth.Value = "Health";
            //Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillSubMasterDetails" + System.Environment.NewLine);
            // objComm.OnlineSubMasterDisplayDetails_GET1(ref _dsFollowuo, strDatavalue, ddlType, ddlsubtype);

            objComm.OnlineMasterDisplayDetails_GET1(ref _dsFollowuo);
        }
        else
        {
            //objComm.OnlineSubMasterDisplayDetails_GET2(ref _dsFollowuo, strDatavalue, ddlType, ddlsubtype);
            hdnHealth.Value = "NotHealth";
            objComm.OnlineMasterDisplayDetails_GET(ref _dsFollowuo);
        }

        //pragati backdating end

        //if (HttpRuntime.Cache["DropDownList"] == null)
        //{

        //    //Step 1 (Call ur procedure

        //    objComm.GetHealthProduct(ref _dsAppNo, strApplicationno, "01");

        //    if (_dsAppNo.Tables[0].Rows.Count > 0)
        //    {
        //        hdnHealth.Value = "Health";
        //        //Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillSubMasterDetails" + System.Environment.NewLine);
        //        // objComm.OnlineSubMasterDisplayDetails_GET1(ref _dsFollowuo, strDatavalue, ddlType, ddlsubtype);

        //        objComm.OnlineMasterDisplayDetails_GET1(ref _dsFollowuo);
        //    }
        //    else
        //    {
        //        //objComm.OnlineSubMasterDisplayDetails_GET2(ref _dsFollowuo, strDatavalue, ddlType, ddlsubtype);
        //        hdnHealth.Value = "NotHealth";
        //        objComm.OnlineMasterDisplayDetails_GET(ref _dsFollowuo);
        //    }

        //    //objComm.OnlineMasterDisplayDetails_GET(ref _dsFollowuo);


        //    //fill cache 
        //    HttpRuntime.Cache.Add("DropDownList", _dsFollowuo, null, System.DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.Normal, null);
        //}
        //else
        //{
        //    //fill DsFolloUp From Cache
        //    _dsFollowuo = (DataSet)HttpRuntime.Cache["DropDownList"];
        //}

        /*end here*/

        if (_dsFollowuo.Tables.Count > 0)
        {

            ViewState["_dsFollowuo"] = _dsFollowuo;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            // BindMasterDetails(ddlUWreason1);
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            BindMasterDetails(ddlUWDecesion);
            BindMasterDetails(ddlBkdateReason);
            BindMasterDetails(ddlLoadType);
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            // BindMasterDetailsCheckBox(ddlLoadRsn1);
            // BindMasterDetails(ddlLoadRsn2);
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            BindMasterDetails(ddlLoadFlatMortality);
            BindMasterDetails(ddlCommonStatus);
            //BindMasterDetails(ddlIfscCode);            
            BindMasterDetails(ddlAutoPaytype);
            /*ID:10 START*/
            if (_dsFollowuo.Tables.Count > 9)
            {
                BindMasterDetails(ddlApplicationDetailsProposalType);
            }
            if (_dsFollowuo.Tables.Count > 10)
            {
                BindMasterDetails(cblDecisionTypeDecisions);
            }
            if (_dsFollowuo.Tables.Count > 11)
            {
                BindMasterDetails(cblDecisionTypeIncompleteDocument);
            }
            if (_dsFollowuo.Tables.Count > 12)
            {
                BindMasterDetails(cblDecisionTypeCleanCase);
            }

            /*ID:10 END*/
            //Added by Suraj on 25 APRIL 2018
            if (_dsFollowuo.Tables.Count > 13)
            {
                BindMasterDetails(ddlAssigmentType);
            }
            //Added by Suraj on 18 APRIL 2018 for add reason dropdown for medical
            if (_dsFollowuo.Tables.Count > 18)
            {
                BindMasterDetails(ddlRequirementMedicalReason);
            }
            //Added by Suraj on 05 JULY 2018 for add country list
            if (_dsFollowuo.Tables.Count > 19)
            {
                BindMasterDetails(ddlcountry);
            }
            //Added by Suraj on 25 JULY 2019 for signature match
            if (_dsFollowuo.Tables.Count > 20)
            {
                BindMasterDetails(ddlSignature);
            }
            //Added by Suraj on 01 JULY 2020 for catagory only for product code E83,84
            if (_dsFollowuo.Tables.Count > 21)
            {
                BindMasterDetails(ddlNAWPCatagory);
            }
            // added by pragati Cr 27524
            if (_dsFollowuo.Tables.Count > 22)// check count
            {
                BindMasterDetails(ddlRsnExcessPremium);
            }
            //  end added by pragati Cr 27524
            // added by Brijesh
            if (_dsFollowuo.Tables.Count > 23)// check count
            {
                BindMasterDetails(ddlRelationStaff);
            }
            //  end added by Brijesh
        }
    }

    [WebMethod]
    public static List<string> GetAutoCompleteData(string IFSC)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :GetAutoCompleteData" + System.Environment.NewLine);
        List<string> empResult = new List<string>();
        List<string> result = new List<string>();
        string connectionString = ConfigurationManager.AppSettings["transactiondbLF"];
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand objcmd = new SqlCommand("usp_GetBankAutoCompleteData", con);
        objcmd.Parameters.AddWithValue("@Ifsc", IFSC);
        DataTable dtIfscData = new DataTable();
        objcmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(objcmd);
        da.Fill(dtIfscData);
        if (dtIfscData.Rows.Count > 0)
        {
            foreach (DataRow dr in dtIfscData.Rows)
            {
                empResult.Add(dr["IFSC CODE"].ToString());
            }
        }
        return empResult;
    }

    [WebMethod]
    public static string[] GetTextChanged(string Ifsc)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :GetTextChanged" + System.Environment.NewLine);
        List<string> strDetailIDList = new List<string>();
        //List<string> empResult = new List<string>();
        // List<string> result = new List<string>();
        string connectionString = ConfigurationManager.AppSettings["transactiondbLF"];
        SqlConnection Sqlconn = new SqlConnection(connectionString);

        SqlCommand objcmd = new SqlCommand("Ups_IfscCode", Sqlconn);
        objcmd.Parameters.AddWithValue("@Ifsc", Ifsc);
        DataSet dsFollowUpDate = new DataSet();
        objcmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(objcmd);
        //DataTable dtIFSC = new DataTable();

        da.Fill(dsFollowUpDate);
        if (dsFollowUpDate.Tables[0].Rows.Count > 0)
        {
            strDetailIDList.Add(dsFollowUpDate.Tables[0].Rows[0][1].ToString());
            strDetailIDList.Add(dsFollowUpDate.Tables[0].Rows[0][4].ToString());
            strDetailIDList.Add(dsFollowUpDate.Tables[0].Rows[0][6].ToString());
        }
        else
        {
            //strDetailIDList.Add(dsFollowUpDate.Tables[0].Rows[0][1].ToString());
            //strDetailIDList.Add(dsFollowUpDate.Tables[0].Rows[0][4].ToString());
            //strDetailIDList.Add(dsFollowUpDate.Tables[0].Rows[0][6].ToString());
        }
        //foreach (DataRow row in dsFollowUpDate.Tables[0].Rows)
        //{

        //}
        string[] strDetailID = strDetailIDList.ToArray();
        return strDetailID;
    }


    private void AddNewRowToGrid1()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :AddNewRowToGrid1" + System.Environment.NewLine);
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;

            DropDownList REQ_followUpCode;
            TextBox REQ_description;
            DropDownList REQ_category;
            DropDownList REQ_criteria;
            DropDownList REQ_lifeType;
            DropDownList REQ_status;
            Label lblRaiseddate;
            //Label lblRaisedby;
            Label lblClosedDate;
            //Label lblClosedBy;


            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                // drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);

                //Store the current data to ViewState
                ViewState["CurrentTable1"] = dtCurrentTable;
                DateTime _strGetdate = DateTime.Now;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //extract the DropDownList Selected Items
                    REQ_followUpCode = (DropDownList)gvRequirmentDetails.Rows[i].Cells[0].FindControl("ddlfollowupcode");
                    REQ_description = (TextBox)gvRequirmentDetails.Rows[i].Cells[1].FindControl("lblfollowupDiscp");
                    REQ_category = (DropDownList)gvRequirmentDetails.Rows[i].Cells[2].FindControl("ddlCategory");
                    REQ_criteria = (DropDownList)gvRequirmentDetails.Rows[i].Cells[3].FindControl("ddlCriteria");
                    REQ_lifeType = (DropDownList)gvRequirmentDetails.Rows[i].Cells[4].FindControl("ddlLifeType");
                    REQ_status = (DropDownList)gvRequirmentDetails.Rows[i].Cells[5].FindControl("ddlStatus");

                    //add requirment raisedby details 17 nov2017
                    lblRaiseddate = (Label)gvRequirmentDetails.Rows[i].Cells[6].FindControl("lblRaiseddate");
                    //lblRaisedby = (Label)gvRequirmentDetails.Rows[i].Cells[6].FindControl("lblRaisedby");
                    lblClosedDate = (Label)gvRequirmentDetails.Rows[i].Cells[7].FindControl("lblClosedDate");
                    //lblClosedBy = (Label)gvRequirmentDetails.Rows[i].Cells[6].FindControl("lblClosedBy");
                    //if (HighRisk == true)
                    //{
                    //    REQ_followUpCode.SelectedValue = "PRE";
                    //}
                    dtCurrentTable.Rows[i]["REQ_followUpCode"] = REQ_followUpCode.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_description"] = REQ_description.Text;
                    dtCurrentTable.Rows[i]["REQ_category"] = REQ_category.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_criteria"] = REQ_criteria.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_lifeType"] = REQ_lifeType.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_status"] = REQ_status.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_RaisedDate"] = lblRaiseddate.Text == "" ? Convert.ToString(_strGetdate) : lblRaiseddate.Text;
                    //dtCurrentTable.Rows[i]["lblRaisedby"] = lblRaisedby.Text;
                    dtCurrentTable.Rows[i]["REQ_ClosedDate"] = lblClosedDate.Text == "" ? (object)DBNull.Value : lblClosedDate.Text;
                    // dtCurrentTable.Rows[i]["lblClosedBy"] = lblClosedBy.Text;

                }
                //35.1 Start of Changes; Sagar Thorave-[mfl00886]
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    REQ_followUpCode = (DropDownList)gvRequirmentDetails.Rows[i].Cells[0].FindControl("ddlfollowupcode");
                    
                    NotSelectMerCheckbox(REQ_followUpCode.Text);
                    SelectMerCheckbox(REQ_followUpCode.Text);
                    if (REQ_followUpCode.Text == "MPN" || REQ_followUpCode.Text == "MPV")
                        break;
                }
                //35.1 End of Changes; Sagar Thorave-[mfl00886]
                //Rebind the Grid with the current data
                gvRequirmentDetails.DataSource = dtCurrentTable;
                gvRequirmentDetails.DataBind();

                UpdatePanel1.Update();
            }
        }

        //Set Previous Data on Postbacks
        SetPreviousData1();
    }

    private void AddNewRowToLoadingGrid1()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :AddNewRowToLoadingGrid1" + System.Environment.NewLine);
        if (ViewState["LoadingView"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["LoadingView"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["LoadingView"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //extract the DropDownList Selected Items
                    Label lblLoadType = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadType");
                    Label lblLoadRsn1 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn1");
                    Label lblLoadRsn2 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn2");
                    Label lblLoadper = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadper");
                    Label lblRateAdjust = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRateAdjust");
                    Label lblFlatMortality = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblFlatMortality");

                    dtCurrentTable.Rows[i]["ddlLoadType"] = lblLoadType.Text;
                    dtCurrentTable.Rows[i]["ddlLoadRsn1"] = lblLoadRsn1.Text;
                    dtCurrentTable.Rows[i]["ddlRsn2"] = lblLoadRsn2.Text;
                    dtCurrentTable.Rows[i]["txtLoadPer"] = lblLoadper.Text;
                    dtCurrentTable.Rows[i]["txtRateAdjust"] = lblRateAdjust.Text;
                    dtCurrentTable.Rows[i]["ddlLoadFlatMortality"] = lblFlatMortality.Text;
                    //dtCurrentTable.Rows[i]["ddlLoadFlatMortality"] = lblFlatMortality.Text;
                }

                //Rebind the Grid with the current data
                gvLoadingDtls.DataSource = dtCurrentTable;
                gvLoadingDtls.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetPreviousLoadingData();
    }

    private void SetPreviousLoadingData()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :SetPreviousLoadingData" + System.Environment.NewLine);
        int rowIndex = 0;
        if (ViewState["LoadingView"] != null)
        {
            DataTable dt = (DataTable)ViewState["LoadingView"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                    Label lblLoadType = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadType");
                    Label lblRsn1Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn1Desc");
                    Label lblRsn2Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn2Desc");
                    Label lblRsn3Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn3Desc");
                    Label lblRsn4Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn4Desc");
                    // Label lblLoadRsn2 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn2");
                    //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
                    Label lblLoadper = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadper");
                    Label lblRateAdjust = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRateAdjust");
                    Label lblFlatMortality = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblFlatMortality");



                    //Fill the DropDownList with Data
                    //FillDropDownList(ddlLoadType, "LoadingCode", "Mst");
                    //BindMasterDetails(ddlLoadType);
                    //BindMasterDetails(ddlLoadType);

                    if (i <= dt.Rows.Count - 1)
                    {
                        //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                        lblLoadType.Text = dt.Rows[i]["ddlLoadType"].ToString();
                        lblRsn1Desc.Text = dt.Rows[i]["txtRsn1Desc"].ToString();
                        lblRsn2Desc.Text = dt.Rows[i]["txtRsn2Desc"].ToString();
                        lblRsn3Desc.Text = dt.Rows[i]["txtRsn3Desc"].ToString();
                        lblRsn4Desc.Text = dt.Rows[i]["txtRsn4Desc"].ToString();
                        //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
                        lblLoadper.Text = dt.Rows[i]["txtLoadPer"].ToString();
                        lblRateAdjust.Text = dt.Rows[i]["txtRateAdjust"].ToString();
                        lblFlatMortality.Text = dt.Rows[i]["ddlLoadFlatMortality"].ToString();

                        //ddlLoadType.Items.FindByText(dt.Rows[i]["ddlLoadType"].ToString()).Selected = true;
                        //txtLoadDesc.Text = dt.Rows[i]["txtLoadDesc"].ToString();
                        //ddlLoadRsn1.Items.FindByText(dt.Rows[i]["ddlLoadRsn1"].ToString()).Selected = true;
                        // txtRsn1Desc.Text = dt.Rows[i]["txtRsn1Desc"].ToString();
                        //ddlRsn2.Items.FindByText(dt.Rows[i]["ddlRsn2"].ToString()).Selected = true;
                        //txtRsn2Desc.Text = dt.Rows[i]["txtRsn2Desc"].ToString();
                        //txtLoadPer.Text = dt.Rows[i]["txtLoadPer"].ToString();
                        //txtRateAdjust.Text = dt.Rows[i]["txtRateAdjust"].ToString();
                        //ddlLoadFlatMortality.Items.FindByText(dt.Rows[i]["ddlLoadFlatMortality"].ToString()).Selected = true;
                        // ddlLoadletterPrint.Items.FindByText(dt.Rows[i]["ddlLoadletterPrint"].ToString()).Selected = true;
                    }
                    rowIndex++;
                }
            }
        }
    }

    private void SetInitialRequirmentGrid()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :SetInitialRequirmentGrid" + System.Environment.NewLine);
        DataTable dt = new DataTable();
        DataSet _ds = new DataSet();
        _ds.Tables.Add(dt);
        DataRow dr = null;

        //Define the Columns
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_followUpCode", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_description", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_category", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_criteria", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_lifeType", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_status", typeof(string)));
        dt.Columns.Add(new DataColumn("lblRaiseddate", typeof(string)));
        //dt.Columns.Add(new DataColumn("lblRaisedby", typeof(string)));
        dt.Columns.Add(new DataColumn("lblClosedDate", typeof(string)));
        //dt.Columns.Add(new DataColumn("lblClosedBy", typeof(string)));

        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable1"] = _ds;
        //Bind the DataTable to the Grid
        gvRequirmentDetails.DataSource = _ds;
        gvRequirmentDetails.DataBind();

        DropDownList ddlfollowupcode = (DropDownList)gvRequirmentDetails.Rows[0].Cells[0].FindControl("ddlfollowupcode");
        TextBox txtLoadRider = (TextBox)gvRequirmentDetails.Rows[0].Cells[1].FindControl("lblfollowupDiscp");
        DropDownList ddlCategory = (DropDownList)gvRequirmentDetails.Rows[0].Cells[2].FindControl("ddlCategory");
        DropDownList ddlCriteria = (DropDownList)gvRequirmentDetails.Rows[0].Cells[3].FindControl("ddlCriteria");
        DropDownList ddlLifeType = (DropDownList)gvRequirmentDetails.Rows[0].Cells[4].FindControl("ddlLifeType");
        DropDownList ddlStatus = (DropDownList)gvRequirmentDetails.Rows[0].Cells[5].FindControl("ddlStatus");
        Label lblRaiseddate = (Label)gvRequirmentDetails.Rows[0].Cells[6].FindControl("lblRaiseddate");
        //Label lblRaisedby = (Label)gvRequirmentDetails.Rows[0].Cells[7].FindControl("lblRaisedby");
        Label lblClosedDate = (Label)gvRequirmentDetails.Rows[0].Cells[8].FindControl("lblClosedDate");
        // Label lblClosedBy = (Label)gvRequirmentDetails.Rows[0].Cells[9].FindControl("lblClosedBy");

        //Fill the DropDownList with Data
        //FillDropDownList(ddlLoadType, "LoadingCode", "Mst");
        btnReqaddrows.CssClass = "btn btn-primary";
        BindMasterDetails(ddlfollowupcode);
        BindMasterDetails(ddlCategory);
        BindMasterDetails(ddlCriteria);
        //BindMasterDetails(ddlLifeType);
        BindMasterDetails(ddlStatus);

    }

    public void PopulateLoadingDetails()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :PopulateLoadingDetails" + System.Environment.NewLine);
        DataTable _dtLoaddata = new DataTable();
        DataTable _dtLoadresoncodes = new DataTable();
        DataSet _dsRate = new DataSet();
        DataRow _drRow = null;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        LoadDtls objLoad = new LoadDtls();
       // 44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
        string _strLoadReason1 = string.Empty;
        string _strLoadReason2 = string.Empty;
        string _strLoadReason3 = string.Empty;
        string _strLoadReason4 = string.Empty;
        
        if (ViewState["LoadingData"] == null)
        {
            string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];
            _dtLoaddata.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadType", typeof(string)));
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //_dtLoaddata.Columns.Add(new DataColumn("ddlLoadRsn1", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("txtRsn1Desc", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("txtRsn2Desc", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("txtRsn3Desc", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("txtRsn4Desc", typeof(string)));
            //_dtLoaddata.Columns.Add(new DataColumn("ddlRsn2", typeof(string)));
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            _dtLoaddata.Columns.Add(new DataColumn("txtLoadPer", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("txtRateAdjust", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadFlatMortality", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("RiderName", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("LoadingDiscp", typeof(string)));
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            // _dtLoaddata.Columns.Add(new DataColumn("Reason1Discp", typeof(string)));
            // _dtLoaddata.Columns.Add(new DataColumn("Reason2Discp", typeof(string)));
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            _dtLoaddata.Columns.Add(new DataColumn("LetterPrint", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadCode", typeof(string)));
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //_dtLoaddata.Columns.Add(new DataColumn("ddlLoadRsn1Code", typeof(string)));
            //_dtLoaddata.Columns.Add(new DataColumn("ddlLoadRsn2Code", typeof(string)));
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            _dtLoaddata.Columns.Add(new DataColumn("RiderCode", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("LoadType", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("Loadrsn1", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("Loadrsn2", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("Loadrsn3", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("Loadrsn4", typeof(string)));
            _drRow = _dtLoaddata.NewRow();
            _drRow[1] = ddlLoadType.SelectedItem.Text;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]k
            //_drRow[2] = ddlLoadRsn1.SelectedItem.Text;
            _drRow[2] = txtRsn1Desc.Text = Request.Form[txtRsn1Desc.UniqueID];
            _drRow[3] = txtRsn2Desc.Text = Request.Form[txtRsn2Desc.UniqueID];
            _drRow[4] = txtRsn3Desc.Text = Request.Form[txtRsn3Desc.UniqueID];
            _drRow[5] = txtRsn4Desc.Text = Request.Form[txtRsn4Desc.UniqueID];
            //_drRow[3] = ddlLoadRsn2.SelectedItem.Text;
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]

            _drRow[6] = txtLoadPer.Text;
            
            _drRow[7] = txtRateAdjust.Text;
            _drRow[8] = ddlLoadFlatMortality.SelectedValue;
            _drRow[9] = ddlLoadRiderCode.SelectedItem.Text;
            _drRow[10] = txtLoadDesc.Text;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //_drRow[10] = txtRsn2Desc.Text;
            _drRow[11] = ddlLoadletterPrint.SelectedValue;
            // _drRow[13] = ddlLoadRsn1.SelectedValue;
            //_drRow[14] = ddlLoadRsn2.SelectedValue;
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            _drRow[12] = ddlLoadType.SelectedValue;
            _drRow[13] = ddlLoadRiderCode.SelectedValue;
            _drRow[14] = ddlLoadType.SelectedItem.Text;
            if (!string.IsNullOrEmpty(loadrsncode))
            {
                for (int k = 0; k < loadrsncode.Split(',').Length; k++)
                {
                    if (k == 0)
                    {
                        _strLoadReason1 = loadrsncode.Split(',')[k];
                    }


                    if (k == 1)
                    {
                        _strLoadReason2 = loadrsncode.Split(',')[k];
                    }

                    if (k == 2)
                    {
                        _strLoadReason3 = loadrsncode.Split(',')[k];
                    }


                    if (k == 3)
                    {
                        _strLoadReason4 = loadrsncode.Split(',')[k];
                    }
                }
            }
            else
            {
                _strLoadReason1 = string.Empty;
                _strLoadReason2 = string.Empty;
                _strLoadReason3 = string.Empty;
                _strLoadReason4 = string.Empty;
            }
            _drRow[15] = _strLoadReason1;
            _drRow[16] = _strLoadReason2;
            _drRow[17] = _strLoadReason3;
            _drRow[18] = _strLoadReason4;
            _dtLoaddata.Rows.Add(_drRow);

            //Session["LoadingData"] = _dtLoaddata;
            ViewState["LoadingData"] = _dtLoaddata.Copy();
            gvLoadingDtls.DataSource = _dtLoaddata;
            gvLoadingDtls.DataBind();
            ddlLoadFlatMortality.CssClass = "form-control";
            txtRateAdjust.CssClass = "form-control ";
            ddlLoadletterPrint.CssClass = "form-control";

            ddlLoadType.SelectedValue = "0";

            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //ddlLoadRsn1.SelectedValue = "";
            txtRsn1Desc.Text = "";
            txtRsn2Desc.Text = "";
            txtRsn3Desc.Text = "";
            txtRsn4Desc.Text = "";
            // ddlLoadRsn2.SelectedValue = "0";
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            ddlLoadFlatMortality.SelectedValue = "0";
            txtLoadPer.Text = "";
            txtLoadDesc.Text = "";
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //txtRsn1Desc.Text = "";
            // txtRsn2Desc.Text = "";
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            txtRateAdjust.Text = "";
            ddlLoadletterPrint.SelectedValue = "0";
            ddlLoadFlatMortality.SelectedValue = "0";


            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];
            //_dtLoadresoncodes.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn1", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn2", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn3", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn4", typeof(string)));
            //_drRow = _dtLoadresoncodes.NewRow();
            //if (!string.IsNullOrEmpty(loadrsncode))
            //{
            //    for (int k = 0; k < loadrsncode.Split(',').Length; k++)
            //    {
            //        if (k == 0)
            //        {
            //            _strLoadReason1 = loadrsncode.Split(',')[k];
            //        }


            //        if (k == 1)
            //        {
            //            _strLoadReason2 = loadrsncode.Split(',')[k];
            //        }

            //        if (k == 2)
            //        {
            //            _strLoadReason3 = loadrsncode.Split(',')[k];
            //        }


            //        if (k == 3)
            //        {
            //            _strLoadReason4 = loadrsncode.Split(',')[k];
            //        }
            //    }
            //}
            //else
            //{
            //    _strLoadReason1 = string.Empty;
            //    _strLoadReason2 = string.Empty;
            //    _strLoadReason3 = string.Empty;
            //    _strLoadReason4 = string.Empty;
            //}
            //_drRow[1] = _strLoadReason1;
            //_drRow[2] = _strLoadReason2;
            //_drRow[3] = _strLoadReason3;
            //_drRow[4] = _strLoadReason4;
            //_dtLoadresoncodes.Rows.Add(_drRow);
            //ViewState["LoadingCODEData"] = _dtLoadresoncodes.Copy();

            updLoadType.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();
            UpdatePanel5.Update();
            updAddloading.Update();


        }
        else
        {
            //_dtLoaddata = (DataTable)Session["LoadingData"];

            //54.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];
            //54.1 End of Changes; Bhaumik Patel - [CR - 3334]

            _dtLoaddata = (DataTable)ViewState["LoadingData"];
            _drRow = _dtLoaddata.NewRow();
            _drRow[1] = ddlLoadType.SelectedItem.Text;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //_drRow[2] = ddlLoadRsn1.SelectedItem.Text;
            _drRow[2] = txtRsn1Desc.Text = Request.Form[txtRsn1Desc.UniqueID];
            _drRow[3] = txtRsn2Desc.Text = Request.Form[txtRsn2Desc.UniqueID];
            _drRow[4] = txtRsn3Desc.Text = Request.Form[txtRsn3Desc.UniqueID];
            _drRow[5] = txtRsn4Desc.Text = Request.Form[txtRsn4Desc.UniqueID];
            //_drRow[3] = ddlLoadRsn2.SelectedItem.Text;
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]

            _drRow[6] = txtLoadPer.Text;
            _drRow[7] = txtRateAdjust.Text;
            _drRow[8] = ddlLoadFlatMortality.SelectedValue;
            _drRow[9] = ddlLoadRiderCode.SelectedItem.Text;
            _drRow[10] = txtLoadDesc.Text;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //_drRow[10] = txtRsn2Desc.Text;
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            _drRow[11] = ddlLoadletterPrint.SelectedValue;
            _drRow[12] = ddlLoadType.SelectedValue;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            // _drRow[13] = ddlLoadRsn1.SelectedValue;
            //_drRow[14] = ddlLoadRsn2.SelectedValue;
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            _drRow[13] = ddlLoadRiderCode.SelectedValue;
            _drRow[14] = ddlLoadType.SelectedItem.Text;
            if (!string.IsNullOrEmpty(loadrsncode))
            {
                for (int k = 0; k < loadrsncode.Split(',').Length; k++)
                {
                    if (k == 0)
                    {
                        _strLoadReason1 = loadrsncode.Split(',')[k];
                    }


                    if (k == 1)
                    {
                        _strLoadReason2 = loadrsncode.Split(',')[k];
                    }

                    if (k == 2)
                    {
                        _strLoadReason3 = loadrsncode.Split(',')[k];
                    }


                    if (k == 3)
                    {
                        _strLoadReason4 = loadrsncode.Split(',')[k];
                    }
                }
            }
            else
            {
                _strLoadReason1 = string.Empty;
                _strLoadReason2 = string.Empty;
                _strLoadReason3 = string.Empty;
                _strLoadReason4 = string.Empty;
            }
            _drRow[15] = _strLoadReason1;
            _drRow[16] = _strLoadReason2;
            _drRow[17] = _strLoadReason3;
            _drRow[18] = _strLoadReason4;
            _dtLoaddata.Rows.Add(_drRow);

           
            // Session["LoadingData"] = _dtLoaddata;
            ViewState["LoadingData"] = _dtLoaddata.Copy();
            gvLoadingDtls.DataSource = _dtLoaddata;
            gvLoadingDtls.DataBind();
            ddlLoadFlatMortality.CssClass = "form-control";
            txtRateAdjust.CssClass = "form-control";
            ddlLoadletterPrint.CssClass = "form-control";

            ddlLoadType.SelectedValue = "0";
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //ddlLoadRsn1.SelectedValue = "";
            txtRsn1Desc.Text = "";
            txtRsn2Desc.Text = "";
            txtRsn3Desc.Text = "";
            txtRsn4Desc.Text = "";
            //ddlLoadRsn2.SelectedValue = "0";
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            ddlLoadFlatMortality.SelectedValue = "0";
            txtLoadPer.Text = "";
            txtLoadDesc.Text = "";
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //txtRsn1Desc.Text = "";
            // txtRsn2Desc.Text = "";
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            txtRateAdjust.Text = "";
            ddlLoadFlatMortality.SelectedValue = "0";
            ddlLoadletterPrint.SelectedValue = "0";
            //string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];
            //_dtLoadresoncodes.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn1", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn2", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn3", typeof(string)));
            //_dtLoadresoncodes.Columns.Add(new DataColumn("Loadrsn4", typeof(string)));
            //_drRow = _dtLoadresoncodes.NewRow();
            //if (!string.IsNullOrEmpty(loadrsncode))
            //{
            //    for (int k = 0; k < loadrsncode.Split(',').Length; k++)
            //    {
            //        if (k == 0)
            //        {
            //            _strLoadReason1 = loadrsncode.Split(',')[k];
            //        }


            //        if (k == 1)
            //        {
            //            _strLoadReason2 = loadrsncode.Split(',')[k];
            //        }

            //        if (k == 2)
            //        {
            //            _strLoadReason3 = loadrsncode.Split(',')[k];
            //        }


            //        if (k == 3)
            //        {
            //            _strLoadReason4 = loadrsncode.Split(',')[k];
            //        }
            //    }
            //}
            //else
            //{
            //    _strLoadReason1 = string.Empty;
            //    _strLoadReason2 = string.Empty;
            //    _strLoadReason3 = string.Empty;
            //    _strLoadReason4 = string.Empty;
            //}
            //_drRow[1] = _strLoadReason1;
            //_drRow[2] = _strLoadReason2;
            //_drRow[3] = _strLoadReason3;
            //_drRow[4] = _strLoadReason4;
            //_dtLoadresoncodes.Rows.Add(_drRow);
            //ViewState["LoadingCODEData"] = _dtLoadresoncodes.Copy();

            updLoadType.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();
            UpdatePanel5.Update();
            updAddloading.Update();
        }
        if (gvLoadingDtls.Rows.Count > 0)
        {

            //hdnIsConsentReq.Value = response.ToString();
            if (objChangeObj.Load_Loadingdetails == null)
            {
                objChangeObj.Load_Loadingdetails = new LoadDtls();
            }

            objLoad._strProdcode = objChangeObj.Load_Loadingdetails._strProdcode;
            objLoad.isConsentRequired = true;
            objChangeObj.Load_Loadingdetails = objLoad;
            Session["objLoginObj"] = objChangeObj;
        }
        else
        {
            objChangeObj.Load_Loadingdetails.isConsentRequired = false;
        }
        Session["objLoginObj"] = objChangeObj;
    }

    private void SetInitialLoadingGrid()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :SetInitialLoadingGrid" + System.Environment.NewLine);
        DataTable dt = new DataTable();
        DataRow dr = null;
        if (ViewState["LoadingView"] != null)
        {

            //Define the Columns
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("ddlLoadType", typeof(string)));
            dt.Columns.Add(new DataColumn("ddlLoadRsn1", typeof(string)));
            dt.Columns.Add(new DataColumn("ddlRsn2", typeof(string)));
            dt.Columns.Add(new DataColumn("txtLoadPer", typeof(string)));
            dt.Columns.Add(new DataColumn("txtRateAdjust", typeof(string)));
            dt.Columns.Add(new DataColumn("ddlLoadFlatMortality", typeof(string)));
            //dt.Columns.Add(new DataColumn("ddlLoadletterPrint", typeof(string)));


            //Add a Dummy Data on Initial Load
            dr = dt.NewRow();

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState

            //Bind the DataTable to the Grid
            gvLoadingDtls.DataSource = dt;
            gvLoadingDtls.DataBind();

            Label lblLoadType = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadType");
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            Label lblRsn1Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn1Desc");
            Label lblRsn2Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn2Desc");
            Label lblRsn3Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn3Desc");
            Label lblRsn4Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn4Desc");
            // Label lblLoadRsn2 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn2");
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            Label lblLoadper = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadper");
            Label lblRateAdjust = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRateAdjust");
            Label lblFlatMortality = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblFlatMortality");
            lblLoadType.Text = ddlLoadType.SelectedItem.Text;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //lblLoadRsn1.Text = ddlLoadRsn1.SelectedItem.Text;
            lblRsn1Desc.Text = txtRsn1Desc.Text;
            lblRsn2Desc.Text = txtRsn2Desc.Text;
            lblRsn3Desc.Text = txtRsn3Desc.Text;
            lblRsn4Desc.Text = txtRsn4Desc.Text;
            //lblLoadRsn2.Text = ddlLoadRsn2.SelectedItem.Text;
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            lblLoadper.Text = txtLoadPer.Text;
            lblRateAdjust.Text = txtRateAdjust.Text;
            lblFlatMortality.Text = ddlLoadFlatMortality.SelectedValue;
            ViewState["LoadingView"] = dt;
            //TextBox txtLoadRider = (TextBox)gvLoadingDtls.Rows[0].Cells[0].FindControl("txtLoadRider");
            //DropDownList ddlLoadType = (DropDownList)gvLoadingDtls.Rows[0].Cells[1].FindControl("ddlLoadType");
            //TextBox txtLoadDesc = (TextBox)gvLoadingDtls.Rows[0].Cells[2].FindControl("txtLoadDesc");
            //DropDownList ddlLoadRsn1 = (DropDownList)gvLoadingDtls.Rows[0].Cells[3].FindControl("ddlLoadRsn1");
            //TextBox txtRsn1Desc = (TextBox)gvLoadingDtls.Rows[0].Cells[4].FindControl("txtRsn1Desc");
            //DropDownList ddlRsn2 = (DropDownList)gvLoadingDtls.Rows[0].Cells[5].FindControl("ddlRsn2");
            //TextBox txtRsnDesc = (TextBox)gvLoadingDtls.Rows[0].Cells[6].FindControl("txtRsn2Desc");
            //TextBox txtLoadPer = (TextBox)gvLoadingDtls.Rows[0].Cells[7].FindControl("txtLoadPer");
            //TextBox txtRateAdjust = (TextBox)gvLoadingDtls.Rows[0].Cells[8].FindControl("txtRateAdjust");
            //DropDownList ddlLoadFlatMortality = (DropDownList)gvLoadingDtls.Rows[0].Cells[9].FindControl("ddlLoadFlatMortality");
            //DropDownList ddlLoadletterPrint = (DropDownList)gvLoadingDtls.Rows[0].Cells[10].FindControl("ddlLoadletterPrint");

            //Fill the DropDownList with Data
            //FillDropDownList(ddlLoadType, "LoadingCode", "Mst");
            //txtLoadRider.Text = txtProname.Text;
            //txtLoadRider.Enabled = false;
            //BindMasterDetails(ddlLoadType);
            //BindMasterDetails(ddlLoadFlatMortality);
        }
        else
        {
            dt = (DataTable)ViewState["LoadingView"];
            dr = dt.NewRow();
            dt.Rows.Add(dr);
            ViewState["LoadingView"] = dt;
            //Bind the DataTable to the Grid
            gvLoadingDtls.DataSource = dt;
            gvLoadingDtls.DataBind();

            Label lblLoadType = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadType");
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            Label lblRsn1Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn1Desc");
            Label lblRsn2Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn2Desc");
            Label lblRsn3Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn3Desc");
            Label lblRsn4Desc = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRsn4Desc");
            // Label lblLoadRsn2 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn2");
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            Label lblLoadper = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadper");
            Label lblRateAdjust = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRateAdjust");
            Label lblFlatMortality = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblFlatMortality");

            lblLoadType.Text = ddlLoadType.SelectedItem.Text;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //lblLoadRsn1.Text = ddlLoadRsn1.SelectedItem.Text;
            lblRsn1Desc.Text = txtRsn1Desc.Text;
            lblRsn2Desc.Text = txtRsn2Desc.Text;
            lblRsn3Desc.Text = txtRsn3Desc.Text;
            lblRsn4Desc.Text = txtRsn4Desc.Text;
            //lblLoadRsn2.Text = ddlLoadRsn2.SelectedItem.Text;
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            lblLoadper.Text = txtLoadPer.Text;
            lblRateAdjust.Text = txtRateAdjust.Text;
            lblFlatMortality.Text = ddlLoadFlatMortality.SelectedValue;
        }
    }

    private void SetPreviousData1()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :SetPreviousData1" + System.Environment.NewLine);
        int rowIndex = 0;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {

                    DropDownList ddlfollowupcode = (DropDownList)gvRequirmentDetails.Rows[i].Cells[0].FindControl("ddlfollowupcode");
                    TextBox lblfollowupDiscp = (TextBox)gvRequirmentDetails.Rows[i].Cells[1].FindControl("lblfollowupDiscp");
                    DropDownList ddlCategory = (DropDownList)gvRequirmentDetails.Rows[i].Cells[2].FindControl("ddlCategory");
                    DropDownList ddlCriteria = (DropDownList)gvRequirmentDetails.Rows[i].Cells[3].FindControl("ddlCriteria");
                    DropDownList ddlLifeType = (DropDownList)gvRequirmentDetails.Rows[i].Cells[4].FindControl("ddlLifeType");
                    DropDownList ddlStatus = (DropDownList)gvRequirmentDetails.Rows[i].Cells[5].FindControl("ddlStatus");
                    Label lblRaiseddate = (Label)gvRequirmentDetails.Rows[i].Cells[6].FindControl("lblRaiseddate");
                    //Label lblRaisedby = (Label)gvRequirmentDetails.Rows[i].Cells[7].FindControl("lblRaisedby");
                    Label lblClosedDate = (Label)gvRequirmentDetails.Rows[i].Cells[7].FindControl("lblClosedDate");
                    //Label lblClosedBy = (Label)gvRequirmentDetails.Rows[i].Cells[9].FindControl("lblClosedBy");

                    //Fill the DropDownList with Data
                    //FillDropDownList(ddlfollowupcode, "followupcode", "Mst");
                    BindMasterDetails(ddlfollowupcode);

                    if (i < dt.Rows.Count - 1)
                    {
                        ddlfollowupcode.ClearSelection();
                        ddlfollowupcode.Items.FindByText(dt.Rows[i]["REQ_followUpCode"].ToString()).Selected = true;
                        ddlfollowupcode.SelectedValue = dt.Rows[i]["REQ_followUpCode"].ToString();
                        lblfollowupDiscp.Text = dt.Rows[i]["REQ_description"].ToString();
                        ddlCategory.SelectedValue = dt.Rows[i]["REQ_category"].ToString();
                        ddlCriteria.SelectedValue = dt.Rows[i]["REQ_criteria"].ToString();
                        ddlLifeType.SelectedValue = dt.Rows[i]["REQ_lifeType"].ToString();
                        ddlStatus.SelectedValue = dt.Rows[i]["REQ_status"].ToString();
                        lblRaiseddate.Text = dt.Rows[i]["REQ_RaisedDate"].ToString();
                        //lblRaisedby.Text = dt.Rows[i]["lblRaisedby"].ToString();
                        lblClosedDate.Text = dt.Rows[i]["REQ_ClosedDate"].ToString();
                        //lblClosedBy.Text = dt.Rows[i]["lblClosedBy"].ToString();
                    }
                    rowIndex++;
                }
            }
            //LinkButton lnkReqRemoveRow = (LinkButton)gvRequirmentDetails.Rows[dt.Rows.Count - 1].Cells[6].FindControl("lnkReqRemoveRow");
            //lnkReqRemoveRow.CssClass = "";
            btnReqaddrows.CssClass = "btn btn-primary";
            gvRequirmentDetails.CssClass = "table table-bordered table-striped";
            UpdatePanel1.Update();
        }
    }

    private ArrayList GetDummyData(string ddlType, string ddlsubtype)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :GetDummyData" + System.Environment.NewLine);
        ArrayList arr = new ArrayList();
        DataSet dt12 = new DataSet();
        FillSubMasterDetails(ref dt12, null, ddlType, ddlsubtype);

        if (dt12.Tables[0].Rows.Count > 0)
        {
            arr.Add(new ListItem("Select", "0"));
            foreach (DataRow row in dt12.Tables[0].Rows)
            {
                arr.Add(new ListItem(row["REQ_followUpCode"].ToString(), row["REQ_followUpCode"].ToString()));
            }
        }
        return arr;


    }

    public void BindMasterDetails(DropDownList ddl)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :BindMasterDetails" + System.Environment.NewLine);
        string strTableName = ddl.ID;
        if (strTableName == "ddlExstingLoadType")
        {
            strTableName = "ddlLoadType";
        }
        //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
        else if (strTableName == "ddlExistingLoadRsn1")
        {

            //strTableName = "ddlLoadRsn1";
        }
        else if (strTableName == "ddlExistingLoadRsn2")
        {
            // strTableName = "ddlLoadRsn2";
        }
        //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
        else if (strTableName == "ddlExistingLoadFlatMort")
        {
            strTableName = "ddlLoadFlatMortality";
        }
        else if (strTableName == "ddlCommonStatus")
        {
            strTableName = "ddlStatus";
        }
        /*ID:10 START*/
        else if (strTableName == "ddlApplicationDetailsProposalType")
        {
            strTableName = "ddlProposalType";
        }
        /*ID:10 END*/
        //Added by Suraj on 25 APRIL 2018
        else if (strTableName == "ddlAssigmentType")
        {
            strTableName = "ddlAssigmentType";
        }
        //Added by Suraj on 18 APRIL 2018 for medical reson dropdown 
        else if (strTableName == "ddlRequirementMedicalReason")
        {
            strTableName = "ddlRequirementMedicalReason";
        }
        //Added by Suraj on 19 APRIL 2018 for close file review question's answer 
        else if (strTableName == "ddlAnswer")
        {
            strTableName = "ddlAnswer";
        }
        //Added by Suraj on 05 JULY 2018 for add country list
        else if (strTableName == "ddlcountry")
        {
            strTableName = "ddlcountry";
        }
        //Added by Suraj on 25 JULY 2019 for signature match flag
        else if (strTableName == "ddlSignature")
        {
            strTableName = "ddlSignature";
        }
        //Added by Suraj on 01 JULY 2020 for catagory only for NAWP product code E83,84 as suggested by prafulla/saumya
        else if (strTableName == "ddlCatagory")
        {
            strTableName = "ddlCatagory";
        }
        //Added by pragati CR 27524 for ddlRsnExcessPremium
        else if (strTableName == "ddlRsnExcessPremium")
        {
            strTableName = "ddlRsnExcessPremium";
        }
        //end

        //Added by Brijesh
        else if (strTableName == "ddlRelationStaff")
        {
            strTableName = "ddlRelationStaff";
        }
        //end

        DataSet _dsMaster = (DataSet)ViewState["_dsFollowuo"];
        ArrayList arr = new ArrayList();
        if (_dsMaster != null && _dsMaster.Tables.Count > 0)
        {
            if (_dsMaster.Tables[strTableName] != null && _dsMaster.Tables[strTableName].Rows.Count > 0)
            {
                //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]
                if (strTableName == "ddlUWDecesion")
                {
                    
                    DataSet ds = new DataSet();
                    commobj.OnlineDecisionRightsDisplayDetails_GET("GetUserAccess_DecisionRights", strUserId, UserLimit, ref ds);
                    ddl.DataSource = ds.Tables[strTableName];
                    ddl.DataTextField = "NAME";
                    ddl.DataValueField = "VALUE";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                    //ddlUWDecesion.SelectedValue = "proposal";
                }
                //48.1 Begin of Changes; Bhaumik Patel - [CR-5855]
                else if (strTableName == "ddlLoadFlatMortality")
                {
                    DataSet ds = new DataSet();
                    commobj.OnlineFlatMortalityDetails_GET("GetFlatMortalityData_BIND", strUserId, ref ds);
                    ddl.DataSource = ds.Tables[strTableName];
                    ddl.DataTextField = "NAME";
                    ddl.DataValueField = "VALUE";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                //48.1 END of Changes; Bhaumik Patel - [CR-5855]
                else
                {
                    ddl.DataSource = _dsMaster.Tables[strTableName];
                    ddl.DataTextField = "NAME";
                    ddl.DataValueField = "VALUE";
                    ddl.DataBind();
                    ddl.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                //45.1 End of Changes; Bhaumik Patel - [CR-5307]
            }
        }
    }
    private void FillDropDownList(DropDownList ddl, string ddlType, string ddlsubtype)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillDropDownList" + System.Environment.NewLine);
        ArrayList arr = GetDummyData(ddlType, ddlsubtype);
        foreach (ListItem item in arr)
        {
            ddl.Items.Add(item);
        }
    }
    public void ShowHideLoader(string LoadingType, string strEvent)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ShowHideLoader" + System.Environment.NewLine);
        string script = "ldrModal.show()";
        if (LoadingType == "Show")
        {
            //string strValidationMsg = "alert('Request is Accepted')";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", script, true);
            //ClientScript.RegisterStartupScript(this.GetType(), LoadingType, script, true);
        }
        else
        {
            Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), LoadingType, "ldrModal.fadeOut()");
        }
    }
    #region Page Events Begins.


    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //Find the DropDownList in the Row
    //        DropDownList ddlfollowupcode = (e.Row.FindControl("ddlfollowupcode") as DropDownList);
    //        ddlfollowupcode.DataSource = _dsFollowuo.Tables[0];
    //        ddlfollowupcode.DataTextField = "REQ_followUpCode";
    //        ddlfollowupcode.DataValueField = "REQ_followUpCode";
    //        ddlfollowupcode.DataBind();

    //        //Add Default Item in the DropDownList
    //        ddlfollowupcode.Items.Insert(0, new ListItem("Please select"));

    //        //Select the Country of Customer in DropDownList
    //        //string country = (e.Row.FindControl("lblCountry") as Label).Text;
    //        //ddlCountries.Items.FindByValue(country).Selected = true;
    //    }
    //}
    //protected void drpAmlRisk(object sender, EventArgs e)
    //{
    //    try {
    //        int i = 0;
    //    }
    //    catch (Exception ex) {

    //    }
    //}
    protected void ddlfollowupcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ddlfollowupcode_SelectedIndexChanged");


            DataSet _dsFollowupSubDtls = new DataSet();
            //Get the master DropDownList and its value
            DropDownList ddlEditEducationEstablishment = (DropDownList)sender;
            string educationEstablishmentCode = ddlEditEducationEstablishment.SelectedValue;
            //35.1 Begin of Changes; Sagar Thorave-[mfl00886]
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];

           // dtCurrentTable.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='payer'");
            //hasValue = dtCurrentTable.AsEnumerable
            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
                //extract the DropDownList Selected Items
               DropDownList REQ_followUpCodedrp = (DropDownList)gvRequirmentDetails.Rows[i].Cells[0].FindControl("ddlfollowupcode");
                string REQ_followUpCode = REQ_followUpCodedrp.SelectedValue;
                //35.1 Start of Changes; Sagar Thorave-[mfl00886]

                NotSelectMerCheckbox(REQ_followUpCode);
                SelectMerCheckbox(REQ_followUpCode);
                if (REQ_followUpCode == "MPN" || REQ_followUpCode=="MPV")
                    break;
            }
            SelectMerCheckbox(educationEstablishmentCode);
            //35.1 End of Changes; Sagar Thorave-[mfl00886]
            // Get the GridViewRow in which this master DropDownList exists
            GridViewRow row = (GridViewRow)ddlEditEducationEstablishment.NamingContainer;
            DropDownList ddlEditSchool = (DropDownList)row.FindControl("ddlfollowupcode");
            FillSubMasterDetails(ref _dsFollowupSubDtls, educationEstablishmentCode, "followupcode", "");

            if (_dsFollowupSubDtls.Tables.Count > 0 && _dsFollowupSubDtls.Tables[0].Rows.Count > 0)
            {
                TextBox lblfollowupDiscp = (TextBox)row.FindControl("lblfollowupDiscp");
                lblfollowupDiscp.Text = _dsFollowupSubDtls.Tables[0].Rows[0]["Followupdescp"].ToString();

                DropDownList ddlCategory = (DropDownList)row.FindControl("ddlCategory");
                ddlCategory.SelectedValue = _dsFollowupSubDtls.Tables[0].Rows[0]["Category"].ToString();


                DropDownList ddlCriteria = (DropDownList)row.FindControl("ddlCriteria");
                ddlCriteria.SelectedValue = _dsFollowupSubDtls.Tables[0].Rows[0]["Criteria"].ToString();

                DropDownList ddlLifeType = (DropDownList)row.FindControl("ddlLifeType");
                ddlLifeType.SelectedValue = _dsFollowupSubDtls.Tables[0].Rows[0]["LifeType"].ToString();

                if (_dsFollowupSubDtls.Tables[0].Rows[0]["Criteria"].ToString() == "Text to be Appended")
                {
                    //lblfollowupDiscp.Enabled = true;
                    lblfollowupDiscp.CssClass = "form-control";
                }
                else
                {
                    lblfollowupDiscp.CssClass = "form-control lblLable";
                }
                UpdatePanel1.Update();
            }
        }
        catch (Exception Error)
        {
            objcomm.SaveErrorLogs("", "Failed", "UWPortfolio", "ddlfollowupcode_SelectedIndexChanged", "UWPortfolio", "E-Error", "", "", Error.ToString());
        }


    }

    protected void gvRequirmentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int ReqCount = Convert.ToInt16(Session["ReqCount"]);
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :gvRequirmentDetails_RowDataBound");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlfollowupcode = (DropDownList)e.Row.FindControl("ddlfollowupcode");
                BindMasterDetails(ddlfollowupcode);

                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlfollowupcode");
                BindMasterDetails(ddl1);

                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                BindMasterDetails(ddlCategory);

                DropDownList ddlCriteria = (DropDownList)e.Row.FindControl("ddlCriteria");
                BindMasterDetails(ddlCriteria);

                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                BindMasterDetails(ddlStatus);
                //FillDropDownList(ddl1, "followupcode", "Mst");
                _Dt = (DataTable)ViewState["CurrentTable1"];
                if (_Dt.Rows.Count >= 1)
                {
                    //LinkButton lb = e.Row.FindControl("lnkReqRemoveRow") as LinkButton;
                    //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);

                    //Image ing = e.Row.FindControl("Image8") as Image;

                    //DropDownList ddlfollowupcode = (DropDownList)e.Row.FindControl("ddlfollowupcode");
                    ddlfollowupcode.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_followUpCode"].ToString();
                    //35.1 Begin of Changes; Sagar Thorave-[mfl00886]
                    SelectMerCheckbox(ddlfollowupcode.SelectedValue);
                    //35.1 End of Changes; Sagar Thorave-[mfl00886]
                    TextBox lblfollowupDiscp = (TextBox)e.Row.FindControl("lblfollowupDiscp");
                    lblfollowupDiscp.Text = _Dt.Rows[e.Row.RowIndex]["REQ_description"].ToString();

                    //DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                    ddlCategory.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_category"].ToString();

                    //DropDownList ddlCriteria = (DropDownList)e.Row.FindControl("ddlCriteria");
                    ddlCriteria.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_criteria"].ToString();

                    DropDownList ddlLifeType = (DropDownList)e.Row.FindControl("ddlLifeType");
                    ddlLifeType.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_lifeType"].ToString();

                    Label lblRaiseddate = (Label)e.Row.FindControl("lblRaiseddate");
                    lblRaiseddate.Text = _Dt.Rows[e.Row.RowIndex]["REQ_RaisedDate"].ToString();

                    //Label lblRaisedby = (Label)e.Row.FindControl("lblRaisedby ");
                    //lblRaisedby.Text = _Dt.Rows[e.Row.RowIndex]["lblRaisedby "].ToString();

                    Label lblClosedDate = (Label)e.Row.FindControl("lblClosedDate");
                    lblClosedDate.Text = _Dt.Rows[e.Row.RowIndex]["REQ_ClosedDate"].ToString();

                    //Label lblClosedBy = (Label)e.Row.FindControl("lblClosedBy");
                    //lblClosedBy.Text = _Dt.Rows[e.Row.RowIndex]["lblClosedBy"].ToString();

                    if (ddlCategory.SelectedValue == "Medical")
                    {
                        DataSet dsREq = new DataSet();
                        objComm.GET_MedicalReportoShow(ref dsREq, strApplicationno);
                        if (dsREq.Tables.Count > 0 && dsREq.Tables[0].Rows.Count > 0)
                        {
                            ddlRequirementMedicalReason.SelectedValue = Convert.ToString(dsREq.Tables[0].Rows[0]["ReqMedicalReason"]);
                        }
                        else
                        {
                            ddlRequirementMedicalReason.SelectedValue = "9";
                        }
                    }

                    //DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    ddlStatus.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_status"].ToString();

                    //Added by Suraj on 19 APRIL 2018 for medical reason dropdown

                    //if (e.Row.RowIndex >= ReqCount)
                    //{
                    //    Image Img = (Image)e.Row.FindControl("Image8");
                    //    Img.CssClass = "";
                    //}
                    //else
                    //{
                    //    Image Img = (Image)e.Row.FindControl("Image8");
                    //    Img.CssClass = "HideControl";
                    //}

                }

            }
        }
        catch (Exception Error)
        {
            objcomm.SaveErrorLogs("", "Failed", "UWPortfolio", "gvRequirmentDetails_RowDataBound", "UWPortfolio", "E-Error", "", "", Error.ToString());
        }
    }

    protected void btnRequirmentDtlsSave_Click(object sender, EventArgs e)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnRequirmentDtlsSave_Click");
        string _strfollowupcode = string.Empty;
        string _strfollowupdiscp = string.Empty;
        string _strfollowupcategory = string.Empty;
        string _strfollowupcriteria = string.Empty;
        string _strfollowuplifetype = string.Empty;
        string _strfollowupstatus = string.Empty;
        int strFollowupResult = 0;
        bool _FollowupStatus = false;
        bool _strFolloUPflag = true;
        lblErrorRequirementDetailBody.Text = lblErrorreqdtls.Text = string.Empty;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        foreach (GridViewRow rowfollowup in gvRequirmentDetails.Rows)
        {
            DropDownList ddlfollowupcode = rowfollowup.FindControl("ddlfollowupcode") as DropDownList;
            DropDownList ddlCategory = rowfollowup.FindControl("ddlCategory") as DropDownList;
            DropDownList ddlCriteria = rowfollowup.FindControl("ddlCriteria") as DropDownList;
            DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;
            DropDownList ddlLifeType = rowfollowup.FindControl("ddlLifeType") as DropDownList;
            TextBox lblfollowupDiscp = rowfollowup.FindControl("lblfollowupDiscp") as TextBox;
            _strfollowupcode = ddlfollowupcode.SelectedValue;
            _strfollowupcategory = ddlCategory.SelectedValue;
            _strfollowupcriteria = ddlCriteria.SelectedValue;
            _strfollowuplifetype = ddlLifeType.SelectedValue;
            _strfollowupstatus = ddlStatus.SelectedValue;
            _strfollowupdiscp = lblfollowupDiscp.Text;
            if (_strfollowupstatus == "O")
            {
                _FollowupStatus = true;
            }
            objBuss.FollowupDetails_Save(strChannelType, strApplicationno, _strfollowupcode, _strfollowupdiscp, _strfollowupcategory, _strfollowupcriteria, "", objChangeObj.userLoginDetails._UserID, _strfollowupstatus,
                _strfollowupcategory, _strfollowuplifetype, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, strChannelType, strApplicationno, ref strFollowupResult);
            if (strFollowupResult == 0)
            {
                _strFolloUPflag = false;
            }
        }
        if (_strFolloUPflag)
        {
            chkReqDtls.Checked = false;
            lblErrorreqdtls.Text = "Requirment details save successfully";
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "FOLLOWUP", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (strLApushErrorCode == 0)
            {
                if (_FollowupStatus == true)
                {
                    //objCommonObj = (CommonObject)Session["objCommonObj"];
                    // strUserId = objCommonObj._Bpmuserdetails._UserID;
                    // strApplicationno = objCommonObj._ApplicationNo;
                    //strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                    strUserGroup = objChangeObj.userLoginDetails._UserGroup;
                    //strAppstatusKey = (strUserGroup == "UW") ? "UR" : "DR";
                    strAppstatusKey = (strUserGroup == "UW") ? "UR" : "DR";
                    objComm.OnlineApplicationchangeStatus(strApplicationno, objChangeObj.userLoginDetails._UserID, strAppstatusKey, "", ref strFollowupResult);
                }
                chkReqDtls.Checked = false;
                btnReqaddrows.CssClass = "btn-primary HideControl";
                lblErrorreqdtls.Text = "Requirment Details Updated in Life Asia successfully";
                lblErrorRequirementDetailBody.Text = "Requirment Details Updated in Life Asia successfully";
                FillRequirmentDetails(strApplicationno, strChannelType);
            }
            else
            {
                chkReqDtls.Checked = false;
                lblErrorreqdtls.Text = "Requirment Details Not Updated in Life Asia,Please Contact system admin";
                lblErrorRequirementDetailBody.Text = strLApushStatus;
            }
        }
        else
        {
            chkReqDtls.Checked = false;
            lblErrorRequirementDetailBody.Text = "Requirment details Not Updated in DataBase";
            lblErrorreqdtls.Text = "Requirment details Not Save CLICK to know more";
        }
    }

    protected void btnReqaddrows_Click(object sender, EventArgs e)
    {
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "REQ_ADD_NEW_ROW", ref intTrackingRet);
            /*end here*/
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnReqaddrows_Click");
            if (gvRequirmentDetails.Rows.Count <= 0)
            {
                SetInitialRequirmentGrid();
            }
            else
            {
                AddNewRowToGrid1();
            }
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }

    }
    //Set Previous Data on Postbacks
    //SetPreviousLoadingData();


    //}
    protected void btnUWCommSave_Click(object sender, EventArgs e)
    {
        SaveUwComment();
        /*
        //suraj
        if (ViewState["CommonViewState"] == null)
        {
            CommonViewState viewstst = new CommonViewState();
            viewstst.IsCommentSave = true;
            ViewState["CommonViewState"] = viewstst;
        }
        else
        {
            CommonViewState viewstst = (CommonViewState)ViewState["CommonViewState"];
            viewstst.IsCommentSave = true;
            ViewState["CommonViewState"] = viewstst;
        }
        */
    }
    protected void btnAddLoadingRow_Click(object sender, EventArgs e)
    {
        // showerrorrisk();
        //updLoadType.Update();
        //UpdatePanel3.Update();
        //UpdatePanel4.Update();
        //UpdatePanel5.Update();
        //updAddloading.Update();
        DataSet _dsRate = new DataSet();
        objUWDecision = new UWSaralDecision.BussLayer();
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnAddLoadingRow_Click");
        /*added by shri on 28 dec 17 to add tracking*/
        int intTrackingRet = -1;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "LOAD_ADD_NEWROW", ref intTrackingRet);
        /*end here*/

        //48.1 Begin of Changes; Bhaumik Patel - [CR-5855]
        commobj.OnlineFlatMortalityDetails_GET_RateAdjust("GetRateAdjustData_ByUSERID", strUserId, ref _dsRate);
        if (_dsRate.Tables.Count > 0 && _dsRate.Tables["RateAdjust"].Rows.Count > 0)
        {
            string RateAdjust = string.IsNullOrEmpty(txtRateAdjust.Text) ? "0": txtRateAdjust.Text;
            if ((Convert.ToInt32(RateAdjust)) > Convert.ToInt32(_dsRate.Tables["RateAdjust"].Rows[0]["Flat_Extra"]))
            {
                ShowPopupMessage("Rate Adjust between 0  To " + _dsRate.Tables["RateAdjust"].Rows[0]["Flat_Extra"] + "");
                //throw new Exception("Rate Adjust between 0  To " + _dsRate.Tables["RateAdjust"].Rows[0]["Flat_Extra"] + "");
            }
            //48.1 END of Changes; Bhaumik Patel - [CR-5855]
            else
            {
                if (gvLoadingDtls.Rows.Count <= 0)
                {
                    ViewState["LoadingData"] = null;
                    //Session["LoadingData"] = null;
                }
                PopulateLoadingDetails();
                // btnAddLoadingRow.CssClass = "btn btn-primary lnkButton";
                /*added by shri on 16 dec 17 to add sis reqirement on addition of loading*/
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                string strReqFollowupCode = "SIS", strReqDescription = "Please provide consent and fresh sis for extra premium in view of Medical/Non Medical Reasons. Sign Enclosed Letter."
                , strReqCategory = "Non Medical", strReqCriteria = "FIXED", strReqLifeType = "01", strReqStatus = "L"
                   , srtReqRaisedBy = objChangeObj.userLoginDetails._UserID;
                AddNewRequirement(strReqFollowupCode, strReqDescription, strReqCategory, strReqCriteria, strReqLifeType, strReqStatus
                    , srtReqRaisedBy);
                /*end here*/
                /*added by shri on 28 dec 17 to update tracking*/
                UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
                /*END HERE*/
                ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
                DataSet _ds = new DataSet(), _dsPrevPol = new DataSet();
                int LAPushErrorCode = 0; string strLAPushErrorMsg = string.Empty; string strConsentResponse = string.Empty;
                _ds = null;
                //Added by Suraj on 19 APRIL 2018 for Real time updating required for any addition or deletion of rate up loading
                ReplicaXml objReplicaXml = new ReplicaXml();
                int intResponse = -1;
                bool isdataSave = true;
                string strResponse = string.Empty;
                ManageLoadingDetails(isdataSave, ref isdataSave, objReplicaXml, _ds, ref strResponse);
                //DataSet _dsPREMCAL = new DataSet();
                PremiumCalculation(objReplicaXml, ref isdataSave, ref _ds, ref intResponse, ref strResponse);
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "proposal", ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
                /*
                if (LAPushErrorCode == 0)
                {
                    if (_ds != null)
                    {
                        if (_ds.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < _ds.Tables[0].Rows.Count; j++)
                            {

                                int BackdatedInt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["BackdatedInt"].ToString()), MidpointRounding.AwayFromZero)));
                                string ComponentCd = _dsPREMCAL.Tables[0].Rows[j]["ComponentCd"].ToString();
                                int EduCess = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["EduCess"].ToString()), MidpointRounding.AwayFromZero)));
                                int ExtraPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["ExtraPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                int InstalmentPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["InstalmentPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                string LifeType = _dsPREMCAL.Tables[0].Rows[j]["LifeType"].ToString();
                                int MedicalLoadingPremium = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["MedicalLoadingPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                //MedicalLoadingPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["MedicalLoadingPremium"].ToString());
                                int MedicalLoadingRate = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["MedicalLoadingRate"].ToString()), MidpointRounding.AwayFromZero)));
                                int ModalPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["ModalPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                int NonMedicalLoadingPremium = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["NonMedicalLoadingPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                int NonMedicalLoadingRate = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["NonMedicalLoadingRate"].ToString()), MidpointRounding.AwayFromZero)));
                                string RiderCtg = _dsPREMCAL.Tables[0].Rows[j]["RiderCtg"].ToString();
                                int RiderPPT = Convert.ToInt32(_dsPREMCAL.Tables[0].Rows[j]["RiderPPT"].ToString());
                                int RiderPT = Convert.ToInt32(_dsPREMCAL.Tables[0].Rows[j]["RiderPT"].ToString());
                                int LoadingSeriveTax = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["SeriveTax"].ToString()), MidpointRounding.AwayFromZero)));
                                long SumAssured = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["SumAssured"].ToString()), MidpointRounding.AwayFromZero)));
                                long SumAssuredAcrossPlans = Convert.ToInt64(_dsPREMCAL.Tables[0].Rows[j]["SumAssuredAcrossPlans"].ToString());
                                long TotalInstalmentPremium = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["TotalInstalmentPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                long TotalPremiumAmount = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPREMCAL.Tables[0].Rows[j]["TotalPremiumAmount"].ToString()), MidpointRounding.AwayFromZero)));
                                //if (strChannelType.ToUpper() == "OFFLINE")
                                //{
                                //objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, strComponentCode, "", "", strComponentRiderType, 0, Convert.ToDecimal(TotalPremium), ddlLoadRsn2.SelectedItem.Text, 0, Convert.ToDecimal(ExtraPremAmt), Convert.ToDecimal(ServiceTax), 0, Convert.ToDecimal(ExtraPremAmt), 0, 0, 0, 0, 0, 0, 0, ref strLApushErrorCode);
                                objComm.OnlinePremiumExtraLoadingdetails_Save_New(0, strApplicationno, txtPolno.Text, ComponentCd, RiderPPT, RiderPT, RiderCtg, BackdatedInt, TotalInstalmentPremium, ddlLoadRsn1.SelectedValue, ModalPremiumAmt, ExtraPremiumAmt, LoadingSeriveTax, EduCess, 0, SumAssured, SumAssuredAcrossPlans, MedicalLoadingPremium, MedicalLoadingRate, NonMedicalLoadingPremium, 0, NonMedicalLoadingRate, ref strLApushErrorCode);
                                //}
                            }
                        }
                    }

                }*/
                UWPreIssueServiceCall(strApplicationno, strChannelType);
                //End
                /*ID:18 Start*/
                chkLoadingDtls.Checked = false;
                //48.1 BEGIN of Changes; Bhaumik Patel - [CR-5855]
                rgvrateAdjust.ErrorMessage = "";
                //48.1 END of Changes; Bhaumik Patel - [CR-5855]
                // ddlLoadRsn1.Items.Clear();
                updLoadingDetails.Update();
                /*added for testing purpose*/
                updProductDetails.Update();
                /*ID:18 End*/
                ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
            }


        }
       
    }
    protected void btnUwdecisiondtlsSave_Click(object sender, EventArgs e)
    {
        BussLayer objBussLayer = new BussLayer();
        int LAPushErrorCode = 0;
        int UWDecisionResult = 0;
        string strLAPushErrorMsg = string.Empty;
        int Result = 0;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        // objCommonObj = new CommonObject();
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        string struserid = objChangeObj.userLoginDetails._UserID;
        // strUserGroup = objChangeObj.userLoginDetails._UserGroup;
        //strApplicationno = objCommonObj._ApplicationNo;
        strUWmode = strChannelType;
        UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
        Logger.Info(strApplicationno + "STAG 2 :PageName :Bpmuwmodule.cs // MethodeName :btnPost_Click" + System.Environment.NewLine);
        //DropDownList ddlUWDecesion = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWDecesion");
        //DropDownList ddlUWreason = (DropDownList)ContentPlaceHolder1.FindControl("ddlUWreason");
        //DropDownList ddlPostpone = (DropDownList)ContentPlaceHolder1.FindControl("ddlPeriod");
        //Label lblErrorDecisiondtls = (Label)ContentPlaceHolder1.FindControl("lblErrorDecisiondtls");

        string _strUWDecesion = ddlUWDecesion.SelectedValue;
        string _strUWPeriod = ddlPeriod.SelectedValue == "0" ? "" : ddlPeriod.SelectedValue;
        string _strDataValue = string.Empty;
        //string _strPolicyStatus = string.Empty;
        int intRetVal = -1;

        //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
        string reasonscode = Request.Form[ddlUWreason1.UniqueID];
        string UWReason2value = reasonscode.Split(',')[1];
        string UWReason2 = Request.Form[txtUWreason2.UniqueID];
        objComm.OnlineUWDecisionDetails_Save(objCommonObj._AppType, strApplicationno, ddlUWDecesion.SelectedItem.Text, ddlUWreason1.SelectedItem.Text, ddlUWDecesion.SelectedValue, ddlUWreason1.SelectedItem.Value, _strUWPeriod, struserid, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, objCommonObj._bpm_creationDate,
          objCommonObj._bpm_systemDate, objCommonObj._bpm_businessDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, UWReason2, UWReason2value, ref UWDecisionResult);
        //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
        if (UWDecisionResult != -1)
        {
            //lblErrorDecisiondtls.Text = "Decision details save successfully";
            objBuss.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strUWmode, objChangeObj, ref _ds, ref _dsPrevPol, ddlUWDecesion.SelectedValue, ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentRespons);
            if (LAPushErrorCode == 0)
            {
                lblErrorDecisiondtls.Text = "Decision Details Updated in Life Asia successfully";
                if (ddlUWDecesion.SelectedValue == "Approved" && LAPushErrorCode == 0)
                {
                    //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                    //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, "", "", ref UWDecisionResult);
                    strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                    objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                    //_strPolicyStatus = "IF";
                    objBussLayer.UpdatePolicyStatus(strApplicationno, "UW", struserid, ref intRetVal);
                }
                else if (ddlUWDecesion.SelectedValue == "Declined" && LAPushErrorCode == 0)
                {
                    //objComm.OnlineUWMISDecision_Save(strApplicationno, ddlUWDecesion.SelectedValue, objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._userBranch,
                    //    objCommonObj._AppType, struserid, objChangeObj.userLoginDetails._UserGroup, ddlUWreason.SelectedValue, ddlUWreason.SelectedItem.Text, ref UWDecisionResult);
                    strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                    objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                    //_strPolicyStatus = "DC";
                    objBussLayer.UpdatePolicyStatus(strApplicationno, "DC", struserid, ref intRetVal);
                }
                else if (ddlUWDecesion.SelectedValue == "Postponed" && LAPushErrorCode == 0)
                {
                    strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                    objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                    //_strPolicyStatus = "PO";
                    objBussLayer.UpdatePolicyStatus(strApplicationno, "PO", struserid, ref intRetVal);
                }
                else if (ddlUWDecesion.SelectedValue == "Withdrawn" && LAPushErrorCode == 0)
                {
                    strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                    objComm.OnlineApplicationchangeStatus(strApplicationno, struserid, strAppstatusKey, "", ref Result);
                    //_strPolicyStatus = "WD";
                    objBussLayer.UpdatePolicyStatus(strApplicationno, "WD", struserid, ref intRetVal);
                }
                /*added by shri on 06-07-17 to close page on success*/
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "show success message", "alert('Decision details save successfully');window.close();", true);
                /*end here*/
            }
            else
            {
                /*commented and added by shri on 06-07-17 to close page on success*/
                //lblErrorDecisiondtls.Text = "Decision Details Not Updated in Life Asia,Please Contact system admin";
                //lblErrorDecisiondtls.Focus();
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision Details Not Updated in Life Asia,Please Contact system admin');", true);
                /*end here*/
            }
        }
        else
        {
            /*commented and added by shri on 06-07-17 to close page on success*/
            //lblErrorDecisiondtls.Text = "Decision details Not Save ,Please Contact system admin";
            //lblErrorDecisiondtls.Focus();
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "show failure message", "alert('Decision details Not Save ,Please Contact system admin');", true);
            /*end here*/
        }

    }
    //protected void btnLoadingDtlsSave_Click(object sender, EventArgs e)
    //{

    //    string _strLoadRider = string.Empty;
    //    string _strLoadType = string.Empty;
    //    string _strLoadDesc = string.Empty;
    //    string _strLoadRsn1 = string.Empty;
    //    string _strRsn1Desc = string.Empty;
    //    string _strRsn2 = string.Empty;
    //    string _strRsn2Desc = string.Empty;
    //    string _strLoadPer = string.Empty;
    //    string _strRateAdjust = string.Empty;
    //    string _strLoadFlatMortality = string.Empty;
    //    string _strLoadletterPrint = string.Empty;
    //    int strFollowupResult = 0;
    //    objCommonObj = (CommonObject)ViewState["objCommonObj"];
    //    foreach (GridViewRow rowfollowup in gvLoadingDtls.Rows)
    //    {
    //        TextBox txtLoadRider = rowfollowup.FindControl("txtLoadRider") as TextBox;
    //        DropDownList ddlLoadType = rowfollowup.FindControl("ddlLoadType") as DropDownList;
    //        TextBox txtLoadDesc = rowfollowup.FindControl("txtLoadDesc") as TextBox;
    //        DropDownList ddlLoadRsn1 = rowfollowup.FindControl("ddlLoadRsn1") as DropDownList;
    //        TextBox txtRsn1Desc = rowfollowup.FindControl("txtRsn1Desc") as TextBox;
    //        DropDownList ddlRsn2 = rowfollowup.FindControl("ddlRsn2") as DropDownList;
    //        TextBox txtRsn2Desc = rowfollowup.FindControl("txtRsn2Desc") as TextBox;
    //        TextBox txtLoadPer = rowfollowup.FindControl("txtLoadPer") as TextBox;
    //        TextBox txtRateAdjust = rowfollowup.FindControl("txtRateAdjust") as TextBox;
    //        DropDownList ddlLoadFlatMortality = rowfollowup.FindControl("ddlLoadFlatMortality") as DropDownList;
    //        DropDownList ddlLoadletterPrint = rowfollowup.FindControl("ddlLoadletterPrint") as DropDownList;
    //        _strLoadRider = txtLoadRider.Text;
    //        _strLoadType = ddlLoadType.SelectedValue;
    //        _strLoadDesc = ddlLoadType.SelectedItem.Text;
    //        _strLoadRsn1 = ddlLoadRsn1.SelectedValue;
    //        _strRsn1Desc = txtRsn1Desc.Text;
    //        _strRsn2 = ddlRsn2.SelectedValue;
    //        _strRsn2Desc = txtRsn2Desc.Text;
    //        _strLoadPer = txtLoadPer.Text;
    //        _strRateAdjust = txtRateAdjust.Text;
    //        _strLoadFlatMortality = ddlLoadFlatMortality.SelectedValue;
    //        _strLoadletterPrint = ddlLoadletterPrint.SelectedValue;
    //        int LoadingResult = 0;
    //        objComm.OnlineLoadingDetails_Save(objCommonObj._ApplicationNo, objCommonObj._ApplicationNo, _strLoadType, _strLoadDesc, _strLoadRsn1, _strRsn2, "", "", "", _strRateAdjust, _strLoadFlatMortality, "", _strLoadletterPrint, "", objCommonObj._bpm_userID, objCommonObj._bpm_userName, objCommonObj._bpmgrp, objCommonObj._bpm_branchCode, objCommonObj._bpm_userBranch, objCommonObj._ProcessName, objCommonObj._ApplicationNo, ref LoadingResult);
    //    }



    //}

    protected void btnBankDtlsSave_Click(object sender, EventArgs e)
    {
        int BankResult = 0;

        //objCommonObj = (CommonObject)Session["objCommonObj"];
        lblErrorbankdtls.Text = string.Empty;
        Logger.Info(strApplicationno + " STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnBankDtlsSave_Click");
        objComm.OnlineBankDetails_Save(strApplicationno, txtBnkClientnumber.Text, txtBnkIfsccode.Text, txtBnkBankname.Text, txtBnkBranchname.Text, txtBnkBankaddress.Text, txtBnkBankaccno.Text, txtBnkClientname.Text, ref BankResult);
        if (BankResult != -1)
        {
            chkBankDtls.Checked = false;
            lblErrorbankdtls.Text = "Bank Details save successfully.";
        }
        else
        {
            lblErrorbankdtls.Text = "Bank Details Not Save ";
        }
    }

    protected void btnAppDtlsSave_Click(object sender, EventArgs e)
    {
        int AppResult = 0;
        int intServicesResultCount = 0, intTotalServiceCount = 3;
        string date = hdnRcdReq.Value;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        lblErrorAppDetailsBody.Text = lblErrorappdtls.Text = string.Empty;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        AppDtls objApplicationdetails = new AppDtls();
        objApplicationdetails._Backdate = Request.Form[txtRcdreq.UniqueID];
        objChangeObj.App_backdate = objApplicationdetails;

        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnAppDtlsSave_Click");
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
        lblErrorAppDetailsBody.Text = strLApushStatus;
        if (strLApushErrorCode == 0)
        {
            intServicesResultCount++;
            LoadDtls objLoad = new LoadDtls();
            objLoad._strProdcode = "";
            objChangeObj.Load_Loadingdetails = objLoad;
            lblErrorappdtls.Text += "Application Details  updated in Life asia";
            //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, "RATEUP", ref strLApushErrorCode, ref strLApushStatus);
            //if (strLApushErrorCode == 0)
            //{
            //    //lblErrorappdtls.Text = "lOADING Details Updated in LifeAsia successfully";
            //}
            FillLoadParam(ref objChangeObj);
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            lblErrorAppDetailsBody.Text = strLApushStatus;
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                intServicesResultCount++;
                //ServiceTax = Convert.ToDecimal(WebConfigurationManager.AppSettings["ServiceTax"]);
                //txtBackDateIntrest.Text = Convert.ToString(Math.Round(Convert.ToDecimal(_ds.Tables[0].Rows[0][1].ToString())+(Convert.ToDecimal(_ds.Tables[0].Rows[0][1].ToString()) * ServiceTax),MidpointRounding.AwayFromZero));
                txtBackDateIntrest.Text = _ds.Tables[0].Rows[0]["BackDateintrest"].ToString();

            }
            //lblErrorappdtls.Text += "Application Details Updated in LifeAsia successfully";
            objComm.OnlineApplicationDetails_Save(strPolicyNo, strApplicationno, "Yes", Request.Form[txtRcdreq.UniqueID], ddlBkdateReason.SelectedItem.Value, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, "UWSaralUnderwritting", strApplicationno, objChangeObj.userLoginDetails._UserID, strChannelType, txtBackDateIntrest.Text, ref AppResult);
            if (AppResult != -1)
            {
                //lblErrorappdtls.Text += "Application Details save and updated in life asia successfully";
                chkAppDtls.Checked = false;
            }
            else
            {
                //lblErrorappdtls.Text += "Application Details Not save,Please Contact system admin";
                chkAppDtls.Checked = false;
            }
            if (intServicesResultCount == intTotalServiceCount)
            {
                lblErrorappdtls.Text = "Backdating details updated Successfully Click to know more";
            }
        }
        else
        {
            lblErrorappdtls.Text += "Application Details Not updated in Life asia CLICK to know more";
            lblErrorAppDetailsBody.Text = strLApushStatus;
            // chkAppDtls.Checked = true;
            chkAppDtls.Checked = false;
        }

    }

    #endregion Page Events End.
    protected void gvRiderDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Logger.Info("STAG 5 :PageName :Uwdecision.aspx.CS // MethodeName :gvRiderDtls_RowDataBound" + System.Environment.NewLine);
        DataTable dt = (DataTable)ViewState["MandatoryRider"];
        CheckBox cbIsActive = (CheckBox)e.Row.FindControl("chkremoveRider");


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[2].Text.Contains("W"))
            //{

            TextBox txtridersa = e.Row.FindControl("txtRiderSumAssure") as TextBox;
            Label lblridercode = e.Row.FindControl("lblRiderCode") as Label;
            if (lblridercode.Text.Contains("W"))
            {
                if (ViewState["RiderSA_WP"] != null)
                {
                    txtridersa.Text = ViewState["RiderSA_WP"].ToString();
                }

            }

            //}
        }

        /*
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[0].Text.ToUpper() == "TRUE")
            {

                cbIsActive.Checked = true;
            }
            else
            {
                cbIsActive.Checked = false;
            }
        }
        */
    }
    protected void btnValidatepan_Click(object sender, EventArgs e)
    {
        try
        {
            //30.1 Begin of Changes; Pooja Shetye - [1133038]
            string ClientMode = "";
            String panno = txtPannumber.Text;
            ClientMode = objcomm.GetClientType(strApplicationno);
            bool IsvalidPAN = ValidatePANNumber(panno, ClientMode);

            if (IsvalidPAN == true)
            { //30.1 End of Changes; Pooja Shetye - [1133038]

                /*ID:18 Start*/
                chkPanDtls.Checked = false;
                /*ID:18 End*/
                int _PanResult = 0;

                //26.1 Begin of Changes; Bibekananda Nanda - [1103055]
                string strnsdlPanNumber = string.Empty;
                string strnsdlLastUpdDT = string.Empty;
                string strnsdlPanStatus = string.Empty;
                string strnsdlPanTitle = string.Empty;
                string strnsdlPanType = string.Empty;
                DateTime? NSDLPropLastUpdDT = null;

                //26.1 End of Changes; Bibekananda Nanda - [1103055]

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "DisplayPrgressShow();", true);
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                Logger.Info(strApplicationno + "STAG 6 :PageName :Uwdecision.aspx.CS // MethodeName :btnValidatepan_Click" + System.Environment.NewLine);
                // objComm.OnlinePancardDetails_Save(txtBnkClientnumber.Text, txtBnkClientname.Text, "", "", "", "", "", "", "", txtPannumber.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, "UWSaral", strChannelType, chkPanCopy.Checked, ref _PanResult, strnsdlPanStatus, strnsdlPanTitle, NSDLPropLastUpdDT, lblnsdlPanType.Text);
                //objComm.OnlinePancardDetails_Save(txtBnkClientnumber.Text, txtBnkClientname.Text, "", "", "", "", "", "", "", txtPannumber.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, "UWSaral", strChannelType, chkPanCopy.Checked, ref _PanResult);
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PANVALIDATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    // chkPanDtls.Checked = false;
                    int intRet = -1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "DisplayPrgress();", false);
                    gvPanResult.DataSource = _ds;
                    gvPanResult.DataBind();
                    btnPandtlsSave.CssClass = btnPandtlsSave.CssClass.Replace("HideControl", "");
                    //call pan details from db
                    DataSet _dsDbPayer = new DataSet();
                    objcomm.FetchClientInfoOnApplciationNo(ref _dsDbPayer, strApplicationno, "PAYER");
                    string strReturn = string.Empty;
                    chkIsPanValidate.Checked = IsPanValid(_ds, _dsDbPayer, ref strReturn);
                    txtPanDescription.Text = strReturn;
                    //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

                    strnsdlPanNumber = _ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    strnsdlLastUpdDT = _ds.Tables[0].Rows[0]["LastUpdateDate"].ToString();
                    strnsdlPanStatus = _ds.Tables[0].Rows[0]["PanStatus"].ToString();
                    strnsdlPanTitle = _ds.Tables[0].Rows[0]["PanTitle"].ToString();
                    //strnsdlPanType = _ds.Tables[0].Rows[0]["PanNumber"].ToString();


                    char c = strnsdlPanNumber[3];
                    if (c.ToString().ToUpper() == "P")
                    {
                        lblnsdlPanType.Text = "Personal";

                    }
                    else
                    {
                        lblnsdlPanType.Text = "Other Than Personal";
                    }

                    lblnsdlLastUpdDt.Visible = false;
                    lblLastUpdDT.Visible = false;
                    lblnsdlLastUpdDt.Text = "";
                    //lblnsdlLastUpdDt.Text = strnsdlLastUpdDT;


                    if (!String.IsNullOrEmpty(strnsdlLastUpdDT))
                    {
                        NSDLPropLastUpdDT = DateTime.ParseExact(strnsdlLastUpdDT, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    //26.1 End of Changes; Bibekananda Nanda - [1103055]


                    if (chkIsPanValidate.Checked)
                    {
                        lblPanIsValidated.Text = "Auto Matched";
                        chkIsPanValidate.Checked = true;
                        /*added by shri on 16 jan 18 to update is pan updated flag*/
                        ManagePanAdharFlagUpdation(strApplicationno, true, 1, ref intRet);
                        ChangeThumbStatus(imgPanVerified, true);
                        /*end here*/
                    }
                    else
                    {
                        lblPanIsValidated.Text = "Not Matched";
                        chkIsPanValidate.Checked = false;
                        ManagePanAdharFlagUpdation(strApplicationno, false, 1, ref intRet);
                        ChangeThumbStatus(imgPanVerified, false);
                    }
                    //chkIsPanValidate.Attributes["class"] = chkIsPanValidate.Attributes["class"].Replace(" HideControl",string.Empty);
                }
                else
                {
                    //chkPanDtls.Checked = false;
                }
                //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

                objComm.OnlinePancardDetails_Save(txtBnkClientnumber.Text, txtBnkClientname.Text, "", "", "", "", "", "", "", txtPannumber.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, "UWSaral", strChannelType, chkPanCopy.Checked, ref _PanResult, strnsdlPanStatus, strnsdlPanTitle, NSDLPropLastUpdDT, lblnsdlPanType.Text);

                objComm.OnlinePanDisplayDetails_GET(ref _dsPandtls, strApplicationno, strChannelType);
                string NSDLPanYear;
                string NSDLIsNSAP;

                NSDLPanYear = Convert.ToString(_dsPandtls.Tables[0].Rows[0]["PanYear"]);
                NSDLIsNSAP = Convert.ToString(_dsPandtls.Tables[0].Rows[0]["IsNSAP"]);

                if (NSDLPanYear == "0" && NSDLIsNSAP == "false")
                {
                    lblPanMsgProp.Visible = true;
                    lblPanMsg.Visible = true;
                    lblPanMsg.Text = "PAN card issued within 1 year, please process with due diligence";
                }
                else
                {
                    lblPanMsgProp.Visible = false;
                    lblPanMsg.Visible = false;
                    lblPanMsg.Text = "";
                }
                //26.1 End of Changes; Bibekananda Nanda - [1103055]

                divPanValidation.Attributes["class"] = divPanValidation.Attributes["class"].Replace(" HideControl", string.Empty);
                divPanManipulate.Attributes["class"] = divPanManipulate.Attributes["class"].Replace(" HideControl", string.Empty);
            }
            else
            {
                //30.1 Begin of Changes; Pooja Shetye - [1133038]
                ShowPopupMessage("Please enter valid PAN number - [4th character of PAN should match with client category. For Personal client it should be 'P', for Corporate client it should be H,A,,F,C,T,B,L,J,G.]");
                throw new Exception("UDE-Please enter valid PAN number - [4th character of PAN should match with client category. For Personal client it should be 'P', for Corporate client it should be H,A,,F,C,T,B,L,J,G.]");
                //30.1 End of Changes; Pooja Shetye - [1133038]
            }
        }
        catch (Exception)
        {

        }
        finally
        {
            updAutoUwDetails.Update();
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    protected void btnPandtlsSave_Click(object sender, EventArgs e)
    {
        /*ID:18 Start*/
        chkPanDtls.Checked = false;
        /*ID:18 End*/
        int _PanResult = 0;
        UWSaralServices.AmlDetails objAml = new UWSaralServices.AmlDetails();
        int strLAPushErrorcode = -1;
        string strLAPushStatus = "";
        lblErrorPanDetailsBody.Text = lblErrorpandtls.Text = string.Empty;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        Logger.Info(strApplicationno + "STAG 7 :PageName :Uwdecision.aspx.CS // MethodeName :btnPandtlsSave_Click" + System.Environment.NewLine);
        /*added by shri on 11 sept 17 to add is applied flag*/
        string strFirstName = string.Empty, strMiddleName = string.Empty, strLastName = string.Empty;

        //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

        string strPanStatus = string.Empty;
        string strPanTitle = string.Empty;
        string strLastUpdDate = string.Empty;

        //26.1 End of Changes; Bibekananda Nanda - [1103055]

        /*fetch first name middle name last name*/
        if (chkPanCopy.Checked)
        {
            int i = 3;
            if (gvPanResult.Rows.Count > 0)
            {
                foreach (GridViewRow rows in gvPanResult.Rows)
                {
                    strLastName = rows.Cells[i].Text;
                    strFirstName = rows.Cells[i + 1].Text;
                    strMiddleName = rows.Cells[i + 2].Text;

                    //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

                    strPanStatus = rows.Cells[2].Text;
                    strPanTitle = rows.Cells[6].Text;
                    strLastUpdDate = rows.Cells[7].Text;

                    //26.1 End of Changes; Bibekananda Nanda - [1103055]
                }
            }
            else
            {
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PANVALIDATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    strLastName = Convert.ToString(_ds.Tables[0].Rows[0][i]);
                    strFirstName = Convert.ToString(_ds.Tables[0].Rows[0][i + 1]);
                    strMiddleName = Convert.ToString(_ds.Tables[0].Rows[0][i + 2]);

                    //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

                    strPanStatus = Convert.ToString(_ds.Tables[0].Rows[0][2]);
                    strPanTitle = Convert.ToString(_ds.Tables[0].Rows[0][6]);
                    strLastUpdDate = Convert.ToString(_ds.Tables[0].Rows[0][7]);

                    //26.1 End of Changes; Bibekananda Nanda - [1103055]
                }
            }

        }


        //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

        DateTime? NSDLPropLastUpdDT = null;
        if (!String.IsNullOrEmpty(strLastUpdDate))
        {
            NSDLPropLastUpdDT = DateTime.ParseExact(strLastUpdDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        string strnsdlPanNumber = "";
        strnsdlPanNumber = txtPannumber.Text;
        if (strnsdlPanNumber != "")
        {
            char c = strnsdlPanNumber[3];
            if (c.ToString().ToUpper() == "P")
            {
                lblnsdlPanType.Text = "Personal";
            }
            else
            {
                lblnsdlPanType.Text = "Other Than Personal";
            }
        }

        /*end here*/

        objComm.OnlinePancardDetails_Save(txtBnkClientnumber.Text, strFirstName, strLastName, strMiddleName, string.Empty, lblPanIsValidated.Text, txtPanDescription.Text, "", txtPanComment.Text, txtPannumber.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, "UWSaral", strChannelType, (chkPanCopy.Checked), ref _PanResult,
            strPanStatus, strPanTitle, NSDLPropLastUpdDT, lblnsdlPanType.Text);

        //26.1 End of Changes; Bibekananda Nanda - [1103055]

        /*end here*/
        if (_PanResult != -1)
        {
            if (chkPanCopy.Checked)
            {
                divPanValidation.Attributes["class"] = divPanValidation.Attributes["class"].Replace("HideControl", string.Empty);
            }
            else
            {
                divPanValidation.Attributes["class"] = divPanValidation.Attributes["class"] + " HideControl";
            }
            lblErrorpandtls.Text = "Pan Details save successfully";
            objcomm.AMLDetails_Get(ref _ds, strApplicationno, strChannelType);
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in _ds.Tables[0].Rows)
                {


                    //36.1 Start of Changes; Sagar Thorave-[mfl00886]
                    DataSet ds12 = null;
                    objComm.Featch_AMLFLAG_Details(ref ds12, txtAppno.Text, "SaralUW");
                    string CLNTRSKIND = (ds12.Tables[0].Rows[0]["CLNTRSKIND"].ToString());

    
                    objAml.AMLPushService(strApplicationno, row, objChangeObj, ref strLAPushErrorcode, ref strLAPushStatus, CLNTRSKIND);
                    //36.1 Start of Changes; Sagar Thorave-[mfl00886]
                    if (strLAPushErrorcode == 0)
                    {
                        chkPanDtls.Checked = false;
                        btnPandtlsSave.CssClass = btnPandtlsSave.CssClass.Insert(0, "HideControl ");
                        lblErrorPanDetailsBody.Text = lblErrorpandtls.Text = "Pan Details Updated in Life Asia successfully";

                    }
                    else
                    {
                        chkPanDtls.Checked = false;
                        lblErrorpandtls.Text = "Pan Details Not Updated Successfully";
                        lblErrorPanDetailsBody.Text = strLApushStatus;
                    }
                }
            }
            else
            {
                chkPanDtls.Checked = false;
                lblErrorpandtls.Text = "Pan Details Not Updated";
                lblErrorJournalDetailsBody.Text = "Pan Details Not Fetched From DataBase";
            }
        }
        else
        {
            chkPanDtls.Checked = false;
            lblErrorpandtls.Text = "Pan Details Not Updated";
            lblErrorJournalDetailsBody.Text = "Pan Details Not Saved In DataBase";
        }
    }

    protected void btnProddtlsSave_Click(object sender, EventArgs e)
    {
        string strComboMonthlyPayout = string.Empty;
        string strProdMonthlyPayout = string.Empty;
        lblErrorProductDetailBody.Text = lblErrorproddtls.Text = "";
        gridPremCal_Product.DataSource = null;
        gridPremCal_Product.DataBind();
        //  objCommonObj = (CommonObject)Session["objCommonObj"];
        lblErrorproddtls.Text = string.Empty;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        Logger.Info(strApplicationno + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :btnProddtlsSave_Click" + System.Environment.NewLine);
        ProdDtls objprodchangevalue = new ProdDtls();

        objprodchangevalue._PolicyTerm = Request.Form[txtPolterm.UniqueID];
        objprodchangevalue._Premiumpayingterm = Request.Form[txtPrepayterm.UniqueID];
        objprodchangevalue._Sumassured = Request.Form[txtSumassure.UniqueID];
        objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
        strProdMonthlyPayout = objprodchangevalue._MonthlyPayoutBase = Request.Form[txtMonthlyPayoutBase.UniqueID];
        objprodchangevalue._ProdcodeBase = txtProdcode.Text;
        // objprodchangevalue._Amountinsis = txtSisamount.Text;
        objprodchangevalue._Basepremiumamount = Request.Form[txtBasepremium.UniqueID];
        objprodchangevalue._TotalPremiumamount = txtTotalpremium.Text;

        if (!string.IsNullOrEmpty(txtCombProdCode.Text))
        {
            objprodchangevalue._ProdcodeCombo = Request.Form[txtCombProdCode.UniqueID];
            objprodchangevalue._PolicyTermCombo = Request.Form[txtCombPolTerm.UniqueID];
            objprodchangevalue._PremiumpayingtermCombo = Request.Form[txtCombPayTerm.UniqueID];
            objprodchangevalue._SumassuredCombo = Request.Form[txtCombSumAssured.UniqueID];
            objprodchangevalue._BasepremiumamountCombo = txtCombPremAmount.Text;
            objprodchangevalue._TotalPremiumamountCombo = txtCombTotalPrem.Text;
            strComboMonthlyPayout = objprodchangevalue._MonthlyPayoutCombo = Request.Form[txtComboMonthlyPayout.UniqueID];
        }
        objChangeObj.Prod_policydetails = objprodchangevalue;


        #region Premium calculation Service call begin.
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);

        List<RiderInfo> lstRiderInfo = new List<RiderInfo>();
        Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstRiderInfo, _ds.Tables[0]);

        if (strLApushErrorCode == 0 && _ds.Tables.Count > 0)
        {
            lblErrorProductDetailBody.Text = "Premium calculation succeed";
            //clsName = divPremiumdetails.Attributes["class"].ToString();
            //divPremiumdetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));

            RiderInfo objrider = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtProdcode.Text)).SingleOrDefault();
            if (objrider != null)
            {
                txtBasepremium.Text = objprodchangevalue._Basepremiumamount = objrider.InstalmentPremiumAmt.ToString();
                txtTotalpremium.Text = objprodchangevalue._TotalPremiumamount = objrider.TotalPremiumAmount.ToString();
                txtServicetax.Text = objprodchangevalue._ServiceTax = objrider.ServiceTax.ToString();
                txtSumassure.Text = objprodchangevalue._Sumassured = objrider.SumAssured.ToString();
            }
            RiderInfo objComb = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtCombProdCode.Text)).SingleOrDefault();
            if (objComb != null)
            {
                txtCombPremAmount.Text = objprodchangevalue._BasepremiumamountCombo = objComb.InstalmentPremiumAmt.ToString();
                txtCombTotalPrem.Text = objprodchangevalue._TotalPremiumamountCombo = objComb.TotalPremiumAmount.ToString();
                txtCombServiceTax.Text = objprodchangevalue._ServiceTaxCombo = objComb.ServiceTax.ToString();
                txtCombSumAssured.Text = objprodchangevalue._SumassuredCombo = objComb.SumAssured.ToString();
                txtComboMonthlyPayout.Text = string.IsNullOrEmpty(strComboMonthlyPayout) ? "0" : strComboMonthlyPayout;
            }

            objChangeObj.Prod_policydetails = objprodchangevalue;
            Session["objprodchangevalue"] = objprodchangevalue;
            #region Call Contract Modification Service Begins.
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (strLApushErrorCode == 0)
            {
                lblErrorProductDetailBody.Text += ",Contract modification succeed";
                //objComm.OnlineProductDetails_Save(strChannelType, strApplicationno, strPolicyNo, txtProdcode.Text, txtSumassure.Text, txtPolterm.Text, txtPrepayterm.Text, ddlFrequency.SelectedValue, txtBasepremium.Text, txtTotalpremium.Text, txtBasepremium.Text, txtMonthlyPayoutBase.Text, ref _ProdResult);
                chkProductDtls.Checked = false;
                int _RiderResult = 0;
                lblErrorProductDetailBody.Text += ",Product Details save succeed";
                //Amit 08062017 Start

                //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, "RATEUP", ref strLApushErrorCode, ref strLApushStatus);
                //if (strLApushErrorCode == 0)
                //{
                //    lblErrorProductDetailBody.Text += ",Loading modification succeed";
                //}
                if (gvRiderDtls.Rows.Count > 0)
                {
                    foreach (GridViewRow rows in gvRiderDtls.Rows)
                    {
                        Label lblRiderCode = (Label)rows.FindControl("lblRiderCode");
                        TextBox txtRiderSumAssure = (TextBox)rows.FindControl("txtRiderSumAssure");
                        TextBox txtRiderPremium = (TextBox)rows.FindControl("txtRiderPremium");
                        objComm.OnlineRiderDetails_Save(strChannelType, strApplicationno, lblRiderCode.Text, txtRiderPremium.Text, txtRiderSumAssure.Text, ref _RiderResult);
                    }
                    if (_RiderResult != -1)
                    {
                        chkProductDtls.Checked = false;
                        lblErrorProductDetailBody.Text += ",Product Details save succeed";
                        lblErrorproddtls.Text = "Product Details Updated Successfully";
                    }
                    else
                    {
                        chkProductDtls.Checked = false;
                        lblErrorProductDetailBody.Text += "Product Details failed,Please Contact system admin";
                        lblErrorproddtls.Text = "Product Details Not Updated Successfully";
                    }
                }
                //Amit 08062017 End.

            }
            else
            {
                FillProductDetails(strApplicationno, strChannelType);
                chkProductDtls.Checked = false;
                lblErrorproddtls.Text = "Product Details Not Updated Successfully";
                lblErrorProductDetailBody.Text += ",Contract modification failed Error:" + strLApushStatus;
                //lblErrorProductDetailBody.Text += ",Contract modification failed,Please Contact system admin";

            }
            #endregion Call Contract Modification Service End.
        }

        //Amit 08062017 start.
        else if (strLApushErrorCode == 1)
        {
            chkProductDtls.Checked = false;
            lblErrorproddtls.Text = strLApushStatus;
            gridPremiumdetails.DataSource = null;
            gridPremiumdetails.DataBind();
            divPremiumdetails.Attributes.Add("class", "col-md-12 HideControl");
        }
        //Amit 08062017 end.
        else
        {
            chkProductDtls.Checked = false;
            lblErrorproddtls.Text = "Product Details Not Updated Successfully";
            lblErrorProductDetailBody.Text = "Premium calculation failed,Please Contact system admin";
            divPremiumdetails.Attributes.Add("class", "col-md-12 HideControl");
        }
        #endregion calculation service call end
        /*added by shri to show updated monthly payout*/
        txtMonthlyPayoutBase.Text = strProdMonthlyPayout;
        /*end here*/
    }

    protected void btnRiderDtlsSave_Click(object sender, EventArgs e)
    {
        int _RiderResult = 0;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        Logger.Info(objCommonObj._ApplicationNo + "STAG 9 :PageName :Uwdecision.aspx.CS // MethodeName :btnRiderDtlsSave_Click" + System.Environment.NewLine);
        foreach (GridViewRow rows in gvRiderDtls.Rows)
        {
            Label lblRiderCode = (Label)rows.FindControl("lblRiderCode");
            TextBox txtRiderSumAssure = (TextBox)rows.FindControl("txtRiderSumAssure");
            TextBox txtRiderPremium = (TextBox)rows.FindControl("txtRiderPremium");
            objComm.OnlineRiderDetails_Save(objCommonObj._ChannelType, objCommonObj._ApplicationNo, lblRiderCode.Text, txtRiderPremium.Text, txtRiderSumAssure.Text, ref _RiderResult);
        }

    }

    protected void btnFundDtlsSave_Click(object sender, EventArgs e)
    {
        int _FundResult = 0;
        lblErrorfunddtls.Text = string.Empty;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        Logger.Info(strApplicationno + "STAG 10 :PageName :Uwdecision.aspx.CS // MethodeName :btnFundDtlsSave_Click" + System.Environment.NewLine);
        DataSet _dsFundResponce = null;
        foreach (GridViewRow rows in gvFundDtls.Rows)
        {
            Label lblFundvalue = (Label)rows.FindControl("lblFundvalue");
            Label lblFundName = (Label)rows.FindControl("lblFundName");
            TextBox txtFundvalue = (TextBox)rows.FindControl("txtFundvalue");
            objComm.OnlineFundDetails(strChannelType, objCommonObj._AppType, strApplicationno, lblFundvalue.Text, lblFundName.Text, txtFundvalue.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref _FundResult);
            if (_FundResult != -1)
            {
                lblErrorfunddtls.Text = "Fund Details save successfully";
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _dsFundResponce, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                if (strLApushErrorCode == 0)
                {
                    lblErrorfunddtls.Text = "Fund Details updated in Life Asia  successfully";
                }
                else
                {
                    lblErrorfunddtls.Text = "Fund Details not updated in Life Asia  successfully";
                }
            }
            else
            {
                lblErrorfunddtls.Text = "Fund Details Not Save ,Please Contact system admin";
            }
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
    protected void ddlUWDecesion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            DataSet _dsDecisionCatrgory = null;
            DataSet _OthereReason = null;
            Logger.Info("STAG 11 :PageName :Uwdecision.aspx.CS // MethodeName :ddlUWDecesion_SelectedIndexChanged" + System.Environment.NewLine);

            FillSubMasterDetailsCategory(ref _dsDecisionCatrgory, "", "", "Category", "");
            ddlUWDecesioncategory.DataSource = _dsDecisionCatrgory.Tables[0];
            if (_dsDecisionCatrgory.Tables.Count > 0 && _dsDecisionCatrgory.Tables[0].Rows.Count > 0)
            {
                if (ddlUWDecesion.SelectedValue == "Postponed" || ddlUWDecesion.SelectedValue == "Declined")
                {
                  
                    divUWCategory.Visible = true;
                    divUWReason1.Visible = false;
                    divUWReason2.Visible = false;
                    Acceptance_Reason.Visible = false;
                    ddlUWDecesioncategory.DataTextField = "NAME";
                    ddlUWDecesioncategory.DataValueField = "VALUE";
                    ddlUWDecesioncategory.DataBind();
                    ddlUWDecesioncategory.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    FillSubMasterDetailsCategory(ref _OthereReason, ddlUWDecesion.SelectedValue, "", "Other", "");
                    if (ddlUWDecesion.SelectedValue == "proposal" || ddlUWDecesion.SelectedValue == "0")
                    {
                        Acceptance_Reason.Visible = false;
                        divUWReason1.Visible = false;
                        divUWReason2.Visible = false;
                        divUWCategory.Visible = false;
                        ddlUWreason1.Items.Clear();
                        divbtnaddnewrow.Visible = true;
                    }
                    else
                    {

                        ddlUWreason1.DataSource = _OthereReason.Tables[0];
                        Acceptance_Reason.Visible = false;
                        divUWReason1.Visible = true;
                        divUWReason2.Visible = false;
                        divUWCategory.Visible = false;
                        ddlUWreason1.Items.Clear();
                        divbtnaddnewrow.Visible = true;
                        //ddlUWreason1.DataTextField = "NAME";
                        //ddlUWreason1.DataValueField = "VALUE";
                        //ddlUWreason1.DataBind();
                    }


                    if (_OthereReason.Tables[0].Rows.Count > 0)
                    {

                        ddlUWreason1.Items.Clear();
                        Acceptance_Reason.Visible = false;
                        //ddlUWreason1.Items.Insert(0, new ListItem("Please select ", ""));
                        ddlUWreason1.SelectedValue = "";
                        foreach (DataRow row in _OthereReason.Tables[0].Rows)
                        {

                            if (row["value"] != null)
                                ddlUWreason1.DataValueField = row["value"].ToString();
                            else
                                ddlUWreason1.DataValueField = "";

                            if (row["name"] != null)
                                ddlUWreason1.DataTextField = row["name"].ToString();
                            else
                                ddlUWreason1.DataTextField = "";

                            ddlUWreason1.Items.Add(new ListItem(ddlUWreason1.DataTextField, ddlUWreason1.DataValueField));

                        }

                    }

                }

            }
            else
            {
                divUWCategory.Visible = false;
                Acceptance_Reason.Visible = false;
            }
           
            if (ddlUWDecesion.SelectedValue == "Postponed")
            {
                divPostponedPeriod.Visible = true;
                Acceptance_Reason.Visible = false;
                //clsName = divPostponedPeriod.Attributes["class"].ToString();
                // divPostponedPeriod.Attributes.Add("class", clsName.Replace(clsName, "col-md-6"));
            }
            //49.1 Begin of Changes; Sagar Thorave - [CR-7049]
            else if (ddlUWDecesion.SelectedValue == "Approved")
            {
              
                if (txtriskcat.Text == "High Risk" || txtriskcat.Text == "Very High Risk")
                {
                    DataSet ds = new DataSet();
                    FillAcceptanceMaster(ref ds);
                    Acceptance_Reason.Visible = true;
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ddlAcceptanceReason.Items.Clear();
                        //ddlUWreason1.Items.Insert(0, new ListItem("Please select ", ""));
                        ddlAcceptanceReason.SelectedValue = "";
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {

                            if (row["value"] != null)
                                ddlAcceptanceReason.DataValueField = row["value"].ToString();
                            else
                                ddlAcceptanceReason.DataValueField = "";

                            if (row["name"] != null)
                                ddlAcceptanceReason.DataTextField = row["name"].ToString();
                            else
                                ddlAcceptanceReason.DataTextField = "";

                            ddlAcceptanceReason.Items.Add(new ListItem(ddlAcceptanceReason.DataTextField, ddlAcceptanceReason.DataValueField));

                        }

                    }
                }
                //49.1 End of Changes; Sagar Thorave - [CR-7049]
                else
                {
                    Acceptance_Reason.Visible = false;
                }

            }
            else
            {
                divPostponedPeriod.Visible = false;
                //divPostponedPeriod.Attributes.Add("class", "HideControl");
            }
            if (gvExtLoadDetails.Rows.Count > 0)
            {
                
                    ddlUWreason1.SelectedValue = "A02";
                    
                   
                
            }


        }
        catch (Exception)
        {


        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    protected void ddlUWDecesioncategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet _dsDecisionReason = null;
        FillSubMasterDetailsCategory(ref _dsDecisionReason, ddlUWDecesion.SelectedValue, ddlUWDecesioncategory.SelectedItem.Text, "UWReason", "");
        ddlUWreason1.DataSource = _dsDecisionReason.Tables[0];
        if (_dsDecisionReason.Tables.Count > 0 && _dsDecisionReason.Tables[0].Rows.Count > 0)
        {
            divUWReason1.Visible = true;
            divUWReason2.Visible = true;
            ddlUWreason1.Items.Clear();
            txtUWreason2.Text = "";
            divbtnaddnewrow.Visible = true;
            //ddlUWreason1.DataTextField = "NAME";
            //ddlUWreason1.DataValueField = "VALUE";
            //ddlUWreason1.DataBind();
            if (_dsDecisionReason.Tables[0].Rows.Count > 0)
            {

                ddlUWreason1.Items.Clear();
                //ddlUWreason1.Items.Insert(0, new ListItem("Please select ", ""));
                ddlUWreason1.SelectedValue = "";
                foreach (DataRow row in _dsDecisionReason.Tables[0].Rows)
                {

                    if (row["value"] != null)
                        ddlUWreason1.DataValueField = row["value"].ToString();
                    else
                        ddlUWreason1.DataValueField = "";

                    if (row["name"] != null)
                        ddlUWreason1.DataTextField = row["name"].ToString();
                    else
                        ddlUWreason1.DataTextField = "";

                    ddlUWreason1.Items.Add(new ListItem(ddlUWreason1.DataTextField, ddlUWreason1.DataValueField));

                }

            }
        }
        else
        {
            divUWReason1.Visible = false;
            divUWReason2.Visible = false;
            divbtnaddnewrow.Visible = false;
        }


    }

    //protected void ddlUWreason1_SelectedIndexChanged(object sender, EventArgs e)
    //{


    //    txtUWreason2.Text = "";
    //    var ddlList = ddlUWreason1.Items.Cast<ListItem>().Where(n => n.Selected).ToList();
    //    int test = 0;
    //    for (int i = 0; i < ddlList.Count; i++)
    //    {
    //        if (i == 0)
    //        {
    //            ddlUWreason1.SelectedItem.Text = ddlList[i].Text.ToString();

    //        }
    //        else if (i == 1)
    //        {
    //            txtUWreason2.Text = ddlList[i].Text.ToString();
    //            txtUWreason2.CssClass = "form-control";
    //        }
    //        if (ddlList[i].Selected == true)
    //        {
    //            test += 1;
    //        }

    //    }
    //    if (test > 2 )
    //    {
    //        btnaddnewrow_Decision.Enabled = false;
    //        lblErrorDecisiondtls.Text = "Please Select Mimimum 1 and Maximum 2 Reasons .......!";
    //    }
    //    else
    //    {
    //        btnaddnewrow_Decision.Enabled = true;
    //        lblErrorDecisiondtls.Text = "";
    //    }


    //}

    protected void btnaddnewrow_Decision_Click(object sender, EventArgs e)
    {

        objUWDecision = new UWSaralDecision.BussLayer();
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnAddLoadingRow_Click");

        int intTrackingRet = -1;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "Decision_ADD_NEWROW", ref intTrackingRet);
        if (gvLoadingDtls.Rows.Count <= 0)
        {
            //ViewState["UWDecisionData"] = null;
            //Session["LoadingData"] = null;
        }
        PopulateDecisionDetails();
        btnaddnewrow_Decision.CssClass = "btn btn-primary lnkButton";
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        string strReqFollowupCode = "SIS", strReqDescription = "Please provide consent and fresh sis for extra premium in view of Medical/Non Medical Reasons. Sign Enclosed Letter."
        , strReqCategory = "Non Medical", strReqCriteria = "FIXED", strReqLifeType = "01", strReqStatus = "L"
           , srtReqRaisedBy = objChangeObj.userLoginDetails._UserID;
        AddNewRequirement(strReqFollowupCode, strReqDescription, strReqCategory, strReqCriteria, strReqLifeType, strReqStatus
            , srtReqRaisedBy);
        UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        DataSet _ds = new DataSet(), _dsPrevPol = new DataSet();
        int LAPushErrorCode = 0; string strLAPushErrorMsg = string.Empty; string strConsentResponse = string.Empty;
        _ds = null;
        ReplicaXml objReplicaXml = new ReplicaXml();
        int intResponse = -1;
        bool isdataSave = true;
        string strResponse = string.Empty;
        ManageDecision_Details(isdataSave, ref isdataSave, objReplicaXml, _ds, ref strResponse);

        //DataSet _dsPREMCAL = new DataSet();
        //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "proposal", ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);

        //UWPreIssueServiceCall(strApplicationno, strChannelType);
        //ddlUWDecesioncategory.SelectedValue = "0";
        // ddlUWreason1.Items.Clear();
        // txtUWreason2.Text = "";

        //End
        /*ID:18 Start*/
        //chkLoadingDtls.Checked = false;
        //updLoadingDetails.Update();
        ///*added for testing purpose*/
        //updProductDetails.Update();
        /*ID:18 End*/
    }

    public void PopulateDecisionDetails()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :PopulateLoadingDetails" + System.Environment.NewLine);
        DataTable _dtDecisiondata = new DataTable();
        DataRow _drRow = null;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        DecisionDtls objdecisiondtls = new DecisionDtls();
        //string uwsplitstr = Request.Form[ddlUWreason1.UniqueID];



        if (ViewState["UWDecisionData"] == null)
        {
            _dtDecisiondata.Columns.Add(new DataColumn("ddlUWDecesion", typeof(string)));
            _dtDecisiondata.Columns.Add(new DataColumn("ddlUWDecesioncategory", typeof(string)));
            _dtDecisiondata.Columns.Add(new DataColumn("ddlUWreason1", typeof(string)));
            _dtDecisiondata.Columns.Add(new DataColumn("txtUWreason2", typeof(string)));
            _dtDecisiondata.Columns.Add(new DataColumn("ddlPeriod", typeof(string)));


            _drRow = _dtDecisiondata.NewRow();
            _drRow[0] = ddlUWDecesion.SelectedItem.Text;
            if (ddlUWDecesioncategory.SelectedValue == null || ddlUWDecesioncategory.SelectedValue == "")
            {
                _drRow[1] = "";
            }
            else
            {
                _drRow[1] = ddlUWDecesioncategory.SelectedItem.Text;
            }


            _drRow[2] = ddlUWreason1.SelectedItem.Text; //= uwsplitstr.Split(',')[0];
            _drRow[3] = txtUWreason2.Text = Request.Form[txtUWreason2.UniqueID];
            if (ddlUWDecesion.SelectedItem.Text == "Postponed")
            {
                _drRow[4] = ddlPeriod.SelectedItem.Text;
            }
            else
            {
                _drRow[4] = "";
            }



            _dtDecisiondata.Rows.Add(_drRow);
            //  Session["UWDecisionData"] = _dtDecisiondata;
            ViewState["UWDecisionData"] = _dtDecisiondata.Copy();
            griddecision.DataSource = _dtDecisiondata;
            griddecision.DataBind();



        }
        else
        {
            //_dtLoaddata = (DataTable)Session["LoadingData"];

            _dtDecisiondata = (DataTable)ViewState["UWDecisionData"];
            _drRow = _dtDecisiondata.NewRow();
            _drRow[0] = ddlUWDecesion.SelectedItem.Text;
            if (ddlUWDecesioncategory.SelectedItem.Text == null || ddlUWDecesioncategory.SelectedValue == "0")
            {
                _drRow[1] = "";
            }
            else
            {
                _drRow[1] = ddlUWDecesioncategory.SelectedItem.Text;
            }

            _drRow[2] = ddlUWreason1.SelectedItem.Text;//= uwsplitstr.Split(',')[0];
            _drRow[3] = txtUWreason2.Text = Request.Form[txtUWreason2.UniqueID];
            if (ddlUWDecesion.SelectedItem.Text == "Postponed")
            {
                _drRow[4] = ddlPeriod.SelectedItem.Text;
            }
            else
            {
                _drRow[4] = ddlPeriod.SelectedValue;
            }

            _dtDecisiondata.Rows.Add(_drRow);
            // Session["UWDecisionData"] = _dtDecisiondata;
            ViewState["UWDecisionData"] = _dtDecisiondata.Copy();
            griddecision.DataSource = _dtDecisiondata;
            griddecision.DataBind();

        }
        if (griddecision.Rows.Count > 0)
        {

            //hdnIsConsentReq.Value = response.ToString();
            if (objChangeObj.Decision_details == null)
            {
                objChangeObj.Decision_details = new DecisionDtls();
            }

            objdecisiondtls._strProdcode = objChangeObj.Decision_details._strProdcode;
            objdecisiondtls.isConsentRequired = true;
            objChangeObj.Decision_details = objdecisiondtls;
            Session["objLoginObj"] = objChangeObj;
        }
        else
        {
            objChangeObj.Decision_details.isConsentRequired = false;
        }
        // Session["objLoginObj"] = objChangeObj;
    }


    protected void lnkDecisionRemoveRow_Click(object sender, EventArgs e)
    {

        try
        {
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :lnkDecisionRemoveRow_Click" + System.Environment.NewLine);
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;

            int rowID = gvRow.RowIndex + 1;
            if (ViewState["UWDecisionData"] != null)
            {
                int intResponse = 0;
                DataTable dt = (DataTable)ViewState["UWDecisionData"];
                if (dt.Rows.Count >= 1)
                {
                    DataRow dr = dt.Rows[rowID - 1];
                    DeleteExistingDecision(strApplicationno, ref intResponse);
                    if (gvRow.RowIndex <= dt.Rows.Count - 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowID - 1]);
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                    objChangeObj.Decision_details.isConsentRequired = false;
                    Session["objLoginObj"] = objChangeObj;
                }
                //Store the current data in ViewState for future reference
                ViewState["UWDecisionData"] = dt;
                //Re bind the GridView for the updated data
                lblErrorDecisiondtls.Text = "";
                if (intResponse > 0)
                {
                    Logger.Info(strApplicationno + "Save View state Loading data to database Success" + System.Environment.NewLine);
                    lblErrorDecisiondtls.Text += string.Empty;
                    lblErrorDecisiondtls.Text += " Decision Details Remove Successfully In DataBase";
                }
                else
                {
                    Logger.Info(strApplicationno + "Save View state Loading data to database Failed" + System.Environment.NewLine);
                    lblErrorDecisiondtls.Text += string.Empty;
                    lblErrorDecisiondtls.Text += " Decision Details Not Remove Successfully In DataBase";
                }
                griddecision.DataSource = dt;
                griddecision.DataBind();
                ddlUWDecesioncategory.Items.Clear();
                ddlUWreason1.Items.Clear();
                txtUWreason2.Text = "";
                ddlPeriod.SelectedValue = "0";
                divUWCategory.Visible = false;
                divUWReason1.Visible = false;
                divUWReason2.Visible = false;
                divbtnaddnewrow.Visible = false;
            }
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    private void DeleteExistingDecision(string strAppno, ref int response)
    {
        Commfun objCommfun = new Commfun();
        objCommfun.DeleteExistingDecision(strAppno, ref response);
    }

    //44.1 End of Changes; Bhaumik Patel - [CR - 3334]

    protected void ddlLoadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Logger.Info("STAG 12 :PageName :Uwdecision.aspx.CS // MethodeName :ddlLoadType_SelectedIndexChanged" + System.Environment.NewLine);
        DataSet _dsLoadVal = new DataSet();
        DataSet _dsRate = new DataSet();
        Boolean _strLoadPer = true;
        Boolean _strRateAdjustment = true;
        Boolean _strFlatMortality = true;
        txtLoadDesc.Text = ddlLoadType.SelectedItem.Text;
        ddlLoadType.CssClass = "form-control";
        ddlLoadType.Enabled = true;
        _dsLoadVal = (DataSet)ViewState["_dsFollowuo"];

        if (_dsLoadVal.Tables.Count > 0 && _dsLoadVal.Tables["ddlLoadTypeVal"].Rows.Count > 0)
        {
            DataTable _dt = _dsLoadVal.Tables["ddlLoadTypeVal"];
            DataTable tblFiltered = _dt.AsEnumerable()
          .Where(row => row.Field<String>("LoadType") == ddlLoadType.SelectedValue
                   && row.Field<String>("PlanId") == "TD"
                   && row.Field<String>("PlanType") == "Base Plan").CopyToDataTable();

            _strLoadPer = Convert.ToBoolean(Convert.ToInt32(tblFiltered.Rows[0]["LoadPer"]));
            _strRateAdjustment = Convert.ToBoolean(Convert.ToInt32(tblFiltered.Rows[0]["RateAdjustment"]));
            _strFlatMortality = Convert.ToBoolean(Convert.ToInt32(tblFiltered.Rows[0]["FlatMortality"]));
            if (_strLoadPer)
            {
                txtLoadPer.CssClass = "form-control Numeric";
            }
            else
            {
                txtLoadPer.CssClass = "form-control lblLable";
            }
             
            if (_strRateAdjustment)
            {
                //48.1 Begin of Changes; Bhaumik Patel - [CR-5855]
                commobj.OnlineFlatMortalityDetails_GET_RateAdjust("GetRateAdjustData_ByUSERID", strUserId, ref _dsRate);
                if(_dsRate.Tables.Count > 0 && _dsRate.Tables["RateAdjust"].Rows.Count > 0)
                {
                    txtRateAdjust.CssClass = "form-control Numeric";
                    rgvrateAdjust.MinimumValue = "0";
                    rgvrateAdjust.MaximumValue = _dsRate.Tables["RateAdjust"].Rows[0]["Flat_Extra"].ToString();
                    rgvrateAdjust.ErrorMessage = "Rate Adjustment Between 0 To " + _dsRate.Tables["RateAdjust"].Rows[0]["Flat_Extra"].ToString() + "";
                    rgvrateAdjust.ForeColor = System.Drawing.Color.Red;
                    UpdatePanel5.Update();
                }
                //48.1 END of Changes; Bhaumik Patel - [CR-5855]
            }
            else
            {
                txtRateAdjust.CssClass = "form-control lblLable";
            }

            if (_strFlatMortality)
            {
                ddlLoadFlatMortality.CssClass = "form-control";
                ddlLoadFlatMortality.Enabled = true;
            }
            else
            {
                ddlLoadFlatMortality.CssClass = "form-control lblLable";
                ddlLoadFlatMortality.Enabled = false;
                ddlLoadFlatMortality.SelectedIndex = 0;
            }
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            txtRsn1Desc.Text = "";
            txtRsn2Desc.Text = "";
            txtRsn3Desc.Text = "";
            txtRsn4Desc.Text = "";
            FillloadingReason(ddlLoadType.SelectedItem.Text);

            ddlLoadletterPrint.CssClass = "form-control";
            ddlLoadletterPrint.Enabled = true;
            UpdatePanel3.Update();
            UpdatePanel4.Update();
            UpdatePanel5.Update();
            updLoadType.Update();
            updAddloading.Update();
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
        }
    }

    //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
    public void FillloadingReason(String lodingtype)
    {
        chkredflag.Checked = false;
        DataTable _reason = new DataTable();
        objComm.OnlinelReason_Based_loadType_MasterDetails_GET(ref _ds, strChannelType, lodingtype, strApplicationno);
        BindRLoadType_Reaso_Details(_ds.Tables[0]);

    }

    public void BindRLoadType_Reaso_Details(DataTable _ds)
    {
        if (_ds.Rows.Count > 0)
        {

            ddlLoadRsn1.Items.Clear();
            ddlLoadRsn1.SelectedValue = "";
            foreach (DataRow row in _ds.Rows)
            {

                if (row["value"] != null)
                    ddlLoadRsn1.DataValueField = row["value"].ToString();
                else
                    ddlLoadRsn1.DataValueField = "";

                if (row["name"] != null)
                    ddlLoadRsn1.DataTextField = row["name"].ToString();
                else
                    ddlLoadRsn1.DataTextField = "";

                ddlLoadRsn1.Items.Add(new ListItem(ddlLoadRsn1.DataTextField, ddlLoadRsn1.DataValueField));

            }

        }



    }

    //protected void ddlLoadRsn1_SelectedIndexChanged(object sender, EventArgs e)
    //{


    //    txtRsn1Desc.Text = "";
    //    txtRsn2Desc.Text = "";
    //    txtRsn3Desc.Text = "";
    //    txtRsn4Desc.Text = "";

    //    var ddlList = ddlLoadRsn1.Items.Cast<ListItem>().Where(n => n.Selected).ToList();
    //    int test = 0;
    //    for (int i = 0; i < ddlList.Count; i++)
    //    {
    //        if (i < 3)
    //        {
    //            if (i == 0)
    //            {
    //                txtRsn1Desc.Text = ddlList[i].Text.ToString();
    //                ddlLoadRsn1.CssClass = "form-control";
    //            }
    //            else if (i == 1)
    //            {
    //                txtRsn2Desc.Text = ddlList[i].Text.ToString();
    //                ddlLoadRsn1.CssClass = "form-control";
    //            }
    //            else if (i == 2)
    //            {
    //                txtRsn3Desc.Text = ddlList[i].Text.ToString();
    //                ddlLoadRsn1.CssClass = "form-control";
    //            }
    //            else if (i == 3)
    //            {
    //                txtRsn4Desc.Text = ddlList[i].Text.ToString();
    //                ddlLoadRsn1.CssClass = "form-control";
    //            }
    //        }
    //        else
    //        {
    //            txtRsn1Desc.Text = "";
    //            txtRsn2Desc.Text = "";
    //            txtRsn3Desc.Text = "";
    //            txtRsn4Desc.Text = "";
    //        }


    //        test += 1;

    //    }
    //    if (test > 4)
    //    {
    //        btnAddLoadingRow.Enabled = false;
    //        btnAddLoadingRow.CssClass = "btn btn-primary lnkButton";
    //        lblErrorLoadingDetailBody.Text = "Please Select Mimimum 1 and Maximum 4 Reasons .......!";
    //    }
    //    else
    //    {
    //        btnAddLoadingRow.Enabled = true;
    //        btnAddLoadingRow.CssClass = "btn btn-primary lnkButton";
    //        lblErrorLoadingDetailBody.Text = "";
    //    }

    //    UpdatePanel3.Update();
    //    UpdatePanel4.Update();
    //    UpdatePanel5.Update();
    //    updAddloading.Update();

    //}


    //protected void ddlLoadRsn1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet _dsLoadReasonDiscp = new DataSet();
    //    Logger.Info("STAG 13 :PageName :Uwdecision.aspx.CS // MethodeName :ddlLoadRsn1_SelectedIndexChanged" + System.Environment.NewLine);
    //    ddlLoadRsn1.Enabled = true;
    //    FillSubMasterDetails(ref _dsLoadReasonDiscp, ddlLoadRsn1.SelectedValue, "LoadReason1", "");
    //    if (_dsLoadReasonDiscp.Tables.Count > 0 && _dsLoadReasonDiscp.Tables[0].Rows.Count > 0)
    //    {
    //        txtRsn1Desc.Text = _dsLoadReasonDiscp.Tables[0].Rows[0]["NAME"].ToString();
    //        ddlLoadRsn1.CssClass = "form-control";
    //    }
    //}
    //protected void ddlLoadRsn2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet _dsLoadReasonDiscp = new DataSet();
    //    Logger.Info("STAG 14 :PageName :Uwdecision.aspx.CS // MethodeName :ddlLoadRsn2_SelectedIndexChanged" + System.Environment.NewLine);
    //    ddlLoadRsn2.Enabled = true;
    //    FillSubMasterDetails(ref _dsLoadReasonDiscp, ddlLoadRsn2.SelectedValue, "LoadReason2", "");
    //    if (_dsLoadReasonDiscp.Tables.Count > 0 && _dsLoadReasonDiscp.Tables[0].Rows.Count > 0)
    //    {
    //        txtRsn2Desc.Text = _dsLoadReasonDiscp.Tables[0].Rows[0]["NAME"].ToString();
    //        ddlLoadRsn2.CssClass = "form-control";

    //    }
    //}
    //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
    protected void btncalculatePrem_Load_Click(object sender, EventArgs e)
    {
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "LOADING_PREM", ref intTrackingRet);
            string strComboMonthlyPayout = string.Empty;
            string strProdMonthlyPayout = string.Empty;
            lblErrorproddtls.Text = "";
            gridPremCal_Product.DataSource = null;
            gridPremCal_Product.DataBind();
            //  objCommonObj = (CommonObject)Session["objCommonObj"];
            lblErrorproddtls.Text = string.Empty;
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            Logger.Info(strApplicationno + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :btnProddtlsSave_Click" + System.Environment.NewLine);
            ProdDtls objprodchangevalue = new ProdDtls();
            gridPremCal_Product.DataSource = null;
            gridPremCal_Product.DataBind();
            objprodchangevalue._PolicyTerm = Request.Form[txtPolterm.UniqueID];
            objprodchangevalue._Premiumpayingterm = Request.Form[txtPrepayterm.UniqueID];
            objprodchangevalue._Sumassured = Request.Form[txtSumassure.UniqueID];
            objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
            strProdMonthlyPayout = objprodchangevalue._MonthlyPayoutBase = Request.Form[txtMonthlyPayoutBase.UniqueID];
            objprodchangevalue._ProdcodeBase = txtProdcode.Text;
            // objprodchangevalue._Amountinsis = txtSisamount.Text;
            objprodchangevalue._Basepremiumamount = Request.Form[txtBasepremium.UniqueID];
            objprodchangevalue._TotalPremiumamount = txtTotalpremium.Text;
            if (!string.IsNullOrEmpty(txtCombProdCode.Text))
            {
                objprodchangevalue._ProdcodeCombo = Request.Form[txtCombProdCode.UniqueID];
                objprodchangevalue._PolicyTermCombo = Request.Form[txtCombPolTerm.UniqueID];
                objprodchangevalue._PremiumpayingtermCombo = Request.Form[txtCombPayTerm.UniqueID];
                objprodchangevalue._SumassuredCombo = Request.Form[txtCombSumAssured.UniqueID];
                objprodchangevalue._BasepremiumamountCombo = txtCombPremAmount.Text;
                objprodchangevalue._TotalPremiumamountCombo = txtCombTotalPrem.Text;
                strComboMonthlyPayout = objprodchangevalue._MonthlyPayoutCombo = Request.Form[txtComboMonthlyPayout.UniqueID];
            }
            objChangeObj.Prod_policydetails = objprodchangevalue;

            //To Bring NonMedicalLoading and MedicalClass
            ReplicaXml objReplicaXml = new ReplicaXml();
            DataSet _ds = new DataSet();
            int intResponse = -1;
            bool isdataSave = true;
            string strResponse = string.Empty;
            // FillLoadParam(ref objChangeObj);

            PremiumCalculation(objReplicaXml, ref isdataSave, ref _ds, ref intResponse, ref strResponse);

            //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                //23nov2017
                //_ds.Tables[0].Columns.RemoveAt(2);
                //_ds.Tables[0].Columns.RemoveAt(2);
                gridPremCal_Loading.DataSource = _ds;
                gridPremCal_Loading.DataBind();
                chkProductDtls.Checked = false;
                lblErrorLoadingDetailBody.Text = lblErrorloadingdtls.Text = "Premium Calculation Success";
                /*ID:18 Start*/
                chkLoadingDtls.Checked = false;
                /*ID:18 End*/
                //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                txtRsn1Desc.Text = Request.Form[txtRsn1Desc.UniqueID];
                txtRsn2Desc.Text = Request.Form[txtRsn2Desc.UniqueID];
                txtRsn3Desc.Text = Request.Form[txtRsn3Desc.UniqueID];
                txtRsn4Desc.Text = Request.Form[txtRsn4Desc.UniqueID];
                //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
                //1.2 Begin of Changes  CR-28153
                hfdCalPremSA.Value = txtSumassure.Text.Trim();
                hfdCalPremFlag.Value = "1";
                //1.2 End of Changes  CR-28153
            }
            else
            {
                /*ID:18 Start*/
                chkLoadingDtls.Checked = false;

                /*ID:18 End*/
                FillProductDetails(strApplicationno, strChannelType);
                chkProductDtls.Checked = false;
                lblErrorloadingdtls.Text = "Premium Calculation Failed";
                lblErrorLoadingDetailBody.Text = strLApushStatus;
            }
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    protected void btncalculatePrem_Prod_Click(object sender, EventArgs e)
    {
        try
        {
            #region Commented Code


            //int _ProdResult = 0;
            //string strComboMonthlyPayout = string.Empty;
            //string strProdMonthlyPayout = string.Empty;
            //lblErrorproddtls.Text = "";
            //gridPremCal_Product.DataSource = null;
            //gridPremCal_Product.DataBind();
            ////  objCommonObj = (CommonObject)Session["objCommonObj"];
            //lblErrorproddtls.Text = string.Empty;
            //objChangeObj = (ChangeValue)Session["objLoginObj"];
            //Logger.Info(strApplicationno + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :btnProddtlsSave_Click" + System.Environment.NewLine);
            //ProdDtls objprodchangevalue = new ProdDtls();
            //gridPremCal_Product.DataSource = null;
            //gridPremCal_Product.DataBind();
            //objprodchangevalue._PolicyTerm = Request.Form[txtPolterm.UniqueID];
            //objprodchangevalue._Premiumpayingterm = Request.Form[txtPrepayterm.UniqueID];
            //objprodchangevalue._Sumassured = Request.Form[txtSumassure.UniqueID];
            //objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
            //strProdMonthlyPayout = objprodchangevalue._MonthlyPayoutBase = Request.Form[txtMonthlyPayoutBase.UniqueID];
            //objprodchangevalue._ProdcodeBase = txtProdcode.Text;
            //// objprodchangevalue._Amountinsis = txtSisamount.Text;
            //objprodchangevalue._Basepremiumamount = Request.Form[txtBasepremium.UniqueID];
            //objprodchangevalue._TotalPremiumamount = txtTotalpremium.Text;
            //if (!string.IsNullOrEmpty(txtCombProdCode.Text))
            //{
            //    objprodchangevalue._ProdcodeCombo = Request.Form[txtCombProdCode.UniqueID];
            //    objprodchangevalue._PolicyTermCombo = Request.Form[txtCombPolTerm.UniqueID];
            //    objprodchangevalue._PremiumpayingtermCombo = Request.Form[txtCombPayTerm.UniqueID];
            //    objprodchangevalue._SumassuredCombo = Request.Form[txtCombSumAssured.UniqueID];
            //    objprodchangevalue._BasepremiumamountCombo = txtCombPremAmount.Text;
            //    objprodchangevalue._TotalPremiumamountCombo = txtCombTotalPrem.Text;
            //    strComboMonthlyPayout = objprodchangevalue._MonthlyPayoutCombo = Request.Form[txtComboMonthlyPayout.UniqueID];
            //}
            //objChangeObj.Prod_policydetails = objprodchangevalue;
            ///*added by shri to rider details */
            //bool blnIsGreateThanSumAssured = true;
            //List<RiderInfo> LstRiderInfo = new List<RiderInfo>();
            //foreach (GridViewRow rowfollowup in gvRiderDtls.Rows)
            //{
            //    RiderInfo objRiderInfo = new RiderInfo();
            //    //define variable            
            //    CheckBox cbIsRider = (CheckBox)rowfollowup.FindControl("chkremoveRider");
            //    Label lblRiderName = (Label)rowfollowup.FindControl("lblRiderName");
            //    Label lblRiderCode = (Label)rowfollowup.FindControl("lblRiderCode");
            //    TextBox txtRiderSumAssured = (TextBox)rowfollowup.FindControl("txtRiderSumAssure");
            //    TextBox txtRiderTotalPremium = (TextBox)rowfollowup.FindControl("txtRiderPremium");
            //    TextBox txtServiceTax = (TextBox)rowfollowup.FindControl("txtriderservicetax");
            //    string[] strRiderSumAssured = txtRiderSumAssured.Text.Split(',');
            //    string[] strRiderTotalPremium = txtRiderTotalPremium.Text.Split(',');
            //    string[] strServiceTax = txtServiceTax.Text.Split(',');
            //    if (strRiderSumAssured.Length > 0)
            //    {
            //        txtRiderSumAssured.Text = strRiderSumAssured[strRiderSumAssured.Length - 1];
            //    }
            //    if (strRiderTotalPremium.Length > 0)
            //    {
            //        txtRiderTotalPremium.Text = strRiderTotalPremium[strRiderTotalPremium.Length - 1];
            //    }
            //    if (strServiceTax.Length > 0)
            //    {
            //        txtServiceTax.Text = strServiceTax[strServiceTax.Length - 1];
            //    }
            //    if (cbIsRider.Checked && string.IsNullOrEmpty(txtRiderSumAssured.Text))
            //    {
            //        lblRiderDetailsError.Text = "Enter sum assured for all the checked riders";
            //        return;
            //    }
            //    else if (cbIsRider.Checked && !string.IsNullOrEmpty(txtRiderSumAssured.Text) && Convert.ToDouble(txtRiderSumAssured.Text) < 1)
            //    {
            //        lblRiderDetailsError.Text = "Enter sum assured for all the checked riders";
            //        return;
            //    }
            //    //get value from it
            //    if (cbIsRider.Checked)
            //    {

            //    }


            //    objRiderInfo.RiderId = lblRiderCode.Text;
            //    objRiderInfo.SumAssured = string.IsNullOrEmpty(txtRiderSumAssured.Text) ? Convert.ToDouble("0.00") : Convert.ToDouble(txtRiderSumAssured.Text);
            //    LstRiderInfo.Add(objRiderInfo);
            //    //add data row to datatable            
            //    if (!string.IsNullOrEmpty(txtRiderSumAssured.Text))
            //    {
            //        if (Convert.ToDouble(txtRiderSumAssured.Text) > Convert.ToDouble(txtSumassure.Text))
            //        {
            //            blnIsGreateThanSumAssured = false;
            //            break;
            //        }
            //    }
            //}
            //if (blnIsGreateThanSumAssured)
            //{
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "show rider message", "alert('Rider Sum sssured should be less than total sum assured');", true);
            //    return;
            //}
            ///*end here*/
            //objChangeObj.RiderInfo = LstRiderInfo;
            //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus);
            //if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            //{
            //    _ds.Tables[0].Columns.RemoveAt(2);
            //    _ds.Tables[0].Columns.RemoveAt(2);
            //    gridPremCal_Product.DataSource = _ds;
            //    gridPremCal_Product.DataBind();
            //    chkProductDtls.Checked = false;
            //}
            //else
            //{
            //    FillProductDetails(strApplicationno, strChannelType);
            //    chkProductDtls.Checked = false;
            //    lblErrorproddtls.Text = "Premium Calculation Failed to know more click here";
            //    lblErrorProductDetailBody.Text = strLApushStatus;
            //}
            #endregion
            ReplicaXml objReplicaXml = new ReplicaXml();
            DataSet _ds = new DataSet();
            int intResponse = -1;
            bool isdataSave = true;
            string strResponse = string.Empty;
            objcomm = new Commfun();
            int intTrackingRet = -1;
            /*added by shri on 28 dec 17 to add tracking*/
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "PRODUCT_PREM", ref intTrackingRet);
            /*end here*/


            PremiumCalculation(objReplicaXml, ref isdataSave, ref _ds, ref intResponse, ref strResponse);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                //_ds.Tables[0].Columns.RemoveAt(2);
                //_ds.Tables[0].Columns.RemoveAt(2);
                gridPremCal_Product.DataSource = _ds;
                gridPremCal_Product.DataBind();
                chkProductDtls.Checked = false;
            }
            else
            {
                //FillProductDetails(strApplicationno, strChannelType);
                chkProductDtls.Checked = false;
                lblErrorproddtls.Text = "Premium Calculation Failed to know more click here";
                lblErrorProductDetailBody.Text = strLApushStatus;
            }
            /*added by suraj(suggested by saumya) for only product code E83 ,update waiver rider sumassured to rider section as per premium calculation */

            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    if (_ds.Tables[0].Rows[i]["RiderCtg"].ToString().Contains("WP") && (_ds.Tables[0].Rows[i]["ComponentCd"].ToString().Contains("E83") || _ds.Tables[0].Rows[i]["ComponentCd"].ToString().Contains("E84")))
                    {

                        DataSet _dsrider = new DataSet();
                        ViewState["RiderSA_WP"] = _ds.Tables[0].Rows[i]["SumAssured"].ToString();
                        objComm.Update_WP_RiderSA(ref _dsrider, strPolicyNo, strApplicationno, _ds.Tables[0].Rows[i]["SumAssured"].ToString(), strChannelType);

                        gvRiderDtls.DataSource = _dsrider.Tables[0];
                        gvRiderDtls.DataBind();

                    }
                }

            }
            /*end*/
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/

        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    protected void btnLoadingDtlsSave_Click(object sender, EventArgs e)
    {
        lblTotalLoadingPremium.Text = lblLoadingPremium.Text = string.Empty;
        chkLoadingDtls.Checked = false;
        DataTable _dtLoaddata = new DataTable();
        lblErrorloadingdtls.Text = string.Empty;
        int LoadingResult = 0;
        //objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        // objChangeObj = (ChangeValue)ViewState["LoadingData"];
        Logger.Info(strApplicationno + "STAG 15 :PageName :Uwdecision.aspx.CS // MethodeName :btnLoadingDtlsSave_Click" + System.Environment.NewLine);
        string _strLoadRiderName = string.Empty;
        string _strLoadType = string.Empty;
        string _strLoadDiscp = string.Empty;
        //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
        string _strLoadReason1 = string.Empty;
        string _strLoadReason2 = string.Empty;
        string _strLoadReason3 = string.Empty;
        string _strLoadReason4 = string.Empty;
        //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
        string _strLoadPercent = string.Empty;
        string _strRateAdjust = string.Empty;
        string _strFlatmortality = string.Empty;
        string _strLetterPrint = string.Empty;
        string _strRiderCode = string.Empty;
        string _strLoadCode = string.Empty;
        string _strReason1code = string.Empty;
        string _strReason2code = string.Empty;
        int LoadedPremiumA = 0;
        int TotalPremiumA = 0;
        int LoadedPremiumB = 0;
        int TotalPremiumB = 0;
        int TotalPremiumAB = 0;
        int LoadedPremiumAB = 0;
        _dtLoaddata = (DataTable)ViewState["LoadingData"];
        int i = 0;

        if (_dsLoadingDtls != null && _dsLoadingDtls.Tables.Count > 0 && _dsLoadingDtls.Tables[0].Rows.Count > 0)
        {
            string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];
            for (i = 0; i < _dtLoaddata.Rows.Count; i++)
            {
                _strLoadRiderName = _dtLoaddata.Rows[i]["RiderName"].ToString();
                _strLoadType = _dtLoaddata.Rows[i]["ddlLoadType"].ToString();
                _strLoadDiscp = _dtLoaddata.Rows[i]["LoadingDiscp"].ToString();
                //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                _strLoadReason1 = _dtLoaddata.Rows[i]["Loadrsn1"].ToString();
                _strLoadReason2 = _dtLoaddata.Rows[i]["Loadrsn2"].ToString();
                 _strLoadReason3 = _dtLoaddata.Rows[i]["Loadrsn3"].ToString();
               _strLoadReason4 = _dtLoaddata.Rows[i]["Loadrsn4"].ToString();
                //_strLoadReason1 = _dS.Tables[0].Rows[0]["LD_reason1"].ToString();
                //_strLoadReason2 = _dS.Tables[0].Rows[0]["LD_reason2"].ToString();
                //_strLoadReason3 = _dS.Tables[0].Rows[0]["LD_reason2"].ToString();
                //_strLoadReason4 = _dS.Tables[0].Rows[0]["LD_reason4"].ToString();
                //for (int k = 0; k < loadrsncode.Split(',').Length; k++)
                //{
                //    if (k == 0)
                //    {
                //        _strLoadReason1 = loadrsncode.Split(',')[k];
                //    }

                //    if (k == 1)
                //    {
                //        _strLoadReason2 = loadrsncode.Split(',')[k];
                //    }

                //    if (k == 2)
                //    {
                //        _strLoadReason3 = loadrsncode.Split(',')[k];
                //    }

                //    if (k == 3)
                //    {
                //        _strLoadReason4 = loadrsncode.Split(',')[k];
                //    }
                //}
                //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
                _strLoadPercent = _dtLoaddata.Rows[i]["txtLoadPer"].ToString();
                _strRateAdjust = _dtLoaddata.Rows[i]["txtRateAdjust"].ToString();
                _strFlatmortality = (_dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString() == "0") ? "" : _dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString();
                _strLetterPrint = _dtLoaddata.Rows[i]["LetterPrint"].ToString();
                _strRiderCode = _dtLoaddata.Rows[i]["RiderCode"].ToString();
                _strLoadCode = _dtLoaddata.Rows[i]["ddlLoadCode"].ToString();
                _strReason1code = _dtLoaddata.Rows[i]["ddlLoadRsn1Code"].ToString();
                _strReason2code = _dtLoaddata.Rows[i]["ddlLoadRsn2Code"].ToString();
                //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, _strLoadCode, _strLoadDiscp, _strLoadReason1, _strLoadReason2, _strLoadReason3, txtSumassure.Text, txtLoadPer.Text, _strRateAdjust, _strFlatmortality, txtPrepayterm.Text, _strLetterPrint, _strRiderCode, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, _strLoadReason4, ref LoadingResult);
                //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            }
            if (LoadingResult != -1)
            {
                chkLoadingDtls.Checked = false;
                //Session["LoadingData"] = null;
                ViewState["LoadingData"] = null;
                gvLoadingDtls.DataSource = null;
                gvLoadingDtls.DataBind();
                ddlLoadType.SelectedValue = "0";
                ddlLoadRsn1.SelectedValue = "0";
                // ddlLoadRsn2.SelectedValue = "0";
                ddlLoadFlatMortality.SelectedValue = "0";
                ddlLoadletterPrint.SelectedValue = "0";
                //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                txtLoadDesc.Text = "";
                txtRsn1Desc.Text = "";
                txtRsn2Desc.Text = "";
                txtRsn3Desc.Text = "";
                txtRsn4Desc.Text = "";
                ddlLoadRsn1.Items.Clear();
                //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
                txtLoadPer.Text = "";
                txtRateAdjust.Text = "";

                FillLoadingDetails(strApplicationno, strChannelType);
                lblErrorloadingdtls.Text = "Loading details save successfully";
                LoadDtls objLoad = new LoadDtls();
                objLoad._strProdcode = ddlLoadRiderCode.SelectedValue;
                objChangeObj.Load_Loadingdetails = objLoad;
                //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, "RATEUP", ref strLApushErrorCode, ref strLApushStatus);

                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                if (strLApushErrorCode == 0)
                {
                    if (_ds.Tables[0].Rows.Count > 0)
                    {
                        TotalPremiumAB = Convert.ToInt32(_ds.Tables[0].Rows[0]["TotalPremiumAB"].ToString().Replace(".00", ""));
                        LoadedPremiumAB = Convert.ToInt32(_ds.Tables[0].Rows[0]["LoadedPremiumAB"].ToString().Replace(".00", ""));
                        TotalPremiumA = Convert.ToInt32(_ds.Tables[0].Rows[0]["TotalPremiumA"].ToString().Replace(".00", ""));
                        LoadedPremiumA = Convert.ToInt32(_ds.Tables[0].Rows[0]["LoadedPremiumA"].ToString().Replace(".00", ""));
                        TotalPremiumB = Convert.ToInt32(_ds.Tables[0].Rows[0]["TotalPremiumB"].ToString().Replace(".00", ""));
                        LoadedPremiumB = Convert.ToInt32(_ds.Tables[0].Rows[0]["LoadedPremiumB"].ToString().Replace(".00", ""));

                        strLApushStatus = "";
                        //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus);
                        //if (strLApushErrorCode == 0 && _ds.Tables.Count > 0)
                        //{
                        //    lblErrorloadingdtls.Text = "Premium calculation succeed";
                        clsName = divPremiumDetailsLoading.Attributes["class"].ToString();
                        divPremiumDetailsLoading.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                        //    gridPremiumDetailsLoading.DataSource = _ds;
                        //    gridPremiumDetailsLoading.DataBind();
                        //}
                        lblLoadingPremium.Text = "Loading premium is : " + LoadedPremiumAB;
                        lblTotalLoadingPremium.Text = "Total premium is : " + TotalPremiumAB;
                        if (strChannelType.ToUpper() == "ONLINE")
                        {
                            objComm.OnlinepremiumLoadingDetails_Save(strApplicationno, LoadedPremiumAB, _strLoadDiscp, _strLoadCode, _strReason1code, "", _strLoadDiscp, LoadedPremiumA, LoadedPremiumB, TotalPremiumA, TotalPremiumB, "", ref strLApushErrorCode);
                        }
                        else if (strChannelType.ToUpper() == "OFFLINE")
                        {
                            //objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, ddlLoadRiderCode.SelectedItem.Text, "", "", ddlLoadRiderCode.SelectedValue, 0, Convert.ToDecimal(TotalPremiumAB), ddlLoadRsn2.SelectedItem.Text, 0, Convert.ToDecimal(LoadedPremiumA), Convert.ToDecimal(TotalPremiumAB * 4.5), 0, Convert.ToDecimal(LoadedPremiumA), 0, 0, 0, 0, 0, 0, 0, ref strLApushErrorCode);

                        }
                    }
                    /*added by shri on 22 july 17 to dont update text box*/
                    //txtTotalpremium.Text = TotalPremium.ToString();
                    //txtBasepremium.Text = LoadedPremium.ToString();
                    /*end here*/
                    btnAddLoadingRow.CssClass = "btn btn-primary HideControl";
                    //btnLoadingDtlsSave.CssClass = "btn-primary HideControl";
                    lblErrorloadingdtls.Text = "Loading Details Updated in Life Asia successfully";
                    FillLoadingDetails(strApplicationno, strChannelType);
                }
                else
                {
                    btnAddLoadingRow.CssClass = "btn btn-primary HideControl";
                    //btnAddLoadingRow.CssClass = "form-control";
                    lblErrorloadingdtls.Text = "Loading Details Not Updated in Life Asia,Please Contact system admin";
                }
            }
            else
            {
                lblErrorloadingdtls.Text = "Loading Details Not save successfully";
                btnAddLoadingRow.CssClass = "btn btn-primary lnkButton";
                //btnLoadingDtlsSave.CssClass = "btn-primary";
            }
        }

    }

    protected void gvLoadingDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Logger.Info("STAG 16 :PageName :Uwdecision.aspx.CS // MethodeName :gvLoadingDtls_RowDataBound" + System.Environment.NewLine);
    }

    protected void gvExtLoadDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable _ds = new DataTable();
        Logger.Info("STAG 17 :PageName :Uwdecision.aspx.CS // MethodeName :gvExtLoadDetails_RowDataBound" + System.Environment.NewLine);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DropDownList ddlLoadType = (DropDownList)e.Row.FindControl("ddlExstingLoadType");
            //BindMasterDetails(ddlLoadType);


            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //DropDownList ddlLoadRsn1 = (DropDownList)e.Row.FindControl("ddlExistingLoadRsn1");
            //BindMasterDetails(ddlLoadRsn1);

            // DropDownList ddlLoadRsn2 = (DropDownList)e.Row.FindControl("ddlExistingLoadRsn2");
            // BindMasterDetails(ddlLoadRsn2);
            //44.1 End of Changes; Bhaumik Patel - [CR - 3334]

            DropDownList ddlLoadFlatMortality = (DropDownList)e.Row.FindControl("ddlExistingLoadFlatMort");
            BindMasterDetails(ddlLoadFlatMortality);


            //FillDropDownList(ddl1, "followupcode", "Mst");
            _ds = (DataTable)ViewState["ExitingLoadingData"];
            if (_ds.Rows.Count > 0)
            {
                ////DropDownList ddlfollowupcode = (DropDownList)e.Row.FindControl("ddlfollowupcode");
                //ddlLoadType.SelectedValue = _ds.Rows[e.Row.RowIndex]["LaodingCode"].ToString();
                //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                Label ddlExstingLoadType = (Label)e.Row.FindControl("ddlExstingLoadType");
                ddlExstingLoadType.Text = _ds.Rows[e.Row.RowIndex]["LoadingType"].ToString();
                Label Rider = (Label)e.Row.FindControl("lblRiderName");
                Rider.Text = _ds.Rows[e.Row.RowIndex]["RiderName"].ToString();
                Label Reason1 = (Label)e.Row.FindControl("lblExistingLoadRsn1");
                Reason1.Text = _ds.Rows[e.Row.RowIndex]["LD_reason1"].ToString();
                Label Reason2 = (Label)e.Row.FindControl("lblExistingLoadRsn2");
                Reason2.Text = _ds.Rows[e.Row.RowIndex]["LD_reason2"].ToString();
                Label Reason3 = (Label)e.Row.FindControl("lblExistingLoadRsn3");
                Reason3.Text = _ds.Rows[e.Row.RowIndex]["LD_reason3"].ToString();
                Label Reason4 = (Label)e.Row.FindControl("lblExistingLoadRsn4");
                Reason4.Text = _ds.Rows[e.Row.RowIndex]["LD_reason4"].ToString();
                // _ds.Rows[e.Row.RowIndex]["Reason4"].ToString();
                //DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                //ddlLoadRsn1.SelectedValue = _ds.Rows[e.Row.RowIndex]["LoadReasoncode1"].ToString();
                //DropDownList ddlCriteria = (DropDownList)e.Row.FindControl("ddlCriteria");
                //ddlLoadRsn2.SelectedValue = _ds.Rows[e.Row.RowIndex]["LoadReasoncode2"].ToString();
                //44.1 End of Changes; Bhaumik Patel - [CR - 3334]

                ddlLoadFlatMortality.SelectedValue = _ds.Rows[e.Row.RowIndex]["FlatMortality"].ToString();
                Label Loadingper = (Label)e.Row.FindControl("lblExistingLoadPer");
                Loadingper.Text = _ds.Rows[e.Row.RowIndex]["Loading"].ToString();

                Label RateAdjust = (Label)e.Row.FindControl("lblExistingRateAdjust");
                RateAdjust.Text = _ds.Rows[e.Row.RowIndex]["RateAdjustment"].ToString();

            }

        }
    }

    protected void lnkBankDtlsRefresh_Click(object sender, EventArgs e)
    {

        chkBankDtls.Checked = false;
        //objCommonObj = (CommonObject)Session["objCommonObj"];
        FillBankDetails(strApplicationno, strChannelType);
    }

    protected void lnkAgentDtlsRefresh_Click(object sender, EventArgs e)
    {
        chkAgentDtls.Checked = false;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        FillAgentDetails(strApplicationno, strChannelType);
    }

    protected void lnkApplicationDtlsRefresh_Click(object sender, EventArgs e)
    {
        chkAppDtls.Checked = false;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        FillApplicationDetails(strApplicationno, strChannelType);
    }

    protected void lnkPreIssueDtlsRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "PRE_ISSUE", ref intTrackingRet);
            /*end here*/
            UWPreIssueServiceCall(strApplicationno, strChannelType);
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
        }
        catch (Exception ex)
        {
            Logger.Error(strApplicationno + "STAG 2 :PageName :UwDecisions.CS // MethodeName :lnkPreIssueDtlsRefresh_Click Error :" + System.Environment.NewLine + ex.ToString());
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    protected void lnkStpDtlsRefresh_Click(object sender, EventArgs e)
    {
        //objCommonObj = (CommonObject)Session["objCommonObj"];
        //UWSTPServiceCall(strApplicationno, strChannelType);
    }

    protected void lnkAwDtlsRefresh_Click(object sender, EventArgs e)
    {
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        //DashbordUWResultDynamic_Get(strApplicationno, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserGroup, strUWmode);
    }

    protected void lnkPanDtlsRefresh_Click(object sender, EventArgs e)
    {
        chkPanDtls.Checked = false;
        //objCommonObj = (CommonObject)Session["objCommonObj"];
        FillPanDetails(strApplicationno, strChannelType);
    }

    protected void lnkSarDtlsRefresh_Click(object sender, EventArgs e)
    {
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        FillTsmrMsarDetails(strApplicationno, strChannelType);
    }

    protected void lnkProductDtlsRefresh_Click(object sender, EventArgs e)
    {
        chkProductDtls.Checked = false;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        FillProductDetails(strApplicationno, strChannelType);
        FillRiderDetails(strApplicationno);
    }

    protected void lnkFundDelsRefresh_Click(object sender, EventArgs e)
    {
        //chkFunddtlsSave.Checked = false;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        //FillfundsDetails(strApplicationno, strChannelType);
    }

    protected void lnkPaymentDtlsRefresh_Click(object sender, EventArgs e)
    {
        // objCommonObj = (CommonObject)Session["objCommonObj"];        
        //FillPaymentsDetails(strApplicationno, strChannelType);
        FillReceiptDetails(strApplicationno, strChannelType);
    }

    protected void lnkReqDtlsRefresh_Click(object sender, EventArgs e)
    {
        chkReqDtls.Checked = false;
        //objCommonObj = (CommonObject)Session["objCommonObj"];
        FillRequirmentDetails(strApplicationno, strChannelType);
    }

    protected void lnkLoadingDtlsRefresh_Click(object sender, EventArgs e)
    {
        chkLoadingDtls.Checked = false;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        FillLoadingDetails(strApplicationno, strChannelType);
    }

    //protected void lnkUwCmntDtlsRefresh_Click(object sender, EventArgs e)
    //{
    //    chkCommentDtls.Checked = false;
    //    objCommonObj = (CommonObject)Session["objCommonObj"];
    //    FillCommentsDetails(strApplicationno, strChannelType, "General");
    //}

    protected void lnkDecisionDtlsRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ddlUWDecesion.SelectedIndex = 0;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
            //ddlUWreason1.SelectedIndex = 0;
            ddlUWDecesion.Enabled = true;
            //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    protected void lnkLoadRemoveRow_Click(object sender, EventArgs e)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :lnkLoadRemoveRow_Click" + System.Environment.NewLine);
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;

            int rowID = gvRow.RowIndex + 1;
            if (ViewState["LoadingData"] != null)
            {
                int intResponse = 0;
                DataTable dt = (DataTable)ViewState["LoadingData"];
                if (dt.Rows.Count >= 1)
                {
                    DataRow dr = dt.Rows[rowID - 1];
                    DeleteExistingLoading(strApplicationno, Convert.ToString(dr["RiderName"]), Convert.ToString(dr["ddlLoadType"]), ref intResponse);
                    if (gvRow.RowIndex <= dt.Rows.Count - 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowID - 1]);
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                    objChangeObj.Load_Loadingdetails.isConsentRequired = false;
                    Session["objLoginObj"] = objChangeObj;
                }
                //Store the current data in ViewState for future reference
                ViewState["LoadingData"] = dt;
                //Re bind the GridView for the updated data
                gvLoadingDtls.DataSource = null;
                gvLoadingDtls.DataBind();

                //44.1 Begin of Changes; Bhaumik Patel - [CR - 3334]
                updLoadingDetails.Update();
                txtRsn1Desc.Text = "";
                txtRsn2Desc.Text = "";
                txtRsn3Desc.Text = "";
                txtRsn4Desc.Text = "";
                //44.1 End of Changes; Bhaumik Patel - [CR - 3334]
            }
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    protected void lnkReqRemoveRow_Click(object sender, EventArgs e)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :lnkReqRemoveRow_Click" + System.Environment.NewLine);
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex + 1;
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1"];
            if (dt.Rows.Count >= 1)
            {

                if (gvRow.RowIndex <= dt.Rows.Count - 1)
                {
                    dt.Rows.Remove(dt.Rows[rowID - 1]);
                }
            }
            //Store the current data in ViewState for future reference
            ViewState["CurrentTable1"] = dt;
            //Re bind the GridView for the updated data
            gvRequirmentDetails.DataSource = dt;
            gvRequirmentDetails.DataBind();
            chkReqDtls.Checked = false;
        }

        //Set Previous Data on Postbacks
        SetPreviousData1();


    }

    protected void lnkExistingLoadRemoveRow_Click(object sender, EventArgs e)
    {
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "LOAD_REMOVE", ref intTrackingRet);
            /*end here*/
            string response = "";
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :lnkExistingLoadRemoveRow_Click" + System.Environment.NewLine);
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex + 1;
            if (ViewState["ExitingLoadingData"] != null)
            {
                DataTable dt = (DataTable)ViewState["ExitingLoadingData"];
                if (dt.Rows.Count >= 1)
                {
                    if (gvRow.RowIndex <= dt.Rows.Count - 1)
                    {
                        objCommonObj = (CommonObject)Session["objCommonObj"];
                        DeleteSpecificLoading(dt.Rows[rowID - 1], objCommonObj._ApplicationNo, objCommonObj._ChannelType, ref response);
                        //if (response != -1)
                        //{
                        dt.Rows.Remove(dt.Rows[rowID - 1]);
                        //}
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    clsName = divExistingloading.Attributes["class"].ToString();
                    divExistingloading.Attributes.Add("class", clsName.Replace(clsName, "col-md-12 HideControl"));
                }
                ViewState["ExitingLoadingData"] = dt;
                gvExtLoadDetails.DataSource = dt;
                gvExtLoadDetails.DataBind();
                BindLoadingDetails(dt);
            }
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    public void DeleteSpecificLoading(DataRow dr, string strApplicationno, string ChannelType, ref string response)
    {
        int result = -1;
        DataSet _ds = new DataSet();
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :DeleteSpecificLoading" + System.Environment.NewLine);
        objcomm.DeleteExistingLoading(dr, ref result);
        if (result != -1)
        {
            lblErrorExistingLoading.Text = "Loading deleted successfully";
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            LoadDtls objLoad = new LoadDtls();
            objLoad._strProdcode = dr["RiderName"].ToString();
            objChangeObj.Load_Loadingdetails = objLoad;
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (strLApushErrorCode == 0)
            {
                //if (_ds.Tables[0].Rows.Count > 0)
                //{

                //}
            }
        }
        else
        {
            lblErrorExistingLoading.Text = "Loading details not updated in Life Asia";
        }
    }

    /*added by shri on 21 june 17 to fetch basic client details*/
    private void FetchClientBasicDetails(ref DataSet _ds, string strApplnNo)
    {
        BussLayer objBussLayer = new BussLayer();
        objBussLayer.ClientBasicInfo_GET(ref _ds, strApplnNo);
    }

    protected void btnRefreshClientProfile_Click(object sender, EventArgs e)
    {
        try
        {
            FetchClientBasicDetails(ref _ds, strApplicationno);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                gvClientDetails.DataSource = _ds;
                gvClientDetails.DataBind();
                _ds.Clear();
            }
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    /*added by shri on 19 july 17 to manage rider info*/
    protected void btnSaveRiderInfo_Click(object sender, EventArgs e)
    {
        lblRiderDetailsError.Text = lblErrorRiderDetailsBody.Text = string.Empty;
        try
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];
            DataTable dtRiderInfo = new DataTable();
            DefineDataTable(ref dtRiderInfo);
            bool blnIsGreateThanSumAssured = true;
            foreach (GridViewRow rowfollowup in gvRiderDtls.Rows)
            {
                //define variable
                DataRow dr = dtRiderInfo.NewRow();
                CheckBox cbIsRider = (CheckBox)rowfollowup.FindControl("chkremoveRider");
                Label lblRiderName = (Label)rowfollowup.FindControl("lblRiderName");
                Label lblRiderCode = (Label)rowfollowup.FindControl("lblRiderCode");
                TextBox txtRiderSumAssured = (TextBox)rowfollowup.FindControl("txtRiderSumAssure");
                TextBox txtRiderTotalPremium = (TextBox)rowfollowup.FindControl("txtRiderPremium");
                TextBox txtServiceTax = (TextBox)rowfollowup.FindControl("txtriderservicetax");
                string[] strRiderSumAssured = txtRiderSumAssured.Text.Split(',');
                string[] strRiderTotalPremium = txtRiderTotalPremium.Text.Split(',');
                string[] strServiceTax = txtServiceTax.Text.Split(',');
                if (strRiderSumAssured.Length > 0)
                {
                    txtRiderSumAssured.Text = strRiderSumAssured[strRiderSumAssured.Length - 1];
                }
                if (strRiderTotalPremium.Length > 0)
                {
                    txtRiderTotalPremium.Text = strRiderTotalPremium[strRiderTotalPremium.Length - 1];
                }
                if (strServiceTax.Length > 0)
                {
                    txtServiceTax.Text = strServiceTax[strServiceTax.Length - 1];
                }
                if (cbIsRider.Checked && string.IsNullOrEmpty(txtRiderSumAssured.Text))
                {
                    lblRiderDetailsError.Text = "Enter sum assured for all the checked riders";
                    return;
                }
                else if (cbIsRider.Checked && !string.IsNullOrEmpty(txtRiderSumAssured.Text) && Convert.ToDouble(txtRiderSumAssured.Text) < 1)
                {
                    lblRiderDetailsError.Text = "Enter sum assured for all the checked riders";
                    return;
                }
                //get value from it
                dr[0] = strApplicationno;
                dr[1] = cbIsRider.Checked;
                dr[2] = lblRiderName.Text;
                dr[3] = lblRiderCode.Text;
                dr[4] = (string.IsNullOrEmpty(txtRiderSumAssured.Text)) ? "0.00" : txtRiderSumAssured.Text;
                dr[5] = (string.IsNullOrEmpty(txtRiderTotalPremium.Text)) ? "0.00" : txtRiderTotalPremium.Text;
                dr[6] = (string.IsNullOrEmpty(txtServiceTax.Text)) ? "0.00" : txtServiceTax.Text;
                dr[7] = string.Empty;
                dr[8] = txtProdcode.Text;
                dr[9] = txtPolterm.Text;
                dr[10] = txtPrepayterm.Text;
                dr[11] = txtSumassure.Text;
                //add data row to datatable
                dtRiderInfo.Rows.Add(dr);
                if (!string.IsNullOrEmpty(txtRiderSumAssured.Text))
                {
                    if (Convert.ToDouble(txtRiderSumAssured.Text) > Convert.ToDouble(txtSumassure.Text))
                    {
                        blnIsGreateThanSumAssured = false;
                        break;
                    }
                }
            }
            if (blnIsGreateThanSumAssured)
            {
                int intResponse = -1;
                //data table is not null then
                if (dtRiderInfo != null && dtRiderInfo.Rows.Count > 0)
                {
                    //save to data base
                    objcomm.ManageRiderInfo(strApplicationno, dtRiderInfo, strUserId, ref intResponse);
                    if (intResponse > 0)
                    {
                        //call premium calculation
                        string strIdentifier = "PREMIUMCAL";
                        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, strIdentifier, ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                        if (strLApushErrorCode == 0)
                        {
                            List<RiderInfo> lstRiderInfo = new List<RiderInfo>();
                            Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstRiderInfo, _ds.Tables[0]);

                            DataTable dtRider = _ds.Tables[0].Copy();
                            dtRider.Columns.Remove("ProductCode");
                            dtRider.Columns.Remove("MedicalLoadingPremium");
                            dtRider.Columns.Remove("NonMedicalLoadingPremium");
                            dtRider.Columns.Remove("TotalInstalmentPremium");
                            //dtRider.Columns.Remove("TotalPremiumAmount");
                            dtRider.Columns.Remove("SumAssured");
                            /*added by shri on 22 aug 17 to remove back date column from offline */
                            if (dtRider.Columns.Contains("BackDateintrest"))
                            {
                                dtRider.Columns.Remove("BackDateintrest");
                            }
                            /*end here*/
                            //if success
                            //update total premium service tax
                            //double dblServiceTax, dblTotalInstalmentPremium;
                            //dblServiceTax = Convert.ToDouble(_ds.Tables[0].Rows[0]["ServiceTax"]);
                            //dblTotalInstalmentPremium = Convert.ToDouble(_ds.Tables[0].Rows[0]["InstalmentPremiumAmt"]);
                            objcomm.UpdateInstalmentWithWithoutTax(strApplicationno, dtRider, ref intResponse);
                            //if (intResponse > 0)
                            //{
                            //    lblErrorRiderDetailsBody.Text = lblErrorRiderDetailsBody.Text + " Rider updated in db";
                            //}
                            //contract modification
                            strIdentifier = "CONTRACTMODIFICATION";
                            objChangeObj = (ChangeValue)Session["objLoginObj"];
                            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(objCommonObj._ApplicationNo, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, strIdentifier
                            , ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                            lblErrorRiderDetailsBody.Text = lblErrorRiderDetailsBody.Text + " Contract Modification " + strLApushStatus;
                            //rebind grid view
                            DataSet dsRiderInfo = new DataSet();
                            objcomm.FetchRiderGrid_GET(strApplicationno, objCommonObj._ChannelType, ref dsRiderInfo);
                            if (dsRiderInfo != null && dsRiderInfo.Tables.Count > 0 && dsRiderInfo.Tables[0].Rows.Count > 0)
                            {
                                gvRiderDtls.DataSource = dsRiderInfo;
                                gvRiderDtls.DataBind();
                            }
                            /*refill product details*/
                            chkProductDtls.Checked = false;
                            // objCommonObj = (CommonObject)Session["objCommonObj"];
                            FillProductDetails(strApplicationno, strChannelType);
                            FillRiderDetails(strApplicationno);
                            updProductDetails.Update();
                            /*end here*/
                            lblErrorRiderDetailsBody.Text = lblErrorRiderDetailsBody.Text + " Rider Info Updated Successfully";
                            lblRiderDetailsError.Text = "Rider Updated Successfully CLICK here to know more";
                        }
                        else
                        {
                            lblErrorRiderDetailsBody.Text = strLApushStatus;
                            lblRiderDetailsError.Text = "Rider Not Updated Successfully CLICK here to know more";
                        }
                    }
                    else
                    {
                        lblRiderDetailsError.Text = "Rider Updated Successfully CLICK here to know more";
                        lblErrorRiderDetailsBody.Text = "Rider Not Updated In Database";
                    }
                }
            }
            else
            {
                lblRiderDetailsError.Text = "Rider Sum Assured Cannot Be Greater Than Produce Sum Assured";
            }
        }

        catch (Exception ex)
        {
            lblRiderDetailsError.Text = "Rider Updated Successfully CLICK here to know more";
            lblErrorRiderDetailsBody.Text = ex.Message; ;
        }
    }

    private void DefineDataTable(ref DataTable dt)
    {
        dt.Columns.Add("ApplicationNo", typeof(string));
        dt.Columns.Add("IsActive", typeof(bool));
        dt.Columns.Add("RiderName", typeof(string));
        dt.Columns.Add("RiderCode", typeof(string));
        dt.Columns.Add("RiderSumAssured", typeof(string));
        dt.Columns.Add("RiderTotalPremium", typeof(string));
        dt.Columns.Add("ServiceTax", typeof(string));
        dt.Columns.Add("ProductType", typeof(string));
        dt.Columns.Add("ProductCode", typeof(string));
        dt.Columns.Add("ProdPoliyTerm", typeof(string));
        dt.Columns.Add("ProdPremPayingTerm", typeof(string));
        dt.Columns.Add("ProdSumAssured", typeof(string));
    }
    /*end here*/

    /*added by shri on 15 july 17 to recalculate premium details*/
    protected void btnClosePopupClientProfile_Click(object sender, EventArgs e)
    {
        FetchClientBasicDetails(ref _ds, strApplicationno);
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            gvClientDetails.DataSource = _ds;
            gvClientDetails.DataBind();
            _ds.Clear();
        }
        string strAppType = string.Empty;
        if (Session["objCommonObj"] != null)
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strAppType = objCommonObj._ChannelType;
        }
        if (Session["objLoginObj"] != null)
        {
            objChangeObj = (ChangeValue)Session["objLoginObj"];
        }
        //40.1  Begin of changes Sushant Devkate- mfl00905
        GetcheckISNRICustomer();
        //40.1 End of changes Sushant Devkate- mfl00905
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strAppType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);

        FillProductDetails(strApplicationno, strChannelType);
        FillRiderDetails(strApplicationno);
    }
    /*added by shri to fill journal details*/
    private void BindJournalDetails(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            txtJournalApplicatonNumber.Text = Convert.ToString(dt.Rows[0]["APPLICATION_NUMBER"]);
            txtJournalDocumentNo.Text = Convert.ToString(dt.Rows[0]["DOCUMENT_NUMBER"]);
            divJournalDetails.Attributes.Add("Class", "col-md-12");
        }
        else
        {
            divJournalDetails.Attributes.Add("Class", "col-md-12 HideControl");
        }
    }

    /*added by shri on 23 oct 17 to manage grid*/
    private void ManageUwDecisionGrid(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            if (1 != Convert.ToInt16(dt.Rows[0]["DecisionGrid"]))
            {
                if (ddlUWDecesion.Items.FindByValue("proposal") != null)
                {
                    ddlUWDecesion.Items.FindByValue("proposal").Selected = true;
                    ddlUWDecesion.Enabled = false;
                }
            }
            //pending state
            //if (Convert.ToString(dt.Rows[0]["FE_STATUS"]).Equals("PENDING"))
            //{
            //    ListItem objListItem = ddlUWDecesion.Items.FindByValue("proposal");

            //    ddlUWDecesion.Items.Clear();
            //    ddlUWDecesion.Items.Add(objListItem);
            //}
        }
    }
    /*end here*/
    protected void lnkBtnRefreshRiderInfo_Click(object sender, EventArgs e)
    {
        lblRiderDetailsError.Text = string.Empty;
        //rebind grid view
        DataSet dsRiderInfo = new DataSet();
        objCommonObj = (CommonObject)Session["objCommonObj"];
        objcomm.FetchRiderGrid_GET(strApplicationno, objCommonObj._ChannelType, ref dsRiderInfo);
        if (dsRiderInfo != null && dsRiderInfo.Tables.Count > 0 && dsRiderInfo.Tables[0].Rows.Count > 0)
        {
            gvRiderDtls.DataSource = dsRiderInfo;
            gvRiderDtls.DataBind();
        }
    }

    protected void ddlCommonStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in gvRequirmentDetails.Rows)
        {
            DropDownList ddlStatus = gvRow.FindControl("ddlStatus") as DropDownList;
            ddlStatus.SelectedValue = ddlCommonStatus.SelectedValue;
        }
        clsName = gvRequirmentDetails.CssClass.Replace("lblLable", "");
        gvRequirmentDetails.CssClass = clsName;
        ddlCommonStatus.Enabled = true;
        clsName = ddlCommonStatus.CssClass.Replace("lblLable", "");
        ddlCommonStatus.CssClass = clsName;

    }

    protected void btnMandateDtlsSave_Click(object sender, EventArgs e)
    {
        string strSpace = " ";
        try
        {
            MandateDetails objMandateDetails = new MandateDetails();
            objMandateDetails.ApplicationNo = strApplicationno;
            objMandateDetails.AccountType = txtMandAccountType.Text;
            objMandateDetails.AccountNumber = txtMandAccountno.Text;
            objMandateDetails.AutoPay = ddlAutoPaytype.SelectedValue;
            if (Session["objLoginObj"] != null)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                objMandateDetails.objLoginUserDetails = objChangeObj.userLoginDetails;
            }
            /*ADDED BY SHRI FOR MANDATE SI */

            if (ddlAutoPaytype.SelectedValue.ToUpper() == "SI")
            {
                objMandateDetails.CreditCardNo = txtCreditcardno.Text;
                objMandateDetails.CreditCardType = txtcreditcardType.Text;
                objMandateDetails.ValidUpto = txtValidupto.Text;
                objMandateDetails.MicroCode = string.Empty;
                objMandateDetails.BankName = string.Empty;
                objMandateDetails.BranchName = string.Empty;
                objMandateDetails.MandateDate = (string.IsNullOrEmpty(txtMandateSigndate.Text)) ? Convert.ToDateTime("01-01-0001") : Convert.ToDateTime(txtMandateSigndate.Text);
                objMandateDetails.AccountHolderName = txtHolderName.Text;
            }
            else
            {
                objMandateDetails.MicroCode = txtMandMicrocode.Text;
                objMandateDetails.BankName = txtMandBankName.Text;
                objMandateDetails.BranchName = txtMandBranchName.Text;
                objMandateDetails.MandateDate = (string.IsNullOrEmpty(txtMandateassigndate.Text)) ? Convert.ToDateTime("01-01-0001") : Convert.ToDateTime(txtMandateassigndate.Text);
                objMandateDetails.AccountHolderName = txtMandAccountholder.Text;
            }
            /*END HERE*/

            int intRetVal = -1;
            objcomm.ManageMandate(objMandateDetails, ref intRetVal);
            if (intRetVal > 0)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                if (Session["objCommonObj"] != null)
                {
                    objCommonObj = (CommonObject)Session["objCommonObj"];
                }
                string strAppType = objCommonObj._ChannelType;
                DataSet _dsMandateResp = new DataSet();
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strAppType, objChangeObj, ref _dsMandateResp, ref _dsPrevPol, "BANKENQ", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                lblErrorMandate.Text = strLApushStatus;
                if (strLApushErrorCode == 0)
                {
                    //contract modification                   
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strAppType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                    if (strLApushErrorCode == 0)
                    {
                        lblErrorMandate.Text = lblErrorMandate.Text + strSpace + "Contract Modified Successfully";
                    }
                }
            }
            HideShowMandate();
        }
        catch (Exception)
        {
            HideShowMandate();
            lblErrorMandate.Text = lblErrorMandate.Text + strSpace + "Try again later";
        }
        chkMandate.Checked = false;
    }
    protected void txtMandMicrocode_TextChanged(object sender, EventArgs e)
    {
        FillDetailsFromMicrCode(txtMandMicrocode.Text);
    }

    private void FillMandateDetails(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            txtMandAccountType.Text = Convert.ToString(dt.Rows[0]["ACCOUNT_TYPE"]);
            txtMandAccountno.Text = Convert.ToString(dt.Rows[0]["ACCOUNT_NUMBER"]);
            txtHolderName.Text = txtMandAccountholder.Text = Convert.ToString(dt.Rows[0]["ACCOUNT_HOLDER_NAME"]);
            txtMandateSigndate.Text = txtMandateassigndate.Text = Convert.ToString(dt.Rows[0]["MANDATE_DATE"]);
            //txtSiMicrCode.Text = txtMandMicrocode.Text = Convert.ToString(dt.Rows[0]["MICRO_CODE"]);
            //txtSiBankName.Text = txtMandBankName.Text = Convert.ToString(dt.Rows[0]["BANK_NAME"]);
            //txtSiBranchCode.Text = txtMandBranchName.Text = Convert.ToString(dt.Rows[0]["BRANCH_NAME"]);
            txtCreditcardno.Text = Convert.ToString(dt.Rows[0]["CREDIT_CARD_NO"]);
            txtcreditcardType.Text = Convert.ToString(dt.Rows[0]["CREDIT_CARD_TYPE"]);
            txtValidupto.Text = Convert.ToString(dt.Rows[0]["VALID_DATE"]);
            txtMandMicrocode.Text = Convert.ToString(dt.Rows[0]["MICRO_CODE"]);
            txtMandBankName.Text = Convert.ToString(dt.Rows[0]["BANK_NAME"]);
            txtMandBranchName.Text = Convert.ToString(dt.Rows[0]["BRANCH_NAME"]);
            /*Added by Suraj on 30/10/2018 as suggest by amit*/
            //txtIFSC.Text = Convert.ToString(dt.Rows[0]["IFSC"]);
            /*Added by Suraj on 13/07/2020 as suggest by darshak/Giridhar for CPG 2.0*/
            txtIFSC.Text = Convert.ToString(dt.Rows[0]["IFSC"]);

            txtPreferredDate.Text = Convert.ToString(dt.Rows[0]["PREFERRED_DATE"]);
            txtAmount.Text = Convert.ToString(dt.Rows[0]["MANDAMT"]);
            /*End*/
            ddlAutoPaytype.ClearSelection();
            if (ddlAutoPaytype.Items.FindByValue(Convert.ToString(dt.Rows[0]["AUTO_PAY_TYPE"])) != null)
            {
                ddlAutoPaytype.Items.FindByValue(Convert.ToString(dt.Rows[0]["AUTO_PAY_TYPE"])).Selected = true;
            }
            HideShowMandate();

        }
    }
    protected void ddlCommentsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillCommentsDetails(strApplicationno, strChannelType, ddlCommentsearch.SelectedValue);
    }
    protected void btnSaveJournal_Click(object sender, EventArgs e)
    {
        try
        {
            lblErrorJournalDetailsBody.Text = lblJournalMessage.Text = string.Empty;
            if (Session["objCommonObj"] != null)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
            }
            string strAppType = objCommonObj._ChannelType;
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "JOURNAL", ref intTrackingRet);
            /*end here*/
            Journal objJournal = new Journal();
            objJournal.ApplicationNumber = txtJournalApplicatonNumber.Text;
            objJournal.DocumentNumber = txtJournalDocumentNo.Text;
            objJournal.Password = txtJournalPassword.Text;

            if (Session["objLoginObj"] != null)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
            }
            objChangeObj.Journal = objJournal;
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strAppType, objChangeObj, ref _ds, ref _dsPrevPol, "JOURNAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (strLApushErrorCode == 0)
            {
                lblErrorJournalDetailsBody.Text = "Authenticated Successfully : " + strLApushStatus;
                lblJournalMessage.Text = "Authenticated Successfully to know more click here";
                lblJournalMessage.CssClass = "LableSuccess";
            }
            else
            {
                lblErrorJournalDetailsBody.Text = "Authentication Failed : " + strLApushStatus;
                lblJournalMessage.Text = "Authentication Failed";
            }
            chkJournal.Checked = false;
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    protected void txtSiMicrCode_TextChanged(object sender, EventArgs e)
    {
        //FillDetailsFromMicrCode(txtSiMicrCode.Text);        
    }
    private void FillDetailsFromMicrCode(string strMicrCode)
    {
        DataSet _ds = new DataSet();
        objComm.OnlineBankdetailsfromMicrocode_GET(ref _ds, strMicrCode, strApplicationno);
        if (_ds != null && _ds.Tables[0].Rows.Count > 0)
        {
            //txtSiBankName.Text = txtMandBankName.Text = _ds.Tables[0].Rows[0]["BankName"].ToString();
            //txtSiBranchCode.Text = txtMandBranchName.Text = _ds.Tables[0].Rows[0]["BankBranchName"].ToString();            
            txtMandBankName.Text = _ds.Tables[0].Rows[0]["BankName"].ToString();
            txtMandBranchName.Text = _ds.Tables[0].Rows[0]["BankBranchName"].ToString();
        }
        HideShowMandate();
    }
    private void HideShowMandate()
    {
        /*hide show mandate si and ecs*/
        divMandate.Attributes["class"] = divMandate.Attributes["class"] + " HideControl";
        divMandatesi.Attributes["class"] = divMandatesi.Attributes["class"] + " HideControl";
        divMandateecs.Attributes["class"] = divMandateecs.Attributes["class"] + " HideControl";
        if (ddlAutoPaytype.SelectedValue == "0" || ddlAutoPaytype.SelectedValue == "NO")
        {
            divMandate.Attributes["class"] = divMandate.Attributes["class"] + " HideControl";
        }
        if (ddlAutoPaytype.SelectedValue == "SI")
        {
            divMandatesi.Attributes["class"] = divMandatesi.Attributes["class"].Replace("HideControl", string.Empty);
            divMandate.Attributes["class"] = divMandate.Attributes["class"].Replace("HideControl", string.Empty);
        }
        else if (ddlAutoPaytype.SelectedValue == "ECS")
        {
            divMandateecs.Attributes["class"] = divMandateecs.Attributes["class"].Replace("HideControl", string.Empty);
            divMandate.Attributes["class"] = divMandate.Attributes["class"].Replace("HideControl", string.Empty);
        }
        else if (ddlAutoPaytype.SelectedValue == "ENACH")
        {
            divMandateecs.Attributes["class"] = divMandateecs.Attributes["class"].Replace("HideControl", string.Empty);
            divMandate.Attributes["class"] = divMandate.Attributes["class"].Replace("HideControl", string.Empty);
        }
    }

    protected void lnkRefreshJournal_Click(object sender, EventArgs e)
    {
        chkJournal.Checked = false;
        FillJournalkDetails(strApplicationno, strChannelType);
    }

    public void FillJournalkDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillJournalkDetails" + System.Environment.NewLine);
        objComm.OnlineJournalDetails_GET(ref _dsJournal, strApplicationno, ChannelType);
        BindJournalDetails(_dsJournal.Tables[0]);
    }

    private bool IsPanValid(DataSet _dsService, DataSet _dsDb, ref string strReturn)
    {

        int intCounter = 0;
        if (_dsService != null && _dsService.Tables.Count > 0 && _dsService.Tables[0].Rows.Count > 0 && _dsDb != null && _dsDb.Tables.Count > 0 && _dsDb.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToString(_dsService.Tables[0].Rows[0]["LastName"]).ToLower().Equals(Convert.ToString(_dsDb.Tables[0].Rows[0]["LAST_NAME"]).ToLower()))
            {
                intCounter++;
                strReturn = "Last Name Matched";
            }
            else
            {
                strReturn = "Last Name Not Matched";
            }
            if (Convert.ToString(_dsService.Tables[0].Rows[0]["FirstName"]).ToLower().Equals(Convert.ToString(_dsDb.Tables[0].Rows[0]["FIRST_NAME"]).ToLower()))
            {
                intCounter++;
                strReturn = strReturn + " First Name Matched";
            }
            else
            {
                strReturn = strReturn + " First Name Not Matched";
            }
            if (intCounter == 2)
            {
                return true;
            }
        }
        return false;
    }
    protected void btnCommonEvent_Command(object sender, CommandEventArgs e)
    {
        //if (Session["objLoginObj"] != null)
        //{
        //    objChangeObj = (ChangeValue)Session["objLoginObj"];
        //    int intRetVal = -1;
        //    objcomm.MaintainApplicationLog(strApplicationno, e.CommandName, string.Empty, objChangeObj.userLoginDetails._UserID, ref intRetVal);
        //}
    }
    protected void btnDedupeClient_Click(object sender, EventArgs e)
    {
        int intTrackingRet = -1;
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "UW_DEDUPE", ref intTrackingRet);
            /*end here*/
            BussLayer objBusinessLayer = new BussLayer();
            DataSet _dsDbLA = new DataSet();
            objcomm.FetchClientInfoOnApplciationNo(ref _dsDbLA, strApplicationno, "LA");
            if (_dsDbLA != null && _dsDbLA.Tables.Count > 0 && _dsDbLA.Tables[0].Rows.Count > 0)
            {
                string strFirstName,
                        strLastName
                        , strGender
                        , strDob;
                //set values
                strFirstName = Convert.ToString(_dsDbLA.Tables[0].Rows[0]["FIRST_NAME"]);
                strLastName = Convert.ToString(_dsDbLA.Tables[0].Rows[0]["LAST_NAME"]);
                strGender = Convert.ToString(_dsDbLA.Tables[0].Rows[0]["GENDER"]);
                strDob = Convert.ToString(_dsDbLA.Tables[0].Rows[0]["DOB"]);
                objBusinessLayer.DedupeSearch_GET(ref _ds, strFirstName, strLastName, (string.IsNullOrEmpty(strDob) ? string.Empty : Convert.ToDateTime(strDob).ToString("MM-dd-yyyy")), (strGender.Equals("MALE")) ? 'M' : 'F', string.Empty);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    dgUwDedupe.DataSource = _ds;
                    dgUwDedupe.DataBind();
                    divDgClientDedupe.Attributes["class"] = divDgClientDedupe.Attributes["class"].Replace(" HideControl", string.Empty);
                }
                else
                {
                    dgUwDedupe.DataSource = null;
                    dgUwDedupe.DataBind();
                    divDgClientDedupe.Attributes["class"] = divDgClientDedupe.Attributes["class"] + " HideControl";
                }
            }

            //strRet = FillDedupeDetails(_ds);
        }
        catch (Exception)
        {


        }
        finally
        {
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {

    }

    private void BindRiskParameter(DataTable dt)
    {
        #region 39.1 Begin Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]
        bool ISTermProduct = false;
        ISTermProduct = new Commfun().GetIsTermProduct(txtProdcode.Text.Trim());
        #endregion 39.1 End Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]

        if (dt != null && dt.Rows.Count > 0)
        {
            txtBMI.Text = Convert.ToString(dt.Rows[0]["DATA"]);
            if (Convert.ToInt32(dt.Rows[1]["DATA"]) == 0)
            {
                chkRiskParamIsSmoker.Checked = false;
            }
            else
            {
                #region 39.1 Begin Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]
                if (ISTermProduct)//Term Product 
                {
                    chkSaralRiskIsSmoker.Enabled = false;
                }
                #endregion 39.1 End Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]

                chkRiskParamIsSmoker.Checked = true;
            }
            /*ID:9 START*/
            txtSaralRiskBmi.Text = Convert.ToString(dt.Rows[0]["DATA"]);
            txtHeight.Text = Convert.ToString(dt.Rows[0]["HEIGHT"]);
            txtWeight.Text = Convert.ToString(dt.Rows[0]["WEIGHT"]);
            if (Convert.ToInt32(dt.Rows[1]["DATA"]) == 0)
            {
                chkSaralRiskIsSmoker.Checked = false;
            }
            else
            {
                #region 39.1 Begin Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]
                if (ISTermProduct)//Term Product 
                {
                    chkSaralRiskIsSmoker.Enabled = false;
                }
                #endregion 39.1 End Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]

                chkSaralRiskIsSmoker.Checked = true;
            }
            /*ID:9 END*/

            #region 38.1 Begin Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]
            if (ISTermProduct == false) //Non Term Product 
            {
                chkSaralRiskIsSmoker.Enabled = false;
            }
            #endregion 38.1 End Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]
        }
    }
    /*added by shri on 24 dec 17 to show excel risk parameter*/
    private void BindExcelRiskParameter(DataTable dt)
    {
        /*ID:9 START*/
        if (Convert.ToString(dt.Rows[0][0]).Equals("EXCEL_UPLOAD"))
        {
            /*ID:9 END*/

            if (dt != null && dt.Rows.Count > 0)
            {
                txtRiskParametersCombination.ToolTip = txtRiskParametersCombination.Text = Convert.ToString(dt.Rows[0]["PARAMETERS_COMBINATION"]);
                txtRiskUnderwritingDueDiligenceRequired.ToolTip = txtRiskUnderwritingDueDiligenceRequired.Text = Convert.ToString(dt.Rows[0]["UNDERWRITING_DUE_DILIGENCE_REQUIRED"]);
                txtRiskScore.ToolTip = txtRiskScore.Text = Convert.ToString(dt.Rows[0]["RISK_SCORE"]);
                txtRiskSuggestiveRequirement.ToolTip = txtRiskSuggestiveRequirement.Text = Convert.ToString(dt.Rows[0]["SUGGESTIVE_REQUIREMENT"]);
                txtRiskRemarks.ToolTip = txtRiskRemarks.Text = Convert.ToString(dt.Rows[0]["RISK_REMARKS"]);
                txtRiskBranchName.ToolTip = txtRiskBranchName.Text = Convert.ToString(dt.Rows[0]["BRANCH_NAME"]);
                //designing based on value



                if (Convert.ToInt32(txtRiskScore.Text) >= 75)
                {

                    txtRiskScore.BackColor = System.Drawing.Color.Red;
                    txtRiskScore.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    txtRiskScore.BackColor = System.Drawing.Color.Green;
                    txtRiskScore.ForeColor = System.Drawing.Color.White;
                }
                if (txtRiskParametersCombination.Text.Contains("Parameters") && Convert.ToInt32(txtRiskParametersCombination.Text.Substring(txtRiskParametersCombination.Text.Length - 1)) > 3)
                {
                    /*ID:19 START*/
                    //FillWarningMessage("8");
                    //DisplaySaralWarningMessage();
                    /*ID:19 END*/
                    txtRiskParametersCombination.BackColor = System.Drawing.Color.Red;
                    txtRiskParametersCombination.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    txtRiskParametersCombination.BackColor = System.Drawing.Color.Green;
                    txtRiskParametersCombination.ForeColor = System.Drawing.Color.White;
                }
                /*COMMENTED AND ADDED BY SHRI ON 30 DEC TO SHOW CHANNEL NAME */
                txtRiskChannel.Text = Convert.ToString(dt.Rows[0]["CHANNEL_NAME"]);

                /*END HERE*/
            }
            /*ID:9 START*/
        }
        else if (Convert.ToString(dt.Rows[0][0]).Equals("SARAL_RISK"))
        {
            //txtSaralRiskParamComb.ToolTip = txtSaralRiskParamComb.Text = Convert.ToString(dt.Rows[0]["PARAMETERS_COMBINATION"]);
            //txtSaralRiskUwDueDiligenceReq.ToolTip = txtSaralRiskUwDueDiligenceReq.Text = Convert.ToString(dt.Rows[0]["UNDERWRITING_DUE_DILIGENCE_REQUIRED"]);
            txtSaralRiskScore.ToolTip = txtSaralRiskScore.Text = Convert.ToString(dt.Rows[0]["RISK_SCORE"]);
            txtSaralRiskSuggestiveReq.ToolTip = txtSaralRiskSuggestiveReq.Text = Convert.ToString(dt.Rows[0]["SUGGESTIVE_REQUIREMENT"]);
            //txtSaralRiskRemark.ToolTip = txtSaralRiskRemark.Text = Convert.ToString(dt.Rows[0]["RISK_REMARKS"]);
            txtSaralRiskBranchName.ToolTip = txtSaralRiskBranchName.Text = Convert.ToString(dt.Rows[0]["BRANCH_NAME"]);
            //designing based on value
            txtSaralRiskChannel.Text = Convert.ToString(dt.Rows[0]["CHANNEL_NAME"]);

            if (!string.IsNullOrWhiteSpace(txtSaralRiskScore.Text) && Convert.ToInt32(txtSaralRiskScore.Text) >= 75)
            {

                txtSaralRiskScore.BackColor = System.Drawing.Color.Red;
                txtSaralRiskScore.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                txtSaralRiskScore.BackColor = System.Drawing.Color.Green;
                txtSaralRiskScore.ForeColor = System.Drawing.Color.White;
            }

            /*ID:21 START*/

            txtENYScore.Text = Convert.ToString(dt.Rows[0]["ENY_SCORE"]);
            if (!string.IsNullOrEmpty(txtENYScore.Text))
            {
                /*ID:1 START*/
                //if (Convert.ToInt32(txtENYScore.Text) >= 72)
                if (Convert.ToInt32(txtENYScore.Text) > 111)/*ID:1 END*/
                {
                    txtENYScore.BackColor = System.Drawing.Color.Red;
                    txtENYScore.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    txtENYScore.BackColor = System.Drawing.Color.Green;
                    txtENYScore.ForeColor = System.Drawing.Color.White;
                }
            }
            /*ID:21 END*/
            //if (txtSaralRiskParamComb.Text.Contains("Parameters") && Convert.ToInt32(txtSaralRiskParamComb.Text.Substring(txtSaralRiskParamComb.Text.Length - 1)) > 3)
            //{
            //    /*ID:19 START*/
            //    //FillWarningMessage("8");
            //    //DisplaySaralWarningMessage();

            //    /*ID:19 END*/
            //    txtSaralRiskParamComb.BackColor = System.Drawing.Color.Red;
            //    txtSaralRiskParamComb.ForeColor = System.Drawing.Color.White;
            //}
            //else
            //{
            //    txtSaralRiskParamComb.BackColor = System.Drawing.Color.Green;
            //    txtSaralRiskParamComb.ForeColor = System.Drawing.Color.White;
            //}

            /*WarningId is comming from warning msg API ,below code should be in page load
            string WarningId = string.Empty;
            string[] Warningsplit = WarningId.Split(',');
            foreach (string  item in Warningsplit)
            {

            }
            */

            /*ID:19 START*/
            DataSet _dsstate = new DataSet();
            DataSet _dsIsClient = new DataSet();
            DataSet _dsPrevstatus = new DataSet();
            objcomm.FetchBranchState(ref _dsstate, strApplicationno);
            string strbranchstate = string.Empty;
            string strcuststate = string.Empty;
            string strCustPincode = string.Empty;
            string strBranchPincode = string.Empty;

            if (_dsstate.Tables.Count > 0)
            {
                if (_dsstate.Tables[0].Rows.Count > 0)
                {
                    strbranchstate = Convert.ToString(_dsstate.Tables[0].Rows[0]["STATE"]).ToUpper().TrimEnd();
                    strBranchPincode = Convert.ToString(_dsstate.Tables[0].Rows[0]["BranchPincode"]).ToUpper().TrimEnd();
                    strBranchPincode = new string(strBranchPincode.Take(3).ToArray());
                }
                if (_dsstate.Tables[1].Rows.Count > 0)
                {
                    strcuststate = Convert.ToString(_dsstate.Tables[1].Rows[0]["ADR_state"]).ToUpper().TrimEnd();
                    strCustPincode = Convert.ToString(_dsstate.Tables[1].Rows[0]["CustPincode"]).ToUpper().TrimEnd();
                    strCustPincode = new string(strCustPincode.Take(3).ToArray());
                }
            }

            //string[] strBranchName = { "New Delhi Pitampura", "EZone Rohini New Delhi", "Gwalior", "Jaipur", "Bangalore", "Guwahati", "Nashik", "Hyderabad", "Gorakhpur", "Bhubaneshwar", "Baroda", "Ahmedabad", "Bhopal", "Aligarh" };
            //for (int i = 0; i < strBranchName.Length; i++)
            //{
            //_dsstate.Tables[2].Rows.Count == 0 this condition is for high risk city is exist

            if (!string.IsNullOrEmpty(txtdistance.Text) && _dsstate != null && _dsstate.Tables.Count > 0)
            {
                //added by suraj on 07-02-2019 for If Login branch is BAG or JAI or BAR or AHM and Channel is Direct and Distance is above 50KM/Other state then msg display
                if (((Convert.ToDouble(txtdistance.Text) > 50 || (!strbranchstate.Equals(strcuststate) && (!strCustPincode.Equals("121") && !strCustPincode.Equals("122") && !strCustPincode.Equals("110") && !strCustPincode.Equals("201")) && (!strBranchPincode.Equals("121") && !strBranchPincode.Equals("122") && !strBranchPincode.Equals("110") && !strBranchPincode.Equals("201")))) && _dsstate.Tables[2].Rows.Count > 0 && (Convert.ToString(_dsstate.Tables[2].Rows[0]["BranchCode"]).Equals("BAG") || Convert.ToString(_dsstate.Tables[2].Rows[0]["BranchCode"]).Equals("JAI") || Convert.ToString(_dsstate.Tables[2].Rows[0]["BranchCode"]).Equals("BAR") || Convert.ToString(_dsstate.Tables[2].Rows[0]["BranchCode"]).Equals("AHM")) && Convert.ToString(txtSaralRiskChannel.Text).Equals("Direct Sales")))
                {
                    FillWarningMessage("64");
                    DisplaySaralWarningMessage();

                }
                if (_dsstate.Tables[0].Rows.Count > 0)
                {
                    if (_dsstate.Tables[2].Rows.Count > 0 && (Convert.ToDouble(txtdistance.Text) > 50 || (!strbranchstate.Equals(strcuststate) && (!strCustPincode.Equals("121") && !strCustPincode.Equals("122") && !strCustPincode.Equals("110") && !strCustPincode.Equals("201")) && (!strBranchPincode.Equals("121") && !strBranchPincode.Equals("122") && !strBranchPincode.Equals("110") && !strBranchPincode.Equals("201")))) && (Convert.ToString(txtSaralRiskChannel.Text).Equals("Direct Sales") || Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-Core") || Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-IM")))
                    {
                        if (!strbranchstate.Equals(strcuststate))
                        {

                        }
                        FillWarningMessage("31");
                        DisplaySaralWarningMessage();
                    }
                    if (_dsstate.Tables[2].Rows.Count == 0 && (Convert.ToDouble(txtdistance.Text) > 50 || (!strbranchstate.Equals(strcuststate) && (!strCustPincode.Equals("121") && !strCustPincode.Equals("122") && !strCustPincode.Equals("110") && !strCustPincode.Equals("201")) && (!strBranchPincode.Equals("121") && !strBranchPincode.Equals("122") && !strBranchPincode.Equals("110") && !strBranchPincode.Equals("201")))) && (Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-Core") || Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-IM")))
                    {
                        FillWarningMessage("23");
                        DisplaySaralWarningMessage();
                    }
                    if (_dsstate.Tables[2].Rows.Count == 0 && (Convert.ToDouble(txtdistance.Text) > 50 || (!strbranchstate.Equals(strcuststate) && (!strCustPincode.Equals("121") && !strCustPincode.Equals("122") && !strCustPincode.Equals("110") && !strCustPincode.Equals("201")) && (!strBranchPincode.Equals("121") && !strBranchPincode.Equals("122") && !strBranchPincode.Equals("110") && !strBranchPincode.Equals("201")))) && (Convert.ToString(txtSaralRiskChannel.Text).Equals("Direct Sales")))
                    {
                        FillWarningMessage("24");
                        DisplaySaralWarningMessage();
                    }
                }
            }
            //}
            if (((txtdistance.Text != null && txtdistance.Text != "" && Convert.ToDouble(txtdistance.Text) > 80) || (!strbranchstate.Equals(strcuststate) && (!strCustPincode.Equals("121") && !strCustPincode.Equals("122") && !strCustPincode.Equals("110") && !strCustPincode.Equals("201")) && (!strBranchPincode.Equals("121") && !strBranchPincode.Equals("122") && !strBranchPincode.Equals("110") && !strBranchPincode.Equals("201")))) && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Direct Sales") && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-Core") && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-IM") && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Bankassurance-AU"))
            {
                FillWarningMessage("25");
                DisplaySaralWarningMessage();
            }
            //Added in Warning store proc
            //if (!string.IsNullOrEmpty(txtSaralRiskScore.Text) && !string.IsNullOrEmpty(txtENYScore.Text) && (Convert.ToInt32(txtSaralRiskScore.Text) == 75 || Convert.ToInt32(txtSaralRiskScore.Text) == 150 || ((Convert.ToInt32(txtENYScore.Text) >= 112 && Convert.ToInt32(txtENYScore.Text) <= 158))))
            //{

            //    FillWarningMessage("27");
            //    DisplaySaralWarningMessage();

            //}
            if (cbIsSicl.Checked)
            {
                FillWarningMessage("50");
                DisplaySaralWarningMessage();
            }

            objcomm.IsClientExist(ref _dsIsClient, strApplicationno);
            if (_dsIsClient != null && _dsIsClient.Tables.Count > 0 && _dsIsClient.Tables[0].Rows.Count > 0)
            {
                for (int f = 0; f < _dsIsClient.Tables[0].Rows.Count; f++)
                {
                    objcomm.PreviousCaseStatus(ref _dsPrevstatus, Convert.ToString(_dsIsClient.Tables[0].Rows[f]["CLT_applicationNo"]));
                    if (_dsPrevstatus.Tables.Count > 0 && _dsPrevstatus.Tables[0].Rows.Count > 0 && (_dsPrevstatus.Tables[0].Rows[0]["POL_riskCommencementStrDate"] != null || Convert.ToString(_dsPrevstatus.Tables[0].Rows[0]["POL_riskCommencementStrDate"]) != ""))
                    {
                        DateTime YR = Convert.ToDateTime(_dsPrevstatus.Tables[0].Rows[0]["POL_riskCommencementStrDate"]);
                        double year = (DateTime.Now - YR).Days / 365;
                        if ((year > 2))
                        {
                            FillWarningMessage("41");
                            DisplaySaralWarningMessage();
                            break;
                        }
                    }
                }
            }
            /*ID:19 END*/

        }
        /*ID:9 END*/
    }
    private void BindIIBScore(DataTable dt)
    {
        if (!string.IsNullOrEmpty(dt.Rows[0]["IIB_SCORE"].ToString()))
        {
            txtIIBscore.ToolTip = txtIIBscore.Text = Convert.ToString(dt.Rows[0]["IIB_SCORE"]);


            if (Convert.ToInt32(txtIIBscore.Text) >= 800)
            {

                txtIIBscore.BackColor = System.Drawing.Color.Red;
                txtIIBscore.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                txtIIBscore.BackColor = System.Drawing.Color.Green;
                txtIIBscore.ForeColor = System.Drawing.Color.White;
            }
        }
    }
    /*end here */
    protected void btnSaveToXml(object sender, EventArgs e)
    {
        Logger.Info(strApplicationno + "PAGE_NAME:UWSaralDecision/btnSaveToXml //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution Begin" + System.Environment.NewLine);
        ReplicaXml objReplica = new ReplicaXml();
        DataSet _dsPremium = new DataSet();
        int intResponse = -1;
        string strResponse = string.Empty;
        bool blnFalse = false;
        Logger.Info(strApplicationno + "STEP :1 Save XMl Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Save data to xml" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :btnSaveToXml" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse: " + blnFalse + System.Environment.NewLine);
        bool isdataSave = true;
        //application details 
        //ManageApplicationDetails(blnFalse, objReplica);

        PremiumCalculation(objReplica, ref isdataSave, ref _dsPremium, ref intResponse, ref strResponse);
        if (isdataSave == true)
        {
            //bank details
            ManageBankDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
            Logger.Info(strApplicationno + "Remark:" + "Bank details" + "Bank Details Section Save in Xml" + System.Environment.NewLine);
            //product & rider details 
            if (isdataSave == true)
            {
                ManageProductRiderDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                Logger.Info(strApplicationno + "Remark:" + "ProductRider details" + "ProductRider Details Section Save in Xml" + System.Environment.NewLine);
                if (isdataSave == true)
                {
                    ManageMandateDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                    Logger.Info(strApplicationno + "Remark:" + "Mandate details" + "Mandate Details Section Save in Xml" + System.Environment.NewLine);
                    //requirement details
                    if (isdataSave == true)
                    {
                        ManageRequirementDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                        Logger.Info(strApplicationno + "Remark:" + "Requirments details" + "Mandate Details Section Save in Xml" + System.Environment.NewLine);

                        //Fund Details
                        if (isdataSave == true)
                        {
                            ManageFundDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                            Logger.Info(strApplicationno + "Remark:" + "Fund details" + "Fund Details Section Save in Xml" + System.Environment.NewLine);

                            //LOADING DETAILS
                            if (isdataSave == true)
                            {
                                ManageLoadingDetails(blnFalse, ref isdataSave, objReplica, _dsPremium, ref strResponse);
                                Logger.Info(strApplicationno + "Remark:" + "Loading details" + "Loading Details Section Save in Xml" + System.Environment.NewLine);
                                //manage applicaiton status                              
                                if (isdataSave == true)
                                {
                                    if (Session["objCommonObj"] != null)
                                    {
                                        Logger.Info(strApplicationno + "STEP :8 Save Replica xml in DB Start" + System.Environment.NewLine);
                                        objCommonObj = (CommonObject)Session["objCommonObj"];
                                        int intRet = -1;
                                        int intSrNo = Convert.ToInt32(hdnSrNo.Value);
                                        Logger.Info(strApplicationno + "******************Replica xml Object Begin**************" + System.Environment.NewLine);
                                        Logger.Info(strApplicationno + "Replica xml Object" + GetXMLFromObject(objReplica) + System.Environment.NewLine);
                                        Logger.Info(strApplicationno + "******************Replica xml Object End**************" + System.Environment.NewLine);
                                        ManageApplicatonObject(intSrNo, strApplicationno, objReplica, false, ref isdataSave, objCommonObj._Bpmuserdetails._UserID, ref intRet);
                                        if (isdataSave == true)
                                        {
                                            ShowPopupMessage("Details Save Successfully");
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('replica Xml Save  succeed')", true);
                                            Logger.Info(strApplicationno + "Remark:" + "Xml details Save in DB" + "Xml details Save in DB " + System.Environment.NewLine);
                                        }
                                        else
                                        {
                                            Logger.Info(strApplicationno + "Remark:" + "Xml details Save in DB" + "Xml details Not Save in DB " + System.Environment.NewLine);
                                            ShowPopupMessage("Error While Save Details ,Please Contact System Admin");
                                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('replica Xml Save Not succeed')", true);
                                            return;
                                        }
                                        hdnSrNo.Value = Convert.ToString(intRet);
                                        Logger.Info(strApplicationno + "STEP :8 Save Replica xml in DB end" + System.Environment.NewLine);
                                    }

                                }
                                else
                                {
                                    return;
                                }
                            }
                            else
                            {
                                Logger.Info(strApplicationno + "Remark:" + "Fund details" + "Fund Details Section Not Save in Xml" + System.Environment.NewLine);
                                ShowPopupMessage(strResponse);
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Requirment Details Not succeed')", true);
                                return;
                            }
                        }
                        else
                        {
                            Logger.Info(strApplicationno + "Remark:" + "Requirement details" + "Requirement Details Section Not Save in Xml" + System.Environment.NewLine);
                            ShowPopupMessage(strResponse);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mandate Details Not succeed')", true);
                            return;
                        }
                    }
                    else
                    {
                        Logger.Info(strApplicationno + "Remark:" + "Mandate details" + "Mandate Details Section Not Save in Xml" + System.Environment.NewLine);
                        ShowPopupMessage(strResponse);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ProductRider Details Not succeed')", true);
                        return;
                    }
                }
                else
                {
                    Logger.Info(strApplicationno + "Remark:" + "Rider details" + "Rider Details Section Not Save in Xml" + System.Environment.NewLine);
                    ShowPopupMessage(strResponse);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bank Details Not succeed')", true);
                    return;
                }
                /*end here*/
            }
            else
            {
                Logger.Info(strApplicationno + "Remark:" + "Bank details" + "Bank Details Section Not Save in Xml" + System.Environment.NewLine);
                ShowPopupMessage(strResponse);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bank Details Not succeed')", true);
                return;
            }

            //MANAGE PRODUCT DETAILS
            //ManageProductDetails(blnFalse, objReplica);              
        }
        else
        {
            Logger.Info(strApplicationno + "Remark:" + "Premium Calculation --XML SAVE" + "Premium Calculation not Success while save data to XML" + System.Environment.NewLine);
            ShowPopupMessage(strResponse);
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Premium Calculation Not succeed, Please Click on Calculate Premium to verify')", true);
            return;
        }
    }

    protected void btnSaveToDatabase(object sender, EventArgs e)
    {
        /*ID:6 START*/
        if (!string.IsNullOrEmpty(txtUWComments.Value))
        {
            SaveUwComment();
        }

        //CR-30489 Added by kavita on 26/08/2021
        SaveIIBImpact(strApplicationno);
        //Ended by kavita on 26/08/2021

        /*ID:6 END*/
        /*ID:5 START*/
        /*
        if (ddlUWDecesion.SelectedValue.Equals("Approved") && strChannelType.ToLower().Equals("online"))
        {
            if (imgAadharVerified.ImageUrl.Contains("Failuer"))
            {
                ShowPopupMessage("Please verify aadhar details");
                throw new Exception("UDE-Verify Aadhar Details");
            }
            if (imgPanVerified.ImageUrl.Contains("Failuer"))
            {
                ShowPopupMessage("Please verify pan details");
                throw new Exception("UDE-Verify pan Details");
            }
        }
        */
        /*ID:5 END*/
        /*ID:8 START*/
        //if (strChannelType.Equals("offline") && (Convert.ToInt32(txtBasepremium.Text)-Convert.ToInt32(txtProdBranchBasePremium.Text))>200)
        //{
        //    throw new Exception("UDE-01");
        //}
        /*ID:8 END*/
        //Addedby Suraj on 02JULY2020 for NAWP catagory
        if (txtProdcode.Text == "E83" || txtProdcode.Text == "E84")
        {
            AppDtls objApplicationdetails = new AppDtls();
            objChangeObj.App_backdate = objApplicationdetails;
            if (ddlNAWPCatagory.SelectedValue != "0" && ddlNAWPCatagory.SelectedValue != null && ddlNAWPCatagory.SelectedValue != "")
            {
                Session["NAWPCatagory"] = ddlNAWPCatagory.SelectedValue;
                objChangeObj.App_backdate._NAWPCatagory = ddlNAWPCatagory.SelectedValue;

            }
            else
            {
                // objChangeObj.App_backdate._NAWPCatagory =string.Empty;
            }
        }
        Logger.Info(strApplicationno + "PAGE_NAME:UWPortfolio/UWdecision/btnSaveToDatabase //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution Begin" + Environment.NewLine);
        ReplicaXml objReplica = new ReplicaXml();
        DataSet _dsPremium = new DataSet();
        int intResponse = -1;
        string strResponse = string.Empty;
        bool blnFalse = !false;
        bool isdataSave = true;
        PremiumCalculation(objReplica, ref isdataSave, ref _dsPremium, ref intResponse, ref strResponse);
        if (isdataSave == true)
        {
            //bank details
            ManageBankDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
            //product & rider details 
            if (isdataSave == true)
            {
                ManageProductRiderDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                //manage mandate 
                if (isdataSave == true)
                {
                    // ManageMandateDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                    //requirement details
                    if (isdataSave == true)
                    {
                        ManageRequirementDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                        //Fund Details
                        if (isdataSave == true)
                        {
                            ManageFundDetails(blnFalse, ref isdataSave, objReplica, ref strResponse);
                            Logger.Info(strApplicationno + "Remark:" + "Fund details" + "Fund Details Section Save in Xml" + System.Environment.NewLine);
                            //LOADING DETAILS
                            if (isdataSave == true)
                            {
                                ManageLoadingDetails(blnFalse, ref isdataSave, objReplica, _dsPremium, ref strResponse);
                                //manage applicaiton status
                                if (isdataSave == true)
                                {
                                    //ManageApplicatoinStatus(ref isdataSave);
                                    //ManageApplicationObjectLog    
                                    if (isdataSave == true)
                                    {
                                        if (Session["objCommonObj"] != null)
                                        {


                                            Logger.Info(strApplicationno + "STEP :8 Save Replica xml in DB Start" + System.Environment.NewLine);
                                            objCommonObj = (CommonObject)Session["objCommonObj"];
                                            int intRet = -1;
                                            int intSrNo = Convert.ToInt32(hdnSrNo.Value);
                                            Logger.Info(strApplicationno + "******************Replica xml Object Begin**************" + System.Environment.NewLine);
                                            Logger.Info(strApplicationno + "Replica xml Object" + GetXMLFromObject(objReplica) + System.Environment.NewLine);
                                            Logger.Info(strApplicationno + "******************Replica xml Object End**************" + System.Environment.NewLine);
                                            ManageApplicatonObject(intSrNo, strApplicationno, objReplica, true, ref isdataSave, objCommonObj._Bpmuserdetails._UserID, ref intRet);
                                            if (isdataSave == true)
                                            {
                                                Logger.Info(strApplicationno + "Remark:" + "UWsaral details post Success" + System.Environment.NewLine);
                                            }
                                            else
                                            {
                                                Logger.Info(strApplicationno + "Remark:" + "Post replica xml details not save in Database" + System.Environment.NewLine);
                                                ShowPopupMessage("Details Post Successfully");
                                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Details Post Successfully')", true);
                                            }
                                            string strEventkey = string.Empty;

                                            if (ddlUWDecesion.SelectedValue != "proposal")
                                            {
                                                strEventkey = ddlUWDecesion.SelectedValue;

                                            }
                                            else
                                            {
                                                strEventkey = "POST";
                                            }
                                            //objcomm.MaintainApplicationLog(strApplicationno, strEventkey, "", objChangeObj.userLoginDetails._UserID, "", ref intRetVal);
                                            Logger.Info(strApplicationno + "STEP :8 Save Replica xml in DB End" + System.Environment.NewLine);
                                            /*ID:10 START*/
                                            ManageDecisionType();
                                            /*ID:10 END*/
                                            //Added by Suraj on 25 APRIL 2018
                                            int resp = 0;
                                            if (Convert.ToInt32(ddlAssigmentType.SelectedValue) != 0 || Convert.ToString(ddlAssigmentType.SelectedValue) != "")
                                            {
                                                objBuss.Save_MedicalReport_Reason(2, strApplicationno, Convert.ToInt32(ddlAssigmentType.SelectedValue), objChangeObj.userLoginDetails._UserID, ref resp);
                                            }
                                            //End
                                            //Added by Suraj on 5 JULY 2018 for save Country in db
                                            int resp1 = 0;

                                            if (Convert.ToString(ddlcountry.SelectedValue) != "" || Convert.ToInt32(ddlcountry.SelectedValue) != 0)
                                            {
                                                objBuss.Save_Country("Country", strApplicationno, Convert.ToString(ddlcountry.SelectedItem.Text), objChangeObj.userLoginDetails._UserID, ref resp1);
                                            }
                                            //end
                                            if (Convert.ToString(ddlSignature.SelectedValue) != "" || Convert.ToInt32(ddlSignature.SelectedValue) != 0)
                                            {
                                                objBuss.Save_Country("Signature", strApplicationno, Convert.ToString(ddlSignature.SelectedItem.Text), objChangeObj.userLoginDetails._UserID, ref resp1);
                                            }
                                            //Added By Royson Pinto on 22nd NOV to storepass values Restrict Further Cover
                                            if (objCommonObj._Restrict_Further_Cover != "")
                                            {
                                                objBuss.Save_Country("RestrictFurtherCover", strApplicationno, objCommonObj._Restrict_Further_Cover, objChangeObj.userLoginDetails._UserID, ref resp1);
                                            }
                                            //End
                                            /*ID:13 START*/
                                            //ManagePolicyPrinting();
                                            /*ID:13 END*/
                                            //pragati start CR 27524 save to db. excess premium
                                            if (ddlRsnExcessPremium.SelectedValue != "0" && ddlRsnExcessPremium.SelectedValue != "")
                                            {
                                                objBuss.Save_RsnExcessPremium("RsnExcessPremium", strApplicationno, Convert.ToString(ddlRsnExcessPremium.SelectedItem.Text), objChangeObj.userLoginDetails._UserID, Convert.ToString(ddlcountry.SelectedItem.Text), ref resp1);
                                            }
                                            //end Pragati 
                                            //Brijesh start 
                                            string RelationStaff = ddlRelationStaff.SelectedValue == "0" ? "" : ddlRelationStaff.SelectedValue;
                                            string Category = ddlCategory.SelectedValue == "0" ? ddlNAWPCatagory.SelectedValue : ddlCategory.SelectedValue;
                                            objBuss.Save_StaffDetails(strApplicationno, RelationStaff, hd_que_2.Checked, Category, "UW", objChangeObj.userLoginDetails._UserID, ref resp1);
                                            //end  
                                            /*ID 29 START*/
                                            objBuss.Save_BusinessException("BusinessException", strApplicationno, chkexcep.Checked, objChangeObj.userLoginDetails._UserID, ref resp1);
                                            /*ID 29 END*/
                                            //36.2 Begin of changes; Sagar Thorave[mfl00886]
                                            string Amlval = GetAmlValTOLifeAsia();
                                            //36.2 End of changes; Sagar Thorave[mfl00886]
                                        }
                                    }
                                    else
                                    {
                                        Logger.Info(strApplicationno + "Remark:" + "ApplicationStatus details" + "ApplicationStatus Details Section Not Save in Xml" + System.Environment.NewLine);
                                        ShowPopupMessage("ApplicationStatus Details Not Save");
                                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ApplicationStatus Details Not succeed')", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    Logger.Info(strApplicationno + "Remark:" + "Loading details" + "Loading Details Section Not Save in Xml" + System.Environment.NewLine);
                                    ShowPopupMessage("Loading Details Not Save");
                                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Loading Details Not succeed')", true);
                                    return;
                                }
                            }
                            else
                            {
                                Logger.Info(strApplicationno + "Remark:" + "Requirment details" + "Requirment Details Section Not Save in Xml" + System.Environment.NewLine);
                                ShowPopupMessage("Requirment Details Not Save");
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Requirment Details Not succeed')", true);
                                return;
                            }
                        }
                        else
                        {
                            Logger.Info(strApplicationno + "Remark:" + "Fund details" + "Fund Details Section Not Save" + System.Environment.NewLine);
                            ShowPopupMessage("Fund Details Not Save");
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Requirment Details Not succeed')", true);
                            return;
                        }

                    }
                    else
                    {
                        Logger.Info(strApplicationno + "Remark:" + "Mandate details" + "Mandate Details Section Not Save in Xml" + System.Environment.NewLine);
                        ShowPopupMessage("Mandate Details Not Save");
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Mandate Details Not succeed')", true);
                        return;
                    }
                }
                else
                {
                    Logger.Info(strApplicationno + "Remark:" + "ProductRider details" + "ProductRider Details Section Not Save in Xml" + System.Environment.NewLine);
                    ShowPopupMessage("ProductRider Details Not Save");
                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ProductRider Details Not succeed')", true);
                    return;
                }
            }
            else
            {
                Logger.Info(strApplicationno + "Remark:" + "Bank details" + "Bank Details Section Not Save in Xml" + System.Environment.NewLine);
                ShowPopupMessage("Bank Details Not Save");
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Bank Details Not succeed')", true);
                return;
            }
        }
        else
        {
            ShowPopupMessage("Premium calculation not succeed,please Click on calculate button");
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Premium calculation not succeed,please calculate on Premium calculation button to verify')", true);
            return;
        }
        //MANAGE PRODUCT DETAILS
        //ManageProductDetails(blnFalse, objReplica);                                            
        Logger.Info(strApplicationno + "PAGE_NAME:UWPortfolio/UWdecision/btnSaveToDatabase //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution Begin" + System.Environment.NewLine);
    }
    /*ADDED BY SHRI TO CALL VALIDATION */
    protected void btnServerValidation(object sender, EventArgs e)
    {
        #region 37.1 End Of Changes Of CR-2394 Validation coustomer mob with agent by Sushant Devkate [MFL00905]
        if (ddlUWDecesion.SelectedValue.Equals("Approved"))
        {
            bool IsAPICAll = Convert.ToBoolean(ConfigurationManager.AppSettings["IsAgntChkAPIURLCall"].ToString());
            if (IsAPICAll)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                ValidationDO ObjValidationDO = new CheckValidationBLL().GetClientHolderDataforValidation(strApplicationno, "UW", objChangeObj.userLoginDetails._UserID);

                if (ObjValidationDO != null)
                {
                    if (ObjValidationDO.IsValid == false && ObjValidationDO.Message != "")
                    {
                        ShowPopupMessage(ObjValidationDO.Message);
                        throw new Exception("UDE-" + ObjValidationDO.Message);
                    }
                }
            }
        }

        #endregion 37.1 End Of Changes Of CR-2394 Validation coustomer mob with agent by Sushant Devkate [MFL00905]
        string strPipe = "|";
        if (lblErrorcommentdtls.Text == "")
        {
            ShowPopupMessage("Please enter comment..!");
            throw new Exception("UDE-Please enter comment..!");
        }
        if (objcomm.CheckIsSignUploaded(strApplicationno))
        {
            if (ddlSignature.SelectedValue.Equals("0"))
            {
                ShowPopupMessage("Please select Signature matched!!");
                throw new Exception("UDE-Please select Signature matched!!");
            }
            //RFVSignatr.Enabled = true;
            //divSignature.Style.Add("display", "inline");
        }


        if (ddlUWDecesion.SelectedValue.Equals("Approved"))
        {
            //30.1 Begin of Changes; Pooja Shetye - [1133038]
            bool IsvalidPAN = true;
            IsvalidPAN = CheckFlaggingofPANandForm60(txtPannumber.Text, chkPanCopy.Checked, chkForm16Copy.Checked);

            if (IsvalidPAN == true)
            { //30.1 End of Changes; Pooja Shetye - [1133038]

                /*ID 29 START*/
                if (chkexcep.Checked == false)
                {
                    string confirmValue = string.Empty;
                    //confirmValue = Request.Form["confirm_value"];
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Confirm()", true);
                    //if (string.IsNullOrEmpty(confirmValue))
                    //{
                    //    string script = "window.onload = function() { Confirm(); };";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "Confirm()", script, true);
                    //    //return;
                    //}
                    confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {

                    }
                    else
                    {
                        ShowPopupMessage("Please select Business Exception!");
                        throw new Exception("UDE-Please select Business Exception!");
                    }
                }
                /*ID 29 END*/

                //added by suraj on 25 JULY 2019 for signature match
                if (ddlSignature.SelectedValue.Equals("12"))
                {
                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["CurrentTable1"];
                    string strSignaturecode = ConfigurationManager.AppSettings["SignatureCode"];
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "REQ_followUpCode = '" + strSignaturecode + "' and (REQ_status = 'R' OR REQ_status = 'W')";
                    //dv.RowFilter = "REQ_status = 'R' OR REQ_status = 'W'";
                    if (dv.Count == 0)
                    {
                        ShowPopupMessage("Please raise requirement code 'SEN'!!");
                        throw new Exception("UDE-Please raise requirement code 'SEN'");
                    }
                }

                DataSet dsresult = new DataSet();
                objcomm.ChkNomineePresence(strApplicationno, ref dsresult);
                if (dsresult != null && dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows.Count == 0)
                {
                    ShowPopupMessage("Nominee Not Exists Even Though LA And Proposer Are Same!!");
                    throw new Exception("UDE-Nominee Not Exists Even Though LA And Proposer Are Same!");
                }

                //added by suraj on 08 APR 2020 for CR NO-26884 
                //UWPreIssueServiceCall(strApplicationno, strChannelType);
                //if (!string.IsNullOrEmpty(txtTotalPremDue.Text) && !string.IsNullOrEmpty(txtTotalpremium.Text))
                //{
                //    if ((Convert.ToDecimal(txtTotalpremium.Text) - Convert.ToDecimal(txtTotalPremDue.Text)) > 30 || (Convert.ToDecimal(txtTotalPremDue.Text) - Convert.ToDecimal(txtTotalpremium.Text)) > 30)
                //    {
                //        ShowPopupMessage("Life Asia total premium and Saral total premium mismatch!!");
                //        throw new Exception("UDE-Life Asia total premium and Saral total premium mismatch");
                //    }
                //}

                //end
                //Pragati Changes start CR-27524
                //Decimal intSumDifference1 = (Convert.ToDecimal(string.IsNullOrEmpty(txtAmountinsuspense.Text) ? "0" : txtAmountinsuspense.Text) - Convert.ToDecimal(string.IsNullOrEmpty(txtTotalPremDue.Text) ? "0" : txtTotalPremDue.Text));
                //int sumamt = Convert.ToInt32(intSumDifference1);
                int? intSumDifference = 0;
                if (!string.IsNullOrEmpty(txtAmountdue.Text))
                {
                    intSumDifference = Convert.ToInt32(Convert.ToDecimal(txtAmountdue.Text));
                }

                if (intSumDifference != null)
                {
                    if (intSumDifference >= 200)
                    {

                        if (ddlRsnExcessPremium.SelectedValue == "0" && ddlRsnExcessPremium.SelectedValue == "")
                        {
                            ddlRsnExcessPremium.Enabled = true;
                            ShowPopupMessage("Please fill Excess Premium Reason Dropdown in Application Details!!");
                            throw new Exception("UDE-Please fill Excess Premium Reason Dropdown in Application Details!!");
                        }


                    }
                    else
                    {
                        ddlRsnExcessPremium.Enabled = false;

                    }
                }
                //Pragati Changes End
                // Start Brijesh Pandey
                if ((hd_que_2.Checked == true || ddlCategory.SelectedValue == "staff" || ddlNAWPCatagory.SelectedValue == "staff") && ddlRelationStaff.SelectedValue == "0")
                {
                    ShowPopupMessage("Please select Relation with Staff Dropdown in Application Details!!");
                    throw new Exception("UDE-Please select Relation with Staff Dropdown in Application Details!!");
                }
                // End

            }
            else
            {
                //30.1 Begin of Changes; Pooja Shetye - [1133038]
                ShowPopupMessage("Correct the PAN/Form 60 Flagging.");
                throw new Exception("UDE-Correct the PAN/Form 60 Flagging.");
                //30.1 End of Changes; Pooja Shetye - [1133038]
            }

            // 41.1 Begin of Changes; Jayendra  - [Webashlar01]

            bool IsUINNoCheck = false;
            IsUINNoCheck = new Commfun().IsUinNoStatusCheck(strApplicationno);

            if (IsUINNoCheck)
            {
                ShowPopupMessage("UINNO is mismatch. Please withdraw the application!");
                throw new Exception("UDE-UINNO is mismatch. Please withdraw the application!");
            }
            // 41.1 End of Changes; Jayendra  - [Webashlar01]


        }
        if (ddlApplicationDetailsProposalType.SelectedValue.Equals("NRI"))
        {
            if (ddlcountry.SelectedValue.Equals("0"))
            {
                ShowPopupMessage("Please select Residence country of customer");
                throw new Exception("UDE-Please select Residence country of customer");
            }

        }
        if (strChannelType.Equals("ONLINE"))
        {
            //Added by suraj on 06 June 2019 -bank details mandatory for all policy bazaar cases(FPB)
            if (ddlUWDecesion.SelectedValue.Equals("Approved"))
            {
                //as discussed with dharmesh sir,add like condition for FPB cases
                if (strApplicationno.Contains("FG"))
                {
                    if (string.IsNullOrEmpty(txtBnkIfsccode.Text) || string.IsNullOrEmpty(txtBnkBankaccno.Text))
                    {
                        ShowPopupMessage("Policy Bazaar Case : Future Payout Bank details are mandatory");
                        throw new Exception("UDE-Policy Bazaar Case : Future Payout Bank details are mandatory");

                    }
                }

            }
            //End
            /*ID:5 START*/
            /*
            if (ddlUWDecesion.SelectedValue.Equals("Approved") && strChannelType.ToLower().Equals("online"))
            {
                if (imgAadharVerified.ImageUrl.Contains("Failuer"))
                {
                    ShowPopupMessage("Please verify aadhar details");
                    throw new Exception("UDE-Verify Aadhar Details");
                }
                if (imgPanVerified.ImageUrl.Contains("Failuer"))
                {
                    ShowPopupMessage("Please verify pan details");
                    throw new Exception("UDE-Verify pan Details");
                }
            }
            */
            /*ID:5 END*/
        }
        if (strChannelType.Equals("Offline"))
        {
            /*ID:10 START*/
            //not proposal
            if (!ddlUWDecesion.SelectedValue.Equals("proposal"))
            {
                //not  decline 
                if (ddlUWDecesion.SelectedValue.Equals("Approved"))
                {
                    //check if Nominee Exists Even Though LA And Proposer Are different
                    if (hdnWarningMessage.Value.Contains(strPipe + "2" + strPipe))
                    {
                        ShowPopupMessage("Nominee Exists Even Though LA And Proposer Are different");
                        throw new Exception("UDE-Nominee Exists Even Though LA And Proposer Are different");
                    }

                    //CIBIL
                    if (hdnWarningMessage.Value.Contains(strPipe + "6" + strPipe) && string.IsNullOrEmpty(txtApplicationDetailsCibil.Text))
                    {
                        ShowPopupMessage("Enter CIBIL Score Before Approving");
                        throw new Exception("UDE-Enter CIBIL Score Before Approving");
                    }

                    /*risk mandatory*/
                    if (string.IsNullOrEmpty(txtENYScore.Text) || txtENYScore.Text.Equals("0") || string.IsNullOrEmpty(txtSaralRiskScore.Text) || txtSaralRiskScore.Text.Equals("0"))
                    {
                        ShowPopupMessage("Cannot Approve Since Score Is Not Updated");
                        throw new Exception("UDE-Cannot Approve Since Score Is Not Updated");
                    }
                    //Added by Suraj on 24 JULY 18 for High Risk category cases
                    DataSet dsresult = new DataSet();
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                    objcomm.ChkHighCatCasesForWarning(strApplicationno, objChangeObj.userLoginDetails._UserID, ref dsresult);
                    if (dsresult.Tables.Count > 0 && dsresult.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dsresult.Tables[0].Rows[0]["ID"]) == 28 || Convert.ToInt32(dsresult.Tables[0].Rows[0]["ID"]) == 35 || cbIsSicl.Checked)
                        {
                            HighRisk = true;
                            AddNewRowToGrid1();
                            ShowPopupMessage("High Risk case wherein Profile investigation is mandatory. Please check");
                            throw new Exception("UDE-High Risk case wherein Profile investigation is mandatory. Please check");
                        }
                    }
                    /*ID:13 START*/
                    //if (ddlDecisionDetailsIsPolicyPrintingToHolding.SelectedValue == "-1")
                    //{
                    //    ShowPopupMessage("UDE-Select Whether To Hold Policy Details Or Not");
                    //    throw new Exception("UDE-Select Whether To Hold Policy Details Or Not");
                    //}
                    /*ID:13 END*/
                }

                //strUserId
                //if (IsUserLimitLessThanSumAssured())
                //{
                //    ShowPopupMessage("You Cannot Process the case since your limit is less than Sum Assured");
                //    throw new Exception("UDE-You Cannot Process the case since your limit is less than Sum Assured");
                //}
            }


            //Added by Suraj on 25 APRIL 2018
            if (ddlApplicationDetailsProposalType.SelectedValue.Equals("EMP"))
            {

                //divassign.Attributes["class"] = divassign.Attributes["class"].ToString().Replace("HideControl", "");
                //clsName = divApplicationDetails.Attributes["class"].ToString();
                //divApplicationDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                if (ddlAssigmentType.SelectedValue.Equals("0"))
                {
                    ShowPopupMessage("Please select Assignment Type");
                    throw new Exception("UDE-Please select Assignment Type");
                }

            }

            //Added by Suraj on 02/12/2018 for those contires which are not allowed to approve the case
            if (Convert.ToString(ddlcountry.SelectedValue) != "" || Convert.ToInt32(ddlcountry.SelectedValue) != 0)
            {
                objBuss.GetCountry(strApplicationno, ref _ds);
                if (_ds.Tables != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    ShowPopupMessage("Country is not valid,please check!!");
                    throw new Exception("UDE-Country is not valid,please check!!");
                }
            }
            //else
            //{
            //    divassign.Attributes.Add("Class", "HideControl");
            //}

            bool blnIsCheckedDecisionType = true;
            //if (!ddlUWDecesion.SelectedValue.Equals("proposal"))
            //{
            blnIsCheckedDecisionType = false;
            foreach (ListItem li in cblDecisionTypeDecisions.Items)
            {
                if (li.Selected)
                {
                    blnIsCheckedDecisionType = true;
                    break;
                }

            }
            foreach (ListItem li in cblDecisionTypeDecisions.Items)
            {
                if (!li.Selected)
                {
                    if (Convert.ToInt32(li.Value) == 14 && txtSaralRiskScore.Text != null && txtSaralRiskScore.Text != "" && Convert.ToInt32(txtSaralRiskScore.Text) > 70)
                    {
                        ShowPopupMessage("HOD approval in decision type is mandatory for this risk score");
                        throw new Exception("UDE-HOD approval is mandatory for this risk score");
                    }
                }
            }
            //}
            if (!blnIsCheckedDecisionType)
            {
                foreach (ListItem li in cblDecisionTypeIncompleteDocument.Items)
                {
                    if (li.Selected)
                    {
                        blnIsCheckedDecisionType = true;
                        break;
                    }
                }
                if (!blnIsCheckedDecisionType)
                {
                    foreach (ListItem li in cblDecisionTypeCleanCase.Items)
                    {
                        if (li.Selected)
                        {
                            blnIsCheckedDecisionType = true;
                            break;
                        }
                    }
                    if (!blnIsCheckedDecisionType)
                    {
                        ShowPopupMessage("Please select decision type before proceeding");
                        throw new Exception("UDE-Please select decision type before proceeding");
                    }
                }
            }
            /*ID:10 END*/
            /*ID:8 START*/
            //if (strChannelType.Equals("offline") && (Convert.ToInt32(txtBasepremium.Text)-Convert.ToInt32(txtProdBranchBasePremium.Text))>200)
            //{
            //    throw new Exception("UDE-01");
            //}
            /*ID:8 END*/
        }

        #region start 31.1: This CR-2591 changes done by Sushant Devkate MFL00905 
        //if (!ddlUWDecesion.SelectedValue.Equals("proposal"))
        if (ddlUWDecesion.SelectedValue != "proposal" && ddlUWDecesion.SelectedValue != "Withdrawn")
        {
            if (IsUserLimitLessThanSumAssured())
            {
                ShowPopupMessage("You Cannot Process the case since your limit is less than FSAR band limit");
                //throw new Exception("UDE-You Cannot Process the case since your limit is less than FSAR band limit");
            }
        }
        #endregion End 31.1: This CR-2591 changes done by Sushant Devkate MFL00905  
        //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]
        if (txtriskcat.Text.ToString().Trim() != "IRSM not applicable Risk") 
        {
            showerrorrisk();
        }
      
        //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]

        //46.1 Begin of Changes; Bhaumik Patel - [CR-4753]
        showEXAprroval();
        //46.1 End of Changes; Bhaumik Patel - [CR-4753]
        //46.1 Begin of Changes; Bhaumik Patel - [CR-4753]
        showEXAprroval();
        //46.1 End of Changes; Bhaumik Patel - [CR-4753]
        //49.1 Begin of Changes; Sagar Thorave - [CR-7049]
        showacceptanceApprove();
        //49.1 End of Changes; Sagar Thorave - [CR-7049]
    }
    /*END HERE*/

    //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]
    protected void showerrorrisk()
    {
        DataSet ds = new DataSet();
        int errocode = 0;
        commobj.OnlineDecisionRightsDisplayDetails_GET("GetUserAccess_Risk_Category", strUserId, UserLimit, ref ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                if (txtriskcat.Text.ToString().Trim() == rows["NAME"].ToString().Trim())
                {
                    errocode = 1;
                }
            }

            if (errocode == 0)
            {
                ShowPopupMessage("" + struserName + " Have No Access For " + txtriskcat.Text + "Risk Category Case ....");
                throw new Exception("UDE-" + struserName + " Have No Access For " + txtriskcat.Text + "Risk Category Case ....");
            }
        }
    }
    //45.1 End of Changes; Bhaumik Patel - [CR-5307]

    //46.1 Begin of Changes; Bhaumik Patel - [CR-4753]
    protected void showEXAprroval()
    {
        foreach (GridViewRow Row in gridExaprroval.Rows)
        {
            if (Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlEXAprrovaluwdecision = (DropDownList)Row.FindControl("ddlEXAprrovaluwdecision");
                string Decision = ddlEXAprrovaluwdecision.SelectedItem.Text.ToString();  //_Dt.Rows[Row.RowIndex]["UW Decision"].ToString();

                if (ddlUWDecesion.SelectedValue != "proposal" && Decision == "Non Acceptable")
                {
                    ShowPopupMessage("" + struserName + " you cannot approve this policy!...exception approval taken is non acceptable.");
                    throw new Exception("UDE-" + struserName + " you cannot approve this policy!...exception approval taken is non acceptable.");
                }
                else
                {
                }

            }

        }

    }
    //46.1 End of Changes; Bhaumik Patel - [CR-4753]
    //49.1 Begin of Changes; Sagar Thorave - [CR-7049]
    protected void showacceptanceApprove()
    {

        if (txtriskcat.Text == "High Risk" || txtriskcat.Text == "Very High Risk")
        {
            if (string.IsNullOrEmpty(ddlAcceptanceReason.SelectedValue))
            {

                ShowPopupMessage("" + struserName + "Please select Acceptance Reason");
                throw new Exception("UDE-" + struserName + "Please select Acceptance Reason");
            }
            else {
                //50.1 Begin of Changes; Jay Patnakar - [WebAshlar01]
                string reasons = Request.Form[ddlAcceptanceReason.UniqueID];

                if (reasons.Contains("Others (Free Text)") && string.IsNullOrEmpty(txtAcceptanceOtherReasonText.Text))
                {
                    ShowPopupMessage("" + struserName + "Please select Acceptance Other Reason Text");
                    throw new Exception("UDE-" + struserName + "Please select Acceptance Other Reason Text");
                }
                //50.1 End of Changes; Jay Patnakar - [WebAshlar01]

                objComm.InsertAcceptanceReason(reasons, txtAcceptanceOtherReasonText.Text, txtAppno.Text, txtAppno.Text);
            }

        }
    }
    //49.1 End of Changes; Sagar Thorave - [CR-7049]
    private void ManageApplicationDetails(bool blnUpdateInLA, ref bool isdataSave, ReplicaXml objReplica)
    {
        //variable declaration part
        int AppResult = 0;
        int intServicesResultCount = 0, intTotalServiceCount = 3;
        string date = hdnRcdReq.Value;
        lblErrorAppDetailsBody.Text = lblErrorappdtls.Text = string.Empty;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        AppDtls objApplicationdetails = new AppDtls();
        objApplicationdetails._Backdate = Request.Form[txtRcdreq.UniqueID];
        objChangeObj.App_backdate = objApplicationdetails;
        if (blnUpdateInLA)
        {
            //update in life asia as well as in database
            Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnAppDtlsSave_Click");
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            lblErrorAppDetailsBody.Text = strLApushStatus;
            if (strLApushErrorCode == 0)
            {
                intServicesResultCount++;
                LoadDtls objLoad = new LoadDtls();
                objLoad._strProdcode = "";
                objChangeObj.Load_Loadingdetails = objLoad;
                lblErrorappdtls.Text += "Application Details  updated in Life asia";
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                lblErrorAppDetailsBody.Text = strLApushStatus;
                if (_ds != null && _ds.Tables[0].Rows.Count > 0)
                {
                    intServicesResultCount++;
                    txtBackDateIntrest.Text = _ds.Tables[0].Rows[0]["BackDateintrest"].ToString();

                }
                objComm.OnlineApplicationDetails_Save(strPolicyNo, strApplicationno, "Yes", Request.Form[txtRcdreq.UniqueID], ddlBkdateReason.SelectedItem.Value, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, "UWSaralUnderwritting", strApplicationno, objChangeObj.userLoginDetails._UserID, strChannelType, txtBackDateIntrest.Text, ref AppResult);
                if (AppResult != -1)
                {
                    chkAppDtls.Checked = false;
                }
                else
                {
                    chkAppDtls.Checked = false;
                }
                if (intServicesResultCount == intTotalServiceCount)
                {
                    lblErrorappdtls.Text = "Backdating details updated Successfully Click to know more";
                }
            }
            else
            {
                lblErrorappdtls.Text += "Application Details Not updated in Life asia CLICK to know more";
                lblErrorAppDetailsBody.Text = strLApushStatus;
                chkAppDtls.Checked = false;
            }
        }
        /*added by shri on 08 nov 17 to add data into replica object */
        ApplicationSection objApplicationSection = new ApplicationSection();
        objApplicationSection.AppNo = txtAppno.Text;
        objApplicationSection.PolNo = txtPolno.Text;
        objApplicationSection.AppSignDate = txtAppsigndate.Text;
        objApplicationSection.PIVCSTATUS = txtPivcStatus.Text;
        objApplicationSection.BACKDATEFLAG = switch_havInsurance.Checked;
        objApplicationSection.BACKDATEINTREST = txtBackDateIntrest.Text;
        objApplicationSection.AgentChannel = txtAgentChannel.Text;
        objApplicationSection.BACKDATE = txtRcdreq.Text;
        objApplicationSection.BACHDATEREASON = ddlBkdateReason.SelectedValue;
        objApplicationSection.IsStaff = hd_que_2.Checked;
        objApplicationSection.PIVCRJCREASON = txtPivcRejectReason.Text;
        objApplicationSection.Channel = txtAppchannel.Text;
        if (imgPivcStatus.ImageUrl.Equals(@"../dist/img/Success.png"))
        {
            objApplicationSection.SCRH_PIVC_STATUS = 1;
        }
        else
        {
            objApplicationSection.SCRH_PIVC_STATUS = 0;
        }
        objApplicationSection.AUTOPAYTYPE = ddlAutoPaytype.SelectedValue;
        objApplicationSection.Section = "ApplicationDetails";
        if (objApplicationSection != null)
        {
            objReplica.ApplicationSection = objApplicationSection;
        }
        /*end here*/
    }

    public static string GetXMLFromObject(object o)
    {
        StringWriter sw = new StringWriter();
        XmlTextWriter tw = null;
        try
        {
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            tw = new XmlTextWriter(sw);
            serializer.Serialize(tw, o);
        }
        catch (Exception)
        {
            //Handle Exception Code
        }
        finally
        {
            sw.Close();
            if (tw != null)
            {
                tw.Close();
            }
        }
        return sw.ToString();
    }

    private void ManageBankDetails(bool blnUpdateInLA, ref bool isdataSave, ReplicaXml objReplica, ref string strErrorMessage)
    {
        //This is the common funcation to save bank details in xml or database base on blnUpdateInLA parameter value.
        Logger.Info(strApplicationno + "STEP :2 Save BANKDETAILS Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Bank details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageBankDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLA + System.Environment.NewLine);
        int BankResult = 0;
        try
        {
            //objCommonObj = (CommonObject)Session["objCommonObj"];
            // Assign bank section control value to replica xml bank class.
            lblErrorbankdtls.Text = string.Empty;
            BankSection objBankSection = new BankSection();
            objBankSection.ClientName = txtBnkClientname.Text;
            objBankSection.ClientType = txtBnkClienttype.Text;
            objBankSection.ClientNumber = txtBnkClientnumber.Text;
            objBankSection.IFSCCode = txtBnkIfsccode.Text;
            objBankSection.BankName = txtBnkBankname.Text;
            objBankSection.BankBranch = txtBnkBranchname.Text;
            objBankSection.BankAddress = txtBnkBankaddress.Text;
            objBankSection.BankAccountNumber = txtBnkBankaccno.Text;
            objBankSection.Section = "BankDetails";

            Logger.Info(strApplicationno + "MethodeName :ManageBankDetails Object" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "MethodeName :ManageBankDetails Object" + GetXMLFromObject(objBankSection) + System.Environment.NewLine);
            //Add Bank object to replica Xml.
            objReplica.BankSection = objBankSection;
            //check weather value of bank section to be updated in database or save in xml.
            if (blnUpdateInLA)
            {
                Logger.Info(strApplicationno + "remark : Save bank details to database " + " STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ManageBankDetails" + System.Environment.NewLine);
                //Save bank details section data in database
                objComm.OnlineBankDetails_Save(strApplicationno, txtBnkClientnumber.Text, txtBnkIfsccode.Text, txtBnkBankname.Text, txtBnkBranchname.Text, txtBnkBankaddress.Text, txtBnkBankaccno.Text, txtBnkClientname.Text, ref BankResult);
                if (BankResult != -1)
                {
                    Logger.Info(strApplicationno + "Remark : Save bank details save in database" + System.Environment.NewLine);
                    chkBankDtls.Checked = false;
                    isdataSave = true;
                    //lblErrorbankdtls.Text = "Bank Details save successfully";
                }
                else
                {
                    isdataSave = false;
                    Logger.Info(strApplicationno + "Remark : Save bank details Not save in database" + System.Environment.NewLine);
                    //lblErrorbankdtls.Text = "Bank Details Not Save ";
                    //lblErrorBankDetailsBody.Text = "Bank Details Not save";
                }
            }
            else
            {

                Logger.Info(strApplicationno + "Remark : bank object save in Xml" + System.Environment.NewLine);
                //lblErrorbankdtls.Text = "Bank Details save successfully";
                //lblErrorBankDetailsBody.Text = "Bank Details save successfully";
            }
            Logger.Info(strApplicationno + "Remark:" + "Bank details End " + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageBankDetails" + System.Environment.NewLine);
        }
        catch (Exception ErrorBank)
        {
            Logger.Info(strApplicationno + "Remark:" + "Exception Bank details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageBankDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + ErrorBank.Message + System.Environment.NewLine);
            strErrorMessage = "Error While Saving Bank Details ,Please Contact System Admin";
            isdataSave = false;
        }
        Logger.Info(strApplicationno + "STEP :2 Save BANKDETAILS End" + System.Environment.NewLine);
    }

    private void ManageProductDetails(bool blnUpdateInLa, ref bool isdataSave, ReplicaXml objReplica)
    {
        Logger.Info(strApplicationno + "Remark:" + "Product details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLa + System.Environment.NewLine);
        try
        {
            if (blnUpdateInLa)
            {
                Logger.Info(strApplicationno + "Remark : product details save in database is begins" + System.Environment.NewLine);
                string strComboMonthlyPayout = string.Empty;
                string strProdMonthlyPayout = string.Empty;
                lblErrorProductDetailBody.Text = lblErrorproddtls.Text = "";
                gridPremCal_Product.DataSource = null;
                gridPremCal_Product.DataBind();
                //  objCommonObj = (CommonObject)Session["objCommonObj"];
                lblErrorproddtls.Text = string.Empty;
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                // Logger.Info(strApplicationno + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :btnProddtlsSave_Click" + System.Environment.NewLine);
                ProdDtls objprodchangevalue = new ProdDtls();

                objprodchangevalue._PolicyTerm = Request.Form[txtPolterm.UniqueID];
                objprodchangevalue._Premiumpayingterm = Request.Form[txtPrepayterm.UniqueID];
                objprodchangevalue._Sumassured = Request.Form[txtSumassure.UniqueID];
                objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
                strProdMonthlyPayout = objprodchangevalue._MonthlyPayoutBase = Request.Form[txtMonthlyPayoutBase.UniqueID];
                objprodchangevalue._ProdcodeBase = txtProdcode.Text;
                objprodchangevalue._Basepremiumamount = Request.Form[txtBasepremium.UniqueID];
                objprodchangevalue._TotalPremiumamount = txtTotalpremium.Text;
                if (!string.IsNullOrEmpty(txtCombProdCode.Text))
                {
                    objprodchangevalue._ProdcodeCombo = Request.Form[txtCombProdCode.UniqueID];
                    objprodchangevalue._PolicyTermCombo = Request.Form[txtCombPolTerm.UniqueID];
                    objprodchangevalue._PremiumpayingtermCombo = Request.Form[txtCombPayTerm.UniqueID];
                    objprodchangevalue._SumassuredCombo = Request.Form[txtCombSumAssured.UniqueID];
                    objprodchangevalue._BasepremiumamountCombo = txtCombPremAmount.Text;
                    objprodchangevalue._TotalPremiumamountCombo = txtCombTotalPrem.Text;
                    strComboMonthlyPayout = objprodchangevalue._MonthlyPayoutCombo = Request.Form[txtComboMonthlyPayout.UniqueID];
                }
                objChangeObj.Prod_policydetails = objprodchangevalue;

                Logger.Info(strApplicationno + "Remark : product details save in xml" + System.Environment.NewLine + objprodchangevalue + System.Environment.NewLine);

                #region Premium calculation Service call begin.
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);

                List<RiderInfo> lstRiderInfo = new List<RiderInfo>();
                Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstRiderInfo, _ds.Tables[0]);

                if (strLApushErrorCode == 0 && _ds.Tables.Count > 0)
                {
                    lblErrorProductDetailBody.Text = "Premium calculation succeed";
                    //clsName = divPremiumdetails.Attributes["class"].ToString();
                    //divPremiumdetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                    Logger.Info(strApplicationno + "Remark : product Premium calculation success while save to database" + System.Environment.NewLine);
                    RiderInfo objrider = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtProdcode.Text)).SingleOrDefault();
                    if (objrider != null)
                    {
                        txtBasepremium.Text = objprodchangevalue._Basepremiumamount = objrider.InstalmentPremiumAmt.ToString();
                        txtTotalpremium.Text = objprodchangevalue._TotalPremiumamount = objrider.TotalPremiumAmount.ToString();
                        txtServicetax.Text = objprodchangevalue._ServiceTax = objrider.ServiceTax.ToString();
                        txtSumassure.Text = objprodchangevalue._Sumassured = objrider.SumAssured.ToString();
                    }
                    RiderInfo objComb = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtCombProdCode.Text)).SingleOrDefault();
                    if (objComb != null)
                    {
                        txtCombPremAmount.Text = objprodchangevalue._BasepremiumamountCombo = objComb.InstalmentPremiumAmt.ToString();
                        txtCombTotalPrem.Text = objprodchangevalue._TotalPremiumamountCombo = objComb.TotalPremiumAmount.ToString();
                        txtCombServiceTax.Text = objprodchangevalue._ServiceTaxCombo = objComb.ServiceTax.ToString();
                        txtCombSumAssured.Text = objprodchangevalue._SumassuredCombo = objComb.SumAssured.ToString();
                        txtComboMonthlyPayout.Text = string.IsNullOrEmpty(strComboMonthlyPayout) ? "0" : strComboMonthlyPayout;
                    }

                    objChangeObj.Prod_policydetails = objprodchangevalue;
                    Logger.Info(strApplicationno + "Remark : product details save with Rider details in xml" + System.Environment.NewLine + objprodchangevalue + System.Environment.NewLine);

                    #region Call Contract Modification Service Begins.
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                    if (strLApushErrorCode == 0)
                    {
                        Logger.Info(strApplicationno + "Remark : Contract modification success to modify contract" + System.Environment.NewLine);
                        lblErrorProductDetailBody.Text += ",Contract modification succeed";
                        //objComm.OnlineProductDetails_Save(strChannelType, strApplicationno, strPolicyNo, txtProdcode.Text, txtSumassure.Text, txtPolterm.Text, txtPrepayterm.Text, ddlFrequency.SelectedValue, txtBasepremium.Text, txtTotalpremium.Text, txtBasepremium.Text, txtMonthlyPayoutBase.Text, ref _ProdResult);
                        chkProductDtls.Checked = false;
                        int _RiderResult = 0;
                        lblErrorProductDetailBody.Text += ",Product Details save succeed";
                        //Amit 08062017 Start

                        //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, "RATEUP", ref strLApushErrorCode, ref strLApushStatus);
                        //if (strLApushErrorCode == 0)
                        //{
                        //    lblErrorProductDetailBody.Text += ",Loading modification succeed";
                        //}
                        if (gvRiderDtls.Rows.Count > 0)
                        {
                            Logger.Info(strApplicationno + "Remark : Rider details save to database" + System.Environment.NewLine);
                            foreach (GridViewRow rows in gvRiderDtls.Rows)
                            {
                                Label lblRiderCode = (Label)rows.FindControl("lblRiderCode");
                                TextBox txtRiderSumAssure = (TextBox)rows.FindControl("txtRiderSumAssure");
                                TextBox txtRiderPremium = (TextBox)rows.FindControl("txtRiderPremium");
                                objComm.OnlineRiderDetails_Save(strChannelType, strApplicationno, lblRiderCode.Text, txtRiderPremium.Text, txtRiderSumAssure.Text, ref _RiderResult);
                            }
                            if (_RiderResult != -1)
                            {
                                chkProductDtls.Checked = false;
                                Logger.Info(strApplicationno + "Remark : Rider details save to database successfully" + System.Environment.NewLine);
                                //lblErrorProductDetailBody.Text += ",Product Details save succeed";
                                // lblErrorproddtls.Text = "Product Details Updated Successfully";
                            }
                            else
                            {
                                chkProductDtls.Checked = false;
                                Logger.Info(strApplicationno + "Remark : Rider details not save to database" + System.Environment.NewLine);
                                //lblErrorProductDetailBody.Text += "Product Details failed,Please Contact system admin";
                                //lblErrorproddtls.Text = "Product Details Not Updated Successfully";
                            }
                        }
                        //Amit 08062017 End.

                    }
                    else
                    {
                        Logger.Info(strApplicationno + "Remark : Contract modification Failed to modify contract" + System.Environment.NewLine);
                        FillProductDetails(strApplicationno, strChannelType);
                        chkProductDtls.Checked = false;
                        //lblErrorproddtls.Text = "Product Details Not Updated Successfully";
                        //lblErrorProductDetailBody.Text += ",Contract modification failed Error:" + strLApushStatus;
                        //lblErrorProductDetailBody.Text += ",Contract modification failed,Please Contact system admin";

                    }
                    #endregion Call Contract Modification Service End.
                }

                //Amit 08062017 start.
                else if (strLApushErrorCode == 1)
                {
                    Logger.Info(strApplicationno + "Remark : product Premium calculation failed while save to database" + System.Environment.NewLine);
                    chkProductDtls.Checked = false;
                    //lblErrorproddtls.Text = strLApushStatus;
                    //gridPremiumdetails.DataSource = null;
                    //gridPremiumdetails.DataBind();
                    divPremiumdetails.Attributes.Add("class", "col-md-12 HideControl");
                }
                //Amit 08062017 end.
                else
                {
                    chkProductDtls.Checked = false;
                    Logger.Info(strApplicationno + "Remark : product Premium calculation failed while save to database" + System.Environment.NewLine);
                    //lblErrorproddtls.Text = "Product Details Not Updated Successfully";
                    //lblErrorProductDetailBody.Text = "Premium calculation failed,Please Contact system admin";
                    divPremiumdetails.Attributes.Add("class", "col-md-12 HideControl");
                }
                #endregion calculation service call end
                /*added by shri to show updated monthly payout*/
                txtMonthlyPayoutBase.Text = string.IsNullOrEmpty(strProdMonthlyPayout) ? "0" : strProdMonthlyPayout;
                /*end here*/
            }
            else
            {
                Logger.Info(strApplicationno + "Remark:" + "Product details save to xml" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductDetails" + System.Environment.NewLine);
                List<ProductSection> lstProductSection = new List<ProductSection>();
                string strComboMonthlyPayout = string.Empty;
                string strProdMonthlyPayout = string.Empty;
                lblErrorProductDetailBody.Text = lblErrorproddtls.Text = "";
                gridPremCal_Product.DataSource = null;
                gridPremCal_Product.DataBind();
                lblErrorproddtls.Text = string.Empty;
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                Logger.Info(strApplicationno + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :btnProddtlsSave_Click" + System.Environment.NewLine);
                ProdDtls objprodchangevalue = new ProdDtls();
                ProductSection objProductSectionBase = new ProductSection();
                ProductSection objProductSectionCombo = new ProductSection();

                objProductSectionBase.ProductCode = objprodchangevalue._ProdcodeBase = txtProdcode.Text;
                objProductSectionBase.PolicyTerm = objprodchangevalue._PolicyTerm = Request.Form[txtPolterm.UniqueID];
                objProductSectionBase.PremiumTerm = objprodchangevalue._Premiumpayingterm = Request.Form[txtPrepayterm.UniqueID];
                objProductSectionBase.SumAssured = objprodchangevalue._Sumassured = Request.Form[txtSumassure.UniqueID];
                objProductSectionBase.PremiumFreq = objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
                objProductSectionBase.BasePremium = objprodchangevalue._Basepremiumamount = Request.Form[txtBasepremium.UniqueID];
                objProductSectionBase.TotalPremium = objprodchangevalue._TotalPremiumamount = txtTotalpremium.Text;
                objProductSectionBase.MonthlyPayout = strProdMonthlyPayout = objprodchangevalue._MonthlyPayoutBase = Request.Form[txtMonthlyPayoutBase.UniqueID];
                objProductSectionBase.MonthlyPayout = (string.IsNullOrEmpty(objProductSectionBase.MonthlyPayout)) ? string.Empty : objProductSectionBase.MonthlyPayout;
                objProductSectionBase.ProductType = hdnProductType.Value;
                objProductSectionBase.ProdcutName = txtProname.Text;
                objProductSectionBase.PolicyNo = txtBasepolno.Text;
                objProductSectionBase.Section = "ProductDetails";
                Logger.Info(strApplicationno + "MethodeName :ManageProductDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "Xml data for Product--base Plan" + objProductSectionBase + System.Environment.NewLine);
                if (!string.IsNullOrEmpty(txtCombProdCode.Text))
                {
                    objProductSectionCombo.ProductCode = objprodchangevalue._ProdcodeCombo = Request.Form[txtCombProdCode.UniqueID];
                    objProductSectionCombo.PolicyTerm = objprodchangevalue._PolicyTermCombo = Request.Form[txtCombPolTerm.UniqueID];
                    objProductSectionCombo.PremiumTerm = objprodchangevalue._PremiumpayingtermCombo = Request.Form[txtCombPayTerm.UniqueID];
                    objProductSectionCombo.SumAssured = objprodchangevalue._SumassuredCombo = Request.Form[txtCombSumAssured.UniqueID];
                    objProductSectionCombo.PremiumFreq = objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
                    objProductSectionCombo.BasePremium = objprodchangevalue._BasepremiumamountCombo = txtCombPremAmount.Text;
                    objProductSectionCombo.TotalPremium = objprodchangevalue._TotalPremiumamountCombo = txtCombTotalPrem.Text;
                    objProductSectionCombo.MonthlyPayout = strComboMonthlyPayout = objprodchangevalue._MonthlyPayoutCombo = Request.Form[txtComboMonthlyPayout.UniqueID];
                    objProductSectionCombo.MonthlyPayout = (string.IsNullOrEmpty(objProductSectionCombo.MonthlyPayout)) ? string.Empty : objProductSectionCombo.MonthlyPayout;
                    objProductSectionCombo.ProductType = hdnProductType.Value;
                    objProductSectionCombo.PolicyNo = txtCombopolno.Text;
                    objProductSectionCombo.ProdcutName = txtCombProdName.Text;
                    objProductSectionCombo.PremiumFreq = ddlComboFrequency.SelectedValue;
                    objProductSectionCombo.Section = "ProductDetails";
                    Logger.Info(strApplicationno + "MethodeName :ManageProductDetails Object" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "Xml data for Product--Combo Plan" + objProductSectionCombo + System.Environment.NewLine);
                }
                objChangeObj.Prod_policydetails = objprodchangevalue;


                #region Premium calculation Service call begin.
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);

                List<RiderInfo> lstRiderInfo = new List<RiderInfo>();
                Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstRiderInfo, _ds.Tables[0]);

                if (strLApushErrorCode == 0 && _ds.Tables.Count > 0)
                {
                    Logger.Info(strApplicationno + "Remark : Premium calculation success to save data in xml" + System.Environment.NewLine);
                    lblErrorProductDetailBody.Text = "Premium calculation succeed";
                    RiderInfo objProd = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtProdcode.Text)).SingleOrDefault();
                    if (objProd != null)
                    {
                        Logger.Info(strApplicationno + "Remark : Rider details  save data in xml begin" + System.Environment.NewLine);
                        objProductSectionBase.BasePremium = txtBasepremium.Text = objprodchangevalue._Basepremiumamount = objProd.InstalmentPremiumAmt.ToString();
                        objProductSectionBase.TotalPremium = txtTotalpremium.Text = objprodchangevalue._TotalPremiumamount = objProd.TotalPremiumAmount.ToString();
                        objProductSectionBase.ServiceTax = txtServicetax.Text = objprodchangevalue._ServiceTax = objProd.ServiceTax.ToString();
                        objProductSectionBase.SumAssured = txtSumassure.Text = objprodchangevalue._Sumassured = objProd.SumAssured.ToString();
                        //objComm.OnlineProductDetails_Save(strChannelType, strApplicationno, strPolicyNo, txtProdcode.Text, txtSumassure.Text, txtPolterm.Text, txtPrepayterm.Text
                        //    , ddlFrequency.SelectedValue, txtBasepremium.Text, txtTotalpremium.Text, txtServicetax.Text, txtMonthlyPayoutBase.Text, ref _ProdResult);
                        //if (_ProdResult > 0)
                        //{                        
                        //lblErrorproddtls.Text = "Product Details Updated Successfully";
                        //lblErrorProductDetailBody.Text += ",Product Details save succeed";
                        //}
                        //else
                        //{
                        //lblErrorproddtls.Text = "Product Details Not Updated Successfully CLICK here to know more";
                        //lblErrorProductDetailBody.Text += ",Product Details NOT save in database";
                        //}
                        lstProductSection.Add(objProductSectionBase);
                    }
                    RiderInfo objComb = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtCombProdCode.Text)).SingleOrDefault();
                    if (objComb != null)
                    {
                        objProductSectionCombo.BasePremium = txtCombPremAmount.Text = objprodchangevalue._BasepremiumamountCombo = objComb.InstalmentPremiumAmt.ToString();
                        objProductSectionCombo.TotalPremium = txtCombTotalPrem.Text = objprodchangevalue._TotalPremiumamountCombo = objComb.TotalPremiumAmount.ToString();
                        objProductSectionCombo.ServiceTax = txtCombServiceTax.Text = objprodchangevalue._ServiceTaxCombo = objComb.ServiceTax.ToString();
                        objProductSectionCombo.SumAssured = txtCombSumAssured.Text = objprodchangevalue._SumassuredCombo = objComb.SumAssured.ToString();
                        objProductSectionCombo.MonthlyPayout = txtComboMonthlyPayout.Text = strComboMonthlyPayout;
                        lstProductSection.Add(objProductSectionCombo);
                        //objComm.OnlineProductDetails_Save(strChannelType, strApplicationno, strPolicyNo, txtCombProdCode.Text, txtCombSumAssured.Text, txtPolterm.Text, txtPrepayterm.Text
                        //    , ddlFrequency.SelectedValue, txtCombPremAmount.Text, txtCombTotalPrem.Text, txtServicetax.Text, txtComboMonthlyPayout.Text, ref _ProdResult);
                        //if (_ProdResult > 0)
                        //{
                        //    lblErrorProductDetailBody.Text += ",Combo Product Details save succeed";
                        //}
                        //else
                        //{
                        //    lblErrorProductDetailBody.Text += ",Combo Product Details NOT save in database";
                        //}

                    }

                    objChangeObj.Prod_policydetails = objprodchangevalue;
                    chkProductDtls.Checked = false;
                    //lblErrorProductDetailBody.Text += ",Product Details save succeed";
                    #endregion Call Contract Modification Service End.
                }

                //Amit 08062017 start.
                else if (strLApushErrorCode == 1)
                {
                    chkProductDtls.Checked = false;
                    lblErrorproddtls.Text = strLApushStatus;
                    gridPremiumdetails.DataSource = null;
                    gridPremiumdetails.DataBind();
                    divPremiumdetails.Attributes.Add("class", "col-md-12 HideControl");
                }
                //Amit 08062017 end.
                else
                {
                    Logger.Info(strApplicationno + "Remark Premium calculation failed while save data to xml" + System.Environment.NewLine);
                    chkProductDtls.Checked = false;
                    //lblErrorproddtls.Text = "Product Details Not Updated Successfully";
                    //lblErrorProductDetailBody.Text = "Premium calculation failed,Please Contact system admin";
                    divPremiumdetails.Attributes.Add("class", "col-md-12 HideControl");
                }
                /*added by shri to show updated monthly payout*/
                txtMonthlyPayoutBase.Text = string.IsNullOrEmpty(strProdMonthlyPayout) ? "0" : strProdMonthlyPayout;
                objReplica.LstProductSection = lstProductSection;
                //FillProductDetails(strApplicationno, strChannelType);
                /*end here*/
            }
        }
        catch (Exception ErrorProduct)
        {
            Logger.Info(strApplicationno + "Remark:" + "Exception Bank details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + ErrorProduct.Message + System.Environment.NewLine);
        }
    }
    /*end here*/
    private void ManageProductRiderDetails(bool blnUpdateInLa, ref bool isdataSave, ReplicaXml objReplica, ref string strErrorMessage)
    {
        Logger.Info(strApplicationno + "STEP :3 Save ProductRiderDetails Start" + System.Environment.NewLine);
        try
        {
            Logger.Info(strApplicationno + "Remark:" + "Product-rider-Application details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductRiderDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLa + System.Environment.NewLine);
            lblRiderDetailsError.Text = lblErrorRiderDetailsBody.Text = string.Empty;
            try
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                DataTable dtRiderInfo = new DataTable();
                DefineDataTable(ref dtRiderInfo);
                bool blnIsGreateThanSumAssured = true;
                int intResponse = -1;
                //check if entered amount is greater than sum assured             
                if (blnIsGreateThanSumAssured)
                {
                    DataSet _dsPremiumCalculation = new DataSet();
                    /*commented by shri no need to call premium calculation*/
                    //PremiumCalculation(objReplica, ref isdataSave, ref _dsPremiumCalculation, ref intResponse, ref strResponse);
                    //if (string.IsNullOrEmpty(strResponse))
                    //{
                    /*back date interest*/
                    Logger.Info(strApplicationno + "Remark :ManageProductRiderDetails: Premium calculation success" + System.Environment.NewLine);
                    txtBackDateIntrest.Text = objReplica.ApplicationSection.BACKDATEINTREST;
                    if (objReplica != null && objReplica.LstRiderInfo != null && objReplica.LstRiderInfo.Count > 0)
                    {
                        List<RiderSection> LstRiderSection = new List<RiderSection>();
                        for (int i = 0; i < objReplica.LstRiderInfo.Count; i++)
                        {
                            RiderSection objRiderSection = new RiderSection();
                            RiderInfo objRiderInfo = objReplica.LstRiderInfo[i];

                            //index of old value
                            var index = LstRiderSection.IndexOf(objRiderSection);

                            //fill rider section
                            objRiderSection.Section = "RiderDetails";
                            objRiderSection.RIDERCODE = objRiderInfo.RiderId;
                            objRiderSection.RIDERSUMASSURED = Convert.ToString(objRiderInfo.SumAssured);
                            objRiderSection.RiderTotalPremium = Convert.ToString(objRiderInfo.TotalInstalmentPremium);
                            objRiderSection.SERVICETAX = Convert.ToString(objRiderInfo.ServiceTax);
                            objRiderSection.IsActive = Convert.ToBoolean(objRiderInfo.IsActive);
                            objRiderSection.RIDERNAME = objRiderInfo.RiderName;
                            LstRiderSection.Add(objRiderSection);
                        }
                        objReplica.LstRiderSection = LstRiderSection;
                        Logger.Info(strApplicationno + "MethodeName :Rider Object" + System.Environment.NewLine);
                        Logger.Info(strApplicationno + "MethodeName :Rider Object" + GetXMLFromObject(LstRiderSection) + System.Environment.NewLine);
                        lblErrorRiderDetailsBody.Text = "Rider Updation Success";
                        lblRiderDetailsError.Text = "Rider Updation Success";


                        if (blnUpdateInLa)
                        {

                            Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Rider Details save in database Begin" + System.Environment.NewLine);
                            /*added by shri to update rider details*/
                            string struserid = objChangeObj.userLoginDetails._UserID;
                            DataTable dtDbRiderInfo = new DataTable();
                            DefineDataTable(ref dtDbRiderInfo);
                            dtDbRiderInfo.Columns.Add("UserId");
                            dtDbRiderInfo.Columns.Add("CurrentDate");
                            dtDbRiderInfo.Columns.Add("ProcessName");
                            for (int i = 0; i < objReplica.LstRiderSection.Count; i++)
                            {
                                RiderSection objRiderSection = LstRiderSection[i];
                                DataRow dr = dtDbRiderInfo.NewRow();

                                //get value from it
                                dr[0] = strApplicationno;
                                dr[1] = objRiderSection.IsActive;
                                dr[2] = objRiderSection.RIDERNAME;
                                dr[3] = objRiderSection.RIDERCODE;
                                dr[4] = objRiderSection.RIDERSUMASSURED;
                                dr[5] = objRiderSection.RiderTotalPremium;
                                dr[6] = objRiderSection.SERVICETAX;
                                dr[7] = objRiderSection.ProductType;
                                dr[8] = objRiderSection.ProductCode;
                                dr[9] = objRiderSection.ProdPoliyTerm;
                                dr[10] = objRiderSection.ProdPremPayingTerm;
                                dr[11] = objRiderSection.RIDERSUMASSURED;
                                dr[12] = struserid;
                                dr[13] = DateTime.Now;
                                dr[13] = "UWSaral";

                                //add data row to datatable
                                dtDbRiderInfo.Rows.Add(dr);
                            }

                            Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Rider Details save in database Begin" + System.Environment.NewLine);
                            objcomm.ManageRiderInfo(strApplicationno, dtDbRiderInfo, struserid, ref intResponse);
                            if (intResponse > 0)
                            {
                                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Rider Details save in database Success" + System.Environment.NewLine);
                                //lblErrorRiderDetailsBody.Text += " Rider Updated Successfuly In Database";
                                //lblRiderDetailsError.Text += " Rider Updated Successfuly In Database";
                            }
                            else
                            {
                                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Rider Details Not save in database Success" + System.Environment.NewLine);
                                //lblErrorRiderDetailsBody.Text += " Rider Not Updated In Database ";
                                //lblRiderDetailsError.Text += " Rider Not Updated In Database Click Here To Know More";
                            }
                        }
                    }
                    if (blnUpdateInLa)
                    {
                        /*added by shri to update application details*/
                        Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Application Details save in database Begin" + System.Environment.NewLine);
                        //check if backdating is done or not if yes then call save details 
                        if (switch_havInsurance.Checked)
                        {
                            objComm.OnlineApplicationDetails_Save(strPolicyNo, strApplicationno, "Yes", Request.Form[txtRcdreq.UniqueID], ddlBkdateReason.SelectedItem.Value
                               , objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._userBranch
                               , objChangeObj.userLoginDetails._userBranch, "UWSaralUnderwritting", strApplicationno, objChangeObj.userLoginDetails._UserID
                               , strChannelType, txtBackDateIntrest.Text, ref intResponse);
                        }
                        if (intResponse > 0)
                        {
                            Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Application Details save in database Success" + System.Environment.NewLine);
                            //lblErrorAppDetailsBody.Text = " Application Details Updated In Database";
                            //lblErrorappdtls.Text = " Application Details Updated In Database";
                        }
                        else
                        {
                            Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Application Details Not save in database" + System.Environment.NewLine);
                            //lblErrorAppDetailsBody.Text += " Application Details Not Updated In Database Click here to know more ";
                            //lblErrorappdtls.Text += " Application Details Not Updated In Database Click here to know more ";
                        }

                        /*added by shri to update product details*/
                        for (int i = 0; i < objReplica.LstProductSection.Count; i++)
                        {
                            Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Product Details save in database Begin" + System.Environment.NewLine);
                            ProductSection objProductSection = objReplica.LstProductSection[i];
                            objComm.OnlineProductDetails_Save(strChannelType, strApplicationno, strPolicyNo, objProductSection.ProductCode, objProductSection.SumAssured
                                , objProductSection.PolicyTerm, objProductSection.PremiumTerm, objProductSection.PremiumFreq, objProductSection.BasePremium
                                , objProductSection.TotalPremium, objProductSection.ServiceTax, objProductSection.MonthlyPayout
                                , (ddlApplicationDetailsProposalType.SelectedValue == "0") ? string.Empty : Convert.ToString(ddlApplicationDetailsProposalType.SelectedValue)
                                , txtApplicationDetailsCibil.Text
                                , ref intResponse);
                            if (intResponse > 0)
                            {
                                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Product Details save in database Success" + System.Environment.NewLine);
                                //lblErrorproddtls.Text = lblErrorproddtls.Text + " Product Details Updated Successfully In Database";
                                //lblErrorProductDetailBody.Text = lblErrorProductDetailBody.Text + " Product Details Updated Successfully In Database";


                            }
                            else
                            {
                                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Product Details not save in database" + System.Environment.NewLine);
                                //lblErrorproddtls.Text = lblErrorproddtls.Text + " Product Details Updated Not In Database Click here to know more ";
                                //lblErrorProductDetailBody.Text = lblErrorProductDetailBody.Text + " Product Details Updated In Database Not Click here to know more ";
                            }

                            //added by suraj for update payout details for product code T36/37/38, E91/92/93/94
                            objcomm.ManagePayoutDetail(strApplicationno, ddlCategory.SelectedValue, ddlPayoutType.SelectedValue, ddlPayoutTerm.SelectedValue
                                , txtLumpSumPercent.Text, ddlpayoutfreq.SelectedValue, ddlprodcategory.SelectedValue, ref intResponse);
                            if (intResponse > 0)
                            {
                                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Payout Details save in database Success" + System.Environment.NewLine);
                            }
                            else
                            {
                                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Payout Details Not save in database Success" + System.Environment.NewLine);
                            }
                        }
                    }
                    //}
                    //else
                    //{
                    //    Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Rider Details not save in database" + System.Environment.NewLine);
                    //    //lblErrorRiderDetailsBody.Text = strResponse;
                    //    //lblRiderDetailsError.Text = "Rider Updation Failed To Know More CLICK Here";
                    //}

                }



                else
                {
                    Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Rider Sum Assured Cannot Be Greater Than Produce Sum Assured" + System.Environment.NewLine);
                    //lblRiderDetailsError.Text = "Rider Sum Assured Cannot Be Greater Than Produce Sum Assured";
                }
            }

            catch (Exception ex)
            {
                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Exception in ManageProductRiderDetails" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + ex.Message + System.Environment.NewLine);
                //lblRiderDetailsError.Text = "Rider Updated Successfully CLICK here to know more";
                //lblErrorRiderDetailsBody.Text = ex.Message; ;
            }
        }
        catch (Exception ErrorRider)
        {
            strErrorMessage = "Error While Saving Productrider Details,Please Contact System Admin";
            isdataSave = false;
            Logger.Info(strApplicationno + "Remark:" + "Exception Bank details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + ErrorRider.Message + System.Environment.NewLine);
        }
        Logger.Info(strApplicationno + "STEP :3 Save ProductRiderDetails End" + System.Environment.NewLine);
    }

    private void ManageRequirementDetails(bool blnUpdateInLa, ref bool isdataSave, ReplicaXml objReplica, ref string strErrorMessage)
    {
        Logger.Info(strApplicationno + "STEP :5 Save requirments details Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Requirment details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageRequirementDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLa + System.Environment.NewLine);
        try
        {
            List<RequirmentSection> LstRequirmentSection = new List<RequirmentSection>();
            if (blnUpdateInLa)
            {
                Logger.Info(strApplicationno + "Remark : Save requirment details in database begins" + System.Environment.NewLine);
                Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnRequirmentDtlsSave_Click");
                string _strfollowupcode = string.Empty;
                string _strfollowupdiscp = string.Empty;
                string _strfollowupcategory = string.Empty;
                string _strfollowupcriteria = string.Empty;
                string _strfollowuplifetype = string.Empty;
                string _strfollowupstatus = string.Empty;
                int strFollowupResult = 0;
                bool _strFolloUPflag = true;
                lblErrorRequirementDetailBody.Text = lblErrorreqdtls.Text = string.Empty;
                // objCommonObj = (CommonObject)Session["objCommonObj"];
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                foreach (GridViewRow rowfollowup in gvRequirmentDetails.Rows)
                {
                    RequirmentSection objRequirmentSection = new RequirmentSection();
                    objRequirmentSection.Section = "RequirmentDetails";
                    DropDownList ddlfollowupcode = rowfollowup.FindControl("ddlfollowupcode") as DropDownList;
                    DropDownList ddlCategory = rowfollowup.FindControl("ddlCategory") as DropDownList;
                    DropDownList ddlCriteria = rowfollowup.FindControl("ddlCriteria") as DropDownList;
                    DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;
                    DropDownList ddlLifeType = rowfollowup.FindControl("ddlLifeType") as DropDownList;
                    TextBox lblfollowupDiscp = rowfollowup.FindControl("lblfollowupDiscp") as TextBox;
                    objRequirmentSection.REQ_followUpCode = _strfollowupcode = ddlfollowupcode.SelectedValue;
                    objRequirmentSection.REQ_category = _strfollowupcategory = ddlCategory.SelectedValue;
                    objRequirmentSection.REQ_criteria = _strfollowupcriteria = ddlCriteria.SelectedValue;
                    objRequirmentSection.REQ_lifeType = _strfollowuplifetype = ddlLifeType.SelectedValue;
                    objRequirmentSection.REQ_status = _strfollowupstatus = ddlStatus.SelectedValue;
                    objRequirmentSection.REQ_description = _strfollowupdiscp = lblfollowupDiscp.Text;
                    if (_strfollowupstatus == "O")
                    {
                    }
                    /*save to database*/
                    Logger.Info(strApplicationno + "MethodeName :ManageRequirementDetails" + "requirment details save in Db for followup code " + ddlfollowupcode.SelectedValue + System.Environment.NewLine);
                    if (!_strfollowupcode.Equals("0"))
                    {
                        objBuss.FollowupDetails_Save(strChannelType, strApplicationno, _strfollowupcode, _strfollowupdiscp, _strfollowupcategory, _strfollowupcriteria, ""
                                        , objChangeObj.userLoginDetails._UserID, _strfollowupstatus, _strfollowupcategory, _strfollowuplifetype, objChangeObj.userLoginDetails._UserID
                                        , objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch
                                        , objChangeObj.userLoginDetails._userBranch, strChannelType, strApplicationno, ref strFollowupResult);
                    }
                    if (strFollowupResult == 0)
                    {
                        _strFolloUPflag = false;
                    }
                    LstRequirmentSection.Add(objRequirmentSection);
                    Logger.Info(strApplicationno + "MethodeName :ManageRequirementDetails Object" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "MethodeName :ManageRequirementDetails Object" + GetXMLFromObject(objRequirmentSection) + System.Environment.NewLine);
                }
                if (_strFolloUPflag)
                {
                    Logger.Info(strApplicationno + "MethodeName :ManageRequirementDetails Object" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "Remark :requirment details save successfully in database" + System.Environment.NewLine);
                    /*ADDED BY SHRI ON 04 NOV 17 TO PUSH MEDICAL LOADING*/
                    UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
                    objCommFun.FetchMedicalRequirement(strApplicationno, ref _ds);
                    if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        UWSaralServices.MedicalDataEntryInvoke objMedicalDataEntryInvoke = new UWSaralServices.MedicalDataEntryInvoke();
                        objMedicalDataEntryInvoke.MedicalPushService(strApplicationno, ref strLApushErrorCode, ref strLApushStatus);
                        if (strLApushErrorCode == 0)
                        {
                            int intRet = -1;
                            objCommFun.UpdateMedicalRequirement(strApplicationno, ref intRet);
                        }
                    }
                    /*END HERE*/
                    //lblErrorRequirementDetailBody.Text = "Requirment details Updated in DataBase";
                    //lblErrorreqdtls.Text = "Requirment details Updated CLICK to know more";
                }
                else
                {
                    chkReqDtls.Checked = false;
                    Logger.Info(strApplicationno + "Remark :requirment details Not save successfully" + System.Environment.NewLine);
                    //lblErrorRequirementDetailBody.Text = "Requirment details Not Updated in DataBase";
                    //lblErrorreqdtls.Text = "Requirment details Not Updated CLICK to know more";
                }
                //Added by Suraj on 19 APRIL 2018 for medical reason dropdown
                int resp = 0;
                if (ddlRequirementMedicalReason.SelectedValue == "0")
                {
                    //lblErrorreqdtls.Text = "Please select Medical Reason!!";
                    //ShowPopupMessage("Please select Medical Reason!!");
                }
                else
                {
                    lblErrorreqdtls.Text = "";
                    //objBuss.Save_MedicalReport_Reason(strApplicationno, Convert.ToInt32(ddlRequirementMedicalReason.SelectedValue), objChangeObj.userLoginDetails._UserID, ref resp);
                    objBuss.Save_MedicalReport_Reason(1, strApplicationno, Convert.ToInt32(ddlRequirementMedicalReason.SelectedValue), objChangeObj.userLoginDetails._UserID, ref resp);
                }
            }
            else
            {
                Logger.Info(strApplicationno + "Remark : Save requirment details in Xml begins" + System.Environment.NewLine);
                Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ManageRequirementDetails");
                string _strfollowupcode = string.Empty;
                string _strfollowupdiscp = string.Empty;
                string _strfollowupcategory = string.Empty;
                string _strfollowupcriteria = string.Empty;
                string _strfollowuplifetype = string.Empty;
                string _strfollowupstatus = string.Empty;
                string _strRaisedDate = string.Empty;
                string _strRaisedBy = string.Empty;
                string _strClosedDate = string.Empty;
                string _strClosedBy = string.Empty;
                bool _strFolloUPflag = true;
                lblErrorRequirementDetailBody.Text = lblErrorreqdtls.Text = string.Empty;
                // objCommonObj = (CommonObject)Session["objCommonObj"];
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                foreach (GridViewRow rowfollowup in gvRequirmentDetails.Rows)
                {
                    RequirmentSection objRequirmentSection = new RequirmentSection();
                    objRequirmentSection.Section = "RequirmentDetails";
                    DropDownList ddlfollowupcode = rowfollowup.FindControl("ddlfollowupcode") as DropDownList;
                    DropDownList ddlCategory = rowfollowup.FindControl("ddlCategory") as DropDownList;
                    DropDownList ddlCriteria = rowfollowup.FindControl("ddlCriteria") as DropDownList;
                    DropDownList ddlStatus = rowfollowup.FindControl("ddlStatus") as DropDownList;
                    DropDownList ddlLifeType = rowfollowup.FindControl("ddlLifeType") as DropDownList;
                    TextBox lblfollowupDiscp = rowfollowup.FindControl("lblfollowupDiscp") as TextBox;
                    Label lblRaiseddate = rowfollowup.FindControl("lblRaiseddate") as Label;
                    //Label lblRaisedby = rowfollowup.FindControl("lblRaisedby") as Label;
                    Label lblClosedDate = rowfollowup.FindControl("lblClosedDate") as Label;
                    //Label lblClosedBy = rowfollowup.FindControl("lblClosedBy") as Label;

                    objRequirmentSection.REQ_followUpCode = _strfollowupcode = ddlfollowupcode.SelectedValue;
                    objRequirmentSection.REQ_category = _strfollowupcategory = ddlCategory.SelectedValue;
                    objRequirmentSection.REQ_criteria = _strfollowupcriteria = ddlCriteria.SelectedValue;
                    objRequirmentSection.REQ_lifeType = _strfollowuplifetype = ddlLifeType.SelectedValue;
                    objRequirmentSection.REQ_status = _strfollowupstatus = ddlStatus.SelectedValue;
                    objRequirmentSection.REQ_description = _strfollowupdiscp = lblfollowupDiscp.Text;

                    objRequirmentSection.REQ_RaisedDate = _strRaisedDate = lblRaiseddate.Text;
                    //objRequirmentSection.REQ_RaisedBy = _strRaisedBy = lblRaisedby.Text;
                    objRequirmentSection.REQ_ClosedDate = _strClosedDate = lblClosedDate.Text;
                    //objRequirmentSection.REQ_ClosedBy = _strClosedBy = lblClosedBy.Text;

                    if (_strfollowupstatus == "O")
                    {
                    }

                    //objBuss.FollowupDetails_Save(strChannelType, strApplicationno, _strfollowupcode, _strfollowupdiscp, _strfollowupcategory, _strfollowupcriteria, "", objChangeObj.userLoginDetails._UserID, _strfollowupstatus,
                    //    _strfollowupcategory, _strfollowuplifetype, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, strChannelType, strApplicationno, ref strFollowupResult);
                    //if (strFollowupResult == )
                    //{
                    //    _strFolloUPflag = false;
                    //}
                    LstRequirmentSection.Add(objRequirmentSection);
                    Logger.Info(strApplicationno + "MethodeName :ManageRequirementDetails Object" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "MethodeName :ManageRequirementDetails Object" + GetXMLFromObject(objRequirmentSection) + System.Environment.NewLine);
                }

                if (_strFolloUPflag)
                {
                    /*ADDED BY SHRI ON 04 NOV 17 TO PUSH MEDICAL LOADING*/
                    Logger.Info(strApplicationno + "MethodeName :ManageRequirementDetails  remark : ths part is not required to save data in xml" + System.Environment.NewLine);

                    UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
                    objCommFun.FetchMedicalRequirement(strApplicationno, ref _ds);
                    if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {

                        objCommFun.FetchMedicalRequirement(strApplicationno, ref _ds);
                        //if record exists then call service 
                        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                        {
                            UWSaralServices.MedicalDataEntryInvoke objMedicalDataEntryInvoke = new UWSaralServices.MedicalDataEntryInvoke();
                            objMedicalDataEntryInvoke.MedicalPushService(strApplicationno, ref strLApushErrorCode, ref strLApushStatus);
                            if (strLApushErrorCode == 0)
                            {
                                int intRet = -1;
                                objCommFun.UpdateMedicalRequirement(strApplicationno, ref intRet);
                            }
                        }
                    }
                    /*END HERE*/
                    //lblErrorRequirementDetailBody.Text = "Requirment details Updated in DataBase";
                    //lblErrorreqdtls.Text = "Requirment details Updated CLICK to know more";
                }
                else
                {
                    chkReqDtls.Checked = false;
                    //lblErrorRequirementDetailBody.Text = "Requirment details Not Updated in DataBase";
                    //lblErrorreqdtls.Text = "Requirment details Not Updated CLICK to know more";
                }
            }
            if (LstRequirmentSection != null)
            {
                objReplica.LstRequirementSection = LstRequirmentSection;
                Logger.Info(strApplicationno + "MethodeName :RequirmentDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :RequirmentDetails" + GetXMLFromObject(LstRequirmentSection) + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :RequirmentDetails  Remark :Requirment object add to replica xml " + System.Environment.NewLine);
            }
        }
        catch (Exception ErrorRequirments)
        {
            strErrorMessage = "Error While Saving Requirments Details,Please Contact System Admin";
            Logger.Info(strApplicationno + "Remark:" + "Exception Requirments details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + ErrorRequirments.Message + System.Environment.NewLine);
        }
        Logger.Info(strApplicationno + "STEP :5 Save requirments details end" + System.Environment.NewLine);
    }

    private void ManageFundDetails(bool blnUpdateInLa, ref bool isdataSave, ReplicaXml objReplica, ref string strErrorMessage)
    {
        Logger.Info(strApplicationno + "STEP :5 Save Fund details Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Fund details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageFundDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLa + System.Environment.NewLine);
        try
        {
            List<FundSection> LstFundSection = new List<FundSection>();
            if (blnUpdateInLa)
            {
                Logger.Info(strApplicationno + "Remark : Save Fund details in database begins" + System.Environment.NewLine);
                Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ManageFundDetails");
                string _strFundCode = string.Empty;
                string _strFundName = string.Empty;
                string _strFundValue = string.Empty;
                int _FundResult = 0;
                lblErrorFundDetailBody.Text = lblErrorfunddtls.Text = string.Empty;
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                foreach (GridViewRow rowFund in gvFundDtls.Rows)
                {
                    FundSection objFundSection = new FundSection();
                    objFundSection.Section = "FundDetails";
                    Label lblFundCode = rowFund.FindControl("lblFundvalue") as Label;
                    //Label lblRaisedby = rowfollowup.FindControl("lblRaisedby") as Label;
                    Label lblFundName = rowFund.FindControl("lblFundName") as Label;
                    TextBox lblFundvalue = rowFund.FindControl("txtFundvalue") as TextBox;
                    //Label lblClosedBy = rowfollowup.FindControl("lblClosedBy") as Label;
                    objFundSection.FND_fundCode = _strFundCode = lblFundCode.Text;
                    objFundSection.FND_fundName = _strFundValue = lblFundvalue.Text;
                    objFundSection.FND_fundComposition = _strFundName = lblFundName.Text;

                    objComm.OnlineFundDetails(strChannelType, objCommonObj._AppType, strApplicationno, _strFundCode, _strFundName, _strFundValue, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref _FundResult);
                    if (_FundResult != -1)
                    {
                        isdataSave = true;
                    }
                    else
                    {
                        isdataSave = false;
                    }
                    LstFundSection.Add(objFundSection);
                    Logger.Info(strApplicationno + "MethodeName :ManageFundDetails Object" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "MethodeName :ManageFundDetails Object" + GetXMLFromObject(objFundSection) + System.Environment.NewLine);
                }
            }
            else
            {
                Logger.Info(strApplicationno + "Remark : Save Fund details in Xml begins" + System.Environment.NewLine);
                Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ManageFundDetails");
                string _strFundCode = string.Empty;
                string _strFundName = string.Empty;
                string _strFundValue = string.Empty;
                lblErrorFundDetailBody.Text = lblErrorreqdtls.Text = string.Empty;
                // objCommonObj = (CommonObject)Session["objCommonObj"];
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                foreach (GridViewRow rowFund in gvFundDtls.Rows)
                {
                    FundSection objFundSection = new FundSection();
                    objFundSection.Section = "FundDetails";
                    Label lblFundCode = rowFund.FindControl("lblFundvalue") as Label;
                    //Label lblRaisedby = rowfollowup.FindControl("lblRaisedby") as Label;
                    Label lblFundName = rowFund.FindControl("lblFundName") as Label;
                    TextBox lblFundvalue = rowFund.FindControl("txtFundvalue") as TextBox;
                    //Label lblClosedBy = rowfollowup.FindControl("lblClosedBy") as Label;
                    objFundSection.FND_fundCode = lblFundCode.Text;
                    objFundSection.FND_fundName = lblFundName.Text;
                    objFundSection.FND_fundComposition = lblFundvalue.Text;
                    //objBuss.FollowupDetails_Save(strChannelType, strApplicationno, _strfollowupcode, _strfollowupdiscp, _strfollowupcategory, _strfollowupcriteria, "", objChangeObj.userLoginDetails._UserID, _strfollowupstatus,
                    //    _strfollowupcategory, _strfollowuplifetype, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, strChannelType, strApplicationno, ref strFollowupResult);
                    //if (strFollowupResult == )
                    //{
                    //    _strFolloUPflag = false;
                    //}
                    // objComm.OnlineFundDetails(strChannelType, objCommonObj._AppType, strApplicationno, lblFundvalue.Text, lblFundName.Text, txtFundvalue.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref _FundResult);
                    LstFundSection.Add(objFundSection);
                    Logger.Info(strApplicationno + "MethodeName :ManageFundDetails Object" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "MethodeName :ManageFundDetails Object" + GetXMLFromObject(objFundSection) + System.Environment.NewLine);
                }
            }
            if (LstFundSection != null)
            {
                objReplica.LstFundSection = LstFundSection;
                isdataSave = true;
                Logger.Info(strApplicationno + "MethodeName :ManageFundDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :ManageFundDetails" + GetXMLFromObject(LstFundSection) + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :ManageFundDetails  Remark :Requirment object add to replica xml " + System.Environment.NewLine);
            }
        }

        catch (Exception ErrorFunds)
        {
            strErrorMessage = "Error While Saving Fund Details,Please Contact System Admin";
            isdataSave = false;
            Logger.Info(strApplicationno + "Remark:" + "Exception Fund details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + ErrorFunds.Message + System.Environment.NewLine);
        }
    }

    private void ManageLoadingDetails(bool blnUpdateInLa, ref bool isdataSave, ReplicaXml objReplica, DataSet _dsPremium, ref string strErrorMessage)
    {
        Logger.Info(strApplicationno + "STEP :6 Save Loading details Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Bank details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageBankDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLa + System.Environment.NewLine);
        try
        {
            lblTotalLoadingPremium.Text = lblLoadingPremium.Text = string.Empty;
            chkLoadingDtls.Checked = false;
            DataTable _dtLoaddata = new DataTable();
            //DataTable _dtLoadresoncodes = new DataTable();
            lblErrorloadingdtls.Text = string.Empty;
            int LoadingResult = 0;
            //objCommonObj = (CommonObject)Session["objCommonObj"];
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            // objChangeObj = (ChangeValue)ViewState["LoadingData"];
            Logger.Info(strApplicationno + "STAG 15 :PageName :Uwdecision.aspx.CS // MethodeName :btnLoadingDtlsSave_Click" + System.Environment.NewLine);
            string _strLoadRiderName = string.Empty;
            string _strLoadType = string.Empty;
            string _strLoadDiscp = string.Empty;
            string _strLoadReason1Discp = string.Empty;
            //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
            string _strLoadReason1 = string.Empty;
            string _strLoadReason2 = string.Empty;
            string _strLoadReason3 = string.Empty;
            string _strLoadReason4 = string.Empty;
            // string _strLoadReason1Discp = string.Empty;
            // string _strLoadReason2Discp = string.Empty;
            //43.1 End of Changes; Bhaumik Patel - [CR-3334]
            string _strLoadPercent = string.Empty;
            string _strRateAdjust = string.Empty;
            string _strFlatmortality = string.Empty;
            string _strLetterPrint = string.Empty;
            string _strRiderCode = string.Empty;
            string _strLoadCode = string.Empty;
            string _strReason1code = string.Empty;
            string _strReason2code = string.Empty;
            //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
            string _strReason3code = string.Empty;
            string _strReason4code = string.Empty;
            //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
            string strComponentCode = string.Empty;
            string strComponentRiderType = string.Empty;
            int ModalPremiumAmt = 0;
            int MedicalLoadingPremium = 0;
            int NonMedicalLoadingPremium = 0;
            _dtLoaddata = (DataTable)ViewState["LoadingData"];
           // _dtLoadresoncodes = (DataTable)ViewState["LoadingCODEData"];
            int i = 0;
            //pull value from gridview        
            List<LoadingSection> lstLoading = new List<LoadingSection>();
            foreach (GridViewRow objGridViewRow in gvExtLoadDetails.Rows)
            {
                Logger.Info(strApplicationno + "Save data from Existing loading grid begin" + System.Environment.NewLine);
                LoadingSection objLoadingSection = new LoadingSection();
                objLoadingSection.Section = "LoadingDetails";
                //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                Label ddlExstingLoadType = (Label)objGridViewRow.FindControl("ddlExstingLoadType");

                Label lblRiderName = (Label)objGridViewRow.FindControl("lblRiderName");

                Label lblExistingLoadRsn1 = (Label)objGridViewRow.FindControl("lblExistingLoadRsn1");
                Label lblExistingLoadRsn2 = (Label)objGridViewRow.FindControl("lblExistingLoadRsn2");
                Label lblExistingLoadRsn3 = (Label)objGridViewRow.FindControl("lblExistingLoadRsn3");
                Label lblExistingLoadRsn4 = (Label)objGridViewRow.FindControl("lblExistingLoadRsn4");
                //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                Label lblExistingLoadPer = (Label)objGridViewRow.FindControl("lblExistingLoadPer");
                Label lblExistingRateAdjust = (Label)objGridViewRow.FindControl("lblExistingRateAdjust");
                DropDownList ddlExistingLoadFlatMort = (DropDownList)objGridViewRow.FindControl("ddlExistingLoadFlatMort");
                DropDownList LetterPrint = (DropDownList)objGridViewRow.FindControl("LetterPrint");


                objLoadingSection.ApplicationNo = strApplicationno;
                objLoadingSection.RiderName = lblRiderName.Text;
                objLoadingSection.LoadingType = ddlExstingLoadType.Text;
                objLoadingSection.LoadingDiscp = ddlExstingLoadType.Text;
                //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                objLoadingSection.LoadReasoncode1 = lblExistingLoadRsn1.Text;
                objLoadingSection.LoadReasonCode2 = lblExistingLoadRsn2.Text;
                //43.1 End of Changes; Bhaumik Patel - [CR-3334]
                objLoadingSection.RateAdjustment = lblExistingRateAdjust.Text;
                objLoadingSection.FlatMortality = ddlExistingLoadFlatMort.SelectedValue;
                objLoadingSection.IsLetterPrint = LetterPrint.SelectedValue;
                objLoadingSection.RiderCode = lblRiderName.Text;
                objLoadingSection.LaodingCode = ddlExstingLoadType.Text;
                objLoadingSection.Loading = lblExistingLoadPer.Text;
                //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];


                //43.1 End of Changes; Bhaumik Patel - [CR-3334]

                if (blnUpdateInLa)
                {
                    //objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, objLoadingSection.LoadingType, 
                    //_strLoadDiscp,objLoadingSection.LoadReasoncode1 , objLoadingSection.LoadReasonCode2, "", txtSumassure.Text, objLoadingSection.Loading,objLoadingSection.RateAdjustment, objLoadingSection.FlatMortality, txtPrepayterm.Text, string.Empty, objLoadingSection.RiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref LoadingResult);
                    Logger.Info(strApplicationno + "Remark : Save data to database begin" + System.Environment.NewLine);
                    //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                    //objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, objLoadingSection.LoadingType,
                    // objLoadingSection.LoadingDiscp, _strReason1code, _strReason2code, _strReason3code, txtSumassure.Text, objLoadingSection.Loading, objLoadingSection.RateAdjustment, objLoadingSection.FlatMortality, txtPrepayterm.Text, string.Empty, objLoadingSection.RiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, _strReason4code, ref LoadingResult);
                    //43.1 End of Changes; Bhaumik Patel - [CR-3334]
                    if (_dsPremium != null)
                    {
                        if (_dsPremium.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < _dsPremium.Tables[0].Rows.Count; j++)
                            {

                                //ModalPremiumAmt InstalmentPremiumAmt
                                //ExtraPremiumAmt ExtraPremiumAmt
                                //SeriveTax SeriveTax
                                // MedicalLoadingPremium MedicalLoadingPremium
                                //MedicalLoadingRate notin datatable
                                //NonMedicalLoadingPremium NonMedicalLoadingPremium
                                //NonMedicalLoadingRate notindatatable
                                /* TotalPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["TotalInstalmentPremium"].ToString());
                                 ExtraPremAmt = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["ExtraPremiumAmt"].ToString());
                                 ModalPremiumAmt = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["ModalPremiumAmt"].ToString());
                                 MedicalLoadingPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["MedicalLoadingPremium"].ToString());
                                 NonMedicalLoadingPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["NonMedicalLoadingPremium"].ToString());
                                 ServiceTax = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["ServiceTax"].ToString());
                                 strComponentCode = _dsPremium.Tables[0].Rows[i]["RiderInfo"].ToString();
                                 strComponentRiderType = _dsPremium.Tables[0].Rows[i]["RiderType"].ToString();*/


                                //  LoadedPremiumA = Convert.ToInt32(_dsPremium.Tables[0].Rows[0]["LoadedPremiumA"].ToString().Replace(".00", ""));
                                //  TotalPremiumB = Convert.ToInt32(_dsPremium.Tables[0].Rows[0]["TotalPremiumB"].ToString().Replace(".00", ""));
                                //  LoadedPremiumB = Convert.ToInt32(_dsPremium.Tables[0].Rows[0]["LoadedPremiumB"].ToString().Replace(".00", ""));

                                //if (strChannelType.ToUpper() == "ONLINE")
                                //{
                                //    objComm.OnlinepremiumLoadingDetails_Save(strApplicationno, LoadedPremiumAB, _strLoadDiscp, _strLoadCode, _strReason1code, "", _strLoadDiscp, LoadedPremiumA, LoadedPremiumB, TotalPremiumA, TotalPremiumB, "", ref strLApushErrorCode);
                                //}
                                int BackdatedInt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["BackdatedInt"].ToString()), MidpointRounding.AwayFromZero)));
                                string ComponentCd = _dsPremium.Tables[0].Rows[j]["ComponentCd"].ToString();
                                int EduCess = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["EduCess"].ToString()), MidpointRounding.AwayFromZero)));
                                int ExtraPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["ExtraPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                int InstalmentPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["InstalmentPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                string LifeType = _dsPremium.Tables[0].Rows[j]["LifeType"].ToString();
                                MedicalLoadingPremium = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["MedicalLoadingPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                //MedicalLoadingPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["MedicalLoadingPremium"].ToString());
                                int MedicalLoadingRate = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["MedicalLoadingRate"].ToString()), MidpointRounding.AwayFromZero)));
                                ModalPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["ModalPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                NonMedicalLoadingPremium = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["NonMedicalLoadingPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                int NonMedicalLoadingRate = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["NonMedicalLoadingRate"].ToString()), MidpointRounding.AwayFromZero)));
                                string RiderCtg = _dsPremium.Tables[0].Rows[j]["RiderCtg"].ToString();
                                int RiderPPT = Convert.ToInt32(_dsPremium.Tables[0].Rows[j]["RiderPPT"].ToString());
                                int RiderPT = Convert.ToInt32(_dsPremium.Tables[0].Rows[j]["RiderPT"].ToString());
                                int LoadingSeriveTax = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["SeriveTax"].ToString()), MidpointRounding.AwayFromZero)));
                                long SumAssured = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["SumAssured"].ToString()), MidpointRounding.AwayFromZero)));
                                long SumAssuredAcrossPlans = Convert.ToInt64(_dsPremium.Tables[0].Rows[j]["SumAssuredAcrossPlans"].ToString());
                                long TotalInstalmentPremium = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["TotalInstalmentPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                long TotalPremiumAmount = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["TotalPremiumAmount"].ToString()), MidpointRounding.AwayFromZero)));
                                //if (strChannelType.ToUpper() == "OFFLINE")
                                //{
                                //objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, strComponentCode, "", "", strComponentRiderType, 0, Convert.ToDecimal(TotalPremium), ddlLoadRsn2.SelectedItem.Text, 0, Convert.ToDecimal(ExtraPremAmt), Convert.ToDecimal(ServiceTax), 0, Convert.ToDecimal(ExtraPremAmt), 0, 0, 0, 0, 0, 0, 0, ref strLApushErrorCode);
                                objComm.OnlinePremiumExtraLoadingdetails_Save_New(strApplicationno, txtPolno.Text, ComponentCd, RiderPPT, RiderPT, RiderCtg, BackdatedInt, TotalInstalmentPremium, ddlLoadRsn1.SelectedValue, ModalPremiumAmt, ExtraPremiumAmt, LoadingSeriveTax, EduCess, 0, SumAssured, SumAssuredAcrossPlans, MedicalLoadingPremium, MedicalLoadingRate, NonMedicalLoadingPremium, 0, NonMedicalLoadingRate, ref strLApushErrorCode);
                                objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, ComponentCd, RiderPPT, RiderPT, RiderCtg, BackdatedInt, TotalInstalmentPremium, ddlLoadRsn1.SelectedValue, ModalPremiumAmt, ExtraPremiumAmt, LoadingSeriveTax, EduCess, 0, SumAssured, SumAssuredAcrossPlans, MedicalLoadingPremium, MedicalLoadingRate, NonMedicalLoadingPremium, 0, NonMedicalLoadingRate, ref strLApushErrorCode);
                                //}
                            }
                        }
                    }

                    //if (LoadingResult > 0)
                    //{
                    //    Logger.Info(strApplicationno + "Remark : Save loading data to database Success" + System.Environment.NewLine);
                    //    lblErrorloadingdtls.Text += " Loading Details Save Successfully In DataBase";
                    //    lblErrorLoadingDetailBody.Text += " Loading Details Save Successfully In DataBase";
                    //}
                    //else
                    //{
                    //    Logger.Info(strApplicationno + "Remark : Save loading data to database Failed" + System.Environment.NewLine);
                    //    lblErrorloadingdtls.Text += " Loading Details Not Save Successfully In DataBase Click Here To Know More";
                    //    lblErrorLoadingDetailBody.Text += " Loading Details Not Save Successfully In DataBase";
                    //}
                }
                lstLoading.Add(objLoadingSection);
                Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + GetXMLFromObject(objLoadingSection) + System.Environment.NewLine);
            }

            //pull value from view state
            if (_dtLoaddata != null && _dtLoaddata.Rows.Count > 0)
            {
                string loadrsncode = Request.Form[ddlLoadRsn1.UniqueID];
                Logger.Info(strApplicationno + "Save data from Selecting loading  begin" + System.Environment.NewLine);
                for (i = 0; i < _dtLoaddata.Rows.Count; i++)
                {
                    LoadingSection objLoadingSection = new LoadingSection();
                    objLoadingSection.Section = "LoadingDetails";
                    objLoadingSection.ApplicationNo = strApplicationno;
                    objLoadingSection.RiderName = _strLoadRiderName = _dtLoaddata.Rows[i]["RiderName"].ToString();
                    objLoadingSection.LoadingType = _strLoadType = _dtLoaddata.Rows[i]["ddlLoadType"].ToString();
                    objLoadingSection.LoadingDiscp = _strLoadDiscp = _dtLoaddata.Rows[i]["LoadingDiscp"].ToString();
                    //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
               
                    objLoadingSection.Reason1 = _strLoadReason1 = _dtLoaddata.Rows[i]["Loadrsn1"].ToString();
                    objLoadingSection.Reason2 = _strLoadReason2 = _dtLoaddata.Rows[i]["Loadrsn2"].ToString();
                    objLoadingSection.Reason3 = _strLoadReason3 = _dtLoaddata.Rows[i]["Loadrsn3"].ToString();
                    objLoadingSection.Reason4 = _strLoadReason4 = _dtLoaddata.Rows[i]["Loadrsn4"].ToString();
             
                    //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                    objLoadingSection.RateAdjustment = _strRateAdjust = _dtLoaddata.Rows[i]["txtRateAdjust"].ToString();
                    objLoadingSection.FlatMortality = _strFlatmortality = (_dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString() == "0") ? "" : _dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString();
                    objLoadingSection.IsLetterPrint = _strLetterPrint = _dtLoaddata.Rows[i]["LetterPrint"].ToString();
                    objLoadingSection.RiderCode = _strRiderCode = _dtLoaddata.Rows[i]["RiderCode"].ToString();
                    objLoadingSection.LaodingCode = _strLoadCode = _dtLoaddata.Rows[i]["ddlLoadCode"].ToString();
                    objLoadingSection.Loading = _strLoadPercent = _dtLoaddata.Rows[i]["txtLoadPer"].ToString();
           
                    if (blnUpdateInLa)
                    {
                        //43.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                        Logger.Info(strApplicationno + "Save View state Loading data from" + System.Environment.NewLine);
                        objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, _strLoadCode, _strLoadDiscp, _strLoadReason1, _strLoadReason2, _strLoadReason3, txtSumassure.Text, txtLoadPer.Text, _strRateAdjust, _strFlatmortality, txtPrepayterm.Text, _strLetterPrint, objLoadingSection.RiderCode, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, _strLoadReason4, ref LoadingResult);
                        //43.1 End of Changes; Bhaumik Patel - [CR-3334]
                        /*extra loading*/
                        if (_dsPremium != null)
                        {
                            if (_dsPremium.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < _dsPremium.Tables[0].Rows.Count; j++)
                                {

                                    //ModalPremiumAmt InstalmentPremiumAmt
                                    //ExtraPremiumAmt ExtraPremiumAmt
                                    //SeriveTax SeriveTax
                                    // MedicalLoadingPremium MedicalLoadingPremium
                                    //MedicalLoadingRate notin datatable
                                    //NonMedicalLoadingPremium NonMedicalLoadingPremium
                                    //NonMedicalLoadingRate notindatatable
                                    /* TotalPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["TotalInstalmentPremium"].ToString());
                                     ExtraPremAmt = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["ExtraPremiumAmt"].ToString());
                                     ModalPremiumAmt = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["ModalPremiumAmt"].ToString());
                                     MedicalLoadingPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["MedicalLoadingPremium"].ToString());
                                     NonMedicalLoadingPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["NonMedicalLoadingPremium"].ToString());
                                     ServiceTax = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["ServiceTax"].ToString());
                                     strComponentCode = _dsPremium.Tables[0].Rows[i]["RiderInfo"].ToString();
                                     strComponentRiderType = _dsPremium.Tables[0].Rows[i]["RiderType"].ToString();*/


                                    //  LoadedPremiumA = Convert.ToInt32(_dsPremium.Tables[0].Rows[0]["LoadedPremiumA"].ToString().Replace(".00", ""));
                                    //  TotalPremiumB = Convert.ToInt32(_dsPremium.Tables[0].Rows[0]["TotalPremiumB"].ToString().Replace(".00", ""));
                                    //  LoadedPremiumB = Convert.ToInt32(_dsPremium.Tables[0].Rows[0]["LoadedPremiumB"].ToString().Replace(".00", ""));

                                    //if (strChannelType.ToUpper() == "ONLINE")
                                    //{
                                    //    objComm.OnlinepremiumLoadingDetails_Save(strApplicationno, LoadedPremiumAB, _strLoadDiscp, _strLoadCode, _strReason1code, "", _strLoadDiscp, LoadedPremiumA, LoadedPremiumB, TotalPremiumA, TotalPremiumB, "", ref strLApushErrorCode);
                                    //}
                                    int BackdatedInt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["BackdatedInt"].ToString()), MidpointRounding.AwayFromZero)));
                                    string ComponentCd = _dsPremium.Tables[0].Rows[j]["ComponentCd"].ToString();
                                    int EduCess = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["EduCess"].ToString()), MidpointRounding.AwayFromZero)));
                                    int ExtraPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["ExtraPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                    int InstalmentPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["InstalmentPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                    string LifeType = _dsPremium.Tables[0].Rows[j]["LifeType"].ToString();
                                    MedicalLoadingPremium = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["MedicalLoadingPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                    //MedicalLoadingPremium = Convert.ToInt32(_dsPremium.Tables[0].Rows[i]["MedicalLoadingPremium"].ToString());
                                    int MedicalLoadingRate = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["MedicalLoadingRate"].ToString()), MidpointRounding.AwayFromZero)));
                                    ModalPremiumAmt = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["ModalPremiumAmt"].ToString()), MidpointRounding.AwayFromZero)));
                                    NonMedicalLoadingPremium = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["NonMedicalLoadingPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                    int NonMedicalLoadingRate = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["NonMedicalLoadingRate"].ToString()), MidpointRounding.AwayFromZero)));
                                    string RiderCtg = _dsPremium.Tables[0].Rows[j]["RiderCtg"].ToString();
                                    int RiderPPT = Convert.ToInt32(_dsPremium.Tables[0].Rows[j]["RiderPPT"].ToString());
                                    int RiderPT = Convert.ToInt32(_dsPremium.Tables[0].Rows[j]["RiderPT"].ToString());
                                    int LoadingSeriveTax = Convert.ToInt32(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["SeriveTax"].ToString()), MidpointRounding.AwayFromZero)));
                                    long SumAssured = Convert.ToInt64(_dsPremium.Tables[0].Rows[j]["SumAssured"].ToString());
                                    long SumAssuredAcrossPlans = Convert.ToInt64(_dsPremium.Tables[0].Rows[j]["SumAssuredAcrossPlans"].ToString());
                                    long TotalInstalmentPremium = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["TotalInstalmentPremium"].ToString()), MidpointRounding.AwayFromZero)));
                                    long TotalPremiumAmount = Convert.ToInt64(Convert.ToString(Math.Round(Convert.ToDecimal(_dsPremium.Tables[0].Rows[j]["TotalPremiumAmount"].ToString()), MidpointRounding.AwayFromZero)));
                                    if (strChannelType.ToUpper() == "OFFLINE")
                                    {
                                        //objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, strComponentCode, "", "", strComponentRiderType, 0, Convert.ToDecimal(TotalPremium), ddlLoadRsn2.SelectedItem.Text, 0, Convert.ToDecimal(ExtraPremAmt), Convert.ToDecimal(ServiceTax), 0, Convert.ToDecimal(ExtraPremAmt), 0, 0, 0, 0, 0, 0, 0, ref strLApushErrorCode);
                                        objComm.OnlinePremiumExtraLoadingdetails_Save_New(strApplicationno, txtPolno.Text, ComponentCd, RiderPPT, RiderPT, RiderCtg, BackdatedInt, TotalInstalmentPremium, ddlLoadRsn1.SelectedValue, ModalPremiumAmt, ExtraPremiumAmt, LoadingSeriveTax, EduCess, 0, SumAssured, SumAssuredAcrossPlans, MedicalLoadingPremium, MedicalLoadingRate, NonMedicalLoadingPremium, 0, NonMedicalLoadingRate, ref strLApushErrorCode);
                                        objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, ComponentCd, RiderPPT, RiderPT, RiderCtg, BackdatedInt, TotalInstalmentPremium, ddlLoadRsn1.SelectedValue, ModalPremiumAmt, ExtraPremiumAmt, LoadingSeriveTax, EduCess, 0, SumAssured, SumAssuredAcrossPlans, MedicalLoadingPremium, MedicalLoadingRate, NonMedicalLoadingPremium, 0, NonMedicalLoadingRate, ref strLApushErrorCode);
                                    }
                                }
                            }
                        }
                        /*end here*/
                        if (LoadingResult > 0)
                        {
                            Logger.Info(strApplicationno + "Save View state Loading data to database Success" + System.Environment.NewLine);
                            lblErrorloadingdtls.Text += " Loading Details Save Successfully In DataBase";
                            //lblErrorLoadingDetailBody.Text += " Loading Details Save Successfully In DataBase";
                        }
                        else
                        {
                            Logger.Info(strApplicationno + "Save View state Loading data to database Failed" + System.Environment.NewLine);
                            lblErrorloadingdtls.Text += " Loading Details Not Save Successfully In DataBase Click Here To Know More";
                            //lblErrorLoadingDetailBody.Text += " Loading Details Not Save Successfully In DataBase";
                        }
                    }
                    lstLoading.Add(objLoadingSection);
                    Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + System.Environment.NewLine);
                    Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + GetXMLFromObject(objLoadingSection) + System.Environment.NewLine);
                }
                //if (LoadingResult != -1)
                //{
                //FillLoadingDetails(strApplicationno, strChannelType);
                //lblErrorloadingdtls.Text = "Loading details save successfully";
                //}
                //else
                //{
                //    lblErrorloadingdtls.Text = "Loading Details Not save successfully";
                //    btnAddLoadingRow.CssClass = "btn btn-primary";
                //    btnLoadingDtlsSave.CssClass = "btn-primary";
                //}
                //objReplica.LstLoadingSection = lstLoading;
                Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + GetXMLFromObject(lstLoading) + System.Environment.NewLine);

            }
            objReplica.LstLoadingSection = lstLoading;
            /*START ID:22*/
            /*HHI loading on both product validation */
            if (txtProdcode.Text.Equals("H05") || txtProdcode.Text.Equals("H06") || txtProdcode.Text.Equals("H07") || txtProdcode.Text.Equals("H08"))
            {
                objComm.CheckRider(ref _ds, Convert.ToString(txtAppno.Text));
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0 && Convert.ToString(_ds.Tables[0].Rows[0][0]) == "1")
                {
                    ShowPopupMessage("Add Appropriate Loading On Heart & Health Product");
                    throw new Exception("UDE-Add Appropriate Loading On Heart & Health Product");
                }
            }
            /*END ID:22*/
        }
        catch (Exception Error)
        {
            strErrorMessage = "Error While Saving Loading Details,Please Contact System Admin ";
            isdataSave = false;
            Logger.Info(strApplicationno + "Remark:" + "Exception Requirments details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageProductDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + Error.Message + System.Environment.NewLine);
            if (Error.Message.Contains("UDE-"))
            {
                throw new Exception(Error.Message);
            }
            else
            {
                throw new Exception("Try Again Later");
            }
        }
        Logger.Info(strApplicationno + "STEP :6 Save Loading details end" + System.Environment.NewLine);
    }



    //41.1 Begin of Changes; Bhaumik Patel - [CR-3334]
    private void ManageDecision_Details(bool blnUpdateInLa, ref bool isdataSave, ReplicaXml objReplica, DataSet _dsPremium, ref string strErrorMessage)
    {
        Logger.Info(strApplicationno + "STEP :6 Save Decision details Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Bank details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageDecision_Details" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLa + System.Environment.NewLine);
        try
        {
            DataTable _dtDecisiondata = new DataTable();
            lblErrorloadingdtls.Text = string.Empty;
            int DecisionResult = 0;
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            Logger.Info(strApplicationno + "STAG 15 :PageName :Uwdecision.aspx.CS // MethodeName :btnLoadingDtlsSave_Click" + System.Environment.NewLine);
            string _strDecisionType = string.Empty;
            string _strCategory = string.Empty;
            string _strUwreason1 = string.Empty;
            string _strUwreason2 = string.Empty;
            string _strperiod = string.Empty;


            _dtDecisiondata = (DataTable)ViewState["UWDecisionData"];
            int i = 0;

            if (blnUpdateInLa)
            {
                //objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, objLoadingSection.LoadingType,
                //_strLoadDiscp, objLoadingSection.LoadReasoncode1, objLoadingSection.LoadReasonCode2, "", txtSumassure.Text, objLoadingSection.Loading, objLoadingSection.RateAdjustment, objLoadingSection.FlatMortality, txtPrepayterm.Text, string.Empty, objLoadingSection.RiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref LoadingResult);
                //Logger.Info(strApplicationno + "Remark : Save data to database begin" + System.Environment.NewLine);
                //objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, objLoadingSection.LoadingType,
                //   objLoadingSection.LoadingDiscp, objLoadingSection.LoadReasoncode1, objLoadingSection.LoadReasonCode2, "", txtSumassure.Text, objLoadingSection.Loading, objLoadingSection.RateAdjustment, objLoadingSection.FlatMortality, txtPrepayterm.Text, string.Empty, objLoadingSection.RiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref LoadingResult);


                if (DecisionResult > 0)
                {
                    Logger.Info(strApplicationno + "Remark : Save loading data to database Success" + System.Environment.NewLine);
                    //lblErrorloadingdtls.Text += " Loading Details Save Successfully In DataBase";
                    //lblErrorLoadingDetailBody.Text += " Loading Details Save Successfully In DataBase";
                }
                else
                {
                    Logger.Info(strApplicationno + "Remark : Save loading data to database Failed" + System.Environment.NewLine);
                    //lblErrorloadingdtls.Text += " Loading Details Not Save Successfully In DataBase Click Here To Know More";
                    //lblErrorLoadingDetailBody.Text += " Loading Details Not Save Successfully In DataBase";
                }
            }
            // lstLoading.Add(objLoadingSection);
            Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + System.Environment.NewLine);
            // Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + GetXMLFromObject(objLoadingSection) + System.Environment.NewLine);


            //pull value from view state
            if (_dtDecisiondata != null && _dtDecisiondata.Rows.Count > 0)
            {
                string UWrsncode = Request.Form[ddlUWreason1.UniqueID];
                Logger.Info(strApplicationno + "Save data from Selecting Decision  begin" + System.Environment.NewLine);
                for (i = 0; i < _dtDecisiondata.Rows.Count; i++)
                {


                    _strDecisionType = _dtDecisiondata.Rows[i]["ddlUWDecesion"].ToString();

                    _strCategory = _dtDecisiondata.Rows[i]["ddlUWDecesioncategory"].ToString();

                    //_strUwreason1 = _dtDecisiondata.Rows[i]["ddlUWreason1"].ToString();

                    //_strUwreason2 = _dtDecisiondata.Rows[i]["txtUWreason2"].ToString();
                    for (int k = 0; k < UWrsncode.Split(',').Length; k++)
                    {
                        if (k == 0)
                        {
                            _strUwreason1 = UWrsncode.Split(',')[k];
                        }


                        if (k == 1)
                        {
                            _strUwreason2 = UWrsncode.Split(',')[k];
                        }

                    }

                    _strperiod = _dtDecisiondata.Rows[i]["ddlPeriod"].ToString();


                    if (blnUpdateInLa)
                    {
                        Logger.Info(strApplicationno + "Save View state Decision data from" + System.Environment.NewLine);
                        objComm.OnlineDecisionDetails_Save( strApplicationno, _strDecisionType, _strCategory, _strUwreason1, _strUwreason2, _strperiod, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName,  ref DecisionResult);


                        if (DecisionResult > 0)
                        {
                            Logger.Info(strApplicationno + "Save View state Loading data to database Success" + System.Environment.NewLine);
                            //lblErrorDecisiondtls.Text += " Decision Details Save Successfully In DataBase";
                            lblErrorDecisiondtls.Text = " Decision Details Save Successfully In DataBase";
                        }
                        else
                        {
                            Logger.Info(strApplicationno + "Save View state Loading data to database Failed" + System.Environment.NewLine);
                            //lblErrorDecisiondtls.Text += " Decision Details Not Save Successfully In DataBase Click Here To Know More";
                            lblErrorDecisiondtls.Text = " Decision Details Not Save Successfully In DataBase";
                        }
                    }
                    // lstLoading.Add(objLoadingSection);
                    Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + System.Environment.NewLine);
                    //Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + GetXMLFromObject(objLoadingSection) + System.Environment.NewLine);
                }

                Logger.Info(strApplicationno + "MethodeName :ManageDecision_Details Object" + System.Environment.NewLine);
                // Logger.Info(strApplicationno + "MethodeName :ManageDecision_Details Object" + GetXMLFromObject(lstLoading) + System.Environment.NewLine);

            }
            //objReplica.LstLoadingSection = lstLoading;
            /*START ID:22*/
            /*HHI loading on both product validation */
            //if (txtProdcode.Text.Equals("H05") || txtProdcode.Text.Equals("H06") || txtProdcode.Text.Equals("H07") || txtProdcode.Text.Equals("H08"))
            //{
            //    objComm.CheckRider(ref _ds, Convert.ToString(txtAppno.Text));
            //    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0 && Convert.ToString(_ds.Tables[0].Rows[0][0]) == "1")
            //    {
            //        ShowPopupMessage("Add Appropriate Loading On Heart & Health Product");
            //        throw new Exception("UDE-Add Appropriate Loading On Heart & Health Product");
            //    }
            //}
            ///*END ID:22*/
        }
        catch (Exception Error)
        {
            strErrorMessage = "Error While Saving Decision Details,Please Contact System Admin ";
            isdataSave = false;
            Logger.Info(strApplicationno + "Remark:" + "Exception Requirments details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageDecision_Details" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + Error.Message + System.Environment.NewLine);
            if (Error.Message.Contains("UDE-"))
            {
                throw new Exception(Error.Message);
            }
            else
            {
                throw new Exception("Try Again Later");
            }
        }
        Logger.Info(strApplicationno + "STEP :6 Save Decision details end" + System.Environment.NewLine);
    }
    //43.1 End of Changes; Bhaumik Patel - [CR-3334]

    private void ManageApplicatoinStatus(ref bool isdataSave)
    {
        Logger.Info(strApplicationno + "STEP :7 Save Application Status details Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Application Stataus" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageApplicatoinStatus" + System.Environment.NewLine);
        try
        {


            if (Session["objLoginObj"] != null)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                //objcomm.MaintainApplicationLog(strApplicationno, "save", string.Empty, objChangeObj.userLoginDetails._UserID, "", ref intRetVal);
            }
        }
        catch (Exception Error)
        {
            Logger.Info(strApplicationno + "Remark:" + "Exception in Manage Application log details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageApplicatoinStatus" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + Error.Message + System.Environment.NewLine);
        }
        Logger.Info(strApplicationno + "STEP :7 Save Application Status details End" + System.Environment.NewLine);
    }

    private void ManageMandateDetails(bool blnUpdateInLa, ref bool isdataSave, ReplicaXml objReplica, ref string strErrorMessage)
    {
        Logger.Info(strApplicationno + "STEP :4 Save ProductRiderDetails Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Mandate details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageMandateDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnUpdateInLa + System.Environment.NewLine);
        try
        {
            MandateDetails objMandateDetails = new MandateDetails();
            objMandateDetails.ApplicationNo = strApplicationno;
            objMandateDetails.AccountType = txtMandAccountType.Text;
            objMandateDetails.AccountNumber = txtMandAccountno.Text;
            objMandateDetails.AutoPay = ddlAutoPaytype.SelectedValue;
            if (Session["objLoginObj"] != null)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                objMandateDetails.objLoginUserDetails = objChangeObj.userLoginDetails;
            }
            /*ADDED BY SHRI FOR MANDATE SI */

            if (ddlAutoPaytype.SelectedValue.ToUpper() == "SI")
            {
                objMandateDetails.CreditCardNo = txtCreditcardno.Text;
                objMandateDetails.CreditCardType = txtcreditcardType.Text;
                objMandateDetails.ValidUpto = txtValidupto.Text;
                objMandateDetails.MicroCode = string.Empty;
                objMandateDetails.BankName = string.Empty;
                objMandateDetails.BranchName = string.Empty;
                objMandateDetails.MandateDate = (string.IsNullOrEmpty(txtMandateSigndate.Text)) ? Convert.ToDateTime("01-01-0001") : Convert.ToDateTime(txtMandateSigndate.Text);
                objMandateDetails.AccountHolderName = txtHolderName.Text;
            }
            else
            {
                objMandateDetails.MicroCode = txtMandMicrocode.Text;
                objMandateDetails.BankName = txtMandBankName.Text;
                objMandateDetails.BranchName = txtMandBranchName.Text;
                objMandateDetails.MandateDate = (string.IsNullOrEmpty(txtMandateassigndate.Text)) ? Convert.ToDateTime("01-01-0001") : Convert.ToDateTime(txtMandateassigndate.Text);
                objMandateDetails.AccountHolderName = txtMandAccountholder.Text;
            }
            /*END HERE*/

            int intRetVal = -1;
            /*commented by shri on 08 nov 17 to comment mandate details db call*/
            if (blnUpdateInLa)
            {
                Logger.Info(strApplicationno + "Remark : manadate details save to database begin:" + System.Environment.NewLine);
                objcomm.ManageMandate(objMandateDetails, ref intRetVal);
            }
            else
            {
                Logger.Info(strApplicationno + "Remark : manadate details save to Xml begin:" + System.Environment.NewLine);
                intRetVal = 1;
            }
            /*end here*/
            HideShowMandate();
            if (intRetVal > 0)
            {
                Logger.Info(strApplicationno + "Remark : manadate details save to database Success:" + System.Environment.NewLine);
                //lblErrorMandate.Text = "Mandate Details Updated In Data Base";
            }
            else
            {
                Logger.Info(strApplicationno + "Remark : manadate details save to database Failed:" + System.Environment.NewLine);
                //lblErrorMandate.Text = "Mandate Details NOT Updated In Data Base";
            }
            MandateSection objMandateSection = new MandateSection();
            objMandateSection.Section = "Mandate";
            objMandateSection.ACCOUNT_TYPE = txtMandAccountType.Text;
            objMandateSection.ACCOUNT_NUMBER = txtMandAccountno.Text;
            objMandateSection.AUTO_PAY_TYPE = ddlAutoPaytype.SelectedValue;
            objMandateSection.CREDIT_CARD_NO = txtCreditcardno.Text;
            objMandateSection.CREDIT_CARD_TYPE = txtcreditcardType.Text;
            objMandateSection.VALID_DATE = txtValidupto.Text;
            if (ddlAutoPaytype.SelectedValue.ToUpper() == "SI")
            {
                objMandateSection.MICRO_CODE = string.Empty;
                objMandateSection.BANK_NAME = string.Empty;
                objMandateSection.BRANCH_NAME = string.Empty;
                objMandateSection.MANDATE_DATE = (string.IsNullOrEmpty(txtMandateSigndate.Text)) ? "01-01-0001" : txtMandateSigndate.Text;
                objMandateSection.ACCOUNT_HOLDER_NAME = txtHolderName.Text;
            }
            else
            {
                objMandateSection.MICRO_CODE = txtMandMicrocode.Text;
                objMandateSection.BANK_NAME = txtMandBankName.Text;
                objMandateSection.BRANCH_NAME = txtMandBranchName.Text;
                objMandateSection.MANDATE_DATE = (string.IsNullOrEmpty(txtMandateassigndate.Text)) ? "01-01-0001" : txtMandateassigndate.Text;
                objMandateSection.ACCOUNT_HOLDER_NAME = txtMandAccountholder.Text;
            }
            objReplica.MandateSection = objMandateSection;
            Logger.Info(strApplicationno + "MethodeName :ManageBankDetails Object" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "MethodeName :ManageBankDetails Object" + GetXMLFromObject(objMandateSection) + System.Environment.NewLine);
            if (blnUpdateInLa)
            {
                objcomm.ManageMandate(objMandateDetails, ref intRetVal);
                if (intRetVal > 0)
                {
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                    if (Session["objCommonObj"] != null)
                    {
                        objCommonObj = (CommonObject)Session["objCommonObj"];
                    }
                    string strAppType = objCommonObj._ChannelType;
                    DataSet _dsMandateResp = new DataSet();
                    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strAppType, objChangeObj, ref _dsMandateResp, ref _dsPrevPol, "BANKENQ", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                }
            }

        }
        catch (Exception Error)
        {
            HideShowMandate();
            strErrorMessage = "Error While Saving Mandate Details,Please Contact System Admin";
            isdataSave = false;
            //lblErrorMandate.Text = lblErrorMandate.Text + strSpace + "Try again later";
            Logger.Info(strApplicationno + "Remark:" + "Exception in Manage Manadte details" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageMandateDetails" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + Error.Message + System.Environment.NewLine);
        }
        chkMandate.Checked = false;
        Logger.Info(strApplicationno + "STEP :4 Save ProductRiderDetails End" + System.Environment.NewLine);
    }

    private void ManageApplicatonObject(int intSrNo, string strApplicationNo, ReplicaXml objReplicaXml, bool blnIsPosted, ref bool isdataSave, string strUserId, ref int intRet)
    {
        Logger.Info(strApplicationno + "Remark:" + "Save XMl to database Begin" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageApplicatonObject" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Is Updated in datatabse:" + blnIsPosted + System.Environment.NewLine);
        try
        {
            string strObjectData = UWSaralServices.CommFun.GetXMLFromObject(objReplicaXml);
            objcomm.ManageApplicationObject(intSrNo, strApplicationNo, strObjectData, blnIsPosted, strUserId, ref intRet);
            Logger.Info(strApplicationno + "Remark:" + "Save XMl to database end" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageApplicatonObject" + System.Environment.NewLine);
        }
        catch (Exception Error)
        {
            Logger.Info(strApplicationno + "Remark:" + "Exception in Application object Save to DB" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageApplicatonObject" + System.Environment.NewLine);
            Logger.Info(strApplicationno + "Exception Message:" + Error.Message + System.Environment.NewLine);
        }
    }

    private void FillUwDecision(DataSet _dsPageData)
    {
        try
        {
            foreach (DataTable dt in _dsPageData.Tables)
            {
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        switch (dt.Rows[0][0].ToString())
                        {
                            case "ApplicationDetails":
                                BindApplicationDetails(dt);
                                clsName = divApplicationDetails.Attributes["class"].ToString();
                                divApplicationDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "AgentDetails":
                                BindAgentDetails(dt);
                                clsName = divAgentDetails.Attributes["class"].ToString();
                                divAgentDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "BankDetails":
                                BindBankDetails(dt);
                                clsName = divBankDetails.Attributes["class"].ToString();
                                divBankDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "PanDetails":
                                BindPanDetails(dt);
                                clsName = divPanDetails.Attributes["class"].ToString();
                                divPanDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "ProductDetails":
                                BindProductDetails(dt);
                                clsName = divProductDetails.Attributes["class"].ToString();
                                divProductDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;
                            //Added by Suraj on 27 APRIL 2018
                            case "AssignmentType":
                                if (dt.Rows.Count > 0)
                                {
                                    if (ddlApplicationDetailsProposalType.SelectedValue.Equals("EMP"))
                                    {
                                        if (ddlAssigmentType.Items.FindByValue(Convert.ToString(dt.Rows[0]["Value"])) != null)
                                        {
                                            ddlAssigmentType.ClearSelection();
                                            ddlAssigmentType.Items.FindByValue(Convert.ToString(dt.Rows[0]["Value"])).Selected = true;
                                        }
                                    }
                                    else
                                    {
                                        ddlAssigmentType.SelectedValue = "0";
                                    }
                                }
                                break;
                            //END
                            case "Country":
                                if (dt.Rows.Count > 0)
                                {
                                    if (ddlApplicationDetailsProposalType.SelectedValue.Equals("NRI"))
                                    {
                                        if (ddlcountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["Value"])) != null)
                                        {
                                            ddlcountry.ClearSelection();
                                            ddlcountry.Items.FindByValue(Convert.ToString(dt.Rows[0]["Value"])).Selected = true;
                                        }
                                    }
                                    else
                                    {
                                        ddlcountry.SelectedValue = "0";
                                    }
                                }
                                break;
                            case "RiderDetails":
                                BindRiderDetails(dt);
                                break;

                            case "FundDetails":
                                BindFundDetails(dt);
                                break;

                            case "ReceiptDetails":
                                BindReciptDetails(dt);
                                clsName = divPaymentDetails.Attributes["class"].ToString();
                                divPaymentDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "RequirmentDetails":
                                BindRequirmentDetails(dt);
                                //UWFollowupServiceCall(strApplicationno, strChannelType);
                                clsName = divRequirementDetails.Attributes["class"].ToString();
                                divRequirementDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;
                            // 47.1 Begin of Changes; Jayendra  - [Webashlar01]
                            case "Requirment_STP":
                                BindRequirmentDetails_STP(dt);
                                //UWFollowupServiceCall(strApplicationno, strChannelType);
                                break;
                            // 47.1 End of Changes; Jayendra  - [Webashlar01]

                            case "LoadingDetails":
                                BindLoadingDetails(dt);
                                clsName = divExistingloading.Attributes["class"].ToString();
                                divExistingloading.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "CommentsDetails":
                                BindCommentsDetails(dt);
                                clsName = divExistingComments.Attributes["class"].ToString();
                                divExistingComments.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "CommonObjectDetails":
                                BindCommonObjectDetails(dt, strChannelType);
                                break;

                            case "PreIssueDetails":
                                //UWPreIssueServiceCall(strApplicationno, strChannelType);
                                clsName = PreIssue_containerBody.Attributes["class"].ToString();
                                PreIssue_containerBody.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            case "StpDetails":
                            //UWSTPServiceCall(strApplicationno, strChannelType);
                            //clsName = STP_containerBody.Attributes["class"].ToString();
                            //STP_containerBody.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                            //break;

                            case "AutoUwResults":
                                //DashbordUWResultDynamic_Get(strApplicationno, strUserId, strUserGroup, strUWmode);
                                clsName = AuResult_ContainerBody.Attributes["class"].ToString();
                                AuResult_ContainerBody.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;

                            //case "DecisionDetails":
                            //  clsName = divDecisionDetails.Attributes["class"].ToString();
                            //  divDecisionDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                            //  break;

                            case "SarDetails":
                                FillTsmrMsarDetails(strApplicationno, strChannelType);
                                clsName = divSarDetails.Attributes["class"].ToString();
                                divSarDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;
                            //38.1 Begin of Changes; Bhaumik Patel - [CR-4134]   
                            case "RedFlagDetails":
                                FillRedFlagDetails(strApplicationno);
                                clsName = divRedFlag.Attributes["class"].ToString();
                                divRedFlag.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;
                            //38.1 End of Changes; Bhaumik Patel - [CR-4134]
                            case "Mandate":
                                FillMandateDetails(dt);
                                break;
                            case "Journal":
                                BindJournalDetails(dt);
                                break;
                            case "RISKPARAMETER":
                                BindRiskParameter(dt);
                                break;
                            case "DecisionGrid":
                                ManageUwDecisionGrid(dt);
                                break;
                            case "EXCEL_UPLOAD":
                                BindExcelRiskParameter(dt);
                                break;
                            case "AADHAR_PAN_VERIFICATION":
                                BindAadharDetails(dt);
                                break;
                            /*ID:9 START*/
                            case "TPA_STATUS":
                                BindTpaStatus(dt);
                                break;
                            /*ID:9 END*/
                            /*ID:9 START*/
                            case "SARAL_RISK":
                                //BindExcelRiskParameter(dt);
                                break;
                            /*ID:9 END*/
                            /*ID:10 START*/
                            case "DECISION_TYPE":
                                BindDecisionType(dt);
                                break;
                            /*ID:10 END*/
                            /*ID:11 START*/
                            case "WARNING":
                                //BindUwWarning(dt);
                                break;
                            case "HINT":
                                //BindUwHint(dt);
                                break;
                            /*ID:11 END*/
                            case "DISTANCE":
                                Binddistance(dt);
                                break;
                            case "TransactionNo":
                                BindAadharTransactionNo(dt);
                                break;
                            case "MedicalDE":
                                BindMedicalDELink(dt);
                                break;
                            case "IIBScore":
                                BindIIBScore(dt);
                                break;
                            //46.1 Begin of Changes; Bhaumik Patel - [CR-4753]
                            case "EXAprrovalDetails":
                                FillEXAprrovalDetails(strApplicationno);
                                clsName = divExceptionapproval.Attributes["class"].ToString();
                                divExceptionapproval.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                                break;
                            //46.1 End of Changes; Bhaumik Patel - [CR-4753]
                            //added by suraj on 14 FEB 2022 for CR-30589 risk category
                            case "RiskCategory":
                                BindRiskCategory(dt);
                                break;
                                //end
                        }
                    }catch(Exception ex)
        {
                        throw ex;
                    }
                }
            }
        }catch(Exception ex)
        {
            throw ex;
        }
       
    }

    private void PremiumCalculation(ReplicaXml objReplica, ref bool isdataSave, ref DataSet _dsPremiumCalculateResponse, ref int intResponseStatus, ref string strResponse)
    {
        /*variable declaration*/
        try
        {
            List<ProductSection> lstProductSection = new List<ProductSection>();
            string strComboMonthlyPayout = string.Empty;
            string strProdMonthlyPayout = string.Empty;
            lblErrorproddtls.Text = "";
            gridPremCal_Product.DataSource = null;
            gridPremCal_Product.DataBind();
            lblErrorproddtls.Text = string.Empty;
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            Logger.Info(strApplicationno + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :private PremiumCalculation" + System.Environment.NewLine);
            ProdDtls objprodchangevalue = new ProdDtls();
            ProductSection objProductSectionBase = new ProductSection();
            ProductSection objProductSectionCombo = new ProductSection();
            ApplicationSection objApplicationSection = new ApplicationSection();
            AppDtls objAppDtls = new AppDtls();
            objAppDtls._Backdate = Request.Form[txtRcdreq.UniqueID];
            objChangeObj.App_backdate = objAppDtls;
            //Addedby Suraj on 02JULY2020 for NAWP catagory
            if (txtProdcode.Text == "E83" || txtProdcode.Text == "E84")
            {
                if (ddlNAWPCatagory.SelectedValue != "0" && ddlNAWPCatagory.SelectedValue != null && ddlNAWPCatagory.SelectedValue != "")
                {
                    Session["NAWPCatagory"] = ddlNAWPCatagory.SelectedValue;
                    objChangeObj.App_backdate._NAWPCatagory = ddlNAWPCatagory.SelectedValue;

                }
            }

            /*added by shri to add application details*/

            #region Bind Application details to replica begin.
            objApplicationSection.AppNo = txtAppno.Text;
            objApplicationSection.PolNo = txtPolno.Text;
            objApplicationSection.AppSignDate = txtAppsigndate.Text;
            objApplicationSection.PIVCSTATUS = txtPivcStatus.Text;
            objApplicationSection.BACKDATEFLAG = switch_havInsurance.Checked;
            objApplicationSection.BACKDATEINTREST = txtBackDateIntrest.Text;
            objApplicationSection.AgentChannel = txtAgentChannel.Text;
            objApplicationSection.BACKDATE = txtRcdreq.Text;
            objApplicationSection.BACHDATEREASON = ddlBkdateReason.SelectedValue;
            objApplicationSection.IsStaff = hd_que_2.Checked;
            objApplicationSection.PIVCRJCREASON = txtPivcRejectReason.Text;
            objApplicationSection.Channel = txtAppchannel.Text;
            objApplicationSection.APP_isNSAP = cbIsNsap.Checked;
            if (imgPivcStatus.ImageUrl.Equals(@"../dist/img/Success.png"))
            {
                objApplicationSection.SCRH_PIVC_STATUS = 1;
            }
            else
            {
                objApplicationSection.SCRH_PIVC_STATUS = 0;
            }
            objApplicationSection.AUTOPAYTYPE = ddlAutoPaytype.SelectedValue;
            objApplicationSection.Section = "ApplicationDetails";
            #endregion Bind Application details to replica End.

            #region  Bind base Product details to replica begins.
            /*added by shri to fill product details*/
            objProductSectionBase.ProductCode = objprodchangevalue._ProdcodeBase = txtProdcode.Text;
            //objProductSectionBase.PolicyTerm = objprodchangevalue._PolicyTerm = Request.Form[txtPolterm.UniqueID];
            //objProductSectionBase.PremiumTerm = objprodchangevalue._Premiumpayingterm = Request.Form[txtPrepayterm.UniqueID];
            //objProductSectionBase.SumAssured = objprodchangevalue._Sumassured = Request.Form[txtSumassure.UniqueID];
            //objProductSectionBase.PremiumFreq = objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
            //objProductSectionBase.BasePremium = objprodchangevalue._Basepremiumamount = Request.Form[txtBasepremium.UniqueID];            
            //objProductSectionBase.MonthlyPayout = strProdMonthlyPayout = objprodchangevalue._MonthlyPayoutBase = Request.Form[txtMonthlyPayoutBase.UniqueID];
            objProductSectionBase.PolicyTerm = objprodchangevalue._PolicyTerm = txtPolterm.Text;
            objProductSectionBase.PremiumTerm = objprodchangevalue._Premiumpayingterm = txtPrepayterm.Text;
            objProductSectionBase.SumAssured = objprodchangevalue._Sumassured = txtSumassure.Text;
            objProductSectionBase.PremiumFreq = objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
            objProductSectionBase.BasePremium = objprodchangevalue._Basepremiumamount = txtBasepremium.Text;
            objProductSectionBase.MonthlyPayout = strProdMonthlyPayout = objprodchangevalue._MonthlyPayoutBase = txtMonthlyPayoutBase.Text;
            objProductSectionBase.TotalPremium = objprodchangevalue._TotalPremiumamount = txtTotalpremium.Text;
            objProductSectionBase.MonthlyPayout = (string.IsNullOrEmpty(objProductSectionBase.MonthlyPayout)) ? string.Empty : objProductSectionBase.MonthlyPayout;
            objProductSectionBase.ProductType = hdnProductType.Value;
            objProductSectionBase.ProdcutName = txtProname.Text;
            objProductSectionBase.PolicyNo = txtBasepolno.Text;
            objProductSectionBase.Section = "ProductDetails";

            //added by suraj for new product code T36/37 & E91/92 & E93/94
            objprodchangevalue._Category = ddlCategory.SelectedValue;
            objprodchangevalue._PayoutTerm = ddlPayoutTerm.SelectedValue;
            objprodchangevalue._PayoutType = ddlPayoutType.SelectedValue;
            objprodchangevalue._PayOutFrquency = ddlpayoutfreq.SelectedValue;
            objprodchangevalue._LumpSumPercent = txtLumpSumPercent.Text;
            objprodchangevalue._ProductCategory = ddlprodcategory.SelectedValue;

            #endregion  Bind base Product details to replica end.


            #region  Bind Combo Product details to replica Begin.
            if (!string.IsNullOrEmpty(txtCombProdCode.Text))
            {
                objProductSectionCombo.ProductCode = objprodchangevalue._ProdcodeCombo = txtCombProdCode.Text;
                objProductSectionCombo.PolicyTerm = objprodchangevalue._PolicyTermCombo = txtCombPolTerm.Text;
                objProductSectionCombo.PremiumTerm = objprodchangevalue._PremiumpayingtermCombo = txtCombPayTerm.Text;
                objProductSectionCombo.SumAssured = objprodchangevalue._SumassuredCombo = txtCombSumAssured.Text;
                objProductSectionBase.PremiumFreq = objprodchangevalue._Paymentfrequency = ddlFrequency.SelectedValue;
                objProductSectionCombo.BasePremium = objprodchangevalue._BasepremiumamountCombo = txtCombPremAmount.Text;
                objProductSectionCombo.TotalPremium = objprodchangevalue._TotalPremiumamountCombo = txtCombTotalPrem.Text;
                objProductSectionCombo.MonthlyPayout = strComboMonthlyPayout = objprodchangevalue._MonthlyPayoutCombo = txtComboMonthlyPayout.Text;
                objProductSectionCombo.MonthlyPayout = (string.IsNullOrEmpty(objProductSectionCombo.MonthlyPayout)) ? string.Empty : objProductSectionCombo.MonthlyPayout;
                objProductSectionCombo.ProductType = hdnProductType.Value;
                objProductSectionCombo.PolicyNo = txtCombopolno.Text;
                objProductSectionCombo.ProdcutName = txtCombProdName.Text;
                objProductSectionCombo.PremiumFreq = ddlComboFrequency.SelectedValue;
                objProductSectionCombo.Section = "ProductDetails";
            }
            #endregion Bind Combo Product details to replica Begin.
            //set product details to object pass to serbtnvices.
            objChangeObj.Prod_policydetails = objprodchangevalue;
            /*added by shri to rider details */
            bool blnIsGreateThanSumAssured = true;
            List<RiderInfo> LstRiderInfo = new List<RiderInfo>();


            foreach (GridViewRow rowfollowup in gvRiderDtls.Rows)
            {
                RiderInfo objRiderInfo = new RiderInfo();
                //define variable            
                CheckBox cbIsRider = (CheckBox)rowfollowup.FindControl("chkremoveRider");
                Label lblRiderName = (Label)rowfollowup.FindControl("lblRiderName");
                Label lblRiderCode = (Label)rowfollowup.FindControl("lblRiderCode");
                TextBox txtRiderSumAssured = (TextBox)rowfollowup.FindControl("txtRiderSumAssure");
                TextBox txtRiderTotalPremium = (TextBox)rowfollowup.FindControl("txtRiderPremium");
                TextBox txtServiceTax = (TextBox)rowfollowup.FindControl("txtriderservicetax");
                string[] strRiderSumAssured = txtRiderSumAssured.Text.Split(',');
                string[] strRiderTotalPremium = txtRiderTotalPremium.Text.Split(',');
                string[] strServiceTax = txtServiceTax.Text.Split(',');
                if (strRiderSumAssured.Length > 0)
                {
                    txtRiderSumAssured.Text = strRiderSumAssured[strRiderSumAssured.Length - 1];
                }
                if (strRiderTotalPremium.Length > 0)
                {
                    txtRiderTotalPremium.Text = strRiderTotalPremium[strRiderTotalPremium.Length - 1];
                }
                if (strServiceTax.Length > 0)
                {
                    txtServiceTax.Text = strServiceTax[strServiceTax.Length - 1];
                }
                if (cbIsRider.Checked && string.IsNullOrEmpty(txtRiderSumAssured.Text))
                {
                    lblRiderDetailsError.Text = "Enter sum assured for all the checked riders";
                    return;
                }
                else if (cbIsRider.Checked && !string.IsNullOrEmpty(txtRiderSumAssured.Text) && Convert.ToDouble(txtRiderSumAssured.Text) < 1)
                {
                    lblRiderDetailsError.Text = "Enter sum assured for all the checked riders";
                    return;
                }
                //get value from it
                objRiderInfo.IsActive = cbIsRider.Checked;
                objRiderInfo.RiderId = lblRiderCode.Text;
                objRiderInfo.SumAssured = string.IsNullOrEmpty(txtRiderSumAssured.Text) ? Convert.ToDouble("0.00") : Convert.ToDouble(txtRiderSumAssured.Text);
                objRiderInfo.RiderName = lblRiderName.Text;
                //objRiderInfo.
                LstRiderInfo.Add(objRiderInfo);
                //add data row to datatable            
                if (!string.IsNullOrEmpty(txtRiderSumAssured.Text))
                {
                    if (Convert.ToDouble(txtRiderSumAssured.Text) > Convert.ToDouble(txtSumassure.Text))
                    {
                        blnIsGreateThanSumAssured = false;
                        break;
                    }
                }
            }
            if (!blnIsGreateThanSumAssured)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Rider Sum sssured should be less than total sum assured')", true);
            }
            /*end here*/
            objChangeObj.RiderInfo = LstRiderInfo;
            //fill application objet
            //Add Application object To Replica Xml
            if (objApplicationSection != null)
            {
                objReplica.ApplicationSection = objApplicationSection;
                Logger.Info(strApplicationno + "MethodeName :ApplicationDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :ApplicationDetails Object" + GetXMLFromObject(objApplicationSection) + System.Environment.NewLine);
            }

            //To get NonMedicalLoading and MedicalClass
            FillLoadParam(ref objChangeObj);

            /*call premium calculation service*/
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _dsPremiumCalculateResponse, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (_dsPremiumCalculateResponse != null && _dsPremiumCalculateResponse.Tables[0].Rows.Count > 0)
            {
                /*added by shri on 12 dec 17 to use proper back date interest*/
                //objApplicationSection.BACKDATEINTREST = Convert.ToString(_dsPremiumCalculateResponse.Tables[0].Rows[0]["BackDateintrest"]);
                objApplicationSection.BACKDATEINTREST = Convert.ToString(_dsPremiumCalculateResponse.Tables[0].Rows[0]["BackdatedInt"]);
                /*end here*/
                int _ProdResult = 0;
                List<RiderInfo> lstPcRiderInfo = new List<RiderInfo>();
                Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstPcRiderInfo, _dsPremiumCalculateResponse.Tables[0]);
                /*update product details*/
                //RiderInfo objProd = lstPcRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtProdcode.Text)).SingleOrDefault();
                RiderInfo objProd = lstPcRiderInfo.Where(x => x.ProductCode.Equals(txtProdcode.Text) && x.RiderType.Equals("BS")).SingleOrDefault();
                if (objProd != null)
                {
                    _ProdResult = 1;
                    objProductSectionBase.BasePremium = txtBasepremium.Text = objprodchangevalue._Basepremiumamount = objProd.InstalmentPremiumAmt.ToString();
                    objProductSectionBase.TotalPremium = txtTotalpremium.Text = objprodchangevalue._TotalPremiumamount = objProd.TotalPremiumAmount.ToString();
                    objProductSectionBase.ServiceTax = txtServicetax.Text = objprodchangevalue._ServiceTax = objProd.ServiceTax.ToString();
                    objProductSectionBase.SumAssured = txtSumassure.Text = objprodchangevalue._Sumassured = objProd.SumAssured.ToString();
                    //objComm.OnlineProductDetails_Save(strChannelType, strApplicationno, strPolicyNo, txtProdcode.Text, txtSumassure.Text, txtPolterm.Text, txtPrepayterm.Text
                    //    , ddlFrequency.SelectedValue, txtBasepremium.Text, txtTotalpremium.Text, txtServicetax.Text, txtMonthlyPayoutBase.Text, ref _ProdResult);
                    if (_ProdResult > 0)
                    {
                        _ProdResult = 0;
                        //lblErrorproddtls.Text = "Product Details Updated Successfully";
                        //lblErrorProductDetailBody.Text += ",Product Details save succeed";
                    }
                    else
                    {
                        //lblErrorproddtls.Text = "Product Details Not Updated Successfully CLICK here to know more";
                        //lblErrorProductDetailBody.Text += ",Product Details NOT save in database";
                    }
                    lstProductSection.Add(objProductSectionBase);
                }
                RiderInfo objComb = lstPcRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(txtCombProdCode.Text)).SingleOrDefault();
                if (objComb != null)
                {
                    _ProdResult = 1;
                    objProductSectionCombo.BasePremium = txtCombPremAmount.Text = objprodchangevalue._BasepremiumamountCombo = objComb.InstalmentPremiumAmt.ToString();
                    objProductSectionCombo.TotalPremium = txtCombTotalPrem.Text = objprodchangevalue._TotalPremiumamountCombo = objComb.TotalPremiumAmount.ToString();
                    objProductSectionCombo.ServiceTax = txtCombServiceTax.Text = objprodchangevalue._ServiceTaxCombo = objComb.ServiceTax.ToString();
                    objProductSectionCombo.SumAssured = txtCombSumAssured.Text = objprodchangevalue._SumassuredCombo = objComb.SumAssured.ToString();
                    objProductSectionCombo.MonthlyPayout = txtComboMonthlyPayout.Text = strComboMonthlyPayout = string.IsNullOrEmpty(strComboMonthlyPayout) ? "0" : strComboMonthlyPayout;
                    lstProductSection.Add(objProductSectionCombo);
                    if (_ProdResult > 0)
                    {
                        _ProdResult = 1;
                        lblErrorProductDetailBody.Text += ",Combo Product Details save succeed";
                    }
                    else
                    {
                        lblErrorProductDetailBody.Text += ",Combo Product Details NOT save in database";
                    }

                }

                objChangeObj.Prod_policydetails = objprodchangevalue;
                //add product object to replica xml.
                objReplica.LstProductSection = lstProductSection;
                Logger.Info(strApplicationno + "MethodeName :ProductDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :ProductDetails Object" + GetXMLFromObject(lstProductSection) + System.Environment.NewLine);
                /*update rider info*/
                //_dsPremiumCalculateResponse.Tables[0].Columns.RemoveAt(2);
                //_dsPremiumCalculateResponse.Tables[0].Columns.RemoveAt(2);
                gridPremCal_Product.DataSource = _dsPremiumCalculateResponse;
                gridPremCal_Product.DataBind();
                chkProductDtls.Checked = false;


                for (int i = 0; i < LstRiderInfo.Count; i++)
                {
                    if (lstPcRiderInfo.Count > i)
                    {
                        RiderInfo objRiderServiceTax = lstPcRiderInfo[i];
                        RiderInfo objRiderInfo = LstRiderInfo.Where(x => x.RiderId == objRiderServiceTax.RiderId).FirstOrDefault();
                        int index = LstRiderInfo.FindIndex(x => x.RiderId == objRiderServiceTax.RiderId);
                        if (index != -1)
                        {
                            objRiderInfo.ServiceTax = objRiderServiceTax.ServiceTax;
                            objRiderInfo.TotalInstalmentPremium = objRiderServiceTax.TotalInstalmentPremium;
                            objRiderInfo.SumAssured = objRiderServiceTax.SumAssured;
                            LstRiderInfo[index] = objRiderInfo;
                        }
                    }

                }
                // add rider object to replica xml.
                objReplica.LstRiderInfo = LstRiderInfo;
                intResponseStatus = 0;
                strResponse = string.Empty;


            }
            else
            {
                //FillProductDetails(strApplicationno, strChannelType);
                chkProductDtls.Checked = false;
                lblErrorproddtls.Text = "Premium Calculation Failed to know more click here";
                strResponse = lblErrorProductDetailBody.Text = strLApushStatus;
                intResponseStatus = 1;
                isdataSave = false;
            }
            /*ID:8 START*/
            IsBasePremiumDifferentChecker();
            /*ID:8 END*/
            /*ID:10 START*/
            if (IsUserLimitLessThanSumAssured())
            {
                RemoveWarningMessage("1");
                FillWarningMessage("1");
                DisplaySaralWarningMessage();
            }
            /*ID:10 END*/
        }
        catch (Exception Error)
        {
            strResponse = "Error While Calculating Premium " + System.Environment.NewLine;
            isdataSave = false;
            Logger.Info(strApplicationno + "PAGE_NAME:UWPortfolio/Uwdecision.aspx/ //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR:Service Call Execution ERROR: PREMIUM CALCULATION" + "ERROR MESSAGE:" + Error.Message + System.Environment.NewLine);
        }
    }

    private void FillProductDetails(DataTable dt, MasterPageComparision objMasterPageComparision)
    {
        List<ProductSection> lstProductSection = new List<ProductSection>();
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductSection objProductSection = new ProductSection();

                objProductSection.PolicyTerm = Convert.ToString(dt.Rows[i]["POLICY_TERM"]);
                objProductSection.PremiumFreq = Convert.ToString(dt.Rows[i]["PREMIUM_FREQ"]);
                objProductSection.SumAssured = Convert.ToString(dt.Rows[i]["SUM_ASSURED"]);
                objProductSection.ProductCode = Convert.ToString(dt.Rows[i]["PROD_CODE"]);
                objProductSection.PremiumTerm = Convert.ToString(dt.Rows[i]["PAYING_TERM"]);
                lstProductSection.Add(objProductSection);
            }
        }
        objMasterPageComparision.LstProductSection = lstProductSection;
    }

    private void FillRiderDetails(DataTable dt, MasterPageComparision objMasterPageComparision)
    {
        if (dt != null & dt.Rows.Count > 0)
        {
            List<RiderSection> lstRiderSection = new List<RiderSection>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RiderSection objRiderSection = new RiderSection();
                objRiderSection.RiderCode = Convert.ToString(dt.Rows[i]["RIDERCODE"]);
                objRiderSection.RiderSumAssured = Convert.ToString(dt.Rows[i]["RIDERSUMASSURED"]);
                lstRiderSection.Add(objRiderSection);
            }
            objMasterPageComparision.LstRiderSection = lstRiderSection;
        }
    }

    private void FillClientDetails(DataTable dt, MasterPageComparision objMasterPageComparision)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            List<ClientSection> lstClientDetails = new List<ClientSection>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ClientSection objClientDetails = new ClientSection();
                objClientDetails.ClientRole = Convert.ToString(dt.Rows[i]["Role"]);
                objClientDetails.ClientId = Convert.ToString(dt.Rows[i]["ClientId"]);
                lstClientDetails.Add(objClientDetails);
            }
            objMasterPageComparision.LstClientDetails = lstClientDetails;
        }
    }

    private void ShowPopupMessage(string alertMessage)
    {
        //lblErrorinfo.Text = alertMessage;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
    }

    public void FillLoadParam(ref ChangeValue objChangeValue)
    {
        List<LoadParam> lstLoadParam = new List<LoadParam>();
        LoadParam objLoadParamBS = new LoadParam();
        LoadParam objLoadParamAD = new LoadParam();
        DataTable _dtExistingLoad = null;
        DataTable _dtLoadMaster = null;
        DataTable _dtNewLoading = null;
        int strNonMedicalLoadingBS = 0, strNonMedicalLoadingAD = 0;
        string strMortalityClassBS = string.Empty;
        string strMortalityClassAD = string.Empty;
        _dtExistingLoad = (DataTable)ViewState["ExitingLoadingData"];
        _dtNewLoading = (DataTable)ViewState["LoadingData"];
        _dtLoadMaster = (DataTable)ViewState["LoadMaster"];
        int k = 0;
        if (_dtExistingLoad != null)
        {
            if (_dtExistingLoad.Rows.Count > 0)
            {
                objLoadParamAD.strMedicalLoadingClass1 = new string[_dtExistingLoad.Rows.Count];
                objLoadParamAD.strNonMedicalLoading1 = new string[_dtExistingLoad.Rows.Count];
                objLoadParamAD.strRiderCode = new string[_dtExistingLoad.Rows.Count];
                objLoadParamBS.strRiderCode = new string[_dtExistingLoad.Rows.Count];
                int ridercount = 0;
                foreach (DataRow dtRow in _dtExistingLoad.Rows)
                {
                    //dtRow["RiderCtg"].ToString().Trim().Equals("BS") &&
                    if (dtRow["RiderCtg"].ToString().Trim().Equals("BS"))
                    {
                        //objLoadParamBS = new LoadParam();
                        if (!dtRow["LoadingType"].ToString().ToUpper().Trim().Equals("MEDICAL"))
                        {
                            strNonMedicalLoadingBS += string.IsNullOrEmpty(dtRow["RateAdjustment"].ToString()) ? 0 : Convert.ToInt32(dtRow["RateAdjustment"]);
                        }
                        else
                        {
                            strMortalityClassBS = Convert.ToString(dtRow["FlatMortality"].ToString().Trim());
                        }
                        objLoadParamAD.strNonMedicalLoading1[ridercount] = Convert.ToString(strNonMedicalLoadingBS);
                        objLoadParamAD.strMedicalLoadingClass1[ridercount] = Convert.ToString(strMortalityClassBS);
                        //objLoadParamBS.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingBS);
                        //objLoadParamBS.strMedicalLoadingClass = Convert.ToString(strMortalityClassBS);
                        objLoadParamAD.strRiderCtg = Convert.ToString(dtRow["RiderCtg"]);
                        objLoadParamAD.strRiderCode[ridercount] = Convert.ToString(dtRow["RiderName"]);
                    }
                    else
                    {
                        //objLoadParamAD = new LoadParam();
                        if (!dtRow["LoadingType"].ToString().ToUpper().Trim().Equals("MEDICAL"))
                        {
                            strNonMedicalLoadingAD = string.IsNullOrEmpty(dtRow["RateAdjustment"].ToString()) ? 0 : Convert.ToInt32(dtRow["RateAdjustment"]);
                        }
                        else
                        {
                            strMortalityClassAD = Convert.ToString(dtRow["FlatMortality"].ToString().Trim());
                        }
                        //objLoadParamAD.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingAD);
                        //objLoadParamAD.strMedicalLoadingClass = Convert.ToString(strMortalityClassAD);
                        k++;
                        objLoadParamAD.strNonMedicalLoading1[ridercount] = Convert.ToString(strNonMedicalLoadingAD);
                        objLoadParamAD.strMedicalLoadingClass1[ridercount] = Convert.ToString(strMortalityClassAD);
                        objLoadParamAD.strRiderCtg = Convert.ToString(dtRow["RiderCtg"]);
                        objLoadParamAD.strRiderCode[ridercount] = Convert.ToString(dtRow["RiderName"]);
                    }
                    ridercount++;
                }
            }
        }

        if (_dtNewLoading != null)
        {
            if (_dtNewLoading.Rows.Count > 0)
            {
                objLoadParamAD.strMedicalLoadingClass1 = new string[_dtNewLoading.Rows.Count];
                objLoadParamAD.strNonMedicalLoading1 = new string[_dtNewLoading.Rows.Count];
                objLoadParamAD.strRiderCode = new string[_dtNewLoading.Rows.Count];
                objLoadParamBS.strRiderCode = new string[_dtNewLoading.Rows.Count];
                int ridercount = 0;
                foreach (DataRow dtRow in _dtNewLoading.Rows)
                {
                    DataView dv = new DataView(_dtLoadMaster);
                    DataTable dt = new DataTable();
                    dt = _dtLoadMaster;
                    string strRiderName = dtRow["LoadType"].ToString();
                    dv.RowFilter = "NAME = '" + strRiderName + "'"; //+ dtRow["RiderName"].ToString();
                    string strRiderCtg = string.Empty;
                    //strRiderCtg = dv[0]["RiderCtg"].ToString();
                    strRiderCtg = dt.Rows[0]["RiderCtg"].ToString();
                    if (strRiderCtg.Trim().ToUpper().Equals("BS"))
                    {      //41.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                        if ((!dtRow["ddlLoadType"].ToString().ToUpper().Trim().Equals("MEDICAL")) && (!dtRow["ddlLoadType"].ToString().ToUpper().Trim().Equals("HABITS")))
                        { //41.1 End of Changes; Bhaumik Patel - [CR-3334]
                            strNonMedicalLoadingBS += string.IsNullOrEmpty(dtRow["txtRateAdjust"].ToString()) ? 0 : Convert.ToInt32(dtRow["txtRateAdjust"]);
                        }
                        else
                        {
                            strMortalityClassBS = Convert.ToString(dtRow["ddlLoadFlatMortality"].ToString().Trim());
                        }
                        objLoadParamAD.strNonMedicalLoading1[ridercount] = Convert.ToString(strNonMedicalLoadingBS);
                        objLoadParamAD.strMedicalLoadingClass1[ridercount] = Convert.ToString(strMortalityClassBS);
                        //objLoadParamBS.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingBS);
                        //objLoadParamBS.strMedicalLoadingClass = Convert.ToString(strMortalityClassBS);
                        objLoadParamAD.strRiderCtg = strRiderCtg;
                        //objLoadParamAD.strRiderCode[ridercount] = dv[0]["VALUE"].ToString();
                        objLoadParamAD.strRiderCode[ridercount] = dt.Rows[0]["VALUE"].ToString();
                    }
                    else
                    {     //41.1 Begin of Changes; Bhaumik Patel - [CR-3334]
                        if ((!dtRow["ddlLoadType"].ToString().ToUpper().Trim().Equals("MEDICAL")) && (!dtRow["ddlLoadType"].ToString().ToUpper().Trim().Equals("HABITS")))
                        {   //41.1 End of Changes; Bhaumik Patel - [CR-3334]
                            strNonMedicalLoadingAD = string.IsNullOrEmpty(dtRow["txtRateAdjust"].ToString()) ? 0 : Convert.ToInt32(dtRow["txtRateAdjust"]);
                        }
                        else
                        {
                            strMortalityClassAD = Convert.ToString(dtRow["ddlLoadFlatMortality"].ToString().Trim());
                        }
                        //objLoadParamAD.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingAD);
                        //objLoadParamAD.strMedicalLoadingClass = Convert.ToString(strMortalityClassAD);
                        k++;
                        objLoadParamAD.strNonMedicalLoading1[ridercount] = Convert.ToString(strNonMedicalLoadingAD);
                        objLoadParamAD.strMedicalLoadingClass1[ridercount] = Convert.ToString(strMortalityClassAD);
                        objLoadParamAD.strRiderCtg = strRiderCtg;
                        //objLoadParamAD.strRiderCode[ridercount] = dv[0]["VALUE"].ToString();
                        objLoadParamAD.strRiderCode[ridercount] = dt.Rows[0]["VALUE"].ToString();
                    }
                    ridercount++;
                }
            }
        }
        if (!string.IsNullOrEmpty(objLoadParamBS.strRiderCtg))
        {
            lstLoadParam.Add(objLoadParamAD);
        }
        if (!string.IsNullOrEmpty(objLoadParamAD.strRiderCtg))
        {
            lstLoadParam.Add(objLoadParamAD);
        }

        if (objChangeValue.Load_Loadingdetails != null)
        {
            objChangeValue.Load_Loadingdetails.lstLoadParam = lstLoadParam;
        }
        else
        {
            objChangeValue.Load_Loadingdetails = new LoadDtls();
            objChangeValue.Load_Loadingdetails.lstLoadParam = lstLoadParam;
        }
    }
    /*added by srhi on 16 dec 17 to add/remove nsap loading */
    protected void cbIsNsap_CheckedChanged(object sender, EventArgs e)
    {
        lblErrorAppDetailsBody.Text = lblErrorappdtls.Text = string.Empty;
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :cbIsNsap_CheckedChanged" + System.Environment.NewLine);
        try
        {
            ChangeNsapValue();
        }
        catch (Exception ex)
        {
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :cbIsNsap_CheckedChanged error" + " " + ex.Message + System.Environment.NewLine);
            lblErrorappdtls.Text = "NSAP Loading not added successfully CLICK HERE to know more";
            lblErrorAppDetailsBody.Text = ex.Message;
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    private void RemoveNsapLoadingDetails(string strApplicationNo, string strChannelType, ref string response)
    {
        if (ViewState["ExitingLoadingData"] != null)
        {
            DataTable dt = (DataTable)ViewState["ExitingLoadingData"];
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("LoadReasoncode1='NN'");
                if (dr != null && dr.Count() > 0)
                {
                    DeleteSpecificLoading(dr[0], strApplicationNo, strChannelType, ref response);
                    dt.Rows.Remove(dr[0]);
                    ViewState["ExitingLoadingData"] = null;
                    BindLoadingDetails(dt);
                }
            }
        }
    }

    /*added by srhi on 16 dec 17 to add sis requirement */
    private void AddNewRequirement(string strReqFollowupCode, string strReqDescription, string strReqCategory, string strReqCriteria, string strReqLifeType, string strReqStatus
            , string srtReqRaisedBy)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :AddNewRowToGrid1" + System.Environment.NewLine);
        if (ViewState["CurrentTable1"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            DataRow drCurrentRow = null;
            //Label lblClosedBy;


            if (dtCurrentTable.Rows.Count > 0)
            {
                //DataRow drTest =dtCurrentTable

                //add new row to DataTable               
                DataRow[] dr = dtCurrentTable.Select("REQ_followupCode='SIS'");
                if (dr != null && dr.Count() < 1)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow[0] = strReqFollowupCode;
                    drCurrentRow[1] = strReqDescription;
                    drCurrentRow[2] = strReqCategory;
                    drCurrentRow[3] = strReqCriteria;
                    drCurrentRow[4] = strReqLifeType;
                    drCurrentRow[5] = strReqStatus;
                    drCurrentRow[6] = srtReqRaisedBy;
                    drCurrentRow[7] = System.DateTime.Now.ToString();
                    drCurrentRow[8] = System.DateTime.Now.ToString();
                    drCurrentRow[9] = System.DateTime.Now.ToString();
                    dtCurrentTable.Rows.Add(drCurrentRow);
                }
                //Rebind the Grid with the current data
                gvRequirmentDetails.DataSource = dtCurrentTable;
                gvRequirmentDetails.DataBind();

                UpdatePanel1.Update();
            }
        }
        //Set Previous Data on Postbacks
        //SetPreviousData1();
    }

    /*added by shri on 28 dec 17 to add tracking*/
    private void InsertUwDecisionTracking(string strApplicationNo, string strUserId, DateTime dtCurrentDateTime, string strEventName, ref int intRet)
    {
        objcomm = new Commfun();
        objcomm.InsertUwDecisionTracking(strApplicationNo, strUserId, dtCurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fff"), strEventName, ref intRet);
    }

    /*added by shri on 28 dec 17 to update tracking*/
    private void UpdateUwDecisionTracking(int intSrNo, DateTime dtEndDate, ref int intRet)
    {
        objcomm = new Commfun();
        objcomm.UpdateUwDecisionTracking(intSrNo, dtEndDate.ToString("yyyy-MM-dd HH:mm:ss:fff"), ref intRet);
    }
    /*ID:3 START*/
    protected void btnAddComment_Click(object sender, EventArgs e)
    {
        try
        {
            AutoComment objAutoComment = new AutoComment();
            objBuss = new BussLayer();
            objBuss.FetchAutoCommentDetails(strApplicationno, strChannelType, ref _ds);
            BindCommentSection(objAutoComment, _ds);
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref dsMSAR, ref _dsPrevPol, "MSAR", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            BindSarDetails(objAutoComment, dsMSAR);
            txtUWComments.Value = objAutoComment.SummaryComment;
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    private void BindCommentSection(AutoComment objAutoComment, DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            objAutoComment.TotalPremium = Convert.ToString(_ds.Tables[0].Rows[0]["TOTAL_PREMIUM"]);
            objAutoComment.NameOfLa = Convert.ToString(_ds.Tables[0].Rows[0]["LA_NAME"]);
            objAutoComment.Gender = Convert.ToString(_ds.Tables[0].Rows[0]["GENDER"]);
            objAutoComment.Age = Convert.ToString(_ds.Tables[0].Rows[0]["AGE"]);
            objAutoComment.Education = Convert.ToString(_ds.Tables[0].Rows[0]["EDUCATION"]);
            objAutoComment.Occupation = Convert.ToString(_ds.Tables[0].Rows[0]["OCCUPATION"]);
            objAutoComment.AnnualIncome = Convert.ToString(_ds.Tables[0].Rows[0]["ANNUAL_INCOME"]);
            objAutoComment.Nominee = Convert.ToString(_ds.Tables[0].Rows[0]["NOMINEE_RELATION"]);
            objAutoComment.Proposer = Convert.ToString(_ds.Tables[0].Rows[0]["PROPOSAR_NAME"]);
            objAutoComment.Bmi = Convert.ToString(_ds.Tables[0].Rows[0]["BMI"]);
            objAutoComment.NatureOfDuty = Convert.ToString(_ds.Tables[0].Rows[0]["NATURE_OF_DUTY"]);
            objAutoComment.TypeOfIncomeProof = Convert.ToString(_ds.Tables[0].Rows[0]["INCOME_PROOF"]);
            objAutoComment.IdProof = Convert.ToString(_ds.Tables[0].Rows[0]["ID_PROOF"]);
            objAutoComment.AddressProof = Convert.ToString(_ds.Tables[0].Rows[0]["ADDR_PROOF"]);
            objAutoComment.AgeProof = Convert.ToString(_ds.Tables[0].Rows[0]["AGE_PROOF"]);
            objAutoComment.FamilyHistory = Convert.ToString(_ds.Tables[0].Rows[0]["FAMILY_HISTORY"]);
            objAutoComment.PersonalMedicalHistory = Convert.ToString(_ds.Tables[0].Rows[0]["PERSONAL_MEDICAL_HISTORY"]);
            //Added by Ajay sahu for auto comment format
            objAutoComment.PlanName = Convert.ToString(_ds.Tables[0].Rows[0]["PLAN_NAME"]);
            objAutoComment.SumAssured = Convert.ToString(_ds.Tables[0].Rows[0]["SUM_ASSURED"]);
            objAutoComment.PolicyTerm = Convert.ToString(_ds.Tables[0].Rows[0]["POLICY_TERM"]);
            objAutoComment.PolicyPayingTerm = Convert.ToString(_ds.Tables[0].Rows[0]["PLOLICY_PAYING_TERM"]);
            objAutoComment.MaritualStatus = Convert.ToString(_ds.Tables[0].Rows[0]["MARITAL_STATUS"]);
            objAutoComment.BranchCode = Convert.ToString(_ds.Tables[0].Rows[0]["BRANCH_CODE"]);
            objAutoComment.RiskScore = Convert.ToString(_ds.Tables[0].Rows[0]["RISK_SCORE"]);
            objAutoComment.EYRiskScore = Convert.ToString(_ds.Tables[0].Rows[0]["ENY_VALUE"]);
            objAutoComment.ChannelName = Convert.ToString(_ds.Tables[0].Rows[0]["CHANNEL_NAME"]);
            string existsins = string.Empty;
            if (_ds.Tables.Count > 0 && _ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < _ds.Tables[1].Rows.Count; i++)
                {
                    var stringArr = _ds.Tables[1].Rows[i].ItemArray.Select(x => x.ToString()).ToArray();
                    existsins += Environment.NewLine + string.Join("/", stringArr.ToArray());

                }
            }
            objAutoComment.ExistingIns = existsins;
            //Added by suraj on 04/09/2018 as discussed with shabbir sir -display total SA(existing insurance+current) for HHI and Non HHI in auto comment
            if (_ds.Tables.Count > 2 && _ds.Tables[2].Rows.Count > 0)
            {
                objAutoComment.TOTALSA_HHI = Convert.ToString(_ds.Tables[2].Rows[0]["HHI_TSA"]);
                objAutoComment.TOTALSA_NON_HHI = Convert.ToString(_ds.Tables[2].Rows[0]["NON_HHI_TSA"]);
            }
        }
    }
    private void BindSarDetails(AutoComment objAutoComment, DataSet _ds)
    {
        if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
        {
            DataRow[] dr = _ds.Tables[0].Select("ClientRole='LA'");
            if (dr != null && dr.Length > 0)
            {
                //objAutoComment.TMSAR = Convert.ToString(dr[0]["TSAR"]);
                objAutoComment.TMSAR = Convert.ToString(dr[0]["MSAR"]);
                objAutoComment.FSAR = Convert.ToString(dr[0]["Fsar"]);
                objAutoComment.TSAR = Convert.ToString(dr[0]["Tsar"]);

            }

        }
        else
        {
            objAutoComment.TMSAR = "";
            objAutoComment.FSAR = "";
            objAutoComment.TSAR = "";
        }
    }
    /*ID:3 END*/
    /*end here*/

    /*ID:4 START*/
    private void ManageContent()
    {
        if (strChannelType.ToLower().Equals("online"))
        {
            string strClass = string.Empty;
            strClass = divPanDetails.Attributes["class"].ToString();
            divPanDetails.Attributes["class"] = strClass.Replace(" HideControl", string.Empty);
            strClass = divAadharDetails.Attributes["class"].ToString();
            divAadharDetails.Attributes["class"] = strClass.Replace(" HideControl", string.Empty);
        }
        else
        {
            string strClass = string.Empty;
            strClass = divPanDetails.Attributes["class"].ToString();
            if (!strClass.Contains("HideControl"))
            {
                divPanDetails.Attributes["class"] = strClass + " HideControl";
            }

            strClass = divAadharDetails.Attributes["class"].ToString();
            if (!strClass.Contains("HideControl"))
            {
                divAadharDetails.Attributes["class"] = strClass + " HideControl";
            }
        }
    }
    /*ID:4 END*/
    /*ID:5 START*/
    protected void BtnAadharDetails_Click(object sender, EventArgs e)
    {
        try
        {
            /*ID:18 Start*/
            CheckBox3.Checked = false;
            /*ID:18 End*/
            int intRet = -1;
            if (!string.IsNullOrEmpty(txtAadharNumber.Text))
            {
                objUWDecision = new UWSaralDecision.BussLayer();
                objChangeObj = new ChangeValue();
                ClientDetails objClientDetails = new ClientDetails();
                objClientDetails.Aadhar = txtAadharNumber.Text;
                objChangeObj.ClientDetails = objClientDetails;
                _ds = new DataSet();
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _ds, "AADHAR_VERIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                if (strLApushErrorCode == -1)
                {
                    lblErrorAadharBody.Text = lblErrorAadharHead.Text = "Try Again Later";
                }
                else if (strLApushErrorCode == 1)
                {
                    lblErrorAadharBody.Text = lblErrorAadharHead.Text = "Aadhar Is Matched";
                    ManagePanAdharFlagUpdation(strApplicationno, true, 2, ref intRet);
                    ChangeThumbStatus(imgAadharVerified, true);
                }
                else if (strLApushErrorCode == 0)
                {
                    lblErrorAadharBody.Text = lblErrorAadharHead.Text = "Aadhar Not Matched";
                    ManagePanAdharFlagUpdation(strApplicationno, true, 2, ref intRet);
                    ChangeThumbStatus(imgAadharVerified, false);
                }
                gvUWDecisionAadharDetails.DataSource = _ds;
                gvUWDecisionAadharDetails.DataBind();
            }
            else
            {
                lblErrorAadharBody.Text = lblErrorAadharHead.Text = "Enter Aadhar Number";
            }
        }
        catch (Exception)
        {
            lblErrorAadharBody.Text = lblErrorAadharHead.Text = "Try Again Later";

        }
        finally
        {
            updAutoUwDetails.Update();
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    protected void btnUpdateAadharDetails_Click(object sender, EventArgs e)
    {
        try
        {
            /*ID:18 Start*/
            CheckBox3.Checked = false;
            /*ID:18 End*/
            int intRet = -1;
            if (!string.IsNullOrEmpty(txtAadharNumber.Text))
            {
                objBuss = new BussLayer();
                if (Session["objLoginObj"] != null)
                {
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                }

                objBuss.UpdateUWDecitionAdditionalInfoDetails(strApplicationno, txtAadharNumber.Text, 1, objChangeObj.userLoginDetails._UserID, ref intRet);
                if (intRet > 0)
                {
                    lblErrorAadharHead.Text = "Aadhar Number Saved";
                    lblErrorAadharBody.Text = "Aadhar Number Saved";
                }
                else
                {
                    lblErrorAadharHead.Text = "Aadhar Number Not Saved";
                    lblErrorAadharBody.Text = "Aadhar Number Not Saved";
                }
            }
            else
            {
                lblErrorAadharHead.Text = "Update aadhar failed click here to know more";
                lblErrorAadharBody.Text = "Enter Aadhar Number";
            }
        }
        catch (Exception)
        {

        }
    }
    private void BindAadharDetails(DataTable dt)
    {

        txtAadharNumber.Text = Convert.ToString(dt.Rows[0]["AadharNumber"]);
        string strImageUrl = string.Empty;
        //if (Convert.ToBoolean(dt.Rows[0]["IsPanVerified"]))
        //{
        //    ChangeThumbStatus(imgPanVerified, true);
        //}
        //else
        //{
        //    ChangeThumbStatus(imgPanVerified, false);
        //}

        if (!string.IsNullOrEmpty(txtAadharNumber.Text))
        {
            ChangeThumbStatus(imgAadharVerified, true);
            chkIsAadharVerified.Checked = true;
        }
        else
        {
            ChangeThumbStatus(imgAadharVerified, false);
            chkIsAadharVerified.Checked = false;
        }


        //End
    }
    private void BindAadharTransactionNo(DataTable dt)
    {
        //Added By Suraj on 28/08/2018 for BDIP

        if (strChannelType == "ONLINE")
        {
            divimgekyc.Attributes.Add("Class", "form-group");
        }
        else
        {
            divimgekyc.Attributes.Add("Class", "form-group HideControl");
        }
        lblekycbdip.Text = Convert.ToString(dt.Rows[0]["AadharTransaction"]);
        if (Convert.ToString(lblekycbdip.Text) == "" || Convert.ToString(lblekycbdip.Text) == null)
        {
            divaadhartran.Attributes.Add("Class", "form-group HideControl");
            ChangeThumbStatus(imgekycverified, false);
        }
        else
        {
            divaadhartran.Attributes.Add("Class", "form-group");
            ChangeThumbStatus(imgekycverified, true);
        }

        //End
    }
    private void ManagePanAdharFlagUpdation(string strApplicationno, bool blnIsVerified, int intFlag, ref int intRet)
    {
        objBuss = new BussLayer();
        objBuss.UpdateUWDecitionAdditionalInfoFlag(strApplicationno, blnIsVerified, intFlag, ref intRet);
    }

    private void ChangeThumbStatus(Image img, bool blnFlag)
    {
        string strImageUrl = string.Empty;
        if (blnFlag)
        {
            strImageUrl = @"../dist/img/Success.png";
            img.ImageUrl = strImageUrl;
        }
        else
        {
            strImageUrl = @"~/dist/img/Failuer.png";
            img.ImageUrl = strImageUrl;
        }
        updAutoUwDetails.Update();
    }
    /*ID:5 END*/
    protected void chkIsPanValidate_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int intRet = -1;
            ManagePanAdharFlagUpdation(strApplicationno, chkIsPanValidate.Checked, 1, ref intRet);
            ChangeThumbStatus(imgPanVerified, chkIsPanValidate.Checked);
        }
        catch (Exception)
        {
            lblErrorAadharHead.Text = lblErrorAadharBody.Text = "Try Again Later";
        }
    }
    /*ID:6 START*/
    private void SaveUwComment()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnUWCommSave_Click");
        int UWCommentResult = 0;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        lblErrorLoadingDetailBody.Text = lblErrorcommentdtls.Text = string.Empty;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
        /*added by shri on 28 dec 17 to add tracking*/
        int intTrackingRet = -1;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "COMMENT_SAVE", ref intTrackingRet);
        /*end here*/
        int intRef = -1;
        objComm.ManageApplicationLifeCycle(strApplicationno, objChangeObj.userLoginDetails._UserID, "UW_DECISION_COMMENT", false, ref intRef);
        /*END HERE*/
        objComm.OnlineUWCommentsDetails_Save(objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._UserGroup, Convert.ToString(txtUWComments.Value), strApplicationno, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ddlComment.SelectedValue, ref UWCommentResult);
        if (UWCommentResult != 0)
        {
            ddlComment.SelectedValue = "General";
            txtUWComments.Value = "";
            FillCommentsDetails(strApplicationno, strChannelType, ddlComment.SelectedValue);
            lblErrorcommentdtls.Text = "Comment details saved successfully";
            lblErrorLoadingDetailBody.Text = "Comment details saved successfully";
        }
        else
        {
            lblErrorcommentdtls.Text = "Comment details Not Saved CLICK To Know More";
            lblErrorLoadingDetailBody.Text = "Comment details Not Saved In DataBase";
        }
        chkCommentDtls.Checked = false;
        /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
        objComm.ManageApplicationLifeCycle(strApplicationno, objChangeObj.userLoginDetails._UserID, "UW_DECISION_COMMENT", true, ref intRef);
        /*added by shri on 28 dec 17 to update tracking*/
        UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
        /*END HERE*/
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        /*END HERE*/
    }
    protected void chkRiskParamIsSmoker_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int intRet = -1;
            objBuss = new BussLayer();
            string strUserId = string.Empty;
            objBuss.UpdateUWDecitionAdditionalInfoFlag(strApplicationno, chkSaralRiskIsSmoker.Checked, 3, ref intRet);
            if (intRet > 0)
            {
                objCommonObj = (CommonObject)Session["objCommonObj"];
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                strUserId = objCommonObj._Bpmuserdetails._UserID.ToUpper();
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                objBuss.InsertUserChangesLog(strApplicationno, (strUserId.IndexOf('F') == 0) ? strUserId.Substring(1) : strUserId, "SMOKER", Convert.ToString(chkRiskParamIsSmoker.Checked), ref intRet);
                lblErrorDetailsRiskParameter.Text = "Smoker Updated Successfully";
            }
            else
            {
                lblErrorDetailsRiskParameter.Text = "Smoker Not Updated Successfully";
            }
            //39.1 Begin of Changes; Sushant Devkate -MFL00905
            if (chkSaralRiskIsSmoker.Checked == false)
            {
                chkSaralRiskIsSmoker.Checked = false;
            }
            //39.1 End of Changes; Sushant Devkate -MFL00905
        }
        catch (Exception)
        {

            lblErrorDetailsRiskParameter.Text = "Try Again Later";
        }
    }
    protected void chkIsAadharVerified_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int intRet = -1;
            ManagePanAdharFlagUpdation(strApplicationno, chkIsAadharVerified.Checked, 2, ref intRet);
            ChangeThumbStatus(imgAadharVerified, chkIsAadharVerified.Checked);
        }
        catch (Exception)
        {
            lblErrorAadharHead.Text = lblErrorAadharBody.Text = "Try Again Later";
        }
    }
    /*ID:6 END*/
    /*ID:7 START*/
    private void ChangeNsapValue()
    {
        Commfun objCommfun = new Commfun();
        objBuss = new BussLayer();
        int intRef = -1;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID.ToUpper();
        objCommfun.ManageNsapFlag(strApplicationno, cbIsNsap.Checked, ref intRef);
        if (cbIsNsap.Checked)
        {
            FillNsaploadingDetails(strApplicationno, strChannelType);
            FillLoadingDetails(strApplicationno, strChannelType);
            lblErrorAppDetailsBody.Text = lblErrorappdtls.Text = "NSAP Loading added successfully";
        }
        else
        {
            RemoveNsapLoadingDetails(strApplicationno, strChannelType, ref response);
            if (response != "-1")
            {
                lblErrorAppDetailsBody.Text = lblErrorappdtls.Text = "NSAP Loading removed successfully";
            }
            else
            {
                lblErrorAppDetailsBody.Text = lblErrorappdtls.Text = "NSAP Loading not removed successfully";
            }
        }
        objBuss.InsertUserChangesLog(strApplicationno, (strUserId.IndexOf('F') == 0) ? strUserId.Substring(1) : strUserId, "NSAP", Convert.ToString(cbIsNsap.Checked), ref intRef);
        updLoadingDetails.Update();
    }
    /*ID:7 END*/
    /*ID:8 START*/
    private void IsBasePremiumDifferentChecker()
    {
        int intSumDifference = (Convert.ToInt32(string.IsNullOrEmpty(txtProdBranchBasePremium.Text) ? "0" : txtProdBranchBasePremium.Text) - Convert.ToInt32(string.IsNullOrEmpty(txtBasepremium.Text) ? "0" : txtBasepremium.Text));
        if (intSumDifference >= 200)
        {
            lblCommentNotificationPremiumChange.Text = "Base premium in saral is different from Base premium entered in branch by Rs. " + Convert.ToString(intSumDifference);
        }
        else
        {
            lblCommentNotificationPremiumChange.Text = string.Empty;
        }
    }
    /*ID:8 END*/
    /*ID:9 START*/
    private void BindTpaStatus(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            Logger.Info(strApplicationno + "STAG3:-Function Call BindTpaStatus" + System.Environment.NewLine);
            divTpaDetails.Attributes.Add("Class", "col-md-12");
            gvTpaDetailsTpaStatus.DataSource = dt;
        }
        else
        {
            gvTpaDetailsTpaStatus.DataSource = null;
            divTpaDetails.Attributes.Add("Class", "col-md-12 HideControl");
        }
        gvTpaDetailsTpaStatus.DataBind();
    }
    /*ID:9 END*/
    /*ID:10 START*/
    private void ManageDecisionType()
    {
        int intRet = -1;
        try
        {
            objBuss = new BussLayer();
            objBuss.UWDecisionManageDecisionDetails(FillDataTableDecisionType(), ref intRet);
        }
        catch (Exception)
        {

        }
    }

    private DataTable FillDataTableDecisionType()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ApplicationNo", typeof(string));
        dt.Columns.Add("DecisionType", typeof(int));
        dt.Columns.Add("UserId", typeof(string));
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        foreach (ListItem item in cblDecisionTypeDecisions.Items)
        {
            if (item.Selected)
            {
                DataRow dr = dt.NewRow();
                dr[0] = strApplicationno;
                dr[1] = item.Value;
                dr[2] = strUserId;
                dt.Rows.Add(dr);
            }
        }
        foreach (ListItem item in cblDecisionTypeIncompleteDocument.Items)
        {
            if (item.Selected)
            {
                DataRow dr = dt.NewRow();
                dr[0] = strApplicationno;
                dr[1] = item.Value;
                dr[2] = strUserId;
                dt.Rows.Add(dr);
            }
        }
        foreach (ListItem item in cblDecisionTypeCleanCase.Items)
        {
            if (item.Selected)
            {
                DataRow dr = dt.NewRow();
                dr[0] = strApplicationno;
                dr[1] = item.Value;
                dr[2] = strUserId;
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }

    private void BindMasterDetails(CheckBoxList ddl)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :BindMasterDetails" + System.Environment.NewLine);
        string strTableName = ddl.ID;
        if (strTableName == "ddlExstingLoadType")
        {
            strTableName = "ddlLoadType";
        }
        else if (strTableName == "ddlExistingLoadRsn1")
        {
            strTableName = "ddlLoadRsn1";
        }
        else if (strTableName == "ddlExistingLoadRsn2")
        {
            strTableName = "ddlLoadRsn2";
        }
        else if (strTableName == "ddlExistingLoadFlatMort")
        {
            strTableName = "ddlLoadFlatMortality";
        }
        else if (strTableName == "ddlCommonStatus")
        {
            strTableName = "ddlStatus";
        }
        /*ID:10 START*/
        else if (strTableName == "ddlApplicationDetailsProposalType")
        {
            strTableName = "ddlProposalType";
        }
        else if (strTableName == "cblDecisionTypeDecisions")
        {
            strTableName = "cblDecisionType";
        }
        else if (strTableName == "cblDecisionTypeIncompleteDocument")
        {
            strTableName = "ddlProposalType2";
        }
        else if (strTableName == "cblDecisionTypeCleanCase")
        {
            strTableName = "ddlProposalType3";
        }
        /*ID:10 END*/
        //Added by Suraj on 25 APRIL 2018
        else if (strTableName == "ddlAssigmentType")
        {
            strTableName = "ddlAssigmentType";
        }
        //END
        //Added by Suraj on 2 JULY 2018
        else if (strTableName == "ddlcountry")
        {
            strTableName = "ddlcountry";
        }
        DataSet _dsMaster = (DataSet)ViewState["_dsFollowuo"];
        ArrayList arr = new ArrayList();
        if (_dsMaster != null && _dsMaster.Tables.Count > 0)
        {
            if (_dsMaster.Tables[strTableName] != null && _dsMaster.Tables[strTableName].Rows.Count > 0)
            {

                ddl.DataSource = _dsMaster.Tables[strTableName];
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "VALUE";
                ddl.DataBind();
            }
        }
    }

    private void BindDecisionType(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cblDecisionTypeDecisions.Items.FindByValue(Convert.ToString(dt.Rows[i][1])) != null)
                {
                    cblDecisionTypeDecisions.Items.FindByValue(Convert.ToString(dt.Rows[i][1])).Selected = true;
                }
                else if (cblDecisionTypeIncompleteDocument.Items.FindByValue(Convert.ToString(dt.Rows[i][1])) != null)
                {
                    cblDecisionTypeIncompleteDocument.Items.FindByValue(Convert.ToString(dt.Rows[i][1])).Selected = true;
                }
                else if (cblDecisionTypeCleanCase.Items.FindByValue(Convert.ToString(dt.Rows[i][1])) != null)
                {
                    cblDecisionTypeCleanCase.Items.FindByValue(Convert.ToString(dt.Rows[i][1])).Selected = true;
                }
            }
        }
    }

    private void DisplaySaralWarningMessage()
    {
        WarningMessage objWarning = new WarningMessage();
        lblUwSaralWarningMessage.Text = objWarning.SetMessage(hdnWarningMessage.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries));
    }

    private void RemoveWarningMessage(string strIndex)
    {
        string strPipe = "|";
        hdnWarningMessage.Value = hdnWarningMessage.Value.Replace(strPipe + strIndex + strPipe, strPipe);
    }
    private void BindUwWarning(DataTable _dt)
    {
        if (_dt != null && _dt.Rows.Count > 0)
        {
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                FillWarningMessage(Convert.ToString(_dt.Rows[i]["Id"]));
            }
        }
        DisplaySaralWarningMessage();
    }

    private void FillWarningMessage(string strInput)
    {
        string strPipe = "|";
        hdnWarningMessage.Value = hdnWarningMessage.Value + strInput + strPipe;
    }

    private bool IsUserLimitLessThanSumAssured()
    {
        #region start 32.1: This CR-2639 changes done by Sushant Devkate MFL00905 

        decimal FSARofLA = objBuss.GetFSARofLA(strApplicationno);
        decimal Userlimit1 = Convert.ToDecimal(hdnUserLimit.Value);

        if (Convert.ToInt64(Userlimit1) < Convert.ToInt64(FSARofLA))
        {
            return true;
        }
        else
        {
            return false;
        }
        #endregion start 32.1: This CR-2639 changes done by Sushant Devkate MFL00905 
    }
    /*ID:10 END*/
    /*ID:17 END*/
    //Added by Suraj on 10 MAY 2018 for disabled assign type dropdown
    protected void ddlApplicationDetailsProposalType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlApplicationDetailsProposalType.SelectedValue != null || ddlApplicationDetailsProposalType.SelectedValue != "0")
        {
            if (ddlApplicationDetailsProposalType.SelectedValue.Equals("EMP"))
            {
                ddlAssigmentType.Attributes["class"] = ddlAssigmentType.Attributes["class"].ToString().Replace("lblLable", "");

            }
            else
            {
                ddlAssigmentType.Attributes.Add("class", "form-control lblLable");
                ddlAssigmentType.SelectedValue = "0";
            }
            if (ddlApplicationDetailsProposalType.SelectedValue.Equals("NRI"))
            {
                ddlcountry.Attributes["class"] = ddlcountry.Attributes["class"].ToString().Replace("lblLable", "");

            }
            else
            {
                ddlcountry.Attributes.Add("class", "form-control lblLable");
                ddlcountry.SelectedValue = "0";
            }
        }

    }
    /*ID:17 END*/

    /*ID:12 START*/
    protected void btnSaveAnswer_Click(object sender, EventArgs e)
    {
        if (Session["objCommonObj"] != null)
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];

        }
        //GridView grd = (GridView)FindControl("grdclosefilerw");
        int QueId = 1;
        //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
        DataTable _dt = new DataTable();
        int resp = 0;
        _dt.Columns.Add("AppNo", typeof(string));
        _dt.Columns.Add("QuesId", typeof(int)); //name of the column
        _dt.Columns.Add("Answer", typeof(int));//name of the column
        _dt.Columns.Add("UserId", typeof(string));
        _dt.Columns.Add("CreatedDate", typeof(DateTime));
        //_dt.Columns.Add("Remark", typeof(string));
        foreach (GridViewRow row in grdclosefilerw.Rows)
        {
            QueId = row.RowIndex + 1;
            DropDownList ddl = (DropDownList)row.FindControl("ddlAnswer");
            TextBox txtCloseFileRemark = (TextBox)row.FindControl("txtCloseFileRemark");
            if (ddl.SelectedValue != "")
            {
                int selectedValue = Convert.ToInt32(ddl.SelectedValue);
                if (selectedValue != 0)
                {
                    DataRow dr = _dt.NewRow();
                    dr["AppNo"] = Convert.ToString(txtAppno.Text);
                    dr["QuesId"] = QueId;
                    dr["Answer"] = selectedValue; //row.Cells[1].Text;
                    dr["UserId"] = Convert.ToString(objCommonObj._Bpmuserdetails._UserID);
                    //dr["Remark"] = txtCloseFileRemark.Text;
                    _dt.Rows.Add(dr);
                }
                else
                {
                    //lblerrorclosefile.Text = "";
                }
            }

        }
        objBuss.SaveAnswer_CloseFile(_dt, txtcloseRemark.Text.Trim(), Convert.ToInt32(ddlCaseCategory.SelectedValue), ref resp);
        if (resp > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SHOW SUCCESS MESSAGE AND CLOSE PAGE", "window.close();alert('Details save successfully');", true);
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Details save successfully');window.close();", true);

        }


    }
    /*ID:12 END*/
    /*ID:13 START*/
    protected void BindQuestion_CloseFile()
    {
        objBuss = new BussLayer();
        objBuss.BindQuestion_CloseFile(ref _dsQuestion);
        if (_dsQuestion.Tables.Count > 0)
        {
            if (_dsQuestion.Tables[0].Rows.Count > 0)
            {
                grdclosefilerw.DataSource = _dsQuestion.Tables[0];
                grdclosefilerw.DataBind();
            }
        }
        /*
        string api = ConfigurationManager.AppSettings["CFRQuestAPI"].ToString();
        string inputJson = string.Empty; string result = string.Empty;
        try
        {
            //inputJson = (new JavaScriptSerializer()).Serialize(;
            //inputJson = JsonConvert.SerializeObject(objNeedParam);
            var client = new RestClient(api);
            //client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", inputJson, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var result1 = response.Content.ToString();
            DataTable dt = new DataTable();
            dt = JsonConvert.DeserializeObject<DataTable>(result1);
        }
        catch (Exception ex)
        {
            //da.SISExceptionLog(name.Text, "", "SaveNeedData()", ex.ToString());
        }
        */
    }
    /*ID:13 END*/
    /*ID:14 START*/
    protected void grdclosefilerw_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlAnswer = (DropDownList)e.Row.FindControl("ddlAnswer");

            BindMasterDetails(ddlAnswer);
        }
    }
    /*ID:14 END*/
    /*ID:15 START*/
    private void DisableControl_CloseFileReview()
    {
        if (Convert.ToString(strClickBucketLink) == "Close_File_Review")
        {
            divCloseFileReview.Visible = true;
            chkAppDtls.Enabled = false;
            chkAgentDtls.Enabled = false;
            chkJournal.Enabled = false;
            chkBankDtls.Enabled = false;
            chkMandate.Enabled = false;
            chkPanDtls.Enabled = false;
            CheckBox3.Enabled = false;
            CheckBox2.Enabled = false;
            CheckBox1.Enabled = false;
            chkProductDtls.Enabled = false;
            chkFunddtlsSave.Enabled = false;
            chkReqDtls.Enabled = false;
            chkLoadingDtls.Enabled = false;
            chkCommentDtls.Enabled = false;
            btnUWCommSave.Enabled = false;
            //btnCloseFileSaveAnswer.Enabled = false;
            ddlUWDecesion.Enabled = false;
            gvExtLoadDetails.Enabled = false;
            Master.FindControl("btnPost").Visible = false;
            Master.FindControl("btnReview").Visible = false;
            Master.FindControl("btnReferToCmo").Visible = false;

        }
        else
        {
            divCloseFileReview.Visible = false;
            chkAppDtls.Enabled = true;
            chkAgentDtls.Enabled = true;
            chkJournal.Enabled = true;
            chkBankDtls.Enabled = true;
            chkMandate.Enabled = true;
            chkPanDtls.Enabled = true;
            CheckBox3.Enabled = true;
            CheckBox2.Enabled = true;
            CheckBox1.Enabled = true;
            chkProductDtls.Enabled = true;
            chkFunddtlsSave.Enabled = true;
            chkReqDtls.Enabled = true;
            chkLoadingDtls.Enabled = true;
            chkCommentDtls.Enabled = true;
            btnUWCommSave.Enabled = true;
            //btnCloseFileSaveAnswer.Enabled = true;
            ddlUWDecesion.Enabled = true;
            gvExtLoadDetails.Enabled = true;
            Master.FindControl("btnPost").Visible = true;
            Master.FindControl("btnReview").Visible = true;
            Master.FindControl("btnReferToCmo").Visible = true;
        }

    }
    /*ID:15 END*/
    /*ID:16 START*/
    private void ManagePolicyPrinting()
    {
        if (ddlUWDecesion.SelectedValue == "Approved")
        {
            int intResp = -1;
            if (ddlDecisionDetailsIsPolicyPrintingToHolding.SelectedValue == "True")
            {
                if (Session["objLoginObj"] != null)
                {
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                }
                objBuss.ManagePolicyPrinting(strApplicationno, "UW_POLICY_HOLD", Convert.ToBoolean(ddlDecisionDetailsIsPolicyPrintingToHolding.SelectedValue), objChangeObj.userLoginDetails._UserID, "THA", objChangeObj.userLoginDetails._UserID.Substring(1), "THA", "UnderWriting", "Saral UnderWriting", "INSERT", ref intResp);
            }
        }
    }
    /*ID:16 End*/
    private void Binddistance(DataTable dt)
    {
        Label5.Text = string.Empty;
        if (dt != null && dt.Rows.Count > 0 && (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["Distance1"])) || !string.IsNullOrWhiteSpace(Convert.ToString(dt.Rows[0]["Distance1"]))))
        {
            txtdistance.Text = Convert.ToString(dt.Rows[0]["Distance1"]);

            /*ID:9 END*/
        }
        else
        {
            Label5.Text = "Distance not available, as branch code is not found";
        }


        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    txtdistance.Text = Convert.ToString(dt.Rows[0]["Distance1"]);


        //    /*ID:9 END*/
        //}
    }

    private void DeleteExistingLoading(string strAppno, string strRiderId, string strLoadingType, ref int response)
    {
        Commfun objCommfun = new Commfun();
        objCommfun.DeleteExistingLoading1(strAppno, strRiderId, strLoadingType, ref response);
    }

    private void BindUwHint(DataTable _dt)
    {
        if (_dt != null && _dt.Rows.Count > 0)
        {
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                FillHintMessage(Convert.ToString(_dt.Rows[i]["Id"]));
            }
        }
        DisplaySaralHintMessage();
    }

    private void FillHintMessage(string strInput)
    {
        string strPipe = "|";
        hdnHintMessage.Value = hdnHintMessage.Value + strInput + strPipe;
    }

    private void DisplaySaralHintMessage()
    {
        WarningMessage objWarning = new WarningMessage();
        lblUwSaralHintMessage.Text = objWarning.SetMessage(hdnHintMessage.Value.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries));
    }
    protected void btnSearchCode_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('REQ_SearchCode.aspx','mywindow','menubar=1,resizable=1,width=900,height=600');", true);
    }
    protected void btnUpadteBMI_Click(object sender, EventArgs e)
    {
        Label5.Text = string.Empty;
        if (!txtHeight.Text.All(c => Char.IsNumber(c)) || !txtWeight.Text.All(c => Char.IsNumber(c)))
        {
            Label5.Text = "Please enter numeric only";
            return;
        }
        else
        {
            try
            {
                DataSet _dsbmiData = new DataSet();
                objcomm.CalculateBMI(ref _dsbmiData, strApplicationno, strChannelType, txtHeight.Text.Trim(), txtWeight.Text.Trim());
                if (_dsbmiData.Tables.Count > 0 && _dsbmiData.Tables[0].Rows.Count > 0 && _dsbmiData.Tables[0].Rows[0]["DATA"] != null)
                {
                    txtSaralRiskBmi.Text = Convert.ToString(_dsbmiData.Tables[0].Rows[0]["DATA"]);
                    Label5.Text = Convert.ToString("BMI updated successfully");
                }
                upSaralRiskParameter.Update();
            }
            catch (Exception)
            {
                Label5.Text = Convert.ToString("Please try again later");
            }
        }

    }

    //ADDED BY AJAY SAHU
    //public void BindPreviousPolictDetailsSaral(DataTable _dsPrev)
    //{
    //    //_dsPrev.Columns.Remove("stat");
    //    //_dsPrev.Columns.Remove("ageProofSubmitted");
    //    //_dsPrev.Columns.Remove("idProofSubmitted");
    //    //_dsPrev.Columns.Remove("addressProofSubmitted");
    //    //_dsPrev.Columns.Remove("incomeProofSubmitted");
    //    //_dsPrev.Columns.Remove("underwriterComments");
    //    if (_dsPrev.Rows.Count > 0)
    //    {
    //        divPrePolicySaral.Visible = true;
    //        gridPrevPolSaral.DataSource = _dsPrev;
    //        gridPrevPolSaral.DataBind();

    //    }
    //    else
    //    {
    //        divPrePolicySaral.Visible = false;
    //        lblErrorPrevpolSaral.Text = "No Previous Policy Details found";
    //    }
    //}

    //public void BindTsarMsarDetailsSaral(DataTable _dsTsarMsarDtls)
    //{
    //    if (_dsTsarMsarDtls.Rows.Count > 0)
    //    {
    //        GridMsarTsarSaral.DataSource = _dsTsarMsarDtls;
    //        GridMsarTsarSaral.DataBind();
    //    }

    //}

    //END

    //protected void btnMedDataentry_Click(object sender, EventArgs e)
    //{
    //    // ClientScript.RegisterStartupScript(Page.GetType(), "", "window.open('Appcode/MedicalDataEntryNew.aspx');", true);
    //    ///Response.Redirect(string.Format("MedicalDataEntryNew.aspx?qsAppNo={0}&qsPolicyNo={1}", strApplicationno, strPolicyNo),false);
    //    Response.Redirect("MedicalDataEntryNew.aspx?qsAppNo="+ strApplicationno + "&qsPolicyNo="+ strPolicyNo + "");
    //}

    protected void btnMedDataentry_Click1(object sender, EventArgs e)
    {
        //string pageurl = "MedicalDataEntryNew.aspx?qsAppNo=" + strApplicationno + "&qsPolicyNo="+ strPolicyNo;
        //Response.Write(String.Format("window.open('{0}','_blank')", ResolveUrl(pageurl)));

        //Response.Write("< script > window.open( 'MedicalDataEntryNew.aspx', '_blank'); </ script >");
        //Response.End();
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "openlink", "window.open('" + url + "');   window.location.href = '~/Appcode/Uwdecision.aspx'", true);
        //Response.Redirect(string.Format("~/Appcode/MedicalDataEntryNew.aspx?qsAppNo={0}&qsPolicyNo={1}", strApplicationno, strPolicyNo), false);
        //btnMedDataentry.Attributes.Add("OnClick", "OpenPage('" + url + "');");
        string url = "MedicalDataEntry.aspx?qsAppNo=" + strApplicationno + "&qsPolicyNo=" + strPolicyNo;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        //Response.Redirect("MedicalDataEntryNew.aspx");
    }

    /*ID:24 START*/
    protected void btnPlvcVideo_Click(object sender, EventArgs e)
    {
        string url = ConfigurationManager.AppSettings["PlvcVideo"];
        url = url + UWSaralDecision.CommFun.Base64EncodingMethod(txtAppno.Text);
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
    }
    /*ID:24 END*/
    protected void BindVideoMER()
    {
        try
        {
            string Sourcepath = ConfigurationManager.AppSettings["TPASourcePath"];
            /*
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Sourcepath);
            request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("AUX_FTP", "password@1");
            request.KeepAlive = false;
            request.UseBinary = true;
            request.UsePassive = true;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            */
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Sourcepath);
            int count = dir.GetFiles().Length;
            //DateTime dt = File.GetLastWriteTime(Sourcepath);
            List<string> files = DirSearch(Sourcepath);
            string StrFileName = string.Empty;
            DataTable dt = new DataTable();
            dt.Columns.Add("Application", typeof(string));
            dt.Columns.Add("Date", typeof(DateTime));
            //table.Columns.Add("remainingUpload", typeof(string));
            if (Directory.Exists(Sourcepath))
            {
                for (int i = 0; i < files.Count; i++)
                {
                    DateTime date = File.GetLastWriteTime(files[i]);
                    StrFileName = System.IO.Path.GetFileName(files[i]);
                    dt.Rows.Add();
                    dt.Rows[i]["Application"] = StrFileName;
                    dt.Rows[i]["Date"] = date;

                }
                if (StrFileName.Contains(strApplicationno))
                {
                    gvVideoMer.DataSource = dt;
                    gvVideoMer.DataBind();
                }
                else
                {
                    gvVideoMer.DataSource = null;
                    gvVideoMer.DataBind();
                    lblVideoMER.Text = "VideoMER is not available.";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;//Logger.Info(strApplicationno + "STAG3:-Function Call BINDVIDEOMER" + " : " + ex + System.Environment.NewLine);
        }
    }
    private List<String> DirSearch(string sDir)
    {
        List<String> files = new List<String>();
        try
        {
            foreach (string f in Directory.GetFiles(sDir))
            {
                files.Add(f);
            }
            foreach (string d in Directory.GetDirectories(sDir))
            {
                files.AddRange(DirSearch(d));
            }
        }
        catch (System.Exception)
        {
            //MessageBox.Show(excpt.Message);
        }

        return files;
    }

    protected void gvVideoMer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            //Determine the RowIndex of the Row whose LinkButton was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            string Sourcepath = ConfigurationManager.AppSettings["TPASourcePath"];
            GridViewRow row = gvVideoMer.Rows[rowIndex];
            string name = (row.FindControl("lnlApp") as LinkButton).Text;
            //LinkButton lnkname = row.FindControl("lnlApp") as LinkButton;
            //lnkname.Attributes.Add("href", "TESTPAGE.aspx");
            //lnkname.Attributes.Add("target", "_blank");
            string file = Sourcepath + "\\" + name;
            string url = UWSaralDecision.CommFun.Base64EncodingMethod(file);
            //Response.Redirect(string.Format("Appcode/result.aspx?path={0}", file), false);
            //string url = "default3.aspx";
            //Response.Write("<script type='text/javascript'>window.open('" + file + "');</script>");
            //Response.Write("<script>");
            //Response.Write("window.open('Appcode/result.aspx','_blank')");
            //Response.Write("</script>");
            //string s = "window.open(' http://www.stackoverflow.com ', 'popup_window', return false;);";

            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //Reference the GridView Row.

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../Appcode/result.aspx?path=" + url + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            //Response.Redirect("/Appcode/result.aspx?path = " + file);
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'http://www.stackoverflow.com', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        }
    }
    protected void BindMedicalDELink(DataTable _dt)
    {
        if (_dt != null && _dt.Rows.Count > 0)
        {
            btnMedDataentry.BackColor = System.Drawing.Color.Green;
            btnMedDataentry.ForeColor = System.Drawing.Color.White;
            btnMedDataentry.Font.Bold = true;
        }

    }

    //protected void btnYes_Click(object sender, EventArgs e)
    //{
    //    strLApushErrorCode = -1;
    //    Master.masterCallEvent += new EventHandler(btnServerValidation);
    //}

    protected void OfacService()
    {
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "OFAC", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
        if (strLApushErrorCode == 0)
        {
            FillWarningMessage("66");
            DisplaySaralWarningMessage();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopupOFAC('This is OFAC case, Please confirm whether you have to proceed further or not!!');", true);
            //throw new Exception("UDE-Please confirm, Are you sure you want to continue?");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "ShowModalPopup('" + alertMessage + "');", true);
        }
    }
    //added by rupali
    public void getFGData()
    {

        #region Start 33.1: This CR-2809 changes done by Sushant Devkate MFL00905 
        string strEnvironment = System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString();


        IIBQueryDataDO ObjIIBQueryDataDOForDE = objComm.getFGDataShowDE_new(strApplicationno, "DE");

        IIBQueryDataDO ObjIIBQueryDataDOUW = objComm.getFGDataShow_Impact_New(strApplicationno, "UW");

        btnFetchIIBQuery.Enabled = true;

        if (ObjIIBQueryDataDOUW.ObjListOFQueryFG.Count > 0 || ObjIIBQueryDataDOUW.ObjListOFQueryOther.Count > 0)
        {
            BindGridforIIB(ObjIIBQueryDataDOUW, 1);
        }
        else if (ObjIIBQueryDataDOForDE.ObjListOFQueryFG.Count > 0 || ObjIIBQueryDataDOForDE.ObjListOFQueryOther.Count > 0)
        {
            BindGridforIIB(ObjIIBQueryDataDOForDE, 2);

            if (ObjIIBQueryDataDOForDE.ObjListOFQueryFG != null && ObjIIBQueryDataDOForDE.ObjListOFQueryOther != null)
            {
                if (ObjIIBQueryDataDOForDE.ObjListOFQueryFG.Count > 0 || ObjIIBQueryDataDOForDE.ObjListOFQueryOther.Count > 0)
                {
                    btnFetchIIBQuery.Enabled = strEnvironment == "UAT" ? true : false;
                }
            }
        }


        #endregion End 33.1: This CR-2809 changes done by Sushant Devkate MFL00905 

    }
    protected void btnFetchIIBQuery_Click(object sender, EventArgs e)
    {
        #region New code
        //Begin of changes ; by Sushant Devkate -MFL00905
        bool IsAPICAll = Convert.ToBoolean(ConfigurationManager.AppSettings["IsIIBMatchAPICall"].ToString());
        if (IsAPICAll)
        {
            //End of changes ; by Sushant Devkate -MFL00905
            string PageName = "Uwdecision.aspx.cs";
            try
            {
                objComm.IIBLogtable(txtAppno.Text, "btnFetchIIBQuery_Click_" + PageName, "", "", "", "", "", "UW", "", "", "IIB query button clicked on UW Decision page");

                List<CoustomerDO> ObjlistCoustomerDO = new List<CoustomerDO>();
                DataSet dtOut = new DataSet();
                ObjlistCoustomerDO = objComm.GetIIBPolicyHolderData(txtAppno.Text);

                if (ObjlistCoustomerDO != null)
                {
                    if (ObjlistCoustomerDO.Count > 0)
                    {
                        ObjlistCoustomerDO = ObjlistCoustomerDO.Distinct().ToList();

                        for (var i = 0; i < ObjlistCoustomerDO.Count; i++)
                        {
                            dtOut = new IIBQueryBLL().GetIIBMatchAPIResponse(ObjlistCoustomerDO[i].ClientType, "GetIIBMatchAPIResponse", ObjlistCoustomerDO[i].FirstName, ObjlistCoustomerDO[i].SurName, ObjlistCoustomerDO[i].DOB, ObjlistCoustomerDO[i].Gender,
                                                           ObjlistCoustomerDO[i].PinCode, "", ObjlistCoustomerDO[i].PanNo, ObjlistCoustomerDO[i].Address,
                                                           ObjlistCoustomerDO[i].Address, ObjlistCoustomerDO[i].MobileNo, ObjlistCoustomerDO[i].PhoneNumber,
                                                           ObjlistCoustomerDO[i].Email, ObjlistCoustomerDO[i].ProductCode,
                                                           ObjlistCoustomerDO[i].AnnualIncome, txtAppno.Text, PageName);

                        }
                    }
                }



                string strEnvironment = System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString();

                IIBQueryDataDO ObjIIBQueryDataDO = objComm.getFGDataShow_Impact_New(strApplicationno, "UW");

                if (ObjIIBQueryDataDO.ObjListOFQueryFG.Count > 0 || ObjIIBQueryDataDO.ObjListOFQueryOther.Count > 0)
                {
                    BindGridforIIB(ObjIIBQueryDataDO, 1);
                }

            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('" + ex.Message + "');hideloading();</script>", false);
                objComm.IIBLogtable(txtAppno.Text, "btnFetchIIBQuery_Click_" + PageName, "", "", "Failed", "", "", "UW", "", ex.ToString(), "Failed");
            }
            //ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading();</script>", false);
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('Click ok to view complete details');hideloading();</script>", false);
          
        }
        //Begin of changes ; by Sushant Devkate -MFL00905
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading();</script>", false);
        }
        //End of changes ; by Sushant Devkate -MFL00905
        #endregion
    }

    //End Changes to rupali ezytek

    protected void BindMatchingpolicydetailsgridview()
    {
        //string appno = Convert.ToString(txtAppno.Text);

        //DataSet ds = objcomm.GetMatchingpolicydetails(appno);
        //if (ds.Tables.Count > 0)
        //{
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        dvMatchedPolicyDetails.Visible = true;
        //        MatchedPolicyDetails.DataSource = ds.Tables[0];
        //        MatchedPolicyDetails.DataBind();
        //    }



        //}
        //else
        //{
        //    dvMatchedPolicyDetails.Visible = false;
        //}

    }

    #region ENYScoreData
    //27.1 Start ENY Score  Shyam Patil
    private void ENYScore(string ApplicationNo)
    {
        try
        {
            DataSet ds = new DataSet();
            objcomm.GetENYDeatils(ref ds, ApplicationNo, "Normal");
            //  string ENYScore = CallENYScore();
            txtENYScore.Text = "";
        }
        catch (Exception)
        {

        }

    }



    private HttpResponseMessage ApiPost1(string apiUrl, string Method, string inputJson, string Authorization)
    {
        try
        {
            //LogRequest(apiUrl, inputJson, "ApiPost", Method, string.Empty, string.Empty, "I");
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings["TOKENSubscriptionKey"].ToString());
            client.DefaultRequestHeaders.Add("Authorization", Authorization);

            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            return client.PostAsync(apiUrl + Method, inputContent).Result;
        }
        catch (Exception)
        {
            // LogRequest(ex.Message.ToString(), ex.ToString(), "ApiPost", Method, string.Empty, string.Empty, "E");
            return null;
        }
    }
    //27.1 End ENY Score  Shyam Patil
    #endregion

    // Added by Brijesh
    protected void ddlNAWPCatagory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNAWPCatagory.SelectedValue == "staff")
        {
            hd_que_2.Checked = true;
            divRelationwithStaff.Visible = true;
        }
        else
        {
            hd_que_2.Checked = false;
            divRelationwithStaff.Visible = false;
            ddlRelationStaff.SelectedValue = "0";
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedValue == "staff")
        {
            hd_que_2.Checked = true;
            divRelationwithStaff.Visible = true;
        }
        else
        {
            hd_que_2.Checked = false;
            divRelationwithStaff.Visible = false;
            ddlRelationStaff.SelectedValue = "0";
        }
    }
    // end

    #region CR - 29431
    //30.1 Begin of Changes; Pooja Shetye - [1133038]
    public bool CheckFlaggingofPANandForm60(string panno, bool copyofpan, bool copyofform60)
    {
        bool result = true; bool IsvalidPAN = true;

        if ((copyofpan == true && copyofform60 == false) || (copyofpan == false && copyofform60 == true))
        {
            result = true;
        }

        if (result == true)
        {
            if (copyofpan == true)
            {
                if (panno.Trim() == "")
                {
                    IsvalidPAN = false;
                }
                else { IsvalidPAN = true; }
            }


            if (copyofform60 == true)
            {
                if (panno.Trim() == "")
                {
                    IsvalidPAN = true;
                }
                else { IsvalidPAN = false; }
            }
        }
        return IsvalidPAN;
    }

    public bool ValidatePANNumber(string PanNo, string ClientType)
    {
        bool IsvalidPAN = true;
        PanNo = PanNo.Trim();
        if (PanNo != "")
        {
            char c = PanNo[3];

            if (ClientType.ToUpper() == "C")
            {
                string[] digit = new string[] { "H", "A", "F", "C", "T", "B", "L", "J", "G" };
                bool isdigitvalidated = false;
                foreach (string s in digit)
                {
                    if (c.ToString().ToUpper().StartsWith(s))
                    {
                        isdigitvalidated = true;
                    }
                }
                if (isdigitvalidated == true)
                {
                    IsvalidPAN = true;
                }
                else { IsvalidPAN = false; }
            }
            else if (ClientType.ToUpper() == "P")
            {
                if (c.ToString().ToUpper() == ClientType.ToUpper())
                {
                    IsvalidPAN = true;

                }
                else
                {
                    IsvalidPAN = false;

                }
            }


        }
        return IsvalidPAN;
    }

    //30.1 End of Changes; Pooja Shetye - [1133038]
    #endregion
    #region 35.1 Begin of changes  CR-30489 kavita mfl00255


    #region To save IIB gridview Data
    public void SaveIIBImpact(string AppNo)
    {
        try
        {
            DataTable dt12 = new DataTable();
            dt12.Columns.Add("Input_Proposal_Policy_No");
            dt12.Columns.Add("QuestDBNo");
            dt12.Columns.Add("Input_Matching_Parameter");
            dt12.Columns.Add("Quest_DoP_DoC");
            dt12.Columns.Add("Quest_Sum_Assured");
            dt12.Columns["Quest_Sum_Assured"].DataType = typeof(double);  //change for sum assured formation
            dt12.Columns.Add("Quest_Policy_Status");
            dt12.Columns.Add("Quest_Date_of_Exit");
            dt12.Columns.Add("Quest_Date_of_Death");
            dt12.Columns.Add("Quest_Cause_of_Death");
            dt12.Columns.Add("Quest_Record_last_updated");
            dt12.Columns.Add("Quest_Entity_Caution_Status");
            dt12.Columns.Add("Quest_Intermediary_Caution_Status");
            dt12.Columns.Add("Quest_Company_Number");
            dt12.Columns.Add("Is_Negative_Match");
            dt12.Columns.Add("LAProposerPayor");
            dt12.Columns.Add("Status");
            dt12.Columns.Add("CreatedBy");
            dt12.Columns.Add("IIBServiceResponse");
            dt12.Columns.Add("Impact");
            //IIBServiceResponse
            //Added by kavita new column in IIB Service changes
            dt12.Columns.Add("Product_Type");
            dt12.Columns.Add("Linked_Non_linked");
            dt12.Columns.Add("Medical_Non_Medical");
            dt12.Columns.Add("Whether_Standard_Life");
            dt12.Columns.Add("Reason_for_Decline");
            dt12.Columns.Add("Reason_for_Postpone");
            dt12.Columns.Add("Reason_for_Repudiation");
            //Addded by Sushant
            #region Start 33.1: This CR-2809 changes done by Sushant Devkate MFL00905 
            dt12.Columns.Add("Client_Type");
            dt12.Columns.Add("RolesType");
            dt12.Columns.Add("Type");

            DataTable LAdt = new DataTable();
            DataTable Proposaldt = new DataTable();
            DataTable Payerdt = new DataTable();

            LAdt = dt12.Copy();
            Proposaldt = dt12.Copy();
            Payerdt = dt12.Copy();

            #region FGIL Grid
            #region LA Griview Bind
            foreach (GridViewRow row in GridViewLA.Rows)
            {
                Label lblQuestDBNo1 = (Label)row.FindControl("lblQuestDBNo1");
                Label lblMatchingPara = (Label)row.FindControl("lblMatchingPara");
                Label lblDoC = (Label)row.FindControl("lblDoC");
                Label lblsa = (Label)row.FindControl("lblsa");
                Label lblPolStatus = (Label)row.FindControl("lblPolStatus");
                Label lblDateofExit = (Label)row.FindControl("lblDateofExit");
                Label lblDateofDeath = (Label)row.FindControl("lblDateofDeath");
                Label lblupdateddate = (Label)row.FindControl("lblupdateddate");
                Label lblCautionStatus = (Label)row.FindControl("lblCautionStatus");
                Label lblInterMedCautionStatus = (Label)row.FindControl("lblInterMedCautionStatus");
                Label lblCompany = (Label)row.FindControl("lblCompany");
                Label lblNegative = (Label)row.FindControl("lblNegative");
                Label lblLAProposerPayor = (Label)row.FindControl("lblLAProposerPayor");
                HiddenField hdnLAClientType = (HiddenField)row.FindControl("hdnLACliectType");
                Label lblLARolesType = (Label)row.FindControl("lblLARolesType");

                string QuestDBNo = lblQuestDBNo1.Text;
                string Matching_Parameter = lblMatchingPara.Text;
                string DoP_DoC = lblDoC.Text;
                string Sum_Assured = lblsa.Text;//.Split('.')[0].ToString();
                string Policy_Status = lblPolStatus.Text;
                string Date_of_Exit = lblDateofExit.Text;
                string Date_of_Death = lblDateofDeath.Text;
                string Cause_of_Death = lblCautionStatus.Text;
                string Record_last_updated = lblupdateddate.Text;
                string Entity_Caution_Status = lblCautionStatus.Text;
                string Intermediary_Caution_Status = lblInterMedCautionStatus.Text;
                string Company_Number = lblCompany.Text;
                string Negative_Match = lblNegative.Text;
                string LAProposerPayor = lblLAProposerPayor.Text;
                string LARolesType = lblLARolesType.Text;
                string LAClientType = hdnLAClientType.Value;

                DropDownList ddl = (DropDownList)row.FindControl("ddlimpact");
                string Impact = ddl.SelectedValue.ToString();

                DataRow LAdr = LAdt.NewRow();
                LAdr["Input_Proposal_Policy_No"] = AppNo;
                LAdr["QuestDBNo"] = QuestDBNo;
                LAdr["Input_Matching_Parameter"] = Matching_Parameter;
                LAdr["Quest_DoP_DoC"] = DoP_DoC;
                LAdr["Quest_Sum_Assured"] = Convert.ToDecimal(Sum_Assured);
                LAdr["Quest_Policy_Status"] = Policy_Status;
                LAdr["Quest_Date_of_Exit"] = Date_of_Exit;
                LAdr["Quest_Date_of_Death"] = Date_of_Death;
                LAdr["Quest_Cause_of_Death"] = Cause_of_Death;
                LAdr["Quest_Record_last_updated"] = Record_last_updated;
                LAdr["Quest_Entity_Caution_Status"] = Entity_Caution_Status;
                LAdr["Quest_Intermediary_Caution_Status"] = Intermediary_Caution_Status;
                LAdr["Quest_Company_Number"] = Company_Number;
                LAdr["Is_Negative_Match"] = Negative_Match;
                LAdr["LAProposerPayor"] = LAProposerPayor;
                LAdr["Status"] = "Active";
                LAdr["CreatedBy"] = "UW";
                LAdr["IIBServiceResponse"] = "Success";
                LAdr["Impact"] = Impact;
                //Added by kavita new column in IIB Service changes
                LAdr["Product_Type"] = string.Empty;
                LAdr["Linked_Non_linked"] = string.Empty;
                LAdr["Medical_Non_Medical"] = string.Empty;
                LAdr["Whether_Standard_Life"] = string.Empty;
                LAdr["Reason_for_Decline"] = string.Empty;
                LAdr["Reason_for_Postpone"] = string.Empty;
                LAdr["Reason_for_Repudiation"] = string.Empty;

                //Added by Sushant 
                LAdr["Client_Type"] = (!string.IsNullOrEmpty(LAClientType)) ? LAClientType : "LA";
                LAdr["RolesType"] = (!string.IsNullOrEmpty(LARolesType)) ? LARolesType : new Commfun().GetPreviousPolicyClientType(QuestDBNo, string.Empty);
                LAdr["Type"] = "FG";
                LAdt.Rows.Add(LAdr);
            }

            #endregion

            #region Proposal Griview Bind
            foreach (GridViewRow row in GridViewProposer.Rows)
            {
                Label lblQuestDBNo1 = (Label)row.FindControl("lblQuestDBNo1");
                Label lblMatchingPara = (Label)row.FindControl("lblMatchingPara");
                Label lblDoC = (Label)row.FindControl("lblDoC");
                Label lblsa = (Label)row.FindControl("lblsa");
                Label lblPolStatus = (Label)row.FindControl("lblPolStatus");
                Label lblDateofExit = (Label)row.FindControl("lblDateofExit");
                Label lblDateofDeath = (Label)row.FindControl("lblDateofDeath");
                Label lblupdateddate = (Label)row.FindControl("lblupdateddate");
                Label lblCautionStatus = (Label)row.FindControl("lblCautionStatus");
                Label lblInterMedCautionStatus = (Label)row.FindControl("lblInterMedCautionStatus");
                Label lblCompany = (Label)row.FindControl("lblCompany");
                Label lblNegative = (Label)row.FindControl("lblNegative");
                Label lblLAProposerPayor = (Label)row.FindControl("lblLAProposerPayor");
                Label lblProposerRoles = (Label)row.FindControl("lblProposerRoles");
                HiddenField hdnProposerClientType = (HiddenField)row.FindControl("hdnProposerClientType");


                string QuestDBNo = lblQuestDBNo1.Text;
                string Matching_Parameter = lblMatchingPara.Text;
                string DoP_DoC = lblDoC.Text;
                string Sum_Assured = lblsa.Text;//.Split('.')[0].ToString();
                string Policy_Status = lblPolStatus.Text;
                string Date_of_Exit = lblDateofExit.Text;
                string Date_of_Death = lblDateofDeath.Text;
                string Cause_of_Death = lblCautionStatus.Text;
                string Record_last_updated = lblupdateddate.Text;
                string Entity_Caution_Status = lblCautionStatus.Text;
                string Intermediary_Caution_Status = lblInterMedCautionStatus.Text;
                string Company_Number = lblCompany.Text;
                string Negative_Match = lblNegative.Text;
                string LAProposerPayor = lblLAProposerPayor.Text;
                string ProposerRoles = lblProposerRoles.Text;
                string ProposerClientType = hdnProposerClientType.Value;
                DropDownList ddl = (DropDownList)row.FindControl("ddlimpact");
                string Impact = ddl.SelectedValue.ToString();
                // string Impact = row.Cells[14].Text;
                //string CreatedBy = "UW";

                DataRow Proposaldr = Proposaldt.NewRow();
                Proposaldr["Input_Proposal_Policy_No"] = AppNo;
                Proposaldr["QuestDBNo"] = QuestDBNo;
                Proposaldr["Input_Matching_Parameter"] = Matching_Parameter;
                Proposaldr["Quest_DoP_DoC"] = DoP_DoC;
                Proposaldr["Quest_Sum_Assured"] = Convert.ToDecimal(Sum_Assured);
                Proposaldr["Quest_Policy_Status"] = Policy_Status;
                Proposaldr["Quest_Date_of_Exit"] = Date_of_Exit;
                Proposaldr["Quest_Date_of_Death"] = Date_of_Death;
                Proposaldr["Quest_Cause_of_Death"] = Cause_of_Death;
                Proposaldr["Quest_Record_last_updated"] = Record_last_updated;
                Proposaldr["Quest_Entity_Caution_Status"] = Entity_Caution_Status;
                Proposaldr["Quest_Intermediary_Caution_Status"] = Intermediary_Caution_Status;
                Proposaldr["Quest_Company_Number"] = Company_Number;
                Proposaldr["Is_Negative_Match"] = Negative_Match;
                Proposaldr["LAProposerPayor"] = LAProposerPayor;
                Proposaldr["Status"] = "Active";
                Proposaldr["CreatedBy"] = "UW";
                Proposaldr["IIBServiceResponse"] = "Success";
                Proposaldr["Impact"] = Impact;
                //Added by kavita new column in IIB Service changes
                Proposaldr["Product_Type"] = string.Empty;
                Proposaldr["Linked_Non_linked"] = string.Empty;
                Proposaldr["Medical_Non_Medical"] = string.Empty;
                Proposaldr["Whether_Standard_Life"] = string.Empty;
                Proposaldr["Reason_for_Decline"] = string.Empty;
                Proposaldr["Reason_for_Postpone"] = string.Empty;
                Proposaldr["Reason_for_Repudiation"] = string.Empty;

                //Added by Sushant 
                Proposaldr["Client_Type"] = (!string.IsNullOrEmpty(ProposerClientType)) ? ProposerClientType : "Proposer";
                Proposaldr["RolesType"] = (!string.IsNullOrEmpty(ProposerRoles)) ? ProposerRoles : new Commfun().GetPreviousPolicyClientType(QuestDBNo, string.Empty);
                Proposaldr["Type"] = "FG";
                Proposaldt.Rows.Add(Proposaldr);
            }

            #endregion

            #region Proposal Griview Bind
            foreach (GridViewRow row in GridViewPayer.Rows)
            {
                Label lblQuestDBNo1 = (Label)row.FindControl("lblQuestDBNo1");
                Label lblMatchingPara = (Label)row.FindControl("lblMatchingPara");
                Label lblDoC = (Label)row.FindControl("lblDoC");
                Label lblsa = (Label)row.FindControl("lblsa");
                Label lblPolStatus = (Label)row.FindControl("lblPolStatus");
                Label lblDateofExit = (Label)row.FindControl("lblDateofExit");
                Label lblDateofDeath = (Label)row.FindControl("lblDateofDeath");
                Label lblupdateddate = (Label)row.FindControl("lblupdateddate");
                Label lblCautionStatus = (Label)row.FindControl("lblCautionStatus");
                Label lblInterMedCautionStatus = (Label)row.FindControl("lblInterMedCautionStatus");
                Label lblCompany = (Label)row.FindControl("lblCompany");
                Label lblNegative = (Label)row.FindControl("lblNegative");
                Label lblLAProposerPayor = (Label)row.FindControl("lblLAProposerPayor");
                Label lblPayorRoles = (Label)row.FindControl("lblPayorRoles");
                HiddenField hdnPayerClientType = (HiddenField)row.FindControl("hdnPayerClientType");
                Label lblLARolesType = (Label)row.FindControl("lblLARolesType");

                string QuestDBNo = lblQuestDBNo1.Text;
                string Matching_Parameter = lblMatchingPara.Text;
                string DoP_DoC = lblDoC.Text;
                string Sum_Assured = lblsa.Text;//.Split('.')[0].ToString();
                string Policy_Status = lblPolStatus.Text;
                string Date_of_Exit = lblDateofExit.Text;
                string Date_of_Death = lblDateofDeath.Text;
                string Cause_of_Death = lblCautionStatus.Text;
                string Record_last_updated = lblupdateddate.Text;
                string Entity_Caution_Status = lblCautionStatus.Text;
                string Intermediary_Caution_Status = lblInterMedCautionStatus.Text;
                string Company_Number = lblCompany.Text;
                string Negative_Match = lblNegative.Text;
                string LAProposerPayor = lblLAProposerPayor.Text;
                string PayorRoles = lblPayorRoles.Text;
                string PayerClientType = hdnPayerClientType.Value;

                DropDownList ddl = (DropDownList)row.FindControl("ddlimpact");
                string Impact = ddl.SelectedValue.ToString();

                DataRow Payerdr = Payerdt.NewRow();
                Payerdr["Input_Proposal_Policy_No"] = AppNo;
                Payerdr["QuestDBNo"] = QuestDBNo;
                Payerdr["Input_Matching_Parameter"] = Matching_Parameter;
                Payerdr["Quest_DoP_DoC"] = DoP_DoC;
                Payerdr["Quest_Sum_Assured"] = Convert.ToDecimal(Sum_Assured);
                Payerdr["Quest_Policy_Status"] = Policy_Status;
                Payerdr["Quest_Date_of_Exit"] = Date_of_Exit;
                Payerdr["Quest_Date_of_Death"] = Date_of_Death;
                Payerdr["Quest_Cause_of_Death"] = Cause_of_Death;
                Payerdr["Quest_Record_last_updated"] = Record_last_updated;
                Payerdr["Quest_Entity_Caution_Status"] = Entity_Caution_Status;
                Payerdr["Quest_Intermediary_Caution_Status"] = Intermediary_Caution_Status;
                Payerdr["Quest_Company_Number"] = Company_Number;
                Payerdr["Is_Negative_Match"] = Negative_Match;
                Payerdr["LAProposerPayor"] = LAProposerPayor;
                Payerdr["Status"] = "Active";
                Payerdr["CreatedBy"] = "UW";
                Payerdr["IIBServiceResponse"] = "Success";
                Payerdr["Impact"] = Impact;
                //Added by kavita new column in IIB Service changes
                Payerdr["Product_Type"] = string.Empty;
                Payerdr["Linked_Non_linked"] = string.Empty;
                Payerdr["Medical_Non_Medical"] = string.Empty;
                Payerdr["Whether_Standard_Life"] = string.Empty;
                Payerdr["Reason_for_Decline"] = string.Empty;
                Payerdr["Reason_for_Postpone"] = string.Empty;
                Payerdr["Reason_for_Repudiation"] = string.Empty;

                //Added by Sushant 
                Payerdr["Client_Type"] = (!string.IsNullOrEmpty(PayerClientType)) ? PayerClientType : "Payer";
                Payerdr["RolesType"] = (!string.IsNullOrEmpty(PayorRoles)) ? PayorRoles : new Commfun().GetPreviousPolicyClientType(QuestDBNo, string.Empty);
                Payerdr["Type"] = "FG";
                Payerdt.Rows.Add(Payerdr);
            }

            #endregion

            #endregion

            #region Other FGIL Grid

            #region Other LA

            foreach (GridViewRow row in GridViewOtherLA.Rows)
            {
                Label lblQuestDBNo1 = (Label)row.FindControl("lblQuestDBNo1");
                Label lblMatchingPara = (Label)row.FindControl("lblMatchingPara");
                Label lblDoC = (Label)row.FindControl("lblDoC");
                Label lblsa = (Label)row.FindControl("lblsa");
                Label lblPolStatus = (Label)row.FindControl("lblPolStatus");
                Label lblDateofExit = (Label)row.FindControl("lblDateofExit");
                Label lblDateofDeath = (Label)row.FindControl("lblDateofDeath");
                Label lblupdateddate = (Label)row.FindControl("lblupdateddate");
                Label lblCautionStatus = (Label)row.FindControl("lblCautionStatus");
                Label lblInterMedCautionStatus = (Label)row.FindControl("lblInterMedCautionStatus");
                Label lblCompany = (Label)row.FindControl("lblCompany");
                Label lblNegative = (Label)row.FindControl("lblNegative");
                Label lblLAProposerPayor = (Label)row.FindControl("lblLAProposerPayor");
                HiddenField hdnOtherLACliectType = (HiddenField)row.FindControl("hdnOtherLACliectType");

                string QuestDBNo = lblQuestDBNo1.Text;
                string Matching_Parameter = lblMatchingPara.Text;
                string DoP_DoC = lblDoC.Text;
                string Sum_Assured = lblsa.Text;//.Split('.')[0].ToString();
                string Policy_Status = lblPolStatus.Text;
                string Date_of_Exit = lblDateofExit.Text;
                string Date_of_Death = lblDateofDeath.Text;
                string Cause_of_Death = lblCautionStatus.Text;
                string Record_last_updated = lblupdateddate.Text;
                string Entity_Caution_Status = lblCautionStatus.Text;
                string Intermediary_Caution_Status = lblInterMedCautionStatus.Text;
                string Company_Number = lblCompany.Text;
                string Negative_Match = lblNegative.Text;
                string LAProposerPayor = lblLAProposerPayor.Text;
                string OtherLACliectType = hdnOtherLACliectType.Value;

                DropDownList ddl = (DropDownList)row.FindControl("ddlimpact");
                string Impact = ddl.SelectedValue.ToString();


                DataRow OtherFGdr = LAdt.NewRow();
                OtherFGdr["Input_Proposal_Policy_No"] = AppNo;
                OtherFGdr["QuestDBNo"] = QuestDBNo;
                OtherFGdr["Input_Matching_Parameter"] = Matching_Parameter;
                OtherFGdr["Quest_DoP_DoC"] = DoP_DoC;
                OtherFGdr["Quest_Sum_Assured"] = Convert.ToDecimal(Sum_Assured);
                OtherFGdr["Quest_Policy_Status"] = Policy_Status;
                OtherFGdr["Quest_Date_of_Exit"] = Date_of_Exit;
                OtherFGdr["Quest_Date_of_Death"] = Date_of_Death;
                OtherFGdr["Quest_Cause_of_Death"] = Cause_of_Death;
                OtherFGdr["Quest_Record_last_updated"] = Record_last_updated;
                OtherFGdr["Quest_Entity_Caution_Status"] = Entity_Caution_Status;
                OtherFGdr["Quest_Intermediary_Caution_Status"] = Intermediary_Caution_Status;
                OtherFGdr["Quest_Company_Number"] = Company_Number;
                OtherFGdr["Is_Negative_Match"] = Negative_Match;
                OtherFGdr["LAProposerPayor"] = LAProposerPayor;
                OtherFGdr["Status"] = "Active";
                OtherFGdr["CreatedBy"] = "UW";
                OtherFGdr["IIBServiceResponse"] = "Success";
                OtherFGdr["Impact"] = Impact;

                //Added by kavita new column in IIB Service changes
                OtherFGdr["Product_Type"] = string.Empty;
                OtherFGdr["Linked_Non_linked"] = string.Empty;
                OtherFGdr["Medical_Non_Medical"] = string.Empty;
                OtherFGdr["Whether_Standard_Life"] = string.Empty;
                OtherFGdr["Reason_for_Decline"] = string.Empty;
                OtherFGdr["Reason_for_Postpone"] = string.Empty;
                OtherFGdr["Reason_for_Repudiation"] = string.Empty;
                //Added by Sushant 
                OtherFGdr["Client_Type"] = (!string.IsNullOrEmpty(OtherLACliectType)) ? OtherLACliectType : "LA";
                OtherFGdr["RolesType"] = string.Empty;
                OtherFGdr["Type"] = "OTHER";
                LAdt.Rows.Add(OtherFGdr);
            }

            #endregion


            #region Other Proposer 

            foreach (GridViewRow row in GridViewOtherProposal.Rows)
            {
                Label lblQuestDBNo1 = (Label)row.FindControl("lblQuestDBNo1");
                Label lblMatchingPara = (Label)row.FindControl("lblMatchingPara");
                Label lblDoC = (Label)row.FindControl("lblDoC");
                Label lblsa = (Label)row.FindControl("lblsa");
                Label lblPolStatus = (Label)row.FindControl("lblPolStatus");
                Label lblDateofExit = (Label)row.FindControl("lblDateofExit");
                Label lblDateofDeath = (Label)row.FindControl("lblDateofDeath");
                Label lblupdateddate = (Label)row.FindControl("lblupdateddate");
                Label lblCautionStatus = (Label)row.FindControl("lblCautionStatus");
                Label lblInterMedCautionStatus = (Label)row.FindControl("lblInterMedCautionStatus");
                Label lblCompany = (Label)row.FindControl("lblCompany");
                Label lblNegative = (Label)row.FindControl("lblNegative");
                Label lblLAProposerPayor = (Label)row.FindControl("lblLAProposerPayor");
                HiddenField hdnOtherProposalCliectType = (HiddenField)row.FindControl("hdnOtherProposalCliectType");

                string QuestDBNo = lblQuestDBNo1.Text;
                string Matching_Parameter = lblMatchingPara.Text;
                string DoP_DoC = lblDoC.Text;
                string Sum_Assured = lblsa.Text;//.Split('.')[0].ToString();
                string Policy_Status = lblPolStatus.Text;
                string Date_of_Exit = lblDateofExit.Text;
                string Date_of_Death = lblDateofDeath.Text;
                string Cause_of_Death = lblCautionStatus.Text;
                string Record_last_updated = lblupdateddate.Text;
                string Entity_Caution_Status = lblCautionStatus.Text;
                string Intermediary_Caution_Status = lblInterMedCautionStatus.Text;
                string Company_Number = lblCompany.Text;
                string Negative_Match = lblNegative.Text;
                string LAProposerPayor = lblLAProposerPayor.Text;
                string OtherProposalCliectType = hdnOtherProposalCliectType.Value;
                DropDownList ddl = (DropDownList)row.FindControl("ddlimpact");
                string Impact = ddl.SelectedValue.ToString();


                DataRow OtherFGdr = Proposaldt.NewRow();
                OtherFGdr["Input_Proposal_Policy_No"] = AppNo;
                OtherFGdr["QuestDBNo"] = QuestDBNo;
                OtherFGdr["Input_Matching_Parameter"] = Matching_Parameter;
                OtherFGdr["Quest_DoP_DoC"] = DoP_DoC;
                OtherFGdr["Quest_Sum_Assured"] = Convert.ToDecimal(Sum_Assured);
                OtherFGdr["Quest_Policy_Status"] = Policy_Status;
                OtherFGdr["Quest_Date_of_Exit"] = Date_of_Exit;
                OtherFGdr["Quest_Date_of_Death"] = Date_of_Death;
                OtherFGdr["Quest_Cause_of_Death"] = Cause_of_Death;
                OtherFGdr["Quest_Record_last_updated"] = Record_last_updated;
                OtherFGdr["Quest_Entity_Caution_Status"] = Entity_Caution_Status;
                OtherFGdr["Quest_Intermediary_Caution_Status"] = Intermediary_Caution_Status;
                OtherFGdr["Quest_Company_Number"] = Company_Number;
                OtherFGdr["Is_Negative_Match"] = Negative_Match;
                OtherFGdr["LAProposerPayor"] = LAProposerPayor;
                OtherFGdr["Status"] = "Active";
                OtherFGdr["CreatedBy"] = "UW";
                OtherFGdr["IIBServiceResponse"] = "Success";
                OtherFGdr["Impact"] = Impact;

                //Added by kavita new column in IIB Service changes
                OtherFGdr["Product_Type"] = string.Empty;
                OtherFGdr["Linked_Non_linked"] = string.Empty;
                OtherFGdr["Medical_Non_Medical"] = string.Empty;
                OtherFGdr["Whether_Standard_Life"] = string.Empty;
                OtherFGdr["Reason_for_Decline"] = string.Empty;
                OtherFGdr["Reason_for_Postpone"] = string.Empty;
                OtherFGdr["Reason_for_Repudiation"] = string.Empty;
                //Added by Sushant 
                OtherFGdr["Client_Type"] = (!string.IsNullOrEmpty(OtherProposalCliectType)) ? OtherProposalCliectType : "Proposer";
                OtherFGdr["RolesType"] = string.Empty;
                OtherFGdr["Type"] = "OTHER";
                Proposaldt.Rows.Add(OtherFGdr);
            }

            #endregion

            #region Other Payer
            foreach (GridViewRow row in GridViewOtherPayer.Rows)
            {
                Label lblQuestDBNo1 = (Label)row.FindControl("lblQuestDBNo1");
                Label lblMatchingPara = (Label)row.FindControl("lblMatchingPara");
                Label lblDoC = (Label)row.FindControl("lblDoC");
                Label lblsa = (Label)row.FindControl("lblsa");
                Label lblPolStatus = (Label)row.FindControl("lblPolStatus");
                Label lblDateofExit = (Label)row.FindControl("lblDateofExit");
                Label lblDateofDeath = (Label)row.FindControl("lblDateofDeath");
                Label lblupdateddate = (Label)row.FindControl("lblupdateddate");
                Label lblCautionStatus = (Label)row.FindControl("lblCautionStatus");
                Label lblInterMedCautionStatus = (Label)row.FindControl("lblInterMedCautionStatus");
                Label lblCompany = (Label)row.FindControl("lblCompany");
                Label lblNegative = (Label)row.FindControl("lblNegative");
                Label lblLAProposerPayor = (Label)row.FindControl("lblLAProposerPayor");
                HiddenField hdnOtherPayerCliectType = (HiddenField)row.FindControl("hdnOtherPayerCliectType");

                string QuestDBNo = lblQuestDBNo1.Text;
                string Matching_Parameter = lblMatchingPara.Text;
                string DoP_DoC = lblDoC.Text;
                string Sum_Assured = lblsa.Text;//.Split('.')[0].ToString();
                string Policy_Status = lblPolStatus.Text;
                string Date_of_Exit = lblDateofExit.Text;
                string Date_of_Death = lblDateofDeath.Text;
                string Cause_of_Death = lblCautionStatus.Text;
                string Record_last_updated = lblupdateddate.Text;
                string Entity_Caution_Status = lblCautionStatus.Text;
                string Intermediary_Caution_Status = lblInterMedCautionStatus.Text;
                string Company_Number = lblCompany.Text;
                string Negative_Match = lblNegative.Text;
                string LAProposerPayor = lblLAProposerPayor.Text;
                string OtherPayerCliectType = hdnOtherPayerCliectType.Value;
                DropDownList ddl = (DropDownList)row.FindControl("ddlimpact");
                string Impact = ddl.SelectedValue.ToString();


                DataRow OtherFGdr = Payerdt.NewRow();
                OtherFGdr["Input_Proposal_Policy_No"] = AppNo;
                OtherFGdr["QuestDBNo"] = QuestDBNo;
                OtherFGdr["Input_Matching_Parameter"] = Matching_Parameter;
                OtherFGdr["Quest_DoP_DoC"] = DoP_DoC;
                OtherFGdr["Quest_Sum_Assured"] = Convert.ToDecimal(Sum_Assured);
                OtherFGdr["Quest_Policy_Status"] = Policy_Status;
                OtherFGdr["Quest_Date_of_Exit"] = Date_of_Exit;
                OtherFGdr["Quest_Date_of_Death"] = Date_of_Death;
                OtherFGdr["Quest_Cause_of_Death"] = Cause_of_Death;
                OtherFGdr["Quest_Record_last_updated"] = Record_last_updated;
                OtherFGdr["Quest_Entity_Caution_Status"] = Entity_Caution_Status;
                OtherFGdr["Quest_Intermediary_Caution_Status"] = Intermediary_Caution_Status;
                OtherFGdr["Quest_Company_Number"] = Company_Number;
                OtherFGdr["Is_Negative_Match"] = Negative_Match;
                OtherFGdr["LAProposerPayor"] = LAProposerPayor;
                OtherFGdr["Status"] = "Active";
                OtherFGdr["CreatedBy"] = "UW";
                OtherFGdr["IIBServiceResponse"] = "Success";
                OtherFGdr["Impact"] = Impact;

                //Added by kavita new column in IIB Service changes
                OtherFGdr["Product_Type"] = string.Empty;
                OtherFGdr["Linked_Non_linked"] = string.Empty;
                OtherFGdr["Medical_Non_Medical"] = string.Empty;
                OtherFGdr["Whether_Standard_Life"] = string.Empty;
                OtherFGdr["Reason_for_Decline"] = string.Empty;
                OtherFGdr["Reason_for_Postpone"] = string.Empty;
                OtherFGdr["Reason_for_Repudiation"] = string.Empty;
                //Added by Sushant 
                OtherFGdr["Client_Type"] = (!string.IsNullOrEmpty(OtherPayerCliectType)) ? OtherPayerCliectType : "Payer";
                OtherFGdr["RolesType"] = string.Empty;
                OtherFGdr["Type"] = "OTHER";
                Payerdt.Rows.Add(OtherFGdr);
            }

            #endregion

            #endregion
            #region Insertion

            if (LAdt.Rows.Count > 0)
            {
                objComm.IIBQueryData_Impact(LAdt);
            }

            if (Proposaldt.Rows.Count > 0)
            {
                objComm.IIBQueryData_Impact(Proposaldt);
            }
            if (Payerdt.Rows.Count > 0)
            {
                objComm.IIBQueryData_Impact(Payerdt);
            }

            #endregion



            #endregion End 33.1: This CR-2809 changes done by Sushant Devkate MFL00905 
        }
        catch (Exception ex)
        { }
    }

    #endregion

    #endregion 35.1 Begin of changes  CR-30489 kavita mfl00255


    #region Start 33.1: This CR-2809 changes done by Sushant Devkate MFL00905 
    public void BindGridforIIB(IIBQueryDataDO ObjIIBQueryDataDO, int Bindgrid)
    {

        #region Display none div of FG and Other FG
        LADiv.Style.Add("display", "none");
        divPayer.Style.Add("display", "none");
        ProposerDiv.Style.Add("display", "none");

        DivGridViewOtherLA.Style.Add("display", "none");
        DivGridViewOtherPayer.Style.Add("display", "none");
        DivGridViewOtherProposal.Style.Add("display", "none");
        #endregion

        if (Bindgrid == 1)  //For UW DATA
        {
            if (ObjIIBQueryDataDO.ObjListOFQueryFG.Count > 0)
            {
                var ObjListClientTypeNull = ObjIIBQueryDataDO.ObjListOFQueryFG.Where(x => x.Client_Type == "").ToList();

                var ObjListClientType = ObjIIBQueryDataDO.ObjListOFQueryFG.Where(x => x.Client_Type != "").ToList();


                if (ObjListClientType.Count > 0)
                {

                    var ObjListLA = ObjListClientType.Where(x => x.Client_Type.ToLower() == "la").ToList();

                    var ObjListPayer = ObjListClientType.Where(x => x.Client_Type.ToLower() == "payer").ToList();

                    var ObjListProposer = ObjListClientType.Where(x => x.Client_Type.ToLower() == "proposer").ToList();

                    if (ObjListLA.Count > 0)
                    {
                        LADiv.Style.Add("display", "block");
                        GridViewLA.DataSource = ObjListLA;
                        GridViewLA.DataBind();
                        GridViewLA.Visible = true;
                        LabelLA.Visible = true;
                        LabelLA.Text = "IIB query output for Life Assured";

                        var LAPolicyCountDetails = (from Pstatus in ObjListLA.AsEnumerable()
                                                    group Pstatus by Pstatus.Quest_Policy_Status
                                                  into g
                                                    select new TotalPolicyDO
                                                    {
                                                        PolicyNo = g.First().QuestDBNo,
                                                        PolicyStatus = g.First().Quest_Policy_Status,
                                                        TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                    }).ToList();

                        if (LAPolicyCountDetails.Count > 0)
                        {
                            GridViewLAPolicy.DataSource = LAPolicyCountDetails;
                            GridViewLAPolicy.DataBind();
                            GridViewLAPolicy.Visible = true;
                            //lblLAPolicyDetails.Visible = true;
                            //lblLAPolicyDetails.Text = "Total Policy Status";

                        }
                    }

                    if (ObjListPayer.Count > 0)
                    {
                        divPayer.Style.Add("display", "block");
                        GridViewPayer.DataSource = ObjListPayer;
                        GridViewPayer.DataBind();
                        GridViewPayer.Visible = true;
                        LabelPayer.Visible = true;
                        LabelPayer.Text = "IIB query output for Payer";

                        var PayerPolicyCountDetails = (from Pstatus in ObjListPayer.AsEnumerable()
                                                       group Pstatus by Pstatus.Quest_Policy_Status
                                                into g
                                                       select new TotalPolicyDO
                                                       {
                                                           PolicyNo = g.First().QuestDBNo,
                                                           PolicyStatus = g.First().Quest_Policy_Status,
                                                           TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                       }).ToList();

                        if (PayerPolicyCountDetails.Count > 0)
                        {
                            GridViewPayerPolicy.DataSource = PayerPolicyCountDetails;
                            GridViewPayerPolicy.DataBind();
                            GridViewPayerPolicy.Visible = true;
                            //lblPayerPolicyDetails.Visible = true;
                            //lblPayerPolicyDetails.Text = "Total Policy Status";

                        }
                    }

                    if (ObjListProposer.Count > 0)
                    {
                        ProposerDiv.Style.Add("display", "block");
                        GridViewProposer.DataSource = ObjListProposer;
                        GridViewProposer.DataBind();
                        GridViewProposer.Visible = true;
                        LabelProposer.Visible = true;
                        LabelProposer.Text = "IIB query output for proposer";

                        var ProposerPolicyCountDetails = (from Pstatus in ObjListProposer.AsEnumerable()
                                                          group Pstatus by Pstatus.Quest_Policy_Status
                                                into g
                                                          select new TotalPolicyDO
                                                          {
                                                              PolicyNo = g.First().QuestDBNo,
                                                              PolicyStatus = g.First().Quest_Policy_Status,
                                                              TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                          }).ToList();

                        if (ProposerPolicyCountDetails.Count > 0)
                        {
                            GridViewProposerPolicy.DataSource = ProposerPolicyCountDetails;
                            GridViewProposerPolicy.DataBind();
                            GridViewProposerPolicy.Visible = true;
                            //lblProposerPolicyDetails.Visible = true;
                            //lblProposerPolicyDetails.Text= "Total Policy Status";

                        }
                    }
                }
                else
                {

                    var ObjListLANULL = ObjListClientTypeNull.ToList();
                    if (ObjListLANULL.Count > 0)
                    {
                        LADiv.Style.Add("display", "block");
                        GridViewLA.DataSource = ObjListLANULL;
                        GridViewLA.DataBind();
                        GridViewLA.Visible = true;
                        LabelLA.Visible = true;
                        LabelLA.Text = "IIB query output for Life Assured";

                        var LAPolicyCountDetailsNUll = (from Pstatus in ObjListLANULL.AsEnumerable()
                                                        group Pstatus by Pstatus.Quest_Policy_Status
                                                  into g
                                                        select new TotalPolicyDO
                                                        {
                                                            PolicyNo = g.First().QuestDBNo,
                                                            PolicyStatus = g.First().Quest_Policy_Status,
                                                            TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                        }).ToList();

                        if (LAPolicyCountDetailsNUll.Count > 0)
                        {
                            GridViewLAPolicy.DataSource = LAPolicyCountDetailsNUll;
                            GridViewLAPolicy.DataBind();
                            GridViewLAPolicy.Visible = true;
                            //lblLAPolicyDetails.Visible = true;
                            //lblLAPolicyDetails.Text = "Total Policy Status";

                        }
                    }

                }

            }

            if (ObjIIBQueryDataDO.ObjListOFQueryOther.Count > 0)
            {
                var ObjListClientTypeNull = ObjIIBQueryDataDO.ObjListOFQueryOther.Where(x => x.Client_Type == "").ToList();

                var ObjListClientType = ObjIIBQueryDataDO.ObjListOFQueryOther.Where(x => x.Client_Type != "").ToList();


                if (ObjListClientType.Count > 0)
                {
                    var ObjListLA = ObjListClientType.Where(x => x.Client_Type.ToLower() == "la").ToList();

                    var ObjListPayer = ObjListClientType.Where(x => x.Client_Type.ToLower() == "payer").ToList();

                    var ObjListProposer = ObjListClientType.Where(x => x.Client_Type.ToLower() == "proposer").ToList();

                    if (ObjListLA.Count > 0)
                    {
                        DivGridViewOtherLA.Style.Add("display", "block");
                        GridViewOtherLA.DataSource = ObjListLA;
                        GridViewOtherLA.DataBind();
                        GridViewOtherLA.Visible = true;
                        lblGridViewOtherLA.Visible = true;
                        lblGridViewOtherLA.Text = "IIB query output for Life Assured";

                        lblothercompany.Visible = true;
                        lblothercompany.ForeColor = System.Drawing.Color.Red;
                        lblothercompany.Text = "The existing policies found in other insurance companies,please ensure to collect the information from client in proposal form / declaration";


                        var LAOtherPolicyCountDetails = (from Pstatus in ObjListLA.AsEnumerable()
                                                         group Pstatus by Pstatus.Quest_Policy_Status
                                                  into g
                                                         select new TotalPolicyDO
                                                         {
                                                             PolicyNo = g.First().QuestDBNo,
                                                             PolicyStatus = g.First().Quest_Policy_Status,
                                                             TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                         }).ToList();

                        if (LAOtherPolicyCountDetails.Count > 0)
                        {
                            GridViewOtherLAPolicy.DataSource = LAOtherPolicyCountDetails;
                            GridViewOtherLAPolicy.DataBind();
                            GridViewOtherLAPolicy.Visible = true;
                        }


                    }

                    if (ObjListPayer.Count > 0)
                    {
                        DivGridViewOtherPayer.Style.Add("display", "block");
                        GridViewOtherPayer.DataSource = ObjListPayer;
                        GridViewOtherPayer.DataBind();
                        GridViewOtherPayer.Visible = true;
                        lblGridViewOtherPayer.Visible = true;
                        lblGridViewOtherPayer.Text = "IIB query output for Payer";

                        lblothercompany.Visible = true;
                        lblothercompany.ForeColor = System.Drawing.Color.Red;
                        lblothercompany.Text = "The existing policies found in other insurance companies,please ensure to collect the information from client in proposal form / declaration";

                        var PayerOTherPolicyCountDetails = (from Pstatus in ObjListPayer.AsEnumerable()
                                                            group Pstatus by Pstatus.Quest_Policy_Status
                                               into g
                                                            select new TotalPolicyDO
                                                            {
                                                                PolicyNo = g.First().QuestDBNo,
                                                                PolicyStatus = g.First().Quest_Policy_Status,
                                                                TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                            }).ToList();

                        if (PayerOTherPolicyCountDetails.Count > 0)
                        {
                            GridViewOtherPayerPolicy.DataSource = PayerOTherPolicyCountDetails;
                            GridViewOtherPayerPolicy.DataBind();
                            GridViewOtherPayerPolicy.Visible = true;
                            //lblPayerPolicyDetails.Visible = true;
                            //lblPayerPolicyDetails.Text = "Total Policy Status";

                        }

                    }

                    if (ObjListProposer.Count > 0)
                    {
                        DivGridViewOtherProposal.Style.Add("display", "block");
                        GridViewOtherProposal.DataSource = ObjListProposer;
                        GridViewOtherProposal.DataBind();
                        GridViewOtherProposal.Visible = true;
                        lblGridViewOtherProposal.Visible = true;
                        lblGridViewOtherProposal.Text = "IIB query output for proposer";

                        lblothercompany.Visible = true;
                        lblothercompany.ForeColor = System.Drawing.Color.Red;
                        lblothercompany.Text = "The existing policies found in other insurance companies,please ensure to collect the information from client in proposal form / declaration";


                        var OtherProposerPolicyCountDetails = (from Pstatus in ObjListProposer.AsEnumerable()
                                                               group Pstatus by Pstatus.Quest_Policy_Status
                                                          into g
                                                               select new TotalPolicyDO
                                                               {
                                                                   PolicyNo = g.First().QuestDBNo,
                                                                   PolicyStatus = g.First().Quest_Policy_Status,
                                                                   TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                               }).ToList();

                        if (OtherProposerPolicyCountDetails.Count > 0)
                        {
                            GridViewOtherProposerPolicy.DataSource = OtherProposerPolicyCountDetails;
                            GridViewOtherProposerPolicy.DataBind();
                            GridViewOtherProposerPolicy.Visible = true;


                        }


                    }

                }
                else
                {
                    var ObjListLANULL = ObjListClientTypeNull.ToList();
                    if (ObjListLANULL.Count > 0)
                    {
                        DivGridViewOtherLA.Style.Add("display", "block");
                        GridViewOtherLA.DataSource = ObjListLANULL;
                        GridViewOtherLA.DataBind();
                        GridViewOtherLA.Visible = true;
                        lblGridViewOtherLA.Visible = true;
                        lblGridViewOtherLA.Text = "IIB query output for Life Assured";

                        lblothercompany.Visible = true;
                        lblothercompany.ForeColor = System.Drawing.Color.Red;
                        lblothercompany.Text = "The existing policies found in other insurance companies,please ensure to collect the information from client in proposal form / declaration";

                        var OtherLAPolicyCountDetailsNUll = (from Pstatus in ObjListLANULL.AsEnumerable()
                                                             group Pstatus by Pstatus.Quest_Policy_Status
                                                 into g
                                                             select new TotalPolicyDO
                                                             {
                                                                 PolicyNo = g.First().QuestDBNo,
                                                                 PolicyStatus = g.First().Quest_Policy_Status,
                                                                 TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                             }).ToList();

                        if (OtherLAPolicyCountDetailsNUll.Count > 0)
                        {
                            GridViewOtherLAPolicy.DataSource = OtherLAPolicyCountDetailsNUll;
                            GridViewOtherLAPolicy.DataBind();
                            GridViewOtherLAPolicy.Visible = true;
                        }


                    }
                }
            }


        }
        else if (Bindgrid == 2)  //IF DE DATA
        {
            if (ObjIIBQueryDataDO.ObjListOFQueryFG.Count > 0)
            {
                var ObjListLA = ObjIIBQueryDataDO.ObjListOFQueryFG.ToList();

                if (ObjListLA.Count > 0)
                {
                    LADiv.Style.Add("display", "block");
                    GridViewLA.DataSource = ObjListLA;
                    GridViewLA.DataBind();
                    GridViewLA.Visible = true;
                    LabelLA.Visible = true;
                    LabelLA.Text = "IIB query output for Life Assured";

                    var LAPolicyCountDetails = (from Pstatus in ObjListLA.AsEnumerable()
                                                group Pstatus by Pstatus.Quest_Policy_Status
                                              into g
                                                select new TotalPolicyDO
                                                {
                                                    PolicyNo = g.First().QuestDBNo,
                                                    PolicyStatus = g.First().Quest_Policy_Status,
                                                    TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                }).ToList();

                    if (LAPolicyCountDetails.Count > 0)
                    {
                        GridViewLAPolicy.DataSource = LAPolicyCountDetails;
                        GridViewLAPolicy.DataBind();
                        GridViewLAPolicy.Visible = true;
                        //lblLAPolicyDetails.Visible = true;
                        //lblLAPolicyDetails.Text = "Total Policy Status";

                    }



                }
            }
            if (ObjIIBQueryDataDO.ObjListOFQueryOther.Count > 0)
            {
                var ObjListClientType = ObjIIBQueryDataDO.ObjListOFQueryOther.ToList();
                if (ObjListClientType.Count > 0)
                {
                    DivGridViewOtherLA.Style.Add("display", "block");
                    GridViewOtherLA.DataSource = ObjListClientType;
                    GridViewOtherLA.DataBind();
                    GridViewOtherLA.Visible = true;
                    lblGridViewOtherLA.Visible = true;
                    lblGridViewOtherLA.Text = "IIB query output for Life Assured";

                    lblothercompany.Visible = true;
                    lblothercompany.ForeColor = System.Drawing.Color.Red;
                    lblothercompany.Text = "The existing policies found in other insurance companies,please ensure to collect the information from client in proposal form / declaration";

                    var OtherLAPolicyCountDetails = (from Pstatus in ObjListClientType.AsEnumerable()
                                                     group Pstatus by Pstatus.Quest_Policy_Status
                                              into g
                                                     select new TotalPolicyDO
                                                     {
                                                         PolicyNo = g.First().QuestDBNo,
                                                         PolicyStatus = g.First().Quest_Policy_Status,
                                                         TotalSA = g.Sum(f => f.Quest_Sum_Assured),

                                                     }).ToList();

                    if (OtherLAPolicyCountDetails.Count > 0)
                    {
                        GridViewOtherLAPolicy.DataSource = OtherLAPolicyCountDetails;
                        GridViewOtherLAPolicy.DataBind();
                        GridViewOtherLAPolicy.Visible = true;

                    }

                }


            }

        }
    }

    #region show some cell in color 
    protected void GridViewLA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;
            string Status = e.Row.Cells[4].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }

                //added by suraj to get red colour for status is pending suggested by Diju on 29 May 2021
                if (Status.Contains("Pending for U/w"))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;

                }
            }
        }
    }
    protected void GridViewProposer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;
            string Status = e.Row.Cells[4].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }

                //added by suraj to get red colour for status is pending suggested by Diju on 29 May 2021
                if (Status.Contains("Pending for U/w"))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;

                }
            }
        }
    }

    protected void GridViewPayer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;
            string Status = e.Row.Cells[4].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }

                //added by suraj to get red colour for status is pending suggested by Diju on 29 May 2021
                if (Status.Contains("Pending for U/w"))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;

                }
            }
        }
    }
    #endregion


    protected void GridViewOtherLA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;
            string Status = e.Row.Cells[4].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }

                //added by suraj to get red colour for status is pending suggested by Diju on 29 May 2021
                if (Status.Contains("Pending for U/w"))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;

                }
            }
        }
    }

    protected void GridViewOtherProposal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;
            string Status = e.Row.Cells[4].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }

                //added by suraj to get red colour for status is pending suggested by Diju on 29 May 2021
                if (Status.Contains("Pending for U/w"))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;

                }
            }
        }
    }

    protected void GridViewOtherPayer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;
            string Status = e.Row.Cells[4].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }

                //added by suraj to get red colour for status is pending suggested by Diju on 29 May 2021
                if (Status.Contains("Pending for U/w"))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;

                }
            }
        }
    }


    #endregion End 33.1: This CR-2809 changes done by Sushant Devkate MFL00905

    //35.1 Begin of Changes; Sagar Thorave-[mfl00886]
    public void SelectMerCheckbox(string FollowUpcode)
    {

        if (FollowUpcode == "MPV")
        {
            foreach (ListItem li in cblDecisionTypeDecisions.Items)
            {
                if (Convert.ToInt32(li.Value) == 12)
                {
                    li.Selected = true;
                }
            }
        }
        if (FollowUpcode == "MPN")
        {
            foreach (ListItem li in cblDecisionTypeDecisions.Items)
            {
                if (Convert.ToInt32(li.Value) == 17)
                {
                    li.Selected = true;
                }
            }
        }
    }
    public void NotSelectMerCheckbox(string FollowUpcode)
    {

        if (FollowUpcode != "MPV")
        {
            foreach (ListItem li in cblDecisionTypeDecisions.Items)
            {
                if (Convert.ToInt32(li.Value) == 12)
                {
                    li.Selected = false;
                }
            }
        }
        if (FollowUpcode != "MPN")
        {
            foreach (ListItem li in cblDecisionTypeDecisions.Items)
            {
                if (Convert.ToInt32(li.Value) == 17)
                {
                    li.Selected = false;
                }
            }
        }
    }
    //35.1 End of Changes; Sagar Thorave-[mfl00886]

    //38.1 Begin of Changes; Bhaumik Patel - [CR-4134]
    protected void btnsaveRedFlag_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow Row in GridRedFlag.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    int RedFlagID = Convert.ToInt32(Row.Cells[0].Text);
                    DropDownList ddlredflaguwdecision = (DropDownList)Row.FindControl("ddlredflaguwdecision");
                    string Decision = ddlredflaguwdecision.SelectedItem.Text.ToString();  //_Dt.Rows[Row.RowIndex]["UW Decision"].ToString();

                    TextBox txtuwcomment = (TextBox)Row.FindControl("txtuwcomment");
                    string Comment = txtuwcomment.Text.ToString();  //_Dt.Rows[Row.RowIndex]["UW Comment"].ToString();

                    if (Decision != "Select" && Comment != "")
                    {
                        UpdateUWDecision_RedFlag(RedFlagID, Decision, Comment);
                    }
                    else
                    {
                        //  ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>ShowErrorMessage();</script>", false);
                    }

                }

            }
            chkredflag.Checked = false;
        }
        catch (Exception ex)
        {
            ShowPopupMessage(ex.Message);
            new Commfun().InsertRedFlagReasonException(strApplicationno, "btnSaveRedFlag", ex.Message, objChangeObj.userLoginDetails._UserID);
        }

    }
    protected void lnkRedFlag_Click(object sender, EventArgs e)
    {
        chkredflag.Checked = false;
        FillRedFlagDetails(strApplicationno);
    }
    public void UpdateUWDecision_RedFlag(int RedFlagID, string UWdecision, string UWcomments)
    {
        objComm.Online_Update_RedFlagDetails("UpdateUWDecision", strApplicationno, RedFlagID, UWdecision, UWcomments, strUserId);

    }
    protected void GridRedFlag_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridRedFlag.PageIndex = e.NewPageIndex;
        FillRedFlagDetails(strApplicationno);
    }
    //38.1 End of Changes; Bhaumik Patel - [CR-4134]

    //36.2 Begin of changes; Sagar Thorave[mfl00886]
    public void GetAmlRiskValue()
    {

        string AmlIndicator = DecideAmlRiskFlag();

        if (AmlIndicator == "1")
        {
            DrpAml.SelectedValue = "1";
            DrpAml.ForeColor = System.Drawing.Color.White;
            DrpAml.BackColor = System.Drawing.Color.DarkGreen;
            //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]
            //txtriskcat.Text = DrpAml.SelectedItem.Text;
            //txtriskcat.ForeColor = System.Drawing.Color.White;
            //txtriskcat.BackColor = System.Drawing.Color.DarkGreen;

        }
        if (AmlIndicator == "2")
        {
            DrpAml.SelectedValue = "2";
            DrpAml.ForeColor = System.Drawing.Color.White;
            DrpAml.BackColor = System.Drawing.Color.Green;
            //txtriskcat.Text = DrpAml.SelectedItem.Text;
            //txtriskcat.ForeColor = System.Drawing.Color.White;
            //txtriskcat.BackColor = System.Drawing.Color.Green;
        }
        if (AmlIndicator == "3")
        {
            DrpAml.SelectedValue = "3";
            DrpAml.ForeColor = System.Drawing.Color.White;
            DrpAml.BackColor = System.Drawing.Color.IndianRed;
            //txtriskcat.Text = DrpAml.SelectedItem.Text;
            //txtriskcat.ForeColor = System.Drawing.Color.White;
            //txtriskcat.BackColor = System.Drawing.Color.IndianRed;
            //45.1 End of Changes; Bhaumik Patel - [CR-5307]
        }
        if (AmlIndicator == "4")
        {
            DrpAml.SelectedValue = "4";
            DrpAml.ForeColor = System.Drawing.Color.White;
            DrpAml.BackColor = System.Drawing.Color.DarkRed;
            //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]
            //txtriskcat.Text = DrpAml.SelectedItem.Text;
            //txtriskcat.ForeColor = System.Drawing.Color.White;
            //txtriskcat.BackColor = System.Drawing.Color.DarkRed;
            //45.1 End of Changes; Bhaumik Patel - [CR-5307]
        }




    }
    public string DecideAmlRiskFlag()
    {
        string MainIndiacator = string.Empty;
        try
        {
            Commfun objComm = new Commfun();

            DataSet ds12 = null;
            DataSet ds13 = null;
            DataSet ds14 = null;
            objComm.Featch_AMLFLAG_Details(ref ds12, txtAppno.Text, "SaralUW");
            string AmlIndicator = (ds12.Tables[0].Rows[0]["CLNTRSKIND"].ToString());
            objComm.Featch_AMLFLAG_Details_Payer(ref ds13, txtAppno.Text, "SaralUW");
            string AmlIndicator_payer = (ds13.Tables[0].Rows[0]["CLNTRSKIND"].ToString());
            objComm.Featch_AMLFLAG_Details_Proposar(ref ds14, txtAppno.Text, "SaralUW");
            string AmlIndicator_proposer = (ds14.Tables[0].Rows[0]["CLNTRSKIND"].ToString());

            if (AmlIndicator.Equals("4") || AmlIndicator_payer.Equals("4") || AmlIndicator_proposer.Equals("4"))
            {
                MainIndiacator = "4";
            }
            else
            {
                MainIndiacator = "1";
            }

            return MainIndiacator;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return MainIndiacator;
    }
    public string GetAmlValTOLifeAsia()
    {
        string AmlValue = string.Empty;
        DataSet ds12 = null;
        DataSet ds14 = null;
        objComm.Featch_AMLFLAG_Details(ref ds12, txtAppno.Text, "SaralUW");
        AmlValue = (ds12.Tables[0].Rows[0]["CLNTRSKIND"].ToString());
        if (ddlApplicationDetailsProposalType.SelectedValue != null || ddlApplicationDetailsProposalType.SelectedValue != "0")
        {
            
            if (ddlApplicationDetailsProposalType.SelectedValue.Equals("ORD"))
            {
                if (AmlValue == "1")
                {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "Low Risk", AmlValue, "Final Value send to Life asia LA", "SaralUW");
                }
                if (AmlValue == "4")
                {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "High Risk", AmlValue, "Final Value send to Life asia LA", "SaralUW");
                }
            }
            else
            {
                AmlValue = "4";
               
                string ClientId_LA = (ds12.Tables[0].Rows[0]["ClientNo"].ToString());
               
                objComm.Featch_AMLFLAG_Details_Proposar(ref ds14, txtAppno.Text, "SaralUW");
                string ClientId_proposer = (ds14.Tables[0].Rows[0]["ClientNo"].ToString());

                if (ClientId_LA == ClientId_proposer)
                {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "High Risk", AmlValue, "Final Value send to Life asia LA", "SaralUW");
                }
                else {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "High Risk", AmlValue, "Final Value send to Life asia LA", "SaralUW");
                    objComm.Insert_AMLFLAG_Details_Propser(txtAppno.Text, "High Risk", AmlValue, "Final Value send to Life asia Proposer", "SaralUW");
                }


            }
        }
        else
        {
           
            if (AmlValue == "1")
            {
                objComm.Insert_AMLFLAG_Details(txtAppno.Text, "Low Risk", AmlValue, "Final Value send to Life asia LA", "SaralUW");
            }
            if (AmlValue == "4")
            {
                objComm.Insert_AMLFLAG_Details(txtAppno.Text, "High Risk", AmlValue, "Final Value send to Life asia LA", "SaralUW");
            }
        }
        return AmlValue;
    }
    protected void DrpAml_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            string AmlIndicator = DrpAml.SelectedValue;
            if (!string.IsNullOrEmpty(AmlIndicator))
            {

                if (AmlIndicator == "1")
                {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "Low", AmlIndicator, "Manually Change By User", "SaralUW");
                    DrpAml.SelectedValue = "1";
                    DrpAml.ForeColor = System.Drawing.Color.White;
                    DrpAml.BackColor = System.Drawing.Color.DarkGreen;
                    //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]
                    //txtriskcat.Text = DrpAml.SelectedItem.Text;
                    //txtriskcat.ForeColor = System.Drawing.Color.White;
                    //txtriskcat.BackColor = System.Drawing.Color.DarkGreen;
                }
                if (AmlIndicator == "2")
                {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "Medium", AmlIndicator, "Manually Change By User", "SaralUW");
                    DrpAml.SelectedValue = "2";
                    DrpAml.ForeColor = System.Drawing.Color.White;
                    DrpAml.BackColor = System.Drawing.Color.Green;
                    //txtriskcat.Text = DrpAml.SelectedItem.Text;
                    //txtriskcat.ForeColor = System.Drawing.Color.White;
                    //txtriskcat.BackColor = System.Drawing.Color.Green;

                }
                if (AmlIndicator == "3")
                {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "High Risk", AmlIndicator, "Manually Change By User", "SaralUW");
                    DrpAml.SelectedValue = "3";
                    DrpAml.ForeColor = System.Drawing.Color.White;
                    DrpAml.BackColor = System.Drawing.Color.IndianRed;
                    //txtriskcat.Text = DrpAml.SelectedItem.Text;
                    //txtriskcat.ForeColor = System.Drawing.Color.White;
                    //txtriskcat.BackColor = System.Drawing.Color.IndianRed;

                }
                //45.1 End of Changes; Bhaumik Patel - [CR-5307]

                if (AmlIndicator == "4")
                {
                    objComm.Insert_AMLFLAG_Details(txtAppno.Text, "Very High Risk", AmlIndicator, "Manually Change By User", "SaralUW");
                    DrpAml.SelectedValue = "4";
                    DrpAml.ForeColor = System.Drawing.Color.White;
                    DrpAml.BackColor = System.Drawing.Color.DarkRed;
                    //45.1 Begin of Changes; Bhaumik Patel - [CR-5307]
                    //txtriskcat.Text = DrpAml.SelectedItem.Text;
                    //txtriskcat.ForeColor = System.Drawing.Color.White;
                    //txtriskcat.BackColor = System.Drawing.Color.DarkRed;
                    //45.1 End of Changes; Bhaumik Patel - [CR-5307]
                }
            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }
    //36.2 End of changes; Sagar Thorave[mfl00886]

    //38.1 Begin of Changes; Sushant Devkate -MFL00905
    protected void ISSmbtnProceed_Click(object sender, EventArgs e)
    {
        string strOtherReason = string.Empty;
        string strReason = string.Empty;
        bool Reason = false;
        string SaveComments = string.Empty;
        int UWCommentResult = 0;
        try
        {
            if (rbUC.Checked)
            {
                SaveComments = strReason = rbUC.Text.Trim();
                Reason = rbUC.Checked;
            }
            else if (RBOther.Checked)
            {
                strReason = RBOther.Text.Trim();
                SaveComments = strOtherReason = txtotherReasons.Text.Trim();
                Reason = RBOther.Checked;
            }

            objChangeObj = (ChangeValue)Session["objLoginObj"];
            int Success = new Commfun().InsertSmokarReason(strApplicationno, chkSaralRiskIsSmoker.Checked, "", strReason, strOtherReason, objChangeObj.userLoginDetails._UserID);
            objCommonObj = (CommonObject)Session["objCommonObj"];
            objComm.OnlineUWCommentsDetails_Save(objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._ProcessName, objChangeObj.userLoginDetails._UserGroup, SaveComments, strApplicationno, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ddlComment.SelectedValue, ref UWCommentResult);
            ddlComment.SelectedValue = "General";
            FillCommentsDetails(strApplicationno, strChannelType, ddlComment.SelectedValue);
            updCommentsDtls.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#myModal", "$('.modal-backdrop').remove();$('#myModal').hide();", true);
            //ShowPopupMessage("Reason inserted successfully");
        }
        catch (Exception ex)
        {
            ShowPopupMessage(ex.Message);
            new Commfun().InsertSmokarReasonException(strApplicationno, "ISSmbtnProceed_Click", ex.Message, objChangeObj.userLoginDetails._UserID);
        }
    }

    protected void btncloseSmoker_Click(object sender, EventArgs e)
    {
        //chkRiskParamIsSmoker_CheckedChanged(null,null);
        chkSaralRiskIsSmoker.Checked = false;
    }

    //38.1 End of Changes; Sushant Devkate -MFL00905
    //43.1 Begin of Changes; Sagar Thorave -MFL00886
    public void SetSpecialInsurance()
    {
        try
        {
            DataSet dsPro = new DataSet();
            objComm.SetProposalType(ref dsPro, txtAppno.Text);
            String SpecialInsurance = dsPro.Tables[0].Rows[0]["SpecialInsurance"].ToString();
            ddlApplicationDetailsProposalType.SelectedValue = SpecialInsurance;
            ddlApplicationDetailsProposalType.Items.FindByValue(Convert.ToString(dsPro.Tables[0].Rows[0]["SpecialInsurance"])).Selected = true;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    //43.1 End of Changes; Sagar Thorave -MFL00886
    //34.1 Begin of Changes; Sagar Thorave-[mfl00886]
    protected void Update_ENY_Click(object sender, EventArgs e)
    {
        if (IsENY == "true")
        {
            Get_EnyScore_Data();
        }

    }
    //34.1 End of Changes; Sagar Thorave-[mfl00886]

    #region 46.1 CR-4753
    //46.1 Begin of Changes; Bhaumik Patel - [CR-4753]
    public void FillEXAprrovalDetails(String strApplicationno)
    {
        chkredflag.Checked = false;
        DataSet _dsEXAprrovalDtls = new DataSet();
        string Mode;
        Mode = "UWDecisionEXAprrovalGrid";
        objComm.OnlineEXAprrovalDetails_GET(ref _dsEXAprrovalDtls, strApplicationno, Mode);
        if (_dsEXAprrovalDtls.Tables.Count > 0)
        {
            BindEXAprrovalDetails(_dsEXAprrovalDtls.Tables[0]);
        }
    }

    public void BindEXAprrovalDetails(DataTable _dsEXAprrovalDtls)
    {
        if (_dsEXAprrovalDtls.Rows.Count > 0)
        {
            gridExaprroval.DataSource = _dsEXAprrovalDtls;
            gridExaprroval.DataBind();
        }

    }

    public void InsertEXAprrovalDetails(string Boeid, string Parameters, string Decision)
    {
        int result = 0;
        objComm.Online_Insert_EXAprrovalDetails( strApplicationno, Boeid, strPolicyNo, Parameters, Decision, strUserId, "Exception Approval", strUserId,ref result);
        if(result >0)
        {
            ShowPopupMessage("Exception Approval Details Inserted Successfuly ..");
        }
        else
        {
            ShowPopupMessage("Exception Approval Details  Not Inserted  ..");
        }

    }

    protected void btnEXAPSave_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow Row in gridExaprroval.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtEXAprrovalParameters = (TextBox)Row.FindControl("txtEXAprrovalParameters");
                    DropDownList ddlEXAprrovaluwdecision = (DropDownList)Row.FindControl("ddlEXAprrovaluwdecision");
                    Label lblBoeid = (Label)Row.FindControl("lblBoeid");
                    string Decision = ddlEXAprrovaluwdecision.SelectedItem.Text.ToString();  //_Dt.Rows[Row.RowIndex]["UW Decision"].ToString();
                    string Parameters = txtEXAprrovalParameters.Text.ToString();
                    string Boeid = lblBoeid.Text.ToString();

                    if (Decision != "Select" && Parameters != "")
                    {
                        InsertEXAprrovalDetails(Boeid,Parameters, Decision);
                    }
                    else
                    {
                        //  ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>ShowErrorMessage();</script>", false);
                    }

                }

            }
            CheckEXAp.Checked = false;
        }
        catch (Exception ex)
        {
            ShowPopupMessage(ex.Message);
        }
    }
    //46.1 End of Changes; Bhaumik Patel - [CR-4753]
    #endregion

    // 47.1 Begin of Changes; Jayendra  - [Webashlar01]

    private void SetInitialRequirmentGrid_STP()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :SetInitialRequirmentGrid_STP" + System.Environment.NewLine);
        DataTable dt = new DataTable();
        DataSet _ds = new DataSet();
        _ds.Tables.Add(dt);
        DataRow dr = null;

        //Define the Columns
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_followUpCode", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_description", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_category", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_criteria", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_lifeType", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_status", typeof(string)));
        dt.Columns.Add(new DataColumn("lblRaiseddate", typeof(string)));
        //dt.Columns.Add(new DataColumn("lblRaisedby", typeof(string)));
        dt.Columns.Add(new DataColumn("lblClosedDate", typeof(string)));
        //dt.Columns.Add(new DataColumn("lblClosedBy", typeof(string)));

        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable1_STP"] = _ds;
        //Bind the DataTable to the Grid
        gvRequirmentDetails.DataSource = _ds;
        gvRequirmentDetails.DataBind();

        DropDownList ddlfollowupcode = (DropDownList)gvRequirmentDetails_STP.Rows[0].Cells[0].FindControl("ddlfollowupcode_STP");
        TextBox txtLoadRider = (TextBox)gvRequirmentDetails_STP.Rows[0].Cells[1].FindControl("lblfollowupDiscp_STP");
        DropDownList ddlCategory = (DropDownList)gvRequirmentDetails_STP.Rows[0].Cells[2].FindControl("ddlCategory_STP");
        DropDownList ddlCriteria = (DropDownList)gvRequirmentDetails_STP.Rows[0].Cells[3].FindControl("ddlCriteria_STP");
        DropDownList ddlLifeType = (DropDownList)gvRequirmentDetails_STP.Rows[0].Cells[4].FindControl("ddlLifeType_STP");
        DropDownList ddlStatus = (DropDownList)gvRequirmentDetails_STP.Rows[0].Cells[5].FindControl("ddlStatus_STP");
        Label lblRaiseddate = (Label)gvRequirmentDetails_STP.Rows[0].Cells[6].FindControl("lblRaiseddate_STP");
        //Label lblRaisedby = (Label)gvRequirmentDetails.Rows[0].Cells[7].FindControl("lblRaisedby");
        Label lblClosedDate = (Label)gvRequirmentDetails_STP.Rows[0].Cells[8].FindControl("lblClosedDate_STP");
        // Label lblClosedBy = (Label)gvRequirmentDetails.Rows[0].Cells[9].FindControl("lblClosedBy");

        //Fill the DropDownList with Data
        //FillDropDownList(ddlLoadType, "LoadingCode", "Mst");
        btnReqaddrows.CssClass = "btn btn-primary";
        BindMasterDetails(ddlfollowupcode);
        BindMasterDetails(ddlCategory);
        BindMasterDetails(ddlCriteria);
        //BindMasterDetails(ddlLifeType);
        BindMasterDetails(ddlStatus);
    }

    private void SetPreviousData1_STP()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :SetPreviousData1_STP" + System.Environment.NewLine);
        int rowIndex = 0;
        if (ViewState["CurrentTable1_STP"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable1_STP"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {

                    DropDownList ddlfollowupcode = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[0].FindControl("ddlfollowupcode_STP");
                    TextBox lblfollowupDiscp = (TextBox)gvRequirmentDetails_STP.Rows[i].Cells[1].FindControl("lblfollowupDiscp_STP");
                    DropDownList ddlCategory = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[2].FindControl("ddlCategory_STP");
                    DropDownList ddlCriteria = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[3].FindControl("ddlCriteria_STP");
                    DropDownList ddlLifeType = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[4].FindControl("ddlLifeType_STP");
                    DropDownList ddlStatus = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[5].FindControl("ddlStatus_STP");
                    Label lblRaiseddate = (Label)gvRequirmentDetails_STP.Rows[i].Cells[6].FindControl("lblRaiseddate_STP");
                    Label lblClosedDate = (Label)gvRequirmentDetails_STP.Rows[i].Cells[7].FindControl("lblClosedDate_STP");
                    //Label lblClosedBy = (Label)gvRequirmentDetails.Rows[i].Cells[9].FindControl("lblClosedBy");

                    //Fill the DropDownList with Data
                    //FillDropDownList(ddlfollowupcode, "followupcode", "Mst");
                    BindMasterDetails(ddlfollowupcode);

                    if (i < dt.Rows.Count - 1)
                    {
                        ddlfollowupcode.ClearSelection();
                        ddlfollowupcode.Items.FindByText(dt.Rows[i]["REQ_followUpCode"].ToString()).Selected = true;
                        ddlfollowupcode.SelectedValue = dt.Rows[i]["REQ_followUpCode"].ToString();
                        lblfollowupDiscp.Text = dt.Rows[i]["REQ_description"].ToString();
                        ddlCategory.SelectedValue = dt.Rows[i]["REQ_category"].ToString();
                        ddlCriteria.SelectedValue = dt.Rows[i]["REQ_criteria"].ToString();
                        ddlLifeType.SelectedValue = dt.Rows[i]["REQ_lifeType"].ToString();
                        ddlStatus.SelectedValue = dt.Rows[i]["REQ_status"].ToString();
                        lblRaiseddate.Text = dt.Rows[i]["REQ_RaisedDate"].ToString();
                        //lblRaisedby.Text = dt.Rows[i]["lblRaisedby"].ToString();
                        lblClosedDate.Text = dt.Rows[i]["REQ_ClosedDate"].ToString();
                        //lblClosedBy.Text = dt.Rows[i]["lblClosedBy"].ToString();
                    }
                    rowIndex++;
                }
            }
            //LinkButton lnkReqRemoveRow = (LinkButton)gvRequirmentDetails.Rows[dt.Rows.Count - 1].Cells[6].FindControl("lnkReqRemoveRow");
            //lnkReqRemoveRow.CssClass = "";
            btnReqaddrows.CssClass = "btn btn-primary";
            gvRequirmentDetails_STP.CssClass = "table table-bordered table-striped";
            UpdatePanel1.Update();
        }
    }

    private void AddNewRowToGrid1_STP()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :AddNewRowToGrid1_STP" + System.Environment.NewLine);
        if (ViewState["CurrentTable1_STP"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1_STP"];
            DataRow drCurrentRow = null;

            DropDownList REQ_followUpCode;
            TextBox REQ_description;
            DropDownList REQ_category;
            DropDownList REQ_criteria;
            DropDownList REQ_lifeType;
            DropDownList REQ_status;
            Label lblRaiseddate;
            //Label lblRaisedby;
            Label lblClosedDate;
            //Label lblClosedBy;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                // drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;
                //add new row to DataTable
                dtCurrentTable.Rows.Add(drCurrentRow);

                //Store the current data to ViewState
                ViewState["CurrentTable1_STP"] = dtCurrentTable;
                DateTime _strGetdate = DateTime.Now;
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    //extract the DropDownList Selected Items
                    REQ_followUpCode = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[0].FindControl("ddlfollowupcode_STP");
                  
                    REQ_description = (TextBox)gvRequirmentDetails_STP.Rows[i].Cells[1].FindControl("lblfollowupDiscp_STP");
                    REQ_category = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[2].FindControl("ddlCategory_STP");
                    REQ_criteria = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[3].FindControl("ddlCriteria_STP");
                    REQ_lifeType = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[4].FindControl("ddlLifeType_STP");
                    REQ_status = (DropDownList)gvRequirmentDetails_STP.Rows[i].Cells[5].FindControl("ddlStatus_STP");
                    lblRaiseddate = (Label)gvRequirmentDetails_STP.Rows[i].Cells[6].FindControl("lblRaiseddate_STP");
                    lblClosedDate = (Label)gvRequirmentDetails_STP.Rows[i].Cells[7].FindControl("lblClosedDate_STP");
                    dtCurrentTable.Rows[i]["REQ_followUpCode"] = REQ_followUpCode.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_description"] = REQ_description.Text;
                    dtCurrentTable.Rows[i]["REQ_category"] = REQ_category.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_criteria"] = REQ_criteria.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_lifeType"] = REQ_lifeType.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_status"] = REQ_status.SelectedValue;
                    dtCurrentTable.Rows[i]["REQ_RaisedDate"] = lblRaiseddate.Text == "" ? Convert.ToString(_strGetdate) : lblRaiseddate.Text;
                    dtCurrentTable.Rows[i]["REQ_ClosedDate"] = lblClosedDate.Text == "" ? (object)DBNull.Value : lblClosedDate.Text;

                }
                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    REQ_followUpCode = (DropDownList)gvRequirmentDetails.Rows[i].Cells[0].FindControl("ddlfollowupcode");

                    NotSelectMerCheckbox(REQ_followUpCode.Text);
                    SelectMerCheckbox(REQ_followUpCode.Text);
                    if (REQ_followUpCode.Text == "MPN" || REQ_followUpCode.Text == "MPV")
                        break;
                }

                gvRequirmentDetails_STP.DataSource = dtCurrentTable;
                gvRequirmentDetails_STP.DataBind();

                UpdatePanel17_STP.Update();
            }
        }

        //Set Previous Data on Postbacks
        SetPreviousData1();
    }

    public void FillRUW_STP_Message(string strApplicationno)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillBankDetails" + System.Environment.NewLine);
        objComm.OnlineRUW_STP_GET(ref _ds, strApplicationno);
        BindRUW_STP_Message(_ds.Tables[0]);
    }

    public void BindRUW_STP_Message(DataTable _ds)
    {
        if (_ds.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(_ds.Rows[0]["ErrorMessage"])))
            {
                clsName = divRUW_STP.Attributes["class"].ToString();
                if (clsName != "col-md-12")
                {
                    divRUW_STP.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                }
                Logger.Info(strApplicationno + "STAG3:-Function Call BindRUW_STP_Message" + System.Environment.NewLine);
                string RUW_Messages = string.Empty;
                foreach (DataRow row in _ds.Rows)
                {
                    RUW_Messages += row["ErrorMessage"].ToString() + "<br />";
                }
                lblRUW_STP.Text = RUW_Messages;
            }
        }
    }

    public void FillRequirmentDetails_STP(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillRequirmentDetails" + System.Environment.NewLine);
        objComm.OnlineRequirmentDisplayDetails_STP_GET(ref _dsRequrmentDtls_STP, strApplicationno, ChannelType);
        BindRequirmentDetails_STP(_dsRequrmentDtls_STP.Tables[0]);
    }

    protected void ddlfollowupcode_STP_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ddlfollowupcode_STP_SelectedIndexChanged");
            DataSet _dsFollowupSubDtls = new DataSet();
            // Get the master DropDownList and its value
            DropDownList ddlEditEducationEstablishment_STP = (DropDownList)sender;
            string educationEstablishmentCode = ddlEditEducationEstablishment_STP.SelectedValue;
            //35.1 Begin of Changes; Sagar Thorave-[mfl00886]
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1_STP"];

            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
               DropDownList REQ_followUpCode = (DropDownList)gvRequirmentDetails.Rows[i].Cells[0].FindControl("ddlfollowupcode");

                NotSelectMerCheckbox(REQ_followUpCode.Text);
                SelectMerCheckbox(REQ_followUpCode.Text);
                if (REQ_followUpCode.Text == "MPN" || REQ_followUpCode.Text == "MPV")
                    break;
            }

            SelectMerCheckbox(educationEstablishmentCode);
            //35.1 End of Changes; Sagar Thorave-[mfl00886]
            // Get the GridViewRow in which this master DropDownList exists
            GridViewRow row = (GridViewRow)ddlEditEducationEstablishment_STP.NamingContainer;
            DropDownList ddlEditSchool = (DropDownList)row.FindControl("ddlfollowupcode_STP");
            FillSubMasterDetails(ref _dsFollowupSubDtls, educationEstablishmentCode, "followupcode", "");

            if (_dsFollowupSubDtls.Tables.Count > 0 && _dsFollowupSubDtls.Tables[0].Rows.Count > 0)
            {
                TextBox lblfollowupDiscp = (TextBox)row.FindControl("lblfollowupDiscp_STP");
                lblfollowupDiscp.Text = _dsFollowupSubDtls.Tables[0].Rows[0]["Followupdescp"].ToString();

                DropDownList ddlCategory = (DropDownList)row.FindControl("ddlCategory_STP");
                ddlCategory.SelectedValue = _dsFollowupSubDtls.Tables[0].Rows[0]["Category"].ToString();


                DropDownList ddlCriteria = (DropDownList)row.FindControl("ddlCriteria_STP");
                ddlCriteria.SelectedValue = _dsFollowupSubDtls.Tables[0].Rows[0]["Criteria"].ToString();

                DropDownList ddlLifeType = (DropDownList)row.FindControl("ddlLifeType_STP");
                ddlLifeType.SelectedValue = _dsFollowupSubDtls.Tables[0].Rows[0]["LifeType"].ToString();

                if (_dsFollowupSubDtls.Tables[0].Rows[0]["Criteria"].ToString() == "Text to be Appended")
                {
                    //lblfollowupDiscp.Enabled = true;
                    lblfollowupDiscp.CssClass = "form-control";
                }
                else
                {
                    lblfollowupDiscp.CssClass = "form-control lblLable";
                }
                UpdatePanel1.Update();
            }
        }
        catch (Exception Error)
        {
            objcomm.SaveErrorLogs("", "Failed", "UWPortfolio", "ddlfollowupcode_STP_SelectedIndexChanged", "UWPortfolio", "E-Error", "", "", Error.ToString());
        }
    }

    protected void gvRequirmentDetails_STP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int ReqCount = Convert.ToInt16(Session["ReqCount"]);
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :gvRequirmentDetails_STP_RowDataBound");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlfollowupcode = (DropDownList)e.Row.FindControl("ddlfollowupcode_STP");
                BindMasterDetails(ddlfollowupcode);

                DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlfollowupcode_STP");
                BindMasterDetails(ddl1);

                DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory_STP");
                BindMasterDetails(ddlCategory);

                DropDownList ddlCriteria = (DropDownList)e.Row.FindControl("ddlCriteria_STP");
                BindMasterDetails(ddlCriteria);

                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus_STP");
                BindMasterDetails(ddlStatus);
                //FillDropDownList(ddl1, "followupcode", "Mst");
                _Dt = (DataTable)ViewState["CurrentTable1_STP"];
                if (_Dt.Rows.Count >= 1)
                {
                    //LinkButton lb = e.Row.FindControl("lnkReqRemoveRow") as LinkButton;
                    //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);

                    //Image ing = e.Row.FindControl("Image8") as Image;

                    //DropDownList ddlfollowupcode = (DropDownList)e.Row.FindControl("ddlfollowupcode");
                    ddlfollowupcode.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_followUpCode"].ToString();
                    //35.1 Begin of Changes; Sagar Thorave-[mfl00886]
                    SelectMerCheckbox(ddlfollowupcode.SelectedValue);
                    //35.1 End of Changes; Sagar Thorave-[mfl00886]
                    TextBox lblfollowupDiscp = (TextBox)e.Row.FindControl("lblfollowupDiscp_STP");
                    lblfollowupDiscp.Text = _Dt.Rows[e.Row.RowIndex]["REQ_description"].ToString();

                    //DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                    ddlCategory.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_category"].ToString();

                    //DropDownList ddlCriteria = (DropDownList)e.Row.FindControl("ddlCriteria");
                    ddlCriteria.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_criteria"].ToString();

                    DropDownList ddlLifeType = (DropDownList)e.Row.FindControl("ddlLifeType_STP");
                    ddlLifeType.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_lifeType"].ToString();

                    Label lblRaiseddate = (Label)e.Row.FindControl("lblRaiseddate_STP");
                    lblRaiseddate.Text = _Dt.Rows[e.Row.RowIndex]["REQ_RaisedDate"].ToString();

                    //Label lblRaisedby = (Label)e.Row.FindControl("lblRaisedby ");
                    //lblRaisedby.Text = _Dt.Rows[e.Row.RowIndex]["lblRaisedby "].ToString();

                    Label lblClosedDate = (Label)e.Row.FindControl("lblClosedDate_STP");
                    lblClosedDate.Text = _Dt.Rows[e.Row.RowIndex]["REQ_ClosedDate"].ToString();

                    //Label lblClosedBy = (Label)e.Row.FindControl("lblClosedBy");
                    //lblClosedBy.Text = _Dt.Rows[e.Row.RowIndex]["lblClosedBy"].ToString();

                    if (ddlCategory.SelectedValue == "Medical")
                    {
                        DataSet dsREq = new DataSet();
                        objComm.GET_MedicalReportoShow(ref dsREq, strApplicationno);
                        if (dsREq.Tables.Count > 0 && dsREq.Tables[0].Rows.Count > 0)
                        {
                            ddlRequirementMedicalReason_STP.SelectedValue = Convert.ToString(dsREq.Tables[0].Rows[0]["ReqMedicalReason"]);
                        }
                        else
                        {
                            ddlRequirementMedicalReason_STP.SelectedValue = "9";
                        }
                    }

                    //DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                    ddlStatus.SelectedValue = _Dt.Rows[e.Row.RowIndex]["REQ_status"].ToString();
                }

            }
        }
        catch (Exception Error)
        {
            objcomm.SaveErrorLogs("", "Failed", "UWPortfolio", "gvRequirmentDetails_STP_RowDataBound", "UWPortfolio", "E-Error", "", "", Error.ToString());
        }
    }

    public void BindRequirmentDetails_STP(DataTable _dtRequrmentDtls_STP)
    {
        _dtRequrmentDtls_STP.Columns.RemoveAt(0);
        if (_dtRequrmentDtls_STP.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(_dtRequrmentDtls_STP.Rows[0]["REQ_Followupcode"])))
              {
                clsName = divRequirementDetails_STP.Attributes["class"].ToString();
                if (clsName != "col-md-12")
                {
                    divRequirementDetails_STP.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                }

                Logger.Info(strApplicationno + "STAG3:-Function Call BindRequirmentDetails_STP" + System.Environment.NewLine);
                ViewState["CurrentTable1_STP"] = _dtRequrmentDtls_STP.Copy();
                //ViewState["CurrentTable1"] = _dtRequrmentDtls;
                Session["ReqCount_STP"] = _dtRequrmentDtls_STP.Rows.Count;
                gvRequirmentDetails_STP.DataSource = _dtRequrmentDtls_STP;
                gvRequirmentDetails_STP.DataBind();
            }


        }
    }

    protected void btnRequirmentDtlsSave_STP_Click(object sender, EventArgs e)
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnRequirmentDtlsSave_STP_Click");
        string _strfollowupcode_stp = string.Empty;
        string _strfollowupdiscp_stp = string.Empty;
        string _strfollowupcategory_stp = string.Empty;
        string _strfollowupcriteria_stp = string.Empty;
        string _strfollowuplifetype_stp = string.Empty;
        string _strfollowupstatus_stp = string.Empty;
        int strFollowupResult_stp = 0;
        bool _FollowupStatus_stp = false;
        bool _strFolloUPflag_stp = true;
        lblErrorRequirementDetailBody.Text = lblErrorreqdtls.Text = string.Empty;
        // objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        foreach (GridViewRow rowfollowup in gvRequirmentDetails_STP.Rows)
        {
            DropDownList ddlfollowupcode_stp = rowfollowup.FindControl("ddlfollowupcode_STP") as DropDownList;
            DropDownList ddlCategory_stp = rowfollowup.FindControl("ddlCategory_STP") as DropDownList;
            DropDownList ddlCriteria_stp = rowfollowup.FindControl("ddlCriteria_STP") as DropDownList;
            DropDownList ddlStatus_stp = rowfollowup.FindControl("ddlStatus_STP") as DropDownList;
            DropDownList ddlLifeType_stp = rowfollowup.FindControl("ddlLifeType_STP") as DropDownList;
            TextBox lblfollowupDiscp_stp = rowfollowup.FindControl("lblfollowupDiscp_STP") as TextBox;
            _strfollowupcode_stp = ddlfollowupcode_stp.SelectedValue;
            _strfollowupcategory_stp = ddlCategory_stp.SelectedValue;
            _strfollowupcriteria_stp = ddlCriteria_stp.SelectedValue;
            _strfollowuplifetype_stp = ddlLifeType_stp.SelectedValue;
            _strfollowupstatus_stp = ddlStatus_stp.SelectedValue;
            _strfollowupdiscp_stp = lblfollowupDiscp_stp.Text;
            if (_strfollowupstatus_stp == "O")
            {
                _FollowupStatus_stp = true;
            }
            objBuss.FollowupDetails_Save(strChannelType, strApplicationno, _strfollowupcode_stp, _strfollowupdiscp_stp, _strfollowupcategory_stp, _strfollowupcriteria_stp, "", objChangeObj.userLoginDetails._UserID, _strfollowupstatus_stp,
                _strfollowupcategory_stp, _strfollowuplifetype_stp, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, strChannelType, strApplicationno, ref strFollowupResult_stp);
            if (strFollowupResult_stp == 0)
            {
                _strFolloUPflag_stp = false;
            }
        }
        if (_strFolloUPflag_stp)
        {
            chkReqDtls.Checked = false;
            lblErrorreqdtls.Text = "Requirment details save successfully";
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "FOLLOWUP", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            if (strLApushErrorCode == 0)
            {
                if (_FollowupStatus_stp == true)
                {
                    //objCommonObj = (CommonObject)Session["objCommonObj"];
                    // strUserId = objCommonObj._Bpmuserdetails._UserID;
                    // strApplicationno = objCommonObj._ApplicationNo;
                    //strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
                    objChangeObj = (ChangeValue)Session["objLoginObj"];
                    strUserGroup = objChangeObj.userLoginDetails._UserGroup;
                    //strAppstatusKey = (strUserGroup == "UW") ? "UR" : "DR";
                    strAppstatusKey = (strUserGroup == "UW") ? "UR" : "DR";
                    objComm.OnlineApplicationchangeStatus(strApplicationno, objChangeObj.userLoginDetails._UserID, strAppstatusKey, "", ref strFollowupResult_stp);
                }
                chkReqDtls.Checked = false;
                btnReqaddrows.CssClass = "btn-primary HideControl";
                lblErrorreqdtls.Text = "Requirment Details Updated in Life Asia successfully";
                lblErrorRequirementDetailBody.Text = "Requirment Details Updated in Life Asia successfully";
                FillRequirmentDetails(strApplicationno, strChannelType);
            }
            else
            {
                chkReqDtls.Checked = false;
                lblErrorreqdtls.Text = "Requirment Details Not Updated in Life Asia,Please Contact system admin";
                lblErrorRequirementDetailBody.Text = strLApushStatus;
            }
        }
        else
        {
            chkReqDtls.Checked = false;
            lblErrorRequirementDetailBody.Text = "Requirment details Not Updated in DataBase";
            lblErrorreqdtls.Text = "Requirment details Not Save CLICK to know more";
        }
    }

    protected void btnReqaddrows_STP_Click(object sender, EventArgs e)
    {
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "REQ_ADD_NEW_ROW", ref intTrackingRet);
            /*end here*/
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnReqaddrows_Click");
            if (gvRequirmentDetails.Rows.Count <= 0)
            {
                SetInitialRequirmentGrid_STP();
            }
            else
            {
                AddNewRowToGrid1_STP();
            }
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
        }
        catch (Exception)
        {

        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }

    }

    protected void lnkReqDtlsRefresh_STP_Click(object sender, EventArgs e)
    {
        chkReqDtls.Checked = false;
        //objCommonObj = (CommonObject)Session["objCommonObj"];
        FillRequirmentDetails_STP(strApplicationno, strChannelType);
    }

    protected void ddlCommonStatus_STP_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in gvRequirmentDetails_STP.Rows)
        {
            DropDownList ddlStatus = gvRow.FindControl("ddlStatus_STP") as DropDownList;
            ddlStatus.SelectedValue = ddlCommonStatus_STP.SelectedValue;
        }
        clsName = gvRequirmentDetails_STP.CssClass.Replace("lblLable", "");
        gvRequirmentDetails_STP.CssClass = clsName;
        ddlCommonStatus_STP.Enabled = true;
        clsName = ddlCommonStatus_STP.CssClass.Replace("lblLable", "");
        ddlCommonStatus_STP.CssClass = clsName;

    }

    protected void btnCommonEvent_STP_Command(object sender, CommandEventArgs e)
    {
        //if (Session["objLoginObj"] != null)
        //{
        //    objChangeObj = (ChangeValue)Session["objLoginObj"];
        //    int intRetVal = -1;
        //    objcomm.MaintainApplicationLog(strApplicationno, e.CommandName, string.Empty, objChangeObj.userLoginDetails._UserID, ref intRetVal);
        //}
    }

    protected void btnSearchCode_STP_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('REQ_SearchCode.aspx','mywindow','menubar=1,resizable=1,width=900,height=600');", true);
    }

    protected void btnMedDataentry_STP_Click1(object sender, EventArgs e)
    {

        string url = "MedicalDataEntry.aspx?qsAppNo=" + strApplicationno + "&qsPolicyNo=" + strPolicyNo;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
    }

    // 47.1 End of Changes; Jayendra  - [Webashlar01]
    //added by suraj on 14 FEB 2022 for CR 30589-Risk category
    protected void BindRiskCategory(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dt.Rows[0]["FINAL_CATEGORY"].ToString()))
            {
                txtriskcat.Text = Convert.ToString(dt.Rows[0]["FINAL_CATEGORY"] + " " + "Risk");
                //txtriskcat.Text = Convert.ToString(dt.Rows[0]["FINAL_CATEGORY"]);
                txtriskcat.ForeColor = System.Drawing.Color.White;
                if (txtriskcat.Text == "Very High Risk")
                {
                    txtriskcat.BackColor = System.Drawing.Color.Red;
                }
                else if (txtriskcat.Text == "High Risk")
                {
                    txtriskcat.BackColor = System.Drawing.Color.Orange;
                }
                else if (txtriskcat.Text == "Medium Risk")
                {
                    txtriskcat.BackColor = System.Drawing.Color.Yellow;
                    txtriskcat.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    txtriskcat.BackColor = System.Drawing.Color.Green;
                    txtriskcat.ForeColor = System.Drawing.Color.White;
                }
            }
            else
            {
                txtriskcat.Text = "Low Risk";
                txtriskcat.BackColor = System.Drawing.Color.Green;
                txtriskcat.ForeColor = System.Drawing.Color.White;
            }
            if (!string.IsNullOrEmpty(dt.Rows[0]["RiskScore"].ToString()))
            {

                txtSaralRiskScore.Text = Convert.ToString(dt.Rows[0]["RiskScore"]);
                if (Convert.ToInt32(txtSaralRiskScore.Text) >= 75)
                {

                    txtSaralRiskScore.BackColor = System.Drawing.Color.Red;
                    txtSaralRiskScore.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    txtSaralRiskScore.BackColor = System.Drawing.Color.Green;
                    txtSaralRiskScore.ForeColor = System.Drawing.Color.White;
                }
            }
            else
            {
                txtSaralRiskScore.Text = "60";
                txtSaralRiskScore.BackColor = System.Drawing.Color.Green;
                txtSaralRiskScore.ForeColor = System.Drawing.Color.White;
            }
        }
        else
        {
            txtriskcat.Text = "Low Risk";
            txtriskcat.BackColor = System.Drawing.Color.Green;
            txtriskcat.ForeColor = System.Drawing.Color.White;
            txtSaralRiskScore.Text = "60";
            txtSaralRiskScore.BackColor = System.Drawing.Color.Green;
            txtSaralRiskScore.ForeColor = System.Drawing.Color.White;
        }
    }
    //end
    //40.1  Begin of changes Sushant Devkate- mfl00905
    public void GetcheckISNRICustomer() 
    {     
        if (new Commfun().GetcheckISNRICustomer(strApplicationno))
        {
            ddlApplicationDetailsProposalType.SelectedValue = "NRI";
            ddlApplicationDetailsProposalType_SelectedIndexChanged("ddlApplicationDetailsProposalType", null);
        }
        else 
        {
            //43.1 Begin of Changes; Sagar Thorave-[mfl00886]
            SetSpecialInsurance();
            //43.1 End of Changes; Sagar Thorave-[mfl00886]
        }
    }
    //40.1 End of changes Sushant Devkate- mfl00905

    public void FillAcceptanceMaster(ref DataSet _dsFollowuo)
    {
        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillSubMasterDetails" + System.Environment.NewLine);
        objComm.FeatchAcceptanemaster(ref _dsFollowuo);
    }
}
