using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using UWSaralObjects;

/// <summary>
/// Summary description for BOUWDec
/// </summary>
namespace UWSaralDecision
{
    public class BOUWDec
    {
        public BOUWDec()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Declaration"
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["groupFG"].ConnectionString);
        string strDBCS = ConfigurationManager.ConnectionStrings["groupFG"].ConnectionString;
        SqlConnection sqlCon = null;
        SqlCommand sqlCmd = null;
        SqlDataReader sqlDR = null;
        SqlDataAdapter sqlDA = null;
        DataSet sqlDS = null;
        DataTable sqlDT = null;
        SqlTransaction SqlTran = null;
        //string MsgText = "";
        #endregion

        #region "Get Member Info by Mem_ID"
        public MemberInfo getMemInfo(int ID, int intUserKey)
        {
            //List<MemberInfo> listDoc = new List<MemberInfo>();
            MemberInfo memInfo = new MemberInfo();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_MEMBER_DTLS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    //sqlCmd.Parameters.AddWithValue("@FLAG", "MEM_DTLS");
                    sqlCmd.Parameters.AddWithValue("@MEMBER_KEY", ID);
                    sqlCon.Open();
                    sqlDR = sqlCmd.ExecuteReader();
                    while (sqlDR.Read())
                    {
                        memInfo.MemberId = Convert.ToInt32(sqlDR["MEMBER_ID"]);
                        memInfo.CustomerName = sqlDR["MEMBER_NAME"].ToString();
                        memInfo.SpouseName = sqlDR["SPOUSE_NAME"].ToString();
                        memInfo.CompanyName = sqlDR["COMPANY_NAME"].ToString();
                        memInfo.EffectiveDate = Convert.ToString(sqlDR["EFFECTIVE_DATE"]);
                        memInfo.DateOfBirth = Convert.ToString(sqlDR["DATE_OF_BIRTH"]);
                        memInfo.SumAssured = sqlDR["SUM_ASSURED"].ToString();
                        memInfo.PolicyCode = sqlDR["POLICY_CODE"].ToString();
                        memInfo.ApplicationNo = Convert.ToString(sqlDR["APPLICATION_NO"]);
                        memInfo.Gender = Convert.ToString(sqlDR["GENDER"]);
                        memInfo.Aging = Convert.ToInt32(sqlDR["AGING"]);
                        memInfo.UwStatus = Convert.ToInt32(sqlDR["UW_STATUS"]);
                        memInfo.CurrentStage = sqlDR["CURRENT_STAGE"].ToString();
                        memInfo.MemberNo = sqlDR["MEMBER_NO"].ToString();
                        memInfo.PostponeByMonth = Convert.ToInt32(sqlDR["UW_DECISION_POSTPONED_BY_MONTH"]);
                        memInfo.ReviewedOn = sqlDR["UW_DECISION_DATE"].ToString();
                        //memInfo.UnderWriterId = Convert.ToInt32(sqlDR["UNDERWRITER_ID"]);
                        //memInfo.UwStatusCode = sqlDR["UW_STATUS_CODE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //throw e;
                LogError("BOUWDec.getMemInfo" + " Error occured while fetching Member information"
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            //JavaScriptSerializer js = new JavaScriptSerializer();return js.Serialize(listDoc);
            return memInfo;
        }
        #endregion

        #region "Get Med, Non-med Req gridview data"
        public DataSet GetGridReqData(string ReqType, int MemKey, int intUserKey)
        {
            DataSet sqlDS = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_MEMBER_REQUIREMENTS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@REQ_TYPE", ReqType);
                    sqlCmd.Parameters.AddWithValue("@MRD_MEM_KEY", MemKey);

                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = sqlCmd;

                    sqlDA.Fill(sqlDS);
                }
            }
            catch (Exception ex)
            {
                sqlDS = null;
                LogError("BOUWDec.GetGridReqData" + " Error occured while fetching Member Requirements"
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return sqlDS;
        }
        #endregion

        #region "Get DDls data on page load"
        public DataSet GetDdlLkpData(string flag, int type, int intUserKey)
        {
            DataSet sqlDS = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_LOOKUP_DATA_FOR_DDLS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@FLAG", flag);
                    sqlCmd.Parameters.AddWithValue("@VALUE", type);

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = sqlCmd;

                    sqlDA.Fill(sqlDS);
                }
            }
            catch (Exception ex)
            {
                sqlDS = null;
                LogError("BOUWDec.GetDdlLkpData" + " Error occured while fetching LookUp data for binding Dropdowns"
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return sqlDS;
        }
        #endregion

        #region "Get DDlReqName data using Ajax"
        public List<DdlData> GetDdlReqNameData(int value, int intUserKey)
        {
            List<DdlData> listDoc = new List<DdlData>();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_LOOKUP_DATA_FOR_DDLS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@FLAG", "REQUIREMENTS");
                    sqlCmd.Parameters.AddWithValue("@VALUE", value);
                    using (sqlDR)
                    {
                        sqlCon.Open();
                        sqlDR = sqlCmd.ExecuteReader();
                        while (sqlDR.Read())
                        {
                            DdlData doc = new DdlData();
                            doc.DataText = sqlDR["LKP_DESC"].ToString();
                            doc.DataValue = sqlDR["LKP_KEY"].ToString();
                            listDoc.Add(doc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("BOUWDec.GetDdlReqNameData" + " Error occured while fetching Member Rquiremnets for binding Dropdown using AJAX."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return listDoc;
        }
        #endregion

        #region "Get Member Docs DDl data on page load"
        public DataSet GetDdlMemDocs(int MemKey, int intUserKey)
        {
            DataSet sqlDS = new DataSet();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {

                    sqlCmd = new SqlCommand("STP_COM_FETCH_MEMBER_DOCS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@MEMBER_KEY", MemKey);

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = sqlCmd;

                    sqlDA.Fill(sqlDS);
                }
            }
            catch (Exception ex)
            {
                sqlDS = null;
                LogError("BOUWDec.GetDdlMemDocs" + " Error occured while fetching Member Documents."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return sqlDS;
        }
        #endregion

        #region "Manage requirements & Comments of a Member"
        //public int manageComments(Comments objComm,List<Uw_Remarks> ListReqs, int LetType, string LetPath)
        public string manageComments(Comments objComm, DataTable dtMemRequirements, int LetType, string LetPath, int intUserKey)
        {
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCon.Open();
                    SqlTran = sqlCon.BeginTransaction();

                    /*sending all requirements once using datatable to DB*/
                    if (dtMemRequirements.Rows.Count > 0)
                    {
                        sqlCmd = new SqlCommand("STP_COM_MANAGE_MEMBER_REQUIREMENTS_BULK", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@TblRequirements", dtMemRequirements);
                        sqlCmd.Parameters.AddWithValue("@USER_KEY", intUserKey);
                        sqlCmd.Parameters.Add("@RES_FLAG", SqlDbType.VarChar, 10);
                        sqlCmd.Parameters["@RES_FLAG"].Direction = ParameterDirection.Output;
                        sqlCmd.Transaction = SqlTran;
                        sqlCmd.ExecuteNonQuery();
                        string res = "";
                        res = sqlCmd.Parameters["@RES_FLAG"].Value.ToString();
                        if (res != "SUCCESS")
                        {
                            SqlTran.Rollback();
                        }
                    }

                    /*for managing members uwdecision*/
                    sqlCmd = new SqlCommand("STP_COM_MANAGE_UW_DECISION", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@UW_COMMENT ", objComm.UWComment);
                    sqlCmd.Parameters.AddWithValue("@HOD_COMMENT", objComm.HODComment);
                    sqlCmd.Parameters.AddWithValue("@CMO_COMMENT", objComm.CMOComment);
                    sqlCmd.Parameters.AddWithValue("@SE_COMMENT", string.Empty);
                    sqlCmd.Parameters.AddWithValue("@MEM_KEY", objComm.MemKey);
                    sqlCmd.Parameters.AddWithValue("@UW_STATUS", objComm.UWStatus);
                    sqlCmd.Parameters.AddWithValue("@UW_COMMENCEMENT_DATE", objComm.UWCommencementDate);
                    sqlCmd.Parameters.AddWithValue("@USER_KEY", intUserKey);

                    if (objComm.UWStatus == 3)
                        sqlCmd.Parameters.AddWithValue("@RATE_UP_POSTPONE_ADD_PREM", objComm.RateUPByPremium);
                    else if (objComm.UWStatus == 8)
                        sqlCmd.Parameters.AddWithValue("@RATE_UP_POSTPONE_ADD_PREM", objComm.PostponeByMonth);
                    else if (objComm.UWStatus != 3 && objComm.UWStatus != 8)
                        sqlCmd.Parameters.AddWithValue("@RATE_UP_POSTPONE_ADD_PREM", 0);

                    sqlCmd.Parameters.Add("@RES_FLAG", SqlDbType.VarChar, 10);
                    sqlCmd.Parameters["@RES_FLAG"].Direction = ParameterDirection.Output;
                    sqlCmd.Transaction = SqlTran;
                    sqlCmd.ExecuteNonQuery();
                    string resl = sqlCmd.Parameters["@RES_FLAG"].Value.ToString();
                    if (resl != "SUCCESS")
                    {
                        SqlTran.Rollback();
                    }


                    if (LetType > 0)/* && LetPath != "" */
                    {
                        /*for inserting member letter*/
                        sqlCmd = new SqlCommand("STP_COM_INSERT_TBL_MEM_LETTERS", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@LET_PATH", LetPath);
                        sqlCmd.Parameters.AddWithValue("@LET_TYPE", LetType);
                        sqlCmd.Parameters.AddWithValue("@LET_MEM_KEY", objComm.MemKey);
                        sqlCmd.Parameters.AddWithValue("@LET_CREATED_BY", intUserKey);
                        sqlCmd.Parameters.Add("@RES_FLAG", SqlDbType.VarChar, 10);
                        sqlCmd.Parameters["@RES_FLAG"].Direction = ParameterDirection.Output;
                        sqlCmd.Transaction = SqlTran;
                        sqlCmd.ExecuteNonQuery();
                        string reslt = sqlCmd.Parameters["@RES_FLAG"].Value.ToString();
                        if (reslt == "SUCCESS")
                        {
                            SqlTran.Commit();
                        }
                        else if (reslt != "SUCCESS")
                        {
                            SqlTran.Rollback();
                        }
                        return reslt;
                    }
                    else
                    {
                        if (resl == "SUCCESS")
                        {
                            SqlTran.Commit();
                        }
                        return resl;
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("BOUWDec.manageComments" + "Error occured while managing UnderWriter Comments."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                    sqlCon.Close();
            }
            return "";
        }
        #endregion

        #region "Get Letters data on Submit"
        public DataTable GetLettersData(string flag, int intUserKey)
        {
            DataTable sqlDT = new DataTable();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_LOOKUP_DATA_FOR_LETTERS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@FLAG", flag);

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = sqlCmd;

                    sqlDA.Fill(sqlDT);
                }
            }
            catch (Exception ex)
            {
                sqlDT = null;
                LogError("BOUWDec.GetLettersData" + " Error occured while fetching Letter Templates Data."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return sqlDT;
        }
        #endregion

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            System.Reflection.PropertyInfo[] Props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            foreach (System.Reflection.PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        #region "Fetch All Requirements on Submit"
        public DataTable FetchAllRequirements(string flag, int intUserKey)
        {
            DataTable sqlDT = new DataTable();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_LOOKUP_DATA_FOR_DDLS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@FLAG", flag);
                    sqlCmd.Parameters.AddWithValue("@VALUE", 0);

                    SqlDataAdapter sqlDA = new SqlDataAdapter();
                    sqlDA.SelectCommand = sqlCmd;

                    sqlDA.Fill(sqlDT);
                }
            }
            catch (Exception ex)
            {
                sqlDT = null;
                LogError("BOUWDec.FetchAllRequirements" + " Error occured while fetching Member Requirements Data."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return sqlDT;
        }
        #endregion

        #region "Get UW Previous Comments using Ajax"
        public List<Comments> GetPreviousComments(int intMemkey, int intUserKey)
        {
            List<Comments> listComments = new List<Comments>();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_FETCH_UW_REMARKS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@MEM_KEY", intMemkey);
                    using (sqlDR)
                    {
                        sqlCon.Open();
                        sqlDR = sqlCmd.ExecuteReader();
                        while (sqlDR.Read())
                        {
                            Comments objComments = new Comments();
                            objComments.ReviewedBy = sqlDR["ReviewedBy"].ToString();
                            objComments.UWComment = sqlDR["UWComment"].ToString();
                            objComments.UWDecision = sqlDR["UWDecision"].ToString();
                            objComments.ReviewedOn = sqlDR["ReviewedOn"].ToString();
                            listComments.Add(objComments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("BOUWDec.GetDdlReqNameData" + " Error occured while fetching Member Rquiremnets for binding Dropdown using AJAX."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return listComments;
        }
        #endregion

        public void LogError(string strUserErrorDesc, string strSysError, string strStackTrace, string strClassName, string strMethodName, string strLayerCode, int intUserKey)
        {
            try
            {
                WriteErrorInDatabase(strUserErrorDesc, strSysError, strStackTrace, strClassName, strMethodName, strLayerCode, intUserKey);
            }
            catch (Exception ex1)
            {
                string strFolderPath = ConfigurationManager.AppSettings["ErrorLogPath"];
                string strFileName = "ErrorLogFile" + "_" + DateTime.Now.ToString("dd-MM-yy").Replace("-", "") + ".log";
                StreamWriter strWriter;
                string strNewLine = "\r\n";
                strWriter = null;

                try
                {
                    if (!Directory.Exists(strFolderPath))
                        Directory.CreateDirectory(strFolderPath);

                    strWriter = new StreamWriter(Path.Combine(strFolderPath, strFileName), true);
                    if (strNewLine != null && strUserErrorDesc != null)
                    {
                        strWriter.Write(strNewLine + strUserErrorDesc);
                        strWriter.Write(strNewLine + strNewLine + "Database Exception --> " + ex1.Message);
                    }
                }
                catch (Exception ex)
                {
                    ////HttpContext.Current.Response.Write(ex.Message);
                }
                finally
                {
                    strWriter.Close();
                }
            }
        }

        public void LogError(string strUserErrorDesc, string strSysError, string strStackTrace, string strClassName, string strMethodName, string strLayerCode)
        {
            try
            {
                string strUserKey = "-1";

                //string strClientIP = GetClientIP();
                WriteErrorInDatabase(strUserErrorDesc, strSysError, strStackTrace, strClassName, strMethodName, strLayerCode, -1);
            }
            catch (Exception ex1)
            {
                string strFolderPath = ConfigurationManager.AppSettings["ErrorLogPath"];
                string strFileName = "ErrorLogFile" + "_" + DateTime.Now.ToString("dd-MM-yy").Replace("-", "") + ".log";
                System.IO.StreamWriter strWriter;
                string strNewLine = "\r\n";
                strWriter = null;

                try
                {
                    if (!Directory.Exists(strFolderPath))
                        Directory.CreateDirectory(strFolderPath);

                    strWriter = new StreamWriter(Path.Combine(strFolderPath, strFileName), true);
                    if (strNewLine != null && strUserErrorDesc != null)
                    {
                        strWriter.Write(strNewLine + strUserErrorDesc);
                        strWriter.Write(strNewLine + strNewLine + "Database Exception --> " + ex1.Message);
                    }
                }
                catch (Exception ex)
                {
                    //HttpContext.Current.Response.Write(ex.Message);
                }
                finally
                {
                    strWriter.Close();
                }
            }
        }

        private void WriteErrorInDatabase(string strErrUserDesc, string strErrDesc, string strStackTrace, string strErrClassName, string strErrMethodName, string strLayerCode, int intUserId)
        {
            using (sqlCon = new SqlConnection(strDBCS))
            {
                sqlCmd = new SqlCommand("STP_CMN_LOG_ERROR", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@ERR_ERROR_USER_DESC", strErrUserDesc);
                sqlCmd.Parameters.AddWithValue("@ERR_ERROR_MSG", strErrDesc);
                sqlCmd.Parameters.AddWithValue("@ERR_ERROR_STACK_TRACE", strStackTrace);
                sqlCmd.Parameters.AddWithValue("@ERR_CLASS_NAME", strErrClassName);
                sqlCmd.Parameters.AddWithValue("@ERR_METHOD_NAME", strErrMethodName);
                sqlCmd.Parameters.AddWithValue("@ERR_LAYER_CODE", strLayerCode);
                sqlCmd.Parameters.AddWithValue("@ERR_USER_KEY", intUserId);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
            }
            //Database db = SqlDatabase.CreateDatabase(ConnectionString);
            //db.ExecuteStoredProcedure("STP_CMN_LOG_ERROR", strErrUserDesc, strErrDesc, strStackTrace, strErrClassName, strErrMethodName, strLayerCode, intUserId);
        }

        //public static class DataTableExtensions
        //{
        //    public static void SetColumnsOrder(this DataTable table, params String[] columnNames)
        //    {
        //        int columnIndex = 0;
        //        foreach (var columnName in columnNames)
        //        {
        //            table.Columns[columnName].SetOrdinal(columnIndex);
        //            columnIndex++;
        //        }
        //    }
        //}
    }
}