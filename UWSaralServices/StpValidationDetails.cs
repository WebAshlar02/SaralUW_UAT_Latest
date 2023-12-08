using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
namespace UWSaralServices
{
    public class StpValidationDetails
    {
        #region variable Declaration Begins.
        string PQuoteNo = string.Empty;
        string QuoteNo = string.Empty;
        string ClientCode = string.Empty;
        string ProposerID = string.Empty;
        Decimal strTSAR = 0;
        string strReqresp_Error = string.Empty;
        string strRefNo = string.Empty;
        string strbpm_UserID = string.Empty;
        string strbpm_userName = string.Empty;
        string strbpm_branchCode = string.Empty;
        string strbpm_creationDate = string.Empty;
        string strbpm_systemDate = string.Empty;
        string strbpm_businessDate = string.Empty;
        string strbpm_userBranch = string.Empty;
        string strbpm_processName = string.Empty;
        string strbpm_applicationName = string.Empty;
        string strPayerID = string.Empty;
        string strlifeAsia_userID = string.Empty;
        string strSmsBody = string.Empty;
        string stremailBody = string.Empty;
        string stremailSub = string.Empty;
        string strReceiverName = string.Empty;
        string strMobileNo = string.Empty;
        string stremail = string.Empty;
        string ErrorCode = string.Empty;
        double TSARtsumatRisk;
        double FSARtsumatRisk;
        double MSARtsumatRisk;
        DataSet _dsSTPSMSEmail;
        DataSet _dsQuestinDtls;
        DataSet _dsProposerDetails;
        CommFun objcomm = new CommFun();
        #endregion variable Declaration End.

        string sRuleSetName = string.Empty;
        LAStpvalidationDetailsService.LifeRuleEngineClient obj = new LAStpvalidationDetailsService.LifeRuleEngineClient();
        LAStpvalidationDetailsService.PolicyEntity ePolicyentity = new LAStpvalidationDetailsService.PolicyEntity();
        LAStpvalidationDetailsService.PolicyHolderEntity epolicyholderEntity = new LAStpvalidationDetailsService.PolicyHolderEntity();
        LAStpvalidationDetailsService.PaymentEntity ePaymententity = new LAStpvalidationDetailsService.PaymentEntity();
        LAStpvalidationDetailsService.ProductEntity eProductEntity = new LAStpvalidationDetailsService.ProductEntity();
        LAStpvalidationDetailsService.SystemEntity eSystemEntity = new LAStpvalidationDetailsService.SystemEntity();
        LAStpvalidationDetailsService.ClientEntity eClientEntity = new LAStpvalidationDetailsService.ClientEntity();
        bool IsFundTransfer = false;
        bool IsPayorChange = false;

        public void StpPushService(string strPQuoteNo, ref DataSet _DsSTPpushRslt, DataSet _dsSTP, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            Logger.Info(strPQuoteNo + "*******Stp Validation Service  Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
            if (_dsSTP != null)
            {
                if (_dsSTP.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        Logger.Info(strPQuoteNo + "STAG 2 :PageName :StpValidationDetails.cs // MethodeName :StpPushService" + System.Environment.NewLine);

                        DataColumnCollection columns = _dsSTP.Tables[0].Columns;
                        if (columns.Contains(PQuoteNo))
                            PQuoteNo = _dsSTP.Tables[0].Rows[0]["PQuoteNo"].ToString();
                        //strRefNo = _dsSTP.Tables[0].Rows[0]["RefNo"].ToString();
                        QuoteNo = _dsSTP.Tables[0].Rows[0]["QuoteNo"].ToString();
                        ClientCode = _dsSTP.Tables[0].Rows[0]["ClientCode"].ToString();
                        ProposerID = _dsSTP.Tables[0].Rows[0]["ProposerID"].ToString();
                        sRuleSetName = _dsSTP.Tables[0].Rows[0]["sRuleSetName"].ToString();
                        strbpm_UserID = _dsSTP.Tables[0].Rows[0]["bpm_UserID"].ToString();
                        strbpm_userName = _dsSTP.Tables[0].Rows[0]["bpm_userName"].ToString();
                        strbpm_branchCode = _dsSTP.Tables[0].Rows[0]["bpm_branchCode"].ToString();
                        strbpm_creationDate = _dsSTP.Tables[0].Rows[0]["bpm_creationDate"].ToString();
                        strbpm_systemDate = _dsSTP.Tables[0].Rows[0]["bpm_systemDate"].ToString();
                        strbpm_businessDate = _dsSTP.Tables[0].Rows[0]["bpm_businessDate"].ToString();
                        strbpm_userBranch = _dsSTP.Tables[0].Rows[0]["bpm_userBranch"].ToString();
                        strbpm_processName = _dsSTP.Tables[0].Rows[0]["bpm_processName"].ToString();
                        strbpm_applicationName = _dsSTP.Tables[0].Rows[0]["bpm_applicationName"].ToString();
                        strlifeAsia_userID = _dsSTP.Tables[0].Rows[0]["lifeAsia_userID"].ToString();
                        //client ID
                        strPayerID = _dsSTP.Tables[0].Rows[0]["ProposerClientID"].ToString();

                        #region ePolicyentity begins

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["AnnualPremium"].ToString()))
                            ePolicyentity.AnnualPremium = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["AnnualPremium"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["ApplicationDate"].ToString()))
                            ePolicyentity.ApplicationDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["ApplicationDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["BackDatingFlag"].ToString()))
                            ePolicyentity.BackDatingFlag = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["BackDatingFlag"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["BackdatingRCD"].ToString()))
                            ePolicyentity.BackdatingRCD = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["BackdatingRCD"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["EODStatus"].ToString()))
                            ePolicyentity.EODStatus = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["EODStatus"].ToString());

