<%--//**********************************************************************
// Sr. No.              : 1
// Company              : Life            
// Module               : UW Saral         
// Program Author       : Bhaumik Patel - WebAshlar02
// BRD/CR/Codesk No/Win : CR-5307 
// Date Of Creation     : 18/11/2022
// Description          :UnderWriting Assignment Details (User Access)
//**********************************************************************--%>

    <%--//1.1 Begin of Changes; Bhaumik Patel - [CR-5307]--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UWDashboardMedicalManagement.aspx.cs" Inherits="Appcode_UWDashboardMedicalManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UW Saral Medical Management Dashboard</title>
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <link href="../plugins/jQuery/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link href="../dist/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../dist/css/skins/_all-skins.min.css" rel="stylesheet" />
    <script src="../dist/js/html5shiv.min.js"></script>
    <script src="../dist/js/respond.min.js"></script>
    <link href="../dist/css/styles-app.css" rel="stylesheet" />
    <link href="../plugins/select2/select2.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />--%>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../grpUW/Styles/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <%--<script src="jquery-ui/ui/i18n/datepicker-de.js"></script>--%>

    <script src="../plugins/jQuery/jquery-ui.js"></script>
    <script src="../plugins/jQuery/datepicker.js"></script>
    <script>
        function fnClear() {
            $('#<%=txtApplicationNumber.ClientID%>').val('');
            $('#<%=txtPolicyNumber.ClientID%>').val('');
            $('#<%=txtStartDate.ClientID%>').val('');
            $('#<%=txtEndDate.ClientID%>').val('');
            $('#<%=ddlCategory.ClientID%>').val('ALL_DATA');
            $('#<%=cbIsOnline.ClientID%>').prop('checked', false);
        }

        function fnDownloadSuccess() {
            alert('File Donwloaded Successfully');
        }
    </script>
    <style type="text/css">
        .ScrollBar {
            height: 550px;
            overflow-y: scroll;
        }

        /*table {
            width: 100%;
        }*/

        /*thead, tbody, tr, td, th {
            display: block;
        }*/

        /*tr:after {
                content: ' ';
                display: block;
                visibility: hidden;
                clear: both;
            }*/

        /*thead th {
                height: 30px;                
            }*/
    </style>
</head>
<body>
    <form id="frmUWDashboardMedicalManagement" runat="server">
        <div id="Appdtls_container" class="box box-warning box-solid ">
            <div class="box-header with-border">
                <div class="col-md-12">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <h3 class="box-title ">UW Saral Medical Management Dashboard</h3>
                            <i class="fa fa-pencil fa-fw" aria-hidden="true"></i>
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                </div>
                <div class="box-tools ">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                </div>
            </div>
            <div id="Appdtls_containerBody" class="box-body">
                <div class="col-md-12 error-container-div">
                    <div class="col-md-12 ">
                        <div class="form-group">
                            <asp:Label ID="lblErrorUWDashboardMedicalManagement" runat="server" Font-Bold="True" ForeColor="Red" Text="test"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <span class="small ">Last Successfully Registered On </span>
                            <asp:Label runat="server" ID="lblRegistered" CssClass="small text-bold">test</asp:Label>
                        </div>
                        <div class="col-md-6">
                            <span class="small ">Last Successfully File Transfered On </span>
                            <asp:Label runat="server" ID="lblTransfer" CssClass="small text-bold">test</asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Application Number</label>
                                <asp:TextBox ID="txtApplicationNumber" CssClass="form-control" Text="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Policy Number</label>
                                <asp:TextBox ID="txtPolicyNumber" CssClass="form-control " Text="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Start Date</label>
                                <asp:TextBox ID="txtStartDate" CssClass="form-control DatePicker" Text="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>End Date</label>
                                <asp:TextBox ID="txtEndDate" CssClass="form-control DatePicker" Text="" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Category</label>
                                <div class="form-group">
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCategory"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Is Online?</label>
                                <div class="form-group">
                                    <asp:CheckBox ID="cbIsOnline" CssClass="form-control " runat="server" Text="Is Online?"></asp:CheckBox><%--onchange="fnChaneNsap();" --%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">                        
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Partner Type</label>
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlPartnerType"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <br />
                                <asp:Button runat="server" CssClass="btn btn-primary pull-right btn-block" ID="btnExportToCSV" Text="Export" OnClick="btnExportToCSV_Click" />
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <br />
                                <asp:Button runat="server" CssClass="btn btn-block btn-primary " ID="btnSearchUWDashboardMedicalManagement" Text="Search" OnClick="btnSearchUWDashboardMedicalManagement_Click" />
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <br />
                                <asp:Button runat="server" CssClass="btn btn-block btn-secondary" ID="btnClear" Text="Clear" OnClientClick="fnClear();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="Appdtls_MedicalGrid" class="box-body">
                <div class="col-md-12">
                    <div id="divMedicalGrid" runat="server" class="col-md-6 table-responsive ">
                        <asp:DataGrid runat="server" ID="dgMedicalGrid" OnItemCommand="dgMedicalGrid_ItemCommand" HeaderStyle-CssClass="btn-primary " OnItemDataBound="dgMedicalGrid_ItemDataBound" CssClass=" table table-bordered" AutoGenerateColumns="true">
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        Appliction No
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkAppicationNo" Text='<%# Bind("AppNo")%>' CommandName="TPA"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        View File
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lnkViewFile" Text="View File" CommandName="VIEWFILE"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <div class="col-md-6 table-responsive">
                        <asp:DataGrid runat="server" ID="dgTpaGrid" HeaderStyle-CssClass="btn-primary " CssClass=" table table-bordered" AutoGenerateColumns="true">
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $('.DatePicker').datepicker({
                autoclose: true,
                format: 'dd-mm-yyyy',
                onSelect: function (date) {
                }
            });
        })
    </script>
</body>
</html>

 <%--//1.1 End of Changes; Bhaumik Patel - [CR-5307]--%>
