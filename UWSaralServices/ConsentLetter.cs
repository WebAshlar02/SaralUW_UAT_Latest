using Platform.Utilities.LoggerFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UWSaralServices
{
    public class ConsentLetter
    {
        string strPartnerRequest = string.Empty;
        string strResultstring = string.Empty;        
        LAConsentLetter.Service1Client objInvoke = new LAConsentLetter.Service1Client();
        LAConsentLetter.Life_UWXRT_Traditional objTraditional = new LAConsentLetter.Life_UWXRT_Traditional();
        LAConsentLetter.Life_UWXRT_ULIP objUlip = new LAConsentLetter.Life_UWXRT_ULIP();
        LAConsentLetter.ComponentResults objCompnent = new LAConsentLetter.ComponentResults();
        // Array<LAConsentLetter.ComponentResults> lstComponentResults = new List<LAConsentLetter.ComponentResults>;
        LAConsentLetter.ComponentResults[] objCompnents = null;
        CommFun objcomm = new CommFun();
        string[] strQuestion = null;
        public void ConsentletterService(string strPQuoteNo, string ConsentType, DataSet _ds, ref string strResultstring)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start:CONSENT LETTER" + System.Environment.NewLine);
            //for (int j = 0; j < _ds.Tables[1].Rows.Count; j++)
            //{
            //    strQuestion[j] = _ds.Tables[1].Rows[j]["Questionaire"].ToString();
            //}
            try
            {

                if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0)
                {

                    if (_ds.Tables[0].Rows[0]["ProductType"].ToString().ToUpper().Equals("TD"))
                    {
                        #region TraditionalParameter
                        objTraditional.ListPlanDetail = new LAConsentLetter.PlanFlatMortalityDetail[_ds.Tables[0].Rows.Count];
                        objTraditional.ApplicationNo = _ds.Tables[0].Rows[0]["ApplicationNumber"].ToString();
                        objTraditional.Salutation = _ds.Tables[0].Rows[0]["Salutation"].ToString();
                        objTraditional.BalanceofPremium = _ds.Tables[0].Rows[0]["BalanceofPremium"].ToString();
                        objTraditional.BusinessDate = _ds.Tables[0].Rows[0]["BusinessDate"].ToString();
                        objTraditional.PHAddress1 = _ds.Tables[0].Rows[0]["PHAddress1"].ToString();
                        objTraditional.PHAddress2 = _ds.Tables[0].Rows[0]["PHAddress2"].ToString();
                        objTraditional.PHAddress3 = _ds.Tables[0].Rows[0]["PHAddress3"].ToString();
                        objTraditional.PHCity = _ds.Tables[0].Rows[0]["PHCity"].ToString();
                        objTraditional.PHCountry = _ds.Tables[0].Rows[0]["PHCountry"].ToString();
                        objTraditional.PHName = _ds.Tables[0].Rows[0]["PHName"].ToString();
                        objTraditional.PHPincode = _ds.Tables[0].Rows[0]["PHPincode"].ToString();
                        objTraditional.PHState = _ds.Tables[0].Rows[0]["PHState"].ToString();
                        objTraditional.PolicyTerm = _ds.Tables[0].Rows[0]["PolicyTerm"].ToString();
                        objTraditional.PremiumPayTerm = _ds.Tables[0].Rows[0]["PremiumPayTerm"].ToString();
                        objTraditional.ProductName = _ds.Tables[0].Rows[0]["ProductName"].ToString();
                        objTraditional.ReasonforRateUp = _ds.Tables[0].Rows[0]["ReasonforRateUp"].ToString();
                        objTraditional.RevisedPremium = _ds.Tables[0].Rows[0]["RevisedPremium"].ToString();
                        objTraditional.RiderPlan = _ds.Tables[0].Rows[0]["RiderPlan"].ToString();
                        objTraditional.SumAssured = _ds.Tables[0].Rows[0]["SumAssured"].ToString();
                        objTraditional.RiderSumAssured = _ds.Tables[0].Rows[0]["RiderSumAssured"].ToString();
                        objTraditional.Questionaries = strQuestion;                        
                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {
                            string strRateAdjust = string.IsNullOrEmpty(_ds.Tables[0].Rows[i]["RateAdjuster"].ToString()) ? "0" : _ds.Tables[0].Rows[i]["RateAdjuster"].ToString();
                            string strLoading = string.IsNullOrEmpty(_ds.Tables[0].Rows[i]["Loading"].ToString()) ? "0" : _ds.Tables[0].Rows[i]["Loading"].ToString();

                            objTraditional.ListPlanDetail[i] = new LAConsentLetter.PlanFlatMortalityDetail();
                            objTraditional.ListPlanDetail[i].PlanName = _ds.Tables[0].Rows[i]["PlanName"].ToString();
                            objTraditional.ListPlanDetail[i].RateAdjuster = Convert.ToDecimal(strRateAdjust);
                            objTraditional.ListPlanDetail[i].FlatMortality = _ds.Tables[0].Rows[i]["FlatMortality"].ToString();
                            objTraditional.ListPlanDetail[i].Loading = Convert.ToDecimal(strLoading);
                            //objTraditional.
                        }

                        #endregion
                    }
                    else if (_ds.Tables[0].Rows[0]["ProductType"].ToString().ToUpper().Equals("UL"))
                    {
                        #region ULIPParameter
                        objUlip.ListPlanDetail = new LAConsentLetter.PlanFlatMortalityDetail[_ds.Tables[0].Rows.Count];
                        objUlip.ProposalNo=objUlip.ApplicationNo = _ds.Tables[0].Rows[0]["ApplicationNumber"].ToString();
                        objUlip.Salutation = _ds.Tables[0].Rows[0]["Salutation"].ToString();
                        objUlip.BusinessDate = _ds.Tables[0].Rows[0]["BusinessDate"].ToString();
                        objUlip.PHAddress1 = _ds.Tables[0].Rows[0]["PHAddress1"].ToString();
                        objUlip.PHAddress2 = _ds.Tables[0].Rows[0]["PHAddress2"].ToString();
                        objUlip.PHAddress3 = _ds.Tables[0].Rows[0]["PHAddress3"].ToString();
                        objUlip.PHCity = _ds.Tables[0].Rows[0]["PHCity"].ToString();
                        objUlip.PHCountry = _ds.Tables[0].Rows[0]["PHCountry"].ToString();
                        objUlip.PHName = _ds.Tables[0].Rows[0]["PHName"].ToString();
                        objUlip.PHPincode = _ds.Tables[0].Rows[0]["PHPincode"].ToString();
                        objUlip.PHState = _ds.Tables[0].Rows[0]["PHState"].ToString();
                        objUlip.PolicyTerm = _ds.Tables[0].Rows[0]["PolicyTerm"].ToString();
                        objUlip.PremiumPayTerm = _ds.Tables[0].Rows[0]["PremiumPayTerm"].ToString();
                        objUlip.ProductName = _ds.Tables[0].Rows[0]["ProductName"].ToString();
                        objUlip.ReasonforRateUp = _ds.Tables[0].Rows[0]["ReasonforRateUp"].ToString();
                        objUlip.RiderPlan = _ds.Tables[0].Rows[0]["RiderPlan"].ToString();
                        objUlip.SumAssured = _ds.Tables[0].Rows[0]["SumAssured"].ToString();
                        objUlip.RiderSumAssured = _ds.Tables[0].Rows[0]["RiderSumAssured"].ToString();                        
                        objUlip.Questionaries = strQuestion;
                        for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                        {
                            string strRateAdjuster = string.IsNullOrEmpty(_ds.Tables[0].Rows[i]["RateAdjuster"].ToString()) ? "0" : _ds.Tables[0].Rows[i]["RateAdjuster"].ToString();
                            string strLoading = string.IsNullOrEmpty(_ds.Tables[0].Rows[i]["Loading"].ToString()) ? "0" : _ds.Tables[0].Rows[i]["Loading"].ToString();

                            objUlip.ListPlanDetail[i] = new LAConsentLetter.PlanFlatMortalityDetail();
                            objUlip.ListPlanDetail[i].PlanName = _ds.Tables[0].Rows[i]["PlanName"].ToString();
                            objUlip.ListPlanDetail[i].RateAdjuster = Convert.ToDecimal(strRateAdjuster);
                            objUlip.ListPlanDetail[i].FlatMortality = _ds.Tables[0].Rows[i]["FlatMortality"].ToString();
                            objUlip.ListPlanDetail[i].Loading = Convert.ToDecimal(strLoading);
                        }
                        #endregion ULIPParameter
                    }

                }

                if (_ds.Tables[2] != null && _ds.Tables[2].Rows.Count > 0)
                {
                    objCompnents = new LAConsentLetter.ComponentResults[_ds.Tables[2].Rows.Count];

                    for (int k = 0; k < _ds.Tables[2].Rows.Count; k++)
                    {
                        // objCompnents = new LAConsentLetter.ComponentResults[_ds.Tables[2].Rows.Count];
                        objCompnent = new LAConsentLetter.ComponentResults();
                        #region ComponentDetails
                        objCompnent.ComponentCd = _ds.Tables[2].Rows[k]["ComponentCd"].ToString();
                        objCompnent.EduCess = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["EduCess"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["EduCess"].ToString());
                        objCompnent.ExtraPremiumAmt = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["ExtraPremiumAmt"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["ExtraPremiumAmt"].ToString());
                        objCompnent.InstalmentPremiumAmt = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["InstalmentPremiumAmt"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["InstalmentPremiumAmt"].ToString());
                        objCompnent.LifeType = _ds.Tables[2].Rows[k]["LifeType"].ToString();
                        objCompnent.MedicalLoadingPremium = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["MedicalLoadingPremium"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["MedicalLoadingPremium"].ToString());
                        objCompnent.MedicalLoadingRate = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["MedicalLoadingRate"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["MedicalLoadingRate"].ToString());
                        objCompnent.ModalPremiumAmt = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["ModalPremiumAmt"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["ModalPremiumAmt"].ToString());
                        objCompnent.NonMedicalLoadingPremium = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["NonMedicalLoadingPremium"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["NonMedicalLoadingPremium"].ToString());
                        objCompnent.NonMedicalLoadingRate = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["NonMedicalLoadingRate"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["NonMedicalLoadingRate"].ToString());
                        //objCompnent[k].ComponentCd = _ds.Tables[1].Rows[k]["ComponentCd"].ToString();
                        //objCompnent[k].ComponentCd = _ds.Tables[1].Rows[k][""].ToString();
                        objCompnent.RiderCtg = _ds.Tables[2].Rows[k]["RiderCtg"].ToString();
                        objCompnent.RiderPPT = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["RiderPPT"].ToString()) ? 0 : Convert.ToInt32(_ds.Tables[2].Rows[k]["RiderPPT"].ToString());
                        objCompnent.RiderPT = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["RiderPT"].ToString()) ? 0 : Convert.ToInt32(_ds.Tables[2].Rows[k]["RiderPT"].ToString());
                        objCompnent.SeriveTax = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["SeriveTax"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["SeriveTax"].ToString());
                        objCompnent.SumAssured = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["SumAssured"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["SumAssured"].ToString());
                        objCompnent.SumAssuredAcrossPlans = string.IsNullOrEmpty(_ds.Tables[2].Rows[k]["SumAssuredAcrossPlans"].ToString()) ? 0.0 : Convert.ToDouble(_ds.Tables[2].Rows[k]["SumAssuredAcrossPlans"].ToString());
                        objCompnents[k] = objCompnent;
                        // objCompnents
                        #endregion ComponentDetails
                    }
                }
                string strTraditional = CommFun.GetXMLFromObject(objTraditional);
                string strComponent = CommFun.GetXMLFromObject(objCompnents);
                string strULIP = CommFun.GetXMLFromObject(objUlip);
                strPartnerRequest = strTraditional + strComponent+strULIP;
                if (_ds.Tables[0] != null && _ds.Tables[0].Rows.Count > 0 && objCompnents != null)
                {
                    if (_ds.Tables[0].Rows[0]["ProductType"].ToString().ToUpper().Equals("TD"))
                    {
                        strResultstring = objInvoke.Life_UWXRT_Traditional(objTraditional, objCompnents);

                        //objcomm.MaintainLog("CONCENTLETTER", "Life_UWXRT_Traditional", strPartnerRequest, strResultstring, string.Empty, string.Empty, "UWSaral", "UWSaral"                            , string.Empty, strPQuoteNo);
                    }
                    else if (_ds.Tables[0].Rows[0]["ProductType"].ToString().ToUpper().Equals("UL"))
                    {
                        strResultstring = objInvoke.Life_UWXRT_ULIP(objUlip, objCompnents);
                        //objcomm.MaintainLog("CONCENTLETTER", "Life_UWXRT_ULIP", strPartnerRequest, strResultstring, string.Empty, string.Empty, "UWSaral", "UWSaral"
                          //  , string.Empty, strPQuoteNo);
                    }
                }
            }
            catch(Exception Error)
            {
                CommFun objcomm = new CommFun();
               // objcomm.MaintainLog("CONCENTLETTER", "CONCENTLETTER", strPartnerRequest, string.Empty, string.Empty, string.Empty, "UWSaral", "UWSaral"                            , Error.Message, strPQuoteNo);
                //strLAPushErrorcode = -1;
                strResultstring = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :ConsentLetter.cs // MethodeName :ConsentletterService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "ConsentLetter.cs", "ConsentletterService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
    }

}
