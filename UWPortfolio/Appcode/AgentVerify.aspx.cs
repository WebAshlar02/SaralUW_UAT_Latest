using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UWSaralServices;
using System.Configuration;

public partial class Appcode_AgentVerify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strApplnNo = "TNA0019402", strAgentCode = "80000189", strAgentType = string.Empty, strAgentApplnNo = string.Empty, strAgentSignaturePath = string.Empty, strApplnPath = string.Empty;
        if (Request.QueryString["ApplnNo"] != null && Request.QueryString["AgentCode"] != null)
        {
            strApplnNo = Convert.ToString(Request.QueryString["ApplnNo"]);
            strAgentApplnNo = strAgentCode = Convert.ToString(Request.QueryString["AgentCode"]);
        }
        AgentEnquiry objAgentEnquiry = new AgentEnquiry();
        objAgentEnquiry.GetAgentDetails(strApplnNo, strAgentCode, ref strAgentType);
        if (strAgentType.ToUpper() == "CA" || strAgentType.ToUpper() == "CT")
        {
            objAgentEnquiry.GetAgentApplicationNumber(strApplnNo, strAgentCode, ref strAgentApplnNo);

        }
        strAgentSignaturePath = Convert.ToString(ConfigurationManager.AppSettings["AgentSignPath"]);
        strApplnPath = Convert.ToString(ConfigurationManager.AppSettings["ApplicationFilePath"]);
        ((HtmlControl)(form12.FindControl("ifrm"))).Attributes["src"] = strApplnPath + strApplnNo + "&DC.Document_Type=Application Form";
        ((HtmlControl)(form13.FindControl("ifrm1"))).Attributes["src"] = strAgentSignaturePath + strAgentCode + "&DC.Document_Type=Signature";
        btnSubmit.Focus();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Commfun objCommfun = new Commfun();
            string strresults = "";
            string strAgentAppNo = string.Empty, strAppNo = string.Empty, strAgentCode = string.Empty, strAgentType = string.Empty, strBpmUserName = string.Empty,
                            strUserId = string.Empty, strBpmBranchCode = string.Empty, strUserBranch = string.Empty;
            int intRetVal = -1;
            if (Session["objCommonObj"] != null)
            {
                CommonObject objCommonObj = (CommonObject)Session["objCommonObj"];
                UserInfo objuser = objCommonObj._Bpmuserdetails;
                strBpmUserName = objuser._UserName;
                strUserId = objuser._UserID;
                strBpmBranchCode = objuser._userBranch;
            }

            objCommfun.SaveAgentVerificationDetails(strAgentAppNo, strAppNo, strAgentCode, strAgentType,
                                                                     "UW Saral", rdoAgentSign.SelectedValue, strBpmUserName, strUserId, strBpmBranchCode, strUserBranch, ref intRetVal);

            ifrm.Attributes["src"] = "about:blank";
            ifrm.Attributes["innerHTML"] = string.Empty;
            ifrm1.Attributes["src"] = "about:blank";
            ifrm1.Attributes["innerHTML"] = string.Empty;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    //production service :
    //http://10.1.41.48:9084/interfacewebservices/services/AgentValidationService?wsdl 
}