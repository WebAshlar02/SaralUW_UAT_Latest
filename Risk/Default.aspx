<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" EnableEventValidation="false" Inherits="_Default" %>

<!DOCTYPE html>
<%--<%@ Register Src="~/User_Control/uc_Updateprogress.ascx" TagPrefix="uc1" TagName="uc_Updateprogress" %>--%>





<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<uc1:uc_Updateprogress runat="server" ID="uc_Updateprogress" />--%>
    <meta charset="utf-8" http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>SaralRisk</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
     <link href="dist/css/skins/skin-maroon.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="downloaded_refs/font-awesome-4.6.3/css/font-awesome.css" />

    <!-- Ionicons -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css"/>--%>
    <link rel="stylesheet" href="downloaded_refs/ionicons-2.0.1/css/ionicons.min.css" />
    <!-- DataTables -->
    <link rel="stylesheet" href="plugins/datatables/dataTables.bootstrap.css" />
    <!-- Theme style -->
    <%--<link rel="stylesheet" href="dist/css/AdminLTE.min.css" />--%>
    <link rel="stylesheet" href="dist/css/styles-app.css" />
    <link href="dist/css/AdminLTE.min.css" rel="stylesheet" />
   
    <!-- jQuery 2.2.3 -->
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
    <script type="text/javascript">


        $(window).load(function () {
            $('#ldrModal').fadeOut();
        });

        function SetTarget() {
            document.forms[0].target = "_blank";
        }

        /*added by shri on 13 june 17 to open on same page*/
        function SetTargetToSelf() {
            document.forms[0].target = "_self";
        }
        /*end here*/

        $(document).ready(function () {
            $("#lnkCommondashbord").click();
        })

        function fnHideShowButton(type) {
            $('#ModalCommon').html('');
            if (type == 'afi') {
                $('#btnAfiSubmit').show();
                $('#btnCfiSubmit').hide();
                $('#btnUWSubmit').hide();
                $('#ModalCommon').html('AFI');
            }
            else if (type == 'cfi') {
                $('#btnAfiSubmit').hide();
                $('#btnCfiSubmit').show();
                $('#btnUWSubmit').hide();
                $('#ModalCommon').html('CFI');
            }
            else if (type = 'uw') {
                $('#btnAfiSubmit').hide();
                $('#btnCfiSubmit').hide();
                $('#btnUWSubmit').show();
                $('#ModalCommon').html('UW');
            }
            //clear user control values
            $('#txtApplnNumber').val('');
            $('#txtPolicyNumber').val('');
            $('#txtComment').val('');
            $('#tblPolicyDetails').hide();
            $('.main').hide();
            $('.children').hide();
            $('#lblError').addClass('hide');
        }
    </script>

