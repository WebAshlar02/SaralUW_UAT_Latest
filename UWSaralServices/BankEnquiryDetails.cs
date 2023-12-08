using Platform.Utilities.LoggerFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using UWSaralObjects;


namespace UWSaralServices
{
    public class BankEnquiryDetails
    {
        CommFun objcomm = new CommFun();
        string CHDRSEL, ZDOCTOR, BPMBRANCH, BPMUSERROLE, BPMUSERID;
        //CBACRT variables
        string CBANKSEL = string.Empty;
        string BANKACCDSC = string.Empty;
        string BANKACOUNT = string.Empty;
        string BANKKEY = string.Empty;
        string BNKACTYP = string.Empty;
        string CURRCODE = string.Empty;
        string DATEFROM = string.Empty;
        string FACTHOUS = string.Empty;
        string SCTYCDE = string.Empty;
        // CBAUPD Variable
        string DATTO = string.Empty;
        //Comman
        string Branch = string.Empty;
        string UserRole = string.Empty;
        string UserID = string.Empty;
        string strpartnerId = string.Empty;
        string strprocessID = string.Empty;
        string strApplicationNo = string.Empty;
        //CLIUPC Variables
        string CLTTWO = string.Empty;
        string CAPITAL = string.Empty;
        string CLTADDR01 = string.Empty;
        string CLTADDR02 = string.Empty;
        string CLTADDR03 = string.Empty;
        string CLTADDR04 = string.Empty;
        string CLTADDR05 = string.Empty;
        string CLTDOBX = string.Empty;
        string CLTPCODE = string.Empty;
        string CLTPHONE01 = string.Empty;
        string CLTPHONE02 = string.Empty;
        string CLTSTAT = string.Empty;
        string CTRYCODE = string.Empty;
        string CTRYORIG = string.Empty;
        string DIRMAIL = string.Empty;
        string ECACT = string.Empty;
        string FAO = string.Empty;
        string FAXNO = string.Empty;
        string LANGUAGE = string.Empty;
        string LGIVNAME = string.Empty;
        string LSURNAME = string.Empty;
        string MAILING = string.Empty;
        string SECURITYNO = string.Empty;
        string SERVBRH = string.Empty;
        string SRDATE = string.Empty;
        string STAFFNO = string.Empty;
        string STATCODE = string.Empty;
        string TAXFLAG = string.Empty;
        string TGRAM = string.Empty;
        string TLXNO = string.Empty;
        string VIP = string.Empty;
        string OLDIDNO = string.Empty;
        string RINTERNET = string.Empty;
        string RTAXIDNUM = string.Empty;
        string ZSPECIND = string.Empty;
        //CLIUPP Variable
        string ADDRTYPE = string.Empty;
        string BIRTHP = string.Empty;
        string DOCNO = string.Empty;
        string CLTSEX = string.Empty;
        string MARRYD = string.Empty;
        string SOE = string.Empty;
        string NATLTY = string.Empty;
        string NMFMT = string.Empty;
        string OCCPCODE = string.Empty;
        string SALUTL = string.Empty;
        string SECUITYNO = string.Empty;
        string ZDOCTIND = string.Empty;
        string RDIDTELNO = string.Empty;
        string RPAGER = string.Empty;
        string RSTAFLAG = string.Empty;
        string DECGRSAL = string.Empty;
        string EMPRTAXR = string.Empty;
        string EVIDDATE = string.Empty;
        string INCDESC = string.Empty;
        string PAYROLLNO = string.Empty;
        string PENSIND = string.Empty;
        string SALCURR = string.Empty;
        string TAXYR = string.Empty;
        string CLTDODX = string.Empty;
        string PRASIND = string.Empty;


