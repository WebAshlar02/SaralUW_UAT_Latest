//******************************************************************************************************************************* 
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sushant      
// BRD/CR/Codesk No/Win : CR-4971  
// Date Of Creation     : 15-11-2022            
// Description          : Enabling NRI contact number in primary number field
//******************************************************************************************************************************* 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using System.Configuration;

namespace UWSaralServices
{
    public class ClientUpdation
    {
        CommFun objcomm = new CommFun();
        public void ClientUpdationModPushService(string strPQuoteNo, DataSet _dsClient, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "*******Client Updation Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :ClientUpdation.CS // MethodeName :ClientUpdationModPushService" + System.Environment.NewLine);

                //LAClientUpdationService.ServiceClient objClientUpdation = new LAClientUpdationService.ServiceClient();
                //LAClientUpdationService.MasterPersonalClient objMasterPersonalClient = new LAClientUpdationService.MasterPersonalClient();
                //LAClientUpdationService.MasterCorporateClient objMasterCorporateClient = new LAClientUpdationService.MasterCorporateClient();

     
                UpdateClientSvc.ServiceClient objClientUpdation = new UpdateClientSvc.ServiceClient();
                UpdateClientSvc.MasterPersonalClient objMasterPersonalClient = new UpdateClientSvc.MasterPersonalClient();

                UpdateClientSvc.MasterCorporateClient objMasterCorporateClient = new UpdateClientSvc.MasterCorporateClient();

                //declare variable 
                string strClttwo = string.Empty, strAddrType = string.Empty, strBirthP = string.Empty, strCltAddr01 = string.Empty, strCltAddr02 = string.Empty
                       , strCltAddr03 = string.Empty, strCltAddr04 = string.Empty, strCltAddr05 = string.Empty, strCltDobx = string.Empty, strCltDodx = string.Empty
                       , strCltPCode = string.Empty, strCltPhone01 = string.Empty
                    , strCltPhone02 = string.Empty, strCltSex = string.Empty, strCltStat = string.Empty, strCtryCode = string.Empty, strDirMail = string.Empty, strDocNo = string.Empty, strLanguage = string.Empty, strLgivName = string.Empty, strLsurName = string.Empty, strMailing = string.Empty, strMarryd = string.Empty, strNatlty = string.Empty,
                    strNmfmt = string.Empty, strOccpCode = string.Empty, strSalutl = string.Empty, strSecutiryNo = string.Empty, strServBrh = string.Empty, strSoe = string.Empty, strSrDate = string.Empty, strStatCode = string.Empty, strTaxFlag = string.Empty, strVIP = string.Empty, strZoctInd = string.Empty, strFaxNo = string.Empty, strOldIdNo = string.Empty
                    , strRdidTelNo = string.Empty, strRinterNet = string.Empty, strRmblPhone = string.Empty, strRPager = string.Empty, strRstaFlag = string.Empty, strRtaxIdNum = string.Empty, strZpecInd = string.Empty, strDecgrSal = string.Empty, strEmprTaxr = string.Empty, strEvidDate = string.Empty, strIncDesc = string.Empty, strPayRollNo = string.Empty, strPensInd = string.Empty, strPrasInd = string.Empty
                    , strSalCurr = string.Empty, strTaxYr = string.Empty, strBranch = string.Empty, strUserRole = string.Empty, strUserId = string.Empty, strPartnerId = string.Empty, strProcessId = string.Empty, strApplicationNo = string.Empty
                    , strCapital = string.Empty, strCtryOrig = string.Empty, strEcact = string.Empty, strFao = string.Empty, strStaffNo = string.Empty, strTgrm = string.Empty, strTlxNo = string.Empty;

