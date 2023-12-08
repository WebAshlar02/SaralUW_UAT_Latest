using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using UWSaralServices;
using UWSaralObjects;
using System.Configuration;
using Platform.Utilities.LoggerFramework;
using System.Net;
using Persits.PDF;
using System.Drawing;
using System.Drawing.Imaging;

//*********************************************************************************************************************************/
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Sagar Thorave              
// BRD/CR/Codesk No/Win : CR - 5335
// Date Of Creation     : 12.12.2022           
// Description          : 1.Convert Pdf To Tiff 
//************************************************************************************************


namespace UWSaralSchedulerTPA
{
    public class TPA_DMS_Service
    {
        //1.1 Begin of Changes; Sagar Thorave - [mfl00886]
        const int WIDTH = 3229;
        const int HEIGHT = 4568;
        const int RESOLUTION = 500;
        const string SUCCESS = "SUCCESS", FAILURE = "FAILURE", EXCEPTION = "EXCEPTION";
        public string strClassName = "PDFtoTIFF";
        public string strTempPathJpg = string.Empty;
        string ApplicationNo = string.Empty;
        //1.1 End of Changes; Sagar Thorave - [mfl00886]
        public static void Main()
        {
            try
            {
                Logger.Info("***********************DMS Service Start****************************" + System.Environment.NewLine);
                //check whether service is running or not 
                Logger.Info("***********************Check whether service is already running or not****************************" + System.Environment.NewLine);
                if (true)
                {
                    //fetch data 
                    UWSaralServices.CommFun objCommFun = new CommFun();
                    List<TIF> lstTiff = new List<TIF>();
                    DataSet _ds = new DataSet();
                    DataTable dtFile = new DataTable();
                    DataTable dtFileMER = new DataTable();

                    Logger.Info("***********************Fetch no of documents which is to be pushed****************************" + System.Environment.NewLine);
                    objCommFun.UWSaralTPA_Fetch_document_To_Be_Pushed(ref _ds);
                    if (_ds != null && _ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                    {
                        dtFile = _ds.Tables[0];
                        //dtFileMER = _ds.Tables[1];
                    }
                    if (dtFile != null)
                    {
                        ListTIff listTiff = new ListTIff();

                        for (int i = 0; i < dtFile.Rows.Count; i++)
                        {

                            TIF objTiff = new TIF();
                            TPA_DMS_Service objTpaDmsService = new TPA_DMS_Service();

                            objTiff.FileName = Convert.ToString(dtFile.Rows[i]["FileName"]) + ".pdf";
                            objTiff.ApplicationNo = Convert.ToString(dtFile.Rows[i]["ApplicationNo"]);
                            string strErrorMessage = string.Empty;
                            //if (objTiff.ApplicationNo == "F10757884A")
                            //{
                            //convert to bytes of array                                                   
                            Logger.Info("***********************Fetch file from source**************************** " + objTiff.FileName + System.Environment.NewLine);
                            objTpaDmsService.FetchPDF(objTiff);
                            if (objTiff.Flag != 1)
                            {
                                //convert into tiff 
                                TiffConversion objTiffConversion = new TiffConversion();
                                Logger.Info("***********************Convert into tiff file **************************** " + objTiff.FileName + System.Environment.NewLine);
                                //objTiffConversion.ConsumeTiffConversion(objTiff, ref strErrorMessage);
                                //if (objTiff.Flag != 1 && objTiff.bytArrReceive != null && objTiff.bytArrReceive.Length > 0)
                                //{
                                //place it in destination folder
                                Logger.Info("***********************save it in dmz folder**************************** " + objTiff.FileName + System.Environment.NewLine);
                                //1.1 Begin of Changes; Sagar Thorave - [mfl00886]
                                //objTpaDmsService.FetchImageFromBytes(objTiff);

                                objTpaDmsService.PdftoTiff(objTiff);
                                //1.1 End of Changes; Sagar Thorave - [mfl00886]
                                //}
                                //else
                                //{
                                //    objTiff.Flag = -4;
                                //}
                            }
                            objTiff.GetMessage();
                            if (objTiff.Flag == 0)
                            {
                                //update status
                                Logger.Info("***********************update resolve status**************************** " + objTiff.ApplicationNo + Environment.NewLine);
                                int intRef = -1;
                                objCommFun.UpdateTPAResolveStatus(objTiff.ApplicationNo, ref intRef);
                            }

                            lstTiff.Add(objTiff);
                            listTiff.LstTiff = lstTiff;
                            Logger.Info("***********************insert status of process**************************** " + objTiff.FileName + Environment.NewLine);
                            //}
                        }
                        //UPDATE FLAG OF DOCUMENT PUSHED 
                        int intRet = -1;
                        objCommFun.UWSaralTPAUpdateDocumentStatus(listTiff.DtLstTiff, ref intRet);
                    }
                    //For MER file pushed to DMS 
                    if (dtFileMER != null)
                    {
                        ListTIff_MER listTiff_MER = new ListTIff_MER();
                        lstTiff.Clear();
                        for (int i = 0; i < dtFileMER.Rows.Count; i++)
                        {

                            TIF objTiff_MER = new TIF();
                            TPA_DMS_Service objTpaDmsService = new TPA_DMS_Service();

                            //objTiff.FileName = Convert.ToString(dtFile.Rows[i]["FileName"]) + ".pdf";
                            objTiff_MER.ApplicationNo = Convert.ToString(dtFileMER.Rows[i]["ApplicationNo"]);
                            objTiff_MER.FileNameMER = Convert.ToString(dtFileMER.Rows[i]["FileName"]) + ".pdf";

                            string strErrorMessage = string.Empty;

                            //convert to bytes of array                                                   
                            Logger.Info("***********************Fetch file from source**************************** " + objTiff_MER.FileNameMER + System.Environment.NewLine);
                            objTpaDmsService.FetchPDF_MER(objTiff_MER);
                            if (objTiff_MER.Flag != 1)
                            {
                                //convert into tiff 
                                TiffConversion objTiffConversion = new TiffConversion();
                                Logger.Info("***********************Convert into tiff file **************************** " + objTiff_MER.FileNameMER + System.Environment.NewLine);
                                //objTiffConversion.ConsumeTiffConversion(objTiff, ref strErrorMessage);
                                //if (objTiff.Flag != 1 && objTiff.bytArrReceive != null && objTiff.bytArrReceive.Length > 0)
                                //{
                                //place it in destination folder
                                Logger.Info("***********************save it in dmz folder**************************** " + objTiff_MER.FileNameMER + System.Environment.NewLine);
                                objTpaDmsService.FetchImageFromBytes_MER(objTiff_MER);
                                //}
                                //else
                                //{///
                                //    objTiff.Flag = -4;
                                //}
                            }
                            objTiff_MER.GetMessage();
                            if (objTiff_MER.Flag == 0)
                            {
                                //update status
                                Logger.Info("***********************update resolve status**************************** " + objTiff_MER.ApplicationNo + Environment.NewLine);
                                int intRef = -1;
                                objCommFun.UpdateTPAResolveStatus(objTiff_MER.ApplicationNo, ref intRef);
                            };

                            lstTiff.Add(objTiff_MER);

                            listTiff_MER.LstTiff_MER = lstTiff;

                            Logger.Info("***********************insert status of process**************************** " + objTiff_MER.FileNameMER + Environment.NewLine);

                        }
                        //UPDATE FLAG OF DOCUMENT PUSHED 
                        int intRet = -1;
                        objCommFun.UWSaralTPAUpdateDocumentStatus_MER(listTiff_MER.DtLstTiff_MER, ref intRet);
                    }

                }
            }
            catch (Exception Error)
            {
                CommFun objComm = new CommFun();
                Logger.Error("STAG 2 :PageName :TPA_DMS_Service.CS // MethodeName :Main Error :" + System.Environment.NewLine + Error.ToString());
                objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", Error.ToString());
            }
        }

        //check whether application is running or not 
        private static bool AlreadyRunning()
        {
            Process[] processes = Process.GetProcesses();
            Process currentProc = Process.GetCurrentProcess();

            foreach (Process process in processes)
            {
                try
                {
                    if (process.Modules[0].FileName == System.Reflection.Assembly.GetExecutingAssembly().Location
                                && currentProc.Id != process.Id)
                        return true;
                }
                catch (Exception ex)
                {
                    CommFun objComm = new CommFun();
                    objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", ex.ToString());
                }
            }

            return false;
        }

        private void FetchPDF(TIF objTif)
        {
            try
            {
                UWSaralServices.CommFun objCommFun = new CommFun();
                Logger.Error("STAG 3 :PageName :TPA_DMS_Service.CS // MethodeName :FetchPDF Start" + System.Environment.NewLine);
                string strFilePath = Properties.Settings.Default.SourcePath;
                if (Directory.Exists(strFilePath))
                {
                    string strFullFile = Path.Combine(strFilePath, objTif.FileName);
                    if (File.Exists(strFullFile))
                    {
                        //objTif.bytArrSend = System.IO.File.ReadAllBytes(strFullFile);
                        //objTif.Extension = Path.GetExtension(strFullFile);
                        objTif.Flag = 0;
                    }
                    else
                    {
                        objTif.Flag = -3;
                    }

                }
                else
                {
                    objTif.Flag = -2;
                }
                if (objTif.Flag == 0)
                {
                    int intRef = -1;
                   // objCommFun.InsertDMSLog(objTif.ApplicationNo, "Found", ref intRef);
                }
                else
                {
                    int intRef = -1;
                    objCommFun.InsertDMSLog(objTif.ApplicationNo, "Not Found", ref intRef);
                }
                Logger.Error("STAG 3 :PageName :TPA_DMS_Service.CS // MethodeName :FetchPDF End" + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                CommFun objComm = new CommFun();
                objTif.Flag = 1;
                objTif.Message = "FetchPDF " + ex.Message;
                Logger.Error("STAG 3 :PageName :TPA_DMS_Service.CS // MethodeName :FetchPDF Error :" + System.Environment.NewLine + ex.ToString());
                objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", ex.ToString());
            }
        }
        private void FetchPDF_MER(TIF objTif_MER)
        {
            try
            {
                UWSaralServices.CommFun objCommFun = new CommFun();
                Logger.Error("STAG 3 :PageName :TPA_DMS_Service.CS // MethodeName :FetchPDF Start" + System.Environment.NewLine);
                string strFilePath = Properties.Settings.Default.SourcePath;
                if (Directory.Exists(strFilePath))
                {

                    string strFullFileMER = Path.Combine(strFilePath, objTif_MER.FileNameMER);
                    if (File.Exists(strFullFileMER))
                    {
                        //objTif.bytArrSend = System.IO.File.ReadAllBytes(strFullFile);
                        //objTif.Extension = Path.GetExtension(strFullFile);
                        objTif_MER.Flag = 0;
                    }
                    else
                    {
                        objTif_MER.Flag = -3;
                    }
                }
                else
                {
                    objTif_MER.Flag = -2;
                }
                if (objTif_MER.Flag == 0)
                {
                    int intRef = -1;
                    objCommFun.InsertDMSLog(objTif_MER.ApplicationNo, "Found", ref intRef);
                }
                else
                {
                    int intRef = -1;
                    objCommFun.InsertDMSLog(objTif_MER.ApplicationNo, "Not Found", ref intRef);
                }
                Logger.Error("STAG 3 :PageName :TPA_DMS_Service.CS // MethodeName :FetchPDF End" + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                CommFun objComm = new CommFun();
                objTif_MER.Flag = 1;
                objTif_MER.Message = "FetchPDF " + ex.Message;
                Logger.Error("STAG 3 :PageName :TPA_DMS_Service.CS // MethodeName :FetchPDF Error :" + System.Environment.NewLine + ex.ToString());
                objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", ex.ToString());
            }
        }
        private void FetchImageFromBytes(TIF objTiff)
        {
            try
            {
                UWSaralServices.CommFun objCommFun = new CommFun();
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes Start " + System.Environment.NewLine);
                //File.WriteAllBytes(Path.Combine(Properties.Settings.Default.DestinationPath, objTiff.FileNameWithTiffExtension), objTiff.bytArrReceive);
                string FTPpathName = Properties.Settings.Default.DestinationPath;
                string FTPUserName = Properties.Settings.Default.UserName;
                string FTPPassword = Properties.Settings.Default.Password;
                string strFilePath = Properties.Settings.Default.SourcePath;
                string strappno = objTiff.ApplicationNo;
                string strFullFile = Path.Combine(strFilePath, objTiff.FileName);
                objTiff.Flag = 1;
                if (File.Exists(strFullFile))
                {

                    FileInfo objfileinfo = null;
                    objfileinfo = new FileInfo(strFullFile);
                    string str = string.Empty;
                    //if (objTiff.FileName.Contains("MER"))
                    //{
                    //    str = "New Business_" + objfileinfo.Name;
                    //}
                    //else
                    //{
                    //str = "New Business_" + objfileinfo.Name.Replace(".pdf", string.Empty) + "_" + "Medical Requirement.pdf";
                    str = "New Business_" + strappno + "_" + "Medical Requirement.pdf";
                    //}
                    string PushedtoDMS = string.Empty;
                    PushedtoDMS = FTPpathName + "\\FG_Processed_Upload" + "\\" + str;
                    //objCommFun.FetchDMSPushedData(ref _ds);
                    if (!File.Exists(PushedtoDMS))
                    {
                        objfileinfo.CopyTo(FTPpathName + "\\" + str, true);
                        objTiff.Flag = 0;
                    }
                    #region commented Code


                    // Creating FTP request                
                    //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPpathName + objTiff.FileName);

                    //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(FTPpathName + objTiff.FileName));
                    //request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                    //request.KeepAlive = true;
                    //request.UseBinary = true;
                    //request.UsePassive = true;
                    //request.EnableSsl = false;
                    //request.Method = WebRequestMethods.Ftp.UploadFile;
                    //request.Proxy = new WebProxy();
                    //FileStream fs = File.OpenRead(strFullFile);
                    //byte[] buffer = new byte[fs.Length];
                    //MemoryStream ms = new MemoryStream(buffer);
                    //string str = "New Business_" + filename.Replace(".pdf",string.Empty) + "_" + "Medical Requirement";

                    //try
                    //{
                    //    int contenctLength;
                    //    using (Stream strm = request.GetRequestStream())
                    //    {
                    //        contenctLength = ms.Read(buffer, 0, buffer.Length);
                    //        while (contenctLength > 0)
                    //        {
                    //            strm.Write(buffer, 0, contenctLength);
                    //            contenctLength = ms.Read(buffer, 0, buffer.Length);
                    //        }
                    //    }

                    //    //return true;
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw new Exception("Failed to upload", ex.InnerException);
                    //}
                    //fs.Read(buffer, 0, buffer.Length);
                    //request.ServicePoint.ConnectionLimit = buffer.Length;
                    //fs.Close();
                    //request.Proxy = new WebProxy();
                    //Stream ftpstream = request.GetRequestStream();
                    //ftpstream.Write(buffer, 0, buffer.Length);
                    //ftpstream.Close();

                    //byte[] data = File.ReadAllBytes(strFullFile);
                    // Uploading stream on FTP
                    //Stream reqStream = request.GetRequestStream();
                    //using (Stream stream = request.GetRequestStream())
                    //{
                    //    stream.Write(data, 0, data.Length);
                    //}

                    //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    //if (response == null || (response.StatusCode != FtpStatusCode.CommandOK))
                    //    throw new Exception("Upload failed.");
                    //reqStream.Write(objTiff.bytArrReceive, 0, objTiff.bytArrReceive.Length);
                    //reqStream.Close();
                    #endregion
                }
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes End " + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                CommFun objComm = new CommFun();
                objTiff.Flag = 1;
                objTiff.Message = "FetchImageFromBytes " + Error.Message;
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes Error :" + System.Environment.NewLine + Error.ToString());
                objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", Error.ToString());
            }
        }
        private void FetchImageFromBytes_MER(TIF objTiff_MER)
        {
            try
            {
                UWSaralServices.CommFun objCommFun = new CommFun();
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes Start " + System.Environment.NewLine);
                //File.WriteAllBytes(Path.Combine(Properties.Settings.Default.DestinationPath, objTiff.FileNameWithTiffExtension), objTiff.bytArrReceive);
                string FTPpathName = Properties.Settings.Default.DestinationPath;
                string FTPUserName = Properties.Settings.Default.UserName;
                string FTPPassword = Properties.Settings.Default.Password;
                string strFilePath = Properties.Settings.Default.SourcePath;
                string strappno_MER = objTiff_MER.ApplicationNo;
                string strFullFile = Path.Combine(strFilePath, objTiff_MER.FileNameMER);
                objTiff_MER.Flag = 1;
                if (File.Exists(strFullFile))
                {

                    FileInfo objfileinfo = null;
                    objfileinfo = new FileInfo(strFullFile);
                    string str = string.Empty;
                    str = "New Business_" + strappno_MER + ".pdf";
                    string PushedtoDMS = string.Empty;
                    PushedtoDMS = FTPpathName + "\\FG_Processed_Upload" + "\\" + str;
                    //objCommFun.FetchDMSPushedData(ref _ds);
                    if (!File.Exists(PushedtoDMS))
                    {
                        objfileinfo.CopyTo(FTPpathName + "\\" + str, true);
                        objTiff_MER.Flag = 0;
                    }
                }
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes End " + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                CommFun objComm = new CommFun();
                objTiff_MER.Flag = 1;
                objTiff_MER.Message = "FetchImageFromBytes " + Error.Message;
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes Error :" + System.Environment.NewLine + Error.ToString());
                objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", Error.ToString());
            }
        }

        //1.1 Begin of Changes; Sagar Thorave - [mfl00886]
        private void PdftoTiff(TIF objTiff)
        {
            string strappno = string.Empty;
            try
            {
                UWSaralServices.CommFun objCommFun = new CommFun();
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :PdfToTIFF Start " + System.Environment.NewLine);
                //File.WriteAllBytes(Path.Combine(Properties.Settings.Default.DestinationPath, objTiff.FileNameWithTiffExtension), objTiff.bytArrReceive);
                bool blnIsResolution = true;
                string FTPpathName = Properties.Settings.Default.DestinationPath;
                string FTPUserName = Properties.Settings.Default.UserName;
                string FTPPassword = Properties.Settings.Default.Password;
                string strFilePath = Properties.Settings.Default.SourcePath;
                strappno = objTiff.ApplicationNo;
                string strFullFile = Path.Combine(strFilePath, objTiff.FileName);
                objTiff.Flag = 1;
                FileInfo objfileinfo = null;
                objfileinfo = new FileInfo(strFullFile);
                string str = string.Empty;
                str = "New Business_" + strappno + "_" + "Medical Requirement";
                string PushedtoDMS = string.Empty;
                PushedtoDMS = FTPpathName + "\\FG_Processed_Upload1" + "\\" + str;
                //objCommFun.FetchDMSPushedData(ref _ds);
                if (!File.Exists(PushedtoDMS))
                {
                    //objfileinfo.CopyTo(FTPpathName + "\\" + str, true);
                    objTiff.Flag = 0;
                }
                if (File.Exists(strFullFile))
                {
                    ConvertPdfToImage(strFullFile, strappno + ".pdf", ref blnIsResolution, true, FTPpathName, strappno);
                }
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes End " + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                CommFun objComm = new CommFun();
                objComm.SavePdfToTiffIssue(strappno, Error.Message, "Main", "PdftoTiff");
               
                objTiff.Flag = 1;
                objTiff.Message = "FetchImageFromBytes " + Error.Message;
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes Error :" + System.Environment.NewLine + Error.ToString());
                objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", Error.ToString());
            }
        }

        public string ConvertPdfToImage(string strSourcePath, string strFileName, ref bool blnIsMultipleFile, bool blnIsFixedResolution, string strDestinationPath, string strDocumentTypeId)
        {
            ApplicationNo = strDocumentTypeId;
            string tempPath = string.Empty;
            PdfDocument objDoc = null;
            try
            {
                PdfManager pdfManager = new PdfManager();
                pdfManager.RegKey = ConfigurationSettings.AppSettings["PDFMergerAndConverterKey"].ToString();
                objDoc = pdfManager.OpenDocument(strSourcePath);
                if (objDoc != null)
                {
                    PdfPreview objPreview;
                    if (objDoc.Pages.Count > 1)
                    {
                        blnIsMultipleFile = true;
                        for (int i = 1; i <= objDoc.Pages.Count; i++)
                        {
                            int resolution = 100 + (i * 20) + 50;
                            if (blnIsFixedResolution)
                            {
                                resolution = RESOLUTION;
                            }
                            string resols = string.Format("ResolutionX={0}; ResolutionY={0}; FastCMYK=False", resolution);
                            objPreview = objDoc.Pages[i].ToImage(resols);
                            string strFileNameToSave = strFileName.Substring(0, (strFileName.LastIndexOf('.')));
                            tempPath = string.Format("{0}\\{2}\\{1}", Path.GetDirectoryName(strSourcePath), strFileNameToSave, strDocumentTypeId);
                            strTempPathJpg = string.Format("{0}\\{1}", Path.GetDirectoryName(strSourcePath), strDocumentTypeId);
                            if (!Directory.Exists(tempPath))
                            {
                                Directory.CreateDirectory(tempPath);
                            }
                            objPreview.Save(tempPath + "\\" + strFileNameToSave + "_" + i + ".jpg");
                            objPreview = null;
                        }
                    }
                    else
                    {
                        int resolution = 100;
                        if (blnIsFixedResolution)
                        {
                            resolution = RESOLUTION;
                        }
                        else
                        {
                            int pdgimgHeight = Convert.ToInt32(objDoc.Pages[1].Height);
                            int pdgimgWidth = Convert.ToInt32(objDoc.Pages[1].Width);
                        }
                        string resols = string.Format("ResolutionX={0}; ResolutionY={0}; FastCMYK=False", resolution);
                        objPreview = objDoc.Pages[1].ToImage(resols);
                        string strFileNameToSave = strFileName.Substring(0, (strFileName.LastIndexOf('.')));
                        tempPath = string.Format("{0}\\{2}\\{1}", Path.GetDirectoryName(strSourcePath), strFileNameToSave, strDocumentTypeId);
                        strTempPathJpg = string.Format("{0}\\{1}", Path.GetDirectoryName(strSourcePath), strDocumentTypeId);

                        if (!Directory.Exists(tempPath))
                        {
                            Directory.CreateDirectory(tempPath);
                        }
                        objPreview.Save(tempPath + "\\" + strFileNameToSave + ".jpg");
                        objPreview = null;
                    }
                }
                else
                {
                    tempPath = string.Format("{0} : Password Protected File", FAILURE);
                }
            }
            catch (Exception ex)
            {
                CommFun objComm = new CommFun();
                objComm.SavePdfToTiffIssue(strDocumentTypeId, ex.Message, "Main", "ConvertPdfToImage");
                tempPath = string.Format("{0}:{1}", EXCEPTION, ex.Message);
            }
            finally
            {
                if (objDoc != null)
                {
                    objDoc.Close();
                }
            }

            ConvertMultipleJpgToTiff(tempPath, strDestinationPath, Path.GetFileNameWithoutExtension(strSourcePath) + ".tif", WIDTH, HEIGHT );

            Directory.Delete(strTempPathJpg, true);

            return tempPath;
        }

        public string ConvertMultipleJpgToTiff(string sourcePath, string destinationPath, string fileName, int width = WIDTH, int height = HEIGHT)
        {
            if (Directory.Exists(sourcePath))
            {
                try
                {
                    height = HEIGHT;
                    width = WIDTH;
                    if (height <= 0)
                    {
                        height = HEIGHT;
                    }
                    if (width <= 0)
                    {
                        width = WIDTH;
                    }
                    DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
                    FileInfo[] pdfFileInfo = directoryInfo.GetFiles();

                    Dictionary<int, string> dctFileName = new Dictionary<int, string>();
                    if (pdfFileInfo.Length > 1)
                    {
                        foreach (FileInfo fileInfo in pdfFileInfo)
                        {
                            string str = fileInfo.FullName;
                            int count = Path.GetFileNameWithoutExtension(str).Split(new char[] { '_' }).Length;
                            int key = Convert.ToInt32(Path.GetFileNameWithoutExtension(str).Split(new char[] { '_' })[count - 1]);

                            dctFileName.Add(key, fileInfo.FullName);
                        }
                    }
                    else
                    {
                        int key = 1;
                        dctFileName.Add(key, pdfFileInfo[0].FullName);
                    }

                    string[] fileNames = dctFileName.OrderBy(x => x.Key).Select(x => x.Value).ToArray();

                    string tiffFileName = string.Empty;

                    try
                    {
                        using (Image imgRoot = System.Drawing.Image.FromFile(fileNames[0]))
                        {
                            using (Bitmap bitmap = new Bitmap((Bitmap)imgRoot, width, height))
                            {
                                bitmap.SetResolution(RESOLUTION, RESOLUTION); //now set the bitmap resolution
                                MemoryStream byteStream = new MemoryStream();
                                bitmap.Save(byteStream, ImageFormat.Tiff);
                                #region internal stream management.
                                using (System.Drawing.Image tiff = System.Drawing.Image.FromStream(byteStream))
                                {
                                    ImageCodecInfo encoderInfo = ImageCodecInfo.GetImageEncoders().First(ie => ie.MimeType == "image/tiff");
                                    for (int i = 0; i < fileNames.Length; i++)
                                    {
                                        if (i == 0)
                                        {
                                            string strappno = fileName.Substring(0, (fileName.LastIndexOf('.')));
                                            string str = string.Empty;
                                            str = "New Business_" + strappno + "_" + "Medical Requirement";
                                            fileName = str + ".tif";
                                            string PushedtoDMS = string.Empty;
                                            PushedtoDMS = destinationPath + "\\FG_Processed_Upload";
                                            if (!Directory.Exists(PushedtoDMS))
                                            {
                                                Directory.CreateDirectory(PushedtoDMS);
                                            }
                                            tiffFileName = string.Format("{0}\\{1}", PushedtoDMS, fileName);

                                            EncoderParameters encoderParams = new EncoderParameters(2);
                                            EncoderParameter SaveEncodeParam = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.MultiFrame);
                                            EncoderParameter CompressionEncodeParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)EncoderValue.CompressionCCITT4);

                                            encoderParams.Param[0] = CompressionEncodeParam;
                                            encoderParams.Param[1] = SaveEncodeParam;
                                            tiff.Save(tiffFileName, encoderInfo, encoderParams);
                                        }
                                        else
                                        {
                                            using (System.Drawing.Image frame = System.Drawing.Image.FromFile(fileNames[i]))
                                            {
                                                Bitmap bitmapnxt = new Bitmap((Bitmap)frame, width, height);
                                                bitmapnxt.SetResolution(RESOLUTION, RESOLUTION); //now set the bitmap resolution
                                                MemoryStream byteStreamnxt = new MemoryStream();
                                                bitmapnxt.Save(byteStreamnxt, ImageFormat.Tiff);
                                                System.Drawing.Image tiffnxt = System.Drawing.Image.FromStream(byteStreamnxt);

                                                EncoderParameters EncoderParams = new EncoderParameters(2);
                                                EncoderParameter SaveEncodeParam = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);
                                                EncoderParameter CompressionEncodeParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)EncoderValue.CompressionCCITT4);
                                                EncoderParams.Param[0] = CompressionEncodeParam;
                                                EncoderParams.Param[1] = SaveEncodeParam;
                                                tiff.SaveAdd(tiffnxt, EncoderParams);
                                            }
                                        }
                                        if (i == fileNames.Length - 1)
                                        {
                                            EncoderParameter SaveEncodeParam = new EncoderParameter(System.Drawing.Imaging.Encoder.SaveFlag, (long)EncoderValue.Flush);
                                            EncoderParameters EncoderParams = new EncoderParameters(1);
                                            EncoderParams.Param[0] = SaveEncodeParam;
                                            tiff.SaveAdd(EncoderParams);
                                        }
                                    }
                                    return string.Format("{0}\\{1}", destinationPath, fileName);
                                }
                                #endregion.
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CommFun objComm = new CommFun();
                        objComm.SavePdfToTiffIssue(ApplicationNo, ex.Message, "Main", "ConvertMultipleJpgToTiff");
                        tiffFileName = string.Format("{0}\\{1}", destinationPath, fileName);
                        File.Delete(tiffFileName);
                    }
                }
                catch (Exception ex)
                {
                    CommFun objComm = new CommFun();
                    objComm.SavePdfToTiffIssue(ApplicationNo, ex.Message, "Main", "ConvertMultipleJpgToTiff");
                    return "-1";
                   
                }
            }
            return "-1";
        }

