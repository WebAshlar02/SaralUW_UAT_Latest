using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using TPASTATUSCODE.BussLayer;
using TPASTATUSCODE.DataLayer;

namespace TPASTATUSCODE
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : Itpastatuscode
    {
        data objdata = new data();
        Responce objResp = new Responce();
        public void genarateTransactionID(string strAppNo,ref string strtransactionId)
        {
            int passwordlength = 1;
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(passwordlength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                strtransactionId += temp;
            }
            strtransactionId = strAppNo + "_" + strtransactionId;

        }

        public Responce PushMedicalStatus(ObjectData objValue)
        {          
            int _Result = 0;
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[3];
                _sqlparam[0] = new SqlParameter("@TpaRefNo", objValue.strTpaRefNo);
                _sqlparam[1] = new SqlParameter("@TpaStatusCode", objValue.strTpaStatusCode);
                _sqlparam[2] = new SqlParameter("@CreatedBy", objValue.strCreatedBy);
                _Result = objdata.dpInsertrecord("USP_UWSARAL_TPA_MEDICALSTATUS", _sqlparam);
                if (_Result > 0)
                {
                    SaveLogs(objValue.strTpaRefNo, "I-Info", "Success", objValue.strCreatedBy);
                    objResp.strErrorCode = "0";
                    objResp.strErrorStatus = "Success";
                    return objResp;
                }
                else
                {
                    SaveLogs(objValue.strTpaRefNo,"I-Info", "Failed", objValue.strCreatedBy);
                    objResp.strErrorCode = "1";
                    objResp.strErrorStatus = "Failed";
                    return objResp;
                }
            }
            catch (Exception Error)
            {
                SaveLogs(objValue.strTpaRefNo, "I-Error", Error.StackTrace.ToString(), objValue.strCreatedBy);
                objResp.strErrorCode = "1";
                objResp.strErrorStatus = "Failed";
                return objResp;
            }

        }


        public Responce TPAIntegrationDetails_Insert(Integration objCoreInt)
        {            
            int _Result = 0;
            string strFgirefNo = string.Empty;
            genarateTransactionID(objCoreInt.strAppno, ref strFgirefNo);
            try
            {
                SqlParameter[] _sqlparam = new SqlParameter[8];
                _sqlparam[0] = new SqlParameter("@AppNo", objCoreInt.strAppno);
                _sqlparam[1] = new SqlParameter("@FgiReFNo", strFgirefNo);
                _sqlparam[2] = new SqlParameter("@FgiSubRefNo", objCoreInt.strFgiSubRefNo);
                _sqlparam[3] = new SqlParameter("@TpaRefNo", objCoreInt.strTpaRefNo);
                _sqlparam[4] = new SqlParameter("@ReqRaisedBy", objCoreInt.strReqRaisedBy);
                _sqlparam[5] = new SqlParameter("@ChannelType", objCoreInt.strChannelType);
                _sqlparam[6] = new SqlParameter("@RequestFrom", objCoreInt.strRequestFrom);
                _sqlparam[7] = new SqlParameter("@CreatedBy", objCoreInt.strCreatedBy);
                _Result = objdata.dpInsertrecord("USP_UWSARAL_TPA_INTEGRATION", _sqlparam);
                if (_Result > 0)
                {
                    SaveLogs(strFgirefNo, "I-Info", "Success", objCoreInt.strCreatedBy);
                    objResp.strErrorCode = "0";
                    objResp.strErrorStatus = "Success";
                    return objResp;
                }
                else
                {
                    SaveLogs(strFgirefNo, "I-Info", "Failed", objCoreInt.strCreatedBy);
                    objResp.strErrorCode = "1";
                    objResp.strErrorStatus = "First request Not Proceed to TPA";
                    return objResp;
                }
            }
            catch (Exception Error)
            {
                SaveLogs(strFgirefNo, "I-Error", Error.StackTrace.ToString(), objCoreInt.strCreatedBy);
                objResp.strErrorCode = "1";
                objResp.strErrorStatus = "Failed";
                return objResp;
            }
        }


        public void SaveLogs(string strTpaRefNo, string Statuscode, string ErrorDiscp, string strCreatedBy)
        {
            int _ErrorCode = 0;
            SqlParameter[] _sqlparam = new SqlParameter[5];
            _sqlparam[0] = new SqlParameter("@TpaRefNo", strTpaRefNo);
            _sqlparam[1] = new SqlParameter("@ServiceName", "TPASTATUSCODE");
            _sqlparam[2] = new SqlParameter("@StatusCode", Statuscode);
            _sqlparam[3] = new SqlParameter("@ErrorDiscp", ErrorDiscp);
            _sqlparam[4] = new SqlParameter("@CreatedBy", strCreatedBy);
            _ErrorCode = objdata.dpInsertrecord("USP_UWSARAL_TPA_SERVICELOGS", _sqlparam);

        }
    }
}
