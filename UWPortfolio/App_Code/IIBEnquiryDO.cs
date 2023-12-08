using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IIBEnquiryDO
/// </summary>

    public class IIBEnquryCoustomerDO
    {
        public string ApplicationNo { get; set; }
        public string ClientType { get; set; }
        public string ClientID { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string PinCode { get; set; }
        public string PanNo { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ProductCode { get; set; }
        public string AnnualIncome { get; set; }



    }

    public class IIBMainEnquryResponseDO
    {

        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public bool IIBResponseLA { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string DecodedString1 { get; set; }
        public string Error { get; set; }
        public List<IIBEnquiryData> FGData { get; set; }
        public List<IIBEnquiryData> OtherData { get; set; }
    }

public class IIBEnquiryData
{
    public string QuestDBNo { get; set; }
    public string Input_Matching_Parameter { get; set; }
    public string Quest_DoP_DoC { get; set; }
    public string Quest_Sum_Assured { get; set; }
    public string Quest_Policy_Status { get; set; }
    public string Quest_Date_of_Exit { get; set; }
    public string Quest_Date_of_Death { get; set; }
    public string Quest_Cause_of_Death { get; set; }
    public string Quest_Record_last_updated { get; set; }
    public string Quest_Entity_Caution_Status { get; set; }
    public string Quest_Intermediary_Caution_Status { get; set; }
    public string Quest_Company_Number { get; set; }
    public string Is_Negative_Match { get; set; }
    public string LAProposerPayor { get; set; }

    //Added by kavita new column in IIB Service changes
    public string Product_TypeOutput { get; set; }
    public string Linked_Non_linked { get; set; }

    public string Medical_NonMedical { get; set; }
    public string Whether_Standard_Life { get; set; }
    public string Reason_for_Decline { get; set; }
    public string Reason_for_Postpone { get; set; }
    public string Reason_for_Repudiation { get; set; }
}

public class IIBEnquiryOtherData
{
    public string QuestDBNo { get; set; }
    public string Input_Matching_Parameter { get; set; }
    public string Quest_DoP_DoC { get; set; }
    public string Quest_Sum_Assured { get; set; }
    public string Quest_Policy_Status { get; set; }
    public string Quest_Date_of_Exit { get; set; }
    public string Quest_Date_of_Death { get; set; }
    public string Quest_Cause_of_Death { get; set; }
    public string Quest_Record_last_updated { get; set; }
    public string Quest_Entity_Caution_Status { get; set; }
    public string Quest_Intermediary_Caution_Status { get; set; }
    public string Quest_Company_Number { get; set; }
    public string Is_Negative_Match { get; set; }
    public string LAProposerPayor { get; set; }
   public string Product_TypeOutput { get; set; }
    public string Linked_Non_linked { get; set; }

    public string Medical_NonMedical { get; set; }
    public string Whether_Standard_Life { get; set; }
    public string Reason_for_Decline { get; set; }
    public string Reason_for_Postpone { get; set; }
    public string Reason_for_Repudiation { get; set; }
}
public class IIBEnquiryRequestDO
    {
        public string DataKeyName { get; set; }
        public string DataKeyValue { get; set; }
        public string ClientID { get; set; }
       public string FirstName { get; set; }
       public string SurName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string PinCode { get; set; }
        public string PanNo { get; set; }
        public string AadharCard { get; set; }
       public string Current_Address { get; set; }
        public string Permanent_Address { get; set; }
        public string Phone_Number_1 { get; set; }
        public string Phone_Number_2 { get; set; }
        public string Email_1 { get; set; }
        public string Email_2 { get; set; }
       public string ProductCode { get; set; }
        public string Annual_Income { get; set; }
        public string Cause_Of_Death { get; set; }



    }

