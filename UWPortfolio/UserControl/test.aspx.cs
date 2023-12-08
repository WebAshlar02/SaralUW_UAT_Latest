using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UWSaralObjects;
using UWSaralServices;
using UWSaralDecision;
using System.Data;
public partial class UserControl_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UWSaralDecision.BussLayer objUWDecision = new UWSaralDecision.BussLayer();
        ChangeValue objChangeObj = new ChangeValue();
        string strApplicationno = "F10234223", strChannelType = "online"; DataSet _ds = new DataSet(), _dsPrevPol = new DataSet();

        string strLApushStatus = string.Empty, strConsentRespons = string.Empty;
        int strLApushErrorCode = 0;
        ClientDetails objClientDetils = new ClientDetails();
        objClientDetils.Aadhar = "628149230366";
        objChangeObj.ClientDetails=objClientDetils;
        objUWDecision.OnlineApplicationLAServiceDetails_PUSH(strApplicationno, strChannelType, objChangeObj, ref _ds, ref _dsPrevPol, "AADHAR_VERIFICATION", ref strLApushErrorCode, ref strLApushStatus, ref strConsentRespons);
    }
}