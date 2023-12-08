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
/// Summary description for BOUWDashBoard
/// </summary>
namespace UWSaralDecision
{
    public class BOUWDashBoard
    {
        public BOUWDashBoard()
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
        //string MsgText = "";
        #endregion

        #region "Get User Key"
        public int GetUserKey(string strUserId)
        {
            int UserKey = 0;
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_FETCH_UW_USER_KEY", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@USER_ID", strUserId);
                    sqlCon.Open();
                    sqlDR = sqlCmd.ExecuteReader();
                    while (sqlDR.Read())
                    {
                        UserKey = Convert.ToInt32(sqlDR["USR_USER_KEY"]);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("BOUWDashBoard.GetUserKey" + "Error occured while fetching Underwriter User Key."
                         , ex.Message, ex.StackTrace, "", "", "BO", UserKey);
                sqlCon.Close();
            }
            return UserKey;
        }
        #endregion

        #region "Get Datafacts Using Ajax"
        public List<DashBoardDataFacts> GetDataFacts(int intUserKey)
        {
            List<DashBoardDataFacts> listDataFacts = new List<DashBoardDataFacts>();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_UW_DASHBOARD_DATAFACTS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    //sqlCmd.Parameters.AddWithValue("@FLAG", "DATAFACTS");
                    sqlCon.Open();
                    sqlDR = sqlCmd.ExecuteReader();
                    while (sqlDR.Read())
                    {
                        var df = new DashBoardDataFacts
                        {
                            UwStatus = sqlDR["UW_STATUS"].ToString(),
                            UwStatusCode = sqlDR["UW_STATUS_CODE"].ToString(),
                            //uw.NO_OF_CASES = Convert.ToInt32(rd["NO_OF_CASES"]),
                            TotalCases = Convert.ToInt32(sqlDR["UW_COUNTER"])
                        };
                        listDataFacts.Add(df);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("BOUWDashBoard.GetDataFacts" + "Error occured while fetching Underwriter DataFacts using AJAX."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return listDataFacts;
        }
        #endregion

        #region "Get Members Info by giving UWDecision Status Using Ajax"
        public List<MemberInfo> GetGridMemsData(int ID, int intUserKey)
        {
            List<MemberInfo> MemList = new List<MemberInfo>();
            try
            {
                using (sqlCon = new SqlConnection(strDBCS))
                {
                    sqlCmd = new SqlCommand("STP_COM_FETCH_MEMBERS_DETAILS", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    //sqlCmd.Parameters.AddWithValue("@FLAG", "MEM_DATA");
                    sqlCmd.Parameters.AddWithValue("@UW_STATUS_CODE", ID);
                    sqlCon.Open();
                    sqlDR = sqlCmd.ExecuteReader();
                    while (sqlDR.Read())
                    {
                        var mem = new MemberInfo
                        {
                            //MemberNo = Convert.ToString(sqlDR["MEMBER_NO"]),
                            MemberId = Convert.ToInt32(sqlDR["MemberId"]),
                            CustomerName = sqlDR["CustomerName"].ToString(),
                            PolicyCode = Convert.ToString(sqlDR["PolicyCode"]),
                            SumAssured = sqlDR["SumAssured"].ToString(),
                            Aging = Convert.ToInt32(sqlDR["Aging"]),
                            CurrentStage = sqlDR["CurrentStage"].ToString(),
                            ApplicationNo = sqlDR["ApplicationNo"].ToString()
                            //UnderWriterId = Convert.ToInt32( sqlDR["UNDERWRITER_ID"]),
                            //UwStatusCode = sqlDR["UW_STATUS_CODE"].ToString()
                        };
                        MemList.Add(mem);
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("BOUWDashBoard.GetGridMemsData" + "Error occured while fetching Members Data using AJAX."
                         , ex.Message, ex.StackTrace, "", "", "BO", intUserKey);
            }
            return MemList;
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

    }
}