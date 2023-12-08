using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;
namespace UWSaralServices
{
    public class AgentDetails
    {
        CommFun objcomm = new CommFun();
        public void AgentdetailsService(string strAgentCode, ref DataSet _dsAgent,ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                //LAAgentDetailsService.AgentValidationServicePortTypeClient obj = new LAAgentDetailsService.AgentValidationServicePortTypeClient();
                //LAAgentDetailsService.AgentValidationTO objRes = new LAAgentDetailsService.AgentValidationTO();
                //objRes = obj.getAgentValidationDetails("80000006");
                //if (objRes != null)
                //{               
                //DataTable return_Datatable = new DataTable();
                //_dsAgent = new DataSet();
                //_dsAgent.Tables.Add(return_Datatable);
                //foreach (PropertyInfo info in typeof(LAAgentDetailsService.AgentValidationTO).GetProperties())
                //{
                //    _dsAgent.Tables[0].Rows.Add(info.Name);
                //}
                //_dsAgent.Tables[0].AcceptChanges();
                //}
            }
            catch (Exception Error)
            {
                objcomm.SaveErrorLogs("", "Failed", "UWSaralServices", "AgentDetails.cs", "AgentdetailsService", "E-Error", "", "", Error.ToString());
            }
        }
    }
}
