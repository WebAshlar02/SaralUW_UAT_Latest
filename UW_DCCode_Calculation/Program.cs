using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWSaralDecision;
using System.Data;
using UWSaralObjects;
using UWSaralServices;
using Platform.Utilities.LoggerFramework;
using System.Diagnostics;

namespace UW_DCCode_Calculation
{
    class Program
    {
        static void Main(string[] args)
        {
            Program objprogram = new Program();
            DataSet _ds = new DataSet();

            string CUSTPINCODE = "";
            string DCPINCODE = "";
            string APPNO = "";
            string DCCODE = "";
            int intResponse = 0;
            int custcount = 0;
            int dccount = 0;
            objprogram.GetDC_Code(ref _ds);
            if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0 && _ds.Tables[1].Rows.Count > 0)
            {
                for (int j = 0; j <= _ds.Tables[0].Rows.Count-1; j++)
                {
                    for (int i = 0; i <= _ds.Tables[1].Rows.Count-1; i++)
                    {

                        CUSTPINCODE = Convert.ToString(_ds.Tables[0].Rows[j]["PINCODE"]);
                        DCPINCODE = Convert.ToString(_ds.Tables[1].Rows[i]["PINCODE"]);
                        //APPNO = Convert.ToString(_ds.Tables[0].Rows[j]["APPNO"]);
                        DCCODE = Convert.ToString(_ds.Tables[1].Rows[i]["DC_CODE"]);
                        objprogram.Save_DC_Code(APPNO, DCCODE, CUSTPINCODE, DCPINCODE, ref intResponse);

                    }
                    custcount++;
                    Console.WriteLine("inserted complete:-" + custcount + " - " + CUSTPINCODE);
                }
                if (intResponse > 0)
                {
                    
                }
            }
        }
        private void GetDC_Code(ref DataSet _ds)
        {
            Logger.Error("STAG 4 :PageName :UW_DCCode_Calculation.CS // MethodeName :GetDC_Code  Start" + System.Environment.NewLine);
            UWSaralDecision.CommFun objComm = new UWSaralDecision.CommFun();
            objComm.GET_DC_PINCODE(ref _ds);
            Logger.Error("STAG 4 :PageName :UW_DCCode_Calculation.CS // MethodeName :GetDC_Code End" + System.Environment.NewLine);

        }
        private void Save_DC_Code(string APPNO, string DCCODE, string CUSTPINCODE, string DCPINCODE, ref int intResponse)
        {
            Logger.Error("STAG 4 :PageName :UW_DCCode_Calculation.CS // MethodeName :GetDC_Code  Start" + System.Environment.NewLine);
            UWSaralDecision.CommFun objComm = new UWSaralDecision.CommFun();
            objComm.Save_DC_Code(APPNO, DCCODE, CUSTPINCODE, DCPINCODE, ref intResponse);
            Logger.Error("STAG 4 :PageName :UW_DCCode_Calculation.CS // MethodeName :GetDC_Code End" + System.Environment.NewLine);

        }
    }
}
