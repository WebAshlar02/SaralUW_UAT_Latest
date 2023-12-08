using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class ReferenceRequest
{
    public string ReferenceID { get; set; }
}

public class ReferenceResponse
{
    public ReferenceResponseResult ResponseResult { get; set; }
    public ReferenceResponseBody ResponseBody { get; set; }
}


public class ReferenceResponseResult
{
    public string responseCode { get; set; }
    public string responseMessage { get; set; }
    public string responseDescription { get; set; }
}

public class ReferenceResponseBody
{
    public string json { get; set; }
}

public class DedupeRequest
{
    public RequestSignature RequestSignature { get; set; }
    public RequestBody RequestBody { get; set; }
}

public class RequestSignature
{
    public string Source { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string UserRole { get; set; }
    public string UserGrp { get; set; }
    public string TaskID { get; set; }
}
public class RequestBody
{
    public string AssuredType { get; set; }
    public string BranchName { get; set; }
    public string BranchCode { get; set; }
    public DateTime? BusinessDate { get; set; }
    public string ClientType { get; set; }
    public string ApplicationNo { get; set; }
    public string Salutation { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public Nullable<DateTime> DOB { get; set; }
    public string Gender { get; set; }
    public string PreferredLang { get; set; }
    public string Nationality { get; set; }
    public string Occupation { get; set; }
    public string PANNo { get; set; }
    public string AadhaarNumber { get; set; }
    public Nullable<DateTime> DateOfIncorporation { get; set; }
    public string LeadID { get; set; }
    public string CltApplicationNo { get; set; }
    public string CltSalutation { get; set; }
    public string CltFirstname { get; set; }
    public string CltLastname { get; set; }
    public Nullable<DateTime> CltDOB { get; set; }
    public string CltGender { get; set; }
    public string CltPreferredLang { get; set; }
    public string CltNationality { get; set; }
    public string CurrentOccupation { get; set; }
    public string CltPanNo { get; set; }
    public string CltAadhaarNumber { get; set; }
    public string AMLPropSpecialInsurance { get; set; }
    public string SpecialInsurance { get; set; }
    public string IsMarried { get; set; }
    public string PrevPolNo { get; set; }
    public string ClientPrevPolNo { get; set; }
    public string ClientCode { get; set; }
    public string CompanyName { get; set; }
    public string IIBvalue { get; set; }
    public string ReasonForNCC { get; set; }
    public string ReasonForNCCText { get; set; }
    public string AddressType { get; set; }
    public string Addressline1 { get; set; }
    public string Addressline2 { get; set; }
    public string Addressline3 { get; set; }
    public string Landmark { get; set; }
    public string Pincode { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string EmailId { get; set; }
    public string MobileNumber { get; set; }
    public string AlternateNumber { get; set; }
    public string MaritalStatus { get; set; }
    public string Relationship { get; set; }
    public string ScreenType { get; set; }
    public string Type { get; set; }
    public string Processname { get; set; }
    public string Fathername { get; set; }
    public string Mothername { get; set; }
    public string Redirecturl { get; set; }
    public int IsCombi { get; set; }
    public string CombiAppNo { get; set; }
}

public class DedupeResponse
{
    public ResponseResult responseResult { get; set; }
    public ResponseBody responseBody { get; set; }

}
public class ResponseResult
{
    public string responseCode { get; set; }
    public string responseMessage { get; set; }
    public string responseDescription { get; set; }
}


public class ResponseBody
{
    public string referenceID { get; set; }
    public string url { get; set; }
}