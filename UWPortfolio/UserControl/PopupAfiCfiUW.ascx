<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopupAfiCfiUW.ascx.cs" Inherits="UserControl_PopupAfiCfiUW" %>
<%--<link href="../bootstrap/css/bootstrap.css" rel="stylesheet" />
<script src="../bootstrap/js/bootstrap.min.js"></script>--%>
<script>
    function fnFetchAFLInfo() {
        //hide error msg
        $('#lblError').addClass('hide');
        $('.children').remove();
        //validate 
        //application & policy no validation
        //if ($('#txtApplnNumber').val() == '') {
        //    if ($('#txtPolicyNumber').val() == '') {
        //        $('#lblError').removeClass('hide');
        //        $('#lblError').show().html('Enter either application number or policy number');
        //        $('#txtApplnNumber').focus();
        //        return;
        //    }
        //}

        if ($('#txtPolicyNumber').val() == '') {
            $('#lblError').removeClass('hide');
            $('#lblError').show().html('Enter policy number');
            $('#txtPolicyNumber').focus();
            return;
        }
        //comment validation
        if ($('#txtComment').val() == '') {
            $('#lblError').removeClass('hide');
            $('#lblError').show().html('Enter comment');
            $('#txtComment').focus();
            return;
        }

        //afi type validation
        if ($("#ddlAFIType").val() == '-1') {
            $('#lblError').removeClass('hide');
            $('#lblError').show().html('Select AFI Type');
            $('#ddlAFIType').focus();
            return;
        }

        //rcd valiation
        if ($("#ddlAFIType").val() == '2' && $("#txtRCD").val() == '') {
            $('#lblError').removeClass('hide');
            $('#lblError').show().html('Select RCD');
            $('#txtRCD').focus();
            return;
        }

        //underwriter validation
        if ($('#<%=ddlAssignTo.ClientID%>').val() == '-1') {
            $('#lblError').removeClass('hide');
            $('#lblError').show().html('Select Underwriter');
            $('#<%=ddlAssignTo.ClientID%>').focus();
            return;
        }
        //call method        
        var response = UserControl_PopupAfiCfiUW.FetchAFIWithUnderWriterSelection($('#txtApplnNumber').val(), $('#txtPolicyNumber').val(), $('#txtComment').val(), $('#ddlAFIType').val(), $('#txtRCD').val(), $('#<%=ddlAssignTo.ClientID%>').val());
        var splits = response.value.split('#');
        //validate response
        if (splits[0] == "0") {
            var Json = JSON.parse(splits[1]);
            var clone = '';
            $('#tblPolicyDetails').show();
            clone = $('.main').clone().removeClass('main').addClass('children').show();
            $(clone).find('.ApplnNumber').html(Json.ApplicationNumber);
            $(clone).find('.PolNumber').html(Json.PolicyNumber);
            $(clone).find('.PolStatus').html(Json.PolicyStatus);
            $(clone).find('.LifeAssuredName').html(Json.FirstNameLgivName);
            $('#tblPolicyDetails tbody:last').append(clone);
            $('#lblError').removeClass('hide').html('success');
        }
        else {
            $('#tblPolicyDetails').hide();
            $('#lblError').removeClass('hide').html(splits[1]);
        }
    }

    function fnFetchCFLInfo() {
        //hide error msg
        $('#lblError').hide();
        $('.children').remove();

        //validate request
        if ($('#txtApplnNumber').val() == '') {
            if ($('#txtPolicyNumber').val() == '') {
                $('#lblError').removeClass('hide');
                $('#lblError').show().html('Enter either application number or policy number');
                $('#txtApplnNumber').focus();
                return;
            }
        }
        if ($('#txtComment').val() == '') {
            $('#lblError').removeClass('hide');
            $('#lblError').show().html('Enter comment');
            $('#txtComment').focus();
            return;
        }

        //call method        
        var response = UserControl_PopupAfiCfiUW.FetchCFI($('#txtApplnNumber').val(), $('#txtPolicyNumber').val(), $('#txtComment').val());
        var splits = response.value.split('#');
        //validate response
        if (splits[0] == "0") {
            var Json = JSON.parse(splits[1]);
            var clone = '';
            $('#tblPolicyDetails').show();
            clone = $('.main').clone().removeClass('main').addClass('children').show();
            $(clone).find('.ApplnNumber').html(Json.ApplicationNumber);
            $(clone).find('.PolNumber').html(Json.PolicyNumber);
            $(clone).find('.PolStatus').html(Json.PolicyStatus);
            $(clone).find('.LifeAssuredName').html(Json.FirstNameLgivName);
            $('#tblPolicyDetails tbody:last').append(clone);
            $('#lblError').removeClass('hide').html('success');
        }
        else {
            $('#tblPolicyDetails').hide();
            $('#lblError').removeClass('hide').html(splits[1]);
        }
    }

    function fnFetchUWInfo() {
        //hide error msg
        $('#lblError').addClass('hide');
        $('.children').remove();

        //validate request
        if ($('#txtApplnNumber').val() == '') {
            if ($('#txtPolicyNumber').val() == '') {
                $('#lblError').removeClass('hide');
                $('#lblError').show().html('Enter either application number or policy number');
                $('#txtApplnNumber').focus();
                return;
            }
        }
        if ($('#txtComment').val() == '') {
            $('#lblError').removeClass('hide');
            $('#lblError').show().html('Enter comment');
            $('#txtComment').focus();
            return;
        }

        //call method                
        var response = UserControl_PopupAfiCfiUW.FetchUW($('#txtApplnNumber').val(), $('#txtPolicyNumber').val(), $('#txtComment').val());
        var splits = response.value.split('#');
        //validate response
        if (splits[0] == "0") {
            var Json = JSON.parse(splits[1]);
            var clone = '';
            $('#tblPolicyDetails').show();
            clone = $('.main').clone().removeClass('main').addClass('children').show();
            $(clone).find('.ApplnNumber').html(Json.ApplicationNumber);
            $(clone).find('.PolNumber').html(Json.PolicyNumber);
            $(clone).find('.PolStatus').html(Json.PolicyStatus);
            $(clone).find('.LifeAssuredName').html(Json.FirstNameLgivName);
            $('#tblPolicyDetails tbody:last').append(clone);
            $('#lblError').removeClass('hide').html('success');
        }
        else {
            $('#tblPolicyDetails').hide();
            $('#lblError').removeClass('hide').html(splits[1]);
        }
    }
