#region "Decleration"

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using UWSaralObjects;
#endregion

public partial class Appcode_MedicalDataEntry : System.Web.UI.Page
{
    #region"Common Objects"

    static string strApplicationno = string.Empty;
    string strPolicyNo = string.Empty;
    public static string connection = ConfigurationManager.AppSettings["transactiondbLF"];
    static Commfun objComm = new Commfun();
    static BussLayer objBuss = new BussLayer();
    static CommonObject objCommonObj = new CommonObject();
    static ChangeValue objChangeObj = new ChangeValue();
    static UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
    static DataSet _ds = new DataSet();
    static DataSet _dsPrevPol = new DataSet();
    public static int strLApushErrorCode;
    public static string strLApushStatus;
    public static string strConsentRespons = string.Empty;
    static DataLayer objDal = new DataLayer();
    static clsError objclsError = new clsError();
    public string Calculatedage = string.Empty;
    #endregion

    #region "Events"
    #region "PageLoad"

    protected void Page_Load(object sender, EventArgs e)
    {

        objCommonObj = (CommonObject)Session["objCommonObj"];
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        Ajax.Utility.RegisterTypeForAjax(typeof(Appcode_MedicalDataEntry));

        if (!IsPostBack)
        {
            if (Request.QueryString["qsAppNo"] != null)
            {
                //strApplicationno = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
                strApplicationno = Request.QueryString["qsAppNo"];
            }
            if (Request.QueryString["qsPolicyNo"] != null)
            {
                //strPolicyNo = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsPolicyNo"]);
                strPolicyNo = Request.QueryString["qsPolicyNo"];
            }
            BindDataToControl();
        }

    }

    #endregion
    #endregion

    #region "Functions"

