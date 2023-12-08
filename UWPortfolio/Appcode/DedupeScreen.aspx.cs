//*//*********************************************************************     
//*                      FUTURE GENERALI INDIA                        *    
//**********************************************************************/      
//*                  I N F O R M A T I O N                                       
//********************************************************************* 
// Sr. No.              : 1      
// Company              : Life            
// Module               : CR-28958-Dedupe        
// Program Author       : Shyam Patil             
// BRD/CR/Codesk No/Win : / / /      
// Date Of Creation     : 08-02-2020           
// Description          : 1. Dedupe new api call and data bind
//**********************************************************************//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Configuration;
using UWSaralDecision;

public partial class Appcode_DedupeScreen : System.Web.UI.Page
{
    //clsErrorLogs clserror = new clsErrorLogs();
    //ApplicationErrorLogs logs = new ApplicationErrorLogs();
    UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //clserror.ApplicationNo = Request.QueryString["ApplicationNo"].ToString();
            //clserror.MethodName = "DedupeScreen-Page_Load";
            //clserror.InnerException = Request.QueryString["JsonId"].ToString();

            string inputJson = string.Empty;
            string URL = string.Empty;
            string result1 = string.Empty;
            string strClientID = string.Empty;
            string ApplicationNo = Request.QueryString["ApplicationNo"].ToString();
            //string ClientID = Request.QueryString["ClientNum"].ToString();
            try
            {
                //string ApplicationNo = Request.QueryString["ApplicationNo"].ToString();
                string ReferenceID = Request.QueryString["JsonId"].ToString();
                hdnclienttype.Value = Request.QueryString["AT"].ToString();

                //clserror.MethodLoop = clserror.MethodLoop + " URL : " + HttpContext.Current.Request.Url.AbsoluteUri.ToString();

                ReferenceRequest referenceRequest = new ReferenceRequest();
                referenceRequest.ReferenceID = ReferenceID;

                inputJson = JsonConvert.SerializeObject(referenceRequest);// (new JavaScriptSerializer()).Serialize(referenceRequest);
                string apiUrl = ConfigurationManager.AppSettings["DedupeAPI"].ToString();
                //string apiUrl = "http://10.9.41.90:5100/DedupeScreenAPI/api/";//ConfigurationManager.AppSettings["DedupeAPI"].ToString();
                //clserror.MethodLoop = clserror.MethodLoop + " Request  :" + inputJson;
                HttpResponseMessage HttpResponse = new HttpResponseMessage();
                HttpResponse = ApiPost(apiUrl, "Getjsondata", inputJson, ApplicationNo);

                ReferenceResponse objResponseClass = new ReferenceResponse();
                if (HttpResponse.IsSuccessStatusCode)
                {
                    result1 = HttpResponse.Content.ReadAsStringAsync().Result;
                    objResponseClass = JsonConvert.DeserializeObject<ReferenceResponse>(HttpResponse.Content.ReadAsStringAsync().Result);
                    Session["strClientData"] = HttpResponse.Content.ReadAsStringAsync().Result;
                    if (objResponseClass.ResponseResult.responseCode == "0")
                    {
                        //?ApplicationNo = ICW000104A & AT = LA & JsonId = l / yidOJ / wNg =
                        // hfdClientType.Value = "LA";
                        object json2 = JsonConvert.DeserializeObject(objResponseClass.ResponseBody.json);
                        DataTable dtClientData = JsonConvert.DeserializeObject<DataTable>(objResponseClass.ResponseBody.json);
                        string strClientData = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(objResponseClass.ResponseBody.json);//JsonConvert.DeserializeObject<string>(objResponseClass.ResponseBody.json);
                        //JavaScriptSerializer objjavascriptSerializer = new JavaScriptSerializer();
                        //hdntable.Value = objjavascriptSerializer.Serialize(dtClientData);
                        //if (!string.IsNullOrEmpty(strClientData))
                        //{
                        //    //Session["strClientData"] = objResponseClass.ResponseBody.json;
                        //}
                        if (dtClientData != null && dtClientData.Rows.Count > 0)
                        {
                            Session["dtreturndata"] = dtClientData;
                            if (!string.IsNullOrEmpty(dtClientData.Rows[0]["ClientCode"].ToString()))
                            {
                                string value = json2.ToString();
                                DataSet _ds = new DataSet();
                                strClientID = dtClientData.Rows[0]["ClientCode"].ToString();
                                (new Commfun()).Get_Update_NEW_Client(ref _ds, ApplicationNo, strClientID);
                                if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                                {
                                    ScriptManager.RegisterStartupScript(base.Page, this.GetType(), ("dialogJavascript" + this.ID), "SendClientData_Update("+ value + ");", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(base.Page, this.GetType(), ("dialogJavascript" + this.ID), "SendClientData_Create(" + value + ");", true);
                                }
                            }
                        }

                        // string json = JsonConvert.DeserializeObject(dtClientData, Newtonsoft.Json.Formatting.Indented);
                        //Session["json"] = json2;
                        //clserror.MethodLoop = clserror.MethodLoop + " IsSuccessStatusCode True :" + json2;
                        // objCommFun.SaveErrorLogs(ApplicationNo, "IsSuccessStatusCode true :"+ json2, "DedupreScreen", "pageLoad", "GetdataFromService", "MSG('"+json2+"')", inputJson, URL, result1);
                        // ScriptManager.RegisterStartupScript(base.Page, this.GetType(), ("dialogJavascript" + this.ID), "SendClientData(" + json2 + ");", true);

                    }
                    else
                    {
                        //clserror.MethodLoop = clserror.MethodLoop + " IsSuccessStatusCode False :" + objResponseClass.ResponseResult.responseDescription.Trim();
                        object msg = objResponseClass.ResponseResult.responseDescription.Trim();
                        objCommFun.SaveErrorLogs(ApplicationNo, "IsSuccessStatusCode False :" + msg, "DedupreScreen", "pageLoad", "GetdataFromService", "ErrorMSG('" + msg + "')", inputJson, URL, result1);
                        ScriptManager.RegisterStartupScript(base.Page, this.GetType(), ("dialogJavascript" + this.ID), "ErrorMSG('" + msg + "');", true);

                    }

                    //HttpContext.Current.Request.Url.AbsoluteUri;

                }
                else
                {
                    //clserror.MethodLoop = clserror.MethodLoop + " IsSuccessStatusCode False";
                    objCommFun.SaveErrorLogs(ApplicationNo, "IsSuccessStatusCode False", "DedupreScreen", "pageLoad", "GetdataFromService", "ErrorMSG", inputJson, URL, result1);
                }
            }
            catch (Exception ex)
            {
                //clserror.MethodLoop = clserror.MethodLoop + " Exception : " + ex.Message.ToString();
                objCommFun.SaveErrorLogs(ApplicationNo, "IsSuccessStatusCode False", "DedupreScreen", "pageLoad", "GetdataFromService", ex.ToString(), inputJson, URL, ex.ToString());
            }
            finally
            {
                //logs.DEDEQCErrorLogs_Add(clserror);
            }
        }
    }

    private HttpResponseMessage ApiPost(string apiUrl, string Method, string inputJson, string ApplicationNo)
    {
        //clserror.ApplicationNo = ApplicationNo + " : " + apiUrl + inputJson;
        //clserror.MethodName = "ApiPost";
        //clserror.InnerException = ApplicationNo;
        try
        {
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent(inputJson, Encoding.UTF8, "application/json");
            return client.PostAsync(apiUrl + Method, inputContent).Result;
        }
        catch (AggregateException ex)
        {
            string ddd = string.Empty;
            foreach (var errInner in ex.InnerExceptions)
            {
                ddd = errInner.ToString();
            }
            //clserror.MethodLoop = clserror.MethodLoop + " Exception " + ddd;
            objCommFun.SaveErrorLogs(ApplicationNo, "", "DedupreScreen", "ApiPost", "GetdataFromService", ddd, inputJson, "", ex.ToString());
            return null;
        }
        finally
        {
            //logs.DEDEQCErrorLogs_Add(clserror);
        }
    }
}