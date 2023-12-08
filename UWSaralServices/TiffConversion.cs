using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;
using Platform.Utilities.LoggerFramework;
namespace UWSaralServices
{
    public class TiffConversion
    {
        public void ConsumeTiffConversion(TIF objTiff, ref string strErrorMsg)
        {
            try
            {
                Logger.Error("STAG 4 :PageName :TiffConversion.CS // MethodeName :ConsumeTiffConversion Start " + System.Environment.NewLine);
                //declaration 
                LAServiceTiffConversion.GetTiffBytesRequest objRequest = new LAServiceTiffConversion.GetTiffBytesRequest();
                LAServiceTiffConversion.GetTiffBytesRequestBody objRequestBody = new LAServiceTiffConversion.GetTiffBytesRequestBody();
                LAServiceTiffConversion.ResponseData objResponse = new LAServiceTiffConversion.ResponseData();
                LAServiceTiffConversion.TiffConverterServiceSoapClient objClient = new LAServiceTiffConversion.TiffConverterServiceSoapClient();

                //calling service                
                objResponse = objClient.GetTiffBytes(objTiff.Extension, objTiff.bytArrSend);

                strErrorMsg= objResponse.ErrorMsg;                
                objTiff.bytArrReceive= objResponse.FileData;
                Logger.Error("STAG 4 :PageName :TiffConversion.CS // MethodeName :ConsumeTiffConversion End " + System.Environment.NewLine);
            }
            catch (Exception ex)
            {
                objTiff.Flag = -1;
                objTiff.Message = "Consume Tiff Conversion "+ex.Message;
                Logger.Error("STAG 4 :PageName :TiffConversion.CS // MethodeName :ConsumeTiffConversion Error " + ex.Message + System.Environment.NewLine);
            }

        }
    }
}