</script>
<link href="./plugins/datepicker/datepicker3.css" rel="stylesheet" />
<script src="./plugins/datepicker/bootstrap-datepicker.js"></script>
<%--<div class="well">--%>
<div class="col-xs-12 col-md-12 col-lg-12">
    <div class="form-group">
        <span id="lblError" class="label-danger label hide"></span>
    </div>
</div>
<div class="col-xs-12 col-md-6 col-lg-6" style="display: none">
    <div class="form-group">
        <label>Application Number</label>
        <input type="text" id="txtApplnNumber" maxlength="9" class="form-control lblLable" />
    </div>
</div>
<div class="col-xs-12 col-md-6 col-lg-6">
    <div class="form-group">
        <label>Policy Number</label>
        <input type="text" id="txtPolicyNumber" maxlength="9" class="form-control lblLable" />
    </div>
</div>
<div class="col-xs-12 col-md-6 col-lg-6">
    <div class="form-group">
        <label>Assign To</label>
        <asp:DropDownList runat="server" ID="ddlAssignTo" AutoPostBack="false" class="form-control ddlAssignTo"></asp:DropDownList>
    </div>
</div>
<div class="col-xs-12 col-md-12 col-lg-12">
    <div class="form-group">
        <label>Comment</label>
        <input type="text" id="txtComment" class="form-control lblLable" />
    </div>
</div>
<div class="row"></div>
<div class="col-xs-12 col-md-6 col-lg-6">
    <div class="form-group">
        <label>AFI Type</label>
        <select class="form-control " id="ddlAFIType">
            <option value="-1">Select</option>
            <option value="1">Customer Request</option>
            <option value="2">Internal Error  </option>
            <option value="3">Cheque Bounce</option>
        </select>
    </div>
</div>
<div class="col-xs-12 col-md-6 col-lg-6">
    <div class="form-group">
        <label>RCD</label>
        <input type="text" id="txtRCD" class="form-control DatePicker" />
    </div>
</div>
<div class="row"></div>
<div class="col-xs-12 col-md-12 col-lg-12">
    <div class="form-group">
        <table id="tblPolicyDetails" class="table-bordered">
            <tr>
                <td>Application Number
                </td>
                <td>Policy Number
                </td>
                <td>Policy Status
                </td>
                <td>Life Assured name
                </td>
            </tr>
            <tr class="main">
                <td><span class="ApplnNumber">1</span>
                </td>
                <td><span class="PolNumber">2</span>
                </td>
                <td><span class="PolStatus">3</span>
                </td>
                <td><span class="LifeAssuredName">4</span>
                </td>
            </tr>
        </table>
    </div>
</div>
<div class="col-xs-12 col-md-6 col-lg-6">
    <div class="form-group">
        <br />
        <input type="button" class="btn btn-primary" id="btnAfiSubmit" value="Fetch AFI Details" onclick="return fnFetchAFLInfo();" />
        <input type="button" class="btn btn-primary" id="btnCfiSubmit" value="Fetch CFI Details" onclick="return fnFetchCFLInfo();" />
        <input type="button" class="btn btn-primary" id="btnUWSubmit" value="Fetch UW Details" onclick="return fnFetchUWInfo();" />
    </div>
</div>
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
<%--</div>--%>
