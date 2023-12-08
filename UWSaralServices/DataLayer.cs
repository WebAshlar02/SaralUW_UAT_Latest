//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : DEQC          
// Program Author       : Sushant Devkate - MFL00905
// BRD/CR/Codesk No/Win : CR-2596-RCD Logic to be changed
// Date Of Creation     : 20/05/2022
// Description          : RCD Logic to be changed
//*********************************//********************************************************************* 


using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWSaralServices
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

        public DataSet RetrieveDataset_Lombardimatersync(string spName, SqlParameter[] sqlParam)
        {
            DataSet _ds;
            string strCon = ConfigurationSettings.AppSettings["Lombardimatersync"];
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

        //1.1 Begin of Changes CR-28153

        public DataSet FetchFundDetails(string spName, SqlParameter[] sqlParam)
        {
            DataTable _dt;
            DataSet _ds;
            string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];
            _ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, spName, sqlParam);
            _dt = _ds.Tables[0];
            return _ds;
        }
        //1.1 End of Changes CR-28153

        #region 1.1 Start:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]
        #region get RCD Dates
        public DataSet UWGetDatesforRCD(string strApplicationNo,string UserID,string Source)
        {
            DataSet ds = new DataSet();
            try
            {
                string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];            
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@ApplicationNo", strApplicationNo);
                ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, "Usp_GetRCDDatesbyappNo", _sqlparam);
            }
            catch (Exception ex)
            {
                new DataLayer().InsertRCDException(strApplicationNo, "UWGetDatesforRCD", ex.Message, UserID, Source);
            }
            return ds;
        }
        #endregion
        #region Check Back Date and FT case
        public DataSet GetIsCheckBackDateandFTcase(string ApplicationNo, string UserID, string Source)
        {
            DataSet ds = new DataSet();
            try
            {
                string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];
                SqlParameter[] _sqlparam = new SqlParameter[1];
                _sqlparam[0] = new SqlParameter("@ApplicationNo", ApplicationNo);
                ds = SqlHelper.ExecuteDataset(strCon, CommandType.StoredProcedure, "Usp_GetIsBackDateandFTCase", _sqlparam);
            }
            catch (Exception ex)
            {
                new DataLayer().InsertRCDException(ApplicationNo, "GetIsCheckBackDateandFTcase", ex.Message, UserID, Source);
            }
            return ds;
        }

        #endregion
        #region Exception lock
        public void InsertRCDException(string ApplicationNo, string MethodName, string ErrorMsg, string UserId, string Source)
        {
            SqlParameter[] sqlparams = new SqlParameter[5];
            try
            {
                string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];
                sqlparams[0] = new SqlParameter("@ApplicationNo", ApplicationNo);
                sqlparams[1] = new SqlParameter("@MethodName", MethodName);
                sqlparams[2] = new SqlParameter("@ErrorMsg", ErrorMsg);
                sqlparams[3] = new SqlParameter("@userId", UserId);
                sqlparams[4] = new SqlParameter("@Source", Source);
                SqlHelper.ExecuteNonQuery(strCon, "Usp_InsertRCDExceptionLog", sqlparams);
            }
            catch (Exception ex)
            {
                string a = ex.Message;
            }

        }

        #endregion

        #endregion 1.1 End:CR-2596-RCD Logic to be changed by Sushant Devkate [MFL00905]


        #endregion Event Ends.
    }

}
