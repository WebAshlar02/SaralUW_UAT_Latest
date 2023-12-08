using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CommanObj
/// </summary>
public class CommanObj
{
    public string _ApplicationNo { get; set; }
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