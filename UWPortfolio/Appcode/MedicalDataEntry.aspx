<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalDataEntry.aspx.cs" Inherits="Appcode_MedicalDataEntry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Medical Data Entry</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />


    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link href="../plugins/jQuery/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="../dist/js/html5shiv.min.js"></script>
    <script src="../dist/js/respond.min.js"></script>
    <link href="../dist/css/styles-app.css" rel="stylesheet" />
    <link href="../plugins/select2/select2.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />--%>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <%--<script src="jquery-ui/ui/i18n/datepicker-de.js"></script>--%>

    <script src="../plugins/jQuery/jquery-ui.js"></script>
    <script src="../plugins/jQuery/datepicker.js"></script>

    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />


    <style type="text/css">
        .lblLable {
            background-color: #E5E5E5;
            /*font-weight: bold;*/
            pointer-events: none;
        }

        .CheckSave {
        }

        html, body {
            font-family: Verdana,sans-serif;
            font-size: 10px;
            line-height: 18px;
        }
    </style>

    <script type="text/javascript">

        var CheckGender = "";

        function btnCollapse() {
            $('.btn-box-tool').click(function () {
                if ($(this).find('i').hasClass('fa-plus')) {
                    $(this).find('i').removeClass('fa-plus').addClass('fa-minus');
                } else {
                    $(this).find('i').removeClass('fa-minus').addClass('fa-plus');
                }
                $(this).parents('.HideControl').find('.box-body').slideToggle().parents('.HideControl').siblings().find('.box-body').slideUp();
                $(this).parents('.HideControl').siblings().find('.btn-box-tool i').removeClass('fa-minus').addClass('fa-plus');
            });
        };

        function OnlyNumeric() {
            // Allow only Numeric data.
            if ($("html").hasClass("no-touch")) {
                $(document).on('keypress', '.Numeric', function (e) {
                    var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                    return ((keycode == 8 || keycode == 9) || (keycode >= 48 && keycode <= 57));
                });
            } else {
                $(document).on('keyup', '.Numeric', function (e) {

                    this.value = this.value.replace(/[^\d\.\-]/g, '');
                });
            }
        }

        function DatePickerToTextbox() {
            $("#txtDOB").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                numberOfMonth: 2,
                //minDate: new Date(1950, 1, 1),
                yearRange: '1950:2025',
                onSelect: function (value, ui) {
                    debugger;
                    var today = new Date();
                    age = today.getFullYear() - ui.selectedYear;
                    $('#txtAGE').val(age);
                    var response = Appcode_MedicalDataEntry.ComparePersonalInfo();
                    var InfoData = JSON.stringify(response);
                    var CheckDOB = (JSON.parse(InfoData).value["Rows"][0].CLT_STRDOB_CLTDOBX);
                    var InputDOB = $('#txtDOB').val();
                    $('#lblCompareDOB').empty();
                    $('#lblCompareAge').empty();
                    if (CheckDOB != InputDOB) {
                        $("#lblCompareDOB").text("DOB mismatch")
                        $("#lblCompareAge").text("Age is Different")
                    }
                }
            });
            $("#txtDateOfTest").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                numberOfMonth: 2,
            });

            $("#dtTestMedication").datepicker({
                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                numberOfMonth: 2,
            });
        }

        function callChkSmokerFun() {
            debugger;
            var isSmoker = $('#chkSmoker').is(':checked') ? true : false;
            $.ajax({
                type: "post",
                url: "MedicalDataEntry.aspx/CheckIsSmoker",
                data: '{"ismokerval":"' + isSmoker + '"}',
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function () {
                    //alert('done!');
                },
                error: function () {
                    //alert('Not Done!');
                }
            })
        }

        function addDataByApi() {
            //alert('hi');
            var clsMedicalData = new Object();
            //debugger;
            var clsHeader = new Object();
            //debugger;
            clsHeader.AppNo = $("#txtAppno").val();
            clsHeader.PolicyNo = "PC00004";
            clsHeader.TpaRcfNo = "TPA_RCF00004";
            clsHeader.TokenId = "0101";
            clsHeader.ApplicationSource = "Application";
            clsHeader.TpaID = "TPA00001";

            var clsMRF = new Object();
            //AppNo = $("#txtAppNo").val();
            //clsMRF.Gender = $("#txtGender").val();
            clsMRF.Gender = $('#ddlGender option:selected').val();
            clsMRF.DOB = $("#txtDOB").val();
            clsMRF.AGE = $("#txtAGE").val();
            clsMRF.Height = $("#txtHeight").val();
            clsMRF.Weight = $("#txtWeight").val();
            clsMRF.BMI = $("#txtBMI").val();
            clsMRF.Pulse = $("#txtPulse").val();
            clsMRF.ChestInspiration = $("#txtPulse").val();
            clsMRF.ChestExpiration = $("#txtChestInspiration").val();
            clsMRF.Systolic1 = $("#txtSystolic").val();
            clsMRF.Systolic2 = $("#txtSystolic1").val();
            clsMRF.Systolic3 = $("#txtSystolic2").val();
            clsMRF.Diastolic1 = $("#txtDiastolic").val();
            clsMRF.Diastolic2 = $("#txtDiastolic2").val();
            clsMRF.Diastolic3 = $("#txtDiastolic2").val();
            clsMRF.Girth = $("#txtGirth").val();
            if ($('#chkHTNCase').is(':checked')) {
                clsMRF.CaseHTN = 1;
            }
            else {
                clsMRF.CaseHTN = 0;
            }
            clsMRF.CaseHTNComments = $("#txtHTNComments").val();

            if ($('#chkDMCase').is(':checked')) {
                clsMRF.caseDM = 1;
            }
            else {
                clsMRF.caseDM = 0;
            }
            clsMRF.CaseDMComments = $("#txtDMComments").val();

            if ($('#chkSmoker').is(':checked')) {
                clsMRF.Smoker = 1;
            }
            else {
                clsMRF.Smoker = 0;
                $("#hdnValue").val(clsMRF.Smoker);

            }
            clsMRF.SmokerCommnets = $("#txtSmokerComments").val();

            if ($('#chkAlcohol').is(':checked')) {
                clsMRF.Alcohol = 1;
            }
            else {
                clsMRF.Alcohol = 0;
            }
            clsMRF.AlcoholComments = $("#txtAlcoholComments").val();

            clsMRF.Comments = $("#txtOtherComments").val();


            var clsCbcEsr = new Object();

            clsCbcEsr.HB = $("#txtHB").val();
            clsCbcEsr.PCV = $("#txtPCV").val();
            clsCbcEsr.RBC = $("#txtRBC").val();
            clsCbcEsr.MCV = $("#txtMCV").val();
            clsCbcEsr.MCH = $("#txtMCH").val();
            clsCbcEsr.MCHC = $("#txtMCHC").val();
            clsCbcEsr.WBC = $("#txtWBC").val();
            clsCbcEsr.NEUTROPHILS = $("#txtNEUTROPHILS").val();
            clsCbcEsr.LYMPHOCYTES = $("#txtLYMPHOCYTES").val();
            clsCbcEsr.EOSINOPHILS = $("#txtEOSINOPHILS").val();
            clsCbcEsr.MONOCYTES = $("#txtMONOCYTES").val();
            clsCbcEsr.BASOPHILS = $("#txtBASOPHILS").val();
            clsCbcEsr.PLATELETCOUNT = $("#txtPLATELETCOUNT").val();
            clsCbcEsr.ESR = $("#txtESR").val();
            //alert('hi1');
            var clsHBSAG = new Object();

            if ($('#chkHBSAG_NA').is(':checked')) {
                clsHBSAG.NA = 1;
            }
            else {
                clsHBSAG.NA = 0;
            }
            if ($('#chkHBSAG_POSITITVE').is(':checked')) {
                clsHBSAG.Positive = 1;

            }
            else {
                clsHBSAG.Positive = 0;
            }
            if ($('#chkHBSAG_NEGATIVE').is(':checked')) {
                clsHBSAG.Negative = 1;
            }
            else {
                clsHBSAG.Negative = 0;
            }


            var clsHIV = new Object();

            if ($('#chkhiv_NA').is(':checked')) {
                clsHIV.NA = 1;
            }
            else {
                clsHIV.NA = 0;
            }
            if ($('#chkhiv_POSITIVE').is(':checked')) {
                clsHIV.Positive = 1;
                $('#lblValidateHIV').empty();
                $('#lblValidateHIV').text('Decline');
                lblValidateHIV
            }
            else {
                clsHIV.Positive = 0;
            }
            if ($('#chkhiv_NEGATIVE').is(':checked')) {
                clsHIV.Negative = 1;
            }
            else {
                clsHIV.Negative = 0;
            }


            var clsFBS_RUA = new Object();
            //alert('hi2');
            clsFBS_RUA.FBS = $("#txtDuration").val();
            clsFBS_RUA.FBS = $("#txtFBS").val();
            clsFBS_RUA.HBA1C = $("#txtHBA1C").val();
            //clsFBS_RUA.RBC = $('#ddlRBC option:selected').val().trim();
            clsFBS_RUA.Albumin = $('#ddl_ALBUMIN option:selected').val();
            clsFBS_RUA.Sugar = $('#ddlSUGAR option:selected').val();
            //clsFBS_RUA.PucCells = $('#ddlPUC option:selected').val();
            clsFBS_RUA.Others = $('#ddlOthers option:selected').val();
            clsFBS_RUA.RBC_FBS = $("#txtRBC_FBS").val();
            clsFBS_RUA.PUS = $("#txtPUS").val();

            var clsLftTest = new Object();

            clsLftTest.SGOT = $("#txtSGOT").val();
            clsLftTest.SGPT = $("#txtSGPT").val();
            clsLftTest.GGT = $("#txtGGT").val();
            clsLftTest.Billirubin1 = $("#txtBILLIRUBIN").val();
            clsLftTest.Billirubin2 = $("#txtBILLIRUBIN2").val();
            clsLftTest.TotalBillirubin = $("#txtTotalBILLIRUBIN").val();
            clsLftTest.ALP = $("#txtALP").val();
            clsLftTest.S_Globilin = $("#txtS_GLOBILIN").val();
            clsLftTest.S_Albumin = $("#txtS_ALBUMIN").val();
            clsLftTest.TotalProtein = $("#txtTOTAL_PROTEIN").val();
            clsLftTest.AGRatio = $("#txtAgRatio").val();

            var clsLipidsTest = new Object();

            clsLipidsTest.Cholestrol = $("#txtCHOLESTEROL").val();
            clsLipidsTest.HDL = $("#txtHDL").val();
            clsLipidsTest.Triglyceride = $("#txtTRIGLYCERIDE").val();
            clsLipidsTest.HdlRatio = $("#txtTcRatio").val();

            var clsRftTest = new Object();

            clsRftTest.S_Creatine = $("#txtS_CREATININE").val();
            clsRftTest.UricAcid = $("#txtURIC_ACID").val();
            clsRftTest.Bun = $("#txtBUN").val();

            var clsECGTest = new Object();

            clsECGTest.DateOfECGTest = $("#txtDateOfTest").val();
            clsECGTest.ECGDecision = $("#txtECGDecision").val();
            clsECGTest.CmoDecisionECG = $("#txtCMODecisionECG").val();

            var clsCtmtTest = new Object();

            clsCtmtTest.DateOfCtmtTest = $("#dtTestMedication").val();
            clsCtmtTest.Medication = $("#txtMedication").val();
            clsCtmtTest.ExerciseTime = $("#txtExerciseTime").val();
            clsCtmtTest.WorkLoad = $("#txtWorkLoad").val();
            clsCtmtTest.BP = $("#txtBP").val();
            clsCtmtTest.THR = $("#txtTHR").val();
            clsCtmtTest.TerminationReason = $("#txtResonTermination").val();
            clsCtmtTest.Decision = $("#txtCTMTDecision").val();
            clsCtmtTest.CmoDecisionCTMT = $("#txtCMODecisionCTMT").val();
            


            clsMedicalData._strclsHeader = clsHeader;
            clsMedicalData._strMrfData = clsMRF;
            clsMedicalData._strCbcEsr = clsCbcEsr;
            clsMedicalData._strHBSAG = clsHBSAG;
            clsMedicalData._strHIV = clsHIV;
            clsMedicalData._strFBS = clsFBS_RUA;
            clsMedicalData._strLftTest = clsLftTest;
            clsMedicalData._strLipidsTest = clsLipidsTest;
            clsMedicalData._strRftTest = clsRftTest;
            clsMedicalData._strECGTest = clsECGTest;
            clsMedicalData._strCtmtTest = clsCtmtTest;
            //alert('hi3');

            //$.post(
            // "http://localhost:22503/api/MedicalData/MedicalDataEntry",
            //     clsMedicalData,
            //    function (data) {
            //        alert('hi');
            //    }).done(function () {
            //        alert("second success");
            //    })
            //    .fail(function (data) {
            //        alert("error" + JSON.parse(data));
            //    });



            debugger;
            $.ajax({
                type: "POST",
                dataType: "json",
                url: "http://10.1.41.185/MedicalDataEntry/api/MedicalData/MedicalDataEntry",
                //url: "http://localhost:22503/api/MedicalData/MedicalDataEntry",
                //contentType: "application/json; charset=utf-8",
                data: clsMedicalData,
                cache: false,
                success: function (response) {
                    alert('Data Save Successfully');
                    window.top.close();
                    alert(response);
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger;
                    try {
                        //alert('fail');
                        // var err = JSON.parse(xhr.responseText);
                        // alert(err.Message);
                        //alert(textStatus);
                        //alert(errorThrown);
                    } catch (ex) {
                        alert('exception' + ex);
                    }
                }
            });

        }

        function displayTextbox() {
            //debugger;
            $('#chkHTNCase').change(function () {
                //debugger;
                $('#lblHTNError').empty();
                if ($('#chkHTNCase').is(':checked')) {
                    $('#caseHTNComments').show();
                    $('#lblHTNError').text("HTN rate accordingly");
                }
                else {
                    $('#caseHTNComments').hide();
                }
            });

            $('#chkDMCase').change(function () {
                //debugger;
                $('#lblDMError').empty();
                if ($('#chkDMCase').is(':checked')) {

                    $('#caseDmComments').show();
                    $('#lblDMError').text("DM rate accordingly");
                }
                else {
                    $('#caseDmComments').hide();
                }
            });

            $('#chkSmoker').change(function () {
                //debugger;
                $('#lblSmokerWarning').empty();
                if ($('#chkSmoker').is(':checked')) {
                    $('#SmokerComments').show();
                    $('#lblSmokerWarning').text("Smoker rates applicable");
                }
                else {
                    $('#SmokerComments').hide();
                }
            });

            $('#chkAlcohol').change(function () {
                //debugger;
                $('#lblAlcoholWarning').empty();
                if ($('#chkAlcohol').is(':checked')) {

                    $('#AlcoholComments').show();
                    //$('#lblAlcoholWarning').text("Alcohol rates applicable");
                }
                else {
                    $('#AlcoholComments').hide();
                }
            });

            $('#HBSAG_ContainerBody').change(function () {
                if ($('#chkHBSAG_POSITITVE').is(':checked')) {
                    $('#lblValidateHBSAG').empty();
                    $('#lblValidateHBSAG').text('Hbsage poistive need to call HBEAG');
                    $('#lblValidateHBSAG').show();
                }
                if ($('#chkHBSAG_NA').is(':checked')) {
                    $('#lblValidateHBSAG').hide();
                }
                if ($('#chkHBSAG_NEGATIVE').is(':checked')) {
                    $('#lblValidateHBSAG').hide();
                }
            });

            $('#HIV_ContainerBody').change(function () {
                if ($('#chkhiv_POSITIVE').is(':checked')) {
                    $('#lblValidateHIV').empty();
                    $('#lblValidateHIV').text('Decline');
                    $('#lblValidateHIV').show();
                }
                if ($('#chkhiv_NA').is(':checked')) {
                    $('#lblValidateHIV').hide();
                }
                if ($('#chkhiv_NEGATIVE').is(':checked')) {
                    $('#lblValidateHIV').hide();
                }
            });
        }

        $(document).ready(function () {
            debugger;
            btnCollapse();
            OnlyNumeric();
            displayTextbox();
            DatePickerToTextbox();
            FBS_RUA_Validation();
            //callChkSmokerFun();
            //$("#form1").submit(function (evt) {
            //    evt.preventDefault();
            //});

            $("#btnSaveMedical").click(function () {
                debugger;
                callChkSmokerFun();
                SavingAllValidationMsg();
                addDataByApi();
                //alert('Api Called');
                //setTimeout(addDataByApi(), 3000);
                //window.top.close();
                //$(window).unload(function () {
                //    alert('Data Save Succsessfully');
                //});
                //Attributes.Add("OnClick", "window.close();");
                //e.preventDefault();
            });

            //});

            $('#chkMRFDtls').change(function () {
                ////debugger;
                if ($('#chkMRFDtls').is(':checked')) {

                    //$('#txtAppno').removeClass('lblLable');
                    $('#ddlGender').removeClass('lblLable');
                    $('#txtDOB').removeClass('lblLable');
                    $('#txtAGE').removeClass('lblLable');
                    $('#txtHeight').removeClass('lblLable');
                    $('#txtWeight').removeClass('lblLable');
                    $('#txtBMI').removeClass('lblLable');
                    $('#txtPulse').removeClass('lblLable');
                    $('#txtChestInspiration').removeClass('lblLable');
                    $('#txtChestExpiration').removeClass('lblLable');
                    $('#txtSystolic').removeClass('lblLable');
                    $('#txtSystolic1').removeClass('lblLable');
                    $('#txtSystolic2').removeClass('lblLable');
                    $('#txtDiastolic').removeClass('lblLable');
                    $('#txtDiastolic1').removeClass('lblLable');
                    $('#txtDiastolic2').removeClass('lblLable');
                    $('#txtGirth').removeClass('lblLable');
                    //$('#chkHTNCase').removeClass('lblLable');
                    $('#txtHTNComments').removeClass('lblLable');
                    $('#txtDMComments').removeClass('lblLable');
                    $('#txtSmokerComments').removeClass('lblLable');
                    $('#txtAlcoholComments').removeClass('lblLable');
                    $('#txtOtherComments').removeClass('lblLable');
                }
                else {
                    //$('#txtAppno').addClass('lblLable');
                    $('#ddlGender').addClass('lblLable');
                    $('#txtDOB').addClass('lblLable');
                    $('#txtAGE').addClass('lblLable');
                    $('#txtHeight').addClass('lblLable');
                    $('#txtWeight').addClass('lblLable');
                    $('#txtBMI').addClass('lblLable');
                    $('#txtPulse').addClass('lblLable');
                    $('#txtChestInspiration').addClass('lblLable');
                    $('#txtChestExpiration').addClass('lblLable');
                    $('#txtSystolic').addClass('lblLable');
                    $('#txtSystolic1').addClass('lblLable');
                    $('#txtSystolic2').addClass('lblLable');
                    $('#txtDiastolic').addClass('lblLable');
                    $('#txtDiastolic1').addClass('lblLable');
                    $('#txtDiastolic2').addClass('lblLable');
                    $('#txtGirth').addClass('lblLable');
                    //$('#chkHTNCase').addClass('lblLable');
                    $('#txtHTNComments').addClass('lblLable');
                    $('#txtDMComments').addClass('lblLable');
                    $('#txtSmokerComments').addClass('lblLable');
                    $('#txtAlcoholComments').addClass('lblLable');
                    $('#txtOtherComments').addClass('lblLable');
                }
            });

            $('#chkCBC_ESR').change(function () {
                //debugger;
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

            $('input[name="HBSG"]').attr('disabled', 'disabled');

            $('#chkHBSAG').change(function () {
                if ($('#chkHBSAG').is(':checked')) {
                    $('input[name="HBSG"]').removeAttr('disabled', 'disabled');
                }
                else {
                    $('input[name="HBSG"]').attr('disabled', 'disabled');
                }
            })

            $('input[name="HIV"]').attr('disabled', 'disabled');

            $('#chkHIV').change(function () {
                if ($('#chkHIV').is(':checked')) {
                    $('input[name="HIV"]').removeAttr('disabled', 'disabled');
                }
                else {
                    $('input[name="HIV"]').attr('disabled', 'disabled');
                }
            })

            $('#chkFBS_RUA').change(function () {
                debugger;
                
                if ($('#chkFBS_RUA').is(':checked')) {
                    $('#txtDuration').removeClass('lblLable');
                    $('#txtFBS').removeClass('lblLable');
                    $('#txtHBA1C').removeClass('lblLable');
                    //$('#ddlRBC').removeClass('lblLable');
                    $('#txtRBC_FBS').removeClass('lblLable');
                    $('#ddl_ALBUMIN').removeClass('lblLable');
                    $('#ddlSUGAR').removeClass('lblLable');
                    //$('#ddlPUC').removeClass('lblLable');
                    $('#txtPUS').removeClass('lblLable');
                    $('#ddlOthers').removeClass('lblLable');
                }
                else {
                    $('#txtDuration').addClass('lblLable');
                    $('#txtFBS').addClass('lblLable');
                    $('#txtHBA1C').addClass('lblLable');
                    //$('#ddlRBC').addClass('lblLable');
                    $('#txtRBC_FBS').addClass('lblLable');
                    $('#ddl_ALBUMIN').addClass('lblLable');
                    $('#ddlSUGAR').addClass('lblLable');
                    //$('#ddlPUC').addClass('lblLable');
                    $('#txtPUS').addClass('lblLable');
                    $('#ddlOthers').addClass('lblLable');
                }
            })

            $('#chkLFT').change(function () {
                //debugger;

                if ($('#chkLFT').is(':checked')) {
                    $('#txtSGOT').removeClass('lblLable');
                    $('#txtSGPT').removeClass('lblLable');
                    $('#txtGGT').removeClass('lblLable');
                    $('#txtBILLIRUBIN').removeClass('lblLable');
                    $('#txtBILLIRUBIN2').removeClass('lblLable');
                    $('#txtALP').removeClass('lblLable');
                    $('#txtS_GLOBILIN').removeClass('lblLable');
                    $('#txtS_ALBUMIN').removeClass('lblLable');
                    $('#txtTOTAL_PROTEIN').removeClass('lblLable');
                    $('#txtAgRatio').removeClass('lblLable');
                }
                else {
                    $('#txtSGOT').addClass('lblLable');
                    $('#txtSGPT').addClass('lblLable');
                    $('#txtGGT').addClass('lblLable');
                    $('#txtBILLIRUBIN').addClass('lblLable');
                    $('#txtBILLIRUBIN2').addClass('lblLable');
                    $('#txtALP').addClass('lblLable');
                    $('#txtS_GLOBILIN').addClass('lblLable');
                    $('#txtS_ALBUMIN').addClass('lblLable');
                    $('#txtTOTAL_PROTEIN').addClass('lblLable');
                    $('#txtAgRatio').addClass('lblLable');
                }
            })

            $('#chkLIPIDS').change(function () {
                //debugger;

                if ($('#chkLIPIDS').is(':checked')) {
                    $('#txtCHOLESTEROL').removeClass('lblLable');
                    $('#txtHDL').removeClass('lblLable');
                    $('#txtTRIGLYCERIDE').removeClass('lblLable');
                    $('#txtTcRatio').removeClass('lblLable');
                }
                else {
                    $('#txtCHOLESTEROL').addClass('lblLable');
                    $('#txtHDL').addClass('lblLable');
                    $('#txtTRIGLYCERIDE').addClass('lblLable');
                    $('#txtTcRatio').addClass('lblLable');
                }
            })

            $('#chkRFT').change(function () {
                //debugger;

                if ($('#chkRFT').is(':checked')) {
                    $('#txtS_CREATININE').removeClass('lblLable');
                    $('#txtURIC_ACID').removeClass('lblLable');
                    $('#txtBUN').removeClass('lblLable');
                }
                else {
                    $('#txtS_CREATININE').addClass('lblLable');
                    $('#txtURIC_ACID').addClass('lblLable');
                    $('#txtBUN').addClass('lblLable');
                }
            })

            $('#chkECG').change(function () {
                if ($('#chkECG').is(':checked')) {
                    $('#txtDateOfTest').removeClass('lblLable');
                    $('#txtECGDecision').removeClass('lblLable');
                    $('#txtCMODecisionECG').removeClass('lblLable');
                }
                else {
                    $('#txtDateOfTest').addClass('lblLable');
                    $('#txtECGDecision').addClass('lblLable');
                    $('#txtCMODecisionECG').addClass('lblLable');

                }
            })

            $('#chkCTMTC').change(function () {
                if ($('#chkCTMTC').is(':checked')) {
                    $('#dtTestMedication').removeClass('lblLable');
                    $('#txtMedication').removeClass('lblLable');
                    $('#txtExerciseTime').removeClass('lblLable');
                    $('#txtWorkLoad').removeClass('lblLable');
                    $('#txtBP').removeClass('lblLable');
                    $('#txtTHR').removeClass('lblLable');
                    $('#txtResonTermination').removeClass('lblLable');
                    $('#txtCTMTDecision').removeClass('lblLable');
                    $('#txtCMODecisionCTMT').removeClass('lblLable');
                }
                else {
                    $('#dtTestMedication').addClass('lblLable');
                    $('#txtMedication').addClass('lblLable');
                    $('#txtExerciseTime').addClass('lblLable');
                    $('#txtWorkLoad').addClass('lblLable');
                    $('#txtBP').addClass('lblLable');
                    $('#txtTHR').addClass('lblLable');
                    $('#txtResonTermination').addClass('lblLable');
                    $('#txtCTMTDecision').addClass('lblLable');
                    $('#txtCMODecisionCTMT').addClass('lblLable');
                }
            })
        });
        //Calculating and validation of BMI
        function GetBMI() {
            debugger;
            var weight = parseFloat($("#txtWeight").val());
            var height = parseFloat($("#txtHeight").val());

            if (isNaN(weight) || isNaN(height)) {
                $("#lblBmiResult").text("please enter height and weight properly");
            }
            else {
                var bmi = weight / (height / 100 * height / 100);
                bmi = Math.round(bmi);
                $("#txtBMI").val(bmi);
                BmiResults();
            }
        }
        function BmiResults() {
            debugger;
            var age = $("#txtAGE").val();
            var bmi = $("#txtBMI").val();

            var response = Appcode_MedicalDataEntry.BmiReports(age, bmi);
            var BMIResponse = JSON.stringify(response);
            $("#lblBmiResult").text(JSON.parse(BMIResponse).value);
        }
        //End Here

        //Calculated Total BILLIRUBIN
        function GetTotalBILLIRUBIN() {
            var DirectBILLIRUBIN = parseFloat($("#txtBILLIRUBIN").val());
            var IndirectBILLIRUBIN = parseFloat($("#txtBILLIRUBIN2").val());

            if (DirectBILLIRUBIN == "" || IndirectBILLIRUBIN == "") {
                $('#lblValidateTotalBILLIRUBIN').text('Please Enter Proper Value of DirectBILLIRUBIN And IndirectBILLIRUBIN');
            }
            else {
                var TotalBILLIRUBIN = (DirectBILLIRUBIN + IndirectBILLIRUBIN);
                calTotalBILLIRUBIN = TotalBILLIRUBIN.toFixed(2);
                $('#txtTotalBILLIRUBIN').val(calTotalBILLIRUBIN);
            }
        }
        //end here


        function ValidateFBS() {
            debugger;
            $('#lblValidateFBS').empty();
            $("#lblValidateHBA1C").empty();
            if ($("#txtFBS").val() > 110) {
                $('#lblValidateFBS').text('Suggestive Decision - Values not in range');
            }

            if ($('#txtHBA1C').val() != "") {
                var Hba1c = $('#txtHBA1C').val();
                var duration = $('#txtDuration').val();
                var age = $('#txtAGE').val();
                if (Hba1c < 6 && duration == "") {
                    return false;
                }

                if (duration == "") {
                    duration = 0;
                }
                var response = Appcode_MedicalDataEntry.Hba1cValidation(age, Hba1c, duration);
                var Hba1cValidateResponse = JSON.stringify(response);
                if (JSON.parse(Hba1cValidateResponse).value == "" || JSON.parse(Hba1cValidateResponse).value == null) {
                    $("#lblValidateHBA1C").empty();
                    return false;
                }
                $("#lblValidateHBA1C").empty();
                $("#lblValidateHBA1C").text(JSON.parse(Hba1cValidateResponse).value);
            }
        }

        //calculating TC/HDl Ratio Base on Cholestrol and  HDl
        function GetTc_HdlRatio() {
            debugger;
             var age = $("#txtAGE").val();
            var Cholestrol = $("#txtCHOLESTEROL").val();
            var HDL = $("#txtHDL").val();            
            if (Cholestrol == "" || HDL == "") {
                document.getElementById("<%=txtTcRatio.ClientID %>").value = "";
                //$("#txtTcRatio").text
                $("#lblValidateHDL_RATIO").empty();
                return false;
            }
            else {
            var Result_Tc_HdlRatio = Cholestrol / HDL;
            FinalResult_Tc_HdlRatio = Result_Tc_HdlRatio.toFixed(2);
            $('#txtTcRatio').val(FinalResult_Tc_HdlRatio);
            var response = Appcode_MedicalDataEntry.CholestrolReports(age, Result_Tc_HdlRatio);
            var CholestrolResponse = JSON.stringify(response);
            if (JSON.parse(CholestrolResponse).value == "") {
                $("#lblValidateHDL_RATIO").empty();
                return false;
            }
            $("#lblValidateHDL_RATIO").empty();
            $("#lblValidateHDL_RATIO").text(JSON.parse(CholestrolResponse).value);
            }
            }
        //End Here

        //validation for Application No, Gender and DOB
        function ComparePersonalInfo() {
            debugger;
            var response = Appcode_MedicalDataEntry.ComparePersonalInfo();
            var InfoData = JSON.stringify(response);
            CheckGender = (JSON.parse(InfoData).value["Rows"][0].CLT_GENDER_CLTSEX);
            var InputGender = $('#ddlGender option:selected').val();
            var InputGenderSub = InputGender.substring(0, 1);
            $('#lblCompareGender').empty();
            if (CheckGender != InputGenderSub) {
                $("#lblCompareGender").text("Gender mismatch");
            }
        }
        //validation for ChestInspiration and Expiration
        function ChestValidation() {
            $('#lblValidateChestIE').empty();
            var ChestInspiration = $('#txtChestInspiration').val();
            var ChestExpiration = $('#txtChestExpiration').val();
            var abc = ChestInspiration - ChestExpiration
            if (abc <= 2) {
                $('#lblValidateChestIE').text('Suggestive Decision - Value not in range')
            }
        }
        //END

        // Validation for FBS and RUA DropDownLists
        function FBS_RUA_Validation() {
            debugger;
            /*
            $('#ddlRBC').change(function () {
                debugger;
                var ddlRBC = $('#ddlRBC option:selected').val();
                if (ddlRBC == 'Select') {
                    $('#lblValidateDdlRBC').empty();
                }
                if (ddlRBC == '0-5') {
                    $('#lblValidateDdlRBC').empty();
                    $('#lblValidateDdlRBC').text('Suggestive Decision - Standard');
                }
                if (ddlRBC == '5-10 HPF') {
                    $('#lblValidateDdlRBC').empty();
                    $('#lblValidateDdlRBC').text('Suggestive Decision - Repeat RUA');
                }
                if (ddlRBC == '>10 HPF') {
                    $('#lblValidateDdlRBC').empty();
                    $('#lblValidateDdlRBC').text('Suggestive Decision - Postponed');
                }
            });*/

            $('#ddl_ALBUMIN').change(function () {
                debugger;
                var ddl_ALBUMIN = $('#ddl_ALBUMIN option:selected').val();
                if (ddl_ALBUMIN == 'Select') {
                    $('#lblValidateddl_ALBUMIN').empty();
                }
                if (ddl_ALBUMIN == 'Nil') {
                    $('#lblValidateddl_ALBUMIN').empty();
                    $('#lblValidateddl_ALBUMIN').text('Suggestive Decision - Standard');
                }
                if (ddl_ALBUMIN == 'Trace') {
                    $('#lblValidateddl_ALBUMIN').empty();
                    $('#lblValidateddl_ALBUMIN').text('Suggestive Decision - Standard');
                }
                if (ddl_ALBUMIN == '1+') {
                    $('#lblValidateddl_ALBUMIN').empty();
                    $('#lblValidateddl_ALBUMIN').text('Suggestive Decision - Repeat RUA / Postponed');
                }
                if (ddl_ALBUMIN == '2+') {
                    $('#lblValidateddl_ALBUMIN').empty();
                    $('#lblValidateddl_ALBUMIN').text('Suggestive Decision - Postponed');
                }
                if (ddl_ALBUMIN == '>2+') {
                    $('#lblValidateddl_ALBUMIN').empty();
                    $('#lblValidateddl_ALBUMIN').text('Suggestive Decision - Declined');
                }
            });

            $('#ddlSUGAR').change(function () {
                debugger;
                var ddlSUGAR = $('#ddlSUGAR option:selected').val();
                if (ddlSUGAR == 'Select') {
                    $('#lblValidateddlSUGAR').empty();
                }
                if (ddlSUGAR == 'Nil') {
                    $('#lblValidateddlSUGAR').empty();
                    $('#lblValidateddlSUGAR').text('Suggestive Decision - Standard');
                }
                if (ddlSUGAR == 'Trace') {
                    $('#lblValidateddlSUGAR').empty();
                    $('#lblValidateddlSUGAR').text('Suggestive Decision - Diabetes Q');
                }
                if (ddlSUGAR == '1+') {
                    $('#lblValidateddlSUGAR').empty();
                    $('#lblValidateddlSUGAR').text('Suggestive Decision - FBS & HbA1c & Diabetes Q');
                }
                if (ddlSUGAR == '2+') {
                    $('#lblValidateddlSUGAR').empty();
                    $('#lblValidateddlSUGAR').text('Suggestive Decision - FBS & HbA1c & Diabetes Q');
                }
                if (ddlSUGAR == '3+') {
                    $('#lblValidateddlSUGAR').empty();
                    $('#lblValidateddlSUGAR').text('Suggestive Decision - FBS & HbA1c & Diabetes Q');
                }
                if (ddlSUGAR == '4+') {
                    $('#lblValidateddlSUGAR').empty();
                    $('#lblValidateddlSUGAR').text('Suggestive Decision - Postponed');
                }
            });
            /*
            $('#ddlPUC').change(function () {
                debugger;
                var ddlPUC = $('#ddlPUC option:selected').val();
                if (ddlPUC == 'Select') {
                    $('#lblValidateddlPUC').empty();
                }
                if (ddlPUC == '0-10') {
                    $('#lblValidateddlPUC').empty();
                    $('#lblValidateddlPUC').text('Suggestive Decision - Standard');
                }
                if (ddlPUC == '>10 -20') {
                    $('#lblValidateddlPUC').empty();
                    $('#lblValidateddlPUC').text('Suggestive Decision - Repeat RUA');
                }
                if (ddlPUC == '> 20') {
                    $('#lblValidateddlPUC').empty();
                    $('#lblValidateddlPUC').text('Suggestive Decision - Postponed');
                }
            });
            */
            $('#ddlOthers').change(function () {
                debugger;
                var ddlOthers = $('#ddlOthers option:selected').val();
                if (ddlOthers == 'Select') {
                    $('#lblValidateddlOthers').empty();
                }
                if (ddlOthers == 'Nil') {
                    $('#lblValidateddlOthers').empty();
                    $('#lblValidateddlOthers').text('Suggestive Decision - Standard');
                }
                if (ddlOthers == 'Cast cells') {
                    $('#lblValidateddlOthers').empty();
                    $('#lblValidateddlOthers').text('Suggestive Decision - 0 std');
                }
                if (ddlOthers == 'Uric acid crystal') {
                    $('#lblValidateddlOthers').empty();
                    $('#lblValidateddlOthers').text('');
                }

                if (ddlOthers == 'Bacteria') {
                    $('#lblValidateddlOthers').empty();
                    $('#lblValidateddlOthers').text('Suggestive Decision - 10 std');
                }
            });
        }
        //End Here

        //validation for MRF textBox
        function FeildErrorWarning(InputTextbox) {
            debugger;
            //if ($("#" + InputTextbox).val() == '') {
            //    return false;
            //}

            ComparePersonalInfo();
            //var Gender = $('#ddlGender option:selected').val();
            var GenderSub = CheckGender.substring(0, 1);
            var InputTextboxID = InputTextbox;
            var objClsError = new Object();

            objClsError.GENDER = GenderSub;

            if (InputTextboxID == 'txtPulse') {
                objClsError.pulse = $('#txtPulse').val();
            }
            if (InputTextboxID == 'txtSystolic') {
                objClsError.systolic1 = $('#txtSystolic').val();
            }
            if (InputTextboxID == 'txtSystolic1') {
                objClsError.systolic2 = $('#txtSystolic1').val();
            }
            if (InputTextboxID == 'txtSystolic2') {
                objClsError.systolic3 = $('#txtSystolic2').val();
            }
            if (InputTextboxID == 'txtDiastolic') {
                objClsError.diastolic1 = $('#txtDiastolic').val();
            }
            if (InputTextboxID == 'txtDiastolic1') {
                objClsError.diastolic2 = $('#txtDiastolic1').val();
            }
            if (InputTextboxID == 'txtDiastolic2') {
                objClsError.diastolic3 = $('#txtDiastolic2').val();
            }
            if (InputTextboxID == 'txtHB') {
                objClsError.HB = $('#txtHB').val();
            }
            if (InputTextboxID == 'txtPCV') {
                objClsError.PCV = $('#txtPCV').val();
            }
            if (InputTextboxID == 'txtRBC') {
                objClsError.RBC = $('#txtRBC').val();
            }
            if (InputTextboxID == 'txtMCV') {
                objClsError.MCV = $('#txtMCV').val();
            }
            if (InputTextboxID == 'txtMCH') {
                objClsError.MCH = $('#txtMCH').val();
            }
            if (InputTextboxID == 'txtMCHC') {
                objClsError.MCHC = $('#txtMCHC').val();
            }
            if (InputTextboxID == 'txtWBC') {
                objClsError.WBC = $('#txtWBC').val();
            }
            if (InputTextboxID == 'txtNEUTROPHILS') {
                objClsError.NEUTROPHILS = $('#txtNEUTROPHILS').val();
            }
            if (InputTextboxID == 'txtLYMPHOCYTES') {
                objClsError.LYMPHOCYTES = $('#txtLYMPHOCYTES').val();
            }
            if (InputTextboxID == 'txtEOSINOPHILS') {
                objClsError.EOSINOPHILS = $('#txtEOSINOPHILS').val();
            }
            if (InputTextboxID == 'txtMONOCYTES') {
                objClsError.MONOCYTES = $('#txtMONOCYTES').val();
            }
            if (InputTextboxID == 'txtBASOPHILS') {
                objClsError.BASOPHILS = $('#txtBASOPHILS').val();
            }
            if (InputTextboxID == 'txtPLATELETCOUNT') {
                objClsError.PLATELET_COUNT = $('#txtPLATELETCOUNT').val();
            }
            if (InputTextboxID == 'txtESR') {
                objClsError.ESR = $('#txtESR').val();
            }
            if (InputTextboxID == 'txtSGOT') {
                objClsError.SGOT = $('#txtSGOT').val();
            }
            if (InputTextboxID == 'txtSGPT') {
                objClsError.SGPT = $('#txtSGPT').val();
            }
            if (InputTextboxID == 'txtGGT') {
                objClsError.GGT = $('#txtGGT').val();
            }
            if (InputTextboxID == 'txtBILLIRUBIN') {
                objClsError.Billirubin1 = $('#txtBILLIRUBIN').val();
            }
            if (InputTextboxID == 'txtBILLIRUBIN2') {
                objClsError.Billirubin2 = $('#txtBILLIRUBIN2').val();
            }
            if (InputTextboxID == 'txtALP') {
                objClsError.ALP = $('#txtALP').val();
            }
            if (InputTextboxID == 'txtS_GLOBILIN') {
                objClsError.S_Globilin = $('#txtS_GLOBILIN').val();
            }
            if (InputTextboxID == 'txtS_ALBUMIN') {
                objClsError.S_Albumin = $('#txtS_ALBUMIN').val();
            }
            if (InputTextboxID == 'txtTOTAL_PROTEIN') {
                objClsError.TotalProtein = $('#txtTOTAL_PROTEIN').val();
            }
            if (InputTextboxID == 'txtAgRatio') {
                objClsError.AGRatio = $('#txtAgRatio').val();
            }
            if (InputTextboxID == 'txtCHOLESTEROL') {
                objClsError.Cholestrol = $('#txtCHOLESTEROL').val();
            }
            if (InputTextboxID == 'txtHDL') {
                objClsError.HDL = $('#txtHDL').val();
            }
            if (InputTextboxID == 'txtTRIGLYCERIDE') {
                objClsError.Triglyceride = $('#txtTRIGLYCERIDE').val();
            }
            if (InputTextboxID == 'txtTcRatio') {
                objClsError.HdlRatio = $('#txtTcRatio').val();
            }
            if (InputTextboxID == 'txtS_CREATININE') {
                objClsError.S_Creatine = $('#txtS_CREATININE').val();
            }
            if (InputTextboxID == 'txtURIC_ACID') {
                objClsError.UricAcid = $('#txtURIC_ACID').val();
            }
            if (InputTextboxID == 'txtBUN') {
                objClsError.Bun = $('#txtBUN').val();
            }
            if (InputTextboxID == 'txtPUS') {
                objClsError.PUS = $('#txtPUS').val();
            }
            if (InputTextboxID == 'txtRBC_FBS') {
                objClsError.RBC_FBS = $('#txtRBC_FBS').val();
            }

            var response = new Object();
            response = Appcode_MedicalDataEntry.ValidateMedicalData(JSON.stringify(objClsError));
            var ErrorResponse = JSON.stringify(response);
            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'PULSE') {
                $('#lblValidatePulse').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtPulse') {
                $('#lblValidatePulse').empty();
            }
            //else if ($('#lblValidatePulse') != '') {
            //    $('#lblValidatePulse').empty();
            //}
            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'SYSTOLIC1') {
                $('#lblValidateSystolic1').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtSystolic') {
                $('#lblValidateSystolic1').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'SYSTOLIC2') {
                $('#lblValidateSystolic2').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtSystolic1') {
                $('#lblValidateSystolic2').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'SYSTOLIC3') {
                $('#lblValidateSystolic3').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtSystolic2') {
                $('#lblValidateSystolic3').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'DIASTOLIC1') {
                $('#lblValidateDiastolic1').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtDiastolic') {
                $('#lblValidateDiastolic1').empty();
            }
            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'DIASTOLIC2') {
                $('#lblValidateDiastolic2').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtDiastolic1') {
                $('#lblValidateDiastolic2').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'DIASTOLIC3') {
                $('#lblValidateDiastolic3').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtDiastolic2') {
                $('#lblValidateDiastolic3').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'HB') {
                $('#lblValidateHB').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtHB') {
                $('#lblValidateHB').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'PCV') {
                $('#lblValidatePCV').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtPCV') {
                $('#lblValidatePCV').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'RBC') {
                $('#lblValidateRBC').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtRBC') {
                $('#lblValidateRBC').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'MCV') {
                $('#lblValidateMCV').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtMCV') {
                $('#lblValidateMCV').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'MCH') {
                $('#lblValidateMCH').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtMCH') {
                $('#lblValidateMCH').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'MCHC') {
                $('#lblValidateMCHC').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtMCHC') {
                $('#lblValidateMCHC').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'WBC') {
                $('#lblValidateWBC').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtWBC') {
                $('#lblValidateWBC').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NEUTROPHILS') {
                $('#lblValidateNEUTROPHILS').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtNEUTROPHILS') {
                $('#lblValidateNEUTROPHILS').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'LYMPHOCYTES') {
                $('#lblValidateLYMPHOCYTES').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtLYMPHOCYTES') {
                $('#lblValidateLYMPHOCYTES').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'EOSINOPHILS') {
                $('#lblValidateEOSINOPHILS').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtEOSINOPHILS') {
                $('#lblValidateEOSINOPHILS').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'MONOCYTES') {
                $('#lblValidateMONOCYTES').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtMONOCYTES') {
                $('#lblValidateMONOCYTES').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'BASOPHILS') {
                $('#lblValidateBASOPHILS').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtBASOPHILS') {
                $('#lblValidateBASOPHILS').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'PLATELET') {
                $('#lblValidatePLATELETCOUNT').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtPLATELETCOUNT') {
                $('#lblValidatePLATELETCOUNT').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'ESR') {
                $('#lblValidateESR').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtESR') {
                $('#lblValidateESR').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'SGOT') {
                $('#lblValidateSGOT').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtSGOT') {
                $('#lblValidateSGOT').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'SGPT') {
                $('#lblValidateSGPT').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtSGPT') {
                $('#lblValidateSGPT').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'GGT') {
                $('#lblValidateGGT').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtGGT') {
                $('#lblValidateGGT').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'DIRECT_BILLIRUBIN') {
                $('#lblValidateBILLIRUBIN').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtBILLIRUBIN') {
                $('#lblValidateBILLIRUBIN').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'INDIRECT_BILLIRUBIN') {
                $('#lblValidateBILLIRUBIN1').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtBILLIRUBIN2') {
                $('#lblValidateBILLIRUBIN1').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'ALP') {
                $('#lblValidateALP').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtALP') {
                $('#lblValidateALP').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'S_GLOBILIN') {
                $('#lblValidateS_GLOBILIN').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtS_GLOBILIN') {
                $('#lblValidateS_GLOBILIN').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'S_ALBUMIN') {
                $('#lblValidateS_ALBUMIN').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtS_ALBUMIN') {
                $('#lblValidateS_ALBUMIN').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'TOTALPROTEIN') {
                $('#lblValidateT_PROTEIN').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtTOTAL_PROTEIN') {
                $('#lblValidateT_PROTEIN').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'AGRATIO') {
                $('#lblValidateAGRATIO').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtAgRatio') {
                $('#lblValidateAGRATIO').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'CHOLESTROL') {
                $('#lblValidateCHOLESTEROL').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtCHOLESTEROL') {
                $('#lblValidateCHOLESTEROL').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'HDL') {
                $('#lblValidateHDL').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtHDL') {
                $('#lblValidateHDL').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'TRIGLYCERIDE') {
                $('#lblValidateTRIGYCERIDE').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtTRIGLYCERIDE') {
                $('#lblValidateTRIGYCERIDE').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'S_CREATINE') {
                $('#lblValidateS_CREATINE').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtS_CREATININE') {
                $('#lblValidateS_CREATINE').empty();
            }

            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'URICACID') {
                $('#lblValidateURIC_ACID').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtURIC_ACID') {
                $('#lblValidateURIC_ACID').empty();
            }
            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'BUN') {
                $('#lblValidateBUN').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtBUN') {
                $('#lblValidateBUN').empty();
            }
            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'PUS') {
                $('#lblValidateddlPUC').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtPUS') {
                $('#lblValidateddlPUC').empty();
            }
            if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'RBC_FBS') {
                $('#lblValidateDdlRBC').text(JSON.parse(ErrorResponse).value.Tables[0].Rows[0].ERROR);
            }
            else if (JSON.parse(ErrorResponse).value.Tables[0].Rows[0].FEILD_NAME == 'NoERROR' && InputTextboxID == 'txtRBC_FBS') {
                $('#lblValidateDdlRBC').empty();
            }
        }
        //end here

        //Saving Validation Msg
        function SavingAllValidationMsg() {
            debugger;
            var ErrorappNO = $('#lblErrorAppNo').text();
            var ErrorGender = $('#lblCompareGender').text();
            var ErrorDOB = $('#lblCompareDOB').text();
            var ErrorAge = $('#lblCompareAge').text();
            var ErrorBmiResult = $('#lblBmiResult').text();
            var ErrorChestIE = $('#lblValidateChestIE').text();
            var ErrorSystolic1 = $('#lblValidateSystolic1').text();
            var ErrorSystolic2 = $('#lblValidateSystolic2').text();
            var ErrorSystolic3 = $('#lblValidateSystolic3').text();
            var ErrorDiastolic1 = $('#lblValidateDiastolic1').text();
            var ErrorDiastolic2 = $('#lblValidateDiastolic2').text();
            var ErrorDiastolic3 = $('#lblValidateDiastolic3').text();
            var ErrorHTNError = $('#lblHTNError').text();
            var ErrorDMError = $('#lblDMError').text();
            var ErrorSmokerWarning = $('#lblSmokerWarning').text();
            //alert('hi1');
            var ErrorPCV = $('#lblValidatePCV').text();
            var ErrorRBC = $('#lblValidateRBC').text();
            var ErrorMCV = $('#lblValidateMCV').text();
            var ErrorMCH = $('#lblValidateMCH').text();
            var ErrorMCHC = $('#lblValidateMCHC').text();
            var ErrorWBC = $('#lblValidateWBC').text();
            var ErrorNEUTROPHILS = $('#lblValidateNEUTROPHILS').text();
            var ErrorLYMPHOCYTES = $('#lblValidateLYMPHOCYTES').text();
            var ErrorEOSINOPHILS = $('#lblValidateEOSINOPHILS').text();
            var ErrorMONOCYTES = $('#lblValidateMONOCYTES').text();
            var ErrorBASOPHILS = $('#lblValidateBASOPHILS').text();
            var ErrorPLATELETCOUNT = $('#lblValidatePLATELETCOUNT').text();
            var ErrorESR = $('#lblValidateESR').text();

            var ErrorHBSAG = $('#lblValidateHBSAG').text();

            var ErrorHIV = $('#lblValidateHIV').text();

            var ErrorSGOT = $('#lblValidateSGOT').text();
            var ErrorSGPT = $('#lblValidateSGPT').text();
            var ErrorGGT = $('#lblValidateGGT').text();
            var ErrorBILLIRUBIN = $('#lblValidateBILLIRUBIN').text();
            var ErrorBILLIRUBIN1 = $('#lblValidateBILLIRUBIN1').text();
            var ErrorTotalBILLIRUBIN = $('#lblValidateTotalBILLIRUBIN').text();
            var ErrorALP = $('#lblValidateALP').text();
            var ErrorS_GLOBILIN = $('#lblValidateS_GLOBILIN').text();
            var ErrorS_ALBUMIN = $('#lblValidateS_ALBUMIN').text();
            var ErrorT_PROTEIN = $('#lblValidateT_PROTEIN').text();
            var ErrorAGRATIO = $('#lblValidateAGRATIO').text();

            var ErrorCHOLESTEROL = $('#lblValidateCHOLESTEROL').text();
            var ErrorHDL = $('#lblValidateHDL').text();
            var ErrorTRIGYCERIDE = $('#lblValidateTRIGYCERIDE').text();
            var ErrorHDL_RATIO = $('#lblValidateHDL_RATIO').text();
            //alert('hi2');
            var ErrorS_CREATINE = $('#lblValidateS_CREATINE').text();
            var ErrorURIC_ACID = $('#lblValidateURIC_ACID').text();
            var ErrorBUN = $('#lblValidateBUN').text(); 
            var ErrorPUS = $('#lblValidateddlPUC').text();
            //alert('hi3');
            var AllValidationError = ("ErrorappNO: " + ErrorappNO + "," +
                                         "ErrorGender: " + ErrorGender + "," +
                                         "ErrorDOB: " + ErrorDOB + "," +
                                         "ErrorAge: " + ErrorAge + "," +
                                         "ErrorBmiResult: " + ErrorBmiResult + "," +
                                         "ErrorChestIE: " + ErrorChestIE + "," +
                                         "ErrorSystolic1: " + ErrorSystolic1 + "," +
                                         "ErrorSystolic2: " + ErrorSystolic2 + "," +
                                         "ErrorSystolic3: " + ErrorSystolic3 + "," +
                                         "ErrorDiastolic1: " + ErrorDiastolic1 + "," +
                                         "ErrorDiastolic2: " + ErrorDiastolic2 + "," +
                                         "ErrorDiastolic3: " + ErrorDiastolic3 + "," +
                                         "ErrorHTNError: " + ErrorHTNError + "," +
                                         "ErrorDMError: " + ErrorDMError + "," +
                                         "ErrorSmokerWarning: " + ErrorSmokerWarning + "," +
                                         "ErrorPCV: " + ErrorPCV + "," +
                                         "ErrorRBC: " + ErrorRBC + "," +
                                         "ErrorMCV: " + ErrorMCV + "," +
                                         "ErrorMCH: " + ErrorMCH + "," +
                                         "ErrorMCHC:" + ErrorMCHC + "," +
                                         "ErrorWBC: " + ErrorWBC + "," +
                                         "ErrorNEUTROPHILS: " + ErrorNEUTROPHILS + "," +
                                         "ErrorLYMPHOCYTES: " + ErrorLYMPHOCYTES + "," +
                                         "ErrorEOSINOPHILS: " + ErrorEOSINOPHILS + "," +
                                         "ErrorMONOCYTES: " + ErrorMONOCYTES + "," +
                                         "ErrorBASOPHILS: " + ErrorBASOPHILS + "," +
                                         "ErrorPLATELETCOUNT: " + ErrorPLATELETCOUNT + "," +
                                         "ErrorESR: " + ErrorESR + "," +
                                         "ErrorHBSAG: " + ErrorHBSAG + "," +
                                         "ErrorHIV: " + ErrorHIV + "," +
                                         "ErrorSGOT: " + ErrorSGOT + "," +
                                         "ErrorSGPT: " + ErrorSGPT + "," +
                                         "ErrorGGT: " + ErrorGGT + "," +
                                         "ErrorBILLIRUBIN :" + ErrorBILLIRUBIN + "," +
                                         "ErrorBILLIRUBIN1: " + ErrorBILLIRUBIN1 + "," +
                                         "ErrorTotalBILLIRUBIN: " + ErrorTotalBILLIRUBIN + "," +
                                         "ErrorALP: " + ErrorALP + "," +
                                         "ErrorS_GLOBILIN: " + ErrorS_GLOBILIN + "," +
                                         "ErrorS_ALBUMIN: " + ErrorS_ALBUMIN + "," +
                                         "ErrorT_PROTEIN: " + ErrorT_PROTEIN + "," +
                                         "ErrorAGRATIO: " + ErrorAGRATIO + "," +
                                         "ErrorCHOLESTEROL: " + ErrorCHOLESTEROL + "," +
                                         "ErrorHDL: " + ErrorHDL + "," +
                                         "ErrorTRIGYCERIDE: " + ErrorTRIGYCERIDE + "," +
                                         "ErrorS_CREATINE: " + ErrorS_CREATINE + "," +
                                         "ErrorURIC_ACID: " + ErrorURIC_ACID + "," +
                                         "ErrorBUN: " + ErrorBUN + "," +
                                         "ErrorPUS: " + ErrorPUS

                                         );
            //alert('hi4');
            Appcode_MedicalDataEntry.SavelblMessage(AllValidationError);
        }
        //end here
    </script>

