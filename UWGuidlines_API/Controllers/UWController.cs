using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
//using System.Web.Services;
//using Platform.Utilities.LoggerFramework;
//using UWSaralObjects;
using System.Web.Configuration;
using System.Web.Caching;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UWSaralDecision;
using UWGuidlines_API;
namespace UWGuidlines_API.Controllers
{
    public class UWController : ApiController
    {
        CommFun objcomm = new CommFun();
        public string Get(string strAppNo)
        {
            try
            {
                DataSet _dsstate = new DataSet();
                DataSet _dsIsClient = new DataSet();
                DataSet _dsPrevstatus = new DataSet();
                string strApplicationno = strAppNo;//"D00108627";
                objcomm.FetchBranchState(ref _dsstate, strApplicationno);
                string strbranchstate = string.Empty;
                string strcuststate = string.Empty;
                string strChannel = string.Empty;
                double Distance = 0;
                string strWarningId = string.Empty;
                int RiskScore = 0;
                int ENYScore = 0;
                string strcomma = ",";
                if (_dsstate != null && _dsstate.Tables.Count > 0)
                {
                    if (_dsstate.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(_dsstate.Tables[0].Rows[0]["STATE"])))
                    {
                        strbranchstate = Convert.ToString(_dsstate.Tables[0].Rows[0]["STATE"]).ToUpper().TrimEnd();
                    }
                    if (_dsstate.Tables.Count > 1 && _dsstate.Tables[1].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(_dsstate.Tables[1].Rows[0]["CUSTSTATE"])))
                        {
                            strcuststate = Convert.ToString(_dsstate.Tables[1].Rows[0]["CUSTSTATE"]).ToUpper().TrimEnd();
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(_dsstate.Tables[1].Rows[0]["CHANNEL"])))
                        {
                            strChannel = Convert.ToString(_dsstate.Tables[1].Rows[0]["CHANNEL"]).ToUpper().TrimEnd();
                        }
                    }
                    if (_dsstate.Tables.Count > 3 && _dsstate.Tables[3].Rows.Count > 0 && !string.IsNullOrEmpty(Convert.ToString(_dsstate.Tables[3].Rows[0]["DISTANCE"])))
                    {
                        Distance = Convert.ToDouble(_dsstate.Tables[3].Rows[0]["DISTANCE"]);
                    }
                    if (_dsstate.Tables.Count > 4 && _dsstate.Tables[4].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(_dsstate.Tables[4].Rows[0]["Risk_Score"])))
                        {
                            RiskScore = Convert.ToInt32(_dsstate.Tables[4].Rows[0]["Risk_Score"]);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(_dsstate.Tables[4].Rows[0]["ENYVALUE"])))
                        {
                            ENYScore = Convert.ToInt32(_dsstate.Tables[4].Rows[0]["ENYVALUE"]);
                        }
                    }
                }

                //string[] strBranchName = { "New Delhi Pitampura", "EZone Rohini New Delhi", "Gwalior", "Jaipur", "Bangalore", "Guwahati", "Nashik", "Hyderabad", "Gorakhpur", "Bhubaneshwar", "Baroda", "Ahmedabad", "Bhopal", "Aligarh" };
                //for (int i = 0; i < strBranchName.Length; i++)
                //{
                //_dsstate.Tables[2].Rows.Count == 0 this condition is for high risk city is exist
                if (_dsstate != null && _dsstate.Tables.Count > 2)
                {
                    if (_dsstate.Tables[2].Rows.Count > 0 && (Distance > 50 || !strbranchstate.Equals(strcuststate)) && ((strChannel.Equals("Direct Sales") || strChannel.Equals("Tied Agency-Core") || strChannel.Equals("Tied Agency-IM"))))
                    {
                        strWarningId = "31";
                        //FillWarningMessage("31");
                        //DisplaySaralWarningMessage();
                    }
                    if (_dsstate.Tables[2].Rows.Count == 0 && (Distance > 80 || !strbranchstate.Equals(strcuststate)) && (strChannel.Equals("Tied Agency-Core") || strChannel.Equals("Tied Agency-IM")))
                    {
                        strWarningId = "23";
                        //FillWarningMessage("23");
                        //DisplaySaralWarningMessage();
                    }
                    if (_dsstate.Tables[2].Rows.Count == 0 && (Distance > 80 || !strbranchstate.Equals(strcuststate)) && (strChannel.Equals("Direct Sales")))
                    {
                        strWarningId = "24";
                        //FillWarningMessage("24");
                        //DisplaySaralWarningMessage();
                    }
                    if (!string.IsNullOrEmpty(strWarningId))
                    {
                        strWarningId = strWarningId + strcomma;
                    }
                }

                if (((Distance > 80) || !strbranchstate.Equals(strcuststate)) && !strChannel.Equals("Direct Sales") && !strChannel.Equals("Tied Agency-Core") && !strChannel.Equals("Tied Agency-IM") && !strChannel.Equals("Bankassurance-AU"))
                {
                    strWarningId += "25" + strcomma;
                    //FillWarningMessage("25");
                    //DisplaySaralWarningMessage();
                }
                if (RiskScore == 75 || RiskScore == 150 || (ENYScore >= 119 && ENYScore <= 158))
                {
                    strWarningId += "27" + strcomma;
                    //FillWarningMessage("27");
                    //DisplaySaralWarningMessage();

                }
                //Prev policy greter than 2 yrs
                objcomm.IsClientExist(ref _dsIsClient, strApplicationno);
                if (_dsIsClient != null && _dsIsClient.Tables.Count > 0 && _dsIsClient.Tables[0].Rows.Count > 0)
                {
                    for (int f = 0; f < _dsIsClient.Tables[0].Rows.Count; f++)
                    {
                        objcomm.PreviousCaseStatus(ref _dsPrevstatus, Convert.ToString(_dsIsClient.Tables[0].Rows[f]["CLT_applicationNo"]));
                        if (_dsPrevstatus.Tables.Count > 0 && _dsPrevstatus.Tables[0].Rows.Count > 0 && (_dsPrevstatus.Tables[0].Rows[0]["POL_riskCommencementStrDate"] != null || Convert.ToString(_dsPrevstatus.Tables[0].Rows[0]["POL_riskCommencementStrDate"]) != ""))
                        {
                            DateTime YR = Convert.ToDateTime(_dsPrevstatus.Tables[0].Rows[0]["POL_riskCommencementStrDate"]);
                            double year = (DateTime.Now - YR).Days / 365;
                            if ((year > 2))
                            {
                                strWarningId += "41" + strcomma;
                                //FillWarningMessage("41");
                                //DisplaySaralWarningMessage();
                                break;
                            }
                        }
                    }
                }
                //SICL is true
                if (_dsIsClient.Tables.Count > 1 && _dsIsClient.Tables[1].Rows.Count > 0)
                {
                    strWarningId += "50";
                    //FillWarningMessage("50");
                    //DisplaySaralWarningMessage();
                }
                if (_dsstate.Tables[4] != null && _dsstate.Tables[4].Rows.Count == 0)
                {
                    AddFolloupCode(_dsstate, strApplicationno);
                }
                return strWarningId;
            }
            catch(Exception Error)
            {
                return null ;
            }
        }
        public void AddFolloupCode(DataSet _ds,string strAppNo)
        {
            try
            {
                if (_ds.Tables.Count > 4 && ((_ds.Tables[5] != null && _ds.Tables[5].Rows.Count > 0) || (_ds.Tables[6] != null && _ds.Tables[6].Rows.Count > 0)))
                {
                    int resp = 0;
                    objcomm.AddWarningMsgFollowup(strAppNo, ref resp);

                }
            }
            catch (Exception Error)
            {

            }
        }
    }
}
