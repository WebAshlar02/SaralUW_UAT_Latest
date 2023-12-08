<%@ Page Title="" Language="C#" MasterPageFile="~/grpUW/GroupMasterPage.master" AutoEventWireup="true" CodeFile="UWDecision.aspx.cs" Inherits="grpUW_UWDecision" EnableEventValidation="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="Bootstrap/css/bootstrap.css" rel="stylesheet" />--%>
    <link href="Styles/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <%--<script src="Bootstrap/js/bootstrap.js"></script>--%>
    <script src="Scripts/bootstrap-datepicker.min.js"></script>
    <script type="text/javascript">
        var intUWUserKey;
        /*to check/uncheck rows in gridview*/
        function fnCheckClick(objRef) {
            //Get the Row based on checkbox
            //var row = objRef.parentNode.parentNode;
            var row = objRef.closest("TR");
            if (objRef.checked) {
                //If checked change color to Aqua //row.style.backgroundColor = "aqua";
            }
            else {
                if ($("TD", row).eq(4).find("input:text[id*=txtDecision]").val() == '') {
                    $("TD", row).eq(4).find("input:text[id*=txtDecision]").css('border', '1px solid #ccc');
                }
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color//row.style.backgroundColor = "#C2D69B";
                }
                else {//row.style.backgroundColor = "white";
                }
            }
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function fnCheckAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked check all checkboxes and highlight all rows //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked uncheck all checkboxes and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color //row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            //row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
        /*to remove row btnDeleteRow  btnDeleteRow */
        function fnRemoveGvFurtherReqRow(button) {
            //Determine the reference of the Row using the Button.
            var row = button.closest("TR");
            var name = $("TD", row).eq(1).find("[id *= lblReqName]").text();
            if (confirm("Do you want to delete: " + name)) {
                //Get the reference of the Table.
                var table = $('[id*=gvFurtherReqs]');//[0]; +
                //Delete the Table row using it's Index.
                $("#" + table.attr("id") + " tr:eq(" + row.rowIndex + ")").remove();
                var rowCount = $('[id*=gvFurtherReqs] tbody tr').length;
                if (rowCount == 0) {
                    var row = fnGetGvFurtherReqDummyRow('', '', '', '', '');
                    $("[id*=gvFurtherReqs] tbody ").append(row);
                    $("[id*=gvFurtherReqs] tbody tr:first").addClass('dummyrow');
                    /*to Deselect Additional Requrements in UWDecision DDL & unable same*/
                    $('[id*=ddlUnderwritersDecision]').val('-1');
                    $('[id*=ddlUnderwritersDecision]').attr('disabled', false);
                }
            }
            else { return false; }
            return false;
        };
        /*function for cloning row*/
        function fnGetGvFurtherReqDummyRow(ReqType, ReqTypeId, ReqName, ReqNameId, Remarks) {
            //Determine the reference of the Row using the Button.
            var row = '<tr data-html="true"><td class="otherCol1i">'
                       + '<label id="lblReqType" class="lblReqTypeCss" >' + ReqType + '</label>'
                       + '<input type="hidden" id="hdnReqTypeId" value = "' + ReqTypeId + '"/>'
                       + '</td><td class="otherCol2i">'
                       + '<label id="lblReqName" class="lblReqNameCss" >' + ReqName + '</label>'
                       + '<input type="hidden" id="hdnReqNameId" value="' + ReqNameId + '" />'
                       + '</td><td class="otherCol3i">'
                       + '<label id="lblUW_Comments" class="lblUW_CommentsCss" >' + Remarks + '</label>'
                       + '</td><td class="otherCol4i"  style="vertical-align: middle; text-align: center;">'
                       + '<input type="image" id="btnDeleteRow" style="visibility: hidden;" onclick="fnRemoveGvFurtherReqRow(this); return false;" class="btnDeleteRowCss"  src="Icons/delRec.png" />'
                       + '</td></tr>';

            /*with html controls var row = '<tr><td class="otherCol1i"><label id="lblReqType" class="lblReqTypeCss" >' + ReqType + '</label><input type="hidden" id="hdnReqTypeId" class="hdnReqTypeIdCss" value="' + ReqTypeId + '" /></td><td class="otherCol2i"><label id="lblReqName" class="lblReqNameCss" >' + ReqName + '</label><input type="hidden" id="hdnReqNameId" class="hdnReqNameIdCss"  value="' + ReqNameId + '" /></td><td class="otherCol3i"><label id="lblUW_Comments" class="lblUW_CommentsCss" >' + UW_Comments + '</label></td><td class="otherCol4i"  style="vertical-align: middle; text-align: center;"><input type="image" id="btnDeleteRow" style="visibility: hidden;" onclick="fnRemoveGvFurtherReqRow(this); return false;" class="btnDeleteRowCss"  src="Icons/delRec.png" /></td></tr>';*/
            /*<td class="otherCol4i"><input type="text" id="txtDecision" style="visibility: hidden;" value="' + Decision + '" title="" class="form-control gridtxt"  onpaste="return false" oncopy="return false" ondrag="return false" ondrop="return false" autocomplete="off" /></td>*/
            return row;
        };
        /*function for validating comments*/
        function fnValidateComment(Comment) {
            //validating data against pattern  /^[a-zA-Z0-9- ,.-]+$/
            var errorText = "";
            filter = /^[a-zA-Z0-9 .,_-]+$/;//filter = /^[a-zA-Z0-9 ',.-_]+$/; 
            //var counter = 0;
            //if (Comment == '') 
            //{
            //    errorText = "Please Enter Comments.";
            //    return errorText;
            //}
            //if (Comment != '') {
            if (Comment.length > 500) {
                errorText = "Comment Atmost Contains 500 Characters.";
                //counter++;
                //return errorText;
            }
            else if (!filter.test(Comment) && Comment.length <= 500) {
                errorText = "Invalid Text Entered";
                //counter++;
                //return errorText;
            }
            else if (filter.test(Comment) && Comment.length <= 500) {
                errorText = "Valid";
                //return errorText;
            }
            //} 
            //if (counter > 0) {
            return errorText;
            //}
        };
        /*function for fetching Previous comments*/
        function fnGetPreviousComment(MemKey) {

            var response = grpUW_UWDecision.getPreviousComments(MemKey, intUWUserKey);

            var tbl = "<table id='tblUWComments'><thead><tr>"
                        + "<th class='text-center' style='width:15%'> Reviewed By </th>"
                        + "<th class='text-center' style='width:15%'> Underwriter Status</th>"
                        + "<th class='text-center' style='width:50%;word-wrap:break-word;'> Comments </th>"
                        + "<th class='text-center' style='width:20%'> Reviewed On </th>"
                        + "</tr></thead><tbody>";

            var tds = "";

            var jsonData = JSON.parse(response.value);
            for (var i = 0; i < jsonData.length; i++) {
                /*for datetime data*/
                //var date = jsonData[i].ReviewedOn; 
                //var nowDate = new Date(parseInt(date.substr(6)));
                //console.log(nowDate.format("dd-mmm-yyyy HH:MM:ss"));
                tds += "<tr>"
                    + "<td>" + jsonData[i].ReviewedBy + "</td>"
                    + "<td>" + jsonData[i].UWDecision + "</td>"
                    + "<td>" + jsonData[i].UWComment + "</td>"
                    + "<td>" + jsonData[i].ReviewedOn + "</td>"
                    //+ "<td>" + nowDate.format("dd-mmm-yyyy HH:MM:ss") + "</td>"
                    + "</tr>";
            }

            if (jsonData.length <= 0) {
                tds += "<tr>"
                    + "<td colspan=4 class='text-center'>No Comments Available.</td>"
                    + "</tr>";
            }
            tbl += tds + "</tbody></table>"

            return tbl;
        };
        /*function for Redirecting To UW Group DashBoard*/
        function fnRedirectToDashBoard() {
            location.href = 'GrpDashBoard.aspx';
        }

        $(document).ready(function () {

            $('[id*=dpCommencementDate]').datepicker({
                format: "dd/M/yyyy",
                todayBtn: "linked",
                autoclose: true,
                todayHighlight: true,
                endDate: "Today()"
            });


            if ($('[id*=hdnUWUserKey]').val() != "")
                intUWUserKey = parseInt($('[id*=hdnUWUserKey]').val());

            //$('.lblUW_CommentsDiv').css('width', $("[id*=gvFurtherReqs] td")[2].getBoundingClientRect().width - 5);
            $("[data-toggle=tooltip]").tooltip();
            $('#dvRatedUp').hide();
            //gridrow = $("[id*=gvFurtherReqs] tbody tr:first").clone(true);

            $(".collapsibleContent").css('display', 'none');
            $("[id*=gvTop]").closest("div").css('display', 'block');
            $("[id*=BottomComments]").closest("div").css('display', 'block');


            /*to fetch previous comments & manage hide/show of bottom Comments section*/
            var UW_Status = parseInt($('[id*=hdnUwStatus]').val());

            var Current_Date = new Date();
            Current_Date.setHours(0, 0, 0, 0);
            var UW_decision_Date = new Date($('[id*=hdnUWDecisionDate]').val());
            UW_decision_Date.setHours(0, 0, 0, 0);
            var UW_decision_PostponeByMonth = parseInt($('[id*=hdnPostponeByMonth]').val());

            //var d = 8;date.setDate(date.getDate() + d);/*for adding no of days*/ 
            /*for adding no of months*/
            if (UW_decision_PostponeByMonth > 0)
                UW_decision_Date.setMonth(UW_decision_Date.getMonth() + UW_decision_PostponeByMonth);

            if (UW_Status == 1 || UW_Status == 7) {
                $('#ViewComments').hide();
                $('#AddComments').show();

                if (UW_Status == 1)
                    $('[id*=btnViewComments]').hide();
            }
            else if (UW_Status == 8
                    && (Current_Date.toString() == UW_decision_Date.toString())) {
                $('#AddComments').show();
                $('#FurtherReq_container').show();
                $('#ViewComments').hide();
            }
            else {
                $('#ViewComments').show();
                $('#AddComments').hide();
                $('#FurtherReq_container').hide();
                $('[id*=gvMedReqs] tbody').find('[id*=txtDecision]').attr('readonly', true);
                $('[id*=gvNonMedReqs] tbody').find('[id*=txtDecision]').attr('readonly', true);

                var tbl = fnGetPreviousComment($('[id*=hdnMemKey]').val());
                if (tbl != '')
                    $('#ViewComments').html(tbl);

                $('#tblUWComments').addClass("table table-bordered table-hover table-striped table-condensed ");
                $('#tblUWComments th').css('text-align', 'center');
                $('#tblUWComments th').css('color', 'maroon');
            }

            /*to disable dropdowns of member requirements grids*/
            $('[id*=ddlMedReqStatus]').attr('disabled', 'disabled');
            $('[id*=ddlNonMedReqStatus]').attr('disabled', 'disabled');
            $(".gridtxt, .bottomTxt ").each(function () {
                if ($(this).css('border-color') != "rgb(255, 0, 0)") {
                    $(this).attr('title', $(this).val());
                }
            });


            /*to disable "new" status in UWDecision DDL*/
            //$('[id*=ddlUnderwritersDecision] option').map(function () { if ($(this).val() == "1") return this; }).attr('disabled', 'disabled');
            $("[id*=ddlUnderwritersDecision] option[value='1']").remove();
            $("[id*=ddlUnderwritersDecision] option[value='6']").remove();
            //$("[id*=ddlUnderwritersDecision]").html($('[id*=ddlUnderwritersDecision] option').sort(function (x, y) {
            //    return $(x).val() < $(y).val() ? -1 : 1;
            //}))
            //$("[id*=ddlUnderwritersDecision]").get(0).selectedIndex = 0;


            if ($('[id*="lblFlag"]').text() == "Error") {
                var response = grpUW_UWDecision.getFurtherReqRowsData();
                $("[id*=gvFurtherReqs] tbody").html('');
                var rows = "";
                var jsonData = JSON.parse(response.value);
                for (var i = 0; i < jsonData.length; i++) {
                    var ReqType = jsonData[i].ReqType;
                    var ReqTypeId = jsonData[i].ReqTypeId;
                    var ReqName = jsonData[i].ReqName;
                    var ReqNameId = jsonData[i].ReqNameId;
                    var Remarks = jsonData[i].Remarks;
                    var row = fnGetGvFurtherReqDummyRow(ReqType, ReqTypeId, ReqName, ReqNameId, Remarks);
                    rows += row;
                }
                $("[id*=gvFurtherReqs] tbody").html(rows);
                $('[id*=ddlUnderwritersDecision]').val('7');
                $('[id*=ddlUnderwritersDecision]').attr('disabled', 'disabled');
                $("[id*=gvFurtherReqs] tbody tr").each(function () {
                    $(this).find("[id*=btnDeleteRow]").css('visibility', 'visible');
                    $(this).css('border', '1px solid #dddddd');
                    $(this).attr('title', '');
                });

                var response = grpUW_UWDecision.getFurtherReqErrorMsgData();
                var jsonData = JSON.parse(response.value);
                for (var i = 0; i < jsonData.length; i++) {
                    if (jsonData[i].RequirementTypeErrorMsg != '') {
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(0).css('color', 'red');
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(0).css('border', '2px solid red');
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(0).attr('data-html', true);
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(0).attr('title', jsonData[i].RequirementTypeErrorMsg);
                    }
                    if (jsonData[i].RequirementErrorMsg != '') {
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(1).css('color', 'red');
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(1).css('border', '2px solid red');
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(1).attr('data-html', true);
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(1).attr('title', jsonData[i].RequirementErrorMsg);
                    }
                    if (jsonData[i].RemarksErrorMsg != '') {
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(2).css('color', 'red');
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(2).css('border', '2px solid red');
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(2).attr('data-html', true);
                        $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).find("TD").eq(2).attr('title', jsonData[i].RemarksErrorMsg);
                    }
                }
                //$("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex).attr('title', jsonData[i].ErrorMsg);
                $("[id*=gvFurtherReqs] tbody tr").eq(jsonData[i].RowIndex - 1).focus();
                //}

                if ($("[id*=gvFurtherReqs] tbody").html() == "") {
                    var row = fnGetGvFurtherReqDummyRow('', '', '', '', '');
                    $("[id*=gvFurtherReqs] tbody ").append(row);
                    $("[id*=gvFurtherReqs] tbody tr:first").addClass('dummyrow');
                }
                //gvMedReqsDiv gvNonMedReqsDiv gvFurtherReqsDiv 
                $("[id*=gvMedReqsDiv]").closest("div").css('display', 'block');
                $("[id*=gvNonMedReqsDiv]").closest("div").css('display', 'block');
                $("[id*=gvFurtherReqsDiv]").closest("div").css('display', 'block');


                $("[id*=ddlUnderwritersDecision]").val('-1');
            }
            else {
                var row = fnGetGvFurtherReqDummyRow('', '', '', '', '');
                $("[id*=gvFurtherReqs] tbody ").append(row);
                $("[id*=gvFurtherReqs] tbody tr:first").addClass('dummyrow');
            }

            //Not allowing to press invalid characters
            $('.gridtxt').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9 ,.-_]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });
            $('.bottomTxt').keypress(function (e) {
                var regex = new RegExp("^[a-zA-Z0-9 ,.-_]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    if (this.value.length < 500) {
                        return true;
                    }
                }
                e.preventDefault();
                return false;
            });
            $('#txtRateUpPostpone').keypress(function (e) {
                var regex = new RegExp("^[0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (regex.test(str)) {
                    return true;
                }
                e.preventDefault();
                return false;
            });

            //for tooltip text on TextBoxes
            //$(".gridtxt, .bottomTxt ").hover(function () {
            //    if ($(this).css('border-color') !== "rgb(255, 0, 0)") {
            //        $(this).attr('title', $(this).val());
            //    }
            //    else {
            //        $(this).attr('title');//?
            //    }
            //});


            $(".CheckAllCss").hover(function () {
                $(this).attr('title', 'Select All');
            });

            $(".gridtxt, .bottomTxt ").keyup(function () {
                if ($(this).css('border-color') != "rgb(255, 0, 0)") {
                    $(this).attr('title', $(this).val());
                }
            });

            //for validating textboxes
            $(".gridtxt").blur(function () {
                //validating data against pattern  /^[a-zA-Z0-9- ,.-]+$/
                $(this).val($(this).val().trim());
                var gridtxt = $(this).val();
                var ErrMsg = "";
                //filter = /^[a-zA-Z0-9 .,_-]+$/;
                var isChecked = $(this).closest('tr').find('[type=checkbox]').prop('checked');
                if (isChecked && gridtxt == '') {
                    alert("Please Enter Comments.");
                    $(this).css('border', '1px solid red');
                    return false;
                }
                else if (isChecked == false && gridtxt == '') {
                    $(this).css('border', '1px solid #ccc');
                }
                if (gridtxt != '') {
                    ErrMsg = fnValidateComment(gridtxt);
                    if (ErrMsg != 'Valid') {
                        alert(ErrMsg);
                        $(this).css('border', '1px solid red');
                        return false;
                    }
                    else if (ErrMsg == 'Valid') {
                        $(this).css('border', '1px solid #ccc');
                        //return false;
                    }
                }
            });
            $(".bottomTxt").blur(function () {
                //validating data against pattern  ^[a-zA-Z0-9-]+$ ("^[a-zA-Z0-9- ,.-]+$")
                //filter = /^[a-zA-Z0-9 .,_-]+$/;
                $(this).val($(this).val().trim());
                var bottomTxt = $(this).val();
                var ErrMsg = "";
                if (bottomTxt != '') {
                    ErrMsg = fnValidateComment(bottomTxt);
                    if (ErrMsg != 'Valid') {
                        alert(ErrMsg);
                        $(this).css('border', '1px solid red');
                        return false;
                    }
                    else if (ErrMsg == 'Valid') {
                        $(this).css('border', '1px solid #ccc');
                        //return false;
                    }
                }
            });
            //for colapsible divs
            var coll = document.getElementsByClassName("collapsible");
            var i;
            for (i = 0; i < coll.length; i++) {
                coll[i].addEventListener("click", function () {
                    this.classList.toggle("active");
                    var collapsibleContent = this.nextElementSibling;
                    if (collapsibleContent.style.display === "block") {
                        collapsibleContent.style.display = "none";
                    }
                    else {
                        collapsibleContent.style.display = "block";
                        collapsibleContent.focus();
                    }
                });
            }
            //ddlUnderwritersDecision
            //for ddlUnderwritersDecision 
            var ddlUnderwritersDecision = $('[id*=ddlUnderwritersDecision]')
            //$('.ddlMedReqStatusCss , .ddlNonMedReqStatusCss').change(function () {  
            ddlUnderwritersDecision.change(function () {
                if ($(this).val() == "-1") {
                    $(this).css('border', '1px solid red');
                    alert('Please Select Underwriters Review Status');
                    $('#dvRatedUp').hide();
                    return false;
                }
                else {
                    if ($(this).css('border-color') === "rgb(255, 0, 0)") {
                        $(this).removeAttr('title');
                        $(this).css('border', '1px solid #ccc');
                    }

                    if ($(this).val() == "2") {
                        $("[id*=lblRateUpPostpone]").text('Commencement Date');
                        $("[id*=dpCommencementDate]").val('');
                        $("[id*=lblRateUpPostponeUnits]").text('');
                        $("[id*=txtRateUpPostpone]").hide();
                        $("[id*=dpCommencementDate]").show();
                        $('#dvRatedUp').show();
                        return false;
                    }
                    else if ($(this).val() == "3") {
                        $("[id*=lblRateUpPostpone]").text('Rate Up');
                        $("[id*=lblRateUpPostponeUnits]").text('in %');
                        $("[id*=txtRateUpPostpone]").attr('MaxLength', '3');
                        $("[id*=dpCommencementDate]").hide();
                        $("[id*=txtRateUpPostpone]").show();
                        $('#dvRatedUp').show();
                        return false;
                    }
                    else if ($(this).val() == "7") {
                        if ($('[id*=gvFurtherReqs] TBODY TR').eq(0).find("TD").eq(2).find('[id*=lblUW_Comments]').html() == '') {
                            alert('Please Add Additional Requirements');
                            $("[id*=gvFurtherReqs]").closest("div").css('display', 'block');
                            $('[id*=gvFurtherReqs] tfoot TR').focus();
                        }
                        $('#dvRatedUp').hide();
                        return false;
                    }
                    else if ($(this).val() == "8") {
                        $("[id*=lblRateUpPostpone]").text('Postpone Period');
                        $("[id*=dpCommencementDate]").hide();
                        $("[id*=txtRateUpPostpone]").show();
                        $("[id*=lblRateUpPostponeUnits]").text('in Months');
                        $("[id*=txtRateUpPostpone]").attr('MaxLength', '2');
                        $('#dvRatedUp').show();
                        var txtRateUpPostponeValue = $("[id*=BottomComments]").find("[id*=txtRateUpPostpone]").val();
                        if (txtRateUpPostponeValue != '' && (txtRateUpPostponeValue < 1 || txtRateUpPostponeValue > 12)) {
                            $("[id*=txtRateUpPostpone]").css('border', '1px solid red');
                            alert('Months Entered must be a Positive Number Less Than 12.');
                            return false;
                        }
                        return false;
                    }
                    $('#dvRatedUp').hide();
                    return false;
                }
            });
            $('[id*=txtRateUpPostpone]').blur(function () {
                var UW_Dec = $("[id*=BottomComments]").find("[id*=ddlUnderwritersDecision]").val();
                //var type = parseInt($(this).attr('type')); 
                var value = parseInt($(this).val());
                if (UW_Dec == '3' && (value < 1 || value > 999)) {
                    $(this).css('border', '1px solid red');
                    alert('Percentages Entered must be a Positive Number Less Than 999.');
                    return false;
                }
                else if (UW_Dec == '8' && (value < 1 || value > 12)) {
                    $(this).css('border', '1px solid red');
                    alert('Months Entered must be a Positive Number Less Than 12.');
                    return false;
                }
                else if ($(this).css('border-color') === "rgb(255, 0, 0)") {

                    $(this).removeAttr('title');
                    $(this).css('border', '1px solid #ccc');
                    return false;
                }
            });
            //for ddls in requirements grids
            var ddlReqStatus = $('[attr="ReqStatus"]')
            //$('.ddlMedReqStatusCss , .ddlNonMedReqStatusCss').change(function () {
            ddlReqStatus.change(function () {
                if ($(this).val() == "-1") {
                    //$(this).css('border', '1px solid red');
                    //alert('Please Select Requirement Type'); return false;
                }
                else {
                    if ($(this).css('border-color') === "rgb(255, 0, 0)") {
                        $(this).removeAttr('title');
                        $(this).css('border', '1px solid #ccc');
                    }
                }
            });

            //grid view other docs code  
            //var ddlReqType = $('.ddlReqTypeCss');//var ddlReqName = $('.ddlReqNameCss'); 
            var ddlReqType = $('[attr=ReqType]');
            var ddlReqName = $('[attr=ReqName]');
            ddlReqType.change(function (e) {
                if ($(this).val() == "-1") {
                    ddlReqName.empty();
                    ddlReqName.append($('<option/>', { value: -1, text: 'Select Name' }));
                    ddlReqName.val('-1');
                    //ddlReqName.prop('disabled', true);
                }
                else {
                    $(this).css('border', '1px solid #ccc');
                    var response = grpUW_UWDecision.getRequirements($(this).val(), intUWUserKey);
                    ddlReqName.empty();
                    //ddlReqName.append($('<option/>', { value: -1 , text: 'Document Name' }));
                    var listItems = "<option value='-1'>" + 'Select Name' + "</option>";
                    var jsonData = JSON.parse(response.value);
                    for (var i = 0; i < jsonData.length; i++) {
                        listItems += "<option value='" + jsonData[i].DataValue + "'>" + jsonData[i].DataText + "</option>";
                    }
                    ddlReqName.html(listItems);
                }
            });
            ddlReqName.change(function () {
                var path = $(this).val();
                if ($(this).val == "-1") {
                    ddlReqName.empty();
                    ddlReqName.append($('<option/>', { value: -1, text: 'Select Type' }));
                    ddlReqName.val('-1');
                }
                else {
                    $(this).css('border', '1px solid #ccc');
                }
                //                else {//$('#imgDoc').attr('src', path)
                //                alert(path);}
            });

            $('.btnViewCommentsCSS').click(function () {

                var tbl = fnGetPreviousComment($('[id*=hdnMemKey]').val());
                if (tbl != '')
                    $('#dvRemarks').html(tbl);

                $('#tblUWComments').addClass("table table-bordered table-hover table-striped table-condensed ");
                $('#tblUWComments th').css('text-align', 'center');
                $('#tblUWComments th').css('color', 'maroon');
                $("#dvModalRemarks").modal('show');
                $('#dvModalRemarks').on('shown.bs.modal', function () {
                    //if ($('#tblFileRemarks').height() < $('#dvRemarks').height()) {
                    //    $('#tblFileRemarks').css('margin-top', Math.round(($('#dvRemarks').height() - $('#tblFileRemarks').height()) / 2));
                    //}
                })
            });


            //for sliding image click
            $('#left_arr').click(function (e) {
                var a = $('#right_arr').css('display');
                if ($('#right_arr').css('display') == 'none') {
                    $('#RightFrame').css('width', '39%');
                    $('#LeftFrame').css('width', '60%');
                    $('#LeftFrame').css('display', 'inline');
                    $('#RightFrame').css('display', 'inline-block');
                    $(this).css('display', 'initial');
                    $('#right_arr').css('display', 'initial');
                    $('.CheckAllCss').css('width', '2%');
                    $('.gridtxtMRD_DESCRIPTION').css('width', '18%');
                    $('.gridSTATUS').css('width', '25%');
                    $('.gridDECISION').css('width', '55%');
                }
                else {
                    $('#RightFrame').css('width', '99%');
                    $('#LeftFrame').css('display', 'none');
                    $(this).css('display', 'none');
                }
            });
            $('#right_arr').click(function (e) {
                if ($('#left_arr').css('display') == 'none') {
                    $('#RightFrame').css('width', '39%');
                    $('#LeftFrame').css('width', '60%');
                    $('#LeftFrame').css('display', 'inline');
                    $('#RightFrame').css('display', 'inline-block');
                    $(this).css('display', 'initial');
                    $('#left_arr').css('display', 'initial');
                }
                else {
                    $('#LeftFrame').css('width', '99%');
                    $('#RightFrame').css('display', 'none');
                    $(this).css('display', 'none');
                    $('.CheckAllCss').css('width', '2%');
                    $('.gridtxtMRD_DESCRIPTION').css('width', '11%');
                    $('.gridSTATUS').css('width', '15%');
                    $('.gridDECISION').css('width', '72%');

                    $('.otherCol1').css('width', '15%');
                    $('.otherCol2').css('width', '20%');
                    $('.otherCol3').css('width', '60%');
                    //$('.otherCol4').css('width', '30%');
                    $('.otherCol4').css('width', '5%');

                }
            });

            //add row to table 
            var btnAdd = $("[id*=btnAddRow]");
            btnAdd.click(function (e) {
                //alert('hi');
                //row.find('tr').removeClass("dummyrow");
                var ReqTypeId = $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(1) [id*=ddlReqType]').val();
                var ReqType = $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(1) [id*=ddlReqType] option:selected').text().trim();
                var ReqName = $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(2) [id*=ddlReqName] option:selected').text().trim();
                var ReqNameId = $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(2) [id*=ddlReqName]').val();
                var UW_Comments = $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(3) [id*=txtUW_Comments]').val().trim();
                var ErrMsg = "";
                //var Decision = '';
                // alert(ReqType); alert(ReqName); alert(UW_Comments);

                if (ReqTypeId == '-1') {
                    alert('Please Select Requirement Type');
                    $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(1) [id*=ddlReqType]').css('border', '1px solid red');
                    $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(1) [id*=ddlReqType]').focus();
                    return false;
                }
                else if (ReqNameId == '-1') {
                    alert('Please Select Requirement Name');
                    $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(2) [id*=ddlReqName]').css('border', '1px solid red');
                    $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(2) [id*=ddlReqName]').focus();
                    return false;
                }
                else if (UW_Comments == '') {
                    alert('Please Add Comments');
                    $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(3) [id*=txtUW_Comments]').css('border', '1px solid red');
                    $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(3) [id*=txtUW_Comments]').focus();
                    return false;
                }
                else if (UW_Comments != '') {
                    ErrMsg = fnValidateComment(UW_Comments);
                    if (ErrMsg != 'Valid') {
                        alert(ErrMsg);
                        $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(3) [id*=txtUW_Comments]').css('border', '1px solid red');
                        $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(3) [id*=txtUW_Comments]').focus();
                        return false;
                    }
                    else if (ErrMsg == 'Valid') {
                        $('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(3) [id*=txtUW_Comments]').css('border', '1px solid #ccc');
                    }
                }
                if (ReqTypeId != '-1' && ReqNameId != '-1' && UW_Comments != '' && ErrMsg == 'Valid') {
                    var row = fnGetGvFurtherReqDummyRow(ReqType, ReqTypeId, ReqName, ReqNameId, UW_Comments);
                    //$('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(1) [id*=ddlReqType]').css('border', '1px solid #ccc');
                    //$('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(2) [id*=ddlReqName]').css('border', '1px solid #ccc');
                    //$('[id*=gvFurtherReqs] tfoot tr:first td:nth-child(3) [id*=txtUW_Comments]').css('border', '1px solid #ccc');

                    $("[id*=gvFurtherReqs] > tbody > tr:last").after(row);
                    $("[id*=gvFurtherReqs] tbody").find(".dummyrow").remove();
                    $("[id*=gvFurtherReqs] tr:last-child").find("[id*=txtDecision]").css('visibility', 'visible');
                    $("[id*=gvFurtherReqs] tr:last-child").find("[id*=btnDeleteRow]").css('visibility', 'visible');
                    $("[id*=gvFurtherReqs] tr:last-child").find("[id*=txtDecision]").val('');
                    $('[id*=gvFurtherReqs] [id*=ddlReqType]').val('-1');
                    $('[id*=gvFurtherReqs] [id*=ddlReqName]').val('-1');
                    $('[id*=gvFurtherReqs] [id*=txtUW_Comments]').val('');
                    $('[id*=gvFurtherReqs] [id*=txtUW_Comments]').attr('title', '');
                    $('[id*=gvFurtherReqs] [id*=ddlReqName]').empty();
                    $('[id*=gvFurtherReqs] [id*=ddlReqName]').append($('<option/>', { value: -1, text: 'Select Document Name' }));
                    /*to select Additional Requrements in UWDecision DDL & disable same*/
                    $('[id*=ddlUnderwritersDecision]').val('7');
                    $('[id*=ddlUnderwritersDecision]').attr('disabled', 'disabled');
                    return false;
                }
                return false;
            });

            //for displaying documents  
            var ddlMemDocs = $("[id*=ddlMemDocs]");
            ddlMemDocs.change(function (e) {
                //$('#objDoc').attr('data', '');
                if ($("[id*=ddlMemDocs]").val() != "-1") {
                    var path = '<%=strURL%>' + $("[id*=ddlMemDocs] option:selected").text();
                    //$('#objDoc').attr('data', path);
                    $('#objDoc').remove();
                    //$('#viewDocument').append('<object id="objDoc" data="' + path + '" height="500px"></object>');
                    $('#viewDocument').append('<iframe id="objDoc" class="iFrame" src="' + path + '" height="100%"></iframe>');
                    return false;
                }
                else {
                    $('#objDoc').remove();
                    $('#viewDocument').append('<iframe id="objDoc" class="iFrame" src="" height="100%"></iframe>');
                    return false;
                }
            });

            //for submitting records of grids.   [attr=btnSubmit]
            var btnSubmit = $('[attr=btnSubmit]');
            btnSubmit.click(function (e) {


                /*to disable dropdowns of member requirements grids*/
                $('[id*=ddlMedReqStatus]').attr('disabled', false);
                $('[id*=ddlNonMedReqStatus]').attr('disabled', false);

                //adding gvFurtherReq data to hidden field
                //$('#hdnFurtherReqsRowsData').html($("[id*=gvFurtherReqs] tbody").html());
                var counter = 0;
                //Loop through the Table(Grid) rows in which checkbox is selected for Medical Requiremnts
                var MedReqArr = new Array();
                var MedSelectedRowsColl = $('[id*=gvMedReqs] TBODY input[name$=CheckItem]:checked');
                if (MedSelectedRowsColl.length > 0) {
                    MedSelectedRowsColl.each(function () {
                        var row = $(this).closest('tr');
                        var objMedReq = new Object();
                        /*Uw_Comments MrdKey ReqType ReqName UwStatus UwComment*/
                        var MrdKey = row.find("TD").eq(0).html();
                        var ReqTypeId = 789;
                        var ReqName = row.find("TD").eq(2).html();
                        var UwStatus = row.find("TD").eq(3).find("select[id*=ddlMedReqStatus]").val();
                        var UwComment = row.find("TD").eq(4).find("input:text[id*=txtDecision]").val().trim();
                        var ErrMsg = "";
                        if (UwStatus == '-1') {
                            alert('Please Select Under Writer Status');
                            row.find("TD").eq(3).find("select").css('border', '1px solid red');
                            row.find("TD").eq(3).find("select").focus();
                            counter++;
                            return false;
                        }
                        else if (UwStatus != '-1') {
                            row.find("TD").eq(3).find("select").css('border', '1px solid #ccc');
                        }
                        if (UwComment == '') {
                            alert('Please Enter Under Writer Comments');
                            row.find("TD").eq(4).find("input").css('border', '1px solid red');
                            row.find("TD").eq(4).find("input").focus();
                            counter++;
                            return false;
                        }
                        else if (UwComment != '') {
                            ErrMsg = fnValidateComment(UwComment);
                            if (ErrMsg != 'Valid') {
                                alert(ErrMsg);
                                row.find("TD").eq(4).find("input").css('border', '1px solid red');
                                row.find("TD").eq(4).find("input").focus();
                                return false;
                            }
                            else if (ErrMsg == 'Valid') {
                                row.find("TD").eq(4).find("input").css('border', '1px solid #ccc');
                                //return false;
                            }
                        }
                        //else if (UwStatus != '-1' && UwComment != '') {
                        //objMedReq.MrdKey = MrdKey;
                        //objMedReq.ReqTypeId = ReqTypeId;
                        //objMedReq.ReqName = ReqName;
                        //objMedReq.Status = UwStatus;
                        //objMedReq.Remarks = UwComment;
                        //MedReqArr.push(objMedReq);
                        //}
                        //alert(row.html());
                    });
                    //alert(JSON.stringify(MedReqArr));
                }
                if (counter > 0)
                { return false; }
                //counter = 0;
                //Loop through the Table(Grid) rows in which checkbox is selected for Non-Medical Requiremnts
                var NonMedReqArr = new Array();
                var NonMedSelectedRowsColl = $('[id*=gvNonMedReqs] TBODY input[name$=CheckItem]:checked');
                if (NonMedSelectedRowsColl.length > 0) {
                    NonMedSelectedRowsColl.each(function () {
                        var row = $(this).closest('tr');
                        var objNonMedReq = new Object();
                        /*Uw_Comments MrdKey ReqType ReqName UwStatus UwComment*/
                        var MrdKey = row.find("TD").eq(0).html();
                        var ReqTypeId = 790;
                        var ReqName = row.find("TD").eq(2).html();
                        var UwStatus = row.find("TD").eq(3).find("select[id*=ddlNonMedReqStatus]").val();
                        var UwComment = row.find("TD").eq(4).find("input:text[id*=txtDecision]").val().trim();
                        var ErrMsg = "";
                        if (UwStatus == '-1') {
                            alert('Please Select Under Writer Status');
                            row.find("TD").eq(3).find("select").css('border', '1px solid red');
                            row.find("TD").eq(3).find("select").focus();
                            counter++;
                            return false;
                        }
                        else if (UwStatus != '-1') {
                            row.find("TD").eq(3).find("select").css('border', '1px solid #ccc');
                        }
                        if (UwComment == '') {
                            alert('Please Enter Under Writer Comments');
                            row.find("TD").eq(4).find("input").css('border', '1px solid red');
                            row.find("TD").eq(4).find("input").focus();
                            counter++;
                            return false;
                        }
                        else if (UwComment != '') {
                            ErrMsg = fnValidateComment(UwComment);
                            if (ErrMsg != 'Valid') {
                                alert(ErrMsg);
                                row.find("TD").eq(4).find("input").css('border', '1px solid red');
                                row.find("TD").eq(4).find("input").focus();
                                return false;
                            }
                            else if (ErrMsg == 'Valid') {
                                row.find("TD").eq(4).find("input").css('border', '1px solid #ccc');
                                //return false;
                            }
                        }
                        //else if (UwStatus != '-1' && UwComment != '') {
                        //objNonMedReq.MrdKey = MrdKey;
                        //objNonMedReq.ReqTypeId = ReqTypeId;
                        //objNonMedReq.ReqName = ReqName;
                        //objNonMedReq.Status = UwStatus;
                        //objNonMedReq.Remarks = UwComment;
                        //NonMedReqArr.push(objNonMedReq);
                        //}
                        //alert(row.html());
                    });
                    //alert(JSON.stringify(NonMedReqArr));
                }

                if (counter > 0)
                { return false; }

                //Loop through the Table(Grid) rows for Further Requiremnts
                var FurtherReqArr = new Array();
                if ($('[id*=gvFurtherReqs] TBODY TR').eq(0).find("TD").eq(3).find('[id*=btnDeleteRow]').css('visibility') == 'visible') {
                    var Cnt = 0;
                    var RowsColl = $('[id*=gvFurtherReqs] TBODY TR');
                    RowsColl.each(function () {
                        var row = $(this);
                        var objFurtherReq = new Object();
                        /*Uw_Comments MrdKey ReqType ReqName UwStatus UwComment*/
                        /*789-> med req type, 790-> non med req type, 801-> med tests, 877-> non med reqs*/
                        objFurtherReq.MrdKey = 0;
                        objFurtherReq.ReqTypeId = parseInt(row.find("TD").eq(0).find('input:hidden[id*=hdnReqTypeId]').val() == "801" ? 789 : 790);
                        objFurtherReq.ReqType = row.find("TD").eq(0).find('label[id*=lblReqType]').text().trim();
                        objFurtherReq.ReqNameId = parseInt(row.find("TD").eq(1).find('input:hidden[id*=hdnReqNameId]').val() == "" ? 0 : row.find("TD").eq(1).find('input:hidden[id*=hdnReqNameId]').val());
                        objFurtherReq.ReqName = row.find("TD").eq(1).find('label[id*=lblReqName]').text().trim();
                        objFurtherReq.Status = 0;
                        objFurtherReq.Remarks = row.find("TD").eq(2).find('label[id*=lblUW_Comments]').text().trim();
                        FurtherReqArr.push(objFurtherReq);
                    });
                }
                //alert(JSON.stringify(FurtherReqArr));


                //validate(string uw_Comments, string uw_Decision) client side
                var txtUnderwritersComments = $("[id*=BottomComments]").find('[id*=txtUnderwritersComments]');
                var ddlUnderwritersDecision = $("[id*=BottomComments]").find("[id*=ddlUnderwritersDecision]");
                var txtCMOsComments = $("[id*=BottomComments]").find("[id*=txtCMOsComments]");
                var txtHODsComments = $("[id*=BottomComments]").find("[id*=txtHODsComments]");
                var txtRateUpPostpone = $("[id*=BottomComments]").find('[id*=txtRateUpPostpone]');
                var dpCommencementDate = $("[id*=BottomComments]").find('[id*=dpCommencementDate]');

                var UW_Comments = txtUnderwritersComments.val().trim();
                var CMO_Comments = txtCMOsComments.val().trim();
                var HOD_Comments = txtHODsComments.val().trim();
                var RateUpPostponeVal = txtRateUpPostpone.val().trim();
                var CommencementDateVal = dpCommencementDate.val().trim();

                var ErrMsgUW_Comments = "";
                var ErrMsgCMO_Comments = "";
                var ErrMsgHOD_Comments = "";

                if (UW_Comments == '') {
                    alert('Please Add Underwriters Comments');
                    txtUnderwritersComments.css('border', '1px solid red');
                    txtUnderwritersComments.focus();
                    counter++;
                    return false;
                }
                else if (UW_Comments != '') {
                    ErrMsgUW_Comments = fnValidateComment(UW_Comments);
                    if (ErrMsgUW_Comments != 'Valid') {
                        alert(ErrMsgUW_Comments);
                        txtUnderwritersComments.css('border', '1px solid red');
                        txtUnderwritersComments.focus();
                        counter++;
                        return false;
                    }
                    else if (ErrMsgUW_Comments == 'Valid') {
                        txtUnderwritersComments.css('border', '1px solid #ccc');
                        //return false;
                    }
                }

                if (counter > 0)
                { return false; }

                if (CMO_Comments != '') {
                    ErrMsgCMO_Comments = fnValidateComment(CMO_Comments);
                    if (ErrMsgCMO_Comments != 'Valid') {
                        alert(ErrMsgCMO_Comments);
                        txtCMOsComments.css('border', '1px solid red');
                        txtCMOsComments.focus();
                        counter++;
                        return false;
                    }
                    else if (ErrMsgCMO_Comments == 'Valid') {
                        txtCMOsComments.css('border', '1px solid #ccc');
                        //return false;
                    }
                }

                if (counter > 0)
                { return false; }

                if (HOD_Comments != '') {
                    ErrMsgHOD_Comments = fnValidateComment(HOD_Comments);
                    if (ErrMsgHOD_Comments != 'Valid') {
                        alert(ErrMsgHOD_Comments);
                        txtHODsComments.css('border', '1px solid red');
                        txtHODsComments.focus();
                        counter++;
                        return false;
                    }
                    else if (ErrMsgHOD_Comments == 'Valid') {
                        txtHODsComments.css('border', '1px solid #ccc');
                        //return false;
                    }
                }

                if (counter > 0)
                { return false; }

                var UW_Dec = ddlUnderwritersDecision.val();
                if (UW_Dec == '-1') {
                    alert('Please Select Underwriters Decision');
                    ddlUnderwritersDecision.css('border', '1px solid red');
                    ddlUnderwritersDecision.focus();
                    counter++;
                    return false;
                }
                else if (UW_Dec == '7' && FurtherReqArr.length == 0) {
                    alert('Please Add Additional Requirements');
                    $("[id*=gvFurtherReqs]").closest("div").css('display', 'block');
                    $('[id*=gvFurtherReqs] tfoot TR').focus();
                    counter++;
                    return false;
                }
                else {
                    ddlUnderwritersDecision.css('border', '1px solid #ccc');
                    //return false;
                }

                if (counter > 0)
                { return false; }
                
                var Current_Financial_Year_Start_Date = new Date();
                if (Current_Financial_Year_Start_Date.getMonth() < 3) {
                    Current_Financial_Year_Start_Date.setFullYear(Current_Financial_Year_Start_Date.getFullYear() - 1);
                }
                Current_Financial_Year_Start_Date.setDate(1);
                Current_Financial_Year_Start_Date.setMonth(3);

                if (UW_Dec == '2' && CommencementDateVal == '') {
                    dpCommencementDate.css('border', '1px solid red');
                    dpCommencementDate.focus();
                    counter++;
                    alert('Please Select Commencement Date.');
                    return false;
                }
                else if (UW_Dec == '2' && CommencementDateVal != '' && Current_Financial_Year_Start_Date > new Date(CommencementDateVal)) {
                    dpCommencementDate.css('border', '1px solid red');
                    dpCommencementDate.focus();
                    counter++;
                    alert('Select Commencement Date after 01-APR-' + Current_Financial_Year_Start_Date.getFullYear());
                    return false;
                }
                else if ((UW_Dec == '3' || UW_Dec == '8') && RateUpPostponeVal != '') {
                    RateUpPostponeValue = parseInt(RateUpPostponeVal);
                }
                else if (UW_Dec == '3' && RateUpPostponeVal == '') {
                    txtRateUpPostpone.css('border', '1px solid red');
                    txtRateUpPostpone.focus();
                    counter++;
                    alert('Enter Rate Up Percentages.');
                    return false;
                }
                else if (UW_Dec == '8' && RateUpPostponeVal == '') {
                    txtRateUpPostpone.css('border', '1px solid red');
                    txtRateUpPostpone.focus();
                    counter++;
                    alert('Enter No of Months to Postpone.');
                    return false;
                }
                else if (UW_Dec == '3' && (RateUpPostponeValue < 1 || RateUpPostponeValue > 999)) {
                    txtRateUpPostpone.css('border', '1px solid red');
                    txtRateUpPostpone.focus();
                    counter++;
                    alert('Percentages Entered must be a Positive Number Less Than 999.');
                    return false;
                }
                else if (UW_Dec == '8' && (RateUpPostponeValue < 1 || RateUpPostponeValue > 12)) {
                    txtRateUpPostpone.css('border', '1px solid red');
                    txtRateUpPostpone.focus();
                    counter++;
                    alert('Months Entered must be a Positive Number Less Than 12.');
                    return false;
                }
                else {
                    txtRateUpPostpone.css('border', '1px solid #ccc');
                    dpCommencementDate.css('border', '1px solid #ccc');
                }

                if (counter > 0)
                { return false; }

                //if (UW_Dec != '-1' && UW_Comments != '' && ErrMsgUW_Comments == 'Valid') {
                //var objComments = new Object();
                //objComments.UWComment = txtUnderwritersComments.val();
                //objComments.CMOComment = txtCMOsComments.val();
                //objComments.HODComment = txtHODsComments.val();
                //objComments.UWStatus = ddlUnderwritersDecision.val();
                //alert(JSON.stringify(objComments));
                //}

                //validate(string uw_Comments, string uw_Decision) server side
                //var response = grpUW_UWDecision.validateFormConrols(objComments.UWComment, objComments.UWStatus);
                //var ResMsg = JSON.parse(response.value);
                //if (ResMsg != '') {
                //    alert(ResMsg);
                //}
                //else {
                //SENDING DATA OF FORM TO SESSIONS

                $('[id*=ddlUnderwritersDecision]').attr('disabled', false);
                $.ajax({
                    type: "POST",
                    url: "UWDecision.aspx/btnSubmitData",
                    //data: JSON.stringify({ MedReq: MedReqArr, NonMedReq: NonMedReqArr, FurtherReq: FurtherReqArr, BottomComments: objComments }),
                    data: JSON.stringify({ FurtherReq: FurtherReqArr }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        //alert("User details has been added successfully.");
                        //window.location.reload();
                    },
                    failure: function (response) {
                        //alert('1');
                        swal({
                            title: "Oops!",
                            text: "Something went wrong.",
                            icon: "error",
                            button: "Ok!",
                            confirmButtonColor: '#7F0000'
                        });
                    },
                    error: function (response) {
                        //alert('2');
                        swal({
                            title: "Oops!",
                            text: "Something went wrong.",
                            icon: "error",
                            button: "Ok!",
                            confirmButtonColor: '#7F0000'
                        });
                    }
                });
                //}
                //return false;
            });
        });
    </script>
    <%--if user has disabled javascript from browser--%>
    <%-- <noscript>
      <div class="jfk-butterBar jfk-butterBar-warning jfk-butterBar-shown">
      <p> JavaScript enabled in your browser.
      <a class="noscript" href="javascript:window.location.reload();" style="color:red;" >
      Refresh this page after you have enabled JavaScript.
      </a>
      </p>
      </div>
    </noscript>--%>
    <style type="text/css">
        th {
            text-align: center;
        }
        /*select {
            padding: 0px 0px;
        }*/
        .MiddleFrame {
            width: 100%;
            height: 100%;
            padding: 0px;
            border: none;
        }

        h3.box-title {
            cursor: pointer;
        }

        .LeftFrame {
            width: 60%;
            height: 500px;
            float: left;
            overflow: auto;
        }

        .Arrows {
            float: inherit;
            width: 1%;
            height: 500px;
            float: left;
            display: inline-block;
            background-color: Silver;
        }

        .ArrowContent {
            position: relative;
            top: 50%;
            transform: translateY(-50%);
            background-color: Silver; /*orange:#f39c12 ,skyblue:#AED6F1*/
            text-align: center;
        }

        .RightFrame {
            position: relative;
            width: 39%;
            height: 500px;
            float: left;
            display: inline-block;
            border-left: none;
            overflow: auto;
        }
        /*.iFrame
        {
            width:100% ;
            height:100%;
        }*/

        .bottomTxt {
            width: 100%;
            height: 100%;
            float: left;
            vertical-align: middle;
            overflow: hidden;
            margin: 3px 0 3px 0;
        }

        #BottomComments #ddlUnderwritersDecision {
            margin: 3px 0 3px 0;
        }

        .gridtxtMRD_DESCRIPTION {
            width: 18%;
        }

        .gridSTATUS {
            width: 25%;
        }

        .gridDECISION {
            width: 55%;
        }

        .otherCol1 {
            width: 15%;
        }

        .otherCol2 {
            width: 20%;
        }

        .otherCol3 {
            width: 60%;
        }

        /*.otherCol4 {
            width: 30%;
        }*/

        .otherCol4 {
            width: 5%;
        }

        .otherCol1i {
            width: 15%;
            height: 18px;
            overflow: hidden;
            text-align: center;
        }

        .otherCol2i {
            width: 20%;
            height: 18px;
            overflow: hidden;
            text-align: center;
        }

        .otherCol3i {
            width: 60%;
            height: 18px;
            text-align: center;
            position: relative;
            overflow: wrap;
            word-wrap: break-word;
        }
        /*overflow:wrap; word-wrap:break-word; */
        /*.otherCol4i {
            width: 30%;
            height: 18px;
            overflow: hidden;
            text-align: left;
        }*/

        .otherCol4i {
            width: 5%;
            height: 18px;
            overflow: hidden;
            text-align: left;
        }
        /*.lblUW_CommentsCss {
            white-space: nowrap;
            width: 100%; 
            overflow: hidden;
            text-overflow: ellipsis; 
            display: inline-block;
        }*/

        #CrHeDecFiled_container {
            margin-top: 10px;
        }

        #VwMedRep_container {
            margin-top: 10px;
        }

        #viewDocument {
            /*margin: 3% 3% 3% 3%;*/
            position: relative;
            text-align: center;
            width: 100%;
            min-height: 500px;
        }

        #objDoc {
            text-align: center;
            width: 100%;
            height: 100%;
            min-height: 500px;
        }

        .CheckAllCss {
            width: 2%;
            text-align: left;
            width: 16px;
        }

        .lblReqTypeCss, .lblReqNameCss, .lblUW_CommentsCss {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 100;
        }

        .form-group {
            margin-bottom: 5px;
        }

        .divInspolinfo {
            margin-bottom: 10px;
        }

        .gridMRD_KEY {
            display: none;
        }

        .required {
            color: red;
        }
        
        /*#dvModalRemarks {*/
        #dvModalDialogRemarks {
            width: 80%;
            margin: 30px auto;
        }
    
        .modal-header,.modal-footer {
            border-bottom-color: #f4f4f4;
            background-color: #7F0000;
            color: white;
            height: 60px;
            text-align: center;
        }
        .modal-footer .btn {
            color: #7f0000;
            text-decoration: none;
        }
        #tblUWComments th, #tblUWComments td:last-child, #tblUWComments td:first-child, #tblUWComments td:nth-child(2) {
            text-align: center;
        }
        #dvRemarks {
            overflow: auto;
            width: 100%;
            height: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <%--<div class="panel panel-info">
            <div class="panel panel-heading ">Underwritting Decision</div>
            <div class="panel-body">--%>
        <div id="divInspolinfo" runat="server" class="col-md-12 HideControl divInspolinfo ">
            <div id="Inspolinfo_container" class="box box-warning box-solid">
                <div class="box-header with-border collapsible">
                    <div class="col-md-12">
                        <h3 class="box-title ">
                            <img src="Icons/m_down.png" alt="Down Arrow" />
                            Insurance Policy Information</h3>
                        <%--<i class="fa fa-user-o"></i>--%>
                    </div>
                </div>
                <div id="gvTop" class="box-body collapsibleContent">

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Name of Member</label><%--<asp:Label runat="server" ID="lblNameOfMember" Text="Name of Member"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtNameOfMember" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Name of Spouse</label><%--<asp:Label runat="server" ID="lblNameOfSpouse" Text="Name of Spouse"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtNameOfSpouse" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label><%--<asp:Label runat="server" ID="lblDate" Text="Date"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtDate" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Policy No.</label><%--<asp:Label runat="server" ID="lblPolicyNo" Text="Policy No."></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtPolicyNo" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Company Name</label><%--<asp:Label runat="server" ID="lblCompanyName" Text="Company Name"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtCompanyName" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Religare Housing Finance</label><%--<asp:Label runat="server" ID="lblReligareHousingFinance" Text="Religare Housing Finance"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtReligareHousingFinance" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Effective Date</label><%--<asp:Label runat="server" ID="lblEffectiveDate" Text="Effective Date"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtEffectiveDate" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Application No</label><%--<asp:Label runat="server" ID="lblApplicationNo" Text="Application ID"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtApplicationNo" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date Of Birth</label><%--<asp:Label runat="server" ID="lblDateOfBirth" Text="Date Of Birth"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtDateOfBirth" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Gender</label><%--<asp:Label runat="server" ID="lblGender" Text="Gender"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtGender" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Total Sum Assured For Life</label><%--<asp:Label runat="server" ID="lblTotalSumAssuredForLife" Text="Total Sum Assured For Life"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtTotalSumAssuredForLife" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Medical Called For</label><%--<asp:Label runat="server" ID="lblMedicalCalledFor" Text="Medical Called For"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtMedicalCalledFor" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Free Cover Limit For Life</label><%--<asp:Label runat="server" ID="lblFreeCoverLimitForLife" Text="Free Cover Limit For Life"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtFreeCoverLimitForLife" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Medical Sum Assured For Life</label><%--<asp:Label runat="server" ID="lblMedicalSumAssuredForLife" Text="Medical Sum Assured For Life"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtMedicalSumAssuredForLife" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Member Number</label><%--<asp:Label runat="server" ID="lblMemberNo" Text="Member Number"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtMemberNo" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">Aging</label><%--<asp:Label runat="server" ID="lblAging" Text="Aging"></asp:Label>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:TextBox ID="txtAging" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>
                                <%--<asp:TextBox ID="txtMemKey" CssClass="form-control lblLable" Text="" runat="server" ReadOnly="true"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div id="divMidFrame" runat="server" class="col-md-12 ">
            <div id="MidFrame_container" class="box box-warning box-solid">
                <%--<div class="box-header with-border collapsible">
                                <div class="col-md-12">
                                    <h3 class="box-title "><img src="Icons/m_down.png" alt="Down Arrow" /> Group Operations Comments</h3>
                                    <i class="fa fa-user-o"></i>
                                </div>
                        </div>--%>
                <div class="box-body MiddleFrame">
                    <div id="LeftFrame" class="LeftFrame">

                        <div id="DivCrHeDecFiled" runat="server" class="col-md-12 HideControl">
                            <div id="CrHeDecFiled_container" class="box box-warning box-solid">
                                <div class="box-header with-border collapsible">
                                    <h3 class="box-title ">
                                        <img src="Icons/s_down.png" alt="Down Arrow" />
                                        Correct Health Declaration Filed</h3>
                                    <%--<i class="fa fa-user-o"></i>--%>
                                </div>
                                <div id="gvMedReqsDiv" class="box-body collapsibleContent">
                                    <asp:GridView ID="gvMedReqs" runat="server" CssClass="table table-bordered table-striped lblLable RequirementBox grid" AutoGenerateColumns="False" OnRowDataBound="gvMedReqsRowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="MRD_KEY" HeaderText="Req Key." ReadOnly="True" SortExpression="MRD_KEY">
                                                <HeaderStyle CssClass="gridMRD_KEY" />
                                                <ItemStyle CssClass="gridMRD_KEY" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderStyle-CssClass="CheckAllCss">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckAll" runat="server" onclick="fnCheckAll(this);" />
                                                    <%--data-toggle="tooltip" title="Select All !"--%>
                                                    <%--<input type='checkbox' id="CheckAll" onclick="fnCheckAll(this);" data-toggle="tooltip" title="Select All !" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckItem" runat="server" onclick="fnCheckClick(this);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="MRD_DESCRIPTION" HeaderText="TEST NAME">
                                                <HeaderStyle CssClass="gridtxtMRD_DESCRIPTION" />
                                                <ItemStyle CssClass="gridtxtMRD_DESCRIPTION" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="STATUS">
                                                <HeaderStyle CssClass="gridSTATUS" />
                                                <ItemStyle CssClass="gridSTATUS" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnMedReqStatus" runat="server" Value='<%# Bind("MRD_STATUS") %>' />
                                                    <asp:DropDownList ID="ddlMedReqStatus" CssClass="form-control ddlMedReqStatusCss" runat="server" DataValueField="LKP_VALUE" DataTextField="LKP_DESC" attr="ReqStatus"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Underwriter Comments	">
                                                <HeaderStyle CssClass="gridDECISION" />
                                                <ItemStyle CssClass="gridDECISION" />
                                                <ItemTemplate>
                                                    <div style=" width:100%; position:relative;"> 
                                                        <div  style=" width:95%; position:relative; float:left;"> 
                                                            <asp:TextBox CssClass="form-control gridtxt" ID="txtDecision" placeholder="Add Your Comments Here" runat="server" onpaste="return true" ondrop="return false" autocomplete="off" MaxLength="500"></asp:TextBox>
                                                        </div>
                                                        <div  style=" width:5%; position:relative; float:right; margin-top: 10px;"> 
                                                            <asp:HiddenField ID="hdnMedReqRemarks" runat="server" Value='<%# Bind("MRR_REMARKS") %>' />
                                                            <asp:Image ID="imgMedReqRemarks" src="Icons/clipboard.png" runat="server" alt="Previous Comment"/>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="MRD_CREATED_ON" HeaderText="ADDED ON">
                                                <HeaderStyle CssClass="gridtxtMRD_CREATED_ON" />
                                                <ItemStyle CssClass="gridtxtMRD_CREATED_ON" />
                                            </asp:BoundField>

                                            <%--<asp:BoundField DataField="MRR_REMARKS" HeaderText="PREVIOUS COMMENTS">
                                                <HeaderStyle CssClass="gridtxtMRR_REMARKS" />
                                                <ItemStyle CssClass="gridtxtMRR_REMARKS" />
                                            </asp:BoundField>--%>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            Don't Have Any Medical Requirements.
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div id="DivNonMedReq" runat="server" class="col-md-12 HideControl">
                            <div id="NonMedReq_container" class="box box-warning box-solid">
                                <div class="box-header with-border collapsible">
                                    <h3 class="box-title ">
                                        <img src="Icons/s_down.png" alt="Down Arrow" />
                                        Non Medical Requirement</h3>
                                    <%--<i class="fa fa-user-o"></i>--%>
                                </div>
                                <div id="gvNonMedReqsDiv" class="box-body collapsibleContent">
                                    <asp:GridView ID="gvNonMedReqs" runat="server" CssClass="table table-bordered table-striped lblLable RequirementBox grid" AutoGenerateColumns="False" OnRowDataBound="gvNonMedReqsRowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="MRD_KEY" HeaderText="Req Key." ReadOnly="True" SortExpression="MRD_KEY">
                                                <HeaderStyle CssClass="gridMRD_KEY" />
                                                <ItemStyle CssClass="gridMRD_KEY" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderStyle-CssClass="CheckAllCss">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="CheckAll" runat="server" onclick="fnCheckAll(this);" />
                                                    <%--data-toggle="tooltip" title="Select All!"--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckItem" runat="server" onclick="fnCheckClick(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="MRD_DESCRIPTION" HeaderText="TEST NAME">
                                                <HeaderStyle CssClass="gridtxtMRD_DESCRIPTION" />
                                                <ItemStyle CssClass="gridtxtMRD_DESCRIPTION" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="STATUS">
                                                <HeaderStyle CssClass="gridSTATUS" />
                                                <ItemStyle CssClass="gridSTATUS" />
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnNonMedReqStatus" runat="server" Value='<%# Bind("MRD_STATUS") %>' />
                                                    <asp:DropDownList ID="ddlNonMedReqStatus" CssClass="form-control ddlNonMedReqStatusCss" runat="server" DataValueField="LKP_VALUE" DataTextField="LKP_DESC" attr="ReqStatus"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Underwriter Comments">
                                                <HeaderStyle CssClass="gridDECISION" />
                                                <ItemStyle CssClass="gridDECISION" />
                                                <ItemTemplate>
                                                    <div style=" width:100%; position:relative;"> 
                                                        <div  style=" width:95%; position:relative; float:left;"> 
                                                            <asp:TextBox CssClass="form-control gridtxt" ID="txtDecision" placeholder="Add Your Comments Here" runat="server" onpaste="return true" ondrop="return false" autocomplete="off" MaxLength="500"></asp:TextBox>
                                                        </div>
                                                        <div  style=" width:5%; position:relative; float:right; margin-top: 10px;"> 
                                                            <asp:HiddenField ID="hdnNonMedReqRemarks" runat="server" Value='<%# Bind("MRR_REMARKS") %>' />
                                                            <asp:Image ID="imgNonMedReqRemarks" src="Icons/clipboard.png" runat="server" alt="Previous Comment"/>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:BoundField DataField="MRD_CREATED_ON" HeaderText="ADDED ON">
                                                <HeaderStyle CssClass="gridtxtMRD_CREATED_ON" />
                                                <ItemStyle CssClass="gridtxtMRD_CREATED_ON" />
                                            </asp:BoundField>

                                        </Columns>
                                        <EmptyDataTemplate>
                                            Don't Have Any Non-Medical Requirements.
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div id="DivFurtherReq" runat="server" class="col-md-12 HideControl">
                            <div id="FurtherReq_container" class="box box-warning box-solid">
                                <div class="box-header with-border collapsible">
                                    <h3 class="box-title ">
                                        <img src="Icons/s_down.png" alt="Down Arrow" />
                                        Add Further Requirements</h3>
                                    <%--<i class="fa fa-user-o"></i>--%>
                                </div>
                                <div id="gvFurtherReqsDiv" class="box-body collapsibleContent">
                                    <table id="gvFurtherReqs" class="table table-bordered table-striped lblLable RequirementBox grid">
                                        <thead>
                                            <tr>
                                                <th class="otherCol1">Requirement Type</th>
                                                <th class="otherCol2">Requirement Name</th>
                                                <th class="otherCol3">Underwriter Comments</th>
                                                <%--<th class="otherCol4">Decision</th>--%>
                                                <th class="otherCol4">Action</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td class="otherCol1f">
                                                    <asp:DropDownList ID="ddlReqType" CssClass="form-control ddlReqTypeCss" runat="server" attr="ReqType">
                                                        <%--DataValueField="LKP_KEY" DataTextField="LKP_DESC"--%>
                                                        <asp:ListItem Selected="True" Text="Select Type" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Medical" Value="801"></asp:ListItem>
                                                        <asp:ListItem Text="Non-Medical" Value="877"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="otherCol2f">
                                                    <asp:DropDownList ID="ddlReqName" CssClass="form-control ddlReqNameCss" runat="server" attr="ReqName">
                                                        <asp:ListItem Selected="True" Text="Select Name" Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="otherCol3f">
                                                    <input type="text" id="txtUW_Comments" title="" class="form-control gridtxt" placeholder="Add Your Comments Here" onpaste="return true" ondrop="return false" autocomplete="off" /><%--<asp:TextBox ID="txtUW_Comments" title="" runat="server" CssClass="form-control gridtxt" placeholder="Add Your Comments Here" onpaste="return true" ondrop="return false" autocomplete="off" /></td>--%>
                                                </td>
                                                <td class="otherCol4f" style="vertical-align: middle; text-align: center;">
                                                    <input type="image" id="btnAddRow" class="btnAddRowCss" src="Icons/addRec.png" /><%--<asp:ImageButton ID="btnAddRow" CssClass="btnAddRowCss" runat="server" ImageUrl="Icons/addRec.png" />--%>
                                                </td>
                                                <%--<td class="otherCol5f">colspan="2"</td>--%>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="Arrows" class="Arrows">
                        <div class="ArrowContent">
                            <img id="left_arr" src="Icons/s_left.png" alt="Left Arrow" />
                            <img id="right_arr" src="Icons/s_right.png" alt="Right Arrow" />
                        </div>
                    </div>
                    <div id="RightFrame" class="RightFrame">
                        <%--<iframe id="objDoc" class="iFrame" src="viewReport.aspx" height="100%"></iframe>--%>
                        <div id="divVwMedRep" runat="server" class="col-md-12 HideControl">
                            <div id="VwMedRep_container" class="box box-warning box-solid">
                                <div class="box-header with-border ">
                                    <div class="col-md-12">
                                        <h3 class="box-title ">View Medical Reports</h3>
                                        <i class="fa fa-user-o"></i>
                                    </div>
                                </div>
                                <div id="gvright" class="box-body ">
                                    <div class="row">
                                        <div class="col-md-4 col-sm-12 text-center">
                                            <div class="form-group">
                                                <label>Document Name </label>
                                                <%--<asp:Label runat="server" ID="lblReqName" Text="Document Name :"></asp:Label>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-8 col-sm-12">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlMemDocs" CssClass="form-control ddlView" runat="server" DataValueField="DOC_PATH" DataTextField="NAME_OF_FILE" onOnClientClick="myfunction();"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col">
                                        <div id="viewDocument" style="height: 500px">
                                            <%--<object id="objDoc" data="" title="Document Viewer" height="100%"></object>--%>
                                            <iframe id="objDoc" class="iFrame" src="" height="100%"></iframe>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divGrpOpComm" runat="server" class="col-md-12 ">
            <div id="GrpOpComm_container" class="box box-warning box-solid">
                <div class="box-header with-border collapsible">
                    <div class="col-md-12">
                        <h3 class="box-title ">
                            <img src="Icons/m_down.png" alt="Down Arrow" />
                            Group Operations Comments</h3>
                        <%--<i class="fa fa-user-o"></i>--%>
                    </div>
                </div>
                <div id="BottomComments" class="box-body collapsibleContent">

                    <div id="AddComments">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Underwriter's Comments</label><span class="required">*</span><%--<asp:Label runat="server" ID="lblUnderwritersComments" Text="Underwriter's Comments"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox class="form-control bottomTxt" ID="txtUnderwritersComments" placeholder="Add Your Comments Here" runat="server" Rows="2" TextMode="MultiLine" onpaste="return true" ondrop="return false" autocomplete="off" MaxLength="500"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>CMO's Comments</label><%--<asp:Label runat="server" ID="lblCMOsComments" Text="CMO's Comments"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox class="form-control bottomTxt" ID="txtCMOsComments" placeholder="Add Your Comments Here" runat="server" Rows="2" TextMode="MultiLine" onpaste="return true" ondrop="return false" autocomplete="off" MaxLength="500"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>HOD's Comments</label><%--<asp:Label runat="server" ID="lblHODsComments" Text="HOD's Comments"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:TextBox class="form-control bottomTxt" ID="txtHODsComments" placeholder="Add Your Comments Here" runat="server" Rows="2" TextMode="MultiLine" onpaste="return true" ondrop="return false" autocomplete="off" MaxLength="500"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Underwriter's Decision</label><span class="required">*</span><%--<asp:Label runat="server" ID="lblUnderwritersDecision" Text="Underwriter's Decision"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlUnderwritersDecision" CssClass="form-control ddlUnderwritersDecisionCss lblLable" runat="server" attr="UWDecision" DataValueField="LKP_VALUE" DataTextField="LKP_DESC"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div id="dvRatedUp" class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label id="lblRateUpPostpone"></label>
                                    <span class="required">*</span><%--<asp:Label runat="server" ID="lblUnderwritersDecision" Text="Underwriter's Decision"></asp:Label>--%>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group" style="position: relative; float: left; width: 10%;">
                                    <asp:TextBox class="form-control bottomTxt" ID="txtRateUpPostpone" placeholder="" runat="server" onpaste="return false;" ondrop="return false;" autocomplete="off"></asp:TextBox>
                                    <asp:TextBox class="form-control" ID="dpCommencementDate" placeholder="" runat="server" onkeypress="return false;" onpaste="return false;" ondrop="return false;" autocomplete="off"></asp:TextBox>
                                    <%--<input class="form-control" id="txtRateUpPostpone" type="text" maxlength="3" onpaste="return false;" ondrop="return false;" runat="server"/>--%>
                                </div>
                                <div class="" style="position: relative;">
                                    <label id="lblRateUpPostponeUnits" class="col-md-4" style="margin-top: 5px;"></label>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6 text-right">
                                <input id="btnViewComments" class="btn-primary btnViewCommentsCSS" type="button" value="View Previous Comments" />
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <asp:Button ID="btnSubmit" CssClass="btn-primary" runat="server" Text="Submit" attr="btnSubmit" OnClick="btnSubmit_Click" />
                                    <%--<asp:Label ID="hdnFurtherReqsRowsData" CssClass="hdnFurtherReqsRowsDataCss" Text="" runat="server" />--%>
                                    <%--<asp:HiddenField ID="hdnFurtherReqsRowsData" runat="server" Value="" />--%>
                                    <input type="hidden" id="hdnMemKey" runat="server" />
                                    <input type="hidden" id="hdnUwStatus" runat="server" />
                                    <input type="hidden" id="hdnPostponeByMonth" runat="server" />
                                    <input type="hidden" id="hdnUWDecisionDate" runat="server" />
                                    <input type="hidden" id="hdnUWUserKey" value="" runat="server"/>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-offset-6 col-md-6 errorMsg" align="center">
                                <div class="form-group">
                                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                    <div style="display: none;">
                                        <asp:Label ID="lblFlag" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="ViewComments">
                        <div class="row text-center">
                            <input id="btnViewUWComments" class="btn-primary btnViewCommentsCSS" type="button" value="View Previous Comments"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <div class="row text-left">
                <input id="btnBack" class="btn-primary btnBackCSS" type="button" value="Back" onclick='fnRedirectToDashBoard();'/>
                <%--<button id="btnBack" class="btn-primary btnBackCSS" onclick='fnRedirectToDashBoard();'><i class="fa fa-arrow-circle-o-left"></i> Back </button>--%>
            </div>
        </div>
    </div>
    <div class="modal fade" id="dvModalRemarks" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" id="dvModalDialogRemarks"  role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <span id="modal-header-text" class="panel-title">Comments</span>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="dvRemarks">
                        
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

