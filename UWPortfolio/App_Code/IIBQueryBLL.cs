using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for IIBQueryBLL
/// </summary>
public class IIBQueryBLL
{

    #region 35.1 Begin of changes  CR-30489 kavita mfl00255

    public string GenerateToken(string strApplicationno,string ClientType)
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

            // Insert Logs
            // objPOlicyDetails.INSERTIIBRiskScoreLog(Type, "GenerateToken", AppNo, inputTokenJson, Convert.ToString(JResponce), IIBRiskScore, ErrorMessage, UserID, EventInvokedBy);

            if (ErrorCode == "0" && ErrorMessage.ToLower() == "success")
            {
                Token = Convert.ToString(JResponce["access_token"]);
            }
        }
        catch (System.Net.WebException ex)
        {
            new Commfun().SaveIIBLogDetails(ex.Message, "DataLayer.cs", "GenerateToken()", "Fail", inputTokenJson, TokenResponse, "", "IIBSchedular", "Schedular For DEQC", "1", ex.Message, "ApplicationNo", strApplicationno, ClientType);
        }
        return Token;
        //Generate Token -End
    }
    public DataSet GetIIBMatchAPIResponse(string IIBClientType, string EventName, string firstname, string surname, string dob, string gender,
    string pincode, string aadharcard, string PanNo, string Current_Address, string Permanent_Address, string Phone_Number_1,
    string Phone_Number_2, string Email_1, string ProductCode, string Annual_Income,string strApplicationno,string RequestedPage)//New fields Added by kavita in IIB service
    {
        DataSet dt1 = new DataSet();
        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();
        string strJsonRequest = "";
        string strJsonResponse = "";
        IIBRequest objIIBRequest = new IIBRequest();
        IIBResponse ObjIIBResponse = new IIBResponse();
        FGData objfgData = new FGData();
        OtherData objOtherData = new OtherData();

        string Token = GenerateToken(strApplicationno, IIBClientType);

        try
        {
            string DOB = Convert.ToString(Convert.ToDateTime(dob).ToString("MM/dd/yyyy"));
            objIIBRequest.DataKeyName = "ApplicationNo";
            objIIBRequest.DataKeyValue = strApplicationno;
            objIIBRequest.ClientID = "";
            objIIBRequest.FirstName = firstname.ToString();
            objIIBRequest.SurName = surname.ToString();
            objIIBRequest.DOB = DOB.ToString();
            objIIBRequest.PanNo = PanNo.ToString();
            objIIBRequest.PinCode = pincode.ToString();
            objIIBRequest.Current_Address = Current_Address.ToString();
            objIIBRequest.Permanent_Address = Permanent_Address.ToString();
            objIIBRequest.Gender = gender.ToString();
            objIIBRequest.Email_1 = Email_1.ToString();
            objIIBRequest.Phone_Number_1 = Phone_Number_1.ToString();
            objIIBRequest.Phone_Number_2 = Phone_Number_2.ToString();
            //New column added by kavita -IIB service changes
            objIIBRequest.ProductCode = ProductCode.ToString();
            objIIBRequest.Annual_Income = Annual_Income.ToString();
            strJsonRequest = objJavaScriptSerializer.Serialize(objIIBRequest);
            new Commfun().IIBLogtable(strApplicationno, "GetIIBMatchAPIResponse_"+ RequestedPage, strJsonRequest, "", "", "", "No Error", "Log before API Calling", IIBClientType, "", "IIB query button Clicked");
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

                //  strJsonResponse = GetResponseFromWebApi(System.Configuration.ConfigurationManager.AppSettings["SmartApiIIBMatch"], strJsonRequest);

                new Commfun().IIBLogtable(strApplicationno, "GetIIBMatchAPIResponse"+ RequestedPage, strJsonRequest, strJsonResponse, "", "", "", "Log After API Calling", IIBClientType, "", "IIB query button Clicked");

            }
            catch (System.Net.WebException ex)
            {
                new Commfun().IIBLogtable(strApplicationno, "GetIIBMatchAPIResponse_"+ RequestedPage, strJsonRequest, strJsonResponse, "", ex.Message, "", "Log After API Calling", IIBClientType, ex.StackTrace, "IIB query button Clicked");
            }

            DataTable dtImpactData = new DataTable();
            DataTable dtDataExceptImpact = new DataTable();
            //string path = "//10.6.41.161/Ctxuserdata/MFL00905/Desktop/Sushant/Projects/IIBResponse.json";
            //strJsonResponse = File.ReadAllText(path);

            ObjIIBResponse = objJavaScriptSerializer.Deserialize<IIBResponse>(strJsonResponse);

            new Commfun().InsertIIBLogDetails(objIIBRequest, ObjIIBResponse, IIBClientType, strJsonRequest, "");

            string IIBDecodingString = ObjIIBResponse.DecodedString1.ToString(); //added by kavita CR-30489

            if (ObjIIBResponse.ResponseCode == 0)
            {

                string FGData = JsonConvert.SerializeObject(ObjIIBResponse.FGData);
                string OtherData = JsonConvert.SerializeObject(ObjIIBResponse.OtherData);
                int CntFGdata = ObjIIBResponse.FGData.Count;
                int CntOtherdata = ObjIIBResponse.OtherData.Count;
                SetInitialRow(ref dtImpactData, "IIB");
                SetInitialRow_ExceptImpact(ref dtDataExceptImpact, "IIB");

                for (int i = 0; i < CntFGdata; i++)
                {
                    objfgData = ObjIIBResponse.FGData[i];
                    dtImpactData = IIBDataCreateFG(objfgData, dtImpactData, strApplicationno, IIBClientType, firstname);
                    dtDataExceptImpact = IIBDataCreateFG_ExceptImpact(objfgData, dtDataExceptImpact, strApplicationno, IIBClientType, firstname);
                }


                for (int j = 0; j < CntOtherdata; j++)
                {
                    objOtherData = ObjIIBResponse.OtherData[j];
                    dtImpactData = IIBDataCreateOther(objOtherData, dtImpactData, strApplicationno, IIBClientType);
                    dtDataExceptImpact = IIBDataCreateOther_ExceptImpact(objOtherData, dtDataExceptImpact, strApplicationno, IIBClientType);
                }

                if (dtImpactData.Rows.Count > 0)
                {
                    new Commfun().IIBQueryData_Impact(dtImpactData);
                }

                if (dtDataExceptImpact.Rows.Count > 0)
                {
                    new Commfun().IIBQueryData(dtDataExceptImpact);
                }


            

                new Commfun().SaveIIBLogDetails("No Eror", RequestedPage, "GetIIBMatchAPIResponse_" + RequestedPage, "Success", strJsonRequest, strJsonResponse, "", "UW Process", "UW Application", Convert.ToString(ObjIIBResponse.ResponseCode), ObjIIBResponse.ResponseMessage, "ApplicationNo", strApplicationno, IIBClientType);
                new Commfun().UpdatelogDetails(objIIBRequest.ClientID, objIIBRequest.DataKeyValue);
            }
            else
            {
                new Commfun().IIBLogtable(strApplicationno, "GetIIBMatchAPIResponse_" + RequestedPage, strJsonRequest, strJsonResponse, "", ObjIIBResponse.ResponseMessage, "", "Log After API Calling", IIBClientType, "", "IIB query button Clicked");
                new Commfun().SaveIIBLogDetails("No Eror", RequestedPage, "GetIIBMatchAPIResponse_" + RequestedPage, "Success", strJsonRequest, strJsonResponse, "", "UW Process", "UW Application", Convert.ToString(ObjIIBResponse.ResponseCode), ObjIIBResponse.ResponseMessage, "ApplicationNo", strApplicationno, IIBClientType);
            }

           
        }
        catch (Exception ex)
        {
            new Commfun().SaveIIBLogDetails(ex.StackTrace, RequestedPage, "GetIIBMatchAPIResponse_" + RequestedPage, "Fail", strJsonRequest, strJsonResponse, "", "UW Process", "UW Application", "1", ex.Message, "ApplicationNo", strApplicationno,IIBClientType);
          
        }

        return dt1;
    }

   

    #region Initial Column for IIB
    public DataTable SetInitialRow(ref DataTable dt, string Type)
    {
        switch (Type)
        {
            case "IIB":
                dt.Columns.Add("Input_Proposal_Policy_No");
                dt.Columns.Add("QuestDBNo");
                dt.Columns.Add("Input_Matching_Parameter");
                dt.Columns.Add("Quest_DoP_DoC");
                dt.Columns.Add("Quest_Sum_Assured");
                dt.Columns["Quest_Sum_Assured"].DataType = typeof(double);  //change for sum assured formation
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
                dt.Columns.Add("CreatedBy");
                dt.Columns.Add("IIBServiceResponse");
                dt.Columns.Add("Impact");

                //Added by kavita new column in IIB Service changes
                dt.Columns.Add("Product_Type");
                dt.Columns.Add("Linked_Non_linked");
                dt.Columns.Add("Medical_Non_Medical");
                dt.Columns.Add("Whether_Standard_Life");
                dt.Columns.Add("Reason_for_Decline");
                dt.Columns.Add("Reason_for_Postpone");
                dt.Columns.Add("Reason_for_Repudiation");
                //Added by sushant Devkate new column in IIB Service changes
                dt.Columns.Add("Client_Type");
                dt.Columns.Add("RolesType");
                dt.Columns.Add("Type");
                break;

            default:
                break;
        }

        return dt;

    }

    public DataTable SetInitialRow_ExceptImpact(ref DataTable dt, string Type)
    {
        switch (Type)
        {
            case "IIB":
                dt.Columns.Add("Input_Proposal_Policy_No");
                dt.Columns.Add("QuestDBNo");
                dt.Columns.Add("Input_Matching_Parameter");
                dt.Columns.Add("Quest_DoP_DoC");
                dt.Columns.Add("Quest_Sum_Assured");
                dt.Columns["Quest_Sum_Assured"].DataType = typeof(double);  //change for sum assured formation
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
                dt.Columns.Add("CreatedBy");
                dt.Columns.Add("IIBServiceResponse");

                //Added by kavita new column in IIB Service changes
                dt.Columns.Add("Product_Type");
                dt.Columns.Add("Linked_Non_linked");
                dt.Columns.Add("Medical_Non_Medical");
                dt.Columns.Add("Whether_Standard_Life");
                dt.Columns.Add("Reason_for_Decline");
                dt.Columns.Add("Reason_for_Postpone");
                dt.Columns.Add("Reason_for_Repudiation");
                //Added by sushant Devkate new column in IIB Service changes
                dt.Columns.Add("Client_Type");
                dt.Columns.Add("RolesType");
                dt.Columns.Add("Type");
                break;

            default:
                break;
        }

        return dt;

    }
    #endregion

    #region Assign the FG IIB Response 
    public DataTable IIBDataCreateFG(FGData fgdt, DataTable dt12, string ApplicationNo, string IIBClientType, string FirstName)
    {
        DataRow dr12 = null;

        dr12 = dt12.NewRow();

        dr12["Client_Type"] = IIBClientType;
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
        dr12["CreatedBy"] = "UW";
        // dr12["IIBServiceResponse"] = "Success";
        dr12["Impact"] = string.Empty;

        //Added by kavita new column in IIB Service changes
        dr12["Product_Type"] = fgdt.Product_TypeOutput;
        dr12["Linked_Non_linked"] = fgdt.Linked_Non_linked;
        dr12["Medical_Non_Medical"] = fgdt.Medical_NonMedical;
        dr12["Whether_Standard_Life"] = fgdt.Whether_Standard_Life;
        dr12["Reason_for_Decline"] = fgdt.Reason_for_Decline;
        dr12["Reason_for_Postpone"] = fgdt.Reason_for_Postpone;
        dr12["Reason_for_Repudiation"] = fgdt.Reason_for_Repudiation;
        //Added by Sushant 
        dr12["Client_Type"] = IIBClientType;
        dr12["RolesType"] = new Commfun().GetPreviousPolicyClientType(fgdt.QuestDBNo, FirstName.Trim());
        dr12["Type"] = "FG";
        dt12.Rows.Add(dr12);

        return dt12;
    }

    public DataTable IIBDataCreateFG_ExceptImpact(FGData fgdt, DataTable dt12, string ApplicationNo, string IIBClientType, string FirstName)
    {
        DataRow dr12 = null;

        dr12 = dt12.NewRow();
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
        dr12["CreatedBy"] = "UW";
        dr12["IIBServiceResponse"] = "Success";

        //Added by kavita new column in IIB Service changes
        dr12["Product_Type"] = fgdt.Product_TypeOutput;
        dr12["Linked_Non_linked"] = fgdt.Linked_Non_linked;
        dr12["Medical_Non_Medical"] = fgdt.Medical_NonMedical;
        dr12["Whether_Standard_Life"] = fgdt.Whether_Standard_Life;
        dr12["Reason_for_Decline"] = fgdt.Reason_for_Decline;
        dr12["Reason_for_Postpone"] = fgdt.Reason_for_Postpone;
        dr12["Reason_for_Repudiation"] = fgdt.Reason_for_Repudiation;
        //Added by Sushant 
        dr12["Client_Type"] = IIBClientType;
        dr12["RolesType"] = new Commfun().GetPreviousPolicyClientType(fgdt.QuestDBNo, FirstName.Trim());
        dr12["Type"] = "FG";
        dt12.Rows.Add(dr12);

        return dt12;
    }
    #endregion

    #region  Assign the Other FGIL IIB Response 

    public DataTable IIBDataCreateOther(OtherData fgdt, DataTable dt12, string ApplicationNo, string IIBClientType)
    {
        DataRow dr12 = null;

        dr12 = dt12.NewRow();
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
        dr12["CreatedBy"] = "UW";
        dr12["IIBServiceResponse"] = "Success";
        dr12["Impact"] = string.Empty;

        //Added by kavita new column in IIB Service changes
        dr12["Product_Type"] = fgdt.Product_TypeOutput;
        dr12["Linked_Non_linked"] = fgdt.Linked_Non_linked;
        dr12["Medical_Non_Medical"] = fgdt.Medical_NonMedical;
        dr12["Whether_Standard_Life"] = fgdt.Whether_Standard_Life;
        dr12["Reason_for_Decline"] = fgdt.Reason_for_Decline;
        dr12["Reason_for_Postpone"] = fgdt.Reason_for_Postpone;
        dr12["Reason_for_Repudiation"] = fgdt.Reason_for_Repudiation;
        //Added by sushant Devkate new column in IIB Service changes
        dr12["Client_Type"] = IIBClientType;
        dr12["RolesType"] = string.Empty;
        dr12["Type"] = "OTHER";
        dt12.Rows.Add(dr12);
        return dt12;
    }
    public DataTable IIBDataCreateOther_ExceptImpact(OtherData fgdt, DataTable dt12, string ApplicationNo,string IIBClientType)
    {
        DataRow dr12 = null;
        dr12 = dt12.NewRow();

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
        dr12["CreatedBy"] = "UW";
        dr12["IIBServiceResponse"] = "Success";

        //Added by kavita new column in IIB Service changes
        dr12["Product_Type"] = fgdt.Product_TypeOutput;
        dr12["Linked_Non_linked"] = fgdt.Linked_Non_linked;
        dr12["Medical_Non_Medical"] = fgdt.Medical_NonMedical;
        dr12["Whether_Standard_Life"] = fgdt.Whether_Standard_Life;
        dr12["Reason_for_Decline"] = fgdt.Reason_for_Decline;
        dr12["Reason_for_Postpone"] = fgdt.Reason_for_Postpone;
        dr12["Reason_for_Repudiation"] = fgdt.Reason_for_Repudiation;
        //Added by sushant Devkate new column in IIB Service changes
        dr12["Client_Type"] = IIBClientType;
        dr12["RolesType"] = string.Empty;
        dr12["Type"] = "OTHER";
        dt12.Rows.Add(dr12);
        return dt12;
    }
    #endregion

    public string GetResponseFromWebApi(string strURL, string strJsonRequest)
    {
        System.Net.WebClient client = new System.Net.WebClient();
        System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
        System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        client.Headers["Content-type"] = "application/json";
        client.Encoding = Encoding.UTF8;
        return client.UploadString(strURL, strJsonRequest);
    }

    #endregion 35.1 Begin of changes  CR-30489 kavita mfl00255
}