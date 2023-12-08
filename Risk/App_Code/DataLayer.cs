using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
/// <summary>
/// Summary description for DataLayer
/// </summary>
public class DataLayer
{
    #region Event Begins.
    public DataSet RetrieveDataset(string spName, SqlParameter[] sqlParam)
    {
        DataSet _ds;
        string strCon = ConfigurationSettings.AppSettings["TransactiondbLFRisk"];
        _ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName, sqlParam);
        return _ds;
    }

    #endregion Event Ends.

    #region Event Begins.
    public int InsertUpdaterecord(string spName, SqlParameter[] sqlParam)
    {
        string cnStr = ConfigurationSettings.AppSettings["FGLICRM"];
        int result;
        result = SqlHelper.ExecuteNonQuery(cnStr, CommandType.StoredProcedure, spName, sqlParam);
        return result;
    }



    public int Insertrecord(string spName, SqlParameter[] sqlParam)
    {
        string cnStr = ConfigurationSettings.AppSettings["transactiondbLFRISK"];
        int result;
        result = SqlHelper.ExecuteNonQuery(cnStr, CommandType.StoredProcedure, spName, sqlParam);
        return result;
    }
    #endregion Event Ends.
}