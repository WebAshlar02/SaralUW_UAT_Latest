<%--//*************************************************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//*****************************************************************************************************/      
//*                  I N F O R M A T I O N                                       
//***************************************************************************************************** 
// Sr. No.              : 26
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bibekananda Nanda [1103055]               
// BRD/CR/Codesk No/Win : ///27626    
// Date Of Creation     : 04.05.2020            
// Description          : 1.All the PAN card details to be saved and displayed after NSDL validation)
//******************************************************************************************************//--%>
<%--//*************************************************************************************************
//*                      FUTURE GENERALI INDIA                        *    
//*****************************************************************************************************/      
//*                  I N F O R M A T I O N                                       
//***************************************************************************************************** 
// Sr. No.              : 27
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Brijesh Pandey              
// BRD/CR/Codesk No/Win : ///29084    
// Date Of Creation     : 08-06-2021.03.2020            
// Description          : Add Relation with Staff field against Is Staff flag 
//******************************************************************************************************//--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Appcode/Bpmuwmodule.master" AutoEventWireup="true"
    ClientIDMode="Static" CodeFile="RevivalUwdecision.aspx.cs" Inherits="Appcode_Default" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Appcode/Bpmuwmodule.master" %>
<%--<%@ Register Src="~/UserControl/uc_Updateprogress.ascx" TagPrefix="uc1" TagName="uc_Updateprogress" %>--%>
<%@ Register Src="~/UserControl/PopupManageProposar.ascx" TagPrefix="uc2" TagName="PopupManageProposar" %>
<%@ Register Src="~/UserControl/PopupDoc.ascx" TagPrefix="Docs" TagName="PendingDocs" %>
<%@ Register Src="~/UserControl/HealthProfile.ascx" TagPrefix="uc3" TagName="HealthProfile" %>
<%@ Register Src="~/UserControl/AmlOffline.ascx" TagPrefix="OfflineAml" TagName="AmlOffline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<uc1:uc_Updateprogress runat="server" ID="uc_Updateprogress" />--%>
    <style type="text/css">
        /*.loader {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url(/dist/img/loading1.gif) 50% 50% no-repeat white;
            /* opacity: .75; /
        opacity: .90;
        }

        */
        /*added by shri on 14 aug 17 to show div*/
        .overlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1000;
        }
        /*end here*/

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

    <script type="text/javascript">

        function SetTarget() {

            //document.forms[0].target = "_blank";
            document.getElementById["btnMedDataentry"].target = "_blank";
        }

    </script>
    <script language="javascript" type="text/javascript">
        function OpenPage(strval) {
            window.open(strval);
        }
    </script>
    <script type="text/javascript">

        //comment reset 
        function fnCommentsReset() {
            $('#<%=txtUWComments.ClientID%>').val('')
            return false;
        }
        //
        function fnChaneNsap() {
            if (confirm('Do you want to change NSAP loading')) {
                return true;
            }
            else {
                return false;
            }
        }
        //TO SHOW COMMON ERROR POPUOP
        function ShowModalPopup(message) {
            $('#lblErrorinfo').html(message);
            $('#myModal').modal();
        }
        //function hideloading() {
        //    $("#loaderdiv").hide();
        //}

        //function showloading() {
        //    $("#loaderdiv").show();
        //}

        //$(window).load(function () {

        //    $('#loaderdiv').fadeOut();
        //});

        //function ShowModalPopupClose(message) {
        //    $('#lblErrorinfo').html(message);
        //    $('#myModal').modal();
        //    window.close();
        //}

        /*added by shri to set pan is valid status*/
        function fnHideShowErrorMsg(req) {
            $(req).parents('.error-container-div').find('.error-detail_div').toggle('HideControl');
        }
        function fnRequirementStatus(req) {
            //////debugger;
            if ($(req).parents('.RequirementBox').length > 0) {
                ////debugger;
                var length = $(req).parents('.RequirementBox').find('.RequirementStatus').length;
                var Wrs = 0;
                $(req).parents('.RequirementBox').find('.RequirementStatus').each(function () {
                    ////debugger;
                    if ($(this).val() == 'RS' || $(this).val() == 'W' || $(this).val() == 'R') {
                        Wrs = Wrs + 1;
                    }

                });

                if (length == Wrs) {
                    //////debugger;
                    if ($('.Decision').val('Pending').hasClass('lblLable')) {
                        $('.Decision').val('0');
                        $('.Decision').removeClass('lblLable');

                    }
                }
                else {
                    //debugger;
                    $('.Decision').val('Pending');
                    $('.Decision').addClass('lblLable');
                }
            }
        }

        function fnChangePanDetails(e) {
            //debugger;
            if ($(e).is(":checked")) {
                $('#lblPanIsValidated').html('Manual Match');
                $('#lblPanIsValidated').val('Manual Match');
                $('#lblPanIsValidated').text('Manual Match');
            }
            else {
                $('#lblPanIsValidated').html('');
                $('#lblPanIsValidated').val('');
            }
            return false;
        }
        /*added by shri on 13 sept 17 script of pan hide show*/
        function fnCngDdlPreissueStatus() {
            var len = $('.ddlPreissueVal').length;
            var a = 0;
            $('.ddlPreissueVal').each(function () {
                //debugger;
                if ($(this).val() == "1") {
                    a++;
                }
            }
            );
            //show button
            if (a == len) {
                $('.system').removeClass("HideControl");
            }
            //hide button
            else {
                $('.system').addClass("HideControl");
            }
        }

        function fnCheckPanType() {
            if ($('#chkForm16Copy').is(':checked')) {
                $('#divPanValidation').addClass('HideControl');
                $('#divPanManipulate').addClass('HideControl');
                $('.panresopnse').addClass('HideControl');
            }
            else if ($('#chkPanCopy').is(':checked')) {
                $('#divPanValidation').removeClass('HideControl');
                $('#divPanManipulate').removeClass('HideControl');
                $('.panresopnse').removeClass('HideControl');
            }
        }
        /*added by shri on 29 aug 17 script of health profile user control*/
        function fnShowAdjDiv(a) {
            //debugger;
            if ($(a).find('input[type="radio"]:checked').val() == 'No') {
                //$('#divHideWeightCause').addClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
            }
            else {
                //$('#divHideWeightCause').removeClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
            }
        }

        function fnShowAdjDivDdl(a) {
            //debugger;
            if ($(a).val() == 'No') {
                $(a).parent().parent().next('.HideShowDiv').addClass('HideControl');
            }
            else {
                $(a).parent().parent().next('.HideShowDiv').removeClass('HideControl');
            }
        }

        function fnShowAdjDivOnYesNo(a) {
            //debugger;
            if ($(a).find('input[type="radio"]:checked').val() == 'No') {
                $(a).parent().parent().closest('div').find('.HideShowDivOnYes').addClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDivOnNo').removeClass('HideControl');
            }
            else {
                $(a).parent().parent().closest('div').find('.HideShowDivOnYes').removeClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
                $(a).parent().parent().closest('div').find('.HideShowDivOnNo').addClass('HideControl');
            }
        }

        function fnShowAsset(a) {
            var count = 0;
            $(a).find('input[type="checkbox"]').each(function () {
                if ($(this).val() == "Life Insurance" && $(this).is(':checked') == true) {
                    count++;
                    return;
                }
                if ($(this).val() == "Health Insurance" && $(this).is(':checked') == true) {
                    count++;
                    return;
                }

            })
            if (count > 0) {
                $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
            }
            else {
                $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
            }
        }

        function fnLoaderShow() {
            $('#loaderdiv').fadeIn();
        }

        function fnCollapsedBox(req) {
            //debugger;
            //Find the box parent        
            var box = $(req).parents(".box").first();
            //Find the body and the footer
            var bf = box.find(".box-warning , .box-solid");
            if (!box.hasClass("collapsed-box")) {
                box.addClass("collapsed-box");
                bf.slideUp();
            } else {
                box.removeClass("collapsed-box");
                bf.slideDown();
            }
        }

        function fnHideShowGrid() {
            if ($('#ddlAutoPaytype').val() == 'ECS') {
                $('#divMandate').removeClass('HideControl');
                $('#divMandateecs').removeClass('HideControl');
                $('#divMandatesi').addClass('HideControl');
            }
            else if ($('#ddlAutoPaytype').val() == 'SI') {
                $('#divMandate').removeClass('HideControl');
                $('#divMandateecs').addClass('HideControl');
                $('#divMandatesi').removeClass('HideControl');
            }
            else {
                $('#divMandate').addClass('HideControl');
                $('#divMandateecs').addClass('HideControl');
                $('#divMandatesi').addClass('HideControl');
            }
        }
        /*end here*/
        $(document).ready(function () {
            //$('#loaderdiv').hide();
            //$('#loaderdiv').css('display','none');




            $('.lnkButton').click(function () {
                $('#loaderdiv').fadeIn();
            });
            //$("#lnkCommondashbord").click();            
            fnRequirementStatus($('#ddlStatus'));
            /*added by shri on 06 sept 17 to show mandate Data*/
            //Mandate details Begins.
            $('#chkMandate').change(function () {
                if ($('#chkMandate').is(':checked')) {
                    $('#btnMandateDtlsSave').removeClass('HideControl');
                    $('#txtMandAccountType').removeClass('lblLable');
                    $('#txtMandAccountno').removeClass('lblLable');
                    $('#txtMandAccountholder').removeClass('lblLable');
                    $('#txtMandateassigndate').removeClass('lblLable');
                    $('#txtMandMicrocode').removeClass('lblLable');
                    $('#txtCreditcardno').removeClass('lblLable');
                    //$('#txtcreditcardType').removeClass('lblLable');
                    $('#txtValidupto').removeClass('lblLable');
                    $('#txtSiMicrCode').removeClass('lblLable');
                    $('#txtSiBankName').removeClass('lblLable');
                    $('#txtSiBranchCode').removeClass('lblLable');
                    $('#txtMandateSigndate').removeClass('lblLable');
                    $('#txtHolderName').removeClass('lblLable');
                    //$('#txtMandBankName').removeClass('lblLable');
                    //$('#txtMandBranchName').removeClass('lblLable');
                }
                else {
                    $('#btnMandateDtlsSave').addClass('HideControl');
                    $('#txtMandAccountType').addClass('lblLable');
                    $('#txtMandAccountno').addClass('lblLable');
                    $('#txtMandAccountholder').addClass('lblLable');
                    $('#txtMandateassigndate').addClass('lblLable');
                    $('#txtMandMicrocode').addClass('lblLable');
                    //$('#txtMandBankName').addClass('lblLable');
                    //$('#txtMandBranchName').addClass('lblLable');
                    $('#txtCreditcardno').addClass('lblLable');
                    //$('#txtcreditcardType').addClass('lblLable');
                    $('#txtValidupto').addClass('lblLable');
                    $('#txtSiMicrCode').addClass('lblLable');
                    $('#txtSiBankName').addClass('lblLable');
                    $('#txtSiBranchCode').addClass('lblLable');
                    $('#txtMandateSigndate').addClass('lblLable');
                    $('#txtHolderName').addClass('lblLable');
                    $('#Mandatedtls_containerBody').find('input[type="text"]').each(function () {
                        $(this).removeClass('EmptyVal');

                    });
                }
            });
            //mandate details End.

            $('#ddlAuthenticate').change(function () {

                if ($('#ddlAuthenticate').val() == 'Yes') {
                    $('.divFundTransfer').removeClass('HideControl');
                }
                else {
                    $('#btnSaveJournal').addClass('HideControl');
                    $('.divFundTransfer').addClass('HideControl');
                }


            });

            // set date picker format through jquery.
            $('#txtRcdreq').datepicker({
                autoclose: true,
                format: 'dd-mm-yyyy',
                onSelect: function (date) {
                }

            });




            $(function () {

                debugger;
                if ($("#txtPremPayingStatus").val() == "Lapsed") {
                    $("#nonRevivalPanel").hide();
                }

                $("#txtBnkIfsccode").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Uwdecision.aspx/GetAutoCompleteData",
                            data: "{'IFSC':'" + document.getElementById('txtBnkIfsccode').value + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                alert('no data');
                            }
                        });
                    },
                    select: function (event, ui) {
                        var ifscNo = ui.item.value;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Uwdecision.aspx/GetTextChanged",
                            dataType: "json",
                            data: "{'Ifsc':'" + ifscNo + "'}",
                            success: function (data) {
                                $('#txtBnkBankname').val(data.d[0]);
                                $('#txtBnkBranchname').val(data.d[1]);
                                $('#txtBnkBankaddress').val(data.d[2])
                            },
                            error: function (result) {
                            }
                        });
                    }
                });
            });

            $('#txtBnkIfsccode').blur(function (e) {
                var ifscNo = $('#txtBnkIfsccode').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "Uwdecision.aspx/GetTextChanged",
                    dataType: "json",
                    data: "{'Ifsc':'" + ifscNo + "'}",
                    success: function (data) {
                        $('#txtBnkBankname').val(data.d[0]);
                        $('#txtBnkBranchname').val(data.d[1]);
                        $('#txtBnkBankaddress').val(data.d[2])
                    },
                    error: function (result) {
                    }
                });

            });

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

            //Pan Number validation

            $('.paccNum').keypress(function (e) {
                var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                if (keycode == 9 || keycode == 8) {
                    return true;
                }
                else {
                    var ctrl = $(this)[0];
                    if ((keycode > 64 && keycode < 91) || (keycode > 95 && keycode < 123) || (keycode >= 48 && keycode <= 57)) {
                        var startPos = ctrl.selectionStart;
                        var endPos = ctrl.selectionEnd;
                        startPos = endPos;
                        var proceed = false;
                        var charCode = (e.charCode) ? e.charCode : e.keyCode;
                        var keyCall = keycode;
                        switch (startPos) {
                            case 0:
                            case 1:
                            case 2:
                            case 4:
                            case 9:
                                if (keycode >= 96 && keycode <= 122) {
                                    //proceed = false; // 
                                    keycode = keycode - 32;
                                }
                                if (keycode > 64 && keycode < 91) {
                                    keyCall = keycode;
                                    proceed = true;
                                }
                                break;
                            case 3:
                                if (keycode == 112 || keycode == 80)
                                    proceed = true;
                                break;
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                                //if (keycode >= 96 && keycode <= 105) {
                                //keycode = keycode - 48;
                                //}
                                if (keycode >= 48 && keycode <= 57) {
                                    keyCall = keycode;
                                    proceed = true;
                                }
                                break;
                        }
                        if (proceed) {
                            var value = '';
                            var txtvalue = ctrl.value.trim();
                            if (txtvalue.length <= ctrl.maxLength) {
                                if (startPos == txtvalue.length) {
                                    if (startPos == ctrl.maxLength) {
                                        for (var i = 0; i < txtvalue.length - 1; i++) {
                                            value = value + txtvalue[i];
                                        }
                                        value = value + String.fromCharCode(keyCall);;
                                    }
                                    else
                                        value = txtvalue + String.fromCharCode(keyCall);
                                }
                                else {
                                    txtvalue = txtvalue.split('');
                                    if (txtvalue.length < ctrl.maxLength) {
                                        for (var i = 0; i < txtvalue.length; i++) {
                                            if (startPos == endPos && i == startPos) {
                                                value = value + String.fromCharCode(keyCall);
                                            }
                                            value = value + txtvalue[i];
                                        }
                                    }
                                    else {
                                        for (var i = 0; i < txtvalue.length; i++) {
                                            if (startPos == endPos && i == startPos) {
                                                value = value + String.fromCharCode(keyCall);
                                            }
                                            else {
                                                if (i >= startPos && i <= endPos) {
                                                    if (i == startPos)
                                                        value = value + String.fromCharCode(keyCall);
                                                }
                                                else {
                                                    value = value + txtvalue[i];
                                                }
                                            }
                                        }
                                    }
                                }
                                if (value != ' ') {
                                    ctrl.value = value.trim();
                                }
                            }
                            return false;
                        }
                        else {
                            return false;
                        }
                    }
                    else
                        return false;
                }
            });

            $('.paccNum').keyup(function () {
                this.value = this.value.toUpperCase();
            });


            //Exit Pan Validation

            //alpha numeric data without space.
            $(".alphanumeric").keypress(function (e) {
                var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57));
            });

            //Pan Number validation

            $('#btnFundDtlsSave').addClass('HideControl');
            //$('#divExistingloading').toggle();
            //$('#btnViewExistingLoad').click(function ()
            //{
            //    $('#divExistingloading').toggle();

            //});
            // Event Binding Function Begins.
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('.lnkButton').click(function () {
                    $('#loaderdiv').fadeIn();
                });
                //comment reset 
                function fnCommentsReset() {
                    $('#<%=txtUWComments.ClientID%>').val('')
                    return false;
                }
                /*added by shri to set pan is valid status*/
                function fnCngDdlPreissueStatus() {
                    //debugger;
                    //ddlPreissueVal
                }
                function fnChangePanDetails(e) {
                    //debugger;
                    if ($(e).is(":checked")) {
                        $('#lblPanIsValidated').html('Manual Match');
                        $('#lblPanIsValidated').val('Manual Match');
                        $('#lblPanIsValidated').text('Manual Match');
                    }
                    else {
                        $('#lblPanIsValidated').html('');
                        $('#lblPanIsValidated').val('');
                    }
                    return false;
                }
                /*added by shri on 06 sept 17 to show mandate Data*/
                //Mandate details Begins.
                $('#chkMandate').change(function () {
                    if ($('#chkMandate').is(':checked')) {
                        $('#btnMandateDtlsSave').removeClass('HideControl');
                        $('#txtMandAccountType').removeClass('lblLable');
                        $('#txtMandAccountno').removeClass('lblLable');
                        $('#txtMandAccountholder').removeClass('lblLable');
                        $('#txtMandateassigndate').removeClass('lblLable');
                        $('#txtMandMicrocode').removeClass('lblLable');
                        //$('#txtMandBankName').removeClass('lblLable');
                        //$('#txtMandBranchName').removeClass('lblLable');
                        $('#txtCreditcardno').removeClass('lblLable');
                        //$('#txtcreditcardType').removeClass('lblLable');
                        $('#txtValidupto').removeClass('lblLable');
                        $('#txtSiMicrCode').removeClass('lblLable');
                        $('#txtSiBankName').removeClass('lblLable');
                        $('#txtSiBranchCode').removeClass('lblLable');
                        $('#txtMandateSigndate').removeClass('lblLable');
                        $('#txtHolderName').removeClass('lblLable');
                    }
                    else {
                        $('#btnMandateDtlsSave').addClass('HideControl');
                        $('#txtMandAccountType').addClass('lblLable');
                        $('#txtMandAccountno').addClass('lblLable');
                        $('#txtMandAccountholder').addClass('lblLable');
                        $('#txtMandateassigndate').addClass('lblLable');
                        $('#txtMandMicrocode').addClass('lblLable');
                        //$('#txtMandBankName').addClass('lblLable');
                        //$('#txtMandBranchName').addClass('lblLable');
                        $('#txtCreditcardno').addClass('lblLable');
                        //$('#txtcreditcardType').addClass('lblLable');
                        $('#txtValidupto').addClass('lblLable');
                        $('#txtSiMicrCode').addClass('lblLable');
                        $('#txtSiBankName').addClass('lblLable');
                        $('#txtSiBranchCode').addClass('lblLable');
                        $('#txtMandateSigndate').addClass('lblLable');
                        $('#txtHolderName').addClass('lblLable');
                        $('#Mandatedtls_containerBody').find('input[type="text"]').each(function () {
                            $(this).removeClass('EmptyVal');

                        });
                    }
                });
                //mandate details End.
                /*added by shri on 29 aug 17 script of health profile user control*/
                function fnShowAdjDiv(a) {
                    //debugger;
                    if ($(a).find('input[type="radio"]:checked').val() == 'No') {
                        //$('#divHideWeightCause').addClass('HideControl');
                        $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
                    }
                    else {
                        //$('#divHideWeightCause').removeClass('HideControl');
                        $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
                    }
                }
                function fnShowAdjDivDdl(a) {
                    //debugger;
                    if ($(a).val() == 'No') {
                        $(a).parent().parent().next('.HideShowDiv').addClass('HideControl');
                    }
                    else {
                        $(a).parent().parent().next('.HideShowDiv').removeClass('HideControl');
                    }
                }
                function fnShowAdjDivOnYesNo(a) {
                    //debugger;
                    if ($(a).find('input[type="radio"]:checked').val() == 'No') {
                        $(a).parent().parent().closest('div').find('.HideShowDivOnYes').addClass('HideControl');
                        $(a).parent().parent().closest('div').find('.HideShowDivOnNo').removeClass('HideControl');
                    }
                    else {
                        $(a).parent().parent().closest('div').find('.HideShowDivOnYes').removeClass('HideControl');
                        $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
                        $(a).parent().parent().closest('div').find('.HideShowDivOnNo').addClass('HideControl');
                    }
                }
                function fnShowAsset(a) {
                    var count = 0;
                    $(a).find('input[type="checkbox"]').each(function () {
                        if ($(this).val() == "Life Insurance" && $(this).is(':checked') == true) {
                            count++;
                            return;
                        }
                        if ($(this).val() == "Health Insurance" && $(this).is(':checked') == true) {
                            count++;
                            return;
                        }

                    })
                    if (count > 0) {
                        $(a).parent().parent().closest('div').find('.HideShowDiv').removeClass('HideControl');
                    }
                    else {
                        $(a).parent().parent().closest('div').find('.HideShowDiv').addClass('HideControl');
                    }
                }

                function fnCollapsedBox(req) {
                    //debugger;
                    //Find the box parent        
                    var box = $(req).parents(".box").first();
                    //Find the body and the footer
                    var bf = box.find(".box-warning , .box-solid");
                    if (!box.hasClass("collapsed-box")) {
                        box.addClass("collapsed-box");
                        bf.slideUp();
                    } else {
                        box.removeClass("collapsed-box");
                        bf.slideDown();
                    }
                }
                /*end here*/
                /*added by shri on 03 july 17 to clear popup selection*/
                function fnClearPopupSelection() {
                    //debugger;
                    $('.box-bodyPopUp').find('input[type="text"]').val('');
                    $('.box-bodyPopUp').find('select').val('-1');
                    $('.DedupeDynamicRow').remove();
                    $('.DedupeDetails').addClass('HideControl');
                    $('.divClient').addClass('HideControl');
                    $('.PolicyDetails').addClass('HideControl');
                    $('.rdoRole').attr('checked', false);
                    $('.modal-backdrop').remove();
                    return false;
                    //$('#divUserControlModal').css("display", "block");		
                }
                function fnCloseClientPopup() {
                    $('#divUserControlModal').modal('toggle');
                    $('.modal-backdrop').remove();
                }
                function fnEnableDisable(req) {
                    //debugger;
                    if ($(req).is(':checked')) {
                        $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('input').removeClass('lblLable');
                        $(req).parent().parent().parent().find('.btnSave').parent().removeClass('HideControl');
                        $(req).parent().parent().parent().parent().parent().parent().find('.OnlyOneRider').removeClass('lblLable');
                    }
                    else {
                        $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('input').addClass('lblLable');
                        $(req).parent().parent().parent().find('.btnSave').parent().addClass('HideControl');
                        $(req).parent().parent().parent().parent().parent().parent().find('.OnlyOneRider').addClass('lblLable');
                    }
                }
                /*added by shri on 13 jan 18 to add javascript validation by new concept  */
                function fnEnableDisableNew(req) {
                    //debugger;
                    if ($(req).find('input[type="checkbox"]').is(':checked')) {
                        $(req).parents().find('.box-body').find('.ReadOnly').removeClass('lblLable');
                        $(req).parents().find('.box-body').find('.btnSave').removeClass('HideControl');
                    }
                    else {
                        $(req).parents().find('.box-body').find('.ReadOnly').addClass('lblLable');
                        $(req).parents().find('.box-body').find('.btnSave').addClass('HideControl');
                    }
                }
                /*end here*/
                function fnCheckNoOfSelection() {
                    $('.OnlyOneRider').each(function () {
                        if ($(this).find('input[type="checkbox"]').is(':checked')) {
                            //Count++;
                            if ($('#txtRiderSumAssure').val() == '') {
                                alert('Enter rider sum assured');
                                return false;
                            }
                        }
                    });
                }
                /*added by shri on 09 aug 17 to add pan card validation after partial post back*/
                $('.paccNum').keypress(function (e) {
                    var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                    if (keycode == 9 || keycode == 8) {
                        return true;
                    }
                    else {
                        var ctrl = $(this)[0];
                        if ((keycode > 64 && keycode < 91) || (keycode > 95 && keycode < 123) || (keycode >= 48 && keycode <= 57)) {
                            var startPos = ctrl.selectionStart;
                            var endPos = ctrl.selectionEnd;
                            startPos = endPos;
                            var proceed = false;
                            var charCode = (e.charCode) ? e.charCode : e.keyCode;
                            var keyCall = keycode;
                            switch (startPos) {
                                case 0:
                                case 1:
                                case 2:
                                case 4:
                                case 9:
                                    if (keycode >= 96 && keycode <= 122) {
                                        //proceed = false; // 
                                        keycode = keycode - 32;
                                    }
                                    if (keycode > 64 && keycode < 91) {
                                        keyCall = keycode;
                                        proceed = true;
                                    }
                                    break;
                                case 3:
                                    if (keycode == 112 || keycode == 80)
                                        proceed = true;
                                    break;
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                    //if (keycode >= 96 && keycode <= 105) {
                                    //keycode = keycode - 48;
                                    //}
                                    if (keycode >= 48 && keycode <= 57) {
                                        keyCall = keycode;
                                        proceed = true;
                                    }
                                    break;
                            }
                            if (proceed) {
                                var value = '';
                                var txtvalue = ctrl.value.trim();
                                if (txtvalue.length <= ctrl.maxLength) {
                                    if (startPos == txtvalue.length) {
                                        if (startPos == ctrl.maxLength) {
                                            for (var i = 0; i < txtvalue.length - 1; i++) {
                                                value = value + txtvalue[i];
                                            }
                                            value = value + String.fromCharCode(keyCall);;
                                        }
                                        else
                                            value = txtvalue + String.fromCharCode(keyCall);
                                    }
                                    else {
                                        txtvalue = txtvalue.split('');
                                        if (txtvalue.length < ctrl.maxLength) {
                                            for (var i = 0; i < txtvalue.length; i++) {
                                                if (startPos == endPos && i == startPos) {
                                                    value = value + String.fromCharCode(keyCall);
                                                }
                                                value = value + txtvalue[i];
                                            }
                                        }
                                        else {
                                            for (var i = 0; i < txtvalue.length; i++) {
                                                if (startPos == endPos && i == startPos) {
                                                    value = value + String.fromCharCode(keyCall);
                                                }
                                                else {
                                                    if (i >= startPos && i <= endPos) {
                                                        if (i == startPos)
                                                            value = value + String.fromCharCode(keyCall);
                                                    }
                                                    else {
                                                        value = value + txtvalue[i];
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (value != ' ') {
                                        ctrl.value = value.trim();
                                    }
                                }
                                return false;
                            }
                            else {
                                return false;
                            }
                        }
                        else
                            return false;
                    }
                });

                $('.paccNum').keyup(function () {
                    this.value = this.value.toUpperCase();
                });
                /*end here*/

                $('#txtRcdreq').datepicker({
                    autoclose: true,
                    format: 'dd-mm-yyyy'

                });

                //$('#btnViewExistingLoad').click(function () {
                //    $('#divExistingloading').toggle();

                //});

                //Autocomplete Text Begin

                $("#txtBnkIfsccode").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Uwdecision.aspx/GetAutoCompleteData",
                            data: "{'IFSC':'" + document.getElementById('txtBnkIfsccode').value + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                alert('no data');
                            }
                        });
                    },
                    select: function (event, ui) {
                        var ifscNo = ui.item.value;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "Uwdecision.aspx/GetTextChanged",
                            dataType: "json",
                            data: "{'Ifsc':'" + ifscNo + "'}",
                            success: function (data) {
                                $('#txtBnkBankname').val(data.d[0]);
                                $('#txtBnkBranchname').val(data.d[1]);
                                $('#txtBnkBankaddress').val(data.d[2])
                            },
                            error: function (result) {
                            }
                        });
                    }
                });
                //Autocomplete Text End
                $('#txtBnkIfsccode').blur(function (e) {
                    var ifscNo = $('#txtBnkIfsccode').val();
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Uwdecision.aspx/GetTextChanged",
                        dataType: "json",
                        data: "{'Ifsc':'" + ifscNo + "'}",
                        success: function (data) {
                            $('#txtBnkBankname').val(data.d[0]);
                            $('#txtBnkBranchname').val(data.d[1]);
                            $('#txtBnkBankaddress').val(data.d[2])
                        },
                        error: function (result) {
                        }
                    });

                });

                //Application details Begins.
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
                //alpha numeric data without space.
                $(".alphanumeric").keypress(function (e) {
                    var k = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
                    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 9 || k == 8 || (k >= 48 && k <= 57));
                });
                $('#chkReqDtls').change(function () {
                    if ($('#chkReqDtls').is(':checked')) {
                        //debugger;
                        $('#gvRequirmentDetails').removeClass('lblLable');
                        $('#btnRequirmentDtlsSave').removeClass('HideControl');
                        $('#btnReqaddrows').removeClass('HideControl');
                        $('#ddlCommonStatus').removeClass("lblLable");
                        $('#ddlCommonStatus').removeAttr("disabled");
                        $('#ddlRequirementMedicalReason').removeClass('lblLable');
                        //$('#lblfollowupDiscp').removeAttr("readonly");
                        //$('#gvRequirmentDetails').removeAttr("disabled");

                            <%-- $("#<%=gvRequirmentDetails.ClientID%> tr:has(td)").each(function () {
                            $(this).find('#ddlCategory').removeAttr("disabled");
                            $(this).find('#ddlCriteria').removeAttr("disabled");
                            $(this).find('#ddlLifeType').removeAttr("disabled");
                            $(this).find('#ddlStatus').removeAttr("disabled");
                        });--%>
                    }
                    else {
                        $('#gvRequirmentDetails').addClass('lblLable');
                        $('#divRequirementDetails').find('*').removeClass('EmptyVal');
                        $('#btnRequirmentDtlsSave').addClass('HideControl');
                        $('#btnReqaddrows').addClass('HideControl');
                        $('#ddlCommonStatus').attr("disabled", "disabled");
                        $('#ddlCommonStatus').addClass("lblLable");
                        $('#ddlRequirementMedicalReason').addClass('lblLable');
                        //$('#gvRequirmentDetails').attr("disabled", "disabled");

                            <%--$("#<%=gvRequirmentDetails.ClientID%> tr:has(td)").each(function () {
                            $(this).find('#ddlCategory').attr("disabled", "disabled");
                            $(this).find('#ddlCriteria').attr("disabled", "disabled");
                            $(this).find('#ddlLifeType').attr("disabled", "disabled");
                            $(this).find('#ddlStatus').attr("disabled", "disabled");
                        });--%>
                    }
                })

                //Journal Details
                $('#chkJournal').change(function () {
                    if ($('#chkJournal').is(':checked')) {
                        $('#txtJournalPassword').removeClass('lblLable');
                        //$('#ddlComment').removeClass('lblLable');
                        //$('#ddlComment').removeAttr("disabled");
                        //$('#ddlAuthenticate').removeAttr("disabled");
                        $('#btnSaveJournal').removeClass('HideControl');

                        //$('#txtUWComments').removeAttr("readonly");
                    }
                    else {
                        //$('#ddlAuthenticate').attr("disabled", "disabled");
                        $('#txtJournalPassword').addClass('lblLable');
                        $('#btnSaveJournal').addClass('HideControl');
                    }
                });


                $('#switch_havInsurance').change(function () {
                    if ($('#switch_havInsurance').is(':checked')) {
                        $('#txtRcdreq').removeClass('lblLable');
                        $('#ddlBkdateReason').removeClass('lblLable');
                        $('#btnAppDtlsSave').removeClass('HideControl');
                    }
                    else {
                        $('#txtRcdreq').addClass('lblLable');
                        $('#ddlBkdateReason').addClass('lblLable');
                        $('#btnAppDtlsSave').addClass('HideControl');
                        $("#ddlBkdateReason").removeClass('EmptyVal');
                        //$('#Appdtls_containerBody').find('input[type="text"]').each(function () {
                        $('#Appdtls_containerBody').find('.validate').each(function () {
                            var control = $.trim($(this).val());
                            if (control == '') {
                                flag = false;
                                $(this).addClass('EmptyVal');
                            }
                            else {
                                $(this).removeClass('EmptyVal');
                            }
                        });
                    }
                })

                $('#chkAppDtls').change(function () {
                    if ($('#chkAppDtls').is(':checked')) {
                        //debugger;

                        if ($('#switch_havInsurance').is(':checked')) {
                            $('#txtRcdreq').removeClass('lblLable');
                            $('#ddlBkdateReason').removeClass('lblLable');
                            $('#btnAppDtlsSave').removeClass('HideControl');
                        }
                        else {
                            $('#txtRcdreq').addClass('lblLable');
                            $('#ddlBkdateReason').addClass('lblLable');
                            $('#btnAppDtlsSave').addClass('HideControl');
                            $("#ddlBkdateReason").removeClass('EmptyVal');
                            //$('#Appdtls_containerBody').find('input[type="text"]').each(function () {
                            $('#Appdtls_containerBody').find('.validate').each(function () {
                                var control = $.trim($(this).val());
                                if (control == '') {
                                    flag = false;
                                    $(this).addClass('EmptyVal');
                                }
                                else {
                                    $(this).removeClass('EmptyVal');
                                }
                            });
                        }
                        //$('#ddlApplicationDetailsProposalType').removeClass('lblLable');
                        $('#lblBackdateCaption').removeClass('lblLable');
                        $('#Appdtls_container').addClass('opencontiner');
                        $('#btnAppDtlsSave').removeClass('HideControl');
                        $('#txtApplicationDetailsCibil').removeClass('lblLable');
                        $('#ddlRelationStaff').removeClass('lblLable');
                        //$('#ContentPlaceHolder1_txtAppno').removeClass('form-control lblLable');
                    }
                    else {
                        //$('#ddlApplicationDetailsProposalType').addClass('lblLable');
                        $('#divApplicationDetails').find('*').removeClass('EmptyVal');
                        $('#lblBackdateCaption').addClass('lblLable');
                        $('#Appdtls_container').removeClass('opencontiner');
                        $('#btnAppDtlsSave').addClass('HideControl');
                        $('#txtRcdreq').addClass('lblLable');
                        $('#ddlBkdateReason').addClass('lblLable');
                        $('#txtApplicationDetailsCibil').addClass('lblLable');
                        $('#ddlRelationStaff').addClass('lblLable');
                    }
                });
                //Application details End.
                //Agent Details Begins.
                $('#chkAgentDtls').change(function () {
                    if ($('#chkAgentDtls').is(':checked')) {
                        $('#txtAgentcode').removeClass('lblLable');
                        $('#txtFgempcode').removeClass('lblLable');
                        $('#txtPartnerempcode').removeClass('lblLable');
                        $('#txtcampaigncode').removeClass('lblLable');
                        $('#txtLeadcode').removeClass('lblLable');
                        $('#Agentdtls_container').addClass('opencontiner');
                        $('#btnAgentDtlsSave').removeClass('HideControl');
                        //$('#ContentPlaceHolder1_txtAppno').removeClass('form-control lblLable');
                    }
                    else {
                        $('#txtAgentcode').addClass('lblLable');
                        $('#txtFgempcode').addClass('lblLable');
                        $('#txtPartnerempcode').addClass('lblLable');
                        $('#txtcampaigncode').addClass('lblLable');
                        $('#txtLeadcode').addClass('lblLable');
                        $('#Agentdtls_container').removeClass('opencontiner');
                        $('#btnAgentDtlsSave').addClass('HideControl');
                    }
                });
                //Agent Details End.
                // Bank details Begins.
                $('#chkBankDtls').change(function () {
                    if ($('#chkBankDtls').is(':checked')) {
                        //$('#txtBnkClientname').removeClass('lblLable');
                        //$('#txtBnkClienttype').removeClass('lblLable');
                        //$('#txtBnkClientnumber').removeClass('lblLable');
                        $('#txtBnkIfsccode').removeClass('lblLable');
                        //$('#txtBnkBankname').removeClass('lblLable');
                        //$('#txtBnkBranchname').removeClass('lblLable');
                        //$('#txtBnkBankaddress').removeClass('lblLable');
                        $('#txtBnkBankaccno').removeClass('lblLable');
                        $('#Bnkdtls_container').addClass('opencontiner');
                        $('#btnBankDtlsSave').removeClass('HideControl');
                        //$('#txtBnkIfsccode').prop('readonly', false);
                        $("#txtBnkIfsccode").removeAttr("disabled");
                        $("#ddlAutoPaytype").removeClass('lblLable');
                        //$("#txtBnkBankaccno").removeAttr("disabled");
                        // $("#txtBnkBankaccno").attr("readonly", false);
                        //$('#ContentPlaceHolder1_txtAppno').removeClass('form-control lblLable');
                    }
                    else {
                        //$('#txtBnkClientname').addClass('lblLable');
                        //$('#txtBnkClienttype').addClass('lblLable');
                        //$('#txtBnkClientnumber').addClass('lblLable');
                        $('#txtBnkIfsccode').addClass('lblLable');
                        //$('#txtBnkBankname').addClass('lblLable');
                        //$('#txtBnkBranchname').addClass('lblLable');
                        // $('#txtBnkBankaddress').addClass('lblLable');
                        $('#txtBnkBankaccno').addClass('lblLable');
                        $('#Bnkdtls_container').removeClass('opencontiner');
                        $('#btnBankDtlsSave').addClass('HideControl');
                        $('#Bnkdtls_containerBody').find('input[type="text"]').each(function () {
                            $(this).removeClass('EmptyVal');
                            $("#ddlAutoPaytype").addClass('lblLable');
                        });
                    }
                });
                // Bank details End.
                //Pan details Begins.
                $('#chkPanDtls').change(function () {
                    if ($('#chkPanDtls').is(':checked')) {
                        $('#txtPannumber').removeClass('lblLable');
                        $('#Pandtls_container').addClass('opencontiner');
                        $('#btnPandtlsSave').removeClass('HideControl');
                        $('#btnValidatepan').removeClass('HideControl');
                        $('#txtPanComment').removeClass('lblLable');
                        ////$('#chkIsPanValidate').addClass('lblLable');
                    }
                    else {
                        $('#txtPannumber').addClass('lblLable');
                        $('#Pandtls_container').removeClass('opencontiner');
                        $('#btnPandtlsSave').addClass('HideControl');
                        $('#btnValidatepan').addClass('HideControl');
                        $('#txtPanComment').addClass('lblLable');
                        ////$('#chkIsPanValidate').addClass('lblLable');
                    }
                });
                //Pan details End.
                //Product Details Begins.
                $('#chkProductDtls').change(function () {
                    if ($('#chkProductDtls').is(':checked')) {
                        $('#Proddtls_container').addClass('opencontiner');
                        $('#btnProddtlsSave').removeClass('HideControl');
                        $('#btncalculatePrem_Prod').removeClass('HideControl');
                    }
                    else {
                        $('#Proddtls_container').addClass('opencontiner');
                        $('#btnProddtlsSave').addClass('HideControl');
                        $('#btncalculatePrem_Prod').addClass('HideControl');
                    }
                });
                $('#chkRiderEdit').change(function () {

                    if ($('#chkRiderEdit').is(':checked')) {
                        var gridView = $("[id*=gvRiderDtls]");
                        $("#<%=gvRiderDtls.ClientID%> tr:has(td)").each(function () {
                            $(this).find('#txtRiderSumAssure').removeClass('lblLable');
                            $(this).find('#txtRiderPremium').removeClass('lblLable');
                            $('#btnRiderDtlsSave').removeClass('HideControl');
                        });
                    }
                    else {
                        //alert('hi');
                        var gridView = $("[id*=gvRiderDtls]");
                        $("#<%=gvRiderDtls.ClientID%> tr:has(td)").each(function () {
                            $(this).find('#txtRiderSumAssure').addClass('lblLable');
                            $(this).find('#txtRiderPremium').addClass('lblLable');
                            $('#btnRiderDtlsSave').addClass('HideControl');
                        });
                    }
                });
                //Product Details End.
                // Fund Details Begins.
                $('#chkFunddtlsSave').change(function () {
                    if ($('#chkFunddtlsSave').is(':checked')) {
                        var gridView = $("[id*=gvFundDtls]");
                        $("#<%=gvFundDtls.ClientID%> tr:has(td)").each(function () {
                            $(this).find('#txtFundvalue').removeClass('lblLable');
                        });
                        $('#btnFundDtlsSave').removeClass('HideControl');
                    }
                    else {
                        // alert('hi');
                        var gridView = $("[id*=gvFundDtls]");
                        $("#<%=gvFundDtls.ClientID%> tr:has(td)").each(function () {
                            $(this).find('#txtFundvalue').addClass('lblLable');
                        });
                        $('#btnFundDtlsSave').addClass('HideControl');
                    }
                });
                // Fund Details End.

                //Loading Details Begins.
                $('#chkLoadingDtls').change(function () {

                    if ($('#chkLoadingDtls').is(':checked')) {
                        $('#btncalculatePrem_Load').removeClass('HideControl');
                        $('#ddlLoadRiderCode').removeClass('lblLable');
                        $('#ddlLoadType').removeClass('lblLable');
                        $('#ddlLoadRsn1').removeClass('lblLable');
                        $('#ddlLoadRsn2').removeClass('lblLable');
                        //$('#txtLoadPer').removeClass('lblLable');
                        //$('#txtRateAdjust').removeClass('lblLable');
                        //$('#ddlLoadFlatMortality').removeClass('lblLable');
                        $('#ddlLoadletterPrint').removeClass('lblLable');
                        $('#btnAddLoadingRow').removeClass('lblLable');
                        $('#btnLoadingDtlsSave').removeClass('HideControl');
                        $('#btnAddLoadingRow').removeClass('HideControl');
                        $('#btnViewExistingLoad').removeClass('HideControl');
                        // $('#txtRateAdjust').attr("readonly",false);
                        $('#ddlLoadRiderCode').removeAttr("disabled");
                        $('#ddlLoadType').removeAttr("disabled");
                        $('#ddlLoadRsn1').removeAttr("disabled");
                        $('#ddlLoadRsn2').removeAttr("disabled");
                        $('#ddlLoadFlatMortality').removeAttr("disabled");
                        $('#ddlLoadletterPrint').removeAttr("disabled");
                    }
                    else {
                        $('#btncalculatePrem_Load').addClass('HideControl');
                        $('#divLoadingDetails').find('*').removeClass('EmptyVal');
                        $('#ddlLoadRiderCode').addClass('lblLable');
                        $('#ddlLoadType').addClass('lblLable');
                        $('#ddlLoadRsn1').addClass('lblLable');
                        $('#ddlLoadRsn2').addClass('lblLable');
                        //$('#txtLoadPer').addClass('lblLable');
                        //$('#txtRateAdjust').addClass('lblLable');
                        //$('#ddlFrequency').addClass('lblLable');
                        $('#ddlLoadFlatMortality').addClass('lblLable');
                        $('#ddlLoadletterPrint').addClass('lblLable');
                        $('#btnLoadingDtlsSave').addClass('HideControl');
                        $('#btnAddLoadingRow').addClass('HideControl');
                        $('#btnViewExistingLoad').addClass('HideControl');

                        $('#ddlLoadRiderCode').attr("disabled", "disabled");
                        $('#ddlLoadType').attr("disabled", "disabled");
                        $('#ddlLoadRsn1').attr("disabled", "disabled");
                        $('#ddlLoadRsn2').attr("disabled", "disabled");
                        $('#ddlLoadFlatMortality').attr("disabled", "disabled");
                        $('#ddlLoadletterPrint').attr("disabled", "disabled");
                    }
                });
                //Loading Details End.

                //Comment Details Start
                $('#chkCommentDtls').change(function () {
                    if ($('#chkCommentDtls').is(':checked')) {
                        $('#txtUWComments').removeClass('lblLable');
                        $('#ddlComment').removeClass('lblLable');
                        $('#ddlComment').removeAttr("disabled");
                        $('#btnUWCommSave').removeClass('HideControl');
                        $('#txtUWComments').removeAttr("readonly");
                    }
                    else {
                        $('#divCommentDetails').find('*').removeClass('EmptyVal');
                        $('#txtUWComments').addClass('lblLable');
                        $('#ddlComment').attr("disabled", "disabled");
                        $('#ddlComment').addClass('lblLable');
                        $('#btnUWCommSave').addClass('HideControl');
                    }
                });
                //Comment Details End                
            });
            // Event Binding Funcation End.

            //Application details Begins.
            $('#chkReqDtls').change(function () {
                if ($('#chkReqDtls').is(':checked')) {
                    //debugger;
                    $('#gvRequirmentDetails').removeClass('lblLable');
                    $('#btnRequirmentDtlsSave').removeClass('HideControl');
                    $('#btnReqaddrows').removeClass('HideControl');
                    $('#ddlCommonStatus').removeClass("lblLable");
                    $('#ddlCommonStatus').removeAttr("disabled");
                    $('#ddlRequirementMedicalReason').removeClass('lblLable');
                    //$('#lblfollowupDiscp').removeAttr("readonly");
                    //$('#gvRequirmentDetails').removeAttr("disabled");
                        <%--$("#<%=gvRequirmentDetails.ClientID%> tr:has(td)").each(function () {
                        $(this).find('#ddlCategory').removeAttr("disabled");
                        $(this).find('#ddlCriteria').removeAttr("disabled");
                        $(this).find('#ddlLifeType').removeAttr("disabled");
                        $(this).find('#ddlStatus').removeAttr("disabled");
                    });--%>
                }
                else {
                    $('#gvRequirmentDetails').addClass('lblLable');
                    $('#btnRequirmentDtlsSave').addClass('HideControl');
                    $('#btnReqaddrows').addClass('HideControl');
                    $('#divRequirementDetails').find('*').removeClass('EmptyVal');
                    $('#ddlCommonStatus').attr("disabled", "disabled");
                    $('#ddlCommonStatus').addClass("lblLable");
                    $('#ddlRequirementMedicalReason').addClass('lblLable');
                    //$('#gvRequirmentDetails').attr("disabled", "disabled");
                        <%--$("#<%=gvRequirmentDetails.ClientID%> tr:has(td)").each(function () {
                        $(this).find('#ddlCategory').attr("disabled", "disabled");
                        $(this).find('#ddlCriteria').attr("disabled", "disabled");
                        $(this).find('#ddlLifeType').attr("disabled", "disabled");
                        $(this).find('#ddlStatus').attr("disabled", "disabled");
                    });--%>

                }
            })

            $('#switch_havInsurance').change(function () {
                if ($('#switch_havInsurance').is(':checked')) {
                    $('#txtRcdreq').removeClass('lblLable');
                    $('#ddlBkdateReason').removeClass('lblLable');
                    $('#btnAppDtlsSave').removeClass('HideControl');
                }
                else {
                    $('#txtRcdreq').addClass('lblLable');
                    $('#ddlBkdateReason').addClass('lblLable');
                    $('#btnAppDtlsSave').addClass('HideControl');
                    $("#ddlBkdateReason").removeClass('EmptyVal');
                    $('#Appdtls_containerBody').find('.validate').each(function () {
                        var control = $.trim($(this).val());
                        if (control == '') {
                            flag = false;
                            $(this).addClass('EmptyVal');
                        }
                        else {
                            $(this).removeClass('EmptyVal');
                        }
                    });
                }
            })

            $('#chkAppDtls').change(function () {
                if ($('#chkAppDtls').is(':checked')) {
                    //debugger;

                    if ($('#switch_havInsurance').is(':checked')) {
                        $('#txtRcdreq').removeClass('lblLable');
                        $('#ddlBkdateReason').removeClass('lblLable');
                        $('#btnAppDtlsSave').removeClass('HideControl');
                    }
                    else {
                        $('#txtRcdreq').addClass('lblLable');
                        $('#ddlBkdateReason').addClass('lblLable');
                        $('#btnAppDtlsSave').addClass('HideControl');
                        $("#ddlBkdateReason").removeClass('EmptyVal');
                        $('#Appdtls_containerBody').find('.validate').each(function () {
                            var control = $.trim($(this).val());
                            if (control == '') {
                                flag = false;
                                $(this).addClass('EmptyVal');
                            }
                            else {
                                $(this).removeClass('EmptyVal');
                            }
                        });
                    }
                    $('#lblBackdateCaption').removeClass('lblLable');
                    $('#Appdtls_container').addClass('opencontiner');
                    $('#btnAppDtlsSave').removeClass('HideControl');
                    //$('#ddlApplicationDetailsProposalType').removeClass('lblLable');
                    $('#txtApplicationDetailsCibil').removeClass('lblLable');
                    $('#ddlRelationStaff').removeClass('lblLable');
                    //$('#ContentPlaceHolder1_txtAppno').removeClass('form-control lblLable');
                }
                else {
                    //$('#txtAppno').addClass('lblLable');
                    //$('#txtPolno').addClass('lblLable');
                    //$('#txtAppsigndate').addClass('lblLable');
                    $('#divApplicationDetails').find('*').removeClass('EmptyVal');
                    $('#lblBackdateCaption').addClass('lblLable');
                    $('#Appdtls_container').removeClass('opencontiner');
                    $('#btnAppDtlsSave').addClass('HideControl');
                    $('#txtRcdreq').addClass('lblLable');
                    $('#ddlBkdateReason').addClass('lblLable');
                    //$('#ddlApplicationDetailsProposalType').addClass('lblLable');
                    $('#txtApplicationDetailsCibil').addClass('lblLable');
                    $('#ddlRelationStaff').addClass('lblLable');
                }
            });
            //Application details End.
            //Agent Details Begins.
            $('#chkAgentDtls').change(function () {
                if ($('#chkAgentDtls').is(':checked')) {
                    $('#txtAgentcode').removeClass('lblLable');
                    $('#txtFgempcode').removeClass('lblLable');
                    $('#txtPartnerempcode').removeClass('lblLable');
                    $('#txtcampaigncode').removeClass('lblLable');
                    $('#txtLeadcode').removeClass('lblLable');
                    $('#Agentdtls_container').addClass('opencontiner');
                    $('#btnAgentDtlsSave').removeClass('HideControl');
                    //$('#ContentPlaceHolder1_txtAppno').removeClass('form-control lblLable');
                }
                else {
                    $('#txtAgentcode').addClass('lblLable');
                    $('#txtFgempcode').addClass('lblLable');
                    $('#txtPartnerempcode').addClass('lblLable');
                    $('#txtcampaigncode').addClass('lblLable');
                    $('#txtLeadcode').addClass('lblLable');
                    $('#Agentdtls_container').removeClass('opencontiner');
                    $('#btnAgentDtlsSave').addClass('HideControl');
                }
            });
            //Agent Details End.
            // Bank details Begins.
            $('#chkBankDtls').change(function () {
                if ($('#chkBankDtls').is(':checked')) {
                    //$('#txtBnkClientname').removeClass('lblLable');
                    //$('#txtBnkClienttype').removeClass('lblLable');
                    //$('#txtBnkClientnumber').removeClass('lblLable');
                    $('#txtBnkIfsccode').removeClass('lblLable');
                    //$('#txtBnkBankname').removeClass('lblLable');
                    //$('#txtBnkBranchname').removeClass('lblLable');
                    //$('#txtBnkBankaddress').removeClass('lblLable');
                    $('#txtBnkBankaccno').removeClass('lblLable');
                    $('#Bnkdtls_container').addClass('opencontiner');
                    $('#btnBankDtlsSave').removeClass('HideControl');
                    $("#txtBnkIfsccode").removeAttr("disabled");
                    $("#ddlAutoPaytype").removeClass('lblLable');
                    //$("#txtBnkBankaccno").removeAttr("disabled");
                    //$('#ContentPlaceHolder1_txtAppno').removeClass('form-control lblLable');

                }
                else {
                    //$('#txtBnkClientname').addClass('lblLable');
                    //$('#txtBnkClienttype').addClass('lblLable');
                    //$('#txtBnkClientnumber').addClass('lblLable');
                    $('#txtBnkIfsccode').addClass('lblLable');
                    //$('#txtBnkBankname').addClass('lblLable');
                    //$('#txtBnkBranchname').addClass('lblLable');
                    // $('#txtBnkBankaddress').addClass('lblLable');
                    $('#txtBnkBankaccno').addClass('lblLable');
                    $('#Bnkdtls_container').removeClass('opencontiner');
                    $('#btnBankDtlsSave').addClass('HideControl');
                    $('#Bnkdtls_containerBody').find('input[type="text"]').each(function () {
                        $(this).removeClass('EmptyVal');
                        $("#ddlAutoPaytype").addClass('lblLable');
                    });
                }
            });
            // Bank details End.
            //Pan details Begins.
            $('#chkPanDtls').change(function () {

                if ($('#chkPanDtls').is(':checked')) {
                    $('#txtPannumber').removeClass('lblLable');
                    $('#Pandtls_container').addClass('opencontiner');
                    $('#btnPandtlsSave').removeClass('HideControl');
                    $('#btnValidatepan').removeClass('HideControl');
                    $('#txtPanComment').removeClass('lblLable');
                    //$('#chkIsPanValidate').addClass('lblLable');
                }
                else {
                    $('#txtPannumber').addClass('lblLable');
                    $('#Pandtls_container').removeClass('opencontiner');
                    $('#btnPandtlsSave').addClass('HideControl');
                    $('#btnValidatepan').addClass('HideControl');
                    $('#txtPanComment').addClass('lblLable');
                    //$('#chkIsPanValidate').addClass('lblLable');
                }
            });
            //Pan details End.
            //Product Details Begins.
            $('#chkProductDtls').change(function () {
                if ($('#chkProductDtls').is(':checked')) {
                    $('#Proddtls_container').addClass('opencontiner');
                    $('#btnProddtlsSave').removeClass('HideControl');
                    $('#btncalculatePrem_Prod').removeClass('HideControl');
                }
                else {
                    $('#Proddtls_container').addClass('opencontiner');
                    $('#btnProddtlsSave').addClass('HideControl');
                    $('#btncalculatePrem_Prod').addClass('HideControl');
                }
            });

            $('#chkRiderEdit').change(function () {

                if ($('#chkRiderEdit').is(':checked')) {
                    var gridView = $("[id*=gvRiderDtls]");
                    $("#<%=gvRiderDtls.ClientID%> tr:has(td)").each(function () {
                        $(this).find('#txtRiderSumAssure').removeClass('lblLable');
                        $(this).find('#txtRiderPremium').removeClass('lblLable');
                        $('#btnRiderDtlsSave').removeClass('HideControl');
                    });
                }
                else {
                    //alert('hi');
                    var gridView = $("[id*=gvRiderDtls]");
                    $("#<%=gvRiderDtls.ClientID%> tr:has(td)").each(function () {
                        $(this).find('#txtRiderSumAssure').addClass('lblLable');
                        $(this).find('#txtRiderPremium').addClass('lblLable');
                        $('#btnRiderDtlsSave').addClass('HideControl');
                    });
                }
            });
            //Product Details End.
            // Fund Details Begins.
            $('#chkFunddtlsSave').change(function () {

                if ($('#chkFunddtlsSave').is(':checked')) {
                    var gridView = $("[id*=gvFundDtls]");
                    $("#<%=gvFundDtls.ClientID%> tr:has(td)").each(function () {
                        $(this).find('#txtFundvalue').removeClass('lblLable');
                    });
                    $('#btnFundDtlsSave').removeClass('HideControl');
                }
                else {
                    // alert('hi');
                    var gridView = $("[id*=gvFundDtls]");
                    $("#<%=gvFundDtls.ClientID%> tr:has(td)").each(function () {
                        $(this).find('#txtFundvalue').addClass('lblLable');
                    });
                    $('#btnFundDtlsSave').addClass('HideControl');
                }
            });
            // Fund Details End.
            //Loading Details Begins.
            $('#chkLoadingDtls').change(function () {

                if ($('#chkLoadingDtls').is(':checked')) {
                    $('#btncalculatePrem_Load').removeClass('HideControl');
                    $('#ddlLoadRiderCode').removeClass('lblLable');
                    $('#ddlLoadType').removeClass('lblLable');
                    $('#ddlLoadRsn1').removeClass('lblLable');
                    $('#ddlLoadRsn2').removeClass('lblLable');
                    //$('#txtLoadPer').removeClass('lblLable');
                    //$('#txtRateAdjust').removeClass('lblLable');
                    //$('#ddlLoadFlatMortality').removeClass('lblLable');
                    $('#ddlFrequency').removeClass('lblLable');
                    $('#ddlLoadletterPrint').removeClass('lblLable');
                    $('#btnAddLoadingRow').removeClass('lblLable');
                    $('#btnLoadingDtlsSave').removeClass('HideControl');
                    $('#btnAddLoadingRow').removeClass('HideControl');
                    $('#btnViewExistingLoad').removeClass('HideControl');
                    //$('#txtRateAdjust').attr("readonly", false);
                    $('#ddlLoadRiderCode').removeAttr("disabled");
                    $('#ddlLoadType').removeAttr("disabled");
                    $('#ddlLoadRsn1').removeAttr("disabled");
                    $('#ddlLoadRsn2').removeAttr("disabled");
                    $('#ddlLoadFlatMortality').removeAttr("disabled");
                    $('#ddlLoadletterPrint').removeAttr("disabled");
                }
                else {
                    $('#btncalculatePrem_Load').addClass('HideControl');
                    $('#divLoadingDetails').find('*').removeClass('EmptyVal');
                    $('#ddlLoadRiderCode').addClass('lblLable');
                    $('#ddlLoadType').addClass('lblLable');
                    $('#ddlLoadRsn1').addClass('lblLable');
                    $('#ddlLoadRsn2').addClass('lblLable');
                    //$('#txtLoadPer').addClass('lblLable');
                    //$('#txtRateAdjust').addClass('lblLable');
                    $('#ddlFrequency').addClass('lblLable');
                    //$('#ddlLoadFlatMortality').addClass('lblLable');
                    $('#ddlLoadletterPrint').addClass('lblLable');
                    $('#btnLoadingDtlsSave').addClass('HideControl');
                    $('#btnAddLoadingRow').addClass('HideControl');
                    $('#btnViewExistingLoad').addClass('HideControl');

                    $('#ddlLoadRiderCode').attr("disabled", "disabled");
                    $('#ddlLoadType').attr("disabled", "disabled");
                    $('#ddlLoadRsn1').attr("disabled", "disabled");
                    $('#ddlLoadRsn2').attr("disabled", "disabled");
                    $('#ddlLoadFlatMortality').attr("disabled", "disabled");
                    $('#ddlLoadletterPrint').attr("disabled", "disabled");
                }
            });
            //Loading Details End.

            //Comment Details Start
            $('#chkCommentDtls').change(function () {
                if ($('#chkCommentDtls').is(':checked')) {
                    $('#txtUWComments').removeClass('lblLable');
                    $('#ddlComment').removeClass('lblLable');
                    $('#ddlComment').removeAttr("disabled");
                    $('#btnUWCommSave').removeClass('HideControl');
                    $('#txtUWComments').removeAttr("readonly");
                }
                else {
                    $('#divCommentDetails').find('*').removeClass('EmptyVal');
                    $('#txtUWComments').addClass('lblLable');
                    $('#ddlComment').addClass('lblLable');
                    $('#ddlComment').attr("disabled", "disabled");
                    $('#btnUWCommSave').addClass('HideControl');
                }
            });
            //Comment Details End

            //Journal Details
            $('#chkJournal').change(function () {
                if ($('#chkJournal').is(':checked')) {
                    $('#txtJournalPassword').removeClass('lblLable');
                    //$('#ddlComment').removeClass('lblLable');
                    //$('#ddlComment').removeAttr("disabled");
                    //$('#ddlAuthenticate').removeAttr("disabled");
                    $('#btnSaveJournal').removeClass('HideControl');

                    //$('#txtUWComments').removeAttr("readonly");
                }
                else {
                    //$('#ddlAuthenticate').attr("disabled", "disabled");
                    $('#txtJournalPassword').addClass('lblLable');
                    $('#btnSaveJournal').addClass('HideControl');
                }
            });

        });
        $(window).load(function () {
            //$('#ldrModal').fadeOut();
        });
        /*added by shri on 03 july 17 to clear popup selection*/
        function fnClearPopupSelection() {
            $('.box-bodyPopUp').find('input[type="text"]').val('');
            $('.box-bodyPopUp').find('select').val('-1');
            $('.DedupeDynamicRow').remove();
            $('.DedupeDetails').addClass('HideControl');
            $('.divClient').addClass('HideControl');
            $('.PolicyDetails').addClass('HideControl');
            $('.rdoRole').attr('checked', false);
        }

        function fnCloseClientPopup() {
            $('#divUserControlModal').modal('toggle');
            $('.modal-backdrop').remove();
        }
        function fnEnableDisable(req) {
            //debugger;
            if ($(req).is(':checked')) {
                $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('input').removeClass('lblLable');
                $(req).parent().parent().parent().find('.btnSave').parent().removeClass('HideControl');
                $(req).parent().parent().parent().parent().parent().parent().find('.OnlyOneRider').removeClass('lblLable');
            }
            else {
                $(req).parent().parent().parent().parent().parent().parent().find('.box-body').find('input').addClass('lblLable');
                $(req).parent().parent().parent().find('.btnSave').parent().addClass('HideControl');
                $(req).parent().parent().parent().parent().parent().parent().find('.OnlyOneRider').addClass('lblLable');
            }
        }
        /*added by shri on 13 jan 18 to add javascript validation by new concept  */
        function fnEnableDisableNew(req) {
            //debugger;
            if ($(req).find('input[type="checkbox"]').is(':checked')) {
                $(req).parents().find('.box-body').find('.ReadOnly').removeClass('lblLable');
                $(req).parents().find('.box-body').find('.btnSave').removeClass('HideControl');
            }
            else {
                $(req).parents().find('.box-body').find('.ReadOnly').addClass('lblLable');
                $(req).parents().find('.box-body').find('.btnSave').addClass('HideControl');
            }
        }
        /*end here*/
        function fnCheckNoOfSelection() {
            //var Count = 0;
            $('.OnlyOneRider').each(function () {
                if ($(this).find('input[type="checkbox"]').is(':checked')) {
                    //Count++;
                    if ($('#txtRiderSumAssure').val() == '') {
                        alert('Enter rider sum assured');
                        return false;
                    }
                }
            });
            //if (Count > 1) {
            //    alert('select only one rider');
            //    return false;
            //}
            //else if (Count <= 0) {
            //    alert('select atleast one rider');
            //    return false;
            //}
            //else if (Count == 1) {
            //    return true;
            //}

        }
        /*end here*/
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="loader" id="loaderdiv"></div>
    <div id="AuResult_ContainerBody" runat="server" class="col-md-12 HideControl">
        <div id="AuResult_Container" class="box box-warning box-solid">
            <div class="box-header with-border">
                <div class="col-md-12">
                    <div class="col-md-9">
                        <h3 class="box-title ">Auto UW Results</h3>
                        <i class="fa fa-thumbs-o-up"></i>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-18" style="float: right">
                            <%--<asp:LinkButton ID="lnkAwDtlsRefresh" OnClick="lnkAwDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>--%>
                        </div>
                        <div class="col-md-4" style="float: right">
                        </div>
                    </div>
                </div>
                <div class="box-tools ">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
            <asp:UpdatePanel ID="updAutoUwDetails" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box-body">
                        <div class="box-body">
                            <h4 class="box-title"><b>Application checklist</b></h4>
                            <div id="divAppChecklist" class="box-body" runat="server">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="imgPivcStatus" runat="server" ImageUrl="~/dist/img/Failuer.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Pivc Status</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/dist/img/Failuer.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label class="text-center">Agent signature verified</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="~/dist/img/Success.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Client dedupe Completed</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/dist/img/Failuer.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Is Bank Updated</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="imgPanVerified" runat="server" ImageUrl="~/dist/img/Failuer.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Is Pan Verified</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="imgAadharVerified" runat="server" ImageUrl="~/dist/img/Failuer.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Is Aadhar Verified</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row"></div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="Image6" runat="server" ImageUrl="~/dist/img/Failuer.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Proposer/Payor different from LA</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div style="text-align: center">
                                            <asp:Image ID="Image7" runat="server" ImageUrl="~/dist/img/Success.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Is nominee Exists</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div id="divimgekyc" runat="server" class="form-group HideControl">
                                        <div style="text-align: center">
                                            <asp:Image ID="imgekycverified" runat="server" ImageUrl="~/dist/img/Failuer.png" /><br />
                                        </div>
                                        <div style="text-align: center">
                                            <label>Is E-KYC Verified</label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <%--stp--%>
    <div id="STP_containerBody" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <div id="STP_container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">STP Details</h3>
                                    <i class="fa fa-usd"></i>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkStpDtlsRefresh" OnClick="lnkStpDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <%--                                    <asp:CheckBox ID="CheckBox2" CssClass="CheckSave" runat="server" Text="Edit" EnableViewState="False" />--%>
                                    <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <%--                                    <asp:Button ID="Button1" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div runat="server" class="box-body">

                        <%--<h4 class="box-title"><b>STP Result</b></h4>--%>

                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblStpResult" Text="" CssClass="LableInfo" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                            <asp:GridView ID="gvStpDetails" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                        </div>
                        <%--<div class="table-responsive">
                            
                        </div>--%>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkStpDtlsRefresh" />
            </Triggers>
        </asp:UpdatePanel>

    </div>
    <%--PreIssue--%>


    <div class="retPoup">
        <div class="modal fade modal-info" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideloading();">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Info Modal</h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            <asp:Label ID="lblErrorinfo" runat="server" Text=""></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="hideloading();">Close</button>
                        <%--<asp:Label ID="Label14" class="form-control lblLable" runat="server" Text="State Bank Of India"></asp:Label>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Refer cases div copy pasted in notepad++ doc 33--%>

    <%--PreIssue Details--%>
    <div id="PreIssue_containerBody" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div id="PreIssue_container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">PreIssue Details</h3>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkPreIssueDtlsRefresh" OnClick="lnkPreIssueDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh lnkButton"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <%--<asp:CheckBox ID="CheckBox3" CssClass="CheckSave" runat="server" Text="Edit" EnableViewState="False" />--%>
                                    <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="Button2" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">

                        <%--<h4 class="box-title"><b>Pre issue validation (LA)</b></h4>--%>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblPreIssueRslt" Text="" Font-Bold="True"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <%--23nov2017 begin--%>
                        <div id="divpreissueval" visible="false" runat="server" class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <div style="text-align: center">
                                        <label>Total Premium Due</label>
                                    </div>
                                    <div style="text-align: center">
                                        <asp:TextBox ID="txtTotalPremDue" CssClass="form-control lblLable Numeric alphanumeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <div style="text-align: center">
                                        <label>Amount in suspense</label>
                                    </div>
                                    <div style="text-align: center">
                                        <asp:TextBox ID="txtAmountinsuspense" CssClass="form-control lblLable Numeric alphanumeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <div style="text-align: center">
                                        <label>Service Tax</label>
                                    </div>
                                    <div style="text-align: center">
                                        <asp:TextBox ID="txtPremServiceTax" CssClass="form-control lblLable Numeric alphanumeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <div style="text-align: center">
                                        <label>BackDate Intrest</label>
                                    </div>
                                    <div style="text-align: center">
                                        <asp:TextBox ID="txtPrebackdateintrest" CssClass="form-control lblLable Numeric alphanumeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <div style="text-align: center">
                                        <label>Amount</label>
                                    </div>
                                    <div style="text-align: center">
                                        <asp:TextBox ID="txtAmountdue" CssClass="form-control lblLable Numeric alphanumeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPreissueDtls" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                                </div>
                            </div>
                        </div>
                        <%--23nov2017 End--%>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkPreIssueDtlsRefresh" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--ApplicationDetails--%>
    <div id="divApplicationDetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="updApplicationDetails" runat="server">
            <ContentTemplate>
                <div id="Appdtls_container" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">Application Details</h3>
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
                                    <asp:CheckBox ID="chkAppDtls" CssClass="CheckSave" runat="server" Text="Edit" EnableViewState="False" />
                                    <%--  <input id="chkAppDtls" class="CheckSave" type="checkbox" value="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnAppDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ApplicationDetails" CssClass="btn-primary HideControl" OnClientClick="return ValidateApplicationBlog();" runat="server" Text="Save" OnClick="btnAppDtlsSave_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Appdtls_containerBody" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorappdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorAppDetailsBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Number</label>
                                    <asp:TextBox ID="txtAppno" CssClass="form-control validate lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Policy Number</label>
                                    <asp:TextBox ID="txtPolno" CssClass="form-control validate lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>AppSign Date</label>
                                    <asp:TextBox ID="txtAppsigndate" CssClass="form-control validate lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Channel</label>
                                    <asp:TextBox ID="txtAppchannel" CssClass="form-control validate lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Agent Channel</label>
                                    <asp:TextBox ID="txtAgentChannel" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
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
                            </div>

                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Backdating  Required</label><br />
                                    <div class="col-right">
                                        <label id="lblBackdateCaption" runat="server" class="form-control lblLable">
                                            <span>NO</span>
                                            <input class="simple-switch-input" runat="server" type="checkbox" id="switch_havInsurance" />
                                            <span class="simple-switch dark yes"></span>Yes
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div id="ReqRcd" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Required RCD</label>
                                    <%-- <asp:TextBox ID="txtRcdreq" CssClass="form-control lblLable" onKeyPress="return false;" runat="server" ReadOnly="true"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtRcdreq" CssClass="form-control validate lblLable" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hdnRcdReq" runat="server" />
                                </div>
                            </div>


                            <div id="divVBKdatereason" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>BackDating reason </label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlBkdateReason" CssClass="form-control  lblLable" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div id="div7" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Pivc Status</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPivcStatus" CssClass="form-control lblLable" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div id="div8" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Backdating Intrest</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtBackDateIntrest" CssClass="form-control lblLable" runat="server" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <%-- Brijesh changes start--%>
                            <div id="divRelationwithStaff" runat="server" class="col-md-2" visible="false">
                                <div class="form-group">
                                    <label>Relation with Staff</label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlRelationStaff" CssClass="form-control  lblLable" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <%-- Brijesh changes End --%>
                            
                            <%--end here--%>
                        </div>
                        <div class="col-md-12">
                            <%--show nsap flag--%>
                            <div id="div14" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>NSAP Caption</label>
                                    <div class="form-group">
                                        <asp:CheckBox ID="cbIsNsap" CssClass="form-control " AutoPostBack="true" onchange="fnLoaderShow();" OnCheckedChanged="cbIsNsap_CheckedChanged" runat="server" Text="Is NSAP"></asp:CheckBox><%--onchange="fnChaneNsap();" --%>
                                    </div>
                                </div>
                            </div>

                            <%--added by shri on 28Mar18 to add new flag FOR SICL--%>
                            <div id="div24" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>Is SICL?</label><br />
                                    <div class="col-right">
                                        <label class="lblLable">
                                            No<input type="checkbox" class="simple-switch-input " runat="server" id="cbIsSicl" /><span class="simple-switch dark"></span>Yes
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <%--added by shri to add CIBIL --%>
                            <div id="div28" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>CIBIL</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtApplicationDetailsCibil" CssClass="form-control Numeric lblLable" MaxLength="3" runat="server" Text=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--end here--%>
                            <div id="div1" runat="server" class="col-md-2">
                                <div class="form-group">
                                    <label>PIVC Reject Reason</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPivcRejectReason" CssClass="form-control lblLable" runat="server" Text=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkApplicationDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnAppDtlsSave1" />
                <asp:AsyncPostBackTrigger ControlID="cbIsNsap" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%-- Agent And lead Details--%>
    <div id="divAgentDetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="updAgentDetails" runat="server">
            <ContentTemplate>
                <div id="Agentdtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Agent & Lead Details</h3>
                                <i class="fa fa-user-o"></i>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18 HideControl" style="float: right">
                                    <asp:LinkButton ID="lnkAgentDtlsRefresh" OnClick="lnkAgentDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkAgentDtls" CssClass="HideControl" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnAgentDtlsSave" CssClass="btn-primary HideControl" runat="server" Text="Save" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrroagentdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Agent Code</label>
                                    <asp:TextBox ID="txtAgentcode1" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>FG Employee Code</label>
                                    <asp:TextBox ID="txtFgempcode" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Partner Code</label>
                                    <asp:TextBox ID="txtPartnerempcode" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Campaign code</label>
                                    <asp:TextBox ID="txtcampaigncode" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Leadcode flag</label><br />
                                    <div class="col-right">
                                        <label>
                                            <span>NO</span>
                                            <input class="simple-switch-input" runat="server" type="checkbox" id="chkAgentLeadcode" />
                                            <span class="simple-switch dark yes"></span>Yes
                                        </label>

                                    </div>
                                </div>
                            </div>
                            <div id="ReqRcd2" class="col-md-2 ">
                                <div class="form-group">
                                    <label>Lead code</label>
                                    <asp:TextBox ID="txtLeadcode" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>is Agent Sig Verfied</label><br />
                                    <div class="col-right">
                                        <label>
                                            No<input type="checkbox" class="simple-switch-input" id="chkAgentverified" runat="server" /><span class="simple-switch dark"></span>Yes
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <%--ADDED ON 27 MAR 18 BY SHRI TO SHOW AGENT NAME --%>
                            <div id="Div15" class="col-md-2 ">
                                <div class="form-group">
                                    <label>Agent Name</label>
                                    <asp:TextBox ID="txtAgentName" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <%--END HERE--%>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkAgentDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnAgentDtlsSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--added by shri to show popup for client details--%>
    <%--Client Details--%>
    <div id="divClientDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="updClientDetails" runat="server">
            <ContentTemplate>
                <div id="Div2" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Client Details</h3>
                                <i class="fa fa-address-card-o"></i>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">

                                    <asp:LinkButton runat="server" ID="btnRefreshClientProfile" CssClass="lnkButton" OnClick="btnRefreshClientProfile_Click">
                                    <%--<a href="#"><span class="glyphicon glyphicon-refresh"></span></a>--%><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Div3" class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvClientDetails" AutoGenerateColumns="false" class="table table-bordered table-striped" runat="server">
                                    <Columns>
                                        <asp:BoundField HeaderText="Client Id" ItemStyle-CssClass="spClientId" DataField="ClientId" />
                                        <asp:BoundField HeaderText="Full Name" ItemStyle-CssClass="spClientFullName" DataField="ClientFullName" />
                                        <asp:BoundField HeaderText="Date Of Birth" ItemStyle-CssClass="spDob" DataField="DOB" />
                                        <asp:BoundField HeaderText="Age" ItemStyle-CssClass="spAge" DataField="Age" />
                                        <asp:BoundField HeaderText="Occupation" DataField="Occupation" />
                                        <asp:BoundField HeaderText="Role" DataField="Role" ItemStyle-CssClass="spRole" />
                                        <asp:BoundField HeaderText="Relation" DataField="Relation" />
                                        <asp:BoundField HeaderText="Is Smoker" DataField="IsSmoker" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <%--added by shri on 31 oct to add dedupe details --%>
                        <div class="col-md-12">
                            <%--<asp:Button ID="btnClientDetailsAML" runat="server" OnClick="btnClientDetailsAML_Click" CssClass="pull-left btn-link" Text="AML" data-backdrop="static" data-keyboard="false" />--%>
                            <div class="form-group">
                                <div class="col-md-6 pull-left">
                                    <asp:Button CssClass="btn-link lnkButton" ID="btnDedupeClient" runat="server" Text="Dedupe" OnClick="btnDedupeClient_Click" />
                                </div>
                                <div class="col-md-6 pull-right">
                                    <a href="#" onclick="fnClearPopupSelection();" class="pull-right" data-toggle="modal" data-target="#divUserControlModal">Select</a>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 HideControl" runat="server" id="divDgClientDedupe">
                            <div class="table-responsive" style="overflow: auto; width: 100%; height: 300px">
                                <asp:DataGrid ID="dgUwDedupe" runat="server" HeaderStyle-CssClass="text-bold" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
                                    <Columns>
                                        <asp:BoundColumn HeaderText="GCN" DataField="gcn" />
                                        <asp:BoundColumn HeaderText="Given Name" DataField="givenname" />
                                        <asp:BoundColumn HeaderText="Surname" DataField="surname" />
                                        <asp:BoundColumn HeaderText="Gender" DataField="gender" />
                                        <asp:BoundColumn HeaderText="Birth Date" DataField="BirthRegDate" />
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                        <%--end here--%>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRefreshClientProfile" />
                <%-- <asp:AsyncPostBackTrigger ControlID="btnClientDetailsAML" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--added by shri to show journal data--%>

    <%--Journal--%>
    <div id="divJournalDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="upJournalDetails" runat="server">
            <ContentTemplate>
                <div id="divJournalDetailsBody" class="box box-warning box-solid ">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <div class="col-md-3">
                                    <h3 class="box-title ">Journal</h3>
                                    <i class="fa fa-pencil fa-fw" aria-hidden="true"></i>
                                </div>
                                <div class="col-md-3">
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkRefreshJournal" OnClick="lnkRefreshJournal_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkJournal" CssClass="CheckSave" runat="server" Text="Edit" EnableViewState="False" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button CommandName="Journal" OnCommand="btnCommonEvent_Command" ID="btnSaveJournal" CssClass="btn-primary HideControl lnkButton" runat="server" Text="Save" OnClick="btnSaveJournal_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Div13" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6 " onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblJournalMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorJournalDetailsBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Application Number</label>
                                    <asp:TextBox ID="txtJournalApplicatonNumber" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Document No</label>
                                    <asp:TextBox ID="txtJournalDocumentNo" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Password</label>
                                    <asp:TextBox ID="txtJournalPassword" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="false" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group HideControl">
                                    <label>Authenticate</label>
                                    <asp:DropDownList ID="ddlAuthenticate" CssClass="form-control " runat="server" Enabled="false">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                        <asp:ListItem Value="No">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2 divFundTransfer HideControl">
                                <div class="form-group">
                                    <label>Fund Transfer Decision</label>
                                    <asp:DropDownList ID="ddlFundDecision" CssClass="form-control " runat="server">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="Approve">Approve</asp:ListItem>
                                        <asp:ListItem Value="Reject">Reject</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkRefreshJournal" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnSaveJournal" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--end here--%>

    <%--Bank Details--%>
    <div id="divBankDetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="updBankDetails" runat="server">
            <ContentTemplate>
                <div id="Bnkdtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Bank Details</h3>
                                <span class="glyphicon">&#xe225;</span>
                            </div>
                            <div class="col-md-3">
                                <%--                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkBankDtlsRefresh" OnClick="lnkBankDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkBankDtls" CssClass="" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnBankDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="BankDtls" OnClientClick="return ValidateBankDetailsBlog()" CssClass="btn-primary HideControl" runat="server" Text="Save" OnClick="btnBankDtlsSave_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Bnkdtls_containerBody" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorbankdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorBankDetailsBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Client Name</label>
                                    <asp:TextBox ID="txtBnkClientname" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    <%--<input type="text" class="form-control" placeholder="" />--%><%--<asp:Label ID="Label13" class="form-control lblLable" runat="server" Text="SBI1458965252"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Client Type</label>
                                    <asp:TextBox ID="txtBnkClienttype" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    <%--<input type="text" class="form-control" placeholder="" />--%><%--<asp:Label ID="Label14" class="form-control lblLable" runat="server" Text="State Bank Of India"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Client Number</label>
                                    <asp:TextBox ID="txtBnkClientnumber" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    <%--<input type="text" class="form-control" placeholder="" />--%><%--<asp:Label ID="Label15" class="form-control lblLable" runat="server" Text="Kasturi Plazza"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Ifsc Code</label>
                                    <asp:TextBox ID="txtBnkIfsccode" CssClass="form-control lblLable alphanumeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                    <%--<asp:DropDownList ID="ddlIfscCode" CssClass="form-control select2" runat="server"></asp:DropDownList>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Bank Name</label>
                                    <asp:TextBox ID="txtBnkBankname" CssClass="form-control lblLable" Text="" runat="server"></asp:TextBox>

                                    <%--<input type="text" class="form-control" placeholder="" />--%><%--<asp:Label ID="Label17" class="form-control lblLable" runat="server" Text="153426365225240"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Branch Name</label>
                                    <asp:TextBox ID="txtBnkBranchname" CssClass="form-control lblLable" Text="" runat="server"></asp:TextBox>
                                    <%--<input type="text" class="form-control" placeholder="" />--%><%-- </div>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Bank Address</label>
                                    <asp:TextBox ID="txtBnkBankaddress" runat="server" CssClass="form-control lblLable" Text=""></asp:TextBox>

                                    <%--Bank Details end--%><%--Pan Details Begins--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Account Number</label>
                                    <asp:TextBox ID="txtBnkBankaccno" CssClass="form-control lblLable Numeric alphanumeric" Text="" runat="server"></asp:TextBox>

                                </div>
                            </div>
                            <%--added by shri on 06 sep 17 to show auto pay type--%>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Auto Pay Type</label>
                                    <asp:DropDownList ID="ddlAutoPaytype" runat="server" CssClass="form-control lblLable" onchange="fnHideShowGrid();">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <%--end here--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkBankDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnBankDtlsSave1" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--Pan Details--%>
    <div id="divPanDetails" runat="server" class="col-md-12 ">
        <asp:UpdatePanel ID="updPandetails" runat="server">
            <ContentTemplate>
                <div id="Pandtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Pan Details</h3>
                                <i class="fa fa-address-card-o"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkPanDtlsRefresh" OnClick="lnkPanDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkPanDtls" CssClass="" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnPandtlsSave" OnCommand="btnCommonEvent_Command" CommandName="PanDtls" OnClientClick="return ValidatePanDetailsBlog();" CssClass="btn-primary HideControl" runat="server" Text="Save" OnClick="btnPandtlsSave_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Pantls_containerBody" class="box-body ">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorpandtls" runat="server" Font-Bold="True" ForeColor="Red" onclick="fnHideShowErrorMsg(this);"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorPanDetailsBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkPanCopy" runat="server" Text="Copy of pan card" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:RadioButton ID="chkForm16Copy" runat="server" Text="Copy of Form 60" GroupName="Pan" onchange="fnCheckPanType();" />
                                </div>
                            </div>
                        </div>
                        <div id="divPanValidation" runat="server" class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Pan Number</label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <%-- <div class="box-body">--%>
                                    <asp:TextBox ID="txtPannumber" MaxLength="10" CssClass="form-control lblLable paccNum p" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnValidatepan" CssClass="btn lnkButton btn-primary HideControl" OnClientClick="return ValidatePanDetailsBlog();" runat="server" Text="Validate Pan" OnClick="btnValidatepan_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 panresopnse">
                            <div class="table-responsive">
                                <asp:GridView ID="gvPanResult" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-12" runat="server" id="divPanManipulate">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" Text="PAN Matched?" ID="lblPanValidated"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="lblPanIsValidated" CssClass="lblLable"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12"><span>Is PAN Valid</span></div>
                                    <div class="col-md-12">
                                        <asp:CheckBox runat="server" ID="chkIsPanValidate" AutoPostBack="true" OnClick="fnChangePanDetails(this);" OnCheckedChanged="chkIsPanValidate_CheckedChanged" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12"><span>Description</span></div>
                                    <div class="col-md-12">
                                        <asp:Label runat="server" ID="txtPanDescription" CssClass="lblLable"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" Text="Comment" ID="lblPanComment"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:TextBox runat="server" ID="txtPanComment" CssClass="form-control lblLable"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <%--26.1 Begin of Changes; Bibekananda Nanda - [1103055]--%>
                             <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" Text="PanType" ID="lblnsdlPanTypeProp"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label runat="server" ID="lblnsdlPanType" CssClass="form-control lblLable"></asp:Label>
                                    </div>
                                </div>
                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" Text="LastUpdatedDate" ID="lblLastUpdDT"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label runat="server" ID="lblnsdlLastUpdDt" CssClass="form-control lblLable"></asp:Label>
                                    </div>
                                </div>
                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" Text="PanMsg" ID="lblPanMsgProp"></asp:Label>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Label runat="server" ID="lblPanMsg" Font-Bold="True" ForeColor="Red" CssClass="form-control lblLable"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <%--26.1 End of Changes; Bibekananda Nanda - [1103055]--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkPanDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnPandtlsSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--commented by shri on 16 oct 17 as per uw requirement, sagar joshi --%>

    <%--added by shri on 06 sep 17 to add mandate --%>
    <div id="divMandate" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="upMandateDetails" runat="server">
            <ContentTemplate>
                <div id="divMandateDetails_Container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Mandate Details</h3>
                                <span class="glyphicon">&#xe225;</span>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="LinkButton3" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkMandate" CssClass="" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnMandateDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="MandateDtls" CssClass="btn-primary HideControl" runat="server" Text="Save" OnClientClick="return ValidateMandateDetailsBlog()" OnClick="btnMandateDtlsSave_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Mandatedtls_containerBody" class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorMandate" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="divMandateecs" runat="server" class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Account Type</label>
                                    <asp:TextBox ID="txtMandAccountType" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Account Number</label>
                                    <asp:TextBox ID="txtMandAccountno" CssClass="form-control validate lblLable Numeric" Text="" runat="server" MaxLength="15"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Acc Holder name</label>
                                    <asp:TextBox ID="txtMandAccountholder" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Mandate Sign date</label>
                                    <asp:TextBox ID="txtMandateassigndate" CssClass="form-control validate DatePicker lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Micro code</label>
                                    <asp:TextBox ID="txtMandMicrocode" CssClass="form-control validate lblLable alphanumeric" Text="" runat="server" AutoPostBack="True" OnTextChanged="txtMandMicrocode_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Bank name</label>
                                    <asp:TextBox ID="txtMandBankName" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Branch Code</label>
                                    <asp:TextBox ID="txtMandBranchName" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <%--  <div class="col-md-2">
                                <div class="form-group">
                                    <label>IFSC</label>
                                    <asp:TextBox ID="txtIFSC" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>--%>
                            <%--Added by Suraj on 30/10/2018 as suggest by amit--%>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Amount</label>
                                    <asp:TextBox ID="txtAmount" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Preferred Date(Days)</label>
                                    <asp:TextBox ID="txtPreferredDate" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <%--End--%>
                        </div>
                        <div id="divMandatesi" runat="server" class="col-md-12 HideControl">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Creditcard No</label>
                                    <asp:TextBox ID="txtCreditcardno" onblur="fnMandateAcountNo(this);" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Creditcard Type</label>
                                    <asp:TextBox ID="txtcreditcardType" TabIndex="-1" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Valid Upto</label>
                                    <asp:TextBox ID="txtValidupto" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Holder name</label>
                                    <asp:TextBox ID="txtHolderName" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Mandate Sign Date</label>
                                    <asp:TextBox ID="txtMandateSigndate" CssClass="form-control validate DatePicker lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>

                            <%--<div class="col-md-2">
                                <div class="form-group">
                                    <label>Micr Code</label>
                                    <asp:TextBox ID="txtSiMicrCode" CssClass="form-control validate lblLable" Text="" runat="server" AutoPostBack="true" OnTextChanged="txtSiMicrCode_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Bank name</label>
                                    <asp:TextBox ID="txtSiBankName" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Branch Code</label>
                                    <asp:TextBox ID="txtSiBranchCode" CssClass="form-control validate lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtMandMicrocode" />
                <%--<asp:AsyncPostBackTrigger ControlID="btnMandateDtlsSave" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--end here--%>

    <%--Adhar Details--%>
    <%--added by shri to add aadhar details on 12 jan 18--%>
    <div id="divAadharDetails" runat="server" class="col-md-12 ">
        <asp:UpdatePanel ID="upAadharDetails" runat="server">
            <ContentTemplate>
                <div id="Div16" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Aadhar Details</h3>
                                <i class="fa fa-address-card-o"></i>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="Edit" onchange="fnEnableDisableNew(this);" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnUpdateAadharDetails" CssClass="btnSave btn-primary HideControl" runat="server" Text="Save" OnClick="btnUpdateAadharDetails_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>

                    <div id="Div17" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorAadharHead" runat="server" Font-Bold="True" ForeColor="Red" onclick="fnHideShowErrorMsg(this);"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorAadharBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="div18" runat="server" class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Aadhar Number</label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <%-- <div class="box-body">--%>
                                    <asp:TextBox ID="txtAadharNumber" MaxLength="16" CssClass="form-control ReadOnly lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnAadharDetails" CssClass="btn-primary lnkButton btnSave HideControl" runat="server" Text="Verify Aadhar" OnClick="BtnAadharDetails_Click" />
                                </div>
                            </div>
                            <%-- <div class="col-md-2">
                                <div class="form-group">
                                    <label>Aadhar Transaction No:</label>
                                </div>
                            </div>--%>
                            <div class="col-md-3">
                                <div id="divaadhartran" runat="server" class="form-group HideControl">
                                    <label><b>Aadhar Transaction No:</b></label>
                                    <asp:Label ID="lblekycbdip" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 panresopnse">
                            <div class="table-responsive">
                                <asp:GridView ID="gvUWDecisionAadharDetails" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <asp:CheckBox runat="server" ID="chkIsAadharVerified" Text="Is Aadhar Validate" OnCheckedChanged="chkIsAadharVerified_CheckedChanged" AutoPostBack="true" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkPanDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnAadharDetails" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--AML Details--%>
    <%--added by shri on 11 july 17 to show online aml details--%>
    <div id="divOnlineAml" runat="server" class="col-md-12">
        <div id="Div4" class="box box-warning box-solid collapsed-box">
            <div class="box-header with-border">
                <div class="col-md-12">
                    <div class="col-md-9">
                        <h3 class="box-title ">AML Details</h3>
                        <i class="fa fa-address-card-o"></i>
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-18" style="float: right">
                            <asp:LinkButton ID="LinkButton1" CssClass="HideControl" OnClick="lnkPanDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                        </div>
                        <div class="col-md-4" style="float: right">
                            <asp:CheckBox ID="CheckBox2" CssClass="" runat="server" Text="Edit" />
                        </div>
                    </div>
                </div>
                <div class="box-tools ">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-plus"></i>
                    </button>
                </div>
            </div>
            <div id="Div5" class="box-body">
                <div class="col-md-12">
                    <div class="col-md-12">
                        <div class="form-group">
                            <Docs:PendingDocs runat="server" ID="PendingDocs" />

                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--added by shri on 04 sept 17 to show offline aml details--%>
    <div id="divOfflineAml" runat="server" class="col-md-12">
        <div id="Div9" class="box box-warning box-solid collapsed-box">
            <div class="box-header with-border">
                <div class="col-md-12">
                    <div class="col-md-9">
                        <h3 class="box-title ">AML Details</h3>
                        <i class="fa fa-address-card-o"></i>
                    </div>
                    <div class="col-md-3 HideControl">
                        <div class="col-md-18" style="float: right">
                            <asp:LinkButton ID="LinkButton2" OnClick="lnkPanDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                        </div>
                        <div class="col-md-4" style="float: right">
                            <asp:CheckBox ID="CheckBox1" CssClass="" runat="server" Text="Edit" />
                        </div>
                    </div>
                </div>
                <div class="box-tools ">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-plus"></i>
                    </button>
                </div>
            </div>
            <div id="Div10" class="box-body">
                <div class="col-md-12">
                    <div class="col-md-12">
                        <div class="form-group">
                            <OfflineAml:AmlOffline runat="server" ID="AmlOffline" />
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView2" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--end here--%>

    <%--Policy Details--%>
    <div id="DivPolicyDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="UpdatePanel20" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="Policy_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Policy Details</h3>
                                <i class="fa fa-hourglass-start"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkLoadingDtlsRefresh" OnClick="lnkLoadingDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <%--   <asp:CheckBox ID="chk" CssClass="" runat="server" Text="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="Label19" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>Policy Number</label>
                                <asp:TextBox ID="txtPolno1" CssClass="form-control validate lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="hdnPolNo" runat="server" />
                                <asp:HiddenField ID="hdnWaiverAvailed" runat="server" />
                                <asp:HiddenField ID="hdnTotalRequiredPremium" runat="server" />
                                <asp:HiddenField ID="hdnTotalInterestAccured" runat="server" />
                                <asp:HiddenField ID="hdnRecoveryPolicyDebt" runat="server" />
                                <asp:HiddenField ID="hdnAdjustmentSuspense" runat="server" />
                                <asp:HiddenField ID="hdnWaiverAmount" runat="server" />
                                <asp:HiddenField ID="hdnReciptAmt" runat="server" />
                                <asp:HiddenField ID="HdnReciptdate" runat="server" />
                                <asp:HiddenField ID="hdnModilenumber" runat="server" />
                                <asp:HiddenField ID="hdnEmail" runat="server" />
                                <asp:HiddenField ID="hdnNewFollowup" runat="server" />
                               <%--  1.1 Start of Changes for Revival 18/04/2020--%>
                                <asp:HiddenField ID="hdnLAFullname" runat="server" />
                                 <%--  1.1 End of Changes for Revival 18/04/2020--%>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Premium Paying Status</label>
                                    <%--<asp:TextBox ID="txtLoadRider" runat="server" CssClass="form-control lblLable"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtPremPayingStatus" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="hdnPremPayingStatus" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>RCD</label>
                                    <asp:TextBox ID="txtRCD" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Policy Status</label>
                                    <asp:TextBox ID="txtPolicyStatus" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="hdnPolicyStatus" runat="server" />
                                    <asp:HiddenField ID="HdnApplicationNumber" runat="server" />
                                    <asp:HiddenField ID="HdnProdCodeMonths" runat="server" />
                                    <asp:HiddenField ID="HdnPTDMonths" runat="server" />
                                    <asp:HiddenField ID="HdnFinalMonthCount" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Premium Cessation Date</label>
                                    <asp:TextBox ID="txtPremCessDate" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>PTD</label>
                                    <asp:TextBox ID="txtPTD" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Product Name</label>
                                    <asp:TextBox ID="txtProname1" CssClass="form-control" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Risk Cessation Date</label>
                                    <asp:TextBox ID="txtRiskCessDate" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Sum Assured</label>
                                    <asp:TextBox ID="txtSumassure1" CssClass="form-control Numeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Type Of Product</label>
                                    <asp:TextBox ID="txtTypeProd" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Reduced Sum Assured</label>
                                    <asp:TextBox ID="txtRedSumAssured" MaxLength="3" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Bonus</label>
                                    <asp:TextBox ID="txtBonus" MaxLength="4" runat="server" CssClass="form-control lblLable Numeric"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Branch</label>
                                    <asp:TextBox ID="txtBranch" MaxLength="4" runat="server" CssClass="form-control lblLable Numeric"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Channel</label>
                                    <asp:TextBox ID="txtChannel" CssClass="form-control Numeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Lapsation Days</label>
                                    <asp:TextBox ID="txtAgeing" CssClass="form-control Numeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>


                        </div>

                        <%-- <div>--%>
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="Button7" CssClass="btn btn-primary HideControl lnkButton" runat="server" Text="Add New Row" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Button ID="btnViewExistingLoad" OnClientClick="return false;" CssClass="btn btn-primary HideControl" runat="server" Text="View Existing Loading" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddLoadingRow" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div id="div36" runat="server" class="col-md-12 HideControl">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="Label20"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="Label21"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>



                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkLoadingDtlsRefresh" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="btnLoadingDtlsSave" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlLoadType" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--Premium Details--%>
    <div id="DivPremiumDetails1" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="UpdatePanel14" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="Premium_container1" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Premium Details</h3>
                                <i class="fa fa-hourglass-start"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkLoadingDtlsRefresh" OnClick="lnkLoadingDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <%--   <asp:CheckBox ID="chkEdit" CssClass="" runat="server" Text="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Frequency</label>
                                    <%--<asp:TextBox ID="txtLoadRider" runat="server" CssClass="form-control lblLable"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtFrequency" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel15" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Annualised Premium</label>
                                            <asp:TextBox ID="txtAnnualPrem" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-2" style="display: none">
                                        <div class="form-group">
                                            <label>Policy Status</label>
                                            <asp:TextBox ID="txtPolicyStatusUW" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLoadType" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel16" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Total Premium Payable</label>
                                            <asp:TextBox ID="txtTotalPrmPay" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Premium Paid</label>
                                            <asp:TextBox ID="txtPremPaid" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLoadRsn1" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel17" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Outstanding Loan</label>
                                            <asp:TextBox ID="txtOutstandingLoan" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Outstanding Loan Interest</label>
                                            <asp:TextBox ID="txtOutstandingLoanInterest" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLoadRsn2" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel18" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Revival Eligibility</label>
                                            <asp:TextBox ID="txtRevivalEligibility" MaxLength="3" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>DGH Required</label>
                                            <asp:TextBox ID="txtDGHRequired" MaxLength="4" runat="server" CssClass="form-control lblLable Numeric"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label></label>
                                    <asp:Button runat="server" ID="Button5" Text="Calculate Premium" CssClass="form-control btn-primary HideControl lnkButton" />
                                </div>
                            </div>
                        </div>
                        <%-- <div>--%>
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="Button6" CssClass="btn btn-primary HideControl lnkButton" runat="server" Text="Add New Row" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Button ID="btnViewExistingLoad" OnClientClick="return false;" CssClass="btn btn-primary HideControl" runat="server" Text="View Existing Loading" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddLoadingRow" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div id="div35" runat="server" class="col-md-12 HideControl">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="Label16"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="Label17"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>



                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkLoadingDtlsRefresh" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="btnLoadingDtlsSave" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlLoadType" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <%--Saral detials--%>
    <div id="divSarDetails" class="col-md-12 HideControl" runat="server">
        <asp:UpdatePanel ID="updMsarTsarDetails" runat="server">
            <ContentTemplate>
                <div id="sarDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">SAR Details</h3>
                                <i class="fa fa-plus-square"></i>
                            </div>
                            <div class="col-md-3 HideControl">
                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkSarDtlsRefresh" OnClick="lnkSarDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnSarDtlsSave" CssClass="btn-primary HideControl" runat="server" Text="Save" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GridMsarTsar" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="Client ID" DataField="ClientID" />
                                    <asp:BoundField HeaderText="Client Role" DataField="ClientRole" />
                                    <asp:BoundField HeaderText="Client Name" DataField="ClientName" />
                                    <asp:BoundField HeaderText="MSAR" DataField="MSAR" />
                                    <asp:BoundField HeaderText="TSAR" DataField="TSAR" />
                                    <asp:BoundField HeaderText="FSAR" DataField="FSAR" />
                                    <asp:BoundField HeaderText="Total Premium" DataField="TOTALPREMIUM" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="divPrePolicy" runat="server" class="box-body">
                        <h6>Previous Policy Details</h6>
                        <br />
                        <asp:Label ID="lblErrorPrevpol" runat="server"></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="gridPrevPol" CssClass="table table-bordered table-striped">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%-- ADDED B AYAY SAHU --%>


    <%-- <div id="divSarDetailsSaral" class="col-md-12 " runat="server">
        <asp:UpdatePanel ID="updMsarTsarDetailsSaral" runat="server">
            <ContentTemplate>
                <div id="sarDtls_containersaral" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">SAR Details Saral</h3>
                                <i class="fa fa-plus-square"></i>
                            </div>
                            <div class="col-md-3 HideControl">
                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkSarDtlsRefreshSaral" OnClick="lnkSarDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnSarDtlsSaveSaral" CssClass="btn-primary HideControl" runat="server" Text="Save" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GridMsarTsarSaral" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="Client ID" DataField="ClientID" />
                                    <asp:BoundField HeaderText="Client Role" DataField="ClientRole" />
                                    <asp:BoundField HeaderText="Client Name" DataField="ClientName" />
                                    <asp:BoundField HeaderText="MSAR" DataField="MSAR" />
                                    <asp:BoundField HeaderText="TSAR" DataField="TSAR" />
                                    <asp:BoundField HeaderText="FSAR" DataField="FSAR" />
                                    <asp:BoundField HeaderText="Total Premium" DataField="TOTALPREMIUM" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="divPrePolicySaral" runat="server" class="box-body">
                        <h6>Previous Policy Details</h6>
                        <br />
                        <asp:Label ID="lblErrorPrevpolSaral" runat="server"></asp:Label>
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="gridPrevPolSaral" CssClass="table table-bordered table-striped">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefreshSaral" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSaveSaral" />
            </Triggers>
        </asp:UpdatePanel>
    </div>--%>
    <%-- END --%>
    <%--1.1 Begin of Changes by Rupali--%>
    <asp:Label runat="server" ID="lblothercompany"></asp:Label>
    <div id="div37" class="col-md-12" runat="server">
        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
            <ContentTemplate>
                <div id="xyz" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">IIB Data Fetched Other Company</h3>
                                <i class="fa fa-building"></i>
                            </div>

                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body" style="overflow-x: auto">
                        <div class="table-responsive">
                            <asp:GridView runat="server" ID="GridViewOther" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowDataBound="GridViewOther_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="QuestDBNo" HeaderText="QuestDBNo" runat="server" />
                                    <asp:BoundField DataField="Input_Matching_Parameter" HeaderText="MatchingParameter" runat="server" />
                                    <asp:BoundField DataField="Quest_DoP_DoC" HeaderText="DoC" runat="server" />
                                    <asp:BoundField DataField="Quest_Sum_Assured" HeaderText="SA" runat="server" />
                                    <asp:BoundField DataField="Quest_Policy_Status" HeaderText="Pol Status" runat="server" />
                                    <asp:BoundField DataField="Quest_Date_of_Exit" HeaderText="Date ofExit" runat="server" />
                                    <asp:BoundField DataField="Quest_Date_of_Death" HeaderText="Date of Death" runat="server" />
                                    <asp:BoundField DataField="Quest_Cause_of_Death" HeaderText="Cause of Death" runat="server" />
                                    <asp:BoundField DataField="Quest_Record_last_updated" HeaderText="Update date" runat="server" />
                                    <asp:BoundField DataField="Quest_Entity_Caution_Status" HeaderText="Entity Caution Status" runat="server" />
                                    <asp:BoundField DataField="Quest_Intermediary_Caution_Status" HeaderText="Intermediary Caution Status" runat="server" />
                                    <asp:BoundField DataField="Quest_Company_Number" HeaderText="Company" runat="server" />
                                    <asp:BoundField DataField="Is_Negative_Match" HeaderText="Negative" runat="server" />
                                    <asp:BoundField DataField="LAProposerPayor" HeaderText="LA/PH/PremiumPayer" runat="server" />
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>


                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="div39" class="col-md-12" runat="server">
        <asp:Label runat="server" ID="Label22"></asp:Label>
        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
            <ContentTemplate>
                <div id="xyze" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">IIB Data Fetched FG Company</h3>
                                <i class="fa fa-building"></i>
                            </div>

                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="table-responsive" style="overflow-x: auto">
                            <asp:GridView runat="server" ID="GridViewFG" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" OnRowDataBound="GridViewFG_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="QuestDBNo" HeaderText="QuestDBNo" runat="server" />
                                    <asp:BoundField DataField="Input_Matching_Parameter" HeaderText="MatchingParameter" runat="server" />
                                    <asp:BoundField DataField="Quest_DoP_DoC" HeaderText="DoC" runat="server" />
                                    <asp:BoundField DataField="Quest_Sum_Assured" HeaderText="SA" runat="server" />
                                    <asp:BoundField DataField="Quest_Policy_Status" HeaderText="Pol Status" runat="server" />
                                    <asp:BoundField DataField="Quest_Date_of_Exit" HeaderText="Date ofExit" runat="server" />
                                    <asp:BoundField DataField="Quest_Date_of_Death" HeaderText="Date of Death" runat="server" />
                                    <asp:BoundField DataField="Quest_Cause_of_Death" HeaderText="Cause of Death" runat="server" />
                                    <asp:BoundField DataField="Quest_Record_last_updated" HeaderText="Update date" runat="server" />
                                    <asp:BoundField DataField="Quest_Entity_Caution_Status" HeaderText="Entity Caution Status" runat="server" />
                                    <asp:BoundField DataField="Quest_Intermediary_Caution_Status" HeaderText="Intermediary Caution Status" runat="server" />
                                    <asp:BoundField DataField="Quest_Company_Number" HeaderText="Company" runat="server" />
                                    <asp:BoundField DataField="Is_Negative_Match" HeaderText="Negative" runat="server" />
                                    <asp:BoundField DataField="LAProposerPayor" HeaderText="LA/PH/PremiumPayer" runat="server" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkSarDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnSarDtlsSave" />
            </Triggers>
        </asp:UpdatePanel>

        <div class="col-md-12">
            <div class="col-md-10">
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnFetchIIBQuery" Enabled="false" runat="server" Text="IIB Query >>" CssClass="btn btn-primary lnkButton" OnClick="btnFetchIIBQuery_Click" />
            </div>
        </div>
        <div>
        </div>
        <br />
        <br />
    </div>
    <%--1.1 End of Changes by Rupali--%>


    <%--Product Details--%>
    <div id="divProductDetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="updProductDetails" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="Proddtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Product Details</h3>
                                <i class="fa fa-product-hunt"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkProductDtlsRefresh" OnClick="lnkProductDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkProductDtls" CssClass="" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">

                                    <asp:Button ID="btnProddtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="ProductDtls" OnClientClick="return ValidateProductDetailsBlog();" CssClass="btn-primary HideControl" runat="server" Text="Save" OnClick="btnProddtlsSave_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="ProdDtls_containerBody" class="box-body">

                        <div id="divProdDetailsBase" runat="server">
                            <div class="col-md-12 error-container-div">
                                <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                    <div class="form-group">
                                        <asp:Label ID="lblErrorproddtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div style="clear: both;"></div>
                                    <div class="col-md-6 HideControl error-detail_div">
                                        <div class="form-group">
                                            <asp:Label ID="lblErrorProductDetailBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Policy Number</label>
                                        <asp:TextBox ID="txtBasepolno" CssClass="form-control" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Product Code</label>
                                        <asp:TextBox ID="txtProdcode" CssClass="form-control" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Product Name</label>
                                        <asp:TextBox ID="txtProname" CssClass="form-control" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Policy Term</label>
                                        <asp:TextBox ID="txtPolterm" CssClass="form-control Numeric" Text="" runat="server" Enabled="false" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Premium Paying Term</label>
                                        <asp:TextBox ID="txtPrepayterm" CssClass="form-control Numeric" Text="" runat="server" Enabled="false" MaxLength="2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sum Assured</label>
                                        <asp:TextBox ID="txtSumassure" CssClass="form-control Numeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Payment Frequency</label>
                                        <asp:DropDownList ID="ddlFrequency" CssClass="form-control " runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="00">Single</asp:ListItem>
                                            <asp:ListItem Value="01">Annual</asp:ListItem>
                                            <asp:ListItem Value="12">Monthly</asp:ListItem>
                                            <asp:ListItem Value="04">Quarterly</asp:ListItem>
                                            <asp:ListItem Value="02">Half Yearly</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Base Premium Amount</label>
                                        <asp:TextBox ID="txtBasepremium" CssClass="form-control  Numeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Service Tax</label>
                                        <asp:TextBox ID="txtServicetax" CssClass="form-control Numeric" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Total Premium</label>
                                        <asp:TextBox ID="txtTotalpremium" CssClass="form-control  Numeric" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Branch Base Premium</label>
                                        <asp:TextBox ID="txtProdBranchBasePremium" CssClass="form-control" Text="0" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label id="lblMonthlyPayout" runat="server">Monthly Payout</label>
                                        <asp:TextBox ID="txtMonthlyPayoutBase" CssClass="form-control Numeric" Text="0" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:Button runat="server" ID="btncalculatePrem_Prod" OnClientClick="return ValidateProductDetailsBlog();" OnClick="btncalculatePrem_Prod_Click" Text="Calculate Premium" CssClass="form-control btn-primary HideControl lnkButton" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div id="divPremiumcal_product" class="table table-responsive">
                                    <asp:GridView ID="gridPremCal_Product" CssClass="table table-bordered table-striped" runat="server">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div id="divcomboprod" runat="server" class="HideControl">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Policy Number</label>
                                        <asp:TextBox ID="txtCombopolno" CssClass="form-control" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Product Code</label>
                                        <asp:TextBox ID="txtCombProdCode" CssClass="form-control  Numeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Product Name</label>
                                        <asp:TextBox ID="txtCombProdName" CssClass="form-control" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Policy Term</label>
                                        <asp:TextBox ID="txtCombPolTerm" CssClass="form-control  Numeric" Text="" Enabled="false" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Premium Paying Term</label>
                                        <asp:TextBox ID="txtCombPayTerm" CssClass="form-control  Numeric" Text="" Enabled="false" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Sum Assured</label>
                                        <asp:TextBox ID="txtCombSumAssured" CssClass="form-control  Numeric" Text="" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Payment Frequency</label>
                                        <asp:DropDownList ID="ddlComboFrequency" CssClass="form-control" runat="server" Enabled="false">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="00">Single</asp:ListItem>
                                            <asp:ListItem Value="01">Annual</asp:ListItem>
                                            <asp:ListItem Value="12">Monthly</asp:ListItem>
                                            <asp:ListItem Value="04">Quarterly</asp:ListItem>
                                            <asp:ListItem Value="02">Half Quarterly</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Premium Amount</label>
                                        <asp:TextBox ID="txtCombPremAmount" CssClass="form-control  Numeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Service Tax</label>
                                        <asp:TextBox ID="txtCombServiceTax" CssClass="form-control  Numeric" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Combo Total Premium</label>
                                        <asp:TextBox ID="txtCombTotalPrem" CssClass="form-control  Numeric" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label id="lblMonthlyPayoutCombo" runat="server">Monthly Payout</label>
                                        <asp:TextBox ID="txtComboMonthlyPayout" CssClass="form-control  Numeric" Text="0" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divRiderdetails" runat="server" class="col-md-12 HideControl">
                            <%--    <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h4 class="box-title ">Rider Details</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvRiderDtls" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" OnRowDataBound="gvRiderDtls_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectRider" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rider Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderName" CssClass="form-control lblLable" runat="server" Text="Label"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rider Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderCode" CssClass="form-control lblLable" runat="server" Text="Label"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RiderSumAssure">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderSumAssure" CssClass="form-control lblLable" runat="server" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rider TotalPremium">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderPremium" CssClass="form-control lblLable" runat="server" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remove">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Remove Option"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkReqRemoveRow" ClientIDMode="AutoID" runat="server" OnClick="lnkReqRemoveRow_Click">
                                                    <asp:Image ID="Image8" Height="45px" runat="server" ImageUrl="~/dist/img/delete2.png" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            --%>
                        </div>
                        <%--Rider Details End--%>

                        <div id="divPremiumdetails" runat="server" class="col-md-12 HideControl">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h4 class="box-title ">Premium Details</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gridPremiumdetails" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkProductDtlsRefresh" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="btnProddtlsSave" />--%>
                <asp:AsyncPostBackTrigger ControlID="btncalculatePrem_Prod" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--<div class="col-md-12">
        <asp:UpdatePanel ID="updRiderdtls" runat="server">
            <ContentTemplate>
                <div class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Rider Details</h3>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">
                                    <a href="#"><span class="glyphicon glyphicon-refresh"></span></a>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkRiderEdit" CssClass="CheckSave" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnRiderDtlsSave" CssClass="btn-primary HideControl" runat="server" Text="Save" OnClick="btnRiderDtlsSave_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorriderdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvRiderDtls" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False" OnRowDataBound="gvRiderDtls_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Rider Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderName" class="form-control lblLable" runat="server" Text="Label"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rider Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderCode" class="form-control lblLable" runat="server" Text="Label"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RiderSumAssure">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderSumAssure" class="form-control lblLable" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rider TotalPremium">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderPremium" class="form-control lblLable" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remove">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Remove Option"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkReqRemoveRow" ClientIDMode="AutoID" runat="server" OnClick="lnkReqRemoveRow_Click">
                                                    <asp:Image ID="Image8" Height="45px" runat="server" ImageUrl="~/dist/img/delete2.png" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>

    <%--<div class="col-md-12">
        <asp:UpdatePanel ID="updRiderdtls" runat="server">
            <ContentTemplate>
                <div class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Rider Details</h3>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">
                                    <a href="#"><span class="glyphicon glyphicon-refresh"></span></a>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkRiderEdit" CssClass="CheckSave" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">                                    
                                    <asp:Button ID="btnRiderDtlsSave" CssClass="btn-primary HideControl" runat="server" Text="Save" OnClick="btnRiderDtlsSave_Click" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorriderdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvRiderDtls" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False" OnRowDataBound="gvRiderDtls_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Rider Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderName" class="form-control lblLable" runat="server" Text="Label"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rider Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderCode" class="form-control lblLable" runat="server" Text="Label"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RiderSumAssure">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderSumAssure" class="form-control lblLable" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rider TotalPremium">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderPremium" class="form-control lblLable" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remove">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Remove Option"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkReqRemoveRow" ClientIDMode="AutoID" runat="server" OnClick="lnkReqRemoveRow_Click">
                                                    <asp:Image ID="Image8" Height="45px" runat="server" ImageUrl="~/dist/img/delete2.png" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>
    <%--added by shri in uat 21 july 17--%>
    <%-- Rider Details--%>
    <div id="div6" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="uptest" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="RiderDetails_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Rider Details</h3>
                                <i class="fa fa-inr"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkBtnRefreshRiderInfo" OnClick="lnkBtnRefreshRiderInfo_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <input type="checkbox" id="cbEditRiderDetail" class="CheckSave CheckSave1" onchange="fnEnableDisable(this);" />
                                    <label for="cbEditRiderDetail">Edit</label>
                                </div>
                                <div class="col-md-3 HideControl" style="float: right">
                                    <asp:Button ID="btnSaveRiderInfo" class="btn-primary btnSave1" runat="server" Text="Save" OnClientClick="return fnCheckNoOfSelection();" OnClick="btnSaveRiderInfo_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblRiderDetailsError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                                <div style="clear: both;"></div>
                                <div class="col-md-6 HideControl error-detail_div">
                                    <div class="form-group">
                                        <asp:Label ID="lblErrorRiderDetailsBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvRiderDtls" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                    <%--OnRowDataBound="gvRiderDtls_RowDataBound"--%>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Select"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:CheckBox ID="chkremoveRider" class="cbEnableDisable OnlyOneRider lblLable" Text='<%#Eval("IsActive") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RiderName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderName" CssClass="form-control lblLable" runat="server" Text='<%#Eval("RIDERNAME") %>' onkeydown="return false;" onkeypress="return false;"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RiderCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRiderCode" CssClass="form-control lblLable" runat="server" Text='<%#Eval("RIDERCODE") %>' onkeydown="return false;" onkeypress="return false;"></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RiderSumAssure">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderSumAssure" Text='<%#Eval("RIDERSUMASSURED") %>' CssClass="form-control lblLable Numeric" runat="server"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RiderTotalPremium">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRiderPremium" Text='<%#Eval("TOTLAPREMIUM")%>' CssClass="form-control lblLable" TabIndex="-1" runat="server" onkeydown="return false;" AutoPostBack="false" onkeypress="return false;"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Service Tax">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtriderservicetax" Text='<%#Eval("SERVICETAX") %>' CssClass="form-control lblLable" TabIndex="-1" runat="server" onkeydown="return false;" AutoPostBack="false" onkeypress="return false;"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Rider Category">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtriderservicetax" Text='<%#Eval("SERVICETAX") %>' CssClass="form-control lblLable" TabIndex="-1" runat="server" onkeydown="return false;" AutoPostBack="false" onkeypress="return false;"></asp:TextBox>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="Remove"> 
                                            <HeaderTemplate> 
                                                <asp:Label ID="Label2" runat="server" Text="Remove Option"></asp:Label> 
                                            </HeaderTemplate> 
                                            <ItemTemplate> 
                                                <asp:LinkButton ID="lnkReqRemoveRow" ClientIDMode="AutoID" runat="server" OnClick="lnkReqRemoveRow_Click">

                                                    <asp:Image ID="Image8" Height="45px" runat="server" ImageUrl="~/dist/img/delete2.png" />

                                                </asp:LinkButton> 
                                            </ItemTemplate> 
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkBtnRefreshRiderInfo" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnSaveRiderInfo" />
                <asp:AsyncPostBackTrigger ControlID="gvRiderDtls" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--end here--%>



    <%-- Customer Details--%>
    <div id="divCustDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="UpdatePanel30" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="CustDetails" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Customer Details</h3>
                                <i class="fa fa-hourglass-start"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkLoadingDtlsRefresh" OnClick="lnkLoadingDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <%--  <asp:CheckBox ID="CheckBox7" CssClass="" runat="server" Text="Edit" />--%>
                                </div>
                                <div class="col-md-3" style="float: right">
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label27" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="Label28" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label>LA Name</label>
                                <asp:TextBox ID="txtLANAME" CssClass="form-control validate lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <%-- <div class="col-md-2">
                                 <div class="form-group">
                                        <label>Product Name</label>
                                        <asp:TextBox ID="TextBox10" CssClass="form-control" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                            </div>--%>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>LA Client ID</label>
                                    <asp:TextBox ID="txtLAClientID" CssClass="form-control Numeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Owner Name</label>
                                    <asp:TextBox ID="txtOwnerName" CssClass="form-control Numeric" Text="" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Owner Client ID</label>
                                    <%--<asp:TextBox ID="txtLoadRider" runat="server" CssClass="form-control lblLable"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtOwnerClientID" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="form-group">
                                    <label></label>
                                    <asp:Button runat="server" ID="Button9" Text="Calculate Premium" CssClass="form-control btn-primary HideControl lnkButton" />
                                </div>
                            </div>
                        </div>
                        <%-- <div>--%>
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="UpdatePanel35" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="Button10" CssClass="btn btn-primary HideControl lnkButton" runat="server" Text="Add New Row" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Button ID="btnViewExistingLoad" OnClientClick="return false;" CssClass="btn btn-primary HideControl" runat="server" Text="View Existing Loading" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddLoadingRow" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div id="div38" runat="server" class="col-md-12 HideControl">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="Label29"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="Label30"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>



                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkLoadingDtlsRefresh" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="btnLoadingDtlsSave" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlLoadType" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%-- Agent Details--%>
    <div id="divAgentDetail" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
            <ContentTemplate>
                <div id="Agentdtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title "><%--Agent & Lead Details--%> Agent Details</h3>
                                <i class="fa fa-user-o"></i>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18 HideControl" style="float: right">
                                    <asp:LinkButton ID="LinkButton3" OnClick="lnkAgentDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="CheckBox4" CssClass="HideControl" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="Button8" CssClass="btn-primary HideControl" runat="server" Text="Save" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label26" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Agent Code</label>
                                    <asp:TextBox ID="txtAgentCode" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Agent Name</label>
                                    <asp:TextBox ID="txtAgentName1" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Agent Status</label>
                                    <asp:TextBox ID="txtAgentStatus" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>SM Name</label>
                                    <asp:TextBox ID="txtSMName" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>SM Mobile</label>
                                    <asp:TextBox ID="txtSMMobile" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>


                        </div>
                        <div class="col-md-12">

                            <%--ADDED ON 27 MAR 18 BY SHRI TO SHOW AGENT NAME --%>

                            <%--END HERE--%>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkAgentDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnAgentDtlsSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="divFunddetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="updfundDtls" runat="server">
            <ContentTemplate>
                <div id="FundsDetails_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Funds Details</h3>
                                <i class="fa fa-inr"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkFundDelsRefresh" OnClick="lnkFundDelsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkFunddtlsSave" CssClass="CheckSave" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnFundDtlsSave1" OnClientClick="return ValidateFundValue()" CssClass="btn-primary HideControl" runat="server" Text="Save" OnClick="btnFundDtlsSave_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorfunddtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorFundDetailBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvFundDtls" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="FundCode">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:Label ID="lblFundvalue" CssClass="form-control lblLable" runat="server" Text='<%#Eval("FND_fundCode") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FundName">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:Label ID="lblFundName" CssClass="form-control lblLable" runat="server" Text='<%#Eval("FND_fundName") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FundValue">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtFundvalue" Width="30%" CssClass="form-control lblLable Numeric" Text='<%#Eval("FND_fundComposition") %>' runat="server"></asp:TextBox>
                                                </div>
                                                <%--<asp:Label ID="lblFundvalue" runat="server" Text="Label"></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkFundDelsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnFundDtlsSave1" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--Payment Details --%>
    <div id="divPaymentDetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <div id="PaymentDetails_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Payment Details</h3>
                                <i class="fa fa-money"></i>
                            </div>
                            <div class="col-md-3 HideControl">
                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkPaymentDtlsRefresh" OnClick="lnkPaymentDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnPaymentDtlsSave" CssClass="btn-primary HideControl" runat="server" Text="Save" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorpaymentdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <%--<div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Installment Amount</label>
                                    <asp:TextBox ID="txtInstalamentAmt" class="form-control lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Total Amount Paid</label>
                                    <asp:TextBox ID="txtTotalamtPaid" class="form-control lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Amount Required</label>
                                    <asp:TextBox ID="txtAmountRequired" class="form-control lblLable" Text="" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvRcptDtls" CssClass="table table-bordered table-striped" runat="server"></asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkPaymentDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnPaymentDtlsSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>



    <%--Requirement Details--%>
    <div id="divRequirementDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="updReqDtls" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="RequirementDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Requirement Details</h3>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkReqDtlsRefresh" OnClick="lnkReqDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">

                                    <asp:CheckBox ID="chkReqDtls" runat="server" AssociatedControlID="chkReqDtls" Text="Edit" />

                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnSearchCode" Text="Search Code" runat="server" OnClick="btnSearchCode_Click" CssClass="btn-primary lnkButton" />
                                    </div>
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnRequirmentDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="RequirementDtls" OnClientClick="return ValidateRequirmentDetailsBlog();" OnClick="btnRequirmentDtlsSave_Click" CssClass="btn-primary HideControl " runat="server" Text="Save" />
                                    <%--<asp:Button ID="btnRequirmentDtlsSave" OnClick="btnRequirmentDtlsSave_Click" CssClass="btn-primary " runat="server" Text="Save" />--%>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorreqdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorRequirementDetailBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <%--  <div class="form-group" >
                            <asp:Button ID="btnSearchCode" Text="Search Code" runat="server" CssClass="btn btn-primary" OnClick="btnSearchCode_Click" />
                            </div>--%>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <div class="col-md-8 HideControl">
                                            <div class="col-md-5">
                                                <asp:Label ID="lblCommonStatus" runat="server" Font-Bold="True" ForeColor="Black" Text="Select Common Status"></asp:Label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:DropDownList CssClass="form-control" Enabled="false" ID="ddlCommonStatus" runat="server" OnSelectedIndexChanged="ddlCommonStatus_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <hr>
                                        </div>

                                        <asp:GridView ID="gvRequirmentDetails" CssClass="table table-bordered table-striped lblLable RequirementBox " runat="server" AutoGenerateColumns="False" OnRowDataBound="gvRequirmentDetails_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Code">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="Code"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlfollowupcode" runat="server" OnSelectedIndexChanged="ddlfollowupcode_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                                            <asp:ListItem Value="0">--select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="ReqDescp">
                                                    <HeaderTemplate>
                                                        <asp:Label runat="server" Text="Description"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblfollowupDiscp" runat="server"></asp:Label>--%>
                                                        <asp:TextBox ID="lblfollowupDiscp" CssClass="form-control" Rows="3" runat="server"></asp:TextBox>
                                                        <%--  <textarea id="lblfollowupDiscp" class="form-control" rows="3" placeholder="Enter ..."></textarea>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="ReqDescp"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="Category"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlCategory" runat="server" ClientIDMode="Static">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Criteria">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="Criteria"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList CssClass="form-control" ID="ddlCriteria" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Life Type">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="Life Type"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList CssClass="form-control" ClientIDMode="Static" ID="ddlLifeType" runat="server">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="01">Primary</asp:ListItem>
                                                            <asp:ListItem Value="02">Secondary</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList CssClass="form-control RequirementStatus" ClientIDMode="Static" ID="ddlStatus" runat="server" onchange="fnRequirementStatus(this);">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RaisedDate">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblRaisedDate_caption" runat="server" Text="RaisedDate"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRaiseddate" runat="server" Text='<%#Eval("REQ_RaisedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="RaisedBy">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblRaisedby_caption" runat="server" Text="RaisedBy"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRaisedby" runat="server" Text='<%#Eval("REQ_raisedBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ClosedDate">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblcloseDate_caption" runat="server" Text="ClosedDate"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClosedDate" runat="server" Text='<%#Eval("REQ_ClosedDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="Closedby">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblClosedBy_caption" runat="server" Text="Closedby"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClosedBy" runat="server" Text='<%#Eval("REQ_closedBy") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--  <asp:TemplateField HeaderText="Remove">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="Remove Option"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkReqRemoveRow" CssClass="lblLable" ClientIDMode="AutoID" runat="server" OnClick="lnkReqRemoveRow_Click">
                                                            <asp:Image ID="Image8" Height="45px" runat="server" CssClass="HideControl" ImageUrl="~/dist/img/delete2.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvRequirmentDetails" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlCommonStatus" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnReqaddrows" OnClientClick="return ValidateRequirmentDtls()" CssClass="btn btn-primary HideControl lnkButton" runat="server" Text="Add New Row" OnClick="btnReqaddrows_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <%--<asp:Button CssClass="btn-link lnkButton" ID="btnMedDataentry"  OnClientClick = "SetTarget();"  runat="server" Text="Medical Data Entry" OnClick="btnMedDataentry_Click" />--%>
                                        <asp:LinkButton ID="btnMedDataentry" Text="Medical Data Entry" runat="server" OnClick="btnMedDataentry_Click1"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-2 pull-right">
                                    <div class="form-group">
                                        <label>Medical Reason</label>
                                        <asp:DropDownList ID="ddlRequirementMedicalReason" runat="server" AutoPostBack="True" CssClass="form-control lblLable" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnReqaddrows" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkReqDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnRequirmentDtlsSave1" />
                <%--<asp:AsyncPostBackTrigger ControlID="ddlfollowupcode" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlfollowupcode" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--loading details--%>
    <div id="divLoadingDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="updLoadingDetails" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div id="LoadingDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Loading Details</h3>
                                <i class="fa fa-hourglass-start"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkLoadingDtlsRefresh" OnClick="lnkLoadingDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkLoadingDtls" CssClass="" runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnLoadingDtlsSave1" OnCommand="btnCommonEvent_Command" CommandName="LoadingDtls" CssClass="btn-primary HideControl" runat="server" OnClientClick="return ValidateLoadingDetailsBlog();" Text="Save" OnClick="btnLoadingDtlsSave_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorloadingdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorLoadingDetailBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Rider Name</label>
                                    <%--<asp:TextBox ID="txtLoadRider" runat="server" CssClass="form-control lblLable"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlLoadRiderCode" CssClass="form-control lblLable" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="updLoadType" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Loading Type</label>
                                            -
                                            <asp:DropDownList ID="ddlLoadType" CssClass="form-control lblLable " runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoadType_SelectedIndexChanged" Enabled="false">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Loading Description</label>
                                            <asp:TextBox ID="txtLoadDesc" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLoadType" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Reason1</label>
                                            <asp:DropDownList ID="ddlLoadRsn1" CssClass="form-control lblLable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoadRsn1_SelectedIndexChanged" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Rsn1 Description</label>
                                            <asp:TextBox ID="txtRsn1Desc" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLoadRsn1" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Reason2</label>
                                            <asp:DropDownList ID="ddlLoadRsn2" CssClass="form-control lblLable" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLoadRsn2_SelectedIndexChanged" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Rsn2 Description</label>
                                            <asp:TextBox ID="txtRsn2Desc" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlLoadRsn2" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Loading %</label>
                                            <asp:TextBox ID="txtLoadPer" MaxLength="3" runat="server" CssClass="form-control lblLable" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Rate Adjust</label>
                                            <asp:TextBox ID="txtRateAdjust" MaxLength="4" runat="server" CssClass="form-control lblLable Numeric"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Flat Mortality</label>
                                            <asp:DropDownList ID="ddlLoadFlatMortality" CssClass="form-control lblLable" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Letter Print</label>
                                            <asp:DropDownList ID="ddlLoadletterPrint" CssClass="form-control lblLable" runat="server" Enabled="false">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label></label>
                                    <asp:Button runat="server" ID="btncalculatePrem_Load" OnClick="btncalculatePrem_Load_Click" Text="Calculate Premium" CssClass="form-control btn-primary HideControl lnkButton" />
                                </div>
                            </div>
                        </div>
                        <%-- <div>--%>
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="updAddloading" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnAddLoadingRow" CssClass="btn btn-primary HideControl lnkButton" OnClientClick="return ValidateLoadingtDtls()" runat="server" Text="Add New Row" OnClick="btnAddLoadingRow_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:Button ID="btnViewExistingLoad" OnClientClick="return false;" CssClass="btn btn-primary HideControl" runat="server" Text="View Existing Loading" />--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvLoadingDtls" CssClass="table table-bordered table-striped" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="LoadingType">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text="Loading Type"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoadType" runat="server" Text='<%# Bind("ddlLoadType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason 1">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text="Reason 1"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoadRsn1" runat="server" Text='<%# Bind("ddlLoadRsn1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Reason 2">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text="Reason 2"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoadRsn2" runat="server" Text='<%# Bind("ddlRsn2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loading %">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text="Loading %"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoadper" runat="server" Text='<%# Bind("txtLoadPer") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate Adjust">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text="Rate Adjust"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRateAdjust" runat="server" Text='<%# Bind("txtRateAdjust") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Flat Mortality">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text="Flat Mortality"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFlatMortality" runat="server" Text='<%# Bind("ddlLoadFlatMortality") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remove">
                                                        <HeaderTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text="Remove Option"></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkLoadRemoveRow" ClientIDMode="AutoID" runat="server" OnClick="lnkLoadRemoveRow_Click" CssClass="lnkButton">
                                                                <asp:Image ID="Image8" Height="45px" runat="server" ImageUrl="~/dist/img/delete2.png" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAddLoadingRow" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <div id="divPremiumDetailsLoading" runat="server" class="col-md-12 HideControl">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="lblLoadingPremium"></asp:Label>
                                </div>
                                <div class="col-md-12">
                                    <asp:Label runat="server" Font-Bold="True" CssClass="label-success" ID="lblTotalLoadingPremium"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="divExistingloading" runat="server" class="col-md-12 HideControl">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <h4>Existing Loading Details</h4>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblErrorExistingLoading" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvExtLoadDetails" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" runat="server" OnRowDataBound="gvExtLoadDetails_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="LoadingType">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Loading Type"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlExstingLoadType" CssClass="form-control lblLable" runat="server" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RiderName">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Rider Name"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRiderName" CssClass="form-control lblLable" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason 1">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Reason 1"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlExistingLoadRsn1" CssClass="form-control lblLable" runat="server" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason 2">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Reason 2"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlExistingLoadRsn2" CssClass="form-control lblLable" runat="server" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loading %">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Loading %"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExistingLoadPer" CssClass="form-control lblLable" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate Adjust">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Rate Adjust"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExistingRateAdjust" CssClass="form-control lblLable" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Flat Mortality">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Flat Mortality"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlExistingLoadFlatMort" CssClass="form-control lblLable" runat="server" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Is Letter Print"><%--HeaderStyle-CssClass="HideControl" ItemStyle-CssClass="HideControl"--%>
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Letter Printing"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="LetterPrint" CssClass="form-control lblLable" runat="server" Enabled="false">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remove Loading">
                                                <HeaderTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text="Remove Loading"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkExistingLoadRemoveRow" CssClass="lnkButton" ClientIDMode="AutoID" runat="server" OnClick="lnkExistingLoadRemoveRow_Click">
                                                        <asp:Image ID="Image8" Height="45px" runat="server" ImageUrl="~/dist/img/delete2.png" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divPremiumacal" class="col-md-12">
                        <div id="divPremiumcal_Loading" class="table table-responsive">
                            <asp:GridView ID="gridPremCal_Loading" CssClass="table table-bordered table-striped" runat="server">
                            </asp:GridView>
                        </div>
                    </div>


                </div>

            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkLoadingDtlsRefresh" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="btnLoadingDtlsSave" />--%>
                <%--<asp:AsyncPostBackTrigger ControlID="ddlLoadType" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--BMI details and Riskdetails--%>
    <div id="divSaralRiskParameter" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="upSaralRiskParameter" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="Div19" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Risk Details(Saral)</h3>
                                <i class="fa fa-money"></i>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-4" style="float: right">
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="Button3" CssClass="btn-primary HideControl" runat="server" Text="Save" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group" style="display: none">
                                    <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>BMI</label>
                                <asp:TextBox ID="txtSaralRiskBmi" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Height</label><br />
                                <asp:TextBox ID="txtHeight" CssClass="form-control " Text="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Weight</label><br />
                                <asp:TextBox ID="txtWeight" CssClass="form-control " Text="" runat="server"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2 HideControl">
                            <div class="form-group">
                                <label class="text-nowrap">UW  Due Diligence Req</label><br />
                                <asp:TextBox ID="txtSaralRiskUwDueDiligenceReq" CssClass="form-control" Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnUpadteBMI" runat="server" Text="Update BMI" CssClass="btn-primary " OnClick="btnUpadteBMI_Click" />
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Risk Score</label><br />
                                <asp:TextBox ID="txtSaralRiskScore" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>E&Y Score </label>
                                <br />
                                <asp:TextBox ID="txtENYScore" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                        <panel id="nonRevivalPanel" style="display: none">
                        <div class="col-md-2" id="Smoker" >
                            <%--<div class="form-group">
                                <label>Is Smoker</label><br />
                                <div class="col-right">
                                    <label>
                                        No
                                        <asp:CheckBox runat="server" ID="chkRiskParamIsSmoker" AutoPostBack="true" OnCheckedChanged="chkRiskParamIsSmoker_CheckedChanged" />                                        
                                        <span class="simple-switch dark"></span>Yes
                                    </label>
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <label>Is Smoker</label><br />
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="chkSaralRiskIsSmoker" AutoPostBack="true" OnCheckedChanged="chkRiskParamIsSmoker_CheckedChanged" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2" id="Branch" >
                            <div class="form-group">
                                <label>Branch Name</label><br />
                                <asp:TextBox ID="txtSaralRiskBranchName" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2" id="distance" >
                            <div class="form-group">
                                <label>Distance(KM)</label><br />
                                <asp:TextBox ID="txtdistance" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2" id="Channel" >
                            <div class="form-group">
                                <label>Channel</label><br />
                                <asp:TextBox ID="txtSaralRiskChannel" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        </panel>
                        <div class="row"></div>
                        <div class="col-md-4 HideControl">
                            <div class="form-group">
                                <label>Parm Comb</label><br />
                                <asp:TextBox ID="txtSaralRiskParamComb" TextMode="MultiLine" Font-Size="X-Small" CssClass="form-control " Rows="5" Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4 HideControl">
                            <div class="form-group">
                                <label>Remarks</label><br />
                                <asp:TextBox ID="txtSaralRiskRemark" TextMode="MultiLine" Font-Size="X-Small" CssClass="form-control " Rows="5" Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Suggestive Req</label><br />
                                <asp:TextBox ID="txtSaralRiskSuggestiveReq" Font-Size="X-Small" TextMode="MultiLine" Rows="5" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkPaymentDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnPaymentDtlsSave" />
                <asp:AsyncPostBackTrigger ControlID="chkRiskParamIsSmoker" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--UW comments--%>
    <div id="divCommentDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="updCommentsDtls" runat="server">
            <ContentTemplate>
                <div id="CommentDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-7">
                                <h3 class="box-title ">UW Comments  Details</h3>
                                <i class="fa fa-comments-o"></i>
                            </div>
                            <div class="col-md-5">
                                <%-- <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkUwCmntDtlsRefresh" OnClick="lnkUwCmntDtlsRefresh_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox ID="chkCommentDtls" CssClass="CheckSave HideControl" runat="server" Text="Edit" Visible="false" />
                                    <asp:Button ID="btnUWCommSave" AccessKey="S" AssociatedControlID="btnUWCommSave" OnCommand="btnCommonEvent_Command" CommandName="CommentDtls" OnClientClick="return ValidateCommentsDetailsBlog();" CssClass="btn-primary lnkButton" OnClick="btnUWCommSave_Click" runat="server" Text="Save" />
                                    <%-- <asp:CheckBox ID="chkCommentDtls" CssClass="CheckSave" runat="server" Text="Edit" />--%>
                                    <%--                                    <asp:Button ID="btnUWCommSave" OnClientClick="return ValidateCommentsDetailsBlog();" CssClass="btn-primary" OnClick="btnUWCommSave_Click" runat="server" Text="Save" />--%>
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-12">
                                        <asp:Button ID="btnAutoComment" CssClass="btn-primary lnkButton" OnClick="btnAddComment_Click" runat="server" Text="Auto Comment" Visible="false" />
                                        <asp:Button ID="btnCommentReset" CssClass="btn-primary " OnClientClick="return fnCommentsReset();" runat="server" Text="Reset" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="CommentsDtls_containerBody" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorcommentdtls" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorCommentDetailBody" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3 form-group">
                            <asp:DropDownList ID="ddlComment" CssClass="form-control lblLable" runat="server" Enabled="false">
                                <asp:ListItem Value="General">General Analysis</asp:ListItem>
                                <asp:ListItem Value="Personal">Personal Analysis</asp:ListItem>
                                <asp:ListItem Value="Medical">Medical Analysis</asp:ListItem>
                                <asp:ListItem Value="Financial">Financial Analysis</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:DropDownList ID="ddlCommentsearch" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCommentsearch_SelectedIndexChanged">
                                <asp:ListItem Value="General">General Analysis</asp:ListItem>
                                <asp:ListItem Value="Personal">Personal Analysis</asp:ListItem>
                                <asp:ListItem Value="Medical">Medical Analysis</asp:ListItem>
                                <asp:ListItem Value="Financial">Financial Analysis</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6 form-group">
                            <asp:Label runat="server" ID="lblCommentNotificationPremiumChange" Text="" CssClass="label-danger"></asp:Label>
                        </div>

                        <div class="col-md-12">
                            <div class="form-group">
                                <textarea class="textarea" runat="server" id="txtUWComments" placeholder="Place some text here" style="width: 100%; height: 85px; font-size: 14px; line-height: 18px; padding: 10px;"></textarea>
                            </div>
                        </div>

                        <div id="divExistingComments" class="col-md-12 HideControl" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvUWComments" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="USERNAME"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("User Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="CATEGORY"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("category") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Remark"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal ID="remarks" runat="server" Text='<%#Eval("Remark") %>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="CommentsDate"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCommentsDate" Text='<%#Eval("CommentsDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="UserID"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("User ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkUwCmntDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnUWCommSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--UW warning message--%>
    <div id="div25" runat="server" class="col-md-6">
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <div id="Div26" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-7">
                                <h3 class="box-title ">UW Warning Message</h3>
                                <i class="fa fa-comments-o"></i>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Div27" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
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
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblUwSaralWarningMessage" Font-Bold="true" ForeColor="Red"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnWarningMessage" Value="|" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkUwCmntDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnUWCommSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--hint message--%>
    <div id="div31" runat="server" class="col-md-6">
        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
                <div id="Div32" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-7">
                                <h3 class="box-title ">UW Hint Message</h3>
                                <i class="fa fa-comments-o"></i>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Div33" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblUwSaralHintMessage" Font-Bold="true" ForeColor="Green"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdnHintMessage" Value="|" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkUwCmntDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnUWCommSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--BOE Comments message--%>
    <div id="div34" runat="server" class="col-md-6">
        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
            <ContentTemplate>
                <div id="Div26" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-7">
                                <h3 class="box-title ">BOE Comments</h3>
                                <i class="fa fa-comments-o"></i>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Div27" class="box-body">

                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblBOEComments" Font-Bold="true" ForeColor="Red"></asp:Label>
                                <asp:HiddenField runat="server" ID="HiddenField1" Value="|" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkUwCmntDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnUWCommSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--decision type--%>
    <div id="div21" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <div id="Div22" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <h3 class="box-title ">Decision Type</h3>
                                <i class="fa fa-comments-o"></i>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Div23" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 form-group">
                            <asp:Label runat="server" ID="Label8" Text="" CssClass="label-danger"></asp:Label>
                        </div>
                        <%--added by shri on 23Mar18 to add new PROPOSAL TYPE/Updated by suraj on 25 APRIL 2018--%>
                        <div id="div20" class="col-md-12" runat="server">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Proposal Type</label>
                                        <asp:DropDownList ID="ddlApplicationDetailsProposalType" AutoPostBack="true" OnSelectedIndexChanged="ddlApplicationDetailsProposalType_SelectedIndexChanged" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div runat="server" id="divassign" class="col-md-2">
                                    <div class="form-group">
                                        <label>Assignment Type</label>

                                        <asp:DropDownList ID="ddlAssigmentType" Class="form-control lblLable" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--ADDED BY Suraj on 03 July 18 TO underwriter selects the NRI flag in Proposal Type, list of countries should be enabled --%>
                                <div runat="server" id="divCountry" class="col-md-4">
                                    <div class="form-group">
                                        <label>Residence country of customer</label>

                                        <asp:DropDownList ID="ddlcountry" Class="form-control lblLable" Width="50%" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--END HERE--%>
                                <%--ADDED BY SHRI TO ASK FOR POLICY HOLD--%>
                                <div class="col-md-2 HideControl">
                                    <div class="form-group">
                                        <label>Policy Printing Is To Be Holded??</label>
                                        <asp:DropDownList ID="ddlDecisionDetailsIsPolicyPrintingToHolding" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="-1">Select</asp:ListItem>
                                            <asp:ListItem Value="False">No</asp:ListItem>
                                            <asp:ListItem Value="True">Yes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--END HERE--%>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <asp:CheckBoxList runat="server" RepeatColumns="5" CellSpacing="5" ID="cblDecisionTypeDecisions" RepeatDirection="Horizontal" TextAlign="Right">
                                </asp:CheckBoxList>
                                <asp:CheckBoxList runat="server" RepeatColumns="7" CellSpacing="5" ID="cblDecisionTypeIncompleteDocument" RepeatDirection="Horizontal" TextAlign="Right">
                                </asp:CheckBoxList>
                                <asp:CheckBoxList runat="server" RepeatColumns="7" CellSpacing="5" ID="cblDecisionTypeCleanCase" RepeatDirection="Horizontal" TextAlign="Right">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkUwCmntDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnUWCommSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


    <%--added by shri to show popup for client details--%>
    <div id="divTpaDetails" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
            <ContentTemplate>
                <div id="Div2" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">TPA Details</h3>
                                <i class="fa fa-address-card-o"></i>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">

                                    <asp:LinkButton runat="server" ID="lnkTpaDetailsRefresh" CssClass="lnkButton"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="Div3" class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvTpaDetailsTpaStatus" AutoGenerateColumns="false" class="table table-bordered table-striped" runat="server">
                                    <Columns>
                                        <asp:BoundField HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" DataField="StatusDate" />
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <%--end here--%>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkTpaDetailsRefresh" />
                <%-- <asp:AsyncPostBackTrigger ControlID="btnClientDetailsAML" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--added by shri--%>
    <div id="divHealth" runat="server" style="overflow: auto; width: 100%; height: 300px" class="HideControl">
        <uc3:HealthProfile runat="server" ID="UwDecisionHealthProfile" />
    </div>
    <%--end here--%>

    <%--Loading details--%>

    <%--<div class="col-md-12">
        <asp:UpdatePanel ID="updReportDetails" runat="server">
            <ContentTemplate>
                <div class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Report Details</h3>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">
                                    <a href="#"><span class="glyphicon glyphicon-refresh"></span></a>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <asp:CheckBox runat="server" Text="Edit" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="btnReportDtlsSave" CssClass="btn-primary HideControl" runat="server" Text="Save" />
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>

    <%--ADDED BY SHRI ON 13 OCT 17 TO SHOW RISK PARAMETER--%>
    <%--Risk details--%>
    <div id="div11" runat="server" class="col-md-12 HideControl">
        <asp:UpdatePanel ID="upRiskParameter" runat="server">
            <ContentTemplate>
                <div id="Div12" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Risk Details</h3>
                                <i class="fa fa-money"></i>
                            </div>
                            <div class="col-md-3">
                                <%--<div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="LinkButton4" OnClick="LinkButton4_Click" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>--%>
                                <div class="col-md-4" style="float: right">
                                </div>
                                <div class="col-md-3" style="float: right">
                                    <asp:Button ID="Button1" CssClass="btn-primary HideControl" runat="server" Text="Save" />

                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorDetailsRiskParameter" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>BMI</label>
                                <asp:TextBox ID="txtBMI" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <%--<div class="form-group">
                                <label>Is Smoker</label><br />
                                <div class="col-right">
                                    <label>
                                        No
                                        <asp:CheckBox runat="server" ID="chkRiskParamIsSmoker" AutoPostBack="true" OnCheckedChanged="chkRiskParamIsSmoker_CheckedChanged" />                                        
                                        <span class="simple-switch dark"></span>Yes
                                    </label>
                                </div>
                            </div>--%>
                            <div class="form-group">
                                <label>Is Smoker</label><br />
                                <div class="form-group">
                                    <asp:CheckBox runat="server" ID="chkRiskParamIsSmoker" AutoPostBack="true" OnCheckedChanged="chkRiskParamIsSmoker_CheckedChanged" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group" id="IsBranch">
                                <label>Branch Name</label><br />
                                <asp:TextBox ID="txtRiskBranchName" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="text-nowrap">UW  Due Diligence Req</label><br />
                                <asp:TextBox ID="txtRiskUnderwritingDueDiligenceRequired" CssClass="form-control" Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Risk Score</label><br />
                                <asp:TextBox ID="txtRiskScore" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Channel</label><br />
                                <asp:TextBox ID="txtRiskChannel" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row"></div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Parm Comb</label><br />
                                <asp:TextBox ID="txtRiskParametersCombination" TextMode="MultiLine" Font-Size="X-Small" CssClass="form-control " Rows="5" Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Remarks</label><br />
                                <asp:TextBox ID="txtRiskRemarks" TextMode="MultiLine" Font-Size="X-Small" CssClass="form-control " Rows="5" Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Suggestive Req</label><br />
                                <asp:TextBox ID="txtRiskSuggestiveRequirement" Font-Size="X-Small" TextMode="MultiLine" Rows="5" CssClass="form-control " Text="" runat="server" onkeypress="return false;"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkPaymentDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="btnPaymentDtlsSave" />
                <asp:AsyncPostBackTrigger ControlID="chkRiskParamIsSmoker" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <%--END HERE--%>
    <%--ADDED BY SHRI ON 13 OCT 17 TO SHOW RISK PARAMETER--%>
    <%--Risk Details(Saral)--%>
    <%--END HERE--%>

    <%--UW Comments--%>

    <%--ADDED BY SURAJ FOR CLOSE FILE REVIEW QUESTIONS--%>
    <div id="divCloseFileReview" runat="server" visible="true" class="col-md-12">
        <asp:UpdatePanel ID="updCloseFileReview" runat="server">
            <ContentTemplate>
                <div id="CloseFileReview_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-7">
                                <h3 class="box-title ">Close File Review Questions</h3>
                                <i class="fa fa-comments-o"></i>
                            </div>

                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div id="CloseFileReview_containerBody" class="box-body">
                        <div class="col-md-12 error-container-div">
                            <div class="col-md-6" onclick="fnHideShowErrorMsg(this);">
                                <div class="form-group">
                                    <asp:Label ID="lblerrorclosefile" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div style="clear: both;"></div>
                            <div class="col-md-6 HideControl error-detail_div">
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 form-group">
                            <asp:GridView ID="grdclosefilerw" class="table table-bordered table-striped" OnRowDataBound="grdclosefilerw_RowDataBound" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <asp:BoundField HeaderText="QuestionId" Visible="false" DataField="QuestionId" />
                                    <asp:BoundField HeaderText="Questions" DataField="Questions" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label2" runat="server" Text="Answer"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="col-md-3 form-group">
                                                <asp:DropDownList ID="ddlAnswer" runat="server" CssClass="form-control" Width="100px" ClientIDMode="Static">
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <div id="div29" runat="server" class="col-md-12">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Case Category</label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlCaseCategory" runat="server" CssClass="form-control validate" Width="100px" ClientIDMode="Static">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="1">Error</asp:ListItem>
                                            <asp:ListItem Value="2">No error</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group ">
                                        <asp:TextBox ID="txtcloseRemark" TextMode="MultiLine" class="form-control" Font-Size="X-Small" Width="300px" Height="80px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" style="float: right">
                                        <asp:Button ID="btnCloseFileSaveAnswer" CssClass="btn-primary lnkButton" OnClientClick="return ValidateCloseFileReviewBlog();" OnClick="btnSaveAnswer_Click" runat="server" Text="Save" />
                                    </div>
                                </div>
                            </div>


                        </div>


                        <%--<div class="col-md-6 form-group">
                            <asp:Label runat="server" ID="Label13" Text="" CssClass="label-danger"></asp:Label>
                        </div>--%>

                        <%-- <div class="col-md-12">
                            <div class="form-group">
                                <textarea class="textarea" runat="server" id="Textarea1" placeholder="Place some text here" style="width: 100%; height: 85px; font-size: 14px; line-height: 18px; padding: 10px;"></textarea>
                            </div>
                        </div>--%>

                        <div id="div30" class="col-md-12 HideControl" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView3" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="USERNAME"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("User Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="CATEGORY"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("category") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Remark"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal ID="remarks" runat="server" Text='<%#Eval("Remark") %>'></asp:Literal>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="CommentsDate"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCommentsDate" Text='<%#Eval("CommentsDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="UserID"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluserName" Text='<%#Eval("User ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="lnkUwCmntDtlsRefresh" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnUWCommSave" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <%--END--%>
    <%--ADDED BY SHRI FOR UW INSTRUCTION ON 30MAR18--%>
    <%--uw waring message--%>
    <%--added by shri for uw hint message on 12JUN18--%>
    <%--uw hint message--%>
    <%--END HERE--%>

    <%--Decision Type--%>

    <div id="divDecisionDetails" runat="server" class="col-md-12">
        <asp:UpdatePanel ID="updUWdecision" runat="server">
            <ContentTemplate>
                <div id="DecisionDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12 ">
                            <div class="col-md-9">
                                <h3 class="box-title ">Decision Details</h3>
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-18" style="float: right">
                                    <asp:LinkButton ID="lnkDecisionDtlsRefresh" OnClick="lnkDecisionDtlsRefresh_Click" CssClass="lnkButton" runat="server"><span class="glyphicon glyphicon-refresh"></span></asp:LinkButton>
                                </div>
                                <div class="col-md-4" style="float: right">
                                    <asp:Button ID="btnUwdecisiondtlsSave" CssClass="btn-primary HideControl " runat="server" Text="Save" OnClientClick="return ValidateMasterSaveBlog();"
                                        OnClick="btnUwdecisiondtlsSave_Click" OnCommand="btnCommonEvent_Command" />
                                </div>
                                <div class="col-md-3" style="float: right">
                                </div>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblErrorDecisiondtls" Text="" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div>
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>UW Decision</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlUWDecesion" CssClass="form-control select2 Decision" Style="width: 100%;" runat="server" AutoPostBack="True" onchange="fnLoaderShow();" OnSelectedIndexChanged="ddlUWDecesion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divUWReason" runat="server">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>UW Reason</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlUWreason" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div id="divPostponedPeriod" class="HideControl" runat="server">
                                <div class="col-md-6">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Postponed Period</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlPeriod" class="form-control select2" Style="width: 100%;" runat="server">
                                                <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                                <asp:ListItem Value="1">1 Month</asp:ListItem>
                                                <asp:ListItem Value="2">2 Month</asp:ListItem>
                                                <asp:ListItem Value="3">3 Month</asp:ListItem>
                                                <asp:ListItem Value="4">4 Month</asp:ListItem>
                                                <asp:ListItem Value="5">5 Month</asp:ListItem>
                                                <asp:ListItem Value="6">6 Month</asp:ListItem>
                                                <asp:ListItem Value="7">7 Month</asp:ListItem>
                                                <asp:ListItem Value="8">8 Month</asp:ListItem>
                                                <asp:ListItem Value="9">9 Month</asp:ListItem>
                                                <asp:ListItem Value="10">10 Month</asp:ListItem>
                                                <asp:ListItem Value="11">11 Month</asp:ListItem>
                                                <asp:ListItem Value="12">12 Month</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkDecisionDtlsRefresh" />
                <asp:AsyncPostBackTrigger ControlID="ddlUWDecesion" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <!-- Modal -->
    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
            <div class="modal" id="divUserControlModal" role="dialog">

                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <h4 class="modal-title success"><b><span id="ModalCommon">Manage Customer Profile</span></b></h4>
                        </div>
                        <div class="modal-body well">
                            <uc2:PopupManageProposar runat="server" ID="PopupManageProposar" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" ID="btnClosePopupClientProfile" Text="Close" class="btn btn-default" OnClientClick="fnCloseClientPopup();" OnClick="btnClosePopupClientProfile_Click" />
                            <%--<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                            <%--data-dismiss="modal" --%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnRefreshClientProfile" />
            <asp:AsyncPostBackTrigger ControlID="btnClosePopupClientProfile" />
        </Triggers>
    </asp:UpdatePanel>
    <%--end here--%>
    <div class="col-md-6">
        <asp:HiddenField ID="hdnIsStaff" runat="server" />
        <asp:HiddenField ID="hdnProductType" runat="server" />
        <asp:HiddenField ID="hdnBasePP" runat="server" />
        <asp:HiddenField ID="hdnSrNo" Value="0" runat="server" />
        <asp:HiddenField runat="server" ID="hdnOldValue" />
        <asp:HiddenField runat="server" ID="hdnUserLimit" Value="0" />
        <asp:HiddenField runat="server" ID="hdfnBankName" />
        <asp:HiddenField runat="server" ID="hdfnBankAcctNumber" />

    </div>
    <%--END HERE--%>
    <%--added by shri on 29 aug 17--%>
    <%--<script type="text/javascript">
        //document.getElementById('divHealth').style.width = window.innerWidth-50;//(window.screen.width) + "px";
        document.getElementById('<%=divHealth.ClientID%>').style.maxLength = "300px";                
    </script>--%>
    <%--end here--%>
</asp:Content>

