using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Models
/// </summary>
namespace UWSaralObjects
{
    public class grpUWModels
    {
        public grpUWModels()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }


    public class MemberInfo
    {
        public MemberInfo()
        {

        }

        private int intMemberId;
        public int MemberId
        {
            get { return intMemberId; }
            set { intMemberId = value; }
        }

        private string strMemberNo;
        public string MemberNo
        {
            get { return strMemberNo; }
            set { strMemberNo = value; }
        }

        private string strCustomerName;
        public string CustomerName
        {
            get { return strCustomerName; }
            set { strCustomerName = value; }
        }

        private string strSpouseName;
        public string SpouseName
        {
            get { return strSpouseName; }
            set { strSpouseName = value; }
        }

        private string strCompanyName;
        public string CompanyName
        {
            get { return strCompanyName; }
            set { strCompanyName = value; }
        }

        private DateTime dtDate;
        public DateTime Date
        {
            get { return dtDate; }
            set { dtDate = value; }
        }

        private string strDateOfBirth;
        public string DateOfBirth
        {
            get { return strDateOfBirth; }
            set { strDateOfBirth = value; }
        }

        private string strEffectiveDate;
        public string EffectiveDate
        {
            get { return strEffectiveDate; }
            set { strEffectiveDate = value; }
        }

        private int intPolicyNo;
        public int PolicyNo
        {
            get { return intPolicyNo; }
            set { intPolicyNo = value; }
        }

        private string strPolicyCode;
        public string PolicyCode
        {
            get { return strPolicyCode; }
            set { strPolicyCode = value; }
        }

        private string strSumAssured;
        public string SumAssured
        {
            get { return strSumAssured; }
            set { strSumAssured = value; }
        }

        private int intAging;
        public int Aging
        {
            get { return intAging; }
            set { intAging = value; }
        }

        private string strCurrentStage;
        public string CurrentStage
        {
            get { return strCurrentStage; }
            set { strCurrentStage = value; }
        }

        private int intUnderWriterId;
        public int UnderWriterId
        {
            get { return intUnderWriterId; }
            set { intUnderWriterId = value; }
        }

        private string strUwStatusCode;
        public string UwStatusCode
        {
            get { return strUwStatusCode; }
            set { strUwStatusCode = value; }
        }

        private int intUwStatus;
        public int UwStatus
        {
            get { return intUwStatus; }
            set { intUwStatus = value; }
        }

        private string strFreeCoverLimit;
        public string FreeCoverLimit
        {
            get { return strFreeCoverLimit; }
            set { strFreeCoverLimit = value; }
        }

        private string strReligareHousingFinance;
        public string ReligareHousingFinance
        {
            get { return strReligareHousingFinance; }
            set { strReligareHousingFinance = value; }
        }

        private string strApplicationNo;
        public string ApplicationNo
        {
            get { return strApplicationNo; }
            set { strApplicationNo = value; }
        }

        private string strGender;
        public string Gender
        {
            get { return strGender; }
            set { strGender = value; }
        }

        private string strMedicalCalledFor;
        public string MedicalCalledFor
        {
            get { return strMedicalCalledFor; }
            set { strMedicalCalledFor = value; }
        }

        private string strMedicalSumAssuredForLife;
        public string MedicalSumAssuredForLife
        {
            get { return strMedicalSumAssuredForLife; }
            set { strMedicalSumAssuredForLife = value; }
        }

        private int intRateUPByPremium;
        public int RateUPByPremium
        {
            get { return intRateUPByPremium; }
            set { intRateUPByPremium = value; }
        }

        private int intRateUPByTenure;
        public int RateUPByTenure
        {
            get { return intRateUPByTenure; }
            set { intRateUPByTenure = value; }
        }

        private int intPostponeByMonth;
        public int PostponeByMonth
        {
            get { return intPostponeByMonth; }
            set { intPostponeByMonth = value; }
        }

        private string strReviewedOn;
        public string ReviewedOn
        {
            get { return strReviewedOn; }
            set { strReviewedOn = value; }
        }
    }

    public class DashBoardDataFacts
    {
        public DashBoardDataFacts()
        {

        }

        private string strUwStatus;
        public string UwStatus
        {
            get { return strUwStatus; }
            set { strUwStatus = value; }
        }

        private string strUwStatusCode;
        public string UwStatusCode
        {
            get { return strUwStatusCode; }
            set { strUwStatusCode = value; }
        }

        private Int32 intNoOfCases;
        public Int32 NoOfCases
        {
            get { return intNoOfCases; }
            set { intNoOfCases = value; }
        }

        private Int32 intTotalCases;
        public Int32 TotalCases
        {
            get { return intTotalCases; }
            set { intTotalCases = value; }
        }
    }

    public class DdlData
    {
        public string DataText { get; set; }
        public string DataValue { get; set; }
    }

    //public class Med
    //{
    //    public Med()
    //    {

    //    }

    //    private bool boolIsChecked;

    //    public bool IsChecked
    //    {
    //        get { return boolIsChecked; }
    //        set { boolIsChecked = value; }
    //    }

    //    private string strtestName;

    //    public string testName
    //    {
    //        get { return strtestName; }
    //        set { strtestName = value; }
    //    }

    //    private string strtestRange;

    //    public string testRange
    //    {
    //        get { return strtestRange; }
    //        set { strtestRange = value; }
    //    }
    //    private string strtestValue;

    //    public string testValue
    //    {
    //        get { return strtestValue; }
    //        set { strtestValue = value; }
    //    }

    //    //private int intestDecision;

    //    //public int testDecision
    //    //{
    //    //    get { return strtestDecision; }
    //    //    set { strtestDecision = value; }
    //    //}

    //    private string strtestDecision;

