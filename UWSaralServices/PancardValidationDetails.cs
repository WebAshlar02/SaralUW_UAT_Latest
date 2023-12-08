using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.IO;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;

namespace UWSaralServices
{
    public class PancardValidationDetails
    {
        CommFun objcomm = new CommFun();
        string strPanResponce = string.Empty;
        public void PanCardPushService(string strPQuoteNo, ref DataSet _dsPanResult, DataSet _dsPan, string strDatavalue, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                Logger.Info(strPQuoteNo + "*******pancard validation begin for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :PancardValidationDetails.cs // MethodeName :PanCardPushService" + System.Environment.NewLine);
                if (!string.IsNullOrEmpty(strDatavalue))
                {
                    strPanResponce = "<?xml version=\"1.0\"?>";
                    strPanResponce = strPanResponce + "<PanDetails>";
                    LAPanValidationService.PanXMLWebServiceSoapClient objClient = new LAPanValidationService.PanXMLWebServiceSoapClient();
                    strPanResponce = strPanResponce + objClient.PanXml(strDatavalue);
                    strPanResponce = strPanResponce + "</PanDetails>";
                }
                else
                {
                    if (_dsPan.Tables.Count > 0 && _dsPan.Tables[0].Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(_dsPan.Tables[0].Rows[0]["PANCardNo"].ToString()))
                        {
                            LAPanValidationService.PanXMLWebServiceSoapClient objClient = new LAPanValidationService.PanXMLWebServiceSoapClient();
                            strPanResponce = "<?xml version=\"1.0\"?>";
                            strPanResponce = strPanResponce + "<PanDetails>";
                            strPanResponce = strPanResponce + objClient.PanXml(_dsPan.Tables[0].Rows[0]["PANCardNo"].ToString());
                            strPanResponce = strPanResponce + "</PanDetails>";
                        }
                    }
                }
                if (!string.IsNullOrEmpty(strPanResponce))
                {
                    Logger.Info(strPQuoteNo + "*******pancard validation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    _dsPanResult.ReadXml(XmlReader.Create(new StringReader(strPanResponce)));
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :PancardValidationDetails.cs // MethodeName :PanCardPushService" + System.Environment.NewLine + "Pancard validation Service Succeed" + System.Environment.NewLine);
                }
                else
                {
                    Logger.Info(strPQuoteNo + " STAG 2 :PageName :PancardValidationDetails.cs // MethodeName :PanCardPushService" + System.Environment.NewLine + "Pancard validation Service Failed" + System.Environment.NewLine);
                    Logger.Info(strPQuoteNo + "*******pancard validation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "PancardValidationDetails.cs", "PanCardPushService", "E-ServiceCallError", "", "", "Pancard Validation Service Failed" + System.Environment.NewLine);
                }



            }
            catch (Exception Error)
            {
                Logger.Info(strPQuoteNo + "*******pancard validation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :PancardValidationDetails.cs // MethodeName :PanCardPushService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "PancardValidationDetails.cs", "PanCardPushService", "E-ExceptionError", "", "", Error.ToString() + System.Environment.NewLine);
            }
        }
    }
}
