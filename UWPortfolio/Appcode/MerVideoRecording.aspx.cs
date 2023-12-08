//******************************************************************************************************************************* 
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Pooja Shetye          
// BRD/CR/Codesk No/Win : CR - 30499    
// Date Of Creation     : 12-04-2022            
// Description          : 1.Video MER requirement-reviewed 
//*********************************************************************************************************************************/
//1.1 Begin of Changes; Sagar Thorave - [MFL00886]
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Appcode_MerVideoRecording : System.Web.UI.Page
{
    ApiBussLayer apiBuss = new ApiBussLayer();
    BussLayer buss = new BussLayer();
    Commfun com = new Commfun();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
   
    protected void Btn_VidByApplicationNo(object sender, EventArgs e)
    {
       string applicationNum= ApplicationNum.Text.ToString().Trim();
        
        if (!string.IsNullOrEmpty(applicationNum))
        {
           ds = com.GetMerVideoDetails(applicationNum);
           
         
                if (ds.Tables[0].Rows.Count > 0)
                {
                    VideoGrid.DataSource = ds;
                    VideoGrid.DataBind();
                    msg.Text = "";
                }
                else {
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                    VideoGrid.DataSource = ds;
                    VideoGrid.DataBind();
                    VideoGrid.Rows[0].Cells.Clear();
                    VideoGrid.Rows[0].Cells.Add(new TableCell());
                    VideoGrid.Rows[0].Cells[0].ColumnSpan = ds.Tables[0].Columns.Count;
                    VideoGrid.Rows[0].Cells[0].Text = "No Video Data Found";
                    VideoGrid.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;

                }

            }
        
        else {
            msg.Text = "Please Enter Application Number";
            msg.ForeColor = System.Drawing.Color.Red;

        }
    }

    protected void VideoGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
      
        try
        {
            string filename = (VideoGrid.SelectedRow.FindControl("BlobFileName") as Label).Text;
            string BlobStoreDate = (VideoGrid.SelectedRow.FindControl("BlobStoreDate") as Label).Text;
            string[] words = filename.Split('_');
            string applicationNum = words[0].ToString();
            string token = buss.BlobTokn();
            string url = apiBuss.GetEmlBlob(token, applicationNum, Convert.ToDateTime(BlobStoreDate).ToString("yyyy"), filename, "comm");
            Literal1.Text = "<Video Width=400 controls><Source src=" + url + " type=video/mp4></video>";
            msg.Text = "Video Play";
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
          
        }

    }
}
//1.1 End of Changes; Sagar Thorave - [MFL00886]