    //    public string testDecision
    //    {
    //        get { return strtestDecision; }
    //        set { strtestDecision = value; }
    //    }
    //}
    //public class NonMed
    //{
    //    public NonMed()
    //    {

    //    }

    //    private bool boolIsChecked;

    //    public bool IsChecked
    //    {
    //        get { return boolIsChecked; }
    //        set { boolIsChecked = value; }
    //    }

    //    private string strnonMedDocName;

    //    public string nonMedDocName
    //    {
    //        get { return strnonMedDocName; }
    //        set { strnonMedDocName = value; }
    //    }

    //    private string strnonMedDocDecision;

    //    public string nonMedDocDecision
    //    {
    //        get { return strnonMedDocDecision; }
    //        set { strnonMedDocDecision = value; }
    //    }
    //}
    //public class other
    //{
    //    public other()
    //    {

    //    }

    //    private string strdoc_Type;

    //    public string doc_Type
    //    {
    //        get { return strdoc_Type; }
    //        set { strdoc_Type = value; }
    //    }

    //    private string strdoc_Name;

    //    public string doc_Name
    //    {
    //        get { return strdoc_Name; }
    //        set { strdoc_Name = value; }
    //    }
    //    private string struw_Comments;

    //    public string uw_Comments
    //    {
    //        get { return struw_Comments; }
    //        set { struw_Comments = value; }
    //    }
    //    private string strdecision;

    //    public string decision
    //    {
    //        get { return strdecision; }
    //        set { strdecision = value; }
    //    }
    //}

    public class Comments
    {
        public Comments()
        {

        }

        private int intUserKey;
        public int UserKey
        {
            get { return intUserKey; }
            set { intUserKey = value; }
        }

        private int intMemKey;
        public int MemKey
        {
            get { return intMemKey; }
            set { intMemKey = value; }
        }

        private string strUWComment;
        public string UWComment
        {
            get { return strUWComment; }
            set { strUWComment = value; }
        }

        private string strCMOComment;
        public string CMOComment
        {
            get { return strCMOComment; }
            set { strCMOComment = value; }

        }

        private string strHODComment;
        public string HODComment
        {
            get { return strHODComment; }
            set { strHODComment = value; }
        }

        private string strUWDecision;
        public string UWDecision
        {
            get { return strUWDecision; }
            set { strUWDecision = value; }
        }

        private int intUWStatus;
        public int UWStatus
        {
            get { return intUWStatus; }
            set { intUWStatus = value; }
        }

        private int intRateUPByPremium;
        public int RateUPByPremium
        {
            get { return intRateUPByPremium; }
            set { intRateUPByPremium = value; }
        }

        private int intRateUPByTenure;
        public int RateUPByTenure
        {
            get { return intRateUPByTenure; }
            set { intRateUPByTenure = value; }
        }

        private int intPostponeByMonth;
        public int PostponeByMonth
        {
            get { return intPostponeByMonth; }
            set { intPostponeByMonth = value; }
        }

        private string strUWDecisionRemarks;
        public string UWDecisionRemarks
        {
            get { return strUWDecisionRemarks; }
            set { strUWDecisionRemarks = value; }
        }

        private string strReviewedBy;
        public string ReviewedBy
        {
            get { return strReviewedBy; }
            set { strReviewedBy = value; }
        }

        private string strReviewedOn;
        public string ReviewedOn
        {
            get { return strReviewedOn; }
            set { strReviewedOn = value; }
        }

        private DateTime dtUWCommencementDate;
        public DateTime UWCommencementDate
        {
            get { return dtUWCommencementDate; }
            set { dtUWCommencementDate = value; }
        }

    }

    public class Uw_Remarks
    {
        public Uw_Remarks()
        {

        }

        private int intUserKey;
        public int UserKey
        {
            get { return intUserKey; }
            set { intUserKey = value; }
        }

        private int intMemKey;
        public int MemKey
        {
            get { return intMemKey; }
            set { intMemKey = value; }
        }

        private int intMrdKey;
        public int MrdKey
        {
            get { return intMrdKey; }
            set { intMrdKey = value; }
        }

        private int intReqTypeId;
        public int ReqTypeId
        {
            get { return intReqTypeId; }
            set { intReqTypeId = value; }
        }

        private string strReqType;
        public string ReqType
        {
            get { return strReqType; }
            set { strReqType = value; }
        }

        private int intReqNameId;
        public int ReqNameId
        {
            get { return intReqNameId; }
            set { intReqNameId = value; }
        }

        private string strReqName;
        public string ReqName
        {
            get { return strReqName; }
            set { strReqName = value; }
        }

        private int intStatus;
        public int Status
        {
            get { return intStatus; }
            set { intStatus = value; }
        }

        private string strRemarks;

        public string Remarks
        {
            get { return strRemarks; }
            set { strRemarks = value; }
        }

        //private int intRowNo;
        //public int RowNo
        //{
        //    get { return intRowNo; }
        //    set { intRowNo = value; }
        //}
    }

    public class FurtherReqErrorMsg
    {
        private int intRowIndex;
        public int RowIndex
        {
            get { return intRowIndex; }
            set { intRowIndex = value; }
        }
        private string strRemarksErrorMsg;
        public string RemarksErrorMsg
        {
            get { return strRemarksErrorMsg; }
            set { strRemarksErrorMsg = value; }
        }

        private string strRequirementErrorMsg;
        public string RequirementErrorMsg
        {
            get { return strRequirementErrorMsg; }
            set { strRequirementErrorMsg = value; }
        }

        private string strRequirementTypeErrorMsg;
        public string RequirementTypeErrorMsg
        {
            get { return strRequirementTypeErrorMsg; }
            set { strRequirementTypeErrorMsg = value; }
        }
    }
}
