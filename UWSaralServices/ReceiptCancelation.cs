using Platform.Utilities.LoggerFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;
namespace UWSaralServices
{
    public class ReceiptCancelation
    {
        CommFun objcomm = new CommFun();
        public void ReceiptCancelation_PUSH(string strPQuoteNo,ChangeValue objChangeValue, DataSet _ds, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + " STAG 2 :PageName :ReceiptCancelation.cs // MethodeName :ReceiptCancelation " + System.Environment.NewLine);
                #region Object Declaration Begins.
                string strBankCode = string.Empty, strReceiptNo = string.Empty, strZcanRsn = string.Empty, strBranch = string.Empty, strUserRole = string.Empty, strUserId = string.Empty, strPartnerId = string.Empty, strProcessId = string.Empty, strApplnNo = string.Empty;
                #endregion Object Declaration End.

                #region AFI Service Enquiry Begins.
                LAReceiptCancelationService.ServiceClient objReceiptCancelationService = new LAReceiptCancelationService.ServiceClient();
                LAReceiptCancelationService.Master objRes = new LAReceiptCancelationService.Master();
                if (_ds != null && _ds.Tables.Count > 0)
                {
                    strBankCode = Convert.ToString(_ds.Tables[0].Rows[0]["BANK_CODE"]);
                    strReceiptNo = Convert.ToString(_ds.Tables[0].Rows[0]["RECEIPT_NUMBER"]);
                    strZcanRsn = Convert.ToString(_ds.Tables[0].Rows[0]["REASON"]);
                    strApplnNo = Convert.ToString(_ds.Tables[0].Rows[0]["APPLN_NO"]);
                    /*commented and added by shri on 10 aug 17*/
                    //strBranch = Convert.ToString(_ds.Tables[0].Rows[0]["REASON"]);
                    //strUserRole = Convert.ToString(_ds.Tables[0].Rows[0]["USER_ROLE"]);
                    //strUserId = Convert.ToString(_ds.Tables[0].Rows[0]["USER_ID"]);
                    LoginUserDetails objLoginUserDetails = objChangeValue.userLoginDetails;
                    strBranch = objLoginUserDetails._userBranch;
                    strUserRole = objLoginUserDetails._UserRole;
                    strUserId = objLoginUserDetails._UserID;
                    /*end here*/
                }
                objRes = objReceiptCancelationService.RCTCAN(strBankCode, strReceiptNo, strZcanRsn, strBranch, strUserRole, strUserId, strPartnerId, strProcessId, strApplnNo);

                if (objRes != null)
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :ReceiptCancelation.cs // MethodeName :ReceiptCancelation" + System.Environment.NewLine + "Receipt Cancelation service called succeed" + System.Environment.NewLine);
                }
                else
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :ReceiptCancelation.cs // MethodeName :ReceiptCancelation" + System.Environment.NewLine + "Receipt Cancelation service called failed" + System.Environment.NewLine);
                }
                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :ReceiptCancelation.cs // MethodeName :ReceiptCancelation" + System.Environment.NewLine + "Receipt Cancelation service called succeed" + System.Environment.NewLine);
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }
                #endregion AFL Service Enquiry End.
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";

                Logger.Error(strPQuoteNo + "STAG 2 :PageName :ReceiptCancelation.cs // MethodeName :ReceiptCancelation Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "Default.cs", "ReceiptCancelation", "E-Error", "", "", Error.ToString() + System.Environment.NewLine);
            }
        }
    }
}
