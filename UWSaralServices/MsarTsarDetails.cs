/*  ************************************************************************************************************************************
COMMENT ID: 1
COMMENTOR NAME :Amit Chaudhary
METHODE/EVENT:
REMARK: masar tasr keyyword is change and same is fetch from database after getting confirmation from saumya .(at the time of HNH live)
DateTime :22JAN17
*********************************************************************************************************************************

*************************************************************************************************************************************
COMMENT ID: 2
COMMENTOR NAME :Bhaumik Patel
METHODE/EVENT:
REMARK: CR-5523 Death Benefit details required in Saral
DateTime: 28 March 2023

* ********************************************************************************************************************************
*/

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
        string strClientrole = "";
        string strtotalPrem = "";
        string strPayerClientid = "";
        string strProposerClientid = "";
        string strRequestdetails = "";
        string strLAClientid = "";
        string strTsar = "";
        string struserRole = "10";
        string strBranch = "";
        string strProdcode = "";
        string strUserID = "";
        string strTsarKeyword = "PostDEQC";
        string strMsarKeyword = "PostDEQC";
        string strTsarKeywordHealth = "CANCER";
        string strMsarKeywordHealth = "CANCER";
        string strpartnerId = "UWSaral";
        string strprocessID = "UWSaral";
        string strPartnerRequest = string.Empty;
        DataSet dsMsarKeyValue;
        decimal strPreviouspolicySumAssured = 0;
        string strApplicationNo = string.Empty;
        public void MsarTsarPushService(string strPQuoteNo, ref DataSet _dsMSARResult, ref DataSet _dsPrevpolicy, DataSet _dsMsarTsar, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "*******MsarTsarFsar creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine);
                LAMsarService.ServiceClient objTsar = new LAMsarService.ServiceClient();
                UWSaralServices.LAMsarService.test objResponce = new LAMsarService.test();
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
                sampleDataTable.Columns.Add("TOTALPREMIUM", typeof(string));
                 //2.1 Begin of Changes; Bhaumik Patel - [CR - 5523]
                 sampleDataTable.Columns.Add("DEATHBENEFIT", typeof(string));
                 //2.1 End of Changes; Bhaumik Patel - [CR - 5523]
                 DataRow sampleDataRow;
                string strType = "";
                int FSAR = 0;
                if (_dsMsarTsar.Tables.Count > 0 && _dsMsarTsar.Tables[0].Rows.Count > 0)
                {
                    //for (int i = 0; i < _dsMsarTsar.Tables[0].Rows.Count; i++)
                    //{
                    FSAR = 0;
                    strApplicationNo = strPQuoteNo;
                    // DataSet _dsPrevpolicy=new DataSet();
                    int output = 0;
                    //sampleDataRow = sampleDataTable.NewRow();



                    strProdcode = _dsMsarTsar.Tables[0].Rows[0]["PRODCODE"].ToString();


                    //for (int i = 0; i < _dsMsarTsar.Tables[0].Rows.Count; i++)
                    //{
                    //    if (_dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"].ToString() == "LA")
                    //    {
                    //        strLAClientid = _dsMsarTsar.Tables[0].Rows[i]["CLT_CLIENTID_CLNTNUM"].ToString();
                    //    }
                    //    if (_dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"].ToString() == "payer")
                    //    {
                    //        strPayerClientid = _dsMsarTsar.Tables[0].Rows[i]["CLT_CLIENTID_CLNTNUM"].ToString();
                    //        //strClientrole = _dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"].ToString();
                    //        //strtotalPrem = _dsMsarTsar.Tables[0].Rows[i]["TOTALPREMIUM"].ToString();
                    //    }
                    //    if (_dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"].ToString() == "proposer")
                    //    {
                    //        strProposerClientid = _dsMsarTsar.Tables[0].Rows[i]["CLT_CLIENTID_CLNTNUM"].ToString();
                    //    }
                    //}

                    var rowColl = _dsMsarTsar.Tables[0].AsEnumerable();
                     strLAClientid = (from r in rowColl
                                   where r.Field<string>("CLT_ASSUREDTYPE").Trim().ToUpper() == "LA"
                                      select r.Field<string>("CLT_CLIENTID_CLNTNUM")).FirstOrDefault();
                     strPayerClientid = (from r in rowColl
                                   where r.Field<string>("CLT_ASSUREDTYPE").Trim().ToUpper() == "PAYER"
                                         select r.Field<string>("CLT_CLIENTID_CLNTNUM")).FirstOrDefault();
                     strProposerClientid = (from r in rowColl
                                   where r.Field<string>("CLT_ASSUREDTYPE").Trim().ToUpper() == "PROPOSER"
                                            select r.Field<string>("CLT_CLIENTID_CLNTNUM")).FirstOrDefault();

                    if (strPayerClientid == "" || strPayerClientid == null)
                    {
                        strPayerClientid = strLAClientid;
                    }
                    if (strProposerClientid == "" || strProposerClientid == null)
                    {
                        strProposerClientid = strLAClientid;
                    }


                    //begin changes 1
                    string strCancervalue = "H01,H02,H03,H04";
                    string strServiceValue = string.Empty;
                    //if (strCancervalue.Contains(strProdcode))
                    //{
                    //    strServiceValue = "CANCER";
                    //}
                    //else
                    //{
                     strServiceValue = "UW";
                    //}
                    objcomm.MSARTSARKeyvalue_GET(ref dsMsarKeyValue, strPQuoteNo, strProdcode, strServiceValue);
                    if (dsMsarKeyValue != null && dsMsarKeyValue.Tables.Count > 0 && dsMsarKeyValue.Tables[0].Rows.Count > 0)
                    {
                        strTsarKeyword = dsMsarKeyValue.Tables[0].Rows[0][0].ToString();
                    }

                    //end changes   1.

                    objcomm.PreviouspolicyDetails_GET(ref _dsPrevpolicy, strPQuoteNo, strLAClientid, "LA", "101", ref output);

                    if (_dsPrevpolicy != null && _dsPrevpolicy.Tables[0].Rows.Count > 0)
                    {
                        for (int pol = 0; pol < _dsPrevpolicy.Tables[0].Rows.Count; pol++)
                        {
                            string.IsNullOrEmpty(_dsPrevpolicy.Tables[0].Rows[pol]["sumAssured"].ToString());
                            strPreviouspolicySumAssured = strPreviouspolicySumAssured + Convert.ToDecimal(_dsPrevpolicy.Tables[0].Rows[pol]["sumAssured"]);
                        }
                    }
                    try
                    {
                        /*added by suraj on 27 DEC 19 to add log of request and response */
                        string strLaEntity = CommFun.GetXMLFromObject(strLAClientid);
                        string strProposerEntity = CommFun.GetXMLFromObject(strProposerClientid);
                        string strPayerEntity = CommFun.GetXMLFromObject(strPayerClientid);
                        string strKeywordEntity = CommFun.GetXMLFromObject(strTsarKeyword);
                        string strRequestEntity = CommFun.GetXMLFromObject(strRequestdetails);
                        string strPrevpolicyEntity = CommFun.GetXMLFromObject(strPreviouspolicySumAssured);
                        string strSourceEntity = CommFun.GetXMLFromObject("UWSARAL");
                        string strErrorFromService = string.Empty;
                        strPartnerRequest = strLaEntity + strProposerEntity + strPayerEntity + strKeywordEntity + strRequestEntity + strPrevpolicyEntity + strSourceEntity;
                        //end
                        Logger.Info(strPQuoteNo + "*******TSAR creation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        if (_dsMsarTsar.Tables[0].Rows[0]["PROD_PRODUCTTYPE"].ToString() == "HL")
                        {
                            objResponce = objTsar.CalculateTSAR_FSAR_MSAR(strLAClientid, strProposerClientid, strPayerClientid, strTsarKeyword, strRequestdetails, strPreviouspolicySumAssured, "UWSARAL");
                        }
                        else
                        {
                            objResponce = objTsar.CalculateTSAR_FSAR_MSAR(strLAClientid, strProposerClientid, strPayerClientid, strTsarKeyword, strRequestdetails, strPreviouspolicySumAssured, "UWSARAL");
                        }
                        /*added by suraj on 26 dec 19 to maintain error log */
                        string strPartnerResponse = CommFun.GetXMLFromObject(objResponce);
                        objcomm.MaintainLog("MSARTSAR", "MSAR", strPartnerRequest, strPartnerResponse, string.Empty, string.Empty, "UWSaral", "UWSaral", strErrorFromService, strPQuoteNo);
                    }
                    catch (Exception Error)
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "TSAR Details Failed" + Error.Message + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******TSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                    }
                    if (objResponce.ErrorCode == "0")
                    {
                        for (int i = 0; i < _dsMsarTsar.Tables[0].Rows.Count; i++)
                        {
                            if (Convert.ToString(_dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"]).Equals("Appointee") || Convert.ToString(_dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"]).Equals("Nominee"))
                            {
                                
                            }
                            else
                            {
                                sampleDataRow = sampleDataTable.NewRow();
                                sampleDataRow["ClientID"] = _dsMsarTsar.Tables[0].Rows[i]["CLT_CLIENTID_CLNTNUM"].ToString();
                                sampleDataRow["ClientName"] = _dsMsarTsar.Tables[0].Rows[i]["ClientName"].ToString();
                                sampleDataRow["ClientRole"] = _dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"].ToString();
                                sampleDataRow["MSAR"] = objResponce.MSAR;
                                sampleDataRow["TSAR"] = objResponce.TSAR;
                                sampleDataRow["FSAR"] = objResponce.FSAR;
                                if (_dsMsarTsar.Tables[0].Rows[i]["CLT_ASSUREDTYPE"].ToString() == "payer")
                                {
                                    sampleDataRow["TOTALPREMIUM"] = _dsMsarTsar.Tables[0].Rows[i]["TOTALPREMIUM"];
                                }
                                //2.1 Begin of Changes; Bhaumik Patel - [CR - 5523]
                                sampleDataRow["DEATHBENEFIT"] = _dsMsarTsar.Tables[0].Rows[i]["DEATHBENEFIT"];
                                //2.1 End of Changes; Bhaumik Patel - [CR - 5523]
                                sampleDataTable.Rows.Add(sampleDataRow);
                                Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "TSAR Details Succeed" + System.Environment.NewLine);
                                Logger.Info(strPQuoteNo + "*******TSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :MsarTsarDetails.cs // MethodeName :MsarTsarPushService" + System.Environment.NewLine + "TSAR Details Failed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******TSAR creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "MsarTsarDetails.cs", "MsarTsarPushService", "E-ServiceCallError", "", "", strClientTsar.ToString() + System.Environment.NewLine);
                    }

                    //}

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
