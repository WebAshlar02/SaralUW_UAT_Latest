<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserRefer.ascx.cs" Inherits="UserControl_UserRefer" %>
<script>    
</script>

<!-- /.box-header -->
<asp:UpdatePanel runat="server" ID="upManualAllocation">
    <ContentTemplate>
        <%--<script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>--%>
        <script>
            function fnValidateReferOption()
            {
                if ($('#<%=ddlAssignBy.ClientID%>').val() == '0')
                {
                    $('#<%=lblManualAllocationMsg.ClientID%>').html('Select Allocate By');
                    return false;
                }
                return true;
            }

        </script>
        <div class="box-body" id="Section2" runat="server">
            <div class="row">
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <span class="lblLabel Lablefailure">Please select decision type from previous page before proceeding</span>
                </div>
                <div class="col-md-12">
                    <asp:Label runat="server" ID="lblManualAllocationMsg" Font-Bold="True" ForeColor="Red"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <div id="Div1" class="col-md-6" runat="server">
                    <div class="form-group">
                       <%-- <label for="exampleInputEmail1">Assigned By</label>--%>
                        <%--<asp:TextBox runat="server" ID="txtAssignedBy" class="form-control"></asp:TextBox>--%>
                        <%--      <asp:DropDownList runat="server" ID="ddlAssignBy" CssClass="form-control" onchange="fnChangeAssignBy(this);">
                    <asp:ListItem Text="Refer To Cmo" Selected="True" Value="signoff"></asp:ListItem>
                </asp:DropDownList>--%>
                        <label for="exampleInputEmail1">Allocate By</label>
                        <asp:DropDownList runat="server" ID="ddlAssignBy" data-backdrop="static" data-keyboard="false"  CssClass="form-control" AutoPostBack="true" onchange="$('#divReferOption').modal('hide');" OnSelectedIndexChanged="ddlAssignBy_SelectedIndexChanged">
                            <asp:ListItem Text="SELECT" Value="0"></asp:ListItem>
                            <%--<asp:ListItem Text="CMO SIGNOFF" Value="CMOSIGNOFF"></asp:ListItem>--%>
                            <asp:ListItem Text="UW SIGNOFF" Value="UWSIGNOFF"></asp:ListItem>
                            <asp:ListItem Text="REFER TO UW" Value="UWREFER"></asp:ListItem>
                            <%--<asp:ListItem Text="SENDTO SOURCE" Value="RESEND"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>
                </div>
                <div id="Div2" class="col-md-6" runat="server">
                    <div class="col-md-6">
                        <label for="exampleInputEmail1">Application Number</label>
                        <asp:Label runat="server" ID="lblApplicationNos"></asp:Label>
                    </div>
                    <div class="com-md-6">
                        <label for="exampleInputEmail1">Channel</label>
                        </br>
                <asp:Label runat="server" ID="lblChannel"></asp:Label>
                    </div>
                </div>
                <div class="col-md-12 ">
                    <div class="form-group">
                        &nbsp;
                    <label for="exampleInputEmail1">Users List</label>
                        <asp:DropDownList runat="server" ID="ddlUsers" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-6">
                        <asp:Button runat="server" ID="btnRefer" Text="Refer" OnClientClick="return fnValidateReferOption();" OnClick="btnRefer_Click" CssClass="btn-primary" />
                            </div>
                        <div class="col-md-6 HideControl" style="display:none;">
                            <asp:Button runat="server" ID="btnReSend" Text="ReRefer" OnClick="btnReSend_Click" CssClass="btn-primary" />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnRefer" />
        <asp:AsyncPostBackTrigger ControlID="ddlAssignBy" />
    </Triggers>
</asp:UpdatePanel>
