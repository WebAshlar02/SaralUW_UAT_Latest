using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_IIBQuery : System.Web.UI.Page
{

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
            }
        }
        else
        {
            Response.Redirect("../9011042143.aspx");
        }
    }

    protected void btnIIBEnquiry_Click(object sender, EventArgs e)
    {
        lblSuccess.Visible = false;
        lblSuccess.Text = "";
        IIBEnquryCoustomerDO ObjCoustomerDO = new IIBEnquryCoustomerDO();
        try
        {

            ObjCoustomerDO.ApplicationNo = txtApplicationNo.Text.Trim();
            ObjCoustomerDO.FirstName = txtFirstName.Text.Trim();
            ObjCoustomerDO.SurName = txtSurName.Text.Trim();
            ObjCoustomerDO.DOB = txtDBO.Text.Trim();
            ObjCoustomerDO.Gender = dllGender.SelectedValue;
            ObjCoustomerDO.PanNo = txtPAN.Text.Trim();
            ObjCoustomerDO.AnnualIncome = txtAnnualIncome.Text.Trim();
            ObjCoustomerDO.Email = txtEmail.Text.Trim();
            ObjCoustomerDO.MobileNo = txtMobNo.Text.Trim();
            ObjCoustomerDO.PhoneNumber = txtPhoneNo.Text.Trim();
            ObjCoustomerDO.PinCode = txtPinCode.Text.Trim();
            ObjCoustomerDO.ProductCode = txtproductCode.Text.Trim();
            ObjCoustomerDO.Address = txtaddress.Text.Trim();

            IIBMainEnquryResponseDO ObjIIBResponse = new IIBMainEnquryResponseDO();
            IIBEnquiryData objfgData = new IIBEnquiryData();
            IIBEnquiryData objOtherData = new IIBEnquiryData();
            DataTable dt12 = new DataTable();
            DataTable dtFGData = new DataTable();
            DataTable dtOtherFGData = new DataTable();
           
            ObjIIBResponse = GetIIBEnquiryMatchAPIResponse(ObjCoustomerDO.ApplicationNo, "GetIIBMatchAPIResponse", ObjCoustomerDO.FirstName, ObjCoustomerDO.SurName, ObjCoustomerDO.DOB, ObjCoustomerDO.Gender,
                                                          ObjCoustomerDO.PinCode, "", ObjCoustomerDO.PanNo, ObjCoustomerDO.Address,
                                                          ObjCoustomerDO.Address, ObjCoustomerDO.MobileNo, ObjCoustomerDO.PhoneNumber,
                                                          ObjCoustomerDO.Email, ObjCoustomerDO.ProductCode,
                                                          ObjCoustomerDO.AnnualIncome);

            if (ObjIIBResponse != null)
            {
                if (ObjIIBResponse.ResponseCode == 0)
                {
                    if (!string.IsNullOrEmpty(ObjIIBResponse.Status))
                    {
                        if (ObjIIBResponse.Status.ToLower()== "success")
                        {
                            if ((ObjIIBResponse.DecodedString1.ToLower()).Replace(" ", "")!= "nomatchfound")
                            {
                                string FGData = JsonConvert.SerializeObject(ObjIIBResponse.FGData);
                                string OtherData = JsonConvert.SerializeObject(ObjIIBResponse.OtherData);
                                int CntFGdata = ObjIIBResponse.FGData.Count;
                                int CntOtherdata = ObjIIBResponse.OtherData.Count;
                                SetInitialRow(ref dt12, "IIB");
                                dtFGData = dt12.Copy();
                                dtOtherFGData = dt12.Copy();
                                for (int i = 0; i < CntFGdata; i++)
                                {
                                    objfgData = ObjIIBResponse.FGData[i];
                                    dtFGData = IIBEnquiryFGData(objfgData, dtFGData, ObjCoustomerDO.ApplicationNo, strUserId, ObjCoustomerDO.PanNo, "FG");
                                }

                                for (int j = 0; j < CntOtherdata; j++)
                                {
                                    objOtherData = ObjIIBResponse.OtherData[j];
                                    dtOtherFGData = IIBEnquiryFGData(objOtherData, dtOtherFGData, ObjCoustomerDO.ApplicationNo, strUserId, ObjCoustomerDO.PanNo, "OTHER");
                                }

                                #region For FG
                                if (CntFGdata > 0)
                                {
                                    if (dtFGData.Rows.Count > 0)
                                    {
                                        new Commfun().InsertIIBQueryData(dtFGData, ObjCoustomerDO.PanNo, ObjCoustomerDO.ApplicationNo, strUserId);

                                        divLA.Style.Add("display", "block");
                                        GridViewFG.DataSource = dtFGData;
                                        GridViewFG.DataBind();
                                        GridViewFG.Visible = true;
                                    }
                                }
                                #endregion

                                #region For Other than FG
                                if (CntOtherdata > 0)
                                {
                                    if (dtOtherFGData.Rows.Count > 0)
                                    {
                                        new Commfun().InsertIIBQueryData(dtOtherFGData, ObjCoustomerDO.PanNo, ObjCoustomerDO.ApplicationNo, strUserId);

                                        divother.Style.Add("display", "block");
                                        GridViewOther.DataSource = dtOtherFGData;
                                        GridViewOther.DataBind();
                                        GridViewOther.Visible = true;

                                    }

                                }
                                #endregion
                                Clear();
                            }
                            else
                            {

                                lblSuccess.Visible = true;
                                lblSuccess.Text ="IIB Respones: "+ ObjIIBResponse.DecodedString1;
                                lblSuccess.ForeColor = System.Drawing.Color.DarkGreen;

                                Clear();
                            }
                        }
                    }
                    else
                    {

                        lblSuccess.Visible = true;
                        lblSuccess.Text = "There is some error occur while fetching the IIB data";
                        lblSuccess.ForeColor = System.Drawing.Color.Red;

                    }
                }
                else
                {
                    divother.Style.Add("display", "none");
                    GridViewFG.DataSource = null;
                    GridViewFG.DataBind();
                    GridViewFG.Visible = false;
                    divLA.Style.Add("display", "none");
                    GridViewOther.DataSource = null;
                    GridViewOther.DataBind();
                    GridViewOther.Visible = false;
                    lblSuccess.Visible = true;
                    lblSuccess.Text = "IIB Respones is failed.Please try again for sometime.";
                    lblSuccess.ForeColor = System.Drawing.Color.Red;

                }

            }
          
        }
        catch (Exception ex)
        {
            new Commfun().InsertIIBexception(ObjCoustomerDO.PanNo, "btnIIBEnquiry_Click", ObjCoustomerDO.ApplicationNo, strUserId, ex.Message);
            lblSuccess.Visible = true;
            lblSuccess.Text = "Error: "+ ex.Message;
            lblSuccess.ForeColor = System.Drawing.Color.Red;
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading();</script>", false);
    }


    #region IIB Response Action
    public IIBMainEnquryResponseDO GetIIBEnquiryMatchAPIResponse(string Applicationno, string EventName, string firstname, string surname, string dob, string gender,
    string pincode, string aadharcard, string PanNo, string Current_Address, string Permanent_Address, string MobNo,
    string Phone_Number_2, string Email_1, string ProductCode, string Annual_Income)

    {
        DataSet dt1 = new DataSet();
        Session["IIBFailResp"] = string.Empty;
        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
        string strJsonRequest = "";
        string strJsonResponse = "";
        IIBEnquiryRequestDO objIIBRequest = new IIBEnquiryRequestDO();
        IIBMainEnquryResponseDO ObjIIBResponse = new IIBMainEnquryResponseDO();
    

        string Token = GenerateTokenIIBEnquiry(PanNo,Applicationno);
        if (!string.IsNullOrEmpty(Token))
        {
            try
            {
                string DOB = Convert.ToString(Convert.ToDateTime(dob).ToString("MM/dd/yyyy"));
                objIIBRequest.DataKeyName = "ApplicationNo";
                objIIBRequest.DataKeyValue = Applicationno;
                objIIBRequest.ClientID = "";
                objIIBRequest.FirstName = firstname;
                objIIBRequest.SurName = surname;
                objIIBRequest.DOB = DOB;
                objIIBRequest.PanNo = PanNo;
                objIIBRequest.PinCode = pincode;
                objIIBRequest.Current_Address = Current_Address;
                objIIBRequest.Permanent_Address = Permanent_Address;
                objIIBRequest.Gender = gender;
                objIIBRequest.Email_1 = Email_1;
                objIIBRequest.Phone_Number_1 = MobNo.ToString();
                objIIBRequest.Phone_Number_2 = Phone_Number_2;
                objIIBRequest.ProductCode = ProductCode;
                objIIBRequest.Annual_Income = Annual_Income;
                strJsonRequest = objJavaScriptSerializer.Serialize(objIIBRequest);
                new Commfun().InsertIIBEnquireyRequestDetails(objIIBRequest, strUserId);
                string IIBDecodingString = string.Empty;
                int ResponseCode = 0;
                string Status = string.Empty;
                try
                {

                    System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    System.Net.WebClient objWebClient = new System.Net.WebClient();
                    objWebClient.Headers["Content-type"] = "application/json";
                    objWebClient.Headers["Ocp-Apim-Subscription-Key"] = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"].ToString();
                    objWebClient.Headers["Authorization"] = Token;
                    objWebClient.Encoding = Encoding.UTF8;
                    strJsonResponse = objWebClient.UploadString(ConfigurationManager.AppSettings["SmartApiIIBMatch"].ToString(), strJsonRequest);

                    // strJsonResponse = new IIBQueryBLL().GetResponseFromWebApi(System.Configuration.ConfigurationManager.AppSettings["SmartApiIIBMatch"], strJsonRequest);
                   
                    ObjIIBResponse = objJavaScriptSerializer.Deserialize<IIBMainEnquryResponseDO>(strJsonResponse);

                    if (ObjIIBResponse != null)
                    {
                        IIBDecodingString = ObjIIBResponse.DecodedString1.ToString();
                        ResponseCode = ObjIIBResponse.ResponseCode;
                        Status = ObjIIBResponse.Status;
                    }
                    new Commfun().InsertIIBEnquiryLog(PanNo, Applicationno, "GetIIBEnquiryMatchAPIResponse", strJsonRequest, strJsonResponse.ToString(), Status, IIBDecodingString, strUserId, Convert.ToInt32(ResponseCode));

                }
                catch (System.Net.WebException ex)
                {
                    new Commfun().InsertIIBEnquiryLog(PanNo, Applicationno, "GetIIBEnquiryMatchAPIResponse", strJsonRequest, strJsonResponse.ToString(), "", "", strUserId, 0);
                    new Commfun().InsertIIBexception(PanNo, "GetIIBEnquiryMatchAPIResponse", Applicationno, strUserId, ex.Message);
                }

            }
            catch (Exception ex)
            {
                new Commfun().InsertIIBexception(PanNo, "GetIIBEnquiryMatchAPIResponse", Applicationno, strUserId, ex.Message);
            }
        }
        return ObjIIBResponse;


    }

    #endregion
    #region Token Generated
    public string GenerateTokenIIBEnquiry(string PanNo,string ApplicationNo)
    {
        string Token = "";
        string TokenResponse = "";
        string inputTokenJson = "{"
                                    + "\"" + "ClientID" + "\"" + ":" + "\"" + ConfigurationManager.AppSettings["ClientID"].ToString() + "\"" + ","
                                    + "\"" + "ClientSecret" + "\"" + ":" + "\"" + ConfigurationManager.AppSettings["ClientSecret"].ToString() + "\"" + ","
                                    + "\"" + "Source" + "\"" + ":" + "\"" + ConfigurationManager.AppSettings["Source"].ToString() + "\"" + ","
                                    + "\"" + "PartnerID" + "\"" + ":" + "\"" + ConfigurationManager.AppSettings["PartnerID"].ToString() + "\""
                                + "}";

        Newtonsoft.Json.Linq.JObject JResponce = null;
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            System.Net.WebClient objWebClient = new System.Net.WebClient();
            objWebClient.Headers["Content-type"] = "application/json";
            objWebClient.Headers["Ocp-Apim-Subscription-Key"] = ConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"].ToString();
            objWebClient.Encoding = Encoding.UTF8;
            TokenResponse = objWebClient.UploadString(ConfigurationManager.AppSettings["IIBGenerateTokenAPILink"].ToString(), inputTokenJson);
            JResponce = Newtonsoft.Json.Linq.JObject.Parse(TokenResponse);

            string ErrorCode = Convert.ToString(JResponce["errorCode"]);
            string ErrorMessage = Convert.ToString(JResponce["errorMessage"]);
            if (ErrorCode == "0" && ErrorMessage.ToLower() == "success")
            {
                Token = Convert.ToString(JResponce["access_token"]);
            }

            new Commfun().InsertIIBEnquiryLog(PanNo,ApplicationNo, "GenerateTokenIIBEnquiry", inputTokenJson,JResponce.ToString(),ErrorMessage,"", strUserId,Convert.ToInt32(ErrorCode));

        }
        catch (System.Net.WebException ex)
        {
            new Commfun().InsertIIBEnquiryLog(PanNo, ApplicationNo, "GenerateTokenIIBEnquiry", inputTokenJson, JResponce.ToString(), "", "", strUserId, 0);
            new Commfun().InsertIIBexception(PanNo, "GenerateTokenIIBEnquiry", ApplicationNo, strUserId, ex.Message);
        }
        return Token;
  
    }

    #endregion


    #region IIB Response

    public DataTable SetInitialRow(ref DataTable dt, string Type)
    {
        switch (Type)
        {
            case "IIB":
                dt.Columns.Add("PANCardNo");
                dt.Columns.Add("Input_Proposal_Policy_No");
                dt.Columns.Add("QuestDBNo");
                dt.Columns.Add("Input_Matching_Parameter");
                dt.Columns.Add("Quest_DoP_DoC");
                dt.Columns.Add("Quest_Sum_Assured");
                dt.Columns["Quest_Sum_Assured"].DataType = typeof(double); 
                dt.Columns.Add("Quest_Policy_Status");
                dt.Columns.Add("Quest_Date_of_Exit");
                dt.Columns.Add("Quest_Date_of_Death");
                dt.Columns.Add("Quest_Cause_of_Death");
                dt.Columns.Add("Quest_Record_last_updated");
                dt.Columns.Add("Quest_Entity_Caution_Status");
                dt.Columns.Add("Quest_Intermediary_Caution_Status");
                dt.Columns.Add("Quest_Company_Number");
                dt.Columns.Add("Is_Negative_Match");
                dt.Columns.Add("LAProposerPayor");
                dt.Columns.Add("Status");
                dt.Columns.Add("IIBServiceResponse");
                dt.Columns.Add("Product_Type");
                dt.Columns.Add("Linked_Non_linked");
                dt.Columns.Add("Medical_Non_Medical");
                dt.Columns.Add("Whether_Standard_Life");
                dt.Columns.Add("Reason_for_Decline");
                dt.Columns.Add("Reason_for_Postpone");
                dt.Columns.Add("Reason_for_Repudiation");
                dt.Columns.Add("Type");
                dt.Columns.Add("CreatedBy");
                break;

            default:
                break;
        }

        return dt;

    }
   
    public DataTable IIBEnquiryFGData(IIBEnquiryData fgdt, DataTable dt12, string ApplicationNo,string UserId,string PANNo,string Type)
    {
        DataRow dr12 = null;
        dr12 = dt12.NewRow();
        dr12["PANCardNo"] = PANNo;
        dr12["Input_Proposal_Policy_No"] = ApplicationNo;
        dr12["QuestDBNo"] = fgdt.QuestDBNo;
        dr12["Input_Matching_Parameter"] = fgdt.Input_Matching_Parameter;
        dr12["Quest_DoP_DoC"] = fgdt.Quest_DoP_DoC;
        dr12["Quest_Sum_Assured"] = fgdt.Quest_Sum_Assured;
        dr12["Quest_Policy_Status"] = fgdt.Quest_Policy_Status;
        dr12["Quest_Date_of_Exit"] = fgdt.Quest_Date_of_Exit;
        dr12["Quest_Date_of_Death"] = fgdt.Quest_Date_of_Death;
        dr12["Quest_Cause_of_Death"] = fgdt.Quest_Cause_of_Death;
        dr12["Quest_Record_last_updated"] = fgdt.Quest_Record_last_updated;
        dr12["Quest_Entity_Caution_Status"] = fgdt.Quest_Entity_Caution_Status;
        dr12["Quest_Intermediary_Caution_Status"] = fgdt.Quest_Intermediary_Caution_Status;
        dr12["Quest_Company_Number"] = fgdt.Quest_Company_Number;
        dr12["Is_Negative_Match"] = fgdt.Is_Negative_Match;
        dr12["LAProposerPayor"] = fgdt.LAProposerPayor;
        dr12["Status"] = "Active";
        dr12["IIBServiceResponse"] = "Success";
        dr12["Product_Type"] = fgdt.Product_TypeOutput;
        dr12["Linked_Non_linked"] = fgdt.Linked_Non_linked;
        dr12["Medical_Non_Medical"] = fgdt.Medical_NonMedical;
        dr12["Whether_Standard_Life"] = fgdt.Whether_Standard_Life;
        dr12["Reason_for_Decline"] = fgdt.Reason_for_Decline;
        dr12["Reason_for_Postpone"] = fgdt.Reason_for_Postpone;
        dr12["Reason_for_Repudiation"] = fgdt.Reason_for_Repudiation;
        dr12["Type"] = Type;
        dr12["CreatedBy"] = UserId;
        dt12.Rows.Add(dr12);
        return dt12;
    }

    public DataTable IIBEnquiryOtherData(IIBEnquiryOtherData fgdt, DataTable dt12, string ApplicationNo, string UserId, string PANNo, string Type)
    {
        DataRow dr12 = null;
        dr12 = dt12.NewRow();
        dr12["PANCardNo"] = PANNo;
        dr12["Input_Proposal_Policy_No"] = ApplicationNo;
        dr12["QuestDBNo"] = fgdt.QuestDBNo;
        dr12["Input_Matching_Parameter"] = fgdt.Input_Matching_Parameter;
        dr12["Quest_DoP_DoC"] = fgdt.Quest_DoP_DoC;
        dr12["Quest_Sum_Assured"] = fgdt.Quest_Sum_Assured;
        dr12["Quest_Policy_Status"] = fgdt.Quest_Policy_Status;
        dr12["Quest_Date_of_Exit"] = fgdt.Quest_Date_of_Exit;
        dr12["Quest_Date_of_Death"] = fgdt.Quest_Date_of_Death;
        dr12["Quest_Cause_of_Death"] = fgdt.Quest_Cause_of_Death;
        dr12["Quest_Record_last_updated"] = fgdt.Quest_Record_last_updated;
        dr12["Quest_Entity_Caution_Status"] = fgdt.Quest_Entity_Caution_Status;
        dr12["Quest_Intermediary_Caution_Status"] = fgdt.Quest_Intermediary_Caution_Status;
        dr12["Quest_Company_Number"] = fgdt.Quest_Company_Number;
        dr12["Is_Negative_Match"] = fgdt.Is_Negative_Match;
        dr12["LAProposerPayor"] = fgdt.LAProposerPayor;
        dr12["Status"] = "Active";
        dr12["CreatedBy"] = UserId;
        dr12["IIBServiceResponse"] = "Success";
        dr12["Product_Type"] = fgdt.Product_TypeOutput;
        dr12["Linked_Non_linked"] = fgdt.Linked_Non_linked;
        dr12["Medical_Non_Medical"] = fgdt.Medical_NonMedical;
        dr12["Whether_Standard_Life"] = fgdt.Whether_Standard_Life;
        dr12["Reason_for_Decline"] = fgdt.Reason_for_Decline;
        dr12["Reason_for_Postpone"] = fgdt.Reason_for_Postpone;
        dr12["Reason_for_Repudiation"] = fgdt.Reason_for_Repudiation;
        dr12["Type"] = Type;
        dt12.Rows.Add(dr12);
        return dt12;
    }


    #endregion

    public void Clear()
    {
        txtApplicationNo.Text = "";
        txtFirstName.Text = "";
        txtSurName.Text = "";
        txtDBO.Text = "";
        txtEmail.Text = "";
        txtMobNo.Text = "";
        txtPAN.Text = "";
        txtPinCode.Text = "";
        txtproductCode.Text = "";
        txtAnnualIncome.Text = "";
        dllGender.SelectedIndex = 0;
        txtaddress.Text = "";
        txtPhoneNo.Text = "";
    }

   
}