using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DMSVeiwerNext : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ApplnNo"]!=null)
        {
            divrw1.Visible = false;
            row2.Visible = true;
            string ApplicationNo = Convert.ToString(Request.QueryString["ApplnNo"]);
            string strGetImage= ConfigurationManager.AppSettings["GETIMAGE"].ToString();
            string strDocKey = ApplicationNo + ConfigurationManager.AppSettings["DOCKEY"].ToString();
            string DocTYP = "Age Proof";
            string DocTYP1 = "Address Proof";
            string DocTYP2 = "Identification Proof";
            string DocTYP3 = "Income Proof";
            string DocTYP4 = "SIS";
            string DocTYP5 = "Photograph";
            string GETIMAGE =  strGetImage+ "=" + ApplicationNo;
            string DocKey = strGetImage + "=" + strDocKey;
            IframeAPP.Src = GETIMAGE;
            IframeAdr.Src = DocKey + DocTYP1;
            IframeAge.Src = DocKey + DocTYP;
            IframeID.Src = DocKey + DocTYP2;
            IframeIncome.Src = DocKey + DocTYP3;
            IframeSIS.Src = DocKey + DocTYP4;
            IframePIC.Src = DocKey + DocTYP5; 
        }        
    }
    protected void Getimg_Click(object sender, EventArgs e)
    {
        




    }
}