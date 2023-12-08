//*********************************************************************                                                                                                              
//*                      FUTURE GENERALI INDIA                                                                                                                                      
//*********************************************************************/                                                                                                   
//*                  I N F O R M A T I O N                                                                                                                                   
//*********************************************************************  
// Sr. No.              : 1                                                                                                         
// Company              : Life                                                                                                          
// Module               : UWSARAL                                                                                                         
// Program Author       : AMIT CHAUDHARY [1119763]                                                                                                     
// BRD/CR/Codesk No/Win :                                                                                                       
// Date Of Creation     : 09.01.2018
// Description          : 1.1 for HNH product we have passed base product policy term and premium paying term to rider policy term and premium payeing term. 
//*********************************************************************   
//*********************************************************************                                                                                                              
//*                      FUTURE GENERALI INDIA                                                                                                                                      
//*********************************************************************/                                                                                                   
//*                  I N F O R M A T I O N                                                                                                                                   
//*********************************************************************  
// Sr. No.              : 1                                                                                                         
// Company              : Life                                                                                                          
// Module               : UWSARAL                                                                                                         
// Program Author       : AMIT CHAUDHARY [1119763]                                                                                                     
// BRD/CR/Codesk No/Win :                                                                                                       
// Date Of Creation     : 09.01.2018
// Description          : 1.2 payer party set to payer client id if payer and proposer are different.
//********************************************************************* 
//**********************************************************************
// Sr. No.              : 2
// Company              : Life            
// Module               : DEQC          
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win : CR-2596-RCD Logic to be changed
// Date Of Creation     : 20/05/2022
// Description          : RCD Logic to be changed
//*********************************//********************************************************************* 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using System.Globalization;
using UWSaralObjects;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace UWSaralServices
{
    public class ContractModification
    {
        int i;
        int _RJ;
        int _JLK;
        int _CL;
        int _RM;
        int _BN;
        int _SO;
        int _FP;
        int _RCQ;
        int count = 0;
        int EntityID = 1, polResult;
        string strApplicationNo = string.Empty;
        CommFun objcomm = new CommFun();
        DataRow[] Liferow;
        DataRow[] NomineeRow;
        DataRow[] ProposerRow;
        DataRow[] dtCoverage;
        DataRow[] JointLifeEntity;
        string strAppType = string.Empty;
        DataSet _dsRider = new DataSet();
        string strLAclientId = string.Empty;
        string strNomineeclientId = string.Empty;
        string strPayerClientId = string.Empty;
        string strProposerClientId = string.Empty;
        string strIsProposer = string.Empty;
        string strPolicyNo = string.Empty;
        string strdataValue = string.Empty;
        CommFun objComm = new CommFun();
        string strPolicyTerm = string.Empty;
        string strPremiumpayingterm = string.Empty;
        string strSumassured = string.Empty;
        string strPaymentfrequency = string.Empty;
        string strBasePremium = string.Empty;
        string strMonthlyPayout = string.Empty;
        string strTotalpremiumamount = string.Empty;
        string strServiceTaxBasePremium = string.Empty;

        string strAmountinsis = string.Empty;
        string strAppNo = string.Empty;
        string strRiskCommensmentdate = string.Empty;
        string strProdcode = string.Empty;
        string strProposerid = string.Empty;
        string RCDCommencementDate = string.Empty;

        public static object DateFormat(object objInput)
        {
            if (object.ReferenceEquals(objInput, DBNull.Value))
            {
                return null;
            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(objInput)))
                {
                    return null;
                }
                else
                {
                    //DateTime dt = DateTime.ParseExact(Convert.ToString(objInput), "mm-dd-yyyy", CultureInfo.InvariantCulture);
                    System.DateTime dt = Convert.ToDateTime(objInput);
                    objInput = dt.ToString("dd/MM/yyyy");
                    return objInput;
                }
            }
        }

        public void ContractModPushService(string strPQuoteNo, DataSet _dsContract, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus, string MWP) /*ID:2 Added MWP Flag by Dinesh Kondabattini*/
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: Contract Modification" + System.Environment.NewLine);
            try
            {
                Logger.Info(strPQuoteNo + "*******Contract Modification Start for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :ContractModification.CS // MethodeName :ContractModPushService" + System.Environment.NewLine);
                LAContractModService.ServiceClient objInvoke;
                LAContractModService.MasterEntityHeaderDetail objHeader;
                LAContractModService.MasterEntityOwnerDetail objOwner;
                LAContractModService.MasterEntityLifeDetailList[] objLifeDetails = null;
                LAContractModService.MasterEntityJointLifeDetailList[] objJoinLifeDetails = null;
                LAContractModService.MasterEntityJointOwnerDetail objJointOwner;
                LAContractModService.MasterEntityCoverageDetailList[] objCoverageList = null;
                LAContractModService.MasterEntityRiderDetailList[] objRider;
                // LAContractModService.MasterEntityRiderDetailList[] objRider = null;
                LAContractModService.MasterEntityBeneficiaryList[] objBeneficiary = null;
                LAContractModService.MasterEntityFollowUp objFollowup;
                LAContractModService.MasterEntityAssignee objAssignee;
                LAContractModService.MasterEntityDespatchCorrespondence objDespatch;
                LAContractModService.MasterEntityPayerDetail objPayer;
                LAContractModService.MasterEntitySpecialTermList[] objSpecialTerm = null;
                LAContractModService.MasterEntityFundDetailList[] objFundDetails;
                LAContractModService.MasterEntityClient objClient;
                LAContractModService.MasterEntityClientBankAccount objBankDetails;
                LAContractModService.MasterEntityMandate objMandate;
                LAContractModService.MasterEntityPremiumReceipt objReceipt;
                LAContractModService.MasterEntityReceiptToContractList[] objReptToCont = null;
                LAContractModService.MasterEntityAgencyDetail objAgencyDetails;
                LAContractModService.MasterEntityAnnuityDetail objAnnuityDetails;
                LAContractModService.MasterEntityGroupDetail objGroupDetails;
                LAContractModService.MasterEntityUserDetail objUserInfo;
                LAContractModService.Master objResponce;
                DataSet _ds = new DataSet();
                string strUserid = string.Empty;
                string _strOwnerClientID = string.Empty;
                _dsContract.Tables[0].TableName = "CONTOBJ";
                _dsContract.Tables[1].TableName = "CLNTDTLS";
                _dsContract.Tables[2].TableName = "FOLLOWUPDTLS";
                _dsContract.Tables[3].TableName = "FUNDDTLS";
                _dsContract.Tables[4].TableName = "RIDERDTLS";
                _dsContract.Tables[5].TableName = "RCPTDTLS";
                _dsContract.Tables[6].TableName = "MANDTLS";

                for (i = 0; i < _dsContract.Tables["CONTOBJ"].Rows.Count; i++)
                {
                    objInvoke = new LAContractModService.ServiceClient();
                    objHeader = new LAContractModService.MasterEntityHeaderDetail();
                    objOwner = new LAContractModService.MasterEntityOwnerDetail();
                    objLifeDetails = null;
                    objJoinLifeDetails = null;
                    objJointOwner = new LAContractModService.MasterEntityJointOwnerDetail();
                    objCoverageList = null;
                    objRider = new LAContractModService.MasterEntityRiderDetailList[0];
                    // LAContractModService.MasterEntityRiderDetailList[] objRider = null;
                    objBeneficiary = null;
                    objFollowup = new LAContractModService.MasterEntityFollowUp();
                    objAssignee = new LAContractModService.MasterEntityAssignee();
                    objDespatch = new LAContractModService.MasterEntityDespatchCorrespondence();
                    objPayer = new LAContractModService.MasterEntityPayerDetail();
                    objSpecialTerm = null;
                    objFundDetails = new LAContractModService.MasterEntityFundDetailList[0];
                    objClient = new LAContractModService.MasterEntityClient();
                    objBankDetails = new LAContractModService.MasterEntityClientBankAccount();
                    objMandate = new LAContractModService.MasterEntityMandate();
                    objReceipt = new LAContractModService.MasterEntityPremiumReceipt();
                    objReptToCont = null;
                    objAgencyDetails = new LAContractModService.MasterEntityAgencyDetail();
                    objAnnuityDetails = new LAContractModService.MasterEntityAnnuityDetail();
                    objGroupDetails = new LAContractModService.MasterEntityGroupDetail();
                    objUserInfo = new LAContractModService.MasterEntityUserDetail();
                    objResponce = new LAContractModService.Master();
                    #region Common feild Begins.
                    strApplicationNo = "'" + _dsContract.Tables["CONTOBJ"].Rows[i]["ApplicationNo"].ToString() + "'";
                    strIsProposer = _dsContract.Tables["CONTOBJ"].Rows[i]["IsProposer"].ToString();
                    strPolicyNo = _dsContract.Tables["CONTOBJ"].Rows[i]["contractNo"].ToString();
                    strAppType = _dsContract.Tables["CONTOBJ"].Rows[i]["AppType"].ToString();
                    strProdcode = _dsContract.Tables["CONTOBJ"].Rows[i]["contractType"].ToString();
                    objcomm.OnlineBussinessDate_GET(ref _ds, strPQuoteNo, strAppType);
                    strRiskCommensmentdate = DateFormat(_ds.Tables[0].Rows[0][0]).ToString();

                    if (objChangeObj.App_backdate != null)
                    {
                        strdataValue = objChangeObj.App_backdate._Backdate;
                    }
                    if (objChangeObj.Prod_policydetails != null)
                    {
                        if (objChangeObj.Prod_policydetails._ProdcodeBase == strProdcode)
                        {
                            strPolicyTerm = objChangeObj.Prod_policydetails._PolicyTerm;
                            strPremiumpayingterm = objChangeObj.Prod_policydetails._Premiumpayingterm;
                            strSumassured = objChangeObj.Prod_policydetails._Sumassured;
                            strTotalpremiumamount = objChangeObj.Prod_policydetails._TotalPremiumamount;
                            strBasePremium = objChangeObj.Prod_policydetails._Basepremiumamount;
                            strServiceTaxBasePremium = objChangeObj.Prod_policydetails._ServiceTax;
                            strMonthlyPayout = objChangeObj.Prod_policydetails._MonthlyPayoutBase;
                        }
                        else
                        {
                            strPolicyTerm = objChangeObj.Prod_policydetails._PolicyTermCombo;
                            strPremiumpayingterm = objChangeObj.Prod_policydetails._PremiumpayingtermCombo;
                            strSumassured = objChangeObj.Prod_policydetails._SumassuredCombo;
                            strTotalpremiumamount = objChangeObj.Prod_policydetails._TotalPremiumamountCombo;
                            strBasePremium = objChangeObj.Prod_policydetails._BasepremiumamountCombo;
                            strServiceTaxBasePremium = objChangeObj.Prod_policydetails._ServiceTaxCombo;
                            strMonthlyPayout = objChangeObj.Prod_policydetails._MonthlyPayoutCombo;
                        }
                        strPaymentfrequency = objChangeObj.Prod_policydetails._Paymentfrequency;
                        strAmountinsis = objChangeObj.Prod_policydetails._Amountinsis;
                    }

                    if (_dsContract.Tables["CLNTDTLS"].Rows.Count > 0)
                    {

                        /*ID:2 START by Dinesh Kondabattini*/
                        //NomineeRow = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='Nominee' or relationshipToLifeAssured='Appointee'");
                        NomineeRow = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='Trustee' or relationshipToLifeAssured='Nominee' or relationshipToLifeAssured='Appointee'"); /*ID:2 Added Trustee condition by Dinesh Kondabattini*/
                        /*ID:2 END by Dinesh Kondabattini*/
                        foreach (DataRow row in NomineeRow)
                        {
                            strNomineeclientId = row["CLT_clientId_CLNTNUM"].ToString();
                        }
                    }

                    if (_dsContract.Tables["CLNTDTLS"].Rows.Count > 0)
                    {
                        Liferow = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='LA'");
                        foreach (DataRow row in Liferow)
                        {
                            strLAclientId = row["CLT_clientId_CLNTNUM"].ToString();
                        }
                    }

                    if (_dsContract.Tables["CLNTDTLS"].Rows.Count > 0)
                    {
                        Liferow = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='payer'");
                        foreach (DataRow row in Liferow)
                        {
                            strPayerClientId = row["CLT_clientId_CLNTNUM"].ToString();
                        }
                    }

                    if (_dsContract.Tables["CLNTDTLS"].Rows.Count > 0)
                    {
                        Liferow = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='proposer'");
                        foreach (DataRow row in Liferow)
                        {
                            strProposerClientId = row["CLT_clientId_CLNTNUM"].ToString();
                        }
                    }

                    if (strIsProposer == "1")
                    {
                        strProposerid = _strOwnerClientID = strLAclientId;

                    }
                    else if (strIsProposer == "0")
                    {
                        strProposerid = _strOwnerClientID = strNomineeclientId;
                    }
                    else if (strPayerClientId == strProposerClientId && strIsProposer == "2")
                    {
                        _strOwnerClientID = strProposerClientId;
                        strProposerid = strProposerClientId;
                    }
                    else if (strPayerClientId != strProposerClientId && strIsProposer == "2")
                    {
                        _strOwnerClientID = strPayerClientId;
                        strProposerid = strProposerClientId;
                    }
                    else
                    {
                        _strOwnerClientID = strProposerClientId;
                        strProposerid = strProposerClientId;
                    }


                    #endregion common feild end.

                    #region MasterEntityHeaderDetail Begins.
                    objHeader.accountType = _dsContract.Tables["CONTOBJ"].Rows[i]["accountType"].ToString();
                    objHeader.agentNo = _dsContract.Tables["CONTOBJ"].Rows[i]["agentNo"].ToString();
                    objHeader.billCD_IND = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[i]["billCD"].ToString());
                    objHeader.billCurrency = _dsContract.Tables["CONTOBJ"].Rows[i]["billCurr"].ToString();
                    objHeader.billFrequency = (string.IsNullOrEmpty(strPaymentfrequency)) ? _dsContract.Tables["CONTOBJ"].Rows[i]["billFrequency"].ToString() : strPaymentfrequency;
                    objHeader.billRenewalDate = DateFormat(_dsContract.Tables["CONTOBJ"].Rows[i]["billRenewalDate"]).ToString();
                    objHeader.contractCurrency = _dsContract.Tables["CONTOBJ"].Rows[i]["contractCurrency"].ToString();
                    objHeader.contractNo = _dsContract.Tables["CONTOBJ"].Rows[i]["contractNo"].ToString();
                    objHeader.contractType = _dsContract.Tables["CONTOBJ"].Rows[i]["contractType"].ToString();
                    objHeader.methodOfPayment = _dsContract.Tables["CONTOBJ"].Rows[i]["methodOfPayment"].ToString();
                    objHeader.noOfPolicy = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[i]["noOfPolicy"].ToString());
                    objcomm.OnlineBussinessDate_GET(ref _ds, strPQuoteNo, strAppType);
                    //objHeader.originalCommencementDate = DateFormat(_dsContract.Tables["CONTOBJ"].Rows[i]["originalCommencementDate"]).ToString();
                    #region 2.1 Start:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
                    
                    if (!string.IsNullOrEmpty(strdataValue))
                    {
                        objHeader.originalCommencementDate=RCDCommencementDate=strdataValue.Replace("-", "/");
                    }
                    else
                    {
                        #region 2.1 Start:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
                        //objHeader.originalCommencementDate = strRiskCommensmentdate;
                        objHeader.originalCommencementDate=RCDCommencementDate = GetRCDDate(strPQuoteNo, Convert.ToDateTime(strRiskCommensmentdate),objChangeObj.userLoginDetails._UserID);
                        #endregion 2.1 End:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
                        //objHeader.originalCommencementDate = DateFormat(_dsContract.Tables["CONTOBJ"].Rows[i]["originalCommencementDate"]).ToString();
                    }



                    #endregion 2.1 End:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]

                    objHeader.proposalDate = DateFormat(_dsContract.Tables["CONTOBJ"].Rows[i]["proposalDate"]).ToString();
                    objHeader.proposalRecievedDate = DateFormat(_dsContract.Tables["CONTOBJ"].Rows[i]["proposalRecievedDate"]).ToString();
                    objHeader.register = _dsContract.Tables["CONTOBJ"].Rows[i]["register"].ToString();
                    objHeader.statisticalCodeA = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeA"].ToString();
                    objHeader.statisticalCodeB = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeB"].ToString();
                    objHeader.statisticalCodeC = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeC"].ToString();
                    objHeader.statisticalCodeD = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeD"].ToString();
                    objHeader.statisticalCodeE = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeE"].ToString();
                    objHeader.temporaryReceiptNo = _dsContract.Tables["CONTOBJ"].Rows[i]["temporaryReceiptNo"].ToString();
                    objHeader.UWDecisionDate = DateFormat(_dsContract.Tables["CONTOBJ"].Rows[i]["UWDecisionDate"]).ToString();
                    objHeader.userID = objChangeObj.userLoginDetails._UserID;
                    #endregion MasterEntityHeaderDetail End.

                    #region MasterEntityOwnerDetail Begins.
                    // Nominee is hardcode for the time .

                    //objOwner.ownerParty = _dsContract.Tables[1].Rows[i]["ownerParty"].ToString();
                    //objOwner.ownerParty = _strOwnerClientID;
                    objOwner.ownerParty = strProposerid;

                    #endregion MasterEntityOwnerDetail End.

                    #region MasterEntityLifeDetailList Begins.
                    if (_dsContract.Tables["CLNTDTLS"].Rows.Count > 0)
                    {
                        DataRow[] LifeEntity = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='LA'");
                        objLifeDetails = new LAContractModService.MasterEntityLifeDetailList[LifeEntity.Length];
                        count = 0;
                        EntityID = 1;
                        foreach (DataRow LifeEntityRow in LifeEntity)
                        {
                            objLifeDetails[count] = new LAContractModService.MasterEntityLifeDetailList();
                            objLifeDetails[count].ANBAge = Convert.ToInt32(LifeEntityRow["ANBAge"].ToString());
                            objLifeDetails[count].dateOfBirth = DateFormat(LifeEntityRow["dateOfBirth"].ToString()).ToString();
                            objLifeDetails[count].jointLifeNo = LifeEntityRow["jointLifeNo"].ToString();
                            //objLifeDetails[count].lifeEntID = LifeEntityRow["lifeEntID"].ToString();
                            objLifeDetails[count].lifeEntID = "L0" + EntityID;
                            objLifeDetails[count].lifeNo = LifeEntityRow["lifeNo"].ToString();
                            objLifeDetails[count].lifeParty = LifeEntityRow["CLT_clientId_CLNTNUM"].ToString();
                            objLifeDetails[count].lifeSelction = LifeEntityRow["lifeSelction"].ToString();
                            objLifeDetails[count].occupationCode = LifeEntityRow["occupationCode"].ToString();
                            objLifeDetails[count].relationshipToLifeAssured = LifeEntityRow["relationshipToLifeAssured"].ToString();
                            objLifeDetails[count].sex = LifeEntityRow["sex"].ToString();
                            objLifeDetails[count].smokingIndicator = LifeEntityRow["smokingIndicator"].ToString();
                            EntityID++;
                            count++;
                        }
                    }

                    //for (_RJ = 0; _RJ < _dsContract.Tables["Life"].Rows.Count; _RJ++)
                    //{
                    //    objLifeDetails[_RJ].ANBAge = Convert.ToInt32(_dsContract.Tables["Life"].Rows[_RJ]["ANBAge"].ToString());
                    //    objLifeDetails[_RJ].dateOfBirth = _dsContract.Tables["Life"].Rows[_RJ]["dateOfBirth"].ToString();
                    //    objLifeDetails[_RJ].jointLifeNo = _dsContract.Tables["Life"].Rows[_RJ]["jointLifeNo"].ToString();
                    //    objLifeDetails[_RJ].lifeEntID = _dsContract.Tables["Life"].Rows[_RJ]["lifeEntID"].ToString();
                    //    objLifeDetails[_RJ].lifeNo = _dsContract.Tables["Life"].Rows[_RJ]["lifeNo"].ToString();
                    //    objLifeDetails[_RJ].lifeParty = _dsContract.Tables["Life"].Rows[_RJ]["lifeParty"].ToString();
                    //    objLifeDetails[_RJ].lifeSelction = _dsContract.Tables["Life"].Rows[_RJ]["lifeSelction"].ToString();
                    //    objLifeDetails[_RJ].occupationCode = _dsContract.Tables["Life"].Rows[_RJ]["occupationCode"].ToString();
                    //    objLifeDetails[_RJ].relationshipToLifeAssured = _dsContract.Tables["Life"].Rows[_RJ]["relationshipToLifeAssured"].ToString();
                    //    objLifeDetails[_RJ].sex = _dsContract.Tables["Life"].Rows[_RJ]["sex"].ToString();
                    //    objLifeDetails[_RJ].smokingIndicator = _dsContract.Tables["Life"].Rows[_RJ]["smokingIndicator"].ToString();
                    //}
                    #endregion MasterEntityLifeDetailList End.

                    #region MasterEntityJointLifeDetailList Begins.
                    // This class set to to null because we are not received this value in FGI
                    objJoinLifeDetails = new LAContractModService.MasterEntityJointLifeDetailList[0];
                    //if (_dsContract.Tables["CLNTDTLS"].Rows.Count > 0) {
                    //    JointLifeEntity = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='LA'");
                    //    objJoinLifeDetails = new LAContractModService.MasterEntityJointLifeDetailList[JointLifeEntity.Length];
                    //    count = 0;
                    //    EntityID = 1;
                    //    foreach (DataRow JointLifeEntityRow in JointLifeEntity)
                    //    {
                    //        objJoinLifeDetails[count] = new LAContractModService.MasterEntityJointLifeDetailList();
                    //        objJoinLifeDetails[count].ANBAge = Convert.ToInt32(JointLifeEntityRow["ANBAge"].ToString());
                    //        objJoinLifeDetails[count].dateOfBirth = JointLifeEntityRow["dateOfBirth"].ToString();
                    //        objJoinLifeDetails[count].jointLifeEntID = "J0" + EntityID;
                    //        //objJoinLifeDetails[count].jointLifeEntID = JointLifeEntityRow["lifeEntID"].ToString();
                    //        objJoinLifeDetails[count].jointLifeNo = JointLifeEntityRow["jointLifeNo"].ToString();
                    //        // objJoinLifeDetails[count].jointLifeParent = JointLifeEntityRow["jointLifeParent"].ToString();
                    //        objJoinLifeDetails[count].jointLifeParent = "L0" + EntityID;
                    //        objJoinLifeDetails[count].jointLifeParty = JointLifeEntityRow["CLT_clientId_CLNTNUM"].ToString();
                    //        objJoinLifeDetails[count].lifeNo = JointLifeEntityRow["lifeNo"].ToString();
                    //        objJoinLifeDetails[count].lifeSelection = JointLifeEntityRow["lifeSelction"].ToString();
                    //        objJoinLifeDetails[count].occupationCode = JointLifeEntityRow["occupationCode"].ToString();
                    //        objJoinLifeDetails[count].relationshipToLifeInsured = JointLifeEntityRow["relationshipToLifeAssured"].ToString();
                    //        objJoinLifeDetails[count].sex = JointLifeEntityRow["sex"].ToString();
                    //        objJoinLifeDetails[count].smokingIndicator = JointLifeEntityRow["smokingIndicator"].ToString();
                    //        count++;
                    //        EntityID++;
                    //    }
                    //}

                    //for (_JLK = 0; _JLK < _dsContract.Tables["joinLife"].Rows.Count; _JLK++)
                    //{
                    //    objJoinLifeDetails[_JLK].ANBAge = Convert.ToInt32(_dsContract.Tables["joinLife"].Rows[_JLK]["ANBAge"].ToString());
                    //    objJoinLifeDetails[_JLK].dateOfBirth = _dsContract.Tables["joinLife"].Rows[_JLK]["dateOfBirth"].ToString();
                    //    objJoinLifeDetails[_JLK].jointLifeEntID = _dsContract.Tables["joinLife"].Rows[_JLK]["jointLifeEntID"].ToString();
                    //    objJoinLifeDetails[_JLK].jointLifeNo = _dsContract.Tables["joinLife"].Rows[_JLK]["jointLifeNo"].ToString();
                    //    objJoinLifeDetails[_JLK].jointLifeParent = _dsContract.Tables["joinLife"].Rows[_JLK]["jointLifeParent"].ToString();
                    //    objJoinLifeDetails[_JLK].jointLifeParty = _dsContract.Tables["joinLife"].Rows[_JLK]["jointLifeParty"].ToString();
                    //    objJoinLifeDetails[_JLK].lifeNo = _dsContract.Tables["joinLife"].Rows[_JLK]["lifeNo"].ToString();
                    //    objJoinLifeDetails[_JLK].lifeSelection = _dsContract.Tables["joinLife"].Rows[_JLK]["lifeSelection"].ToString();
                    //    objJoinLifeDetails[_JLK].occupationCode = _dsContract.Tables["joinLife"].Rows[_JLK]["occupationCode"].ToString();
                    //    objJoinLifeDetails[_JLK].relationshipToLifeInsured = _dsContract.Tables["joinLife"].Rows[_JLK]["relationshipToLifeInsured"].ToString();
                    //    objJoinLifeDetails[_JLK].sex = _dsContract.Tables["joinLife"].Rows[_JLK]["sex"].ToString();
                    //    objJoinLifeDetails[_JLK].smokingIndicator = _dsContract.Tables["joinLife"].Rows[_JLK]["smokingIndicator"].ToString();
                    //}

                    #endregion MasterEntityJointLifeDetailList End.

                    #region MasterEntityJointOwnerDetail Begins.
                    // This class set to to null because we are not received this value in FGI
                    //objJointOwner.jointOwnerParty = _strOwnerClientID;
                    #endregion MasterEntityJointOwnerDetail end.

                    #region MasterEntityCoverageDetailList Begins.
                    if (_dsContract.Tables["CONTOBJ"].Rows.Count > 0)
                    {
                        count = 0;
                        EntityID = 1;

                        string CoverageNO = "ApplicationNo = " + strApplicationNo;
                        dtCoverage = _dsContract.Tables["CONTOBJ"].Select(CoverageNO);
                        objCoverageList = new LAContractModService.MasterEntityCoverageDetailList[dtCoverage.Length];
                        foreach (DataRow CoverageDtls in dtCoverage)
                        {
                            objCoverageList[count] = new LAContractModService.MasterEntityCoverageDetailList();
                            objCoverageList[count].coverageEndID = "C0" + EntityID;
                            //objCoverageList[_CL].coverageEndID = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageEndID"].ToString();
                            objCoverageList[count].coverageNo = CoverageDtls["coverageNo"].ToString();
                            //objCoverageList[_CL].coverageParent = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageParent"].ToString();
                            objCoverageList[count].coverageParent = "L0" + EntityID;
                            objCoverageList[count].coverageRiderTable = CoverageDtls["coverageRiderTable"].ToString();
                            objCoverageList[count].lifeNo = CoverageDtls["lifeNo"].ToString();
                            objCoverageList[count].lumpSumContribution = Convert.ToInt32(CoverageDtls["lumpSumContribution"].ToString());
                            objCoverageList[count].mortalityClass = CoverageDtls["mortalityClass"].ToString();
                            objCoverageList[count].premiumCessadtionAge = Convert.ToInt32(CoverageDtls["premiumCessadtionAge"].ToString());
                            objCoverageList[count].premiumCessadtionTerm = (string.IsNullOrEmpty(strPremiumpayingterm)) ? Convert.ToInt32(CoverageDtls["premiumCessadtionTerm"].ToString()) : Convert.ToInt32(strPremiumpayingterm);
                            objCoverageList[count].riderNo = _dsContract.Tables["CONTOBJ"].Rows[_CL]["riderNo"].ToString();
                            objCoverageList[count].riskCessationAge = Convert.ToInt32(CoverageDtls["riskCessationAge"].ToString());

                            objCoverageList[count].riskCessationTerm = (string.IsNullOrEmpty(strPolicyTerm)) ? Convert.ToInt32(CoverageDtls["riskCessationTerm"].ToString()) : Convert.ToInt32(strPolicyTerm);

                            objCoverageList[count].sumAssured = (string.IsNullOrEmpty(strSumassured)) ? Convert.ToInt32(CoverageDtls["sumAssured"]) : Convert.ToInt32(strSumassured);
                            objCoverageList[count].unitReserveAllocationDate = CoverageDtls["unitReserveAllocationDate"].ToString();
                            objCoverageList[count].unitReserveUnits = CoverageDtls["unitReserveUnits"].ToString();
                            objCoverageList[count].covRiderInstalmentAmount = (string.IsNullOrEmpty(strBasePremium) || strBasePremium == "0") ? Convert.ToInt32(CoverageDtls["covRiderInstalmentAmount"]) : Convert.ToInt32(strBasePremium);
                        }
                    }


                    //objCoverageList = new LAContractModService.MasterEntityCoverageDetailList[_dsContract.Tables["CONTOBJ"].Rows.Count];
                    //for (_CL = 0; _CL < _dsContract.Tables["CONTOBJ"].Rows.Count; _CL++)
                    //{

                    //    objCoverageList[_CL] = new LAContractModService.MasterEntityCoverageDetailList();
                    //    objCoverageList[_CL].coverageEndID = "C0" + EntityID;

                    //    objCoverageList[_CL].coverageNo = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageNo"].ToString();

                    //    objCoverageList[_CL].coverageParent = "L0" + EntityID;
                    //    objCoverageList[_CL].coverageRiderTable = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageRiderTable"].ToString();
                    //    objCoverageList[_CL].lifeNo = _dsContract.Tables["CONTOBJ"].Rows[_CL]["lifeNo"].ToString();
                    //    objCoverageList[_CL].lumpSumContribution = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["lumpSumContribution"].ToString());
                    //    objCoverageList[_CL].mortalityClass = _dsContract.Tables["CONTOBJ"].Rows[_CL]["mortalityClass"].ToString();
                    //    objCoverageList[_CL].premiumCessadtionAge = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["premiumCessadtionAge"].ToString());

                    //    objCoverageList[_CL].premiumCessadtionTerm = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["premiumCessadtionTerm"].ToString());

                    //    objCoverageList[_CL].riderNo = _dsContract.Tables["CONTOBJ"].Rows[_CL]["riderNo"].ToString();
                    //    objCoverageList[_CL].riskCessationAge = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["riskCessationAge"].ToString());
                    //    objCoverageList[_CL].riskCessationTerm = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["riskCessationTerm"].ToString());
                    //    objCoverageList[_CL].sumAssured = (string.IsNullOrEmpty(strSumassured)) ? Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["sumAssured"].ToString()) : Convert.ToInt32(strSumassured);


                    //    objCoverageList[_CL].unitReserveAllocationDate = _dsContract.Tables["CONTOBJ"].Rows[_CL]["unitReserveAllocationDate"].ToString();
                    //    objCoverageList[_CL].unitReserveUnits = _dsContract.Tables["CONTOBJ"].Rows[_CL]["unitReserveUnits"].ToString();
                    //    EntityID++;
                    //}

                    #endregion MasterEntityCoverageDetailList End.

                    #region MasterEntityRiderDetailList Begins.
                    strAppNo = _dsContract.Tables["CONTOBJ"].Rows[i]["ApplicationNo"].ToString();
                    if (strAppType.ToUpper() == "ONLINE")
                    {

                        objcomm.OnlineCRTUPDRiderDetails_GET(ref _dsRider, strAppNo, strAppType);
                        if (_dsRider != null && _dsRider.Tables[0].Rows.Count > 0)
                        {
                            DataTable _dt = _dsRider.Tables[0];
                            _dsContract.Tables[4].Reset();
                            _dsContract.Tables[4].Merge(_dt);
                        }
                        else
                        {
                            _dsContract.Tables[4].Clear();
                        }
                    }

                    if (_dsContract.Tables["RIDERDTLS"].Rows.Count > 0)
                    {
                        EntityID = 1;
                        objRider = new LAContractModService.MasterEntityRiderDetailList[_dsContract.Tables["RIDERDTLS"].Rows.Count];
                        for (_RM = 0; _RM < _dsContract.Tables["RIDERDTLS"].Rows.Count; _RM++)
                        {
                            objRider[_RM] = new LAContractModService.MasterEntityRiderDetailList();
                            objRider[_RM].coverageNo = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["coverageNo"].ToString();
                            objRider[_RM].coverageRiderTable = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["coverageRiderTable"].ToString();
                            objRider[_RM].lifeNo = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["lifeNo"].ToString();
                            objRider[_RM].mortalityClass = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["mortalityClass"].ToString();
                            objRider[_RM].premiumCessationAge = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationAge"].ToString());
                            //1.1 begin
                            if (strProdcode.Contains("H05") || strProdcode.Contains("H06") || strProdcode.Contains("H07") || strProdcode.Contains("H08"))
                            {
                                objRider[_RM].premiumCessationTerm = Convert.ToInt32(strPremiumpayingterm);
                            }
                            //1.1 end
                            if (_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationTerm"].ToString() != "")
                            {
                                //objRider[_RM].premiumCessationTerm = (string.IsNullOrEmpty(strPremiumpayingterm)) ? Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationTerm"].ToString()) : Convert.ToInt32(strPremiumpayingterm);
                                //objRider[_RM].premiumCessationTerm = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationTerm"].ToString());

                                objRider[_RM].premiumCessationTerm = (string.IsNullOrEmpty(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationTerm"].ToString())) ? Convert.ToInt32(strPremiumpayingterm) : Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationTerm"].ToString());

                            }
                            objRider[_RM].riderEntID = "R0" + EntityID;
                            //objRider[_RM].riderEntID = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderEntID"].ToString();
                            objRider[_RM].riderNo = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderNo"].ToString();
                            //objRider[_RM].riderParent = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderParent"].ToString();
                            objRider[_RM].riderParent = "C01";// +EntityID; ;

                            objRider[_RM].riskCessationAge = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationAge"].ToString());
                            //1.1 begin
                            if (strProdcode.Contains("H05") || strProdcode.Contains("H06") || strProdcode.Contains("H07") || strProdcode.Contains("H08"))
                            {
                                objRider[_RM].riskCessationTerm = Convert.ToInt32(strPremiumpayingterm);
                            }
                            //1.1 begin
                            if (_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationTerm"].ToString() != "")
                            {
                                //objRider[_RM].riskCessationTerm = (string.IsNullOrEmpty(strPolicyTerm)) ? Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationTerm"].ToString()) : Convert.ToInt32(strPolicyTerm);
                                objRider[_RM].riskCessationTerm = (string.IsNullOrEmpty(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationTerm"].ToString())) ? Convert.ToInt32(strPolicyTerm) : Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationTerm"].ToString());

                            }
                            objRider[_RM].sumAssured = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["sumAssured"].ToString());
                            EntityID++;
                        }
                    }
                    else
                    {
                        objRider = new LAContractModService.MasterEntityRiderDetailList[0];
                    }
                    //else -- this part is commented as space and default value are handle in service after discussion with dharmesh.
                    //{
                    //    objRider = new LAContractModService.MasterEntityRiderDetailList[10];
                    //    for (_RM = 0; _RM < 10; _RM++)
                    //    {

                    //        objRider[_RM] = new LAContractModService.MasterEntityRiderDetailList();
                    //        objRider[_RM].coverageNo = " ";
                    //        objRider[_RM].coverageRiderTable = " ";
                    //        objRider[_RM].lifeNo = " ";
                    //        objRider[_RM].mortalityClass = " ";
                    //        objRider[_RM].premiumCessationAge = 0;
                    //        objRider[_RM].premiumCessationTerm = 0;
                    //        objRider[_RM].riderEntID = "R0" + EntityID;
                    //        //objRider[_RM].riderEntID = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderEntID"].ToString();
                    //        objRider[_RM].riderNo = " ";
                    //        //objRider[_RM].riderParent = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderParent"].ToString();
                    //        objRider[_RM].riderParent = "C0" + EntityID; ;
                    //        objRider[_RM].riskCessationAge = 0;

                    //        objRider[_RM].riskCessationTerm = 0;


                    //        objRider[_RM].sumAssured = 0;
                    //        EntityID++;
                    //    }
                    //}
                    #endregion MasterEntityRiderDetailList End.

                    #region MasterEntityBeneficiaryList Begins.
                    // objBeneficiary = new LAContractModService.MasterEntityBeneficiaryList[_dsContract.Tables["Beneficiary"].Rows.Count];
                    string strProdExclNominee = "H01,H02,H03,H04,H05,H06,H07,H08,U32,U33,U34,U35,U36";
                    if (strIsProposer == "0" && strProdExclNominee.Contains(strProdcode))
                    {
                        // this logic will be revised after confirmation from bs team.                                        
                        objBeneficiary = new LAContractModService.MasterEntityBeneficiaryList[0];
                    }
                    else
                    {
                        objBeneficiary = new LAContractModService.MasterEntityBeneficiaryList[NomineeRow.Length];
                        count = 0;
                        int Perc = 100;
                        DataRow[] _drNomcount = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='Nominee'");
                        int nomineecount = _drNomcount.Length;
                        decimal balance = 0;
                        int sharecount = 0;
                        decimal shareper = 0;
                        string strRelation = string.Empty;
                        /*ID:2 START by Dinesh Kondabattini*/
                        if (MWP == "Yes")
                        {

                            foreach (DataRow row in NomineeRow.OrderByDescending(row => row[7]))
                            {
                                objBeneficiary[count] = new LAContractModService.MasterEntityBeneficiaryList();
                                string _code;
                                if (row[7].ToString() == "Trustee" || row[7].ToString() == "TR")
                                {
                                    _code = "";
                                    objBeneficiary[count].beneficiaryRelationship = "TR";
                                    objBeneficiary[count].beneficiaryPercentage = 100;
                                }
                                else if (row[7].ToString() == "Appointee" || row[7].ToString() == "Nominee")
                                {
                                    _code = "N";
                                    objBeneficiary[count].beneficiaryPercentage = 0;
                                }
                                else
                                {
                                    _code = row["beneficiaryCode"].ToString();
                                }
                                if (row[7].ToString() == "Appointee" || row[7].ToString() == "AP")
                                {
                                    objBeneficiary[count].beneficiaryRelationship = "AP";
                                }
                                else if (row[7].ToString() == "Nominee")
                                {
                                    objBeneficiary[count].beneficiaryRelationship = row["relationship"].ToString();
                                }

                                objBeneficiary[count].beneficiaryCode = _code;
                                objBeneficiary[count].beneficiaryParty = row["CLT_clientId_CLNTNUM"].ToString();
                                objBeneficiary[count].effectiveDate = DateFormat(row["effectiveDate"]).ToString();
                                count++;
                            }
                        }
                        else
                        {
                            /*ID:2 End by Dinesh Kondabattini*/
                            foreach (DataRow row in NomineeRow.OrderByDescending(row => row[7]))
                            {
                                string _code;
                                if (row[7].ToString() == "Appointee" || row[7].ToString() == "AP")
                                {
                                    _code = "N";
                                    balance = 0;
                                }
                                else
                                {
                                    _code = row["beneficiaryCode"].ToString();
                                    strRelation = "";
                                    if (strAppType.ToUpper().Equals("ONLINE"))
                                    {

                                        if (string.IsNullOrEmpty(Convert.ToString(row["beneficiaryPercentage"])) || Convert.ToString(row["beneficiaryPercentage"]) == "0")
                                        {
                                            balance = Perc / nomineecount;
                                            balance = Math.Round(balance);
                                            if (sharecount == (nomineecount - 1))
                                            {
                                                balance = 100 - shareper;
                                            }
                                            else
                                            {
                                                shareper = shareper + balance;
                                            }
                                            sharecount++;
                                        }
                                        else
                                        {
                                            balance = Convert.ToInt32(row["beneficiaryPercentage"]);
                                            shareper = shareper + balance;
                                        }
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(Convert.ToString(row["beneficiaryPercentage"])) || Convert.ToString(row["beneficiaryPercentage"]) == "0")
                                        {
                                            balance = Perc / nomineecount;
                                            balance = Math.Round(balance);
                                            if (sharecount == (nomineecount - 1))
                                            {
                                                balance = 100 - shareper;
                                            }
                                            else
                                            {
                                                shareper = shareper + balance;
                                            }
                                            sharecount++;
                                        }
                                        else
                                        {
                                            balance = Convert.ToInt32(row["beneficiaryPercentage"]);
                                            shareper = shareper + balance;
                                        }
                                    }

                                }


                                objBeneficiary[count] = new LAContractModService.MasterEntityBeneficiaryList();
                                //objBeneficiary[count].beneficiaryCode = row["beneficiaryCode"].ToString();
                                objBeneficiary[count].beneficiaryCode = _code;
                                objBeneficiary[count].beneficiaryParty = row["CLT_clientId_CLNTNUM"].ToString();
                                //objBeneficiary[count].beneficiaryParty = _strOwnerClientID;
                                //objBeneficiary[count].beneficiaryPercentage = Convert.ToInt32(row["beneficiaryPercentage"]);  
                                objBeneficiary[count].beneficiaryPercentage = Convert.ToInt32(balance);
                                objBeneficiary[count].beneficiaryRelationship = (string.IsNullOrEmpty(strRelation)) ? row["relationship"].ToString() : strRelation;



                                if (!string.IsNullOrEmpty(strdataValue))
                                {
                                    objBeneficiary[count].effectiveDate=RCDCommencementDate = strdataValue.Replace("-", "/");
                                }
                                else
                                {
                                    //objBeneficiary[count].effectiveDate = DateFormat(row["effectiveDate"]).ToString();
                                    #region 2.1 Start:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
                                    //objBeneficiary[count].effectiveDate =   strRiskCommensmentdate;
                                    objBeneficiary[count].effectiveDate = RCDCommencementDate;
                                    #endregion 2.1 End:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
                                }

                                count++;
                            }
                            /*ID:2 START by Dinesh Kondabattini*/
                        }
                        /*ID:2 End by Dinesh Kondabattini*/


                        //objBeneficiary[count] = new LAContractModService.MasterEntityBeneficiaryList();
                        //objBeneficiary[count].beneficiaryCode = row["beneficiaryCode"].ToString();
                        //objBeneficiary[count].beneficiaryParty = row["beneficiaryParty"].ToString();
                        ////objBeneficiary[count].beneficiaryParty = _strOwnerClientID;
                        //objBeneficiary[count].beneficiaryPercentage =  Convert.ToInt32(row["beneficiaryPercentage"]);
                        //objBeneficiary[count].beneficiaryRelationship = row["relationship"].ToString();
                        //objBeneficiary[count].effectiveDate = DateFormat(row["effectiveDate"]).ToString();
                        //count++;
                    }

                    //for (_BN = 0; _BN < _dsContract.Tables["Beneficiary"].Rows.Count; _BN++)
                    //{
                    //    objBeneficiary[_BN] = new LAContractModService.MasterEntityBeneficiaryList();
                    //    objBeneficiary[_BN].beneficiaryCode = _dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryCode"].ToString();
                    //    objBeneficiary[_BN].beneficiaryParty = _dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryParty"].ToString();
                    //    objBeneficiary[_BN].beneficiaryPercentage = Convert.ToInt32(_dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryPercentage"].ToString());
                    //    objBeneficiary[_BN].beneficiaryRelationship = _dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryRelationship"].ToString();
                    //    objBeneficiary[_BN].effectiveDate = _dsContract.Tables["Beneficiary"].Rows[_BN]["effectiveDate"].ToString();
                    //}
                    #endregion MasterEntityBeneficiaryList End.

                    #region MasterEntityFollowUp Begins.
                    if (_dsContract.Tables["FOLLOWUPDTLS"].Rows.Count > 0)
                    {
                        objFollowup.doctorClientNo = _dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["doctorClientNo"].ToString();
                        objFollowup.followUpCode = _dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followUpCode"].ToString();
                        objFollowup.followUpNo = Convert.ToInt32(_dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followUpNo"].ToString());
                        objFollowup.followUpStatus = _dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followUpStatus"].ToString();
                        if (!string.IsNullOrEmpty(_dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followuUpReminderDate"].ToString()))
                            objFollowup.followuUpReminderDate = DateFormat(_dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followuUpReminderDate"]).ToString();
                        objFollowup.lifeNo = Convert.ToInt32(_dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["lifeNo"].ToString());
                    }
                    //else --this part is commented as space and default value are handle in service after discussion with dharmeshh.
                    //{
                    //    objFollowup.doctorClientNo = "";
                    //    objFollowup.followUpCode = "";
                    //    objFollowup.followUpNo = 0;
                    //    objFollowup.followUpStatus = "";
                    //    objFollowup.followuUpReminderDate = "";
                    //    objFollowup.lifeNo = 0;
                    //}
                    #endregion MasterEntityFollowUp End.

                    #region MasterEntityAssignee Begins.
                    //LA Loop
                    if (_dsContract.Tables["CLNTDTLS"].Rows.Count > 0)
                    {
                        DataRow[] Assignee = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='Assignee'");
                        if (Assignee.Length > 0)
                        {
                            foreach (DataRow JointLifeEntityRow in Assignee)
                            {
                                objAssignee.assigneeParty = JointLifeEntityRow["assigneeParty"].ToString();
                                objAssignee.commissionFromDate = JointLifeEntityRow["commissionFromDate"].ToString();
                                objAssignee.commissionToDate = JointLifeEntityRow["commissionToDate"].ToString();
                                objAssignee.incomeProof = JointLifeEntityRow["incomeProof"].ToString();
                                objAssignee.incomeProofIndicator = JointLifeEntityRow["incomeProofIndicator"].ToString();
                                objAssignee.reasonCode = JointLifeEntityRow["reasonCode"].ToString();
                            }
                        }

                    }

                    //objAssignee.assigneeParty = _dsContract.Tables["Assignee"].Rows[0]["assigneeParty"].ToString();
                    //objAssignee.commissionFromDate = _dsContract.Tables["Assignee"].Rows[0]["commissionFromDate"].ToString();
                    //objAssignee.commissionToDate = _dsContract.Tables["Assignee"].Rows[0]["commissionToDate"].ToString();
                    //objAssignee.incomeProof = _dsContract.Tables["Assignee"].Rows[0]["incomeProof"].ToString();
                    //objAssignee.incomeProofIndicator = _dsContract.Tables["Assignee"].Rows[0]["incomeProofIndicator"].ToString();
                    //objAssignee.reasonCode = _dsContract.Tables["Assignee"].Rows[0]["reasonCode"].ToString();


                    #endregion MasterEntityAssignee End.

                    #region MasterEntityDespatchCorrespondence Begins.
                    //  objDespatch.despatchParty = _dsContract.Tables["Despatch"].Rows[0]["despatchParty"].ToString();
                    foreach (DataRow row in _dsContract.Tables["CONTOBJ"].Rows)
                    {
                        objDespatch.despatchParty = row["despatchParty"].ToString();
                    }

                    #endregion MasterEntityDespatchCorrespondence End.

                    #region MasterEntityPayerDetail Begins.
                    //Nomine loop
                    //foreach (DataRow row in NomineeRow)
                    //{
                    //    objPayer.payerParty = row["CLT_clientId_CLNTNUM"].ToString();
                    //}
                    if (strPayerClientId != strProposerClientId && strIsProposer == "2")
                    {
                        objPayer.payerParty = strPayerClientId;
                    }
                    else
                    {
                        objPayer.payerParty = _strOwnerClientID;
                    }
                    #endregion MasterEntityPayerDetail End.

                    #region MasterEntitySpecialTermList Begins.

                    objSpecialTerm = new LAContractModService.MasterEntitySpecialTermList[dtCoverage.Length];
                    count = 0;
                    EntityID = 1;
                    foreach (DataRow SpecialTerm in dtCoverage)
                    {
                        objSpecialTerm[count] = new LAContractModService.MasterEntitySpecialTermList();
                        objSpecialTerm[count].adjustmentCode = SpecialTerm["adjustmentCode"].ToString();
                        objSpecialTerm[count].adjustmentDuration = Convert.ToInt32(SpecialTerm["adjustmentDuration"].ToString());
                        objSpecialTerm[count].adjustmentPercentage = Convert.ToInt32(SpecialTerm["adjustmentPercentage"].ToString());
                        objSpecialTerm[count].ageRating = Convert.ToInt32(SpecialTerm["ageRating"].ToString());
                        objSpecialTerm[count].lineOfSublife = SpecialTerm["lineOfSublife"].ToString();
                        objSpecialTerm[count].rateAdjustment = Convert.ToInt32(SpecialTerm["rateAdjustment"].ToString());
                        objSpecialTerm[count].reassuranceIndictor = SpecialTerm["reassuranceIndictor"].ToString();
                        //objSpecialTerm[count].specTermsEntID = row["specTermsEntID"].ToString();
                        objSpecialTerm[count].specTermsEntID = "S0" + EntityID;
                        // objSpecialTerm[count].specTermsParent = row["specTermsParent"].ToString();
                        objSpecialTerm[count].specTermsParent = "C0" + EntityID; ;
                        count++;
                        EntityID++;
                    }
                    //for (_SO = 0; _SO < _dsContract.Tables["SpecialTerm"].Rows.Count; _SO++)
                    //{
                    //    objSpecialTerm[_SO].adjustmentCode = _dsContract.Tables["SpecialTerm"].Rows[_SO]["adjustmentCode"].ToString();
                    //    objSpecialTerm[_SO].adjustmentDuration = Convert.ToInt32(_dsContract.Tables["SpecialTerm"].Rows[_SO]["adjustmentDuration"].ToString());
                    //    objSpecialTerm[_SO].adjustmentPercentage = Convert.ToInt32(_dsContract.Tables["SpecialTerm"].Rows[_SO]["adjustmentPercentage"].ToString());
                    //    objSpecialTerm[_SO].ageRating = Convert.ToInt32(_dsContract.Tables["SpecialTerm"].Rows[_SO]["ageRating"].ToString());
                    //    objSpecialTerm[_SO].lineOfSublife = _dsContract.Tables["SpecialTerm"].Rows[_SO]["lineOfSublife"].ToString();
                    //    objSpecialTerm[_SO].rateAdjustment = Convert.ToInt32(_dsContract.Tables["SpecialTerm"].Rows[_SO]["rateAdjustment"].ToString());
                    //    objSpecialTerm[_SO].reassuranceIndictor = _dsContract.Tables["SpecialTerm"].Rows[_SO]["reassuranceIndictor"].ToString();
                    //    objSpecialTerm[_SO].specTermsEntID = _dsContract.Tables["SpecialTerm"].Rows[_SO]["specTermsEntID"].ToString();
                    //    objSpecialTerm[_SO].specTermsParent = _dsContract.Tables["SpecialTerm"].Rows[_SO]["specTermsParent"].ToString();

                    //}
                    #endregion MasterEntitySpecialTermList End.

                    #region MasterEntityFundDetailList Begins.
                    EntityID = 1;
                    string[] unitLinkedFund = new string[10];
                    int[] unitPercent = new int[10];
                    if (_dsContract.Tables["FUNDDTLS"].Rows.Count > 0)
                    {
                        //objFundDetails = new LAContractModService.MasterEntityFundDetailList[_dsContract.Tables["FUNDDTLS"].Rows.Count];
                        objFundDetails = new LAContractModService.MasterEntityFundDetailList[1];
                        for (int j = 0; j < 10; j++)
                        {
                            objFundDetails[0] = new LAContractModService.MasterEntityFundDetailList();
                            unitLinkedFund[j] = string.Empty;
                            unitPercent[j] = 0;
                        }

                        for (_FP = 0; _FP < _dsContract.Tables["FUNDDTLS"].Rows.Count; _FP++)
                        {
                            unitLinkedFund[_FP] = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["fundEntID"].ToString();
                            unitPercent[_FP] = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent"].ToString());
                        }
                        objFundDetails[0].fundEntID = "F0" + EntityID;
                        objFundDetails[0].fundParent = "C0" + EntityID;
                        objFundDetails[0].unitLinkedFund01 = unitLinkedFund[0].ToString();
                        objFundDetails[0].unitLinkedFund02 = unitLinkedFund[1].ToString();
                        objFundDetails[0].unitLinkedFund03 = unitLinkedFund[2].ToString();
                        objFundDetails[0].unitLinkedFund04 = unitLinkedFund[3].ToString();
                        objFundDetails[0].unitLinkedFund05 = unitLinkedFund[4].ToString();
                        objFundDetails[0].unitLinkedFund06 = unitLinkedFund[5].ToString();
                        objFundDetails[0].unitLinkedFund07 = unitLinkedFund[6].ToString();
                        objFundDetails[0].unitLinkedFund08 = unitLinkedFund[7].ToString();
                        objFundDetails[0].unitLinkedFund09 = unitLinkedFund[8].ToString();
                        objFundDetails[0].unitLinkedFund10 = unitLinkedFund[9].ToString();
                        objFundDetails[0].unitPercent01 = unitPercent[0];
                        objFundDetails[0].unitPercent02 = unitPercent[1];
                        objFundDetails[0].unitPercent03 = unitPercent[2];
                        objFundDetails[0].unitPercent04 = unitPercent[3];
                        objFundDetails[0].unitPercent05 = unitPercent[4];
                        objFundDetails[0].unitPercent06 = unitPercent[5];
                        objFundDetails[0].unitPercent07 = unitPercent[6];
                        objFundDetails[0].unitPercent08 = unitPercent[7];
                        objFundDetails[0].unitPercent09 = unitPercent[8];
                        objFundDetails[0].unitPercent10 = unitPercent[9];
                        objFundDetails[0].unitPercentage = _dsContract.Tables["FUNDDTLS"].Rows[0]["unitPercentage"].ToString();
                        objFundDetails[0].unitVirtualFundSplitMethod = _dsContract.Tables["FUNDDTLS"].Rows[0]["unitVirtualFundSplitMethod"].ToString();

                        EntityID++;
                    }

                    //else -- this part is commented as space and default value are handle in service after discussion with dharmesh.
                    //{
                    //    objFundDetails = new LAContractModService.MasterEntityFundDetailList[5];
                    //    for (_FP = 0; _FP < 5; _FP++)
                    //    {
                    //        objFundDetails[_FP] = new LAContractModService.MasterEntityFundDetailList();
                    //        objFundDetails[_FP].fundEntID = "F0" + EntityID;
                    //        objFundDetails[_FP].fundParent = "L0" + EntityID;
                    //        objFundDetails[_FP].unitLinkedFund01 = " ";
                    //        objFundDetails[_FP].unitLinkedFund02 = " ";
                    //        objFundDetails[_FP].unitLinkedFund03 = " ";
                    //        objFundDetails[_FP].unitLinkedFund04 = " ";
                    //        objFundDetails[_FP].unitLinkedFund05 = " ";
                    //        objFundDetails[_FP].unitLinkedFund06 = " ";
                    //        objFundDetails[_FP].unitLinkedFund07 = " ";
                    //        objFundDetails[_FP].unitLinkedFund08 = " ";
                    //        objFundDetails[_FP].unitLinkedFund09 = " ";
                    //        objFundDetails[_FP].unitLinkedFund10 = " ";
                    //        objFundDetails[_FP].unitPercent01 = 0;
                    //        objFundDetails[_FP].unitPercent02 = 0;
                    //        objFundDetails[_FP].unitPercent03 = 0;
                    //        objFundDetails[_FP].unitPercent04 = 0;
                    //        objFundDetails[_FP].unitPercent05 = 0;
                    //        objFundDetails[_FP].unitPercent06 = 0;
                    //        objFundDetails[_FP].unitPercent07 = 0;
                    //        objFundDetails[_FP].unitPercent08 = 0;
                    //        objFundDetails[_FP].unitPercent09 = 0;
                    //        objFundDetails[_FP].unitPercent10 = 0;
                    //        objFundDetails[_FP].unitPercentage = " ";
                    //        objFundDetails[_FP].unitVirtualFundSplitMethod = " ";

                    //        EntityID++;
                    //    }
                    //}
                    #endregion MasterEntityFundDetailList End.


                    #region MasterEntityClient Begins.
                    JointLifeEntity = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='LA'");
                    foreach (DataRow JointLifeEntityRow in JointLifeEntity)
                    {
                        objClient.addrType = JointLifeEntityRow["addrType"].ToString();
                        objClient.birthPlace = JointLifeEntityRow["birthPlace"].ToString();
                        objClient.categoryStatisticalStatus = JointLifeEntityRow["categoryStatisticalStatus"].ToString();
                        objClient.clientAddress01 = JointLifeEntityRow["clientAddress01"].ToString();
                        objClient.clientAddress02 = JointLifeEntityRow["clientAddress02"].ToString();
                        objClient.clientAddress03 = JointLifeEntityRow["clientAddress03"].ToString();
                        objClient.clientAddress04 = JointLifeEntityRow["clientAddress04"].ToString();
                        objClient.clientEntID = JointLifeEntityRow["CLT_clientId_CLNTNUM"].ToString();
                        objClient.clientNo = JointLifeEntityRow["CLT_clientId_CLNTNUM"].ToString();
                        objClient.clientParty = JointLifeEntityRow["CLT_clientId_CLNTNUM"].ToString();
                        objClient.clientPhone01 = JointLifeEntityRow["clientPhone01"].ToString();
                        objClient.clientPhone02 = JointLifeEntityRow["clientPhone02"].ToString();
                        objClient.clientType = JointLifeEntityRow["clientType"].ToString();
                        objClient.countryCode = JointLifeEntityRow["countryCode"].ToString();
                        objClient.dateOfBirth = DateFormat(JointLifeEntityRow["dateOfBirth"]).ToString();
                        objClient.directMailIndicator = JointLifeEntityRow["directMailIndicator"].ToString();
                        objClient.documentNo = JointLifeEntityRow["documentNo"].ToString();
                        objClient.forTheAttentionOf = JointLifeEntityRow["forTheAttentionOf"].ToString();
                        objClient.language = JointLifeEntityRow["language"].ToString();
                        objClient.longGivenName = JointLifeEntityRow["longGivenName"].ToString();
                        objClient.longSurname = JointLifeEntityRow["longSurname"].ToString();
                        objClient.mailingIndicator = JointLifeEntityRow["mailingIndicator"].ToString();
                        objClient.marriedIndicator = JointLifeEntityRow["marriedIndicator"].ToString();
                        objClient.nameFormatting = JointLifeEntityRow["nameFormatting"].ToString();
                        objClient.nationality = JointLifeEntityRow["nationality"].ToString();
                        objClient.occupationCode = JointLifeEntityRow["occupationCode"].ToString();
                        objClient.postcode = JointLifeEntityRow["postcode"].ToString();
                        objClient.salutation = JointLifeEntityRow["salutation"].ToString();
                        objClient.sex = JointLifeEntityRow["sex"].ToString();
                        objClient.socialSecurityNo = JointLifeEntityRow["socialSecurityNo"].ToString();
                        objClient.sourceOfEvidence = JointLifeEntityRow["sourceOfEvidence"].ToString();
                        objClient.VIPIndicator = JointLifeEntityRow["VIPIndicator"].ToString();
                    }
                    //objClient.addrType = _dsContract.Tables[""].Rows[_FP]["addrType"].ToString();
                    //objClient.birthPlace = _dsContract.Tables[""].Rows[_FP]["birthPlace"].ToString();
                    //objClient.categoryStatisticalStatus = _dsContract.Tables[""].Rows[_FP]["categoryStatisticalStatus"].ToString();
                    //objClient.clientAddress01 = _dsContract.Tables[""].Rows[_FP]["clientAddress01"].ToString();
                    //objClient.clientAddress02 = _dsContract.Tables[""].Rows[_FP]["clientAddress02"].ToString();
                    //objClient.clientAddress03 = _dsContract.Tables[""].Rows[_FP]["clientAddress03"].ToString();
                    //objClient.clientAddress04 = _dsContract.Tables[""].Rows[_FP]["clientAddress04"].ToString();
                    //objClient.clientEntID = _dsContract.Tables[""].Rows[_FP]["clientEntID"].ToString();
                    //objClient.clientNo = _dsContract.Tables[""].Rows[_FP]["clientNo"].ToString();
                    //objClient.clientParty = _dsContract.Tables[""].Rows[_FP]["clientParty"].ToString();
                    //objClient.clientPhone01 = _dsContract.Tables[""].Rows[_FP]["clientPhone01"].ToString();
                    //objClient.clientPhone02 = _dsContract.Tables[""].Rows[_FP]["clientPhone02"].ToString();
                    //objClient.clientType = _dsContract.Tables[""].Rows[_FP]["clientType"].ToString();
                    //objClient.countryCode = _dsContract.Tables[""].Rows[_FP]["countryCode"].ToString();
                    //objClient.dateOfBirth = _dsContract.Tables[""].Rows[_FP]["dateOfBirth"].ToString();
                    //objClient.directMailIndicator = _dsContract.Tables[""].Rows[_FP]["directMailIndicator"].ToString();
                    //objClient.documentNo = _dsContract.Tables[""].Rows[_FP]["documentNo"].ToString();
                    //objClient.forTheAttentionOf = _dsContract.Tables[""].Rows[_FP]["forTheAttentionOf"].ToString();
                    //objClient.language = _dsContract.Tables[""].Rows[_FP]["language"].ToString();
                    //objClient.longGivenName = _dsContract.Tables[""].Rows[_FP]["longGivenName"].ToString();
                    //objClient.longSurname = _dsContract.Tables[""].Rows[_FP]["longSurname"].ToString();
                    //objClient.marriedIndicator = _dsContract.Tables[""].Rows[_FP]["marriedIndicator"].ToString();
                    //objClient.nameFormatting = _dsContract.Tables[""].Rows[_FP]["nameFormatting"].ToString();
                    //objClient.nationality = _dsContract.Tables[""].Rows[_FP]["nationality"].ToString();
                    //objClient.occupationCode = _dsContract.Tables[""].Rows[_FP]["occupationCode"].ToString();
                    //objClient.postcode = _dsContract.Tables[""].Rows[_FP]["postcode"].ToString();
                    //objClient.salutation = _dsContract.Tables[""].Rows[_FP]["salutation"].ToString();
                    //objClient.sex = _dsContract.Tables[""].Rows[_FP]["sex"].ToString();
                    //objClient.socialSecurityNo = _dsContract.Tables[""].Rows[_FP]["socialSecurityNo"].ToString();
                    //objClient.sourceOfEvidence = _dsContract.Tables[""].Rows[_FP]["sourceOfEvidence"].ToString();
                    //objClient.VIPIndicator = _dsContract.Tables[""].Rows[_FP]["VIPIndicator"].ToString();
                    #endregion MasterEntityClient End.

                    #region MasterEntityReceiptToContractList Begins.

                    if (_dsContract.Tables["RCPTDTLS"].Rows.Count > 0)
                    {
                        string RecepitAppNO = "RCPT_AppNo = " + strApplicationNo;
                        DataRow[] dtRecepitDT = _dsContract.Tables["RCPTDTLS"].Select(RecepitAppNO);
                        if (dtRecepitDT.Length > 0)
                        {
                            count = 0;
                            objReptToCont = new LAContractModService.MasterEntityReceiptToContractList[dtRecepitDT.Length];
                            foreach (DataRow row in dtRecepitDT)
                            {
                                objReptToCont[count] = new LAContractModService.MasterEntityReceiptToContractList();
                                objReptToCont[count].receiptNo = row["receiptNo"].ToString();
                                objReptToCont[count].tranSequence = row["tranSequence"].ToString();
                                count++;
                            }
                        }
                    }
                    else
                    {
                        objReptToCont = new LAContractModService.MasterEntityReceiptToContractList[0];
                    }

                    #endregion MasterEntityReceiptToContractList End.

                    #region MasterEntityMandate begins.
                    if (_dsContract.Tables["MANDTLS"].Rows.Count > 0)
                    {

                        // call bank enquiry service to get factory house number.
                        UWSaralServices.BankEnquiryDetails objBankEnquiryDetails = new UWSaralServices.BankEnquiryDetails();
                        UWSaralServices.MandateDetails objMandateDetails = new UWSaralServices.MandateDetails();
                        DataSet _dsManadtae = new DataSet();
                        string strResp = string.Empty;
                        //if ((_dsContract.Tables["MANDTLS"].Rows[0]["bankKey"].ToString() == "" || _dsContract.Tables["MANDTLS"].Rows[0]["bankKey"].ToString() == null) && (_dsContract.Tables["MANDTLS"].Rows[0]["factorisingHouse"].ToString() == "" || _dsContract.Tables["MANDTLS"].Rows[0]["factorisingHouse"].ToString() == null))
                        //{


                        //fetch data from db 
                        objComm.BankEnqManageDetails_GET(strPQuoteNo, ref _dsManadtae);
                        //call bank enquiry service                
                        objBankEnquiryDetails.BankEnquireService(strPQuoteNo, _dsManadtae, objChangeObj, ref strLAPushErrorcode, ref strLAPushStatus);
                        //JObject json = new JObject();
                        //dynamic json = JsonConvert.DeserializeObject(strLAPushStatus);
                        if (strLAPushErrorcode == -3)
                        {
                            JObject obj = JObject.Parse(strLAPushStatus);
                            string FACTHOUS = (string)obj["FACTHOUS"];
                            string BANKKEY = (string)obj["BANKKEY"];
                            string BANKACOUNT = (string)obj["BANKACOUNT"];

                            _dsContract.Tables["MANDTLS"].Rows[0]["bankKey"] = BANKKEY.Trim();
                            _dsContract.Tables["MANDTLS"].Rows[0]["factorisingHouse"] = FACTHOUS.Trim();
                            strLAPushStatus = String.Empty;
                        }
                        //}
                        // end

                        objMandate.bankaccessApprovalNo = _dsContract.Tables["MANDTLS"].Rows[0]["bankaccessApprovalNo"].ToString();
                        objMandate.bankAccountNo = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountNo"].ToString();
                        objMandate.bankKey = _dsContract.Tables["MANDTLS"].Rows[0]["bankKey"].ToString();
                        if (strPayerClientId == strProposerClientId && strIsProposer == "2")
                        {
                            objMandate.clientNo = strProposerClientId;
                        }
                        if (strPayerClientId != strProposerClientId && strIsProposer == "2")
                        {
                            objMandate.clientNo = strPayerClientId;
                        }
                        else
                        {
                            objMandate.clientNo = _strOwnerClientID;
                        }
                        //objMandate.effectiveDate = DateFormat(_dsContract.Tables["MANDTLS"].Rows[0]["effectiveDate"]).ToString();
                        objMandate.effectiveDate = DateFormat(_dsContract.Tables["MANDTLS"].Rows[0]["effectiveDate"]).ToString();
                        objMandate.mandateAmount = Convert.ToInt32(_dsContract.Tables["MANDTLS"].Rows[0]["mandateAmount"].ToString());
                        objMandate.mandateReferenceNo = _dsContract.Tables["MANDTLS"].Rows[0]["mandateReferenceNo"].ToString();
                        objMandate.mandateStatusCode = _dsContract.Tables["MANDTLS"].Rows[0]["mandateStatusCode"].ToString();
                        objMandate.summaryIndicator = _dsContract.Tables["MANDTLS"].Rows[0]["summaryIndicator"].ToString();
                        objMandate.timesToUse = Convert.ToInt32(_dsContract.Tables["MANDTLS"].Rows[0]["timesToUse"].ToString());
                        objMandate.factorisingHouse = _dsContract.Tables["MANDTLS"].Rows[0]["factorisingHouse"].ToString();
                        //objMandate.factorisingHouse = "05";
                    }
                    //else this part is commented as space and default value are handle in service after discussion with dharmesh.
                    //{
                    //    objMandate.bankaccessApprovalNo = " ";
                    //    objMandate.bankAccountNo = " ";
                    //    objMandate.bankKey = " ";
                    //    objMandate.clientNo = _strOwnerClientID;
                    //    objMandate.effectiveDate = " ";
                    //    objMandate.mandateAmount = 0;
                    //    objMandate.mandateReferenceNo = " ";
                    //    objMandate.mandateStatusCode = "";
                    //    objMandate.summaryIndicator = "";
                    //    objMandate.timesToUse = 0;
                    //    //  objMandate.factorisingHouse = _dsContract.Tables["MANDTLS"].Rows[0]["factorisingHouse"].ToString();
                    //    objMandate.factorisingHouse = "05";
                    //}

                    #endregion  MasterEntityMandate End.

                    #region MasterEntityClientBankAccount Begins.
                    if (_dsContract.Tables["MANDTLS"].Rows.Count > 0)
                    {
                        objBankDetails.bankAccountDescription = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountDescription"].ToString();
                        objBankDetails.bankAccountNo = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountNo"].ToString();
                        objBankDetails.bankAccountType = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountType"].ToString();
                        objBankDetails.bankKey = _dsContract.Tables["MANDTLS"].Rows[0]["bankKey"].ToString();
                        objBankDetails.bankSecurityCode = _dsContract.Tables["MANDTLS"].Rows[0]["bankSecurityCode"].ToString();
                        //  objBankDetails.clientBankDetails = _dsContract.Tables["MANDTLS"].Rows[_FP]["clientBankDetails"].ToString(); 
                        objBankDetails.clientBankDetails = _strOwnerClientID;
                        objBankDetails.currencyCode = _dsContract.Tables["MANDTLS"].Rows[0]["currencyCode"].ToString();
                        objBankDetails.dateFrom = DateFormat(_dsContract.Tables["MANDTLS"].Rows[0]["dateFrom"]).ToString();
                        objBankDetails.factorisingHouse = _dsContract.Tables["MANDTLS"].Rows[0]["factorisingHouse"].ToString();
                        //objBankDetails.factorisingHouse = "05";

                    }
                    //else this part is commented as space and default value are handle in service after discussion with dharmesh.
                    //{
                    //    objBankDetails.bankAccountDescription = " ";
                    //    objBankDetails.bankAccountNo = " ";
                    //    objBankDetails.bankAccountType = " ";
                    //    objBankDetails.bankKey = " ";
                    //    objBankDetails.bankSecurityCode = " ";
                    //    //  objBankDetails.clientBankDetails = _dsContract.Tables["MANDTLS"].Rows[_FP]["clientBankDetails"].ToString(); 
                    //    objBankDetails.clientBankDetails = _strOwnerClientID;
                    //    objBankDetails.currencyCode = " ";
                    //    objBankDetails.dateFrom = "99/99/9999";
                    //    objBankDetails.factorisingHouse = " ";
                    //    //objBankDetails.factorisingHouse = "05";
                    //}

                    #endregion MasterEntityClientBankAccount End.

                    #region MasterEntityPremiumReceipt Begin.
                    foreach (DataRow premiumRecepit in dtCoverage)
                    {
                        objReceipt.amountOriginalCurrency = Convert.ToInt32(premiumRecepit["amountOriginalCurrency"].ToString());
                        objReceipt.bankCode = premiumRecepit["bankCode"].ToString();
                        objReceipt.bankDescription01 = premiumRecepit["bankDescription01"].ToString();
                        objReceipt.bankDescription02 = premiumRecepit["bankDescription02"].ToString();
                        objReceipt.bankDescription03 = premiumRecepit["bankDescription03"].ToString();
                        objReceipt.bankedFlag = premiumRecepit["bankedFlag"].ToString();
                        objReceipt.bankKey = premiumRecepit["bankKey"].ToString();
                        objReceipt.chequeDate = premiumRecepit["chequeDate"].ToString();
                        objReceipt.chequeNo = premiumRecepit["chequeNo"].ToString();
                        objReceipt.currencyRate = Convert.ToInt32(premiumRecepit["currencyRate"].ToString());
                        objReceipt.dissectionNo = Convert.ToInt32(premiumRecepit["dissectionNo"].ToString());
                        objReceipt.documentAmountOriginalCurrency = Convert.ToInt32(premiumRecepit["documentAmountOriginalCurrency"].ToString());
                        objReceipt.entity = premiumRecepit["entity"].ToString();
                        objReceipt.originalCurrency = premiumRecepit["originalCurrency"].ToString();
                        objReceipt.paymentType = premiumRecepit["paymentType"].ToString();
                        objReceipt.protectIND = premiumRecepit["protectIND"].ToString();
                        objReceipt.receivedFromCode = premiumRecepit["receivedFromCode"].ToString();
                        objReceipt.receivedFromNo = premiumRecepit["receivedFromNo"].ToString();
                        objReceipt.subAccountCode = premiumRecepit["subAccountCode"].ToString();
                        objReceipt.subAccountType = premiumRecepit["subAccountType"].ToString();
                        objReceipt.timestamp = premiumRecepit["timestamp"].ToString();
                        objReceipt.transactionDate = premiumRecepit["transactionDate"].ToString();
                    }

                    #endregion  MasterEntityPremiumReceipt End.

                    #region MasterEntityAgencyDetail Begins.

                    foreach (DataRow AgencyDetail in dtCoverage)
                    {
                        objAgencyDetails.agentNo = AgencyDetail["agentNo"].ToString();
                        objAgencyDetails.frontEndRegionCode01 = AgencyDetail["frontEndRegionCode01"].ToString();
                        objAgencyDetails.frontEndRegionCode02 = AgencyDetail["frontEndRegionCode02"].ToString();
                        objAgencyDetails.frontEndRegionCode03 = AgencyDetail["frontEndRegionCode03"].ToString();
                        objAgencyDetails.frontEndRegionCode04 = AgencyDetail["frontEndRegionCode04"].ToString();
                        objAgencyDetails.newAgentAreaCode = AgencyDetail["newAgentAreaCode"].ToString();
                        objAgencyDetails.reportingAgent01 = AgencyDetail["reportingAgent01"].ToString();
                        objAgencyDetails.reportingAgent02 = AgencyDetail["reportingAgent02"].ToString();
                        objAgencyDetails.sourceOfBusiness01 = AgencyDetail["sourceOfBusiness01"].ToString();
                        objAgencyDetails.sourceOfBusiness02 = AgencyDetail["sourceOfBusiness02"].ToString();

                    }

                    #endregion MasterEntityAgencyDetail end.

                    #region MasterEntityAnnuityDetail Begins.
                    foreach (DataRow AnnuityDetail in dtCoverage)
                    {
                        objAnnuityDetails.annuityPaymentInAdvance = AnnuityDetail["annuityPaymentInAdvance"].ToString();
                        objAnnuityDetails.annuityPaymentsInArrears = AnnuityDetail["annuityPaymentsInArrears"].ToString();
                        objAnnuityDetails.annuityPaymentsWithoutProportion = AnnuityDetail["annuityPaymentsWithoutProportion"].ToString();
                        objAnnuityDetails.annuityPaymentsWithProportion = AnnuityDetail["annuityPaymentsWithProportion"].ToString();
                        objAnnuityDetails.capitalContentAnnuityPayment = Convert.ToInt32(AnnuityDetail["capitalContentAnnuityPayment"].ToString());
                        objAnnuityDetails.frequencyOfAnnuityPAyments = AnnuityDetail["frequencyOfAnnuityPAyments"].ToString();
                        objAnnuityDetails.guaranteedPaymentPeriod = Convert.ToInt32(AnnuityDetail["guaranteedPaymentPeriod"].ToString());
                        objAnnuityDetails.interestRateInAnnuityCalculations = Convert.ToInt32(AnnuityDetail["interestRateInAnnuityCalculations"].ToString());
                        objAnnuityDetails.NominatedLife = AnnuityDetail["NominatedLife"].ToString();
                        objAnnuityDetails.percentageDropOnDeathOfNominatedLife = Convert.ToInt32(AnnuityDetail["percentageDropOnDeathOfNominatedLife"].ToString());
                        objAnnuityDetails.percentageDropOnDeathOfOtherLife = Convert.ToInt32(AnnuityDetail["percentageDropOnDeathOfOtherLife"].ToString());
                        objAnnuityDetails.purchasePriceIndicator = AnnuityDetail["purchasePriceIndicator"].ToString();
                    }


                    #endregion MasterEntityAnnuityDetail End.

                    #region MasterEntityGroupDetail Begin.
                    foreach (DataRow GroupDetail in dtCoverage)
                    {
                        objGroupDetails.groupMemberNo = GroupDetail["groupMemberNo"].ToString();
                        objGroupDetails.groupNo = GroupDetail["groupNo"].ToString();
                        objGroupDetails.masterPolicyNo = GroupDetail["masterPolicyNo"].ToString();

                    }

                    #endregion MasterEntityGroupDetail End.

                    #region Invoke UserInfo method begin
                    objUserInfo.partnerID = "UWSaral";
                    objUserInfo.processID = "UWSaral";
                    objUserInfo.ApplicationNo = strPQuoteNo;
                    #endregion Invoke UserInfo method end


                    #region Invoke Service methode begin.

                    objResponce = objInvoke.UpdateContract(objHeader, objOwner, objLifeDetails.ToList(), objJoinLifeDetails.ToList(), objJointOwner, objCoverageList.ToList(), objRider.ToList(), objBeneficiary.ToList(), objFollowup, objAssignee, objDespatch, objPayer, objSpecialTerm.ToList(), objFundDetails.ToList(), objClient, objBankDetails, objMandate, objReceipt, objReptToCont.ToList(), objAgencyDetails, objAnnuityDetails, objGroupDetails, objUserInfo);

                    if (objResponce.errorCode == "0")
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: Contract Modification" + System.Environment.NewLine);
                        if (objChangeObj.Prod_policydetails != null)
                        { 
                            //passed new parameter - RCDCommencementDate, objChangeObj.userLoginDetails._UserID -  4.1 CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
                            objcomm.OnlineProductDetails_Save(strAppType, strAppNo, strPolicyNo, strProdcode, strSumassured, strPolicyTerm, strPremiumpayingterm
                                , strPaymentfrequency, strBasePremium, strTotalpremiumamount, strServiceTaxBasePremium, strMonthlyPayout, ref polResult, RCDCommencementDate, objChangeObj.userLoginDetails._UserID);
                        }
                        strLAPushErrorcode = 0;
                        strLAPushStatus = strLAPushStatus + " Success";

                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: Contract Modification" + System.Environment.NewLine);
                        strLAPushErrorcode = -1;
                        strLAPushStatus = strLAPushStatus + " " + objResponce.errorMessage.ToString();

                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "ContractModification.cs", "ContractModPushService", "E-ServiceCallError", "", "", objResponce.errorMessage.ToString() + System.Environment.NewLine);

                    }

                    // objResponce = objInvoke.UpdateContract(objHeader, objOwner, objLifeDetails, objJoinLifeDetails, objJointOwner, objCoverageList, objRider, objBeneficiary, objFollowup, objAssignee, objDespatch, objPayer, objSpecialTerm, objFundDetails, objClient, objBankDetails, objMandate, objReceipt, objReptToCont, objAgencyDetails, objAnnuityDetails, objGroupDetails, UserName);
                    #endregion Invoke Service methode end.
                }
            }

            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = strLAPushStatus + " Failed";
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR:Service Call Execution ERROR: Contract Modification" + "ERROR MESSAGE:" + Error.Message + System.Environment.NewLine);
            }
        }

        #region 2.1 Start:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
        public string GetRCDDate(string ApplicationNo,DateTime originalCommencementDate,string UserID)
        {

            bool IsBackDate = false;
            bool IsFTCase = false;
            string StrFinalRCDDate = string.Empty;
            DataSet dscheckBcdateandFTcase = new DataLayer().GetIsCheckBackDateandFTcase(ApplicationNo, UserID, "UW");
            if (dscheckBcdateandFTcase != null)
            {
                if (dscheckBcdateandFTcase.Tables[0].Rows.Count > 0)
                {
                    IsBackDate = Convert.ToBoolean(dscheckBcdateandFTcase.Tables[0].Rows[0]["IsBackdate"]);
                }

                if (dscheckBcdateandFTcase.Tables[1].Rows.Count > 0)
                {
                    IsFTCase = Convert.ToBoolean(dscheckBcdateandFTcase.Tables[1].Rows[0]["ISFTCase"]);
                }
            }

            if (IsFTCase)
            {
                DataSet dsdate = new DataLayer().UWGetDatesforRCD(ApplicationNo, UserID, "UW");
                //DateTime? PaymentRealizationDate = null;
                //DateTime? PIVCDate = null;
                DateTime? FTApprovalDate = null;
                if (dsdate.Tables.Count > 0)
                {
                    if (dsdate.Tables[0].Rows.Count > 0)
                    {
                        FTApprovalDate = Convert.ToDateTime(dsdate.Tables[0].Rows[0]["FTApprovalDate"]);

                    }
                    //if (dsdate.Tables[1].Rows.Count > 0)
                    //{
                    //    PIVCDate = Convert.ToDateTime(dsdate.Tables[1].Rows[0]["PIVCDate"]);
                    //}

                }

                List<string> objDatelist = new List<string>();
                if (!string.IsNullOrEmpty(Convert.ToString(FTApprovalDate)))
                {
                    objDatelist.Add(DateFormat(FTApprovalDate).ToString());
                }
                //if (!string.IsNullOrEmpty(Convert.ToString(PIVCDate)))
                //{
                //    objDatelist.Add(DateFormat(PIVCDate).ToString());
                //}
                if (!string.IsNullOrEmpty(Convert.ToString(originalCommencementDate)))
                {
                    objDatelist.Add(DateFormat(originalCommencementDate).ToString());
                }

                RCDListDO objRCDlist = new RCDListDO();
                List<RDCDO> list = new List<RDCDO>();
                if (objDatelist.Count > 0)
                {
                    for (int i = 0; i < objDatelist.Count; i++)
                    {
                        var RCDDO = new RDCDO();
                        RCDDO.Dates = Convert.ToDateTime(objDatelist[i]);
                        objRCDlist.ObjRCDdatelist.Add(RCDDO);
                        list.Add(RCDDO);
                    }
                }

                var FinalRCDDate = objRCDlist.ObjRCDdatelist.Select(d => d.Dates).Max();
                StrFinalRCDDate = DateFormat(FinalRCDDate).ToString();
            }
            else
            {
                StrFinalRCDDate = DateFormat(originalCommencementDate).ToString();
            }
            return StrFinalRCDDate;
        }
        #endregion 2.1 End:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
    }

    #region 2.1 Start:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]



    public class RCDListDO
    {
        public List<RDCDO> ObjRCDdatelist = new List<RDCDO>();
    }
    public class RDCDO
    {
        public DateTime Dates { get; set; }
    }
#endregion 2.1 End:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]

}