</head>
<body>

    <form id="form1" runat="server">
        <div>
            <asp:HiddenField runat="server" ID="hdnValue" ClientIDMode="Static" />
            <asp:ScriptManager runat="server" ID="smDataEntry" EnablePageMethods="true"></asp:ScriptManager>
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
                                            <%--<input id="chkMRFDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
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
                                            <label>Application Number:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAppno" CssClass="form-control validate lblLable" ClientIDMode="Static" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server" ClientIDMode="Static" ID="lblErrorAppNo" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <%--<div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Gender:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtGender" CssClass="form-control validate lblLable" ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>--%>

                                <div class="col-md-12">
                                    <div class="col-md-2 form-group">
                                        <label>Gender</label>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:DropDownList ID="ddlGender" CssClass="form-control validate lblLable" AutoPostBack="false" ClientIDMode="Static" runat="server" onblur="ComparePersonalInfo()">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                            <asp:ListItem Value="Male">Male</asp:ListItem>
                                            <asp:ListItem Value="Female">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblCompareGender" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>DOB</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDOB" CssClass="form-control validate lblLable" ClientIDMode="Static" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblCompareDOB" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>AGE</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAGE" CssClass="form-control validate lblLable" ClientIDMode="Static" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblCompareAge" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Height (in Cm)</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtHeight" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Weight (in Kg)</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtWeight" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" onblur="GetBMI()" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BMI</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtBMI" CssClass="form-control validate lblLable" ClientIDMode="Static" ReadOnly="true" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblBmiResult" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            <%--<asp:Button runat="server" ID="btnCalBMI" CssClass="btn-primary" ClientIDMode="Static" OnClientClick="return false;" Text="Calculate BMI" />--%>
                                            <%--<button id="btnCalBMI">Calculate BMI</button>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Pulse</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtPulse" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidatePulse" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Chest Inspiration</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtChestInspiration" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Chest Expiration</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtChestExpiration" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="ChestValidation()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateChestIE" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>


                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Systolic 1: </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSystolic" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateSystolic1" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Systolic 2: </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSystolic1" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateSystolic2" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Systolic 3: </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSystolic2" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateSystolic3" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Diastolic 1:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDiastolic" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateDiastolic1" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Diastolic 2: </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDiastolic1" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateDiastolic2" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Diastolic 3:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDiastolic2" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateDiastolic3" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>


                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Girth</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtGirth" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <%--</div>--%>

                                <div class="col-md-12">
                                    <div id="divHTNCase" runat="server" clientidmode="Static" class="col-md-2">
                                        <div class="form-group">
                                            <label>Known Case of HTN</label><br />
                                            <div class="col-right">
                                                <label class="lblHTNCase">
                                                    No<input type="checkbox" class="simple-switch-input" runat="server" id="chkHTNCase" /><span class="simple-switch dark"></span>Yes
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="caseHTNComments" class="col-md-2" clientidmode="Static" runat="server">
                                        <div class="form-group">
                                            <label>Comment on Case HTN</label><br />
                                            <asp:TextBox ID="txtHTNComments" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="3" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblHTNError" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div id="divDMCase" runat="server" class="col-md-2" clientidmode="Static">
                                        <div class="form-group">
                                            <label>Known Case of DM</label><br />
                                            <div class="col-right">
                                                <label class="lblDMCase">
                                                    No<input type="checkbox" class="simple-switch-input " runat="server" id="chkDMCase" /><span class="simple-switch dark"></span>Yes
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="caseDmComments" runat="server" clientidmode="Static" class="col-md-2">
                                        <div class="form-group">
                                            <label>Comment on Case DM</label><br />
                                            <asp:TextBox ID="txtDMComments" TextMode="MultiLine" Font-Size="X-Small" CssClass="form-control validate lblLable" ClientIDMode="Static" Rows="3" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblDMError" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <%--end here--%>
                                <%--</div>--%>


                                <div class="col-md-12">

                                    <div id="divSmoker" runat="server" class="col-md-2" clientidmode="Static">
                                        <div class="form-group">
                                            <label>Smoker</label><br />
                                            <div class="col-right">
                                                <label class="lblDMCase">
                                                    No<input type="checkbox" class="simple-switch-input " runat="server" id="chkSmoker" /><span class="simple-switch dark"></span>Yes
                                                </label>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="SmokerComments" runat="server" class="col-md-2" clientidmode="Static">
                                        <div class="form-group">
                                            <label>Smoker Comments</label><br />
                                            <asp:TextBox ID="txtSmokerComments" TextMode="MultiLine" Font-Size="X-Small" ClientIDMode="Static" CssClass="form-control validate lblLable" Rows="3" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblSmokerWarning" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div id="divAlcohol" runat="server" class="col-md-2" clientidmode="Static">
                                        <div class="form-group">
                                            <label>Alcohol</label><br />
                                            <div class="col-right">
                                                <label class="lblDMCase">
                                                    No<input type="checkbox" class="simple-switch-input " runat="server" id="chkAlcohol" /><span class="simple-switch dark"></span>Yes
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <%--<div id="AlcoholComments" runat="server" clientidmode="Static" class="col-md-2">
                                        <div class="form-group">
                                            <label>Alcohol Comments</label><br />
                                            <asp:TextBox ID="txtAlcoholComments" TextMode="MultiLine" Font-Size="X-Small" ClientIDMode="Static" CssClass="form-control validate lblLable" Rows="3" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblAlcoholWarning" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>


                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Other Comments / Finding</label><br />
                                            <asp:TextBox ID="txtOtherComments" TextMode="MultiLine" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="5" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <%--end here--%>
                                <%--</div>--%>
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
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="CBC_ESR_ContainerBody" class="box-body" style="display: none;">
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
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>HB:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtHB" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateHB" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>PCV:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtPCV" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidatePCV" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>RBC:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtRBC" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateRBC" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>MCV:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtMCV" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateMCV" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>MCH:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtMCH" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateMCH" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>MCHC:</label>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtMCHC" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateMCHC" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>WBC:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtWBC" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateWBC" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>NEUTROPHILS:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtNEUTROPHILS" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateNEUTROPHILS" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>LYMPHOCYTES:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtLYMPHOCYTES" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateLYMPHOCYTES" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>EOSINOPHILS:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtEOSINOPHILS" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateEOSINOPHILS" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>MONOCYTES:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtMONOCYTES" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateMONOCYTES" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BASOPHILS:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtBASOPHILS" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateBASOPHILS" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>PLATELET COUNT:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtPLATELETCOUNT" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidatePLATELETCOUNT" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>ESR:</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtESR" CssClass="form-control lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateESR" Font-Bold="true" ForeColor="Red"></asp:Label>
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
                                            <asp:CheckBox ID="chkHBSAG" CssClass="CheckSave" runat="server" Text="Edit" EnableViewState="False" ClientIDMode="Static" />
                                            <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                        </div>
                                        <div class="col-md-3" style="float: right">
                                            <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-tools ">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="HBSAG_ContainerBody" class="box-body" style="display: none">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:RadioButton ID="chkHBSAG_NA" runat="server" Text="NA" GroupName="HBSG" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:RadioButton ID="chkHBSAG_POSITITVE" runat="server" Text="POSITIVE" GroupName="HBSG" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:RadioButton ID="chkHBSAG_NEGATIVE" runat="server" Text="NEGATIVE" GroupName="HBSG" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateHBSAG" Font-Bold="true" ForeColor="Red"></asp:Label>
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
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="HIV_ContainerBody" class="box-body" style="display: none">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:RadioButton ID="chkhiv_NA" runat="server" Text="NA" GroupName="HIV" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:RadioButton ID="chkhiv_POSITIVE" runat="server" Text="POSITIVE" GroupName="HIV" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:RadioButton ID="chkhiv_NEGATIVE" runat="server" Text="NEGATIVE" GroupName="HIV" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateHIV" Font-Bold="true" ForeColor="Red"></asp:Label>
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
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="FBS_RUA_ContainerBody" class="box-body" style="display: none">
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
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Duration</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDuration" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>FBS</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtFBS" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="ValidateFBS()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateFBS" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>


                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>HBA1C</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtHBA1C" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="ValidateFBS()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateHBA1C" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <%-- <div class="col-md-2">
                                <div class="form-group">
                                    <label>RUA</label>
                                    <asp:TextBox ID="txtRUA" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>--%>


                                <%--</div>--%>
                                <div class="col-md-12">
                                    <div class="col-md-2 form-group">
                                        <label>RBC</label>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:DropDownList ID="ddlRBC" CssClass="form-control lblLable" AutoPostBack="false" ClientIDMode="Static" runat="server">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="0-5">0-5</asp:ListItem>
                                                <asp:ListItem Value="5-10 HPF">5-10 HPF</asp:ListItem>
                                                <asp:ListItem Value=">10 HPF">>10 HPF</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtRBC_FBS" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateDdlRBC" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2 form-group">
                                        <label>ALBUMIN</label>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddl_ALBUMIN" CssClass="form-control validate lblLable" AutoPostBack="false" ClientIDMode="Static" runat="server">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="Nil">Nil</asp:ListItem>
                                                <asp:ListItem Value="Trace">Trace</asp:ListItem>
                                                <asp:ListItem Value="1+">1+</asp:ListItem>
                                                <asp:ListItem Value="2+">2+</asp:ListItem>
                                                <asp:ListItem Value=">2+">>2+</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateddl_ALBUMIN" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2 form-group">
                                        <label>SUGAR</label>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSUGAR" CssClass="form-control validate lblLable" AutoPostBack="false" ClientIDMode="Static" runat="server">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="Nil">Nil</asp:ListItem>
                                                <asp:ListItem Value="Trace">Trace</asp:ListItem>
                                                <asp:ListItem Value="1+">1+</asp:ListItem>
                                                <asp:ListItem Value="2+">2+</asp:ListItem>
                                                <asp:ListItem Value="3+">3+</asp:ListItem>
                                                <asp:ListItem Value="4+">4+</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateddlSUGAR" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2 form-group">
                                        <label>PUS CELLS</label>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                           <%-- <asp:DropDownList ID="ddlPUC" CssClass="form-control validate lblLable" AutoPostBack="false" ClientIDMode="Static" runat="server">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="0-10">0-10</asp:ListItem>
                                                <asp:ListItem Value=">10 -20">>10 -20</asp:ListItem>
                                                <asp:ListItem Value="> 20">> 20</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtPUS" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateddlPUC" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2 form-group">
                                        <label>Others</label>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlOthers" CssClass="form-control validate lblLable" AutoPostBack="false" ClientIDMode="Static" runat="server">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="Nil">Nil</asp:ListItem>
                                                <asp:ListItem Value="Cast cells">Cast cells</asp:ListItem>
                                                <asp:ListItem Value="Uric acid crystal">Uric acid crystal</asp:ListItem>
                                                <asp:ListItem Value="Bacteria">Bacteria</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateddlOthers" Font-Bold="true" ForeColor="Red"></asp:Label>
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

            <div id="divLFT_Test" runat="server" class="col-md-12 HideControl">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div id="LFT_Test_Container" class="box box-warning box-solid ">
                            <div class="box-header with-border">
                                <div class="col-md-12">
                                    <div class="col-md-9">
                                        <div class="col-md-3">
                                            <h3 class="box-title ">LFT Test</h3>
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
                                            <asp:CheckBox ID="chkLFT" CssClass="CheckSave" ClientIDMode="Static" runat="server" Text="Edit" EnableViewState="False" />
                                            <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                        </div>
                                        <div class="col-md-3" style="float: right">
                                            <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-tools ">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="LFT_Test_ContainerBody" class="box-body" style="display: none">
                                <div class="col-md-12 error-container-div">
                                    <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="col-md-6 HideControl error-detail_div">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SGOT</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSGOT" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateSGOT" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <%--<div class="col-md-3"></div>--%>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>SGPT</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSGPT" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateSGPT" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                    <%--<div class="col-md-3"></div>--%>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>GGT</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtGGT" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateGGT" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <%--<div class="col-md-3"></div>--%>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Direct BILLIRUBIN</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtBILLIRUBIN" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateBILLIRUBIN" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Indirect BILLIRUBIN</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtBILLIRUBIN2" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id); GetTotalBILLIRUBIN();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateBILLIRUBIN1" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total BILLIRUBIN</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtTotalBILLIRUBIN" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateTotalBILLIRUBIN" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>ALP</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtALP" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateALP" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <%--<div class="col-md-3"></div>--%>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>S-GLOBILIN</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtS_GLOBILIN" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateS_GLOBILIN" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                    <%--<div class="col-md-3"></div>--%>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>S-ALBUMIN</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtS_ALBUMIN" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateS_ALBUMIN" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <%--<div class="col-md-3"></div>--%>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>TOTAL PROTEIN</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtTOTAL_PROTEIN" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateT_PROTEIN" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>


                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>AG Ratio</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAgRatio" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateAGRATIO" Font-Bold="true" ForeColor="Red"></asp:Label>
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

            <div id="divLIPIDS" runat="server" class="col-md-12 HideControl">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <div id="LIPIDSContainer" class="box box-warning box-solid ">
                            <div class="box-header with-border">
                                <div class="col-md-12">
                                    <div class="col-md-9">
                                        <div class="col-md-3">
                                            <h3 class="box-title ">LIPIDS Test</h3>
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
                                            <asp:CheckBox ID="chkLIPIDS" CssClass="CheckSave" ClientIDMode="Static" runat="server" Text="Edit" EnableViewState="False" />
                                            <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                        </div>
                                        <div class="col-md-3" style="float: right">
                                            <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-tools ">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="LIPIDSContainerBody" class="box-body" style="display: none">
                                <div class="col-md-12 error-container-div">
                                    <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="col-md-6 HideControl error-detail_div">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CHOLESTEROL</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtCHOLESTEROL" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateCHOLESTEROL" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>HDL</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtHDL" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id); GetTc_HdlRatio();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateHDL" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>TRIGLYCERIDE</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtTRIGLYCERIDE" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateTRIGYCERIDE" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>TC/HDL RATIO</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:TextBox ID="txtTcRatio" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" ReadOnly="true" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtTcRatio" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" ReadOnly="true" Text="" runat="server" onchange="GetTc_HdlRatio()"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateHDL_RATIO" Font-Bold="true" ForeColor="Red"></asp:Label>
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

            <div id="divRFT" runat="server" class="col-md-12 HideControl">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <div id="RFTContainer" class="box box-warning box-solid ">
                            <div class="box-header with-border">
                                <div class="col-md-12">
                                    <div class="col-md-9">
                                        <div class="col-md-3">
                                            <h3 class="box-title ">RFT Test</h3>
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
                                            <asp:CheckBox ID="chkRFT" CssClass="CheckSave" ClientIDMode="Static" runat="server" Text="Edit" EnableViewState="False" />
                                            <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                        </div>
                                        <div class="col-md-3" style="float: right">
                                            <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-tools ">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="RFTContainerBody" class="box-body" style="display: none">
                                <div class="col-md-12 error-container-div">
                                    <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="col-md-6 HideControl error-detail_div">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>S.CREATININE</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtS_CREATININE" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateS_CREATINE" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>URIC ACID</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtURIC_ACID" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateURIC_ACID" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BUN</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtBUN" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server" onblur="FeildErrorWarning(this.id)"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2 form-group">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblValidateBUN" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <%-- <div class="col-md-3">
                                <div class="form-group">
                                    <label>TC/HDL RATIO</label>
                                    <asp:TextBox ID="TextBox5" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3"></div>--%>
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

            <div id="divECG" runat="server" class="col-md-12 HideControl">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <div id="ECGContainer" class="box box-warning box-solid ">
                            <div class="box-header with-border">
                                <div class="col-md-12">
                                    <div class="col-md-9">
                                        <div class="col-md-3">
                                            <h3 class="box-title ">ECG Test</h3>
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
                                            <asp:CheckBox ID="chkECG" CssClass="CheckSave" ClientIDMode="Static" runat="server" Text="Edit" EnableViewState="False" />
                                            <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                        </div>
                                        <div class="col-md-3" style="float: right">
                                            <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-tools ">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="ECGContainerBody" class="box-body" style="display: none">
                                <div class="col-md-12 error-container-div">
                                    <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="col-md-6 HideControl error-detail_div">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date of test</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtDateOfTest" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Decision</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:TextBox ID="txtDecision" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtECGDecision" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="5" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CMO Decision</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:TextBox ID="txtDecision" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtCMODecisionECG" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="5" Text="" runat="server"></asp:TextBox>
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

            <div id="divCTMT" runat="server" class="col-md-12 HideControl">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <div id="CTMTContainer" class="box box-warning box-solid ">
                            <div class="box-header with-border">
                                <div class="col-md-12">
                                    <div class="col-md-9">
                                        <div class="col-md-3">
                                            <h3 class="box-title ">CTMT Test</h3>
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
                                            <asp:CheckBox ID="chkCTMTC" CssClass="CheckSave" ClientIDMode="Static" runat="server" Text="Edit" EnableViewState="False" />
                                            <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                        </div>
                                        <div class="col-md-3" style="float: right">
                                            <%--<asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-tools ">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="CTMTContainerBody" class="box-body" style="display: none">
                                <div class="col-md-12 error-container-div">
                                    <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="col-md-6 HideControl error-detail_div">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date of test</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="dtTestMedication" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Medication</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:TextBox ID="txtDecision" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtMedication" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="5" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Exercise Time</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtExerciseTime" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Work Load</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtWorkLoad" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BP</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtBP" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>THR</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtTHR" CssClass="form-control validate lblLable Numeric " ClientIDMode="Static" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Reason for termination</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:TextBox ID="txtDecision" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtResonTermination" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="5" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Decision</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:TextBox ID="txtDecision" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtCTMTDecision" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="5" Text="" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>CMO Decision</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:TextBox ID="txtDecision" CssClass="form-control validate lblLable" ClientIDMode="Static" Text="" runat="server"></asp:TextBox>--%>
                                            <asp:TextBox ID="txtCMODecisionCTMT" TextMode="MultiLine" ClientIDMode="Static" Font-Size="X-Small" CssClass="form-control validate lblLable" Rows="5" Text="" runat="server"></asp:TextBox>
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

            <div style="text-align: center">
                <%--<asp:Button runat="server" ID="btnSaveMedical" Text="Save" ClientIDMode="Static"/>--%>
                <asp:Button runat="server" ID="btnSaveMedical" OnClientClick="return false;" Text="Save" CssClass="btn-primary; width:5%" ClientIDMode="Static" />
                <%--OnClientClick="javascript:window.close()"--%>
            </div>

        </div>

    </form>
</body>
</html>
