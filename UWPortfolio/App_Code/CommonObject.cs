using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

[Serializable]
public class CommonObject
{
    public string _ApplicationNo { get; set; }
    public string _Restrict_Further_Cover { get; set; }
    public string _AppType { get; set; }
    public string _PolicyNo { get; set; }
    public string _bpm_userID { get; set; }
    public string _bpm_userName { get; set; }
    public string _bpmgrp { get; set; }
    public string _bpm_branchCode { get; set; }
    public string _bpm_creationDate { get; set; }
    public string _bpm_systemDate { get; set; }
    public string _bpm_businessDate { get; set; }
    public string _bpm_userBranch { get; set; }
    public string _bpm_applicationName { get; set; }
    public string _ProductCode { get; set; }
    public string _ProductType { get; set; }
    public string _ProductName { get; set; }
    public string _ProcessName { get; set; }
    public string _ChannelType { get; set; }
    public UserInfo _Bpmuserdetails { get; set; }
}

public class UserInfo
{
    public string _UserID { get; set; }
    public string _UserGroup { get; set; }
    public string _UserName { get; set; }
    public string _MinSumassured { get; set; }
    public string _MaxSumassured { get; set; }
    public string _userBranch { get; set; }
    public string _UserRole { get; set; }
    public string _UserMessage { get; set; }
}
[Serializable]
public class CommonViewState
{
    public bool IsCommentSave { get; set; }
}
//1-1 Start of changes: Sagar Thorave [MFL00886] CR- 30573
public class BmpsDataListModel {
    public string policyid { get; set; }
    public string policyriskflag { get; set; }
   
}
//1-1 END of changes: Sagar Thorave [MFL00886] CR- 30573

//1-1 Start of changes: Sushant Devkate [MFL00905] CR- 30363
public class PinCodeDO
{
   public string CircleName { get; set; }
public string RegionName { get; set; }
public string DivisionName { get; set; }
public string OfficeName { get; set; }
public string PinCode { get; set; }
public string OfficeType { get; set; }
public string District { get; set; }
public string StateName { get; set; }
public string  Delivery { get; set; }
public string Remarks { get; set; }
public string UserId { get; set; }

}
//1-1 END of changes: Sushant Devkate [MFL00905] CR- 30363

//Mer videos Sagar Thorave [MFL00886]
public class BlobDO
{

}

public class RequestGenerateToken
{
    public string userName { get; set; }
    public string password { get; set; }
    public string source { get; set; }
}

public class ResponseGenerateToken
{
    public string tokenErrorCode { get; set; }
    public string tokenErrorDesc { get; set; }
    public string token { get; set; }
}
public class RequestGetBlobStorage
{
    public string applicationNumber { get; set; }
    public string containName { get; set; }
    public string subFolder { get; set; }
    public string fileName { get; set; }
    public string fileBytes { get; set; }
    public string token { get; set; }
}
public class ResponseGetBlobStorage
{
    public string errorCode { get; set; }
    public string errorDesc { get; set; }
    public string gender { get; set; }
    public string bmi { get; set; }
    public string age { get; set; }
    public string sentiment { get; set; }
    public string filePath { get; set; }
}


//Mfl00886 Sagar Thorave MerVideos

public class ValidationResponseDO
{
    public bool Issuccess { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public bool ContactDetailsMatched { get; set; }
    public bool NameMatched { get; set; }

    public bool IsAppendMsg { get; set; }
}

public class ValidationDO
{
    public string Message { get; set; }
    public bool IsValid { get; set; }

    public bool IsAppendMsg { get; set; }

    public string ClientType { get; set; }
}


