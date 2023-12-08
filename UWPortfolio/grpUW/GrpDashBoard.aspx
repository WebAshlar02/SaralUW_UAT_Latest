<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GrpDashBoard.aspx.cs" Inherits="grpUW_GrpDashBoard" EnableEventValidation="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<uc1:uc_Updateprogress runat="server" ID="uc_Updateprogress" />--%>
    <meta charset="utf-8" http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>SaralUW</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
   <link rel="stylesheet" href="../bootstrap/css/bootstrap.min.css" />

    <!-- Font Awesome -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css"/>--%>
    <link rel="stylesheet" href="../downloaded_refs/font-awesome-4.6.3/css/font-awesome.css" />

    <!-- Ionicons -->
    <%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css"/>--%>
    <link rel="stylesheet" href="../downloaded_refs/ionicons-2.0.1/css/ionicons.min.css" />
    <!-- DataTables -->
    <link rel="stylesheet" href="../plugins/datatables/dataTables.bootstrap.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="../dist/css/styles-app.css" />
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
        page. However, you can choose any other skin. Make sure you
        apply the skin class to the body tag so the changes take effect.
  -->
    <link rel="stylesheet" href="../dist/css/skins/skin-maroon.min.css" />
    <!-- jQuery 2.2.3 -->
    <script src="../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <%--<script src="Scripts/jquery-3.2.1.min.js"></script>--%>
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <script src="../bootstrap/js/bootstrap.min.js"></script>
     <!-- DataTables -->
    <link href="Styles/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="Scripts/jquery.dataTables.min.js"></script>
   
    <script type="text/javascript">
        function hideloading() {
            $("#loaderdiv").hide();
        }

        function showloading() {
            $("#loaderdiv").show();
        }

        $(window).load(function () {

            $('#loaderdiv').fadeOut();
        });

        //function LoaderPop()
        //{
        //    alert('Hi');
        //    $('#ldrModal').show();
        //}        
        function SetTarget() {
            document.forms[0].target = "_blank";
        }

        /*added by shri on 13 june 17 to open on same page*/
        function SetTargetToSelf() {
            document.forms[0].target = "_self";
        }

        /*added by shri on 30 oct 17 to open on policy enquiry*/
        /*function fnPolicyEnquiery() {
            var enq = 
            window.open(enq, '_blank')
        }*/
        /*end here*/

        //$(document).ready(function () {
        //    //    $('#example1').DataTable({
        //    //        "order": [[9, "desc"]]
        //    //    });

        //    var prm = Sys.WebForms.PageRequestManager.getInstance();
        //    prm.add_endRequest(function () {

        //        if (!$.fn.DataTable.isDataTable('#example1')) {
        //            $('#example1').dataTable({
        //                "order": [[9, "desc"]]
        //            });
        //        }
        //        $('.lnkButton').click(function () {
        //            $('#loaderdiv').fadeIn();
        //        });
        //    });
        //    //$("#lnkCommondashbord").click();
        //    $('.lnkButton').click(function () {
        //        $('#loaderdiv').fadeIn();
        //    });

        //});

        //function fnHideShowButton(type) {
        //    $('#ModalCommon').html('');
        //    if (type == 'afi') {
        //        $('#btnAfiSubmit').show();
        //        $('#btnCfiSubmit').hide();
        //        $('#btnUWSubmit').hide();
        //        $('#ModalCommon').html('AFI');
        //    }
        //    else if (type == 'cfi') {
        //        $('#btnAfiSubmit').hide();
        //        $('#btnCfiSubmit').show();
        //        $('#btnUWSubmit').hide();
        //        $('#ModalCommon').html('CFI');
        //    }
        //    else if (type = 'uw') {
        //        $('#btnAfiSubmit').hide();
        //        $('#btnCfiSubmit').hide();
        //        $('#btnUWSubmit').show();
        //        $('#ModalCommon').html('UW');
        //    }
        //    //clear user control values
        //    $('#txtApplnNumber').val('');
        //    $('#txtPolicyNumber').val('');
        //    $('#txtComment').val('');
        //    $('#tblPolicyDetails').hide();
        //    $('.main').hide();
        //    $('.children').hide();
        //    $('#lblError').addClass('hide');
        //}
    </script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            //for displying datafacts aqua fuchsia red purple orange yellow primary danger warning success info
            var colorArr = ["small-box bg-aqua text-white", "small-box bg-fuchsia text-white", "small-box bg-red text-white", "small-box bg-purple text-white", "small-box bg-orange text-white", "small-box bg-yellow text-white"];
            var intUWUserKey;
            if ($('#hdnUWUserKey').val() != "")
                intUWUserKey = parseInt($('#hdnUWUserKey').val());
            var datafactsResponse = grpUW_GrpDashBoard.getDashBoard(intUWUserKey);  //alert(datafactsResponse.value);
            var data = JSON.parse(datafactsResponse.value);
            //alert(data.length);
            try {
                var datafacts = "";
                $.each(data, function (index, item) {
                    var i = index % 6;
                    var statusbox = "<div class='col-lg-2 col-sm-3 col-xs-6'><div class='" + colorArr[i] + "'><div class='inner text-center'><h3><a href='#' attr='ours' class='lnkStatus' style='font-size: 38px;' type='" + item.UwStatusCode + "' >" + item.TotalCases + "</a></h3><p><u><a href='#' attr='ours' class='lnkStatus' type='" + item.UwStatusCode + "' >" + item.UwStatus + "</a></u></p></div></div></div>";
                    datafacts += statusbox;
                });
                $('#divDashbord').html(datafacts);
            }
            catch (e) {
                alert(e.message);
            }

            //for displaying table on click of link
            var status = $('.lnkStatus');
            status.click(function () {
                $('#MemInfo').remove();
                //$('#MemInfo').DataTable().clear();
                var value = $(this).attr('type');
                //var type = $(this).attr('attr');
                var response;
                //if (value != '' && type =='mine')
                //{
                //    var uw_id = 1;
                //    response = grpUW_GrpDashBoard.getMemInfo(value, uw_id);
                //}
                //else if (value != '' && type == 'ours')
                //{
                //   var uw_id = 0;
                response = grpUW_GrpDashBoard.getMemInfo(value, intUWUserKey);
                //}
                //alert(response.value);
                var data = JSON.parse(response.value);
                try {
                    var htmlTable = '<table id="MemInfo" class="display"><thead><tr><th>' + "Member Application No" + '</th>'
                                + '<th>' + "Customer Name" + '</th>'
                                + '<th>' + "Policy No." + '</th>'
                                + '<th>' + "Sum Assuered" + '</th>'
                                + '<th>' + "Aging" + '</th>'
                                + '<th>' + "Current Stage" + '</th></tr></thead><tbody>';
                    $.each(data, function (index, item) {
                        //var row = "<tr><td>" + item.CustomerName + "</td><td>" + item.PolicyNo + "</td><td>" + item.SumAssuered + "</td><td>" + item.Aging + "</td><td>" + item.CurrentStage + "</td></tr>";
                        var m_id = item.MemberId;
                        var UW_DEC_ON = item.MemberId;
                        //var param = "id=\"" + c_name + "\"";
                        var row = "";
                        //if (value == 1 || value == 7)
                        row = "<tr><td><a style='cursor:default' onclick='fnCall(\"" + m_id + "\")' >" + item.ApplicationNo + "</a></td>";
                        //else if (value == 8)
                        //    row = "<tr><td><a style='cursor:default' onclick='fnPostponeCall(\"" + m_id + "\")' >" + item.MemberNo + "</a></td>";
                        //else
                        //    row = "<tr><td>" + item.MemberNo + "</td>";//<a style='cursor:default' onclick='fnCallModal(\"" + m_id + "\")' >" + item.MemberNo + "</a>

                        row += "<td>" + item.CustomerName + "</td>"
                            + "<td>" + item.PolicyCode + "</td>"
                            + "<td>" + item.SumAssured + "</td>"
                            + "<td>" + item.Aging + "</td>"
                            + "<td>" + item.CurrentStage + "</td></tr>";
                        htmlTable += row;

                    });
                    htmlTable += '</tbody></table>';
                    $('#divResults').html(htmlTable);
                    $('#MemInfo').DataTable();
                    //$('#MemInfo').DataTable({ "pagingType": "full_numbers", lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]], pageLength: 5 });
                    $('#MemInfo').addClass('table table_responsive text-center table-bordered table-striped');
                }
                catch (e) {
                    alert(e.message);
                }
                return false;
            });
        });


        function fnCall(param) {
            location.href = 'UWDecision.aspx?id=' + param;
        }
    </script>
    <style>
        .bg-title {
            background-color: #a81d22;
            color: #fff;
        }
        .small-box {min-height:130px;}        
        .loader {
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url(../dist/img/loading1.gif) 50% 50% no-repeat white;
            /* opacity: .75; */
            opacity: .90;
        }

        .small-box .inner a {
            color: #fff;
            font-size: 14px;
        }
    </style>
