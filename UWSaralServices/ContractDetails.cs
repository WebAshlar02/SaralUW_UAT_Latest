using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWSaralServices
{
    public class ContractDetails
    {
        CommFun objcomm = new CommFun();
        public void ContractPushService(string strPQuoteNo, DataSet _dsPol, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                if (_dsPol.Tables.Count > 0 && _dsPol.Tables[0].Rows.Count > 0)
                {

                }
            }
            catch (Exception Error)
            {
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "ContractDetails.cs", "ContractPushService", "E-Error", "", "", Error.ToString());
            }
        }
    }
}