</head>
<body class="hold-transition skin-blue sidebar-mini">
    <form id="form1" runat="server">
        <%-- <div id="ldrModal" runat="server" class="LoaderModal">
            <div class="center">
                <img alt="" src="./dist/img/loader4.gif" />
            </div>
        </div>--%>
        <div class="wrapper">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <!-- Main Header -->
            <header class="main-header" style="background-color: #a81d22;">

                <a href="#" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                    <span class="logo-mini"><b>S</b>Risks</span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg"><b>Saral</b>RISK</span>


                </a>
                <nav class="navbar navbar-static-top" role="navigation">
                    <!-- Sidebar toggle button-->
                    <%-- <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">--%>
                    <%--   <span class="sr-only">Toggle navigation</span>
                    </a>--%>
                    <!-- Navbar Right Menu -->
                    <div class="navbar-custom-menu">
                        
                                <!-- Menu toggle button -->
                               <%-- <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-envelope-o"></i>
                                    <span class="label label-success">4</span>
                                </a>--%>
                                
                            <!-- /.messages-menu -->

                            <!-- Notifications Menu -->
                          
                    </div>
                </nav>
            </header>

            <!-- Content Wrapper. Contains page content -->
            <div class="">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Dashboard
                    <small>General</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active">General</li>
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="box box-solid">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="box-group" id="accordion">
                                <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                                <div class="panel box box-primary">
                                    <div class="box-header with-border">
                                        <h4 class="box-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Datafacts</a>
                                        </h4>
                                    </div>
                                    <div id="collapseOne" class="panel-collapse collapse in">
                                        <div class="box-body">
                                            <!-- Small boxes (Stat box) -->
                                            <div class="row" id="divDashbord" runat="server">
                                                <asp:Repeater runat="server" ID="RptDivDynamic">
                                                    <ItemTemplate>

                                                        <div class="col-lg-3 col-xs-6">
                                                            <!-- small box -->
                                                            <div class='<%# Eval("Css") %>'>
                                                                <div class="inner">
                                                                    <h3>
                                                                        <asp:Label ID="lblFreshcount" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                    </h3>
                                                                    <p>
                                                                        <%--<asp:LinkButton  Id="<%# Eval("Id") %>" OnClick="lnkFreshCase_Click" Text="<%# Eval("Text") %>" runat="server"></asp:LinkButton>--%>
                                                                        <asp:LinkButton ID="LinkButton1" Text='<%# Eval("Name") %>' OnClick="lnkFreshCase_Click" runat="server"></asp:LinkButton>
                                                                    </p>
                                                                </div>
                                                                <div class="icon">
                                                                    <i class="ion ion-person-add"></i>
                                                                </div>
                                                                <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                            </div>
                                                        </div>






                                                      
                                                    </ItemTemplate>
                                                </asp:Repeater>



                                                <%-- <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-aqua">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblFreshcount" runat="server" Text="Label"></asp:Label>
                                                            </h3>
                                                            <p>
                                                                <asp:LinkButton ID="lnkFreshCase"  OnClick="lnkFreshCase_Click" runat="server">Risk Upload</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-person-add"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>--%>
                                                <!-- ./col -->
                                                <%--      <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-fuchsia">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblUWRefercount" runat="server" Text="Label"></asp:Label></h3>
                                                            <p>
                                                                <asp:LinkButton ID="lnkUwrefer" OnClientClick="SetTargetToSelf();" OnClick="lnkUwrefer_Click" runat="server">Risk View</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-person-stalker"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>--%>
                                                <!-- ./col -->
                                                <%--    <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-fuchsia">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblPendingcount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkPendingcases" OnClientClick="SetTargetToSelf();" OnClick="lnkPendingcases_Click" runat="server">Risk Score Most</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-person-stalker"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>--%>
                                                <!-- ./col -->
                                        <%--       <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-purple">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblPendingVerificationcount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkPendingVerification" OnClientClick="SetTargetToSelf();"  OnClick="lnkPendingVerification_Click" runat="server">Risk Parameter</asp:LinkButton>

                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-ios-medkit"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>--%>
                                                <!-- ./col -->
                                                <%--    <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-orange">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblDocVerifiedcount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkDocVerified" OnClientClick="SetTargetToSelf();"  runat="server">Documents Verified</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-document-text"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>--%>
                                                <!-- ./col -->
                                                <%--    <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-yellow">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblDocRealizedCount" runat="server" Text="Label"></asp:Label></h3>
                                                            <p>
                                                                <asp:LinkButton ID="lnkDocRealized" OnClientClick="SetTargetToSelf();"  runat="server">Payment Realized</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-android-call"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>--%>
                                                <!-- ./col -->
                                                <%--     <div class="col-lg-3 col-xs-6">
                                                    <!-- small box -->
                                                    <div class="small-box bg-green">
                                                        <div class="inner">
                                                            <h3>
                                                                <asp:Label ID="lblReadytoissuecount" runat="server" Text="Label"></asp:Label></h3>

                                                            <p>
                                                                <asp:LinkButton ID="lnkReadyToIssue" OnClientClick="SetTargetToSelf();"  runat="server">Ready To Issue</asp:LinkButton>
                                                            </p>
                                                        </div>
                                                        <div class="icon">
                                                            <i class="ion ion-thumbsup"></i>
                                                        </div>
                                                        <a href="#" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
                                                    </div>
                                                </div>--%>
                                            </div>
                                            <!-- /.row -->
                                        </div>
                                    </div>
                                </div>
                                <div class="panel box box-success">
                                    <div class="box-header with-border">
                                        <h4 class="box-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Inbox</a>
                                        </h4>
                                    </div>
                                    <div id="collapseTwo" runat="server" class="panel-collapse ">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box-body">
                                                    <div class="box">
                                                        <div visible="false" id="dvSearch" runat="server" class="box box-warning box-solid">
                                                            <div class="box-header with-border">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-9">
                                                                        <h3 class="box-title ">Search Box</h3>
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
                                                                        <asp:Label runat="server" ID="lblUploadRiskParameter" CssClass="label-danger" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row"></div>
                                                                <div class="col-md-12">
                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">

                                                                        <ContentTemplate>
                                                                            <div class="col-md-6 ">
                                                                                Application Numbers<br />
                                                                                <asp:TextBox runat="server" ID="txtSearchApplicationNo" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-md-6 pull-right">
                                                                                </br>
                                                     <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn-primary" OnClick="btnSearch_Click" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="col-md-12 table-responsive ">
                                                                        <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                                            </Triggers>
                                                                            <ContentTemplate>
                                                                                <asp:DataGrid runat="server" ID="dgRiskApplication" CssClass="table " AutoGenerateColumns="false" HeaderStyle-CssClass="btn-primary ">
                                                                                    <Columns>
                                                                                        <asp:BoundColumn DataField="POLICYNO" HeaderText="Policy Number"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="UNDERWRITING_DUE_DILIGENCE_REQUIRED" HeaderText="Underwriting Due Diligence Required"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="PARAMETERS_COMBINATION" HeaderText="Parameters Combination"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="Risk_Score" HeaderText="Risk Score"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="ENY_ACTION" HeaderText="E&Y Action"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="ENY_SCORE" HeaderText="E&Y Score"></asp:BoundColumn>                                                                                        
                                                                                        <asp:BoundColumn DataField="SUGGESTIVE_REQUIREMENT" HeaderText="Suggestive Requirement"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="REMARKS" HeaderText="Remarks"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="APPNO" HeaderText="Application Number"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="SUMASSURED" HeaderText="Sum Assured"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="AGNTNUM" HeaderText="Agent Number"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="CHANNEL_CODE" HeaderText="Channel Code"></asp:BoundColumn>
                                                                                        <asp:BoundColumn DataField="CHANNEL_NAME" HeaderText="Channel Name"></asp:BoundColumn>
                                                                                    </Columns>
                                                                                </asp:DataGrid>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </div>
                                                                <div></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- /.box-body -->
                                                    <div class="box">
                                                        <div id="LoadingDtls_container" visible="false" runat="server" class="box box-warning box-solid">
                                                            <div class="box-header with-border">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-12>">
                                                                        <asp:Label runat="server" ID="lblUploadRiskDetailsErrorMessage" CssClass="label-danger" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="col-md-9">
                                                                        <h3 class="box-title ">Upload Risk Details</h3>
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
                                                                        <asp:Label runat="server" ID="Label1" CssClass="label-danger" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="row"></div>
                                                                <div class="col-md-12">
                                                                    <div class="col-md-6">                                                                                                                                   
                                                                        <label>Upload File:</label>
                                                                <asp:FileUpload runat="server" CssClass="form-control" ID="fuBulkRisk" />
                                                                    </div>
                                                                    <div class="col-md-3">                                                                        
                                                 <label>Risk Type:</label>
                                                                        <asp:DropDownList runat="server" ID="ddlRiskType" CssClass="form-control">
                                                                            <asp:ListItem Text="Risk Type" Value="-1"></asp:ListItem>
                                                                            <asp:ListItem Text="Risk Parameter & Score" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Risk E&N" Value="2"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="col-md-3 pull-right">
                                                                        </br>                                                                        
                                                 <asp:Button runat="server" ID="btnUploadFile" Text="Upload File" CssClass="btn-primary" OnClick="btnUploadFile_Click" />
                                                                    </div>
                                                                </div>
                                                                <div></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>






                                </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>


                </section>
            </div>
            <%--added by shri to show popup for AFI CFI and UW--%>
            <!-- Modal -->
            <%--<div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <b>
                                <h4 class="modal-title success"><span id="ModalCommon"></span></h4>
                            </b>
                        </div>
                        <div class="modal-body well">
                            <AfiCfiWU:PopupAfiCfiUW runat="server" ID="PopUpAfiCfiUW" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>--%>
            <!-- /.content -->
            <%-- <div class="modal fade" id="divPendingDocPopup" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title success"><b><span id="ModalCommon1">Pending Documents</span></b></h4>
                        </div>
                        <div class="">
                            <Docs:PendingDocs runat="server" ID="PendingDocs" />
                        </div>

                    </div>
                </div>
            </div>--%>
            <%--Bulk Decision popup--%>
            <%--<div class="modal fade" id="divBulkPopup" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header box-header with-border text-center bg-title">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title success"><b><span id="ModalCommon2">Decision</span></b></h4>
                        </div>
                        <div class="">
                            <Bulk:BulkDec runat="server" ID="BulkDecision" />
                        </div>

                    </div>
                </div>
            </div>--%>
            <div>
                <asp:HiddenField ID="hdnMenuoption" runat="server" />
                <asp:LinkButton ID="lnkCommondashbord" runat="server" Text="Getdashbord Details" CssClass="hidden">Getdashbord Details</asp:LinkButton>
            </div>
            <!-- /.content-wrapper -->

            <%-- <div class="modal fade modal-info" id="myModalNoDocPending" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Info Modal</h4>
                        </div>
                        <div class="modal-body">
                            <h4>
                                <asp:Label ID="Label2" runat="server" Text="No documents are pending for the selected case"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                            <%--<asp:Label ID="Label14" class="form-control lblLable" runat="server" Text="State Bank Of India"></asp:Label>--%>
            <%--</div>
                    </div>
                </div>
            </div>--%>


            <!-- Main Footer -->
            <footer class="main-footer">
                <!-- To the right -->
                <div class="pull-right hidden-xs">
                    Developed by FG-IT Life Team
                </div>
                <!-- Default to the left -->
                <strong>Copyright &copy; 2016 <a href="#">Future Generali India Life Insurance Company Limited</a>.</strong> All rights reserved.
            </footer>

            <!-- Control Sidebar -->
            <aside class="control-sidebar control-sidebar-dark">
                <!-- Create the tabs -->
                <ul class="nav nav-tabs nav-justified control-sidebar-tabs">
                    <li class="active"><a href="#control-sidebar-home-tab" data-toggle="tab"><i class="fa fa-home"></i></a></li>
                    <li><a href="#control-sidebar-settings-tab" data-toggle="tab"><i class="fa fa-gears"></i></a></li>
                </ul>
                <!-- Tab panes -->
                <div class="tab-content">
                    <!-- Home tab content -->
                    <div class="tab-pane active" id="control-sidebar-home-tab">
                        <h3 class="control-sidebar-heading">Recent Activity</h3>
                        <ul class="control-sidebar-menu">
                            <li>
                                <a href="javascript::;">
                                    <i class="menu-icon fa fa-birthday-cake bg-red"></i>

                                    <div class="menu-info">
                                        <h4 class="control-sidebar-subheading">Langdon's Birthday</h4>

                                        <p>Will be 23 on April 24th</p>
                                    </div>
                                </a>
                            </li>
                        </ul>
                        <!-- /.control-sidebar-menu -->

                        <h3 class="control-sidebar-heading">Tasks Progress</h3>
                        <ul class="control-sidebar-menu">
                            <li>
                                <a href="javascript::;">
                                    <h4 class="control-sidebar-subheading">Custom Template Design
                <span class="pull-right-container">
                    <span class="label label-danger pull-right">70%</span>
                </span>
                                    </h4>

                                    <div class="progress progress-xxs">
                                        <div class="progress-bar progress-bar-danger" style="width: 70%"></div>
                                    </div>
                                </a>
                            </li>
                        </ul>
                        <!-- /.control-sidebar-menu -->

                    </div>
                    <!-- /.tab-pane -->
                    <!-- Stats tab content -->
                    <div class="tab-pane" id="control-sidebar-stats-tab">Stats Tab Content</div>
                    <!-- /.tab-pane -->
                    <!-- Settings tab content -->
                    <div class="tab-pane" id="control-sidebar-settings-tab">
                        <form method="post">
                            <h3 class="control-sidebar-heading">General Settings</h3>

                            <div class="form-group">
                                <label class="control-sidebar-subheading">
                                    Report panel usage
                            <input type="checkbox" class="pull-right" checked="checked" />
                                </label>

                                <p>
                                    Some information about this general settings option
                                </p>
                            </div>
                            <!-- /.form-group -->
                        </form>
                    </div>
                    <asp:HiddenField ID="hdnUWoption" runat="server" />
                    <!-- /.tab-pane -->
                </div>
            </aside>
            <!-- /.control-sidebar -->
            <!-- Add the sidebar's background. This div must be placed
       immediately after the control sidebar -->
            <div class="control-sidebar-bg"></div>
        </div>
        <!-- ./wrapper -->

        <!-- REQUIRED JS SCRIPTS -->

        <div class="modal fade modal-info" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">Info Modal</h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            <asp:Label ID="lblErrorinfo" runat="server" Text="The case is already locked by other user"></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
                        <%--<asp:Label ID="Label14" class="form-control lblLable" runat="server" Text="State Bank Of India"></asp:Label>--%>
                    </div>
                </div>
            </div>
        </div>


        <!-- Bootstrap 3.3.6 -->
        <script src="bootstrap/js/bootstrap.min.js"></script>
        <!-- AdminLTE App -->
        <script src="dist/js/app.min.js"></script>

        <!-- DataTables -->
        <script src="plugins/datatables/jquery.dataTables.min.js"></script>
        <script src="plugins/datatables/dataTables.bootstrap.min.js"></script>
        <!-- SlimScroll -->
        <script src="plugins/slimScroll/jquery.slimscroll.min.js"></script>
        <!-- FastClick -->
        <script src="plugins/fastclick/fastclick.js"></script>

        <!-- AdminLTE for demo purposes -->
        <script src="dist/js/demo.js"></script>
        <!-- page script -->
        <script>
            $(function () {
                $("#example1").DataTable();

                /*$('#example2').DataTable({
                    "paging": true,
                    "lengthChange": false,
                    "searching": false,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });*/
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {

                $("#example1").DataTable();
            });
        </script>
    </form>
</body>
</html>
