/// <reference path="../../plugins/jQuery/jquery-2.2.3.min.js" />
function ValidatePage() {
    debugger;
    try {
        if ($('#chkAppDtls').is(":checked")) {
            if (!ValidateApplicationBlog()) {
                return false;
            }
        }
        if ($('#chkBankDtls').is(":checked")) {
            if (!ValidateBankDetailsBlog()) {
                return false;
            }
        }
        if ($('#chkPanDtls').is(":checked")) {
            if (!ValidatePanDetailsBlog()) {
                return false;
            }
        }
        if ($('#chkProductDtls').is(":checked")) {
            if (!ValidateProductDetailsBlog()) {
                return false;
            }
        }
        if ($('#chkReqDtls').is(":checked")) {
            if (!ValidateRequirmentDetailsBlog()) {
                return false;
            }
        }
        if ($('#chkLoadingDtls').is(":checked")) {
            if (!ValidateLoadingDetailsBlog()) {
                return false;
            }
        }
        if ($('#chkCommentDtls').is(":checked"))
            if (!ValidateCommentsDetailsBlog()) {
                return false;
            }
    }
    catch (err) {
        return false;
    }
    return true;
}


function ValidateRequirmentDtls() {
    //alert('req');
    debugger;
    var gridView = $("[id*=gvRequirmentDetails]");
    var status = true;
    $("#gvRequirmentDetails tr:has(td)").each(function () {
        //alert($(this).find('#ddlfollowupcode').val());
        if ($(this).find('#ddlfollowupcode').val() == "0") {
            status = false;
            $('#lblErrorinfo').html("Please Select Follow up code ");
            $('#myModal').modal();
        }
        else {
            //alert('value selected');
            $('#btnReqremoverows').removeClass('HideControl');
        }
    });
    //alert(status);
    return status;
}

function ValidateRequirmentDetailsBlog() {
    var flag = true;
    $("#gvRequirmentDetails tr:has(td)").each(function () {        
        var ddlfollowupcode = $(this).find('#ddlfollowupcode').val();        
        var ddlStatus = $(this).find('#ddlStatus').val();
        if (ddlfollowupcode != "0") {
            if (ddlStatus == "0") {
                $(this).find('#ddlStatus').addClass('EmptyVal');
                flag = false;
            }
            else {
                $(this).removeClass('EmptyVal');
            }
        }
    });
    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}
function ValidateRequirmentDetailsBlogOld() {
    var flag = true;
    $("#gvRequirmentDetails tr:has(td)").each(function () {
        var ddlfollowupcode = $(this).find('#ddlfollowupcode').val();
        var ddlStatus = $(this).find('#ddlStatus').val();
        if (ddlfollowupcode == "0") {

            $(this).find('#ddlfollowupcode').addClass('EmptyVal');
            flag = false;
        }
        else {
            $(this).removeClass('EmptyVal');
        }

        if (ddlStatus == "0") {
            $(this).find('#ddlStatus').addClass('EmptyVal');
            flag = false;
        }
        else {
            $(this).removeClass('EmptyVal');
        }

    });
    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}



function ValidateLoadingDetailsBlog() {

    var flag = true;
    var totalRows = $("#gvLoadingDtls tr").length;
    if (totalRows == "0") {

        flag = false
    }
    else {
        flag = true;
    }
    if (flag == false) {
        $('#lblErrorinfo').html("Please select at list one loading to grid before save");
        $('#myModal').modal();
    }
    return flag;

}



function ValidateFundValue() {
    var FundStatus = false;
    if (!$('#divFunddetails').hasClass('HideControl')) {
        var gridView = $("[id*=gvFundDtls]");
        var Fundcount;
        Fundcount = parseInt('00.00');
        $("#gvFundDtls tr:has(td)").each(function () {
            debugger;
            fundvalue = parseInt($(this).find('#txtFundvalue').val());
            if (fundvalue == '' || isNaN(fundvalue)) {
                fundvalue = 00;
            }
            Fundcount = parseInt(Fundcount) + fundvalue;
        });
        //alert(Fundcount);    
        if (Fundcount == '100') {
            FundStatus = true;
            $("#chkFunddtlsSave").attr("checked", false);

        }
        else {
            FundStatus = false;
            $('#lblErrorinfo').html("Total Fund Value Sholud be 100");
            $('#myModal').modal();

        }
    }
    else {
        FundStatus = true;
    }
    return FundStatus;
}

