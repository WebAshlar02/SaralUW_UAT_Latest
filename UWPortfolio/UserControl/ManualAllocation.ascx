<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManualAllocation.ascx.cs" Inherits="UserControl_ManualAllocation" %>
<asp:Panel class="box box-health-question box-warning box-solid " runat="server" ID="Panel1">
    <script type="text/javascript">
        //numeric		
        $(document).on('keypress', '.Numeric', function (e) {
            var keycode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
            return ((keycode == 8 || keycode == 9) || (keycode >= 48 && keycode <= 57));
        });
        /*manage applicationNo */
        function fnManageCheckedApplicationNo(req) {
            if ($(req).find("input[type='checkbox']").is(':checked')) {
                if ($('#<%=hdnManageApplicationNo.ClientID%>').val().indexOf("|" + $(req).parent().parent().find('.applicationno').html() + "|") == -1) {
                    $('#<%=hdnManageApplicationNo.ClientID%>').val($('#<%=hdnManageApplicationNo.ClientID%>').val() + $(req).parent().parent().find('.applicationno').html() + "|");
                }
            }
            else {
                $('#<%=hdnManageApplicationNo.ClientID%>').val($('#<%=hdnManageApplicationNo.ClientID%>').val().replace("|" + $(req).parent().parent().find('.applicationno').html() + "|", "|"));
            }
        }

        function fnManageCheckedElegibleUser(req) {
            if ($(req).find("input[type='checkbox']").is(':checked')) {
                if ($('#<%=hdnManageElegibleUser.ClientID%>').val().indexOf("|" + $(req).parent().parent().find('.userid').html() + "|") == -1) {
                    $('#<%=hdnManageElegibleUser.ClientID%>').val($('#<%=hdnManageElegibleUser.ClientID%>').val() + $(req).parent().parent().find('.userid').html() + "|");
                }
            }
            else {
                $('#<%=hdnManageElegibleUser.ClientID%>').val($('#<%=hdnManageElegibleUser.ClientID%>').val().replace("|" + $(req).parent().parent().find('.userid').html() + "|", "|"));
            }
        }
        function fnValidate() {
            $('#<%=lblManualAllocationMsg.ClientID%>').html('');
            if ($('#<%=ddlAssignBy.ClientID%>').val() == '0') {
                $('#<%=lblManualAllocationMsg.ClientID%>').html('Select Allocate By');
                return false;
            }
            else if ($('#<%=ddlAssignBy.ClientID%>').val() == '1') {
                if (isNaN($('#<%=txtAssignedFrom.ClientID%>').val())) {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Please Enter Numeric Data In Assigned From Text Box');
                    return false;
                }
                if (isNaN($('#<%=txtAssignedTo.ClientID%>').val())) {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Please Enter Numeric Data In Assigned To Text Box');
                    return false;
                }
                if ($('#<%=txtAssignedFrom.ClientID%>').val() == '') {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Enter SumAssured From Value');
                    return false;
                }
                if ($('#<%=txtAssignedTo.ClientID%>').val() == '') {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Enter SumAssured To Value');
                    return false;
                }
            }
            else if ($('#<%=ddlAssignBy.ClientID%>').val() == '2') {
                if (isNaN($('#<%=txtAssignedFrom.ClientID%>').val())) {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Please Enter Numeric Data In Assigned From Text Box');
                    return false;
                }
                if (isNaN($('#<%=txtAssignedTo.ClientID%>').val())) {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Please Enter Numeric Data In Assigned To Text Box');
                    return false;
                }
                if ($('#<%=txtAssignedFrom.ClientID%>').val() == '') {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Enter Minimum Premium');
                    return false;
                }
                if ($('#<%=txtAssignedTo.ClientID%>').val() == '') {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Enter Maximum Premium');
                    return false;
                }
            }
            else if ($('#<%=ddlAssignBy.ClientID%>').val() == '4') {
                if ($('#<%=ddlUsers.ClientID%>').val() == '-1') {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Select User');
                    return false;
                }
            }
            else if ($('#<%=ddlAssignBy.ClientID%>').val() == '5') {
                if ($('#<%=txtAssignedFrom.ClientID%>').val() == '') {
                    debugger;
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Enter Application Number');

                    return false;
                }
            }
    return true;
}

function fnChangeChannel() {

    $('.Blocks').hide();
    $('.Header').hide();
}


