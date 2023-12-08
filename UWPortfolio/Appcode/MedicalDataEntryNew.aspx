<%@ Page Title="" Language="C#" MasterPageFile="~/Appcode/Bpmuwmodule.master" AutoEventWireup="true" CodeFile="MedicalDataEntryNew.aspx.cs" Inherits="Appcode_MedicalDataEntryNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .lblLable {
            background-color: #E5E5E5;
            /*font-weight: bold;*/
            pointer-events: none;
        }

        .CheckSave {
        }
    </style>

    <script type="text/javascript">

        function addDataByApi() {
            debugger;
            //var clsMedicalData = new Object();
            //var clsHeader = new Object();
            //clsHeader.AppNo = "786";
            //clsHeader.PolicyNo = "7869";
            //clsHeader.TpaRcfNo = "89598";
            //clsHeader.TokenId = "null";
            //clsHeader.ApplicationSource = "Application";
            //clsHeader.TpaID = "210694";

            //clsMRF = new Object();
            //clsMRF.Gender = $("#txtGender").val();
            //clsMRF.DOB = $("#txtDOB").val();
            //clsMRF.AGE = 1;//$("#txtAGE").val();
            //clsMRF.Height = 5;//$("#txtHeight").val();
            //clsMRF.Weight = 5;$("#txtWeight").val();
            //clsMRF.BMI = 5;//$("#txtBMI").val();
            //clsMRF.Pulse = 5;$("#txtPulse").val();
            //clsMRF.ChestInspiration = 6;//$("#txtPulse").val();
            //clsMRF.ChestExpiration = 0;//$("#txtChestInspiration").val();
            //clsMRF.Systolic = 0;//$("#txtChestExpiration").val();
            //clsMRF.Diastolic = 0;//$("#txtSystolic").val();
            //clsMRF.Girth = 0;//$("#txtDiastolic").val();
            ////clsMRF.CaseHTNComments = "";
            ////clsMRF.caseDM = 0;
            ////clsMRF.CaseDMComments = "";
            ////clsMRF.Smoker = 0;
            ////clsMRF.SmokerCommnets = "";
            ////clsMRF.Alcohol = true;
            ////clsMRF.AlcoholComments = "";
            ////clsMRF.Comments = "";
            
            //clsMedicalData._strclsHeader = clsHeader;
            //clsMedicalData._strMrfData = clsMRF;
            
            //$.ajax({
            //    url: 'http://localhost:22503/api/MedicalData/MedicalDataEntry',
            //    type: 'POST',
            //    dataType: 'json',
            //    data: clsMedicalData,
            //    success: function (data, textStatus, xhr) {
            //        console.log(data);
            //    },
            //    error: function (xhr, textStatus, errorThrown) {
            //        console.log('Error in Operation');
            //    }
            //});



            var clsMedicalData = new Object();
            var clsHeader = new Object();
            debugger;
            clsHeader.AppNo = $("#txtAppno").val();
            clsHeader.PolicyNo = "7869";
            clsHeader.TpaRcfNo = "89598";
            clsHeader.TokenId = "null";
            clsHeader.ApplicationSource = "Application";
            clsHeader.TpaID = "210694";

            clsMRF = new Object();
            //AppNo = $("#txtAppNo").val();
            clsMRF.Gender = $("#txtGender").val();
            clsMRF.DOB = $("#txtDOB").val();
            clsMRF.AGE = $("#txtAGE").val();
            clsMRF.Height = $("#txtHeight").val();
            clsMRF.Weight = $("#txtWeight").val();
            clsMRF.BMI = $("#txtBMI").val();
            clsMRF.Pulse = $("#txtPulse").val();
            clsMRF.ChestInspiration = $("#txtPulse").val();
            clsMRF.ChestExpiration = $("#txtChestInspiration").val();
            clsMRF.Systolic = $("#txtChestExpiration").val();
            clsMRF.Diastolic = $("#txtSystolic").val();
            clsMRF.Girth = $("#txtDiastolic").val();

            clsCbcEsr = new Object();

            clsCbcEsr.HB = $("#txtHB").val();
            clsCbcEsr.PCV = $("#txtPCV").val();
            clsCbcEsr.RBC = $("#txtRBC").val();
            clsCbcEsr.MCV = $("#txtMCV").val();
            clsCbcEsr.MCH = $("#txtMCH").val();
            clsCbcEsr.MCHC = $("#txtMCHC").val();
            clsCbcEsr.WBC = $("#txtWBC").val();
            clsCbcEsr.NEUTROPHILS = $("#txNEUTROPHILS").val();
            clsCbcEsr.LYMPHOCYTES = $("#txLYMPHOCYTES").val();
            clsCbcEsr.EOSINOPHILS = $("#txEOSINOPHILS").val();
            clsCbcEsr.MONOCYTES = $("MONOCYTES").val();
            clsCbcEsr.BASOPHILS = $("BASOPHILS").val();
            clsCbcEsr.PLATELETCOUNT = $("PLATELETCOUNT").val();
            clsCbcEsr.ESR = $("#ESR").val();

            clsFBS_RUA = new Object();

            clsFBS_RUA.FBS = $("#txtFBS").val()
            clsFBS_RUA.HBA1C = $("#txtHBA1C").val()
            clsFBS_RUA.RBC = $("#txtRBC").val()




            clsMedicalData._strclsHeader = clsHeader;
            clsMedicalData._strMrfData = clsMRF;
            clsMedicalData._strCbcEsr = clsCbcEsr;
            clsMedicalData._strFBS = clsFBS_RUA;
            //var CbcStrDetails = new Object();

            //CbcStrDetails.HB = 
            //CbcStrDetails.PCV = 
            //CbcStrDetails.RBC = 
            //CbcStrDetails.MCV = 
            //CbcStrDetails.MCH =
            //CbcStrDetails.MCHC = 
            //CbcStrDetails.WBC = 
            //CbcStrDetails.NEUTROPHILS =
            //CbcStrDetails.LYMPHOCYTES =
            //CbcStrDetails.EOSINOPHILS =
            //CbcStrDetails.MONOCYTES =
            //CbcStrDetails.BASOPHILS =
            //CbcStrDetails.PLATELETCOUNT =
            //CbcStrDetails.ESR =


            //objMedicalData._strclsHeader = HeaderClass;
            //objMedicalData._strMrfData = MrfTestDetails;

            //_strclsHeader.chtn = $("#txtCaseHTN").val();
            //_strclsHeader.cdm = $("#txtcaseDM").val();
            //_strclsHeader.smk = $("#txtSmoker").val();
            //_strclsHeader.alc = $("#txtAlcohol").val();
            // _strMrfData.comm = $("#txtComments").val();

            $.ajax({
                type: "POST",
                url: "http://localhost:22503/api/MedicalData/MedicalDataEntry",
                dataType: "json",
                //contentType: "application/json; charset=utf-8",
                data: clsMedicalData,
                success: function (response) {
                    alert(response);
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger;
                    console.log('Error in Operation');
                }
            });
        }




        $(document).ready(function () {
            $("#btnSave").click(function () {
                debugger;
                alert('hi');

                addDataByApi();
                //e.preventDefault();
            });

            //});

            $('#chkMRFDtls').change(function () {
                debugger;
                if ($('#chkMRFDtls').is(':checked')) {

                    //$('#txtAppno').removeClass('lblLable');
                    $('#txtGender').removeClass('lblLable');
                    $('#txtDOB').removeClass('lblLable');
                    $('#txtAGE').removeClass('lblLable');
                    $('#txtHeight').removeClass('lblLable');
                    $('#txtWeight').removeClass('lblLable');
                    $('#txtBMI').removeClass('lblLable');
                    $('#txtPulse').removeClass('lblLable');
                    $('#txtChestInspiration').removeClass('lblLable');
                    $('#txtChestExpiration').removeClass('lblLable');
                    $('#txtSystolic').removeClass('lblLable');
                    $('#txtDiastolic').removeClass('lblLable');
                    $('#txtGirth').removeClass('lblLable');
                }
                else {
                    //$('#txtAppno').addClass('lblLable');
                    $('#txtGender').addClass('lblLable');
                    $('#txtDOB').addClass('lblLable');
                    $('#txtAGE').addClass('lblLable');
                    $('#txtHeight').addClass('lblLable');
                    $('#txtWeight').addClass('lblLable');
                    $('#txtBMI').addClass('lblLable');
                    $('#txtPulse').addClass('lblLable');
                    $('#txtChestInspiration').addClass('lblLable');
                    $('#txtChestExpiration').addClass('lblLable');
                    $('#txtSystolic').addClass('lblLable');
                    $('#txtDiastolic').addClass('lblLable');
                    $('#txtGirth').addClass('lblLable');
                }
            });

            $('#chkCBC_ESR').change(function () {
                debugger;
                if ($('#chkCBC_ESR').is(':checked')) {

                    $('#txtHB').removeClass('lblLable');
                    $('#txtPCV').removeClass('lblLable');
                    $('#txtRBC').removeClass('lblLable');
                    $('#txtMCV').removeClass('lblLable');
                    $('#txtMCH').removeClass('lblLable');
                    $('#txtMCHC').removeClass('lblLable');
                    $('#txtWBC').removeClass('lblLable');
                    $('#txtNEUTROPHILS').removeClass('lblLable');
                    $('#txtLYMPHOCYTES').removeClass('lblLable');
                    $('#txtEOSINOPHILS').removeClass('lblLable');
                    $('#txtMONOCYTES').removeClass('lblLable');
                    $('#txtBASOPHILS').removeClass('lblLable');
                    $('#txtPLATELETCOUNT').removeClass('lblLable');
                    $('#txtESR').removeClass('lblLable');
                }
                else {
                    $('#txtHB').addClass('lblLable');
                    $('#txtPCV').addClass('lblLable');
                    $('#txtRBC').addClass('lblLable');
                    $('#txtMCV').addClass('lblLable');
                    $('#txtMCH').addClass('lblLable');
                    $('#txtMCHC').addClass('lblLable');
                    $('#txtWBC').addClass('lblLable');
                    $('#txtNEUTROPHILS').addClass('lblLable');
                    $('#txtLYMPHOCYTES').addClass('lblLable');
                    $('#txtEOSINOPHILS').addClass('lblLable');
                    $('#txtMONOCYTES').addClass('lblLable');
                    $('#txtBASOPHILS').addClass('lblLable');
                    $('#txtPLATELETCOUNT').addClass('lblLable');
                    $('#txtESR').addClass('lblLable');
                }
            })

            //$('#chkHBSAG').change(function () {
            //})

            //$('#chkHIV').change(function () {
            //})

            $('#chkFBS_RUA').change(function () {
                debugger;

                if ($('#chkFBS_RUA').is(':checked')) {
                    $('#txtFBS').removeClass('lblLable');
                    $('#txtHBA1C').removeClass('lblLable');
                    $('#txtRUA').removeClass('lblLable');
                    $('#ddlComment').removeClass('lblLable');
                }
                else {
                    $('#txtFBS').addClass('lblLable');
                    $('#txtHBA1C').addClass('lblLable');
                    $('#txtRUA').addClass('lblLable');
                    $('#ddlComment').removeClass('lblLable');
                }
            })

            //$('chkHBSAG').change(function () {
            //})

        });





    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="divMRFDetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="updMRFDetails" runat="server">
            <ContentTemplate>
                <div id="MRF_container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">MRF</h3>
                                    <i class="fa fa-pencil fa-fw" aria-hidden="true"></i>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkApplicationDtlsRefresh" OnClick="lnkApplicationDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkMRFDtls" CssClass="CheckSave" runat="server" ClientIDMode="Static" Text="Edit" EnableViewState="False" />
                                    <%--  <input id="chkMRFDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="MRF_containerBody" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorappdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorMRFDetailsBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Number</label>
                                    <asp:TextBox ID="txtAppno" CssClass="form-control validate lblLable" ClientIDMode="Static" ReadOnly="true" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Gender</label>
                                    <asp:TextBox ID="txtGender" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>DOB</label>
                                    <asp:TextBox ID="txtDOB" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>AGE</label>
                                    <asp:TextBox ID="txtAGE" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <%--  <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Agent Channel</label>
                                        <asp:TextBox ID="txtAgentChannel" CssClass="form-control lblLable" Text="" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Is Staff</label><br />
                                        <div class="col-right">
                                            <label class="lblLable">
                                                No<input type="checkbox" class="simple-switch-input " runat="server" id="hd_que_2" /><span class="simple-switch dark"></span>Yes
                                            </label>
                                        </div>
                                    </div>
                                </div>--%>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Height</label>
                                    <asp:TextBox ID="txtHeight" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Weight</label>
                                    <asp:TextBox ID="txtWeight" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>BMI</label>
                                    <asp:TextBox ID="txtBMI" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Pulse</label>
                                    <asp:TextBox ID="txtPulse" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Chest Inspiration</label>
                                    <asp:TextBox ID="txtChestInspiration" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Chest Expiration</label>
                                    <asp:TextBox ID="txtChestExpiration" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Systolic</label>
                                    <asp:TextBox ID="txtSystolic" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Diastolic</label>
                                    <asp:TextBox ID="txtDiastolic" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Girth</label>
                                    <asp:TextBox ID="txtGirth" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div id="divHTNCase" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Known Case of DM</label><br />
                                    <div class="col-right">
                                        <label class="lblHTNCase">
                                            No<input type="checkbox" class="simple-switch-input " runat="server" id="chkHTNCase" /><span class="simple-switch dark"></span>Yes
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div id="divDMCase" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Known Case of DM</label><br />
                                    <div class="col-right">
                                        <label class="lblDMCase">
                                            No<input type="checkbox" class="simple-switch-input " runat="server" id="chkDMCase" /><span class="simple-switch dark"></span>Yes
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div id="divSmoker" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Smoker</label><br />
                                    <div class="col-right">
                                        <label class="lblDMCase">
                                            No<input type="checkbox" class="simple-switch-input " runat="server" id="chkSmoker" /><span class="simple-switch dark"></span>Yes
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <div id="divAlcohol" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Alcohol</label><br />
                                    <div class="col-right">
                                        <label class="lblDMCase">
                                            No<input type="checkbox" class="simple-switch-input " runat="server" id="chkAlcohol" /><span class="simple-switch dark"></span>Yes
                                        </label>
                                    </div>
                                </div>
                            </div>

                            <%--end here--%>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Other Comments / Finding</label><br />
                                    <asp:TextBox ID="txtOtherComments" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control " Rows="5" Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkApplicationDtlsRefresh" />--%>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAppDtlsSave1" />
                <asp:AsyncPostBackTrigger ControlID="cbIsNsap" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="divCBC_ESR" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="CBC_ESR_Container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">CBC & ESR</h3>
                                    <i class="fa fa-pencil fa-fw" aria-hidden="true"></i>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkApplicationDtlsRefresh" OnClick="lnkApplicationDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkCBC_ESR" CssClass="CheckSave" ClientIDMode="Static" runat="server" Text="Edit" EnableViewState="False" />
                                    <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="CBC_ESR_ContainerBody" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorPath" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorESRDetailsBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>HB</label>
                                    <asp:TextBox ID="txtHB" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>PCV</label>
                                    <asp:TextBox ID="txtPCV" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>RBC</label>
                                    <asp:TextBox ID="txtRBC" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MCV</label>
                                    <asp:TextBox ID="txtMCV" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MCH</label>
                                    <asp:TextBox ID="txtMCH" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MCHC</label>
                                    <asp:TextBox ID="txtMCHC" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>WBC</label>
                                    <asp:TextBox ID="txtWBC" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>NEUTROPHILS</label>
                                    <asp:TextBox ID="txtNEUTROPHILS" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>LYMPHOCYTES</label>
                                    <asp:TextBox ID="txtLYMPHOCYTES" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>EOSINOPHILS</label>
                                    <asp:TextBox ID="txtEOSINOPHILS" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MONOCYTES</label>
                                    <asp:TextBox ID="txtMONOCYTES" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>BASOPHILS</label>
                                    <asp:TextBox ID="txtBASOPHILS" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>PLATELET COUNT</label>
                                    <asp:TextBox ID="txtPLATELETCOUNT" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>ESR</label>
                                    <asp:TextBox ID="txtESR" CssClass="form-control lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>


                </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkApplicationDtlsRefresh" />--%>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAppDtlsSave1" />
                <asp:AsyncPostBackTrigger ControlID="cbIsNsap" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="divHBSAG" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="HBSAG_Container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">HBSAG</h3>
                                    <i class="fa fa-pencil fa-fw" aria-hidden="true"></i>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkApplicationDtlsRefresh" OnClick="lnkApplicationDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkHBSAG" CssClass="CheckSave" runat="server" Text="Edit" EnableViewState="False" />
                                    <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="HBSAG_ContainerBody" class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkHBSAG_NA" runat="server" Text="NA" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkHBSAG_POSITITVE" runat="server" Text="POSITIVE" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkHBSAG_NEGATIVE" runat="server" Text="NEGATIVE" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkApplicationDtlsRefresh" />--%>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAppDtlsSave1" />
                <asp:AsyncPostBackTrigger ControlID="cbIsNsap" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="divHIV" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div id="HIV_Container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">HIV</h3>
                                    <i class="fa fa-pencil fa-fw" aria-hidden="true"></i>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkApplicationDtlsRefresh" OnClick="lnkApplicationDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkHIV" CssClass="CheckSave" runat="server" Text="Edit" EnableViewState="False" />
                                    <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="HIV_ContainerBody" class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkhiv_NA" runat="server" Text="NA" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkhiv_POSITIVE" runat="server" Text="POSITIVE" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkhiv_NEGATIVE" runat="server" Text="NEGATIVE" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkApplicationDtlsRefresh" />--%>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAppDtlsSave1" />
                <asp:AsyncPostBackTrigger ControlID="cbIsNsap" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="divFBS_RUA" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div id="FBS_RUA_Container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">FBS, RUA</h3>
                                    <i class="fa fa-pencil fa-fw" aria-hidden="true"></i>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkApplicationDtlsRefresh" OnClick="lnkApplicationDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkFBS_RUA" CssClass="CheckSave" ClientIDMode="Static" runat="server" Text="Edit" EnableViewState="False" />
                                    <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="FBS_RUA_ContainerBody" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>FBS</label>
                                    <asp:TextBox ID="txtFBS" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>HBA1C</label>
                                    <asp:TextBox ID="txtHBA1C" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>RUA</label>
                                    <asp:TextBox ID="txtRUA" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3 form-group">
                                <label>RUA</label>
                                <asp:DropDownList ID="ddlComment" CssClass="form-control validate lblLable" ClientIDMode="Static" runat="server">
                                    <asp:ListItem Value="General">General Analysis</asp:ListItem>
                                    <asp:ListItem Value="Personal">Personal Analysis</asp:ListItem>
                                    <asp:ListItem Value="Medical">Medical Analysis</asp:ListItem>
                                    <asp:ListItem Value="Financial">Financial Analysis</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkApplicationDtlsRefresh" />--%>
                <%-- <asp:AsyncPostBackTrigger ControlID="btnAppDtlsSave1" />
                <asp:AsyncPostBackTrigger ControlID="cbIsNsap" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div>
        <asp:Button runat="server" ID="btnSave" OnClientClick="addDataByApi();" Text="Save" />
    </div>
</asp:Content>

