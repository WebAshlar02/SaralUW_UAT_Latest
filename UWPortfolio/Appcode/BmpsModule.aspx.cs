// Start of changes: Sagar Thorave [MFL00886] CR- 30573
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;


public partial class BmpsModule : System.Web.UI.Page
{
    string conStr1 = ConfigurationSettings.AppSettings["transactiondbLF"];
    
   
    string FilePath = "";
    CommonObject objCommonObj = new CommonObject();
    string strUserId = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        objCommonObj = (CommonObject)Session["objCommonObj"];
        strUserId = objCommonObj._Bpmuserdetails._UserID;

    }

    
    protected void btndownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PolicyRiskFlag.xls"));
            Response.ContentType = "application/Excel";
            DataTable dt = GetDatafromDatabase();
            if (dt != null && dt.Rows.Count > 0)
            {
                string str = "";
                foreach (DataColumn dtcol in dt.Columns)
                {
                    Response.Write(str + dtcol.ColumnName);
                    str = "\t";
                }
                Response.Write("\n");
                foreach (DataRow dr in dt.Rows)
                {
                    str = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Response.Write(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    Response.Write("\n");
                }

            }
            
            Response.Flush();
            Response.Close();
            Response.SuppressContent = true;
            labelMsg.Text = "File has been successfully downloaded";
            labelMsg.ForeColor = System.Drawing.Color.DarkGreen;
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Response.OutputStream.Close();
            try {
                Response.End();
            }
            catch (ThreadAbortException ex) {

            }
            Response.Redirect(Request.RawUrl);
           


        }
        catch (Exception ex) {


        }


    }
   
    protected DataTable GetDatafromDatabase() {
        DataTable dt = new DataTable();
        try
        {
            SqlConnection connection = new SqlConnection(conStr1);
            connection.Open();
            SqlCommand cmd = new SqlCommand("Sp_FeatchPolicyRiskFlag", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connection.Close();
            return dt;
        }
        catch (Exception ex) {
        
            dt = null;
            return dt;
        }

    }


    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUploadDocument.HasFile)
            {
                string FileName = Path.GetFileName(FileUploadDocument.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUploadDocument.PostedFile.FileName);
              
                if (Extension.ToLower() == ".xlsx" || Extension.ToLower() == ".xls")
                {
                      Import_To_Database(FilePath, Extension);


                }
                else
                {
                    labelMsg.Text= "Please select the file with .xlsx and .xls extension.";
                    labelMsg.ForeColor = System.Drawing.Color.DarkRed;
                }

            }
            else {
                labelMsg.Text = "Please select the file.";
                labelMsg.ForeColor = System.Drawing.Color.DarkRed;
            }

        }
        catch (Exception ex)
        {
            SaveErrorLogs(ex.Message);
        }
    }

 
    private void Import_To_Database(string FilePath, string Extension)
    {

        try
        {
            DataTable dt = new DataTable();
            using (XLWorkbook workBook = new XLWorkbook(FileUploadDocument.PostedFile.InputStream))
            {
                IXLWorksheet workSheet = workBook.Worksheet(1);
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable. 
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable. 
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

            }

                if (dt.Rows.Count > 0)
                   {
                 dt = dt.Rows
                 .Cast<DataRow>()
                 .Where(row => !row.ItemArray.All(f=>string.IsNullOrEmpty(f as string ?? f.ToString())))
                 .CopyToDataTable();

                List<BmpsDataListModel> objBmpList = new List<BmpsDataListModel>();

            string RiskFlagArray = ConfigurationSettings.AppSettings["PolicyFlagValues"].ToString();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (dt.Rows[i]["Policy_Id"].ToString() != "" && dt.Rows[i]["Policy_Risk_Flag"].ToString() != "")
                        {

                       string Flagval = dt.Rows[i]["Policy_Risk_Flag"].ToString().ToLower().Trim().Replace(" ", string.Empty);
                        
                        if (RiskFlagArray.Split(',').ToArray().Contains(Flagval))
                                {
                                    BmpsDataListModel bmpmodel = new BmpsDataListModel();
                                    bmpmodel.policyid = dt.Rows[i]["Policy_Id"].ToString();
                                    bmpmodel.policyriskflag = dt.Rows[i]["Policy_Risk_Flag"].ToString();
                                    objBmpList.Add(bmpmodel);
                                }
                                else
                                {
                                 objBmpList = new List<BmpsDataListModel>();
                                    break;
                                }
                        
                }
                else
                {
                    objBmpList = new List<BmpsDataListModel>();
                    break;
                }
            }


                if (objBmpList.Count > 0)
                {
                    XmlSerializer xmlObjBmps = new XmlSerializer(objBmpList.GetType());
                    StringWriter swriter = new StringWriter();
                    XmlWriter w = XmlWriter.Create(swriter);
                    xmlObjBmps.Serialize(w, objBmpList);
                    w.Close();

                    SqlConnection con = new SqlConnection(conStr1);
                    SqlCommand cmd = new SqlCommand("Sp_InsertPolicyRiskFlag", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", strUserId);
                    cmd.Parameters.AddWithValue("@UserName", strUserId);
                    cmd.Parameters.AddWithValue("@List", swriter.ToString());
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (result > 0)
                    {
                        labelMsg.Text = "Records inserted successfully.";
                        labelMsg.ForeColor = System.Drawing.Color.DarkGreen;
                    }
                    else
                    {
                        labelMsg.Text = "Records inserted unsuccessfully.";
                        labelMsg.ForeColor = System.Drawing.Color.DarkRed;
                        
                    }
                }
                else
                {
                    labelMsg.Text = "There is some issue (May be in risk flag value-we are considering only 4 values i.e. Very High, High, Medium, Low  or it is blank) in file please check and try again'.";
                    labelMsg.ForeColor = System.Drawing.Color.DarkRed;
                    
                }

            }
            }
        catch (Exception ex)
        {

            SaveErrorLogs(ex.Message);
        }      
        }
   
    public void SaveErrorLogs(string strerror)
    {
        try
        {
           Commfun objcomm = new Commfun();

            objcomm.InsertPolicyRiskFlagErrorLogs( strUserId, strerror);

            labelMsg.Text = "There is some issue while uploding file, please check file and try again";
            labelMsg.ForeColor = System.Drawing.Color.DarkRed;
        }
        catch (Exception ex)
        {
          
        }
    }


 
}
// End of changes: Sagar Thorave [MFL00886] CR- 30573
