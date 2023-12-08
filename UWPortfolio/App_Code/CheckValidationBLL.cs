using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Summary description for CheckValidationBLL
/// </summary>
public class CheckValidationBLL
{
    #region Begin Of Changes Of CR-2394 Validation coustomer mob with agent by Sushant Devkate [MFL00905]
    public ValidationDO GetClientHolderDataforValidation(string ApplicationNo, string Source, string UserId)
    {
        List<CoustomerDO> ObjlistCoustomerDO = new List<CoustomerDO>();
        ObjlistCoustomerDO = new Commfun().GetClientHolderDataforValidation(ApplicationNo, UserId, Source);
        string Message = string.Empty;
        bool ISValid = false;
        bool IsAppendMsg = false;
        List<ValidationDO> ObjlstFinalvalidationDO = new List<ValidationDO>();
        ValidationDO ObjvalidationDO = new ValidationDO();
        string ClientType = string.Empty;
        try
        {
            string[] str = { "6666666666", "7777777777", "8888888888", "9999999999" };

            if (ObjlistCoustomerDO != null)
            {
                if (ObjlistCoustomerDO.Count > 0)
                {
                    ObjlstFinalvalidationDO = new List<ValidationDO>();
                    #region Check Validation 
                    ObjlistCoustomerDO = ObjlistCoustomerDO.Distinct().ToList();
                    Regex Regex = new Regex(@"^([6-9]{1})([0-9]{9})$");
                    for (var i = 0; i < ObjlistCoustomerDO.Count; i++)
                    {
                        if (!ObjlistCoustomerDO[i].IsNRICustomer)
                        {
                            ClientType = ObjlistCoustomerDO[i].ClientType;
                            if (!Regex.IsMatch(ObjlistCoustomerDO[i].MobileNo))
                            {
                                var VD = new ValidationDO();
                                VD.IsValid = false;
                                VD.ClientType = ClientType;
                                VD.Message = ClientType + "" + " Mobile Number should be start from 6,7,8 & 9 and all the 10 digits should not be the same";
                                VD.IsAppendMsg = false;
                                if (str.Contains(ObjlistCoustomerDO[i].MobileNo))
                                {
                                    VD.Message = VD.Message; //+ " and all the 10 digits should not be the same";
                                }
                                ObjlstFinalvalidationDO.Add(VD);
                            }
                            else if (str.Contains(ObjlistCoustomerDO[i].MobileNo))
                            {
                                var VD = new ValidationDO();
                                VD.IsValid = false;
                                VD.ClientType = ClientType;
                                VD.Message = ClientType + "" + " All Mobile Number 10 digits should not be the same";
                                VD.IsAppendMsg = false;
                                ObjlstFinalvalidationDO.Add(VD);
                            }
                        }
                    }

                    #endregion
                    if (ObjlstFinalvalidationDO.Count == 0)
                    {
                        ObjlstFinalvalidationDO = new List<ValidationDO>();
                        ValidationDO ObjValidationDO = new ValidationDO();
                        bool ISAPICall = false;
                        for (var i = 0; i < ObjlistCoustomerDO.Count; i++)
                        {

                            if (ObjlistCoustomerDO[i].ClientType == "LA")
                            {
                                #region If LA is minor then we are not calling API, for this calling api from proposer
                                if (CalulateAge(ObjlistCoustomerDO[i].DOB.Trim()) > 17)
                                {
                                    ISAPICall = true;
                                }
                                else
                                {
                                    ISAPICall = false;
                                }
                                #endregion
                            }
                            else
                            {
                                ISAPICall = true;

                            }

                            if (ISAPICall)
                            {
                                ObjValidationDO = IsValidationwithWithAgentDB(ApplicationNo, ObjlistCoustomerDO[i].MobileNo.Trim(), ObjlistCoustomerDO[i].AlterNateMobno.Trim(), ObjlistCoustomerDO[i].FirstName.Trim(), ObjlistCoustomerDO[i].SurName.Trim(), ObjlistCoustomerDO[i].Email.Trim(), ObjlistCoustomerDO[i].ClientType, UserId, Source);

                                ObjlstFinalvalidationDO.Add(ObjValidationDO);
                            }

                        }

                        //Call API
                    }

                }
                else
                {
                    ObjlstFinalvalidationDO = new List<ValidationDO>();
                    var VD = new ValidationDO();
                    VD.IsValid = false;
                    VD.Message = "Agent API validation failed";
                    VD.IsAppendMsg = false;
                    ObjlstFinalvalidationDO.Add(VD);

                }
            }
            else
            {
                ObjlstFinalvalidationDO = new List<ValidationDO>();
                var VD = new ValidationDO();
                VD.IsValid = false;
                VD.Message = "Agent API validation failed";
                VD.IsAppendMsg = false;
                ObjlstFinalvalidationDO.Add(VD);
            }

            #region Final OUT of API and Validation
            if (ObjlstFinalvalidationDO.Count > 0)
            {
                ObjlstFinalvalidationDO =
                        ObjlstFinalvalidationDO.Where(p => p.IsValid == false)
                        .Select(m => new ValidationDO
                        {
                            IsValid = m.IsValid,
                            IsAppendMsg = m.IsAppendMsg,
                            Message = m.Message,
                            ClientType = m.ClientType
                        }).ToList();


                if (ObjlstFinalvalidationDO.Count > 0)
                {
                    for (var i = 0; i < ObjlstFinalvalidationDO.Count; i++)
                    {
                        Message += ObjlstFinalvalidationDO[i].Message + " ,";
                        ISValid = ObjlstFinalvalidationDO[i].IsValid;
                        IsAppendMsg = ObjlstFinalvalidationDO[i].IsAppendMsg;
                    }

                    Message = Message.Substring(0, Message.Length - 1);
                }
                else
                {
                    Message = "";
                    ISValid = true;
                    IsAppendMsg = false;
                }
                var AppendMsg = IsAppendMsg ? " Please change same for further process" : "";
                ObjvalidationDO.Message = Message + AppendMsg;
                ObjvalidationDO.IsValid = ISValid;
                ObjvalidationDO.IsAppendMsg = IsAppendMsg;
            }
            #endregion
        }
        catch (Exception ex)
        {
            var VD = new ValidationDO();
            ObjvalidationDO.IsValid = false;
            ObjvalidationDO.IsAppendMsg = false;
            ObjvalidationDO.Message = ex.Message;
            new Commfun().InsertValidationAPIException(ApplicationNo, "GetClientHolderDataforValidation", ex.Message, UserId, Source, ClientType);
        }

        return ObjvalidationDO;

    }



