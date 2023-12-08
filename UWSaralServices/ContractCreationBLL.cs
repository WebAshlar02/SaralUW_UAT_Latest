using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Platform.Utilities.LoggerFramework;

public class ContractCreationBLL
{
    string UserID = "MFL00241", UserName = "FMFL00241", Branchcode = "KAN", UserBranch = "Kanpur Branch Office", ProcessName = "Manual Data Entry", AppName = "FGLIFE";
    int i;
    int _RJ;
    int _JLK;
    int _CL;
    int _RM;
    int _BN;
    int _SO;
    int _FP;
    int _RCQ;
    public void ContractModPushService(string strPQuoteNo, DataSet _dsContract)
    {
        try
        {
            //Logger.Info(strPQuoteNo + " STAG 2 :PageName :ContractCreationBLL.cs // MethodeName :ContractModPushService");
            ContractCreationService.ServiceClient objInvoke = new ContractCreationService.ServiceClient();
            ContractCreationService.MasterEntityHeaderDetail objHeader = new ContractCreationService.MasterEntityHeaderDetail();
            ContractCreationService.MasterEntityOwnerDetail objOwner = new ContractCreationService.MasterEntityOwnerDetail();
            ContractCreationService.MasterEntityLifeDetailList[] objLifeDetails = null;
            ContractCreationService.MasterEntityJointLifeDetailList[] objJoinLifeDetails = null;
            ContractCreationService.MasterEntityJointOwnerDetail objJointOwner = new ContractCreationService.MasterEntityJointOwnerDetail();
            ContractCreationService.MasterEntityCoverageDetailList[] objCoverageList = null;
            ContractCreationService.MasterEntityRiderDetailList[] objRider = null;
            ContractCreationService.MasterEntityBeneficiaryList[] objBeneficiary = null;
            ContractCreationService.MasterEntityFollowUp objFollowup = new ContractCreationService.MasterEntityFollowUp();
            ContractCreationService.MasterEntityAssignee objAssignee = new ContractCreationService.MasterEntityAssignee();
            ContractCreationService.MasterEntityDespatchCorrespondence objDespatch = new ContractCreationService.MasterEntityDespatchCorrespondence();
            ContractCreationService.MasterEntityPayerDetail objPayer = new ContractCreationService.MasterEntityPayerDetail();
            ContractCreationService.MasterEntitySpecialTermList[] objSpecialTerm = null;
            ContractCreationService.MasterEntityFundDetailList[] objFundDetails = null;
            ContractCreationService.MasterEntityClient objClient = new ContractCreationService.MasterEntityClient();
            ContractCreationService.MasterEntityClientBankAccount objBankDetails = new ContractCreationService.MasterEntityClientBankAccount();
            ContractCreationService.MasterEntityMandate objMandate = new ContractCreationService.MasterEntityMandate();
            ContractCreationService.MasterEntityPremiumReceipt objReceipt = new ContractCreationService.MasterEntityPremiumReceipt();
            ContractCreationService.MasterEntityReceiptToContractList[] objReptToCont = null;
            ContractCreationService.MasterEntityAgencyDetail objAgencyDetails = new ContractCreationService.MasterEntityAgencyDetail();
            ContractCreationService.MasterEntityAnnuityDetail objAnnuityDetails = new ContractCreationService.MasterEntityAnnuityDetail();
            ContractCreationService.MasterEntityGroupDetail objGroupDetails = new ContractCreationService.MasterEntityGroupDetail();
           
            ContractCreationService.Master objResponce = new ContractCreationService.Master();
            string strUserid = string.Empty;
            
            _dsContract.Tables[0].TableName = "CONTOBJ";
            _dsContract.Tables[1].TableName = "CLNTDTLS";
            _dsContract.Tables[2].TableName = "FOLLOWUPDTLS";
            _dsContract.Tables[3].TableName = "FUNDDTLS";
            _dsContract.Tables[4].TableName = "RIDERDTLS";
            _dsContract.Tables[5].TableName = "RCPTDTLS";
            _dsContract.Tables[6].TableName = "MANDTLS";

            for (i = 0; i < _dsContract.Tables["CONTOBJ"].Rows.Count; i++)
            {
                #region MasterEntityHeaderDetail Begins.
                objHeader.accountType = _dsContract.Tables["CONTOBJ"].Rows[i]["accountType"].ToString();
                objHeader.agentNo = _dsContract.Tables["CONTOBJ"].Rows[i]["agentNo"].ToString();
               objHeader.billCD_IND = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[i]["billCD"].ToString());
                objHeader.billCurrency = _dsContract.Tables["CONTOBJ"].Rows[i]["billCurr"].ToString();
               objHeader.billFrequency = _dsContract.Tables["CONTOBJ"].Rows[i]["billFrequency"].ToString();
               objHeader.billRenewalDate = _dsContract.Tables["CONTOBJ"].Rows[i]["billRenewalDate"].ToString();
               objHeader.contractCurrency = _dsContract.Tables["CONTOBJ"].Rows[i]["contractCurrency"].ToString();
                objHeader.contractNo = _dsContract.Tables["CONTOBJ"].Rows[i]["contractNo"].ToString();
                objHeader.contractType = _dsContract.Tables["CONTOBJ"].Rows[i]["contractType"].ToString();
                objHeader.methodOfPayment = _dsContract.Tables["CONTOBJ"].Rows[i]["methodOfPayment"].ToString();
                objHeader.noOfPolicy = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[i]["noOfPolicy"].ToString());
                objHeader.originalCommencementDate = _dsContract.Tables["CONTOBJ"].Rows[i]["originalCommencementDate"].ToString();
                objHeader.proposalDate = _dsContract.Tables["CONTOBJ"].Rows[i]["proposalDate"].ToString();
                objHeader.proposalRecievedDate = _dsContract.Tables["CONTOBJ"].Rows[i]["proposalRecievedDate"].ToString();
                objHeader.register = _dsContract.Tables["CONTOBJ"].Rows[i]["register"].ToString();
                objHeader.statisticalCodeA = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeA"].ToString();
                objHeader.statisticalCodeB = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeB"].ToString();
                objHeader.statisticalCodeC = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeC"].ToString();
                objHeader.statisticalCodeD = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeD"].ToString();
                objHeader.statisticalCodeE = _dsContract.Tables["CONTOBJ"].Rows[i]["statisticalCodeE"].ToString();
                objHeader.temporaryReceiptNo = _dsContract.Tables["CONTOBJ"].Rows[i]["temporaryReceiptNo"].ToString();
                objHeader.UWDecisionDate = _dsContract.Tables["CONTOBJ"].Rows[i]["UWDecisionDate"].ToString();
                #endregion MasterEntityHeaderDetail End.

                #region MasterEntityOwnerDetail Begins.
                string _strOwnerClientID = string.Empty;

                DataRow[] rowtable1 = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='Nominee'");
                foreach (DataRow row in rowtable1)
                {
                    _strOwnerClientID = row["CLT_clientId_CLNTNUM"].ToString();
                }

                //objOwner.ownerParty = _dsContract.Tables[1].Rows[i]["ownerParty"].ToString();
                objOwner.ownerParty = _strOwnerClientID;
                #endregion MasterEntityOwnerDetail End.

                #region MasterEntityLifeDetailList Begins.
                DataRow[] LifeEntity = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='LA'");
                objLifeDetails = new ContractCreationService.MasterEntityLifeDetailList[LifeEntity.Length];
                int count = 0;
                int EntityID = 1;
                foreach (DataRow LifeEntityRow in LifeEntity)
                {
                    objLifeDetails[count] = new ContractCreationService.MasterEntityLifeDetailList();
                    objLifeDetails[count].ANBAge = Convert.ToInt32(LifeEntityRow["ANBAge"].ToString());
                    objLifeDetails[count].dateOfBirth = LifeEntityRow["dateOfBirth"].ToString();
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

                DataRow[] JointLifeEntity = _dsContract.Tables["CLNTDTLS"].Select("relationshipToLifeAssured='LA'");
                objJoinLifeDetails = new ContractCreationService.MasterEntityJointLifeDetailList[JointLifeEntity.Length];
                count = 0;
                EntityID = 1;
                foreach (DataRow JointLifeEntityRow in JointLifeEntity)
                {

                    objJoinLifeDetails[count] = new ContractCreationService.MasterEntityJointLifeDetailList();
                    objJoinLifeDetails[count].ANBAge = Convert.ToInt32(JointLifeEntityRow["ANBAge"].ToString());
                    objJoinLifeDetails[count].dateOfBirth = JointLifeEntityRow["dateOfBirth"].ToString();
                    objJoinLifeDetails[count].jointLifeEntID = "J0" + EntityID;
                    //objJoinLifeDetails[count].jointLifeEntID = JointLifeEntityRow["lifeEntID"].ToString();
                    objJoinLifeDetails[count].jointLifeNo = JointLifeEntityRow["jointLifeNo"].ToString();
                   // objJoinLifeDetails[count].jointLifeParent = JointLifeEntityRow["jointLifeParent"].ToString();
                    objJoinLifeDetails[count].jointLifeParent = "L0" + EntityID;
                    objJoinLifeDetails[count].jointLifeParty = JointLifeEntityRow["CLT_clientId_CLNTNUM"].ToString();
                    objJoinLifeDetails[count].lifeNo = JointLifeEntityRow["lifeNo"].ToString();
                    objJoinLifeDetails[count].lifeSelection = JointLifeEntityRow["lifeSelction"].ToString();
                    objJoinLifeDetails[count].occupationCode = JointLifeEntityRow["occupationCode"].ToString();
                    objJoinLifeDetails[count].relationshipToLifeInsured = JointLifeEntityRow["relationshipToLifeAssured"].ToString();
                    objJoinLifeDetails[count].sex = JointLifeEntityRow["sex"].ToString();
                    objJoinLifeDetails[count].smokingIndicator = JointLifeEntityRow["smokingIndicator"].ToString();
                    count++;
                    EntityID++;
                }
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

                // objJointOwner.jointOwnerParty = _dsContract.Tables["joinLife"].Rows[_JLK]["jointOwnerParty"].ToString();
                objJointOwner.jointOwnerParty = _strOwnerClientID;
                #endregion MasterEntityJointOwnerDetail end.

                #region MasterEntityCoverageDetailList Begins.
                EntityID = 1;
                objCoverageList = new ContractCreationService.MasterEntityCoverageDetailList[_dsContract.Tables["CONTOBJ"].Rows.Count];
                 for (_CL = 0; _CL < _dsContract.Tables["CONTOBJ"].Rows.Count; _CL++)
                {

                    objCoverageList[_CL] = new ContractCreationService.MasterEntityCoverageDetailList();
                    objCoverageList[_CL].coverageEndID = "C0" + EntityID;
                    //objCoverageList[_CL].coverageEndID = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageEndID"].ToString();
                    objCoverageList[_CL].coverageNo = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageNo"].ToString();
                    //objCoverageList[_CL].coverageParent = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageParent"].ToString();
                    objCoverageList[_CL].coverageParent = "L0" + EntityID;
                    objCoverageList[_CL].coverageRiderTable = _dsContract.Tables["CONTOBJ"].Rows[_CL]["coverageRiderTable"].ToString();
                    objCoverageList[_CL].lifeNo = _dsContract.Tables["CONTOBJ"].Rows[_CL]["lifeNo"].ToString();
                    objCoverageList[_CL].lumpSumContribution = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["lumpSumContribution"].ToString());
                    objCoverageList[_CL].mortalityClass = _dsContract.Tables["CONTOBJ"].Rows[_CL]["mortalityClass"].ToString();
                    objCoverageList[_CL].premiumCessadtionAge = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["premiumCessadtionAge"].ToString());
                    
                    objCoverageList[_CL].premiumCessadtionTerm = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["premiumCessadtionTerm"].ToString());

