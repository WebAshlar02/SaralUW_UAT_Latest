//******************************************************************************************************************************* 
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant      
// BRD/CR/Codesk No/Win : CR-4971  
// Date Of Creation     : 23-11-2022            
// Description          : Enabling NRI contact number in primary number field
//******************************************************************************************************************************* 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using UWSaralObjects;
using Platform.Utilities.LoggerFramework;
using System.Configuration;
public partial class UserControl_PopupManageProposar : System.Web.UI.UserControl
{
    #region variable declaration
    public string strClientInfo = string.Empty;
    public string strApplnNo = string.Empty;
    public string strChannelType = string.Empty;
    string strConsentRespons = string.Empty;
    string strspace = " ";
    DataSet _dsPrevPol = new DataSet();
    #endregion

    #region events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //load dropdown data         
            /*added by shri to use ajax on 08 june 17*/
            Ajax.Utility.RegisterTypeForAjax(typeof(UserControl_PopupManageProposar), Page);
            /*end here*/
            //fetch client basic info
            DataSet ds = new DataSet();
            if (Request.QueryString["qsAppNo"] != null)
            {
                ////strApplnNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
                strApplnNo = Request.QueryString["qsAppNo"];
            }
            if (Request.QueryString["qsChannelName"] != null)
            {
                strChannelType = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsChannelName"]);
            }
            FetchClientBasicDetails(ref ds, strApplnNo);

            //create dynamic table
            StringBuilder strBld = new StringBuilder();
            //strBld.Append("<Table class=\"table\">");

