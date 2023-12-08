
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;
using Platform.Utilities.LoggerFramework;

namespace UWSaralServices
{
    public class Ofac
    {
        CommFun objcomm = new CommFun();
        //CIP Match start
        public void VerifyOfac(string strPQuoteNo, DataSet _dsOfac, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "*******Ofac creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :OfacDetails.cs // MethodeName :VerifyOfac" + System.Environment.NewLine);
                int CIPPercentage = 0
                                , SDNPercentage = 0;

                string strSearchType = string.Empty
                    , strLastName = string.Empty
                    , strSDNListname = string.Empty
                    , SDN_name = string.Empty
                    , SDN_country = string.Empty
                    , SDN_passport = string.Empty
                    , SDN_dob = string.Empty
                    , type = string.Empty;
                if (_dsOfac != null && _dsOfac.Tables.Count > 0 && _dsOfac.Tables[0].Rows.Count > 0)
                {
                    strSearchType = _dsOfac.Tables[0].Rows[0]["SEARCH_TYPE"].ToString();
                    strSDNListname = _dsOfac.Tables[0].Rows[0]["LIST_NAME"].ToString();
                    SDN_name = _dsOfac.Tables[0].Rows[0]["SDN_NAME"].ToString();
                    SDN_country = _dsOfac.Tables[0].Rows[0]["SDN_COUNTRY"].ToString();
                    SDN_dob = _dsOfac.Tables[0].Rows[0]["SDN_DOB"].ToString();
                    type = _dsOfac.Tables[0].Rows[0]["TYPE"].ToString();
                    SDN_passport = _dsOfac.Tables[0].Rows[0]["SDN_PASSPORT"].ToString();
                    CIPPercentage = Convert.ToInt32(_dsOfac.Tables[0].Rows[0]["CIPPercentage"].ToString());
                    SDNPercentage = Convert.ToInt32(_dsOfac.Tables[0].Rows[0]["SDNPercentage"].ToString());
                    LAOfacService.Service1Client objCallSdnClient = new LAOfacService.Service1Client();
                    LAOfacService.OfacResult[] objOFAC = objCallSdnClient.OFACService(strSearchType, strLastName, SDNPercentage, CIPPercentage, type, SDN_country, SDN_dob, SDN_name, strPQuoteNo);
                    //LAOfacService.[] objSdnBean = objCallSdnClient.getCIPMatch(strSearchType, strSDNListname, SDN_name, SDN_country, SDN_passport, SDN_dob, SDNPercentage, CIPPercentage, type);   
                    if (objOFAC.Length > 0 && !string.IsNullOrEmpty(objOFAC[0].Name.ToString()))
                    {
                        strLAPushErrorcode = 0;
                        strLAPushStatus = "success";
                    }
                    else
                    {
                        strLAPushErrorcode = -1;
                        strLAPushStatus = "Error";
                    }
                }
                else
                {
                    strLAPushErrorcode = -1;
                    strLAPushStatus = "Error";
                }

                //strLAPushErrorcode = 0;
                //strLAPushStatus = "success";
            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :OfacDetails.cs // MethodeName :VerifyOfac Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "OfacDetails.cs", "OfacDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }

    }
}
