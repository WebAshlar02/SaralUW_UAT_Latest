/*
*********************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :SHRIJEET 
METHODE/EVENT:CHANGE VALUE
REMARK: ADDED COMMENT OBJECT 
DateTime :09JAN17
**********************************************************************************************************************************
**********************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :SHRIJEET 
METHODE/EVENT:CHANGE VALUE
REMARK: ADDED ADD ADHAR PROPERTY 
DateTime :09JAN17
**********************************************************************************************************************************
 * **********************************************************************************************************************************
COMMENT ID: 3
COMMENTOR NAME :ROSHAN 
METHODE/EVENT: Dictionary
REMARK: ADDED ILIST
DateTime :23JAN18
**********************************************************************************************************************************
 ** **********************************************************************************************************************************
COMMENT ID: 4
COMMENTOR NAME :SHRIJEET
METHODE/EVENT: Dictionary
REMARK: ADDED TIFF OBJECT 
DateTime :06FEB18
**********************************************************************************************************************************
 ***********************************************************************************************************************************
COMMENT ID: 5
COMMENTOR NAME :SHRIJEET
METHODE/EVENT: 
REMARK: ADDED TPARegisteration service
DateTime :06Feb18
**********************************************************************************************************************************
 ***********************************************************************************************************************************
COMMENT ID: 6
COMMENTOR NAME :SHRIJEET
METHODE/EVENT: 
REMARK: ADDED TPARegisteration service
DateTime :30Mar18
**********************************************************************************************************************************
* ************************************************************************************************************************************
COMMENT ID: 7
COMMENTOR NAME :AJAY SAHU
METHODE/EVENT: 
REMARK: MODIFY AUTO COMMENT
DateTime :08JUNE18
**********************************************************************************************************************************
* * ************************************************************************************************************************************
COMMENT ID: 8
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT: 
REMARK: ADD CLASS OF UW MEDICAL MANAGEMENT 
DateTime :08JUNE18
**********************************************************************************************************************************
* * ************************************************************************************************************************************
COMMENT ID: 9
COMMENTOR NAME :SHRIJEET SHANIL
METHODE/EVENT: 
REMARK: ADD LOADING ADVANCED LOGIC
DateTime :22APRIL19
**********************************************************************************************************************************
**//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-3334 
// Date Of Creation     : 18/11/2022
// Description          :Retup Multiple Reason Option
//**********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace UWSaralObjects
{
    /*ID:3 START*/
    public class ListReportList
    {
        public List<ReportList> ReportList = new List<ReportList>();

        public ListReportList()
        {
            FillReportList("DOC QC Reject", 1);
            FillReportList("STP Cases", 2);
            //FillReportList("Financial Year", 3);
            FillReportList("UW Productivity", 4);
            FillReportList("Requirements Raised", 5);
        }

        private void FillReportList(string strText, int intValue)
        {
            ReportList objReportList = new ReportList();
            objReportList.Text = strText;
            objReportList.Value = intValue;
            ReportList.Add(objReportList);
        }
    }

    public class ReportList
    {
        private int intValue;

        public int Value
        {
            get { return intValue; }
            set { intValue = value; }
        }

        private string strText;

        public string Text
        {
            get { return strText; }
            set { strText = value; }
        }


    }
    /*ID:3 END*/
    public class ChangeValue
    {
        public AppDtls App_backdate { get; set; }
        public ProdDtls Prod_policydetails { get; set; }
        public LoadDtls Load_Loadingdetails { get; set; }
        //1.1 Begin of Changes; Bhaumik Patel - [CR-3334]
        public DecisionDtls Decision_details { get; set; }
        //1.1 End of Changes; Bhaumik Patel - [CR-3334]
        public AfiCfiUW AfiCfiUw { get; set; }
        public LoginUserDetails userLoginDetails { get; set; }
        public ClientDetails ClientDetails { get; set; }
        public Journal Journal { get; set; }

        public List<RiderInfo> RiderInfo { get; set; }

    }
    public class AppDtls
    {
        private string strBackdate;

        public string _Backdate
        {
            get { return (strBackdate.Contains("1900-01-01")) ? string.Empty : strBackdate; }
            set { strBackdate = value; }
        }
        public string _NAWPCatagory { get; set; }
        //public string _Backdate { get; set; }        
    }
    public class ProdDtls
    {
        public string _ProdcodeBase { get; set; }
        public string _PolicyTerm { get; set; }
        public string _Premiumpayingterm { get; set; }
        public string _Sumassured { get; set; }
        public string _Paymentfrequency { get; set; }
        public string _Amountinsis { get; set; }
        public string _Basepremiumamount { get; set; }
        public string _TotalPremiumamount { get; set; }
        public string _ServiceTax { get; set; }
        public string _ProdcodeCombo { get; set; }
        public string _PolicyTermCombo { get; set; }
        public string _PremiumpayingtermCombo { get; set; }
        public string _SumassuredCombo { get; set; }
        public string _BasepremiumamountCombo { get; set; }
        public string _TotalPremiumamountCombo { get; set; }
        public string _MonthlyPayoutCombo { get; set; }
        public string _MonthlyPayoutBase { get; set; }
        public string _ServiceTaxCombo { get; set; }

        public string _Category { get; set; }
        public string _PayoutType { get; set; }
        public string _PayoutTerm { get; set; }
        public string _LumpSumPercent { get; set; }
        public string _PayOutFrquency { get; set; }
        public string _ProductCategory { get; set; }
    }

    public class LoadDtls
    {
        public string _strProdcode { get; set; }
        public bool isConsentRequired { get; set; }
        public List<LoadParam> lstLoadParam { get; set; }
    }

    //1.1 Begin of Changes; Bhaumik Patel - [CR-3334]
    public class DecisionDtls
    {
        public string _strProdcode { get; set; }
        public bool isConsentRequired { get; set; }
    }
    //1.1 Begin of Changes; Bhaumik Patel - [CR-3334]
    public class LoadParam
    {
        public string strRiderCtg { get; set; }
        public string strMedicalLoadingClass { get; set; }
        public string strNonMedicalLoading { get; set; }
        public string[] strNonMedicalLoading1 { get; set; }
        public string[] strMedicalLoadingClass1 { get; set; }
        public string[] strRiderCode { get; set; }
    }

    public class AfiCfiUW
    {
        public string _PolicyNo { get; set; }
        public string _ReasonId { get; set; }
        public string _ReasonDesc { get; set; }
        public string _Branch { get; set; }
        public string _UserRole { get; set; }
        public string _UserId { get; set; }
        public string _PartnerId { get; set; }
        public string _strProcessId { get; set; }
        public string _ApplicationNo { get; set; }
    }

    public class LoginUserDetails
    {
        /*added by shri*/
        private static string ReplaceWithEmpty(string strGet)
        {
            return (string.IsNullOrEmpty(strGet) ? string.Empty : strGet.Trim());
        }
        /*end here*/
        public string _UserID { get; set; }
        public string _UserGroup { get; set; }
        public string _UserName { get; set; }
        public string _MinSumassured { get; set; }
        public string _MaxSumassured { get; set; }
        public string _userBranch { get; set; }
        public string _UserRole { get; set; }
        public string _UserMessage { get; set; }
        public string _ProcessName { get; set; } /*added by shri*/
        private DateTime dtCreationDate;
        public DateTime _CreationDate
        {
            get { return dtCreationDate; }
            set { dtCreationDate = value; }
        }
        private DateTime dtBmpSystemDate;
        public DateTime _SystemDate
        {
            get { return dtBmpSystemDate; }
            set { dtBmpSystemDate = value; }
        }
        private DateTime dtBmpBusinessDate;
        public DateTime _BusinessDate
        {
            get { return dtBmpBusinessDate; }
            set { dtBmpBusinessDate = value; }
        }
        private string strBmpApplicationName;
        public string _ApplicationName
        {
            get { return ReplaceWithEmpty(strBmpApplicationName = "UWSaral"); }
            set { strBmpApplicationName = value; }
        }
        /*end here*/
    }


    public class PolicyDetails
    {
        private string strApplicationNumber;

        public string ApplicationNumber
        {
            get { return strApplicationNumber; }
            set { strApplicationNumber = value; }
        }

        private string strPolicyNumber;

        public string PolicyNumber
        {
            get { return strPolicyNumber; }
            set { strPolicyNumber = value; }
        }

        private string strPolicyStatus;

        public string PolicyStatus
        {
            get { return strPolicyStatus; }
            set { strPolicyStatus = value; }
        }

        private string strFirstNameLgivName;

        public string FirstNameLgivName
        {
            get { return strFirstNameLgivName; }
            set { strFirstNameLgivName = value; }
        }

    }

    /*added by shri on 27 june 17*/
    public class ClientDetails
    {
        private static string ReplaceWithEmpty(string strGet)
        {
            return (string.IsNullOrEmpty(strGet) ? string.Empty : strGet.Trim());
        }

        private bool blnIsFromPage;

        public bool IsFromPage
        {
            get { return blnIsFromPage; }
            set { blnIsFromPage = value; }
        }

        private string strClientid;

        public string ClientId
        {
            get { return ReplaceWithEmpty(strClientid); }
            set { strClientid = value; }
        }

        private string strSalutation;

        public string Salutation
        {
            get { return ReplaceWithEmpty(strSalutation); }
            set { strSalutation = value; }
        }

        private string strClientFirstName;

        public string ClientFirstName
        {
            get { return ReplaceWithEmpty(strClientFirstName); }
            set { strClientFirstName = value; }
        }

        private string strClientFatherName;

        public string ClientFatherName
        {
            get { return ReplaceWithEmpty(strClientFatherName); }
            set { strClientFatherName = value; }
        }

        private string strClientLastName;

        public string ClinetLastName
        {
            get { return ReplaceWithEmpty(strClientLastName); }
            set { strClientLastName = value; }
        }

        private DateTime dtClientDob;

        public DateTime ClientDob
        {
            get { return dtClientDob; }
            set { dtClientDob = value; }
        }

        private string strClientDob;

        public string ClientDateOfBirth
        {
            get
            {
                strClientDob = ClientDob.ToString("dd-MM-yyyy");
                return strClientDob;
            }
            set { strClientDob = value; }
        }

        private char chrClientGender;

        public char ClientGender
        {
            get { return chrClientGender; }
            set { chrClientGender = value; }
        }

        private bool blnClientGender;

        public bool boolClientGender
        {
            get
            {
                if (chrClientGender == 'M')
                {
                    blnClientGender = true;
                }
                else
                {
                    blnClientGender = false;
                }
                return blnClientGender;
            }
            set { blnClientGender = value; }
        }

        private bool blnIsSmoker;

        public bool IsSmoker
        {
            get { return blnIsSmoker; }
            set { blnIsSmoker = value; }
        }

        private string strClientRole;

        public string ClientRole
        {
            get { return ReplaceWithEmpty(strClientRole); }
            set { strClientRole = value; }
        }

        private string strApplicationNo;

        public string ApplicationNo
        {
            get { return ReplaceWithEmpty(strApplicationNo); }
            set { strApplicationNo = value; }
        }

        private string strClientType;

        private bool blnIsNSAP;

        public bool IsNSAP
        {
            get { return blnIsNSAP; }
            set { blnIsNSAP = value; }
        }

        private string strOccupation;

        public string Occupation
        {
            get { return ReplaceWithEmpty(strOccupation); }
            set { strOccupation = value; }
        }

        public string ClientType
        {
            get { return ReplaceWithEmpty(strClientType); }
            set { strClientType = value; }
        }

        private string strAssuredType;

        public string AssuredType
        {
            get { return ReplaceWithEmpty(strAssuredType); }
            set { strAssuredType = value; }
        }

        private string strNationality;

        public string Nationality
        {
            get { return ReplaceWithEmpty(strNationality); }
            set { strNationality = value; }
        }

        /*ADDED BY SHRI*/
        private string strIsProposer;
        public string IsProposer
        {
            get { return strIsProposer; }
            set { strIsProposer = value; }
        }
        /*END HERE*/

        /*ID:2 START*/
        private string strAadhar;

        public string Aadhar
        {
            get { return strAadhar; }
            set { strAadhar = value; }
        }

        /*ID:2 END*/
        public List<ClientAddress> lstClientAddress;

        public LoginUserDetails objBmpUserInfo;

        public bool IsNRICustomer {get;set;}
    }

    public class ClientAddress
    {
        public static string ReplaceWithEmpty(string strGet)
        {
            return (string.IsNullOrEmpty(strGet)) ? string.Empty : strGet.Trim();
        }
        private string strAddressLine1;

        public string AddressLine1
        {
            get { return ReplaceWithEmpty(strAddressLine1); }
            set { strAddressLine1 = value; }
        }

        private string strAddressLine2;

        public string AddressLine2
        {
            get { return ReplaceWithEmpty(strAddressLine2); }
            set { strAddressLine2 = value; }
        }

        private string strAddressLine3;

        public string AddressLine3
        {
            get { return ReplaceWithEmpty(strAddressLine3); }
            set { strAddressLine3 = value; }
        }
        private string strAddressLine4;

        public string AddressLine4
        {
            get { return ReplaceWithEmpty(strAddressLine4); }
            set { strAddressLine4 = value; }
        }
        private string strAddressLine5;

        public string AddressLine5
        {
            get { return ReplaceWithEmpty(strAddressLine5); }
            set { strAddressLine5 = value; }
        }

        private string strLandMark;

        public string LandMark
        {
            get { return ReplaceWithEmpty(strLandMark); }
            set { strLandMark = value; }
        }

        private int intPinCode;

        public int PinCode
        {
            get { return intPinCode; }
            set { intPinCode = value; }
        }

        private string strCity;

        public string City
        {
            get { return strCity; }
            set { strCity = value; }
        }

        private string strDistrict;

        public string District
        {
            get { return ReplaceWithEmpty(strDistrict); }
            set { strDistrict = value; }
        }

        private string strState;

        public string State
        {
            get { return ReplaceWithEmpty(strState); }
            set { strState = value; }
        }

        private string strCountryCode;

        public string CountryCode
        {
            get { return ReplaceWithEmpty(strCountryCode); }
            set { strCountryCode = value; }
        }

        private string strAddressRemark;

        public string AddressRemark
        {
            get { return ReplaceWithEmpty(strAddressRemark); }
            set { strAddressRemark = value; }
        }

        private string strPhone1;

        public string Phone1
        {
            get { return ReplaceWithEmpty(strPhone1); }
            set { strPhone1 = value; }
        }

        private string strPhone2;

        public string Phone2
        {
            get { return ReplaceWithEmpty(strPhone2); }
            set { strPhone2 = value; }
        }

        private string strTelNo;

        public string TelNo
        {
            get { return ReplaceWithEmpty(strTelNo); }
            set { strTelNo = value; }
        }

        private string strMobileNo;

        public string MobileNo
        {
            get { return ReplaceWithEmpty(strMobileNo); }
            set { strMobileNo = value; }
        }

        private string strEmailId;

        public string EmailId
        {
            get { return ReplaceWithEmpty(strEmailId); }
            set { strEmailId = value; }
        }

        private string strFaxNo;

        public string FaxNo
        {
            get { return ReplaceWithEmpty(strFaxNo); }
            set { strFaxNo = value; }
        }

        private string strAddressType;

        public string AddressType
        {
            get { return ReplaceWithEmpty(strAddressType); }
            set { strAddressType = value; }
        }
    }

    /*added by shri on 06 sept 17 to add mandate details*/
    public class BankDetails
    {
        public LoginUserDetails objLoginUserDetails { get; set; }
        private string strBankName;

        public string BankName
        {
            get { return strBankName; }
            set { strBankName = value; }
        }

        private string strBranchName;

        public string BranchName
        {
            get { return strBranchName; }
            set { strBranchName = value; }
        }
        private string strBankBranchCode;

        public string BankBranchCode
        {
            get { return strBankBranchCode; }
            set { strBankBranchCode = value; }
        }

        private string strAutoPay;

        public string AutoPay
        {
            get { return strAutoPay; }
            set { strAutoPay = value; }
        }

    }

    public class MandateDetails : BankDetails
    {
        private string strApplicationNo;

        public string ApplicationNo
        {
            get { return strApplicationNo; }
            set { strApplicationNo = value; }
        }

        private string strMicrCode;

        public string MicroCode
        {
            get { return strMicrCode; }
            set { strMicrCode = value; }
        }

        private string strAccountType;

        public string AccountType
        {
            get { return strAccountType; }
            set { strAccountType = value; }
        }

        private string strAccountNumber;

        public string AccountNumber
        {
            get { return strAccountNumber; }
            set { strAccountNumber = value; }
        }

        private string strAccountHolderName;

        public string AccountHolderName
        {
            get { return strAccountHolderName; }
            set { strAccountHolderName = value; }
        }

        private DateTime dtMandateDate;

        public DateTime MandateDate
        {
            get { return dtMandateDate; }
            set { dtMandateDate = value; }
        }

        public string strMandateDate
        {
            get { return dtMandateDate.ToString("yyyyMMdd"); }
            //set { dtMandateDate.ToString("yyyyMMdd") = value; }
        }

        private string strCreditCardNo;

        public string CreditCardNo
        {
            get { return StringNull(strCreditCardNo); }
            set { strCreditCardNo = value; }
        }

        private string strCreditCardType;

        public string CreditCardType
        {
            get { return StringNull(strCreditCardType); }
            set { strCreditCardType = value; }
        }

        private string strValidUpto;

        public string ValidUpto
        {
            get { return StringNull(strValidUpto); }
            set { strValidUpto = value; }
        }

        public string CreditCardMask
        {
            get
            {
                if (!string.IsNullOrEmpty(strCreditCardNo))
                {
                    return string.IsNullOrEmpty(strCreditCardNo) ? string.Empty : Masking(strCreditCardNo);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private string Masking(string strInput)
        {
            string strRet = string.Empty;
            StringBuilder objStrBuild = new StringBuilder();
            objStrBuild.Append(strInput);
            objStrBuild.Remove(3, 8);
            objStrBuild.Insert(3, "XXXXXXXX");
            return strRet;
        }

        private string StringNull(string strInput)
        {
            return string.IsNullOrEmpty(strInput) ? string.Empty : strInput.Trim();
        }
    }

    public class PremiumCalcDtls
    {
        private double dblInstalmentPremiumAmt;

        public double InstalmentPremiumAmt
        {
            get { return dblInstalmentPremiumAmt; }
            set { dblInstalmentPremiumAmt = value; }
        }

        private double dblMedicalLoadingPremium;

        public double MedicalLoadingPremium
        {
            get { return dblMedicalLoadingPremium; }
            set { dblMedicalLoadingPremium = value; }
        }
        private double dblNonMedicalLoadingPremium;

        public double NonMedicalLoadingPremium
        {
            get { return dblNonMedicalLoadingPremium; }
            set { dblNonMedicalLoadingPremium = value; }
        }

        private double dblSumAssured;

        public double SumAssured
        {
            get { return dblSumAssured; }
            set { dblSumAssured = value; }
        }
        private double dblTotalInstalmentPremium;

        public double TotalInstalmentPremium
        {
            get { return dblTotalInstalmentPremium; }
            set { dblTotalInstalmentPremium = value; }
        }
        private double dblTotalPremiumAmt;

        public double TotalPremiumAmt
        {
            get { return dblTotalPremiumAmt; }
            set { dblTotalPremiumAmt = value; }
        }
    }
    /*end here*/

    public class KeyValuePair
    {
        private string strKey;
        public string Key
        {
            get { return strKey; }
            set { strKey = value; }
        }
        private string strValue;
        public string Value
        {
            get { return strValue; }
            set { strValue = value; }
        }

    }

    /*added by shri on 26 july 17 to create class of rider*/
    public class RiderInfo
    {
        private string strProductCode;

        public string ProductCode
        {
            get { return strProductCode; }
            set { strProductCode = value; }
        }

        private double dblInstalmentPremiumAmt;

        public double InstalmentPremiumAmt
        {
            get { return dblInstalmentPremiumAmt; }
            set { dblInstalmentPremiumAmt = value; }
        }

        private double dblMedicalLoadingPremium;

        public double MedicalLoadingPremium
        {
            get { return dblMedicalLoadingPremium; }
            set { dblMedicalLoadingPremium = value; }
        }

        private double dblNonMedicalLoadingPremium;

        public double NonMedicalLoadingPremium
        {
            get { return dblNonMedicalLoadingPremium; }
            set { dblNonMedicalLoadingPremium = value; }
        }

        private double dblSumAssured;

        public double SumAssured
        {
            get { return dblSumAssured; }
            set { dblSumAssured = value; }
        }

        private double dblServiceTax;

        public double ServiceTax
        {
            get { return dblServiceTax; }
            set { dblServiceTax = value; }
        }
        private double dblTotalInstalmentPremium;

        public double TotalInstalmentPremium
        {
            get { return dblTotalInstalmentPremium; }
            set { dblTotalInstalmentPremium = value; }
        }

        private double dblTotalPremiumAmount;

        public double TotalPremiumAmount
        {
            get { return dblTotalPremiumAmount; }
            set { dblTotalPremiumAmount = value; }
        }

        private string strRiderId;

        public string RiderId
        {
            get { return strRiderId; }
            set { strRiderId = value; }
        }

        private string strRiderType;

        public string RiderType
        {
            get { return strRiderType; }
            set { strRiderType = value; }
        }
        private bool blnIsActive;

        public bool IsActive
        {
            get { return blnIsActive; }
            set { blnIsActive = value; }
        }
        private string strRiderName;

        public string RiderName
        {
            get { return strRiderName; }
            set { strRiderName = value; }
        }

    }

    /*added by shri on 08 sept 17 to add class of journal*/
    public class Journal
    {
        private string strApplicationNumber;

        public string ApplicationNumber
        {
            get { return strApplicationNumber; }
            set { strApplicationNumber = value; }
        }

        private string strDocumentNumber;

        public string DocumentNumber
        {
            get { return strDocumentNumber; }
            set { strDocumentNumber = value; }
        }

        private string strPassword;

        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }


    }
    /*end here*/

    public enum ClientAction
    {
        Create = 0
        ,
        Update
            ,
        Afi
            ,
        Cfi
            , Uw
    }
    /*ID:1 START*/
    public class AutoComment
    {
        string strDoubleSlash = @"//";
        string strSpace = " ";
        string strSingleSlash = @"/";
        string strNewLine = string.Empty;// "&#13;&#10;";

        private string strTotalSAHHI;

        public string TOTALSA_HHI
        {
            get { return strTotalSAHHI.Trim(); }
            set { strTotalSAHHI = value; }
        }

        private string strTotalSA;

        public string TOTALSA_NON_HHI
        {
            get { return strTotalSA.Trim(); }
            set { strTotalSA = value; }
        }
        private string strTMsar;

        public string TMSAR
        {
            get { return strTMsar.Trim(); }
            set { strTMsar = value; }
        }

        private string strTsar;

        public string TSAR
        {
            get { return strTsar.Trim(); }
            set { strTsar = value; }
        }

        private string strFsar;

        public string FSAR
        {
            get { return strFsar.Trim(); }
            set { strFsar = value; }
        }

        private string strTotalPremium;

        public string TotalPremium
        {
            get { return strTotalPremium.Trim(); }
            set { strTotalPremium = value; }
        }


        private string strSummaryComment;

        public string SummaryComment
        {
            get { return SummaryFormat(strSummaryComment, Convert.ToInt32(SummaryType.AutoSummary)); }
            set { strSummaryComment = value; }
        }


        private string strNameOfLa;

        public string NameOfLa
        {
            get { return strNameOfLa.Trim(); }
            set { strNameOfLa = value; }
        }

        private string strGender;

        public string Gender
        {
            get { return strGender.Trim(); }
            set { strGender = value; }
        }

        private string strEducation;

        public string Education
        {
            get { return strEducation.Trim(); }
            set { strEducation = value; }
        }

        private string strOccupation;

        public string Occupation
        {
            get { return strOccupation.Trim(); }
            set { strOccupation = value; }
        }

        private string strAnnualIncome;
        public string AnnualIncome
        {
            get { return strAnnualIncome.Trim(); }
            set { strAnnualIncome = value; }
        }


        private string strNominee;

        public string Nominee
        {
            get { return strNominee.Trim(); }
            set { strNominee = value; }
        }

        private string strProposer;

        public string Proposer
        {
            get { return strProposer.Trim(); }
            set { strProposer = value; }
        }

        private string strBmi;

        public string Bmi
        {
            get { return strBmi.Trim(); }
            set { strBmi = value; }
        }


        private string strNatureOfDuty;

        public string NatureOfDuty
        {
            get { return strNatureOfDuty.Trim(); }
            set { strNatureOfDuty = value; }
        }



        private string strTypeOfIncomeProof;
        public string TypeOfIncomeProof
        {
            get { return strTypeOfIncomeProof.Trim(); }
            set { strTypeOfIncomeProof = value; }
        }

        private string strIdProof;

        public string IdProof
        {
            get { return strIdProof.Trim(); }
            set { strIdProof = value; }
        }

        private string strAddressProof;

        public string AddressProof
        {
            get { return strAddressProof.Trim(); }
            set { strAddressProof = value; }
        }

        private string strFamilyHistory;

        public string FamilyHistory
        {
            get { return strFamilyHistory.Trim(); }
            set { strFamilyHistory = value; }
        }

        private string strPersonalMedicalHistory;

        public string PersonalMedicalHistory
        {
            get { return strPersonalMedicalHistory.Trim(); }
            set { strPersonalMedicalHistory = value; }
        }


        private string strAge;

        public string Age
        {
            get { return strAge.Trim(); }
            set { strAge = value; }
        }

        private string strAgeProof;

        public string AgeProof
        {
            get { return strAgeProof.Trim(); }
            set { strAgeProof = value; }
        }
        private string strPlanName;

        public string PlanName
        {
            get { return strPlanName.Trim(); }
            set { strPlanName = value; }
        }
        private string strSumAssured;

        public string SumAssured
        {
            get { return strSumAssured.Trim(); }
            set { strSumAssured = value; }
        }

        private string strPolicyTerm;

        public string PolicyTerm
        {
            get { return strPolicyTerm.Trim(); }
            set { strPolicyTerm = value; }
        }
        private string strPolicyPayingTerm;

        public string PolicyPayingTerm
        {
            get { return strPolicyPayingTerm.Trim(); }
            set { strPolicyPayingTerm = value; }
        }

        private string strMaritualStatus;

        public string MaritualStatus
        {
            get { return strMaritualStatus.Trim(); }
            set { strMaritualStatus = value; }
        }

        private string strBranchCode;

        public string BranchCode
        {
            get { return strBranchCode.Trim(); }
            set { strBranchCode = value; }
        }

        private string strRiskScore;

        public string RiskScore
        {
            get { return strRiskScore.Trim(); }
            set { strRiskScore = value; }
        }

        private string strEYRiskScore;

        public string EYRiskScore
        {
            get { return strEYRiskScore.Trim(); }
            set { strEYRiskScore = value; }
        }

        private string strChannelName;
        public string ChannelName
        {
            get { return strChannelName.Trim(); }
            set { strChannelName = value; }
        }

        private string strExistingIns;
        public string ExistingIns
        {
            get { return strExistingIns.Trim(); }
            set { strExistingIns = value; }
        }


        private string SummaryFormat(string strSummaryComment, int intCommentType)
        {
            if (intCommentType == 0)
            {
                //strSummaryComment = "TMSAR" + strSpace + TMSAR + strSpace + strDoubleSlash + strSpace + "FSAR" + strSpace + FSAR + strSpace + strDoubleSlash
                //    + strSpace + "TotalPremium" + strSpace + TotalPremium + strNewLine + strNewLine + strSpace + "Name of LA" + strSpace + NameOfLa + strSingleSlash + strSpace + "Gender" + strSpace + Gender
                //    + strSpace + "Age(in yrs)" + strSpace + Age + strSpace + strSingleSlash + "Age proof" + strSpace + AgeProof + strSpace + strDoubleSlash + strSpace
                //    + "Education" + strSpace + Education + strSpace + strSingleSlash + strSpace + "Occupation" + strSpace + Occupation + strSpace + strSingleSlash + strSpace
                //    + "Nature of duties" + strSpace + NatureOfDuty + strSpace + "Annual income" + strSpace + AnnualIncome + strSpace + strDoubleSlash + strSpace
                //    + "Type of income proof" + strSpace + TypeOfIncomeProof + "ID proof" + strSpace + IdProof + strSpace + strDoubleSlash + strSpace
                //    + "Address proof" + strSpace + AddressProof + strSpace + strDoubleSlash + strSpace + "Nominee" + strSpace + Nominee + strSpace + strSingleSlash
                //    + strSpace + "Proposer" + strSpace + Proposer + strSpace + "BMI" + strSpace + Bmi + strSpace + "Family History" + strSpace
                //    + FamilyHistory + strSpace + strDoubleSlash + strSpace + "Personal Medical History" + strSpace + PersonalMedicalHistory;
                // Personal Medical History (If any adverse finding mentioned)
                /*START ID:7*/
                strSummaryComment = "PROFILE : " + ChannelName + "-" + BranchCode + strSingleSlash + Gender + strSingleSlash + MaritualStatus + strSingleSlash + Age + strSingleSlash +
                    AgeProof + strSingleSlash + AddressProof + strSingleSlash + Education + strSingleSlash + Occupation + strSingleSlash + "Ai-" + AnnualIncome +
                    Environment.NewLine +

                    "HEALTH : " + "FamilyHistory-" + FamilyHistory + strSingleSlash + "Bmi-" + Bmi +
                    Environment.NewLine +

                    "EXISTING INS : " + ExistingIns +
                    Environment.NewLine +

                    "PLAN : " + PlanName + strSingleSlash + "SA-" + SumAssured + strSingleSlash + "Prem-" + TotalPremium + strSingleSlash + "PT-" + PolicyTerm + strSingleSlash + "PPT-" + PolicyPayingTerm +
                    Environment.NewLine +

                    "NOMINEE : " + Nominee + strSingleSlash + "Proposer- " + Proposer +
                    Environment.NewLine +

                    "MSAR : " + TMSAR +
                    Environment.NewLine +

                    "FSAR : " + FSAR +
                    Environment.NewLine +

                    "TSAR : " + TSAR +
                    Environment.NewLine +

                    "TOTAL SUMASSURED HHI : " + TOTALSA_HHI +
                    Environment.NewLine +

                    "TOTAL SUMASSURED NON HHI : " + TOTALSA_NON_HHI +
                    Environment.NewLine +

                    "LA total premium (FY) :" + TotalPremium +
                    Environment.NewLine +

                    "AML & KYC :" + Age + strSingleSlash + AddressProof + strSingleSlash + IdProof + strSingleSlash + TypeOfIncomeProof +
                    Environment.NewLine +

                    "RISK SCORE :" + "Risk score " + RiskScore + " & EY risk score " + EYRiskScore;

                    


                /*END ID:7*/
            }
            return strSummaryComment;
        }

    }

    public enum SummaryType
    {
        AutoSummary = 0

    }
    /*ID:1 END*/

    /*ID:4 START*/
    public class TIF : ErrorHandeling
    {
        private string strApplicationNo;

        public string ApplicationNo
        {
            get { return strApplicationNo; }
            set { strApplicationNo = value; }
        }

        private string strFileName;

        public string FileName
        {
            get { return strFileName; }
            set { strFileName = value; }
        }

        private string strFileNameMER;
        public string FileNameMER
        {
            get { return strFileNameMER; }
            set { strFileNameMER = value; }
        }

        public byte[] bytArrSend
        {
            get;
            set;
        }

        public byte[] bytArrReceive
        {
            get;
            set;
        }
        private string strExtension;

        public string Extension
        {
            get { return strExtension; }
            set { strExtension = value; }
        }



        public string FileNameWithTiffExtension
        {
            get { return FileName.Substring(0, FileName.IndexOf('.')) + ".tif"; }
        }

        public override void GetMessage()
        {
            Dictionary<int, string> objDict = new Dictionary<int, string>();
            if (objDict.Count <= 0)
            {
                objDict.Add(-2, "Direcotry Does Not Exists");
                objDict.Add(-3, "File Does Not Exists");
                objDict.Add(-4, "Tiff Conversion Issue");
                objDict.Add(0, "Success");
            }
            if (objDict.ContainsKey(Flag))
            {
                Message = objDict[Flag];
            }
        }

    }

    public class ErrorHandeling
    {
        private int intFlag = 1;

        public int Flag
        {
            get { return intFlag; }
            set { intFlag = value; }
        }

        private string strMessage;

        public string Message
        {
            get { return strMessage; }
            set { strMessage = value; }
        }
        public virtual void GetMessage()
        {
            throw new Exception("Cant Call");
        }
    }

    public class ListTIff
    {
        public List<TIF> LstTiff { get; set; }

        public DataTable DtLstTiff
        {
            get { return FetchDataTable(); }
        }
        private DataTable FetchDataTable()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            dt.Columns.Add("ApplicationNo", typeof(string));
            dt.Columns.Add("Flag", typeof(int));
            dt.Columns.Add("Msg", typeof(string));
            for (int i = 0; i < LstTiff.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = LstTiff[i].ApplicationNo;
                dr[1] = LstTiff[i].Flag;
                dr[2] = LstTiff[i].Message;
                dt.Rows.Add(dr);
            }
            return dt;
        }
       
    }
    public class ListTIff_MER
    {
        public List<TIF> LstTiff_MER { get; set; }

        //For MER
        public DataTable DtLstTiff_MER
        {
            get { return FetchDataTable_MER(); }
        }
        private DataTable FetchDataTable_MER()
        {
            DataTable dt = new DataTable();
            dt = new DataTable();
            dt.Columns.Add("ApplicationNo", typeof(string));
            dt.Columns.Add("Flag", typeof(int));
            dt.Columns.Add("Msg", typeof(string));
            for (int i = 0; i < LstTiff_MER.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = LstTiff_MER[i].ApplicationNo;
                dr[1] = LstTiff_MER[i].Flag;
                dr[2] = LstTiff_MER[i].Message;
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
    /*ID:4 END*/
    /*ID:5 START*/
    public class TPARegisteration
    {
        private string strUserName;

        public string UserName
        {
            get { return strUserName; }
            set { strUserName = value; }
        }

        private string strPassword;

        public string Password
        {
            get { return strPassword; }
            set { strPassword = value; }
        }

        private string strProposalDate;

        public string ProposalDate
        {
            get { return strProposalDate.Substring(0, 10); }
            set { strProposalDate = value; }
        }

        private string strProposalNo;

        public string ProposalNo
        {
            get { return strProposalNo; }
            set { strProposalNo = value; }
        }

        private string strMasterPolicyNo;

        public string MasterPolicyNo
        {
            get { return strMasterPolicyNo; }
            set { strMasterPolicyNo = value; }
        }

        private string strProposerInitial;

        public string ProposerInitial
        {
            get { return strProposerInitial; }
            set { strProposerInitial = value; }
        }

        private string strProposerName;

        public string ProposerName
        {
            get { return strProposerName; }
            set { strProposerName = value; }
        }
        private string strGender;

        public string Gender
        {
            get { return strGender; }
            set { strGender = value; }
        }

        private string strTestConducted;

        public string TestConducted
        {
            get { return strTestConducted; }
            set { strTestConducted = value; }
        }

        private string strAddress1;

        public string Address1
        {
            get { return strAddress1; }
            set { strAddress1 = value; }
        }

        private string strAddress2;

        public string Address2
        {
            get { return strAddress2; }
            set { strAddress2 = value; }
        }
        private string strCity;

        public string City
        {
            get { return strCity; }
            set { strCity = value; }
        }
        private string strState;

        public string State
        {
            get { return strState; }
            set { strState = value; }
        }

        private string strDistrict;

        public string District
        {
            get { return strDistrict; }
            set { strDistrict = value; }
        }
        private string strTaluka;

        public string Taluka
        {
            get { return strTaluka; }
            set { strTaluka = value; }
        }
        private string strPincode;

        public string Pincode
        {
            get { return strPincode; }
            set { strPincode = value; }
        }
        private string strLandMark;

        public string LandMark
        {
            get { return strLandMark; }
            set { strLandMark = value; }
        }

        private string strTelephone;

        public string Telephone
        {
            get { return strTelephone; }
            set { strTelephone = value; }
        }
        private string strMobileNo;

        public string MobileNo
        {
            get { return strMobileNo; }
            set { strMobileNo = value; }
        }
        private string strHNIFlag;

        public string HNIFlag
        {
            get { return strHNIFlag; }
            set { strHNIFlag = value; }
        }
        private string strHomeVisitFlag;

        public string HomeVisitFlag
        {
            get { return strHomeVisitFlag; }
            set { strHomeVisitFlag = value; }
        }

        private string strAgentCode;

        public string AgentCode
        {
            get { return strAgentCode; }
            set { strAgentCode = value; }
        }

        private string strAgentName;

        public string AgentName
        {
            get { return strAgentName; }
            set { strAgentName = value; }
        }
        private string strAgentContactDetails;

        public string AgentContactDetails
        {
            get { return strAgentContactDetails; }
            set { strAgentContactDetails = value; }
        }

        private string strChannel;

        public string Channel
        {
            get { return strChannel; }
            set { strChannel = value; }
        }
        private string strBranchName;

        public string BranchName
        {
            get { return strBranchName; }
            set { strBranchName = value; }
        }
        private string strPlanType;

        public string PlanType
        {
            get { return strPlanType; }
            set { strPlanType = value; }
        }
        private string strProductName;

        public string ProductName
        {
            get { return strProductName; }
            set { strProductName = value; }
        }
        private string strMemberDOB;

        public string MemberDOB
        {
            get { return strMemberDOB.Substring(0, 10); }
            set { strMemberDOB = value; }
        }
        private string strCustomerEmail;

        public string CustomerEmail
        {
            get { return strCustomerEmail; }
            set { strCustomerEmail = value; }
        }

        private string strRemark;

        public string Remark
        {
            get { return strRemark; }
            set { strRemark = value; }
        }
        private string strPreferredDate;

        public string PreferredDate
        {
            get { return strPreferredDate; }
            set { strPreferredDate = value; }
        }
        private string strPreferredTime;

        public string PreferredTime
        {
            get { return strPreferredTime; }
            set { strPreferredTime = value; }
        }
        private string strPreferredProviderNo;

        public string PreferredProviderNo
        {
            get { return strPreferredProviderNo; }
            set { strPreferredProviderNo = value; }
        }
        private string strAge;

        public string Age
        {
            get { return strAge; }
            set { strAge = value; }
        }
        private string strRMContactNumber;

        public string RMContactNumber
        {
            get { return strRMContactNumber; }
            set { strRMContactNumber = value; }
        }
        private string strRMEmailId;

        public string RMEmailId
        {
            get { return strRMEmailId; }
            set { strRMEmailId = value; }
        }
        private string strIMDEmailId;

        public string IMDEmailId
        {
            get { return strIMDEmailId; }
            set { strIMDEmailId = value; }
        }
        private string strPaymentType;

        public string PaymentType
        {
            get { return strPaymentType; }
            set { strPaymentType = value; }
        }

        private string strStatus;

        public string Status
        {
            get { return strStatus; }
            set { strStatus = value; }
        }
        private string strAppilcantOfficeNumber;

        public string AppilcantOfficeNumber
        {
            get { return strAppilcantOfficeNumber; }
            set { strAppilcantOfficeNumber = value; }
        }
        private string strDCName;

        public string DCName
        {
            get { return strDCName; }
            set { strDCName = value; }
        }
        private string strProposalStatus;

        public string ProposalStatus
        {
            get { return strProposalStatus; }
            set { strProposalStatus = value; }
        }
        private string strCashieringDate;

        public string CashieringDate
        {
            get { return strCashieringDate; }
            set { strCashieringDate = value; }
        }
        private string strCashieredAmount;

        public string CashieredAmount
        {
            get { return strCashieredAmount; }
            set { strCashieredAmount = value; }
        }
        private string strPCName;

        public string PCName
        {
            get { return strPCName; }
            set { strPCName = value; }
        }
        private string strRegion;

        public string Region
        {
            get { return strRegion; }
            set { strRegion = value; }
        }
        private string strRepeatCase;

        public string RepeatCase
        {
            get { return strRepeatCase; }
            set { strRepeatCase = value; }
        }
        private string strTPACost;

        public string TPACost
        {
            get { return strTPACost; }
            set { strTPACost = value; }
        }
        private string strPreferredDCDetails;

        public string PreferredDCDetails
        {
            get { return strPreferredDCDetails; }
            set { strPreferredDCDetails = value; }
        }
        private string strResponse;

        public string Response
        {
            get { return strResponse; }
            set { strResponse = value; }
        }
        private string strRiskScore;

       

        //private string [] ArrResponse
        //{
        //    get { return strResponse.Split(','); }            
        //}


        public bool IsRequestSuccess
        {
            get { return IsResponseSuccess(); }
        }
        private string strControlNumber;

        public string ControlNumber
        {
            get { return strControlNumber; }
            set { strControlNumber = value; }
        }

        private bool IsResponseSuccess()
        {
            string[] ArrResponse = strResponse.Split(',');
            if (ArrResponse != null && ArrResponse.Length >= 3)
            {
                if (ArrResponse[2].Contains("Success") && ArrResponse[3].Contains("Saved Successfully!"))
                {
                    if (ArrResponse[0].Split(':').Length == 2)
                    {
                        ControlNumber = ArrResponse[0].Split(':')[1];
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private int intRegisterKey;

        public int RegisterKey
        {
            get { return intRegisterKey; }
            set { intRegisterKey = value; }
        }

        private string strRequest;

        public string Request
        {
            get { return strRequest; }
            set { strRequest = value; }
        }
    }

    public class TPARegisterationSummary
    {
        public List<TPARegisteration> LstTPARegisteration { set; get; }

        public DataTable DtTPARegisteration
        {
            get { return GetDataTableFromList(); }

        }

        private DataTable GetDataTableFromList()
        {
            DataTable dt = null;
            if (LstTPARegisteration != null && LstTPARegisteration.Count() > 0)
            {
                dt = new DataTable();
                dt.Columns.Add("RegisterKey", typeof(int));
                dt.Columns.Add("ApplicationNo", typeof(string));
                dt.Columns.Add("ControlNumber", typeof(string));
                dt.Columns.Add("Flag", typeof(bool));
                dt.Columns.Add("Response", typeof(string));
                dt.Columns.Add("Request", typeof(string));
                for (int i = 0; i < LstTPARegisteration.Count(); i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = LstTPARegisteration[i].RegisterKey;
                    dr[1] = LstTPARegisteration[i].ProposalNo;
                    dr[2] = LstTPARegisteration[i].ControlNumber;
                    dr[3] = LstTPARegisteration[i].IsRequestSuccess;
                    dr[4] = LstTPARegisteration[i].Response;
                    dr[5] = LstTPARegisteration[i].Request;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }

    }
    /*ID:5 END*/
    /*ID:6 START*/
    public class WarningMessage
    {
        Dictionary<string, string> dic = null;
        string strSpace = "<br>";
        public WarningMessage()
        {
            if (dic == null)
            {
                dic = new Dictionary<string, string>();
                /*ID:7 START*/
                DataSet _ds = new DataSet();
                int strParentID = 36;
                UWSaralDecision.CommFun objBussLayer = new UWSaralDecision.CommFun();
                objBussLayer.WarningMessage(ref _ds, strParentID);
                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    dic = _ds.Tables[0].AsEnumerable().ToDictionary<DataRow, string, string>(row => row.Field<string>(0), row => row.Field<string>(1));
                }
                /*ID:7 END*/
                /*
                dic.Add("1", "Sum Assured Is Greater Than Your Limit" + strSpace);
                dic.Add("2", "Nominee Exists Even Though LA And Proposer Are different" + strSpace);
                dic.Add("3", "Profile Investigation Mandatory" + strSpace);
                dic.Add("4", "CIBIL Score Mandatory" + strSpace);
                dic.Add("5", "Both Profile Investigation  & CIBIL Score Mandatory" + strSpace);
                dic.Add("6", "CIBIL Score Is Mandatory For Approval " + strSpace);
                dic.Add("7", "Call for MHR by BM" + strSpace);
                dic.Add("8", "Risk Parameter above 4, RM recommendation required to proceed" + strSpace);
                dic.Add("9", "Risk Score 75 or Above, HOD approval required  for issurance of the proposal" + strSpace);
                dic.Add("10", "You are declining the proposal with <75 Risk Score, please provide complete justification. Select follow up code INN and waive for future reference" + strSpace);
                dic.Add("11", "AU bank case, please get signoff of HOD for proceeding to Delcine" + strSpace);
                dic.Add("12", "No Income proof is required up to 50 lakh for Saraswat Co-op Bank" + strSpace);
                dic.Add("13", "6 months' bank statement can be considered as Standard Income proof for Saraswat co-op bank cases" + strSpace);
                dic.Add("14", "“Call for Medical for all cases/ Random risk verification/ Special MHR”" + strSpace);
                dic.Add("15", "No login without Branch Manager / BOE / CRO personally visiting the customer - MHR to be completed (Rajkot Branch manager joined, to be reviewed)" + strSpace);
                dic.Add("16", "No login Allowed (Barpeta to be reviewed)" + strSpace);
                dic.Add("17", "Photographs of the Life assured with SM and BM calling where photograph not possible" + strSpace);
                dic.Add("18", "Call For Random Medicals" + strSpace);
                dic.Add("19", "Physical Verification is mandatory for this HRP flagged case (Refer separate sheet also)" + strSpace);
                dic.Add("20", "Tele Verification is mandatory for this HRP flagged case  (Refer separate sheet also)" + strSpace);
                dic.Add("21", "FOR CURRENT EMP CASE, LA HAVE MULTIPLE EMP POLICY" + strSpace);
                dic.Add("22", "Payment only by cheque/DD (Cash Not allowed)|Bank statement showing Debit entry of DD or Cheque with pre-printed name|Bank statement with minimum 5 transactions|Occupation and Educational proof|Profile Investigation Mandatory|RM Check list mandatory" + strSpace);
                dic.Add("23", "OCL confirmation reprot by BM or equivelent, SM selfie with LA, MHR by SM or above" + strSpace);
                dic.Add("24", "OCL confirmation reprot by FDM or above, FDM selfie with LA, MHR by FDM or above" + strSpace);
                dic.Add("25", "OCL confirmation reprot by BDM or above" + strSpace);
                dic.Add("26", "Risk Parameter above 4, RM recommendation required to proceed" + strSpace);
                dic.Add("27", "Profile Investigation Mandatory (Prior approval of HOD for Robinhood cases)" + strSpace);
                dic.Add("28", "High Risk Score but eligible for Exclusion (ULIP Plan)" + strSpace);
                dic.Add("29", "High Risk Score but eligible for Exclusion (Single Premium)" + strSpace);
                dic.Add("30", "High Risk Score but eligible for Exclusion (Minor Life)" + strSpace);
                //dic.Add("", ""+strSpace);
                */
            }
        }

        public string SetMessage(string[] strRequest)
        {
            string strResponse = string.Empty;
            if (strRequest.Length > 0)
            {
                for (int i = 0; i < strRequest.Length; i++)
                {
                    strResponse = strResponse + dic[strRequest[i]]+strSpace;
                }
            }
            return strResponse;
        }
    }
    /*ID:6 END*/
    /*ID:8 START*/
    public class DashboardMedicalManagement
    {
        private string strApplicationNo;

        public string ApplicationNo
        {
            get { return strApplicationNo; }
            set { strApplicationNo = value; }
        }
        private string strPolicyNo;

        public string PolicyNo
        {
            get { return strPolicyNo; }
            set { strPolicyNo = value; }
        }

        private DateTime dtStartDate;

        public DateTime StartDate
        {
            get { return dtStartDate; }
            set { dtStartDate = value; }
        }
        private DateTime dtEndDate;

        public DateTime EndDate
        {
            get { return dtEndDate; }
            set { dtEndDate = value; }
        }

        private string strCategory;

        public string Category
        {
            get { return strCategory; }
            set { strCategory = value; }
        }
        private bool blnIsOnline;

        public bool IsOnline
        {
            get { return blnIsOnline; }
            set { blnIsOnline = value; }
        }

    }
    /*ID:8 END*/
    /*ID:1 START*/
    public class AdvLoadParam
    {
        public string strRiderCtg { get; set; }
        public string strMedicalLoadingClass { get; set; }
        public string strNonMedicalLoading { get; set; }
        public string strRiderCode { get; set; }
    }
    /*ID:1 END*/
}