            strBld.Append("<Table class=\"table\" id=\"tblClientInfo\">"); // Added by Dinesh Kondabattini [MFL00456]  id=tblClientInfo
            strBld.Append("<thead>");
            strBld.Append("<tr>");
            //header
            strBld.Append("<th>Option</th>");
            strBld.Append("<th>Role</th>");
            strBld.Append("<th>Client Id</th>");
            strBld.Append("<th>Client Name</th>");
            strBld.Append("<th>Date of Birth</th>");
            strBld.Append("<th>Occupation</th>");
            strBld.Append("</tr>");
            strBld.Append("</thead>");
            strBld.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strBld.Append("<tr>");
                //radio
                strBld.Append("<td>");
                strBld.Append("<input type=\"radio\" class=\"rdoRole\" onchange=\"fnFillClientDetails();\" name=\"Role\" value=\"" + ds.Tables[0].Rows[i]["Role"] + "\"/>");
                strBld.Append("</td>");
                //role
                strBld.Append("<td>");
                strBld.Append("<span class=\"spanRole\">" + ds.Tables[0].Rows[i]["Role"] + "</span>");
                strBld.Append("</td>");
                //client id
                strBld.Append("<td>");
                strBld.Append("<span class=\"spanClientId\">" + ds.Tables[0].Rows[i]["ClientId"] + "</span>");
                strBld.Append("</td>");
                //client full name
                strBld.Append("<td>");
                strBld.Append("<span class=\"spanClientName\">" + ds.Tables[0].Rows[i]["ClientFullName"] + "</span>");
                strBld.Append("</td>");
                //date of birth
                strBld.Append("<td>");
                strBld.Append("<span class=\"spanClientName\">" + ds.Tables[0].Rows[i]["DOB"] + "</span>");
                strBld.Append("</td>");
                //client occupation
                strBld.Append("<td>");
                strBld.Append("<span class=\"spanClientName\">" + ds.Tables[0].Rows[i]["Occupation"] + "</span>");
                strBld.Append("</td>");
                strBld.Append("</tr>");
            }
            strBld.Append("</tbody>");
            strBld.Append("</table>");
            strClientInfo = Convert.ToString(strBld);
        }
        catch (Exception ex)
        {
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :_Load");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "_Load", "E-Error", "", "", ex.ToString());
        }
    }
    #endregion

    #region ajax method
    //fetch client full details
    [Ajax.AjaxMethod]
    public string FetchDetails(string strClientId, string strApplnNo, string strType)
    {
        string strRet = string.Empty;
        try
        {
            BussLayer objBusinessLayer = new BussLayer();
            DataSet _ds = new DataSet();
            objBusinessLayer.ClientFullInfo_GET(ref _ds, strClientId, strApplnNo, strType);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                strRet = FillClientObject(_ds);
            }
            else
            {
                strRet = "1#no record found";
            }
        }
        catch (Exception ex)
        {
            strRet = "1#try again later";
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :FetchDetails");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "FetchDetails", "E-Error", "", "", ex.ToString());
        }
        return strRet;
    }

    //premium calculation
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string PremiumCalc(string strApplicationNo, string strDateOfBirth, bool isMale, bool isSmoker, string strOldValue)
    {
        string strRet = string.Empty;
        try
        {
            //variable declaration
            UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
            ChangeValue objChangeObj = new ChangeValue();
            DataSet _ds = new DataSet();
            string strIdentifier = "PREMIUMCAL";
            string strLApushStatus = string.Empty;
            int strLApushErrorCode = -1;
            CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            string strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplnNo, strUserId, DateTime.Now, "CLIENT_PREM", ref intTrackingRet);
            /*end here*/
            //test
            if (objCommonObj == null)
            {
                objCommonObj = new CommonObject();
                objCommonObj._ChannelType = "OFFLINE";
            }
            ClientDetails objOldValue = new ClientDetails();

            //fill obj
            ClientDetails objClientDetails = new ClientDetails();
            objClientDetails.ClientDob = Convert.ToDateTime(strDateOfBirth);
            objClientDetails.ClientGender = (isMale) ? 'M' : 'F';
            objClientDetails.IsSmoker = isSmoker;
            objChangeObj.ClientDetails = objClientDetails;

            //ProdDtls objprodchangevalue = new ProdDtls();
            //objChangeObj.Prod_policydetails = objprodchangevalue;
            objOldValue = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<ClientDetails>(strOldValue);
            if (objClientDetails.ClientGender != objOldValue.ClientGender || objClientDetails.ClientDob.ToShortDateString().Equals(objOldValue.ClientDob.ToShortDateString()) == false || objClientDetails.IsSmoker != objOldValue.IsSmoker)
            {
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, strIdentifier, ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                PremiumCalcDtls objPremiumCalcDtls = new PremiumCalcDtls();
                if (strLApushErrorCode == 0)
                {
                    List<RiderInfo> lstRiderInfo = new List<RiderInfo>();
                    /*added by shri on 26 july 17 to show data in grid*/
                    //if (_ds != null && _ds.Tables[0].Rows.Count > 0)
                    //{
                    //    objPremiumCalcDtls.InstalmentPremiumAmt = Convert.ToDouble(_ds.Tables[0].Rows[0]["InstalmentPremiumAmt"]);
                    //    objPremiumCalcDtls.MedicalLoadingPremium = (string.IsNullOrEmpty(_ds.Tables[0].Rows[0]["MedicalLoadingPremium"].ToString())) ? 0.00 : Convert.ToDouble(_ds.Tables[0].Rows[0]["MedicalLoadingProgram"]);
                    //    objPremiumCalcDtls.NonMedicalLoadingPremium = (string.IsNullOrEmpty(_ds.Tables[0].Rows[0]["NonMedicalLoadingPremium"].ToString())) ? 0.00 : Convert.ToDouble(_ds.Tables[0].Rows[0]["NonMedicalLoadingProgram"]);
                    //    objPremiumCalcDtls.SumAssured = Convert.ToDouble(_ds.Tables[0].Rows[0]["SumAssured"]);
                    //    objPremiumCalcDtls.TotalInstalmentPremium = Convert.ToDouble(_ds.Tables[0].Rows[0]["TotalInstalmentPremium"]);
                    //    objPremiumCalcDtls.TotalPremiumAmt = Convert.ToDouble(_ds.Tables[0].Rows[0]["TotalPremiumAmount"]);
                    //}
                    Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstRiderInfo, _ds.Tables[0]);
                    if (lstRiderInfo != null && lstRiderInfo.Count > 0)
                    {
                        System.Text.StringBuilder objStringBuilder = new StringBuilder();
                        //create table header
                        objStringBuilder.Append("<table class=\"table premiumcalcbox\">");
                        objStringBuilder.Append("<tr class=\"btn-primary\">");
                        objStringBuilder.Append("<th>ProductCode");
                        objStringBuilder.Append("</th>");
                        objStringBuilder.Append("<th>InstalmentPremiumAmt");
                        objStringBuilder.Append("</th>");
                        //objStringBuilder.Append("<th>MedicalLoadingPremium");
                        //objStringBuilder.Append("</th>");
                        //objStringBuilder.Append("<th>NonMedicalLoadingPremium");
                        //objStringBuilder.Append("</th>");
                        objStringBuilder.Append("<th>SumAssured");
                        objStringBuilder.Append("</th>");
                        objStringBuilder.Append("<th>ServiceTax");
                        objStringBuilder.Append("</th>");
                        objStringBuilder.Append("<th>TotalInstalmentPremium");
                        //objStringBuilder.Append("</th>");
                        //objStringBuilder.Append("<th>TotalPremiumAmount");
                        //objStringBuilder.Append("</th>");
                        //objStringBuilder.Append("<th>RiderInfo");
                        //objStringBuilder.Append("</th>");
                        //objStringBuilder.Append("<th>RiderType");
                        //objStringBuilder.Append("</th>");
                        objStringBuilder.Append("</tr>");
                        //create table body
                        for (int i = 0; i < lstRiderInfo.Count; i++)
                        {
                            //prod code
                            objStringBuilder.Append("<tr>");
                            objStringBuilder.Append("<td>");
                            objStringBuilder.Append(lstRiderInfo[i].ProductCode);
                            objStringBuilder.Append("</td>");




                            objStringBuilder.Append("<td>");
                            objStringBuilder.Append(lstRiderInfo[i].InstalmentPremiumAmt);
                            objStringBuilder.Append("</td>");



                            //objStringBuilder.Append("<td>");
                            //objStringBuilder.Append(lstRiderInfo[i].MedicalLoadingPremium);
                            //objStringBuilder.Append("</td>");



                            //objStringBuilder.Append("<td>");
                            //objStringBuilder.Append(lstRiderInfo[i].NonMedicalLoadingPremium);
                            //objStringBuilder.Append("</td>");



                            objStringBuilder.Append("<td>");
                            objStringBuilder.Append(lstRiderInfo[i].SumAssured);
                            objStringBuilder.Append("</td>");



                            objStringBuilder.Append("<td>");
                            objStringBuilder.Append(lstRiderInfo[i].ServiceTax);
                            objStringBuilder.Append("</td>");



                            objStringBuilder.Append("<td>");
                            objStringBuilder.Append(lstRiderInfo[i].TotalInstalmentPremium);
                            objStringBuilder.Append("</td>");



                            //objStringBuilder.Append("<td>");
                            //objStringBuilder.Append(lstRiderInfo[i].TotalPremiumAmount);
                            //objStringBuilder.Append("</td>");



                            //objStringBuilder.Append("<td>");
                            //objStringBuilder.Append(lstRiderInfo[i].RiderId);
                            //objStringBuilder.Append("</td>");

                            //objStringBuilder.Append("<td>");
                            //objStringBuilder.Append(lstRiderInfo[i].RiderType);
                            //objStringBuilder.Append("</td>");
                            objStringBuilder.Append("</tr>");
                        }

                        objStringBuilder.Append("</table>");
                        strRet = "0#" + objStringBuilder.ToString();
                    }
                    /*end here*/
                }
                else if (strLApushErrorCode == 1)
                {
                    strRet = Convert.ToString(strLApushErrorCode) + "#" + strLApushStatus;
                }
                else
                {
                    strRet = Convert.ToString(strLApushErrorCode) + "#" + strLApushStatus;
                }
            }
            else
            {
                strRet = "2#NoEffectOnClient";
            }
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
        }
        catch (Exception ex)
        {
            strRet = "-1#Try Again Later";
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :PremiumCalc");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "PremiumCalc", "E-Error", "", "", ex.ToString());
        }
        return strRet;
    }

    //update client information 
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string UpdateClientInfo(
                    //client basic info
                    string strClientId, string strRole, string strSalutation, string strFirstName, string strMiddleName, string strLastName, string strDateOfBirth, bool isMale
                    , bool isSmoker, string strApplicationNo, string strChannel
                    //client permanent address change
                    , string strPermanentAddr1, string strPermanentAddr2, string strPermanentAddr3, string strPermanentAddr4, string strPermanentAddr5, string strPermanentLandMark
                    , string strPermanentPinCode, string strPermanentCity, string strPermanentDistrict, string strPermanentState, string strPermanentCountryCode
                    , string strPermanentAddressRemark, string strPermanentPhone1, string strPermanentPhone2, string strPermanentTelNo, string strPermanentMobileNo
                    , string strPermanentEmailId, string strPermanentFaxNo
                    //client Communication address change
                    , string strCommuAddr1, string strCommuAddr2, string strCommuAddr3, string strCommuAddr4, string strCommuAddr5, string strCommuLandMark
                    , string strCommuPinCode, string strCommuCity, string strCommuDistrict, string strCommuState, string strCommuCountryCode
                    , string strCommuAddressRemark, string strCommuPhone1, string strCommuPhone2, string strCommuTelNo, string strCommuMobileNo
                    , string strCommuEmailId, string strCommuFaxNo, string strNationality, bool blnIsNsap, string strOccupation
        , string strOldValue,
                    // 1.1 Begin of changes CR-4971 Sushant Devkate -MFL00905
                    bool IsNRICustomer
        // 1.1 End of changes CR-4971 Sushant Devkate -MFL00905
        )
    {
        //initailize object
        CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
        ChangeValue objChangeValue = (ChangeValue)System.Web.HttpContext.Current.Session["objLoginObj"];
        ClientDetails objOldClientDetails = new ClientDetails();
        BussLayer objBusinessLayer = new BussLayer();
        ClientDetails objClientDetails = new ClientDetails();
        ChangeValue objChangeObj = new ChangeValue();
        string strLApushStatus = string.Empty;
        int strLApushErrorCode = -1;
        DataSet _ds = new DataSet();
        string strRet = string.Empty;
        bool blnCancelReceipt = false;
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            string strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplicationNo, strUserId, DateTime.Now, "CLIENT_UPDATE", ref intTrackingRet);
            /*end here*/
            //fill object         
            objOldClientDetails = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<ClientDetails>(strOldValue);
            FillClientObjectFromString(ref objClientDetails,
                                     //client basic info
                                     strClientId, strRole, strSalutation, strFirstName, strMiddleName, strLastName, strDateOfBirth, isMale
                                    , isSmoker
                                    //client permanent address change
                                    , strPermanentAddr1, strPermanentAddr2, strPermanentAddr3, strPermanentAddr4, strPermanentAddr5, strPermanentLandMark
                                    , strPermanentPinCode, strPermanentCity, strPermanentDistrict, strPermanentState, strPermanentCountryCode
                                    , strPermanentAddressRemark, strPermanentPhone1, strPermanentPhone2, strPermanentTelNo, strPermanentMobileNo
                                    , strPermanentEmailId, strPermanentFaxNo
                                    //client Communication address change
                                    , strCommuAddr1, strCommuAddr2, strCommuAddr3, strCommuAddr4, strCommuAddr5, strCommuLandMark
                                    , strCommuPinCode, strCommuCity, strCommuDistrict, strCommuState, strCommuCountryCode
                                    , strCommuAddressRemark, strCommuPhone1, strCommuPhone2, strCommuTelNo, strCommuMobileNo
                                    , strCommuEmailId, strCommuFaxNo, strNationality, blnIsNsap, strOccupation, strApplicationNo, objChangeValue, IsNRICustomer);  // IsNRICustomer 1.1 Begin of changes CR-4971 Sushant Devkate -MFL00905
            if (objClientDetails != null)
            {
                objClientDetails.ClientType = objOldClientDetails.ClientType;
                objClientDetails.AssuredType = objOldClientDetails.AssuredType;
                objChangeObj.ClientDetails = objClientDetails;
            }
            int intRetVal = -1;
            //VALIDATE WHETHER CLIENT EXISTS OR NOT
            objBusinessLayer.ValidateRedundantClientOnApplication(objClientDetails, objOldClientDetails.ClientId, ref intRetVal);
            if (intRetVal > 0)
            {
                ////client modification
                ClientModification(objCommonObj, objChangeObj, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                if (strLApushErrorCode == 0)
                {
                    //update database
                    objBusinessLayer.UpdateClientBasicInfo(objClientDetails, objOldClientDetails.ClientId, ref intRetVal);
                    strRet = strRet + " Client Updated Successfully In Life  Asia \r\n";
                    if (intRetVal > 0)
                    {
                        strRet = strRet + " Client Updated Successfully In Database " + strLApushStatus.ToUpper() + "\r\n";
                        string strNewClient = Commfun.GetXMLFromObject(objClientDetails);
                        string strOldClient = Commfun.GetXMLFromObject(objOldClientDetails);
                        (new UWSaralDecision.CommFun()).LogClientProfile_Push(strOldClient, strNewClient, (int)ClientAction.Update, objClientDetails.objBmpUserInfo._UserID
                                        , objClientDetails.objBmpUserInfo._UserName, objClientDetails.objBmpUserInfo._UserGroup, ref intRetVal);
                        //receipt cancelation
                        if (objCommonObj._ChannelType.ToUpper().Equals("ONLINE") && objClientDetails.ClientRole.ToUpper().Contains("LA")
                                        && objOldClientDetails.IsProposer.Contains("1") && (!objClientDetails.ClientId.Equals(objOldClientDetails.ClientId)))
                        {
                            //receipt cancelation
                            ReceiptCancelation(objCommonObj, objChangeValue, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                            if (strLApushErrorCode == 0)
                            {
                                strRet = strRet + "Receipt Canceled Successfully \r\n";
                                blnCancelReceipt = !blnCancelReceipt;
                            }
                            else
                            {
                                strRet = strRet + " " + strLApushStatus + "\r\n";
                            }
                        }
                        else if (objCommonObj._ChannelType.ToUpper().Equals("ONLINE") && objClientDetails.ClientRole.Contains("Nominee") && objOldClientDetails.IsProposer.Contains("0") && (!objClientDetails.ClientId.Equals(objOldClientDetails.ClientId)))
                        {
                            //receipt cancelation
                            ReceiptCancelation(objCommonObj, objChangeValue, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                            if (strLApushErrorCode == 0)
                            {
                                strRet = strRet + "Receipt Canceled Successfully \r\n";
                                blnCancelReceipt = !blnCancelReceipt;
                            }
                            else
                            {
                                strRet = strRet + " " + strLApushStatus + " \r\n";
                            }
                        }
                        else if (objCommonObj._ChannelType.ToUpper().Equals("OFFLINE") && objClientDetails.ClientRole.Contains("payer") && (!objClientDetails.ClientId.Equals(objOldClientDetails.ClientId)))
                        {
                            //receipt cancelation
                            ReceiptCancelation(objCommonObj, objChangeValue, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                            if (strLApushErrorCode == 0)
                            {
                                strRet = strRet + "Receipt Canceled Successfully \r\n";
                                blnCancelReceipt = !blnCancelReceipt;
                            }
                            else
                            {
                                strRet = strRet + " " + strLApushStatus + "\r\n";
                            }
                        }
                        //call premium calculation and contract modification
                        UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
                        //if (objClientDetails.ClientRole == "LA")
                        //{
                        string strIdentifier = "PREMIUMCAL";
                        ProdDtls objprodchangevalue = new ProdDtls();
                        FillProductDetails(strApplicationNo, objCommonObj._ChannelType, ref objprodchangevalue);
                        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, strIdentifier, ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);

                        List<RiderInfo> lstRiderInfo = new List<RiderInfo>();
                        Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstRiderInfo, _ds.Tables[0]);

                        RiderInfo objrider = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(objprodchangevalue._ProdcodeBase)).SingleOrDefault();
                        if (objrider != null)
                        {
                            objprodchangevalue._Basepremiumamount = objrider.InstalmentPremiumAmt.ToString();
                            objprodchangevalue._TotalPremiumamount = objrider.TotalPremiumAmount.ToString();
                            objprodchangevalue._ServiceTax = objrider.ServiceTax.ToString();
                            objprodchangevalue._Sumassured = objrider.SumAssured.ToString();
                        }
                        RiderInfo objComb = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(objprodchangevalue._ProdcodeCombo)).SingleOrDefault();
                        if (objComb != null)
                        {
                            objprodchangevalue._BasepremiumamountCombo = objComb.InstalmentPremiumAmt.ToString();
                            objprodchangevalue._TotalPremiumamountCombo = objComb.TotalPremiumAmount.ToString();
                            objprodchangevalue._ServiceTaxCombo = objComb.ServiceTax.ToString();
                            objprodchangevalue._SumassuredCombo = objComb.SumAssured.ToString();
                        }
                        objChangeObj.Prod_policydetails = objprodchangevalue;
                        //}
                        objChangeObj.userLoginDetails = objChangeValue.userLoginDetails;
                        //contract modification
                        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, strChannel, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                        if (strLApushErrorCode == 0)
                        {
                            strRet = strRet + " Contract Updated Successfully \r\n";
                        }
                        else
                        {
                            strRet = strRet + strspace + strLApushStatus + "\r\n";
                        }
                    }
                    else
                    {
                        strRet = strRet + "Client Not Updated In Database \r\n";
                    }
                }
                else
                {
                    strRet = strRet + "Client Not Update In Life Asia," + strspace + strLApushStatus + "\r\n";
                }
            }
            else
            {
                if (intRetVal == -1)
                {
                    strRet = "Client with same role already assigned to application no";
                }
                else if (intRetVal == 0)
                {
                    strRet = "try again later";
                }
            }
            if (blnCancelReceipt)
            {
                //receipt creation
            }
            /*
                        else if (objClientDetails.ClientRole.Contains("proposer") && objClientDetails.ClientRole.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length == 1)
                        {
                            //receipt cancelation
                            ReceiptCancelation(objCommonObj, objChangeObj, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                            if (strLApushErrorCode == 0)
                            {
                                strRet = strRet + "Receipt Canceled Successfully \r\n";
                                blnCancelReceipt = !blnCancelReceipt;
                            }
                            else
                            {
                                strRet = strRet + "Receipt Not Canceled \r\n";
                            }
                        }
                        */
            Commfun objCommFun = new Commfun();
            //objCommFun.MaintainApplicationLog(strApplnNo, "UpdateClientDtls", string.Empty, objChangeValue.userLoginDetails._UserID, "", ref intRetVal);            
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("UCE"))
            {
                strRet = ex.Message.Substring(4);
            }
            else
            {
                strRet = "try again later";
            }
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :UpdateClientInfo");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "UpdateClientInfo", "E-Error", "", "", ex.ToString());
        }
        return strRet;
    }

    //fetch dedupe basic search
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string FetchDedupSearchDetails(string strFirstName, string strLastName, string strDob, bool blnGender)
    {
        string strRet = string.Empty;
        string strUserId = string.Empty;
        DataSet _ds = new DataSet();
        int intTrackingRet = -1;
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplnNo, strUserId, DateTime.Now, "CLIENT_DEDUPE", ref intTrackingRet);
            /*end here*/
            BussLayer objBusinessLayer = new BussLayer();
            objBusinessLayer.DedupeSearch_GET(ref _ds, strFirstName, strLastName, (string.IsNullOrEmpty(strDob) ? string.Empty : Convert.ToDateTime(strDob).ToString("MM-dd-yyyy")), (blnGender) ? 'M' : 'F', string.Empty);
            strRet = FillDedupeDetails(_ds);
        }
        catch (Exception ex)
        {
            strRet = "-1#try again later";
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :FetchDedupSearchDetails");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "FetchDedupSearchDetails", "E-Error", "", "", ex.ToString());
        }
        /*added by shri on 28 dec 17 to update tracking*/
        UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
        /*END HERE*/
        return strRet;
    }

    //fetch deduped client details 
    [Ajax.AjaxMethod]
    public string FetchDedupDetails(string strClientId)
    {
        string strRet = string.Empty;
        try
        {
            BussLayer objBusinessLayer = new BussLayer();
            DataSet _ds = new DataSet();
            objBusinessLayer.DedupeDetails_GET(ref _ds, strClientId);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                strRet = FillClientObject(_ds);
            }
            else
            {
                strRet = "-1#no record found";
            }
        }
        catch (Exception ex)
        {
            strRet = "-1#try again later";
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :FetchDedupDetails");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "FetchDedupDetails", "E-Error", "", "", ex.ToString());
        }
        return strRet;
    }


    //create client 
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string CreateClientInfo(
                    //client basic info
                    string strClientId, string strRole, string strSalutation, string strFirstName, string strMiddleName, string strLastName, string strDateOfBirth, bool isMale
                    , bool isSmoker
                    //client permanent address change
                    , string strPermanentAddr1, string strPermanentAddr2, string strPermanentAddr3, string strPermanentAddr4, string strPermanentAddr5, string strPermanentLandMark
                    , string strPermanentPinCode, string strPermanentCity, string strPermanentDistrict, string strPermanentState, string strPermanentCountryCode
                    , string strPermanentAddressRemark, string strPermanentPhone1, string strPermanentPhone2, string strPermanentTelNo, string strPermanentMobileNo
                    , string strPermanentEmailId, string strPermanentFaxNo
                    //client Communication address change
                    , string strCommuAddr1, string strCommuAddr2, string strCommuAddr3, string strCommuAddr4, string strCommuAddr5, string strCommuLandMark
                    , string strCommuPinCode, string strCommuCity, string strCommuDistrict, string strCommuState, string strCommuCountryCode
                    , string strCommuAddressRemark, string strCommuPhone1, string strCommuPhone2, string strCommuTelNo, string strCommuMobileNo
                    , string strCommuEmailId, string strCommuFaxNo, string strNationality, bool blnIsNsap, string strOccupation, string strApplicationNo, string strChannel
                    , string strOldValue,
                    // 1.1 Begin of changes CR-4971 Sushant Devkate -MFL00905
                    bool IsNRICustomer
               // 1.1 End of changes CR-4971 Sushant Devkate -MFL00905
               )
    {
        //initailize object
        CommonObject objCommonObj = (CommonObject)System.Web.HttpContext.Current.Session["objCommonObj"];
        ChangeValue objChangeValue = (ChangeValue)System.Web.HttpContext.Current.Session["objLoginObj"];
        ClientDetails objOldClientDetails = new ClientDetails();
        BussLayer objBusinessLayer = new BussLayer();
        ClientDetails objClientDetails = new ClientDetails();
        ChangeValue objChangeObj = new ChangeValue();
        string strLApushStatus = string.Empty;
        int strLApushErrorCode = -1;
        DataSet _ds = new DataSet();
        string strRet = string.Empty;
        bool blnCancelReceipt = false;
        try
        {
            /*added by shri on 28 dec 17 to add tracking*/
            int intTrackingRet = -1;
            string strUserId = objCommonObj._Bpmuserdetails._UserID;
            InsertUwDecisionTracking(strApplnNo, strUserId, DateTime.Now, "CLIENT_CREATE", ref intTrackingRet);
            /*end here*/
            //fill object         
            objOldClientDetails = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<ClientDetails>(strOldValue);
            FillClientObjectFromString(ref objClientDetails,
                                     //client basic info
                                     strClientId, strRole, strSalutation, strFirstName, strMiddleName, strLastName, strDateOfBirth, isMale
                                    , isSmoker
                                    //client permanent address change
                                    , strPermanentAddr1, strPermanentAddr2, strPermanentAddr3, strPermanentAddr4, strPermanentAddr5, strPermanentLandMark
                                    , strPermanentPinCode, strPermanentCity, strPermanentDistrict, strPermanentState, strPermanentCountryCode
                                    , strPermanentAddressRemark, strPermanentPhone1, strPermanentPhone2, strPermanentTelNo, strPermanentMobileNo
                                    , strPermanentEmailId, strPermanentFaxNo
                                    //client Communication address change
                                    , strCommuAddr1, strCommuAddr2, strCommuAddr3, strCommuAddr4, strCommuAddr5, strCommuLandMark
                                    , strCommuPinCode, strCommuCity, strCommuDistrict, strCommuState, strCommuCountryCode
                                    , strCommuAddressRemark, strCommuPhone1, strCommuPhone2, strCommuTelNo, strCommuMobileNo
                                    , strCommuEmailId, strCommuFaxNo, strNationality, blnIsNsap, strOccupation, strApplicationNo, objChangeValue, IsNRICustomer);  // IsNRICustomer 1.1 Begin of changes CR-4971 Sushant Devkate -MFL00905
            if (objClientDetails != null)
            {
                objClientDetails.AssuredType = strRole;
                objClientDetails.ClientType = objOldClientDetails.ClientType;
                objChangeObj.ClientDetails = objClientDetails;
            }
            int intRetVal = -1;
            //client modification
            ClientCreation(objCommonObj, objChangeObj, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
            if (strLApushErrorCode == 0)
            {
                objChangeObj.ClientDetails.ClientId = strLApushStatus;
                //save it in our db
                (new Commfun()).ClientFullInfo_Save(objChangeObj.ClientDetails, objOldClientDetails.ClientId, ref strLApushErrorCode);
                string strNewClient = Commfun.GetXMLFromObject(objClientDetails);
                (new UWSaralDecision.CommFun()).LogClientProfile_Push(string.Empty, strNewClient, (int)ClientAction.Create, objClientDetails.objBmpUserInfo._UserID
                            , objClientDetails.objBmpUserInfo._UserName, objClientDetails.objBmpUserInfo._UserGroup, ref intRetVal);
                strRet = "0#" + strLApushStatus + "#Client Added Successfully";
                //receipt cancelation
                if (objCommonObj._ChannelType.ToUpper().Equals("ONLINE") && objClientDetails.ClientRole.ToUpper().Contains("LA") && objOldClientDetails.IsProposer.Contains("1") && (!objClientDetails.ClientId.Equals(objOldClientDetails.ClientId)))
                {
                    //receipt cancelation
                    ReceiptCancelation(objCommonObj, objChangeValue, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                    if (strLApushErrorCode == 0)
                    {
                        strRet = strRet + "Receipt Canceled Successfully \r\n";
                        blnCancelReceipt = !blnCancelReceipt;
                    }
                    else
                    {
                        strRet = strRet + " " + strLApushStatus + "\r\n";
                    }
                }
                else if (objCommonObj._ChannelType.ToUpper().Equals("ONLINE") && objClientDetails.ClientRole.Contains("Nominee") && objOldClientDetails.IsProposer.Contains("0") && (!objClientDetails.ClientId.Equals(objOldClientDetails.ClientId)))
                {
                    //receipt cancelation
                    ReceiptCancelation(objCommonObj, objChangeValue, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                    if (strLApushErrorCode == 0)
                    {
                        strRet = strRet + "Receipt Canceled Successfully \r\n";
                        blnCancelReceipt = !blnCancelReceipt;
                    }
                    else
                    {
                        strRet = strRet + " " + strLApushStatus + " \r\n";
                    }
                }
                else if (objCommonObj._ChannelType.ToUpper().Equals("OFFLINE") && objClientDetails.ClientRole.Contains("payer") && (!objClientDetails.ClientId.Equals(objOldClientDetails.ClientId)))
                {
                    //receipt cancelation
                    ReceiptCancelation(objCommonObj, objChangeValue, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                    if (strLApushErrorCode == 0)
                    {
                        strRet = strRet + "Receipt Canceled Successfully \r\n";
                        blnCancelReceipt = !blnCancelReceipt;
                    }
                    else
                    {
                        strRet = strRet + " " + strLApushStatus + "\r\n";
                    }
                }

                //call premium calculation and contract modification
                UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
                //if (objClientDetails.ClientRole == "LA")
                //{
                string strIdentifier = "PREMIUMCAL";
                ProdDtls objprodchangevalue = new ProdDtls();
                FillProductDetails(strApplicationNo, objCommonObj._ChannelType, ref objprodchangevalue);
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, strIdentifier, ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);

                List<RiderInfo> lstRiderInfo = new List<RiderInfo>();
                Commfun.FillRiderInfoFromPremiumCalcDataTable(ref lstRiderInfo, _ds.Tables[0]);

                RiderInfo objrider = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(objprodchangevalue._ProdcodeBase)).SingleOrDefault();
                if (objrider != null)
                {
                    objprodchangevalue._Basepremiumamount = objrider.InstalmentPremiumAmt.ToString();
                    objprodchangevalue._TotalPremiumamount = objrider.TotalPremiumAmount.ToString();
                    objprodchangevalue._ServiceTax = objrider.ServiceTax.ToString();
                    objprodchangevalue._Sumassured = objrider.SumAssured.ToString();
                }
                RiderInfo objComb = lstRiderInfo.Where(x => x.RiderType.Equals("BS") && x.ProductCode.Equals(objprodchangevalue._ProdcodeCombo)).SingleOrDefault();
                if (objComb != null)
                {
                    objprodchangevalue._BasepremiumamountCombo = objComb.InstalmentPremiumAmt.ToString();
                    objprodchangevalue._TotalPremiumamountCombo = objComb.TotalPremiumAmount.ToString();
                    objprodchangevalue._ServiceTaxCombo = objComb.ServiceTax.ToString();
                    objprodchangevalue._SumassuredCombo = objComb.SumAssured.ToString();
                }
                objChangeObj.Prod_policydetails = objprodchangevalue;
                //}
                objChangeObj.userLoginDetails = objChangeValue.userLoginDetails;
                //contract modification
                strChannelType = Convert.ToString(ViewState["channel"]);
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationNo, strChannel, objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                if (strLApushErrorCode == 0)
                {
                    strRet = strRet + " Contract Updated Successfully \r\n";
                }
                else
                {
                    strRet = strRet + strspace + strLApushStatus + "\r\n";
                }
                /*
                else if (objClientDetails.ClientRole.Contains("proposer") && objClientDetails.ClientRole.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length == 1)
                {
                    //receipt cancelation
                    ReceiptCancelation(objCommonObj, objChangeValue, ref _ds, ref strLApushErrorCode, ref strLApushStatus);
                    if (strLApushErrorCode == 0)
                    {
                        strRet = strRet + "Receipt Canceled Successfully \r\n";
                        blnCancelReceipt = !blnCancelReceipt;
                    }
                    else
                    {
                        strRet = strRet + "Receipt Not Canceled \r\n";
                    }
                }
                */
                //contract modification
                //ContractModification(objCommonObj, objChangeObj, ref _ds, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus);
                //if (strLApushErrorCode == 0)
                //{
                //    strRet = strRet + "Contract Updated Successfully \r\n";
                //}
                //else
                //{
                //    strRet = strRet + " Contract Not Update \r\n";
                //}
            }
            else if (strLApushErrorCode > 0)
            {
                strRet = "1#" + strLApushStatus;
            }
            /*added by shri on 28 dec 17 to update tracking*/
            UpdateUwDecisionTracking(intTrackingRet, DateTime.Now, ref intTrackingRet);
            /*END HERE*/
            //objCommFun.MaintainApplicationLog(strApplnNo, "CreateClientDtls", string.Empty,       
            // objChangeValue.userLoginDetails._UserID, "", ref intRetVal);
        }
        catch (Exception ex)
        {
            strRet = "-1#try again later " + ex.Message;
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :CreateClientInfo");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "CreateClientInfo", "E-Error", "", "", ex.ToString());
        }
        return strRet;
    }

    //fetch country city state based on pincode
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
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :FillNestedDetails");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "FillNestedDetails", "E-Error", "", "", ex.ToString());
        }
        return strRet;
    }

    //fill drop down details
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string FetchCommonData()
    {
        string strRet = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            (new Commfun()).FetchCommonDropDownList(ref ds);
            strRet = FillCommonDropDownToList(ds);
        }
        catch (Exception ex)
        {
            strRet = string.Empty;
            UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
            Logger.Error(strApplnNo + ":UserControl :UserControl_PopupManageProposar // MethodeName :FetchCommonData");
            objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "FetchCommonData", "E-Error", "", "", ex.ToString());
        }
        return strRet;
    }
    #endregion

    #region private method
    //fetch client basic details
    private void FetchClientBasicDetails(ref DataSet _ds, string strApplnNo)
    {
        try
        {
            BussLayer objBussLayer = new BussLayer();
            objBussLayer.ClientBasicInfo_GET(ref _ds, strApplnNo);
        }
        catch (Exception ex)
        {
            _ds = null;
        }

    }

    //file client object from dataset and returns JSON string
    private string FillClientObject(DataSet ds)
    {
        UWSaralObjects.ClientDetails objClientDetails = new ClientDetails();
        List<UWSaralObjects.ClientAddress> lstClientAddress = new List<ClientAddress>();
        string strRet = string.Empty;
        try
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ClientAddress objClientAddress = new ClientAddress();
                if (i == 0)
                {
                    //fill client info 
                    objClientDetails.ClientId = Convert.ToString(ds.Tables[0].Rows[0]["CLIENTID"]);
                    objClientDetails.Salutation = Convert.ToString(ds.Tables[0].Rows[0]["SALUTATION"]);
                    objClientDetails.ClientFirstName = Convert.ToString(ds.Tables[0].Rows[0]["FIRST_NAME"]);
                    objClientDetails.ClientFatherName = Convert.ToString(ds.Tables[0].Rows[0]["MIDDLE_NAME"]);
                    objClientDetails.ClinetLastName = Convert.ToString(ds.Tables[0].Rows[0]["LAST_NAME"]);
                    objClientDetails.ClientDob = string.IsNullOrEmpty(Convert.ToString(ds.Tables[0].Rows[0]["CLIENT_DOB"])) ? objClientDetails.ClientDob
                                    : Convert.ToDateTime(ds.Tables[0].Rows[0]["CLIENT_DOB"]);
                    objClientDetails.ClientGender = Convert.ToChar(ds.Tables[0].Rows[0]["CLIENT_GENDER"]);
                    objClientDetails.IsSmoker = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_SMOKER"]);
                    objClientDetails.ClientType = Convert.ToString(ds.Tables[0].Rows[0]["CLIENT_TYPE"]);
                    objClientDetails.IsNSAP = Convert.ToBoolean(ds.Tables[0].Rows[0]["IS_NSAP"]);
                    objClientDetails.Occupation = Convert.ToString(ds.Tables[0].Rows[0]["OCCUPATION"]);
                    objClientDetails.Nationality = Convert.ToString(ds.Tables[0].Rows[0]["NATLTY"]);
                    objClientDetails.IsProposer = Convert.ToString(ds.Tables[0].Rows[0]["IS_PROPOSER"]);
                    objClientDetails.AssuredType = Convert.ToString(ds.Tables[0].Rows[0]["ASSURED_TYPE"]);
                    // 1.1 Begin of changes CR-4971 Sushant Devkate -MFL00905
                    objClientDetails.IsNRICustomer = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsNRICustomer"]);
                    // 1.1 End of changes CR-4971 Sushant Devkate -MFL00905

                }
                //fill client residental info
                objClientAddress.AddressLine1 = Convert.ToString(ds.Tables[0].Rows[i]["ADDR1"]);
                objClientAddress.AddressLine2 = Convert.ToString(ds.Tables[0].Rows[i]["ADDR2"]);
                objClientAddress.AddressLine3 = Convert.ToString(ds.Tables[0].Rows[i]["ADDR3"]);
                objClientAddress.AddressLine4 = Convert.ToString(ds.Tables[0].Rows[i]["ADDR4"]);
                objClientAddress.AddressLine5 = Convert.ToString(ds.Tables[0].Rows[i]["ADDR5"]);
                objClientAddress.LandMark = Convert.ToString(ds.Tables[0].Rows[i]["LANDMARK"]);
                objClientAddress.PinCode = Convert.ToInt32(ds.Tables[0].Rows[i]["PINCODE"]);
                objClientAddress.City = Convert.ToString(ds.Tables[0].Rows[i]["CITY"]);
                objClientAddress.District = Convert.ToString(ds.Tables[0].Rows[i]["DISTRICT"]);
                objClientAddress.State = Convert.ToString(ds.Tables[0].Rows[i]["STATE"]);
                objClientAddress.CountryCode = Convert.ToString(ds.Tables[0].Rows[i]["COUNTRY_CODE"]);
                objClientAddress.AddressRemark = Convert.ToString(ds.Tables[0].Rows[i]["ADDRESS_REMARK"]);
                objClientAddress.Phone1 = Convert.ToString(ds.Tables[0].Rows[i]["PHONE1"]);
                objClientAddress.Phone2 = Convert.ToString(ds.Tables[0].Rows[i]["PHONE2"]);
                objClientAddress.TelNo = Convert.ToString(ds.Tables[0].Rows[i]["TEL_NO"]);
                objClientAddress.MobileNo = Convert.ToString(ds.Tables[0].Rows[i]["MOBILE_NO"]);
                objClientAddress.EmailId = Convert.ToString(ds.Tables[0].Rows[i]["EMAIL_ID"]);
                objClientAddress.FaxNo = Convert.ToString(ds.Tables[0].Rows[i]["FAX"]);
                objClientAddress.AddressType = Convert.ToString(ds.Tables[0].Rows[i]["ADDRTYPE"]);
                lstClientAddress.Add(objClientAddress);
            }
            objClientDetails.lstClientAddress = lstClientAddress;
            string strOldValue = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(objClientDetails);
            strRet = "0#" + strOldValue;
        }
        catch (Exception ex)
        {

            strRet = "1#Try Again Later";
        }
        return strRet;
    }


    //fill client object from string
    private void FillClientObjectFromString(ref ClientDetails objClientDetails,
                    //client basic info
                    string strClientId, string strRole, string strSalutation, string strFirstName, string strMiddleName, string strLastName, string strDateOfBirth, bool isMale
                    , bool isSmoker
                    //client permanent address change
                    , string strPermanentAddr1, string strPermanentAddr2, string strPermanentAddr3, string strPermanentAddr4, string strPermanentAddr5, string strPermanentLandMark
                    , string strPermanentPinCode, string strPermanentCity, string strPermanentDistrict, string strPermanentState, string strPermanentCountryCode
                    , string strPermanentAddressRemark, string strPermanentPhone1, string strPermanentPhone2, string strPermanentTelNo, string strPermanentMobileNo
                    , string strPermanentEmailId, string strPermanentFaxNo
                    //client Communication address change
                    , string strCommuAddr1, string strCommuAddr2, string strCommuAddr3, string strCommuAddr4, string strCommuAddr5, string strCommuLandMark
                    , string strCommuPinCode, string strCommuCity, string strCommuDistrict, string strCommuState, string strCommuCountryCode
                    , string strCommuAddressRemark, string strCommuPhone1, string strCommuPhone2, string strCommuTelNo, string strCommuMobileNo
                    , string strCommuEmailId, string strCommuFaxNo, string strNationality, bool isNasp, string strOccupation, string strApplicationNo, ChangeValue objCommonObj, bool IsNRICustomer)  // IsNRICustomer 1.1 Begin of changes CR-4971 Sushant Devkate -MFL00905
    {
        List<ClientAddress> lstClientAddress = new List<ClientAddress>();
        //fill client communication address
        ClientAddress objCommunClientAddress = new ClientAddress();
        objCommunClientAddress.AddressLine1 = strCommuAddr1;
        objCommunClientAddress.AddressLine2 = strCommuAddr2;
        objCommunClientAddress.AddressLine3 = strCommuAddr3;
        objCommunClientAddress.AddressLine4 = strCommuAddr4;
        objCommunClientAddress.AddressLine5 = strCommuAddr5;
        objCommunClientAddress.LandMark = strCommuLandMark;
        objCommunClientAddress.PinCode = string.IsNullOrEmpty(strCommuPinCode) ? -1 : Convert.ToInt32(strCommuPinCode);
        objCommunClientAddress.City = strCommuCity;
        objCommunClientAddress.District = strCommuDistrict;
        objCommunClientAddress.State = strCommuState;
        objCommunClientAddress.CountryCode = strCommuCountryCode;
        objCommunClientAddress.AddressRemark = strCommuAddressRemark;
        objCommunClientAddress.Phone1 = strCommuPhone1;
        objCommunClientAddress.Phone2 = strCommuPhone2;
        objCommunClientAddress.TelNo = strCommuTelNo;
        objCommunClientAddress.MobileNo = strCommuMobileNo;
        objCommunClientAddress.EmailId = strCommuEmailId;
        objCommunClientAddress.FaxNo = strCommuFaxNo;
        objCommunClientAddress.AddressType = "R";
        lstClientAddress.Add(objCommunClientAddress);

        //fill client permanent address        
        if (!string.IsNullOrEmpty(strPermanentAddr1))
        {
            ClientAddress objPermanentClientAddress = new ClientAddress();
            objPermanentClientAddress.AddressLine1 = strPermanentAddr1;
            objPermanentClientAddress.AddressLine2 = strPermanentAddr2;
            objPermanentClientAddress.AddressLine3 = strPermanentAddr3;
            objPermanentClientAddress.AddressLine4 = strPermanentAddr4;
            objPermanentClientAddress.AddressLine5 = strPermanentAddr5;
            objPermanentClientAddress.LandMark = strPermanentLandMark;
            objPermanentClientAddress.PinCode = (string.IsNullOrEmpty(strPermanentPinCode)) ? -1 : Convert.ToInt32(strPermanentPinCode);
            objPermanentClientAddress.City = strPermanentCity;
            objPermanentClientAddress.District = strPermanentDistrict;
            objPermanentClientAddress.State = strPermanentState;
            objPermanentClientAddress.CountryCode = strPermanentCountryCode;
            objPermanentClientAddress.AddressRemark = strPermanentAddressRemark;
            objPermanentClientAddress.Phone1 = strPermanentPhone1;
            objPermanentClientAddress.Phone2 = strPermanentPhone2;
            objPermanentClientAddress.TelNo = strPermanentTelNo;
            objPermanentClientAddress.MobileNo = strPermanentMobileNo;
            objPermanentClientAddress.EmailId = strPermanentEmailId;
            objPermanentClientAddress.FaxNo = strPermanentFaxNo;
            objPermanentClientAddress.AddressType = "P";
            lstClientAddress.Add(objPermanentClientAddress);
        }

        //fill client object        
        objClientDetails.Salutation = strSalutation;
        objClientDetails.ClientId = strClientId;
        objClientDetails.ClientFirstName = strFirstName;
        objClientDetails.ClientFatherName = strMiddleName;
        objClientDetails.ClinetLastName = strLastName;
        objClientDetails.ClientDob = Convert.ToDateTime(strDateOfBirth);
        objClientDetails.ClientGender = (isMale) ? 'M' : 'F';
        objClientDetails.IsSmoker = isSmoker;
        objClientDetails.ApplicationNo = strApplicationNo;
        objClientDetails.ClientRole = strRole;
        objClientDetails.Nationality = strNationality;
        objClientDetails.IsNSAP = isNasp;
        objClientDetails.Occupation = strOccupation;

        // 1.1 Begin of changes CR-4971 Sushant Devkate -MFL00905
        objClientDetails.IsNRICustomer = IsNRICustomer;
        // 1.1 End of changes CR-4971 Sushant Devkate -MFL00905

        //fill bmp details
        objClientDetails.objBmpUserInfo = objCommonObj.userLoginDetails;

        //add list of client address
        objClientDetails.lstClientAddress = lstClientAddress;
    }

    //fill dedupe details 
    private string FillDedupeDetails(DataSet _ds)
    {
        string strRet = string.Empty;
        List<ClientDetails> lstClientDetails = new List<ClientDetails>();
        try
        {
            if (_ds != null && _ds.Tables.Count > 0)
            {
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    ClientDetails objClientDetails = new ClientDetails();
                    objClientDetails.ClientId = Convert.ToString(_ds.Tables[0].Rows[i]["gcn"]);
                    objClientDetails.ClientFirstName = Convert.ToString(_ds.Tables[0].Rows[i]["givenname"]);
                    objClientDetails.ClinetLastName = Convert.ToString(_ds.Tables[0].Rows[i]["surname"]);
                    objClientDetails.ClientGender = Convert.ToChar(_ds.Tables[0].Rows[i]["gender"]);
                    objClientDetails.ClientDob = Convert.ToDateTime(_ds.Tables[0].Rows[i]["BirthRegDate"]);
                    lstClientDetails.Add(objClientDetails);
                }
            }
            strRet = "0#" + (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(lstClientDetails);
        }
        catch (Exception exec)
        {
            strRet = "-1#try again later";
        }
        return strRet;
    }

    //call contract modification service
    private void ContractModification(CommonObject objCommonObj, ChangeValue objChangeObj, ref DataSet _ds, string strIdentificationFlag, ref int strLApushErrorCode
                        , ref string strLApushStatus)
    {
        UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(objCommonObj._ApplicationNo, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, strIdentificationFlag
                    , ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
    }

    //call client modification service
    private void ClientModification(CommonObject objCommonObj, ChangeValue objChangeObj, ref DataSet _ds, ref int strLApushErrorCode, ref string strLApushStatus)
    {
        UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(objCommonObj._ApplicationNo, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CLIENTUPDATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
    }

    //call receipt cancelation service
    private void ReceiptCancelation(CommonObject objCommonObj, ChangeValue objChangeValue, ref DataSet _ds, ref int strLApushErrorCode, ref string strLApushStatus)
    {
        UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(objCommonObj._ApplicationNo, objCommonObj._ChannelType, objChangeValue, ref _ds, ref _dsPrevPol, "RECEIPTCANCELATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
    }

    //call client creation service
    private void ClientCreation(CommonObject objCommonObj, ChangeValue objChangeObj, ref DataSet _ds, ref int strLApushErrorCode, ref string strLApushStatus)
    {
        UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(objCommonObj._ApplicationNo, objCommonObj._ChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "CLIENTCREATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
    }

    //fill country state city based on pincode
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

    //fill drop down details
    private string FillCommonDropDownToList(DataSet ds)
    {
        string strRet = string.Empty;
        List<List<KeyValuePair>> mainList = new List<List<KeyValuePair>>();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                List<KeyValuePair> lsstKeyValuePair = new List<KeyValuePair>();
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    KeyValuePair objKeyValuePair = new KeyValuePair();
                    objKeyValuePair.Key = Convert.ToString(ds.Tables[i].Rows[j][0]);
                    objKeyValuePair.Value = Convert.ToString(ds.Tables[i].Rows[j][1]);
                    lsstKeyValuePair.Add(objKeyValuePair);
                }
                mainList.Add(lsstKeyValuePair);
            }
        }
        strRet = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(mainList);
        return strRet;
    }


    //fill product details form database
    public void FillProductDetails(string strApplicationno, string ChannelType, ref ProdDtls ProdDtls)
    {
        Commfun objComm = new Commfun();
        DataSet _dsProdDtls = new DataSet();
        Logger.Info(strApplicationno + "STAG 3 :PageName :Uwdecision.aspx.CS // MethodeName :FillProductDetails" + System.Environment.NewLine);
        objComm.OnlineProductDisplayDetails_GET(ref _dsProdDtls, strApplicationno, ChannelType);
        ProdDtls = BindProductDetails(_dsProdDtls.Tables[0]);
        //BindRiderDetails(_dsProdDtls.Tables[1]);
    }

    public ProdDtls BindProductDetails(DataTable _dsProdDtls)
    {
        ProdDtls ojbProdDtls = new ProdDtls();
        string ProductName = string.Empty;
        if (_dsProdDtls.Rows.Count > 0)
        {
            double strServiceTax = Convert.ToDouble(ConfigurationManager.AppSettings["ServiceTax"]);
            double strBasePremium = Math.Round(Convert.ToInt32(_dsProdDtls.Rows[0]["BasePremium"].ToString()) * strServiceTax) + Convert.ToInt32(_dsProdDtls.Rows[0]["BasePremium"].ToString());

            //hdnProductType.Value = _dsProdDtls.Rows[0]["ProductType"].ToString();
            ojbProdDtls._ProdcodeBase = _dsProdDtls.Rows[0]["ProductCode"].ToString();
            //ojbProdDtls.nam= _dsProdDtls.Rows[0]["ProdcutName"].ToString();
            //ojbProdDtls.poli = _dsProdDtls.Rows[0]["PolicyNo"].ToString();
            ojbProdDtls._PolicyTerm = _dsProdDtls.Rows[0]["PolicyTerm"].ToString();
            ojbProdDtls._Premiumpayingterm = _dsProdDtls.Rows[0]["PremiumTerm"].ToString();
            ojbProdDtls._Sumassured = _dsProdDtls.Rows[0]["SumAssured"].ToString();
            ojbProdDtls._Paymentfrequency = _dsProdDtls.Rows[0]["PremiumFreq"].ToString();
            // txtSisamount.Text = _dsProdDtls.Rows[0]["AmountInSIS"].ToString();
            ojbProdDtls._Basepremiumamount = _dsProdDtls.Rows[0]["BasePremium"].ToString();
            ojbProdDtls._ServiceTax = _dsProdDtls.Rows[0]["ServiceTax"].ToString();
            ojbProdDtls._TotalPremiumamount = _dsProdDtls.Rows[0]["TotalPremium"].ToString();
            ojbProdDtls._MonthlyPayoutBase = _dsProdDtls.Rows[0]["MonthlyPayout"].ToString();

            if (_dsProdDtls.Rows.Count > 1)
            {
                ojbProdDtls._ProdcodeCombo = _dsProdDtls.Rows[1]["ProductCode"].ToString();
                //ojbProdDtls.pol = _dsProdDtls.Rows[1]["PolicyNo"].ToString();
                ojbProdDtls._MonthlyPayoutCombo = _dsProdDtls.Rows[1]["MonthlyPayout"].ToString();
                //ojbProdDtls.produ = _dsProdDtls.Rows[1]["ProdcutName"].ToString();
                ojbProdDtls._Paymentfrequency = _dsProdDtls.Rows[1]["PremiumFreq"].ToString();
                ojbProdDtls._ProdcodeCombo = _dsProdDtls.Rows[1]["ProductCode"].ToString();
                ojbProdDtls._PolicyTermCombo = _dsProdDtls.Rows[1]["PolicyTerm"].ToString();
                ojbProdDtls._PremiumpayingtermCombo = _dsProdDtls.Rows[1]["PremiumTerm"].ToString();
                ojbProdDtls._SumassuredCombo = _dsProdDtls.Rows[1]["SumAssured"].ToString();
                ojbProdDtls._ServiceTax = _dsProdDtls.Rows[1]["ServiceTax"].ToString();
                ojbProdDtls._TotalPremiumamountCombo = _dsProdDtls.Rows[1]["TotalPremium"].ToString();
                ojbProdDtls._BasepremiumamountCombo = _dsProdDtls.Rows[1]["BasePremium"].ToString();
            }
        }
        return ojbProdDtls;
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
    #endregion
}