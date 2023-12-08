<%@ Page Title="" Language="C#" MasterPageFile="~/Appcode/Bpmuwmodule.master" AutoEventWireup="true" CodeFile="CMO.aspx.cs" Inherits="Appcode_CMO" %>

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
            //$('.lnkButton').click(function () {
            //    $('#loaderdiv').fadeIn();
            //});
            //$("#lnkCommondashbord").click();            
            fnRequirementStatus($('#ddlStatus'));


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
    

                $('.paccNum').keyup(function () {
                    this.value = this.value.toUpperCase();
                });
                /*end here*/

                $('#txtRcdreq').datepicker({
                    autoclose: true,
                    format: 'dd-mm-yyyy'

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
         function ViewDocs() {
            debugger;
            /*added by shri on 10 aug 17 to add dms page of megha into our system*/            
            window.open('DMSVeiwer.aspx?ApplnNo=' + $('#lblCaptionAppNo').html().split(' ')[2]);//($('#lblCaptionAppNo').html().split(' ')[2])            
            /*end here*/
            return false;
            /*end here*/
        };
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

    <%--  <div id="ldrModal" runat="server" class="LoaderModal">
        <div class="center">
            <img alt="" src="./dist/img/loader4.gif" />
        </div>
    </div>--%>


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
                                    <asp:Button ID="btnsbmit" CssClass="btn-primary lnkButton" OnClick="btnsbmit_Click" runat="server" Text="Submit" />
                                    <asp:Button ID="btnViewDoc" CssClass="btn-primary lnkButton" OnClick="btnViewDoc_Click"  runat="server" Text="View Doc" />
                                    <%-- <asp:CheckBox ID="chkCommentDtls" CssClass="CheckSave" runat="server" Text="Edit" />--%>
                                    <%--                                    <asp:Button ID="btnUWCommSave" OnClientClick="return ValidateCommentsDetailsBlog();" CssClass="btn-primary" OnClick="btnUWCommSave_Click" runat="server" Text="Save" />--%>
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-12">
                                       <%-- <asp:Button ID="btnAutoComment" CssClass="btn-primary lnkButton" OnClick="btnAddComment_Click" runat="server" Text="Auto Comment" />--%>
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


</asp:Content>
