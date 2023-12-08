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

public partial class AddNominee : System.Web.UI.Page
{
    Commfun objcomm = new Commfun();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            /*added by shri to use ajax on 08 june 17*/
            Ajax.Utility.RegisterTypeForAjax(typeof(AddNominee), Page);
            /*end here*/
            if (!IsPostBack)
            {
                FetchCommonData();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void FetchClientBasicDetails(ref DataSet ds, string strApplnNo)
    {
        try
        {
            lblMsg.Text = string.Empty;
            BussLayer objBussLayer = new BussLayer();
            objBussLayer.ClientBasicInfo_GET(ref ds, strApplnNo);
        }
        catch (Exception ex)
        {
            ds = null;
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please try again";
        }

    }
    private void BindEntitygrid(DataSet ds)
    {
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = string.Empty;
            gvEntity.DataSource = null;
            gvEntity.DataBind();
            gvEntity.DataSource = ds;
            lblMsg.ForeColor = System.Drawing.Color.Blue;
            lblMsg.Text = "Record fetched successfully!";
        }
        else
        {
            gvEntity.DataSource = null;
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "No record found!";
        }
        gvEntity.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            clearcontrols();
            lblMsg.Text = string.Empty;
            cbAddressCopy.Checked = false;
            DataSet ds = new DataSet();
            if (txtAppno.Text != string.Empty)
            {
                FetchClientBasicDetails(ref ds, txtAppno.Text);
                BindEntitygrid(ds);
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
    protected void btnDedupeClient_Click(object sender, EventArgs e)
    {
        cbAddressCopy.Checked = false;
        DataSet _ds = new DataSet();
        int intTrackingRet = -1;
        try
        {
            lblMsg.Text = string.Empty;
            if (txtLaFirstName.Text == string.Empty)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please Enter First Name";
                return;
            }
            if (txtLaLastName.Text == string.Empty)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please Enter Last Name";
                return;
            }
            if (txtLaDob.Text == string.Empty)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please Enter DOB";
                return;
            }
            CommonObject objCommonObj = new CommonObject();
            string strUserId = string.Empty;
            /*added by shri on 28 dec 17 to add tracking*/
            objCommonObj = (CommonObject)Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(txtAppno.Text, strUserId, DateTime.Now, "UW_DEDUPE", ref intTrackingRet);
            /*end here*/
            BussLayer objBusinessLayer = new BussLayer();
            DataSet _dsDbLA = new DataSet();
            //objcomm.FetchClientInfoOnApplciationNo(ref _dsDbLA, txtAppno.Text, "LA");
            //if (_dsDbLA != null && _dsDbLA.Tables.Count > 0 && _dsDbLA.Tables[0].Rows.Count > 0)
            //{
            string strFirstName = string.Empty;
            string strLastName = string.Empty;
            //bool strGender = chkClientGender.Checked?true:false;
            string strDob = string.Empty;
            //set values
            strFirstName = Convert.ToString(txtLaFirstName.Text);
            strLastName = Convert.ToString(txtLaLastName.Text);
            //strGender = (chkClientGender.Checked);
            strDob = Convert.ToString(txtLaDob.Text);
            objBusinessLayer.DedupeSearch_GET(ref _ds, strFirstName, strLastName, (string.IsNullOrEmpty(strDob) ? string.Empty : Convert.ToDateTime(strDob).ToString("MM-dd-yyyy")), (chkClientGender.Checked) ? 'M' : 'F', string.Empty);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                dgUwDedupe.DataSource = _ds;
                dgUwDedupe.DataBind();
                divDgClientDedupe.Attributes["class"] = divDgClientDedupe.Attributes["class"].Replace(" HideControl", string.Empty);
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "Record fetched successfully!";
                GetSelectedRecord();
            }
            else
            {
                dgUwDedupe.DataSource = null;
                dgUwDedupe.DataBind();
                divDgClientDedupe.Attributes["class"] = divDgClientDedupe.Attributes["class"] + " HideControl";
                lblMsg.ForeColor = System.Drawing.Color.Blue;
                lblMsg.Text = "No record found!";
            }
            //}
            //strRet = FillDedupeDetails(_ds);
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please Try again";
        }
        finally
        {
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
            ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
        }
    }
    private void InsertUwDecisionTracking(string strApplicationNo, string strUserId, DateTime dtCurrentDateTime, string strEventName, ref int intRet)
    {
        objcomm = new Commfun();
        objcomm.InsertUwDecisionTracking(strApplicationNo, strUserId, dtCurrentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fff"), strEventName, ref intRet);
    }
    private void UpdateUwDecisionTracking(int intSrNo, DateTime dtEndDate, ref int intRet)
    {
        objcomm = new Commfun();
        objcomm.UpdateUwDecisionTracking(intSrNo, dtEndDate.ToString("yyyy-MM-dd HH:mm:ss:fff"), ref intRet);
    }
    private void GetSelectedRecord()
    {
        for (int i = 0; i < dgUwDedupe.Rows.Count; i++)
        {
            RadioButton rb = (RadioButton)dgUwDedupe.Rows[i]
                            .Cells[0].FindControl("RadioButton1");
            if (rb != null)
            {
                if (rb.Checked)
                {
                    HiddenField hf = (HiddenField)dgUwDedupe.Rows[i]
                                    .Cells[0].FindControl("HiddenField1");
                    if (hf != null)
                    {
                        ViewState["SelectedContact"] = hf.Value;
                    }
                    break;
                }
            }
        }
    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        GetSelectedRecord();
        if (ViewState["SelectedContact"].ToString() != null)
        {
            string ClientID = ViewState["SelectedContact"].ToString();
            FetchDedupDetails(ClientID);
        }
    }
    private void FetchDedupDetails(string strClientId)
    {
        try
        {
            lblMsg.Text = string.Empty;
            clearcontrols();
            BussLayer objBusinessLayer = new BussLayer();
            DataSet _ds = new DataSet();
            objBusinessLayer.DedupeDetails_GET(ref _ds, strClientId);
            if (_ds.Tables[0].Rows.Count != 0)
            {
                txtLaClientId.Text = Convert.ToString(_ds.Tables[0].Rows[0]["CLIENTID"]);
                if (cbSalutation.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[0]["SALUTATION"])) != null)
                {
                    cbSalutation.ClearSelection();
                    cbSalutation.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[0]["SALUTATION"])).Selected = true;
                }
                txtLaFirstName.Text = Convert.ToString(_ds.Tables[0].Rows[0]["FIRST_NAME"]);
                txtLaMiddleName.Text = Convert.ToString(_ds.Tables[0].Rows[0]["MIDDLE_NAME"]);
                txtLaLastName.Text = Convert.ToString(_ds.Tables[0].Rows[0]["LAST_NAME"]);
                txtLaDob.Text = Convert.ToString(_ds.Tables[0].Rows[0]["CLIENT_DOB"]);
                chkClientGender.Checked = Convert.ToBoolean(Convert.ToString(_ds.Tables[0].Rows[0]["CLIENT_GENDER"]).Equals("M")) ? true : false;
                if (cbOccupation.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[0]["OCCUPATION"])) != null)
                {
                    cbOccupation.ClearSelection();
                    cbOccupation.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[0]["OCCUPATION"])).Selected = true;
                }
                if (cbNationality.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[0]["NATLTY"])) != null)
                {
                    cbNationality.ClearSelection();
                    cbNationality.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[0]["NATLTY"])).Selected = true;
                }

            }
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    if (Convert.ToString(_ds.Tables[0].Rows[i]["ADDRTYPE"]) == "P")
                    {
                        txtPermanentAddress1.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR1"]);
                        txtPermanentAddress2.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR2"]);
                        txtPermanentAddress3.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR3"]);
                        txtPermanentAddress4.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR4"]);
                        txtPermanentAddress5.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR5"]);
                        txtPermanentLandmark.Text = Convert.ToString(_ds.Tables[0].Rows[i]["LANDMARK"]);
                        txtPermanentPinCode.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PINCODE"]);
                        txtPermanentCity.Text = Convert.ToString(_ds.Tables[0].Rows[i]["CITY"]);
                        txtPermanentDistrict.Text = Convert.ToString(_ds.Tables[0].Rows[i]["DISTRICT"]);
                        txtPermanentState.Text = Convert.ToString(_ds.Tables[0].Rows[i]["STATE"]);
                        Session["PermanentState"] = Convert.ToString(_ds.Tables[0].Rows[i]["STATE"]);
                        if (txtPermanentCountryCode.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[i]["COUNTRY_CODE"])) != null)
                        {
                            txtPermanentCountryCode.ClearSelection();
                            txtPermanentCountryCode.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[i]["COUNTRY_CODE"])).Selected = true;
                        }
                        txtPermanentAddressRemark.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDRESS_REMARK"]);
                        txtPermanentPhone1.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PHONE1"]);
                        txtPermanentPhone2.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PHONE2"]);
                        txtPermanentTelNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["TEL_NO"]);
                        txtPermanentMobileNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["MOBILE_NO"]);
                        txtPermanentEmailId.Text = Convert.ToString(_ds.Tables[0].Rows[i]["EMAIL_ID"]);
                        txtPermanentFaxNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["FAX"]);
                    }
                    if (Convert.ToString(_ds.Tables[0].Rows[i]["ADDRTYPE"]) == "R")
                    {
                        txtCommuAddress1.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR1"]);
                        txtCommuAddress2.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR2"]);
                        txtCommuAddress3.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR3"]);
                        txtCommuAddress4.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR4"]);
                        txtCommuAddress5.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR5"]);
                        txtCommuLandmark.Text = Convert.ToString(_ds.Tables[0].Rows[i]["LANDMARK"]);
                        txtCommuPinCode.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PINCODE"]);
                        txtCommuCity.Text = Convert.ToString(_ds.Tables[0].Rows[i]["CITY"]);
                        txtCommuDistrict.Text = Convert.ToString(_ds.Tables[0].Rows[i]["DISTRICT"]);
                        txtCommuState.Text = Convert.ToString(_ds.Tables[0].Rows[i]["STATE"]);
                        if (txtCommuCountryCode.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[i]["COUNTRY_CODE"])) != null)
                        {
                            txtCommuCountryCode.ClearSelection();
                            txtCommuCountryCode.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[i]["COUNTRY_CODE"])).Selected = true;
                        }
                        txtCommuAddressRemark.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDRESS_REMARK"]);
                        txtCommuPhone1.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PHONE1"]);
                        txtCommuPhone2.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PHONE2"]);
                        txtCommuTelNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["TEL_NO"]);
                        txtCommuMobileNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["MOBILE_NO"]);
                        txtCommuEmailId.Text = Convert.ToString(_ds.Tables[0].Rows[i]["EMAIL_ID"]);
                        txtCommuFaxNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["FAX"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please try again";
        }
    }

    private void clearcontrols()
    {
        lblMsg.Text = string.Empty;

        txtLaClientId.Text = string.Empty;
        cbSalutation.SelectedIndex = 0;
        txtLaFirstName.Text = string.Empty;
        txtLaMiddleName.Text = string.Empty;
        txtLaLastName.Text = string.Empty;
        txtLaDob.Text = string.Empty;
        chkClientGender.Checked = false;
        cbOccupation.SelectedIndex = 0;
        cbNationality.SelectedIndex = 0;

        txtPermanentAddress1.Text = string.Empty;
        txtPermanentAddress2.Text = string.Empty;
        txtPermanentAddress3.Text = string.Empty;
        txtPermanentAddress4.Text = string.Empty;
        txtPermanentAddress5.Text = string.Empty;
        txtPermanentLandmark.Text = string.Empty;
        txtPermanentPinCode.Text = string.Empty;
        txtPermanentCity.Text = string.Empty;
        txtPermanentDistrict.Text = string.Empty;
        txtPermanentState.Text = string.Empty;
        txtPermanentCountryCode.SelectedIndex = 0;
        txtPermanentAddressRemark.Text = string.Empty;
        txtPermanentPhone1.Text = string.Empty;
        txtPermanentPhone2.Text = string.Empty;
        txtPermanentTelNo.Text = string.Empty;
        txtPermanentMobileNo.Text = string.Empty;
        txtPermanentEmailId.Text = string.Empty;
        txtPermanentFaxNo.Text = string.Empty;

        txtCommuAddress1.Text = string.Empty;
        txtCommuAddress2.Text = string.Empty;
        txtCommuAddress3.Text = string.Empty;
        txtCommuAddress4.Text = string.Empty;
        txtCommuAddress5.Text = string.Empty;
        txtCommuLandmark.Text = string.Empty;
        txtCommuPinCode.Text = string.Empty;
        txtCommuCity.Text = string.Empty;
        txtCommuDistrict.Text = string.Empty;
        txtCommuState.Text = string.Empty;
        txtCommuCountryCode.SelectedIndex = 0;
        txtCommuAddressRemark.Text = string.Empty;
        txtCommuPhone1.Text = string.Empty;
        txtCommuPhone2.Text = string.Empty;
        txtCommuTelNo.Text = string.Empty;
        txtCommuMobileNo.Text = string.Empty;
        txtCommuEmailId.Text = string.Empty;
        txtCommuFaxNo.Text = string.Empty;
    }
    public void FetchCommonData()
    {

        try
        {
            lblMsg.Text = string.Empty;
            DataSet ds = new DataSet();
            (new Commfun()).FetchCommonDropDownList(ref ds);
            //strRet = FillCommonDropDownToList(ds);
            cbSalutation.DataSource = ds.Tables[3];
            cbSalutation.DataTextField = "Name";
            cbSalutation.DataValueField = "Value";
            cbSalutation.DataBind();
            cbSalutation.Items.Insert(0, new ListItem("--Select--", "0"));
            cbOccupation.DataSource = ds.Tables[2];
            cbOccupation.DataTextField = "Name";
            cbOccupation.DataValueField = "Value";
            cbOccupation.DataBind();
            cbOccupation.Items.Insert(0, new ListItem("--Select--", "0"));
            cbNationality.DataSource = ds.Tables[1];
            cbNationality.DataTextField = "Name";
            cbNationality.DataValueField = "Value";
            cbNationality.DataBind();
            cbNationality.Items.Insert(0, new ListItem("--Select--", "0"));
            txtCommuCountryCode.DataSource = ds.Tables[0];
            txtCommuCountryCode.DataTextField = "Name";
            txtCommuCountryCode.DataValueField = "Value";
            txtCommuCountryCode.DataBind();
            txtCommuCountryCode.Items.Insert(0, new ListItem("--Select--", "0"));
            txtPermanentCountryCode.DataSource = ds.Tables[0];
            txtPermanentCountryCode.DataTextField = "Name";
            txtPermanentCountryCode.DataValueField = "Value";
            txtPermanentCountryCode.DataBind();
            txtPermanentCountryCode.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please try again";
            //UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            //Logger.Error(txtAppno.Text + ":UserControl :UserControl_PopupManageProposar // MethodeName :FetchCommonData");
            //objCommFun.SaveErrorLogs(txtAppno.Text, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "FetchCommonData", "E-Error", "", "", ex.ToString());
        }

    }

    protected void btnAddNominee_Click(object sender, EventArgs e)
    {
        try
        {
            cbAddressCopy.Checked = false;
            lblMsg.Text = string.Empty;
            validatecontrol();
            //CreateClientInfo();
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please try again";
        }
    }

    private void validatecontrol()
    {
        System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        if (string.IsNullOrEmpty(txtAppno.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please Enter Application Number";
            return;
        }
        if (cbSalutation.SelectedIndex == 0)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter salutation";
            return;
        }
        if (string.IsNullOrEmpty(txtLaFirstName.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter first name";
            return;
        }
        if (string.IsNullOrEmpty(txtLaLastName.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter last name";
            return;
        }
        if (string.IsNullOrEmpty(txtLaDob.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter DOB";
            return;
        }
        if (cbOccupation.SelectedIndex == 0)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter occuption";
            return;
        }
        if (cbNationality.SelectedIndex == 0)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter nationality";
            return;
        }
        if (cbAddressCopy.Checked == false)
        {
            if (string.IsNullOrEmpty(txtPermanentAddress1.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter permanent-Address1";
                return;
            }
            if (string.IsNullOrEmpty(txtPermanentPinCode.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter permanent-Pincode";
                return;
            }
            if (string.IsNullOrEmpty(txtPermanentCity.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter permanent-City";
                return;
            }
            if (string.IsNullOrEmpty(txtPermanentDistrict.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter permanent-District";
                return;
            }
            if (string.IsNullOrEmpty(txtPermanentState.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter permanent-State";
                return;
            }
            if (txtPermanentCountryCode.SelectedIndex == 0)
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter permanent-Countrycode";
                return;
            }

            if (string.IsNullOrEmpty(txtPermanentMobileNo.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Please enter permanent-MobileNo";
                return;
            }
            if (txtPermanentEmailId.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtPermanentEmailId.Text))
                {
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    lblMsg.Text = "Invalid permanent-email address.";
                    return;
                }
            }
        }

        if (string.IsNullOrEmpty(txtCommuAddress1.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter Communiction-Address1";
            return;
        }
        if (string.IsNullOrEmpty(txtCommuPinCode.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter Communiction-Pincode";
            return;
        }
        if (string.IsNullOrEmpty(txtCommuCity.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter Communiction-City";
            return;
        }
        if (string.IsNullOrEmpty(txtCommuDistrict.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter Communiction-District";
            return;
        }
        if (string.IsNullOrEmpty(txtCommuState.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter Communiction-State";
            return;
        }
        if (txtCommuCountryCode.SelectedIndex == 0)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter Communiction-Countrycode";
            return;
        }
        if (string.IsNullOrEmpty(txtCommuMobileNo.Text))
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Please enter Communiction-MobileNo";
            return;
        }
        if (txtCommuEmailId.Text.Length > 0)
        {
            if (!rEMail.IsMatch(txtCommuEmailId.Text))
            {
                lblMsg.ForeColor = System.Drawing.Color.Red;
                lblMsg.Text = "Invalid Communiction-email address.";
                return;
            }
        }
        //IF CLIENT ID TEXT FIELD IS NULL OR EMPTY CALL CLIENT CREATION 
        CreateClientInfo();
    }
    public void CreateClientInfo()
    {
        CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
        ChangeValue objChangeObj = new ChangeValue();
        string strLApushStatus = string.Empty;
        int strLApushErrorCode = -1;
        DataSet _ds = new DataSet();
        string strRet = string.Empty;
        string strSpace = " ";
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        ClientDetails objClientDetails = new ClientDetails();
        try
        {
            objClientDetails.lstClientAddress = new List<ClientAddress>();
            objClientDetails.ApplicationNo = txtAppno.Text;
            objClientDetails.AssuredType = objClientDetails.ClientRole = "Nominee";
            objClientDetails.ClientType = "P";
            //lstClientAddress
            //fill client communication address
            ClientAddress objCommunClientAddress = new ClientAddress();
            //List<ClientAddress> lstClientAddress = new List<ClientAddress>();
            objCommunClientAddress.AddressLine1 = txtCommuAddress1.Text;
            objCommunClientAddress.AddressLine2 = txtCommuAddress2.Text;
            objCommunClientAddress.AddressLine3 = txtCommuAddress3.Text;
            objCommunClientAddress.AddressLine4 = txtCommuAddress4.Text;
            objCommunClientAddress.AddressLine5 = txtCommuAddress5.Text;
            objCommunClientAddress.LandMark = txtCommuLandmark.Text;
            objCommunClientAddress.PinCode = string.IsNullOrEmpty(txtCommuPinCode.Text) ? -1 : Convert.ToInt32(txtCommuPinCode.Text);
            objCommunClientAddress.City = txtCommuCity.Text;
            objCommunClientAddress.District = txtCommuDistrict.Text;
            objCommunClientAddress.State = txtCommuState.Text;
            objCommunClientAddress.CountryCode = txtCommuCountryCode.SelectedValue;
            objCommunClientAddress.AddressRemark = txtCommuAddressRemark.Text;
            objCommunClientAddress.Phone1 = txtCommuPhone1.Text;
            objCommunClientAddress.Phone2 = txtCommuPhone2.Text;
            objCommunClientAddress.TelNo = txtCommuTelNo.Text;
            objCommunClientAddress.MobileNo = txtCommuMobileNo.Text;
            objCommunClientAddress.EmailId = txtCommuEmailId.Text;
            objCommunClientAddress.FaxNo = txtCommuFaxNo.Text;
            objCommunClientAddress.AddressType = "R";
            objClientDetails.lstClientAddress.Add(objCommunClientAddress);

            //fill client permanent address        
            ClientAddress objPermanentClientAddress = new ClientAddress();
            objPermanentClientAddress.AddressLine1 = txtPermanentAddress1.Text;
            objPermanentClientAddress.AddressLine2 = txtPermanentAddress2.Text;
            objPermanentClientAddress.AddressLine3 = txtPermanentAddress3.Text;
            objPermanentClientAddress.AddressLine4 = txtPermanentAddress4.Text;
            objPermanentClientAddress.AddressLine5 = txtPermanentAddress5.Text;
            objPermanentClientAddress.LandMark = txtPermanentLandmark.Text;
            objPermanentClientAddress.PinCode = (string.IsNullOrEmpty(txtPermanentPinCode.Text)) ? -1 : Convert.ToInt32(txtPermanentPinCode.Text);
            objPermanentClientAddress.City = txtPermanentCity.Text;
            objPermanentClientAddress.District = txtPermanentDistrict.Text;
            objPermanentClientAddress.State = txtPermanentState.Text;
            objPermanentClientAddress.CountryCode = txtPermanentCountryCode.SelectedValue;
            objPermanentClientAddress.AddressRemark = txtPermanentAddressRemark.Text;
            objPermanentClientAddress.Phone1 = txtPermanentPhone1.Text;
            objPermanentClientAddress.Phone2 = txtPermanentPhone2.Text;
            objPermanentClientAddress.TelNo = txtPermanentTelNo.Text;
            objPermanentClientAddress.MobileNo = txtPermanentMobileNo.Text;
            objPermanentClientAddress.EmailId = txtPermanentEmailId.Text;
            objPermanentClientAddress.FaxNo = txtPermanentFaxNo.Text;
            objPermanentClientAddress.AddressType = "P";
            objClientDetails.lstClientAddress.Add(objPermanentClientAddress);


            //fill client object        
            objClientDetails.Salutation = cbSalutation.SelectedValue;
            objClientDetails.ClientId = txtLaClientId.Text;
            objClientDetails.ClientFirstName = txtLaFirstName.Text;
            objClientDetails.ClientFatherName = txtLaMiddleName.Text;
            objClientDetails.ClinetLastName = txtLaLastName.Text;
            objClientDetails.ClientDob = Convert.ToDateTime(txtLaDob.Text);
            objClientDetails.ClientGender = (chkClientGender.Checked) ? 'M' : 'F';
            //objClientDetails.IsSmoker = txt;
            //objClientDetails.ApplicationNo = strApplicationNo;
            //objClientDetails.ClientRole = strRole;
            objClientDetails.Nationality = cbNationality.SelectedValue;
            //objClientDetails.IsNSAP = isNasp;
            objClientDetails.Occupation = cbOccupation.SelectedValue;

            //fill bmp details
            if (Session["objLoginObj"] != null)
            {
                ChangeValue objChangeValue = (ChangeValue)System.Web.HttpContext.Current.Session["objLoginObj"];
                objClientDetails.objBmpUserInfo = objChangeValue.userLoginDetails;
            }


            //
            objChangeObj.ClientDetails = objClientDetails;

            if (txtLaClientId.Text == string.Empty)
            {

                ClientCreation(objCommonObj, objChangeObj, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                if (strLApushErrorCode == 0)
                {
                    objChangeObj.ClientDetails.ClientId = strLApushStatus;
                }
            }
            else
            {
                strLApushErrorCode = 0;
            }
            //save it in our db
            string strOldClientId = string.Empty;
            if (strLApushErrorCode == 0)
            {
                (new Commfun()).ClientFullInfo_Insert(objClientDetails, strOldClientId, ref  strLApushErrorCode);
            }
            else if (strLApushErrorCode > 0)
            {
                strRet = strRet + strSpace + strLApushStatus;
            }
            //
            string strApplicationNo = string.Empty;
            string strChannel = string.Empty;
            strChannel = objCommonObj._ChannelType;
            string strConsentRespons = string.Empty;
            DataSet _dsPrevPol = new DataSet();
            UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
            objUWDecision.OnlineApplicationLAServiceDetails_PUSH(txtAppno.Text.Trim(), strChannel, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
            strRet = strRet + strSpace + strLApushStatus;
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = strRet;
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Try again later";
        }
    }
    private void ClientCreation(CommonObject objCommonObj, ChangeValue objChangeObj, ref DataSet _ds, ref int strLApushErrorCode, ref string strLApushStatus)
    {
        DataSet _dsPrevPol = new DataSet();
        string strConsentRespons = string.Empty;
        UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(txtAppno.Text.Trim(), objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CLIENTCREATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string FillNestedDetails(string strPinCode)
    {
        string strRet = string.Empty;

        try
        {
            strRet = FillCoutryStateCityString(strPinCode);
        }
        catch (Exception ex)
        {
            strRet = string.Empty + "#" + string.Empty + "#" + string.Empty;
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
        }
        return strRet;
    }
    private string FillCoutryStateCityString(string strPincode)
    {
        DataSet ds = new DataSet();
        string strRet = string.Empty;
        Commfun objCommfun = new Commfun();
        objCommfun.FetchCountryStateCityFromPincode(ref ds, strPincode);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            strRet = Convert.ToString(ds.Tables[0].Rows[0]["CITY"]) + "#" + Convert.ToString(ds.Tables[0].Rows[0]["DISTRICT"]) + "#" + Convert.ToString(ds.Tables[0].Rows[0]["STATE"]);
        }
        else
        {
            strRet = string.Empty + "#" + string.Empty + "#" + string.Empty;
        }
        return strRet;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        cbAddressCopy.Checked = false;
        clearcontrols();
    }
    protected void btnFillLADetails_Click(object sender, EventArgs e)
    {
        try
        {
            //clearcontrols
            txtCommuAddress1.Text = string.Empty;
            txtCommuAddress2.Text = string.Empty;
            txtCommuAddress3.Text = string.Empty;
            txtCommuAddress4.Text = string.Empty;
            txtCommuAddress5.Text = string.Empty;
            txtCommuLandmark.Text = string.Empty;
            txtCommuPinCode.Text = string.Empty;
            txtCommuCity.Text = string.Empty;
            txtCommuDistrict.Text = string.Empty;
            txtCommuState.Text = string.Empty;
            txtCommuCountryCode.SelectedIndex = 0;
            txtCommuAddressRemark.Text = string.Empty;
            txtCommuPhone1.Text = string.Empty;
            txtCommuPhone2.Text = string.Empty;
            txtCommuTelNo.Text = string.Empty;
            txtCommuMobileNo.Text = string.Empty;
            txtCommuEmailId.Text = string.Empty;
            txtCommuFaxNo.Text = string.Empty;
            //end 
            lblMsg.Text = string.Empty;
            cbAddressCopy.Checked = false;

            if (txtAppno.Text != string.Empty)
            {
                BussLayer objBusinessLayer = new BussLayer();
                DataSet _ds = new DataSet();
                DataSet ds = new DataSet();
                string strClientId = string.Empty;
                FetchClientBasicDetails(ref ds, txtAppno.Text);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    string strClientRole = ds.Tables[0].Rows[i]["Role"].ToString();
                    if (strClientRole.Equals("LA"))
                    {
                        strClientId = ds.Tables[0].Rows[i]["ClientID"].ToString();
                    }

                }

                objBusinessLayer.ClientFullInfo_GET(ref _ds, strClientId, Convert.ToString(txtAppno.Text.Trim()), "LA");

                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {

                        if (Convert.ToString(_ds.Tables[0].Rows[i]["ADDRTYPE"]) == "R")
                        {
                            txtCommuAddress1.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR1"]);
                            txtCommuAddress2.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR2"]);
                            txtCommuAddress3.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR3"]);
                            txtCommuAddress4.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR4"]);
                            txtCommuAddress5.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDR5"]);
                            txtCommuLandmark.Text = Convert.ToString(_ds.Tables[0].Rows[i]["LANDMARK"]);
                            txtCommuPinCode.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PINCODE"]);
                            txtCommuCity.Text = Convert.ToString(_ds.Tables[0].Rows[i]["CITY"]);
                            txtCommuDistrict.Text = Convert.ToString(_ds.Tables[0].Rows[i]["DISTRICT"]);
                            txtCommuState.Text = Convert.ToString(_ds.Tables[0].Rows[i]["STATE"]);
                            if (txtCommuCountryCode.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[i]["COUNTRY_CODE"])) != null)
                            {
                                txtCommuCountryCode.ClearSelection();
                                txtCommuCountryCode.Items.FindByValue(Convert.ToString(_ds.Tables[0].Rows[i]["COUNTRY_CODE"])).Selected = true;
                            }
                            txtCommuAddressRemark.Text = Convert.ToString(_ds.Tables[0].Rows[i]["ADDRESS_REMARK"]);
                            txtCommuPhone1.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PHONE1"]);
                            txtCommuPhone2.Text = Convert.ToString(_ds.Tables[0].Rows[i]["PHONE2"]);
                            txtCommuTelNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["TEL_NO"]);
                            txtCommuMobileNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["MOBILE_NO"]);
                            txtCommuEmailId.Text = Convert.ToString(_ds.Tables[0].Rows[i]["EMAIL_ID"]);
                            txtCommuFaxNo.Text = Convert.ToString(_ds.Tables[0].Rows[i]["FAX"]);
                        }
                    }
                }
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
}