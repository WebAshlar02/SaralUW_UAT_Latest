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
public partial class AppCode_UWSaralTPAManualDocPush : System.Web.UI.Page
{
    DataTable _dtExcel = null;
    int fileSize = 0;
    Commfun objCommFun = null;
    DataSet ds = null;
    int intRet;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUploadTPADocFile_Click(object sender, EventArgs e)
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
                            objCommFun = new Commfun();
                            objCommFun.TPADocsManualInsert(_dtExcel, ref intRet);
                            if (intRet>0)
                            {
                                lblUploadRiskParameter.Text = "File uploaded successfully";    
                            }
                            else
                            {
                                lblUploadRiskParameter.Text = "Try again later";    
                            }
                            
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
}