    public int CalulateAge(string DBO)
    {
        DateTime DOB = Convert.ToDateTime(DBO);
        int age = 0;
        try
        {
            age = DateTime.Now.AddYears(-DOB.Year).Year;
        }
        catch (Exception ex)
        {
            age = 0;
        }
        return age;
    }

    #region Check the Validation with Agent DB 
    public ValidationDO IsValidationwithWithAgentDB(string ApplicationNo, string MobileNo, string AlternateNo, string FirstName, string LastName, string EmailId, string ClientType, string UserId, string Source)
    {
        List<ValidationResponseDO> ObjLstValidationResponseDOs = new List<ValidationResponseDO>();
        ValidationDO ObjFinalvalidationDO = new ValidationDO();
        string Response = string.Empty;
        try
        {
            string apiUrl = ConfigurationManager.AppSettings["CustMobAgntChk_APIURL"].ToString();

            bool IsAPICAll = Convert.ToBoolean(ConfigurationManager.AppSettings["IsAgntChkAPIURLCall"].ToString());

            if (IsAPICAll)
            {
                string RequestJson = "{\"MobileNumber\":\"" + MobileNo + "\""
                                   + ",\"AlternateNumber\":\"" + AlternateNo + "\""
                                    + ",\"FirstName\":\"" + FirstName + "\""
                                    + ",\"LastName\":\"" + LastName + "\""
                                    + ",\"Email\":\"" + EmailId + "\"}";

                Response = CheckValidationWithAgentDB(RequestJson, apiUrl, ClientType, "POST");

                new Commfun().InsertValidationAPILogs(ApplicationNo, "IsValidationwithWithAgentDB", RequestJson, Response, UserId, Source, ClientType);

                if (!string.IsNullOrEmpty(Response))
                {
                    JArray ArrayList = JArray.Parse(Response);
                    for (int i = 0; i < ArrayList.Count; i++)
                    {
                        var VDO = new ValidationResponseDO();
                        VDO.Type = Convert.ToString(ArrayList[i]["Type"]);
                        VDO.ContactDetailsMatched = Convert.ToBoolean(ArrayList[i]["ContactDetailsMatched"]);
                        VDO.NameMatched = Convert.ToBoolean(ArrayList[i]["NameMatched"]);
                        VDO.Message = Convert.ToString(ArrayList[i]["Message"]);
                        VDO.Issuccess = true;
                        VDO.IsAppendMsg = true;
                        ObjLstValidationResponseDOs.Add(VDO);
                    }

                }
                else
                {
                    ObjLstValidationResponseDOs = new List<ValidationResponseDO>();
                    var VDO = new ValidationResponseDO();
                    VDO.Message = "Agent API validation failed";
                    VDO.ContactDetailsMatched = false;
                    VDO.NameMatched = true;
                    VDO.Issuccess = false;
                    VDO.IsAppendMsg = false;
                    ObjLstValidationResponseDOs.Add(VDO);
                }

                ObjFinalvalidationDO = GetAPIValidation(ApplicationNo, ObjLstValidationResponseDOs, UserId, ClientType, Source);
            }
            else
            {
                ObjFinalvalidationDO.Message = "";//NOT call API
                ObjFinalvalidationDO.IsValid = true;
                ObjFinalvalidationDO.ClientType = ClientType;
            }

        }
        catch (Exception ex)
        {
            ObjFinalvalidationDO.Message = Response +" "+ ex.Message;
            ObjFinalvalidationDO.IsValid = false;
            ObjFinalvalidationDO.IsAppendMsg = false;
            ObjFinalvalidationDO.ClientType = ClientType;
            new Commfun().InsertValidationAPIException(ApplicationNo, "IsValidationwithWithAgentDB", ObjFinalvalidationDO.Message, UserId, Source, ClientType);
        }
        return ObjFinalvalidationDO;
    }

