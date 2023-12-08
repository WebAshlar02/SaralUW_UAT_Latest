using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Xml;

public partial class Appcode_MedicalDataEntryNew : System.Web.UI.Page
{
    string strApplicationno = string.Empty;
    string strPolicyNo = string.Empty;
    public static string connection = ConfigurationManager.AppSettings["transactiondbLF"];

    protected void Page_Load(object sender, EventArgs e)
    {
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

    public void BindDataToControl()
    {
        StringReader stream = null;
        XmlTextReader reader = null;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from TBL_MEDICAL_DATA where APP_NO = 'FG00001'", con);
        
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string xmlData = dt.Rows[0]["REQUEST_XML"].ToString();
        stream = new StringReader(xmlData);
        reader = new XmlTextReader(stream);
        
        //txtAGE.Text = xmlData 
        ds.ReadXml(reader);
        //dt.ReadXml(reader);
        txtAppno.Text = strApplicationno;//ds.Tables[0].Rows[0][0].ToString();
        txtGender.Text = ds.Tables[1].Rows[0][0].ToString();
        txtDOB.Text = ds.Tables[1].Rows[0][1].ToString();
        txtAGE.Text = ds.Tables[1].Rows[0][2].ToString();
        txtHeight.Text = ds.Tables[1].Rows[0][3].ToString();
        txtWeight.Text = ds.Tables[1].Rows[0][4].ToString();
        txtBMI.Text = ds.Tables[1].Rows[0][5].ToString();
        txtPulse.Text = ds.Tables[1].Rows[0][6].ToString();
        txtChestInspiration.Text = ds.Tables[1].Rows[0][7].ToString();
        txtChestExpiration.Text = ds.Tables[1].Rows[0][8].ToString();
        txtSystolic.Text = ds.Tables[1].Rows[0][9].ToString();
        txtDiastolic.Text = ds.Tables[1].Rows[0][10].ToString();
        txtGirth.Text = ds.Tables[1].Rows[0][11].ToString();
        
        //chkHTNCase.Checked 
        //chkDMCase
        //chkSmoker
        //chkAlcohol

        #region"CBC&ESR"
        txtHB.Text = ds.Tables[2].Rows[0][0].ToString();
        txtPCV.Text = ds.Tables[2].Rows[0][1].ToString();
        txtRBC.Text = ds.Tables[2].Rows[0][2].ToString();
        txtMCV.Text = ds.Tables[2].Rows[0][3].ToString();
        txtMCH.Text = ds.Tables[2].Rows[0][4].ToString();
        txtMCHC.Text = ds.Tables[2].Rows[0][5].ToString();
        txtWBC.Text = ds.Tables[2].Rows[0][6].ToString();
        txtNEUTROPHILS.Text = ds.Tables[2].Rows[0][7].ToString();
        txtLYMPHOCYTES.Text = ds.Tables[2].Rows[0][8].ToString();
        txtEOSINOPHILS.Text = ds.Tables[2].Rows[0][9].ToString();
        txtMONOCYTES.Text = ds.Tables[2].Rows[0][10].ToString();
        txtBASOPHILS.Text = ds.Tables[2].Rows[0][11].ToString();
        txtPLATELETCOUNT.Text = ds.Tables[2].Rows[0][12].ToString();
        txtESR.Text = ds.Tables[2].Rows[0][13].ToString();
        #endregion

        //#region "HBSAG"
        //    if()
        //    {

        //    }
        //#endregion

        #region "HIV"
            //if()
            //{

            //}
        #endregion

        #region "FBS, RUA"
        txtFBS.Text = ds.Tables[3].Rows[0][0].ToString();
        txtHBA1C.Text = ds.Tables[3].Rows[0][1].ToString();
        txtRUA.Text = ds.Tables[3].Rows[0][2].ToString();
        
        #endregion
        
        
        
        
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
    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    string apiUrl = "http://localhost:22503/api/MRF/AddMrfData";
    //    object input = new
    //    {
    //        txtAppno.Text
    //    };
    //}
}