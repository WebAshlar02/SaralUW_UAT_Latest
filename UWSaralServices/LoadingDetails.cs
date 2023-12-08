using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
namespace UWSaralServices
{
    public class LoadingDetails
    {
        CommFun objcomm = new CommFun();
        string CHDRSEL, LIFE, COVERAGE, RIDER, CRTABLE, BPMBRANCH, BPMUSERROLE, BPMUSERID;
        string[] ECESTRM = new string[8];
        string[] INSPRM = new string[8];
        string[] OPCDA = new string[8];
        string[] REASIND = new string[8];
        string[] SELECT = new string[8];
        string[] ZFMORPCT = new string[8];
        string[] AGERATE = new string[8];
        string[] ZNADJPERC = new string[8];
        string[] OPPC = new string[8];
        string strpartnerId = string.Empty;
        string strprocessID = string.Empty;
        string strApplicationNo = string.Empty;
        string strProdcode = string.Empty;
        decimal strLoadedPremiumA = 0;
        decimal strTotalPremiumA = 0;
        decimal strLoadedPremiumB = 0;
        decimal strTotalPremiumB = 0;
        decimal strTotalPremiumAB = 0;
        decimal strLoadedPremiumAB = 0;
        public void LoadingPushService(string strPQuoteNo, DataSet _dsLoading, ChangeValue objChangeObj, ref DataSet _dsLoadingValResult, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
           Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution Start: LOADING " + System.Environment.NewLine);
            try
            {                
                LALoadingService.ServiceClient ObjLoading = new LALoadingService.ServiceClient();
                LALoadingService.MasterPersonelLoadingCreate ObjRes = new LALoadingService.MasterPersonelLoadingCreate();
                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //strProdcode = objChangeObj.Load_Loadingdetails._strProdcode;
                if (_dsLoading.Tables.Count > 0 && _dsLoading.Tables[0].Rows.Count > 0)
                {
                    List<string> strAppNo = _dsLoading.Tables[0].AsEnumerable().Select(c => (string)c["CRTABLE"]).Distinct().ToList();
                    foreach (string strPolicyNo in strAppNo)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            AGERATE[j] = "0";
                            ECESTRM[j] = "0";
                            INSPRM[j] = "0";
                            OPCDA[j] = string.Empty;
                            REASIND[j] = string.Empty;
                            SELECT[j] = string.Empty;
                            ZNADJPERC[j] = string.Empty;
                            OPPC[j] = "0";
                        }
                        int k = 0;
                        for (int i = 0; i < _dsLoading.Tables[0].Rows.Count; i++)
                        {
                            if (strPolicyNo == _dsLoading.Tables[0].Rows[i]["CRTABLE"].ToString())
                            {
                                AGERATE[k] = _dsLoading.Tables[0].Rows[i]["AGERATE"].ToString();
                                ECESTRM[k] = _dsLoading.Tables[0].Rows[i]["ECESTRM"].ToString();
                                INSPRM[k] = _dsLoading.Tables[0].Rows[i]["INSPRM"].ToString();
                                OPCDA[k] = _dsLoading.Tables[0].Rows[i]["OPCDA"].ToString();
                                REASIND[k] = _dsLoading.Tables[0].Rows[i]["REASIND"].ToString();
                                SELECT[k] = _dsLoading.Tables[0].Rows[i]["SELECT"].ToString();
                                ZFMORPCT[k] = _dsLoading.Tables[0].Rows[i]["ZFMORPCT"].ToString();
                                OPPC[k] = _dsLoading.Tables[0].Rows[i]["OPPC"].ToString();
                                CHDRSEL = _dsLoading.Tables[0].Rows[i]["CHDRSEL"].ToString();
                                CRTABLE = _dsLoading.Tables[0].Rows[i]["CRTABLE"].ToString();
                                LIFE = _dsLoading.Tables[0].Rows[0]["LIFE"].ToString();
                                COVERAGE = _dsLoading.Tables[0].Rows[0]["COVERAGE"].ToString();
                                RIDER = _dsLoading.Tables[0].Rows[0]["RIDER"].ToString();
                                //CRTABLE = (string.IsNullOrEmpty(strProdcode)) ? _dsLoading.Tables[0].Rows[0]["CRTABLE"].ToString() : strProdcode;
                                BPMBRANCH = _dsLoading.Tables[0].Rows[0]["BPMBRANCHCODE"].ToString();
                                BPMUSERROLE = _dsLoading.Tables[0].Rows[0]["BPMUSERGROUP"].ToString();
                                //BPMUSERID = _dsLoading.Tables[0].Rows[0]["BMPUSERNAME"].ToString();
                                BPMUSERID = objChangeObj.userLoginDetails._UserID;
                                k++;

                            }
                        }
                        if (k > 0)
                        {
                            ObjRes = ObjLoading.LODCRT(CHDRSEL, LIFE, COVERAGE, RIDER, CRTABLE, AGERATE[0], AGERATE[1], AGERATE[2], AGERATE[3], AGERATE[4], AGERATE[5], AGERATE[6], AGERATE[7],
                        ECESTRM[0], ECESTRM[1], ECESTRM[2], ECESTRM[3], ECESTRM[4], ECESTRM[5], ECESTRM[6], ECESTRM[7],
                        INSPRM[0], INSPRM[1], INSPRM[2], INSPRM[3], INSPRM[4], INSPRM[5], INSPRM[6], INSPRM[7],
                        OPCDA[0], OPCDA[1], OPCDA[2], OPCDA[3], OPCDA[4], OPCDA[5], OPCDA[6], OPCDA[7],
                        OPPC[0], OPPC[1], OPPC[2], OPPC[3], OPPC[4], OPPC[5], OPPC[6], OPPC[7],
                        REASIND[0], REASIND[1], REASIND[2], REASIND[3], REASIND[4], REASIND[5], REASIND[6], REASIND[7],
                        SELECT[0], SELECT[1], SELECT[2], SELECT[3], SELECT[4], SELECT[5], SELECT[6], SELECT[7],
                        ZFMORPCT[0], ZFMORPCT[1], ZFMORPCT[2], ZFMORPCT[3], ZFMORPCT[4], ZFMORPCT[5], ZFMORPCT[6], ZFMORPCT[7],
                        ZNADJPERC[0], ZNADJPERC[1], ZNADJPERC[2], ZNADJPERC[3], ZNADJPERC[4], ZNADJPERC[5], ZNADJPERC[6], ZNADJPERC[7], 
                        BPMBRANCH, BPMUSERROLE, BPMUSERID, strpartnerId, strprocessID, strApplicationNo);

                            //if (_dsLoading.Tables[0].Rows[i]["RiderType"].ToString() == "BS")
                            //{
                            //    CRTABLE = _dsLoading.Tables[0].Rows[i]["CRTABLE"].ToString();
                            //}
                            //else
                            //{
                            //    CRTABLE = strProdcode;
                            //}



                            if (ObjRes != null && ObjRes.ERRORCODE == "0")
                            {
                                strLoadedPremiumAB = strLoadedPremiumAB + Convert.ToDecimal(ObjRes.ZLINSTPREM.Trim());
                                strTotalPremiumAB = strTotalPremiumAB + Convert.ToDecimal(ObjRes.INSTPREM.Trim());
                                if (strPolicyNo.Substring(0, 3) == "T10" || strPolicyNo.Substring(0, 3) == "T11")
                                {
                                    strLoadedPremiumB = Convert.ToDecimal(ObjRes.ZLINSTPREM.Trim());
                                    strTotalPremiumB = Convert.ToDecimal(ObjRes.INSTPREM.Trim());
                                }
                                else
                                {
                                    strLoadedPremiumA = Convert.ToDecimal(ObjRes.ZLINSTPREM.Trim());
                                    strTotalPremiumA = Convert.ToDecimal(ObjRes.INSTPREM.Trim());
                                }
                            }
                            else
                            {
                                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "LoadingDetails.cs", "LoadingPushService", "E-ServiceCallError", "", "", ObjRes.VALUES.ToString() + System.Environment.NewLine);
                            }
                        }

                    }
                    if (ObjRes != null && ObjRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution SUCCEED: LOADING" + System.Environment.NewLine);
                        strLAPushErrorcode = Convert.ToInt16(ObjRes.ERRORCODE);
                        strLAPushStatus = "Success";
                        _dsLoadingValResult = new DataSet();
                        _dsLoadingValResult.Locale = CultureInfo.InvariantCulture;
                        DataTable sampleDataTable = _dsLoadingValResult.Tables.Add("SampleData");
                        sampleDataTable.Columns.Add("LoadedPremiumA", typeof(string));
                        sampleDataTable.Columns.Add("TotalPremiumA", typeof(string));
                        sampleDataTable.Columns.Add("LoadedPremiumB", typeof(string));
                        sampleDataTable.Columns.Add("TotalPremiumB", typeof(string));
                        sampleDataTable.Columns.Add("TotalPremiumAB", typeof(string));
                        sampleDataTable.Columns.Add("LoadedPremiumAB", typeof(string));

                        DataRow sampleDataRow;
                        if (!string.IsNullOrEmpty(ObjRes.INSTPREM))
                        {
                            sampleDataRow = sampleDataTable.NewRow();
                            sampleDataRow["LoadedPremiumA"] = strLoadedPremiumA;
                            sampleDataRow["TotalPremiumA"] = strTotalPremiumA;
                            sampleDataRow["LoadedPremiumB"] = strLoadedPremiumB;
                            sampleDataRow["TotalPremiumB"] = strTotalPremiumB;
                            sampleDataRow["TotalPremiumAB"] = strTotalPremiumAB;
                            sampleDataRow["LoadedPremiumAB"] = strLoadedPremiumAB;
                            sampleDataTable.Rows.Add(sampleDataRow);
                            
                        }
                    }
                    else
                    {
                        strLAPushErrorcode = Convert.ToInt32(ObjRes.ERRORCODE.ToString());
                        strLAPushStatus =Convert.ToString(ObjRes.VALUES);                        
                        Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//I-INFO:Service Call Execution BUSSINESS ERROR: LOADING" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "LoadingDetails.cs", "LoadingPushService", "E-ServiceCallError", "", "",Convert.ToString(ObjRes.VALUES) + System.Environment.NewLine);
                        
                    }
                }
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = 1;
                strLAPushStatus = "Error While Saving Loading Details,Please contact System Admin";
                Logger.Info(strPQuoteNo + "PAGE_NAME:UWSaralDecision/BussLayer //EVENT_NAME:OnlineApplicationLAServiceDetails_PUSH//E-ERROR:Service Call Execution ERROR: LOADING" + "ERROR MESSAGE:" + Error.Message + System.Environment.NewLine);
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "LoadingDetails.cs", "LoadingPushService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
               
            }
        }
    }
}
