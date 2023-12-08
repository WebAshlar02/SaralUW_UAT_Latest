using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Web.Security;
using System.Globalization;

public partial class Entry : System.Web.UI.Page
{
  
    CommonObject objCommonObj = new CommonObject();
    Commfun objcomm = new Commfun();
    DataSet _dsuserInfo = new DataSet();
    DataLayer objDal = new DataLayer();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Logger.Info("**************** New Journey Begins At " + DateTime.Now + "***************" + System.Environment.NewLine);
            Logger.Info("****************Login Page begin**************" + System.Environment.NewLine);

            #region user Authentication begin.
            //string strUserName = System.Environment.UserName;
            //Logger.Info("Login by " + strUserName + System.Environment.NewLine);
            //Validate(strUserName);
            //Response.Redirect("~/Default.aspx?Userid=" + strUserName);		
            #endregion user Authentication end.
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //ExamAuthentication(txtUserid.Text);
        Validate(txtUserid.Text);

    }

    private void Validate(string strUserName)
    {
        if (!string.IsNullOrEmpty(strUserName))
        {
            Logger.Info("****************User validation Begins**************" + System.Environment.NewLine);
            objcomm.BpmUserDetails_Get(strUserName, ref _dsuserInfo);
            if (_dsuserInfo != null && _dsuserInfo.Tables[0].Rows.Count > 0)
            {
                LoginUserDetails objLogin = new LoginUserDetails();

                UserInfo objuser = new UserInfo();
                objuser._UserGroup = _dsuserInfo.Tables[0].Rows[0]["USERGROUP"].ToString();
                objuser._UserID = _dsuserInfo.Tables[0].Rows[0]["USERID"].ToString();
                objuser._UserName = _dsuserInfo.Tables[0].Rows[0]["USERNAME"].ToString();
                objuser._userBranch = _dsuserInfo.Tables[0].Rows[0]["BRANCH"].ToString();
                objuser._MinSumassured = _dsuserInfo.Tables[0].Rows[0]["MINSUMASSURED"].ToString();
                objuser._MaxSumassured = _dsuserInfo.Tables[0].Rows[0]["MAXSUMASSURED"].ToString();
                objuser._UserMessage = _dsuserInfo.Tables[0].Rows[0]["USERMESSAGE"].ToString();
                objCommonObj._Bpmuserdetails = objuser;

                objLogin._UserGroup = _dsuserInfo.Tables[0].Rows[0]["USERGROUP"].ToString();
                objLogin._UserID = _dsuserInfo.Tables[0].Rows[0]["USERID"].ToString();
                objLogin._UserName = _dsuserInfo.Tables[0].Rows[0]["USERNAME"].ToString();
                objLogin._userBranch = _dsuserInfo.Tables[0].Rows[0]["BRANCH"].ToString();
                objLogin._MinSumassured = _dsuserInfo.Tables[0].Rows[0]["MINSUMASSURED"].ToString();
                objLogin._MaxSumassured = _dsuserInfo.Tables[0].Rows[0]["MAXSUMASSURED"].ToString();
                objLogin._UserMessage = _dsuserInfo.Tables[0].Rows[0]["USERMESSAGE"].ToString();
                objLogin._ProcessName = "UWSaral";
                Session["objCommonObj"] = objCommonObj;

                ChangeValue objChangeobject = new ChangeValue();
                objChangeobject.userLoginDetails = objLogin;
                Session["objLoginObj"] = objChangeobject;
               // int result = 0;
                //CommAssD.Underwriting_AssignmentDetails_LOGINLOGOUT_Log(objLogin._UserID, objLogin._UserName, objLogin._UserGroup, "Login", "Insert_Login_Logout_Logs", ref result);
                //if (result > 0)
                //{
                    Logger.Info("****************User validation Success**************" + System.Environment.NewLine);
                    Response.Redirect("~/Default3.aspx?Userid=" + strUserName, true);

               // }
            }
            else
            {
                lblError.Text = "Please Enter valid user id ";
                Logger.Info("****************Invalid user found**************" + System.Environment.NewLine);
            }
        }
        else
        {
            lblError.Text = "Please Enter user id ";
            Logger.Info("****************No user found**************" + System.Environment.NewLine);
        }

    }

    //Added by Rohit Shirke -- validation for assign exams que to user
    #region "Assign Exams Que by UserID"
    protected void ExamAuthentication(string strUserName)
    {
        int status;
        //SqlConnection con = new SqlConnection(strCon);
        //con.Open();
        //SqlCommand cmd = new SqlCommand("USP_USR_EXAM_LOGIN", con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@USR_ID", txtUserid.Text);
        string UserId = txtUserid.Text;
        UserId = UserId.Substring(1);
        SqlParameter[] _objSqlParam = new SqlParameter[1];
        _objSqlParam[0] = new SqlParameter("@UWName", UserId);

        //ds = objDal.RetrieveDataset("USP_USR_EXAM_LOGIN", _objSqlParam);
        ds = objDal.RetrieveDataset("USP_UW_USR_EXAM_LOGIN", _objSqlParam);
        //cmd.Parameters.AddWithValue("@STATUS", DbType.Int16);

        Session["UserId"] = txtUserid.Text;

        //status = Convert.ToInt16(cmd.ExecuteScalar());
        status = Convert.ToInt16(ds.Tables[0].Rows[0][0].ToString());

        if (status == 1)
        {
            Response.Redirect("Appcode/UWExam.aspx");
        }
        else
        {
            lblExamError.Text = "Incorrect UserId";
        }
        //con.Close();
    }
    #endregion
}