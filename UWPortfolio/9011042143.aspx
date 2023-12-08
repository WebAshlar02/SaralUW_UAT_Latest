<%@ Page Language="C#" AutoEventWireup="true" CodeFile="9011042143.aspx.cs" Inherits="Entry" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-lg-6 col-lg-offset-3" style="padding-top: 150px">
            <div class="table-responsive">
                <table class="table table-bordered">
                    <tr class="success">
                        <td colspan="2">
                            <h4><asp:Label ID="iblcaption" Text="User Info" runat="server"></asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Please Enter UserID</b></td>
                        <td>
                            <asp:TextBox CssClass="form-control" ID="txtUserid" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSubmit" CssClass="form-control btn btn-success" Text="Submit" runat="server" OnClick="btnSubmit_Click" />
                        </td>
                        <td>
                            <asp:Label ID="lblError" CssClass="label-danger" runat="server"></asp:Label>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td colspan="2"><h4>Please use 1119763 for UW User and 1113322 for Docqc user.</h4>
                        </td>
                    </tr>--%>
                </table>
            </div>
        </div>
        <div>
             <asp:Label runat="server" ID="lblExamError" Text=""></asp:Label> 
        </div>
    </form>
</body>
</html>
