using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;

namespace TPAMEDICALINTEGRATIONSYNC.DataLayer
{
    class data
    {
        public DataSet dpRetrieveDataset(string spName, string ErrorCode)
        {
            DataSet _ds;
            SqlParameter[] _sqlparam = new SqlParameter[1];
            _sqlparam[0] = new SqlParameter("@ErrorCode", ErrorCode);
            string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];
            _ds = SqlHelper.ExecuteDataset(cnStr, CommandType.StoredProcedure, spName, _sqlparam);
            return _ds;
        }

        public DataSet dpRetrieveDataset_woParam(string spName)
        {
            string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];
            DataSet _ds;
            _ds = SqlHelper.ExecuteDataset(cnStr, CommandType.StoredProcedure, spName);
            return _ds;
        }

        //Use this method to insert the recrd  using executenonquery
        public int dpInsertrecord(string spName, SqlParameter[] sqlParam)
        {
            string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];
            int result;
            result = SqlHelper.ExecuteNonQuery(cnStr, CommandType.StoredProcedure, spName, sqlParam);
            return result;
        }

        //Use this method to get single value
        public string dpLookupSingleItem(string cnStr, string spName, SqlParameter[] sqlParam)
        {
            string retVal = "";

            retVal = (string)SqlHelper.ExecuteScalar(cnStr, CommandType.StoredProcedure,
                                                          spName, sqlParam);


            return retVal;
        }

        public DataSet dpRetrieveDataset(string cnStr, string spName, SqlParameter[] sqlParam)
        {
            DataSet _ds;
            _ds = SqlHelper.ExecuteDataset(cnStr, CommandType.StoredProcedure, spName, sqlParam);
            return _ds;
        }

        public DataSet dpRetrieveDataset_woParam(string cnStr, string spName)
        {
            DataSet _ds;
            string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];
            _ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName);
            return _ds;
        }
    }
}
