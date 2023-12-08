<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAccessFlatMortality.aspx.cs" Inherits="Appcode_UserAccessFlatMortality" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 450px;
            height: 540px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }

    </style>
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

    <script type="text/javascript">

        function SetTarget() {
            debugger
            document.forms[0].target = "_blank";
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="divMortality" class="col-md-12 " runat="server">
            <div id="Mortality_container" class="box box-warning box-solid">
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-10">
                            <h2>Flat Mortality Details</h2>
                        </div>
                        <div class="col-md-2" style="margin-top: 20px;">
                            <h4>
                                <asp:Label ID="lblusername" runat="server" Text=""></asp:Label></h4>
                            <i class="fa fa-plus-square"></i>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <div id="divid" runat="server" visible="false">
                                <div class="form-group">
                                    <h4><b>ID</b></h4>
                                    <asp:TextBox ID="txtid" CssClass="form-control " Style="width: 20%;" runat="server" ReadOnly="true">
                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>UserName / UserID</b></h4>
                                <asp:DropDownList ID="ddluserid" CssClass="form-control select2 " Style="width: 100%;margin-top:36px;"  runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvuserid" runat="server" ErrorMessage="* Please Select User....!" ForeColor="Red" ControlToValidate="ddluserid"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>Mortality<h5>(Medical)</h5></b></h4>
                                <asp:DropDownList ID="ddlMortality" CssClass="form-control select2 " Style="width: 100%;" runat="server"  onchange="fnLoaderShow(); ">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvMortality" runat="server" ErrorMessage="* Please Select Mortality Value ...!" ForeColor="Red" ControlToValidate="ddlMortality"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>Flat Extra<h5>(Non-Medical/Occupation/Resident/etc)</h5></b></h4>
                                <asp:DropDownList ID="ddllimit" CssClass="form-control select2 " Style="width: 100%;" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvlimit" runat="server" ErrorMessage="* Please Select Limit....!" ForeColor="Red" ControlToValidate="ddllimit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div style="display: flex; flex-direction: column; align-items: center">
                                    <div id="divsave" runat="server" visible="true">
                                        <asp:Button ID="Btnsave" Style="float: right; margin-right: 10px; margin-bottom: 7px;" CssClass="btn btn-success lnkButton" runat="server" Text="  SAVE  " OnClick="Btnsave_Click" Enabled="true" AutoPostBack="True" />
                                    </div>
                                    <div id="divupdate" runat="server" visible="true">
                                        <asp:Button ID="btnupdate" Style="float: right; margin-right: 10px; margin-bottom: 7px;" CssClass="btn btn-info lnkButton" runat="server" Text="UPDATE" OnClick="btnupdate_Click" Enabled="true" AutoPostBack="True" />
                                    </div>
                                    <div id="divclear" runat="server" visible="true">
                                        <asp:Button ID="btnclear" Style="float: right; margin-right: 10px; margin-bottom: 7px;" CssClass="btn btn-danger lnkButton" runat="server" Text="CLEAR" Enabled="true" OnClick="btnclear_Click" AutoPostBack="True" CausesValidation="False" UseSubmitBehavior="False" />
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div id="divlbl" runat="server" visible="true" class="col-md-3">
                        <div class="form-group">
                            <asp:Label ID="lblcheckboxlistvalue1" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblcheckboxlistvalue2" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblcheckboxlistvalue3" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="col-md-12">
            <div class="panel box box-danger" runat="server" id="div_Mortality" visible="true">
                <div id="collapseThree">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box-body">
                                <asp:HiddenField ID="hfSelectedRecord" runat="server" />
                                <asp:Repeater ID="rp_Mortality" runat="server" OnItemCommand="rp_Mortality_ItemCommand">
                                    <HeaderTemplate>
                                        <table id="example2" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>UserName</th>
                                                    <th>Mortality</th>
                                                    <th>Flat_Extra</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="lblid" Text='<%# Eval("ID")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lbluwbucket" Text='<%# Eval("UserName")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblUserName" Text='<%# Eval("Mortality")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblLimit" Text='<%# Eval("Flat_Extra")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkadit" runat="server" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' CausesValidation="False" UseSubmitBehavior="False">EDIT</asp:LinkButton>
                                                <span>/</span>
                                                <asp:LinkButton ID="lnkdelete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this?')" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' CausesValidation="False" UseSubmitBehavior="False">DELETE</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th>ID</th>
                                                <th>UserName</th>
                                                <th>Mortality</th>
                                                <th>Flat_Extra</th>
                                                <th>Action</th>
                                            </tr>
                                        </tfoot>
                                        </table>                                                  
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
