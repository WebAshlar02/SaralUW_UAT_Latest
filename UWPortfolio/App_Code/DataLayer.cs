//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral   
//METHODE/EVENT:BUSSINESS LAYER
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win :  CR-30363 
// Date Of Creation     : 21/02/2022
// Description          : delete the Negative Pincode
//**********************************************************************
//********************************************************************
// Sr. No.              : 2
// Company              : Life            
// Module               : UW Saral   
//METHODE/EVENT:BUSSINESS LAYER
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win :  CR-30543 
// Date Of Creation     :  12/09/2022
// Description          : Added functinality on IsSmokar 
//***********************************************************

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
    public DataLayer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
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

    public int InsertUpdaterecord(string spName, SqlParameter[] sqlParam)
    {
        string cnStr = ConfigurationSettings.AppSettings["FGLICRM"];
        int result;
        result = SqlHelper.ExecuteNonQuery(cnStr, CommandType.StoredProcedure, spName, sqlParam);
        return result;
    }
	//public DataSet RetrieveDataset_OnlineSales(string spName, SqlParameter[] sqlParam)
	//{
	//    DataSet _ds;
	//    string strCon = ConfigurationSettings.AppSettings["OnlineSalesSTG"];
	//    _ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName, sqlParam);
	//    return _ds;
	//}
	#endregion Event Ends.

	//########################## Added by Shyam Starts ###########################################
	public DataSet RetrieveDataset_STPDetails(string spName, SqlParameter[] sqlParam)
	{
		DataSet _ds;
		string strCon = ConfigurationSettings.AppSettings["SqlPOSTDEVConnectionString"];
		_ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName, sqlParam);
		return _ds;
	}
	public DataSet RetrieveDataset_PROCODE(string spName, SqlParameter[] sqlParam)
	{
		DataSet _ds;
		string strCon = ConfigurationSettings.AppSettings["Lombardimatersync"];
		_ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName, sqlParam);
		return _ds;
	}
    public DataSet revivaldate(string spName, SqlParameter[] sqlParam)
    {
        //DataTable dtdate;
        DataSet dsdate;
        string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];
        
        dsdate = SqlHelper.ExecuteDataset(cnStr, CommandType.StoredProcedure, spName, sqlParam);
        //dtdate = dsdate.Tables[0];
        return dsdate;
    }
    //########################## Added by Shyam End ###########################################

    //########################## 1.1 BEGIN OF CHANGES CR 28153 ###########################################
    public DataSet GetClientDetails(string spName, SqlParameter[] sqlParam)
    {
        //DataTable dtdate;
        DataSet dsClientdata;
        string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];

        dsClientdata = SqlHelper.ExecuteDataset(cnStr, CommandType.StoredProcedure, spName, sqlParam);
        //dtdate = dsdate.Tables[0];
        return dsClientdata;
    }
    public DataSet GetClientdata(string spName, SqlParameter[] sqlParam)
    {
        //DataTable dtdate;
        DataSet dsClientdata;
        string cnStr = ConfigurationSettings.AppSettings["SqlPOSTDEVConnectionString"];

        dsClientdata = SqlHelper.ExecuteDataset(cnStr, CommandType.StoredProcedure, spName, sqlParam);
        //dtdate = dsdate.Tables[0];
        return dsClientdata;
    }
    //########################## 1.1 END OF CHANGES CR 28153 ###########################################

    #region  31.1 Start: CR-30363-Add and Delete Negative Pincode functionality changes by Sushant Devkate [MFL00905]
    public int DeleteRecord(string spName, SqlParameter[] sqlParam)
    {
        string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];
        int result;
        result = SqlHelper.ExecuteNonQuery(cnStr, CommandType.StoredProcedure, spName, sqlParam);
        return result;
    }
    #endregion 31.1 End: CR-30363-Add and Delete Negative Pincode functionality changes by Sushant Devkate [MFL00905]


    #region 2.1 Begin Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]
    public bool IsCheckData(string spName, SqlParameter[] sqlParam)
    {
        string cnStr = ConfigurationSettings.AppSettings["transactiondbLF"];
        bool IsSucess = false;

        IsSucess = Convert.ToBoolean(SqlHelper.ExecuteScalar(cnStr, CommandType.StoredProcedure, spName, sqlParam));

        return IsSucess;
    }

    #endregion 2.1 End Of Changes Of CR-30543 changes by Sushant Devkate [MFL00905]
}