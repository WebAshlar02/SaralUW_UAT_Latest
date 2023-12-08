using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UWSaralObjects;
using System.Configuration;
using Platform.Utilities.LoggerFramework;
namespace UWSaralServices
{
    public class LAAdhar
    {
        public void Demographic(string strPQuoteNo, DataSet _dsRequest, ref DataSet _dsResponse, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            //Logger.Info(strPQuoteNo + "PAGE_NAME:LAAdhar//UWSaralServices//EVENT_NAME:Demographic//I-INFO:Service Call Execution Start: Aadhar card" + System.Environment.NewLine);
            try
            {
                //instantiate
                Aadhar.IeKYCServiceClient obj = new Aadhar.IeKYCServiceClient();
                string username = string.Empty, password = string.Empty, ApplicationNo = string.Empty, RequestSource = string.Empty, requesttype = string.Empty
                    , Uid = string.Empty, name = string.Empty, gender = string.Empty, dob = string.Empty, ms = string.Empty, mv = string.Empty, validateName = string.Empty;
                
                //check if data set is empty or not 
                if (_dsRequest!=null && _dsRequest.Tables.Count>0 && _dsRequest.Tables[0].Rows.Count>0)
                {
                    ApplicationNo = strPQuoteNo;
                    RequestSource = "SARALUW";                    
                    Uid = objChangeObj.ClientDetails.Aadhar;
                    name = Convert.ToString(_dsRequest.Tables[0].Rows[0]["NAME"]);
                    gender = Convert.ToString(_dsRequest.Tables[0].Rows[0]["SEX"]);
                    dob = Convert.ToString(_dsRequest.Tables[0].Rows[0]["DOB"]);                    
                }
                username = CommFun.AadharEncrypt(Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Username"]));
                password = CommFun.AadharEncrypt(Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Password"]));
                //call service
                _dsResponse= obj.Demographic(username, password, ApplicationNo, "SARALUW", "", Uid, name, gender, dob);                 

                //set response from service 
                if (_dsResponse != null && _dsResponse.Tables.Count > 0 && _dsResponse.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(_dsResponse.Tables[0].Rows[0]["authstatus"])=="y")
                    {
                        strLAPushErrorcode = 1;
                    }
                    else
                    {
                        strLAPushErrorcode = 0;
                    }
                }
                else
                {
                    strLAPushErrorcode = 0;
                }
            }
            catch (Exception ex)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = ex.Message;
                //Logger.Info(strPQuoteNo + "PAGE_NAME:LAAdhar//UWSaralServices//EVENT_NAME:Demographic//I-INFO:Service Call Execution Start: Aadhar card" + System.Environment.NewLine+ex.Message);
                //throw ex;
            }
        }

        
    }
}