</head>
<!--
BODY TAG OPTIONS:
=================
Apply one or more of the following classes to get the
desired effect
|---------------------------------------------------------|
| SKINS         | skin-blue                               |
|               | skin-black                              |
|               | skin-purple                             |
|               | skin-yellow                             |
|               | skin-red                                |
|               | skin-green                              |
|---------------------------------------------------------|
|LAYOUT OPTIONS | fixed                                   |
|               | layout-boxed                            |
|               | layout-top-nav                          |
|               | sidebar-collapse                        |
|               | sidebar-mini                            |
|---------------------------------------------------------|
-->
<body class="hold-transition skin-maroon fixed">
    <form id="Form1" runat="server">
        <%--<div id="ldrModal" runat="server" class="LoaderModal">
            <div class="center">
                <img alt="" src="./dist/img/loader4.gif" />
            </div>
        </div>--%>
        <div class="wrapper">
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
            <div class="loader" id="loaderdiv"></div>
            <!-- Main Header -->
            <header class="main-header">

                <!-- Logo -->
                <a href="#" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                    <span class="logo-mini"><b>S</b>UW</span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg"><b>Saral</b>UW</span>
                </a>

                <!-- Header Navbar -->
                <nav class="navbar navbar-static-top" role="navigation">
                    <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">


                            <li class="dropdown user user-menu">

                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">

                                    <img src="../dist/img/avatar5.png" class="user-image" alt="User Image" />

                                    <span id="spanusername" runat="server" class="hidden-xs"></span>
                                </a>
                                <%--  <ul class="dropdown-menu">--%>

                                <%--   <li class="user-header">
                                        <img src="../dist/img/avatar5.png" class="img-circle" alt="User Image" />

                                    </li>--%>

                                <%--  <li class="user-body">
                                        <div class="row">
                                            <div class="col-xs-4 text-center">
                                                <a href="#">Followers</a>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <a href="#">Sales</a>
                                            </div>
                                            <div class="col-xs-4 text-center">
                                                <a href="#">Friends</a>
                                            </div>
                                        </div>

                                    </li>--%>

                                <%--   <li class="user-footer">
                                        <div class="pull-left">
                                            <a href="#" class="btn btn-default btn-flat">Profile</a>
                                        </div>
                                        <div class="pull-right">
                                            <a href="#" class="btn btn-default btn-flat">Sign out</a>
                                        </div>
                                    </li>--%>
                                <%-- </ul>--%>
                            </li>
                            <%-- <li>
                                <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
                            </li>--%>
                        </ul>
                    </div>
                </nav>
            </header>
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="main-sidebar">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">

                    <!-- Sidebar user panel (optional) -->
                    <div class="user-panel">
                        <div class="pull-left image">
                            <img src="../dist/img/avatar5.png" class="img-circle" alt="User Image" />
                        </div>
                        <div class="pull-left info">
                            <%-- <p>Dharmesh D Patel</p>--%>
                            <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                            <!-- Status -->
                            <a href="#"><i class="fa fa-circle text-success"></i>Branch</a>
                        </div>
                    </div>

                    <!-- search form (Optional) -->
                    <form action="#" method="get" class="sidebar-form">
                        <div class="input-group">
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" placeholder="search..."></asp:TextBox>
                            </div>
                            <div class="col-md-1">
                                <%--<asp:LinkButton runat="server" ID="lnkSearchAppilcation" OnClientClick="SetTargetToSelf();" OnClick="lnkSearchAppilcation_Click"><i class="fa fa-search col-md-12"></i></asp:LinkButton>--%>
                            </div>
                        </div>
                    </form>
                    <!-- /.search form -->

                    <!-- Sidebar Menu -->
                    <ul class="sidebar-menu">
                        <li class="header">Menu</li>
                        <!-- Optionally, you can add icons to the links -->
                        <%--        <li class="active"><a href="#"><i class="fa fa-link"></i> <span>Link</span></a></li>
        <li><a href="#"><i class="fa fa-link"></i> <span>Another Link</span></a></li>--%>
                        <li class="treeview">
                            <a href="#"><i class="fa fa-link"></i><span>Dashboards</span>
                                <span class="pull-right-container">
                                    <i class="fa fa-angle-left pull-right"></i>
                                </span>
                            </a>
                            <ul class="treeview-menu">
                                <li>
                                    <asp:LinkButton ID="lnkOnline" runat="server" Text="ONLINE" OnClientClick="SetTargetToSelf();" OnClick="lnkOnline_Click" CssClass="lnkButton">Online</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkoffline" runat="server" Text="OFFLINE" OnClientClick="SetTargetToSelf();" OnClick="lnkoffline_Click" CssClass="lnkButton">Branch</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkGrp" runat="server" Text="GROUP" OnClientClick="SetTargetToSelf();" OnClick="lnkGrp_Click" CssClass="lnkButton">Group</asp:LinkButton>
                                </li>
                            </ul>
                        </li>
                        
                        
                    </ul>
                    <!-- /.sidebar-menu -->
                </section>
                <!-- /.sidebar -->
            </aside>

            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Dashboards
                   <%-- <small>General</small>--%>
                    </h1>
                    <%--  <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active">General</li>
                    </ol>--%>
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
                                            <div class="row" id="divDashbord">
                                                
                                            </div>
                                            <!-- /.row -->
                                        </div>
                                    </div>
                                </div>

                                <!-- /.box-body -->
                                <div class="panel box box-success">
                                    <div class="box-header with-border">
                                        <h4 class="box-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Inbox</a>
                                        </h4>
                                    </div>
                                    <div id="collapseTwo" class="panel-collapse collapse in">
                                        <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box-body">
                                                    <div class="box">
                                                        <div class="box-header">
                                                            <h3 class="box-title">Application Details</h3>
                                                        </div>
                                                        <!-- /.box-header -->
                                                        <div id="divResults" class="box-body table-responsive">

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
                    <div><input type="hidden" id="hdnUWUserKey" value="" runat="server"/></div>
                </section>
            </div>

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
        <script src="../bootstrap/js/bootstrap.min.js"></script>
        <!-- AdminLTE App -->
        <script src="../dist/js/app.min.js"></script>

       
        <!-- SlimScroll -->
        <script src="../plugins/slimScroll/jquery.slimscroll.min.js"></script>
        <!-- FastClick -->
        <script src="../plugins/fastclick/fastclick.js"></script>

        <!-- AdminLTE for demo purposes -->
        <script src="../dist/js/demo.js"></script>
        <!-- page script -->
        
        <!-- Optionally, you can add Slimscroll and FastClick plugins.
     Both of these plugins are recommended to enhance the
     user experience. Slimscroll is required when using the
     fixed layout. -->
    </form>
</body>
</html>

