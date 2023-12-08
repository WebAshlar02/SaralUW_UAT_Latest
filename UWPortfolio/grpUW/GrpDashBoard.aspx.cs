using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Platform.Utilities.LoggerFramework;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using UWSaralObjects;
using UWSaralDecision;

public partial class grpUW_GrpDashBoard : System.Web.UI.Page
{
    #region "Declaration"
    string strDBCS = ConfigurationManager.ConnectionStrings["groupFG"].ConnectionString;
    SqlConnection sqlCon = null;
    SqlCommand sqlCmd = null;
    SqlDataReader sqlDR = null;
    UWSaralDecision.BOUWDashBoard objBOUWDB = new UWSaralDecision.BOUWDashBoard();
    CommonObject objCommonObj = new CommonObject();
    DataSet _ds = new DataSet();
    BussLayer ObjBuss = new BussLayer();
    string strUserId = string.Empty;
    string strUWmode = string.Empty;
    string strUserGroup = string.Empty;
    string strApplicationno = string.Empty;
    string strOptinselected = string.Empty;
    string strAppstatusKey = string.Empty;
    public string strPolicyEnquiery = string.Empty;
    int Result = 0;
    DataSet _dsDashcount = null;
    Commfun objComm = new Commfun();
    Boolean IsPageRefresh;
    int intUserKey = 0;
    #endregion


    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["objLoginObj"] == null)
        {
            Response.Redirect("~/LoginPage.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Ajax.Utility.RegisterTypeForAjax(typeof(grpUW_GrpDashBoard), Page);

        if (!IsPostBack)
        {
            objCommonObj = (CommonObject)Session["objCommonObj"];
            spanusername.InnerText = objCommonObj._Bpmuserdetails._UserName;
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            intUserKey = objBOUWDB.GetUserKey(strUserId);
            hdnUWUserKey.Value = intUserKey.ToString();
        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }


    #region "Get list DataFacts Info using Ajax"
    [Ajax.AjaxMethod]
    public string getDashBoard(int intUWUserKey)
    {
        List<DashBoardDataFacts> listDataFacts = new List<DashBoardDataFacts>();
        listDataFacts = objBOUWDB.GetDataFacts(intUWUserKey);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(listDataFacts);
    }
    #endregion

    #region "Get Member Info using Ajax"
    [Ajax.AjaxMethod]
    public string getMemInfo(Int16 ID, int intUWUserKey)
    {
        List<MemberInfo> MemsList = new List<MemberInfo>();
        MemsList = objBOUWDB.GetGridMemsData(ID, intUWUserKey);
        JavaScriptSerializer js = new JavaScriptSerializer();
        return js.Serialize(MemsList);
    }
    #endregion

    protected void lnkOnline_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("/UWPortfolio/DashboardOnline.aspx", false);
            objCommonObj = (CommonObject)Session["objCommonObj"];
            objCommonObj._ChannelType = lnkOnline.Text;
            Session["objCommonObj"] = objCommonObj;
            strOptinselected = "FRESH";
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            strUWmode = "ONLINE";
            ////GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCountAll_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    protected void lnkoffline_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("/UWPortfolio/Default3.aspx", false);
            objCommonObj = (CommonObject)Session["objCommonObj"];
            objCommonObj._ChannelType = "offline";
            Session["objCommonObj"] = objCommonObj;
            strOptinselected = "FRESH";
            strUserId = objCommonObj._Bpmuserdetails._UserID;
            strUserGroup = objCommonObj._Bpmuserdetails._UserGroup;
            strUWmode = "OFFLINE";
            //GetDashbordDetails(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCount_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
            //DashbordBucketsCountAll_Get(strUserId, strUserGroup, strOptinselected, strUWmode);
        }
        catch (Exception EX)
        {

        }
        ScriptManager.RegisterStartupScript(Page, GetType(), "disp_confirm", "<script>hideloading()</script>", false);
    }

    protected void lnkGrp_Click(object sender, EventArgs e)
    {
        Response.Redirect("/UWPortfolio/grpUW/GrpDashBoard.aspx", false);
    }


}