    #region "Bind data to form"
    public void BindDataToControl()
    {
        ComparePersonalInfo();
        StringReader stream = null;
        XmlTextReader reader = null;

        SqlConnection con = new SqlConnection(connection);
        con.Open();
        SqlCommand cmd = new SqlCommand("USP_RETRIVE_MEDICAL_DATA", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@QSAPPNO", strApplicationno);


        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string xmlData = dt.Rows[0]["REQUEST_XML"].ToString();
            stream = new StringReader(xmlData);
            reader = new XmlTextReader(stream);
        }
        DataSet ds = new DataSet();
        ds.ReadXml(reader);
        //txtAGE.Text = xmlData 

        //dt.ReadXml(reader);
        if (ds != null && ds.Tables.Count > 0)
        {
            #region "Mrf Data"
            string appNo = ViewState["AppNum"].ToString();
            if (appNo == strApplicationno)
            {
                txtAppno.Text = strApplicationno;
            }
            else
            {
                lblErrorAppNo.Text = "Application number mismatch";
            }
            ddlGender.SelectedValue = ds.Tables[1].Rows[0]["Gender"].ToString();
            objclsError.GENDER = ddlGender.SelectedValue;
            txtDOB.Text = ds.Tables[1].Rows[0]["DOB"].ToString();


            if (txtDOB.Text != "")
            {
                DateTime dateOfBirth = Convert.ToDateTime(txtDOB.Text);
                Calculatedage = Convert.ToString(CalculateAge(dateOfBirth));
            }
            txtAGE.Text = ds.Tables[1].Rows[0]["AGE"].ToString();
            if (txtAGE.Text == "" || txtAGE.Text == "0")
            {
                txtAGE.Text = Calculatedage;
            }
            txtHeight.Text = ds.Tables[1].Rows[0]["Height"].ToString();
            txtWeight.Text = ds.Tables[1].Rows[0]["Weight"].ToString();
            string BMI = ds.Tables[1].Rows[0]["BMI"].ToString();
            if (BMI == "" || BMI == null || BMI == "NaN")
            {
                txtBMI.Text = "";
            }
            else
            {
                txtBMI.Text = BMI;
            }
            txtPulse.Text = ds.Tables[1].Rows[0]["Pulse"].ToString();
            objclsError.pulse = txtPulse.Text;
            txtChestInspiration.Text = ds.Tables[1].Rows[0]["ChestInspiration"].ToString();
            txtChestExpiration.Text = ds.Tables[1].Rows[0]["ChestExpiration"].ToString();
            txtSystolic.Text = ds.Tables[1].Rows[0]["Systolic1"].ToString();
            objclsError.systolic1 = txtSystolic.Text;
            txtSystolic1.Text = ds.Tables[1].Rows[0]["Systolic2"].ToString();
            objclsError.systolic2 = txtSystolic1.Text;
            txtSystolic2.Text = ds.Tables[1].Rows[0]["Systolic3"].ToString();
            objclsError.systolic3 = txtSystolic2.Text;
            txtDiastolic.Text = ds.Tables[1].Rows[0]["Diastolic1"].ToString();
            objclsError.diastolic1 = txtDiastolic.Text;
            txtDiastolic1.Text = ds.Tables[1].Rows[0]["Diastolic2"].ToString();
            objclsError.diastolic2 = txtDiastolic1.Text;
            txtDiastolic2.Text = ds.Tables[1].Rows[0]["Diastolic3"].ToString();
            objclsError.diastolic3 = txtDiastolic2.Text;
            txtGirth.Text = ds.Tables[1].Rows[0]["Girth"].ToString();

            int CaseHTN = Convert.ToInt32(ds.Tables[1].Rows[0]["CaseHTN"]);
            if (CaseHTN == 1)
            {

                chkHTNCase.Checked = true;
                caseHTNComments.Attributes.Add("style", "display:block");
                txtHTNComments.Text = ds.Tables[1].Rows[0]["caseHTNComments"].ToString();

                divHTNCase.Visible = true;
                lblHTNError.Text = "HTN rate accordingly";
            }
            else
            {
                //chkHTNCase.Checked = false;
                ////txtHTNComments.Visible = false;
                caseHTNComments.Attributes.Add("style", "display:none");

            }

            int caseDM = Convert.ToInt32(ds.Tables[1].Rows[0]["caseDM"]);
            if (caseDM == 1)
            {
                chkDMCase.Checked = true;
                caseDmComments.Attributes.Add("style", "display:blcok");
                txtDMComments.Text = ds.Tables[1].Rows[0]["caseDMComments"].ToString();

                lblDMError.Text = "DM rate accordingly";
            }
            else
            {
                //chkDMCase.Checked = false;
                caseDmComments.Attributes.Add("style", "display:none");
            }

            int isSmoker = Convert.ToInt32(ds.Tables[1].Rows[0]["Smoker"]);
            if (isSmoker == 1)
            {
                chkSmoker.Checked = true;
                SmokerComments.Attributes.Add("style", "display:block");
                txtSmokerComments.Text = ds.Tables[1].Rows[0]["SmokerCommnets"].ToString();
                lblSmokerWarning.Text = "Smoker rates applicable";
            }
            else
            {
                //chkSmoker.Checked = false;
                SmokerComments.Attributes.Add("style", "display:none");
            }

            int isAlcohol = Convert.ToInt32(ds.Tables[1].Rows[0]["Alcohol"]);
            if (isAlcohol == 1)
            {
                chkAlcohol.Checked = true;
                //AlcoholComments.Attributes.Add("style", "display:block");
                //txtAlcoholComments.Text = ds.Tables[1].Rows[0]["AlcoholComments"].ToString();
            }
            else
            {
                //chkAlcohol.Checked = false;
                //AlcoholComments.Attributes.Add("style", "display:none");

            }
            txtOtherComments.Text = ds.Tables[1].Rows[0]["Comments"].ToString();
            #endregion

            #region"CBC&ESR"
            txtHB.Text = ds.Tables[2].Rows[0]["HB"].ToString();
            objclsError.HB = txtHB.Text;
            txtPCV.Text = ds.Tables[2].Rows[0]["PCV"].ToString();
            objclsError.PCV = txtPCV.Text;
            txtRBC.Text = ds.Tables[2].Rows[0]["RBC"].ToString();
            objclsError.RBC = txtRBC.Text;
            txtMCV.Text = ds.Tables[2].Rows[0]["MCV"].ToString();
            objclsError.MCV = txtMCV.Text;
            txtMCH.Text = ds.Tables[2].Rows[0]["MCH"].ToString();
            objclsError.MCH = txtMCH.Text;
            txtMCHC.Text = ds.Tables[2].Rows[0]["MCHC"].ToString();
            objclsError.MCHC = txtMCHC.Text;
            txtWBC.Text = ds.Tables[2].Rows[0]["WBC"].ToString();
            objclsError.WBC = txtWBC.Text;
            txtNEUTROPHILS.Text = ds.Tables[2].Rows[0]["NEUTROPHILS"].ToString();
            objclsError.NEUTROPHILS = txtNEUTROPHILS.Text;
            txtLYMPHOCYTES.Text = ds.Tables[2].Rows[0]["LYMPHOCYTES"].ToString();
            objclsError.LYMPHOCYTES = txtLYMPHOCYTES.Text;
            txtEOSINOPHILS.Text = ds.Tables[2].Rows[0]["EOSINOPHILS"].ToString();
            objclsError.EOSINOPHILS = txtEOSINOPHILS.Text;
            txtMONOCYTES.Text = ds.Tables[2].Rows[0]["MONOCYTES"].ToString();
            objclsError.MONOCYTES = txtMONOCYTES.Text;
            txtBASOPHILS.Text = ds.Tables[2].Rows[0]["BASOPHILS"].ToString();
            objclsError.BASOPHILS = txtBASOPHILS.Text;
            txtPLATELETCOUNT.Text = ds.Tables[2].Rows[0]["PLATELETCOUNT"].ToString();
            objclsError.PLATELET_COUNT = txtPLATELETCOUNT.Text;
            txtESR.Text = ds.Tables[2].Rows[0]["ESR"].ToString();
            objclsError.ESR = txtESR.Text;
            #endregion

            #region "HBSAG"
            int chkNAHbsag = Convert.ToInt32(ds.Tables[3].Rows[0]["NA"].ToString());
            if (chkNAHbsag == 1)
            {
                chkHBSAG_NA.Checked = true;
            }

            int chkPositiveHbsag = Convert.ToInt32(ds.Tables[3].Rows[0]["Positive"].ToString());
            if (chkPositiveHbsag == 1)
            {
                chkHBSAG_POSITITVE.Checked = true;
            }
            int chkNegativeHbsag = Convert.ToInt32(ds.Tables[3].Rows[0]["Negative"].ToString());
            if (chkNegativeHbsag == 1)
            {
                chkHBSAG_NEGATIVE.Checked = true;
            }

            #endregion

            #region "HIV"
            int chkNAHIV = Convert.ToInt32(ds.Tables[4].Rows[0]["NA"].ToString());
            if (chkNAHIV == 1)
            {
                chkhiv_NA.Checked = true;
            }

            int chkPositiveHIV = Convert.ToInt32(ds.Tables[4].Rows[0]["Positive"].ToString());
            if (chkPositiveHIV == 1)
            {
                chkhiv_POSITIVE.Checked = true;
            }
            int chkNegativeHIV = Convert.ToInt32(ds.Tables[4].Rows[0]["Negative"].ToString());
            if (chkNegativeHIV == 1)
            {
                chkhiv_NEGATIVE.Checked = true;
            }
            #endregion

            #region "FBS, RUA"
            txtDuration.Text = ds.Tables[5].Rows[0]["Duration"].ToString();
            txtFBS.Text = ds.Tables[5].Rows[0]["FBS"].ToString();
            txtHBA1C.Text = ds.Tables[5].Rows[0]["HBA1C"].ToString();
            txtPUS.Text = ds.Tables[5].Rows[0]["PucCells"].ToString();
            //txtRUA.Text = ds.Tables[5].Rows[0][2].ToString();
            //ddlRBC.SelectedValue = ds.Tables[5].Rows[0]["RBC"].ToString();
            txtRBC_FBS.Text = ds.Tables[5].Rows[0]["RBC"].ToString();
            ddl_ALBUMIN.SelectedValue = ds.Tables[5].Rows[0]["Albumin"].ToString();
            ddlSUGAR.SelectedValue = ds.Tables[5].Rows[0]["Sugar"].ToString();
            //ddlPUC.SelectedValue = ds.Tables[5].Rows[0]["PucCells"].ToString();
            ddlOthers.SelectedValue = ds.Tables[5].Rows[0]["Others"].ToString();
            objclsError.PUS = txtPUS.Text;
            objclsError.RBC_FBS = txtRBC_FBS.Text;
            #endregion

            #region "LFT Test"

            txtSGOT.Text = ds.Tables[6].Rows[0]["SGOT"].ToString();
            objclsError.SGOT = txtSGOT.Text;
            txtSGPT.Text = ds.Tables[6].Rows[0]["SGPT"].ToString();
            objclsError.SGPT = txtSGPT.Text;
            txtGGT.Text = ds.Tables[6].Rows[0]["GGT"].ToString();
            objclsError.GGT = txtGGT.Text;
            txtBILLIRUBIN.Text = ds.Tables[6].Rows[0]["Billirubin1"].ToString();
            objclsError.Billirubin1 = txtBILLIRUBIN.Text;
            txtBILLIRUBIN2.Text = ds.Tables[6].Rows[0]["Billirubin2"].ToString();
            objclsError.Billirubin2 = txtBILLIRUBIN2.Text;
            txtTotalBILLIRUBIN.Text = ds.Tables[6].Rows[0]["TotalBillirubin"].ToString();
            txtALP.Text = ds.Tables[6].Rows[0]["ALP"].ToString();
            objclsError.ALP = txtALP.Text;
            txtS_GLOBILIN.Text = ds.Tables[6].Rows[0]["S_Globilin"].ToString();
            objclsError.S_Globilin = txtS_GLOBILIN.Text;
            txtS_ALBUMIN.Text = ds.Tables[6].Rows[0]["S_Albumin"].ToString();
            objclsError.S_Albumin = txtS_ALBUMIN.Text;
            txtTOTAL_PROTEIN.Text = ds.Tables[6].Rows[0]["TotalProtein"].ToString();
            objclsError.TotalProtein = txtTOTAL_PROTEIN.Text;
            txtAgRatio.Text = ds.Tables[6].Rows[0]["AGRatio"].ToString();
            objclsError.AGRatio = txtAgRatio.Text;
            #endregion

            #region "LIPIDS Test"

            txtCHOLESTEROL.Text = ds.Tables[7].Rows[0]["Cholestrol"].ToString();
            txtHDL.Text = ds.Tables[7].Rows[0]["HDL"].ToString();
            txtTRIGLYCERIDE.Text = ds.Tables[7].Rows[0]["Triglyceride"].ToString();
            objclsError.Triglyceride = txtTRIGLYCERIDE.Text;
            txtTcRatio.Text = ds.Tables[7].Rows[0]["HdlRatio"].ToString();

            #endregion

            #region"RFT Test"

            txtS_CREATININE.Text = ds.Tables[8].Rows[0]["S_Creatine"].ToString();
            objclsError.S_Creatine = txtS_CREATININE.Text;
            txtURIC_ACID.Text = ds.Tables[8].Rows[0]["UricAcid"].ToString();
            objclsError.UricAcid = txtS_CREATININE.Text;
            txtBUN.Text = ds.Tables[8].Rows[0]["Bun"].ToString();
            objclsError.Bun = txtBUN.Text;

            #endregion

            #region "ECG Test"

            txtDateOfTest.Text = ds.Tables[9].Rows[0]["DateOfECGTest"].ToString();
            txtECGDecision.Text = ds.Tables[9].Rows[0]["ECGDecision"].ToString();
            txtCMODecisionECG.Text = ds.Tables[9].Rows[0]["CmoDecisionECG"].ToString();
            #endregion

            #region "CTMT Test"

            dtTestMedication.Text = ds.Tables[10].Rows[0]["DateOfCtmtTest"].ToString();
            txtMedication.Text = ds.Tables[10].Rows[0]["Medication"].ToString();
            txtExerciseTime.Text = ds.Tables[10].Rows[0]["ExerciseTime"].ToString();
            txtWorkLoad.Text = ds.Tables[10].Rows[0]["WorkLoad"].ToString();
            txtBP.Text = ds.Tables[10].Rows[0]["BP"].ToString();
            txtTHR.Text = ds.Tables[10].Rows[0]["THR"].ToString();
            txtResonTermination.Text = ds.Tables[10].Rows[0]["TerminationReason"].ToString();
            txtCTMTDecision.Text = ds.Tables[10].Rows[0]["Decision"].ToString();
            txtCMODecisionCTMT.Text = ds.Tables[10].Rows[0]["CmoDecisionCTMT"].ToString();
            #endregion
            //var sample = objclsError;
            var objClsErrorJSONFormat = JsonConvert.SerializeObject(objclsError);
            ValidatePageLoadMedicalData(objClsErrorJSONFormat);


            Hba1cValidation(txtAGE.Text, txtHBA1C.Text, txtDuration.Text);


        }
        else
        {
            string appNo = string.Empty;
            appNo = ViewState["AppNum"].ToString();
            if (appNo == strApplicationno)
            {
                txtAppno.Text = strApplicationno;
            }
            else
            {
                lblErrorAppNo.Text = "Application number mismatch";
            }
            caseHTNComments.Attributes.Add("style", "display:none");
            caseDmComments.Attributes.Add("style", "display:none");
            SmokerComments.Attributes.Add("style", "display:none");
            //AlcoholComments.Attributes.Add("style", "display:none");
        }
        //XmlDocument xdoc = new XmlDocument();
        ////XmlNode node = new XmlNode();
        //xdoc.Load(stream);
        //foreach (XmlNode node in xdoc)
        //{
        //    XmlElement AppNo = node["AppNo"];
        //    string AppN = xdoc.InnerText[

        //}


        //if(dt.Rows.Count <= 1)
        //{
        //    txtAppno.Text = dt.Rows[0]["APP_NO"].ToString();
        //    //txtAGE.Text = dt.Rows.ReadXml
        //}

        con.Close();
    }
    #endregion

