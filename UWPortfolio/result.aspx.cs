using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strpath = string.Empty;
        //if (Request.QueryString["path"] != null)
        //{
            string path = "D:\\TFS\\FGUniconnect\\SaralUW\\WebApplication\\BPMDesign\\UWPortfolio\\Video\\D00358130_1.webm";
            Page.Controls.Add(new LiteralControl("<video width='320' height='240' controls='controls'><source src=" + path + " type='video/avi'></video>"));
            ////strApplicationno = UWSaralDecision.CommFun.Base64DecodingMethod(Request.QueryString["qsAppNo"]);
            //strpath = Request.QueryString["path"];
            //HtmlGenericControl control = new HtmlGenericControl("source");
            //control.Attributes.Add("src", strpath);
        //}
    }
}