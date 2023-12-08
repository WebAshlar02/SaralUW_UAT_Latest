using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

/// <summary>
/// Summary description for ReplicaXml
/// </summary>
/// 

namespace UWSaralObjects
{
    public class ReplicaXml
    {
        public ApplicationSection ApplicationSection { get; set; }
        public BankSection BankSection { get; set; }
        public MandateSection MandateSection { get; set; }
        //public AmlSection strAmlSection { get; set; }
        public List<ProductSection> LstProductSection { get; set; }
        public List<RiderSection> LstRiderSection { get; set; }
        //public ReceiptSection[] LstReceiptSection { get; set; }
        //public RequirmentSection[] LstRequirmentSection { get; set; }
        public List<LoadingSection> LstLoadingSection { get; set; }
        public List<FundSection> LstFundSection { get; set; }
        public List<RequirmentSection> LstRequirementSection { get; set; }

        public List<RiderInfo> LstRiderInfo = new List<RiderInfo>();
    }
    public class MasterPageComparision
    {
        public List<ProductSection> LstProductSection { get; set; }
        public List<RiderSection> LstRiderSection { get; set; }
        public List<ClientSection> LstClientDetails { get; set; }
    }
    public class ApplicationSection
    {
        public string Section { get; set; }
        public string AppNo { get; set; }
        public string PolNo { get; set; }
        public string AppSignDate { get; set; }
        public string Channel { get; set; }
        public string AgentChannel { get; set; }
        public bool IsStaff { get; set; }
        public bool BACKDATEFLAG { get; set; }
        public string BACKDATE { get; set; }
        public string BACHDATEREASON { get; set; }
        public string PIVCSTATUS { get; set; }
        public string BACKDATEINTREST { get; set; }
        public string PIVCRJCREASON { get; set; }
        public string AUTOPAYTYPE { get; set; }
        private bool strAPP_isNSAP;

        public bool APP_isNSAP
        {
            get { return strAPP_isNSAP; }
            set { strAPP_isNSAP = value; }
        }
        
        public int SCRH_PIVC_STATUS { get; set; }
    }

    public class ClientSection
    {
        private string strClientName;

        public string ClientId
        {
            get { return strClientName; }
            set { strClientName = value; }
        }


        private string strRole;
        public string ClientRole
        {
            get { return strRole; }
            set { strRole = value; }
        }

    }
    public class AgentSection
    {
        public string AgentCode { get; set; }
        public string EmpCode { get; set; }
        public string PartnerCode { get; set; }
        public string CampaignCode { get; set; }
        //public string Agent_LeadcodeFlag { get; set; }
        public string LeadCode { get; set; }
        public string IsAgentVerified { get; set; }
    }

    public class ClntSection
    {
        public string Clnt_strClientID { get; set; }
        public string Clnt_strSalutation { get; set; }
        public string Clnt_strFirstname { get; set; }
        public string Clnt_strMiddlename { get; set; }
        public string Clnt_strLastname { get; set; }
        public string Clnt_strDataofBirth { get; set; }
        public string Clnt_strGender { get; set; }
        public string Clnt_strOccupation { get; set; }
        public string Clnt_strNationality { get; set; }
        public ClntAddressDetails[] Clnt_AddressType { get; set; }
    }


    public class ClntAddressDetails
    {
        public string ADR_addressType { get; set; }
        public string ADR_addressLine1_CLTADDR01 { get; set; }
        public string ADR_addressLine2_CLTADDR02 { get; set; }
        public string ADR_addressLine3_CLTADDR03 { get; set; }
        public string ADR_addressLine4_CLTADDR04 { get; set; }
        public string ADR_addressLine5_CLTADDR05 { get; set; }
        public string ADR_landmark { get; set; }
        public string ADR_pinCode_CLTPCODE { get; set; }
        public string ADR_city { get; set; }
        public string ADR_district { get; set; }
        public string ADR_state { get; set; }
        public string ADR_countryCode_CTRYCODE { get; set; }
        public string ADR_addressRemark { get; set; }
        public string ADR_phone1_CLTPHONE01 { get; set; }
        public string ADR_phone2_CLTPHONE02 { get; set; }
        public string ADR_didTel_RDIDTELNO { get; set; }
        public string ADR_mobileNo_MBLPHONE { get; set; }
        public string ADR_emailId_RINTERNET { get; set; }
        public string ADR_fax_FAXNO { get; set; }
    }