        //1.1 End of Changes; Sagar Thorave - [mfl00886]
        /*
        private void FetchImageFromBytes(TIF objTiff)
        {
            try
            {
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes Start " + System.Environment.NewLine);
                //File.WriteAllBytes(Path.Combine(Properties.Settings.Default.DestinationPath, objTiff.FileNameWithTiffExtension), objTiff.bytArrReceive);
                string FTPpathName = Properties.Settings.Default.DestinationPath;
                string FTPUserName = Properties.Settings.Default.UserName;
                string FTPPassword = Properties.Settings.Default.Password;

                // Creating FTP request                
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(FTPpathName + objTiff.FileNameWithTiffExtension);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);                
                // Uploading stream on FTP
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(objTiff.bytArrReceive, 0, objTiff.bytArrReceive.Length);
                reqStream.Close();
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes End " + System.Environment.NewLine);
            }
            catch (Exception Error)
            {
                CommFun objComm = new CommFun();
                objTiff.Flag = 1;
                objTiff.Message = "FetchImageFromBytes " + Error.Message;
                Logger.Error("STAG 5 :PageName :TPA_DMS_Service.CS // MethodeName :FetchImageFromBytes Error :" + System.Environment.NewLine + Error.ToString());
                objComm.SaveErrorLogs(string.Empty, "Failed", "TPA_DMS_Service", "Commfun.cs", "Main", "E-Error", "", "", Error.ToString());
            }
        }
        */
    }
}
