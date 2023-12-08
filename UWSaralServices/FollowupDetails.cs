using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
namespace UWSaralServices
{

    public class FollowupDetails
    {
        CommFun objcomm = new CommFun();
        string CHDRSEL, ZDOCTOR, BPMBRANCH, BPMUSERROLE, BPMUSERID;

        //string[] FUPCDE = new string[25];
        //string[] FUPDT = new string[25];
        //string[] FUPRMK = new string[25];
        //string[] FUPSTS = new string[25];
        //string[] JLIFE = new string[25];
        //string[] LIFENO = new string[25];

        string[] FUPCDE = new string[50];
        string[] FUPDT = new string[50];
        string[] FUPRMK = new string[50];
        string[] FUPSTS = new string[50];
        string[] JLIFE = new string[50];
        string[] LIFENO = new string[50];
        string strpartnerId = string.Empty;
        string strprocessID = string.Empty;
        string strApplicationNo = string.Empty;
        int i = 0;
        public void FollowuPushService(string strPQuoteNo, DataSet _dsFollowup, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: REQUIRMENTS Modification" + System.Environment.NewLine);
            try
            {
                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                LAFollowupService.Master objRes = new LAFollowupService.Master();
                //FollowUp_Test.ServiceClient objFollowup = new FollowUp_Test.ServiceClient();
                //FollowUp_Test.Master objRes = new FollowUp_Test.Master();
                if (_dsFollowup.Tables.Count > 0 && _dsFollowup.Tables[0].Rows.Count > 0)
                {

                    List<string> strAppNo = _dsFollowup.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();

                    foreach (string strPolicyNo in strAppNo)
                    {
                        //for (int j = 0; j < 25; j++)
                        for (int j = 0; j < 50; j++)
                        {
                            FUPCDE[j] = string.Empty;
                            FUPDT[j] = string.Empty;
                            FUPRMK[j] = string.Empty;
                            FUPSTS[j] = string.Empty;
                            JLIFE[j] = string.Empty;
                            LIFENO[j] = string.Empty;
                        }
                        int k = 0;
                        for (i = 0; i < _dsFollowup.Tables[0].Rows.Count; i++)
                        {
                            if (strPolicyNo == _dsFollowup.Tables[0].Rows[i]["CHDRSEL"].ToString())
                            {
                                FUPCDE[k] = _dsFollowup.Tables[0].Rows[i]["FUPCDE"].ToString();
                                FUPDT[k] = _dsFollowup.Tables[0].Rows[i]["FUPDT"].ToString();
                                FUPRMK[k] = _dsFollowup.Tables[0].Rows[i]["FUPRMK"].ToString();
                                FUPSTS[k] = _dsFollowup.Tables[0].Rows[i]["FUPSTS"].ToString();
                                JLIFE[k] = _dsFollowup.Tables[0].Rows[i]["JLIFE"].ToString();
                                LIFENO[k] = _dsFollowup.Tables[0].Rows[i]["LIFENO"].ToString();
                                CHDRSEL = _dsFollowup.Tables[0].Rows[i]["CHDRSEL"].ToString();
                                k++;
                            }
                        }
                        //CHDRSEL = _dsFollowup.Tables[0].Rows[0]["CHDRSEL"].ToString();
                        ZDOCTOR = _dsFollowup.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                        BPMBRANCH = _dsFollowup.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                        BPMUSERROLE = _dsFollowup.Tables[0].Rows[0]["BPMUSERROLE"].ToString();
                        //BPMUSERID = _dsFollowup.Tables[0].Rows[0]["BMPUSERNAME"].ToString();
                        BPMUSERID = objChangeObj.userLoginDetails._UserID;

                        objRes = objFollowup.FLPCRTI(CHDRSEL, ZDOCTOR,
                      FUPCDE[0], FUPCDE[1], FUPCDE[2], FUPCDE[3], FUPCDE[4], FUPCDE[5], FUPCDE[6], FUPCDE[7], FUPCDE[8], FUPCDE[9], FUPCDE[10], FUPCDE[11], FUPCDE[12], FUPCDE[13],
                      FUPCDE[14], FUPCDE[15], FUPCDE[16], FUPCDE[17], FUPCDE[18], FUPCDE[19], FUPCDE[20], FUPCDE[21], FUPCDE[22], FUPCDE[23], FUPCDE[24],
                      FUPCDE[25], FUPCDE[26], FUPCDE[27], FUPCDE[28], FUPCDE[29], FUPCDE[30], FUPCDE[31], FUPCDE[32], FUPCDE[33], FUPCDE[34], FUPCDE[35], FUPCDE[36], FUPCDE[37],
                      FUPCDE[38], FUPCDE[39], FUPCDE[40], FUPCDE[41], FUPCDE[42], FUPCDE[43], FUPCDE[44], FUPCDE[45], FUPCDE[46], FUPCDE[47], FUPCDE[48], FUPCDE[49],

                      FUPDT[0], FUPDT[1], FUPDT[2], FUPDT[3], FUPDT[4], FUPDT[5], FUPDT[6], FUPDT[7], FUPDT[8], FUPDT[9], FUPDT[10], FUPDT[11], FUPDT[12], FUPDT[13], FUPDT[14], FUPDT[15],
                      FUPDT[16], FUPDT[17], FUPDT[18], FUPDT[19], FUPDT[20], FUPDT[21], FUPDT[22], FUPDT[23], FUPDT[24],
                      FUPDT[25], FUPDT[26], FUPDT[27], FUPDT[28], FUPDT[29], FUPDT[30], FUPDT[31], FUPDT[32], FUPDT[33], FUPDT[34], FUPDT[35], FUPDT[36], FUPDT[37],
                      FUPDT[38], FUPDT[39], FUPDT[40], FUPDT[41], FUPDT[42], FUPDT[43], FUPDT[44], FUPDT[45], FUPDT[46], FUPDT[47], FUPDT[48], FUPDT[49],

                      FUPRMK[0], FUPRMK[1], FUPRMK[2], FUPRMK[3], FUPRMK[4], FUPRMK[5], FUPRMK[6], FUPRMK[7], FUPRMK[8], FUPRMK[9], FUPRMK[10], FUPRMK[11], FUPRMK[12], FUPRMK[13],
                      FUPRMK[14], FUPRMK[15], FUPRMK[16], FUPRMK[17], FUPRMK[18], FUPRMK[19], FUPRMK[20], FUPRMK[21], FUPRMK[22], FUPRMK[23], FUPRMK[24],
                      FUPRMK[25], FUPRMK[26], FUPRMK[27], FUPRMK[28], FUPRMK[29], FUPRMK[30], FUPRMK[31], FUPRMK[32], FUPRMK[33], FUPRMK[34], FUPRMK[35], FUPRMK[36], FUPRMK[37],
                      FUPRMK[38], FUPRMK[39], FUPRMK[40], FUPRMK[41], FUPRMK[42], FUPRMK[43], FUPRMK[44], FUPRMK[45], FUPRMK[46], FUPRMK[47], FUPRMK[48], FUPRMK[49],

                      FUPSTS[0], FUPSTS[1], FUPSTS[2], FUPSTS[3], FUPSTS[4], FUPSTS[5], FUPSTS[6], FUPSTS[7], FUPSTS[8], FUPSTS[9], FUPSTS[10], FUPSTS[11], FUPSTS[12], FUPSTS[13],
                      FUPSTS[14], FUPSTS[15], FUPSTS[16], FUPSTS[17], FUPSTS[18], FUPSTS[19], FUPSTS[20], FUPSTS[21], FUPSTS[22], FUPSTS[23], FUPSTS[24],
                      FUPSTS[25], FUPSTS[26], FUPSTS[27], FUPSTS[28], FUPSTS[29], FUPSTS[30], FUPSTS[31], FUPSTS[32], FUPSTS[33], FUPSTS[34], FUPSTS[35], FUPSTS[36], FUPSTS[37],
                      FUPSTS[38], FUPSTS[39], FUPSTS[40], FUPSTS[41], FUPSTS[42], FUPSTS[43], FUPSTS[44], FUPSTS[45], FUPSTS[46], FUPSTS[47], FUPSTS[48], FUPSTS[49],

                      JLIFE[0], JLIFE[1], JLIFE[2], JLIFE[3], JLIFE[4], JLIFE[5], JLIFE[6], JLIFE[7], JLIFE[8], JLIFE[9], JLIFE[10], JLIFE[11], JLIFE[12], JLIFE[13], JLIFE[14],
                      JLIFE[15], JLIFE[16], JLIFE[17], JLIFE[18], JLIFE[19], JLIFE[20], JLIFE[21], JLIFE[22], JLIFE[23], JLIFE[24],
                      JLIFE[25], JLIFE[26], JLIFE[27], JLIFE[28], JLIFE[29], JLIFE[30], JLIFE[31], JLIFE[32], JLIFE[33], JLIFE[34], JLIFE[35], JLIFE[36], JLIFE[37],
                      JLIFE[38], JLIFE[39], JLIFE[40], JLIFE[41], JLIFE[42], JLIFE[43], JLIFE[44], JLIFE[45], JLIFE[46], JLIFE[47], JLIFE[48], JLIFE[49],

                      LIFENO[0], LIFENO[1], LIFENO[2], LIFENO[3], LIFENO[4], LIFENO[5], LIFENO[6], LIFENO[7], LIFENO[8], LIFENO[9], LIFENO[10], LIFENO[11], LIFENO[12],
                      LIFENO[13], LIFENO[14], LIFENO[15], LIFENO[16], LIFENO[17], LIFENO[18], LIFENO[19], LIFENO[20], LIFENO[21], LIFENO[22], LIFENO[23], LIFENO[24],
                      LIFENO[25], LIFENO[26], LIFENO[27], LIFENO[28], LIFENO[29], LIFENO[30], LIFENO[31], LIFENO[32], LIFENO[33], LIFENO[34], LIFENO[35], LIFENO[36], LIFENO[37],
                      LIFENO[38], LIFENO[39], LIFENO[40], LIFENO[41], LIFENO[42], LIFENO[43], LIFENO[44], LIFENO[45], LIFENO[46], LIFENO[47], LIFENO[48], LIFENO[49],

                      BPMBRANCH, BPMUSERROLE, BPMUSERID, strpartnerId, strprocessID, strApplicationNo);
                    }
                }
                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: REQUIRMENT Modification" + System.Environment.NewLine);
                    }
                    else
                    {
                        strLAPushStatus = objRes.VALUES.ToString();
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: REQUIRMENT Modification" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "FollowupDetails.cs", "FollowupDetailsService", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = Convert.ToString(objRes.VALUES);
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Error While Saving Requirments Details,Please Contact System Admin";
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "FollowupDetails.cs", "FollowupDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR:Service Call Execution ERROR: REQUIRMENT Modification" + "ERROR MESSAGE:" + Error.Message + System.Environment.NewLine);
            }
        }
    }
}
