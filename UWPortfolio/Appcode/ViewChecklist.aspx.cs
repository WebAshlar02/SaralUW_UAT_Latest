using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_ViewChecklist : System.Web.UI.Page
{
    Commfun objCommFun = new Commfun();
    BussLayer objBussLayer = new BussLayer();
    DataSet _ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (!Page.IsPostBack)
            {
               if(Request.QueryString["AppNo"].ToString()!="")
                {
                    string AppNo = Request.QueryString["AppNo"].ToString();
                    objBussLayer.FetchCheckListReportData(AppNo,ref ds);
                }

                if (ds.Tables.Count > 0)
                {
                    dgViewChecklist.DataSource = ds.Tables[0];
                    dgViewChecklist.DataBind();
                }
                
            }
        }
        catch (Exception ex)
        {

            //lblUWStatus.Text = "Try Again Later";
        }

        
    }
}