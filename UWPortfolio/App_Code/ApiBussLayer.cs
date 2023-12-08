//*********************************************************************************************************************************/
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave              
// BRD/CR/Codesk No/Win : CR - 30499
// Date Of Creation     : 12.04.2022           
// Description          : 1.Video MER requirement-reviewed
//*******************************************************************************************************************
//1.1 Begin of Changes; Sagar Thorave - [MFL00886]
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

public class ApiBussLayer
{
    string BlobGetAPI = ConfigurationManager.AppSettings["GetBlobStorage"].ToString().Trim();
    Commfun objComm = new Commfun();
    public ApiBussLayer()
    {
       
    }

    public string GetEmlBlob(string token, string applno, string containname, string filname, string subFldr)
    {
        RequestGetBlobStorage putBlobStorage = new RequestGetBlobStorage();
        putBlobStorage.applicationNumber = applno.ToLower();
        putBlobStorage.containName = containname;
        putBlobStorage.subFolder = subFldr;
        putBlobStorage.fileName = filname;
        putBlobStorage.token = token;

        var json2 = SimpleJson.SerializeObject(putBlobStorage);
        var webResponse2 = objComm.GenerateWebRequest(BlobGetAPI, json2);
        var vOthr = new JavaScriptSerializer().Deserialize<ResponseGetBlobStorage>(webResponse2);

        if (vOthr.errorCode == "0")
        {
            return vOthr.filePath;
        }
        else
        {
            return "";
        }
    }


}
//1.1 End of Changes; Sagar Thorave - [MFL00886]