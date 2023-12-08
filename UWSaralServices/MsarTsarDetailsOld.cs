using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using System.Globalization;
using UWSaralObjects;
namespace UWSaralServices
{
    public class MsarTsarDetails
    {
        CommFun objcomm = new CommFun();
        Decimal strClientTsar;
        Decimal strClientMsar;
        string strClientid = "";
        string strTsar = "";
        string struserRole = "10";
        string strBranch = "";
        string strUserID = "";
        string strTsarKeyword = "PostDEQC";
        string strMsarKeyword = "PostDEQC";
        string strTsarKeywordHealth = "CANCER";
        string strMsarKeywordHealth = "CANCER";
        string strpartnerId = "UWSaral";
        string strprocessID = "UWSaral";
        string strApplicationNo = string.Empty;
        public void MsarTsarPushService(string strPQuoteNo, ref DataSet _dsMSARResult, DataSet _dsMsarTsar, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "*******MsarTsarFsar creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine);
                LAMsarService.ServiceClient objTsar = new LAMsarService.ServiceClient();
                LAFSARCalculationService.ServiceClient objFSAR = new LAFSARCalculationService.ServiceClient();
                LAFSARCalculationService.MasterUnderwriting[] ObjSTPResult = null;

                _dsMSARResult = new DataSet();
                _dsMSARResult.Locale = CultureInfo.InvariantCulture;
                DataTable sampleDataTable = _dsMSARResult.Tables.Add("MSARRESULT");
                sampleDataTable.Columns.Add("ClientID", typeof(string));
                sampleDataTable.Columns.Add("ClientRole", typeof(string));
                sampleDataTable.Columns.Add("ClientName", typeof(string));
                sampleDataTable.Columns.Add("MSAR", typeof(string));
                sampleDataTable.Columns.Add("TSAR", typeof(string));
                sampleDataTable.Columns.Add("FSAR", typeof(string));
                DataRow sampleDataRow;
                string strType = "";
                int FSAR = 0;
                if (_dsMsarTsar.Tables.Count > 0 && _dsMsarTsar.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < _dsMsarTsar.Tables[1].Rows.Count; i++)
                    {
                        FSAR = 0;
                        strApplicationNo = strPQuoteNo;
                        sampleDataRow = sampleDataTable.NewRow();
                        strClientid = _dsMsarTsar.Tables[1].Rows[i]["CLT_clientId_CLNTNUM"].ToString();
                        strBranch = _dsMsarTsar.Tables[0].Rows[0]["register"].ToString();
                        strTsar = "";// _dsMsarTsar.Tables[1].Rows[i]["TSAR"].ToString();
                        //strKeyword = _dsMsarTsar.Tables[1].Rows[i]["KEYWORD"].ToString();
                        strType = "";//_dsMsarTsar.Tables[1].Rows[i]["TSARTYPE"].ToString();
                        try
                        {
                            Logger.Info(strPQuoteNo + "*******TSAR creation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            if (_dsMsarTsar.Tables[0].Rows[0]["ProductType"].ToString() == "HL")
                            {
                                strClientTsar = objTsar.CalculateTSAR(strClientid, strTsar, strTsarKeywordHealth, strType);
                            }
                            else
                            {
                                strClientTsar = objTsar.CalculateTSAR(strClientid, strTsar, strTsarKeyword, strType);
                            }

                            if (strClientTsar.ToString() == "0")
                            {
                                Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "TSAR Details Succeed" + System.Environment.NewLine);
                                Logger.Info(strPQuoteNo + "*******TSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            }
                            else
                            {
                                Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "TSAR Details Failed" + System.Environment.NewLine);
                                Logger.Info(strPQuoteNo + "*******TSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ServiceCallError", "", "", strClientTsar.ToString() + System.Environment.NewLine);
                            }
                        }
                        catch (Exception Error)
                        {
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "TSAR Details Failed" + Error.Message + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + "*******TSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                        }

                        try
                        {
                            Logger.Info(strPQuoteNo + "*******MSAR creation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            if (_dsMsarTsar.Tables[0].Rows[0]["ProductType"].ToString() == "HL")
                            {
                                strClientMsar = objTsar.CalculateTSAR(strClientid, strTsar, strMsarKeywordHealth, strType);
                            }
                            else
                            {
                                strClientMsar = objTsar.CalculateTSAR(strClientid, strTsar, strMsarKeyword, strType);
                            }
                            if (strClientMsar.ToString() == "0")
                            {
                                Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "MSAR Details Succeed" + System.Environment.NewLine);
                                Logger.Info(strPQuoteNo + "*******MSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            }
                            else
                            {
                                Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "MSAR Details Failed" + System.Environment.NewLine);
                                Logger.Info(strPQuoteNo + "*******MSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ServiceCallError", "", "", strMsarKeyword.ToString() + System.Environment.NewLine);
                            }
                        }
                        catch (Exception Error)
                        {
                            Logger.Info(strPQuoteNo + "*******MSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "MSAR Details Failed" + Error.Message + System.Environment.NewLine);
                            objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                        }

                        try
                        {
                            Logger.Info(strPQuoteNo + "*******FSAR creation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            strUserID = objChangeObj.userLoginDetails._UserID;
                            ObjSTPResult = objFSAR.UWEFSA(strClientid, strBranch, struserRole, strUserID, strpartnerId, strprocessID, strApplicationNo);
                            if (ObjSTPResult[0].ERRORCODE == "0")
                            {
                                Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "FSAR Details Succeed" + System.Environment.NewLine);
                                Logger.Info(strPQuoteNo + "*******FSAR creation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            }
                            else
                            {
                                Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "FSAR Details Failed" + System.Environment.NewLine);
                                Logger.Info(strPQuoteNo + "*******FSAR creation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ServiceCallError", "", "", ObjSTPResult[0].VALUES.ToString() + System.Environment.NewLine);
                            }

                        }
                        catch (Exception Error)
                        {
                            Logger.Info(strPQuoteNo + "*******FSAR creation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "FSAR Details Failed" + Error.Message + System.Environment.NewLine);
                            objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                        }
                        if (ObjSTPResult.Length > 0)
                        {
                            for (int zi = 0; zi < ObjSTPResult.Length; zi++)
                            {
                                FSAR = FSAR + Convert.ToInt32(ObjSTPResult[zi].SUMIN);
                            }
                        }
                        sampleDataRow["ClientID"] = _dsMsarTsar.Tables[1].Rows[i]["CLT_clientId_CLNTNUM"].ToString();
                        sampleDataRow["ClientName"] = _dsMsarTsar.Tables[1].Rows[i]["ClientName"].ToString();
                        sampleDataRow["ClientRole"] = _dsMsarTsar.Tables[1].Rows[i]["relationshipToLifeAssured"].ToString();
                        sampleDataRow["MSAR"] = strClientTsar;
                        sampleDataRow["TSAR"] = strClientMsar;
                        sampleDataRow["FSAR"] = FSAR;
                        sampleDataTable.Rows.Add(sampleDataRow);
                    }

                }
            }
            catch (Exception Error)
            {
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :MsarTsarDetails.CS // MethodeName :MsarTsarPushService Error :" + System.Environment.NewLine + Error.ToString() + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******MsarTsarFsar creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
    }
}
