using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralDecision;
using System.Data;
using UWSaralObjects;
using UWSaralServices;
using Platform.Utilities.LoggerFramework;
using System.Diagnostics;
namespace TPAMEDICALINTEGRATIONSYNC
{
    class Program
    {                
        static void Main(string[] args)
        {
            //check whether application is already running or not
            if (true)
            {
                TPARegisterationSummary objTPARegisterationSummary = new TPARegisterationSummary();
                Program objprogram = new Program();
                DataSet _ds = new DataSet();
                List<UWSaralObjects.TPARegisteration> LstTpaRegisteration = new List<UWSaralObjects.TPARegisteration>();

                //save service called 
                Logger.Error("PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :Main" + System.Environment.NewLine);
                objprogram.SaveMedicalTest();

                //fetch medical report
                Logger.Error("PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :Main" + System.Environment.NewLine);
                objprogram.GetServiceDataMedicalTest(ref _ds);

                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        //fill object
                        Program objProgram = new Program();
                        UWSaralObjects.TPARegisteration objTPARegisteration = new UWSaralObjects.TPARegisteration();
                        Logger.Error("PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :Main" + System.Environment.NewLine);
                        objprogram.FillTPARegisteration(objTPARegisteration, _ds.Tables[0].Rows[i]);
                        //call service 
                        TPARegis objTpaRegis = new TPARegis();
                        Logger.Error("PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :Main "+objTPARegisteration.ProposalNo + System.Environment.NewLine);
                        objTpaRegis.PushDataIntoTPARegisteration(objTPARegisteration);
                        LstTpaRegisteration.Add(objTPARegisteration);
                    }
                    objTPARegisterationSummary.LstTPARegisteration = LstTpaRegisteration;
                }
                Logger.Error("PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :Main" + System.Environment.NewLine);
                objprogram.UpdateTPARegisterRefNo(objTPARegisterationSummary.DtTPARegisteration);                
            }
        }
        private void SaveMedicalTest()
        {
            Logger.Error("STAG 3 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :SaveMedicalTest Start" + System.Environment.NewLine);
            UWSaralDecision.CommFun objComm = new UWSaralDecision.CommFun();
            DataSet _ds = new DataSet();
            objComm.PostTpaMedicalTest(ref _ds);
            Logger.Error("STAG 3 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :SaveMedicalTest End" + System.Environment.NewLine);
        }

        private void GetServiceDataMedicalTest(ref DataSet _ds)
        {
            Logger.Error("STAG 4 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :GetServiceDataMedicalTest Start" + System.Environment.NewLine);
            UWSaralDecision.CommFun objComm = new UWSaralDecision.CommFun();
            objComm.GetTpaMedicalTest(ref _ds);
            Logger.Error("STAG 4 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :GetServiceDataMedicalTest End" + System.Environment.NewLine);

        }
        private void FillTPARegisteration(UWSaralObjects.TPARegisteration objRegisteration, DataRow dr)
        {
            try
            {
                Logger.Error("STAG 5 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :FillTPARegisteration Start" + System.Environment.NewLine);                
                objRegisteration.ProposalNo = Convert.ToString(dr["ProposalNo"]);
                objRegisteration.UserName = Convert.ToString(dr["UserName"]);
                objRegisteration.Password = Convert.ToString(dr["Password"]);
                objRegisteration.ProposalDate = Convert.ToString(dr["ProposalDate"]);
                objRegisteration.ProposerName = Convert.ToString(dr["CLIENTNAME"]);
                objRegisteration.Gender = Convert.ToString(dr["GENDER"]);
                objRegisteration.Address1 = Convert.ToString(dr["ADDRESS1"]);
                objRegisteration.City = Convert.ToString(dr["CITY"]);
                objRegisteration.State = Convert.ToString(dr["STATE"]);
                objRegisteration.Pincode = Convert.ToString(dr["PINCODE"]);
                objRegisteration.MobileNo = Convert.ToString(dr["MBLPHONE"]);
                objRegisteration.HNIFlag = Convert.ToString(dr["HNIFlag"]);
                objRegisteration.HomeVisitFlag = Convert.ToString(dr["HomeVisitFlag"]);
                objRegisteration.BranchName = Convert.ToString(dr["BranchName"]);
                objRegisteration.MemberDOB = Convert.ToString(dr["MemberDOB"]);
                objRegisteration.CustomerEmail = Convert.ToString(dr["CustomerEmail"]);
                objRegisteration.TestConducted = Convert.ToString(dr["FOLLOW_DESC"]);
                objRegisteration.Address2 = Convert.ToString(dr["Address2"]);
                objRegisteration.District = Convert.ToString(dr["District"]);
                objRegisteration.MasterPolicyNo = Convert.ToString(dr["MasterPolicyNo"]);
                objRegisteration.ProposerInitial = Convert.ToString(dr["ProposerInitial"]);
                objRegisteration.Taluka = Convert.ToString(dr["Taluka"]);
                objRegisteration.LandMark = Convert.ToString(dr["LandMark"]);
                objRegisteration.Telephone = Convert.ToString(dr["Telephone"]);
                objRegisteration.Telephone = Convert.ToString(dr["PHONE1"]);
                objRegisteration.AgentCode = Convert.ToString(dr["AgentCode"]);
                objRegisteration.AgentName = Convert.ToString(dr["AgentName"]);
                objRegisteration.AgentContactDetails = Convert.ToString(dr["AgentContactDetails"]);
                objRegisteration.Channel = Convert.ToString(dr["Channel"]);
                objRegisteration.PlanType = Convert.ToString(dr["PlanType"]);
                objRegisteration.ProductName = Convert.ToString(dr["ProductName"]);
                objRegisteration.Remark = Convert.ToString(dr["Remark"]);
                objRegisteration.PreferredDate = Convert.ToString(dr["PreferredDate"]);
                objRegisteration.PreferredTime = Convert.ToString(dr["PreferredTime"]);
                objRegisteration.PreferredProviderNo = Convert.ToString(dr["PreferredProviderNo"]);
                objRegisteration.Age = Convert.ToString(dr["Age"]);
                objRegisteration.RMContactNumber = Convert.ToString(dr["RMContactNumber"]);
                objRegisteration.RMEmailId = Convert.ToString(dr["RMEmailId"]);
                objRegisteration.IMDEmailId = Convert.ToString(dr["IMDEmailId"]);
                objRegisteration.PaymentType = Convert.ToString(dr["PaymentType"]);
                objRegisteration.Status = Convert.ToString(dr["Status"]);
                objRegisteration.AppilcantOfficeNumber = Convert.ToString(dr["AppilcantOfficeNumber"]);
                objRegisteration.DCName = Convert.ToString(dr["DCName"]);
                objRegisteration.ProposalStatus = Convert.ToString(dr["ProposalStatus"]);
                objRegisteration.CashieringDate = Convert.ToString(dr["CashieringDate"]);
                objRegisteration.CashieredAmount = Convert.ToString(dr["CashieredAmount"]);
                objRegisteration.PCName = Convert.ToString(dr["PCName"]);
                objRegisteration.Region = Convert.ToString(dr["Region"]);
                objRegisteration.RepeatCase = Convert.ToString(dr["RepeatCase"]);
                objRegisteration.TPACost = Convert.ToString(dr["TPACost"]);
                objRegisteration.PreferredDCDetails = Convert.ToString(dr["PreferredDCDetails"]);
                objRegisteration.RegisterKey = Convert.ToInt32(dr["RegisterKey"]);
                Logger.Error("STAG 5 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :FillTPARegisteration End" + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                UWSaralServices.CommFun objComm = new UWSaralServices.CommFun();
                Logger.Error("STAG 5 :PageName :TpaMedicalIntegraionSync.CS // MethodeName :FillTPARegisteration Error :" + System.Environment.NewLine + Error.ToString());
                objComm.SaveErrorLogs("", "Failed", "TpaMedicalIntegraionSync", "Main.cs", "FillTPARegisteration ", "E-Error", "", "", Error.ToString());
            }
        }

        private void UpdateTPARegisterRefNo(DataTable dt)
        {
            Logger.Error("STAG 3 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :UpdateTPARegisterRefNo Start" + System.Environment.NewLine);            
            int intResponse = -1;
            UWSaralDecision.CommFun objComm = new UWSaralDecision.CommFun();
            objComm.UpdateTPARegistertaionRerNo(dt, ref intResponse);
            Logger.Error("STAG 3 :PageName :TPAMEDICALINTEGRATIONSYNC.CS // MethodeName :UpdateTPARegisterRefNo End" + System.Environment.NewLine);
        }

        //check whether application is running or not 
        private static bool AlreadyRunning()
        {
            Process[] processes = Process.GetProcesses();
            Process currentProc = Process.GetCurrentProcess();

            foreach (Process process in processes)
            {
                try
                {
                    if (process.Modules[0].FileName == System.Reflection.Assembly.GetExecutingAssembly().Location
                                && currentProc.Id != process.Id)
                        return true;
                }
                catch (Exception ex)
                {
                    UWSaralServices.CommFun objComm = new UWSaralServices.CommFun();
                    objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", ex.ToString());
                }
            }

            return false;
        }
    }
}