function fnChangeAssignBy(req) {
    if ($(req).val() == '1') {
        $('.Blocks').hide();
        $('.Header').hide();
        $('#divSaveButtonBlock').hide();
        $('.AssignTo').show();
        $('#lblAssignFrom').html("Minimum SumAssured");
        $('.AssignFrom').show();
        $('#lblAssignTo').html("Maximum SumAssured");
        $('.Users').hide();

    }
    else if ($(req).val() == '2') {
        $('.Blocks').hide();
        $('.Header').hide();
        $('.AssignTo').show();
        $('#divSaveButtonBlock').hide();
        $('#lblAssignTo').html("Premium To");
        $('.AssignFrom').show();
        $('#lblAssignFrom').html("Premium From");
        $('.Users').hide();
        if (!$('#<%=txtAssignedFrom.ClientID%>').hasClass('Numeric')) {
            $('#<%=txtAssignedFrom.ClientID%>').addClass('Numeric')
        }
    }
    else if ($(req).val() == '3') {
        $('.Blocks').hide();
        $('.Header').hide();
        $('#divSaveButtonBlock').hide();
        $('.AssignTo').show();
        $('.AssignFrom').show();
        $('.Users').hide();
        if (!$('#<%=txtAssignedFrom.ClientID%>').hasClass('Numeric')) {
            $('#<%=txtAssignedFrom.ClientID%>').addClass('Numeric')
        }
    }
    else if ($(req).val() == '4') {
        $('.Blocks').hide();
        $('.Header').hide();
        $('#divSaveButtonBlock').hide();
        $('.AssignTo').hide();
        $('.AssignFrom').hide();
        $('.Users').show();
        if ($('#<%=txtAssignedFrom.ClientID%>').hasClass('Numeric')) {
            $('#<%=txtAssignedFrom.ClientID%>').removeClass('Numeric')
        }
    }
    else if ($(req).val() == '5') {
        $('.Blocks').hide();
        $('.Header').hide();
        $('#divSaveButtonBlock').hide();
        $('.AssignTo').hide();
        $('.Users').hide();
        $('.AssignFrom').show();
        $('#lblAssignFrom').html("Application Number");
        if ($('#<%=txtAssignedFrom.ClientID%>').hasClass('Numeric')) {
            $('#<%=txtAssignedFrom.ClientID%>').removeClass('Numeric')
        }
    }

}
/*end here*/
    </script>
    <div class="box-header with-border">
        <%--<div class="col-md-12">--%>
        <%-- <div class="col-md-9">
                <h3 class="box-title">Manual Allocation</h3>
            </div>--%>
        <%-- <div class="col-md-3">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>--%>
        <%--</div>--%>
    </div>
    <!-- /.box-header -->

    <div class="box-body" id="Section2" runat="server">
        <div class="row">
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <div class="col-md-12">
                    <asp:Label runat="server" ID="lblManualAllocationMsg" Font-Bold="True" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <%-- <asp:RadioButton runat="server" ID="rdoOnline" AutoPostBack="true" Text="Online" GroupName="channel" Checked="true" OnCheckedChanged="rdoOnline_CheckedChanged" />
                    <asp:RadioButton runat="server" ID="rdoOffline" AutoPostBack="true" Text="Offline" GroupName="channel" OnCheckedChanged="rdoOnline_CheckedChanged" />--%>
                        <label for="exampleInputEmail1">Channel</label>
                        <%--<asp:TextBox runat="server" ID="txtAssignedBy" class="form-control"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlChannel" OnSelectedIndexChanged="ddlChannel_SelectedIndexChanged1" CssClass="form-control">
                            <asp:ListItem Text="Channel" Value="Channel" Selected></asp:ListItem>
                            <asp:ListItem Text="Online" Value="Online"></asp:ListItem>
                            <asp:ListItem Text="Offline" Value="Offline"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <%-- <asp:RadioButton runat="server" ID="rdoOnline" AutoPostBack="true" Text="Online" GroupName="channel" Checked="true" OnCheckedChanged="rdoOnline_CheckedChanged" />
                    <asp:RadioButton runat="server" ID="rdoOffline" AutoPostBack="true" Text="Offline" GroupName="channel" OnCheckedChanged="rdoOnline_CheckedChanged" />--%>
                        <label for="exampleInputEmail1">Filter Bucket</label>
                        <%--<asp:TextBox runat="server" ID="txtAssignedBy" class="form-control"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlBucket" OnSelectedIndexChanged="ddlBucket_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Text="All" Value="All" Selected></asp:ListItem>
                            <asp:ListItem Text="Fresh" Value="Fresh"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                            <asp:ListItem Text="Resolved" Value="Resolved"></asp:ListItem>
							<%--########################## Added by Shyam Starts ###########################################--%>
							<asp:ListItem Text="Revival" Value="Revival"></asp:ListItem>
							<%--########################## Added by Shyam End ###########################################--%>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3" runat="server">
                    <div class="form-group">
                        <label for="exampleInputEmail1">Allocate By</label>
                        <%--<asp:TextBox runat="server" ID="txtAssignedBy" class="form-control"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" ID="ddlAssignBy" CssClass="form-control" onchange="fnChangeAssignBy(this);">
                            <asp:ListItem Text="Alloted By" Value="0" Selected></asp:ListItem>
                            <asp:ListItem Text="Sum Assured" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Premium" Value="2"></asp:ListItem>
                            <asp:ListItem style="display: none;" Text="Aging" Value="3"></asp:ListItem>
                            <asp:ListItem Text="User" Value="4"></asp:ListItem>
                            <asp:ListItem Text="Application Number" Value="5"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 Users" style="display: none;" runat="server" id="divUserList">
                    <div class="form-group">
                        &nbsp;
                    <label for="exampleInputEmail1">Users List</label>
                        <asp:DropDownList runat="server" ID="ddlUsers" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="form-group col-md-12">

            <div class="col-md-3 AssignTo AssignFrom" runat="server" id="divAssignedFrom">
                <div class="form-group">
                    <label id="lblAssignFrom" for="exampleInputEmail1">SumAssured From</label>
                    <asp:TextBox runat="server" ID="txtAssignedFrom" class="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-3 AssignTo" runat="server" id="divAssignedTo">
                <div class="form-group">
                    <label id="lblAssignTo" for="exampleInputEmail1">SumAssured To</label>
                    <asp:TextBox runat="server" ID="txtAssignedTo" class="form-control"></asp:TextBox>
                </div>
            </div>

            <div style="clear: both;"></div>
            <div class="col-md-12">
                <div class="form-group">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                    <div class="col-md-12" style="margin-bottom: 5%;">
                        <asp:Button runat="server" ID="btnFetch" Text="Submit" CssClass="btn-primary col-md-offset-5 pull-right" OnClientClick="return fnValidate();" OnClick="btnFetch_Click" />
                    </div>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div class="col-md-12 table-responsive">

                <div id="divApplicationNumber" runat="server" class="form-group Header" style="display: none">
                    <label class="form-control Hide-Control">Application Numbers</label>
                </div>

                <asp:DataGrid runat="server" OnPageIndexChanged="dgApplicationNo_PageIndexChanged" HeaderStyle-CssClass="btn-primary" CssClass="table table-bordered Blocks"
                    ID="dgApplicationNo" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" AllowCustomPaging="false" OnItemDataBound="dgApplicationNo_ItemDataBound">
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                Select
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%--<input type="checkbox" onchange="fnManageCheckedApplicationNo(this);" />--%>
                                <asp:CheckBox runat="server" ID="chApplication" onchange="fnManageCheckedApplicationNo(this);" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="Application Number" ItemStyle-CssClass="applicationno" DataField="ApplicationNo"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Policy Number" DataField="PolicyNumber"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Sum Assured" DataField="SumAssured"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Premium" DataField="Premium"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Aging" DataField="Aging"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                </asp:DataGrid>
            </div>
            <div class="col-md-12">
                <div id="divWithinRangeUsers" runat="server" class="form-group Header" style="display: none">
                    <label class="form-control">Users Within Range</label>
                </div>
            </div>
            <asp:DataGrid runat="server" HeaderStyle-CssClass="btn-primary" CssClass="table table-bordered Blocks" ID="dgUserWithinLimit" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="10" OnPageIndexChanged="dgUserWithinLimit_PageIndexChanged" OnItemDataBound="dgUserWithinLimit_ItemDataBound">
                <Columns>
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            Select
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%--<input type="checkbox" onchange="fnManageCheckedElegibleUser(this);" />--%>
                            <asp:CheckBox runat="server" ID="chEligibleUser" onchange="fnManageCheckedElegibleUser(this);" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn HeaderText="User Id" ItemStyle-CssClass="userid" DataField="UserId"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="User Name" DataField="UserName"></asp:BoundColumn>
                    <asp:BoundColumn HeaderText="Total" DataField="TotalCases"></asp:BoundColumn>
                </Columns>
                <PagerStyle Mode="NumericPages" />
            </asp:DataGrid>
            <div class="col-md-12">
                <div id="divRemainingUsers" runat="server" class="form-group Header" style="display: none">
                    <label class="form-control">Remaining Users</label>
                </div>
                <asp:DataGrid runat="server" HeaderStyle-CssClass="btn-primary" CssClass="table table-bordered Blocks" ID="dgUserNotInLimit" AllowPaging="true" PageSize="10"
                    OnPageIndexChanged="dgUserNotInLimit_PageIndexChanged" OnItemDataBound="dgUserNotInLimit_ItemDataBound" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderTemplate>
                                Select
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%--<input type="checkbox" onchange="fnManageCheckedElegibleUser(this);" />--%>
                                <asp:CheckBox runat="server" ID="cbNonEligibleUser" onchange="fnManageCheckedElegibleUser(this);" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="User Id" ItemStyle-CssClass="userid" DataField="UserId"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="User Name" DataField="UserName"></asp:BoundColumn>
                        <asp:BoundColumn HeaderText="Total" DataField="TotalCases"></asp:BoundColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                </asp:DataGrid>
            </div>
            <div class="col-md-12">
                <div id="divSaveButtonBlock" runat="server" class="form-group">
                    <asp:Button runat="server" ID="btnUpdateData" CssClass="btn-primary col-md-offset-5 pull-right" Text="Save" OnClick="btnUpdateData_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdnManageApplicationNo" Value="|"></asp:HiddenField>
    <asp:HiddenField runat="server" ID="hdnManageElegibleUser" Value="|"></asp:HiddenField>
</asp:Panel>
