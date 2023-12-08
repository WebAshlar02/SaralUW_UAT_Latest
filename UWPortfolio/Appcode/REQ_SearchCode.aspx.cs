using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using System.Web.Configuration;
using System.Web.Caching;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public partial class Appcode_REQ_SearchCode : System.Web.UI.Page
{
    Commfun objComm = new Commfun();
    DataSet _dsRequrmentDtls = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = string.Empty;
        gvSearch.DataSource = null;
        gvSearch.DataBind();
        if (string.IsNullOrEmpty(txtSearch.Text))
        {
            lblMsg.Text = "Please enter text to search";
            return;
        }
        else
        {
            string strFLP_CODE_DESC = Convert.ToString(txtSearch.Text.Trim());
            objComm.GetReqDetails(ref _dsRequrmentDtls, strFLP_CODE_DESC);
            if (_dsRequrmentDtls.Tables.Count > 0 && _dsRequrmentDtls.Tables[0].Rows.Count > 0)
            {
                gvSearch.DataSource = _dsRequrmentDtls.Tables[0];
                gvSearch.DataBind();
            }
            else
            {
                lblMsg.Text = "No Records Found";
            }
        }
    }
}