using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UWSaralObjects;
namespace UWSaralServices
{
    public class MedicalDataEntryInvoke
    {

        public void MedicalPushService(string strPQuoteNo, DataSet _dsFollowUp, ChangeValue objChangeValue, ref string LApushErrorCode, ref int LAPushErrorMsg)
        {
            DataTable _dt = _dsFollowUp.Tables[1];
            if (_dt != null && _dt.Rows.Count > 0)
            {
                LAMedicalEntryInvoke.startMedicaldataEntryRequest objRequest = new LAMedicalEntryInvoke.startMedicaldataEntryRequest();
                LAMedicalEntryInvoke.startMedicaldataEntryRequestBody objRequestBody = new LAMedicalEntryInvoke.startMedicaldataEntryRequestBody();
                LAMedicalEntryInvoke.MedicalDataEntryInvokePortTypeClient objPortTypeClient = new LAMedicalEntryInvoke.MedicalDataEntryInvokePortTypeClient();


                objRequestBody.applicationNumber = Convert.ToString(_dt.Rows[0]["applicationNumber"]);
                objRequest.Body = objRequestBody;
                LApushErrorCode = objPortTypeClient.startMedicaldataEntry(objRequestBody.applicationNumber);

            }
        }
        public void MedicalPushService(string strPQuoteNo,ref int LApushErrorCode , ref string LApushErrorMsg)
        {
            try
            {
                LAMedicalEntryInvoke.startMedicaldataEntryRequest objRequest = new LAMedicalEntryInvoke.startMedicaldataEntryRequest();
                LAMedicalEntryInvoke.startMedicaldataEntryRequestBody objRequestBody = new LAMedicalEntryInvoke.startMedicaldataEntryRequestBody();
                LAMedicalEntryInvoke.MedicalDataEntryInvokePortTypeClient objPortTypeClient = new LAMedicalEntryInvoke.MedicalDataEntryInvokePortTypeClient();

                objRequestBody.applicationNumber = strPQuoteNo;
                objRequest.Body = objRequestBody;
                LApushErrorMsg = objPortTypeClient.startMedicaldataEntry(objRequestBody.applicationNumber);
                LApushErrorCode = 0;
            }
            catch (Exception ex)
            {

                LApushErrorCode = -1;
                LApushErrorMsg = ex.Message;
            }

        }
    }
}
