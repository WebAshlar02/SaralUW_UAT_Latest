<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CMOApprove.aspx.cs" Inherits="Appcode_CMOApprove" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</head>
<body style="padding: 20px">
    <br />
    <form runat="server" style="border: 1px solid rgba(0,0,0,.125); border-radius: .25rem; padding: 20px">
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="Application_Number">Application Number</label>
                    <asp:TextBox ReadOnly="true" class="form-control" ID="Application_Number" runat="server"></asp:TextBox>
                </div>

            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="Follow_Up_Code">Follow Up Code</label>
                    <asp:TextBox ReadOnly="true" class="form-control" ID="Follow_Up_Code" runat="server">UCN</asp:TextBox>
                </div>

            </div>
        </div>
        <div class="form-group">
            <label for="Status">Status</label>
            <asp:DropDownList ReadOnly="true" class="form-control" ID="Status" runat="server">
                <asp:ListItem Text="Resolved" />
            </asp:DropDownList>
        </div>
        <div class="form-group">
            <label for="Comments">Comments</label>
            <asp:TextBox TextMode="MultiLine" ID="txtComments" class="form-control" Rows="3" runat="server"></asp:TextBox>
        </div>
         <asp:Button runat="server" ID="btnSubmit" type="button" class="btn btn-primary" Text="Submit" Height="36px" OnClick="btnSubmit_Click"></asp:Button>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="btnViewDoc" type="button" class="btn btn-primary" Text="View Document" Height="36px" OnClick="btnViewDoc_Click"></asp:LinkButton>

        <br />
        <br />
        <h4>Previous Comments</h4>
        <br />
        <div>
            <asp:GridView ID="TableCMO" runat="server" CssClass="table table-bordered" DataKeyNames="Application_Number" GridLines="Vertical" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Application Name">
                        <ItemTemplate>
                            <asp:Label ID="Application_Number" runat="server" Text='<%# Bind("Application_Number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Comments">
                        <ItemTemplate>
                            <asp:Label ID="Comments" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="User_Name" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Created Date">
                        <ItemTemplate>
                            <asp:Label ID="Created_Date" runat="server" Text='<%# Bind("Created_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--<asp:Button OnClick="btn_Submit" Text="Submit" ID="SubmitButton" class="btn btn-primary" runat="server" />--%>
        </div>
 
    </form>

</body>
</html>
