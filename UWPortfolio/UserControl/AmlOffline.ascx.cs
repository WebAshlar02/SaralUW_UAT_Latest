/*
**********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET (013)
METHODE/EVENT:PAGE LOAD
REMARK: 
DateTime :
**********************************************************************************************************************************
*********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :SHRIJEET (013)
METHODE/EVENT:PAGE LOAD
REMARK: CALL POST BACK AT END OF SAVE BUTTON
DateTime :24Feb18
**********************************************************************************************************************************
*********************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT:
REMARK:  AML risk categorisation in Life Asia
DateTime :22Jun22
**********************************************************************************************************************************
*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UWSaralObjects;

public partial class UserControl_AmlOffline : System.Web.UI.UserControl
{
    BussLayer ObjBuss = new BussLayer();
    DataSet dsPendingDocs = new DataSet();
    ChangeValue objChangeObj = new ChangeValue();        
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {            

            string strAppNo = string.Empty;//"C00190148";// "C00190147";//"C00002691""NUA0015135"
            DataSet dsPendingDocs = new DataSet();
            lblMsg.Text = "";
            if (Request.QueryString["qsAppNo"] != null)
            {
                //strAppNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
                strAppNo = Request.QueryString["qsAppNo"];
                FetchPendingDocs(ref dsPendingDocs, strAppNo);
            }
            //else
            //{
            //    FetchPendingDocs(ref dsPendingDocs, "C00199182");
            //}
        }
    }    
    /*added by shri on 29 nov 17 to comment old value and add new */
    protected void btnDoc_Click(object sender, EventArgs e)
    {
        try
        {
            /*ID:1 START*/
            ChangeValue objChangeValue = new ChangeValue();
            DataSet _dsResponce = new DataSet();
            DataSet _dsPrevPol = new DataSet();
            string strIdentitiFlag = "CONTRACTMODIFICATION", LAPushErrorMsg = string.Empty, strConsentRespons = string.Empty;
            int LApushErrorCode = -1;
            /*ID:1 END*/
            string strApplicationNumber = Convert.ToString(ViewState["ApplicationNumber"]);
            if (Session["objLoginObj"] != null)
            {
                objChangeObj = (ChangeValue)Session["objLoginObj"];
            }
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            string strUserId = objChangeObj.userLoginDetails._UserID;
            InsertUwDecisionTracking(strApplicationNumber, strUserId, DateTime.Now, "AML_OFFLINE", ref intTrackingRet);
            /*end here*/
            /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
            int intRef = -1;
            Commfun objComm = new Commfun();
            objComm.ManageApplicationLifeCycle(strApplicationNumber, objChangeObj.userLoginDetails._UserID, "UW_DECISION_AML_OFFLINE", false, ref intRef);            
            /*END HERE*/
            string channelType = "offline";
            string DocSMSMessage = string.Empty;
            string commentSystemGen = string.Empty;
            string strLAPushStatus = string.Empty;
            lblMsg.Text = string.Empty;
            foreach (RepeaterItem rptItm in rptDocumentList.Items)
            {
                //declare variable 
                int fileSize = 0;
                string fileName = string.Empty;
                Label lblDocTypeId = (Label)rptItm.FindControl("lblDocTypeId");
                Label lblAssuredType = (Label)rptItm.FindControl("lblAssuredType");
                Label lblDocTypeName = (Label)rptItm.FindControl("lblDocTypeName");

                //check has file or not                 
                DropDownList ddlDocProof = (DropDownList)rptItm.FindControl("ddlDocProof");
                if (ddlDocProof.SelectedValue != "0")
                {
                    //isFileToUpload = true;
                    //fileSize = FileUploadDoc.PostedFile.ContentLength;
                    string serverPath = string.Empty;
                    //fileName = FileUploadDoc.FileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, fileName.Length - (fileName.LastIndexOf("\\") + 1));
                    string FilePath = string.Empty;
                    string FileSqlPath = string.Empty;
                    //string strApplicationNumber = lblApplicationNo.Text;//Convert.ToString(ViewState["ApplicationNumber"]);
                    string docUploadPath = Convert.ToString(ConfigurationManager.AppSettings["DocUploadPath"]);
                    string newFileName = string.Empty;
                    string fileSavePath = string.Empty;

                    try
                    {
                        //SAVE IN DB
                        if (string.IsNullOrEmpty(lblMsg.Text))
                        {
                            ObjBuss.ManageOfflineDocument(strApplicationNumber, ddlDocProof.SelectedValue, lblDocTypeName.Text, lblAssuredType.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserGroup
                                    , objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName
                                    , objChangeObj.userLoginDetails._ApplicationName);
                        }
                    }
                    catch (Exception ex)
                    {
                        //isAMLReq = false;
                    }
                }
            }

            //AML Updation Call
            //if (!string.IsNullOrEmpty(leadId) && isAMLReq)
            //{
            DataSet _ds = new DataSet();
            ObjBuss.AMLDetailsOffline_Get(ref _ds, strApplicationNumber);
            UWSaralServices.AmlDetails objAml = new UWSaralServices.AmlDetails();
            //3.1 Begin of Changes; Sagar Thorave-[mfl00886]
            UWSaralDecision.CommFun comm = new UWSaralDecision.CommFun();
            //3.1 End of Changes; Sagar Thorave-[mfl00886]
            int strLAPushErrorcode = 0;
            if (_ds != null && _ds.Tables[0].Rows.Count > 0)
            {
                // lblMsg.Text = "";
                StringBuilder strMessage = new StringBuilder();
                foreach (DataRow row in _ds.Tables[0].Rows)
                {
                    //3.1 Begin of Changes; Sagar Thorave-[mfl00886]
                    DataSet ds12 = null;
                    objComm.Featch_AMLFLAG_Details(ref ds12, strApplicationNumber, "SaralUW");
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
                    objAml.AMLPushService(strApplicationNumber, row, objChangeObj, ref strLAPushErrorcode, ref strLAPushStatus, CLNTRSKIND);
                    //3.1 End of Changes; Sagar Thorave-[mfl00886]
                    lblMsg.Visible = true;
                    if (strLAPushErrorcode != 0 || strLAPushErrorcode == null)
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        strMessage.Append("AML Failed for ClientId : " + row["CLTTWO"].ToString());
                        strMessage.Append("<br />");
                    }
                    else
                    {
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        strMessage.Append("AML Successful for ClientID : " + row["CLTTWO"].ToString());
                        strMessage.Append("<br />");
                    }
                }

                lblMsg.Text = strMessage.ToString();
            }
            objComm.ManageApplicationLifeCycle(strApplicationNumber, objChangeObj.userLoginDetails._UserID, "UW_DECISION_AML_OFFLINE", true, ref intRef);
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*ID:1 START*/
            UWSaralDecision.BussLayer objBussLayer = new UWSaralDecision.BussLayer();
            objBussLayer.OnlineApplicationLAServiceDetails_PUSH(strApplicationNumber, "OFFLINE", objChangeValue, ref _dsResponce, ref _dsPrevPol, strIdentitiFlag
                , ref LApushErrorCode, ref LAPushErrorMsg, ref strConsentRespons);
            /*ID:1 END*/
            /*END HERE*/
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //}
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);            
        }
        catch (Exception ex)
        {

        }
        finally
        {            
            /*ID:1 START*/
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>__doPostBack('aml','aml');hideloading()</script>", false);
            /*ID:1 END*/
        }
    }
    //protected void btnDoc_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string strApplicationNumber = Convert.ToString(ViewState["ApplicationNumber"]);
    //        if (Session["objLoginObj"] != null)
    //        {
    //            objChangeObj = (ChangeValue)Session["objLoginObj"];
    //        }
    //        /*ADDED BY SHRI ON 07 NOV 17 TO CALL MANAGE APPLICATION LIFE CYCLE*/
    //        int intRef = -1;
    //        Commfun objComm = new Commfun();
    //        objComm.ManageApplicationLifeCycle(strApplicationNumber, objChangeObj.userLoginDetails._UserID, "UW_DECISION_AML_OFFLINE", false, ref intRef);
    //        /*END HERE*/
    //        //string leadId = Convert.ToString(ViewState["LeadId"]);
    //        string channelType = "offline";
    //        string DocSMSMessage = string.Empty;
    //        string commentSystemGen = string.Empty;
    //        //string eventTypeComment = "7";
    //        //int strLAPushErrorcode = 0;
    //        string strLAPushStatus = string.Empty;
    //        lblMsg.Text = string.Empty;
    //        //get documents list
    //        DataSet dsDocsForComments = new DataSet();
    //        //dsDocsForComments = UserData.GetDocumentDetailsByLeadID(Convert.ToString(leadId));

    //        foreach (RepeaterItem rptItm in rptDocumentList.Items)
    //        {
    //            //declare variable 
    //            int fileSize = 0;
    //            string fileName = string.Empty;
    //            Label lblDocTypeId = (Label)rptItm.FindControl("lblDocTypeId");
    //            Label lblAssuredType = (Label)rptItm.FindControl("lblAssuredType");
    //            Label lblDocTypeName = (Label)rptItm.FindControl("lblDocTypeName");

    //            //check has file or not 
    //            FileUpload FileUploadDoc = (FileUpload)rptItm.FindControl("FileUploadDoc");
    //            DropDownList ddlDocProof = (DropDownList)rptItm.FindControl("ddlDocProof");
    //            if (ddlDocProof.SelectedValue != "0")
    //            {
    //                if (FileUploadDoc.HasFile)
    //                {
    //                    //isFileToUpload = true;
    //                    fileSize = FileUploadDoc.PostedFile.ContentLength;
    //                    string serverPath = string.Empty;
    //                    fileName = FileUploadDoc.FileName;
    //                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, fileName.Length - (fileName.LastIndexOf("\\") + 1));
    //                    string FilePath = string.Empty;
    //                    string FileSqlPath = string.Empty;
    //                    //string strApplicationNumber = lblApplicationNo.Text;//Convert.ToString(ViewState["ApplicationNumber"]);
    //                    string docUploadPath = Convert.ToString(ConfigurationManager.AppSettings["DocUploadPath"]);
    //                    string newFileName = string.Empty;
    //                    string fileSavePath = string.Empty;

    //                    FileSqlPath = Path.Combine(docUploadPath + strApplicationNumber + "\\Documents\\", FileUploadDoc.FileName);

    //                    bool isValidFile = false;
    //                    FilePath = docUploadPath;
    //                    String fileExtension = System.IO.Path.GetExtension(FileUploadDoc.FileName).ToLower();
    //                    string allowedExtentions = Convert.ToString(ConfigurationManager.AppSettings["FileExtentions"]);

    //                    String[] Extensions = allowedExtentions.Split(' ');
    //                    string strFileExt = FileUploadDoc.FileName.Substring(FileUploadDoc.FileName.LastIndexOf('.'));

    //                    for (int i = 0; i < Extensions.Length; i++)
    //                    {
    //                        if (fileExtension == Extensions[i])
    //                        {
    //                            isValidFile = true;
    //                            break;
    //                        }
    //                    }

    //                    if (isValidFile)
    //                    {
    //                        if (fileSize <= 2000000)
    //                        {
    //                            try
    //                            {
    //                                if (!Directory.Exists(FilePath))
    //                                {
    //                                    Directory.CreateDirectory(FilePath);
    //                                }

    //                                if (!Directory.Exists(FilePath + strApplicationNumber))
    //                                {
    //                                    Directory.CreateDirectory(FilePath + strApplicationNumber);
    //                                }

    //                                if (Directory.Exists(FilePath + strApplicationNumber))
    //                                {
    //                                    serverPath = FilePath + strApplicationNumber + "\\Documents\\" + fileName;
    //                                    string documentTemplatePath = string.Empty;
    //                                    string fileUploadPath = string.Format("{0}\\{1}\\Documents\\", docUploadPath, strApplicationNumber);

    //                                    if (!FileUploadDoc.PostedFile.FileName.Equals(string.Empty))
    //                                    {
    //                                        switch (lblDocTypeId.Text)
    //                                        {

    //                                            //case "":
    //                                            //    newFileName = string.Format(strApplicationNumber + "_ONLINEVERF" + strFileExt);
    //                                            //    //fileSavePath = string.Format("{0}\\" + "Id_" + fileUPload.FileName, fileUploadPath);
    //                                            //    break;
    //                                            case "TJ689":
    //                                                newFileName = string.Format(strApplicationNumber + "_IDPROOF" + strFileExt);
    //                                                //fileSavePath = string.Format("{0}\\" + "Id_" + fileUPload.FileName, fileUploadPath);
    //                                                break;

    //                                            case "TJ690":
    //                                                newFileName = string.Format(strApplicationNumber + "_AGEPROOF" + strFileExt);
    //                                                //fileSavePath = string.Format("{0}\\" + "Age_" + fileUPload.FileName, fileUploadPath);
    //                                                break;

    //                                            case "TAL02":
    //                                                newFileName = string.Format(strApplicationNumber + "_ADDPROOF" + strFileExt);
    //                                                //fileSavePath = string.Format("{0}\\" + "Address_" + fileUPload.FileName, fileUploadPath);
    //                                                break;

    //                                            case "TAL04":
    //                                                newFileName = string.Format(strApplicationNumber + "_INCPROOF" + strFileExt);
    //                                                //fileSavePath = string.Format("{0}\\" + "Income_" + fileUPload.FileName, fileUploadPath);
    //                                                break;

    //                                            default:
    //                                                {
    //                                                    newFileName = string.Format(strApplicationNumber + "_OTHERS" + strFileExt);
    //                                                    break;
    //                                                }
    //                                        }
    //                                    }

    //                                    fileSavePath = string.Format("{0}\\" + newFileName, fileUploadPath);

    //                                    if (!File.Exists(fileSavePath))
    //                                    {

    //                                        if (!Directory.Exists(fileUploadPath))
    //                                        {
    //                                            Directory.CreateDirectory(fileUploadPath);
    //                                        }
    //                                    }

    //                                    FileUploadDoc.PostedFile.SaveAs(fileSavePath);

    //                                    //SAVE IN DB
    //                                    if (string.IsNullOrEmpty(lblMsg.Text))
    //                                    {
    //                                        ObjBuss.ManageOfflineDocument(strApplicationNumber, ddlDocProof.SelectedValue, lblDocTypeName.Text, lblAssuredType.Text, objChangeObj.userLoginDetails._UserID, objChangeObj.userLoginDetails._UserGroup
    //                                                , objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName
    //                                                , objChangeObj.userLoginDetails._ApplicationName);
    //                                    }
    //                                }
    //                            }
    //                            catch (Exception ex)
    //                            {
    //                                //isAMLReq = false;
    //                            }
    //                        }
    //                        else
    //                        {
    //                            lblMsg.Text = "Please upload file with size below 2MB";
    //                            lblMsg.Visible = true;
    //                            lblMsg.ForeColor = System.Drawing.Color.Red;
    //                            //isAMLReq = false;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        lblMsg.Text = "Invalid file extention";
    //                        lblMsg.Visible = true;
    //                        lblMsg.ForeColor = System.Drawing.Color.Red;
    //                        //isAMLReq = false;
    //                    }
    //                }
    //            }
    //        }

    //        //AML Updation Call
    //        //if (!string.IsNullOrEmpty(leadId) && isAMLReq)
    //        //{
    //        DataSet _ds = new DataSet();
    //        ObjBuss.AMLDetailsOffline_Get(ref _ds, strApplicationNumber);
    //        UWSaralServices.AmlDetails objAml = new UWSaralServices.AmlDetails();
    //        int strLAPushErrorcode = 0;
    //        if (_ds != null && _ds.Tables[0].Rows.Count > 0)
    //        {
    //            // lblMsg.Text = "";
    //            StringBuilder strMessage = new StringBuilder();
    //            foreach (DataRow row in _ds.Tables[0].Rows)
    //            {
    //                objAml.AMLPushService(strApplicationNumber, row, objChangeObj, ref strLAPushErrorcode, ref strLAPushStatus);
    //                lblMsg.Visible = true;
    //                if (strLAPushErrorcode != 0 || strLAPushErrorcode == null)
    //                {
    //                    lblMsg.ForeColor = System.Drawing.Color.Red;
    //                    strMessage.Append("AML Failed for ClientId : " + row["CLTTWO"].ToString());
    //                    strMessage.Append("<br />");
    //                }
    //                else
    //                {
    //                    lblMsg.ForeColor = System.Drawing.Color.Green;
    //                    strMessage.Append("AML Successful for ClientID : " + row["CLTTWO"].ToString());
    //                    strMessage.Append("<br />");
    //                }
    //            }

    //            lblMsg.Text = strMessage.ToString();
    //        }
    //        objComm.ManageApplicationLifeCycle(strApplicationNumber, objChangeObj.userLoginDetails._UserID, "UW_DECISION_AML_OFFLINE", true, ref intRef);
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
    //        //}
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally {
    //        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    //    }
    //}
    /*end here*/
    public void FetchPendingDocs(ref DataSet dsPendingDocs, string appNo)
    {
        ObjBuss.FetchPendingDocsOffline(ref dsPendingDocs, appNo);
        if (dsPendingDocs != null && dsPendingDocs.Tables[0].Rows.Count > 0)
        {
            rptDocumentList.DataSource = dsPendingDocs.Tables[0];
            rptDocumentList.DataBind();
            int intCount = 0;
            lblApplicationNo.Text = "Application Number : " + appNo;
            //lblClientName.Text = "Name : " + Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[intCount]["CustomerName"]);
            //lblMobileNo.Text = "Mobile No : " + Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[intCount]["MobileNumber"]);
            foreach (RepeaterItem i in rptDocumentList.Items)
            {
                DropDownList ddlDocProof = (DropDownList)i.FindControl("ddlDocProof");
                Label lblDocTypeId = (Label)i.FindControl("lblDocTypeId");
                //dsPendingDocs.Tables[1].DefaultView.RowFilter = Convert.ToString(dsPendingDocs.Tables[1].Columns["FILTER"]) + '=' + lblDocTypeId.Text;

                DataTable tblFiltered = dsPendingDocs.Tables[1].AsEnumerable()
                             .Where(r => r.Field<string>("FILTER") == lblDocTypeId.Text)
                             .CopyToDataTable();
                ddlDocProof.DataSource = tblFiltered;
                ddlDocProof.DataValueField = "value";
                ddlDocProof.DataTextField = "name";
                ddlDocProof.DataBind();
                ddlDocProof.Items.Insert(0, new ListItem("Select", "0"));
                if (ddlDocProof.Items.FindByValue(Convert.ToString(dsPendingDocs.Tables[0].Rows[intCount]["DOCUMENT_NAME"]).Trim()) != null)
                {
                    ddlDocProof.SelectedValue = Convert.ToString(dsPendingDocs.Tables[0].Rows[intCount]["DOCUMENT_NAME"]).Trim();
                }
                intCount++;
            }
            ViewState["ApplicationNumber"] = appNo;
            //ViewState["LeadId"] = Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[0]["LeadId"]);
            //ViewState["ChannelType"] = appType;
        }
    }
    protected void rptDocumentList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    /*added by shri on 28 dec 17 to add tracking*/
    private void InsertUwDecisionTracking(string strApplicationNo, string strUserId, DateTime dtCurrentDateTime, string strEventName, ref int intRet)
    {
        Commfun objcomm = new Commfun();        
        objcomm.InsertUwDecisionTracking(strApplicationNo, strUserId, dtCurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fff"), strEventName, ref intRet);
    }

    /*added by shri on 28 dec 17 to update tracking*/
    private void UpdateUwDecisionTracking(int intSrNo, DateTime dtEndDate, ref int intRet)
    {
        Commfun objcomm = new Commfun();
        objcomm.UpdateUwDecisionTracking(intSrNo, dtEndDate.ToString("yyyy-MM-dd HH:mm:ss:fff"), ref intRet);
    }
    /*end here*/
}