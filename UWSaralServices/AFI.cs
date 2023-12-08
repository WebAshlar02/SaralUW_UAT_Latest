using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using System.Data;
using UWSaralObjects;
using System.Globalization;

namespace UWSaralServices
{
    public class AFI
    {
        CommFun objcomm = new CommFun();

        public void ConAFI(string strPQuoteNo, ChangeValue objChangeValue,  ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + " STAG 2 :PageName :Default.cs // MethodeName :ConAFI " + System.Environment.NewLine);
                #region Object Declaration Begins.                            
                AFICFI.ServiceClient objAfiCfi = new AFICFI.ServiceClient();
                AFICFI.Master objRes = new AFICFI.Master();
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
                objRes = objAfiCfi.CONAFI(strPolicyNO, strReasonId, strReasonDesc, strBranch, strUserRole, strUserId, strPartnerId, strProcessId, strApplicationNo);
                strLAPushErrorcode = Convert.ToInt32(objRes.ERRORCODE);
                strLAPushStatus = objRes.VALUES;

                if (objRes != null)
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :Default.cs // MethodeName :ConAFI" + System.Environment.NewLine + "AFL service called succeed" + System.Environment.NewLine);
                }
                else
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :AmlDetails.cs // MethodeName :AMLPushService" + System.Environment.NewLine + "Aml Enquiry service called failed" + System.Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :AmlDetails.cs // MethodeName :AMLPushService" + System.Environment.NewLine + "Aml creation service called succeed" + System.Environment.NewLine);
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }
                #endregion AFL Service Enquiry End.
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";

                Logger.Error(strPQuoteNo + "STAG 2 :PageName :Default.cs // MethodeName :ConAFL Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "Default.cs", "ConAFL", "E-Error", "", "", Error.ToString() + System.Environment.NewLine);
            }
        }

        public void ConCFI(string strPQuoteNo, ChangeValue objChangeValue, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + " STAG 2 :PageName :Default.cs // MethodeName :ConCFI " + System.Environment.NewLine);
                #region Object Declaration Begins.
                AFICFI.ServiceClient objAfiCfi = new AFICFI.ServiceClient();
                AFICFI.Master objRes = new AFICFI.Master();
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
                objRes = objAfiCfi.CONCFI(strPolicyNO, strReasonId, strReasonDesc, strBranch, strUserRole, strUserId, strPartnerId, strProcessId, strApplicationNo);
                strLAPushErrorcode = Convert.ToInt32(objRes.ERRORCODE);
                strLAPushStatus = objRes.VALUES;

                if (objRes != null)
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :Default.cs // MethodeName :ConAFI" + System.Environment.NewLine + "AFL service called succeed" + System.Environment.NewLine);
                }
                else
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :AmlDetails.cs // MethodeName :AMLPushService" + System.Environment.NewLine + "Aml Enquiry service called failed" + System.Environment.NewLine);
                    objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "AFI.cs", "ConAFL", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :AmlDetails.cs // MethodeName :AMLPushService" + System.Environment.NewLine + "Aml creation service called succeed" + System.Environment.NewLine);
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }
                #endregion AFL Service Enquiry End.
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";

                Logger.Error(strPQuoteNo + "STAG 2 :PageName :Default.cs // MethodeName :ConAFL Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "AFI.cs", "ConAFL", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
            }
        }
    }
}
