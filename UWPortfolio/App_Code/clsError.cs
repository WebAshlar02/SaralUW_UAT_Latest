using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for clsError
/// </summary>
public class clsError
{
    public string errorCode { get; set; }
    public string Description { get; set; }
    public string ErrorMessage { get; set; }
    public string pulse { get; set; }
    public string systolic1 { get; set; }
    public string systolic2 { get; set; }
    public string systolic3 { get; set; }
    public string diastolic1 { get; set; }
    public string diastolic2 { get; set; }
    public string diastolic3 { get; set; }
    public string GENDER { get; set; }
    public string HB { get; set; }
    public string PCV { get; set; }
    public string RBC { get; set; }
    public string MCV { get; set; }
    public string MCH { get; set; }
    public string MCHC { get; set; }
    public string WBC { get; set; }
    public string NEUTROPHILS { get; set; }
    public string LYMPHOCYTES { get; set; }
    public string EOSINOPHILS { get; set; }
    public string MONOCYTES { get; set; }
    public string BASOPHILS { get; set; }
    public string PLATELET_COUNT { get; set; }
    public string ESR { get; set; }
    public string SGOT { get; set; }
    public string SGPT { get; set; }
    public string GGT { get; set; }
    public string Billirubin1 { get; set; }
    public string Billirubin2 { get; set; }
    public string ALP { get; set; }
    public string S_Globilin { get; set; }
    public string S_Albumin { get; set; }
    public string TotalProtein { get; set; }
    public string AGRatio { get; set; }
    public string Cholestrol { get; set; }
    public string HDL { get; set; }
    public string Triglyceride { get; set; }
    public string HdlRatio { get; set; }
    public string S_Creatine { get; set; }
    public string UricAcid { get; set; }
    public string Bun { get; set; }
    public string PUS { get; set; }
    public string RBC_FBS { get; set; }
    


    public clsError()
    {
        errorCode = string.Empty;
        Description = string.Empty;
        ErrorMessage = string.Empty;
        pulse = string.Empty;
        systolic1 = string.Empty;
        systolic2 = string.Empty;
        systolic3 = string.Empty;
        diastolic1 = string.Empty;
        diastolic2 = string.Empty;
        diastolic3 = string.Empty;
        GENDER = string.Empty;
        HB = string.Empty;
        PCV = string.Empty;
        RBC = string.Empty;
        MCV = string.Empty;
        MCH = string.Empty;
        MCHC = string.Empty;
        WBC = string.Empty;
        NEUTROPHILS = string.Empty;
        LYMPHOCYTES = string.Empty;
        EOSINOPHILS = string.Empty;
        MONOCYTES = string.Empty;
        BASOPHILS = string.Empty;
        PLATELET_COUNT = string.Empty;
        ESR = string.Empty;

        SGOT = string.Empty;
        SGPT = string.Empty;
        GGT = string.Empty;
        Billirubin1 = string.Empty;
        Billirubin2 = string.Empty;
        ALP = string.Empty;
        S_Globilin = string.Empty;
        S_Albumin = string.Empty;
        TotalProtein = string.Empty;
        AGRatio = string.Empty;

        Cholestrol = string.Empty;
        HDL = string.Empty;
        Triglyceride = string.Empty;
        HdlRatio = string.Empty;

        S_Creatine = string.Empty;
        UricAcid = string.Empty;
        Bun = string.Empty;

        PUS = string.Empty;
        RBC_FBS = string.Empty;
        //
        // TODO: Add constructor logic here
        //
    }
}