    #region
    public DataSet ValidatePageLoadMedicalData(string strError)
    {
        try
        {
            string response = string.Empty;
            clsError objclsError = JsonConvert.DeserializeObject<clsError>(strError);
            SqlParameter[] _sqlParam = new SqlParameter[41];
            _sqlParam[0] = new SqlParameter("@PULSE", objclsError.pulse);
            _sqlParam[1] = new SqlParameter("@SYSTOLIC1", objclsError.systolic1);
            _sqlParam[2] = new SqlParameter("@SYSTOLIC2", objclsError.systolic2);
            _sqlParam[3] = new SqlParameter("@SYSTOLIC3", objclsError.systolic3);
            _sqlParam[4] = new SqlParameter("@DIASTOLIC1", objclsError.diastolic1);
            _sqlParam[5] = new SqlParameter("@DIASTOLIC2", objclsError.diastolic2);
            _sqlParam[6] = new SqlParameter("@DIASTOLIC3", objclsError.diastolic3);
            _sqlParam[7] = new SqlParameter("@GENDER", objclsError.GENDER);
            _sqlParam[8] = new SqlParameter("@HB", objclsError.HB);
            _sqlParam[9] = new SqlParameter("@PCV", objclsError.PCV);
            if (objclsError.RBC == "-")
            {
                objclsError.RBC = "";
            }
            _sqlParam[10] = new SqlParameter("@RBC", objclsError.RBC);
            _sqlParam[11] = new SqlParameter("@MCV", objclsError.MCV);
            _sqlParam[12] = new SqlParameter("@MCH", objclsError.MCH);
            _sqlParam[13] = new SqlParameter("@MCHC", objclsError.MCHC);
            _sqlParam[14] = new SqlParameter("@WBC", objclsError.WBC);
            _sqlParam[15] = new SqlParameter("@NEUTROPHILS", objclsError.NEUTROPHILS);
            _sqlParam[16] = new SqlParameter("@LYMPHOCYTES", objclsError.LYMPHOCYTES);
            _sqlParam[17] = new SqlParameter("@EOSINOPHILS", objclsError.EOSINOPHILS);
            _sqlParam[18] = new SqlParameter("@MONOCYTES", objclsError.MONOCYTES);
            _sqlParam[19] = new SqlParameter("@BASOPHILS", objclsError.BASOPHILS);
            _sqlParam[20] = new SqlParameter("@PLATELET_COUNT", objclsError.PLATELET_COUNT);
            _sqlParam[21] = new SqlParameter("@ESR", objclsError.ESR);
            _sqlParam[22] = new SqlParameter("@SGOT", objclsError.SGOT);
            _sqlParam[23] = new SqlParameter("@SGPT", objclsError.SGPT);
            _sqlParam[24] = new SqlParameter("@GGT", objclsError.GGT);
            _sqlParam[25] = new SqlParameter("@BILLIRUBIN1", objclsError.Billirubin1);
            _sqlParam[26] = new SqlParameter("@BILLIRUBIN2", objclsError.Billirubin2);
            _sqlParam[27] = new SqlParameter("@ALP", objclsError.ALP);
            _sqlParam[28] = new SqlParameter("@S_GLOBILIN", objclsError.S_Globilin);
            _sqlParam[29] = new SqlParameter("@S_ALBUMIN", objclsError.S_Albumin);
            _sqlParam[30] = new SqlParameter("@TOTALPROTEIN", objclsError.TotalProtein);
            _sqlParam[31] = new SqlParameter("@AGRATIO", objclsError.AGRatio);
            _sqlParam[32] = new SqlParameter("@CHOLESTROL", "");
            _sqlParam[33] = new SqlParameter("@HDL", "");
            _sqlParam[34] = new SqlParameter("@TRIGLYCERIDE", objclsError.Triglyceride);
            _sqlParam[35] = new SqlParameter("@HDLRATIO", "");
            _sqlParam[36] = new SqlParameter("@S_CREATINE", objclsError.S_Creatine);
            _sqlParam[37] = new SqlParameter("@URICACID", objclsError.UricAcid);
            _sqlParam[38] = new SqlParameter("@BUN", objclsError.Bun);
            if (objclsError.PUS == "-")
            {
                objclsError.PUS = "";
            }
            _sqlParam[39] = new SqlParameter("@PUS", objclsError.PUS);
            if (objclsError.RBC_FBS == "-")
            {
                objclsError.RBC_FBS = "";
            }
            _sqlParam[40] = new SqlParameter("@RBC_FBS", objclsError.RBC_FBS);
            _ds = objDal.RetrieveDataset("USP_RETRIVE_MEDICAL_FEILDS_ERROR_ON_PAGELOAD_V1", _sqlParam);

            //var pulse = _ds
            string Msgs = JsonConvert.SerializeObject(_ds);


            if (_ds != null && _ds.Tables.Count > 0)
            {
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drMedical in _ds.Tables[0].Rows)
                    {
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("PULSE"))
                        {
                            lblValidatePulse.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("SYSTOLIC1"))
                        {
                            lblValidateSystolic1.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("SYSTOLIC2"))
                        {
                            lblValidateSystolic2.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("SYSTOLIC3"))
                        {
                            lblValidateSystolic3.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("DIASTOLIC1"))
                        {
                            lblValidateDiastolic1.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("DIASTOLIC2"))
                        {
                            lblValidateDiastolic2.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("DIASTOLIC3"))
                        {
                            lblValidateDiastolic3.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("HB"))
                        {
                            lblValidateHB.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("PCV"))
                        {
                            lblValidatePCV.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("RBC"))
                        {
                            lblValidateRBC.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("MCV"))
                        {
                            lblValidateMCV.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("MCH"))
                        {
                            lblValidateMCH.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("MCHC"))
                        {
                            lblValidateMCHC.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("WBC"))
                        {
                            lblValidateWBC.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("NEUTROPHILS"))
                        {
                            lblValidateNEUTROPHILS.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("LYMPHOCYTES"))
                        {
                            lblValidateLYMPHOCYTES.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("EOSINOPHILS"))
                        {
                            lblValidateEOSINOPHILS.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("MONOCYTES"))
                        {
                            lblValidateMONOCYTES.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("BASOPHILS"))
                        {
                            lblValidateBASOPHILS.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("PLATELET"))
                        {
                            lblValidatePLATELETCOUNT.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("ESR"))
                        {
                            lblValidateESR.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("SGOT"))
                        {
                            lblValidateSGOT.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("SGPT"))
                        {
                            lblValidateSGPT.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("GGT"))
                        {
                            lblValidateGGT.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("DIRECT_BILLIRUBIN"))
                        {
                            lblValidateBILLIRUBIN.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("INDIRECT_BILLIRUBIN"))
                        {
                            lblValidateBILLIRUBIN1.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("ALP"))
                        {
                            lblValidateALP.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("S_GLOBILIN"))
                        {
                            lblValidateS_GLOBILIN.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("S_ALBUMIN"))
                        {
                            lblValidateS_ALBUMIN.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("TOTALPROTEIN"))
                        {
                            lblValidateT_PROTEIN.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("AGRATIO"))
                        {
                            lblValidateAGRATIO.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("TRIGLYCERIDE"))
                        {
                            lblValidateTRIGYCERIDE.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("S_CREATINE"))
                        {
                            lblValidateS_CREATINE.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("URICACID"))
                        {
                            lblValidateURIC_ACID.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("BUN"))
                        {
                            lblValidateBUN.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("PUS"))
                        {
                            lblValidateddlPUC.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                        if (Convert.ToString(drMedical["FIELD_NAME"]).Equals("RBC_FBS"))
                        {
                            lblValidateDdlRBC.Text = Convert.ToString(drMedical["ERROR_MSG"]);
                        }
                    }
                }
            }

