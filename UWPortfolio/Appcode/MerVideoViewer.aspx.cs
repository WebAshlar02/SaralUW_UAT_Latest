//**********************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//**********************************************************************/      
//*                  I N F O R M A T I O N                                       
//********************************************************************* 
// Sr. No.              : 1  
// Company              : Life            
// Module               : UW Saral          
// Program Author       : Jayendra Patankar [WebAshlar01]            
// BRD/CR/Codesk No/Win : CR-4126         
// Date Of Creation     : 27-09-2022            
// Description          : CR-4126 - SFTP integration with TPA for storing Video MER recordings
//**********************************************************************//
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_VideoViewer : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["objLoginObj"] == null)
        {
            Response.Redirect("~/9011042143.aspx");
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btn_VidByApplicationNo(object sender, EventArgs e)
    {
        try
        {
            bind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;

        }
    }
    protected void VideoGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            var url = "";
            string fileName = (VideoGrid.SelectedRow.FindControl("FullName") as Label).Text;
            string Extension = (VideoGrid.SelectedRow.FindControl("Extension") as Label).Text;
            int index = 0;
            if (fileName.Contains("Health-Video"))
            {
                var serverPath = ConfigurationManager.AppSettings["HealthVideoUrl"].ToString();  //"http://10.1.41.136:105".ToString();
                index = "Health-Video".Length + fileName.IndexOf("Health-Video");
                url = serverPath + fileName.Substring(index).Replace("\\", "/").ToString();
            }
            else if (fileName.Contains("Docsup-vi"))
            {
                var serverPath = ConfigurationManager.AppSettings["DocsupViUrl"].ToString(); //"http://10.1.41.136:106".ToString();
                index = "Docsup-vi".Length + fileName.IndexOf("Docsup-vi");
                url = serverPath + fileName.Substring(index).Replace("\\", "/").ToString();
            }
            else if (fileName.Contains("fg-vm"))
            {
                var serverPath = ConfigurationManager.AppSettings["FgVmUrl"].ToString(); // "http://10.1.41.136:107".ToString();
                index = "fg-vm".Length + fileName.IndexOf("fg-vm");
                url = serverPath + fileName.Substring(index).Replace("\\", "/").ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Video not found on Server !!');", true);
            }
            if(Extension == ".pdf")
            {
                
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('"+ url + "','_blank');", true);
                
            }
            else
            {
                Literal1.Text = "<Video Width=600 controls autoplay><Source src=" + url + " type=video/mp4></video>";
                Image1.CssClass = "displayVideo";
            }

            
        }
        catch (Exception ex)
        {
            string msg = ex.Message;

        }
    }

    protected void bind()
    {
        try
        {
            string applicationNum = ApplicationNum.Text.ToString().Trim();
            Literal1.Text = "";
            FileInfo[] HealthVideoFiles;
            FileInfo[] DocsupviFiles;
            FileInfo[] fgvmFiles;
            //FileInfo[] AllFiles;
            DirectoryInfo folder1 = new DirectoryInfo(ConfigurationManager.AppSettings["HealthVideoPath"].ToString());
            HealthVideoFiles = folder1.GetFiles(applicationNum + "*.*", SearchOption.AllDirectories);
            DirectoryInfo folder2 = new DirectoryInfo(ConfigurationManager.AppSettings["DocsupViPath"].ToString());
            DocsupviFiles = folder2.GetFiles(applicationNum + "*.*", SearchOption.AllDirectories);
            DirectoryInfo folder3 = new DirectoryInfo(ConfigurationManager.AppSettings["FgVmPath"].ToString());
            fgvmFiles = folder3.GetFiles(applicationNum + "*.*", SearchOption.AllDirectories);

            FileInfo[] AllFiles = new FileInfo[HealthVideoFiles.Length + DocsupviFiles.Length + fgvmFiles.Length];
            //AllFiles = [HealthVideoFiles.Length + DocsupviFiles.Length + fgvmFiles.Length];
            Array.Copy(HealthVideoFiles, AllFiles, HealthVideoFiles.Length);
            Array.Copy(DocsupviFiles, 0, AllFiles, HealthVideoFiles.Length, DocsupviFiles.Length);
            Array.Copy(fgvmFiles, 0, AllFiles, (HealthVideoFiles.Length + DocsupviFiles.Length), fgvmFiles.Length);

            DataTable MERVideodata = new DataTable();
            DataRow _drRow = null;

            if (!string.IsNullOrEmpty(applicationNum))
            {
                if (AllFiles.Length > 0)
                {

                    MERVideodata.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                    MERVideodata.Columns.Add(new DataColumn("Name", typeof(string)));
                    MERVideodata.Columns.Add(new DataColumn("FullName", typeof(string)));
                    MERVideodata.Columns.Add(new DataColumn("Extension", typeof(string)));
                    MERVideodata.Columns.Add(new DataColumn("CreationTime", typeof(string)));
                    MERVideodata.Columns.Add(new DataColumn("WatchVideo", typeof(string)));
                    MERVideodata.Columns.Add(new DataColumn("ViewCheckList", typeof(string)));
                    for (int i = 0; i < AllFiles.Length; i++)
                    {
                        _drRow = MERVideodata.NewRow();
                        _drRow[1] = AllFiles[i].Name;
                        _drRow[2] = AllFiles[i].FullName;
                        _drRow[3] = AllFiles[i].Extension;
                        _drRow[4] = AllFiles[i].CreationTime;
                        _drRow[5] = AllFiles[i].Extension != ".pdf" ? "Watch Video" : "";
                        _drRow[6] = AllFiles[i].Extension == ".pdf" ? "View CheckList" : "";
                        MERVideodata.Rows.Add(_drRow);
                        ViewState["VideoMER_data"] = MERVideodata.Copy();
                    }
                   
                    VideoGrid.Visible = true;
                    VideoGrid.DataSource = MERVideodata;
                    VideoGrid.DataBind();
                    Image1.CssClass = "displayVideo";

                }
                else
                {
                    Image1.Visible = true;
                    Literal1.Text = "";
                    Image1.CssClass = "";
                    VideoGrid.Visible = false;
                }
            }
            else
            {
                VideoGrid.Visible = false;
                Image1.Visible = false;
            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;

        }

    }
    protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        VideoGrid.PageIndex = e.NewPageIndex;
        bind();
    }
}