    public class BankSection
    {
        public string Section { get; set; }
        public string ClientName { get; set; }
        public string ClientType { get; set; }
        public string ClientNumber { get; set; }
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankAddress { get; set; }
        public string BankAccountNumber { get; set; }
        //public string Bnk_AutopayType { get; set; }
        //public string Bnk_Proposer { get; set; }
    }

    public class MandateSection
    {
        public string Section { get; set; }
        public string ACCOUNT_TYPE { get; set; }
        public string ACCOUNT_NUMBER { get; set; }
        public string ACCOUNT_HOLDER_NAME { get; set; }
        public string MANDATE_DATE { get; set; }
        public string CREDIT_CARD_NO { get; set; }
        public string CREDIT_CARD_TYPE { get; set; }
        public string VALID_DATE { get; set; }
        public string MICRO_CODE { get; set; }
        public string BANK_NAME { get; set; }
        public string BRANCH_NAME { get; set; }
        public string AUTO_PAY_TYPE { get; set; }


    }

    public class AmlSection
    {
        public string Aml_AgeProof { get; set; }
        public string Aml_photoId { get; set; }
        public string Aml_ProposerPhotoId { get; set; }
        public string Aml_ProposerAddressProof { get; set; }
        public string Aml_ProposerIncomeProof { get; set; }
    }

    public class ProductSection
    {
        public string CheckNull(string strInput)
        {
            return string.IsNullOrEmpty(strInput) ? string.Empty : strInput;
        }
        private string strSection;

        public string Section
        {
            get { return CheckNull(strSection); }
            set { strSection = value; }
        }

        private string strPolicyNo;

        public string PolicyNo
        {
            get { return CheckNull(strPolicyNo); }
            set { strPolicyNo = value; }
        }

        private string strProductCode;

        public string ProductCode
        {
            get { return CheckNull(strProductCode); }
            set { strProductCode = value; }
        }

        private string strProdcutName;

        public string ProdcutName
        {
            get { return CheckNull(strProdcutName); }
            set { strProdcutName = value; }
        }

        private string strPolicyTerm;

        public string PolicyTerm
        {
            get { return CheckNull(strPolicyTerm); }
            set { strPolicyTerm = value; }
        }

        private string strPremiumTerm;

        public string PremiumTerm
        {
            get { return CheckNull(strPremiumTerm); }
            set { strPremiumTerm = value; }
        }
        private string strSumAssured;

        public string SumAssured
        {
            get { return CheckNull(strSumAssured); }
            set { strSumAssured = value; }
        }

        private string strPremiumFreq;

        public string PremiumFreq
        {
            get { return CheckNull(strPremiumFreq); }
            set { strPremiumFreq = value; }
        }
        private string strBasePremium;

        public string BasePremium
        {
            get { return CheckNull(strBasePremium); }
            set { strBasePremium = value; }
        }

        private string strServiceTax;

        public string ServiceTax
        {
            get { return CheckNull(strServiceTax); }
            set { strServiceTax = value; }
        }
        private string strTotalPremium;

        public string TotalPremium
        {
            get { return CheckNull(strTotalPremium); }
            set { strTotalPremium = value; }
        }
        private string strProductType;

        public string ProductType
        {
            get { return CheckNull(strProductType); }
            set { strProductType = value; }
        }
        private string strMonthlyPayout;

        public string MonthlyPayout
        {
            get { return CheckNull(strMonthlyPayout); }
            set { strMonthlyPayout = value; }
        }

