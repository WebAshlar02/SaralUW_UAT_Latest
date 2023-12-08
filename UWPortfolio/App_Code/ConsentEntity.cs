using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ConsentEntity
/// </summary>
public class ConsentEntity
{
    public ConsentEntity()
    {
       
    }
    public class ConsentParams
    {
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal B_SumAssured { get; set; }
        public int B_PolicyTerm { get; set; }
        public int B_PremiumPaymentTerm { get; set; }
        public string B_PlanId { get; set; }
        public string Proposer { get; set; }
        public decimal R_SumAssured { get; set; }
        public int R_PolicyTerm { get; set; }
        public int R_PremiumPaymentTerm { get; set; }
        public decimal B_BasePremium { get; set; }
        public decimal b_LoadedPremium { get; set; }
        public decimal B_Tax { get; set; }
        public decimal B_TotalPayableAmount { get; set; }
        public decimal R_BasePremium { get; set; }
        public decimal R_LoadedPremium { get; set; }
        public decimal R_TotalTax { get; set; }
        public decimal R_TotalPayableAmount { get; set; }
        public int RiderPremiumPayment { get; set; }
        public string ConsentCreatedBy { get; set; }
        public string Consentupdatedby { get; set; }
        public bool IsBasePlan { get; set; }

        public string Guid { get; set; }

        public string Ridername { get; set; }
        public string RiderId { get; set; }
        public decimal B_TotalAmountPaidTillNow { get; set; }

        public decimal B_BalanceAmountPayable { get; set; }

        public string ConsentStatus { get; set; }
        public string EmaiILD { get; set; }
        public string MobileNo { get; set; }

    }

    public class EmailParams
    {
        public string Flag1 = "01";
        public string Flag2 = "02";
        public string Flag3 = "03";
        public string CommunicationType { get; set; }
        public string CommunicationKey { get; set; }
        public string Process { get; set; }
        public string TemplateId = "55";
        public string MailTo { get; set; }
        public string MailCC { get; set; }
        public string MobileNo { get; set; }
        public string Mode { get; set; }
        public string CreatedBy { get; set; }
        public string IsAttached { get; set; }
        public string AttachedFiles { get; set; }
        public string ApplicationNo { get; set; }
        public string ParameterList { get; set; }
        public string FileName { get; set; }


    }
}