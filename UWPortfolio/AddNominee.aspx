<%--//**********************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//**********************************************************************/      
//*                  I N F O R M A T I O N                                       
//********************************************************************* 
// Sr. No.              : 1  
// Company              : Life            
// Module               :           
// Program Author       : Dinesh Kondabattini [MFL00456]            
// BRD/CR/Codesk No/Win : / / /   18500        
// Date Of Creation     : 5.1.2019            
// Description          : CR - 18500 - Validation of LA and Nominee deatils
//**********************************************************************//--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNominee.aspx.cs" Inherits="AddNominee" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SaralUW | AddNominee</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="./bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="./plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link href="./dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="./dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="./dist/js/html5shiv.min.js"></script>
    <script src="./dist/js/respond.min.js"></script>
    <link href="./dist/css/styles-app.css" rel="stylesheet" />
    <link href="./plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="./plugins/jQueryUI/jquery-ui.js"></script>
    <link href="./plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="./plugins/datepicker/bootstrap-datepicker.js"></script>

    <script type="text/javascript">
        $(function () {
            $('.DatePicker').datepicker({

                autoclose: true,
                format: 'dd-mm-yyyy',

                onSelect: function (date) {

                }

            });
        })
    </script>

    <script type="text/javascript">
        function RadioCheck(rb) {
            var gv = document.getElementById("<%=dgUwDedupe.ClientID%>");
            var rbs = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>

    <%-- Script --%>
    <script type="text/javascript">
        function ShowProgressBar() {
            //document.getElementById('dvProgressBar').style.visibility = 'visible';
            $('#dvProgressBar').removeClass('HideControl');
        }

        function HideProgressBar() {
            //document.getElementById('dvProgressBar').style.visibility = "hidden";
            $('#dvProgressBar').addClass('HideControl');
        }
        $(document).ready(function () {

            $('.validatePolicyNo').keypress(function (e) {
            });

            $(".alphanumeric").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57));
            });

            $(".alphanumericwithspace").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57) || k == 32 || k == 44 || k == 46);
            });

            $(".onlyalphabetswithspace").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || k == 32);
            });

            $(".onlyalphabets").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8);
            });

            $(".address").keypress(function (e) {
                //added || k == 9 to allow TAB - Rajeev Sengar
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 9 || k == 32 || k == 35 || k == 37 || k == 38 || k == 40 || k == 41 || k == 44 || k == 45 || k == 46 || (k >= 48 && k <= 57));
            });

            $(".RemoveContinueMultipleSpaces").keyup(function () {
                while (this.value.indexOf("  ") != -1) {
                    this.value = this.value.replace("  ", " ");
                }
                if (this.value.charAt(0) == " ") {
                    this.value = this.value.trim();
                }
            });
            //fetch load data
            var resp = UserControl_PopupManageProposar.FetchCommonData();
            var Json = JSON.parse(resp.value);

            for (var i = 0; i < Json.length; i++) {
                if (i == 0) {
                    fnFillDropDownDetails('.CountryCode', Json[i]);
                }
                else if (i == 1) {
                    fnFillDropDownDetails('#cbNationality', Json[i]);
                }
                else if (i == 2) {
                    fnFillDropDownDetails('#cbOccupation', Json[i]);
                }
                else if (i == 3) {
                    fnFillDropDownDetails('#cbSalutation', Json[i]);
                }
            }
        });
        $(document).on('keypress', '.Numeric', function (e) {
            var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
            return ((keycode == 8 || keycode == 9) || (keycode >= 48 && keycode <= 57));
        });

        //$(window).load(function () {
        //    $('#ldrModal').fadeOut();
        //});

        function isValidEmailAddress() {
            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                return false;
            }
            else {
                return true;
            }
        };
    </script>
    <style type="text/css">
        .LoaderModal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .lblLable {
            background-color: #E5E5E5;
            /*font-weight: bold;*/
            pointer-events: none;
        }

        .LableSuccess {
            color: green;
        }

        .LableInfo {
            color: blue;
        }

        .Lablefailure {
            color: red;
        }

        .CheckSave {
        }

        .lnkAddImage {
            background-image: url('~/dist/img/delete2.png');
        }

        .EmptyVal {
            border-color: red;
            border-width: thin;
            border-style: solid;
            font-size: 16px;
        }

        .ReqDescp {
            width: 30%;
        }

        .bg-Fgi_UWIndicator {
            background-color: #ff9999;
        }

        .HideControl {
            display: none;
        }

        .editeOpt {
            margin-left: 1300px;
        }

        .opencontiner {
            background-color: palegoldenrod;
        }
    </style>
    <script>
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            //validation
            $('.validatePolicyNo').keypress(function (e) {
            });

            $(".alphanumeric").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57));
            });

            $(".alphanumericwithspace").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57) || k == 32 || k == 44 || k == 46);
            });

            $(".onlyalphabetswithspace").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || k == 32);
            });

            $(".onlyalphabets").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8);
            });

            $(".address").keypress(function (e) {
                //added || k == 9 to allow TAB - Rajeev Sengar
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 9 || k == 32 || k == 35 || k == 37 || k == 38 || k == 40 || k == 41 || k == 44 || k == 45 || k == 46 || (k >= 48 && k <= 57));
            });

            $(".RemoveContinueMultipleSpaces").keyup(function () {
                while (this.value.indexOf("  ") != -1) {
                    this.value = this.value.replace("  ", " ");
                }
                if (this.value.charAt(0) == " ") {
                    this.value = this.value.trim();
                }
            });
            //fetch load data
            var resp = UserControl_PopupManageProposar.FetchCommonData();
            var Json = JSON.parse(resp.value);

            for (var i = 0; i < Json.length; i++) {
                if (i == 0) {
                    fnFillDropDownDetails('.CountryCode', Json[i]);
                }
                else if (i == 1) {
                    fnFillDropDownDetails('#cbNationality', Json[i]);
                }
                else if (i == 2) {
                    fnFillDropDownDetails('#cbOccupation', Json[i]);
                }
                else if (i == 3) {
                    fnFillDropDownDetails('#cbSalutation', Json[i]);
                }
            }
            $(document).on('keypress', '.Numeric', function (e) {
                var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((keycode == 8 || keycode == 9) || (keycode >= 48 && keycode <= 57));
            });

            //date picker
            $('.DatePicker').datepicker({
                autoclose: true,
                format: 'dd-mm-yyyy',
                onSelect: function (date) {
                }
            });
        });
        var ClientDetails = '';
        var dedupeFlag = false;
        //$('.CheckSave').change(
        function fnCheckSave(req) {
            debugger;
            if ($(req).is(':checked')) {
                //enables controls inside body
                $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('input').removeClass('lblLable');
                $('#btnDedupe').parent().parent().parent().parent().removeClass('HideControl');
                $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('select').removeClass('lblLable');
                //$(req).parent().parent().parent().parent().parent().parent().find('.box-bodyPopUp').find('.cbEnableDisable').attr("enabled", false);
                $(req).parent().parent().parent().parent().parent().parent().find('.box-bodyPopUp').find('.cbEnableDisable').removeAttr("disabled");

                $('.btnNewClient').parent().removeClass('HideControl');
                if ($('.btnNewClient').val() != 'Save') {
                    $('.btnSave').parent().removeClass('HideControl');
                }
            }
            else {
                //disable controls inside body
                $('.btnNewClient').val('New Client').parent().addClass('HideControl');
                $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('input').addClass('lblLable');
                $(req).parent().parent().parent().find('.btnSave').parent().addClass('HideControl');
                $('#btnDedupe').parent().parent().parent().parent().addClass('HideControl');
                $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('select').addClass('lblLable');
                $(req).parent().parent().parent().parent().parent().parent().find('.box-bodyPopUp').find('.cbEnableDisable').attr("disabled", true);
                $('.btnNewClient').parent().addClass('HideControl');
                $('.btnRevert').parent().addClass('HideControl');
            }
            $('#txtLaClientId').addClass('lblLable');
            $('.DedupeDetails').addClass('HideControl');
            $('.DedupeDynamicRow').remove();
            $('.PolicyDetails').addClass('HideControl');
            $('#cbAddressCopy').attr('checked', false);
            $('.permanent').removeClass('HideControl');
            $('#btnEditCommu').addClass('HideControl');
        }//);    

        //$('.rdoRole').change(
        function fnFillClientDetails() {
            ShowProgressBar();
            debugger;
            setTimeout(function () {
                $('#cbAddressCopy').attr('checked', false);
                $('.premanent').removeClass('HideControl');
                $('#btnEditCommu').addClass('HideControl');
                //client policy header        
                $('.btnSave').parent().addClass('HideControl');
                $('.CheckSave').parent().parent().removeClass('HideControl');
                $('.btnNewClient').val('New Client').parent().addClass('HideControl');
                $('.btnRevert').parent().addClass('HideControl');
                if ($('.CheckSave').is(':checked')) {
                    //uncheck edit button and read only controls inside body
                    $('.CheckSave').prop('checked', false);
                    $('.CheckSave').parent().parent().parent().parent().parent().parent().find('.box-body').find('input').addClass('lblLable');
                    //$('.CheckSave').parent().parent().find('.btnSave').addClass('HideControl');
                    $('#btnDedupe').parent().parent().parent().parent().addClass('HideControl');
                    $('.CheckSave').parent().parent().parent().parent().parent().parent().find('.box-body').find('select').addClass('lblLable');
                    $('.CheckSave').parent().parent().parent().parent().parent().parent().find('.box-body').find('.cbEnableDisable').attr("disabled", true);
                }
                //client policy body
                $('.box-bodyPopUp').find('input[type="text"]').val('');
                $('.box-bodyPopUp').find('select').val('-1');
                $('.Lablefailure').addClass('HideControl');
                //dedupe gui change
                $('.DedupeDynamicRow').remove();
                $('.DedupeDetails').addClass('HideControl');
                $('.PolicyDetails').addClass('HideControl');
                $('#txtLaClientId').addClass('lblLable');
                fnFillClientDetailsDetailsFromRole();
            }, 0);
            HideProgressBar();
        }

        function fnFillDedupeDetails() {
            if (confirm('Do you want to change client')) {
                ShowProgressBar();
                setTimeout(function () {
                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please Wait... Fetching Client Information...');
                    //clear all data
                    $('.box-bodyPopUp').find('input[type="text"]').val('');
                    $('.box-bodyPopUp').find('select').val('-1');
                    $('.Lablefailure').addClass('HideControl');
                    //client details header
                    //$('.CheckSave').parent().addClass('HideControl');
                    //client details body
                    //$('.details').find('input').addClass('lblLable');
                    //$('.details').find('select').addClass('lblLable');
                    //$('.details').find('.cbEnableDisable').attr("disabled", true);
                    $('#btnDedupe').parent().parent().parent().parent().addClass('HideControl');

                    var ClientId = $('input[name="dedupe"]:checked').parent().parent().find('.spClientId').html();
                    var response = UserControl_PopupManageProposar.FetchDedupDetails(ClientId);
                    var splits = response.value.split('#');
                    if (splits[0] == '0') {
                        //client details part
                        var ClientDetailsFromDedupe = JSON.parse(splits[1]);
                        //$('#txtOldValue').val(splits[1]);
                        //call control binding function
                        fnFillClientControls(ClientDetailsFromDedupe);
                        dedupeFlag = true;
                        //auto scroll to client header
                        $('#divUserControlModal').animate({
                            scrollTop: $(".divClient").offset().top
                        }, 500);
                        $('.DedupeDetails').addClass('HideControl');
                    }
                    else {

                        $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                    }
                    HideProgressBar();
                }, 0);
            }

        }

        function fnFillClientDetailsDetailsFromRole() {
            debugger;
            dedupeFlag = false;
            //fetch client details from radio button
            $('.divClient').removeClass('HideControl');
            var ClientId = $('input[name="Role"]:checked').parent().parent().find('.spanClientId').html();
            var Type = $('input[name="Role"]:checked').parent().parent().find('.spanRole').html();
           <%-- var ApplnNo = '<%=strApplnNo%>';--%>
            var response = UserControl_PopupManageProposar.FetchDetails(ClientId, ApplnNo, Type);
            var splits = response.value.split('#');
            if (splits[0] == '0') {
                $('#DivTitle').html(Type + ' Details');
                //client details part
                ClientDetails = JSON.parse(splits[1]);
                $('#txtOldValue').val(splits[1]);
                fnFillClientControls(ClientDetails);
            }
            else {
                alert(splits[1]);
            }
        }

        function fnIsCommPremanentAddrSame() {
            //check if checkbox is checked or not
            debugger;
            if ($('#cbAddressCopy').is(':checked')) {
                if (confirm('Is permanent address same as communication address?')) {
                    $('#btnEditCommu').removeClass('HideControl');
                    //paste to communication address from permanent address
                    $('#txtPermanentAddress1').val($('#txtCommuAddress1').val());
                    $('#txtPermanentAddress2').val($('#txtCommuAddress2').val());
                    $('#txtPermanentAddress3').val($('#txtCommuAddress3').val());
                    $('#txtPermanentAddress4').val($('#txtCommuAddress4').val());
                    $('#txtPermanentAddress5').val($('#txtCommuAddress5').val());
                    $('#txtPermanentLandmark').val($('#txtCommuLandmark').val());
                    $('#txtPermanentPinCode').val($('#txtCommuPinCode').val());
                    $("#txtPermanentCity").val($('#txtCommuCity').val());
                    $("#txtPermanentDistrict").val($('#txtCommuDistrict').val());
                    $('#txtPermanentState').val($('#txtCommuState').val());
                    $('#txtPermanentAddressRemark').val($('#txtCommuAddressRemark').val());
                    $('#txtPermanentPhone1').val($('#txtCommuPhone1').val());
                    $('#txtPermanentPhone2').val($('#txtCommuPhone2').val());
                    $('#txtPermanentTelNo').val($('#txtCommuTelNo').val());
                    $('#txtPermanentMobileNo').val($('#txtCommuMobileNo').val());
                    $('#txtPermanentEmailId').val($('#txtCommuEmailId').val());
                    $('#txtPermanentFaxNo').val($('#txtCommuFaxNo').val());
                    /*city district state is not fetched from dropdown*/
                    if ($("#txtPermanentCountryCode option[value=" + $('#txtCommuCountryCode').val() + "]").length > 0) {
                        $('#txtPermanentCountryCode').val($('#txtCommuCountryCode').val());
                    }
                    else {
                        $('#txtPermanentCountryCode').val('-1');
                    }
                    /*end here*/
                    $('.premanent').addClass('HideControl');
                    fnIsEditAddressDetails('1');
                }
                else {
                    //uncheck copy checkbox
                    $('#cbAddressCopy').prop('checked', false);
                    $('#btnEditCommu').addClass('HideControl');
                }
            }
            else {
                $('#btnEditCommu').addClass('HideControl');
                $('#txtPermanentAddress1').val('');
                $('#txtPermanentAddress2').val('');
                $('#txtPermanentAddress3').val('');
                $('#txtPermanentAddress4').val('');
                $('#txtPermanentAddress5').val('');
                $('#txtPermanentLandmark').val('');
                $('#txtPermanentPinCode').val('');
                $("#txtPermanentCity").val('');
                $("#txtPermanentDistrict").val('');
                $('#txtPermanentState').val('');
                $('#txtPermanentAddressRemark').val('');
                $('#txtPermanentPhone1').val('');
                $('#txtPermanentPhone2').val('');
                $('#txtPermanentTelNo').val('');
                $('#txtPermanentMobileNo').val('');
                $('#txtPermanentEmailId').val('');
                $('#txtPermanentFaxNo').val('');
                /*city district state is not fetched from dropdown*/
                $('#txtPermanentCountryCode').val('-1');

                $('.premanent').removeClass('HideControl');
                fnIsEditAddressDetails('0');
            }
        }

        function fnPremiumClalc() {
            $('#divErrorMsgClient').find('#ResponseStatus').addClass('HideControl');
            $('.lblError').addClass('HideControl');
            //validation
            if (!dedupeFlag) {
                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Dedupe Client First');
                return false;
            }
            if ($('#txtLaDob').val() == '') {
                $('#txtLaDob').parent().find('.lblError').removeClass('HideControl').html('Enter Date Of Birth');
                $('#txtLaDob').focus();
                return false;
            }
            $('.PolicyDetails').addClass('HideControl');
       <%-- var strApplicationNo = '<%=strApplnNo%>';--%>
            var Role = $('input[name="Role"]:checked').val()
            var Roles = Role.split(',');
            var count = 0;
            if (Roles.length > 1) {
                ShowProgressBar();
                setTimeout(function () {
                    if ('|' + Roles[i] + '|' == '|LA|') {
                        $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please wait...');
                        var Ret = UserControl_PopupManageProposar.PremiumCalc(strApplicationNo, $('#txtLaDob').val(), $('#chkClientGender').is(':checked'), $('#chkClientIsSmoker').is(':checked'), $('#txtOldValue').val());
                        alert(Ret);
                        var splits = Ret.value.split('#');
                        if (splits[0] == '0') {
                            /*commented and added by shri on 26 july 17 show premium calc */
                            //fill policy calculation details
                            //var PolicyDetails = JSON.parse(splits[1]);
                            //$('.PolicyDetails').removeClass('HideControl');
                            //fnFillPolicyDetails(PolicyDetails);
                            $('.PolicyDetails').removeClass('HideControl');
                            $("#divPolicyTable").find(".premiumcalcbox").remove();
                            $("#divPolicyTable").append(splits[1]);
                            $('#divUserControlModal').animate({
                                scrollTop: $(".PolicyDetails").offset().top
                            }, 500);
                            /*end here*/
                        }
                        else if (splits[0] == '1') {
                            //show the error msg
                            $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                        }
                        else if (splits[0] == '2') {
                            //call saving procedure
                            fnSave();
                        }
                        else if (splits[0] == '-1') {
                            $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('try again later');
                        }

                    }
                    HideProgressBar();
                }, 0);
            }
            else {
                for (var i = 0; i < Roles.length; i++) {
                    if ('|' + Roles[i] + '|' == '|LA|') {
                        count++;
                        break;
                    }
                }
            }
            if (count > 0) {
                ShowProgressBar();
                setTimeout(function () {
                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please wait...');
                    var Ret = UserControl_PopupManageProposar.PremiumCalc(strApplicationNo, $('#txtLaDob').val(), $('#chkClientGender').is(':checked'), $('#chkClientIsSmoker').is(':checked'), $('#txtOldValue').val());
                    var splits = Ret.value.split('#');
                    if (splits[0] == '0') {
                        /*commented and added by shri on 26 july 17 show premium calc */
                        //fill policy calculation details
                        //var PolicyDetails = JSON.parse(splits[1]);
                        //$('.PolicyDetails').removeClass('HideControl');
                        //fnFillPolicyDetails(PolicyDetails);
                        $('.PolicyDetails').removeClass('HideControl');
                        $("#divPolicyTable").find(".premiumcalcbox").remove();
                        $("#divPolicyTable").append(splits[1]);
                        $('#divUserControlModal').animate({
                            scrollTop: $(".PolicyDetails").offset().top
                        }, 500);
                        /*end here*/
                    }
                    else if (splits[0] == '1') {
                        $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                    }
                    else if (splits[0] == '-1') {
                        $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                    }
                    else if (splits[0] == '2') {
                        //call saving procedure
                        fnSave();
                    }
                    HideProgressBar();
                }, 0);
            }
            else {
                if (confirm('Do you want to update client information?')) {
                    ShowProgressBar();
                    setTimeout(function () {
                        fnSave();
                        HideProgressBar();
                    }, 0);
                }
            }
        }
        function fnFillPolicyDetails(PolicyDetails) {
            $('#txtInstalmentPremiumAmt').val(PolicyDetails.InstalmentPremiumAmt);
            $('#txtMedicalLoadingPremium').val(PolicyDetails.MedicalLoadingPremium);
            $('#txtNonMedicalLoadingPremium').val(PolicyDetails.NonMedicalLoadingPremium);
            $('#txtSumAssured').val(PolicyDetails.SumAssured);
            $('#TxtTotalInstalmentPremium').val(PolicyDetails.TotalInstalmentPremium);
            $('#txtTotalPremiumAmt').val(PolicyDetails.TotalPremiumAmt);
        }

        function fnFillClientControls(ClientDetails) {
            debugger;
            $('#txtLaClientId').val((ClientDetails.ClientId == '') ? '' : ClientDetails.ClientId);
            if ($("#cbSalutation option[value='" + ClientDetails.Salutation + "']").length > 0) {
                $('#cbSalutation').val(ClientDetails.Salutation);
            }
            else {
                $('#cbSalutation').val("-1");
            }
            $('#txtLaFirstName').val((ClientDetails.ClientFirstName == '') ? '' : ClientDetails.ClientFirstName);
            $('#txtLaMiddleName').val((ClientDetails.ClientFatherName == '') ? '' : ClientDetails.ClientFatherName);
            $('#txtLaLastName').val((ClientDetails.ClinetLastName == '') ? '' : ClientDetails.ClinetLastName);
            $('#txtLaDob').val((ClientDetails.ClientDateOfBirth == '01-01-0001') ? '' : ClientDetails.ClientDateOfBirth);
            //facing problem with checkboxes            
            $('#chkClientGender').prop('checked', ClientDetails.boolClientGender);
            $('#chkClientIsSmoker').prop('checked', ClientDetails.IsSmoker);
            $('#chkIsNSAP').prop('checked', ClientDetails.IsNSAP);
            if ($("#cbOccupation option[value='" + ClientDetails.Occupation + "']").length > 0) {
                $('#cbOccupation').val((ClientDetails.Occupation == '') ? '-1' : ClientDetails.Occupation);
            }
            else {
                $('#cbOccupation').val('-1');
            }
            if ($("#cbNationality option[value='" + ClientDetails.Nationality + "']").length > 0) {
                $('#cbNationality').val((ClientDetails.Nationality == '') ? '-1' : ClientDetails.Nationality);
            }
            else {
                $('#cbNationality').val('-1');
            }
            //fill address part
            for (var i = 0; i < ClientDetails.lstClientAddress.length; i++) {
                if (ClientDetails.lstClientAddress[i].AddressType == 'R') {
                    $('#txtCommuAddress1').val((ClientDetails.lstClientAddress[i].AddressLine1 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine1);
                    $('#txtCommuAddress2').val((ClientDetails.lstClientAddress[i].AddressLine2 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine2);
                    $('#txtCommuAddress3').val((ClientDetails.lstClientAddress[i].AddressLine3 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine3);
                    $('#txtCommuAddress4').val((ClientDetails.lstClientAddress[i].AddressLine4 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine4);
                    $('#txtCommuAddress5').val((ClientDetails.lstClientAddress[i].AddressLine5 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine5);
                    $('#txtCommuLandmark').val((ClientDetails.lstClientAddress[i].LandMark == '') ? '' : ClientDetails.lstClientAddress[i].LandMark);
                    $('#txtCommuPinCode').val((ClientDetails.lstClientAddress[i].PinCode == '-1') ? '' : ClientDetails.lstClientAddress[i].PinCode);
                    /*added and commented by shri to change control*/
                    $('#txtCommuCity').val(ClientDetails.lstClientAddress[i].City);
                    $('#txtCommuDistrict').val(ClientDetails.lstClientAddress[i].District);
                    $('#txtCommuState').val(ClientDetails.lstClientAddress[i].State);
                    /*
                    if ($("#txtCommuCity option[value='" + ClientDetails.lstClientAddress[i].City + "']").length > 0) {
                        $('#txtCommuCity').val(ClientDetails.lstClientAddress[i].City);
                    }
                    else {
                        $('#txtCommuCity').val("-1");
                    }
    
                    if ($("#txtCommuDistrict option[value='" + ClientDetails.lstClientAddress[i].District + "']").length > 0) {
                        $('#txtCommuDistrict').val(ClientDetails.lstClientAddress[i].District);
                    }
                    else {
                        $('#txtCommuDistrict').val('-1');
                    }
    
                    if ($("#txtCommuState option[value='" + ClientDetails.lstClientAddress[i].State + "']").length > 0) {
                        $('#txtCommuState').val(ClientDetails.lstClientAddress[i].State);
                    }
                    else {
                        $('#txtCommuState').val('-1');
                    }                
                    */
                    if ($("#txtCommuCountryCode option[value='" + ClientDetails.lstClientAddress[i].CountryCode + "']").length > 0) {
                        $('#txtCommuCountryCode').val(ClientDetails.lstClientAddress[i].CountryCode);
                    }
                    else {
                        $('#txtCommuCountryCode').val('-1');
                    }
                    /*end here*/
                    $('#txtCommuAddressRemark').val((ClientDetails.lstClientAddress[i].AddressRemark == '') ? '' : ClientDetails.lstClientAddress[i].AddressRemark);
                    $('#txtCommuPhone1').val((ClientDetails.lstClientAddress[i].Phone1 == '') ? '' : ClientDetails.lstClientAddress[i].Phone1);
                    $('#txtCommuPhone2').val((ClientDetails.lstClientAddress[i].Phone2 == '') ? '' : ClientDetails.lstClientAddress[i].Phone2);
                    $('#txtCommuTelNo').val((ClientDetails.lstClientAddress[i].TelNo == '') ? '' : ClientDetails.lstClientAddress[i].TelNo);
                    $('#txtCommuMobileNo').val((ClientDetails.lstClientAddress[i].MobileNo == '') ? '' : ClientDetails.lstClientAddress[i].MobileNo);
                    $('#txtCommuEmailId').val((ClientDetails.lstClientAddress[i].EmailId == '') ? '' : ClientDetails.lstClientAddress[i].EmailId);
                    $('#txtCommuFaxNo').val((ClientDetails.lstClientAddress[i].FaxNo == '') ? '' : ClientDetails.lstClientAddress[i].FaxNo);
                }
                else if (ClientDetails.lstClientAddress[i].AddressType == 'P') {
                    $('#txtPermanentAddress1').val((ClientDetails.lstClientAddress[i].AddressLine1 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine1);
                    $('#txtPermanentAddress2').val((ClientDetails.lstClientAddress[i].AddressLine2 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine2);
                    $('#txtPermanentAddress3').val((ClientDetails.lstClientAddress[i].AddressLine3 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine3);
                    $('#txtPermanentAddress4').val((ClientDetails.lstClientAddress[i].AddressLine4 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine4);
                    $('#txtPermanentAddress5').val((ClientDetails.lstClientAddress[i].AddressLine5 == '') ? '' : ClientDetails.lstClientAddress[i].AddressLine5);
                    $('#txtPermanentLandmark').val((ClientDetails.lstClientAddress[i].LandMark == '') ? '' : ClientDetails.lstClientAddress[i].LandMark);
                    $('#txtPermanentPinCode').val((ClientDetails.lstClientAddress[i].PinCode == '-1') ? '' : ClientDetails.lstClientAddress[i].PinCode);
                    /*added and commented by shri on 29 june 17 to change control type*/
                    $('#txtPermanentCity').val(ClientDetails.lstClientAddress[i].City);
                    $('#txtPermanentDistrict').val(ClientDetails.lstClientAddress[i].District);
                    $('#txtPermanentState').val(ClientDetails.lstClientAddress[i].State);
                    /*
                    if ($("#txtPermanentCity option[value='" + ClientDetails.lstClientAddress[i].City + "']").length > 0) {
                        $('#txtPermanentCity').val(ClientDetails.lstClientAddress[i].City);
                    }
                    else {
                        $('#txtPermanentCity').val('-1');
                    }
    
                    if ($("#txtPermanentDistrict option[value='" + ClientDetails.lstClientAddress[i].District + "']").length > 0) {
                        $('#txtPermanentDistrict').val(ClientDetails.lstClientAddress[i].District);
                    }
                    else {
                        $('#txtPermanentDistrict').val('-1');
                    }
    
                    if ($("#txtPermanentState option[value='" + ClientDetails.lstClientAddress[i].State + "']").length > 0) {
                        $('#txtPermanentState').val(ClientDetails.lstClientAddress[i].State);
                    }
                    else {
                        $('#txtPermanentState').val('-1');
                    }                
                    */
                    if ($("#txtPermanentCountryCode option[value='" + ClientDetails.lstClientAddress[i].CountryCode + "']").length > 0) {
                        $('#txtPermanentCountryCode').val((ClientDetails.lstClientAddress[i].CountryCode == '') ? '-1' : ClientDetails.lstClientAddress[i].CountryCode);
                    }
                    else {
                        $('#txtPermanentCountryCode').val('-1');
                    }
                    /*end here*/
                    $('#txtPermanentAddressRemark').val((ClientDetails.lstClientAddress[i].AddressRemark == '') ? '' : ClientDetails.lstClientAddress[i].AddressRemark);
                    $('#txtPermanentPhone1').val((ClientDetails.lstClientAddress[i].Phone1 == '') ? '' : ClientDetails.lstClientAddress[i].Phone1);
                    $('#txtPermanentPhone2').val((ClientDetails.lstClientAddress[i].Phone2 == '') ? '' : ClientDetails.lstClientAddress[i].Phone2);
                    $('#txtPermanentTelNo').val((ClientDetails.lstClientAddress[i].TelNo == '') ? '' : ClientDetails.lstClientAddress[i].TelNo);
                    $('#txtPermanentMobileNo').val((ClientDetails.lstClientAddress[i].MobileNo == '') ? '' : ClientDetails.lstClientAddress[i].MobileNo);
                    $('#txtPermanentEmailId').val((ClientDetails.lstClientAddress[i].EmailId == '') ? '' : ClientDetails.lstClientAddress[i].EmailId);
                    $('#txtPermanentFaxNo').val((ClientDetails.lstClientAddress[i].FaxNo == '') ? '' : ClientDetails.lstClientAddress[i].FaxNo);
                }
            }
        }

        function fnValidateClientDetails(UpdateCreate) {
            $('.lblError').addClass('HideControl');
            //validate basic details
            if (UpdateCreate == "0") {
                if (!dedupeFlag) {
                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Dedupe Client First');
                    return false;
                }
                if ($('#txtLaClientId').val() == '') {
                    $('#txtLaClientId').parent().find('.lblError').removeClass('HideControl').html('Enter Client Id');
                    $('#txtLaClientId').focus();
                    return false;
                }
            }

            if ($('#cbSalutation :checked').val() == '-1') {
                $('#cbSalutation').parent().find('.lblError').removeClass('HideControl').html('Select Salutation');
                $('#cbSalutation').focus();
                return false;
            }
            if ($('#txtLaFirstName').val() == '') {
                $('#txtLaFirstName').parent().find('.lblError').removeClass('HideControl').html('Enter First Name');
                $('#txtLaFirstName').focus();
                return false;
            }
            if ($('#txtLaLastName').val() == '') {
                $('#txtLaLastName').parent().find('.lblError').removeClass('HideControl').html('Enter Last Name');
                $('#txtLaLastName').focus();
                return false;
            }
            if ($('#txtLaDob').val() == '') {
                $('#txtLaDob').parent().find('.lblError').removeClass('HideControl').html('Enter Date Of Birth');
                $('#txtLaDob').focus();
                return false;
            }
            if ($('#cbOccupation :checked').val() == '-1') {
                $('#cbOccupation').parent().find('.lblError').removeClass('HideControl').html('Select Occupation');
                $('#cbOccupation').focus();
                return false;
            }

            if ($('#cbNationality').val() == '-1') {
                $('#cbNationality').parent().find('.lblError').removeClass('HideControl').html('Select State');
                $('#cbNationality').focus();
                return false;
            }

            //validate communication address details        
            if ($('input[name="Role"]:checked').val().toLowerCase() != 'nominee') {
                if ($('#txtCommuAddress1').val() == '') {
                    $('#txtCommuAddress1').parent().find('.lblError').removeClass('HideControl').html('Enter Address Line 1');
                    $('#txtCommuAddress1').focus();
                    return false;
                }

                if ($('#txtCommuPinCode').val() == '') {
                    debugger;
                    $('#txtCommuPinCode').parent().find('.lblError').removeClass('HideControl').html('Enter Pin Code');
                    $('#txtCommuPinCode').focus();
                    return false;
                }

                if ($('#txtCommuPinCode').val() == '') {
                    $('#txtCommuPinCode').parent().find('.lblError').removeClass('HideControl').html('Enter Pin Code');
                    $('#txtCommuPinCode').focus();
                    return false;
                }
                if ($('#txtCommuCity').val() == '-1') {
                    $('#txtCommuCity').parent().find('.lblError').removeClass('HideControl').html('Select City');
                    $('#txtCommuCity').focus();
                    return false;
                }
                if ($('#txtCommuDistrict').val() == '-1') {
                    $('#txtCommuDistrict').parent().find('.lblError').removeClass('HideControl').html('Select District');
                    $('#txtCommuDistrict').focus();
                    return false;
                }
                if ($('#txtCommuState').val() == '-1') {
                    $('#txtCommuState').parent().find('.lblError').removeClass('HideControl').html('Select State');
                    $('#txtCommuState').focus();
                    return false;
                }
                if ($('#txtCommuCountryCode').val() == '-1') {
                    $('#txtCommuCountryCode').parent().find('.lblError').removeClass('HideControl').html('Select Country Code');
                    $('#txtCommuCountryCode').focus();
                    return false;
                }
                if ($('#txtCommuEmailId').val() != '') {
                    if (!isValidEmailAddress($('#txtCommuEmailId').val())) {
                        $('#txtCommuEmailId').parent().find('.lblError').removeClass('HideControl').html('Enter Email Id');
                        $('#txtCommuEmailId').focus();
                        return false;
                    }
                }
                if ($('#txtCommuMobileNo').val() == '') {
                    $('#txtCommuMobileNo').parent().find('.lblError').removeClass('HideControl').html('Enter Mobile No');
                    $('#txtCommuMobileNo').focus();
                    return false;
                }
            }

            if ($('#txtPermanentAddress1').val() != '') {
                //validate permanent address details
                if ($('#txtPermanentAddress1').val() == '') {
                    $('#txtPermanentAddress1').parent().find('.lblError').removeClass('HideControl').html('Enter Address Line 1');
                    $('#txtPermanentAddress1').focus();
                    return false;
                }
                if ($('#txtPermanentPinCode').val() == '') {
                    $('#txtPermanentPinCode').parent().find('.lblError').removeClass('HideControl').html('Enter Pin Code');
                    $('#txtPermanentPinCode').focus();
                    return false;
                }
                if ($('#txtPermanentCity :checked').val() == '-1') {
                    $('#txtPermanentCity').parent().find('.lblError').removeClass('HideControl').html('Select City');
                    $('#txtPermanentCity').focus();
                    return false;
                }
                if ($('#txtPermanentDistrict :checked').val() == '-1') {
                    $('#txtPermanentDistrict').parent().find('.lblError').removeClass('HideControl').html('Select District');
                    $('#txtPermanentDistrict').focus();
                    return false;
                }
                if ($('#txtPermanentState :checked').val() == '-1') {
                    $('#txtPermanentState').parent().find('.lblError').removeClass('HideControl').html('Select State');
                    $('#txtPermanentState').focus();
                    return false;
                }
                if ($('#txtPermanentCountryCode :checked').val() == '-1') {
                    $('#txtPermanentCountryCode').parent().find('.lblError').removeClass('HideControl').html('Select Country Code');
                    $('#txtPermanentCountryCode').focus();
                    return false;
                }
                if ($('#txtPermanentEmailId').val() != '') {
                    if (!isValidEmailAddress($('#txtPermanentEmailId').val())) {
                        $('#txtPermanentEmailId').parent().find('.lblError').removeClass('HideControl').html('Enter Email Id');
                        $('#txtPermanentEmailId').focus();
                        return false;
                    }
                }
                if ($('#txtPermanentMobileNo').val() == '') {
                    $('#txtPermanentMobileNo').parent().find('.lblError').removeClass('HideControl').html('Enter Mobile No');
                    $('#txtPermanentMobileNo').focus();
                    return false;
                }
            }
            return true;
        }

        function fnSave() {
            $('.lblError').addClass('HideControl');
       <%-- var strApplnNo = '<%=strApplnNo%>';
        var Channel = '<%=strChannelType%>';--%>
            //validation                
            if (fnValidateClientDetails("0")) {
                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please Wait... Updating client...');
                //call service                                    
                var response = UserControl_PopupManageProposar.UpdateClientInfo($('#txtLaClientId').val(), $('input[name="Role"]:checked').val(), $('#cbSalutation :checked').val(), $('#txtLaFirstName').val(), $('#txtLaMiddleName').val()
                                , $('#txtLaLastName').val(), $('#txtLaDob').val(), $('#chkClientGender').is(':checked'), $('#chkClientIsSmoker').is(':checked'), strApplnNo, Channel
                                //permant address
                                , $('#txtPermanentAddress1').val(), $('#txtPermanentAddress2').val(), $('#txtPermanentAddress3').val(), $('#txtPermanentAddress4').val(), $('#txtPermanentAddress5').val()
                                , $('#txtPermanentLandmark').val(), $('#txtPermanentPinCode').val(), $('#txtPermanentCity').val(), $('#txtPermanentDistrict').val(), $('#txtPermanentState').val()
                                , $('#txtPermanentCountryCode :checked').val(), $('#txtPermanentAddressRemark').val(), $('#txtPermanentPhone1').val(), $('#txtPermanentPhone2').val(), $('#txtPermanentTelNo').val()
                                , $('#txtPermanentMobileNo').val(), $('#txtPermanentEmailId').val(), $('#txtPermanentFaxNo').val()
                                //communication address
                                , $('#txtCommuAddress1').val(), $('#txtCommuAddress2').val(), $('#txtCommuAddress3').val(), $('#txtCommuAddress4').val(), $('#txtCommuAddress5').val()
                                , $('#txtCommuLandmark').val(), $('#txtCommuPinCode').val(), $('#txtCommuCity').val(), $('#txtCommuDistrict').val(), $('#txtCommuState').val()
                                , $('#txtCommuCountryCode :checked').val(), $('#txtCommuAddressRemark').val(), $('#txtCommuPhone1').val(), $('#txtCommuPhone2').val(), $('#txtCommuTelNo').val()
                                , $('#txtCommuMobileNo').val(), $('#txtCommuEmailId').val(), $('#txtCommuFaxNo').val(), $('#cbNationality :checked').val(), $('#chkIsNSAP').is(':checked')
                                , $('#cbOccupation :checked').val(), $('#txtOldValue').val()
                                );
                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(response.value);
                $('#divUserControlModal').animate({
                    scrollTop: $("#divErrorMsgClient").offset().top
                }, 500);
                $('.btnSave').parent().addClass('HideControl');
                $('.btnRevert').parent().addClass('HideControl');
                $('.btnNewClient').parent().addClass('HideControl');
                $('#cbLAEdit').attr('checked', false);
                $('#cbLAEdit').trigger('change');
            }
        }
        function fnSaveCreateClient() {
            if (fnValidateClientDetails("1")) {
                //create client
         <%--   var strApplnNo = '<%=strApplnNo%>';
            var strChannelType = '<%=strChannelType%>';--%>
                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please wait... Creating new client');
                var response = UserControl_PopupManageProposar.CreateClientInfo($('#txtLaClientId').val(), $('input[name="Role"]:checked').val(), $('#cbSalutation :checked').val(), $('#txtLaFirstName').val(), $('#txtLaMiddleName').val()
                            , $('#txtLaLastName').val(), $('#txtLaDob').val(), $('#chkClientGender').is(':checked'), $('#chkClientIsSmoker').is(':checked')
                            //permant address
                            , $('#txtPermanentAddress1').val(), $('#txtPermanentAddress2').val(), $('#txtPermanentAddress3').val(), $('#txtPermanentAddress4').val(), $('#txtPermanentAddress5').val()
                            , $('#txtPermanentLandmark').val(), $('#txtPermanentPinCode').val(), $('#txtPermanentCity').val(), $('#txtPermanentDistrict').val(), $('#txtPermanentState').val()
                            , $('#txtPermanentCountryCode :checked').val(), $('#txtPermanentAddressRemark').val(), $('#txtPermanentPhone1').val(), $('#txtPermanentPhone2').val(), $('#txtPermanentTelNo').val()
                            , $('#txtPermanentMobileNo').val(), $('#txtPermanentEmailId').val(), $('#txtPermanentFaxNo').val()
                            //communication address
                            , $('#txtCommuAddress1').val(), $('#txtCommuAddress2').val(), $('#txtCommuAddress3').val(), $('#txtCommuAddress4').val(), $('#txtCommuAddress5').val()
                            , $('#txtCommuLandmark').val(), $('#txtCommuPinCode').val(), $('#txtCommuCity').val(), $('#txtCommuDistrict').val(), $('#txtCommuState').val()
                            , $('#txtCommuCountryCode :checked').val(), $('#txtCommuAddressRemark').val(), $('#txtCommuPhone1').val(), $('#txtCommuPhone2').val(), $('#txtCommuTelNo').val()
                            , $('#txtCommuMobileNo').val(), $('#txtCommuEmailId').val(), $('#txtCommuFaxNo').val(), $('#cbNationality :checked').val(), $('#chkIsNSAP').is(':checked')
                            , $('#cbOccupation :checked').val(), strApplnNo, strChannelType, $('#txtOldValue').val()
                            );
                var splits = response.value.split('#');
                if (splits[0] == "0") {
                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[2]);
                    $('#txtLaClientId').val(splits[1]);
                    $('.btnNewClient').val('New Client');
                    $('.btnSave').parent().addClass('HideControl');
                    $('.btnRevert').parent().addClass('HideControl');
                    $('.btnNewClient').parent().addClass('HideControl');
                    $('#cbLAEdit').attr('checked', false);
                    $('#cbLAEdit').trigger('change');
                }
                else if (splits[0] == "1") {
                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                }
                else {
                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('try again later');
                }
                $('#divUserControlModal').animate({
                    scrollTop: $("#divErrorMsgClient").offset().top
                }, 500);
            }
        }
        function fnFetchDedupeDetails() {
            //validate dedupe request

            if ($('#txtLaFirstName').val() == '') {
                $('#txtLaFirstName').parent().find('.lblError').removeClass('HideControl').html('Enter First Name');
                $('#txtLaFirstName').focus();
                return false;
            }
            if ($('#txtLaLastName').val() == '') {
                $('#txtLaLastName').parent().find('.lblError').removeClass('HideControl').html('Enter Last Name');
                $('#txtLaLastName').focus();
                return false;
            }
            if ($('#txtLaDob').val() == '') {
                $('#txtLaDob').parent().find('.lblError').removeClass('HideControl').html('Enter Date Of Birth');
                $('#txtLaDob').focus();
                return false;
            }

            $('.DedupeDynamicRow').remove();
            $('.DedupeDetails').addClass('HideControl');
            ShowProgressBar();
            setTimeout(function () {
                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please Wait... Fetching Client Information...');
                var varResp = UserControl_PopupManageProposar.FetchDedupSearchDetails($('#txtLaFirstName').val(), $('#txtLaLastName').val(), $('#txtLaDob').val(), $('#chkClientGender').is(':checked'));
                $('#divErrorMsgClient').find('#ResponseStatus').addClass('HideControl');
                var response = varResp.value.split('#');
                if (response[0] == "0") {
                    var json = JSON.parse(response[1]);
                    if (json.length > 0) {
                        $('.DedupeDetails').removeClass('HideControl');
                        for (var i = 0; i < json.length; i++) {
                            var Clone;
                            Clone = $('#tblDedupe').find('.main').clone();
                            $(Clone).addClass('DedupeDynamicRow');
                            $(Clone).removeClass('main');
                            $(Clone).removeClass('HideControl');
                            $(Clone).find('.spClientId').html(json[i].ClientId);
                            $(Clone).find('.spGivenName').html(json[i].ClientFirstName);
                            $(Clone).find('.spSurname').html(json[i].ClinetLastName);
                            $(Clone).find('.spGender').html((json[i].ClientGender) ? 'Male' : 'Female');
                            $(Clone).find('.spBirthRegDate').html(json[i].ClientDateOfBirth);
                            $('#tblDedupe').append(Clone);
                        }
                        $('#divUserControlModal').animate({
                            scrollTop: $("#tblDedupe").offset().top
                        }, 500);
                    }
                    else {
                        $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('No record found');
                    }
                }
                else if (response[0] == "-1") {
                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(response[1]);
                }
                HideProgressBar();
            }, 0);
        }

        function fnCreateNewClient(type) {
            $('#txtLaClientId').addClass('lblLable');
            if ($(type).val() == 'New Client') {
                debugger;
                dedupe = 1;
                ShowProgressBar();
                setTimeout(function () {
                    $('.premanent').removeClass('HideControl');
                    $(type).val('Save');
                    $('.details').find('input[type="text"]').val('');
                    $('.details').find('select').val('-1');
                    $('.details').find('.cbEnableDisable').removeAttr("disabled");
                    $('.Lablefailure').addClass('HideControl');
                    $('.btnSave').parent().addClass('HideControl');
                    //$('.CheckSave').parent().addClass('HideControl');
                    $('.details').find('input').removeClass('lblLable');
                    $('.details').find('select').removeClass('lblLable');
                    $('.btnRevert').parent().removeClass('HideControl');
                    $('#btnDedupe').parent().parent().parent().parent().removeClass('HideControl');
                    $('#cbAddressCopy').attr('checked', false);
                    HideProgressBar();
                    $('#cbAddressCopy').trigger('change');
                    HideProgressBar();
                }
                , 0);
            }
            else if ($(type).val() == 'Save') {
                $('.lblError').addClass('HideControl');
                if (fnValidateClientDetails("1")) {
                    //premium calculation
                    //validation
                    ShowProgressBar();
                    setTimeout(function () {
                        if ($('#txtLaDob').val() == '') {
                            $('#txtLaDob').parent().find('.lblError').removeClass('HideControl').html('Enter Date Of Birth');
                            $('#txtLaDob').focus();
                            return false;
                        }
                        $('.PolicyDetails').addClass('HideControl');
                 <%--   var strApplicationNo = '<%=strApplnNo%>';--%>
                        var Role = $('input[name="Role"]:checked').val()
                        var Roles = Role.split(',');
                        var count = 0;
                        if (Roles.length > 1) {
                            if ('|' + Roles[i] + '|' == '|LA|') {
                                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please wait...');
                                var Ret = UserControl_PopupManageProposar.PremiumCalc(strApplicationNo, $('#txtLaDob').val(), $('#chkClientGender').is(':checked'), $('#chkClientIsSmoker').is(':checked'), $('#txtOldValue').val());
                                alert(Ret);
                                var splits = Ret.value.split('#');
                                if (splits[0] == '0') {
                                    /*commented and added by shri on 26 july 17 show premium calc */
                                    //fill policy calculation details
                                    //var PolicyDetails = JSON.parse(splits[1]);
                                    //$('.PolicyDetails').removeClass('HideControl');
                                    //fnFillPolicyDetails(PolicyDetails);
                                    $('.PolicyDetails').removeClass('HideControl');
                                    $("#divPolicyTable").find(".premiumcalcbox").remove();
                                    $("#divPolicyTable").append(splits[1]);
                                    $('#divUserControlModal').animate({
                                        scrollTop: $(".PolicyDetails").offset().top
                                    }, 500);
                                    /*end here*/
                                }
                                else if (splits[0] == '1') {
                                    //show the error msg
                                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                                }
                                else if (splits[0] == '2') {
                                    $('.DedupeDetails').addClass('HideControl');
                                    $('.PolicyDetails').addClass('HideControl');
                                    fnSaveCreateClient();
                                }
                                else if (splits[0] == '-1') {
                                    $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('try again later');
                                }

                            }
                        }
                        else {
                            for (var i = 0; i < Roles.length; i++) {
                                if ('|' + Roles[i] + '|' == '|LA|') {
                                    count++;
                                    break;
                                }
                            }
                        }
                        if (count > 0) {
                            $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html('Please wait...');
                            var Ret = UserControl_PopupManageProposar.PremiumCalc(strApplicationNo, $('#txtLaDob').val(), $('#chkClientGender').is(':checked'), $('#chkClientIsSmoker').is(':checked'), $('#txtOldValue').val());
                            var splits = Ret.value.split('#');
                            if (splits[0] == '0') {
                                /*commented and added by shri on 26 july 17 show premium calc */
                                //fill policy calculation details
                                //var PolicyDetails = JSON.parse(splits[1]);
                                //$('.PolicyDetails').removeClass('HideControl');
                                //fnFillPolicyDetails(PolicyDetails);
                                $('.PolicyDetails').removeClass('HideControl');
                                $("#divPolicyTable").find(".premiumcalcbox").remove();
                                $("#divPolicyTable").append(splits[1]);
                                $('#divUserControlModal').animate({
                                    scrollTop: $(".PolicyDetails").offset().top
                                }, 500);
                                /*end here*/
                            }
                            else if (splits[0] == '1') {
                                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                            }
                            else if (splits[0] == '-1') {
                                $('#divErrorMsgClient').find('#ResponseStatus').removeClass('HideControl').html(splits[1]);
                            }
                            else if (splits[0] == '2') {
                                $('.DedupeDetails').addClass('HideControl');
                                $('.PolicyDetails').addClass('HideControl');
                                fnSaveCreateClient();
                            }
                        }
                        else {
                            if (confirm('Do you want to create client ?')) {
                                $('.DedupeDetails').addClass('HideControl');
                                $('.PolicyDetails').addClass('HideControl');
                                fnSaveCreateClient();
                            }
                        }
                        HideProgressBar();
                    }, 0)
                }
            }
    }

    function fnCallPreviousData() {
        if (ClientDetails != '') {
            $('#txtOldValue').val(ClientDetails);
            fnFillClientControls(ClientDetails);
            $('.divClient').find('.btnNewClient').val('New Client');
            $('.divClient').find('.btnRevert').parent().addClass('HideControl');
            $('.divClient').find('#cbLAEdit').parent().removeClass('HideControl');
            $('.divClient').find('.btnSave').parent().removeClass('HideControl');
            $('#btnDedupe').parent().parent().parent().parent().removeClass('HideControl');
        }
    }

    function fnFillOtherInfo(a) {
        if ($(a).val() != '') {
            $('.lblError').addClass('HideControl');
            var response = AddNominee.FillNestedDetails($(a).val());
            var splits = response.value.split('#');
            if (splits.length == 3) {
                if ($(a).hasClass('PPincode')) {
                    $(a).parent().parent().parent().find('.PCity').val(splits[0]);
                    $(a).parent().parent().parent().find('.PDistrict').val(splits[1]);
                    $(a).parent().parent().parent().find('.PState').val(splits[2]);
                }
                else if ($(a).hasClass('CPincode')) {
                    $(a).parent().parent().parent().find('.CCity').val(splits[0]);
                    $(a).parent().parent().parent().find('.CDistrict').val(splits[1]);
                    $(a).parent().parent().parent().find('.CState').val(splits[2]);
                }
            }
        }
    }

    function fnPinCodeMsg(a) {
        $('.lblError').addClass('HideControl');
        if ($(a).hasClass('permanent')) {
            $('#txtPermanentPinCode').parent().find('.lblError').removeClass('HideControl').html('Enter Pincode');
        }
        else if ($(a).hasClass('commu')) {
            $('#txtCommuPinCode').parent().find('.lblError').removeClass('HideControl').html('Enter Pincode');
        }
        return false;
    }

    function fnFillDropDownDetails(select, Json) {
        $(select).append('<option value="-1">Select</option>')
        for (var i = 0; i < Json.length; i++) {
            $(select).append('<option value=' + Json[i].Value + '>' + Json[i].Key + '</option>')
        }
        $(select).val('-1');
    }

    function fnCreateUpdateClient() {
        ShowProgressBar();
        setTimeout(function () {
            $('.PolicyDetails').addClass('HideControl');
            if ($('.btnSave').parent().hasClass('HideControl')) {
                //create client
                fnSaveCreateClient();
            }
            else {
                //update client        
                fnSave();
            }
            HideProgressBar();
        }, 0);
    }

    function fnIsEditAddressDetails(bit) {
        if (bit == '0') {
            $('.communication').find('input[type="text"]').removeClass('lblLable');
            $('.communication').find('select').removeClass('lblLable');
            $('.communication').find('select').removeClass('lblLable');
            $('.communication').find('.box-bodyPopUp').find('.cbEnableDisable').attr("disabled", false);
            $('#cbAddressCopy').attr('checked', false);
            $('#cbAddressCopy').trigger('change');
        }
        else if (bit == '1') {
            $('.communication').find('input[type="text"]').addClass('lblLable');
            $('.communication').find('select').addClass('lblLable');
            $('.communication').find('select').addClass('lblLable');
            $('.communication').find('.box-bodyPopUp').find('.cbEnableDisable').attr("disabled", true);
        }
    }

    // 1 Added By Dinesh Kondabattini
function fnIsLAPropSame() {
        var LAPropName, LAPropDOB;
        var LAClientId = '';
        var ProposerClientId = '';
        var myTab = document.getElementById("<%= gvEntity.ClientID %>");
        var isOk = false;
        for (i = 1; i < myTab.rows.length; i++) {
            var objCells = myTab.rows.item(i).cells;
            //if (objCells[0].innerText != 'Nominee') {

            if (objCells[0].innerText == 'LA') {
                LAClientId = objCells[1].innerText;
            }
            else if (objCells[0].innerText == 'proposer') {
                ProposerClientId = objCells[1].innerText;
            }
            LAPropName = objCells[2].innerText;
            LAPropDOB = objCells[3].innerText;
            var NomineeName = $('#cbSalutation :checked').val().toUpperCase().trim() + ' ' + $('#txtLaFirstName').val().toUpperCase().trim() + ' ' + $('#txtLaLastName').val().toUpperCase().trim();
            var dob = $('#txtLaDob').val().replace('-', '/');
            dob = dob.replace('-', '/');
            if ((LAClientId != '') && (ProposerClientId != '')) {
                if (ProposerClientId != LAClientId) {
                    alert("You can't add Nominee because, LA and Proposer details are different.");
                    return false;
                }
            }
            if (LAPropName.toUpperCase().trim() == NomineeName.toUpperCase().trim() && LAPropDOB == dob && objCells[0].innerText.toUpperCase().trim() == 'LA') {
                alert("" + objCells[0].innerText.toUpperCase().trim() + " and NOMINEE details are same");
                return false;
            }
            else if (LAPropName.toUpperCase().trim() == NomineeName.toUpperCase().trim() && LAPropDOB == dob && objCells[0].innerText.toUpperCase().trim() == 'PROPOSER') {
                alert("" + objCells[0].innerText.toUpperCase().trim() + " and NOMINEE details are same");
                return false;
            }
            else if (LAPropName.toUpperCase().trim() == NomineeName.toUpperCase().trim() && LAPropDOB == dob && objCells[0].innerText.toUpperCase().trim() == 'NOMINEE') {
                alert("" + objCells[0].innerText.toUpperCase().trim() + " details are same");
                return false;
            }
            else if (LAPropName.toUpperCase().trim() == NomineeName.toUpperCase().trim() && LAPropDOB == dob && objCells[0].innerText.toUpperCase().trim() == 'APPOINTEE') {
                alert("" + objCells[0].innerText.toUpperCase().trim() + " and NOMINEE details are same");
                return false;
            }
            if (!isOk) {
                if (LAPropDOB == dob) {
                    var answer = confirm("Please check deduplication of client id as the DOB of <LA & Nominee> <LA & Appointee> <Appointee & Nominee> are same.");
                    if (!answer) {
                        return false;
                    }
                    else {
                        isOk = true;
                    }
                }
            }
            //  }
        }

    }
    // 2 End by Dinesh Kondabattini
    </script>
    <%--  --%>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div id="divLoadingDetails" runat="server" class="col-md-12">
            <div id="LoadingDtls_container" class="box box-warning box-solid">
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <h3 class="box-title">Add Nominee</h3>
                            <i class="fa fa-hourglass-start"></i>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="col-md-4">
                            <asp:Label ID="lblAppno" runat="server" Text="Application No :"></asp:Label>&nbsp;
                                <asp:TextBox runat="server" ID="txtAppno" CssClass="box-input"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="col-md-12">
                        <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
                    </div>
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:DataGrid ID="gvEntity" CssClass="table" AutoGenerateColumns="false" HeaderStyle-CssClass="btn-primary" runat="server">
                                <Columns>
                                    <asp:BoundColumn HeaderText="Role" DataField="Role" />
                                    <asp:BoundColumn HeaderText="ClientId" DataField="ClientId" />
                                    <asp:BoundColumn HeaderText="ClientName" DataField="ClientFullName" />
                                    <asp:BoundColumn HeaderText="DOB" DataField="DOB" />
                                    <asp:BoundColumn HeaderText="Occupation" DataField="Occupation" />
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <%--  --%>
                    <div class="col-md-12">
                        <%--<asp:Button ID="btnClientDetailsAML" runat="server" OnClick="btnClientDetailsAML_Click" CssClass="pull-left btn-link" Text="AML" data-backdrop="static" data-keyboard="false" />--%>
                        <div class="form-group">
                            <div class="col-md-6 pull-left">
                                <asp:Button CssClass="btn-link lnkButton" ID="btnDedupeClient" runat="server" Text="Dedupe" OnClick="btnDedupeClient_Click" />
                                <asp:Button CssClass="btn-link lnkButton" ID="btnFillLADetails" runat="server" Text="Fill LA Details" OnClick="btnFillLADetails_Click"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" runat="server" id="divDgClientDedupe">
                        <div class="table-responsive">
                            <%--   <asp:DataGrid ID="dgUwDedupe" runat="server" HeaderStyle-CssClass="btn-primary" AutoGenerateColumns="false" CssClass="table">                                
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>Select ClientId</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton runat="server" ID="rdoAddNomineeDedupeClientId" AutoPostBack="true"/>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn HeaderText="GCN"  DataField="gcn" />
                                    <asp:BoundColumn HeaderText="Given Name" DataField="givenname" />
                                    <asp:BoundColumn HeaderText="Surname" DataField="surname" />
                                    <asp:BoundColumn HeaderText="Gender" DataField="gender" />
                                    <asp:BoundColumn HeaderText="Birth Date" DataField="BirthRegDate" />
                                </Columns>
                            </asp:DataGrid>--%>
                            <asp:GridView ID="dgUwDedupe" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="btn-primary" CssClass="table">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Select
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="RadioButton1" runat="server" onclick="RadioCheck(this);" AutoPostBack="true" OnCheckedChanged="RadioButton1_CheckedChanged" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("gcn")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="GCN" DataField="gcn" />
                                    <asp:BoundField HeaderText="Given Name" DataField="givenname" />
                                    <asp:BoundField HeaderText="Surname" DataField="surname" />
                                    <asp:BoundField HeaderText="Gender" DataField="gender" />
                                    <asp:BoundField HeaderText="Birth Date" DataField="BirthRegDate" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="col-md-6 pull-left">
                                <asp:Button CssClass="btn-link lnkButton" ID="btnAddNominee" runat="server" Text="Add Nominee" OnClientClick="return fnIsLAPropSame()" OnClick="btnAddNominee_Click" />
                                <asp:Button CssClass="btn-link lnkButton" ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                    <%--basic information of Life assured--%>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="label-default h3">Basic Info</label>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Client Id</label>
                            <span class="lblError Lablefailure HideControl"></span>
                            <asp:TextBox ID="txtLaClientId" onkeydown="return false;" class="form-control BasicInfo Numeric" MaxLength="20" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Salutation</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <%-- <select id="cbSalutation" class="form-control BasicInfo cbEnableDisable" runat="server">
                            </select>--%>
                            <asp:DropDownList ID="cbSalutation" runat="server" class="form-control BasicInfo cbEnableDisable"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>First Name</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtLaFirstName" class="form-control BasicInfo " MaxLength="100" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Middle Name</label>
                            <asp:TextBox ID="txtLaMiddleName" class="form-control BasicInfo " MaxLength="100" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Last Name</label>
                            <span class="lblError Lablefailure HideControl"></span>
                            <asp:TextBox ID="txtLaLastName" class="form-control BasicInfo " MaxLength="100" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Date of birth</label>
                            <span class="lblError Lablefailure HideControl"></span>
                            <asp:TextBox ID="txtLaDob" class="form-control BasicInfo  DatePicker" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 HideControl">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label>Is Smoker?</label>
                            </div>
                            <div class="col-md-12 col-right">
                                <label>
                                    <span>NO</span>
                                    <input class="simple-switch-input BasicInfo" type="checkbox" id="chkClientIsSmoker" />
                                    <span class="simple-switch dark yes"></span>Yes
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label>Gender</label>
                            </div>
                            <div class="col-md-12 col-right">
                                <label>
                                    <span>Female</span>
                                    <input class="simple-switch-input BasicInfo " type="checkbox" id="chkClientGender" runat="server" />
                                    <%--  <asp:CheckBox ID="chkClientGender" runat="server" class="simple-switch-input" />--%>
                                    <span class="simple-switch dark"></span>Male
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 HideControl">
                        <div class="form-group">
                            <div class="col-md-12">
                                <label>Is NSAP</label>
                            </div>
                            <div class="col-md-12 col-right">
                                <label>
                                    <span>No</span>
                                    <input class="simple-switch-input BasicInfo " type="checkbox" id="chkIsNSAP" runat="server" />
                                    <span class="simple-switch dark yes"></span>Yes
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Occupation</label>
                            <span class="lblError Lablefailure BasicInfo HideControl "></span>
                            <%--  <select id="cbOccupation" class="form-control" runat="server">
                            </select>--%>
                            <asp:DropDownList ID="cbOccupation" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label>Nationality</label>
                            <span class="lblError Lablefailure BasicInfo HideControl "></span>
                            <%--<select id="cbNationality" class="form-control cbEnableDisable" runat="server">
                            </select>--%>
                            <asp:DropDownList ID="cbNationality" runat="server" class="form-control cbEnableDisable"></asp:DropDownList>
                        </div>
                    </div>
                    <%-- <div class="col-md-12 HideControl text-right">
                            <div class="form-group">
                                <br />
                                <div class="col-right">
                                    <label>
                                        <input type="button" id="btnDedupe" class="btn-link" onclick="fnFetchDedupeDetails();" value="Click To Dedupe" />
                                    </label>
                                </div>
                            </div>
                        </div>--%>
                    <%--permanent address of life assured--%>
                    <%--   <div class="row"></div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="label-default h3">Communication Address</label>
                                <input type="button" id="btnEditCommu" class="btn-link pull-right HideControl" onclick="fnIsEditAddressDetails('0');" value="Click to change address details" />
                            </div>
                        </div>--%>
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="label-default h3">Communication Address</label>
                              <input type="button" id="btnEditCommu" class="btn-link pull-right HideControl" onclick="fnIsEditAddressDetails('0');" value="Click to change address details" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Address Line 1</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtCommuAddress1" MaxLength="200" class="form-control alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Address Line 2</label>
                            <asp:TextBox ID="txtCommuAddress2" MaxLength="200" class="form-control alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Address Line 3</label>
                            <asp:TextBox ID="txtCommuAddress3" MaxLength="200" class="form-control alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Address Line 4</label>
                            <asp:TextBox ID="txtCommuAddress4" MaxLength="200" class="form-control alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Address Line 5</label>
                            <asp:TextBox ID="txtCommuAddress5" MaxLength="200" class="form-control alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Landmark</label>
                            <asp:TextBox ID="txtCommuLandmark" MaxLength="100" class="form-control alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Pin Code</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtCommuPinCode" onblur="fnFillOtherInfo(this);" MaxLength="8" class="form-control CPincode Numeric" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>City</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtCommuCity" TabIndex="-1" onkeydown="return fnPinCodeMsg(this);" class="form-control CCity commu" runat="server" />
                            <%--<select id="txtCommuCity" class="form-control lblLable">
                        <option value="-1">Select City</option>
                        <option value="Thane">Thane</option>
                    </select>--%>
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>District</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtCommuDistrict" TabIndex="-1" onkeydown="return fnPinCodeMsg(this);" class="form-control CDistrict commu" runat="server" />
                            <%--<select id="txtCommuDistrict" class="form-control lblLable District">
                        <option value="-1">Select District</option>
                        <option value="Thane">Thane</option>
                    </select>--%>
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>State</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtCommuState" TabIndex="-1" onkeydown="return fnPinCodeMsg(this);" class="form-control CState commu" runat="server" />
                            <%--<select id="txtCommuState" class="form-control lblLable">
                        <option value="-1">Select State</option>
                        <option value="Maharashtra">Maharashtra</option>
                    </select>--%>
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Country Code</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <%--<select id="txtCommuCountryCode" class="form-control CountryCode" runat="server">
                            </select>--%>
                            <asp:DropDownList ID="txtCommuCountryCode" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Address Remark</label>
                            <asp:TextBox ID="txtCommuAddressRemark" class="form-control onlyalphabetswithspace" MaxLength="100" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Phone 1</label>
                            <asp:TextBox ID="txtCommuPhone1" class="form-control Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Phone 2</label>
                            <asp:TextBox ID="txtCommuPhone2" class="form-control Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Tel. Number</label>
                            <asp:TextBox ID="txtCommuTelNo" class="form-control Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Mobile No.</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtCommuMobileNo" class="form-control Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Email id</label>
                            <span class="lblError Lablefailure HideControl"></span>
                            <asp:TextBox ID="txtCommuEmailId" class="form-control " MaxLength="100" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 communication">
                        <div class="form-group">
                            <label>Fax No</label>
                            <asp:TextBox ID="txtCommuFaxNo" class="form-control Numeric" MaxLength="15" runat="server" />
                        </div>
                    </div>
                    <%--rental address of life assured--%>
                    <div class="row"></div>
                    <div class="col-md-12 text-center">
                        <div class="form-group">
                            <input type="checkbox" class="cbEnableDisable" onchange="fnIsCommPremanentAddrSame();" id="cbAddressCopy" runat="server" />
                            <label for="cbAddressCopy">Is permanent address same as communication address ??</label>
                        </div>
                    </div>
                    <div class="col-md-12 premanent">
                        <div class="form-group">
                            <label class="label-default h3">Permanent Address</label>
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Address Line 1</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtPermanentAddress1" MaxLength="200" class="form-control  alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Address Line 2</label>
                            <asp:TextBox ID="txtPermanentAddress2" MaxLength="200" class="form-control  alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Address Line 3</label>
                            <asp:TextBox ID="txtPermanentAddress3" MaxLength="200" class="form-control  alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Address Line 4</label>
                            <asp:TextBox ID="txtPermanentAddress4" MaxLength="200" class="form-control  alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Address Line 5</label>
                            <asp:TextBox ID="txtPermanentAddress5" MaxLength="200" class="form-control  alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Landmark</label>
                            <asp:TextBox ID="txtPermanentLandmark" MaxLength="100" class="form-control alphanumericwithspace" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Pin Code</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtPermanentPinCode" onblur="fnFillOtherInfo(this);" MaxLength="8" class="form-control PPincode" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>City</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtPermanentCity" TabIndex="-1" onkeydown="return fnPinCodeMsg(this);" class="form-control  PCity permanent" runat="server" />
                            <%--<select id="txtPermanentCity" class="form-control lblLable">
                        <option value="-1">Select City</option>
                        <option value="Thane">Thane</option>
                        <option value="Palghar">Palghar</option>
                    </select>--%>
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>District</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtPermanentDistrict" TabIndex="-1" onkeydown="return fnPinCodeMsg(this);" class="form-control  PDistrict permanent" runat="server" />
                            <%--<select id="txtPermanentDistrict" class="form-control lblLable">
                        <option value="-1">Select District</option>
                        <option value="Thane">Thane</option>
                        <option value="Palghar">Palghar</option>
                    </select>--%>
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>State</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtPermanentState" TabIndex="-1" onkeydown="return fnPinCodeMsg(this);" class="form-control  PState permanent" runat="server" />
                            <%--<select id="txtPermanentState" class="form-control lblLable">
                        <option value="-1">Select State</option>
                        <option value="Maharashtra">Maharashtra</option>
                    </select>--%>
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Country Code</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <%-- <select id="txtPermanentCountryCode" class="form-control  CountryCode cbEnableDisable" runat="server">
                            </select>--%>
                            <asp:DropDownList ID="txtPermanentCountryCode" runat="server" class="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Address Remark</label>
                            <asp:TextBox ID="txtPermanentAddressRemark" class="form-control  onlyalphabetswithspace" MaxLength="100" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Phone 1</label>
                            <asp:TextBox ID="txtPermanentPhone1" class="form-control  Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Phone 2</label>
                            <asp:TextBox ID="txtPermanentPhone2" class="form-control  Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Tel. Number</label>
                            <asp:TextBox ID="txtPermanentTelNo" class="form-control  Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Mobile No.</label>
                            <span class="lblError Lablefailure HideControl "></span>
                            <asp:TextBox ID="txtPermanentMobileNo" class="form-control  Numeric" MaxLength="10" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Email id</label>
                            <span class="lblError Lablefailure HideControl"></span>
                            <asp:TextBox ID="txtPermanentEmailId" class="form-control" MaxLength="100" runat="server" />
                        </div>
                    </div>
                    <div class="col-md-4 premanent">
                        <div class="form-group">
                            <label>Fax No</label>
                            <asp:TextBox ID="txtPermanentFaxNo" class="form-control  Numeric" MaxLength="15" runat="server" />
                        </div>
                    </div>

                </div>
            </div>
            <%--      <uc:PopupManage ID="pmp" runat="server" Visible="false" />--%>
            <%--policy details--%>
            <%--      <div class="box box-warning box-solid PolicyDetails HideControl">
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <h3 class="box-title "><span id="Span1">Policy Premium Details</span></h3>
                            <i class="fa fa-hourglass-start"></i>
                        </div>
                    </div>
                </div>
                <div class="box-body details">
                    <div class="col-md-12">
                        <div class="col-md-12">
                            <div class="form-group">
                                There is change in policy
                            </div>
                        </div>
                        <div class="col-md-12 table-responsive" id="divPolicyTable">
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                If you want to proceed <a href="javascript:fnCreateUpdateClient();">Click Here</a>
                            </div>
                        </div>
                        <div class="row"></div>
                    </div>
                </div>
            </div>--%>

            <%--dedupe details--%>
            <%--  <div class="box box-warning box-solid DedupeDetails HideControl">
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-9">
                            <h3 class="box-title "><span id="Span2">Dedupe Details</span></h3>
                            <i class="fa fa-hourglass-start"></i>
                        </div>
                    </div>
                </div>
            </div>--%>
        </div>
    </form>
</body>
</html>