                    objCoverageList[_CL].riderNo = _dsContract.Tables["CONTOBJ"].Rows[_CL]["riderNo"].ToString();
                    objCoverageList[_CL].riskCessationAge = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["riskCessationAge"].ToString());
                    objCoverageList[_CL].riskCessationTerm = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["riskCessationTerm"].ToString());
                    objCoverageList[_CL].sumAssured = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[_CL]["sumAssured"].ToString());
                    objCoverageList[_CL].unitReserveAllocationDate = _dsContract.Tables["CONTOBJ"].Rows[_CL]["unitReserveAllocationDate"].ToString();
                    objCoverageList[_CL].unitReserveUnits = _dsContract.Tables["CONTOBJ"].Rows[_CL]["unitReserveUnits"].ToString();
                    EntityID++;
                }
                #endregion MasterEntityCoverageDetailList End.

                #region MasterEntityRiderDetailList Begins.
                 objRider = new ContractCreationService.MasterEntityRiderDetailList[_dsContract.Tables["RIDERDTLS"].Rows.Count];
                 EntityID = 1;
                 for (_RM = 0; _RM < _dsContract.Tables["RIDERDTLS"].Rows.Count; _RM++)
                {

                    objRider[_RM] = new ContractCreationService.MasterEntityRiderDetailList();
                    objRider[_RM].coverageNo = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["coverageNo"].ToString();
                    objRider[_RM].coverageRiderTable = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["coverageRiderTable"].ToString();
                    objRider[_RM].lifeNo = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["lifeNo"].ToString();
                    objRider[_RM].mortalityClass = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["mortalityClass"].ToString();
                   
                    objRider[_RM].premiumCessationAge = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationAge"].ToString());
                    if (_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationTerm"].ToString() != "")
                    {
                        objRider[_RM].premiumCessationTerm = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["premiumCessationTerm"].ToString());
                    }
                    objRider[_RM].riderEntID = "R0" + EntityID;
                    //objRider[_RM].riderEntID = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderEntID"].ToString();
                    objRider[_RM].riderNo = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderNo"].ToString();
                    //objRider[_RM].riderParent = _dsContract.Tables["RIDERDTLS"].Rows[_RM]["riderParent"].ToString();
                    objRider[_RM].riderParent = "C0" + EntityID; ;
                    objRider[_RM].riskCessationAge = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationAge"].ToString());
                    if (_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationTerm"].ToString() != "")
                    {
                        objRider[_RM].riskCessationTerm = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["riskCessationTerm"].ToString());
                    }
                   
                    objRider[_RM].sumAssured = Convert.ToInt32(_dsContract.Tables["RIDERDTLS"].Rows[_RM]["sumAssured"].ToString());
                    EntityID++;
                }
                #endregion MasterEntityRiderDetailList End.

                #region MasterEntityBeneficiaryList Begins.
               // objBeneficiary = new ContractCreationService.MasterEntityBeneficiaryList[_dsContract.Tables["Beneficiary"].Rows.Count];
                 objBeneficiary = new ContractCreationService.MasterEntityBeneficiaryList[rowtable1.Length];
                 count = 0;
                foreach (DataRow row in rowtable1)
                {

                    objBeneficiary[count] = new ContractCreationService.MasterEntityBeneficiaryList();
                    objBeneficiary[count].beneficiaryCode = row["beneficiaryCode"].ToString();
                    objBeneficiary[count].beneficiaryParty = row["beneficiaryParty"].ToString();
                    objBeneficiary[count].beneficiaryPercentage = Convert.ToInt32(row["beneficiaryPercentage"].ToString());
                    objBeneficiary[count].beneficiaryRelationship = row["relationshipToLifeAssured"].ToString();
                    objBeneficiary[count].effectiveDate = row["effectiveDate"].ToString();
                    count++;
                }
                //for (_BN = 0; _BN < _dsContract.Tables["Beneficiary"].Rows.Count; _BN++)
                //{
                //    objBeneficiary[_BN] = new ContractCreationService.MasterEntityBeneficiaryList();
                //    objBeneficiary[_BN].beneficiaryCode = _dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryCode"].ToString();
                //    objBeneficiary[_BN].beneficiaryParty = _dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryParty"].ToString();
                //    objBeneficiary[_BN].beneficiaryPercentage = Convert.ToInt32(_dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryPercentage"].ToString());
                //    objBeneficiary[_BN].beneficiaryRelationship = _dsContract.Tables["Beneficiary"].Rows[_BN]["beneficiaryRelationship"].ToString();
                //    objBeneficiary[_BN].effectiveDate = _dsContract.Tables["Beneficiary"].Rows[_BN]["effectiveDate"].ToString();
                //}
                #endregion MasterEntityBeneficiaryList End.

                #region MasterEntityFollowUp Begins.
                objFollowup.doctorClientNo = _dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["doctorClientNo"].ToString();
                objFollowup.followUpCode = _dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followUpCode"].ToString();
                objFollowup.followUpNo = Convert.ToInt32(_dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followUpNo"].ToString());
                objFollowup.followUpStatus = _dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followUpStatus"].ToString();
                objFollowup.followuUpReminderDate = _dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["followuUpReminderDate"].ToString();
                objFollowup.lifeNo = Convert.ToInt32(_dsContract.Tables["FOLLOWUPDTLS"].Rows[0]["lifeNo"].ToString());

                #endregion MasterEntityFollowUp End.

                #region MasterEntityAssignee Begins.
                //LA Loop
                foreach (DataRow JointLifeEntityRow in JointLifeEntity) {
                    objAssignee.assigneeParty = JointLifeEntityRow["assigneeParty"].ToString();
                    objAssignee.commissionFromDate = JointLifeEntityRow["commissionFromDate"].ToString();
                    objAssignee.commissionToDate = JointLifeEntityRow["commissionToDate"].ToString();
                    objAssignee.incomeProof = JointLifeEntityRow["incomeProof"].ToString();
                    objAssignee.incomeProofIndicator = JointLifeEntityRow["incomeProofIndicator"].ToString();
                    objAssignee.reasonCode = JointLifeEntityRow["reasonCode"].ToString();
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
                foreach (DataRow row in _dsContract.Tables["CONTOBJ"].Rows) {
                    objDespatch.despatchParty = row["despatchParty"].ToString();
                }
               
                #endregion MasterEntityDespatchCorrespondence End.

                #region MasterEntityPayerDetail Begins.
                //Nomine loop
                foreach (DataRow row in rowtable1)
                {
                    objPayer.payerParty = row["CLT_clientId_CLNTNUM"].ToString();
                }
               // objPayer.payerParty = _dsContract.Tables[""].Rows[0]["payerParty"].ToString();
                #endregion MasterEntityPayerDetail End.

                #region MasterEntitySpecialTermList Begins.
                objSpecialTerm = new ContractCreationService.MasterEntitySpecialTermList[_dsContract.Tables["CONTOBJ"].Rows.Count];
                count = 0;
                EntityID = 1;
                foreach (DataRow row in _dsContract.Tables["CONTOBJ"].Rows)
                {
                    objSpecialTerm[count] = new ContractCreationService.MasterEntitySpecialTermList();
                    objSpecialTerm[count].adjustmentCode = row["adjustmentCode"].ToString();
                    objSpecialTerm[count].adjustmentDuration = Convert.ToInt32(row["adjustmentDuration"].ToString());
                    objSpecialTerm[count].adjustmentPercentage = Convert.ToInt32(row["adjustmentPercentage"].ToString());
                    objSpecialTerm[count].ageRating = Convert.ToInt32(row["ageRating"].ToString());
                    objSpecialTerm[count].lineOfSublife = row["lineOfSublife"].ToString();
                    objSpecialTerm[count].rateAdjustment = Convert.ToInt32(row["rateAdjustment"].ToString());
                    objSpecialTerm[count].reassuranceIndictor = row["reassuranceIndictor"].ToString();
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
                objFundDetails = new ContractCreationService.MasterEntityFundDetailList[_dsContract.Tables["FUNDDTLS"].Rows.Count];
                for (_FP = 0; _FP < _dsContract.Tables["FUNDDTLS"].Rows.Count; _FP++)
                {
                    objFundDetails[_FP] = new ContractCreationService.MasterEntityFundDetailList();
                    //objFundDetails[_FP].fundEntID = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["fundEntID"].ToString();
                    objFundDetails[_FP].fundEntID = "F0" + EntityID;
                    //objFundDetails[_FP].fundParent = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["fundParent"].ToString();
                    objFundDetails[_FP].fundParent = "L0" + EntityID;
                    objFundDetails[_FP].unitLinkedFund01 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund01"].ToString();
                    objFundDetails[_FP].unitLinkedFund02 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund02"].ToString();
                    objFundDetails[_FP].unitLinkedFund03 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund03"].ToString();
                    objFundDetails[_FP].unitLinkedFund04 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund04"].ToString();
                    objFundDetails[_FP].unitLinkedFund05 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund05"].ToString();
                    objFundDetails[_FP].unitLinkedFund06 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund06"].ToString();
                    objFundDetails[_FP].unitLinkedFund07 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund07"].ToString();
                    objFundDetails[_FP].unitLinkedFund08 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund08"].ToString();
                    objFundDetails[_FP].unitLinkedFund09 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund09"].ToString();
                    objFundDetails[_FP].unitLinkedFund10 = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitLinkedFund10"].ToString();
                    objFundDetails[_FP].unitPercent01 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent01"].ToString());
                    objFundDetails[_FP].unitPercent02 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent02"].ToString());
                    objFundDetails[_FP].unitPercent03 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent03"].ToString());
                    objFundDetails[_FP].unitPercent04 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent04"].ToString());
                    objFundDetails[_FP].unitPercent05 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent05"].ToString());
                    objFundDetails[_FP].unitPercent06 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent06"].ToString());
                    objFundDetails[_FP].unitPercent07 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent07"].ToString());
                    objFundDetails[_FP].unitPercent08 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent08"].ToString());
                    objFundDetails[_FP].unitPercent09 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent09"].ToString());
                    objFundDetails[_FP].unitPercent10 = Convert.ToInt32(_dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercent10"].ToString());
                    objFundDetails[_FP].unitPercentage = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitPercentage"].ToString();
                    objFundDetails[_FP].unitVirtualFundSplitMethod = _dsContract.Tables["FUNDDTLS"].Rows[_FP]["unitVirtualFundSplitMethod"].ToString();

                    EntityID++;
                }

                #endregion MasterEntityFundDetailList End.


                #region MasterEntityClient Begins.

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
                    objClient.dateOfBirth = JointLifeEntityRow["dateOfBirth"].ToString();
                    objClient.directMailIndicator = JointLifeEntityRow["directMailIndicator"].ToString();
                    objClient.documentNo = JointLifeEntityRow["documentNo"].ToString();
                    objClient.forTheAttentionOf = JointLifeEntityRow["forTheAttentionOf"].ToString();
                    objClient.language = JointLifeEntityRow["language"].ToString();
                    objClient.longGivenName = JointLifeEntityRow["longGivenName"].ToString();
                    objClient.longSurname = JointLifeEntityRow["longSurname"].ToString();
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
                objReptToCont = new ContractCreationService.MasterEntityReceiptToContractList[_dsContract.Tables["RCPTDTLS"].Rows.Count];
                for (_RCQ = 0; _RCQ < _dsContract.Tables["RCPTDTLS"].Rows.Count; _RCQ++)
                {
                    objReptToCont[_RCQ] = new ContractCreationService.MasterEntityReceiptToContractList();
                    objReptToCont[_RCQ].receiptNo = _dsContract.Tables["RCPTDTLS"].Rows[_RCQ]["receiptNo"].ToString();
                    objReptToCont[_RCQ].tranSequence = _dsContract.Tables["RCPTDTLS"].Rows[_RCQ]["tranSequence"].ToString();
                }
                #endregion MasterEntityReceiptToContractList End.

                #region MasterEntityMandate begins.
                if (_dsContract.Tables["MANDTLS"].Rows.Count > 0) {
                   
                    objMandate.bankaccessApprovalNo = _dsContract.Tables["MANDTLS"].Rows[0]["bankaccessApprovalNo"].ToString();
                    objMandate.bankAccountNo = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountNo"].ToString();
                    objMandate.bankKey = _dsContract.Tables["MANDTLS"].Rows[0]["bankKey"].ToString();
                    objMandate.clientNo = _strOwnerClientID;
                    objMandate.effectiveDate = _dsContract.Tables["MANDTLS"].Rows[0]["effectiveDate"].ToString();
                    objMandate.mandateAmount = Convert.ToInt32(_dsContract.Tables["MANDTLS"].Rows[0]["mandateAmount"].ToString());
                    objMandate.mandateReferenceNo = _dsContract.Tables["MANDTLS"].Rows[0]["mandateReferenceNo"].ToString();
                    objMandate.mandateStatusCode = _dsContract.Tables["MANDTLS"].Rows[0]["mandateStatusCode"].ToString();
                    objMandate.summaryIndicator = _dsContract.Tables["MANDTLS"].Rows[0]["summaryIndicator"].ToString();
                    objMandate.timesToUse = Convert.ToInt32(_dsContract.Tables["MANDTLS"].Rows[0]["timesToUse"].ToString());
                }
               
                #endregion  MasterEntityMandate End.




                #region MasterEntityClientBankAccount Begins.
                if (_dsContract.Tables["MANDTLS"].Rows.Count > 0) {
                    objBankDetails.bankAccountDescription = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountDescription"].ToString();
                    objBankDetails.bankAccountNo = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountNo"].ToString();
                    objBankDetails.bankAccountType = _dsContract.Tables["MANDTLS"].Rows[0]["bankAccountType"].ToString();
                    objBankDetails.bankKey = _dsContract.Tables["MANDTLS"].Rows[0]["bankKey"].ToString();
                    objBankDetails.bankSecurityCode = _dsContract.Tables["MANDTLS"].Rows[0]["bankSecurityCode"].ToString();
                    //  objBankDetails.clientBankDetails = _dsContract.Tables["MANDTLS"].Rows[_FP]["clientBankDetails"].ToString(); 
                    objBankDetails.clientBankDetails = _strOwnerClientID;
                    objBankDetails.currencyCode = _dsContract.Tables["MANDTLS"].Rows[0]["currencyCode"].ToString();
                    objBankDetails.dateFrom = _dsContract.Tables["MANDTLS"].Rows[0]["dateFrom"].ToString();

                }
               
                #endregion MasterEntityClientBankAccount End.

               

                #region MasterEntityPremiumReceipt Begin.
                objReceipt.amountOriginalCurrency = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["amountOriginalCurrency"].ToString());
                objReceipt.bankCode = _dsContract.Tables["CONTOBJ"].Rows[0]["bankCode"].ToString();
                objReceipt.bankDescription01 = _dsContract.Tables["CONTOBJ"].Rows[0]["bankDescription01"].ToString();
                objReceipt.bankDescription02 = _dsContract.Tables["CONTOBJ"].Rows[0]["bankDescription02"].ToString();
                objReceipt.bankDescription03 = _dsContract.Tables["CONTOBJ"].Rows[0]["bankDescription03"].ToString();
                objReceipt.bankedFlag = _dsContract.Tables["CONTOBJ"].Rows[0]["bankedFlag"].ToString();
                objReceipt.bankKey = _dsContract.Tables["CONTOBJ"].Rows[0]["bankKey"].ToString();
                objReceipt.chequeDate = _dsContract.Tables["CONTOBJ"].Rows[0]["chequeDate"].ToString();
                objReceipt.chequeNo = _dsContract.Tables["CONTOBJ"].Rows[0]["chequeNo"].ToString();
                objReceipt.currencyRate = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["currencyRate"].ToString());
                objReceipt.dissectionNo = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["dissectionNo"].ToString());
                objReceipt.documentAmountOriginalCurrency = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["documentAmountOriginalCurrency"].ToString());
                objReceipt.entity = _dsContract.Tables["CONTOBJ"].Rows[0]["entity"].ToString();
                objReceipt.originalCurrency = _dsContract.Tables["CONTOBJ"].Rows[0]["originalCurrency"].ToString();
                objReceipt.paymentType = _dsContract.Tables["CONTOBJ"].Rows[0]["paymentType"].ToString();
                objReceipt.protectIND = _dsContract.Tables["CONTOBJ"].Rows[0]["protectIND"].ToString();
                objReceipt.receivedFromCode = _dsContract.Tables["CONTOBJ"].Rows[0]["receivedFromCode"].ToString();
                objReceipt.receivedFromNo = _dsContract.Tables["CONTOBJ"].Rows[0]["receivedFromNo"].ToString();
                objReceipt.subAccountCode = _dsContract.Tables["CONTOBJ"].Rows[0]["subAccountCode"].ToString();
                objReceipt.subAccountType = _dsContract.Tables["CONTOBJ"].Rows[0]["subAccountType"].ToString();
                objReceipt.timestamp = _dsContract.Tables["CONTOBJ"].Rows[0]["timestamp"].ToString();
                objReceipt.transactionDate = _dsContract.Tables["CONTOBJ"].Rows[0]["transactionDate"].ToString();
                #endregion  MasterEntityPremiumReceipt End.


               

                #region MasterEntityAgencyDetail Begins.
                objAgencyDetails.agentNo = _dsContract.Tables["CONTOBJ"].Rows[0]["agentNo"].ToString();
                objAgencyDetails.frontEndRegionCode01 = _dsContract.Tables["CONTOBJ"].Rows[0]["frontEndRegionCode01"].ToString();
                objAgencyDetails.frontEndRegionCode02 = _dsContract.Tables["CONTOBJ"].Rows[0]["frontEndRegionCode02"].ToString();
                objAgencyDetails.frontEndRegionCode03 = _dsContract.Tables["CONTOBJ"].Rows[0]["frontEndRegionCode03"].ToString();
                objAgencyDetails.frontEndRegionCode04 = _dsContract.Tables["CONTOBJ"].Rows[0]["frontEndRegionCode04"].ToString();
                objAgencyDetails.newAgentAreaCode = _dsContract.Tables["CONTOBJ"].Rows[0]["newAgentAreaCode"].ToString();
                objAgencyDetails.reportingAgent01 = _dsContract.Tables["CONTOBJ"].Rows[0]["reportingAgent01"].ToString();
                objAgencyDetails.reportingAgent02 = _dsContract.Tables["CONTOBJ"].Rows[0]["reportingAgent02"].ToString();
                objAgencyDetails.sourceOfBusiness01 = _dsContract.Tables["CONTOBJ"].Rows[0]["sourceOfBusiness01"].ToString();
                objAgencyDetails.sourceOfBusiness02 = _dsContract.Tables["CONTOBJ"].Rows[0]["sourceOfBusiness02"].ToString();

                #endregion MasterEntityAgencyDetail end.

                #region MasterEntityAnnuityDetail Begins.
                objAnnuityDetails.annuityPaymentInAdvance = _dsContract.Tables["CONTOBJ"].Rows[0]["annuityPaymentInAdvance"].ToString();
                objAnnuityDetails.annuityPaymentsInArrears = _dsContract.Tables["CONTOBJ"].Rows[0]["annuityPaymentsInArrears"].ToString();
                objAnnuityDetails.annuityPaymentsWithoutProportion = _dsContract.Tables["CONTOBJ"].Rows[0]["annuityPaymentsWithoutProportion"].ToString();
                objAnnuityDetails.annuityPaymentsWithProportion = _dsContract.Tables["CONTOBJ"].Rows[0]["annuityPaymentsWithProportion"].ToString();
                objAnnuityDetails.capitalContentAnnuityPayment = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["capitalContentAnnuityPayment"].ToString());
                objAnnuityDetails.frequencyOfAnnuityPAyments = _dsContract.Tables["CONTOBJ"].Rows[0]["frequencyOfAnnuityPAyments"].ToString();
                objAnnuityDetails.guaranteedPaymentPeriod = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["guaranteedPaymentPeriod"].ToString());
                objAnnuityDetails.interestRateInAnnuityCalculations = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["interestRateInAnnuityCalculations"].ToString());
                objAnnuityDetails.NominatedLife = _dsContract.Tables["CONTOBJ"].Rows[0]["NominatedLife"].ToString();
                objAnnuityDetails.percentageDropOnDeathOfNominatedLife = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["percentageDropOnDeathOfNominatedLife"].ToString());
                objAnnuityDetails.percentageDropOnDeathOfOtherLife = Convert.ToInt32(_dsContract.Tables["CONTOBJ"].Rows[0]["percentageDropOnDeathOfOtherLife"].ToString());
                objAnnuityDetails.purchasePriceIndicator = _dsContract.Tables["CONTOBJ"].Rows[0]["purchasePriceIndicator"].ToString();

                #endregion MasterEntityAnnuityDetail End.


                #region MasterEntityGroupDetail Begin.

                objGroupDetails.groupMemberNo = _dsContract.Tables["CONTOBJ"].Rows[0]["groupMemberNo"].ToString();
                objGroupDetails.groupNo = _dsContract.Tables["CONTOBJ"].Rows[0]["groupNo"].ToString();
                objGroupDetails.masterPolicyNo = _dsContract.Tables["CONTOBJ"].Rows[0]["masterPolicyNo"].ToString();

                #endregion MasterEntityGroupDetail End.

                #region Invoke Service methode begin.

                
                //objResponce = objInvoke.CreateContract(objHeader, objOwner, objLifeDetails, objJoinLifeDetails, objJointOwner, objCoverageList, objRider, objBeneficiary, objFollowup, objAssignee, objDespatch, objPayer, objSpecialTerm, objFundDetails, objClient, objBankDetails, objMandate, objReceipt, objReptToCont, objAgencyDetails, objAnnuityDetails, objGroupDetails, UserName);
                //
                objResponce = objInvoke.UpdateContract(objHeader, objOwner, objLifeDetails.ToList(), objJoinLifeDetails.ToList(), objJointOwner, objCoverageList.ToList(), objRider.ToList(), objBeneficiary.ToList(), objFollowup, objAssignee, objDespatch, objPayer, objSpecialTerm.ToList(), objFundDetails.ToList(), objClient, objBankDetails, objMandate, objReceipt, objReptToCont.ToList(), objAgencyDetails, objAnnuityDetails, objGroupDetails, strUserid);
               // objResponce = objInvoke.UpdateContract(objHeader, objOwner, objLifeDetails, objJoinLifeDetails, objJointOwner, objCoverageList, objRider, objBeneficiary, objFollowup, objAssignee, objDespatch, objPayer, objSpecialTerm, objFundDetails, objClient, objBankDetails, objMandate, objReceipt, objReptToCont, objAgencyDetails, objAnnuityDetails, objGroupDetails, UserName);
                #endregion Invoke Service methode end.
            }
        }
        catch (Exception Error)
        {
            CommonService.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "ContractModification.cs", "ContractModPushService", "E-Error", "", "", Error.ToString());
        }
    }

 

}