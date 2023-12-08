using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using System.Data;
using UWSaralObjects;
using System.Globalization;
using UWSaralObjects;
namespace UWSaralServices
{
    public class UnderWriter
    {
        CommFun objcomm = new CommFun();
        public void UWAREV(string strPQuoteNo, ChangeValue objChangeValue, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + " STAG 2 :PageName :UnderWriter.cs // MethodeName :UWAREV " + System.Environment.NewLine);
                #region Object Declaration Begins.
                LAUWreversalService.ServiceClient objUnderWriter = new LAUWreversalService.ServiceClient();
                LAUWreversalService.Master objRes = new LAUWreversalService.Master();
                #endregion Object Declaration End.

                #region AFI Service Enquiry Begins.
                string strPolicyNO, strReasonId, strReasonDesc, strBranch, strUserRole, strUserId, strPartnerId, strProcessId, strApplicationNo;
                strPolicyNO = objChangeValue.AfiCfiUw._PolicyNo;
                strReasonId = objChangeValue.AfiCfiUw._ReasonId;
                strReasonDesc = objChangeValue.AfiCfiUw._ReasonDesc;
                strBranch = objChangeValue.AfiCfiUw._Branch;
                strUserRole = objChangeValue.AfiCfiUw._UserRole;
                strUserId = objChangeValue.AfiCfiUw._UserId;                
                strPartnerId = objChangeValue.AfiCfiUw._PartnerId;
                strProcessId = objChangeValue.AfiCfiUw._strProcessId;
                strApplicationNo = objChangeValue.AfiCfiUw._ApplicationNo;
                objRes = objUnderWriter.UWAREV(strPolicyNO, strBranch, strUserRole, strUserId, strPartnerId, strProcessId, strApplicationNo);
                strLAPushErrorcode = Convert.ToInt32(objRes.ERRORCODE);
                strLAPushStatus = objRes.VALUES;

                if (objRes != null)
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :UnderWriter.cs // MethodeName :UWAREV" + System.Environment.NewLine + "UW service called succeed" + System.Environment.NewLine);
                }
                else
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :UnderWriter.cs // MethodeName :UWAREV" + System.Environment.NewLine + "UW Enquiry service called failed" + System.Environment.NewLine);
                    objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UnderWriter.cs", "UWAREV", "E-ServiceCallError", "", "", strLAPushStatus + System.Environment.NewLine);

                }
                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :UnderWriter.cs // MethodeName :UWAREV" + System.Environment.NewLine + "UW creation service called succeed" + System.Environment.NewLine);
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }
                #endregion AFL Service Enquiry End.
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";

                Logger.Error(strPQuoteNo + "STAG 2 :PageName :UnderWriter.cs // MethodeName :UWAREV Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "UnderWriter.cs", "UWAREV", "E-ExceptionError", "", "", Error.ToString() + System.Environment.NewLine);
            }
        }
    }
}