                if (objChangeObj != null && objChangeObj.ClientDetails.lstClientAddress.Count > 0)
                {
                    if (objChangeObj.ClientDetails.AssuredType.ToLower() == "nominee")
                    {
                        int intCount = 0;
                        if (objChangeObj.ClientDetails.lstClientAddress.Count > 1)
                        {
                            intCount = 1;
                        }
                        //get parameter value
                        strClttwo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClientId : Convert.ToString(_dsClient.Tables[0].Rows[0]["Clttwo"]);
                        strAddrType = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].AddressType : Convert.ToString(_dsClient.Tables[0].Rows[0]["AddrType"]);
                        strCltAddr01 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine1 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr01"]);
                        strCltAddr02 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine2 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr02"]);
                        strCltAddr03 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine3 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr03"]);
                        strCltAddr04 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].City : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr04"]);
                        strCltAddr05 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].State : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr05"]);
                        strCltDobx = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClientDob.ToString("dd-MM-yyyy") : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltDobx"]);
                        strCltDodx = "";// objChangeObj.ClientDetails.ClientDob.AddYears(10).ToString("dd/MM/yyyy");
                        strCltPCode = (objChangeObj.ClientDetails != null) ? Convert.ToString(objChangeObj.ClientDetails.lstClientAddress[intCount].PinCode) : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltPCode"]);
                        strCltPhone01 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].Phone1 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltPhone01"]);
                        strCltPhone02 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].Phone2 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltPhone02"]);
                        strCltSex = (objChangeObj.ClientDetails != null) ? Convert.ToString(objChangeObj.ClientDetails.ClientGender) : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltSex"]);
                        strCltStat = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].State : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltStat"]);
                        strCtryCode = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].CountryCode : Convert.ToString(_dsClient.Tables[0].Rows[0]["CtryCode"]);
                        strDirMail = Convert.ToString(_dsClient.Tables[0].Rows[0]["DirMail"]);
                        strDocNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["DocNo"]);
                        strLanguage = (0 == 0) ? "E" : Convert.ToString(_dsClient.Tables[0].Rows[0]["Language"]);
                        strLgivName = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClientFirstName : Convert.ToString(_dsClient.Tables[0].Rows[0]["LgivName"]);
                        strLsurName = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClinetLastName : Convert.ToString(_dsClient.Tables[0].Rows[0]["LsurName"]);
                        strMailing = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].EmailId : Convert.ToString(_dsClient.Tables[0].Rows[0]["Mailing"]);
                        strMarryd = (string.IsNullOrEmpty(Convert.ToString(_dsClient.Tables[0].Rows[0]["Marryd"]))) ? "M" : Convert.ToString(_dsClient.Tables[0].Rows[0]["Marryd"]);
                        strNatlty = Convert.ToString(_dsClient.Tables[0].Rows[0]["Natlty"]);
                        strNmfmt = Convert.ToString(_dsClient.Tables[0].Rows[0]["Nmfmt"]);
                        strOccpCode = Convert.ToString(_dsClient.Tables[0].Rows[0]["OccpCode"]);
                        strSalutl = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.Salutation : Convert.ToString(_dsClient.Tables[0].Rows[0]["Salutl"]);
                        strSecutiryNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["SecutiryNo"]);
                        strServBrh = (0 == 0) ? "10" : Convert.ToString(_dsClient.Tables[0].Rows[0]["ServBrh"]);
                        strSoe = Convert.ToString(_dsClient.Tables[0].Rows[0]["Soe"]);
                        strNmfmt = Convert.ToString(_dsClient.Tables[0].Rows[0]["SrDate"]);
                        strStatCode = Convert.ToString(_dsClient.Tables[0].Rows[0]["StatCode"]);
                        strTaxFlag = (0 == 0) ? "Y" : Convert.ToString(_dsClient.Tables[0].Rows[0]["TaxFlag"]);
                        strVIP = Convert.ToString(_dsClient.Tables[0].Rows[0]["VIP"]);
                        strZoctInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["ZoctInd"]);
                        strFaxNo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].FaxNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["FaxNo"]);
                        strOldIdNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["OldIdNo"]);
                        strRdidTelNo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].TelNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["RdidTelNo"]);
                        strRinterNet = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].EmailId : Convert.ToString(_dsClient.Tables[0].Rows[0]["RinterNet"]);
                        strRmblPhone = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[intCount].MobileNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["RmblPhone"]);
                        strRPager = Convert.ToString(_dsClient.Tables[0].Rows[0]["RPager"]);
                        strRstaFlag = Convert.ToString(_dsClient.Tables[0].Rows[0]["RstaFlag"]);
                        strRtaxIdNum = Convert.ToString(_dsClient.Tables[0].Rows[0]["RtaxIdNum"]);
                        strZpecInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["ZpecInd"]);
                        strDecgrSal = Convert.ToString(_dsClient.Tables[0].Rows[0]["DecgrSal"]);
                        strEmprTaxr = Convert.ToString(_dsClient.Tables[0].Rows[0]["EmprTaxr"]);
                        strEvidDate = Convert.ToString(_dsClient.Tables[0].Rows[0]["EvidDate"]);
                        strIncDesc = Convert.ToString(_dsClient.Tables[0].Rows[0]["IncDesc"]);
                        strPayRollNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["PayRollNo"]);
                        strPensInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["PensInd"]);
                        strPrasInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["PrasInd"]);
                        strSalCurr = Convert.ToString(_dsClient.Tables[0].Rows[0]["SalCurr"]);
                        strTaxYr = Convert.ToString(_dsClient.Tables[0].Rows[0]["TaxYr"]);
                        strBranch = (0 == 0) ? "pra" : Convert.ToString(_dsClient.Tables[0].Rows[0]["Branch"]);
                        strUserRole = (0 == 0) ? "10" : Convert.ToString(_dsClient.Tables[0].Rows[0]["UserRole"]);
                        strUserId = objChangeObj.ClientDetails.objBmpUserInfo._UserID;//(0 == 0) ? "rpandya" : Convert.ToString(_dsClient.Tables[0].Rows[0]["UserId"]);
                        strPartnerId = Convert.ToString(_dsClient.Tables[0].Rows[0]["PartnerId"]);
                        strProcessId = Convert.ToString(_dsClient.Tables[0].Rows[0]["ProcessId"]);
                        strCapital = (0 == 0) ? "0" : Convert.ToString(_dsClient.Tables[0].Rows[0]["Capital"]);
                        strCtryOrig = Convert.ToString(_dsClient.Tables[0].Rows[0]["CtryOrig"]);
                        strEcact = Convert.ToString(_dsClient.Tables[0].Rows[0]["Ecact"]);
                        strFao = Convert.ToString(_dsClient.Tables[0].Rows[0]["Fao"]);
                        strStaffNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["StaffNo"]);
                        strTgrm = Convert.ToString(_dsClient.Tables[0].Rows[0]["Tgrm"]);
                        strTlxNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["TlxNo"]);
                        strApplicationNo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ApplicationNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["ApplicationNo"]);
                    }
                    else
                    {

                        //get parameter value
                        strClttwo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClientId : Convert.ToString(_dsClient.Tables[0].Rows[0]["Clttwo"]);
                        strAddrType = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].AddressType : Convert.ToString(_dsClient.Tables[0].Rows[0]["AddrType"]);
                        strCltAddr01 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].AddressLine1 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr01"]);
                        strCltAddr02 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].AddressLine2 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr02"]);
                        strCltAddr03 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].AddressLine3 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr03"]);
                        strCltAddr04 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].City : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr04"]);
                        strCltAddr05 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].State : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltAddr05"]);
                        strCltDobx = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClientDob.ToString("dd-MM-yyyy") : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltDobx"]);
                        strCltDodx = "";// objChangeObj.ClientDetails.ClientDob.AddYears(10).ToString("dd/MM/yyyy");
                        strCltPCode = (objChangeObj.ClientDetails != null) ? Convert.ToString(objChangeObj.ClientDetails.lstClientAddress[0].PinCode) : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltPCode"]);
                        strCltPhone01 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].Phone1 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltPhone01"]);
                        strCltPhone02 = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].Phone2 : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltPhone02"]);
                        strCltSex = (objChangeObj.ClientDetails != null) ? Convert.ToString(objChangeObj.ClientDetails.ClientGender) : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltSex"]);
                        // 1.1 Begin of Changes CR-4971 Sushant Devkate - MFL00905
                        strCltStat = "10"; //(objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].State : Convert.ToString(_dsClient.Tables[0].Rows[0]["CltStat"]);
                         //1.1 End of Changes CR-4971 Sushant Devkate - MFL00905
                        strCtryCode = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].CountryCode : Convert.ToString(_dsClient.Tables[0].Rows[0]["CtryCode"]);
                        strDirMail = Convert.ToString(_dsClient.Tables[0].Rows[0]["DirMail"]);
                        strDocNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["DocNo"]);
                        strLanguage = (0 == 0) ? "E" : Convert.ToString(_dsClient.Tables[0].Rows[0]["Language"]);
                        strLgivName = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClientFirstName : Convert.ToString(_dsClient.Tables[0].Rows[0]["LgivName"]);
                        strLsurName = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ClinetLastName : Convert.ToString(_dsClient.Tables[0].Rows[0]["LsurName"]);
                        strMailing = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].EmailId : Convert.ToString(_dsClient.Tables[0].Rows[0]["Mailing"]);
                        strMarryd = (string.IsNullOrEmpty(Convert.ToString(_dsClient.Tables[0].Rows[0]["Marryd"]))) ? "M" : Convert.ToString(_dsClient.Tables[0].Rows[0]["Marryd"]);                       
                        strNatlty = Convert.ToString(_dsClient.Tables[0].Rows[0]["Natlty"]);
                        

                        strNmfmt = Convert.ToString(_dsClient.Tables[0].Rows[0]["Nmfmt"]);
                        strOccpCode = Convert.ToString(_dsClient.Tables[0].Rows[0]["OccpCode"]);
                        strSalutl = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.Salutation : Convert.ToString(_dsClient.Tables[0].Rows[0]["Salutl"]);
                        strSecutiryNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["SecutiryNo"]);
                        strServBrh = (0 == 0) ? "10" : Convert.ToString(_dsClient.Tables[0].Rows[0]["ServBrh"]);
                        strSoe = Convert.ToString(_dsClient.Tables[0].Rows[0]["Soe"]);
                        strNmfmt = Convert.ToString(_dsClient.Tables[0].Rows[0]["SrDate"]);
                        strStatCode = Convert.ToString(_dsClient.Tables[0].Rows[0]["StatCode"]);
                        strTaxFlag = (0 == 0) ? "Y" : Convert.ToString(_dsClient.Tables[0].Rows[0]["TaxFlag"]);
                        strVIP = Convert.ToString(_dsClient.Tables[0].Rows[0]["VIP"]);
                        strZoctInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["ZoctInd"]);
                        strFaxNo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].FaxNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["FaxNo"]);
                        strOldIdNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["OldIdNo"]);
                        strRdidTelNo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].TelNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["RdidTelNo"]);
                        strRinterNet = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].EmailId : Convert.ToString(_dsClient.Tables[0].Rows[0]["RinterNet"]);
                        strRmblPhone = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.lstClientAddress[0].MobileNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["RmblPhone"]);
                        strRPager = Convert.ToString(_dsClient.Tables[0].Rows[0]["RPager"]);
                        strRstaFlag = Convert.ToString(_dsClient.Tables[0].Rows[0]["RstaFlag"]);
                        strRtaxIdNum = Convert.ToString(_dsClient.Tables[0].Rows[0]["RtaxIdNum"]);
                        strZpecInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["ZpecInd"]);
                        strDecgrSal = Convert.ToString(_dsClient.Tables[0].Rows[0]["DecgrSal"]);
                        strEmprTaxr = Convert.ToString(_dsClient.Tables[0].Rows[0]["EmprTaxr"]);
                        strEvidDate = Convert.ToString(_dsClient.Tables[0].Rows[0]["EvidDate"]);
                        strIncDesc = Convert.ToString(_dsClient.Tables[0].Rows[0]["IncDesc"]);
                        strPayRollNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["PayRollNo"]);
                        strPensInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["PensInd"]);
                        strPrasInd = Convert.ToString(_dsClient.Tables[0].Rows[0]["PrasInd"]);
                        strSalCurr = Convert.ToString(_dsClient.Tables[0].Rows[0]["SalCurr"]);
                        strTaxYr = Convert.ToString(_dsClient.Tables[0].Rows[0]["TaxYr"]);
                        strBranch = (!string.IsNullOrEmpty(Convert.ToString(_dsClient.Tables[0].Rows[0]["Branch"]))) ? "pra" : Convert.ToString(_dsClient.Tables[0].Rows[0]["Branch"]);
                        //strUserRole = (0 == 0) ? "10" : Convert.ToString(_dsClient.Tables[0].Rows[0]["UserRole"]);
                        strUserId = objChangeObj.ClientDetails.objBmpUserInfo._UserID;
                        //(!string.IsNullOrEmpty(Convert.ToString(_dsClient.Tables[0].Rows[0]["UserId"]))) ? "rpandya" : Convert.ToString(_dsClient.Tables[0].Rows[0]["UserId"]);
                        strPartnerId = Convert.ToString(_dsClient.Tables[0].Rows[0]["PartnerId"]);
                        strProcessId = Convert.ToString(_dsClient.Tables[0].Rows[0]["ProcessId"]);
                        strCapital = "0";
                        strCtryOrig = Convert.ToString(_dsClient.Tables[0].Rows[0]["CtryOrig"]);
                        strEcact = Convert.ToString(_dsClient.Tables[0].Rows[0]["Ecact"]);
                        strFao = Convert.ToString(_dsClient.Tables[0].Rows[0]["Fao"]);
                        strStaffNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["StaffNo"]);
                        strTgrm = Convert.ToString(_dsClient.Tables[0].Rows[0]["Tgrm"]);
                        strTlxNo = Convert.ToString(_dsClient.Tables[0].Rows[0]["TlxNo"]);
                        strApplicationNo = (objChangeObj.ClientDetails != null) ? objChangeObj.ClientDetails.ApplicationNo : Convert.ToString(_dsClient.Tables[0].Rows[0]["ApplicationNo"]);
                    }

                    //call service 
                    //hardcoded value remove once testing is done
                    //strClttwo = "50041036";
                    //strCapital = "0";
                    //strCltAddr01 = "A46";
                    //strCltAddr04 = "Kalyan";
                    //strCltAddr05 = "Maharashtra";
                    //strCltDobx = "20010202";
                    //strCltPCode = "421201";
                    //strCltStat = "AC";
                    //strCtryCode = "IND";
                    strLanguage = "E";
                    //strServBrh = "10";
                    //strSrDate = "20161205";
                    //strTaxFlag = "Y";
                    //strBranch = "pra";
                    strUserRole = "10";
                    //strUserId = "rpandya";

                    //strCltDobx = "20161205";
                    strZoctInd = "N";
                    strNmfmt = "2";
                    //calling service                         

                    if (objChangeObj.ClientDetails.ClientType.Equals("P"))
                    {
                   
                        objMasterPersonalClient = objClientUpdation.CLIUPP(strClttwo, strAddrType, strBirthP, strCltAddr01, strCltAddr02, strCltAddr03, strCltAddr04, strCltAddr05, strCltDobx, strCltDodx
                                        , strCltPCode, strCltPhone01, strCltPhone02, strCltSex, strCltStat, strCtryCode, strDirMail, strDocNo, strLanguage, strLgivName, strLsurName
                                        , strMailing, strMarryd, strNatlty, strNmfmt, strOccpCode, strSalutl, strSecutiryNo, strServBrh, strSoe, strSrDate, strStatCode, strTaxFlag
                                        , strVIP, strZoctInd, strFaxNo, strOldIdNo, strRdidTelNo, strRinterNet, strRmblPhone, strRPager, strRstaFlag, strRtaxIdNum, strZpecInd
                                        , strDecgrSal, strEmprTaxr, strEvidDate, strIncDesc, strPayRollNo, strPensInd, strPrasInd, strSalCurr, strTaxYr, strBranch, strUserRole
                                        , strUserId, strPartnerId, strProcessId, strApplicationNo);

                        if (objMasterPersonalClient.ERRORCODE.Equals("0"))
                        {
                            strLAPushErrorcode = 0;
                            strLAPushStatus = "Success";
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :ClientUpdation.cs // MethodeName :ClientModPushService" + System.Environment.NewLine + "Client Updation Succeed" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + "*******Client Modification end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        }
                        else
                        {
                            strLAPushErrorcode = -1;
                             strLAPushStatus = objMasterPersonalClient.VALUES;

                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :ClientUpdation.cs // MethodeName :ClientModPushService" + System.Environment.NewLine + "Client Modification Failed" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + "*******Client Modification end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        }
                    }
                    else if (objChangeObj.ClientDetails.ClientType.Equals("C"))
                    {
                        // 1.1 Begin of Changes CR-4971 Sushant Devkate - MFL00905
                        objMasterCorporateClient = objClientUpdation.CLIUPC(strClttwo, strCapital, strCltAddr01, strCltAddr02, strCltAddr03, strCltAddr04, strCltAddr05, strCltDobx
                                       , strCltPCode, strCltPhone01, strCltPhone02, strCltStat, strCtryCode, strCtryOrig, strDirMail, strEcact, strFao, strFaxNo, strLanguage
                                       , strLgivName, strLsurName, strMailing, strSecutiryNo, strServBrh, strSrDate, strStaffNo, strStatCode, strTaxFlag, strTgrm, strTlxNo, strVIP
                                       , strOldIdNo, strRinterNet, strRtaxIdNum, strZpecInd, strBranch, strUserRole, strUserId, strPartnerId, strProcessId, strApplicationNo);

                        //objMasterCorporateClient = obj.CLIUPC(strClttwo, strCapital, strCltAddr01, strCltAddr02, strCltAddr03, strCltAddr04, strCltAddr05, strCltDobx
                        //           , strCltPCode, strCltPhone01, strCltPhone02, strCltStat, strCtryCode, strCtryOrig, strDirMail, strEcact, strFao, strFaxNo, strLanguage
                        //           , strLgivName, strLsurName, strMailing, strSecutiryNo, strServBrh, strSrDate, strStaffNo, strStatCode, strTaxFlag, strTgrm, strTlxNo, strVIP
                        //           , strOldIdNo, strRinterNet, strRtaxIdNum, strZpecInd, strBranch, strUserRole, strUserId, strPartnerId, strProcessId, strApplicationNo);



                        // 1.1 End of Changes CR-4971 Sushant Devkate - MFL00905
                        if (objMasterCorporateClient.ERRORCODE.Equals("0"))
                        {
                            strLAPushErrorcode = 0;
                            strLAPushStatus = "Success";
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :ClientUpdation.cs // MethodeName :ClientModPushService" + System.Environment.NewLine + "Client Updation Succeed" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + "*******Client Modification end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        }
                        else
                        {
                            strLAPushErrorcode = -1;
                            strLAPushStatus = objMasterCorporateClient.VALUES;
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :ContractModification.cs // MethodeName :ClientModPushService" + System.Environment.NewLine + "Client Modification Failed" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + "*******Client Modification end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        }
                    }
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :ClientUpdation.CS // MethodeName :ClientModPushService Error :" + System.Environment.NewLine + Error.ToString() + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "ClientModification.cs", "ClientModPushService", "E-Error", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******Client Modification end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
    }
}