        private string strCategoryt;
        public string Category
        {
            get { return CheckNull(strCategoryt); }
            set { strCategoryt = value; }
        }
        private string strPayoutTerm;
        public string PayoutTerm
        {
            get { return CheckNull(strPayoutTerm); }
            set { strPayoutTerm = value; }
        }
        private string strPayoutType;
        public string PayoutType
        {
            get { return CheckNull(strPayoutType); }
            set { strPayoutType = value; }
        }
        private string strPayoutFreq;
        public string PayoutFreq
        {
            get { return CheckNull(strPayoutFreq); }
            set { strPayoutFreq = value; }
        }
        private string strLumpsumPer;
        public string LumpsumPer
        {
            get { return CheckNull(strLumpsumPer); }
            set { strLumpsumPer = value; }
        }
        private string strProductCatagory;
        public string ProductCategory
        {
            get { return CheckNull(strProductCatagory); }
            set { strProductCatagory = value; }
        }
    }

    public class FundSection
    {
        public string Section { get; set; }
        public string FND_fundCode { get; set; }
        public string FND_fundName { get; set; }
        public string FND_fundComposition { get; set; }
    }

    public class RequirmentSection
    {
        public string Section { get; set; }
        public string REQ_followUpCode { get; set; }
        public string REQ_description { get; set; }
        public string REQ_category { get; set; }
        public string REQ_criteria { get; set; }
        public string REQ_lifeType { get; set; }
        public string REQ_status { get; set; }

        public string REQ_RaisedDate { get; set; }
        public string REQ_RaisedBy { get; set; }
        public string REQ_ClosedDate { get; set; }
        public string REQ_ClosedBy { get; set; }
    }

    public class RiderSection
    {
        public string CheckNull(string strInput)
        {
            return string.IsNullOrEmpty(strInput) ? string.Empty : strInput;
        }
        private string strSection;

        public string Section
        {
            get { return strSection; }
            set { strSection = value; }
        }
        public string RIDERNAME { get; set; }
        public string RiderName
        {
            get
            {
                return CheckNull(RIDERNAME);
            }
            set
            {
                RIDERNAME
                    = value;
            }
        }
        public string RIDERCODE { get; set; }
        public string RiderCode
        {
            get
            {
                return CheckNull(RIDERCODE);
            }
            set
            {
                RIDERCODE = value;
            }
        }
        public string RIDERSUMASSURED { get; set; }
        public string RiderSumAssured
        {
            get
            {
                return CheckNull(RIDERSUMASSURED);
            }
            set
            {
                RIDERSUMASSURED = value;
            }
        }
        public string TOTLAPREMIUM { get; set; }
        public string RiderTotalPremium
        {
            get
            {
                return CheckNull(TOTLAPREMIUM);
            }
            set
            {
                TOTLAPREMIUM = value;
            }
        }
        public string SERVICETAX { get; set; }
        public string ServiceTax
        {
            get
            {
                return CheckNull(SERVICETAX);
            }
            set
            {
                SERVICETAX = value;
            }
        }
        public bool IsActive { get; set; }

        private string strApplicationNo;

        public string ApplicationNo
        {
            get { return strApplicationNo; }
            set { strApplicationNo = value; }
        }
        private string strProductType;

        public string ProductType
        {
            get { return strProductType; }
            set { strProductType = value; }
        }
        private string strProductCode;

        public string ProductCode
        {
            get { return strProductCode; }
            set { strProductCode = value; }
        }

        private string strProdPoliyTerm;

        public string ProdPoliyTerm
        {
            get { return strProdPoliyTerm; }
            set { strProdPoliyTerm = value; }
        }

        private string strProdPremPayingTerm;

        public string ProdPremPayingTerm
        {
            get { return strProdPremPayingTerm; }
            set { strProdPremPayingTerm = value; }
        }

        private string strProdSumAssured;

        public string ProdSumAssured
        {
            get { return strProdSumAssured; }
            set { strProdSumAssured = value; }
        }
    }

    public class ReceiptSection
    {
        public string RECEIPTNO { get; set; }
        public string RECEIPTSTATUS { get; set; }
        public bool ISRECEIPTCANCELED { get; set; }
        public string RECEIPTAMOUNT { get; set; }
        public string RECEIPTDATE { get; set; }
        public string RECEIPTMODE { get; set; }
        public string PAYMENTMODE { get; set; }
    }

