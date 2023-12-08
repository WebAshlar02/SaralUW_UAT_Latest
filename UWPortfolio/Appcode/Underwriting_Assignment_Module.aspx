<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Underwriting_Assignment_Module.aspx.cs" Inherits="Appcode_Underwriting_Assignment_Module" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        .btn{
           width :100px;
           
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
        $(document).ready(function () {
            $('#chkuserid').SumoSelect(
            );
        });



        function ValidateAllocationParameter(source, args) {

            var chkListModules = document.getElementById('<%= chkallocation.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function ValidateDecisionRights(source, args) {

            var chkListModules = document.getElementById('<%= chkdecision.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function Validatecategory(source, args) {

            var chkListModules = document.getElementById('<%= chkcategory.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            for (var i = 0; i < chkListinputs.length; i++) {
                if (chkListinputs[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        function showmodal() {
            debugger
            var modal = $find("mp1");
            modal.show();
            // $("#mp1").show();
            return true;
        }
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
        <div id="divAssignment" class="col-md-12 " runat="server">
            <div id="Assignment_container" class="box box-warning box-solid">
                <div class="box-header with-border">
                    <div class="col-md-12">
                        <div class="col-md-10">

                            <h2>Underwriting Assignment Details(User Access)</h2>
                            </div>
                            <div class="col-md-2" style="margin-top:20px;">
                                <h4><asp:Label ID="lblusername" runat="server" Text="" ></asp:Label></h4>
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
                                <h4><b>UW Bucket</b></h4>
                                <asp:DropDownList ID="ddlbucket" CssClass="form-control select2 " Style="width: 75%;" runat="server" AutoPostBack="True" onchange="fnLoaderShow(); ">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvbucket" runat="server" ErrorMessage="* Please Select Bucket Value ...!" ForeColor="Red" ControlToValidate="ddlbucket"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>UserName / UserID</b></h4>
                                <div>
                                    <asp:ListBox ID="chkuserid" CssClass="form-control " Style="width: 85%;" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvuserid" runat="server" ErrorMessage="* Please Select User....!" ForeColor="Red" ControlToValidate="chkuserid"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3" runat="server" visible="false">
                            <div class="form-group">
                                <h4><b>Limit</b></h4>
                                <asp:DropDownList ID="ddllimit" CssClass="form-control select2 " Style="width: 75%;" AutoPostBack="True" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvlimit" runat="server" ErrorMessage="* Please Select Limit....!" ForeColor="Red" ControlToValidate="ddllimit"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>Allocation Limit</b></h4>
                                <asp:TextBox ID="txtallocationlimit" CssClass="form-control " Style="width: 60%;" runat="server" TextMode="Number"></asp:TextBox>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfvallocationlimit" runat="server" ErrorMessage="* Please Select Allocation Limit....!" ForeColor="Red" ControlToValidate="txtallocationlimit"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtallocationlimit"
                                        ErrorMessage="Enter value in specified range Between 01 To 999" ForeColor="Red" MaximumValue="999" MinimumValue="1"
                                        SetFocusOnError="True" Type=" Integer"></asp:RangeValidator>
                                </div>
                                <%-- <div>
                                    <asp:RegularExpressionValidator ID="rfvnumeric" runat="server" ControlToValidate="txtallocationlimit" ErrorMessage="Please Select Only Numeric Value...!" ForeColor="Red" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>Decision Rights</b></h4>
                                <asp:CheckBoxList ID="chkdecision" runat="server">
                                </asp:CheckBoxList>

                                <asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="ValidateDecisionRights" ForeColor="Red" ErrorMessage="* Please Select Decision....!"></asp:CustomValidator>
                                <%--<asp:RequiredFieldValidator ID="rfvdecision" runat="server" ErrorMessage="* Please Select Decision Rights....!" ForeColor="Red" ControlToValidate="chkdecision"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>Risk Category Allowed</b></h4>
                                <asp:CheckBoxList ID="chkcategory" runat="server">
                                </asp:CheckBoxList>
                                <asp:CustomValidator runat="server" ID="CustomValidator2" ClientValidationFunction="Validatecategory" ForeColor="Red" ErrorMessage="* Please Select Category....!"></asp:CustomValidator>
                                <%--<asp:RequiredFieldValidator ID="rfvcategory" runat="server" ErrorMessage="* Please Select Decision Rights....!" ForeColor="Red" ControlToValidate="chkcategory"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h4><b>Allocation Parameters</b></h4>
                                <asp:CheckBoxList ID="chkallocation" runat="server">
                                </asp:CheckBoxList>
                                <asp:CustomValidator runat="server" ID="cvallocation" ClientValidationFunction="ValidateAllocationParameter" ForeColor="Red" ErrorMessage="* Please Select Allocation Parameters....!"></asp:CustomValidator>
                                <%--<asp:RequiredFieldValidator ID="rfvallocation" runat="server" ErrorMessage="* Please Select Allocation Parameters....!" ForeColor="Red" ControlToValidate="chkallocation"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <div style="display:flex;flex-direction:column; align-items:center">
                                <div id="divsave" runat="server" visible="true">
                                    <asp:Button ID="btnsave" Style="float: right; margin-right: 10px; margin-bottom:7px; font-size: 14px; padding:5px; border-radius:8px;"  CssClass="btn btn-success lnkButton " runat="server" Text="  SAVE  " Enabled="true" AutoPostBack="True" OnClick="btnsave_Click" />
                                </div>
                                <div id="divupdate" runat="server" visible="false">
                                    <asp:Button ID="btnupdate" Style="float: right; margin-right: 10px;margin-bottom:7px; font-size: 14px; padding:5px; border-radius:8px;" CssClass="btn btn-info lnkButton " runat="server" Text="UPDATE" Enabled="true" AutoPostBack="True" OnClick="btnupdate_Click" />
                                </div>
                                <div id="divcleare" runat="server" visible="true">
                                    <asp:Button ID="btncleare" Style="float: right; margin-right: 10px;margin-bottom:7px; font-size: 14px; padding:5px; border-radius:8px;" CssClass="btn btn-danger lnkButton " runat="server" Text="CLEAR" Enabled="true" AutoPostBack="True" OnClick="btncleare_Click" CausesValidation="False" UseSubmitBehavior="False" />
                                </div>
                                    <div id="divPriority" runat="server" visible="true">
                                    <asp:Button ID="btnPriority" Style="float: right; margin-right: 10px;margin-bottom:7px; font-size: 14px; padding:5px; border-radius:8px;" CssClass="btn btn-warning lnkButton " runat="server" Text="PRIORITY" Enabled="true" AutoPostBack="True" OnClick="btnPriority_Click"  CausesValidation="False" UseSubmitBehavior="False" />
                                </div>

                                     <%--<div id="divMortality" runat="server" visible="false">
                                    <asp:Button ID="btnMortality" Style="float: right; margin-right: 10px;margin-bottom:7px;" CssClass="btn btn-danger lnkButton" runat="server" Text="MORTALITY" Enabled="true" AutoPostBack="True" OnClick="btnMortality_Click"  CausesValidation="False" UseSubmitBehavior="False" />
                                </div>--%>

                                <div id="divAddNewGroup" runat="server" visible="false">
                                    <asp:Button ID="btnaddnew" runat="server" Style="float: right; margin-right: 12px; font-size: 14px; padding:5px; border-radius:8px;" CssClass="btn btn-primary lnkButton "  OnClick="btnaddnew_Click" Text="ADD GROUP"  Enabled="true" CausesValidation="False" UseSubmitBehavior="False"  />
                                   <%-- <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnaddnew"
                                        CancelControlID="btnclose" BackgroundCssClass="Background">
                                    </cc1:ModalPopupExtender>
                                    <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none">
                                        <iframe style="width: 430px; height: 480px; border: 0px solid;" id="irm1" src="UWAssignment_AddNewGP.aspx" runat="server"></iframe>
                                        <br />
                                        <asp:Button ID="btnclose" runat="server" CssClass="btn btn-danger lnkButton" Text="Close" CausesValidation="False" UseSubmitBehavior="False" />
                                    </asp:Panel>--%>
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
                            <asp:Label ID="lblcheckboxlistvalue4" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblcheckboxlistvalue5" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <div class="col-md-12">
            <div class="panel box box-danger" runat="server" id="div_Assignment" visible="true">
                <div id="collapseThree">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box-body">
                                <asp:HiddenField ID="hfSelectedRecord" runat="server" />
                                <asp:Repeater ID="rp_Assignment" runat="server" OnItemCommand="rp_Assignment_ItemCommand">
                                    <HeaderTemplate>
                                        <table id="example2" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th>ID</th>
                                                    <th>UW Bucket</th>
                                                    <th>UserID</th>
                                                    <th>Limit</th>
                                                    <th>Allocation Parameters</th>
                                                    <th>Decision Rights</th>
                                                    <th>Allocation Risk Category</th>
                                                    <th>Allocation Limit</th>
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
                                                <asp:Literal ID="lbluwbucket" Text='<%# Eval("UW_Bucket")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblUserName" Text='<%# Eval("UserName")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblLimit" Text='<%# Eval("Limit")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblAllocationParameters" Text='<%# Eval("Allocation_Parameters")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblDecisionRights" Text='<%# Eval("Decision_Rights")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblrickcategory" Text='<%# Eval("Risk_Category")%>' runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:Literal ID="lblAllocationLimit" Text='<%# Eval("Allocation_Limit")%>' runat="server"></asp:Literal>
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
                                                <th>UW Bucket</th>
                                                <th>UserID</th>
                                                <th>Limit</th>
                                                <th>Allocation Parameters</th>
                                                <th>Decision Rights</th>
                                                <th>Allocation Risk Category</th>
                                                <th>Allocation Limit</th>
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