function ValidateLoadingtDtls() {
    debugger
    var Flag = true;
    var ddlLoadType = $('#ddlLoadType').val();
    if (ddlLoadType == "0") {
        $('#ddlLoadType').addClass('EmptyVal');
        Flag = false;
    }
    else {
        $('#ddlLoadType').removeClass('EmptyVal');

    }
    var ddlLoadRsn1 = $('#ddlLoadRsn1').val();
    if (ddlLoadRsn1 == "0") {
        $('#ddlLoadRsn1').addClass('EmptyVal');
        Flag = false;
    }
    else {
        $('#ddlLoadRsn1').removeClass('EmptyVal');

    }
    if (Flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return Flag;
}


function ValidateMasterSaveBlogOld() {
    var Flag = true;
    var ddlUWDecesion = $("#ddlUWDecesion option:selected").val();
    var ddlUWreason = $("#ddlUWreason option:selected").val();
    var ddlPeriod = $("#ddlPeriod option:selected").val();

    if ($('#chkLoadingDtls').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Loading  details before Post Data");
        $('#updLoadingDetails').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkAppDtls').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Application details before Post Data");
        $('#updApplicationDetails').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkBankDtls').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Bank details before Post Data");
        $('#updBankDetails').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkPanDtls').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Bank details before Post Data");
        $('#updPandetails').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkProductDtls').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Bank details before Post Data");
        $('#updProductDetails').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkRiderEdit').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Bank details before Post Data");
        $('#updRiderdtls').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkFunddtlsSave').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Bank details before Post Data");
        $('#updfundDtls').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkFunddtlsSave').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Bank details before Post Data");
        $('#updfundDtls').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    else if ($('#chkReqDtls').is(':checked')) {
        $('#lblErrorinfo').html("Please Save Requirments details before Post Data");
        $('#updReqDtls').find(':input:enabled:visible:first').focus();
        Flag = false;
    }


    if (ddlUWDecesion == "0") {
        $('#lblErrorinfo').html("Please select underwritting decesion before Post Data");
        $('#ddlUWDecesion').addClass('EmptyVal');
        $('#UpdatePanel13').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    if (ddlUWreason == "0" && $('#ddlUWDecesion').val() != 'Proposal') {
        $('#lblErrorinfo').html("Please select underwritting Reason before Post Data");
        $('#ddlUWreason').addClass('EmptyVal');
        $('#UpdatePanel13').find(':input:enabled:visible:first').focus();
        Flag = false;
    }

    if (!$('#divPostponedPeriod').hasClass('HideControl')) {
        if (ddlPeriod == "0") {
            $('#lblErrorinfo').html("Please select Postpone period");
            $('#ddlPeriod').addClass('EmptyVal');
            $('#UpdatePanel13').find(':input:enabled:visible:first').focus();
            Flag = false;
        }
    }

    if (Flag == false) {

        $('#myModal').modal();
    }

    return Flag;
}

function ValidateMasterSaveBlog() {
    var Flag = true;
    //application filter        
    if (!$('#divFunddetails').hasClass("HideControl")) {
        if ($('#chkFunddtlsSave').is(':checked')) {
            ShowModalPopup('Please Save Fund details before Post Data');
            $('#updfundDtls').find(':input:enabled:visible:first').focus();
            return false;
        }
    }

    if (!ValidateApplicationBlogNew()) {
        Flag = false;
        return Flag;
    }
    //bank details
    //if (!ValidateBankDetailsBlogNew()) {
    //    Flag = false;
    //    return Flag;
    //}

    //mandate details        
    if (!ValidateMandateDetailsBlog()) {
        Flag = false;
        return Flag;
    }

    //product details
    if (!ValidateProductDetailsBlog()) {
        Flag = false;
        return Flag;
    }


    //rider details
    //if (!fnCheckNoOfSelection()) {
    //    alert('in if');
    //    Flag = false;
    //    return Flag;
    //}
    //requirement details 
    //if (!ValidateRequirmentDetailsBlog()) {
    //    Flag = false;
    //    return Flag;
    //}

    //if (!ValidateLoadingDetailsBlog()) {
    //    Flag = false;
    //    return Flag;
    //}

    //FUNDS 
    if (!ValidateFundValue()) {
        Flag = false;
        return Flag;
    }
    return Flag;
}

function ValidateMasterPostBlog() {
    var Flag = true;
    var ddlUWDecesion = $("#ddlUWDecesion option:selected").val();
    var ddlUWreason = $("#ddlUWreason option:selected").val();
    var ddlPeriod = $("#ddlPeriod option:selected").val();

    //if ($('#chkLoadingDtls').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Loading  details before Post Data");
    //    $('#updLoadingDetails').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkAppDtls').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Application details before Post Data");
    //    $('#updApplicationDetails').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkBankDtls').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Bank details before Post Data");
    //    $('#updBankDetails').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkPanDtls').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Bank details before Post Data");
    //    $('#updPandetails').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkProductDtls').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Bank details before Post Data");
    //    $('#updProductDetails').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkRiderEdit').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Bank details before Post Data");
    //    $('#updRiderdtls').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkFunddtlsSave').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Bank details before Post Data");
    //    $('#updfundDtls').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkFunddtlsSave').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Bank details before Post Data");
    //    $('#updfundDtls').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    //else if ($('#chkReqDtls').is(':checked')) {
    //    $('#lblErrorinfo').html("Please Save Requirments details before Post Data");
    //    $('#updReqDtls').find(':input:enabled:visible:first').focus();
    //    Flag = false;
    //}
    if (ddlUWDecesion == "0") {
        $('#lblErrorinfo').html("Please select underwritting decesion before Post Data");
        $('#ddlUWDecesion').addClass('EmptyVal');
        $('#UpdatePanel13').find(':input:enabled:visible:first').focus();
        Flag = false;
    }
    if (ddlUWreason == "0" && $('#ddlUWDecesion').val() != 'Proposal') {
        $('#lblErrorinfo').html("Please select underwritting Reason before Post Data");
        $('#ddlUWreason').addClass('EmptyVal');
        $('#UpdatePanel13').find(':input:enabled:visible:first').focus();
        Flag = false;
    }

    if (!$('#divPostponedPeriod').hasClass('HideControl')) {
        if (ddlPeriod == "0") {
            $('#lblErrorinfo').html("Please select Postpone period");
            $('#ddlPeriod').addClass('EmptyVal');
            $('#UpdatePanel13').find(':input:enabled:visible:first').focus();
            Flag = false;
        }
    }
    if (Flag == false) {

        $('#myModal').modal();
        return Flag;
    }
    //application filter    
    if (!ValidateApplicationBlogNew()) {
        Flag = false;
        return Flag;
    }

    //bank details
    //if (!ValidateBankDetailsBlogNew()) {
    //    Flag = false;
    //    return Flag;
    //}

    //mandate details
    //if (!ValidateMandateDetailsBlog()) {
    //    Flag = false;
    //    return Flag;
    //}

    //product details
    if (!ValidateProductDetailsBlog()) {
        Flag = false;
        return Flag;
    }

    //FUNDS 
    if (!ValidateFundValue()) {
        Flag = false;
        return Flag;
    }
    //rider details
    //if (!fnCheckNoOfSelection()) {
    //    Flag = false;
    //    return Flag;
    //}

    if (!ValidateRequirmentDetailsBlog()) {
        Flag = false;
        return Flag;
    }

    //if (!ValidateLoadingDetailsBlog()) {
    //    Flag = false;
    //    return Flag;
    //}
    return Flag;
}

/*added by shri on 14 nov 17*/
function fnCheckNoOfSelection() {
    alert('in rider');
    debugger;
    if ($('.OnlyOneRider').length > 0) {
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
    else {
        return true;
    }
}
function ValidateApplicationBlogNew() {
    var flag = true;
    debugger;
    //check if backdate is selected or not
    if ($('#switch_havInsurance').is(':checked')) {
        if ($("#ddlBkdateReason option:selected").val() == '0') {
            $('#ddlBkdateReason').addClass('EmptyVal');
            $('#lblErrorinfo').html("field in red marked are mandatory field");
            $('#myModal').modal();
            flag = false;
            return flag;
        }
        else {
            $('#lblBackdateCaption').addClass('EmptyVal');
        }

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
        if (flag == false) {
            $('#lblErrorinfo').html("field in red marked are mandatory field");
            $('#myModal').modal();
        }
    }
    return flag;
}
function ValidateBankDetailsBlogNew() {
    var flag = true;
    if ($('#txtBnkIfsccode').val() != '') {
        $('#Bnkdtls_containerBody').find('input[type="text"]').each(function () {
            var control = $.trim($(this).val());
            if (control == '') {
                flag = false;
                $(this).addClass('EmptyVal');
            }
            else {
                $(this).removeClass('EmptyVal');
            }
        });
        if (flag == false) {
            $('#lblErrorinfo').html("field in red marked are mandatory field");
            $('#myModal').modal();
        }
    }
    return flag;
}
/*end here*/
function ValidateApplicationBlog() {
    var flag = true;

    //  alert(reason);
    if ($('#switch_havInsurance').is(':checked')) {
        flag = true;
        $('#lblBackdateCaption').removeClass('EmptyVal');
    }
    else {
        flag = false;
        $('#lblBackdateCaption').addClass('EmptyVal');
    }

    var reason = $("#ddlBkdateReason option:selected").val();
    if (reason == '0') {
        flag = false;
        $("#ddlBkdateReason").addClass('EmptyVal');
    }
    else {
        $("#ddlBkdateReason").removeClass('EmptyVal');
    }

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
    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}

function ValidateBankDetailsBlog() {
    var flag = true;
    $('#Bnkdtls_containerBody').find('input[type="text"]').each(function () {
        var control = $.trim($(this).val());
        if (control == '') {
            flag = false;
            $(this).addClass('EmptyVal');
        }
        else {
            $(this).removeClass('EmptyVal');
        }
    });
    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}

function ValidateBankDetailsBlog() {
    debugger;
    var flag = true;
    $('#Bnkdtls_containerBody').find('input[type="text"]').each(function () {
        var control = $.trim($(this).val());
        if (control == '') {
            flag = false;
            $(this).addClass('EmptyVal');
        }
        else {
            $(this).removeClass('EmptyVal');
        }
    });
    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}

function ValidatePanDetailsBlog() {
    var flag = true;
    if ($("#chkPanCopy").is(":checked") && $("#txtPannumber").val() == '') {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
        $("#txtPannumber").addClass('EmptyVal');
        return !flag;
    }
    if ($("#lblPanIsValidated").html() == 'Manual Match' && $("#txtPanComment").html() != '') {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
        $('##txtPanComment').addClass('EmptyVal');
        return !flag;
    }
    //$('#Pantls_containerBody').find('input[type="text"]').each(function () {
    //    var control = $.trim($(this).val());
    //    if (control == '') {
    //        flag = false;
    //        $(this).addClass('EmptyVal');
    //    }
    //    else {
    //        $(this).removeClass('EmptyVal');
    //    }
    //});
    //if (flag == false) {
    //    $('#lblErrorinfo').html("field in red marked are mandatory field");
    //    $('#myModal').modal();
    //}
    return flag;
}

function ValidateProductDetailsBlog() {
    var flag = true;

    $('#divProdDetailsBase').find('input[type="text"]').each(function () {
        var control = $.trim($(this).val());
        if (control == '') {
            flag = false;
            $(this).addClass('EmptyVal');
        }
        else {
            $(this).removeClass('EmptyVal');
        }
    });

    var reason = $("#ddlFrequency option:selected").val();
    if (reason == '0') {
        flag = false;
        $("#ddlFrequency").addClass('EmptyVal');
    }
    else {
        $("#ddlFrequency").removeClass('EmptyVal');
    }

    if (!$('#divcomboprod').hasClass('HideControl')) {
        $('#divcomboprod').find('input[type="text"]').each(function () {
            var control = $.trim($(this).val());
            if (control == '') {
                flag = false;
                $(this).addClass('EmptyVal');
            }
            else {
                $(this).removeClass('EmptyVal');
            }
        });

        var reason = $("#ddlComboFrequency option:selected").val();
        if (reason == '0') {
            flag = false;
            $("#ddlComboFrequency").addClass('EmptyVal');
        }
        else {
            $("#ddlComboFrequency").removeClass('EmptyVal');
        }
    }


    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}

function ValidateCommentsDetailsBlog() {
    debugger;
    var flag = true;
    var control = $("#txtUWComments").val();
    if (control == '') {
        flag = false;
        $('#txtUWComments').addClass('EmptyVal');
    }
    else {
        $('#txtUWComments').removeClass('EmptyVal');
    }

    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}


function ValidateUWDecisionDetailsBlog() {
    var flag = true;
    var ddlUWDecesion = $("#ddlUWDecesion option:selected").val();
    var ddlUWreason = $("#ddlUWreason option:selected").val();
    if (ddlUWDecesion == "0") {
        flag = false;
        $('#ddlUWDecesion').addClass('EmptyVal');
    }
    else {
        $('#ddlUWDecesion').removeClass('EmptyVal');
    }

    if (ddlUWreason == "0") {
        flag = false;
        $('#ddlUWreason').addClass('EmptyVal');
    }
    else {
        $('#ddlUWreason').removeClass('EmptyVal');
    }

    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}

/*added by shri on 06 sept for mandate details blog*/
function ValidateMandateDetailsBlog() {
    var flag = true;
    debugger;
    if ($('#ddlAutoPaytype').val() == 'SI') {
        $('#divMandatesi').find('.validate').each(function () {
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
    else if ($('#ddlAutoPaytype').val() == 'ECS') {
        $('#divMandateecs').find('.validate').each(function () {
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
    else {
        flag = true;
    }
    if (flag == false) {
        $('#lblErrorinfo').html("field in red marked are mandatory field");
        $('#myModal').modal();
    }
    return flag;
}
function fnMandateAcountNo(e) {
    if ($(e).val().length > 0) {
        if ($(e).val().charAt(0) == "4") {
            $("#txtcreditcardType").val("Visa");
        }
        else if ($(e).val().charAt(0) == "5") {
            $("#txtcreditcardType").val("MasterCard");
        }
        else if ($(e).val().charAt(0) == "3") {
            $("#txtcreditcardType").val("American Express");
        }
        else {
            $("#txtcreditcardType").val("miscellaneous");
        }

    }
    else {
        $("#txtcreditcardType").val("");
    }
    return false;
}

/*end here*/