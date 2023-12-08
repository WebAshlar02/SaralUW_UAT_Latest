using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DmsVeiwer : System.Web.UI.Page
{
    string Appno = string.Empty;
    string DocType = string.Empty;
    string DocTYP1 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ApplnNo"] != null)
        {
            Appno = Convert.ToString(Request.QueryString["ApplnNo"]);

        }
        if (Request.QueryString["DocType"] != null)
        {
            DocType = Convert.ToString(Request.QueryString["DocType"]);
        }
        FetchData(Appno, DocType);
    }
    protected void Getimg_Click(object sender, EventArgs e)
    {
        FetchData(TxtAppNo.Text, "");
    }

    private void FetchData(string strApplicationNo, string Doctype)
    {
        divrw1.Visible = false;
        row2.Visible = true;
        string ApplicationNo = "H00043957";//strApplicationNo;//"H00043957";
        // string DocTYP = "Age Proof";
        if (!string.IsNullOrEmpty(Doctype))
        {
            DocTYP1 = "Medical Requirement";
        }
        else
        {
            DocTYP1 = "Application Form";
        }

        //string DocTYP2 = "Identification Proof";
        // string DocTYP3 = "Income Proof";
        //string DocTYP4 = "SIS";
        //string DocTYP5 = "Photograph";
        string GETIMAGE = ConfigurationManager.AppSettings["GETIMAGE"].ToString() + "=" + ApplicationNo;
        string DocKey = ConfigurationManager.AppSettings["GETIMAGE"].ToString() + "=" + ApplicationNo + ConfigurationManager.AppSettings["DOCKEY"].ToString();
        if (!string.IsNullOrEmpty(Doctype))
        {
            IframeAdr.Src = DocKey + DocTYP1;
        }
        else
        {
            IframeAPP.Src = GETIMAGE;
            IframeAdr.Src = DocKey + DocTYP1;
        }
        //IframeAge.Src = DocKey + DocTYP;
        //IframeID.Src = DocKey + DocTYP2;
        //IframeIncome.Src = DocKey + DocTYP3;
        // IframeSIS.Src = DocKey + DocTYP4;
        //IframePIC.Src = DocKey + DocTYP5; 
    }
}