        //CBACRT start
        public void BankPushService(string strPQuoteNo, DataSet _dsBank, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******BANK creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankPushService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LABankEnquiry.ServiceClient objBankCreate = new LABankEnquiry.ServiceClient();
                LABankEnquiry.MasterClientBankAccCreate objRes = new LABankEnquiry.MasterClientBankAccCreate();



                if (_dsBank.Tables.Count > 0 && _dsBank.Tables[0].Rows.Count > 0)
                {

                    //List<string> strAppNo = _dsBank.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CBANKSEL = _dsBank.Tables[0].Rows[0]["CBANKSEL"].ToString();
                    BANKACCDSC = _dsBank.Tables[0].Rows[0]["BANKACCDSC"].ToString();
                    BANKACOUNT = _dsBank.Tables[0].Rows[0]["BANKACOUNT"].ToString();
                    BANKKEY = _dsBank.Tables[0].Rows[0]["BANKKEY"].ToString();
                    BNKACTYP = _dsBank.Tables[0].Rows[0]["BNKACTYP"].ToString();
                    CURRCODE = _dsBank.Tables[0].Rows[0]["CURRCODE"].ToString();
                    DATEFROM = _dsBank.Tables[0].Rows[0]["DATEFROM"].ToString();
                    FACTHOUS = _dsBank.Tables[0].Rows[0]["FACTHOUS"].ToString();
                    SCTYCDE = _dsBank.Tables[0].Rows[0]["SCTYCDE"].ToString();
                    Branch = _dsBank.Tables[0].Rows[0]["Branch"].ToString();
                    UserRole = _dsBank.Tables[0].Rows[0]["UserRole"].ToString();
                    UserID = _dsBank.Tables[0].Rows[0]["UserID"].ToString();
                    //strpartnerId =_dsMandate.Tables[0].Rows[0]["strpartnerId"].ToString();
                    //strprocessID =_dsMandate.Tables[0].Rows[0]["strprocessID"].ToString();
                    //strApplicationNo =_dsMandate.Tables[0].Rows[0]["strApplicationNo"].ToString();

                    //ZDOCTOR = _dsMandate.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                    //BPMBRANCH = _dsMandate.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                    //BPMUSERROLE = _dsMandate.Tables[0].Rows[0]["BPMUSERROLE"].ToString();

                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    objRes = objBankCreate.CBACRT(CBANKSEL, BANKACCDSC, BANKACOUNT, BANKKEY, BNKACTYP, CURRCODE, DATEFROM, FACTHOUS, SCTYCDE, Branch, UserRole, UserID,
                        strpartnerId, strprocessID, strApplicationNo);



                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :CBACRT" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******BankEnquiryDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankPushService" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs// MethodeName :BankPushService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }

        //CBAENQ start
        public void BankEnquireService(string strPQuoteNo, DataSet _dsBank, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {
                strLAPushStatus = string.Empty;
                Logger.Info(strPQuoteNo + "*******BANK creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankEnquireService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LABankEnquiry.ServiceClient objBankEnq = new LABankEnquiry.ServiceClient();
                LABankEnquiry.MasterClientBankAccEnquiry objRes = new LABankEnquiry.MasterClientBankAccEnquiry();



                if (_dsBank.Tables.Count > 0 && _dsBank.Tables[0].Rows.Count > 0)
                {

                    //List<string> strAppNo = _dsBank.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CBANKSEL = _dsBank.Tables[0].Rows[0]["CBANKSEL"].ToString();

                    BANKACOUNT = _dsBank.Tables[0].Rows[0]["BANKACOUNT"].ToString();
                    BANKKEY = _dsBank.Tables[0].Rows[0]["BANKKEY"].ToString();

                    Branch = _dsBank.Tables[0].Rows[0]["Branch"].ToString();
                    UserRole = _dsBank.Tables[0].Rows[0]["UserRole"].ToString();
                    UserID = _dsBank.Tables[0].Rows[0]["UserID"].ToString();
                    //strpartnerId =_dsMandate.Tables[0].Rows[0]["strpartnerId"].ToString();
                    //strprocessID =_dsMandate.Tables[0].Rows[0]["strprocessID"].ToString();
                    //strApplicationNo =_dsMandate.Tables[0].Rows[0]["strApplicationNo"].ToString();

                    //ZDOCTOR = _dsMandate.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                    //BPMBRANCH = _dsMandate.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                    //BPMUSERROLE = _dsMandate.Tables[0].Rows[0]["BPMUSERROLE"].ToString();

                    BPMUSERID = objChangeObj.userLoginDetails._UserID;


                    objRes = objBankEnq.CBAENQ(CBANKSEL, BANKACOUNT, BANKKEY, Branch, UserRole, BPMUSERID, strpartnerId, strprocessID, strApplicationNo);
                    //objRes = objBankEnq.CBAENQ(CBANKSEL, BANKACOUNT, BANKKEY, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);

                    ;
                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0" && (!string.IsNullOrEmpty(objRes.BANKACCDSC)))
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :CBAENQ" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******BankEnquiryDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);

                        MemoryStream stream = new MemoryStream();
                        DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(LABankEnquiry.MasterClientBankAccEnquiry));
                        jsonSer.WriteObject(stream, objRes);
                        stream.Position = 0;
                        StreamReader sr = new StreamReader(stream);
                        strLAPushStatus = sr.ReadToEnd();
                        strLAPushErrorcode = -3;//Convert.ToInt16(objRes.ERRORCODE);                  
                    }
                    else if (objRes.VALUES.Equals("F826Bank account not on file"))
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankEnquireService" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                        strLAPushStatus = objRes.VALUES;
                        strLAPushErrorcode = -2;                  
                    }
                    else
                    {
                        strLAPushErrorcode = Convert.ToInt32(objRes.ERRORCODE);
                        strLAPushStatus = objRes.VALUES;
                    }
                      
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs// MethodeName :BankEnquireService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }
        //CBAUPD start
        public void BankUpdateService(string strPQuoteNo, DataSet _dsBank, ChangeValue objChangeObj,  ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******BANK creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankUpdateService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LABankEnquiry.ServiceClient objBankUPD = new LABankEnquiry.ServiceClient();
                LABankEnquiry.MasterClientBankAccUpdate objRes = new LABankEnquiry.MasterClientBankAccUpdate();

                //deserialize object 
                LABankEnquiry.MasterClientBankAccEnquiry objBankAccEnq = new LABankEnquiry.MasterClientBankAccEnquiry();
                DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(LABankEnquiry.MasterClientBankAccEnquiry));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(strLAPushStatus));
                objBankAccEnq = (LABankEnquiry.MasterClientBankAccEnquiry)jsonSer.ReadObject(stream);


                //if (objBankAccEnq != null)
                //{
                //    CBANKSEL = strApplicationNo;
                //    BANKACCDSC = objBankAccEnq.BANKACCDSC;
                //    BANKACOUNT = objBankAccEnq.BANKACOUNT;// _dsBank.Tables[0].Rows[0]["BANKACOUNT"].ToString();
                //    BANKKEY = objBankAccEnq.BANKKEY;// _dsBank.Tables[0].Rows[0]["BANKKEY"].ToString();
                //    BNKACTYP = objBankAccEnq.BNKACTYP;//_dsBank.Tables[0].Rows[0]["BNKACTYP"].ToString();
                //    CURRCODE = objBankAccEnq.CURRCODE;//_dsBank.Tables[0].Rows[0]["CURRCODE"].ToString();
                //    DATEFROM = objBankAccEnq.DATEFROM;//_dsBank.Tables[0].Rows[0]["DATEFROM"].ToString();
                //    DATTO = objBankAccEnq.DATTO; //_dsBank.Tables[0].Rows[0]["DATEFROM"].ToString();
                //    FACTHOUS = objBankAccEnq.FACTHOUS;//_dsBank.Tables[0].Rows[0]["DATTO"].ToString();
                //    SCTYCDE = objBankAccEnq.SCTYCDE;//_dsBank.Tables[0].Rows[0]["SCTYCDE"].ToString();
                //    Branch = _dsBank.Tables[0].Rows[0]["Branch"].ToString();
                //    UserRole = _dsBank.Tables[0].Rows[0]["UserRole"].ToString();
                //    UserID = _dsBank.Tables[0].Rows[0]["UserID"].ToString();
                //    //strpartnerId =_dsMandate.Tables[0].Rows[0]["strpartnerId"].ToString();
                //    //strprocessID =_dsMandate.Tables[0].Rows[0]["strprocessID"].ToString();
                //    //strApplicationNo =_dsMandate.Tables[0].Rows[0]["strApplicationNo"].ToString();

                //    //ZDOCTOR = _dsMandate.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                //    //BPMBRANCH = _dsMandate.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                //    //BPMUSERROLE = _dsMandate.Tables[0].Rows[0]["BPMUSERROLE"].ToString();

                //    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                //    objRes = objBankUPD.CBAUPD(CBANKSEL, BANKACCDSC, BANKACOUNT, BANKKEY, BNKACTYP, CURRCODE, DATEFROM, DATTO, FACTHOUS, SCTYCDE, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);

                //}
                //else 
                if (_dsBank.Tables.Count > 0 && _dsBank.Tables[0].Rows.Count > 0)
                {

                    //List<string> strAppNo = _dsBank.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();
                    CBANKSEL = _dsBank.Tables[0].Rows[0]["CBANKSEL"].ToString();
                    BANKACCDSC = _dsBank.Tables[0].Rows[0]["BANKACCDSC"].ToString();
                    BANKACOUNT = _dsBank.Tables[0].Rows[0]["BANKACOUNT"].ToString();
                    BANKKEY = _dsBank.Tables[0].Rows[0]["BANKKEY"].ToString();
                    BNKACTYP = _dsBank.Tables[0].Rows[0]["BNKACTYP"].ToString();
                    CURRCODE = _dsBank.Tables[0].Rows[0]["CURRCODE"].ToString();
                    DATEFROM = _dsBank.Tables[0].Rows[0]["DATEFROM"].ToString();
                    //DATTO = objBankAccEnq.DATTO;
                    FACTHOUS = _dsBank.Tables[0].Rows[0]["FACTHOUS"].ToString(); //objBankAccEnq.FACTHOUS;//_dsBank.Tables[0].Rows[0]["DATTO"].ToString();
                    SCTYCDE = _dsBank.Tables[0].Rows[0]["SCTYCDE"].ToString();
                    Branch = _dsBank.Tables[0].Rows[0]["Branch"].ToString();
                    UserRole = _dsBank.Tables[0].Rows[0]["UserRole"].ToString();
                    UserID = _dsBank.Tables[0].Rows[0]["UserID"].ToString();
                    //strpartnerId =_dsMandate.Tables[0].Rows[0]["strpartnerId"].ToString();
                    //strprocessID =_dsMandate.Tables[0].Rows[0]["strprocessID"].ToString();
                    //strApplicationNo =_dsMandate.Tables[0].Rows[0]["strApplicationNo"].ToString();

                    //ZDOCTOR = _dsMandate.Tables[0].Rows[0]["ZDOCTOR"].ToString();
                    //BPMBRANCH = _dsMandate.Tables[0].Rows[0]["BPMBRANCH"].ToString();
                    //BPMUSERROLE = _dsMandate.Tables[0].Rows[0]["BPMUSERROLE"].ToString();

                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    objRes = objBankUPD.CBAUPD(CBANKSEL, BANKACCDSC, BANKACOUNT, BANKKEY, BNKACTYP, CURRCODE, DATEFROM, DATTO, FACTHOUS, SCTYCDE, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);



                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :CBAENQ" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******BankEnquiryDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankUpdateService" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs// MethodeName :BankUpdateService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }


        //CLIUPC start
        public void BankCoprateClientService(string strPQuoteNo, DataSet _dsBank, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******BANK creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankCoprateClientService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LABankEnquiry.ServiceClient objBankCCL = new LABankEnquiry.ServiceClient();
                LABankEnquiry.MasterCorporateClient objRes = new LABankEnquiry.MasterCorporateClient();



                if (_dsBank.Tables.Count > 0 && _dsBank.Tables[0].Rows.Count > 0)
                {

                    List<string> strAppNo = _dsBank.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CLTTWO = _dsBank.Tables[0].Rows[0]["CLTTWO"].ToString();
                    CAPITAL = _dsBank.Tables[0].Rows[0]["CAPITAL"].ToString();
                    CLTADDR01 = _dsBank.Tables[0].Rows[0]["CLTADDR01"].ToString();
                    CLTADDR02 = _dsBank.Tables[0].Rows[0]["CLTADDR02"].ToString();
                    CLTADDR03 = _dsBank.Tables[0].Rows[0]["CLTADDR03"].ToString();
                    CLTADDR04 = _dsBank.Tables[0].Rows[0]["CLTADDR04"].ToString();
                    CLTADDR05 = _dsBank.Tables[0].Rows[0]["CLTADDR05"].ToString();
                    CLTDOBX = _dsBank.Tables[0].Rows[0]["CLTDOBX"].ToString();
                    CLTPCODE = _dsBank.Tables[0].Rows[0]["CLTPCODE"].ToString();
                    CLTPHONE01 = _dsBank.Tables[0].Rows[0]["CLTPHONE01"].ToString();
                    CLTPHONE02 = _dsBank.Tables[0].Rows[0]["CLTPHONE02"].ToString();
                    CLTSTAT = _dsBank.Tables[0].Rows[0]["CLTSTAT"].ToString();
                    CTRYCODE = _dsBank.Tables[0].Rows[0]["CTRYCODE"].ToString();
                    CTRYORIG = _dsBank.Tables[0].Rows[0]["CTRYORIG"].ToString();
                    DIRMAIL = _dsBank.Tables[0].Rows[0]["DIRMAIL"].ToString();
                    ECACT = _dsBank.Tables[0].Rows[0]["ECACT"].ToString();
                    FAO = _dsBank.Tables[0].Rows[0]["FAO"].ToString();
                    FAXNO = _dsBank.Tables[0].Rows[0]["FAXNO"].ToString();
                    LANGUAGE = _dsBank.Tables[0].Rows[0]["LANGUAGE"].ToString();
                    LGIVNAME = _dsBank.Tables[0].Rows[0]["LGIVNAME"].ToString();
                    LSURNAME = _dsBank.Tables[0].Rows[0]["LSURNAME"].ToString();
                    MAILING = _dsBank.Tables[0].Rows[0]["MAILING"].ToString();
                    SECURITYNO = _dsBank.Tables[0].Rows[0]["SECURITYNO"].ToString();
                    SERVBRH = _dsBank.Tables[0].Rows[0]["SERVBRH"].ToString();
                    SRDATE = _dsBank.Tables[0].Rows[0]["SRDATE"].ToString();
                    STAFFNO = _dsBank.Tables[0].Rows[0]["STAFFNO"].ToString();
                    STATCODE = _dsBank.Tables[0].Rows[0]["STATCODE"].ToString();
                    TAXFLAG = _dsBank.Tables[0].Rows[0]["TAXFLAG"].ToString();
                    TGRAM = _dsBank.Tables[0].Rows[0]["TGRAM"].ToString();
                    TLXNO = _dsBank.Tables[0].Rows[0]["TLXNO"].ToString();
                    VIP = _dsBank.Tables[0].Rows[0]["VIP"].ToString();
                    OLDIDNO = _dsBank.Tables[0].Rows[0]["OLDIDNO"].ToString();
                    RINTERNET = _dsBank.Tables[0].Rows[0]["RINTERNET"].ToString();
                    RTAXIDNUM = _dsBank.Tables[0].Rows[0]["RTAXIDNUM"].ToString();
                    ZSPECIND = _dsBank.Tables[0].Rows[0]["ZSPECIND"].ToString();


                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    objRes = objBankCCL.CLIUPC(CLTTWO, CAPITAL, CLTADDR01, CLTADDR02, CLTADDR03, CLTADDR04, CLTADDR05, CLTDOBX, CLTPCODE, CLTPHONE01, CLTPHONE02, CLTSTAT, CTRYCODE, CTRYORIG, DIRMAIL, ECACT, FAO, FAXNO, LANGUAGE, LGIVNAME, LSURNAME, MAILING, SECURITYNO, SERVBRH, SRDATE, STAFFNO, STATCODE, TAXFLAG, TGRAM, TLXNO, VIP, OLDIDNO, RINTERNET, RTAXIDNUM, ZSPECIND, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);



                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :CLIUPC" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******BankEnquiryDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankCoprateClientService" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs// MethodeName :BankUpdateService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }


        //CLIUPP start
        public void BankPersonalClientService(string strPQuoteNo, DataSet _dsBank, ChangeValue objChangeObj, ref int strLAPushErrorcode, ref string strLAPushStatus)
        {
            try
            {

                Logger.Info(strPQuoteNo + "*******BANK creation Begins for " + strPQuoteNo + "******" + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankPersonalClientService" + System.Environment.NewLine);

                strpartnerId = "UWSaral";
                strprocessID = "UWSaral";
                strApplicationNo = strPQuoteNo;
                //LAFollowupService.ServiceClient objFollowup = new LAFollowupService.ServiceClient();
                //LAFollowupService.Master objRes = new LAFollowupService.Master();
                LABankEnquiry.ServiceClient objBankCCL = new LABankEnquiry.ServiceClient();
                LABankEnquiry.MasterPersonalClient objRes = new LABankEnquiry.MasterPersonalClient();



                if (_dsBank.Tables.Count > 0 && _dsBank.Tables[0].Rows.Count > 0)
                {

                    List<string> strAppNo = _dsBank.Tables[0].AsEnumerable().Select(c => (string)c["CHDRSEL"]).Distinct().ToList();



                    CLTTWO = _dsBank.Tables[0].Rows[0]["CLTTWO"].ToString();
                    ADDRTYPE = _dsBank.Tables[0].Rows[0]["ADDRTYPE"].ToString();
                    BIRTHP = _dsBank.Tables[0].Rows[0]["BIRTHP"].ToString();
                    CLTADDR01 = _dsBank.Tables[0].Rows[0]["CLTADDR01"].ToString();
                    CLTADDR02 = _dsBank.Tables[0].Rows[0]["CLTADDR02"].ToString();
                    CLTADDR03 = _dsBank.Tables[0].Rows[0]["CLTADDR03"].ToString();
                    CLTADDR04 = _dsBank.Tables[0].Rows[0]["CLTADDR04"].ToString();
                    CLTADDR05 = _dsBank.Tables[0].Rows[0]["CLTADDR05"].ToString();
                    CLTDOBX = _dsBank.Tables[0].Rows[0]["CLTDOBX"].ToString();
                    CLTDODX = _dsBank.Tables[0].Rows[0]["CLTDODX"].ToString();
                    CLTPCODE = _dsBank.Tables[0].Rows[0]["CLTPCODE"].ToString();
                    CLTPHONE01 = _dsBank.Tables[0].Rows[0]["CLTPHONE01"].ToString();
                    CLTPHONE02 = _dsBank.Tables[0].Rows[0]["CLTPHONE02"].ToString();
                    CLTSEX = _dsBank.Tables[0].Rows[0]["CLTSEX"].ToString();
                    CLTSTAT = _dsBank.Tables[0].Rows[0]["CLTSTAT"].ToString();
                    CTRYCODE = _dsBank.Tables[0].Rows[0]["CTRYCODE"].ToString();
                    DIRMAIL = _dsBank.Tables[0].Rows[0]["DIRMAIL"].ToString();
                    DOCNO = _dsBank.Tables[0].Rows[0]["DOCNO"].ToString();
                    LANGUAGE = _dsBank.Tables[0].Rows[0]["LANGUAGE"].ToString();
                    LGIVNAME = _dsBank.Tables[0].Rows[0]["LGIVNAME"].ToString();
                    LSURNAME = _dsBank.Tables[0].Rows[0]["LSURNAME"].ToString();
                    MAILING = _dsBank.Tables[0].Rows[0]["MAILING"].ToString();
                    SECURITYNO = _dsBank.Tables[0].Rows[0]["SECURITYNO"].ToString();
                    MARRYD = _dsBank.Tables[0].Rows[0]["MARRYD"].ToString();
                    SOE = _dsBank.Tables[0].Rows[0]["SOE"].ToString();
                    SERVBRH = _dsBank.Tables[0].Rows[0]["SERVBRH"].ToString();
                    SRDATE = _dsBank.Tables[0].Rows[0]["SRDATE"].ToString();
                    STATCODE = _dsBank.Tables[0].Rows[0]["STATCODE"].ToString();
                    TAXFLAG = _dsBank.Tables[0].Rows[0]["TAXFLAG"].ToString();
                    //TGRAM = _dsBank.Tables[0].Rows[0]["TGRAM"].ToString();
                    //TLXNO = _dsBank.Tables[0].Rows[0]["TLXNO"].ToString();
                    VIP = _dsBank.Tables[0].Rows[0]["VIP"].ToString();
                    OLDIDNO = _dsBank.Tables[0].Rows[0]["OLDIDNO"].ToString();
                    RINTERNET = _dsBank.Tables[0].Rows[0]["RINTERNET"].ToString();
                    RTAXIDNUM = _dsBank.Tables[0].Rows[0]["RTAXIDNUM"].ToString();
                    ZSPECIND = _dsBank.Tables[0].Rows[0]["ZSPECIND"].ToString();
                    NATLTY = _dsBank.Tables[0].Rows[0]["NATLTY"].ToString();
                    NMFMT = _dsBank.Tables[0].Rows[0]["NMFMT"].ToString();
                    OCCPCODE = _dsBank.Tables[0].Rows[0]["OCCPCODE"].ToString();
                    SALUTL = _dsBank.Tables[0].Rows[0]["SALUTL"].ToString();
                    SECUITYNO = _dsBank.Tables[0].Rows[0]["SECUITYNO"].ToString();
                    ZDOCTIND = _dsBank.Tables[0].Rows[0]["ZDOCTIND"].ToString();
                    RDIDTELNO = _dsBank.Tables[0].Rows[0]["RDIDTELNO"].ToString();
                    RPAGER = _dsBank.Tables[0].Rows[0]["RPAGER"].ToString();
                    RSTAFLAG = _dsBank.Tables[0].Rows[0]["RSTAFLAG"].ToString();
                    DECGRSAL = _dsBank.Tables[0].Rows[0]["DECGRSAL"].ToString();
                    EMPRTAXR = _dsBank.Tables[0].Rows[0]["EMPRTAXR"].ToString();
                    EVIDDATE = _dsBank.Tables[0].Rows[0]["EVIDDATE"].ToString();
                    INCDESC = _dsBank.Tables[0].Rows[0]["INCDESC"].ToString();
                    PAYROLLNO = _dsBank.Tables[0].Rows[0]["PAYROLLNO"].ToString();
                    PENSIND = _dsBank.Tables[0].Rows[0]["PENSIND"].ToString();
                    SALCURR = _dsBank.Tables[0].Rows[0]["SALCURR"].ToString();
                    TAXYR = _dsBank.Tables[0].Rows[0]["TAXYR"].ToString();
                    PRASIND = _dsBank.Tables[0].Rows[0]["PRASIND"].ToString();


                    BPMUSERID = objChangeObj.userLoginDetails._UserID;



                    objRes = objBankCCL.CLIUPP(CLTTWO, ADDRTYPE, BIRTHP, CLTADDR01, CLTADDR02, CLTADDR03, CLTADDR04, CLTADDR05, CLTDOBX, CLTDODX, CLTPCODE, CLTPHONE01, CLTPHONE02, CLTSEX, CLTSTAT, CTRYCODE, DIRMAIL
                        , DOCNO, LANGUAGE, LGIVNAME, LSURNAME, MAILING, MARRYD, NATLTY, NMFMT, OCCPCODE, SALUTL, SECUITYNO, SERVBRH, SOE, SRDATE, STATCODE, TAXFLAG, VIP, ZDOCTIND, FAXNO, OLDIDNO, RDIDTELNO, RINTERNET, RPAGER, RSTAFLAG, RTAXIDNUM,
                        RTAXIDNUM, ZSPECIND, DECGRSAL, EMPRTAXR, EVIDDATE, INCDESC, PAYROLLNO, PENSIND, PRASIND, SALCURR, TAXYR, Branch, UserRole, UserID, strpartnerId, strprocessID, strApplicationNo);



                }

                if (!string.IsNullOrEmpty(objRes.ERRORCODE))
                {
                    if (objRes.ERRORCODE == "0")
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :CLIUPP" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Succeed" + System.Environment.NewLine);
                        Logger.Info(strPQuoteNo + "*******BankEnquiryDetails creation End for " + strPQuoteNo + "******" + System.Environment.NewLine);
                    }
                    else
                    {
                        Logger.Info(strPQuoteNo + " STAG 2 :PageName :BankEnquiryDetails.cs // MethodeName :BankPersonalClientService" + System.Environment.NewLine + "BANKENQUIRYDetails Modification Failed" + System.Environment.NewLine);
                        //Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
                        objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsServices", "E-ServiceCallError", "", "", objRes.VALUES.ToString() + System.Environment.NewLine);
                    }
                    strLAPushErrorcode = Convert.ToInt16(objRes.ERRORCODE);
                    strLAPushStatus = objRes.VALUES;
                }

            }
            catch (Exception Error)
            {
                strLAPushErrorcode = -1;
                strLAPushStatus = "Failed";
                Logger.Error(strPQuoteNo + "STAG 2 :PageName :BankEnquiryDetails.cs// MethodeName :BankUpdateService Error :" + System.Environment.NewLine + Error.ToString());
                objcomm.SaveErrorLogs(strPQuoteNo, "Failed", "UWSaralServices", "BankEnquiryDetails.cs", "BankEnquiryDetailsService", "E-ErrorException", "", "", Error.ToString() + System.Environment.NewLine);
                Logger.Info(strPQuoteNo + "*******followup creation end for " + strPQuoteNo + "******" + System.Environment.NewLine);
            }
        }

    }
}
