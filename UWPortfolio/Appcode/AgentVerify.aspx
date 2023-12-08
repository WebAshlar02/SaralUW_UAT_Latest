<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgentVerify.aspx.cs" Inherits="Appcode_AgentVerify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:UpdatePanel ID="UpdateAgentSign" runat="server">
        <ContentTemplate>--%>
            <div class="example-modal" id="pnlAgentSign" runat="server" style="width: 90%; position: absolute; z-index: 100001;">
                <div class="modal-header" style="padding-bottom: 10px;">
                    <asp:LinkButton runat="server" ID="LinkButton6" CssClass="pull-right"><i class="fa fa-times" aria-hidden="true"></i></asp:LinkButton><%--OnClick="btncanel_Click"--%>
                    <h4 class="modal-title">Agent Signature Verification</h4>
                </div>
                <%-- <div class="modal-dialog modal-lg" style="width: 100%;">--%>
                <div class="modal-content" style="width: 100%;">
                    <div class="modal-body">
                        <div class="col-lg-12">
                            <div class="col-lg-6" runat="server" id="form12">
                                <iframe id="ifrm" width="600" height="550" runat="server"></iframe>
                            </div>
                            <div class="col-lg-6" runat="server" id="form13">
                                <iframe id="ifrm1" width="600" height="550" runat="server"></iframe>
                            </div>
                        </div>


                    </div>
                    <div style="clear: both; margin-bottom: 5px;">
                        <div class="modal-footer">
                            <asp:RadioButtonList ID="rdoAgentSign" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Value="1">YES</asp:ListItem>
                                <asp:ListItem Value="2">NO</asp:ListItem>
                                <asp:ListItem Value="3">N/A</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="rfvrdoAgentSign" runat="server" Display="Dynamic" Style="float: left;" ControlToValidate="rdoAgentSign"
                                Font-Bold="True" ForeColor="Red" ValidationGroup="vq">*</asp:RequiredFieldValidator>
                            <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Submit" ValidationGroup="vq" OnClick="btnSubmit_Click" />
                        </div>
                    </div>

                </div>
                <!-- /.modal-content -->
                <%-- </div>--%>
                <!-- /.modal-dialog -->
                <!-- /.modal -->
            </div>
            <!-- /.example-modal -->
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