    public class LoadingSection
    {
        public string Section { get; set; }
        public string ApplicationNo { get; set; }
        public string RiderName { get; set; }
        public string LaodingCode { get; set; }
        public string LoadingType { get; set; }
      //1.1 Begin of Changes; Bhaumik Patel - [CR-3334]
        public string Reason1 { get; set; }
        public string Reason2 { get; set; }
        public string Reason3 { get; set; }
        public string Reason4 { get; set; }
      //1.1 Begin of Changes; Bhaumik Patel - [CR-3334]
        public string LoadReasoncode1 { get; set; }
        public string LoadReasonCode2 { get; set; }
        public string Loading { get; set; }
        public string IsLetterPrint { get; set; }
        public string RateAdjustment { get; set; }
        public string FlatMortality { get; set; }
        public string RiderCode { get; set; }
        public string LoadingDiscp { get; set; }
        public string IsConsentRequired { get; set; }
        public string RiderCtg { get; set; }
    }

    public class CommentsSection
    {
        public string Comment_UserName { get; set; }
        public string Comment_category { get; set; }
        public string Comment_Remark { get; set; }
        public string Comment_userid { get; set; }
    }

    public class RiskParameter
    {
        public string BMI { get; set; }
    }
    public static class CompareTwoObjects
    {
        public static object CompareEquals<T>(this T objectFromCompare, T objectToCompare)
        {
            if (objectFromCompare == null && objectToCompare == null)
                return true;

            else if (objectFromCompare == null && objectToCompare != null)
                return false;

            else if (objectFromCompare != null && objectToCompare == null)
                return false;

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                object dataFromCompare =
                         objectFromCompare.GetType().GetProperty(prop.Name).GetValue(objectFromCompare, null);

                object dataToCompare =
                objectToCompare.GetType().GetProperty(prop.Name).GetValue(objectToCompare, null);

                Type type =
                objectFromCompare.GetType().GetProperty(prop.Name).GetValue(objectToCompare, null).GetType();

                if (prop.PropertyType.IsClass &&
                !prop.PropertyType.FullName.Contains("System.String"))
                {
                    dynamic convertedFromValue = Convert.ChangeType(dataFromCompare, type);
                    dynamic convertedToValue = Convert.ChangeType(dataToCompare, type);

                    object result = CompareTwoObjects.CompareEquals(convertedFromValue, convertedToValue);

                    bool compareResult = (bool)result;
                    if (!compareResult)
                        return false;
                }

                else if (!dataFromCompare.Equals(dataToCompare))
                    return false;
            }

            return true;
        }



        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);
                        //if (CompareTwoObjects.IsGenericList(self))
                        //{
                        //    CompareTwoObjects.PublicInstancePropertiesEqual<object>(self, to);                            
                        //}
                        //else
                        //{                            
                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                        //}                        
                    }
                }
                return true;
            }
            return self == to;
        }

        public static bool IsGenericList(this object o)
        {
            var oType = o.GetType();
            return (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)));
        }
        public static IList<string> GetDifferingProperties(object source, object target)
        {
            var sourceType = source.GetType();
            var sourceProperties = sourceType.GetProperties();
            var targetType = target.GetType();
            var targetProperties = targetType.GetProperties();

            var result = new List<string>();

            foreach (var property in
                (from s in sourceProperties
                 from t in targetProperties
                 where s.Name == t.Name &&
                 s.PropertyType == t.PropertyType &&
                 !Equals(s.GetValue(source, null), t.GetValue(target, null))
                 select new { Source = s, Target = t }))
            {
                // it's up to you to decide how primitive is primitive enough
                if (IsPrimitive(property.Source.PropertyType))
                {
                    result.Add(property.Source.Name);
                }
                else
                {
                    foreach (var subProperty in GetDifferingProperties(
                        property.Source.GetValue(source, null),
                        property.Target.GetValue(target, null)))
                    {
                        result.Add(property.Source.Name + "." + subProperty);
                    }
                }
            }

            return result;
        }

        private static bool IsPrimitive(Type type)
        {
            return type == typeof(string) || type == typeof(int);
        }


    }

}