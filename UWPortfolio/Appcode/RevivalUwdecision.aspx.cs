using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using System.Web.Configuration;
using System.Web.Caching;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.ServiceModel.Activation;
using System.Text;
using System.IO.Compression;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

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
*********************************************************************************************************************************1
* **********************************************************************************************************************************
COMMENT ID:24
COMMENTOR NAME : AKSHADA WAGH
METHODE/EVENT:
REMARK: TSAR MSAR REVIVAL CHANGES 
DateTime :19MARCH2020

*********************************************************************************************************************************
* /
//*******************************************************************************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//*******************************************************************************************************************************      
//*                  I N F O R M A T I O N                                       
//******************************************************************************************************************************* 
// Sr. No.              : 26
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bibekananda Nanda [1103055]               
// BRD/CR/Codesk No/Win : ///27626    
// Date Of Creation     : 12.05.2020            
// Description          : 1.All the PAN card details to be saved and displayed after NSDL validation)
//*********************************************************************************************************************************/
//*******************************************************************************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//*******************************************************************************************************************************      
//*                  I N F O R M A T I O N                                       
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
//*                      FUTURE GENERALI INDIA                        *    
//*******************************************************************************************************************************      
//*                  I N F O R M A T I O N                                       
//******************************************************************************************************************************* 
// Sr. No.              : 28
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Brijesh Pandey              
// BRD/CR/Codesk No/Win : ///29084    
// Date Of Creation     : 08-06-2021.03.2020            
// Description          : Add Relation with Staff field against Is Staff flag 
//*********************************************************************************************************************************/
//*                      FUTURE GENERALI INDIA                        *    
//*******************************************************************************************************************************      
//*                  I N F O R M A T I O N                                       
//******************************************************************************************************************************* 
// Sr. No.              : 29
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave            
// BRD/CR/Codesk No/Win : CR-3387  
// Date Of Creation     : 22-06-2022            
// Description          : AML risk categorisation in Life Asia
//******************************************************************************************************************************* 

public partial class Appcode_Default : System.Web.UI.Page
{
    //ServiceQuote.ServiceClient objService = new ServiceQuote.ServiceClient();
    WCFGenerateInputOutput.WCFGenerateInputOutputClient clientService = new WCFGenerateInputOutput.WCFGenerateInputOutputClient();
    WCFHitMQ.WCFHitMQClient mqHit = new WCFHitMQ.WCFHitMQClient();

    TSAR.ServiceClient CalTSAR = new TSAR.ServiceClient();
    string MaxTSAR = string.Empty;
    string OwnerClientID = string.Empty;
    string TSAR = string.Empty;
    string LAClientID = string.Empty;
    long Count = 0;

    string PayerClientId = string.Empty;
    string ProposerClientID = string.Empty;
    string KeyWord = string.Empty;
    string RequestDetails = string.Empty;
    string ProductCode = string.Empty;
    decimal OtherPolicyDetails = 0;
    string PartnerId = "BMP";
    string PolicyNumber = string.Empty;
    string Age = string.Empty;
    string LAName = string.Empty;
    string Premium = string.Empty;

    string strApplicationno = string.Empty;
    string strChannelType = string.Empty;
    string strUserId = string.Empty;
    string strUWmode = string.Empty;
    string strUserGroup = string.Empty;
    string strOptinselected = string.Empty;
    string strAppstatusKey = string.Empty;
    string strPolicyNo = string.Empty;
    string strConsentRespons = string.Empty;
    string strClickBucketLink = string.Empty;//Added by Suraj on 24 APRIL 2018 for Close file review

