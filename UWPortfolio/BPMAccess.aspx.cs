using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Reflection;
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
    DataLayer objDal = new DataLayer();
    DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
            if (Request.QueryString["TaskId"] != null)
            {
                string strPreviousPage = "";
                if (Request.CurrentExecutionFilePath != null)
                {
                    strPreviousPage = Request.CurrentExecutionFilePath.ToString();
                    Session["PreviousPage"] = strPreviousPage;
                }
                String taskid = Request.QueryString["TaskId"];//"128227";
                Session["TaskId"] = taskid;
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
                CommonObject objCommonObj = new CommonObject();
                LoginUserDetails objLogin = new LoginUserDetails();
                ChangeValue objChangeobject = new ChangeValue();
                //user info && login info                                
                objLogin._UserID = objUserInfo._UserID = "F"+dsCD.Tables["Config"].Rows[0]["bpm_userID"].ToString();
                objLogin._UserGroup = objUserInfo._UserGroup = dsCD.Tables["Config"].Rows[0]["userGrp"].ToString();
                objLogin._UserName = objUserInfo._UserName = dsCD.Tables["Config"].Rows[0]["bpm_userName"].ToString();
                objLogin._userBranch = objUserInfo._userBranch = dsCD.Tables["Config"].Rows[0]["bpm_branchCode"].ToString();

                //Start Pragati backdating
                objLogin._BusinessDate = Convert.ToDateTime(dsCD.Tables["Config"].Rows[0]["bpm_businessDate"].ToString());
                //End Pragati backdating

                objLogin._ProcessName = "UWSaral";                
                objCommonObj._Bpmuserdetails = objUserInfo;


                //session filling part 
                Session["objCommonObj"] = objCommonObj;
                objChangeobject.userLoginDetails = objLogin;
                Session["objLoginObj"] = objChangeobject;
                //added by roshit for display Knowledge boosting question
                ExamAuthentication(objLogin._UserID);
                //redirect to new page
                if (Session["objLoginObj"] != null)
                {
                    Response.Redirect("~/Default3.aspx?Userid=" + objLogin._UserID);
                }
                else
                {
                    Response.Redirect("~/9011042143.aspx");
                }

                //Response.Redirect("~/Appcode/Default.aspx?Userid=" + objLogin._UserID);

                //Session["BPMBRANCHNAME"] = dsCD.Tables["Config"].Rows[0]["BranchName"].ToString();


                //Session["LIFEASIAUSERID"] = dsCD.Tables["Config"].Rows[0]["lifeAsia_UserID"].ToString();
                //Session["MENUNAME"] = dsCD.Tables["Config"].Rows[0]["MenuName"].ToString();
                //Session["BUSSINESSDATE"] = dsCD.Tables["Config"].Rows[0]["bpm_businessDate"].ToString();
                //Session["BUSSINESSDATE"] = Session["BUSSINESSDATE"].ToString().Substring(0, 10);

                //Session["BPMUSERBRANCH"] = dsCD.Tables["Config"].Rows[0]["bpm_userBranch"].ToString();

                //Session["CARRIERCODE"] = 2;
                //Session["USERROLE"] = 10;

                //Session.Add("VALIDATE", "validate");


                //Session["USERID"] = dsCD.Tables["Config"].Rows[0]["bpm_userID"].ToString();
                //Session["USERGRP"] = dsCD.Tables["Config"].Rows[0]["userGrp"].ToString();
                //Session["BPMUSERNAME"] = dsCD.Tables["Config"].Rows[0]["bpm_userName"].ToString();               
                //Session["BPMBRANCHCODE"] = dsCD.Tables["Config"].Rows[0]["bpm_branchCode"].ToString();

                //Required below Session value to Input for Life Asia BO
                //Session["ISACTIVE"] = IsActive;
                //Session["USERGRP"] = userGrp;
                //Session["BPMUSERNAME"] = bpmUserName;
                //Session["USERID"] = UserId;
                //Session["BPMBRANCHCODE"] = bpmBranchCode;
                //Session["LIFEASIAUSERID"] = lifeAsiaUserId;
                //Session["MENUNAME"] = Menu;

                // updateTaskId(TaskID, Session["USERID"].ToString());

                DataSet UserAccess = new DataSet();
                UserAccess = GetUserAccess(Session["USERGRP"].ToString(), Session["MENUNAME"].ToString());

                if (UserAccess.Tables[0].Rows.Count > 0)
                {

                    String pagename = UserAccess.Tables[0].Rows[0]["PageName"].ToString().Trim();
                    Response.Redirect("~/Default.aspx");
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
            EX.ToString();
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

    //Added by Rohit Shirke -- validation for assign exams que to user
    #region "Assign Exams Que by UserID"
    protected void ExamAuthentication(string strUserId)
    {
        int status;
        //SqlConnection con = new SqlConnection(strCon);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("USP_USR_EXAM_LOGIN", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@USR_ID", txtUserid.Text);
        string UserId = strUserId;
        UserId = UserId.Substring(1);
        SqlParameter[] _objSqlParam = new SqlParameter[1];
        _objSqlParam[0] = new SqlParameter("@UWName", UserId);

        //ds = objDal.RetrieveDataset("USP_USR_EXAM_LOGIN", _objSqlParam);
        ds = objDal.RetrieveDataset("USP_UW_USR_EXAM_LOGIN", _objSqlParam);
        //cmd.Parameters.AddWithValue("@STATUS", DbType.Int16);

        Session["UserId"] = strUserId;

        //status = Convert.ToInt16(cmd.ExecuteScalar());
        status = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());

        if (status == 1)
        {
            Response.Redirect("Appcode/UWExam.aspx");
        }
        else
        {
            //lblExamError.Text = "Incorrect UserId";
        }
        //con.Close();
    }
    #endregion

    protected void cmdGetTaskID_Click(object sender, EventArgs e)
    {
        fetchConfigDetails(txtTaskID.Text.ToString());
    }
}