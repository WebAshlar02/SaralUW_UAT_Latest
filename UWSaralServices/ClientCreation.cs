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
    public class ClientCreation
    {
        CommFun objcomm = new CommFun();
        public void ClientCreationModPushService(string strPQuoteNo, DataSet _dsClient, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "*******Client Creation Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :ClientCreation.CS // MethodeName :ClientCreationModPushService" + System.Environment.NewLine);
                //instanciate
                LAClientCreationService.ServiceClient objClientCreation = new LAClientCreationService.ServiceClient();
                LAClientCreationService.MasterCreate objMasterCreation = new LAClientCreationService.MasterCreate();

               ClientSvc.ServiceClient objClient = new ClientSvc.ServiceClient();
               ClientSvc.MasterCreate objClientCreationResult = new ClientSvc.MasterCreate();


                string strCapital = string.Empty, strCltAddr001 = string.Empty, strCltAddr002 = string.Empty, strCltAddr003 = string.Empty, strCltAddr004 = string.Empty
                       , strCltAddr005 = string.Empty, strCltDobx = string.Empty, strCltPCode = string.Empty, strCltPhone01 = string.Empty, strCLtPhone02 = string.Empty
                       , strCltStat = string.Empty, strCtryCode = string.Empty, strCtryOrig = string.Empty, strDirMail = string.Empty, strEcact = string.Empty
                       , strFao = string.Empty, strFaxNo = string.Empty, strLanguage = string.Empty, strLgivName = string.Empty, strLSurname = string.Empty
                       , strMailing = string.Empty, strSecurityNo = string.Empty, strServbhr = string.Empty, strSRDate = string.Empty, strStaffNo = string.Empty
                       , strCategory = string.Empty, strTaxFlag = string.Empty, strTGram = string.Empty, strTlxNo = string.Empty, strVip = string.Empty, strOldIdNo = string.Empty
                       , strRInternet = string.Empty, strRTaxIdNum = string.Empty, strZSpecInd = string.Empty, strBranch = string.Empty, strUserRole = string.Empty
                       , strUserId = string.Empty, strPartnerId = string.Empty, strProcessId = string.Empty, strApplicationNo = string.Empty
                    //personal
                        , strAddrType = string.Empty, strBirthP = string.Empty, strCltDodX = string.Empty, strCltSex = string.Empty, strDocNo = string.Empty
                        , strMarryd = string.Empty, strNatlty = string.Empty, strMNFMt = string.Empty, strOccupCode = string.Empty, strSalutl = string.Empty
                        , strServBrh = string.Empty, strSoe = string.Empty, strStatCode = string.Empty, strZDoctInd = string.Empty, strRpager = string.Empty, strRstafFlag = string.Empty
                        , strDecgrSal = string.Empty, strEmprTaxR = string.Empty, strEvidDate = string.Empty, strIncDesc = string.Empty, strMobileNo = string.Empty
                        , strPayRollNo = string.Empty, strPensInd = string.Empty, strPrasInd = string.Empty, strSalCurr = string.Empty, strTaxYr = string.Empty;
                if (objChangeObj.ClientDetails != null)
                {
                    strUserId = objChangeObj.ClientDetails.objBmpUserInfo._UserID;
                    if (objChangeObj.ClientDetails.AssuredType=="Nominee")
                    {
                        int intCount = 0;
                        if (objChangeObj.ClientDetails.lstClientAddress.Count > 1)
                        {
                            intCount = 1;
                        }
                        //assign value to string from client details object 
                        
                        strCltAddr001 = objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine1;
                        strCltAddr002 = objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine2;
                        strCltAddr003 = objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine3;
                        strCltAddr004 = objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine4;
                        strCltAddr005 = objChangeObj.ClientDetails.lstClientAddress[intCount].AddressLine5;
                        strCltDobx = objChangeObj.ClientDetails.ClientDob.ToString("dd/MM/yyyy");
                        strCltPhone01 = objChangeObj.ClientDetails.lstClientAddress[intCount].Phone1;
                        strCLtPhone02 = objChangeObj.ClientDetails.lstClientAddress[intCount].Phone2;
                        strCltStat = objChangeObj.ClientDetails.lstClientAddress[intCount].State;
                        strCtryCode = objChangeObj.ClientDetails.lstClientAddress[intCount].CountryCode;
                        strDirMail = objChangeObj.ClientDetails.lstClientAddress[intCount].EmailId;
                        strEcact = string.Empty;
                        strFao = string.Empty;
                        strFaxNo = objChangeObj.ClientDetails.lstClientAddress[intCount].FaxNo;
                        strLanguage = "E";
                        strLgivName = objChangeObj.ClientDetails.ClientFirstName;
                        strLSurname = objChangeObj.ClientDetails.ClinetLastName;
                        strMailing = objChangeObj.ClientDetails.lstClientAddress[intCount].EmailId;
                        strSecurityNo = string.Empty;
                        strServbhr = string.Empty;
                        strSRDate = string.Empty;
                        strStaffNo = string.Empty;
                        strCategory = string.Empty;
                        strTaxFlag = string.Empty;
                        strTGram = objChangeObj.ClientDetails.lstClientAddress[intCount].FaxNo;
                        strTlxNo = objChangeObj.ClientDetails.lstClientAddress[intCount].TelNo;
                        strVip = string.Empty;
                        strOldIdNo = string.Empty;
                        strRInternet = objChangeObj.ClientDetails.lstClientAddress[intCount].EmailId;
                        strRTaxIdNum = string.Empty;
                        strZSpecInd = string.Empty;
                        strBranch = "PRA";
                        strUserRole = "10";
                        strApplicationNo = objChangeObj.ClientDetails.ApplicationNo;
                        strAddrType = objChangeObj.ClientDetails.lstClientAddress[intCount].AddressType;
                        strBirthP = string.Empty;
                        strCltDodX = string.Empty;
                        strCltSex = Convert.ToString(objChangeObj.ClientDetails.ClientGender);
                        strDocNo = string.Empty;
                        strMarryd = "M";
                        strNatlty = "IND";
                        strMNFMt = "2";
                        strOccupCode = objChangeObj.ClientDetails.Occupation;
                        strSalutl = objChangeObj.ClientDetails.Salutation;
                        strServBrh = string.Empty;
                        strSoe = string.Empty;
                        strStatCode = string.Empty;
                        strZDoctInd = string.Empty;
                        strMobileNo = objChangeObj.ClientDetails.lstClientAddress[intCount].MobileNo;
                        strCltPCode = Convert.ToString(objChangeObj.ClientDetails.lstClientAddress[intCount].PinCode);    
                    }
                    else
                    {
                        //assign value to string from client details object                         
                        strCltAddr001 = objChangeObj.ClientDetails.lstClientAddress[0].AddressLine1;
                        strCltAddr002 = objChangeObj.ClientDetails.lstClientAddress[0].AddressLine2;
                        strCltAddr003 = objChangeObj.ClientDetails.lstClientAddress[0].AddressLine3;
                        strCltAddr004 = objChangeObj.ClientDetails.lstClientAddress[0].AddressLine4;
                        strCltAddr005 = objChangeObj.ClientDetails.lstClientAddress[0].AddressLine5;
                        strCltDobx = objChangeObj.ClientDetails.ClientDob.ToString("dd/MM/yyyy");
                        strCltPhone01 = objChangeObj.ClientDetails.lstClientAddress[0].Phone1;
                        strCLtPhone02 = objChangeObj.ClientDetails.lstClientAddress[0].Phone2;
                        strCltStat = "AC"; //objChangeObj.ClientDetails.lstClientAddress[0].State;
                        strCtryCode = objChangeObj.ClientDetails.lstClientAddress[0].CountryCode;
                        strDirMail = objChangeObj.ClientDetails.lstClientAddress[0].EmailId;
                        strEcact = string.Empty;
                        strFao = string.Empty;
                        strFaxNo = objChangeObj.ClientDetails.lstClientAddress[0].FaxNo;
                        strLanguage = "E";
                        strLgivName = objChangeObj.ClientDetails.ClientFirstName;
                        strLSurname = objChangeObj.ClientDetails.ClinetLastName;
                        strMailing = objChangeObj.ClientDetails.lstClientAddress[0].EmailId;
                        strSecurityNo = string.Empty;
                        strServbhr = string.Empty;
                        strSRDate = string.Empty;
                        strStaffNo = string.Empty;
                        strCategory = string.Empty;
                        strTaxFlag = string.Empty;
                        strTGram = objChangeObj.ClientDetails.lstClientAddress[0].FaxNo;
                        strTlxNo = objChangeObj.ClientDetails.lstClientAddress[0].TelNo;
                        strVip = string.Empty;
                        strOldIdNo = string.Empty;
                        strRInternet = objChangeObj.ClientDetails.lstClientAddress[0].EmailId;
                        strRTaxIdNum = string.Empty;
                        strZSpecInd = string.Empty;
                        strBranch = "PRA";
                        strUserRole = "10";
                        strApplicationNo = objChangeObj.ClientDetails.ApplicationNo;
                        strAddrType = objChangeObj.ClientDetails.lstClientAddress[0].AddressType;
                        strBirthP = string.Empty;
                        strCltDodX = string.Empty;
                        strCltSex = Convert.ToString(objChangeObj.ClientDetails.ClientGender);
                        strDocNo = string.Empty;
                        strMarryd = "M";

                        // 1.1 Begin of Changes CR-4971 Sushant Devkate - MFL00905
                        //strNatlty = "IND";
                        if (objChangeObj.ClientDetails.IsNRICustomer)
                        {
                            bool ISNRI = Convert.ToBoolean(ConfigurationManager.AppSettings["ISNRI"].ToString());
                            if (ISNRI)
                            {
                                strNatlty = "NRI";
                            }
                            else
                            {
                                strNatlty = objChangeObj.ClientDetails.Nationality;
                            }
                        }
                        else
                        {
                            strNatlty = "IND";
                        }
                        //1.1 End of Changes CR-4971 Sushant Devkate - MFL00905


                        strMNFMt = "2";
                        strOccupCode = objChangeObj.ClientDetails.Occupation;
                        strSalutl = objChangeObj.ClientDetails.Salutation;
                        strServBrh = string.Empty;
                        strSoe = string.Empty;
                        strStatCode = string.Empty;
                        strZDoctInd = string.Empty;
                        strMobileNo = objChangeObj.ClientDetails.lstClientAddress[0].MobileNo;
                        strCltPCode = Convert.ToString(objChangeObj.ClientDetails.lstClientAddress[0].PinCode);
                    }
                    

                    if (objChangeObj.ClientDetails.ClientType == "C")
                    {
                        // 1.1 Begin of Changes CR-4971 Sushant Devkate - MFL00905
                        //corporate client creation
                        //objMasterCreation = objClientCreation.CLICRC(strCapital, strCltAddr001, strCltAddr002, strCltAddr003, strCltAddr004, strCltAddr005, strCltDobx, strCltPCode, strCltPhone01, strCLtPhone02, strCltStat
                        //                         , strCtryCode, strCtryOrig, strDirMail, strEcact, strFao, strFaxNo, strLanguage, strLgivName, strLSurname, strMailing, strSecurityNo, strServbhr, strSRDate
                        //                         , strStaffNo, strCategory, strTaxFlag, strTGram, strTlxNo, strVip, strOldIdNo, strRInternet, strRTaxIdNum, strZSpecInd, strBranch, strUserRole, strUserId, strPartnerId
                        //                         , strProcessId, strApplicationNo);

                        objClientCreationResult = objClient.CLICRC(strCapital, strCltAddr001, strCltAddr002, strCltAddr003, strCltAddr004, strCltAddr005, strCltDobx, strCltPCode, strCltPhone01, strCLtPhone02, strCltStat
                                              , strCtryCode, strCtryOrig, strDirMail, strEcact, strFao, strFaxNo, strLanguage, strLgivName, strLSurname, strMailing, strSecurityNo, strServbhr, strSRDate
                                              , strStaffNo, strCategory, strTaxFlag, strTGram, strTlxNo, strVip, strOldIdNo, strRInternet, strRTaxIdNum, strZSpecInd, strBranch, strUserRole, strUserId, strPartnerId
                                              , strProcessId, strApplicationNo);
                        //1.1 End of Changes CR-4971 Sushant Devkate - MFL00905
                    }
                    else if (objChangeObj.ClientDetails.ClientType == "P")
                    {
                        // 1.1 Begin of Changes CR-4971 Sushant Devkate - MFL00905
                        //personal client creation
                        //objMasterCreation = objClientCreation.CLICRP(strAddrType, strBirthP, strCltAddr001, strCltAddr002, strCltAddr003, strCltAddr004, strCltAddr005, strCltDobx, strCltDodX, strCltPCode, strCltPhone01
                        //                        , strCLtPhone02, strCltSex, strCltStat, strCtryCode, strDirMail, strDocNo, strLanguage, strLgivName, strLSurname, strMailing, strMarryd, strNatlty, strMNFMt
                        //                        , strOccupCode, strSalutl, strSecurityNo, strServBrh, strSoe, strSRDate, strStatCode, strTaxFlag, strVip, strZDoctInd, strFaxNo, strOldIdNo, strTlxNo, strRInternet, strMobileNo,
                        //                        strRpager, strRstafFlag, strRTaxIdNum, strZSpecInd, strDecgrSal, strEmprTaxR, strEvidDate, strIncDesc, strPayRollNo, strPensInd, strPrasInd, strSalCurr, strTaxYr, strBranch, strUserRole, strUserId, strPartnerId
                        //                        , strProcessId, strApplicationNo);

                        objClientCreationResult = objClient.CLICRP(strAddrType, strBirthP, strCltAddr001, strCltAddr002, strCltAddr003, strCltAddr004, strCltAddr005, strCltDobx, strCltDodX, strCltPCode, strCltPhone01
                                                    , strCLtPhone02, strCltSex, strCltStat, strCtryCode, strDirMail, strDocNo, strLanguage, strLgivName, strLSurname, strMailing, strMarryd, strNatlty, strMNFMt
                                                    , strOccupCode, strSalutl, strSecurityNo, strServBrh, strSoe, strSRDate, strStatCode, strTaxFlag, strVip, strZDoctInd, strFaxNo, strOldIdNo, strTlxNo, strRInternet, strMobileNo,
                                                    strRpager, strRstafFlag, strRTaxIdNum, strZSpecInd, strDecgrSal, strEmprTaxR, strEvidDate, strIncDesc, strPayRollNo, strPensInd, strPrasInd, strSalCurr, strTaxYr, strBranch, strUserRole, strUserId, strPartnerId
                                                    , strProcessId, strApplicationNo);
                        //1.1 End of Changes CR-4971 Sushant Devkate - MFL00905
                    }
                    if (objClientCreationResult.ERRORCODE == "0")
                    {
                        // 1.1 Begin of Changes CR-4971 Sushant Devkate - MFL00905
                        //strLAPushStatus = objMasterCreation.CLNTNUM;
                        //strLAPushErrorcode = Convert.ToInt32(objMasterCreation.ERRORCODE);

                        strLAPushStatus = objClientCreationResult.CLNTNUM;
                        strLAPushErrorcode = Convert.ToInt32(objClientCreationResult.ERRORCODE);
                        //1.1 End of Changes CR-4971 Sushant Devkate - MFL00905
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :ClientCreation.cs // MethodeName :ClientCreationModPushService" + System.Environment.NewLine + "Client Creation Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******Client Creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        // 1.1 Begin of Changes CR-4971 Sushant Devkate - MFL00905
                        //strLAPushStatus = objMasterCreation.VALUES;
                        //strLAPushErrorcode = Convert.ToInt32(objMasterCreation.ERRORCODE);

                        strLAPushStatus = objClientCreationResult.VALUES;
                        strLAPushErrorcode = Convert.ToInt32(objClientCreationResult.ERRORCODE);

                        //1.1 End of Changes CR-4971 Sushant Devkate - MFL00905
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :ClientCreation.cs // MethodeName :ClientCreationModPushService" + System.Environment.NewLine + "Client Creation Failed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******Client Creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :ClientCreation.CS // MethodeName :ClientCreationModPushService Error :" + System.Environment.NewLine + Error.ToString() + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "ClientCreation.cs", "ClientCreationModPushService", "E-Error", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******Client Creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
    }
}