    DataSet _dsTMAX = new DataSet();
    DataSet _dsPremium = new DataSet();
    Commfun objComm = new Commfun();
    BussLayer objBuss = new BussLayer();
    DataSet _ds = new DataSet();
    DataSet _ds1 = new DataSet();
    DataSet dsMSAR = new DataSet();
    DataTable _Dt = new DataTable();
    DataSet _dsAgentdetails = new DataSet();
    DataSet _dsBank = new DataSet();
    DataSet _dsPrevPol = new DataSet();
    DataSet _dsJournal = new DataSet();
    DataSet _dsFollowuo = new DataSet();
    DataSet _dsProdontrol = new DataSet();
    DataSet _dsUWDisplayOption = new DataSet();
    DataSet _dsAppdtls = new DataSet();
    DataSet __dsOnlineOffline = new DataSet();
    DataSet _dsPandtls = new DataSet();
    DataSet _dsTsarMsarDtls = new DataSet();
    DataSet _dsSTPDetails = new DataSet();
    DataSet _dsProdDtls = new DataSet();
    DataTable _dsRiderDtls = new DataTable();
    DataSet _dsFundsDtls = new DataSet();
    DataSet _dscommentDtls = new DataSet();
    DataSet _dsPymentsDtls = new DataSet();
    DataSet _dsRequrmentDtls = new DataSet();
    DataSet _dsReceiptDtls = new DataSet();
    DataSet _dsLoadingDtls = new DataSet();
    DataSet _dsReportDtls = new DataSet();
    DataSet _dsUWMaster = new DataSet();
    DataSet _dsPageData = new DataSet();
    //DataSet _dsRevivalData = new DataSet();
    DataSet _dsDashcount = null;
    DataSet _dsQuestion = new DataSet();
    string response = "";
    UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
    CommonObject objCommonObj = new CommonObject();
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
    bool IsCombi = false;//added by suraj for combi product
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["objLoginObj"] == null)
        {
            Response.Redirect("~/Entry.aspx");
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

            //Added by Akshay testing code
            //FillPremiumDetails(strApplicationno, strPolicyNo);

            //if (Request.QueryString["qsClickBucketLink"] != null)
            //{
            //    strClickBucketLink = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsClickBucketLink"]);
            //}
            //end
            if (Request.QueryString["qsUserLimit"] != null)
            {
                hdnUserLimit.Value = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsUserLimit"]);
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

            if (!IsPostBack)
            {
                //Begin of changes by rupaliezytek
                getFGData();
                //End  of changes by rupaliezytek
                string strPreviousPage = "";
                if (Request.UrlReferrer != null)
                {
                    strPreviousPage = Request.UrlReferrer.Segments[Request.UrlReferrer.Segments.Length - 1];
                }
                if (strPreviousPage == "")
                {
                    Response.Redirect("~/Entry.aspx");
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



                        Logger.Info(strApplicationno + " STAG2:-O" + System.Environment.NewLine);
                        FillProductControlDetails(strApplicationno, strChannelType);
                        Logger.Info(strApplicationno + " STAG2:-P" + System.Environment.NewLine);


                        //############################## To Fill all dropdownlist,checkboxlist added by akshay ###################################### 
                        FillMasterDetails(ref _dsUWMaster);
                        #endregion Fill master details end.
                        //############################## END ######################################

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
                        //############# FG CODE TO DETAILS OF PANELS TO BIND Added by Akshay ######################################################
                        objcomm.LoadUWsaralPageDetails(ref _dsPageData, strApplicationno, strChannelType, strUserId);
                        //############# FG CODE TO DETAILS OF PANELS TO BIND ######################################################

                        Logger.Info(strApplicationno + " STAG2:-T" + System.Environment.NewLine);
                        if (_dsPageData != null && _dsPageData.Tables.Count > 0)
                        {
                            //############# CODE TO DETAILS OF PANELS TO BIND Revival Cases Added by Akshay ######################################################
                            if (_dsPageData.Tables[0].Rows[0]["FE_STATUS"].ToString() == "Revival_Cases")
                            {
                                FillProductDetails(strApplicationno, strChannelType);
                                //Procedure Revival cases
                                objcomm.LoadUWsaralPageDetails_RevivalCases(ref _dsPageData, strApplicationno, strChannelType, strUserId, strPolicyNo);
                                //######################### CODE TO BIND PANEL REQUIRED FOR REVIVAL CASES ####################################################
                                FillUwDecisionRevival(_dsPageData);


                                //######################### CODE TO BIND PANEL REQUIRED FOR REVIVAL CASES ####################################################
                            }
                            //############# CODE TO DETAILS OF PANELS TO BIND Revival Cases ######################################################
                            else
                            {
                                Logger.Info(strApplicationno + " STAG2:-U" + System.Environment.NewLine);
                                //######################### FG CODE TO BIND PANEL REQUIRED FOR NON REVIVAL CASES Added by Akshay ####################################################

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

                                FillUwDecision(_dsPageData);
                                //27.1 Start ENY Score  Shyam Patil
                                ENYScore(strApplicationno);
                                //27.1 End ENY Score  Shyam Patil



                                //######################### FG CODE TO BIND PANEL REQUIRED FOR NON REVIVAL CASES ####################################################
                                Logger.Info(strApplicationno + " STAG2:-W" + System.Environment.NewLine);
                            }


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
                        divUWReason.Visible = false;
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
                    //intTrackingRet 
                }
                else
                {
                    Response.Redirect("../Entry.aspx");
                }
                System.Web.Script.Serialization.JavaScriptSerializer objSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                objSerializer.Serialize(objOldMasterComparision);
                hdnOldValue.Value = objSerializer.Serialize(objOldMasterComparision); //Commfun.GetXMLFromObject(objOldMasterComparision);
                                                                                      /*added by shri on 05 dec 17 to add application log*/
                int intRet = -1;
                //Added parameter "SARAL" to change status SP for enrty in mis report  table from which application
                //objcomm.ManageApplicationStatus(strApplicationno, "OPEN", string.Empty, strUserId, string.Empty,"SARALUW", ref intRet);
                /*end here*/
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
            //        Response.Redirect("../Entry.aspx");
            //    }

            //}            
            /*ID:4 START*/
            //ManageContent();
            /*ID:4 END*/

            Logger.Info(strApplicationno + " STAG2:-page load end" + System.Environment.NewLine);
        }

        catch (Exception ex)
        {

        }

    }

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

        //on post        
        Master.masterCallEvent += new EventHandler(btnServerValidation);
        Master.masterCallEvent += new EventHandler(btnSaveToDatabase);
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
            decimal _AmountDue = 0;
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

    //########################################### added by Akshay Revival cases Start ###############################################
    public void FillPremiumDetails(string strApplicationno, string strPolicyNo)
    {
        objcomm.UWSaralPolicyPremDetails_GET(ref _ds, strApplicationno, strPolicyNo);
        objcomm.UWReinstatementReducedSumAssured_GET(ref _ds1, strApplicationno, strPolicyNo);
        BindPremiumDetails(_ds.Tables[0]);
        // txtChannel.Text = strChannelType;

        //DataRow dr4 = _ds.Tables[3].Rows[0];
        //txtChannel.Text = dr4["Channel"].ToString().Trim();  //_ds.Tables[3].Rows[0]["Channel"].ToString(); 
    }

    public void BindPremiumDetails(DataTable _ds)
    {
        if (_ds.Rows.Count > 0)
        {
            txtPolno1.Text = PolicyNumber = hdnPolNo.Value = _ds.Rows[0]["PolicyNumber"].ToString();
            txtPremPayingStatus.Text = _ds.Rows[0]["PremiumPayingStatus"].ToString();
            hdnPremPayingStatus.Value = _ds.Rows[0]["PremiumPStatus"].ToString();
            hdnModilenumber.Value = _ds.Rows[0]["MobileNumber"].ToString();
            hdnEmail.Value = _ds.Rows[0]["Email"].ToString();
            hdfnBankName.Value= _ds.Rows[0]["BankName"].ToString();
            hdfnBankAcctNumber.Value= _ds.Rows[0]["AccountNumber"].ToString();
            

            try
            {
                txtRCD.Text = Convert.ToDateTime(_ds.Rows[0]["RCD"].ToString().Trim()).ToString("dd-MMM-yyyy");
                if (txtRCD.Text == "01-Jan-1900")
                {
                    txtRCD.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                txtRCD.Text = string.Empty;
            }

            try
            {
                txtPremCessDate.Text = Convert.ToDateTime(_ds.Rows[0]["PremiumCessationDate"].ToString().Trim()).ToString("dd-MMM-yyyy");
                if (txtPremCessDate.Text == "01-Jan-1900")
                {
                    txtPremCessDate.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                txtPremCessDate.Text = string.Empty;
            }

            try
            {
                txtAgeing.Text = _ds.Rows[0]["AgeingUWSaral"].ToString().Trim();
            }
            catch (Exception)
            {
                //			int zero = 0;
                txtAgeing.Text = "0";// Convert.ToInt16(zero).ToString().Trim();
            }

            try
            {
                OwnerClientID = _ds.Rows[0]["OwnerClientID"].ToString().Trim();
            }
            catch (Exception)
            {
                OwnerClientID = "0";
            }

            try
            {
                lblBOEComments.Text = _ds.Rows[0]["BOEComments"].ToString().Trim();
            }
            catch (Exception)
            {
                lblBOEComments.Text = "";
            }

            try
            {
                txtPTD.Text = Convert.ToDateTime(_ds.Rows[0]["PTD"].ToString().Trim()).ToString("dd-MMM-yyyy");
                if (txtPTD.Text == "01-Jan-1900")
                {
                    txtPTD.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                txtPTD.Text = string.Empty;
            }

            try
            {
                txtRiskCessDate.Text = Convert.ToDateTime(_ds.Rows[0]["RiskCessationDate"].ToString().Trim()).ToString("dd-MMM-yyyy");
                if (txtRiskCessDate.Text == "01-Jan-1900")
                {
                    txtRiskCessDate.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                txtRiskCessDate.Text = string.Empty;
            }

            try
            {
                txtBonus.Text = _ds.Rows[0]["Bouns"].ToString();
            }
            catch (Exception)
            {
                //int zero = 0;
                txtBonus.Text = "0";// Convert.ToInt16(zero).ToString();
            }

            //PremiumPStatus
            txtPolicyStatus.Text = _ds.Rows[0]["PolicyStatus"].ToString();
            if (txtPolicyStatus.Text == "PU")
            {
                if (_ds1.Tables[0].Rows.Count != 0)
                {
                    DataRow dr1 = _ds1.Tables[0].Rows[0];
                    if (string.IsNullOrEmpty(dr1["SUMINS"].ToString().Trim()))
                    {
                        txtRedSumAssured.Text = "0";
                        txtSumassure1.Text = "0";
                    }
                    else
                    {
                        txtRedSumAssured.Text = _ds.Rows[0]["SumAssured"].ToString();  //dr1["SumAssured"].ToString().Trim();
                        txtSumassure1.Text = dr1["SUMINS"].ToString().Trim();
                    }
                }
                else
                {
                    txtRedSumAssured.Text = _ds.Rows[0]["SumAssured"].ToString();
                    txtSumassure1.Text = "0";
                }

                //_ds1.Tables[0].Columns["SUMINS"].ToString();
            }
            else
            {
                txtRedSumAssured.Text = "0";
                txtSumassure1.Text = _ds.Rows[0]["SumAssured"].ToString();
            }
            hdnPolicyStatus.Value = _ds.Rows[0]["PolicyStatus"].ToString();
            txtTypeProd.Text = _ds.Rows[0]["TypeOfProduct"].ToString();
            txtBranch.Text = _ds.Rows[0]["Branch"].ToString();
            txtFrequency.Text = _ds.Rows[0]["Frequency"].ToString();

            //################################ Calculate Annualised Premium ############################
            int frequency = Convert.ToInt32(_ds.Rows[0]["FrequencyCd"].ToString());
            Decimal premiumAmount = Convert.ToDecimal(_ds.Rows[0]["InstallementPremium"].ToString());
            var AnnualisedPremium = (frequency * premiumAmount);
            txtAnnualPrem.Text = Convert.ToString(AnnualisedPremium);
            //############################### END ######################################################
            //ReciptDate
            if (string.IsNullOrEmpty(_ds.Rows[0]["ReciptDate"].ToString().Trim()))
            {
                HdnReciptdate.Value = DateTime.Today.ToString("yyyyMMdd");
            }
            else
            {
                HdnReciptdate.Value = _ds.Rows[0]["ReciptDate"].ToString().Trim();
            }

            //ReciptAmt
            if (string.IsNullOrEmpty(_ds.Rows[0]["ReciptAmt"].ToString().Trim()))
            {
                hdnReciptAmt.Value = "0";
                txtPremPaid.Text = "0";
            }
            else
            {
                hdnReciptAmt.Value = _ds.Rows[0]["ReciptAmt"].ToString().Trim();
                txtPremPaid.Text = _ds.Rows[0]["ReciptAmt"].ToString().Trim();
            }

            //txtPremPaid.Text = _ds.Rows[0]["AdjustmentofSuspense"].ToString().Trim();
            CallTotalRequiredPremium(strPolicyNo);
            CallOWLoan(strPolicyNo);
            CallTotalInterestAccured(strPolicyNo);

            txtRevivalEligibility.Text = _ds.Rows[0]["RevivalCampaignEligibility"].ToString();
            txtDGHRequired.Text = _ds.Rows[0]["DGHRequired"].ToString();
            txtPolicyStatusUW.Text = _ds.Rows[0]["PolicyStatus"].ToString();
            txtProname.Text = _ds.Rows[0]["ProductName"].ToString();
            txtProname1.Text = _ds.Rows[0]["ProductName"].ToString();
            txtLANAME.Text = _ds.Rows[0]["LAGIVNAME"].ToString();
            txtOwnerName.Text = _ds.Rows[0]["OGIVNAME"].ToString();
            txtOwnerClientID.Text = _ds.Rows[0]["OwnerClientID"].ToString();
            txtLAClientID.Text = _ds.Rows[0]["LAClientID"].ToString();
            txtAgentCode.Text = _ds.Rows[0]["AgentCode"].ToString();
            txtAgentName1.Text = _ds.Rows[0]["AgentFirstName"].ToString();
            txtAgentStatus.Text = _ds.Rows[0]["AgentStatus"].ToString();
            txtSMName.Text = _ds.Rows[0]["SMName"].ToString();
            txtSMMobile.Text = _ds.Rows[0]["SMMobile"].ToString();
            LAClientID = _ds.Rows[0]["LAClientID"].ToString().Trim();
            PayerClientId = _ds.Rows[0]["OwnerClientID"].ToString().Trim();
            ProposerClientID = _ds.Rows[0]["OwnerClientID"].ToString().Trim();
            ProductCode = _ds.Rows[0]["ProductCode"].ToString().Trim();
            LAName = _ds.Rows[0]["LASALUT"].ToString().Trim() + " " + _ds.Rows[0]["LAGIVNAME"].ToString().Trim() + " " + _ds.Rows[0]["LASURNAME"].ToString().Trim();
            Premium = _ds.Rows[0]["InstallementPremium"].ToString().Trim();
            hdnRecoveryPolicyDebt.Value = _ds.Rows[0]["RecoveryofPolicyDebt"].ToString().Trim();
            hdnAdjustmentSuspense.Value = _ds.Rows[0]["AdjustmentofSuspense"].ToString().Trim();
            HdnApplicationNumber.Value = _ds.Rows[0]["ApplicationNo"].ToString().Trim();
            //1.1 Start of Changes for Revival 18 / 04 / 2020
            hdnLAFullname.Value = _ds.Rows[0]["LASALUT"].ToString().Trim() + " " + _ds.Rows[0]["LAGIVNAME"].ToString().Trim() + " " + _ds.Rows[0]["LASURNAME"].ToString().Trim();
            //1.1 End of Changes for Revival 18 / 04 / 2020



            if (string.IsNullOrEmpty(_ds.Rows[0]["PTDMonths"].ToString().Trim()))
            {
                HdnPTDMonths.Value = "0";
            }
            else
            {
                HdnPTDMonths.Value = _ds.Rows[0]["PTDMonths"].ToString().Trim();
            }

            if (string.IsNullOrEmpty(_ds.Rows[0]["ProdCodeMonths"].ToString().Trim()))
            {
                HdnProdCodeMonths.Value = "0";
            }
            else
            {
                HdnProdCodeMonths.Value = _ds.Rows[0]["ProdCodeMonths"].ToString().Trim();
            }

            Count = Convert.ToInt64(HdnProdCodeMonths.Value) - Convert.ToInt64(HdnPTDMonths.Value);
            HdnFinalMonthCount.Value = Count.ToString();
            if (string.IsNullOrEmpty(_ds.Rows[0]["WaiverAmount"].ToString().Trim()))
            {
                hdnWaiverAmount.Value = "0";
            }
            else
            {
                hdnWaiverAmount.Value = _ds.Rows[0]["WaiverAmount"].ToString().Trim();
            }

            Decimal totalRequiredPremium = Convert.ToDecimal(hdnTotalRequiredPremium.Value.Trim());
            Decimal totalinterestAccured = Convert.ToDecimal(hdnTotalInterestAccured.Value.Trim());
            Decimal recoveryofpolicyDebt = Convert.ToDecimal(hdnRecoveryPolicyDebt.Value.Trim());
            Decimal adjustmentofSuspense = Convert.ToDecimal(hdnAdjustmentSuspense.Value.Trim());
            Decimal WaiverAmount = Convert.ToDecimal(hdnWaiverAmount.Value.Trim());

            string Channel = string.Empty;
            decimal WaiverApproveOnline = 0;
            string Flag = "1";
            objComm.GetOfflineOnlinePolicyDetails(ref __dsOnlineOffline, PolicyNumber, strApplicationno, Flag);
            //DataSet dsOnlineOffline = new DataSet();
            DataRow dr6 = __dsOnlineOffline.Tables[0].Rows[0];
            if (string.IsNullOrEmpty(dr6["Channel"].ToString().Trim()))
            {
                Channel = "";
                txtChannel.Text = string.Empty;
            }
            else
            {
                Channel = dr6["Channel"].ToString().Trim();
                txtChannel.Text = Channel;
            }

            if (Channel.ToUpper() == "ONLINE")
            {
                Flag = "2";
                objComm.GetOfflineOnlinePolicyDetails(ref __dsOnlineOffline, PolicyNumber, strApplicationno, Flag);
                if (__dsOnlineOffline != null && __dsOnlineOffline.Tables.Count > 0 && __dsOnlineOffline.Tables[0].Rows.Count > 0)
                {
                    DataRow drWaiverAMTOnline = __dsOnlineOffline.Tables[0].Rows[0];
                    if (string.IsNullOrEmpty(drWaiverAMTOnline["WaiverAmount"].ToString().Trim()))
                    {
                        WaiverApproveOnline = 0;
                        //Decimal AMT = Convert.ToDecimal(txtPremPaid.Text) + WaiverApproveOnline;
                        //txtTotalPrmPay.Text = AMT.ToString();
                    }
                    else
                    {
                        WaiverApproveOnline = Convert.ToDecimal(drWaiverAMTOnline["WaiverAmount"].ToString().Trim());
                        //Decimal AMT = Convert.ToDecimal(txtPremPaid.Text) + WaiverApproveOnline;
                        //txtTotalPrmPay.Text = AMT.ToString();
                    }
                }
                else
                {
                    WaiverApproveOnline = 0;
                }
                Decimal AMT = Convert.ToDecimal(txtPremPaid.Text) + WaiverApproveOnline;
                txtTotalPrmPay.Text = AMT.ToString();
            }
            else
            {
                try
                {
                    //txtTotalPrmPay.Text = (totalRequiredPremium + totalinterestAccured + recoveryofpolicyDebt + adjustmentofSuspense - WaiverAmount).ToString();
                    txtTotalPrmPay.Text = _ds.Rows[0]["NetPremium"].ToString().Trim();
                }
                catch (Exception)
                {
                }
            }
        }
    }

    //########################################### added by Akshay Revival cases END ###############################################



    //########################################### added by Akshay BO Revival cases Start ###############################################

    public void CallTotalRequiredPremium(string strPolicyNo)
    {
        try
        {
            DataSet ds = new DataSet();
            string MQResponce = string.Empty;
            string input = string.Empty;
            int result = 0;
            string err = "";

            WCFGenerateInputOutput.ReinstatementZCEENQIInput RNBFInput = new WCFGenerateInputOutput.ReinstatementZCEENQIInput();
            WCFGenerateInputOutput.ReinstatementZCEENQOOutput RNBFOutput = new WCFGenerateInputOutput.ReinstatementZCEENQOOutput();

            RNBFInput.ACTION = "B";
            RNBFInput.CHDRSEL = strPolicyNo;//00041263,
            RNBFInput.EFFDATE = HdnReciptdate.Value.Trim(); //DateTime.Today.ToString("yyyyMMdd");// policyNo;//00041263,
            RNBFInput.CarrierCode = ConfigurationManager.AppSettings["CarrierCode"].ToString();
            RNBFInput.Branch = ConfigurationManager.AppSettings["Branch"].ToString();
            //Session["BPMBRANCHCODE"].ToString();
            RNBFInput.UserId = ConfigurationManager.AppSettings["UserId"].ToString();
            RNBFInput.UserRole = ConfigurationManager.AppSettings["UserRole"].ToString();
            input = clientService.ZCEENQI(RNBFInput);

            MQResponce = mqHit.getMQResponce(input);
            RNBFOutput.ResMsg = MQResponce;
            clientService.ZCEENQO(RNBFOutput, ref err, ref result, ref ds);

            DataRow dr = ds.Tables[0].Rows[0];
            if (dr["LdrErrLvl"].ToString().Trim() == "0")
            {
                hdnWaiverAvailed.Value = dr["STIMTHO"].ToString().Trim();
                hdnTotalRequiredPremium.Value = dr["OSBAL"].ToString().Trim();

                txtTotalPrmPay.Text = dr["OSBAL"].ToString().Trim();
            }
            else if (dr["LdrErrLvl"].ToString().Trim() == "2")
            {

                txtTotalPrmPay.Text = "0";
                hdnWaiverAvailed.Value = "0";
                hdnTotalRequiredPremium.Value = "0";
                //txtWaiverAvailedBOE.Text = "0";
                //txtPremiumPayable.Text = "0";

                //string msg = "Error : " + dr[1].ToString() + "  " + dr[3].ToString();
                if (dr["EROR"].ToString() == "G544" || dr["EROR"].ToString() == "J073" || dr["EROR"].ToString() == "J074" || dr["EROR"].ToString() == "H137" || dr["EROR"].ToString() == "JL83" || dr["EROR"].ToString() == "HL67" || dr["EROR"].ToString() == "E767" || dr["EROR"].ToString() == "HL69" || dr["EROR"].ToString() == "ML05")
                {
                    //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", false);
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", true);
                //}
            }
            else
            {
                txtTotalPrmPay.Text = "0";
                hdnWaiverAvailed.Value = "0";
                hdnTotalRequiredPremium.Value = "0";
                //txtWaiverAvailedBOE.Text = "0";
                //txtPremiumPayable.Text = "0";

                //string msg = "Error : " + dr["EROR"].ToString() + "  " + dr["DESC"].ToString();
                if (dr["EROR"].ToString() == "G544" || dr["EROR"].ToString() == "J073" || dr["EROR"].ToString() == "J074" || dr["EROR"].ToString() == "H137" || dr["EROR"].ToString() == "JL83" || dr["EROR"].ToString() == "HL67" || dr["EROR"].ToString() == "E767" || dr["EROR"].ToString() == "HL69" || dr["EROR"].ToString() == "ML05")
                {
                    //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", false);
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", true);
                //}
            }

            //BOCollection.BOCollectionEnqClient client = new BOCollection.BOCollectionEnqClient();
            //BOCollection.MasterBOCollection objInput = client.GetBORequestParameter(policyNo, DateTime.Today.ToString("yyyyMMdd"), Session["BPMBRANCHCODE"].ToString(), ConfigurationManager.AppSettings["UserRole"].ToString(), ConfigurationManager.AppSettings["UserId"].ToString());

            //if (objInput.ERRORCODE == "0")
            //{
            //    txtTotalRequiredPremium.Text = objInput.OSBAL.ToString();
            //}
            //else
            //{
            //    txtTotalRequiredPremium.Text = "0";
            //    ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + objInput.ERRORMESSAGE + "');", true);
            //}
        }
        catch (Exception ex)
        {
            txtTotalPrmPay.Text = "0";
            hdnTotalRequiredPremium.Value = "0";
            txtTotalPrmPay.Text = "0";
            hdnWaiverAvailed.Value = "0";
            //txtWaiverAvailed.Text = "0";
            //txtTotalRequiredPremium.Text = "0";
            //txtWaiverAvailedBOE.Text = "0";
            //txtPremiumPayable.Text = "0";
            //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + ex.Message.ToString() + "');", true);
        }
    }

    public void CallOWLoan(string strPolicyNo)
    {
        try
        {
            DataSet ds = new DataSet();
            string MQResponce = string.Empty;
            string input = string.Empty;
            int result = 0;
            string err = "";

            WCFGenerateInputOutput.ReinstatementLONENQIInput RNBFInput = new WCFGenerateInputOutput.ReinstatementLONENQIInput();
            WCFGenerateInputOutput.ReinstatementLONENQOOutput RNBFOutput = new WCFGenerateInputOutput.ReinstatementLONENQOOutput();

            RNBFInput.CHDRSEL = strPolicyNo;//00041263,
            RNBFInput.CarrierCode = ConfigurationManager.AppSettings["CarrierCode"].ToString();
            RNBFInput.Branch = ConfigurationManager.AppSettings["Branch"].ToString();
            //Session["BPMBRANCHCODE"].ToString();
            RNBFInput.UserId = ConfigurationManager.AppSettings["UserId"].ToString();
            RNBFInput.UserRole = ConfigurationManager.AppSettings["UserRole"].ToString();
            input = clientService.LONENQI(RNBFInput);

            MQResponce = mqHit.getMQResponce(input);
            RNBFOutput.ResMsg = MQResponce;
            clientService.LONENQO(RNBFOutput, ref err, ref result, ref ds);

            DataRow dr = ds.Tables[0].Rows[0];
            if (dr["LdrErrLvl"].ToString().Trim() == "0")
            {
                txtOutstandingLoan.Text = dr["HPLEAMT"].ToString().Trim();
                txtOutstandingLoanInterest.Text = dr["HPLEINT"].ToString().Trim();
            }
            else if (dr["LdrErrLvl"].ToString().Trim() == "2")
            {
                txtOutstandingLoanInterest.Text = "0";
                txtOutstandingLoan.Text = "0";
                //string msg = "Error : " + dr[1].ToString() + "  " + dr[3].ToString();
                if (dr["EROR"].ToString() == "G544" || dr["EROR"].ToString() == "J073" || dr["EROR"].ToString() == "J074" || dr["EROR"].ToString() == "H137" || dr["EROR"].ToString() == "JL83" || dr["EROR"].ToString() == "HL67" || dr["EROR"].ToString() == "E767" || dr["EROR"].ToString() == "HL69" || dr["EROR"].ToString() == "ML05")
                {
                    //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", false);
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", true);
                //}
            }
            else
            {
                txtOutstandingLoanInterest.Text = "0";
                txtOutstandingLoan.Text = "0";
                //string msg = "Error : " + dr["EROR"].ToString() + "  " + dr["DESC"].ToString();
                if (dr["EROR"].ToString() == "G544" || dr["EROR"].ToString() == "J073" || dr["EROR"].ToString() == "J074" || dr["EROR"].ToString() == "H137" || dr["EROR"].ToString() == "JL83" || dr["EROR"].ToString() == "HL67" || dr["EROR"].ToString() == "E767" || dr["EROR"].ToString() == "HL69" || dr["EROR"].ToString() == "ML05")
                {
                    //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", false);
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            txtOutstandingLoanInterest.Text = "0";
            txtOutstandingLoan.Text = "0";
            //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + ex.Message.ToString() + "');", true);
        }

    }

    public void CallTotalInterestAccured(string strPolicyNo)
    {
        try
        {
            DataSet ds = new DataSet();
            string MQResponce = string.Empty;
            string input = string.Empty;
            int result = 0;
            string err = "";

            WCFGenerateInputOutput.ReinstatementLPSRQOIInput RNBFInput = new WCFGenerateInputOutput.ReinstatementLPSRQOIInput();
            WCFGenerateInputOutput.ReinstatementLPSRQOOOutput RNBFOutput = new WCFGenerateInputOutput.ReinstatementLPSRQOOOutput();

            RNBFInput.CHDRSEL = strPolicyNo;//00041263,
            RNBFInput.EFFDATE = HdnReciptdate.Value.Trim();//DateTime.Today.ToString("yyyyMMdd");// policyNo;//00041263,
            RNBFInput.ZSCHPRT = "N";
            RNBFInput.CarrierCode = ConfigurationManager.AppSettings["CarrierCode"].ToString();  //Session["CARRIERCODE"].ToString();// ConfigurationManager.AppSettings["CarrierCode"].ToString();
            RNBFInput.Branch = ConfigurationManager.AppSettings["Branch"].ToString();//Session["BPMBRANCHCODE"].ToString();
            RNBFInput.UserId = ConfigurationManager.AppSettings["UserId"].ToString(); // Session["USERID"].ToString().StartsWith("F") ? Session["USERID"].ToString() : "F" + Session["USERID"].ToString(); //Session["USERID"].ToString();// ConfigurationManager.AppSettings["UserId"].ToString();
            RNBFInput.UserRole = ConfigurationManager.AppSettings["UserRole"].ToString(); //Session["USERROLE"].ToString();// ConfigurationManager.AppSettings["UserRole"].ToString();
            input = clientService.LPSRQOI(RNBFInput);

            MQResponce = mqHit.getMQResponce(input);
            RNBFOutput.ResMsg = MQResponce;
            clientService.LPSRQOO(RNBFOutput, ref err, ref result, ref ds);

            DataRow dr = ds.Tables[0].Rows[0];
            if (dr["LdrErrLvl"].ToString().Trim() == "0")
            {
                hdnTotalInterestAccured.Value = dr["ZINTRST01"].ToString().Trim();

            }
            else if (dr["LdrErrLvl"].ToString().Trim() == "2")
            {
                hdnTotalInterestAccured.Value = "0";

                //txtPremPaid.Text = "0";
                //txtIntAmountAccured.Text = "0";
                //string msg = "Error : " + dr[1].ToString() + "  " + dr[3].ToString();
                //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", false);
                if (dr["EROR"].ToString() == "G544" || dr["EROR"].ToString() == "J073" || dr["EROR"].ToString() == "J074" || dr["EROR"].ToString() == "H137" || dr["EROR"].ToString() == "JL83" || dr["EROR"].ToString() == "HL67" || dr["EROR"].ToString() == "E767" || dr["EROR"].ToString() == "HL69" || dr["EROR"].ToString() == "ML05")
                {
                    //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", false);
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", true);
                //}
            }
            else
            {
                hdnTotalInterestAccured.Value = "0";

                //txtIntAmountAccured.Text = "0";
                //string msg = "Error : " + dr["EROR"].ToString() + "  " + dr["DESC"].ToString();
                //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", true);
                if (dr["EROR"].ToString() == "G544" || dr["EROR"].ToString() == "J073" || dr["EROR"].ToString() == "J074" || dr["EROR"].ToString() == "H137" || dr["EROR"].ToString() == "JL83" || dr["EROR"].ToString() == "HL67" || dr["EROR"].ToString() == "E767" || dr["EROR"].ToString() == "HL69" || dr["EROR"].ToString() == "ML05")
                {
                    //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", false);
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + msg + "');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            hdnTotalInterestAccured.Value = "0";
            //txtIntAmountAccured.Text = "0";
            //ClientScript.RegisterStartupScript(GetType(), "alertbox", "alert('" + ex.Message.ToString() + "');", true);
        }

    }


    //########################################### added by Akshay BO Revival cases END ###############################################

    public void BindApplicationDetails(DataTable _dsAppdtls)
    {

        Logger.Info(strApplicationno + "STAG3:-Function Call BindApplicationDetails" + System.Environment.NewLine);
        if (_dsAppdtls.Rows.Count > 0)
        {
            string strImageUrl = string.Empty;
            txtAppno.Text = _dsAppdtls.Rows[0]["AppNo"].ToString();
            txtPolno.Text = _dsAppdtls.Rows[0]["PolNo"].ToString();
            txtPolno1.Text = _dsAppdtls.Rows[0]["PolNo"].ToString();
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

                if (ddlUWDecesion.Items.FindByValue("Pending") != null)
                {
                    ddlUWDecesion.Items.FindByValue("Pending").Selected = true;
                    ddlUWDecesion.Enabled = false;
                }

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
            HideShowMandate();
            /*end here*/
            //FillPremiumDetails(strApplicationno,strPolicyNo);

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
            Logger.Info(strApplicationno + "STAG3:-Function Call BindAgentDetails" + System.Environment.NewLine);
            txtAgentcode1.Text = _dsAgentdetails.Rows[0]["AgentCode"].ToString();
            txtFgempcode.Text = _dsAgentdetails.Rows[0]["EmpCode"].ToString();
            txtPartnerempcode.Text = _dsAgentdetails.Rows[0]["PartnerCode"].ToString();
            txtcampaigncode.Text = _dsAgentdetails.Rows[0]["CampaignCode"].ToString();
            txtLeadcode.Text = _dsAgentdetails.Rows[0]["LeadCode"].ToString();
            chkAgentverified.Checked = Convert.ToBoolean(_dsAgentdetails.Rows[0]["IsAgentVerified"]);
            txtAgentName.Text = Convert.ToString(_dsAgentdetails.Rows[0]["AgentName"]);
            if (!string.IsNullOrEmpty(txtLeadcode.Text))
            {
                chkAgentLeadcode.Checked = true;

            }
            else
            {
                chkAgentLeadcode.Checked = false;
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
            /*added by shri on 22 july 17 to check pan conditionally*/
            //chkPanCopy.Checked = true;
            /*end here*/
            Logger.Info(strApplicationno + "STAG3:-Function Call BindPanDetails" + System.Environment.NewLine);
            txtPannumber.Text = _dsPandtls.Rows[0]["PANCardNo"].ToString();
            /*added by shri on 22 july 17 to check pan conditionally*/
            if (Convert.ToBoolean(_dsPandtls.Rows[0]["ISPANAPPLIED"]) && string.IsNullOrEmpty(txtPannumber.Text))
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



        objComm.GetSTPDetails(ref _dsSTPDetails, PolicyNumber);
        DataRow dr4 = _dsSTPDetails.Tables[0].Rows[0];
        if (string.IsNullOrEmpty(dr4["Age"].ToString().Trim()))
        {
            Age = null;
        }
        else
        {
            Age = dr4["Age"].ToString().Trim();
        }

        string src = "bpm";
        objComm.GetHealthProducts(ref _dsTsarMsarDtls, ProductCode, src);
        DataRow dr5 = _dsTsarMsarDtls.Tables[0].Rows[0];
        KeyWord = dr5["keyword"].ToString().Trim();
        //string OtherPolicyDetails11 = "PolicyNumber-" + PolicyNumber;
        //1.1
        string RequestDetails = "PolicyNumber-" + PolicyNumber;
        var TASR = CalTSAR.CalculateTSAR_FSAR_MSAR(LAClientID, ProposerClientID, PayerClientId, KeyWord, RequestDetails, OtherPolicyDetails, PartnerId);
        //1.1 Revival Changes start 
        double MSAR11 = Convert.ToDouble(TASR.MSAR);
        //1.1 Revival Changes end 

        StringBuilder sb = new StringBuilder();
        sb.Append(LAClientID);
        sb.Append(", ");
        sb.Append(ProposerClientID);
        sb.Append(", ");
        sb.Append(PayerClientId);
        sb.Append(", ");
        sb.Append(KeyWord);
        sb.Append(", ");
        sb.Append(RequestDetails);
        sb.Append(", ");
        sb.Append(OtherPolicyDetails);
        sb.Append(", ");
        sb.Append(PartnerId);

        string TSARIinput = sb.ToString();


        string Flag = "6";
        objComm.GetTMAX(ref _dsTMAX, Age, Flag);
        DataRow dr6 = _dsTMAX.Tables[0].Rows[0];


        if (string.IsNullOrEmpty(dr6["MAX_TDTSAR"].ToString().Trim()))
        {
            MaxTSAR = "0";
        }
        else
        {
            MaxTSAR = dr6["MAX_TDTSAR"].ToString().Trim();
        }

        //string OwnerClientID = string.Empty;

        Flag = "8";
        objComm.GetTotalPremium(ref _dsPremium, PolicyNumber, Flag);
        DataRow drpremium = _dsPremium.Tables[0].Rows[0];


        if (string.IsNullOrEmpty(drpremium["Premium"].ToString().Trim()))
        {
            Premium = "0";
        }
        else
        {
            Premium = drpremium["Premium"].ToString().Trim();
        }

        DataTable SARdt = new DataTable();
        SARdt.Clear();
        SARdt.Columns.Add("ClientID");
        SARdt.Columns.Add("Client Role");
        SARdt.Columns.Add("Client Name");
        SARdt.Columns.Add("MSAR");
        SARdt.Columns.Add("TSAR");
        SARdt.Columns.Add("FSAR");
        SARdt.Columns.Add("Premium");

        DataRow _SAR = SARdt.NewRow();
        _SAR["ClientID"] = LAClientID;
        _SAR["Client Role"] = "LA";
        _SAR["Client Name"] = LAName;
        //_SAR["MSAR"] = MaxTSAR;
        //1.2 Revival Changes start
        _SAR["MSAR"] = MSAR11;
        //1.2 Revival Changes end
        _SAR["TSAR"] = TASR.TSAR;
        _SAR["FSAR"] = TASR.FSAR;
        _SAR["Premium"] = Premium;
        SARdt.Rows.Add(_SAR);
        //_SAR[""] = "";
        //SARdt.Columns.Add();
        //SARdt.Columns.Add();

        BindPreviousPolictDetails(SARdt);



        Logger.Info(strApplicationno + "STAG3:-Function Call FillTsmrMsarDetails" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :FillTsmrMsarDetails");
        DataSet _dsMsarTsar = new DataSet();
        //strLApushErrorCode = 0;
        //strLApushStatus = string.Empty;
        //objChangeObj = (ChangeValue)Session["objLoginObj"];
        //objComm.OnlineTsmrMsarDisplayDetails_GET(ref _dsTsarMsarDtls, strApplicationno, strChannelType);
        //if (_dsTsarMsarDtls != null && _dsTsarMsarDtls.Tables.Count > 0 && _dsTsarMsarDtls.Tables[0].Rows.Count > 0)
        //{
        //    objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, ChannelType, objChangeObj, ref _dsMsarTsar, ref _dsPrevPol, "MSAR", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
        //    BindTsarMsarDetails(_dsMsarTsar.Tables[0]);



        //    BindPreviousPolictDetails(_dsPrevPol.Tables[0]);
        //    //objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, ChannelType, objChangeObj, ref _dsMsarTsar, ref _dsPrevPol, "MSARSARAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
        //    //BindTsarMsarDetailsSaral(_dsMsarTsar.Tables[0]);
        //    //BindPreviousPolictDetailsSaral(_dsPrevPol.Tables[0]);
        //}
    }

    public void BindPreviousPolictDetails(DataTable _dsPrev)
    {
        if (_dsPrev.Rows.Count > 0)
        {
            //_dsPrev.Columns.Remove("stat");
            //_dsPrev.Columns.Remove("ageProofSubmitted");
            //_dsPrev.Columns.Remove("idProofSubmitted");
            //_dsPrev.Columns.Remove("addressProofSubmitted");
            //_dsPrev.Columns.Remove("incomeProofSubmitted");
            //_dsPrev.Columns.Remove("underwriterComments");

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

    public void FillProductDetails(string strApplicationno, string ChannelType)
    {
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillProductDetails" + System.Environment.NewLine);
        objComm.OnlineProductDisplayDetails_GETRevival(ref _dsProdDtls, strApplicationno, ChannelType);
        BindProductDetails(_dsProdDtls.Tables[0]);
        BindRiderDetails(_dsProdDtls.Tables[1]);


        if (_dsProdDtls != null && _dsProdDtls.Tables[1].Rows.Count > 0)
        {
            Logger.Info(strApplicationno + " STAG2:-R" + System.Environment.NewLine);
            ViewState["LoadMaster"] = _dsProdDtls.Tables[1];
            ddlLoadRiderCode.DataSource = _dsProdDtls.Tables[1];
            ddlLoadRiderCode.DataTextField = "RIDERNAME";
            ddlLoadRiderCode.DataValueField = "RIDERCODE";
            //ddlLoadRiderCode.DataMember = "Code";
            ddlLoadRiderCode.DataBind();
            ddlLoadRiderCode.Items.Insert(0, new ListItem("--Select--", "0"));
            Logger.Info(strApplicationno + " STAG2:-S" + System.Environment.NewLine);
        }
    }

    public void BindProductDetails(DataTable _dsProdDtls)
    {
        string ProductName = string.Empty;
        if (_dsProdDtls.Rows.Count > 0)
        {

            double strServiceTax = Convert.ToDouble(ConfigurationManager.AppSettings["ServiceTax"]);
            double strBasePremium = Math.Round(Convert.ToInt32(_dsProdDtls.Rows[0]["BasePremium"].ToString()) * strServiceTax) + Convert.ToInt32(_dsProdDtls.Rows[0]["BasePremium"].ToString());
            Logger.Info(strApplicationno + "STAG3:-Function Call BindProductDetails" + System.Environment.NewLine);
            hdnProductType.Value = _dsProdDtls.Rows[0]["ProductType"].ToString();
            txtProdcode.Text = _dsProdDtls.Rows[0]["ProductCode"].ToString();
            txtProname.Text = _dsProdDtls.Rows[0]["ProdcutName"].ToString();
            txtProname1.Text = _dsProdDtls.Rows[0]["ProdcutName"].ToString();
            txtBasepolno.Text = _dsProdDtls.Rows[0]["PolicyNo"].ToString();
            txtPolterm.Text = _dsProdDtls.Rows[0]["PolicyTerm"].ToString();
            txtPrepayterm.Text = _dsProdDtls.Rows[0]["PremiumTerm"].ToString();
            txtSumassure.Text = _dsProdDtls.Rows[0]["SumAssured"].ToString();
            //txtSumassure1.Text = _dsProdDtls.Rows[0]["SumAssured"].ToString();
            ddlFrequency.SelectedValue = _dsProdDtls.Rows[0]["PremiumFreq"].ToString();
            // txtSisamount.Text = _dsProdDtls.Rows[0]["AmountInSIS"].ToString();
            txtBasepremium.Text = _dsProdDtls.Rows[0]["BasePremium"].ToString();
            txtServicetax.Text = _dsProdDtls.Rows[0]["ServiceTax"].ToString();
            txtTotalpremium.Text = _dsProdDtls.Rows[0]["TotalPremium"].ToString();
            txtMonthlyPayoutBase.Text = (string.IsNullOrEmpty(Convert.ToString(_dsProdDtls.Rows[0]["MonthlyPayout"]))) ? "0" : _dsProdDtls.Rows[0]["MonthlyPayout"].ToString();
            /*ID:8 START*/
            txtProdBranchBasePremium.Text = Convert.ToString(_dsProdDtls.Rows[0]["BranchBasePremium"]);
            IsBasePremiumDifferentChecker();
            /*ID:8 END*/
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
                ProductName = dr["ProdcutName"].ToString() + " + ";
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
        // dt.Columns["IsActive"].DataType = typeof(bool);
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
            string _strLoadReason1Discp = string.Empty;
            string _strLoadReason2 = string.Empty;
            string _strLoadReason2Discp = string.Empty;
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
            _strReason1code = _dS.Tables[0].Rows[0]["LD_reason1"].ToString();
            _strReason2code = _dS.Tables[0].Rows[0]["LD_reason2"].ToString();
            _strSumAssured = _dS.Tables[0].Rows[0]["LD_sumAssured"].ToString();
            _strRateAdjust = _dS.Tables[0].Rows[0]["LD_rateAdjustment"].ToString();
            _strFlatmortality = _dS.Tables[0].Rows[0]["LD_flatMortality"].ToString();
            _strPremiumPayingterm = _dS.Tables[0].Rows[0]["LD_premiumPayingTerm"].ToString();
            _strLetterPrint = _dS.Tables[0].Rows[0]["LD_isLetterPrintRequired"].ToString();
            _strLoadRiderName = _dS.Tables[0].Rows[0]["LD_rider"].ToString();
            LoadDtls objLoad = new LoadDtls();
            objLoad._strProdcode = _dS.Tables[0].Rows[0]["LD_rider"].ToString();
            objChangeObj.Load_Loadingdetails = objLoad;
            objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, _strLoadCode, _strLoadDiscp, _strReason1code, _strReason2code, "", _strSumAssured, txtLoadPer.Text, _strRateAdjust, _strFlatmortality, _strPremiumPayingterm, _strLetterPrint, _strLoadRiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno,"", ref LoadingResult);
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
        BindProductControlDetails(_dsProdontrol.Tables[0]);
    }

    public void BindProductControlDetails(DataTable _dt)
    {
        if (_dt != null && _dt.Rows.Count > 0)
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
            BindMasterDetails(ddlUWreason);
            BindMasterDetails(ddlUWDecesion);
            BindMasterDetails(ddlBkdateReason);
            BindMasterDetails(ddlLoadType);
            BindMasterDetails(ddlLoadRsn1);
            BindMasterDetails(ddlLoadRsn2);
            BindMasterDetails(ddlLoadFlatMortality);
            BindMasterDetails(ddlCommonStatus);
            //BindMasterDetails(ddlIfscCode);            
            BindMasterDetails(ddlAutoPaytype);
            //Added by Suraj on 18 APRIL 2018 for add reason dropdown for medical
            BindMasterDetails(ddlRequirementMedicalReason);
        }
    }

    public void FillMasterDetails(ref DataSet _dsFollowuo)
    {
        Logger.Info(strApplicationno + "STAG 2A:-Fill master Details Region" + System.Environment.NewLine);
        /*check if cache is null or not */
        //objComm.OnlineMasterDisplayDetails_GET_RevivalCases(ref _dsFollowuo);
        if (HttpRuntime.Cache["DropDownList"] == null)
        {
            //objComm.OnlineMasterDisplayDetails_GET(ref _dsFollowuo);
            objComm.OnlineMasterDisplayDetails_GETRevival(ref _dsFollowuo);
            //fill cache 
            HttpRuntime.Cache.Add("DropDownList", _dsFollowuo, null, System.DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.Normal, null);
        }
        else
        {
            //fill DsFolloUp From Cache
            _dsFollowuo = (DataSet)HttpRuntime.Cache["DropDownList"];
        }

        /*end here*/

        if (_dsFollowuo.Tables.Count > 0)
        {
            ViewState["_dsFollowuo"] = _dsFollowuo;
            BindMasterDetails(ddlUWreason);
            BindMasterDetails(ddlUWDecesion);
            BindMasterDetails(ddlBkdateReason);
            BindMasterDetails(ddlLoadType);
            BindMasterDetails(ddlLoadRsn1);
            BindMasterDetails(ddlLoadRsn2);
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

                //for (int i = dtCurrentTable.Rows.Count-2; i > 0; i--)
                //{
                //    DropDownList REQ_followUpCode1 = new DropDownList();
                //    REQ_followUpCode1 = (DropDownList)gvRequirmentDetails.Rows[i].Cells[1].FindControl("ddlfollowupcode");
                //    REQ_followUpCode1.Enabled = true;
                //}

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
                    Label lblLoadType = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadType");
                    Label lblLoadRsn1 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn1");
                    Label lblLoadRsn2 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn2");
                    Label lblLoadper = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadper");
                    Label lblRateAdjust = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRateAdjust");
                    Label lblFlatMortality = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblFlatMortality");


                    //Fill the DropDownList with Data
                    //FillDropDownList(ddlLoadType, "LoadingCode", "Mst");
                    //BindMasterDetails(ddlLoadType);
                    //BindMasterDetails(ddlLoadType);

                    if (i <= dt.Rows.Count - 1)
                    {

                        lblLoadType.Text = dt.Rows[i]["ddlLoadType"].ToString();
                        lblLoadRsn1.Text = dt.Rows[i]["ddlLoadRsn1"].ToString();
                        lblLoadRsn2.Text = dt.Rows[i]["ddlRsn2"].ToString();
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
        dt.Columns.Add(new DataColumn("REQ_RaisedDate", typeof(string)));
        //dt.Columns.Add(new DataColumn("lblRaisedby", typeof(string)));
        dt.Columns.Add(new DataColumn("REQ_ClosedDate", typeof(string)));
        //dt.Columns.Add(new DataColumn("lblClosedBy", typeof(string)));

        //Add a Dummy Data on Initial Load
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable1"] = dt;
        //Bind the DataTable to the Grid
        gvRequirmentDetails.DataSource = dt;
        gvRequirmentDetails.DataBind();

        DropDownList ddlfollowupcode = (DropDownList)gvRequirmentDetails.Rows[0].Cells[0].FindControl("ddlfollowupcode");
        TextBox txtLoadRider = (TextBox)gvRequirmentDetails.Rows[0].Cells[1].FindControl("lblfollowupDiscp");
        DropDownList ddlCategory = (DropDownList)gvRequirmentDetails.Rows[0].Cells[2].FindControl("ddlCategory");
        DropDownList ddlCriteria = (DropDownList)gvRequirmentDetails.Rows[0].Cells[3].FindControl("ddlCriteria");
        DropDownList ddlLifeType = (DropDownList)gvRequirmentDetails.Rows[0].Cells[4].FindControl("ddlLifeType");
        DropDownList ddlStatus = (DropDownList)gvRequirmentDetails.Rows[0].Cells[5].FindControl("ddlStatus");
        Label lblRaiseddate = (Label)gvRequirmentDetails.Rows[0].Cells[6].FindControl("lblRaiseddate");
        //Label lblRaisedby = (Label)gvRequirmentDetails.Rows[0].Cells[7].FindControl("lblRaisedby");
        Label lblClosedDate = (Label)gvRequirmentDetails.Rows[0].Cells[7].FindControl("lblClosedDate");//Change Shyam 7---8
                                                                                                       // Label lblClosedBy = (Label)gvRequirmentDetails.Rows[0].Cells[9].FindControl("lblClosedBy");

        ////Fill the DropDownList with Data
        ////FillDropDownList(ddlLoadType, "LoadingCode", "Mst");
        //btnReqaddrows.CssClass = "btn btn-primary";
        //BindMasterDetails(ddlfollowupcode);
        //BindMasterDetails(ddlCategory);
        //BindMasterDetails(ddlCriteria);
        ////BindMasterDetails(ddlLifeType);
        //BindMasterDetails(ddlStatus);

        btnReqaddrows.CssClass = "btn btn-primary";
        gvRequirmentDetails.CssClass = "table table-bordered table-striped";

        hdnNewFollowup.Value = "TRUE";

        UpdatePanel1.Update();
    }

    public void PopulateLoadingDetails()
    {
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :PopulateLoadingDetails" + System.Environment.NewLine);
        DataTable _dtLoaddata = new DataTable();
        DataRow _drRow = null;
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        LoadDtls objLoad = new LoadDtls();
        //if (Session["LoadingData"] == null)
        //{
        if (ViewState["LoadingData"] == null)
        {
            _dtLoaddata.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadType", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadRsn1", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlRsn2", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("txtLoadPer", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("txtRateAdjust", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadFlatMortality", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("RiderName", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("LoadingDiscp", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("Reason1Discp", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("Reason2Discp", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("LetterPrint", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadCode", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadRsn1Code", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("ddlLoadRsn2Code", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("RiderCode", typeof(string)));
            _dtLoaddata.Columns.Add(new DataColumn("LoadType", typeof(string)));

            _drRow = _dtLoaddata.NewRow();
            _drRow[1] = ddlLoadType.SelectedItem.Text;
            _drRow[2] = ddlLoadRsn1.SelectedItem.Text;
            _drRow[3] = ddlLoadRsn2.SelectedItem.Text;
            _drRow[4] = txtLoadPer.Text;
            _drRow[5] = txtRateAdjust.Text;
            _drRow[6] = ddlLoadFlatMortality.SelectedValue;
            _drRow[7] = ddlLoadRiderCode.SelectedValue;
            _drRow[8] = txtLoadDesc.Text;
            _drRow[9] = txtRsn1Desc.Text;
            _drRow[10] = txtRsn2Desc.Text;
            _drRow[11] = ddlLoadletterPrint.SelectedValue;
            _drRow[12] = ddlLoadType.SelectedValue;
            _drRow[13] = ddlLoadRsn1.SelectedValue;
            _drRow[14] = ddlLoadRsn2.SelectedValue;
            _drRow[15] = txtProdcode.Text;
            _drRow[16] = ddlLoadRiderCode.SelectedItem.Text;

            _dtLoaddata.Rows.Add(_drRow);
            //Session["LoadingData"] = _dtLoaddata;
            ViewState["LoadingData"] = _dtLoaddata.Copy();
            gvLoadingDtls.DataSource = _dtLoaddata;
            gvLoadingDtls.DataBind();


            //ddlLoadType.CssClass = "form-control";
            //ddlLoadRsn1.CssClass = "form-control";
            //ddlLoadRsn2.CssClass = "form-control";            
            //txtLoadPer.CssClass = "form-control";
            //txtLoadDesc.CssClass = "form-control";
            //txtRsn1Desc.CssClass = "form-control";
            //txtRsn2Desc.CssClass = "form-control";
            ddlLoadFlatMortality.CssClass = "form-control";
            txtRateAdjust.CssClass = "form-control ";
            ddlLoadletterPrint.CssClass = "form-control";

            ddlLoadType.SelectedValue = "0";
            ddlLoadRsn1.SelectedValue = "0";
            ddlLoadRsn2.SelectedValue = "0";
            ddlLoadFlatMortality.SelectedValue = "0";
            txtLoadPer.Text = "";
            txtLoadDesc.Text = "";
            txtRsn1Desc.Text = "";
            txtRsn2Desc.Text = "";
            txtRateAdjust.Text = "";
            ddlLoadletterPrint.SelectedValue = "0";
            ddlLoadFlatMortality.SelectedValue = "0";



            updLoadType.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();
            UpdatePanel5.Update();


        }
        else
        {
            //_dtLoaddata = (DataTable)Session["LoadingData"];
            _dtLoaddata = (DataTable)ViewState["LoadingData"];
            _drRow = _dtLoaddata.NewRow();
            _drRow[1] = ddlLoadType.SelectedItem.Text;
            _drRow[2] = ddlLoadRsn1.SelectedItem.Text;
            _drRow[3] = ddlLoadRsn2.SelectedItem.Text;
            _drRow[4] = txtLoadPer.Text;
            _drRow[5] = txtRateAdjust.Text;
            _drRow[6] = ddlLoadFlatMortality.SelectedValue;
            _drRow[7] = ddlLoadRiderCode.SelectedValue;
            _drRow[8] = txtLoadDesc.Text;
            _drRow[9] = txtRsn1Desc.Text;
            _drRow[10] = txtRsn2Desc.Text;
            _drRow[11] = ddlLoadletterPrint.SelectedValue;
            _drRow[11] = ddlLoadletterPrint.SelectedValue;
            _drRow[12] = ddlLoadType.SelectedValue;
            _drRow[13] = ddlLoadRsn1.SelectedValue;
            _drRow[14] = ddlLoadRsn2.SelectedValue;
            _drRow[15] = txtProdcode.Text;
            _drRow[16] = ddlLoadRiderCode.SelectedItem.Text;

            _dtLoaddata.Rows.Add(_drRow);
            // Session["LoadingData"] = _dtLoaddata;
            ViewState["LoadingData"] = _dtLoaddata.Copy();
            gvLoadingDtls.DataSource = _dtLoaddata;
            gvLoadingDtls.DataBind();


            //ddlLoadType.CssClass = "form-control";
            //ddlLoadRsn1.CssClass = "form-control";
            //ddlLoadRsn2.CssClass = "form-control";
            //ddlLoadFlatMortality.CssClass = "form-control";
            //txtLoadPer.CssClass = "form-control";
            //txtLoadDesc.CssClass = "form-control";
            //txtRsn1Desc.CssClass = "form-control";
            //txtRsn2Desc.CssClass = "form-control";
            //txtRateAdjust.CssClass = "form-control";

            ddlLoadFlatMortality.CssClass = "form-control";
            txtRateAdjust.CssClass = "form-control";
            ddlLoadletterPrint.CssClass = "form-control";

            ddlLoadType.SelectedValue = "0";
            ddlLoadRsn1.SelectedValue = "0";
            ddlLoadRsn2.SelectedValue = "0";
            ddlLoadFlatMortality.SelectedValue = "0";
            txtLoadPer.Text = "";
            txtLoadDesc.Text = "";
            txtRsn1Desc.Text = "";
            txtRsn2Desc.Text = "";
            txtRateAdjust.Text = "";
            ddlLoadFlatMortality.SelectedValue = "0";
            ddlLoadletterPrint.SelectedValue = "0";

            updLoadType.Update();
            UpdatePanel3.Update();
            UpdatePanel4.Update();
            UpdatePanel5.Update();
        }
        if (gvLoadingDtls.Rows.Count > 0)
        {

            //hdnIsConsentReq.Value = response.ToString();
            if (objChangeObj.Load_Loadingdetails == null)
                objChangeObj.Load_Loadingdetails = new LoadDtls();
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
            Label lblLoadRsn1 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn1");
            Label lblLoadRsn2 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn2");
            Label lblLoadper = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadper");
            Label lblRateAdjust = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRateAdjust");
            Label lblFlatMortality = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblFlatMortality");

            lblLoadType.Text = ddlLoadType.SelectedItem.Text;
            lblLoadRsn1.Text = ddlLoadRsn1.SelectedItem.Text;
            lblLoadRsn2.Text = ddlLoadRsn2.SelectedItem.Text;
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
            Label lblLoadRsn1 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn1");
            Label lblLoadRsn2 = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadRsn2");
            Label lblLoadper = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblLoadper");
            Label lblRateAdjust = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblRateAdjust");
            Label lblFlatMortality = (Label)gvLoadingDtls.Rows[0].Cells[0].FindControl("lblFlatMortality");

            lblLoadType.Text = ddlLoadType.SelectedItem.Text;
            lblLoadRsn1.Text = ddlLoadRsn1.SelectedItem.Text;
            lblLoadRsn2.Text = ddlLoadRsn2.SelectedItem.Text;
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

                ddl.DataSource = _dsMaster.Tables[strTableName];
                ddl.DataTextField = "NAME";
                ddl.DataValueField = "VALUE";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", "0"));
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
    protected void ddlfollowupcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :ddlfollowupcode_SelectedIndexChanged");
            DataSet _dsFollowupSubDtls = new DataSet();
            // Get the master DropDownList and its value
            DropDownList ddlEditEducationEstablishment = (DropDownList)sender;
            string educationEstablishmentCode = ddlEditEducationEstablishment.SelectedValue;

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

            string Value = "TRUE";
            hdnNewFollowup.Value = Value.ToString();

            Session["hdnNewFollowup"] = "TRUE";

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
        catch (Exception ex)
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
        objUWDecision = new UWSaralDecision.BussLayer();
        Logger.Info("STAG 2 :PageName :Uwdecision.aspx.CS // MethodeName :btnAddLoadingRow_Click");
        /*added by shri on 28 dec 17 to add tracking*/
        int intTrackingRet = -1;
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;
        InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "LOAD_ADD_NEWROW", ref intTrackingRet);
        /*end here*/
        if (gvLoadingDtls.Rows.Count <= 0)
        {
            ViewState["LoadingData"] = null;
            //Session["LoadingData"] = null;
        }
        PopulateLoadingDetails();
        btnAddLoadingRow.CssClass = "btn btn-primary lnkButton";
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
        PremiumCalculation(objReplicaXml, ref isdataSave, ref _ds, ref intResponse, ref strResponse);
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "Pending", ref LAPushErrorCode, ref strLAPushErrorMsg, ref strConsentResponse);
        UWPreIssueServiceCall(strApplicationno, strChannelType);
        //End
        /*ID:18 Start*/
        chkLoadingDtls.Checked = false;
        updLoadingDetails.Update();
        /*added for testing purpose*/
        updProductDetails.Update();
        /*ID:18 End*/
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
      
        objComm.OnlineUWDecisionDetails_Save(objCommonObj._AppType, strApplicationno, ddlUWDecesion.SelectedItem.Text, ddlUWreason.SelectedItem.Text, ddlUWDecesion.SelectedValue, ddlUWreason.SelectedItem.Value, _strUWPeriod, struserid, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objCommonObj._bpm_branchCode, objCommonObj._bpm_creationDate,
          objCommonObj._bpm_systemDate, objCommonObj._bpm_businessDate, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno,"","", ref UWDecisionResult);
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
        CheckBox cbIsActive = (CheckBox)e.Row.FindControl("chkremoveRider");
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
    }

    protected void btnValidatepan_Click(object sender, EventArgs e)
    {
        try
        {
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
           // objComm.OnlinePancardDetails_Save(txtBnkClientnumber.Text, txtBnkClientname.Text, "", "", "", "", "", "", "", txtPannumber.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, "UWSaral", strChannelType, chkPanCopy.Checked, ref _PanResult);
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
        catch (Exception ex)
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
        /*fetch first name middle name last name*/
        //26.1 Begin of Changes; Bibekananda Nanda - [1103055]

        string strPanStatus = string.Empty;
        string strPanTitle = string.Empty;
        string strLastUpdDate = string.Empty;

        //26.1 End of Changes; Bibekananda Nanda - [1103055]
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
        string strnsdlPanNumber = txtPannumber.Text;
        char c = strnsdlPanNumber[3];
        if (c.ToString().ToUpper() == "P")
        {
            lblnsdlPanType.Text = "Personal";
        }
        else
        {
            lblnsdlPanType.Text = "Other Than Personal";
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
                    //29.1 Begin of Changes; Sagar Thorave-[mfl00886]
                    DataSet ds12 = null;
              
                    objComm.Featch_AMLFLAG_Details(ref ds12, strApplicationno, "SaralUW");
                    string CLNTRSKIND = (ds12.Tables[0].Rows[0]["CLNTRSKIND"].ToString());
                    //string ClientNo = string.Empty;
                    //if (ds12.Tables[0] != null)
                    //{
                    //    if (ds12.Tables[0].Rows.Count > 0)
                    //    {
                    //        ClientNo = (ds12.Tables[0].Rows[0]["ClientNo"].ToString());
                    //    }
                    //}
                    //if (!string.IsNullOrEmpty(ClientNo))
                    //{

                    //    row["CLTTWO"] = ClientNo;
                    //}
                    objAml.AMLPushService(strApplicationno, row, objChangeObj, ref strLAPushErrorcode, ref strLAPushStatus, CLNTRSKIND);
                    //29.1 End of Changes; Sagar Thorave-[mfl00886]
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
        int _ProdResult = 0;
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
            DataSet _dsPremiumCalculation;
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

    protected void ddlUWDecesion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet _dsDecisionReason = null;
            Logger.Info("STAG 11 :PageName :Uwdecision.aspx.CS // MethodeName :ddlUWDecesion_SelectedIndexChanged" + System.Environment.NewLine);
            FillSubMasterDetails(ref _dsDecisionReason, ddlUWDecesion.SelectedValue, "UWReason", "");
            ddlUWreason.DataSource = _dsDecisionReason.Tables[0];
            if (_dsDecisionReason.Tables.Count > 0 && _dsDecisionReason.Tables[0].Rows.Count > 0)
            {
                divUWReason.Visible = true;
                ddlUWreason.DataTextField = "NAME";
                ddlUWreason.DataValueField = "VALUE";
                ddlUWreason.DataBind();
                ddlUWreason.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                divUWReason.Visible = false;
            }
            if (ddlUWDecesion.SelectedValue == "Postponed")
            {
                clsName = divPostponedPeriod.Attributes["class"].ToString();
                divPostponedPeriod.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
            }
            else
            {
                divPostponedPeriod.Attributes.Add("class", "HideControl");
            }
            if (gvExtLoadDetails.Rows.Count > 0)
            {
                if (ddlUWDecesion.SelectedValue == "Approved")
                {
                    ddlUWreason.SelectedValue = "A02";
                }
            }
            /*ID:19 START*/
            //if (ddlUWDecesion.SelectedValue.Equals("Declined"))
            //{
            //    if (!string.IsNullOrEmpty(txtSaralRiskScore.Text) && Convert.ToInt32(txtSaralRiskScore.Text) < 75)
            //    {
            //        FillWarningMessage("10");
            //        DisplaySaralWarningMessage();
            //    }
            //    if (!string.IsNullOrEmpty(txtSaralRiskChannel.Text) && txtSaralRiskChannel.Text.Equals("Bankassurance-AU"))
            //    {
            //        FillWarningMessage("11");
            //        DisplaySaralWarningMessage();
            //    }
            //}
            /*ID:19 END*/
        }
        catch (Exception ex)
        {


        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }

    protected void ddlLoadType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Logger.Info("STAG 12 :PageName :Uwdecision.aspx.CS // MethodeName :ddlLoadType_SelectedIndexChanged" + System.Environment.NewLine);
        DataSet _dsLoadVal = new DataSet();
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
                txtRateAdjust.CssClass = "form-control Numeric";
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
            ddlLoadletterPrint.CssClass = "form-control";
            ddlLoadletterPrint.Enabled = true;
            UpdatePanel5.Update();
            updLoadType.Update();
        }
    }
    protected void ddlLoadRsn1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet _dsLoadReasonDiscp = new DataSet();
        Logger.Info("STAG 13 :PageName :Uwdecision.aspx.CS // MethodeName :ddlLoadRsn1_SelectedIndexChanged" + System.Environment.NewLine);
        ddlLoadRsn1.Enabled = true;
        FillSubMasterDetails(ref _dsLoadReasonDiscp, ddlLoadRsn1.SelectedValue, "LoadReason1", "");
        if (_dsLoadReasonDiscp.Tables.Count > 0 && _dsLoadReasonDiscp.Tables[0].Rows.Count > 0)
        {
            txtRsn1Desc.Text = _dsLoadReasonDiscp.Tables[0].Rows[0]["NAME"].ToString();
            ddlLoadRsn1.CssClass = "form-control";
        }
    }
    protected void ddlLoadRsn2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet _dsLoadReasonDiscp = new DataSet();
        Logger.Info("STAG 14 :PageName :Uwdecision.aspx.CS // MethodeName :ddlLoadRsn2_SelectedIndexChanged" + System.Environment.NewLine);
        ddlLoadRsn2.Enabled = true;
        FillSubMasterDetails(ref _dsLoadReasonDiscp, ddlLoadRsn2.SelectedValue, "LoadReason2", "");
        if (_dsLoadReasonDiscp.Tables.Count > 0 && _dsLoadReasonDiscp.Tables[0].Rows.Count > 0)
        {
            txtRsn2Desc.Text = _dsLoadReasonDiscp.Tables[0].Rows[0]["NAME"].ToString();
            ddlLoadRsn2.CssClass = "form-control";

        }
    }
    protected void btncalculatePrem_Load_Click(object sender, EventArgs e)
    {
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "LOADING_PREM", ref intTrackingRet);
            /*end here*/
            int _ProdResult = 0;
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
            FillLoadParam(ref objChangeObj);

            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "PREMIUMCAL", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
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
        catch (Exception ex)
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
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/

        }
        catch (Exception ex)
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
        string _strLoadReason1 = string.Empty;
        string _strLoadReason1Discp = string.Empty;
        string _strLoadReason2 = string.Empty;
        string _strLoadReason2Discp = string.Empty;
        string _strLoadPercent = string.Empty;
        string _strRateAdjust = string.Empty;
        string _strFlatmortality = string.Empty;
        string _strLetterPrint = string.Empty;
        string _strRiderCode = string.Empty;
        string _strLoadCode = string.Empty;
        string _strReason1code = string.Empty;
        string _strReason2code = string.Empty;
        int strTotalLoadedPremiumAB = 0;
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
            for (i = 0; i < _dtLoaddata.Rows.Count; i++)
            {
                _strLoadRiderName = _dtLoaddata.Rows[i]["RiderName"].ToString();
                _strLoadType = _dtLoaddata.Rows[i]["ddlLoadType"].ToString();
                _strLoadDiscp = _dtLoaddata.Rows[i]["LoadingDiscp"].ToString();
                _strLoadReason1 = _dtLoaddata.Rows[i]["ddlLoadRsn1"].ToString();
                _strLoadReason1Discp = _dtLoaddata.Rows[i]["Reason1Discp"].ToString();
                _strLoadReason2 = _dtLoaddata.Rows[i]["ddlRsn2"].ToString();
                _strLoadReason2Discp = _dtLoaddata.Rows[i]["Reason2Discp"].ToString();
                _strLoadPercent = _dtLoaddata.Rows[i]["txtLoadPer"].ToString();
                _strRateAdjust = _dtLoaddata.Rows[i]["txtRateAdjust"].ToString();
                _strFlatmortality = (_dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString() == "0") ? "" : _dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString();
                _strLetterPrint = _dtLoaddata.Rows[i]["LetterPrint"].ToString();
                _strRiderCode = _dtLoaddata.Rows[i]["RiderCode"].ToString();
                _strLoadCode = _dtLoaddata.Rows[i]["ddlLoadCode"].ToString();
                _strReason1code = _dtLoaddata.Rows[i]["ddlLoadRsn1Code"].ToString();
                _strReason2code = _dtLoaddata.Rows[i]["ddlLoadRsn2Code"].ToString();

                objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, _strLoadCode, _strLoadDiscp, _strReason1code, _strReason2code, "", txtSumassure.Text, txtLoadPer.Text, _strRateAdjust, _strFlatmortality, txtPrepayterm.Text, _strLetterPrint, _strLoadRiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno,"", ref LoadingResult);

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
                ddlLoadRsn2.SelectedValue = "0";
                ddlLoadFlatMortality.SelectedValue = "0";
                ddlLoadletterPrint.SelectedValue = "0";
                txtLoadDesc.Text = "";
                txtRsn1Desc.Text = "";
                txtRsn2Desc.Text = "";
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
                btnAddLoadingRow.CssClass = "btn btn-primary";
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
            DropDownList ddlLoadType = (DropDownList)e.Row.FindControl("ddlExstingLoadType");
            BindMasterDetails(ddlLoadType);

            DropDownList ddlLoadRsn1 = (DropDownList)e.Row.FindControl("ddlExistingLoadRsn1");
            BindMasterDetails(ddlLoadRsn1);

            DropDownList ddlLoadRsn2 = (DropDownList)e.Row.FindControl("ddlExistingLoadRsn2");
            BindMasterDetails(ddlLoadRsn2);

            DropDownList ddlLoadFlatMortality = (DropDownList)e.Row.FindControl("ddlExistingLoadFlatMort");
            BindMasterDetails(ddlLoadFlatMortality);


            //FillDropDownList(ddl1, "followupcode", "Mst");
            _ds = (DataTable)ViewState["ExitingLoadingData"];
            if (_ds.Rows.Count > 0)
            {
                //DropDownList ddlfollowupcode = (DropDownList)e.Row.FindControl("ddlfollowupcode");
                ddlLoadType.SelectedValue = _ds.Rows[e.Row.RowIndex]["LaodingCode"].ToString();

                Label RiderName = (Label)e.Row.FindControl("lblRiderName");
                RiderName.Text = _ds.Rows[e.Row.RowIndex]["RiderName"].ToString();
                //DropDownList ddlCategory = (DropDownList)e.Row.FindControl("ddlCategory");
                ddlLoadRsn1.SelectedValue = _ds.Rows[e.Row.RowIndex]["LoadReasoncode1"].ToString();

                //DropDownList ddlCriteria = (DropDownList)e.Row.FindControl("ddlCriteria");
                ddlLoadRsn2.SelectedValue = _ds.Rows[e.Row.RowIndex]["LoadReasoncode2"].ToString();

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
            ddlUWreason.SelectedIndex = 0;
            ddlUWDecesion.Enabled = true;
        }
        catch (Exception ex)
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
                gvLoadingDtls.DataSource = dt;
                gvLoadingDtls.DataBind();
            }
        }
        catch (Exception ex)
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
        catch (Exception ex)
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
                    objcomm.ManageRiderInfo(strApplicationno, dtRiderInfo, strUserId, ref intResponse);//Parameter change by shyam Patil
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
                if (ddlUWDecesion.Items.FindByValue("Pending") != null)
                {
                    ddlUWDecesion.Items.FindByValue("Pending").Selected = true;
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
        catch (Exception ex)
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
            //txtPreferredDate.Text = Convert.ToString(dt.Rows[0]["PREFERRED_DATE"]);
            //txtAmount.Text = Convert.ToString(dt.Rows[0]["MANDAMT"]);
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        if (dt != null && dt.Rows.Count > 0)
        {
            txtBMI.Text = Convert.ToString(dt.Rows[0]["DATA"]);
            //if (Convert.ToInt32(dt.Rows[1]["DATA"]) == 0)
            //{
            //    chkRiskParamIsSmoker.Checked = false;
            //}
            //else
            //{
            //    chkRiskParamIsSmoker.Checked = true;
            //}
            /*ID:9 START*/
            txtSaralRiskBmi.Text = Convert.ToString(dt.Rows[0]["DATA"]);
            txtHeight.Text = Convert.ToString(dt.Rows[0]["HEIGHT"]);
            txtWeight.Text = Convert.ToString(dt.Rows[0]["WEIGHT"]);
            //if (Convert.ToInt32(dt.Rows[1]["DATA"]) == 0)
            //{
            //    chkSaralRiskIsSmoker.Checked = false;
            //}
            //else
            //{
            //    chkSaralRiskIsSmoker.Checked = true;
            //}
            /*ID:9 END*/
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
                if (Convert.ToInt32(txtENYScore.Text) >= 119)
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
            if (_dsstate.Tables.Count > 0)
            {
                if (_dsstate.Tables[0].Rows.Count > 0)
                {
                    strbranchstate = Convert.ToString(_dsstate.Tables[0].Rows[0]["STATE"]).ToUpper().TrimEnd();
                }
                if (_dsstate.Tables[1].Rows.Count > 0)
                {
                    strcuststate = Convert.ToString(_dsstate.Tables[1].Rows[0]["ADR_state"]).ToUpper().TrimEnd();
                }
            }

            //string[] strBranchName = { "New Delhi Pitampura", "EZone Rohini New Delhi", "Gwalior", "Jaipur", "Bangalore", "Guwahati", "Nashik", "Hyderabad", "Gorakhpur", "Bhubaneshwar", "Baroda", "Ahmedabad", "Bhopal", "Aligarh" };
            //for (int i = 0; i < strBranchName.Length; i++)
            //{
            //_dsstate.Tables[2].Rows.Count == 0 this condition is for high risk city is exist
            if (txtdistance.Text != null && txtdistance.Text != "" && _dsstate.Tables.Count > 0 && _dsstate.Tables[0].Rows.Count > 0)
            {
                if (_dsstate.Tables[2].Rows.Count > 0 && (Convert.ToDouble(txtdistance.Text) > 50 || !strbranchstate.Equals(strcuststate)) && (Convert.ToString(txtSaralRiskChannel.Text).Equals("Direct Sales") || Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-Core") || Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-IM")))
                {
                    if (!strbranchstate.Equals(strcuststate))
                    {

                    }
                    FillWarningMessage("31");
                    DisplaySaralWarningMessage();
                }
                if (_dsstate.Tables[2].Rows.Count == 0 && (Convert.ToDouble(txtdistance.Text) > 80 || !strbranchstate.Equals(strcuststate)) && (Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-Core") || Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-IM")))
                {
                    FillWarningMessage("23");
                    DisplaySaralWarningMessage();
                }
                if (_dsstate.Tables[2].Rows.Count == 0 && (Convert.ToDouble(txtdistance.Text) > 80 || !strbranchstate.Equals(strcuststate)) && (Convert.ToString(txtSaralRiskChannel.Text).Equals("Direct Sales")))
                {
                    FillWarningMessage("24");
                    DisplaySaralWarningMessage();
                }
            }
            //}
            if (((txtdistance.Text != null && txtdistance.Text != "" && Convert.ToDouble(txtdistance.Text) > 80) || !strbranchstate.Equals(strcuststate)) && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Direct Sales") && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-Core") && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Tied Agency-IM") && !Convert.ToString(txtSaralRiskChannel.Text).Equals("Bankassurance-AU"))
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
        Logger.Info(strApplicationno + "PAGE_NAME:UWPortfolio/UWdecision/btnSaveToDatabase //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Funcation Execution Begin" + Environment.NewLine);
        ReplicaXml objReplica = new ReplicaXml();
        DataSet _dsPremium = new DataSet();
        int intResponse = -1;
        string strResponse = string.Empty;
        bool blnFalse = !false;
        bool isdataSave = true;
        if (hdnPremPayingStatus.Value.Trim() != "Lapsed" ||
                    hdnPremPayingStatus.Value.Trim() != "HA" || hdnPremPayingStatus.Value.Trim() != "AC"
                    || hdnPremPayingStatus.Value.Trim() != "PH" || hdnPremPayingStatus.Value.Trim() != "UD" || hdnPolicyStatus.Value.Trim() != "LA" ||
                    hdnPolicyStatus.Value.Trim() != "PU" || hdnPolicyStatus.Value.Trim() != "DU" || hdnPolicyStatus.Value.Trim() != "HP")
        {
            //PremiumCalculation(objReplica, ref isdataSave, ref _dsPremium, ref intResponse, ref strResponse);

            isdataSave = true;
        }

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
                                            int intRetVal = -1;
                                            string strEventkey = string.Empty;

                                            if (ddlUWDecesion.SelectedValue != "Pending")
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
                                                objBuss.Save_Country("OFFLINE", strApplicationno, Convert.ToString(ddlcountry.SelectedValue), objChangeObj.userLoginDetails._UserID, ref resp1);
                                                //Mode change by Shyam Patil
                                            }
                                            //end
                                            /*ID:13 START*/
                                            //ManagePolicyPrinting();
                                            /*ID:13 END*/

                                            //Brijesh start 
                                            string RelationStaff = ddlRelationStaff.SelectedValue == "0" ? "" : ddlRelationStaff.SelectedValue;
                                            objBuss.Save_StaffDetails(strApplicationno, RelationStaff, hd_que_2.Checked, "", "UW", objChangeObj.userLoginDetails._UserID, ref resp1);
                                            //end  
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
        string strPipe = "|";
        if (lblErrorcommentdtls.Text == "")
        {
            ShowPopupMessage("Please enter comment..!");
            throw new Exception("UDE-Please enter comment..!");
        }
        if (strChannelType.Equals("online"))
        {
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
            if (!ddlUWDecesion.SelectedValue.Equals("Pending"))
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
                        ShowPopupMessage("Enter CIBIL Score Before Approving");
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

                    // Start Brijesh Pandey
                    if (hd_que_2.Checked == true  && ddlRelationStaff.SelectedValue == "0")
                    {
                        ShowPopupMessage("Please select Relation with Staff Dropdown in Application Details!!");
                        throw new Exception("UDE-Please select Relation with Staff Dropdown in Application Details!!");
                    }
                    // End
                }

                //strUserId
                if (IsUserLimitLessThanSumAssured())
                {
                    ShowPopupMessage("You Cannot Process the case since your limit is less than Sum Assured");
                    throw new Exception("UDE-You Cannot Process the case since your limit is less than Sum Assured");
                }
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
            if (ddlApplicationDetailsProposalType.SelectedValue.Equals("NRI"))
            {
                if (ddlcountry.SelectedValue.Equals("0"))
                {
                    ShowPopupMessage("Please select Residence country of customer");
                    throw new Exception("UDE-Please select Residence country of customer");
                }

            }
            //else
            //{
            //    divassign.Attributes.Add("Class", "HideControl");
            //}

            bool blnIsCheckedDecisionType = true;
            //if (!ddlUWDecesion.SelectedValue.Equals("Pending"))
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
                //if (!li.Selected)
                //{
                //    if (Convert.ToInt32(li.Value) == 14 && txtSaralRiskScore.Text != null && txtSaralRiskScore.Text != "" && Convert.ToInt32(txtSaralRiskScore.Text) > 70)
                //    {
                //        ShowPopupMessage("HOD approval in decision type is mandatory for this risk score");
                //        throw new Exception("UDE-HOD approval is mandatory for this risk score");
                //    }
                //}
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
    }
    /*END HERE*/
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
        catch (Exception ex)
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
                    DataSet _dsPremiumCalculation;
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
                int _ProdResult = 0;
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

                    DataSet _dsPremiumCalculation;
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
                    int _RiderResult = 0;
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
                            DataTable dtDbRiderInfo = new DataTable();
                            DefineDataTable(ref dtDbRiderInfo);
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

                                //add data row to datatable
                                dtDbRiderInfo.Rows.Add(dr);
                            }
                            Logger.Info(strApplicationno + "Remark:" + "ManageProductRiderDetails" + "Rider Details save in database Begin" + System.Environment.NewLine);
                            objcomm.ManageRiderInfo(strApplicationno, dtDbRiderInfo, strUserId, ref intResponse);
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
                bool _FollowupStatus = false;
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
                        _FollowupStatus = true;
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
                int strFollowupResult = 0;
                bool _FollowupStatus = false;
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
                        _FollowupStatus = true;
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
                bool _FollowupStatus = false;
                bool _strFundflag = true;
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
                int strFollowupResult = 0;
                bool _FollowupStatus = false;
                bool _strFundflag = true;
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
            lblErrorloadingdtls.Text = string.Empty;
            int LoadingResult = 0;
            //objCommonObj = (CommonObject)Session["objCommonObj"];
            objChangeObj = (ChangeValue)Session["objLoginObj"];
            // objChangeObj = (ChangeValue)ViewState["LoadingData"];
            Logger.Info(strApplicationno + "STAG 15 :PageName :Uwdecision.aspx.CS // MethodeName :btnLoadingDtlsSave_Click" + System.Environment.NewLine);
            string _strLoadRiderName = string.Empty;
            string _strLoadType = string.Empty;
            string _strLoadDiscp = string.Empty;
            string _strLoadReason1 = string.Empty;
            string _strLoadReason1Discp = string.Empty;
            string _strLoadReason2 = string.Empty;
            string _strLoadReason2Discp = string.Empty;
            string _strLoadPercent = string.Empty;
            string _strRateAdjust = string.Empty;
            string _strFlatmortality = string.Empty;
            string _strLetterPrint = string.Empty;
            string _strRiderCode = string.Empty;
            string _strLoadCode = string.Empty;
            string _strReason1code = string.Empty;
            string _strReason2code = string.Empty;
            int strTotalLoadedPremiumAB = 0;
            string strComponentCode = string.Empty;
            string strComponentRiderType = string.Empty;
            int TotalPremium = 0;
            int ExtraPremAmt = 0;
            int ModalPremiumAmt = 0;
            int MedicalLoadingPremium = 0;
            int NonMedicalLoadingPremium = 0;
            _dtLoaddata = (DataTable)ViewState["LoadingData"];
            int i = 0;
            //pull value from gridview        
            List<LoadingSection> lstLoading = new List<LoadingSection>();
            foreach (GridViewRow objGridViewRow in gvExtLoadDetails.Rows)
            {
                Logger.Info(strApplicationno + "Save data from Existing loading grid begin" + System.Environment.NewLine);
                LoadingSection objLoadingSection = new LoadingSection();
                objLoadingSection.Section = "LoadingDetails";
                DropDownList ddlExstingLoadType = (DropDownList)objGridViewRow.FindControl("ddlExstingLoadType");

                Label lblRiderName = (Label)objGridViewRow.FindControl("lblRiderName");
                DropDownList ddlExistingLoadRsn1 = (DropDownList)objGridViewRow.FindControl("ddlExistingLoadRsn1");
                DropDownList ddlExistingLoadRsn2 = (DropDownList)objGridViewRow.FindControl("ddlExistingLoadRsn2");
                Label lblExistingLoadPer = (Label)objGridViewRow.FindControl("lblExistingLoadPer");
                Label lblExistingRateAdjust = (Label)objGridViewRow.FindControl("lblExistingRateAdjust");
                DropDownList ddlExistingLoadFlatMort = (DropDownList)objGridViewRow.FindControl("ddlExistingLoadFlatMort");
                DropDownList LetterPrint = (DropDownList)objGridViewRow.FindControl("LetterPrint");


                objLoadingSection.ApplicationNo = strApplicationno;
                objLoadingSection.RiderName = lblRiderName.Text;
                objLoadingSection.LoadingType = ddlExstingLoadType.SelectedValue;
                objLoadingSection.LoadingDiscp = ddlExstingLoadType.SelectedItem.Text;
                objLoadingSection.LoadReasoncode1 = ddlExistingLoadRsn1.SelectedValue;
                objLoadingSection.LoadReasonCode2 = ddlExistingLoadRsn2.SelectedValue;
                objLoadingSection.RateAdjustment = lblExistingRateAdjust.Text;
                objLoadingSection.FlatMortality = ddlExistingLoadFlatMort.SelectedValue;
                objLoadingSection.IsLetterPrint = LetterPrint.SelectedValue;
                objLoadingSection.RiderCode = lblRiderName.Text;
                objLoadingSection.LaodingCode = ddlExstingLoadType.SelectedValue;
                objLoadingSection.Loading = lblExistingLoadPer.Text;
                _strReason1code = Convert.ToString(ddlExistingLoadRsn1.SelectedItem);
                _strReason2code = Convert.ToString(ddlExistingLoadRsn2.SelectedItem);
                _strLoadReason1Discp = Convert.ToString(ddlExistingLoadRsn1.SelectedItem);
                _strLoadReason2Discp = Convert.ToString(ddlExistingLoadRsn2.SelectedItem);

                if (blnUpdateInLa)
                {
                    //objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, objLoadingSection.LoadingType, 
                    //_strLoadDiscp,objLoadingSection.LoadReasoncode1 , objLoadingSection.LoadReasonCode2, "", txtSumassure.Text, objLoadingSection.Loading,objLoadingSection.RateAdjustment, objLoadingSection.FlatMortality, txtPrepayterm.Text, string.Empty, objLoadingSection.RiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno, ref LoadingResult);
                    Logger.Info(strApplicationno + "Remark : Save data to database begin" + System.Environment.NewLine);
                    objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, objLoadingSection.LoadingType,
                       objLoadingSection.LoadingDiscp, objLoadingSection.LoadReasoncode1, objLoadingSection.LoadReasonCode2, "", txtSumassure.Text, objLoadingSection.Loading, objLoadingSection.RateAdjustment, objLoadingSection.FlatMortality, txtPrepayterm.Text, string.Empty, objLoadingSection.RiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno,"", ref LoadingResult);

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
                                if (strChannelType.ToUpper() == "OFFLINE")
                                {
                                    //objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, strComponentCode, "", "", strComponentRiderType, 0, Convert.ToDecimal(TotalPremium), ddlLoadRsn2.SelectedItem.Text, 0, Convert.ToDecimal(ExtraPremAmt), Convert.ToDecimal(ServiceTax), 0, Convert.ToDecimal(ExtraPremAmt), 0, 0, 0, 0, 0, 0, 0, ref strLApushErrorCode);
                                    objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, ComponentCd, RiderPPT, RiderPT, RiderCtg, BackdatedInt, TotalInstalmentPremium, ddlLoadRsn1.SelectedValue, ModalPremiumAmt, ExtraPremiumAmt, LoadingSeriveTax, EduCess, 0, SumAssured, SumAssuredAcrossPlans, MedicalLoadingPremium, MedicalLoadingRate, NonMedicalLoadingPremium, 0, NonMedicalLoadingRate, ref strLApushErrorCode);
                                }
                            }
                        }
                    }

                    if (LoadingResult > 0)
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
                lstLoading.Add(objLoadingSection);
                Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + System.Environment.NewLine);
                Logger.Info(strApplicationno + "MethodeName :ManageLoadingDetails Object" + GetXMLFromObject(objLoadingSection) + System.Environment.NewLine);
            }

            //pull value from view state
            if (_dtLoaddata != null && _dtLoaddata.Rows.Count > 0)
            {
                Logger.Info(strApplicationno + "Save data from Selecting loading  begin" + System.Environment.NewLine);
                for (i = 0; i < _dtLoaddata.Rows.Count; i++)
                {
                    LoadingSection objLoadingSection = new LoadingSection();
                    objLoadingSection.Section = "LoadingDetails";
                    objLoadingSection.ApplicationNo = strApplicationno;
                    objLoadingSection.RiderName = _strLoadRiderName = _dtLoaddata.Rows[i]["RiderName"].ToString();
                    objLoadingSection.LoadingType = _strLoadType = _dtLoaddata.Rows[i]["ddlLoadType"].ToString();
                    objLoadingSection.LoadingDiscp = _strLoadDiscp = _dtLoaddata.Rows[i]["LoadingDiscp"].ToString();
                    objLoadingSection.LoadReasoncode1 = _strLoadReason1 = _dtLoaddata.Rows[i]["ddlLoadRsn1Code"].ToString();
                    objLoadingSection.LoadReasonCode2 = _strLoadReason2 = _dtLoaddata.Rows[i]["ddlLoadRsn2Code"].ToString();
                    objLoadingSection.RateAdjustment = _strRateAdjust = _dtLoaddata.Rows[i]["txtRateAdjust"].ToString();
                    objLoadingSection.FlatMortality = _strFlatmortality = (_dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString() == "0") ? "" : _dtLoaddata.Rows[i]["ddlLoadFlatMortality"].ToString();
                    objLoadingSection.IsLetterPrint = _strLetterPrint = _dtLoaddata.Rows[i]["LetterPrint"].ToString();
                    objLoadingSection.RiderCode = _strRiderCode = _dtLoaddata.Rows[i]["RiderCode"].ToString();
                    objLoadingSection.LaodingCode = _strLoadCode = _dtLoaddata.Rows[i]["ddlLoadCode"].ToString();
                    objLoadingSection.Loading = _strLoadPercent = _dtLoaddata.Rows[i]["txtLoadPer"].ToString();
                    _strReason1code = _dtLoaddata.Rows[i]["ddlLoadRsn1Code"].ToString();
                    _strReason2code = _dtLoaddata.Rows[i]["ddlLoadRsn2Code"].ToString();
                    _strLoadReason1Discp = _dtLoaddata.Rows[i]["Reason1Discp"].ToString();
                    _strLoadReason2Discp = _dtLoaddata.Rows[i]["Reason2Discp"].ToString();
                    if (blnUpdateInLa)
                    {
                        Logger.Info(strApplicationno + "Save View state Loading data from" + System.Environment.NewLine);
                        objComm.OnlineLoadingDetails_Save(objCommonObj._AppType, strApplicationno, _strLoadCode, _strLoadDiscp, _strReason1code, _strReason2code, "", txtSumassure.Text, txtLoadPer.Text, _strRateAdjust, _strFlatmortality, txtPrepayterm.Text, _strLetterPrint, _strLoadRiderName, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, strApplicationno,"", ref LoadingResult);
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
                                        objComm.OnlinePremiumExtraLoadingdetails_Save(0, strApplicationno, txtPolno.Text, ComponentCd, RiderPPT, RiderPT, RiderCtg, BackdatedInt, TotalInstalmentPremium, ddlLoadRsn1.SelectedValue, ModalPremiumAmt, ExtraPremiumAmt, LoadingSeriveTax, EduCess, 0, SumAssured, SumAssuredAcrossPlans, MedicalLoadingPremium, MedicalLoadingRate, NonMedicalLoadingPremium, 0, NonMedicalLoadingRate, ref strLApushErrorCode);
                                    }
                                }
                            }
                        }
                        /*end here*/
                        if (LoadingResult > 0)
                        {
                            Logger.Info(strApplicationno + "Save View state Loading data to database Success" + System.Environment.NewLine);
                            //lblErrorloadingdtls.Text += " Loading Details Save Successfully In DataBase";
                            //lblErrorLoadingDetailBody.Text += " Loading Details Save Successfully In DataBase";
                        }
                        else
                        {
                            Logger.Info(strApplicationno + "Save View state Loading data to database Failed" + System.Environment.NewLine);
                            //lblErrorloadingdtls.Text += " Loading Details Not Save Successfully In DataBase Click Here To Know More";
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

    private void ManageApplicatoinStatus(ref bool isdataSave)
    {
        Logger.Info(strApplicationno + "STEP :7 Save Application Status details Start" + System.Environment.NewLine);
        Logger.Info(strApplicationno + "Remark:" + "Application Stataus" + "STAG 8 :PageName :Uwdecision.aspx.CS // MethodeName :ManageApplicatoinStatus" + System.Environment.NewLine);
        try
        {


            if (Session["objLoginObj"] != null)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
                int intRetVal = -1;
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

    //########################################################## FG Code Non Revival Cases Binding Panel data #####################################################################
    private void FillUwDecision(DataSet _dsPageData)
    {
        DivPolicyDetails.Visible = false;
        DivPremiumDetails1.Visible = false;
        divAgentDetail.Visible = false;
        divCustDetails.Visible = false;
        foreach (DataTable dt in _dsPageData.Tables)
        {
            if (dt.Rows.Count > 0)
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
                        UWFollowupServiceCall(strApplicationno, strChannelType);
                        clsName = divRequirementDetails.Attributes["class"].ToString();
                        divRequirementDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                        break;

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
                        BindExcelRiskParameter(dt);
                        break;
                    /*ID:9 END*/
                    /*ID:10 START*/
                    case "DECISION_TYPE":
                        BindDecisionType(dt);
                        break;
                    /*ID:10 END*/
                    /*ID:11 START*/
                    case "WARNING":
                        BindUwWarning(dt);
                        break;
                    case "HINT":
                        BindUwHint(dt);
                        break;
                    /*ID:11 END*/
                    case "DISTANCE":
                        Binddistance(dt);
                        break;
                    case "TransactionNo":
                        BindAadharTransactionNo(dt);
                        break;
                        //case "PremPolDetails":
                        //    FillPremiumDetails(dt);
                        //    break;

                }
            }
        }
    }
    //########################################################## FG Code Non Revival Cases Binding Panel data #####################################################################



    //##########################################################  Revival Cases Binding Panel data ################################################################################
    private void FillUwDecisionRevival(DataSet _dsPageData)
    {
        divApplicationDetails.Visible = false;
        divProductDetails.Visible = false;
        divPaymentDetails.Visible = false;
        divJournalDetails.Visible = false;
        PreIssue_containerBody.Visible = false;
        divBankDetails.Visible = false;
        divClientDetails.Visible = false;
        divAgentDetails.Visible = false;
        divPanDetails.Visible = false;
        divAadharDetails.Visible = false;
        divOnlineAml.Visible = false;
        divOfflineAml.Visible = false;
        AuResult_ContainerBody.Visible = false;
        //nonRevivalPanel.Visible = false;

        foreach (DataTable dt in _dsPageData.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                switch (dt.Rows[0][0].ToString())
                {
                    case "PolicyPremDetails":
                        //BindPremiumDetails(dt);
                        FillPremiumDetails(strApplicationno, strPolicyNo);
                        break;

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

                    //case "ProductDetails":
                    //    BindProductDetails(dt);
                    //    clsName = divProductDetails.Attributes["class"].ToString();
                    //    divProductDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                    //    break;
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
                    //case "RiderDetails":
                    //    BindRiderDetails(dt);
                    //    break;

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
                        UWFollowupServiceCall(strApplicationno, strChannelType);
                        clsName = divRequirementDetails.Attributes["class"].ToString();
                        divRequirementDetails.Attributes.Add("class", clsName.Replace(clsName, "col-md-12"));
                        break;

                    case "LoadingDetails ":
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
                        BindExcelRiskParameter(dt);
                        break;
                    /*ID:9 END*/
                    /*ID:10 START*/
                    case "DECISION_TYPE":
                        BindDecisionType(dt);
                        break;
                    /*ID:10 END*/
                    /*ID:11 START*/
                    case "WARNING":
                        BindUwWarning(dt);
                        break;
                    case "HINT":
                        BindUwHint(dt);
                        break;
                    /*ID:11 END*/
                    case "DISTANCE":
                        Binddistance(dt);
                        break;
                    case "TransactionNo":
                        BindAadharTransactionNo(dt);
                        break;
                        //case "PremPolDetails":
                        //    FillPremiumDetails(dt);
                        //    break;

                }
            }
        }
    }

    //##########################################################  Revival Cases Binding Panel data ################################################################################

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
        if (_dtExistingLoad != null)
        {
            if (_dtExistingLoad.Rows.Count > 0)
            {
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
                        objLoadParamBS.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingBS);
                        objLoadParamBS.strMedicalLoadingClass = Convert.ToString(strMortalityClassBS);
                        objLoadParamBS.strRiderCtg = Convert.ToString(dtRow["RiderCtg"]);
                    }
                    else
                    {
                        //objLoadParamAD = new LoadParam();
                        if (!dtRow["LoadingType"].ToString().ToUpper().Trim().Equals("MEDICAL"))
                        {
                            strNonMedicalLoadingAD += string.IsNullOrEmpty(dtRow["RateAdjustment"].ToString()) ? 0 : Convert.ToInt32(dtRow["RateAdjustment"]);
                        }
                        else
                        {
                            strMortalityClassAD = Convert.ToString(dtRow["FlatMortality"].ToString().Trim());
                        }
                        objLoadParamAD.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingAD);
                        objLoadParamAD.strMedicalLoadingClass = Convert.ToString(strMortalityClassAD);
                        objLoadParamAD.strRiderCtg = Convert.ToString(dtRow["RiderCtg"]);
                    }
                }
            }
        }

        if (_dtNewLoading != null)
        {
            if (_dtNewLoading.Rows.Count > 0)
            {
                foreach (DataRow dtRow in _dtNewLoading.Rows)
                {
                    DataView dv = new DataView(_dtLoadMaster);
                    string strRiderName = dtRow["LoadType"].ToString();
                    dv.RowFilter = "NAME = '" + strRiderName + "'"; //+ dtRow["RiderName"].ToString();
                    string strRiderCtg = string.Empty;
                    strRiderCtg = dv[0]["RiderCtg"].ToString();
                    if (strRiderCtg.Trim().ToUpper().Equals("BS"))
                    {
                        if (!dtRow["ddlLoadType"].ToString().ToUpper().Trim().Equals("MEDICAL"))
                        {
                            strNonMedicalLoadingBS += string.IsNullOrEmpty(dtRow["txtRateAdjust"].ToString()) ? 0 : Convert.ToInt32(dtRow["txtRateAdjust"]);
                        }
                        else
                        {
                            strMortalityClassBS = Convert.ToString(dtRow["ddlLoadFlatMortality"].ToString().Trim());
                        }
                        objLoadParamBS.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingBS);
                        objLoadParamBS.strMedicalLoadingClass = Convert.ToString(strMortalityClassBS);
                        objLoadParamBS.strRiderCtg = strRiderCtg;
                    }
                    else
                    {
                        if (!dtRow["ddlLoadType"].ToString().ToUpper().Trim().Equals("MEDICAL"))
                        {
                            strNonMedicalLoadingAD += string.IsNullOrEmpty(dtRow["txtRateAdjust"].ToString()) ? 0 : Convert.ToInt32(dtRow["txtRateAdjust"]);
                        }
                        else
                        {
                            strMortalityClassAD = Convert.ToString(dtRow["ddlLoadFlatMortality"].ToString().Trim());
                        }
                        objLoadParamAD.strNonMedicalLoading = Convert.ToString(strNonMedicalLoadingAD);
                        objLoadParamAD.strMedicalLoadingClass = Convert.ToString(strMortalityClassAD);
                        objLoadParamAD.strRiderCtg = strRiderCtg;
                    }

                }
            }
        }
        if (!string.IsNullOrEmpty(objLoadParamBS.strRiderCtg))
        {
            lstLoadParam.Add(objLoadParamBS);
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
        string response = "";
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
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
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, "OFFLINE", objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                objBuss.InsertUserChangesLog(strApplicationno, (strUserId.IndexOf('F') == 0) ? strUserId.Substring(1) : strUserId, "SMOKER", Convert.ToString(chkRiskParamIsSmoker.Checked), ref intRet);
                lblErrorDetailsRiskParameter.Text = "Smoker Updated Successfully";
            }
            else
            {
                lblErrorDetailsRiskParameter.Text = "Smoker Not Updated Successfully";
            }
        }
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        if (Convert.ToInt64(hdnUserLimit.Value) < Convert.ToInt64(txtSumassure.Text))
        {
            return true;
        }
        else
        {
            return false;
        }
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
            catch (Exception ex)
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
        string url = "MedicalDataEntryNew.aspx?qsAppNo=" + strApplicationno + "&qsPolicyNo=" + strPolicyNo;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        //Response.Redirect("MedicalDataEntryNew.aspx");
    }

    //1.2 Begin of Changes by Rupali
    public void getFGData()
    {
        DataSet ds = objComm.getFGDataShow(strApplicationno, "DE");

        string strEnvironment = System.Configuration.ConfigurationManager.AppSettings["Environment"].ToString();
        btnFetchIIBQuery.Enabled = true;

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //first dataset contains data with no 133 and second dataset contains data not equal to 133
                    GridViewFG.DataSource = ds.Tables[0];
                    GridViewFG.DataBind();
                    GridViewFG.Visible = true;
                }
            }

            if (ds.Tables[1] != null)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    GridViewOther.DataSource = ds.Tables[1];
                    GridViewOther.DataBind();
                    GridViewOther.Visible = true;
                    lblothercompany.Visible = true;
                    lblothercompany.ForeColor = System.Drawing.Color.Red;
                    lblothercompany.Text = "The existing policies found in other insurance companies,please ensure to collect the information from client in proposal form / declaration";
                }
            }

            if (ds.Tables[0] != null && ds.Tables[1] != null && ds.Tables[2] != null)
            {
                if ((ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0) && ds.Tables[2].Rows.Count > 0)
                {
                    btnFetchIIBQuery.Enabled = strEnvironment == "UAT" ? true : false;
                }
            }
        }
    }
    protected void btnFetchIIBQuery_Click(object sender, EventArgs e)
    {
        try
        {
            List<multiplexml> mxml = new List<multiplexml>();
            DataSet dt = objComm.FetchRevivalIIBData(strApplicationno.Trim());
            if (dt.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    if (dt.Tables[0].Rows[i]["CLRRROLEDesc"].ToString() == "Life_Assured")
                    {
                        multiplexml cxml = new multiplexml();
                        cxml.Policy_Number = dt.Tables[0].Rows[i]["Policy_Number"].ToString();
                        cxml.Proposal_Number = dt.Tables[0].Rows[i]["Proposal_Number"].ToString();
                        cxml.Query_Type = dt.Tables[0].Rows[i]["Query_Type"].ToString();
                        //cxml.DoP_DoC = Convert.ToDateTime(txtAppsigndate.Text).ToString("yyyy-MM-dd");//txtAppSignDate.Text
                        if (string.IsNullOrEmpty(dt.Tables[0].Rows[i]["DoP_DoC"].ToString()))
                        {
                            cxml.DoP_DoC = "";
                        }
                        else
                        {
                            cxml.DoP_DoC = Convert.ToDateTime(dt.Tables[0].Rows[i]["DoP_DoC"].ToString()).ToString("yyyy-MM-dd");

                        }
                        cxml.Sum_Assured = dt.Tables[0].Rows[i]["Sum_Assured"].ToString();
                        cxml.LA_First_Name = dt.Tables[0].Rows[i]["LA_First_Name"].ToString();
                        cxml.LA_Middle_Name = "";
                        cxml.LA_Last_Name = dt.Tables[0].Rows[i]["LA_Last_Name"].ToString();
                        if (string.IsNullOrEmpty(dt.Tables[0].Rows[i]["LA_DoB"].ToString()))
                        {
                            cxml.LA_DoB = "";
                        }
                        else
                        {
                            cxml.LA_DoB = Convert.ToDateTime(dt.Tables[0].Rows[i]["LA_DoB"].ToString()).ToString("yyyy-MM-dd");

                        }
                        cxml.LA_Gender = dt.Tables[0].Rows[i]["LA_Gender"].ToString();
                        cxml.LA_Current_Address = "";
                        cxml.LA_Permanent_Address = "";
                        cxml.LA_Pin_Code = dt.Tables[0].Rows[i]["LA_Pin_Code"].ToString();
                        cxml.LA_PAN = dt.Tables[0].Rows[i]["LA_PAN"].ToString();//"BABPK3030L"; //"ASUPK5335L";         //"BABPK3030L";
                        cxml.LA_Aadhar = "";
                        cxml.LA_Ckyc = "";
                        cxml.LA_Passport = "";
                        cxml.LA_DL = "";
                        cxml.LA_Voter_Id = "";
                        cxml.LA_Phone_Number_1 = "";
                        cxml.LA_Phone_Number_2 = "";
                        cxml.LA_Email_1 = "";
                        cxml.LA_Email_2 = "";
                        cxml.Date_of_Death = "";
                        cxml.type = "LA";
                        mxml.Add(cxml);
                    }
                    else if (dt.Tables[0].Rows[i]["CLRRROLEDesc"].ToString() == "proposer")
                    {
                        multiplexml cxml = new multiplexml();
                        cxml.Policy_Number = dt.Tables[0].Rows[i]["Policy_Number"].ToString();
                        cxml.Proposal_Number = dt.Tables[0].Rows[i]["Proposal_Number"].ToString();
                        cxml.Query_Type = dt.Tables[0].Rows[i]["Query_Type"].ToString();
                        //cxml.DoP_DoC = Convert.ToDateTime(dt.Tables[0].Rows[i]["bpm_creationDate"].ToString()).ToString("yyyy-MM-dd");//txtAppSignDate.Text
                        if (string.IsNullOrEmpty(dt.Tables[0].Rows[i]["DoP_DoC"].ToString()))
                        {
                            cxml.DoP_DoC = "";
                        }
                        else
                        {
                            cxml.DoP_DoC = Convert.ToDateTime(dt.Tables[0].Rows[i]["DoP_DoC"].ToString()).ToString("yyyy-MM-dd");

                        }
                        cxml.Sum_Assured = dt.Tables[0].Rows[i]["Sum_Assured"].ToString();
                        cxml.LA_First_Name = dt.Tables[0].Rows[i]["LA_First_Name"].ToString();
                        cxml.LA_Middle_Name = "";
                        cxml.LA_Last_Name = dt.Tables[0].Rows[i]["LA_Last_Name"].ToString();
                        //cxml.LA_DoB = Convert.ToDateTime(dt.Tables[0].Rows[i]["CLT_dob_CLTDOBX"].ToString()).ToString("yyyy-MM-dd");
                        if (string.IsNullOrEmpty(dt.Tables[0].Rows[i]["LA_DoB"].ToString()))
                        {
                            cxml.LA_DoB = "";
                        }
                        else
                        {
                            cxml.LA_DoB = Convert.ToDateTime(dt.Tables[0].Rows[i]["LA_DoB"].ToString()).ToString("yyyy-MM-dd");

                        }
                        cxml.LA_Gender = dt.Tables[0].Rows[i]["LA_Gender"].ToString();
                        cxml.LA_Current_Address = "";
                        cxml.LA_Permanent_Address = "";
                        cxml.LA_Pin_Code = dt.Tables[0].Rows[i]["LA_Pin_Code"].ToString();
                        cxml.LA_PAN = dt.Tables[0].Rows[i]["LA_PAN"].ToString();//"BABPK3030L"; //"ASUPK5335L";         //"BABPK3030L";
                        cxml.LA_Aadhar = "";
                        cxml.LA_Ckyc = "";
                        cxml.LA_Passport = "";
                        cxml.LA_DL = "";
                        cxml.LA_Voter_Id = "";
                        cxml.LA_Phone_Number_1 = "";
                        cxml.LA_Phone_Number_2 = "";
                        cxml.LA_Email_1 = "";
                        cxml.LA_Email_2 = "";
                        cxml.Date_of_Death = "";
                        cxml.type = "proposer";
                        mxml.Add(cxml);
                    }
                    else if (dt.Tables[0].Rows[i]["CLRRROLEDesc"].ToString() == "payer")
                    {
                        multiplexml cxml = new multiplexml();
                        cxml.Policy_Number = dt.Tables[0].Rows[i]["Policy_Number"].ToString();
                        cxml.Proposal_Number = dt.Tables[0].Rows[i]["Proposal_Number"].ToString();
                        cxml.Query_Type = dt.Tables[0].Rows[i]["Query_Type"].ToString();
                        // cxml.DoP_DoC = Convert.ToDateTime(dt.Tables[0].Rows[i]["bpm_creationDate"].ToString()).ToString("yyyy-MM-dd");//txtAppSignDate.Text   
                        if (string.IsNullOrEmpty(dt.Tables[0].Rows[i]["DoP_DoC"].ToString()))
                        {
                            cxml.DoP_DoC = "";
                        }
                        else
                        {
                            cxml.DoP_DoC = Convert.ToDateTime(dt.Tables[0].Rows[i]["DoP_DoC"].ToString()).ToString("yyyy-MM-dd");

                        }
                        cxml.Sum_Assured = dt.Tables[0].Rows[i]["Sum_Assured"].ToString();
                        cxml.LA_First_Name = dt.Tables[0].Rows[i]["LA_First_Name"].ToString();
                        cxml.LA_Middle_Name = "";
                        cxml.LA_Last_Name = dt.Tables[0].Rows[i]["LA_Last_Name"].ToString();
                        //cxml.LA_DoB = Convert.ToDateTime(dt.Tables[0].Rows[i]["CLT_dob_CLTDOBX"].ToString()).ToString("yyyy-MM-dd");
                        if (string.IsNullOrEmpty(dt.Tables[0].Rows[i]["LA_DoB"].ToString()))
                        {
                            cxml.LA_DoB = "";
                        }
                        else
                        {
                            cxml.LA_DoB = Convert.ToDateTime(dt.Tables[0].Rows[i]["LA_DoB"].ToString()).ToString("yyyy-MM-dd");

                        }
                        cxml.LA_Gender = dt.Tables[0].Rows[i]["LA_Gender"].ToString();
                        cxml.LA_Current_Address = "";
                        cxml.LA_Permanent_Address = "";
                        cxml.LA_Pin_Code = dt.Tables[0].Rows[i]["LA_Pin_Code"].ToString();
                        cxml.LA_PAN = dt.Tables[0].Rows[i]["LA_PAN"].ToString();//"BABPK3030L"; //"ASUPK5335L";         //"BABPK3030L";
                        cxml.LA_Aadhar = "";
                        cxml.LA_Ckyc = "";
                        cxml.LA_Passport = "";
                        cxml.LA_DL = "";
                        cxml.LA_Voter_Id = "";
                        cxml.LA_Phone_Number_1 = "";
                        cxml.LA_Phone_Number_2 = "";
                        cxml.LA_Email_1 = "";
                        cxml.LA_Email_2 = "";
                        cxml.Date_of_Death = "";
                        cxml.type = "payer";
                        mxml.Add(cxml);
                    }
                }
            }
            //Call mapping master here in a dataset
            DataSet dsmapcode = objComm.MappingMaster("");
            List<Employee> list = new List<Employee>();
            if (dsmapcode != null && dsmapcode.Tables[0].Rows.Count > 0)
            {
                var myData = dsmapcode.Tables[0].AsEnumerable().Select(r => new Employee
                {
                    Code = r.Field<string>("Code"),
                    desc = r.Field<string>("PolicyStatusDesc")
                });
                 list = myData.ToList();

            }

            DataTable alldt = new DataTable();
            //loop the list to create xml depending upon the chkbox and hit that many times and get the data in dataset and merge all 
            //if only one dt is created then no extra column added but if 2 or 3 dt created extar column is added of type:LA/Proposer
            foreach (var item in mxml)
            {
                XmlDocument xmldoc = new XmlDocument();
                XmlElement queryNode = xmldoc.CreateElement("Query");
                queryNode.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
                //rootNode.AppendChild(detailsNode);
                XmlElement rootNode = xmldoc.CreateElement("Details");

                XmlNode userNode = xmldoc.CreateElement("Policy_Number");
                userNode.InnerText = item.Policy_Number;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("Proposal_Number");
                userNode.InnerText = item.Proposal_Number;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("Query_Type");
                userNode.InnerText = item.Query_Type;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("DoP_DoC");
                userNode.InnerText = item.DoP_DoC;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("Sum_Assured");
                userNode.InnerText = item.Sum_Assured;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_First_Name");
                userNode.InnerText = item.LA_First_Name;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Middle_Name");
                userNode.InnerText = item.LA_Middle_Name;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Last_Name");
                userNode.InnerText = item.LA_Last_Name;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_DoB");
                userNode.InnerText = item.LA_DoB;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Gender");
                userNode.InnerText = item.LA_Gender;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Current_Address");
                userNode.InnerText = item.LA_Current_Address;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Permanent_Address");
                userNode.InnerText = item.LA_Permanent_Address;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Pin_Code");
                userNode.InnerText = item.LA_Pin_Code;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_PAN");
                userNode.InnerText = item.LA_PAN;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Aadhar");
                userNode.InnerText = item.LA_Aadhar;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Ckyc");
                userNode.InnerText = item.LA_Ckyc;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Passport");
                userNode.InnerText = item.LA_Passport;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_DL");
                userNode.InnerText = item.LA_DL;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Voter_Id");
                userNode.InnerText = item.LA_Voter_Id;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Phone_Number_1");
                userNode.InnerText = item.LA_Phone_Number_1;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Phone_Number_2");
                userNode.InnerText = item.LA_Phone_Number_2;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Email_1");
                userNode.InnerText = item.LA_Email_1;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("LA_Email_2");
                userNode.InnerText = item.LA_Email_2;
                rootNode.AppendChild(userNode);

                userNode = xmldoc.CreateElement("Date_of_Death");
                userNode.InnerText = item.Date_of_Death;
                rootNode.AppendChild(userNode);

                queryNode.AppendChild(rootNode);
                xmldoc.AppendChild(queryNode);

                string FileNameRequest = "IIBRequest" + DateTime.Now.ToString("ddMMyyyyhhmmss");
                string FolderPathRequest = ConfigurationManager.AppSettings["FolderPathRequest"];
                string FolderPathzip = ConfigurationManager.AppSettings["FolderPathZip"];
                string FilePathRequest = HttpContext.Current.Server.MapPath(FolderPathRequest + "\\" + FileNameRequest + ".xml");
                string Requestpath = FilePathRequest;
                xmldoc.Save(Requestpath);
                #region ZipFile
                objComm.IIBLogtable(txtAppno.Text, "", FileNameRequest, "", "", "", "", "UW", "", "", "");

                string pathzip = HttpContext.Current.Server.MapPath(FolderPathzip);
                string IIBFilePathZip = pathzip + "\\" + FileNameRequest + ".zip";
                using (var zip = ZipFile.Open(IIBFilePathZip, ZipArchiveMode.Create))
                    zip.CreateEntryFromFile(FilePathRequest, FileNameRequest + ".xml");
                #endregion
                string ZipFileName = FileNameRequest + ".zip";
                // Create a byte array of file stream length
                byte[] bytes = System.IO.File.ReadAllBytes(IIBFilePathZip);

                //IIBRequest--Request abd response ig going to be hit 3 times if depends on checkbox.
                IIBData.QueryUploadRequest objqueryuploadrequest = new IIBData.QueryUploadRequest();
                objqueryuploadrequest.FileContent = bytes;

                //IIBResponse 
                IIBData.QueryUploadResponse objqueryuploadresponse = new IIBData.QueryUploadResponse();
                IIBData.ServiceSoapClient serviceSoapClient = new IIBData.ServiceSoapClient();
                byte[] data = serviceSoapClient.QueryUpload(Commfun.GetIIBLoginDetails(), objqueryuploadrequest.FileContent);
                objComm.IIBLogtable(txtAppno.Text, "", IIBFilePathZip, "", "", "", "", "UW", "", "", "IIBQueryhit");
                string decodedString = Encoding.UTF8.GetString(data);
                xmldoc.LoadXml(decodedString.Replace(@"\r\n", ""));
                XmlNodeList xnList = xmldoc.SelectNodes("/UploadFile");
                foreach (XmlNode xn in xnList)
                {
                    XmlNode anode = xn.SelectSingleNode("FileHeader");
                    if (anode != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        string Status = anode.SelectSingleNode("Status").InnerText;
                        string Remarks = anode.SelectSingleNode("Remarks").InnerText;
                        string TransactionID = anode.SelectSingleNode("TransactionID").InnerText;
                        if (Status == "Success")
                        {
                            objComm.IIBLogtable(txtAppno.Text, "", FileNameRequest, "", Status, Remarks, "", "UW", "", "", "");
                            IIBData.GetQueryDataRequest objgetRequest = new IIBData.GetQueryDataRequest();
                            objgetRequest.TransactionRefereceNo = TransactionID;
                            IIBData.GetQueryDataResponse objgetResponse = new IIBData.GetQueryDataResponse();
                            IIBData.ServiceSoapClient objserviceSoapClient = new IIBData.ServiceSoapClient();
                            byte[] datareq = objserviceSoapClient.GetQueryData(Commfun.GetIIBLoginDetails(), objgetRequest.TransactionRefereceNo);
                            string decodedString1 = Encoding.UTF8.GetString(datareq);
                            decodedString1 = decodedString1.Replace("\r\n\r\n\0", string.Empty);
                            if (decodedString1.Trim() == "No Match Found")
                            {
                                //ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('No Match Found');hideloading();</script>", false);
                                ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('No Match Found');hideloading();</script>", false);
                                GridViewOther.Visible = false;
                                GridViewFG.Visible = false;
                                btnFetchIIBQuery.Enabled = false;
                                objComm.IIBLogtable(txtAppno.Text, "", FileNameRequest, "", "", "", "NoMatchFound", "UW", "", "", "DataNotMatchFound");

                            }
                            else
                            {
                                string FileNameResponse = DateTime.Now.ToString("ddMMyyyyhhmmss");
                                string FolderPathResponse = ConfigurationManager.AppSettings["FolderPathCSVFiles"];
                                string FilePathResponse = HttpContext.Current.Server.MapPath(FolderPathResponse + "\\" + FileNameResponse + ".csv");
                                string Responsepath = FilePathResponse;
                                FileStream fs = new FileStream(Responsepath, FileMode.Create, FileAccess.ReadWrite);
                                objComm.IIBLogtable(txtAppno.Text, "", FileNameRequest, FileNameResponse, "", "", "", "UW", "", "", "ResponseFileData");
                                BinaryWriter bw = new BinaryWriter(fs);
                                bw.Write(datareq);
                                bw.Close();
                                //string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Responsepath1);
                                //string[] files = File.ReadAllLines(path);
                                DataTable dtCsv = new DataTable();
                                string Fulltext;
                                using (StreamReader sr = new StreamReader(Responsepath))
                                {
                                    while (!sr.EndOfStream)
                                    {
                                        Fulltext = sr.ReadToEnd().ToString(); //read full file text  
                                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                                        for (int i = 0; i < rows.Count() - 1; i++)
                                        {
                                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                                            {
                                                if (i == 0)
                                                {
                                                    for (int j = 0; j < rowValues.Count(); j++)
                                                    {
                                                        dtCsv.Columns.Add(rowValues[j].Trim()); //add headers  
                                                    }
                                                }
                                                else
                                                {
                                                    DataRow dr = dtCsv.NewRow();
                                                    for (int k = 0; k < rowValues.Count(); k++)
                                                    {
                                                        if (k == 5)
                                                        {
                                                            var desc = list.Find(x => x.Code == rowValues[k]);
                                                            dr[k] = desc.desc;
                                                        }
                                                        else
                                                        {
                                                            dr[k] = rowValues[k].ToString().Trim();
                                                        }

                                                    }
                                                    dtCsv.Rows.Add(dr); //add other rows  
                                                }
                                            }
                                        }
                                    }
                                }
                                //Merge datatable accoreding to checkbox 
                                if (item.type == "proposer")
                                {
                                    //Add column for LA0-----------------------  
                                    // DataRow drowItem;
                                    DataColumn dcolColumn = new DataColumn("LAProposerPayor", typeof(string));
                                    //DataColumn isnegativematch = new DataColumn("Is_Negative_Match", typeof(string));
                                    //dtCsv.Columns.Add(isnegativematch);
                                    dtCsv.Columns.Add(dcolColumn);
                                    foreach (DataRow row in dtCsv.Rows)
                                    {
                                        row["LAProposerPayor"] = "proposer";
                                        //row["Is_Negative_Match"] = "Y";
                                    }
                                    dtCsv.Rows.RemoveAt(dtCsv.Rows.Count - 1);

                                    alldt.Merge(dtCsv);
                                }
                                else if (item.type == "payer")
                                {
                                    //Add Column for Proposer.
                                    //Add column for LA
                                    DataColumn dcolColumn = new DataColumn("LAProposerPayor", typeof(string));
                                    //DataColumn isnegativematch = new DataColumn("Is_Negative_Match", typeof(string));
                                    //dtCsv.Columns.Add(isnegativematch);
                                    dtCsv.Columns.Add(dcolColumn);
                                    foreach (DataRow row in dtCsv.Rows)
                                    {
                                        row["LAProposerPayor"] = "payer";
                                        //row["Is_Negative_Match"] = "Y";
                                    }
                                    dtCsv.Rows.RemoveAt(dtCsv.Rows.Count - 1);
                                    alldt.Merge(dtCsv);
                                }
                                else
                                {
                                    DataColumn dcolColumn = new DataColumn("LAProposerPayor", typeof(string));
                                    //DataColumn isnegativematch = new DataColumn("Is_Negative_Match", typeof(string));
                                    //dtCsv.Columns.Add(isnegativematch);
                                    dtCsv.Columns.Add(dcolColumn);
                                    foreach (DataRow row in dtCsv.Rows)
                                    {
                                        row["LAProposerPayor"] = "LA";
                                        //row["Is_Negative_Match"] = "Y";
                                    }
                                    dtCsv.Rows.RemoveAt(dtCsv.Rows.Count - 1);
                                    //}

                                    alldt.Merge(dtCsv);
                                    DataTable dt12 = new DataTable();
                                    dt12.Columns.Add("Input_Proposal_Policy_No");
                                    dt12.Columns.Add("QuestDBNo");
                                    dt12.Columns.Add("Input_Matching_Parameter");
                                    dt12.Columns.Add("Quest_DoP_DoC");
                                    dt12.Columns.Add("Quest_Sum_Assured");
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
                                    DataRow dr12 = null;


                                    foreach (DataRow item1 in alldt.Rows)
                                    {
                                        //DataRow dr = dttemp.NewRow();
                                        string ApplicationNo = item1["Input_Proposal_Policy_No"].ToString();
                                        string QuestDBNo = item1["QuestDBNo"].ToString();
                                        string Matching_Parameter = item1["Input_Matching_Parameter"].ToString();
                                        string DoP_DoC = item1["Quest_DoP_DoC"].ToString();
                                        string Sum_Assured = item1["Quest_Sum_Assured"].ToString().Split('.')[0].ToString();
                                        string Policy_Status = item1["Quest_Policy_Status"].ToString();
                                        string Date_of_Exit = item1["Quest_Date_of_Exit"].ToString();
                                        string Date_of_Death = item1["Quest_Date_of_Death"].ToString();
                                        string Cause_of_Death = item1["Quest_Cause_of_Death"].ToString();
                                        string Record_last_updated = item1["Quest_Record_last_updated"].ToString();
                                        string Entity_Caution_Status = item1["Quest_Entity_Caution_Status"].ToString();
                                        string Intermediary_Caution_Status = item1["Quest_Intermediary_Caution_Status"].ToString();
                                        string Company_Number = item1["Quest_Company_Number"].ToString();
                                        string Negative_Match = item1["Is_Negative_Match"].ToString().Trim();
                                        string LAProposerPayor = item1["LAProposerPayor"].ToString();
                                        //string CreatedBy = "UW";

                                        dr12 = dt12.NewRow();
                                        dr12["Input_Proposal_Policy_No"] = ApplicationNo;
                                        dr12["QuestDBNo"] = QuestDBNo;
                                        dr12["Input_Matching_Parameter"] = Matching_Parameter;
                                        dr12["Quest_DoP_DoC"] = DoP_DoC;
                                        dr12["Quest_Sum_Assured"] = Sum_Assured;
                                        dr12["Quest_Policy_Status"] = Policy_Status;
                                        dr12["Quest_Date_of_Exit"] = Date_of_Exit;
                                        dr12["Quest_Date_of_Death"] = Date_of_Death;
                                        dr12["Quest_Cause_of_Death"] = Cause_of_Death;
                                        dr12["Quest_Record_last_updated"] = Record_last_updated;
                                        dr12["Quest_Entity_Caution_Status"] = Entity_Caution_Status;
                                        dr12["Quest_Intermediary_Caution_Status"] = Intermediary_Caution_Status;
                                        dr12["Quest_Company_Number"] = Company_Number;
                                        dr12["Is_Negative_Match"] = Negative_Match;
                                        dr12["LAProposerPayor"] = LAProposerPayor;
                                        dr12["Status"] = "Active";
                                        dr12["CreatedBy"] = "UW";
                                        dt12.Rows.Add(dr12);

                                        //objComm.SaveIIBData(ApplicationNo, QuestDBNo, Matching_Parameter, DoP_DoC, Sum_Assured,
                                        //Policy_Status, Date_of_Exit, Date_of_Death, Cause_of_Death, Record_last_updated, Entity_Caution_Status, Intermediary_Caution_Status, Company_Number, Negative_Match, LAProposerPayor, CreatedBy);
                                    }
                                    objComm.IIBQueryData(dt12);
                                    //dispalythe grid here
                                }

                            }
                        }
                        else if (Status == "Failed")
                        {
                            string replaceSTR = Remarks.Replace("'", "''").Trim();
                            string repalce1Str = replaceSTR.Replace("''", " ");
                            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('" + repalce1Str + "');hideloading();</script>", false);
                        }
                    }
                    else
                    {
                    }
                }
            }
            //display the grid here
            DataTable dt11 = new DataTable();
            //Changes Start by Shweta on 07042020 [If alldt table is null then to handle error will check 1st if table is empty or not]
            if (alldt != null && alldt.Rows.Count > 0)
            {
                alldt.Columns.Remove("Input_Proposal_Policy_No");
                //alldt.Columns.Remove("Quest_Company_Number");
                //alldt.Columns.Remove("Quest_Date_of_Exit");
                //alldt.Columns.Remove("Quest_Date_of_Death");
                //alldt.Columns.Remove("Quest_Cause_of_Death");
                //alldt.Columns.Remove("Quest_Entity_Caution_Status");
                //alldt.Columns.Remove("Quest_Intermediary_Caution_Status"); 
                alldt.AsEnumerable().ToList().ForEach(p => p.SetField<string>("Quest_Sum_Assured", Math.Round(double.Parse(p.Field<string>("Quest_Sum_Assured"), System.Globalization.CultureInfo.InvariantCulture), 1).ToString()));
                dt11 = alldt.AsEnumerable()
                   .Where(r => r.Field<string>("Quest_Company_Number") == "133")
                   .CopyToDataTable();
                GridViewFG.DataSource = dt11;
                GridViewFG.DataBind();
                GridViewFG.Visible = true;
                btnFetchIIBQuery.Visible = true;


                //DataTable dt2 = new DataTable();
                ////dt2.Merge(alldt);
                //dt2 = alldt.AsEnumerable()
                //               .Where(r => r.Field<string>("Quest_Company_Number") != "133")
                //               .CopyToDataTable();
                //GridViewOther.DataSource = dt2;
                //GridViewOther.DataBind();
                //GridViewOther.Visible = true;

                DataTable dt2 = new DataTable();
                var dtdt2 = alldt.AsEnumerable()
              .Where(r => r.Field<string>("Quest_Company_Number") != "133");
                //.CopyToDataTable();
                var resdtdt2 = dtdt2.Any() ? "1" : "2";
                if (resdtdt2.ToString() == "1")
                {
                    dt2 = dtdt2.CopyToDataTable();

                    GridViewOther.DataSource = dt2;
                    GridViewOther.DataBind();
                    GridViewOther.Visible = true;
                    lblothercompany.Visible = true;
                    lblothercompany.ForeColor = System.Drawing.Color.Red;
                    lblothercompany.Text = "The existing policies found in other insurance companies,please ensure to collect the information from client in proposal form / declaration";
                }

                GridViewOther.Visible = true;
                btnFetchIIBQuery.Visible = true;
            }
            //Changes Start by Shweta on 07042020 
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('" + ex.Message + "');hideloading();</script>", false);
            objComm.IIBLogtable(txtAppno.Text, "", "", "", "Failed", "", "", "UW", "", "", "Failed");
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>alert('Show IIB Data');hideloading();</script>", false);
    }
    public class Employee
    {
        public string Code { get; set; }
        public string desc { get; set; }
    }
    public class multiplexml
    {
        public string type;
        public string Policy_Number;
        public string Proposal_Number;
        public string Query_Type;
        public string DoP_DoC;
        public string Sum_Assured;
        public string LA_First_Name;
        public string LA_Middle_Name;
        public string LA_Last_Name;
        public string LA_DoB;
        public string LA_Gender;
        public string LA_Current_Address;
        public string LA_Permanent_Address;
        public string LA_Pin_Code;
        public string LA_PAN;
        public string LA_Aadhar;
        public string LA_Ckyc;
        public string LA_Passport;
        public string LA_DL;
        public string LA_Voter_Id;
        public string LA_Phone_Number_1;
        public string LA_Phone_Number_2;
        public string LA_Email_1;
        public string LA_Email_2;
        public string Date_of_Death;
    }
    protected void GridViewFG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {

                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;

                }

            }
        }
    }
    protected void GridViewOther_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string type = e.Row.Cells[12].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (type == "Y")
                {
                    e.Row.Cells[12].BackColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    //1.2 End of Changes by Rupali

    #region ENYScoreData
    //27.1 Start ENY Score  Shyam Patil
    private void ENYScore(string ApplicationNo)
    {
        try
        {
            DataSet ds = new DataSet();
            objcomm.GetENYDeatils(ref ds, ApplicationNo, "Revival");
            string ENYScore = CallENYScore(ds.Tables[0].Rows[0]);
            txtENYScore.Text = ENYScore;
        }
        catch (Exception)
        {

        }

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

                //postData.Add(new KeyValuePair<string, string>("ClientID", "d5cc933c-587d-4077-8798-21417b84bffd"));
                //postData.Add(new KeyValuePair<string, string>("ClientSecret", "ssTifEJIcCRkgXMGlGzJyhZqTs4F+uTYpZzXHUBq1kg="));
                //postData.Add(new KeyValuePair<string, string>("Source", "TOKEN"));
                //postData.Add(new KeyValuePair<string, string>("PartnerID", "SARAL"));

                //client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client1.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "7b83a48f03374f2794d6da09f559f6dd");

                //HttpContent content = new FormUrlEncodedContent(postData);

                //var responseResult = client1.PostAsync("https://lfagentapigw-rsg.azure-api.net/generateToken/api/GenerateToken", content).Result;

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
        catch (Exception ex)
        {
        }

    }

    private string CallENYScore(DataRow dataRow)
    {
        string inputJson = string.Empty;
        string result = string.Empty;
        string Authorization = string.Empty;
        string apiUrl = ConfigurationManager.AppSettings["RiskScoreApiURL"].ToString();
        generateToken(ref Authorization);

        EnyScoreRequest enyScoreRequest = new EnyScoreRequest();
        enyScoreRequest.DataKeyName = dataRow["DataKeyName"].ToString();
        enyScoreRequest.DataKeyValue = dataRow["DataKeyValue"].ToString();
        enyScoreRequest.Register = dataRow["Register"].ToString();
        enyScoreRequest.Sumassured = dataRow["Sumassured"].ToString();
        enyScoreRequest.Product_Code = dataRow["ProductCode"].ToString();
        enyScoreRequest.Product_Name = dataRow["ProductName"].ToString();
        enyScoreRequest.Premium_Amt = dataRow["PremiumAmt"].ToString();
        enyScoreRequest.Branch_Name = dataRow["BranchName"].ToString();
        enyScoreRequest.La_Code = dataRow["LACode"].ToString();
        enyScoreRequest.Laname = dataRow["LAName"].ToString();
        enyScoreRequest.La_Pcode = dataRow["LAPincode"].ToString();
        enyScoreRequest.La_Occupation = dataRow["LAOccupation"].ToString();
        enyScoreRequest.La_Education = dataRow["LAEducation"].ToString();
        enyScoreRequest.La_Education_Desc = dataRow["LAEducationDesc"].ToString();
        enyScoreRequest.La_Age_At_Entry = dataRow["LAAgeAtEntry"].ToString();
        enyScoreRequest.La_Dob = dataRow["LADob"].ToString();
        enyScoreRequest.La_Age_Proof = dataRow["LAAgeProof"].ToString();
        enyScoreRequest.Channel = dataRow["Channel"].ToString();
        enyScoreRequest.Agntnum = dataRow["AgentNumber"].ToString();
        enyScoreRequest.Agentname = dataRow["AgentName"].ToString();
        enyScoreRequest.La_Gender = dataRow["LAGender"].ToString();
        enyScoreRequest.La_Annual_Income = dataRow["LAAnnualIncome"].ToString();
        enyScoreRequest.Billfreq = dataRow["Frequency"].ToString();
        enyScoreRequest.Statcode = dataRow["Statcode"].ToString();
        enyScoreRequest.Owner_Pcode = dataRow["OwnerPincode"].ToString();
        enyScoreRequest.Channel_Name = dataRow["ChannelName"].ToString();
        enyScoreRequest.Ape = dataRow["Ape"].ToString();
        enyScoreRequest.La_Incomeprf = dataRow["LAIncomeprf"].ToString();
        enyScoreRequest.Ppt = dataRow["PPT"].ToString();
        enyScoreRequest.Pt = dataRow["PT"].ToString();
        enyScoreRequest.Nominee_Dob = dataRow["NomineeDob"].ToString();
        enyScoreRequest.Nomineerelation = dataRow["NomineeRelation"].ToString();
        enyScoreRequest.Owner_Pancard = dataRow["OwnerPancard"].ToString();
        enyScoreRequest.Owner_Address_Proof = dataRow["OwnerAddressProof"].ToString();
        enyScoreRequest.Owner_Address_Proof_Desc = dataRow["OwnerAddressProofDesc"].ToString();
        enyScoreRequest.La_State = dataRow["LAState"].ToString();
        enyScoreRequest.First_Payment_Mode = dataRow["FirstPaymentMode"].ToString();
        enyScoreRequest.APIDetails = dataRow["APIDetails"].ToString();
        enyScoreRequest.Source = dataRow["Source"].ToString();

        inputJson = (new JavaScriptSerializer()).Serialize(enyScoreRequest);

        HttpResponseMessage HttpResponse = new HttpResponseMessage();

        HttpResponse = ApiPost1(apiUrl, "GetRiskScore", inputJson, Authorization);

        if (HttpResponse.IsSuccessStatusCode)
        {
            result = HttpResponse.Content.ReadAsStringAsync().Result;
            EnyScoreResponse objResponseClass = JsonConvert.DeserializeObject<EnyScoreResponse>(HttpResponse.Content.ReadAsStringAsync().Result);

            if (objResponseClass.responseResult.responseCode == "0")
            {
                return objResponseClass.responseBody.risk_Score;
            }
        }
        return "0";
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
        catch (Exception ex)
        {
            // LogRequest(ex.Message.ToString(), ex.ToString(), "ApiPost", Method, string.Empty, string.Empty, "E");
            return null;
        }
    }
    //27.1 End ENY Score  Shyam Patil
    #endregion

}