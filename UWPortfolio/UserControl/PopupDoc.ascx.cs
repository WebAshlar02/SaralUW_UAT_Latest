using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UWSaralObjects;
using System.Text;
/*
**********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :Sagar Thorave
METHODE/EVENT:
REMARK:  CR-3387 AML risk categorisation in Life Asia
DateTime :22Jun22
**********************************************************************************************************************************
*/
public partial class UserControl_PopupDoc : System.Web.UI.UserControl
{
    BussLayer ObjBuss = new BussLayer();
    DataSet dsPendingDocs = new DataSet();
    DataSet _ds = new DataSet();
    Commfun objComm = new Commfun();
    ChangeValue objChangeObj = new ChangeValue();
    CommonObject objCommonObj = new CommonObject();
    protected void Page_Load(object sender, EventArgs e)
    {
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        objCommonObj = (CommonObject)Session["objCommonObj"];
        /*added by shri to populate docs*/
        if (!IsPostBack)
        {
            if (Request.QueryString["qsAppNo"] != null)
            {
                string strAppNo = string.Empty;
                strAppNo = Request.QueryString["qsAppNo"];
                //strAppNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
                int intRet = -1;
                FillPendingDoc(strAppNo, "ONLINE", ref intRet);
            }
        }
        /*end here*/
    }

    public void FetchPendingDocs(ref DataSet dsPendingDocs, string appNo)
    {
        ObjBuss.FetchPendingDocs(ref dsPendingDocs, appNo);
    }