                        #region FSAR Service Begins.
                        //objFSAR.callEngine(strRefNo, PQuoteNo, QuoteNo, ClientCode, strbpm_UserID, strbpm_userName, strbpm_branchCode, strbpm_creationDate, strbpm_systemDate, strbpm_businessDate, strbpm_userBranch, strbpm_processName, strbpm_applicationName, strlifeAsia_userID, ProposerID, ref TSARtsumatRisk, ref FSARtsumatRisk, ref MSARtsumatRisk);
                        //if (!string.IsNullOrEmpty(FSARtsumatRisk.ToString()))
                        //{
                        //    if (!string.IsNullOrEmpty(FSARtsumatRisk.ToString()))
                        //        ePolicyentity.FSAR = FSARtsumatRisk;
                        //}
                        ePolicyentity.FSAR = 0.0;
                        #endregion FSAR Service End.

                        ePolicyentity.InsuredPersonPanNumber = _dsSTP.Tables[0].Rows[0]["InsuredPersonPanNumber"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsPanAccepted"].ToString()))
                            ePolicyentity.IsPanAccepted = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsPanAccepted"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsSimplified"].ToString()))
                            ePolicyentity.IsSimplified = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsSimplified"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsStaff"].ToString()))
                            ePolicyentity.IsStaff = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsStaff"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["OFACStatus"].ToString()))
                            ePolicyentity.OFACStatus = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["OFACStatus"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PanApplied"].ToString()))
                            ePolicyentity.PanApplied = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["PanApplied"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PANNumber"].ToString()))
                            ePolicyentity.PANNumber = _dsSTP.Tables[0].Rows[0]["PANNumber"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PayerAnnualIncome"].ToString()))
                            ePolicyentity.PayerAnnualIncome = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["PayerAnnualIncome"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PayerDOB"].ToString()))
                            ePolicyentity.PayerDOB = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["PayerDOB"].ToString());

                        //if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PayerID"].ToString()))
                        //    ePolicyentity.PayerID = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["PayerID"].ToString());

                        //if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PayerID"].ToString()))
                        ePolicyentity.PayerID = Convert.ToInt32(strPayerID);

                        //if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PayerID"].ToString()))
                        //    ePolicyentity.PayerID = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["PayerID"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PolicyFrequency"].ToString()))
                            ePolicyentity.PolicyFrequency = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["PolicyFrequency"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PolicyHolderPanNumber"].ToString()))
                            ePolicyentity.PolicyHolderPanNumber = _dsSTP.Tables[0].Rows[0]["PolicyHolderPanNumber"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PolicyStatus"].ToString()))
                            ePolicyentity.PolicyStatus = _dsSTP.Tables[0].Rows[0]["PolicyStatus"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PolicyTerm"].ToString()))
                            ePolicyentity.PolicyTerm = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["PolicyTerm"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PolicyTerm"].ToString()))
                            ePolicyentity.PolicyTerm = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["PolicyTerm"].ToString());

                        //if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PremiumPayingTerm"].ToString()))
                        //   ePolicyentity.PremiumPayingTerm =Convert.(_dsSTP.Tables[0].Rows[0]["PremiumPayingTerm"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PremiumPaymentTerm"].ToString()))
                            ePolicyentity.PremiumPaymentTerm = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["PremiumPaymentTerm"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PremiumStatus"].ToString()))
                            ePolicyentity.PremiumStatus = _dsSTP.Tables[0].Rows[0]["PremiumStatus"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PrevPolicyPremiumAmount"].ToString()))
                            ePolicyentity.PrevPolicyPremiumAmount = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["PrevPolicyPremiumAmount"].ToString());

                        //if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["ProposerID"].ToString()))
                        //    ePolicyentity.ProposerID = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["ProposerID"].ToString());

                        if (!string.IsNullOrEmpty(strPayerID.ToString()))
                            ePolicyentity.ProposerID = Convert.ToInt32(strPayerID);



                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["RequiredRCD"].ToString()))
                            ePolicyentity.RequiredRCD = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["RequiredRCD"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["SimplifiedCheckFlag"].ToString()))
                            ePolicyentity.SimplifiedCheckFlag = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["SimplifiedCheckFlag"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["SpecialInsuranceKeyman"].ToString()))
                            ePolicyentity.SpecialInsuranceKeyman = _dsSTP.Tables[0].Rows[0]["SpecialInsuranceKeyman"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["SumAssured"].ToString()))
                            ePolicyentity.SumAssured = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["SumAssured"].ToString());

                        #region TSAR Service
                        //objTSAR.CallEngine(PQuoteNo, QuoteNo, ClientCode, ref strTSAR);
                        //if (!string.IsNullOrEmpty(strTSAR.ToString()))
                        //{
                        //    ePolicyentity.TSAR = Convert.ToDouble(strTSAR);
                        //}
                        ePolicyentity.TSAR = 0.0;
                        #endregion TSAR Service

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["UnderwriterCommentsFlag"].ToString()))
                            ePolicyentity.UnderwriterCommentsFlag = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["UnderwriterCommentsFlag"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["UserRole"].ToString()))
                            ePolicyentity.UserRole = _dsSTP.Tables[0].Rows[0]["UserRole"].ToString();

                        #endregion ePolicyentity End.

                        #region epolicyholderEntity Begins.

                        epolicyholderEntity.AddressProof = _dsSTP.Tables[0].Rows[0]["AddressProof"].ToString();
                        epolicyholderEntity.AgeProofDocCode = _dsSTP.Tables[0].Rows[0]["AgeProofDocCode"].ToString();
                        epolicyholderEntity.ApplicantType = _dsSTP.Tables[0].Rows[0]["ApplicantType"].ToString();
                        epolicyholderEntity.Qualification = _dsSTP.Tables[0].Rows[0]["Qualification"].ToString();
                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["BMI"].ToString()))
                            epolicyholderEntity.BMI = Convert.ToDecimal(_dsSTP.Tables[0].Rows[0]["BMI"].ToString());
                        epolicyholderEntity.Country = _dsSTP.Tables[0].Rows[0]["Country"].ToString();
                        epolicyholderEntity.CountryCode = _dsSTP.Tables[0].Rows[0]["AnnualPremium"].ToString();


                        if (_dsSTP.Tables[0] != null && _dsSTP.Tables[0].Rows.Count > 0)
                        {
                            epolicyholderEntity.DrugDetails = new LAStpvalidationDetailsService.DrugDetails[_dsSTP.Tables[0].Rows.Count];
                            for (int k = 0; k < _dsSTP.Tables[0].Rows.Count; k++)
                            {
                                epolicyholderEntity.DrugDetails[k] = new LAStpvalidationDetailsService.DrugDetails();
                                if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[k]["dbDrugQty"].ToString()))
                                    epolicyholderEntity.DrugDetails[k].dbDrugQty = Convert.ToDouble(_dsSTP.Tables[0].Rows[k]["dbDrugQty"].ToString());

                                if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[k]["DrugQty"].ToString()))
                                    epolicyholderEntity.DrugDetails[k].DrugQty = Convert.ToDouble(_dsSTP.Tables[0].Rows[k]["DrugQty"].ToString());

                                if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[k]["DrugType"].ToString()))
                                    epolicyholderEntity.DrugDetails[k].DrugType = _dsSTP.Tables[0].Rows[k]["DrugType"].ToString();
                            }
                        }




                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["HasAnyGynaecologicalProblem"].ToString()))
                            epolicyholderEntity.HasAnyGynaecologicalProblem = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["HasAnyGynaecologicalProblem"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["HealthQtnsResponse"].ToString()))
                            epolicyholderEntity.HealthQtnsResponse = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["HealthQtnsResponse"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IdentityProofIssueDate"].ToString()))
                            epolicyholderEntity.IdentityProofIssueDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["IdentityProofIssueDate"].ToString());

                        epolicyholderEntity.IdentityProofSubmitted = _dsSTP.Tables[0].Rows[0]["IdentityProofSubmitted"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["InsurabilityDeclaration"].ToString()))
                            epolicyholderEntity.InsurabilityDeclaration = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["InsurabilityDeclaration"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsAdventurous"].ToString()))
                            epolicyholderEntity.IsAdventurous = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsAdventurous"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsArmedPerson"].ToString()))
                            epolicyholderEntity.IsArmedPerson = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsArmedPerson"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsCriminallyConvicted"].ToString()))
                            epolicyholderEntity.IsCriminallyConvicted = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsCriminallyConvicted"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsNarchotic"].ToString()))
                            epolicyholderEntity.IsNarchotic = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsNarchotic"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsOccupationHazardous"].ToString()))
                            epolicyholderEntity.IsOccupationHazardous = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsOccupationHazardous"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsPoliticallyExposed"].ToString()))
                            epolicyholderEntity.IsPoliticallyExposed = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsPoliticallyExposed"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsPregnant"].ToString()))
                            epolicyholderEntity.IsPregnant = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsPregnant"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsRecentPhotoPasted"].ToString()))
                            epolicyholderEntity.IsRecentPhotoPasted = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsRecentPhotoPasted"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsRetiredPerson"].ToString()))
                            epolicyholderEntity.IsRetiredPerson = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsRetiredPerson"]);

                        epolicyholderEntity.LAType = _dsSTP.Tables[0].Rows[0]["LAType"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["LifeAssuredAnnualPremium"].ToString()))
                            epolicyholderEntity.LifeAssuredAnnualPremium = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["LifeAssuredAnnualPremium"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["LifeAssuredDOB"].ToString()))
                            epolicyholderEntity.LifeAssuredDOB = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["LifeAssuredDOB"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["LifeAssuredID"].ToString()))
                            epolicyholderEntity.LifeAssuredID = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["LifeAssuredID"]);

                        epolicyholderEntity.LifeAssuredOccupation = _dsSTP.Tables[0].Rows[0]["LifeAssuredOccupation"].ToString();

                        epolicyholderEntity.MandateRejectReason = _dsSTP.Tables[0].Rows[0]["MandateRejectReason"].ToString();

                        epolicyholderEntity.Nationality = _dsSTP.Tables[0].Rows[0]["Nationality"].ToString();

                        epolicyholderEntity.OccupationCode = _dsSTP.Tables[0].Rows[0]["OccupationCode"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["OccupationCodeHazardous"].ToString()))
                            epolicyholderEntity.OccupationCodeHazardous = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["OccupationCodeHazardous"]);


                        epolicyholderEntity.OtherPolicyStatusCode = new string[1];
                        for (int k = 0; k < 1; k++)
                        {
                            epolicyholderEntity.OtherPolicyStatusCode[k] = _dsSTP.Tables[0].Rows[0]["OtherPolicyStatusCode"].ToString();
                        }

                        epolicyholderEntity.PhotoIDProof = _dsSTP.Tables[0].Rows[0]["PhotoIDProof"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PolicyHolderDOB"].ToString()))
                            epolicyholderEntity.PolicyHolderDOB = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["PolicyHolderDOB"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["ProposerDOB"].ToString()))
                            epolicyholderEntity.ProposerDOB = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["ProposerDOB"].ToString());

                        epolicyholderEntity.RecentPhotograph = _dsSTP.Tables[0].Rows[0]["RecentPhotograph"].ToString();

                        epolicyholderEntity.RelationWithProposer = _dsSTP.Tables[0].Rows[0]["RelationWithProposer"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["Smoker"].ToString()))
                            epolicyholderEntity.Smoker = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["Smoker"].ToString());

                        epolicyholderEntity.SourceOfIncome = _dsSTP.Tables[0].Rows[0]["SourceOfIncome"].ToString();

                        epolicyholderEntity.SpecialClassCode = _dsSTP.Tables[0].Rows[0]["SpecialClassCode"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["hasWeightChanged"].ToString()))
                            epolicyholderEntity.hasWeightChanged = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["hasWeightChanged"]);

                        #endregion epolicyholderEntity End.

                        #region ePaymententity Begins.
                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["AmountReceived"].ToString()))
                            ePaymententity.AmountReceived = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["AmountReceived"].ToString());

                        LAStpvalidationDetailsService.CreditDebitCardEntity[] objCreditcard = new LAStpvalidationDetailsService.CreditDebitCardEntity[1];
                        for (int a = 0; a < 1; a++)
                        {
                            objCreditcard[a] = new LAStpvalidationDetailsService.CreditDebitCardEntity();
                            objCreditcard[a].CardNo = _dsSTP.Tables[0].Rows[0]["CardNo"].ToString();
                            objCreditcard[a].CardType = _dsSTP.Tables[0].Rows[0]["CardType"].ToString();
                            objCreditcard[a].OperationType = _dsSTP.Tables[0].Rows[0]["OperationType_Creditcard"].ToString();
                        }
                        //ePaymententity.CardDetails = objCreditcard;

                        LAStpvalidationDetailsService.ChequeEntity[] objCheckEntity = new LAStpvalidationDetailsService.ChequeEntity[1];
                        for (int b = 0; b < 1; b++)
                        {
                            objCheckEntity[b] = new LAStpvalidationDetailsService.ChequeEntity();
                            if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["Date_CheckEntity"].ToString()))
                                objCheckEntity[b].Date = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["Date_CheckEntity"].ToString());
                            objCheckEntity[b].OperationType = _dsSTP.Tables[0].Rows[0]["OperationType_CheckEntity"].ToString();
                        }
                        ePaymententity.ChequeDetails = objCheckEntity;
                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["CreditCardExpiryDate"].ToString()))
                            ePaymententity.CreditCardExpiryDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["CreditCardExpiryDate"].ToString());

                        ePaymententity.CreditCardNumber = new string[1];
                        for (int c = 0; c < 1; c++)
                        {
                            ePaymententity.CreditCardNumber[c] = _dsSTP.Tables[0].Rows[0]["CreditCardNumber"].ToString();
                        }

                        ePaymententity.CreditCardType = new string[1];
                        for (int d = 0; d < 1; d++)
                        {
                            ePaymententity.CreditCardType[d] = _dsSTP.Tables[0].Rows[0]["CreditCardType"].ToString();
                        }

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["DDCharge"].ToString()))
                            ePaymententity.DDCharge = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["DDCharge"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["DemandDraftAmount"].ToString()))
                            ePaymententity.DemandDraftAmount = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["DemandDraftAmount"].ToString());

                        LAStpvalidationDetailsService.DDEntity[] objddEntity = new LAStpvalidationDetailsService.DDEntity[1];
                        for (int e = 0; e < 1; e++)
                        {
                            objddEntity[e] = new LAStpvalidationDetailsService.DDEntity();
                            if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["Amount"].ToString()))
                                objddEntity[e].Amount = Convert.ToInt64(_dsSTP.Tables[0].Rows[0]["Amount"].ToString());

                            if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["Date_ddEntity"].ToString()))
                                objddEntity[e].Date = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["Date_ddEntity"].ToString());

                            objddEntity[e].OperationType = _dsSTP.Tables[0].Rows[0]["OperationType_DDEntity"].ToString();
                        }
                        ePaymententity.DemandDraftDetails = objddEntity;
                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["DestinationPolicyPremiumAmount"].ToString()))
                            ePaymententity.DestinationPolicyPremiumAmount = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["DestinationPolicyPremiumAmount"].ToString());
                        ePaymententity.DestinationPolicyStatus = _dsSTP.Tables[0].Rows[0]["DestinationPolicyStatus"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["InstallmentPremium"].ToString()))
                            ePaymententity.InstallmentPremium = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["InstallmentPremium"].ToString());
                        ePaymententity.InstrumentDate = new DateTime[1];
                        for (int f = 0; f < 1; f++)
                        {
                            ePaymententity.InstrumentDate[f] = new DateTime();
                            if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["InstrumentDate"].ToString()))
                                ePaymententity.InstrumentDate[f] = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["InstrumentDate"].ToString());
                        }
                        ePaymententity.InstrumentType = _dsSTP.Tables[0].Rows[0]["InstrumentType"].ToString();

                        ePaymententity.MICRCode = _dsSTP.Tables[0].Rows[0]["MICRCode"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["NetPremiumPayable"].ToString()))
                            ePaymententity.NetPremiumPayable = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["NetPremiumPayable"].ToString());

                        ePaymententity.OperationType = _dsSTP.Tables[0].Rows[0]["OperationType"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["OutstandingPremium"].ToString()))
                            ePaymententity.OutstandingPremium = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["OutstandingPremium"].ToString());

                        ePaymententity.PaymentOption = _dsSTP.Tables[0].Rows[0]["PaymentOption"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PremiumAmount"].ToString()))
                            ePaymententity.PremiumAmount = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["PremiumAmount"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["SourcePolicyFTAmount"].ToString()))
                            ePaymententity.SourcePolicyFTAmount = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["SourcePolicyFTAmount"].ToString());

                        ePaymententity.SourcePolicyStatus = _dsSTP.Tables[0].Rows[0]["SourcePolicyStatus"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["SuspenseAmount"].ToString()))
                            ePaymententity.SuspenseAmount = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["SuspenseAmount"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["TotalPayment"].ToString()))
                            ePaymententity.TotalPayment = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["TotalPayment"].ToString());

                        ePaymententity.TypeofReceipting = _dsSTP.Tables[0].Rows[0]["TypeofReceipting"].ToString();

                        #endregion ePaymententity End

                        #region eProductEntity begins.

                        eProductEntity.ProductType = _dsSTP.Tables[0].Rows[0]["ProductType"].ToString();
                        eProductEntity.ProductCode = _dsSTP.Tables[0].Rows[0]["ProductCode"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["Frequency"].ToString()))
                            eProductEntity.Frequency = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["Frequency"].ToString());

                        // ProductSubCategory to add in sql table
                        //eProductEntity.ProductSubCategory = _dsSTP.Tables[0].Rows[0]["ProductSubCategory"].ToString();
                        eProductEntity.AgentType = _dsSTP.Tables[0].Rows[0]["AgentType"].ToString();
                        eProductEntity.SpecifiedAgentType = _dsSTP.Tables[0].Rows[0]["SpecifiedAgentType"].ToString();

                        #endregion eProductEntity end.

                        #region eSystemEntity begins.
                        //if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["ApprovedDate"].ToString()))
                        //    eSystemEntity.ApprovedDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["ApprovedDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["BilledToDate"].ToString()))
                            eSystemEntity.BilledToDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["BilledToDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["BusinessDate"].ToString()))
                            eSystemEntity.BusinessDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["BusinessDate"]);


                        eSystemEntity.CoInsurance = _dsSTP.Tables[0].Rows[0]["CoInsurance"].ToString();


                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["CoverNoteDate"].ToString()))
                            eSystemEntity.CoverNoteDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["CoverNoteDate"].ToString());


                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["CoverNoteNumber"].ToString()))
                            eSystemEntity.CoverNoteNumber = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["CoverNoteNumber"].ToString());


                        eSystemEntity.FGSubChannel = _dsSTP.Tables[0].Rows[0]["FGSubChannel"].ToString();


                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["InwardDate"].ToString()))
                            eSystemEntity.InwardDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["InwardDate"].ToString());


                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["MISIntermediaryCode"].ToString()))
                            eSystemEntity.MISIntermediaryCode = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["MISIntermediaryCode"].ToString());


                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["OurShare"].ToString()))
                            eSystemEntity.OurShare = Convert.ToDecimal(_dsSTP.Tables[0].Rows[0]["OurShare"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PaidToDate"].ToString()))
                            eSystemEntity.PaidToDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["PaidToDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["QuoteNumber"].ToString()))
                            eSystemEntity.QuoteNumber = Convert.ToInt32(_dsSTP.Tables[0].Rows[0]["QuoteNumber"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["ReceivedDate"].ToString()))
                            eSystemEntity.ReceivedDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["ReceivedDate"].ToString());

                        eSystemEntity.Remarks = _dsSTP.Tables[0].Rows[0]["Remarks"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["RiskCommencementDate"].ToString()))
                            eSystemEntity.RiskCommencementDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["RiskCommencementDate"].ToString());

                        //if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["SystemDate"].ToString()))
                        //    eSystemEntity.SystemDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["SystemDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["TransactionDate"].ToString()))
                            eSystemEntity.TransactionDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["TransactionDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["TriggerDate"].ToString()))
                            eSystemEntity.TriggerDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["TriggerDate"].ToString());

                        #endregion eSystemEntity end.

                        #region eClientEntity Begins

                        eClientEntity.Address1 = _dsSTP.Tables[0].Rows[0]["Address1"].ToString();


                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["BusinessDate"].ToString()))
                            eClientEntity.BusinessDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["BusinessDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["ClientCode"].ToString()))
                            eClientEntity.ClientCode = Convert.ToInt64(_dsSTP.Tables[0].Rows[0]["ClientCode"].ToString());

                        eClientEntity.ClientName = _dsSTP.Tables[0].Rows[0]["ClientName"].ToString();
                        eClientEntity.ClientType = _dsSTP.Tables[0].Rows[0]["ClientType"].ToString();
                        eClientEntity.CompanyName = _dsSTP.Tables[0].Rows[0]["CompanyName"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["CompanyRegistrationDate"].ToString()))
                            eClientEntity.CompanyRegistrationDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["CompanyRegistrationDate"].ToString());

                        eClientEntity.Country = _dsSTP.Tables[0].Rows[0]["Country"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["CurrentFromDate"].ToString()))
                            eClientEntity.CurrentFromDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["CurrentFromDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["CurrentToDate"].ToString()))
                            eClientEntity.CurrentToDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["CurrentToDate"].ToString());

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["DateOfBirth"].ToString()))
                            eClientEntity.DateOfBirth = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["DateOfBirth"].ToString());
                        eClientEntity.EntityType = _dsSTP.Tables[0].Rows[0]["EntityType"].ToString();
                        eClientEntity.FirstName = _dsSTP.Tables[0].Rows[0]["FirstName"].ToString();
                        eClientEntity.Gender = _dsSTP.Tables[0].Rows[0]["Gender"].ToString();
                        eClientEntity.LastName = _dsSTP.Tables[0].Rows[0]["LastName"].ToString();
                        eClientEntity.MaritalStatus = _dsSTP.Tables[0].Rows[0]["MaritalStatus"].ToString();
                        eClientEntity.PANNumber = _dsSTP.Tables[0].Rows[0]["PANNumber"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["PinCode"].ToString()))
                            eClientEntity.PinCode = Convert.ToInt64(_dsSTP.Tables[0].Rows[0]["PinCode"].ToString());

                        eClientEntity.Salutation = _dsSTP.Tables[0].Rows[0]["Salutation"].ToString();

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["TransactionDate"].ToString()))
                            eClientEntity.TransactionDate = Convert.ToDateTime(_dsSTP.Tables[0].Rows[0]["TransactionDate"].ToString());

                        #endregion eClientEntity end.

                        #region eRiderEntity Begins.
                        LAStpvalidationDetailsService.RiderEntity[] eRiderEntity = new LAStpvalidationDetailsService.RiderEntity[1];
                        for (int j = 0; j < 1; j++)
                        {
                            eRiderEntity[j] = new LAStpvalidationDetailsService.RiderEntity();
                            eRiderEntity[j].RiderCode = _dsSTP.Tables[0].Rows[0]["RiderCode"].ToString();
                            eRiderEntity[j].RiderName = _dsSTP.Tables[0].Rows[0]["RiderName"].ToString();

                            if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["RiderSumAssured"].ToString()))
                                eRiderEntity[j].RiderSumAssured = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["RiderSumAssured"].ToString());

                            if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["NSAPFlag"].ToString()))
                                eRiderEntity[j].NSAPFlag = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["NSAPFlag"]);

                            if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["NonMedicalLoading"].ToString()))
                                eRiderEntity[j].NonMedicalLoading = Convert.ToDouble(_dsSTP.Tables[0].Rows[0]["NonMedicalLoading"].ToString());
                            eRiderEntity[j].MedicalLoadingClass = _dsSTP.Tables[0].Rows[0]["MedicalLoadingClass"].ToString();
                            eRiderEntity[j].LifeType = _dsSTP.Tables[0].Rows[0]["LifeType"].ToString();
                        }

                        #endregion eRiderEntity End.

                        #region other properties.
                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsFundTransfer"].ToString()))
                            IsFundTransfer = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsFundTransfer"]);

                        if (!string.IsNullOrEmpty(_dsSTP.Tables[0].Rows[0]["IsPayorChange"].ToString()))
                            IsPayorChange = Convert.ToBoolean(_dsSTP.Tables[0].Rows[0]["IsPayorChange"]);
                        #endregion other properties.


                        #region Esb Service call
                        LAStpvalidationDetailsService.BusinessRuleMessage[] objResponse = new LAStpvalidationDetailsService.BusinessRuleMessage[21];

                        objResponse = obj.ValidateSTP(sRuleSetName, ePolicyentity, epolicyholderEntity, ePaymententity, eProductEntity, eSystemEntity, eClientEntity, eRiderEntity, IsFundTransfer, IsPayorChange);
                        //context.Response.Write(xml);
                        #endregion Esb Service call

                        string strRejectReason = string.Empty;
                        string strStatus = string.Empty;
                        if (objResponse.Length > 0)
                        {
                            _DsSTPpushRslt = new DataSet();
                            _DsSTPpushRslt.Locale = CultureInfo.InvariantCulture;
                            DataTable sampleDataTable = _DsSTPpushRslt.Tables.Add("STPRESULT");
                            sampleDataTable.Columns.Add("RuleID", typeof(string));
                            sampleDataTable.Columns.Add("RejectReasonCode", typeof(string));
                            sampleDataTable.Columns.Add("SucessMessage", typeof(string));
                            DataRow sampleDataRow;
                            #region check STP status is failed or passed
                            for (int z = 0; z < objResponse.Length; z++)
                            {
                                // objResponse[z] = new BusinessRuleMessage();
                                sampleDataRow = sampleDataTable.NewRow();
                                strRejectReason = objResponse[z].RejectReasonCode;
                                if (!string.IsNullOrEmpty(strRejectReason))
                                {
                                    strStatus = "FAILED";
                                    sampleDataRow["RuleID"] = objResponse[z].RuleID;
                                    sampleDataRow["RejectReasonCode"] = objResponse[z].RejectReasonCode;
                                    sampleDataRow["SucessMessage"] = "FAILED";
                                    sampleDataTable.Rows.Add(sampleDataRow);
                                }
                                strLAPushErrorcode = 0;
                                strLAPushStatus = "Success";
                            }
                            #endregion check STP status is failed or passed
                            Logger.Error(strPQuoteNo + "STAG 2 :PageName :StpValidationDetails.cs // MethodeName :StpPushService Success");
                        }
                        else
                        {
                            Logger.Error(strPQuoteNo + "STAG 2 :PageName :StpValidationDetails.cs // MethodeName :StpPushService Failed");
                            objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "StpValidationDetails.cs", "StpPushService", "E-ServiceCallError", "", "", "No Records Found");
                        }

                        Logger.Info(strPQuoteNo + "*******Stp Validation Service  end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    catch (Exception Error)
                    {
                        strLAPushErrorcode = 1;
                        strLAPushStatus = "Failed";
                        Logger.Error(strPQuoteNo + "STAG 2 :PageName :StpValidationDetails.cs // MethodeName :StpPushService Error :" + System.Environment.NewLine + Error.StackTrace);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "StpValidationDetails.cs", "StpPushService", "E-ExceptionError", "", "", Error.ToString());

                        Logger.Info(strPQuoteNo + "*******Stp Validation Service end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                }
            }
            else
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "No Data";
                Logger.Info(strPQuoteNo + "*******Stp Validation Service end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "StpValidationDetails.cs", "StpPushService", "E-ServiceCallError", "", "", "No Records Found");

            }

        }
    }

}
