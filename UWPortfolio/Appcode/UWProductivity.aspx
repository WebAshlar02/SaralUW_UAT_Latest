<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UWProductivity.aspx.cs" Inherits="Appcode_UWProductivity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <%--<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css" />--%>

    <%--<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>--%>
    <script src="../plugins/jQueryUI/jquery-ui.js"></script>
    <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>--%>
    <%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />--%>
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
                                <h3 class="box-title ">UW Productivity</h3>
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
                                <asp:Label runat="server" ID="lblUWProductivity" CssClass="label-danger" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="row"></div>
                        <div class="col-md-12">
                            <div class="col-md-4">
                               Report Type :                                                                            
                               <br /><asp:DropDownList ID="ddlrpttype"  runat="server" Height="30px" Width="200px">
                               </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                From Date :                                                                            
                                <asp:TextBox runat="server" ID="txtFromDate" CssClass="form-control DatePicker"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                To Date :
                                <asp:TextBox runat="server" ID="txtToDate" CssClass="form-control DatePicker"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                User Name :
                                <asp:DropDownList runat="server" ID="ddlProductivityUser" CssClass="form-control "></asp:DropDownList>
                            </div>
                            <div class="col-md-2 pull-right">
                                <br/>
                            <asp:Button runat="server" ID="btnFetchRecord" Text="Fetch Record" OnClick="btnFetchRecord_Click" CssClass="btn-primary" />
                        </div>
                            <div class="col-md-2 pull-right">
                                <br/>
                            <asp:Button runat="server" ID="btnExportToCsv" Text="Export" OnClick="btnExportToCsv_Click" CssClass="btn-primary" />
                        </div>
                        </div>                        
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <h6><b>Note:</b> If date(from/to) is empty then default date todays date</h6>
                            </div>
                        </div>
                        <div class="row"> <br/> <br/> <br/> </div>
                        <div class="col-md-12">
                            <div class="table-responsive" style="overflow: auto; width:100%;height: 500px" >
<%--                                <asp:DataGrid runat="server" ID="dgUWProductivity" CssClass="table " AutoGenerateColumns="false" HeaderStyle-CssClass="btn-primary ">
                                    <Columns>
                                        <asp:BoundColumn DataField="APPLICATION_NUMBER" HeaderText="Application Number"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RCVD_DATE" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Received Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="USERID" HeaderText="User Id"></asp:BoundColumn>     
                                        <asp:BoundColumn DataField="USER_NAME" HeaderText="User Name"></asp:BoundColumn>                                   
                                        <asp:BoundColumn DataField="BUCKET" HeaderText="Bucket"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="EVENT_NAME" HeaderText="Event Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="LA_STATUS" HeaderText="LA Status"></asp:BoundColumn>                                        
                                    </Columns>
                                </asp:DataGrid>--%>
                                <asp:DataGrid ID="dgUWProductivity" CssClass="table " AutoGenerateColumns="True" HeaderStyle-CssClass="btn-primary" runat="server"> 
                                    <Columns>

                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function () {
        $('.DatePicker').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            onSelect: function (date) {
                alert('tail');
            }
        });
    })
</script>