    public void FillPendingDoc(string appNo, string appType, ref int response)
    {
        try
        {
            response = 0;
            lblMsg.Text = "";
            FetchPendingDocs(ref dsPendingDocs, appNo);
            if (dsPendingDocs != null && dsPendingDocs.Tables[0].Rows.Count > 0)
            {
                rptDocumentList.DataSource = dsPendingDocs.Tables["LeadDocuments"];
                rptDocumentList.DataBind();
                response = 1;
                int intCount = 0;
                lblApplicationNo.Text = "Application Number : " + appNo;
                lblClientName.Text = "Name : " + Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[intCount]["CustomerName"]);
                lblMobileNo.Text = "Mobile No : " + Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[intCount]["MobileNumber"]);
                foreach (RepeaterItem i in rptDocumentList.Items)
                {
                    DropDownList ddlDocProof = (DropDownList)i.FindControl("ddlDocProof");
                    DropDownList ddlUWStatus = (DropDownList)i.FindControl("ddlUWStatus");
                    Label lblDocTypeId = (Label)i.FindControl("lblDocTypeId");
                    dsPendingDocs.Tables["DocumentProofs"].DefaultView.RowFilter = dsPendingDocs.Tables["DocumentProofs"].Columns["DocumentTypeId"].ToString() + '=' + lblDocTypeId.Text;
                    ddlDocProof.DataSource = (dsPendingDocs.Tables["DocumentProofs"].DefaultView);
                    ddlDocProof.DataValueField = "DocumentProofId";
                    ddlDocProof.DataTextField = "DocumentName";
                    ddlDocProof.DataBind();
                    ddlDocProof.Items.Insert(0, new ListItem("Select", "0"));

                    if (ddlDocProof.Items.Count <= 1)
                    {
                        ddlDocProof.Attributes.Add("disabled", "false");
                    }
                    ddlDocProof.SelectedValue = Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[intCount]["DocumentProofId"]);
                    ddlUWStatus.SelectedValue = Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[intCount]["UWStatus"]);
                    intCount++;
                }
                ViewState["ApplicationNumber"] = appNo;
                ViewState["LeadId"] = Convert.ToString(dsPendingDocs.Tables["LeadDocuments"].Rows[0]["LeadId"]);
                ViewState["ChannelType"] = appType;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnDoc_Click(object sender, EventArgs e)
    {
        try
        {
            string ApplicationNumber = Convert.ToString(ViewState["ApplicationNumber"]);
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            string strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(ApplicationNumber, strUserId, DateTime.Now, "AML_ONLINE", ref intTrackingRet);
            /*end here*/
            string filePath = string.Empty;
            string subject = string.Empty; ;
            string strUploadedFiles = string.Empty;
            int result = 0;
            int flagRecieved = 1;
            int flagVerified = 1;
            int flagMedical = 1;
            int flagPending = 0;
            bool received = true, isAMLReq = true;
            string leadId = Convert.ToString(ViewState["LeadId"]);
            string channelType = Convert.ToString(ViewState["ChannelType"]);
            string DocSMSMessage = string.Empty;
            string commentSystemGen = string.Empty;
            string eventTypeComment = "7";
            int strLAPushErrorcode = 0;
            string strLAPushStatus = string.Empty;
            lblMsg.Text = string.Empty;
            //get documents list
            DataSet dsDocsForComments = new DataSet();
            //dsDocsForComments = UserData.GetDocumentDetailsByLeadID(Convert.ToString(leadId));

            foreach (RepeaterItem rptItm in rptDocumentList.Items)
            {
                DropDownList ddlDocProof = (DropDownList)rptItm.FindControl("ddlDocProof");
                HyperLink HlkDocument = (HyperLink)rptItm.FindControl("HlkDocument");
                //CheckBox chkVerified = (CheckBox)rptItm.FindControl("chkVerified");
                bool isVerified = false;
                Label lblID = (Label)rptItm.FindControl("lblID");
                //FileUpload FileUploadDoc = (FileUpload)rptItm.FindControl("FileUploadDoc");
                Label lblFileName = (Label)rptItm.FindControl("lblFileName");
                Label lblDocTypeId = (Label)rptItm.FindControl("lblDocTypeId");
                Label objlblFilePath = (Label)rptItm.FindControl("lblFilePath");
                int fileSize = 0;
                CheckBox chkItemChecked = (CheckBox)rptItm.FindControl("chkItemChecked");
                //CheckBox chkDeleteDoc = (CheckBox)rptItm.FindControl("chkDeleteDoc");

                DropDownList ddlUWStatus = (DropDownList)rptItm.FindControl("ddlUWStatus");

                string applicationNumber = Convert.ToString(ViewState["ApplicationNumber"]);
                string newFileName = string.Empty;
                string fileSavePath = string.Empty;

                string fileName = string.Empty;
                string FileSqlPath = string.Empty;
                string FilePath = string.Empty;
                commentSystemGen = string.Empty;
                bool isFileToUpload = false;
                // file upload
                /////For Delete Doc
                if (ddlUWStatus.SelectedValue == "Rejected")
                {
                    if (File.Exists(objlblFilePath.Text))
                        File.Delete(objlblFilePath.Text);
                    lblFileName.Text = "";
                    objlblFilePath.Text = "";
                }
                /////
                /*
                if (FileUploadDoc.HasFile)
                {
                    isFileToUpload = true;
                    fileSize = FileUploadDoc.PostedFile.ContentLength;
                    string serverPath = string.Empty;
                    fileName = FileUploadDoc.FileName;
                    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, fileName.Length - (fileName.LastIndexOf("\\") + 1));
                    string docUploadPath = Convert.ToString(ConfigurationManager.AppSettings["DocUploadPath"]);
                    FileSqlPath = Path.Combine(docUploadPath + applicationNumber + "\\Documents\\", FileUploadDoc.FileName);
                    bool isValidFile = false;
                    FilePath = docUploadPath;
                    String fileExtension = System.IO.Path.GetExtension(FileUploadDoc.FileName).ToLower();
                    string allowedExtentions = Convert.ToString(ConfigurationManager.AppSettings["FileExtentions"]);

                    String[] Extensions = allowedExtentions.Split(' ');
                    string strFileExt = FileUploadDoc.FileName.Substring(FileUploadDoc.FileName.LastIndexOf('.'));

                    for (int i = 0; i < Extensions.Length; i++)
                    {
                        if (fileExtension == Extensions[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    
                    if (isValidFile)
                    {
                        if (fileSize <= 2000000)
                        {
                            try
                            {
                                if (!Directory.Exists(FilePath))
                                {
                                    Directory.CreateDirectory(FilePath);
                                }

                                if (!Directory.Exists(FilePath + applicationNumber))
                                {
                                    Directory.CreateDirectory(FilePath + applicationNumber);
                                }

                                if (Directory.Exists(FilePath + applicationNumber))
                                {
                                    serverPath = FilePath + applicationNumber + "\\Documents\\" + fileName;
                                    string documentTemplatePath = string.Empty;
                                    string fileUploadPath = string.Format("{0}\\{1}\\Documents\\", docUploadPath, applicationNumber);

                                    if (!FileUploadDoc.PostedFile.FileName.Equals(string.Empty))
                                    {
                                        switch (Convert.ToInt32(lblDocTypeId.Text))
                                        {

                                            case 1:
                                                newFileName = string.Format(applicationNumber + "_ONLINEVERF" + strFileExt);
                                                //fileSavePath = string.Format("{0}\\" + "Id_" + fileUPload.FileName, fileUploadPath);
                                                break;
                                            case 2:
                                                newFileName = string.Format(applicationNumber + "_IDPROOF" + strFileExt);
                                                //fileSavePath = string.Format("{0}\\" + "Id_" + fileUPload.FileName, fileUploadPath);
                                                break;

                                            case 3:
                                                newFileName = string.Format(applicationNumber + "_AGEPROOF" + strFileExt);
                                                //fileSavePath = string.Format("{0}\\" + "Age_" + fileUPload.FileName, fileUploadPath);
                                                break;

                                            case 4:
                                                newFileName = string.Format(applicationNumber + "_ADDPROOF" + strFileExt);
                                                //fileSavePath = string.Format("{0}\\" + "Address_" + fileUPload.FileName, fileUploadPath);
                                                break;

                                            case 5:
                                                newFileName = string.Format(applicationNumber + "_INCPROOF" + strFileExt);
                                                //fileSavePath = string.Format("{0}\\" + "Income_" + fileUPload.FileName, fileUploadPath);
                                                break;

                                            default:
                                                {
                                                    newFileName = string.Format(applicationNumber + "_OTHERS" + strFileExt);
                                                    break;
                                                }
                                        }
                                    }

                                    fileSavePath = string.Format("{0}\\" + newFileName, fileUploadPath);

                                    if (!File.Exists(fileSavePath))
                                    {

                                        if (!Directory.Exists(fileUploadPath))
                                        {
                                            Directory.CreateDirectory(fileUploadPath);
                                        }
                                    }

                                    FileUploadDoc.PostedFile.SaveAs(fileSavePath);
                                }
                            }
                            catch (Exception)
                            {
                                isAMLReq = false;
                            }
                        }
                        else
                        {
                            lblMsg.Text = "Please upload file with size below 2MB";
                            //lblMsg.Visible = true;
                            lblMsg.CssClass = lblMsg.CssClass + " HideControl";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            isAMLReq = false;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Invalid file extention";
                        //lblMsg.Visible = true;
                        lblMsg.CssClass = lblMsg.CssClass.Replace(" HideControl", string.Empty); ;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        isAMLReq = false;
                    }
                }
                else
                {
                    fileName = lblFileName.Text;
                }
                
                if (string.IsNullOrEmpty(fileName))
                {
                    flagRecieved = 0;
                    received = false;
                }
                else
                {
                */
                flagPending = 1;
                    received = true;
                //}

                if (ddlUWStatus.SelectedValue == "Verified")
                {
                    isVerified = true;
                }
                /*
                if (FileUploadDoc.PostedFile.FileName.Length <= 0)
                {
                    FileSqlPath = objlblFilePath.Text;
                    fileSavePath = objlblFilePath.Text;
                    newFileName = lblFileName.Text;
                }
                */
                if (string.IsNullOrEmpty(lblMsg.Text))
                {
                    ObjBuss.InsertUpdateLeadDocument(lblID.Text, newFileName, fileSavePath, objChangeObj.userLoginDetails._UserID, received, isVerified, Convert.ToString(ViewState["LeadId"]), ddlDocProof.SelectedValue, lblDocTypeId.Text, ddlUWStatus.SelectedValue, ref result);
                }
            }

            //AML Updation Call
            if (!string.IsNullOrEmpty(leadId) && isAMLReq)
            {
                ObjBuss.AMLDetails_Get(ref _ds, ApplicationNumber, "ONLINE");
                UWSaralServices.AmlDetails objAml = new UWSaralServices.AmlDetails();
                if (_ds != null && _ds.Tables[0].Rows.Count > 0)
                {
                    // lblMsg.Text = "";
                    StringBuilder strMessage = new StringBuilder();
                    foreach (DataRow row in _ds.Tables[0].Rows)
                    {
                        //1.1 Start of Changes; Sagar Thorave-[mfl00886]
                        DataSet ds12 = null;
                        objComm.Featch_AMLFLAG_Details(ref ds12, ApplicationNumber, "SaralUW");
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
                        objAml.AMLPushService(ApplicationNumber, row, objChangeObj, ref strLAPushErrorcode, ref strLAPushStatus, CLNTRSKIND);
                        //1.1 End of Changes; Sagar Thorave-[mfl00886] 
                        //lblMsg.Visible = true;
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
                    lblMsg.CssClass = lblMsg.CssClass.Replace(" HideControl", string.Empty).Replace("HideControl", string.Empty);

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            Commfun objCommFun = new Commfun();
            int intRet = -1;
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
            // objCommFun.MaintainApplicationLog(ApplicationNumber, "AmlDtls", string.Empty,objChangeObj.userLoginDetails._UserID,"",ref intRet);            
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
            lblMsg.CssClass = lblMsg.CssClass.Replace(" HideControl", string.Empty);
        }
        //AML Updation Call End
    }

    protected void btnDedupeClient_Click(object sender, EventArgs e)
    {
        try
        {
            BussLayer objBusinessLayer = new BussLayer();
            DataSet _dsDbLA = new DataSet();
            Commfun objcomm = new Commfun();
            string strApplicationno = Convert.ToString(ViewState["ApplicationNumber"]);
            objcomm.FetchClientInfoOnApplciationNo(ref _dsDbLA, strApplicationno, "LA");
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            objCommonObj = (CommonObject)Session["objCommonObj"];
            string strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationno, strUserId, DateTime.Now, "AML_DEDUPE", ref intTrackingRet);
            /*end here*/
            ;
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
                    dgOnlineAMlUwDedupe.DataSource = _ds;
                    dgOnlineAMlUwDedupe.DataBind();
                    divDgOnlineAMlClientDedupe.Attributes["class"] = divDgOnlineAMlClientDedupe.Attributes["class"].Replace(" HideControl", string.Empty);
                }
                else
                {
                    dgOnlineAMlUwDedupe.DataSource = null;
                    dgOnlineAMlUwDedupe.DataBind();
                    divDgOnlineAMlClientDedupe.Attributes["class"] = divDgOnlineAMlClientDedupe.Attributes["class"] + " HideControl";
                }
            }
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            //strRet = FillDedupeDetails(_ds);
        }
        catch (Exception ex)
        {

        }
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