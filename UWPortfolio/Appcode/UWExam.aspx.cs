#region "Declerations"

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Platform.Utilities.LoggerFramework;
using UWSaralObjects;
using System.Xml;

#endregion

public partial class Appcode_UWExam : System.Web.UI.Page
{
    #region "Common Object"

    string strCon = ConfigurationSettings.AppSettings["transactiondbLF"];
    CommonObject objCommonObj = new CommonObject();
    Commfun objcomm = new Commfun();
    DataSet _dsuserInfo = new DataSet();
    DataLayer objDAL = new DataLayer();
    #endregion

    #region "Events"

    #region "page load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SendValue();
        }
    }
    #endregion

    #region "Exam Submit Button"
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string userIDRedirection = Session["userID"].ToString();
        try
        {
            var UserId = Session["UserId"].ToString();
            UserId = UserId.Substring(1);
            var AnswerValue = rbtnAnsOpt.SelectedItem.Value;
            var AnswerText = rbtnAnsOpt.SelectedItem.Text;
            var CurrentQid = ViewState["currentQId"];

            SqlParameter[] objSqlParam = new SqlParameter[3];
            objSqlParam[0] = new SqlParameter("@UWName", UserId);
            objSqlParam[1] = new SqlParameter("@QUESTION_ID", CurrentQid);
            objSqlParam[2] = new SqlParameter ("@ANSWER_OPT_VALUE", AnswerValue);

            //objDAL.Insertrecord("USP_INSERT_INTO_TBL_TRANS", objSqlParam);
            objDAL.Insertrecord("USP_INSERT_INTO_TBL_TRANS_UW", objSqlParam);
            Validate(userIDRedirection);
            //Response.Redirect("Uwdecision.aspx");
        }
        catch (Exception ex)
        {
            var abc = ex.Message;
        }
    }
    #endregion

    #region "Close Button"
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string userIDRedirection = Session["userID"].ToString();
        Validate(userIDRedirection);
    }
    #endregion

    #endregion

    #region "Functions"

    #region "Assign Que To User"
    public void SendValue()
    {
        string userId = Convert.ToString(Session["UserId"]);
        string DBUsrID = userId.Substring(1);
        try
        {
            btnClose.Visible = false;
            DataSet dsQue = new DataSet();
            SqlParameter[] _objSqlParam = new SqlParameter[1];
            _objSqlParam[0] = new SqlParameter("@UWName", DBUsrID);
            //dsQue = objDAL.RetrieveDataset("USP_ASSIGN_QUE_TO_USERS", _objSqlParam);
            //dsQue = objDAL.RetrieveDataset("USP_ASSIGN_QUE_TO_UW_USERS", _objSqlParam);
            dsQue = objDAL.RetrieveDataset("USP_ASSIGN_QUE_TO_UW_USERS_V1", _objSqlParam);

            if (dsQue.Tables.Count > 0)
            {
                lblQue.Text = dsQue.Tables[0].Rows[0][3].ToString();

                rbtnAnsOpt.DataSource = dsQue;
                rbtnAnsOpt.DataValueField = "ANS_OPT_ID";
                rbtnAnsOpt.DataTextField = "ANS_OPT_DESC";
                rbtnAnsOpt.DataBind();
            }
            var currentQId = dsQue.Tables[0].Rows[0][2];
            ViewState["currentQId"] = currentQId;

        }
        catch (Exception ex)
        {
            if (ex != null)
            {
                Validate(userId);
                //Response.Redirect("Default3.aspx");
                //Response.Redirect("Uwdecision.aspx");
            }

        }
        finally
        {
            
        }

    }

    #endregion


    #region "validate - Redirect on Home page"

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
                Logger.Info("****************User validation Success**************" + System.Environment.NewLine);
                Response.Redirect("~/Default3.aspx?Userid=" + strUserName);
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

    #endregion

    #endregion
}