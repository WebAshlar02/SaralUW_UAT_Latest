using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;


public partial class UserControl_UWExamQue : System.Web.UI.UserControl
{
    #region "Common Objects"

    private SqlConnection con;
    DataSet dsQue = new DataSet();
    DataLayer objDAL = new DataLayer();

    #endregion


    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnQueUpload_Click(object sender, EventArgs e)
    {
        string ExcelPath = Server.MapPath(Path.GetFileName(QueFileUploadExcel.PostedFile.FileName));
        QueFileUploadExcel.SaveAs(ExcelPath);

        dsQue = null;
        dsQue = ConvertExcelToDataTable(ExcelPath);

        foreach (DataRow dr in dsQue.Tables[0].Rows)
        {
            var queDesc = dr["QUEST_DISCP"].ToString();
            var ansOpt1 = dr["ANS_OPT_DESC"].ToString();
            var ansOpt2 = dr["ANS_OPT_DESC1"].ToString();
            var ansOpt3 = dr["ANS_OPT_DESC2"].ToString();
            var ansOpt4 = dr["ANS_OPT_DESC3"].ToString();
            var grpId = dr["GRP_ID"].ToString();

            SqlParameter[] _objSqlParam = new SqlParameter[6];

            _objSqlParam[0] = new SqlParameter("@QUEDESC", queDesc);
            _objSqlParam[1] = new SqlParameter("@ANSOPT1", ansOpt1);
            _objSqlParam[2] = new SqlParameter("@ANSOPT2", ansOpt2);
            _objSqlParam[3] = new SqlParameter("@ANSOPT3", ansOpt3);
            _objSqlParam[4] = new SqlParameter("@ANSOPT4", ansOpt4);
            _objSqlParam[5] = new SqlParameter("@GRPID", grpId);
            objDAL.Insertrecord("USP_IMPORT_EXAM_EXCEL_DATA", _objSqlParam);
        }
    }

    #endregion

    #region "Function"
    #region "Upload Que file Excel"

    public static DataSet ConvertExcelToDataTable(string FileName)
    {
        DataSet dtResult = null;
        int totalSheet = 0; //No of sheets on excel file  
        try
        {
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";

                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds);
                dtResult = ds;
                objConn.Close();
            }
        }
        catch (Exception ex)
        {

        }

        return dtResult; //Returning Dattable 

    }

    #endregion
    #endregion
}