            return _ds;

        }
        catch (Exception ex)
        {

            throw;
        }


    }
    #endregion

    #region "Calculate Age"
    public static int CalculateAge(DateTime dateOfBirth)
    {
        int age = 0;
        age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age = age - 1;

        return age;
    }
    #endregion

    #region "Check smoker on Uwdecision page"
    [WebMethod]
    public static void CheckIsSmoker(string ismokerval)
    {

        try
        {
            int intRet = -1;
            //BussLayer objBuss = new BussLayer();
            string strUserId = string.Empty;
            objBuss.UpdateUWDecitionAdditionalInfoFlag(strApplicationno, Convert.ToBoolean(ismokerval), 3, ref intRet);
            if (intRet > 0)
            {
                strUserId = objCommonObj._Bpmuserdetails._UserID.ToUpper();
                objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, "OFFLINE", objChangeObj, ref _ds, ref _dsPrevPol, "CONTRACTMODIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
                objBuss.InsertUserChangesLog(strApplicationno, (strUserId.IndexOf('F') == 0) ? strUserId.Substring(1) : strUserId, "SMOKER", Convert.ToString(ismokerval), ref intRet);
                //lblErrorDetailsRiskParameter.Text = "Smoker Updated Successfully";
            }
            else
            {
                //lblErrorDetailsRiskParameter.Text = "Smoker Not Updated Successfully";
            }
        }
        catch (Exception ex)
        {

            //lblErrorDetailsRiskParameter.Text = "Try Again Later";
        }
    }

    #endregion

    #region "BMI report"
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string BmiReports(string age, string bmi)
    {
        string response = string.Empty;
        try
        {
            if (Convert.ToInt32(age) >= 18 && Convert.ToInt32(age) <= 36)
            {
                SqlParameter[] _sqlParam = new SqlParameter[1];
                _sqlParam[0] = new SqlParameter("@CALCULATED_BMI", bmi);
                _ds = objDal.RetrieveDataset("USP_BMI_RESULTS_AGE_18TO36", _sqlParam);
                response = _ds.Tables[0].Rows[0][0].ToString();
            }

            else if (Convert.ToInt32(age) >= 37 && Convert.ToInt32(age) <= 40)
            {
                SqlParameter[] _sqlParam = new SqlParameter[1];
                _sqlParam[0] = new SqlParameter("@CALCULATED_BMI", bmi);
                _ds = objDal.RetrieveDataset("USP_BMI_RESULTS_AGE_37TO40", _sqlParam);
                response = _ds.Tables[0].Rows[0][0].ToString();
            }

            else if (Convert.ToInt32(age) >= 41 && Convert.ToInt32(age) <= 50)
            {
                SqlParameter[] _sqlParam = new SqlParameter[1];
                _sqlParam[0] = new SqlParameter("@CALCULATED_BMI", bmi);
                _ds = objDal.RetrieveDataset("USP_BMI_RESULTS_AGE_41TO50", _sqlParam);
                response = _ds.Tables[0].Rows[0][0].ToString();
            }

            else if (Convert.ToInt32(age) >= 51)
            {
                SqlParameter[] _sqlParam = new SqlParameter[1];
                _sqlParam[0] = new SqlParameter("@CALCULATED_BMI", bmi);
                _ds = objDal.RetrieveDataset("USP_BMI_RESULTS_AGE_ABOVE50", _sqlParam);
                response = _ds.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            //throw;
            //response = "please enter height and weight properly";
        }

        return response;
    }
    #endregion

    #region "HBA1C Validation"
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string Hba1cValidation(string age, string Hba1c, string duration)
    {
        string response = string.Empty;
        decimal InputAge = Convert.ToDecimal(age);
        decimal InputHba1c = Convert.ToDecimal(Hba1c);

        if (InputHba1c < 6 && duration == "")
        {
            response = "";
        }
        else
        {
            if (duration == "")
            {
                duration = "0";
            }
            SqlParameter[] _sqlParam = new SqlParameter[3];
            _sqlParam[0] = new SqlParameter("@HBA1C", InputHba1c);
            _sqlParam[1] = new SqlParameter("@AGE", InputAge);
            _sqlParam[2] = new SqlParameter("@DURATION", Convert.ToDecimal(duration));

            _ds = objDal.RetrieveDataset("USP_VALIDATE_HBA1C", _sqlParam);
            if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
            {
                response = _ds.Tables[0].Rows[0]["HBA1C_VALUE"].ToString();
                //lblValidateHBA1C.Text = Convert.ToString(response);
            }
        }

        return response;
    }

    #endregion

    #region "Cholestrol Validation"
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public string CholestrolReports(string age, string Result_Tc_HdlRatio)
    {
        string response = string.Empty;

        SqlParameter[] _sqlParam = new SqlParameter[2];
        _sqlParam[0] = new SqlParameter("@CALCULATED_CHOLESTROL", Result_Tc_HdlRatio);
        _sqlParam[1] = new SqlParameter("@AGE", age);
        _ds = objDal.RetrieveDataset("USP_CHOLESTROL_VALIDATION_RESULT", _sqlParam);
        if (_ds != null && _ds.Tables.Count > 0)
        {
            response = _ds.Tables[0].Rows[0][0].ToString();
        }
        return response;
    }
    #endregion

    #region "Retrive AppNo, Gender, DOB, AGE"
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public DataTable ComparePersonalInfo()
    {
        string response = string.Empty;

        SqlParameter[] _sqlParam = new SqlParameter[1];
        _sqlParam[0] = new SqlParameter("@APP_NO", strApplicationno);
        _ds = objDal.RetrieveDataset("USP_RETRIVE_USER_PERSONAL_INFO", _sqlParam);

        if (_ds.Tables.Count > 0)
        {
            string AppNO = _ds.Tables[0].Rows[0]["APPLICATION_NUM"].ToString();
            ViewState["AppNum"] = AppNO;
            return _ds.Tables[0];
        }
        else
        {
            return new DataTable();
        }
    }
    #endregion

    #region "Medical Data Feild Validation"
    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public DataSet ValidateMedicalData(string strError)
    {
        try
        {
            string response = string.Empty;
            clsError objclsError = JsonConvert.DeserializeObject<clsError>(strError);
            SqlParameter[] _sqlParam = new SqlParameter[41];
            _sqlParam[0] = new SqlParameter("@PULSE", objclsError.pulse);
            _sqlParam[1] = new SqlParameter("@SYSTOLIC1", objclsError.systolic1);
            _sqlParam[2] = new SqlParameter("@SYSTOLIC2", objclsError.systolic2);
            _sqlParam[3] = new SqlParameter("@SYSTOLIC3", objclsError.systolic3);
            _sqlParam[4] = new SqlParameter("@DIASTOLIC1", objclsError.diastolic1);
            _sqlParam[5] = new SqlParameter("@DIASTOLIC2", objclsError.diastolic2);
            _sqlParam[6] = new SqlParameter("@DIASTOLIC3", objclsError.diastolic3);
            _sqlParam[7] = new SqlParameter("@GENDER", objclsError.GENDER);
            _sqlParam[8] = new SqlParameter("@HB", objclsError.HB);
            _sqlParam[9] = new SqlParameter("@PCV", objclsError.PCV);
            _sqlParam[10] = new SqlParameter("@RBC", objclsError.RBC);
            _sqlParam[11] = new SqlParameter("@MCV", objclsError.MCV);
            _sqlParam[12] = new SqlParameter("@MCH", objclsError.MCH);
            _sqlParam[13] = new SqlParameter("@MCHC", objclsError.MCHC);
            _sqlParam[14] = new SqlParameter("@WBC", objclsError.WBC);
            _sqlParam[15] = new SqlParameter("@NEUTROPHILS", objclsError.NEUTROPHILS);
            _sqlParam[16] = new SqlParameter("@LYMPHOCYTES", objclsError.LYMPHOCYTES);
            _sqlParam[17] = new SqlParameter("@EOSINOPHILS", objclsError.EOSINOPHILS);
            _sqlParam[18] = new SqlParameter("@MONOCYTES", objclsError.MONOCYTES);
            _sqlParam[19] = new SqlParameter("@BASOPHILS", objclsError.BASOPHILS);
            _sqlParam[20] = new SqlParameter("@PLATELET_COUNT", objclsError.PLATELET_COUNT);
            _sqlParam[21] = new SqlParameter("@ESR", objclsError.ESR);
            _sqlParam[22] = new SqlParameter("@SGOT", objclsError.SGOT);
            _sqlParam[23] = new SqlParameter("@SGPT", objclsError.SGPT);
            _sqlParam[24] = new SqlParameter("@GGT", objclsError.GGT);
            _sqlParam[25] = new SqlParameter("@BILLIRUBIN1", objclsError.Billirubin1);
            _sqlParam[26] = new SqlParameter("@BILLIRUBIN2", objclsError.Billirubin2);
            _sqlParam[27] = new SqlParameter("@ALP", objclsError.ALP);
            _sqlParam[28] = new SqlParameter("@S_GLOBILIN", objclsError.S_Globilin);
            _sqlParam[29] = new SqlParameter("@S_ALBUMIN", objclsError.S_Albumin);
            _sqlParam[30] = new SqlParameter("@TOTALPROTEIN", objclsError.TotalProtein);
            _sqlParam[31] = new SqlParameter("@AGRATIO", objclsError.AGRatio);
            _sqlParam[32] = new SqlParameter("@CHOLESTROL", "");
            _sqlParam[33] = new SqlParameter("@HDL", "");
            _sqlParam[34] = new SqlParameter("@TRIGLYCERIDE", objclsError.Triglyceride);
            _sqlParam[35] = new SqlParameter("@HDLRATIO", "");
            _sqlParam[36] = new SqlParameter("@S_CREATINE", objclsError.S_Creatine);
            _sqlParam[37] = new SqlParameter("@URICACID", objclsError.UricAcid);
            _sqlParam[38] = new SqlParameter("@BUN", objclsError.Bun);
            if(objclsError.PUS=="-")
            {
                objclsError.PUS = "";
            }
            _sqlParam[39] = new SqlParameter("@PUS", objclsError.PUS);
            if (objclsError.RBC_FBS == "-")
            {
                objclsError.RBC_FBS = "";
            }
            _sqlParam[40] = new SqlParameter("@RBC_FBS", objclsError.RBC_FBS);
            _ds = objDal.RetrieveDataset("USP_RETRIVE_MEDICAL_FEILDS_ERROR_V1", _sqlParam);

            return _ds;

        }
        catch (Exception ex)
        {

            throw;
        }


    }
    #endregion

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
    public void SavelblMessage(string AllValidationError)
    {
        try
        {
            //string ErrorappNO = string.Empty;
            //string ErrorGender = string.Empty;
            //string ErrorDOB = string.Empty;
            //string ErrorAge = string.Empty;
            //string ErrorBmiResult = string.Empty;
            //string ErrorChestIE = string.Empty;
            //string ErrorSystolic1 = string.Empty;
            //string ErrorSystolic2 = string.Empty;
            //string ErrorSystolic3 = string.Empty;
            //string ErrorDiastolic1 = string.Empty;
            //string ErrorDiastolic2 = string.Empty;
            //string ErrorDiastolic3 = string.Empty;
            //string ErrorHTNError = string.Empty;
            //string ErrorDMError = string.Empty;
            //string ErrorSmokerWarning = string.Empty;
            //string ErrorPCV = string.Empty;
            //string ErrorRBC = string.Empty;
            //string ErrorMCV = string.Empty;
            //string ErrorMCH = string.Empty;
            //string ErrorMCHC = string.Empty;
            //string ErrorWBC = string.Empty;
            //string ErrorNEUTROPHILS = string.Empty;
            //string ErrorLYMPHOCYTES = string.Empty;
            //string ErrorEOSINOPHILS = string.Empty;
            //string ErrorMONOCYTES = string.Empty;
            //string ErrorBASOPHILS = string.Empty;
            //string ErrorPLATELETCOUNT = string.Empty;
            //string ErrorESR = string.Empty;
            //string ErrorHBSAG = string.Empty;
            //string ErrorHIV = string.Empty;
            //string ErrorSGOT = string.Empty;
            //string ErrorSGPT = string.Empty;
            //string ErrorGGT = string.Empty;
            //string ErrorBILLIRUBIN = string.Empty;
            //string ErrorBILLIRUBIN1 = string.Empty;
            //string ErrorTotalBILLIRUBIN = string.Empty;
            //string ErrorALP = string.Empty;
            //string ErrorS_GLOBILIN = string.Empty;
            //string ErrorS_ALBUMIN = string.Empty;
            //string ErrorT_PROTEIN = string.Empty;
            //string ErrorAGRATIO = string.Empty;
            //string ErrorCHOLESTEROL = string.Empty;
            //string ErrorHDL = string.Empty;
            //string ErrorTRIGYCERIDE = string.Empty;
            //string ErrorHDL_RATIO = string.Empty;
            //string ErrorS_CREATINE = string.Empty;
            //string ErrorURIC_ACID = string.Empty;
            //string ErrorBUN = string.Empty;

            //ErrorappNO = lblErrorAppNo.Text;
            //ErrorGender = lblCompareGender.Text;
            //ErrorDOB = lblCompareDOB.Text;
            //ErrorAge = lblCompareAge.Text;
            //ErrorBmiResult = lblBmiResult.Text;
            //ErrorChestIE = lblValidateChestIE.Text;
            //ErrorSystolic1 = lblValidateSystolic1.Text;
            //ErrorSystolic2 = lblValidateSystolic2.Text;
            //ErrorSystolic3 = lblValidateSystolic3.Text;
            //ErrorDiastolic1 = lblValidateDiastolic1.Text;
            //ErrorDiastolic2 = lblValidateDiastolic2.Text;
            //ErrorDiastolic3 = lblValidateDiastolic3.Text;
            //ErrorHTNError = lblHTNError.Text;
            //ErrorDMError = lblDMError.Text;
            //ErrorSmokerWarning = lblSmokerWarning.Text;

            //ErrorPCV = lblValidatePCV.Text;
            //ErrorRBC = lblValidateRBC.Text;
            //ErrorMCV = lblValidateMCV.Text;
            //ErrorMCH = lblValidateMCH.Text;
            //ErrorMCHC = lblValidateMCHC.Text;
            //ErrorWBC = lblValidateWBC.Text;
            //ErrorNEUTROPHILS = lblValidateNEUTROPHILS.Text;
            //ErrorLYMPHOCYTES = lblValidateLYMPHOCYTES.Text;
            //ErrorEOSINOPHILS = lblValidateEOSINOPHILS.Text;
            //ErrorMONOCYTES = lblValidateMONOCYTES.Text;
            //ErrorBASOPHILS = lblValidateBASOPHILS.Text;
            //ErrorPLATELETCOUNT = lblValidatePLATELETCOUNT.Text;
            //ErrorESR = lblValidateESR.Text;

            //ErrorHBSAG = lblValidateHBSAG.Text;

            //ErrorHIV = lblValidateHIV.Text;

            //ErrorSGOT = lblValidateSGOT.Text;
            //ErrorSGPT = lblValidateSGPT.Text;
            //ErrorGGT = lblValidateGGT.Text;
            //ErrorBILLIRUBIN = lblValidateBILLIRUBIN.Text;
            //ErrorBILLIRUBIN1 = lblValidateBILLIRUBIN1.Text;
            //ErrorTotalBILLIRUBIN = lblValidateTotalBILLIRUBIN.Text;
            //ErrorALP = lblValidateALP.Text;
            //ErrorS_GLOBILIN = lblValidateS_GLOBILIN.Text;
            //ErrorS_ALBUMIN = lblValidateS_ALBUMIN.Text;
            //ErrorT_PROTEIN = lblValidateT_PROTEIN.Text;
            //ErrorAGRATIO = lblValidateAGRATIO.Text;

            //ErrorCHOLESTEROL = lblValidateCHOLESTEROL.Text;
            //ErrorHDL = lblValidateHDL.Text;
            //ErrorTRIGYCERIDE = lblValidateTRIGYCERIDE.Text;
            //ErrorHDL_RATIO = lblValidateHDL_RATIO.Text;

            //ErrorS_CREATINE = lblValidateS_CREATINE.Text;
            //ErrorURIC_ACID = lblValidateURIC_ACID.Text;
            //ErrorBUN = lblValidateBUN.Text;

            //string AllValidationError = ("ErrorappNO: " + ErrorappNO + "," +
            //                             "ErrorGender: " + ErrorGender + "," +
            //                             "ErrorDOB: " + ErrorDOB + "," +
            //                             "ErrorAge: " + ErrorAge + "," +
            //                             "ErrorBmiResult: " + ErrorBmiResult + "," +
            //                             "ErrorChestIE: " + ErrorChestIE + "," +
            //                             "ErrorSystolic1: " + ErrorSystolic1 + "," +
            //                             "ErrorSystolic2: " + ErrorSystolic2 + "," +
            //                             "ErrorSystolic3: " + ErrorSystolic3 + "," +
            //                             "ErrorDiastolic1: " + ErrorDiastolic1 + "," +
            //                             "ErrorDiastolic2: " + ErrorDiastolic2 + "," +
            //                             "ErrorDiastolic3: " + ErrorDiastolic3 + "," +
            //                             "ErrorHTNError: " + ErrorHTNError + "," +
            //                             "ErrorDMError: " + ErrorDMError + "," +
            //                             "ErrorSmokerWarning: " + ErrorSmokerWarning + "," +
            //                             "ErrorPCV: " + ErrorPCV + "," +
            //                             "ErrorRBC: " + ErrorRBC + "," +
            //                             "ErrorMCV: " + ErrorMCV + "," +
            //                             "ErrorMCH: " + ErrorMCH + "," +
            //                             "ErrorMCHC:" + ErrorMCHC + "," +
            //                             "ErrorWBC: " + ErrorWBC + "," +
            //                             "ErrorNEUTROPHILS: " + ErrorNEUTROPHILS + "," +
            //                             "ErrorLYMPHOCYTES: " + ErrorLYMPHOCYTES + "," +
            //                             "ErrorEOSINOPHILS: " + ErrorEOSINOPHILS + "," +
            //                             "ErrorMONOCYTES: " + ErrorMONOCYTES + "," +
            //                             "ErrorBASOPHILS: " + ErrorBASOPHILS + "," +
            //                             "ErrorPLATELETCOUNT: " + ErrorPLATELETCOUNT + "," +
            //                             "ErrorESR: " + ErrorESR + "," +
            //                             "ErrorHBSAG: " + ErrorHBSAG + "," +
            //                             "ErrorHIV: " + ErrorHIV + "," +
            //                             "ErrorSGOT: " + ErrorSGOT + "," +
            //                             "ErrorSGPT: " + ErrorSGPT + "," +
            //                             "ErrorGGT: " + ErrorGGT + "," +
            //                             "ErrorBILLIRUBIN :" + ErrorBILLIRUBIN + "," +
            //                             "ErrorBILLIRUBIN1: " + ErrorBILLIRUBIN1 + "," +
            //                             "ErrorTotalBILLIRUBIN: " + ErrorTotalBILLIRUBIN + "," +
            //                             "ErrorALP: " + ErrorALP + "," +
            //                             "ErrorS_GLOBILIN: " + ErrorS_GLOBILIN + "," +
            //                             "ErrorS_ALBUMIN: " + ErrorS_ALBUMIN + "," +
            //                             "ErrorT_PROTEIN: " + ErrorT_PROTEIN + "," +
            //                             "ErrorAGRATIO: " + ErrorAGRATIO + "," +
            //                             "ErrorCHOLESTEROL: " + ErrorCHOLESTEROL + "," +
            //                             "ErrorHDL: " + ErrorHDL + "," +
            //                             "ErrorTRIGYCERIDE: " + ErrorTRIGYCERIDE + "," +
            //                             "ErrorS_CREATINE: " + ErrorS_CREATINE + "," +
            //                             "ErrorURIC_ACID: " + ErrorURIC_ACID + "," +
            //                             "ErrorBUN: " + ErrorBUN
            //                             );
            SqlParameter[] _objSqlParam = new SqlParameter[2];

            _objSqlParam[0] = new SqlParameter("@APP_NO", strApplicationno);
            _objSqlParam[1] = new SqlParameter("@ALL_VALIDATION_ERROR", AllValidationError);

            objDal.Insertrecord("USP_MEDICAL_DATA_VALIDATION_MSG", _objSqlParam);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    #endregion
}