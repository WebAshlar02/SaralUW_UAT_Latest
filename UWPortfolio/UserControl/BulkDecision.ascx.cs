using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UWSaralObjects;
using System.Text;
using System.Data.OleDb;
using Platform.Utilities.LoggerFramework;

public partial class UserControl_BulkDecision : System.Web.UI.UserControl
{

    BussLayer ObjBuss = new BussLayer();
    DataSet dsPendingDocs = new DataSet();
    DataSet _ds = new DataSet();
    DataTable _dtExcel = new DataTable();
    ChangeValue objChangeObj = new ChangeValue();
    CommonObject objCommonObj = new CommonObject();
    Commfun objComm = new Commfun();
    DataSet _dsPrevPol = new DataSet();
    UWSaralDecision.BussLayer objBuss = new UWSaralDecision.BussLayer();
    BussLayer objBussiness = new BussLayer();
    int fileSize = 0;
    string strChannelType = string.Empty;
    string strConsentRespons = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        objChangeObj = (ChangeValue)Session["objLoginObj"];
        objCommonObj = (CommonObject)Session["objCommonObj"];
    }

    //public void FillcommonObjectDetails(string strApplicationNo, string strAppType)
    //{
    //    Logger.Info(strApplicationno + "STAG 3 :PageName :BulkDecision.ascx // MethodeName :FillcommonObjectDetails" + System.Environment.NewLine);
    //    objComm.OnlineCommonObjectDetails_GET(ref _ds, strApplicationno, strAppType);
    //    BindCommonObjectDetails(_ds.Tables[0], strAppType);

    //}



    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUploadExcel.HasFile)
            {
                bool isValidFile = false;
                string FilePath = string.Empty;
                string fileSavePath = string.Empty;
                string fileName = string.Empty;
                string strUserGroup = string.Empty;
                string strAppstatusKey = string.Empty;
                int UWDecisionResult = 0;
                int LAPushErrorCode = 0;
                int Result = 0;
                int intRetVal = -1;
                string strLAPushErrorMsg = string.Empty;
                lblMsg.Text = string.Empty;
                fileName = FileUploadExcel.FileName;
                fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1, fileName.Length - (fileName.LastIndexOf("\\") + 1));
                string strFileExt = FileUploadExcel.FileName.Substring(FileUploadExcel.FileName.LastIndexOf('.'));
                String fileExtension = System.IO.Path.GetExtension(FileUploadExcel.FileName).ToLower();
                string allowedExtentions = ".xlsx";
                string docUploadPath = Convert.ToString(ConfigurationManager.AppSettings["DocUploadPath"]);
                String[] Extensions = allowedExtentions.Split(' ');
                string struserid = objChangeObj.userLoginDetails._UserID;
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
                                if (!FileUploadExcel.PostedFile.FileName.Equals(string.Empty))
                                {
                                    if (!File.Exists(fileSavePath))
                                    {
                                        FileUploadExcel.PostedFile.SaveAs(fileSavePath);
                                        _dtExcel = ConvertExcelToDataTable(fileSavePath);
                                    }
                                }
                            }
                            if (_dtExcel != null && _dtExcel.Columns.Contains("Application No"))
                            {
                                if (_dtExcel.Rows.Count > 0)
                                {
                                    DataTable _dtResultLog = new DataTable();
                                    _dtResultLog.Columns.Add("User Id");
                                    _dtResultLog.Columns.Add("Application Number");
                                    _dtResultLog.Columns.Add("Decision");
                                    _dtResultLog.Columns.Add("Status");
                                    _dtResultLog.Columns.Add("CreatedDate");
                                    _dtResultLog.Columns.Add("Channel");
                                    _dtResultLog.Columns.Add("IsUpdatedInLa");
                                    _dtResultLog.Columns.Add("Processing Flag");
                                    _dtResultLog.Columns.Add("Source");
                                    foreach (DataRow row in _dtExcel.Rows)
                                    {
                                        if (!string.IsNullOrEmpty(row["Application No"].ToString().Trim()))
                                        {
                                            objComm.OnlineUWDecisionDetails_Save("SINGLE", row["Application No"].ToString(), row["UW Decision Text"].ToString(),
                                                row["Rejection Reason"].ToString(), row["UW Decision Value"].ToString(), row["Rejection Reason Value"].ToString(),
                                                row["Postponed Period"].ToString(), struserid, objChangeObj.userLoginDetails._UserName, objChangeObj.userLoginDetails._UserGroup,
                                                objCommonObj._bpm_branchCode, objCommonObj._bpm_creationDate, objCommonObj._bpm_systemDate, objCommonObj._bpm_businessDate,
                                                objChangeObj.userLoginDetails._userBranch, objChangeObj.userLoginDetails._ProcessName, row["Application No"].ToString(),
                                                "","",ref UWDecisionResult);
                                            if (UWDecisionResult != -1)
                                            {
                                                //DataRow rowLog = new DataRow();
                                                objBuss.OnlineApplicationLAServiceDetails_PUSH(row["Application No"].ToString(), "ONLINE", objChangeObj, ref _ds, ref _dsPrevPol, row["UW Decision Value"].ToString(), ref LAPushErrorCode, ref  strLAPushErrorMsg, ref strConsentRespons);
                                                if (LAPushErrorCode == 0)
                                                {
                                                    strUserGroup = objChangeObj.userLoginDetails._UserGroup;
                                                    if (row["UW Decision Value"].ToString().Trim() == "Approved" && LAPushErrorCode == 0)
                                                    {
                                                        //objComm.OnlineUWMISDecision_Save(row["Application No"].ToString(), row["UW Decision Value"].ToString(), objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName
                                                        //    , objChangeObj.userLoginDetails._userBranch, "SINGLE", struserid, objChangeObj.userLoginDetails._UserGroup, row["Rejection Reason Value"].ToString(),
                                                        //    row["Rejection Reason"].ToString(), ref LAPushErrorCode);
                                                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                                                        objComm.OnlineApplicationchangeStatus(row["Application No"].ToString(), struserid, strAppstatusKey, "", ref Result);
                                                        objBussiness.UpdatePolicyStatus(row["Application No"].ToString().Trim(), "IF", ref intRetVal);
                                                    }
                                                    else if (row["UW Decision Value"].ToString().Trim() == "Declined" && LAPushErrorCode == 0)
                                                    {
                                                        //objComm.OnlineUWMISDecision_Save(row["Application No"].ToString(), row["UW Decision Value"].ToString(), objCommonObj._bpm_branchCode, objChangeObj.userLoginDetails._ProcessName
                                                        //    , objChangeObj.userLoginDetails._userBranch, "SINGLE", struserid, objChangeObj.userLoginDetails._UserGroup, row["Rejection Reason Value"].ToString(),
                                                        //    row["Rejection Reason"].ToString(), ref LAPushErrorCode);
                                                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                                                        objComm.OnlineApplicationchangeStatus(row["Application No"].ToString(), struserid, strAppstatusKey, "", ref Result);
                                                        objBussiness.UpdatePolicyStatus(row["Application No"].ToString().Trim(), "DC", ref intRetVal);
                                                    }
                                                    else if (row["UW Decision Value"].ToString().Trim() == "Postponed" && LAPushErrorCode == 0)
                                                    {
                                                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                                                        objComm.OnlineApplicationchangeStatus(row["Application No"].ToString(), struserid, strAppstatusKey, "", ref Result);
                                                        objBussiness.UpdatePolicyStatus(row["Application No"].ToString().Trim(), "PO", ref intRetVal);
                                                    }
                                                    else if (row["UW Decision Value"].ToString().Trim() == "Withdrawn" && LAPushErrorCode == 0)
                                                    {
                                                        strAppstatusKey = (strUserGroup == "UW") ? "UC" : "DC";
                                                        objComm.OnlineApplicationchangeStatus(row["Application No"].ToString(), struserid, strAppstatusKey, "", ref Result);
                                                        objBussiness.UpdatePolicyStatus(row["Application No"].ToString().Trim(), "WD", ref intRetVal);
                                                    }

                                                    _dtResultLog.Rows.Add(struserid, row["Application No"].ToString(), row["UW Decision Value"].ToString().Trim(), strLAPushErrorMsg, DateTime.Now.ToString("yyyy-MM-dd"), "ONLINE", 1, "UW", "SYSTEM");
                                                    strLAPushErrorMsg = string.Empty;
                                                }
                                                else
                                                {
                                                    _dtResultLog.Rows.Add(struserid, row["Application No"].ToString(), row["UW Decision Value"].ToString().Trim(), strLAPushErrorMsg, DateTime.Now.ToString("yyyy-MM-dd"), "ONLINE", 0, "UW", "SYSTEM");
                                                    strLAPushErrorMsg = string.Empty;

                                                }
                                            }
                                            else
                                            {
                                                Logger.Error(":UserControl :UserControl_BulkDecision // MethodeName :btnUpload_Click : Failed to save record in LF_UWDecisionDetail table");
                                            }
                                        }
                                    }
                                    if (_dtResultLog != null && _dtResultLog.Rows.Count > 0)
                                    {
                                        objComm.OnlineUWBulkDecision_Save(_dtResultLog, ref Result);
                                        gvDecisionLog.DataSource = _dtResultLog;
                                        gvDecisionLog.DataBind();
                                    }
                                }
                            }
                            else
                            {
                                lblMsg.Text = "Please upload well formatted Excel";
                                lblMsg.Visible = true;
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                            }

                        }
                        catch (Exception ex)
                        {
                            //UWSaralDecision.CommFun objCommFun = new UWSaralDecision.CommFun();
                            Logger.Error(ex.Message + ":UserControl :UserControl_BulkDecision // MethodeName :btnUpload_Click");
                            //objCommFun.SaveErrorLogs(strApplnNo, "Failed", "UWSaralDecision", "UserContrl_PopupManageProposar", "_Load", "E-Error", "", "", ex.ToString());
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please upload file with size below 2MB";
                        lblMsg.Visible = true;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid file extention";
                    lblMsg.Visible = true;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openBulkModal();", true);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openBulkModal();", true);
        }
        catch (Exception ex)
        {
            Logger.Error(ex.Message + ":UserControl :UserControl_BulkDecision // MethodeName :btnUpload_Click");
        }
    }

    public static DataTable ConvertExcelToDataTable(string FileName)
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
            Logger.Error(ex.Message + ":UserControl :UserControl_BulkDecision // MethodeName :ConvertExcelToDataTable");
        }

        return dtResult; //Returning Dattable 

    }

}