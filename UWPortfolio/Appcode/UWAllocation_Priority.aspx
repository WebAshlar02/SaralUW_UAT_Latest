<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UWAllocation_Priority.aspx.cs" Inherits="Appcode_UWAllocation_Priority" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />--%>
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="../dist/js/html5shiv.min.js"></script>
    <script src="../dist/js/respond.min.js"></script>
    <link href="../dist/css/styles-app.css" rel="stylesheet" />
    <link href="../plugins/select2/select2.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />--%>    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>--%>
    <script src="../plugins/jQueryUI/jquery-ui.js"></script>
    <link href="../dist/css/sumoselect.min.css" rel="stylesheet" />
    <script src="../dist/js/jquery.sumoselect.min.js"></script>


    <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>--%><%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"

        rel="Stylesheet" type="text/css" />--%>
    <link href="../plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../plugins/datepicker/bootstrap-datepicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="col-md-12">
            <div class="col-md-3">
                <div class="form-group">
                    <h4><b>Priority</b></h4>
                    <asp:DropDownList ID="ddlPriority" CssClass="form-control select2 " Style="width: 100%;" runat="server" AutoPostBack="True" onchange="fnLoaderShow(); ">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvPriority" runat="server" ErrorMessage="* Please Select Priority Value ...!" ForeColor="Red" ControlToValidate="ddlPriority"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-md-3">
                <div class="form-group">
                    <div id="divsave" runat="server" visible="true">
                        <asp:Button ID="btnsave" Style="float: right; margin-right: 10px; margin-bottom: 7px;" CssClass="btn btn-success lnkButton" runat="server" Text="  SAVE  " Enabled="true" AutoPostBack="True"  OnClick="btnsave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
