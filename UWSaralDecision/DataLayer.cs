using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace UWSaralDecision
{
    public class DataLayer
    {
        #region variable declaration Begins

        string strCon = string.Empty;
        SqlConnection sqlCon = null;
        SqlCommand sqlCom = null;
        SqlDataAdapter sqlDA = null;
        SqlDataReader _dr;

        #endregion variable declaration End

        #region Event Begins.
        public DataSet RetrieveDataset_woParam(string spName)
        {
            DataSet _ds;
            string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];
            _ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName);
            return _ds;
        }

        public DataSet RetrieveDataset(string spName, SqlParameter[] sqlParam)
        {
            DataSet _ds;
            string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];
            _ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName, sqlParam);
            return _ds;
        }

        public int Insertrecord(string spName, SqlParameter[] sqlParam)
        {
            string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];
            int result;
            result = SqlHelper.ExecuteNonQuery(cnStr, CommandType.StoredProcedure, spName, sqlParam);
            return result;
        }
        #endregion Event Ends.
    }
}
