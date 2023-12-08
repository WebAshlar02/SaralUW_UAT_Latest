using Platform.Utilities.LoggerFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UWSaralObjects;
namespace UWSaralServices
{
    public class LAJournalDetails
    {
        CommFun objcomm = new CommFun();
        string strpartnerId
               , strprocessID
               , strApplicationNo;
        public void JournalAuth(string strPQuoteNo, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                Logger.Info(strPQuoteNo + "*******Mandate creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :JournalService.cs // MethodeName :JournalAuth" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                string DOCUMENT = string.Empty
                    , PASSWD = string.Empty, Branch = string.Empty, UserRole = string.Empty, UserID = string.Empty;

                if (objChangeObj != null && objChangeObj.Journal != null)
                {
                    DOCUMENT = objChangeObj.Journal.DocumentNumber;
                    PASSWD = objChangeObj.Journal.Password;
                    Branch = objChangeObj.userLoginDetails._userBranch;
                    UserRole = objChangeObj.userLoginDetails._UserRole;
                    UserID = objChangeObj.userLoginDetails._UserID;
                }
                LAJournalService.ServiceClient objJournal = new LAJournalService.ServiceClient();

                LAJournalService.MasterJournal objRes = objJournal.JRNAUT(DOCUMENT, PASSWD, Branch, UserRole, UserID);


                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :JournalService.cs // MethodeName :DDMAPR" + System.Environment.NewLine + "JournalService Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******JournalService creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :JournalService.cs // MethodeName :JournalAuth" + System.Environment.NewLine + "JournalService Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "JournalService.cs", "JournalServiceServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :JournalService.cs // MethodeName :JournalAuth Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "JournalService.cs", "JournalServiceService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
    }
}