    public ValidationDO GetAPIValidation(string ApplicationNo, List<ValidationResponseDO> ObjlstValidationResponseDO, string UserId, string ClientType, string Source)
    {
        string Message = string.Empty;
        bool ISValid = false;
        bool IsAppendMsg = false;
        ValidationDO ObjFinalvalidationDO = new ValidationDO();
        try
        {
            if (ObjlstValidationResponseDO.Count > 0)
            {
                List<ValidationResponseDO> ObjlstValidationResponseDO1 =
                    ObjlstValidationResponseDO.Where(p => (p.ContactDetailsMatched == false && p.NameMatched == true) || (p.ContactDetailsMatched == true && p.NameMatched == false))
                    .Select(m => new ValidationResponseDO
                    {
                        Issuccess = false,
                        IsAppendMsg = m.IsAppendMsg,
                        Type = m.Type,
                        NameMatched = m.NameMatched,
                        ContactDetailsMatched = m.ContactDetailsMatched,
                        Message = m.Message,
                    }).ToList();

                if (ObjlstValidationResponseDO1.Count > 0)
                {
                    for (var i = 0; i < ObjlstValidationResponseDO1.Count; i++)
                    {
                        Message += ClientType + " " + ObjlstValidationResponseDO1[i].Message + " ,";
                        ISValid = ObjlstValidationResponseDO1[i].Issuccess;
                        IsAppendMsg = ObjlstValidationResponseDO1[i].IsAppendMsg;
                    }

                    Message = Message.Substring(0, Message.Length - 1);
                }
                else
                {
                    Message = "";//Allow to submit
                    ISValid = true;
                    IsAppendMsg = false;
                }
            }


            ObjFinalvalidationDO.Message = Message; //+ " Please change same for further process";
            ObjFinalvalidationDO.IsValid = ISValid;
            ObjFinalvalidationDO.IsAppendMsg = IsAppendMsg;
            ObjFinalvalidationDO.ClientType = ClientType;
        }
        catch (Exception ex)
        {
            ObjFinalvalidationDO.Message = ex.Message;
            ObjFinalvalidationDO.IsValid = false;
            ObjFinalvalidationDO.IsAppendMsg = false;
            new Commfun().InsertValidationAPIException(ApplicationNo, "GetAPIValidation", ex.Message, UserId, Source, ClientType);
        }

        return ObjFinalvalidationDO;
    }
    #endregion

    #region Check Validation With AgentDB- API CAll 
    public string CheckValidationWithAgentDB(string RequestJson, string apiUrl, string ClientType, string Method)
    {
        string Response = string.Empty;
        try
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUrl));
            httpRequest.ContentType = "application/json";
            httpRequest.Method = Method;

            byte[] bytes = Encoding.UTF8.GetBytes(RequestJson);
            using (Stream stream = httpRequest.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            using (HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse())
            {
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    Response = (new StreamReader(stream)).ReadToEnd();
                }
            }

        }
        catch (Exception ex)
        {

            Response = ex.Message;
        }
        return Response.ToString();
    }
    #endregion

    #endregion End Of Changes Of CR-2394 Validation coustomer mob with agent by Sushant Devkate [MFL00905]





}