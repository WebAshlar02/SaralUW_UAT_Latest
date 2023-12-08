using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data;
using Platform.Utilities.LoggerFramework;
using System.Data.OleDb;
public partial class Appcode_BulkRiskParameterUpload : System.Web.UI.Page
{
    DataTable _dtExcel = null;
    int fileSize = 0;
    Commfun objCommFun = null;
    DataSet ds = null;
    int intRet;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUploadFile_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuBulkRisk.HasFile)
            {
                intRet = -1;
                bool isValidFile = false;
                string FilePath = string.Empty;
                string fileSavePath = string.Empty;
                string fileName = string.Empty;
                string strUserGroup = string.Empty;
                string strAppstatusKey = string.Empty;
                string strLAPushErrorMsg = string.Empty;
                lblUploadRiskParameter.Text = string.Empty;
                fileName = fuBulkRisk.FileName;
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, fileName.Length - (fileName.LastIndexOf("\\") + 1));
                string strFileExt = fuBulkRisk.FileName.Substring(fuBulkRisk.FileName.LastIndexOf('.'));
                String fileExtension = System.IO.Path.GetExtension(fuBulkRisk.FileName).ToLower();
                string allowedExtentions = ".xlsx";
                string docUploadPath = Convert.ToString(ConfigurationManager.AppSettings["DocUploadPath"]);
                String[] Extensions = allowedExtentions.Split(' ');
                for (int i = 0; i < Extensions.Length; i++)
                {
                    if (fileExtension == Extensions[i])
                    {
                        isValidFile = true;
                        break;
                    }
                }

                if (isValidFile)
                {
                    if (fileSize <= 2000000)
                    {
                        try
                        {
                            if (!Directory.Exists(docUploadPath))
                            {
                                Directory.CreateDirectory(docUploadPath);
                            }
                            if (Directory.Exists(docUploadPath))
                            {
                                fileSavePath = string.Format("{0}\\{1}", docUploadPath, DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "_" + "BulkDecision" + strFileExt);
                                if (!fuBulkRisk.PostedFile.FileName.Equals(string.Empty))
                                {
                                    if (!File.Exists(fileSavePath))
                                    {
                                        fuBulkRisk.PostedFile.SaveAs(fileSavePath);
                                        _dtExcel = ConvertExcelToDataTable(fileSavePath);
                                    }
                                }
                            }
                            if (_dtExcel != null && _dtExcel.Rows.Count > 0)
                            {
                                RemoveUnwantedColumns(ref _dtExcel);
                            }
                            objCommFun = new Commfun();
                            objCommFun.ManageBulkRiskParameter(ReplicateDataFromDataTable(_dtExcel), ref intRet);
                            lblUploadRiskParameter.Text = "File uploaded successfully";
                        }
                        catch (Exception ex)
                        {
                            lblUploadRiskParameter.Text = ex.Message;
                            //UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
                            Logger.Error(ex.Message + ":UserControl :UserControl_BulkDecision // MethodeName :btnUpload_Click");
                            //objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "_Load", "E-Error", "", "", ex.ToString());
                        }
                    }
                    else
                    {
                        lblUploadRiskParameter.Text = "Please upload file with size below 2MB";
                        lblUploadRiskParameter.Visible = true;
                        lblUploadRiskParameter.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblUploadRiskParameter.Text = "Invalid file extention";                    
                    lblUploadRiskParameter.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblUploadRiskParameter.Text = "Please select file";
            }
        }
        catch (Exception ex)
        {

        }
    }

    private static DataTable ConvertExcelToDataTable(string FileName)
    {
        DataTable dtResult = null;
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
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
            }
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message + ":Page :BulkRiskParameterUpload // MethodeName :ConvertExcelToDataTable");
        }

        return dtResult; //Returning Dattable 
    }

    private void RemoveUnwantedColumns(ref DataTable dt)
    {
        dt.Columns.Remove("REGISTER");
        dt.Columns.Remove("LA_PCODE");
        dt.Columns.Remove("LA_Occupation");
        dt.Columns.Remove("LA_Education");
        dt.Columns.Remove("Product_Name");
        dt.Columns.Remove("LA_Age_At_Entry");
        dt.Columns.Remove("Age_Proof");
        dt.Columns.Remove("LACode");
        dt.Columns.Remove("LA_Annual_Income");
        dt.Columns.Remove("Premium_Amt");
        dt.Columns.Remove("Branch Name");
        dt.Columns["Channel"].ColumnName = "CHANNEL_CODE";
        dt.Columns["channel1"].ColumnName = "CHANNEL_NAME";
        dt.Columns["Risk_Score _(Acturial Model)"].ColumnName = "Risk_Score";


        ////TEST 
        //dt.Columns.Remove("REMARKS");
        //dt.Columns.Remove("APPNO");
        //dt.Columns.Remove("SUMASSURED");
        dt.Columns.Remove("LANAME");
        //dt.Columns.Remove("AGNTNUM");
        //dt.Columns.Remove("CHANNEL_CODE");
        //dt.Columns.Remove("CHANNEL_NAME");
    }

    private DataTable ReplicateDataFromDataTable(DataTable dtOld)
    {
        DataTable dt = new DataTable();
        //give column structure 
        dt.Columns.Add("POLICYNO", typeof(string));
        dt.Columns.Add("Underwriting  Due Diligence Required", typeof(string));
        dt.Columns.Add("PARAMETERS_COMBINATION", typeof(string));
        dt.Columns.Add("Risk_Score", typeof(string));
        dt.Columns.Add("Suggestive _Requirement", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
        dt.Columns.Add("APPNO", typeof(string));
        dt.Columns.Add("SumAssured", typeof(string));
        //dt.Columns.Add("LANAME", typeof(string));
        dt.Columns.Add("CHANNEL_CODE", typeof(string));
        dt.Columns.Add("AGNTNUM", typeof(string));
        dt.Columns.Add("CHANNEL_NAME", typeof(string));

        //fill data 
        if (dtOld!=null && dtOld.Rows.Count>0)
        {
            foreach (DataRow row in dtOld.Rows)
            {
                dt.ImportRow(row);
            }
        }
        return dt;            
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblUploadRiskParameter.Text = string.Empty;

        try
        {
            if (!string.IsNullOrEmpty(txtSearchApplicationNo.Text))
            {
                ds = new DataSet();
                objCommFun = new Commfun();
                objCommFun.FetchBulkRiskApplication(txtSearchApplicationNo.Text, ref ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    dgRiskApplication.DataSource = ds;
                }
                else
                {
                    dgRiskApplication.DataSource = null;
                    lblUploadRiskParameter.Text = "No record found";
                }
                dgRiskApplication.DataBind();

            }
            else
            {
                lblUploadRiskParameter.Text = "Enter application number";
            }
        }
        catch (Exception ex)
        {

            lblUploadRiskParameter.Text = "Try again later";
        }
    }
}