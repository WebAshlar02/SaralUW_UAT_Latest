using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;
using Platform.Utilities.LoggerFramework;
namespace UWSaralServices
{
    public class TPARegis
    {
        public void PushDataIntoTPARegisteration(UWSaralObjects.TPARegisteration objTPARegisteration)
        {
            //service input parameter
            string strUserName = string.Empty, strPassword = string.Empty, strProposalDate = string.Empty, strProposalNo = string.Empty, strMasterPolicyNo = string.Empty, strProposerInitial = string.Empty, strProposerName = string.Empty, strGender = string.Empty, strTestConducted = string.Empty
                        , strAddress1 = string.Empty, strAddress2 = string.Empty, strCity = string.Empty, strState = string.Empty, strDistrict = string.Empty, strTaluka = string.Empty, strPincode = string.Empty, strLandMark = string.Empty, strTelephone = string.Empty, strMobileNo = string.Empty, strHNIFlag = string.Empty, strHomeVisitFlag = string.Empty,
                        strAgentCode = string.Empty, strAgentName = string.Empty, strAgentContactDetails = string.Empty, strChannel = string.Empty, strBranchName = string.Empty, strPlanType = string.Empty, strProductName = string.Empty, strMemberDOB = string.Empty, strCustomerEmail = string.Empty,
                        strRemark = string.Empty, strPreferredDate = string.Empty, strPreferredTime = string.Empty, strPreferredProviderNo = string.Empty, strAge = string.Empty, strRMContactNumber = string.Empty, strRMEmailId = string.Empty, strIMDEmailId = string.Empty, strPaymentType = string.Empty
                        , strStatus = string.Empty, strAppilcantOfficeNumber = string.Empty, strDCName = string.Empty, strProposalStatus = string.Empty, strCashieringDate = string.Empty, strCashieredAmount = string.Empty, strPCName = string.Empty, strRegion = string.Empty, strRepeatCase = string.Empty,
                        strTPACost = string.Empty, strPreferredDCDetails = string.Empty, strResponse = string.Empty;
            try
            {
                Logger.Error("STAG 6 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :FetchPDF Start" + System.Environment.NewLine);
                //declaration                
                TPARegisteration.HITPA_PIMSClient objClient = new TPARegisteration.HITPA_PIMSClient();

                //fill string 
                strUserName = objTPARegisteration.UserName;
                strPassword = objTPARegisteration.Password;
                strProposalDate = objTPARegisteration.ProposalDate;
                strProposalNo = objTPARegisteration.ProposalNo;
                strMasterPolicyNo = objTPARegisteration.MasterPolicyNo;
                strProposerInitial = objTPARegisteration.ProposerInitial;
                strProposerName = objTPARegisteration.ProposerName;
                strGender = objTPARegisteration.Gender;
                strTestConducted = objTPARegisteration.TestConducted;
                strAddress1 = objTPARegisteration.Address1;
                strAddress2 = objTPARegisteration.Address2;
                strCity = objTPARegisteration.City;
                strState = objTPARegisteration.State;
                strDistrict = objTPARegisteration.District;
                strTaluka = objTPARegisteration.Taluka;
                strPincode = objTPARegisteration.Pincode;
                strLandMark = objTPARegisteration.LandMark;
                strTelephone = objTPARegisteration.Telephone;
                strMobileNo = objTPARegisteration.MobileNo;
                strHNIFlag = objTPARegisteration.HNIFlag;
                strHomeVisitFlag = objTPARegisteration.HomeVisitFlag;
                strAgentCode = objTPARegisteration.AgentCode;
                strAgentName = objTPARegisteration.AgentName;
                strAgentContactDetails = objTPARegisteration.AgentContactDetails;
                strChannel = objTPARegisteration.Channel;
                strBranchName = objTPARegisteration.BranchName;
                strPlanType = objTPARegisteration.PlanType;
                strProductName = objTPARegisteration.ProductName;
                strMemberDOB = objTPARegisteration.MemberDOB;
                strCustomerEmail = objTPARegisteration.CustomerEmail;
                strRemark = objTPARegisteration.Remark;
                strPreferredDate = objTPARegisteration.PreferredDate;
                strPreferredTime = objTPARegisteration.PreferredTime;
                strPreferredProviderNo = objTPARegisteration.PreferredProviderNo;
                strAge = objTPARegisteration.Age;
                strRMContactNumber = objTPARegisteration.RMContactNumber;
                strRMEmailId = objTPARegisteration.RMEmailId;
                strIMDEmailId = objTPARegisteration.IMDEmailId;
                strPaymentType = objTPARegisteration.PaymentType;
                strStatus = objTPARegisteration.Status;
                strAppilcantOfficeNumber = objTPARegisteration.AppilcantOfficeNumber;
                strDCName = objTPARegisteration.DCName;
                strProposalStatus = objTPARegisteration.ProposalStatus;
                strCashieringDate = objTPARegisteration.CashieringDate;
                strCashieredAmount = objTPARegisteration.CashieredAmount;
                strPCName = objTPARegisteration.PCName;
                strRegion = objTPARegisteration.Region;
                strRepeatCase = objTPARegisteration.RepeatCase;
                strTPACost = objTPARegisteration.TPACost;
                strPreferredDCDetails = objTPARegisteration.PreferredDCDetails;

                //call service                
                strResponse = objClient.RegisterCase(strUserName, strPassword, strProposalDate, strProposalNo, strMasterPolicyNo, strProposerInitial, strProposerName, strGender, strTestConducted, strAddress1, strAddress2, strCity, strState, strDistrict, strTaluka, strPincode, strLandMark, strTelephone, strMobileNo, strHNIFlag, strHomeVisitFlag,
                  strAgentCode, strAgentName, strAgentContactDetails, strChannel, strBranchName, strPlanType, strProductName, strMemberDOB, strCustomerEmail, strRemark, strPreferredDate, strPreferredTime, strPreferredProviderNo, strAge, strRMContactNumber, strRMEmailId, strIMDEmailId, strPaymentType, strStatus, strAppilcantOfficeNumber, strDCName, strProposalStatus, strCashieringDate, strCashieredAmount, strPCName, strRegion, strRepeatCase, strTPACost, strPreferredDCDetails);
                Logger.Error("STAG 6 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :FetchPDF End" + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                strResponse = ex.Message;
                UWSaralServices.CommFun objComm = new UWSaralServices.CommFun();
                objComm.SaveErrorLogs(string.Empty, "Failed", "UWSaralServices", "TPARegis.cs", "PushDataIntoTPARegisteration", "E-Error", "", "", ex.ToString());                
            }
            finally {
                objTPARegisteration.Response= strResponse;
                objTPARegisteration.Request = CommFun.GetXMLFromObject(objTPARegisteration);
                bool bln= objTPARegisteration.IsRequestSuccess;
            }
        }
    }
}
