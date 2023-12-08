using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UWSaralObjects;

public partial class BPMAccess : System.Web.UI.Page
{
    SqlConnection strconLomb = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["cnLombardimatersync"].ToString());
    SqlConnection strconTrans = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["transactiondbLFBPMAcess"].ToString());
    SqlConnection strconLBPMDBLF = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["CON_LBPMDBLF"].ToString());

    //transactiondbLF


    SqlCommand cmdCD = new SqlCommand();
    DataSet dsCD = new DataSet();
    SqlDataAdapter daCD = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["TaskId"] != null)
                {
                    String taskid = Request.QueryString["TaskId"];//"128227";
                    fetchConfigDetails(taskid);
                }
                else
                {
                    string taskid = "E388A4F6-229A-43CF-AF94-5E26CA10721E";
                    fetchConfigDetails(taskid);
                    //   String taskid = string.Empty;
                    //   Response.Redirect("Error500.aspx");
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }


    }
    public void fetchConfigDetails(String TaskID)
    {
        try
        {
            cmdCD.Connection = strconLBPMDBLF;
            strconLBPMDBLF.Open();
            cmdCD.CommandType = CommandType.StoredProcedure;
            cmdCD.CommandTimeout = 0;
            cmdCD.CommandText = "usp_TaskID_Get_NB";
            cmdCD.Parameters.AddWithValue("@taskid", TaskID);
            daCD.SelectCommand = cmdCD;
            daCD.Fill(dsCD, "Config");
            //cmdCD.ExecuteNonQuery();// why execute non query??
            strconLBPMDBLF.Close();
            cmdCD.Parameters.Clear();
            if (dsCD.Tables[0].Rows.Count > 0)
            {
                //declaration
                UserInfo objUserInfo = new UserInfo();
                CommanObj objCommonObj = new CommanObj();
                LoginUserDetails objLogin = new LoginUserDetails();
                ChangeValue objChangeobject = new ChangeValue();
                //user info && login info                                
                objLogin._UserID = objUserInfo._UserID = "F" + dsCD.Tables["Config"].Rows[0]["bpm_userID"].ToString();
                objLogin._UserGroup = objUserInfo._UserGroup = dsCD.Tables["Config"].Rows[0]["userGrp"].ToString();
                objLogin._UserName = objUserInfo._UserName = dsCD.Tables["Config"].Rows[0]["bpm_userName"].ToString();
                objLogin._userBranch = objUserInfo._userBranch = dsCD.Tables["Config"].Rows[0]["bpm_branchCode"].ToString();
                objLogin._ProcessName = "UWSaral";

                objCommonObj._Bpmuserdetails = objUserInfo;


                //session filling part 
                Session["objCommonObj"] = objCommonObj;
                objChangeobject.userLoginDetails = objLogin;
                Session["objLoginObj"] = objChangeobject;

                //redirect to new page
                Response.Redirect("~/Default.aspx?Userid=" + objLogin._UserID);


               

                DataSet UserAccess = new DataSet();
                UserAccess = GetUserAccess(Session["USERGRP"].ToString(), Session["MENUNAME"].ToString());

                if (UserAccess.Tables[0].Rows.Count > 0)
                {

                    String pagename = UserAccess.Tables[0].Rows[0]["PageName"].ToString().Trim();
                    Response.Redirect("~/Default.aspx",false);
                    //String usrrole = UserAccess.Tables[0].Rows[0]["UserRole"].ToString().Trim();                    
                    //String MenuName = UserAccess.Tables[0].Rows[0]["MenuName"].ToString().Trim();



                    //if (usrrole == userGrp && MenuName == Menu)
                    //{

                    //  pagename = pagename.Replace("http://service.fglife.in/FG.LF.POSBOE/", "http://localhost:52848/");

                    //Response.Redirect(pagename);                    
                    // Response.Redirect("http://10.1.41.221/FG.LF.POSBOE/Application/PayOutWorkbench.aspx");

                    //}
                    //else if (userGrp == "POS")
                    //{
                    //    Response.Redirect(pagename);

                    //}
                    //else
                    //{
                    //    Response.Redirect("Oops.aspx");
                    //}

                }
                else
                {
                    Response.Redirect("Error500.aspx");
                }
            }
        }
        catch (SqlException EX)
        {
            EX.ToString();
        }

    }

    public void updateTaskId(String TaskID, String UpdatedBy)
    {
        try
        {
            cmdCD.Connection = strconLBPMDBLF;
            strconLBPMDBLF.Open();
            cmdCD.CommandType = CommandType.StoredProcedure;
            cmdCD.CommandTimeout = 0;
            cmdCD.CommandText = "usp_TaskID_Upd_NB";
            cmdCD.Parameters.AddWithValue("@taskid", TaskID);
            //cmdCD.Parameters.AddWithValue("@updatedBY", UpdatedBy);
            daCD.SelectCommand = cmdCD;
            daCD.Fill(dsCD, "UpdTask");
            cmdCD.ExecuteNonQuery();
            strconLBPMDBLF.Close();
            cmdCD.Parameters.Clear();

        }
        catch (SqlException EX)
        {
            Response.Redirect(@"http://10.8.41.39/SaralRisk/Default.aspx");
        }
    }

    public DataSet GetUserAccess(String UserRole, String Menu)
    {
        DataSet dsUserRole = new DataSet();
        cmdCD.Connection = strconLomb;
        strconLomb.Open();
        cmdCD.CommandType = CommandType.StoredProcedure;
        cmdCD.CommandTimeout = 30000;
        //for local debug to UAT
        cmdCD.CommandText = "usp_UserAccess_Get_NB";
        cmdCD.Parameters.AddWithValue("@UserRole", UserRole);
        cmdCD.Parameters.AddWithValue("@Menu", Menu);
        daCD.SelectCommand = cmdCD;
        daCD.Fill(dsUserRole, "UserAccess");
        strconLomb.Close();
        cmdCD.Parameters.Clear();
        return dsUserRole;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void cmdGetTaskID_Click(object sender, EventArgs e)
    {
        fetchConfigDetails(txtTaskID.Text.ToString());
    }
}