<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkManualAssignment.aspx.cs" Inherits="Appcode_BulkManualAssignment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="../dist/js/html5shiv.min.js"></script>
    <script src="../dist/js/respond.min.js"></script>
    <link href="../dist/css/styles-app.css" rel="stylesheet" />
    <link href="../plugins/select2/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />

    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script src="../dist/js/UWSaral.js"></script>
    <script src="../dist/js/CommonValidation.js"></script>
    <script>
        function fnValidate()
        {
            debugger;
            if ($('#<%=txtApplicationNo.ClientID%>').val() == '')
            {
                $('#<%=lblBulkManualAllocationMsg.ClientID%>').html('Enter Application Number');
                $('#<%=txtApplicationNo.ClientID%>').focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divLoadingDetails" runat="server" class="col-md-12">


                <div id="LoadingDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Bulk Manual Allocation</h3>
                                <i class="fa fa-hourglass-start"></i>
                            </div>
                        </div>
                        <div class="box-tools ">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                <i class="fa fa-minus"></i>
                            </button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="col-md-12">
                            <div class="col-md-12>">
                                <asp:Label runat="server" ID="lblBulkManualAllocationMsg" CssClass="label-danger" Text="" ></asp:Label>
                            </div>
                        </div>
                        <div class="row"></div>
                        <div class="col-md-12">
                            <div class="col-md-12">
                                Channel:                                                                                                            
                                <asp:RadioButtonList runat="server" ID="rblChannel" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblChannel_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Offline " Value="OFFLINE"></asp:ListItem>
                                    <asp:ListItem Text="Online " Value="ONLINE"></asp:ListItem>
                                </asp:RadioButtonList>                                
                            </div>                                 
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-6">
                                Application Number :                                                                            
                                <asp:TextBox rows="4" Columns="100" runat="server" ID="txtApplicationNo" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>                                
                            </div>                                 
                            <div class="col-md-6">
                                Users List :                                                                            
                                <asp:DropDownList runat="server" ID="ddlUserName" CssClass="form-control"></asp:DropDownList>
                            </div>                                 
                        </div>
                           <div class="col-md-2 pull-right">
                                <asp:Button runat="server" ID="btnBulkManualAllocate" Text="Manual Allocate" OnClientClick="return fnValidate();" OnClick="btnBulkManualAllocate_Click" CssClass="btn-primary" />
                            </div>
                        <div></div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
