using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;
using System.IO;
using System.Data;
using UWSaralServices;

namespace UWSaralServices
{
   public class BillingService
    {
        
        public static string BillingBoService(string ApplicationNo, DataSet _ds,ChangeValue objchangevalue,ref int strLAPushErrorcode, ref string LAPushErrorMsg)
        {
            CommFun objComm = new CommFun();
            DataSet _dsmandate = new DataSet();
            string ManNo = string.Empty;
            objComm.GetMandateNo(ApplicationNo, ref _dsmandate);

            string value = string.Empty;
            string strbranch = string.Empty;
            string strpolicyno = string.Empty;
            string strpartnerID = string.Empty;
            string strpreferredDate = string.Empty;
            string strsource = string.Empty;
            string strUserID = string.Empty;
            string strUserRole = string.Empty;

            objComm.BillingChangeRequest_GET(ApplicationNo, ref _ds);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                strbranch = _ds.Tables[0].Rows[0]["BranchCode"].ToString();
                strpolicyno = _ds.Tables[0].Rows[0]["PolicyNo"].ToString();
                strpartnerID = objchangevalue.userLoginDetails._UserID;//_ds.Tables[0].Rows[0][].ToString();
                strpreferredDate = _ds.Tables[0].Rows[0]["PreferredDt"].ToString();
                strsource = "UWSaral";//_ds.Tables[0].Rows[0][].ToString();
                strUserID = objchangevalue.userLoginDetails._UserID;//_ds.Tables[0].Rows[0][].ToString();
                strUserRole = "10";//_ds.Tables[0].Rows[0][].ToString();
            }
            //string ManNo = LF_DEmandateDetail.GetManNo("GetManNo", ApplicationNo);
            if (_dsmandate != null && _dsmandate.Tables.Count > 0 && _dsmandate.Tables[0].Rows.Count > 0)
            {
                ManNo = _dsmandate.Tables[0].Rows[0]["MND_mandateNumber"].ToString();
            }
            LABillingChangeService.BillingChangeResponse Response = new LABillingChangeService.BillingChangeResponse();
            LABillingChangeService.BillingChange Request = new LABillingChangeService.BillingChange();
            LABillingChangeService.Service1Client service1Client = new LABillingChangeService.Service1Client();

            Request.ApplicationNo = ApplicationNo;
            Request.Branch = strbranch;
            Request.ContractNo = strpolicyno;
            Request.MandateReferenceNo = ManNo;
            Request.PartnerID = strpartnerID;
            Request.PreferredDebitDate = strpreferredDate;
            Request.Source = strsource;
            Request.UserID = strUserID;
            Request.UserRole = strUserRole;

            Response = service1Client.BillingChange(Request);

            if (Response.ErrorCode == "0")
            {
                value = "Success";
                LAPushErrorMsg = value;
                strLAPushErrorcode = 0;
            }
            else
            {
                value = Response.ErrorMessage;
                LAPushErrorMsg = value;
                strLAPushErrorcode = -1;
            }
            return value;
        }
    }
}
