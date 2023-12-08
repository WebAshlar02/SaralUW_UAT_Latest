<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="example1" runat="server" AutoGenerateColumns="False" OnRowCommand="example1_RowCommand" OnRowDataBound="example1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Follow Up Code">
                        <HeaderTemplate>
                            <asp:Label ID="Label1" runat="server" Text="Follow up Code"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:DropDownList CssClass="form-control" ID="ddlfollowupcode" runat="server">
                                <asp:ListItem Value="0">--select--</asp:ListItem>
                                <asp:ListItem>MBP</asp:ListItem>
                                <asp:ListItem>MCT</asp:ListItem>
                                <asp:ListItem>MEN</asp:ListItem>
                                <asp:ListItem>MGN</asp:ListItem>
                                <asp:ListItem>MIN</asp:ListItem>
                                <asp:ListItem>MPN</asp:ListItem>
                                <asp:ListItem>MRN</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Desecription" HeaderStyle-CssClass="ReqDescp">
                        <HeaderTemplate>
                            <asp:Label runat="server" Text="Discription"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblfollowupDiscp" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <HeaderTemplate>
                            <asp:Label ID="Label2" runat="server" Text="Category"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:DropDownList CssClass="form-control" ID="ddlCategory" runat="server">
                                <asp:ListItem Value="0">--select--</asp:ListItem>
                                <asp:ListItem>Medical</asp:ListItem>
                                <asp:ListItem>Non Medical</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Criteria">
                        <HeaderTemplate>
                            <asp:Label ID="Label2" runat="server" Text="Criteria"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:DropDownList CssClass="form-control" ID="ddlCriteria" runat="server">
                                <asp:ListItem Value="0">--select--</asp:ListItem>
                                <asp:ListItem>Fixed</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Life Type">
                        <HeaderTemplate>
                            <asp:Label ID="Label2" runat="server" Text="Life Type"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:DropDownList CssClass="form-control" ID="ddlLifeType" runat="server">
                                <asp:ListItem Value="0">--select--</asp:ListItem>
                                <asp:ListItem Value="01">Primary</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <HeaderTemplate>
                            <asp:Label ID="Label2" runat="server" Text="ddlLifeTypeddlLifeType"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server">
                                <asp:ListItem Value="0">--select--</asp:ListItem>
                                <asp:ListItem Value="O">Outstanding</asp:ListItem>
                                <asp:ListItem Value="W">Waived</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">LinkButton</asp:LinkButton>
        <br />

        <br />
    </form>
</body>
</html>
