using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;

namespace UWSaralServices
{
    public class AgentEnquiry
    {
        LAAgentEnquiry.getAgent objRequest = new LAAgentEnquiry.getAgent();
        CommFun objcomm = new CommFun();
        LAAgentEnquiry.LFAgentServiceBPMIntfClient objInvoke = new LAAgentEnquiry.LFAgentServiceBPMIntfClient();
        LAAgentEnquiry.getAgentResponse objResponce = new LAAgentEnquiry.getAgentResponse();
        /*added by srhi on 10 aug to show agent application no*/
        LAAgentValidationService.getAgentValidationDetailsRequest objReq = new LAAgentValidationService.getAgentValidationDetailsRequest();
        LAAgentValidationService.AgentValidationTO objAgentDetails = new LAAgentValidationService.AgentValidationTO();
        /*end here*/
        public void GetAgentDetails(string strAppNo, string strAgentCode,ref string AgentType)
        {
            try
            {
                objRequest.agentCode = strAgentCode;
                objResponce = objInvoke.getAgent(objRequest);
                if (objResponce.agentDetail != null)
                {
                    AgentType = objResponce.agentDetail.designationCode;
                    Logger.Error(strAppNo + "STAG 2 :PageName :AgentEnquiry.CS // MethodeName :GetAgentDetails Error :" + System.Environment.NewLine + "Agent Enquiry Success" + System.Environment.NewLine);
                }
            }
            catch(Exception Error)
            {
                Logger.Error(strAppNo + "STAG 2 :PageName :AgentEnquiry.CS // MethodeName :GetAgentDetails Error :" + System.Environment.NewLine + Error.ToString() + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strAppNo, "Failed", "UWSaralServices", "AgentEnquiry.cs", "GetAgentDetails", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strAppNo + "*******Agent Enquiry end for " + strAppNo + "******" + System.Environment.NewLine);
            }
        }
        /*added by shr on 10 aug 17 to show agent application no*/
        public void GetAgentApplicationNumber(string strAppNo, string strAgentCode, ref string strAgentApplicationNo)
        {

            try
            {
                LAAgentValidationService.AgentValidationServicePortTypeClient obj = new LAAgentValidationService.AgentValidationServicePortTypeClient();
                objAgentDetails = obj.getAgentValidationDetails(strAgentCode);
                if (objAgentDetails != null)
                {
                    strAgentApplicationNo = objAgentDetails.applicationNum;
                }
            }
            catch (Exception Error)
            {
                Logger.Error(strAppNo + "STAG 2 :PageName :AgentEnquiry.CS // MethodeName :GetAgentApplicationNumber Error :" + System.Environment.NewLine + Error.ToString() + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strAppNo, "Failed", "UWSaralServices", "AgentEnquiry.cs", "GetAgentApplicationNumber", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strAppNo + "*******Agent Enquiry end for " + strAppNo + "******" + System.Environment.NewLine);
            }
        }
        /*added by shr on 29 DEC 17 to SHOW AGENT DETAILS*/
        public LAAgentEnquiry.getAgentResponse GetAgentDetails(string strAppNo, string strAgentCode)
        {
            try
            {
                objRequest.agentCode = strAgentCode;
                objResponce = objInvoke.getAgent(objRequest);                
            }
            catch (Exception Error)
            {
                objResponce = null;
                Logger.Error(strAppNo + "STAG 2 :PageName :AgentEnquiry.CS // MethodeName :GetAgentDetails Error :" + System.Environment.NewLine + Error.ToString() + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strAppNo, "Failed", "UWSaralServices", "AgentEnquiry.cs", "GetAgentDetails", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strAppNo + "*******Agent Enquiry end for " + strAppNo + "******" + System.Environment.NewLine);
            }
            return objResponce;
        }
        /*end here*/
    }
}
