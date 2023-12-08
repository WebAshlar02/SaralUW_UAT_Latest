<%@ Page Language="C#" AutoEventWireup="true" CodeFile="REQ_SearchCode.aspx.cs" Inherits="Appcode_REQ_SearchCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="../dist/js/html5shiv.min.js"></script>
    <script src="../dist/js/respond.min.js"></script>
    <link href="../dist/css/styles-app.css" rel="stylesheet" />
    <link href="../plugins/select2/select2.min.css" rel="stylesheet" />
    <script src="../plugins/jQueryUI/jquery-ui.js"></script>
    <link href="../plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../plugins/datepicker/bootstrap-datepicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="divLoadingDetails" runat="server" class="col-md-12">
                <div id="LoadingDtls_container" class="box box-warning box-solid">
                    <div class="box-header with-border">
                        <div class="col-md-12">
                            <div class="col-md-9">
                                <h3 class="box-title ">Requirement Details</h3>
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
                                <asp:Label runat="server" ID="lblMsg" CssClass="label-info" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row"></div>
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <asp:TextBox ID="txtSearch" runat="server" MaxLength="100" class="form-control"></asp:TextBox>
                                <div class="row">
                                    <br />
                                </div>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn-primary" />
                            </div>
                            <div class="col-md-12">
                                <%--    <div class="col-md-2" runat="server" id="divStatusDesc" visible="false">
                                    <b>Requirement Code and Description:
                                    </b>
                                </div>--%>
                                <div class="row">
                                    <br />
                                </div>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvSearch" runat="server" CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
